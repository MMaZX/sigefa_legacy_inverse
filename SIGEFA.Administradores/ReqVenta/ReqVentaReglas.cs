using System;

namespace SIGEFA.Administradores.ReqVenta;

// Reglas puras de anulación de requerimientos de venta (T2a).
// Sin base de datos ni interfaz gráfica: solo decide a partir del
// tipo de requerimiento y del estado (tabla secciones_de_etiquetas, origen 2).
public static class ReqVentaReglas
{
    // Tipo de requerimiento de venta.
    public const int TipoReqVenta = 2;

    // Estados de la tabla secciones_de_etiquetas (origen 2).
    public const int Pendiente = 7;
    public const int Aprobado = 8;
    public const int Cerrado = 9;
    public const int AtendidaParcial = 10;
    public const int AtendidaTotal = 11;
    public const int Anulado = 12;
    public const int AprobadoTransferido = 13;
    public const int Facturado = 17;

    // Estados que admiten anulación, en un solo lugar fácil de cambiar.
    // (En C# un arreglo no puede ser const, por eso es static readonly).
    public static readonly int[] EstadosAnulables = { Pendiente, AprobadoTransferido };

    // Evalúa si un requerimiento admite anulación y con qué acción.
    // tipoReq distinto de 2 se deniega; el estado 12 ya está anulado;
    // el 7 anula directo y el 13 anula con extorno; el resto se deniega.
    public static DecisionAnulacion Evaluar(int tipoReq, int estado)
    {
        if (tipoReq != TipoReqVenta)
        {
            return Denegar("solo requerimientos de venta: tipo recibido " + tipoReq + ".");
        }

        if (estado == Anulado)
        {
            return Denegar("el requerimiento ya está anulado (estado 12).");
        }

        // EstadosAnulables es la única fuente de verdad: lo que no esté ahí se deniega.
        if (Array.IndexOf(EstadosAnulables, estado) < 0)
        {
            return Denegar("el estado " + estado + " (" + NombreEstado(estado) + ") no admite anulación.");
        }

        switch (estado)
        {
            case Pendiente:
                return new DecisionAnulacion(
                    AccionAnulacion.AnularPendiente,
                    true,
                    "pendiente de atención: admite anulación directa (estado 7).");
            case AprobadoTransferido:
                return new DecisionAnulacion(
                    AccionAnulacion.AnularConExtorno,
                    true,
                    "aprobado y transferido: admite anulación con extorno (estado 13).");
            default:
                // Un estado agregado a EstadosAnulables sin acción definida aquí no debe anular nada.
                return Denegar("el estado " + estado + " (" + NombreEstado(estado) + ") no tiene una acción de anulación definida.");
        }
    }

    // Nombre conocido del estado para que el motivo sea legible;
    // los estados fuera de la tabla se reportan como desconocidos.
    private static string NombreEstado(int estado)
    {
        switch (estado)
        {
            case Pendiente:
                return "pendiente";
            case Aprobado:
                return "aprobado";
            case Cerrado:
                return "cerrado";
            case AtendidaParcial:
                return "atendida parcial";
            case AtendidaTotal:
                return "atendida total";
            case Anulado:
                return "anulado";
            case AprobadoTransferido:
                return "aprobado transferido";
            case Facturado:
                return "facturado";
            default:
                return "desconocido";
        }
    }

    // Atajo para denegar: la acción siempre es Ninguna y el motivo nunca sale vacío.
    private static DecisionAnulacion Denegar(string motivo)
    {
        return new DecisionAnulacion(AccionAnulacion.Ninguna, false, motivo);
    }
}
