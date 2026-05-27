using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptGananciaxArticulo : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptGananciaxArticulo;

	public frmRptGananciaxArticulo()
	{
		InitializeComponent();
	}

	private void frmRptGananciaxArticulo_Load(object sender, EventArgs e)
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
		this.crvRptGananciaxArticulo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptGananciaxArticulo.ActiveViewIndex = -1;
		this.crvRptGananciaxArticulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptGananciaxArticulo.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvRptGananciaxArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptGananciaxArticulo.Location = new System.Drawing.Point(0, 0);
		this.crvRptGananciaxArticulo.Name = "crvRptGananciaxArticulo";
		this.crvRptGananciaxArticulo.Size = new System.Drawing.Size(391, 357);
		this.crvRptGananciaxArticulo.TabIndex = 1;
		this.crvRptGananciaxArticulo.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(391, 357);
		base.Controls.Add(this.crvRptGananciaxArticulo);
		base.Name = "frmRptGananciaxArticulo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte Ganancia por Articulo";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRptGananciaxArticulo_Load);
		base.ResumeLayout(false);
	}
}
