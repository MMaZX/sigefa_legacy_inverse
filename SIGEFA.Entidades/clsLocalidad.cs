using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Entidades;

internal class clsLocalidad
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	private string sCodLocalidad;

	private string sCodPadre;

	private string sNombre;

	private int iNivel;

	public string CodLocalidad
	{
		get
		{
			return sCodLocalidad;
		}
		set
		{
			sCodLocalidad = value;
		}
	}

	public string CodPadre
	{
		get
		{
			return sCodPadre;
		}
		set
		{
			sCodPadre = value;
		}
	}

	public string Nombre
	{
		get
		{
			return sNombre;
		}
		set
		{
			sNombre = value;
		}
	}

	public int Nivel
	{
		get
		{
			return iNivel;
		}
		set
		{
			iNivel = value;
		}
	}

	public DataTable CargaLocalidades(string CodPadre, int Nivel)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaLocalidades", con.conector);
			cmd.Parameters.AddWithValue("codpad", CodPadre);
			cmd.Parameters.AddWithValue("niv", Nivel);
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
