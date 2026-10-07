using System;
using System.Collections.Generic;
using System.Reflection;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas de anulación con extorno de requerimientos de venta aprobados (estado 13, T2d).
// Las unitarias usan un IConsultor falso de este archivo y no tocan BD.
// La integración usa el req 8416 dentro de una transacción que siempre se revierte.
public class ReqVentaAnulacionConExtornoTests
{
    private const string CadenaDummy = "Server=127.0.0.1;Database=x;Uid=x;Pwd=x;";

    private sealed class ReversionEsperada : Exception
    {
    }

    // Falso mínimo de IConsultor para T2d: sirve filas precargadas según el SQL
    // y devuelve identificadores configurables para cada CALL. No abre conexiones.
    private sealed class ConsultorFalso : IConsultor
    {
        private readonly Dictionary<string, object> _requerimiento;
        private readonly List<Dictionary<string, object>> _transferencias;
        private readonly Dictionary<string, object> _cabecera;
        private readonly List<Dictionary<string, object>> _lineas;
        private readonly Dictionary<string, object> _stockSolicitante;
        private readonly Dictionary<string, object> _stockDespacho;
        private readonly Dictionary<string, object> _factor;
        private readonly int _almSolicitante;
        private readonly int _almDespacho;

        public ConsultorFalso(
            Dictionary<string, object> requerimiento,
            List<Dictionary<string, object>> transferencias,
            Dictionary<string, object> cabecera,
            List<Dictionary<string, object>> lineas,
            Dictionary<string, object> stockSolicitante,
            Dictionary<string, object> stockDespacho,
            Dictionary<string, object> factor,
            int almSolicitante,
            int almDespacho)
        {
            _requerimiento = requerimiento;
            _transferencias = transferencias ?? new List<Dictionary<string, object>>();
            _cabecera = cabecera;
            _lineas = lineas ?? new List<Dictionary<string, object>>();
            _stockSolicitante = stockSolicitante;
            _stockDespacho = stockDespacho;
            _factor = factor;
            _almSolicitante = almSolicitante;
            _almDespacho = almDespacho;
        }

        public int NewIdTransferencia = 9001;
        public int NewIdDetalleTransferencia = 9101;
        public int NewIdNotaSalida = 9201;
        public int NewIdDetalleSalida = 9301;
        public int NewIdNotaIngreso = 9401;
        public int NewIdDetalleIngreso = 9501;
        public bool AprobacionOk = true;
        public int FilasParaMarcado = 1;

        public readonly List<string> Ejecutados = new List<string>();
        public readonly List<string> Consultados = new List<string>();

        public Consulta Consultar(string sql, object parametros = null)
        {
            Consultados.Add(sql);
            if (sql.Contains("CALL GuardaTransferencia"))
            {
                return Unica(NuevaFila(NewIdTransferencia));
            }

            if (sql.Contains("CALL GuardaDetalleTransferencia"))
            {
                return Unica(NuevaFila(NewIdDetalleTransferencia));
            }

            if (sql.Contains("CALL GuardaNotaSalida"))
            {
                return Unica(NuevaFila(NewIdNotaSalida));
            }

            if (sql.Contains("CALL GuardaDetalleSalida"))
            {
                return Unica(NuevaFila(NewIdDetalleSalida));
            }

            if (sql.Contains("CALL GuardaNotaIngreso"))
            {
                return Unica(NuevaFila(NewIdNotaIngreso));
            }

            if (sql.Contains("CALL GuardaDetalleIngreso"))
            {
                return Unica(NuevaFila(NewIdDetalleIngreso));
            }

            if (sql.Contains("FROM req_almacen"))
            {
                return Unica(_requerimiento);
            }

            if (sql.Contains("FROM detalletransferencia"))
            {
                return Varias(_lineas);
            }

            if (sql.Contains("FROM transferencia"))
            {
                if (sql.Contains("tiene_extorno"))
                {
                    return Varias(_transferencias);
                }

                if (sql.Contains("EstadoTrnas"))
                {
                    return Unica(FilaAprobacion());
                }

                return Unica(_cabecera);
            }

            if (sql.Contains("FROM productoalmacen"))
            {
                int alm = ExtraerAlm(parametros);
                if (alm == _almDespacho)
                {
                    return Unica(_stockDespacho);
                }

                return Unica(_stockSolicitante);
            }

            if (sql.Contains("unidadequivalente"))
            {
                return Unica(_factor);
            }

            return Unica(null);
        }

