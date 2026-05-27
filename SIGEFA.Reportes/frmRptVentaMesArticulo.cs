using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptVentaMesArticulo : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptVentaMesArticulo;

	public frmRptVentaMesArticulo()
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
		this.crvRptVentaMesArticulo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptVentaMesArticulo.ActiveViewIndex = -1;
		this.crvRptVentaMesArticulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptVentaMesArticulo.DisplayGroupTree = false;
		this.crvRptVentaMesArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptVentaMesArticulo.Location = new System.Drawing.Point(0, 0);
		this.crvRptVentaMesArticulo.Name = "crvRptVentaMesArticulo";
		this.crvRptVentaMesArticulo.SelectionFormula = "";
		this.crvRptVentaMesArticulo.Size = new System.Drawing.Size(638, 395);
		this.crvRptVentaMesArticulo.TabIndex = 0;
		this.crvRptVentaMesArticulo.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(638, 395);
		base.Controls.Add(this.crvRptVentaMesArticulo);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmRptVentaMesArticulo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Venta de Articulo por Mes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
