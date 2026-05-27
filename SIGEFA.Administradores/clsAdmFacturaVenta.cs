using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmFacturaVenta
{
	private IFacturaVenta Mventa = new MysqlFacturaVenta();

	public bool insert(clsFacturaVenta venta)
	{
		try
		{
			return Mventa.insert(venta);
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

	public bool insertComprobante(clsFacturaVenta venta)
	{
		try
		{
			return Mventa.insertComprobante(venta);
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

	public bool rollback(int codventa, int tipo)
	{
		try
		{
			return Mventa.rollbackfactura(codventa, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertdetalle(clsDetalleFacturaVenta detalle)
	{
		try
		{
			return Mventa.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsFacturaVenta venta)
	{
		try
		{
			return Mventa.update(venta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable ReporteAnalisisDetalladoVenta(string buscar, DateTime desde, DateTime hasta, string codAnalisis, string codAlmacen)
	{
		try
		{
			return Mventa.ReporteAnalisisDetalladoVenta(buscar, desde, hasta, codAnalisis, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool updateCobroVenta(clsFacturaVenta venta)
	{
		try
		{
			return Mventa.updateCobroVenta(venta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleFacturaVenta detalle)
	{
		try
		{
			return Mventa.updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int codventa)
	{
		try
		{
			return Mventa.delete(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal double getTotalNotaCreditoSegunFechayAlmacen(int codAlmacen, DateTime desde, DateTime hasta, int codSucursal)
	{
		try
		{
			return Mventa.getTotalNotaCreditoSegunFechayAlmacen(codAlmacen, desde, hasta, codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return double.NaN;
		}
	}

	public bool anular(int codventa, int codUsuario = 0)
	{
		try
		{
			return Mventa.anular(codventa, codUsuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool AnulaDetalleVenta(int coddetalleventa, int codproducto)
	{
		try
		{
			return Mventa.AnulaDetalleVenta(coddetalleventa, codproducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool activar(int codventa)
	{
		try
		{
			return Mventa.activar(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int codventa)
	{
		try
		{
			return Mventa.deletedetalle(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaEstadoImpreso(int codventa)
	{
		try
		{
			return Mventa.actualizaEstadoImpreso(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsFacturaVenta FechaCorrelativoAnterior(int codserie)
	{
		try
		{
			return Mventa.fechaCorrelativoAnterior(codserie);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal clsFacturaVenta CargaFacturaVentaSegunOV(int codPedido)
	{
		try
		{
			return Mventa.CargaFacturaVentaSegunOV(codPedido);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsFacturaVenta CargaFacturaVenta(int codventa)
	{
		try
		{
			return Mventa.CargaFacturaVenta(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsFacturaVenta CargaFacturaVenta_Regeneracion(int codventa)
	{
		try
		{
			return Mventa.CargaFacturaVentaRegeneracion(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleNotaSalida(int codventa, int codAlmacen)
	{
		try
		{
			return Mventa.CargaDetalleNotaSalida(codventa, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool UpdateKardex(int codNota, int codProd, int Codalma, decimal Cant, decimal valProm)
	{
		try
		{
			return Mventa.UpdateKardex(codNota, codProd, Codalma, Cant, valProm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsNotaIngreso BuscaNotaSalida(int codVenta, int codAlmacen)
	{
		try
		{
			return Mventa.BuscaNotaSalida(codVenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsFacturaVenta BuscaFacturaVenta(int codVenta, int codAlmacen)
	{
		try
		{
			return Mventa.BuscaFacturaVenta(codVenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaFacturaVenta(int codalmacen)
	{
		try
		{
			return Mventa.ListaFacturaVenta(codalmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int codventa, int codAlmacen, int guia)
	{
		try
		{
			return Mventa.CargaDetalleVenta(codventa, codAlmacen, guia);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleCodventa(clsFacturaVenta codventa)
	{
		try
		{
			return Mventa.CargaDetalleVentaCodventa(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleCodventaxLineaFamilia(clsFacturaVenta codventa)
	{
		try
		{
			return Mventa.CargaDetalleCodventaxLineaFamilia(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle_Regeneracion(int codventa, int codAlmacen)
	{
		try
		{
			return Mventa.CargaDetalle_Regeneracion(codventa, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraCobrosVenta(int Estado, int codAlmacen, DateTime Fecha1, DateTime Fecha2, int codTipo, int codSucursal, int codFormaPago)
	{
		try
		{
			return Mventa.MuestraCobros(Estado, codAlmacen, Fecha1, Fecha2, codTipo, codSucursal, codFormaPago);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public string MuestraFechaPrimerCobro(int Estado, int codAlmacen, int codTipo, int codSucursal, int codFormaPago)
	{
		try
		{
			return Mventa.MuestraFechaPrimerCobro(Estado, codAlmacen, codTipo, codSucursal, codFormaPago);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable DocumentosPorCliente(int CodCliente)
	{
		try
		{
			return Mventa.DocumentosPorCliente(CodCliente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable Ventas(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int codsucursal, int verifica, int codProducto = 0)
	{
		try
		{
			return Mventa.Ventas(CodAlmacen, FechaInicial, FechaFinal, codsucursal, verifica, codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable Ventasboletas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin, int codsucursal, int codtipdoc)
	{
		try
		{
			return Mventa.Ventasboletas(CodAlmacen, FechaInicio, FechaFin, codsucursal, codtipdoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable VentasCodlineaCodfamilia(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int codsucursal)
	{
		try
		{
			return Mventa.VentasCodlineaCodfamilia(CodAlmacen, FechaInicial, FechaFinal, codsucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertdetalleventasalida(int codVen, int codSalida)
	{
		try
		{
			return Mventa.insertdetalleventasalida(codVen, codSalida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraGuiaVenta(int CodAlmacen, int CodCliente)
	{
		try
		{
			return Mventa.MuestraGuiaVenta(CodAlmacen, CodCliente);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraDetalleGuiaVenta(int CodAlmacen, int codNota)
	{
		try
		{
			return Mventa.MuestraDetalleGuiaVenta(CodAlmacen, codNota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraDetalleGuiaVenta2(int CodAlmacen)
	{
		try
		{
			return Mventa.MuestraDetalleGuiaVenta2(CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool deletedetalleventasalida()
	{
		try
		{
			return Mventa.deletedetalleventasalida();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraDetalleGuia(int CodAlmacen, int codnota)
	{
		try
		{
			return Mventa.MuestraDetalleGuia(CodAlmacen, codnota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraDetalleVentaGuia(int codventa, int codalmacen)
	{
		try
		{
			return Mventa.MuestraDetalleVentaGuia(codventa, codalmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool VistaSucursal(int codventa, int valor)
	{
		try
		{
			return Mventa.VistaSucursal(codventa, valor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable CargaDetalleVentaCredito(int codventa, int codAlmacen)
	{
		try
		{
			return Mventa.CargaDetalleVentaCredito(codventa, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ActualizaPendienteCredito(decimal monto, int codnota, int codalm, int tipo)
	{
		try
		{
			return Mventa.ActualizaPendienteCredito(monto, codnota, codalm, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaNotasDebito(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return Mventa.ListaNotasDebito(CodAlmacen, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int chekeaImpresion(int codVenta)
	{
		try
		{
			return Mventa.chekeaImpresion(codVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public bool actualizaFactura_venta(int CodSerie, string txtSeries, string txtNumeros, string CodVenta)
	{
		try
		{
			return Mventa.actualizaFactura_venta(CodSerie, txtSeries, txtNumeros, CodVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaFacturas_ventaCliente(int codcli)
	{
		try
		{
			return Mventa.ListaFacturas_ventaCliente(codcli);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool updatensconsultext(clsFacturaVenta venta)
	{
		try
		{
			return Mventa.updatensconsultext(venta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable VentasDiarias(int CodAlmacen, DateTime FechaInicial, int CodVendedor)
	{
		try
		{
			return Mventa.VentasDiarias(CodVendedor, CodAlmacen, FechaInicial);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable VentasPendientesdedespacho(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return Mventa.VentasPendientesdedespacho(CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetallexEntregar(int codventa, int codAlmacen)
	{
		try
		{
			return Mventa.CargaDetallexEntregar(codventa, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int GetCantidadPendiente(int lista)
	{
		try
		{
			return Mventa.GetCantidadPendiente(lista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public bool CambiaEstadoDetalle(int codigo)
	{
		try
		{
			return Mventa.CambiaEstadoDetalle(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool CambiaEstadoFactura(int CodVenta)
	{
		try
		{
			return Mventa.CambiaEstadoFactura(CodVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable despachosxventa(int Codfactura)
	{
		try
		{
			return Mventa.despachosxventa(Codfactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertventaentregar(clsFacturaVenta venta)
	{
		try
		{
			return Mventa.insertventaentregar(venta);
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

	public bool insertdetalleventaentregar(clsDetalleFacturaVenta detalle)
	{
		try
		{
			return Mventa.insertdetalleventaentregar(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool VentaPendiente(int CodVenta)
	{
		try
		{
			return Mventa.VentaPendiente(CodVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizaEstadoEnvio(int codigo, int codVenta)
	{
		try
		{
			return Mventa.actualizaEstadoEnvio(codigo, codVenta);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizaEstadoEnvioConError(int codigo, int codVenta)
	{
		try
		{
			return Mventa.actualizaEstadoEnvioConError(codigo, codVenta);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaBoletaSunat(int codventa)
	{
		try
		{
			return Mventa.ActualizaBoletaSunat(codventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ValidaAnulacionVenta(int codigoventa)
	{
		try
		{
			return Mventa.ValidaAnulacionVenta(codigoventa);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ReporteVentasResumido(DateTime desde, DateTime hasta)
	{
		try
		{
			return Mventa.ReporteVentasResumido(desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool VerificaEstadoEnvioDocumentoElectronico(int codigoEmpresa, int codigoSucursal, int codigoAlmacen, int codigoFacturaVenta)
	{
		try
		{
			return Mventa.VerificaEstadoEnvioDocumentoElectronico(codigoEmpresa, codigoSucursal, codigoAlmacen, codigoFacturaVenta);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaStockSinFacturar(clsDetalleFacturaVenta det, int unibas, decimal factor, int engresoegreso)
	{
		try
		{
			return Mventa.ActualizaStockSinFacturar(det, unibas, factor, engresoegreso);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool InsertaProductoSinFacturar(clsDetalleFacturaVenta det, decimal vps, decimal stock, decimal soles, int unibas)
	{
		try
		{
			return Mventa.InsertaProductoSinFacturar(det, vps, stock, soles, unibas);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable VentaSinRepositorio(int alma, DateTime fechaini, DateTime fechafin, int tipdoc)
	{
		try
		{
			return Mventa.VentaSinRepositorio(alma, fechaini, fechafin, tipdoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ResumenDiario(DateTime dia, int codSucursal, int codAlmacen)
	{
		try
		{
			return Mventa.ResumenDiario(dia, codSucursal, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int getCantidadResumen(DateTime dia, int codSucursal, int codAlmacen)
	{
		try
		{
			return Mventa.getCantidadResumen(dia, codSucursal, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public bool actualizaCanalVenta(string codigo, int codVenta)
	{
		try
		{
			return Mventa.actualizaCanalVenta(codigo, codVenta);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualizacuentabanco(int codbanco, int codcuenta, string numcuenta, int codventa)
	{
		try
		{
			return Mventa.actualizacuentabanco(codbanco, codcuenta, numcuenta, codventa);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
