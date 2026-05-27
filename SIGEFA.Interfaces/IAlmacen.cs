using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IAlmacen
{
	bool Insert(clsAlmacen AlmacenNuevo);

	bool Update(clsAlmacen Almacen);

	bool Delete(int CodigoAlm);

	clsAlmacen CargaAlmacen(int CodigoAlm);

	DataTable AlmacenXUbicacion(int codalma);

	DataTable ListaAlmacenes(int CodEmpre);

	DataTable ListaAlmacenesEmp2(int Codpedido);

	DataTable ListaAlmacenesEmp(int CodAlma);

	DataTable BuscaAlmacenes(int Criterio, string Filtro);

	DataTable AlmacenesDisponible(int CodSucursal);

	DataTable ListaAlmacenes2();

	DataTable CargaAlmacen2(int codempre);

	DataTable CargaAlmacenes(int iNivel, int iEmpresa, int iUsuario);

	DataTable listaAlmacenxEmpresa();

	DataTable RelacionProductosStockMin(int codTipo, int codAlm);

	DataTable AlertaFacturasLetrasXVencer();

	DataTable almacenxNombre(int codalma);

	DataTable almacenxNombre2(int codalma);

	DataTable almacenesReporte();

	DataTable promedioVentasxAlmacen(int codProducto, int codAlmacen);

	DataTable MuestraAlmacenesConRA(int codPedido);
}
