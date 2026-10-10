using System.Collections.Generic;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Contrato de persistencia de la bitácora del cierre. La implementación MySQL
// (T3) usa una conexión aparte en autocommit, nunca la transacción del cierre.
// Ambos métodos pueden lanzar: BitacoraCierre los captura y usa el respaldo.
public interface IBitacoraRepositorio
{
    // Crea la cabecera del intento (estado EN_CURSO) y devuelve el número de
    // intento asignado (MAX+1 con reintento ante el error 1062). logId es el id de la fila.
    int CrearIntento(IntentoBitacora intento, out long logId);

    // Hace el UPDATE de la cabecera y un único INSERT múltiple con todos los eventos.
    void Finalizar(long logId, ResultadoBitacora resultado, IReadOnlyList<EventoBitacora> eventos);
}
