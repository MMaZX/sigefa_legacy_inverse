using System.Collections.Generic;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Respaldo en archivo cuando la BD de la bitácora falla.
public interface IRespaldoArchivo
{
    // Escribe el intento completo en un solo archivo y devuelve su ruta, o null
    // si no pudo escribir. Nunca lanza. numeroIntento es el que asignó la BD,
    // o null si la BD no llegó a asignarlo.
    string Escribir(
        IntentoBitacora intento, int? numeroIntento, ResultadoBitacora resultado,
        IReadOnlyList<EventoBitacora> eventos);
}
