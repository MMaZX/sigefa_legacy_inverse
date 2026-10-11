using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SIGEFA.Administradores.VentaCierreBitacora;
using Xunit;

namespace SIGEFA.Tests.VentaCierreBitacora;

// Pruebas del orquestador BitacoraCierre (T2 de venta-cierre-bitacora).
// Regla central: la bitácora nunca lanza al llamador; si la BD falla, el
// paso a paso se guarda en un archivo de respaldo. Todo con falsos.
public class BitacoraCierreTests : IDisposable
{
    private readonly CarpetaTemporal _carpeta = new CarpetaTemporal();
    private readonly RepositorioFalso _repositorio = new RepositorioFalso();
    private readonly RelojFalso _reloj = new RelojFalso();

    public void Dispose()
    {
        _carpeta.Dispose();
    }

    private BitacoraCierre Crear(IBitacoraRepositorio repositorio = null, IRespaldoArchivo respaldo = null)
    {
        return new BitacoraCierre(
            repositorio ?? _repositorio,
            respaldo ?? new RespaldoArchivo(_carpeta.Ruta),
            _reloj.Ahora);
    }

    private static void Registrar(BitacoraCierre bitacora, string paso, string detalle = "")
    {
        bitacora.RegistrarEvento(1, "ALMACEN CENTRAL", paso, null, null, ResultadoEvento.Info, null, detalle);
    }

    [Fact]
    public void Iniciar_CreaElIntentoUnaSolaVezConLosDatosRecibidos()
    {
        BitacoraCierre bitacora = Crear();

        bitacora.Iniciar(DatosBitacora.Intento(7));
        bitacora.Iniciar(DatosBitacora.Intento(7));

        Assert.Single(_repositorio.Intentos);
        Assert.Equal(7, _repositorio.Intentos[0].CodPedido);
    }

