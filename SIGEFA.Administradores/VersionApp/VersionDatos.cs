namespace SIGEFA.Administradores.VersionApp;

// Datos que muestra el dialogo de version. Los arma la capa de formularios.
public class VersionDatos
{
    public string Version { get; set; }
    public string Commit { get; set; }
    public string Asunto { get; set; }
    public bool ConCambiosLocales { get; set; }
    public string FechaCompilacion { get; set; }
    public string BaseDatos { get; set; }
    public string Servidor { get; set; }
    public string Equipo { get; set; }
    public string RutaEjecutable { get; set; }
}
