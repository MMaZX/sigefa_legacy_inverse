using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListaFacturasPorProveedor : Office2007Form
{
	public clsNotaIngreso nota = new clsNotaIngreso();

	public int CodProveedor = 0;

	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsAdmFactura AdmFactura = new clsAdmFactura();

	public clsFactura factura = new clsFactura();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public List<int> seleccion = new List<int>();

	public int tipo = 0;

	public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();

	private IContainer components = null;

	private Button btnAceptar;

	private ImageList imageList1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private RadGridView rgvDetalle;

	public frmListaFacturasPorProveedor()
	{
		InitializeComponent();
	}

	private void frmListaDocumentosSinGuia_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		rgvDetalle.DataSource = data;
		data.DataSource = AdmFactura.MuestraFacturasProveedor(frmLogin.iCodAlmacen, CodProveedor, tipo);
		data.Filter = string.Empty;
		filtro = string.Empty;
		rgvDetalle.ClearSelection();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		RecorreDetalle();
		Close();
	}

	private void frmListaFacturasPorProveedor_Shown(object sender, EventArgs e)
	{
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
	}

	private void CargaDetalle()
	{
	}

	private void RecorreDetalle()
	{
	}

	private void añadedetalle(DataGridViewRow row)
	{
	}

	private void rgvDetalle_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvDetalle.Rows.Count > 0 && e.RowIndex > -1)
		{
			factura.CodFactura = Convert.ToInt32(rgvDetalle.Rows[e.RowIndex].Cells["codFactura"].Value.ToString());
			factura.DocumentoFactura = rgvDetalle.Rows[e.RowIndex].Cells["documento"].Value.ToString();
		}
	}

	private void rgvDetalle_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaFacturasPorProveedor));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.rgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(578, 3);
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
		this.groupBox2.Controls.Add(this.btnAceptar);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 305);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(691, 40);
		this.groupBox2.TabIndex = 8;
		this.groupBox2.TabStop = false;
		this.groupBox3.Controls.Add(this.rgvDetalle);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(691, 305);
		this.groupBox3.TabIndex = 10;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Facturas";
		this.rgvDetalle.AutoScroll = true;
		this.rgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.rgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.rgvDetalle.MasterTemplate.AllowDeleteRow = false;
		this.rgvDetalle.MasterTemplate.AllowDragToGroup = false;
		this.rgvDetalle.MasterTemplate.AllowEditRow = false;
		this.rgvDetalle.MasterTemplate.AutoGenerateColumns = false;
		this.rgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codFactura";
		gridViewTextBoxColumn1.HeaderText = "codigo";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codFactura";
		gridViewTextBoxColumn2.DataType = typeof(System.DateTime);
		gridViewTextBoxColumn2.FieldName = "fecha";
		gridViewTextBoxColumn2.FormatInfo = new System.Globalization.CultureInfo("es-PE");
		gridViewTextBoxColumn2.HeaderText = "Fecha";
		gridViewTextBoxColumn2.Name = "fechasalida";
		gridViewTextBoxColumn2.Width = 114;
		gridViewTextBoxColumn3.FieldName = "ruc";
		gridViewTextBoxColumn3.HeaderText = "RUC";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "ruc";
		gridViewTextBoxColumn4.FieldName = "razonsocial";
		gridViewTextBoxColumn4.HeaderText = "Razon Social";
		gridViewTextBoxColumn4.Multiline = true;
		gridViewTextBoxColumn4.Name = "razonsocial";
		gridViewTextBoxColumn4.Width = 114;
		gridViewTextBoxColumn5.FieldName = "documento";
		gridViewTextBoxColumn5.HeaderText = "Documento";
		gridViewTextBoxColumn5.Name = "documento";
		gridViewTextBoxColumn5.Width = 114;
		gridViewTextBoxColumn6.FieldName = "responsable";
		gridViewTextBoxColumn6.HeaderText = "Responable";
		gridViewTextBoxColumn6.Name = "responsable";
		gridViewTextBoxColumn6.Width = 114;
		gridViewTextBoxColumn7.FieldName = "comentario";
		gridViewTextBoxColumn7.HeaderText = "Comentario";
		gridViewTextBoxColumn7.Name = "comentario";
		gridViewTextBoxColumn7.Width = 114;
		gridViewTextBoxColumn8.FieldName = "Estado";
		gridViewTextBoxColumn8.HeaderText = "Estado";
		gridViewTextBoxColumn8.Name = "estado";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 119;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.EnableGrouping = false;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.Size = new System.Drawing.Size(685, 286);
		this.rgvDetalle.TabIndex = 11;
		this.rgvDetalle.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDetalle_CellClick);
		this.rgvDetalle.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDetalle_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnAceptar;
		base.ClientSize = new System.Drawing.Size(691, 345);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmListaFacturasPorProveedor";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Facturas de Compra";
		base.Load += new System.EventHandler(frmListaDocumentosSinGuia_Load);
		base.Shown += new System.EventHandler(frmListaFacturasPorProveedor_Shown);
		this.groupBox2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
