using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlRequerimiento : IRequerimiento
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsRequerimiento Requerimiento)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalma", Requerimiento.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("comentario", Requerimiento.Comentario);
			oParam = cmd.Parameters.AddWithValue("codTipoDocumento", Requerimiento.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codserie", Requerimiento.CodSerie);
			oParam = cmd.Parameters.AddWithValue("numeracion", Requerimiento.NumDoc);
			oParam = cmd.Parameters.AddWithValue("fechaorden", Requerimiento.FechaOrden);
			oParam = cmd.Parameters.AddWithValue("codUsuario", Requerimiento.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Requerimiento.CodRequerimientoNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool update(clsRequerimiento Requerimiento)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codReq", Convert.ToInt32(Requerimiento.CodRequerimiento));
			cmd.Parameters.AddWithValue("codalma", Requerimiento.CodAlmacen);
			cmd.Parameters.AddWithValue("comentario", Requerimiento.Comentario);
			cmd.Parameters.AddWithValue("codTipoDocumento", Requerimiento.CodTipoDocumento);
			cmd.Parameters.AddWithValue("codserie", Requerimiento.CodSerie);
			cmd.Parameters.AddWithValue("numeracion", Requerimiento.NumDoc);
			cmd.Parameters.AddWithValue("fechaorden", Requerimiento.FechaOrden);
			cmd.Parameters.AddWithValue("codUsuario", Requerimiento.CodUser);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool delete(int CodigoRequerimiento)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodigoRequerimiento);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool envio(int codalmadestino, int codreq)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("enviarrequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalmadestino", codalmadestino);
			cmd.Parameters.AddWithValue("cod", codreq);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool Atender(int codreq)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("atenderrequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod", codreq);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool rechazado(int CodigoRequerimiento, string comentario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("rechazarrequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodigoRequerimiento);
			cmd.Parameters.AddWithValue("comentario", comentario);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool anular(int CodigoRequerimiento)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodigoRequerimiento);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool anularDetalle(int CodigoDetalleRequerimiento)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularDetalleRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodigoDetalleRequerimiento);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool insertdetalleRequerimiento(clsDetalleRequerimiento Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codProducto", Detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codRequerim", Detalle.CodRequerimiento);
			oParam = cmd.Parameters.AddWithValue("unidad", Detalle.Unidad);
			oParam = cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("codUser", Detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Detalle.CodDetalleRequerimiento = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool updatedetalleRequerimiento(clsDetalleRequerimiento Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codDetalleRequerim", Detalle.CodDetalleRequerimiento);
			cmd.Parameters.AddWithValue("codProducto", Detalle.CodProducto);
			cmd.Parameters.AddWithValue("codRequer", Detalle.CodRequerimiento);
			cmd.Parameters.AddWithValue("unidad", Detalle.Unidad);
			cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
			cmd.Parameters.AddWithValue("codUser", Detalle.CodUser);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool deletedetalle(int CodigoDetalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", CodigoDetalle);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public clsRequerimiento CargaRequerimiento(int CodOrden)
	{
		clsRequerimiento orden = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaRequerimiento", con.conector);
			cmd.Parameters.AddWithValue("codOrd", CodOrden);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					orden = new clsRequerimiento();
					orden.CodRequerimiento = dr.GetInt32(0);
					orden.CodAlmacen = dr.GetInt32(2);
					orden.CodTipoDocumento = dr.GetInt32(4);
					orden.SiglaDocumento = dr.GetString(5);
					orden.DescripcionDocumento = dr.GetString(6);
					orden.CodSerie = dr.GetInt32(7);
					orden.Serie = dr.GetString(8);
					orden.NumDoc = dr.GetString(9);
					orden.FechaOrden = dr.GetDateTime(10);
					orden.Comentario = dr.GetString(11);
					orden.comentarioRechazado = dr.GetString(12);
				}
			}
			return orden;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable Cargaconsolidado(int codalma, int codprov)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaDetalleConsolidado", con.conector);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codprov", codprov);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable cargaRequerimientosTotales(int alma, int almaori)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("cargaRequerimientosTotales", con.conector);
			cmd.Parameters.AddWithValue("alma", alma);
			cmd.Parameters.AddWithValue("almaori", almaori);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable CargaDetalle(int coddetalle)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("BuscaDetalleRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", coddetalle);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable ListaRequerimiento(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("BuscaRequerimientoPorSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable ListaRequerimientoHistorial(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaRequerimientoHistorial", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable ListaRequerimientoHistorial_x_almacen(int CodAlmacen, int almades, DateTime FechaInicial, DateTime FechaFinal, int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaRequerimientoHistorial_x_almacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", almades);
			cmd.Parameters.AddWithValue("codOrddestino", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("tip", tipo);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool actualiza_det_requerimientos_vigentes(clsDetalleRequerimiento Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualiza_det_requerimientos_vigentes", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("vigente_ex", Detalle.EstadoVigente);
			cmd.Parameters.AddWithValue("coddetalle_ex", Detalle.CodDetalleRequerimiento);
			cmd.Parameters.AddWithValue("codProducto_ex", Detalle.CodProducto);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool actualiza_det_requerimientos_comentario(int coddeta, string coment)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualiza_requerimientos_comentario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddetalle_ex", coddeta);
			cmd.Parameters.AddWithValue("comentario_ex", coment);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool actualiza_requerimientos_vigentes(clsRequerimiento Requerimiento)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualiza_requerimientos_vigentes", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codrequerimiento_ex", Requerimiento.CodRequerimiento);
			cmd.Parameters.AddWithValue("vigente_ex", Requerimiento.Atendido);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable cargaRequerimientosTotales_x_requerimiento(int codrequerimiento_ex)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("cargaRequerimientosTotales_x_requerimiento", con.conector);
			cmd.Parameters.AddWithValue("codrequerimiento_ex", codrequerimiento_ex);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}
}
