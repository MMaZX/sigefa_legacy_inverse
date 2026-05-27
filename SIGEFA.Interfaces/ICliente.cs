using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ICliente
{
	bool Insert(clsCliente ClienteNuevo);

	bool Update(clsCliente Cliente);

	bool UpdateCategoria(int codcliente, int codcategoria);

	bool Delete(int CodigoCli);

	bool CambioHabilitado(int CodCliente, bool Estado);

	clsCliente CargaCliente(int CodigoCli);

	DataTable CargaClientes();

	DataTable ListaClientes();

	clsCliente MuestraClienteNota(int CodigoCli);

	clsCliente BuscaCliente(string Codigo, int Tipo);

	DataTable BuscaClientes(int Criterio, string Filtro);

	DataTable RelacionClientes();

	DataTable RelacionClientesFiltrada(string filtrocli);

	string CodigoPersonalizado();

	clsCliente CargaDeuda(clsCliente Cliente);

	clsCliente CargaFacturasVencidas(clsCliente cliente);

	DataTable ListaClientesConsultor(int CodEntConsExt);

	clsCliente ConsultaCliente(string DNIRUC);

	bool InsertConCli(int codEntConsExt, int CodCliente);

	bool DeleteConCli(int codEntConsExt, int CodCliente);

	int GetUltimoId();
}
