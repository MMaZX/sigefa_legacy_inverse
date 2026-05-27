using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmSerie
{
	private ISerie Mser = new MysqlSerie();

	public bool insert(clsSerie ser)
	{
		try
		{
			return Mser.Insert(ser);
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

	public bool update(clsSerie ser)
	{
		try
		{
			return Mser.Update(ser);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int Codser)
	{
		try
		{
			return Mser.Delete(Codser);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public DataTable MuestraSeries(int codDoc, int codAlmacen)
	{
		try
		{
			return Mser.ListaSeries(codDoc, codAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsSerie BuscaSerie(string Serie, int Documento, int Almacen)
	{
		try
		{
			return Mser.BuscaSerie(Serie, Documento, Almacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsSerie BuscaSeriexDocumento(int Documento, int Almacen)
	{
		try
		{
			return Mser.BuscaSeriexDocumento(Documento, Almacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsSerie MuestraSerie(int CodSerie, int CodAlmacen)
	{
		try
		{
			return Mser.CargaSerie(CodSerie, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public int ExisteSeries(int CodDocumento, int CodAlmacen)
	{
		try
		{
			return Mser.ExistenSeries(CodDocumento, CodAlmacen);
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
			return -1;
		}
	}

	public int GetCodigoSerie(int CodDocumento, int CodAlmacen)
	{
		try
		{
			return Mser.GetCodigoSerie(CodDocumento, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return -1;
		}
	}

	public int traeNumeracion(int codal, int coddoc)
	{
		try
		{
			return Mser.traeNumeracion(codal, coddoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public int traeCodSerie(int codal, int coddoc)
	{
		try
		{
			return Mser.traeCodSerie(codal, coddoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 0;
		}
	}

	public clsSerie CargaSerieEmpresa(int CodigoE, int codigoDoc)
	{
		try
		{
			return Mser.CargaSerieEmpresa(CodigoE, codigoDoc);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsSerie CargaSerieOV(int Sucursal, int CodAlmacen)
	{
		try
		{
			return Mser.CargaSerieOV(Sucursal, CodAlmacen);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsSerie MuestraSeriePorDocumentoAsociado(int CodDocumento, int CodAlmacen, int CodDocumentoAsociado)
	{
		try
		{
			return Mser.CargaSeriePorDocumentoAsociado(CodDocumento, CodAlmacen, CodDocumentoAsociado);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
