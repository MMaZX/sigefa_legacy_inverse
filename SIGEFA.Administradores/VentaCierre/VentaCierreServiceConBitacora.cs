using System;
using System.Collections.Generic;
using System.IO;
using SIGEFA.Administradores.VentaCierreBitacora;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.VentaCierre;

// Cierre de venta de la ruta nueva que además deja su bitácora en la BD (una fila
// por cada codPedido del cierre). No cambia el resultado del cierre: toda la
// bitácora corre dentro de try/catch propio y, si no se puede armar, el cierre
// se ejecuta normal y sin bitácora. El fallo original se relanza sin alterarlo.
public sealed class VentaCierreServiceConBitacora : VentaCierreService
{
    private readonly int? _codUsuario;
    private readonly string _usuario;
    private readonly string _equipo;
    private readonly string _versionApp;

    public VentaCierreServiceConBitacora(int? codUsuario, string usuario, string equipo, string versionApp)
    {
        _codUsuario = codUsuario;
        _usuario = usuario;
        _equipo = equipo;
        _versionApp = versionApp;
    }

    public override IList<VentaCierreResultado> ejecutarOrdenAtomica(
        IList<VentaCierreDatosBloque> bloques,
        IProgress<VentaCierreProgreso> progreso = null)
    {
        // Iniciar va antes de lanzar el servicio y en este mismo hilo (requisito de BitacoraCierre).
        SesionBitacora sesion = SesionBitacora.Abrir(bloques, _codUsuario, _usuario, _equipo, _versionApp);
        if (sesion == null)
        {
            return base.ejecutarOrdenAtomica(bloques, progreso);
        }

        IList<VentaCierreResultado> resultados;
        try
        {
            resultados = base.ejecutarOrdenAtomica(bloques, sesion.CrearProgreso(progreso));
        }
        catch (Exception ex)
        {
            sesion.RegistrarFallo(ex);
            throw;
        }

        sesion.RegistrarExito(resultados);
        return resultados;
    }

    // Las bitácoras abiertas de un cierre (una por pedido). Ningún método lanza.
    private sealed class SesionBitacora
    {
        private readonly IList<int> _pedidoPorBloque;
        private readonly Dictionary<int, BitacoraCierre> _bitacoraPorPedido;
        private readonly Func<DateTime> _reloj;
        private readonly DateTime _inicio;

        private SesionBitacora(
            IList<int> pedidoPorBloque, Dictionary<int, BitacoraCierre> bitacoraPorPedido,
            Func<DateTime> reloj, DateTime inicio)
        {
            _pedidoPorBloque = pedidoPorBloque;
            _bitacoraPorPedido = bitacoraPorPedido;
            _reloj = reloj;
            _inicio = inicio;
        }

        // Devuelve null si no se pudo armar la bitácora: el cierre sigue sin ella.
        public static SesionBitacora Abrir(
            IList<VentaCierreDatosBloque> bloques, int? codUsuario, string usuario, string equipo, string versionApp)
        {
            try
            {
                IList<int> pedidoPorBloque = PedidosPorBloque(bloques);
                if (pedidoPorBloque == null)
                {
                    return null;
                }

                Func<DateTime> reloj = () => DateTime.Now;
                var repositorio = new BitacoraRepositorioMySql(new ConsultorMySql(clsConexionMysql.sConex));
                var respaldo = new RespaldoArchivo(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "documentos", "SIGEFA_LOGS"));
                DateTime inicio = reloj();

                var bitacoras = new Dictionary<int, BitacoraCierre>();
                foreach (KeyValuePair<int, int> par in BloquesPorPedido(pedidoPorBloque))
                {
                    var bitacora = new BitacoraCierre(repositorio, respaldo, reloj);
                    bitacora.Iniciar(new IntentoBitacora(
                        par.Key, codUsuario, usuario, equipo, versionApp, par.Value, inicio));
                    bitacoras[par.Key] = bitacora;
                }

                return new SesionBitacora(pedidoPorBloque, bitacoras, reloj, inicio);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IProgress<VentaCierreProgreso> CrearProgreso(IProgress<VentaCierreProgreso> interno)
        {
            return new ProgresoPorPedido(
                new List<int>(_pedidoPorBloque), _bitacoraPorPedido, interno);
        }

        // Éxito: cada pedido cierra con sus bloques y la factura de su primer bloque.
        public void RegistrarExito(IList<VentaCierreResultado> resultados)
        {
            try
            {
                DateTime fin = _reloj();
                foreach (KeyValuePair<int, BitacoraCierre> par in _bitacoraPorPedido)
                {
                    int? codFactura = FacturaDelPedido(par.Key, resultados);
                    par.Value.Finalizar(ProgresoConBitacora.DesdeExito(
                        BloquesPorPedido(_pedidoPorBloque)[par.Key], codFactura, _inicio, fin));
                }
            }
            catch (Exception)
            {
            }
        }

        // Fallo: la transacción es una sola y hace rollback total, así que todos los pedidos fallan igual.
        public void RegistrarFallo(Exception ex)
        {
            try
            {
                DateTime fin = _reloj();
                foreach (BitacoraCierre bitacora in _bitacoraPorPedido.Values)
                {
                    bitacora.Finalizar(ResultadoDeFallo(ex, fin));
                }
            }
            catch (Exception)
            {
            }
        }

        private ResultadoBitacora ResultadoDeFallo(Exception ex, DateTime fin)
        {
            var cierre = ex as VentaCierreException;
            if (cierre == null)
            {
                return ProgresoConBitacora.DesdeExcepcion(ex, 0, _inicio, fin);
            }

            string mensaje = string.IsNullOrEmpty(cierre.mysqlMensaje) ? cierre.Message : cierre.mysqlMensaje;
            int? mysqlNumero = cierre.mysqlNumero > 0 ? cierre.mysqlNumero : (int?)null;
            return ProgresoConBitacora.DesdeFallo(
                cierre.paso.ToString(), cierre.procedimiento, mysqlNumero, cierre.sqlState,
                mensaje, 0, _inicio, fin);
        }

        // Factura del primer bloque del pedido (los resultados van por índice de bloque).
        private int? FacturaDelPedido(int codPedido, IList<VentaCierreResultado> resultados)
        {
            int indice = _pedidoPorBloque.IndexOf(codPedido);
            if (resultados == null || indice < 0 || indice >= resultados.Count || resultados[indice] == null)
            {
                return null;
            }

            return resultados[indice].facturaVentaId;
        }

        // Pedido de cada bloque (índice 0 = bloque 1); null si la orden no es válida (el servicio la rechazará).
        private static IList<int> PedidosPorBloque(IList<VentaCierreDatosBloque> bloques)
        {
            if (bloques == null || bloques.Count == 0)
            {
                return null;
            }

            var pedidos = new List<int>();
            foreach (VentaCierreDatosBloque bloque in bloques)
            {
                if (bloque == null || bloque.venta == null)
                {
                    return null;
                }

                pedidos.Add(bloque.venta.CodPedido);
            }

            return pedidos;
        }

        // Cantidad de bloques de cada codPedido distinto.
        private static Dictionary<int, int> BloquesPorPedido(IList<int> pedidoPorBloque)
        {
            var cuenta = new Dictionary<int, int>();
            foreach (int pedido in pedidoPorBloque)
            {
                int actual;
                cuenta.TryGetValue(pedido, out actual);
                cuenta[pedido] = actual + 1;
            }

            return cuenta;
        }
    }
}
