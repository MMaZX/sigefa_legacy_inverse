using System;
using System.Collections.Generic;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas del servicio que decide entre anular un requerimiento pendiente (7) o
// aprobado con extorno (13) dentro de UNA transacción (T2e).
// Las unitarias usan un IConsultor falso y un ejecutor de transacción falso con la
// misma semántica que Db.Transaccion: confirma si la acción termina sin lanzar y
// revierte si lanza. La integración corre SIEMPRE con rollback.
public class ReqVentaFlujoServiceTests
{
    private sealed class ConsultorFalso : IConsultor
    {
        private readonly Dictionary<string, object> _requerimiento;
        private readonly List<Dictionary<string, object>> _transferencias;
        private readonly List<Dictionary<string, object>> _detalle;

        public ConsultorFalso(
            Dictionary<string, object> requerimiento,
            List<Dictionary<string, object>> transferencias,
            List<Dictionary<string, object>> detalle)
        {
            _requerimiento = requerimiento;
            _transferencias = transferencias ?? new List<Dictionary<string, object>>();
            _detalle = detalle ?? new List<Dictionary<string, object>>();
        }

        public int FilasParaRechazo = 1;
        public int FilasParaAnulado = 1;
        public Exception LanzarAlConsultar;
        public readonly List<string> Ejecutados = new List<string>();

        public Consulta Consultar(string sql, object parametros = null)
        {
            if (LanzarAlConsultar != null)
            {
                throw LanzarAlConsultar;
            }

            if (sql.Contains("FROM req_almacen"))
            {
                return Filas(_requerimiento == null ? new List<Dictionary<string, object>>() : new List<Dictionary<string, object>> { _requerimiento });
            }

            if (sql.Contains("FROM transferencia"))
            {
                return Filas(_transferencias);
            }

            if (sql.Contains("FROM detalle_req_almacen"))
            {
                return Filas(_detalle);
            }

            return Filas(new List<Dictionary<string, object>>());
        }

        public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
        {
            Ejecutados.Add(sql);
            if (sql.Contains("RechazarTransferencia"))
            {
                return new ResultadoEjecucion(0, FilasParaRechazo);
            }

            return new ResultadoEjecucion(0, FilasParaAnulado);
        }

        private static Consulta Filas(List<Dictionary<string, object>> filas)
        {
            return new Consulta(limite => filas);
        }
    }

    // Misma semántica que Db.Transaccion<T>: commit si la acción termina, rollback si lanza (y relanza).
    private sealed class TransaccionFalsa
    {
        private readonly IConsultor _consultor;

        public TransaccionFalsa(IConsultor consultor)
        {
            _consultor = consultor;
        }

        public int Invocaciones;
        public bool Confirmada;
        public bool Revertida;

