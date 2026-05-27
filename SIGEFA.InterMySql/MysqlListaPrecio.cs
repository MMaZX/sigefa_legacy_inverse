using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlListaPrecio : IListaPrecio
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsListaPrecio lista)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaListaPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codSu", lista.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("nombre", lista.Nombre);
			oParam = cmd.Parameters.AddWithValue("margenprov", lista.MargenProv);
			oParam = cmd.Parameters.AddWithValue("margen", lista.Margen);
			oParam = cmd.Parameters.AddWithValue("desc1", lista.Descuento1);
			oParam = cmd.Parameters.AddWithValue("desc2", lista.Descuento2);
			oParam = cmd.Parameters.AddWithValue("desc3", lista.Descuento3);
			oParam = cmd.Parameters.AddWithValue("precioprom", lista.PrecioProm);
			oParam = cmd.Parameters.AddWithValue("listaorigen", lista.ListaOrigen);
			oParam = cmd.Parameters.AddWithValue("variacion", lista.Variacion);
			oParam = cmd.Parameters.AddWithValue("updateauto", lista.Update);
			oParam = cmd.Parameters.AddWithValue("decimales", lista.Decimales);
			oParam = cmd.Parameters.AddWithValue("redondear", lista.Redondear);
			oParam = cmd.Parameters.AddWithValue("codFormaP", lista.CodFormaPago);
			oParam = cmd.Parameters.AddWithValue("codusu", lista.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			lista.CodListaPrecio = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsListaPrecio lista)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaListaPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codlista", lista.CodListaPrecio);
			cmd.Parameters.AddWithValue("nombre", lista.Nombre);
			cmd.Parameters.AddWithValue("margenprov", lista.MargenProv);
			cmd.Parameters.AddWithValue("margen", lista.Margen);
			cmd.Parameters.AddWithValue("desc1", lista.Descuento1);
			cmd.Parameters.AddWithValue("desc2", lista.Descuento2);
			cmd.Parameters.AddWithValue("desc3", lista.Descuento3);
			cmd.Parameters.AddWithValue("precioprom", lista.PrecioProm);
			cmd.Parameters.AddWithValue("listaorigen", lista.ListaOrigen);
			cmd.Parameters.AddWithValue("variacion", lista.Variacion);
			cmd.Parameters.AddWithValue("updateauto", lista.Update);
			cmd.Parameters.AddWithValue("decimales", lista.Decimales);
			cmd.Parameters.AddWithValue("redondear", lista.Redondear);
			cmd.Parameters.AddWithValue("codFormaP", lista.CodFormaPago);
			cmd.Parameters.AddWithValue("estado", lista.Estado);
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

	public bool Delete(int CodListaPrecio)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarListaPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codlista", CodListaPrecio);
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

	public bool Anular(int codSucursal, int Codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularListaPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codSu", codSucursal);
			cmd.Parameters.AddWithValue("codlista", Codigo);
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

	public bool Activar(int codSucursal, int Codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActivarListaPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codSu", codSucursal);
			cmd.Parameters.AddWithValue("codlista", Codigo);
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

	public bool Updatedetalle(clsDetalleListaPrecio detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleListaPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codlista", detalle.CodListaPrecio);
			cmd.Parameters.AddWithValue("coddetalista", detalle.CodDetalleLista);
			cmd.Parameters.AddWithValue("margen", detalle.Margen);
			cmd.Parameters.AddWithValue("desc1", detalle.Descuento1);
			cmd.Parameters.AddWithValue("desc2", detalle.Descuento2);
			cmd.Parameters.AddWithValue("desc3", detalle.Descuento3);
			cmd.Parameters.AddWithValue("precioneto", detalle.PrecioNeto);
			cmd.Parameters.AddWithValue("precio", detalle.Precio);
			cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
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

	public bool updatedetallePorFiltro(clsDetalleListaPrecio detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleListaPrecio_FiltroProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codlista", detalle.CodListaPrecio);
			cmd.Parameters.AddWithValue("margen", detalle.Margen);
			cmd.Parameters.AddWithValue("desc1", detalle.Descuento1);
			cmd.Parameters.AddWithValue("desc2", detalle.Descuento2);
			cmd.Parameters.AddWithValue("desc3", detalle.Descuento3);
			cmd.Parameters.AddWithValue("precioneto", detalle.PrecioNeto);
			cmd.Parameters.AddWithValue("precio", detalle.Precio);
			cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
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

	public DataTable MuestraListas(int codsu)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListasPrecios", con.conector);
			cmd.Parameters.AddWithValue("codSu", codsu);
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

	public clsListaPrecio CargaListaPrecio(int Codigo)
	{
		clsListaPrecio lista = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecio", con.conector);
			cmd.Parameters.AddWithValue("codlista", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					lista = new clsListaPrecio();
					lista.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(0));
					lista.CodSucursal = Convert.ToInt32(dr.GetDecimal(1));
					lista.Nombre = dr.GetString(2);
					lista.MargenProv = dr.GetBoolean(3);
					lista.Margen = Convert.ToDouble(dr.GetDecimal(4));
					lista.Descuento1 = Convert.ToDouble(dr.GetDecimal(5));
					lista.Descuento2 = Convert.ToDouble(dr.GetDecimal(6));
					lista.Descuento3 = Convert.ToDouble(dr.GetDecimal(7));
					lista.PrecioProm = dr.GetBoolean(8);
					lista.ListaOrigen = Convert.ToInt32(dr.GetDecimal(9));
					lista.Variacion = Convert.ToDouble(dr.GetDecimal(10));
					lista.Update = dr.GetBoolean(11);
					lista.Decimales = Convert.ToInt32(dr.GetDecimal(12));
					lista.Redondear = dr.GetBoolean(13);
					lista.Estado = dr.GetBoolean(14);
					lista.CodUser = Convert.ToInt32(dr.GetDecimal(15));
					lista.FechaRegistro = dr.GetDateTime(16);
					lista.CodFormaPago = dr.GetInt32(17);
				}
			}
			return lista;
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

	public DataTable MuestraPreciosProducto(int CodProducto, int codsu, int codalma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPreciosProducto2", con.conector);
			cmd.Parameters.AddWithValue("codpro", CodProducto);
			cmd.Parameters.AddWithValue("codSu", codsu);
			cmd.Parameters.AddWithValue("codAlma", codalma);
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

	public bool GeneraPreciosLista(int CodLista, int codalma, int Decimales)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GeneraListaPrecios", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codlista", CodLista);
			oParam = cmd.Parameters.AddWithValue("codalma", codalma);
			oParam = cmd.Parameters.AddWithValue("decimales", Decimales);
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

	public bool GeneraPreciosListaProveedor(int CodLista, int CodAlmacen, int Decimales, int CodProveedor)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GeneraListaPreciosProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codlista", CodLista);
			oParam = cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("decimales", Decimales);
			oParam = cmd.Parameters.AddWithValue("codprove", CodProveedor);
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

	public List<int> ListaProductosAlmacen(int CodAlmacen)
	{
		List<int> lista = new List<int>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaProdListaPrecios", con.conector);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					lista.Add(Convert.ToInt32(dr["codProductoAlmacen"]));
				}
			}
			return lista;
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

	public DataTable CargaListaPrecios(int CodLista)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("BuscaProductoListaPrecio", con.conector);
			cmd.Parameters.AddWithValue("codlista", CodLista);
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

	public List<int> MuestraListasProveedor(int CodAlmacen)
	{
		List<int> lista = new List<int>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaListaPrecios", con.conector);
			cmd.Parameters.AddWithValue("codSu", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					lista.Add(Convert.ToInt32(dr["codListaPrecio"]));
				}
			}
			return lista;
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

	public DataTable MuestraListasPorFiltro(int CodAlmacen, int rango1, int rango2, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxRango", con.conector);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("rango1", rango1);
			cmd.Parameters.AddWithValue("rango2", rango2);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorProveedor(int codAlmacen, int codProv, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxProveedor", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codProv", codProv);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorFamilia(int codAlmacen, int codFam, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxFamilia", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorLinea(int codAlmacen, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxLinea", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("codLin", codLin);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorRangoProv(int codAlmacen, int rango1, int rango2, int codProv, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxRangoProv", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("rango1", rango1);
			cmd.Parameters.AddWithValue("rango2", rango2);
			cmd.Parameters.AddWithValue("codProv", codProv);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorRangoFam(int codAlmacen, int rango1, int rango2, int codFam, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxRangoFam", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("rango1", rango1);
			cmd.Parameters.AddWithValue("rango2", rango2);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorProveedorFam(int codAlmacen, int codProv, int codFam, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxProveedorFam", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codProv", codProv);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorTodos(int codAlmacen, int rango1, int rango2, int codProv, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxTodos", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("rango1", rango1);
			cmd.Parameters.AddWithValue("rango2", rango2);
			cmd.Parameters.AddWithValue("codProv", codProv);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("codLin", codLin);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPorRangoFamLin(int codAlmacen, int rango1, int rango2, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxRanFamLin", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("rango1", rango1);
			cmd.Parameters.AddWithValue("rango2", rango2);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("codLin", codLin);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public DataTable MuestraListaPorProveedorFamLin(int codAlmacen, int codProv, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxProveedorFamLin", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codProv", codProv);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("codLin", codLin);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaParcial(int codAlmacen, int rango1, int rango2, int codProv, int codFam, int listaorigen, int decimales)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioParcial", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("rango1", rango1);
			cmd.Parameters.AddWithValue("rango2", rango2);
			cmd.Parameters.AddWithValue("codProv", codProv);
			cmd.Parameters.AddWithValue("codFam", codFam);
			cmd.Parameters.AddWithValue("lista", listaorigen);
			cmd.Parameters.AddWithValue("decimales", decimales);
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

	public DataTable MuestraListaPrecioxFormaPago(int codAlmacen, int codForma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPrecioxFormaPago", con.conector);
			cmd.Parameters.AddWithValue("codSu", codAlmacen);
			cmd.Parameters.AddWithValue("codformapago", codForma);
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
