using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptPagosxDia : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvPagosDia;

	public frmRptPagosxDia()
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
		this.crvPagosDia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvPagosDia.ActiveViewIndex = -1;
		this.crvPagosDia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvPagosDia.DisplayGroupTree = false;
		this.crvPagosDia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvPagosDia.Location = new System.Drawing.Point(0, 0);
		this.crvPagosDia.Name = "crvPagosDia";
		this.crvPagosDia.SelectionFormula = "";
		this.crvPagosDia.Size = new System.Drawing.Size(284, 262);
		this.crvPagosDia.TabIndex = 0;
		this.crvPagosDia.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvPagosDia);
		base.Name = "frmRptPagosxDia";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Pagos por Dia";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
