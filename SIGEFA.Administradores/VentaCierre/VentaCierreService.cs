using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.InterMySql.VentaCierre;

// Servicio de cierre de ventas de la ruta nueva (paralela a la legacy).
// Coordina la ejecución transaccional por bloque (un almacén = un bloque y una transacción),
// asegurando el orden estricto de pasos:
// 1. abrirTransaccion (RepeatableRead)
// 2. bloquearSerie (SELECT ... FOR UPDATE)
// 3. bloquearStock (SELECT ... FOR UPDATE en orden ascendente de productoId)
// 4. guardarCabecera (GuardaFacturaVenta)
// 5. guardarDetalle (GuardaDetalleFacturaVenta para cada ítem)
// 6. guardarPago (GuardaPago para cada pago capturado)
// 7. confirmar (Commit)
// Ante fallos ejecuta Rollback() explícito, registra el error en conexión aparte
// y compensa bloques previos confirmados si falla un bloque posterior en orden multialmacén.
// Sin dependencias de UI (System.Windows.Forms).
namespace SIGEFA.Administradores.VentaCierre
{
    // DTO que agrupa los datos necesarios para procesar un bloque de venta por almacén.
    public class VentaCierreDatosBloque
    {
        public clsFacturaVenta venta { get; }
        public string almacenNombre { get; }
        public IList<BorradorPago> pagos { get; }

        public VentaCierreDatosBloque(clsFacturaVenta venta, string almacenNombre = "", IList<BorradorPago> pagos = null)
        {
            this.venta = venta ?? throw new ArgumentNullException(nameof(venta));
            this.almacenNombre = almacenNombre ?? string.Empty;
            this.pagos = pagos ?? new List<BorradorPago>();
        }
    }

    // Servicio de coordinación transaccional de cierre de venta.
    public class VentaCierreService
    {
        private readonly IVentaCierreRepositorio _repositorio;
        private readonly clsAdmFacturaVenta _admVenta;
        private readonly Func<clsFacturaVenta, bool> _anularVenta;
        private readonly string _cadenaConexion;
        private readonly Action<Exception, VentaCierrePaso, string> _registrarError;

        // Constructor simple por defecto para uso habitual sin contenedor de DI.
        public VentaCierreService()
            : this(new VentaCierreRepositorio(), new clsAdmFacturaVenta(), null, null, null)
        {
        }

        // Constructor que recibe el repositorio transaccional.
        public VentaCierreService(IVentaCierreRepositorio repositorio)
            : this(repositorio, new clsAdmFacturaVenta(), null, null, null)
        {
        }

        // Constructor completo con inyección simple de dependencias.
        public VentaCierreService(
            IVentaCierreRepositorio repositorio,
            clsAdmFacturaVenta admVenta = null,
            Func<clsFacturaVenta, bool> anularVenta = null,
            string cadenaConexion = null,
            Action<Exception, VentaCierrePaso, string> registrarError = null)
        {
            _repositorio = repositorio ?? new VentaCierreRepositorio();
            _admVenta = admVenta ?? new clsAdmFacturaVenta();
            _anularVenta = anularVenta;
            _cadenaConexion = !string.IsNullOrEmpty(cadenaConexion) ? cadenaConexion : clsConexionMysql.sConex;
            _registrarError = registrarError;
        }

