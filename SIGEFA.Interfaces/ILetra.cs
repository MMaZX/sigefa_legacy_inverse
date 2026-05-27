using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ILetra
{
	bool Insert(clsLetra NuevoLetra);

	bool update(clsLetra Letra);

	bool delete(int CodigoLetra);

	clsLetra CargaLetra(int CodLetra);

	DataTable MuestraListaLetrasNota(int CodNotaIngreso);

	bool AnularLetra(int CodigoLetra);

	int GetCodigoFactura(int codigonota);
}
