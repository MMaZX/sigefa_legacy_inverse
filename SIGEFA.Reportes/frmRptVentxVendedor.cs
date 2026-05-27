using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptVentxVendedor : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptVentxVendedor;

	public frmRptVentxVendedor()
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
		this.crvRptVentxVendedor = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptVentxVendedor.ActiveViewIndex = -1;
		this.crvRptVentxVendedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptVentxVendedor.DisplayGroupTree = false;
		this.crvRptVentxVendedor.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptVentxVendedor.Location = new System.Drawing.Point(0, 0);
		this.crvRptVentxVendedor.Name = "crvRptVentxVendedor";
		this.crvRptVentxVendedor.SelectionFormula = "";
		this.crvRptVentxVendedor.Size = new System.Drawing.Size(284, 262);
		this.crvRptVentxVendedor.TabIndex = 0;
		this.crvRptVentxVendedor.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvRptVentxVendedor);
		base.Name = "frmRptVentxVendedor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Ventas por Vendedor";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
