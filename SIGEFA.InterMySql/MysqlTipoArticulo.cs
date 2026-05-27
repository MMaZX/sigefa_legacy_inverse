using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlTipoArticulo : ITipoArticulo
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsTipoArticulo tip)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTipoArticulo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("referencia", tip.Referencia);
			oParam = cmd.Parameters.AddWithValue("descripcion", tip.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", tip.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			tip.CodTipoArticuloNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsTipoArticulo tip)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaTipoArticulo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtipo", tip.CodTipoArticulo);
			cmd.Parameters.AddWithValue("referencia", tip.Referencia);
			cmd.Parameters.AddWithValue("descripcion", tip.Descripcion);
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

	public bool Delete(int CodTipoArticulo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarTipoArticulo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtipo", CodTipoArticulo);
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

	public clsTipoArticulo CargaTipoArticulo(int Codigo)
	{
		clsTipoArticulo tip = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTipoArticulo", con.conector);
			cmd.Parameters.AddWithValue("codtipo", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					tip = new clsTipoArticulo();
					tip.CodTipoArticulo = Convert.ToInt32(dr.GetDecimal(0));
					tip.Referencia = dr.GetString(1);
					tip.Descripcion = dr.GetString(2);
					tip.Estado = dr.GetBoolean(3);
					tip.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					tip.FechaRegistro = dr.GetDateTime(5);
				}
			}
			return tip;
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

	public DataTable ListaTipoArticulos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaArticulos", con.conector);
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
