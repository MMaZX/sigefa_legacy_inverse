using System;

namespace SIGEFA.SunatFacElec;

internal class Conversion
{
	public string enletras(string num)
	{
		string dec = "";
		double nro;
		try
		{
			nro = Convert.ToDouble(num);
		}
		catch
		{
			return "";
		}
		long entero = Convert.ToInt64(Math.Truncate(nro));
		int decimales = Convert.ToInt32(Math.Round((nro - (double)entero) * 100.0, 2));
		if (decimales > 0)
		{
			dec = " CON " + decimales + "/100";
		}
		return toText(Convert.ToDouble(entero)) + dec;
	}

	private string toText(double value)
	{
		string Num2Text = "";
		value = Math.Truncate(value);
		if (value == 0.0)
		{
			Num2Text = "CERO";
		}
		else if (value == 1.0)
		{
			Num2Text = "UNO";
		}
		else if (value == 2.0)
		{
			Num2Text = "DOS";
		}
		else if (value == 3.0)
		{
			Num2Text = "TRES";
		}
		else if (value == 4.0)
		{
			Num2Text = "CUATRO";
		}
		else if (value == 5.0)
		{
			Num2Text = "CINCO";
		}
		else if (value == 6.0)
		{
			Num2Text = "SEIS";
		}
		else if (value == 7.0)
		{
			Num2Text = "SIETE";
		}
		else if (value == 8.0)
		{
			Num2Text = "OCHO";
		}
		else if (value == 9.0)
		{
			Num2Text = "NUEVE";
		}
		else if (value == 10.0)
		{
			Num2Text = "DIEZ";
		}
		else if (value == 11.0)
		{
			Num2Text = "ONCE";
		}
		else if (value == 12.0)
		{
			Num2Text = "DOCE";
		}
		else if (value == 13.0)
		{
			Num2Text = "TRECE";
		}
		else if (value == 14.0)
		{
			Num2Text = "CATORCE";
		}
		else if (value == 15.0)
		{
			Num2Text = "QUINCE";
		}
		else if (value < 20.0)
		{
			Num2Text = "DIECI" + toText(value - 10.0);
		}
		else if (value == 20.0)
		{
			Num2Text = "VEINTE";
		}
		else if (value < 30.0)
		{
			Num2Text = "VEINTI" + toText(value - 20.0);
		}
		else if (value == 30.0)
		{
			Num2Text = "TREINTA";
		}
		else if (value == 40.0)
		{
			Num2Text = "CUARENTA";
		}
		else if (value == 50.0)
		{
			Num2Text = "CINCUENTA";
		}
		else if (value == 60.0)
		{
			Num2Text = "SESENTA";
		}
		else if (value == 70.0)
		{
			Num2Text = "SETENTA";
		}
		else if (value == 80.0)
		{
			Num2Text = "OCHENTA";
		}
		else if (value == 90.0)
		{
			Num2Text = "NOVENTA";
		}
		else if (value < 100.0)
		{
			Num2Text = toText(Math.Truncate(value / 10.0) * 10.0) + " Y " + toText(value % 10.0);
		}
		else if (value == 100.0)
		{
			Num2Text = "CIEN";
		}
		else if (value < 200.0)
		{
			Num2Text = "CIENTO " + toText(value - 100.0);
		}
		else if (value == 200.0 || value == 300.0 || value == 400.0 || value == 600.0 || value == 800.0)
		{
			Num2Text = toText(Math.Truncate(value / 100.0)) + "CIENTOS";
		}
		else if (value == 500.0)
		{
			Num2Text = "QUINIENTOS";
		}
		else if (value == 700.0)
		{
			Num2Text = "SETECIENTOS";
		}
		else if (value == 900.0)
		{
			Num2Text = "NOVECIENTOS";
		}
		else if (value < 1000.0)
		{
			Num2Text = toText(Math.Truncate(value / 100.0) * 100.0) + " " + toText(value % 100.0);
		}
		else if (value == 1000.0)
		{
			Num2Text = "MIL";
		}
		else if (value < 2000.0)
		{
			Num2Text = "MIL " + toText(value % 1000.0);
		}
		else if (value < 1000000.0)
		{
			Num2Text = toText(Math.Truncate(value / 1000.0)) + " MIL";
			if (value % 1000.0 > 0.0)
			{
				Num2Text = Num2Text + " " + toText(value % 1000.0);
			}
		}
		else if (value == 1000000.0)
		{
			Num2Text = "UN MILLON";
		}
		else if (value < 2000000.0)
		{
			Num2Text = "UN MILLON " + toText(value % 1000000.0);
		}
		else if (value < 1000000000000.0)
		{
			Num2Text = toText(Math.Truncate(value / 1000000.0)) + " MILLONES ";
			if (value - Math.Truncate(value / 1000000.0) * 1000000.0 > 0.0)
			{
				Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000.0) * 1000000.0);
			}
		}
		else if (value == 1000000000000.0)
		{
			Num2Text = "UN BILLON";
		}
		else if (value < 2000000000000.0)
		{
			Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000.0) * 1000000000000.0);
		}
		else
		{
			Num2Text = toText(Math.Truncate(value / 1000000000000.0)) + " BILLONES";
			if (value - Math.Truncate(value / 1000000000000.0) * 1000000000000.0 > 0.0)
			{
				Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000.0) * 1000000000000.0);
			}
		}
		return Num2Text;
	}
}
