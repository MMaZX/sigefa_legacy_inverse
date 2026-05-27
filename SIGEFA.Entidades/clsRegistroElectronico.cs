using System;

namespace SIGEFA.Entidades;

internal class clsRegistroElectronico
{
	private int codlibroregistronuevo;

	private int codlibroregistro;

	private int codlibros;

	private string codsunat;

	private string descripcion;

	private string codigo;

	private int estado;

	private int coduser;

	private DateTime fecharegistro;

	public int Codlibroregistronuevo
	{
		get
		{
			return codlibroregistronuevo;
		}
		set
		{
			codlibroregistronuevo = value;
		}
	}

	public int Codlibroregistro
	{
		get
		{
			return codlibroregistro;
		}
		set
		{
			codlibroregistro = value;
		}
	}

	public int Codlibros
	{
		get
		{
			return codlibros;
		}
		set
		{
			codlibros = value;
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

	public string Codigo
	{
		get
		{
			return codigo;
		}
		set
		{
			codigo = value;
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
}
