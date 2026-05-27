using System;

namespace SIGEFA.Entidades;

public class clsSerie
{
	private int iCodSerie;

	private int iCodDocumento;

	private int iCodEmpresa;

	private int iCodAlmacen;

	private string sDescripcion;

	private string sSerie;

	private int iInicio;

	private int iFin;

	private int iNumeracion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string sNombreImpresora;

	private string sPaperSize;

	private string sSerieImpresora;

	private bool bPreImpreso;

	public int CodSerie
	{
		get
		{
			return iCodSerie;
		}
		set
		{
			iCodSerie = value;
		}
	}

	public int CodDocumento
	{
		get
		{
			return iCodDocumento;
		}
		set
		{
			iCodDocumento = value;
		}
	}

	public int CodEmpresa
	{
		get
		{
			return iCodEmpresa;
		}
		set
		{
			iCodEmpresa = value;
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

	public string Serie
	{
		get
		{
			return sSerie;
		}
		set
		{
			sSerie = value;
		}
	}

	public string Descripcion
	{
		get
		{
			return sDescripcion;
		}
		set
		{
			sDescripcion = value;
		}
	}

	public string NombreImpresora
	{
		get
		{
			return sNombreImpresora;
		}
		set
		{
			sNombreImpresora = value;
		}
	}

	public string PaperSize
	{
		get
		{
			return sPaperSize;
		}
		set
		{
			sPaperSize = value;
		}
	}

	public int Inicio
	{
		get
		{
			return iInicio;
		}
		set
		{
			iInicio = value;
		}
	}

	public int Fin
	{
		get
		{
			return iFin;
		}
		set
		{
			iFin = value;
		}
	}

	public int Numeracion
	{
		get
		{
			return iNumeracion;
		}
		set
		{
			iNumeracion = value;
		}
	}

	public bool Estado
	{
		get
		{
			return iEstado;
		}
		set
		{
			iEstado = value;
		}
	}

	public int CodUser
	{
		get
		{
			return iCodUser;
		}
		set
		{
			iCodUser = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return dtFechaRegistro;
		}
		set
		{
			dtFechaRegistro = value;
		}
	}

	public string SerieImpresora
	{
		get
		{
			return sSerieImpresora;
		}
		set
		{
			sSerieImpresora = value;
		}
	}

	public bool PreImpreso
	{
		get
		{
			return bPreImpreso;
		}
		set
		{
			bPreImpreso = value;
		}
	}
}
