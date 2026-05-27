using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

public interface IClinica
{
	bool InsertPaciente(clsPaciente Paciente);

	bool UpdatePaciente(clsPaciente Paciente);

	bool DeletePaciente(int Codigo);

	clsPaciente CargaPaciente(int Codigo);

	DataTable ListaPacientes();

	bool InsertHistoriaCabecera(clsHistoria Historia);

	bool InsertHistoriaDetalle(clsDetalleHistoria Detalle);

	bool UpdateHistoriaCabecera(clsHistoria Historia);

	bool UpdateHistoriaDetalle(clsDetalleHistoria Detalle);

	bool DeleteHistoria(int Codigo);

	clsHistoria CargaHistoriaCabecera(string Numero);

	clsDetalleHistoria CargaHistoriaDetalle(int Codigo);

	DataTable ListaDetalleHistorial(int CodigoCab);
}
