using System;
using System.Collections.Generic;
using System.Reflection;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using Xunit;

namespace SIGEFA.Tests.ReqVenta
{
    // Pruebas del progreso de anulación (T2 de ui-progreso-requerimiento).
    // El servicio y las anulaciones informan cada paso con IProgress<PasoOperacion>;
    // MarcarAnulado queda Listo solo tras confirmar la transacción.
    // Todo con falsos en este archivo; no tocan BD.
    public class ReqVentaProgresoTests
    {
        // Progreso falso y síncrono: registra cada paso informado, en orden.
        private sealed class ProgresoFalso : IProgress<PasoOperacion>
        {
            public readonly List<PasoOperacion> Informados = new List<PasoOperacion>();

            public void Report(PasoOperacion paso)
            {
                Informados.Add(paso);
            }
        }

        // Consultor falso combinado para T2: sirve las filas precargadas según el SQL
        // (camino pendiente y camino con extorno) y devuelve newid configurables.
        private sealed class ConsultorFalso : IConsultor
        {
            public Dictionary<string, object> Requerimiento;
            public List<Dictionary<string, object>> Transferencias = new List<Dictionary<string, object>>();
            public List<Dictionary<string, object>> DetallePendiente = new List<Dictionary<string, object>>();
            public Dictionary<string, object> Cabecera;
            public List<Dictionary<string, object>> Lineas = new List<Dictionary<string, object>>();
            public Dictionary<string, object> StockSolicitante;
            public Dictionary<string, object> StockDespacho;
            public Dictionary<string, object> Factor;
            public int AlmSolicitante = 4;
            public int AlmDespacho = 3;

            public int FilasParaRechazo = 1;
            public int FilasParaStock = 1;
            public int FilasParaMarcado = 1;
            public int NewIdTransferencia = 9001;
            public int NewIdDetalleTransferencia = 9101;
            public int NewIdNotaSalida = 9201;
            public int NewIdDetalleSalida = 9301;
            public int NewIdNotaIngreso = 9401;
            public int NewIdDetalleIngreso = 9501;
            public bool AprobacionOk = true;

            public Consulta Consultar(string sql, object parametros = null)
            {
                if (sql.Contains("CALL GuardaDetalleTransferencia"))
                {
                    return Unica(NuevaFila(NewIdDetalleTransferencia));
                }

                if (sql.Contains("CALL GuardaTransferencia"))
                {
                    return Unica(NuevaFila(NewIdTransferencia));
                }

                if (sql.Contains("CALL GuardaDetalleSalida"))
                {
                    return Unica(NuevaFila(NewIdDetalleSalida));
                }

                if (sql.Contains("CALL GuardaNotaSalida"))
                {
                    return Unica(NuevaFila(NewIdNotaSalida));
                }

                if (sql.Contains("CALL GuardaDetalleIngreso"))
                {
                    return Unica(NuevaFila(NewIdDetalleIngreso));
                }

                if (sql.Contains("CALL GuardaNotaIngreso"))
                {
                    return Unica(NuevaFila(NewIdNotaIngreso));
                }

                if (sql.Contains("FROM req_almacen"))
                {
                    return Unica(Requerimiento);
                }

                if (sql.Contains("FROM detalletransferencia"))
                {
                    return Varias(Lineas);
                }

                if (sql.Contains("FROM detalle_req_almacen"))
                {
                    return Varias(DetallePendiente);
                }

                if (sql.Contains("FROM transferencia"))
                {
                    if (sql.Contains("tiene_extorno"))
                    {
                        return Varias(Transferencias);
                    }

                    if (sql.Contains("EstadoTrnas"))
                    {
                        return Unica(FilaAprobacion());
                    }

                    return Unica(Cabecera);
                }

                if (sql.Contains("FROM productoalmacen"))
                {
                    if (ExtraerAlm(parametros) == AlmDespacho)
                    {
                        return Unica(StockDespacho);
                    }

                    return Unica(StockSolicitante);
                }

                if (sql.Contains("unidadequivalente"))
                {
                    return Unica(Factor);
                }

                return Unica(null);
            }

