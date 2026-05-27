using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlRecibo : IRecibo
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsRecibos recibo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaReciboCajaChica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("des", recibo.Concepto);
			cmd.Parameters.AddWithValue("mon", recibo.Monto);
			cmd.Parameters.AddWithValue("fec", recibo.Fecha);
			cmd.Parameters.AddWithValue("codusu", recibo.CodUser);
			cmd.Parameters.AddWithValue("codSucur", recibo.CodSucursal);
			cmd.Parameters.AddWithValue("codtipodoc", recibo.CodTipoDocumento);
			cmd.Parameters.AddWithValue("codse", recibo.CodSerie);
			cmd.Parameters.AddWithValue("num", recibo.Numeracion);
			cmd.Parameters.AddWithValue("tipo", recibo.TipoCaja);
			cmd.Parameters.AddWithValue("ingresoegreso_ex", recibo.Igresoegreso);
			cmd.Parameters.AddWithValue("codtipopagocajachica_ex", recibo.Codtipopagocajahica);
			cmd.Parameters.AddWithValue("nombre_ex", recibo.Nombre);
			cmd.Parameters.AddWithValue("direccion_ex", recibo.Direccion);
			cmd.Parameters.AddWithValue("dni_ex", recibo.Dni);
			cmd.Parameters.AddWithValue("saldo_ex", recibo.Saldocaja);
			MySqlParameter oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			recibo.CodRecibosNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsRecibos recibo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaRecibos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codigo", recibo.CodRecibos);
			cmd.Parameters.AddWithValue("concep", recibo.Concepto);
			cmd.Parameters.AddWithValue("mont", recibo.Monto);
			cmd.Parameters.AddWithValue("fech", recibo.Fecha);
			cmd.Parameters.AddWithValue("codUser", recibo.CodUser);
			cmd.Parameters.AddWithValue("codSucur", recibo.CodSucursal);
			cmd.Parameters.AddWithValue("tipo", recibo.TipoCaja);
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

	public DataTable ListaRecibos(int codSucur, DateTime fecha1, DateTime fecha2, int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaRecibos", con.conector);
			cmd.Parameters.AddWithValue("codSucur", codSucur);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("tipo", tipo);
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

	public DataTable ListaRecibosEgreso(int codSucur, int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaRecibosEgreso", con.conector);
			cmd.Parameters.AddWithValue("codSucur", codSucur);
			cmd.Parameters.AddWithValue("tipo", tipo);
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

	public int Correlativo(int codtipo)
	{
		try
		{
			int correl = 0;
			con.conectarBD();
			cmd = new MySqlCommand("TraerCorrelativoTipoCajaChica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtipopagocajachica_ex", codtipo);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					correl = dr.GetInt32(0);
				}
			}
			return correl;
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
