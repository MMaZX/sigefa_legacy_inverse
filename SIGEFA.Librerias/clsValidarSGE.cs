using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGEFA.Librerias;

public class clsValidarSGE
{
	public bool Valida = true;

	public static bool internet = true;

	private Ping pinguin = new Ping();

	private int timeout = 3;

	private string ips = "google.com";

	public bool AccesoInternet()
	{
		try
		{
			if (pinguin.Send(ips, timeout).Status == IPStatus.Success)
			{
				internet = true;
				return true;
			}
			internet = false;
			return false;
		}
		catch (Exception)
		{
			internet = false;
			return false;
		}
	}

	public void KeyTab(KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			e.Handled = true;
			SendKeys.Send("{TAB}");
		}
	}

	public bool esHoraValida(string hora)
	{
		Regex r = new Regex("([0-1][0-9]|2[0-3]):[0-5][0-9]");
		Match m = r.Match(hora);
		return m.Success;
	}

	public int ObtieneNumeroSemana(DateTime fecha)
	{
		CultureInfo ciCurr = CultureInfo.CurrentCulture;
		return ciCurr.Calendar.GetWeekOfYear(fecha, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
	}

	public void ValidarNumeros(object sender, KeyPressEventArgs e)
	{
		if (char.IsDigit(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsControl(e.KeyChar))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	public void ActivaCajas(Control.ControlCollection Coleccion)
	{
		foreach (Control a in Coleccion)
		{
			if (!a.Enabled && a is TextBox)
			{
				TextBox tx = (TextBox)a;
				tx.BackColor = Color.White;
				tx.Text = "";
				tx.Enabled = true;
				tx.Focus();
			}
			if (!a.Enabled && a is RichTextBox)
			{
				RichTextBox tx2 = (RichTextBox)a;
				tx2.BackColor = Color.White;
				tx2.Text = "";
				tx2.Enabled = true;
				tx2.Focus();
			}
			if (!a.Enabled && a is MaskedTextBox)
			{
				MaskedTextBox tx3 = (MaskedTextBox)a;
				tx3.BackColor = Color.White;
				tx3.Text = "";
				tx3.Enabled = true;
				tx3.Focus();
			}
			if (!a.Enabled && a is ComboBox)
			{
				ComboBox cb = (ComboBox)a;
				cb.SelectedIndex = -1;
				cb.Enabled = true;
			}
			if (!a.Enabled && a is DateTimePicker)
			{
				DateTimePicker dt = (DateTimePicker)a;
				dt.Enabled = true;
			}
			if (!a.Enabled && a is CheckedListBox)
			{
				CheckedListBox chk = (CheckedListBox)a;
				while (chk.CheckedIndices.Count > 0)
				{
					chk.SetItemChecked(chk.CheckedIndices[0], value: false);
				}
				chk.Enabled = true;
			}
			if (a.HasChildren)
			{
				ActivaCajas(a.Controls);
			}
		}
	}

	public void Limpiar(Control.ControlCollection Coleccion)
	{
		foreach (Control a in Coleccion)
		{
			if (a.Enabled && a is TextBox)
			{
				TextBox tx = (TextBox)a;
				tx.BackColor = Color.White;
				tx.Text = "";
				tx.Enabled = false;
				tx.Focus();
			}
			if (a.Enabled && a is RichTextBox)
			{
				RichTextBox tx2 = (RichTextBox)a;
				tx2.BackColor = Color.White;
				tx2.Text = "";
				tx2.Enabled = false;
				tx2.Focus();
			}
			if (a.Enabled && a is MaskedTextBox)
			{
				MaskedTextBox tx3 = (MaskedTextBox)a;
				tx3.BackColor = Color.White;
				tx3.Text = "";
				tx3.Enabled = false;
				tx3.Focus();
			}
			if (a.Enabled && a is ComboBox)
			{
				ComboBox cb = (ComboBox)a;
				cb.SelectedIndex = -1;
				cb.Text = "";
				cb.Enabled = false;
			}
			if (a.Enabled && a is DateTimePicker)
			{
				DateTimePicker dt = (DateTimePicker)a;
				dt.Format = DateTimePickerFormat.Long;
				dt.CustomFormat = "";
				dt.Enabled = false;
			}
			if (a.Enabled && a is CheckedListBox)
			{
				CheckedListBox chk = (CheckedListBox)a;
				while (chk.CheckedIndices.Count > 0)
				{
					chk.SetItemChecked(chk.CheckedIndices[0], value: false);
				}
				chk.Enabled = false;
			}
			if (a.HasChildren)
			{
				Limpiar(a.Controls);
			}
		}
	}

	public void LimpiarDataGrid(DataGridView grilla)
	{
		DataTable dt = (DataTable)grilla.DataSource;
		dt.Clear();
	}

	public void ValidarDatos(Control.ControlCollection Coleccion)
	{
		Valida = true;
		foreach (Control c in Coleccion)
		{
			if (c.Enabled && c.Text == "" && c is TextBox)
			{
				TextBox tx = (TextBox)c;
				tx.BackColor = Color.LightPink;
				Valida = false;
				tx.Focus();
			}
			if (c.Enabled && c.Text == "" && c is RichTextBox)
			{
				RichTextBox tx2 = (RichTextBox)c;
				tx2.BackColor = Color.LightPink;
				Valida = false;
				tx2.Focus();
			}
			if (c.Enabled && c is ComboBox)
			{
				ComboBox cb = (ComboBox)c;
				if (cb.SelectedIndex == -1)
				{
					Valida = false;
					cb.Focus();
				}
			}
			if (c.HasChildren && Valida)
			{
				ValidarDatos(c.Controls);
			}
		}
	}

	public void Numeros(KeyPressEventArgs e)
	{
		if (char.IsNumber(e.KeyChar))
		{
			e.Handled = false;
		}
		if (char.IsLetter(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	public void SoloNumeros(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	public void MontoTope(object sender, KeyPressEventArgs e, double Monto)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
		if (Convert.ToDouble((sender as TextBox).Text) <= Monto)
		{
			e.Handled = true;
		}
	}

	public void telefono(KeyPressEventArgs e)
	{
		string Aceptados = "0123456789-" + Convert.ToChar(8);
		if (Aceptados.Contains(e.KeyChar.ToString() ?? ""))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	public void enteros(KeyPressEventArgs e)
	{
		string Aceptados = "0123456789" + Convert.ToChar(8);
		if (Aceptados.Contains(e.KeyChar.ToString() ?? ""))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	public void letras(KeyPressEventArgs e)
	{
		if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
		{
			e.Handled = true;
		}
		else
		{
			e.Handled = false;
		}
	}

	public void SoloLectura(Control.ControlCollection Coleccion)
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
			if (c.Enabled && c is NumericUpDown)
			{
				NumericUpDown nud = (NumericUpDown)c;
				nud.Enabled = false;
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
				SoloLectura(c.Controls);
			}
		}
	}

	public bool email_bien_escrito(string email)
	{
		string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
		if (Regex.IsMatch(email, expresion))
		{
			if (Regex.Replace(email, expresion, string.Empty).Length == 0)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public void NumerosEnteros(KeyPressEventArgs e)
	{
		if (char.IsDigit(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsControl(e.KeyChar))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	public void NumerosDecimales(KeyPressEventArgs e, TextBox t)
	{
		if (e.KeyChar == '\b')
		{
			e.Handled = false;
			return;
		}
		bool IsDec = false;
		int nroDec = 0;
		for (int i = 0; i < t.Text.Length; i++)
		{
			if (t.Text[i] == '.')
			{
				IsDec = true;
			}
			if (IsDec && nroDec++ >= 3)
			{
				e.Handled = true;
				return;
			}
		}
		if (e.KeyChar >= '0' && e.KeyChar <= '9')
		{
			e.Handled = false;
		}
		else if (e.KeyChar == '.')
		{
			e.Handled = (IsDec ? true : false);
		}
		else
		{
			e.Handled = true;
		}
	}

	public void SoloNumerosDoc(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '-')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
		{
			e.Handled = true;
		}
	}

	public void decimalesNegativos(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
		if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > 0)
		{
			e.Handled = true;
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
