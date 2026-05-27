using System;

namespace SIGEFA.Entidades;

public class clsDestaque
{
	private int iCodDestaque;

	private int iCodDestaqueNuevo;

	private int iCodVendedor;

	private int iCodZona;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodDestaqueNuevo
	{
		get
		{
			return iCodDestaqueNuevo;
		}
		set
		{
			iCodDestaqueNuevo = value;
		}
	}

	public int CodDestaque
	{
		get
		{
			return iCodDestaque;
		}
		set
		{
			iCodDestaque = value;
		}
	}

	public int CodZona
	{
		get
		{
			return iCodZona;
		}
		set
		{
			iCodZona = value;
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
