using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace SIGEFA.Conexion;

/// <summary>
/// Punto de entrada del helper de consultas estilo Query Builder.
/// <code>
/// var filas = Db.Consultar("SELECT * FROM t WHERE estado=@e", new { e = 7 }).Get();
/// var r = Db.Ejecutar("UPDATE t SET x=@x WHERE id=@id", new { x = 1, id = 5 });
/// Db.Transaccion(tx => { tx.Ejecutar("..."); });
/// </code>
/// </summary>
public static class Db
{
    private const string NombreCadenaPorDefecto = "ConnNegocio";

    /// <summary>
    /// Cadena de conexion. Si es null o vacia se resuelve de app.config (ConnNegocio) al usarla.
    /// </summary>
    public static string CadenaConexion { get; set; }

    public static Consulta Consultar(string sql, object parametros = null)
    {
        return new ConsultorMySql(ObtenerCadena()).Consultar(sql, parametros);
    }

    public static ResultadoEjecucion Ejecutar(string sql, object parametros = null)
    {
        return new ConsultorMySql(ObtenerCadena()).Ejecutar(sql, parametros);
    }

    public static void Transaccion(Action<IConsultor> accion)
    {
        if (accion == null)
        {
            throw new ArgumentNullException(nameof(accion));
        }

        Transaccion<int>(tx =>
        {
            accion(tx);
            return 0;
        });
    }

    /// <summary>
    /// Ejecuta la accion en una unica conexion y transaccion. Commit si termina bien;
    /// ante cualquier excepcion hace rollback y relanza la original.
    /// </summary>
    public static T Transaccion<T>(Func<IConsultor, T> accion)
    {
        if (accion == null)
        {
            throw new ArgumentNullException(nameof(accion));
        }

        using (var conexion = new MySqlConnection(ObtenerCadena()))
        {
            conexion.Open();
            using (var transaccion = conexion.BeginTransaction())
            {
                try
                {
                    var resultado = accion(new ConsultorMySql(conexion, transaccion));
                    transaccion.Commit();
                    return resultado;
                }
                catch
                {
                    RevertirSinOcultar(transaccion);
                    throw;
                }
            }
        }
    }

    private static void RevertirSinOcultar(MySqlTransaction transaccion)
    {
        try
        {
            transaccion.Rollback();
        }
        catch
        {
            // Si el rollback falla no se oculta la excepcion original que provoco la reversion.
        }
    }

    private static string ObtenerCadena()
    {
        string cadena;
        if (!string.IsNullOrWhiteSpace(CadenaConexion))
        {
            cadena = CadenaConexion;
        }
        else
        {
            var configurada = ConfigurationManager.ConnectionStrings[NombreCadenaPorDefecto];
            if (configurada == null)
            {
                throw new InvalidOperationException(
                    "No hay cadena de conexion: asigne Db.CadenaConexion o defina '" + NombreCadenaPorDefecto + "' en app.config.");
            }
            cadena = configurada.ConnectionString;
        }

        return ConsultorMySql.NormalizarCadena(cadena);
    }
}
