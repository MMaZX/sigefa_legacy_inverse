using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmNotaSalida
{
	private INotaSalida Mnota = new MysqlNotaSalida();

	public bool insert(clsNotaSalida nota)
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

	public bool insertdetalle(clsDetalleNotaSalida detalle)
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

	public bool insertdetalle2(clsDetalleNotaSalida detalle)
	{
		try
		{
			return Mnota.insertdetalle2(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsNotaSalida nota)
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

	public bool updatedetalle(clsDetalleNotaSalida detalle)
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

	public bool ActualizaCantidadPendienteCotizacion(double cantidad, int produc, int CodCoti)
	{
		try
		{
			return Mnota.ActualizaCantidadPendienteCotizacion(cantidad, produc, CodCoti);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaCantidadPendienteVenta(double cantidad, int produc, int CodVen)
	{
		try
		{
			return Mnota.ActualizaCantidadPendienteVenta(cantidad, produc, CodVen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaCotizacionAprobada(int codCotizacion)
	{
		try
		{
			return Mnota.ActualizaCotizacionAprobada(codCotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaCotizacionVigente(int codCotizacion, int codDetalleCotizacion)
	{
		try
		{
			return Mnota.ActualizaCotizacionVigente(codCotizacion, codDetalleCotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular(int Codnota)
	{
		try
		{
			return Mnota.anular(Codnota);
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

	public bool insertadetalleventaSalida(int codVenta, int codCoti, int codSalida)
	{
		try
		{
			return Mnota.insertadetalleventaSalida(codVenta, codCoti, codSalida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalleventaSalida()
	{
		try
		{
			return Mnota.deletedetalleventaSalida();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsNotaSalida CargaNotaSalida(int CodNotaSalida)
	{
		try
		{
			return Mnota.CargaNotaSalida(CodNotaSalida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int CodNotaSalida)
	{
		try
		{
			return Mnota.CargaDetalle(CodNotaSalida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleNotaCredito(int CodNotaSalida)
	{
		try
		{
			return Mnota.CargaDetalleNotaCredito(CodNotaSalida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraNotasSalida(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return Mnota.ListaNotasSalida(Criterio, CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraCobros(int Estado, int codAlmacen, DateTime Fecha1, DateTime Fecha2, int codTipo)
	{
		try
		{
			return Mnota.MuestraCobros(Estado, codAlmacen, Fecha1, Fecha2, codTipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable DocumentosSinGuia(int CodAlmacen, int CodCliente, int Tipo)
	{
		try
		{
			return Mnota.DocumentosSinGuia(CodAlmacen, CodCliente, Tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable DocumentosPorCliente(int CodCliente, int tipo)
	{
		try
		{
			return Mnota.DocumentosPorCliente(CodCliente, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable Ventas(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return Mnota.Ventas(CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraFacturasVentaCliente(int CodAlmacen, int CodSucursal, int CodEmpresa)
	{
		try
		{
			return Mnota.MuestraFacturasVentaCliente(CodAlmacen, CodSucursal, CodEmpresa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraVentaSalida(int CodAlmacen, int codVenta)
	{
		try
		{
			return Mnota.MuestraVentaSalida(CodAlmacen, codVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraCotizacionSalida(int CodAlmacen, int codCotizacion)
	{
		try
		{
			return Mnota.MuestraCotizacionSalida(CodAlmacen, codCotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTipoDocumentoNota(int CodAlmacen, int codCliente)
	{
		try
		{
			return Mnota.MuestraTipoDocumentoNota(CodAlmacen, codCliente);
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
			return Mnota.deletedetalleventaSalida();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsNotaSalida CargaNotaSalidaCredito(int CodNota)
	{
		try
		{
			return Mnota.CargaNotaSalidaCredito(CodNota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaNotasCreditoCompra(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return Mnota.ListaNotasCreditoCompra(CodAlmacen, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraNotaAlmacen(int codAlmacen, int tipo)
	{
		try
		{
			return Mnota.MuestraNotaAlmacen(codAlmacen, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleNotaSalida(int CodNotaSalida, int proceso)
	{
		try
		{
			return Mnota.CargaDetalleNotaSalida(CodNotaSalida, proceso);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsNotaSalida CargaNotaSalidaCreditoVentas(int CodNota)
	{
		try
		{
			return Mnota.CargaNotaSalidaCreditoVentas(CodNota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsNotaSalida CargaNotaSalidaDebitoVentas(int CodNotaSalida)
	{
		try
		{
			return Mnota.CargaNotaSalidaDebitoVentas(CodNotaSalida);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool VerificarNCCompraAplicada(clsNotaSalida nota)
	{
		try
		{
			return Mnota.VerificarNCCompraAplicada(nota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListarNotaSalidaParaNDCompra(int codAlm, int codProv)
	{
		try
		{
			return Mnota.ListarNotaSalidaParaNDCompra(codAlm, codProv);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleNotaSalidaNDC(int codNota, int codAlm)
	{
		try
		{
			return Mnota.CargaDetalleNotaSalidaNDC(codNota, codAlm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ActualizaNCreditoCompraSinAplicar(clsNotaSalida nota)
	{
		try
		{
			return Mnota.ActualizaNCreditoCompraSinAplicar(nota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizaSalidaDevolucion(int codnota, int codfact)
	{
		try
		{
			return Mnota.ActualizaSalidaDevolucion(codnota, codfact);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public decimal VerificarStock(int codpro, int codalma, int opc)
	{
		try
		{
			return Mnota.VerificarStock(codpro, codalma, opc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public clsNotaSalida CargaNS(int codTransDirecta)
	{
		try
		{
			return Mnota.CargaNS(codTransDirecta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
