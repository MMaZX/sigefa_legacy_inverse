using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptVentxArtixVendedor : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptVentxCliente;

	public frmRptVentxArtixVendedor()
	{
		InitializeComponent();
	}

	private void frmRptVentxCliente_Load(object sender, EventArgs e)
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
		this.crvRptVentxCliente = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptVentxCliente.ActiveViewIndex = -1;
		this.crvRptVentxCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptVentxCliente.DisplayGroupTree = false;
		this.crvRptVentxCliente.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptVentxCliente.Location = new System.Drawing.Point(0, 0);
		this.crvRptVentxCliente.Name = "crvRptVentxCliente";
		this.crvRptVentxCliente.SelectionFormula = "";
		this.crvRptVentxCliente.Size = new System.Drawing.Size(674, 427);
		this.crvRptVentxCliente.TabIndex = 0;
		this.crvRptVentxCliente.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(674, 427);
		base.Controls.Add(this.crvRptVentxCliente);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmRptVentxArtixVendedor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Ventas por Articulo";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRptVentxCliente_Load);
		base.ResumeLayout(false);
	}
}
