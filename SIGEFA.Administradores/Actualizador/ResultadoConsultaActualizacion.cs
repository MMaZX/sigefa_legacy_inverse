namespace SIGEFA.Administradores.Actualizador;

// Lo que se supo al preguntarle al servidor por la version publicada.
public sealed class ResultadoConsultaActualizacion
{
    public ResultadoConsultaActualizacion(EstadoActualizacion estado, ManifiestoVersion manifiesto, int buildLocal, string motivo)
    {
        Estado = estado;
        Manifiesto = manifiesto;
        BuildLocal = buildLocal;
        Motivo = motivo;
    }

    public EstadoActualizacion Estado { get; }

    // null si el servidor no respondio o la ficha era invalida.
    public ManifiestoVersion Manifiesto { get; }

    public int BuildLocal { get; }

    // Por que no se pudo consultar (null si se pudo).
    public string Motivo { get; }

    public bool ServidorRespondio
    {
        get { return Manifiesto != null; }
    }

    // El servidor exige un build mayor al de este SIGEFA: no puede iniciar sesion hasta actualizar.
    public bool BloqueaInicioDeSesion
    {
        get { return Estado == EstadoActualizacion.Obligatoria; }
    }

    // No se pudo consultar: se trata como "al dia" para no molestar ni bloquear a nadie.
    public static ResultadoConsultaActualizacion SinConsulta(int buildLocal, string motivo)
    {
        return new ResultadoConsultaActualizacion(EstadoActualizacion.AlDia, null, buildLocal, motivo);
    }
}
