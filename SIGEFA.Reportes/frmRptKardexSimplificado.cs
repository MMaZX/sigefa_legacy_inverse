using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SIGEFA.Reportes;

public class frmRptKardexSimplificado : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvKardex;

	private CustomInstaller customInstaller1;

	public frmRptKardexSimplificado()
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
		this.crvKardex = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		this.customInstaller1 = new MySql.Data.MySqlClient.CustomInstaller();
		base.SuspendLayout();
		this.crvKardex.ActiveViewIndex = -1;
		this.crvKardex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvKardex.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvKardex.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvKardex.Location = new System.Drawing.Point(0, 0);
		this.crvKardex.Name = "crvKardex";
		this.crvKardex.SelectionFormula = "";
		this.crvKardex.Size = new System.Drawing.Size(284, 262);
		this.crvKardex.TabIndex = 0;
		this.crvKardex.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		this.crvKardex.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvKardex);
		base.Name = "frmRptKardexSimplificado";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptKardexSimplificado";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRptKardex_Load);
		base.ResumeLayout(false);
	}
}
