using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptDespachoArticulo : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvDespacho;

	public frmRptDespachoArticulo()
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
		this.crvDespacho = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvDespacho.ActiveViewIndex = -1;
		this.crvDespacho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvDespacho.DisplayGroupTree = false;
		this.crvDespacho.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvDespacho.Location = new System.Drawing.Point(0, 0);
		this.crvDespacho.Name = "crvDespacho";
		this.crvDespacho.SelectionFormula = "";
		this.crvDespacho.Size = new System.Drawing.Size(284, 262);
		this.crvDespacho.TabIndex = 0;
		this.crvDespacho.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvDespacho);
		base.Name = "frmRptDespachoArticulo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptDespachoArticulo";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
