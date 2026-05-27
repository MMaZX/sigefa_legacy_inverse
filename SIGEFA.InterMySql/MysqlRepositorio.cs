using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlRepositorio : IRepositorio
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private MySqlTransaction mysqltransaccion;

	private List<clsRepositorio> lista = null;

	private clsRepositorio clsrepo = null;

	private string consulta = "";

	public bool registra_repositorio(clsRepositorio repo)
	{
		try
		{
			con.conectarBD();
			mysqltransaccion = con.conector.BeginTransaction();
			consulta = "registrar_repositorio";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_tipdoc", repo.Tipodoc);
			oParam = cmd.Parameters.AddWithValue("_fechaemision", repo.Fechaemision.ToString("yyyy/MM/dd"));
			oParam = cmd.Parameters.AddWithValue("_serie", repo.Serie);
			oParam = cmd.Parameters.AddWithValue("_correlativo", repo.Correlativo);
			oParam = cmd.Parameters.AddWithValue("_monto", repo.Monto);
			oParam = cmd.Parameters.AddWithValue("_estadosunat", repo.Estadosunat);
			oParam = cmd.Parameters.AddWithValue("_mensajesunat", repo.Mensajesunat);
			oParam = cmd.Parameters.AddWithValue("_nombredoc", repo.Nombredoc);
			oParam = cmd.Parameters.AddWithValue("_usuario", repo.Usuario);
			oParam = cmd.Parameters.AddWithValue("_codEmpresa", repo.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("_codSucursal", repo.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", repo.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("_codFacturaVenta", repo.CodFacturaVenta);
			oParam = cmd.Parameters.AddWithValue("TipDocRelacion_ex", repo.TipDocRelacion);
			oParam = cmd.Parameters.AddWithValue("_codigoHash", repo.CodigoHash);
			oParam = cmd.Parameters.AddWithValue("_resultado", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			x = Convert.ToInt32(cmd.Parameters["_resultado"].Value);
			mysqltransaccion.Commit();
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			if (mysqltransaccion != null)
			{
				mysqltransaccion.Rollback();
			}
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsRepositorio> buscar_repositorio(clsRepositorio repo)
	{
		try
		{
			con.conectarBD();
			consulta = "buscar_repositorio";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_tipdoc", repo.Tipodoc);
			oParam = cmd.Parameters.AddWithValue("_serie", repo.Serie);
			oParam = cmd.Parameters.AddWithValue("_correlativo", repo.Correlativo);
			oParam = cmd.Parameters.AddWithValue("_fechaemision", repo.Fechaemision.ToString("yyyy/MM/dd"));
			oParam = cmd.Parameters.AddWithValue("_monto", repo.Monto);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				lista = new List<clsRepositorio>();
				while (dr.Read())
				{
					clsrepo = new clsRepositorio();
					clsrepo.Repoid = (int)dr["repositorioid"];
					clsrepo.Tipodoc = (int)dr["tipdoc"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fechaemision"].ToString()).Date;
					clsrepo.Serie = (string)dr["serie"];
					clsrepo.Correlativo = (string)dr["correlativo"];
					clsrepo.Monto = (decimal)dr["monto"];
					clsrepo.Estadosunat = (string)dr["estadosunat"];
					clsrepo.Mensajesunat = (string)dr["mensajesunat"];
					clsrepo.Pdf = (byte[])dr["docpdf"];
					clsrepo.Xml = (byte[])dr["docxml"];
					clsrepo.Nombredoc = (string)dr["nombredoc"];
					clsrepo.Usuario = (int)dr["usuario"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fecharegistro"].ToString());
					lista.Add(clsrepo);
				}
			}
			return lista;
		}
		catch (MySqlException)
		{
			return lista;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsRepositorio> listar_repositorio(string estado, int codsucu, int codalma, DateTime desde, DateTime hasta)
	{
		try
		{
			lista = null;
			con.conectarBD();
			consulta = "listar_repositorio";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 2500000;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_estadosunat", estado);
			oParam = cmd.Parameters.AddWithValue("_codSucursal", codsucu);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", codalma);
			oParam = cmd.Parameters.AddWithValue("_desde", desde);
			oParam = cmd.Parameters.AddWithValue("_hasta", hasta);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				lista = new List<clsRepositorio>();
				while (dr.Read())
				{
					clsrepo = new clsRepositorio();
					clsrepo.Repoid = (int)dr["repositorioid"];
					clsrepo.Tipodoc = (int)dr["tipdoc"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fechaemision"].ToString());
					clsrepo.Serie = (string)dr["serie"];
					clsrepo.Correlativo = (string)dr["correlativo"];
					clsrepo.Monto = (decimal)dr["monto"];
					clsrepo.Estadosunat = (string)dr["estadosunat"];
					clsrepo.Mensajesunat = (string)dr["mensajesunat"];
					clsrepo.Nombredoc = (string)dr["nombredoc"];
					clsrepo.Usuario = (int)dr["usuario"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fecharegistro"].ToString());
					clsrepo.CodEmpresa = (int)dr["codempresa"];
					clsrepo.FechaActualiza = dr["env"].ToString();
					clsrepo.CodFacturaVenta = Convert.ToInt32(dr["codFacturaVenta"]);
					lista.Add(clsrepo);
				}
			}
			return lista;
		}
		catch (MySqlException)
		{
			return lista;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsRepositorio> listar_documentos_pendientes(string estado, int codsucu, DateTime desde, DateTime hasta)
	{
		try
		{
			lista = null;
			con.conectarBD();
			consulta = "listar_documentos_pendientes";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 2500000;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_estadosunat", estado);
			oParam = cmd.Parameters.AddWithValue("_codSucursal", codsucu);
			oParam = cmd.Parameters.AddWithValue("_desde", desde);
			oParam = cmd.Parameters.AddWithValue("_hasta", hasta);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				lista = new List<clsRepositorio>();
				while (dr.Read())
				{
					clsrepo = new clsRepositorio();
					clsrepo.Repoid = (int)dr["repositorioid"];
					clsrepo.Tipodoc = (int)dr["tipdoc"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fechaemision"].ToString());
					clsrepo.Serie = (string)dr["serie"];
					clsrepo.Correlativo = (string)dr["correlativo"];
					clsrepo.Monto = (decimal)dr["monto"];
					clsrepo.Estadosunat = (string)dr["estadosunat"];
					clsrepo.Mensajesunat = (string)dr["mensajesunat"];
					clsrepo.documento = (string)dr["documento"];
					clsrepo.Nombredoc = (string)dr["nombredoc"];
					clsrepo.Usuario = (int)dr["usuario"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fecharegistro"].ToString());
					clsrepo.CodEmpresa = (int)dr["codempresa"];
					clsrepo.FechaActualiza = dr["env"].ToString();
					clsrepo.CodFacturaVenta = Convert.ToInt32(dr["codFacturaVenta"]);
					lista.Add(clsrepo);
				}
			}
			return lista;
		}
		catch (MySqlException)
		{
			return lista;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool actualiza_repositorio(clsRepositorio repo)
	{
		try
		{
			con.conectarBD();
			mysqltransaccion = con.conector.BeginTransaction();
			consulta = "actualiza_estadosunat_repositorio";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_repositorioid", repo.Repoid);
			oParam = cmd.Parameters.AddWithValue("_estadosunat", repo.Estadosunat);
			oParam = cmd.Parameters.AddWithValue("_mensajesunat", repo.Mensajesunat);
			oParam = cmd.Parameters.AddWithValue("_cdrzip", repo.CDR);
			oParam = cmd.Parameters.AddWithValue("_ticket", repo.ticket);
			oParam = cmd.Parameters.AddWithValue("_resultado", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			x = Convert.ToInt32(cmd.Parameters["_resultado"].Value);
			mysqltransaccion.Commit();
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			if (mysqltransaccion != null)
			{
				mysqltransaccion.Rollback();
			}
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool ActualizaCorrelativoDocResp(int codtipodoc, int codalma)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCorrelativoDocResp", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codDocumento_ex", codtipodoc);
			oParam = cmd.Parameters.AddWithValue("codAlmacen_ex", codalma);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			if (mysqltransaccion != null)
			{
				mysqltransaccion.Rollback();
			}
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsRepositorio> listar_repositorio_Enviados(string estado, int codsucu, int codalma, DateTime desde, DateTime hasta)
	{
		try
		{
			lista = null;
			con.conectarBD();
			consulta = "listar_repositorio_Enviados";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_estadosunat", estado);
			oParam = cmd.Parameters.AddWithValue("_codSucursal", codsucu);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", codalma);
			oParam = cmd.Parameters.AddWithValue("_desde", desde);
			oParam = cmd.Parameters.AddWithValue("_hasta", hasta);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				lista = new List<clsRepositorio>();
				while (dr.Read())
				{
					clsrepo = new clsRepositorio();
					clsrepo.Repoid = (int)dr["repositorioid"];
					clsrepo.Tipodoc = (int)dr["tipdoc"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fechaemision"].ToString());
					clsrepo.Serie = (string)dr["serie"];
					clsrepo.Correlativo = (string)dr["correlativo"];
					clsrepo.Monto = (decimal)dr["monto"];
					clsrepo.Estadosunat = (string)dr["estadosunat"];
					clsrepo.Mensajesunat = (string)dr["mensajesunat"];
					clsrepo.Nombredoc = (string)dr["nombredoc"];
					clsrepo.Usuario = (int)dr["usuario"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fecharegistro"].ToString());
					clsrepo.FechaActualiza = dr["env"].ToString();
					lista.Add(clsrepo);
				}
			}
			return lista;
		}
		catch (MySqlException)
		{
			return lista;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool Registra_Resumen(clsRepositorio repo)
	{
		try
		{
			con.conectarBD();
			mysqltransaccion = con.conector.BeginTransaction();
			consulta = "Registra_Resumen";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("Nombre_ex", repo.Nombredoc);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			repo.id_resumen = Convert.ToInt32(cmd.Parameters["newid"].Value);
			mysqltransaccion.Commit();
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			if (mysqltransaccion != null)
			{
				mysqltransaccion.Rollback();
			}
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool Registra_Det_Resumen(int id_resumen, int codFacturaVenta)
	{
		try
		{
			con.conectarBD();
			mysqltransaccion = con.conector.BeginTransaction();
			consulta = "Registra_Det_Resumen";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("Id_resumen_ex", id_resumen);
			oParam = cmd.Parameters.AddWithValue("codFactura_venta_ex", codFacturaVenta);
			int x = cmd.ExecuteNonQuery();
			mysqltransaccion.Commit();
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			if (mysqltransaccion != null)
			{
				mysqltransaccion.Rollback();
			}
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsRepositorio> listarDocumentoPendientesResumen(DateTime fechaInicio, DateTime fechaFin, int codigoSucursal, int codigoAlmacen)
	{
		try
		{
			lista = null;
			con.conectarBD();
			consulta = "ListarDocumentosPendientesResumen";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("fecha_inicio", fechaInicio);
			oParam = cmd.Parameters.AddWithValue("fecha_fin", fechaFin);
			oParam = cmd.Parameters.AddWithValue("codigo_sucursal", codigoSucursal);
			oParam = cmd.Parameters.AddWithValue("codigo_almacen", codigoAlmacen);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				lista = new List<clsRepositorio>();
				while (dr.Read())
				{
					clsrepo = new clsRepositorio();
					clsrepo.Repoid = (int)dr["repositorioid"];
					clsrepo.Tipodoc = (int)dr["tipdoc"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fechaemision"].ToString());
					clsrepo.Serie = Convert.ToString(dr["serie"]);
					clsrepo.Correlativo = (string)dr["correlativo"];
					clsrepo.Monto = (decimal)dr["monto"];
					clsrepo.Estadosunat = (string)dr["estadosunat"];
					clsrepo.Mensajesunat = (string)dr["mensajesunat"];
					clsrepo.Nombredoc = (string)dr["nombredoc"];
					clsrepo.Usuario = (int)dr["usuario"];
					clsrepo.ticket = Convert.ToString(dr["ticket"]);
					lista.Add(clsrepo);
				}
			}
			return lista;
		}
		catch (MySqlException)
		{
			return lista;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsRepositorio> listarDocumentoEnviadosResumen(DateTime fechaInicio, DateTime fechaFin, int codigoSucursal, int codigoAlmacen)
	{
		try
		{
			lista = null;
			con.conectarBD();
			consulta = "ListarDocumentosEnviadosResumen";
			cmd = new MySqlCommand(consulta, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("fecha_inicio", fechaInicio);
			oParam = cmd.Parameters.AddWithValue("fecha_fin", fechaFin);
			oParam = cmd.Parameters.AddWithValue("codigo_sucursal", codigoSucursal);
			oParam = cmd.Parameters.AddWithValue("codigo_almacen", codigoAlmacen);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				lista = new List<clsRepositorio>();
				while (dr.Read())
				{
					clsrepo = new clsRepositorio();
					clsrepo.Repoid = (int)dr["repositorioid"];
					clsrepo.Tipodoc = (int)dr["tipdoc"];
					clsrepo.Fechaemision = DateTime.Parse(dr["fechaemision"].ToString());
					clsrepo.Serie = Convert.ToString(dr["serie"]);
					clsrepo.Correlativo = (string)dr["correlativo"];
					clsrepo.Monto = (decimal)dr["monto"];
					clsrepo.Estadosunat = (string)dr["estadosunat"];
					clsrepo.Mensajesunat = (string)dr["mensajesunat"];
					clsrepo.Nombredoc = (string)dr["nombredoc"];
					clsrepo.Usuario = (int)dr["usuario"];
					clsrepo.ticket = Convert.ToString(dr["ticket"]);
					lista.Add(clsrepo);
				}
			}
			return lista;
		}
		catch (MySqlException)
		{
			return lista;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}
}
