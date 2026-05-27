using System;

namespace SIGEFA.Entidades;

public class clsVehiculoTransporte
{
	private int iCodVehiculoTransporte;

	private int iCodVehiculoTransporteNuevo;

	private string sPlaca;

	private int iCodMarca;

	private string sMarca;

	private int iCodModelo;

	private string sModelo;

	private int iAño;

	private string sConstanciaInscripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string sSoat;

	private string sConfVehicular;

	private string sMTC;

	public string MTC
	{
		get
		{
			return sMTC;
		}
		set
		{
			sMTC = value;
		}
	}

	public string ConfVehicular
	{
		get
		{
			return sConfVehicular;
		}
		set
		{
			sConfVehicular = value;
		}
	}

	public string Soat
	{
		get
		{
			return sSoat;
		}
		set
		{
			sSoat = value;
		}
	}

	public int CodVehiculoTransporteNuevo
	{
		get
		{
			return iCodVehiculoTransporteNuevo;
		}
		set
		{
			iCodVehiculoTransporteNuevo = value;
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

	public int Año
	{
		get
		{
			return iAño;
		}
		set
		{
			iAño = value;
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
}
