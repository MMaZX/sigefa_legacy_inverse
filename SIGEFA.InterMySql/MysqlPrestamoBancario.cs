using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlPrestamoBancario : IPrestamoBancario
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsPrestamoBancario preBan)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPrestamoBancario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codban", Convert.ToInt32(preBan.CodBanco));
			oParam = cmd.Parameters.AddWithValue("codmon", Convert.ToInt32(preBan.CodMoneda));
			oParam = cmd.Parameters.AddWithValue("tipcam", preBan.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("monpre", preBan.Montoprestamo);
			oParam = cmd.Parameters.AddWithValue("monint", preBan.Montointeres);
			oParam = cmd.Parameters.AddWithValue("mondev", preBan.Montodevolver);
			oParam = cmd.Parameters.AddWithValue("fecapr", preBan.Fechaaprobacion);
			oParam = cmd.Parameters.AddWithValue("fecven", preBan.Fechavencimiento);
			oParam = cmd.Parameters.AddWithValue("descr", preBan.Descripcion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			preBan.CodPrestamoBancario = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public DataTable ListaPrestamos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPrestamosBancarios", con.conector);
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

	public bool Delete(int CodPreBan)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarPrestamoBancario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpreban", CodPreBan);
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

	public DataTable MuestraPagosPrestamo(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPagosPrestamoBancario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("esta", Estado);
			cmd.Parameters.AddWithValue("empre", codEmpresa);
			cmd.Parameters.AddWithValue("fecha1", Fecha1);
			cmd.Parameters.AddWithValue("fecha2", Fecha2);
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

	public clsPrestamoBancario CargaPrestamoBancario(int CodPreBan)
	{
		clsPrestamoBancario prestamo = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPrestamoBancario", con.conector);
			cmd.Parameters.AddWithValue("codpreban", CodPreBan);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					prestamo = new clsPrestamoBancario();
					prestamo.CodPrestamoBancario = dr.GetInt32(0);
					prestamo.CodBanco = dr.GetInt32(1);
					prestamo.DescBanco = dr.GetString(2);
					prestamo.CodMoneda = dr.GetInt32(3);
					prestamo.DescMoneda = dr.GetString(4);
					prestamo.TipoCambio = dr.GetDecimal(5);
					prestamo.Montoprestamo = dr.GetDecimal(6);
					prestamo.Montointeres = dr.GetDecimal(7);
					prestamo.Montodevolver = dr.GetDecimal(8);
					prestamo.Pendiente = dr.GetDecimal(9);
					prestamo.Fechaaprobacion = dr.GetDateTime(10);
					prestamo.Descripcion = dr.GetString(12);
					prestamo.Cancelado = dr.GetInt32(14);
					prestamo.CantCuotas = dr.GetInt32(17);
				}
			}
			return prestamo;
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
