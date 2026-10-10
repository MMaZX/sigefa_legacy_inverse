using System;
using System.Collections.Generic;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Integración con rollback SIEMPRE (T5b-2): crea un requerimiento de venta pendiente dentro de la
// transacción, lo aprueba con ReqVentaAprobacion.AprobarEn y verifica columna por columna la cabecera
// del requerimiento, la transferencia, las notas de salida e ingreso y sus detalles, además del stock.
// Fuera de la transacción comprueba que los conteos de todas las tablas quedan intactos.
// Colección compartida con las pruebas que bloquean filas de req_venta: en paralelo provocaban
// deadlocks por orden de bloqueo distinto entre pruebas.
[Collection("BdReqVentaFilasCompartidas")]
public class ReqVentaAprobacionIntegracionTests
{
    private const int AlmacenDespacho = 3;
    private const int AlmacenSolicitante = 4;
    private const int CodUser = 99;
    private const int CodAutorizador = 5;
    private const decimal Tolerancia = 0.0001m;

    private static readonly string[] Tablas =
    {
        "req_almacen", "detalle_req_almacen", "transferencia", "detalletransferencia",
        "notasalida", "detallenotasalida", "notaingreso", "detallenotaingreso",
    };

    private sealed class ReversionEsperada : Exception
    {
    }

    // Una línea del requerimiento de prueba y lo que se espera de ella tras aprobar.
    private sealed class Linea
    {
        public int CodProducto;
        public decimal Cantidad;
        public int CodDetalle;
        public double Precio;
        public double Subtotal;
        public double ValorVenta;
        public double Igv;
        public double PrecioReal;
        public double ValoReal;
        public decimal StockActualAntes;
        public decimal StockDisponibleAntes;
        public decimal StockActualSolicitanteAntes;
    }

    [HechoConBd]
    public void AprobarEn_ContraBdReal_GuardaColumnasYRevierte()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        string previa = Db.CadenaConexion;
        Db.CadenaConexion = cadena;
        try
        {
            var lectura = new ConsultorMySql(cadena);
            Dictionary<string, object> base5273 = lectura.Consultar(
                "SELECT cod_tipo_documento, cod_serie, num_serie, cod_almacen_registro, cod_user_registro " +
                "FROM req_almacen WHERE id_req_almacen = 5273").First();
            Dictionary<string, object> serie = lectura.Consultar(
                "CALL BuscaSeriexDocumento(@doc, @alm)", new { doc = 14, alm = AlmacenDespacho }).First();
            if (base5273 == null || serie == null)
            {
                return;
            }

            var antes = new Dictionary<string, long>();
            foreach (string tabla in Tablas)
            {
                antes[tabla] = Contar(lectura, tabla);
            }

            string numDoc = "ZZT" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpperInvariant();
            DateTime ahora = DateTime.Now;
            DateTime fechaIngreso = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, ahora.Second);
            var progreso = new ProgresoRegistrado();
            ResultadoOperacion resultado = null;

            try
            {
                Db.Transaccion(tx =>
                {
                    List<Linea> lineas = CrearRequerimientoPendiente(tx, base5273, numDoc, out int codReq);
                    var datos = new DatosAprobacionRequerimiento(
                        codReq, CodUser, CodAutorizador, "Entregar hoy", "77", 18.0, fechaIngreso);

                    PrepararEsperados(tx, lineas);
                    resultado = ReqVentaAprobacion.AprobarEn(tx, accion => accion(tx), datos, progreso, null);

                    Assert.True(resultado.Ok, resultado.Mensaje);
                    VerificarRequerimiento(tx, codReq, lineas);
                    int codTrans = VerificarTransferencia(tx, codReq, serie, lineas);
                    VerificarNotaDeSalida(tx, codTrans, serie, lineas);
                    VerificarNotaDeIngreso(tx, codTrans, serie, lineas, fechaIngreso, ahora.Date);
                    VerificarStock(tx, lineas);
                    throw new ReversionEsperada();
                });
            }
            catch (ReversionEsperada)
            {
            }

