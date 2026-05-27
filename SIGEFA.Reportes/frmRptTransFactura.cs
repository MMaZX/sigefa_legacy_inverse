using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptTransFactura : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvfrmRptTransFactura;

	public frmRptTransFactura()
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
		this.crvfrmRptTransFactura = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvfrmRptTransFactura.ActiveViewIndex = -1;
		this.crvfrmRptTransFactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvfrmRptTransFactura.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvfrmRptTransFactura.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvfrmRptTransFactura.Location = new System.Drawing.Point(0, 0);
		this.crvfrmRptTransFactura.Name = "crvfrmRptTransFactura";
		this.crvfrmRptTransFactura.SelectionFormula = "";
		this.crvfrmRptTransFactura.Size = new System.Drawing.Size(284, 262);
		this.crvfrmRptTransFactura.TabIndex = 0;
		this.crvfrmRptTransFactura.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvfrmRptTransFactura);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmRptTransFactura";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptTransFactura";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
