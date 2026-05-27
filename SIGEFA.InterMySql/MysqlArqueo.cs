using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlArqueo : IArqueo
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public DataTable ListaArqueos(int opcion1, int opcion2, int mes1, int anio1, int codAlman)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaArqueos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("opcion1", opcion1);
			cmd.Parameters.AddWithValue("opcion2", opcion2);
			cmd.Parameters.AddWithValue("mes1", mes1);
			cmd.Parameters.AddWithValue("anio1", anio1);
			cmd.Parameters.AddWithValue("codAlman", codAlman);
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

	public DataTable ListaDetalleArqueos(int codArq)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleArqueo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codArq", codArq);
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

	public bool Insert(clsArqueo arqe)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaArqueo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codUser", arqe.ICodUsuario);
			oParam = cmd.Parameters.AddWithValue("fech", arqe.DFecha);
			oParam = cmd.Parameters.AddWithValue("codAlman", arqe.ICodAlma);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			arqe.ICodArqueoNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool InsertDetalle(clsDetalleArqueo detarqe)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleArqueo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codArq", detarqe.ICodArqueo);
			oParam = cmd.Parameters.AddWithValue("codUser", detarqe.ICodUsuario);
			oParam = cmd.Parameters.AddWithValue("observ", detarqe.SObservacion);
			oParam = cmd.Parameters.AddWithValue("codAlma", detarqe.ICodAlma);
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

	public bool InsertChekeoDetalle(clsDetalleArqueo detarqe, int codArq)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ChekeaDetalleArqueo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("fisico", detarqe.DStockF);
			oParam = cmd.Parameters.AddWithValue("dif", detarqe.DDiferencia);
			oParam = cmd.Parameters.AddWithValue("observa", detarqe.SObservacion);
			oParam = cmd.Parameters.AddWithValue("codDeta", detarqe.ICodDetalle);
			oParam = cmd.Parameters.AddWithValue("codArq", codArq);
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

	public bool Update(clsArqueo arqe)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaArqueoEstado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("observa", arqe.SObservacion);
			cmd.Parameters.AddWithValue("codUsAp", arqe.ICodUsuarioApro);
			cmd.Parameters.AddWithValue("codArq", arqe.ICodArqueo);
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
