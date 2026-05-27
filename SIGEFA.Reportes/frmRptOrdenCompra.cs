using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptOrdenCompra : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvOrdenCompra;

	public frmRptOrdenCompra()
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
		this.crvOrdenCompra = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvOrdenCompra.ActiveViewIndex = -1;
		this.crvOrdenCompra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvOrdenCompra.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvOrdenCompra.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvOrdenCompra.Location = new System.Drawing.Point(0, 0);
		this.crvOrdenCompra.Name = "crvOrdenCompra";
		this.crvOrdenCompra.SelectionFormula = "";
		this.crvOrdenCompra.Size = new System.Drawing.Size(284, 262);
		this.crvOrdenCompra.TabIndex = 0;
		this.crvOrdenCompra.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvOrdenCompra);
		base.Name = "frmRptOrdenCompra";
		this.Text = "Reporte - Orden Compra";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
