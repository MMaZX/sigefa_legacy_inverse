using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptDespachoDocumento : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvDespachoDocumento;

	public frmRptDespachoDocumento()
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
		this.crvDespachoDocumento = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvDespachoDocumento.ActiveViewIndex = -1;
		this.crvDespachoDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvDespachoDocumento.DisplayGroupTree = false;
		this.crvDespachoDocumento.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvDespachoDocumento.Location = new System.Drawing.Point(0, 0);
		this.crvDespachoDocumento.Name = "crvDespachoDocumento";
		this.crvDespachoDocumento.SelectionFormula = "";
		this.crvDespachoDocumento.Size = new System.Drawing.Size(284, 262);
		this.crvDespachoDocumento.TabIndex = 0;
		this.crvDespachoDocumento.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvDespachoDocumento);
		base.Name = "frmRptDespachoDocumento";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Despachos por Documento";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
