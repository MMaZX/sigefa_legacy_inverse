using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

public class MysqlFacturaVenta : IFacturaVenta
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private MySqlTransaction tra = null;

	private DataTable tabla = null;

	public bool insert(clsFacturaVenta factura_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codSu", factura_venta.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("codalma", factura_venta.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codtran", factura_venta.CodTipoTransaccion);
			oParam = cmd.Parameters.AddWithValue("codtipo", factura_venta.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codserie", factura_venta.CodSerie);
			oParam = cmd.Parameters.AddWithValue("serie", factura_venta.Serie);
			oParam = cmd.Parameters.AddWithValue("numdoc", factura_venta.NumDoc);
			oParam = cmd.Parameters.AddWithValue("tipocliente", factura_venta.TipoCliente);
			if (factura_venta.CodCliente != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codcli", factura_venta.CodCliente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codcli", 1);
			}
			oParam = cmd.Parameters.AddWithValue("moneda", factura_venta.Moneda);
			oParam = cmd.Parameters.AddWithValue("codlista", factura_venta.CodListaPrecio);
			oParam = cmd.Parameters.AddWithValue("tipocambio", factura_venta.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechasalida", factura_venta.FechaSalida);
			oParam = cmd.Parameters.AddWithValue("comentario", factura_venta.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", factura_venta.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", factura_venta.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", factura_venta.Igv);
			oParam = cmd.Parameters.AddWithValue("total", factura_venta.Total);
			oParam = cmd.Parameters.AddWithValue("pendiente", factura_venta.Total);
			oParam = cmd.Parameters.AddWithValue("estado", factura_venta.Estado);
			if (factura_venta.FormaPago != 0)
			{
				oParam = cmd.Parameters.AddWithValue("formapago", factura_venta.FormaPago);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParam = cmd.Parameters.AddWithValue("fechapago", factura_venta.FechaPago);
			oParam = cmd.Parameters.AddWithValue("codven", factura_venta.CodVendedor);
			oParam = cmd.Parameters.AddWithValue("codCoti", factura_venta.CodCotizacion);
			oParam = cmd.Parameters.AddWithValue("codusu", factura_venta.CodUser);
			oParam = cmd.Parameters.AddWithValue("consultorext", factura_venta.Consultorext);
			oParam = cmd.Parameters.AddWithValue("codsalidaconsulext", factura_venta.Codsalidaconsulext);
			if (factura_venta.DocumentoReferencia != null)
			{
				cmd.Parameters.AddWithValue("docreferencia", factura_venta.DocumentoReferencia);
			}
			else
			{
				cmd.Parameters.AddWithValue("docreferencia", null);
			}
			if (factura_venta.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", factura_venta.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			if (factura_venta.Detallecomentario != "")
			{
				cmd.Parameters.AddWithValue("detcoment", factura_venta.Detallecomentario);
			}
			else
			{
				cmd.Parameters.AddWithValue("detcoment", null);
			}
			oParam = cmd.Parameters.AddWithValue("codped", factura_venta.CodPedido);
			oParam = cmd.Parameters.AddWithValue("entregado_ex", factura_venta.Entregado);
			oParam = cmd.Parameters.AddWithValue("codsep", factura_venta.CodSeparacion);
			oParam = cmd.Parameters.AddWithValue("tipoventa_ex", factura_venta.Tipoventa);
			oParam = cmd.Parameters.AddWithValue("gravadas_ex", factura_venta.Gravadas);
			oParam = cmd.Parameters.AddWithValue("exoneradas_ex", factura_venta.Exoneradas);
			oParam = cmd.Parameters.AddWithValue("inafectas_ex", factura_venta.Inafectas);
			oParam = cmd.Parameters.AddWithValue("gratuitas_ex", factura_venta.Gratuitas);
			oParam = cmd.Parameters.AddWithValue("codEmpresa_ex", factura_venta.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("Boletafactura_ex", factura_venta.Boletafactura);
			oParam = cmd.Parameters.AddWithValue("codigobarras_ex", factura_venta.CodigoBarras);
			oParam = cmd.Parameters.AddWithValue("codigoBarrasCifrado_ex", factura_venta.CodigoBarrasCifrado);
			oParam = cmd.Parameters.AddWithValue("nombrecliente_ex", factura_venta.Nombre);
			oParam = cmd.Parameters.AddWithValue("TipoDocumentoAnticipo_ex", null);
			oParam = cmd.Parameters.AddWithValue("DocumentoReferenciaAnticipo_ex", null);
			oParam = cmd.Parameters.AddWithValue("MontoAnticipo_ex", null);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			oParam = cmd.Parameters.AddWithValue("numeraDoc", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			factura_venta.CodFacturaVenta = Convert.ToString(cmd.Parameters["newid"].Value);
			factura_venta.NumDoc = Convert.ToString(cmd.Parameters["numeraDoc"].Value);
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

	public bool insertComprobante(clsFacturaVenta factura_venta)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamC = cmd.Parameters.AddWithValue("codSu", factura_venta.CodSucursal);
			oParamC = cmd.Parameters.AddWithValue("codalma", factura_venta.CodAlmacen);
			oParamC = cmd.Parameters.AddWithValue("codtran", factura_venta.CodTipoTransaccion);
			oParamC = cmd.Parameters.AddWithValue("codtipo", factura_venta.CodTipoDocumento);
			oParamC = cmd.Parameters.AddWithValue("codser", factura_venta.CodSerie);
			oParamC = cmd.Parameters.AddWithValue("serie", factura_venta.Serie);
			oParamC = cmd.Parameters.AddWithValue("numdoc", factura_venta.NumDoc);
			oParamC = cmd.Parameters.AddWithValue("tipocliente", factura_venta.TipoCliente);
			if (factura_venta.CodCliente != 0)
			{
				oParamC = cmd.Parameters.AddWithValue("codcli", factura_venta.CodCliente);
			}
			else
			{
				oParamC = cmd.Parameters.AddWithValue("codcli", 1);
			}
			oParamC = cmd.Parameters.AddWithValue("moneda", factura_venta.Moneda);
			oParamC = cmd.Parameters.AddWithValue("codlista", factura_venta.CodListaPrecio);
			oParamC = cmd.Parameters.AddWithValue("tipocambio", factura_venta.TipoCambio);
			oParamC = cmd.Parameters.AddWithValue("fechasalida", factura_venta.FechaSalida);
			oParamC = cmd.Parameters.AddWithValue("comentario", factura_venta.Comentario);
			oParamC = cmd.Parameters.AddWithValue("bruto", factura_venta.MontoBruto);
			oParamC = cmd.Parameters.AddWithValue("montodscto", factura_venta.MontoDscto);
			oParamC = cmd.Parameters.AddWithValue("igv", factura_venta.Igv);
			oParamC = cmd.Parameters.AddWithValue("total", factura_venta.Total);
			oParamC = cmd.Parameters.AddWithValue("pendiente", factura_venta.Total);
			oParamC = cmd.Parameters.AddWithValue("estado", factura_venta.Estado);
			if (factura_venta.FormaPago != 0)
			{
				oParamC = cmd.Parameters.AddWithValue("formapago", factura_venta.FormaPago);
			}
			else
			{
				oParamC = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParamC = cmd.Parameters.AddWithValue("fechapago", factura_venta.FechaPago);
			oParamC = cmd.Parameters.AddWithValue("codven", factura_venta.CodVendedor);
			oParamC = cmd.Parameters.AddWithValue("codCoti", factura_venta.CodCotizacion);
			oParamC = cmd.Parameters.AddWithValue("codusu", factura_venta.CodUser);
			oParamC = cmd.Parameters.AddWithValue("consultorext", factura_venta.Consultorext);
			oParamC = cmd.Parameters.AddWithValue("codsalidaconsulext", factura_venta.Codsalidaconsulext);
			if (factura_venta.DocumentoReferencia != null)
			{
				cmd.Parameters.AddWithValue("docreferencia", factura_venta.DocumentoReferencia);
			}
			else
			{
				cmd.Parameters.AddWithValue("docreferencia", null);
			}
			if (factura_venta.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", factura_venta.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			if (factura_venta.Detallecomentario != "")
			{
				cmd.Parameters.AddWithValue("detcoment", factura_venta.Detallecomentario);
			}
			else
			{
				cmd.Parameters.AddWithValue("detcoment", null);
			}
			oParamC = cmd.Parameters.AddWithValue("codped", factura_venta.CodPedido);
			oParamC = cmd.Parameters.AddWithValue("entregado_ex", factura_venta.Entregado);
			oParamC = cmd.Parameters.AddWithValue("codsep", factura_venta.CodSeparacion);
			oParamC = cmd.Parameters.AddWithValue("tipoventa_ex", factura_venta.Tipoventa);
			oParamC = cmd.Parameters.AddWithValue("gravadas_ex", factura_venta.Gravadas);
			oParamC = cmd.Parameters.AddWithValue("exoneradas_ex", factura_venta.Exoneradas);
			oParamC = cmd.Parameters.AddWithValue("inafectas_ex", factura_venta.Inafectas);
			oParamC = cmd.Parameters.AddWithValue("gratuitas_ex", factura_venta.Gratuitas);
			oParamC = cmd.Parameters.AddWithValue("codEmpresa_ex", factura_venta.CodEmpresa);
			oParamC = cmd.Parameters.AddWithValue("Boletafactura_ex", factura_venta.Boletafactura);
			oParamC = cmd.Parameters.AddWithValue("codigobarras_ex", factura_venta.CodigoBarras);
			oParamC = cmd.Parameters.AddWithValue("codigoBarrasCifrado_ex", factura_venta.CodigoBarrasCifrado);
			oParamC = cmd.Parameters.AddWithValue("nombrecliente_ex", factura_venta.Nombre);
			oParamC = cmd.Parameters.AddWithValue("TipoDocumentoAnticipo_ex", null);
			oParamC = cmd.Parameters.AddWithValue("DocumentoReferenciaAnticipo_ex", null);
			oParamC = cmd.Parameters.AddWithValue("numeroDocumentoIdentidad_ex", factura_venta.NumeroDocumentoCliente);
			oParamC = cmd.Parameters.AddWithValue("codigoDocumentoIdentidad_ex", factura_venta.DocumentoIdentidad.CodDocumentoIdentidad);
			oParamC = cmd.Parameters.AddWithValue("MontoAnticipo_ex", null);
			oParamC = cmd.Parameters.AddWithValue("ventasinstock_ex", factura_venta.ventasinstock);
			oParamC = cmd.Parameters.AddWithValue("_icbper", factura_venta.icbper);
			oParamC = cmd.Parameters.AddWithValue("_valorRetencion", factura_venta.valorRetencion);
			if (factura_venta.idTecnico > 0)
			{
				oParamC = cmd.Parameters.AddWithValue("_idTecnico", factura_venta.idTecnico);
			}
			else
			{
				oParamC = cmd.Parameters.AddWithValue("_idTecnico", null);
			}
			if (factura_venta.idZona > 0)
			{
				oParamC = cmd.Parameters.AddWithValue("_idZona", factura_venta.idZona);
			}
			else
			{
				oParamC = cmd.Parameters.AddWithValue("_idZona", null);
			}
			if (factura_venta.CodCanalVenta.Length > 6)
			{
				oParamC = cmd.Parameters.AddWithValue("_codCanalVenta", null);
			}
			else
			{
				oParamC = cmd.Parameters.AddWithValue("_codCanalVenta", factura_venta.CodCanalVenta);
			}
			oParamC = cmd.Parameters.AddWithValue("newid", 0);
			oParamC.Direction = ParameterDirection.Output;
			oParamC = cmd.Parameters.AddWithValue("numeraDoc", 0);
			oParamC.Direction = ParameterDirection.Output;
			int xC = cmd.ExecuteNonQuery();
			factura_venta.CodFacturaVenta = Convert.ToString(cmd.Parameters["newid"].Value);
			factura_venta.NumDoc = Convert.ToString(cmd.Parameters["numeraDoc"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)factura_venta.CodFacturaVenta, (Func<char, bool>)char.IsDigit) || factura_venta.CodFacturaVenta == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			int codProductoSinStock = 0;
			foreach (clsDetalleFacturaVenta det in factura_venta.Detalle)
			{
				if (det.Igv == 0m)
				{
					throw new Exception("El producto con codigo: " + det.CodProducto + " tiene calculado el IGV en cero");
				}
				cmd = new MySqlCommand("GuardaDetalleFacturaVenta", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParamD = cmd.Parameters.AddWithValue("codpro", det.CodProducto);
				oParamD = cmd.Parameters.AddWithValue("codventa", factura_venta.CodFacturaVenta);
				oParamD = cmd.Parameters.AddWithValue("codalma", det.CodAlmacen);
				oParamD = cmd.Parameters.AddWithValue("unidad", det.UnidadIngresada);
				oParamD = cmd.Parameters.AddWithValue("serielote", det.SerieLote);
				oParamD = cmd.Parameters.AddWithValue("cantidad", det.Cantidad);
				oParamD = cmd.Parameters.AddWithValue("cantidadp", det.CantidadPendiente);
				oParamD = cmd.Parameters.AddWithValue("moneda", det.Moneda);
				oParamD = cmd.Parameters.AddWithValue("precio", det.PrecioUnitario);
				oParamD = cmd.Parameters.AddWithValue("subtotal", det.Subtotal);
				oParamD = cmd.Parameters.AddWithValue("dscto1", det.Descuento1);
				oParamD = cmd.Parameters.AddWithValue("dscto2", det.Descuento2);
				oParamD = cmd.Parameters.AddWithValue("dscto3", det.Descuento3);
				oParamD = cmd.Parameters.AddWithValue("montodscto", det.MontoDescuento);
				oParamD = cmd.Parameters.AddWithValue("igv", det.Igv);
				oParamD = cmd.Parameters.AddWithValue("importe", det.Importe);
				oParamD = cmd.Parameters.AddWithValue("precioreal", det.PrecioReal);
				oParamD = cmd.Parameters.AddWithValue("valoreal", det.ValoReal);
				oParamD = cmd.Parameters.AddWithValue("codDetaCoti", det.CodDetalleCotizacion);
				oParamD = cmd.Parameters.AddWithValue("codusu", det.CodUser);
				oParamD = cmd.Parameters.AddWithValue("codDetaPed", det.CodDetallePedido);
				oParamD = cmd.Parameters.AddWithValue("entregado_ex", det.Entregado);
				oParamD = cmd.Parameters.AddWithValue("codDetaSep", det.CodDetalleSeparacion);
				oParamD = cmd.Parameters.AddWithValue("serie_", det.SerieMotor);
				oParamD = cmd.Parameters.AddWithValue("nrochasis_", det.NroChasis);
				oParamD = cmd.Parameters.AddWithValue("modelo_", det.Modelo);
				oParamD = cmd.Parameters.AddWithValue("marca_", det.Marca);
				oParamD = cmd.Parameters.AddWithValue("color_", det.Color);
				oParamD = cmd.Parameters.AddWithValue("_icbper", det.icbper);
				oParamD = cmd.Parameters.AddWithValue("_icbper_band", det.icbper_band);
				oParamD = cmd.Parameters.AddWithValue("codlinea", det.codlinea);
				oParamD = cmd.Parameters.AddWithValue("codfamilia", det.codfamilia);
				oParamD = cmd.Parameters.AddWithValue("newid", 0);
				oParamD.Direction = ParameterDirection.Output;
				int xD = cmd.ExecuteNonQuery();
				det.CodDetalleVenta = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (det.CodDetalleVenta == 0)
				{
					rpta = false;
					factura_venta.CodFacturaVenta = null;
					break;
				}
				if (det.CodDetalleVenta == -1)
				{
					codProductoSinStock = det.CodProducto;
					factura_venta.CodFacturaVenta = null;
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				if (codProductoSinStock != 0)
				{
					MessageBox.Show("El producto  con código " + codProductoSinStock + " no tiene stock suficiente para la venta o el requerimiento de almacén no cubre la cantidad necesaria de venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return rpta;
			}
			Scope.Complete();
			Scope.Dispose();
			return rpta;
		}
		catch (MySqlException ex)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			rpta = false;
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool update(clsFacturaVenta factura_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", Convert.ToInt32(factura_venta.CodFacturaVenta));
			cmd.Parameters.AddWithValue("codalma", factura_venta.CodAlmacen);
			cmd.Parameters.AddWithValue("codtran", factura_venta.CodTipoTransaccion);
			cmd.Parameters.AddWithValue("codtipo", factura_venta.CodTipoDocumento);
			cmd.Parameters.AddWithValue("codserie", factura_venta.CodSerie);
			cmd.Parameters.AddWithValue("serie", factura_venta.Serie);
			cmd.Parameters.AddWithValue("numdoc", factura_venta.NumDoc);
			cmd.Parameters.AddWithValue("tipocliente", factura_venta.TipoCliente);
			if (factura_venta.CodCliente != 0)
			{
				cmd.Parameters.AddWithValue("codcli", factura_venta.CodCliente);
			}
			else
			{
				cmd.Parameters.AddWithValue("codcli", null);
			}
			cmd.Parameters.AddWithValue("moneda", factura_venta.Moneda);
			cmd.Parameters.AddWithValue("codlista", factura_venta.CodListaPrecio);
			cmd.Parameters.AddWithValue("tipocambio", factura_venta.TipoCambio);
			cmd.Parameters.AddWithValue("fechasalida", factura_venta.FechaSalida);
			cmd.Parameters.AddWithValue("comentario", factura_venta.Comentario);
			cmd.Parameters.AddWithValue("bruto", factura_venta.MontoBruto);
			cmd.Parameters.AddWithValue("montodscto", factura_venta.MontoDscto);
			cmd.Parameters.AddWithValue("igv", factura_venta.Igv);
			cmd.Parameters.AddWithValue("total", factura_venta.Total);
			cmd.Parameters.AddWithValue("estado", factura_venta.Estado);
			cmd.Parameters.AddWithValue("formapago", factura_venta.FormaPago);
			cmd.Parameters.AddWithValue("fechapago", factura_venta.FechaPago);
			cmd.Parameters.AddWithValue("codcredito", factura_venta.CodNotaCredito);
			cmd.Parameters.AddWithValue("documento", factura_venta.DocumentoReferencia);
			cmd.Parameters.AddWithValue("codven", factura_venta.CodVendedor);
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

	public bool updateCobroVenta(clsFacturaVenta factura_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaFacturaVentaCobro", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", Convert.ToInt32(factura_venta.CodFacturaVenta));
			cmd.Parameters.AddWithValue("codalma", factura_venta.CodAlmacen);
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

	public bool rollbackfactura(int codigoventa, int tipo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("rollbackfactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codigoventa);
			cmd.Parameters.AddWithValue("tipo", tipo);
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

	public bool delete(int codigoventa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codigoventa);
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

	public bool AnulaDetalleVenta(int codigoventa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codigoventa);
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

	public bool anular(int codigoventa, int codUsuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codigoventa);
			cmd.Parameters.AddWithValue("_codUsuario", codUsuario);
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

	public bool activar(int codigoventa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActivarFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codigoventa);
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

	public bool insertdetalle(clsDetalleFacturaVenta detalle_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle_venta.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codventa", detalle_venta.CodVenta);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle_venta.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle_venta.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle_venta.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", detalle_venta.Cantidad);
			oParam = cmd.Parameters.AddWithValue("cantidadp", detalle_venta.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("moneda", detalle_venta.Moneda);
			oParam = cmd.Parameters.AddWithValue("precio", detalle_venta.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", detalle_venta.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", detalle_venta.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", detalle_venta.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", detalle_venta.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", detalle_venta.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", detalle_venta.Igv);
			oParam = cmd.Parameters.AddWithValue("importe", detalle_venta.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", detalle_venta.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle_venta.ValoReal);
			oParam = cmd.Parameters.AddWithValue("codDetaCoti", detalle_venta.CodDetalleCotizacion);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle_venta.CodUser);
			oParam = cmd.Parameters.AddWithValue("codDetaPed", detalle_venta.CodDetallePedido);
			oParam = cmd.Parameters.AddWithValue("entregado_ex", detalle_venta.Entregado);
			oParam = cmd.Parameters.AddWithValue("codDetaSep", detalle_venta.CodDetalleSeparacion);
			oParam = cmd.Parameters.AddWithValue("serie_", detalle_venta.SerieMotor);
			oParam = cmd.Parameters.AddWithValue("nrochasis_", detalle_venta.NroChasis);
			oParam = cmd.Parameters.AddWithValue("modelo_", detalle_venta.Modelo);
			oParam = cmd.Parameters.AddWithValue("marca_", detalle_venta.Marca);
			oParam = cmd.Parameters.AddWithValue("color_", detalle_venta.Color);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle_venta.CodDetalleVenta = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool updatedetalle(clsDetalleFacturaVenta detalle_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle_venta.CodDetalleVenta);
			cmd.Parameters.AddWithValue("unidad", detalle_venta.UnidadIngresada);
			cmd.Parameters.AddWithValue("serielote", detalle_venta.SerieLote);
			cmd.Parameters.AddWithValue("cantidad", detalle_venta.Cantidad);
			cmd.Parameters.AddWithValue("precio", detalle_venta.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", detalle_venta.Subtotal);
			cmd.Parameters.AddWithValue("dscto1", detalle_venta.Descuento1);
			cmd.Parameters.AddWithValue("dscto2", detalle_venta.Descuento2);
			cmd.Parameters.AddWithValue("dscto3", detalle_venta.Descuento3);
			cmd.Parameters.AddWithValue("montodscto", detalle_venta.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", detalle_venta.Igv);
			cmd.Parameters.AddWithValue("importe", detalle_venta.Importe);
			cmd.Parameters.AddWithValue("precioreal", detalle_venta.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", detalle_venta.ValoReal);
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

	public bool ActualizaStockSinFacturar(clsDetalleFacturaVenta detalle_venta, int unibas, decimal factor, int ingresoegreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaStockSinFacturar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CodDetalleVenta_ex", detalle_venta.CodDetalleVenta);
			cmd.Parameters.AddWithValue("UnidadIngresada_ex", detalle_venta.UnidadIngresada);
			cmd.Parameters.AddWithValue("CodProducto_ex", detalle_venta.CodProducto);
			cmd.Parameters.AddWithValue("Cantidad_ex", detalle_venta.Cantidad);
			cmd.Parameters.AddWithValue("ValoReal_ex", detalle_venta.ValoReal);
			cmd.Parameters.AddWithValue("CodAlmacen_ex", detalle_venta.CodAlmacen);
			cmd.Parameters.AddWithValue("unibas_ex", unibas);
			cmd.Parameters.AddWithValue("factor_ex", factor);
			cmd.Parameters.AddWithValue("ingresoegreso_ex", ingresoegreso);
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

	public bool InsertaProductoSinFacturar(clsDetalleFacturaVenta detalle_venta, decimal vps, decimal stock, decimal soles, int unibas)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaProductoSinFacturar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codProducto_ex", detalle_venta.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codAlmacen_ex", detalle_venta.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("valorpromediosoles_ex", vps);
			oParam = cmd.Parameters.AddWithValue("stockactual_ex", stock);
			oParam = cmd.Parameters.AddWithValue("stockdisponible_ex", stock);
			oParam = cmd.Parameters.AddWithValue("soles_ex", soles);
			oParam = cmd.Parameters.AddWithValue("totalingresos_ex", stock);
			oParam = cmd.Parameters.AddWithValue("totalsalidas_ex", 0);
			oParam = cmd.Parameters.AddWithValue("totalsolesingresos_ex", soles);
			oParam = cmd.Parameters.AddWithValue("totalsolessalidas_ex", 0);
			oParam = cmd.Parameters.AddWithValue("codUser_ex", detalle_venta.CodUser);
			oParam = cmd.Parameters.AddWithValue("Unidad_ex", unibas);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle_venta.CodDetalleVenta = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool deletedetalle(int codigodetalle_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", codigodetalle_venta);
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
	}

	public bool deletedetalleventasalida()
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

	public bool actualizaEstadoImpreso(int codVen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizarVentaImpresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("venta", codVen);
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

	public clsFacturaVenta fechaCorrelativoAnterior(int codserie)
	{
		clsFacturaVenta factura = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaFechaAnteriorSerie", con.conector);
			cmd.Parameters.AddWithValue("codse", codserie);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					factura = new clsFacturaVenta();
					factura.FechaSalida = dr.GetDateTime(0);
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

	public clsFacturaVenta BuscaFacturaVenta(int codVenta, int codAlmacen)
	{
		clsFacturaVenta factura = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaFacturaVentaxDocumento", con.conector);
			cmd.Parameters.AddWithValue("codventa", codVenta);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					factura = new clsFacturaVenta();
					factura.CodFacturaVenta = dr.GetString(0);
					factura.CodAlmacen = dr.GetInt32(1);
					factura.CodTipoTransaccion = dr.GetInt32(2);
					factura.SiglaTransaccion = dr.GetString(3);
					factura.DescripcionTransaccion = dr.GetString(4);
					factura.CodTipoDocumento = dr.GetInt32(5);
					factura.SiglaDocumento = dr.GetString(6);
					factura.CodSerie = dr.GetInt32(7);
					factura.Serie = dr.GetString(8);
					factura.NumDoc = dr.GetString(9);
					factura.TipoCliente = dr.GetInt32(10);
					factura.CodCliente = dr.GetInt32(11);
					factura.DNI = dr.GetString(12);
					factura.RUCCliente = dr.GetString(13);
					factura.CodigoPersonalizado = dr.GetString(14);
					factura.RazonSocialCliente = dr.GetString(15);
					factura.Nombre = dr.GetString(16);
					factura.Direccion = dr.GetString(17);
					factura.Moneda = dr.GetInt32(18);
					factura.TipoCambio = dr.GetDouble(19);
					factura.FechaSalida = dr.GetDateTime(20);
					factura.Comentario = dr.GetString(21);
					factura.MontoBruto = dr.GetDecimal(22);
					factura.MontoDscto = dr.GetDecimal(23);
					factura.Igv = dr.GetDecimal(24);
					factura.Total = dr.GetDecimal(25);
					factura.Estado = dr.GetInt32(26);
					factura.FormaPago = dr.GetInt32(27);
					factura.FechaPago = dr.GetDateTime(28);
					factura.CodUser = dr.GetInt32(29);
					factura.FechaRegistro = dr.GetDateTime(30);
					factura.CodEmpresa = dr.GetInt32(31);
					factura.icbper = dr.GetDecimal(32);
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

	public clsNotaIngreso BuscaNotaSalida(int codVenta, int codAlmacen)
	{
		clsNotaIngreso nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaNotaSalida", con.conector);
			cmd.Parameters.AddWithValue("docref", codVenta);
			cmd.Parameters.AddWithValue("alma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaIngreso();
					nota.CodNotaIngreso = dr.GetString(0);
					nota.CodAlmacen = dr.GetInt32(2);
					nota.CodTipoTransaccion = dr.GetInt32(3);
					nota.SiglaTransaccion = dr.GetString(4);
					nota.DescripcionTransaccion = dr.GetString(5);
					nota.CodTipoDocumento = dr.GetInt32(6);
					nota.SiglaDocumento = dr.GetString(7);
					nota.CodSerie = dr.GetInt32(8);
					nota.Serie = dr.GetString(9);
					nota.NumDoc = dr.GetString(10);
					nota.Moneda = dr.GetInt32(19);
					nota.TipoCambio = dr.GetDouble(20);
					nota.FechaIngreso = dr.GetDateTime(21);
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
					nota.DocumentoReferencia = dr.GetInt32(34);
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

	public bool UpdateKardex(int codNota, int codProd, int Codalma, decimal Cant, decimal valProm)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnulaKardex", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codn", Convert.ToInt32(codNota));
			cmd.Parameters.AddWithValue("prod", codProd);
			cmd.Parameters.AddWithValue("codalma", Codalma);
			cmd.Parameters.AddWithValue("cant", Cant);
			cmd.Parameters.AddWithValue("valProm", valProm);
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

	public DataTable CargaDetalleNotaSalida(int codventa, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("BuscaDetalleNotaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", codventa);
			cmd.Parameters.AddWithValue("codalm", codAlmacen);
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

	public clsFacturaVenta CargaFacturaVenta(int codventa)
	{
		clsFacturaVenta factura = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFacturaVenta", con.conector);
			cmd.Parameters.AddWithValue("codventa", codventa);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					factura = new clsFacturaVenta();
					factura.CodFacturaVenta = dr.GetString(0);
					factura.CodAlmacen = dr.GetInt32(1);
					factura.CodTipoTransaccion = dr.GetInt32(2);
					factura.SiglaTransaccion = dr.GetString(3);
					factura.DescripcionTransaccion = dr.GetString(4);
					factura.CodTipoDocumento = dr.GetInt32(5);
					factura.SiglaDocumento = dr.GetString(6);
					factura.CodSerie = dr.GetInt32(7);
					factura.Serie = dr.GetString(8);
					factura.NumDoc = dr.GetString(9);
					factura.TipoCliente = dr.GetInt32(10);
					factura.CodCliente = dr.GetInt32(11);
					factura.DNI = dr.GetString(12);
					factura.RUCCliente = dr.GetString(13);
					factura.CodigoPersonalizado = dr.GetString(14);
					factura.RazonSocialCliente = dr.GetString(15);
					factura.Nombre = dr.GetString(16);
					factura.Direccion = dr.GetString(17);
					factura.Moneda = dr.GetInt32(18);
					factura.TipoCambio = dr.GetDouble(19);
					factura.FechaSalida = dr.GetDateTime(20);
					factura.Comentario = dr.GetString(21);
					factura.MontoBruto = dr.GetDecimal(22);
					factura.MontoDscto = dr.GetDecimal(23);
					factura.Igv = dr.GetDecimal(24);
					factura.Total = dr.GetDecimal(25);
					factura.Estado = dr.GetInt32(26);
					factura.FormaPago = dr.GetInt32(27);
					factura.FechaPago = dr.GetDateTime(28);
					factura.CodUser = dr.GetInt32(29);
					factura.FechaRegistro = dr.GetDateTime(30);
					factura.Pendiente = dr.GetDecimal(31);
					factura.CodNotaCredito = dr.GetInt32(32);
					factura.DocumentoReferencia = dr.GetString(33);
					factura.CodVendedor = dr.GetInt32(34);
					factura.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(35));
					factura.LineaCreditoCliente = dr.GetDouble(36);
					factura.CodCotizacion = Convert.ToInt32(dr.GetDecimal(37));
					factura.Motivo = dr.GetString(39);
					factura.Detallecomentario = dr.GetString(40);
					factura.Tipoventa = dr.GetInt32(41);
					factura.CodEmpresa = dr.GetInt32(42);
					factura.CodSucursal = dr.GetInt32(43);
					factura.Anulado = dr.GetInt16(44);
					factura.icbper = dr.GetDecimal(45);
					factura.valorRetencion = ((!dr.IsDBNull(46)) ? dr.GetInt32(46) : 0);
					factura.CodPedido = ((!dr.IsDBNull(47)) ? dr.GetInt32(47) : 0);
					factura.idTecnico = ((!dr.IsDBNull(48)) ? dr.GetInt32(48) : 0);
					factura.idZona = ((!dr.IsDBNull(49)) ? dr.GetInt32(49) : 0);
					factura.CodCanalVenta = (dr.IsDBNull(50) ? "0" : dr.GetString(50));
					factura.TieneNotaCredito = (dr.IsDBNull(51) ? "N" : dr.GetString(51));
					factura.UsuarioAnulador = dr.GetString(52);
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

	public clsFacturaVenta CargaFacturaVentaSegunOV(int codPedido)
	{
		clsFacturaVenta factura = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFacturaVentaSegunOV", con.conector);
			cmd.Parameters.AddWithValue("_codPedido", codPedido);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					factura = new clsFacturaVenta();
					factura.CodFacturaVenta = dr.GetString(0);
					factura.CodAlmacen = dr.GetInt32(1);
					factura.CodTipoTransaccion = dr.GetInt32(2);
					factura.SiglaTransaccion = dr.GetString(3);
					factura.DescripcionTransaccion = dr.GetString(4);
					factura.CodTipoDocumento = dr.GetInt32(5);
					factura.SiglaDocumento = dr.GetString(6);
					factura.CodSerie = dr.GetInt32(7);
					factura.Serie = dr.GetString(8);
					factura.NumDoc = dr.GetString(9);
					factura.TipoCliente = dr.GetInt32(10);
					factura.CodCliente = dr.GetInt32(11);
					factura.DNI = dr.GetString(12);
					factura.RUCCliente = dr.GetString(13);
					factura.CodigoPersonalizado = dr.GetString(14);
					factura.RazonSocialCliente = dr.GetString(15);
					factura.Nombre = dr.GetString(16);
					factura.Direccion = dr.GetString(17);
					factura.Moneda = dr.GetInt32(18);
					factura.TipoCambio = dr.GetDouble(19);
					factura.FechaSalida = dr.GetDateTime(20);
					factura.Comentario = dr.GetString(21);
					factura.MontoBruto = dr.GetDecimal(22);
					factura.MontoDscto = dr.GetDecimal(23);
					factura.Igv = dr.GetDecimal(24);
					factura.Total = dr.GetDecimal(25);
					factura.Estado = dr.GetInt32(26);
					factura.FormaPago = dr.GetInt32(27);
					factura.FechaPago = dr.GetDateTime(28);
					factura.CodUser = dr.GetInt32(29);
					factura.FechaRegistro = dr.GetDateTime(30);
					factura.Pendiente = dr.GetDecimal(31);
					factura.CodNotaCredito = dr.GetInt32(32);
					factura.DocumentoReferencia = dr.GetString(33);
					factura.CodVendedor = dr.GetInt32(34);
					factura.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(35));
					factura.LineaCreditoCliente = dr.GetDouble(36);
					factura.CodCotizacion = Convert.ToInt32(dr.GetDecimal(37));
					factura.Motivo = dr.GetString(39);
					factura.Detallecomentario = dr.GetString(40);
					factura.Tipoventa = dr.GetInt32(41);
					factura.CodEmpresa = dr.GetInt32(42);
					factura.CodSucursal = dr.GetInt32(43);
					factura.Anulado = dr.GetInt16(44);
					factura.icbper = dr.GetDecimal(45);
					factura.valorRetencion = ((!dr.IsDBNull(46)) ? dr.GetInt32(46) : 0);
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

	public DataTable ListaFacturaVenta(int codalmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codalmacen);
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

	public DataTable CargaDetalleVenta(int codventa, int codAlmacen, int guia)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codventa);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("guia", guia);
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

	public DataTable CargaDetalleVentaCodventa(clsFacturaVenta codventa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleFacturaVenta_Reporte_Excel", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codventa.CodVenta);
			cmd.Parameters.AddWithValue("codalma", codventa.CodAlmacen);
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

	public DataTable CargaDetalleCodventaxLineaFamilia(clsFacturaVenta codventa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleFacturaVenta_Reporte_Excel", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codventa.CodVenta);
			cmd.Parameters.AddWithValue("codalma", codventa.CodAlmacen);
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

	public DataTable MuestraCobros(int Estado, int codAlmacen, DateTime Fecha1, DateTime Fecha2, int codTipo, int codSucursal, int codFormaPago)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCobrosFacturaVenta1", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("alma", codAlmacen);
			cmd.Parameters.AddWithValue("fecha1", Fecha1.ToString("yyyy-MM-dd"));
			cmd.Parameters.AddWithValue("fecha2", Fecha2.ToString("yyyy-MM-dd"));
			cmd.Parameters.AddWithValue("esta", Estado);
			cmd.Parameters.AddWithValue("codtipo", codTipo);
			cmd.Parameters.AddWithValue("codsuc", codSucursal);
			cmd.Parameters.AddWithValue("codFormaP", codFormaPago);
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

	public string MuestraFechaPrimerCobro(int Estado, int codAlmacen, int codTipo, int codSucursal, int codFormaPago)
	{
		try
		{
			tabla = new DataTable();
			string fecha = null;
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFechaPrimerCobroFacturaVenta1", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("alma", codAlmacen);
			cmd.Parameters.AddWithValue("esta", Estado);
			cmd.Parameters.AddWithValue("codtipo", codTipo);
			cmd.Parameters.AddWithValue("codsuc", codSucursal);
			cmd.Parameters.AddWithValue("codFormaP", codFormaPago);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			if (tabla.Rows.Count > 0)
			{
				DataRow row = tabla.Rows[0];
				fecha = Convert.ToString(row["fecha"]);
			}
			return fecha;
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

	public DataTable DocumentosPorCliente(int CodCliente)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDocumentosPorCliente_FV", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcli", CodCliente);
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

	public DataTable Ventas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin, int codsucursal, int verifica, int codProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVentas_FV_2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicio);
			cmd.Parameters.AddWithValue("fechafin", FechaFin);
			cmd.Parameters.AddWithValue("codsucur", codsucursal);
			cmd.Parameters.AddWithValue("codproducto", codProducto);
			cmd.Parameters.AddWithValue("verifica", verifica);
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

	public double getTotalNotaCreditoSegunFechayAlmacen(int codAlmacen, DateTime desde, DateTime hasta, int codSucursal)
	{
		try
		{
			double total = double.NaN;
			con.conectarBD();
			cmd = new MySqlCommand("getTotalNotaCredito", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
			cmd.Parameters.AddWithValue("codsucur", codSucursal);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					total = dr.GetDouble(0);
				}
			}
			return total;
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

	public DataTable Ventasboletas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin, int codsucursal, int codtipdoc)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVentas_boletas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicio);
			cmd.Parameters.AddWithValue("fechafin", FechaFin);
			cmd.Parameters.AddWithValue("codsucur", codsucursal);
			cmd.Parameters.AddWithValue("_codtipo", codtipdoc);
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

	public DataTable VentasCodlineaCodfamilia(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin, int codsucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteMuestraVentas_FV", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicio);
			cmd.Parameters.AddWithValue("fechafin", FechaFin);
			cmd.Parameters.AddWithValue("codsucur", codsucursal);
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

	public DataTable MuestraGuiaVenta(int CodAlmacen, int CodCliente)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraGuiasFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codcliente", CodCliente);
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

	public DataTable MuestraDetalleGuiaVenta(int CodAlmacen, int codNota)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleGuiaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codSalida", codNota);
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

	public DataTable MuestraDetalleGuiaVenta2(int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleGuiaVenta2", con.conector);
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

	public DataTable MuestraDetalleGuia(int CodAlmacen, int codNota)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleGuiaVenta2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codnota", CodAlmacen);
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

	public bool insertdetalleventasalida(int codVen, int codSalida)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleVentaSalida", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codVen", codVen);
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

	public DataTable MuestraDetalleVentaGuia(int codventa, int codalmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaDetalleFacturaVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codventa);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
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

	public bool VistaSucursal(int codigoventa, int valor)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VistaSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codigoventa);
			cmd.Parameters.AddWithValue("valor", valor);
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

	public DataTable CargaDetalleVentaCredito(int codventa, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaDetalleVentaCredito", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codventa);
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

	public bool ActualizaPendienteCredito(decimal monto, int codnota, int codalm, int tipo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPendienteCredito", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("monto", monto);
			cmd.Parameters.AddWithValue("codnota", codnota);
			cmd.Parameters.AddWithValue("codalm", codalm);
			cmd.Parameters.AddWithValue("tipo", tipo);
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

	public DataTable ListaNotasDebito(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotaDebitoVenta", con.conector);
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

	public int chekeaImpresion(int codVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("chekeaImpresion", con.conector);
			cmd.Parameters.AddWithValue("codVenta", codVenta);
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

	public bool actualizaFactura_venta(int CodSerie, string txtSeries, string txtNumeros, string CodVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaFactura_venta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codser", CodSerie);
			cmd.Parameters.AddWithValue("numdoc", txtNumeros);
			cmd.Parameters.AddWithValue("ser", txtSeries);
			cmd.Parameters.AddWithValue("codfact", CodVenta);
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

	public DataTable ListaFacturas_ventaCliente(int codcli)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaFacturas_ventaCliente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcli", codcli);
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

	public bool updatensconsultext(clsFacturaVenta factura_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaFacturaVentaConsExt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", Convert.ToInt32(factura_venta.CodFacturaVenta));
			cmd.Parameters.AddWithValue("codsalconext", factura_venta.Codsalidaconsulext);
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

	public DataTable VentasDiarias(int codvendedor, int CodAlmacen, DateTime FechaInicio)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVentasDiarias", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicio);
			cmd.Parameters.AddWithValue("codvendedor", codvendedor);
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

	public DataTable VentasPendientesdedespacho(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVentasPendientesdedespacho", con.conector);
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

	public DataTable CargaDetallexEntregar(int codventa, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaDetallexEntregar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codventa);
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

	public int GetCantidadPendiente(int lista)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCantidadPendiente", con.conector);
			cmd.Parameters.AddWithValue("codigo", lista);
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

	public bool CambiaEstadoDetalle(int coddetalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CambiaEstadoDetalle", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddetalle_ex", coddetalle);
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

	public bool CambiaEstadoFactura(int CodVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CambiaEstadoFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CodVenta_ex", CodVenta);
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

	public DataTable despachosxventa(int codfactura)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDespachoxVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codfactura_ex", codfactura);
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

	public bool insertventaentregar(clsFacturaVenta factura_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaVentaEntregar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codfacturaventa_ex", factura_venta.CodFacturaVenta);
			if (factura_venta.CodCliente != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codcliente_ex", factura_venta.CodCliente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codcliente_ex", null);
			}
			oParam = cmd.Parameters.AddWithValue("codalmacen_ex", factura_venta.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codsucursal_ex", factura_venta.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("estado_ex", factura_venta.Estado);
			oParam = cmd.Parameters.AddWithValue("coduser_ex", factura_venta.CodUser);
			oParam = cmd.Parameters.AddWithValue("fecharegistro_ex", factura_venta.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			factura_venta.Codventaentregar = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalleventaentregar(clsDetalleFacturaVenta detalle_venta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleVentaentregar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codventaxentregar_ex", detalle_venta.Codventaentregar);
			oParam = cmd.Parameters.AddWithValue("codfacturaventa_ex", detalle_venta.CodVenta);
			oParam = cmd.Parameters.AddWithValue("coddetallefacturaventa_ex", detalle_venta.CodDetalleVenta);
			oParam = cmd.Parameters.AddWithValue("codproducto_ex", detalle_venta.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codunidad_ex", detalle_venta.CodUnidad);
			oParam = cmd.Parameters.AddWithValue("cantidad_ex", detalle_venta.Cantidad);
			oParam = cmd.Parameters.AddWithValue("coduser_ex", detalle_venta.CodUser);
			oParam = cmd.Parameters.AddWithValue("fecharegistro_ex", detalle_venta.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle_venta.CodDetalleVenta = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool VentaPendiente(int CodVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CambiarVentaPorPendiente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codventa", CodVenta);
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

	public bool ActualizaBoletaSunat(int codigoventa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaBoletaSunat", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codigoventa);
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

	public bool actualizaEstadoEnvio(int codigo, int codVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaEstadoEnvio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codVenta);
			cmd.Parameters.AddWithValue("codigo", codigo);
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

	public bool actualizaEstadoEnvioConError(int codigo, int codVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaEstadoEnvioConError", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codVenta);
			cmd.Parameters.AddWithValue("codigo", codigo);
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

	public bool ValidaAnulacionVenta(int codigoventa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaAnulacionVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codFacturaV_ex", codigoventa);
			if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
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

	public DataTable ReporteVentasResumido(DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ReportVentaResumidoTotalizado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fecha_inicio", desde);
			cmd.Parameters.AddWithValue("fecha_fin", hasta);
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

	public bool VerificaEstadoEnvioDocumentoElectronico(int codigoEmpresa, int codigoSucursal, int codigoAlmacen, int codigoFacturaVenta)
	{
		bool documentoEnviado = false;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaEstadoEnvioDocumentoElectronico", con.conector);
			cmd.Parameters.AddWithValue("codigo_empresa", codigoEmpresa);
			cmd.Parameters.AddWithValue("codigo_sucursal", codigoSucursal);
			cmd.Parameters.AddWithValue("codigo_almacen", codigoAlmacen);
			cmd.Parameters.AddWithValue("codigo_factura_venta", codigoFacturaVenta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					documentoEnviado = dr.GetBoolean(0);
				}
			}
			return documentoEnviado;
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

	public DataTable VentaSinRepositorio(int alma, DateTime fechaini, DateTime fechafin, int tipdoc)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MostrarVentaSinRepositorio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod_almacen", alma);
			cmd.Parameters.AddWithValue("fecha_inicio", fechaini);
			cmd.Parameters.AddWithValue("fecha_fin", fechafin);
			cmd.Parameters.AddWithValue("tipdoc", tipdoc);
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

	public clsFacturaVenta CargaFacturaVentaRegeneracion(int codventa)
	{
		clsFacturaVenta factura = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraFacturaVenta_Regeneracion", con.conector);
			cmd.Parameters.AddWithValue("codventa", codventa);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					factura = new clsFacturaVenta();
					factura.CodFacturaVenta = dr.GetString(0);
					factura.CodAlmacen = dr.GetInt32(1);
					factura.CodTipoTransaccion = dr.GetInt32(2);
					factura.SiglaTransaccion = dr.GetString(3);
					factura.DescripcionTransaccion = dr.GetString(4);
					factura.CodTipoDocumento = dr.GetInt32(5);
					factura.SiglaDocumento = dr.GetString(6);
					factura.CodSerie = dr.GetInt32(7);
					factura.Serie = dr.GetString(8);
					factura.NumDoc = dr.GetString(9);
					factura.TipoCliente = dr.GetInt32(10);
					factura.CodCliente = dr.GetInt32(11);
					factura.DNI = dr.GetString(12);
					factura.RUCCliente = dr.GetString(13);
					factura.CodigoPersonalizado = dr.GetString(14);
					factura.RazonSocialCliente = dr.GetString(15);
					factura.Nombre = dr.GetString(16);
					factura.Direccion = dr.GetString(17);
					factura.Moneda = dr.GetInt32(18);
					factura.TipoCambio = dr.GetDouble(19);
					factura.FechaSalida = dr.GetDateTime(20);
					factura.Comentario = dr.GetString(21);
					factura.MontoBruto = dr.GetDecimal(22);
					factura.MontoDscto = dr.GetDecimal(23);
					factura.Igv = dr.GetDecimal(24);
					factura.Total = dr.GetDecimal(25);
					factura.Estado = dr.GetInt32(26);
					factura.FormaPago = dr.GetInt32(27);
					factura.FechaPago = dr.GetDateTime(28);
					factura.CodUser = dr.GetInt32(29);
					factura.FechaRegistro = dr.GetDateTime(30);
					factura.Pendiente = dr.GetDecimal(31);
					factura.CodNotaCredito = dr.GetInt32(32);
					factura.DocumentoReferencia = dr.GetString(33);
					factura.CodVendedor = dr.GetInt32(34);
					factura.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(35));
					factura.LineaCreditoCliente = dr.GetDouble(36);
					factura.CodCotizacion = Convert.ToInt32(dr.GetDecimal(37));
					factura.Motivo = dr.GetString(39);
					factura.Detallecomentario = dr.GetString(40);
					factura.Tipoventa = dr.GetInt32(41);
					factura.CodEmpresa = dr.GetInt32(42);
					factura.CodSucursal = dr.GetInt32(43);
					factura.NumeroDocumentoCliente = dr.GetString(44);
					factura.CodigoDocumentoIdentidad = dr.GetInt32(45);
					factura.empresa = new clsEmpresa
					{
						Ruc = Convert.ToString(dr["ruc"]),
						UsuarioSunat = Convert.ToString(dr["usuariosunat"]),
						ClaveSunat = Convert.ToString(dr["clavesunat"]),
						Url = Convert.ToString(dr["url"])
					};
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

	public DataTable CargaDetalle_Regeneracion(int codventa, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleFacturaVenta_Regeneracion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codventa", codventa);
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

	public bool AnulaDetalleVenta(int codigoDetalle, int codproducto)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnulaDetalleVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_cod", codigoDetalle);
			cmd.Parameters.AddWithValue("_codprod", codproducto);
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

	public DataTable ResumenDiario(DateTime dia, int codSucursal, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GetResumenDiario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("dia_ex", dia);
			cmd.Parameters.AddWithValue("codSucursal_ex", codSucursal);
			cmd.Parameters.AddWithValue("codAlmacen_ex", codAlmacen);
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

	public int getCantidadResumen(DateTime dia, int codSucursal, int codAlmacen)
	{
		clsFacturaVenta factura = null;
		try
		{
			con.conectarBD();
			int cantidad = 0;
			cmd = new MySqlCommand("getCantidadResumen", con.conector);
			cmd.Parameters.AddWithValue("dia_ex", dia);
			cmd.Parameters.AddWithValue("codSucursal_ex", codSucursal);
			cmd.Parameters.AddWithValue("codAlmacen_ex", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cantidad = dr.GetInt32(0);
				}
			}
			return cantidad;
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

	public DataTable ReporteAnalisisDetalladoVenta(string buscar, DateTime desde, DateTime hasta, string codAnalisis, string codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand(buscar, con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_desde", desde);
			cmd.Parameters.AddWithValue("_hasta", hasta);
			cmd.Parameters.AddWithValue("_codAnalisis", codAnalisis);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
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

	public bool actualizaCanalVenta(string codigo, int codVenta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaCanalVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("canalventa", codigo);
			cmd.Parameters.AddWithValue("codventa", codVenta);
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

	public bool actualizacuentabanco(int codbanco, int codcuenta, string numcuenta, int codventa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actaulizaCuentaBancoPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codbanco", codbanco);
			cmd.Parameters.AddWithValue("codcuenta", codcuenta);
			cmd.Parameters.AddWithValue("numcuenta", numcuenta);
			cmd.Parameters.AddWithValue("codventa", codventa);
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
