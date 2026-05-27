using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmFacturasLetrasXVencer : OfficeForm
{
	private clsAdmAlmacen Admalmac = new clsAdmAlmacen();

	private IContainer components = null;

	private Button button1;

	private DateTimePicker dtpFechaPago;

	private Button btnAceptar;

	private Label label1;

	private DataGridView dgvProductos;

	private ComboBox cbTipoArticulo;

	private ImageList imageList1;

	private DataGridViewTextBoxColumn Documento;

	private DataGridViewTextBoxColumn proveedor;

	private DataGridViewTextBoxColumn fechavence;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn pendiente;

	public frmFacturasLetrasXVencer()
	{
		InitializeComponent();
	}

	private void frmFacturasLetrasXVencer_Load(object sender, EventArgs e)
	{
		dgvProductos.DataSource = Admalmac.AlertaFacturasLetrasXVencer();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		clsReporteFactura dso = new clsReporteFactura();
		CRFacturasLetrasXvencer rpt = new CRFacturasLetrasXvencer();
		frmRptKardex frm = new frmRptKardex();
		rpt.SetDataSource(dso.RPTFacturasLetraXvencer());
		frm.crvKardex.ReportSource = rpt;
		frm.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmFacturasLetrasXVencer));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.button1 = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvProductos = new System.Windows.Forms.DataGridView();
		this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechavence = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cbTipoArticulo = new System.Windows.Forms.ComboBox();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).BeginInit();
		base.SuspendLayout();
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 3;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(528, 216);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(85, 32);
		this.button1.TabIndex = 89;
		this.button1.Text = "Reporte";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(600, 12);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(96, 20);
		this.dtpFechaPago.TabIndex = 87;
		this.dtpFechaPago.Tag = "16";
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(619, 216);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 86;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(548, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 13);
		this.label1.TabIndex = 85;
		this.label1.Text = "Fecha";
		this.dgvProductos.AllowUserToAddRows = false;
		this.dgvProductos.AllowUserToDeleteRows = false;
		this.dgvProductos.AllowUserToResizeColumns = false;
		this.dgvProductos.AllowUserToResizeRows = false;
		this.dgvProductos.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProductos.Columns.AddRange(this.Documento, this.proveedor, this.fechavence, this.total, this.pendiente);
		this.dgvProductos.Location = new System.Drawing.Point(12, 41);
		this.dgvProductos.Name = "dgvProductos";
		this.dgvProductos.ReadOnly = true;
		this.dgvProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvProductos.RowHeadersVisible = false;
		this.dgvProductos.RowHeadersWidth = 40;
		this.dgvProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProductos.Size = new System.Drawing.Size(684, 164);
		this.dgvProductos.StandardTab = true;
		this.dgvProductos.TabIndex = 84;
		this.Documento.DataPropertyName = "Documento";
		this.Documento.HeaderText = "Documento";
		this.Documento.Name = "Documento";
		this.Documento.ReadOnly = true;
		this.Documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Documento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Documento.Width = 80;
		this.proveedor.DataPropertyName = "proveedor";
		this.proveedor.HeaderText = "Proveedor";
		this.proveedor.Name = "proveedor";
		this.proveedor.ReadOnly = true;
		this.proveedor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.proveedor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.proveedor.Width = 300;
		this.fechavence.DataPropertyName = "fechavence";
		this.fechavence.HeaderText = "Fecha Vencimiento";
		this.fechavence.Name = "fechavence";
		this.fechavence.ReadOnly = true;
		this.total.DataPropertyName = "total";
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.pendiente.DataPropertyName = "pendiente";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		dataGridViewCellStyle2.NullValue = null;
		this.pendiente.DefaultCellStyle = dataGridViewCellStyle2;
		this.pendiente.HeaderText = "Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.pendiente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.pendiente.Width = 60;
		this.cbTipoArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbTipoArticulo.Enabled = false;
		this.cbTipoArticulo.FormattingEnabled = true;
		this.cbTipoArticulo.Location = new System.Drawing.Point(416, 106);
		this.cbTipoArticulo.Name = "cbTipoArticulo";
		this.cbTipoArticulo.Size = new System.Drawing.Size(180, 21);
		this.cbTipoArticulo.TabIndex = 88;
		this.cbTipoArticulo.Tag = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(708, 260);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dtpFechaPago);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dgvProductos);
		base.Controls.Add(this.cbTipoArticulo);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmFacturasLetrasXVencer";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "FACTURAS Y LETRAS POR VENCER";
		base.Load += new System.EventHandler(frmFacturasLetrasXVencer_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
