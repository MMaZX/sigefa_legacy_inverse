using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptCuotas : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvCuotas;

	public frmRptCuotas()
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
		this.crvCuotas = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvCuotas.ActiveViewIndex = -1;
		this.crvCuotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvCuotas.DisplayGroupTree = false;
		this.crvCuotas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvCuotas.Location = new System.Drawing.Point(0, 0);
		this.crvCuotas.Name = "crvCuotas";
		this.crvCuotas.SelectionFormula = "";
		this.crvCuotas.Size = new System.Drawing.Size(284, 262);
		this.crvCuotas.TabIndex = 1;
		this.crvCuotas.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvCuotas);
		base.Name = "frmRptCuotas";
		this.Text = "frmRptCuotas";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
