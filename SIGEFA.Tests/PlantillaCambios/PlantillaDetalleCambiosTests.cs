using System.Linq;
using SIGEFA.Administradores.PlantillaCambios;
using SIGEFA.Entidades;
using Xunit;

namespace SIGEFA.Tests.PlantillaCambios;

// Reglas del tracker de cambios del detalle de una plantilla ya guardada.
// Sin BD ni WinForms: solo decide que filas se insertan, actualizan o eliminan al guardar.
public class PlantillaDetalleCambiosTests
{
    private static clsDetallePlantillaDeProductos Detalle(int codigoProducto, int codigo = 0, double cantidad = 1)
    {
        return new clsDetallePlantillaDeProductos
        {
            Codigo_Producto = codigoProducto,
            Codigo = codigo,
            Cantidad = cantidad,
        };
    }

    [Fact]
    public void SinCambios_NoHayCambiosPendientes()
    {
        var cambios = new PlantillaDetalleCambios();

        Assert.False(cambios.HayCambios);
    }

    [Fact]
    public void Agregar_ProductoNuevo_QuedaEnInsertar()
    {
        var cambios = new PlantillaDetalleCambios();

        cambios.Agregar(Detalle(10));

        Assert.True(cambios.HayCambios);
        Assert.Equal(new[] { 10 }, cambios.Insertar.Select(x => x.Codigo_Producto).ToArray());
        Assert.Empty(cambios.Actualizar);
        Assert.Empty(cambios.Eliminar);
    }

    [Fact]
    public void Agregar_ProductoYaPendienteDeInsertar_NoLoDuplica()
    {
        var cambios = new PlantillaDetalleCambios();
        cambios.Agregar(Detalle(10));

        cambios.Agregar(Detalle(10));

        Assert.Single(cambios.Insertar);
    }

    [Fact]
    public void Agregar_ProductoMarcadoParaEliminar_PasaAActualizarConservandoElCodigoDeDetalle()
    {
        var cambios = new PlantillaDetalleCambios();
        cambios.Quitar(Detalle(10, codigo: 77));

        cambios.Agregar(Detalle(10, codigo: -1));

        Assert.Empty(cambios.Eliminar);
        Assert.Empty(cambios.Insertar);
        var actualizado = Assert.Single(cambios.Actualizar);
        Assert.Equal(77, actualizado.Codigo);
    }

    [Fact]
    public void Quitar_ProductoRecienAgregado_SoloSeDescarta()
    {
        var cambios = new PlantillaDetalleCambios();
        cambios.Agregar(Detalle(10));

        cambios.Quitar(Detalle(10));

        Assert.False(cambios.HayCambios);
        Assert.Empty(cambios.Eliminar);
    }

    [Fact]
    public void Quitar_ProductoGuardado_QuedaEnEliminar()
    {
        var cambios = new PlantillaDetalleCambios();

        cambios.Quitar(Detalle(10, codigo: 77));

        var eliminado = Assert.Single(cambios.Eliminar);
        Assert.Equal(77, eliminado.Codigo);
    }

    [Fact]
    public void Quitar_ProductoModificado_SaleDeActualizarYQuedaEnEliminar()
    {
        var cambios = new PlantillaDetalleCambios();
        cambios.Modificar(Detalle(10, codigo: 77, cantidad: 5));

        cambios.Quitar(Detalle(10, codigo: 77));

        Assert.Empty(cambios.Actualizar);
        Assert.Single(cambios.Eliminar);
    }

    [Fact]
    public void Modificar_ProductoGuardado_QuedaEnActualizar()
    {
        var cambios = new PlantillaDetalleCambios();

        cambios.Modificar(Detalle(10, codigo: 77, cantidad: 5));

        var actualizado = Assert.Single(cambios.Actualizar);
        Assert.Equal(5, actualizado.Cantidad);
    }

    [Fact]
    public void Modificar_ProductoPendienteDeInsertar_ReemplazaEnInsertar()
    {
        var cambios = new PlantillaDetalleCambios();
        cambios.Agregar(Detalle(10, cantidad: 1));

        cambios.Modificar(Detalle(10, cantidad: 8));

        Assert.Empty(cambios.Actualizar);
        Assert.Equal(8, Assert.Single(cambios.Insertar).Cantidad);
    }

    [Fact]
    public void Modificar_ProductoYaPendienteDeActualizar_ReemplazaConservandoElCodigo()
    {
        var cambios = new PlantillaDetalleCambios();
        cambios.Modificar(Detalle(10, codigo: 77, cantidad: 5));

        cambios.Modificar(Detalle(10, codigo: -1, cantidad: 9));

        var actualizado = Assert.Single(cambios.Actualizar);
        Assert.Equal(77, actualizado.Codigo);
        Assert.Equal(9, actualizado.Cantidad);
    }

    [Fact]
    public void Limpiar_DejaTodoSinPendientes()
    {
        var cambios = new PlantillaDetalleCambios();
        cambios.Agregar(Detalle(10));
        cambios.Modificar(Detalle(11, codigo: 5));
        cambios.Quitar(Detalle(12, codigo: 6));

        cambios.Limpiar();

        Assert.False(cambios.HayCambios);
    }
}