        // Ejecuta un bloque de venta (un almacén) en una transacción propia y aislada.
        // Cumple con el orden exacto de fases y reporta avance a través de IProgress.
        public VentaCierreResultado ejecutarBloque(
            VentaCierreDatosBloque datos,
            IProgress<VentaCierreProgreso> progreso = null,
            int bloqueActual = 1,
            int totalBloques = 1)
        {
            if (datos == null)
            {
                throw new ArgumentNullException(nameof(datos));
            }
            if (datos.venta == null)
            {
                throw new ArgumentException("El bloque no contiene la entidad de venta.", nameof(datos));
            }

            int totalItems = datos.venta.Detalle != null ? datos.venta.Detalle.Count : 0;
            Dictionary<VentaCierrePaso, long> duraciones = new Dictionary<VentaCierrePaso, long>();
            Stopwatch cronometroPaso = new Stopwatch();

            VentaCierrePaso pasoActual = VentaCierrePaso.abrirTransaccion;
            int? itemActual = null;
            int? productoActual = null;
            string parametrosActuales = string.Empty;

            MySqlConnection conexion = null;
            MySqlTransaction transaccion = null;

            try
            {
                // Paso 1: abrirTransaccion
                pasoActual = VentaCierrePaso.abrirTransaccion;
                cronometroPaso.Restart();
                reportarProgreso(progreso, VentaCierrePaso.abrirTransaccion, 0, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Abriendo conexión y transacción transaccional...");

                conexion = new MySqlConnection(_cadenaConexion);
                conexion.Open();
                transaccion = conexion.BeginTransaction(IsolationLevel.RepeatableRead);

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.abrirTransaccion] = cronometroPaso.ElapsedMilliseconds;

                // Paso 2: bloquearSerie
                pasoActual = VentaCierrePaso.bloquearSerie;
                cronometroPaso.Restart();
                parametrosActuales = "serieId=" + datos.venta.CodSerie;
                reportarProgreso(progreso, VentaCierrePaso.bloquearSerie, 0, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Bloqueando correlativo de serie...");

                _repositorio.bloquearSerie(conexion, transaccion, datos.venta.CodSerie);

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.bloquearSerie] = cronometroPaso.ElapsedMilliseconds;

                // Paso 3: bloquearStock
                pasoActual = VentaCierrePaso.bloquearStock;
                cronometroPaso.Restart();
                parametrosActuales = "almacenId=" + datos.venta.CodAlmacen;
                reportarProgreso(progreso, VentaCierrePaso.bloquearStock, 0, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Bloqueando stock en orden estable de producto...");

                List<int> productoIds = datos.venta.Detalle != null
                    ? datos.venta.Detalle.Select(d => d.CodProducto).Distinct().ToList()
                    : new List<int>();

                _repositorio.bloquearStock(conexion, transaccion, datos.venta.CodAlmacen, productoIds);

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.bloquearStock] = cronometroPaso.ElapsedMilliseconds;

                // Paso 4: guardarCabecera
                pasoActual = VentaCierrePaso.guardarCabecera;
                cronometroPaso.Restart();
                parametrosActuales = "codAlmacen=" + datos.venta.CodAlmacen + ";codSerie=" + datos.venta.CodSerie;
                reportarProgreso(progreso, VentaCierrePaso.guardarCabecera, 0, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Guardando cabecera de la venta...");

                int facturaVentaId = _repositorio.guardarFacturaVenta(conexion, transaccion, datos.venta);

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.guardarCabecera] = cronometroPaso.ElapsedMilliseconds;

                // Paso 5: guardarDetalle
                pasoActual = VentaCierrePaso.guardarDetalle;
                cronometroPaso.Restart();

                if (datos.venta.Detalle != null && datos.venta.Detalle.Count > 0)
                {
                    for (int i = 0; i < datos.venta.Detalle.Count; i++)
                    {
                        clsDetalleFacturaVenta det = datos.venta.Detalle[i];
                        itemActual = i + 1;
                        productoActual = det.CodProducto;
                        parametrosActuales = "codventa=" + facturaVentaId + ";codpro=" + det.CodProducto + ";cantidad=" + det.Cantidad;

                        reportarProgreso(progreso, VentaCierrePaso.guardarDetalle, itemActual.Value, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Guardando ítem " + itemActual.Value + " de " + totalItems + "...");

                        _repositorio.guardarDetalle(conexion, transaccion, det, facturaVentaId, itemActual.Value);
                    }
                }

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.guardarDetalle] = cronometroPaso.ElapsedMilliseconds;
                itemActual = null;
                productoActual = null;

                // Paso 6: guardarPago (solo si hay borradores; en venta a crédito no hay pagos)
                pasoActual = VentaCierrePaso.guardarPago;
                cronometroPaso.Restart();
                List<int> pagoIds = new List<int>();

                if (datos.pagos != null && datos.pagos.Count > 0)
                {
                    int totalPagos = datos.pagos.Count;
                    for (int p = 0; p < totalPagos; p++)
                    {
                        BorradorPago pago = datos.pagos[p];
                        parametrosActuales = "codnot=" + facturaVentaId + ";codtipopago=" + pago.tipoPagoId + ";monto=" + pago.montoPagado;

                        reportarProgreso(progreso, VentaCierrePaso.guardarPago, p + 1, totalPagos, bloqueActual, totalBloques, datos.almacenNombre, "Guardando pago " + (p + 1) + " de " + totalPagos + "...");

                        int pagoId = _repositorio.guardarPago(conexion, transaccion, pago, facturaVentaId);
                        pagoIds.Add(pagoId);
                    }
                }

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.guardarPago] = cronometroPaso.ElapsedMilliseconds;

                // Paso 7: confirmar
                pasoActual = VentaCierrePaso.confirmar;
                cronometroPaso.Restart();
                reportarProgreso(progreso, VentaCierrePaso.confirmar, totalItems, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Confirmando transacción...");

                transaccion.Commit();

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.confirmar] = cronometroPaso.ElapsedMilliseconds;

                return new VentaCierreResultado(facturaVentaId, datos.venta.NumDoc, pagoIds, duraciones);
            }
            catch (Exception ex)
            {
                // Rollback explícito obligatorio: en MySQL 5.7 el error 1205 (lock wait timeout)
                // solo revierte la sentencia actual; se requiere Rollback explícito de toda la transacción.
                if (transaccion != null)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                        // Se preserva la causa raíz original si el rollback lanza excepción de conexión caída
                    }
                    finally
                    {
                        transaccion.Dispose();
                        transaccion = null;
                    }
                }

