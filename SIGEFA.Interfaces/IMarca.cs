using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IMarca
{
	bool Insert(clsMarca NuevaMarca);

	bool Update(clsMarca Marca);

	bool Delete(int Codigo);

	clsMarca CargaMarca(int Codigo);

	DataTable ListaMarcas();

	DataTable listaproductosmarca(int codmarca, int almacen, int familia, int proveedor);

	DataTable listaproductoslineamarca(int linea, int almacen, DateTime fechainicio, DateTime fechafin, int familia, int sucursal);
}
