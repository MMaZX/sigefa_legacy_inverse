using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmPago
{
	private IPago Mpag = new MysqlPago();

	public bool insert(clsPago pag)
	{
		try
		{
			return Mpag.Insert(pag);
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

	public bool insertPagoPendiente(clsPago pag)
	{
		try
		{
			return Mpag.InsertPagoPendiente(pag);
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

	public bool insertpagomultiple(clsPago pag)
	{
		try
		{
			return Mpag.Insert(pag);
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

	public int obtenerOpcionDeSumaCaja(int codigo)
	{
		try
		{
			return Mpag.obtenerOpcionSuma(codigo);
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
			return 0;
		}
	}

	public bool insertpagodetraccion(clsPago pag)
	{
		try
		{
			return Mpag.InsertPagoDetraccion(pag);
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

	public DataTable MuestraListaPagosPorNota(int CodNotaIngreso, bool InOut, int Tipo, int codAlmacen)
	{
		try
		{
			return Mpag.MuestraListaPagosNota(CodNotaIngreso, InOut, Tipo, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool AnularPago(int Codpago)
	{
		try
		{
			return Mpag.AnularPago(Codpago);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool AnularPagoPendiente(int Codpago)
	{
		try
		{
			return Mpag.AnularPagoPendiente(Codpago);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool ActualizarSeleccion(int Codpago, string Color)
	{
		try
		{
			return Mpag.ActualizarSeleccion(Codpago, Color);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsPago MuestraPagoVenta(int codAlmacen, int venta)
	{
		try
		{
			return Mpag.MuestraPagoVenta(codAlmacen, venta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraPagosDetraccion(int Estado, DateTime Fecha1, DateTime Fecha2)
	{
		try
		{
			return Mpag.MuestraPagosDetraccion(Estado, Fecha1, Fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraPagosPorAprobar(int Estado, DateTime Fecha1, DateTime Fecha2)
	{
		try
		{
			return Mpag.MuestraPagosPorAprobar(Estado, Fecha1, Fecha2);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraPagoVentaAnula(int codAlmacen, int nota)
	{
		try
		{
			return Mpag.MuestraPagoVentaAnula(codAlmacen, nota);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool AprobarPago(int codigo, int valor)
	{
		try
		{
			return Mpag.AprobarPago(codigo, valor);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraListaPagosPorNota2(int CodNotaIngreso, bool InOut, int Tipo)
	{
		try
		{
			return Mpag.MuestraListaPagosNota2(CodNotaIngreso, InOut, Tipo);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool ActualizaPagoAprobado(string ser, string numdoc, int codpag)
	{
		try
		{
			return Mpag.ActualizaPagoAprobado(ser, numdoc, codpag);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable GetPagosVenta(int codigoalmacen, int codigoventa)
	{
		try
		{
			return Mpag.GetPagosVenta(codigoalmacen, codigoventa);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
