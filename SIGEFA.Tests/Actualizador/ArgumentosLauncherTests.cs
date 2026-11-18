using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de como el launcher interpreta su linea de comandos.
public class ArgumentosLauncherTests
{
    [Fact]
    public void Interpretar_SinArgumentos_EsArranqueNormal()
    {
        var a = ArgumentosLauncher.Interpretar(new string[0]);

        Assert.Equal(ModoLauncher.Normal, a.Modo);
        Assert.False(a.OmitirAutoactualizacion);
        Assert.Empty(a.ArgumentosSigefa);
    }

    [Fact]
    public void Interpretar_Null_EsArranqueNormal()
    {
        Assert.Equal(ModoLauncher.Normal, ArgumentosLauncher.Interpretar(null).Modo);
    }

    // Lo que no es del launcher se le pasa a SIGEFA sin tocarlo.
    [Fact]
    public void Interpretar_ArgumentosDeSigefa_SePasanTalCual()
    {
        var a = ArgumentosLauncher.Interpretar(new[] { "--usuario", "ana" });

        Assert.Equal(ModoLauncher.Normal, a.Modo);
        Assert.Equal(new[] { "--usuario", "ana" }, a.ArgumentosSigefa);
    }

    [Fact]
    public void Interpretar_Verificar_EsModoVerificar()
    {
        Assert.Equal(ModoLauncher.Verificar, ArgumentosLauncher.Interpretar(new[] { "--verificar-launcher" }).Modo);
    }

    [Fact]
    public void Interpretar_Reemplazar_LeePidDestinoNuevoYArgumentosDeSigefa()
    {
        var a = ArgumentosLauncher.Interpretar(new[] { "--reemplazar-launcher", "1234", @"C:\L\SIGEFA.Launcher.exe", @"C:\D\nuevo.exe", "--", "--usuario", "ana" });

        Assert.Equal(ModoLauncher.Reemplazar, a.Modo);
        Assert.Equal(1234, a.PidEsperado);
        Assert.Equal(@"C:\L\SIGEFA.Launcher.exe", a.Destino);
        Assert.Equal(@"C:\D\nuevo.exe", a.Nuevo);
        Assert.Equal(new[] { "--usuario", "ana" }, a.ArgumentosSigefa);
    }

    [Fact]
    public void Interpretar_ReemplazarSinArgumentosDeSigefa_LosDejaVacios()
    {
        var a = ArgumentosLauncher.Interpretar(new[] { "--reemplazar-launcher", "7", "d.exe", "n.exe" });

        Assert.Equal(ModoLauncher.Reemplazar, a.Modo);
        Assert.Empty(a.ArgumentosSigefa);
    }

    // Un reemplazo mal formado nunca debe impedir abrir SIGEFA: se trata como arranque normal.
    [Theory]
    [InlineData("--reemplazar-launcher", "abc", "d.exe", "n.exe")]
    [InlineData("--reemplazar-launcher", "0", "d.exe", "n.exe")]
    [InlineData("--reemplazar-launcher", "5", "d.exe")]
    [InlineData("--reemplazar-launcher", "5", "", "n.exe")]
    public void Interpretar_ReemplazoMalFormado_EsArranqueNormal(params string[] args)
    {
        var a = ArgumentosLauncher.Interpretar(args);

        Assert.Equal(ModoLauncher.Normal, a.Modo);
        Assert.Equal(args, a.ArgumentosSigefa);
    }

    [Fact]
    public void Interpretar_SinAutoactualizacion_SeQuitaDeLosArgumentosDeSigefa()
    {
        var a = ArgumentosLauncher.Interpretar(new[] { "--sin-autoactualizacion", "--usuario", "ana" });

        Assert.Equal(ModoLauncher.Normal, a.Modo);
        Assert.True(a.OmitirAutoactualizacion);
        Assert.Equal(new[] { "--usuario", "ana" }, a.ArgumentosSigefa);
    }

    [Fact]
    public void ParaReemplazar_SeInterpretaDeVueltaSinPerderNada()
    {
        var args = ArgumentosLauncher.ParaReemplazar(99, @"C:\L\a.exe", @"C:\D\b.exe", new[] { "x", "y" });

        var a = ArgumentosLauncher.Interpretar(args);

        Assert.Equal(ModoLauncher.Reemplazar, a.Modo);
        Assert.Equal(99, a.PidEsperado);
        Assert.Equal(@"C:\L\a.exe", a.Destino);
        Assert.Equal(@"C:\D\b.exe", a.Nuevo);
        Assert.Equal(new[] { "x", "y" }, a.ArgumentosSigefa);
    }

    // El launcher relanzado no debe volver a intentar actualizarse: seria un bucle si el reemplazo fallo.
    [Fact]
    public void ParaRelanzar_SeInterpretaComoSinAutoactualizacion()
    {
        var a = ArgumentosLauncher.Interpretar(ArgumentosLauncher.ParaRelanzar(new[] { "x" }));

        Assert.True(a.OmitirAutoactualizacion);
        Assert.Equal(new[] { "x" }, a.ArgumentosSigefa);
    }
}
