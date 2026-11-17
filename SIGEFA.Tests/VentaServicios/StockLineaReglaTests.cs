using System;
using SIGEFA.Administradores.VentaServicios;
using Xunit;

namespace SIGEFA.Tests.VentaServicios;

// Pruebas de la regla pura de stock por linea de venta (servicios sin stock).
// Sin base de datos ni interfaz grafica.
public class StockLineaReglaTests
{
    // is_enabled_stock = 0 (servicio) no lleva stock; 1 (mercaderia) si.
    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    public void LlevaStock_SegunFlag(int flag, bool esperado)
    {
        Assert.Equal(esperado, StockLineaRegla.LlevaStock(flag));
    }

    // MySQL puede devolver el flag como long o sbyte segun el tipo de la columna.
    [Fact]
    public void LlevaStock_CeroComoLong_NoLlevaStock()
    {
        Assert.False(StockLineaRegla.LlevaStock(0L));
    }

    // Sin flag (columna ausente o nula) se trata como mercaderia: falla cerrado, igual que el SP (IFNULL(is_enabled_stock, 1)).
    [Fact]
    public void LlevaStock_NuloODbNull_LlevaStock()
    {
        Assert.True(StockLineaRegla.LlevaStock(null));
        Assert.True(StockLineaRegla.LlevaStock(DBNull.Value));
    }

    // Un servicio no se limita por stock: cualquier cantidad pasa aunque el stock sea 0.
    [Fact]
    public void StockCubreCantidad_ServicioSinStock_Pasa()
    {
        Assert.True(StockLineaRegla.StockCubreCantidad(false, 2500.75m, 0m));
    }

    // La mercaderia conserva la regla anterior: cantidad <= stock.
    [Theory]
    [InlineData(5, 5, true)]
    [InlineData(6, 5, false)]
    [InlineData(1, 0, false)]
    public void StockCubreCantidad_Mercaderia_IgualQueAntes(int cantidad, int stock, bool esperado)
    {
        Assert.Equal(esperado, StockLineaRegla.StockCubreCantidad(true, cantidad, stock));
    }
}
