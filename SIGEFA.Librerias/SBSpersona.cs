using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SIGEFA.Librerias;

public class SBSpersona
{
	public enum Resul
	{
		Ok,
		NoResul,
		Error
	}

	public enum Method
	{
		GET,
		POST
	}

	private string _ApellidoPaterno;

	private string _ApellidoMaterno;

	private string _PrimerNombre;

	private string _SegundoNombre;

	private string _TipoTrabajador;

	private string _Sexo;

	private string _Nacionalidad;

	private string _LugarNacimiento;

	private string _LugarResidencia;

	private string _EstadoCivil;

	private string _FechaNacimiento;

	private string _FechaDefuncion;

	private string _FechaProcesoDefuncion;

	private string _OrigenAfiliacion;

	private string _EntidadAfiliacion;

	private string _TipoComisionAfiliacion;

	private string _CodigoAfiliacion;

	private string _FechaIngresoAfiliacion;

	private string _SituacionAfiliacion;

	private Resul state;

	private bool _ok;

	private string _error;

	private string xApellidoPaterno = "";

	private string xApellidoMaterno = "";

	private string xPrimerNombre = "";

	private string xSegundoNombre = "";

	private string xTipoTrabajador = "";

	private string xSexo = "";

	private string xNacionalidad = "";

	private string xLugarNacimiento = "";

	private string xLugarResidencia = "";

	private string xEstadoCivil = "";

	private string xFechaNacimiento;

	private string xFechaDefuncion;

	private string xFechaProcesoDefuncion;

	private string xOrigenAfiliacion = "";

	private string xEntidadAfiliacion = "";

	private string xTipoComisionAfiliacion = "";

	private string xCodigoAfiliacion = "";

	private string xFechaIngresoAfiliacion;

	private string xSituacionAfiliacion = "";

	public string ApellidoPaterno => _ApellidoPaterno;

	public string ApellidoMaterno => _ApellidoMaterno;

	public string PrimerNombre => _PrimerNombre;

	public string SegundoNombre => _SegundoNombre;

	public string TipoTrabajador => _TipoTrabajador;

	public string Sexo => _Sexo;

	public string Nacionalidad => _Nacionalidad;

	public string LugarNacimiento => _LugarNacimiento;

	public string LugarResidencia => _LugarResidencia;

	public string EstadoCivil => _EstadoCivil;

	public string FechaNacimiento => _FechaNacimiento;

	public string FechaDefuncion => _FechaDefuncion;

	public string FechaProcesoDefuncion => _FechaProcesoDefuncion;

	public string OrigenAfiliacion => _OrigenAfiliacion;

	public string EntidadAfiliacion => _EntidadAfiliacion;

	public string TipoComisionAfiliacion => _TipoComisionAfiliacion;

	public string CodigoAfiliacion => _CodigoAfiliacion;

	public string FechaIngresoAfiliacion => _FechaIngresoAfiliacion;

	public string SituacionAfiliacion => _SituacionAfiliacion;

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

	public void EnviarDatos(string numDni)
	{
		string UrlBase = "http://www.sbs.gob.pe/app/spp/Afiliados/afil_detalle.asp";
		Dictionary<string, string> Parametros = new Dictionary<string, string>();
		Parametros.Add("tp", "2");
		Parametros.Add("tip_doc", "00");
		Parametros.Add("num_doc", numDni);
		string respuestaServidor = GetResponse(UrlBase, Parametros, Method.POST);
	}

	public string GetResponse(string urlBase, Dictionary<string, string> parameters, Method method)
	{
		return method switch
		{
			Method.GET => GetResponse_GET(urlBase, parameters), 
			Method.POST => ConsultaSBS(urlBase, parameters), 
			_ => throw new NotImplementedException(), 
		};
	}

