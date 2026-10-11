using System;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Seam RED: conserva la obtención directa; todavía no protege fallos de metadata.
public static class MetadataBitacora
{
    public static string ObtenerEquipo(Func<string> obtenerEquipo)
    {
        return obtenerEquipo();
    }
}
