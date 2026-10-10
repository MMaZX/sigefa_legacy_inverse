using System;
using System.Collections.Generic;
using SIGEFA.Administradores.VentaCierre;
using SIGEFA.Administradores.VentaCierreBitacora;
using Xunit;

namespace SIGEFA.Tests.VentaCierreBitacora;

// Pruebas del decorador ProgresoConBitacora (T2 de venta-cierre-bitacora).
// Registra cada avance del cierre en la bitácora y luego delega al progreso
// interno (el diálogo), sin que un fallo de la bitácora llegue al cierre.
public class ProgresoConBitacoraTests : IDisposable
{
    private readonly CarpetaTemporal _carpeta = new CarpetaTemporal();
    private readonly RepositorioFalso _repositorio = new RepositorioFalso();
    private readonly RelojFalso _reloj = new RelojFalso();

    public void Dispose()
    {
        _carpeta.Dispose();
    }

    // Progreso interno falso: guarda lo reportado y puede lanzar.
    private sealed class ProgresoFalso : IProgress<VentaCierreProgreso>
    {
        public readonly List<VentaCierreProgreso> Informados = new List<VentaCierreProgreso>();

        public void Report(VentaCierreProgreso valor)
        {
            Informados.Add(valor);
        }
    }

    private BitacoraCierre CrearBitacoraIniciada()
    {
        var bitacora = new BitacoraCierre(_repositorio, new RespaldoArchivo(_carpeta.Ruta), _reloj.Ahora);
        bitacora.Iniciar(DatosBitacora.Intento());
        return bitacora;
    }

    [Fact]
    public void Report_RegistraElEventoYDespuesDelegaAlProgresoInterno()
    {
        BitacoraCierre bitacora = CrearBitacoraIniciada();
        var interno = new ProgresoFalso();
        var decorador = new ProgresoConBitacora(bitacora, interno);
        var progreso = new VentaCierreProgreso(VentaCierrePaso.bloquearSerie, 1, 4, 2, 3, "ALMACEN NORTE", "serie F001");

        decorador.Report(progreso);
        bitacora.Finalizar(DatosBitacora.Ok());

        Assert.Single(interno.Informados);
        Assert.Same(progreso, interno.Informados[0]);
        EventoBitacora evento = _repositorio.EventosFinalizados[0][0];
        Assert.Equal("bloquearSerie", evento.Paso);
        Assert.Equal(2, evento.Bloque);
        Assert.Equal("ALMACEN NORTE", evento.Almacen);
        Assert.Equal("serie F001", evento.Detalle);
        Assert.Equal(ResultadoEvento.Info, evento.Resultado);
    }

    [Fact]
    public void Report_GuardaElItemSoloEnElPasoGuardarDetalle()
    {
        BitacoraCierre bitacora = CrearBitacoraIniciada();
        var decorador = new ProgresoConBitacora(bitacora, null);

        decorador.Report(new VentaCierreProgreso(VentaCierrePaso.guardarDetalle, 3, 4, 1, 1, "A", string.Empty));
        decorador.Report(new VentaCierreProgreso(VentaCierrePaso.guardarPago, 3, 4, 1, 1, "A", string.Empty));
        bitacora.Finalizar(DatosBitacora.Ok());

        IReadOnlyList<EventoBitacora> eventos = _repositorio.EventosFinalizados[0];
        Assert.Equal(3, eventos[0].Item);
        Assert.Null(eventos[1].Item);
    }

    [Fact]
    public void Report_ConProgresoInternoNulo_NoLanzaYRegistra()
    {
        BitacoraCierre bitacora = CrearBitacoraIniciada();
        var decorador = new ProgresoConBitacora(bitacora, null);

        Exception falla = Record.Exception(() =>
            decorador.Report(new VentaCierreProgreso(VentaCierrePaso.confirmar, 1, 1, 1, 1, "A", string.Empty)));
        bitacora.Finalizar(DatosBitacora.Ok());

        Assert.Null(falla);
        Assert.Single(_repositorio.EventosFinalizados[0]);
    }

    [Fact]
    public void Report_ConBitacoraQueFalla_AunAsiDelegaAlProgresoInternoYNoLanza()
    {
        var interno = new ProgresoFalso();
        var decorador = new ProgresoConBitacora(null, interno);
        var progreso = new VentaCierreProgreso(VentaCierrePaso.abrirTransaccion, 1, 1, 1, 1, "A", string.Empty);

        Exception falla = Record.Exception(() => decorador.Report(progreso));

        Assert.Null(falla);
        Assert.Single(interno.Informados);
        Assert.Same(progreso, interno.Informados[0]);
    }

    [Fact]
    public void DesdeFallo_ArmaUnResultadoDeErrorConLaDuracionEnMilisegundos()
    {
        var inicio = new DateTime(2026, 10, 10, 14, 3, 22, 123);
        var fin = new DateTime(2026, 10, 10, 14, 3, 25, 789);

        ResultadoBitacora resultado = ProgresoConBitacora.DesdeFallo(
            "guardarDetalle", "GuardaDetalleFacturaVenta", 1062, "23000", "duplicado", 1, inicio, fin);

        Assert.Equal(EstadoBitacora.Error, resultado.Estado);
        Assert.Equal(1, resultado.BloquesOk);
        Assert.Equal(3666, resultado.DuracionMs);
        Assert.Equal(fin, resultado.Fin);
        Assert.Equal("guardarDetalle", resultado.ErrorPaso);
        Assert.Equal("GuardaDetalleFacturaVenta", resultado.ErrorProcedimiento);
        Assert.Equal(1062, resultado.ErrorMysqlNum);
        Assert.Equal("23000", resultado.ErrorSqlState);
        Assert.Equal("duplicado", resultado.ErrorMensaje);
    }

    [Fact]
    public void DesdeExcepcion_UsaElMensajeDeLaExcepcionYDejaElRestoVacio()
    {
        var inicio = new DateTime(2026, 10, 10, 14, 3, 22, 0);
        var fin = new DateTime(2026, 10, 10, 14, 3, 23, 0);

        ResultadoBitacora resultado = ProgresoConBitacora.DesdeExcepcion(
            new InvalidOperationException("se cayó"), 0, inicio, fin);

        Assert.Equal(EstadoBitacora.Error, resultado.Estado);
        Assert.Equal("se cayó", resultado.ErrorMensaje);
        Assert.Equal(1000, resultado.DuracionMs);
        Assert.Equal(string.Empty, resultado.ErrorPaso);
        Assert.Null(resultado.ErrorMysqlNum);
    }

    [Fact]
    public void DesdeExito_ArmaUnResultadoOkConLaFactura()
    {
        var inicio = new DateTime(2026, 10, 10, 14, 3, 22, 0);
        var fin = new DateTime(2026, 10, 10, 14, 3, 22, 250);

        ResultadoBitacora resultado = ProgresoConBitacora.DesdeExito(2, 555, inicio, fin);

        Assert.Equal(EstadoBitacora.Ok, resultado.Estado);
        Assert.Equal(2, resultado.BloquesOk);
        Assert.Equal(250, resultado.DuracionMs);
        Assert.Equal(555, resultado.CodFacturaVenta);
    }
}
