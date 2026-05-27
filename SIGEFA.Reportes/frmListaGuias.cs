using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmListaGuias : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvListaGuias;

	public frmListaGuias()
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
		this.crvListaGuias = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvListaGuias.ActiveViewIndex = -1;
		this.crvListaGuias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvListaGuias.DisplayGroupTree = false;
		this.crvListaGuias.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvListaGuias.Location = new System.Drawing.Point(0, 0);
		this.crvListaGuias.Name = "crvListaGuias";
		this.crvListaGuias.SelectionFormula = "";
		this.crvListaGuias.Size = new System.Drawing.Size(284, 262);
		this.crvListaGuias.TabIndex = 0;
		this.crvListaGuias.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvListaGuias);
		base.Name = "frmListaGuias";
		this.Text = "frmListaGuias";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
