using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlMarcaTransporte : IMarcaTransporte
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsMarcaTransporte mar)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaMarcaTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("descripcion", mar.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", mar.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			mar.CodMarcaTransporteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsMarcaTransporte mar)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaMarcaTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codmar", mar.CodMarcaTransporte);
			cmd.Parameters.AddWithValue("descripcion", mar.Descripcion);
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

	public bool Delete(int CodMarcaTransporte)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarMarcaTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codmar", CodMarcaTransporte);
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

	public clsMarcaTransporte CargaMarcaTransporte(int Codigo)
	{
		clsMarcaTransporte mar = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraMarcaTransporte", con.conector);
			cmd.Parameters.AddWithValue("codmar", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					mar = new clsMarcaTransporte();
					mar.CodMarcaTransporte = Convert.ToInt32(dr.GetDecimal(0));
					mar.Descripcion = dr.GetString(1);
					mar.Estado = dr.GetBoolean(2);
					mar.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					mar.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return mar;
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

	public DataTable ListaMarcaTransportes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaMarcaTransporte", con.conector);
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
