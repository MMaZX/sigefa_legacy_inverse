namespace SIGEFA.Conexion;

/// <summary>
/// Contrato comun para ejecutar SQL parametrizado, ya sea sobre conexiones por llamada
/// (<see cref="Db"/>) o dentro de una transaccion (<see cref="Db.Transaccion(System.Action{IConsultor})"/>).
/// </summary>
public interface IConsultor
{
    /// <summary>
    /// Prepara una consulta de lectura. No se ejecuta hasta llamar a Get() o First().
    /// </summary>
    /// <param name="sql">SQL con parametros nombrados (@nombre).</param>
    /// <param name="parametros">Objeto anonimo o IDictionary&lt;string, object&gt; con los valores.</param>
    Consulta Consultar(string sql, object parametros = null);

    /// <summary>
    /// Ejecuta una sentencia de escritura (INSERT, UPDATE, DELETE) de inmediato.
    /// </summary>
    ResultadoEjecucion Ejecutar(string sql, object parametros = null);
}
