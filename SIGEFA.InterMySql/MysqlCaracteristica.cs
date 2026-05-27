using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlCaracteristica : ICaracteristica
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsCaracteristica car)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCaracteristica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("descripcion", car.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", car.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			car.CodCaracteristicaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsCaracteristica car)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCaracteristica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcar", car.CodCaracteristica);
			cmd.Parameters.AddWithValue("descripcion", car.Descripcion);
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

	public bool Delete(int CodCaracteristica)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarCaracteristica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcar", CodCaracteristica);
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

	public clsCaracteristica CargaCaracteristica(int Codigo)
	{
		clsCaracteristica car = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCaracteristica", con.conector);
			cmd.Parameters.AddWithValue("codcar", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					car = new clsCaracteristica();
					car.CodCaracteristica = Convert.ToInt32(dr.GetDecimal(0));
					car.Descripcion = dr.GetString(1);
					car.Estado = dr.GetBoolean(2);
					car.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					car.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return car;
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

	public DataTable ListaCaracteristicas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCaracteristicas", con.conector);
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
