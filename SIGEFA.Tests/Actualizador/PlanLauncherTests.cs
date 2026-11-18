using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de lo que hace el launcher al arrancar.
public class PlanLauncherTests
{
    private const string Sha = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";

    private static ManifiestoVersion Manifiesto(int build)
    {
        return ManifiestoVersion.Leer("build=" + build + "\nsha256=" + Sha + "\narchivo=a.zip");
    }

    [Fact]
    public void Decidir_MismoBuild_LanzaLoInstalado()
    {
        Assert.Equal(AccionLauncher.Lanzar, PlanLauncher.Decidir(10, Manifiesto(10)));
    }

    [Fact]
    public void Decidir_ServidorTieneUnaMasNueva_Actualiza()
    {
        Assert.Equal(AccionLauncher.ActualizarYLanzar, PlanLauncher.Decidir(9, Manifiesto(10)));
    }

    // Primera vez en la PC: no hay nada instalado, se instala lo del servidor.
    [Fact]
    public void Decidir_NadaInstalado_ConManifiesto_Instala()
    {
        Assert.Equal(AccionLauncher.ActualizarYLanzar, PlanLauncher.Decidir(0, Manifiesto(10)));
    }

    // Rollback del servidor: nunca se baja a una version mas vieja.
    [Fact]
    public void Decidir_ServidorTieneUnaMasVieja_ConservaLaInstalada()
    {
        Assert.Equal(AccionLauncher.Lanzar, PlanLauncher.Decidir(12, Manifiesto(10)));
    }

    // Servidor caido con SIGEFA ya instalado: se abre igual, las ventas no se paran.
    [Fact]
    public void Decidir_SinManifiesto_ConVersionInstalada_Lanza()
    {
        Assert.Equal(AccionLauncher.Lanzar, PlanLauncher.Decidir(10, null));
    }

    [Fact]
    public void Decidir_SinManifiesto_YNadaInstalado_NoHayVersion()
    {
        Assert.Equal(AccionLauncher.SinVersion, PlanLauncher.Decidir(0, null));
    }
}
