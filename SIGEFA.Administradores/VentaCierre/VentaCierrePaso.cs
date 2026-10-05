// Pasos del cierre de una venta en la ruta nueva (paralela a la ruta vieja).
// Cada valor representa una etapa dentro de la transacción de un bloque
// (un bloque = un almacén). El orden de los valores refleja el orden de
// ejecución: primero se bloquea la serie, luego el stock, y recién después
// se escriben cabecera, detalle y pagos. Sin lógica de negocio ni acceso a datos.
namespace SIGEFA.Administradores.VentaCierre;

// Etapa del cierre de venta. Los nombres usan camelCase de dominio,
// tal como lo exige el plan de la ruta nueva.
public enum VentaCierrePaso
{
    // Abre la conexión propia y la transacción explícita (sin TransactionScope).
    abrirTransaccion = 0,

    // Bloquea la fila de la serie con SELECT ... FOR UPDATE (primera lectura).
    bloquearSerie = 1,

    // Bloquea las filas de productoalmacen con SELECT ... FOR UPDATE,
    // en orden estable de productoId y sin duplicados.
    bloquearStock = 2,

    // Guarda la cabecera con GuardaFacturaVenta y obtiene facturaVentaId.
    guardarCabecera = 3,

    // Guarda cada ítem con GuardaDetalleFacturaVenta (informa itemActual).
    guardarDetalle = 4,

    // Guarda cada pago capturado con GuardaPago (solo si hay borradores).
    guardarPago = 5,

    // Confirma la transacción con Commit (ya fuera del try de escritura).
    confirmar = 6
}

// Texto legible en español para mostrar cada paso en el diálogo de progreso.
// Clase sin estado: solo transforma un valor del enum en su descripción.
public static class VentaCierrePasoTexto
{
    // Devuelve el nombre legible del paso para la lista de tareas del diálogo.
    public static string obtenerNombre(VentaCierrePaso paso)
    {
        switch (paso)
        {
            case VentaCierrePaso.abrirTransaccion:
                return "Abrir transacción";
            case VentaCierrePaso.bloquearSerie:
                return "Bloquear serie";
            case VentaCierrePaso.bloquearStock:
                return "Bloquear stock";
            case VentaCierrePaso.guardarCabecera:
                return "Guardar cabecera";
            case VentaCierrePaso.guardarDetalle:
                return "Guardar detalle";
            case VentaCierrePaso.guardarPago:
                return "Guardar pago";
            case VentaCierrePaso.confirmar:
                return "Confirmar";
            default:
                return "Paso desconocido";
        }
    }
}
