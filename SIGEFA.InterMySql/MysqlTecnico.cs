using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;

namespace SIGEFA.InterMySql;

internal class MysqlTecnico
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	internal bool insert(clsTecnico tec_aux)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTecnico", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_dni", tec_aux.Dni);
			oParam = cmd.Parameters.AddWithValue("_nombre", tec_aux.Nombre);
			oParam = cmd.Parameters.AddWithValue("_apellidos", tec_aux.Apellidos);
			oParam = cmd.Parameters.AddWithValue("_celular", tec_aux.Celular);
			oParam = cmd.Parameters.AddWithValue("_fechaNacimiento", tec_aux.FechaNacimiento);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", tec_aux.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_direccion", tec_aux.Direccion);
			oParam = cmd.Parameters.AddWithValue("_codUserRegistro", tec_aux.CodUserRegistro);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			if (cmd.ExecuteNonQuery() != 0)
			{
				tec_aux.Id = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	internal clsTecnico cargaTecnico(int codTecnico)
	{
		clsTecnico tec = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTecnico", con.conector);
			cmd.Parameters.AddWithValue("_idTecnico", codTecnico);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					tec = new clsTecnico();
					tec.Id = dr.GetInt32(0);
					tec.Dni = dr.GetString(1);
					tec.Nombre = dr.GetString(2);
					tec.Celular = dr.GetString(3);
					tec.FechaNacimiento = dr.GetDateTime(4);
					tec.Direccion = dr.GetString(5);
					tec.Apellidos = dr.GetString(6);
					tec.Oficios = new List<clsOficio>();
				}
			}
			return tec;
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

	internal DataTable listaTecnicos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTecnicos", con.conector);
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

	internal bool update(clsTecnico tec_aux)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaTecnico", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_idTecnico", tec_aux.Id);
			cmd.Parameters.AddWithValue("_dni", tec_aux.Dni);
			cmd.Parameters.AddWithValue("_nombre", tec_aux.Nombre);
			cmd.Parameters.AddWithValue("_apellidos", tec_aux.Apellidos);
			cmd.Parameters.AddWithValue("_celular", tec_aux.Celular);
			cmd.Parameters.AddWithValue("_fechaNacimiento", tec_aux.FechaNacimiento);
			cmd.Parameters.AddWithValue("_direccion", tec_aux.Direccion);
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

	internal bool eliminarOficios(int idTecnico)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarOficiosDeTecnico", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_idTecnico", idTecnico);
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

	internal bool registraOficio(int idTecnico, int idOficio)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("RegistraOficioEnTecnico", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_idTecnico", idTecnico);
			oParam = cmd.Parameters.AddWithValue("_idOficio", idOficio);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			if (cmd.ExecuteNonQuery() != 0)
			{
				int aux = Convert.ToInt32(cmd.Parameters["newid"].Value);
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
