using System;

namespace SIGEFA.Entidades;

internal class clsDetalleArqueFondoFijo
{
	private int coddetallearqueofondofijo;

	private int coddetallearqueofondofijoNuevo;

	private int codarqueofondofijo;

	private int coddinero;

	private int cantidad;

	private decimal importe;

	private int estado;

	private int coduser;

	private DateTime fecharegistro;

	public int Coddetallearqueofondofijo
	{
		get
		{
			return coddetallearqueofondofijo;
		}
		set
		{
			coddetallearqueofondofijo = value;
		}
	}

	public int CoddetallearqueofondofijoNuevo
	{
		get
		{
			return coddetallearqueofondofijoNuevo;
		}
		set
		{
			coddetallearqueofondofijoNuevo = value;
		}
	}

	public int Codarqueofondofijo
	{
		get
		{
			return codarqueofondofijo;
		}
		set
		{
			codarqueofondofijo = value;
		}
	}

	public int Coddinero
	{
		get
		{
			return coddinero;
		}
		set
		{
			coddinero = value;
		}
	}

	public int Cantidad
	{
		get
		{
			return cantidad;
		}
		set
		{
			cantidad = value;
		}
	}

	public decimal Importe
	{
		get
		{
			return importe;
		}
		set
		{
			importe = value;
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
