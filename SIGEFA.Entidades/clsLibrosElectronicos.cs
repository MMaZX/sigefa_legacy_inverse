using System;

namespace SIGEFA.Entidades;

internal class clsLibrosElectronicos
{
	private int codlibro;

	private string codsunat;

	private string descripcion;

	private int aplicaanio;

	private int aplicames;

	private int aplicadia;

	private int aplicaoportunidad;

	private int estado;

	private int coduser;

	private DateTime fecharegistro;

	private int codnuevolibro;

	public int Codnuevolibro
	{
		get
		{
			return codnuevolibro;
		}
		set
		{
			codnuevolibro = value;
		}
	}

	public DateTime Fecharegistro
	{
		get
		{
			return fecharegistro;
		}
		set
		{
			fecharegistro = value;
		}
	}

	public int Coduser
	{
		get
		{
			return coduser;
		}
		set
		{
			coduser = value;
		}
	}

	public int Estado
	{
		get
		{
			return estado;
		}
		set
		{
			estado = value;
		}
	}

	public int Aplicaoportunidad
	{
		get
		{
			return aplicaoportunidad;
		}
		set
		{
			aplicaoportunidad = value;
		}
	}

	public int Aplicadia
	{
		get
		{
			return aplicadia;
		}
		set
		{
			aplicadia = value;
		}
	}

	public int Aplicames
	{
		get
		{
			return aplicames;
		}
		set
		{
			aplicames = value;
		}
	}

	public int Aplicaanio
	{
		get
		{
			return aplicaanio;
		}
		set
		{
			aplicaanio = value;
		}
	}

	public string Descripcion
	{
		get
		{
			return descripcion;
		}
		set
		{
			descripcion = value;
		}
	}

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

	public int Codlibro
	{
		get
		{
			return codlibro;
		}
		set
		{
			codlibro = value;
		}
	}
}
