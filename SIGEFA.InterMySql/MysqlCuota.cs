using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlCuota : ICuota
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsCuota cuota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCuota", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpreban", cuota.CodPrestamoBancario);
			oParam = cmd.Parameters.AddWithValue("nrocuo", cuota.NroCuota);
			oParam = cmd.Parameters.AddWithValue("codmon", cuota.CodMoneda);
			oParam = cmd.Parameters.AddWithValue("fechaemision", cuota.FechaEmision);
			oParam = cmd.Parameters.AddWithValue("fechavencimiento", cuota.FechaVencimiento);
			oParam = cmd.Parameters.AddWithValue("monto", cuota.Monto);
			oParam = cmd.Parameters.AddWithValue("montopendiente", cuota.MontoPendiente);
			oParam = cmd.Parameters.AddWithValue("codusu", cuota.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			cuota.CodCuotaPrestamo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public clsCuota CargaCuota(int Codcuota)
	{
		clsCuota cuota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCuota", con.conector);
			cmd.Parameters.AddWithValue("codcu", Codcuota);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cuota = new clsCuota();
					cuota.CodCuotaPrestamo = Convert.ToInt32(dr.GetDecimal(0));
					cuota.CodPrestamoBancario = Convert.ToInt32(dr.GetString(1));
					cuota.CodMoneda = Convert.ToInt32(dr.GetString(2));
					cuota.DescMoneda = dr.GetString(3);
					cuota.TipoCambio = dr.GetDecimal(4);
					cuota.FechaEmision = dr.GetDateTime(5);
					cuota.FechaVencimiento = dr.GetDateTime(6);
					cuota.FechaCancelado = dr.GetDateTime(7);
					cuota.Monto = Convert.ToDecimal(dr.GetDecimal(8));
					cuota.MontoPendiente = Convert.ToDecimal(dr.GetDecimal(9));
					cuota.Montoadicional = Convert.ToDecimal(dr.GetDecimal(10));
					cuota.Cancelado = dr.GetBoolean(11);
					cuota.Estado = dr.GetBoolean(12);
					cuota.CodUser = Convert.ToInt32(dr.GetDecimal(13));
					cuota.FechaRegistro = dr.GetDateTime(14);
				}
			}
			return cuota;
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

	public DataTable MuestraListaCuotasPrestamo(int CodPreBan)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaCuotasPrestamo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpreban", CodPreBan);
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
