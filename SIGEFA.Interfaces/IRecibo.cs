using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IRecibo
{
	bool Insert(clsRecibos NuevaCajaChica);

	bool Update(clsRecibos CajaChica);

	DataTable ListaRecibos(int codSucursal, DateTime fecha1, DateTime fecha2, int tipo);

	DataTable ListaRecibosEgreso(int codSucursal, int tipo);

	int Correlativo(int codtipo);
}
