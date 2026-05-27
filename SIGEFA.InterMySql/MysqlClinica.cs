using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

public class MysqlClinica : IClinica
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool InsertPaciente(clsPaciente Paciente)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPaciente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_nombre", Paciente.Nombre);
			oParam = cmd.Parameters.AddWithValue("_fechaNacimiento", Paciente.FechaNacimiento);
			oParam = cmd.Parameters.AddWithValue("_propietario", Paciente.Propietario);
			oParam = cmd.Parameters.AddWithValue("_especie", Paciente.Especie);
			oParam = cmd.Parameters.AddWithValue("_raza", Paciente.Raza);
			oParam = cmd.Parameters.AddWithValue("_sexo", Paciente.Sexo);
			oParam = cmd.Parameters.AddWithValue("_direccion", Paciente.Direccion);
			oParam = cmd.Parameters.AddWithValue("_usuario", Paciente.UsuarioID);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", Paciente.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_estado", Paciente.Estado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Paciente.ID = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool UpdatePaciente(clsPaciente Paciente)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPaciente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_id", Paciente.ID);
			oParam = cmd.Parameters.AddWithValue("_nombre", Paciente.Nombre);
			oParam = cmd.Parameters.AddWithValue("_fechaNacimiento", Paciente.FechaNacimiento);
			oParam = cmd.Parameters.AddWithValue("_propietario", Paciente.Propietario);
			oParam = cmd.Parameters.AddWithValue("_especie", Paciente.Especie);
			oParam = cmd.Parameters.AddWithValue("_raza", Paciente.Raza);
			oParam = cmd.Parameters.AddWithValue("_sexo", Paciente.Sexo);
			oParam = cmd.Parameters.AddWithValue("_direccion", Paciente.Direccion);
			oParam = cmd.Parameters.AddWithValue("_usuario", Paciente.UsuarioID);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", Paciente.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_estado", Paciente.Estado);
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

	public bool DeletePaciente(int Codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarPaciente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_id", Codigo);
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

	public clsPaciente CargaPaciente(int Codigo)
	{
		clsPaciente Pac = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPaciente", con.conector);
			cmd.Parameters.AddWithValue("_id", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					Pac = new clsPaciente();
					Pac.ID = dr.GetInt32(0);
					Pac.Nombre = dr.GetString(1);
					Pac.FechaNacimiento = dr.GetDateTime(2);
					Pac.Propietario = dr.GetString(3);
					Pac.Especie = dr.GetString(4);
					Pac.Raza = dr.GetString(5);
					Pac.Sexo = dr.GetString(6);
					Pac.Direccion = dr.GetString(7);
					Pac.UsuarioID = dr.GetInt32(8);
					Pac.FechaRegistro = dr.GetDateTime(9);
					Pac.Estado = dr.GetInt32(10);
				}
			}
			return Pac;
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

	public DataTable ListaPacientes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaPacientes", con.conector);
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

	public bool InsertHistoriaCabecera(clsHistoria Historia)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCabeceraHistoria", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_numero", Historia.Numero);
			oParam = cmd.Parameters.AddWithValue("_pacienteid", Historia.PacienteID);
			oParam = cmd.Parameters.AddWithValue("_usuarioid", Historia.UsuarioID);
			oParam = cmd.Parameters.AddWithValue("_fecharegistro", Historia.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Historia.ID = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool InsertHistoriaDetalle(clsDetalleHistoria Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleHistoria", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_historiaid", Detalle.HistoriaID);
			oParam = cmd.Parameters.AddWithValue("_fechahora", Detalle.FechaHora);
			oParam = cmd.Parameters.AddWithValue("_temperatura", Detalle.Temperatura);
			oParam = cmd.Parameters.AddWithValue("_peso", Detalle.Peso);
			oParam = cmd.Parameters.AddWithValue("_notas", Detalle.Notas);
			oParam = cmd.Parameters.AddWithValue("_tratamientos", Detalle.Tratamientos);
			oParam = cmd.Parameters.AddWithValue("_fallecido", Detalle.Fallecimiento);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Detalle.ID = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool UpdateHistoriaCabecera(clsHistoria Historia)
	{
		throw new NotImplementedException();
	}

	public bool UpdateHistoriaDetalle(clsDetalleHistoria Detalle)
	{
		throw new NotImplementedException();
	}

	public bool DeleteHistoria(int Codigo)
	{
		throw new NotImplementedException();
	}

	public clsHistoria CargaHistoriaCabecera(string Numero)
	{
		clsHistoria Result = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCabeceraHistoria", con.conector);
			cmd.Parameters.AddWithValue("_numero", Numero);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					Result = new clsHistoria();
					Result.ID = dr.GetInt32(0);
					Result.PacienteID = dr.GetInt32(1);
					Result.Numero = dr.GetString(2);
					Result.UsuarioID = dr.GetInt32(3);
					Result.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return Result;
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

	public clsDetalleHistoria CargaHistoriaDetalle(int Codigo)
	{
		throw new NotImplementedException();
	}

	public DataTable ListaDetalleHistorial(int CodigoCab)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDetalleHistorial", con.conector);
			cmd.Parameters.AddWithValue("_historiaid", CodigoCab);
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
