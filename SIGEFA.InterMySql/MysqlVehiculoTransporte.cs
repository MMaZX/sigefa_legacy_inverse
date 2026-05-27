using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlVehiculoTransporte : IVehiculoTransporte
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsVehiculoTransporte veh)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaVehiculoTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("placa", veh.Placa);
			oParam = cmd.Parameters.AddWithValue("codmar", veh.CodMarca);
			oParam = cmd.Parameters.AddWithValue("codmod", veh.CodModelo);
			oParam = cmd.Parameters.AddWithValue("año", veh.Año);
			oParam = cmd.Parameters.AddWithValue("constancia", veh.ConstanciaInscripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", veh.CodUser);
			oParam = cmd.Parameters.AddWithValue("confvehicular", veh.ConfVehicular);
			oParam = cmd.Parameters.AddWithValue("soat", veh.Soat);
			oParam = cmd.Parameters.AddWithValue("mtc", veh.MTC);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			veh.CodVehiculoTransporteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsVehiculoTransporte veh)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaVehiculoTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codveh", veh.CodVehiculoTransporte);
			cmd.Parameters.AddWithValue("placa", veh.Placa);
			cmd.Parameters.AddWithValue("codmar", veh.CodMarca);
			cmd.Parameters.AddWithValue("codmod", veh.CodModelo);
			cmd.Parameters.AddWithValue("año", veh.Año);
			cmd.Parameters.AddWithValue("constancia", veh.ConstanciaInscripcion);
			cmd.Parameters.AddWithValue("confvehicular", veh.ConfVehicular);
			cmd.Parameters.AddWithValue("mtc", veh.MTC);
			cmd.Parameters.AddWithValue("soat", veh.Soat);
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

	public bool Delete(int CodVehiculoTransporte)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaVehiculoTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codveh", CodVehiculoTransporte);
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

	public clsVehiculoTransporte CargaVehiculoTransporte(int Codigo)
	{
		clsVehiculoTransporte veh = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVehiculoTransporte", con.conector);
			cmd.Parameters.AddWithValue("codveh", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					veh = new clsVehiculoTransporte();
					veh.CodVehiculoTransporte = Convert.ToInt32(dr.GetDecimal(0));
					veh.Placa = dr.GetString(1);
					veh.CodMarca = Convert.ToInt32(dr.GetDecimal(2));
					veh.Marca = dr.GetString(3);
					veh.CodModelo = Convert.ToInt32(dr.GetDecimal(4));
					veh.Modelo = dr.GetString(5);
					veh.Año = Convert.ToInt32(dr.GetDecimal(6));
					veh.ConstanciaInscripcion = dr.GetString(7);
					veh.Estado = dr.GetBoolean(8);
					veh.CodUser = Convert.ToInt32(dr.GetDecimal(9));
					veh.FechaRegistro = dr.GetDateTime(10);
					veh.Soat = dr.GetString(11);
					veh.ConfVehicular = dr.GetString(12);
					veh.MTC = dr.GetString(13);
				}
			}
			return veh;
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

	public DataTable ListaVehiculoTransportes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaVehiculoTransportes", con.conector);
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

	public DataTable CargaVehiculoTransportes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaVehiculoTransportes", con.conector);
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
