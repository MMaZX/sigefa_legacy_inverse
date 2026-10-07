using System;
using System.Collections.Generic;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.ReqVenta
{
    // Pruebas del guardado de requerimiento de venta en segundo plano (T6).
    // ReqVentaGuardado replica 1+N de clsAdmRequerimientoAlmacen.insert
    // (GuardaRequerimientoAlmacen + GuardaDetalleRequerimientoAlmacen en una
    // sola transacción) pero devuelve ResultadoOperacion sin mostrar cuadros.
    // Todo con falsos en este archivo; no tocan BD.
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
}
