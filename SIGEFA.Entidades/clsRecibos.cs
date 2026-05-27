using System;

namespace SIGEFA.Entidades;

public class clsRecibos
{
	private string nombre;

	private string direccion;

	private string dni;

	private int igresoegreso;

	private int correlativo;

	private int codtipopagocajahica;

	private decimal saldocaja;

	private int iCodRecibos;

	private int iCodRecibosNuevo;

	private string sConcepto;

	private decimal dMonto;

	private DateTime dtFecha;

	private bool bEstado;

	private int iCodUser;

	private int iCodSucursal;

	private int icodTipoDocumento;

	private int iCodSerie;

	private string sNumeracion;

	private bool bAnulado;

	private decimal dMontoPendiente;

	private decimal dMontoRendido;

	private bool bSustentado;

	private int iTipoCaja;

	public decimal Saldocaja
	{
		get
		{
			return saldocaja;
		}
		set
		{
			saldocaja = value;
		}
	}

	public int Codtipopagocajahica
	{
		get
		{
			return codtipopagocajahica;
		}
		set
		{
			codtipopagocajahica = value;
		}
	}

	public int Correlativo
	{
		get
		{
			return correlativo;
		}
		set
		{
			correlativo = value;
		}
	}

	public int Igresoegreso
	{
		get
		{
			return igresoegreso;
		}
		set
		{
			igresoegreso = value;
		}
	}

	public string Dni
	{
		get
		{
			return dni;
		}
		set
		{
			dni = value;
		}
	}

	public string Direccion
	{
		get
		{
			return direccion;
		}
		set
		{
			direccion = value;
		}
	}

	public string Nombre
	{
		get
		{
			return nombre;
		}
		set
		{
			nombre = value;
		}
	}

	public int CodRecibos
	{
		get
		{
			return iCodRecibos;
		}
		set
		{
			iCodRecibos = value;
		}
	}

	public int CodRecibosNuevo
	{
		get
		{
			return iCodRecibosNuevo;
		}
		set
		{
			iCodRecibosNuevo = value;
		}
	}

	public string Concepto
	{
		get
		{
			return sConcepto;
		}
		set
		{
			sConcepto = value;
		}
	}

	public decimal Monto
	{
		get
		{
			return dMonto;
		}
		set
		{
			dMonto = value;
		}
	}

	public DateTime Fecha
	{
		get
		{
			return dtFecha;
		}
		set
		{
			dtFecha = value;
		}
	}

	public bool Estado
	{
		get
		{
			return bEstado;
		}
		set
		{
			bEstado = value;
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

	public int CodSucursal
	{
		get
		{
			return iCodSucursal;
		}
		set
		{
			iCodSucursal = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return icodTipoDocumento;
		}
		set
		{
			icodTipoDocumento = value;
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

	public string Numeracion
	{
		get
		{
			return sNumeracion;
		}
		set
		{
			sNumeracion = value;
		}
	}

	public bool Anulado
	{
		get
		{
			return bAnulado;
		}
		set
		{
			bAnulado = value;
		}
	}

	public decimal MontoPendiente
	{
		get
		{
			return dMontoPendiente;
		}
		set
		{
			dMontoPendiente = value;
		}
	}

	public decimal MontoRendido
	{
		get
		{
			return dMontoRendido;
		}
		set
		{
			dMontoRendido = value;
		}
	}

	public bool Sustentado
	{
		get
		{
			return bSustentado;
		}
		set
		{
			bSustentado = value;
		}
	}

	public int TipoCaja
	{
		get
		{
			return iTipoCaja;
		}
		set
		{
			iTipoCaja = value;
		}
	}
}
