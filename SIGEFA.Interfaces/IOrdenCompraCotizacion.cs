using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IOrdenCompraCotizacion
{
	bool Insert(clsOrdenCompraCotizacion ingreso);

	bool insertdetalle(clsDetalleOrdenCompraCotizacion Detalle);

	bool updatecotizacion(clsDetalleCotizacion Detalle);

	bool Update(clsOrdenCompraCotizacion ingreso);

	DataTable ListadaOrdenesCompra(int CodOrdenCompra);

	clsOrdenCompraCotizacion CargaOrdenCompraCotizacion(int CodOrdenCompraCotizacion, int CodAlmacen);

	DataTable CargaDetalleOrdenCompra(int CodOrdenCompra, int CodAlmacen);

	DataTable ListaOrdenesCompraCotizacionesxVigente(int CodAlmacen, DateTime fecha1, DateTime fecha2);
}
