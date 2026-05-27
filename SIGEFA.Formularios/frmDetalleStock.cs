using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;

namespace SIGEFA.Formularios;

public class frmDetalleStock : Form
{
	public int CodProducto = 0;

	private clsAdmOrdenCompra admOrden = new clsAdmOrdenCompra();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private DataGridView dgvDetalleStock;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn stock_act;

	public frmDetalleStock()
	{
		InitializeComponent();
	}

	private void CargaDetalle()
	{
		dgvDetalleStock.DataSource = data;
		data.DataSource = admOrden.StockActualProducto(CodProducto);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleStock.ClearSelection();
	}

	private void frmDetalleStock_Load(object sender, EventArgs e)
	{
		CargaDetalle();
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
		this.dgvDetalleStock = new System.Windows.Forms.DataGridView();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stock_act = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalleStock).BeginInit();
		base.SuspendLayout();
		this.dgvDetalleStock.AllowUserToAddRows = false;
		this.dgvDetalleStock.AllowUserToDeleteRows = false;
		this.dgvDetalleStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalleStock.Columns.AddRange(this.almacen, this.stock_act);
		this.dgvDetalleStock.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalleStock.Location = new System.Drawing.Point(0, 0);
		this.dgvDetalleStock.Name = "dgvDetalleStock";
		this.dgvDetalleStock.ReadOnly = true;
		this.dgvDetalleStock.RowHeadersVisible = false;
		this.dgvDetalleStock.Size = new System.Drawing.Size(263, 182);
		this.dgvDetalleStock.TabIndex = 0;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "ALMACEN";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.Width = 150;
		this.stock_act.DataPropertyName = "StockActual";
		this.stock_act.HeaderText = "STOCK ACTUAL";
		this.stock_act.Name = "stock_act";
		this.stock_act.ReadOnly = true;
		this.stock_act.Width = 110;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(263, 182);
		base.Controls.Add(this.dgvDetalleStock);
		base.Name = "frmDetalleStock";
		this.Text = "StockActualPorAlmacen";
		base.Load += new System.EventHandler(frmDetalleStock_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalleStock).EndInit();
		base.ResumeLayout(false);
	}
}
