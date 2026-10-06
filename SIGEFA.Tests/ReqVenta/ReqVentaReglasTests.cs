using SIGEFA.Administradores.ReqVenta;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas de las reglas puras de anulación de requerimientos de venta (T2a).
// Solo cubren ReqVentaReglas.Evaluar, sin base de datos ni interfaz gráfica.
public class ReqVentaReglasTests
{
    // El pendiente (7) con tipo venta (2) permite anulación directa.
    [Fact]
    public void Evaluar_PendienteConTipoVenta_PermiteAnularPendiente()
    {
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(2, 7);

        Assert.True(decision.Permitido);
        Assert.Equal(AccionAnulacion.AnularPendiente, decision.Accion);
        Assert.False(string.IsNullOrWhiteSpace(decision.Motivo));
    }

    // El aprobado transferido (13) con tipo venta (2) permite anular con extorno.
    [Fact]
    public void Evaluar_AprobadoTransferidoConTipoVenta_PermiteAnularConExtorno()
    {
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(2, 13);

        Assert.True(decision.Permitido);
        Assert.Equal(AccionAnulacion.AnularConExtorno, decision.Accion);
        Assert.False(string.IsNullOrWhiteSpace(decision.Motivo));
    }

    // El ya anulado (12) no se puede volver a anular.
    [Fact]
    public void Evaluar_AnuladoConTipoVenta_NoPermite()
    {
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(2, 12);

        Assert.False(decision.Permitido);
        Assert.Equal(AccionAnulacion.Ninguna, decision.Accion);
        Assert.False(string.IsNullOrWhiteSpace(decision.Motivo));
    }

    // Los estados no anulables (y los desconocidos como 99) se deniegan
    // y el motivo nombra el número de estado recibido.
    [Theory]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(17)]
    [InlineData(99)]
    public void Evaluar_EstadoNoAnulableConTipoVenta_NoPermiteYMencionaEstado(int estado)
    {
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(2, estado);

        Assert.False(decision.Permitido);
        Assert.Equal(AccionAnulacion.Ninguna, decision.Accion);
        Assert.False(string.IsNullOrWhiteSpace(decision.Motivo));
        Assert.Contains(estado.ToString(), decision.Motivo);
    }

    // Solo el tipo venta (2) admite anulación; otro tipo se deniega
    // y el motivo lo explica con el tipo recibido.
    [Fact]
    public void Evaluar_TipoDistintoDeVenta_NoPermite()
    {
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(1, 7);

        Assert.False(decision.Permitido);
        Assert.Equal(AccionAnulacion.Ninguna, decision.Accion);
        Assert.Contains("solo requerimientos de venta", decision.Motivo);
        Assert.Contains("1", decision.Motivo);
    }

    // Ninguna decisión sale sin motivo: cubre todos los caminos de Evaluar.
    [Theory]
    [InlineData(2, 7)]
    [InlineData(2, 8)]
    [InlineData(2, 9)]
    [InlineData(2, 10)]
    [InlineData(2, 11)]
    [InlineData(2, 12)]
    [InlineData(2, 13)]
    [InlineData(2, 17)]
    [InlineData(2, 99)]
    [InlineData(1, 7)]
    [InlineData(1, 12)]
    public void Evaluar_CualquierCombinacion_MotivoNuncaVacio(int tipoReq, int estado)
    {
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(tipoReq, estado);

        Assert.False(string.IsNullOrWhiteSpace(decision.Motivo));
    }
}
