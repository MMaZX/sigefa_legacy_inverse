using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmListaPrecio
{
	private IListaPrecio Mlista = new MysqlListaPrecio();

	public bool insert(clsListaPrecio lista)
	{
		try
		{
			return Mlista.Insert(lista);
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

	public bool update(clsListaPrecio lista)
	{
		try
		{
			return Mlista.Update(lista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetalle(clsDetalleListaPrecio detalle)
	{
		try
		{
			return Mlista.Updatedetalle(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool updatedetallePorFiltro(clsDetalleListaPrecio detalle)
	{
		try
		{
			return Mlista.updatedetallePorFiltro(detalle);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool delete(int CodLista)
	{
		try
		{
			return Mlista.Delete(CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool anular(int codSucursal, int CodLista)
	{
		try
		{
			return Mlista.Anular(codSucursal, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public bool activar(int codSucursal, int CodLista)
	{
		try
		{
			return Mlista.Activar(codSucursal, CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
	}

	public clsListaPrecio CargaListaPrecio(int CodListaPrecio)
	{
		try
		{
			return Mlista.CargaListaPrecio(CodListaPrecio);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListas(int codSucursal)
	{
		try
		{
			return Mlista.MuestraListas(codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable PreciosProducto(int codProducto, int codSucursal, int codalma)
	{
		try
		{
			return Mlista.MuestraPreciosProducto(codProducto, codSucursal, codalma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable CargaListaPrecios(int CodLista)
	{
		try
		{
			return Mlista.CargaListaPrecios(CodLista);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public bool GeneraLista(int lista, int codalma, int decimales)
	{
		try
		{
			return Mlista.GeneraPreciosLista(lista, codalma, decimales);
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

	public bool GeneraListaProveedor(int lista, int codSucursal, int decimales, int codProveedor)
	{
		try
		{
			return Mlista.GeneraPreciosListaProveedor(lista, codSucursal, decimales, codProveedor);
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

	public List<int> MuestraListasProveedor(int codSucursal)
	{
		try
		{
			return Mlista.MuestraListasProveedor(codSucursal);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorFiltro(int codSucursal, int rango1, int rango2, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListasPorFiltro(codSucursal, rango1, rango2, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorProveedor(int codSucursal, int codProv, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorProveedor(codSucursal, codProv, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorFamilia(int codSucursal, int codFam, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorFamilia(codSucursal, codFam, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorLinea(int codSucursal, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorLinea(codSucursal, codFam, codLin, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorRangoProv(int codSucursal, int rango1, int rango2, int codProv, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorRangoProv(codSucursal, rango1, rango2, codProv, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorRangoFam(int codSucursal, int rango1, int rango2, int codFam, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorRangoFam(codSucursal, rango1, rango2, codFam, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorProveedorFam(int codSucursal, int codProv, int codFam, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorProveedorFam(codSucursal, codProv, codFam, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorRangoFamLin(int codSucursal, int rango1, int rango2, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorRangoFamLin(codSucursal, rango1, rango2, codFam, codLin, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorProveedorFamLin(int codSucursal, int codProv, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorProveedorFamLin(codSucursal, codProv, codFam, codLin, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasParcial(int codSucursal, int rango1, int rango2, int codProv, int codFam, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaParcial(codSucursal, rango1, rango2, codProv, codFam, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListasPorTodos(int codSucursal, int rango1, int rango2, int codProv, int codFam, int codLin, int listaorigen, int decimales)
	{
		try
		{
			return Mlista.MuestraListaPorTodos(codSucursal, rango1, rango2, codProv, codFam, codLin, listaorigen, decimales);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public DataTable MuestraListaPrecioxFormaPago(int codSucursal, int codForma)
	{
		try
		{
			return Mlista.MuestraListaPrecioxFormaPago(codSucursal, codForma);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
