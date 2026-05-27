using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGEFA.Entidades;

internal class clsValidar
{
	public void KeyTab(KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			e.Handled = true;
			SendKeys.Send("{TAB}");
		}
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

	public void NumerosDecimales(KeyPressEventArgs e, TextBox t, int NroDecimales = 3)
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
			if (IsDec && nroDec++ >= NroDecimales)
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

	public void SOLONumeros(object sender, KeyPressEventArgs e)
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

	public void SOLONumerosDoc(object sender, KeyPressEventArgs e)
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

	public string Encode(string chaine)
	{
		int ind = 1;
		int checksum = 0;
		string code128 = "";
		int longueur = chaine.Length;
		if (longueur == 0)
		{
			Console.WriteLine("\n chaine vide");
		}
		else
		{
			for (ind = 0; ind < longueur; ind++)
			{
				if (chaine[ind] < ' ' || chaine[ind] > '~')
				{
					Console.WriteLine("\n chaine invalide");
				}
			}
		}
		bool tableB = true;
		ind = 0;
		while (ind < longueur)
		{
			if (tableB)
			{
				int mini = ((ind != 0 && ind + 3 != longueur - 1) ? 6 : 4);
				mini--;
				if (ind + mini <= longueur - 1)
				{
					while (mini >= 0)
					{
						if (chaine[ind + mini] < '0' || chaine[ind + mini] > '9')
						{
							Console.WriteLine("\n exit");
							break;
						}
						mini--;
					}
				}
				if (mini < 0)
				{
					code128 = ((ind != 0) ? (code128 + char.ToString('Ç')) : char.ToString('Í'));
					tableB = false;
				}
				else if (ind == 0)
				{
					code128 = char.ToString('Ì');
				}
			}
			if (!tableB)
			{
				int mini = 2;
				mini--;
				if (ind + mini < longueur)
				{
					while (mini >= 0 && chaine[ind + mini] >= '0' && chaine[ind] <= '9')
					{
						mini--;
					}
				}
				if (mini < 0)
				{
					int dummy = int.Parse(chaine.Substring(ind, 2));
					Console.WriteLine("\n  dummy ici : " + dummy);
					dummy = ((dummy >= 95) ? (dummy + 100) : (dummy + 32));
					code128 += (char)dummy;
					ind += 2;
				}
				else
				{
					code128 += char.ToString('È');
					tableB = true;
				}
			}
			if (tableB)
			{
				code128 += chaine[ind];
				ind++;
			}
		}
		for (ind = 0; ind <= code128.Length - 1; ind++)
		{
			int dummy = code128[ind];
			Console.WriteLine("\n  et voila dummy : " + dummy);
			dummy = ((dummy >= 127) ? (dummy - 100) : (dummy - 32));
			if (ind == 0)
			{
				checksum = dummy;
			}
			checksum = (checksum + ind * dummy) % 103;
		}
		checksum = ((checksum >= 95) ? (checksum + 100) : (checksum + 32));
		return code128 + char.ToString((char)checksum) + char.ToString('Î');
	}
}
