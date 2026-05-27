using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlFormaPago : IFormaPago
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsFormaPago pago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaFormaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("dias", pago.Dias);
			oParam = cmd.Parameters.AddWithValue("descripcion", pago.Descripcion);
			oParam = cmd.Parameters.AddWithValue("tipo", pago.Tipo);
			oParam = cmd.Parameters.AddWithValue("codusu", pago.CodUser);
			oParam = cmd.Parameters.AddWithValue("tipoa", pago.Tipoaccion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			pago.CodFormaPago = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsFormaPago pago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaFormaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpago", pago.CodFormaPago);
			cmd.Parameters.AddWithValue("dias", pago.Dias);
			cmd.Parameters.AddWithValue("descripcion", pago.Descripcion);
			cmd.Parameters.AddWithValue("tipo", pago.Tipo);
			cmd.Parameters.AddWithValue("tipoa", pago.Tipoaccion);
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

	public bool Delete(int CodFormaPago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarFormaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpago", CodFormaPago);
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

	public clsFormaPago CargaFormaPago(int Codigo)
	{
		clsFormaPago pago = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFormaPago", con.conector);
			cmd.Parameters.AddWithValue("codpago", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pago = new clsFormaPago();
					pago.CodFormaPago = Convert.ToInt32(dr.GetDecimal(0));
					pago.Dias = Convert.ToInt32(dr.GetDecimal(1));
					pago.Descripcion = dr.GetString(2);
					pago.Estado = dr.GetBoolean(3);
					pago.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					pago.FechaRegistro = dr.GetDateTime(5);
					pago.Tipo = dr.GetBoolean(6);
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

	public clsFormaPago BuscaFormaPago(string Codigo)
	{
		clsFormaPago pago = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaFormaPago", con.conector);
			cmd.Parameters.AddWithValue("cod", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pago = new clsFormaPago();
					pago.CodFormaPago = Convert.ToInt32(dr.GetDecimal(0));
					pago.Dias = Convert.ToInt32(dr.GetDecimal(1));
					pago.Descripcion = dr.GetString(2);
					pago.Tipo = dr.GetBoolean(3);
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

	public DataTable ListaFormaPagos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFormaPagos", con.conector);
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

	public DataTable CargaFormaPagos(int tip)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaFormaPagos", con.conector);
			cmd.Parameters.AddWithValue("tip", tip);
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

	public DataTable CargaFormaPagosReportes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaFormaPagosReportes", con.conector);
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

	public clsFormaPago BuscaFormaPagoVenta(int Codigo)
	{
		clsFormaPago pago = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaFormaPagoVenta", con.conector);
			cmd.Parameters.AddWithValue("codforma", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pago = new clsFormaPago();
					pago.CodFormaPago = Convert.ToInt32(dr.GetDecimal(0));
					pago.Dias = Convert.ToInt32(dr.GetDecimal(1));
					pago.Descripcion = dr.GetString(2);
					pago.Estado = dr.GetBoolean(3);
					pago.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					pago.FechaRegistro = dr.GetDateTime(5);
					pago.Tipo = dr.GetBoolean(6);
					pago.Tipoaccion = dr.GetBoolean(7);
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

	public DataTable CargaFormaPagosCuotas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFormaPagosCuotas", con.conector);
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
