using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;

namespace SIGEFA.InterMySql;

internal class MysqlGuiaRemisionCompra
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	internal string getEtiquetaSegunCodigo(int codEtiqueta)
	{
		string etiqueta = "";
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetEtiquetaByCod", con.conector);
			cmd.Parameters.AddWithValue("codetiqueta", codEtiqueta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					etiqueta = dr.GetString(0);
				}
			}
			return etiqueta;
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

	internal int getCodigoPrimeraGRCGenerada(int codOC)
	{
		int codigogrc = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPrimeraGRCGenerada", con.conector);
			cmd.Parameters.AddWithValue("_codigoOrdenCompra", codOC);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigogrc = dr.GetInt32(0);
				}
			}
			return codigogrc;
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

	internal DataTable ListadoDeGuiaDeRemisionDeCompraSegunOrdenCompra(int codOrdenCompra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoDeGuiaDeRemisionDeCompraSegunOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codOC", codOrdenCompra);
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

	internal int getCodigoEtiquetaSegun(string cadEtiqueta)
	{
		int etiqueta = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetEtiquetaByCadena", con.conector);
			cmd.Parameters.AddWithValue("codetiqueta", cadEtiqueta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					etiqueta = dr.GetInt32(0);
				}
			}
			return etiqueta;
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

	internal DataTable ListadoDocumentosRelacionados(int codGRC)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDocumentosRelacionadosGuiaRemisionCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codGRC", codGRC);
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

	public bool insert(clsGuiaRemision GuiaRemision)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaGuiaRemisionCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_numDocumentoGRC", GuiaRemision.NumDoc);
			oParam = cmd.Parameters.AddWithValue("_codOrdenCompra", GuiaRemision.ICodOrdenCompra);
			oParam = cmd.Parameters.AddWithValue("_numRelacionadoOC", GuiaRemision.numeroOc);
			oParam = cmd.Parameters.AddWithValue("_codAlmacen", GuiaRemision.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("_codMotivo", GuiaRemision.CodMotivo);
			oParam = cmd.Parameters.AddWithValue("_fechaEmision", GuiaRemision.FechaEmision);
			oParam = cmd.Parameters.AddWithValue("_fechaTraslado", GuiaRemision.FechaTraslado);
			oParam = cmd.Parameters.AddWithValue("_fechaIngresoAlmacen", GuiaRemision.fechaingresoalmacen);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", GuiaRemision.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_coduser", GuiaRemision.CodUser);
			oParam = cmd.Parameters.AddWithValue("_codEmpresaTransporte", GuiaRemision.CodEmpresaTransporte);
			oParam = cmd.Parameters.AddWithValue("_comentario", GuiaRemision.Comentario);
			oParam = cmd.Parameters.AddWithValue("_estado", GuiaRemision.Estado);
			if (GuiaRemision.CodProveedor == 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codProveedor", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codProveedor", GuiaRemision.CodProveedor);
			}
			oParam = cmd.Parameters.AddWithValue("_fechaModificacion", GuiaRemision.FechaModificacion);
			oParam = cmd.Parameters.AddWithValue("_codUsuarioModificacion", GuiaRemision.CodUserModificacion);
			oParam = cmd.Parameters.AddWithValue("_estadoGeneracion", GuiaRemision.EstadoGeneracion);
			oParam = cmd.Parameters.AddWithValue("_opcionFlete", GuiaRemision.OpcionFlete);
			oParam = cmd.Parameters.AddWithValue("_fleteconigv", GuiaRemision.FleteConIgv);
			oParam = cmd.Parameters.AddWithValue("_fletesinigv", GuiaRemision.FleteSinIgv);
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

	internal DataTable cargarNotasCreditoCompraGeneradas(int codigoGuiaRemision)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaNotasCreditosCompraGeneradasGuiasDeRemisionCompra", con.conector);
			cmd.Parameters.AddWithValue("codgrc", codigoGuiaRemision);
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

	internal bool deletedetalledegrc(int codigoGuiaRemision)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleDeGuiaRemisionCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codgrc", codigoGuiaRemision);
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

	internal bool anulacion(int cod_grc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarGuiaRemisionCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codguia", cod_grc);
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

	internal string getInfoDocumentoRelacionadoGRC(int codGuiaRemision, int tipoDoc)
	{
		string info = "";
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("obtenerInfoDocRelacGRC", con.conector);
			cmd.Parameters.AddWithValue("codegr", codGuiaRemision);
			cmd.Parameters.AddWithValue("tipoDR", tipoDoc);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					info = dr.GetString(0);
				}
			}
			return info;
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

	internal DataTable cargaEstados()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaEstadosGuiaRemisionCompra", con.conector);
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

	public bool update(clsGuiaRemision GuiaRemision)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaGuiaRemisionCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", Convert.ToInt32(GuiaRemision.CodGuiaRemision));
			cmd.Parameters.AddWithValue("_numDocumentoGRC", GuiaRemision.NumDoc);
			cmd.Parameters.AddWithValue("_codOrdenCompra", GuiaRemision.ICodOrdenCompra);
			cmd.Parameters.AddWithValue("_numRelacionadoOC", GuiaRemision.numeroOc);
			cmd.Parameters.AddWithValue("_codAlmacen", GuiaRemision.CodAlmacen);
			cmd.Parameters.AddWithValue("_codMotivo", GuiaRemision.CodMotivo);
			cmd.Parameters.AddWithValue("_fechaEmision", GuiaRemision.FechaEmision);
			cmd.Parameters.AddWithValue("_fechaTraslado", GuiaRemision.FechaTraslado);
			cmd.Parameters.AddWithValue("_fechaIngresoAlmacen", GuiaRemision.fechaingresoalmacen);
			cmd.Parameters.AddWithValue("_fechaRegistro", GuiaRemision.FechaRegistro);
			cmd.Parameters.AddWithValue("_coduser", GuiaRemision.CodUser);
			cmd.Parameters.AddWithValue("_codEmpresaTransporte", GuiaRemision.CodEmpresaTransporte);
			cmd.Parameters.AddWithValue("_comentario", GuiaRemision.Comentario);
			cmd.Parameters.AddWithValue("_estado", GuiaRemision.Estado);
			if (GuiaRemision.CodProveedor == 0)
			{
				cmd.Parameters.AddWithValue("_codProveedor", null);
			}
			else
			{
				cmd.Parameters.AddWithValue("_codProveedor", GuiaRemision.CodProveedor);
			}
			cmd.Parameters.AddWithValue("_fechaModificacion", GuiaRemision.FechaModificacion);
			cmd.Parameters.AddWithValue("_codUsuarioModificacion", GuiaRemision.CodUserModificacion);
			cmd.Parameters.AddWithValue("_estadoGeneracion", GuiaRemision.EstadoGeneracion);
			cmd.Parameters.AddWithValue("_opcionFlete", GuiaRemision.OpcionFlete);
			cmd.Parameters.AddWithValue("_fleteconigv", GuiaRemision.FleteConIgv);
			cmd.Parameters.AddWithValue("_fletesinigv", GuiaRemision.FleteSinIgv);
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

	public bool insertdetalle(clsDetalleGuiaRemisionCompra detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleGuiaRemisionCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codProducto", detalle.ICodProducto);
			oParam = cmd.Parameters.AddWithValue("_codDetalleOrdenCompra", detalle.ICodDetalleOrdenCOmpra);
			oParam = cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", detalle.ICodGuiaRemisionCOmpra);
			oParam = cmd.Parameters.AddWithValue("_moneda", detalle.IcodMoneda);
			oParam = cmd.Parameters.AddWithValue("_unidadingresada", detalle.IUnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.DCantidad);
			oParam = cmd.Parameters.AddWithValue("_cantidadrespaldo", detalle.DCantidadRespaldo);
			oParam = cmd.Parameters.AddWithValue("_fechaingreso", detalle.FFechaIngreso);
			oParam = cmd.Parameters.AddWithValue("_estado", 1);
			oParam = cmd.Parameters.AddWithValue("_codUser", detalle.ICOdUser);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", detalle.FFechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_anulado", detalle.IAnulado);
			oParam = cmd.Parameters.AddWithValue("_codEstado", detalle.IEstado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.ICodDetalleGuiaRemisionCompra = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	internal DataTable estadosAlInicioGeneracion(int iCodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaEstadoAlInicioGeneracion", con.conector);
			cmd.Parameters.AddWithValue("codAlm", iCodAlmacen);
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

	internal DataTable generaFacturaFleteDeGRC(int codGuiaRemisionCompra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("generarDetalleNotaIngresoFacturaFleteDeGRC", con.conector);
			cmd.Parameters.AddWithValue("_codGRC", codGuiaRemisionCompra);
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

	internal bool insertarDocumentoRelacionado(clsGuiaRemisionCompraDocumentoRelacionado nuevo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaGuiaRemisionCompraDocumentoRelacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", nuevo.CodGuiaRemisionCompra);
			oParam = cmd.Parameters.AddWithValue("_codDocumentoRelacionado", nuevo.CodDocumentoRelacionado);
			oParam = cmd.Parameters.AddWithValue("_codTipoDocumento", nuevo.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("_docCompraVenta", nuevo.TipoGRCDR);
			oParam = cmd.Parameters.AddWithValue("_anulado", nuevo.Anulado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			nuevo.CodGuiaRemisionCompraDocumentoRelacionado = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	internal bool updateDocumentoRelacionado(clsGuiaRemisionCompraDocumentoRelacionado nuevo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaGuiaRemisionCompraDocumentoRelacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codGuiaRemisionCompraDocumentoRelacionado", nuevo.CodGuiaRemisionCompraDocumentoRelacionado);
			oParam = cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", nuevo.CodGuiaRemisionCompra);
			oParam = cmd.Parameters.AddWithValue("_codDocumentoRelacionado", nuevo.CodDocumentoRelacionado);
			oParam = cmd.Parameters.AddWithValue("_codTipoDocumento", nuevo.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("_docCompraVenta", nuevo.TipoGRCDR);
			oParam = cmd.Parameters.AddWithValue("_anulado", nuevo.Anulado);
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

	internal clsGuiaRemisionCompraDocumentoRelacionado cargaDocumentoRelacionado(int codigo)
	{
		clsGuiaRemisionCompraDocumentoRelacionado nuevo = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaGuiaRemisionCompraDocumentoRelacion", con.conector);
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompraDocumentoRelacionado", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nuevo.CodGuiaRemisionCompra = dr.GetInt32(0);
					nuevo.CodDocumentoRelacionado = dr.GetInt32(1);
					nuevo.CodTipoDocumento = dr.GetInt32(2);
					nuevo.Anulado = dr.GetInt32(3);
					nuevo.TipoGRCDR = dr.GetInt32(4);
				}
			}
			return nuevo;
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

	internal clsGuiaRemisionCompraDocumentoRelacionado buscaDocumentoRelacionado(int codDoc, int tipoDoc)
	{
		clsGuiaRemisionCompraDocumentoRelacionado nuevo = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaGuiaRemisionCompraDocumentoRelacion1", con.conector);
			cmd.Parameters.AddWithValue("_codDocumentoRelacionado", codDoc);
			cmd.Parameters.AddWithValue("_tipoDoc", tipoDoc);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nuevo.CodGuiaRemisionCompra = dr.GetInt32(0);
					nuevo.CodDocumentoRelacionado = dr.GetInt32(1);
					nuevo.CodTipoDocumento = dr.GetInt32(2);
					nuevo.Anulado = dr.GetInt32(3);
					nuevo.TipoGRCDR = dr.GetInt32(4);
				}
			}
			return nuevo;
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

	internal int obtenerTipoFacturaRelacionadoAGRC(int codNotaIngreso)
	{
		int tipoFactura = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("obtenerTipoFacturaCompraRelacionadoAGRC", con.conector);
			cmd.Parameters.AddWithValue("_codNotaIngreso", codNotaIngreso);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					tipoFactura = dr.GetInt32(0);
				}
			}
			return tipoFactura;
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

	internal DataTable obtenerListadoDocRelacFacturaCompraGRC(int tipo, int codNotaIngreso)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("obtenerListadoDocRelacFacturaCompraDeGRC", con.conector);
			cmd.Parameters.AddWithValue("_tipo", tipo);
			cmd.Parameters.AddWithValue("_codNotaIngreso", codNotaIngreso);
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

	internal int obtenerCodigoFacturacionSegunCodNotaDeIngreso(int codFactura)
	{
		int codFacturacion = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("obtenerCodigoFacturacionSegunCodNotaDeIngreso", con.conector);
			cmd.Parameters.AddWithValue("_codNotaIngreso", codFactura);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codFacturacion = dr.GetInt32(0);
				}
			}
			return codFacturacion;
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

	internal clsGuiaRemisionCompraDocumentoRelacionado buscaDocumentoRelacionado(int codGRC, int tipoDoc, int anulado)
	{
		clsGuiaRemisionCompraDocumentoRelacionado nuevo = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaGuiaRemisionCompraDocumentoRelacion2", con.conector);
			cmd.Parameters.AddWithValue("_codGRC", codGRC);
			cmd.Parameters.AddWithValue("_tipoDoc", tipoDoc);
			cmd.Parameters.AddWithValue("_anulado", anulado);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nuevo.CodGuiaRemisionCompra = dr.GetInt32(0);
					nuevo.CodDocumentoRelacionado = dr.GetInt32(1);
					nuevo.CodTipoDocumento = dr.GetInt32(2);
					nuevo.Anulado = dr.GetInt32(3);
					nuevo.TipoGRCDR = dr.GetInt32(4);
				}
			}
			return nuevo;
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

	internal int getEstadoDocumentoRelacionado(int codGRC, int tipoDoc)
	{
		int nuevo = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetEstadoGuiaRemisionCompraDocumentoRelacion", con.conector);
			cmd.Parameters.AddWithValue("_codGRC", codGRC);
			cmd.Parameters.AddWithValue("_tipoDoc", tipoDoc);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					nuevo = dr.GetInt32(0);
				}
			}
			return nuevo;
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

	internal DataTable obtenerListadoProductoFlete(int codGuiaRemisionCompra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoProductoFleteGRC", con.conector);
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", codGuiaRemisionCompra);
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

	internal DataTable generaFacturaVentaDeGRC(int codGuiaRemisionCompra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GenerarFacturaVentaDeGuiaRemisionCompra", con.conector);
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", codGuiaRemisionCompra);
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

	internal DataTable generaNotaCreditoDeGRC(int codGuiaRemisionCompra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GenerarNotaDeCreditoDeGuiaRemisionCompra_1", con.conector);
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", codGuiaRemisionCompra);
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

	internal bool setCodFacturaCompra(int codGRC, int codNI)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("setCodFacturaCompraGRC", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codGRC", codGRC);
			cmd.Parameters.AddWithValue("_codNotaIngreso", codNI);
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

	internal DataTable generaFacturaCompraDeGRC(int codGuiaRemisionCompra, int mostrarFlete)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GenerarFacturaCompraDeGuiaRemisionCompra", con.conector);
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", codGuiaRemisionCompra);
			cmd.Parameters.AddWithValue("_mostrarFlete", mostrarFlete);
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

	internal DataTable generaFacturaCompraDeGRC_1(int codGuiaRemisionCompra, int mostrarFlete)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GenerarFacturaCompraDeGuiaRemisionCompra_1", con.conector);
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", codGuiaRemisionCompra);
			cmd.Parameters.AddWithValue("_mostrarFlete", mostrarFlete);
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

	public bool updatedetalle(clsDetalleGuiaRemisionCompra detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleGuiaRemisionCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDetalleGuiaRemisionCompra", detalle.ICodDetalleGuiaRemisionCompra);
			cmd.Parameters.AddWithValue("_codProducto", detalle.ICodProducto);
			cmd.Parameters.AddWithValue("_codDetalleordenCompra", detalle.ICodDetalleOrdenCOmpra);
			cmd.Parameters.AddWithValue("_codGuiaRemisionCompra", detalle.ICodGuiaRemisionCOmpra);
			cmd.Parameters.AddWithValue("_moneda", detalle.IcodMoneda);
			cmd.Parameters.AddWithValue("_unidadingresada", detalle.IUnidadIngresada);
			cmd.Parameters.AddWithValue("_cantidad", detalle.DCantidad);
			cmd.Parameters.AddWithValue("_cantidadrespaldo", detalle.DCantidadRespaldo);
			cmd.Parameters.AddWithValue("_fechaingreso", detalle.FFechaIngreso);
			cmd.Parameters.AddWithValue("_estado", 1);
			cmd.Parameters.AddWithValue("_codUser", detalle.ICOdUser);
			cmd.Parameters.AddWithValue("_fechaRegistro", detalle.FFechaRegistro);
			cmd.Parameters.AddWithValue("_anulado", detalle.IAnulado);
			cmd.Parameters.AddWithValue("_codEstado", detalle.IEstado);
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
			cmd = new MySqlCommand("EliminarDetalleGuiaRemisionCompra", con.conector);
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
			cmd = new MySqlCommand("MuestraGuiaRemisionCompra", con.conector);
			cmd.Parameters.AddWithValue("codguia", CodGuiaRemision);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					GuiaRemision = new clsGuiaRemision();
					GuiaRemision.CodGuiaRemision = dr.GetString(0);
					GuiaRemision.NumDoc = (dr.IsDBNull(1) ? "" : dr.GetString(1));
					GuiaRemision.ICodOrdenCompra = ((!dr.IsDBNull(2)) ? dr.GetInt32(2) : 0);
					GuiaRemision.numeroOc = (dr.IsDBNull(3) ? "" : dr.GetString(3));
					GuiaRemision.CodFactura = ((!dr.IsDBNull(4)) ? dr.GetInt32(4) : 0);
					GuiaRemision.CodDocumentoRelacionado = ((!dr.IsDBNull(5)) ? dr.GetInt32(5) : 0);
					GuiaRemision.CodAlmacen = ((!dr.IsDBNull(6)) ? dr.GetInt32(6) : 0);
					GuiaRemision.CodMotivo = ((!dr.IsDBNull(7)) ? dr.GetInt32(7) : 0);
					GuiaRemision.FechaEmision = (dr.IsDBNull(8) ? DateTime.MinValue : dr.GetDateTime(8));
					GuiaRemision.FechaTraslado = (dr.IsDBNull(9) ? DateTime.MinValue : dr.GetDateTime(9));
					GuiaRemision.fechaingresoalmacen = (dr.IsDBNull(10) ? DateTime.MinValue : dr.GetDateTime(10));
					GuiaRemision.FechaRegistro = dr.GetDateTime(11);
					GuiaRemision.CodUser = ((!dr.IsDBNull(12)) ? dr.GetInt32(12) : 0);
					GuiaRemision.CodEmpresaTransporte = ((!dr.IsDBNull(13)) ? dr.GetInt32(13) : 0);
					GuiaRemision.Comentario = dr.GetString(14);
					GuiaRemision.Estado = dr.GetInt32(15);
					GuiaRemision.CodProveedor = dr.GetInt32(16);
					GuiaRemision.FechaModificacion = dr.GetDateTime(17);
					GuiaRemision.CodUserModificacion = dr.GetInt32(18);
					GuiaRemision.EstadoGeneracion = dr.GetInt32(19);
					GuiaRemision.FleteConIgv = dr.GetDouble(20);
					GuiaRemision.FleteSinIgv = dr.GetDouble(21);
					GuiaRemision.OpcionFlete = dr.GetInt32(22);
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
			cmd = new MySqlCommand("MuestraDetalleGuiaRemisionCompra", con.conector);
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

	public DataTable ListaGuiaRemisiones(int CodAlmacen, int codsucursal, int tipoFecha, DateTime fechaInicio, DateTime fechaFinal, int codProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaGuiaRemisionesCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("codsucu", codsucursal);
			cmd.Parameters.AddWithValue("tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
			cmd.Parameters.AddWithValue("fechaFinal", fechaFinal);
			cmd.Parameters.AddWithValue("_codProducto", codProducto);
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
