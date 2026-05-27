using System;

namespace SIGEFA.Entidades;

public class clsArqueo
{
	private int iCodArqueo;

	private int iCodArqueoNuevo;

	private int iCodUsuario;

	private DateTime dFecha;

	private int iEstado;

	private string sObservacion;

	private DateTime dFechaRegistro;

	private int iCodAlma;

	private int iCodUsuarioApro;

	public int ICodUsuarioApro
	{
		get
		{
			return iCodUsuarioApro;
		}
		set
		{
			iCodUsuarioApro = value;
		}
	}

	public int ICodAlma
	{
		get
		{
			return iCodAlma;
		}
		set
		{
			iCodAlma = value;
		}
	}

	public int ICodArqueo
	{
		get
		{
			return iCodArqueo;
		}
		set
		{
			iCodArqueo = value;
		}
	}

	public int ICodArqueoNuevo
	{
		get
		{
			return iCodArqueoNuevo;
		}
		set
		{
			iCodArqueoNuevo = value;
		}
	}

	public int ICodUsuario
	{
		get
		{
			return iCodUsuario;
		}
		set
		{
			iCodUsuario = value;
		}
	}

	public DateTime DFecha
	{
		get
		{
			return dFecha;
		}
		set
		{
			dFecha = value;
		}
	}

	public int IEstado
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

	public string SObservacion
	{
		get
		{
			return sObservacion;
		}
		set
		{
			sObservacion = value;
		}
	}

	public DateTime DFechaRegistro
	{
		get
		{
			return dFechaRegistro;
		}
		set
		{
			dFechaRegistro = value;
		}
	}
}
