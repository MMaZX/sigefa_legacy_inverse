using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptUtilidad : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvInventario;

	public frmRptUtilidad()
	{
		InitializeComponent();
	}

	private void frmRptKardex_Load(object sender, EventArgs e)
	{
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
		this.crvInventario = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvInventario.ActiveViewIndex = -1;
		this.crvInventario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvInventario.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvInventario.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvInventario.Location = new System.Drawing.Point(0, 0);
		this.crvInventario.Name = "crvInventario";
		this.crvInventario.SelectionFormula = "";
		this.crvInventario.Size = new System.Drawing.Size(1007, 522);
		this.crvInventario.TabIndex = 0;
		this.crvInventario.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		this.crvInventario.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1007, 522);
		base.Controls.Add(this.crvInventario);
		base.Name = "frmRptUtilidad";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptInventario";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRptKardex_Load);
		base.ResumeLayout(false);
	}
}
