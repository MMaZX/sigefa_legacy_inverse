using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmSeleccionDespachoNC
{
	private MysqlSeleccionDespachoNC Msel = new MysqlSeleccionDespachoNC();

	public bool insert(clsSeleccionDespachoNC sel)
	{
		try
		{
			return Msel.Insert(sel);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	internal DataTable cargaDataParaInterfazSeleccionador(int codNotaCredito)
	{
		try
		{
			return Msel.cargaDataParaInterfazSeleccionador(codNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable cargaAlmacenesDeSeleccionDeNC(int codNotaCredito)
	{
		try
		{
			return Msel.cargaAlmacenesDeSeleccionDeNC(codNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool tieneDataSeleccionada(string codNotaCredito)
	{
		try
		{
			return Msel.tieneDataSeleccionada(codNotaCredito);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}
}
