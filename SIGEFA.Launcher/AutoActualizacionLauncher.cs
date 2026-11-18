using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using SIGEFA.Administradores.Actualizador;

namespace SIGEFA.Launcher;

// El launcher se actualiza solo. Windows no deja sobrescribir un .exe en ejecucion, asi que:
//  1. El launcher viejo baja el nuevo, comprueba su sha256 y que arranca, y lanza una copia de si mismo desde %TEMP%.
//  2. Esa copia espera a que el viejo se cierre, cambia el exe instalado (guardando el anterior como .anterior),
//     comprueba que el nuevo arranca (si no, restaura el anterior) y lo abre para que SIGEFA arranque con normalidad.
// Todo falla "hacia abierto": ante cualquier problema se sigue con el launcher que ya habia y SIGEFA abre igual.
internal static class AutoActualizacionLauncher
{
    private const string NombreExe = "SIGEFA.Launcher.exe";
    private const string NombreExeNuevo = "SIGEFA.Launcher.nuevo.exe";
    private const string PrefijoTemporal = "sigefa_launcher_";
    private const int EsperaVerificacionMs = 15000;
    private const int EsperaCierreOriginalMs = 30000;
    private const int IntentosReemplazo = 10;

    // Devuelve true si dejo en marcha el reemplazo: el launcher actual debe terminar YA, sin abrir SIGEFA
    // (lo abrira el launcher nuevo). Devuelve false si no hay nada que hacer o algo fallo: se sigue normal.
    public static bool Intentar(ConfiguracionLauncher config, ServidorActualizacion servidor, string[] argumentosSigefa)
    {
        try
        {
            string propio = Assembly.GetExecutingAssembly().Location;
            if (propio.StartsWith(@"\\", StringComparison.Ordinal))
            {
                // Ejecutado desde el servidor (solo lectura para los clientes): no hay nada que reemplazar.
                return false;
            }
            var manifiesto = servidor.LeerManifiestoLauncher(out string motivo);
            if (manifiesto == null)
            {
                RegistroLauncher.Escribir("Ficha del launcher no disponible: " + motivo);
                return false;
            }
            if (PlanLauncher.Decidir(VersionLauncher.Build, manifiesto) != AccionLauncher.ActualizarYLanzar)
            {
                return false;
            }
            string nuevo = null;
            FormProgreso.Ejecutar("Actualizando el launcher a la version " + manifiesto.Build + "...", () =>
            {
                nuevo = Preparar(servidor, manifiesto, config);
            });
            IniciarReemplazo(propio, nuevo, argumentosSigefa);
            RegistroLauncher.Escribir("Launcher " + VersionLauncher.Build + " -> " + manifiesto.Build + ": reemplazo en marcha.");
            return true;
        }
        catch (Exception ex)
        {
            RegistroLauncher.Escribir("Autoactualizacion del launcher fallida: " + ex);
            return false;
        }
    }

    // Modo --reemplazar-launcher: corre en la copia temporal. Siempre termina abriendo el launcher instalado.
    public static int Reemplazar(ArgumentosLauncher modo)
    {
        string instalacion = Path.GetDirectoryName(Path.GetDirectoryName(modo.Destino));
        RegistroLauncher.Iniciar(instalacion);
        EsperarCierre(modo.PidEsperado);
        var resultado = ResultadoReemplazo.NoSePudo;
        try
        {
            resultado = ReemplazoLauncher.Reemplazar(modo.Destino, modo.Nuevo, Verificar, IntentosReemplazo, TimeSpan.FromSeconds(1));
        }
        catch (Exception ex)
        {
            RegistroLauncher.Escribir("Reemplazo del launcher fallido: " + ex);
        }
        RegistroLauncher.Escribir("Reemplazo del launcher: " + resultado + ".");
        BorrarSiExiste(modo.Nuevo);
        try
        {
            // Con "--sin-autoactualizacion" para que, si el reemplazo fallo, no se vuelva a intentar en bucle.
            Process.Start(new ProcessStartInfo(modo.Destino, LanzadorSigefa.Unir(ArgumentosLauncher.ParaRelanzar(modo.ArgumentosSigefa)))
            {
                UseShellExecute = false,
                WorkingDirectory = Path.GetDirectoryName(modo.Destino)
            });
        }
        catch (Exception ex)
        {
            RegistroLauncher.Escribir("No pude abrir el launcher despues del reemplazo: " + ex);
            return 1;
        }
        return 0;
    }

