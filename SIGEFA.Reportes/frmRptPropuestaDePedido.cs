using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptPropuestaDePedido : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvPropuestaDePedido;

	public frmRptPropuestaDePedido()
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
		this.crvPropuestaDePedido = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvPropuestaDePedido.ActiveViewIndex = -1;
		this.crvPropuestaDePedido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvPropuestaDePedido.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvPropuestaDePedido.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvPropuestaDePedido.Location = new System.Drawing.Point(0, 0);
		this.crvPropuestaDePedido.Name = "crvPropuestaDePedido";
		this.crvPropuestaDePedido.Size = new System.Drawing.Size(649, 332);
		this.crvPropuestaDePedido.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(649, 332);
		base.Controls.Add(this.crvPropuestaDePedido);
		base.Name = "frmRptPropuestaDePedido";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte Propuesta De Pedido";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
