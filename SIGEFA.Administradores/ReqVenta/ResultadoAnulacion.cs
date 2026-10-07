namespace SIGEFA.Administradores.ReqVenta;

// Resultado de anular un requerimiento de venta pendiente (T2c).
// Clase pequeña sin tuplas: informa si salió bien y el mensaje para el llamador.
public class ResultadoAnulacion
{
    // Crea el resultado. El mensaje nulo se guarda vacío para no propagar nulos.
    public ResultadoAnulacion(bool ok, string mensaje)
    {
        this.Ok = ok;
        this.Mensaje = mensaje ?? string.Empty;
    }

    // Verdadero cuando la anulación se ejecutó completa.
    public bool Ok { get; }

    // Explicación del resultado, nunca nula.
    public string Mensaje { get; }
}
