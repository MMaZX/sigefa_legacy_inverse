using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmNotasIngresoSalida : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvNotasIngresoSalida;

	public frmNotasIngresoSalida()
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
		this.crvNotasIngresoSalida = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvNotasIngresoSalida.ActiveViewIndex = -1;
		this.crvNotasIngresoSalida.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvNotasIngresoSalida.DisplayGroupTree = false;
		this.crvNotasIngresoSalida.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvNotasIngresoSalida.Location = new System.Drawing.Point(0, 0);
		this.crvNotasIngresoSalida.Name = "crvNotasIngresoSalida";
		this.crvNotasIngresoSalida.SelectionFormula = "";
		this.crvNotasIngresoSalida.Size = new System.Drawing.Size(284, 262);
		this.crvNotasIngresoSalida.TabIndex = 0;
		this.crvNotasIngresoSalida.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvNotasIngresoSalida);
		base.Name = "frmNotasIngresoSalida";
		this.Text = "frmNotasIngresoSalida";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
