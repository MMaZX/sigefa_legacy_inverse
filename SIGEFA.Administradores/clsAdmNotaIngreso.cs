using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

public class clsAdmNotaIngreso
{
	private INotaIngreso Mnota = new MysqlNotaIngreso();

	public bool insert(clsNotaIngreso nota)
	{
		try
		{
			return Mnota.insert(nota);
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

	public bool insertingresoguia(clsNotaIngreso nota)
	{
		try
		{
			return Mnota.insertingresoguia(nota);
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

	public bool insertarNota(clsNotaIngreso nota)
	{
		try
		{
			return Mnota.insertarNota(nota);
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

	public bool insertarNotayFactura(clsNotaIngreso nota, clsFactura factura)
	{
		try
		{
			return Mnota.insertarNotayFactura(nota, factura);
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

	public bool insertdetalleConsolidado(int orden, int Nota, int codAlma, int codUsu)
	{
		try
		{
			return Mnota.insertdetalleConsolidado(orden, Nota, codAlma, codUsu);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deleteConsolidado(int codalma, int codusu)
	{
		try
		{
			return Mnota.deleteConsolidado(codalma, codusu);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertdetalle(clsDetalleNotaIngreso detalle)
	{
		try
		{
			return Mnota.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaCantidadPendiente(double cantidad, int produc, int CodOrden, int coddeta)
	{
		try
		{
			return Mnota.ActualizaCantidadPendiente(cantidad, produc, CodOrden, coddeta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaCantidadPendiente2(double cantidad, int produc, int alma, int coduser)
	{
		try
		{
			return Mnota.ActualizaCantidadPendiente2(cantidad, produc, alma, coduser);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaCodNotaIngreso(double cantidad, int produc, int CodDetalle, int tipo)
	{
		try
		{
			return Mnota.ActualizaCodNotaIngreso(cantidad, produc, CodDetalle, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsNotaIngreso nota)
	{
		try
		{
			return Mnota.update(nota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleNotaIngreso detalle)
	{
		try
		{
			return Mnota.updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int Codnota)
	{
		try
		{
			return Mnota.delete(Codnota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular(int CodSerie, string NumDoc, string text)
	{
		try
		{
			return Mnota.anular(CodSerie, NumDoc, text);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular1(int codigo)
	{
		try
		{
			return Mnota.anular1(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool activar(int Codnota)
	{
		try
		{
			return Mnota.activar(Codnota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int Codnota)
	{
		try
		{
			return Mnota.deletedetalle(Codnota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsNotaIngreso CargaNotaIngreso(int CodNotaIngreso)
	{
		try
		{
			return Mnota.CargaNotaIngreso(CodNotaIngreso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsNotaSalida buscaNotaIngreso(int CodNotaIngreso)
	{
		try
		{
			return Mnota.buscaNotaIngreso(CodNotaIngreso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int CodNotaIngreso)
	{
		try
		{
			return Mnota.CargaDetalle(CodNotaIngreso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleSinEstado(int CodNotaIngreso)
	{
		try
		{
			return Mnota.CargaDetalleSinEstado(CodNotaIngreso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraOrdenAlmacen(int codAlmacen, int codUsu)
	{
		try
		{
			return Mnota.MuestraOrdenAlmacen(codAlmacen, codUsu);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraNotaIngresoOrden(int codAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return Mnota.MuestraNotaIngresoOrden(codAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraNotasIngreso(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int tipoFecha)
	{
		try
		{
			return Mnota.ListaNotasIngreso(Criterio, CodAlmacen, FechaInicial, FechaFinal, tipoFecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable reporteinventario(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return Mnota.reporteinventario(CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable busquedapornumero(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, string numero)
	{
		try
		{
			return Mnota.busquedapornumero(CodAlmacen, FechaInicial, FechaFinal, numero);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraNotasIngresoxProducto(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal, int codprod, int tipoFecha)
	{
		try
		{
			return Mnota.ListaNotasIngresoxProducto(Criterio, CodAlmacen, FechaInicial, FechaFinal, codprod, tipoFecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraPagos(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2)
	{
		try
		{
			return Mnota.MuestraPagos(Estado, codEmpresa, Fecha1, Fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int obtenerCodNCsegun(string codNotaIngreso)
	{
		try
		{
			return Mnota.obtenerCodNCsegun(codNotaIngreso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return Mnota.ListaNotasCredito(CodAlmacen, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTransferenciasVigentes(int CodAlmacen)
	{
		try
		{
			return Mnota.MuestraTransferenciasVigentes(CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool atender(int codigo, int CodSerie, string NumDoc, int User)
	{
		try
		{
			return Mnota.atender(codigo, CodSerie, NumDoc, User);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable CargaDetalleTransferencia(int codigotransferencia)
	{
		try
		{
			return Mnota.CargaDetalleTransferencia(codigotransferencia);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool UpdateComentario(clsNotaIngreso nota)
	{
		try
		{
			return Mnota.UpdateComentario(nota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraGuia(int codAlmacen, int codUsu)
	{
		try
		{
			return Mnota.MuestraGuia(codAlmacen, codUsu);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaNotaCreditoSinAplicar(int Codcli, int VentComp, int codAlmacen, string fecha)
	{
		try
		{
			return Mnota.CargaNotaCreditoSinAplicar(Codcli, VentComp, codAlmacen, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ActualizaNCreditoVentaSinAplicar(clsNotaIngreso nota)
	{
		try
		{
			return Mnota.ActualizaNCreditoVentaSinAplicar(nota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool VerificarNCVentaAplicada(clsNotaIngreso nota)
	{
		try
		{
			return Mnota.VerificarNCVentaAplicada(nota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable CargaNotaIngresoSD(int Codprov, int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return Mnota.CargaNotaIngresoSD(Codprov, CodAlmacen, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListarCodigoNotasSalida()
	{
		try
		{
			return Mnota.ListarCodigoNotasSalida();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ActualizaStockPA(int codalmacenorig, int codalmacenrecep, int codigoProd, int codigoNI, decimal cantidadenviada, decimal preciounit, decimal valorreal, decimal valorrealsoles, int codigouser, string serie, string numerodoc, int codserie)
	{
		try
		{
			return Mnota.ActualizaStockPA(codalmacenorig, codalmacenrecep, codigoProd, codigoNI, cantidadenviada, preciounit, valorreal, valorrealsoles, codigouser, serie, numerodoc, codserie);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaStockAR(int codalmacenorig, int codalmacenrecep, int codigoProd, int codigoNI, decimal cantidadenviada, decimal preciounit, decimal valorreal, decimal valorrealsoles, int codigouser, string serie, string numerodoc, int codserie)
	{
		try
		{
			return Mnota.ActualizaStockAR(codalmacenorig, codalmacenrecep, codigoProd, codigoNI, cantidadenviada, preciounit, valorreal, valorrealsoles, codigouser, serie, numerodoc, codserie);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsNotaIngreso CargaNI(int codTransDirecta)
	{
		try
		{
			return Mnota.CargaNI(codTransDirecta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ValidarCompraNotaIngreso(int codigoTipoDocumento, string serieDocumento, string numeroDocumento, int codigoProveedor)
	{
		try
		{
			return Mnota.ValidarCompraNotaIngreso(codigoTipoDocumento, serieDocumento, numeroDocumento, codigoProveedor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
