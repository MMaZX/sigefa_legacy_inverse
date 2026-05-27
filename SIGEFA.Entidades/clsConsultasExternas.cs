using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace SIGEFA.Entidades;

public class clsConsultasExternas
{
	private string sRazonSocial;

	private string sDireccionLegal;

	private string cadhtml = "";

	private string año = "";

	private string mes = "";

	private bool consulta = false;

	private bool rpta = false;

	public string RazonSocial
	{
		get
		{
			return sRazonSocial;
		}
		set
		{
			sRazonSocial = value;
		}
	}

	public string DireccionLegal
	{
		get
		{
			return sDireccionLegal;
		}
		set
		{
			sDireccionLegal = value;
		}
	}

	public DataTable ConsultaTCSunat(DateTime Fecha)
	{
		try
		{
			año = Fecha.ToString("yyyy");
			mes = Fecha.ToString("MM");
			WebBrowser objWebBrowser = new WebBrowser();
			objWebBrowser.DocumentCompleted += WebBrowserDocumentCompleted;
			objWebBrowser.Navigate("https://e-consulta.sunat.gob.pe/cl-at-ittipcam/tcS01Alias");
			while (!rpta)
			{
				Application.DoEvents();
			}
			HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
			document.LoadHtml(cadhtml);
			HtmlNodeCollection NodesTr = document.DocumentNode.SelectNodes("//table[@class='class=\"form-table\"']//tr");
			if (NodesTr != null)
			{
				DataTable dt = new DataTable();
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
				return dt;
			}
			consulta = false;
			rpta = false;
			mes = "";
			año = "";
			cadhtml = "";
			return null;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return null;
		}
	}

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
			MessageBox.Show(ex.Message, "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public bool rucsunat(string codigo)
	{
		try
		{
			if (codigo.Length == 11)
			{
				string url = "http://www.sunat.gob.pe/w/wapS01Alias?ruc=" + codigo;
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				Stream requestStream = req.GetRequestStream();
				requestStream.Close();
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				if (res.StatusCode == HttpStatusCode.OK)
				{
					StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.Default);
					string backstr = sr.ReadToEnd();
					if (capturardatos(backstr, codigo))
					{
						sr.Close();
						res.Close();
						return true;
					}
					sr.Close();
					res.Close();
					return false;
				}
				MessageBox.Show("No responde el servicio de la SUNAT", "Consulta Sunat", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				res.Close();
				return false;
			}
			MessageBox.Show("EL RUC ingresado no es válido", "Consulta Sunat", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		catch (WebException ex)
		{
			Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", ex.Status);
			return false;
		}
		catch (Exception ex2)
		{
			Console.WriteLine("\nThe following Exception was raised : {0}", ex2.Message);
			return false;
		}
	}

	private bool capturardatos(string resp, string cod)
	{
		resp = resp.Replace("     ", " ");
		resp = resp.Replace("    ", " ");
		resp = resp.Replace("   ", " ");
		resp = resp.Replace("  ", " ");
		resp = resp.Replace("( ", "(");
		resp = resp.Replace(" )", ")");
		string[] stringSeparators = new string[1] { "<small>" };
		string[] words = resp.Split(stringSeparators, StringSplitOptions.None);
		string word1 = string.Empty;
		string word2 = string.Empty;
		string word3 = string.Empty;
		string word4 = string.Empty;
		if (words[0].Contains("Resultado"))
		{
			string[] array = words;
			foreach (string word5 in array)
			{
				if (word5.Contains("<b>N&#xFA;mero Ruc. </b> " + cod + " - "))
				{
					word1 = word5.Replace("<b>N&#xFA;mero Ruc. </b> " + cod + " - ", "");
					word1 = word1.Replace(" <br/></small>", "");
				}
				if (word5.Contains("Estado."))
				{
					word2 = word5.Replace("<b>Estado.</b>", "");
					word2 = word2.Replace("</small><br/>", "");
				}
				if (word5.Contains("Direcci"))
				{
					word3 = word5.Replace("<b>Direcci&#xF3;n.</b><br/>", "");
					word3 = word3.Replace("</small><br/>", "");
				}
				if (word5.Contains("Situaci"))
				{
					word4 = word5.Replace("Situaci&#xF3;n.<b> ", "");
					word4 = word4.Replace("</b></small><br/>", "");
				}
			}
			string RazSoc = word1.ToString();
			string Est = word2.ToString();
			string Dir = word3.ToString();
			string Con = word4.ToString();
			RazSoc = RazSoc.Replace("&#209;", "Ñ");
			RazSoc = RazSoc.Replace("&#xD1;", "Ñ");
			RazSoc = RazSoc.Replace("&#193;", "Á");
			RazSoc = RazSoc.Replace("&#201;", "É");
			RazSoc = RazSoc.Replace("&#205;", "Í");
			RazSoc = RazSoc.Replace("&#211;", "Ó");
			RazSoc = RazSoc.Replace("&#218;", "Ú");
			RazSoc = RazSoc.Replace("&#xC1;", "Á");
			RazSoc = RazSoc.Replace("&#xC9;", "É");
			RazSoc = RazSoc.Replace("&#xCD;", "Í");
			RazSoc = RazSoc.Replace("&#xD3;", "Ó");
			RazSoc = RazSoc.Replace("&#xDA;", "Ú");
			RazSoc = RazSoc.Substring(0, RazSoc.Length - 3);
			Dir = Dir.Replace("&#209;", "Ñ");
			Dir = Dir.Replace("&#xD1;", "Ñ");
			Dir = Dir.Replace("&#193;", "Á");
			Dir = Dir.Replace("&#201;", "É");
			Dir = Dir.Replace("&#205;", "Í");
			Dir = Dir.Replace("&#211;", "Ó");
			Dir = Dir.Replace("&#218;", "Ú");
			Dir = Dir.Replace("&#xC1;", "Á");
			Dir = Dir.Replace("&#xC9;", "É");
			Dir = Dir.Replace("&#xCD;", "Í");
			Dir = Dir.Replace("&#xD3;", "Ó");
			Dir = Dir.Replace("&#xDA;", "Ú");
			Est = Est.Substring(0, Est.Length - 6);
			Con = Con.Substring(0, Con.Length - 3);
			Dir = Dir.Substring(0, Dir.Length - 3);
			RazonSocial = HttpUtility.HtmlDecode(RazSoc);
			DireccionLegal = Dir;
			return true;
		}
		if (words[0].Contains("Mensaje"))
		{
			MessageBox.Show("El RUC ingresado no existe en la base de datos de la Sunat", "Consulta Sunat", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		return false;
	}

	public void limpiar(Control.ControlCollection Coleccion)
	{
		foreach (Control c in Coleccion)
		{
			if (c.Enabled && c is TextBox)
			{
				c.Text = "";
			}
			if (c.Enabled && c is ComboBox)
			{
				ComboBox com = (ComboBox)c;
				com.SelectedIndex = -1;
			}
			if (c.Enabled && c is CheckBox)
			{
				CheckBox ch = (CheckBox)c;
				ch.Checked = false;
			}
			if (c.HasChildren)
			{
				limpiar(c.Controls);
			}
		}
	}

	public void sololectura(Control.ControlCollection Coleccion)
	{
		foreach (Control c in Coleccion)
		{
			if (c.Enabled && c is TextBox)
			{
				TextBox tx = (TextBox)c;
				tx.ReadOnly = true;
			}
			if (c.Enabled && c is ComboBox)
			{
				ComboBox com = (ComboBox)c;
				com.Enabled = false;
			}
			if (c.Enabled && c is CheckBox)
			{
				CheckBox ch = (CheckBox)c;
				ch.Enabled = false;
			}
			if (c.Enabled && c is Button)
			{
				Button bt = (Button)c;
				bt.Enabled = false;
			}
			if (c.HasChildren)
			{
				sololectura(c.Controls);
			}
		}
	}

	public int GetIDPaperSize(string PrinterName, string PaperSizeName)
	{
		PrintDocument pdprint = new PrintDocument();
		pdprint.PrinterSettings.PrinterName = PrinterName;
		int PaperSizeID = 0;
		foreach (PaperSize ps in pdprint.PrinterSettings.PaperSizes)
		{
			if (ps.PaperName == PaperSizeName)
			{
				PaperSizeID = ps.RawKind;
				break;
			}
		}
		return PaperSizeID;
	}
}
