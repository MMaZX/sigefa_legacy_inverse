namespace SIGEFA.Administradores.VersionApp;

// Arma el texto de version que se muestra en el login. La version numerica (AssemblyVersion)
// esta fija, por eso lo que identifica de verdad el build es el commit y la fecha de compilacion.
// Sin base de datos ni WinForms.
public static class VersionTexto
{
    public const string Desconocido = "sin-git";

    public static string Formatear(string version, string commit, string rama, bool conCambiosLocales, string fechaCompilacion)
    {
        string numero = RecortarVersion(version);
        string origen = string.IsNullOrWhiteSpace(commit) ? Desconocido : commit.Trim();
        if (!string.IsNullOrWhiteSpace(rama))
        {
            origen = rama.Trim() + "@" + origen;
        }
        if (conCambiosLocales)
        {
            origen += "+cambios";
        }
        string texto = "Versión " + numero + " · " + origen;
        return string.IsNullOrWhiteSpace(fechaCompilacion) ? texto : texto + " · " + fechaCompilacion.Trim();
    }

    // Texto del dialogo de version del home: explica cada dato del build.
    public static string Detalle(string version, string commit, string rama, bool conCambiosLocales, string fechaCompilacion)
    {
        string fecha = string.IsNullOrWhiteSpace(fechaCompilacion) ? "desconocida" : fechaCompilacion.Trim();
        string codigo = string.IsNullOrWhiteSpace(commit) ? Desconocido : commit.Trim();
        string nombreRama = string.IsNullOrWhiteSpace(rama) ? Desconocido : rama.Trim();
        return "Fecha y hora del build: " + fecha + "\n"
            + "Commit: " + codigo + "\n"
            + "Rama: " + nombreRama + "\n"
            + "Cambios sin commitear: " + (conCambiosLocales ? "Sí" : "No") + "\n"
            + "Versión del ensamblado: " + RecortarVersion(version) + "\n\n"
            + "La fecha y hora indican cuándo se compiló este ejecutable. "
            + "El commit identifica exactamente el código con el que se compiló. "
            + "Si hay cambios sin commitear, el ejecutable incluye código que no está en ese commit.";
    }

    // "1.0.0.0" -> "1.0.0": el cuarto componente nunca se usa.
    private static string RecortarVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            return "?";
        }
        string[] partes = version.Trim().Split('.');
        return partes.Length == 4 ? partes[0] + "." + partes[1] + "." + partes[2] : version.Trim();
    }
}
