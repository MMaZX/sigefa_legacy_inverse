using System;
using System.Collections.Generic;
using System.Linq;
using SIGEFA.Administradores.VentaCierre;
using SIGEFA.Administradores.VentaCierreBitacora;
using Xunit;

namespace SIGEFA.Tests.VentaCierreBitacora;

// Pruebas de ProgresoPorPedido (T4 de venta-cierre-bitacora).
// Un cierre con varios pedidos tiene una bitácora por pedido: los eventos globales
// van a todas y los de bloque solo a la del pedido de ese bloque.
public class ProgresoPorPedidoTests : IDisposable
{
    private readonly CarpetaTemporal _carpeta = new CarpetaTemporal();
    private readonly RelojFalso _reloj = new RelojFalso();
    private readonly Dictionary<int, RepositorioFalso> _repositorios = new Dictionary<int, RepositorioFalso>();
    private readonly Dictionary<int, BitacoraCierre> _bitacoras = new Dictionary<int, BitacoraCierre>();

    public void Dispose()
    {
        _carpeta.Dispose();
    }

    private sealed class ProgresoFalso : IProgress<VentaCierreProgreso>
    {
        public readonly List<VentaCierreProgreso> Informados = new List<VentaCierreProgreso>();

        public void Report(VentaCierreProgreso valor)
        {
            Informados.Add(valor);
        }
    }

    // Crea una bitácora iniciada por pedido, cada una con su repositorio falso.
    private void CrearBitacoras(params int[] pedidos)
    {
        foreach (int pedido in pedidos)
        {
            var repositorio = new RepositorioFalso();
            var bitacora = new BitacoraCierre(repositorio, new RespaldoArchivo(_carpeta.Ruta), _reloj.Ahora);
            bitacora.Iniciar(DatosBitacora.Intento(pedido));
            _repositorios[pedido] = repositorio;
            _bitacoras[pedido] = bitacora;
        }
    }

    // Cierra todas las bitácoras y devuelve los eventos guardados del pedido.
    private IReadOnlyList<EventoBitacora> EventosDe(int pedido)
    {
        foreach (BitacoraCierre bitacora in _bitacoras.Values)
        {
            bitacora.Finalizar(DatosBitacora.Ok());
        }

        RepositorioFalso repositorio = _repositorios[pedido];
        return repositorio.EventosFinalizados.Count == 0
            ? new List<EventoBitacora>()
            : repositorio.EventosFinalizados[0];
    }

    private static VentaCierreProgreso Global(VentaCierrePaso paso)
    {
        return new VentaCierreProgreso(paso, 0, 0, 1, 3, "Global", "evento global");
    }

    private static VentaCierreProgreso DeBloque(VentaCierrePaso paso, int bloque, string almacen, int item = 0)
    {
        return new VentaCierreProgreso(paso, item, 2, bloque, 3, almacen, "evento de bloque " + bloque);
    }

    [Fact]
    public void Report_EventoGlobal_VaATodasLasBitacoras()
    {
        CrearBitacoras(10, 20);
        var progreso = new ProgresoPorPedido(new[] { 10, 20 }, _bitacoras, null);

        progreso.Report(Global(VentaCierrePaso.abrirTransaccion));

        Assert.Equal(new[] { "abrirTransaccion" }, EventosDe(10).Select(e => e.Paso).ToArray());
        Assert.Equal(new[] { "abrirTransaccion" }, EventosDe(20).Select(e => e.Paso).ToArray());
    }

    [Theory]
    [InlineData(VentaCierrePaso.abrirTransaccion)]
    [InlineData(VentaCierrePaso.bloquearSerie)]
    [InlineData(VentaCierrePaso.bloquearStock)]
    [InlineData(VentaCierrePaso.confirmar)]
    public void Report_PasoGlobal_ConAlmacenNoGlobal_VaATodosLosPedidos(VentaCierrePaso paso)
    {
        CrearBitacoras(10, 20, 30);
        var progreso = new ProgresoPorPedido(new[] { 10, 20, 30 }, _bitacoras, null);

        // Un índice válido evita que el fallback de bloque fuera de rango oculte el defecto.
        progreso.Report(DeBloque(paso, 2, "ALMACEN B"));

        foreach (int pedido in new[] { 10, 20, 30 })
        {
            EventoBitacora evento = EventosDe(pedido).Single();
            Assert.Equal(paso.ToString(), evento.Paso);
            Assert.Equal(2, evento.Bloque);
            Assert.Equal("ALMACEN B", evento.Almacen);
        }
    }

