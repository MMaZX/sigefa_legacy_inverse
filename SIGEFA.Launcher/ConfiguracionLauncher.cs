using System;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace SIGEFA.Launcher;

// Lo que el launcher lee de su propio .config. Sin ruta de servidor no puede actualizar, pero si abrir lo instalado.
internal sealed class ConfiguracionLauncher
{
    private const int EsperaPorDefectoSegundos = 8;

    private ConfiguracionLauncher(string rutaActualizacion, string carpetaInstalacion, TimeSpan esperaServidor)
    {
        RutaActualizacion = rutaActualizacion;
        CarpetaInstalacion = carpetaInstalacion;
        EsperaServidor = esperaServidor;
    }

    public string RutaActualizacion { get; }

    public string CarpetaInstalacion { get; }

    public TimeSpan EsperaServidor { get; }

    public static ConfiguracionLauncher Leer()
    {
        string ruta = Valor("RutaActualizacion");
        string carpeta = Valor("CarpetaInstalacion");
        if (carpeta.Length == 0)
        {
            carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SIGEFA");
        }
        bool valida = int.TryParse(Valor("EsperaServidorSegundos"), NumberStyles.None, CultureInfo.InvariantCulture, out int segundos) && segundos > 0;
        return new ConfiguracionLauncher(ruta, carpeta, TimeSpan.FromSeconds(valida ? segundos : EsperaPorDefectoSegundos));
    }

    private static string Valor(string clave)
    {
        return (ConfigurationManager.AppSettings[clave] ?? string.Empty).Trim();
    }
}
