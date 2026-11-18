using System;
using System.IO;
using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas del cambio del launcher instalado por uno nuevo (con copia de respaldo y vuelta atras).
public sealed class ReemplazoLauncherTests : IDisposable
{
    private readonly string carpeta = Path.Combine(Path.GetTempPath(), "sigefa_reemplazo_" + Guid.NewGuid().ToString("N"));
    private readonly string destino;
    private readonly string nuevo;

    public ReemplazoLauncherTests()
    {
        Directory.CreateDirectory(carpeta);
        destino = Path.Combine(carpeta, "SIGEFA.Launcher.exe");
        nuevo = Path.Combine(carpeta, "descargado.exe");
        File.WriteAllText(nuevo, "nuevo");
    }

    public void Dispose()
    {
        Directory.Delete(carpeta, true);
    }

    private ResultadoReemplazo Reemplazar(Func<string, bool> verificar, int intentos = 1)
    {
        return ReemplazoLauncher.Reemplazar(destino, nuevo, verificar, intentos, TimeSpan.Zero);
    }

    [Fact]
    public void Reemplazar_Verificado_DejaElNuevoYGuardaElAnterior()
    {
        File.WriteAllText(destino, "viejo");

        var resultado = Reemplazar(ruta => File.ReadAllText(ruta) == "nuevo");

        Assert.Equal(ResultadoReemplazo.Reemplazado, resultado);
        Assert.Equal("nuevo", File.ReadAllText(destino));
        Assert.Equal("viejo", File.ReadAllText(destino + ReemplazoLauncher.SufijoAnterior));
        Assert.False(File.Exists(destino + ".nuevo"));
    }

    [Fact]
    public void Reemplazar_SiElNuevoNoArranca_RestauraElAnterior()
    {
        File.WriteAllText(destino, "viejo");

        var resultado = Reemplazar(ruta => false);

        Assert.Equal(ResultadoReemplazo.Revertido, resultado);
        Assert.Equal("viejo", File.ReadAllText(destino));
    }

    // Si la comprobacion misma se rompe, no se deja un launcher dudoso: se vuelve al anterior.
    [Fact]
    public void Reemplazar_SiLaVerificacionLanzaUnaExcepcion_RestauraElAnterior()
    {
        File.WriteAllText(destino, "viejo");

        var resultado = Reemplazar(ruta => throw new InvalidOperationException("no pudo comprobarse"));

        Assert.Equal(ResultadoReemplazo.Revertido, resultado);
        Assert.Equal("viejo", File.ReadAllText(destino));
    }

    // Mientras el launcher viejo siga abierto Windows no deja tocarlo: no se pierde nada.
    [Fact]
    public void Reemplazar_SiElDestinoEstaBloqueado_NoLoToca()
    {
        File.WriteAllText(destino, "viejo");
        ResultadoReemplazo resultado;
        using (new FileStream(destino, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            resultado = Reemplazar(ruta => true, intentos: 2);
        }

        Assert.Equal(ResultadoReemplazo.NoSePudo, resultado);
        Assert.Equal("viejo", File.ReadAllText(destino));
        Assert.False(File.Exists(destino + ".nuevo"));
    }

    [Fact]
    public void Reemplazar_SinLauncherPrevio_LoInstala()
    {
        var resultado = Reemplazar(ruta => true);

        Assert.Equal(ResultadoReemplazo.Reemplazado, resultado);
        Assert.Equal("nuevo", File.ReadAllText(destino));
    }

    [Fact]
    public void Reemplazar_SinLauncherPrevioYNuevoRoto_NoDejaNada()
    {
        var resultado = Reemplazar(ruta => false);

        Assert.Equal(ResultadoReemplazo.Revertido, resultado);
        Assert.False(File.Exists(destino));
    }
}
