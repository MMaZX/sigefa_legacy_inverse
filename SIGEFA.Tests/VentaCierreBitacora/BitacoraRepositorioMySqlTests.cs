using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using SIGEFA.Administradores.VentaCierreBitacora;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.VentaCierreBitacora;

// Pruebas unitarias del repositorio MySQL de la bitácora (T3). Un consultor falso graba cada
// sentencia y sus parámetros; ninguna prueba toca la BD. Los nombres de columna se contrastan
// con el DDL de la migración 2026_10_10_130000_create_venta_cierre_log_tables.
public class BitacoraRepositorioMySqlTests
{
    private const string ColumnasLog =
        "cod_pedido, intento, cod_usuario, usuario, equipo, version_app, estado, total_bloques, inicio";

    private const string ColumnasUpdate =
        "estado, bloques_ok, fin, duracion_ms, error_paso, error_procedimiento, error_mysql_num, " +
        "error_sqlstate, error_mensaje, cod_factura_venta";

    private const string ColumnasEvento =
        "log_id, orden, hora, bloque, almacen, paso, item, cod_producto, resultado, duracion_ms, detalle";

    private sealed class Llamada
    {
        public string Sql;
        public object Parametros;

        public bool Es(string inicio)
        {
            return Sql.StartsWith(inicio, StringComparison.Ordinal);
        }

        public List<MySqlParameter> Lista()
        {
            return ParametrosSql.Desde(Parametros);
        }

        public object Valor(string nombre)
        {
            return Lista().Single(p => p.ParameterName == "@" + nombre).Value;
        }
    }

    // Graba todo; los errores y los ids se programan por tipo de sentencia.
    private sealed class ConsultorFalso : IConsultor
    {
        public readonly List<Llamada> Escrituras = new List<Llamada>();
        public readonly List<Llamada> Lecturas = new List<Llamada>();
        public readonly Queue<Exception> ErroresAlCrearIntento = new Queue<Exception>();
        public Exception ErrorAlInsertarEventos;
        public Exception ErrorAlActualizar;
        public long IdGenerado = 77;
        public object IntentoLeido = 3;

        public Consulta Consultar(string sql, object parametros = null)
        {
            Lecturas.Add(new Llamada { Sql = Normalizar(sql), Parametros = parametros });
            return new Consulta(limite =>
            {
                var fila = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                if (IntentoLeido != null)
                {
                    fila["intento"] = IntentoLeido;
                }
                return IntentoLeido == null
                    ? new List<Dictionary<string, object>>()
                    : new List<Dictionary<string, object>> { fila };
            });
        }

        public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
        {
            var llamada = new Llamada { Sql = Normalizar(sql), Parametros = parametros };
            Escrituras.Add(llamada);

            if (llamada.Es("INSERT INTO venta_cierre_log (") && ErroresAlCrearIntento.Count > 0)
            {
                throw ErroresAlCrearIntento.Dequeue();
            }
            if (llamada.Es("UPDATE") && ErrorAlActualizar != null)
            {
                throw ErrorAlActualizar;
            }
            if (llamada.Es("INSERT INTO venta_cierre_log_evento") && ErrorAlInsertarEventos != null)
            {
                throw ErrorAlInsertarEventos;
            }
            return new ResultadoEjecucion(IdGenerado, 1);
        }

        public List<Llamada> InsertsDeCabecera()
        {
            return Escrituras.Where(l => l.Es("INSERT INTO venta_cierre_log (")).ToList();
        }

        public List<Llamada> InsertsDeEventos()
        {
            return Escrituras.Where(l => l.Es("INSERT INTO venta_cierre_log_evento")).ToList();
        }

        public List<Llamada> Updates()
        {
            return Escrituras.Where(l => l.Es("UPDATE")).ToList();
        }
    }

    private static string Normalizar(string sql)
    {
        return Regex.Replace(sql.Trim(), @"\s+", " ");
    }

