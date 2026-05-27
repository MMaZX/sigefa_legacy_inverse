using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlProducto : IProducto
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsProducto prod)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codUsuario_ex", prod.CodUsuario);
			if (prod.CodGrupo != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codGrupo_ex", prod.CodGrupo);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codGrupo_ex", null);
			}
			if (prod.CodLinea != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codLinea_ex", prod.CodLinea);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codLinea_ex", null);
			}
			oParam = cmd.Parameters.AddWithValue("codFamilia_ex", prod.CodFamilia);
			oParam = cmd.Parameters.AddWithValue("codUnidadMedida_ex", prod.CodUnidadMedida);
			oParam = cmd.Parameters.AddWithValue("codTipoArticulo_ex", prod.CodTipoArticulo);
			if (prod.CodMarca != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codMarca_ex", prod.CodMarca);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codMarca_ex", null);
			}
			oParam = cmd.Parameters.AddWithValue("codControlStock_ex", prod.CodControlStock);
			oParam = cmd.Parameters.AddWithValue("referencia_ex", prod.Referencia);
			oParam = cmd.Parameters.AddWithValue("descripcion_ex", prod.Descripcion);
			oParam = cmd.Parameters.AddWithValue("tipoimpuesto_ex", prod.TipoImpuesto);
			oParam = cmd.Parameters.AddWithValue("codsunat_ex", prod.CodSunat);
			oParam = cmd.Parameters.AddWithValue("detraccion_ex", prod.Detraccion);
			oParam = cmd.Parameters.AddWithValue("estado_ex", prod.Estado);
			oParam = cmd.Parameters.AddWithValue("comision_ex", prod.Comision);
			oParam = cmd.Parameters.AddWithValue("preciocatalogo_ex", prod.PrecioCatalogo);
			oParam = cmd.Parameters.AddWithValue("preciocompracatalago_ex", prod.PrecioCompra);
			oParam = cmd.Parameters.AddWithValue("maxPorcDescto_ex", prod.MaxPorcDesc);
			oParam = cmd.Parameters.AddWithValue("propeso", prod.Peso);
			oParam = cmd.Parameters.AddWithValue("_porcentajeretencion", prod.Porcentajerentencion);
			oParam = cmd.Parameters.AddWithValue("codigo_universal", prod.SCodUniversal);
			oParam = cmd.Parameters.AddWithValue("ubicacion_almacen", prod.SUbicacion);
			oParam = cmd.Parameters.AddWithValue("stock_minimo", prod.StockMinimo);
			oParam = cmd.Parameters.AddWithValue("venta_ticket_ex", prod.VentaConTicket);
			oParam = cmd.Parameters.AddWithValue("_icbper", prod.ICBPER);
			oParam = cmd.Parameters.AddWithValue("_flete", prod.Flete_estimado);
			oParam = cmd.Parameters.AddWithValue("_desestiva", prod.Desestiva);
			oParam = cmd.Parameters.AddWithValue("_estiva", prod.Estiva);
			oParam = cmd.Parameters.AddWithValue("_comision1", prod.Comision1);
			oParam = cmd.Parameters.AddWithValue("_comision2", prod.Comision2);
			oParam = cmd.Parameters.AddWithValue("_comision3", prod.Comision3);
			oParam = cmd.Parameters.AddWithValue("_gastosadmin", prod.GastosAdmin);
			oParam = cmd.Parameters.AddWithValue("_gastosadic", prod.GastosAdic);
			oParam = cmd.Parameters.AddWithValue("_proveedor1", prod.Proveedor1);
			oParam = cmd.Parameters.AddWithValue("_proveedor2", prod.Proveedor2);
			oParam = cmd.Parameters.AddWithValue("_proveedor3", prod.Proveedor3);
			oParam = cmd.Parameters.AddWithValue("_StockMaximo", prod.StockMaximo);
			oParam = cmd.Parameters.AddWithValue("_descontinuado", prod.descontinuado);
			oParam = cmd.Parameters.AddWithValue("_cotizacion", prod.Cotizacion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			prod.CodProducto = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool InsertProductoAlmacen(clsProducto prod)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaProductoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codusu", prod.CodUsuario);
			oParam = cmd.Parameters.AddWithValue("valorpromedio", prod.ValorProm);
			oParam = cmd.Parameters.AddWithValue("preciopromedio", prod.PrecioProm);
			oParam = cmd.Parameters.AddWithValue("recargo", prod.Recargo);
			oParam = cmd.Parameters.AddWithValue("precioventa", prod.PrecioVenta);
			oParam = cmd.Parameters.AddWithValue("oferta", prod.Oferta);
			oParam = cmd.Parameters.AddWithValue("descuento", prod.PDescuento);
			oParam = cmd.Parameters.AddWithValue("montodescuento", prod.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("preciooferta", prod.PrecioOferta);
			oParam = cmd.Parameters.AddWithValue("preciovariable", prod.PrecioVariable);
			oParam = cmd.Parameters.AddWithValue("maximodscto", prod.MaximoDscto);
			oParam = cmd.Parameters.AddWithValue("stockminimo", prod.StockMinimo);
			oParam = cmd.Parameters.AddWithValue("stockmaximo", prod.StockMaximo);
			oParam = cmd.Parameters.AddWithValue("stockreposicion", prod.StockReposicion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			prod.CodProductoAlmacen = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool InsertCaracteristica(clsCaracteristicaProducto carpro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCaracteristicaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", carpro.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codcar", carpro.CodCaracteristica);
			oParam = cmd.Parameters.AddWithValue("valor", carpro.Valor);
			oParam = cmd.Parameters.AddWithValue("codusu", carpro.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			carpro.CodCaracteristicaProductoNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool InsertNota(clsNotaProducto notpro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaNotaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", notpro.CodProducto);
			oParam = cmd.Parameters.AddWithValue("nota", notpro.Nota);
			oParam = cmd.Parameters.AddWithValue("codusu", notpro.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			notpro.CodNotaProducto = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsProducto prod)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", prod.CodProducto);
			if (prod.CodGrupo != 0)
			{
				cmd.Parameters.AddWithValue("codgru", prod.CodGrupo);
			}
			else
			{
				cmd.Parameters.AddWithValue("codgru", null);
			}
			if (prod.CodLinea != 0)
			{
				cmd.Parameters.AddWithValue("codlin", prod.CodLinea);
			}
			else
			{
				cmd.Parameters.AddWithValue("codlin", null);
			}
			cmd.Parameters.AddWithValue("codfam", prod.CodFamilia);
			if (prod.CodMarca != 0)
			{
				cmd.Parameters.AddWithValue("codmar", prod.CodMarca);
			}
			else
			{
				cmd.Parameters.AddWithValue("codmar", null);
			}
			cmd.Parameters.AddWithValue("coduni", prod.CodUnidadMedida);
			cmd.Parameters.AddWithValue("codtip", prod.CodTipoArticulo);
			cmd.Parameters.AddWithValue("control", prod.CodControlStock);
			cmd.Parameters.AddWithValue("referencia", prod.Referencia);
			cmd.Parameters.AddWithValue("descripcion", prod.Descripcion);
			cmd.Parameters.AddWithValue("tipoimpuesto_ex", prod.TipoImpuesto);
			cmd.Parameters.AddWithValue("codsunat_ex", prod.CodSunat);
			cmd.Parameters.AddWithValue("detraccion", prod.Detraccion);
			cmd.Parameters.AddWithValue("estado", prod.Estado);
			cmd.Parameters.AddWithValue("comision", prod.Comision);
			cmd.Parameters.AddWithValue("precioca", prod.PrecioCatalogo);
			cmd.Parameters.AddWithValue("maxPorcDesc", prod.MaxPorcDesc);
			cmd.Parameters.AddWithValue("propeso", prod.Peso);
			cmd.Parameters.AddWithValue("_procentajeretencion", prod.Porcentajerentencion);
			cmd.Parameters.AddWithValue("codigo_universal", prod.SCodUniversal);
			cmd.Parameters.AddWithValue("ubicacion_almacen", prod.SUbicacion);
			cmd.Parameters.AddWithValue("stock_minimo", prod.StockMinimo);
			cmd.Parameters.AddWithValue("venta_ticket_ex", prod.VentaConTicket);
			cmd.Parameters.AddWithValue("_icbper", prod.ICBPER);
			cmd.Parameters.AddWithValue("_flete", prod.Flete_estimado);
			cmd.Parameters.AddWithValue("_desestiva", prod.Desestiva);
			cmd.Parameters.AddWithValue("_estiva", prod.Estiva);
			cmd.Parameters.AddWithValue("_comision1", prod.Comision1);
			cmd.Parameters.AddWithValue("_comision2", prod.Comision2);
			cmd.Parameters.AddWithValue("_comision3", prod.Comision3);
			cmd.Parameters.AddWithValue("_gastosadmin", prod.GastosAdmin);
			cmd.Parameters.AddWithValue("_gastosadic", prod.GastosAdic);
			cmd.Parameters.AddWithValue("_proveedor1", prod.Proveedor1);
			cmd.Parameters.AddWithValue("_proveedor2", prod.Proveedor2);
			cmd.Parameters.AddWithValue("_proveedor3", prod.Proveedor3);
			cmd.Parameters.AddWithValue("_StockMaximo", prod.StockMaximo);
			cmd.Parameters.AddWithValue("_descontinuado", prod.descontinuado);
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

	public bool UpdateMasivo(DataTable prods)
	{
		try
		{
			con.conectarBD();
			clsProducto prod = new clsProducto();
			cmd = new MySqlCommand("actualizaProductoMasivo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("codpro", MySqlDbType.Int32).SourceColumn = "codpro";
			cmd.Parameters.Add("codgru", MySqlDbType.Int32).SourceColumn = "codgru";
			cmd.Parameters.Add("codlin", MySqlDbType.Int32).SourceColumn = "codlin";
			cmd.Parameters.Add("codfam", MySqlDbType.Int32).SourceColumn = "codfam";
			cmd.Parameters.Add("codmar", MySqlDbType.Int32).SourceColumn = "codmar";
			cmd.Parameters.Add("precioca", MySqlDbType.Double).SourceColumn = "precioca";
			cmd.Parameters.Add("preciocompra", MySqlDbType.Double).SourceColumn = "preciocompra";
			MySqlDataAdapter da = new MySqlDataAdapter();
			cmd.CommandTimeout = 250000000;
			da.InsertCommand = cmd;
			da.UpdateCommand = cmd;
			cmd.UpdatedRowSource = UpdateRowSource.None;
			da.UpdateBatchSize = 100;
			int records = da.Update(prods);
			return true;
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

	public bool UpdateProductoAlmacen(clsProducto prod)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaProductoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", prod.CodProducto);
			cmd.Parameters.AddWithValue("codalma", prod.CodAlmacen);
			cmd.Parameters.AddWithValue("valorprom", prod.ValorProm);
			cmd.Parameters.AddWithValue("precioprom", prod.PrecioProm);
			cmd.Parameters.AddWithValue("recargo", prod.Recargo);
			cmd.Parameters.AddWithValue("valorventa", prod.ValorVenta);
			cmd.Parameters.AddWithValue("precioventa", prod.PrecioVenta);
			cmd.Parameters.AddWithValue("oferta", prod.Oferta);
			cmd.Parameters.AddWithValue("descuento", prod.PDescuento);
			cmd.Parameters.AddWithValue("montodescuento", prod.MontoDscto);
			cmd.Parameters.AddWithValue("preciooferta", prod.PrecioOferta);
			cmd.Parameters.AddWithValue("preciovariable", prod.PrecioVariable);
			cmd.Parameters.AddWithValue("maximodscto", prod.MaximoDscto);
			cmd.Parameters.AddWithValue("stockminimo", prod.StockMinimo);
			cmd.Parameters.AddWithValue("stockmaximo", prod.StockMaximo);
			cmd.Parameters.AddWithValue("stockreposicion", prod.StockReposicion);
			cmd.Parameters.AddWithValue("tipoimpuesto_ex", prod.TipoImpuesto);
			cmd.Parameters.AddWithValue("codsunat_ex", prod.CodSunat);
			cmd.Parameters.AddWithValue("detraccion", prod.Detraccion);
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

	public bool Delete(int CodProducto)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", CodProducto);
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

	public bool DeleteProductoAlmacen(int CodProductoAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarProductoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", CodProductoAlmacen);
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

	public bool DeleteCaracteristica(int CodCarPro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarCaracteristicaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcarpro", CodCarPro);
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

	public bool DeleteNota(int CodNotaProducto)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarNotaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", CodNotaProducto);
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

	public clsProducto ListaTotalprod2(int CodPro, int codAlmacen, int CodUnidad)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ListaTotalprod2", con.conector);
			cmd.Parameters.AddWithValue("codproducto", CodPro);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codunidad", CodUnidad);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.StockMaximo = (dr.IsDBNull(0) ? 0.0 : Convert.ToDouble(dr.GetString(0)));
				}
			}
			return pro;
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

	public clsProducto CargaProducto(int CodPro, int CodAlm)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProducto", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.CodProducto = dr.GetInt32(0);
					pro.Referencia = dr.GetString(1);
					pro.Descripcion = dr.GetString(2);
					pro.TipoImpuesto = dr.GetInt32(3);
					pro.Detraccion = dr.GetBoolean(4);
					pro.CodTipoArticulo = dr.GetInt32(5);
					pro.CodFamilia = dr.GetInt32(6);
					pro.CodLinea = dr.GetInt32(7);
					pro.CodGrupo = dr.GetInt32(8);
					pro.CodMarca = dr.GetInt32(9);
					pro.CodControlStock = dr.GetInt32(10);
					pro.CodUnidadMedida = dr.GetInt32(11);
					pro.CodUsuario = dr.GetInt32(13);
					pro.UltimaModificacion = dr.GetDateTime(14);
					pro.FechaRegistro = dr.GetDateTime(15);
					pro.CodProductoAlmacen = dr.GetInt32(16);
					pro.CodAlmacen = dr.GetInt32(17);
					pro.PrecioProm = dr.GetDecimal(18);
					pro.ValorProm = Convert.ToDouble(dr.GetString(19));
					pro.Recargo = Convert.ToDouble(dr.GetString(20));
					pro.ValorVenta = Convert.ToDouble(dr.GetString(21));
					pro.PrecioVenta = Convert.ToDouble(dr.GetString(22));
					pro.PDescuento = dr.GetDouble(24);
					pro.MontoDscto = dr.GetDouble(25);
					pro.PrecioOferta = dr.GetDouble(26);
					pro.MaximoDscto = dr.GetDouble(27);
					pro.StockActual = dr.GetDouble(30);
					pro.StockDisponible = dr.GetDecimal(31);
					pro.StockMaximo = dr.GetDouble(32);
					pro.StockReposicion = dr.GetDouble(34);
					pro.Comision = Convert.ToDecimal(dr.GetString(35));
					pro.PrecioCatalogo = Convert.ToDecimal(dr.GetString(36));
					pro.Estado = dr.GetBoolean(12);
					pro.Oferta = dr.GetBoolean(23);
					pro.CodSunat = dr.GetString(28);
					pro.PrecioVariable = dr.GetBoolean(29);
					pro.StockFuturo = dr.GetDecimal(38);
					pro.StockPorRecibir = dr.GetDecimal(37);
					pro.MaxPorcDesc = dr.GetDecimal(39);
					pro.Peso = dr.GetDecimal(40);
					pro.Porcentajerentencion = dr.GetDecimal(41);
					pro.SCodUniversal = dr.GetString(42);
					pro.SUbicacion = dr.GetString(43);
					pro.StockMinimo = dr.GetDouble(44);
					pro.VentaConTicket = dr.GetBoolean(45);
					pro.ICBPER = Convert.ToBoolean(dr.GetBoolean(48));
					pro.Flete_estimado = dr.GetDecimal(49);
					pro.Desestiva = dr.GetDecimal(50);
					pro.Estiva = dr.GetDecimal(51);
					pro.Comision1 = dr.GetDecimal(52);
					pro.Comision2 = dr.GetDecimal(53);
					pro.Comision3 = dr.GetDecimal(54);
					pro.GastosAdmin = dr.GetDecimal(55);
					pro.GastosAdic = dr.GetDecimal(56);
					pro.Proveedor1 = dr.GetInt32(57);
					pro.Proveedor2 = dr.GetInt32(58);
					pro.Proveedor3 = dr.GetInt32(59);
					pro.descontinuado = dr.GetBoolean(60);
				}
			}
			return pro;
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

	public clsProducto CargaProductoCotizacion(int CodPro, int CodAlm)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.CodProducto = dr.GetInt32(0);
					pro.Referencia = dr.GetString(1);
					pro.Descripcion = dr.GetString(2);
					pro.TipoImpuesto = dr.GetInt32(3);
					pro.Detraccion = dr.GetBoolean(4);
					pro.CodTipoArticulo = dr.GetInt32(5);
					pro.CodFamilia = dr.GetInt32(6);
					pro.CodLinea = dr.GetInt32(7);
					pro.CodGrupo = dr.GetInt32(8);
					pro.CodMarca = dr.GetInt32(9);
					pro.CodControlStock = dr.GetInt32(10);
					pro.CodUnidadMedida = dr.GetInt32(11);
					pro.CodUsuario = dr.GetInt32(13);
					pro.UltimaModificacion = dr.GetDateTime(14);
					pro.FechaRegistro = dr.GetDateTime(15);
					pro.CodProductoAlmacen = dr.GetInt32(16);
					pro.CodAlmacen = dr.GetInt32(17);
					pro.PrecioProm = dr.GetDecimal(18);
					pro.ValorProm = Convert.ToDouble(dr.GetString(19));
					pro.Recargo = Convert.ToDouble(dr.GetString(20));
					pro.ValorVenta = Convert.ToDouble(dr.GetString(21));
					pro.PrecioVenta = Convert.ToDouble(dr.GetString(22));
					pro.PDescuento = dr.GetDouble(24);
					pro.MontoDscto = dr.GetDouble(25);
					pro.PrecioOferta = dr.GetDouble(26);
					pro.MaximoDscto = dr.GetDouble(27);
					pro.StockActual = dr.GetDouble(30);
					pro.StockDisponible = dr.GetDecimal(31);
					pro.StockMaximo = dr.GetDouble(32);
					pro.StockReposicion = dr.GetDouble(34);
					pro.Comision = Convert.ToDecimal(dr.GetString(35));
					pro.PrecioCatalogo = Convert.ToDecimal(dr.GetString(36));
					pro.Estado = dr.GetBoolean(12);
					pro.Oferta = dr.GetBoolean(23);
					pro.CodSunat = dr.GetString(28);
					pro.PrecioVariable = dr.GetBoolean(29);
					pro.StockFuturo = dr.GetDecimal(38);
					pro.StockPorRecibir = dr.GetDecimal(37);
					pro.MaxPorcDesc = dr.GetDecimal(39);
					pro.Peso = dr.GetDecimal(40);
					pro.Porcentajerentencion = dr.GetDecimal(41);
					pro.SCodUniversal = dr.GetString(42);
					pro.SUbicacion = dr.GetString(43);
					pro.StockMinimo = dr.GetDouble(44);
					pro.VentaConTicket = dr.GetBoolean(45);
					pro.ICBPER = Convert.ToBoolean(dr.GetBoolean(48));
					pro.Flete_estimado = dr.GetDecimal(49);
					pro.Desestiva = dr.GetDecimal(50);
					pro.Estiva = dr.GetDecimal(51);
					pro.Comision1 = dr.GetDecimal(52);
					pro.Comision2 = dr.GetDecimal(53);
					pro.Comision3 = dr.GetDecimal(54);
					pro.GastosAdmin = dr.GetDecimal(55);
					pro.GastosAdic = dr.GetDecimal(56);
					pro.Proveedor1 = dr.GetInt32(57);
					pro.Proveedor2 = dr.GetInt32(58);
					pro.Proveedor3 = dr.GetInt32(59);
				}
			}
			return pro;
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

	public clsProducto CargaProductoDetalle(int CodPro, int CodAlm, int Caso, int CodLista, int totalstock)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoDetalle", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.Parameters.AddWithValue("caso", Caso);
			cmd.Parameters.AddWithValue("lista", CodLista);
			cmd.Parameters.AddWithValue("totalstock", totalstock);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					if (Caso == 1)
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.SCodUniversal = dr.GetString(2);
						pro.SUbicacion = dr.GetString(3);
						pro.Descripcion = dr.GetString(4);
						pro.StockDisponible = dr.GetDecimal(5);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(6));
						pro.UnidadDescrip = dr.GetString(7);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(8));
						pro.CodSunat = dr.GetString(9);
						pro.TipoImpuesto = dr.GetInt32(10);
						pro.MaxPorcDesc = dr.GetDecimal(11);
						pro.StockMinimo = Convert.ToDouble(dr.GetDecimal(13));
					}
					else
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.SCodUniversal = dr.GetString(2);
						pro.SUbicacion = dr.GetString(3);
						pro.Descripcion = dr.GetString(4);
						pro.StockDisponible = dr.GetDecimal(5);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(6));
						pro.UnidadDescrip = dr.GetString(7);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(8));
						pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(9));
						pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(10));
						pro.PrecioVariable = dr.GetBoolean(11);
						pro.Oferta = dr.GetBoolean(12);
						pro.PDescuento = Convert.ToDouble(dr.GetDecimal(13));
						pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(14));
						pro.CodSunat = dr.GetString(15);
						pro.TipoImpuesto = dr.GetInt32(16);
						pro.MaxPorcDesc = dr.GetDecimal(20);
						pro.CodFamilia = dr.GetInt32(21);
						pro.StockMinimo = Convert.ToDouble(dr.GetDecimal(22));
						pro.CodTipoArticulo = dr.GetInt32(23);
						pro.codli = Convert.ToInt32(dr["codLinea"]);
						pro.codfami = Convert.ToInt32(dr["codFamilia"]);
					}
				}
			}
			return pro;
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

	public clsProducto CargaProductoDetalleCotizacion(int CodPro, int CodCotizacion, int CodAlm, int Caso, int CodLista)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoDetalleCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("CodCotizacion", CodCotizacion);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.Parameters.AddWithValue("caso", Caso);
			cmd.Parameters.AddWithValue("lista", CodLista);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					if (Caso == 1)
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.SCodUniversal = dr.GetString(2);
						pro.SUbicacion = dr.GetString(3);
						pro.Descripcion = dr.GetString(4);
						pro.StockDisponible = dr.GetDecimal(5);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(6));
						pro.UnidadDescrip = dr.GetString(7);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(8));
						pro.CodSunat = dr.GetString(9);
						pro.TipoImpuesto = dr.GetInt32(10);
						pro.MaxPorcDesc = dr.GetDecimal(11);
						pro.StockMinimo = Convert.ToDouble(dr.GetDecimal(13));
						pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(17));
						pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(18));
					}
					else
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.SCodUniversal = dr.GetString(2);
						pro.SUbicacion = dr.GetString(3);
						pro.Descripcion = dr.GetString(4);
						pro.StockDisponible = dr.GetDecimal(5);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(6));
						pro.UnidadDescrip = dr.GetString(7);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(8));
						pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(9));
						pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(10));
						pro.PrecioVariable = dr.GetBoolean(11);
						pro.Oferta = dr.GetBoolean(12);
						pro.PDescuento = Convert.ToDouble(dr.GetDecimal(13));
						pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(14));
						pro.CodSunat = dr.GetString(15);
						pro.TipoImpuesto = dr.GetInt32(16);
						pro.MaxPorcDesc = dr.GetDecimal(20);
						pro.CodFamilia = dr.GetInt32(21);
						pro.StockMinimo = Convert.ToDouble(dr.GetDecimal(22));
						pro.CodTipoArticulo = dr.GetInt32(23);
						pro.codli = Convert.ToInt32(dr["codLinea"]);
						pro.codfami = Convert.ToInt32(dr["codFamilia"]);
						pro.codmarca = Convert.ToInt32(dr["codMarca"]);
						pro.Cotizacion = dr.GetBoolean(28);
						pro.CodAlmacen = dr.GetInt32(29);
					}
				}
			}
			return pro;
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

	public clsProducto CargaProductoDetalleSinAfectarStock(int CodPro, int CodAlm, int Caso, int CodLista)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaProductoDetalleSinAfectarStock", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.Parameters.AddWithValue("caso", Caso);
			cmd.Parameters.AddWithValue("lista", CodLista);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
					pro.Referencia = dr.GetString(1);
					pro.SCodUniversal = dr.GetString(2);
					pro.SUbicacion = dr.GetString(3);
					pro.Descripcion = dr.GetString(4);
					pro.StockDisponible = dr.GetDecimal(5);
					pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(6));
					pro.UnidadDescrip = dr.GetString(7);
					pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(8));
					pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(9));
					pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(10));
					pro.PrecioVariable = dr.GetBoolean(11);
					pro.Oferta = dr.GetBoolean(12);
					pro.PDescuento = Convert.ToDouble(dr.GetDecimal(13));
					pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(14));
					pro.CodSunat = dr.GetString(15);
					pro.TipoImpuesto = dr.GetInt32(16);
					pro.MaxPorcDesc = dr.GetDecimal(20);
					pro.CodFamilia = dr.GetInt32(22);
					pro.StockMinimo = Convert.ToDouble(dr.GetDecimal(23));
					pro.CodTipoArticulo = dr.GetInt32(24);
				}
			}
			return pro;
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

	public clsProducto CargaProductoDetalle1(int CodPro, int CodAlm, int Caso, int CodLista)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoDetalle1", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.Parameters.AddWithValue("caso", Caso);
			cmd.Parameters.AddWithValue("lista", CodLista);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					if (Caso == 1)
					{
						pro = new clsProducto();
						pro.CodProducto = dr.GetInt32(0);
						pro.Referencia = dr.GetString(1);
						pro.Descripcion = dr.GetString(2);
						pro.StockDisponible = Convert.ToDecimal(dr.GetDecimal(3));
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
						pro.UnidadDescrip = dr.GetString(5);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
						pro.ConIgv = dr.GetBoolean(7);
						pro.TipoImpuesto = dr.GetInt32(8);
						pro.ValorProm = dr.GetDouble(11);
						pro.PrecioCompra = dr.GetDecimal(12);
					}
					else
					{
						pro = new clsProducto();
						pro.CodProducto = dr.GetInt32(0);
						pro.Referencia = dr.GetString(1);
						pro.Descripcion = dr.GetString(2);
						pro.StockDisponible = dr.GetDecimal(3);
						pro.CodUnidadMedida = dr.GetInt32(4);
						pro.UnidadDescrip = dr.GetString(5);
						pro.CodControlStock = dr.GetInt32(6);
						pro.PrecioVenta = dr.GetDouble(7);
						pro.PrecioVariable = dr.GetBoolean(8);
						pro.ConIgv = dr.GetBoolean(9);
						pro.TipoImpuesto = dr.GetInt32(10);
						pro.ValorProm = dr.GetDouble(11);
						pro.PrecioProm = dr.GetDecimal(12);
					}
				}
			}
			return pro;
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

	public clsProducto CargaDatosProductoOrden(int CodPro, int CodAlm, int codusu, decimal cant)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaDatosProductoOrden", con.conector);
			cmd.Parameters.AddWithValue("alma", CodAlm);
			cmd.Parameters.AddWithValue("usu", codusu);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("cant", cant);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.Porllegar = Convert.ToInt32(dr.GetDecimal(0));
					pro.PorAtender = Convert.ToInt32(dr.GetDecimal(1));
					pro.PorCompletar = Convert.ToInt32(dr.GetDecimal(2));
				}
			}
			return pro;
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

	public clsProducto CargaDatosProductoAgrupados(int CodPro)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaDatosProductoAgrapadosCabecera", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.nombreplantilla = dr.GetString(0);
					pro.descripcionp = dr.GetString(1);
					pro.codmarca = dr.GetInt32(2);
					pro.codproveedor = dr.GetInt32(3);
					pro.codfamilia = dr.GetInt32(4);
					pro.FechaRegistro = dr.GetDateTime(5);
				}
			}
			return pro;
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

	public clsProducto CargaProductoDetalleR(string Referencia, int CodAlm, int Caso, int Lista)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoDetalleR", con.conector);
			cmd.Parameters.AddWithValue("refe", Referencia);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.Parameters.AddWithValue("caso", Caso);
			cmd.Parameters.AddWithValue("lista", Lista);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					if (Caso == 1)
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.Descripcion = dr.GetString(2);
						pro.StockDisponible = dr.GetDecimal(3);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
						pro.UnidadDescrip = dr.GetString(5);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
						pro.CodSunat = dr.GetString(7);
						pro.TipoImpuesto = dr.GetInt32(8);
						pro.MaxPorcDesc = dr.GetDecimal(9);
					}
					else
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.Descripcion = dr.GetString(2);
						pro.StockDisponible = dr.GetDecimal(3);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
						pro.UnidadDescrip = dr.GetString(5);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
						pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(7));
						pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(8));
						pro.PrecioVariable = dr.GetBoolean(9);
						pro.Oferta = dr.GetBoolean(10);
						pro.PDescuento = Convert.ToDouble(dr.GetDecimal(11));
						pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(12));
						pro.CodSunat = dr.GetString(13);
						pro.TipoImpuesto = dr.GetInt32(14);
						pro.MaxPorcDesc = dr.GetDecimal(18);
					}
				}
			}
			return pro;
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

	public DataTable ListaProductos(int nivel, int codigo, int codalmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaProductos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("nivel", nivel);
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.Parameters.AddWithValue("codalm", codalmacen);
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

	public DataTable CatalogoProductos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CatalogoProductosII", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", 1);
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

	public DataTable CatalogoProductosCotizacion()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CatalogoProductosCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", 1);
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

	public bool ActualizaEstadoProductoCotizacion(int CodProducto)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEstadoProductoCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", CodProducto);
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

	public DataTable StockProductoAlmacenes(int codempre, int codpro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("StockProductoxAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codpro);
			cmd.Parameters.AddWithValue("codempre", codempre);
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

	public DataTable ListaProductosReporte(int codalmacen, int Tipo, int Inicio)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaProductosReporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("inicio", Inicio);
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

	public DataTable RelacionProductosIngreso(int Tipo, int codalma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosIngreso", con.conector);
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codalma);
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

	public DataTable RelacionIngresoPorProveedor(int Tipo, int codalma, int codproveedor)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosIngresoPorProveedor", con.conector);
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codprov", codproveedor);
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

	public DataTable RelacionProductosSalida(int Tipo, int codalmacen, int codlista)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("codlista", codlista);
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

	public DataTable RelacionProductosSalidaTodo(int Tipo, int codalmacen, int codlista)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosSalidaTodo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("codlista", codlista);
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

	public DataTable RelacionProductosParaRequerimientoAlmacen(int Tipo, int codalmacen, int codlista)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosParaReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("codlista", codlista);
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

	public DataTable RelacionProductosSalidaTodoSinStock(int Tipo, int codalmacen, int codlista)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosSalidaTodoSinStock", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("codlista", codlista);
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

	public DataTable RelacionSalidaSinAfectarStock(int Tipo, int codalmacen, int codlista)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionSalidaSinAfectarStock", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("codlista", codlista);
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

	public DataTable ListaCaracteristicas(int codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCaracteristicaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codigo);
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

	public DataTable ListaNotas(int codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotasProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codigo);
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

	public DataTable BuscaProductos(int Criterio, string Filtro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("FiltraProductos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@criterio", Criterio);
			cmd.Parameters.AddWithValue("@filtro", Filtro);
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

	public DataTable ArbolProductos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaArbolProductos", con.conector);
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

	public DataTable MuestraProductosProveedor(int codProducto, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductosProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public clsProducto MuestraProductosTransferencia(int codProducto, int codAlmacen)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.ValorProm = Convert.ToDouble(dr.GetDecimal(0));
					pro.ValorPromsoles = Convert.ToDecimal(dr.GetDecimal(1));
					pro.PrecioProm = dr.GetDecimal(2);
					pro.StockActual = Convert.ToDouble(dr.GetDecimal(3));
				}
			}
			return pro;
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

	public clsProducto MuestraProductosTransferencia_nuevo(int codProducto, int codAlmacen)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.ValorProm = Convert.ToDouble(dr.GetDecimal(0));
					pro.ValorPromsoles = Convert.ToDecimal(dr.GetDecimal(1));
					pro.PrecioProm = dr.GetDecimal(2);
					pro.StockActual = Convert.ToDouble(dr.GetDecimal(3));
					pro.Cantidad = Convert.ToInt32(dr.GetInt32(4));
				}
			}
			return pro;
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

	public DataTable RelacionProductosCotizacion(int Tipo, int codAlmacen, int codlista, int todos)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", Tipo);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codlista", codlista);
			cmd.Parameters.AddWithValue("todos", todos);
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

	public decimal CargaPrecioProducto(int CodPro, int CodAlm, int codmon)
	{
		decimal Precio = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaPrecioProducto", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("codalma", CodAlm);
			cmd.Parameters.AddWithValue("mon", codmon);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					Precio = Convert.ToDecimal(dr.GetDecimal(0));
				}
			}
			return Precio;
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

	public bool ActualizarPrecioVentaProductoPorUnidad(int codigoProducto, int codigoUnidad, int codigoAlmacen, decimal nuevoPrecioVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPrecioVentaProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad", codigoUnidad);
			cmd.Parameters.AddWithValue("codigo_almacen", codigoAlmacen);
			cmd.Parameters.AddWithValue("nuevo_precio_venta", nuevoPrecioVenta);
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

	public DataTable MuestraStockAlmacenes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("consultadinamicastock", con.conector);
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

	public DataTable MuestraStockAlmacenesPendientes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("consultadinamicastockPendiente", con.conector);
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

	public bool InsertUnidad(clsUnidadEquivalente uni, int coti)
	{
		try
		{
			con.conectarBD();
			if (coti != 1)
			{
				cmd = new MySqlCommand("GuardaUnidadEquivalente", con.conector);
			}
			else
			{
				cmd = new MySqlCommand("GuardaUnidadEquivalente_cotizacion", con.conector);
			}
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", uni.CodProducto);
			oParam = cmd.Parameters.AddWithValue("coduni", uni.CodUnidad);
			oParam = cmd.Parameters.AddWithValue("factor", uni.Factor);
			oParam = cmd.Parameters.AddWithValue("codUndEqui", uni.CodEquivalente);
			oParam = cmd.Parameters.AddWithValue("codTipo", uni.Tipo);
			oParam = cmd.Parameters.AddWithValue("precio", uni.Precio);
			oParam = cmd.Parameters.AddWithValue("codAlmacen", uni.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codusu", uni.CodUser);
			oParam = cmd.Parameters.AddWithValue("compra_venta", uni.CompraVenta);
			oParam = cmd.Parameters.AddWithValue("codMoneda_ex", uni.ICodMoneda);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			uni.CodUnidadEquivalente = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool UpdateUnidad(clsUnidadEquivalente uni)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaUnidadEquivalente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("factor", uni.Factor);
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

	public bool DeleteUnidad(int CodUnidad)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarUnidadEquivalente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coduni", CodUnidad);
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

	public bool creaProductoAlmacenMasivo(int codProducto, int codUsuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("creaciondeproductoalmacenmasivo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codProd", codProducto);
			cmd.Parameters.AddWithValue("_codUser", codUsuario);
			if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
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

	public clsUnidadEquivalente CargaUnidadEquivalente(int Coduni, int Codpro, int compraVenta)
	{
		clsUnidadEquivalente uni = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUnidadEquivalente", con.conector);
			cmd.Parameters.AddWithValue("coduni", Coduni);
			cmd.Parameters.AddWithValue("codpro", Codpro);
			cmd.Parameters.AddWithValue("compraVenta", compraVenta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = new clsUnidadEquivalente();
					uni.CodUnidadEquivalente = Convert.ToInt32(dr.GetDecimal(0));
					uni.CodProducto = Convert.ToInt32(dr.GetDecimal(1));
					uni.CodUnidad = Convert.ToInt32(dr.GetDecimal(2));
					uni.Factor = dr.GetDecimal(3);
					uni.CodUser = Convert.ToInt32(dr.GetDecimal(7));
					uni.FechaRegistro = dr.GetDateTime(8);
				}
			}
			return uni;
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

	public DataTable ListaUnidadesEquivalentes(int CodigoProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidadesEquivalentes", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodigoProducto);
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

	public DataTable CargaUnidadesEquivalentes(int CodigoProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaUnidadesEquivalentes", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodigoProducto);
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

	public DataTable BuscarProducto(int codProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("BuscarProducto", con.conector);
			cmd.Parameters.AddWithValue("codprod", codProducto);
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

	public int GetUnidadesEquivalentesPorUnidadBase(int codProducto, int codUnidadBase)
	{
		try
		{
			int numeroUnidades = 0;
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUnidadEquivalentesPorProductoYUnidadBase", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codProducto);
			cmd.Parameters.AddWithValue("codigo_ubase", codUnidadBase);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					numeroUnidades = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return numeroUnidades;
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

	public DataTable RelacionProductos(int codalma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductos", con.conector);
			cmd.Parameters.AddWithValue("codalma", codalma);
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

	public List<clsProducto> ListaProdAlmacen(int Codproducto, int Codalmacen)
	{
		clsProducto pro = null;
		List<clsProducto> listalm = new List<clsProducto>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("muestraStockAlmacenes_ProductoVen", con.conector);
			cmd.Parameters.AddWithValue("_codproducto", Codproducto);
			cmd.Parameters.AddWithValue("codalm", Codalmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.CodProducto = dr.GetInt32(0);
					pro.StockDisponible = dr.GetInt32(1);
					listalm.Add(pro);
				}
			}
			return listalm;
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

	public List<clsProducto> ListaProdConsultor(int CodVendedor)
	{
		clsProducto pro = null;
		List<clsProducto> list1 = new List<clsProducto>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProdConsultor", con.conector);
			cmd.Parameters.AddWithValue("codvendedor", CodVendedor);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.CodProducto = dr.GetInt32(0);
					pro.Descripcion = dr.GetString(1);
					pro.StockActual = dr.GetInt32(2);
					list1.Add(pro);
				}
			}
			return list1;
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

	public DataTable RelacionVendedor(int CodTipArt, int CodAlmacen, int CodLista, int CodVendedor)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosVendedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipo", CodTipArt);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codlista", CodLista);
			cmd.Parameters.AddWithValue("codvendedor", CodVendedor);
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

	public List<clsProducto> VentasProductosCount(int CodFac)
	{
		clsProducto pro = null;
		List<clsProducto> list1 = new List<clsProducto>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VentasProductosCount", con.conector);
			cmd.Parameters.AddWithValue("codfac", CodFac);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsProducto();
					pro.CodProducto = dr.GetInt32(0);
					pro.Descripcion = dr.GetString(1);
					pro.StockActual = dr.GetInt32(2);
					list1.Add(pro);
				}
			}
			return list1;
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

	public bool UpdateUnidadEquivalente(int cod, decimal precio)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaUnidadEquivalenteCodigo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod", cod);
			cmd.Parameters.AddWithValue("p", precio);
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

	public clsPrecioEquivalente PrecioStock(int cmunidad, int CodProducto, int undBase)
	{
		clsPrecioEquivalente p = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUnidad", con.conector);
			cmd.Parameters.AddWithValue("unid", cmunidad);
			cmd.Parameters.AddWithValue("codpro", CodProducto);
			cmd.Parameters.AddWithValue("undbase", undBase);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					p = new clsPrecioEquivalente();
					p.Stock = Convert.ToDecimal(dr.GetDecimal(0));
					p.Precio = Convert.ToInt32(dr.GetInt32(1));
					p.Und = dr.GetInt32(2);
					p.p_compra = dr.GetDecimal(3);
				}
			}
			return p;
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

	public DataTable ListaUnidadesEquivalentesCompra(int CodigoProducto, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidadesEquivalentesCompra", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodigoProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable ListaUnidadesEquivalentesVentaCotizacion(int CodigoProducto, int codAlmacen, int CodCotizacion)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidadesEquivalentesVentaCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodigoProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("CodCotizacion", CodCotizacion);
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

	public DataTable cargaetiquetas(int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaEtiquetas", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable cargaetiquetasolola3(int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaEtiqueta3", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable cargaetiquetauna(int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("cargaetiqueta_una", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable ListaUnidadesEquivalentesVenta(int CodigoProducto, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidadesEquivalentesVenta", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodigoProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable ListaUnidadesEquivalentesVenta1(int CodigoProducto, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidadesEquivalentesVenta1", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodigoProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable ListaUnidadesEquivalentes(int CodigoProducto, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidadesEquivalentes", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodigoProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public int getUnidadCompra(int codProd)
	{
		try
		{
			int unidcompra = 0;
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUC", con.conector);
			cmd.Parameters.AddWithValue("codpro", codProd);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					unidcompra = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return unidcompra;
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

	public clsUnidadEquivalente Factor(int codProducto, int codUnidadMedida, int codUnidaEqui)
	{
		clsUnidadEquivalente uni = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFactor", con.conector);
			cmd.Parameters.AddWithValue("codpro", codProducto);
			cmd.Parameters.AddWithValue("coduni", codUnidadMedida);
			cmd.Parameters.AddWithValue("coduniEqui", codUnidaEqui);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = new clsUnidadEquivalente();
					uni.Factor = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return uni;
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

	public clsProducto PrecioPromedio(int codProducto, int codalm)
	{
		clsProducto prod = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPrecioPromedio", con.conector);
			cmd.Parameters.AddWithValue("codpro", codProducto);
			cmd.Parameters.AddWithValue("codalma", codalm);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					prod = new clsProducto();
					prod.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
					prod.PrecioProm = Convert.ToDecimal(dr.GetDecimal(1));
					prod.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(2));
				}
			}
			return prod;
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

	public clsUnidadEquivalente PrecioVenta(int coduni, int codalmacen)
	{
		clsUnidadEquivalente uni = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPrecioVenta", con.conector);
			cmd.Parameters.AddWithValue("undequi", coduni);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = new clsUnidadEquivalente();
					uni.Stock = dr.GetDecimal(0);
					uni.CodUnidad = dr.GetInt32(1);
					uni.Precio = dr.GetDecimal(2);
					uni.Tipo = dr.GetInt32(3);
				}
			}
			return uni;
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

	public clsUnidadEquivalente PrecioVentaCotizacion(int coduni, int codalmacen, int codcotizacion)
	{
		clsUnidadEquivalente uni = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPrecioVentaCotizacion", con.conector);
			cmd.Parameters.AddWithValue("undequi", coduni);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("CodCotizacion", codcotizacion);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = new clsUnidadEquivalente();
					uni.Stock = dr.GetDecimal(0);
					uni.CodUnidad = dr.GetInt32(1);
					uni.Precio = dr.GetDecimal(2);
					uni.Tipo = dr.GetInt32(3);
				}
			}
			return uni;
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

	public clsUnidadEquivalente PrecioVentaSinStock(int coduni, int codalmacen)
	{
		clsUnidadEquivalente uni = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("PrecioVentaSinStock", con.conector);
			cmd.Parameters.AddWithValue("undequi", coduni);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = new clsUnidadEquivalente();
					uni.Stock = dr.GetDecimal(0);
					uni.CodUnidad = dr.GetInt32(1);
					uni.Precio = dr.GetDecimal(2);
					uni.Tipo = dr.GetInt32(3);
				}
			}
			return uni;
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

	public decimal UltimoPrecioCompraProducto(int codigoProducto, int codigoUnidad, int codigoUnidadEquivalente)
	{
		decimal ultimoPrecioProducto = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUltimoPrecioCompraPorProductoyUnidad", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad", codigoUnidad);
			cmd.Parameters.AddWithValue("codigo_unidad_equivalente", codigoUnidadEquivalente);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ultimoPrecioProducto = dr.GetDecimal(0);
				}
			}
			return ultimoPrecioProducto;
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

	public decimal UltimoPrecioCompraProductoCotizacion(int codigoProducto, int codigoUnidadEquivalente)
	{
		decimal ultimoPrecioProducto = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUltimoPrecioCompraPorProductoyUnidadCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad_equivalente", codigoUnidadEquivalente);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ultimoPrecioProducto = dr.GetDecimal(0);
				}
			}
			return ultimoPrecioProducto;
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

	public DataTable CostoTotalProducto(int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CostoTotalProducto", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad_equivalente", codigoUnidadEquivalente);
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

	public DataTable CostoTotalProductoCotizacion(int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CostoTotalProductoCotizacion", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad_equivalente", codigoUnidadEquivalente);
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

	public decimal UltimoPrecioVentaProductoCotizacion(int CodCliente, int codigoProducto, int codigoUnidadEquivalente)
	{
		decimal ultimoPrecioProducto = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUltimoPrecioVentaClienteCotizacion", con.conector);
			cmd.Parameters.AddWithValue("CodCliente", CodCliente);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad_equivalente", codigoUnidadEquivalente);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ultimoPrecioProducto = dr.GetDecimal(0);
				}
			}
			return ultimoPrecioProducto;
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

	public decimal UltimoPrecioVentaProducto(int codigoProducto, int codigoUnidad)
	{
		decimal ultimoPrecioVentaProducto = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPrecioVentaPorProductoyUnidad", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad", codigoUnidad);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ultimoPrecioVentaProducto = dr.GetDecimal(0);
				}
			}
			return ultimoPrecioVentaProducto;
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

	public int UnidadBase(int codProd, int codalma)
	{
		int uni = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("UnidadBase", con.conector);
			cmd.Parameters.AddWithValue("codProd", codProd);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = dr.GetInt32(0);
				}
			}
			return uni;
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

	public decimal FactorProducto(int codPro, int undBase, int undEqui, int tipo)
	{
		decimal uniEqui = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("FactorProducto", con.conector);
			cmd.Parameters.AddWithValue("codProd", codPro);
			cmd.Parameters.AddWithValue("undBase", undBase);
			cmd.Parameters.AddWithValue("undEqui", undEqui);
			cmd.Parameters.AddWithValue("tipo", tipo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uniEqui = dr.GetDecimal(0);
				}
			}
			return uniEqui;
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

	public string SiglaUnidadBase(int codUnd)
	{
		string uni = "";
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("SiglaUnidadBase", con.conector);
			cmd.Parameters.AddWithValue("codUnidadMedida_ex", codUnd);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = dr.GetString(0);
				}
			}
			return uni;
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

	public int GetCodProducto_xDescripcion(string descripcion)
	{
		int codProd = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodProducto_xDescripcion", con.conector);
			cmd.Parameters.AddWithValue("nombrePro", descripcion);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codProd = dr.GetInt32(0);
				}
			}
			return codProd;
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

	public int ValidaCodigoUE(int codigo)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaCodigoUE", con.conector);
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int ValidaCodigoUE(string unidad)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaCodigoUE", con.conector);
			cmd.Parameters.AddWithValue("unidad", unidad);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int ValidaCodigoProducto(int codigo)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaCodigoProducto", con.conector);
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int ExisteProductoSinFacturar(int codigo)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ExisteProductoSinFacturar", con.conector);
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int ValidaCodigoMoneda(int codigo)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaCodigoMoneda", con.conector);
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int ValidaCodigoMoneda(string moneda)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaCodigoMoneda", con.conector);
			cmd.Parameters.AddWithValue("moneda", moneda);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int ValidaTipoPrecio(int codigo)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaTipoPrecio", con.conector);
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int ValidaTipoPrecio(string tipoPrecio)
	{
		int resultado = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaTipoPrecio", con.conector);
			cmd.Parameters.AddWithValue("tipoPrecio", tipoPrecio);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	public int GetCodUnidad(string descripcion)
	{
		int codUnidad = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodUnidad_xDescripcion", con.conector);
			cmd.Parameters.AddWithValue("nombreUnd", descripcion);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codUnidad = dr.GetInt32(0);
				}
			}
			return codUnidad;
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

	public int GetCodTipoPrecio(string descripcion)
	{
		int CodTipoPrecio = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodTipoPrecio_xDescripcion", con.conector);
			cmd.Parameters.AddWithValue("nombreTipoP", descripcion);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					CodTipoPrecio = dr.GetInt32(0);
				}
			}
			return CodTipoPrecio;
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

	public int GetCodMoneda(string descripcion)
	{
		int GetCodMoneda = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodMoneda_xDescripcion", con.conector);
			cmd.Parameters.AddWithValue("nombreMoneda", descripcion);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					GetCodMoneda = dr.GetInt32(0);
				}
			}
			return GetCodMoneda;
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

	public int ValidaUnidadEquivalente(int codigo)
	{
		int Cantidad = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaUnidadEquivalente", con.conector);
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					Cantidad = dr.GetInt32(0);
				}
			}
			return Cantidad;
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

	public clsProducto CargaProductoDetalleCodBarras(string CodPro, int CodAlm, int Caso, int CodLista)
	{
		clsProducto pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductoDetalleCodBarras", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodPro);
			cmd.Parameters.AddWithValue("codalm", CodAlm);
			cmd.Parameters.AddWithValue("caso", Caso);
			cmd.Parameters.AddWithValue("lista", CodLista);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					if (Caso == 1)
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.Descripcion = dr.GetString(2);
						pro.StockDisponible = dr.GetDecimal(3);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
						pro.UnidadDescrip = dr.GetString(5);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
						pro.ConIgv = dr.GetBoolean(7);
						pro.TipoImpuesto = dr.GetInt32(8);
						pro.MaxPorcDesc = dr.GetDecimal(9);
					}
					else
					{
						pro = new clsProducto();
						pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
						pro.Referencia = dr.GetString(1);
						pro.Descripcion = dr.GetString(2);
						pro.StockDisponible = dr.GetDecimal(3);
						pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
						pro.UnidadDescrip = dr.GetString(5);
						pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
						pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(7));
						pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(8));
						pro.PrecioVariable = dr.GetBoolean(9);
						pro.Oferta = dr.GetBoolean(10);
						pro.PDescuento = Convert.ToDouble(dr.GetDecimal(11));
						pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(12));
						pro.ConIgv = dr.GetBoolean(13);
						pro.TipoImpuesto = dr.GetInt32(14);
						pro.MaxPorcDesc = dr.GetDecimal(18);
					}
				}
			}
			return pro;
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

	public DataTable MuestratipoNC()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestratipoNC", con.conector);
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

	public int MuestraCantidadProductos()
	{
		try
		{
			int cantidadProductos = 0;
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCantidadProductos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cantidadProductos = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return cantidadProductos;
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

	public int GetNumeroUnidadesEquivalentesPorProducto(int codProducto)
	{
		try
		{
			int numeroUnidades = 0;
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNumeroUnidadesEquivalentesPorProducto", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codProducto);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					numeroUnidades = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return numeroUnidades;
		}
		catch (MySqlException)
		{
			return -1;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public int VerificaProductoAlmacen(int codProducto)
	{
		try
		{
			int numeroUnidades = 0;
			con.conectarBD();
			cmd = new MySqlCommand("VerificaProductoAlmacen", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codProducto);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					numeroUnidades = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return numeroUnidades;
		}
		catch (MySqlException)
		{
			return -1;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public decimal GetValorPromedioSoles(int codprod, int codalma)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetValorPromedioSoles", con.conector);
			cmd.Parameters.AddWithValue("codProducto_ex", codprod);
			cmd.Parameters.AddWithValue("codAlmacen_ex", codalma);
			cmd.CommandType = CommandType.StoredProcedure;
			return Convert.ToDecimal(cmd.ExecuteScalar());
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

	public DataTable GetPromedioProductosVendidos(DateTime fechainicio, DateTime fechafinal, int codFamilia, int codLinea, int codGrupo, int codMarca, int codProducto, int cantidadDias)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GetPromedioProductosVendidos", con.conector);
			cmd.Parameters.AddWithValue("fechaInicio", fechainicio.ToString("yyyy-MM-dd"));
			cmd.Parameters.AddWithValue("fechaFinal", fechafinal.ToString("yyyy-MM-dd"));
			cmd.Parameters.AddWithValue("inCodFamilia", codFamilia);
			cmd.Parameters.AddWithValue("inCodLinea", codLinea);
			cmd.Parameters.AddWithValue("inCodGrupo", codGrupo);
			cmd.Parameters.AddWithValue("inCodMarca", codMarca);
			cmd.Parameters.AddWithValue("inCodProducto", codProducto);
			cmd.Parameters.AddWithValue("cantidadDias", cantidadDias);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
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

	public DataTable GetTotalizadoProductosVendidos(DateTime fechainicio, DateTime fechafinal, int codFamilia, int codLinea, int codGrupo, int codMarca, int codProducto, int cantidadDias)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GetTotalizadosProductosVendidos", con.conector);
			cmd.Parameters.AddWithValue("fechaInicio", fechainicio.ToString("yyyy-MM-dd"));
			cmd.Parameters.AddWithValue("fechaFinal", fechafinal.ToString("yyyy-MM-dd"));
			cmd.Parameters.AddWithValue("inCodFamilia", codFamilia);
			cmd.Parameters.AddWithValue("inCodLinea", codLinea);
			cmd.Parameters.AddWithValue("inCodGrupo", codGrupo);
			cmd.Parameters.AddWithValue("inCodMarca", codMarca);
			cmd.Parameters.AddWithValue("inCodProducto", codProducto);
			cmd.Parameters.AddWithValue("cantidadDias", cantidadDias);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
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

	public int GetProductoFacturado(int codprod)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetProductoFacturado", con.conector);
			cmd.Parameters.AddWithValue("codProducto_ex", codprod);
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

	public DataTable CargaProductoSunat()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaProductoSunat", con.conector);
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

	public DataTable muestraStockProducto_almacenes(int CodProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("muestraStockAlmacenes_Producto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codproducto", CodProducto);
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

	public List<clsUnidadEquivalente> unidadCompraxProducto(int codproducto)
	{
		List<clsUnidadEquivalente> list = new List<clsUnidadEquivalente>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("unidadCompraxProducto", con.conector);
			cmd.Parameters.AddWithValue("codprod", codproducto);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsUnidadEquivalente und = new clsUnidadEquivalente();
					und.CodUnidadEquivalente = dr.GetInt32(0);
					und.CodProducto = dr.GetInt32(1);
					und.CodUnidad = dr.GetInt32(2);
					und.nombreUnd = dr.GetString(3);
					und.Precio = dr.GetDecimal(4);
					list.Add(und);
				}
			}
			return list;
		}
		catch (MySqlException)
		{
			return list;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsUnidadEquivalente> unidadVentaxProducto(int codproducto)
	{
		List<clsUnidadEquivalente> list = new List<clsUnidadEquivalente>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("unidadVentaxProducto", con.conector);
			cmd.Parameters.AddWithValue("codprod", codproducto);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsUnidadEquivalente und = new clsUnidadEquivalente();
					und.CodUnidadEquivalente = dr.GetInt32(0);
					und.CodProducto = dr.GetInt32(1);
					und.CodUnidad = dr.GetInt32(2);
					und.nombreUnd = dr.GetString(3);
					und.Precio = dr.GetDecimal(4);
					list.Add(und);
				}
			}
			return list;
		}
		catch (MySqlException)
		{
			return list;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool UpdateUnidadEquivalenteMasivo(DataTable unds)
	{
		try
		{
			con.conectarBD();
			clsProducto prod = new clsProducto();
			cmd = new MySqlCommand("ActualizaUnidadEquivalenteCodigoMasivo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("cod", MySqlDbType.Int32).SourceColumn = "cod";
			cmd.Parameters.Add("p", MySqlDbType.Decimal).SourceColumn = "p";
			MySqlDataAdapter da = new MySqlDataAdapter();
			cmd.CommandTimeout = 250000000;
			da.InsertCommand = cmd;
			da.UpdateCommand = cmd;
			cmd.UpdatedRowSource = UpdateRowSource.None;
			da.UpdateBatchSize = 100;
			int records = da.Update(unds);
			return true;
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

	public DataTable listarStocksProducto(int codpro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listarStockProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codpro);
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

	public bool insertaStocksProducto(int codpro, int codalma, decimal stockmin, decimal stockmax, decimal capmax)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("insertaStockProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codpro);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("stockmin", stockmin);
			cmd.Parameters.AddWithValue("stockmax", stockmax);
			cmd.Parameters.AddWithValue("capmax", capmax);
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

	public bool updateStocksProducto(int codpro, int codalma, decimal stockmin, decimal stockmax, decimal capmax)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaStockProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codpro);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("stockmin", stockmin);
			cmd.Parameters.AddWithValue("stockmax", stockmax);
			cmd.Parameters.AddWithValue("capmax", capmax);
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

	public bool eliminaStocksProducto(int codpro, int codalma)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("eliminaStockProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codpro);
			cmd.Parameters.AddWithValue("codalma", codalma);
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

	public bool ValidaStockProducto(int Codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaStockProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codprod", Codigo);
			oParam = cmd.Parameters.AddWithValue("cont", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			if (Convert.ToInt32(cmd.Parameters["cont"].Value) != 0)
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

	public DataTable MuestraStockAlmacenes(int CodProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraStockAlmacenes", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", CodProducto);
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

	public DataTable PrecioVentaProductoPorUnidad(int codigoProducto, int codigoUnidad)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPreciosVentaxUnidadMedida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", codigoProducto);
			cmd.Parameters.AddWithValue("codum", codigoUnidad);
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

	public DataTable listaPreciosCantidad(int codproducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listaPreciosCantidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", codproducto);
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

	public bool GuardaPrecioCantidad(int codueq, decimal cantmax, decimal cantmin, int coduser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPrecioCantidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codueq", codueq);
			oParam = cmd.Parameters.AddWithValue("cantmax", cantmax);
			oParam = cmd.Parameters.AddWithValue("cantmin", cantmin);
			oParam = cmd.Parameters.AddWithValue("codusu", coduser);
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

	public bool GuardaCategorizacion(int codproducto, string desde, string hasta, string descripcion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCategorizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("idproducto", codproducto);
			oParam = cmd.Parameters.AddWithValue("desde", desde);
			oParam = cmd.Parameters.AddWithValue("hasta", hasta);
			oParam = cmd.Parameters.AddWithValue("descripcion", descripcion);
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

	public bool GuardaSituacion(int codproducto, string desde, string hasta, string descripcion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaSituacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("idproducto", codproducto);
			oParam = cmd.Parameters.AddWithValue("desde", desde);
			oParam = cmd.Parameters.AddWithValue("hasta", hasta);
			oParam = cmd.Parameters.AddWithValue("descripcion", descripcion);
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

	public bool ActualizaStockDisponible(int codproducto, int codalmacen, decimal stock)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaStockDisponible", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codproducto", codproducto);
			oParam = cmd.Parameters.AddWithValue("codalmacen", codalmacen);
			oParam = cmd.Parameters.AddWithValue("stock", stock);
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

	public bool ActualizaCategorizacion(int codcategorizacion, string condicion, string descripcion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCategorizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codcategorizacion", codcategorizacion);
			oParam = cmd.Parameters.AddWithValue("_condicion", condicion);
			oParam = cmd.Parameters.AddWithValue("_descripcion", descripcion);
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

	public bool ActualizaSituacion(int codsituacion, string desde, string hasta, string descripcion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaSituacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codsituacion", codsituacion);
			oParam = cmd.Parameters.AddWithValue("_desde", desde);
			oParam = cmd.Parameters.AddWithValue("_hasta", hasta);
			oParam = cmd.Parameters.AddWithValue("_descripcion", descripcion);
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

	public bool EliminaPrecioCantidad(int codpreciocantidad)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaPrecioCantidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpreciocantidad", codpreciocantidad);
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

	public bool EliminaCategorizacion(int codcategorizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaCategorizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codcategorizacion", codcategorizacion);
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

	public bool EliminaSituacion(int codsituacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaSituacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codsituacion", codsituacion);
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

	public DataTable validaPrecioCantidad(int codequi, int codpro, decimal cant)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("validaPrecioCantidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codequi", codequi);
			cmd.Parameters.AddWithValue("codpro", codpro);
			cmd.Parameters.AddWithValue("cant", cant);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception)
		{
			return null;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable listadodeproductos(int almacen, int marca, int familia, int linea, int grupo, int proveedor)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaProductos_ProductosAgrupados", con.conector);
			cmd.Parameters.AddWithValue("codmarca", marca);
			cmd.Parameters.AddWithValue("codalm", almacen);
			cmd.Parameters.AddWithValue("_codFamilia", familia);
			cmd.Parameters.AddWithValue("_codLinea", linea);
			cmd.Parameters.AddWithValue("_codGrupo", grupo);
			cmd.Parameters.AddWithValue("_codProveedor", proveedor);
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

	public double obtenerFleteDeProducto(double codProducto, int igv, int codunidad, double cantidad)
	{
		double flete = double.NaN;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("getFleteSegunProducto", con.conector);
			cmd.Parameters.AddWithValue("codprod", codProducto);
			cmd.Parameters.AddWithValue("igv", igv);
			cmd.Parameters.AddWithValue("codunidad", codunidad);
			cmd.Parameters.AddWithValue("cantidad", cantidad);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				dr.Read();
				flete = dr.GetDouble(0);
			}
			return flete;
		}
		catch (MySqlException)
		{
			return double.NaN;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool cambiarFleteDeProducto(int codProd, double flete)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("cambiaFleteDeProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codprod", codProd);
			oParam = cmd.Parameters.AddWithValue("_flete", flete);
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

	public bool insertaProductoAsociado(int codProd, int codAsoc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("insertaProductoAsociado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codprod", codProd);
			oParam = cmd.Parameters.AddWithValue("_codAsoc", codAsoc);
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

	public bool deleteAsociadosDeProducto(int codProd, int codAsoc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("deleteAsociadosDeProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codprod", codProd);
			oParam = cmd.Parameters.AddWithValue("_codAsoc", codAsoc);
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

	public DataTable cargaProductosAsociados()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoProductosAsociados", con.conector);
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

	public DataTable cargaProductos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoProductos", con.conector);
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

	public DataTable cargaStockProductos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoStockProductos", con.conector);
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

	public int getCantidadMaximaAsociadosXProducto()
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("getCantidadMaximaAsociadosXProducto", con.conector);
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

	public DataTable cargaReferenciasExternas(int codProducto, int codUnidadMedida)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaReferenciasExternas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codproducto", codProducto);
			oParam = cmd.Parameters.AddWithValue("_codunidadmedida", codUnidadMedida);
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

	public bool registraReferenciaExterna(ref ReferenciaExterna obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("registraReferenciaExterna", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codProducto", obj.codProducto);
			oParam = cmd.Parameters.AddWithValue("_codUnidadMedida", obj.codUnidadMedida);
			oParam = cmd.Parameters.AddWithValue("_precioReferencial", obj.precioReferencial);
			oParam = cmd.Parameters.AddWithValue("_skuProducto", obj.skuProducto);
			oParam = cmd.Parameters.AddWithValue("_nombreTienda", obj.nombreTienda);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			obj.codReferenciaExterna = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool editaReferenciaExterna(ReferenciaExterna obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("editaReferenciaExterna", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codReferenciaExterna", obj.codReferenciaExterna);
			oParam = cmd.Parameters.AddWithValue("_codProducto", obj.codProducto);
			oParam = cmd.Parameters.AddWithValue("_codUnidadMedida", obj.codUnidadMedida);
			oParam = cmd.Parameters.AddWithValue("_precioReferencial", obj.precioReferencial);
			oParam = cmd.Parameters.AddWithValue("_skuProducto", obj.skuProducto);
			oParam = cmd.Parameters.AddWithValue("_nombreTienda", obj.nombreTienda);
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

	public bool eliminaReferenciaExterna(int codReferencia)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("eliminaReferenciaExterna", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codReferencia", codReferencia);
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

	public DataTable CargaCategorizacion(int codproducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaCategorizacion", con.conector);
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codproducto", codproducto);
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

	public DataTable CargaSituacion(int codproducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaSituacion", con.conector);
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codproducto", codproducto);
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

	public DataTable CatalogoCombosProductos(bool estado, int tipoformulario)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CatalogoCombosProductos", con.conector);
			cmd.Parameters.AddWithValue("_estado", estado);
			cmd.Parameters.AddWithValue("tipoformulario", tipoformulario);
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

	public bool Deletecombo(int codcombo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("Eliminarcombo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codcombo", codcombo);
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

	public bool InsertCombo(clsComboProductos combo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCombo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_CodUsuario", combo.CodUsuario);
			oParam = cmd.Parameters.AddWithValue("_NombreCombo", combo.NombreCombo);
			oParam = cmd.Parameters.AddWithValue("_Estado", combo.Estado);
			oParam = cmd.Parameters.AddWithValue("_FechaRegistro", combo.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_FechaVencimiento", combo.FechaVencimiento);
			oParam = cmd.Parameters.AddWithValue("_Total", combo.Total);
			oParam = cmd.Parameters.AddWithValue("_stockcombo", combo.stockcombo);
			oParam = cmd.Parameters.AddWithValue("_stockcombodisponible", combo.stockcombodisponible);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			combo.CodCombo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public clsComboProductos CargaCombo(int codcombo)
	{
		clsComboProductos combo = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCombo", con.conector);
			cmd.Parameters.AddWithValue("codcombo", codcombo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					combo = new clsComboProductos();
					combo.CodCombo = dr.GetInt32(0);
					combo.NombreCombo = dr.GetString(1);
					combo.FechaVencimiento = dr.GetDateTime(2);
					combo.FechaRegistro = dr.GetDateTime(3);
					combo.Estado = dr.GetBoolean(4);
					combo.stockcombo = dr.GetInt32(5);
					combo.stockcombodisponible = dr.GetInt32(6);
				}
			}
			return combo;
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

	public DataTable CargaProductosCombo(int CodPro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProductosCombo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", CodPro);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
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

	public DataTable CargaDetalleComboVenta(int codcombo, int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleComboVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcombo", codcombo);
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

	public bool insertdetallecombo(clsDetalleCombo detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleCombo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codproducto", detalle.codproducto);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.cantidad);
			oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.codunidad);
			oParam = cmd.Parameters.AddWithValue("_precio", detalle.precio);
			oParam = cmd.Parameters.AddWithValue("_codcombo", detalle.codcombo);
			oParam = cmd.Parameters.AddWithValue("_codalmacen", detalle.codalmacen);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.id = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Updatecombo(clsComboProductos combo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("Actualizacombo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_CodCombo", combo.CodCombo);
			cmd.Parameters.AddWithValue("_NombreCombo", combo.NombreCombo);
			cmd.Parameters.AddWithValue("_Total", combo.Total);
			cmd.Parameters.AddWithValue("_FechaVencimiento", combo.FechaVencimiento);
			cmd.Parameters.AddWithValue("_stockcombo", combo.stockcombo);
			cmd.Parameters.AddWithValue("_stockcombodisponible", combo.stockcombodisponible);
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

	public decimal UltimoPrecioCompraProductoVenta(int codigoProducto, int codigoUnidad, int codigoUnidadEquivalente)
	{
		decimal ultimoPrecioProducto = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUltimoPrecioCompraPorProductoyUnidadVenta", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad", codigoUnidad);
			cmd.Parameters.AddWithValue("codigo_unidad_equivalente", codigoUnidadEquivalente);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ultimoPrecioProducto = dr.GetDecimal(0);
				}
			}
			return ultimoPrecioProducto;
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

	public DataTable CostoTotalProductoVenta(int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CostoTotalProductoDescuentosVenta", con.conector);
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("codigo_unidad_equivalente", codigoUnidadEquivalente);
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
