using System;
using System.Collections.Generic;
using SIGEFA.Administradores.VentaCierreBitacora;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.VentaCierreBitacora;

// Integración del repositorio MySQL de la bitácora contra la BD de pruebas (T3). Usa un pedido
// sintético altísimo y borra al final por cod_pedido (el ON DELETE CASCADE borra los eventos).
// Requiere la migración 2026_10_10_130000_create_venta_cierre_log_tables aplicada en la BD de
// prueba: si las tablas no existen la prueba FALLA, nunca pasa en silencio.
[Collection("BdReqVentaFilasCompartidas")]
public class BitacoraRepositorioMySqlIntegracionTests
{
    private const string MensajeSinMigracion =
        "Aplicar la migración 2026_10_10_130000_create_venta_cierre_log_tables en la BD de prueba";

    private static readonly Random Azar = new Random();

    [HechoConBd]
    public void RepositorioReal_GuardaColumnaPorColumnaYNumeraLosIntentos()
    {
        var consultor = new ConsultorMySql(HechoConBdAttribute.CadenaConexion());
        ExigirTablas(consultor);
        int codPedido = 2000000000 + Azar.Next(0, 90000000);
        try
        {
            var repositorio = new BitacoraRepositorioMySql(consultor);
            var inicio = new DateTime(2026, 10, 10, 14, 3, 22, 123);

            long id1;
            int intento1 = repositorio.CrearIntento(
                new IntentoBitacora(codPedido, 15, "jperez", "PC-01", "2026-10-10 09:30", 2, inicio), out id1);
            long id2;
            int intento2 = repositorio.CrearIntento(
                new IntentoBitacora(codPedido, null, "ana", "PC-02", "2026-10-10 09:30", 1, inicio.AddSeconds(5)), out id2);
            long id3;
            int intento3 = repositorio.CrearIntento(
                new IntentoBitacora(codPedido, 15, "jperez", "PC-01", "2026-10-10 09:30", 2, inicio.AddSeconds(9)), out id3);

            Assert.Equal(new[] { 1, 2, 3 }, new[] { intento1, intento2, intento3 });
            Assert.True(id1 > 0 && id2 > id1 && id3 > id2);

            var cabeceraInicial = LeerCabecera(consultor, id1);
            Assert.Equal(codPedido, cabeceraInicial.Valor<int>("cod_pedido"));
            Assert.Equal(1, cabeceraInicial.Valor<int>("intento"));
            Assert.Equal(15, cabeceraInicial.Valor<int>("cod_usuario"));
            Assert.Equal("jperez", cabeceraInicial.Valor<string>("usuario"));
            Assert.Equal("PC-01", cabeceraInicial.Valor<string>("equipo"));
            Assert.Equal("2026-10-10 09:30", cabeceraInicial.Valor<string>("version_app"));
            Assert.Equal("EN_CURSO", cabeceraInicial.Valor<string>("estado"));
            Assert.Equal(2, cabeceraInicial.Valor<int>("total_bloques"));
            Assert.Equal(inicio, cabeceraInicial.Valor<DateTime>("inicio"));
            Assert.Null(cabeceraInicial.Valor<DateTime?>("fin"));
            Assert.Null(LeerCabecera(consultor, id2).Valor<int?>("cod_usuario"));

            var fin = new DateTime(2026, 10, 10, 14, 3, 25, 789);
            var resultado = new ResultadoBitacora(
                EstadoBitacora.Error, 1, fin, 3666, "guardarDetalle", "GuardaDetalleFacturaVenta",
                1062, "23000", "Duplicate entry", null);
            var eventos = new List<EventoBitacora>
            {
                new EventoBitacora(1, inicio.AddMilliseconds(10), 1, "ALMACEN CENTRAL", "abrirTransaccion",
                    null, null, ResultadoEvento.Info, null, "inicio"),
                new EventoBitacora(2, inicio.AddMilliseconds(250), 1, "ALMACEN CENTRAL", "guardarDetalle",
                    3, 901, ResultadoEvento.Ok, 12, "ítem 3"),
                new EventoBitacora(3, inicio.AddMilliseconds(999), null, null, "confirmar",
                    null, null, ResultadoEvento.Error, null, null),
            };

            repositorio.Finalizar(id1, resultado, eventos);

            var cabecera = LeerCabecera(consultor, id1);
            Assert.Equal("ERROR", cabecera.Valor<string>("estado"));
            Assert.Equal(1, cabecera.Valor<int>("bloques_ok"));
            Assert.Equal(fin, cabecera.Valor<DateTime>("fin"));
            Assert.Equal(3666, cabecera.Valor<int>("duracion_ms"));
            Assert.Equal("guardarDetalle", cabecera.Valor<string>("error_paso"));
            Assert.Equal("GuardaDetalleFacturaVenta", cabecera.Valor<string>("error_procedimiento"));
            Assert.Equal(1062, cabecera.Valor<int>("error_mysql_num"));
            Assert.Equal("23000", cabecera.Valor<string>("error_sqlstate"));
            Assert.Equal("Duplicate entry", cabecera.Valor<string>("error_mensaje"));
            Assert.Null(cabecera.Valor<int?>("cod_factura_venta"));
            // El resto de la cabecera no cambia con el UPDATE.
            Assert.Equal(codPedido, cabecera.Valor<int>("cod_pedido"));
            Assert.Equal("jperez", cabecera.Valor<string>("usuario"));
            Assert.Equal(inicio, cabecera.Valor<DateTime>("inicio"));
            // Los otros intentos siguen en curso.
            Assert.Equal("EN_CURSO", LeerCabecera(consultor, id2).Valor<string>("estado"));

            var filas = consultor.Consultar(
                "SELECT * FROM venta_cierre_log_evento WHERE log_id = @id ORDER BY orden", new { id = id1 }).Get();
            Assert.Equal(3, filas.Count);

            Assert.Equal(id1, filas[0].Valor<long>("log_id"));
            Assert.Equal(1, filas[0].Valor<int>("orden"));
            Assert.Equal(inicio.AddMilliseconds(10), filas[0].Valor<DateTime>("hora"));
            Assert.Equal(1, filas[0].Valor<int>("bloque"));
            Assert.Equal("ALMACEN CENTRAL", filas[0].Valor<string>("almacen"));
            Assert.Equal("abrirTransaccion", filas[0].Valor<string>("paso"));
            Assert.Null(filas[0].Valor<int?>("item"));
            Assert.Null(filas[0].Valor<int?>("cod_producto"));
            Assert.Equal("INFO", filas[0].Valor<string>("resultado"));
            Assert.Null(filas[0].Valor<int?>("duracion_ms"));
            Assert.Equal("inicio", filas[0].Valor<string>("detalle"));

            Assert.Equal(inicio.AddMilliseconds(250), filas[1].Valor<DateTime>("hora"));
            Assert.Equal(3, filas[1].Valor<int>("item"));
            Assert.Equal(901, filas[1].Valor<int>("cod_producto"));
            Assert.Equal("OK", filas[1].Valor<string>("resultado"));
            Assert.Equal(12, filas[1].Valor<int>("duracion_ms"));
            Assert.Equal("ítem 3", filas[1].Valor<string>("detalle"));

            Assert.Equal(inicio.AddMilliseconds(999), filas[2].Valor<DateTime>("hora"));
            Assert.Null(filas[2].Valor<int?>("bloque"));
            Assert.Null(filas[2].Valor<string>("almacen"));
            Assert.Equal("confirmar", filas[2].Valor<string>("paso"));
            Assert.Equal("ERROR", filas[2].Valor<string>("resultado"));
            Assert.Null(filas[2].Valor<string>("detalle"));
        }
        finally
        {
            consultor.Ejecutar("DELETE FROM venta_cierre_log WHERE cod_pedido = @codPedido", new { codPedido });
        }
    }

