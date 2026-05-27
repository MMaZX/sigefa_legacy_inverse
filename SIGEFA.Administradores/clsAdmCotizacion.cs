using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmCotizacion
{
	private ICotizacion Mcotizacion = new MysqlCotizacion();

	public bool insert(clsCotizacion cotizacion)
	{
		try
		{
			return Mcotizacion.insert(cotizacion);
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

	public bool insertdetalle(clsDetalleCotizacion detalle)
	{
		try
		{
			return Mcotizacion.insertdetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool update(clsCotizacion cotizacion)
	{
		try
		{
			return Mcotizacion.update(cotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleCotizacion detalle)
	{
		try
		{
			return Mcotizacion.updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updateAprobado(int CodCotizacion)
	{
		try
		{
			return Mcotizacion.updateAprobado(CodCotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ValidarDescuento(int CodCotizacion)
	{
		try
		{
			return Mcotizacion.ValidarDescuento(CodCotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int CodigoCotizacion, int codusuario, string mensaje)
	{
		try
		{
			return Mcotizacion.delete(CodigoCotizacion, codusuario, mensaje);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool deletedetalle(int Codcotizacion)
	{
		try
		{
			return Mcotizacion.deletedetalle(Codcotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsCotizacion CargaCotizacion(int CodCotizacion, int CodAlmacen)
	{
		try
		{
			return Mcotizacion.CargaCotizacion(CodCotizacion, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCotizacion BuscaCotizacion(string CodCotizacion, int CodAlmacen)
	{
		try
		{
			return Mcotizacion.BuscaCotizacion(CodCotizacion, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsDetalleCotizacion CargaDetalleCotizacion(int CodCotizacion, int CodAlmacen)
	{
		try
		{
			return Mcotizacion.CargaDetalleCotizacion(CodCotizacion, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalle(int CodCotizacion, int CodAlmacen)
	{
		try
		{
			return Mcotizacion.CargaDetalle(CodCotizacion, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDatosRecalcular(int Codproducto, int Codunidad, int CodAlmacen)
	{
		try
		{
			return Mcotizacion.CargaDatosRecalcular(Codproducto, Codunidad, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaDetalleVenta(int CodCotizacion, int CodAlmacen)
	{
		try
		{
			return Mcotizacion.CargaDetalleVenta(CodCotizacion, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool DuplicarCotizacion(int CodCotizacion)
	{
		try
		{
			return Mcotizacion.DuplicarCotizacion(CodCotizacion);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraCotizaciones(int CodAlmacen)
	{
		try
		{
			return Mcotizacion.ListaCotizaciones(CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraCotizacionesxVigente(int CodAlmacen, int estado, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			return Mcotizacion.ListaCotizacionesxVigente(CodAlmacen, estado, fecha1, fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool CotizacionesVencidas()
	{
		try
		{
			return Mcotizacion.CotizacionesVencidas();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}
}
