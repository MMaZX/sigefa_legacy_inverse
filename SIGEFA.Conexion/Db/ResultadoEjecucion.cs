namespace SIGEFA.Conexion;

/// <summary>
/// Resultado de una sentencia de escritura.
/// </summary>
public sealed class ResultadoEjecucion
{
    public ResultadoEjecucion(long id, int filasAfectadas)
    {
        Id = id;
        FilasAfectadas = filasAfectadas;
    }

    /// <summary>Ultimo id autoincremental generado por la conexion (0 si no hubo).</summary>
    public long Id { get; }

    /// <summary>Filas afectadas segun el servidor.</summary>
    public int FilasAfectadas { get; }

    /// <summary>Verdadero si la sentencia afecto al menos una fila.</summary>
    public bool Ok => FilasAfectadas > 0;
}
