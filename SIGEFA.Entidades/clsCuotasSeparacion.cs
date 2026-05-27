using System;

namespace SIGEFA.Entidades;

internal class clsCuotasSeparacion
{
	private int icodCuota;

	private DateTime ifechaRegistro;

	private decimal imonto;

	private int icodSeparacion;

	private int icodUsuario;

	private decimal itotal;

	private int icodMoneda;

	private decimal itipoCambio;

	private int icodAlmacen;

	private int icodSerie;

	private string iserie;

	private string inumDocumento;

	private int icodTipoDocumento;

	private string idesdocumento;

	public string Desdocumento
	{
		get
		{
			return idesdocumento;
		}
		set
		{
			idesdocumento = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return icodTipoDocumento;
		}
		set
		{
			icodTipoDocumento = value;
		}
	}

	public string NumDocumento
	{
		get
		{
			return inumDocumento;
		}
		set
		{
			inumDocumento = value;
		}
	}

	public string Serie
	{
		get
		{
			return iserie;
		}
		set
		{
			iserie = value;
		}
	}

	public int CodSerie
	{
		get
		{
			return icodSerie;
		}
		set
		{
			icodSerie = value;
		}
	}

	public int CodAlmacen
	{
		get
		{
			return icodAlmacen;
		}
		set
		{
			icodAlmacen = value;
		}
	}

	public decimal TipoCambio
	{
		get
		{
			return itipoCambio;
		}
		set
		{
			itipoCambio = value;
		}
	}

	public int CodMoneda
	{
		get
		{
			return icodMoneda;
		}
		set
		{
			icodMoneda = value;
		}
	}

	public decimal Total
	{
		get
		{
			return itotal;
		}
		set
		{
			itotal = value;
		}
	}

	public int CodUsuario
	{
		get
		{
			return icodUsuario;
		}
		set
		{
			icodUsuario = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return ifechaRegistro;
		}
		set
		{
			ifechaRegistro = value;
		}
	}

	public int CodCuota
	{
		get
		{
			return icodCuota;
		}
		set
		{
			icodCuota = value;
		}
	}

	public decimal Monto
	{
		get
		{
			return imonto;
		}
		set
		{
			imonto = value;
		}
	}

	public int CodSeparacion
	{
		get
		{
			return icodSeparacion;
		}
		set
		{
			icodSeparacion = value;
		}
	}
}
