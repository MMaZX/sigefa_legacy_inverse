using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface INotaSalida
{
	bool insert(clsNotaSalida Nota);

	bool update(clsNotaSalida Nota);

	bool delete(int CodigoNota);

	bool anular(int CodigoNota);

	bool activar(int CodigoNota);

	bool ActualizaCantidadPendienteCotizacion(double cantidad, int produc, int CodCoti);

	bool ActualizaCantidadPendienteVenta(double cantidad, int produc, int CodVen);

	bool ActualizaCotizacionAprobada(int codCotizacion);

	bool ActualizaCotizacionVigente(int codCotizacion, int codDetalleCotizacion);

	bool insertdetalle(clsDetalleNotaSalida Detalle);

	bool updatedetalle(clsDetalleNotaSalida Detalle);

	bool deletedetalle(int CodigoDetalle);

	bool insertdetalle2(clsDetalleNotaSalida Detalle);

	bool insertadetalleventaSalida(int codVenta, int codCoti, int codSalida);

	bool deletedetalleventaSalida();

	clsNotaSalida CargaNotaSalida(int CodNota);

	DataTable CargaDetalle(int CodNota);

	DataTable CargaDetalleNotaCredito(int CodNota);

	DataTable ListaNotasSalida(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);

	DataTable MuestraCobros(int Estado, int codAlmacen, DateTime Fecha1, DateTime Fecha2, int codTipo);

	DataTable DocumentosSinGuia(int CodAlmacen, int CodCliente, int Tipo);

	DataTable DocumentosPorCliente(int CodCliente, int tipo);

	DataTable Ventas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin);

	DataTable MuestraFacturasVentaCliente(int CodAlmacen, int CodSucursal, int CodEmpresa);

	DataTable MuestraVentaSalida(int CodAlmacen, int codVenta);

	DataTable MuestraCotizacionSalida(int CodAlmacen, int codCotizacion);

	DataTable MuestraTipoDocumentoNota(int CodAlmacen, int codCliente);

	clsNotaSalida CargaNotaSalidaCredito(int CodNota);

	DataTable ListaNotasCreditoCompra(int CodAlmacen, DateTime fecha1, DateTime fecha2);

	DataTable MuestraNotaAlmacen(int codAlmacen, int tipo);

	DataTable CargaDetalleNotaSalida(int codNota, int proceso);

	clsNotaSalida CargaNotaSalidaCreditoVentas(int CodNota);

	clsNotaSalida CargaNotaSalidaDebitoVentas(int CodNota);

	bool VerificarNCCompraAplicada(clsNotaSalida nota);

	DataTable ListarNotaSalidaParaNDCompra(int codAlm, int codProv);

	DataTable CargaDetalleNotaSalidaNDC(int codNota, int codAlm);

	bool ActualizaNCreditoCompraSinAplicar(clsNotaSalida nota);

	bool ActualizaSalidaDevolucion(int codnota, int codfact);

	decimal VerificarStock(int codpro, int codalma, int opc);

	clsNotaSalida CargaNS(int codTransDirecta);
}
