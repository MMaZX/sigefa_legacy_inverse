using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmNewConfiguracion
{
	private MysqlNewConfiguracion Mconf = new MysqlNewConfiguracion();

	public bool guardaConfiguracion(string codconfig, string dataconfig, string origen, string comentario)
	{
		try
		{
			return Mconf.guardaConfiguracion(codconfig, dataconfig, origen, comentario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizaConfiguracion(string codconfig, string origen, string dataconfig)
	{
		try
		{
			return Mconf.actualizaConfiguracion(codconfig, origen, dataconfig);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public int getConfiguracion(string codconfig, string origen)
	{
		try
		{
			return Mconf.getConfiguracion(codconfig, origen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -666;
		}
	}

	internal bool eliminaConfiguracion(string codconfig, string origen)
	{
		try
		{
			return Mconf.deleteConfiguracion(codconfig, origen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
