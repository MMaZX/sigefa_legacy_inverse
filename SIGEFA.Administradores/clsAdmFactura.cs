using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmFactura
{
	private IFactura Mfactura = new MysqlFactura();

	public bool insert(clsFactura factura)
	{
		try
		{
			return Mfactura.insert(factura);
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

	public bool insertdetalle(clsDetalleFactura detalle)
	{
		try
		{
			return Mfactura.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsFactura factura)
	{
		try
		{
			return Mfactura.update(factura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleFactura detalle)
	{
		try
		{
			return Mfactura.updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(string Codfactura)
	{
		try
		{
			return Mfactura.delete(Codfactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular(int Codfactura)
	{
		try
		{
			return Mfactura.anular(Codfactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular_1(int Codfactura, int Codusu)
	{
		try
		{
			return Mfactura.anular_1(Codfactura, Codusu);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool activar(string Codfactura)
	{
		try
		{
			return Mfactura.activar(Codfactura);
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
			return Mfactura.deletedetalle(CodigoDetalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsFactura CargaFactura(int CodFactura)
	{
		try
		{
			return Mfactura.CargaFactura(CodFactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int CodFactura)
	{
		try
		{
			return Mfactura.CargaDetalle(CodFactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraFactura(int tipoFecha, DateTime FechaInicial, DateTime FechaFinal, int alma)
	{
		try
		{
			return Mfactura.ListaFactura(tipoFecha, FechaInicial, FechaFinal, alma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraFacturaCombo(DateTime FechaInicial, DateTime FechaFinal, int alma, int estado)
	{
		try
		{
			return Mfactura.ListaFacturaCombo(FechaInicial, FechaFinal, alma, estado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraFacturaxProducto(DateTime FechaInicial, DateTime FechaFinal, int alma, int codprod)
	{
		try
		{
			return Mfactura.ListaFacturaxProducto(FechaInicial, FechaFinal, alma, codprod);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraFactura_Facturacion(DateTime FechaInicial, DateTime FechaFinal, int alma)
	{
		try
		{
			return Mfactura.ListaFactura_Facturacion(FechaInicial, FechaFinal, alma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraFactura(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return Mfactura.ListaFactura(Criterio, CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraPagosFactura(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2, int tipoBusqueda)
	{
		try
		{
			return Mfactura.MuestraPagos(Estado, codEmpresa, Fecha1, Fecha2, tipoBusqueda);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return Mfactura.ListaNotasCredito(CodAlmacen, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraFacturasProveedor(int alma, int codpro, int tipo)
	{
		try
		{
			return Mfactura.MuestraFacturasProveedor(alma, codpro, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ActualizaPendienteCreditoCompra(double monto, int codcompra, int codalm)
	{
		try
		{
			return Mfactura.ActualizaPendienteCreditoCompra(monto, codcompra, codalm);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaNotasDebitoCompra(int CodAlmacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return Mfactura.ListaNotasDebitoCompra(CodAlmacen, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public string guiasPorFactura(int codfactura)
	{
		try
		{
			return Mfactura.guiasPorFactura(codfactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	internal bool verificaFleteEnFactura(int codFactura)
	{
		try
		{
			return Mfactura.verificaFleteFactura(codFactura);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