            public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
            {
                if (sql.Contains("RechazarTransferencia"))
                {
                    return new ResultadoEjecucion(0, FilasParaRechazo);
                }

                if (sql.Contains("AprobarTransferencia"))
                {
                    return new ResultadoEjecucion(0, 1);
                }

                if (sql.Contains("productoalmacen"))
                {
                    return new ResultadoEjecucion(0, FilasParaStock);
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

            public ResultadoAnulacion Ejecutar(Func<IConsultor, ResultadoAnulacion> accion)
            {
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

        // Ejecuta la acción y luego lanza: simula que el commit falla tras escribir todo.
        private sealed class TransaccionQueFallaAlConfirmar
        {
            private readonly IConsultor _consultor;

            public TransaccionQueFallaAlConfirmar(IConsultor consultor)
            {
                _consultor = consultor;
            }

            public ResultadoAnulacion Ejecutar(Func<IConsultor, ResultadoAnulacion> accion)
            {
                ResultadoAnulacion resultado = accion(_consultor);
                if (!resultado.Ok)
                {
                    return resultado;
                }

                throw new InvalidOperationException("falló el commit");
            }
        }

        private static Dictionary<string, object> Requerimiento(int id, int tipoReq, int estado, int solicitante, int despacho)
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "id_req_almacen", id },
                { "estado", estado },
                { "tipo_req", tipoReq },
                { "cod_almacen_solicitante", solicitante },
                { "cod_almacen_despacho", despacho },
                { "codPedidoVenta", null },
                { "codFacturaVenta", null },
            };
        }

