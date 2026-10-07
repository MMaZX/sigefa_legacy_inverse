using System.Collections.Generic;

namespace SIGEFA.Administradores.ReqVenta;

// Catálogo de pasos en español claro para el diálogo de progreso.
// Cada método devuelve la lista ordenada, con todos los pasos en Pendiente
// y en una lista nueva por llamada para que el llamador no altere el catálogo.
public static class ReqVentaTextos
{
    // Claves estables de paso; T2 las repite al emitir el progreso.
    public const string ComprobarRequerimiento = "ComprobarRequerimiento";
    public const string RevisarStock = "RevisarStock";
    public const string CrearExtorno = "CrearExtorno";
    public const string RegistrarSalida = "RegistrarSalida";
    public const string RegistrarIngreso = "RegistrarIngreso";
    public const string AprobarExtorno = "AprobarExtorno";
    public const string MarcarAnulado = "MarcarAnulado";
    public const string RechazarTransferencias = "RechazarTransferencias";
    public const string DevolverReservas = "DevolverReservas";

    // Pasos de la anulación directa de un pendiente, en orden.
    public static IReadOnlyList<PasoOperacion> PasosAnulacionPendiente()
    {
        return new List<PasoOperacion>
        {
            new PasoOperacion(ComprobarRequerimiento, "Comprobar el requerimiento", EstadoPaso.Pendiente),
            new PasoOperacion(RechazarTransferencias, "Rechazar las transferencias pendientes", EstadoPaso.Pendiente),
            new PasoOperacion(DevolverReservas, "Devolver el stock reservado al almacén de despacho", EstadoPaso.Pendiente),
            new PasoOperacion(MarcarAnulado, "Marcar el requerimiento como anulado", EstadoPaso.Pendiente),
        };
    }

    // Pasos de la anulación con extorno, en orden.
    public static IReadOnlyList<PasoOperacion> PasosAnulacionConExtorno()
    {
        return new List<PasoOperacion>
        {
            new PasoOperacion(ComprobarRequerimiento, "Comprobar el requerimiento", EstadoPaso.Pendiente),
            new PasoOperacion(RevisarStock, "Revisar que haya stock para devolver", EstadoPaso.Pendiente),
            new PasoOperacion(CrearExtorno, "Crear el extorno de la transferencia", EstadoPaso.Pendiente),
            new PasoOperacion(RegistrarSalida, "Registrar la nota de salida", EstadoPaso.Pendiente),
            new PasoOperacion(RegistrarIngreso, "Registrar la nota de ingreso en el almacén de despacho", EstadoPaso.Pendiente),
            new PasoOperacion(AprobarExtorno, "Aprobar el extorno", EstadoPaso.Pendiente),
            new PasoOperacion(MarcarAnulado, "Marcar el requerimiento como anulado", EstadoPaso.Pendiente),
        };
    }

    // Arma el paso para informar el progreso con el texto del catálogo por clave.
    // Si la clave no está en el catálogo, usa la clave como texto para no perderla.
    public static PasoOperacion Paso(string clave, EstadoPaso estado, string detalle = null)
    {
        foreach (PasoOperacion paso in PasosAnulacionConExtorno())
        {
            if (paso.Clave == clave)
            {
                return new PasoOperacion(clave, paso.Texto, estado, detalle);
            }
        }

        foreach (PasoOperacion paso in PasosAnulacionPendiente())
        {
            if (paso.Clave == clave)
            {
                return new PasoOperacion(clave, paso.Texto, estado, detalle);
            }
        }

        return new PasoOperacion(clave, clave, estado, detalle);
    }
}
