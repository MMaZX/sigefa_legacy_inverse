namespace SIGEFA.Administradores.Actualizador;

// Que dice y como se comporta el boton "Buscar Actualizaciones" segun el resultado de la consulta. Sin WinForms.
public sealed class PresentacionBotonActualizacion
{
    public const string TextoNormal = "Buscar Actualizaciones";

    private PresentacionBotonActualizacion(string texto, bool parpadea)
    {
        Texto = texto;
        Parpadea = parpadea;
    }

    public string Texto { get; }

    // Con una actualizacion pendiente el boton parpadea en colores de alerta hasta que se actualice.
    public bool Parpadea { get; }

    public static PresentacionBotonActualizacion Para(EstadoActualizacion estado, int buildNueva)
    {
        switch (estado)
        {
            case EstadoActualizacion.Obligatoria:
                return new PresentacionBotonActualizacion("¡ACTUALIZACIÓN OBLIGATORIA: build " + buildNueva + "!", true);
            case EstadoActualizacion.Disponible:
                return new PresentacionBotonActualizacion("¡Tienes una actualización pendiente a la build " + buildNueva + "!", true);
            default:
                return new PresentacionBotonActualizacion(TextoNormal, false);
        }
    }
}
