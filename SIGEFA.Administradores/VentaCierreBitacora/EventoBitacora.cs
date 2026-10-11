using System;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Resultado de un evento del paso a paso.
public enum ResultadoEvento
{
    Ok = 0,
    Error = 1,
    Info = 2
}

// Un paso del cierre, con la hora en que se informó (hilo del servicio).
// El detalle ya llega enmascarado y recortado por BitacoraCierre.
public sealed class EventoBitacora
{
    public EventoBitacora(
        int orden, DateTime hora, int? bloque, string almacen, string paso,
        int? item, int? codProducto, ResultadoEvento resultado, int? duracionMs, string detalle)
    {
        Orden = orden;
        Hora = hora;
        Bloque = bloque;
        Almacen = almacen ?? string.Empty;
        Paso = paso ?? string.Empty;
        Item = item;
        CodProducto = codProducto;
        Resultado = resultado;
        DuracionMs = duracionMs;
        Detalle = detalle ?? string.Empty;
    }

    public int Orden { get; }

    public DateTime Hora { get; }

    public int? Bloque { get; }

    public string Almacen { get; }

    public string Paso { get; }

    public int? Item { get; }

    public int? CodProducto { get; }

    public ResultadoEvento Resultado { get; }

    public int? DuracionMs { get; }

    public string Detalle { get; }
}
