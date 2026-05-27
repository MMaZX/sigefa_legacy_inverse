using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Data;

namespace SIGEFA.Formularios;

public class frmMuestraStock : Form
{
	public int CodProducto;

	public int CodAlmacen;

	private IContainer components = null;

	private DataGridView dgvstock;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn stock;

	public frmMuestraStock()
	{
		InitializeComponent();
	}

	private void frmMuestraStock_Load(object sender, EventArgs e)
	{
		try
		{
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			dBAccess.AddParameter("codproducto", CodProducto);
			dBAccess.AddParameter("codalmacen", CodAlmacen);
			ds = dBAccess.ExecuteDataSet("ConsultaStock");
			dgvstock.DataSource = ds.Tables[0];
			if (dgvstock.Rows.Count <= 0)
			{
				MessageBox.Show("No se encontraron registros para mostrar.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error : " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
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
		this.dgvstock = new System.Windows.Forms.DataGridView();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvstock).BeginInit();
		base.SuspendLayout();
		this.dgvstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvstock.Columns.AddRange(this.almacen, this.stock);
		this.dgvstock.Location = new System.Drawing.Point(2, 3);
		this.dgvstock.MultiSelect = false;
		this.dgvstock.Name = "dgvstock";
		this.dgvstock.ReadOnly = true;
		this.dgvstock.Size = new System.Drawing.Size(293, 99);
		this.dgvstock.TabIndex = 0;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.Width = 150;
		this.stock.DataPropertyName = "stock";
		this.stock.HeaderText = "Stock";
		this.stock.Name = "stock";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(296, 106);
		base.Controls.Add(this.dgvstock);
		base.Name = "frmMuestraStock";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmMuestraStock";
		base.Load += new System.EventHandler(frmMuestraStock_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvstock).EndInit();
		base.ResumeLayout(false);
	}
}
