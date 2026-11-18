using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SIGEFA.Administradores.Actualizador;

namespace SIGEFA.Launcher;

internal static class Program
{
    private const string NombreMutex = @"Local\SIGEFA.Launcher";

    [STAThread]
    private static int Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var config = ConfiguracionLauncher.Leer();
        RegistroLauncher.Iniciar(config.CarpetaInstalacion);
        // Un solo launcher a la vez por usuario: si hay otro instalando, se espera a que termine.
        using (var mutex = new Mutex(false, NombreMutex))
        {
            if (!Adquirir(mutex))
            {
                return Avisar("Hay otra actualizacion de SIGEFA en curso. Intente de nuevo en un momento.", 1);
            }
            try
            {
                return Ejecutar(config, args);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }

    private static bool Adquirir(Mutex mutex)
    {
        try
        {
            return mutex.WaitOne(TimeSpan.FromSeconds(60));
        }
        catch (AbandonedMutexException)
        {
            // Otro launcher termino de golpe: el candado quedo libre y es nuestro.
            return true;
        }
    }

    private static int Ejecutar(ConfiguracionLauncher config, string[] args)
    {
        var instalador = new InstaladorVersiones(config.CarpetaInstalacion);
        var servidor = new ServidorActualizacion(config.RutaActualizacion, config.EsperaServidor);
        var manifiesto = servidor.LeerManifiesto(out string motivo);
        if (manifiesto == null)
        {
            RegistroLauncher.Escribir("Manifiesto no disponible: " + motivo);
        }
        var accion = PlanLauncher.Decidir(instalador.BuildActual(), manifiesto);
        if (accion == AccionLauncher.SinVersion)
        {
            return Avisar("SIGEFA no esta instalado en este equipo y no se pudo contactar al servidor.\n\n" + motivo, 1);
        }
        string errorActualizacion = accion == AccionLauncher.ActualizarYLanzar
            ? Actualizar(instalador, servidor, manifiesto, config)
            : null;
        string exe = instalador.ExeActual();
        if (exe == null)
        {
            return Avisar("No se pudo instalar SIGEFA.\n\n" + errorActualizacion, 1);
        }
        LanzadorSigefa.Lanzar(exe, args);
        return 0;
    }

    // Devuelve null si salio bien o el motivo del fallo. Si falla, sigue activa la version que ya habia.
    private static string Actualizar(InstaladorVersiones instalador, ServidorActualizacion servidor, ManifiestoVersion manifiesto, ConfiguracionLauncher config)
    {
        string zip = null;
        try
        {
            FormProgreso.Ejecutar("Actualizando SIGEFA a la version " + manifiesto.Build + "...", () =>
            {
                zip = servidor.Descargar(manifiesto, Path.Combine(config.CarpetaInstalacion, "descargas"));
                instalador.Instalar(manifiesto, zip);
            });
            RegistroLauncher.Escribir("Instalada la version " + manifiesto.Build + ".");
            return null;
        }
        catch (Exception ex)
        {
            // Cualquier fallo se registra y se abre la version anterior: el launcher no debe impedir trabajar.
            RegistroLauncher.Escribir("Actualizacion fallida: " + ex);
            return ex.Message;
        }
        finally
        {
            BorrarSiExiste(zip);
        }
    }

    private static void BorrarSiExiste(string archivo)
    {
        if (archivo != null && File.Exists(archivo))
        {
            File.Delete(archivo);
        }
    }

    private static int Avisar(string mensaje, int codigo)
    {
        MessageBox.Show(mensaje, "SIGEFA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return codigo;
    }
}
