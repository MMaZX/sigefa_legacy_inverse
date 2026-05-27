using System;

namespace SIGEFA.Entidades;

internal class clsArqueoFondoFijo
{
	private int codarqueofondodijo;

	private int codarqueofondodijoNuevo;

	private string encargado;

	private string horainicio;

	private string horafin;

	private decimal total;

	private int estado;

	private int coduser;

	private DateTime fecharegistro;

	private int codsucursa;

	public int Codarqueofondodijo
	{
		get
		{
			return codarqueofondodijo;
		}
		set
		{
			codarqueofondodijo = value;
		}
	}

	public int CodarqueofondodijoNuevo
	{
		get
		{
			return codarqueofondodijoNuevo;
		}
		set
		{
			codarqueofondodijoNuevo = value;
		}
	}

	public string Encargado
	{
		get
		{
			return encargado;
		}
		set
		{
			encargado = value;
		}
	}

	public string Horainicio
	{
		get
		{
			return horainicio;
		}
		set
		{
			horainicio = value;
		}
	}

	public string Horafin
	{
		get
		{
			return horafin;
		}
		set
		{
			horafin = value;
		}
	}

	public decimal Total
	{
		get
		{
			return total;
		}
		set
		{
			total = value;
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

	public int Codsucursa
	{
		get
		{
			return codsucursa;
		}
		set
		{
			codsucursa = value;
		}
	}
}
