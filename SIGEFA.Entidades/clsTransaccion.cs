using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsTransaccion
{
	private int iCodTransaccion;

	private int iCodTransaccionNuevo;

	private string sSigla;

	private string sDescripcion;

	private int iTipo;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private List<int> lConfiguracion;

	private string codsunat;

	public string Codsunat
	{
		get
		{
			return codsunat;
		}
		set
		{
			codsunat = value;
		}
	}

	public int CodTransaccionNuevo
	{
		get
		{
			return iCodTransaccionNuevo;
		}
		set
		{
			iCodTransaccionNuevo = value;
		}
	}

	public int CodTransaccion
	{
		get
		{
			return iCodTransaccion;
		}
		set
		{
			iCodTransaccion = value;
		}
	}

	public string Sigla
	{
		get
		{
			return sSigla;
		}
		set
		{
			sSigla = value;
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

	public List<int> Configuracion
	{
		get
		{
			return lConfiguracion;
		}
		set
		{
			lConfiguracion = value;
		}
	}
}
