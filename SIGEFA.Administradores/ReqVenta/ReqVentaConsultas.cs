using System;
using System.Collections.Generic;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.ReqVenta;

// Lecturas de requerimientos de venta (T2b).
// Solo SELECT con parametros; nunca escribe ni muestra dialogos.
public static class ReqVentaConsultas
{
    // Nombres de columna en un solo lugar para no repetir literales.
    private static class Columnas
    {
        public const string IdReq = "id_req_almacen";
        public const string Estado = "estado";
        public const string TipoReq = "tipo_req";
        public const string AlmacenSolicitante = "cod_almacen_solicitante";
        public const string AlmacenDespacho = "cod_almacen_despacho";
        public const string CodPedidoVenta = "codPedidoVenta";
        public const string CodFacturaVenta = "codFacturaVenta";

        public const string CodTransDir = "codTransDir";
        public const string AlmacenOrigen = "codAlmacenOrigen";
        public const string AlmacenDestino = "codAlmacenDestino";
        public const string Total = "total";
        public const string Pendiente = "pendiente";
        public const string TieneExtorno = "tiene_extorno";
        public const string CodDocExtornacion = "codDocExtornacion";

        public const string IdDetalle = "id_det_req_almacen";
        public const string CodProducto = "cod_producto";
        public const string CodUnidad = "cod_unidad";
        public const string Cantidad = "cantidad";
        public const string CantidadPedida = "cantidad_pedida";
        public const string CantidadConfirmada = "cantidad_confirmada";
        public const string CantidadPendiente = "cantidad_pendiente";
        public const string CantidadPendienteAprobada = "cantidad_pendiente_aprobada";
    }

    // Lee el encabezado del requerimiento o null si no existe.
    // Si bloquear es verdadero agrega FOR UPDATE para retener la fila.
    public static Dictionary<string, object> ObtenerRequerimiento(IConsultor consultor, int codReq, bool bloquear)
    {
        if (consultor == null)
        {
            throw new ArgumentNullException(nameof(consultor));
        }

        string sql = "SELECT " + Columnas.IdReq + ", " + Columnas.Estado + ", " + Columnas.TipoReq + ", "
            + Columnas.AlmacenSolicitante + ", " + Columnas.AlmacenDespacho + ", "
            + Columnas.CodPedidoVenta + ", " + Columnas.CodFacturaVenta
            + " FROM req_almacen WHERE " + Columnas.IdReq + " = @id";
        if (bloquear)
        {
            sql += " FOR UPDATE";
        }

        return consultor.Consultar(sql, new { id = codReq }).First();
    }

    // Lee las transferencias originales del requerimiento (excluye extornos).
    // tiene_extorno indica si la original ya tiene extorno (LEFT JOIN por codDocExtornacion).
    // El orden por codTransDir es estable; con bloqueo termina en FOR UPDATE.
    public static List<Dictionary<string, object>> ObtenerTransferencias(IConsultor consultor, int codReq, bool bloquear)
    {
        if (consultor == null)
        {
            throw new ArgumentNullException(nameof(consultor));
        }

        string sql = "SELECT o." + Columnas.CodTransDir + ", o." + Columnas.AlmacenOrigen + ", o." + Columnas.AlmacenDestino + ", o." + Columnas.Total + ", "
            + "o." + Columnas.Estado + "+0 AS " + Columnas.Estado + ", o." + Columnas.Pendiente + "+0 AS " + Columnas.Pendiente + ", "
            + "(e." + Columnas.CodTransDir + " IS NOT NULL) AS " + Columnas.TieneExtorno + " "
            + "FROM transferencia o LEFT JOIN transferencia e ON e." + Columnas.CodDocExtornacion + " = o." + Columnas.CodTransDir + " "
            + "WHERE o." + Columnas.IdReq + " = @id AND o." + Columnas.CodDocExtornacion + " IS NULL "
            + "ORDER BY o." + Columnas.CodTransDir;
        if (bloquear)
        {
            sql += " FOR UPDATE";
        }

        return consultor.Consultar(sql, new { id = codReq }).Get();
    }

    // Lee el detalle del requerimiento. Sin bloqueo propio:
    // el bloqueo lo hace el SELECT del requerimiento.
    public static List<Dictionary<string, object>> ObtenerDetalle(IConsultor consultor, int codReq)
    {
        if (consultor == null)
        {
            throw new ArgumentNullException(nameof(consultor));
        }

        string sql = "SELECT " + Columnas.IdDetalle + ", " + Columnas.IdReq + ", " + Columnas.CodProducto + ", " + Columnas.CodUnidad + ", "
            + Columnas.Cantidad + ", " + Columnas.CantidadPedida + ", " + Columnas.CantidadConfirmada + ", "
            + Columnas.CantidadPendiente + ", " + Columnas.CantidadPendienteAprobada
            + " FROM detalle_req_almacen WHERE " + Columnas.IdReq + " = @id "
            + "ORDER BY " + Columnas.IdDetalle;

        return consultor.Consultar(sql, new { id = codReq }).Get();
    }
}