    [HechoConBd]
    public void RepositorioReal_ConMasDe200EventosLosGuardaTodosEnOrden()
    {
        var consultor = new ConsultorMySql(HechoConBdAttribute.CadenaConexion());
        ExigirTablas(consultor);
        int codPedido = 2000000000 + Azar.Next(0, 90000000);
        try
        {
            var repositorio = new BitacoraRepositorioMySql(consultor);
            var inicio = new DateTime(2026, 10, 10, 8, 0, 0, 0);
            long id;
            repositorio.CrearIntento(new IntentoBitacora(codPedido, 1, "u", "e", "v", 1, inicio), out id);
            var eventos = new List<EventoBitacora>();
            for (int i = 1; i <= 450; i++)
            {
                eventos.Add(new EventoBitacora(
                    i, inicio.AddMilliseconds(i), 1, "A", "guardarDetalle", i, null, ResultadoEvento.Ok, null, null));
            }

            repositorio.Finalizar(id, new ResultadoBitacora(
                EstadoBitacora.Ok, 1, inicio.AddSeconds(1), 1000, null, null, null, null, null, 321), eventos);

            var total = consultor.Consultar(
                "SELECT COUNT(*) AS n, MIN(orden) AS minimo, MAX(orden) AS maximo FROM venta_cierre_log_evento WHERE log_id = @id",
                new { id }).First();
            Assert.Equal(450L, total.Valor<long>("n"));
            Assert.Equal(1, total.Valor<int>("minimo"));
            Assert.Equal(450, total.Valor<int>("maximo"));
            var cabecera = LeerCabecera(consultor, id);
            Assert.Equal("OK", cabecera.Valor<string>("estado"));
            Assert.Equal(321, cabecera.Valor<int>("cod_factura_venta"));
            Assert.Null(cabecera.Valor<string>("error_paso"));
        }
        finally
        {
            consultor.Ejecutar("DELETE FROM venta_cierre_log WHERE cod_pedido = @codPedido", new { codPedido });
        }
    }

    private static Dictionary<string, object> LeerCabecera(IConsultor consultor, long id)
    {
        return consultor.Consultar("SELECT * FROM venta_cierre_log WHERE id = @id", new { id }).First();
    }

    // Falla (no omite, no retorna) si la migración no está aplicada en la BD de prueba.
    private static void ExigirTablas(IConsultor consultor)
    {
        var fila = consultor.Consultar(
            "SELECT COUNT(*) AS n FROM information_schema.tables " +
            "WHERE table_schema = DATABASE() AND table_name IN ('venta_cierre_log', 'venta_cierre_log_evento')").First();
        Assert.True(fila.Valor<long>("n") == 2, MensajeSinMigracion);
    }
}
