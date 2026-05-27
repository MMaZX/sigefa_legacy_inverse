using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmReporteArqueoFondoFijoRPT : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvArqueoFondoFijo;

	public frmReporteArqueoFondoFijoRPT()
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
		this.crvArqueoFondoFijo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvArqueoFondoFijo.ActiveViewIndex = -1;
		this.crvArqueoFondoFijo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvArqueoFondoFijo.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvArqueoFondoFijo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvArqueoFondoFijo.Location = new System.Drawing.Point(0, 0);
		this.crvArqueoFondoFijo.Name = "crvArqueoFondoFijo";
		this.crvArqueoFondoFijo.Size = new System.Drawing.Size(292, 273);
		this.crvArqueoFondoFijo.TabIndex = 0;
		this.crvArqueoFondoFijo.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 273);
		base.Controls.Add(this.crvArqueoFondoFijo);
		base.Name = "frmReporteArqueoFondoFijoRPT";
		this.Text = "Reporte Arqueo Fondo Fijo";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
