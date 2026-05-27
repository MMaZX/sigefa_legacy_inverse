using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlSerie : ISerie
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsSerie ser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaSerie", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("coddoc", ser.CodDocumento);
			oParam = cmd.Parameters.AddWithValue("codalm", ser.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("serie", ser.Serie);
			oParam = cmd.Parameters.AddWithValue("inicio", ser.Inicio);
			oParam = cmd.Parameters.AddWithValue("fin", ser.Fin);
			oParam = cmd.Parameters.AddWithValue("numeracion", ser.Numeracion);
			oParam = cmd.Parameters.AddWithValue("codusu", ser.CodUser);
			oParam = cmd.Parameters.AddWithValue("nomimpre", ser.NombreImpresora);
			oParam = cmd.Parameters.AddWithValue("paper", ser.PaperSize);
			oParam = cmd.Parameters.AddWithValue("serimpre", ser.SerieImpresora);
			oParam = cmd.Parameters.AddWithValue("preimpreso", ser.PreImpreso);
			oParam = cmd.Parameters.AddWithValue("codempresa", ser.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			ser.CodSerie = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsSerie ser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaSerie", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codse", ser.CodSerie);
			cmd.Parameters.AddWithValue("serie", ser.Serie);
			cmd.Parameters.AddWithValue("inicio", ser.Inicio);
			cmd.Parameters.AddWithValue("fin", ser.Fin);
			cmd.Parameters.AddWithValue("numeracion", ser.Numeracion);
			cmd.Parameters.AddWithValue("nomimpre", ser.NombreImpresora);
			cmd.Parameters.AddWithValue("paper", ser.PaperSize);
			cmd.Parameters.AddWithValue("serie_impresora", ser.SerieImpresora);
			cmd.Parameters.AddWithValue("preimpreso", ser.PreImpreso);
			cmd.Parameters.AddWithValue("almacen", ser.CodAlmacen);
			cmd.Parameters.AddWithValue("empresa", ser.CodEmpresa);
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

	public int ExistenSeries(int CodDocumento, int CodAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ExisteSeries", con.conector);
			cmd.Parameters.AddWithValue("coddoc", CodDocumento);
			cmd.Parameters.AddWithValue("codalm", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public int GetCodigoSerie(int CodDocumento, int CodAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoSerie", con.conector);
			cmd.Parameters.AddWithValue("coddoc", CodDocumento);
			cmd.Parameters.AddWithValue("codalm", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public clsSerie BuscaSeriexDocumento(int codDocumento, int CodAlmacen)
	{
		clsSerie ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaSeriexDocumento", con.conector);
			cmd.Parameters.AddWithValue("doc", codDocumento);
			cmd.Parameters.AddWithValue("alm", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsSerie();
					ser.CodSerie = Convert.ToInt32(dr.GetDecimal(0));
					ser.Serie = dr.GetString(1);
					ser.Numeracion = Convert.ToInt32(dr.GetDecimal(2));
					ser.NombreImpresora = dr.GetString(3);
					ser.PaperSize = dr.GetString(4);
					ser.PreImpreso = dr.GetBoolean(5);
				}
			}
			return ser;
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

	public bool Delete(int CodSerie)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarSerie", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codser", CodSerie);
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

	public clsSerie CargaSerie(int Codigo, int CodAlmacen)
	{
		clsSerie ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraSerie", con.conector);
			cmd.Parameters.AddWithValue("codser", Codigo);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsSerie();
					ser.CodSerie = Convert.ToInt32(dr.GetDecimal(0));
					ser.CodDocumento = Convert.ToInt32(dr.GetDecimal(1));
					ser.CodEmpresa = Convert.ToInt32(dr.GetDecimal(2));
					ser.CodAlmacen = Convert.ToInt32(dr.GetDecimal(3));
					ser.Serie = dr.GetString(4);
					ser.Inicio = Convert.ToInt32(dr.GetDecimal(5));
					ser.Fin = Convert.ToInt32(dr.GetDecimal(6));
					ser.Numeracion = Convert.ToInt32(dr.GetDecimal(7));
					ser.Estado = dr.GetBoolean(8);
					ser.CodUser = Convert.ToInt32(dr.GetDecimal(9));
					ser.FechaRegistro = dr.GetDateTime(10);
					ser.NombreImpresora = dr.GetString(11);
					ser.PaperSize = dr.GetString(12);
					ser.SerieImpresora = dr.GetString(13);
					ser.PreImpreso = dr.GetBoolean(14);
				}
			}
			return ser;
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

	public DataTable ListaSeries(int CodDoc, int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaSeries", con.conector);
			cmd.Parameters.AddWithValue("coddoc", CodDoc);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
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

	public clsSerie BuscaSerie(string Serie, int Documento, int Almacen)
	{
		clsSerie ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaSerie", con.conector);
			cmd.Parameters.AddWithValue("ser", Serie);
			cmd.Parameters.AddWithValue("doc", Documento);
			cmd.Parameters.AddWithValue("alm", Almacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsSerie();
					ser.CodSerie = Convert.ToInt32(dr.GetDecimal(0));
					ser.Serie = dr.GetString(1);
					ser.Numeracion = Convert.ToInt32(dr.GetDecimal(2));
					ser.NombreImpresora = dr.GetString(3);
					ser.PaperSize = dr.GetString(4);
					ser.PreImpreso = dr.GetBoolean(5);
				}
			}
			return ser;
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

	public int traeNumeracion(int codal, int coddoc)
	{
		int numero = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("traeNumeracion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codal", codal);
			cmd.Parameters.AddWithValue("coddoc", coddoc);
			numero = (int)cmd.ExecuteScalar();
			return numero;
		}
		catch (MySqlException)
		{
			return numero;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public int traeCodSerie(int codal, int coddoc)
	{
		int numero = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("traeCodSerie", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codal", codal);
			cmd.Parameters.AddWithValue("coddoc", coddoc);
			numero = (int)cmd.ExecuteScalar();
			return numero;
		}
		catch (MySqlException)
		{
			return numero;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public clsSerie CargaSerieEmpresa(int CodigoAlmacen, int codigoDoc)
	{
		clsSerie ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraSerieEmpresa", con.conector);
			cmd.Parameters.AddWithValue("codEmpresa_ex", CodigoAlmacen);
			cmd.Parameters.AddWithValue("codDocumento_ex", codigoDoc);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsSerie();
					ser.CodSerie = Convert.ToInt32(dr.GetDecimal(0));
					ser.CodDocumento = Convert.ToInt32(dr.GetDecimal(1));
					ser.CodEmpresa = Convert.ToInt32(dr.GetDecimal(2));
					ser.CodAlmacen = Convert.ToInt32(dr.GetDecimal(3));
					ser.Serie = dr.GetString(4);
					ser.Inicio = Convert.ToInt32(dr.GetDecimal(5));
					ser.Fin = Convert.ToInt32(dr.GetDecimal(6));
					ser.Numeracion = Convert.ToInt32(dr.GetDecimal(7));
					ser.Estado = dr.GetBoolean(8);
					ser.CodUser = Convert.ToInt32(dr.GetDecimal(9));
					ser.FechaRegistro = dr.GetDateTime(10);
					ser.NombreImpresora = dr.GetString(11);
					ser.PaperSize = dr.GetString(12);
					ser.SerieImpresora = dr.GetString(13);
					ser.PreImpreso = dr.GetBoolean(14);
				}
			}
			return ser;
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

	public clsSerie CargaSerieOV(int Sucursal, int CodAlmacen)
	{
		clsSerie ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaSerieOV", con.conector);
			cmd.Parameters.AddWithValue("codsucur", Sucursal);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsSerie();
					ser.CodSerie = Convert.ToInt32(dr.GetDecimal(0));
					ser.CodDocumento = Convert.ToInt32(dr.GetDecimal(1));
					ser.CodEmpresa = Convert.ToInt32(dr.GetDecimal(2));
					ser.CodAlmacen = Convert.ToInt32(dr.GetDecimal(3));
					ser.Serie = dr.GetString(4);
					ser.Inicio = Convert.ToInt32(dr.GetDecimal(5));
					ser.Fin = Convert.ToInt32(dr.GetDecimal(6));
					ser.Numeracion = Convert.ToInt32(dr.GetDecimal(7));
					ser.Estado = dr.GetBoolean(8);
					ser.CodUser = Convert.ToInt32(dr.GetDecimal(9));
					ser.FechaRegistro = dr.GetDateTime(10);
					ser.NombreImpresora = dr.GetString(11);
					ser.PaperSize = dr.GetString(12);
					ser.SerieImpresora = dr.GetString(13);
					ser.PreImpreso = dr.GetBoolean(14);
				}
			}
			return ser;
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

	public clsSerie CargaSeriePorDocumentoAsociado(int CodigoDocumento, int CodAlmacen, int CodDocumentoAsociado)
	{
		clsSerie ser = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraSeriePorDocumentoAsociado", con.conector);
			cmd.Parameters.AddWithValue("codigo_documento", CodigoDocumento);
			cmd.Parameters.AddWithValue("codigo_almacen", CodAlmacen);
			cmd.Parameters.AddWithValue("codigo_documento_asociado", CodDocumentoAsociado);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ser = new clsSerie();
					ser.CodSerie = Convert.ToInt32(dr.GetDecimal(0));
					ser.CodDocumento = Convert.ToInt32(dr.GetDecimal(1));
					ser.CodEmpresa = Convert.ToInt32(dr.GetDecimal(2));
					ser.CodAlmacen = Convert.ToInt32(dr.GetDecimal(3));
					ser.Serie = dr.GetString(4);
					ser.Inicio = Convert.ToInt32(dr.GetDecimal(5));
					ser.Fin = Convert.ToInt32(dr.GetDecimal(6));
					ser.Numeracion = Convert.ToInt32(dr.GetDecimal(7));
					ser.Estado = dr.GetBoolean(8);
					ser.CodUser = Convert.ToInt32(dr.GetDecimal(9));
					ser.FechaRegistro = dr.GetDateTime(10);
					ser.NombreImpresora = dr.GetString(11);
					ser.PaperSize = dr.GetString(12);
					ser.SerieImpresora = dr.GetString(13);
					ser.PreImpreso = dr.GetBoolean(14);
				}
			}
			return ser;
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
