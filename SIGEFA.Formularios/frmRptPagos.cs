using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmRptPagos : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvReportePagos;

	private CRReportePagos CRReportePagos1;

	public frmRptPagos()
	{
		InitializeComponent();
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
		this.crvReportePagos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		this.CRReportePagos1 = new SIGEFA.Reportes.CRReportePagos();
		base.SuspendLayout();
		this.crvReportePagos.ActiveViewIndex = -1;
		this.crvReportePagos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvReportePagos.DisplayGroupTree = false;
		this.crvReportePagos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvReportePagos.Location = new System.Drawing.Point(0, 0);
		this.crvReportePagos.Name = "crvReportePagos";
		this.crvReportePagos.SelectionFormula = "";
		this.crvReportePagos.Size = new System.Drawing.Size(629, 438);
		this.crvReportePagos.TabIndex = 0;
		this.crvReportePagos.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(629, 438);
		base.Controls.Add(this.crvReportePagos);
		base.Name = "frmRptPagos";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmRptPagos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
