using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlConciliacionBancaria : IConciliacionBancaria
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsConciliacionBancaria acce)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaConciliacionBancaria", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codbanco_ex", acce.Codbanco);
			oParam = cmd.Parameters.AddWithValue("codcuenta_ex", acce.Codcuenta);
			oParam = cmd.Parameters.AddWithValue("saldoextracto_ex", acce.Saldoextracto);
			oParam = cmd.Parameters.AddWithValue("montonocobrado_ex", acce.Montonocobrado);
			oParam = cmd.Parameters.AddWithValue("saldolibro_ex", acce.Saldolibro);
			oParam = cmd.Parameters.AddWithValue("codmoneda_ex", acce.Codmoneda);
			oParam = cmd.Parameters.AddWithValue("coduser_ex", acce.Coduser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			acce.CodconciliacionNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalle(clsDetalleConciliacion det)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleConciliacionBancaria", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codconciliacion_ex", det.Codconciliacion);
			oParam = cmd.Parameters.AddWithValue("codctamovimiento_ex", det.Codctamovimiento);
			oParam = cmd.Parameters.AddWithValue("monto_ex", det.Monto);
			oParam = cmd.Parameters.AddWithValue("estado_ex", det.Actico_conci);
			oParam = cmd.Parameters.AddWithValue("coduser_ex", det.Coduser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			det.CodconciliacionNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(int codalma, int codbanco, int codcuenta, int CodConciliacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaMovimientosConci", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codbanco", codbanco);
			cmd.Parameters.AddWithValue("codcuenta", codcuenta);
			cmd.Parameters.AddWithValue("codigo", CodConciliacion);
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

	public bool UpdateBandera(int codalma, int codbanco, int codcuenta, int CodConciliacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaBanderaConciliacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codbanco", codbanco);
			cmd.Parameters.AddWithValue("codcuenta", codcuenta);
			cmd.Parameters.AddWithValue("codigo", CodConciliacion);
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
}