	private string ConcatParams(Dictionary<string, string> parameters)
	{
		bool FirstParam = true;
		StringBuilder Parametros = null;
		if (parameters != null)
		{
			Parametros = new StringBuilder();
			foreach (KeyValuePair<string, string> param in parameters)
			{
				Parametros.Append(FirstParam ? "" : "&");
				Parametros.Append(param.Key + "=" + HttpUtility.UrlEncode(param.Value));
				FirstParam = false;
			}
		}
		return (Parametros == null) ? string.Empty : Parametros.ToString();
	}

	private bool ValidarCertificado(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	public string GetResponse_GET(string url, Dictionary<string, string> parameters)
	{
		try
		{
			string parametrosConcatenados = ConcatParams(parameters);
			string urlConParametros = url + "?" + parametrosConcatenados;
			WebRequest wr = (HttpWebRequest)WebRequest.Create(urlConParametros);
			wr.Method = "GET";
			wr.ContentType = "application/x-www-form-urlencoded";
			WebResponse response = wr.GetResponse();
			Stream newStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(newStream);
			string responseFromServer = reader.ReadToEnd();
			reader.Close();
			newStream.Close();
			response.Close();
			return responseFromServer;
		}
		catch (HttpException ex)
		{
			if (ex.ErrorCode == 404)
			{
				throw new Exception("Not found remote service " + url);
			}
			throw ex;
		}
	}

	public string ConsultaSBS(string url, Dictionary<string, string> parameters)
	{
		try
		{
			string parametrosConcatenados = ConcatParams(parameters);
			ServicePointManager.ServerCertificateValidationCallback = ValidarCertificado;
			WebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
			myWebRequest.Method = "POST";
			myWebRequest.ContentType = "application/x-www-form-urlencoded";
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] byte1 = encoding.GetBytes(parametrosConcatenados);
			myWebRequest.ContentLength = byte1.Length;
			Stream myStream = myWebRequest.GetRequestStream();
			myStream.Write(byte1, 0, byte1.Length);
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
			myStream = myHttpWebResponse.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myStream);
			string xDat = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
			xDat = xDat.Replace("     ", " ");
			xDat = xDat.Replace("    ", " ");
			xDat = xDat.Replace("   ", " ");
			xDat = xDat.Replace("  ", " ");
			xDat = xDat.Replace("</td>\r\n ", "");
			xDat = xDat.Replace("</tr>\r\n ", "");
			xDat = xDat.Replace("<tr>\r\n ", "");
			xDat = xDat.Replace("width=\"95px\" ", "");
			xDat = xDat.Replace("width=\"20px\">\u00a0", "");
			xDat = xDat.Replace("width=\"75px\" ", "");
			xDat = xDat.Replace("width=\"100px\" ", "");
			xDat = xDat.Replace("width=\"210px\" ", "");
			xDat = xDat.Replace("colspan=\"2\" ", "");
			xDat = xDat.Replace("colspan=\"3\">", "");
			xDat = xDat.Replace("align=\"left\" class=\"APLI_txt", "");
			xDat = xDat.Replace("      ", "");
			xDat = xDat.Replace("   ", "");
			string[] tabla = Regex.Split(xDat, "<td ");
			List<string> _resul = new List<string>();
			for (int i = 0; i < tabla.Length; i++)
			{
				if (!string.IsNullOrEmpty(tabla[i].Trim()))
				{
					_resul.Add(tabla[i].Trim());
				}
			}
			if (_resul.Count == 2)
			{
				_ok = false;
				_error = "Documento de Identidad no registrado en el SPP.";
			}
			else if (_resul[1].Contains("Consulta de Afiliados del SPP"))
			{
				_ok = true;
			}
			switch (_resul.Count)
			{
			case 2:
				state = Resul.NoResul;
				break;
			case 41:
				state = Resul.Ok;
				break;
			default:
				state = Resul.Error;
				break;
			}
			if (state == Resul.Ok)
			{
				tabla[6] = tabla[6].Replace("Actualizado\">", "");
				tabla[8] = tabla[8].Replace("Actualizado\">", "");
				tabla[11] = tabla[11].Replace("Actualizado\">", "");
				tabla[13] = tabla[13].Replace("Actualizado\">", "");
				tabla[35] = tabla[35].Replace("Actualizado\">", "");
				tabla[42] = tabla[42].Replace("Actualizado\">", "");
				tabla[31] = tabla[31].Replace("Actualizado\">", "");
				tabla[18] = tabla[18].Replace("Campo\">Estado\u00a0Civil\u00a0:<span class=\"APLI_txtActualizado\">", "");
				tabla[18] = tabla[18].Replace("</span>", "");
				tabla[45] = tabla[45].Replace("Actualizado\">", "");
				tabla[40] = tabla[40].Replace("Actualizado\">", "");
				tabla[33] = tabla[33].Replace("Actualizado\">", "");
				tabla[33] = tabla[33].Replace("</td> \r\n ", "");
				tabla[37] = tabla[37].Replace("Actualizado\">", "");
				tabla[16] = tabla[16].Replace("Actualizado\">", "");
				tabla[17] = tabla[17].Replace("Campo\">Sexo\u00a0:<span class=\"APLI_txtActualizado\">", "");
				tabla[17] = tabla[17].Replace("</span>", "");
				tabla[17] = tabla[17].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
				tabla[47] = tabla[47].Replace("Actualizado\">", "");
				tabla[29] = tabla[29].Replace("Actualizado\">", "");
				tabla[29] = tabla[29].Replace("</TD>\r\n ", "");
				tabla[23] = tabla[23].Replace("Actualizado\">", "");
				tabla[21] = tabla[21].Replace("Actualizado\">", "");
				tabla[26] = tabla[26].Replace("colspan=\"3\" Actualizado\">", "");
				xApellidoPaterno = tabla[6].Trim();
				xApellidoMaterno = tabla[8].Trim();
				xPrimerNombre = tabla[11].Trim();
				xSegundoNombre = tabla[13].Trim();
				xTipoTrabajador = tabla[31].Trim();
				xSexo = tabla[17].Trim();
				xNacionalidad = tabla[23].Trim();
				xLugarNacimiento = tabla[21].Trim();
				xLugarResidencia = tabla[26].Trim();
				xEstadoCivil = tabla[18].Trim();
				xFechaNacimiento = tabla[16].Trim();
				xFechaDefuncion = tabla[45].Trim();
				xFechaProcesoDefuncion = tabla[47].Trim();
				xOrigenAfiliacion = tabla[29].Trim();
				xEntidadAfiliacion = tabla[35].Trim();
				xTipoComisionAfiliacion = tabla[42].Trim();
				xCodigoAfiliacion = tabla[33].Trim();
				xFechaIngresoAfiliacion = tabla[37].Trim();
				xSituacionAfiliacion = tabla[40].Trim();
			}
			if (state == Resul.Ok)
			{
				_ApellidoPaterno = xApellidoPaterno;
				_ApellidoMaterno = xApellidoMaterno;
				_PrimerNombre = xPrimerNombre;
				_SegundoNombre = xSegundoNombre;
				_TipoTrabajador = xTipoTrabajador;
				_Sexo = xSexo;
				_Nacionalidad = xNacionalidad;
				_LugarNacimiento = xLugarNacimiento;
				_LugarResidencia = xLugarResidencia;
				_EstadoCivil = xEstadoCivil;
				_FechaNacimiento = xFechaNacimiento;
				_FechaDefuncion = xFechaDefuncion;
				_FechaProcesoDefuncion = xFechaProcesoDefuncion;
				_OrigenAfiliacion = xOrigenAfiliacion;
				_EntidadAfiliacion = xEntidadAfiliacion;
				_TipoComisionAfiliacion = xTipoComisionAfiliacion;
				_CodigoAfiliacion = xCodigoAfiliacion;
				_FechaIngresoAfiliacion = xFechaIngresoAfiliacion;
				_SituacionAfiliacion = xSituacionAfiliacion;
			}
			return "Exito!!!";
		}
		catch (Exception ex)
		{
			_ok = false;
			_error += ex.Message;
			return _error;
		}
	}
}
