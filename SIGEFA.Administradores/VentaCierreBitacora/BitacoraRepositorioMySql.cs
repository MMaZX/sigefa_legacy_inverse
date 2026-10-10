using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Repositorio MySQL de la bitácora del cierre (T3). Usa solo IConsultor: en la app se construye con
// new ConsultorMySql(cadena), una conexión por llamada en autocommit y aparte de la transacción del
// cierre, así un rollback del cierre no borra el log del error. No captura excepciones: BitacoraCierre
// las atrapa y escribe el respaldo en archivo. Tablas según la migración
// 2026_10_10_130000_create_venta_cierre_log_tables.
public sealed class BitacoraRepositorioMySql : IBitacoraRepositorio
{
    private const int ErrorEntradaDuplicada = 1062;
    private const int MaximoIntentos = 5;
    private const int EventosPorSentencia = 200;

    private const string SqlCrearIntento =
        "INSERT INTO venta_cierre_log (cod_pedido, intento, cod_usuario, usuario, equipo, version_app, " +
        "estado, total_bloques, inicio) " +
        "SELECT @codPedido, COALESCE(MAX(intento), 0) + 1, @codUsuario, @usuario, @equipo, @versionApp, " +
        "'EN_CURSO', @totalBloques, @inicio " +
        "FROM venta_cierre_log WHERE cod_pedido = @codPedido";

    private const string SqlLeerIntento = "SELECT intento FROM venta_cierre_log WHERE id = @id";

    private const string SqlFinalizar =
        "UPDATE venta_cierre_log SET estado = @estado, bloques_ok = @bloquesOk, fin = @fin, " +
        "duracion_ms = @duracionMs, error_paso = @errorPaso, error_procedimiento = @errorProcedimiento, " +
        "error_mysql_num = @errorMysqlNum, error_sqlstate = @errorSqlState, error_mensaje = @errorMensaje, " +
        "cod_factura_venta = @codFacturaVenta WHERE id = @id";

    private const string SqlEventosEncabezado =
        "INSERT INTO venta_cierre_log_evento (log_id, orden, hora, bloque, almacen, paso, item, " +
        "cod_producto, resultado, duracion_ms, detalle) VALUES ";

    private readonly IConsultor _consultor;

    public BitacoraRepositorioMySql(IConsultor consultor)
    {
        if (consultor == null)
        {
            throw new ArgumentNullException(nameof(consultor));
        }
        _consultor = consultor;
    }

    public int CrearIntento(IntentoBitacora intento, out long logId)
    {
        if (intento == null)
        {
            throw new ArgumentNullException(nameof(intento));
        }

        var parametros = ParametrosDeCabecera(intento);
        var insertado = InsertarConReintento(parametros);
        logId = insertado.Id;
        return LeerIntento(logId);
    }

    public void Finalizar(long logId, ResultadoBitacora resultado, IReadOnlyList<EventoBitacora> eventos)
    {
        if (resultado == null)
        {
            throw new ArgumentNullException(nameof(resultado));
        }

        _consultor.Ejecutar(SqlFinalizar, ParametrosDeResultado(logId, resultado));

        if (eventos == null || eventos.Count == 0)
        {
            return;
        }

        for (int desde = 0; desde < eventos.Count; desde += EventosPorSentencia)
        {
            int cantidad = Math.Min(EventosPorSentencia, eventos.Count - desde);
            InsertarEventos(logId, eventos, desde, cantidad);
        }
    }

    // El UNIQUE (cod_pedido, intento) frena la carrera entre cajas; el perdedor repite la misma
    // sentencia, que recalcula MAX+1. Cualquier otro error sube tal cual.
    private ResultadoEjecucion InsertarConReintento(IDictionary<string, object> parametros)
    {
        for (int numero = 1; ; numero++)
        {
            try
            {
                return _consultor.Ejecutar(SqlCrearIntento, parametros);
            }
            catch (MySqlException ex) when (ex.Number == ErrorEntradaDuplicada && numero < MaximoIntentos)
            {
            }
        }
    }

    private int LeerIntento(long logId)
    {
        var fila = _consultor.Consultar(SqlLeerIntento, new Dictionary<string, object> { { "id", logId } }).First();
        if (fila == null || !fila.ContainsKey("intento") || fila["intento"] == null)
        {
            throw new InvalidOperationException(
                "No se encontró en venta_cierre_log la fila recién creada (id " + logId + ").");
        }
        return Convert.ToInt32(fila["intento"]);
    }

