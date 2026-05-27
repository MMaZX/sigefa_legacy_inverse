using System;
using System.Collections.Generic;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmRepositorio
{
	private IRepositorio irepo = new MysqlRepositorio();

	public bool registra_repositorio(clsRepositorio repo)
	{
		return irepo.registra_repositorio(repo);
	}

	public List<clsRepositorio> buscar_repositorio(clsRepositorio repo)
	{
		return irepo.buscar_repositorio(repo);
	}

	public List<clsRepositorio> listar_repositorio(string estado, int codsucu, int codalma, DateTime desde, DateTime hasta)
	{
		return irepo.listar_repositorio(estado, codsucu, codalma, desde, hasta);
	}

	public List<clsRepositorio> listar_documentos_pendientes(string estado, int codsucu, DateTime desde, DateTime hasta)
	{
		return irepo.listar_documentos_pendientes(estado, codsucu, desde, hasta);
	}

	public bool actualiza_repositorio(clsRepositorio repo)
	{
		return irepo.actualiza_repositorio(repo);
	}

	public bool ActualizaCorrelativoDocResp(int codtipodoc, int codalma)
	{
		return irepo.ActualizaCorrelativoDocResp(codtipodoc, codalma);
	}

	public List<clsRepositorio> listar_repositorio_Enviados(string estado, int codsucu, int codalma, DateTime desde, DateTime hasta)
	{
		return irepo.listar_repositorio_Enviados(estado, codsucu, codalma, desde, hasta);
	}

	public bool Registra_Resumen(clsRepositorio repo)
	{
		return irepo.Registra_Resumen(repo);
	}

	public bool Registra_Det_Resumen(int id_resumen, int codFacturaVenta)
	{
		return irepo.Registra_Det_Resumen(id_resumen, codFacturaVenta);
	}

	public List<clsRepositorio> listarDocumentoPendientesResumen(DateTime fechaInicio, DateTime fechaFin, int codigoSucursal, int codigoAlmacen)
	{
		return irepo.listarDocumentoPendientesResumen(fechaInicio, fechaFin, codigoSucursal, codigoAlmacen);
	}

	public List<clsRepositorio> listarDocumentoEnviadosResumen(DateTime fechaInicio, DateTime fechaFin, int codigoSucursal, int codigoAlmacen)
	{
		return irepo.listarDocumentoEnviadosResumen(fechaInicio, fechaFin, codigoSucursal, codigoAlmacen);
	}
}
