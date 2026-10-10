using System;
using SIGEFA.Administradores.VentaCierre;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Decorador de IProgress<VentaCierreProgreso>: registra cada avance del cierre
// en la bitácora y después lo delega al progreso interno (el diálogo). Corre en
// el hilo del servicio y jamás lanza por culpa de la bitácora.
public sealed class ProgresoConBitacora : IProgress<VentaCierreProgreso>
{
    private readonly BitacoraCierre _bitacora;
    private readonly IProgress<VentaCierreProgreso> _interno;

    // interno puede ser null (solo se registra).
    public ProgresoConBitacora(BitacoraCierre bitacora, IProgress<VentaCierreProgreso> interno)
    {
        _bitacora = bitacora;
        _interno = interno;
    }

    public void Report(VentaCierreProgreso progreso)
    {
        Registrar(progreso);
        if (_interno != null)
        {
            _interno.Report(progreso);
        }
    }

    private void Registrar(VentaCierreProgreso progreso)
    {
        try
        {
            int? item = progreso.paso == VentaCierrePaso.guardarDetalle ? progreso.itemActual : (int?)null;
            _bitacora.RegistrarEvento(
                progreso.bloqueActual, progreso.almacenNombre, progreso.paso.ToString(),
                item, null, ResultadoEvento.Info, null, progreso.mensaje);
        }
        catch (Exception)
        {
        }
    }

    // Resultado OK al terminar el cierre.
    public static ResultadoBitacora DesdeExito(int bloquesOk, int? codFacturaVenta, DateTime inicio, DateTime fin)
    {
        return new ResultadoBitacora(
            EstadoBitacora.Ok, bloquesOk, fin, Duracion(inicio, fin),
            string.Empty, string.Empty, null, string.Empty, string.Empty, codFacturaVenta);
    }

    // Resultado de error con los datos del fallo ya extraídos (T4 los mapea desde VentaCierreException).
    public static ResultadoBitacora DesdeFallo(
        string errorPaso, string errorProcedimiento, int? errorMysqlNum, string errorSqlState,
        string mensaje, int bloquesOk, DateTime inicio, DateTime fin)
    {
        return new ResultadoBitacora(
            EstadoBitacora.Error, bloquesOk, fin, Duracion(inicio, fin),
            errorPaso, errorProcedimiento, errorMysqlNum, errorSqlState, mensaje, null);
    }

    // Resultado de error para una excepción cualquiera: solo se conoce el mensaje.
    public static ResultadoBitacora DesdeExcepcion(Exception ex, int bloquesOk, DateTime inicio, DateTime fin)
    {
        string mensaje = ex == null ? string.Empty : ex.Message;
        return DesdeFallo(string.Empty, string.Empty, null, string.Empty, mensaje, bloquesOk, inicio, fin);
    }

    private static int Duracion(DateTime inicio, DateTime fin)
    {
        return (int)(fin - inicio).TotalMilliseconds;
    }
}
