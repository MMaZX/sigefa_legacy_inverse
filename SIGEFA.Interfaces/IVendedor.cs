using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IVendedor
{
	bool Insert(clsVendedor VendedorNuevo);

	bool Update(clsVendedor Vendedor);

	bool Delete(int Codigo);

	clsVendedor CargaVendedor(int Codigo);

	DataTable ListaVendedores();

	DataTable ListaVendedoresDestaque();

	DataTable BuscaVendedores(int Criterio, string Filtro);

	DataTable ListaComisiones(DateTime FechaInicial, DateTime FechaFinal);

	DataTable MuestraComisonesFiltros(int Codigo, DateTime FechaInicial, DateTime FechaFinal);

	DataTable CargaVendedores();

	DataTable ListarComisionesPorDocumentoFecha();

	DataTable MuestraComisionPorDocumentoFecha(int Mes, int Año);

	DataTable MuestraComisionPorDocumentoPorVendedor(int Mes, int Año, int CodVendedor);

	DataTable MuestraComisionPorDocumentoPorVendedorZona(int Mes, int Año, int CodVendedor, int CodZona);

	DataTable CargaVendedoresReportes();

	DataTable ListaVendedoresDestaque2();
}
