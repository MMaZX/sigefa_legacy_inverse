using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlVariante : IVariante
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsVariante var)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaVariante", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codcar", var.CodCaracteristica);
			oParam = cmd.Parameters.AddWithValue("descripcion", var.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", var.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			var.CodVarianteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsVariante var)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaVariante", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codvar", var.CodVariante);
			cmd.Parameters.AddWithValue("descripcion", var.Descripcion);
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

	public bool Delete(int CodVariante)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarVariante", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codvar", CodVariante);
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

	public clsVariante CargaVariante(int Codigo)
	{
		clsVariante var = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVariante", con.conector);
			cmd.Parameters.AddWithValue("codvar", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					var = new clsVariante();
					var.CodVariante = Convert.ToInt32(dr.GetDecimal(0));
					var.CodCaracteristica = Convert.ToInt32(dr.GetDecimal(1));
					var.Descripcion = dr.GetString(2);
					var.Estado = dr.GetBoolean(3);
					var.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					var.FechaRegistro = dr.GetDateTime(5);
				}
			}
			return var;
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

	public DataTable ListaVariantes(int CodCar)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaVariantes", con.conector);
			cmd.Parameters.AddWithValue("codcar", CodCar);
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
