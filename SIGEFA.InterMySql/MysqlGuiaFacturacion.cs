using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlGuiaFacturacion : IGuiaFacturacion
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private MySqlTransaction tra = null;

	private DataTable tabla = null;

	public bool Insert(clsGuiaFacturacion guia)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaGuiaFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codVenta", guia.codVenta);
			oParam = cmd.Parameters.AddWithValue("_codCliente", guia.codCliente);
			oParam = cmd.Parameters.AddWithValue("_codOrdenCompra", guia.codOrdenCompra);
			oParam = cmd.Parameters.AddWithValue("_codSerie", guia.codSerie);
			oParam = cmd.Parameters.AddWithValue("_numSerie", guia.numSerie);
			oParam = cmd.Parameters.AddWithValue("_correlativo", guia.correlativo);
			oParam = cmd.Parameters.AddWithValue("_codalmacen", guia.codalmacen);
			oParam = cmd.Parameters.AddWithValue("_estado", guia.estado);
			oParam = cmd.Parameters.AddWithValue("_fecharegistro", guia.fecharegistro);
			oParam = cmd.Parameters.AddWithValue("_fechaemision", guia.fechaemision);
			oParam = cmd.Parameters.AddWithValue("_codUsuario", guia.codUsuario);
			oParam = cmd.Parameters.AddWithValue("_tipocambio", guia.tipocambio);
			oParam = cmd.Parameters.AddWithValue("_codSucursal", guia.codSucursal);
			oParam = cmd.Parameters.AddWithValue("_codTipoDocumento", guia.codTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("_codMoneda", guia.codMoneda);
			oParam = cmd.Parameters.AddWithValue("_comentario", guia.comentario);
			oParam = cmd.Parameters.AddWithValue("_valorventa", guia.valorventa);
			oParam = cmd.Parameters.AddWithValue("_igv", guia.igv);
			oParam = cmd.Parameters.AddWithValue("_total", guia.total);
			oParam = cmd.Parameters.AddWithValue("_doctransporte", guia.doctransporte);
			oParam = cmd.Parameters.AddWithValue("_razonsocialtransporte", guia.razonsocialtransporte);
			oParam = cmd.Parameters.AddWithValue("_codmodotransporte", guia.codmodotransporte);
			oParam = cmd.Parameters.AddWithValue("_codmotivotransporte", guia.codmotivotransporte);
			oParam = cmd.Parameters.AddWithValue("_descripciontransporte", guia.descripciontransporte);
			oParam = cmd.Parameters.AddWithValue("_fechatransporte", guia.fechatransporte);
			oParam = cmd.Parameters.AddWithValue("_docconductor", guia.docconductor);
			oParam = cmd.Parameters.AddWithValue("_razonsocialcondutor", guia.razonsocialcondutor);
			oParam = cmd.Parameters.AddWithValue("_placa", guia.placa);
			oParam = cmd.Parameters.AddWithValue("_puntopartida", guia.puntopartida);
			oParam = cmd.Parameters.AddWithValue("_puntollegada", guia.puntollegada);
			oParam = cmd.Parameters.AddWithValue("_ubigueollegada", guia.ubigueollegada);
			oParam = cmd.Parameters.AddWithValue("_vehiculomenor", guia.vehiculomenor);
			oParam = cmd.Parameters.AddWithValue("_pesobruto", guia.pesobruto);
			oParam = cmd.Parameters.AddWithValue("_nrolicencia", guia.nrolicencia);
			oParam = cmd.Parameters.AddWithValue("_nropallets", guia.nropallets);
			oParam = cmd.Parameters.AddWithValue("_estadosunat", guia.estadosunat);
			oParam = cmd.Parameters.AddWithValue("_apellidoconductor", guia.apellidoconductor);
			oParam = cmd.Parameters.AddWithValue("_glosa", guia.glosa);
			oParam = cmd.Parameters.AddWithValue("_numoc", guia.NumOc);
			oParam = cmd.Parameters.AddWithValue("_flete", guia.Flete);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			guia.codGuia = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool InsertDetalle(clsDetalleGuiaFacturacion detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleGuiaFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codGuia", detalle.codGuia);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", detalle.codAlmacen);
			oParam = cmd.Parameters.AddWithValue("_codProducto", detalle.codProducto);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.cantidad);
			oParam = cmd.Parameters.AddWithValue("_preciounitario", detalle.preciounitario);
			oParam = cmd.Parameters.AddWithValue("_valorventa", detalle.valorventa);
			oParam = cmd.Parameters.AddWithValue("_igv", detalle.igv);
			oParam = cmd.Parameters.AddWithValue("_total", detalle.total);
			oParam = cmd.Parameters.AddWithValue("_estado", detalle.estado);
			oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.codUnidad);
			oParam = cmd.Parameters.AddWithValue("_unidad", detalle.unidad);
			oParam = cmd.Parameters.AddWithValue("_codMoneda", detalle.codMoneda);
			oParam = cmd.Parameters.AddWithValue("_fecharegistro", detalle.fecharegistro);
			oParam = cmd.Parameters.AddWithValue("_producto", detalle.producto);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.codDetalle = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool UpdateDetalle(clsDetalleGuiaFacturacion detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleGuiaFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codGuia", detalle.codGuia);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", detalle.codAlmacen);
			oParam = cmd.Parameters.AddWithValue("_codProducto", detalle.codProducto);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.cantidad);
			oParam = cmd.Parameters.AddWithValue("_preciounitario", detalle.preciounitario);
			oParam = cmd.Parameters.AddWithValue("_valorventa", detalle.valorventa);
			oParam = cmd.Parameters.AddWithValue("_igv", detalle.igv);
			oParam = cmd.Parameters.AddWithValue("_total", detalle.total);
			oParam = cmd.Parameters.AddWithValue("_estado", detalle.estado);
			oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.codUnidad);
			oParam = cmd.Parameters.AddWithValue("_unidad", detalle.unidad);
			oParam = cmd.Parameters.AddWithValue("_codMoneda", detalle.codMoneda);
			oParam = cmd.Parameters.AddWithValue("_fecharegistro", detalle.fecharegistro);
			oParam = cmd.Parameters.AddWithValue("_producto", detalle.producto);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool UpdateGuiaFacturacion(clsGuiaFacturacion guia)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaGuiaFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codGuia", guia.codGuia);
			oParam = cmd.Parameters.AddWithValue("_fechaemision", guia.fechaemision);
			oParam = cmd.Parameters.AddWithValue("_codUsuario", guia.codUsuario);
			oParam = cmd.Parameters.AddWithValue("_fechatransporte", guia.fechatransporte);
			oParam = cmd.Parameters.AddWithValue("_NumOc", guia.NumOc);
			oParam = cmd.Parameters.AddWithValue("_total", guia.total);
			oParam = cmd.Parameters.AddWithValue("_valorventa", guia.valorventa);
			oParam = cmd.Parameters.AddWithValue("_igv", guia.igv);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable ListaGuiasFacturacion(DateTime fecha1, DateTime fecha2, int codsucursal, bool estadosunat, bool respuestasunat)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaGuiasFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codsucur", codsucursal);
			cmd.Parameters.AddWithValue("_EstadoSunat", estadosunat);
			cmd.Parameters.AddWithValue("_RespuestaSunat", respuestasunat);
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

	public clsGuiaFacturacion ListaGuiaFacturacion(int codguia)
	{
		clsGuiaFacturacion guia = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ListaGuiaFacturacion", con.conector);
			cmd.Parameters.AddWithValue("codguia", codguia);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					guia = new clsGuiaFacturacion();
					guia.codGuia = dr.GetInt32(0);
					guia.codVenta = dr.GetInt32(1);
					guia.codCliente = dr.GetInt32(2);
					guia.codOrdenCompra = dr.GetInt32(3);
					guia.codSerie = dr.GetInt32(4);
					guia.numSerie = dr.GetString(5);
					guia.correlativo = dr.GetInt32(6);
					guia.codalmacen = dr.GetInt32(7);
					guia.estado = dr.GetBoolean(8);
					guia.fecharegistro = dr.GetDateTime(9);
					guia.fechaemision = dr.GetDateTime(10);
					guia.codUsuario = dr.GetInt32(11);
					guia.tipocambio = dr.GetDecimal(12);
					guia.codMoneda = dr.GetInt32(13);
					guia.comentario = dr.GetString(14);
					guia.valorventa = dr.GetDecimal(15);
					guia.igv = dr.GetDecimal(16);
					guia.total = dr.GetDecimal(17);
					guia.RazonSocial = dr.GetString(18);
					guia.Direccion = dr.GetString(19);
					guia.RucDni = dr.GetString(20);
					guia.NumOrdenCompra = dr.GetString(21);
					guia.codSucursal = dr.GetInt32(22);
					guia.Referencia = dr.GetString(23);
					guia.fechatransporte = dr.GetDateTime(24);
					guia.descripciontransporte = dr.GetString(25);
					guia.doctransporte = dr.GetString(26);
					guia.razonsocialtransporte = dr.GetString(27);
					guia.docconductor = dr.GetString(28);
					guia.razonsocialcondutor = dr.GetString(29);
					guia.placa = dr.GetString(30);
					guia.puntollegada = dr.GetString(31);
					guia.puntopartida = dr.GetString(32);
					guia.ubigueollegada = dr.GetString(33);
					guia.glosa = dr.GetString(34);
					guia.pesobruto = dr.GetDecimal(35);
					guia.nropallets = dr.GetInt32(36);
					guia.codTipoDocumento = dr.GetInt32(37);
					guia.codmodotransporte = dr.GetString(38);
					guia.vehiculomenor = dr.GetBoolean(39);
					guia.apellidoconductor = dr.GetString(40);
					guia.nrolicencia = dr.GetString(41);
					guia.codmotivotransporte = dr.GetString(42);
					guia.NroTicket = dr.GetString(43);
					guia.NumOc = dr.GetString(44);
				}
			}
			return guia;
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

	public DataTable ListaDetalleGuia(int codguia)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDetalleGuiaFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", codguia);
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

	public bool Anular(int codguia, int codusuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularGuiaFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codguia", codguia);
			cmd.Parameters.AddWithValue("codusuario", codusuario);
			using MySqlDataReader reader = cmd.ExecuteReader();
			if (reader.Read())
			{
				int totalFilasAfectadas = reader.GetInt32(0);
				return totalFilasAfectadas > 0;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public int VerificaGuia(int codfactura, int consulta)
	{
		int rpta = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaGuias", con.conector);
			cmd.Parameters.AddWithValue("codfactura", codfactura);
			cmd.Parameters.AddWithValue("consulta", consulta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rpta = dr.GetInt32(0);
				}
			}
			return rpta;
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

	public DataTable ListaModoTransporte()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaModoTransporte", con.conector);
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

	public DataTable ListaMotivotransporte()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaMotivoTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}
