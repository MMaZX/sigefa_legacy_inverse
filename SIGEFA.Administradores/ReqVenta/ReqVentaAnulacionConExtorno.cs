using System;
using System.Collections.Generic;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.ReqVenta;

// Anulación de un requerimiento de venta aprobado (estado 13) generando el extorno (T2d).
// Trabaja sobre UNA conexión y UNA transacción recibida por parámetro.
// Si cualquier paso falla, no hace commit ni rollback internos: devuelve un ResultadoAnulacion con error legible
// para que el llamador ejecute el rollback total de la transacción.
public static class ReqVentaAnulacionConExtorno
{
    private static class Constantes
    {
        public const int TipoDocExtornacion = 50; // 'DET'
        public const int TransaccionTransferencia = 15;
    }

    public static ResultadoAnulacion AnularConExtorno(IConsultor consultor, int codReq, int codUser)
    {
        if (consultor == null)
        {
            return Fallo("El consultor es obligatorio.");
        }

        if (codReq <= 0)
        {
            return Fallo("El requerimiento debe ser mayor que cero.");
        }

        if (codUser <= 0)
        {
            return Fallo("El usuario que anula debe ser mayor que cero.");
        }

        try
        {
            return Ejecutar(consultor, codReq, codUser);
        }
        catch (Exception ex)
        {
            return Fallo("No se pudo anular con extorno el requerimiento " + codReq + ": " + ex.Message);
        }
    }

