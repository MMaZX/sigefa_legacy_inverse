using System;
using System.Collections.Generic;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace SIGEFA.Conexion;

/// <summary>
/// Convierte un objeto anonimo o un diccionario en parametros MySQL. Logica pura, sin conexion.
/// </summary>
public static class ParametrosSql
{
    public static List<MySqlParameter> Desde(object parametros)
    {
        var resultado = new List<MySqlParameter>();
        if (parametros == null)
        {
            return resultado;
        }

        var diccionario = parametros as IDictionary<string, object>;
        if (diccionario != null)
        {
            foreach (var par in diccionario)
            {
                resultado.Add(Crear(par.Key, par.Value));
            }
            return resultado;
        }

        var propiedades = parametros.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var propiedad in propiedades)
        {
            if (!propiedad.CanRead || propiedad.GetIndexParameters().Length > 0)
            {
                continue;
            }
            resultado.Add(Crear(propiedad.Name, propiedad.GetValue(parametros, null)));
        }
        return resultado;
    }

    private static MySqlParameter Crear(string nombre, object valor)
    {
        var nombreFinal = nombre.StartsWith("@", StringComparison.Ordinal) ? nombre : "@" + nombre;
        return new MySqlParameter(nombreFinal, valor ?? DBNull.Value);
    }
}
