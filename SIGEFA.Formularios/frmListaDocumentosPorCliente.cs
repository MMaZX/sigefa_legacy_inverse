using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmListaDocumentosPorCliente : Office2007Form
{
	public clsFacturaVenta venta = new clsFacturaVenta();

	public int CodCliente = 0;

	private clsAdmFacturaVenta Admventa = new clsAdmFacturaVenta();

	private clsAdmNotaSalida AdmNota = new clsAdmNotaSalida();

	private clsNotaSalida nota = new clsNotaSalida();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int tipo;

	private IContainer components = null;

	private DataGridView dgvDocumentos;

	private Button btnAceptar;

	private ImageList imageList1;

	private DataGridViewTextBoxColumn codnota;

	private DataGridViewTextBoxColumn fechasalida;

	private DataGridViewTextBoxColumn sigla;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn empresa;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn sucursal;

	public frmListaDocumentosPorCliente()
	{
		InitializeComponent();
	}

	private void frmListaDocumentosSinGuia_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		dgvDocumentos.DataSource = data;
		data.DataSource = AdmNota.DocumentosPorCliente(CodCliente, tipo);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDocumentos.ClearSelection();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmListaDocumentosSinGuia_Shown(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvDocumentos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		Close();
	}

	private void dgvDocumentos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		Close();
	}

	private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.SelectedRows.Count > 0 && e.RowIndex != -1)
		{
			venta.CodFacturaVenta = dgvDocumentos.Rows[e.RowIndex].Cells[codnota.Name].Value.ToString();
			venta.CodAlmacen = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells[almacen.Name].Value);
			venta.CodEmpresa = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells[empresa.Name].Value);
			venta.CodSucursal = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells[sucursal.Name].Value);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaDocumentosPorCliente));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dgvDocumentos = new System.Windows.Forms.DataGridView();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.codnota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.empresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).BeginInit();
		base.SuspendLayout();
		this.dgvDocumentos.AllowUserToAddRows = false;
		this.dgvDocumentos.AllowUserToDeleteRows = false;
		this.dgvDocumentos.AllowUserToResizeColumns = false;
		this.dgvDocumentos.AllowUserToResizeRows = false;
		this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDocumentos.Columns.AddRange(this.codnota, this.fechasalida, this.sigla, this.numdoc, this.empresa, this.almacen, this.sucursal);
		this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Top;
		this.dgvDocumentos.Location = new System.Drawing.Point(0, 0);
		this.dgvDocumentos.MultiSelect = false;
		this.dgvDocumentos.Name = "dgvDocumentos";
		this.dgvDocumentos.ReadOnly = true;
		this.dgvDocumentos.RowHeadersVisible = false;
		this.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDocumentos.Size = new System.Drawing.Size(365, 285);
		this.dgvDocumentos.TabIndex = 0;
		this.dgvDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellClick);
		this.dgvDocumentos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellDoubleClick);
		this.dgvDocumentos.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDocumentos_CellMouseDoubleClick);
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(276, 291);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 5;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.codnota.DataPropertyName = "codigo";
		this.codnota.HeaderText = "codigo";
		this.codnota.Name = "codnota";
		this.codnota.ReadOnly = true;
		this.codnota.Visible = false;
		this.fechasalida.DataPropertyName = "fecha";
		dataGridViewCellStyle1.Format = "d";
		dataGridViewCellStyle1.NullValue = null;
		this.fechasalida.DefaultCellStyle = dataGridViewCellStyle1;
		this.fechasalida.HeaderText = "Fecha";
		this.fechasalida.Name = "fechasalida";
		this.fechasalida.ReadOnly = true;
		this.sigla.DataPropertyName = "sigla";
		this.sigla.HeaderText = "Tipo Doc.";
		this.sigla.Name = "sigla";
		this.sigla.ReadOnly = true;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N° Documento";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.numdoc.Width = 150;
		this.empresa.DataPropertyName = "empresa";
		this.empresa.HeaderText = "codempresa";
		this.empresa.Name = "empresa";
		this.empresa.ReadOnly = true;
		this.empresa.Visible = false;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "codalmacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.Visible = false;
		this.sucursal.DataPropertyName = "sucursal";
		this.sucursal.HeaderText = "codsucursal";
		this.sucursal.Name = "sucursal";
		this.sucursal.ReadOnly = true;
		this.sucursal.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnAceptar;
		base.ClientSize = new System.Drawing.Size(365, 328);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.dgvDocumentos);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmListaDocumentosPorCliente";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Documentos Sin Guia";
		base.Load += new System.EventHandler(frmListaDocumentosSinGuia_Load);
		base.Shown += new System.EventHandler(frmListaDocumentosSinGuia_Shown);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).EndInit();
		base.ResumeLayout(false);
	}
}
