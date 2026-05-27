using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptVentxCliente2 : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptVentxCliente2;

	public frmRptVentxCliente2()
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
		this.crvRptVentxCliente2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptVentxCliente2.ActiveViewIndex = -1;
		this.crvRptVentxCliente2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptVentxCliente2.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvRptVentxCliente2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptVentxCliente2.Location = new System.Drawing.Point(0, 0);
		this.crvRptVentxCliente2.Name = "crvRptVentxCliente2";
		this.crvRptVentxCliente2.SelectionFormula = "";
		this.crvRptVentxCliente2.Size = new System.Drawing.Size(635, 370);
		this.crvRptVentxCliente2.TabIndex = 1;
		this.crvRptVentxCliente2.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(635, 370);
		base.Controls.Add(this.crvRptVentxCliente2);
		base.Name = "frmRptVentxCliente2";
		this.Text = "Reporte de Venta por Cliente / Articulo";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
