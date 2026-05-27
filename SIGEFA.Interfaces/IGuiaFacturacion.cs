using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IGuiaFacturacion
{
	bool Insert(clsGuiaFacturacion guia);

	bool InsertDetalle(clsDetalleGuiaFacturacion detalle);

	bool UpdateDetalle(clsDetalleGuiaFacturacion detalle);

	DataTable ListaGuiasFacturacion(DateTime fecha1, DateTime fecha2, int codsucursal, bool estadosunat, bool respuestasunat);

	clsGuiaFacturacion ListaGuiaFacturacion(int codguia);

	DataTable ListaDetalleGuia(int codguia);

	bool Anular(int codguia, int codusuario);

	int VerificaGuia(int codfactura, int consulta);

	DataTable ListaModoTransporte();

	DataTable ListaMotivotransporte();

	bool UpdateGuiaFacturacion(clsGuiaFacturacion guia);
}
