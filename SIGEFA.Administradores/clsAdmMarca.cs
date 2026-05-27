using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmMarca
{
	private IMarca Mmar = new MysqlMarca();

	public bool insert(clsMarca mar)
	{
		try
		{
			return Mmar.Insert(mar);
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

	public bool update(clsMarca mar)
	{
		try
		{
			return Mmar.Update(mar);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int Codmar)
	{
		try
		{
			return Mmar.Delete(Codmar);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraMarcas()
	{
		try
		{
			return Mmar.ListaMarcas();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable listaproductosmarca(int codmarca, int almacen, int familia, int proveedor)
	{
		try
		{
			return Mmar.listaproductosmarca(codmarca, almacen, familia, proveedor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable listaproductoslineafamilia(int linea, int almacen, DateTime fechainicio, DateTime fechafin, int familia, int sucursal)
	{
		try
		{
			return Mmar.listaproductoslineamarca(linea, almacen, fechainicio, fechafin, familia, sucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
