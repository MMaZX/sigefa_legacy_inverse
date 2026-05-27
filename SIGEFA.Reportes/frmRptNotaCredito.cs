using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptNotaCredito : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvNotaCredito;

	public frmRptNotaCredito()
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
		this.crvNotaCredito = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvNotaCredito.ActiveViewIndex = -1;
		this.crvNotaCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvNotaCredito.DisplayGroupTree = false;
		this.crvNotaCredito.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvNotaCredito.Location = new System.Drawing.Point(0, 0);
		this.crvNotaCredito.Name = "crvNotaCredito";
		this.crvNotaCredito.SelectionFormula = "";
		this.crvNotaCredito.Size = new System.Drawing.Size(284, 262);
		this.crvNotaCredito.TabIndex = 0;
		this.crvNotaCredito.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvNotaCredito);
		base.Name = "frmRptNotaCredito";
		this.Text = "frmRptNotaCredito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
