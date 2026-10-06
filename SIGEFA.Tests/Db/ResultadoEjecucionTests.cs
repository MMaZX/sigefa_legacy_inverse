using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.Helper;

public class ResultadoEjecucionTests
{
    [Fact]
    public void Ok_ConFilasAfectadas_EsVerdadero()
    {
        var resultado = new ResultadoEjecucion(15, 1);

        Assert.True(resultado.Ok);
        Assert.Equal(15, resultado.Id);
        Assert.Equal(1, resultado.FilasAfectadas);
    }

    [Fact]
    public void Ok_SinFilasAfectadas_EsFalso()
    {
        var resultado = new ResultadoEjecucion(0, 0);

        Assert.False(resultado.Ok);
    }
}
