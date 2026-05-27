using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptCobrosxDia : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvCobros;

	public frmRptCobrosxDia()
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
		this.crvCobros = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvCobros.ActiveViewIndex = -1;
		this.crvCobros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvCobros.DisplayGroupTree = false;
		this.crvCobros.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvCobros.Location = new System.Drawing.Point(0, 0);
		this.crvCobros.Name = "crvCobros";
		this.crvCobros.SelectionFormula = "";
		this.crvCobros.Size = new System.Drawing.Size(284, 262);
		this.crvCobros.TabIndex = 0;
		this.crvCobros.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvCobros);
		base.Name = "frmRptCobrosxDia";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Cobros por Dia";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
