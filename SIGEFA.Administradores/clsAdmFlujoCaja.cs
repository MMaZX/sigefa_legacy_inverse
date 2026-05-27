using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmFlujoCaja
{
	private IFlujoCaja Mflu = new MysqlFlujoCaja();

	public bool Insert(clsFlujoCaja flujo)
	{
		try
		{
			return Mflu.Insert(flujo);
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

	public bool Update(clsFlujoCaja flujo)
	{
		try
		{
			return Mflu.Update(flujo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Delete(int CodFlujoCaja, int CodSucursal)
	{
		try
		{
			return Mflu.Delete(CodFlujoCaja, CodSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraFlujoCaja(int CodSucursal)
	{
		try
		{
			return Mflu.ListaFlujosCaja(CodSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsFlujoCaja CargaFlujosCaja(DateTime fecha, int CodSucursal)
	{
		try
		{
			return Mflu.CargaFlujosCaja(fecha, CodSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaPagoCobro(int tipo)
	{
		try
		{
			return Mflu.ListaPagoCobro(tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsFlujoCaja VerificaSaldoCaja(int codSucursal)
	{
		try
		{
			return Mflu.VerificaSaldoCaja(codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int VerificaAperturaCaja(int codSucursal)
	{
		try
		{
			return Mflu.VerificaAperturaCaja(codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public clsFlujoCaja VerificaDepositoCaja(int codSucursal, DateTime fecha)
	{
		try
		{
			return Mflu.VerificaDepositoCaja(codSucursal, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
