using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmReporteVentasDiarias : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvReporteVentasDiarias;

	private CRVentasDiarias CRVentasDiarias1;

	public frmReporteVentasDiarias()
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
		this.crvReporteVentasDiarias = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		this.CRVentasDiarias1 = new SIGEFA.Reportes.CRVentasDiarias();
		base.SuspendLayout();
		this.crvReporteVentasDiarias.ActiveViewIndex = 0;
		this.crvReporteVentasDiarias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvReporteVentasDiarias.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvReporteVentasDiarias.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvReporteVentasDiarias.Location = new System.Drawing.Point(0, 0);
		this.crvReporteVentasDiarias.Name = "crvReporteVentasDiarias";
		this.crvReporteVentasDiarias.ReportSource = this.CRVentasDiarias1;
		this.crvReporteVentasDiarias.Size = new System.Drawing.Size(1239, 589);
		this.crvReporteVentasDiarias.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1239, 589);
		base.Controls.Add(this.crvReporteVentasDiarias);
		base.Name = "frmReporteVentasDiarias";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmReporteVentasDiarias";
		base.ResumeLayout(false);
	}
}
