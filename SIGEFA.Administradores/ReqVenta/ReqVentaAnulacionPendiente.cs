using System;
using System.Collections.Generic;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.ReqVenta;

// Anulación directa de un requerimiento de venta pendiente (estado 7, T2c).
// Usa la conexión y la transacción del llamador: no abre conexiones nuevas
// ni confirma ni revierte; cualquier fallo sale como resultado fallido para
// que el llamador revierta.
public static class ReqVentaAnulacionPendiente
{
    // Anula un requerimiento pendiente: rechaza sus transferencias pendientes,
    // devuelve el stock reservado y lo marca con estado 12.
    // Informa cada paso al progreso (puede ser null). MarcarAnulado solo se inicia
    // aquí: lo marca Listo el servicio cuando la transacción ya confirmó.
    public static ResultadoAnulacion AnularPendiente(IConsultor consultor, int codReq, int codUser, IProgress<PasoOperacion> progreso = null)
    {
        if (consultor == null)
        {
            return Fallo("el consultor es obligatorio.");
        }

        if (codReq <= 0)
        {
            return Fallo("el requerimiento debe ser mayor que cero.");
        }

        if (codUser <= 0)
        {
            return Fallo("el usuario que anula debe ser mayor que cero.");
        }

        try
        {
            return Ejecutar(consultor, codReq, codUser, progreso);
        }
        catch (Exception ex)
        {
            return Fallo("no se pudo anular el requerimiento " + codReq + ": " + ex.Message);
        }
    }

    // Flujo con la fila del requerimiento ya bloqueada (FOR UPDATE):
    // valida la regla, rechaza transferencias, devuelve stock y marca el 12.
    private static ResultadoAnulacion Ejecutar(IConsultor consultor, int codReq, int codUser, IProgress<PasoOperacion> progreso)
    {
        Informar(progreso, ReqVentaTextos.ComprobarRequerimiento, EstadoPaso.EnCurso);
        Dictionary<string, object> requerimiento = ReqVentaConsultas.ObtenerRequerimiento(consultor, codReq, true);
        if (requerimiento == null)
        {
            return FalloDePaso(progreso, ReqVentaTextos.ComprobarRequerimiento, "el requerimiento " + codReq + " no existe.");
        }

        // El tipo y el estado se leen una sola vez; la regla decide, no se duplica aquí.
        int tipoReq = requerimiento.Valor<int>("tipo_req");
        int estado = requerimiento.Valor<int>("estado");
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(tipoReq, estado);
        switch (decision.Accion)
        {
            case AccionAnulacion.AnularPendiente:
                break;
            default:
                return FalloDePaso(progreso, ReqVentaTextos.ComprobarRequerimiento, decision.Motivo);
        }

        Informar(progreso, ReqVentaTextos.ComprobarRequerimiento, EstadoPaso.Listo);

        Informar(progreso, ReqVentaTextos.RechazarTransferencias, EstadoPaso.EnCurso);
        ResultadoAnulacion rechazo = RechazarPendientes(consultor, codReq);
        if (!rechazo.Ok)
        {
            return FalloDePaso(progreso, ReqVentaTextos.RechazarTransferencias, rechazo.Mensaje);
        }

        Informar(progreso, ReqVentaTextos.RechazarTransferencias, EstadoPaso.Listo);

        Informar(progreso, ReqVentaTextos.DevolverReservas, EstadoPaso.EnCurso);
        int almacenDespacho = requerimiento.Valor<int>("cod_almacen_despacho");
        ResultadoAnulacion devolucion = DevolverReservas(consultor, codReq, almacenDespacho);
        if (!devolucion.Ok)
        {
            return FalloDePaso(progreso, ReqVentaTextos.DevolverReservas, devolucion.Mensaje);
        }

        Informar(progreso, ReqVentaTextos.DevolverReservas, EstadoPaso.Listo);

        // Solo se informa el inicio: Listo lo marca el servicio tras confirmar la transacción.
        Informar(progreso, ReqVentaTextos.MarcarAnulado, EstadoPaso.EnCurso);
        ResultadoEjecucion marcado = consultor.Ejecutar(
            "UPDATE req_almacen SET estado = 12, fecha_anulo = NOW(), cod_user_anulo = @user WHERE id_req_almacen = @id",
            new { user = codUser, id = codReq });
        if (marcado.FilasAfectadas != 1)
        {
            return FalloDePaso(progreso, ReqVentaTextos.MarcarAnulado, "no se pudo marcar como anulado el requerimiento " + codReq + ".");
        }

        return new ResultadoAnulacion(true, "requerimiento " + codReq + " anulado.");
    }

    // Rechaza las transferencias que siguen pendientes (estado 1 y pendiente 1,
    // leídas con bloqueo). El procedimiento no valida el estado (defecto 9),
    // por eso se filtra aquí antes de llamar.
    private static ResultadoAnulacion RechazarPendientes(IConsultor consultor, int codReq)
    {
        List<Dictionary<string, object>> transferencias = ReqVentaConsultas.ObtenerTransferencias(consultor, codReq, true);
        foreach (Dictionary<string, object> transferencia in transferencias)
        {
            int estado = transferencia.Valor<int>("estado");
            int pendiente = transferencia.Valor<int>("pendiente");
            if (estado != 1 || pendiente != 1)
            {
                continue;
            }

            int codTrans = transferencia.Valor<int>("codTransDir");
            ResultadoEjecucion rechazo = consultor.Ejecutar(
                "CALL RechazarTransferencia(@cod, @descripcion)",
                new { cod = codTrans, descripcion = "Anulación del requerimiento pendiente " + codReq + "." });
            if (rechazo.FilasAfectadas != 1)
            {
                return Fallo("no se pudo rechazar la transferencia " + codTrans + " del requerimiento " + codReq + ".");
            }
        }

        return new ResultadoAnulacion(true, string.Empty);
    }

