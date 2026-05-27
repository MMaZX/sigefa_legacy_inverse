using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsOrdenCompra
{
	internal DateTime fechaModificacion;

	public clsUsuario usuario { get; set; }

	public clsProveedor prove { get; set; }

	public clsGuiaRemision guia { get; set; }

	public clsDetalleGuiaRemision detalleguia { get; set; }

	public int estadoOrden { get; set; }

	public string I { get; set; }

	public int CodOrdenCompraNuevo { get; set; }

	public int CodOrdenCompra { get; set; }

	public int CodAlmacen { get; set; }

	public int CodTipoTransaccion { get; set; }

	public string SiglaTransaccion { get; set; }

	public string DescripcionTransaccion { get; set; }

	public int CodTipoDocumento { get; set; }

	public string SiglaDocumento { get; set; }

	public int CodSerie { get; set; }

	public string Serie { get; set; }

	public string NumDoc { get; set; }

	public int CodProveedor { get; set; }

	public string RUCProveedor { get; set; }

	public string RazonSocialProveedor { get; set; }

	public string ReferenciaProveedor { get; set; }

	public string Moneda { get; set; }

	public decimal TipoCambio { get; set; }

	public DateTime FechaIngreso { get; set; }

	public int CodAutorizado { get; set; }

	public string NombreAutorizado { get; set; }

	public int FormaPago { get; set; }

	public DateTime FechaPago { get; set; }

	public string Comentario { get; set; }

	public decimal MontoBruto { get; set; }

	public decimal PorcDscto { get; set; }

	public decimal MontoDscto { get; set; }

	public decimal Igv { get; set; }

	public decimal Flete { get; set; }

	public decimal Total { get; set; }

	public bool Pendiente { get; set; }

	public decimal Abonado { get; set; }

	public bool Estado { get; set; }

	public int Recibido { get; set; }

	public int Cancelado { get; set; }

	public DateTime FechaCancelado { get; set; }

	public int CodUser { get; set; }

	public DateTime FechaRegistro { get; set; }

	public int CodReferencia { get; set; }

	public List<clsDetalleOrdenCompra> Detalle { get; set; }

	public int codEmpresaTransporte { get; internal set; }

	public int fleteTransportista { get; internal set; }

	public int UsuarioModificador { get; internal set; }

	public int codPropuestaPedido { get; internal set; }

	public int Anulado { get; internal set; }

	public decimal tipCambioProv { get; set; }

	public string TituloOc { get; set; }
}
