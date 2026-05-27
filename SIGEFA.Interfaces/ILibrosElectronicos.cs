using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ILibrosElectronicos
{
	bool Insert(clsLibrosElectronicos libroElect);

	bool Update(clsLibrosElectronicos libroElect);

	bool Delete(int Codle);

	clsLibrosElectronicos MuestraLE(int Codigo);

	DataTable CargaLibrosElectronicos();

	DataTable CargaRegistrosElectronicos(int Codle);

	clsRegistroElectronico MuestraRE(int Codigo);

	DataTable CargaOperaciones();

	DataTable CargaContenido();

	DataTable CargaGeneradoPor();

	DataTable GetVentas_Mes_LEV(int mes);

	DataTable GetVentas_Mes_LEV2(int mes, string periodo);

	DataTable FacturasComprasLE(int mes, int codalma, string cadena, string periodo);

	int ValidaCampoTipoFacturacion(int mes, int Anio);
}
