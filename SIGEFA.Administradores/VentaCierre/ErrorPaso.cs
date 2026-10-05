using System;
using System.Text;

// Representa un error o advertencia ocurrido durante una etapa del cierre de venta.
// Permite conservar diagnósticos detallados sin alterar el resultado del guardado principal.
namespace SIGEFA.Administradores.VentaCierre;

public class ErrorPaso
{
    // Nombre del paso donde se originó el error.
    public string paso { get; set; }

    // Momento exacto en que ocurrió la incidencia.
    public DateTime hora { get; set; }

    // Nombre completo del tipo de excepción (System.Exception, etc.).
    public string tipoExcepcion { get; set; }

    // Mensaje descriptivo del error.
    public string mensaje { get; set; }

    // Mensaje de la excepción interna, si existe.
    public string causaInterna { get; set; }

    // Pila de llamadas asociada a la excepción.
    public string stackTrace { get; set; }

    // Contexto de negocio
    public string facturaId { get; set; }
    public string serieNumero { get; set; }
    public string almacen { get; set; }
    public string pedido { get; set; }
    public string usuario { get; set; }

    public ErrorPaso()
    {
        this.hora = DateTime.Now;
    }

    public ErrorPaso(
        string paso,
        Exception ex,
        string facturaId = null,
        string serieNumero = null,
        string almacen = null,
        string pedido = null,
        string usuario = null)
    {
        this.paso = paso;
        this.hora = DateTime.Now;
        if (ex != null)
        {
            this.tipoExcepcion = ex.GetType().FullName;
            this.mensaje = ex.Message;
            this.causaInterna = ex.InnerException != null ? ex.InnerException.Message : null;
            this.stackTrace = ex.StackTrace;
        }
        this.facturaId = facturaId;
        this.serieNumero = serieNumero;
        this.almacen = almacen;
        this.pedido = pedido;
        this.usuario = usuario;
    }

    public ErrorPaso(
        string paso,
        string mensaje,
        Exception ex = null,
        string facturaId = null,
        string serieNumero = null,
        string almacen = null,
        string pedido = null,
        string usuario = null)
    {
        this.paso = paso;
        this.hora = DateTime.Now;
        this.mensaje = mensaje;
        if (ex != null)
        {
            this.tipoExcepcion = ex.GetType().FullName;
            if (string.IsNullOrEmpty(this.mensaje))
            {
                this.mensaje = ex.Message;
            }
            this.causaInterna = ex.InnerException != null ? ex.InnerException.Message : null;
            this.stackTrace = ex.StackTrace;
        }
        this.facturaId = facturaId;
        this.serieNumero = serieNumero;
        this.almacen = almacen;
        this.pedido = pedido;
        this.usuario = usuario;
    }

    // Devuelve un texto detallado y estructurado de la incidencia para mostrar o copiar al portapapeles.
    public string obtenerDetalleFormateado()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Paso: " + (paso ?? string.Empty));
        sb.AppendLine("Hora: " + hora.ToString("yyyy-MM-dd HH:mm:ss"));
        if (!string.IsNullOrEmpty(tipoExcepcion))
        {
            sb.AppendLine("Tipo: " + tipoExcepcion);
        }
        if (!string.IsNullOrEmpty(mensaje))
        {
            sb.AppendLine("Mensaje: " + VentaCierreRegistroErrores.enmascararCredenciales(mensaje));
        }
        if (!string.IsNullOrEmpty(causaInterna))
        {
            sb.AppendLine("Causa interna: " + VentaCierreRegistroErrores.enmascararCredenciales(causaInterna));
        }
        if (!string.IsNullOrEmpty(facturaId))
        {
            sb.AppendLine("Factura ID: " + facturaId);
        }
        if (!string.IsNullOrEmpty(serieNumero))
        {
            sb.AppendLine("Serie-Número: " + serieNumero);
        }
        if (!string.IsNullOrEmpty(almacen))
        {
            sb.AppendLine("Almacén: " + almacen);
        }
        if (!string.IsNullOrEmpty(pedido))
        {
            sb.AppendLine("Pedido: " + pedido);
        }
        if (!string.IsNullOrEmpty(usuario))
        {
            sb.AppendLine("Usuario: " + usuario);
        }
        if (!string.IsNullOrEmpty(stackTrace))
        {
            sb.AppendLine("StackTrace:");
            sb.AppendLine(stackTrace);
        }
        return sb.ToString();
    }
}