    private static ResultadoAnulacion Ejecutar(IConsultor consultor, int codReq, int codUser)
    {
        // 1. Bloqueo de req_almacen con FOR UPDATE
        Dictionary<string, object> requerimiento = ReqVentaConsultas.ObtenerRequerimiento(consultor, codReq, true);
        if (requerimiento == null)
        {
            return Fallo("El requerimiento " + codReq + " no existe.");
        }

        int tipoReq = requerimiento.Valor<int>("tipo_req");
        int estado = requerimiento.Valor<int>("estado");
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(tipoReq, estado);
        switch (decision.Accion)
        {
            case AccionAnulacion.AnularConExtorno:
                break;
            default:
                return Fallo(decision.Motivo);
        }

        // 2. Tomar la transferencia original con ObtenerTransferencias bloqueando con FOR UPDATE
        List<Dictionary<string, object>> transferencias = ReqVentaConsultas.ObtenerTransferencias(consultor, codReq, true);
        List<Dictionary<string, object>> vigentesSinExtorno = new List<Dictionary<string, object>>();
        foreach (Dictionary<string, object> t in transferencias)
        {
            bool tieneExtorno = t.Valor<bool>("tiene_extorno");
            if (!tieneExtorno)
            {
                vigentesSinExtorno.Add(t);
            }
        }

        if (vigentesSinExtorno.Count == 0)
        {
            return Fallo("El requerimiento " + codReq + " no tiene ninguna transferencia original sin extorno.");
        }

        if (vigentesSinExtorno.Count > 1)
        {
            return Fallo("El requerimiento " + codReq + " tiene más de una transferencia original sin extorno.");
        }

        int codTransOriginal = vigentesSinExtorno[0].Valor<int>("codTransDir");

        // Leer cabecera completa de la transferencia original
        Dictionary<string, object> originalHeader = consultor.Consultar(
            "SELECT codTransDir, codAlmacenOrigen, codAlmacenDestino, codserie, serie, numerodoc, bruto, montodscto, igv, total, moneda, tipocambio " +
            "FROM transferencia WHERE codTransDir = @id FOR UPDATE",
            new { id = codTransOriginal }).First();
        if (originalHeader == null)
        {
            return Fallo("No se encontró la cabecera de la transferencia original " + codTransOriginal + ".");
        }

        // Leer detalles de la transferencia original
        List<Dictionary<string, object>> lineasOriginal = consultor.Consultar(
            "SELECT codDetalleTransDir, codProducto, codAlmacenOrigen, unidadingresada, codAlmacenDestino, serielote, cantidad, preciounitario, " +
            "subtotal, descuento1, descuento2, descuento3, montodscto, igv, importe, precioreal, valoreal, cantidadpendiente, codProv, PrecioIgv, " +
            "valorpromedio, id_det_req_almacen " +
            "FROM detalletransferencia WHERE codTransDir = @id ORDER BY codDetalleTransDir FOR UPDATE",
            new { id = codTransOriginal }).Get();
        if (lineasOriginal.Count == 0)
        {
            return Fallo("La transferencia original " + codTransOriginal + " no tiene líneas de detalle.");
        }

        int almacenSolicitante = requerimiento.Valor<int>("cod_almacen_solicitante");
        int almacenDespacho = requerimiento.Valor<int>("cod_almacen_despacho");

        // 3. Bloqueo de productoalmacen y validación de stock con el factor de unidad ANTES de GuardaDetalleSalida
        ResultadoAnulacion validacionStock = ValidarYBloquearStock(consultor, lineasOriginal, almacenSolicitante, almacenDespacho);
        if (!validacionStock.Ok)
        {
            return validacionStock;
        }

        // 4. GuardaTransferencia para el extorno (almacenes invertidos: origen=solicitante, destino=despacho)
        int idExtorno = InsertarCabeceraExtorno(consultor, codReq, codTransOriginal, originalHeader, almacenSolicitante, almacenDespacho, codUser);
        if (idExtorno <= 0)
        {
            return Fallo("Fallo al registrar la cabecera de transferencia de extorno (newid = 0).");
        }

        // 5. GuardaDetalleTransferencia para cada línea
        ResultadoAnulacion resDetExt = InsertarDetallesExtorno(consultor, idExtorno, lineasOriginal, almacenSolicitante, almacenDespacho, codUser);
        if (!resDetExt.Ok)
        {
            return resDetExt;
        }

        // 6. GuardaNotaSalida y GuardaDetalleSalida
        int idNotaSalida = InsertarCabeceraNotaSalida(consultor, idExtorno, originalHeader, almacenSolicitante, codUser);
        if (idNotaSalida <= 0)
        {
            return Fallo("Fallo al registrar la cabecera de nota de salida para el extorno (newid = 0).");
        }

        ResultadoAnulacion resDetSalida = InsertarDetallesNotaSalida(consultor, idNotaSalida, lineasOriginal, almacenSolicitante, codUser);
        if (!resDetSalida.Ok)
        {
            return resDetSalida;
        }

        // 7. GuardaNotaIngreso y GuardaDetalleIngreso
        int idNotaIngreso = InsertarCabeceraNotaIngreso(consultor, idExtorno, originalHeader, almacenDespacho, codUser);
        if (idNotaIngreso <= 0)
        {
            return Fallo("Fallo al registrar la cabecera de nota de ingreso para el extorno (newid = 0).");
        }

        ResultadoAnulacion resDetIngreso = InsertarDetallesNotaIngreso(consultor, idNotaIngreso, lineasOriginal, almacenDespacho, codUser);
        if (!resDetIngreso.Ok)
        {
            return resDetIngreso;
        }

        // 8. AprobarTransferencia del extorno
        ResultadoEjecucion ejecAprobar = consultor.Ejecutar("CALL AprobarTransferencia(@id)", new { id = idExtorno });

        // 9. Marcar el requerimiento como anulado (12)
        ResultadoEjecucion marcado = consultor.Ejecutar(
            "UPDATE req_almacen SET estado = 12, fecha_anulo = NOW(), cod_user_anulo = @user WHERE id_req_almacen = @id",
            new { user = codUser, id = codReq });
        if (marcado.FilasAfectadas != 1)
        {
            return Fallo("No se pudo marcar como anulado el requerimiento " + codReq + ".");
        }

        return new ResultadoAnulacion(true, "Requerimiento " + codReq + " anulado con extorno " + idExtorno + ".");
    }

