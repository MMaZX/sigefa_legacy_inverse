using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmModificarPrecioCompraProducto : Office2007Form
{
	private IContainer components = null;

	public frmModificarPrecioCompraProducto()
	{
		InitializeComponent();
	}

	private void frmModificarPrecioCompraProducto_Load(object sender, EventArgs e)
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
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(495, 178);
		this.DoubleBuffered = true;
		base.Name = "frmModificarPrecioCompraProducto";
		this.Text = "CAMBIO EN EL PRECIO DE COMPRA DE PRODUCTO";
		base.Load += new System.EventHandler(frmModificarPrecioCompraProducto_Load);
		base.ResumeLayout(false);
	}
}
