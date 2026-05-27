using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmCtaCte
{
	private ICtaCte Mcta = new MysqlCtaCte();

	public bool Insert(clsCtaCte cta)
	{
		try
		{
			return Mcta.Insert(cta);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public bool Update(clsCtaCte cta)
	{
		try
		{
			return Mcta.Update(cta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool Delete(int CodCtaCte, int codAlmacen)
	{
		try
		{
			return Mcta.Delete(CodCtaCte, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable ListaCtasBanco(int CodBanco, int codAlmacen)
	{
		try
		{
			return Mcta.ListaCtasBanco(CodBanco, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaCtaCte(int codAlmacen)
	{
		try
		{
			return Mcta.ListaCtaCte(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool InsertMovi(clsCtaCte cta)
	{
		try
		{
			return Mcta.InsertMovi(cta);
		}
		catch (Exception ex)
		{
			if (ex.Message.Contains("Duplicate entry"))
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
	}

	public DataTable ListaMovimientos(int codAlmacen)
	{
		try
		{
			return Mcta.ListaMovimientos(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListarMovientoscta(int codAlmacen, int codBanco, int codCuenta)
	{
		try
		{
			return Mcta.ListarMovientoscta(codAlmacen, codBanco, codCuenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaMovimientosDesactivos(int codbanco, int codcuenta, int codAlmacen)
	{
		try
		{
			return Mcta.ListaMovimientosDesactivos(codbanco, codcuenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaEgresosCaja(int CodSucursal, DateTime fecha)
	{
		try
		{
			return Mcta.ListaEgresosCaja(CodSucursal, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public decimal TotalConciliacion(int codAlmacen, int codBanco, int codCuenta)
	{
		try
		{
			return Mcta.TotalConciliacion(codAlmacen, codBanco, codCuenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public clsCtaCte CargaTipoCuenta(int CodCuenta, int codAlmacen)
	{
		try
		{
			return Mcta.CargaTipoCuenta(CodCuenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCtaCte BuscaMovimiento(int CodCuenta, int codAlmacen)
	{
		try
		{
			return Mcta.BuscaMovimiento(CodCuenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool UpdateMovi(clsCtaCte cta)
	{
		try
		{
			return Mcta.UpdateMovi(cta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool DeleteMov(int CodMov, int codAlmacen)
	{
		try
		{
			return Mcta.DeleteMov(CodMov, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable CargarMovxCuenta(string Cuenta, int codAlmacen)
	{
		try
		{
			return Mcta.CargarMovxCuenta(Cuenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListatipoCtas_x_Banco(int CodBanco, int codAlmacen)
	{
		try
		{
			return Mcta.ListatipoCtas_x_Banco(CodBanco, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListanumCta_x_tipocta(int CodBanco, string tipocuenta, int codAlmacen)
	{
		try
		{
			return Mcta.ListanumCta_x_tipocta(CodBanco, tipocuenta, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaCaja(int codSucursal, DateTime fecha)
	{
		try
		{
			return Mcta.ListaCaja(codSucursal, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCtaCte VerificaEgresoCaja(int CodSucursal, DateTime fecha)
	{
		try
		{
			return Mcta.VerificaEgresoCaja(CodSucursal, fecha);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaCtaCtexBancoxMoneda(int codBanco, int codMoneda)
	{
		try
		{
			return Mcta.ListaCtaCtexBancoxMoneda(codBanco, codMoneda);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaBancoxMoneda(int codMoneda)
	{
		try
		{
			return Mcta.ListaBancoxMoneda(codMoneda);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int Correlativo(int codtipo)
	{
		try
		{
			return Mcta.Correlativo(codtipo);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return 0;
		}
	}

	public bool activar(int codtipo)
	{
		try
		{
			return Mcta.activar(codtipo);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	public bool desactivar(int codigo)
	{
		try
		{
			return Mcta.desactivar(codigo);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}
}