        public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
        {
            Ejecutados.Add(sql);
            if (sql.Contains("AprobarTransferencia"))
            {
                return new ResultadoEjecucion(0, 1);
            }

            if (sql.Contains("UPDATE req_almacen"))
            {
                return new ResultadoEjecucion(0, FilasParaMarcado);
            }

            return new ResultadoEjecucion(0, 1);
        }

        private Dictionary<string, object> FilaAprobacion()
        {
            if (AprobacionOk)
            {
                return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                {
                    { "pendiente", 0 },
                    { "EstadoTrnas", 1 },
                };
            }

            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "pendiente", 1 },
                { "EstadoTrnas", 0 },
            };
        }

        private static Dictionary<string, object> NuevaFila(int newid)
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "newid", newid },
            };
        }

        private static int ExtraerAlm(object parametros)
        {
            if (parametros == null)
            {
                return 0;
            }

            var diccionario = parametros as IDictionary<string, object>;
            if (diccionario != null)
            {
                foreach (KeyValuePair<string, object> par in diccionario)
                {
                    if (string.Equals(par.Key, "alm", StringComparison.OrdinalIgnoreCase))
                    {
                        return Convert.ToInt32(par.Value);
                    }
                }

                return 0;
            }

            PropertyInfo propiedad = parametros.GetType().GetProperty(
                "alm",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (propiedad == null)
            {
                return 0;
            }

            return Convert.ToInt32(propiedad.GetValue(parametros, null));
        }

        private static Consulta Unica(Dictionary<string, object> fila)
        {
            return new Consulta(limite =>
            {
                if (fila == null)
                {
                    return new List<Dictionary<string, object>>();
                }

                return new List<Dictionary<string, object>> { fila };
            });
        }

        private static Consulta Varias(List<Dictionary<string, object>> filas)
        {
            return new Consulta(limite => filas);
        }
    }

    // Decora una transacción real y falla solo en el marcado final (UPDATE req_almacen).
    // Así el fallo ocurre DESPUÉS de haber escrito cabeceras, detalles, notas y aprobación.
    private sealed class ConsultorQueFallaAlMarcar : IConsultor
    {
        private readonly IConsultor _base;

        public ConsultorQueFallaAlMarcar(IConsultor baseConsultor)
        {
            _base = baseConsultor;
        }

        public Consulta Consultar(string sql, object parametros = null)
        {
            return _base.Consultar(sql, parametros);
        }

        public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
        {
            if (sql.Contains("UPDATE req_almacen"))
            {
                return new ResultadoEjecucion(0, 0);
            }

            return _base.Ejecutar(sql, parametros);
        }
    }

    private static Dictionary<string, object> RequerimientoExtorno()
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "id_req_almacen", 8416 },
            { "tipo_req", 2 },
            { "estado", 13 },
            { "cod_almacen_solicitante", 4 },
            { "cod_almacen_despacho", 3 },
            { "codPedidoVenta", null },
            { "codFacturaVenta", null },
        };
    }

    private static List<Dictionary<string, object>> TransferenciasSinExtorno()
    {
        return new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "codTransDir", 16098 },
                { "codAlmacenOrigen", 3 },
                { "codAlmacenDestino", 4 },
                { "total", 3.2992m },
                { "estado", 1 },
                { "pendiente", 0 },
                { "tiene_extorno", false },
            },
        };
    }

    private static Dictionary<string, object> CabeceraOriginal()
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "codTransDir", 16098 },
            { "codAlmacenOrigen", 3 },
            { "codAlmacenDestino", 4 },
            { "codserie", 1 },
            { "serie", "001" },
            { "numerodoc", "000123" },
            { "bruto", 3.2992 },
            { "montodscto", 0.0 },
            { "igv", 0.0 },
            { "total", 3.2992 },
            { "moneda", 1 },
            { "tipocambio", 0.0 },
        };
    }

    private static List<Dictionary<string, object>> LineasOriginales()
    {
        return new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "codDetalleTransDir", 43566 },
                { "codProducto", 5004 },
                { "codAlmacenOrigen", 3 },
                { "unidadingresada", 55 },
                { "codAlmacenDestino", 4 },
                { "serielote", "0" },
                { "cantidad", 2m },
                { "preciounitario", 1.6496m },
                { "subtotal", 3.2992m },
                { "descuento1", 0m },
                { "descuento2", 0m },
                { "descuento3", 0m },
                { "montodscto", 0m },
                { "igv", 0m },
                { "importe", 3.2992m },
                { "precioreal", 1.6496m },
                { "valoreal", 1.6496m },
                { "cantidadpendiente", 0m },
                { "codProv", 1 },
                { "PrecioIgv", 0 },
                { "valorpromedio", 1.6496m },
                { "id_det_req_almacen", 1 },
            },
        };
    }

    private static Dictionary<string, object> StockSolicitante(decimal actual, decimal disponible)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "Unidad", 55 },
            { "stockactual", actual },
            { "stockdisponible", disponible },
        };
    }

    private static Dictionary<string, object> StockDespacho(decimal actual, decimal disponible)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "Unidad", 55 },
            { "stockactual", actual },
            { "stockdisponible", disponible },
        };
    }

    private static Dictionary<string, object> Factor(decimal valor)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "factor", valor },
        };
    }

    private static ConsultorFalso CasoBase()
    {
        return new ConsultorFalso(
            RequerimientoExtorno(),
            TransferenciasSinExtorno(),
            CabeceraOriginal(),
            LineasOriginales(),
            StockSolicitante(100m, 100m),
            StockDespacho(100m, 100m),
            Factor(1m),
            4,
            3);
    }

    // La cadena de BD para integración se toma TAL CUAL viene de SIGEFA_TEST_CONN.
    // La normalización de AllowUserVariables la debe resolver la biblioteca de datos (Db / ConsultorMySql).
    private static string CadenaBd()
    {
        return HechoConBdAttribute.CadenaConexion();
    }

    [Fact]
    public void AnularConExtorno_ConsultorNulo_DevuelveFallo()
    {
        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(null, 8416, 1);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("consultor", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AnularConExtorno_ReqInvalido_DevuelveFallo(int codReq)
    {
        IConsultor consultor = new ConsultorMySql(CadenaDummy);
        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, codReq, 1);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("requerimiento", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AnularConExtorno_UsuarioInvalido_DevuelveFallo(int codUser)
    {
        IConsultor consultor = new ConsultorMySql(CadenaDummy);
        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, 8416, codUser);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("usuario", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AnularConExtorno_StockInsuficiente_DenegaAntesDeEscribir()
    {
        ConsultorFalso consultor = CasoBase();
        consultor = new ConsultorFalso(
            RequerimientoExtorno(),
            TransferenciasSinExtorno(),
            CabeceraOriginal(),
            LineasOriginales(),
            StockSolicitante(1m, 1m),
            StockDespacho(100m, 100m),
            Factor(1m),
            4,
            3);

        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, 8416, 18);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("stock", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(consultor.Ejecutados, s => s.Contains("UPDATE req_almacen"));
    }

    [Fact]
    public void AnularConExtorno_CabeceraExtornoSinId_Denega()
    {
        ConsultorFalso consultor = CasoBase();
        consultor.NewIdTransferencia = 0;

        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, 8416, 18);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("extorno", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AnularConExtorno_AprobacionSinEfecto_DenegaParaRevertir()
    {
        ConsultorFalso consultor = CasoBase();
        consultor.AprobacionOk = false;

        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, 8416, 18);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("aprobación", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AnularConExtorno_SinFilaEnDespacho_DenegaAntesDeEscribir()
    {
        ConsultorFalso consultor = new ConsultorFalso(
            RequerimientoExtorno(),
            TransferenciasSinExtorno(),
            CabeceraOriginal(),
            LineasOriginales(),
            StockSolicitante(100m, 100m),
            null,
            Factor(1m),
            4,
            3);

        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, 8416, 18);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("despacho", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(consultor.Ejecutados, s => s.Contains("UPDATE req_almacen"));
    }

    // Integración contra la BD dev: caso req 8416 (aprobado estado 13, transf 16098, producto 5004 con stock suficiente).
    // Corre dentro de una transacción y SIEMPRE se revierte (rollback). Verifica que en el rollback no queda nada escrito.
    [HechoConBd]
    public void AnularConExtorno_Req8416_EnTransaccion_AlRevertirSigueEn13YSinExtorno()
    {
        string cadena = CadenaBd();
        string cadenaPrevia = Db.CadenaConexion;
        Db.CadenaConexion = cadena;

        try
        {
            var lectura = new ConsultorMySql(cadena);
            Dictionary<string, object> antes = ReqVentaConsultas.ObtenerRequerimiento(lectura, 8416, false);
            if (antes == null || antes.Valor<int>("tipo_req") != 2 || antes.Valor<int>("estado") != 13)
            {
                return;
            }

            List<Dictionary<string, object>> transAntes = ReqVentaConsultas.ObtenerTransferencias(lectura, 8416, false);
            if (transAntes.Count != 1 || transAntes[0].Valor<bool>("tiene_extorno"))
            {
                return;
            }

            ResultadoAnulacion resultadoDentro = null;
            try
            {
                Db.Transaccion(tx =>
                {
                    resultadoDentro = ReqVentaAnulacionConExtorno.AnularConExtorno(tx, 8416, 18);
                    Assert.True(resultadoDentro.Ok, resultadoDentro.Mensaje);

                    // Durante la transacción: el requerimiento debe figurar en 12
                    Dictionary<string, object> durante = ReqVentaConsultas.ObtenerRequerimiento(tx, 8416, false);
                    Assert.Equal(12, durante.Valor<int>("estado"));

                    // Debe tener el extorno generado
                    List<Dictionary<string, object>> transDurante = ReqVentaConsultas.ObtenerTransferencias(tx, 8416, false);
                    Assert.Single(transDurante);
                    Assert.True(transDurante[0].Valor<bool>("tiene_extorno"));

                    throw new ReversionEsperada();
                });
            }
            catch (ReversionEsperada)
            {
            }

            Assert.NotNull(resultadoDentro);
            Assert.True(resultadoDentro.Ok, resultadoDentro.Mensaje);

            // Fuera de la transacción: rollback total confirmado
            Dictionary<string, object> despues = ReqVentaConsultas.ObtenerRequerimiento(new ConsultorMySql(cadena), 8416, false);
            Assert.NotNull(despues);
            Assert.Equal(13, despues.Valor<int>("estado"));

            List<Dictionary<string, object>> transDespues = ReqVentaConsultas.ObtenerTransferencias(new ConsultorMySql(cadena), 8416, false);
            Assert.Single(transDespues);
            Assert.False(transDespues[0].Valor<bool>("tiene_extorno"));
        }
        finally
        {
            Db.CadenaConexion = cadenaPrevia;
        }
    }

    // Integración contra la BD dev: fallo REAL después de haber escrito (el marcado final no afecta filas).
    // Todo corre en transacción con ROLLBACK: fuera no debe quedar extorno, notas ni cambio de estado.
    [HechoConBd]
    public void AnularConExtorno_FalloTrasEscribir_RollbackTotalMantiene13SinExtornoNiNotas()
    {
        string cadena = CadenaBd();
        string cadenaPrevia = Db.CadenaConexion;
        Db.CadenaConexion = cadena;

        try
        {
            var lectura = new ConsultorMySql(cadena);
            Dictionary<string, object> antes = ReqVentaConsultas.ObtenerRequerimiento(lectura, 8416, false);
            if (antes == null || antes.Valor<int>("tipo_req") != 2 || antes.Valor<int>("estado") != 13)
            {
                return;
            }

            List<Dictionary<string, object>> transAntes = ReqVentaConsultas.ObtenerTransferencias(lectura, 8416, false);
            if (transAntes.Count != 1 || transAntes[0].Valor<bool>("tiene_extorno"))
            {
                return;
            }

            int codOriginal = transAntes[0].Valor<int>("codTransDir");
            Dictionary<string, object> maxAntes = lectura.Consultar("SELECT IFNULL(MAX(codTransDir), 0) AS maximo FROM transferencia").First();
            long maximoAntes = Convert.ToInt64(maxAntes["maximo"]);

            ResultadoAnulacion resultadoDentro = null;
            try
            {
                Db.Transaccion(tx =>
                {
                    IConsultor conFallo = new ConsultorQueFallaAlMarcar(tx);
                    resultadoDentro = ReqVentaAnulacionConExtorno.AnularConExtorno(conFallo, 8416, 18);
                    Assert.NotNull(resultadoDentro);
                    Assert.False(resultadoDentro.Ok);
                    Assert.False(string.IsNullOrWhiteSpace(resultadoDentro.Mensaje));

                    throw new ReversionEsperada();
                });
            }
            catch (ReversionEsperada)
            {
            }

            Assert.NotNull(resultadoDentro);
            Assert.False(resultadoDentro.Ok);

            Dictionary<string, object> despues = ReqVentaConsultas.ObtenerRequerimiento(new ConsultorMySql(cadena), 8416, false);
            Assert.NotNull(despues);
            Assert.Equal(13, despues.Valor<int>("estado"));

            List<Dictionary<string, object>> transDespues = ReqVentaConsultas.ObtenerTransferencias(new ConsultorMySql(cadena), 8416, false);
            Assert.Single(transDespues);
            Assert.False(transDespues[0].Valor<bool>("tiene_extorno"));

            var verificacion = new ConsultorMySql(cadena);
            List<Dictionary<string, object>> extornos = verificacion.Consultar(
                "SELECT codTransDir FROM transferencia WHERE codDocExtornacion = @id",
                new { id = codOriginal }).Get();
            Assert.Empty(extornos);

            Dictionary<string, object> maxDespues = verificacion.Consultar("SELECT IFNULL(MAX(codTransDir), 0) AS maximo FROM transferencia").First();
            Assert.Equal(maximoAntes, Convert.ToInt64(maxDespues["maximo"]));
        }
        finally
        {
            Db.CadenaConexion = cadenaPrevia;
        }
    }

    // Integración contra la BD dev: se pone en cero el stock del producto 5004 en el almacén solicitante (4)
    // DENTRO de la transacción. La anulación debe denegar con "Stock insuficiente" sin escribir nada, y tras el
    // rollback el stock original (incluido el cero forzado) debe volver a estar como estaba.
    [HechoConBd]
    public void AnularConExtorno_Req8416_ConStockEnCero_DenegaYElRollbackRestauraElStock()
    {
        string cadena = CadenaBd();
        string cadenaPrevia = Db.CadenaConexion;
        Db.CadenaConexion = cadena;

        try
        {
            var lectura = new ConsultorMySql(cadena);
            Dictionary<string, object> antes = ReqVentaConsultas.ObtenerRequerimiento(lectura, 8416, false);
            if (antes == null || antes.Valor<int>("tipo_req") != 2 || antes.Valor<int>("estado") != 13)
            {
                return;
            }

            const string consultaStock = "SELECT stockactual, stockdisponible FROM productoalmacen WHERE codProducto = 5004 AND codAlmacen = 4";
            Dictionary<string, object> stockAntes = lectura.Consultar(consultaStock).First();
            if (stockAntes == null)
            {
                return;
            }

            decimal actualAntes = stockAntes.Valor<decimal>("stockactual");
            decimal disponibleAntes = stockAntes.Valor<decimal>("stockdisponible");
            long maximoAntes = Convert.ToInt64(lectura.Consultar("SELECT IFNULL(MAX(codTransDir), 0) AS maximo FROM transferencia").First()["maximo"]);

            ResultadoAnulacion resultadoDentro = null;
            try
            {
                Db.Transaccion(tx =>
                {
                    tx.Ejecutar("UPDATE productoalmacen SET stockactual = 0, stockdisponible = 0 WHERE codProducto = 5004 AND codAlmacen = 4");
                    resultadoDentro = ReqVentaAnulacionConExtorno.AnularConExtorno(tx, 8416, 18);
                    throw new ReversionEsperada();
                });
            }
            catch (ReversionEsperada)
            {
            }

            Assert.NotNull(resultadoDentro);
            Assert.False(resultadoDentro.Ok);
            Assert.Contains("Stock insuficiente", resultadoDentro.Mensaje, StringComparison.OrdinalIgnoreCase);

            var verificacion = new ConsultorMySql(cadena);
            Dictionary<string, object> stockDespues = verificacion.Consultar(consultaStock).First();
            Assert.Equal(actualAntes, stockDespues.Valor<decimal>("stockactual"));
            Assert.Equal(disponibleAntes, stockDespues.Valor<decimal>("stockdisponible"));
            Assert.Equal(13, ReqVentaConsultas.ObtenerRequerimiento(verificacion, 8416, false).Valor<int>("estado"));
            Assert.Equal(maximoAntes, Convert.ToInt64(verificacion.Consultar("SELECT IFNULL(MAX(codTransDir), 0) AS maximo FROM transferencia").First()["maximo"]));
        }
        finally
        {
            Db.CadenaConexion = cadenaPrevia;
        }
    }
}
