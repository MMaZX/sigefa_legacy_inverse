using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmArqueoFondoFijo
{
	private IArqueoFondoFijo Marqueo = new MysqlArqueFondoFijo();

	public bool insert(clsArqueoFondoFijo arqe)
	{
		try
		{
			return Marqueo.Insert(arqe);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia");
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia");
			}
			return false;
		}
	}

	public bool insertDetalle(clsDetalleArqueFondoFijo det)
	{
		try
		{
			return Marqueo.insertDetalle(det);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia");
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia");
			}
			return false;
		}
	}

	public DataTable ListaDinero(int tipo)
	{
		try
		{
			return Marqueo.ListaDinero(tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public decimal TraeValor(int codigo)
	{
		try
		{
			return Marqueo.TraeValor(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}
}
