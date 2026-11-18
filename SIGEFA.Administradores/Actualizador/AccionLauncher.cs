namespace SIGEFA.Administradores.Actualizador;

public enum AccionLauncher
{
    // Abrir la version que ya esta instalada.
    Lanzar,

    // Bajar e instalar la version del servidor y luego abrirla.
    ActualizarYLanzar,

    // No hay nada instalado ni forma de instalarlo: no se puede abrir SIGEFA.
    SinVersion
}
