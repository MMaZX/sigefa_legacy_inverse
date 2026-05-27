using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEFA.Formularios;

public class frmProductoSunat : Form
{
	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox2;

	private Button btnAceptar;

	private GroupBox groupBox3;

	public DataGridView dgvProductoSunat;

	private DataGridViewTextBoxColumn producto;

	private DataGridViewTextBoxColumn codigosunat;

	public frmProductoSunat()
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductoSunat));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgvProductoSunat = new System.Windows.Forms.DataGridView();
		this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigosunat = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProductoSunat).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.groupBox2.Controls.Add(this.btnAceptar);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 227);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(580, 40);
		this.groupBox2.TabIndex = 9;
		this.groupBox2.TabStop = false;
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(494, 5);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 5;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.groupBox3.Controls.Add(this.dgvProductoSunat);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(580, 227);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Facturas";
		this.dgvProductoSunat.AllowUserToAddRows = false;
		this.dgvProductoSunat.AllowUserToDeleteRows = false;
		this.dgvProductoSunat.AllowUserToResizeColumns = false;
		this.dgvProductoSunat.AllowUserToResizeRows = false;
		this.dgvProductoSunat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProductoSunat.Columns.AddRange(this.producto, this.codigosunat);
		this.dgvProductoSunat.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvProductoSunat.Location = new System.Drawing.Point(3, 16);
		this.dgvProductoSunat.MultiSelect = false;
		this.dgvProductoSunat.Name = "dgvProductoSunat";
		this.dgvProductoSunat.ReadOnly = true;
		this.dgvProductoSunat.RowHeadersVisible = false;
		this.dgvProductoSunat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProductoSunat.Size = new System.Drawing.Size(574, 208);
		this.dgvProductoSunat.TabIndex = 10;
		this.producto.DataPropertyName = "producto";
		this.producto.HeaderText = "Producto";
		this.producto.Name = "producto";
		this.producto.ReadOnly = true;
		this.producto.Width = 450;
		this.codigosunat.DataPropertyName = "codigosunat";
		dataGridViewCellStyle1.Format = "d";
		dataGridViewCellStyle1.NullValue = null;
		this.codigosunat.DefaultCellStyle = dataGridViewCellStyle1;
		this.codigosunat.HeaderText = "Codigo Sunat";
		this.codigosunat.Name = "codigosunat";
		this.codigosunat.ReadOnly = true;
		this.codigosunat.Width = 150;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(580, 267);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmProductoSunat";
		this.Text = "Productos Sunat";
		this.groupBox2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvProductoSunat).EndInit();
		base.ResumeLayout(false);
	}
}
