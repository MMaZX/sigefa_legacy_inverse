using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IListaPrecio
{
	bool Insert(clsListaPrecio NuevaListaPrecio);

	bool Update(clsListaPrecio ListaPrecio);

	bool Delete(int Codigo);

	bool Anular(int codSucursal, int Codigo);

	bool Activar(int codSucursal, int Codigo);

	bool Updatedetalle(clsDetalleListaPrecio detalle);

	bool updatedetallePorFiltro(clsDetalleListaPrecio detalle);

	clsListaPrecio CargaListaPrecio(int Codigo);

	DataTable MuestraListas(int codSucursal);

	DataTable MuestraPreciosProducto(int CodProducto, int codSucursal, int codalma);

	bool GeneraPreciosLista(int CodLista, int codalma, int Decimales);

	bool GeneraPreciosListaProveedor(int CodLista, int codSucursal, int Decimales, int CodProveedor);

	List<int> ListaProductosAlmacen(int codSucursal);

	DataTable CargaListaPrecios(int CodLista);

	List<int> MuestraListasProveedor(int codSucursal);

	DataTable MuestraListasPorFiltro(int codSucursal, int rango1, int rango2, int listaorigen, int decimales);

	DataTable MuestraListaPorProveedor(int codSucursal, int codProv, int listaorigen, int decimales);

	DataTable MuestraListaPorFamilia(int codSucursal, int codFam, int listaorigen, int decimales);

	DataTable MuestraListaPorLinea(int codSucursal, int codFam, int codLin, int listaorigen, int decimales);

	DataTable MuestraListaPorRangoProv(int codSucursal, int rango1, int rango2, int codProv, int listaorigen, int decimales);

	DataTable MuestraListaPorRangoFam(int codSucursal, int rango1, int rango2, int codFam, int listaorigen, int decimales);

	DataTable MuestraListaPorProveedorFam(int codSucursal, int codProv, int codFam, int listaorigen, int decimales);

	DataTable MuestraListaPorTodos(int codSucursal, int rango1, int rango2, int codProv, int codFam, int codLin, int listaorigen, int decimales);

	DataTable MuestraListaPorRangoFamLin(int codSucursal, int rango1, int rango2, int codFam, int codLin, int listaorigen, int decimales);

	DataTable MuestraListaPorProveedorFamLin(int codSucursal, int codProv, int codFam, int codLin, int listaorigen, int decimales);

	DataTable MuestraListaParcial(int codSucursal, int rango1, int rango2, int codProv, int codFam, int listaorigen, int decimales);

	DataTable MuestraListaPrecioxFormaPago(int codSucursal, int codForma);
}
