using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmMovimientoStock
{
	private MysqlMovimientoStock MMovStock = new MysqlMovimientoStock();

	internal DataTable listaMovimientoStock(int codAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			return MMovStock.ListaMovimientoStock(codAlmacen, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
