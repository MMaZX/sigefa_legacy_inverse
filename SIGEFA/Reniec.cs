using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;

namespace SIGEFA;

public class Reniec
{
	public enum Resul
	{
		Ok,
		NoResul,
		ErrorCapcha,
		Error
	}

	private string _Nombres;

	private string _ApePaterno;

	private string _ApeMaterno;

	private string _Dni;

	private Resul state;

	private CookieContainer myCookie;

	public Image GetCapcha => ReadCapcha();

	public string Dni => _Dni;

	public string Nombres => _Nombres;

	public string ApePaterno => _ApePaterno;

	public string ApeMaterno => _ApeMaterno;

	public Resul GetResul => state;

	public Reniec()
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

	private Image ReadCapcha()
	{
		try
		{
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("https://cel.reniec.gob.pe/valreg/codigo.do");
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

	public void GetInfo(string numDni, string ImgCapcha)
	{
		try
		{
			string myUrl = $"https://cel.reniec.gob.pe/valreg/valreg.do?accion=buscar&nuDni={numDni}&imagen={ImgCapcha}";
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
			myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
			myWebRequest.CookieContainer = myCookie;
			myWebRequest.Credentials = CredentialCache.DefaultCredentials;
			myWebRequest.Proxy = null;
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
			Stream myStream = myHttpWebResponse.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myStream);
			string _WebSource = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
			string[] _split = _WebSource.Split('<', '>', '\n', '\r');
			List<string> _resul = new List<string>();
			for (int i = 0; i < _split.Length; i++)
			{
				if (!string.IsNullOrEmpty(_split[i].Trim()))
				{
					_resul.Add(_split[i].Trim());
				}
			}
			switch (_resul.Count)
			{
			case 217:
				state = Resul.ErrorCapcha;
				break;
			case 232:
				state = Resul.Ok;
				break;
			case 222:
				state = Resul.NoResul;
				break;
			default:
				state = Resul.Error;
				break;
			}
			if (state == Resul.Ok)
			{
				_Dni = numDni;
				_Nombres = _resul[185];
				_ApePaterno = _resul[186];
				_ApeMaterno = _resul[187];
			}
			myHttpWebResponse.Close();
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}
