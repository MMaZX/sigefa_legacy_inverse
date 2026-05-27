using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlCotizacion : ICotizacion
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsCotizacion cotizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codSu", cotizacion.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("codalma", cotizacion.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("tipocliente", cotizacion.TipoCliente);
			if (cotizacion.CodCliente != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codcli", cotizacion.CodCliente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codcli", null);
			}
			oParam = cmd.Parameters.AddWithValue("docref", cotizacion.DocRef);
			oParam = cmd.Parameters.AddWithValue("moneda", cotizacion.Moneda);
			oParam = cmd.Parameters.AddWithValue("codlista", cotizacion.CodListaPrecio);
			oParam = cmd.Parameters.AddWithValue("tipocambio", cotizacion.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechacotizacion", cotizacion.FechaCotizacion);
			oParam = cmd.Parameters.AddWithValue("auto", cotizacion.CodAutorizado);
			oParam = cmd.Parameters.AddWithValue("comentario", cotizacion.Comentario);
			oParam = cmd.Parameters.AddWithValue("vigencia", cotizacion.Vigencia);
			oParam = cmd.Parameters.AddWithValue("fechavigencia", cotizacion.FechaVigencia);
			oParam = cmd.Parameters.AddWithValue("bruto", cotizacion.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", cotizacion.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", cotizacion.Igv);
			oParam = cmd.Parameters.AddWithValue("total", cotizacion.Total);
			oParam = cmd.Parameters.AddWithValue("formapago", cotizacion.FormaPago);
			oParam = cmd.Parameters.AddWithValue("fechapago", cotizacion.FechaPago);
			oParam = cmd.Parameters.AddWithValue("codusu", cotizacion.CodUser);
			oParam = cmd.Parameters.AddWithValue("lugarentrega", cotizacion.SLugarEntrega);
			oParam = cmd.Parameters.AddWithValue("validadescuentos", cotizacion.ValidaDescuentos1);
			oParam = cmd.Parameters.AddWithValue("gananciamonto", cotizacion.Margendegananciamonto);
			oParam = cmd.Parameters.AddWithValue("gananciaporcentaje", cotizacion.Margendegananciaporcentaje);
			oParam = cmd.Parameters.AddWithValue("codserie", cotizacion.codserie);
			oParam = cmd.Parameters.AddWithValue("serie", cotizacion.serie);
			oParam = cmd.Parameters.AddWithValue("correlativo", cotizacion.correlativo);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			cotizacion.CodCotizacion = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool update(clsCotizacion cotizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codCotizacion", Convert.ToInt32(cotizacion.CodCotizacion));
			cmd.Parameters.AddWithValue("codalma", cotizacion.CodAlmacen);
			cmd.Parameters.AddWithValue("tipocliente", cotizacion.TipoCliente);
			if (cotizacion.CodCliente != 0)
			{
				cmd.Parameters.AddWithValue("codcli", cotizacion.CodCliente);
			}
			else
			{
				cmd.Parameters.AddWithValue("codcli", null);
			}
			cmd.Parameters.AddWithValue("moneda", cotizacion.Moneda);
			cmd.Parameters.AddWithValue("codtipo", cotizacion.DocRef);
			cmd.Parameters.AddWithValue("codlista", cotizacion.CodListaPrecio);
			cmd.Parameters.AddWithValue("tipocambio", cotizacion.TipoCambio);
			cmd.Parameters.AddWithValue("fechacotizacion", cotizacion.FechaCotizacion);
			cmd.Parameters.AddWithValue("auto", cotizacion.CodAutorizado);
			cmd.Parameters.AddWithValue("comentario", cotizacion.Comentario);
			cmd.Parameters.AddWithValue("vigencia", cotizacion.Vigencia);
			cmd.Parameters.AddWithValue("fechavigencia", cotizacion.FechaVigencia);
			cmd.Parameters.AddWithValue("bruto", cotizacion.MontoBruto);
			cmd.Parameters.AddWithValue("montodscto", cotizacion.MontoDscto);
			cmd.Parameters.AddWithValue("igv", cotizacion.Igv);
			cmd.Parameters.AddWithValue("total", cotizacion.Total);
			cmd.Parameters.AddWithValue("estado", cotizacion.Estado);
			cmd.Parameters.AddWithValue("formapago", cotizacion.FormaPago);
			cmd.Parameters.AddWithValue("fechapago", cotizacion.FechaPago);
			cmd.Parameters.AddWithValue("_lugarentrega", cotizacion.SLugarEntrega);
			cmd.Parameters.AddWithValue("_descue", cotizacion.ValidaDescuentos1);
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

	public bool updateAprobado(int CodCotizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaAprobadoCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcoti", CodCotizacion);
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

	public bool DuplicarCotizacion(int CodCotizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DuplicarCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcoti", CodCotizacion);
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

	public bool ValidarDescuento(int CodCotizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidarDescuentoCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcoti", CodCotizacion);
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

	public bool delete(int CodigoCotizacion, int codusuario, string mensaje)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcoti", CodigoCotizacion);
			cmd.Parameters.AddWithValue("codusuario", codusuario);
			cmd.Parameters.AddWithValue("mensaje", mensaje);
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

	public bool insertdetalle(clsDetalleCotizacion detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("descpro", detalle.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codCotizacion", detalle.CodCotizacion);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("cantidadp", detalle.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("importe", detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("diasentrega", detalle.Diasentrega);
			oParam = cmd.Parameters.AddWithValue("stockdisponible", detalle.Stockdisponible);
			oParam = cmd.Parameters.AddWithValue("codmarca", detalle.Codmarca);
			oParam = cmd.Parameters.AddWithValue("_aplicadescuento", detalle.Validadescuento);
			oParam = cmd.Parameters.AddWithValue("_stockseparado", detalle.Stockseparado);
			oParam = cmd.Parameters.AddWithValue("gananciamonto", detalle.Ganciamonto);
			oParam = cmd.Parameters.AddWithValue("gananciaporcentaje", detalle.Ganciaporcentaje);
			oParam = cmd.Parameters.AddWithValue("costototal", detalle.Totalcosto);
			oParam = cmd.Parameters.AddWithValue("cotizacion", detalle.Cotizacion);
			oParam = cmd.Parameters.AddWithValue("_codtipoprecio", detalle.codtipoprecio);
			oParam = cmd.Parameters.AddWithValue("_preciocompra", detalle.preciocompra);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleCotizacion = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool updatedetalle(clsDetalleCotizacion detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle.CodDetalleCotizacion);
			cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			cmd.Parameters.AddWithValue("descuento1", detalle.Descuento1);
			cmd.Parameters.AddWithValue("descuento2", detalle.Descuento2);
			cmd.Parameters.AddWithValue("descuento3", detalle.Descuento3);
			cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", detalle.Igv);
			cmd.Parameters.AddWithValue("importe", detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
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

	public bool deletedetalle(int CodigoDetalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", CodigoDetalle);
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

	public clsCotizacion CargaCotizacion(int CodCotizacion, int CodAlmacen)
	{
		clsCotizacion cotizacion = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codCotizacion", CodCotizacion);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cotizacion = new clsCotizacion();
					cotizacion.CodCotizacion = dr.GetString(0);
					cotizacion.CodAlmacen = dr.GetInt32(1);
					cotizacion.TipoCliente = dr.GetInt32(2);
					cotizacion.CodCliente = dr.GetInt32(3);
					cotizacion.DNI = dr.GetString(4);
					cotizacion.RUCCliente = dr.GetString(5);
					cotizacion.CodigoPersonalizado = dr.GetString(6);
					cotizacion.RazonSocialCliente = dr.GetString(7);
					cotizacion.Nombre = dr.GetString(8);
					cotizacion.Direccion = dr.GetString(9);
					cotizacion.Moneda = dr.GetInt32(10);
					cotizacion.TipoCambio = dr.GetDecimal(11);
					cotizacion.FechaCotizacion = dr.GetDateTime(12);
					cotizacion.Comentario = dr.GetString(13);
					cotizacion.MontoBruto = dr.GetDecimal(14);
					cotizacion.MontoDscto = dr.GetDecimal(15);
					cotizacion.Igv = dr.GetDecimal(16);
					cotizacion.Total = dr.GetDecimal(17);
					cotizacion.Estado = Convert.ToInt32(dr.GetDecimal(18));
					cotizacion.FormaPago = Convert.ToInt32(dr.GetString(19));
					cotizacion.FechaPago = dr.GetDateTime(20);
					cotizacion.CodUser = Convert.ToInt32(dr.GetDecimal(21));
					cotizacion.FechaRegistro = dr.GetDateTime(22);
					cotizacion.CodAutorizado = Convert.ToInt32(dr.GetDecimal(23));
					cotizacion.NombreAutorizado = dr.GetString(24);
					cotizacion.Vigencia = Convert.ToInt32(dr.GetDecimal(25));
					cotizacion.FechaVigencia = dr.GetDateTime(26);
					cotizacion.CodListaPrecio = Convert.ToInt32(dr.GetString(27));
					cotizacion.DocRef = Convert.ToInt32(dr.GetDecimal(28));
					cotizacion.SiglaDocRef = dr.GetString(29);
					cotizacion.CodSucursal = dr.GetInt32(30);
					cotizacion.Pendiente = dr.GetInt32(31);
					cotizacion.Anulado = dr.GetInt32(32);
					cotizacion.LineaCredito = dr.GetDecimal(33);
					cotizacion.LineaCreditoDisponible = dr.GetDecimal(34);
					cotizacion.LineaCreditoUso = dr.GetDecimal(35);
					cotizacion.SLugarEntrega = dr.GetString(36);
					cotizacion.ValidaDescuentos1 = dr.GetBoolean(37);
					cotizacion.Margendegananciamonto = dr.GetDecimal(38);
					cotizacion.Margendegananciaporcentaje = dr.GetDecimal(39);
					cotizacion.serie = dr.GetString(40);
					cotizacion.correlativo = dr.GetInt32(41);
				}
			}
			return cotizacion;
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

	public clsCotizacion BuscaCotizacion(string CodCotizacion, int CodAlmacen)
	{
		clsCotizacion cotizacion = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codpe", Convert.ToInt32(CodCotizacion));
			cmd.Parameters.AddWithValue("codalm", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cotizacion = new clsCotizacion();
					cotizacion.CodCotizacion = dr.GetString(0);
					cotizacion.TipoCliente = Convert.ToInt32(dr.GetString(1));
					cotizacion.CodCliente = Convert.ToInt32(dr.GetString(2));
					cotizacion.DNI = dr.GetString(3);
					cotizacion.RUCCliente = dr.GetString(4);
					cotizacion.CodigoPersonalizado = dr.GetString(5);
					cotizacion.RazonSocialCliente = dr.GetString(6);
					cotizacion.Nombre = dr.GetString(7);
					cotizacion.Direccion = dr.GetString(8);
					cotizacion.Moneda = Convert.ToInt32(dr.GetString(9));
					cotizacion.TipoCambio = dr.GetDecimal(10);
					cotizacion.FechaCotizacion = dr.GetDateTime(11);
					cotizacion.Comentario = dr.GetString(12);
					cotizacion.MontoBruto = dr.GetDecimal(13);
					cotizacion.MontoDscto = dr.GetDecimal(14);
					cotizacion.Igv = dr.GetDecimal(15);
					cotizacion.Total = dr.GetDecimal(16);
					cotizacion.Estado = Convert.ToInt32(dr.GetDecimal(17));
					cotizacion.FormaPago = Convert.ToInt32(dr.GetString(18));
					cotizacion.FechaPago = dr.GetDateTime(19);
					cotizacion.CodUser = Convert.ToInt32(dr.GetDecimal(20));
					cotizacion.FechaRegistro = dr.GetDateTime(21);
					cotizacion.Vigencia = Convert.ToInt32(dr.GetDecimal(22));
					cotizacion.FechaVigencia = dr.GetDateTime(23);
					cotizacion.CodListaPrecio = Convert.ToInt32(dr.GetString(24));
				}
			}
			return cotizacion;
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

	public clsDetalleCotizacion CargaDetalleCotizacion(int CodCotizacion, int CodAlmacen)
	{
		clsDetalleCotizacion cotizacion = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codCotizacion", CodCotizacion);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cotizacion = new clsDetalleCotizacion();
					cotizacion.CodDetalleCotizacion = dr.GetInt32(0);
					cotizacion.CodProducto = dr.GetInt32(1);
					cotizacion.Referencia = dr.GetString(2);
					cotizacion.Descripcion = dr.GetString(3);
					cotizacion.UnidadIngresada = dr.GetInt32(4);
					cotizacion.Unidad = dr.GetString(5);
					cotizacion.SerieLote = dr.GetString(6);
					cotizacion.Cantidad = dr.GetDouble(7);
					cotizacion.PrecioUnitario = dr.GetDouble(8);
					cotizacion.Subtotal = dr.GetDouble(9);
					cotizacion.Descuento1 = dr.GetDouble(10);
					cotizacion.Descuento2 = dr.GetDouble(11);
					cotizacion.Descuento3 = dr.GetDouble(12);
					cotizacion.MontoDescuento = dr.GetDouble(13);
					cotizacion.ValorVenta = dr.GetDouble(14);
					cotizacion.Igv = dr.GetDouble(15);
					cotizacion.Importe = dr.GetDouble(16);
					cotizacion.PrecioReal = dr.GetDouble(17);
					cotizacion.ValoReal = dr.GetDouble(18);
					cotizacion.CodUser = Convert.ToInt32(dr.GetDecimal(19));
					cotizacion.FechaRegistro = dr.GetDateTime(20);
				}
			}
			return cotizacion;
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

	public DataTable CargaDetalle(int CodCotizacion, int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcotizacion", CodCotizacion);
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

	public DataTable CargaDatosRecalcular(int Codproducto, int Codunidad, int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDatosRecalcularCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codproducto", Codproducto);
			cmd.Parameters.AddWithValue("codunidad", Codunidad);
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

	public DataTable CargaDetalleVenta(int CodCotizacion, int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleCotizacionNew", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcotizacion", CodCotizacion);
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

	public DataTable ListaCotizaciones(int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCotizaciones", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public DataTable ListaCotizacionesxVigente(int CodAlmacen, int estado, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCotizacionesxVigente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("esta", estado);
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

	public bool CotizacionesVencidas()
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCotizacionVencida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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
