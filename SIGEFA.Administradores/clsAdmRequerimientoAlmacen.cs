using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmRequerimientoAlmacen
{
	private MysqlRequerimientoAlmacen MReqAlmacen = new MysqlRequerimientoAlmacen();

	public bool insert(clsRequerimientoAlmacen Requerimiento, List<clsDetalleRequerimientoAlmacen> detalle)
	{
		using TransactionScope Scope = new TransactionScope();
		try
		{
			bool rpta = MReqAlmacen.insert(Requerimiento);
			if (rpta)
			{
				foreach (clsDetalleRequerimientoAlmacen deta in detalle)
				{
					deta.CodRequerimiento = Requerimiento.Codigo;
					rpta = MReqAlmacen.insertdetalle(deta);
					if (!rpta)
					{
						break;
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
			}
			else
			{
				Scope.Complete();
				Scope.Dispose();
			}
			return rpta;
		}
		catch (Exception ex)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
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

	internal DataSet ListadoConsumoRequerimiento(int tipoFecha, DateTime desde, DateTime hasta)
	{
		try
		{
			return MReqAlmacen.ListadoConsumoRequerimiento(tipoFecha, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable CargaRequerimientosSegunPedido(int codPedido)
	{
		try
		{
			return MReqAlmacen.CargaRequerimientosSegunPedido(codPedido);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool update(clsRequerimientoAlmacen Requerimiento, List<clsDetalleRequerimientoAlmacen> detalleNew, List<clsDetalleRequerimientoAlmacen> detalleOld)
	{
		using TransactionScope Scope = new TransactionScope();
		try
		{
			bool rpta = MReqAlmacen.update(Requerimiento);
			if (rpta)
			{
				rpta = MReqAlmacen.deletedetalleRequerimiento(Requerimiento.Codigo);
				if (rpta)
				{
					foreach (clsDetalleRequerimientoAlmacen deta in detalleNew)
					{
						deta.CodRequerimiento = Requerimiento.Codigo;
						rpta = MReqAlmacen.insertdetalle(deta);
						if (!rpta)
						{
							break;
						}
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
			}
			else
			{
				Scope.Complete();
				Scope.Dispose();
			}
			return rpta;
		}
		catch (Exception ex)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
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

	public bool anular(int CodigoRequerimiento, int codUsuario)
	{
		try
		{
			return MReqAlmacen.anular(CodigoRequerimiento, codUsuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable listadoTransferenciasGeneradas(int codigoReq, int codAlmacen)
	{
		try
		{
			return MReqAlmacen.ListadoTransferenciasGeneradas(codigoReq, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool aprobar(int CodigoRequerimiento, int codUsuario)
	{
		try
		{
			return MReqAlmacen.aprobar(CodigoRequerimiento, codUsuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int CodigoDetalleRequerimiento)
	{
		try
		{
			return MReqAlmacen.deletedetalle(CodigoDetalleRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsRequerimientoAlmacen CargaRequerimiento(int codReqAlmacen)
	{
		try
		{
			return MReqAlmacen.CargaRequerimiento(codReqAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public List<clsDetalleRequerimientoAlmacen> CargaDetalleRequerimientoAlmacen(int codRequerimiento)
	{
		try
		{
			return MReqAlmacen.CargaDetalleRequerimientoAlmacen(codRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListadoRequerimientos(int CodAlmacen, int codSucursal, int tipoFecha, DateTime FechaInicial, DateTime FechaFinal, int codProducto, int tipoListado = 0)
	{
		try
		{
			return MReqAlmacen.ListadoRequerimientoAlmacen(CodAlmacen, codSucursal, tipoFecha, FechaInicial, FechaFinal, codProducto, tipoListado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListadoParaControlDeRequerimiento(int CodAlmacen, int codSucursal, int tipoFecha, DateTime FechaInicial, DateTime FechaFinal, int codProducto, int tipoEstado)
	{
		try
		{
			return MReqAlmacen.ListadoParaControlDeRequerimiento(CodAlmacen, codSucursal, tipoFecha, FechaInicial, FechaFinal, codProducto, tipoEstado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaDetalleRequerimiento(int codRequerimiento)
	{
		try
		{
			return MReqAlmacen.ListaDetalleRequerimientoAlmacen(codRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool registrarTransferencia(int CodigoRequerimiento, int codTransferencia, int codUsuario)
	{
		try
		{
			return MReqAlmacen.registrarTransferencia(CodigoRequerimiento, codTransferencia, codUsuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataTable generarDatosParaFormularioIntermedioTransferencia(int codReqAlm)
	{
		try
		{
			return MReqAlmacen.generarDatosParaFormularioIntermedioTransferencia(codReqAlm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable cargaTransferenciasAprobadas(int codReqAlm)
	{
		try
		{
			return MReqAlmacen.ListaTransferenciasAprobadas(codReqAlm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool actualizaCantidadPendienteReqAlmacen(int codigo)
	{
		try
		{
			return MReqAlmacen.actualizaCantidadPendienteReqAlmacen(codigo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool actualizaEstadoReqAlmacen(int codigo, int estado)
	{
		try
		{
			return MReqAlmacen.actualizaEstadoReqAlmacen(codigo, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool retornarStock(int codAlmacenDespacho, int codProducto, int codUnidad, decimal cantidadPendienteAprobada, bool modificarStockActual = true, int codDetalleReqAlmacen = 0)
	{
		try
		{
			return MReqAlmacen.retornarStock(codAlmacenDespacho, codProducto, codUnidad, cantidadPendienteAprobada, modificarStockActual, codDetalleReqAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool separarStock(int codAlmacenDespacho, int codProducto, int codUnidad, decimal cantidadConfirmada, int codDetalleReqAlm = 0)
	{
		try
		{
			return MReqAlmacen.separarStock(codAlmacenDespacho, codProducto, codUnidad, cantidadConfirmada, codDetalleReqAlm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal DataSet ReporteImprimirReqAlm(int codRequerimiento)
	{
		try
		{
			return MReqAlmacen.reporteImprimirDatosRequerimiento(codRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal DataTable cargaTransferenciasPendientes(int codReq)
	{
		try
		{
			return MReqAlmacen.ListaTransferenciasPendientes(codReq);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal clsRequerimientoAlmacen CargaRequerimientosSegun(int codPedidoVenta, int codAlmacenVenta, int estadoReq)
	{
		try
		{
			return MReqAlmacen.CargaRequerimientosSegun(codPedidoVenta, codAlmacenVenta, estadoReq);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool ModificarCtdadPendienteAprobadaDeDetalleReqAlmacen(int operacion, double cantidadVenta, int codigoDetalleReq)
	{
		try
		{
			return MReqAlmacen.ModificarCtdadPendienteAprobadaDeDetalleReqAlmacen(operacion, cantidadVenta, codigoDetalleReq);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool asignarAutorizador(int codReqAlm, int idAutorizador)
	{
		try
		{
			return MReqAlmacen.asignarAutorizador(codReqAlm, idAutorizador);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool asignarFacturaVenta(int codReqAlm, int codVentaFV)
	{
		try
		{
			return MReqAlmacen.asignarFacturaVenta(codReqAlm, codVentaFV);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal clsRequerimientoAlmacen CargaRequerimientosSegun(int codFacturaVenta)
	{
		try
		{
			return MReqAlmacen.CargaRequerimientosSegun(codFacturaVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool actualizaCantidadPendienteAprobadaReqAlmacen(int codReqAlm, bool movimientostock = true)
	{
		try
		{
			return MReqAlmacen.actualizaCantidadPendienteAprobadaReqAlmacen(codReqAlm, movimientostock);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal bool ModificarCantidadConfirmada(int idDetalle, double nuevaCtdadConfirmacion)
	{
		try
		{
			return MReqAlmacen.ModificarCantidadConfirmada(idDetalle, nuevaCtdadConfirmacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	internal double getCantidadProductoTransferenciasActivas(int codigoReq, int codProducto)
	{
		try
		{
			return MReqAlmacen.getCantidadProductoTransferenciasActivas(codigoReq, codProducto);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return double.NaN;
		}
	}

	internal bool cerrar(int CodigoRequerimiento, int codUsuario)
	{
		try
		{
			return MReqAlmacen.cerrar(CodigoRequerimiento, codUsuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
