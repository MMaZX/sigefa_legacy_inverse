using System;
using System.Collections.Generic;

namespace SIGEFA.Conexion;

/// <summary>
/// Consulta de lectura de ejecucion perezosa: corre al llamar Get() o First(), no al construirla.
/// </summary>
public sealed class Consulta
{
    private readonly Func<int?, List<Dictionary<string, object>>> _ejecutor;

    /// <param name="ejecutor">
    /// Recibe un limite de filas (null = todas, 1 = solo la primera) y devuelve las filas leidas.
    /// </param>
    public Consulta(Func<int?, List<Dictionary<string, object>>> ejecutor)
    {
        if (ejecutor == null)
        {
            throw new ArgumentNullException(nameof(ejecutor));
        }
        _ejecutor = ejecutor;
    }

    /// <summary>Todas las filas. Lista vacia (nunca null) si no hay resultados.</summary>
    public List<Dictionary<string, object>> Get()
    {
        var filas = _ejecutor(null);
        return filas ?? new List<Dictionary<string, object>>();
    }

    /// <summary>La primera fila, o null si no hay resultados. Pide solo una fila al origen.</summary>
    public Dictionary<string, object> First()
    {
        var filas = _ejecutor(1);
        if (filas == null || filas.Count == 0)
        {
            return null;
        }
        return filas[0];
    }
}
