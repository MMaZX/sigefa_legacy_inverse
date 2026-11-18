using System;

namespace SIGEFA.Administradores.Actualizador;

// El manifiesto del servidor no se pudo interpretar. El mensaje es apto para el log.
public sealed class ManifiestoInvalidoException : Exception
{
    public ManifiestoInvalidoException(string mensaje) : base(mensaje)
    {
    }
}
