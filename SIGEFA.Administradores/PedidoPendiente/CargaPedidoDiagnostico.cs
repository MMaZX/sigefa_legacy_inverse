using System;
using System.Text;

// Arma el texto de diagnostico que se muestra y se guarda cuando falla la carga de un
// pedido pendiente en la venta. Solo formatea: no accede a datos, a la UI ni a archivos.
namespace SIGEFA.Administradores.PedidoPendiente;

// Datos del pedido que acompanan al error para saber en que pedido y cliente fallo la carga.
public sealed class CargaPedidoContexto
{
    public string CodPedido { get; set; }
    public string Numeracion { get; set; }
    public int CodCliente { get; set; }
    public string DniPedido { get; set; }
    public int BoletaFactura { get; set; }
    public int CodUsuario { get; set; }
    public int CodAlmacen { get; set; }
}

public static class CargaPedidoDiagnostico
{
    public const string Titulo = "Error al cargar el pedido";

    // Devuelve el detalle completo: paso, pedido, cliente, tipo de excepcion, metodo de origen
    // y la traza completa (incluye las excepciones internas).
    public static string ArmarDetalle(Exception ex, string paso, CargaPedidoContexto contexto)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Paso: " + Valor(paso));
        AgregarContexto(sb, contexto);
        sb.AppendLine("Tipo: " + (ex == null ? "(sin excepción)" : ex.GetType().FullName));
        sb.AppendLine("Mensaje: " + Valor(ex?.Message));
        sb.AppendLine("Origen: " + Origen(ex));
        sb.AppendLine();
        sb.AppendLine("Detalle técnico:");
        sb.Append(ex == null ? "(sin excepción)" : ex.ToString());
        return sb.ToString();
    }

    // Nombre del comprobante segun pedidosventa.Boletafactura.
    public static string TipoComprobante(int boletaFactura)
    {
        switch (boletaFactura)
        {
            case 1:
                return "BOLETA";
            case 2:
                return "FACTURA";
            case 3:
                return "TICKET";
            default:
                return "DESCONOCIDO (" + boletaFactura + ")";
        }
    }

    private static void AgregarContexto(StringBuilder sb, CargaPedidoContexto contexto)
    {
        if (contexto == null)
        {
            sb.AppendLine("Pedido: (no se llegó a cargar)");
            return;
        }
        sb.AppendLine("Pedido: codPedido " + Valor(contexto.CodPedido) + " | numeración " + Valor(contexto.Numeracion));
        sb.AppendLine("Comprobante: " + TipoComprobante(contexto.BoletaFactura));
        sb.AppendLine("Cliente: codCliente " + contexto.CodCliente + " | DNI en pedido " + Valor(contexto.DniPedido));
        sb.AppendLine("Usuario: " + contexto.CodUsuario + " | Almacén: " + contexto.CodAlmacen);
    }

    private static string Origen(Exception ex)
    {
        if (ex?.TargetSite == null)
        {
            return "(desconocido)";
        }
        string tipo = ex.TargetSite.DeclaringType == null ? "" : ex.TargetSite.DeclaringType.Name + ".";
        return tipo + ex.TargetSite.Name;
    }

    private static string Valor(string texto)
    {
        return string.IsNullOrEmpty(texto) ? "(vacío)" : texto;
    }
}
