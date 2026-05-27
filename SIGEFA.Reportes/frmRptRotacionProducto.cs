using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptRotacionProducto : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVRotacionProducto;

	public frmRptRotacionProducto()
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
		this.cRVRotacionProducto = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVRotacionProducto.ActiveViewIndex = -1;
		this.cRVRotacionProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVRotacionProducto.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVRotacionProducto.Location = new System.Drawing.Point(0, 0);
		this.cRVRotacionProducto.Name = "cRVRotacionProducto";
		this.cRVRotacionProducto.SelectionFormula = "";
		this.cRVRotacionProducto.Size = new System.Drawing.Size(532, 437);
		this.cRVRotacionProducto.TabIndex = 0;
		this.cRVRotacionProducto.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(532, 437);
		base.Controls.Add(this.cRVRotacionProducto);
		base.Name = "frmRptRotacionProducto";
		this.Text = "frmRptRotacionProducto";
		base.ResumeLayout(false);
	}
}
