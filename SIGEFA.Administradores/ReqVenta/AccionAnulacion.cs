namespace SIGEFA.Administradores.ReqVenta;

// Acción que corresponde al anular un requerimiento de venta.
public enum AccionAnulacion
{
    // No se anula (la decisión sale denegada).
    Ninguna,

    // Anulación directa de un pendiente (estado 7).
    AnularPendiente,

    // Anulación con extorno de un aprobado transferido (estado 13).
    AnularConExtorno,
}