        private static Dictionary<string, object> Transferencia(int codTrans, int estado, int pendiente, bool tieneExtorno)
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "codTransDir", codTrans },
                { "codAlmacenOrigen", 3 },
                { "codAlmacenDestino", 4 },
                { "total", 10m },
                { "estado", estado },
                { "pendiente", pendiente },
                { "tiene_extorno", tieneExtorno },
            };
        }

        private static Dictionary<string, object> DetallePendiente(int idDetalle, decimal pendienteAprobada)
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

        private static Dictionary<string, object> Stock(int unidad, decimal actual, decimal disponible)
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "Unidad", unidad },
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

        private static ConsultorFalso PendienteSinMovimientos()
        {
            var consultor = new ConsultorFalso();
            consultor.Requerimiento = Requerimiento(5273, 2, 7, 4, 3);
            consultor.DetallePendiente = new List<Dictionary<string, object>> { DetallePendiente(13907, 0m) };
            return consultor;
        }

        private static ConsultorFalso CasoBaseExtorno()
        {
            var consultor = new ConsultorFalso();
            consultor.Requerimiento = Requerimiento(8416, 2, 13, 4, 3);
            consultor.Transferencias = new List<Dictionary<string, object>> { Transferencia(16098, 1, 0, false) };
            consultor.Cabecera = CabeceraOriginal();
            consultor.Lineas = LineasOriginales();
            consultor.StockSolicitante = Stock(55, 100m, 100m);
            consultor.StockDespacho = Stock(55, 100m, 100m);
            consultor.Factor = Factor(1m);
            return consultor;
        }

        private static string UsuarioFalso(int codUser)
        {
            return "JUAN GR";
        }

        // Último estado informado para la clave; si nunca se informó, es Pendiente.
        private static EstadoPaso UltimoEstado(List<PasoOperacion> informados, string clave)
        {
            EstadoPaso estado = EstadoPaso.Pendiente;
            foreach (PasoOperacion paso in informados)
            {
                if (paso.Clave == clave)
                {
                    estado = paso.Estado;
                }
            }

            return estado;
        }

        private static int Contar(List<PasoOperacion> informados, string clave, EstadoPaso estado)
        {
            int total = 0;
            foreach (PasoOperacion paso in informados)
            {
                if (paso.Clave == clave && paso.Estado == estado)
                {
                    total++;
                }
            }

            return total;
        }

        private static string DetalleDe(List<PasoOperacion> informados, string clave, EstadoPaso estado)
        {
            foreach (PasoOperacion paso in informados)
            {
                if (paso.Clave == clave && paso.Estado == estado)
                {
                    return paso.Detalle;
                }
            }

            return null;
        }

        [Fact]
        public void Anular_PendienteConProgreso_EmiteLosPasosEnOrdenYMarcaListoTrasElCommit()
        {
            ConsultorFalso consultor = PendienteSinMovimientos();
            var transaccion = new TransaccionFalsa(consultor);
            var progreso = new ProgresoFalso();

            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                5273, 18, UsuarioFalso, transaccion.Ejecutar, linea => { }, progreso);

            Assert.True(resultado.Ok, resultado.Mensaje);
            Assert.True(transaccion.Confirmada);

            List<PasoOperacion> informados = progreso.Informados;
            IReadOnlyList<PasoOperacion> catalogo = ReqVentaTextos.PasosAnulacionPendiente();
            Assert.Equal(catalogo.Count * 2, informados.Count);
            for (int i = 0; i < catalogo.Count; i++)
            {
                Assert.Equal(catalogo[i].Clave, informados[i * 2].Clave);
                Assert.Equal(EstadoPaso.EnCurso, informados[i * 2].Estado);
                Assert.Equal(catalogo[i].Texto, informados[i * 2].Texto);
                Assert.Equal(catalogo[i].Clave, informados[i * 2 + 1].Clave);
                Assert.Equal(EstadoPaso.Listo, informados[i * 2 + 1].Estado);
            }

            PasoOperacion ultimo = informados[informados.Count - 1];
            Assert.Equal(ReqVentaTextos.MarcarAnulado, ultimo.Clave);
            Assert.Equal(EstadoPaso.Listo, ultimo.Estado);
        }

        [Fact]
        public void Anular_ExtornoConProgreso_EmiteLosPasosEnOrdenYMarcaListoTrasElCommit()
        {
            ConsultorFalso consultor = CasoBaseExtorno();
            var transaccion = new TransaccionFalsa(consultor);
            var progreso = new ProgresoFalso();

            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                8416, 18, UsuarioFalso, transaccion.Ejecutar, linea => { }, progreso);

            Assert.True(resultado.Ok, resultado.Mensaje);
            Assert.True(transaccion.Confirmada);

            List<PasoOperacion> informados = progreso.Informados;
            IReadOnlyList<PasoOperacion> catalogo = ReqVentaTextos.PasosAnulacionConExtorno();
            Assert.Equal(catalogo.Count * 2, informados.Count);
            for (int i = 0; i < catalogo.Count; i++)
            {
                Assert.Equal(catalogo[i].Clave, informados[i * 2].Clave);
                Assert.Equal(EstadoPaso.EnCurso, informados[i * 2].Estado);
                Assert.Equal(catalogo[i].Texto, informados[i * 2].Texto);
                Assert.Equal(catalogo[i].Clave, informados[i * 2 + 1].Clave);
                Assert.Equal(EstadoPaso.Listo, informados[i * 2 + 1].Estado);
            }

            PasoOperacion ultimo = informados[informados.Count - 1];
            Assert.Equal(ReqVentaTextos.MarcarAnulado, ultimo.Clave);
            Assert.Equal(EstadoPaso.Listo, ultimo.Estado);
        }

        [Fact]
        public void Anular_ExtornoConStockInsuficiente_MarcaRevisarStockEnErrorConDetalle()
        {
            ConsultorFalso consultor = CasoBaseExtorno();
            consultor.StockSolicitante = Stock(55, 1m, 1m);
            var transaccion = new TransaccionFalsa(consultor);
            var progreso = new ProgresoFalso();

            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                8416, 18, UsuarioFalso, transaccion.Ejecutar, linea => { }, progreso);

            Assert.False(resultado.Ok);
            Assert.True(transaccion.Revertida);
            Assert.False(transaccion.Confirmada);

            List<PasoOperacion> informados = progreso.Informados;
            Assert.Equal(EstadoPaso.Listo, UltimoEstado(informados, ReqVentaTextos.ComprobarRequerimiento));
            Assert.Equal(1, Contar(informados, ReqVentaTextos.RevisarStock, EstadoPaso.Error));
            Assert.False(string.IsNullOrWhiteSpace(DetalleDe(informados, ReqVentaTextos.RevisarStock, EstadoPaso.Error)));
            Assert.Equal(0, Contar(informados, ReqVentaTextos.CrearExtorno, EstadoPaso.EnCurso));
            Assert.Equal(0, Contar(informados, ReqVentaTextos.MarcarAnulado, EstadoPaso.Listo));
            Assert.Equal(EstadoPaso.Error, UltimoEstado(informados, ReqVentaTextos.MarcarAnulado));
            Assert.False(string.IsNullOrWhiteSpace(DetalleDe(informados, ReqVentaTextos.MarcarAnulado, EstadoPaso.Error)));
        }

        [Fact]
        public void AnularPendiente_RequerimientoInexistente_MarcaComprobarEnErrorConDetalle()
        {
            var consultor = new ConsultorFalso();
            var progreso = new ProgresoFalso();

            ResultadoAnulacion resultado = ReqVentaAnulacionPendiente.AnularPendiente(consultor, 5273, 1, progreso);

            Assert.False(resultado.Ok);
            Assert.Equal(EstadoPaso.Error, UltimoEstado(progreso.Informados, ReqVentaTextos.ComprobarRequerimiento));
            Assert.False(string.IsNullOrWhiteSpace(DetalleDe(progreso.Informados, ReqVentaTextos.ComprobarRequerimiento, EstadoPaso.Error)));
        }

        [Fact]
        public void Anular_ConProgresoNulo_NoLanzaYDevuelveElResultadoDeSiempre()
        {
            ConsultorFalso pendiente = PendienteSinMovimientos();
            ResultadoAnulacion porServicio = ReqVentaFlujoService.Anular(
                5273, 18, UsuarioFalso, new TransaccionFalsa(pendiente).Ejecutar, linea => { }, null);
            Assert.True(porServicio.Ok, porServicio.Mensaje);

            ResultadoAnulacion directo = ReqVentaAnulacionPendiente.AnularPendiente(PendienteSinMovimientos(), 5273, 1);
            Assert.True(directo.Ok, directo.Mensaje);

            ResultadoAnulacion extorno = ReqVentaAnulacionConExtorno.AnularConExtorno(CasoBaseExtorno(), 8416, 18, null);
            Assert.True(extorno.Ok, extorno.Mensaje);
        }

        [Fact]
        public void Anular_ConCommitFallido_MarcaMarcarAnuladoEnError()
        {
            ConsultorFalso consultor = PendienteSinMovimientos();
            var progreso = new ProgresoFalso();

            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                5273, 18, UsuarioFalso, new TransaccionQueFallaAlConfirmar(consultor).Ejecutar, linea => { }, progreso);

            Assert.False(resultado.Ok);

            List<PasoOperacion> informados = progreso.Informados;
            Assert.Equal(1, Contar(informados, ReqVentaTextos.MarcarAnulado, EstadoPaso.EnCurso));
            Assert.Equal(0, Contar(informados, ReqVentaTextos.MarcarAnulado, EstadoPaso.Listo));
            Assert.Equal(EstadoPaso.Error, UltimoEstado(informados, ReqVentaTextos.MarcarAnulado));
            Assert.False(string.IsNullOrWhiteSpace(DetalleDe(informados, ReqVentaTextos.MarcarAnulado, EstadoPaso.Error)));
        }

        [Fact]
        public void Anular_ConMarcadoFinalFallido_MarcaMarcarAnuladoEnErrorUnaSolaVez()
        {
            ConsultorFalso consultor = PendienteSinMovimientos();
            consultor.FilasParaMarcado = 0;
            var transaccion = new TransaccionFalsa(consultor);
            var progreso = new ProgresoFalso();

            ResultadoAnulacion resultado = ReqVentaFlujoService.Anular(
                5273, 18, UsuarioFalso, transaccion.Ejecutar, linea => { }, progreso);

            Assert.False(resultado.Ok);
            Assert.True(transaccion.Revertida);

            List<PasoOperacion> informados = progreso.Informados;
            Assert.Equal(EstadoPaso.Listo, UltimoEstado(informados, ReqVentaTextos.ComprobarRequerimiento));
            Assert.Equal(EstadoPaso.Listo, UltimoEstado(informados, ReqVentaTextos.RechazarTransferencias));
            Assert.Equal(EstadoPaso.Listo, UltimoEstado(informados, ReqVentaTextos.DevolverReservas));
            Assert.Equal(1, Contar(informados, ReqVentaTextos.MarcarAnulado, EstadoPaso.Error));
            Assert.False(string.IsNullOrWhiteSpace(DetalleDe(informados, ReqVentaTextos.MarcarAnulado, EstadoPaso.Error)));
        }

        [Fact]
        public void PasoPorClave_UsaElTextoDelCatalogoYDetalleSinNulos()
        {
            IReadOnlyList<PasoOperacion> catalogo = ReqVentaTextos.PasosAnulacionConExtorno();

            foreach (PasoOperacion esperado in catalogo)
            {
                PasoOperacion paso = ReqVentaTextos.Paso(esperado.Clave, EstadoPaso.EnCurso);
                Assert.Equal(esperado.Clave, paso.Clave);
                Assert.Equal(esperado.Texto, paso.Texto);
                Assert.Equal(EstadoPaso.EnCurso, paso.Estado);
                Assert.Equal(string.Empty, paso.Detalle);
            }

            PasoOperacion conDetalle = ReqVentaTextos.Paso(ReqVentaTextos.RevisarStock, EstadoPaso.Error, "sin stock");
            Assert.Equal("sin stock", conDetalle.Detalle);
        }
    }
}
