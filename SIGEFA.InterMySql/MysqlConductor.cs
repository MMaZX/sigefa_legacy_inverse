using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlConductor : IConductor
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsConductor cond)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaConductor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("dni", cond.Dni);
			oParam = cmd.Parameters.AddWithValue("ruc", cond.Ruc);
			oParam = cmd.Parameters.AddWithValue("nombre", cond.Nombre);
			oParam = cmd.Parameters.AddWithValue("licencia", cond.Licencia);
			oParam = cmd.Parameters.AddWithValue("telefono", cond.Telefono);
			oParam = cmd.Parameters.AddWithValue("direccion", cond.Direccion);
			oParam = cmd.Parameters.AddWithValue("codusu", cond.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			cond.CodConductorNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsConductor cond)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaConductor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcon", cond.CodConductor);
			cmd.Parameters.AddWithValue("dni", cond.Dni);
			cmd.Parameters.AddWithValue("ruc", cond.Ruc);
			cmd.Parameters.AddWithValue("nombre", cond.Nombre);
			cmd.Parameters.AddWithValue("licencia", cond.Licencia);
			cmd.Parameters.AddWithValue("telefono", cond.Telefono);
			cmd.Parameters.AddWithValue("direccion", cond.Direccion);
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

	public bool Delete(int CodConductor)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaConductor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcon", CodConductor);
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

	public clsConductor CargaConductor(int Codigo)
	{
		clsConductor cond = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraConductor", con.conector);
			cmd.Parameters.AddWithValue("codcon", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cond = new clsConductor();
					cond.CodConductor = Convert.ToInt32(dr.GetDecimal(0));
					cond.Dni = dr.GetString(1);
					cond.Ruc = dr.GetString(2);
					cond.Nombre = dr.GetString(3);
					cond.Licencia = dr.GetString(4);
					cond.Telefono = dr.GetString(5);
					cond.Direccion = dr.GetString(6);
					cond.Estado = dr.GetBoolean(7);
					cond.CodUser = Convert.ToInt32(dr.GetDecimal(8));
					cond.FechaRegistro = dr.GetDateTime(9);
				}
			}
			return cond;
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

	public clsConductor BuscaConductor(int tipodocumento, string documento)
	{
		clsConductor cond = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaConductor", con.conector);
			cmd.Parameters.AddWithValue("tipodocumento", tipodocumento);
			cmd.Parameters.AddWithValue("documento", documento);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cond = new clsConductor();
					cond.CodConductor = Convert.ToInt32(dr.GetDecimal(0));
					cond.Dni = dr.GetString(1);
					cond.Ruc = dr.GetString(2);
					cond.Nombre = dr.GetString(3);
					cond.Licencia = dr.GetString(4);
					cond.Telefono = dr.GetString(5);
					cond.Direccion = dr.GetString(6);
					cond.Estado = dr.GetBoolean(7);
					cond.CodUser = Convert.ToInt32(dr.GetDecimal(8));
					cond.FechaRegistro = dr.GetDateTime(9);
				}
			}
			return cond;
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

	public DataTable ListaConductores(int tipodocuemento)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaConductores", con.conector);
			cmd.Parameters.AddWithValue("tipodocumento", tipodocuemento);
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

	public DataTable CargaConductores()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaConductores", con.conector);
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
