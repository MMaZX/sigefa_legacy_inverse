using System;

namespace SIGEFA.Entidades;

internal class clsCtaCte
{
	private int iCodBanco;

	private string sNombreBanco;

	private int iCodCtaCte;

	private int iCodCtaCteNuevo;

	private string sCtaCte;

	private int iCodMovi;

	private string sMovimiento;

	private string sTipoCuenta;

	private int iMoneda;

	private bool bEstado;

	private int iCoduser;

	private DateTime dFechaRegistro;

	private decimal itipocambio;

	private string iDescrip;

	private decimal iIngreso;

	private decimal iEgreso;

	private decimal iSaldo;

	private decimal dmonto;

	private int tipo;

	private string descTipo;

	private DateTime dFechaMovimiento;

	private int iCodAlmacen;

	private int iCodSucursal;

	private decimal dTipoCVenta;

	private int iTipoProcedencia;

	private DateTime dFechaCierreCaja;

	private string nombre;

	private string direccion;

	private string dni;

	private int igresoegreso;

	private int correlativo;

	private int iCodTipoPagoServicio;

	private string iNumTransaccion;

	public int Correlativo
	{
		get
		{
			return correlativo;
		}
		set
		{
			correlativo = value;
		}
	}

	public int Igresoegreso
	{
		get
		{
			return igresoegreso;
		}
		set
		{
			igresoegreso = value;
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

	public string Direccion
	{
		get
		{
			return direccion;
		}
		set
		{
			direccion = value;
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

	public decimal TipoCVenta
	{
		get
		{
			return dTipoCVenta;
		}
		set
		{
			dTipoCVenta = value;
		}
	}

	public string NumTransaccion
	{
		get
		{
			return iNumTransaccion;
		}
		set
		{
			iNumTransaccion = value;
		}
	}

	public int CodTipoPagoServicio
	{
		get
		{
			return iCodTipoPagoServicio;
		}
		set
		{
			iCodTipoPagoServicio = value;
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

	public decimal Dmonto
	{
		get
		{
			return dmonto;
		}
		set
		{
			dmonto = value;
		}
	}

	public string descripcion
	{
		get
		{
			return iDescrip;
		}
		set
		{
			iDescrip = value;
		}
	}

	public decimal ingreso
	{
		get
		{
			return iIngreso;
		}
		set
		{
			iIngreso = value;
		}
	}

	public decimal egreso
	{
		get
		{
			return iEgreso;
		}
		set
		{
			iEgreso = value;
		}
	}

	public decimal saldo
	{
		get
		{
			return iSaldo;
		}
		set
		{
			iSaldo = value;
		}
	}

	public decimal tipocambio
	{
		get
		{
			return itipocambio;
		}
		set
		{
			itipocambio = value;
		}
	}

	public int CodBanco
	{
		get
		{
			return iCodBanco;
		}
		set
		{
			iCodBanco = value;
		}
	}

	public int CodMovi
	{
		get
		{
			return iCodMovi;
		}
		set
		{
			iCodMovi = value;
		}
	}

	public int CodCtaCte
	{
		get
		{
			return iCodCtaCte;
		}
		set
		{
			iCodCtaCte = value;
		}
	}

	public int CodCtaCteNuevo
	{
		get
		{
			return iCodCtaCteNuevo;
		}
		set
		{
			iCodCtaCteNuevo = value;
		}
	}

	public string CtaCte
	{
		get
		{
			return sCtaCte;
		}
		set
		{
			sCtaCte = value;
		}
	}

	public string TipoCuenta
	{
		get
		{
			return sTipoCuenta;
		}
		set
		{
			sTipoCuenta = value;
		}
	}

	public int Moneda
	{
		get
		{
			return iMoneda;
		}
		set
		{
			iMoneda = value;
		}
	}

	public bool Estado
	{
		get
		{
			return bEstado;
		}
		set
		{
			bEstado = value;
		}
	}

	public int Coduser
	{
		get
		{
			return iCoduser;
		}
		set
		{
			iCoduser = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return dFechaRegistro;
		}
		set
		{
			dFechaRegistro = value;
		}
	}

	public int CodAlmacen
	{
		get
		{
			return iCodAlmacen;
		}
		set
		{
			iCodAlmacen = value;
		}
	}

	public string NombreBanco
	{
		get
		{
			return sNombreBanco;
		}
		set
		{
			sNombreBanco = value;
		}
	}

	public string Movimiento
	{
		get
		{
			return sMovimiento;
		}
		set
		{
			sMovimiento = value;
		}
	}

	public DateTime FechaMovimiento
	{
		get
		{
			return dFechaMovimiento;
		}
		set
		{
			dFechaMovimiento = value;
		}
	}

	public string DescTipo
	{
		get
		{
			return descTipo;
		}
		set
		{
			descTipo = value;
		}
	}

	public int CodSucursal
	{
		get
		{
			return iCodSucursal;
		}
		set
		{
			iCodSucursal = value;
		}
	}

	public int TipoProcedencia
	{
		get
		{
			return iTipoProcedencia;
		}
		set
		{
			iTipoProcedencia = value;
		}
	}

	public DateTime FechaCierreCaja
	{
		get
		{
			return dFechaCierreCaja;
		}
		set
		{
			dFechaCierreCaja = value;
		}
	}
}
