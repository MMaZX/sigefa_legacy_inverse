using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmNotaCredito
{
	private INotaCredito MNotaC = new MysqlNotaCredito();

	public bool insert(clsNotaCredito factura)
	{
		try
		{
			return MNotaC.insert(factura);
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

	public bool insertdetalle(clsDetalleNotaCredito detalle)
	{
		try
		{
			return MNotaC.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2, int codsuc)
	{
		try
		{
			return MNotaC.ListaNotasCredito(CodAlmacen, fecha1, fecha2, codsuc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaNotasCreditoAplicadas(int CodAlmacen, DateTime fecha1, DateTime fecha2, int codsuc)
	{
		try
		{
			return MNotaC.ListaNotasCreditoAplicadas(CodAlmacen, fecha1, fecha2, codsuc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaNotasCreditoXProducto(int CodAlmacen, DateTime fecha1, DateTime fecha2, int codsuc, int codprod)
	{
		try
		{
			return MNotaC.ListaNotasCreditoXProducto(CodAlmacen, fecha1, fecha2, codsuc, codprod);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsNotaCredito CargaNotaCredito(int CodNotaCredito)
	{
		try
		{
			return MNotaC.CargaNotaCredito(CodNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsNotaCredito CargaNotaCredito_Regeneracion(int CodNotaCredito)
	{
		try
		{
			return MNotaC.CargaNotaCredito_Regeneracion(CodNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int CodNotaCredito)
	{
		try
		{
			return MNotaC.CargaDetalle(CodNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsNotaCredito> BuscarNotasXCliente(int codCliente)
	{
		try
		{
			return MNotaC.BuscarNotasXCliente(codCliente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool anular(int codNotaCredito)
	{
		try
		{
			return MNotaC.anular(codNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anularFactura_venta(int codFacturaVenta)
	{
		try
		{
			return MNotaC.anularFactura_venta(codFacturaVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizarCodNotaCreditoFV(int codFactura_venta, int codNota, string tipoNotaCredito)
	{
		try
		{
			return MNotaC.actualizarCodNotaCreditoFV(codFactura_venta, codNota, tipoNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizarStockNotaCredito(int codpro, double valor)
	{
		try
		{
			return MNotaC.actualizarStockNotaCredito(codpro, valor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable NCsinRepositorio(int alma, DateTime fechaini, DateTime fechafin)
	{
		try
		{
			return MNotaC.NCsinRepositorio(alma, fechaini, fechafin);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
