using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptRankingCliente : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRankingCliente;

	public frmRptRankingCliente()
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
		this.crvRankingCliente = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRankingCliente.ActiveViewIndex = -1;
		this.crvRankingCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRankingCliente.DisplayGroupTree = false;
		this.crvRankingCliente.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRankingCliente.Location = new System.Drawing.Point(0, 0);
		this.crvRankingCliente.Name = "crvRankingCliente";
		this.crvRankingCliente.SelectionFormula = "";
		this.crvRankingCliente.Size = new System.Drawing.Size(284, 262);
		this.crvRankingCliente.TabIndex = 0;
		this.crvRankingCliente.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvRankingCliente);
		base.Name = "frmRptRankingCliente";
		this.Text = "frmRptRankingCliente";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
