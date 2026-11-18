using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de lo que dice y como se comporta el boton "Buscar Actualizaciones".
public class PresentacionBotonActualizacionTests
{
    [Fact]
    public void Para_AlDia_MuestraBuscarActualizacionesSinAviso()
    {
        var p = PresentacionBotonActualizacion.Para(EstadoActualizacion.AlDia, 0);

        Assert.Equal("Buscar Actualizaciones", p.Texto);
        Assert.Equal(UrgenciaBotonActualizacion.Ninguna, p.Urgencia);
    }

    [Fact]
    public void Para_Disponible_AvisaLaBuildPendienteConAcentoDeAviso()
    {
        var p = PresentacionBotonActualizacion.Para(EstadoActualizacion.Disponible, 245);

        Assert.Equal("¡Tienes una actualización pendiente a la build 245!", p.Texto);
        Assert.Equal(UrgenciaBotonActualizacion.Aviso, p.Urgencia);
    }

    [Fact]
    public void Para_Obligatoria_LoDiceEnMayusculasConAcentoObligatorio()
    {
        var p = PresentacionBotonActualizacion.Para(EstadoActualizacion.Obligatoria, 245);

        Assert.Equal("¡ACTUALIZACIÓN OBLIGATORIA: build 245!", p.Texto);
        Assert.Equal(UrgenciaBotonActualizacion.Obligatoria, p.Urgencia);
    }
}
