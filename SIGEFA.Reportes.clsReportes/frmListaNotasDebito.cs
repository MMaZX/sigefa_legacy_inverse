using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes.clsReportes;

public class frmListaNotasDebito : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvNotasDebito;

	public frmListaNotasDebito()
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
		this.crvNotasDebito = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvNotasDebito.ActiveViewIndex = -1;
		this.crvNotasDebito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvNotasDebito.DisplayGroupTree = false;
		this.crvNotasDebito.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvNotasDebito.Location = new System.Drawing.Point(0, 0);
		this.crvNotasDebito.Name = "crvNotasDebito";
		this.crvNotasDebito.SelectionFormula = "";
		this.crvNotasDebito.Size = new System.Drawing.Size(284, 262);
		this.crvNotasDebito.TabIndex = 1;
		this.crvNotasDebito.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvNotasDebito);
		base.Name = "frmListaNotasDebito";
		this.Text = "Notas Débito Venta";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
