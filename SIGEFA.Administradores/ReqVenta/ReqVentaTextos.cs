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
            new PasoOperacion(RechazarTransferencias, "Rechazar los envíos pendientes", EstadoPaso.Pendiente),
            new PasoOperacion(DevolverReservas, "Devolver las reservas al almacén", EstadoPaso.Pendiente),
            new PasoOperacion(MarcarAnulado, "Marcar el requerimiento como anulado", EstadoPaso.Pendiente),
        };
    }

    // Pasos de la anulación con extorno, en orden.
    public static IReadOnlyList<PasoOperacion> PasosAnulacionConExtorno()
    {
        return new List<PasoOperacion>
        {
            new PasoOperacion(ComprobarRequerimiento, "Comprobar el requerimiento", EstadoPaso.Pendiente),
            new PasoOperacion(RevisarStock, "Revisar el stock disponible", EstadoPaso.Pendiente),
            new PasoOperacion(CrearExtorno, "Crear el movimiento que revierte el envío", EstadoPaso.Pendiente),
            new PasoOperacion(RegistrarSalida, "Registrar la salida del almacén", EstadoPaso.Pendiente),
            new PasoOperacion(RegistrarIngreso, "Registrar el ingreso al almacén de despacho", EstadoPaso.Pendiente),
            new PasoOperacion(AprobarExtorno, "Aprobar la reversión", EstadoPaso.Pendiente),
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
