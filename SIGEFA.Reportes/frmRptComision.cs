using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptComision : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvComision;

	public frmRptComision()
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
		this.crvComision = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvComision.ActiveViewIndex = -1;
		this.crvComision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvComision.DisplayGroupTree = false;
		this.crvComision.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvComision.Location = new System.Drawing.Point(0, 0);
		this.crvComision.Name = "crvComision";
		this.crvComision.SelectionFormula = "";
		this.crvComision.Size = new System.Drawing.Size(284, 262);
		this.crvComision.TabIndex = 0;
		this.crvComision.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvComision);
		base.Name = "frmRptComision";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptComision";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
