using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmOrdenCompra
{
	private IOrdenCompra MOrden = new MysqlOrdenCompra();

	public bool insert(clsOrdenCompra Orden)
	{
		try
		{
			return MOrden.insert(Orden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertdetalle(clsDetalleOrdenCompra detalle)
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

	public bool insertdetalle_1(clsDetalleOrdenCompra detalle)
	{
		try
		{
			return MOrden.insertdetalle_1(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaProductosModificarPrecio(int codOrdenCompra)
	{
		try
		{
			return MOrden.ListaProductosModificarPrecio(codOrdenCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaProductosModificarPrecioA_1(int codOrdenCompra, int codemp)
	{
		try
		{
			return MOrden.ListaProductosModificarPrecio_1(codOrdenCompra, codemp);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool update(clsOrdenCompra Orden)
	{
		try
		{
			return MOrden.update(Orden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleOrdenCompra detalle)
	{
		try
		{
			return MOrden.updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle_1(clsDetalleOrdenCompra detalle)
	{
		try
		{
			return MOrden.updatedetalle_1(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Eliminar_detalleOC_Promocion_1(int orden, int codProd, int CodEstado)
	{
		try
		{
			return MOrden.Eliminar_detalleOC_Promocion_1(orden, codProd, CodEstado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizaestadorden(int CodDetalleOrdenCompra, int estado, int Codguia)
	{
		try
		{
			return MOrden.actualizaestadorden(CodDetalleOrdenCompra, estado, Codguia);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizacantidadpendiente(int CodDetalleOrdenCompra, int estado, int CodGuia)
	{
		try
		{
			return MOrden.actualizacantidadpendiente(CodDetalleOrdenCompra, estado, CodGuia);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizaCantidadPendienteordenCompra(int CodOrdenCompra)
	{
		try
		{
			return MOrden.actualizaCantidadPendienteOrdenCompra(CodOrdenCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizanuevaestadorden(int CodDetalleOrdenCompra, int estado)
	{
		try
		{
			return MOrden.actualizanuevaestadorden(CodDetalleOrdenCompra, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaOrdenCompra_Estado(int orden, int estado)
	{
		try
		{
			return MOrden.ActualizaOrdenCompra_Estado(orden, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaDetOrdenCompra_Estado(int orden, int codProd, int estado)
	{
		try
		{
			return MOrden.ActualizarDetOrdenCompra_Estado(orden, codProd, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizacantidad(int CodDetalleOrdenCompra, int estado)
	{
		try
		{
			return MOrden.actualizacantidad(CodDetalleOrdenCompra, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizaestadocabeceraorden(int codOrdenCompra, int estado)
	{
		try
		{
			return MOrden.actualizaestadocabeceraorden(codOrdenCompra, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizanuevaestadocabeceraorden(int codOrdenCompra, int estado)
	{
		try
		{
			return MOrden.actualizaestadocabeceraorden(codOrdenCompra, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int CodigoOrden)
	{
		try
		{
			return MOrden.delete(CodigoOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular(int CodigoOrden)
	{
		try
		{
			return MOrden.anular(CodigoOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool enviarDatoModificarPrecio(DetalleModificarPrecio obj)
	{
		try
		{
			return MOrden.enviarDatoModificarPrecio(obj);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool enviarDatoModificarPrecio_fletenuevo(DetalleModificarPrecio obj)
	{
		try
		{
			return MOrden.enviarDatoModificarPrecio_fletenuevo(obj);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool enviarDatoModificarPrecio_preciocompranuevo(DetalleModificarPrecio obj)
	{
		try
		{
			return MOrden.enviarDatoModificarPrecio_preciocompranuevo(obj);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool enviarDatoModificarPrecio_precioventanuevo(DetalleModificarPrecio obj)
	{
		try
		{
			return MOrden.enviarDatoModificarPrecio_precioventanuevo(obj);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool enviarDatoModificarPrecio_precioventa_competencia(DetalleModificarPrecio obj)
	{
		try
		{
			return MOrden.enviarDatoModificarPrecio_precioventa_competencia(obj);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool enviarDatoModificarPrecio_precioventa_SKU(DetalleModificarPrecio obj)
	{
		try
		{
			return MOrden.enviarDatoModificarPrecio_precioventa_SKU(obj);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool enviarDatoModificarPrecio_precioventa_link(DetalleModificarPrecio obj)
	{
		try
		{
			return MOrden.enviarDatoModificarPrecio_precioventa_link(obj);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool activar(int CodigoOrden)
	{
		try
		{
			return MOrden.activar(CodigoOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int CodigoOrden)
	{
		try
		{
			return MOrden.deletedetalle(CodigoOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalleorden(int Codproducto, int CodigoOrden)
	{
		try
		{
			return MOrden.deletedetalleorden(Codproducto, CodigoOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsOrdenCompra CargaOrdenCompra(int CodOrden)
	{
		try
		{
			return MOrden.CargaOrdenCompra(CodOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsDetalleOrdenCompra> cargadetalleorden(int CodOrden, int almacen)
	{
		try
		{
			return MOrden.cargadetalleorden(CodOrden, almacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int CodOrdenn)
	{
		try
		{
			return MOrden.CargaDetalle(CodOrdenn);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle_Factura_Venta(int CodOrdennVenta)
	{
		try
		{
			return MOrden.CargaDetalle_Factura_Venta(CodOrdennVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleOrden(int CodOrden)
	{
		try
		{
			return MOrden.CargaDetalleOrden(CodOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaOrdenesCompra(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return MOrden.ListaOrdenesCompra(Criterio, CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable listacomboOrden()
	{
		try
		{
			return MOrden.listacomboOrden();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable muestraGRCGeneradas(int codigoOC)
	{
		try
		{
			return MOrden.listarGRCGeneradas(codigoOC);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraOrdenes(int CodAlmacen, int tipoFecha, DateTime fechaInicio, DateTime fechaFinal, int codEstado, int codProd)
	{
		try
		{
			return MOrden.ListaOrdenes(CodAlmacen, tipoFecha, fechaInicio, fechaFinal, codProd, codEstado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraOrdenes_seteados(int CodAlmacen, int codemp)
	{
		try
		{
			return MOrden.ListaOrdenes_seteados(CodAlmacen, codemp);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsOrdenCompra BuscaOrden(string CodOrden, int CodAlmacen)
	{
		try
		{
			return MOrden.BuscaOrden(CodOrden, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaOrden()
	{
		try
		{
			return MOrden.ListaOrden();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable StockActualProducto(int CodProducto)
	{
		try
		{
			return MOrden.StockActualProducto(CodProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public string getEstadoOrdenCompra(int estadoOrden)
	{
		try
		{
			return MOrden.getEstadoOrdenCompra(estadoOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable generarGuiaRemisionOrdenCompra(int codOrdenCompra)
	{
		try
		{
			return MOrden.generarGuiaRemisionCOmpra(codOrdenCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int obtenerCodUltimaGuiaRemisionGenerada(int codOrdenCompra)
	{
		try
		{
			return MOrden.getCodUltimaGuiaRemisionGenerada(codOrdenCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	internal bool setEstadoOrdenCompra(int codOrdenCompra, int nuevoEstado)
	{
		try
		{
			return MOrden.setEstadoOrdenCompra(codOrdenCompra, nuevoEstado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool registrarModificacionDeOC(int codOrdenCompra, int iCodUser, DateTime fecha)
	{
		try
		{
			return MOrden.registrarModificacionDeOC(codOrdenCompra, iCodUser, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool registrarAprobadorDeOC(int codOC, int iCodUser, DateTime fecha)
	{
		try
		{
			return MOrden.registrarAprobacionDeOC(codOC, iCodUser, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable obtenerListadoProductoFleteDeGRC(int codigoOC)
	{
		try
		{
			return MOrden.obtenerListadoProductoFleteDeGRC(codigoOC);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
