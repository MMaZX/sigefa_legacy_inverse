using System;
using System.Collections.Generic;
using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de como un SIGEFA instalado por el launcher encuentra el launcher y el servidor.
public class ConfiguracionActualizacionTests
{
    private const string Base = @"C:\Users\ana\AppData\Local\SIGEFA\versions\245\";
    private const string ExeConvencion = @"C:\Users\ana\AppData\Local\SIGEFA\launcher\SIGEFA.Launcher.exe";

    private static ConfiguracionActualizacion Detectar(
        string directorioBase,
        IDictionary<string, string> entorno,
        Func<string, bool> existe,
        Func<string, string> ruta)
    {
        return ConfiguracionActualizacion.Detectar(
            directorioBase,
            nombre => entorno.TryGetValue(nombre, out string valor) ? valor : null,
            existe,
            ruta);
    }

    private static readonly IDictionary<string, string> SinEntorno = new Dictionary<string, string>();

    [Fact]
    public void Detectar_PorConvencion_EncuentraElLauncherYLeeLaRutaDeSuConfig()
    {
        var config = Detectar(Base, SinEntorno, ruta => ruta == ExeConvencion, ruta => ruta == ExeConvencion + ".config" ? @"\\srv\actualizador" : null);

        Assert.NotNull(config);
        Assert.Equal(ExeConvencion, config.ExeLauncher);
        Assert.Equal(@"\\srv\actualizador", config.RutaServidor);
    }

    [Fact]
    public void Detectar_ConBarraFinalOSinElla_DaLoMismo()
    {
        var conBarra = Detectar(Base, SinEntorno, ruta => true, ruta => @"\\srv\a");
        var sinBarra = Detectar(Base.TrimEnd('\\'), SinEntorno, ruta => true, ruta => @"\\srv\a");

        Assert.Equal(conBarra.ExeLauncher, sinBarra.ExeLauncher);
    }

    // SIGEFA abierto directo (desarrollo o acceso directo viejo): no esta en "versions", no hay a quien pedirle que actualice.
    [Fact]
    public void Detectar_FueraDeLaCarpetaVersions_NoHayConfiguracion()
    {
        Assert.Null(Detectar(@"C:\Debug_GR\", SinEntorno, ruta => true, ruta => @"\\srv\a"));
    }

    [Fact]
    public void Detectar_SinLauncherInstalado_NoHayConfiguracion()
    {
        Assert.Null(Detectar(Base, SinEntorno, ruta => false, ruta => @"\\srv\a"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Detectar_ConfigSinRutaDeServidor_NoHayConfiguracion(string rutaLeida)
    {
        Assert.Null(Detectar(Base, SinEntorno, ruta => true, ruta => rutaLeida));
    }

    // Si el launcher fija las variables de entorno, mandan sobre la convencion de carpetas.
    [Fact]
    public void Detectar_ConVariablesDeEntorno_GananSobreLaConvencion()
    {
        var entorno = new Dictionary<string, string>
        {
            [ConfiguracionActualizacion.VariableLauncher] = @"D:\otro\SIGEFA.Launcher.exe",
            [ConfiguracionActualizacion.VariableServidor] = @"\\otro\servidor"
        };

        var config = Detectar(@"C:\cualquier\lugar\", entorno, ruta => ruta == @"D:\otro\SIGEFA.Launcher.exe", ruta => null);

        Assert.Equal(@"D:\otro\SIGEFA.Launcher.exe", config.ExeLauncher);
        Assert.Equal(@"\\otro\servidor", config.RutaServidor);
    }
}