    // Devuelve el stock reservado solo de los detalles con pendiente aprobada mayor
    // que cero (los pendientes suelen tener cero). Replica en C# la lógica de
    // RetornandoStockAlAnularReqAlmacen sin invocarlo: ese procedimiento recibe
    // la cantidad como decimal sin escala y redondea (defecto 3), y sus SELECT
    // INTO dejan el factor en NULL cuando no hay fila (defecto 5).
    private static ResultadoAnulacion DevolverReservas(IConsultor consultor, int codReq, int almacenDespacho)
    {
        List<Dictionary<string, object>> detalle = ReqVentaConsultas.ObtenerDetalle(consultor, codReq);
        foreach (Dictionary<string, object> linea in detalle)
        {
            decimal pendienteAprobada = linea.Valor<decimal>("cantidad_pendiente_aprobada");
            if (pendienteAprobada <= 0)
            {
                continue;
            }

            int producto = linea.Valor<int>("cod_producto");
            int unidad = linea.Valor<int>("cod_unidad");
            ResultadoAnulacion devolucion = DevolverReserva(consultor, almacenDespacho, producto, unidad, pendienteAprobada);
            if (!devolucion.Ok)
            {
                return devolucion;
            }
        }

        return new ResultadoAnulacion(true, string.Empty);
    }

    // Devuelve una línea de reserva al disponible del almacén de despacho.
    // Falla si falta la fila de stock o el factor de unidad, en vez de sumar
    // cero o ensuciar el stock con NULL (defecto 5).
    private static ResultadoAnulacion DevolverReserva(IConsultor consultor, int almacen, int producto, int unidad, decimal cantidad)
    {
        if (almacen <= 0)
        {
            return Fallo("el almacén de despacho no es válido para devolver el stock del producto " + producto + ".");
        }

        Dictionary<string, object> existencia = consultor.Consultar(
            "SELECT Unidad, stockdisponible FROM productoalmacen WHERE codProducto = @prod AND codAlmacen = @alm FOR UPDATE",
            new { prod = producto, alm = almacen }).First();
        if (existencia == null)
        {
            return Fallo("no hay fila de stock para el producto " + producto + " en el almacén " + almacen + ".");
        }

        int? unidadBase = existencia.Valor<int?>("Unidad");
        decimal? disponible = existencia.Valor<decimal?>("stockdisponible");
        if (!unidadBase.HasValue || !disponible.HasValue)
        {
            return Fallo("el stock del producto " + producto + " en el almacén " + almacen + " está incompleto.");
        }

        Dictionary<string, object> equivalencia = consultor.Consultar(
            "SELECT factor FROM unidadequivalente WHERE codProducto = @prod AND codUnidadMedida = @unidad AND codUndEqui = @baseUnd AND compra_venta = 2",
            new { prod = producto, unidad = unidad, baseUnd = unidadBase.Value }).First();
        if (equivalencia == null)
        {
            return Fallo("no hay factor de unidad para el producto " + producto + ".");
        }

        decimal? factor = equivalencia.Valor<decimal?>("factor");
        if (!factor.HasValue || factor.Value <= 0)
        {
            return Fallo("el factor de unidad del producto " + producto + " no es válido.");
        }

        // El cálculo se hace en C# con decimal exacto, sin el redondeo del procedimiento.
        decimal monto = cantidad * factor.Value;
        ResultadoEjecucion devolucion = consultor.Ejecutar(
            "UPDATE productoalmacen SET stockdisponible = stockdisponible + @monto WHERE codProducto = @prod AND codAlmacen = @alm",
            new { monto = monto, prod = producto, alm = almacen });
        if (devolucion.FilasAfectadas != 1)
        {
            return Fallo("no se pudo devolver el stock del producto " + producto + ".");
        }

        return new ResultadoAnulacion(true, string.Empty);
    }

    // Informa el paso con el texto del catálogo; sin progreso no hace nada.
    private static void Informar(IProgress<PasoOperacion> progreso, string clave, EstadoPaso estado, string detalle = null)
    {
        if (progreso == null)
        {
            return;
        }

        progreso.Report(ReqVentaTextos.Paso(clave, estado, detalle));
    }

    // Deja el paso en Error con el motivo y devuelve el fallo para salir de la fase.
    private static ResultadoAnulacion FalloDePaso(IProgress<PasoOperacion> progreso, string clave, string mensaje)
    {
        Informar(progreso, clave, EstadoPaso.Error, mensaje);
        return Fallo(mensaje);
    }

    private static ResultadoAnulacion Fallo(string mensaje)
    {
        return new ResultadoAnulacion(false, mensaje);
    }
}
