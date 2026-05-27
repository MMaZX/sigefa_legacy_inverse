using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITransferencia
{
	bool insert(clsTransferencia transf);

	bool insert2(clsTransferencia transf);

	bool update(clsTransferencia transf);

	bool delete(int codtrans);

	clsTransferencia CargaTransferencia(int codtrans);

	clsTransferencia CargaTransferenciaCodPedido(int codpedido);

	clsTransferencia CargaDetalleTransferencia(int codtrans);

	clsTransferencia CargaTransferenciaCod();

	clsTransferencia BuscaTransferencia(string codtrans, int codAlmacenOrigen);

	DataTable ListaTranferencias(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta);

	DataTable ListaTranferencias2(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta);

	DataTable ListaTranferenciasDesp(int codtrans);

	DataTable ListaTranferenciasEntrega(DateTime fechaE, int codigotra);

	DataTable ListaTransferenciasEnviados(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta);

	DataTable ListaTranferenciasPendientes(int codAlmacen, int codPedido);

	DataTable listatodastranferencias(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta);

	DataTable ListaTranferenciasxProducto(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta, int codprod);

	bool insertdetalle(clsDetalleTransferencia detalle);

	bool insertdetalle2(clsDetalleTransferencia detalle);

	bool updatedetalle(clsDetalleTransferencia detalle);

	bool updatedetalle2(clsDetalleTransferencia detalle);

	bool deletedetalle(int coddeta);

	DataTable CargaDetalle(int codTransDir);

	DataTable CargaDetallePedido(int codTransDir);

	DataTable CargaDetalleTrans(clsTransferencia tra);

	bool rechazad2(int codTransDirecta, string anulado);

	bool rechazado(int codTransDirecta, string desc);

	bool devuelveproductos(clsDetalleTransferencia det);

	bool Aprobar(int codTransDirecta);

	bool TransFactura(int codPedido);

	bool Entregar(int codTransDirecta);

	DataTable CargaDetalleGuiaT(string CodigoTransferencia);

	bool atendido(int CodTransDirecta, int codUsuario);
}
