using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmListaNotasCredito : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvNotasCredito;

	public frmListaNotasCredito()
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
		this.crvNotasCredito = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvNotasCredito.ActiveViewIndex = -1;
		this.crvNotasCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvNotasCredito.DisplayGroupTree = false;
		this.crvNotasCredito.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvNotasCredito.Location = new System.Drawing.Point(0, 0);
		this.crvNotasCredito.Name = "crvNotasCredito";
		this.crvNotasCredito.SelectionFormula = "";
		this.crvNotasCredito.Size = new System.Drawing.Size(284, 262);
		this.crvNotasCredito.TabIndex = 0;
		this.crvNotasCredito.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvNotasCredito);
		base.Name = "frmListaNotasCredito";
		this.Text = "frmListaNotasCredito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
