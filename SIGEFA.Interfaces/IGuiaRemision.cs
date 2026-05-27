using System;
using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IGuiaRemision
{
	bool insert(clsGuiaRemision GuiaRemision);

	bool update(clsGuiaRemision GuiaRemision);

	bool delete(int CodigoGuiaRemision);

	bool insertdetalle(clsDetalleGuiaRemision Detalle);

	bool updatedetalle(clsDetalleGuiaRemision Detalle);

	bool deletedetalle(int CodigoDetalle);

	bool insertrelacionguia(int codguia, int codventa, int codalmacen, int codusuario, int codTrans);

	clsGuiaRemision CargaGuiaRemision(int CodGuiaRemision);

	clsGuiaRemision CargaGuiaTransferencia(int cod);

	clsGuiaRemision CargaGuiaVenta(int CodVenta);

	clsGuiaRemision BuscaGuiaRemision(string CodGuiaRemision, int CodAlmacen);

	List<clsDetalleGuiaRemision> listaDetalleGuiaRemision(string CodGuiaRemision);

	List<clsDetalleGuiaRemision> listaDetalleGuiaRemisionventa(string CodGuiaRemision);

	DataTable CargaDetalle(int CodGuiaRemision);

	DataTable CargaDetalleGuiaVenta(int CodGuiaRemision);

	DataTable ListaGuiaRemisiones(int CodAlmacen);

	DataTable MuestraGuias(DateTime fecha1, DateTime fecha2);

	DataTable MuestraGuiasBusqueda(DateTime fecha1, DateTime fecha2, string numdocumento);

	DataTable CargaFacturasGuia(int codGuia, int codAlmacen);
}
