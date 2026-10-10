using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas del servicio de aprobación de un requerimiento de venta (T5b-2).
// ReqVentaAprobacion replica btnAprobar_Click rama TipoReq==2 sin controles ni cuadros de mensaje:
// mismos procedimientos y mismo orden que el legacy, tramos transaccionales como el legacy
// (actualizar el requerimiento, nota de salida y nota de ingreso) y el resto en autocommit.
// Las unitarias usan consultor falso y no tocan BD; la integración con rollback está en
// ReqVentaAprobacionIntegracionTests.
public class ReqVentaAprobacionTests
{
    private const int CodReq = 12044;
    private const int CodUser = 99;
    private const int CodAutorizador = 5;

    // Orden esperado de procedimientos para 2 líneas con cantidad mayor que cero.
    private static readonly string[] OrdenEsperado =
    {
        "CargaRequerimientoAlmacen",
        "ListadoDetalleRequerimientoAlmacen",
        "BuscaSeriexDocumento",
        "MuestraUltimoPrecioCompraPorProductoyUnidad",
        "MuestraUltimoPrecioCompraPorProductoyUnidad",
        "MuestraTransaccion",
        "BuscaTipoDocumento",
        "AprobarRequerimientoAlmacen",
        "SetAutorizadorEnRequerimientoAlmacen",
        "CargaRequerimientoAlmacen",
        "ActualizaRequerimientoAlmacen",
        "EliminaDetalleRequerimientoAlmacen",
        "GuardaDetalleRequerimientoAlmacen",
        "GuardaDetalleRequerimientoAlmacen",
        "SeparandoStockAlAprobarReqAlmacen",
        "SeparandoStockAlAprobarReqAlmacen",
        "GuardaTransferencia",
        "RegistrarTransferenciaRequerimientoAlmacen",
        "GuardaDetalleTransferencia",
        "GuardaDetalleTransferencia",
        "GuardaNotaSalida",
        "GuardaDetalleSalida",
        "GuardaDetalleSalida",
        "GuardaNotaIngreso",
        "GuardaDetalleIngreso",
        "GuardaDetalleIngreso",
        "AprobarTransferencia",
        "ActualizaCantidadPendienteReqAlmacen",
        "ActualizaEstadoReqAlmacen",
    };

    // Cantidad real de argumentos de cada procedimiento según SHOW CREATE PROCEDURE en dev (2026-10-10).
    private static readonly Dictionary<string, int> ArgumentosReales = new Dictionary<string, int>
    {
        { "CargaRequerimientoAlmacen", 1 },
        { "ListadoDetalleRequerimientoAlmacen", 1 },
        { "BuscaSeriexDocumento", 2 },
        { "MuestraUltimoPrecioCompraPorProductoyUnidad", 3 },
        { "MuestraTransaccion", 1 },
        { "BuscaTipoDocumento", 1 },
        { "AprobarRequerimientoAlmacen", 2 },
        { "SetAutorizadorEnRequerimientoAlmacen", 2 },
        { "ActualizaRequerimientoAlmacen", 22 },
        { "EliminaDetalleRequerimientoAlmacen", 1 },
        { "GuardaDetalleRequerimientoAlmacen", 10 },
        { "SeparandoStockAlAprobarReqAlmacen", 5 },
        { "GuardaTransferencia", 24 },
        { "RegistrarTransferenciaRequerimientoAlmacen", 3 },
        { "GuardaDetalleTransferencia", 24 },
        { "GuardaNotaSalida", 35 },
        { "GuardaDetalleSalida", 23 },
        { "GuardaNotaIngreso", 34 },
        { "GuardaDetalleIngreso", 25 },
        { "AprobarTransferencia", 1 },
        { "ActualizaCantidadPendienteReqAlmacen", 1 },
        { "ActualizaEstadoReqAlmacen", 2 },
    };

    private sealed class Llamada
    {
        public string Procedimiento;
        public string Sql;
        public IDictionary<string, object> Parametros;
        public bool EnTramo;
    }

    // Consultor falso: registra cada CALL con sus parámetros y responde como la BD de dev.
    private sealed class ConsultorFalso : IConsultor
    {
        private static readonly Regex Patron = new Regex(@"CALL\s+(\w+)", RegexOptions.Compiled);

