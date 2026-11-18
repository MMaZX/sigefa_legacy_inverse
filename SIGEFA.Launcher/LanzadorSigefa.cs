using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SIGEFA.Launcher;

internal static class LanzadorSigefa
{
    // Abre SIGEFA con su propia carpeta como directorio de trabajo y le pasa los argumentos recibidos.
    public static void Lanzar(string exe, string[] argumentos)
    {
        var inicio = new ProcessStartInfo(exe)
        {
            WorkingDirectory = Path.GetDirectoryName(exe),
            UseShellExecute = false,
            Arguments = string.Join(" ", argumentos.Select(Entrecomillar))
        };
        Process.Start(inicio);
    }

    private static string Entrecomillar(string argumento)
    {
        return "\"" + argumento.Replace("\"", "\\\"") + "\"";
    }
}
