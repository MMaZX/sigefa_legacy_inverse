using System;
using SIGEFA.Conexion;

namespace SIGEFA.Tests.Helper;

/// <summary>
/// Base de las pruebas de integracion. Crea una tabla de andamiaje con nombre unico por instancia
/// y la elimina en Dispose. El DDL va fuera de transacciones porque provoca commit implicito.
/// </summary>
public abstract class BdPruebaBase : IDisposable
{
    private readonly ConsultorMySql _administrador;

    protected BdPruebaBase()
    {
        var cadena = HechoConBdAttribute.CadenaConexion();
        Db.CadenaConexion = cadena;
        _administrador = new ConsultorMySql(cadena);
        // El nombre es un identificador generado (prefijo fijo + guid), nunca entrada de usuario.
        // Los identificadores no admiten parametros, por eso es la unica concatenacion de SQL.
        Tabla = "zz_test_db_" + Guid.NewGuid().ToString("N");
        _administrador.Ejecutar(
            "CREATE TABLE " + Tabla + " (" +
            "id INT NOT NULL AUTO_INCREMENT PRIMARY KEY, " +
            "nombre VARCHAR(50) NULL, " +
            "cantidad INT NULL, " +
            "activo BIT(1) NOT NULL DEFAULT 1)");
    }

    protected string Tabla { get; }

    /// <summary>Reemplaza el marcador {t} por el nombre de la tabla de andamiaje (identificador generado).</summary>
    protected string Sql(string plantilla)
    {
        return plantilla.Replace("{t}", Tabla);
    }

    protected ResultadoEjecucion Insertar(string nombre, int? cantidad)
    {
        return Db.Ejecutar(
            Sql("INSERT INTO {t} (nombre, cantidad) VALUES (@nombre, @cantidad)"),
            new { nombre, cantidad });
    }

    protected long Contar()
    {
        var fila = Db.Consultar(Sql("SELECT COUNT(*) AS total FROM {t}")).First();
        return fila.Valor<long>("total");
    }

    public void Dispose()
    {
        _administrador.Ejecutar("DROP TABLE IF EXISTS " + Tabla);
    }
}
