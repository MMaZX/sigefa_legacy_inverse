using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmNotaCreditoCompra
{
	private INotaCreditoCompra nota = new MysqlNotaCreditoCompra();

	public bool insert(clsNotaSalida notaS)
	{
		try
		{
			return nota.insert(notaS);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show(ex.Message, "No Se Pudo Guardar");
			return false;
		}
	}

	internal DataTable cargaTipoNCC()
	{
		try
		{
			return nota.cargaTipoNCC();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int getAccionSegunTipoSeleccionado(int seleccionado)
	{
		try
		{
			return nota.getAccionSegunTipoSeleccionado(seleccionado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show(ex.Message, "Error");
			return -1;
		}
	}

	internal bool verificarCodProductoAgregableANotaDeCredito(int codProducto)
	{
		try
		{
			return nota.verificarCodProductoAgregableANotaDeCredito(codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show(ex.Message, "Error");
			return false;
		}
	}

	internal bool insert(clsNotaCredito nota_credito)
	{
		try
		{
			return nota.insert(nota_credito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool insertdetalle(clsDetalleNotaCredito detalle)
	{
		try
		{
			return nota.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool setCodNotaSalida(string codNotaSalida, int codNotaCreditoNueva)
	{
		try
		{
			return nota.setCodNotaSalida(codNotaSalida, codNotaCreditoNueva);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable ListadoEstandarNotaCreditoCompra(int codAlmacen, int codSucursal, int tipoFecha, DateTime date1, DateTime date2, int codProd)
	{
		try
		{
			return nota.ListadoEstandarNotaCreditoCompra(codAlmacen, codSucursal, tipoFecha, date1, date2, codProd);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal clsNotaCredito cargaNotaCredito(int codNotaCC)
	{
		try
		{
			return nota.cargaNotaCredito(codNotaCC);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable cargaDetalleNCC(string codNotaCredito)
	{
		try
		{
			return nota.cargaDetalleNCC(codNotaCredito);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool actualizaSerieyCorrelativo(string codNotaCredito, string serie, string numdoc)
	{
		try
		{
			return nota.actualizaSerieyCorrelativo(codNotaCredito, serie, numdoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool actualizaAsignador(string codNotaCredito, int codUser, DateTime fecha)
	{
		try
		{
			return nota.actualizaAsignador(codNotaCredito, codUser, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool actualizaEstado(string codNotaCredito, int estado)
	{
		try
		{
			return nota.actualizaEstado(codNotaCredito, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool verificarCodFacturaTieneNotaCredito(int codFactura)
	{
		try
		{
			return nota.verificarCodFacturaTieneNotaCredito(codFactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool actualizaFormaPago(string codNotaCredito, int estadoFormaPago)
	{
		try
		{
			return nota.actualizaFormaPago(codNotaCredito, estadoFormaPago);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
