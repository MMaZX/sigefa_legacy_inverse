using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlDocumentoIdentidad : IDocumentoIdentidad
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public DataTable ListaDocumentoIdentidad(int codigoTipoDocumento)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDocumentoIdentidad", con.conector);
			cmd.Parameters.AddWithValue("codigo_tipo_documento", codigoTipoDocumento);
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

	public clsDocumentoIdentidad MuestraDocumentoIdentidad(int codigoDocumentoIdentidad)
	{
		clsDocumentoIdentidad documentoIdentidad = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDocumentoIdentidad", con.conector);
			cmd.Parameters.AddWithValue("codigo_documento_identidad", codigoDocumentoIdentidad);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					documentoIdentidad = new clsDocumentoIdentidad();
					documentoIdentidad.CodDocumentoIdentidad = Convert.ToInt32(dr.GetDecimal(0));
					documentoIdentidad.CodigoSunat = Convert.ToInt32(dr.GetDecimal(1));
					documentoIdentidad.Descripcion = dr.GetString(2);
					documentoIdentidad.Longitud = Convert.ToInt32(dr.GetDecimal(3));
					documentoIdentidad.CodigoTipoDocumento = Convert.ToInt32(dr.GetDecimal(4));
				}
			}
			return documentoIdentidad;
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

	public clsDocumentoIdentidad ObtenerDocumentoIdentidadDeVenta(int codigoFacturaVenta)
	{
		clsDocumentoIdentidad documentoIdentidad = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ObtenerDocumentoIdentidadDeVenta", con.conector);
			cmd.Parameters.AddWithValue("codigo_factura_venta", codigoFacturaVenta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					documentoIdentidad = new clsDocumentoIdentidad();
					documentoIdentidad.CodDocumentoIdentidad = Convert.ToInt32(dr.GetDecimal(0));
					documentoIdentidad.CodigoSunat = Convert.ToInt32(dr.GetDecimal(1));
					documentoIdentidad.Descripcion = dr.GetString(2);
					documentoIdentidad.Longitud = Convert.ToInt32(dr.GetDecimal(3));
					documentoIdentidad.CodigoTipoDocumento = Convert.ToInt32(dr.GetDecimal(4));
				}
			}
			return documentoIdentidad;
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
