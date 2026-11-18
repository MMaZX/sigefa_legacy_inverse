namespace SIGEFA.Administradores.Actualizador;

// Nivel de aviso del boton: decide el color de acento, nunca un parpadeo.
public enum UrgenciaBotonActualizacion
{
    Ninguna,
    Aviso,
    Obligatoria
}

// Que dice y como se ve el boton "Buscar Actualizaciones" segun el resultado de la consulta. Sin WinForms.
public sealed class PresentacionBotonActualizacion
{
    public const string TextoNormal = "Buscar Actualizaciones";

    private PresentacionBotonActualizacion(string texto, UrgenciaBotonActualizacion urgencia)
    {
        Texto = texto;
        Urgencia = urgencia;
    }

    public string Texto { get; }

    // Con una actualizacion pendiente el boton cambia a un color de acento fijo (sin parpadear) hasta que se actualice.
    public UrgenciaBotonActualizacion Urgencia { get; }

    public static PresentacionBotonActualizacion Para(EstadoActualizacion estado, int buildNueva)
    {
        switch (estado)
        {
            case EstadoActualizacion.Obligatoria:
                return new PresentacionBotonActualizacion("¡ACTUALIZACIÓN OBLIGATORIA: build " + buildNueva + "!", UrgenciaBotonActualizacion.Obligatoria);
            case EstadoActualizacion.Disponible:
                return new PresentacionBotonActualizacion("¡Tienes una actualización pendiente a la build " + buildNueva + "!", UrgenciaBotonActualizacion.Aviso);
            default:
                return new PresentacionBotonActualizacion(TextoNormal, UrgenciaBotonActualizacion.Ninguna);
        }
    }
}
