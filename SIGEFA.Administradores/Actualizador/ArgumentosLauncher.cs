using System;
using System.Globalization;
using System.Linq;

namespace SIGEFA.Administradores.Actualizador;

public enum ModoLauncher
{
    // Abre SIGEFA (y antes se actualiza a si mismo y a SIGEFA si hay version nueva).
    Normal,

    // Solo comprueba que este launcher arranca: sale con codigo 0 sin hacer nada mas.
    Verificar,

    // Sale con su numero de build como codigo de salida: asi publicar-launcher.ps1 lee el build del exe que sube.
    MostrarBuild,

    // Copia temporal del launcher: espera que el original se cierre, lo reemplaza y lo vuelve a abrir.
    Reemplazar
}

// Lo que el launcher entiende de su linea de comandos. Sin E/S.
// Normal:     [--sin-autoactualizacion] <argumentos de SIGEFA...>
// Verificar:  --verificar-launcher
// MostrarBuild: --build-launcher
// Reemplazar: --reemplazar-launcher <pid> <destino> <nuevo> [-- <argumentos de SIGEFA...>]
public sealed class ArgumentosLauncher
{
    public const string FlagVerificar = "--verificar-launcher";
    public const string FlagBuild = "--build-launcher";
    public const string FlagReemplazar = "--reemplazar-launcher";
    public const string FlagSinAutoactualizacion = "--sin-autoactualizacion";
    public const string Separador = "--";

    private ArgumentosLauncher(ModoLauncher modo, string[] argumentosSigefa)
    {
        Modo = modo;
        ArgumentosSigefa = argumentosSigefa;
    }

    public ModoLauncher Modo { get; private set; }

    // Proceso del launcher original, que hay que esperar antes de reemplazarlo (solo en modo Reemplazar).
    public int PidEsperado { get; private set; }

    // Ruta del launcher instalado que se va a reemplazar (solo en modo Reemplazar).
    public string Destino { get; private set; }

    // Ruta del launcher nuevo ya descargado y verificado (solo en modo Reemplazar).
    public string Nuevo { get; private set; }

    // Evita que un launcher recien relanzado vuelva a intentar actualizarse en bucle.
    public bool OmitirAutoactualizacion { get; private set; }

    // Lo que hay que pasarle a SIGEFA tal cual lo recibio el launcher.
    public string[] ArgumentosSigefa { get; private set; }

    public static ArgumentosLauncher Interpretar(string[] args)
    {
        args = args ?? new string[0];
        if (args.Length == 1 && EsFlag(args[0], FlagVerificar))
        {
            return new ArgumentosLauncher(ModoLauncher.Verificar, new string[0]);
        }
        if (args.Length == 1 && EsFlag(args[0], FlagBuild))
        {
            return new ArgumentosLauncher(ModoLauncher.MostrarBuild, new string[0]);
        }
        if (EsReemplazoValido(args, out int pid))
        {
            bool hayArgumentosSigefa = args.Length > 4 && args[4] == Separador;
            return new ArgumentosLauncher(ModoLauncher.Reemplazar, hayArgumentosSigefa ? args.Skip(5).ToArray() : new string[0])
            {
                PidEsperado = pid,
                Destino = args[2],
                Nuevo = args[3]
            };
        }
        if (args.Length > 0 && EsFlag(args[0], FlagSinAutoactualizacion))
        {
            return new ArgumentosLauncher(ModoLauncher.Normal, args.Skip(1).ToArray()) { OmitirAutoactualizacion = true };
        }
        // Cualquier otra cosa (incluido un reemplazo mal formado) se trata como un arranque normal: nunca bloquea abrir SIGEFA.
        return new ArgumentosLauncher(ModoLauncher.Normal, args);
    }

    // Argumentos con los que el launcher original lanza la copia temporal que lo va a reemplazar.
    public static string[] ParaReemplazar(int pid, string destino, string nuevo, string[] argumentosSigefa)
    {
        var lista = new System.Collections.Generic.List<string>
        {
            FlagReemplazar,
            pid.ToString(CultureInfo.InvariantCulture),
            destino,
            nuevo,
            Separador
        };
        lista.AddRange(argumentosSigefa ?? new string[0]);
        return lista.ToArray();
    }

    // Argumentos con los que la copia temporal relanza el launcher ya reemplazado.
    public static string[] ParaRelanzar(string[] argumentosSigefa)
    {
        return new[] { FlagSinAutoactualizacion }.Concat(argumentosSigefa ?? new string[0]).ToArray();
    }

    private static bool EsReemplazoValido(string[] args, out int pid)
    {
        pid = 0;
        return args.Length >= 4
            && EsFlag(args[0], FlagReemplazar)
            && int.TryParse(args[1], NumberStyles.None, CultureInfo.InvariantCulture, out pid)
            && pid > 0
            && args[2].Length > 0
            && args[3].Length > 0;
    }

    private static bool EsFlag(string argumento, string flag)
    {
        return string.Equals(argumento, flag, StringComparison.OrdinalIgnoreCase);
    }
}
