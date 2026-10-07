namespace SIGEFA.Administradores.ReqVenta;

// Paso de una operación larga para el diálogo de progreso.
// Objeto inmutable: la clave identifica el paso, el texto va en español claro
// y el detalle amplía lo ocurrido sin propagar nulos.
public sealed class PasoOperacion
{
    // Crea el paso. La clave y el texto nulos se guardan vacíos;
    // el detalle nulo se guarda vacío para no propagar nulos.
    public PasoOperacion(string clave, string texto, EstadoPaso estado, string detalle = null)
    {
        this.Clave = clave ?? string.Empty;
        this.Texto = texto ?? string.Empty;
        this.Estado = estado;
        this.Detalle = detalle ?? string.Empty;
    }

    // Clave estable del paso; las actualizaciones repiten la clave.
    public string Clave { get; }

    // Texto en español claro, sin jerga técnica.
    public string Texto { get; }

    // Estado actual del paso.
    public EstadoPaso Estado { get; }

    // Detalle de lo ocurrido, nunca nulo (cadena vacía).
    public string Detalle { get; }
}
