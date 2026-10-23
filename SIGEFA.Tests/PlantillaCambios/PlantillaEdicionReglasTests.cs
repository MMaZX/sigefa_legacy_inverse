using SIGEFA.Administradores.PlantillaCambios;
using Xunit;

namespace SIGEFA.Tests.PlantillaCambios;

// Que ediciones de la grilla de plantilla se guardan y como se completa el stock minimo.
public class PlantillaEdicionReglasTests
{
    [Theory]
    [InlineData("colTxtStockMinimo")]
    [InlineData("colTxtStockMaximo")]
    [InlineData("colUndXPaquete")]
    [InlineData("unidadd")]
    public void RegistraCambio_ColumnasEditablesQueSeGuardan(string columna)
    {
        Assert.True(PlantillaEdicionReglas.RegistraCambio(columna));
    }

    [Theory]
    [InlineData("referencias")]
    [InlineData("descripcionn")]
    [InlineData("marcaa")]
    [InlineData("stock_Actual")]
    public void NoRegistraCambio_ColumnasDeSoloLectura(string columna)
    {
        Assert.False(PlantillaEdicionReglas.RegistraCambio(columna));
    }

    [Fact]
    public void MinimoPorDefecto_MinimoVacio_EsLaMitadDelMaximo()
    {
        Assert.Equal(5.0, PlantillaEdicionReglas.MinimoPorDefecto("10", ""));
    }

    [Fact]
    public void MinimoPorDefecto_MinimoNulo_EsLaMitadDelMaximo()
    {
        Assert.Equal(5.0, PlantillaEdicionReglas.MinimoPorDefecto("10", null));
    }

    [Fact]
    public void MinimoPorDefecto_MinimoYaEditado_NoSePisa()
    {
        Assert.Null(PlantillaEdicionReglas.MinimoPorDefecto("10", "3"));
    }

    [Fact]
    public void MinimoPorDefecto_MaximoNoNumerico_NoCambiaElMinimo()
    {
        Assert.Null(PlantillaEdicionReglas.MinimoPorDefecto("abc", ""));
    }
}
