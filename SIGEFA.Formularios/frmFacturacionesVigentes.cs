using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmFacturacionesVigentes : Office2007Form
{
	private clsAdmFactura Admfac = new clsAdmFactura();

	private clsFactura fac = new clsFactura();

	public int Proce = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int OrdCom;

	public clsProveedor deta = new clsProveedor();

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvOrdenes;

	private Button btnSalir;

	private Button btnIrCotizacion;

	private ImageList imageList1;

	private Button btnAnular;

	private ImageList imageList2;

	private ImageList imageList3;

	private Button btnConsultar;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private ImageList imageList4;

	private DataGridViewTextBoxColumn Documento;

	private DataGridViewTextBoxColumn codfactura;

	private DataGridViewTextBoxColumn RUC;

	private DataGridViewTextBoxColumn proveedor;

	private DataGridViewTextBoxColumn codProveedor;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn Comentario;

	private DataGridViewTextBoxColumn Estado;

	public frmFacturacionesVigentes()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		dgvOrdenes.DataSource = data;
		data.DataSource = Admfac.MuestraFactura_Facturacion(dtpDesde.Value.Date, dtpHasta.Value.Date, frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void btnIrCotizacion_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && dgvOrdenes.CurrentRow != null)
		{
			DataGridViewRow row = dgvOrdenes.CurrentRow;
			frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
			form.MdiParent = base.MdiParent;
			form.codFac = fac.CodFactura;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void frmOrdenesVigentes_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvCotizaciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && e.Row.Selected)
		{
			fac.CodFactura = Convert.ToInt32(e.Row.Cells[codfactura.Name].Value);
		}
	}

	private void dgvCotizaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
			form.MdiParent = base.MdiParent;
			form.codFac = fac.CodFactura;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.CurrentRow != null)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la Factura seleccionada", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && Admfac.anular(fac.CodFactura))
			{
				MessageBox.Show("La Factura ha sido anulada correctamente", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		CargaLista();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmFacturacionesVigentes));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvOrdenes = new System.Windows.Forms.DataGridView();
		this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codfactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.RUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnIrCotizacion = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.btnConsultar = new System.Windows.Forms.Button();
		this.imageList4 = new System.Windows.Forms.ImageList(this.components);
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvOrdenes);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(860, 360);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Vigentes";
		this.dgvOrdenes.AllowUserToAddRows = false;
		this.dgvOrdenes.AllowUserToDeleteRows = false;
		this.dgvOrdenes.AllowUserToOrderColumns = true;
		this.dgvOrdenes.AllowUserToResizeRows = false;
		this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvOrdenes.Columns.AddRange(this.Documento, this.codfactura, this.RUC, this.proveedor, this.codProveedor, this.fecha, this.responsable, this.Comentario, this.Estado);
		this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvOrdenes.Location = new System.Drawing.Point(3, 16);
		this.dgvOrdenes.MultiSelect = false;
		this.dgvOrdenes.Name = "dgvOrdenes";
		this.dgvOrdenes.ReadOnly = true;
		this.dgvOrdenes.RowHeadersVisible = false;
		this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvOrdenes.Size = new System.Drawing.Size(854, 341);
		this.dgvOrdenes.TabIndex = 0;
		this.dgvOrdenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCotizaciones_CellDoubleClick);
		this.dgvOrdenes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCotizaciones_RowStateChanged);
		this.Documento.DataPropertyName = "documento";
		this.Documento.HeaderText = "Documento";
		this.Documento.Name = "Documento";
		this.Documento.ReadOnly = true;
		this.codfactura.DataPropertyName = "codfactura";
		this.codfactura.HeaderText = "codfactura";
		this.codfactura.Name = "codfactura";
		this.codfactura.ReadOnly = true;
		this.codfactura.Visible = false;
		this.RUC.DataPropertyName = "ruc";
		this.RUC.HeaderText = "Ruc";
		this.RUC.Name = "RUC";
		this.RUC.ReadOnly = true;
		this.proveedor.DataPropertyName = "razonsocial";
		this.proveedor.HeaderText = "Proveedor";
		this.proveedor.Name = "proveedor";
		this.proveedor.ReadOnly = true;
		this.proveedor.Width = 270;
		this.codProveedor.DataPropertyName = "codProveedor";
		this.codProveedor.HeaderText = "codProveedor";
		this.codProveedor.Name = "codProveedor";
		this.codProveedor.ReadOnly = true;
		this.codProveedor.Visible = false;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle2;
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 90;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 180;
		this.Comentario.DataPropertyName = "comentario";
		this.Comentario.HeaderText = "Comentario";
		this.Comentario.Name = "Comentario";
		this.Comentario.ReadOnly = true;
		this.Comentario.Visible = false;
		this.Estado.DataPropertyName = "Estado";
		this.Estado.HeaderText = "Estado";
		this.Estado.Name = "Estado";
		this.Estado.ReadOnly = true;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(657, 369);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular Orden";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btnIrCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrCotizacion.ImageIndex = 1;
		this.btnIrCotizacion.ImageList = this.imageList1;
		this.btnIrCotizacion.Location = new System.Drawing.Point(554, 372);
		this.btnIrCotizacion.Name = "btnIrCotizacion";
		this.btnIrCotizacion.Size = new System.Drawing.Size(97, 37);
		this.btnIrCotizacion.TabIndex = 2;
		this.btnIrCotizacion.Text = "Ir Facturación";
		this.btnIrCotizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrCotizacion.UseVisualStyleBackColor = true;
		this.btnIrCotizacion.Click += new System.EventHandler(btnIrCotizacion_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(759, 369);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList2.Images.SetKeyName(1, "Add.png");
		this.imageList2.Images.SetKeyName(2, "Remove.png");
		this.imageList2.Images.SetKeyName(3, "Write Document.png");
		this.imageList2.Images.SetKeyName(4, "New Document.png");
		this.imageList2.Images.SetKeyName(5, "Remove Document.png");
		this.imageList2.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList2.Images.SetKeyName(7, "document-print.png");
		this.imageList2.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList2.Images.SetKeyName(9, "refresh_256.png");
		this.imageList2.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList2.Images.SetKeyName(11, "search (1).png");
		this.imageList2.Images.SetKeyName(12, "search (5).png");
		this.imageList2.Images.SetKeyName(13, "search (6).png");
		this.imageList2.Images.SetKeyName(14, "search (8).png");
		this.imageList2.Images.SetKeyName(15, "search_top.png");
		this.imageList2.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList2.Images.SetKeyName(17, "Folder open.png");
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Write Document.png");
		this.imageList3.Images.SetKeyName(1, "New Document.png");
		this.imageList3.Images.SetKeyName(2, "Remove Document.png");
		this.imageList3.Images.SetKeyName(3, "document-print.png");
		this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList3.Images.SetKeyName(5, "exit.png");
		this.imageList3.Images.SetKeyName(6, "OK_Verde.png");
		this.btnConsultar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnConsultar.ImageIndex = 6;
		this.btnConsultar.ImageList = this.imageList4;
		this.btnConsultar.Location = new System.Drawing.Point(345, 371);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(105, 32);
		this.btnConsultar.TabIndex = 24;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.imageList4.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList4.ImageStream");
		this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList4.Images.SetKeyName(0, "Write Document.png");
		this.imageList4.Images.SetKeyName(1, "New Document.png");
		this.imageList4.Images.SetKeyName(2, "Remove Document.png");
		this.imageList4.Images.SetKeyName(3, "document-print.png");
		this.imageList4.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList4.Images.SetKeyName(5, "exit.png");
		this.imageList4.Images.SetKeyName(6, "search (1).png");
		this.imageList4.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList4.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList4.Images.SetKeyName(9, "document_delete.png");
		this.imageList4.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList4.Images.SetKeyName(11, "OK_Verde.png");
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(177, 381);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 23;
		this.label6.Text = "Hasta :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 381);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 22;
		this.label5.Text = "Desde :";
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(65, 378);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 21;
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(224, 378);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 20;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(860, 415);
		base.Controls.Add(this.btnConsultar);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btnIrCotizacion);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmFacturacionesVigentes";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Facturaciones";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmOrdenesVigentes_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
