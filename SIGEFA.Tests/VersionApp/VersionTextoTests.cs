using SIGEFA.Administradores.VersionApp;
using Xunit;

namespace SIGEFA.Tests.VersionApp;

// Pruebas del texto de version que muestra el login.
public class VersionTextoTests
{
    [Fact]
    public void Formatear_ConTodo_MuestraVersionRamaCommitYFecha()
    {
        string texto = VersionTexto.Formatear("1.0.0.0", "a1228db", "feat/x", false, "2026-11-17 16:40");

        Assert.Equal("Versión 1.0.0 · feat/x@a1228db · 2026-11-17 16:40", texto);
    }

    // Si se compilo con cambios sin commitear, el commit no describe todo el codigo.
    [Fact]
    public void Formatear_ConCambiosLocales_LoMarca()
    {
        string texto = VersionTexto.Formatear("1.0.0.0", "a1228db", "main", true, "2026-11-17 16:40");

        Assert.Equal("Versión 1.0.0 · main@a1228db+cambios · 2026-11-17 16:40", texto);
    }

    // Compilado sin git (por ejemplo desde un zip): no falla y lo dice.
    [Fact]
    public void Formatear_SinGit_UsaDesconocido()
    {
        string texto = VersionTexto.Formatear("1.0.0.0", "", "", false, "");

        Assert.Equal("Versión 1.0.0 · " + VersionTexto.Desconocido, texto);
    }
}
