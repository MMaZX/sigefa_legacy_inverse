using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ISucursal
{
	bool Insert(clsSucursal SucursalNueva);

	bool Update(clsSucursal Sucursal);

	bool Delete(int CodigoSuc);

	clsSucursal CargaSucursal(int CodigoSuc);

	bool VerificaRUC(string RUC);

	DataTable CargaSucursales(int Codigo);

	DataTable CargaSucursalesXusuario(int Codigo, int CodUsuario);

	DataTable CargaSucursalesSeleccion(int Codigo);

	DataTable ListaSucursales();

	DataTable BuscaSucursales(int Criterio, string Filtro);

	bool UpdateConfiguracion(clsParametros Configuracion);

	clsParametros CargaConfiguracion();

	DataTable ListaSucursales_Empresa(int Codigo);

	int sucursalxalmacen(int almacen);
}
