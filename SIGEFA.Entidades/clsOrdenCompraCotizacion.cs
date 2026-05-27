using System;

namespace SIGEFA.Entidades;

internal class clsOrdenCompraCotizacion
{
	public int codCotizacion { get; set; }

	public int codOrdenCompra { get; set; }

	public int codSucursal { get; set; }

	public int codAlmacen { get; set; }

	public int codCliente { get; set; }

	public int codTipoDocumento { get; set; }

	public int moneda { get; set; }

	public decimal tipocambio { get; set; }

	public string comentario { get; set; }

	public decimal montodscto { get; set; }

	public decimal igv { get; set; }

	public decimal subtotal { get; set; }

	public decimal total { get; set; }

	public bool estado { get; set; }

	public int formapago { get; set; }

	public DateTime fecharegistro { get; set; }

	public DateTime fechacotizacion { get; set; }

	public int codUsuario { get; set; }

	public int estadoproceso { get; set; }

	public decimal margenganciamonto { get; set; }

	public decimal margengananciaporcentaje { get; set; }

	public string RazonSocialCliente { get; set; }

	public string direccion { get; set; }

	public string numerooccliente { get; set; }
}
