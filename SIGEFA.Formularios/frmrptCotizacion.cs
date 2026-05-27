using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmrptCotizacion : Form
{
	public int CodCotizacion;

	public bool afectacion;

	private DataSet data = null;

	public int tipo = 0;

	public int formato = 0;

	private IContainer components = null;

	private CrystalReportViewer crystalReportViewer1;

	public frmrptCotizacion()
	{
		InitializeComponent();
	}

	private void frmrptCotizacion_Load(object sender, EventArgs e)
	{
		if (tipo == 1)
		{
			generareporte();
		}
		else if (tipo == 2)
		{
			generareporteRequerimiento();
		}
		else if (tipo == 3)
		{
			generareporteOrden();
		}
		else if (tipo == 4)
		{
			generareporteSucursal();
		}
		else if (tipo == 5)
		{
			generareporteLinea();
		}
		else if (tipo == 6)
		{
			generareporteGrupo();
		}
		else if (tipo == 7)
		{
			generareportemetodopago();
		}
		else if (tipo == 8)
		{
			generareporteListaPrecio();
		}
		else if (tipo == 9)
		{
			generareporteVehiculoTransporte();
		}
		else if (tipo == 10)
		{
			generareporteconductores();
		}
		else if (tipo == 11)
		{
			generareporteEmpresaTransporte();
		}
		else if (tipo == 12)
		{
			generareportezonas();
		}
		else if (tipo == 13)
		{
			generareporteVendedores();
		}
		else if (tipo == 14)
		{
			generareporteDestaques();
		}
		else if (tipo == 15)
		{
			generareporteTarjetaPago();
		}
		else if (tipo == 16)
		{
			generareporteTransferenciaxDevoluc();
		}
		else if (tipo == 17)
		{
			generareporteRendiciones();
		}
		else if (tipo == 18)
		{
			generareporteCotizacionCompra();
		}
	}

	private void generareporteRendiciones()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Rendiciones(CodCotizacion);
			CRRendiciones myDataReport = new CRRendiciones();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteTransferenciaxDevoluc()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.TransferenciaxDevolucion(CodCotizacion);
			CRTransferenciaxDevolucion myDataReport = new CRTransferenciaxDevolucion();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteTarjetaPago()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.TarjetaPago();
			CRTarjetadePago myDataReport = new CRTarjetadePago();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteDestaques()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Destaques();
			CRDestaque myDataReport = new CRDestaque();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteVendedores()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Vendedores();
			CRVendedores myDataReport = new CRVendedores();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareportezonas()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Zonas();
			CRZonas myDataReport = new CRZonas();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteEmpresaTransporte()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.EmpresaTransporte();
			CREmpresaTransporte myDataReport = new CREmpresaTransporte();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteconductores()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Conductores();
			CRConductores myDataReport = new CRConductores();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteVehiculoTransporte()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.VehiculoTransporte();
			CRVehiculoTransporte myDataReport = new CRVehiculoTransporte();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteListaPrecio()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.ListaPrecios();
			CRListaPrecios myDataReport = new CRListaPrecios();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareportemetodopago()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.MetodoPago();
			CRMetodoPago myDataReport = new CRMetodoPago();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteGrupo()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Grupos(CodCotizacion);
			CRGrupos myDataReport = new CRGrupos();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteLinea()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Lineas(CodCotizacion);
			CRLineas myDataReport = new CRLineas();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteSucursal()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Sucursal();
			CRSucursales myDataReport = new CRSucursales();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporte()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Cotizacion(CodCotizacion, afectacion);
			if (formato == 0)
			{
				CRCotizacion myDataReport = new CRCotizacion();
				myDataReport.SetDataSource(data.Tables[0].DefaultView);
				crystalReportViewer1.ReportSource = myDataReport;
			}
			else
			{
				CRCotizacionSimple myDataReport2 = new CRCotizacionSimple();
				myDataReport2.SetDataSource(data.Tables[0].DefaultView);
				crystalReportViewer1.ReportSource = myDataReport2;
			}
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteRequerimiento()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Requerimiento(CodCotizacion);
			CRRequerimiento myDataReport = new CRRequerimiento();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteOrden()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Orden(CodCotizacion);
			CROrdenCompra myDataReport = new CROrdenCompra();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generareporteCotizacionCompra()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.Orden(CodCotizacion);
			CRCotizacionCompra myDataReport = new CRCotizacionCompra();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crystalReportViewer1.ActiveViewIndex = -1;
		this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crystalReportViewer1.DisplayGroupTree = false;
		this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
		this.crystalReportViewer1.Name = "crystalReportViewer1";
		this.crystalReportViewer1.SelectionFormula = "";
		this.crystalReportViewer1.Size = new System.Drawing.Size(784, 482);
		this.crystalReportViewer1.TabIndex = 0;
		this.crystalReportViewer1.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(784, 482);
		base.Controls.Add(this.crystalReportViewer1);
		base.Name = "frmrptCotizacion";
		this.Text = "Reportes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmrptCotizacion_Load);
		base.ResumeLayout(false);
	}
}
