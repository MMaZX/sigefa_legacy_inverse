using System;
using System.Collections.Generic;
using System.Globalization;

namespace SIGEFA.Conexion;

/// <summary>
/// Lectura tipada de las filas devueltas por <see cref="Consulta"/>.
/// </summary>
public static class FilaExtensiones
{
    /// <summary>
    /// Lee una columna convertida a T. NULL de BD (null o DBNull) devuelve <paramref name="defecto"/>.
    /// </summary>
    /// <exception cref="KeyNotFoundException">La columna no existe en la fila.</exception>
    /// <exception cref="InvalidCastException">El valor no se puede convertir a T.</exception>
    public static T Valor<T>(this IDictionary<string, object> fila, string clave, T defecto = default(T))
    {
        if (fila == null)
        {
            throw new ArgumentNullException(nameof(fila));
        }

        object valor;
        if (!fila.TryGetValue(clave, out valor))
        {
            throw new KeyNotFoundException(
                "La fila no tiene la columna '" + clave + "'. Columnas disponibles: " + string.Join(", ", fila.Keys) + ".");
        }

        if (valor == null || valor is DBNull)
        {
            return defecto;
        }

        var destino = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        return (T)Convertir(valor, destino, clave);
    }

    private static object Convertir(object valor, Type destino, string clave)
    {
        if (destino.IsInstanceOfType(valor))
        {
            return valor;
        }

        try
        {
            if (destino == typeof(bool))
            {
                return ABool(valor);
            }
            return Convert.ChangeType(valor, destino, CultureInfo.InvariantCulture);
        }
        catch (Exception ex) when (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
        {
            throw new InvalidCastException(
                "No se pudo convertir la columna '" + clave + "' de " + valor.GetType().Name + " a " + destino.Name + ".", ex);
        }
    }

    // Las columnas bit(1) pueden llegar como bool, ulong u otro entero segun el driver.
    private static object ABool(object valor)
    {
        switch (valor)
        {
            case byte b: return b != 0;
            case sbyte sb: return sb != 0;
            case short s: return s != 0;
            case ushort us: return us != 0;
            case int i: return i != 0;
            case uint ui: return ui != 0;
            case long l: return l != 0;
            case ulong ul: return ul != 0;
            default: return Convert.ChangeType(valor, typeof(bool), CultureInfo.InvariantCulture);
        }
    }
}
