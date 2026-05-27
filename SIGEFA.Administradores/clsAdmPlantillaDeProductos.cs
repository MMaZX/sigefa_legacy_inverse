using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmPlantillaDeProductos
{
	private IPlantillaDeProductos MPlantillaDeProductos = new MysqlPlantillaDeProductos();

	public int insertproductosagrupados(clsPlantillaDeProductos producto)
	{
		try
		{
			return MPlantillaDeProductos.insertproductosagrupados(producto);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -1;
		}
	}

	public DataTable cargadetalleproductosagrupados(int codproductoo)
	{
		try
		{
			return MPlantillaDeProductos.cargadetalleproductosagrupados(codproductoo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable cargadetalleproductosagrupados_111(int codproductoo, int codEmp)
	{
		try
		{
			return MPlantillaDeProductos.cargadetalleproductosagrupados_111(codproductoo, codEmp);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsPlantillaDeProductos CargaProductoAgrupado(int CodProducto)
	{
		try
		{
			return MPlantillaDeProductos.cargaProductoAgrupado(CodProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool cambiaEstadoPlantilla(int codPlantilla, int estado, int codusuario = 0)
	{
		try
		{
			return MPlantillaDeProductos.cambiaEstadoPlantilla(codPlantilla, estado, codusuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable listaPlantillasPorGenerar(int codAlmacen, int codSucursal, int tipo = 0)
	{
		try
		{
			return MPlantillaDeProductos.listaPlantillasPorGenerar(codAlmacen, codSucursal, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable listaDetPlantillas()
	{
		try
		{
			return MPlantillaDeProductos.listaDetPlantillas();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool existeProducto(string codProducto)
	{
		try
		{
			return MPlantillaDeProductos.existeProducto(codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable listaplantillas(int codAlmacen, int codSucursal)
	{
		try
		{
			return MPlantillaDeProductos.listaplantillas(codAlmacen, codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool SetDataProductosPlantillas(clsDetallePlantillaDeProductos aux, int codAlmacen)
	{
		try
		{
			return MPlantillaDeProductos.SetDataProductosPlantillas(aux, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable listaplantillas(int codAlmacen, int codSucursal, int codProducto, int tipoPlantilla, int tipoFecha, DateTime fechaInicio, DateTime fechaFin, int codEm)
	{
		try
		{
			return MPlantillaDeProductos.listaplantillas(codAlmacen, codSucursal, codProducto, tipoPlantilla, tipoFecha, fechaInicio, fechaFin, codEm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListadoTipoPlantilla()
	{
		try
		{
			return MPlantillaDeProductos.listatipoplantillas();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool actualizaPlantilla(clsPlantillaDeProductos clsplantillaprod, List<clsDetallePlantillaDeProductos> listainsertar, List<clsDetallePlantillaDeProductos> listaactualizar, List<clsDetallePlantillaDeProductos> listaeliminar)
	{
		try
		{
			return MPlantillaDeProductos.actualizaPlantilla(clsplantillaprod, listainsertar, listaeliminar, listaactualizar);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool actualizaStockdeDetallePlantilla(clsDetallePlantillaDeProductos aux)
	{
		try
		{
			return MPlantillaDeProductos.actualizaStockdeDetallePlantilla(aux);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool actualizadatosproducto(clsProducto producto)
	{
		try
		{
			return MPlantillaDeProductos.actualizadatosproducto(producto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraPlantillaxProducto(int codAlmacen, int codSucursal, int codProducto)
	{
		try
		{
			return MPlantillaDeProductos.listaPlantillasXProducto(codAlmacen, codSucursal, codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool validarEliminacionPlantilla(int codigo, int tipo, out string mensaje)
	{
		try
		{
			return MPlantillaDeProductos.validarEliminacionPlantilla(codigo, tipo, out mensaje);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			mensaje = ex.Message;
			return false;
		}
	}
}
