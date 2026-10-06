using System.Collections.Generic;
using System.Linq;
using SIGEFA.Administradores.ReqVenta;
using SIGEFA.Conexion;
using SIGEFA.Tests.Helper;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas de lectura de requerimientos de venta (T2b).
// Solo lectura contra MySQL real (variable SIGEFA_TEST_CONN); se omiten sin ella.
// No hay INSERT, UPDATE, DELETE, CREATE ni DROP en este archivo.
public class ReqVentaConsultasTests
{
    // Crea un consultor de lectura con la misma cadena que usa Db.
    // Db es una clase estatica (no implementa IConsultor), por eso se usa
    // ConsultorMySql con la misma cadena para la clase bajo prueba.
    private static IConsultor NuevoConsultor()
    {
        string cadena = HechoConBdAttribute.CadenaConexion();
        Db.CadenaConexion = cadena;
        return new ConsultorMySql(cadena);
    }

    [HechoConBd]
    public void ObtenerRequerimiento_IdInexistente_DevuelveNulo()
    {
        IConsultor consultor = NuevoConsultor();

        Dictionary<string, object> fila = ReqVentaConsultas.ObtenerRequerimiento(consultor, -1, false);

        Assert.Null(fila);
    }

    [HechoConBd]
    public void ObtenerTransferencias_IdInexistente_DevuelveVacia()
    {
        IConsultor consultor = NuevoConsultor();

        List<Dictionary<string, object>> filas = ReqVentaConsultas.ObtenerTransferencias(consultor, -1, false);

        Assert.NotNull(filas);
        Assert.Empty(filas);
    }

    [HechoConBd]
    public void ObtenerDetalle_IdInexistente_DevuelveVacio()
    {
        IConsultor consultor = NuevoConsultor();

        List<Dictionary<string, object>> filas = ReqVentaConsultas.ObtenerDetalle(consultor, -1);

        Assert.NotNull(filas);
        Assert.Empty(filas);
    }

    [HechoConBd]
    public void ObtenerRequerimiento_6318_DevuelveEstado12Tipo2()
    {
        IConsultor consultor = NuevoConsultor();

        Dictionary<string, object> fila = ReqVentaConsultas.ObtenerRequerimiento(consultor, 6318, false);

        Assert.NotNull(fila);
        Assert.Equal(12, fila.Valor<int>("estado"));
        Assert.Equal(2, fila.Valor<int>("tipo_req"));
    }

    [HechoConBd]
    public void ObtenerTransferencias_6318_UnaOriginalSinExtorno()
    {
        IConsultor consultor = NuevoConsultor();

        List<Dictionary<string, object>> filas = ReqVentaConsultas.ObtenerTransferencias(consultor, 6318, false);

        Assert.Single(filas);
        Assert.Equal(13791, filas[0].Valor<int>("codTransDir"));
        // IS NOT NULL devuelve 1 o 0 numerico; Valor<bool> lo admite (igual que bit(1) en T1d).
        Assert.False(filas[0].Valor<bool>("tiene_extorno"));
    }

    [HechoConBd]
    public void ObtenerTransferencias_11713_DosOriginales()
    {
        IConsultor consultor = NuevoConsultor();

        List<Dictionary<string, object>> filas = ReqVentaConsultas.ObtenerTransferencias(consultor, 11713, false);

        Assert.Equal(2, filas.Count);
        Assert.Contains(filas, f => f.Valor<int>("codTransDir") == 19720);
        Assert.Contains(filas, f => f.Valor<int>("codTransDir") == 19721);
    }

    [HechoConBd]
    public void ObtenerTransferencias_Req1_AlMenosUnaConExtorno()
    {
        IConsultor consultor = NuevoConsultor();

        List<Dictionary<string, object>> filas = ReqVentaConsultas.ObtenerTransferencias(consultor, 1, false);

        Assert.NotEmpty(filas);
        Assert.Contains(filas, f => f.Valor<bool>("tiene_extorno"));
    }

    [HechoConBd]
    public void ObtenerDetalle_6318_ContieneProd2429ConPendAprobUno()
    {
        IConsultor consultor = NuevoConsultor();

        List<Dictionary<string, object>> filas = ReqVentaConsultas.ObtenerDetalle(consultor, 6318);

        Dictionary<string, object> fila = filas.FirstOrDefault(d => d.Valor<int>("cod_producto") == 2429);

        Assert.NotNull(fila);
        Assert.Equal(1.0m, fila.Valor<decimal>("cantidad_pendiente_aprobada"));
    }
}
