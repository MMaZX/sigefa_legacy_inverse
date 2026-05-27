using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptFactura : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvReporteFactura;

	public frmRptFactura()
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
		this.crvReporteFactura = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvReporteFactura.ActiveViewIndex = -1;
		this.crvReporteFactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvReporteFactura.DisplayGroupTree = false;
		this.crvReporteFactura.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvReporteFactura.Location = new System.Drawing.Point(0, 0);
		this.crvReporteFactura.Name = "crvReporteFactura";
		this.crvReporteFactura.SelectionFormula = "";
		this.crvReporteFactura.Size = new System.Drawing.Size(284, 262);
		this.crvReporteFactura.TabIndex = 0;
		this.crvReporteFactura.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvReporteFactura);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmRptFactura";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptFactura";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
