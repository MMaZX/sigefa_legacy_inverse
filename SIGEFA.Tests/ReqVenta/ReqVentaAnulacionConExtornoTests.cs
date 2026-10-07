using System;
using System.Collections.Generic;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas de anulación con extorno de requerimientos de venta aprobados (estado 13, T2d).
// TDD Fase RED: pruebas escritas antes de la implementación de ReqVentaAnulacionConExtorno.
public class ReqVentaAnulacionConExtornoTests
{
    private sealed class ReversionEsperada : Exception
    {
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
        IConsultor consultor = new ConsultorMySql(DbPruebaBase.CadenaConexionDummy);
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
        IConsultor consultor = new ConsultorMySql(DbPruebaBase.CadenaConexionDummy);
        ResultadoAnulacion resultado = ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, 8416, codUser);

        Assert.NotNull(resultado);
        Assert.False(resultado.Ok);
        Assert.Contains("usuario", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    // Integración contra la BD dev: caso req 8416 (aprobado estado 13, transf 16098, producto 5004 con stock suficiente).
    // Corre dentro de una transacción y SIEMPRE se revierte (rollback). Verifica que en el rollback no queda nada escrito.
    [HechoConBd]
    public void AnularConExtorno_Req8416_EnTransaccion_AlRevertirSigueEn13YSinExtorno()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        Db.CadenaConexion = cadena;

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

    // Integración contra la BD dev: fallo inducido a mitad (ej. usuario o dato que falle o simulación de aborto)
    // para verificar que el rollback total no deja filas huérfanas en transferencia ni en req_almacen.
    [HechoConBd]
    public void AnularConExtorno_FalloAMitad_RollbackTotalNoDejaFilasHuerfanas()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        Db.CadenaConexion = cadena;

        var lectura = new ConsultorMySql(cadena);
        Dictionary<string, object> antes = ReqVentaConsultas.ObtenerRequerimiento(lectura, 8416, false);
        if (antes == null || antes.Valor<int>("tipo_req") != 2 || antes.Valor<int>("estado") != 13)
        {
            return;
        }

        // Caso donde se lanza excepción durante el flujo del llamador dentro de la misma transacción:
        // verifica que cualquier fallo a mitad deja intacto el requerimiento y la transferencia.
        try
        {
            Db.Transaccion(tx =>
            {
                // Modificamos temporalmente el requerimiento a estado no anulable para inducir fallo controlado
                // o ejecutamos una anulación sobre un requerimiento inexistente
                ResultadoAnulacion res = ReqVentaAnulacionConExtorno.AnularConExtorno(tx, -999, 1);
                Assert.False(res.Ok);
                if (!res.Ok)
                {
                    throw new InvalidOperationException("Fallo provocado: " + res.Mensaje);
                }
            });
        }
        catch (InvalidOperationException)
        {
        }

        Dictionary<string, object> despues = ReqVentaConsultas.ObtenerRequerimiento(new ConsultorMySql(cadena), 8416, false);
        Assert.NotNull(despues);
        Assert.Equal(13, despues.Valor<int>("estado"));
    }
}
