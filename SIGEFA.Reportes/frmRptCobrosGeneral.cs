using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptCobrosGeneral : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvCobrosGeneral;

	public frmRptCobrosGeneral()
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
		this.crvCobrosGeneral = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvCobrosGeneral.ActiveViewIndex = -1;
		this.crvCobrosGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvCobrosGeneral.DisplayGroupTree = false;
		this.crvCobrosGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvCobrosGeneral.Location = new System.Drawing.Point(0, 0);
		this.crvCobrosGeneral.Name = "crvCobrosGeneral";
		this.crvCobrosGeneral.SelectionFormula = "";
		this.crvCobrosGeneral.Size = new System.Drawing.Size(284, 262);
		this.crvCobrosGeneral.TabIndex = 0;
		this.crvCobrosGeneral.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvCobrosGeneral);
		base.Name = "frmRptCobrosGeneral";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptCobrosGeneral";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