    private void InsertarEventos(long logId, IReadOnlyList<EventoBitacora> eventos, int desde, int cantidad)
    {
        var sql = new StringBuilder(SqlEventosEncabezado);
        var parametros = new Dictionary<string, object> { { "logId", logId } };

        for (int i = 0; i < cantidad; i++)
        {
            if (i > 0)
            {
                sql.Append(", ");
            }
            sql.Append("(@logId, @orden").Append(i)
                .Append(", @hora").Append(i)
                .Append(", @bloque").Append(i)
                .Append(", @almacen").Append(i)
                .Append(", @paso").Append(i)
                .Append(", @item").Append(i)
                .Append(", @codProducto").Append(i)
                .Append(", @resultado").Append(i)
                .Append(", @duracionMs").Append(i)
                .Append(", @detalle").Append(i)
                .Append(')');
            AgregarEvento(parametros, i, eventos[desde + i]);
        }

        _consultor.Ejecutar(sql.ToString(), parametros);
    }

    private static void AgregarEvento(IDictionary<string, object> parametros, int i, EventoBitacora evento)
    {
        parametros["orden" + i] = evento.Orden;
        parametros["hora" + i] = evento.Hora;
        parametros["bloque" + i] = Nulable(evento.Bloque);
        parametros["almacen" + i] = Texto(evento.Almacen, 80);
        parametros["paso" + i] = Recortar(evento.Paso, 40);
        parametros["item" + i] = Nulable(evento.Item);
        parametros["codProducto" + i] = Nulable(evento.CodProducto);
        parametros["resultado" + i] = TextoDeResultado(evento.Resultado);
        parametros["duracionMs" + i] = Nulable(evento.DuracionMs);
        parametros["detalle" + i] = Texto(evento.Detalle, 300);
    }

    private static IDictionary<string, object> ParametrosDeCabecera(IntentoBitacora intento)
    {
        return new Dictionary<string, object>
        {
            { "codPedido", intento.CodPedido },
            { "codUsuario", Nulable(intento.CodUsuario) },
            { "usuario", Texto(intento.Usuario, 80) },
            { "equipo", Texto(intento.Equipo, 60) },
            { "versionApp", Texto(intento.VersionApp, 20) },
            { "totalBloques", intento.TotalBloques },
            { "inicio", intento.Inicio },
        };
    }

    private static IDictionary<string, object> ParametrosDeResultado(long logId, ResultadoBitacora resultado)
    {
        return new Dictionary<string, object>
        {
            { "estado", resultado.Estado == EstadoBitacora.Ok ? "OK" : "ERROR" },
            { "bloquesOk", resultado.BloquesOk },
            { "fin", resultado.Fin },
            { "duracionMs", resultado.DuracionMs },
            { "errorPaso", Texto(resultado.ErrorPaso, 40) },
            { "errorProcedimiento", Texto(resultado.ErrorProcedimiento, 80) },
            { "errorMysqlNum", Nulable(resultado.ErrorMysqlNum) },
            { "errorSqlState", Texto(resultado.ErrorSqlState, 5) },
            { "errorMensaje", Texto(resultado.ErrorMensaje, 500) },
            { "codFacturaVenta", Nulable(resultado.CodFacturaVenta) },
            { "id", logId },
        };
    }

    private static string TextoDeResultado(ResultadoEvento resultado)
    {
        switch (resultado)
        {
            case ResultadoEvento.Ok:
                return "OK";
            case ResultadoEvento.Error:
                return "ERROR";
            default:
                return "INFO";
        }
    }

    private static object Nulable(int? valor)
    {
        return valor.HasValue ? (object)valor.Value : DBNull.Value;
    }

    // Los DTO normalizan null a vacío; en la tabla un texto sin contenido se guarda como NULL.
    // Se recorta al ancho de la columna: con STRICT_TRANS_TABLES un texto largo haría fallar el INSERT.
    private static object Texto(string valor, int ancho)
    {
        return string.IsNullOrEmpty(valor) ? (object)DBNull.Value : Recortar(valor, ancho);
    }

    private static string Recortar(string valor, int ancho)
    {
        return valor.Length > ancho ? valor.Substring(0, ancho) : valor;
    }
}
