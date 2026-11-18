using System;
using System.IO;

namespace SIGEFA.Administradores.Actualizador;

// Donde estan el servidor de versiones y el launcher, visto desde un SIGEFA instalado por el launcher.
// Si SIGEFA se abre directo (sin launcher, o en desarrollo) no hay configuracion y las funciones de
// actualizacion simplemente se apagan: no hay a quien pedirle que actualice.
public sealed class ConfiguracionActualizacion
{
    // Opcionales: si el launcher las define tienen prioridad sobre la convencion de carpetas.
    public const string VariableLauncher = "SIGEFA_LAUNCHER_EXE";
    public const string VariableServidor = "SIGEFA_RUTA_ACTUALIZACION";

    private ConfiguracionActualizacion(string rutaServidor, string exeLauncher)
    {
        RutaServidor = rutaServidor;
        ExeLauncher = exeLauncher;
    }

    public string RutaServidor { get; }

    public string ExeLauncher { get; }

    // Devuelve null si no hay launcher o no se sabe cual es el servidor. Todo lo externo entra por parametro
    // para poder probarlo sin tocar el disco ni el entorno reales.
    public static ConfiguracionActualizacion Detectar(
        string directorioBase,
        Func<string, string> entorno,
        Func<string, bool> existeArchivo,
        Func<string, string> leerRutaServidor)
    {
        string exe = Valor(entorno(VariableLauncher)) ?? PorConvencion(directorioBase);
        if (exe == null || !existeArchivo(exe))
        {
            return null;
        }
        string ruta = Valor(entorno(VariableServidor)) ?? Valor(leerRutaServidor(exe + ".config"));
        return ruta == null ? null : new ConfiguracionActualizacion(ruta, exe);
    }

    // El launcher instala SIGEFA en <raiz>\versions\<build>\ y el propio launcher queda en <raiz>\launcher\
    // (es donde lo deja instalar-cliente.ps1).
    private static string PorConvencion(string directorioBase)
    {
        if (string.IsNullOrWhiteSpace(directorioBase))
        {
            return null;
        }
        string carpetaVersion = directorioBase.TrimEnd('\\', '/');
        string versiones = Path.GetDirectoryName(carpetaVersion);
        if (versiones == null || !string.Equals(Path.GetFileName(versiones), "versions", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        string raiz = Path.GetDirectoryName(versiones);
        return raiz == null ? null : Path.Combine(raiz, "launcher", "SIGEFA.Launcher.exe");
    }

    private static string Valor(string texto)
    {
        return string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
    }
}
