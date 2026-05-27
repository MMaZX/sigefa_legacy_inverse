using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmVentasPendientesdeDespacho : Office2007Form
{
	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida nota = new clsNotaSalida();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsPago pag = new clsPago();

	private clsAdmPago admPago = new clsAdmPago();

	private clsDocumentorescom docres = new clsDocumentorescom();

	private clsDetalleDocumentoRescom detres = new clsDetalleDocumentoRescom();

	private clsAdmDocumentoeRescom admdocres = new clsAdmDocumentoeRescom();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoDocumento admdoc = new clsAdmTipoDocumento();

	private clsSerie ser = new clsSerie();

	private List<clsFacturaVenta> listadocumentos = new List<clsFacturaVenta>();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private int CodTipoDoc = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvVentas;

	private Button btnSalir;

	private ImageList imageList1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnReporte;

	private Button btnActualizarLista;

	private ImageList imageList2;

	private Button btnIrPedido;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewAutoFilterTextBoxColumn fecha;

	private DataGridViewAutoFilterTextBoxColumn documento;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewAutoFilterTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewAutoFilterTextBoxColumn formapago;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewAutoFilterTextBoxColumn estado;

	private DataGridViewAutoFilterTextBoxColumn impreso;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MuestraDespachosToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	public frmVentasPendientesdeDespacho()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		dgvVentas.DataSource = data;
		data.DataSource = AdmVenta.VentasPendientesdedespacho(frmLogin.iCodAlmacen, dtpDesde.Value, dtpHasta.Value);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvVentas.ClearSelection();
		dgvVentas.ReadOnly = false;
	}

	private void frmPedidosPendientes_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvPedidosPendientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvVentas.Rows.Count >= 1 && e.Row.Selected)
		{
			venta.CodFacturaVenta = e.Row.Cells[codigo.Name].Value.ToString();
		}
	}

	private void dtpDesde_CloseUp(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_CloseUp(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnGenerarComunicaciondeBaja_Click(object sender, EventArgs e)
	{
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
	}

	private void dgvVentas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		if (dgvVentas.IsCurrentCellDirty)
		{
			dgvVentas.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
	}

	private void btnActualizarLista_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow != null)
		{
			DataGridViewRow row = dgvVentas.CurrentRow;
			if (dgvVentas.Rows.Count >= 1)
			{
				frmVentaxEentregar form = new frmVentaxEentregar();
				form.MdiParent = base.MdiParent;
				form.CodVenta = venta.CodFacturaVenta;
				form.Proceso = 3;
				form.Show();
			}
			CargaLista();
		}
	}

	private void muestraPagosToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvVentas.SelectedRows[0];
		venta.CodFacturaVenta = Row.Cells[codigo.Name].Value.ToString();
		decimal totalM = Convert.ToDecimal(Row.Cells[importe.Name].Value.ToString());
		frmDespachos form = new frmDespachos();
		form.codfactura = Convert.ToInt32(venta.CodFacturaVenta);
		form.ShowDialog();
	}

	private void dgvVentas_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		dgvVentas.ContextMenuStrip = new ContextMenuStrip();
		if (e.RowIndex == -1)
		{
			return;
		}
		dgvVentas.Rows[e.RowIndex].Selected = true;
		if (e.Button == MouseButtons.Right && e.RowIndex != -1 && dgvVentas.SelectedCells.Count > 0)
		{
			dgvVentas.ContextMenuStrip = contextMenuStrip1;
			if (Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells[codigo.Name].Value) > 0)
			{
				MuestraDespachosToolStripMenuItem.Enabled = true;
			}
		}
	}

	private void dgvVentas_Click(object sender, EventArgs e)
	{
		if (dgvVentas.RowCount > 0)
		{
			if (dgvVentas.CurrentRow.Cells[estado.Name].Value.ToString() == "PENDIENTE")
			{
				btnIrPedido.Enabled = true;
			}
			else if (dgvVentas.CurrentRow.Cells[estado.Name].Value.ToString() == "ATENDIDO")
			{
				btnIrPedido.Enabled = false;
			}
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVentasPendientesdeDespacho));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvVentas = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.documento = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formapago = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.impreso = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnActualizarLista = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnIrPedido = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MuestraDespachosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvVentas);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(881, 330);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Ventas Pendientes de Despacho";
		this.dgvVentas.AllowUserToAddRows = false;
		this.dgvVentas.AllowUserToDeleteRows = false;
		this.dgvVentas.AllowUserToResizeRows = false;
		this.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvVentas.Columns.AddRange(this.codigo, this.fecha, this.documento, this.numdoc, this.codcliente, this.cliente, this.moneda, this.importe, this.formapago, this.fechapago, this.estado, this.impreso);
		this.dgvVentas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVentas.Location = new System.Drawing.Point(3, 16);
		this.dgvVentas.MultiSelect = false;
		this.dgvVentas.Name = "dgvVentas";
		this.dgvVentas.RowHeadersVisible = false;
		this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVentas.Size = new System.Drawing.Size(875, 311);
		this.dgvVentas.TabIndex = 0;
		this.dgvVentas.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvVentas_CellMouseDown);
		this.dgvVentas.CurrentCellDirtyStateChanged += new System.EventHandler(dgvVentas_CurrentCellDirtyStateChanged);
		this.dgvVentas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvPedidosPendientes_RowStateChanged);
		this.dgvVentas.Click += new System.EventHandler(dgvVentas_Click);
		this.codigo.DataPropertyName = "codFactura";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Visible = false;
		this.codigo.Width = 80;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 80;
		this.documento.DataPropertyName = "documento";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle3;
		this.documento.HeaderText = "T. doc.";
		this.documento.Name = "documento";
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Width = 60;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N° Documento";
		this.numdoc.Name = "numdoc";
		this.codcliente.DataPropertyName = "codcliente";
		this.codcliente.HeaderText = "Cod. Cliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.codcliente.Visible = false;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cliente.Width = 270;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle4;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.formapago.DataPropertyName = "formapago";
		this.formapago.HeaderText = "F. pago";
		this.formapago.Name = "formapago";
		this.formapago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.formapago.Visible = false;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "Fech. Pago";
		this.fechapago.Name = "fechapago";
		this.fechapago.Visible = false;
		this.estado.DataPropertyName = "anulado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.impreso.DataPropertyName = "impreso";
		this.impreso.HeaderText = "Impreso";
		this.impreso.Name = "impreso";
		this.impreso.Visible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "document_print.png");
		this.imageList1.Images.SetKeyName(8, "docbaj.png");
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(158, 350);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(-15, 350);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(38, 347);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 15;
		this.dtpDesde.CloseUp += new System.EventHandler(dtpDesde_CloseUp);
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(205, 347);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 14;
		this.dtpHasta.CloseUp += new System.EventHandler(dtpHasta_CloseUp);
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
		this.imageList2.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList2.Images.SetKeyName(19, "img_visto.jpg");
		this.btnActualizarLista.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnActualizarLista.ImageIndex = 8;
		this.btnActualizarLista.ImageList = this.imageList2;
		this.btnActualizarLista.Location = new System.Drawing.Point(322, 338);
		this.btnActualizarLista.Name = "btnActualizarLista";
		this.btnActualizarLista.Size = new System.Drawing.Size(93, 37);
		this.btnActualizarLista.TabIndex = 31;
		this.btnActualizarLista.Text = "Actualizar Lista";
		this.btnActualizarLista.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnActualizarLista.UseVisualStyleBackColor = true;
		this.btnActualizarLista.Click += new System.EventHandler(btnActualizarLista_Click);
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.ImageIndex = 7;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(599, 336);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(90, 37);
		this.btnReporte.TabIndex = 30;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(794, 336);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnIrPedido.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrPedido.Enabled = false;
		this.btnIrPedido.ImageIndex = 1;
		this.btnIrPedido.ImageList = this.imageList1;
		this.btnIrPedido.Location = new System.Drawing.Point(695, 336);
		this.btnIrPedido.Name = "btnIrPedido";
		this.btnIrPedido.Size = new System.Drawing.Size(93, 37);
		this.btnIrPedido.TabIndex = 32;
		this.btnIrPedido.Text = "Consulta";
		this.btnIrPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrPedido.UseVisualStyleBackColor = true;
		this.btnIrPedido.Click += new System.EventHandler(btnIrPedido_Click);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.MuestraDespachosToolStripMenuItem, this.toolStripSeparator1 });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(166, 32);
		this.MuestraDespachosToolStripMenuItem.Name = "MuestraDespachosToolStripMenuItem";
		this.MuestraDespachosToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
		this.MuestraDespachosToolStripMenuItem.Text = "Muestra Entregas";
		this.MuestraDespachosToolStripMenuItem.Click += new System.EventHandler(muestraPagosToolStripMenuItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(881, 385);
		base.Controls.Add(this.btnIrPedido);
		base.Controls.Add(this.btnActualizarLista);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVentasPendientesdeDespacho";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ventas Pendientes de Despacho";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPedidosPendientes_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
