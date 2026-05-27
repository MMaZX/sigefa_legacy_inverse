using System;

namespace SIGEFA.Entidades;

public class clsTarjetaPago
{
	private int iCodTarjeta;

	private int iCodTarjetaNuevo;

	private string sTipo;

	private string sDescripcion;

	private bool bEstado;

	private int iCoduser;

	private DateTime dFecharegistro;

	private int iCodAlmacen;

	private double dPorcComision;

	private double dAlquilerEquipo;

	public int CodTarjeta
	{
		get
		{
			return iCodTarjeta;
		}
		set
		{
			iCodTarjeta = value;
		}
	}

	public int CodTarjetaNuevo
	{
		get
		{
			return iCodTarjetaNuevo;
		}
		set
		{
			iCodTarjetaNuevo = value;
		}
	}

	public string Tipo
	{
		get
		{
			return sTipo;
		}
		set
		{
			sTipo = value;
		}
	}

	public string Descripcion
	{
		get
		{
			return sDescripcion;
		}
		set
		{
			sDescripcion = value;
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

	public int Coduser
	{
		get
		{
			return iCoduser;
		}
		set
		{
			iCoduser = value;
		}
	}

	public DateTime Fecharegistro
	{
		get
		{
			return dFecharegistro;
		}
		set
		{
			dFecharegistro = value;
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

	public double PorcComision
	{
		get
		{
			return dPorcComision;
		}
		set
		{
			dPorcComision = value;
		}
	}

	public double AlquilerEquipo
	{
		get
		{
			return dAlquilerEquipo;
		}
		set
		{
			dAlquilerEquipo = value;
		}
	}
}
