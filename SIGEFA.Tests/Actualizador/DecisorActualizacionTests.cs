using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de la decision: que hace el cliente al comparar su build con el manifiesto.
public class DecisorActualizacionTests
{
    private const string Sha = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";

    private static ManifiestoVersion Manifiesto(int build, int minBuild)
    {
        return ManifiestoVersion.Leer("build=" + build + "\nminbuild=" + minBuild + "\nsha256=" + Sha + "\narchivo=a.zip");
    }

    [Fact]
    public void Evaluar_MismoBuild_EstaAlDia()
    {
        Assert.Equal(EstadoActualizacion.AlDia, DecisorActualizacion.Evaluar(10, Manifiesto(10, 0)));
    }

    // Un cliente mas nuevo que el manifiesto (rollback del servidor) no se baja a una version vieja.
    [Fact]
    public void Evaluar_ClienteMasNuevo_EstaAlDia()
    {
        Assert.Equal(EstadoActualizacion.AlDia, DecisorActualizacion.Evaluar(12, Manifiesto(10, 0)));
    }

    [Fact]
    public void Evaluar_HayVersionNuevaSinMinimo_EsDisponible()
    {
        Assert.Equal(EstadoActualizacion.Disponible, DecisorActualizacion.Evaluar(9, Manifiesto(10, 0)));
    }

    [Fact]
    public void Evaluar_ConMinimoCumplido_SigueSiendoDisponible()
    {
        Assert.Equal(EstadoActualizacion.Disponible, DecisorActualizacion.Evaluar(9, Manifiesto(10, 9)));
    }

    [Fact]
    public void Evaluar_BajoElMinimo_EsObligatoria()
    {
        Assert.Equal(EstadoActualizacion.Obligatoria, DecisorActualizacion.Evaluar(8, Manifiesto(10, 9)));
    }

    // Compilaciones sin numero de build (desarrollo): nunca se avisan ni se bloquean.
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Evaluar_SinNumeroDeBuild_EstaAlDia(int buildLocal)
    {
        Assert.Equal(EstadoActualizacion.AlDia, DecisorActualizacion.Evaluar(buildLocal, Manifiesto(10, 9)));
    }

    // Sin manifiesto (servidor inaccesible) no se bloquea a nadie.
    [Fact]
    public void Evaluar_SinManifiesto_EstaAlDia()
    {
        Assert.Equal(EstadoActualizacion.AlDia, DecisorActualizacion.Evaluar(5, null));
    }
}
