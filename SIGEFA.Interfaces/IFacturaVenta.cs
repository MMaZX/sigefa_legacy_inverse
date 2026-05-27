using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IFacturaVenta
{
	bool insert(clsFacturaVenta factura_venta);

	bool insertComprobante(clsFacturaVenta factura_venta);

	bool update(clsFacturaVenta factura_venta);

	bool updateCobroVenta(clsFacturaVenta factura_venta);

	bool delete(int codigoventa);

	bool anular(int codigoventa, int codUsuario);

	bool activar(int codigoventa);

	bool rollbackfactura(int codigoventa, int tipo);

	bool insertdetalle(clsDetalleFacturaVenta detalle_venta);

	bool updatedetalle(clsDetalleFacturaVenta detalle_venta);

	bool deletedetalle(int codigodetalle_venta);

	bool AnulaDetalleVenta(int codigoDetalle, int codproducto);

	bool insertdetalleventasalida(int codVen, int codSalida);

	bool deletedetalleventasalida();

	bool actualizaEstadoImpreso(int codVen);

	clsFacturaVenta fechaCorrelativoAnterior(int codse);

	clsFacturaVenta BuscaFacturaVenta(int codVenta, int codAlmacen);

	clsFacturaVenta CargaFacturaVenta(int codventa);

	clsFacturaVenta CargaFacturaVentaRegeneracion(int codventa);

	clsNotaIngreso BuscaNotaSalida(int codVenta, int codAlmacen);

	DataTable CargaDetalleNotaSalida(int codventa, int codAlmacen);

	bool UpdateKardex(int codNota, int codProd, int Codalma, decimal Cant, decimal valProm);

	DataTable ListaFacturaVenta(int codalmacen);

	DataTable CargaDetalleVenta(int codventa, int codAlmacen, int guia);

	DataTable CargaDetalleVentaCodventa(clsFacturaVenta codventa);

	DataTable CargaDetalleCodventaxLineaFamilia(clsFacturaVenta codventa);

	DataTable MuestraCobros(int Estado, int codAlmacen, DateTime Fecha1, DateTime Fecha2, int codTipo, int codSucursal, int codFormaPago);

	string MuestraFechaPrimerCobro(int Estado, int codAlmacen, int codTipo, int codSucursal, int codFormaPago);

	DataTable DocumentosPorCliente(int CodCliente);

	DataTable Ventas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin, int codsucursal, int verifica, int codProducto);

	DataTable Ventasboletas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin, int codsucursal, int codtipdoc);

	DataTable VentasCodlineaCodfamilia(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin, int codsucursal);

	DataTable MuestraGuiaVenta(int CodAlmacen, int CodCliente);

	DataTable MuestraDetalleGuiaVenta(int CodAlmacen, int codNota);

	DataTable MuestraDetalleGuiaVenta2(int CodAlmacen);

	DataTable MuestraDetalleGuia(int CodAlmacen, int codNota);

	DataTable MuestraDetalleVentaGuia(int codventa, int codalmacen);

	bool VistaSucursal(int codventa, int valor);

	DataTable CargaDetalleVentaCredito(int codventa, int codAlmacen);

	bool ActualizaPendienteCredito(decimal monto, int codnota, int codalm, int tipo);

	DataTable ReporteAnalisisDetalladoVenta(string buscar, DateTime desde, DateTime hasta, string codAnalisis, string codAlmacen);

	DataTable ListaNotasDebito(int CodAlmacen, DateTime fecha1, DateTime fecha2);

	int chekeaImpresion(int codVenta);

	bool actualizaFactura_venta(int CodSerie, string txtSeries, string txtNumeros, string CodVenta);

	DataTable ListaFacturas_ventaCliente(int codcli);

	bool updatensconsultext(clsFacturaVenta factura_venta);

	DataTable VentasDiarias(int CodVendedor, int CodAlmacen, DateTime FechaInicio);

	DataTable VentasPendientesdedespacho(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin);

	DataTable CargaDetallexEntregar(int codventa, int codAlmacen);

	int GetCantidadPendiente(int lista);

	bool CambiaEstadoDetalle(int codigo);

	bool CambiaEstadoFactura(int CodVenta);

	DataTable despachosxventa(int Codfactura);

	bool insertventaentregar(clsFacturaVenta factura_venta);

	bool insertdetalleventaentregar(clsDetalleFacturaVenta detalle_venta);

	bool VentaPendiente(int CodVenta);

	bool actualizaEstadoEnvio(int codigo, int codVenta);

	bool actualizaEstadoEnvioConError(int codigo, int codVenta);

	bool ActualizaBoletaSunat(int codventa);

	bool ValidaAnulacionVenta(int codigoventa);

	DataTable ReporteVentasResumido(DateTime desde, DateTime hasta);

	bool VerificaEstadoEnvioDocumentoElectronico(int codigoEmpresa, int codigoSucursal, int codigoAlmacen, int codigoFacturaVenta);

	double getTotalNotaCreditoSegunFechayAlmacen(int codAlmacen, DateTime desde, DateTime hasta, int codSucursal);

	bool ActualizaStockSinFacturar(clsDetalleFacturaVenta det, int unibas, decimal factor, int engresoegreso);

	bool InsertaProductoSinFacturar(clsDetalleFacturaVenta det, decimal vps, decimal stock, decimal soles, int unibas);

	DataTable VentaSinRepositorio(int alma, DateTime fechaini, DateTime fechafin, int tipdoc);

	DataTable CargaDetalle_Regeneracion(int codventa, int codAlmacen);

	DataTable ResumenDiario(DateTime dia, int codSucursal, int codAlmacen);

	int getCantidadResumen(DateTime dia, int codSucursal, int codAlmacen);

	clsFacturaVenta CargaFacturaVentaSegunOV(int codPedido);

	bool actualizaCanalVenta(string codigo, int codVenta);

	bool actualizacuentabanco(int codbanco, int codcuenta, string numcuenta, int codventa);
}
