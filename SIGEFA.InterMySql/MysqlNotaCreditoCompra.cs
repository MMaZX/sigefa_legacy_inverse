using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlNotaCreditoCompra : INotaCreditoCompra
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsNotaSalida nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaNotaCreditoCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codSu", nota.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			oParam = cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codserie", nota.CodSerie);
			oParam = cmd.Parameters.AddWithValue("serie", nota.Serie);
			oParam = cmd.Parameters.AddWithValue("numdoc", nota.NumDoc);
			oParam = cmd.Parameters.AddWithValue("tipocliente", nota.TipoCliente);
			if (nota.CodCliente != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codcli", nota.CodCliente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codcli", null);
			}
			oParam = cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", nota.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechasalida", nota.FechaSalida);
			oParam = cmd.Parameters.AddWithValue("comentario", nota.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", nota.Igv);
			oParam = cmd.Parameters.AddWithValue("total", nota.Total);
			oParam = cmd.Parameters.AddWithValue("pendiente", nota.Total);
			oParam = cmd.Parameters.AddWithValue("estado", nota.Estado);
			oParam = cmd.Parameters.AddWithValue("codven", nota.CodVendedor);
			oParam = cmd.Parameters.AddWithValue("codusu", nota.CodUser);
			oParam = cmd.Parameters.AddWithValue("documentorefe", nota.DocumentoReferencia);
			oParam = cmd.Parameters.AddWithValue("aplicad", nota.Aplicada);
			if (nota.Aplicada != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codaplicad", nota.CodAplicada);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codaplicad", null);
			}
			if (nota.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", nota.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			if (nota.CodProveedor != 0)
			{
				cmd.Parameters.AddWithValue("codprov", nota.CodProveedor);
			}
			else
			{
				cmd.Parameters.AddWithValue("CodProv", null);
			}
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public DataTable cargaTipoNCC()
	{
		try
		{
			DataTable tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadotipoNCparaNCC", con.conector);
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

	public int getAccionSegunTipoSeleccionado(int seleccionado)
	{
		int rspta = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetValorSegunCodigoNotaDeCreditoSeleccionado", con.conector);
			cmd.Parameters.AddWithValue("_codSeleccionado", seleccionado);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rspta = dr.GetInt32(0);
				}
			}
			return rspta;
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

	public bool verificarCodProductoAgregableANotaDeCredito(int codProducto)
	{
		int rspta = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("verificaProductoAgregrableANotaCreditoCompra", con.conector);
			cmd.Parameters.AddWithValue("_codProducto", codProducto);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rspta = dr.GetInt32(0);
				}
			}
			return (rspta != 0) ? true : false;
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

	public bool insert(clsNotaCredito nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaNotaCreditoCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_docnota", nota.DocumentoNotaCredito);
			oParam = cmd.Parameters.AddWithValue("_codtran", nota.CodTipoTransaccion);
			oParam = cmd.Parameters.AddWithValue("_codtipo", nota.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("_numdoc", nota.NumFac);
			oParam = cmd.Parameters.AddWithValue("_moneda", nota.Moneda);
			oParam = cmd.Parameters.AddWithValue("_tipocambio", nota.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("_fechaemision", nota.FechaEmision);
			oParam = cmd.Parameters.AddWithValue("_comentario", nota.Comentario);
			oParam = cmd.Parameters.AddWithValue("_bruto", nota.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("_montodscto", nota.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("_igv", nota.Igv);
			oParam = cmd.Parameters.AddWithValue("_total", nota.Total);
			oParam = cmd.Parameters.AddWithValue("_estado", nota.Estado);
			oParam = cmd.Parameters.AddWithValue("_fecharegistro", nota.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_codusu", nota.CodUser);
			oParam = cmd.Parameters.AddWithValue("_codref", nota.CodReferencia);
			oParam = cmd.Parameters.AddWithValue("_serie", nota.Serie);
			oParam = cmd.Parameters.AddWithValue("_codprov", nota.CodProveedor);
			oParam = cmd.Parameters.AddWithValue("_codalma", nota.CodAlmacen);
			if (nota.Motivo != "")
			{
				cmd.Parameters.AddWithValue("_motiv", nota.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("_motiv", null);
			}
			oParam = cmd.Parameters.AddWithValue("_serieGRSP", nota.serieGRSP);
			oParam = cmd.Parameters.AddWithValue("_numdocGRSP", nota.numdocGRSP);
			oParam = cmd.Parameters.AddWithValue("_productoDestruido", nota.ProductoDestruido ? 1 : 0);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			nota.CodNotaCreditoNueva = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalle(clsDetalleNotaCredito Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleNotaCreditoCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codpro", Detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("_codnotacredito", Detalle.CodNotaCredito);
			oParam = cmd.Parameters.AddWithValue("_codnota", Detalle.CodNotaSalida);
			oParam = cmd.Parameters.AddWithValue("_unidad", Detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("_serielote", Detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("_precio", Detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("_subtotal", Detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("_dscto1", Detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("_dscto2", Detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("_dscto3", Detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("_montodscto", Detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("_igv", Detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("_importe", Detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("_precioreal", Detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("_valoreal", Detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", Detalle.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_codusu", Detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("_codalma", Detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("_cant", Detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("_descrip", Detalle.Descripcion);
			oParam = cmd.Parameters.AddWithValue("_tipoDetalle", Detalle.TipoDetalle);
			oParam = cmd.Parameters.AddWithValue("_valorCompra", Detalle.valorCompra);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Detalle.CodDetalleNotaCredito = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool setCodNotaSalida(string codNotaSalida, int codNotaCreditoNueva)
	{
		int rspta = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("setCodNotaSalidaaNotaCreditoCompra", con.conector);
			cmd.Parameters.AddWithValue("_codNotaCreditoCompra", codNotaCreditoNueva);
			cmd.Parameters.AddWithValue("_codNotaSalida", codNotaSalida);
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

	public DataTable ListadoEstandarNotaCreditoCompra(int codAlmacen, int codSucursal, int tipoFecha, DateTime date1, DateTime date2, int codProd)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoEstandarNotaCreditoCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", codSucursal);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_FechaInicio", date1);
			cmd.Parameters.AddWithValue("_FechaFin", date2);
			cmd.Parameters.AddWithValue("_codProducto", codProd);
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

	public clsNotaCredito cargaNotaCredito(int codNotaCC)
	{
		try
		{
			clsNotaCredito ncc = null;
			con.conectarBD();
			cmd = new MySqlCommand("CargaNotaCreditoCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codNotaCC", codNotaCC);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ncc = new clsNotaCredito();
					ncc.DocumentoNotaCredito = dr.GetString(0);
					ncc.CodTipoTransaccion = dr.GetInt32(1);
					ncc.CodTipoDocumento = dr.GetInt32(2);
					ncc.NumFac = dr.GetString(3);
					ncc.Moneda = dr.GetInt32(4);
					ncc.TipoCambio = dr.GetDouble(5);
					ncc.FechaEmision = dr.GetDateTime(6);
					ncc.Comentario = dr.GetString(7);
					ncc.MontoBruto = dr.GetDouble(8);
					ncc.MontoDscto = dr.GetDouble(9);
					ncc.Igv = dr.GetDouble(10);
					ncc.Total = dr.GetDouble(11);
					ncc.Estado = dr.GetInt32(12);
					ncc.FechaRegistro = dr.GetDateTime(13);
					ncc.CodUser = dr.GetInt32(14);
					ncc.CodReferencia = dr.GetInt32(15);
					ncc.Serie = dr.GetString(16);
					ncc.CodProveedor = dr.GetInt32(17);
					ncc.CodAlmacen = dr.GetInt32(18);
					ncc.Motivo = dr.GetString(19);
					ncc.serieGRSP = dr.GetString(20);
					ncc.numdocGRSP = dr.GetString(21);
					ncc.ProductoDestruido = dr.GetInt32(22) == 1;
					ncc.CodNotaCredito = dr.GetString(23);
					ncc.codNotaSalida = (dr.IsDBNull(24) ? "" : dr.GetString(24));
					ncc.SiglaDocumento = "NCC";
				}
			}
			return ncc;
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

	public DataTable cargaDetalleNCC(string codNotaCredito)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("cargaDetalleNotaCreditoCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codNotaCredito", codNotaCredito);
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

	public bool actualizaSerieyCorrelativo(string codNotaCredito, string serie, string numdoc)
	{
		int rspta = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaSerieyCorrelativoNotaCreditoCompra", con.conector);
			cmd.Parameters.AddWithValue("_codNotaCreditoCompra", codNotaCredito);
			cmd.Parameters.AddWithValue("_serie", serie);
			cmd.Parameters.AddWithValue("_numdoc", numdoc);
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

	public bool actualizaAsignador(string codNotaCredito, int codUser, DateTime fecha)
	{
		int rspta = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaAsignadorNotaCreditoCompra", con.conector);
			cmd.Parameters.AddWithValue("_codNotaCreditoCompra", codNotaCredito);
			cmd.Parameters.AddWithValue("_codUser", codUser);
			cmd.Parameters.AddWithValue("_fechaasignacion", fecha);
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

	public bool actualizaEstado(string codNotaCredito, int estado)
	{
		int rspta = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaEstadoNotaCreditoCompra", con.conector);
			cmd.Parameters.AddWithValue("_codNotaCreditoCompra", codNotaCredito);
			cmd.Parameters.AddWithValue("_estado", estado);
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

	public bool verificarCodFacturaTieneNotaCredito(int codFactura)
	{
		try
		{
			int rpta = 0;
			con.conectarBD();
			cmd = new MySqlCommand("verificarCodFacturaTieneNotaCredito", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codFactura", codFactura);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rpta = dr.GetInt32(0);
				}
			}
			return rpta == 1;
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

	public bool actualizaFormaPago(string codNotaCredito, int estadoFormaPago)
	{
		try
		{
			int rpta = 0;
			con.conectarBD();
			cmd = new MySqlCommand("actualizaNotaCreditoCompraFormaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codNotaCredito", codNotaCredito);
			cmd.Parameters.AddWithValue("_estadoFP", estadoFormaPago);
			cmd.CommandType = CommandType.StoredProcedure;
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
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
