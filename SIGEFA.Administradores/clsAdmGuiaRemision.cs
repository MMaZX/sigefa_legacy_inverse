using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmGuiaRemision
{
	private IGuiaRemision MGuiaRemision = new MysqlGuiaRemision();

	public bool insert(clsGuiaRemision GuiaRemision)
	{
		try
		{
			return MGuiaRemision.insert(GuiaRemision);
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

	public bool insertdetalle(clsDetalleGuiaRemision detalle)
	{
		try
		{
			return MGuiaRemision.insertdetalle(detalle);
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
			return MGuiaRemision.update(GuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleGuiaRemision detalle)
	{
		try
		{
			return MGuiaRemision.updatedetalle(detalle);
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
			return MGuiaRemision.delete(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int CodGuiaRemision)
	{
		try
		{
			return MGuiaRemision.deletedetalle(CodGuiaRemision);
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
			return MGuiaRemision.insertrelacionguia(codguia, codventa, codalmacen, codusuario, codTrans);
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
			return MGuiaRemision.CargaGuiaRemision(CodGuiaRemision);
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
			return MGuiaRemision.CargaGuiaTransferencia(cod);
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
			return MGuiaRemision.CargaGuiaVenta(CodVenta);
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
			return MGuiaRemision.BuscaGuiaRemision(CodGuiaRemision, CodAlmacen);
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
			return MGuiaRemision.listaDetalleGuiaRemision(CodGuiaRemision);
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
			return MGuiaRemision.listaDetalleGuiaRemision(CodGuiaRemision);
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
			return MGuiaRemision.CargaDetalle(CodGuiaRemision);
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
			return MGuiaRemision.CargaDetalleGuiaVenta(CodGuiaRemision);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraGuiaRemisiones(int CodAlmacen)
	{
		try
		{
			return MGuiaRemision.ListaGuiaRemisiones(CodAlmacen);
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
			return MGuiaRemision.MuestraGuias(fecha1, fecha2);
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
			return MGuiaRemision.MuestraGuiasBusqueda(fecha1, fecha2, numdocumento);
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
			return MGuiaRemision.CargaFacturasGuia(codGuia, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
