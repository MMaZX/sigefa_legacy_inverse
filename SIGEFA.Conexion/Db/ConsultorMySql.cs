using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SIGEFA.Conexion;

/// <summary>
/// Implementacion de <see cref="IConsultor"/> sobre MySQL. Dos modos:
/// con cadena de conexion (abre y cierra una conexion por llamada) o con una conexion y una
/// transaccion ya abiertas (las reutiliza y no las cierra). No captura excepciones ni muestra dialogos.
/// </summary>
public sealed class ConsultorMySql : IConsultor
{
    private readonly string _cadenaConexion;
    private readonly MySqlConnection _conexion;
    private readonly MySqlTransaction _transaccion;

    public ConsultorMySql(string cadenaConexion)
    {
        if (string.IsNullOrWhiteSpace(cadenaConexion))
        {
            throw new ArgumentException("La cadena de conexion es obligatoria.", nameof(cadenaConexion));
        }
        _cadenaConexion = NormalizarCadena(cadenaConexion);
    }

    public ConsultorMySql(MySqlConnection conexion, MySqlTransaction transaccion)
    {
        if (conexion == null)
        {
            throw new ArgumentNullException(nameof(conexion));
        }
        if (transaccion == null)
        {
            throw new ArgumentNullException(nameof(transaccion));
        }
        _conexion = conexion;
        _transaccion = transaccion;
    }

    public Consulta Consultar(string sql, object parametros = null)
    {
        return new Consulta(limite => EnConexion(cx => Leer(cx, sql, parametros, limite)));
    }

    public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
    {
        return EnConexion(cx => Escribir(cx, sql, parametros));
    }

    private T EnConexion<T>(Func<MySqlConnection, T> accion)
    {
        if (_conexion != null)
        {
            return accion(_conexion);
        }

        using (var conexion = new MySqlConnection(_cadenaConexion))
        {
            conexion.Open();
            return accion(conexion);
        }
    }

    // Punto 2 T2d: sin ampliar IConsultor (fuera de archivos permitidos) no caben parámetros OUT;
    // se activa Allow User Variables aquí, sin tocar app.config, para los CALL con @newid.
    internal static string NormalizarCadena(string cadena)
    {
        try
        {
            var constructor = new MySqlConnectionStringBuilder(cadena);
            if (!constructor.AllowUserVariables)
            {
                constructor.AllowUserVariables = true;
            }

            return constructor.ConnectionString;
        }
        catch
        {
            if (cadena.IndexOf("Allow User Variables", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return cadena;
            }

            if (cadena.IndexOf("AllowUserVariables", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return cadena;
            }

            return cadena.TrimEnd(' ', ';') + ";Allow User Variables=true;";
        }
    }

    private MySqlCommand CrearComando(MySqlConnection conexion, string sql, object parametros)
    {
        var comando = new MySqlCommand(sql, conexion);
        // Con MySql.Data un comando sin transaccion corre fuera de ella en silencio: se asigna siempre.
        comando.Transaction = _transaccion;
        foreach (var parametro in ParametrosSql.Desde(parametros))
        {
            comando.Parameters.Add(parametro);
        }
        return comando;
    }

    private List<Dictionary<string, object>> Leer(MySqlConnection conexion, string sql, object parametros, int? limite)
    {
        var filas = new List<Dictionary<string, object>>();
        using (var comando = CrearComando(conexion, sql, parametros))
        using (var lector = comando.ExecuteReader())
        {
            while (lector.Read())
            {
                filas.Add(LeerFila(lector));
                if (limite.HasValue && filas.Count >= limite.Value)
                {
                    break;
                }
            }
        }
        return filas;
    }

    private static Dictionary<string, object> LeerFila(MySqlDataReader lector)
    {
        var fila = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < lector.FieldCount; i++)
        {
            fila[lector.GetName(i)] = lector.IsDBNull(i) ? null : lector.GetValue(i);
        }
        return fila;
    }

    private ResultadoEjecucion Escribir(MySqlConnection conexion, string sql, object parametros)
    {
        using (var comando = CrearComando(conexion, sql, parametros))
        {
            var filasAfectadas = comando.ExecuteNonQuery();
            return new ResultadoEjecucion(comando.LastInsertedId, filasAfectadas);
        }
    }
}
