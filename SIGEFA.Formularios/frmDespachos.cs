using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmDespachos : Office2007Form
{
	private clsReporteFactura ds = new clsReporteFactura();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int codfactura = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvVentas;

	private GroupBox groupBox2;

	private Button btnimprimir;

	private ImageList imageList1;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewAutoFilterTextBoxColumn fecha;

	private DataGridViewAutoFilterTextBoxColumn documento;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn CodDespacho;

	private DataGridViewTextBoxColumn FechaDespacho;

	private DataGridViewAutoFilterTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewAutoFilterTextBoxColumn formapago;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewAutoFilterTextBoxColumn estado;

	private DataGridViewAutoFilterTextBoxColumn impreso;

	public frmDespachos()
	{
		InitializeComponent();
	}

	private void frmDespachos_Load(object sender, EventArgs e)
	{
		cargalista();
	}

	private void cargalista()
	{
		dgvVentas.DataSource = data;
		data.DataSource = AdmVenta.despachosxventa(codfactura);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvVentas.ClearSelection();
	}

	private void btnimprimir_Click(object sender, EventArgs e)
	{
		try
		{
			if (frmLogin.iCodAlmacen != 0)
			{
				frmRptFactura frm = new frmRptFactura();
				CRSalidaDespacho rpt = new CRSalidaDespacho();
				rpt.Load("CRSalidaDespacho.rpt");
				rpt.SetDataSource(ds.salidadespacho(codfactura, Convert.ToInt32(dgvVentas.CurrentRow.Cells[CodDespacho.Name].Value)));
				frm.crvReporteFactura.ReportSource = rpt;
				frm.ShowDialog();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmDespachos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvVentas = new System.Windows.Forms.DataGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnimprimir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.documento = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodDespacho = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.FechaDespacho = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formapago = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.impreso = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvVentas);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(832, 275);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Despachos";
		this.dgvVentas.AllowUserToAddRows = false;
		this.dgvVentas.AllowUserToDeleteRows = false;
		this.dgvVentas.AllowUserToResizeRows = false;
		this.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvVentas.Columns.AddRange(this.codigo, this.fecha, this.documento, this.numdoc, this.CodDespacho, this.FechaDespacho, this.codcliente, this.cliente, this.moneda, this.importe, this.formapago, this.fechapago, this.estado, this.impreso);
		this.dgvVentas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVentas.Location = new System.Drawing.Point(3, 16);
		this.dgvVentas.MultiSelect = false;
		this.dgvVentas.Name = "dgvVentas";
		this.dgvVentas.RowHeadersVisible = false;
		this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVentas.Size = new System.Drawing.Size(826, 256);
		this.dgvVentas.TabIndex = 1;
		this.groupBox2.Controls.Add(this.btnimprimir);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 271);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(832, 52);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.btnimprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnimprimir.ImageKey = "document_print.png";
		this.btnimprimir.ImageList = this.imageList1;
		this.btnimprimir.Location = new System.Drawing.Point(745, 10);
		this.btnimprimir.Name = "btnimprimir";
		this.btnimprimir.Size = new System.Drawing.Size(75, 30);
		this.btnimprimir.TabIndex = 0;
		this.btnimprimir.Text = "Imprimir";
		this.btnimprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnimprimir.UseVisualStyleBackColor = true;
		this.btnimprimir.Click += new System.EventHandler(btnimprimir_Click);
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
		this.codigo.DataPropertyName = "codFactura";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Width = 80;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha Compra";
		this.fecha.Name = "fecha";
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 80;
		this.documento.DataPropertyName = "documento";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle5;
		this.documento.HeaderText = "T. doc.";
		this.documento.Name = "documento";
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Width = 60;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N° Documento";
		this.numdoc.Name = "numdoc";
		this.CodDespacho.DataPropertyName = "CodDespacho";
		this.CodDespacho.HeaderText = "Cod. Despacho";
		this.CodDespacho.Name = "CodDespacho";
		this.FechaDespacho.DataPropertyName = "FechaDespacho";
		this.FechaDespacho.HeaderText = "Fecha Despacho";
		this.FechaDespacho.Name = "FechaDespacho";
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
		this.moneda.Visible = false;
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle6;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.Visible = false;
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(832, 323);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmDespachos";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Text = "Despachos por Venta";
		base.Load += new System.EventHandler(frmDespachos_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
