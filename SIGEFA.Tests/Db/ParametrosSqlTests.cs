using System;
using System.Collections.Generic;
using System.Linq;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.Db;

public class ParametrosSqlTests
{
    [Fact]
    public void Desde_ObjetoAnonimo_CreaUnParametroPorPropiedadConArroba()
    {
        var parametros = ParametrosSql.Desde(new { estado = 7, nombre = "abc" });

        Assert.Equal(2, parametros.Count);
        var estado = parametros.Single(p => p.ParameterName == "@estado");
        var nombre = parametros.Single(p => p.ParameterName == "@nombre");
        Assert.Equal(7, estado.Value);
        Assert.Equal("abc", nombre.Value);
    }

    [Fact]
    public void Desde_Diccionario_AceptaNombreConArroba()
    {
        var datos = new Dictionary<string, object> { { "@id", 5 } };

        var parametros = ParametrosSql.Desde(datos);

        var unico = Assert.Single(parametros);
        Assert.Equal("@id", unico.ParameterName);
        Assert.Equal(5, unico.Value);
    }

    [Fact]
    public void Desde_Diccionario_AceptaNombreSinArrobaYLoAgrega()
    {
        var datos = new Dictionary<string, object> { { "id", 5 } };

        var parametros = ParametrosSql.Desde(datos);

        var unico = Assert.Single(parametros);
        Assert.Equal("@id", unico.ParameterName);
    }

    [Fact]
    public void Desde_ValorNulo_UsaDBNull()
    {
        var parametros = ParametrosSql.Desde(new { nombre = (string)null });

        var unico = Assert.Single(parametros);
        Assert.Same(DBNull.Value, unico.Value);
    }

    [Fact]
    public void Desde_ObjetoNulo_DevuelveListaVacia()
    {
        var parametros = ParametrosSql.Desde(null);

        Assert.NotNull(parametros);
        Assert.Empty(parametros);
    }

    [Fact]
    public void Desde_ObjetoSinPropiedades_DevuelveListaVacia()
    {
        var parametros = ParametrosSql.Desde(new { });

        Assert.Empty(parametros);
    }
}
