using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IArqueo
{
	DataTable ListaArqueos(int opcion1, int opcion2, int mes1, int anio1, int codAlman);

	DataTable ListaDetalleArqueos(int codArq);

	bool Insert(clsArqueo NuevoArqueo);

	bool InsertDetalle(clsDetalleArqueo NuevoDetalleArqueo);

	bool InsertChekeoDetalle(clsDetalleArqueo UpDetalleArqueo, int codArq);

	bool Update(clsArqueo UpArqueo);
}
