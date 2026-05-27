using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlMoneda : IMoneda
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsMoneda moned)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaMoneda", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpais", moned.IcodPais);
			oParam = cmd.Parameters.AddWithValue("descrip", moned.SDescripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", moned.ICodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			moned.IcodMoneda = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsMoneda ser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaMoneda", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpa", ser.IcodPais);
			cmd.Parameters.AddWithValue("descrip", ser.SDescripcion);
			cmd.Parameters.AddWithValue("codUsu", ser.ICodUser);
			cmd.Parameters.AddWithValue("codMon", ser.IcodMoneda);
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

	public bool Delete(int CodMoned)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("Eliminarmoneda", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codmon", CodMoned);
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

	public clsMoneda CargaMoneda(int Codigo)
	{
		clsMoneda ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraMoneda", con.conector);
			cmd.Parameters.AddWithValue("codMon", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsMoneda();
					ser.IcodMoneda = Convert.ToInt32(dr.GetInt32(0));
					ser.SDescripcion = dr.GetString(1);
					ser.IcodPais = Convert.ToInt32(dr.GetInt32(2));
					ser.SNombrePais = dr.GetString(3);
					ser.ICodUser = Convert.ToInt32(dr.GetString(5));
					ser.SUsuario = dr.GetString(4);
					ser.DtFechaRegistro = dr.GetDateTime(6);
					ser.IEstado = dr.GetBoolean(7);
				}
			}
			return ser;
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

	public DataTable CargaPais()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaPais", con.conector);
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

	public DataTable ListaMonedas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaMonedas", con.conector);
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

	public clsMoneda Buscamoneda(string descrip)
	{
		clsMoneda ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaMoneda", con.conector);
			cmd.Parameters.AddWithValue("descrip", descrip);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsMoneda();
					ser.IcodMoneda = Convert.ToInt32(dr.GetDecimal(0));
					ser.SDescripcion = dr.GetString(1);
					ser.IcodPais = Convert.ToInt32(dr.GetDecimal(2));
					ser.SNombrePais = dr.GetString(3);
				}
			}
			return ser;
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

	public DataTable CargaMonedasHabiles()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraMonedasHabiles", con.conector);
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

	public int BuscamonedaXdescripcion(string descrip)
	{
		int codigo = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaMoneda", con.conector);
			cmd.Parameters.AddWithValue("descrip", descrip);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return codigo;
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
}