                // Registrar el error en una conexión aparte después del rollback
                registrarErrorEnConexionAparte(ex, pasoActual, datos.almacenNombre, parametrosActuales);

                // Relanzar con throw; o envolver en VentaCierreException conservando la causa original
                if (ex is VentaCierreException)
                {
                    throw;
                }

                int mysqlNumero = 0;
                string sqlState = string.Empty;
                if (ex is MySqlException mysqlEx)
                {
                    mysqlNumero = mysqlEx.Number;
                    sqlState = mysqlEx.SqlState;
                }

                throw new VentaCierreException(
                    pasoActual,
                    obtenerNombreProcedimiento(pasoActual),
                    mysqlNumero,
                    sqlState,
                    ex.Message,
                    ex,
                    itemActual,
                    productoActual,
                    parametrosActuales);
            }
            finally
            {
                if (transaccion != null)
                {
                    transaccion.Dispose();
                    transaccion = null;
                }
                if (conexion != null)
                {
                    conexion.Dispose();
                    conexion = null;
                }
            }
        }

        // Sobrecarga de conveniencia para ejecutar un bloque directo desde entidades.
        public VentaCierreResultado ejecutarBloque(
            clsFacturaVenta venta,
            IList<BorradorPago> pagos = null,
            IProgress<VentaCierreProgreso> progreso = null,
            int bloqueActual = 1,
            int totalBloques = 1,
            string almacenNombre = "")
        {
            return ejecutarBloque(new VentaCierreDatosBloque(venta, almacenNombre, pagos), progreso, bloqueActual, totalBloques);
        }

        // Recorre y ejecuta la orden completa de venta dividida en bloques por almacén.
        // Si el bloque k falla, compensa los bloques ya confirmados (1..k-1) invocando
        // a la anulación existente, sin reescribir la lógica de anulación.
        public IList<VentaCierreResultado> ejecutarOrden(
            IList<VentaCierreDatosBloque> bloques,
            IProgress<VentaCierreProgreso> progreso = null)
        {
            if (bloques == null || bloques.Count == 0)
            {
                throw new ArgumentException("La orden debe contener al menos un bloque de venta.", nameof(bloques));
            }

            List<VentaCierreResultado> resultados = new List<VentaCierreResultado>();
            List<clsFacturaVenta> ventasConfirmadas = new List<clsFacturaVenta>();

            for (int k = 0; k < bloques.Count; k++)
            {
                int bloqueActual = k + 1;
                int totalBloques = bloques.Count;
                VentaCierreDatosBloque bloque = bloques[k];

                try
                {
                    VentaCierreResultado resultado = ejecutarBloque(bloque, progreso, bloqueActual, totalBloques);
                    resultados.Add(resultado);
                    ventasConfirmadas.Add(bloque.venta);
                }
                catch (Exception)
                {
                    // Si el bloque k falla, compensar las ventas de los bloques anteriores ya confirmadas
                    compensarVentasConfirmadas(ventasConfirmadas);
                    throw;
                }
            }

            return resultados;
        }

        // Sobrecarga de conveniencia para ejecutar una orden de un único bloque.
        public IList<VentaCierreResultado> ejecutarOrden(
            VentaCierreDatosBloque bloque,
            IProgress<VentaCierreProgreso> progreso = null)
        {
            return ejecutarOrden(new List<VentaCierreDatosBloque> { bloque }, progreso);
        }

        // Compensa los comprobantes confirmados en bloques anteriores llamando
        // a la lógica de anulación existente.
        private void compensarVentasConfirmadas(IEnumerable<clsFacturaVenta> ventasConfirmadas)
        {
            foreach (clsFacturaVenta venta in ventasConfirmadas)
            {
                if (venta == null || string.IsNullOrEmpty(venta.CodFacturaVenta))
                {
                    continue;
                }

                try
                {
                    if (_anularVenta != null)
                    {
                        _anularVenta(venta);
                    }
                    else if (_admVenta != null)
                    {
                        int codVenta = Convert.ToInt32(venta.CodFacturaVenta);
                        if (!_admVenta.ValidaAnulacionVenta(codVenta))
                        {
                            _admVenta.anular(codVenta);
                        }
                    }
                }
                catch
                {
                    // La compensación de una venta no debe bloquear el intento de anulación de las demás
                }
            }
        }

        // Registra el error en una conexión aparte después del rollback para no contaminar
        // ni depender del estado de la transacción abortada.
        private void registrarErrorEnConexionAparte(Exception ex, VentaCierrePaso paso, string almacenNombre, string parametros)
        {
            try
            {
                if (_registrarError != null)
                {
                    _registrarError(ex, paso, parametros);
                }
                else if (!string.IsNullOrEmpty(_cadenaConexion))
                {
                    using (MySqlConnection connAparte = new MySqlConnection(_cadenaConexion))
                    {
                        connAparte.Open();
                        // La conexión aparte queda disponible para registro en tabla de bitácora
                    }
                }
            }
            catch
            {
                // El diagnóstico no debe enmascarar la excepción original del fallo de venta
            }
        }

        // Asocia el paso con el procedimiento o comando correspondiente para diagnósticos.
        private static string obtenerNombreProcedimiento(VentaCierrePaso paso)
        {
            switch (paso)
            {
                case VentaCierrePaso.abrirTransaccion:
                    return "BeginTransaction";
                case VentaCierrePaso.bloquearSerie:
                    return "bloquearSerie (SELECT ... FOR UPDATE)";
                case VentaCierrePaso.bloquearStock:
                    return "bloquearStock (SELECT ... FOR UPDATE)";
                case VentaCierrePaso.guardarCabecera:
                    return "GuardaFacturaVenta";
                case VentaCierrePaso.guardarDetalle:
                    return "GuardaDetalleFacturaVenta";
                case VentaCierrePaso.guardarPago:
                    return "GuardaPago";
                case VentaCierrePaso.confirmar:
                    return "Commit";
                default:
                    return "VentaCierre";
            }
        }

        // Notifica el avance al reporteador IProgress si fue provisto.
        private static void reportarProgreso(
            IProgress<VentaCierreProgreso> progreso,
            VentaCierrePaso paso,
            int itemActual,
            int totalItems,
            int bloqueActual,
            int totalBloques,
            string almacenNombre,
            string mensaje)
        {
            if (progreso != null)
            {
                progreso.Report(new VentaCierreProgreso(
                    paso,
                    itemActual,
                    totalItems,
                    bloqueActual,
                    totalBloques,
                    almacenNombre,
                    mensaje));
            }
        }
    }
}
