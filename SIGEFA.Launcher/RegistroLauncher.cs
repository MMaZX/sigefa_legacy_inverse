using System;
using System.IO;

namespace SIGEFA.Launcher;

// Registro simple en <instalacion>\launcher.log para saber por que una PC no se actualizo.
internal static class RegistroLauncher
{
    private const long TamanoMaximo = 200000;

    private static string archivo;

    public static void Iniciar(string carpetaInstalacion)
    {
        archivo = Path.Combine(carpetaInstalacion, "launcher.log");
    }

    // Escribir el registro nunca debe impedir abrir SIGEFA: sus propios fallos de disco se ignoran.
    public static void Escribir(string mensaje)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(archivo));
            if (File.Exists(archivo) && new FileInfo(archivo).Length > TamanoMaximo)
            {
                File.Delete(archivo);
            }
            File.AppendAllText(archivo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + mensaje + Environment.NewLine);
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
