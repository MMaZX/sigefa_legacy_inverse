using System;

namespace SIGEFA.Entidades;

public class clsDetalleOrdenCompra
{
	public int estadoOrden { get; set; }

	public decimal cantidadpendiente { get; set; }

	public string descripcion { get; set; }

	public int psoli { get; set; }

	public string etiqueta { get; set; }

	public string codEstadoo { get; set; }

	public int etiquetaint { get; set; }

	public int codOrdenNuevo { get; set; }

	public int uno { get; set; }

	public int CodDetalleOrdenCompra { get; set; }

	public int CodProducto { get; set; }

	public int CodOrdenCompra { get; set; }

	public int CodAlmacen { get; set; }

	public int Moneda { get; set; }

	public int UnidadIngresada { get; set; }

	public string SerieLote { get; set; }

	public decimal Cantidad { get; set; }

	public int CodUnidad { get; set; }

	public double PrecioUnitario { get; set; }

	public double Subtotal { get; set; }

	public double Descuento1 { get; set; }

	public double Descuento2 { get; set; }

	public double Descuento3 { get; set; }

	public double MontoDescuento { get; set; }

	public double Igv { get; set; }

	public double Flete { get; set; }

	public double Importe { get; set; }

	public double PrecioReal { get; set; }

	public double ValoReal { get; set; }

	public bool Estado { get; set; }

	public DateTime FechaIngreso { get; set; }

	public DateTime FechaRegistro { get; set; }

	public int CodUser { get; set; }

	public int CodProveedor { get; set; }

	public int TipoPrecio { get; internal set; }
}
