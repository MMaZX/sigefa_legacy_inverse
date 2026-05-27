using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlGuiaRemision : IGuiaRemision
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsGuiaRemision GuiaRemision)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaGuiaRemision", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalma", GuiaRemision.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("coddoc", GuiaRemision.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codserie", GuiaRemision.CodSerie);
			oParam = cmd.Parameters.AddWithValue("numdoc", GuiaRemision.NumDoc);
			oParam = cmd.Parameters.AddWithValue("codmoti", GuiaRemision.CodMotivo);
			if (GuiaRemision.CodPedido != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codped", GuiaRemision.CodPedido);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codped", null);
			}
			if (GuiaRemision.CodFactura != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codfat", GuiaRemision.CodFactura);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codfat", null);
			}
			if (GuiaRemision.CodTransferencia != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codtra", GuiaRemision.CodTransferencia);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codtra", null);
			}
			oParam = cmd.Parameters.AddWithValue("fechaemision", GuiaRemision.FechaEmision);
			oParam = cmd.Parameters.AddWithValue("fecharegistro1", GuiaRemision.fecharegistro1);
			oParam = cmd.Parameters.AddWithValue("fechaingresoalma", GuiaRemision.fechaingresoalmacen);
			oParam = cmd.Parameters.AddWithValue("fechatraslado", GuiaRemision.FechaTraslado);
			if (GuiaRemision.CodCliente != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codcli", GuiaRemision.CodCliente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codcli", null);
			}
			oParam = cmd.Parameters.AddWithValue("codvehi", GuiaRemision.CodVehiculoTransporte);
			oParam = cmd.Parameters.AddWithValue("codcond", GuiaRemision.CodConductor);
			oParam = cmd.Parameters.AddWithValue("codemp", GuiaRemision.CodEmpresaTransporte);
			oParam = cmd.Parameters.AddWithValue("facturado", GuiaRemision.Facturado);
			oParam = cmd.Parameters.AddWithValue("comentario", GuiaRemision.Comentario);
			oParam = cmd.Parameters.AddWithValue("estado", GuiaRemision.Estado);
			oParam = cmd.Parameters.AddWithValue("codusu", GuiaRemision.CodUser);
			oParam = cmd.Parameters.AddWithValue("codreq", GuiaRemision.CodReq);
			if (GuiaRemision.CodProveedor == 0)
			{
				oParam = cmd.Parameters.AddWithValue("codprov", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codprov", GuiaRemision.CodProveedor);
			}
			oParam = cmd.Parameters.AddWithValue("numrelaOC", GuiaRemision.numeroOc);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			GuiaRemision.CodGuiaRemision = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool update(clsGuiaRemision GuiaRemision)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaGuiaRemision", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", Convert.ToInt32(GuiaRemision.CodGuiaRemision));
			cmd.Parameters.AddWithValue("codalma", GuiaRemision.CodAlmacen);
			cmd.Parameters.AddWithValue("coddoc", GuiaRemision.CodTipoDocumento);
			cmd.Parameters.AddWithValue("codserie", GuiaRemision.CodSerie);
			cmd.Parameters.AddWithValue("numdoc", GuiaRemision.NumDoc);
			cmd.Parameters.AddWithValue("codmoti", GuiaRemision.CodMotivo);
			if (GuiaRemision.CodPedido != 0)
			{
				cmd.Parameters.AddWithValue("codped", GuiaRemision.CodPedido);
			}
			else
			{
				cmd.Parameters.AddWithValue("codped", null);
			}
			if (GuiaRemision.CodFactura != 0)
			{
				cmd.Parameters.AddWithValue("codfat", GuiaRemision.CodFactura);
			}
			else
			{
				cmd.Parameters.AddWithValue("codfat", null);
			}
			cmd.Parameters.AddWithValue("fechaemision", GuiaRemision.FechaEmision);
			cmd.Parameters.AddWithValue("fechatraslado", GuiaRemision.FechaTraslado);
			if (GuiaRemision.CodCliente != 0)
			{
				cmd.Parameters.AddWithValue("codcli", GuiaRemision.CodCliente);
			}
			else
			{
				cmd.Parameters.AddWithValue("codcli", null);
			}
			cmd.Parameters.AddWithValue("codvehi", GuiaRemision.CodVehiculoTransporte);
			cmd.Parameters.AddWithValue("codcond", GuiaRemision.CodConductor);
			cmd.Parameters.AddWithValue("codemp", GuiaRemision.CodEmpresaTransporte);
			cmd.Parameters.AddWithValue("facturado", GuiaRemision.Facturado);
			cmd.Parameters.AddWithValue("comentario", GuiaRemision.Comentario);
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

	public bool delete(int CodigoGuiaRemision)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarGuiaRemision", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", CodigoGuiaRemision);
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

	public bool insertdetalle(clsDetalleGuiaRemision detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleGuiaRemision", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codguia", detalle.CodGuiaRemision);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("precio", detalle.precio);
			oParam = cmd.Parameters.AddWithValue("cantidadpendiente", detalle.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("peso", detalle.Peso);
			oParam = cmd.Parameters.AddWithValue("pendiente", detalle.Pendiente);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("productosolicitado", detalle.productosolicitado);
			oParam = cmd.Parameters.AddWithValue("etiqueta", detalle.etiqueta);
			oParam = cmd.Parameters.AddWithValue("estadoOrden", detalle.estadoOrden);
			oParam = cmd.Parameters.AddWithValue("_codDetalleOrden", detalle.codDtalleOrden);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleGuiaRemision = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool updatedetalle(clsDetalleGuiaRemision detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleGuiaRemision", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle.CodDetalleGuiaRemision);
			cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			cmd.Parameters.AddWithValue("cantidadpendiente", detalle.CantidadPendiente);
			cmd.Parameters.AddWithValue("peso", detalle.Peso);
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
			cmd = new MySqlCommand("EliminarDetalleGuiaRemision", con.conector);
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

	public bool insertrelacionguia(int codguia, int codventa, int codalmacen, int codusuario, int CodTrans)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaRelacionGuia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", codguia);
			cmd.Parameters.AddWithValue("codventa", codventa);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("codusu", codusuario);
			cmd.Parameters.AddWithValue("codtrans", CodTrans);
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

	public clsGuiaRemision CargaGuiaRemision(int CodGuiaRemision)
	{
		clsGuiaRemision GuiaRemision = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraGuiaRemision", con.conector);
			cmd.Parameters.AddWithValue("codguia", CodGuiaRemision);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					GuiaRemision = new clsGuiaRemision();
					GuiaRemision.CodGuiaRemision = dr.GetString(0);
					GuiaRemision.CodAlmacen = Convert.ToInt32(dr.GetDecimal(1));
					GuiaRemision.CodMotivo = Convert.ToInt32(dr.GetDecimal(2));
					GuiaRemision.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(3));
					GuiaRemision.SiglaDocumento = dr.GetString(4);
					GuiaRemision.CodSerie = Convert.ToInt32(dr.GetDecimal(5));
					GuiaRemision.Serie = dr.GetString(6);
					GuiaRemision.NumDoc = dr.GetString(7);
					GuiaRemision.CodCliente = Convert.ToInt32(dr.GetDecimal(8));
					GuiaRemision.CodigoPersonalizado = dr.GetString(9);
					GuiaRemision.DNI = dr.GetString(10);
					GuiaRemision.RUCCliente = dr.GetString(11);
					GuiaRemision.RazonSocialCliente = dr.GetString(12);
					GuiaRemision.Nombre = dr.GetString(13);
					GuiaRemision.Direccion = dr.GetString(14);
					GuiaRemision.CodVehiculoTransporte = Convert.ToInt32(dr.GetString(15));
					GuiaRemision.CodMarca = Convert.ToInt32(dr.GetString(16));
					GuiaRemision.CodModelo = Convert.ToInt32(dr.GetString(17));
					GuiaRemision.Placa = dr.GetString(18);
					GuiaRemision.Marca = dr.GetString(19);
					GuiaRemision.Modelo = dr.GetString(20);
					GuiaRemision.ConstanciaInscripcion = dr.GetString(21);
					GuiaRemision.CodConductor = Convert.ToInt32(dr.GetString(22));
					GuiaRemision.NombreConductor = dr.GetString(23);
					GuiaRemision.Licencia = dr.GetString(24);
					GuiaRemision.CodEmpresaTransporte = Convert.ToInt32(dr.GetString(25));
					GuiaRemision.RUCEmpresaTransporte = dr.GetString(26);
					GuiaRemision.RazonSocialTransporte = dr.GetString(27);
					GuiaRemision.DireccionTransporte = dr.GetString(28);
					GuiaRemision.FechaEmision = dr.GetDateTime(29);
					GuiaRemision.FechaTraslado = dr.GetDateTime(30);
					GuiaRemision.Comentario = dr.GetString(31);
					GuiaRemision.Estado = Convert.ToInt32(dr.GetDecimal(32));
					GuiaRemision.Facturado = Convert.ToInt32(dr.GetDecimal(33));
					GuiaRemision.CodUser = Convert.ToInt32(dr.GetDecimal(34));
					GuiaRemision.FechaRegistro = dr.GetDateTime(35);
					GuiaRemision.CodPedido = Convert.ToInt32(dr.GetDecimal(36));
					GuiaRemision.CodFactura = Convert.ToInt32(dr.GetDecimal(37));
					GuiaRemision.CodAlmacenDestino = dr.GetInt32(38);
					GuiaRemision.NomAlmacenDestino = dr.GetString(39);
					GuiaRemision.UbicacionAlmacenDest = dr.GetString(40);
					GuiaRemision.CodProveedor = dr.GetInt32(41);
					GuiaRemision.numeroOc = Convert.ToString(dr["numorden"]);
				}
			}
			return GuiaRemision;
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

	public clsGuiaRemision CargaGuiaTransferencia(int cod)
	{
		clsGuiaRemision GuiaRemision = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraGuiaTransferencia", con.conector);
			cmd.Parameters.AddWithValue("codnota", cod);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					GuiaRemision = new clsGuiaRemision();
					GuiaRemision.FechaTraslado = dr.GetDateTime(0);
					GuiaRemision.FechaEmision = dr.GetDateTime(1);
				}
			}
			return GuiaRemision;
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

	public clsGuiaRemision CargaGuiaVenta(int CodVenta)
	{
		clsGuiaRemision GuiaRemision = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraGuiaVenta", con.conector);
			cmd.Parameters.AddWithValue("codventa", CodVenta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					GuiaRemision = new clsGuiaRemision();
					GuiaRemision.CodGuiaRemision = dr.GetString(0);
					GuiaRemision.CodAlmacen = Convert.ToInt32(dr.GetDecimal(1));
					GuiaRemision.CodMotivo = Convert.ToInt32(dr.GetDecimal(2));
					GuiaRemision.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(3));
					GuiaRemision.SiglaDocumento = dr.GetString(4);
					GuiaRemision.CodSerie = Convert.ToInt32(dr.GetDecimal(5));
					GuiaRemision.Serie = dr.GetString(6);
					GuiaRemision.NumDoc = dr.GetString(7);
					GuiaRemision.CodCliente = Convert.ToInt32(dr.GetDecimal(8));
					GuiaRemision.CodigoPersonalizado = dr.GetString(9);
					GuiaRemision.DNI = dr.GetString(10);
					GuiaRemision.RUCCliente = dr.GetString(11);
					GuiaRemision.RazonSocialCliente = dr.GetString(12);
					GuiaRemision.Nombre = dr.GetString(13);
					GuiaRemision.Direccion = dr.GetString(14);
					GuiaRemision.CodVehiculoTransporte = Convert.ToInt32(dr.GetString(15));
					GuiaRemision.CodMarca = Convert.ToInt32(dr.GetString(16));
					GuiaRemision.CodModelo = Convert.ToInt32(dr.GetString(17));
					GuiaRemision.Placa = dr.GetString(18);
					GuiaRemision.Marca = dr.GetString(19);
					GuiaRemision.Modelo = dr.GetString(20);
					GuiaRemision.ConstanciaInscripcion = dr.GetString(21);
					GuiaRemision.CodConductor = Convert.ToInt32(dr.GetString(22));
					GuiaRemision.NombreConductor = dr.GetString(23);
					GuiaRemision.Licencia = dr.GetString(24);
					GuiaRemision.CodEmpresaTransporte = Convert.ToInt32(dr.GetString(25));
					GuiaRemision.RUCEmpresaTransporte = dr.GetString(26);
					GuiaRemision.RazonSocialTransporte = dr.GetString(27);
					GuiaRemision.DireccionTransporte = dr.GetString(28);
					GuiaRemision.FechaEmision = dr.GetDateTime(29);
					GuiaRemision.FechaTraslado = dr.GetDateTime(30);
					GuiaRemision.Comentario = dr.GetString(31);
					GuiaRemision.Estado = Convert.ToInt32(dr.GetDecimal(32));
					GuiaRemision.Facturado = Convert.ToInt32(dr.GetDecimal(33));
					GuiaRemision.CodUser = Convert.ToInt32(dr.GetDecimal(34));
					GuiaRemision.FechaRegistro = dr.GetDateTime(35);
					GuiaRemision.CodPedido = Convert.ToInt32(dr.GetDecimal(36));
					GuiaRemision.CodFactura = Convert.ToInt32(dr.GetDecimal(37));
				}
			}
			return GuiaRemision;
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

	public clsGuiaRemision BuscaGuiaRemision(string CodGuiaRemision, int CodAlmacen)
	{
		clsGuiaRemision GuiaRemision = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaGuiaRemision", con.conector);
			cmd.Parameters.AddWithValue("codguia", Convert.ToInt32(CodGuiaRemision));
			cmd.Parameters.AddWithValue("codalm", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					GuiaRemision = new clsGuiaRemision();
					GuiaRemision.CodGuiaRemision = dr.GetString(0);
					GuiaRemision.CodAlmacen = Convert.ToInt32(dr.GetDecimal(1));
					GuiaRemision.CodMotivo = Convert.ToInt32(dr.GetDecimal(2));
					GuiaRemision.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(3));
					GuiaRemision.SiglaDocumento = dr.GetString(4);
					GuiaRemision.CodSerie = Convert.ToInt32(dr.GetDecimal(5));
					GuiaRemision.Serie = dr.GetString(6);
					GuiaRemision.NumDoc = dr.GetString(7);
					GuiaRemision.CodCliente = Convert.ToInt32(dr.GetString(8));
					GuiaRemision.CodigoPersonalizado = dr.GetString(9);
					GuiaRemision.DNI = dr.GetString(10);
					GuiaRemision.RUCCliente = dr.GetString(11);
					GuiaRemision.RazonSocialCliente = dr.GetString(12);
					GuiaRemision.Nombre = dr.GetString(13);
					GuiaRemision.Direccion = dr.GetString(14);
					GuiaRemision.CodVehiculoTransporte = Convert.ToInt32(dr.GetString(15));
					GuiaRemision.CodMarca = Convert.ToInt32(dr.GetString(16));
					GuiaRemision.CodModelo = Convert.ToInt32(dr.GetString(17));
					GuiaRemision.Placa = dr.GetString(18);
					GuiaRemision.Marca = dr.GetString(19);
					GuiaRemision.Modelo = dr.GetString(20);
					GuiaRemision.ConstanciaInscripcion = dr.GetString(21);
					GuiaRemision.CodConductor = Convert.ToInt32(dr.GetString(22));
					GuiaRemision.NombreConductor = dr.GetString(23);
					GuiaRemision.Licencia = dr.GetString(24);
					GuiaRemision.CodEmpresaTransporte = Convert.ToInt32(dr.GetString(25));
					GuiaRemision.RazonSocialTransporte = dr.GetString(26);
					GuiaRemision.DireccionTransporte = dr.GetString(27);
					GuiaRemision.FechaEmision = dr.GetDateTime(28);
					GuiaRemision.FechaTraslado = dr.GetDateTime(29);
					GuiaRemision.Comentario = dr.GetString(30);
					GuiaRemision.Estado = Convert.ToInt32(dr.GetDecimal(31));
					GuiaRemision.Facturado = Convert.ToInt32(dr.GetDecimal(32));
					GuiaRemision.CodUser = Convert.ToInt32(dr.GetDecimal(33));
					GuiaRemision.FechaRegistro = dr.GetDateTime(34);
				}
			}
			return GuiaRemision;
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

	public List<clsDetalleGuiaRemision> listaDetalleGuiaRemision(string CodGuiaRemision)
	{
		List<clsDetalleGuiaRemision> detalleGuiaRemision = null;
		clsGuiaRemision guia = null;
		clsDetalleGuiaRemision detguia = null;
		con.conectarBD();
		cmd = new MySqlCommand("MuestraDetalleGuiaRemision", con.conector);
		cmd.Parameters.AddWithValue("codguia", Convert.ToInt32(CodGuiaRemision));
		cmd.CommandType = CommandType.StoredProcedure;
		dr = cmd.ExecuteReader();
		if (dr.HasRows)
		{
			detalleGuiaRemision = new List<clsDetalleGuiaRemision>();
			while (dr.Read())
			{
				detguia = new clsDetalleGuiaRemision();
				detguia.CodDetalleGuiaRemision = dr.GetInt32(0);
				detguia.Cantidad = Convert.ToDouble(dr.GetDecimal(7));
				detguia.guia = new clsGuiaRemision();
				detguia.guia.numeroOc = dr.GetString(12);
				detalleGuiaRemision.Add(detguia);
			}
		}
		return detalleGuiaRemision;
	}

	public List<clsDetalleGuiaRemision> listaDetalleGuiaRemisionventa(string CodGuiaRemision)
	{
		List<clsDetalleGuiaRemision> detalleGuiaRemision = null;
		clsGuiaRemision guia = null;
		clsDetalleGuiaRemision detguia = null;
		con.conectarBD();
		cmd = new MySqlCommand("MuestraDetalleGuiaRemisionVenta", con.conector);
		cmd.Parameters.AddWithValue("codguia", Convert.ToInt32(CodGuiaRemision));
		cmd.CommandType = CommandType.StoredProcedure;
		dr = cmd.ExecuteReader();
		if (dr.HasRows)
		{
			detalleGuiaRemision = new List<clsDetalleGuiaRemision>();
			while (dr.Read())
			{
				detguia = new clsDetalleGuiaRemision();
				detguia.CodDetalleGuiaRemision = dr.GetInt32(0);
				detguia.Cantidad = Convert.ToDouble(dr.GetDecimal(7));
				detguia.guia = new clsGuiaRemision();
				detguia.guia.numeroOc = dr.GetString(12);
				detalleGuiaRemision.Add(detguia);
			}
		}
		return detalleGuiaRemision;
	}

	public DataTable CargaDetalle(int CodGuiaRemision)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleGuiaRemision", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", CodGuiaRemision);
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

	public DataTable CargaDetalleGuiaCompleta(int CodGuiaRemision)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleGuiaRemision", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", CodGuiaRemision);
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

	public DataTable CargaDetalleGuiaVenta(int CodGuiaRemision)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleGuiaRemisionVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", CodGuiaRemision);
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

	public DataTable ListaGuiaRemisiones(int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaGuiaRemisiones", con.conector);
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

	public DataTable MuestraGuias(DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraGuias", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public DataTable MuestraGuiasBusqueda(DateTime fecha1, DateTime fecha2, string numdocumento)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraGuias", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("_numdocumento", numdocumento);
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

	public DataTable CargaFacturasGuia(int codGuia, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaFacturasGuia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", codGuia);
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
}
