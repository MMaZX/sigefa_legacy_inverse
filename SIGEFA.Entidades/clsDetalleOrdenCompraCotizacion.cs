using System;

namespace SIGEFA.Entidades;

public class clsDetalleOrdenCompraCotizacion
{
	public int codDetalle { get; set; }

	public int codOrdenCompra { get; set; }

	public int codProducto { get; set; }

	public string referencia { get; set; }

	public int codAlmacen { get; set; }

	public int codUnidadMedida { get; set; }

	public decimal cantidad { get; set; }

	public decimal cantidadpendiente { get; set; }

	public decimal preciounitario { get; set; }

	public decimal subtotal { get; set; }

	public decimal igv { get; set; }

	public decimal total { get; set; }

	public decimal montodscto { get; set; }

	public decimal precioreal { get; set; }

	public bool estado { get; set; }

	public bool pendiente { get; set; }

	public int codUser { get; set; }

	public DateTime fecharegistro { get; set; }

	public decimal stockactual { get; set; }

	public int codmarca { get; set; }

	public decimal margenganciamonto { get; set; }

	public decimal margengananciaporcentaje { get; set; }

	public decimal costototal { get; set; }

	public bool productocotizacion { get; set; }
}
