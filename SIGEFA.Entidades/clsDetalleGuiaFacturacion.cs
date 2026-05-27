using System;

namespace SIGEFA.Entidades;

public class clsDetalleGuiaFacturacion
{
	public int codDetalle { get; set; }

	public int codGuia { get; set; }

	public int codAlmacen { get; set; }

	public int codProducto { get; set; }

	public decimal cantidad { get; set; }

	public decimal preciounitario { get; set; }

	public decimal valorventa { get; set; }

	public decimal igv { get; set; }

	public decimal total { get; set; }

	public bool estado { get; set; }

	public int codUnidad { get; set; }

	public string unidad { get; set; }

	public int codMoneda { get; set; }

	public DateTime fecharegistro { get; set; }

	public string producto { get; set; }
}
