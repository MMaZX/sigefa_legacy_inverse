using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlFactura : IFactura
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsFactura Factura)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("docfactura", Factura.DocumentoFactura);
			oParam = cmd.Parameters.AddWithValue("codtran", Factura.CodTipoTransaccion);
			oParam = cmd.Parameters.AddWithValue("codtipo", Factura.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("numdoc", Factura.NumFac);
			oParam = cmd.Parameters.AddWithValue("moneda", Factura.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", Factura.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechaingreso", Factura.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("comentario", Factura.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", Factura.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", Factura.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", Factura.Igv);
			oParam = cmd.Parameters.AddWithValue("flete", Factura.Flete);
			oParam = cmd.Parameters.AddWithValue("total", Factura.Total);
			oParam = cmd.Parameters.AddWithValue("pendiente", Factura.Total);
			oParam = cmd.Parameters.AddWithValue("estado", Factura.Estado);
			oParam = cmd.Parameters.AddWithValue("recibido", Factura.Recibido);
			if (Factura.FormaPago != 0)
			{
				oParam = cmd.Parameters.AddWithValue("formapago", Factura.FormaPago);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParam = cmd.Parameters.AddWithValue("fechapago", Factura.FechaPago);
			oParam = cmd.Parameters.AddWithValue("fechacancelado", Factura.FechaCancelado);
			oParam = cmd.Parameters.AddWithValue("cancelado", Factura.Cancelado);
			oParam = cmd.Parameters.AddWithValue("codusu", Factura.CodUser);
			oParam = cmd.Parameters.AddWithValue("codref", Factura.CodReferencia);
			oParam = cmd.Parameters.AddWithValue("codser", Factura.CodSerie);
			oParam = cmd.Parameters.AddWithValue("serie", Factura.Serie);
			oParam = cmd.Parameters.AddWithValue("codpro", Factura.CodProveedor);
			oParam = cmd.Parameters.AddWithValue("codalma", Factura.CodAlmacen);
			if (Factura.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", Factura.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			oParam = cmd.Parameters.AddWithValue("codNotaI_ex", Factura.CodNotaIngreso);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Factura.CodFacturaNueva = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool update(clsFactura Factura)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura", Factura.CodFactura);
			cmd.Parameters.AddWithValue("codtipo", Factura.CodTipoDocumento);
			cmd.Parameters.AddWithValue("numdoc", Factura.NumFac);
			cmd.Parameters.AddWithValue("moneda", Factura.Moneda);
			cmd.Parameters.AddWithValue("tipocambio", Factura.TipoCambio);
			cmd.Parameters.AddWithValue("fechaingreso", Factura.FechaIngreso);
			cmd.Parameters.AddWithValue("comentario", Factura.Comentario);
			cmd.Parameters.AddWithValue("bruto", Factura.MontoBruto);
			cmd.Parameters.AddWithValue("montodscto", Factura.MontoDscto);
			cmd.Parameters.AddWithValue("igv", Factura.Igv);
			cmd.Parameters.AddWithValue("flete", Factura.Flete);
			cmd.Parameters.AddWithValue("total", Factura.Total);
			cmd.Parameters.AddWithValue("estado", Factura.Estado);
			cmd.Parameters.AddWithValue("recibido", Factura.Recibido);
			cmd.Parameters.AddWithValue("formapago", Factura.FormaPago);
			cmd.Parameters.AddWithValue("fechapago", Factura.FechaPago);
			cmd.Parameters.AddWithValue("fechacancelado", Factura.FechaCancelado);
			cmd.Parameters.AddWithValue("cancelado", Factura.Cancelado);
			cmd.Parameters.AddWithValue("codpro", Factura.CodProveedor);
			cmd.Parameters.AddWithValue("codalma", Factura.CodAlmacen);
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

	public bool delete(string CodigoFactura)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura", CodigoFactura);
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

	public bool anular(int CodigoFactura)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura", CodigoFactura);
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

	public bool anular_1(int CodigoFactura, int coduser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularFactura_1", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura", CodigoFactura);
			cmd.Parameters.AddWithValue("codUser", coduser);
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

	public bool activar(string CodigoFactura)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActivarFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura", CodigoFactura);
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

	public bool insertdetalle(clsDetalleFactura Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", Detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codfactura", Detalle.CodFactura);
			oParam = cmd.Parameters.AddWithValue("codnota", Detalle.CodNotaIngreso);
			oParam = cmd.Parameters.AddWithValue("moneda", Detalle.Moneda);
			oParam = cmd.Parameters.AddWithValue("unidad", Detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", Detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("precio", Detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", Detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", Detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", Detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", Detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", Detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", Detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("flete", Detalle.Flete);
			oParam = cmd.Parameters.AddWithValue("importe", Detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", Detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", Detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("fecha", Detalle.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("codusu", Detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("codalma", Detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("cant", Detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Detalle.CodDetalleFactura = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool updatedetalle(clsDetalleFactura Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", Detalle.CodDetalleFactura);
			cmd.Parameters.AddWithValue("moneda", Detalle.Moneda);
			cmd.Parameters.AddWithValue("unidad", Detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("serielote", Detalle.SerieLote);
			cmd.Parameters.AddWithValue("precio", Detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", Detalle.Subtotal);
			cmd.Parameters.AddWithValue("dscto1", Detalle.Descuento1);
			cmd.Parameters.AddWithValue("dscto2", Detalle.Descuento2);
			cmd.Parameters.AddWithValue("dscto3", Detalle.Descuento3);
			cmd.Parameters.AddWithValue("montodscto", Detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", Detalle.Igv);
			cmd.Parameters.AddWithValue("flete", Detalle.Flete);
			cmd.Parameters.AddWithValue("importe", Detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", Detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", Detalle.ValoReal);
			cmd.Parameters.AddWithValue("fecha", Detalle.FechaIngreso);
			cmd.Parameters.AddWithValue("codalma", Detalle.CodAlmacen);
			cmd.Parameters.AddWithValue("cant", Detalle.Cantidad);
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
			cmd = new MySqlCommand("EliminarDetalleFactura", con.conector);
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

	public clsFactura CargaFactura(int CodFactura)
	{
		clsFactura factura = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFactura", con.conector);
			cmd.Parameters.AddWithValue("codfactura", CodFactura);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					factura = new clsFactura();
					factura.CodFactura = dr.GetInt32(0);
					factura.CodNotaIngreso = dr.GetInt32(1);
					factura.CodTipoTransaccion = dr.GetInt32(2);
					factura.SiglaTransaccion = dr.GetString(3);
					factura.DescripcionTransaccion = dr.GetString(4);
					factura.CodTipoDocumento = Convert.ToInt32(dr.GetString(5));
					factura.SiglaDocumento = dr.GetString(6);
					factura.DocumentoFactura = dr.GetString(7);
					factura.Moneda = Convert.ToInt32(dr.GetString(8));
					factura.TipoCambio = dr.GetDouble(9);
					factura.FechaIngreso = dr.GetDateTime(10);
					factura.Comentario = dr.GetString(11);
					factura.MontoBruto = dr.GetDouble(12);
					factura.MontoDscto = dr.GetDouble(13);
					factura.Igv = dr.GetDouble(14);
					factura.Total = dr.GetDouble(15);
					factura.Abonado = dr.GetDouble(16);
					factura.Pendiente = dr.GetDouble(17);
					factura.Estado = dr.GetInt32(18);
					factura.Recibido = dr.GetInt32(19);
					factura.FormaPago = dr.GetInt32(20);
					factura.FechaPago = dr.GetDateTime(21);
					factura.Cancelado = dr.GetInt32(22);
					factura.CodUser = dr.GetInt32(23);
					factura.FechaRegistro = dr.GetDateTime(24);
					factura.Flete = dr.GetDouble(25);
					factura.CodProveedor = dr.GetInt32(26);
					factura.RazonSocialProveedor = dr.GetString(27);
					factura.RUCProveedor = dr.GetString(28);
					factura.Motivo = dr.GetString(29);
					factura.CodSerie = dr.GetInt32(30);
				}
			}
			return factura;
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

	public DataTable CargaDetalle(int CodFactura)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura", CodFactura);
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

	public DataTable ListaFactura(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotasFactura", con.conector);
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

	public DataTable ListaFactura(int tipoFecha, DateTime FechaInicial, DateTime FechaFinal, int alma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFacturas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("alma", alma);
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

	public DataTable ListaFacturaCombo(DateTime FechaInicial, DateTime FechaFinal, int alma, int estado)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFacturasCombo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("alma", alma);
			cmd.Parameters.AddWithValue("cmb", estado);
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

	public DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotasCreditoFactura", con.conector);
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

	public DataTable MuestraPagos(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2, int tipoBusqueda)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPagos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("esta", Estado);
			cmd.Parameters.AddWithValue("empre", codEmpresa);
			cmd.Parameters.AddWithValue("fecha1", Fecha1);
			cmd.Parameters.AddWithValue("fecha2", Fecha2);
			cmd.Parameters.AddWithValue("tipoBusqueda", tipoBusqueda);
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

	public DataTable MuestraFacturasProveedor(int alma, int codpro, int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFacturasProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("alma", alma);
			cmd.Parameters.AddWithValue("codpro", codpro);
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

	public bool ActualizaPendienteCreditoCompra(double monto, int codcompra, int codalm)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPendienteCreditoCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("monto", monto);
			cmd.Parameters.AddWithValue("codcompra", codcompra);
			cmd.Parameters.AddWithValue("codalm", codalm);
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

	public DataTable ListaNotasDebitoCompra(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotaDebitoCompra", con.conector);
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

	public string guiasPorFactura(int codfactura)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GuiasPorFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura", codfactura);
			return cmd.ExecuteScalar().ToString();
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

	public DataTable ListaFactura_Facturacion(DateTime FechaInicial, DateTime FechaFinal, int alma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFacturas_Facturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("alma", alma);
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

	public DataTable ListaFacturaxProducto(DateTime FechaInicial, DateTime FechaFinal, int alma, int codprod)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFacturasxProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("alma", alma);
			cmd.Parameters.AddWithValue("codprod", codprod);
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

	public bool verificaFleteFactura(int codFactura)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("verificaFleteEnFactura", con.conector);
			cmd.Parameters.AddWithValue("_codFactura", codFactura);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows && dr.Read())
			{
				return dr.GetInt32(0) == 1;
			}
			return true;
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
