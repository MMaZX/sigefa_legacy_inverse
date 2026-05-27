using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;

namespace SIGEFA.Librerias;

public class EsSalud
{
	public enum Resul
	{
		Ok,
		NoResul,
		ErrorCapcha,
		Error
	}

	private Resul state;

	private string _Nombres;

	private string _ApePaterno;

	private string _ApeMaterno;

	private string _fechanac;

	private string _TipoAsegurado;

	private string _Autogenerado;

	private string _TipoSeguro;

	private string _CentroAsistencial;

	private string _DireccionCA;

	private string _AfiliadoA;

	private string _desde;

	private string _hasta;

	private bool _ok;

	private string _error;

	private string DNI;

	private CookieContainer myCookie;

	public string FechaNac => _fechanac;

	public string TipoAsegurado => _TipoAsegurado;

	public string Autogenerado => _Autogenerado;

	public string TipoSeguro => _TipoSeguro;

	public string CentroAsistencial => _CentroAsistencial;

	public string DireccionCA => _DireccionCA;

	public string AfiliadoA => _AfiliadoA;

	public string Desde => _desde;

	public string Hasta => _hasta;

	public Image GetCapcha => ReadCapcha();

	public string Nombres => _Nombres;

	public string ApePaterno => _ApePaterno;

	public string ApeMaterno => _ApeMaterno;

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

	public EsSalud()
	{
		try
		{
			myCookie = null;
			myCookie = new CookieContainer();
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
			ReadCapcha();
		}
		catch (Exception)
		{
			_ok = false;
			_error = "Error al procesar informacion de ESSALUD";
		}
	}

	private Image ReadCapcha()
	{
		try
		{
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://ww4.essalud.gob.pe:7777/acredita/captcha.jpg");
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

	private bool ParseInfoEsSalud(List<string> _result)
	{
		try
		{
			for (int i = 0; i < _result.Count; i++)
			{
				switch (_result[i])
				{
				case "Asegurados":
					_ok = false;
					_error = "No se encontraron registros para las siguientes condiciones: Número de DNI " + DNI;
					break;
				case "Nombres":
					_ApePaterno = _result[i + 1];
					_ApeMaterno = _result[i + 2];
					_Nombres = _result[i + 3] + " " + _result[i + 4];
					break;
				case "Nacimiento":
					_fechanac = _result[i + 1];
					break;
				case "Asegurado":
					if (_TipoAsegurado == null)
					{
						_TipoAsegurado = _result[i + 1];
					}
					break;
				case "Seguro":
					if (_TipoSeguro == null)
					{
						_TipoSeguro = _result[i + 1];
					}
					break;
				case "Asistencial":
					if (_CentroAsistencial == null)
					{
						_CentroAsistencial = _result[i + 1] + " " + _result[i + 2];
					}
					break;
				case "Desde":
					_desde = _result[i + 1];
					break;
				case "C.A.":
					if (_DireccionCA == null)
					{
						_DireccionCA = _result[i + 1] + " " + _result[i + 2] + " " + _result[i + 3] + " " + _result[i + 4];
					}
					break;
				case "Hasta":
					_hasta = _result[i + 1];
					break;
				case "Afiliado(a)":
					_AfiliadoA = _result[i + 2];
					break;
				}
			}
			return true;
		}
		catch (Exception)
		{
			_ok = false;
			_error = "Error al procesar informacion de sunat(Funcion ParseInfo)";
		}
		return false;
	}

	public void ConsultaEsSalud(string numDni, string ImgCapcha)
	{
		try
		{
			string myUrl = $"http://ww4.essalud.gob.pe:7777/acredita/servlet/Ctrlwacre?pg=1&ll=Libreta+Electoral%2FDNI&td=1&nd={numDni}&submit=Consultar&captchafield_doc={ImgCapcha}";
			DNI = numDni;
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
			myWebRequest.CookieContainer = myCookie;
			myWebRequest.Credentials = CredentialCache.DefaultCredentials;
			myWebRequest.Proxy = null;
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
			Stream myStream = myHttpWebResponse.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myStream);
			string _WebSource = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(_WebSource);
			HtmlNode node = doc.DocumentNode.SelectSingleNode("//input[@name='auto']");
			if (node != null)
			{
				string text = node.OuterHtml;
				_Autogenerado = text.Substring(40, 15);
			}
			string strRegexScript = "(?m)<body[^>]*>(\\w|\\W)*?</body[^>]*>";
			string strRegex = "<[^>]*>";
			string strMatchScript = string.Empty;
			string strWholeHtml = string.Empty;
			strWholeHtml = _WebSource;
			Match matchText = Regex.Match(strWholeHtml, strRegexScript, RegexOptions.IgnoreCase);
			strMatchScript = matchText.Groups[0].Value;
			string strPureText = Regex.Replace(strMatchScript, strRegex, string.Empty, RegexOptions.ExplicitCapture);
			strPureText = strPureText.Replace(Environment.NewLine, " ");
			string[] _split = strPureText.Split();
			List<string> _resul = new List<string>();
			for (int i = 0; i < _split.Length; i++)
			{
				if (!string.IsNullOrEmpty(_split[i].Trim()))
				{
					_resul.Add(_split[i].Trim());
				}
			}
			ParseInfoEsSalud(_resul);
			myHttpWebResponse.Close();
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}
