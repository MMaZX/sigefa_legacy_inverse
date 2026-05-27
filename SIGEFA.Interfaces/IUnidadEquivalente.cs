using System.Data;

namespace SIGEFA.Interfaces;

internal interface IUnidadEquivalente
{
	DataTable listar_unidad_equivalente(int codalma);

	DataTable listar_unidad_equivalente_plantilla_productos();
}
