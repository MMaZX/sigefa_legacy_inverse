using System.Data;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmUnidadEquivalente
{
	private IUnidadEquivalente iuniequ = new MysqlUnidadEquivalente();

	public DataTable listar_unidad_equivalente(int codalma)
	{
		return iuniequ.listar_unidad_equivalente(codalma);
	}

	public DataTable listar_unidad_equivalente_plantilla_productos()
	{
		return iuniequ.listar_unidad_equivalente_plantilla_productos();
	}
}
