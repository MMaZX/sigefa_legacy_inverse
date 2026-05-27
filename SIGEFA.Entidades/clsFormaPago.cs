using System;

namespace SIGEFA.Entidades;

public class clsFormaPago
{
	private int iCodFormaPago;

	private string sDescripcion;

	private int iDias;

	private bool bTipo;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private bool btipoaccion;

	public int CodFormaPago
	{
		get
		{
			return iCodFormaPago;
		}
		set
		{
			iCodFormaPago = value;
		}
	}

	public int Dias
	{
		get
		{
			return iDias;
		}
		set
		{
			iDias = value;
		}
	}

	public bool Tipo
	{
		get
		{
			return bTipo;
		}
		set
		{
			bTipo = value;
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

	public bool Tipoaccion
	{
		get
		{
			return btipoaccion;
		}
		set
		{
			btipoaccion = value;
		}
	}
}
