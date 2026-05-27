using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IComposicionQuimica
{
	bool Insert(clsComposicionQuimica NuevoComQui);

	bool Delete(int codCompQuim);

	DataTable ListaComposicion(int codProducto);
}
