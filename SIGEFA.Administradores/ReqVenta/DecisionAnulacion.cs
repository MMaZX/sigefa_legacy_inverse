namespace SIGEFA.Administradores.ReqVenta;

// Decisión de anulación que devuelve ReqVentaReglas.Evaluar.
// DTO inmutable: informa la acción, si está permitida y el motivo.
public class DecisionAnulacion
{
    // Crea la decisión. El motivo nulo se guarda vacío para no propagar nulos.
    public DecisionAnulacion(AccionAnulacion accion, bool permitido, string motivo)
    {
        this.Accion = accion;
        this.Permitido = permitido;
        this.Motivo = motivo ?? string.Empty;
    }

    // Acción que corresponde ejecutar (Ninguna si se deniega).
    public AccionAnulacion Accion { get; }

    // Verdadero cuando la anulación está permitida.
    public bool Permitido { get; }

    // Explicación de la decisión, nunca nula.
    public string Motivo { get; }
}
