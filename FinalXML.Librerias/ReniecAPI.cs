using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace FinalXML.Librerias;

public class ReniecAPI
{
	public string sConexDNI = ConfigurationManager.ConnectionStrings["URLDNI"].ConnectionString;

	public string sConexRUC = ConfigurationManager.ConnectionStrings["URLRUC"].ConnectionString;

	public string nConexDNI = "https://api.apis.net.pe/v1/dni?numero=";

	public string GetInfo(string numDNI)
	{
		try
		{
			if (!string.IsNullOrEmpty(numDNI) && numDNI.Length == 8)
			{
				string myUrl = $"{sConexDNI}&dni={numDNI}";
				HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
				myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
				myWebRequest.Credentials = CredentialCache.DefaultCredentials;
				myWebRequest.Proxy = null;
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
				Stream myStream = myHttpWebResponse.GetResponseStream();
				StreamReader myStreamReader = new StreamReader(myStream);
				string myData = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
				if (myData == null)
				{
					Consultadni2(numDNI);
				}
				myHttpWebResponse.Close();
				return myData;
			}
			return string.Empty;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public string Consultadni2(string numDNI)
	{
		try
		{
			if (!string.IsNullOrEmpty(numDNI) && numDNI.Length == 8)
			{
				string myUrl = nConexDNI + numDNI;
				HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
				myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
				myWebRequest.Credentials = CredentialCache.DefaultCredentials;
				myWebRequest.Proxy = null;
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
				Stream myStream = myHttpWebResponse.GetResponseStream();
				StreamReader myStreamReader = new StreamReader(myStream);
				string myData = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
				myData = metodoModificaDataDeNuevaConexionDNI(myData);
				myHttpWebResponse.Close();
				return myData;
			}
			return string.Empty;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private string metodoModificaDataDeNuevaConexionDNI(string myData)
	{
		string respuesta = "";
		JObject json = JObject.Parse(myData);
		return json.GetValue("nombre").ToString();
	}

	public string GetInfoRuc(string numRUC)
	{
		try
		{
			if (!string.IsNullOrEmpty(numRUC) && numRUC.Length == 11)
			{
				string myUrl = $"{sConexRUC}&ruc={numRUC}";
				HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
				myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
				myWebRequest.Credentials = CredentialCache.DefaultCredentials;
				myWebRequest.Proxy = null;
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
				Stream myStream = myHttpWebResponse.GetResponseStream();
				StreamReader myStreamReader = new StreamReader(myStream);
				string myData = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
				myHttpWebResponse.Close();
				return myData;
			}
			return string.Empty;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}
