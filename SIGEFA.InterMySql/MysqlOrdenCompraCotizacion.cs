using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlOrdenCompraCotizacion : IOrdenCompraCotizacion
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsOrdenCompraCotizacion ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaOrdenCompraCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codCotizacion", ingreso.codCotizacion);
			oParam = cmd.Parameters.AddWithValue("_codSucursal", ingreso.codSucursal);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", ingreso.codAlmacen);
			oParam = cmd.Parameters.AddWithValue("_codCliente", ingreso.codCliente);
			oParam = cmd.Parameters.AddWithValue("_codTipoDocumento", ingreso.codTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("_moneda", ingreso.moneda);
			oParam = cmd.Parameters.AddWithValue("_tipocambio", ingreso.tipocambio);
			oParam = cmd.Parameters.AddWithValue("_comentario", ingreso.comentario);
			oParam = cmd.Parameters.AddWithValue("_montodscto", ingreso.montodscto);
			oParam = cmd.Parameters.AddWithValue("_igv", ingreso.igv);
			oParam = cmd.Parameters.AddWithValue("_subtotal", ingreso.subtotal);
			oParam = cmd.Parameters.AddWithValue("_total", ingreso.total);
			oParam = cmd.Parameters.AddWithValue("_estado", ingreso.estado);
			oParam = cmd.Parameters.AddWithValue("_formapago", ingreso.formapago);
			oParam = cmd.Parameters.AddWithValue("_fecharegistro", ingreso.fecharegistro);
			oParam = cmd.Parameters.AddWithValue("_fechacotizacion", ingreso.fechacotizacion);
			oParam = cmd.Parameters.AddWithValue("_codUsuario", ingreso.codUsuario);
			oParam = cmd.Parameters.AddWithValue("_estadoproceso", ingreso.estadoproceso);
			oParam = cmd.Parameters.AddWithValue("_margenganciamonto", ingreso.margenganciamonto);
			oParam = cmd.Parameters.AddWithValue("_margengananciaporcentaje", ingreso.margengananciaporcentaje);
			oParam = cmd.Parameters.AddWithValue("_numerooccliente", ingreso.numerooccliente);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			ingreso.codOrdenCompra = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalle(clsDetalleOrdenCompraCotizacion detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleOrdenCompraCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codOrdenCompra", detalle.codOrdenCompra);
			oParam = cmd.Parameters.AddWithValue("_codProducto", detalle.codProducto);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", detalle.codAlmacen);
			oParam = cmd.Parameters.AddWithValue("_codUnidadMedida", detalle.codUnidadMedida);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.cantidad);
			oParam = cmd.Parameters.AddWithValue("_cantidadpendiente", detalle.cantidadpendiente);
			oParam = cmd.Parameters.AddWithValue("_preciounitario", detalle.preciounitario);
			oParam = cmd.Parameters.AddWithValue("_subtotal", detalle.subtotal);
			oParam = cmd.Parameters.AddWithValue("_igv", detalle.igv);
			oParam = cmd.Parameters.AddWithValue("_total", detalle.total);
			oParam = cmd.Parameters.AddWithValue("_montodscto", detalle.montodscto);
			oParam = cmd.Parameters.AddWithValue("_precioreal", detalle.precioreal);
			oParam = cmd.Parameters.AddWithValue("_estado", detalle.estado);
			oParam = cmd.Parameters.AddWithValue("_pendiente", detalle.pendiente);
			oParam = cmd.Parameters.AddWithValue("_codUser", detalle.codUser);
			oParam = cmd.Parameters.AddWithValue("_fecharegistro", detalle.fecharegistro);
			oParam = cmd.Parameters.AddWithValue("_stockactual", detalle.stockactual);
			oParam = cmd.Parameters.AddWithValue("_codmarca", detalle.codmarca);
			oParam = cmd.Parameters.AddWithValue("_margenganciamonto", detalle.margenganciamonto);
			oParam = cmd.Parameters.AddWithValue("_margengananciaporcentaje", detalle.margengananciaporcentaje);
			oParam = cmd.Parameters.AddWithValue("_costototal", detalle.costototal);
			oParam = cmd.Parameters.AddWithValue("_productocotizacion", detalle.productocotizacion);
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

	public bool updatecotizacion(clsDetalleCotizacion detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaMotivoCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_coddetalle", detalle.CodDetalleCotizacion);
			oParam = cmd.Parameters.AddWithValue("_motivo", detalle.motivo);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
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

	public bool Update(clsOrdenCompraCotizacion ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaParametroDescuento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_CodParametro", ingreso.codAlmacen);
			oParam = cmd.Parameters.AddWithValue("_Desde", ingreso.codCliente);
			oParam = cmd.Parameters.AddWithValue("_Hasta", ingreso.codCotizacion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
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

	public DataTable ListadaOrdenesCompra(int CodEmpresa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoParametrosDescuentos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_CodEmpresa", CodEmpresa);
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

	public clsOrdenCompraCotizacion CargaOrdenCompraCotizacion(int CodOrdenCompraCotizacion, int CodAlmacen)
	{
		clsOrdenCompraCotizacion ordencompracotiacion = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraOrdenCompraCotizacion", con.conector);
			cmd.Parameters.AddWithValue("_codOrdenCompra", CodOrdenCompraCotizacion);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ordencompracotiacion = new clsOrdenCompraCotizacion();
					ordencompracotiacion.codOrdenCompra = dr.GetInt32(0);
					ordencompracotiacion.codAlmacen = dr.GetInt32(1);
					ordencompracotiacion.codCliente = dr.GetInt32(2);
					ordencompracotiacion.RazonSocialCliente = dr.GetString(3);
					ordencompracotiacion.direccion = dr.GetString(4);
					ordencompracotiacion.moneda = dr.GetInt32(5);
					ordencompracotiacion.tipocambio = dr.GetDecimal(6);
					ordencompracotiacion.formapago = dr.GetInt32(7);
					ordencompracotiacion.fecharegistro = dr.GetDateTime(8);
					ordencompracotiacion.fechacotizacion = dr.GetDateTime(9);
					ordencompracotiacion.comentario = dr.GetString(10);
					ordencompracotiacion.montodscto = dr.GetDecimal(11);
					ordencompracotiacion.subtotal = dr.GetDecimal(12);
					ordencompracotiacion.igv = dr.GetDecimal(13);
					ordencompracotiacion.total = dr.GetDecimal(14);
					ordencompracotiacion.estado = dr.GetBoolean(15);
					ordencompracotiacion.codCotizacion = dr.GetInt32(16);
				}
			}
			return ordencompracotiacion;
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

	public DataTable CargaDetalleOrdenCompra(int CodOrdenCompra, int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleOrdenCompraCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codordencompra", CodOrdenCompra);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
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

	public DataTable ListaOrdenesCompraCotizacionesxVigente(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaOrdenesCompraCotizaciones", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
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