        private readonly Dictionary<string, int> _contadoresNewId = new Dictionary<string, int>();
        private int _cargas;
        private int _precios;

        public readonly List<Llamada> Llamadas = new List<Llamada>();
        public readonly Dictionary<string, Queue<int>> NewIds = new Dictionary<string, Queue<int>>();
        public readonly Dictionary<string, Exception> Excepciones = new Dictionary<string, Exception>();

        public bool EnTramo;
        public bool SinRequerimiento;
        public bool SinSerie;
        public int TipoReq = 2;
        public int EstadoAntes = 7;
        public int EstadoDespues = 8;
        public string ComentarioDespachoActual;
        public decimal[] Cantidades = { 2m, 1m };
        public decimal[] Precios = { 0.47m, 1.25m };

        public Consulta Consultar(string sql, object parametros = null)
        {
            string procedimiento = Registrar(sql, parametros);
            return Unica(Responder(procedimiento));
        }

        public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
        {
            Registrar(sql, parametros);
            return new ResultadoEjecucion(0, 1);
        }

        public List<string> Nombres()
        {
            return Llamadas.Select(llamada => llamada.Procedimiento).ToList();
        }

        public List<Llamada> De(string procedimiento)
        {
            return Llamadas.Where(llamada => llamada.Procedimiento == procedimiento).ToList();
        }

        private string Registrar(string sql, object parametros)
        {
            Match coincidencia = Patron.Match(sql);
            string procedimiento = coincidencia.Success ? coincidencia.Groups[1].Value : sql;
            Llamadas.Add(new Llamada
            {
                Procedimiento = procedimiento,
                Sql = sql,
                Parametros = parametros as IDictionary<string, object> ?? new Dictionary<string, object>(),
                EnTramo = EnTramo,
            });

            Exception excepcion;
            if (Excepciones.TryGetValue(procedimiento, out excepcion))
            {
                throw excepcion;
            }

            return procedimiento;
        }

        private Dictionary<string, object> Responder(string procedimiento)
        {
            switch (procedimiento)
            {
                case "CargaRequerimientoAlmacen":
                    return Cabecera();
                case "ListadoDetalleRequerimientoAlmacen":
                    return null;
                case "BuscaSeriexDocumento":
                    return SinSerie ? null : Fila("codSerie", 24m, "serie", "001", "numeracion", 12821m);
                case "MuestraUltimoPrecioCompraPorProductoyUnidad":
                    return Fila("ultimo_precio_compra", Precios[_precios++]);
                case "MuestraTransaccion":
                    return Fila("codTransaccion", 15m);
                case "BuscaTipoDocumento":
                    return Fila("codTipoDocumento", 14m);
                case "GuardaTransferencia":
                    return Fila("newid", SiguienteNewId(procedimiento, 700));
                case "GuardaNotaSalida":
                    return Fila("newid", SiguienteNewId(procedimiento, 800));
                case "GuardaNotaIngreso":
                    return Fila("newid", SiguienteNewId(procedimiento, 900));
                case "GuardaDetalleRequerimientoAlmacen":
                case "GuardaDetalleTransferencia":
                case "GuardaDetalleSalida":
                case "GuardaDetalleIngreso":
                    return Fila("newid", SiguienteNewId(procedimiento, 1000));
                default:
                    return null;
            }
        }

        // El listado de líneas devuelve varias filas: lo atiende Consultar con Get().
        private Dictionary<string, object> Cabecera()
        {
            if (SinRequerimiento)
            {
                return null;
            }

            _cargas++;
            int estado = _cargas == 1 ? EstadoAntes : EstadoDespues;
            return Fila(
                "id_req_almacen", CodReq, "cod_tipo_documento", 49, "num_documento", "00003019",
                "cod_serie", 49, "num_serie", "004", "cod_almacen_registro", 1, "cod_user_registro", 78,
                "fecha_registro", new DateTime(2026, 10, 9, 8, 0, 0), "cod_user_modifico", null, "fecha_modifico", null,
                "cod_almacen_solicitante", 4, "cod_almacen_despacho", 3,
                "fecha_requerimiento", new DateTime(2026, 10, 9), "cod_user_anulo", null, "fecha_anulo", null,
                "estado", estado, "comentario_solicitante", "Para obra", "comentario_despacho", ComentarioDespachoActual,
                "tipo_req", TipoReq, "Delivery", 0, "DireccionDelivery", null, "AutorizadoPor", "VICTOR CASTILLO");
        }

