using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmGuiaRemisionCompra
{
	private MysqlGuiaRemisionCompra MGuiaRemisionCompra = new MysqlGuiaRemisionCompra();

	public string getEtiquetaGRC(int CodEtiqueta)
	{
		try
		{
			return MGuiaRemisionCompra.getEtiquetaSegunCodigo(CodEtiqueta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int getCodigoPrimeraGRCGenerada(int codOC)
	{
		try
		{
			return MGuiaRemisionCompra.getCodigoPrimeraGRCGenerada(codOC);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int getCodigoEtiquetaGRC(string cadEtiqueta)
	{
		try
		{
			return MGuiaRemisionCompra.getCodigoEtiquetaSegun(cadEtiqueta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	internal DataTable listarGRCdeOC(int codOrdenCompra)
	{
		try
		{
			return MGuiaRemisionCompra.ListadoDeGuiaDeRemisionDeCompraSegunOrdenCompra(codOrdenCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable MuestraDocumentosRelacionados(int codGRC)
	{
		try
		{
			return MGuiaRemisionCompra.ListadoDocumentosRelacionados(codGRC);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insert(clsGuiaRemision GuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.insert(GuiaRemision);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: N°- de Documento Repetido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertdetalle(clsDetalleGuiaRemisionCompra detalle)
	{
		try
		{
			return MGuiaRemisionCompra.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable cargaEstados()
	{
		try
		{
			return MGuiaRemisionCompra.cargaEstados();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool anularGuiaRemisionCompra(int cod_grc)
	{
		try
		{
			return MGuiaRemisionCompra.anulacion(cod_grc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsGuiaRemision GuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.update(GuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleGuiaRemisionCompra detalle)
	{
		try
		{
			return MGuiaRemisionCompra.updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int CodGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.delete(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int CodigoDetalle)
	{
		try
		{
			return MGuiaRemisionCompra.deletedetalle(CodigoDetalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable cargarNotasCreditoCompraGeneradas(int CodigoGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.cargarNotasCreditoCompraGeneradas(CodigoGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool deletedetalledegrc(int CodigoGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.deletedetalledegrc(CodigoGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertrelacionguia(int codguia, int codventa, int codalmacen, int codusuario, int codTrans)
	{
		try
		{
			return MGuiaRemisionCompra.insertrelacionguia(codguia, codventa, codalmacen, codusuario, codTrans);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsGuiaRemision CargaGuiaRemision(int CodGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.CargaGuiaRemision(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal string getInfoDocumentoRelacionadoGRC(int codGuiaRemision, int tipoDoc)
	{
		try
		{
			return MGuiaRemisionCompra.getInfoDocumentoRelacionadoGRC(codGuiaRemision, tipoDoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsGuiaRemision CargaGuiaTransferencia(int cod)
	{
		try
		{
			return MGuiaRemisionCompra.CargaGuiaTransferencia(cod);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsGuiaRemision CargaGuiaVenta(int CodVenta)
	{
		try
		{
			return MGuiaRemisionCompra.CargaGuiaVenta(CodVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsGuiaRemision BuscaGuiaRemision(string CodGuiaRemision, int CodAlmacen)
	{
		try
		{
			return MGuiaRemisionCompra.BuscaGuiaRemision(CodGuiaRemision, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsDetalleGuiaRemision> listaDetalleGuiaRemision(string CodGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.listaDetalleGuiaRemision(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsDetalleGuiaRemision> listaDetalleGuiaRemisionventa(string CodGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.listaDetalleGuiaRemision(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int CodGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.CargaDetalle(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleGuiaVenta(int CodGuiaRemision)
	{
		try
		{
			return MGuiaRemisionCompra.CargaDetalleGuiaVenta(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraGuiaRemisiones(int CodAlmacen, int codSucursal, int tipoFecha, DateTime fechaInicio, DateTime fechaFinal, int codProducto)
	{
		try
		{
			return MGuiaRemisionCompra.ListaGuiaRemisiones(CodAlmacen, codSucursal, tipoFecha, fechaInicio, fechaFinal, codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraGuias(DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return MGuiaRemisionCompra.MuestraGuias(fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraGuiasBusqueda(DateTime fecha1, DateTime fecha2, string numdocumento)
	{
		try
		{
			return MGuiaRemisionCompra.MuestraGuiasBusqueda(fecha1, fecha2, numdocumento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaFacturasGuia(int codGuia, int codAlmacen)
	{
		try
		{
			return MGuiaRemisionCompra.CargaFacturasGuia(codGuia, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable estadosAlInicioGeneracion(int iCodAlmacen)
	{
		try
		{
			return MGuiaRemisionCompra.estadosAlInicioGeneracion(iCodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable generaFacturaCompraDeGRC(int codGuiaRemisionCompra, int mostrarFlete)
	{
		try
		{
			return MGuiaRemisionCompra.generaFacturaCompraDeGRC(codGuiaRemisionCompra, mostrarFlete);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable generaFacturaCompraDeGRC_1(int codGuiaRemisionCompra, int mostrarFlete)
	{
		try
		{
			return MGuiaRemisionCompra.generaFacturaCompraDeGRC_1(codGuiaRemisionCompra, mostrarFlete);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable generaFleteDeGRC(int codGuiaRemisionCompra)
	{
		try
		{
			return MGuiaRemisionCompra.generaFacturaFleteDeGRC(codGuiaRemisionCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool setCodFacturaCompra(int codGRC, int codNI)
	{
		try
		{
			return MGuiaRemisionCompra.setCodFacturaCompra(codGRC, codNI);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool insertarDocumentoRelacionado(clsGuiaRemisionCompraDocumentoRelacionado nuevo)
	{
		try
		{
			return MGuiaRemisionCompra.insertarDocumentoRelacionado(nuevo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool updateDocumentoRelacionado(clsGuiaRemisionCompraDocumentoRelacionado nuevo)
	{
		try
		{
			return MGuiaRemisionCompra.updateDocumentoRelacionado(nuevo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal clsGuiaRemisionCompraDocumentoRelacionado cargaDocumentoRelacionado(int codigo)
	{
		try
		{
			return MGuiaRemisionCompra.cargaDocumentoRelacionado(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal clsGuiaRemisionCompraDocumentoRelacionado buscaDocumentoRelacionado(int codDocRel, int tipoDoc)
	{
		try
		{
			return MGuiaRemisionCompra.buscaDocumentoRelacionado(codDocRel, tipoDoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal clsGuiaRemisionCompraDocumentoRelacionado buscaDocumentoRelacionado(int codGRC, int tipoDoc, int anulado)
	{
		try
		{
			return MGuiaRemisionCompra.buscaDocumentoRelacionado(codGRC, tipoDoc, anulado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int getEstadoDocumentoRelacionado(int codGRC, int tipoDoc)
	{
		try
		{
			return MGuiaRemisionCompra.getEstadoDocumentoRelacionado(codGRC, tipoDoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	internal DataTable generaNotaCreditoDeGRC(int codGuiaRemisionCompra)
	{
		try
		{
			return MGuiaRemisionCompra.generaNotaCreditoDeGRC(codGuiaRemisionCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable generaFacturaVentaDeGRC(int codGuiaRemisionCompra)
	{
		try
		{
			return MGuiaRemisionCompra.generaFacturaVentaDeGRC(codGuiaRemisionCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable obtenerListadoProductoFleteDeGRC(int codGuiaRemisionCompra)
	{
		try
		{
			return MGuiaRemisionCompra.obtenerListadoProductoFlete(codGuiaRemisionCompra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int obtenerCodigoFacturacionSegunCodNotaDeIngreso(int codFactura)
	{
		try
		{
			return MGuiaRemisionCompra.obtenerCodigoFacturacionSegunCodNotaDeIngreso(codFactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	internal int getTipoFaturaRelacionadoAGRC(int codNotaIngreso)
	{
		try
		{
			return MGuiaRemisionCompra.obtenerTipoFacturaRelacionadoAGRC(codNotaIngreso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -1;
		}
	}

	internal DataTable getDatosDocRelacGrillaFacturaCompra(int tipo, int codNotaIngreso)
	{
		try
		{
			return MGuiaRemisionCompra.obtenerListadoDocRelacFacturaCompraGRC(tipo, codNotaIngreso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