    private static ResultadoAnulacion ValidarYBloquearStock(
        IConsultor consultor,
        List<Dictionary<string, object>> lineas,
        int almacenSolicitante,
        int almacenDespacho)
    {
        // Ordenamos los productos por (almacen, producto) para evitar interbloqueos
        SortedSet<int> productos = new SortedSet<int>();
        foreach (Dictionary<string, object> linea in lineas)
        {
            productos.Add(linea.Valor<int>("codProducto"));
        }

        // Bloqueamos primero el almacén solicitante (que es de donde saldrá el extorno)
        foreach (int prod in productos)
        {
            consultor.Consultar(
                "SELECT stockactual, stockdisponible, Unidad FROM productoalmacen WHERE codProducto = @prod AND codAlmacen = @alm FOR UPDATE",
                new { prod = prod, alm = almacenSolicitante }).First();
        }

        // Luego bloqueamos el almacén destino (despacho)
        foreach (int prod in productos)
        {
            consultor.Consultar(
                "SELECT stockactual, stockdisponible, Unidad FROM productoalmacen WHERE codProducto = @prod AND codAlmacen = @alm FOR UPDATE",
                new { prod = prod, alm = almacenDespacho }).First();
        }

        // Validamos que en el almacén solicitante exista stock suficiente considerando el factor de unidad
        foreach (Dictionary<string, object> linea in lineas)
        {
            int prod = linea.Valor<int>("codProducto");
            int unidadIngresada = linea.Valor<int>("unidadingresada");
            decimal cantidad = linea.Valor<decimal>("cantidad");

            Dictionary<string, object> pa = consultor.Consultar(
                "SELECT stockactual, stockdisponible, Unidad FROM productoalmacen WHERE codProducto = @prod AND codAlmacen = @alm",
                new { prod = prod, alm = almacenSolicitante }).First();
            if (pa == null)
            {
                return Fallo("No existe registro de producto " + prod + " en el almacén solicitante " + almacenSolicitante + ".");
            }

            object stockObj = pa["stockactual"];
            if (stockObj == null || stockObj == DBNull.Value)
            {
                return Fallo("El stock del producto " + prod + " en el almacén solicitante " + almacenSolicitante + " es NULL.");
            }

            decimal stockActual = Convert.ToDecimal(stockObj);
            int unidadBase = pa.Valor<int>("Unidad");

            decimal factor = 1m;
            if (unidadIngresada != unidadBase)
            {
                Dictionary<string, object> fRow = consultor.Consultar(
                    "SELECT factor FROM unidadequivalente WHERE codProducto = @prod AND codUnidadMedida = @um AND codUndEqui = @base AND compra_venta = 2",
                    new { prod = prod, um = unidadIngresada, @base = unidadBase }).First();
                if (fRow == null || fRow["factor"] == null || fRow["factor"] == DBNull.Value)
                {
                    return Fallo("No existe factor de conversión para el producto " + prod + " de unidad " + unidadIngresada + " a unidad base " + unidadBase + ".");
                }
                factor = Convert.ToDecimal(fRow["factor"]);
            }

            decimal cantidadRequerida = cantidad * factor;
            if (stockActual < cantidadRequerida)
            {
                return Fallo("Stock insuficiente para el producto " + prod + " en almacén solicitante " + almacenSolicitante + ". Stock actual: " + stockActual + ", requerido: " + cantidadRequerida + ".");
            }
        }

        return new ResultadoAnulacion(true, string.Empty);
    }

    private static int InsertarCabeceraExtorno(
        IConsultor consultor,
        int codReq,
        int codTransOriginal,
        Dictionary<string, object> original,
        int almacenSolicitante,
        int almacenDespacho,
        int codUser)
    {
        string comentario = "Documento de Extornacion para Transf: " + codTransOriginal;
        if (comentario.Length > 110)
        {
            comentario = comentario.Substring(0, 110);
        }

        Dictionary<string, object> res = consultor.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaTransferencia(@codalmaorig, @codtipo, @codalmadest, @moneda, @tipocambio, NOW(), NOW(), '', @comentario, " +
            "@bruto, @montodscto, @igv, @total, 1, 0, NOW(), 0, @codusu, @codserie, @serie, @numdoc, @codreq, @codextornar, @newid); " +
            "SELECT @newid AS newid;",
            new
            {
                codalmaorig = almacenSolicitante,
                codtipo = Constantes.TipoDocExtornacion,
                codalmadest = almacenDespacho,
                moneda = original.Valor<int>("moneda"),
                tipocambio = original.Valor<double>("tipocambio"),
                comentario = comentario,
                bruto = original.Valor<double>("bruto"),
                montodscto = original.Valor<double>("montodscto"),
                igv = original.Valor<double>("igv"),
                total = original.Valor<double>("total"),
                codusu = codUser,
                codserie = original.Valor<int>("codserie"),
                serie = original.Valor<string>("serie"),
                numdoc = original.Valor<string>("numerodoc"),
                codreq = codReq,
                codextornar = codTransOriginal
            }).First();

        if (res == null || res["newid"] == null)
        {
            return 0;
        }

        return Convert.ToInt32(res["newid"]);
    }

