using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmProductosStockMin : Office2007Form
{
	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsSerie ser = new clsSerie();

	private clsAdmAlmacen Admalmac = new clsAdmAlmacen();

	private clsAdmTipoArticulo AdmTip = new clsAdmTipoArticulo();

	public int codalmacen = 0;

	private IContainer components = null;

	private Label label1;

	private DataGridView dgvProductos;

	private ImageList imageList1;

	private Button btnAceptar;

	private DateTimePicker dtpFechaPago;

	private Button button1;

	private ComboBox cbTipoArticulo;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn stockdisponible;

	private DataGridViewTextBoxColumn stockminimo;

	private DataGridViewTextBoxColumn nombre;

	public frmProductosStockMin()
	{
		InitializeComponent();
	}

	private void CargaTipoArticulos()
	{
		cbTipoArticulo.DataSource = AdmTip.MuestraTipoArticulos();
		cbTipoArticulo.DisplayMember = "descripcion";
		cbTipoArticulo.ValueMember = "codTipoArticulo";
		cbTipoArticulo.SelectedIndex = 0;
	}

	private void frmProductosLista_Load(object sender, EventArgs e)
	{
		CargaTipoArticulos();
		int codTipArt = int.Parse(cbTipoArticulo.SelectedValue.ToString());
		dgvProductos.DataSource = Admalmac.RelacionProductosStockMin(codTipArt, codalmacen);
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmProductosLista_FormClosing(object sender, FormClosingEventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		clsReporteKardex dso = new clsReporteKardex();
		CRStockAgotar rpt = new CRStockAgotar();
		frmRptKardex frm = new frmRptKardex();
		int codTipArt = int.Parse(cbTipoArticulo.SelectedValue.ToString());
		PrintOptions rptoption = rpt.PrintOptions;
		rptoption.PrinterName = ser.NombreImpresora;
		rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
		rpt.SetDataSource(dso.StockPorAgotar(codTipArt, codalmacen).Tables[0]);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductosStockMin));
		this.label1 = new System.Windows.Forms.Label();
		this.dgvProductos = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockdisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockminimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.button1 = new System.Windows.Forms.Button();
		this.cbTipoArticulo = new System.Windows.Forms.ComboBox();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(509, 14);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Fecha";
		this.dgvProductos.AllowUserToAddRows = false;
		this.dgvProductos.AllowUserToDeleteRows = false;
		this.dgvProductos.AllowUserToResizeColumns = false;
		this.dgvProductos.AllowUserToResizeRows = false;
		this.dgvProductos.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProductos.Columns.AddRange(this.codigo, this.referencia, this.descripcion, this.codunidad, this.unidad, this.stockdisponible, this.stockminimo, this.nombre);
		this.dgvProductos.Location = new System.Drawing.Point(13, 40);
		this.dgvProductos.Name = "dgvProductos";
		this.dgvProductos.ReadOnly = true;
		this.dgvProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvProductos.RowHeadersVisible = false;
		this.dgvProductos.RowHeadersWidth = 40;
		this.dgvProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProductos.Size = new System.Drawing.Size(648, 164);
		this.dgvProductos.StandardTab = true;
		this.dgvProductos.TabIndex = 3;
		this.codigo.DataPropertyName = "codProducto";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 80;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.descripcion.Width = 300;
		this.codunidad.DataPropertyName = "codunidad";
		this.codunidad.HeaderText = "codunidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unid.Medida";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.stockdisponible.DataPropertyName = "stockdisponible";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		dataGridViewCellStyle1.NullValue = null;
		this.stockdisponible.DefaultCellStyle = dataGridViewCellStyle1;
		this.stockdisponible.HeaderText = "Stock";
		this.stockdisponible.Name = "stockdisponible";
		this.stockdisponible.ReadOnly = true;
		this.stockdisponible.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.stockdisponible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.stockdisponible.Width = 60;
		this.stockminimo.DataPropertyName = "stockminimo";
		this.stockminimo.HeaderText = "Stockminimo";
		this.stockminimo.Name = "stockminimo";
		this.stockminimo.ReadOnly = true;
		this.nombre.DataPropertyName = "nombre";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		dataGridViewCellStyle2.NullValue = null;
		this.nombre.DefaultCellStyle = dataGridViewCellStyle2;
		this.nombre.HeaderText = "Almacen";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Visible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(580, 216);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 4;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(561, 11);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(96, 20);
		this.dtpFechaPago.TabIndex = 80;
		this.dtpFechaPago.Tag = "16";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 3;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(489, 216);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(85, 32);
		this.button1.TabIndex = 83;
		this.button1.Text = "Reporte";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.cbTipoArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbTipoArticulo.Enabled = false;
		this.cbTipoArticulo.FormattingEnabled = true;
		this.cbTipoArticulo.Location = new System.Drawing.Point(367, 105);
		this.cbTipoArticulo.Name = "cbTipoArticulo";
		this.cbTipoArticulo.Size = new System.Drawing.Size(180, 21);
		this.cbTipoArticulo.TabIndex = 81;
		this.cbTipoArticulo.Tag = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(672, 260);
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
		base.Name = "frmProductosStockMin";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Productos por Agotar Stock";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmProductosLista_FormClosing);
		base.Load += new System.EventHandler(frmProductosLista_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
