using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;

namespace SIGEFA.Librerias;

internal class SunatPersona
{
	public enum Resul
	{
		Ok,
		NoResul,
		ErrorCapcha,
		Error
	}

	private Resul state;

	private bool _ok;

	private string _error;

	private string _RazonSocial;

	private string _TipoContribuyente;

	private string _TipoDocumento;

	private string _NumeroDocumento;

	private string _NombreComercial;

	private string _FechaInscpricion;

	private string _FechaInicioActividades;

	private string _EstadoContribuyente;

	private string _CondicionContribuyente;

	private string _DomicilioFiscal;

	private string _SistemaEmisionComprobante;

	private string _ActividadComercioExterior;

	private string _SistemaContabilidad;

	private string _ProfesionuOficio;

	private string _ActividadesEconomicas;

	private string _ComprobantesPago;

	private string _SistemaEmisionElectronica;

	private string _EmisorElectronicoDesde;

	private string _ComprobantesElectronicos;

	private string _AfiliadoPLE;

	private string _Padrones;

	private string _AgenteRetencion;

	private string _AgentePercepcion;

	private string _AfectoNuevoRus;

	private CookieContainer myCookie;

	private string xRazonSocial = "";

	private string xTipoContribuyente = "";

	private string xTipoDocumento = "";

	private string xNumeroDocumento = "";

	private string xNombreComercial = "";

	private string xFechaInscpricion = "";

	private string xFechaInicioActividades = "";

	private string xEstadoContribuyente = "";

	private string xCondicionContribuyente = "";

	private string xDomicilioFiscal = "";

	private string xSistemaEmisionComprobante = "";

	private string xActividadComercioExterior = "";

	private string xSistemaContabilidad = "";

	private string xProfesionuOficio = "";

	private string xActividadesEconomicas = "";

	private string xComprobantesPago = "";

	private string xSistemaEmisionElectronica = "";

	private string xEmisorElectronicoDesde = "";

	private string xComprobantesElectronicos = "";

	private string xAfiliadoPLE = "";

	private string xPadrones = "";

	private string xAgenteretencion = "";

	private string xAgentepercepcion = "";

	private string xAfectonuevorus = "";

	public Image GetCapcha => ReadCapcha();

	public string RazonSocial => _RazonSocial;

	public string TipoContribuyente => _TipoContribuyente;

	public string TipoDocumento => _TipoDocumento;

	public string NumeroDocumento => _NumeroDocumento;

	public string NombreComercial => _NombreComercial;

	public string FechaIscripcion => _FechaInscpricion;

	public string FechaInicioActividades => _FechaInicioActividades;

	public string EstadoContribuyente => _EstadoContribuyente;

	public string CondicionContribuyente => _CondicionContribuyente;

	public string DomicilioFiscal => _DomicilioFiscal;

	public string SistemaEmisionComprobante => _SistemaEmisionComprobante;

	public string ActividadComercioExterior => _ActividadComercioExterior;

	public string SistemaContabilidad => _SistemaContabilidad;

	public string ProfesionuOficio => _ProfesionuOficio;

	public string ActividadesEconomicas => _ActividadesEconomicas;

	public string ComprobantesPago => _ComprobantesPago;

	public string SistemaEmisionElectronica => _SistemaEmisionElectronica;

	public string EmisorElectronicoDesde => _EmisorElectronicoDesde;

	public string ComprobantesElectronicos => _ComprobantesElectronicos;

	public string AfiliadoPLE => _AfiliadoPLE;

	public string Padrones => _Padrones;

	public string AgenteRetencion => _AgenteRetencion;

	public string AgentePercepcion => _AgentePercepcion;

	public string AfectoNuevoRus => _AfectoNuevoRus;

	public Resul GetResul => state;

	public string Error
	{
		get
		{
			if (_ok)
			{
				return string.Empty;
			}
			return _error;
		}
	}