    // MySqlException no tiene constructor público estable entre versiones: se crea por reflexión.
    private static MySqlException ErrorMySql(int numero)
    {
        const BindingFlags todos = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        foreach (var constructor in typeof(MySqlException).GetConstructors(todos))
        {
            var tipos = constructor.GetParameters().Select(p => p.ParameterType).ToArray();
            if (tipos.Length == 2 && tipos[0] == typeof(string) && tipos[1] == typeof(int))
            {
                return (MySqlException)constructor.Invoke(new object[] { "error simulado", numero });
            }
            if (tipos.Length == 3 && tipos[0] == typeof(string) && tipos[1] == typeof(int) && tipos[2] == typeof(Exception))
            {
                return (MySqlException)constructor.Invoke(new object[] { "error simulado", numero, null });
            }
        }
        throw new InvalidOperationException("MySqlException sin constructor (string, int) utilizable.");
    }

    private static List<EventoBitacora> Eventos(int cantidad)
    {
        return Enumerable.Range(1, cantidad)
            .Select(i => DatosBitacora.Evento(i, "guardarDetalle", ResultadoEvento.Ok))
            .ToList();
    }

    // CrearIntento

    [Fact]
    public void CrearIntento_EmiteUnSoloInsertSelectSobreLaMismaTabla()
    {
        var consultor = new ConsultorFalso();
        long logId;

        new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId);

