using System;

namespace SIGEFA.Entidades;

public class clsDetalleDocumentoRescom
{
	private int coddetallerescom;

	private int coddocumentorescom;

	private int codFacturaV;

	private int codTipoDocumento;

	private string numDocumento;

	private int codAlmacen;

	private int codCliente;

	private int codSerie;

	private string serie;

	private decimal bruto;

	private decimal montodscto;

	private decimal valorventa;

	private decimal igv;

	private decimal total;

	private bool estado;

	private int codUsuario;

	private DateTime fecharegistro;

	private int coddetallerescomNuevo;

	public int CoddetallerescomNuevo
	{
		get
		{
			return coddetallerescomNuevo;
		}
		set
		{
			coddetallerescomNuevo = value;
		}
	}

	public int Coddetallerescom
	{
		get
		{
			return coddetallerescom;
		}
		set
		{
			coddetallerescom = value;
		}
	}

	public int Coddocumentorescom
	{
		get
		{
			return coddocumentorescom;
		}
		set
		{
			coddocumentorescom = value;
		}
	}

	public int CodFacturaV
	{
		get
		{
			return codFacturaV;
		}
		set
		{
			codFacturaV = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return codTipoDocumento;
		}
		set
		{
			codTipoDocumento = value;
		}
	}

	public string NumDocumento
	{
		get
		{
			return numDocumento;
		}
		set
		{
			numDocumento = value;
		}
	}

	public int CodAlmacen
	{
		get
		{
			return codAlmacen;
		}
		set
		{
			codAlmacen = value;
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

	public decimal Bruto
	{
		get
		{
			return bruto;
		}
		set
		{
			bruto = value;
		}
	}

	public decimal Montodscto
	{
		get
		{
			return montodscto;
		}
		set
		{
			montodscto = value;
		}
	}

	public decimal Valorventa
	{
		get
		{
			return valorventa;
		}
		set
		{
			valorventa = value;
		}
	}

	public decimal Igv
	{
		get
		{
			return igv;
		}
		set
		{
			igv = value;
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

	public bool Estado
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
