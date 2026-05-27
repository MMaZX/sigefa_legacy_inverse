using System;

namespace SIGEFA.Entidades;

internal class clsCajaChicaMov
{
	private int codMovCaja;

	private int codSucursal;

	private int codUser;

	private DateTime fecharegistro;

	private int codcaja;

	private int codPago;

	private string concepto;

	private decimal monto;

	private int codTipoPagoCaja;

	private int estado;

	private string nombre;

	private string NumDocumento;

	private decimal toneladas;

	private int tipo;

	private int tipomovimiento;

	private DateTime fecha;

	private int codSerie;

	private int tipodocumento;

	private string serie;

	private string dni;

	private int codalmacen;

	private int codmoneda;

	private decimal tcventa;

	public decimal Tcventa
	{
		get
		{
			return tcventa;
		}
		set
		{
			tcventa = value;
		}
	}

	public int Codmoneda
	{
		get
		{
			return codmoneda;
		}
		set
		{
			codmoneda = value;
		}
	}

	public int Codalmacen
	{
		get
		{
			return codalmacen;
		}
		set
		{
			codalmacen = value;
		}
	}

	public string Dni
	{
		get
		{
			return dni;
		}
		set
		{
			dni = value;
		}
	}

	public int Tipomovimiento
	{
		get
		{
			return tipomovimiento;
		}
		set
		{
			tipomovimiento = value;
		}
	}

	public DateTime Fecha
	{
		get
		{
			return fecha;
		}
		set
		{
			fecha = value;
		}
	}

	public int Tipodocumento
	{
		get
		{
			return tipodocumento;
		}
		set
		{
			tipodocumento = value;
		}
	}

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

	public string NumDocumento1
	{
		get
		{
			return NumDocumento;
		}
		set
		{
			NumDocumento = value;
		}
	}

	public decimal Monto
	{
		get
		{
			return monto;
		}
		set
		{
			monto = value;
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

	public decimal Toneladas
	{
		get
		{
			return toneladas;
		}
		set
		{
			toneladas = value;
		}
	}

	public int CodTipoPagoCaja
	{
		get
		{
			return codTipoPagoCaja;
		}
		set
		{
			codTipoPagoCaja = value;
		}
	}

	public int Tipo
	{
		get
		{
			return tipo;
		}
		set
		{
			tipo = value;
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

	public int CodMovCaja
	{
		get
		{
			return codMovCaja;
		}
		set
		{
			codMovCaja = value;
		}
	}

	public int CodUser
	{
		get
		{
			return codUser;
		}
		set
		{
			codUser = value;
		}
	}

	public int CodSucursal
	{
		get
		{
			return codSucursal;
		}
		set
		{
			codSucursal = value;
		}
	}

	public int Codcaja
	{
		get
		{
			return codcaja;
		}
		set
		{
			codcaja = value;
		}
	}

	public int CodPago
	{
		get
		{
			return codPago;
		}
		set
		{
			codPago = value;
		}
	}

	public string Concepto
	{
		get
		{
			return concepto;
		}
		set
		{
			concepto = value;
		}
	}
}