        var insert = Assert.Single(consultor.InsertsDeCabecera());
        Assert.Equal(
            "INSERT INTO venta_cierre_log (" + ColumnasLog + ") " +
            "SELECT @codPedido, COALESCE(MAX(intento), 0) + 1, @codUsuario, @usuario, @equipo, @versionApp, " +
            "'EN_CURSO', @totalBloques, @inicio FROM venta_cierre_log WHERE cod_pedido = @codPedido",
            insert.Sql);
    }

    [Fact]
    public void CrearIntento_PasaLosParametrosEnElOrdenDeLaSentencia()
    {
        var consultor = new ConsultorFalso();
        long logId;

        new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(codPedido: 9, totalBloques: 4), out logId);

        var insert = consultor.InsertsDeCabecera().Single();
        Assert.Equal(
            new[] { "@codPedido", "@codUsuario", "@usuario", "@equipo", "@versionApp", "@totalBloques", "@inicio" },
            insert.Lista().Select(p => p.ParameterName).ToArray());
        Assert.Equal(9, insert.Valor("codPedido"));
        Assert.Equal(15, insert.Valor("codUsuario"));
        Assert.Equal("jperez", insert.Valor("usuario"));
        Assert.Equal("PC-01", insert.Valor("equipo"));
        Assert.Equal("1.0.0.0", insert.Valor("versionApp"));
        Assert.Equal(4, insert.Valor("totalBloques"));
        Assert.Equal(new DateTime(2026, 10, 10, 14, 3, 22, 123), insert.Valor("inicio"));
    }

    [Fact]
    public void CrearIntento_UsuarioNuloYCadenasVaciasVanComoDbNull()
    {
        var consultor = new ConsultorFalso();
        var intento = new IntentoBitacora(7, null, string.Empty, string.Empty, string.Empty, 1, DateTime.Now);
        long logId;

        new BitacoraRepositorioMySql(consultor).CrearIntento(intento, out logId);

        var insert = consultor.InsertsDeCabecera().Single();
        Assert.Same(DBNull.Value, insert.Valor("codUsuario"));
        Assert.Same(DBNull.Value, insert.Valor("usuario"));
        Assert.Same(DBNull.Value, insert.Valor("equipo"));
        Assert.Same(DBNull.Value, insert.Valor("versionApp"));
    }

    [Fact]
    public void CrearIntento_DevuelveElIdDeLastInsertedIdYLeeElIntentoPorId()
    {
        var consultor = new ConsultorFalso { IdGenerado = 4321, IntentoLeido = 5L };
        long logId;

        int intento = new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId);

        Assert.Equal(4321L, logId);
        Assert.Equal(5, intento);
        var lectura = Assert.Single(consultor.Lecturas);
        Assert.Equal("SELECT intento FROM venta_cierre_log WHERE id = @id", lectura.Sql);
        Assert.Equal(4321L, lectura.Lista().Single(p => p.ParameterName == "@id").Value);
    }

    [Fact]
    public void CrearIntento_ReintentaConLaMismaSentenciaAnteElError1062()
    {
        var consultor = new ConsultorFalso();
        consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1062));
        consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1062));
        long logId;

        new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId);

        var inserts = consultor.InsertsDeCabecera();
        Assert.Equal(3, inserts.Count);
        Assert.Equal(inserts[0].Sql, inserts[1].Sql);
        Assert.Equal(inserts[0].Sql, inserts[2].Sql);
        Assert.Equal(77L, logId);
    }

    [Fact]
    public void CrearIntento_ReintentaConLaMismaSentenciaAnteElDeadlock1213()
    {
        var consultor = new ConsultorFalso();
        consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1213));
        consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1062));
        long logId;

        new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId);

        var inserts = consultor.InsertsDeCabecera();
        Assert.Equal(3, inserts.Count);
        Assert.Equal(inserts[0].Sql, inserts[2].Sql);
        Assert.Equal(77L, logId);
    }

    [Fact]
    public void CrearIntento_ElTimeoutDeBloqueo1205SePropagaSinReintentar()
    {
        var consultor = new ConsultorFalso();
        consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1205));
        long logId;

        var ex = Assert.Throws<MySqlException>(
            () => new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId));

        Assert.Equal(1205, ex.Number);
        Assert.Single(consultor.InsertsDeCabecera());
    }

    [Fact]
    public void CrearIntento_TieneCincoIntentosEnTotalYLuegoPropagaElError1062()
    {
        var consultor = new ConsultorFalso();
        for (int i = 0; i < 10; i++)
        {
            consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1062));
        }
        long logId;

        var ex = Assert.Throws<MySqlException>(
            () => new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId));

        Assert.Equal(1062, ex.Number);
        Assert.Equal(5, consultor.InsertsDeCabecera().Count);
        Assert.Empty(consultor.Lecturas);
    }

    [Fact]
    public void CrearIntento_ConElCuartoReintentoExitosoNoLanza()
    {
        var consultor = new ConsultorFalso();
        for (int i = 0; i < 4; i++)
        {
            consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1062));
        }
        long logId;

        new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId);

        Assert.Equal(5, consultor.InsertsDeCabecera().Count);
    }

    [Fact]
    public void CrearIntento_OtroErrorDeMySqlSePropagaSinReintentar()
    {
        var consultor = new ConsultorFalso();
        consultor.ErroresAlCrearIntento.Enqueue(ErrorMySql(1146));
        long logId;

        var ex = Assert.Throws<MySqlException>(
            () => new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId));

        Assert.Equal(1146, ex.Number);
        Assert.Single(consultor.InsertsDeCabecera());
    }

    [Fact]
    public void CrearIntento_UnaExcepcionQueNoEsDeMySqlSePropagaSinReintentar()
    {
        var consultor = new ConsultorFalso();
        consultor.ErroresAlCrearIntento.Enqueue(new InvalidOperationException("sin red"));
        long logId;

        Assert.Throws<InvalidOperationException>(
            () => new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId));

        Assert.Single(consultor.InsertsDeCabecera());
    }

    [Fact]
    public void CrearIntento_SiNoSeEncuentraLaFilaCreadaLanza()
    {
        var consultor = new ConsultorFalso { IntentoLeido = null };
        long logId;

        Assert.Throws<InvalidOperationException>(
            () => new BitacoraRepositorioMySql(consultor).CrearIntento(DatosBitacora.Intento(), out logId));
    }

    [Fact]
    public void Constructor_SinConsultorLanza()
    {
        Assert.Throws<ArgumentNullException>(() => new BitacoraRepositorioMySql(null));
    }

    // Finalizar: UPDATE de la cabecera

    [Fact]
    public void Finalizar_ActualizaLaCabeceraConTodasLasColumnasDelDdl()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Error("mensaje"), new List<EventoBitacora>());

        var update = Assert.Single(consultor.Updates());
        Assert.Equal(
            "UPDATE venta_cierre_log SET estado = @estado, bloques_ok = @bloquesOk, fin = @fin, " +
            "duracion_ms = @duracionMs, error_paso = @errorPaso, error_procedimiento = @errorProcedimiento, " +
            "error_mysql_num = @errorMysqlNum, error_sqlstate = @errorSqlState, error_mensaje = @errorMensaje, " +
            "cod_factura_venta = @codFacturaVenta WHERE id = @id",
            update.Sql);
        Assert.Equal(
            new[]
            {
                "@estado", "@bloquesOk", "@fin", "@duracionMs", "@errorPaso", "@errorProcedimiento",
                "@errorMysqlNum", "@errorSqlState", "@errorMensaje", "@codFacturaVenta", "@id",
            },
            update.Lista().Select(p => p.ParameterName).ToArray());
        Assert.Equal("ERROR", update.Valor("estado"));
        Assert.Equal(1, update.Valor("bloquesOk"));
        Assert.Equal(new DateTime(2026, 10, 10, 14, 3, 25, 789), update.Valor("fin"));
        Assert.Equal(3666, update.Valor("duracionMs"));
        Assert.Equal("guardarDetalle", update.Valor("errorPaso"));
        Assert.Equal("GuardaDetalleFacturaVenta", update.Valor("errorProcedimiento"));
        Assert.Equal(1062, update.Valor("errorMysqlNum"));
        Assert.Equal("23000", update.Valor("errorSqlState"));
        Assert.Equal("mensaje", update.Valor("errorMensaje"));
        Assert.Same(DBNull.Value, update.Valor("codFacturaVenta"));
        Assert.Equal(55L, update.Valor("id"));
    }

    [Fact]
    public void Finalizar_ResultadoOkEscribeOkYDejaLosCamposDeErrorEnNull()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), new List<EventoBitacora>());

        var update = consultor.Updates().Single();
        Assert.Equal("OK", update.Valor("estado"));
        Assert.Equal(555, update.Valor("codFacturaVenta"));
        Assert.Same(DBNull.Value, update.Valor("errorPaso"));
        Assert.Same(DBNull.Value, update.Valor("errorProcedimiento"));
        Assert.Same(DBNull.Value, update.Valor("errorMysqlNum"));
        Assert.Same(DBNull.Value, update.Valor("errorSqlState"));
        Assert.Same(DBNull.Value, update.Valor("errorMensaje"));
    }

    // Finalizar: INSERT múltiple de eventos

    [Fact]
    public void Finalizar_SinEventosNoEmiteInsertDeEventos()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), new List<EventoBitacora>());

        Assert.Single(consultor.Updates());
        Assert.Empty(consultor.InsertsDeEventos());
    }

    [Fact]
    public void Finalizar_ConEventosNullTratadoComoVacio()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), null);

        Assert.Single(consultor.Updates());
        Assert.Empty(consultor.InsertsDeEventos());
    }

    [Fact]
    public void Finalizar_EventosCabenEnUnaSentenciaConUnaFilaPorEvento()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), Eventos(3));

        var insert = Assert.Single(consultor.InsertsDeEventos());
        string fila(int i)
        {
            return "(@logId, @orden" + i + ", @hora" + i + ", @bloque" + i + ", @almacen" + i + ", @paso" + i +
                ", @item" + i + ", @codProducto" + i + ", @resultado" + i + ", @duracionMs" + i + ", @detalle" + i + ")";
        }
        Assert.Equal(
            "INSERT INTO venta_cierre_log_evento (" + ColumnasEvento + ") VALUES " +
            fila(0) + ", " + fila(1) + ", " + fila(2),
            insert.Sql);
        Assert.Equal(1 + 3 * 10, insert.Lista().Count);
    }

    [Fact]
    public void Finalizar_ConDoscientosEventosEmiteUnaSolaSentencia()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), Eventos(200));

        Assert.Single(consultor.InsertsDeEventos());
    }

    [Fact]
    public void Finalizar_CuatrocientosCincuentaEventosVanEnTresSentenciasDe200_200_50()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), Eventos(450));

        var inserts = consultor.InsertsDeEventos();
        Assert.Equal(3, inserts.Count);
        Assert.Equal(new[] { 200, 200, 50 }, inserts.Select(i => Regex.Matches(i.Sql, @"\(@logId").Count).ToArray());
        // El orden de los eventos se conserva a lo largo de los lotes.
        Assert.Equal(new[] { 1, 201, 401 }, inserts.Select(i => (int)i.Valor("orden0")).ToArray());
    }

    [Fact]
    public void Finalizar_ElUpdateSeEjecutaAntesQueLosEventos()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), Eventos(2));

        Assert.True(consultor.Escrituras[0].Es("UPDATE"));
        Assert.True(consultor.Escrituras[1].Es("INSERT INTO venta_cierre_log_evento"));
    }

    [Fact]
    public void Finalizar_EscribeCadaColumnaDelEventoYNulosComoDbNull()
    {
        var consultor = new ConsultorFalso();
        var completo = new EventoBitacora(
            4, new DateTime(2026, 10, 10, 14, 3, 22, 456), 2, "ALMACEN CENTRAL", "guardarDetalle",
            3, 901, ResultadoEvento.Error, 12, "falló el ítem");
        var vacio = new EventoBitacora(
            5, new DateTime(2026, 10, 10, 14, 3, 23, 1), null, null, "confirmar",
            null, null, ResultadoEvento.Info, null, null);

        new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), new List<EventoBitacora> { completo, vacio });

        var insert = consultor.InsertsDeEventos().Single();
        Assert.Equal(55L, insert.Valor("logId"));
        Assert.Equal(4, insert.Valor("orden0"));
        Assert.Equal(new DateTime(2026, 10, 10, 14, 3, 22, 456), insert.Valor("hora0"));
        Assert.Equal(2, insert.Valor("bloque0"));
        Assert.Equal("ALMACEN CENTRAL", insert.Valor("almacen0"));
        Assert.Equal("guardarDetalle", insert.Valor("paso0"));
        Assert.Equal(3, insert.Valor("item0"));
        Assert.Equal(901, insert.Valor("codProducto0"));
        Assert.Equal("ERROR", insert.Valor("resultado0"));
        Assert.Equal(12, insert.Valor("duracionMs0"));
        Assert.Equal("falló el ítem", insert.Valor("detalle0"));

        Assert.Equal(5, insert.Valor("orden1"));
        Assert.Same(DBNull.Value, insert.Valor("bloque1"));
        Assert.Same(DBNull.Value, insert.Valor("almacen1"));
        Assert.Equal("confirmar", insert.Valor("paso1"));
        Assert.Same(DBNull.Value, insert.Valor("item1"));
        Assert.Same(DBNull.Value, insert.Valor("codProducto1"));
        Assert.Equal("INFO", insert.Valor("resultado1"));
        Assert.Same(DBNull.Value, insert.Valor("duracionMs1"));
        Assert.Same(DBNull.Value, insert.Valor("detalle1"));
    }

    [Fact]
    public void Finalizar_ResultadoOkDeEventoSeEscribeComoOk()
    {
        var consultor = new ConsultorFalso();

        new BitacoraRepositorioMySql(consultor).Finalizar(
            55, DatosBitacora.Ok(), new List<EventoBitacora> { DatosBitacora.Evento(1, "x", ResultadoEvento.Ok) });

        Assert.Equal("OK", consultor.InsertsDeEventos().Single().Valor("resultado0"));
    }

    [Fact]
    public void Finalizar_SiFallaElInsertDeEventosLaExcepcionSube()
    {
        var consultor = new ConsultorFalso { ErrorAlInsertarEventos = ErrorMySql(1213) };

        var ex = Assert.Throws<MySqlException>(
            () => new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), Eventos(2)));

        Assert.Equal(1213, ex.Number);
    }

    [Fact]
    public void Finalizar_SiFallaElUpdateLaExcepcionSubeYNoSeInsertanEventos()
    {
        var consultor = new ConsultorFalso { ErrorAlActualizar = new InvalidOperationException("sin red") };

        Assert.Throws<InvalidOperationException>(
            () => new BitacoraRepositorioMySql(consultor).Finalizar(55, DatosBitacora.Ok(), Eventos(2)));

        Assert.Empty(consultor.InsertsDeEventos());
    }

    [Fact]
    public void Finalizar_SinResultadoLanza()
    {
        var consultor = new ConsultorFalso();

        Assert.Throws<ArgumentNullException>(
            () => new BitacoraRepositorioMySql(consultor).Finalizar(55, null, Eventos(1)));
    }

    // Recorte al ancho de cada columna (STRICT_TRANS_TABLES rechazaría el texto largo)

    [Fact]
    public void CrearIntento_RecortaUsuarioEquipoYVersionAlAnchoDeLaColumna()
    {
        var consultor = new ConsultorFalso();
        var intento = new IntentoBitacora(
            7, 1, new string('u', 200), new string('e', 200), new string('v', 200), 1, DateTime.Now);
        long logId;

        new BitacoraRepositorioMySql(consultor).CrearIntento(intento, out logId);

        var insert = consultor.InsertsDeCabecera().Single();
        Assert.Equal(80, ((string)insert.Valor("usuario")).Length);
        Assert.Equal(60, ((string)insert.Valor("equipo")).Length);
        Assert.Equal(20, ((string)insert.Valor("versionApp")).Length);
    }

    [Fact]
    public void Finalizar_RecortaLosCamposDeErrorAlAnchoDeLaColumna()
    {
        var consultor = new ConsultorFalso();
        var resultado = new ResultadoBitacora(
            EstadoBitacora.Error, 0, DateTime.Now, 1, new string('p', 100), new string('q', 200),
            1, new string('s', 20), new string('m', 900), null);

        new BitacoraRepositorioMySql(consultor).Finalizar(1, resultado, new List<EventoBitacora>());

        var update = consultor.Updates().Single();
        Assert.Equal(40, ((string)update.Valor("errorPaso")).Length);
        Assert.Equal(80, ((string)update.Valor("errorProcedimiento")).Length);
        Assert.Equal(5, ((string)update.Valor("errorSqlState")).Length);
        Assert.Equal(500, ((string)update.Valor("errorMensaje")).Length);
    }

    [Fact]
    public void Finalizar_RecortaAlmacenPasoYDetalleDelEvento()
    {
        var consultor = new ConsultorFalso();
        var evento = new EventoBitacora(
            1, DateTime.Now, 1, new string('a', 200), new string('p', 200), null, null,
            ResultadoEvento.Info, null, new string('d', 900));

        new BitacoraRepositorioMySql(consultor).Finalizar(1, DatosBitacora.Ok(), new List<EventoBitacora> { evento });

        var insert = consultor.InsertsDeEventos().Single();
        Assert.Equal(80, ((string)insert.Valor("almacen0")).Length);
        Assert.Equal(40, ((string)insert.Valor("paso0")).Length);
        Assert.Equal(300, ((string)insert.Valor("detalle0")).Length);
    }
}