    // Borra las copias temporales de reemplazos anteriores (la ultima no se puede borrar mientras corre).
    public static void LimpiarTemporales()
    {
        try
        {
            foreach (string carpeta in Directory.GetDirectories(Path.GetTempPath(), PrefijoTemporal + "*"))
            {
                if (DateTime.UtcNow - Directory.GetCreationTimeUtc(carpeta) > TimeSpan.FromMinutes(10))
                {
                    Directory.Delete(carpeta, true);
                }
            }
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
        {
            // Limpieza de cortesia: si no se puede, queda para la proxima.
        }
    }

    // Baja el zip, comprueba su sha256, saca el exe y comprueba que arranca. Devuelve la ruta del exe nuevo.
    private static string Preparar(ServidorActualizacion servidor, ManifiestoVersion manifiesto, ConfiguracionLauncher config)
    {
        string carpeta = Path.Combine(config.CarpetaInstalacion, "descargas");
        string zip = null;
        try
        {
            zip = servidor.Descargar(manifiesto, carpeta);
            if (!HashArchivo.Coincide(zip, manifiesto.Sha256))
            {
                throw new InstalacionException("El launcher descargado no coincide con el sha256 de su ficha.");
            }
            string nuevo = Path.Combine(carpeta, NombreExeNuevo);
            using (var archivo = ZipFile.OpenRead(zip))
            {
                var entrada = archivo.GetEntry(NombreExe) ?? throw new InstalacionException("El zip del launcher no trae " + NombreExe + ".");
                entrada.ExtractToFile(nuevo, true);
            }
            if (!Verificar(nuevo))
            {
                BorrarSiExiste(nuevo);
                throw new InstalacionException("El launcher nuevo no arranca; se descarta.");
            }
            return nuevo;
        }
        finally
        {
            BorrarSiExiste(zip);
        }
    }

    private static void IniciarReemplazo(string propio, string nuevo, string[] argumentosSigefa)
    {
        string carpetaTemporal = Path.Combine(Path.GetTempPath(), PrefijoTemporal + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(carpetaTemporal);
        string copia = Path.Combine(carpetaTemporal, NombreExe);
        File.Copy(propio, copia, true);
        var argumentos = ArgumentosLauncher.ParaReemplazar(Process.GetCurrentProcess().Id, propio, nuevo, argumentosSigefa);
        Process.Start(new ProcessStartInfo(copia, LanzadorSigefa.Unir(argumentos))
        {
            UseShellExecute = false,
            WorkingDirectory = carpetaTemporal
        });
    }

    // Un launcher "arranca" si, abierto con --verificar-launcher, termina con codigo 0 en pocos segundos.
    private static bool Verificar(string exe)
    {
        using (var proceso = Process.Start(new ProcessStartInfo(exe, ArgumentosLauncher.FlagVerificar)
        {
            UseShellExecute = false,
            CreateNoWindow = true
        }))
        {
            if (!proceso.WaitForExit(EsperaVerificacionMs))
            {
                proceso.Kill();
                return false;
            }
            return proceso.ExitCode == 0;
        }
    }

    private static void EsperarCierre(int pid)
    {
        try
        {
            using (var original = Process.GetProcessById(pid))
            {
                original.WaitForExit(EsperaCierreOriginalMs);
            }
        }
        catch (ArgumentException)
        {
            // Ya habia terminado.
        }
    }

    private static void BorrarSiExiste(string archivo)
    {
        try
        {
            if (archivo != null && File.Exists(archivo))
            {
                File.Delete(archivo);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
