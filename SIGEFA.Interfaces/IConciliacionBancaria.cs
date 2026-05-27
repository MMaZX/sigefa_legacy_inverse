using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IConciliacionBancaria
{
	bool Insert(clsConciliacionBancaria conciliacion);

	bool insertdetalle(clsDetalleConciliacion detalle);

	bool Update(int codalma, int codbanco, int codcuenta, int CodConciliacion);

	bool UpdateBandera(int codalma, int codbanco, int codcuenta, int CodConciliacion);
}
