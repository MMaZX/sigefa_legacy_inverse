using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlFlujoCaja : IFlujoCaja
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsFlujoCaja flu)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaFlujoCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("sucur", flu.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("fec", flu.FechaApertura);
			oParam = cmd.Parameters.AddWithValue("montoape", flu.MontoApertura);
			oParam = cmd.Parameters.AddWithValue("codusu", flu.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			flu.CodFlujoCajaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				if (flu.CodFlujoCajaNuevo > 0)
				{
					return true;
				}
				return false;
			}
			if (flu.CodFlujoCajaNuevo > 0)
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

	public bool Update(clsFlujoCaja flu)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaFlujoCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codflu", flu.CodFlujoCaja);
			cmd.Parameters.AddWithValue("sucur", flu.CodSucursal);
			cmd.Parameters.AddWithValue("montodis", flu.MontoDisponible);
			cmd.Parameters.AddWithValue("montoing", flu.MontoIngresado);
			cmd.Parameters.AddWithValue("montocie", flu.MontoCierre);
			cmd.Parameters.AddWithValue("fec", flu.FechaCierre);
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

	public bool Delete(int CodFlujoCaja, int CodSucursal)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarFlujoCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codflu", CodFlujoCaja);
			cmd.Parameters.AddWithValue("sucur", CodSucursal);
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

	public clsFlujoCaja CargaFlujosCaja(DateTime fecha, int CodSucursal)
	{
		clsFlujoCaja flu = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFlujoCaja", con.conector);
			cmd.Parameters.AddWithValue("fec", fecha);
			cmd.Parameters.AddWithValue("sucur", CodSucursal);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					flu = new clsFlujoCaja();
					flu.CodFlujoCaja = Convert.ToInt32(dr.GetDecimal(0));
					flu.CodSucursal = Convert.ToInt32(dr.GetDecimal(1));
					flu.FechaApertura = dr.GetDateTime(2);
					flu.MontoApertura = dr.GetDecimal(3);
					flu.FechaCierre = dr.GetDateTime(4);
					flu.MontoCierre = dr.GetDecimal(5);
					flu.MontoIngresado = dr.GetDecimal(6);
					flu.MontoDepositado = dr.GetDecimal(7);
					flu.MontoDisponible = dr.GetDecimal(8);
					flu.FechaDeposito = dr.GetDateTime(9);
					flu.Estado = dr.GetBoolean(10);
					flu.Deposito = dr.GetBoolean(11);
					flu.FechaRegistro = dr.GetDateTime(12);
					flu.CodUser = Convert.ToInt32(dr.GetDecimal(13));
				}
			}
			return flu;
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

	public DataTable ListaFlujosCaja(int codSucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFlujoCaja", con.conector);
			cmd.Parameters.AddWithValue("sucur", codSucursal);
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

	public DataTable ListaPagoCobro(int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarTipoPagoServ", con.conector);
			cmd.Parameters.AddWithValue("tipo_ex", tipo);
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

	public clsFlujoCaja VerificaSaldoCaja(int CodSucursal)
	{
		clsFlujoCaja caja = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaSaldoCaja", con.conector);
			cmd.Parameters.AddWithValue("codSucur", CodSucursal);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					caja = new clsFlujoCaja();
					caja.MontoApertura = Convert.ToDecimal(dr.GetDecimal(0));
					caja.MontoIngresado = Convert.ToDecimal(dr.GetDecimal(1));
					caja.MontoDepositado = Convert.ToDecimal(dr.GetDecimal(2));
					caja.MontoDisponible = Convert.ToDecimal(dr.GetDecimal(3));
				}
			}
			return caja;
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

	public int VerificaAperturaCaja(int codSucursal)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaAperturaCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("sucur", codSucursal);
			return Convert.ToInt32(cmd.ExecuteScalar());
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

	public clsFlujoCaja VerificaDepositoCaja(int CodSucursal, DateTime fecha)
	{
		clsFlujoCaja caja = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaDepositoCaja", con.conector);
			cmd.Parameters.AddWithValue("sucur", CodSucursal);
			cmd.Parameters.AddWithValue("fec", fecha);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					caja = new clsFlujoCaja();
					caja.FechaCierre = dr.GetDateTime(0);
					caja.MontoIngresado = Convert.ToDecimal(dr.GetDecimal(1));
				}
			}
			return caja;
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
