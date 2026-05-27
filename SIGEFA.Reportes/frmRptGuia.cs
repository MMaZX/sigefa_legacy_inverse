using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptGuia : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvGuia;

	public frmRptGuia()
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
		this.crvGuia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvGuia.ActiveViewIndex = -1;
		this.crvGuia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvGuia.DisplayGroupTree = false;
		this.crvGuia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvGuia.Location = new System.Drawing.Point(0, 0);
		this.crvGuia.Name = "crvGuia";
		this.crvGuia.SelectionFormula = "";
		this.crvGuia.Size = new System.Drawing.Size(284, 262);
		this.crvGuia.TabIndex = 0;
		this.crvGuia.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvGuia);
		base.Name = "frmRptGuia";
		this.Text = "frmRptGuia";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
