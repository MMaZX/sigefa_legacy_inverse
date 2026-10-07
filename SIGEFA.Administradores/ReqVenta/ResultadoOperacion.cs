namespace SIGEFA.Administradores.ReqVenta;

// Resultado genérico de una operación larga para el diálogo de progreso.
// Clase pequeña sin tuplas: informa si salió bien y el mensaje para el llamador.
public sealed class ResultadoOperacion
{
    // Crea el resultado. El mensaje nulo se guarda vacío para no propagar nulos.
    public ResultadoOperacion(bool ok, string mensaje)
    {
        this.Ok = ok;
        this.Mensaje = mensaje ?? string.Empty;
    }

    // Verdadero cuando la operación se ejecutó completa.
    public bool Ok { get; }

    // Explicación del resultado, nunca nula.
    public string Mensaje { get; }

    // Convierte el resultado de una anulación. El nulo se informa como fallo.
    public static ResultadoOperacion De(ResultadoAnulacion resultado)
    {
        if (resultado == null)
        {
            return new ResultadoOperacion(false, "No se recibió el resultado de la anulación.");
        }

        return new ResultadoOperacion(resultado.Ok, resultado.Mensaje);
    }
}
