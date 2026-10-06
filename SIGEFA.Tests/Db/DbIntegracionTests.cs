using System;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.Helper;

/// <summary>
/// Pruebas de integracion contra MySQL real (variable SIGEFA_TEST_CONN). Se omiten si no existe.
/// Los valores siempre viajan como parametros; solo el nombre de la tabla de andamiaje se concatena.
/// </summary>
public class DbIntegracionTests : BdPruebaBase
{
    private sealed class FalloDePrueba : Exception
    {
        public FalloDePrueba(string mensaje) : base(mensaje)
        {
        }
    }

    [HechoConBd]
    public void Get_ConFilas_DevuelveTodas()
    {
        Insertar("a", 1);
        Insertar("b", 2);

        var filas = Db.Consultar(Sql("SELECT id, nombre FROM {t} ORDER BY id")).Get();

        Assert.Equal(2, filas.Count);
        Assert.Equal("a", filas[0].Valor<string>("nombre"));
        Assert.Equal("b", filas[1].Valor<string>("nombre"));
    }

    [HechoConBd]
    public void Get_SinFilas_DevuelveListaVacia()
    {
        var filas = Db.Consultar(Sql("SELECT id FROM {t} WHERE nombre=@n"), new { n = "no existe" }).Get();

        Assert.NotNull(filas);
        Assert.Empty(filas);
    }

    [HechoConBd]
    public void First_ConFilas_DevuelveLaPrimera()
    {
        Insertar("a", 1);
        Insertar("b", 2);

        var fila = Db.Consultar(Sql("SELECT nombre FROM {t} ORDER BY id")).First();

        Assert.NotNull(fila);
        Assert.Equal("a", fila.Valor<string>("nombre"));
    }

    [HechoConBd]
    public void First_SinFilas_DevuelveNulo()
    {
        var fila = Db.Consultar(Sql("SELECT id FROM {t} WHERE id=@id"), new { id = -1 }).First();

        Assert.Null(fila);
    }

    [HechoConBd]
    public void Ejecutar_Insert_DevuelveIdYUnaFilaAfectada()
    {
        var resultado = Insertar("a", 1);

        Assert.True(resultado.Id > 0);
        Assert.Equal(1, resultado.FilasAfectadas);
        Assert.True(resultado.Ok);
    }

    [HechoConBd]
    public void Ejecutar_Update_DevuelveFilasAfectadas()
    {
        Insertar("a", 1);
        Insertar("a", 2);

        var resultado = Db.Ejecutar(Sql("UPDATE {t} SET cantidad=@c WHERE nombre=@n"), new { c = 10, n = "a" });

        Assert.Equal(2, resultado.FilasAfectadas);
        Assert.True(resultado.Ok);
    }

    [HechoConBd]
    public void Ejecutar_UpdateSinCoincidencias_NoEsOk()
    {
        var resultado = Db.Ejecutar(Sql("UPDATE {t} SET cantidad=@c WHERE id=@id"), new { c = 1, id = -1 });

        Assert.Equal(0, resultado.FilasAfectadas);
        Assert.False(resultado.Ok);
    }

    [HechoConBd]
    public void Transaccion_TerminaBien_HaceCommit()
    {
        Db.Transaccion(tx =>
        {
            tx.Ejecutar(Sql("INSERT INTO {t} (nombre, cantidad) VALUES (@n, @c)"), new { n = "dentro", c = 1 });
        });

        Assert.Equal(1, Contar());
    }

    [HechoConBd]
    public void Transaccion_ConExcepcion_HaceRollbackYRelanzaLaOriginal()
    {
        var ex = Assert.Throws<FalloDePrueba>(() =>
            Db.Transaccion(tx =>
            {
                tx.Ejecutar(Sql("INSERT INTO {t} (nombre, cantidad) VALUES (@n, @c)"), new { n = "dentro", c = 1 });
                throw new FalloDePrueba("falla provocada");
            }));

        Assert.Equal("falla provocada", ex.Message);
        Assert.Equal(0, Contar());
    }

    [HechoConBd]
    public void Transaccion_DentroDeLaTransaccion_VeSusPropiosCambiosSinCommit()
    {
        long vistoDentro = -1;

        Assert.Throws<FalloDePrueba>(() =>
            Db.Transaccion(tx =>
            {
                tx.Ejecutar(Sql("INSERT INTO {t} (nombre) VALUES (@n)"), new { n = "x" });
                vistoDentro = tx.Consultar(Sql("SELECT COUNT(*) AS total FROM {t}")).First().Valor<long>("total");
                throw new FalloDePrueba("revertir");
            }));

        Assert.Equal(1, vistoDentro);
        Assert.Equal(0, Contar());
    }

    [HechoConBd]
    public void TransaccionGenerica_DevuelveElValorDeLaAccion()
    {
        var id = Db.Transaccion(tx =>
        {
            var insercion = tx.Ejecutar(Sql("INSERT INTO {t} (nombre) VALUES (@n)"), new { n = "x" });
            return insercion.Id;
        });

        Assert.True(id > 0);
        Assert.Equal(1, Contar());
    }

    [HechoConBd]
    public void Parametros_ValorConInyeccion_NoDevuelveTodasLasFilas()
    {
        Insertar("a", 1);
        Insertar("b", 2);

        var filas = Db.Consultar(Sql("SELECT id FROM {t} WHERE nombre=@n"), new { n = "' OR 1=1 --" }).Get();

        Assert.Empty(filas);
        Assert.Equal(2, Contar());
    }

    [HechoConBd]
    public void ColumnaNula_SeLeeComoNuloEnElDiccionario()
    {
        Insertar(null, null);

        var fila = Db.Consultar(Sql("SELECT nombre, cantidad FROM {t}")).First();

        Assert.Null(fila["nombre"]);
        Assert.Null(fila["cantidad"]);
        Assert.Equal("sin nombre", fila.Valor("nombre", "sin nombre"));
        Assert.Null(fila.Valor<int?>("cantidad"));
    }

    [HechoConBd]
    public void ColumnaBit_SeLeeComoBoolConValor()
    {
        Db.Ejecutar(Sql("INSERT INTO {t} (nombre, activo) VALUES (@n, @a)"), new { n = "on", a = 1 });
        Db.Ejecutar(Sql("INSERT INTO {t} (nombre, activo) VALUES (@n, @a)"), new { n = "off", a = 0 });

        var filas = Db.Consultar(Sql("SELECT nombre, activo FROM {t} ORDER BY id")).Get();

        // El tipo CLR que entrega el driver para BIT(1) depende de la version de MySql.Data;
        // Valor<bool> admite bool y los enteros, asi que no se fija aqui.
        Assert.True(filas[0].Valor<bool>("activo"));
        Assert.False(filas[1].Valor<bool>("activo"));
    }

    [HechoConBd]
    public void ColumnaBit_ValorPorDefecto_EsVerdadero()
    {
        Insertar("defecto", 1);

        var fila = Db.Consultar(Sql("SELECT activo FROM {t}")).First();

        Assert.True(fila.Valor<bool>("activo"));
    }
}
