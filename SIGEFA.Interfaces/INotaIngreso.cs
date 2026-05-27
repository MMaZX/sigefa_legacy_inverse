using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface INotaIngreso
{
	bool insert(clsNotaIngreso Nota);

	bool insertingresoguia(clsNotaIngreso Nota);

	bool insertarNota(clsNotaIngreso nota);

	bool insertarNotayFactura(clsNotaIngreso nota, clsFactura factura);

	bool update(clsNotaIngreso Nota);

	bool ActualizaCantidadPendiente(double cantidad, int produc, int CodOrden, int coddeta);

	bool ActualizaCantidadPendiente2(double cantidad, int produc, int alma, int coduser);

	bool ActualizaCodNotaIngreso(double cantidad, int produc, int CodDetalle, int tipo);

	bool delete(int CodigoNota);

	bool anular(int CodSerie, string NumDoc, string text);

	bool anular1(int codigo);

	bool activar(int CodigoNota);

	bool atender(int codigo, int CodSerie, string NumDoc, int User);

	bool insertdetalle(clsDetalleNotaIngreso Detalle);

	bool insertdetalleConsolidado(int orden, int nota, int codAlma, int codUsu);

	bool updatedetalle(clsDetalleNotaIngreso Detalle);

	bool deletedetalle(int CodigoDetalle);

	bool deleteConsolidado(int codalma, int codusu);

	clsNotaIngreso CargaNotaIngreso(int CodNota);

	clsNotaSalida buscaNotaIngreso(int CodNota);

	DataTable CargaDetalle(int CodNota);

	DataTable CargaDetalleSinEstado(int codNotaIngreso);

	DataTable ListaNotasIngreso(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int tipoFecha);

	DataTable reporteinventario(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);

	DataTable busquedapornumero(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, string numero);

	DataTable ListaNotasIngresoxProducto(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int codprod, int tipoFecha);

	DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2);

	DataTable MuestraPagos(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2);

	DataTable MuestraOrdenAlmacen(int codAlmacen, int codUsu);

	DataTable MuestraNotaIngresoOrden(int codAlmacen, DateTime FechaInicial, DateTime FechaFinal);

	DataTable MuestraTransferenciasVigentes(int CodAlmacen);

	DataTable CargaDetalleTransferencia(int codigotransferencia);

	bool UpdateComentario(clsNotaIngreso Nota);

	DataTable MuestraGuia(int codAlmacen, int codUsu);

	DataTable CargaNotaCreditoSinAplicar(int Codcli, int VentComp, int codAlmacen, string fecha);

	bool ActualizaNCreditoVentaSinAplicar(clsNotaIngreso nota);

	bool VerificarNCVentaAplicada(clsNotaIngreso nota);

	DataTable CargaNotaIngresoSD(int Codprov, int CodAlmacen, DateTime fecha1, DateTime fecha2);

	DataTable ListarCodigoNotasSalida();

	bool ActualizaStockPA(int codalmacenorig, int codalmacenrecep, int codigoProd, int codigoNI, decimal cantidadenviada, decimal preciounit, decimal valorreal, decimal valorrealsoles, int codigouser, string serie, string numerodoc, int codserie);

	bool ActualizaStockAR(int codalmacenorig, int codalmacenrecep, int codigoProd, int codigoNI, decimal cantidadenviada, decimal preciounit, decimal valorreal, decimal valorrealsoles, int codigouser, string serie, string numerodoc, int codserie);

	clsNotaIngreso CargaNI(int codTransDirecta);

	bool ValidarCompraNotaIngreso(int codigoTipoDocumento, string serieDocumento, string numeroDocumento, int codigoProveedor);

	int obtenerCodNCsegun(string codNotaIngreso);
}
