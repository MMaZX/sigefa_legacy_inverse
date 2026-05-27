using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ISerie
{
	bool Insert(clsSerie NuevoSerie);

	bool Update(clsSerie Serie);

	bool Delete(int Codigo);

	clsSerie CargaSerie(int Codigo, int CodAlmacen);

	DataTable ListaSeries(int codDocumento, int codAlmacen);

	clsSerie BuscaSerie(string Serie, int Documento, int Almacen);

	int ExistenSeries(int CodDocumento, int CodAlmacen);

	int GetCodigoSerie(int CodDocumento, int CodAlmacen);

	clsSerie BuscaSeriexDocumento(int codDocumento, int CodAlmacen);

	int traeNumeracion(int codal, int coddoc);

	int traeCodSerie(int codal, int coddoc);

	clsSerie CargaSerieEmpresa(int CodigoAlmacen, int codigoDoc);

	clsSerie CargaSerieOV(int Sucursal, int CodAlmacen);

	clsSerie CargaSeriePorDocumentoAsociado(int codDocumento, int codAlmacen, int codDocumentoAsociado);
}
