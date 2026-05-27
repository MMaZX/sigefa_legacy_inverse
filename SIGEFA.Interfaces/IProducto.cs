using System;
using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IProducto
{
	bool Insert(clsProducto ProductoNuevo);

	bool Update(clsProducto Producto);

	bool Delete(int Codigo);

	bool ValidaStockProducto(int Codigo);

	DataTable MuestraStockAlmacenes(int CodProducto);

	bool InsertProductoAlmacen(clsProducto ProductoAlmacenNuevo);

	bool UpdateProductoAlmacen(clsProducto ProductoAlmacen);

	bool DeleteProductoAlmacen(int CodProductoAlmacen);

	bool InsertCaracteristica(clsCaracteristicaProducto CaracNuevo);

	bool DeleteCaracteristica(int Codigo);

	DataTable ListaCaracteristicas(int CodProducto);

	bool InsertNota(clsNotaProducto NotaProducto);

	bool DeleteNota(int Codigo);

	DataTable ListaNotas(int CodProducto);

	bool InsertUnidad(clsUnidadEquivalente NuevaUnidad, int coti);

	bool UpdateUnidad(clsUnidadEquivalente Unidad);

	bool DeleteUnidad(int Codigo);

	DataTable cargaReferenciasExternas(int codProducto, int codUnidadMedida);

	clsUnidadEquivalente CargaUnidadEquivalente(int Coduni, int Codpro, int compraVenta);

	DataTable ListaUnidadesEquivalentesCompra(int CodigoProducto, int codAlmacen);

	DataTable ListaUnidadesEquivalentesVentaCotizacion(int CodigoProducto, int codAlmacen, int CodCotizacion);

	DataTable cargaetiquetas(int codAlmacen);

	DataTable cargaetiquetasolola3(int codAlmacen);

	DataTable cargaetiquetauna(int codAlmacen);

	DataTable ListaUnidadesEquivalentesVenta(int CodigoProducto, int codAlmacen);

	DataTable ListaUnidadesEquivalentesVenta1(int CodigoProducto, int codAlmacen);

	DataTable ListaUnidadesEquivalentes(int CodigoProducto, int codAlmacen);

	DataTable CargaUnidadesEquivalentes(int CodigoProducto);

	clsPrecioEquivalente PrecioStock(int cmunidad, int CodProducto, int undBase);

	bool UpdateUnidadEquivalente(int cod, decimal precio);

	int getUnidadCompra(int codProd);

	clsProducto ListaTotalprod2(int CodProducto, int CodAlmacen, int CodUnidad);

	clsProducto CargaProducto(int CodProducto, int CodAlmacen);

	clsProducto CargaProductoCotizacion(int CodProducto, int CodAlmacen);

	clsProducto CargaProductoDetalle(int CodPro, int CodAlm, int Caso, int Lista, int totalstock);

	clsProducto CargaProductoDetalleCotizacion(int CodPro, int CodCotizacion, int CodAlm, int Caso, int Lista);

	clsProducto CargaProductoDetalleSinAfectarStock(int CodPro, int CodAlm, int Caso, int Lista);

	clsProducto CargaProductoDetalleCodBarras(string CodProducto, int CodAlmacen, int Caso, int CodLista);

	clsProducto CargaProductoDetalle1(int CodPro, int CodAlm, int Caso, int Lista);

	clsProducto CargaDatosProductoOrden(int CodPro, int CodAlm, int codusu, decimal cant);

	clsProducto CargaDatosProductoAgrupados(int CodPro);

	clsProducto CargaProductoDetalleR(string Referencia, int CodAlm, int Caso, int Lista);

	DataTable ListaProductos(int nivel, int codigo, int CodAlmacen);

	bool registraReferenciaExterna(ref ReferenciaExterna obj);

	DataTable CatalogoProductos();

	DataTable CatalogoProductosCotizacion();

	bool ActualizaEstadoProductoCotizacion(int Codigo);

	DataTable ListaProductosReporte(int CodAlmacen, int Tipo, int Inicio);

	DataTable RelacionProductosIngreso(int Tipo, int codalma);

	DataTable RelacionIngresoPorProveedor(int Tipo, int codalma, int codproveedor);

	DataTable RelacionProductosSalida(int Tipo, int codalmacen, int codlista);

	DataTable RelacionProductosSalidaTodo(int Tipo, int codalmacen, int codlista);

	DataTable RelacionProductosSalidaTodoSinStock(int Tipo, int codalmacen, int codlista);

	DataTable RelacionSalidaSinAfectarStock(int Tipo, int codalmacen, int codlista);

	DataTable BuscaProductos(int Criterio, string Filtro);

	DataTable ArbolProductos();

	DataTable StockProductoAlmacenes(int codEmpre, int codPro);

	DataTable MuestraProductosProveedor(int codProducto, int codAlmacen);

	clsProducto MuestraProductosTransferencia(int codProducto, int codAlmacen);

	clsProducto MuestraProductosTransferencia_nuevo(int codProducto, int codAlmacen);

	bool cambiarFleteDeProducto(int codProd, double flete);

	DataTable RelacionProductosCotizacion(int Tipo, int codAlmacen, int codlista, int todos);

	decimal CargaPrecioProducto(int CodProducto, int CodAlmacen, int codmon);

	DataTable MuestraStockAlmacenes();

	DataTable MuestraStockAlmacenesPendientes();

	DataTable muestraStockProducto_almacenes(int CodProducto);

	DataTable BuscarProducto(int codProducto);

	DataTable RelacionProductos(int codalma);

	List<clsProducto> VentasProductosCount(int CodFac);

	DataTable RelacionVendedor(int CodTipArt, int CodAlmacen, int CodLista, int CodVendedor);

	List<clsProducto> ListaProdConsultor(int CodVendedor);

	bool editaReferenciaExterna(ReferenciaExterna obj);

	List<clsProducto> ListaProdAlmacen(int codProducto, int CodAlmacen);

	int UnidadBase(int codPro, int codalma);

	decimal FactorProducto(int codPro, int undBase, int undEqui, int tipo);

	string SiglaUnidadBase(int codUnd);

	clsUnidadEquivalente PrecioVenta(int coduni, int codalmacen);

	clsUnidadEquivalente PrecioVentaCotizacion(int coduni, int codalmacen, int codcotizacion);

	clsUnidadEquivalente PrecioVentaSinStock(int coduni, int codalmacen);

	clsUnidadEquivalente Factor(int codProducto, int codUnidadMedida, int codUnidaEqui);

	clsProducto PrecioPromedio(int codProducto, int codalm);

	int GetCodProducto_xDescripcion(string descripcion);

	bool eliminaReferenciaExterna(int codReferencia);

	int ValidaCodigoUE(int codigo);

	int ValidaCodigoUE(string unidad);

	int ValidaCodigoProducto(int codigo);

	int ExisteProductoSinFacturar(int codigo);

	int ValidaCodigoMoneda(int codigo);

	int ValidaCodigoMoneda(string moneda);

	int ValidaTipoPrecio(int codigo);

	int GetCodUnidad(string descripcion);

	int GetCodTipoPrecio(string descripcion);

	int GetCodMoneda(string descripcion);

	int ValidaTipoPrecio(string tipoPrecio);

	int ValidaUnidadEquivalente(int codigo);

	DataTable MuestratipoNC();

	decimal UltimoPrecioCompraProducto(int codigoProducto, int codigoUnidad, int codigoUnidadEquivalente);

	decimal UltimoPrecioCompraProductoCotizacion(int codigoProducto, int codigoUnidadEquivalente);

	DataTable CostoTotalProducto(int codigoProducto, int codigoUnidadEquivalente);

	DataTable CostoTotalProductoCotizacion(int codigoProducto, int codigoUnidadEquivalente);

	decimal UltimoPrecioVentaProductoCotizacion(int CodCliente, int codigoProducto, int codigoUnidadEquivalente);

	decimal UltimoPrecioVentaProducto(int codigoProducto, int codigoUnidad);

	bool ActualizarPrecioVentaProductoPorUnidad(int codigoProducto, int codigoUnidad, int codigoAlmacen, decimal nuevoPrecioVenta);

	int GetUnidadesEquivalentesPorUnidadBase(int codProducto, int codUnidadBase);

	int MuestraCantidadProductos();

	int GetNumeroUnidadesEquivalentesPorProducto(int codProducto);

	int VerificaProductoAlmacen(int codProducto);

	decimal GetValorPromedioSoles(int codProducto, int codAlmacen);

	DataTable GetPromedioProductosVendidos(DateTime fechainicio, DateTime fechafinal, int codFamilia, int codLinea, int codGrupo, int codMarca, int codProducto, int cantidadDias);

	DataTable GetTotalizadoProductosVendidos(DateTime fechainicio, DateTime fechafinal, int codFamilia, int codLinea, int codGrupo, int codMarca, int codProducto, int cantidadDias);

	int GetProductoFacturado(int codProducto);

	DataTable CargaProductoSunat();

	bool UpdateMasivo(DataTable prods);

	bool UpdateUnidadEquivalenteMasivo(DataTable unds);

	List<clsUnidadEquivalente> unidadCompraxProducto(int codproducto);

	List<clsUnidadEquivalente> unidadVentaxProducto(int codproducto);

	DataTable listarStocksProducto(int codpro);

	bool insertaStocksProducto(int codpro, int codalma, decimal stockmin, decimal stockmax, decimal capmax);

	bool updateStocksProducto(int codpro, int codalma, decimal stockmin, decimal stockmax, decimal capmax);

	bool eliminaStocksProducto(int codpro, int codalma);

	DataTable PrecioVentaProductoPorUnidad(int codigoProducto, int codigoUnidad);

	DataTable listaPreciosCantidad(int codproducto);

	bool GuardaPrecioCantidad(int codueq, decimal cantmax, decimal cantmin, int coduser);

	bool GuardaCategorizacion(int codproducto, string desde, string hasta, string descripcion);

	bool GuardaSituacion(int codproducto, string desde, string hasta, string descripcion);

	bool ActualizaStockDisponible(int codproducto, int codalmacen, decimal stock);

	bool ActualizaCategorizacion(int codcategorizacion, string condicion, string descripcion);

	bool ActualizaSituacion(int codsituacion, string desde, string hasta, string descripcion);

	bool EliminaPrecioCantidad(int codpreciocantidad);

	DataTable validaPrecioCantidad(int codequi, int codpro, decimal cant);

	DataTable listadodeproductos(int almacen, int marca, int familia, int linea, int grupo, int proveedor);

	double obtenerFleteDeProducto(double codProducto, int igv, int codunidad, double cantidad);

	DataTable RelacionProductosParaRequerimientoAlmacen(int tipoArt, int CodAlmacen, int codLista);

	bool creaProductoAlmacenMasivo(int codProducto, int codUsuario);

	bool insertaProductoAsociado(int codProd, int codAsoc);

	bool deleteAsociadosDeProducto(int codProd, int codAsoc);

	DataTable cargaProductosAsociados();

	DataTable cargaProductos();

	DataTable cargaStockProductos();

	int getCantidadMaximaAsociadosXProducto();

	DataTable CargaCategorizacion(int codproducto);

	DataTable CargaSituacion(int codproducto);

	bool EliminaCategorizacion(int codcategorizacion);

	bool EliminaSituacion(int codsituacion);

	DataTable CatalogoCombosProductos(bool estado, int tipoformulario);

	bool Deletecombo(int codcombo);

	bool InsertCombo(clsComboProductos combo);

	clsComboProductos CargaCombo(int CodProducto);

	DataTable CargaProductosCombo(int CodProducto);

	DataTable CargaDetalleComboVenta(int codcombo, int CodAlmacen);

	bool insertdetallecombo(clsDetalleCombo Detalle);

	bool Updatecombo(clsComboProductos combo);

	decimal UltimoPrecioCompraProductoVenta(int codigoProducto, int codigoUnidad, int codigoUnidadEquivalente);

	DataTable CostoTotalProductoVenta(int codigoProducto, int codigoUnidadEquivalente);
}