    [Theory]
    [InlineData(VentaCierrePaso.guardarCabecera)]
    [InlineData(VentaCierrePaso.guardarDetalle)]
    [InlineData(VentaCierrePaso.reservarNotaCredito)]
    [InlineData(VentaCierrePaso.guardarPago)]
    public void Report_PasoDeBloque_ConAlmacenGlobal_VaSoloAlPedidoCorrespondiente(VentaCierrePaso paso)
    {
        CrearBitacoras(10, 20, 30);
        var progreso = new ProgresoPorPedido(new[] { 10, 20, 30 }, _bitacoras, null);

        progreso.Report(DeBloque(paso, 2, "Global", 1));

        Assert.Empty(EventosDe(10));
        EventoBitacora evento = EventosDe(20).Single();
        Assert.Equal(paso.ToString(), evento.Paso);
        Assert.Equal(2, evento.Bloque);
        Assert.Equal("Global", evento.Almacen);
        Assert.Empty(EventosDe(30));
    }

    [Theory]
    [InlineData(VentaCierrePaso.guardarCabecera)]
    [InlineData(VentaCierrePaso.guardarDetalle)]
    [InlineData(VentaCierrePaso.reservarNotaCredito)]
    [InlineData(VentaCierrePaso.guardarPago)]
    public void Report_PasoDeBloque_ConAlmacenGlobalYPedidoRepetido_NoTocaOtroPedido(VentaCierrePaso paso)
    {
        CrearBitacoras(10, 20);
        var progreso = new ProgresoPorPedido(new[] { 10, 10, 20 }, _bitacoras, null);

        progreso.Report(DeBloque(paso, 1, "Global", 1));
        progreso.Report(DeBloque(paso, 2, "Global", 2));

        IReadOnlyList<EventoBitacora> propios = EventosDe(10);
        Assert.Equal(new[] { paso.ToString(), paso.ToString() }, propios.Select(e => e.Paso).ToArray());
        Assert.Equal(new int?[] { 1, 2 }, propios.Select(e => e.Bloque).ToArray());
        Assert.All(propios, evento => Assert.Equal("Global", evento.Almacen));
        Assert.Empty(EventosDe(20));
    }

