using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmRequerimiento
{
	private IRequerimiento MOrden = new MysqlRequerimiento();

	public bool insert(clsRequerimiento Requerimiento)
	{
		try
		{
			return MOrden.insert(Requerimiento);
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

	public bool insertdetalle(clsDetalleRequerimiento detalle)
	{
		try
		{
			return MOrden.insertdetalleRequerimiento(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsRequerimiento Requerimiento)
	{
		try
		{
			return MOrden.update(Requerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleRequerimiento detalle)
	{
		try
		{
			return MOrden.updatedetalleRequerimiento(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int CodigoRequerimiento)
	{
		try
		{
			return MOrden.delete(CodigoRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Envio(int codAlmaDestino, int codreq)
	{
		try
		{
			return MOrden.envio(codAlmaDestino, codreq);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Atender(int codreq)
	{
		try
		{
			return MOrden.Atender(codreq);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool rechazado(int CodigoRequerimiento, string comentario)
	{
		try
		{
			return MOrden.rechazado(CodigoRequerimiento, comentario);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular(int CodigoRequerimiento)
	{
		try
		{
			return MOrden.anular(CodigoRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anularDetalle(int CodigoDetalleRequerimiento)
	{
		try
		{
			return MOrden.anularDetalle(CodigoDetalleRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int CodigoRequerimiento)
	{
		try
		{
			return MOrden.deletedetalle(CodigoRequerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsRequerimiento CargaRequerimiento(int CodOrden)
	{
		try
		{
			return MOrden.CargaRequerimiento(CodOrden);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable Cargaconsolidado(int codalma, int codprov)
	{
		try
		{
			return MOrden.Cargaconsolidado(codalma, codprov);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable cargaRequerimientosTotales(int alma, int almaori)
	{
		try
		{
			return MOrden.cargaRequerimientosTotales(alma, almaori);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int codReq)
	{
		try
		{
			return MOrden.CargaDetalle(codReq);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraRequerimiento(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return MOrden.ListaRequerimiento(CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraRequerimientoHistorial(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			return MOrden.ListaRequerimientoHistorial(CodAlmacen, FechaInicial, FechaFinal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool actualiza_requerimientos_vigentes(clsRequerimiento requerimiento)
	{
		try
		{
			return MOrden.actualiza_requerimientos_vigentes(requerimiento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualiza_det_requerimientos_vigentes(clsDetalleRequerimiento detalle)
	{
		try
		{
			return MOrden.actualiza_det_requerimientos_vigentes(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool actualiza_det_requerimientos_comentario(int coddeta, string coment)
	{
		try
		{
			return MOrden.actualiza_det_requerimientos_comentario(coddeta, coment);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable cargaRequerimientosTotales_x_requerimiento(int codrequerimiento_ex)
	{
		try
		{
			return MOrden.cargaRequerimientosTotales_x_requerimiento(codrequerimiento_ex);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaRequerimientoHistorial_x_almacen(int CodAlmacen, int almades, DateTime FechaInicial, DateTime FechaFinal, int tipo)
	{
		try
		{
			return MOrden.ListaRequerimientoHistorial_x_almacen(CodAlmacen, almades, FechaInicial, FechaFinal, tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
