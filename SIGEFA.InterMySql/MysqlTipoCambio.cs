using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlTipoCambio : ITipoCambio
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsTipoCambio tc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTipoCambio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("compra", tc.Compra);
			oParam = cmd.Parameters.AddWithValue("venta", tc.Venta);
			oParam = cmd.Parameters.AddWithValue("fecha", tc.Fecha);
			oParam = cmd.Parameters.AddWithValue("codusu", tc.CodUser);
			oParam = cmd.Parameters.AddWithValue("codmon", tc.ICodMoneda);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			tc.CodTipoCambio = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsTipoCambio tc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaTipoCambio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtc", tc.CodTipoCambio);
			cmd.Parameters.AddWithValue("compra", tc.Compra);
			cmd.Parameters.AddWithValue("venta", tc.Venta);
			cmd.Parameters.AddWithValue("codMon", tc.ICodMoneda);
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

	public bool Delete(int CodTipoCambio)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarTipoCambio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtc", CodTipoCambio);
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

	public clsTipoCambio CargaTipoCambio(DateTime Fecha, int codMoneda)
	{
		clsTipoCambio tc = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTipoCambio", con.conector);
			cmd.Parameters.AddWithValue("fechab", Fecha.ToString("yyyy-MM-dd"));
			cmd.Parameters.AddWithValue("codM", codMoneda);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					tc = new clsTipoCambio();
					tc.CodTipoCambio = Convert.ToInt32(dr.GetDecimal(0));
					tc.Compra = dr.GetDouble(1);
					tc.Venta = dr.GetDouble(2);
					tc.Fecha = dr.GetDateTime(3);
					tc.Estado = dr.GetBoolean(4);
					tc.CodUser = Convert.ToInt32(dr.GetDecimal(5));
					tc.FechaRegistro = dr.GetDateTime(6);
				}
			}
			return tc;
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

	public bool VerificaTCfecha(DateTime Fecha)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaTipoCambio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fechab", Fecha.ToString("yyyy-MM-dd"));
			if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
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

	public DataTable ListaTipoCambios(int Mes, int Año)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTipoCambios", con.conector);
			cmd.Parameters.AddWithValue("mes", Mes);
			cmd.Parameters.AddWithValue("año", Año);
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
