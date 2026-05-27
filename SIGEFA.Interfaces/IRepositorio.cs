using System;
using System.Collections.Generic;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IRepositorio
{
	bool registra_repositorio(clsRepositorio repo);

	List<clsRepositorio> buscar_repositorio(clsRepositorio repo);

	List<clsRepositorio> listar_repositorio(string estado, int codsuc, int codalma, DateTime desde, DateTime hasta);

	List<clsRepositorio> listar_documentos_pendientes(string estado, int codsuc, DateTime desde, DateTime hasta);

	bool actualiza_repositorio(clsRepositorio repo);

	bool ActualizaCorrelativoDocResp(int codtipodoc, int codalma);

	List<clsRepositorio> listar_repositorio_Enviados(string estado, int codsucu, int codalma, DateTime desde, DateTime hasta);

	List<clsRepositorio> listarDocumentoPendientesResumen(DateTime fechaInicio, DateTime fechaFin, int codigoSucursal, int codigoAlmacen);

	List<clsRepositorio> listarDocumentoEnviadosResumen(DateTime fechaInicio, DateTime fechaFin, int codigoSucursal, int codigoAlmacen);

	bool Registra_Resumen(clsRepositorio repo);

	bool Registra_Det_Resumen(int id_resumen, int codFacturaVenta);
}
