using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de lo que dice y como se comporta el boton "Buscar Actualizaciones".
public class PresentacionBotonActualizacionTests
{
    [Fact]
    public void Para_AlDia_MuestraBuscarActualizacionesYNoParpadea()
    {
        var p = PresentacionBotonActualizacion.Para(EstadoActualizacion.AlDia, 0);

        Assert.Equal("Buscar Actualizaciones", p.Texto);
        Assert.False(p.Parpadea);
    }

    [Fact]
    public void Para_Disponible_AvisaLaBuildPendienteYParpadea()
    {
        var p = PresentacionBotonActualizacion.Para(EstadoActualizacion.Disponible, 245);

        Assert.Equal("¡Tienes una actualización pendiente a la build 245!", p.Texto);
        Assert.True(p.Parpadea);
    }

    [Fact]
    public void Para_Obligatoria_LoDiceEnMayusculasYParpadea()
    {
        var p = PresentacionBotonActualizacion.Para(EstadoActualizacion.Obligatoria, 245);

        Assert.Equal("¡ACTUALIZACIÓN OBLIGATORIA: build 245!", p.Texto);
        Assert.True(p.Parpadea);
    }
}
