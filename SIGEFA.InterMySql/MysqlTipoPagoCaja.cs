using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlTipoPagoCaja : ITipoPagoCaja
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsTipoPagoCaja TPcaja)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTipoPagoCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("descrip", TPcaja.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", TPcaja.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			TPcaja.CodTipoPagoServicioNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsTipoPagoCaja TPcaja)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaTipoPagoCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codigo", TPcaja.CodTipoPagoServicio);
			cmd.Parameters.AddWithValue("descripcion", TPcaja.Descripcion);
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

	public bool Delete(int Codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarTipoPagoCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codigo", Codigo);
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

	public clsTipoPagoCaja CargaTipoPagoCaja(int Codigo)
	{
		clsTipoPagoCaja TPCaja = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaTipoPagoCaja", con.conector);
			cmd.Parameters.AddWithValue("codigo", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					TPCaja = new clsTipoPagoCaja();
					TPCaja.CodTipoPagoServicio = Convert.ToInt32(dr.GetDecimal(0));
					TPCaja.Descripcion = dr.GetString(1);
					TPCaja.Estado = dr.GetBoolean(2);
					TPCaja.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					TPCaja.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return TPCaja;
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

	public DataTable ListaTipoPagoCaja()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTipoPagoCaja", con.conector);
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
