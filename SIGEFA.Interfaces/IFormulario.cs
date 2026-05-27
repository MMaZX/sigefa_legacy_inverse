using System;
using System.Data;

namespace SIGEFA.Interfaces;

internal interface IFormulario
{
	DataTable MuestraFormularios();

	int getPermisoEditarPlantilla();

	bool ejecutarActualizacionDatosProductosDePlantillas();

	int getPermisoAnularOrdenCompra();

	int getPermisoGenerarPropuestaDeCompra();

	int getPermisoAprobarOrdenCompra();

	int getPermisoCerrarOrdenCompra();

	int getPermisoEditarPlantilladeReqAlmacen();

	int getPermisoGenerarPropuestaDeReqAlmacen();

	DataTable cargaListadoProductosADespachar(int tipoFecha, DateTime fecha1, DateTime fecha2, int codAlmacen, int codSucursal);

	DataTable cargaDocumentosDeProductosADespachar(int codProducto, int codUnidad, int codAlmacen, int codSucursal);

	DataTable cargaDocumentosDeProductosADespachar2(int codProducto, int codUnidad, int codAlmacen, int codSucursal);

	int getPermisoCrearDespachoDesdeVenta();

	DataTable reporteDetalladoListadoProductosADespachar(int tipoFecha, DateTime fecha1, DateTime fecha2, int codAlmacen, int codSucursal);

	int getPermisoGenerarEntrega();

	int getPermisoAnularEntrega();

	DataTable listadoTotalCanalesVenta();

	int getPermisoEditarReqReposStock();

	int getPermisoAnularVentas();

	int getPermisoPasarPendiente();

	int getPermisoEliminarPlantillaAlmacen();

	int getPermisoEliminarPlantillaOrdenCompra();

	int getPermisoAceptarTransferencia();

	int getPermisoRechazarTransferencia();

	int getPermisoExportarExcelVentas();

	int getPermisoAumentarPreciodeCompraFacturaGeneradaGR();
}
