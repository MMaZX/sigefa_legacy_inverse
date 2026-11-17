using System;

namespace SIGEFA.Administradores.ConsultaDocumento;

// SUNAT no respondio o respondio algo ilegible (distinto de "documento no encontrado").
public class ConsultaDocumentoException : Exception
{
    public ConsultaDocumentoException(string message, Exception inner = null) : base(message, inner)
    {
    }
}
