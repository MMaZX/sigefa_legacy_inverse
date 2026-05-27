using System;

namespace SIGEFA.Entidades;

public class clsCliente
{
	private int iCodCliente;

	private int iCodClienteNuevo;

	private int iCodListaPrecio;

	private string sCodigoPersonalizado;

	private int iCodVendedor;

	private int iTipo;

	private string sDni;

	private string sNombre;

	private string sRuc;

	private string sRazonSocial;

	private string sDireccionLegal;

	private string sDireccionEntrega;

	private string sTelefono;

	private string sEmail;

	private string sWeb;

	private string iPais;

	private string iDepartamento;

	private string iProvincia;

	private string iDistrito;

	private int iZona;

	private decimal dDescuento;

	private int iFormaPago;

	private int iMoneda;

	private decimal dLineaCredito;

	private decimal dLineaCreditoDisponible;

	private string sComentario;

	private string sBanco;

	private string sCtaCte;

	private string sContacto;

	private string sTelefonoContacto;

	private int iCalificacion;

	private bool iEstado;

	private bool iHabilitado;

	private int iCantidad;

	private decimal dDeuda;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string sRuc_Dni;

	private decimal dLineaCreditoUsado;

	private bool bClienteFacturasVencidas;

	private decimal dTasa;

	public bool CliEspecial { get; set; }

	public int codCategoriaCliente { get; set; }

	public clsDocumentoIdentidad DocumentoIdentidad { get; set; }

	public decimal Tasa
	{
		get
		{
			return dTasa;
		}
		set
		{
			dTasa = value;
		}
	}

	public int CodClienteNuevo
	{
		get
		{
			return iCodClienteNuevo;
		}
		set
		{
			iCodClienteNuevo = value;
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

	public int CodListaPrecio
	{
		get
		{
			return iCodListaPrecio;
		}
		set
		{
			iCodListaPrecio = value;
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

	public int CodVendedor
	{
		get
		{
			return iCodVendedor;
		}
		set
		{
			iCodVendedor = value;
		}
	}

	public int Tipo
	{
		get
		{
			return iTipo;
		}
		set
		{
			iTipo = value;
		}
	}

	public string Dni
	{
		get
		{
			return sDni;
		}
		set
		{
			sDni = value;
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

	public string Ruc
	{
		get
		{
			return sRuc;
		}
		set
		{
			sRuc = value;
		}
	}

	public string RazonSocial
	{
		get
		{
			return sRazonSocial;
		}
		set
		{
			sRazonSocial = value;
		}
	}

	public string DireccionLegal
	{
		get
		{
			return sDireccionLegal;
		}
		set
		{
			sDireccionLegal = value;
		}
	}

	public string DireccionEntrega
	{
		get
		{
			return sDireccionEntrega;
		}
		set
		{
			sDireccionEntrega = value;
		}
	}

	public string Telefono
	{
		get
		{
			return sTelefono;
		}
		set
		{
			sTelefono = value;
		}
	}

	public string Email
	{
		get
		{
			return sEmail;
		}
		set
		{
			sEmail = value;
		}
	}

	public string Web
	{
		get
		{
			return sWeb;
		}
		set
		{
			sWeb = value;
		}
	}

	public string Pais
	{
		get
		{
			return iPais;
		}
		set
		{
			iPais = value;
		}
	}

	public string Departamento
	{
		get
		{
			return iDepartamento;
		}
		set
		{
			iDepartamento = value;
		}
	}

	public string Provincia
	{
		get
		{
			return iProvincia;
		}
		set
		{
			iProvincia = value;
		}
	}

	public string Distrito
	{
		get
		{
			return iDistrito;
		}
		set
		{
			iDistrito = value;
		}
	}

	public int Zona
	{
		get
		{
			return iZona;
		}
		set
		{
			iZona = value;
		}
	}

	public decimal Descuento
	{
		get
		{
			return dDescuento;
		}
		set
		{
			dDescuento = value;
		}
	}

	public int FormaPago
	{
		get
		{
			return iFormaPago;
		}
		set
		{
			iFormaPago = value;
		}
	}

	public int Moneda
	{
		get
		{
			return iMoneda;
		}
		set
		{
			iMoneda = value;
		}
	}

	public decimal LineaCredito
	{
		get
		{
			return dLineaCredito;
		}
		set
		{
			dLineaCredito = value;
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

	public string Banco
	{
		get
		{
			return sBanco;
		}
		set
		{
			sBanco = value;
		}
	}

	public string CtaCte
	{
		get
		{
			return sCtaCte;
		}
		set
		{
			sCtaCte = value;
		}
	}

	public string Contacto
	{
		get
		{
			return sContacto;
		}
		set
		{
			sContacto = value;
		}
	}

	public string TelefonoContacto
	{
		get
		{
			return sTelefonoContacto;
		}
		set
		{
			sTelefonoContacto = value;
		}
	}

	public int Calificacion
	{
		get
		{
			return iCalificacion;
		}
		set
		{
			iCalificacion = value;
		}
	}

	public bool Estado
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

	public bool Habilitado
	{
		get
		{
			return iHabilitado;
		}
		set
		{
			iHabilitado = value;
		}
	}

	public int Cantidad
	{
		get
		{
			return iCantidad;
		}
		set
		{
			iCantidad = value;
		}
	}

	public decimal Deuda
	{
		get
		{
			return dDeuda;
		}
		set
		{
			dDeuda = value;
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

	public decimal LineaCreditoDisponible
	{
		get
		{
			return dLineaCreditoDisponible;
		}
		set
		{
			dLineaCreditoDisponible = value;
		}
	}

	public string RucDni
	{
		get
		{
			return sRuc_Dni;
		}
		set
		{
			sRuc_Dni = value;
		}
	}

	public decimal LineaCreditoUsado
	{
		get
		{
			return dLineaCreditoUsado;
		}
		set
		{
			dLineaCreditoUsado = value;
		}
	}

	public bool ClienteFacturasVencidas
	{
		get
		{
			return bClienteFacturasVencidas;
		}
		set
		{
			bClienteFacturasVencidas = value;
		}
	}

	public clsCliente()
	{
		DocumentoIdentidad = new clsDocumentoIdentidad();
	}
}
