using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlLinea : ILinea
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsLinea lin)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaLinea", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codfam", lin.CodFamilia);
			oParam = cmd.Parameters.AddWithValue("referencia", lin.Referencia);
			oParam = cmd.Parameters.AddWithValue("descripcion", lin.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", lin.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			lin.CodLineaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsLinea lin)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaLinea", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codlin", lin.CodLinea);
			cmd.Parameters.AddWithValue("referencia", lin.Referencia);
			cmd.Parameters.AddWithValue("descripcion", lin.Descripcion);
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

	public bool Delete(int CodLinea)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarLinea", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codlin", CodLinea);
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

	public clsLinea CargaLinea(int Codigo)
	{
		clsLinea lin = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraLinea", con.conector);
			cmd.Parameters.AddWithValue("codlin", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					lin = new clsLinea();
					lin.CodLinea = Convert.ToInt32(dr.GetDecimal(0));
					lin.CodFamilia = Convert.ToInt32(dr.GetDecimal(1));
					lin.Referencia = dr.GetString(2);
					lin.Descripcion = dr.GetString(3);
					lin.Estado = dr.GetBoolean(4);
					lin.CodUser = Convert.ToInt32(dr.GetDecimal(5));
					lin.FechaRegistro = dr.GetDateTime(6);
				}
			}
			return lin;
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

	public DataTable ListaLineas(int CodFam)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaLineas", con.conector);
			cmd.Parameters.AddWithValue("codfam", CodFam);
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
