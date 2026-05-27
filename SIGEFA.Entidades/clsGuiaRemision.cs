using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsGuiaRemision
{
	private string sCodGuiaRemision;

	private int iCodAlmacen;

	private string sCodigosPedidos;

	private string sCodigosFacturas;

	private int iCodMotivo;

	private int iCodTipoDocumento;

	private string sSiglaDocumento;

	private int iCodSerie;

	private string sSerie;

	private string sNumDoc;

	private int iTipoCliente;

	private int iCodCliente;

	private string sCodigoPersonalizado;

	private string sRUCCliente;

	private string sDNI;

	private string sRazonSocialCliente;

	private string sNombre;

	private string sDireccion;

	private int iCodVehiculoTransporte;

	private int iCodMarca;

	private int iCodModelo;

	private string sPlaca;

	private string sMarca;

	private string sModelo;

	private string sConstanciaInscripcion;

	private int iCodConductor;

	private string sNombreConductor;

	private string sLicencia;

	private int iCodEmpresaTransporte;

	private string sRUCEmpresaTransporte;

	private string sRazonSocialTransporte;

	private string sDireccionTransporte;

	private DateTime dtFechaEmision;

	private DateTime dtFechaTraslado;

	private string sComentario;

	private int iEstado;

	private int iFacturado;

	private int iCodPedido;

	private int iCodFactura;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private int sCodAlmacenDestino;

	private string sNomAlmacenDestino;

	private string sUbicacionAlmacenDest;

	private List<clsDetalleGuiaRemision> lDetalle;

	private int iCodReq;

	private int iCodProveedor;

	private int iCodTransferencia;

	private int iCodOrdenCompra;

	private int codDocumentoRelacionado;

	private int estadoGeneracion;

	private DateTime fechaModificacion;

	private int coduserModificacion;

	private int opcionFlete;

	private double fleteConIgv;

	private double fleteSinIgv;

	public decimal precio { get; set; }

	public string numeroOc { get; set; }

	public DateTime fecharegistro1 { get; set; }

	public DateTime fechaingresoalmacen { get; set; }

	public int codnotaingreso { get; set; }

	public int CodTransferencia
	{
		get
		{
			return iCodTransferencia;
		}
		set
		{
			iCodTransferencia = value;
		}
	}

	public int CodProveedor
	{
		get
		{
			return iCodProveedor;
		}
		set
		{
			iCodProveedor = value;
		}
	}

	public int CodReq
	{
		get
		{
			return iCodReq;
		}
		set
		{
			iCodReq = value;
		}
	}

	public string CodGuiaRemision
	{
		get
		{
			return sCodGuiaRemision;
		}
		set
		{
			sCodGuiaRemision = value;
		}
	}

	public int CodAlmacen
	{
		get
		{
			return iCodAlmacen;
		}
		set
		{
			iCodAlmacen = value;
		}
	}

	public string CodigosPedidos
	{
		get
		{
			return sCodigosPedidos;
		}
		set
		{
			sCodigosPedidos = value;
		}
	}

	public string CodigosFacturas
	{
		get
		{
			return sCodigosFacturas;
		}
		set
		{
			sCodigosFacturas = value;
		}
	}

	public int CodMotivo
	{
		get
		{
			return iCodMotivo;
		}
		set
		{
			iCodMotivo = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return iCodTipoDocumento;
		}
		set
		{
			iCodTipoDocumento = value;
		}
	}

	public string SiglaDocumento
	{
		get
		{
			return sSiglaDocumento;
		}
		set
		{
			sSiglaDocumento = value;
		}
	}

	public int CodSerie
	{
		get
		{
			return iCodSerie;
		}
		set
		{
			iCodSerie = value;
		}
	}

	public string Serie
	{
		get
		{
			return sSerie;
		}
		set
		{
			sSerie = value;
		}
	}

	public string NumDoc
	{
		get
		{
			return sNumDoc;
		}
		set
		{
			sNumDoc = value;
		}
	}

	public int TipoCliente
	{
		get
		{
			return iTipoCliente;
		}
		set
		{
			iTipoCliente = value;
		}
	}

	public int CodCliente
	{
		get
		{
			return iCodCliente;
		}
		set
		{
			iCodCliente = value;
		}
	}

	public string CodigoPersonalizado
	{
		get
		{
			return sCodigoPersonalizado;
		}
		set
		{
			sCodigoPersonalizado = value;
		}
	}

	public string RUCCliente
	{
		get
		{
			return sRUCCliente;
		}
		set
		{
			sRUCCliente = value;
		}
	}

	public string DNI
	{
		get
		{
			return sDNI;
		}
		set
		{
			sDNI = value;
		}
	}

	public string RazonSocialCliente
	{
		get
		{
			return sRazonSocialCliente;
		}
		set
		{
			sRazonSocialCliente = value;
		}
	}

	public string Nombre
	{
		get
		{
			return sNombre;
		}
		set
		{
			sNombre = value;
		}
	}

	public string Direccion
	{
		get
		{
			return sDireccion;
		}
		set
		{
			sDireccion = value;
		}
	}

	public int CodVehiculoTransporte
	{
		get
		{
			return iCodVehiculoTransporte;
		}
		set
		{
			iCodVehiculoTransporte = value;
		}
	}

	public int CodMarca
	{
		get
		{
			return iCodMarca;
		}
		set
		{
			iCodMarca = value;
		}
	}

	public int CodModelo
	{
		get
		{
			return iCodModelo;
		}
		set
		{
			iCodModelo = value;
		}
	}

	public string Placa
	{
		get
		{
			return sPlaca;
		}
		set
		{
			sPlaca = value;
		}
	}

	public string Marca
	{
		get
		{
			return sMarca;
		}
		set
		{
			sMarca = value;
		}
	}

	public string Modelo
	{
		get
		{
			return sModelo;
		}
		set
		{
			sModelo = value;
		}
	}

	public string ConstanciaInscripcion
	{
		get
		{
			return sConstanciaInscripcion;
		}
		set
		{
			sConstanciaInscripcion = value;
		}
	}

	public int CodConductor
	{
		get
		{
			return iCodConductor;
		}
		set
		{
			iCodConductor = value;
		}
	}

	public string NombreConductor
	{
		get
		{
			return sNombreConductor;
		}
		set
		{
			sNombreConductor = value;
		}
	}

	public string Licencia
	{
		get
		{
			return sLicencia;
		}
		set
		{
			sLicencia = value;
		}
	}

	public int CodEmpresaTransporte
	{
		get
		{
			return iCodEmpresaTransporte;
		}
		set
		{
			iCodEmpresaTransporte = value;
		}
	}

	public string RUCEmpresaTransporte
	{
		get
		{
			return sRUCEmpresaTransporte;
		}
		set
		{
			sRUCEmpresaTransporte = value;
		}
	}

	public string RazonSocialTransporte
	{
		get
		{
			return sRazonSocialTransporte;
		}
		set
		{
			sRazonSocialTransporte = value;
		}
	}

	public string DireccionTransporte
	{
		get
		{
			return sDireccionTransporte;
		}
		set
		{
			sDireccionTransporte = value;
		}
	}

	public DateTime FechaEmision
	{
		get
		{
			return dtFechaEmision;
		}
		set
		{
			dtFechaEmision = value;
		}
	}

	public DateTime FechaTraslado
	{
		get
		{
			return dtFechaTraslado;
		}
		set
		{
			dtFechaTraslado = value;
		}
	}

	public string Comentario
	{
		get
		{
			return sComentario;
		}
		set
		{
			sComentario = value;
		}
	}

	public int Estado
	{
		get
		{
			return iEstado;
		}
		set
		{
			iEstado = value;
		}
	}

	public int Facturado
	{
		get
		{
			return iFacturado;
		}
		set
		{
			iFacturado = value;
		}
	}

	public int CodPedido
	{
		get
		{
			return iCodPedido;
		}
		set
		{
			iCodPedido = value;
		}
	}

	public int CodFactura
	{
		get
		{
			return iCodFactura;
		}
		set
		{
			iCodFactura = value;
		}
	}

	public int CodUser
	{
		get
		{
			return iCodUser;
		}
		set
		{
			iCodUser = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return dtFechaRegistro;
		}
		set
		{
			dtFechaRegistro = value;
		}
	}

	public List<clsDetalleGuiaRemision> Detalle
	{
		get
		{
			return lDetalle;
		}
		set
		{
			lDetalle = value;
		}
	}

	public int CodAlmacenDestino
	{
		get
		{
			return sCodAlmacenDestino;
		}
		set
		{
			sCodAlmacenDestino = value;
		}
	}

	public string NomAlmacenDestino
	{
		get
		{
			return sNomAlmacenDestino;
		}
		set
		{
			sNomAlmacenDestino = value;
		}
	}

	public string UbicacionAlmacenDest
	{
		get
		{
			return sUbicacionAlmacenDest;
		}
		set
		{
			sUbicacionAlmacenDest = value;
		}
	}

	public int ICodOrdenCompra
	{
		get
		{
			return iCodOrdenCompra;
		}
		set
		{
			iCodOrdenCompra = value;
		}
	}

	public int CodDocumentoRelacionado
	{
		get
		{
			return codDocumentoRelacionado;
		}
		set
		{
			codDocumentoRelacionado = value;
		}
	}

	public int EstadoGeneracion
	{
		get
		{
			return estadoGeneracion;
		}
		set
		{
			estadoGeneracion = value;
		}
	}

	public DateTime FechaModificacion
	{
		get
		{
			return fechaModificacion;
		}
		set
		{
			fechaModificacion = value;
		}
	}

	public int CodUserModificacion
	{
		get
		{
			return coduserModificacion;
		}
		set
		{
			coduserModificacion = value;
		}
	}

	public int OpcionFlete
	{
		get
		{
			return opcionFlete;
		}
		set
		{
			opcionFlete = value;
		}
	}

	public double FleteConIgv
	{
		get
		{
			return fleteConIgv;
		}
		set
		{
			fleteConIgv = value;
		}
	}

	public double FleteSinIgv
	{
		get
		{
			return fleteSinIgv;
		}
		set
		{
			fleteSinIgv = value;
		}
	}
}
