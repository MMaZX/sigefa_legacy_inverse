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
