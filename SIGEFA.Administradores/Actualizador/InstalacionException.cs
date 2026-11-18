using System;

namespace SIGEFA.Administradores.Actualizador;

// La version descargada no se pudo instalar. Nunca deja una instalacion a medias: la anterior sigue activa.
public sealed class InstalacionException : Exception
{
    public InstalacionException(string mensaje) : base(mensaje)
    {
    }
}
