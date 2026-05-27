using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmOrdenCompraCotizacion
{
	private IOrdenCompraCotizacion MOrden = new MysqlOrdenCompraCotizacion();

	public bool insert(clsOrdenCompraCotizacion parametro)
	{
		try
		{
			return MOrden.Insert(parametro);
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

	public bool insertdetalle(clsDetalleOrdenCompraCotizacion detalle)
	{
		try
		{
			return MOrden.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatecotizacion(clsDetalleCotizacion detalle)
	{
		try
		{
			return MOrden.updatecotizacion(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsOrdenCompraCotizacion parametro)
	{
		try
		{
			return MOrden.Update(parametro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListadaOrdenesCompra(int CodEmpresa)
	{
		try
		{
			return MOrden.ListadaOrdenesCompra(CodEmpresa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsOrdenCompraCotizacion CargaOrdenCompraCotizacion(int CodOrdenCompraCotizacion, int CodAlmacen)
	{
		try
		{
			return MOrden.CargaOrdenCompraCotizacion(CodOrdenCompraCotizacion, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleOrdenCompra(int CodOrdenCompra, int CodAlmacen)
	{
		try
		{
			return MOrden.CargaDetalleOrdenCompra(CodOrdenCompra, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaOrdenesCompraCotizacionesxVigente(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return MOrden.ListaOrdenesCompraCotizacionesxVigente(CodAlmacen, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
