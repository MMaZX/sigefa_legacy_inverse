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

public class frmFormaPagoRP : Form
{
	public DataSet data = null;

	private IContainer components = null;

	private CrystalReportViewer cRVFormaPago;

	public frmFormaPagoRP()
	{
		InitializeComponent();
	}

	private void frmFormaPagoRP_Load(object sender, EventArgs e)
	{
		generareporte();
	}

	private void generareporte()
	{
		clsDocumentosImpresos doc = new clsDocumentosImpresos();
		try
		{
			data = doc.FormaPago();
			CRFormaPago myDataReport = new CRFormaPago();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			cRVFormaPago.ReportSource = myDataReport;
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
		this.cRVFormaPago = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVFormaPago.ActiveViewIndex = -1;
		this.cRVFormaPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVFormaPago.DisplayGroupTree = false;
		this.cRVFormaPago.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVFormaPago.Location = new System.Drawing.Point(0, 0);
		this.cRVFormaPago.Name = "cRVFormaPago";
		this.cRVFormaPago.SelectionFormula = "";
		this.cRVFormaPago.Size = new System.Drawing.Size(607, 319);
		this.cRVFormaPago.TabIndex = 0;
		this.cRVFormaPago.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(607, 319);
		base.Controls.Add(this.cRVFormaPago);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmFormaPagoRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmFormaPagoRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmFormaPagoRP_Load);
		base.ResumeLayout(false);
	}
}
