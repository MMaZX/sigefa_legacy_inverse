using System;

namespace SIGEFA.Entidades;

public class clsMoneda
{
	private int icodMoneda;

	private int icodPais;

	private string sNombrePais;

	private string sDescripcion;

	private DateTime dtFechaRegistro;

	private int iCodUser;

	private string sUsuario;

	private bool iEstado;

	public int IcodMoneda
	{
		get
		{
			return icodMoneda;
		}
		set
		{
			icodMoneda = value;
		}
	}

	public int IcodPais
	{
		get
		{
			return icodPais;
		}
		set
		{
			icodPais = value;
		}
	}

	public string SNombrePais
	{
		get
		{
			return sNombrePais;
		}
		set
		{
			sNombrePais = value;
		}
	}

	public string SDescripcion
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

	public DateTime DtFechaRegistro
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

	public int ICodUser
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

	public string SUsuario
	{
		get
		{
			return sUsuario;
		}
		set
		{
			sUsuario = value;
		}
	}

	public bool IEstado
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
}
