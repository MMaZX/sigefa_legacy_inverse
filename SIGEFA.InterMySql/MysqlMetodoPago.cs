using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlMetodoPago : IMetodoPago
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsMetodoPago pago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaMetodoPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("descripcion", pago.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", pago.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			pago.CodMetodoPago = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsMetodoPago pago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaMetodoPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpago", pago.CodMetodoPago);
			cmd.Parameters.AddWithValue("descripcion", pago.Descripcion);
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

	public bool Delete(int CodMetodoPago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarMetodoPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpago", CodMetodoPago);
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

	public clsMetodoPago CargaMetodoPago(int Codigo)
	{
		clsMetodoPago pago = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraMetodoPago", con.conector);
			cmd.Parameters.AddWithValue("codpago", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pago = new clsMetodoPago();
					pago.CodMetodoPago = Convert.ToInt32(dr.GetDecimal(0));
					pago.Descripcion = dr.GetString(1);
					pago.Estado = dr.GetBoolean(2);
					pago.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					pago.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return pago;
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

	public clsMetodoPago BuscaMetodoPago(string Codigo)
	{
		clsMetodoPago pago = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaMetodoPago", con.conector);
			cmd.Parameters.AddWithValue("cod", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pago = new clsMetodoPago();
					pago.CodMetodoPago = Convert.ToInt32(dr.GetDecimal(0));
					pago.Descripcion = dr.GetString(1);
				}
			}
			return pago;
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

	public DataTable ListaMetodoPagos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaMetodoPagos", con.conector);
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

	public DataTable CargaMetodoPagos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaMetodoPagos", con.conector);
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

	public DataTable CargaMetodoPagosIE()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaMetodoPagosIE", con.conector);
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
