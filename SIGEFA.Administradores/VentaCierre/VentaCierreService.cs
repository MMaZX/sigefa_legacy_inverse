using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.InterMySql.VentaCierre;

// Servicio de cierre de ventas de la ruta nueva (paralela a la legacy).
// Coordina la ejecución transaccional por bloque (un almacén = un bloque y una transacción),
// asegurando el orden estricto de fases:
// 1. abrirTransaccion (RepeatableRead)
// 2. bloquearSerie (SELECT ... FOR UPDATE)
// 3. bloquearStock (SELECT ... FOR UPDATE en orden ascendente de productoId y revalidación)
// 4. guardarCabecera (GuardaFacturaVenta)
// 5. guardarDetalle (GuardaDetalleFacturaVenta para cada ítem)
// 6. guardarPago (GuardaPago para cada pago capturado)
// 7. confirmar (Commit)
// Ante fallos ejecuta Rollback() explícito, restaura las entidades, registra el error localmente
// en %LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log y compensa bloques previos confirmados
// reportando explícitamente si alguna anulación no pudo completarse.
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

        // Constructor con visibilidad internal para evitar error de accesibilidad CS0051
        // dado que clsAdmFacturaVenta tiene modificador internal en el ensamblado.
        internal VentaCierreService(
            IVentaCierreRepositorio repositorio,
            clsAdmFacturaVenta admVenta,
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

            // Hallazgo 5: Respaldar estado previo para garantizar que las entidades
            // solo reciban ids tras Commit o se restauren intactas ante aborto.
            string codFacturaVentaOriginal = datos.venta.CodFacturaVenta;
            string numDocOriginal = datos.venta.NumDoc;
            List<int> codDetallesOriginales = new List<int>();
            if (datos.venta.Detalle != null)
            {
                foreach (clsDetalleFacturaVenta d in datos.venta.Detalle)
                {
                    if (d != null)
                    {
                        codDetallesOriginales.Add(d.CodDetalleVenta);
                    }
                }
            }

            MySqlConnection conexion = null;
            MySqlTransaction transaccion = null;

            int facturaVentaId = 0;
            string numeroDocumentoGenerado = null;
            List<int> detalleIds = new List<int>();
            List<int> pagoIds = new List<int>();

            try
            {
                // Validación de la colección Detalle dentro del flujo protegido para que cualquier error
                // se capture y reporte exactamente como VentaCierreException
                if (datos.venta.Detalle == null || datos.venta.Detalle.Count == 0)
                {
                    throw new VentaCierreException(
                        VentaCierrePaso.abrirTransaccion,
                        "ValidarDetalle",
                        0,
                        string.Empty,
                        "La colección Detalle de la venta es nula o no contiene elementos.",
                        null,
                        null,
                        null,
                        "codAlmacen=" + datos.venta.CodAlmacen);
                }

                for (int i = 0; i < datos.venta.Detalle.Count; i++)
                {
                    if (datos.venta.Detalle[i] == null)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarDetalle,
                            "ValidarDetalle",
                            0,
                            string.Empty,
                            "El elemento en la posición " + (i + 1) + " de la colección Detalle es nulo.",
                            null,
                            i + 1,
                            null,
                            "codAlmacen=" + datos.venta.CodAlmacen + ";itemIndice=" + (i + 1));
                    }
                }

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

                // Paso 3: bloquearStock (bloqueo por PK y revalidación de almacén/producto con FOR UPDATE)
                pasoActual = VentaCierrePaso.bloquearStock;
                cronometroPaso.Restart();
                parametrosActuales = "almacenId=" + datos.venta.CodAlmacen;
                reportarProgreso(progreso, VentaCierrePaso.bloquearStock, 0, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Bloqueando stock en orden estable de producto...");

                List<int> productoIds = datos.venta.Detalle
                    .Where(d => d != null)
                    .Select(d => d.CodProducto)
                    .Distinct()
                    .ToList();

                _repositorio.bloquearStock(conexion, transaccion, datos.venta.CodAlmacen, productoIds);

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.bloquearStock] = cronometroPaso.ElapsedMilliseconds;

                // Paso 4: guardarCabecera
                pasoActual = VentaCierrePaso.guardarCabecera;
                cronometroPaso.Restart();
                parametrosActuales = "codAlmacen=" + datos.venta.CodAlmacen + ";codSerie=" + datos.venta.CodSerie;
                reportarProgreso(progreso, VentaCierrePaso.guardarCabecera, 0, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Guardando la venta...");

                facturaVentaId = _repositorio.guardarFacturaVenta(conexion, transaccion, datos.venta, out numeroDocumentoGenerado);

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.guardarCabecera] = cronometroPaso.ElapsedMilliseconds;

                // Paso 5: guardarDetalle
                pasoActual = VentaCierrePaso.guardarDetalle;
                cronometroPaso.Restart();

                for (int i = 0; i < datos.venta.Detalle.Count; i++)
                {
                    clsDetalleFacturaVenta det = datos.venta.Detalle[i];
                    itemActual = i + 1;
                    productoActual = det.CodProducto;
                    parametrosActuales = "codventa=" + facturaVentaId + ";codpro=" + det.CodProducto + ";cantidad=" + det.Cantidad;

                    reportarProgreso(progreso, VentaCierrePaso.guardarDetalle, itemActual.Value, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Guardando ítem " + itemActual.Value + " de " + totalItems + "...");

                    int detId = _repositorio.guardarDetalle(conexion, transaccion, det, facturaVentaId, itemActual.Value);
                    detalleIds.Add(detId);
                }

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.guardarDetalle] = cronometroPaso.ElapsedMilliseconds;
                itemActual = null;
                productoActual = null;

                // Paso 6: reservarNotaCredito (solo si hay borradores con notaCreditoId > 0)
                pasoActual = VentaCierrePaso.reservarNotaCredito;
                cronometroPaso.Restart();

                if (datos.pagos != null && datos.pagos.Count > 0)
                {
                    List<int> ncsDistintas = datos.pagos
                        .Where(p => p != null && p.notaCreditoId > 0)
                        .Select(p => p.notaCreditoId)
                        .Distinct()
                        .OrderBy(id => id)
                        .ToList();

                    for (int n = 0; n < ncsDistintas.Count; n++)
                    {
                        int ncId = ncsDistintas[n];
                        decimal sumaCobradoNC = datos.pagos
                            .Where(p => p != null && p.notaCreditoId == ncId)
                            .Sum(p => p.montoCobrado);

                        parametrosActuales = "notaCreditoId=" + ncId + ";sumaCobrado=" + sumaCobradoNC;
                        reportarProgreso(progreso, VentaCierrePaso.reservarNotaCredito, n + 1, ncsDistintas.Count, bloqueActual, totalBloques, datos.almacenNombre, "Validando nota de crédito " + (n + 1) + " de " + ncsDistintas.Count + "...");

                        decimal pendienteActual = _repositorio.bloquearNotaCredito(conexion, transaccion, ncId);
                        if (sumaCobradoNC > pendienteActual)
                        {
                            throw new VentaCierreException(
                                VentaCierrePaso.reservarNotaCredito,
                                "bloquearNotaCredito",
                                0,
                                string.Empty,
                                "Saldo insuficiente en nota de crédito ID " + ncId + ": monto a aplicar (" + sumaCobradoNC + ") supera el saldo pendiente (" + pendienteActual + ").",
                                null,
                                null,
                                null,
                                parametrosActuales + ";pendiente=" + pendienteActual);
                        }
                    }
                }

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.reservarNotaCredito] = cronometroPaso.ElapsedMilliseconds;

                // Paso 7: guardarPago (solo si hay borradores; en venta a crédito no hay pagos)
                pasoActual = VentaCierrePaso.guardarPago;
                cronometroPaso.Restart();

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

                // Paso 8: confirmar
                pasoActual = VentaCierrePaso.confirmar;
                cronometroPaso.Restart();
                reportarProgreso(progreso, VentaCierrePaso.confirmar, totalItems, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Confirmando transacción...");

                transaccion.Commit();

                cronometroPaso.Stop();
                duraciones[VentaCierrePaso.confirmar] = cronometroPaso.ElapsedMilliseconds;

                // Hallazgo 5: Asignar identificadores y número de documento a las entidades
                // ÚNICAMENTE después de confirmar la transacción exitosamente (Commit).
                datos.venta.CodFacturaVenta = facturaVentaId.ToString();
                datos.venta.NumDoc = numeroDocumentoGenerado;

                for (int i = 0; i < datos.venta.Detalle.Count && i < detalleIds.Count; i++)
                {
                    datos.venta.Detalle[i].CodDetalleVenta = detalleIds[i];
                }

                return new VentaCierreResultado(facturaVentaId, numeroDocumentoGenerado, pagoIds, duraciones);
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

                // Hallazgo 5: Restaurar estado previo de las entidades para no dejar asignados ids abortados
                if (datos != null && datos.venta != null)
                {
                    datos.venta.CodFacturaVenta = codFacturaVentaOriginal;
                    datos.venta.NumDoc = numDocOriginal;

                    if (datos.venta.Detalle != null)
                    {
                        for (int i = 0; i < datos.venta.Detalle.Count && i < codDetallesOriginales.Count; i++)
                        {
                            if (datos.venta.Detalle[i] != null)
                            {
                                datos.venta.Detalle[i].CodDetalleVenta = codDetallesOriginales[i];
                            }
                        }
                    }
                }

                // Hallazgo 2: Registrar el error en archivo local %LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log
                registrarErrorLocal(ex, pasoActual, obtenerNombreProcedimiento(pasoActual), itemActual, productoActual, parametrosActuales);

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
        // a la anulación existente. Si alguna anulación falla (false o excepción), informa
        // explícitamente qué bloques quedaron sin compensar conservando la causa original.
        public IList<VentaCierreResultado> ejecutarOrden(
            IList<VentaCierreDatosBloque> bloques,
            IProgress<VentaCierreProgreso> progreso = null)
        {
            if (bloques == null || bloques.Count == 0)
            {
                throw new ArgumentException("La orden debe contener al menos un bloque de venta.", nameof(bloques));
            }

            List<VentaCierreResultado> resultados = new List<VentaCierreResultado>();
            List<VentaCierreDatosBloque> bloquesConfirmados = new List<VentaCierreDatosBloque>();

            for (int k = 0; k < bloques.Count; k++)
            {
                int bloqueActual = k + 1;
                int totalBloques = bloques.Count;
                VentaCierreDatosBloque bloque = bloques[k];

                try
                {
                    VentaCierreResultado resultado = ejecutarBloque(bloque, progreso, bloqueActual, totalBloques);
                    resultados.Add(resultado);
                    bloquesConfirmados.Add(bloque);
                }
                catch (Exception ex)
                {
                    // Hallazgo 1: Si el bloque k falla, compensar las ventas ya confirmadas y registrar
                    // el resultado de cada anulación. Si alguna falla, relanzar informando los bloques no compensados.
                    compensarBloquesConfirmados(bloquesConfirmados, ex);
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

        // T13c: Cierre multialmacén atómico en una sola transacción explícita (RepeatableRead).
        // Bloquea todas las series distintas en orden ascendente de serieId, luego todo el stock
        // en orden ascendente de (almacenId, productoId), y luego procesa bloque por bloque (cabecera,
        // detalle y pagos). Si dos bloques comparten serie, la numeración avanza correlativa dentro de
        // la misma transacción. Un solo Commit al final. Ante cualquier fallo ejecuta Rollback completo
        // y restaura las entidades sin compensación posterior.
        public IList<VentaCierreResultado> ejecutarOrdenAtomica(
            IList<VentaCierreDatosBloque> bloques,
            IProgress<VentaCierreProgreso> progreso = null)
        {
            if (bloques == null || bloques.Count == 0)
            {
                throw new ArgumentException("La orden debe contener al menos un bloque de venta.", nameof(bloques));
            }

            int totalBloques = bloques.Count;

            // Validación previa de todos los bloques
            for (int k = 0; k < totalBloques; k++)
            {
                VentaCierreDatosBloque b = bloques[k];
                if (b == null || b.venta == null)
                {
                    throw new ArgumentException("El bloque en la posición " + (k + 1) + " no contiene una entidad de venta válida.", nameof(bloques));
                }

                if (b.venta.Detalle == null || b.venta.Detalle.Count == 0)
                {
                    throw new VentaCierreException(
                        VentaCierrePaso.abrirTransaccion,
                        "ValidarDetalle",
                        0,
                        string.Empty,
                        "La colección Detalle del bloque " + (k + 1) + " es nula o no contiene elementos.",
                        null,
                        null,
                        null,
                        "bloque=" + (k + 1) + ";codAlmacen=" + b.venta.CodAlmacen);
                }

                for (int i = 0; i < b.venta.Detalle.Count; i++)
                {
                    if (b.venta.Detalle[i] == null)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarDetalle,
                            "ValidarDetalle",
                            0,
                            string.Empty,
                            "El elemento en la posición " + (i + 1) + " de la colección Detalle del bloque " + (k + 1) + " es nulo.",
                            null,
                            i + 1,
                            null,
                            "bloque=" + (k + 1) + ";item=" + (i + 1));
                    }
                }
            }

            // Respaldar estado previo de todas las entidades para restauración ante rollback
            List<EstadoOriginalBloque> estadosOriginales = new List<EstadoOriginalBloque>();
            foreach (VentaCierreDatosBloque b in bloques)
            {
                EstadoOriginalBloque est = new EstadoOriginalBloque
                {
                    venta = b.venta,
                    codFacturaVenta = b.venta.CodFacturaVenta,
                    numDoc = b.venta.NumDoc,
                    codDetalles = b.venta.Detalle.Select(d => d.CodDetalleVenta).ToList()
                };
                estadosOriginales.Add(est);
            }

            VentaCierrePaso pasoActual = VentaCierrePaso.abrirTransaccion;
            int? itemActual = null;
            int? productoActual = null;
            string parametrosActuales = string.Empty;
            int bloqueActualError = 1;

            MySqlConnection conexion = null;
            MySqlTransaction transaccion = null;

            List<BloqueEjecutadoInfo> bloquesEjecutados = new List<BloqueEjecutadoInfo>();

            try
            {
                conexion = new MySqlConnection(_cadenaConexion);
                conexion.Open();
                transaccion = conexion.BeginTransaction(IsolationLevel.RepeatableRead);

                reportarProgreso(progreso, VentaCierrePaso.abrirTransaccion, 0, 0, 1, totalBloques, "Global", "Abriendo transacción atómica RepeatableRead...");

                // 1. Bloqueo global de series distintas en orden determinístico ascendente
                pasoActual = VentaCierrePaso.bloquearSerie;
                List<int> seriesDistintas = bloques
                    .Where(b => b.venta != null && b.venta.CodSerie > 0)
                    .Select(b => b.venta.CodSerie)
                    .Distinct()
                    .OrderBy(id => id)
                    .ToList();

                reportarProgreso(progreso, VentaCierrePaso.bloquearSerie, 0, 0, 1, totalBloques, "Global", "Bloqueando series en orden ascendente...");
                foreach (int serieId in seriesDistintas)
                {
                    parametrosActuales = "codSerie=" + serieId;
                    _repositorio.bloquearSerie(conexion, transaccion, serieId);
                }

                // 2. Bloqueo global de stock en orden ascendente de (almacenId, productoId)
                pasoActual = VentaCierrePaso.bloquearStock;
                List<int> almacenesDistintos = bloques
                    .Where(b => b.venta != null && b.venta.CodAlmacen > 0)
                    .Select(b => b.venta.CodAlmacen)
                    .Distinct()
                    .OrderBy(a => a)
                    .ToList();

                reportarProgreso(progreso, VentaCierrePaso.bloquearStock, 0, 0, 1, totalBloques, "Global", "Bloqueando stock en orden estable de almacén y producto...");
                foreach (int almId in almacenesDistintos)
                {
                    List<int> productosDelAlmacen = bloques
                        .Where(b => b.venta != null && b.venta.CodAlmacen == almId && b.venta.Detalle != null)
                        .SelectMany(b => b.venta.Detalle)
                        .Where(d => d != null)
                        .Select(d => d.CodProducto)
                        .Distinct()
                        .OrderBy(p => p)
                        .ToList();

                    if (productosDelAlmacen.Count > 0)
                    {
                        parametrosActuales = "codAlmacen=" + almId + ";productos=" + string.Join(",", productosDelAlmacen);
                        _repositorio.bloquearStock(conexion, transaccion, almId, productosDelAlmacen);
                    }
                }

                // 3. Procesamiento bloque por bloque
                for (int k = 0; k < totalBloques; k++)
                {
                    VentaCierreDatosBloque datos = bloques[k];
                    int bloqueActual = k + 1;
                    bloqueActualError = bloqueActual;
                    int totalItems = datos.venta.Detalle.Count;

                    BloqueEjecutadoInfo info = new BloqueEjecutadoInfo(datos);

                    // Guardar cabecera
                    pasoActual = VentaCierrePaso.guardarCabecera;
                    itemActual = null;
                    productoActual = null;
                    parametrosActuales = "bloque=" + bloqueActual + ";codAlmacen=" + datos.venta.CodAlmacen + ";codSerie=" + datos.venta.CodSerie;
                    reportarProgreso(progreso, VentaCierrePaso.guardarCabecera, 0, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Guardando la venta...");

                    string numDocGen;
                    int factId = _repositorio.guardarFacturaVenta(conexion, transaccion, datos.venta, out numDocGen);
                    info.facturaVentaId = factId;
                    info.numeroDocumentoGenerado = numDocGen;

                    // Guardar detalle
                    pasoActual = VentaCierrePaso.guardarDetalle;
                    for (int i = 0; i < datos.venta.Detalle.Count; i++)
                    {
                        clsDetalleFacturaVenta det = datos.venta.Detalle[i];
                        itemActual = i + 1;
                        productoActual = det.CodProducto;
                        parametrosActuales = "bloque=" + bloqueActual + ";codventa=" + factId + ";codpro=" + det.CodProducto + ";cantidad=" + det.Cantidad;

                        reportarProgreso(progreso, VentaCierrePaso.guardarDetalle, itemActual.Value, totalItems, bloqueActual, totalBloques, datos.almacenNombre, "Guardando ítem " + itemActual.Value + " de " + totalItems + "...");
                        int detId = _repositorio.guardarDetalle(conexion, transaccion, det, factId, itemActual.Value);
                        info.detalleIds.Add(detId);
                    }
                    itemActual = null;
                    productoActual = null;

                    // Validar y reservar notas de crédito del bloque
                    pasoActual = VentaCierrePaso.reservarNotaCredito;
                    if (datos.pagos != null && datos.pagos.Count > 0)
                    {
                        List<int> ncsDistintasBloque = datos.pagos
                            .Where(p => p != null && p.notaCreditoId > 0)
                            .Select(p => p.notaCreditoId)
                            .Distinct()
                            .OrderBy(id => id)
                            .ToList();

                        for (int n = 0; n < ncsDistintasBloque.Count; n++)
                        {
                            int ncId = ncsDistintasBloque[n];
                            decimal sumaCobradoNC = datos.pagos
                                .Where(p => p != null && p.notaCreditoId == ncId)
                                .Sum(p => p.montoCobrado);

                            itemActual = n + 1;
                            parametrosActuales = "bloque=" + bloqueActual + ";notaCreditoId=" + ncId + ";sumaCobrado=" + sumaCobradoNC;
                            reportarProgreso(progreso, VentaCierrePaso.reservarNotaCredito, itemActual.Value, ncsDistintasBloque.Count, bloqueActual, totalBloques, datos.almacenNombre, "Validando nota de crédito " + itemActual.Value + " de " + ncsDistintasBloque.Count + "...");

                            decimal pendienteActual = _repositorio.bloquearNotaCredito(conexion, transaccion, ncId);
                            if (sumaCobradoNC > pendienteActual)
                            {
                                throw new VentaCierreException(
                                    VentaCierrePaso.reservarNotaCredito,
                                    "bloquearNotaCredito",
                                    0,
                                    string.Empty,
                                    "Saldo insuficiente en nota de crédito ID " + ncId + " (bloque " + bloqueActual + "): monto a aplicar (" + sumaCobradoNC + ") supera el saldo pendiente (" + pendienteActual + ").",
                                    null,
                                    null,
                                    null,
                                    parametrosActuales + ";pendiente=" + pendienteActual);
                            }
                        }
                    }
                    itemActual = null;

                    // Guardar pagos
                    pasoActual = VentaCierrePaso.guardarPago;
                    if (datos.pagos != null && datos.pagos.Count > 0)
                    {
                        int totalPagos = datos.pagos.Count;
                        for (int p = 0; p < totalPagos; p++)
                        {
                            BorradorPago pago = datos.pagos[p];
                            itemActual = p + 1;
                            parametrosActuales = "bloque=" + bloqueActual + ";codnot=" + factId + ";codtipopago=" + pago.tipoPagoId + ";monto=" + pago.montoPagado;

                            reportarProgreso(progreso, VentaCierrePaso.guardarPago, itemActual.Value, totalPagos, bloqueActual, totalBloques, datos.almacenNombre, "Guardando pago " + itemActual.Value + " de " + totalPagos + "...");
                            int pId = _repositorio.guardarPago(conexion, transaccion, pago, factId);
                            info.pagoIds.Add(pId);
                        }
                    }
                    itemActual = null;

                    bloquesEjecutados.Add(info);
                }

                // 4. Confirmar transacción única
                pasoActual = VentaCierrePaso.confirmar;
                itemActual = null;
                productoActual = null;
                parametrosActuales = "totalBloques=" + totalBloques;
                reportarProgreso(progreso, VentaCierrePaso.confirmar, 0, 0, totalBloques, totalBloques, "Global", "Confirmando la venta...");

                transaccion.Commit();

                // 5. Asignar identificadores generados a las entidades solo tras Commit
                List<VentaCierreResultado> resultados = new List<VentaCierreResultado>();
                foreach (BloqueEjecutadoInfo be in bloquesEjecutados)
                {
                    be.datos.venta.CodFacturaVenta = be.facturaVentaId.ToString();
                    be.datos.venta.NumDoc = be.numeroDocumentoGenerado;
                    for (int i = 0; i < be.datos.venta.Detalle.Count && i < be.detalleIds.Count; i++)
                    {
                        be.datos.venta.Detalle[i].CodDetalleVenta = be.detalleIds[i];
                    }

                    resultados.Add(new VentaCierreResultado(
                        be.facturaVentaId,
                        be.numeroDocumentoGenerado,
                        be.pagoIds,
                        new Dictionary<VentaCierrePaso, long>()));
                }

                return resultados;
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                        // El fallo en rollback no debe ocultar la causa original
                    }
                }

                // Restaurar entidades
                foreach (EstadoOriginalBloque est in estadosOriginales)
                {
                    if (est.venta != null)
                    {
                        est.venta.CodFacturaVenta = est.codFacturaVenta;
                        est.venta.NumDoc = est.numDoc;
                        if (est.venta.Detalle != null)
                        {
                            for (int i = 0; i < est.venta.Detalle.Count && i < est.codDetalles.Count; i++)
                            {
                                if (est.venta.Detalle[i] != null)
                                {
                                    est.venta.Detalle[i].CodDetalleVenta = est.codDetalles[i];
                                }
                            }
                        }
                    }
                }

                registrarErrorLocal(ex, pasoActual, obtenerNombreProcedimiento(pasoActual), itemActual, productoActual, parametrosActuales + ";bloque=" + bloqueActualError);

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
                    "Error al ejecutar orden atómica en bloque " + bloqueActualError + " (" + pasoActual + "): " + ex.Message,
                    ex,
                    itemActual,
                    productoActual,
                    parametrosActuales + ";bloque=" + bloqueActualError);
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
                    conexion.Close();
                    conexion.Dispose();
                    conexion = null;
                }
            }
        }

        private class EstadoOriginalBloque
        {
            public clsFacturaVenta venta;
            public string codFacturaVenta;
            public string numDoc;
            public List<int> codDetalles = new List<int>();
        }

        private class BloqueEjecutadoInfo
        {
            public VentaCierreDatosBloque datos;
            public int facturaVentaId;
            public string numeroDocumentoGenerado;
            public List<int> detalleIds = new List<int>();
            public List<int> pagoIds = new List<int>();

            public BloqueEjecutadoInfo(VentaCierreDatosBloque datos)
            {
                this.datos = datos;
            }
        }

        // Hallazgo 1: Compensa los comprobantes confirmados en bloques anteriores llamando
        // a la anulación existente, registrando el resultado de cada anulación. Si alguna falla
        // (false, excepción o datos incompletos), lanza una excepción que detalla explícitamente qué bloques
        // quedaron sin compensar, conservando la causa original como InnerException.
        private void compensarBloquesConfirmados(IList<VentaCierreDatosBloque> bloquesConfirmados, Exception causaOriginal)
        {
            if (bloquesConfirmados == null || bloquesConfirmados.Count == 0)
            {
                return;
            }

            List<string> fallosCompensacion = new List<string>();

            for (int i = 0; i < bloquesConfirmados.Count; i++)
            {
                VentaCierreDatosBloque bloque = bloquesConfirmados[i];
                clsFacturaVenta venta = bloque != null ? bloque.venta : null;

                // Si un bloque confirmado no tiene venta o id, se registra como no compensado
                // e incluye su índice en la excepción final.
                if (venta == null || string.IsNullOrEmpty(venta.CodFacturaVenta))
                {
                    string nombreAlm = (bloque != null && !string.IsNullOrEmpty(bloque.almacenNombre))
                        ? bloque.almacenNombre
                        : (venta != null ? venta.CodAlmacen.ToString() : "desconocido");

                    fallosCompensacion.Add(string.Format(
                        "Bloque {0} (Almacén: '{1}', FacturaVentaId: {2}): bloque confirmado sin entidad de venta o identificador de factura para anular",
                        i + 1,
                        nombreAlm,
                        venta != null ? (venta.CodFacturaVenta ?? "null") : "null"));
                    continue;
                }

                bool exito = false;
                string motivoFallo = null;

                try
                {
                    if (_anularVenta != null)
                    {
                        exito = _anularVenta(venta);
                        if (!exito)
                        {
                            motivoFallo = "el delegado de anulación devolvió false";
                        }
                    }
                    else if (_admVenta != null)
                    {
                        int codVenta = Convert.ToInt32(venta.CodFacturaVenta);
                        if (!_admVenta.ValidaAnulacionVenta(codVenta))
                        {
                            exito = _admVenta.anular(codVenta);
                            if (!exito)
                            {
                                motivoFallo = "clsAdmFacturaVenta.anular devolvió false";
                            }
                        }
                        else
                        {
                            // La venta ya figuraba como anulada en base de datos
                            exito = true;
                        }
                    }
                    else
                    {
                        motivoFallo = "no se configuró un mecanismo de anulación";
                    }
                }
                catch (Exception anulaEx)
                {
                    exito = false;
                    motivoFallo = "excepción al anular: " + anulaEx.Message;
                }

                if (!exito)
                {
                    string nombreAlmacen = !string.IsNullOrEmpty(bloque.almacenNombre)
                        ? bloque.almacenNombre
                        : venta.CodAlmacen.ToString();

                    fallosCompensacion.Add(string.Format(
                        "Bloque {0} (Almacén: '{1}', FacturaVentaId: {2}): {3}",
                        i + 1,
                        nombreAlmacen,
                        venta.CodFacturaVenta,
                        motivoFallo ?? "fallo no especificado"));
                }
            }

            if (fallosCompensacion.Count > 0)
            {
                string mensajeCompensacion = string.Format(
                    "Fallo al procesar la orden y la compensación posterior fue incompleta. Los siguientes bloques confirmados NO pudieron anularse: [{0}]. Causa raíz: {1}",
                    string.Join("; ", fallosCompensacion),
                    causaOriginal != null ? causaOriginal.Message : "desconocida");

                if (causaOriginal is VentaCierreException vcEx)
                {
                    throw new VentaCierreException(
                        vcEx.paso,
                        vcEx.procedimiento,
                        vcEx.mysqlNumero,
                        vcEx.sqlState,
                        mensajeCompensacion,
                        causaOriginal,
                        vcEx.itemIndice,
                        vcEx.productoId,
                        vcEx.parametros);
                }

                throw new InvalidOperationException(mensajeCompensacion, causaOriginal);
            }
        }

        // Enmascara patrones comunes de credenciales como Pwd=..., Password=..., Uid=..., User Id=...
        private static string enmascararCredenciales(string texto)
        {
            return VentaCierreRegistroErrores.enmascararCredenciales(texto);
        }

        // Registra el error en un archivo local %LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log
        // a través de VentaCierreRegistroErrores.
        // Si la escritura en el archivo falla, no debe ocultar la excepción original.
        private void registrarErrorLocal(Exception ex, VentaCierrePaso paso, string procedimiento, int? itemIndice, int? productoId, string parametros)
        {
            try
            {
                if (_registrarError != null)
                {
                    _registrarError(ex, paso, parametros);
                    return;
                }

                VentaCierreRegistroErrores.registrarErrorTransaccional(ex, paso, procedimiento, itemIndice, productoId, parametros);
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
                case VentaCierrePaso.reservarNotaCredito:
                    return "bloquearNotaCredito (SELECT pendiente FROM notacredito FOR UPDATE)";
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
