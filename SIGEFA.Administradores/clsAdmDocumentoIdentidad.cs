using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmDocumentoIdentidad
{
	private IDocumentoIdentidad MDocumentoIdentidad = new MysqlDocumentoIdentidad();

	public DataTable ListaDocumentoIdentidad(int codigoTipoDocumento)
	{
		try
		{
			return MDocumentoIdentidad.ListaDocumentoIdentidad(codigoTipoDocumento);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsDocumentoIdentidad MuestraDocumentoIdentidad(int codigoDocumentoIdentidad)
	{
		try
		{
			return MDocumentoIdentidad.MuestraDocumentoIdentidad(codigoDocumentoIdentidad);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}

	public clsDocumentoIdentidad ObtenerDocumentoIdentidadDeVenta(int codigoFacturaVenta)
	{
		try
		{
			return MDocumentoIdentidad.ObtenerDocumentoIdentidadDeVenta(codigoFacturaVenta);
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
	}
}
