using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmStatusCajaChica
{
	private IStatusCajaChica Msta = new MysqlStatusCajaChica();

	public clsStatusCajaChica CargaStatusFlujosCajaChica(DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return Msta.CargaStatusFlujosCajaChica(FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsStatusCajaChica CargaStatusFlujosCajaChica_SP(DateTime Fecha)
	{
		try
		{
			return Msta.CargaStatusFlujosCajaChica_SP(Fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsStatusCajaChica CargaStatusFlujosCaja_SP(DateTime Fecha, int CodSucursal)
	{
		try
		{
			return Msta.CargaStatusFlujosCaja_SP(Fecha, CodSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsStatusCajaChica VerificaStadoCajaChica()
	{
		try
		{
			return Msta.VerificaStadoCajaChica();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsStatusCajaChica VerificaStadoCaja()
	{
		try
		{
			return Msta.VerificaStadoCaja();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsStatusCajaChica CargaStatusFlujosCaja(DateTime FechaInicial, DateTime FechaFinal, int CodSucursal)
	{
		try
		{
			return Msta.CargaStatusFlujosCaja(FechaInicial, FechaFinal, CodSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
