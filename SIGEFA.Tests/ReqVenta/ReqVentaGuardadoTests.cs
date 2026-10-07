using System;
using System.Collections.Generic;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.ReqVenta
{
    // Pruebas del guardado de requerimiento de venta en segundo plano (T6).
    // ReqVentaGuardado replica 1+N de clsAdmRequerimientoAlmacen.insert
    // (GuardaRequerimientoAlmacen + GuardaDetalleRequerimientoAlmacen en una
    // sola transacción) pero devuelve ResultadoOperacion sin mostrar cuadros.
    // Las unitarias usan falsos y no tocan BD; la integración corre SIEMPRE
    // con rollback y verifica columna por columna lo guardado.
    public class ReqVentaGuardadoTests
    {
        // Consultor falso para T6: sirve newid de cabecera y de detalles en orden.
        private sealed class ConsultorFalsoGuardado : IConsultor
        {
            public int NewIdCabecera = 101;
            public Queue<int> NewIdsDetalle = new Queue<int>(new[] { 201, 202 });
            public Exception ExcepcionAlConsultar;

            public Consulta Consultar(string sql, object parametros = null)
            {
                if (ExcepcionAlConsultar != null)
                {
                    throw ExcepcionAlConsultar;
                }

                if (sql.Contains("GuardaRequerimientoAlmacen"))
                {
                    return Unica(NuevaFila(NewIdCabecera));
                }

                if (sql.Contains("GuardaDetalleRequerimientoAlmacen"))
                {
                    int newid = 0;
                    if (NewIdsDetalle.Count > 0)
                    {
                        newid = NewIdsDetalle.Dequeue();
                    }

                    return Unica(NuevaFila(newid));
                }

                return Unica(null);
            }

            public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
            {
                return new ResultadoEjecucion(0, 1);
            }

            private static Dictionary<string, object> NuevaFila(int newid)
            {
                return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                {
                    { "newid", newid },
                };
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
        }

        // Misma semántica que Db.Transaccion<T>: confirma si la acción termina sin lanzar.
        private sealed class TransaccionFalsa
        {
            private readonly IConsultor _consultor;

            public TransaccionFalsa(IConsultor consultor)
            {
                _consultor = consultor;
            }

            public bool Confirmada;
            public bool Revertida;

            public ResultadoOperacion Ejecutar(Func<IConsultor, ResultadoOperacion> accion)
            {
                try
                {
                    ResultadoOperacion resultado = accion(_consultor);
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

        private static DatosGuardadoRequerimiento CabeceraValida()
        {
            return new DatosGuardadoRequerimiento(
                1, "00000012", 3, "001",
                4, 18, new DateTime(2026, 10, 7, 10, 0, 0),
                4, 3, new DateTime(2026, 10, 7),
                7, "Para obra", null, 2,
                0, "OV-77",
                "Juan Perez", "987654321", 0, "", "Despachador Uno");
        }

        private static DatosGuardadoDetalle DetalleValido(int codigo = 0, int producto = 5004)
        {
            return new DatosGuardadoDetalle(codigo, producto, 55, 7m, 7m, 0m, 7m, 0m);
        }

        private static List<DatosGuardadoDetalle> DosDetalles()
        {
            return new List<DatosGuardadoDetalle> { DetalleValido(), DetalleValido(0, 5005) };
        }

        [Fact]
        public void Guardar_CabeceraNula_DevuelveFalloSinAbrirTransaccion()
        {
            bool llamada = false;
            Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion =
                accion => { llamada = true; return new ResultadoOperacion(true, string.Empty); };

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(null, DosDetalles(), transaccion);

            Assert.False(resultado.Ok);
            Assert.False(llamada);
            Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
        }

        [Fact]
        public void Guardar_DetallesNulos_DevuelveFalloSinAbrirTransaccion()
        {
            bool llamada = false;
            Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion =
                accion => { llamada = true; return new ResultadoOperacion(true, string.Empty); };

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(CabeceraValida(), null, transaccion);

            Assert.False(resultado.Ok);
            Assert.False(llamada);
        }

        [Fact]
        public void Guardar_TransaccionNula_DevuelveFallo()
        {
            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(CabeceraValida(), DosDetalles(), null);

            Assert.False(resultado.Ok);
            Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
        }

        [Fact]
        public void Guardar_CabeceraConDosDetalles_DevuelveOkYAsignaCodigo()
        {
            var consultor = new ConsultorFalsoGuardado();
            var transaccion = new TransaccionFalsa(consultor);
            DatosGuardadoRequerimiento cabecera = CabeceraValida();

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(cabecera, DosDetalles(), transaccion.Ejecutar);

            Assert.True(resultado.Ok, resultado.Mensaje);
            Assert.True(transaccion.Confirmada);
            Assert.False(transaccion.Revertida);
            Assert.Equal(101, cabecera.Codigo);
        }

        [Fact]
        public void Guardar_CabeceraSinNewId_RevierteYDevuelveFallo()
        {
            var consultor = new ConsultorFalsoGuardado();
            consultor.NewIdCabecera = 0;
            var transaccion = new TransaccionFalsa(consultor);

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(CabeceraValida(), DosDetalles(), transaccion.Ejecutar);

            Assert.False(resultado.Ok);
            Assert.True(transaccion.Revertida);
            Assert.False(transaccion.Confirmada);
        }

        [Fact]
        public void Guardar_DetalleSinNewId_RevierteYDevuelveFallo()
        {
            var consultor = new ConsultorFalsoGuardado();
            consultor.NewIdsDetalle = new Queue<int>(new[] { 201, 0 });
            var transaccion = new TransaccionFalsa(consultor);

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(CabeceraValida(), DosDetalles(), transaccion.Ejecutar);

            Assert.False(resultado.Ok);
            Assert.True(transaccion.Revertida);
            Assert.False(transaccion.Confirmada);
        }

        [Fact]
        public void Guardar_ConDocumentoRepetido_DevuelveMensajeDeRepetido()
        {
            var consultor = new ConsultorFalsoGuardado();
            consultor.ExcepcionAlConsultar = new InvalidOperationException("Duplicate entry '12' for key 'num'");
            var transaccion = new TransaccionFalsa(consultor);

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(CabeceraValida(), DosDetalles(), transaccion.Ejecutar);

            Assert.False(resultado.Ok);
            Assert.True(transaccion.Revertida);
            Assert.Contains("Repetido", resultado.Mensaje);
        }

        [Fact]
        public void Guardar_ConErrorGenerico_DevuelveProblemaConCausa()
        {
            var consultor = new ConsultorFalsoGuardado();
            consultor.ExcepcionAlConsultar = new InvalidOperationException("fallo bd");
            var transaccion = new TransaccionFalsa(consultor);

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(CabeceraValida(), DosDetalles(), transaccion.Ejecutar);

            Assert.False(resultado.Ok);
            Assert.Contains("Se encontr", resultado.Mensaje);
            Assert.Contains("fallo bd", resultado.Mensaje);
        }

        [Fact]
        public void GuardarEn_ConsultorNulo_DevuelveFallo()
        {
            ResultadoOperacion resultado = ReqVentaGuardado.GuardarEn(null, CabeceraValida(), DosDetalles());

            Assert.False(resultado.Ok);
        }

        [Fact]
        public void Guardar_DetalleExistenteSinNewId_SigueOk()
        {
            var consultor = new ConsultorFalsoGuardado();
            consultor.NewIdsDetalle = new Queue<int>(new[] { 0 });
            var transaccion = new TransaccionFalsa(consultor);
            var detalles = new List<DatosGuardadoDetalle> { DetalleValido(55) };

            ResultadoOperacion resultado = ReqVentaGuardado.Guardar(CabeceraValida(), detalles, transaccion.Ejecutar);

            Assert.True(resultado.Ok, resultado.Mensaje);
            Assert.True(transaccion.Confirmada);
        }
    }

    // Integración con rollback SIEMPRE (corrección T6): guarda una cabecera con 2
    // líneas copiando valores válidos del req 5273 de dev y verifica columna por
    // columna dentro de la transacción. Con el orden viejo de parámetros falla:
    // el detalle queda con id_req_almacen NULL y no aparece por el código nuevo.
    // Colección compartida con las pruebas que bloquean filas de req_venta: en
    // paralelo provocaban deadlocks por orden de bloqueo distinto entre pruebas.
    [Collection("BdReqVentaFilasCompartidas")]
    public class ReqVentaGuardadoIntegracionTests
    {
        private sealed class ReversionEsperada : Exception
        {
        }

        // Integración con rollback SIEMPRE: corre la acción del servicio en una
        // transacción real, verifica dentro y la revierte aunque haya salido bien.
        private static Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> TransaccionRealConRollback(
            Action<IConsultor> verificarDentro)
        {
            return accion =>
            {
                ResultadoOperacion resultado = null;
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
        public void Guardar_CabeceraConDosDetalles_ContraBdReal_GuardaColumnasYRevierte()
        {
            string cadena = HechoConBdAttribute.CadenaConexion();
            string previa = Db.CadenaConexion;
            Db.CadenaConexion = cadena;
            try
            {
                var lectura = new ConsultorMySql(cadena);
                Dictionary<string, object> base5273 = lectura.Consultar(
                    "SELECT cod_tipo_documento, cod_serie, num_serie, cod_almacen_registro, cod_user_registro, " +
                    "cod_almacen_solicitante, cod_almacen_despacho, codPropuestaPedido, codPedidoVenta, " +
                    "NombreContacto, TelefonoContacto, Delivery, DireccionDelivery, AutorizadoPor " +
                    "FROM req_almacen WHERE id_req_almacen = 5273").First();
                if (base5273 == null)
                {
                    return;
                }

                long totalAntes = lectura.Consultar("SELECT COUNT(*) AS total FROM req_almacen").First().Valor<long>("total");
                string numDoc = "ZZT" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpperInvariant();

                var cabecera = new DatosGuardadoRequerimiento(
                    base5273.Valor<int>("cod_tipo_documento"), numDoc,
                    base5273.Valor<int>("cod_serie"), base5273.Valor<string>("num_serie"),
                    base5273.Valor<int>("cod_almacen_registro"), base5273.Valor<int>("cod_user_registro"), DateTime.Now,
                    base5273.Valor<int>("cod_almacen_solicitante"), base5273.Valor<int>("cod_almacen_despacho"), DateTime.Now,
                    7, "Para obra", null, 2,
                    base5273.Valor<int?>("codPropuestaPedido") ?? 0, base5273.Valor<string>("codPedidoVenta"),
                    base5273.Valor<string>("NombreContacto"), base5273.Valor<string>("TelefonoContacto"),
                    base5273.Valor<int>("Delivery"), base5273.Valor<string>("DireccionDelivery"),
                    base5273.Valor<string>("AutorizadoPor"));
                var detalles = new List<DatosGuardadoDetalle>
                {
                    new DatosGuardadoDetalle(0, 5072, 55, 7m, 7m, 0m, 7m, 0m),
                    new DatosGuardadoDetalle(0, 5877, 55, 3m, 3m, 0m, 3m, 0m),
                };

                ResultadoOperacion resultado = ReqVentaGuardado.Guardar(
                    cabecera, detalles,
                    TransaccionRealConRollback(tx => VerificarGuardadoDentro(tx, cabecera, detalles, numDoc)));

                Assert.True(resultado.Ok, resultado.Mensaje);
                Assert.True(cabecera.Codigo > 0);

                var despues = new ConsultorMySql(cadena);
                Assert.Equal(totalAntes, despues.Consultar("SELECT COUNT(*) AS total FROM req_almacen").First().Valor<long>("total"));
                Assert.Equal(0, despues.Consultar(
                    "SELECT COUNT(*) AS total FROM req_almacen WHERE num_documento = @num",
                    new { num = numDoc }).First().Valor<long>("total"));
            }
            finally
            {
                Db.CadenaConexion = previa;
            }
        }

        private static void VerificarGuardadoDentro(
            IConsultor tx, DatosGuardadoRequerimiento cabecera, List<DatosGuardadoDetalle> esperados, string numDoc)
        {
            Dictionary<string, object> fila = tx.Consultar(
                "SELECT id_req_almacen, tipo_req, estado, cod_almacen_solicitante, cod_almacen_despacho, num_documento " +
                "FROM req_almacen WHERE id_req_almacen = @id",
                new { id = cabecera.Codigo }).First();
            Assert.NotNull(fila);
            Assert.Equal(2, fila.Valor<int>("tipo_req"));
            Assert.Equal(7, fila.Valor<int>("estado"));
            Assert.Equal(4, fila.Valor<int>("cod_almacen_solicitante"));
            Assert.Equal(3, fila.Valor<int>("cod_almacen_despacho"));
            Assert.Equal(numDoc, fila.Valor<string>("num_documento"));

            List<Dictionary<string, object>> lineas = tx.Consultar(
                "SELECT id_req_almacen, cod_producto, cod_unidad, cantidad, cantidad_pedida, " +
                "cantidad_pendiente, cantidad_confirmada, cantidad_pendiente_aprobada " +
                "FROM detalle_req_almacen WHERE id_req_almacen = @id ORDER BY id_det_req_almacen",
                new { id = cabecera.Codigo }).Get();
            Assert.Equal(esperados.Count, lineas.Count);
            for (int i = 0; i < esperados.Count; i++)
            {
                Assert.Equal(cabecera.Codigo, lineas[i].Valor<int>("id_req_almacen"));
                Assert.Equal(esperados[i].CodProducto, lineas[i].Valor<int>("cod_producto"));
                Assert.Equal(esperados[i].CodUnidad, lineas[i].Valor<int>("cod_unidad"));
                Assert.Equal(esperados[i].Cantidad, lineas[i].Valor<decimal>("cantidad"));
                Assert.Equal(esperados[i].CantidadPedida, lineas[i].Valor<decimal>("cantidad_pedida"));
                Assert.Equal(esperados[i].CantidadPendiente, lineas[i].Valor<decimal>("cantidad_pendiente"));
                Assert.Equal(esperados[i].CantidadConfirmada, lineas[i].Valor<decimal>("cantidad_confirmada"));
                Assert.Equal(esperados[i].CantidadPendienteAprobada, lineas[i].Valor<decimal>("cantidad_pendiente_aprobada"));
            }
        }
    }
}
