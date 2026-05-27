using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmDespacho
{
	private MysqlDespacho MDesp = new MysqlDespacho();

	public bool insert(clsDespacho despacho)
	{
		try
		{
			return MDesp.insert(despacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable ListadoReporteEntrega(DateTime desde, DateTime hasta, string codAnalisis, string codAlmacen)
	{
		try
		{
			return MDesp.ListadoReporteEntrega(desde, hasta, codAnalisis, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertDetalle(clsDetalleDespacho detalle_despacho)
	{
		try
		{
			return MDesp.insertDetalle(detalle_despacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool update(clsDespacho despacho)
	{
		try
		{
			return MDesp.update(despacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataSet ExportarListadoReporteEntrega(DateTime desde, DateTime hasta, string codAnalisis, string codAlmacen)
	{
		try
		{
			return MDesp.ExportarListadoReporteEntrega(desde, hasta, codAnalisis, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool updateDetalle(clsDetalleDespacho detalle_despacho)
	{
		try
		{
			return MDesp.updateDetalle(detalle_despacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable listaDetalleEntrega(int codEntrega)
	{
		try
		{
			return MDesp.listaDetalleEntrega(codEntrega);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool anularEntrega(int codEntrega, int codUsuario, DateTime now)
	{
		try
		{
			return MDesp.anularEntrega(codEntrega, codUsuario, now);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal clsEntrega cargaEntrega(int codEntrega)
	{
		try
		{
			return MDesp.CargaEntrega(codEntrega);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable listaEntregas(int codDespacho)
	{
		try
		{
			return MDesp.listaEntregas(codDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsDespacho cargaDespacho(int codDespacho)
	{
		try
		{
			return MDesp.CargaDespacho(codDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable VerificaRequerimientoAnularVenta(int codalmacen, int codDocRelacionado)
	{
		try
		{
			return MDesp.VerificaRequerimientoAnularVenta(codalmacen, codDocRelacionado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool registrarEntrega(clsEntrega entrega)
	{
		try
		{
			return MDesp.insertEntrega(entrega);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal int obtenerCodEstado(int codDespacho)
	{
		try
		{
			return MDesp.obtenerCodEstado(codDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -1;
		}
	}

	public bool insertDetalleEntrega(clsDetalleEntrega detalle_entrega)
	{
		try
		{
			return MDesp.insertDetalleEntrega(detalle_entrega);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable listaDespacho(int codAlmacen, int codSucursal, int tipoFecha, DateTime desde, DateTime hasta, int codCliente, int tipoListado, int tipoEstado)
	{
		try
		{
			return MDesp.ListaDespacho(codAlmacen, codSucursal, tipoFecha, desde, hasta, codCliente, tipoListado, tipoEstado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable generarDatosParaFormularioIntermedioTransferencia(int codDespacho, int codAlmacen = 0, int codSucursal = 0)
	{
		try
		{
			return MDesp.generarDatosParaFormularioIntermedio(codDespacho, codAlmacen, codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsDetalleDespacho> ListaDetalleDespacho(int codDespacho)
	{
		try
		{
			return MDesp.CargaListadoDetalleDespacho(codDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool actualizaCantidadPendienteDespacho(int codDespacho)
	{
		try
		{
			return MDesp.actualizaCantidadPendienteDespacho(codDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaDetalleDespacho2(int codDespacho, int codAlmacen = 0)
	{
		try
		{
			return MDesp.CargaListadoDetalleDespacho2(codDespacho, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal clsDespacho cargaDespachoSegunDocRelacionado(int tipoDocRelacionado, string codDocRelacionado)
	{
		try
		{
			return MDesp.CargaDespachoSegunDocRelacionado(tipoDocRelacionado, codDocRelacionado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool anular(clsDespacho despacho)
	{
		try
		{
			return MDesp.anular(despacho.CodDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal int VerificaEntregasActivasRespectoADespacho(int tipoDocRelacionado, string codDocRelacionado)
	{
		try
		{
			return MDesp.VerificaEntregasActivasRespectoADespacho(tipoDocRelacionado, codDocRelacionado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -1;
		}
	}

	internal int VerificaEntregasActivasDeDespacho(int codDespacho)
	{
		try
		{
			return MDesp.VerificaEntregasActivasDeDespacho(codDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -1;
		}
	}

	internal bool cambioEstado(int codDespacho, int codEstado)
	{
		try
		{
			return MDesp.cambioEstado(codDespacho, codEstado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataSet ReporteImprimirDespacho(int codDespacho, int codEmpresa)
	{
		try
		{
			return MDesp.reporteImprimirDatosDespacho(codDespacho, codEmpresa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataSet ReporteImprimirEntrega(int codEntrega)
	{
		try
		{
			return MDesp.reporteImprimirDatosEntrega(codEntrega);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable ListaDetalleDespacho3(int idTablaDocRelacionado, int codFacturaVenta, int codAlmacen = 0)
	{
		try
		{
			return MDesp.CargaListadoDetalleDespacho3(idTablaDocRelacionado, codFacturaVenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal int ObtenerDatoAntesDeListarDetalleDespacho3(int idTablaDocRelacionado, int codFacturaVenta)
	{
		try
		{
			return MDesp.GetDatoListadoDetalleDespachoSegun(idTablaDocRelacionado, codFacturaVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -1;
		}
	}

	internal DataSet ReporteResumenEntregas(int codDespacho)
	{
		try
		{
			return MDesp.reporteResumenEntregas(codDespacho);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable DetalleParaVerificarRetornoDeProductos(int codDespacho, int iCodAlmacen)
	{
		try
		{
			return MDesp.DetalleParaVerificarRetornoDeProductos(codDespacho, iCodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool tieneEntregasActivas(int codDespacho)
	{
		try
		{
			DataTable entregas = MDesp.listaEntregas(codDespacho);
			foreach (DataRow fila in entregas.Rows)
			{
				if (fila.Field<object>("anulado") == "0")
				{
					return true;
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message + "\nSe devolvio false en el metodo tieneEntregasActivas()", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
