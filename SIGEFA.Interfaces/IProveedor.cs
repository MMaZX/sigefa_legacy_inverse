using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IProveedor
{
	bool Insert(clsProveedor ProveedorNuevo);

	bool Update(clsProveedor Proveedor);

	bool Delete(int CodigoProv);

	clsProveedor CargaProveedor(int CodigoProv);

	clsProveedor BuscaProveedor(string RUC);

	DataTable CargaProveedores();

	DataTable ListaProveedores();

	DataTable RelacionProveedores();

	DataTable BuscaProveedores(int Criterio, string Filtro);

	DataTable ListaCorreosProveedores(int codpro);

	bool CambiaProveedor(int codProd, int codProv1, int codProv2);
}
