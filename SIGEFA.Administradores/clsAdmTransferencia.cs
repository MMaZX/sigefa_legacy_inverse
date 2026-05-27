using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmTransferencia
{
	private ITransferencia MTrans = new MysqlTransferencia();

	public bool insert(clsTransferencia transf)
	{
		try
		{
			return MTrans.insert(transf);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insert2(clsTransferencia transf)
	{
		try
		{
			return MTrans.insert2(transf);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsTransferencia transf)
	{
		try
		{
			return MTrans.update(transf);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int Codtrans)
	{
		try
		{
			return MTrans.delete(Codtrans);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsTransferencia CargaTransferencia(int Codtrans)
	{
		try
		{
			return MTrans.CargaTransferencia(Codtrans);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsTransferencia CargaTransferenciaCodPedido(int codpedido)
	{
		try
		{
			return MTrans.CargaTransferenciaCodPedido(codpedido);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsTransferencia CargaDetalleTransferencia(int Codtrans)
	{
		try
		{
			return MTrans.CargaDetalleTransferencia(Codtrans);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsTransferencia CargaTransferenciaCod()
	{
		try
		{
			return MTrans.CargaTransferenciaCod();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsTransferencia BuscaTransferencia(string Codtrans, int CodAlmacenOrigen)
	{
		try
		{
			return MTrans.BuscaTransferencia(Codtrans, CodAlmacenOrigen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTranferencias(int caso, int user, int CodAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			return MTrans.ListaTranferencias(caso, user, CodAlmacen, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTranferencias2(int caso, int user, int CodAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			return MTrans.ListaTranferencias2(caso, user, CodAlmacen, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTranferenciasDesp(int codtrans)
	{
		try
		{
			return MTrans.ListaTranferenciasDesp(codtrans);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTranferenciaEntrega(DateTime fechaE, int codigotra)
	{
		try
		{
			return MTrans.ListaTranferenciasEntrega(fechaE, codigotra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTranferenciasEnviadas(int caso, int user, int CodAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			return MTrans.ListaTransferenciasEnviados(caso, user, CodAlmacen, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTranferenciasPendiente(int CodAlmacen, int CodPedido)
	{
		try
		{
			return MTrans.ListaTranferenciasPendientes(CodAlmacen, CodPedido);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable listatodastranferencias(int caso, int user, int CodAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			return MTrans.listatodastranferencias(caso, user, CodAlmacen, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraTranferenciasxProducto(int caso, int user, int CodAlmacen, DateTime desde, DateTime hasta, int codprod)
	{
		try
		{
			return MTrans.ListaTranferenciasxProducto(caso, user, CodAlmacen, desde, hasta, codprod);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool insertdetalle(clsDetalleTransferencia detalle)
	{
		try
		{
			return MTrans.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool insertdetalle2(clsDetalleTransferencia detalle)
	{
		try
		{
			return MTrans.insertdetalle2(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleTransferencia detalle)
	{
		try
		{
			return MTrans.updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle2(clsDetalleTransferencia detalle)
	{
		try
		{
			return MTrans.updatedetalle2(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int Coddeta)
	{
		try
		{
			return MTrans.deletedetalle(Coddeta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable CargaDetallePedido(int codTransDir)
	{
		try
		{
			return MTrans.CargaDetallePedido(codTransDir);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int codTransDir)
	{
		try
		{
			return MTrans.CargaDetalle(codTransDir);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleTrans(clsTransferencia tra)
	{
		try
		{
			return MTrans.CargaDetalleTrans(tra);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool rechazado(int codTransDirecta, string desc)
	{
		try
		{
			return MTrans.rechazado(codTransDirecta, desc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool rechazad2(int codTransDirecta, string anulado)
	{
		try
		{
			return MTrans.rechazad2(codTransDirecta, anulado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool devuelveproductos(clsDetalleTransferencia det)
	{
		try
		{
			return MTrans.devuelveproductos(det);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Aprobar(int codTransDirecta)
	{
		try
		{
			return MTrans.Aprobar(codTransDirecta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool TransFactura(int codPedido)
	{
		try
		{
			return MTrans.TransFactura(codPedido);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Entregar(int codTransDirecta)
	{
		try
		{
			return MTrans.Entregar(codTransDirecta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable CargaDetalleGuiaT(string CodigoTransferencia)
	{
		try
		{
			return MTrans.CargaDetalleGuiaT(CodigoTransferencia);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool atendido(int CodTransDirecta, int codUsuario)
	{
		try
		{
			return MTrans.atendido(CodTransDirecta, codUsuario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
