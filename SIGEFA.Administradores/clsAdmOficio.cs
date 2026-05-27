using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

public class clsAdmOficio
{
	private MysqlOficio Mofi = new MysqlOficio();

	internal DataTable listaOficios()
	{
		try
		{
			return Mofi.listaOficios();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool insert(clsOficio nuevo)
	{
		try
		{
			return Mofi.insert(nuevo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool update(clsOficio nuevo)
	{
		try
		{
			return Mofi.update(nuevo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal List<clsOficio> listaOficios(int codTecnico)
	{
		try
		{
			return Mofi.listaOficios(codTecnico);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool elimina(int codOIficio)
	{
		try
		{
			return Mofi.delete(codOIficio);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool getSiOficioUtilizado(int codOficio)
	{
		try
		{
			return Mofi.getSiOficioUtilizado(codOficio);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return true;
		}
	}
}