    private static ResultadoAnulacion InsertarDetallesExtorno(
        IConsultor consultor,
        int idExtorno,
        List<Dictionary<string, object>> lineasOriginal,
        int almacenSolicitante,
        int almacenDespacho,
        int codUser)
    {
        foreach (Dictionary<string, object> linea in lineasOriginal)
        {
            int prod = linea.Valor<int>("codProducto");
            Dictionary<string, object> res = consultor.Consultar(
                "SET @newid = 0; " +
                "CALL GuardaDetalleTransferencia(@codpro, @codtrans, @codalmaorig, @unidad, @codalmadest, @serielote, @cantidad, " +
                "@precio, @subtotal, @dscto1, @dscto2, @dscto3, @montodscto, @igv, @importe, @precioreal, @valoreal, @codusu, " +
                "@cantp, @codprov, @precioigv, @promedio, 0, @newid); " +
                "SELECT @newid AS newid;",
                new
                {
                    codpro = prod,
                    codtrans = idExtorno,
                    codalmaorig = almacenSolicitante,
                    unidad = linea.Valor<int>("unidadingresada"),
                    codalmadest = almacenDespacho,
                    serielote = linea.Valor<string>("serielote") ?? string.Empty,
                    cantidad = linea.Valor<double>("cantidad"),
                    precio = linea.Valor<double>("preciounitario"),
                    subtotal = linea.Valor<double>("subtotal"),
                    dscto1 = linea.Valor<double>("descuento1"),
                    dscto2 = linea.Valor<double>("descuento2"),
                    dscto3 = linea.Valor<double>("descuento3"),
                    montodscto = linea.Valor<double>("montodscto"),
                    igv = linea.Valor<double>("igv"),
                    importe = linea.Valor<double>("importe"),
                    precioreal = linea.Valor<double>("precioreal"),
                    valoreal = linea.Valor<double>("valoreal"),
                    codusu = codUser,
                    cantp = linea.Valor<double>("cantidadpendiente"),
                    codprov = linea.Valor<int>("codProv"),
                    precioigv = linea.Valor<int>("PrecioIgv") == 1,
                    promedio = linea.Valor<decimal>("valorpromedio")
                }).First();

            if (res == null || res["newid"] == null || Convert.ToInt32(res["newid"]) == 0)
            {
                return Fallo("Fallo al guardar detalle de transferencia para producto " + prod + " (newid = 0).");
            }
        }

        return new ResultadoAnulacion(true, string.Empty);
    }

    private static int InsertarCabeceraNotaSalida(
        IConsultor consultor,
        int idExtorno,
        Dictionary<string, object> original,
        int almacenSolicitante,
        int codUser)
    {
        Dictionary<string, object> res = consultor.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaNotaSalida(@codSu, @codalma, @codtran, @codtipo, @codserie, @serie, @numdoc, 0, NULL, @moneda, @tipocambio, " +
            "NOW(), '', @bruto, @montodscto, @igv, @total, @total, 1, NULL, NOW(), 0, @codusu, NULL, NULL, NULL, 0, 0, NULL, '', NULL, " +
            "@codextorno, '', 0, @newid); " +
            "SELECT @newid AS newid;",
            new
            {
                codSu = almacenSolicitante,
                codalma = almacenSolicitante,
                codtran = Constantes.TransaccionTransferencia,
                codtipo = Constantes.TipoDocExtornacion,
                codserie = original.Valor<int>("codserie"),
                serie = original.Valor<string>("serie"),
                numdoc = original.Valor<string>("numerodoc"),
                moneda = original.Valor<int>("moneda"),
                tipocambio = original.Valor<double>("tipocambio"),
                bruto = original.Valor<double>("bruto"),
                montodscto = original.Valor<double>("montodscto"),
                igv = original.Valor<double>("igv"),
                total = original.Valor<double>("total"),
                codusu = codUser,
                codextorno = idExtorno
            }).First();

        if (res == null || res["newid"] == null)
        {
            return 0;
        }

        return Convert.ToInt32(res["newid"]);
    }

    private static ResultadoAnulacion InsertarDetallesNotaSalida(
        IConsultor consultor,
        int idNotaSalida,
        List<Dictionary<string, object>> lineasOriginal,
        int almacenSolicitante,
        int codUser)
    {
        foreach (Dictionary<string, object> linea in lineasOriginal)
        {
            int prod = linea.Valor<int>("codProducto");
            Dictionary<string, object> res = consultor.Consultar(
                "SET @newid = 0; " +
                "CALL GuardaDetalleSalida(@codpro, @codnota, @codalma, 0, 0, 0, @unidad, @serielote, @canti, @precio, @subtotal, " +
                "@dscto1, @dscto2, @dscto3, @montodscto, @igv, @importe, @precioreal, @valoreal, @codusu, 0.0, @cantp, @newid); " +
                "SELECT @newid AS newid;",
                new
                {
                    codpro = prod,
                    codnota = idNotaSalida,
                    codalma = almacenSolicitante,
                    unidad = linea.Valor<int>("unidadingresada"),
                    serielote = "0",
                    canti = linea.Valor<decimal>("cantidad"),
                    precio = linea.Valor<decimal>("preciounitario"),
                    subtotal = linea.Valor<decimal>("subtotal"),
                    dscto1 = linea.Valor<decimal>("descuento1"),
                    dscto2 = linea.Valor<decimal>("descuento2"),
                    dscto3 = linea.Valor<decimal>("descuento3"),
                    montodscto = linea.Valor<decimal>("montodscto"),
                    igv = linea.Valor<decimal>("igv"),
                    importe = linea.Valor<decimal>("importe"),
                    precioreal = linea.Valor<decimal>("precioreal"),
                    valoreal = linea.Valor<decimal>("valoreal"),
                    codusu = codUser,
                    cantp = linea.Valor<decimal>("cantidadpendiente")
                }).First();

            if (res == null || res["newid"] == null || Convert.ToInt32(res["newid"]) == 0)
            {
                return Fallo("Fallo al guardar detalle de nota de salida para producto " + prod + " (newid = 0).");
            }
        }

        return new ResultadoAnulacion(true, string.Empty);
    }

