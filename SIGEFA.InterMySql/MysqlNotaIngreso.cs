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

internal class MysqlNotaIngreso : INotaIngreso
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsNotaIngreso nota)
	{
		try
		{
			con.conectarBD();
			string msj = "";
			cmd = new MySqlCommand("GuardaNotaIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			oParam = cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("numdoc", nota.NumDoc);
			if (nota.CodProveedor != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codprov", nota.CodProveedor);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codprov", null);
			}
			oParam = cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", nota.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechaingreso", nota.FechaIngreso);
			string comentario = ((nota.Comentario.Length > 60) ? nota.Comentario.Substring(0, 60) : nota.Comentario);
			oParam = cmd.Parameters.AddWithValue("comentario", comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", nota.Igv);
			oParam = cmd.Parameters.AddWithValue("flete", nota.Flete);
			oParam = cmd.Parameters.AddWithValue("total", nota.Total);
			oParam = cmd.Parameters.AddWithValue("pend", nota.Total);
			oParam = cmd.Parameters.AddWithValue("estado", nota.Estado);
			oParam = cmd.Parameters.AddWithValue("recibido", nota.Recibido);
			if (nota.FormaPago != 0)
			{
				oParam = cmd.Parameters.AddWithValue("formapago", nota.FormaPago);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParam = cmd.Parameters.AddWithValue("fechapago", nota.FechaPago);
			oParam = cmd.Parameters.AddWithValue("fechacancelado", nota.FechaCancelado);
			oParam = cmd.Parameters.AddWithValue("cancelado", nota.Cancelado);
			oParam = cmd.Parameters.AddWithValue("codusu", nota.CodUser);
			oParam = cmd.Parameters.AddWithValue("codref", nota.CodReferencia);
			oParam = cmd.Parameters.AddWithValue("codser", nota.CodSerie);
			oParam = cmd.Parameters.AddWithValue("serie", nota.Serie);
			oParam = cmd.Parameters.AddWithValue("CodOrd", nota.CodOrdenCompra);
			oParam = cmd.Parameters.AddWithValue("codalmacenemisor_ex", nota.codalmacenemisor);
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
			oParam = cmd.Parameters.AddWithValue("codTransferencia_ex", nota.Codtransferencia);
			oParam = cmd.Parameters.AddWithValue("codguiaremision", nota.codguia);
			oParam = cmd.Parameters.AddWithValue("_responsable", nota.responsable);
			oParam = cmd.Parameters.AddWithValue("_area", nota.area);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			nota.CodNotaIngreso = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool insertingresoguia(clsNotaIngreso nota)
	{
		try
		{
			con.conectarBD();
			string msj = "";
			cmd = new MySqlCommand("GuardaNotaIngresoGuia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			oParam = cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codserie", nota.CodSerie);
			oParam = cmd.Parameters.AddWithValue("numdoc", nota.NumDoc);
			if (nota.CodProveedor != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codprov", nota.CodProveedor);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codprov", null);
			}
			oParam = cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			oParam = cmd.Parameters.AddWithValue("fechaingreso", nota.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("comentario", nota.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", nota.Igv);
			oParam = cmd.Parameters.AddWithValue("total", nota.Total);
			oParam = cmd.Parameters.AddWithValue("pend", nota.Total);
			oParam = cmd.Parameters.AddWithValue("estado", nota.Estado);
			oParam = cmd.Parameters.AddWithValue("codusu", nota.CodUser);
			oParam = cmd.Parameters.AddWithValue("CodOrd", nota.CodOrdenCompra);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			nota.CodNotaIngreso = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool insertarNota(clsNotaIngreso nota)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			string msj = "";
			cmd = new MySqlCommand("GuardaNotaIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamNI = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			oParamNI = cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			oParamNI = cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			oParamNI = cmd.Parameters.AddWithValue("numdoc", nota.NumDoc);
			if (nota.CodProveedor != 0)
			{
				oParamNI = cmd.Parameters.AddWithValue("codprov", nota.CodProveedor);
			}
			else
			{
				oParamNI = cmd.Parameters.AddWithValue("codprov", null);
			}
			oParamNI = cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			oParamNI = cmd.Parameters.AddWithValue("tipocambio", nota.TipoCambio);
			oParamNI = cmd.Parameters.AddWithValue("fechaingreso", nota.FechaIngreso);
			oParamNI = cmd.Parameters.AddWithValue("comentario", nota.Comentario);
			oParamNI = cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			oParamNI = cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			oParamNI = cmd.Parameters.AddWithValue("igv", nota.Igv);
			oParamNI = cmd.Parameters.AddWithValue("flete", nota.Flete);
			oParamNI = cmd.Parameters.AddWithValue("total", nota.Total);
			oParamNI = cmd.Parameters.AddWithValue("pend", nota.Total);
			oParamNI = cmd.Parameters.AddWithValue("estado", nota.Estado);
			oParamNI = cmd.Parameters.AddWithValue("recibido", nota.Recibido);
			if (nota.FormaPago != 0)
			{
				oParamNI = cmd.Parameters.AddWithValue("formapago", nota.FormaPago);
			}
			else
			{
				oParamNI = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParamNI = cmd.Parameters.AddWithValue("fechapago", nota.FechaPago);
			oParamNI = cmd.Parameters.AddWithValue("fechacancelado", nota.FechaCancelado);
			oParamNI = cmd.Parameters.AddWithValue("cancelado", nota.Cancelado);
			oParamNI = cmd.Parameters.AddWithValue("codusu", nota.CodUser);
			oParamNI = cmd.Parameters.AddWithValue("codref", nota.CodReferencia);
			oParamNI = cmd.Parameters.AddWithValue("codser", nota.CodSerie);
			oParamNI = cmd.Parameters.AddWithValue("serie", nota.Serie);
			oParamNI = cmd.Parameters.AddWithValue("CodOrd", nota.CodOrdenCompra);
			oParamNI = cmd.Parameters.AddWithValue("codalmacenemisor_ex", nota.codalmacenemisor);
			oParamNI = cmd.Parameters.AddWithValue("aplicad", nota.Aplicada);
			if (nota.Aplicada != 0)
			{
				oParamNI = cmd.Parameters.AddWithValue("codaplicad", nota.CodAplicada);
			}
			else
			{
				oParamNI = cmd.Parameters.AddWithValue("codaplicad", null);
			}
			if (nota.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", nota.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			oParamNI = cmd.Parameters.AddWithValue("codTransferencia_ex", nota.Codtransferencia);
			oParamNI = cmd.Parameters.AddWithValue("codguiaremision", nota.codguia);
			oParamNI = cmd.Parameters.AddWithValue("_responsable", nota.responsable);
			oParamNI = cmd.Parameters.AddWithValue("_area", nota.area);
			oParamNI = cmd.Parameters.AddWithValue("newid", 0);
			oParamNI.Direction = ParameterDirection.Output;
			int xNI = cmd.ExecuteNonQuery();
			nota.CodNotaIngreso = Convert.ToString(cmd.Parameters["newid"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)nota.CodNotaIngreso, (Func<char, bool>)char.IsDigit) || nota.CodNotaIngreso == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetalleNotaIngreso detalle in nota.Detalle)
			{
				cmd = new MySqlCommand("GuardaDetalleIngreso", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParamDNI = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
				oParamDNI = cmd.Parameters.AddWithValue("codnota", nota.CodNotaIngreso);
				oParamDNI = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
				oParamDNI = cmd.Parameters.AddWithValue("moneda", detalle.Moneda);
				oParamDNI = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
				oParamDNI = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
				oParamDNI = cmd.Parameters.AddWithValue("canti", detalle.Cantidad);
				oParamDNI = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
				oParamDNI = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
				oParamDNI = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
				oParamDNI = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
				oParamDNI = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
				oParamDNI = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
				oParamDNI = cmd.Parameters.AddWithValue("igv", detalle.Igv);
				oParamDNI = cmd.Parameters.AddWithValue("flete", detalle.Flete);
				oParamDNI = cmd.Parameters.AddWithValue("importe", detalle.Importe);
				oParamDNI = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
				oParamDNI = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
				oParamDNI = cmd.Parameters.AddWithValue("fecha", detalle.FechaIngreso);
				oParamDNI = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
				oParamDNI = cmd.Parameters.AddWithValue("valorrealS", detalle.ValorrealSoles);
				oParamDNI = cmd.Parameters.AddWithValue("codrequer", detalle.CodDetalleRequerimiento);
				oParamDNI = cmd.Parameters.AddWithValue("bonific", detalle.Bonificacion);
				oParamDNI = cmd.Parameters.AddWithValue("codguiaremision", detalle.codguiaremision);
				oParamDNI = cmd.Parameters.AddWithValue("newid", 0);
				oParamDNI.Direction = ParameterDirection.Output;
				int xDNI = cmd.ExecuteNonQuery();
				detalle.CodDetalleIngreso = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.CodDetalleIngreso == 0)
				{
					rpta = false;
					Transaction.Current.Rollback();
					Scope.Dispose();
					return rpta;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			Scope.Complete();
			Scope.Dispose();
			return rpta;
		}
		catch (MySqlException)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool insertarNotayFactura(clsNotaIngreso nota, clsFactura factura)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			string msj = "";
			cmd = new MySqlCommand("GuardaNotaIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamNI = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			oParamNI = cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			oParamNI = cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			oParamNI = cmd.Parameters.AddWithValue("numdoc", nota.NumDoc);
			if (nota.CodProveedor != 0)
			{
				oParamNI = cmd.Parameters.AddWithValue("codprov", nota.CodProveedor);
			}
			else
			{
				oParamNI = cmd.Parameters.AddWithValue("codprov", null);
			}
			oParamNI = cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			oParamNI = cmd.Parameters.AddWithValue("tipocambio", nota.TipoCambio);
			oParamNI = cmd.Parameters.AddWithValue("fechaingreso", nota.FechaIngreso);
			oParamNI = cmd.Parameters.AddWithValue("comentario", nota.Comentario);
			oParamNI = cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			oParamNI = cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			oParamNI = cmd.Parameters.AddWithValue("igv", nota.Igv);
			oParamNI = cmd.Parameters.AddWithValue("flete", nota.Flete);
			oParamNI = cmd.Parameters.AddWithValue("total", nota.Total);
			oParamNI = cmd.Parameters.AddWithValue("pend", nota.Total);
			oParamNI = cmd.Parameters.AddWithValue("estado", nota.Estado);
			oParamNI = cmd.Parameters.AddWithValue("recibido", nota.Recibido);
			if (nota.FormaPago != 0)
			{
				oParamNI = cmd.Parameters.AddWithValue("formapago", nota.FormaPago);
			}
			else
			{
				oParamNI = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParamNI = cmd.Parameters.AddWithValue("fechapago", nota.FechaPago);
			oParamNI = cmd.Parameters.AddWithValue("fechacancelado", nota.FechaCancelado);
			oParamNI = cmd.Parameters.AddWithValue("cancelado", nota.Cancelado);
			oParamNI = cmd.Parameters.AddWithValue("codusu", nota.CodUser);
			oParamNI = cmd.Parameters.AddWithValue("codref", nota.CodReferencia);
			oParamNI = cmd.Parameters.AddWithValue("codser", nota.CodSerie);
			oParamNI = cmd.Parameters.AddWithValue("serie", nota.Serie);
			oParamNI = cmd.Parameters.AddWithValue("CodOrd", nota.CodOrdenCompra);
			oParamNI = cmd.Parameters.AddWithValue("codalmacenemisor_ex", nota.codalmacenemisor);
			oParamNI = cmd.Parameters.AddWithValue("aplicad", nota.Aplicada);
			if (nota.Aplicada != 0)
			{
				oParamNI = cmd.Parameters.AddWithValue("codaplicad", nota.CodAplicada);
			}
			else
			{
				oParamNI = cmd.Parameters.AddWithValue("codaplicad", null);
			}
			if (nota.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", nota.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			oParamNI = cmd.Parameters.AddWithValue("codTransferencia_ex", nota.Codtransferencia);
			oParamNI = cmd.Parameters.AddWithValue("codguiaremision", nota.codguia);
			oParamNI = cmd.Parameters.AddWithValue("_responsable", nota.responsable);
			oParamNI = cmd.Parameters.AddWithValue("_area", nota.area);
			oParamNI = cmd.Parameters.AddWithValue("newid", 0);
			oParamNI.Direction = ParameterDirection.Output;
			int xNI = cmd.ExecuteNonQuery();
			nota.CodNotaIngreso = Convert.ToString(cmd.Parameters["newid"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)nota.CodNotaIngreso, (Func<char, bool>)char.IsDigit) || nota.CodNotaIngreso == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetalleNotaIngreso detalle in nota.Detalle)
			{
				cmd = new MySqlCommand("GuardaDetalleIngreso", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParamDNI = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
				oParamDNI = cmd.Parameters.AddWithValue("codnota", nota.CodNotaIngreso);
				oParamDNI = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
				oParamDNI = cmd.Parameters.AddWithValue("moneda", detalle.Moneda);
				oParamDNI = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
				oParamDNI = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
				oParamDNI = cmd.Parameters.AddWithValue("canti", detalle.Cantidad);
				oParamDNI = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
				oParamDNI = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
				oParamDNI = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
				oParamDNI = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
				oParamDNI = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
				oParamDNI = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
				oParamDNI = cmd.Parameters.AddWithValue("igv", detalle.Igv);
				oParamDNI = cmd.Parameters.AddWithValue("flete", detalle.Flete);
				oParamDNI = cmd.Parameters.AddWithValue("importe", detalle.Importe);
				oParamDNI = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
				oParamDNI = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
				oParamDNI = cmd.Parameters.AddWithValue("fecha", detalle.FechaIngreso);
				oParamDNI = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
				oParamDNI = cmd.Parameters.AddWithValue("valorrealS", detalle.ValorrealSoles);
				oParamDNI = cmd.Parameters.AddWithValue("codrequer", detalle.CodDetalleRequerimiento);
				oParamDNI = cmd.Parameters.AddWithValue("bonific", detalle.Bonificacion);
				oParamDNI = cmd.Parameters.AddWithValue("codguiaremision", detalle.codguiaremision);
				oParamDNI = cmd.Parameters.AddWithValue("_estado", detalle.stado);
				oParamDNI = cmd.Parameters.AddWithValue("newid", 0);
				oParamDNI.Direction = ParameterDirection.Output;
				int xDNI = cmd.ExecuteNonQuery();
				detalle.CodDetalleIngreso = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.CodDetalleIngreso == 0)
				{
					rpta = false;
					Transaction.Current.Rollback();
					Scope.Dispose();
					return rpta;
				}
			}
			cmd = new MySqlCommand("GuardaFactura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamF = cmd.Parameters.AddWithValue("docfactura", factura.DocumentoFactura);
			oParamF = cmd.Parameters.AddWithValue("codtran", factura.CodTipoTransaccion);
			oParamF = cmd.Parameters.AddWithValue("codtipo", factura.CodTipoDocumento);
			oParamF = cmd.Parameters.AddWithValue("numdoc", factura.NumFac);
			oParamF = cmd.Parameters.AddWithValue("moneda", factura.Moneda);
			oParamF = cmd.Parameters.AddWithValue("tipocambio", factura.TipoCambio);
			oParamF = cmd.Parameters.AddWithValue("fechaingreso", factura.FechaIngreso);
			oParamF = cmd.Parameters.AddWithValue("comentario", factura.Comentario);
			oParamF = cmd.Parameters.AddWithValue("bruto", factura.MontoBruto);
			oParamF = cmd.Parameters.AddWithValue("montodscto", factura.MontoDscto);
			oParamF = cmd.Parameters.AddWithValue("igv", factura.Igv);
			oParamF = cmd.Parameters.AddWithValue("flete", factura.Flete);
			oParamF = cmd.Parameters.AddWithValue("total", factura.Total);
			oParamF = cmd.Parameters.AddWithValue("pendiente", factura.Total);
			oParamF = cmd.Parameters.AddWithValue("estado", factura.Estado);
			oParamF = cmd.Parameters.AddWithValue("recibido", factura.Recibido);
			if (factura.FormaPago != 0)
			{
				oParamF = cmd.Parameters.AddWithValue("formapago", factura.FormaPago);
			}
			else
			{
				oParamF = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParamF = cmd.Parameters.AddWithValue("fechapago", factura.FechaPago);
			oParamF = cmd.Parameters.AddWithValue("fechacancelado", factura.FechaCancelado);
			oParamF = cmd.Parameters.AddWithValue("cancelado", factura.Cancelado);
			oParamF = cmd.Parameters.AddWithValue("codusu", factura.CodUser);
			oParamF = cmd.Parameters.AddWithValue("codref", factura.CodReferencia);
			oParamF = cmd.Parameters.AddWithValue("codser", factura.CodSerie);
			oParamF = cmd.Parameters.AddWithValue("serie", factura.Serie);
			oParamF = cmd.Parameters.AddWithValue("codpro", factura.CodProveedor);
			oParamF = cmd.Parameters.AddWithValue("codalma", factura.CodAlmacen);
			if (factura.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", factura.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			oParamF = cmd.Parameters.AddWithValue("codNotaI_ex", nota.CodNotaIngreso);
			oParamF = cmd.Parameters.AddWithValue("newid", 0);
			oParamF.Direction = ParameterDirection.Output;
			int xF = cmd.ExecuteNonQuery();
			factura.CodFacturaNueva = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (factura.CodFacturaNueva == 0)
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetalleFactura detalle2 in factura.Detalle)
			{
				cmd = new MySqlCommand("GuardaDetalleFactura", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle2.CodProducto);
				oParam = cmd.Parameters.AddWithValue("codfactura", factura.CodFacturaNueva);
				oParam = cmd.Parameters.AddWithValue("codnota", nota.CodNotaIngreso);
				oParam = cmd.Parameters.AddWithValue("moneda", detalle2.Moneda);
				oParam = cmd.Parameters.AddWithValue("unidad", detalle2.UnidadIngresada);
				oParam = cmd.Parameters.AddWithValue("serielote", detalle2.SerieLote);
				oParam = cmd.Parameters.AddWithValue("precio", detalle2.PrecioUnitario);
				oParam = cmd.Parameters.AddWithValue("subtotal", detalle2.Subtotal);
				oParam = cmd.Parameters.AddWithValue("dscto1", detalle2.Descuento1);
				oParam = cmd.Parameters.AddWithValue("dscto2", detalle2.Descuento2);
				oParam = cmd.Parameters.AddWithValue("dscto3", detalle2.Descuento3);
				oParam = cmd.Parameters.AddWithValue("montodscto", detalle2.MontoDescuento);
				oParam = cmd.Parameters.AddWithValue("igv", detalle2.Igv);
				oParam = cmd.Parameters.AddWithValue("flete", detalle2.Flete);
				oParam = cmd.Parameters.AddWithValue("importe", detalle2.Importe);
				oParam = cmd.Parameters.AddWithValue("precioreal", detalle2.PrecioReal);
				oParam = cmd.Parameters.AddWithValue("valoreal", detalle2.ValoReal);
				oParam = cmd.Parameters.AddWithValue("fecha", detalle2.FechaIngreso);
				oParam = cmd.Parameters.AddWithValue("codusu", detalle2.CodUser);
				oParam = cmd.Parameters.AddWithValue("codalma", detalle2.CodAlmacen);
				oParam = cmd.Parameters.AddWithValue("cant", detalle2.Cantidad);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDF = cmd.ExecuteNonQuery();
				detalle2.CodDetalleFactura = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle2.CodDetalleFactura == 0)
				{
					rpta = false;
					Transaction.Current.Rollback();
					Scope.Dispose();
					return rpta;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			Scope.Complete();
			Scope.Dispose();
			return rpta;
		}
		catch (MySqlException)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool ActualizaCantidadPendiente(double cantidad, int produc, int CodOrden, int coddeta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaOrdenCompraCantidadPendiente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cant", cantidad);
			cmd.Parameters.AddWithValue("codpro", produc);
			cmd.Parameters.AddWithValue("codord", CodOrden);
			cmd.Parameters.AddWithValue("coddeta", coddeta);
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

	public bool ActualizaCantidadPendiente2(double cantidad, int produc, int alma, int coduser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteProductoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cant", cantidad);
			cmd.Parameters.AddWithValue("codpro", produc);
			cmd.Parameters.AddWithValue("alma", alma);
			cmd.Parameters.AddWithValue("coduser", coduser);
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

	public bool ActualizaCodNotaIngreso(double cantidad, int produc, int CodDetalle, int tipo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCodNotaIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cant", cantidad);
			cmd.Parameters.AddWithValue("codpro", produc);
			cmd.Parameters.AddWithValue("CodDetalle", CodDetalle);
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

	public bool update(clsNotaIngreso nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNotaIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", Convert.ToInt32(nota.CodNotaIngreso));
			cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			cmd.Parameters.AddWithValue("numdoc", nota.NumDoc);
			if (nota.CodProveedor != 0)
			{
				cmd.Parameters.AddWithValue("codprov", nota.CodProveedor);
			}
			else
			{
				cmd.Parameters.AddWithValue("codprov", null);
			}
			cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			cmd.Parameters.AddWithValue("tipocambio", nota.TipoCambio);
			cmd.Parameters.AddWithValue("fechaingreso", nota.FechaIngreso);
			cmd.Parameters.AddWithValue("comentario", nota.Comentario);
			cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			cmd.Parameters.AddWithValue("igv", nota.Igv);
			cmd.Parameters.AddWithValue("flete", nota.Flete);
			cmd.Parameters.AddWithValue("total", nota.Total);
			cmd.Parameters.AddWithValue("estado", nota.Estado);
			cmd.Parameters.AddWithValue("recibido", nota.Recibido);
			cmd.Parameters.AddWithValue("formapago", nota.FormaPago);
			cmd.Parameters.AddWithValue("fechapago", nota.FechaPago);
			cmd.Parameters.AddWithValue("fechacancelado", nota.FechaCancelado);
			cmd.Parameters.AddWithValue("cancelado", nota.Cancelado);
			cmd.Parameters.AddWithValue("_ordenOC", nota.ordenOC);
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
			cmd = new MySqlCommand("EliminarNotaIngreso", con.conector);
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

	public bool anular(int CodSerie, string NumDoc, string text)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularNotaIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod", CodSerie);
			cmd.Parameters.AddWithValue("numero", NumDoc);
			cmd.Parameters.AddWithValue("comentario", text);
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

	public bool anular1(int codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularNotaIngreso1", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", codigo);
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
			cmd = new MySqlCommand("ActivarNotaIngreso", con.conector);
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

	public bool atender(int codigo, int CodSerie, string NumDoc, int User)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AtenderTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codigo", codigo);
			cmd.Parameters.AddWithValue("cod", CodSerie);
			cmd.Parameters.AddWithValue("numero", NumDoc);
			cmd.Parameters.AddWithValue("Usu", User);
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

	public bool insertdetalle(clsDetalleNotaIngreso detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codnota", detalle.CodNotaIngreso);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("moneda", detalle.Moneda);
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
			oParam = cmd.Parameters.AddWithValue("flete", detalle.Flete);
			oParam = cmd.Parameters.AddWithValue("importe", detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("fecha", detalle.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("valorrealS", detalle.ValorrealSoles);
			oParam = cmd.Parameters.AddWithValue("codrequer", detalle.CodDetalleRequerimiento);
			oParam = cmd.Parameters.AddWithValue("bonific", detalle.Bonificacion);
			oParam = cmd.Parameters.AddWithValue("codguiaremision", detalle.codguiaremision);
			oParam = cmd.Parameters.AddWithValue("_estado", detalle.stado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleIngreso = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException ex)
		{
			MessageBox.Show(ex.Message ?? "", "");
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool insertdetalleConsolidado(int orden, int nota, int codAlma, int codUsu)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleConsolidadoNota", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codord", orden);
			oParam = cmd.Parameters.AddWithValue("codnota", nota);
			oParam = cmd.Parameters.AddWithValue("alma", codAlma);
			oParam = cmd.Parameters.AddWithValue("usu", codUsu);
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

	public bool updatedetalle(clsDetalleNotaIngreso detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleIngreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle.CodDetalleIngreso);
			cmd.Parameters.AddWithValue("moneda", detalle.Moneda);
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
			cmd.Parameters.AddWithValue("flete", detalle.Flete);
			cmd.Parameters.AddWithValue("importe", detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			cmd.Parameters.AddWithValue("fecha", detalle.FechaIngreso);
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

	public bool deleteConsolidado(int codalma, int codusu)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarConsolidadoNota", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codusu", codusu);
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
			cmd = new MySqlCommand("EliminarDetalleIngreso", con.conector);
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

	public clsNotaIngreso CargaNotaIngreso(int CodNota)
	{
		clsNotaIngreso nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaIngreso", con.conector);
			cmd.Parameters.AddWithValue("codnota", CodNota);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaIngreso();
					nota.CodNotaIngreso = dr.GetString(0);
					nota.CodAlmacen = Convert.ToInt32(dr.GetDecimal(1));
					nota.CodTipoTransaccion = Convert.ToInt32(dr.GetDecimal(2));
					nota.SiglaTransaccion = dr.GetString(3);
					nota.DescripcionTransaccion = dr.GetString(4);
					nota.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(5));
					nota.SiglaDocumento = dr.GetString(6);
					nota.NumDoc = dr.GetString(7);
					nota.CodProveedor = Convert.ToInt32(dr.GetString(8));
					nota.RUCProveedor = dr.GetString(9);
					nota.RazonSocialProveedor = dr.GetString(10);
					nota.Moneda = Convert.ToInt32(dr.GetString(11));
					nota.TipoCambio = dr.GetDouble(12);
					nota.FechaIngreso = dr.GetDateTime(13);
					nota.Comentario = dr.GetString(14);
					nota.MontoBruto = dr.GetDouble(15);
					nota.MontoDscto = dr.GetDouble(16);
					nota.Igv = dr.GetDouble(17);
					nota.Total = dr.GetDouble(18);
					nota.Abonado = dr.GetDouble(19);
					nota.Pendiente = dr.GetDouble(20);
					nota.FormaPago = Convert.ToInt32(dr.GetString(23));
					nota.FechaPago = dr.GetDateTime(24);
					nota.Cancelado = Convert.ToInt32(dr.GetDecimal(25));
					nota.CodUser = Convert.ToInt32(dr.GetDecimal(26));
					nota.FechaRegistro = dr.GetDateTime(27);
					nota.CodSerie = Convert.ToInt32(dr.GetDecimal(28));
					nota.Serie = dr.GetString(29);
					nota.CodReferencia = Convert.ToInt32(dr.GetDecimal(30));
					nota.Flete = dr.GetDouble(31);
					nota.SDocumentoOrden = dr.GetString(32);
					nota.codalmacenemisor = dr.GetInt32(33);
					nota.Codconductor = dr.GetInt32(34);
					nota.Codvehiculotransporte = dr.GetInt32(35);
					nota.Estado = Convert.ToInt32(dr.GetDecimal(21));
					nota.Recibido = Convert.ToInt32(dr.GetDecimal(22));
					nota.Aplicada = Convert.ToInt32(dr.GetString(36));
					nota.CodAplicada = dr.GetInt32(37);
					nota.Motivo = dr.GetString(38);
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

	public clsNotaSalida buscaNotaIngreso(int CodNota)
	{
		clsNotaSalida nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaIngreso", con.conector);
			cmd.Parameters.AddWithValue("codnota", CodNota);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaSalida();
					nota.CodNotaSalida = dr.GetString(0);
					nota.CodAlmacen = Convert.ToInt32(dr.GetDecimal(1));
					nota.CodTipoTransaccion = Convert.ToInt32(dr.GetDecimal(2));
					nota.SiglaTransaccion = dr.GetString(3);
					nota.DescripcionTransaccion = dr.GetString(4);
					nota.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(5));
					nota.SiglaDocumento = dr.GetString(6);
					nota.NumDoc = dr.GetString(7);
					nota.CodProveedor = Convert.ToInt32(dr.GetString(8));
					nota.RUCCliente = dr.GetString(9);
					nota.RazonSocialCliente = dr.GetString(10);
					nota.Moneda = Convert.ToInt32(dr.GetString(11));
					nota.TipoCambio = dr.GetDouble(12);
					nota.FechaSalida = dr.GetDateTime(13);
					nota.Comentario = dr.GetString(14);
					nota.MontoBruto = dr.GetDouble(15);
					nota.MontoDscto = dr.GetDouble(16);
					nota.Igv = dr.GetDouble(17);
					nota.Total = dr.GetDouble(18);
					nota.Abonado = dr.GetDouble(19);
					nota.Pendiente = dr.GetDouble(20);
					nota.FormaPago = Convert.ToInt32(dr.GetString(23));
					nota.FechaPago = dr.GetDateTime(24);
					nota.Cancelado = Convert.ToInt32(dr.GetDecimal(25));
					nota.CodUser = Convert.ToInt32(dr.GetDecimal(26));
					nota.FechaRegistro = dr.GetDateTime(27);
					nota.CodSerie = Convert.ToInt32(dr.GetDecimal(28));
					nota.Serie = dr.GetString(29);
					nota.Estado = Convert.ToInt32(dr.GetDecimal(21));
					nota.Aplicada = Convert.ToInt32(dr.GetString(36));
					nota.CodAplicada = dr.GetInt32(37);
					nota.Motivo = dr.GetString(38);
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
			cmd = new MySqlCommand("MuestraDetalleNotaIngreso", con.conector);
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

	public DataTable CargaDetalleSinEstado(int codNotaIngreso)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleNotaIngresoSinEstado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", codNotaIngreso);
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

	public DataTable CargaDetalleTransferencia(int codigotransferencia)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("BuscaDetalleTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod", codigotransferencia);
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

	public bool UpdateComentario(clsNotaIngreso nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNotaIngresoComentario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnota", Convert.ToInt32(nota.CodNotaIngreso));
			cmd.Parameters.AddWithValue("comentario", nota.Comentario);
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

	public DataTable ListaNotasIngreso(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int tipoFecha)
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
			cmd.Parameters.AddWithValue("tipoFecha", tipoFecha);
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

	public DataTable reporteinventario(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteInventarioNotas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public DataTable busquedapornumero(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, string numero)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("busquedapornumero", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("num", numero);
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

	public DataTable MuestraPagos(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2)
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

	public DataTable MuestraOrdenAlmacen(int codAlmacen, int codUsu)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraOrdenAlmacen", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("usu", codUsu);
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

	public DataTable MuestraNotaIngresoOrden(int codAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaIngresoOrden", con.conector);
			cmd.Parameters.AddWithValue("finicial", FechaInicial);
			cmd.Parameters.AddWithValue("ffinal", FechaFinal);
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

	public DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotasCredito", con.conector);
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

	public DataTable MuestraTransferenciasVigentes(int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciasVigentes", con.conector);
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

	public DataTable MuestraGuia(int codAlmacen, int codUsu)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("VerificaProductoBonificadoCompra_1", con.conector);
			cmd.Parameters.AddWithValue("codalmacen_ex", codAlmacen);
			cmd.Parameters.AddWithValue("usu", codUsu);
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

	public DataTable CargaNotaCreditoSinAplicar(int Codcli, int VentComp, int codAlmacen, string fecha)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaCreditoSinAplicar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codcli", Codcli);
			cmd.Parameters.AddWithValue("tipo", VentComp);
			cmd.Parameters.AddWithValue("codAlma", codAlmacen);
			cmd.Parameters.AddWithValue("_fecha", fecha);
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

	public bool ActualizaNCreditoVentaSinAplicar(clsNotaIngreso nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNCreditoVentaSinAplicar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codNotaI", nota.CodNotaIngreso);
			cmd.Parameters.AddWithValue("codRef", nota.CodReferencia);
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

	public bool VerificarNCVentaAplicada(clsNotaIngreso nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificarNCVentaAplicada", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("aplicad", nota.Aplicada);
			oParam = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codref", nota.CodReferencia);
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

	public DataTable CargaNotaIngresoSD(int Codprov, int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNotaIngresoSD", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprov", Codprov);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("finicial", fecha1);
			cmd.Parameters.AddWithValue("ffinal", fecha2);
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

	public DataTable ListarCodigoNotasSalida()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarCodigoNotasSalida", con.conector);
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

	public bool ActualizaStockPA(int codalmacenorig, int codalmacenrecep, int codigoProd, int codigoNI, decimal cantidadenviada, decimal preciounit, decimal valorreal, decimal valorrealsoles, int codigouser, string serie, string numerodoc, int codserie)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaStockPA", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codAlmacenORG_ex", codalmacenorig);
			oParam = cmd.Parameters.AddWithValue("codAlmacenREC_ex", codalmacenrecep);
			oParam = cmd.Parameters.AddWithValue("codProducto_ex", codigoProd);
			oParam = cmd.Parameters.AddWithValue("codigoNI_ex", codigoNI);
			oParam = cmd.Parameters.AddWithValue("cantidadenviada_ex", cantidadenviada);
			oParam = cmd.Parameters.AddWithValue("preciounitario_ex", preciounit);
			oParam = cmd.Parameters.AddWithValue("valorreal_ex", valorreal);
			oParam = cmd.Parameters.AddWithValue("valorrealsoles_ex", valorrealsoles);
			oParam = cmd.Parameters.AddWithValue("codigouser_ex", codigouser);
			oParam = cmd.Parameters.AddWithValue("serie_ex", serie);
			oParam = cmd.Parameters.AddWithValue("numerodoc_ex", numerodoc);
			oParam = cmd.Parameters.AddWithValue("codserie_ex", codserie);
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

	public bool ActualizaStockAR(int codalmacenorig, int codalmacenrecep, int codigoProd, int codigoNI, decimal cantidadenviada, decimal preciounit, decimal valorreal, decimal valorrealsoles, int codigouser, string serie, string numerodoc, int codserie)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaStockAR", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codAlmacenORG_ex", codalmacenorig);
			oParam = cmd.Parameters.AddWithValue("codAlmacenREC_ex", codalmacenrecep);
			oParam = cmd.Parameters.AddWithValue("codProducto_ex", codigoProd);
			oParam = cmd.Parameters.AddWithValue("codigoNI_ex", codigoNI);
			oParam = cmd.Parameters.AddWithValue("cantidadenviada_ex", cantidadenviada);
			oParam = cmd.Parameters.AddWithValue("preciounitario_ex", preciounit);
			oParam = cmd.Parameters.AddWithValue("valorreal_ex", valorreal);
			oParam = cmd.Parameters.AddWithValue("valorrealsoles_ex", valorrealsoles);
			oParam = cmd.Parameters.AddWithValue("codigouser_ex", codigouser);
			oParam = cmd.Parameters.AddWithValue("serie_ex", serie);
			oParam = cmd.Parameters.AddWithValue("numerodoc_ex", numerodoc);
			oParam = cmd.Parameters.AddWithValue("codserie_ex", codserie);
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

	public clsNotaIngreso CargaNI(int codTransDirecta)
	{
		clsNotaIngreso nota = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraNIngreso", con.conector);
			cmd.Parameters.AddWithValue("cod", codTransDirecta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nota = new clsNotaIngreso();
					nota.CodNotaIngreso = dr.GetString(0);
					nota.CodReferencia = dr.GetInt32(1);
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

	public bool ValidarCompraNotaIngreso(int codigoTipoDocumento, string serieDocumento, string numeroDocumento, int codigoProveedor)
	{
		bool hayRegistros = false;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidarCompraNotaIngreso", con.conector);
			cmd.Parameters.AddWithValue("codigo_tipo_documento", codigoTipoDocumento);
			cmd.Parameters.AddWithValue("serie_documento", serieDocumento);
			cmd.Parameters.AddWithValue("numero_documento", numeroDocumento);
			cmd.Parameters.AddWithValue("codigo_proveedor", codigoProveedor);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					hayRegistros = dr.GetBoolean(0);
				}
			}
			return hayRegistros;
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

	public int obtenerCodNCsegun(string codNotaIngreso)
	{
		int codNotaCredito = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodNotaCreditoSegunCodNotaIngreso", con.conector);
			cmd.Parameters.AddWithValue("_codNotaIngreso", codNotaIngreso);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codNotaCredito = dr.GetInt32(0);
				}
			}
			return codNotaCredito;
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

	public DataTable ListaNotasIngresoxProducto(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int codprod, int tipoFecha)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaNotasxProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("criterio", Criterio);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("codprod", codprod);
			cmd.Parameters.AddWithValue("tipoFecha", tipoFecha);
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
