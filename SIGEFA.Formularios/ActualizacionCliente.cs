using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIGEFA.Administradores.Actualizador;
using SIGEFA.Administradores.VersionApp;

namespace SIGEFA.Formularios;

// Punto unico para que SIGEFA pregunte si hay una version nueva y le pida al launcher que la instale.
// Solo funciona si SIGEFA fue instalado por el launcher; abierto directo no hay nada que hacer y todo se apaga.
internal static class ActualizacionCliente
{
    private static readonly TimeSpan EsperaServidor = TimeSpan.FromSeconds(5);

    private static ConfiguracionActualizacion configuracion;
    private static bool detectada;

    public static ConfiguracionActualizacion Configuracion
    {
        get
        {
            if (!detectada)
            {
                configuracion = ConfiguracionActualizacion.Detectar(
                    AppDomain.CurrentDomain.BaseDirectory,
                    Environment.GetEnvironmentVariable,
                    File.Exists,
                    LectorConfigLauncher.LeerRutaActualizacion);
                detectada = true;
            }
            return configuracion;
        }
    }

    // true si este SIGEFA puede pedir actualizaciones (fue instalado por el launcher).
    public static bool Disponible
    {
        get { return Configuracion != null; }
    }

    // Consulta al servidor en otro hilo, sin bloquear la pantalla. Nunca falla: ante cualquier problema
    // devuelve "al dia" con el motivo, para no molestar ni bloquear a nadie por un servidor caido.
    public static Task<ResultadoConsultaActualizacion> ConsultarAsync()
    {
        var config = Configuracion;
        int buildLocal = VersionCompilacion.Build;
        if (config == null)
        {
            return Task.FromResult(ResultadoConsultaActualizacion.SinConsulta(buildLocal, "SIGEFA no fue instalado por el launcher."));
        }
        return Task.Run(() =>
        {
            try
            {
                return ConsultaActualizacion.Consultar(config.RutaServidor, buildLocal, EsperaServidor);
            }
            catch (Exception ex)
            {
                // Una ruta de servidor invalida u otro fallo inesperado: se trata como "no se pudo consultar".
                return ResultadoConsultaActualizacion.SinConsulta(buildLocal, ex.Message);
            }
        });
    }

    // Abre el launcher, que instala la version nueva y vuelve a abrir SIGEFA.
    public static void LanzarActualizador()
    {
        string exe = Configuracion.ExeLauncher;
        Process.Start(new ProcessStartInfo(exe)
        {
            WorkingDirectory = Path.GetDirectoryName(exe),
            UseShellExecute = false
        });
    }

    // Pregunta al usuario y, si acepta, abre el launcher y cierra la ventana (que cierra SIGEFA).
    // Devuelve true si la actualizacion quedo en marcha.
    public static bool OfrecerActualizar(Form ventana, string mensaje)
    {
        var respuesta = MessageBox.Show(ventana, mensaje, "Actualizar SIGEFA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (respuesta != DialogResult.Yes)
        {
            return false;
        }
        try
        {
            LanzarActualizador();
        }
        catch (Win32Exception ex)
        {
            MessageBox.Show(ventana, "No se pudo abrir el actualizador de SIGEFA:\n\n" + ex.Message, "Actualizar SIGEFA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        ventana.Close();
        return true;
    }
}
