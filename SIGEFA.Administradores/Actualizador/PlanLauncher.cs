namespace SIGEFA.Administradores.Actualizador;

// Que hace el launcher al arrancar. Sin E/S: recibe lo ya leido.
public static class PlanLauncher
{
    // buildInstalado: version activa en esta PC (0 = ninguna).
    // manifiesto: lo publicado en el servidor, o null si no se pudo leer (servidor caido, share inaccesible).
    public static AccionLauncher Decidir(int buildInstalado, ManifiestoVersion manifiesto)
    {
        bool hayInstalada = buildInstalado > 0;
        if (manifiesto == null)
        {
            // Servidor inaccesible: si ya hay una version se abre igual, para no frenar las ventas.
            return hayInstalada ? AccionLauncher.Lanzar : AccionLauncher.SinVersion;
        }
        // Nunca se baja de version: si el servidor publica algo mas viejo, se conserva la instalada.
        bool servidorTieneMasNueva = !hayInstalada || buildInstalado < manifiesto.Build;
        return servidorTieneMasNueva ? AccionLauncher.ActualizarYLanzar : AccionLauncher.Lanzar;
    }
}