        private int SiguienteNewId(string procedimiento, int inicio)
        {
            Queue<int> cola;
            if (NewIds.TryGetValue(procedimiento, out cola) && cola.Count > 0)
            {
                return cola.Dequeue();
            }

            int usados;
            _contadoresNewId.TryGetValue(procedimiento, out usados);
            _contadoresNewId[procedimiento] = usados + 1;
            return inicio + usados;
        }

        public static Dictionary<string, object> Fila(params object[] pares)
        {
            var fila = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < pares.Length; i += 2)
            {
                fila[(string)pares[i]] = pares[i + 1];
            }

            return fila;
        }

        private Consulta Unica(Dictionary<string, object> fila)
        {
            string ultimo = Llamadas[Llamadas.Count - 1].Procedimiento;
            if (ultimo == "ListadoDetalleRequerimientoAlmacen")
            {
                return new Consulta(limite => Lineas());
            }

            return new Consulta(limite =>
            {
                if (fila == null)
                {
                    return new List<Dictionary<string, object>>();
                }

                return new List<Dictionary<string, object>> { fila };
            });
        }

        private List<Dictionary<string, object>> Lineas()
        {
            var lineas = new List<Dictionary<string, object>>();
            int[] productos = { 5072, 5877 };
            int[] codigos = { 13907, 13908 };
            decimal[] pedidas = { 7m, 3m };
            for (int i = 0; i < Cantidades.Length; i++)
            {
                lineas.Add(Fila(
                    "codReqAlm", CodReq, "codDetalle", codigos[i], "codProducto", productos[i],
                    "codUnidad", 55, "cantidad", pedidas[i], "ctdadRequerimiento", Cantidades[i], "ctdadPendiente", 0m));
            }

            return lineas;
        }
    }

    // Misma semántica que Db.Transaccion<T>: confirma si la acción termina sin lanzar y revierte si lanza.
    private sealed class TransaccionFalsa
    {
        private readonly ConsultorFalso _consultor;

        public TransaccionFalsa(ConsultorFalso consultor)
        {
            _consultor = consultor;
        }

        public int Confirmadas;
        public int Revertidas;

        public ResultadoOperacion Ejecutar(Func<IConsultor, ResultadoOperacion> accion)
        {
            _consultor.EnTramo = true;
            try
            {
                ResultadoOperacion resultado = accion(_consultor);
                Confirmadas++;
                return resultado;
            }
            catch
            {
                Revertidas++;
                throw;
            }
            finally
            {
                _consultor.EnTramo = false;
            }
        }
    }

    private sealed class ProgresoFalso : IProgress<PasoOperacion>
    {
        public readonly List<PasoOperacion> Pasos = new List<PasoOperacion>();

        public void Report(PasoOperacion paso)
        {
            Pasos.Add(paso);
        }

        public PasoOperacion Ultimo(string clave)
        {
            return Pasos.LastOrDefault(paso => paso.Clave == clave);
        }
    }

    private static DatosAprobacionRequerimiento Datos(string comentario = "Entregar hoy")
    {
        return new DatosAprobacionRequerimiento(
            CodReq, CodUser, CodAutorizador, comentario, "77", 18.0, new DateTime(2026, 10, 10, 9, 30, 0));
    }

    private static ResultadoOperacion Aprobar(
        ConsultorFalso consultor, DatosAprobacionRequerimiento datos, ProgresoFalso progreso,
        out TransaccionFalsa transaccion, List<string> registrados = null)
    {
        transaccion = new TransaccionFalsa(consultor);
        Action<string> registrar = registrados == null ? (Action<string>)null : registrados.Add;
        return ReqVentaAprobacion.AprobarEn(consultor, transaccion.Ejecutar, datos, progreso, registrar);
    }

    private static string[] ClavesDelCatalogo()
    {
        return ReqVentaTextos.PasosAprobacion().Select(paso => paso.Clave).ToArray();
    }

    private static bool ContieneValor(Llamada llamada, object esperado)
    {
        return llamada.Parametros.Values.Any(valor => Equals(valor, esperado));
    }

    private static bool ContieneDouble(Llamada llamada, double esperado)
    {
        return llamada.Parametros.Values.OfType<double>().Any(valor => Math.Abs(valor - esperado) < 1e-9);
    }

    // Catálogo de pasos

    [Fact]
    public void PasosAprobacion_TieneOchoPasosEnOrdenYTodosPendientes()
    {
        IReadOnlyList<PasoOperacion> pasos = ReqVentaTextos.PasosAprobacion();

        Assert.Equal(8, pasos.Count);
        Assert.Equal(8, pasos.Select(paso => paso.Clave).Distinct().Count());
        Assert.All(pasos, paso => Assert.Equal(EstadoPaso.Pendiente, paso.Estado));
        Assert.All(pasos, paso => Assert.False(string.IsNullOrWhiteSpace(paso.Texto)));
    }

    [Fact]
    public void PasosAprobacion_TextosSinJergaYConVocabularioDeLasPantallas()
    {
        string[] prohibidos = { "CALL", "Guarda", "req_almacen", "productoalmacen", "detallenota", "newid" };
        IReadOnlyList<PasoOperacion> pasos = ReqVentaTextos.PasosAprobacion();

        foreach (PasoOperacion paso in pasos)
        {
            foreach (string termino in prohibidos)
            {
                Assert.False(
                    paso.Texto.IndexOf(termino, StringComparison.OrdinalIgnoreCase) >= 0,
                    "El texto '" + paso.Texto + "' contiene jerga técnica: " + termino + ".");
            }
        }

        string[] textos = pasos.Select(paso => paso.Texto).ToArray();
        Assert.Contains("transferencia", textos[0], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("requerimiento", textos[1], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("stock", textos[2], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("transferencia", textos[3], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("nota de salida", textos[4], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("nota de ingreso", textos[5], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("transferencia", textos[6], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("requerimiento", textos[7], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PasosAprobacion_CadaLlamadaDevuelveUnaListaNueva()
    {
        Assert.NotSame(ReqVentaTextos.PasosAprobacion(), ReqVentaTextos.PasosAprobacion());
    }

    // Orden de procedimientos y de progreso

    [Fact]
    public void AprobarEn_Exito_EjecutaLosProcedimientosEnElOrdenDelLegacy()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.Equal(OrdenEsperado, consultor.Nombres().ToArray());
    }

    [Fact]
    public void AprobarEn_Exito_InformaCadaPasoEnCursoYListoEnElOrdenDelCatalogo()
    {
        var consultor = new ConsultorFalso();
        var progreso = new ProgresoFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), progreso, out transaccion);

        Assert.True(resultado.Ok, resultado.Mensaje);
        string[] claves = ClavesDelCatalogo();
        var esperado = new List<string>();
        foreach (string clave in claves)
        {
            esperado.Add(clave + ":EnCurso");
            esperado.Add(clave + ":Listo");
        }

        Assert.Equal(esperado, progreso.Pasos.Select(paso => paso.Clave + ":" + paso.Estado).ToList());
    }

    [Fact]
    public void AprobarEn_ProgresoNulo_NoLanzaYAprueba()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), null, out transaccion);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.Equal(OrdenEsperado.Length, consultor.Llamadas.Count);
    }

    [Fact]
    public void AprobarEn_Exito_SoloLosTresTramosCorrenEnTransaccionYTodosConfirman()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        Assert.Equal(3, transaccion.Confirmadas);
        Assert.Equal(0, transaccion.Revertidas);
        string[] enTramo =
        {
            "ActualizaRequerimientoAlmacen", "EliminaDetalleRequerimientoAlmacen", "GuardaDetalleRequerimientoAlmacen",
            "GuardaNotaSalida", "GuardaDetalleSalida", "GuardaNotaIngreso", "GuardaDetalleIngreso",
        };
        foreach (Llamada llamada in consultor.Llamadas)
        {
            Assert.Equal(enTramo.Contains(llamada.Procedimiento), llamada.EnTramo);
        }
    }

    // Fallos: la nota de salida corta el proceso (decisión del usuario 2026-10-10)

    [Fact]
    public void AprobarEn_DetalleDeSalidaSinNewId_CortaYNoRegistraIngresoNiApruebaTransferencia()
    {
        var consultor = new ConsultorFalso();
        consultor.NewIds["GuardaDetalleSalida"] = new Queue<int>(new[] { 1001, 0 });
        var progreso = new ProgresoFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), progreso, out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains("No hay stock suficiente del producto", resultado.Mensaje);
        Assert.Contains("5877", resultado.Mensaje);
        Assert.Empty(consultor.De("GuardaNotaIngreso"));
        Assert.Empty(consultor.De("GuardaDetalleIngreso"));
        Assert.Empty(consultor.De("AprobarTransferencia"));
        Assert.Empty(consultor.De("ActualizaCantidadPendienteReqAlmacen"));
        Assert.Empty(consultor.De("ActualizaEstadoReqAlmacen"));
        Assert.Equal(1, transaccion.Confirmadas);
        Assert.Equal(1, transaccion.Revertidas);

        string[] claves = ClavesDelCatalogo();
        PasoOperacion salida = progreso.Ultimo(claves[4]);
        Assert.Equal(EstadoPaso.Error, salida.Estado);
        Assert.Contains("No hay stock suficiente", salida.Detalle);
        for (int i = 5; i < claves.Length; i++)
        {
            Assert.Null(progreso.Ultimo(claves[i]));
        }

        for (int i = 0; i < 4; i++)
        {
            Assert.Equal(EstadoPaso.Listo, progreso.Ultimo(claves[i]).Estado);
        }
    }

    [Fact]
    public void AprobarEn_CabeceraDeSalidaSinNewId_CortaConMensajeClaro()
    {
        var consultor = new ConsultorFalso();
        consultor.NewIds["GuardaNotaSalida"] = new Queue<int>(new[] { 0 });
        var progreso = new ProgresoFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), progreso, out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains("nota de salida", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(consultor.De("GuardaDetalleSalida"));
        Assert.Empty(consultor.De("GuardaNotaIngreso"));
        Assert.Empty(consultor.De("AprobarTransferencia"));
        Assert.Equal(EstadoPaso.Error, progreso.Ultimo(ClavesDelCatalogo()[4]).Estado);
    }

    [Fact]
    public void AprobarEn_CabeceraDeIngresoSinNewId_CortaYNoApruebaTransferencia()
    {
        var consultor = new ConsultorFalso();
        consultor.NewIds["GuardaNotaIngreso"] = new Queue<int>(new[] { 0 });
        var progreso = new ProgresoFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), progreso, out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains("nota de ingreso", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(consultor.De("AprobarTransferencia"));
        Assert.Empty(consultor.De("ActualizaEstadoReqAlmacen"));
        Assert.Equal(EstadoPaso.Error, progreso.Ultimo(ClavesDelCatalogo()[5]).Estado);
        Assert.Null(progreso.Ultimo(ClavesDelCatalogo()[6]));
    }

    [Fact]
    public void AprobarEn_ExcepcionEnUnPaso_MarcaElPasoEnErrorConLaCausaYDejaLosSiguientesPendientes()
    {
        var consultor = new ConsultorFalso();
        consultor.Excepciones["SeparandoStockAlAprobarReqAlmacen"] = new InvalidOperationException("fallo bd");
        var progreso = new ProgresoFalso();
        var registrados = new List<string>();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), progreso, out transaccion, registrados);

        Assert.False(resultado.Ok);
        Assert.Contains("fallo bd", resultado.Mensaje);
        string[] claves = ClavesDelCatalogo();
        PasoOperacion separar = progreso.Ultimo(claves[2]);
        Assert.Equal(EstadoPaso.Error, separar.Estado);
        Assert.Contains("fallo bd", separar.Detalle);
        for (int i = 3; i < claves.Length; i++)
        {
            Assert.Null(progreso.Ultimo(claves[i]));
        }

        Assert.Empty(consultor.De("GuardaTransferencia"));
        Assert.Single(registrados);
        Assert.Contains("fallo bd", registrados[0]);
        Assert.Contains(CodReq.ToString(), registrados[0]);
    }

    [Fact]
    public void AprobarEn_ExcepcionDentroDeUnTramo_RevierteElTramoYDevuelveFallo()
    {
        var consultor = new ConsultorFalso();
        consultor.Excepciones["EliminaDetalleRequerimientoAlmacen"] = new InvalidOperationException("fallo al borrar");
        var progreso = new ProgresoFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), progreso, out transaccion);

        Assert.False(resultado.Ok);
        Assert.Equal(1, transaccion.Revertidas);
        Assert.Equal(0, transaccion.Confirmadas);
        Assert.Equal(EstadoPaso.Error, progreso.Ultimo(ClavesDelCatalogo()[1]).Estado);
        Assert.Empty(consultor.De("SeparandoStockAlAprobarReqAlmacen"));
    }

    // Validaciones del paso 1: no se escribe nada

    [Fact]
    public void AprobarEn_DatosNulos_DevuelveFalloSinLlamadas()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, null, new ProgresoFalso(), out transaccion);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
        Assert.Empty(consultor.Llamadas);
    }

    [Fact]
    public void AprobarEn_ConsultorNulo_DevuelveFalloSinLanzar()
    {
        ResultadoOperacion resultado = ReqVentaAprobacion.AprobarEn(
            null, accion => new ResultadoOperacion(true, string.Empty), Datos(), null, null);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    [Fact]
    public void AprobarEn_SinAutorizador_PideDefinirloYNoEscribe()
    {
        var consultor = new ConsultorFalso();
        var progreso = new ProgresoFalso();
        TransaccionFalsa transaccion;
        var datos = new DatosAprobacionRequerimiento(CodReq, CodUser, 0, "x", "77", 18.0, DateTime.Now);

        ResultadoOperacion resultado = Aprobar(consultor, datos, progreso, out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains("autorizador", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(consultor.Llamadas);
        Assert.Equal(EstadoPaso.Error, progreso.Ultimo(ClavesDelCatalogo()[0]).Estado);
    }

    [Fact]
    public void AprobarEn_SinSerieDeTransferencia_FallaEnElPrimerPasoSinEscribir()
    {
        var consultor = new ConsultorFalso { SinSerie = true };
        var progreso = new ProgresoFalso();
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), progreso, out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains("serie", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(consultor.De("AprobarRequerimientoAlmacen"));
        Assert.Empty(consultor.De("GuardaTransferencia"));
        Assert.Equal(EstadoPaso.Error, progreso.Ultimo(ClavesDelCatalogo()[0]).Estado);
        Assert.Null(progreso.Ultimo(ClavesDelCatalogo()[1]));
    }

    [Fact]
    public void AprobarEn_RequerimientoInexistente_Falla()
    {
        var consultor = new ConsultorFalso { SinRequerimiento = true };
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains(CodReq.ToString(), resultado.Mensaje);
        Assert.Empty(consultor.De("AprobarRequerimientoAlmacen"));
    }

    [Fact]
    public void AprobarEn_RequerimientoQueNoEsDeVenta_Falla()
    {
        var consultor = new ConsultorFalso { TipoReq = 1 };
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        Assert.False(resultado.Ok);
        Assert.Empty(consultor.De("AprobarRequerimientoAlmacen"));
    }

    [Fact]
    public void AprobarEn_RequerimientoQueYaNoEstaPendiente_FallaSinEscribir()
    {
        var consultor = new ConsultorFalso { EstadoAntes = ReqVentaReglas.AprobadoTransferido };
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains("pendiente", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(consultor.De("AprobarRequerimientoAlmacen"));
        Assert.Empty(consultor.De("GuardaTransferencia"));
    }

    [Fact]
    public void AprobarEn_TodasLasCantidadesEnCero_FallaSinEscribir()
    {
        var consultor = new ConsultorFalso { Cantidades = new[] { 0m, 0m } };
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        Assert.False(resultado.Ok);
        Assert.Contains("cantidad", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(consultor.De("AprobarRequerimientoAlmacen"));
    }

    // Datos que llegan a los CALL

    [Fact]
    public void AprobarEn_EnviaLosDatosDelDtoALosProcedimientos()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;
        DatosAprobacionRequerimiento datos = Datos("Entregar hoy");

        ResultadoOperacion resultado = Aprobar(consultor, datos, new ProgresoFalso(), out transaccion);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.True(ContieneValor(consultor.De("AprobarRequerimientoAlmacen")[0], CodUser));
        Assert.True(ContieneValor(consultor.De("SetAutorizadorEnRequerimientoAlmacen")[0], CodAutorizador));
        Assert.True(ContieneValor(consultor.De("ActualizaRequerimientoAlmacen")[0], "Entregar hoy"));
        Assert.True(ContieneValor(consultor.De("ActualizaRequerimientoAlmacen")[0], 8));
        Assert.True(ContieneValor(consultor.De("GuardaTransferencia")[0], "Req Almacen para Ventas generado de O.V: 77"));
        Assert.True(ContieneValor(consultor.De("GuardaTransferencia")[0], CodReq));
        Assert.True(ContieneValor(consultor.De("GuardaTransferencia")[0], CodUser));
        Assert.True(ContieneValor(consultor.De("GuardaTransferencia")[0], "012821"));
        Assert.True(ContieneValor(consultor.De("RegistrarTransferenciaRequerimientoAlmacen")[0], 700));
        Assert.True(ContieneValor(consultor.De("RegistrarTransferenciaRequerimientoAlmacen")[0], CodUser));
        Assert.True(ContieneValor(consultor.De("GuardaNotaSalida")[0], 700));
        Assert.True(ContieneValor(consultor.De("GuardaNotaIngreso")[0], 700));
        Assert.True(ContieneValor(consultor.De("AprobarTransferencia")[0], 700));
        Assert.True(ContieneValor(consultor.De("GuardaDetalleIngreso")[0], datos.FechaIngreso));
        Assert.True(ContieneValor(consultor.De("GuardaDetalleIngreso")[0], 900));
        Assert.True(ContieneValor(consultor.De("GuardaDetalleSalida")[0], 800));
        Assert.True(ContieneValor(consultor.De("ActualizaEstadoReqAlmacen")[0], ReqVentaReglas.AprobadoTransferido));
        Assert.True(ContieneValor(consultor.De("ActualizaCantidadPendienteReqAlmacen")[0], CodReq));
    }

    [Fact]
    public void AprobarEn_ComentarioDespacho_SeAgregaAlQueYaTieneElRequerimiento()
    {
        var consultor = new ConsultorFalso { ComentarioDespachoActual = "Previo" };
        TransaccionFalsa transaccion;

        Aprobar(consultor, Datos("Entregar hoy"), new ProgresoFalso(), out transaccion);

        Assert.True(ContieneValor(consultor.De("ActualizaRequerimientoAlmacen")[0], "Previo\nEntregar hoy"));
    }

    [Fact]
    public void AprobarEn_ComentarioVacio_ConservaElQueTeniaElRequerimiento()
    {
        var consultor = new ConsultorFalso { ComentarioDespachoActual = "Previo" };
        TransaccionFalsa transaccion;

        Aprobar(consultor, Datos("   "), new ProgresoFalso(), out transaccion);

        Assert.True(ContieneValor(consultor.De("ActualizaRequerimientoAlmacen")[0], "Previo"));
    }

    [Fact]
    public void AprobarEn_CalculaMontosDeLaTransferenciaConElUltimoPrecioDeCompraYElIgv()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        double subtotal1 = 0.47 * 2;
        double igv1 = subtotal1 - (subtotal1 / (18.0 / 100.0 + 1.0));
        List<Llamada> detalles = consultor.De("GuardaDetalleTransferencia");
        Assert.True(ContieneDouble(detalles[0], subtotal1));
        Assert.True(ContieneDouble(detalles[0], igv1));
        Assert.True(ContieneDouble(detalles[0], 0.47));
        Assert.True(ContieneValor(detalles[0], 13907));
        Assert.True(ContieneValor(detalles[1], 13908));
        Assert.True(ContieneValor(consultor.De("GuardaTransferencia")[0], 2.19m));
        Assert.True(ContieneValor(consultor.De("SeparandoStockAlAprobarReqAlmacen")[0], 2m));
    }

    [Fact]
    public void AprobarEn_LineaConCantidadCero_SeSeparaYSeActualizaPeroNoViajaEnLaTransferencia()
    {
        var consultor = new ConsultorFalso { Cantidades = new[] { 2m, 0m } };
        TransaccionFalsa transaccion;

        ResultadoOperacion resultado = Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.Equal(2, consultor.De("GuardaDetalleRequerimientoAlmacen").Count);
        Assert.Equal(2, consultor.De("SeparandoStockAlAprobarReqAlmacen").Count);
        Assert.Single(consultor.De("MuestraUltimoPrecioCompraPorProductoyUnidad"));
        Assert.Single(consultor.De("GuardaDetalleTransferencia"));
        Assert.Single(consultor.De("GuardaDetalleSalida"));
        Assert.Single(consultor.De("GuardaDetalleIngreso"));
    }

    [Fact]
    public void AprobarEn_CantidadesDelDto_ReemplazanLasDeLaBaseParaEsaLinea()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;
        DatosAprobacionRequerimiento datos = Datos();
        datos.CantidadesADespachar = new Dictionary<int, decimal> { { 13907, 1.5m } };

        ResultadoOperacion resultado = Aprobar(consultor, datos, new ProgresoFalso(), out transaccion);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.True(ContieneValor(consultor.De("SeparandoStockAlAprobarReqAlmacen")[0], 1.5m));
        Assert.True(ContieneValor(consultor.De("SeparandoStockAlAprobarReqAlmacen")[1], 1m));
        Assert.True(ContieneValor(consultor.De("GuardaDetalleRequerimientoAlmacen")[0], 1.5m));
    }

    // Regla de oro: cada CALL posicional lleva la cantidad real de argumentos y ningún parámetro sin valor

    [Fact]
    public void AprobarEn_CadaCallTieneLaCantidadDeArgumentosDeLaFirmaReal()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        foreach (Llamada llamada in consultor.Llamadas)
        {
            int esperados;
            Assert.True(ArgumentosReales.TryGetValue(llamada.Procedimiento, out esperados), "Procedimiento sin firma conocida: " + llamada.Procedimiento);
            Assert.Equal(esperados, ContarArgumentos(llamada.Sql, llamada.Procedimiento));
        }
    }

    [Fact]
    public void AprobarEn_TodoParametroDelSqlTieneSuValor()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        foreach (Llamada llamada in consultor.Llamadas)
        {
            foreach (Match parametro in Regex.Matches(llamada.Sql, @"@(\w+)"))
            {
                string nombre = parametro.Groups[1].Value;
                if (nombre.Equals("newid", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                Assert.True(
                    llamada.Parametros.Keys.Any(clave => clave.Equals(nombre, StringComparison.OrdinalIgnoreCase)),
                    llamada.Procedimiento + " usa @" + nombre + " sin valor.");
            }
        }
    }

    [Fact]
    public void AprobarEn_LosCallConSalidaUsanSetNewIdYSelectNewId()
    {
        var consultor = new ConsultorFalso();
        TransaccionFalsa transaccion;

        Aprobar(consultor, Datos(), new ProgresoFalso(), out transaccion);

        string[] conSalida =
        {
            "GuardaDetalleRequerimientoAlmacen", "GuardaTransferencia", "GuardaDetalleTransferencia",
            "GuardaNotaSalida", "GuardaDetalleSalida", "GuardaNotaIngreso", "GuardaDetalleIngreso",
        };
        foreach (string procedimiento in conSalida)
        {
            foreach (Llamada llamada in consultor.De(procedimiento))
            {
                Assert.Contains("SET @newid = 0;", llamada.Sql);
                Assert.Contains("SELECT @newid AS newid;", llamada.Sql);
            }
        }
    }

    // Cuenta los argumentos del CALL de primer nivel (las comas dentro de NOW() o de textos no cuentan).
    private static int ContarArgumentos(string sql, string procedimiento)
    {
        int inicio = sql.IndexOf("CALL " + procedimiento + "(", StringComparison.Ordinal) + ("CALL " + procedimiento + "(").Length;
        int profundidad = 1;
        int argumentos = 1;
        bool enTexto = false;
        for (int i = inicio; i < sql.Length && profundidad > 0; i++)
        {
            char c = sql[i];
            if (c == '\'')
            {
                enTexto = !enTexto;
            }
            else if (!enTexto && c == '(')
            {
                profundidad++;
            }
            else if (!enTexto && c == ')')
            {
                profundidad--;
            }
            else if (!enTexto && profundidad == 1 && c == ',')
            {
                argumentos++;
            }
        }

        return argumentos;
    }
}
