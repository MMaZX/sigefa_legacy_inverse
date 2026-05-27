using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ISeparacion
{
	bool insert(clsSeparacion sepa);

	DataTable CargarVentas(int codAlmacen, DateTime desde, DateTime hasta, int estado);

	clsSeparacion BuscarSeparacion(int id, int CodAlmacen);

	bool insertdetalle(clsDetalleSeparacionVenta detalle);

	clsSeparacion BuscarSeparacionXid(int codSeparacion, int CodAlmacen);

	DataTable CargaDetalle(int codSeparacion);

	bool anular(int codSeparacion);

	double CargaSeparacion(int cod_alamcen);
}
