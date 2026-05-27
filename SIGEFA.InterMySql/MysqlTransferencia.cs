using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlTransferencia : ITransferencia
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsTransferencia transf)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalmaorig", transf.CodAlmacenOrigen);
			oParam = cmd.Parameters.AddWithValue("codtipo", transf.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codalmadest", transf.CodAlmacenDestino);
			oParam = cmd.Parameters.AddWithValue("moneda", transf.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", transf.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechaenvio", transf.FechaEnvio);
			oParam = cmd.Parameters.AddWithValue("fechaentrega", transf.FechaEntrega);
			oParam = cmd.Parameters.AddWithValue("codlista", transf.CodListaPrecio);
			oParam = cmd.Parameters.AddWithValue("descripcion", transf.DescripcionRechazo);
			string comentario = ((transf.Comentario.Length > 110) ? transf.Comentario.Substring(0, 110) : transf.Comentario);
			oParam = cmd.Parameters.AddWithValue("comentario", comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", transf.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", transf.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", transf.Igv);
			oParam = cmd.Parameters.AddWithValue("total", transf.Total);
			oParam = cmd.Parameters.AddWithValue("estado", transf.Estado);
			oParam = cmd.Parameters.AddWithValue("formapago", transf.FormaPago);
			oParam = cmd.Parameters.AddWithValue("fechapago", transf.FechaPago);
			oParam = cmd.Parameters.AddWithValue("codusu", transf.CodUser);
			oParam = cmd.Parameters.AddWithValue("codserie_ex", transf.Codserie);
			oParam = cmd.Parameters.AddWithValue("serie_ex", transf.Serie);
			oParam = cmd.Parameters.AddWithValue("numerodoc_ex", transf.Numerodocumento);
			if (transf.codReqAlm == 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codreqalm", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codreqalm", transf.codReqAlm);
			}
			if (transf.codTransAExtornar == 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codTransferenciaExtornar", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codTransferenciaExtornar", transf.codTransAExtornar);
			}
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			transf.CodTransDir = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool insert2(clsTransferencia transf)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTransferencia2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalmaorig", transf.CodAlmacenOrigen);
			oParam = cmd.Parameters.AddWithValue("codtipo", transf.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codalmadest", transf.CodAlmacenDestino);
			oParam = cmd.Parameters.AddWithValue("moneda", transf.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", transf.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechaenvio", transf.FechaEnvio);
			oParam = cmd.Parameters.AddWithValue("fechaentrega", transf.FechaEntrega);
			oParam = cmd.Parameters.AddWithValue("codlista", transf.CodListaPrecio);
			oParam = cmd.Parameters.AddWithValue("descripcion", transf.DescripcionRechazo);
			oParam = cmd.Parameters.AddWithValue("comentario", transf.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", transf.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", transf.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", transf.Igv);
			oParam = cmd.Parameters.AddWithValue("total", transf.Total);
			oParam = cmd.Parameters.AddWithValue("estado", transf.Estado);
			oParam = cmd.Parameters.AddWithValue("formapago", transf.FormaPago);
			oParam = cmd.Parameters.AddWithValue("fechapago", transf.FechaPago);
			oParam = cmd.Parameters.AddWithValue("codusu", transf.CodUser);
			oParam = cmd.Parameters.AddWithValue("codserie_ex", transf.Codserie);
			oParam = cmd.Parameters.AddWithValue("serie_ex", transf.Serie);
			oParam = cmd.Parameters.AddWithValue("numerodoc_ex", transf.Numerodocumento);
			oParam = cmd.Parameters.AddWithValue("Nombretrans", transf.Nombretrans);
			oParam = cmd.Parameters.AddWithValue("Telefonotrans", transf.Telefonotrans);
			oParam = cmd.Parameters.AddWithValue("Direcciontrans", transf.Direcciontrans);
			oParam = cmd.Parameters.AddWithValue("Numpedido", transf.Numpedido);
			oParam = cmd.Parameters.AddWithValue("docTransBF", transf.DocTransBF);
			oParam = cmd.Parameters.AddWithValue("EstadoTrnas", transf.EstadoTrnas);
			oParam = cmd.Parameters.AddWithValue("autorizadopor", transf.Autorizadopor);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			transf.CodTransDir = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool update(clsTransferencia transf)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtrans", Convert.ToInt32(transf.CodTransDir));
			cmd.Parameters.AddWithValue("nombre", transf.Nombretrans);
			cmd.Parameters.AddWithValue("codTipoDoc", transf.CodTipoDocumento);
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

	public bool delete(int codtrans)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtrans", codtrans);
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

	public clsTransferencia CargaTransferenciaCod()
	{
		clsTransferencia trans = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciaCod", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					trans = new clsTransferencia();
					trans.CodTransDir = dr.GetString(0);
				}
			}
			return trans;
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

	public clsTransferencia CargaTransferencia(int codtrans)
	{
		clsTransferencia trans = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTransferencia", con.conector);
			cmd.Parameters.AddWithValue("codtransf", codtrans);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					trans = new clsTransferencia();
					trans.CodTransDir = dr.GetString(0);
					trans.CodAlmacenOrigen = Convert.ToInt32(dr.GetDecimal(1));
					trans.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(2));
					trans.SiglaDocumento = dr.GetString(3);
					trans.DescripcionDocumento = dr.GetString(4);
					trans.CodAlmacenDestino = Convert.ToInt32(dr.GetString(5));
					trans.Moneda = Convert.ToInt32(dr.GetString(6));
					trans.TipoCambio = dr.GetDecimal(7);
					trans.FechaEnvio = dr.GetDateTime(8);
					trans.FechaEntrega = dr.GetDateTime(9);
					trans.Comentario = dr.GetString(10);
					trans.MontoBruto = dr.GetDecimal(11);
					trans.MontoDscto = dr.GetDecimal(12);
					trans.Igv = dr.GetDecimal(13);
					trans.Total = dr.GetDecimal(14);
					trans.Estado = Convert.ToInt32(dr.GetDecimal(15));
					trans.FormaPago = Convert.ToInt32(dr.GetString(16));
					trans.FechaPago = dr.GetDateTime(17);
					trans.CodUser = Convert.ToInt32(dr.GetDecimal(18));
					trans.FechaRegistro = dr.GetDateTime(19);
					trans.DescripcionRechazo = dr.GetString(20);
					trans.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(21));
					trans.Codserie = dr.GetInt32(22);
					trans.Serie = dr.GetString(23);
					trans.Numerodocumento = dr.GetString(24);
					trans.Nombretrans = (dr.IsDBNull(25) ? "" : dr.GetString(25));
					trans.Telefonotrans = (dr.IsDBNull(26) ? "" : dr.GetString(26));
					trans.Direcciontrans = (dr.IsDBNull(27) ? "" : dr.GetString(27));
					trans.Autorizadopor = (dr.IsDBNull(28) ? "" : dr.GetString(28));
					trans.Numpedido = Convert.ToInt32(dr.IsDBNull(29) ? "0" : dr.GetString(29));
					trans.EstadoTrnas = ((!dr.IsDBNull(30)) ? dr.GetInt32(30) : 0);
					trans.codReqAlm = ((!dr.IsDBNull(31)) ? dr.GetInt32(31) : 0);
				}
			}
			return trans;
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

	public clsTransferencia CargaTransferenciaCodPedido(int codpedido)
	{
		clsTransferencia trans = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTransferenciacod", con.conector);
			cmd.Parameters.AddWithValue("codpedido", codpedido);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					trans = new clsTransferencia();
					trans.CodTransDir = dr.GetString(0);
					trans.CodAlmacenOrigen = Convert.ToInt32(dr.GetDecimal(1));
					trans.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(2));
					trans.SiglaDocumento = dr.GetString(3);
					trans.DescripcionDocumento = dr.GetString(4);
					trans.CodAlmacenDestino = Convert.ToInt32(dr.GetString(5));
					trans.Moneda = Convert.ToInt32(dr.GetString(6));
					trans.TipoCambio = dr.GetDecimal(7);
					trans.FechaEnvio = dr.GetDateTime(8);
					trans.FechaEntrega = dr.GetDateTime(9);
					trans.Comentario = dr.GetString(10);
					trans.MontoBruto = dr.GetDecimal(11);
					trans.MontoDscto = dr.GetDecimal(12);
					trans.Igv = dr.GetDecimal(13);
					trans.Total = dr.GetDecimal(14);
					trans.Estado = Convert.ToInt32(dr.GetDecimal(15));
					trans.FormaPago = Convert.ToInt32(dr.GetString(16));
					trans.FechaPago = dr.GetDateTime(17);
					trans.CodUser = Convert.ToInt32(dr.GetDecimal(18));
					trans.FechaRegistro = dr.GetDateTime(19);
					trans.DescripcionRechazo = dr.GetString(20);
					trans.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(21));
					trans.Codserie = dr.GetInt32(22);
					trans.Serie = dr.GetString(23);
					trans.Numerodocumento = dr.GetString(24);
					trans.Nombretrans = dr.GetString(25);
					trans.Telefonotrans = dr.GetString(26);
					trans.Direcciontrans = dr.GetString(27);
					trans.Autorizadopor = dr.GetString(28);
					trans.Numpedido = Convert.ToInt32(dr.GetString(29));
					trans.EstadoTrnas = dr.GetInt32(30);
				}
			}
			return trans;
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

	public clsTransferencia CargaDetalleTransferencia(int codtrans)
	{
		clsTransferencia trans = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleTransferencia2", con.conector);
			cmd.Parameters.AddWithValue("codtransf", codtrans);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					trans = new clsTransferencia();
					trans.EstadoTrnas = dr.GetInt32(0);
				}
			}
			return trans;
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

	public clsTransferencia BuscaTransferencia(string codtrans, int codAlmacenOrigen)
	{
		clsTransferencia trans = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaTransferencia", con.conector);
			cmd.Parameters.AddWithValue("codtrans", Convert.ToInt32(codtrans));
			cmd.Parameters.AddWithValue("codAlmacenOrigen", codAlmacenOrigen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					trans = new clsTransferencia();
					trans.CodTransDir = dr.GetString(0);
					trans.CodAlmacenOrigen = Convert.ToInt32(dr.GetDecimal(1));
					trans.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(2));
					trans.SiglaDocumento = dr.GetString(3);
					trans.DescripcionDocumento = dr.GetString(4);
					trans.CodAlmacenDestino = Convert.ToInt32(dr.GetString(5));
					trans.Moneda = Convert.ToInt32(dr.GetString(6));
					trans.TipoCambio = dr.GetDecimal(7);
					trans.FechaEnvio = dr.GetDateTime(8);
					trans.FechaEntrega = dr.GetDateTime(9);
					trans.Comentario = dr.GetString(10);
					trans.MontoBruto = dr.GetDecimal(11);
					trans.MontoDscto = dr.GetDecimal(12);
					trans.Igv = dr.GetDecimal(13);
					trans.Total = dr.GetDecimal(14);
					trans.Estado = Convert.ToInt32(dr.GetDecimal(15));
					trans.FormaPago = Convert.ToInt32(dr.GetString(16));
					trans.FechaPago = dr.GetDateTime(17);
					trans.CodUser = Convert.ToInt32(dr.GetDecimal(18));
					trans.FechaRegistro = dr.GetDateTime(19);
					trans.DescripcionRechazo = dr.GetString(20);
				}
			}
			return trans;
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

	public DataTable ListaTranferenciasPendientes(int codAlmacen, int codPedido)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciasPedidos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codpedido", codPedido);
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

	public DataTable ListaTranferenciasEntrega(DateTime fechaE, int codigotra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTranferenciasEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fechaent", fechaE);
			cmd.Parameters.AddWithValue("codtransf", codigotra);
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

	public DataTable ListaTranferencias(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferencias", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("usu", user);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
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

	public DataTable ListaTranferenciasDesp(int codtrans)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTranferenciasDesp", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtransf", codtrans);
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

	public DataTable ListaTranferencias2(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferencias2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("usu", user);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
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

	public DataTable ListaTransferenciasEnviados(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciasEnviados", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("usu", user);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
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

	public DataTable listatodastranferencias(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteTransferenciaDirecta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("usu", user);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
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

	public bool insertdetalle(clsDetalleTransferencia detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codtrans", detalle.CodTransDir);
			oParam = cmd.Parameters.AddWithValue("codalmaorig", detalle.CodAlmacenOrigen);
			oParam = cmd.Parameters.AddWithValue("codalmadest", detalle.CodAlmacenDestino);
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
			oParam = cmd.Parameters.AddWithValue("cantidadp", detalle.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("codprov", detalle.CodProv);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("precioigv", detalle.Precioigv);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("promedio", detalle.Valorpromedio);
			oParam = cmd.Parameters.AddWithValue("_coddetallereqalm", detalle.codDetalleReqAlm);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleTransfer = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalle2(clsDetalleTransferencia detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleTransferencia2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codtrans", detalle.CodTransDir);
			oParam = cmd.Parameters.AddWithValue("codalmaorig", detalle.CodAlmacenOrigen);
			oParam = cmd.Parameters.AddWithValue("codalmadest", detalle.CodAlmacenDestino);
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
			oParam = cmd.Parameters.AddWithValue("cantidadp", detalle.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("codprov", detalle.CodProv);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("precioigv", detalle.Precioigv);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("promedio", detalle.Valorpromedio);
			oParam = cmd.Parameters.AddWithValue("EstadoTrnas", detalle.EstadoTrnas);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleTransfer = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool updatedetalle(clsDetalleTransferencia detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codTrans", detalle.CodTransDir);
			cmd.Parameters.AddWithValue("codProd", detalle.CodProducto);
			cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", detalle.Igv);
			cmd.Parameters.AddWithValue("importe", detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			cmd.Parameters.AddWithValue("precioigv", detalle.Precioigv);
			cmd.Parameters.AddWithValue("promedio", detalle.Valorpromedio);
			cmd.Parameters.AddWithValue("cantidadp", detalle.CantidadPendiente);
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

	public bool updatedetalle2(clsDetalleTransferencia detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalletransferencia2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle.CodTransDir);
			cmd.Parameters.AddWithValue("codprud", detalle.CodProducto);
			cmd.Parameters.AddWithValue("CPendiente", detalle.CantidadPendiente);
			cmd.Parameters.AddWithValue("CEntrega", detalle.CantidadEntrega);
			cmd.Parameters.AddWithValue("Despachado", detalle.Despachado);
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

	public bool deletedetalle(int coddeta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public DataTable CargaDetallePedido(int codTransDir)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleTransferenciaPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codTransDir", codTransDir);
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

	public DataTable CargaDetalle(int codTransDir)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codTransDir", codTransDir);
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

	public DataTable CargaDetalleTrans(clsTransferencia tra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraReporteDetalleTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codTransDir", tra.codtrans);
			cmd.Parameters.AddWithValue("codalma", tra.CodAlmacenOrigen);
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

	public bool rechazad2(int codTransDirecta, string anulado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("RechazarTransferencia2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtrans", codTransDirecta);
			cmd.Parameters.AddWithValue("descripcion", anulado);
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

	public bool rechazado(int codTransDirecta, string desc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("RechazarTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtrans", codTransDirecta);
			cmd.Parameters.AddWithValue("descripcion", desc);
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

	public int unidadPA(int codprod, int codalma)
	{
		int unidad = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CogeUnidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", codprod);
			cmd.Parameters.AddWithValue("almaorg", codalma);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					unidad = Convert.ToInt32(dr.GetDecimal(0));
				}
			}
			return unidad;
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

	public double Factor(int codprod, int unidad, int unidadequi)
	{
		double fac = 0.0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CogeFactor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", codprod);
			cmd.Parameters.AddWithValue("unidad", unidad);
			cmd.Parameters.AddWithValue("unidadEqui", unidadequi);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					fac = dr.GetDouble(0);
				}
			}
			return fac;
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

	public bool devuelveproductos(clsDetalleTransferencia detalle)
	{
		try
		{
			int unidad = unidadPA(detalle.CodProducto, detalle.CodAlmacenOrigen);
			double factor = Factor(detalle.CodProducto, detalle.UnidadIngresada, unidad);
			factor *= detalle.Cantidad;
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPA", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", detalle.CodProducto);
			cmd.Parameters.AddWithValue("almaorg", detalle.CodAlmacenOrigen);
			cmd.Parameters.AddWithValue("stock", factor);
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

	public bool Aprobar(int codTransDirecta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AprobarTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtrans", codTransDirecta);
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

	public bool TransFactura(int codPedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("FacturaTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpedido", codPedido);
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

	public bool Entregar(int codTransDirecta)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EntregarTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtrans", codTransDirecta);
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

	public DataTable CargaDetalleGuiaT(string CodigoTransferencia)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleTranferenciaGuia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtranf", CodigoTransferencia);
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

	public DataTable ListaTranferenciasxProducto(int caso, int user, int codAlmacen, DateTime desde, DateTime hasta, int codprod)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciaxProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("usu", user);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
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

	public bool atendido(int codTransDirecta, int codUsuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AgregarUsuarioAtendioTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codtrans", codTransDirecta);
			cmd.Parameters.AddWithValue("_coduser", codUsuario);
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
