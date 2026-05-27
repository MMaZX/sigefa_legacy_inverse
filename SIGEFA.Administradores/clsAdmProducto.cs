using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmProducto
{
	private IProducto Mpro = new MysqlProducto();

	public bool insert(clsProducto pro)
	{
		try
		{
			return Mpro.Insert(pro);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	internal DataTable cargaReferenciasExternas(int codProducto, int codUnidadMedida)
	{
		try
		{
			return Mpro.cargaReferenciasExternas(codProducto, codUnidadMedida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertproductoalmacen(clsProducto pro)
	{
		try
		{
			return Mpro.InsertProductoAlmacen(pro);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool insertcaracteristica(clsCaracteristicaProducto carpro)
	{
		try
		{
			return Mpro.InsertCaracteristica(carpro);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	internal bool registraReferenciaExterna(ref ReferenciaExterna obj)
	{
		try
		{
			return Mpro.registraReferenciaExterna(ref obj);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool insertnota(clsNotaProducto notapro)
	{
		try
		{
			return Mpro.InsertNota(notapro);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool insertunidadequivalente(clsUnidadEquivalente unidadequi, int coti)
	{
		try
		{
			return Mpro.InsertUnidad(unidadequi, coti);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	internal bool editaReferenciaExterna(ReferenciaExterna obj)
	{
		try
		{
			return Mpro.editaReferenciaExterna(obj);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	internal bool eliminaReferenciaExterna(int codReferencia)
	{
		try
		{
			return Mpro.eliminaReferenciaExterna(codReferencia);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	internal bool cambiarFleteDeProducto(int codProd, double flete)
	{
		try
		{
			return Mpro.cambiarFleteDeProducto(codProd, flete);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool update(clsProducto pro)
	{
		try
		{
			return Mpro.Update(pro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updateMasivo(DataTable pro)
	{
		try
		{
			return Mpro.UpdateMasivo(pro);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool updateproductoalmacen(clsProducto pro)
	{
		try
		{
			return Mpro.UpdateProductoAlmacen(pro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updateunidadequivalente(clsUnidadEquivalente unidadequi)
	{
		try
		{
			return Mpro.UpdateUnidad(unidadequi);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public int GetUnidadesEquivalentesPorUnidadBase(int codProducto, int codUnidadBase)
	{
		try
		{
			return Mpro.GetUnidadesEquivalentesPorUnidadBase(codProducto, codUnidadBase);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public bool delete(int Codpro)
	{
		try
		{
			return Mpro.Delete(Codpro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ValidaStockProducto(int Codpro)
	{
		try
		{
			return Mpro.ValidaStockProducto(Codpro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraStockAlmacenes(int codproducto)
	{
		try
		{
			return Mpro.MuestraStockAlmacenes(codproducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool deleteproductoalmacen(int Codpro)
	{
		try
		{
			return Mpro.DeleteProductoAlmacen(Codpro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletecaracteristica(int Codcarpro)
	{
		try
		{
			return Mpro.DeleteCaracteristica(Codcarpro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletenota(int Codnota)
	{
		try
		{
			return Mpro.DeleteNota(Codnota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deleteunidadequivalente(int Coduniequi)
	{
		try
		{
			return Mpro.DeleteUnidad(Coduniequi);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraProductos(int Nivel, int Codigo, int CodAlmacen)
	{
		try
		{
			return Mpro.ListaProductos(Nivel, Codigo, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CatalogoProductos()
	{
		try
		{
			return Mpro.CatalogoProductos();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CatalogoProductosCotizacion()
	{
		try
		{
			return Mpro.CatalogoProductosCotizacion();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ActualizaEstadoProductoCotizacion(int Codpro)
	{
		try
		{
			return Mpro.ActualizaEstadoProductoCotizacion(Codpro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaProductosReporte(int CodAlmacen, int Tipo, int Inicio)
	{
		try
		{
			return Mpro.ListaProductosReporte(CodAlmacen, Tipo, Inicio);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable StockProductoAlmacenes(int CodEmpresa, int CodProducto)
	{
		try
		{
			return Mpro.StockProductoAlmacenes(CodEmpresa, CodProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionIngreso(int Tipo, int codalma)
	{
		try
		{
			return Mpro.RelacionProductosIngreso(Tipo, codalma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionIngresoPorProveedor(int Tipo, int codalma, int codproveedor)
	{
		try
		{
			return Mpro.RelacionIngresoPorProveedor(Tipo, codalma, codproveedor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionSalida(int Tipo, int CodAlmacen, int CodLista)
	{
		try
		{
			return Mpro.RelacionProductosSalida(Tipo, CodAlmacen, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionSalidaTodo(int Tipo, int CodAlmacen, int CodLista)
	{
		try
		{
			return Mpro.RelacionProductosSalidaTodo(Tipo, CodAlmacen, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionSalidaTodoSinStock(int Tipo, int CodAlmacen, int CodLista)
	{
		try
		{
			return Mpro.RelacionProductosSalidaTodoSinStock(Tipo, CodAlmacen, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionSalidaSinAfectarStock(int Tipo, int CodAlmacen, int CodLista)
	{
		try
		{
			return Mpro.RelacionSalidaSinAfectarStock(Tipo, CodAlmacen, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraCaracteristicas(int CodigoProducto)
	{
		try
		{
			return Mpro.ListaCaracteristicas(CodigoProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraProductosProveedor(int CodigoProducto, int CodigoAlmacen)
	{
		try
		{
			return Mpro.MuestraProductosProveedor(CodigoProducto, CodigoAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable ListadoProductosParaRequerimientoAlmacen(int tipoArt, int CodAlmacen, int codLista)
	{
		try
		{
			return Mpro.RelacionProductosParaRequerimientoAlmacen(tipoArt, CodAlmacen, codLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraNotas(int CodigoProducto)
	{
		try
		{
			return Mpro.ListaNotas(CodigoProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProducto(int CodProducto, int CodAlmacen)
	{
		try
		{
			return Mpro.CargaProducto(CodProducto, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProductoCotizacion(int CodProducto, int CodAlmacen)
	{
		try
		{
			return Mpro.CargaProductoCotizacion(CodProducto, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto ListaTotalprod2(int CodProducto, int CodAlmacen, int CodUnidad)
	{
		try
		{
			return Mpro.ListaTotalprod2(CodProducto, CodAlmacen, CodUnidad);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProductoDetalle(int CodProducto, int CodAlmacen, int Caso, int CodLista, int totalstock)
	{
		try
		{
			return Mpro.CargaProductoDetalle(CodProducto, CodAlmacen, Caso, CodLista, totalstock);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProductoDetalleCotizacion(int CodProducto, int CodCotizacion, int CodAlmacen, int Caso, int CodLista)
	{
		try
		{
			return Mpro.CargaProductoDetalleCotizacion(CodProducto, CodCotizacion, CodAlmacen, Caso, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProductoDetalleSinAfectarStock(int CodProducto, int CodAlmacen, int Caso, int CodLista)
	{
		try
		{
			return Mpro.CargaProductoDetalleSinAfectarStock(CodProducto, CodAlmacen, Caso, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProductoDetalleCodBarras(string CodProducto, int CodAlmacen, int Caso, int CodLista)
	{
		try
		{
			return Mpro.CargaProductoDetalleCodBarras(CodProducto, CodAlmacen, Caso, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProductoDetalle1(int CodProducto, int CodAlmacen, int Caso, int CodLista)
	{
		try
		{
			return Mpro.CargaProductoDetalle1(CodProducto, CodAlmacen, Caso, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaDatosProductoOrden(int CodProducto, int CodAlmacen, int codusu, decimal cant)
	{
		try
		{
			return Mpro.CargaDatosProductoOrden(CodProducto, CodAlmacen, codusu, cant);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaDatosProductoAgrupados(int CodProducto)
	{
		try
		{
			return Mpro.CargaDatosProductoAgrupados(CodProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto CargaProductoDetalleR(string Referencia, int CodAlmacen, int Caso, int CodLista)
	{
		try
		{
			return Mpro.CargaProductoDetalleR(Referencia, CodAlmacen, Caso, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ArbolProductos()
	{
		try
		{
			return Mpro.ArbolProductos();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto MuestraProductosTransferencia(int codProducto, int codAlmacen)
	{
		try
		{
			return Mpro.MuestraProductosTransferencia(codProducto, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsProducto MuestraProductosTransferencia_nuevo(int codProducto, int codAlmacen)
	{
		try
		{
			return Mpro.MuestraProductosTransferencia_nuevo(codProducto, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionCotizacion(int Tipo, int CodAlmacen, int CodLista, int todos)
	{
		try
		{
			return Mpro.RelacionProductosCotizacion(Tipo, CodAlmacen, CodLista, todos);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool creaProductoAlmacenMasivo(int codProducto, int codUsuario)
	{
		try
		{
			return Mpro.creaProductoAlmacenMasivo(codProducto, codUsuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public decimal CargaPrecioProducto(int CodProducto, int CodAlmacen, int codmon)
	{
		try
		{
			return Mpro.CargaPrecioProducto(CodProducto, CodAlmacen, codmon);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public DataTable MuestraStockAlmacenes()
	{
		try
		{
			return Mpro.MuestraStockAlmacenes();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraStockAlmacenesPendientes()
	{
		try
		{
			return Mpro.MuestraStockAlmacenesPendientes();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable BuscarProducto(int codProducto)
	{
		try
		{
			return Mpro.BuscarProducto(codProducto);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return null;
		}
	}

	public DataTable RelacionProductos(int codalma)
	{
		try
		{
			return Mpro.RelacionProductos(codalma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable RelacionVendedor(int CodTipArt, int CodAlmacen, int CodLista, int CodVendedor)
	{
		try
		{
			return Mpro.RelacionVendedor(CodTipArt, CodAlmacen, CodLista, CodVendedor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsProducto> ListaProdAlmacen(int codProducto, int CodAlmacen)
	{
		try
		{
			return Mpro.ListaProdAlmacen(codProducto, CodAlmacen);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return null;
		}
	}

	public List<clsProducto> ListaProdConsultor(int CodVendedor)
	{
		try
		{
			return Mpro.ListaProdConsultor(CodVendedor);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return null;
		}
	}

	public DataTable CargaProductoSunat()
	{
		try
		{
			return Mpro.CargaProductoSunat();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsProducto> VentasProductosCount(int CodFac)
	{
		try
		{
			return Mpro.VentasProductosCount(CodFac);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return null;
		}
	}

	public DataTable MuestraUnidadesEquivalentesCompra(int CodigoProducto, int codAlmacen)
	{
		try
		{
			return Mpro.ListaUnidadesEquivalentesCompra(CodigoProducto, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraUnidadesEquivalentesVentaCotizacion(int CodigoProducto, int codAlmacen, int CodCotizacion)
	{
		try
		{
			return Mpro.ListaUnidadesEquivalentesVentaCotizacion(CodigoProducto, codAlmacen, CodCotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable cargaetiquetas(int codAlmacen)
	{
		try
		{
			return Mpro.cargaetiquetas(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable cargaetiquetasolola3(int codAlmacen)
	{
		try
		{
			return Mpro.cargaetiquetasolola3(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable cargaetiquetauna(int codAlmacen)
	{
		try
		{
			return Mpro.cargaetiquetauna(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraUnidadesEquivalentesVenta(int CodigoProducto, int codAlmacen)
	{
		try
		{
			return Mpro.ListaUnidadesEquivalentesVenta(CodigoProducto, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraUnidadesEquivalentesVenta1(int CodigoProducto, int codAlmacen)
	{
		try
		{
			return Mpro.ListaUnidadesEquivalentesVenta1(CodigoProducto, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraUnidadesEquivalentes(int CodigoProducto, int codAlmacen)
	{
		try
		{
			return Mpro.ListaUnidadesEquivalentes(CodigoProducto, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaUnidadesEquivalentes(int CodigoProducto)
	{
		try
		{
			return Mpro.CargaUnidadesEquivalentes(CodigoProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsUnidadEquivalente CargaUnidadEquivalente(int CodigoUnidad, int CodigoProducto, int compraVenta)
	{
		try
		{
			return Mpro.CargaUnidadEquivalente(CodigoUnidad, CodigoProducto, compraVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int getUnidadCompra(int codProd)
	{
		try
		{
			return Mpro.getUnidadCompra(codProd);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public bool updateunidadequivalente(int cod, decimal precio)
	{
		try
		{
			return Mpro.UpdateUnidadEquivalente(cod, precio);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public int UnidadBase(int codPro, int codalma)
	{
		try
		{
			return Mpro.UnidadBase(codPro, codalma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public decimal FactorProducto(int codPro, int undBase, int undEqui, int tipo)
	{
		try
		{
			return Mpro.FactorProducto(codPro, undBase, undEqui, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public string SiglaUnidadBase(int codUnd)
	{
		try
		{
			return Mpro.SiglaUnidadBase(codUnd);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return "";
		}
	}

	public clsUnidadEquivalente PrecioVenta(int coduni, int codalmacen)
	{
		try
		{
			return Mpro.PrecioVenta(coduni, codalmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsUnidadEquivalente PrecioVentaCotización(int coduni, int codalmacen, int codcotizacion)
	{
		try
		{
			return Mpro.PrecioVentaCotizacion(coduni, codalmacen, codcotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsUnidadEquivalente PrecioVentaSinStock(int coduni, int codalmacen)
	{
		try
		{
			return Mpro.PrecioVentaSinStock(coduni, codalmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public decimal UltimoPrecioCompraProducto(int codigoProducto, int codigoUnidad, int codigoUnidadEquivalente)
	{
		try
		{
			return Mpro.UltimoPrecioCompraProducto(codigoProducto, codigoUnidad, codigoUnidadEquivalente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public decimal UltimoPrecioCompraProductoCotizacion(int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			return Mpro.UltimoPrecioCompraProductoCotizacion(codigoProducto, codigoUnidadEquivalente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public DataTable CostoTotalProducto(int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			return Mpro.CostoTotalProducto(codigoProducto, codigoUnidadEquivalente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CostoTotalProductoCotizacion(int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			return Mpro.CostoTotalProductoCotizacion(codigoProducto, codigoUnidadEquivalente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public decimal UltimoPrecioVentaProductoCotizacion(int CodCliente, int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			return Mpro.UltimoPrecioVentaProductoCotizacion(CodCliente, codigoProducto, codigoUnidadEquivalente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public decimal UltimoPrecioVentaProducto(int codigoProducto, int codigoUnidad)
	{
		try
		{
			return Mpro.UltimoPrecioVentaProducto(codigoProducto, codigoUnidad);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public bool ActualizarPrecioVentaProductoPorUnidad(int codigoProducto, int codigoUnidad, int codigoAlmacen, decimal nuevoPrecioVenta)
	{
		try
		{
			return Mpro.ActualizarPrecioVentaProductoPorUnidad(codigoProducto, codigoUnidad, codigoAlmacen, nuevoPrecioVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsUnidadEquivalente Factor(int codProducto, int codUnidadMedida, int codUnidaEqui)
	{
		return Mpro.Factor(codProducto, codUnidadMedida, codUnidaEqui);
	}

	public clsProducto PrecioPromedio(int codProducto, int codalm)
	{
		try
		{
			return Mpro.PrecioPromedio(codProducto, codalm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int GetCodProducto_xDescripcion(string descripcion)
	{
		try
		{
			return Mpro.GetCodProducto_xDescripcion(descripcion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaCodigoUE(int codigo)
	{
		try
		{
			return Mpro.ValidaCodigoUE(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaCodigoUE(string unidad)
	{
		try
		{
			return Mpro.ValidaCodigoUE(unidad);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaCodigoProducto(int codigo)
	{
		try
		{
			return Mpro.ValidaCodigoProducto(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ExisteProductoSinFacturar(int codigo)
	{
		try
		{
			return Mpro.ExisteProductoSinFacturar(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaCodigoMoneda(int codigo)
	{
		try
		{
			return Mpro.ValidaCodigoMoneda(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaCodigoMoneda(string moneda)
	{
		try
		{
			return Mpro.ValidaCodigoMoneda(moneda);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaTipoPrecio(string tipoPrecio)
	{
		try
		{
			return Mpro.ValidaTipoPrecio(tipoPrecio);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaTipoPrecio(int codigo)
	{
		try
		{
			return Mpro.ValidaTipoPrecio(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int GetCodUnidad(string descripcion)
	{
		try
		{
			return Mpro.GetCodUnidad(descripcion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int GetCodTipoPrecio(string descripcion)
	{
		try
		{
			return Mpro.GetCodTipoPrecio(descripcion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int GetCodMoneda(string descripcion)
	{
		try
		{
			return Mpro.GetCodMoneda(descripcion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int ValidaUnidadEquivalente(int codigo)
	{
		try
		{
			return Mpro.ValidaUnidadEquivalente(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public DataTable MuestratipoNC()
	{
		try
		{
			return Mpro.MuestratipoNC();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int MuestraCantidadProductos()
	{
		try
		{
			return Mpro.MuestraCantidadProductos();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int GetUnidadesEquivalentesPorUnidadBase(int codProducto)
	{
		try
		{
			return Mpro.GetNumeroUnidadesEquivalentesPorProducto(codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int VerificaProductoAlmacen(int codProducto)
	{
		try
		{
			return Mpro.VerificaProductoAlmacen(codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public decimal GetValorPromedioSoles(int codProducto, int codAlmacen)
	{
		try
		{
			return Mpro.GetValorPromedioSoles(codProducto, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public DataTable GetPromedioProductosVendidos(DateTime fechainicio, DateTime fechafinal, int codFamilia, int codLinea, int codGrupo, int codMarca, int codProducto, int cantidadDias)
	{
		try
		{
			return Mpro.GetPromedioProductosVendidos(fechainicio, fechafinal, codFamilia, codLinea, codGrupo, codMarca, codProducto, cantidadDias);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable GetTotalizadoProductosVendidos(DateTime fechainicio, DateTime fechafinal, int codFamilia, int codLinea, int codGrupo, int codMarca, int codProducto, int cantidadDias)
	{
		try
		{
			return Mpro.GetTotalizadoProductosVendidos(fechainicio, fechafinal, codFamilia, codLinea, codGrupo, codMarca, codProducto, cantidadDias);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int GetProductoFacturado(int codProducto)
	{
		try
		{
			return Mpro.GetProductoFacturado(codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	internal double obtenerFleteDeProducto(double CodProducto, int igv, int codunidad, double cantidad)
	{
		try
		{
			return Mpro.obtenerFleteDeProducto(CodProducto, igv, codunidad, cantidad);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return double.NaN;
		}
	}

	public DataTable muestraStockProducto_almacenes(int CodProducto)
	{
		try
		{
			return Mpro.muestraStockProducto_almacenes(CodProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsUnidadEquivalente> unidadCompraxProducto(int codproducto)
	{
		try
		{
			return Mpro.unidadCompraxProducto(codproducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsUnidadEquivalente> unidadVentaxProducto(int codproducto)
	{
		try
		{
			return Mpro.unidadVentaxProducto(codproducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool UpdateUnidadEquivalenteMasivo(DataTable unds)
	{
		try
		{
			return Mpro.UpdateUnidadEquivalenteMasivo(unds);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable listarStocksProducto(int codpro)
	{
		try
		{
			return Mpro.listarStocksProducto(codpro);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertaStocksProducto(int codpro, int codalma, decimal stockmin, decimal stockmax, decimal capmax)
	{
		try
		{
			return Mpro.insertaStocksProducto(codpro, codalma, stockmin, stockmax, capmax);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool updateStocksProducto(int codpro, int codalma, decimal stockmin, decimal stockmax, decimal capmax)
	{
		try
		{
			return Mpro.updateStocksProducto(codpro, codalma, stockmin, stockmax, capmax);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool eliminaStocksProducto(int codpro, int codalma)
	{
		try
		{
			return Mpro.eliminaStocksProducto(codpro, codalma);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public DataTable PrecioVentaProductoPorUnidad(int CodigoProducto, int codunidadmedida)
	{
		try
		{
			return Mpro.PrecioVentaProductoPorUnidad(CodigoProducto, codunidadmedida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable listaPreciosCantidad(int CodigoProducto)
	{
		try
		{
			return Mpro.listaPreciosCantidad(CodigoProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool GuardaPrecioCantidad(int codueq, decimal cantmax, decimal cantmin, int coduser)
	{
		try
		{
			return Mpro.GuardaPrecioCantidad(codueq, cantmax, cantmin, coduser);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool GuardaCategorizacion(int codproducto, string desde, string hasta, string descripcion)
	{
		try
		{
			return Mpro.GuardaCategorizacion(codproducto, desde, hasta, descripcion);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool GuardaSituacion(int codproducto, string desde, string hasta, string descripcion)
	{
		try
		{
			return Mpro.GuardaSituacion(codproducto, desde, hasta, descripcion);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool ActualizaStockDisponible(int codproducto, int codalmacen, decimal stock)
	{
		try
		{
			return Mpro.ActualizaStockDisponible(codproducto, codalmacen, stock);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool ActualizaCategorizacion(int codcategorizacion, string condicion, string descripcion)
	{
		try
		{
			return Mpro.ActualizaCategorizacion(codcategorizacion, condicion, descripcion);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool ActualizaSituacion(int codsituacion, string desde, string hasta, string descripcion)
	{
		try
		{
			return Mpro.ActualizaSituacion(codsituacion, desde, hasta, descripcion);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool EliminaPrecioCantidad(int codpreciocantidad)
	{
		try
		{
			return Mpro.EliminaPrecioCantidad(codpreciocantidad);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool EliminaCategorizacion(int codcategorizacion)
	{
		try
		{
			return Mpro.EliminaCategorizacion(codcategorizacion);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool EliminaSituacion(int codsituacion)
	{
		try
		{
			return Mpro.EliminaSituacion(codsituacion);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public DataTable validaPrecioCantidad(int codequi, int codpro, decimal cant)
	{
		try
		{
			return Mpro.validaPrecioCantidad(codequi, codpro, cant);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable listadodeProductos(int almacen, int marca, int familia, int linea, int grupo, int proveedor)
	{
		try
		{
			return Mpro.listadodeproductos(almacen, marca, familia, linea, grupo, proveedor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertaProductoAsociado(int codProd, int codAsoc)
	{
		try
		{
			return Mpro.insertaProductoAsociado(codProd, codAsoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deleteAsociadosDeProducto(int codProd, int codAsoc)
	{
		try
		{
			return Mpro.deleteAsociadosDeProducto(codProd, codAsoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable cargaProductosAsociados()
	{
		try
		{
			return Mpro.cargaProductosAsociados();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable cargaProductos()
	{
		try
		{
			return Mpro.cargaProductos();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable cargaStockProductos()
	{
		try
		{
			return Mpro.cargaStockProductos();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int getCantidadMaximaAsociadosXProducto()
	{
		try
		{
			return Mpro.getCantidadMaximaAsociadosXProducto();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public DataTable CargaCategorizacion(int codproducto)
	{
		try
		{
			return Mpro.CargaCategorizacion(codproducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaSituacion(int codproducto)
	{
		try
		{
			return Mpro.CargaSituacion(codproducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CatalogoCombosProductos(bool estado, int tipoformulario)
	{
		try
		{
			return Mpro.CatalogoCombosProductos(estado, tipoformulario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool deletecombo(int codcombo)
	{
		try
		{
			return Mpro.Deletecombo(codcombo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertcombo(clsComboProductos combo)
	{
		try
		{
			return Mpro.InsertCombo(combo);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public clsComboProductos CargaCombo(int codcombo)
	{
		try
		{
			return Mpro.CargaCombo(codcombo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaProductosCombo(int CodProducto)
	{
		try
		{
			return Mpro.CargaProductosCombo(CodProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleComboVenta(int codcombo, int CodAlmacen)
	{
		try
		{
			return Mpro.CargaDetalleComboVenta(codcombo, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertdetallecombo(clsDetalleCombo detalle)
	{
		try
		{
			return Mpro.insertdetallecombo(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatecombo(clsComboProductos combo)
	{
		try
		{
			return Mpro.Updatecombo(combo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public decimal UltimoPrecioCompraProductoVenta(int codigoProducto, int codigoUnidad, int codigoUnidadEquivalente)
	{
		try
		{
			return Mpro.UltimoPrecioCompraProductoVenta(codigoProducto, codigoUnidad, codigoUnidadEquivalente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public DataTable CostoTotalProductoVenta(int codigoProducto, int codigoUnidadEquivalente)
	{
		try
		{
			return Mpro.CostoTotalProductoVenta(codigoProducto, codigoUnidadEquivalente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
