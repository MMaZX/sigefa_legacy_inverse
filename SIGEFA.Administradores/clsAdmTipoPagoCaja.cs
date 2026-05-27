using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmTipoPagoCaja
{
	private ITipoPagoCaja MTPcaja = new MysqlTipoPagoCaja();

	public bool insert(clsTipoPagoCaja Codigo)
	{
		try
		{
			return MTPcaja.Insert(Codigo);
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

	public bool update(clsTipoPagoCaja Codigo)
	{
		try
		{
			return MTPcaja.Update(Codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int Codigo)
	{
		try
		{
			return MTPcaja.Delete(Codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaTipoPagoCaja()
	{
		try
		{
			return MTPcaja.ListaTipoPagoCaja();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
