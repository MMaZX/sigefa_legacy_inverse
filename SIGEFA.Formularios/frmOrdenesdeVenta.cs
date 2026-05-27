using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmOrdenesdeVenta : Office2007Form
{
	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	public static BindingSource data = new BindingSource();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private TextBox txtFiltro;

	private Label label10;

	private DataGridView dgvPedidosPendientes;

	private ImageList imageList1;

	private Button btnSalir;

	private ImageList imageList2;

	private Button btnBusqueda;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn Numeracion;

	private DataGridViewTextBoxColumn Pedido_;

	private DataGridViewTextBoxColumn RazonSocial;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn responsable;

	private Label lblCantidadRegistros;

	public frmOrdenesdeVenta()
	{
		InitializeComponent();
	}

	private void frmOrdenesdeVenta_Load(object sender, EventArgs e)
	{
		dtpDesde.Value = DateTime.Now.AddDays(-6.0);
		CargaLista();
	}

	public void CargaLista()
	{
		dgvPedidosPendientes.DataSource = data;
		data.DataSource = AdmPedido.MuestraPedidosTodos(frmLogin.iCodUser, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		dgvPedidosPendientes.ClearSelection();
		lblCantidadRegistros.Text = "Nº DE ÓRDENES ENCONTRADAS: " + dgvPedidosPendientes.Rows.Count;
		lblCantidadRegistros.Visible = true;
	}

	private void dgvPedidosPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmOrdenVenta form = new frmOrdenVenta();
			form.CodPedido = dgvPedidosPendientes.CurrentRow.Cells[codigo.Name].Value.ToString();
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void btnBusqueda_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = string.Format("[{0}] like '*{1}*'", "documento", txtFiltro.Text.Trim());
			}
			else
			{
				data.Filter = string.Empty;
			}
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			if (ee.KeyChar != '(')
			{
				dgvPedidosPendientes.ClearSelection();
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtFiltro_Enter(object sender, EventArgs e)
	{
		txtFiltro.SelectionStart = txtFiltro.Text.Length;
		txtFiltro.SelectionLength = 0;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmOrdenesdeVenta));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblCantidadRegistros = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.dgvPedidosPendientes = new System.Windows.Forms.DataGridView();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Pedido_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPedidosPendientes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.lblCantidadRegistros);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.btnSalir);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.dgvPedidosPendientes);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(959, 382);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Pendientes";
		this.lblCantidadRegistros.AutoSize = true;
		this.lblCantidadRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadRegistros.Location = new System.Drawing.Point(6, 64);
		this.lblCantidadRegistros.Name = "lblCantidadRegistros";
		this.lblCantidadRegistros.Size = new System.Drawing.Size(251, 16);
		this.lblCantidadRegistros.TabIndex = 34;
		this.lblCantidadRegistros.Text = "Nº DE ÓRDENES ENCONTRADAS:";
		this.lblCantidadRegistros.Visible = false;
		this.btnBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList2;
		this.btnBusqueda.Location = new System.Drawing.Point(373, 21);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(96, 33);
		this.btnBusqueda.TabIndex = 33;
		this.btnBusqueda.Text = "BUSCAR";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
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
		this.imageList2.Images.SetKeyName(16, "folder_open (1).png");
		this.imageList2.Images.SetKeyName(17, "folder-open-icon.png");
		this.imageList2.Images.SetKeyName(18, "Glossy-Open-icon.png");
		this.imageList2.Images.SetKeyName(19, "Ocean Blue Open.png");
		this.imageList2.Images.SetKeyName(20, "Open (1).png");
		this.imageList2.Images.SetKeyName(21, "open_folder_green.png");
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(914, 20);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(33, 37);
		this.btnSalir.TabIndex = 32;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(188, 30);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(57, 16);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(6, 30);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(62, 16);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(74, 27);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 22);
		this.dtpDesde.TabIndex = 15;
		this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(251, 27);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 22);
		this.dtpHasta.TabIndex = 14;
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(712, 28);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(163, 20);
		this.txtFiltro.TabIndex = 8;
		this.txtFiltro.Text = "OV001-00000";
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.Enter += new System.EventHandler(txtFiltro_Enter);
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(525, 30);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(181, 16);
		this.label10.TabIndex = 7;
		this.label10.Text = "Buscar Por Nº de Orden :";
		this.dgvPedidosPendientes.AllowUserToAddRows = false;
		this.dgvPedidosPendientes.AllowUserToDeleteRows = false;
		this.dgvPedidosPendientes.AllowUserToOrderColumns = true;
		this.dgvPedidosPendientes.AllowUserToResizeRows = false;
		this.dgvPedidosPendientes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvPedidosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvPedidosPendientes.Columns.AddRange(this.documento, this.codigo, this.Numeracion, this.Pedido_, this.RazonSocial, this.cliente, this.importe, this.fecha, this.responsable);
		this.dgvPedidosPendientes.Location = new System.Drawing.Point(0, 83);
		this.dgvPedidosPendientes.MultiSelect = false;
		this.dgvPedidosPendientes.Name = "dgvPedidosPendientes";
		this.dgvPedidosPendientes.ReadOnly = true;
		this.dgvPedidosPendientes.RowHeadersVisible = false;
		this.dgvPedidosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPedidosPendientes.Size = new System.Drawing.Size(959, 296);
		this.dgvPedidosPendientes.TabIndex = 0;
		this.dgvPedidosPendientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPedidosPendientes_CellDoubleClick);
		this.documento.DataPropertyName = "documento";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle5;
		this.documento.HeaderText = "Número de Orden";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Width = 150;
		this.codigo.DataPropertyName = "codPedido";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Visible = false;
		this.codigo.Width = 80;
		this.Numeracion.DataPropertyName = "numeracion";
		this.Numeracion.HeaderText = "Numeracion";
		this.Numeracion.Name = "Numeracion";
		this.Numeracion.ReadOnly = true;
		this.Numeracion.Visible = false;
		this.Pedido_.DataPropertyName = "pedido";
		this.Pedido_.HeaderText = "Pedido";
		this.Pedido_.Name = "Pedido_";
		this.Pedido_.ReadOnly = true;
		this.Pedido_.Visible = false;
		this.RazonSocial.DataPropertyName = "cliente";
		this.RazonSocial.HeaderText = "Cliente";
		this.RazonSocial.Name = "RazonSocial";
		this.RazonSocial.ReadOnly = true;
		this.RazonSocial.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.RazonSocial.Width = 400;
		this.cliente.DataPropertyName = "clientebole";
		this.cliente.HeaderText = "cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Visible = false;
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle6;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 120;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 250;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(959, 382);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmOrdenesdeVenta";
		this.Text = "Ordenes de Venta";
		base.Load += new System.EventHandler(frmOrdenesdeVenta_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPedidosPendientes).EndInit();
		base.ResumeLayout(false);
	}
}
