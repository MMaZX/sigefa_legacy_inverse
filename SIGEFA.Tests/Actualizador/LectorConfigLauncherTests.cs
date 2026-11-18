using System;
using System.IO;
using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de la lectura de la ruta del servidor desde el .config del launcher.
public class LectorConfigLauncherTests : IDisposable
{
    private readonly string carpeta = Path.Combine(Path.GetTempPath(), "sigefa_cfg_" + Guid.NewGuid().ToString("N"));

    public LectorConfigLauncherTests()
    {
        Directory.CreateDirectory(carpeta);
    }

    public void Dispose()
    {
        Directory.Delete(carpeta, true);
    }

    private string Config(string contenido)
    {
        string ruta = Path.Combine(carpeta, Guid.NewGuid().ToString("N") + ".config");
        File.WriteAllText(ruta, contenido);
        return ruta;
    }

    [Fact]
    public void LeerRutaActualizacion_ConLaClave_DevuelveElValor()
    {
        string ruta = Config(@"<configuration><appSettings><add key=""CarpetaInstalacion"" value="""" /><add key=""RutaActualizacion"" value=""\\srv\actualizador"" /></appSettings></configuration>");

        Assert.Equal(@"\\srv\actualizador", LectorConfigLauncher.LeerRutaActualizacion(ruta));
    }

    [Fact]
    public void LeerRutaActualizacion_SinLaClave_DevuelveNull()
    {
        string ruta = Config(@"<configuration><appSettings><add key=""Otra"" value=""x"" /></appSettings></configuration>");

        Assert.Null(LectorConfigLauncher.LeerRutaActualizacion(ruta));
    }

    [Fact]
    public void LeerRutaActualizacion_ConValorVacio_DevuelveNull()
    {
        string ruta = Config(@"<configuration><appSettings><add key=""RutaActualizacion"" value=""  "" /></appSettings></configuration>");

        Assert.Null(LectorConfigLauncher.LeerRutaActualizacion(ruta));
    }

    [Fact]
    public void LeerRutaActualizacion_ArchivoInexistente_DevuelveNull()
    {
        Assert.Null(LectorConfigLauncher.LeerRutaActualizacion(Path.Combine(carpeta, "no_existe.config")));
    }

    [Fact]
    public void LeerRutaActualizacion_XmlDanado_DevuelveNullSinFallar()
    {
        Assert.Null(LectorConfigLauncher.LeerRutaActualizacion(Config("esto no es xml <<<")));
    }
}