        public ResultadoAnulacion Ejecutar(Func<IConsultor, ResultadoAnulacion> accion)
        {
            Invocaciones++;
            try
            {
                ResultadoAnulacion resultado = accion(_consultor);
                Confirmada = true;
                return resultado;
            }
            catch
            {
                Revertida = true;
                throw;
            }
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

    private static Dictionary<string, object> TransferenciaPendiente(int codTrans)
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "codTransDir", codTrans },
            { "codAlmacenOrigen", 3 },
            { "codAlmacenDestino", 4 },
            { "total", 10m },
            { "estado", 1 },
            { "pendiente", 1 },
            { "tiene_extorno", false },
        };
    }

    private static Dictionary<string, object> DetalleSinReserva()
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            { "id_det_req_almacen", 1 },
            { "id_req_almacen", 5273 },
            { "cod_producto", 5072 },
            { "cod_unidad", 55 },
            { "cantidad", 7m },
            { "cantidad_pedida", 7m },
            { "cantidad_confirmada", 7m },
            { "cantidad_pendiente", 0m },
            { "cantidad_pendiente_aprobada", 0m },
        };
    }

    private static ConsultorFalso Pendiente()
    {
        return new ConsultorFalso(Requerimiento(2, 7), null, new List<Dictionary<string, object>> { DetalleSinReserva() });
    }

    private sealed class Registro
    {
        public readonly List<string> Lineas = new List<string>();

        public void Registrar(string linea)
        {
            Lineas.Add(linea);
        }
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    [InlineData(5273, 0)]
    [InlineData(5273, -2)]
    public void Anular_CodigosInvalidos_DenegaSinAbrirTransaccion(int codReq, int codUser)
    {
        var transaccion = new TransaccionFalsa(Pendiente());
        var registro = new Registro();

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(codReq, codUser, transaccion.Ejecutar, registro.Registrar);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
        Assert.Equal(0, transaccion.Invocaciones);
    }

    [Fact]
    public void Anular_Pendiente_AnulaYConfirma()
    {
        ConsultorFalso consultor = Pendiente();
        var transaccion = new TransaccionFalsa(consultor);

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, transaccion.Ejecutar, new Registro().Registrar);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.True(transaccion.Confirmada);
        Assert.False(transaccion.Revertida);
        Assert.Contains(consultor.Ejecutados, s => s.Contains("UPDATE req_almacen"));
    }

    [Fact]
    public void Anular_RequerimientoInexistente_DenegaYRevierte()
    {
        var consultor = new ConsultorFalso(null, null, null);
        var transaccion = new TransaccionFalsa(consultor);

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(999999, 18, transaccion.Ejecutar, new Registro().Registrar);

        Assert.False(resultado.Ok);
        Assert.Contains("999999", resultado.Mensaje);
        Assert.True(transaccion.Revertida);
        Assert.False(transaccion.Confirmada);
        Assert.Empty(consultor.Ejecutados);
    }

    [Theory]
    [InlineData(2, 12, "12")]
    [InlineData(2, 9, "9")]
    [InlineData(2, 17, "17")]
    [InlineData(1, 7, "venta")]
    public void Anular_NoAnulable_DenegaConMotivoDeLaReglaYRevierte(int tipoReq, int estado, string fragmento)
    {
        var consultor = new ConsultorFalso(Requerimiento(tipoReq, estado), null, null);
        var transaccion = new TransaccionFalsa(consultor);

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, transaccion.Ejecutar, new Registro().Registrar);

        Assert.False(resultado.Ok);
        Assert.Contains(fragmento, resultado.Mensaje);
        Assert.True(transaccion.Revertida);
        Assert.False(transaccion.Confirmada);
        Assert.Empty(consultor.Ejecutados);
    }

    [Fact]
    public void Anular_Aprobado13_VaPorElCaminoDelExtornoYNoPorElPendiente()
    {
        // Sin transferencia original el extorno falla con su propio mensaje: prueba que se eligió ese camino.
        var consultor = new ConsultorFalso(Requerimiento(2, 13), null, null);
        var transaccion = new TransaccionFalsa(consultor);

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, transaccion.Ejecutar, new Registro().Registrar);

        Assert.False(resultado.Ok);
        Assert.Contains("transferencia original", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.True(transaccion.Revertida);
        Assert.DoesNotContain(consultor.Ejecutados, s => s.Contains("RechazarTransferencia"));
    }

    [Fact]
    public void Anular_FalloDelSubservicioTrasEscribir_RevierteYDevuelveElMotivo()
    {
        // El rechazo de la transferencia no afecta filas: el subservicio devuelve fallo SIN lanzar.
        // El servicio debe forzar el rollback (Db.Transaccion confirmaría una acción que no lanza).
        var consultor = new ConsultorFalso(
            Requerimiento(2, 7),
            new List<Dictionary<string, object>> { TransferenciaPendiente(14001) },
            new List<Dictionary<string, object>> { DetalleSinReserva() });
        consultor.FilasParaRechazo = 0;
        var transaccion = new TransaccionFalsa(consultor);

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, transaccion.Ejecutar, new Registro().Registrar);

        Assert.False(resultado.Ok);
        Assert.Contains("14001", resultado.Mensaje);
        Assert.True(transaccion.Revertida);
        Assert.False(transaccion.Confirmada);
    }

    [Fact]
    public void Anular_MarcadoFinalSinFilas_RevierteYNoConfirma()
    {
        ConsultorFalso consultor = Pendiente();
        consultor.FilasParaAnulado = 0;
        var transaccion = new TransaccionFalsa(consultor);

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, transaccion.Ejecutar, new Registro().Registrar);

        Assert.False(resultado.Ok);
        Assert.True(transaccion.Revertida);
        Assert.False(transaccion.Confirmada);
    }

    [Fact]
    public void Anular_ExcepcionInesperada_DevuelveFalloConCausaYLoRegistra()
    {
        ConsultorFalso consultor = Pendiente();
        consultor.LanzarAlConsultar = new InvalidOperationException("se cayó la conexión", new TimeoutException("tiempo agotado"));
        var transaccion = new TransaccionFalsa(consultor);
        var registro = new Registro();

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, transaccion.Ejecutar, registro.Registrar);

        Assert.False(resultado.Ok);
        Assert.Contains("se cayó la conexión", resultado.Mensaje);
        Assert.Contains("tiempo agotado", resultado.Mensaje);
        Assert.True(transaccion.Revertida);
        Assert.Single(registro.Lineas);
        Assert.Contains("5273", registro.Lineas[0]);
    }

    [Fact]
    public void Anular_ErrorDeNegocio_SeRegistraUnaSolaVez()
    {
        var consultor = new ConsultorFalso(Requerimiento(2, 12), null, null);
        var registro = new Registro();

        ReqVentaFlujoService.Anular(5273, 18, new TransaccionFalsa(consultor).Ejecutar, registro.Registrar);

        Assert.Single(registro.Lineas);
    }

    [Fact]
    public void Anular_Exito_NoRegistraNada()
    {
        var registro = new Registro();

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, new TransaccionFalsa(Pendiente()).Ejecutar, registro.Registrar);

        Assert.True(resultado.Ok, resultado.Mensaje);
        Assert.Empty(registro.Lineas);
    }

    [Fact]
    public void Anular_RegistradorQueFalla_NoOcultaElResultado()
    {
        var consultor = new ConsultorFalso(Requerimiento(2, 12), null, null);

        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
            5273, 18, new TransaccionFalsa(consultor).Ejecutar, linea => throw new InvalidOperationException("disco lleno"));

        Assert.False(resultado.Ok);
        Assert.Contains("12", resultado.Mensaje);
    }

    [Fact]
    public void Anular_EjecutorNulo_Denega()
    {
        ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(5273, 18, null, new Registro().Registrar);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    [Theory]
    [InlineData("Server=x;Pwd=secreto;Database=d", "secreto")]
    [InlineData("fallo con Password=otra", "otra")]
    [InlineData("fallo con Uid=root;", "root")]
    public void Registro_EnmascaraCredenciales(string texto, string secreto)
    {
        string limpio = ReqVentaRegistroErrores.Enmascarar(texto);

        Assert.DoesNotContain(secreto, limpio);
        Assert.Contains("***", limpio);
    }

    // Integración con rollback SIEMPRE: el ejecutor corre la acción del servicio en una transacción real y la
    // revierte aunque haya salido bien. Verifica que el servicio elige el camino correcto contra MySQL real.
    private static Func<Func<IConsultor, ResultadoAnulacion>, ResultadoAnulacion> TransaccionRealConRollback(Action<IConsultor> verificarDentro)
    {
        return accion =>
        {
            ResultadoAnulacion resultado = null;
            try
            {
                Db.Transaccion(tx =>
                {
                    resultado = accion(tx);
                    verificarDentro(tx);
                    throw new ReversionEsperada();
                });
            }
            catch (ReversionEsperada)
            {
            }

            return resultado;
        };
    }

    [HechoConBd]
    public void Anular_Pendiente5273_ContraBdReal_AnulaDentroYRevierte()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        string previa = Db.CadenaConexion;
        Db.CadenaConexion = cadena;
        try
        {
            Dictionary<string, object> antes = ReqVentaConsultas.ObtenerRequerimiento(new ConsultorMySql(cadena), 5273, false);
            if (antes == null || antes.Valor<int>("tipo_req") != 2 || antes.Valor<int>("estado") != 7)
            {
                return;
            }

            int estadoDentro = 0;
            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                5273, 1,
                TransaccionRealConRollback(tx => estadoDentro = ReqVentaConsultas.ObtenerRequerimiento(tx, 5273, false).Valor<int>("estado")),
                new Registro().Registrar);

            Assert.True(resultado.Ok, resultado.Mensaje);
            Assert.Equal(12, estadoDentro);
            Assert.Equal(7, ReqVentaConsultas.ObtenerRequerimiento(new ConsultorMySql(cadena), 5273, false).Valor<int>("estado"));
        }
        finally
        {
            Db.CadenaConexion = previa;
        }
    }

    [HechoConBd]
    public void Anular_Aprobado8416_ContraBdReal_GeneraExtornoDentroYRevierte()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        string previa = Db.CadenaConexion;
        Db.CadenaConexion = cadena;
        try
        {
            var lectura = new ConsultorMySql(cadena);
            Dictionary<string, object> antes = ReqVentaConsultas.ObtenerRequerimiento(lectura, 8416, false);
            List<Dictionary<string, object>> transAntes = ReqVentaConsultas.ObtenerTransferencias(lectura, 8416, false);
            if (antes == null || antes.Valor<int>("tipo_req") != 2 || antes.Valor<int>("estado") != 13
                || transAntes.Count != 1 || transAntes[0].Valor<bool>("tiene_extorno"))
            {
                return;
            }

            int estadoDentro = 0;
            bool extornoDentro = false;
            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                8416, 18,
                TransaccionRealConRollback(tx =>
                {
                    estadoDentro = ReqVentaConsultas.ObtenerRequerimiento(tx, 8416, false).Valor<int>("estado");
                    extornoDentro = ReqVentaConsultas.ObtenerTransferencias(tx, 8416, false)[0].Valor<bool>("tiene_extorno");
                }),
                new Registro().Registrar);

            Assert.True(resultado.Ok, resultado.Mensaje);
            Assert.Equal(12, estadoDentro);
            Assert.True(extornoDentro);

            var despues = new ConsultorMySql(cadena);
            Assert.Equal(13, ReqVentaConsultas.ObtenerRequerimiento(despues, 8416, false).Valor<int>("estado"));
            Assert.False(ReqVentaConsultas.ObtenerTransferencias(despues, 8416, false)[0].Valor<bool>("tiene_extorno"));
        }
        finally
        {
            Db.CadenaConexion = previa;
        }
    }

    [HechoConBd]
    public void Anular_Aprobado8416ConStockEnCero_ContraBdReal_DenegaSinDejarNada()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        string previa = Db.CadenaConexion;
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
            var registro = new Registro();
            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                8416, 18,
                accion =>
                {
                    ResultadoAnulacion r = null;
                    try
                    {
                        Db.Transaccion(tx =>
                        {
                            tx.Ejecutar("UPDATE productoalmacen SET stockactual = 0, stockdisponible = 0 WHERE codProducto = 5004 AND codAlmacen = 4");
                            r = accion(tx);
                            throw new ReversionEsperada();
                        });
                    }
                    catch (ReversionEsperada)
                    {
                    }

                    return r;
                },
                registro.Registrar);

            Assert.False(resultado.Ok);
            Assert.Contains("Stock insuficiente", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
            Assert.Single(registro.Lineas);

            var despues = new ConsultorMySql(cadena);
            Assert.Equal(actualAntes, despues.Consultar(consultaStock).First().Valor<decimal>("stockactual"));
            Assert.Equal(13, ReqVentaConsultas.ObtenerRequerimiento(despues, 8416, false).Valor<int>("estado"));
        }
        finally
        {
            Db.CadenaConexion = previa;
        }
    }
}