            Assert.NotNull(resultado);
            Assert.Equal(8, ReqVentaTextos.PasosAprobacion().Count);
            foreach (PasoOperacion paso in ReqVentaTextos.PasosAprobacion())
            {
                Assert.Equal(EstadoPaso.Listo, progreso.Ultimo(paso.Clave).Estado);
            }

            var despues = new ConsultorMySql(cadena);
            foreach (string tabla in Tablas)
            {
                Assert.Equal(antes[tabla], Contar(despues, tabla));
            }

            Assert.Equal(0, despues.Consultar(
                "SELECT COUNT(*) AS total FROM req_almacen WHERE num_documento = @num",
                new { num = numDoc }).First().Valor<long>("total"));
        }
        finally
        {
            Db.CadenaConexion = previa;
        }
    }

    private sealed class ProgresoRegistrado : IProgress<PasoOperacion>
    {
        private readonly List<PasoOperacion> _pasos = new List<PasoOperacion>();

        public void Report(PasoOperacion paso)
        {
            _pasos.Add(paso);
        }

        public PasoOperacion Ultimo(string clave)
        {
            return _pasos.FindLast(paso => paso.Clave == clave);
        }
    }

    private static long Contar(IConsultor consultor, string tabla)
    {
        return consultor.Consultar("SELECT COUNT(*) AS total FROM " + tabla).First().Valor<long>("total");
    }

    // Requerimiento de venta pendiente (estado 7, tipo 2) con dos líneas del almacén de despacho,
    // creado con el servicio de guardado de T6 dentro de la misma transacción.
    private static List<Linea> CrearRequerimientoPendiente(
        IConsultor tx, Dictionary<string, object> baseReq, string numDoc, out int codReq)
    {
        var cabecera = new DatosGuardadoRequerimiento(
            baseReq.Valor<int>("cod_tipo_documento"), numDoc,
            baseReq.Valor<int>("cod_serie"), baseReq.Valor<string>("num_serie"),
            baseReq.Valor<int>("cod_almacen_registro"), baseReq.Valor<int>("cod_user_registro"), DateTime.Now,
            AlmacenSolicitante, AlmacenDespacho, DateTime.Now,
            ReqVentaReglas.Pendiente, "Para obra", null, ReqVentaReglas.TipoReqVenta,
            0, "77", "Juan", "987654321", 0, "", "Despachador Uno");
        var lineas = new List<Linea>
        {
            new Linea { CodProducto = 5072, Cantidad = 2m },
            new Linea { CodProducto = 5877, Cantidad = 1m },
        };
        var detalles = new List<DatosGuardadoDetalle>();
        foreach (Linea linea in lineas)
        {
            detalles.Add(new DatosGuardadoDetalle(0, linea.CodProducto, 55, linea.Cantidad, linea.Cantidad, 0m, linea.Cantidad, 0m));
        }

        ResultadoOperacion guardado = ReqVentaGuardado.GuardarEn(tx, cabecera, detalles);
        Assert.True(guardado.Ok, guardado.Mensaje);
        codReq = cabecera.Codigo;

        List<Dictionary<string, object>> guardadas = tx.Consultar(
            "SELECT id_det_req_almacen FROM detalle_req_almacen WHERE id_req_almacen = @id ORDER BY cod_producto",
            new { id = codReq }).Get();
        Assert.Equal(lineas.Count, guardadas.Count);
        for (int i = 0; i < lineas.Count; i++)
        {
            lineas[i].CodDetalle = guardadas[i].Valor<int>("id_det_req_almacen");
        }

        return lineas;
    }

    // Lee el último precio de compra y el stock inicial, y calcula los montos esperados con las
    // fórmulas del legacy (IGV 18 %, precio real y valor real por unidad).
    private static void PrepararEsperados(IConsultor tx, List<Linea> lineas)
    {
        double factor = 18.0 / 100.0 + 1.0;
        foreach (Linea linea in lineas)
        {
            linea.Precio = Convert.ToDouble(tx.Consultar(
                "CALL MuestraUltimoPrecioCompraPorProductoyUnidad(@prod, @und, 0)",
                new { prod = linea.CodProducto, und = 55 }).First().Valor<decimal>("ultimo_precio_compra"));
            double cantidad = Convert.ToDouble(linea.Cantidad);
            linea.Subtotal = linea.Precio * cantidad;
            linea.ValorVenta = linea.Subtotal / factor;
            linea.Igv = linea.Subtotal - linea.ValorVenta;
            linea.PrecioReal = linea.Subtotal / cantidad;
            linea.ValoReal = linea.ValorVenta / cantidad;

            Dictionary<string, object> despacho = StockDe(tx, linea.CodProducto, AlmacenDespacho);
            linea.StockActualAntes = despacho.Valor<decimal>("stockactual");
            linea.StockDisponibleAntes = despacho.Valor<decimal>("stockdisponible");
            linea.StockActualSolicitanteAntes = StockDe(tx, linea.CodProducto, AlmacenSolicitante).Valor<decimal>("stockactual");
        }
    }

    private static Dictionary<string, object> StockDe(IConsultor tx, int codProducto, int codAlmacen)
    {
        return tx.Consultar(
            "SELECT stockactual, stockdisponible FROM productoalmacen WHERE codProducto = @prod AND codAlmacen = @alm",
            new { prod = codProducto, alm = codAlmacen }).First();
    }

    private static void VerificarRequerimiento(IConsultor tx, int codReq, List<Linea> lineas)
    {
        Dictionary<string, object> req = tx.Consultar(
            "SELECT estado, tipo_req, idAutorizador, cod_user_aprobo, comentario_despacho, " +
            "cod_almacen_solicitante, cod_almacen_despacho FROM req_almacen WHERE id_req_almacen = @id",
            new { id = codReq }).First();
        Assert.NotNull(req);
        Assert.Equal(ReqVentaReglas.AprobadoTransferido, req.Valor<int>("estado"));
        Assert.Equal(2, req.Valor<int>("tipo_req"));
        Assert.Equal(CodAutorizador, req.Valor<int>("idAutorizador"));
        Assert.Equal(CodUser, req.Valor<int>("cod_user_aprobo"));
        Assert.Equal("Entregar hoy", req.Valor<string>("comentario_despacho"));
        Assert.Equal(AlmacenSolicitante, req.Valor<int>("cod_almacen_solicitante"));
        Assert.Equal(AlmacenDespacho, req.Valor<int>("cod_almacen_despacho"));

        List<Dictionary<string, object>> filas = tx.Consultar(
            "SELECT id_det_req_almacen, id_req_almacen, cod_producto, cod_unidad, cantidad, cantidad_pedida, " +
            "cantidad_confirmada, cantidad_pendiente, cantidad_pendiente_aprobada " +
            "FROM detalle_req_almacen WHERE id_req_almacen = @id ORDER BY cod_producto",
            new { id = codReq }).Get();
        Assert.Equal(lineas.Count, filas.Count);
        for (int i = 0; i < lineas.Count; i++)
        {
            Assert.Equal(lineas[i].CodDetalle, filas[i].Valor<int>("id_det_req_almacen"));
            Assert.Equal(codReq, filas[i].Valor<int>("id_req_almacen"));
            Assert.Equal(lineas[i].CodProducto, filas[i].Valor<int>("cod_producto"));
            Assert.Equal(55, filas[i].Valor<int>("cod_unidad"));
            Assert.Equal(lineas[i].Cantidad, filas[i].Valor<decimal>("cantidad"));
            Assert.Equal(lineas[i].Cantidad, filas[i].Valor<decimal>("cantidad_pedida"));
            Assert.Equal(lineas[i].Cantidad, filas[i].Valor<decimal>("cantidad_confirmada"));
            Assert.Equal(0m, filas[i].Valor<decimal>("cantidad_pendiente"));
            Assert.Equal(lineas[i].Cantidad, filas[i].Valor<decimal>("cantidad_pendiente_aprobada"));
        }
    }

    private static int VerificarTransferencia(IConsultor tx, int codReq, Dictionary<string, object> serie, List<Linea> lineas)
    {
        List<Dictionary<string, object>> cabeceras = tx.Consultar(
            "SELECT codTransDir, codAlmacenOrigen, codAlmacenDestino, codTipoDocumento, moneda, comentario, " +
            "bruto, montodscto, igv, total, estado+0 AS estado, pendiente+0 AS pendiente, codUsuario, codserie, " +
            "serie, numerodoc, EstadoTrnas FROM transferencia " +
            "WHERE id_req_almacen = @id AND codDocExtornacion IS NULL",
            new { id = codReq }).Get();
        Assert.Single(cabeceras);
        Dictionary<string, object> t = cabeceras[0];
        int codTrans = t.Valor<int>("codTransDir");

        Assert.Equal(AlmacenDespacho, t.Valor<int>("codAlmacenOrigen"));
        Assert.Equal(AlmacenSolicitante, t.Valor<int>("codAlmacenDestino"));
        Assert.Equal(14, t.Valor<int>("codTipoDocumento"));
        Assert.Equal(1, t.Valor<int>("moneda"));
        Assert.Equal("Req Almacen para Ventas generado de O.V: 77", t.Valor<string>("comentario"));
        Igual(SumaDecimal(lineas, l => l.Subtotal), t.Valor<decimal>("bruto"));
        Igual(0m, t.Valor<decimal>("montodscto"));
        Igual(SumaDecimal(lineas, l => l.Igv), t.Valor<decimal>("igv"));
        Igual(SumaDecimal(lineas, l => l.Subtotal), t.Valor<decimal>("total"));
        Assert.Equal(1, t.Valor<int>("estado"));
        Assert.Equal(0, t.Valor<int>("pendiente"));
        Assert.Equal(CodUser, t.Valor<int>("codUsuario"));
        Assert.Equal(Convert.ToInt32(serie["codSerie"]), t.Valor<int>("codserie"));
        Assert.Equal("001", t.Valor<string>("serie"));
        Assert.Equal(NumeroDeTransferencia(serie), t.Valor<string>("numerodoc"));
        Assert.Equal(1, t.Valor<int>("EstadoTrnas"));

        List<Dictionary<string, object>> filas = tx.Consultar(
            "SELECT codProducto, unidadingresada, codAlmacenOrigen, codAlmacenDestino, serielote, cantidad, " +
            "preciounitario, subtotal, descuento1, descuento2, descuento3, montodscto, igv, importe, precioreal, " +
            "valoreal, codUser, codProv, PrecioIgv+0 AS PrecioIgv, valorpromedio, id_det_req_almacen " +
            "FROM detalletransferencia WHERE codTransDir = @id ORDER BY codProducto",
            new { id = codTrans }).Get();
        Assert.Equal(lineas.Count, filas.Count);
        for (int i = 0; i < lineas.Count; i++)
        {
            Linea l = lineas[i];
            Dictionary<string, object> f = filas[i];
            Assert.Equal(l.CodProducto, f.Valor<int>("codProducto"));
            Assert.Equal(55, f.Valor<int>("unidadingresada"));
            Assert.Equal(AlmacenDespacho, f.Valor<int>("codAlmacenOrigen"));
            Assert.Equal(AlmacenSolicitante, f.Valor<int>("codAlmacenDestino"));
            Assert.Equal(string.Empty, f.Valor<string>("serielote"));
            Igual(l.Cantidad, f.Valor<decimal>("cantidad"));
            Igual(Convert.ToDecimal(l.Precio), f.Valor<decimal>("preciounitario"));
            Igual(Convert.ToDecimal(l.Subtotal), f.Valor<decimal>("subtotal"));
            Igual(0m, f.Valor<decimal>("descuento1"));
            Igual(0m, f.Valor<decimal>("descuento2"));
            Igual(0m, f.Valor<decimal>("descuento3"));
            Igual(0m, f.Valor<decimal>("montodscto"));
            Igual(Convert.ToDecimal(l.Igv), f.Valor<decimal>("igv"));
            Igual(Convert.ToDecimal(l.Subtotal), f.Valor<decimal>("importe"));
            Igual(Convert.ToDecimal(l.PrecioReal), f.Valor<decimal>("precioreal"));
            Igual(Convert.ToDecimal(l.ValoReal), f.Valor<decimal>("valoreal"));
            Assert.Equal(CodUser, f.Valor<int>("codUser"));
            Assert.Equal(0, f.Valor<int>("codProv"));
            Assert.Equal(0, f.Valor<int>("PrecioIgv"));
            Igual(Convert.ToDecimal(l.Precio), f.Valor<decimal>("valorpromedio"));
            Assert.Equal(l.CodDetalle, f.Valor<int>("id_det_req_almacen"));
        }

        return codTrans;
    }

    private static void VerificarNotaDeSalida(IConsultor tx, int codTrans, Dictionary<string, object> serie, List<Linea> lineas)
    {
        List<Dictionary<string, object>> cabeceras = tx.Consultar(
            "SELECT codNotaSalida, codSucursal, codAlmacen, codTransaccion, codTipoDocumento, codSerie, serie, " +
            "numdocumento, tipocliente, codCliente, moneda, comentario, bruto, montodscto, igv, total, pendiente, " +
            "estado+0 AS estado, formapago, codUsuario, documentoreferencia, codVendedor " +
            "FROM notasalida WHERE codTransferecia = @id",
            new { id = codTrans }).Get();
        Assert.Single(cabeceras);
        Dictionary<string, object> n = cabeceras[0];

        Assert.Equal(AlmacenDespacho, n.Valor<int>("codSucursal"));
        Assert.Equal(AlmacenDespacho, n.Valor<int>("codAlmacen"));
        Assert.Equal(15, n.Valor<int>("codTransaccion"));
        Assert.Equal(14, n.Valor<int>("codTipoDocumento"));
        Assert.Equal(Convert.ToInt32(serie["codSerie"]), n.Valor<int>("codSerie"));
        Assert.Equal("001", n.Valor<string>("serie"));
        Assert.Equal(NumeroDeTransferencia(serie), n.Valor<string>("numdocumento"));
        Assert.Equal(0, n.Valor<int>("tipocliente"));
        Assert.Null(n["codCliente"]);
        Assert.Equal(1, n.Valor<int>("moneda"));
        Assert.Equal(string.Empty, n.Valor<string>("comentario"));
        Igual(SumaDecimal(lineas, l => l.Subtotal), n.Valor<decimal>("bruto"));
        Igual(0m, n.Valor<decimal>("montodscto"));
        Igual(0m, n.Valor<decimal>("igv"));
        Igual(SumaDecimal(lineas, l => l.Subtotal), n.Valor<decimal>("total"));
        Igual(SumaDecimal(lineas, l => l.Subtotal), n.Valor<decimal>("pendiente"));
        Assert.Equal(1, n.Valor<int>("estado"));
        Assert.Null(n["formapago"]);
        Assert.Equal(CodUser, n.Valor<int>("codUsuario"));
        Assert.Equal(0, n.Valor<int>("documentoreferencia"));
        Assert.Equal(0, n.Valor<int>("codVendedor"));

        List<Dictionary<string, object>> filas = tx.Consultar(
            "SELECT codProducto, codAlmacen, unidadingresada, serielote, cantidad, preciounitario, subtotal, " +
            "descuento1, descuento2, descuento3, montodscto, igv, importe, precioreal, valoreal, codUser, " +
            "codVenta, codCotizacion, codListaPrecio, valorrealsoles, cantidadpendiente " +
            "FROM detallenotasalida WHERE codNotaSalida = @id ORDER BY codProducto",
            new { id = n.Valor<int>("codNotaSalida") }).Get();
        Assert.Equal(lineas.Count, filas.Count);
        for (int i = 0; i < lineas.Count; i++)
        {
            Linea l = lineas[i];
            Dictionary<string, object> f = filas[i];
            Assert.Equal(l.CodProducto, f.Valor<int>("codProducto"));
            Assert.Equal(AlmacenDespacho, f.Valor<int>("codAlmacen"));
            Assert.Equal(55, f.Valor<int>("unidadingresada"));
            Assert.Equal("0", f.Valor<string>("serielote"));
            Igual(l.Cantidad, f.Valor<decimal>("cantidad"));
            Igual(Convert.ToDecimal(l.Precio), f.Valor<decimal>("preciounitario"));
            Igual(Convert.ToDecimal(l.Subtotal), f.Valor<decimal>("subtotal"));
            Igual(0m, f.Valor<decimal>("descuento1"));
            Igual(0m, f.Valor<decimal>("descuento2"));
            Igual(0m, f.Valor<decimal>("descuento3"));
            Igual(0m, f.Valor<decimal>("montodscto"));
            Igual(Convert.ToDecimal(l.Igv), f.Valor<decimal>("igv"));
            Igual(Convert.ToDecimal(l.Subtotal), f.Valor<decimal>("importe"));
            Igual(Convert.ToDecimal(l.PrecioReal), f.Valor<decimal>("precioreal"));
            Igual(Convert.ToDecimal(l.ValoReal), f.Valor<decimal>("valoreal"));
            Assert.Equal(CodUser, f.Valor<int>("codUser"));
            Assert.Equal(0, f.Valor<int>("codVenta"));
            Assert.Equal(0, f.Valor<int>("codCotizacion"));
            Assert.Equal(0, f.Valor<int>("codListaPrecio"));
            Igual(0m, f.Valor<decimal>("valorrealsoles"));
            Igual(0m, f.Valor<decimal>("cantidadpendiente"));
        }
    }

    private static void VerificarNotaDeIngreso(
        IConsultor tx, int codTrans, Dictionary<string, object> serie, List<Linea> lineas,
        DateTime fechaIngreso, DateTime hoy)
    {
        List<Dictionary<string, object>> cabeceras = tx.Consultar(
            "SELECT codNotaIngreso, codAlmacen, codTransaccion, codTipoDocumento, numdocumento, codProveedor, " +
            "moneda, comentario, bruto, montodscto, igv, flete, total, pendiente, estado+0 AS estado, " +
            "recibido+0 AS recibido, formapago, cancelado+0 AS cancelado, codUsuario, codSerie, serie, " +
            "codReferencia, CodOrdenCompra, codalmacenemisor, codguiaremision, Responsable, Area, fechaingreso " +
            "FROM notaingreso WHERE codTransferencia = @id",
            new { id = codTrans }).Get();
        Assert.Single(cabeceras);
        Dictionary<string, object> n = cabeceras[0];

        Assert.Equal(AlmacenSolicitante, n.Valor<int>("codAlmacen"));
        Assert.Equal(15, n.Valor<int>("codTransaccion"));
        Assert.Equal(14, n.Valor<int>("codTipoDocumento"));
        Assert.Equal(NumeroDeTransferencia(serie), n.Valor<string>("numdocumento"));
        Assert.Null(n["codProveedor"]);
        Assert.Equal(1, n.Valor<int>("moneda"));
        Assert.Equal(string.Empty, n.Valor<string>("comentario"));

        // El legacy no asigna el monto bruto de la nota de ingreso (lo asigna por error a la de salida):
        // queda en cero. Corregirlo cambia montos contables y se decide aparte.
        Igual(0m, n.Valor<decimal>("bruto"));
        Igual(0m, n.Valor<decimal>("montodscto"));
        Igual(0m, n.Valor<decimal>("igv"));
        Igual(0m, n.Valor<decimal>("flete"));
        Igual(SumaDecimal(lineas, l => l.Subtotal), n.Valor<decimal>("total"));
        Igual(SumaDecimal(lineas, l => l.Subtotal), n.Valor<decimal>("pendiente"));
        Assert.Equal(1, n.Valor<int>("estado"));
        Assert.Equal(0, n.Valor<int>("recibido"));
        Assert.Null(n["formapago"]);
        Assert.Equal(0, n.Valor<int>("cancelado"));
        Assert.Equal(CodUser, n.Valor<int>("codUsuario"));
        Assert.Equal(Convert.ToInt32(serie["codSerie"]), n.Valor<int>("codSerie"));
        Assert.Equal("001", n.Valor<string>("serie"));
        Assert.Equal(0, n.Valor<int>("codReferencia"));
        Assert.Equal(0, n.Valor<int>("CodOrdenCompra"));
        Assert.Null(n["codalmacenemisor"]);
        Assert.Equal(0, n.Valor<int>("codguiaremision"));
        Assert.Equal(0, n.Valor<int>("Responsable"));
        Assert.Null(n["Area"]);
        Assert.Equal(hoy, n.Valor<DateTime>("fechaingreso"));

        List<Dictionary<string, object>> filas = tx.Consultar(
            "SELECT codProducto, codAlmacen, moneda, unidadingresada, serielote, cantidad, preciounitario, " +
            "subtotal, descuento1, descuento2, descuento3, montodscto, igv, flete, importe, precioreal, valoreal, " +
            "fechaingreso, codUser, valorrealsoles, coddetallerequerimiento, bonificacion+0 AS bonificacion, " +
            "codguiaremision FROM detallenotaingreso WHERE codNotaIngreso = @id ORDER BY codProducto",
            new { id = n.Valor<int>("codNotaIngreso") }).Get();
        Assert.Equal(lineas.Count, filas.Count);
        for (int i = 0; i < lineas.Count; i++)
        {
            Linea l = lineas[i];
            Dictionary<string, object> f = filas[i];
            double igvIngreso = (l.Precio / 1.18) * 0.18;
            Assert.Equal(l.CodProducto, f.Valor<int>("codProducto"));
            Assert.Equal(AlmacenSolicitante, f.Valor<int>("codAlmacen"));
            Assert.Equal(0, f.Valor<int>("moneda"));
            Assert.Equal(55, f.Valor<int>("unidadingresada"));
            Assert.Equal("0", f.Valor<string>("serielote"));
            Igual(l.Cantidad, f.Valor<decimal>("cantidad"));
            Igual(Convert.ToDecimal(l.Precio), f.Valor<decimal>("preciounitario"));
            Igual(Convert.ToDecimal(l.Subtotal), f.Valor<decimal>("subtotal"));
            Igual(0m, f.Valor<decimal>("descuento1"));
            Igual(0m, f.Valor<decimal>("descuento2"));
            Igual(0m, f.Valor<decimal>("descuento3"));
            Igual(0m, f.Valor<decimal>("montodscto"));
            Igual(Convert.ToDecimal(igvIngreso), f.Valor<decimal>("igv"));
            Igual(0m, f.Valor<decimal>("flete"));
            Igual(Convert.ToDecimal(l.Subtotal), f.Valor<decimal>("importe"));
            Igual(Convert.ToDecimal(l.Precio), f.Valor<decimal>("precioreal"));
            Igual(Convert.ToDecimal(l.ValoReal), f.Valor<decimal>("valoreal"));
            Assert.Equal(fechaIngreso, f.Valor<DateTime>("fechaingreso"));
            Assert.Equal(CodUser, f.Valor<int>("codUser"));
            Igual(0m, f.Valor<decimal>("valorrealsoles"));
            Assert.Equal(0, f.Valor<int>("coddetallerequerimiento"));
            Assert.Equal(0, f.Valor<int>("bonificacion"));
            Assert.Equal(0, f.Valor<int>("codguiaremision"));
        }
    }

    // Despacho: baja el stock actual y el disponible; solicitante: sube el stock actual.
    private static void VerificarStock(IConsultor tx, List<Linea> lineas)
    {
        foreach (Linea l in lineas)
        {
            Dictionary<string, object> despacho = StockDe(tx, l.CodProducto, AlmacenDespacho);
            Igual(l.StockActualAntes - l.Cantidad, despacho.Valor<decimal>("stockactual"));
            Igual(l.StockDisponibleAntes - l.Cantidad, despacho.Valor<decimal>("stockdisponible"));

            Dictionary<string, object> solicitante = StockDe(tx, l.CodProducto, AlmacenSolicitante);
            Igual(l.StockActualSolicitanteAntes + l.Cantidad, solicitante.Valor<decimal>("stockactual"));
        }
    }

    private static string NumeroDeTransferencia(Dictionary<string, object> serie)
    {
        return Convert.ToInt32(serie["numeracion"]).ToString().PadLeft(6, '0');
    }

    private static decimal SumaDecimal(List<Linea> lineas, Func<Linea, double> selector)
    {
        decimal suma = 0m;
        foreach (Linea linea in lineas)
        {
            suma += Convert.ToDecimal(selector(linea));
        }

        return suma;
    }

    // Las columnas guardan 4 decimales: se compara con una tolerancia de una diezmilésima.
    private static void Igual(decimal esperado, decimal real)
    {
        Assert.InRange(real, esperado - Tolerancia, esperado + Tolerancia);
    }
}