	public SunatPersona()
	{
		try
		{
			myCookie = null;
			myCookie = new CookieContainer();
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
			ReadCapcha();
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private bool ValidarCertificado(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	private Image ReadCapcha()
	{
		try
		{
			ServicePointManager.ServerCertificateValidationCallback = ValidarCertificado;
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image&magic=2");
			myWebRequest.CookieContainer = myCookie;
			myWebRequest.Proxy = null;
			myWebRequest.Credentials = CredentialCache.DefaultCredentials;
			HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
			Stream myImgStream = myWebResponse.GetResponseStream();
			return Image.FromStream(myImgStream);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void GetInfo(string numRuc, string ImgCapcha)
	{
		try
		{
			string myUrl = $"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc={numRuc}&codigo={ImgCapcha}";
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
			myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
			myWebRequest.CookieContainer = myCookie;
			myWebRequest.Credentials = CredentialCache.DefaultCredentials;
			myWebRequest.Proxy = null;
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
			Stream myStream = myHttpWebResponse.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myStream);
			string xDat = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
			if (xDat.Length <= 635)
			{
				return;
			}
			xDat = xDat.Replace("     ", " ");
			xDat = xDat.Replace("    ", " ");
			xDat = xDat.Replace("   ", " ");
			xDat = xDat.Replace("  ", " ");
			xDat = xDat.Replace("( ", "(");
			xDat = xDat.Replace(" )", ")");
			xDat = xDat.Replace("\r", "");
			xDat = xDat.Replace("\n", "");
			xDat = xDat.Replace("\t", "");
			xDat = xDat.Replace("\r\n ", "");
			xDat = xDat.Replace("      ", "");
			xDat = xDat.Replace("   ", "");
			xDat = xDat.Replace("> <", "><");
			xDat = xDat.Replace(">  <", "><");
			xDat = xDat.Replace("</option><option value=\"00\" >", " /");
			xDat = xDat.Replace("<!-- JRR - 20/09/2010 - Se a\ufffdade cambio de Igor -->", "");
			xDat = xDat.Replace("<!-- -->", "");
			string[] tabla = Regex.Split(xDat, "<td class");
			List<string> _resul = new List<string>();
			for (int i = 0; i < tabla.Length; i++)
			{
				if (!string.IsNullOrEmpty(tabla[i].Trim()))
				{
					_resul.Add(tabla[i].Trim());
				}
			}
			if (_resul.Count == 4)
			{
				_ok = false;
				_error = " El número de RUC " + numRuc + " consultado no es válido. Debe verificar el número y volver a ingresar. ";
			}
			else if (_resul[0].Contains("Consulta RUC"))
			{
				_ok = true;
			}
			else if (_resul[0].Contains("El codigo ingresado es incorrecto"))
			{
				_ok = false;
				_error = "La aplicación ha retornado el siguiente mensaje : El Código Ingresado es Incorrecto";
			}
			switch (_resul.Count)
			{
			case 1:
				state = Resul.ErrorCapcha;
				break;
			case 4:
				state = Resul.NoResul;
				break;
			case 84:
				state = Resul.Ok;
				break;
			case 76:
				state = Resul.Ok;
				break;
			case 78:
				state = Resul.Ok;
				break;
			default:
				state = Resul.Error;
				break;
			}
			if (numRuc.StartsWith("1") && state == Resul.Ok && _resul[8].Contains("Afecto al Nuevo RUS"))
			{
				tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>", "");
				tabla[1] = tabla[1].Replace("</td></tr><tr>", "");
				tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
				tabla[3] = tabla[3].Replace("</td></tr><tr>", "");
				tabla[5] = tabla[5].Replace("=\"bg\" colspan=3>", "");
				tabla[5] = tabla[5].Replace("</td></tr><tr>", "");
				tabla[7] = tabla[7].Replace("=\"bg\" colspan=1>", "");
				tabla[7] = tabla[7].Replace("</td>", "");
				tabla[9] = tabla[9].Replace("=\"bg\" colspan=1>", "");
				tabla[9] = tabla[9].Replace("</td></tr><tr>", "");
				tabla[11] = tabla[11].Replace("=\"bg\" colspan=1>", "");
				tabla[11] = tabla[11].Replace("</td><td width=\"27%\" colspan=1 class=\"bgn\">Fecha de Inicio de Actividades:</td>", "");
				tabla[12] = tabla[12].Replace("=\"bg\" colspan=1>", "");
				tabla[12] = tabla[12].Replace("</td></tr><tr>", "");
				tabla[14] = tabla[14].Replace("=\"bg\" colspan=1>", "");
				tabla[14] = tabla[14].Replace("</td>", "");
				tabla[17] = tabla[17].Replace("=\"bg\" colspan=3>", "");
				tabla[17] = tabla[17].Replace("</td></tr><tr>", "");
				tabla[19] = tabla[19].Replace("=\"bg\" colspan=3>", "");
				tabla[19] = tabla[19].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
				tabla[25] = tabla[25].Replace("=\"bg\" colspan=1>", "");
				tabla[25] = tabla[25].Replace("</td>", "");
				tabla[27] = tabla[27].Replace("=\"bg\" colspan=1>", "");
				tabla[27] = tabla[27].Replace("</td></tr><tr>", "");
				tabla[29] = tabla[29].Replace("=\"bg\" colspan=1>", "");
				tabla[29] = tabla[29].Replace("</td></tr><tr>", "");
				tabla[31] = tabla[31].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[31] = tabla[31].Replace("</option></select></td></tr><tr>", "");
				tabla[33] = tabla[33].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
				tabla[33] = tabla[33].Replace("</option></select></td></tr><!-- PCR Inicio Cambios --><tr>", "");
				if (tabla[35].Contains("option value"))
				{
					tabla[35] = tabla[35].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
					tabla[35] = tabla[35].Replace("</option></select></td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				else
				{
					tabla[35] = tabla[35].Replace("=\"bg\" colspan=3>", "");
					tabla[35] = tabla[35].Replace("</td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				tabla[37] = tabla[37].Replace("=\"bg\" colspan=3>", "");
				tabla[37] = tabla[37].Replace("</td></tr><tr>", "");
				tabla[39] = tabla[39].Replace("=\"bg\" colspan=3>", "");
				tabla[39] = tabla[39].Replace("</td></tr><!-- MLR Fin Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				tabla[41] = tabla[41].Replace("=\"bg\" colspan=3>", "");
				tabla[41] = tabla[41].Replace("</td></tr><!-- PCR Fin Cambios --><tr>", "");
				tabla[43] = tabla[43].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[43] = tabla[43].Replace("</option></select><div id=\"print\" style=\"display:none;\"><table cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" class=\"form-table\"><tr>", "");
				xRazonSocial = tabla[1].Substring(13);
				xTipoContribuyente = tabla[3];
				xTipoDocumento = tabla[5].Substring(0, 3);
				xNumeroDocumento = tabla[5].Substring(4, 9);
				xNombreComercial = tabla[7];
				xAfectonuevorus = tabla[9];
				xFechaInscpricion = tabla[11];
				xFechaInicioActividades = tabla[12];
				xEstadoContribuyente = tabla[14].Trim();
				xCondicionContribuyente = tabla[17].Trim();
				xDomicilioFiscal = tabla[19];
				xSistemaEmisionComprobante = tabla[25];
				xActividadComercioExterior = tabla[27];
				xSistemaContabilidad = tabla[29];
				xActividadesEconomicas = tabla[31];
				xComprobantesPago = tabla[33];
				xSistemaEmisionElectronica = tabla[35];
				xEmisorElectronicoDesde = tabla[37];
				xComprobantesElectronicos = tabla[39];
				xAfiliadoPLE = tabla[41];
				xPadrones = tabla[43];
				if (xPadrones.Equals("NINGUNO"))
				{
					xAgenteretencion = "NO";
				}
				else
				{
					xAgenteretencion = "SI";
				}
			}
			else if (numRuc.StartsWith("1") && state == Resul.Ok)
			{
				tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>", "");
				tabla[1] = tabla[1].Replace("</td></tr><tr>", "");
				tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
				tabla[3] = tabla[3].Replace("</td></tr><tr>", "");
				tabla[5] = tabla[5].Replace("=\"bg\" colspan=3>", "");
				tabla[5] = tabla[5].Replace("</td></tr><tr>", "");
				tabla[7] = tabla[7].Replace("=\"bg\" colspan=1>", "");
				tabla[7] = tabla[7].Replace("</td></tr><tr>", "");
				tabla[9] = tabla[9].Replace("=\"bg\" colspan=1>", "");
				tabla[9] = tabla[9].Replace("</td><td width=\"27%\" colspan=1 class=\"bgn\">Fecha de Inicio de Actividades:</td>", "");
				tabla[10] = tabla[10].Replace("=\"bg\" colspan=1> ", "");
				tabla[10] = tabla[10].Replace("</td></tr><tr>", "");
				tabla[12] = tabla[12].Replace("=\"bg\" colspan=1>", "");
				tabla[12] = tabla[12].Replace("</td>", "");
				if (tabla[15].Contains("Deberá declarar el nuevo domicilio"))
				{
					tabla[15] = tabla[15].Replace("=\"bg\" colspan=3><a target=\"_blank\" href=\"http://www.sunat.gob.pe/orientacion/Nohallados/index.html\" title=\"Deberá declarar el nuevo domicilio fiscal o confirmar el señalado en el RUC. Para ello, deberá acercarse a los Centros de Servicios al Contribuyente con los documentos que sustenten el nuevo domicilio.\" >", "");
					tabla[15] = tabla[15].Replace("</a></td></tr><tr>", "");
				}
				else
				{
					tabla[15] = tabla[15].Replace("=\"bg\" colspan=3>", "");
					tabla[15] = tabla[15].Replace("</td></tr><tr>", "");
				}
				tabla[17] = tabla[17].Replace("=\"bg\" colspan=3>", "");
				tabla[17] = tabla[17].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
				tabla[23] = tabla[23].Replace("=\"bg\" colspan=1>", "");
				tabla[23] = tabla[23].Replace("</td>", "");
				tabla[25] = tabla[25].Replace("=\"bg\" colspan=1>", "");
				tabla[25] = tabla[25].Replace("</td></tr><tr>", "");
				tabla[27] = tabla[27].Replace("=\"bg\" colspan=1>", "");
				tabla[27] = tabla[27].Replace("</td>", "");
				tabla[29] = tabla[29].Replace("=\"bg\" colspan=1>", "");
				tabla[29] = tabla[29].Replace("</td></tr><tr>", "");
				tabla[31] = tabla[31].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[31] = tabla[31].Replace("</option></select></td></tr><tr>", "");
				tabla[33] = tabla[33].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
				tabla[33] = tabla[33].Replace("</option></select></td></tr><!-- PCR Inicio Cambios --><tr>", "");
				if (tabla[35].Contains("option value"))
				{
					tabla[35] = tabla[35].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
					tabla[35] = tabla[35].Replace("</option></select></td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				else
				{
					tabla[35] = tabla[35].Replace("=\"bg\" colspan=3>", "");
					tabla[35] = tabla[35].Replace("</td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				tabla[37] = tabla[37].Replace("=\"bg\" colspan=3>", "");
				tabla[37] = tabla[37].Replace("</td></tr><tr>", "");
				tabla[39] = tabla[39].Replace("=\"bg\" colspan=3>", "");
				tabla[39] = tabla[39].Replace("</td></tr><!-- MLR Fin Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				tabla[41] = tabla[41].Replace("=\"bg\" colspan=3>", "");
				tabla[41] = tabla[41].Replace("</td></tr><!-- PCR Fin Cambios --><tr>", "");
				tabla[43] = tabla[43].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[43] = tabla[43].Replace("</option></select><div id=\"print\" style=\"display:none;\"><table cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" class=\"form-table\"><tr>", "");
				xRazonSocial = tabla[1].Substring(13);
				xTipoContribuyente = tabla[3];
				xTipoDocumento = tabla[5].Substring(0, 3);
				xNumeroDocumento = tabla[5].Substring(4, 9);
				xNombreComercial = tabla[7];
				xFechaInscpricion = tabla[9].Substring(0, 10);
				xFechaInicioActividades = tabla[10];
				xEstadoContribuyente = tabla[12].Trim();
				xCondicionContribuyente = tabla[15].Trim();
				xDomicilioFiscal = tabla[17];
				xSistemaEmisionComprobante = tabla[23];
				xActividadComercioExterior = tabla[25];
				xSistemaContabilidad = tabla[27];
				xProfesionuOficio = tabla[29];
				xActividadesEconomicas = tabla[31];
				xComprobantesPago = tabla[33];
				xSistemaEmisionElectronica = tabla[35];
				xEmisorElectronicoDesde = tabla[37];
				xComprobantesElectronicos = tabla[39];
				xAfiliadoPLE = tabla[41];
				xPadrones = tabla[43];
				if (xPadrones.Equals("NINGUNO"))
				{
					xAgenteretencion = "NO";
				}
				else
				{
					xAgenteretencion = "SI";
				}
				xAfectonuevorus = "";
			}
			else if (numRuc.StartsWith("2") && state == Resul.Ok && (_resul.Count == 84 || _resul.Count == 76))
			{
				tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>", "");
				tabla[1] = tabla[1].Replace("</td></tr><tr>", "");
				tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
				tabla[3] = tabla[3].Replace("</td></tr><tr>", "");
				tabla[5] = tabla[5].Replace("=\"bg\" colspan=1>", "");
				tabla[5] = tabla[5].Replace("</td></tr><tr>", "");
				tabla[7] = tabla[7].Replace("=\"bg\" colspan=1>", "");
				tabla[7] = tabla[7].Replace("</td><td width=\"27%\" colspan=1 class=\"bgn\">Fecha de Inicio de Actividades:</td>", "");
				tabla[8] = tabla[8].Replace("=\"bg\" colspan=1> ", "");
				tabla[8] = tabla[8].Replace("</td></tr><tr>", "");
				tabla[10] = tabla[10].Replace("=\"bg\" colspan=1>", "");
				tabla[10] = tabla[10].Replace("</td>", "");
				tabla[13] = tabla[13].Replace("=\"bg\" colspan=3>", "");
				tabla[13] = tabla[13].Replace("</td></tr><tr>", "");
				tabla[15] = tabla[15].Replace("=\"bg\" colspan=3>", "");
				tabla[15] = tabla[15].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
				tabla[21] = tabla[21].Replace("=\"bg\" colspan=1>", "");
				tabla[21] = tabla[21].Replace("</td>", "");
				tabla[23] = tabla[23].Replace("=\"bg\" colspan=1>", "");
				tabla[23] = tabla[23].Replace("</td></tr><tr>", "");
				tabla[25] = tabla[25].Replace("=\"bg\" colspan=1>", "");
				tabla[25] = tabla[25].Replace("</td></tr><tr>", "");
				tabla[27] = tabla[27].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[27] = tabla[27].Replace("</option></select></td></tr><tr>", "");
				tabla[29] = tabla[29].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
				tabla[29] = tabla[29].Replace("</option></select></td></tr><!-- PCR Inicio Cambios --><tr>", "");
				if (tabla[31].Length > 92)
				{
					tabla[31] = tabla[31].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
					tabla[31] = tabla[31].Replace("</option></select></td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				else
				{
					tabla[31] = tabla[31].Replace("=\"bg\" colspan=3>", "");
					tabla[31] = tabla[31].Replace("</td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				tabla[33] = tabla[33].Replace("=\"bg\" colspan=3>", "");
				tabla[33] = tabla[33].Replace("</td></tr><tr>", "");
				tabla[35] = tabla[35].Replace("=\"bg\" colspan=3>", "");
				tabla[35] = tabla[35].Replace("</td></tr><!-- MLR Fin Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				tabla[37] = tabla[37].Replace("=\"bg\" colspan=3>", "");
				tabla[37] = tabla[37].Replace("</td></tr><!-- PCR Fin Cambios --><tr>", "");
				tabla[39] = tabla[39].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[39] = tabla[39].Replace("</option></select><div id=\"print\" style=\"display:none;\"><table cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" class=\"form-table\"><tr>", "");
				xRazonSocial = tabla[1].Substring(13).Trim();
				xTipoContribuyente = tabla[3];
				xTipoDocumento = "RUC";
				xNumeroDocumento = tabla[1].Substring(0, 11);
				xNombreComercial = tabla[5];
				xFechaInscpricion = tabla[7].Substring(0, 10);
				xFechaInicioActividades = tabla[8];
				xEstadoContribuyente = tabla[10].Trim();
				xCondicionContribuyente = tabla[13].Trim();
				xDomicilioFiscal = tabla[15];
				xSistemaEmisionComprobante = tabla[21];
				xActividadComercioExterior = tabla[23];
				xSistemaContabilidad = tabla[25];
				xProfesionuOficio = "NO APLICA POR SER EMPRESA";
				xActividadesEconomicas = tabla[27];
				xComprobantesPago = tabla[29];
				xSistemaEmisionElectronica = tabla[31];
				xEmisorElectronicoDesde = tabla[33];
				xComprobantesElectronicos = tabla[35];
				xAfiliadoPLE = tabla[37];
				xPadrones = tabla[39];
				if (xPadrones.Equals("NINGUNO"))
				{
					xAgenteretencion = "NO";
					xAgentepercepcion = "NO";
				}
				else if (xPadrones.Contains("Retención de IGV"))
				{
					xAgenteretencion = "SI";
				}
				if (xPadrones.Contains("Percepción de IGV"))
				{
					xAgentepercepcion = "SI";
				}
			}
			else if (numRuc.StartsWith("2") && state == Resul.Ok && _resul.Count == 78)
			{
				tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>", "");
				tabla[1] = tabla[1].Replace("</td></tr><tr>", "");
				tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
				tabla[3] = tabla[3].Replace("</td></tr><tr>", "");
				tabla[5] = tabla[5].Replace("=\"bg\" colspan=1>", "");
				tabla[5] = tabla[5].Replace("</td></tr><tr>", "");
				tabla[7] = tabla[7].Replace("=\"bg\" colspan=1>", "");
				tabla[7] = tabla[7].Replace("</td><td width=\"27%\" colspan=1 class=\"bgn\">Fecha de Inicio de Actividades:</td>", "");
				tabla[8] = tabla[8].Replace("=\"bg\" colspan=1> ", "");
				tabla[8] = tabla[8].Replace("</td></tr><tr>", "");
				tabla[10] = tabla[10].Replace("=\"bg\" colspan=1>", "");
				tabla[10] = tabla[10].Replace("</td>", "");
				if (tabla[14].Contains("http://www.sunat.gob.pe/orientacion/Nohallados/index.html"))
				{
					tabla[14] = tabla[14].Replace("=\"bg\" colspan=3><a target=\"_blank\" href=\"http://www.sunat.gob.pe/orientacion/Nohallados/index.html\" title=\"Deberá declarar el nuevo domicilio fiscal o confirmar el señalado en el RUC. Para ello, deberá acercarse a los Centros de Servicios al Contribuyente con los documentos que sustenten el nuevo domicilio.\" >", "");
					tabla[14] = tabla[14].Replace("</a></td></tr><tr>", "");
				}
				else
				{
					tabla[14] = tabla[14].Replace("=\"bg\" colspan=3>", "");
					tabla[14] = tabla[14].Replace("</td></tr><tr>", "");
				}
				tabla[16] = tabla[16].Replace("=\"bg\" colspan=3>", "");
				tabla[16] = tabla[16].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
				tabla[22] = tabla[22].Replace("=\"bg\" colspan=1>", "");
				tabla[22] = tabla[22].Replace("</td>", "");
				tabla[24] = tabla[24].Replace("=\"bg\" colspan=1>", "");
				tabla[24] = tabla[24].Replace("</td></tr><tr>", "");
				tabla[26] = tabla[26].Replace("=\"bg\" colspan=1>", "");
				tabla[26] = tabla[26].Replace("</td></tr><tr>", "");
				tabla[28] = tabla[28].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[28] = tabla[28].Replace("</option></select></td></tr><tr>", "");
				tabla[30] = tabla[30].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
				tabla[30] = tabla[30].Replace("</option></select></td></tr><!-- PCR Inicio Cambios --><tr>", "");
				if (tabla[31].Length > 92)
				{
					tabla[32] = tabla[32].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
					tabla[32] = tabla[32].Replace("</option></select></td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				else
				{
					tabla[32] = tabla[32].Replace("=\"bg\" colspan=3>", "");
					tabla[32] = tabla[32].Replace("</td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				}
				tabla[34] = tabla[34].Replace("=\"bg\" colspan=3>", "");
				tabla[34] = tabla[34].Replace("</td></tr><tr>", "");
				tabla[36] = tabla[36].Replace("=\"bg\" colspan=3>", "");
				tabla[36] = tabla[36].Replace("</td></tr><!-- MLR Fin Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
				tabla[38] = tabla[38].Replace("=\"bg\" colspan=3>", "");
				tabla[38] = tabla[38].Replace("</td></tr><!-- PCR Fin Cambios --><tr>", "");
				tabla[40] = tabla[40].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
				tabla[40] = tabla[40].Replace("</option></select><div id=\"print\" style=\"display:none;\"><table cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" class=\"form-table\"><tr>", "");
				xRazonSocial = tabla[1].Substring(13).Trim();
				xTipoContribuyente = tabla[3];
				xTipoDocumento = "RUC";
				xNumeroDocumento = tabla[1].Substring(0, 11);
				xNombreComercial = tabla[5];
				xFechaInscpricion = tabla[7].Substring(0, 10);
				xFechaInicioActividades = tabla[8];
				xEstadoContribuyente = tabla[10].Trim();
				xCondicionContribuyente = tabla[14].Trim();
				xDomicilioFiscal = tabla[16];
				xSistemaEmisionComprobante = tabla[22];
				xActividadComercioExterior = tabla[24];
				xSistemaContabilidad = tabla[26];
				xProfesionuOficio = "NO APLICA POR SER EMPRESA";
				xActividadesEconomicas = tabla[28];
				xComprobantesPago = tabla[30];
				xSistemaEmisionElectronica = tabla[32];
				xEmisorElectronicoDesde = tabla[34];
				xComprobantesElectronicos = tabla[36];
				xAfiliadoPLE = tabla[38];
				xPadrones = tabla[40];
				if (xPadrones.Equals("NINGUNO"))
				{
					xAgenteretencion = "NO";
					xAgentepercepcion = "NO";
				}
				else if (xPadrones.Contains("Retención de IGV"))
				{
					xAgenteretencion = "SI";
				}
				else if (xPadrones.Contains("Percepción de IGV"))
				{
					xAgentepercepcion = "SI";
				}
			}
			if (state == Resul.Ok)
			{
				_RazonSocial = xRazonSocial;
				_TipoContribuyente = xTipoContribuyente;
				_TipoDocumento = xTipoDocumento;
				_NumeroDocumento = xNumeroDocumento;
				_NombreComercial = xNombreComercial;
				_FechaInscpricion = xFechaInscpricion;
				_FechaInicioActividades = xFechaInicioActividades;
				_EstadoContribuyente = xEstadoContribuyente;
				_CondicionContribuyente = xCondicionContribuyente;
				_DomicilioFiscal = xDomicilioFiscal;
				_SistemaEmisionComprobante = xSistemaEmisionComprobante;
				_ActividadComercioExterior = xActividadComercioExterior;
				_SistemaContabilidad = xSistemaContabilidad;
				_ProfesionuOficio = xProfesionuOficio;
				_ActividadesEconomicas = xActividadesEconomicas;
				_ComprobantesPago = xComprobantesPago;
				_SistemaEmisionElectronica = xSistemaEmisionElectronica;
				_EmisorElectronicoDesde = xEmisorElectronicoDesde;
				_ComprobantesElectronicos = xComprobantesElectronicos;
				_AfiliadoPLE = xAfiliadoPLE;
				_Padrones = xPadrones;
				_AgenteRetencion = xAgenteretencion;
				_AgentePercepcion = xAgentepercepcion;
				_AfectoNuevoRus = xAfectonuevorus;
			}
		}
		catch (Exception ex)
		{
			_ok = false;
			_error += ex.Message;
		}
	}
}
