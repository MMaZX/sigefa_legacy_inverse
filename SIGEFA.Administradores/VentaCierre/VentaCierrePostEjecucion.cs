using System;
using System.Collections.Generic;

// Contexto de ejecución de un paso posterior al cierre de la venta.
// Permite acumular uno o más errores (ErrorPaso) sin abortar la ejecución,
// marcar el paso como omitido y proveer contexto de negocio.
namespace SIGEFA.Administradores.VentaCierre;

public class VentaCierrePostEjecucion
{
    // Paso posterior que se está ejecutando.
    public VentaCierrePostPaso paso { get; }

    // Lista de errores o incidencias ocurridas durante este paso.
    public List<ErrorPaso> errores { get; }

    // Indica si el paso fue omitido justificadamente (ej. sin requerimiento).
    public bool omitido { get; private set; }

    // Detalle o motivo por el que se omitió el paso.
    public string detalleOmitido { get; private set; }

    // Contexto de la venta
    public string facturaId { get; set; }
    public string serieNumero { get; set; }
    public string almacen { get; set; }
    public string pedido { get; set; }
    public string usuario { get; set; }

    public VentaCierrePostEjecucion(VentaCierrePostPaso paso)
    {
        this.paso = paso;
        this.errores = new List<ErrorPaso>();
        this.omitido = false;
        this.detalleOmitido = null;
    }

    // Agrega un objeto de error existente a la lista.
    public void agregarError(ErrorPaso error)
    {
        if (error != null)
        {
            errores.Add(error);
        }
    }

    // Crea y agrega un error a partir de una excepción.
    public void agregarError(
        Exception ex,
        string facturaId = null,
        string serieNumero = null,
        string almacen = null,
        string pedido = null,
        string usuario = null)
    {
        if (ex != null)
        {
            errores.Add(new ErrorPaso(
                VentaCierrePostPasoTexto.obtenerNombre(paso),
                ex,
                facturaId ?? this.facturaId,
                serieNumero ?? this.serieNumero,
                almacen ?? this.almacen,
                pedido ?? this.pedido,
                usuario ?? this.usuario));
        }
    }

    // Crea y agrega un error a partir de un mensaje y una excepción opcional.
    public void agregarError(
        string mensaje,
        Exception ex = null,
        string facturaId = null,
        string serieNumero = null,
        string almacen = null,
        string pedido = null,
        string usuario = null)
    {
        errores.Add(new ErrorPaso(
            VentaCierrePostPasoTexto.obtenerNombre(paso),
            mensaje,
            ex,
            facturaId ?? this.facturaId,
            serieNumero ?? this.serieNumero,
            almacen ?? this.almacen,
            pedido ?? this.pedido,
            usuario ?? this.usuario));
    }

    // Marca el paso como omitido con un motivo explicativo.
    public void marcarOmitido(string detalle = "sin requerimiento")
    {
        this.omitido = true;
        this.detalleOmitido = detalle;
    }
}
