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

    // Texto del dialogo "Version del sistema" del home.
    public static string Detalle(VersionDatos datos)
    {
        string commit = string.IsNullOrWhiteSpace(datos.Commit) ? Desconocido : datos.Commit.Trim();
        if (!string.IsNullOrWhiteSpace(datos.Asunto))
        {
            commit += " - " + datos.Asunto.Trim();
        }
        return "Último commit: " + commit + "\n"
            + "Fecha y hora del build: " + ValorODesconocido(datos.FechaCompilacion) + "\n"
            + "Versión del ensamblado: " + RecortarVersion(datos.Version) + "\n"
            + "Cambios sin commitear: " + (datos.ConCambiosLocales ? "Sí" : "No") + "\n\n"
            + "Base de datos: " + ValorODesconocido(datos.BaseDatos) + "\n"
            + "Servidor: " + ValorODesconocido(datos.Servidor) + "\n"
            + "Equipo: " + ValorODesconocido(datos.Equipo) + "\n"
            + "Ejecutable: " + ValorODesconocido(datos.RutaEjecutable);
    }

    private static string ValorODesconocido(string valor)
    {
        return string.IsNullOrWhiteSpace(valor) ? "desconocido" : valor.Trim();
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
