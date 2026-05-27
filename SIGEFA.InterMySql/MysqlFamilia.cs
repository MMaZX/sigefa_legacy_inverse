using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlFamilia : IFamilias
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsFamilia fam)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaFamilia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("referencia", fam.Referencia);
			oParam = cmd.Parameters.AddWithValue("descripcion", fam.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", fam.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			fam.CodFamiliaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsFamilia fam)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaFamilia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfam", fam.CodFamilia);
			cmd.Parameters.AddWithValue("referencia", fam.Referencia);
			cmd.Parameters.AddWithValue("descripcion", fam.Descripcion);
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

	public bool Delete(int CodFamilia)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarFamilia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfam", CodFamilia);
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

	public clsFamilia CargaFamilia(int Codigo)
	{
		clsFamilia fam = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFamilia", con.conector);
			cmd.Parameters.AddWithValue("codfam", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					fam = new clsFamilia();
					fam.CodFamilia = Convert.ToInt32(dr.GetDecimal(0));
					fam.Referencia = dr.GetString(1);
					fam.Descripcion = dr.GetString(2);
					fam.Estado = dr.GetBoolean(3);
					fam.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					fam.FechaRegistro = dr.GetDateTime(5);
				}
			}
			return fam;
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

	public DataTable ListaFamilias()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFamilias", con.conector);
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

	public DataTable MuestraLinea()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaLinea", con.conector);
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
