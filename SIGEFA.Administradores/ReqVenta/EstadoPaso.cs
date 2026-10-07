namespace SIGEFA.Administradores.ReqVenta;

// Estado de un paso de una operación larga (anular, aprobar, guardar).
public enum EstadoPaso
{
    // El paso aún no empezó.
    Pendiente,

    // El paso está en curso.
    EnCurso,

    // El paso terminó bien.
    Listo,

    // El paso falló.
    Error,
}
