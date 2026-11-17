using System;
using SIGEFA.Administradores.PedidoPendiente;
using Xunit;

namespace SIGEFA.Tests.PedidoPendiente;

// Pruebas del texto de diagnostico que se muestra al fallar la carga de un pedido pendiente.
public class CargaPedidoDiagnosticoTests
{
    private static Exception Lanzada()
    {
        try
        {
            object nulo = null;
            nulo.ToString();
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    // Caso real: boleta de un cliente con solo RUC, DNI vacio en el pedido.
    [Fact]
    public void ArmarDetalle_ConContexto_IncluyePasoPedidoClienteYTraza()
    {
        Exception ex = Lanzada();
        CargaPedidoContexto contexto = new CargaPedidoContexto
        {
            CodPedido = "00000491597",
            Numeracion = "72331",
            CodCliente = 29051,
            DniPedido = "",
            BoletaFactura = 1,
            CodUsuario = 99,
            CodAlmacen = 3
        };

        string detalle = CargaPedidoDiagnostico.ArmarDetalle(ex, "Cliente de boleta", contexto);

        Assert.Contains("Paso: Cliente de boleta", detalle);
        Assert.Contains("Pedido: codPedido 00000491597 | numeración 72331", detalle);
        Assert.Contains("Comprobante: BOLETA", detalle);
        Assert.Contains("Cliente: codCliente 29051 | DNI en pedido (vacío)", detalle);
        Assert.Contains("Usuario: 99 | Almacén: 3", detalle);
        Assert.Contains("Tipo: System.NullReferenceException", detalle);
        Assert.Contains("Origen: CargaPedidoDiagnosticoTests.Lanzada", detalle);
        Assert.Contains("Detalle técnico:", detalle);
        Assert.Contains(ex.StackTrace, detalle);
    }

    // Si el pedido ni se llego a leer, igual se informa el paso y el error.
    [Fact]
    public void ArmarDetalle_SinContexto_IndicaQueNoSeCargoElPedido()
    {
        string detalle = CargaPedidoDiagnostico.ArmarDetalle(new InvalidOperationException("falla"), "Leer el pedido", null);

        Assert.Contains("Paso: Leer el pedido", detalle);
        Assert.Contains("Pedido: (no se llegó a cargar)", detalle);
        Assert.Contains("Mensaje: falla", detalle);
    }

    // La traza completa incluye la excepcion interna (por ejemplo un error de MySQL envuelto).
    [Fact]
    public void ArmarDetalle_ConExcepcionInterna_LaIncluye()
    {
        Exception ex = new InvalidOperationException("externa", new ArgumentException("interna"));

        string detalle = CargaPedidoDiagnostico.ArmarDetalle(ex, "Detalle del pedido", null);

        Assert.Contains("interna", detalle);
    }

    [Theory]
    [InlineData(1, "BOLETA")]
    [InlineData(2, "FACTURA")]
    [InlineData(3, "TICKET")]
    [InlineData(9, "DESCONOCIDO (9)")]
    public void TipoComprobante_MapeaBoletafactura(int valor, string esperado)
    {
        Assert.Equal(esperado, CargaPedidoDiagnostico.TipoComprobante(valor));
    }
}
