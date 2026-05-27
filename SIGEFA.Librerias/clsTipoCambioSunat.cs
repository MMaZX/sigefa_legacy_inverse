using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace SIGEFA.Librerias;

public class clsTipoCambioSunat
{
	private string url = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias";

	private string cadhtml = "";

	private string año = "";

	private string mes = "";

	private bool consulta = false;

	private bool rpta = false;

	private void WebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
	{
		try
		{
			WebBrowser webBrowser = (WebBrowser)sender;
			System.Windows.Forms.HtmlDocument html = webBrowser.Document;
			string sTitle = html.Title;
			foreach (HtmlElement select in html.GetElementsByTagName("select"))
			{
				if (select.Name.Equals("anho"))
				{
					select.Focus();
					select.SetAttribute("value", año);
					select.InvokeMember("onchange");
					select.RemoveFocus();
				}
				if (select.Name.Equals("mes"))
				{
					select.Focus();
					select.SetAttribute("value", mes);
					select.InvokeMember("onchange");
					select.RemoveFocus();
				}
			}
			foreach (HtmlElement button in html.GetElementsByTagName("input"))
			{
				if (button.Name.Equals("B1"))
				{
					cadhtml = webBrowser.DocumentText;
					if (consulta)
					{
						rpta = true;
						break;
					}
					button.RaiseEvent("onclick");
					consulta = true;
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public bool AccesoInternet()
	{
		try
		{
			IPHostEntry host = Dns.GetHostEntry("www.google.com");
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public DataTable ConsultaTCSunat(DateTime fecha)
	{
		año = fecha.ToString("yyyy");
		mes = fecha.ToString("MM");
		DataTable dt = new DataTable();
		WebBrowser objWebBrowser = new WebBrowser();
		objWebBrowser.DocumentCompleted += WebBrowserDocumentCompleted;
		objWebBrowser.Navigate(url);
		while (!rpta && AccesoInternet())
		{
			Application.DoEvents();
		}
		HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
		document.LoadHtml(cadhtml);
		HtmlNodeCollection NodesTr = document.DocumentNode.SelectNodes("//table[@class='class=\"form-table\"']//tr");
		if (NodesTr != null)
		{
			dt.Columns.Add("Día", typeof(string));
			dt.Columns.Add("Compra", typeof(string));
			dt.Columns.Add("Venta", typeof(string));
			int iNumFila = 0;
			foreach (HtmlNode Node in (IEnumerable<HtmlNode>)NodesTr)
			{
				if (iNumFila > 0)
				{
					int iNumColumna = 0;
					DataRow dr = dt.NewRow();
					foreach (HtmlNode subNode in Node.Elements("td"))
					{
						if (iNumColumna == 0)
						{
							dr = dt.NewRow();
						}
						string sValue = subNode.InnerHtml.ToString().Trim();
						sValue = Regex.Replace(sValue, "<.*?>", " ");
						dr[iNumColumna] = sValue;
						iNumColumna++;
						if (iNumColumna == 3)
						{
							dt.Rows.Add(dr);
							iNumColumna = 0;
						}
					}
				}
				iNumFila++;
			}
			dt.AcceptChanges();
		}
		consulta = false;
		rpta = false;
		mes = "";
		año = "";
		cadhtml = "";
		return dt;
	}
}
