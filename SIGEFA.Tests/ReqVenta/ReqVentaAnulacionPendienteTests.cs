using System;
using System.Collections.Generic;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas de anulación directa de un requerimiento de venta pendiente (estado 7, T2c).
// Las unitarias usan un IConsultor falso en este mismo archivo y no tocan BD.
// La integración usa el req 5273 (pendiente estable en dev) dentro de una
// transacción que siempre se revierte; fuera de ella solo hay SELECT.
// Misma colección que las demás pruebas que bloquean filas compartidas: en paralelo daban deadlock.
[Collection("BdReqVentaFilasCompartidas")]
public class ReqVentaAnulacionPendienteTests
{
    // Falso mínimo de IConsultor: sirve filas precargadas según la tabla del SELECT
    // y devuelve un número configurable de filas afectadas por tipo de escritura.
    private sealed class ConsultorFalso : IConsultor
    {
        private readonly Dictionary<string, object> _requerimiento;
        private readonly List<Dictionary<string, object>> _transferencias;
        private readonly List<Dictionary<string, object>> _detalle;
        private readonly Dictionary<string, object> _productoAlmacen;
        private readonly Dictionary<string, object> _factor;

        public ConsultorFalso(
            Dictionary<string, object> requerimiento,
            List<Dictionary<string, object>> transferencias,
            List<Dictionary<string, object>> detalle,
            Dictionary<string, object> productoAlmacen,
            Dictionary<string, object> factor)
        {
            _requerimiento = requerimiento;
            _transferencias = transferencias ?? new List<Dictionary<string, object>>();
            _detalle = detalle ?? new List<Dictionary<string, object>>();
            _productoAlmacen = productoAlmacen;
            _factor = factor;
        }

        // Filas afectadas por tipo de escritura; por defecto todo afecta 1 fila.
        public int FilasParaRechazo = 1;
        public int FilasParaStock = 1;
        public int FilasParaAnulado = 1;

        // Sentencias recibidas por Ejecutar, en orden, para verificar el flujo.
        public readonly List<string> Ejecutados = new List<string>();

        public Consulta Consultar(string sql, object parametros = null)
        {
            if (sql.Contains("FROM req_almacen"))
            {
                return Unica(_requerimiento);
            }

            if (sql.Contains("FROM transferencia"))
            {
                return Varias(_transferencias);
            }

            if (sql.Contains("FROM detalle_req_almacen"))
            {
                return Varias(_detalle);
            }

            if (sql.Contains("FROM productoalmacen"))
            {
                return Unica(_productoAlmacen);
            }

            return Unica(_factor);
        }