    [Fact]
    public void Report_EventoDeBloque_VaSoloAlPedidoDeEseBloque()
    {
        CrearBitacoras(10, 20, 30);
        var progreso = new ProgresoPorPedido(new[] { 10, 20, 30 }, _bitacoras, null);

        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 2, "ALMACEN B"));

        Assert.Empty(EventosDe(10));
        Assert.Equal(new[] { "guardarCabecera" }, EventosDe(20).Select(e => e.Paso).ToArray());
        Assert.Empty(EventosDe(30));
    }

    [Fact]
    public void Report_EventoDeBloque_ConservaBloqueYAlmacenDelEvento()
    {
        CrearBitacoras(10, 20);
        var progreso = new ProgresoPorPedido(new[] { 10, 20 }, _bitacoras, null);

        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 2, "ALMACEN B"));

        EventoBitacora evento = EventosDe(20).Single();
        Assert.Equal(2, evento.Bloque);
        Assert.Equal("ALMACEN B", evento.Almacen);
    }

    [Fact]
    public void Report_DosBloquesDelMismoPedido_AmbosVanALaMismaBitacora()
    {
        CrearBitacoras(10, 20);
        var progreso = new ProgresoPorPedido(new[] { 10, 10, 20 }, _bitacoras, null);

        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 1, "ALMACEN A"));
        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 2, "ALMACEN B"));
        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 3, "ALMACEN C"));

        Assert.Equal(new int?[] { 1, 2 }, EventosDe(10).Select(e => e.Bloque).ToArray());
        Assert.Equal(new int?[] { 3 }, EventosDe(20).Select(e => e.Bloque).ToArray());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(99)]
    public void Report_BloqueFueraDeRango_VaATodasLasBitacoras(int bloque)
    {
        CrearBitacoras(10, 20);
        var progreso = new ProgresoPorPedido(new[] { 10, 20 }, _bitacoras, null);

        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, bloque, "ALMACEN X"));

        Assert.Single(EventosDe(10));
        Assert.Single(EventosDe(20));
    }

    [Fact]
    public void Report_DelegaAlProgresoInternoUnaVezPorEventoYEnOrden()
    {
        CrearBitacoras(10, 20);
        var interno = new ProgresoFalso();
        var progreso = new ProgresoPorPedido(new[] { 10, 20 }, _bitacoras, interno);
        VentaCierreProgreso primero = Global(VentaCierrePaso.abrirTransaccion);
        VentaCierreProgreso segundo = DeBloque(VentaCierrePaso.guardarCabecera, 1, "ALMACEN A");

        progreso.Report(primero);
        progreso.Report(segundo);

        Assert.Equal(2, interno.Informados.Count);
        Assert.Same(primero, interno.Informados[0]);
        Assert.Same(segundo, interno.Informados[1]);
    }

    [Fact]
    public void Report_AunqueLaBitacoraFalle_DelegaAlProgresoInterno()
    {
        CrearBitacoras(10, 20);
        _repositorios[10].LanzarEnFinalizar = true;
        var interno = new ProgresoFalso();
        var progreso = new ProgresoPorPedido(new[] { 10, 20 }, _bitacoras, interno);

        progreso.Report(Global(VentaCierrePaso.abrirTransaccion));
        _bitacoras[10].Finalizar(DatosBitacora.Ok());

        Assert.Single(interno.Informados);
    }

    [Fact]
    public void Report_ConBitacoraNulaEnElDiccionario_NoLanzaYDelega()
    {
        CrearBitacoras(10);
        var porPedido = new Dictionary<int, BitacoraCierre> { { 10, _bitacoras[10] }, { 20, null } };
        var interno = new ProgresoFalso();
        var progreso = new ProgresoPorPedido(new[] { 10, 20 }, porPedido, interno);

        progreso.Report(Global(VentaCierrePaso.abrirTransaccion));
        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 2, "ALMACEN B"));

        Assert.Equal(2, interno.Informados.Count);
    }

    [Fact]
    public void Report_ConInternoNulo_SoloRegistraYNoLanza()
    {
        CrearBitacoras(10);
        var progreso = new ProgresoPorPedido(new[] { 10 }, _bitacoras, null);

        progreso.Report(Global(VentaCierrePaso.abrirTransaccion));

        Assert.Single(EventosDe(10));
    }

    [Fact]
    public void Report_VariosEventos_ConservaElOrdenPorPedido()
    {
        CrearBitacoras(10, 20);
        var progreso = new ProgresoPorPedido(new[] { 10, 20 }, _bitacoras, null);

        progreso.Report(Global(VentaCierrePaso.abrirTransaccion));
        progreso.Report(Global(VentaCierrePaso.bloquearSerie));
        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 1, "ALMACEN A"));
        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 2, "ALMACEN B"));
        progreso.Report(DeBloque(VentaCierrePaso.guardarDetalle, 2, "ALMACEN B", 1));
        progreso.Report(Global(VentaCierrePaso.confirmar));

        IReadOnlyList<EventoBitacora> del10 = EventosDe(10);
        IReadOnlyList<EventoBitacora> del20 = EventosDe(20);

        Assert.Equal(
            new[] { "abrirTransaccion", "bloquearSerie", "guardarCabecera", "confirmar" },
            del10.Select(e => e.Paso).ToArray());
        Assert.Equal(new[] { 1, 2, 3, 4 }, del10.Select(e => e.Orden).ToArray());
        Assert.Equal(
            new[] { "abrirTransaccion", "bloquearSerie", "guardarCabecera", "guardarDetalle", "confirmar" },
            del20.Select(e => e.Paso).ToArray());
        Assert.Equal(new[] { 1, 2, 3, 4, 5 }, del20.Select(e => e.Orden).ToArray());
    }

    [Fact]
    public void Constructor_ConDatosNulos_NoLanzaAlReportar()
    {
        var interno = new ProgresoFalso();
        var progreso = new ProgresoPorPedido(null, null, interno);

        progreso.Report(Global(VentaCierrePaso.abrirTransaccion));
        progreso.Report(DeBloque(VentaCierrePaso.guardarCabecera, 1, "ALMACEN A"));

        Assert.Equal(2, interno.Informados.Count);
    }
}
