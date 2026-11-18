using System.Collections.Generic;

namespace SIGEFA.Administradores.Actualizador;

public sealed class ResultadoInstalacion
{
    public ResultadoInstalacion(int build, string carpeta, IReadOnlyList<string> versionesNoEliminadas)
    {
        Build = build;
        Carpeta = carpeta;
        VersionesNoEliminadas = versionesNoEliminadas;
    }

    public int Build { get; }

    public string Carpeta { get; }

    // Versiones viejas que no se pudieron borrar (por ejemplo, otro proceso las tiene abiertas).
    // No es un error: se reintentan en la siguiente actualizacion.
    public IReadOnlyList<string> VersionesNoEliminadas { get; }
}
