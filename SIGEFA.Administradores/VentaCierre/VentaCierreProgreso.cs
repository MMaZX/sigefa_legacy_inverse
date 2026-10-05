// Avance del cierre de una venta para el diálogo de progreso.
// DTO inmutable: el servicio crea una instancia por cada avance y la reporta
// con IProgress<T> al hilo de la interfaz. Sin lógica de negocio ni acceso a datos.
namespace SIGEFA.Administradores.VentaCierre;

// Foto del progreso en un momento dado. Todos los valores se fijan en el
// constructor y no cambian después (la instancia se puede compartir entre hilos).
public class VentaCierreProgreso
{
    // Crea un reporte de avance. itemActual y bloqueActual empiezan en 1;
    // almacenNombre y mensaje pueden ser vacíos pero nunca nulos.
    public VentaCierreProgreso(
        VentaCierrePaso paso,
        int itemActual,
        int totalItems,
        int bloqueActual,
        int totalBloques,
        string almacenNombre,
        string mensaje)
    {
        this.paso = paso;
        this.itemActual = itemActual;
        this.totalItems = totalItems;
        this.bloqueActual = bloqueActual;
        this.totalBloques = totalBloques;
        this.almacenNombre = almacenNombre ?? string.Empty;
        this.mensaje = mensaje ?? string.Empty;
    }

    // Paso que se está ejecutando o se acaba de terminar.
    public VentaCierrePaso paso { get; }

    // Ítem actual dentro del paso guardarDetalle (1..totalItems).
    public int itemActual { get; }

    // Cantidad total de ítems del bloque actual.
    public int totalItems { get; }

    // Bloque actual (un bloque = un almacén), contado desde 1.
    public int bloqueActual { get; }

    // Cantidad total de bloques de la venta.
    public int totalBloques { get; }

    // Nombre del almacén del bloque actual, para el encabezado "Bloque k/n".
    public string almacenNombre { get; }

    // Texto libre con el detalle del avance (por ejemplo el número de documento).
    public string mensaje { get; }
}
