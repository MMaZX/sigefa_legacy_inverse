using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class FrmRptVentaxArticulo : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvVentaxArticulo;

	public FrmRptVentaxArticulo()
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
		this.crvVentaxArticulo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvVentaxArticulo.ActiveViewIndex = -1;
		this.crvVentaxArticulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvVentaxArticulo.DisplayGroupTree = false;
		this.crvVentaxArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvVentaxArticulo.Location = new System.Drawing.Point(0, 0);
		this.crvVentaxArticulo.Name = "crvVentaxArticulo";
		this.crvVentaxArticulo.SelectionFormula = "";
		this.crvVentaxArticulo.Size = new System.Drawing.Size(284, 262);
		this.crvVentaxArticulo.TabIndex = 0;
		this.crvVentaxArticulo.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvVentaxArticulo);
		base.Name = "FrmRptVentaxArticulo";
		this.Text = "Reporte de Venta por Articulo";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
