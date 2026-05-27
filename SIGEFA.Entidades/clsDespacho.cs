using System;

namespace SIGEFA.Entidades;

internal class clsDespacho
{
	private int codDespacho;

	private int codCliente;

	private int codAlmacenRegistro;

	private DateTime fechaDespacho;

	private DateTime fechaRegistro;

	private int codTablaDocRelacionada;

	private int codDocRelacionado;

	private int estado;

	private int codSerie;

	private string serie;

	private string numeracion;

	private string razonSocial;

	private string tituloAlmacenRegistro;

	private string nombreCliente;

	private string tituloDocRelacionado;

	private int codUserRegistro;

	private string userRegistro;

	private string comentario;

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

	public int CodCliente
	{
		get
		{
			return codCliente;
		}
		set
		{
			codCliente = value;
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

	public DateTime FechaDespacho
	{
		get
		{
			return fechaDespacho;
		}
		set
		{
			fechaDespacho = value;
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

	public int CodTablaDocRelacionada
	{
		get
		{
			return codTablaDocRelacionada;
		}
		set
		{
			codTablaDocRelacionada = value;
		}
	}

	public int CodDocRelacionado
	{
		get
		{
			return codDocRelacionado;
		}
		set
		{
			codDocRelacionado = value;
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

	public string TituloDocRelacionado
	{
		get
		{
			return tituloDocRelacionado;
		}
		set
		{
			tituloDocRelacionado = value;
		}
	}

	public string RazonSocial
	{
		get
		{
			return razonSocial;
		}
		set
		{
			razonSocial = value;
		}
	}

	public string TituloAlmacenRegistro
	{
		get
		{
			return tituloAlmacenRegistro;
		}
		set
		{
			tituloAlmacenRegistro = value;
		}
	}

	public string RucDni { get; internal set; }

	public int codReqAlmRelacionado { get; internal set; }

	public string NombreContacto { get; internal set; }

	public string TelefonoContacto { get; internal set; }

	public string TituloReqAlmacen { get; internal set; }

	public int CodEstado { get; internal set; }

	public string DescripEstado { get; internal set; }

	public int Anulado { get; internal set; }

	public string DescripNotaCredito { get; internal set; }

	public int CodUserRegistro
	{
		get
		{
			return codUserRegistro;
		}
		set
		{
			codUserRegistro = value;
		}
	}

	public string UserRegistro
	{
		get
		{
			return userRegistro;
		}
		set
		{
			userRegistro = value;
		}
	}

	public string DireccionDelivery { get; internal set; }

	public string CodNotaCredito { get; internal set; }

	public string CodNotaIngresoNC { get; internal set; }

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

	public string Comentario
	{
		get
		{
			return comentario;
		}
		set
		{
			comentario = value;
		}
	}
}
