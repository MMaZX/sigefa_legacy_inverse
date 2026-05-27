using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITransaccion
{
	bool Insert(clsTransaccion NuevaTransaccion);

	bool Update(clsTransaccion Transaccion);

	bool Delete(int Codigo);

	clsTransaccion CargaTransaccion(int Codigo);

	clsTransaccion CargaTransaccionS(string Sigla, int Tipo);

	DataTable ListaTransacciones(int Caso);

	bool InsertConfiguracion(int CodTransaccion, int CodDetalle, int CodUser);

	bool LimpiarConfiguracion(int CodTransaccion);

	List<int> MuestraConfiguracion(int CodTransaccion);
}