    private static int InsertarCabeceraNotaIngreso(
        IConsultor consultor,
        int idExtorno,
        Dictionary<string, object> original,
        int almacenDespacho,
        int codUser)
    {
        Dictionary<string, object> res = consultor.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaNotaIngreso(@codalma, @codtran, @codtipo, @numdoc, 0, NULL, @moneda, @tipocambio, NOW(), '', 0.0, 0.0, 0.0, 0.0, " +
            "@total, @total, 0, 1, NULL, NOW(), 0, @codusu, @codser, @serie, 0, NULL, 0, NULL, '', @codextorno, 0, '', 0, @newid); " +
            "SELECT @newid AS newid;",
            new
            {
                codalma = almacenDespacho,
                codtran = Constantes.TransaccionTransferencia,
                codtipo = Constantes.TipoDocExtornacion,
                numdoc = original.Valor<string>("numerodoc"),
                moneda = original.Valor<int>("moneda"),
                tipocambio = original.Valor<decimal>("tipocambio"),
                total = original.Valor<decimal>("total"),
                codusu = codUser,
                codser = original.Valor<int>("codserie"),
                serie = original.Valor<string>("serie"),
                codextorno = idExtorno
            }).First();

        if (res == null || res["newid"] == null)
        {
            return 0;
        }

        return Convert.ToInt32(res["newid"]);
    }

    private static ResultadoAnulacion InsertarDetallesNotaIngreso(
        IConsultor consultor,
        int idNotaIngreso,
        List<Dictionary<string, object>> lineasOriginal,
        int almacenDespacho,
        int codUser)
    {
        foreach (Dictionary<string, object> linea in lineasOriginal)
        {
            int prod = linea.Valor<int>("codProducto");
            Dictionary<string, object> res = consultor.Consultar(
                "SET @newid = 0; " +
                "CALL GuardaDetalleIngreso(@codpro, @codnota, @codalma, @moneda, @unidad, @serielote, @canti, @precio, @subtotal, " +
                "@dscto1, @dscto2, @dscto3, @montodscto, @igv, 0.0, @importe, @precioreal, @valoreal, NOW(), @codusu, 0.0, 0, 0, 0, @newid); " +
                "SELECT @newid AS newid;",
                new
                {
                    codpro = prod,
                    codnota = idNotaIngreso,
                    codalma = almacenDespacho,
                    moneda = linea.Valor<int>("PrecioIgv"),
                    unidad = linea.Valor<int>("unidadingresada"),
                    serielote = "0",
                    canti = linea.Valor<decimal>("cantidad"),
                    precio = linea.Valor<decimal>("preciounitario"),
                    subtotal = linea.Valor<decimal>("subtotal"),
                    dscto1 = linea.Valor<decimal>("descuento1"),
                    dscto2 = linea.Valor<decimal>("descuento2"),
                    dscto3 = linea.Valor<decimal>("descuento3"),
                    montodscto = linea.Valor<decimal>("montodscto"),
                    igv = linea.Valor<decimal>("igv"),
                    importe = linea.Valor<decimal>("importe"),
                    precioreal = linea.Valor<decimal>("precioreal"),
                    valoreal = linea.Valor<decimal>("valoreal"),
                    codusu = codUser
                }).First();

            if (res == null || res["newid"] == null || Convert.ToInt32(res["newid"]) == 0)
            {
                return Fallo("Fallo al guardar detalle de nota de ingreso para producto " + prod + " (newid = 0).");
            }
        }

        return new ResultadoAnulacion(true, string.Empty);
    }

    private static ResultadoAnulacion Fallo(string mensaje)
    {
        return new ResultadoAnulacion(false, mensaje);
    }
}
