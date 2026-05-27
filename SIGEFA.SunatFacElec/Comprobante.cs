using System;
using System.Xml;

namespace SIGEFA.SunatFacElec;

public class Comprobante
{
	private string URI_SAT = "http://www.sat.gob.mx/cfd/2";

	private XmlDocument m_xmlDOM;

	private XmlNode Nodo;

	private string DIR_SAT;

	private string DIR_PKI;

	private string Archivo_XSLT;

	private string Archivo_CERT;

	private string Archivo_KEY;

	private string Archivo_XML;

	private string Directorio_Guardado;

	private string Carpeta_xml;

	private string Rutafactura;

	private string vSerie;

	private string vFolio;

	private DateTime vFecha;

	private string vNoAprobacion;

	private int vAnoAprobacion;

	private string vFormaDePago;

	private string vMetodoDePago;

	private string vSubTotal;

	private decimal vDescuento;

	private string vMotivoDescuento;

	private int vTipoComprobante;

	private decimal vTotal;

	private string vDomicilioReceptor;

	private string vDomicilioExpedicion;

	private string vDomicilioFiscalEmisor;

	private decimal vIVA;

	private decimal vIVARetenido;

	private decimal vIEPS;

	private decimal vISRRetenido;

	public string Archivo_XSLT1
	{
		get
		{
			return Archivo_XSLT;
		}
		set
		{
			Archivo_XSLT = value;
		}
	}

	public string Archivo_CERT1
	{
		get
		{
			return Archivo_CERT;
		}
		set
		{
			Archivo_CERT = value;
		}
	}

	public string Archivo_KEY1
	{
		get
		{
			return Archivo_KEY;
		}
		set
		{
			Archivo_KEY = value;
		}
	}

	public string Archivo_XML1
	{
		get
		{
			return Archivo_XML;
		}
		set
		{
			Archivo_XML = value;
		}
	}

	public string Directorio_Guardado1
	{
		get
		{
			return Directorio_Guardado;
		}
		set
		{
			Directorio_Guardado = value;
		}
	}

	public string Carpeta_xml1
	{
		get
		{
			return Carpeta_xml;
		}
		set
		{
			Carpeta_xml = value;
		}
	}

	public string Rutafactura1
	{
		get
		{
			return Rutafactura;
		}
		set
		{
			Rutafactura = value;
		}
	}

	public string VSerie
	{
		get
		{
			return vSerie;
		}
		set
		{
			vSerie = value;
		}
	}

	public string VFolio
	{
		get
		{
			return vFolio;
		}
		set
		{
			vFolio = value;
		}
	}

	public DateTime VFecha
	{
		get
		{
			return vFecha;
		}
		set
		{
			vFecha = value;
		}
	}

	public string VNoAprobacion
	{
		get
		{
			return vNoAprobacion;
		}
		set
		{
			vNoAprobacion = value;
		}
	}

	public int VAnoAprobacion
	{
		get
		{
			return vAnoAprobacion;
		}
		set
		{
			vAnoAprobacion = value;
		}
	}

	public string VFormaDePago
	{
		get
		{
			return vFormaDePago;
		}
		set
		{
			vFormaDePago = value;
		}
	}

	public string VMetodoDePago
	{
		get
		{
			return vMetodoDePago;
		}
		set
		{
			vMetodoDePago = value;
		}
	}

	public string VSubTotal
	{
		get
		{
			return vSubTotal;
		}
		set
		{
			vSubTotal = value;
		}
	}
}
