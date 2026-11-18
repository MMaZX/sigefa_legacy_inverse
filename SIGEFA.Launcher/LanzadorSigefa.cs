using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SIGEFA.Administradores.RutasSistema;

namespace SIGEFA.Launcher;

internal static class LanzadorSigefa
{
    // Abre SIGEFA con su propia carpeta como directorio de trabajo y le pasa los argumentos recibidos.
    // carpetaDocumentos queda fuera de versions\: asi los comprobantes sobreviven al borrado de versiones viejas.
    // Si el equipo ya define SIGEFA_RUTA_DOCUMENTOS, se respeta.
    public static void Lanzar(string exe, string[] argumentos, string carpetaDocumentos)
    {
        var inicio = new ProcessStartInfo(exe)
        {
            WorkingDirectory = Path.GetDirectoryName(exe),
            UseShellExecute = false,
            Arguments = string.Join(" ", argumentos.Select(Entrecomillar))
        };
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(RutaDocumentos.VariableEntorno)))
        {
            inicio.EnvironmentVariables[RutaDocumentos.VariableEntorno] = carpetaDocumentos;
        }
        Process.Start(inicio);
    }

    private static string Entrecomillar(string argumento)
    {
        return "\"" + argumento.Replace("\"", "\\\"") + "\"";
    }
}
