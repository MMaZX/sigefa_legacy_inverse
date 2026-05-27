using System;

namespace SIGEFA.Entidades;

internal class clsEntrega
{
	private int codEntrega;

	private int codDespacho;

	private int codUsuario;

	private string nombreCliente;

	private int codAlmacenRegistro;

	private DateTime fechaEntrega;

	private DateTime fechaRegistro;

	private int estado;

	private int anulado;

	private int codSerie;

	private string serie;

	private string numeracion;

	private string nombreUsuario;

	public int CodEntrega
	{
		get
		{
			return codEntrega;
		}
		set
		{
			codEntrega = value;
		}
	}

	public int CodDespacho
	{
		get
		{
			return codDespacho;
		}
		set
		{
			codDespacho = value;
		}
	}

	public int CodUsuario
	{
		get
		{
			return codUsuario;
		}
		set
		{
			codUsuario = value;
		}
	}

	public string NombreCliente
	{
		get
		{
			return nombreCliente;
		}
		set
		{
			nombreCliente = value;
		}
	}

	public int CodAlmacenRegistro
	{
		get
		{
			return codAlmacenRegistro;
		}
		set
		{
			codAlmacenRegistro = value;
		}
	}

	public DateTime FechaEntrega
	{
		get
		{
			return fechaEntrega;
		}
		set
		{
			fechaEntrega = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return fechaRegistro;
		}
		set
		{
			fechaRegistro = value;
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

	public int Anulado
	{
		get
		{
			return anulado;
		}
		set
		{
			anulado = value;
		}
	}

	public string NombreUsuario
	{
		get
		{
			return nombreUsuario;
		}
		set
		{
			nombreUsuario = value;
		}
	}

	public int CodUsuarioAnulado { get; internal set; }

	public string NombreUsuarioAnulacion { get; internal set; }

	public DateTime FechaAnulacion { get; internal set; }

	public int CodSerie
	{
		get
		{
			return codSerie;
		}
		set
		{
			codSerie = value;
		}
	}

	public string Serie
	{
		get
		{
			return serie;
		}
		set
		{
			serie = value;
		}
	}

	public string Numeracion
	{
		get
		{
			return numeracion;
		}
		set
		{
			numeracion = value;
		}
	}
}
