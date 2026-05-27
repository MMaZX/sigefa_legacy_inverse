using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ILinea
{
	bool Insert(clsLinea NuevaLinea);

	bool Update(clsLinea Linea);

	bool Delete(int Codigo);

	clsLinea CargaLinea(int Codigo);

	DataTable ListaLineas(int codFamilia);
}
