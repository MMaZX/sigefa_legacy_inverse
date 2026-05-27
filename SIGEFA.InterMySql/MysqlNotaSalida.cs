using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlNotaSalida : INotaSalida
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	private int tran;

	public bool insert(clsNotaSalida nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaNotaSalida", con.conector);
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
			string comentario = ((nota.Comentario.Length > 60) ? nota.Comentario.Substring(0, 60) : nota.Comentario);
			oParam = cmd.Parameters.AddWithValue("comentario", comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", nota.Igv);
			oParam = cmd.Parameters.AddWithValue("total", nota.Total);
			oParam = cmd.Parameters.AddWithValue("pendiente", nota.Total);
			oParam = cmd.Parameters.AddWithValue("estado", nota.Estado);
			if (nota.FormaPago != 0)
			{
				oParam = cmd.Parameters.AddWithValue("formapago", nota.FormaPago);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParam = cmd.Parameters.AddWithValue("fechapago", nota.FechaPago);
			oParam = cmd.Parameters.AddWithValue("codven", nota.CodVendedor);
			oParam = cmd.Parameters.AddWithValue("codusu", nota.CodUser);
			oParam = cmd.Parameters.AddWithValue("documentorefe", nota.DocumentoReferencia);
			if (nota.codVehiculoTransporte != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codVehiculoTransporte_ex", nota.codVehiculoTransporte);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codVehiculoTransporte_ex", null);
			}
			if (nota.codConductor != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codConductor_ex", nota.codConductor);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codConductor_ex", null);
			}
			if (nota.codalmacenreceptor != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codalmacenreceptor_ex", nota.codalmacenreceptor);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codalmacenreceptor_ex", null);
			}
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
			oParam = cmd.Parameters.AddWithValue("codTransferecia_ex", nota.Codtransferencia);
			oParam = cmd.Parameters.AddWithValue("_responsable", nota.responsable);
			oParam = cmd.Parameters.AddWithValue("_area", nota.area);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			nota.CodNotaSalida = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool update(clsNotaSalida nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", Convert.ToInt32(nota.CodNotaSalida));
			cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			cmd.Parameters.AddWithValue("codserie", nota.CodSerie);
			cmd.Parameters.AddWithValue("serie", nota.Serie);
			cmd.Parameters.AddWithValue("numdoc", nota.NumDoc);
			cmd.Parameters.AddWithValue("tipocliente", nota.TipoCliente);
			if (nota.CodCliente != 0)
			{
				cmd.Parameters.AddWithValue("codcli", nota.CodCliente);
			}
			else
			{
				cmd.Parameters.AddWithValue("codcli", null);
			}
			cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			cmd.Parameters.AddWithValue("codlista", nota.CodListaPrecio);
			cmd.Parameters.AddWithValue("tipocambio", nota.TipoCambio);
			cmd.Parameters.AddWithValue("fechasalida", nota.FechaSalida);
			cmd.Parameters.AddWithValue("comentario", nota.Comentario);
			cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			cmd.Parameters.AddWithValue("igv", nota.Igv);
			cmd.Parameters.AddWithValue("total", nota.Total);
			cmd.Parameters.AddWithValue("estado", nota.Estado);
			cmd.Parameters.AddWithValue("formapago", nota.FormaPago);
			cmd.Parameters.AddWithValue("fechapago", nota.FechaPago);
			cmd.Parameters.AddWithValue("codcredito", nota.CodNotaCredito);
			cmd.Parameters.AddWithValue("documento", nota.DocumentoReferencia);
			cmd.Parameters.AddWithValue("codven", nota.CodVendedor);
			cmd.Parameters.AddWithValue("codsta", nota.CodStand);
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

	public bool delete(int CodigoNota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", CodigoNota);
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

	public bool anular(int CodigoNota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", CodigoNota);
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

	public bool activar(int CodigoNota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActivarNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", CodigoNota);
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

	public bool ActualizaCantidadPendienteCotizacion(double cantidad, int produc, int CodCoti)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteDCotizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cant", cantidad);
			cmd.Parameters.AddWithValue("codpro", produc);
			cmd.Parameters.AddWithValue("codcoti", CodCoti);
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

	public bool ActualizaCantidadPendienteVenta(double cantidad, int produc, int CodVen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteDFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cant", cantidad);
			cmd.Parameters.AddWithValue("codpro", produc);
			cmd.Parameters.AddWithValue("codventa", CodVen);
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

	public bool ActualizaCotizacionAprobada(int codCotizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCotizacionAprobada", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codCoti", codCotizacion);
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

	public bool ActualizaCotizacionVigente(int codCotizacion, int codDetalleCotizacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCotizacionEstadoVigente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codCoti", codCotizacion);
			cmd.Parameters.AddWithValue("codDetalle", codDetalleCotizacion);
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

	public bool insertdetalle(clsDetalleNotaSalida detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codnota", detalle.CodNotaSalida);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codven", detalle.CodVenta);
			oParam = cmd.Parameters.AddWithValue("codcoti", detalle.CodCoti);
			oParam = cmd.Parameters.AddWithValue("codlista", detalle.CodLista);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("canti", detalle.Cantidad);
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
			oParam = cmd.Parameters.AddWithValue("valorrealS", detalle.ValorRealSoles);
			if (detalle.CantidadPendiente >= 0.0)
			{
				oParam = cmd.Parameters.AddWithValue("cantp", detalle.CantidadPendiente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("cantp", null);
			}
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleSalida = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (detalle.CodDetalleSalida != 0)
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

	public bool updatedetalle(clsDetalleNotaSalida detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle.CodDetalleSalida);
			cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
			cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
			cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
			cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", detalle.Igv);
			cmd.Parameters.AddWithValue("importe", detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			if (detalle.CantidadPendiente > 0.0)
			{
				cmd.Parameters.AddWithValue("cantp", detalle.CantidadPendiente);
			}
			else
			{
				cmd.Parameters.AddWithValue("cantp", null);
			}
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
			cmd = new MySqlCommand("EliminarDetalleSalida", con.conector);
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

	public bool insertdetalle2(clsDetalleNotaSalida detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codnota", detalle.CodNotaSalida);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codven", detalle.CodVenta);
			oParam = cmd.Parameters.AddWithValue("codcoti", detalle.CodCoti);
			oParam = cmd.Parameters.AddWithValue("codDetaCoti", detalle.CodDetalleCotizacion);
			oParam = cmd.Parameters.AddWithValue("codlista", detalle.CodLista);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
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
			oParam = cmd.Parameters.AddWithValue("valorrealS", detalle.ValorRealSoles);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleSalida = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertadetalleventaSalida(int codVenta, int codCoti, int codSalida)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleVentaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codVen", codVenta);
			oParam = cmd.Parameters.AddWithValue("codCoti", codCoti);
			oParam = cmd.Parameters.AddWithValue("codSalida", codSalida);
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

	public bool deletedetalleventaSalida()
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleVentaSalida", con.conector);
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

	public clsNotaSalida CargaNotaSalida(int CodNota)
	{
		clsNotaSalida nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaSalida", con.conector);
			cmd.Parameters.AddWithValue("codnota", CodNota);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaSalida();
					nota.CodNotaSalida = dr.GetString(0);
					nota.CodSucursal = dr.GetInt32(1);
					nota.CodAlmacen = dr.GetInt32(2);
					nota.CodTipoTransaccion = dr.GetInt32(3);
					nota.SiglaTransaccion = dr.GetString(4);
					nota.DescripcionTransaccion = dr.GetString(5);
					nota.CodTipoDocumento = dr.GetInt32(6);
					nota.SiglaDocumento = dr.GetString(7);
					nota.CodSerie = dr.GetInt32(8);
					nota.Serie = dr.GetString(9);
					nota.NumDoc = dr.GetString(10);
					nota.TipoCliente = dr.GetInt32(11);
					nota.CodCliente = dr.GetInt32(12);
					nota.DNI = dr.GetString(13);
					nota.RUCCliente = dr.GetString(14);
					nota.CodigoPersonalizado = dr.GetString(15);
					nota.RazonSocialCliente = dr.GetString(16);
					nota.Nombre = dr.GetString(17);
					nota.Direccion = dr.GetString(18);
					nota.Moneda = dr.GetInt32(19);
					nota.TipoCambio = dr.GetDouble(20);
					nota.FechaSalida = dr.GetDateTime(21);
					nota.Comentario = dr.GetString(22);
					nota.MontoBruto = dr.GetDouble(23);
					nota.MontoDscto = dr.GetDouble(24);
					nota.Igv = dr.GetDouble(25);
					nota.Total = dr.GetDouble(26);
					nota.Estado = dr.GetInt32(27);
					nota.FormaPago = dr.GetInt32(28);
					nota.FechaPago = dr.GetDateTime(29);
					nota.CodUser = dr.GetInt32(30);
					nota.FechaRegistro = dr.GetDateTime(31);
					nota.Pendiente = dr.GetDouble(32);
					nota.CodNotaCredito = dr.GetInt32(33);
					nota.DocumentoReferencia = dr.GetInt32(34);
					nota.CodVendedor = dr.GetInt32(35);
				}
			}
			return nota;
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

	public DataTable CargaDetalle(int CodNota)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", CodNota);
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

	public DataTable CargaDetalleNotaCredito(int CodNota)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleNotaCredito", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", CodNota);
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

	public DataTable ListaNotasSalida(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("criterio", Criterio);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
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

	public DataTable MuestraCobros(int Estado, int codAlmacen, DateTime Fecha1, DateTime Fecha2, int codTipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCobrosFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("esta", Estado);
			cmd.Parameters.AddWithValue("alma", codAlmacen);
			cmd.Parameters.AddWithValue("fecha1", Fecha1);
			cmd.Parameters.AddWithValue("fecha2", Fecha2);
			cmd.Parameters.AddWithValue("codtipo", codTipo);
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

	public DataTable DocumentosSinGuia(int CodAlmacen, int CodCliente, int Tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDocumentosGuias", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("alm", CodAlmacen);
			cmd.Parameters.AddWithValue("codcliente", CodCliente);
			cmd.Parameters.AddWithValue("tipo", Tipo);
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

	public DataTable DocumentosPorCliente(int CodCliente, int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDocumentosPorCliente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcli", CodCliente);
			cmd.Parameters.AddWithValue("tipo", tipo);
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

	public DataTable Ventas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVentas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicio);
			cmd.Parameters.AddWithValue("fechafin", FechaFin);
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

	public DataTable MuestraFacturasVentaCliente(int CodAlmacen, int CodSucursal, int CodEmpresa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVentaNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("sucur", CodSucursal);
			cmd.Parameters.AddWithValue("empre", CodEmpresa);
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

	public DataTable MuestraVentaSalida(int CodAlmacen, int codVenta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFacturaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codventa", codVenta);
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

	public DataTable MuestraCotizacionSalida(int CodAlmacen, int codCotizacion)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCotizacionSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codcoti", codCotizacion);
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

	public DataTable MuestraTipoDocumentoNota(int CodAlmacen, int codCliente)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTipoDocumentoOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codcliente", codCliente);
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

	public clsNotaSalida CargaNotaSalidaCredito(int CodNota)
	{
		clsNotaSalida nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaSalidaCreditoCompras", con.conector);
			cmd.Parameters.AddWithValue("codnota", CodNota);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaSalida();
					nota.CodNotaSalida = dr.GetString(0);
					nota.CodSucursal = dr.GetInt32(1);
					nota.CodAlmacen = dr.GetInt32(2);
					nota.CodTipoTransaccion = dr.GetInt32(3);
					nota.SiglaTransaccion = dr.GetString(4);
					nota.DescripcionTransaccion = dr.GetString(5);
					nota.CodTipoDocumento = dr.GetInt32(6);
					nota.SiglaDocumento = dr.GetString(7);
					nota.CodSerie = dr.GetInt32(8);
					nota.Serie = dr.GetString(9);
					nota.NumDoc = dr.GetString(10);
					nota.TipoCliente = dr.GetInt32(11);
					nota.CodCliente = dr.GetInt32(12);
					nota.DNI = dr.GetString(13);
					nota.RUCCliente = dr.GetString(36);
					nota.CodigoPersonalizado = dr.GetString(15);
					nota.RazonSocialCliente = dr.GetString(37);
					nota.Nombre = dr.GetString(17);
					nota.Direccion = dr.GetString(18);
					nota.Moneda = dr.GetInt32(19);
					nota.TipoCambio = dr.GetDouble(20);
					nota.FechaSalida = dr.GetDateTime(21);
					nota.Comentario = dr.GetString(22);
					nota.MontoBruto = dr.GetDouble(23);
					nota.MontoDscto = dr.GetDouble(24);
					nota.Igv = dr.GetDouble(25);
					nota.Total = dr.GetDouble(26);
					nota.Estado = dr.GetInt32(27);
					nota.FormaPago = dr.GetInt32(28);
					nota.FechaPago = dr.GetDateTime(29);
					nota.CodUser = dr.GetInt32(30);
					nota.FechaRegistro = dr.GetDateTime(31);
					nota.Pendiente = dr.GetDouble(32);
					nota.CodNotaCredito = dr.GetInt32(33);
					nota.DocumentoReferencia = dr.GetInt32(34);
					nota.CodVendedor = dr.GetInt32(35);
					nota.Docref = dr.GetString(38);
					nota.Aplicada = Convert.ToInt32(dr.GetString(39));
					nota.CodAplicada = dr.GetInt32(40);
					nota.Motivo = dr.GetString(41);
					nota.DocumentoNotaSalida = dr.GetString(42);
					nota.DocumentoFacturaAplicada = dr.GetString(43);
				}
			}
			return nota;
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

	public DataTable ListaNotasCreditoCompra(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotasCreditoCompra", con.conector);
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

	public DataTable MuestraNotaAlmacen(int codAlmacen, int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraSalidaAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("tipo", tipo);
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

	public DataTable CargaDetalleNotaSalida(int codNota, int proceso)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaDetalleNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", codNota);
			cmd.Parameters.AddWithValue("proceso", proceso);
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

	public clsNotaSalida CargaNotaSalidaCreditoVentas(int CodNota)
	{
		clsNotaSalida nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaCreditoVenta", con.conector);
			cmd.Parameters.AddWithValue("codnota", CodNota);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaSalida();
					nota.CodNotaSalida = dr.GetString(0);
					nota.CodSucursal = dr.GetInt32(1);
					nota.CodAlmacen = dr.GetInt32(2);
					nota.CodTipoTransaccion = dr.GetInt32(3);
					nota.SiglaTransaccion = dr.GetString(4);
					nota.DescripcionTransaccion = dr.GetString(5);
					nota.CodTipoDocumento = dr.GetInt32(6);
					nota.SiglaDocumento = dr.GetString(7);
					nota.CodSerie = dr.GetInt32(8);
					nota.Serie = dr.GetString(9);
					nota.NumDoc = dr.GetString(10);
					nota.TipoCliente = dr.GetInt32(11);
					nota.CodCliente = dr.GetInt32(12);
					nota.DNI = dr.GetString(13);
					nota.RUCCliente = dr.GetString(14);
					nota.CodigoPersonalizado = dr.GetString(15);
					nota.RazonSocialCliente = dr.GetString(16);
					nota.Nombre = dr.GetString(17);
					nota.Direccion = dr.GetString(18);
					nota.Moneda = dr.GetInt32(19);
					nota.TipoCambio = dr.GetDouble(20);
					nota.FechaSalida = dr.GetDateTime(21);
					nota.Comentario = dr.GetString(22);
					nota.MontoBruto = dr.GetDouble(23);
					nota.MontoDscto = dr.GetDouble(24);
					nota.Igv = dr.GetDouble(25);
					nota.Total = dr.GetDouble(26);
					nota.Estado = dr.GetInt32(27);
					nota.FormaPago = dr.GetInt32(28);
					nota.FechaPago = dr.GetDateTime(29);
					nota.CodUser = dr.GetInt32(30);
					nota.FechaRegistro = dr.GetDateTime(31);
					nota.Pendiente = dr.GetDouble(32);
					nota.CodNotaCredito = dr.GetInt32(33);
					nota.DocumentoReferencia = dr.GetInt32(34);
					nota.CodVendedor = dr.GetInt32(35);
				}
			}
			return nota;
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

	public clsNotaSalida CargaNotaSalidaDebitoVentas(int CodNota)
	{
		clsNotaSalida nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaDebitoVenta", con.conector);
			cmd.Parameters.AddWithValue("codnota", CodNota);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaSalida();
					nota.CodNotaSalida = dr.GetString(0);
					nota.CodSucursal = dr.GetInt32(1);
					nota.CodAlmacen = dr.GetInt32(2);
					nota.CodTipoTransaccion = dr.GetInt32(3);
					nota.SiglaTransaccion = dr.GetString(4);
					nota.DescripcionTransaccion = dr.GetString(5);
					nota.CodTipoDocumento = dr.GetInt32(6);
					nota.SiglaDocumento = dr.GetString(7);
					nota.CodSerie = dr.GetInt32(8);
					nota.Serie = dr.GetString(9);
					nota.NumDoc = dr.GetString(10);
					nota.TipoCliente = dr.GetInt32(11);
					nota.CodCliente = dr.GetInt32(12);
					nota.DNI = dr.GetString(13);
					nota.RUCCliente = dr.GetString(14);
					nota.CodigoPersonalizado = dr.GetString(15);
					nota.RazonSocialCliente = dr.GetString(16);
					nota.Nombre = dr.GetString(17);
					nota.Direccion = dr.GetString(18);
					nota.Moneda = dr.GetInt32(19);
					nota.TipoCambio = dr.GetDouble(20);
					nota.FechaSalida = dr.GetDateTime(21);
					nota.Comentario = dr.GetString(22);
					nota.MontoBruto = dr.GetDouble(23);
					nota.MontoDscto = dr.GetDouble(24);
					nota.Igv = dr.GetDouble(25);
					nota.Total = dr.GetDouble(26);
					nota.Estado = dr.GetInt32(27);
					nota.FormaPago = dr.GetInt32(28);
					nota.FechaPago = dr.GetDateTime(29);
					nota.CodUser = dr.GetInt32(30);
					nota.FechaRegistro = dr.GetDateTime(31);
					nota.Pendiente = dr.GetDouble(32);
					nota.CodNotaCredito = dr.GetInt32(33);
					nota.DocumentoReferencia = dr.GetInt32(34);
					nota.CodVendedor = dr.GetInt32(35);
				}
			}
			return nota;
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

	public bool VerificarNCCompraAplicada(clsNotaSalida nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificarNCCompraAplicada", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("aplicad", nota.Aplicada);
			oParam = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codref", nota.CodAplicada);
			oParam = cmd.Parameters.AddWithValue("total", nota.Total);
			oParam = cmd.Parameters.AddWithValue("msj", "0");
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			nota.Comentario = Convert.ToString(cmd.Parameters["msj"].Value);
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

	public DataTable ListarNotaSalidaParaNDCompra(int codAlm, int codProv)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarNotaSalidaParaNDCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("alma", codAlm);
			cmd.Parameters.AddWithValue("codpro", codProv);
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

	public DataTable CargaDetalleNotaSalidaNDC(int codNota, int codAlm)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaDetalleNotaSalidaNDC", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", codNota);
			cmd.Parameters.AddWithValue("codalm", codAlm);
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

	public bool ActualizaNCreditoCompraSinAplicar(clsNotaSalida nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNCreditoCompraSinAplicar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codNotaS", nota.CodNotaSalida);
			cmd.Parameters.AddWithValue("codRef", nota.DocumentoReferencia);
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

	public bool ActualizaSalidaDevolucion(int codnota, int codfact)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaSalidaDevolucion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", codnota);
			cmd.Parameters.AddWithValue("codfac", codfact);
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

	public clsNotaSalida CargaNS(int codTransDirecta)
	{
		clsNotaSalida nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNSalida", con.conector);
			cmd.Parameters.AddWithValue("cod", codTransDirecta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaSalida();
					nota.CodNotaSalida = dr.GetString(0);
					nota.DocumentoReferencia = dr.GetInt32(1);
				}
			}
			return nota;
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

	public decimal VerificarStock(int codpro, int codalma, int opc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificarStockDisponible", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codProducto_ex", codpro);
			cmd.Parameters.AddWithValue("codAlmacen_ex", codalma);
			cmd.Parameters.AddWithValue("opc", opc);
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
}
