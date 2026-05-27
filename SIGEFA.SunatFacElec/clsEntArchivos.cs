using System;

namespace SIGEFA.SunatFacElec;

public class clsEntArchivos
{
	private int codarchivo;

	private int codrelacion;

	private int codrequisito;

	private string nombre;

	private string numerodoc;

	private string tipodoc;

	private string tipoarchivo;

	private string pcruta;

	private int coduser;

	private int estado;

	private DateTime fecharegistro;

	private int nuevocodarchivo;

	private byte[] imagen;

	public int Codrequisito
	{
		get
		{
			return codrequisito;
		}
		set
		{
			codrequisito = value;
		}
	}

	public byte[] Imagen
	{
		get
		{
			return imagen;
		}
		set
		{
			imagen = value;
		}
	}

	public int Nuevocodarchivo
	{
		get
		{
			return nuevocodarchivo;
		}
		set
		{
			nuevocodarchivo = value;
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

	public string Pcruta
	{
		get
		{
			return pcruta;
		}
		set
		{
			pcruta = value;
		}
	}

	public string Tipoarchivo
	{
		get
		{
			return tipoarchivo;
		}
		set
		{
			tipoarchivo = value;
		}
	}

	public string Tipodoc
	{
		get
		{
			return tipodoc;
		}
		set
		{
			tipodoc = value;
		}
	}

	public string Numerodoc
	{
		get
		{
			return numerodoc;
		}
		set
		{
			numerodoc = value;
		}
	}

	public string Nombre
	{
		get
		{
			return nombre;
		}
		set
		{
			nombre = value;
		}
	}

	public int Codrelacion
	{
		get
		{
			return codrelacion;
		}
		set
		{
			codrelacion = value;
		}
	}

	public int Codarchivo
	{
		get
		{
			return codarchivo;
		}
		set
		{
			codarchivo = value;
		}
	}
}