    [Fact]
    public void RegistrarEvento_AsignaOrdenCorrelativoYLaHoraDelRelojAlLlamar()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());

        _reloj.Actual = new DateTime(2026, 10, 10, 14, 3, 22, 500);
        Registrar(bitacora, "abrirTransaccion");
        _reloj.Actual = new DateTime(2026, 10, 10, 14, 3, 22, 731);
        Registrar(bitacora, "bloquearSerie");
        bitacora.Finalizar(DatosBitacora.Ok());

        IReadOnlyList<EventoBitacora> eventos = _repositorio.EventosFinalizados[0];
        Assert.Equal(2, eventos.Count);
        Assert.Equal(1, eventos[0].Orden);
        Assert.Equal(new DateTime(2026, 10, 10, 14, 3, 22, 500), eventos[0].Hora);
        Assert.Equal("abrirTransaccion", eventos[0].Paso);
        Assert.Equal(2, eventos[1].Orden);
        Assert.Equal(new DateTime(2026, 10, 10, 14, 3, 22, 731), eventos[1].Hora);
    }

    [Fact]
    public void Finalizar_LlamaUnaSolaVezAlRepositorioConTodosLosEventosYElLogId()
    {
        _repositorio.LogId = 321;
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());
        Registrar(bitacora, "abrirTransaccion");
        Registrar(bitacora, "bloquearSerie");
        Registrar(bitacora, "confirmar");

        bitacora.Finalizar(DatosBitacora.Ok());

        Assert.Single(_repositorio.LogIdsFinalizados);
        Assert.Equal(321L, _repositorio.LogIdsFinalizados[0]);
        Assert.Equal(3, _repositorio.EventosFinalizados[0].Count);
        Assert.Equal(EstadoBitacora.Ok, _repositorio.Resultados[0].Estado);
    }

    [Fact]
    public void Finalizar_ConRepositorioSano_NoEscribeArchivo()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());

        bitacora.Finalizar(DatosBitacora.Ok());

        Assert.False(Directory.Exists(_carpeta.Ruta));
    }

    [Fact]
    public void CrearIntentoLanza_NoLanzaYEscribeElRespaldoConElNumeroLibreSiguiente()
    {
        _carpeta.Crear();
        File.WriteAllText(_carpeta.Archivo("7.1.log"), "previo");
        File.WriteAllText(_carpeta.Archivo("7.2.log"), "previo");
        _repositorio.LanzarEnCrear = true;
        BitacoraCierre bitacora = Crear();

        Exception falla = Record.Exception(() =>
        {
            bitacora.Iniciar(DatosBitacora.Intento(7));
            Registrar(bitacora, "abrirTransaccion");
            bitacora.Finalizar(DatosBitacora.Error());
        });

        Assert.Null(falla);
        Assert.True(File.Exists(_carpeta.Archivo("7.3.log")));
        Assert.Empty(_repositorio.LogIdsFinalizados);
        Assert.Contains("abrirTransaccion", File.ReadAllText(_carpeta.Archivo("7.3.log")));
    }

    [Fact]
    public void FinalizarLanza_NoLanzaYEscribeElRespaldoConElNumeroDeIntentoDelRepositorio()
    {
        _repositorio.NumeroIntento = 4;
        _repositorio.LanzarEnFinalizar = true;
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento(7));
        Registrar(bitacora, "guardarCabecera");

        Exception falla = Record.Exception(() => bitacora.Finalizar(DatosBitacora.Ok()));

        Assert.Null(falla);
        string ruta = _carpeta.Archivo("7.4.log");
        Assert.True(File.Exists(ruta));
        Assert.Contains("guardarCabecera", File.ReadAllText(ruta));
    }

    [Fact]
    public void NuncaLanza_NiConRepositorioNiRespaldoRotos_NiSinIniciar_NiConArgumentosNulos()
    {
        _repositorio.LanzarEnCrear = true;
        _repositorio.LanzarEnFinalizar = true;
        BitacoraCierre roto = Crear(_repositorio, new RespaldoQueLanza());
        BitacoraCierre sinIniciar = Crear();

        Exception falla = Record.Exception(() =>
        {
            sinIniciar.RegistrarEvento(1, null, null, null, null, ResultadoEvento.Info, null, null);
            sinIniciar.Finalizar(DatosBitacora.Ok());
            roto.Iniciar(null);
            roto.Iniciar(DatosBitacora.Intento());
            roto.RegistrarEvento(null, null, null, null, null, ResultadoEvento.Error, null, null);
            roto.Finalizar(null);
            roto.Finalizar(DatosBitacora.Error());
        });

        Assert.Null(falla);
    }

    [Fact]
    public void Eventos_EnmascaranCredencialesQuitanSaltosYAplicanElTopeDe300()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());

        Registrar(bitacora, "guardarCabecera", "fallo Server=db;Uid=root;Pwd=secreto;\r\nsegunda linea");
        Registrar(bitacora, "guardarDetalle", new string('x', 450));
        bitacora.Finalizar(DatosBitacora.Ok());

        IReadOnlyList<EventoBitacora> eventos = _repositorio.EventosFinalizados[0];
        Assert.DoesNotContain("secreto", eventos[0].Detalle);
        Assert.DoesNotContain("root", eventos[0].Detalle);
        Assert.Contains("Pwd=***", eventos[0].Detalle);
        Assert.Contains("Uid=***", eventos[0].Detalle);
        Assert.DoesNotContain("\n", eventos[0].Detalle);
        Assert.DoesNotContain("\r", eventos[0].Detalle);
        Assert.Equal(300, eventos[1].Detalle.Length);
    }

    [Fact]
    public void Resultado_EnmascaraCredencialesYAplicaElTopeDe500AlMensaje()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());

        bitacora.Finalizar(DatosBitacora.Error("password = abc123; " + new string('y', 700)));

        string mensaje = _repositorio.Resultados[0].ErrorMensaje;
        Assert.DoesNotContain("abc123", mensaje);
        Assert.StartsWith("password=***", mensaje);
        Assert.Equal(500, mensaje.Length);
    }

    [Fact]
    public void Finalizar_DosVeces_SoloEscribeUnaVez()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());
        Registrar(bitacora, "confirmar");

        bitacora.Finalizar(DatosBitacora.Ok());
        bitacora.Finalizar(DatosBitacora.Error());

        Assert.Single(_repositorio.LogIdsFinalizados);
        Assert.Equal(EstadoBitacora.Ok, _repositorio.Resultados[0].Estado);
    }

    [Fact]
    public void Finalizar_DosVecesEnRespaldo_SoloEscribeUnArchivo()
    {
        _repositorio.LanzarEnCrear = true;
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento(7));

        bitacora.Finalizar(DatosBitacora.Ok());
        bitacora.Finalizar(DatosBitacora.Ok());

        Assert.Single(Directory.GetFiles(_carpeta.Ruta));
    }

    [Fact]
    public void Finalizar_SinHaberIniciado_NoHaceNada()
    {
        BitacoraCierre bitacora = Crear();

        bitacora.Finalizar(DatosBitacora.Ok());

        Assert.Empty(_repositorio.LogIdsFinalizados);
        Assert.False(Directory.Exists(_carpeta.Ruta));
    }

    [Fact]
    public void RegistrarEvento_TrasFinalizar_SeIgnora()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());
        Registrar(bitacora, "confirmar");
        bitacora.Finalizar(DatosBitacora.Ok());

        Registrar(bitacora, "tardio");
        bitacora.Finalizar(DatosBitacora.Ok());

        Assert.Single(_repositorio.EventosFinalizados);
        Assert.Single(_repositorio.EventosFinalizados[0]);
    }

    [Fact]
    public void RegistrarEvento_Con50Hilos_GuardaCincuentaEventosConOrdenUnicoDel1Al50()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());
        var hilos = new List<Thread>();
        var salida = new ManualResetEventSlim(false);
        for (int i = 0; i < 50; i++)
        {
            var hilo = new Thread(() =>
            {
                salida.Wait();
                Registrar(bitacora, "guardarDetalle");
            });
            hilos.Add(hilo);
            hilo.Start();
        }

        salida.Set();
        foreach (Thread hilo in hilos)
        {
            hilo.Join();
        }

        bitacora.Finalizar(DatosBitacora.Ok());

        IReadOnlyList<EventoBitacora> eventos = _repositorio.EventosFinalizados[0];
        Assert.Equal(50, eventos.Count);
        var ordenes = new HashSet<int>();
        foreach (EventoBitacora evento in eventos)
        {
            ordenes.Add(evento.Orden);
        }

        Assert.Equal(50, ordenes.Count);
        Assert.Equal(1, Min(ordenes));
        Assert.Equal(50, Max(ordenes));
    }

    [Fact]
    public void RegistrarEvento_RecortaAlmacenYPasoASusTopes()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());

        bitacora.RegistrarEvento(1, new string('a', 200), new string('p', 100), null, null, ResultadoEvento.Info, null, "d");
        bitacora.Finalizar(DatosBitacora.Ok());

        EventoBitacora evento = _repositorio.EventosFinalizados[0][0];
        Assert.Equal(80, evento.Almacen.Length);
        Assert.Equal(40, evento.Paso.Length);
    }

    [Fact]
    public void Iniciar_RecortaUsuarioEquipoYVersionASusTopes()
    {
        BitacoraCierre bitacora = Crear();
        var intento = new IntentoBitacora(
            7, 15, new string('u', 200) + "\n", new string('e', 200), new string('v', 200), 2,
            new DateTime(2026, 10, 10, 14, 3, 22, 123));

        bitacora.Iniciar(intento);

        IntentoBitacora guardado = _repositorio.Intentos[0];
        Assert.Equal(80, guardado.Usuario.Length);
        Assert.Equal(60, guardado.Equipo.Length);
        Assert.Equal(20, guardado.VersionApp.Length);
    }

    [Fact]
    public void Finalizar_RecortaLosCamposDeErrorASusTopes()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());
        var resultado = new ResultadoBitacora(
            EstadoBitacora.Error, 1, new DateTime(2026, 10, 10, 14, 3, 25, 789), 10,
            new string('p', 100), new string('q', 200), 1062, "230000", "boom", null);

        bitacora.Finalizar(resultado);

        ResultadoBitacora guardado = _repositorio.Resultados[0];
        Assert.Equal(40, guardado.ErrorPaso.Length);
        Assert.Equal(80, guardado.ErrorProcedimiento.Length);
        Assert.Equal(5, guardado.ErrorSqlState.Length);
    }

    [Fact]
    public void RegistrarEvento_EnmascaraLaCredencialAntesDelTope()
    {
        BitacoraCierre bitacora = Crear();
        bitacora.Iniciar(DatosBitacora.Intento());

        Registrar(bitacora, "paso", new string('a', 295) + " Pwd=secreto");
        bitacora.Finalizar(DatosBitacora.Ok());

        string detalle = _repositorio.EventosFinalizados[0][0].Detalle;
        Assert.DoesNotContain("secreto", detalle);
        Assert.True(detalle.Length <= BitacoraCierre.TopeDetalle);
    }

    [Fact]
    public void ConDependenciasNulas_IniciarRegistrarYFinalizarNoLanzan()
    {
        var bitacora = new BitacoraCierre(null, null, null);

        Exception falla = Record.Exception(() =>
        {
            bitacora.Iniciar(DatosBitacora.Intento());
            bitacora.RegistrarEvento(1, "A", "p", null, null, ResultadoEvento.Info, null, "d");
            bitacora.Finalizar(DatosBitacora.Error());
        });

        Assert.Null(falla);
    }

    private static int Min(HashSet<int> valores)
    {
        int minimo = int.MaxValue;
        foreach (int valor in valores)
        {
            minimo = Math.Min(minimo, valor);
        }

        return minimo;
    }

    private static int Max(HashSet<int> valores)
    {
        int maximo = int.MinValue;
        foreach (int valor in valores)
        {
            maximo = Math.Max(maximo, valor);
        }

        return maximo;
    }
}
