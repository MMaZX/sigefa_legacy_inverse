namespace SIGEFA.Entidades;

public struct DetalleModificarPrecio
{
	internal int codProducto;

	internal int codUnidadMedida;

	internal int codUnidadEquivalente;

	internal double fleteNuevo;

	internal double precioCompraNuevo;

	internal double precioVentaNuevo;

	internal double precioVenta_competencia;

	internal string precioVenta_sku;

	internal string link_competencia;

	internal int codordencompra;
}
