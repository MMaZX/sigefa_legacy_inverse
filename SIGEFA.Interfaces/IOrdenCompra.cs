using System;
using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IOrdenCompra
{
	bool insert(clsOrdenCompra Orden);

	bool update(clsOrdenCompra Orden);

	bool delete(int CodigoOrden);

	bool anular(int CodigoOrden);

	bool activar(int CodigoOrden);

	bool insertdetalle(clsDetalleOrdenCompra Detalle);

	bool insertdetalle_1(clsDetalleOrdenCompra Detalle);

	bool updatedetalle(clsDetalleOrdenCompra Detalle);

	bool updatedetalle_1(clsDetalleOrdenCompra Detalle);

	bool Eliminar_detalleOC_Promocion_1(int orden, int codProd, int CodEstado);

	bool actualizaestadorden(int CodDetalleOrdenCompra, int estado, int codGuia);

	bool actualizacantidadpendiente(int CodDetalleOrdenCompra, int estado, int CodGuia);

	bool actualizanuevaestadorden(int CodDetalleOrdenCompra, int estado);

	bool ActualizaOrdenCompra_Estado(int orden, int estado);

	bool ActualizarDetOrdenCompra_Estado(int orden, int codProd, int estado);

	bool actualizacantidad(int CodDetalleOrdenCompra, int estado);

	bool actualizaestadocabeceraorden(int codOrdenCompra, int estado);

	bool actualizanuevaestadocabeceraorden(int codOrdenCompra, int estado);

	bool deletedetalle(int CodigoDetalle);

	bool deletedetalleorden(int Codproducto, int CodigoOrden);

	clsOrdenCompra CargaOrdenCompra(int CodOrden);

	List<clsDetalleOrdenCompra> cargadetalleorden(int CodOrden, int almacen);

	DataTable ListaProductosModificarPrecio(int codOrdenCompra);

	DataTable ListaProductosModificarPrecio_1(int codOrdenCompra, int codemp);

	DataTable CargaDetalle(int CodOrdenn);

	DataTable CargaDetalle_Factura_Venta(int CodOrdennVenta);

	DataTable CargaDetalleOrden(int CodOrden);

	DataTable ListaOrdenesCompra(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);

	DataTable ListaOrdenes(int CodAlmacen, int tipoFecha, DateTime fechaInicio, DateTime fechaFinal, int codProd, int codEstado);

	DataTable ListaOrdenes_seteados(int CodAlmacen, int codemp);

	DataTable listacomboOrden();

	DataTable ListaOrden();

	clsOrdenCompra BuscaOrden(string CodOrden, int CodAlmacen);

	DataTable StockActualProducto(int CodProducto);

	string getEstadoOrdenCompra(int estadoOrden);

	DataTable generarGuiaRemisionCOmpra(int codOrdenCompra);

	int getCodUltimaGuiaRemisionGenerada(int codOrdenCompra);

	bool setEstadoOrdenCompra(int codOrdenCompra, int nuevoEstado);

	bool actualizaCantidadPendienteOrdenCompra(int CodDetalleOrdenCompra);

	bool registrarModificacionDeOC(int codOrdenCompra, int iCodUser, DateTime fecha);

	bool registrarAprobacionDeOC(int codOrdenCompra, int iCodUser, DateTime fecha);

	DataTable obtenerListadoProductoFleteDeGRC(int codigoOC);

	DataTable listarGRCGeneradas(int codigoOC);

	bool enviarDatoModificarPrecio(DetalleModificarPrecio obj);

	bool enviarDatoModificarPrecio_fletenuevo(DetalleModificarPrecio obj);

	bool enviarDatoModificarPrecio_preciocompranuevo(DetalleModificarPrecio obj);

	bool enviarDatoModificarPrecio_precioventanuevo(DetalleModificarPrecio obj);

	bool enviarDatoModificarPrecio_precioventa_competencia(DetalleModificarPrecio obj);

	bool enviarDatoModificarPrecio_precioventa_SKU(DetalleModificarPrecio obj);

	bool enviarDatoModificarPrecio_precioventa_link(DetalleModificarPrecio obj);
}
