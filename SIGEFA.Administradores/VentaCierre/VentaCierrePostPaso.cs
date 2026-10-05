// Pasos posteriores a la confirmación de la venta en base de datos.
// Se ejecutan en el diálogo de progreso sin anular la venta en caso de advertencia o error.
namespace SIGEFA.Administradores.VentaCierre;

// Etapas posteriores al cierre de venta en base de datos.
// Orden estricto definido en el plan:
// 1. crearDespacho (primero porque es independiente y puede omitirse si no hay requerimiento)
// 2. guardarCodigoBarras (actualiza codigobarras en pedidosventa)
// 3. generarComprobanteElectronico (genera XML, firma y PDF; el comprobante impreso necesita la firma)
// 4. imprimirComprobante (imprime el comprobante generado con el QR/firma)
public enum VentaCierrePostPaso
{
    // Creación y eventual impresión del despacho de almacén.
    crearDespacho = 0,

    // Actualización de la etiqueta de código de barras en los pedidos venta.
    guardarCodigoBarras = 1,

    // Generación del XML, firma digital, PDF y registro local del comprobante electrónico.
    generarComprobanteElectronico = 2,

    // Impresión física del comprobante de venta.
    imprimirComprobante = 3
}

// Estados posibles de un paso durante y después del proceso de guardado.
public enum VentaCierreEstadoPaso
{
    Pendiente = 0,
    EnCurso = 1,
    Listo = 2,
    Advertencia = 3,
    Omitido = 4
}

// Texto legible en español neutro para los pasos posteriores al cierre.
public static class VentaCierrePostPasoTexto
{
    // Devuelve el nombre legible del paso para la lista de tareas del diálogo.
    public static string obtenerNombre(VentaCierrePostPaso paso)
    {
        switch (paso)
        {
            case VentaCierrePostPaso.crearDespacho:
                return "Crear despacho";
            case VentaCierrePostPaso.guardarCodigoBarras:
                return "Guardar código de barras";
            case VentaCierrePostPaso.generarComprobanteElectronico:
                return "Generar comprobante electrónico";
            case VentaCierrePostPaso.imprimirComprobante:
                return "Imprimir comprobante";
            default:
                return "Paso posterior desconocido";
        }
    }

    // Devuelve la etiqueta de texto para el estado del paso.
    public static string obtenerTextoEstado(VentaCierreEstadoPaso estado)
    {
        switch (estado)
        {
            case VentaCierreEstadoPaso.Pendiente:
                return "Pendiente";
            case VentaCierreEstadoPaso.EnCurso:
                return "En curso";
            case VentaCierreEstadoPaso.Listo:
                return "Listo";
            case VentaCierreEstadoPaso.Advertencia:
                return "Advertencia";
            case VentaCierreEstadoPaso.Omitido:
                return "Omitido";
            default:
                return estado.ToString();
        }
    }
}
