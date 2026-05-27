using System;

namespace SIGEFA.Entidades;

public class clsRepositorio
{
	private int repoid;

	private int tipodoc;

	private DateTime fechaemision;

	private string serie;

	private string correlativo;

	private decimal monto;

	private string estadosunat = "-1";

	private string mensajesunat;

	private byte[] pdf;

	private byte[] xml;

	private int usuario;

	private string nombredoc;

	private byte[] cdr;

	private int codEmpresa;

	private int codSucursal;

	private int codAlmacen;

	private int codFacturaVenta;

	public string CodigoHash { get; set; }

	public DateTime fechaEnvio { get; set; }

	public int id_resumen { get; set; }

	public string ticket { get; set; }

	public string documento { get; set; }

	public int Repoid
	{
		get
		{
			return repoid;
		}
		set
		{
			repoid = value;
		}
	}

	public int Tipodoc
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

	public DateTime Fechaemision
	{
		get
		{
			return fechaemision;
		}
		set
		{
			fechaemision = value;
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

	public string Correlativo
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

	public string Estadosunat
	{
		get
		{
			return estadosunat;
		}
		set
		{
			estadosunat = value;
		}
	}

	public string Mensajesunat
	{
		get
		{
			return mensajesunat;
		}
		set
		{
			mensajesunat = value;
		}
	}

	public byte[] Pdf
	{
		get
		{
			return pdf;
		}
		set
		{
			pdf = value;
		}
	}

	public byte[] Xml
	{
		get
		{
			return xml;
		}
		set
		{
			xml = value;
		}
	}

	public int Usuario
	{
		get
		{
			return usuario;
		}
		set
		{
			usuario = value;
		}
	}

	public string Nombredoc
	{
		get
		{
			return nombredoc;
		}
		set
		{
			nombredoc = value;
		}
	}

	public byte[] CDR
	{
		get
		{
			return cdr;
		}
		set
		{
			cdr = value;
		}
	}

	public int CodEmpresa
	{
		get
		{
			return codEmpresa;
		}
		set
		{
			codEmpresa = value;
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

	public int CodFacturaVenta
	{
		get
		{
			return codFacturaVenta;
		}
		set
		{
			codFacturaVenta = value;
		}
	}

	public string TipDocRelacion { get; set; }

	public string FechaActualiza { get; set; }
}
