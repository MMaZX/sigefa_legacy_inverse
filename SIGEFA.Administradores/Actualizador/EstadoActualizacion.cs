namespace SIGEFA.Administradores.Actualizador;

public enum EstadoActualizacion
{
    // El cliente ya tiene la ultima version (o no se puede comparar).
    AlDia,

    // Hay una version nueva: se avisa, pero se puede seguir trabajando.
    Disponible,

    // La version del cliente es menor que el minimo: no puede iniciar sesion.
    Obligatoria
}
