using System;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Estado final de un intento.
public enum EstadoBitacora
{
    Ok = 0,
    Error = 1
}

// Cierre del intento: estado, tiempos y, si falló, el detalle del error.
// ErrorMensaje llega enmascarado y recortado por BitacoraCierre.
public sealed class ResultadoBitacora
{
    public ResultadoBitacora(
        EstadoBitacora estado, int bloquesOk, DateTime fin, int duracionMs,
        string errorPaso, string errorProcedimiento, int? errorMysqlNum,
        string errorSqlState, string errorMensaje, int? codFacturaVenta)
    {
        Estado = estado;
        BloquesOk = bloquesOk;
        Fin = fin;
        DuracionMs = duracionMs;
        ErrorPaso = errorPaso ?? string.Empty;
        ErrorProcedimiento = errorProcedimiento ?? string.Empty;
        ErrorMysqlNum = errorMysqlNum;
        ErrorSqlState = errorSqlState ?? string.Empty;
        ErrorMensaje = errorMensaje ?? string.Empty;
        CodFacturaVenta = codFacturaVenta;
    }

    public EstadoBitacora Estado { get; }

    public int BloquesOk { get; }

    public DateTime Fin { get; }

    public int DuracionMs { get; }

    public string ErrorPaso { get; }

    public string ErrorProcedimiento { get; }

    public int? ErrorMysqlNum { get; }

    public string ErrorSqlState { get; }

    public string ErrorMensaje { get; }

    public int? CodFacturaVenta { get; }
}
