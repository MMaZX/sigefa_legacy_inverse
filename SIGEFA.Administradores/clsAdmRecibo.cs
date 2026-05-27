using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmRecibo
{
	private IRecibo MRecibo = new MysqlRecibo();

	public bool insert(clsRecibos recibo)
	{
		try
		{
			return MRecibo.Insert(recibo);
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

	public bool update(clsRecibos recibo)
	{
		try
		{
			return MRecibo.Update(recibo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaRecibos(int codSucur, DateTime fecha1, DateTime fecha2, int tipo)
	{
		try
		{
			return MRecibo.ListaRecibos(codSucur, fecha1, fecha2, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaRecibosEgreso(int codSucur, int tipo)
	{
		try
		{
			return MRecibo.ListaRecibosEgreso(codSucur, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int Correlativo(int codtipo)
	{
		try
		{
			return MRecibo.Correlativo(codtipo);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return 0;
		}
	}
}
