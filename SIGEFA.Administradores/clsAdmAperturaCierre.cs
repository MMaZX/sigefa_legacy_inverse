using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmAperturaCierre : IAperturaCierre
{
	private IAperturaCierre Maper = new MysqlAperturaCierreCaja();

	public bool Insert(clsCaja aper)
	{
		try
		{
			return Maper.Insert(aper);
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

	public bool UpdateApertura(clsCaja aper)
	{
		try
		{
			return Maper.UpdateApertura(aper);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool UpdateCierre(clsCaja aper)
	{
		try
		{
			return Maper.UpdateCierre(aper);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool AnularCierre(int codAlmacen)
	{
		try
		{
			return Maper.AnularCierre(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsCaja CargaAperturaCaja(int codAlmacen)
	{
		try
		{
			return Maper.CargaAperturaCaja(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCaja CargaCierreCaja(int codAlmacen)
	{
		try
		{
			return Maper.CargaCierreCaja(codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCaja GetUltimaCajaVentas(int codsucursal, int tipocaja, int codalma)
	{
		try
		{
			return Maper.GetUltimaCajaVentas(codsucursal, tipocaja, codalma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public decimal traersaldo()
	{
		try
		{
			return Maper.traersaldo();
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public bool InsertAperturaCaja(clsCaja caja)
	{
		try
		{
			return Maper.InsertAperturaCaja(caja);
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

	public clsCaja CargaCierreAnterior(int iCodSucursal, int tipocaja)
	{
		try
		{
			return Maper.CargaCierreAnterior(iCodSucursal, tipocaja);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable ListaCierresDiarios(int codSucursal, DateTime desde, DateTime hasta)
	{
		try
		{
			return Maper.ListaCierresDiarios(codSucursal, desde, hasta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public decimal SumaVentaEfectivoCaja(int codSuc, DateTime fech1, int codigocaja)
	{
		try
		{
			return Maper.SumaVentaEfectivoCaja(codSuc, fech1, codigocaja);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public decimal SumaVentasEfectivoCaja(int codSuc, DateTime fechaDesde, DateTime fechaHasta, int codigocaja)
	{
		try
		{
			return Maper.SumaVentasEfectivoCaja(codSuc, fechaDesde, fechaHasta, codigocaja);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0m;
		}
	}

	public DataTable ListaCajaDiaria(int codSucursal, DateTime fecha1, int codigocaja, int codalma, int codEstadoCaja = 1)
	{
		try
		{
			return Maper.ListaCajaDiaria(codSucursal, fecha1, codigocaja, codalma, codEstadoCaja);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool CerrarCajaVentas(int codSucursal, DateTime fecha1, int codcaja, int codalma)
	{
		try
		{
			return Maper.CerrarCajaVentas(codSucursal, fecha1, codcaja, codalma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool InsertMovCajaChica(clsCajaChicaMov movchi)
	{
		try
		{
			return Maper.InsertMovCajaChica(movchi);
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

	public DataTable ListaCajaChica(int codSucursal, DateTime fecha1, int codigocaja, int codalma)
	{
		return Maper.ListaCajaChica(codSucursal, fecha1, codigocaja, codalma);
	}

	public DataTable ConsultaCajas(int almacen, DateTime fecha1, DateTime fecha2)
	{
		return Maper.ConsultaCajas(almacen, fecha1, fecha2);
	}

	public clsCaja ValidarAperturaDia(int codSucursal, DateTime fecha1, int tipocaja, int codalma, int CodUser)
	{
		try
		{
			return Maper.ValidarAperturaDia(codSucursal, fecha1, tipocaja, codalma, CodUser);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCaja ListaTotalesVentas(int codSucursal, DateTime fechaInicio, DateTime fechaFin, int tipocaja, int codalma, int CodUser)
	{
		try
		{
			return Maper.ListaTotalesVentas(codSucursal, fechaInicio, fechaFin, tipocaja, codalma, CodUser);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsCaja GetCaja(int codSucursal, DateTime fecha1, int tipocaja, int codalma)
	{
		try
		{
			return Maper.GetCaja(codSucursal, fecha1, tipocaja, codalma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable getVentasNoEstanEnCajaMovimientos(DateTime fechaCaja, int codAlmacen, int iCodSucursal, int codCaja)
	{
		try
		{
			return Maper.getVentasNoEstanEnCajaMovimientos(fechaCaja, codAlmacen, iCodSucursal, codCaja);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