        public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
        {
            Ejecutados.Add(sql);
            if (sql.Contains("RechazarTransferencia"))
            {
                return new ResultadoEjecucion(0, FilasParaRechazo);
            }

            if (sql.Contains("productoalmacen"))
            {
                return new ResultadoEjecucion(0, FilasParaStock);
            }

            return new ResultadoEjecucion(0, FilasParaAnulado);
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

    private sealed class ReversionEsperada : Exception
    {
    }

    private static Dictionary<string, object> Requerimiento(int tipoReq, int estado)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "id_req_almacen", 5273 },
            { "estado", estado },
            { "tipo_req", tipoReq },
            { "cod_almacen_solicitante", 4 },
            { "cod_almacen_despacho", 3 },
            { "codPedidoVenta", null },
            { "codFacturaVenta", null },
        };
    }

    private static Dictionary<string, object> Transferencia(int codTrans, int estado, int pendiente)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "codTransDir", codTrans },
            { "codAlmacenOrigen", 3 },
            { "codAlmacenDestino", 4 },
            { "total", 10m },
            { "estado", estado },
            { "pendiente", pendiente },
            { "tiene_extorno", false },
        };
    }

    private static Dictionary<string, object> Detalle(int idDetalle, decimal pendienteAprobada)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "id_det_req_almacen", idDetalle },
            { "id_req_almacen", 5273 },
            { "cod_producto", 5072 },
            { "cod_unidad", 55 },
            { "cantidad", 7m },
            { "cantidad_pedida", 7m },
            { "cantidad_confirmada", 7m },
            { "cantidad_pendiente", 0m },
            { "cantidad_pendiente_aprobada", pendienteAprobada },
        };
    }

    private static Dictionary<string, object> ProductoAlmacen()
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "Unidad", 55 },
            { "stockdisponible", 17m },
        };
    }

    private static Dictionary<string, object> Factor(decimal valor)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "factor", valor },
        };
    }

    private static ConsultorFalso PendienteSinMovimientos()
    {
        return new ConsultorFalso(
            Requerimiento(2, 7),
            new List<Dictionary<string, object>>(),
            new List<Dictionary<string, object>> { Detalle(13907, 0m) },
            ProductoAlmacen(),
            Factor(1m));
    }

    [Fact]
    public void AnularPendiente_ConsultorNulo_Denega()
    {
        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(null, 5273, 1);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    [InlineData(5273, 0)]
    [InlineData(5273, -2)]
    public void AnularPendiente_CodigosInvalidos_Denega(int codReq, int codUser)
    {
        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(PendienteSinMovimientos(), codReq, codUser);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    [Fact]
    public void AnularPendiente_RequerimientoInexistente_Denega()
    {
        var consultor = new ConsultorFalso(null, null, null, null, null);

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    [Fact]
    public void AnularPendiente_Estado12_DenegaConMotivoDeLaRegla()
    {
        var consultor = new ConsultorFalso(Requerimiento(2, 12), null, null, null, null);

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 6318, 1);

        Assert.False(resultado.Ok);
        Assert.Contains("12", resultado.Mensaje);
    }

    [Fact]
    public void AnularPendiente_TipoReq1_DenegaPorqueNoEsVenta()
    {
        var consultor = new ConsultorFalso(Requerimiento(1, 7), null, null, null, null);

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 100, 1);

        Assert.False(resultado.Ok);
        Assert.Contains("venta", resultado.Mensaje);
    }

    [Fact]
    public void AnularPendiente_Estado8_DenegaPorqueNoEsPendiente()
    {
        var consultor = new ConsultorFalso(Requerimiento(2, 8), null, null, null, null);

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    [Fact]
    public void AnularPendiente_PendienteSinMovimientos_AnulaYMarcaEstado12()
    {
        ConsultorFalso consultor = PendienteSinMovimientos();

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.Contains(consultor.Ejecutados, s => s.Contains("UPDATE req_almacen"));
    }

    [Fact]
    public void AnularPendiente_TransferenciaPendiente_LaRechazaAntesDeAnular()
    {
        var consultor = new ConsultorFalso(
            Requerimiento(2, 7),
            new List<Dictionary<string, object>> { Transferencia(14001, 1, 1) },
            new List<Dictionary<string, object>> { Detalle(13907, 0m) },
            ProductoAlmacen(),
            Factor(1m));

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.Contains(consultor.Ejecutados, s => s.Contains("RechazarTransferencia"));
        int rechazo = consultor.Ejecutados.FindIndex(s => s.Contains("RechazarTransferencia"));
        int anulado = consultor.Ejecutados.FindIndex(s => s.Contains("UPDATE req_almacen"));
        Assert.True(rechazo >= 0 && anulado > rechazo);
    }

    [Fact]
    public void AnularPendiente_RechazoSinFilas_Denega()
    {
        var consultor = new ConsultorFalso(
            Requerimiento(2, 7),
            new List<Dictionary<string, object>> { Transferencia(14001, 1, 1) },
            new List<Dictionary<string, object>> { Detalle(13907, 0m) },
            ProductoAlmacen(),
            Factor(1m));
        consultor.FilasParaRechazo = 0;

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.False(resultado.Ok);
    }

    [Fact]
    public void AnularPendiente_ReservaSinFactor_Denega()
    {
        var consultor = new ConsultorFalso(
            Requerimiento(2, 7),
            new List<Dictionary<string, object>>(),
            new List<Dictionary<string, object>> { Detalle(13907, 2m) },
            ProductoAlmacen(),
            null);

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.False(resultado.Ok);
        Assert.DoesNotContain(consultor.Ejecutados, s => s.Contains("UPDATE req_almacen"));
    }

    [Fact]
    public void AnularPendiente_ReservaConFactor_DevuelveStockYAnula()
    {
        var consultor = new ConsultorFalso(
            Requerimiento(2, 7),
            new List<Dictionary<string, object>>(),
            new List<Dictionary<string, object>> { Detalle(13907, 2m) },
            ProductoAlmacen(),
            Factor(1m));

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.Contains(consultor.Ejecutados, s => s.Contains("UPDATE productoalmacen"));
    }

    [Fact]
    public void AnularPendiente_MarcadoSinFilas_Denega()
    {
        ConsultorFalso consultor = PendienteSinMovimientos();
        consultor.FilasParaAnulado = 0;

        ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1);

        Assert.False(resultado.Ok);
    }

    // Integración con el req 5273 (único pendiente tipo venta en dev: estado 7,
    // tipo_req 2, sin transferencias, detalle con pendiente aprobada 0).
    // Todo lo que escribe corre dentro de Db.Transaccion y se revierte con una
    // excepción; fuera solo hay SELECT. Si el 5273 deja de estar pendiente,
    // se omite sin inventar datos.
    [HechoConBd]
    public void AnularPendiente_5273_EnTransaccion_AlRevertirSigueEn7()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        Db.CadenaConexion = cadena;

        var lectura = new ConsultorMySql(cadena);
        Dictionary<string, object> antes = ReqVentaConsultas.ObtenerRequerimiento(lectura, 5273, false);
        if (antes == null || antes.Valor<int>("tipo_req") != 2 || antes.Valor<int>("estado") != 7)
        {
            return;
        }

        ResultadoAnulacion dentro = null;
        try
        {
            Db.Transaccion(tx =>
            {
                dentro = ReqVentaAnulacionPendiente.AnularPendiente(tx, 5273, 1);
                Assert.True(dentro.Ok, dentro.Mensaje);
                Dictionary<string, object> durante = ReqVentaConsultas.ObtenerRequerimiento(tx, 5273, false);
                Assert.Equal(12, durante.Valor<int>("estado"));
                throw new ReversionEsperada();
            });
        }
        catch (ReversionEsperada)
        {
        }

        Assert.NotNull(dentro);
        Assert.True(dentro.Ok, dentro.Mensaje);
        Dictionary<string, object> despues = ReqVentaConsultas.ObtenerRequerimiento(new ConsultorMySql(cadena), 5273, false);
        Assert.NotNull(despues);
        Assert.Equal(7, despues.Valor<int>("estado"));
    }
}
