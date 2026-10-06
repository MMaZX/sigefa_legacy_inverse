using System;
using System.Collections.Generic;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.Helper;

public class FilaExtensionesTests
{
    private static Dictionary<string, object> NuevaFila(params object[] clavesYValores)
    {
        var fila = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < clavesYValores.Length; i += 2)
        {
            fila[(string)clavesYValores[i]] = clavesYValores[i + 1];
        }
        return fila;
    }

    [Fact]
    public void Valor_IntDesdeLong_Convierte()
    {
        var fila = NuevaFila("cantidad", 42L);

        Assert.Equal(42, fila.Valor<int>("cantidad"));
    }

    [Fact]
    public void Valor_IntDesdeDecimal_Convierte()
    {
        var fila = NuevaFila("cantidad", 7m);

        Assert.Equal(7, fila.Valor<int>("cantidad"));
    }

    [Fact]
    public void Valor_Cadena_DevuelveElTexto()
    {
        var fila = NuevaFila("nombre", "Servicio");

        Assert.Equal("Servicio", fila.Valor<string>("nombre"));
    }

    [Fact]
    public void Valor_ValorNulo_DevuelveElDefectoIndicado()
    {
        var fila = NuevaFila("cantidad", null);

        Assert.Equal(-1, fila.Valor("cantidad", -1));
    }

    [Fact]
    public void Valor_DBNull_DevuelveElDefectoIndicado()
    {
        var fila = NuevaFila("nombre", DBNull.Value);

        Assert.Equal("sin dato", fila.Valor("nombre", "sin dato"));
    }

    [Fact]
    public void Valor_NulosSinDefecto_DevuelveDefaultDelTipo()
    {
        var fila = NuevaFila("cantidad", DBNull.Value, "nombre", null);

        Assert.Equal(0, fila.Valor<int>("cantidad"));
        Assert.Null(fila.Valor<string>("nombre"));
    }

    [Fact]
    public void Valor_NullableConValor_Convierte()
    {
        var fila = NuevaFila("cantidad", 5L);

        Assert.Equal(5, fila.Valor<int?>("cantidad"));
    }

    [Fact]
    public void Valor_NullableConDBNull_DevuelveNulo()
    {
        var fila = NuevaFila("cantidad", DBNull.Value);

        Assert.Null(fila.Valor<int?>("cantidad"));
    }

    [Fact]
    public void Valor_BoolDesdeUlong_DistintoDeCeroEsVerdadero()
    {
        var fila = NuevaFila("activo", 1UL, "inactivo", 0UL);

        Assert.True(fila.Valor<bool>("activo"));
        Assert.False(fila.Valor<bool>("inactivo"));
    }

    [Fact]
    public void Valor_BoolDesdeSbyte_Convierte()
    {
        var fila = NuevaFila("activo", (sbyte)1, "inactivo", (sbyte)0);

        Assert.True(fila.Valor<bool>("activo"));
        Assert.False(fila.Valor<bool>("inactivo"));
    }

    [Fact]
    public void Valor_BoolDesdeBool_DevuelveElMismoValor()
    {
        var fila = NuevaFila("activo", true);

        Assert.True(fila.Valor<bool>("activo"));
    }

    [Fact]
    public void Valor_BoolDesdeEnterosVarios_Convierte()
    {
        var fila = NuevaFila("a", (byte)2, "b", (short)0, "c", 3, "d", 0L);

        Assert.True(fila.Valor<bool>("a"));
        Assert.False(fila.Valor<bool>("b"));
        Assert.True(fila.Valor<bool>("c"));
        Assert.False(fila.Valor<bool>("d"));
    }

    [Fact]
    public void Valor_NullableBoolDesdeUlong_Convierte()
    {
        var fila = NuevaFila("activo", 1UL);

        Assert.True(fila.Valor<bool?>("activo"));
    }

    [Fact]
    public void Valor_ClaveInexistente_LanzaExcepcionConClaveYColumnasDisponibles()
    {
        var fila = NuevaFila("id", 1, "nombre", "x");

        var ex = Assert.Throws<KeyNotFoundException>(() => fila.Valor<int>("cantidad"));

        Assert.Contains("cantidad", ex.Message);
        Assert.Contains("id", ex.Message);
        Assert.Contains("nombre", ex.Message);
    }

    [Fact]
    public void Valor_ConversionInvalida_LanzaInvalidCastConColumnaYTipos()
    {
        var fila = NuevaFila("nombre", "no es numero");

        var ex = Assert.Throws<InvalidCastException>(() => fila.Valor<int>("nombre"));

        Assert.Contains("nombre", ex.Message);
        Assert.Contains("String", ex.Message);
        Assert.Contains("Int32", ex.Message);
    }

    [Fact]
    public void Valor_ClaveEnOtraCapitalizacion_LaEncuentraConDiccionarioOrdinalIgnoreCase()
    {
        var fila = NuevaFila("Cantidad", 9L);

        Assert.Equal(9, fila.Valor<int>("CANTIDAD"));
    }
}
