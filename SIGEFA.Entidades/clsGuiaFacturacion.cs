using System;

namespace SIGEFA.Entidades;

public class clsGuiaFacturacion
{
	public int codGuia { get; set; }

	public int codVenta { get; set; }

	public int codCliente { get; set; }

	public int codOrdenCompra { get; set; }

	public int codSerie { get; set; }

	public string numSerie { get; set; }

	public int correlativo { get; set; }

	public int codalmacen { get; set; }

	public bool estado { get; set; }

	public DateTime fecharegistro { get; set; }

	public DateTime fechaemision { get; set; }

	public int codUsuario { get; set; }

	public decimal tipocambio { get; set; }

	public int codSucursal { get; set; }

	public int codTipoDocumento { get; set; }

	public int codMoneda { get; set; }

	public string comentario { get; set; }

	public decimal valorventa { get; set; }

	public decimal igv { get; set; }

	public decimal total { get; set; }

	public string doctransporte { get; set; }

	public string razonsocialtransporte { get; set; }

	public string codmodotransporte { get; set; }

	public string codmotivotransporte { get; set; }

	public string descripciontransporte { get; set; }

	public DateTime fechatransporte { get; set; }

	public string docconductor { get; set; }

	public string razonsocialcondutor { get; set; }

	public string placa { get; set; }

	public string puntopartida { get; set; }

	public string puntollegada { get; set; }

	public string ubigueollegada { get; set; }

	public bool vehiculomenor { get; set; }

	public string nrolicencia { get; set; }

	public decimal pesobruto { get; set; }

	public int nropallets { get; set; }

	public string estadosunat { get; set; }

	public string apellidoconductor { get; set; }

	public string glosa { get; set; }

	public string NroTicket { get; set; }

	public string NumOc { get; set; }

	public decimal Flete { get; set; }

	public string RazonSocial { get; set; }

	public string Direccion { get; set; }

	public string RucDni { get; set; }

	public string NumOrdenCompra { get; set; }

	public string Referencia { get; set; }
}
