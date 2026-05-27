using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmTarjetaPago
{
	private ITarjetaPago Mtar = new MysqlTarjetaPago();

	public bool Insert(clsTarjetaPago tar)
	{
		try
		{
			return Mtar.Insert(tar);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool Update(clsTarjetaPago tar)
	{
		try
		{
			return Mtar.Update(tar);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Delete(int Codtar, int CodAlmacen)
	{
		try
		{
			return Mtar.Delete(Codtar, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraTarjetas(int codAlmacen)
	{
		try
		{
			return Mtar.ListaTarjetas(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsTarjetaPago CargaTarjeta(int Codigo, int CodAlmacen)
	{
		try
		{
			return Mtar.CargaTarjeta(Codigo, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public double SumaTotalTarjetas(string fecha1, string fecha2, int almacen, int codtarjeta, int sucursal, int codcaja)
	{
		try
		{
			return Mtar.SumaTotalTarjetas(fecha1, fecha2, almacen, codtarjeta, sucursal, codcaja);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0.0;
		}
	}
}
