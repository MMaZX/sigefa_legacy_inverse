using System.Collections.Generic;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.Db;

public class ConsultaTests
{
    private static Dictionary<string, object> Fila(int id)
    {
        return new Dictionary<string, object>(System.StringComparer.OrdinalIgnoreCase) { { "id", id } };
    }

    [Fact]
    public void Get_SinFilas_DevuelveListaVaciaNuncaNula()
    {
        var consulta = new Consulta(limite => new List<Dictionary<string, object>>());

        var filas = consulta.Get();

        Assert.NotNull(filas);
        Assert.Empty(filas);
    }

    [Fact]
    public void Get_EjecutorDevuelveNulo_DevuelveListaVacia()
    {
        var consulta = new Consulta(limite => null);

        var filas = consulta.Get();

        Assert.NotNull(filas);
        Assert.Empty(filas);
    }

    [Fact]
    public void Get_ConFilas_DevuelveTodasSinLimite()
    {
        int? limiteRecibido = 99;
        var consulta = new Consulta(limite =>
        {
            limiteRecibido = limite;
            return new List<Dictionary<string, object>> { Fila(1), Fila(2) };
        });

        var filas = consulta.Get();

        Assert.Equal(2, filas.Count);
        Assert.Null(limiteRecibido);
    }

    [Fact]
    public void First_SinFilas_DevuelveNulo()
    {
        var consulta = new Consulta(limite => new List<Dictionary<string, object>>());

        Assert.Null(consulta.First());
    }

    [Fact]
    public void First_EjecutorDevuelveNulo_DevuelveNulo()
    {
        var consulta = new Consulta(limite => null);

        Assert.Null(consulta.First());
    }

    [Fact]
    public void First_PideUnaSolaFilaAlEjecutor()
    {
        int? limiteRecibido = null;
        var consulta = new Consulta(limite =>
        {
            limiteRecibido = limite;
            return new List<Dictionary<string, object>> { Fila(1) };
        });

        var fila = consulta.First();

        Assert.Equal(1, limiteRecibido);
        Assert.Equal(1, fila["id"]);
    }

    [Fact]
    public void First_ConVariasFilas_DevuelveLaPrimera()
    {
        var consulta = new Consulta(limite => new List<Dictionary<string, object>> { Fila(10), Fila(20) });

        Assert.Equal(10, consulta.First()["id"]);
    }

    [Fact]
    public void Construir_NoEjecutaLaConsulta_EsPerezosa()
    {
        var ejecuciones = 0;
        var consulta = new Consulta(limite =>
        {
            ejecuciones++;
            return new List<Dictionary<string, object>>();
        });

        Assert.Equal(0, ejecuciones);
        consulta.Get();
        Assert.Equal(1, ejecuciones);
        consulta.First();
        Assert.Equal(2, ejecuciones);
    }
}
