using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;

namespace SIGEFA;

public class Sunat
{
	public enum Resul
	{
		Ok,
		NoResul,
		ErrorCapcha,
		Error
	}

	private string _RazonSocial;

	private string _Direcion;

	private string _Ruc;

	private string _EstadoContr;

	private string _TipoContr;

	private string _Telefono;

	private CookieContainer myCookie;

	private Resul state;

	public Image GetCapcha => ReadCapcha();

	public string RazonSocial => _RazonSocial;

	public string Direcion => _Direcion;

	public string Ruc => _Ruc;

	public string EstadoContr => _EstadoContr;

	public string TipoContr => _TipoContr;

	public string Telefono => _Telefono;

	public Resul GetResul => state;

	public Sunat()
	{
		try
		{
			myCookie = null;
			myCookie = new CookieContainer();
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
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
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

	public void GetInfo(string numRuc, string TextoCapcha)
	{
		try
		{
			string myUrl = $"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc={numRuc}&codigo={TextoCapcha}&tipdoc=1";
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
			myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
			myWebRequest.CookieContainer = myCookie;
			myWebRequest.Credentials = CredentialCache.DefaultCredentials;
			myWebRequest.Proxy = null;
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
			Stream myStream = myHttpWebResponse.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myStream);
			string xDat = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
			string[] _split = xDat.Split('<', '>', '\n', '\r');
			List<string> _result = new List<string>();
			for (int i = 0; i < _split.Length; i++)
			{
				if (!string.IsNullOrEmpty(_split[i].Trim()))
				{
					_result.Add(_split[i].Trim());
				}
			}
			if (_result.Count == 77)
			{
				state = Resul.ErrorCapcha;
			}
			if (_result.Count == 147)
			{
				state = Resul.NoResul;
			}
			if (_result.Count >= 635)
			{
				state = Resul.Ok;
			}
			switch (state)
			{
			case Resul.Ok:
				StateOK(xDat, numRuc);
				break;
			}
			myHttpWebResponse.Close();
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void StateOK(string xDat, string numRuc)
	{
		string xRuc = string.Empty;
		string xRazSoc = string.Empty;
		string xDir = string.Empty;
		string xEsCn = string.Empty;
		string xTpCn = string.Empty;
		string xTef = string.Empty;
		xDat = xDat.Replace("     ", " ");
		xDat = xDat.Replace("    ", " ");
		xDat = xDat.Replace("   ", " ");
		xDat = xDat.Replace("  ", " ");
		xDat = xDat.Replace("( ", "(");
		xDat = xDat.Replace(" )", ")");
		string[] tabla = Regex.Split(xDat, "<td class");
		if (numRuc.StartsWith("1"))
		{
			tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>" + numRuc + " - ", "");
			tabla[1] = tabla[1].Replace("</td>\r\n </tr>\r\n <tr>\r\n", "");
			tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
			tabla[3] = tabla[3].Replace("</td>\r\n </tr>\r\n \r\n <tr>\r\n ", "");
			tabla[8] = tabla[8].Replace("=\"bgn\" colspan=1 >", "");
			tabla[8] = tabla[8].Replace("</td>\r\n ", "");
			string NuevoRUS = tabla[8].Trim();
			if (NuevoRUS == "Afecto al Nuevo RUS:")
			{
				tabla[14] = tabla[14].Replace("=\"bg\" colspan=1>", "");
				tabla[14] = tabla[14].Replace("</td>\r\n ", "");
				tabla[19] = tabla[19].Replace("=\"bg\" colspan=3>", "");
				tabla[19] = tabla[19].Replace("</td>\r\n </tr>\r\n<!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 -->\r\n<!-- <tr> -->\r\n<!-- ", "");
				tabla[21] = tabla[21].Replace("=\"bg\" colspan=1>", "");
				tabla[21] = tabla[21].Replace("</td> -->\r\n<!-- ", "");
				xEsCn = tabla[14];
				xDir = tabla[19];
				xTef = tabla[21];
			}
			else
			{
				tabla[12] = tabla[12].Replace("=\"bg\" colspan=1>", "");
				tabla[12] = tabla[12].Replace("</td>\r\n ", "");
				tabla[17] = tabla[17].Replace("=\"bg\" colspan=3>", "");
				tabla[17] = tabla[17].Replace("</td>\r\n </tr>\r\n<!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 -->\r\n<!-- <tr> -->\r\n<!-- ", "");
				tabla[19] = tabla[19].Replace("=\"bg\" colspan=1>", "");
				tabla[19] = tabla[19].Replace("</td> -->\r\n<!-- ", "");
				xEsCn = tabla[12];
				xDir = tabla[17];
				xTef = tabla[19];
			}
			xRuc = numRuc;
			xRazSoc = tabla[1];
			xTpCn = tabla[3];
		}
		else if (numRuc.StartsWith("2"))
		{
			tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>" + numRuc + " - ", "");
			tabla[1] = tabla[1].Replace("</td>\r\n </tr>\r\n <tr>\r\n", "");
			tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
			tabla[3] = tabla[3].Replace("</td>\r\n </tr>\r\n \r\n <tr>\r\n ", "");
			tabla[10] = tabla[10].Replace("=\"bg\" colspan=1>", "");
			tabla[10] = tabla[10].Replace("</td>\r\n", "");
			tabla[15] = tabla[15].Replace("=\"bg\" colspan=3>", "");
			tabla[15] = tabla[15].Replace("</td>\r\n </tr>\r\n<!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 -->\r\n<!-- <tr> -->\r\n<!-- ", "");
			tabla[17] = tabla[17].Replace("=\"bg\" colspan=1>", "");
			tabla[17] = tabla[17].Replace("</td> -->\r\n<!-- ", "");
			xRuc = numRuc;
			xRazSoc = tabla[1];
			xTpCn = tabla[3];
			xEsCn = tabla[10];
			xDir = tabla[15];
			xTef = tabla[17];
		}
		_Ruc = xRuc;
		_TipoContr = xTpCn;
		_RazonSocial = xRazSoc;
		_Direcion = xDir;
		_EstadoContr = xEsCn;
		_Telefono = xTef;
	}
}
