using System;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Seam RED: conserva la obtención directa; todavía no protege fallos de metadata.
public static class MetadataBitacora
{
    public const string EquipoDesconocido = "desconocido";

    public static string ObtenerEquipo(Func<string> obtenerEquipo)
    {
        if (obtenerEquipo == null)
        {
            return EquipoDesconocido;
        }

        try
        {
            string nombre = obtenerEquipo();
            return string.IsNullOrWhiteSpace(nombre) ? EquipoDesconocido : nombre;
        }
        catch (Exception)
        {
            return EquipoDesconocido;
        }
    }
}
