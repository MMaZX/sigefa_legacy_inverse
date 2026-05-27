using System;

namespace SIGEFA.Entidades;

internal class clsSeparacion
{
	private int icodSeparacion;

	private int icodAlmacen;

	private int icodTipoDocumento;

	private int itipoCliente;

	private int icodCliente;

	private int imoneda;

	private decimal itipoCambio;

	private DateTime ifechaPedido;

	private DateTime ifechaEntrega;

	private decimal ibruto;

	private decimal imontoDescuento;

	private decimal iigv;

	private decimal itotal;

	private int iestado;

	private int ipendiente;

	private int iformaPago;

	private DateTime ifechaPago;

	private int icodUsuario;

	private DateTime ifechaRegistro;

	private int inumeracion;

	private string inumDocumento;

	private int icodTipoTransaccion;

	private string icomentario;

	private int icodVendedor;

	private decimal isaldopendiente;

	private decimal isaldocancelado;

	private int icodSerie;

	private string iserie;

	private string iSigla;

	private string inomCliente;

	public string NomCliente
	{
		get
		{
			return inomCliente;
		}
		set
		{
			inomCliente = value;
		}
	}

	public string Sigla
	{
		get
		{
			return iSigla;
		}
		set
		{
			iSigla = value;
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

	public decimal SaldoPendiente
	{
		get
		{
			return isaldopendiente;
		}
		set
		{
			isaldopendiente = value;
		}
	}

	public decimal SaldoCancelado
	{
		get
		{
			return isaldocancelado;
		}
		set
		{
			isaldocancelado = value;
		}
	}

	public int CodVendedor
	{
		get
		{
			return icodVendedor;
		}
		set
		{
			icodVendedor = value;
		}
	}

	public string Comentario
	{
		get
		{
			return icomentario;
		}
		set
		{
			icomentario = value;
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

	public int TipoCliente
	{
		get
		{
			return itipoCliente;
		}
		set
		{
			itipoCliente = value;
		}
	}

	public int CodCliente
	{
		get
		{
			return icodCliente;
		}
		set
		{
			icodCliente = value;
		}
	}

	public int Moneda
	{
		get
		{
			return imoneda;
		}
		set
		{
			imoneda = value;
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

	public DateTime FechaPedido
	{
		get
		{
			return ifechaPedido;
		}
		set
		{
			ifechaPedido = value;
		}
	}

	public DateTime FechaEntrega
	{
		get
		{
			return ifechaEntrega;
		}
		set
		{
			ifechaEntrega = value;
		}
	}

	public decimal Bruto
	{
		get
		{
			return ibruto;
		}
		set
		{
			ibruto = value;
		}
	}

	public decimal MontoDescuento
	{
		get
		{
			return imontoDescuento;
		}
		set
		{
			imontoDescuento = value;
		}
	}

	public decimal Igv
	{
		get
		{
			return iigv;
		}
		set
		{
			iigv = value;
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

	public int Estado
	{
		get
		{
			return iestado;
		}
		set
		{
			iestado = value;
		}
	}

	public int FormaPago
	{
		get
		{
			return iformaPago;
		}
		set
		{
			iformaPago = value;
		}
	}

	public int Pendiente
	{
		get
		{
			return ipendiente;
		}
		set
		{
			ipendiente = value;
		}
	}

	public DateTime FechaPago
	{
		get
		{
			return ifechaPago;
		}
		set
		{
			ifechaPago = value;
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

	public int Numeracion
	{
		get
		{
			return inumeracion;
		}
		set
		{
			inumeracion = value;
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

	public int CodTipoTransaccion
	{
		get
		{
			return icodTipoTransaccion;
		}
		set
		{
			icodTipoTransaccion = value;
		}
	}
}
