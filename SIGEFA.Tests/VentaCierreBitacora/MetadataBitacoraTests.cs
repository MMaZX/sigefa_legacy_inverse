using System;
using SIGEFA.Administradores.VentaCierreBitacora;
using Xunit;

namespace SIGEFA.Tests.VentaCierreBitacora;

public class MetadataBitacoraTests
{
    [Fact]
    public void ObtenerEquipo_ConNombreNormal_ConservaElNombre()
    {
        string equipo = MetadataBitacora.ObtenerEquipo(() => "CAJA-01");

        Assert.Equal("CAJA-01", equipo);
    }

    [Fact]
    public void ObtenerEquipo_ConProveedorQueLanza_DevuelveDesconocidoSinLanzar()
    {
        int llamadas = 0;
        string equipo = MetadataBitacora.ObtenerEquipo(() =>
        {
            llamadas++;
            throw new InvalidOperationException("Equipo no disponible");
        });

        Assert.Equal("desconocido", equipo);
        Assert.Equal(1, llamadas);
    }

    [Fact]
    public void ObtenerEquipo_ConProveedorNulo_DevuelveDesconocido()
    {
        string equipo = MetadataBitacora.ObtenerEquipo(null);

        Assert.Equal("desconocido", equipo);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ObtenerEquipo_ConNombreNuloOVacio_DevuelveDesconocido(string nombre)
    {
        int llamadas = 0;
        string equipo = MetadataBitacora.ObtenerEquipo(() =>
        {
            llamadas++;
            return nombre;
        });

        Assert.Equal("desconocido", equipo);
        Assert.Equal(1, llamadas);
    }

    [Fact]
    public void ObtenerEquipo_InvocaElProveedorUnaSolaVez()
    {
        int llamadas = 0;
        string equipo = MetadataBitacora.ObtenerEquipo(() =>
        {
            llamadas++;
            return llamadas == 1 ? "CAJA-01" : "OTRO-EQUIPO";
        });

        Assert.Equal("CAJA-01", equipo);
        Assert.Equal(1, llamadas);
    }
}
