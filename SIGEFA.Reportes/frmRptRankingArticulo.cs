using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptRankingArticulo : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRankingArticulo;

	public frmRptRankingArticulo()
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
		this.crvRankingArticulo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRankingArticulo.ActiveViewIndex = -1;
		this.crvRankingArticulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRankingArticulo.DisplayGroupTree = false;
		this.crvRankingArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRankingArticulo.Location = new System.Drawing.Point(0, 0);
		this.crvRankingArticulo.Name = "crvRankingArticulo";
		this.crvRankingArticulo.SelectionFormula = "";
		this.crvRankingArticulo.Size = new System.Drawing.Size(284, 262);
		this.crvRankingArticulo.TabIndex = 0;
		this.crvRankingArticulo.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvRankingArticulo);
		base.Name = "frmRptRankingArticulo";
		this.Text = "frmRptRankingArticulo";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
