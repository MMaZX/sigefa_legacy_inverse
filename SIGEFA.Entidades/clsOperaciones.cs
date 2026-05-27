using System;

namespace SIGEFA.Entidades;

internal class clsOperaciones
{
	private int codoperacion;

	private string codigo;

	private string descripcion;

	private int estado;

	private int coduser;

	private DateTime fecharegistro;

	public int Codoperacion
	{
		get
		{
			return codoperacion;
		}
		set
		{
			codoperacion = value;
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
