using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmPropuestaDePedido
{
	private MysqlPropuestaDePedido MPropuestaDePedido = new MysqlPropuestaDePedido();

	public int insertPropuestaDePedido_OC(clsPropuestaDePedido propuesta)
	{
		try
		{
			return MPropuestaDePedido.insertpropuestapedido_oc(propuesta);
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

	public int insertPropuestaDePedido_RA(clsPropuestaDePedido propuesta)
	{
		try
		{
			return MPropuestaDePedido.insertpropuestapedido_ra(propuesta);
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

	public bool insertDetallePropuestaPedidoVisualizacion(int codigo_propuesta_creada, List<clsDetallePropuestaDePedido> lista)
	{
		try
		{
			return MPropuestaDePedido.insertaDetallePropuestaPedidoVisualizacion(codigo_propuesta_creada, lista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsPropuestaDePedido cargaPropuestaDePedido(int codPropuesta)
	{
		try
		{
			return MPropuestaDePedido.cargaPropuestaDePedido(codPropuesta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable cargaDetallePropuestaDePedido(int codigo)
	{
		try
		{
			return MPropuestaDePedido.cargaDetallePropuestaDePedido(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable listadoPropuestaOrdenDeCompra(int tipo_propuesta, int cod_almacen, int cod_sucursal)
	{
		try
		{
			return MPropuestaDePedido.listadoPropuestaOrdenDeCompra(tipo_propuesta, cod_almacen, cod_sucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable listadoPropuestaOrdenDeCompraSegunFecha(int tipo_propuesta, int cod_almacen, int cod_sucursal, int fecha, DateTime desde, DateTime hasta)
	{
		try
		{
			return MPropuestaDePedido.listadoPropuestaOrdenDeCompraSegunFecha(tipo_propuesta, cod_almacen, cod_sucursal, fecha, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable cargaDetallePropuestaDePedidoVisualizacion(int codigo)
	{
		try
		{
			return MPropuestaDePedido.cargaDetallePropuestaDePedidoVisualizacion(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsCotizacionPropuestaDePedido> cargaListaDeCotizacion(int codigo_propuesta)
	{
		try
		{
			return MPropuestaDePedido.cargaListadoDeCotizacion(codigo_propuesta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsAlmacenPropuestaDePedido> cargaListadoDeAlmacenes(int codigo_propuesta)
	{
		try
		{
			return MPropuestaDePedido.cargaListadoDeAlmacenes(codigo_propuesta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCotizacionPropuestaDePedido cargaCotizacionDepropuestaDePedidoSeleccionada(int codigo_detalle)
	{
		try
		{
			return MPropuestaDePedido.cargaCotizacionDepropuestaDePedidoSeleccionada(codigo_detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsAlmacenPropuestaDePedido cargaStockAlmacenDepropuestaDePedidoSeleccionada(int codigo_detalle)
	{
		try
		{
			return MPropuestaDePedido.cargaStockAlmacenDepropuestaDePedidoSeleccionada(codigo_detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool actualizarPropuestaDePedido(clsPropuestaDePedido prop_pedi)
	{
		try
		{
			return MPropuestaDePedido.actualizaPropuestaDePedido(prop_pedi);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal clsDetallePropuestaDePedido getDetallePropuestaxCodProducto(int codigo_Producto, int codigoPropuesta)
	{
		try
		{
			return MPropuestaDePedido.getDetallePropuesta(codigo_Producto, codigoPropuesta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool actualizaDetallePropuestaDePedido(List<clsDetallePropuestaDePedido> lista_detalle_prop_insertar, List<clsDetallePropuestaDePedido> lista_detalle_prop_actualizar, List<clsDetallePropuestaDePedido> lista_detalle_prop_cot_selecc, List<clsDetallePropuestaDePedido> lista_detalle_prop_eliminar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_insertar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_eliminar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_insertar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_actualizar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_eliminar)
	{
		try
		{
			return MPropuestaDePedido.actualizaDetallePropuestaDePedido(lista_detalle_prop_insertar, lista_detalle_prop_actualizar, lista_detalle_prop_cot_selecc, lista_detalle_prop_eliminar, listado_cotizacion_insertar, listado_cotizacion_eliminar, listado_cotizacion_detalle_insertar, listado_cotizacion_detalle_actualizar, listado_cotizacion_detalle_eliminar);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool actualizaDetallePropuestaDePedido(List<clsDetallePropuestaDePedido> lista_detalle_prop_insertar, List<clsDetallePropuestaDePedido> lista_detalle_prop_actualizar, List<clsDetallePropuestaDePedido> lista_detalle_prop_cot_selecc, List<clsDetallePropuestaDePedido> lista_detalle_prop_eliminar, List<clsAlmacenPropuestaDePedido> listado_cotizacion_insertar, List<clsAlmacenPropuestaDePedido> listado_cotizacion_eliminar, List<clsAlmacenPropuestaDePedido> listado_cotizacion_actualizar)
	{
		try
		{
			return MPropuestaDePedido.actualizaDetallePropuestaDePedido(lista_detalle_prop_insertar, lista_detalle_prop_actualizar, lista_detalle_prop_cot_selecc, lista_detalle_prop_eliminar, listado_cotizacion_insertar, listado_cotizacion_eliminar, listado_cotizacion_actualizar);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertdetalleproductosagrupados(clsDetallePlantillaDeProductos detalle)
	{
		try
		{
			return MPropuestaDePedido.insertdetalleproductosagrupados(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updateproductosagrupados(clsPlantillaDeProductos producto)
	{
		try
		{
			return MPropuestaDePedido.updateproductosagrupados(producto);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalleproductosagrupados(clsDetallePlantillaDeProductos detalle)
	{
		try
		{
			return MPropuestaDePedido.updatedetalleproductosagrupados(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable cargadetalleproductosagrupados(int codproductoo)
	{
		try
		{
			return MPropuestaDePedido.cargaDetallePropuestaDePedido(codproductoo);
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
			return MPropuestaDePedido.cargaProductoAgrupado(CodProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable listarRequerimientosDeAlmacenGenerados(int codigo)
	{
		try
		{
			return MPropuestaDePedido.lista_req_almacen_generados(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable listaplantillas(int codAlmacen, int codSucursal)
	{
		try
		{
			return MPropuestaDePedido.listaplantillas(codAlmacen, codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable cargaDetallePropuestaDePedidoVisualizacionSegunPlantillaGenerada(int codPlantillaGenerada, int codAlmacen, int codPropPedi)
	{
		try
		{
			return MPropuestaDePedido.cargaDetallePropuestaDePedidoVisualizacionSegunPlantillaGenerada(codPlantillaGenerada, codAlmacen, codPropPedi);
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
			return MPropuestaDePedido.listatipoplantillas();
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
			return MPropuestaDePedido.actualizaPlantilla(clsplantillaprod, listainsertar, listaeliminar, listaactualizar);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool cambiarEstadoPropuesta(int codigo_prop, int estado)
	{
		try
		{
			return MPropuestaDePedido.cambiaEstadoPropuesta(codigo_prop, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable listarOrdenesDeCompraGeneradas(int codPropPedido)
	{
		try
		{
			return MPropuestaDePedido.listarOrdenesDeCompraGeneradas(codPropPedido);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable MuestraPropuestaxProducto(int tipoPropuesta, int tipoFecha, DateTime desde, DateTime hasta, int codAlmacen, int codSucursal, int codProd)
	{
		try
		{
			return MPropuestaDePedido.listarPropuestasXProducto(tipoPropuesta, tipoFecha, desde, hasta, codAlmacen, codSucursal, codProd);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal void actualizaDetallePropuestaRecalculoVisualizacion(List<clsDetallePropuestaDePedido> lista_vis_act)
	{
		try
		{
			MPropuestaDePedido.actualizaDetallePropuestaRecalculoVisualizacion(lista_vis_act);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	internal DataTable ListaTiposModificacionesParaPUC()
	{
		try
		{
			return MPropuestaDePedido.ListaModificacionesParaPUC();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable listarAlmacenesParaPropReqAlmacen(int codAlmacen)
	{
		try
		{
			return MPropuestaDePedido.ListaAlmacenesParaPropRA(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool actualizaDetallePropuestaRecalculo(List<clsDetallePropuestaDePedido> lista_ins, List<clsDetallePropuestaDePedido> lista_act, List<clsDetallePropuestaDePedido> lista_del)
	{
		try
		{
			return MPropuestaDePedido.actualizaDetallePropuestaRecalculo(lista_ins, lista_act, lista_del);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool eliminarPropuesta(int codigoProp, int eliminado)
	{
		try
		{
			return MPropuestaDePedido.eliminarPropuestaDePedido(codigoProp, eliminado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool eliminaDetallePropuestaVisualizacion(int codigoProp)
	{
		try
		{
			return MPropuestaDePedido.eliminarPropuestaDePedidoVisualizacion(codigoProp);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
