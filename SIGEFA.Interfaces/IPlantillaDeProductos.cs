using System;
using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IPlantillaDeProductos
{
	int insertproductosagrupados(clsPlantillaDeProductos pla_producto);

	DataTable listaplantillas(int codAlmacen, int codSucursal);

	clsPlantillaDeProductos cargaProductoAgrupado(int CodPro);

	DataTable cargadetalleproductosagrupados(int cod_pla_producto);

	DataTable cargadetalleproductosagrupados_111(int cod_pla_producto, int codEmp);

	DataTable listatipoplantillas();

	bool actualizaPlantilla(clsPlantillaDeProductos clsplantillaprod, List<clsDetallePlantillaDeProductos> listainsertar, List<clsDetallePlantillaDeProductos> listaeliminar, List<clsDetallePlantillaDeProductos> listaactualizar);

	DataTable listaPlantillasXProducto(int codAlmacen, int codSucursal, int codProducto);

	DataTable listaPlantillasPorGenerar(int codAlmacen, int codSucursal, int tipo);

	DataTable listaDetPlantillas();

	DataTable listaplantillas(int codAlmacen, int codSucursal, int codProducto, int tipoPlantilla, int tipoFecha, DateTime fechaInicio, DateTime fechaFin, int codEm);

	bool cambiaEstadoPlantilla(int codPlantilla, int estado, int codusuario);

	bool validarEliminacionPlantilla(int codigo, int tipo, out string mensaje);

	bool existeProducto(string codProducto);

	bool SetDataProductosPlantillas(clsDetallePlantillaDeProductos aux, int codAlmacen);

	bool actualizaStockdeDetallePlantilla(clsDetallePlantillaDeProductos aux);

	bool actualizadatosproducto(clsProducto producto);
}
