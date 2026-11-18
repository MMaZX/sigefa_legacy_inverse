namespace SIGEFA.Administradores.Actualizador;

// Decide que debe hacer el cliente al comparar su build con el manifiesto. Sin E/S.
public static class DecisorActualizacion
{
    // buildLocal <= 0 significa "sin numero de build" (compilado a mano o sin git):
    // esas compilaciones de desarrollo nunca se bloquean ni se avisan.
    public static EstadoActualizacion Evaluar(int buildLocal, ManifiestoVersion manifiesto)
    {
        if (buildLocal <= 0 || manifiesto == null)
        {
            return EstadoActualizacion.AlDia;
        }
        if (buildLocal < manifiesto.MinBuild)
        {
            return EstadoActualizacion.Obligatoria;
        }
        return buildLocal < manifiesto.Build ? EstadoActualizacion.Disponible : EstadoActualizacion.AlDia;
    }
}
