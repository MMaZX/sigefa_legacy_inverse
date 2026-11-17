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

    [Fact]
    public void Detalle_MuestraCommitConAsuntoFechaVersionYCambios()
    {
        string texto = VersionTexto.Detalle(new VersionDatos
        {
            Version = "1.0.0.0",
            Commit = "4fbd707",
            Asunto = "feat(login): muestra la version",
            ConCambiosLocales = false,
            FechaCompilacion = "2026-11-17 16:40",
            BaseDatos = "database_multi_final",
            Servidor = "192.168.1.129:3307",
            Equipo = "CAJA-01",
            RutaEjecutable = @"C:\SIGEFA\SIGEFA.exe"
        });

        Assert.Contains("Último commit: 4fbd707 - feat(login): muestra la version", texto);
        Assert.Contains("Fecha y hora del build: 2026-11-17 16:40", texto);
        Assert.Contains("Versión del ensamblado: 1.0.0", texto);
        Assert.Contains("Cambios sin commitear: No", texto);
        Assert.Contains("Base de datos: database_multi_final", texto);
        Assert.Contains("Servidor: 192.168.1.129:3307", texto);
        Assert.Contains("Equipo: CAJA-01", texto);
        Assert.Contains(@"Ejecutable: C:\SIGEFA\SIGEFA.exe", texto);
    }

    // La rama ya no se muestra en el dialogo.
    [Fact]
    public void Detalle_NoMuestraLaRama()
    {
        string texto = VersionTexto.Detalle(new VersionDatos { Commit = "4fbd707" });

        Assert.DoesNotContain("Rama", texto);
    }

    [Fact]
    public void Detalle_SinDatos_NoFallaYLoDice()
    {
        string texto = VersionTexto.Detalle(new VersionDatos { ConCambiosLocales = true });

        Assert.Contains("Último commit: " + VersionTexto.Desconocido, texto);
        Assert.Contains("Fecha y hora del build: desconocido", texto);
        Assert.Contains("Cambios sin commitear: Sí", texto);
    }
}
