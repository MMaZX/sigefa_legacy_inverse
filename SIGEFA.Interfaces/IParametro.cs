using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IParametro
{
	bool ConsultarParametroVenta(int codigo);

	bool ActualizarParametroVenta(string valorParametro);

	bool actualizaDocumentoVenta(string valorParametro);

	clsParametro ObtenerParametro(int codigo);

	bool Insert(clsParametro ingreso);

	bool Update(clsParametro ingreso);

	DataTable ListadoParametros();
}
