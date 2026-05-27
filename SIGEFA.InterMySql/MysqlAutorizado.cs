using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlAutorizado : IAutorizado
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsAutorizado aut)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaAutorizado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("nombre", aut.Nombre);
			oParam = cmd.Parameters.AddWithValue("codusu", aut.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			aut.CodAutorizadoNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsAutorizado aut)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaAutorizado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codaut", aut.CodAutorizado);
			cmd.Parameters.AddWithValue("nombre", aut.Nombre);
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

	public bool Delete(int CodAutorizado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarAutorizado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codaut", CodAutorizado);
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

	public clsAutorizado CargaAutorizado(int Codigo)
	{
		clsAutorizado aut = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraAutorizado", con.conector);
			cmd.Parameters.AddWithValue("codaut", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					aut = new clsAutorizado();
					aut.CodAutorizado = Convert.ToInt32(dr.GetDecimal(0));
					aut.Nombre = dr.GetString(1);
					aut.Estado = dr.GetBoolean(2);
					aut.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					aut.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return aut;
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

	public DataTable ListaAutorizados()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAutorizados", con.conector);
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
