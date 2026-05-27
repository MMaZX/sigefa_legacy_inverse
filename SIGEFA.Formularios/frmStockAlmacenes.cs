using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls;
using Telerik.WinControls.Export;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmStockAlmacenes : Office2007Form
{
	private clsAdmProducto admPro = new clsAdmProducto();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsProducto pro = new clsProducto();

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private TextBox txtFiltro;

	private Label label3;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private DataGridView dgvDetalle_1;

	private ImageList imageList1;

	private RadButton btnActualizar;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private GroupBox groupBox3;

	private System.Windows.Forms.TabControl tabControl1;

	private TabPage tabPage2;

	private RadGridView dgvPVenta;

	private MaterialTheme materialTheme1;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn producto;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn marca;

	private DataGridViewTextBoxColumn familia;

	private DataGridViewTextBoxColumn Proveedor;

	private RadButton btnReporte;

	private RadGridView dgvDetalle;

	public frmStockAlmacenes()
	{
		InitializeComponent();
	}

	private void CargaStock()
	{
		try
		{
			data.DataSource = null;
			dgvDetalle.DataSource = data;
			data.DataSource = admPro.MuestraStockAlmacenes();
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvDetalle.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void ajustarColumnas()
	{
		foreach (GridViewDataColumn co in dgvDetalle.Columns)
		{
			if (co.Index > 7)
			{
				co.Width = 155;
			}
		}
	}

	private void frmStockAlmacenes_Load(object sender, EventArgs e)
	{
		try
		{
			CargaStock();
			label2.Text = "producto";
			label3.Text = "producto";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void frmStockAlmacenes_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
		ajustarColumnas();
	}

	private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtFiltro.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltro.Text != "")
				{
					string filterCod = txtFiltro.Text;
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					if (cad.Count() > 1)
					{
						string[] array = cad;
						foreach (string c in array)
						{
							if (cont == 1)
							{
								queries.Add($"[{label2.Text}] LIKE '%{c}%'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"[{label2.Text}] LIKE '%{c}%'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"[{label2.Text}] LIKE '%{filterCod}%'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		CargaStock();
		ajustarColumnas();
	}

	private void CargaListaEquivalencias()
	{
		dgvPVenta.DataSource = admPro.MuestraUnidadesEquivalentesVenta1(pro.CodProducto, frmLogin.iCodAlmacen);
		dgvPVenta.ClearSelection();
	}

	private void dgvDetalle_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.Row == dgvDetalle.MasterView.TableHeaderRow && e.ColumnIndex > -1)
		{
			label2.Text = e.Column.Name;
			label3.Text = e.Column.Name;
		}
		else if (dgvDetalle.RowCount > 0 && e.RowIndex != -1)
		{
			pro.CodProducto = Convert.ToInt32(e.Row.Cells["codproducto"].Value);
			CargaListaEquivalencias();
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(dgvDetalle);
		spreadStreamExport.ExportVisualSettings = true;
		spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
		string ruta = AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx";
		if (File.Exists(ruta))
		{
			File.Delete(ruta);
		}
		spreadStreamExport.RunExport(AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx", new SpreadStreamExportRenderer());
		FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx");
		if (fi.Exists)
		{
			Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx");
		}
	}

	private void dgvDetalle_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
	{
		e.CellElement.BorderLeftWidth = 1f;
		e.CellElement.BorderRightWidth = 1f;
		e.CellElement.BorderTopWidth = 1f;
		e.CellElement.BorderBottomWidth = 1f;
	}

	private void dgvDetalle_RowFormatting(object sender, RowFormattingEventArgs e)
	{
		if (e.RowElement is GridTableHeaderRowElement)
		{
			e.RowElement.RowInfo.Height = 30;
		}
		if (Convert.ToInt32(e.RowElement.RowInfo.Cells["estado"].Value) == 0)
		{
			e.RowElement.DrawFill = true;
			e.RowElement.GradientStyle = GradientStyles.Solid;
			e.RowElement.BackColor = Color.LightCoral;
		}
		else
		{
			e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
			e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
			e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmStockAlmacenes));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnReporte = new Telerik.WinControls.UI.RadButton();
		this.btnActualizar = new Telerik.WinControls.UI.RadButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.dgvDetalle_1 = new System.Windows.Forms.DataGridView();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.familia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dgvPVenta = new Telerik.WinControls.UI.RadGridView();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnReporte).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnActualizar).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle_1).BeginInit();
		this.groupBox3.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPVenta).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvPVenta.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(6, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 20);
		this.label1.TabIndex = 1;
		this.label1.Text = "Filtro Por : ";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(92, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(18, 20);
		this.label2.TabIndex = 2;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(187, 16);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(399, 20);
		this.txtFiltro.TabIndex = 3;
		this.txtFiltro.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(592, 21);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.btnActualizar);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(925, 57);
		this.groupBox1.TabIndex = 5;
		this.groupBox1.TabStop = false;
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.Image = SIGEFA.Properties.Resources.printer;
		this.btnReporte.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnReporte.Location = new System.Drawing.Point(854, 15);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(43, 36);
		this.btnReporte.TabIndex = 6;
		this.btnReporte.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.ThemeName = "Material";
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.Image = SIGEFA.Properties.Resources.cambio;
		this.btnActualizar.Location = new System.Drawing.Point(696, 15);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(135, 36);
		this.btnActualizar.TabIndex = 5;
		this.btnActualizar.Text = "Actualizar";
		this.btnActualizar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnActualizar.ThemeName = "Material";
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Controls.Add(this.dgvDetalle_1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 57);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(925, 339);
		this.groupBox2.TabIndex = 6;
		this.groupBox2.TabStop = false;
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.dgvDetalle.MasterTemplate.AllowDragToGroup = false;
		this.dgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codProducto";
		gridViewTextBoxColumn1.HeaderText = "codProducto";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codProducto";
		gridViewTextBoxColumn1.Width = 131;
		gridViewTextBoxColumn2.FieldName = "referencia";
		gridViewTextBoxColumn2.HeaderText = "Referencia";
		gridViewTextBoxColumn2.Name = "referencia";
		gridViewTextBoxColumn2.Width = 67;
		gridViewTextBoxColumn3.FieldName = "producto";
		gridViewTextBoxColumn3.HeaderText = "Producto";
		gridViewTextBoxColumn3.Multiline = true;
		gridViewTextBoxColumn3.Name = "producto";
		gridViewTextBoxColumn3.Width = 166;
		gridViewTextBoxColumn3.WrapText = true;
		gridViewTextBoxColumn4.FieldName = "unidad";
		gridViewTextBoxColumn4.HeaderText = "Unidad";
		gridViewTextBoxColumn4.Name = "colunidad";
		gridViewTextBoxColumn4.Width = 44;
		gridViewTextBoxColumn5.FieldName = "estado";
		gridViewTextBoxColumn5.HeaderText = "Estado";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "estado";
		gridViewTextBoxColumn5.Width = 153;
		gridViewTextBoxColumn6.FieldName = "marca";
		gridViewTextBoxColumn6.HeaderText = "Marca";
		gridViewTextBoxColumn6.Multiline = true;
		gridViewTextBoxColumn6.Name = "marca";
		gridViewTextBoxColumn6.Width = 96;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "familia";
		gridViewTextBoxColumn7.HeaderText = "Familia";
		gridViewTextBoxColumn7.Multiline = true;
		gridViewTextBoxColumn7.Name = "familia";
		gridViewTextBoxColumn7.Width = 96;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "proveedor";
		gridViewTextBoxColumn8.HeaderText = "Proveedor";
		gridViewTextBoxColumn8.Multiline = true;
		gridViewTextBoxColumn8.Name = "proveedor";
		gridViewTextBoxColumn8.Width = 403;
		gridViewTextBoxColumn8.WrapText = true;
		gridViewTextBoxColumn9.FieldName = "situacion";
		gridViewTextBoxColumn9.HeaderText = "Situación";
		gridViewTextBoxColumn9.Name = "situacion";
		gridViewTextBoxColumn9.Width = 47;
		this.dgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9);
		this.dgvDetalle.MasterTemplate.EnableGrouping = false;
		this.dgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.ShowGroupPanel = false;
		this.dgvDetalle.Size = new System.Drawing.Size(919, 320);
		this.dgvDetalle.TabIndex = 3;
		this.dgvDetalle.ThemeName = "Material";
		this.dgvDetalle.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(dgvDetalle_RowFormatting);
		this.dgvDetalle.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(dgvDetalle_CellFormatting);
		this.dgvDetalle.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle_1.AllowUserToAddRows = false;
		this.dgvDetalle_1.AllowUserToDeleteRows = false;
		this.dgvDetalle_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle_1.Columns.AddRange(this.codproducto, this.referencia, this.producto, this.estado, this.marca, this.familia, this.Proveedor);
		this.dgvDetalle_1.Location = new System.Drawing.Point(254, 219);
		this.dgvDetalle_1.Name = "dgvDetalle_1";
		this.dgvDetalle_1.ReadOnly = true;
		this.dgvDetalle_1.RowHeadersVisible = false;
		this.dgvDetalle_1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle_1.Size = new System.Drawing.Size(60, 80);
		this.dgvDetalle_1.TabIndex = 2;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.producto.DataPropertyName = "producto";
		this.producto.HeaderText = "Producto";
		this.producto.Name = "producto";
		this.producto.ReadOnly = true;
		this.producto.Width = 300;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.marca.DataPropertyName = "marca";
		this.marca.HeaderText = "Marca";
		this.marca.Name = "marca";
		this.marca.ReadOnly = true;
		this.familia.DataPropertyName = "familia";
		this.familia.HeaderText = "Familia";
		this.familia.Name = "familia";
		this.familia.ReadOnly = true;
		this.Proveedor.DataPropertyName = "proveedor";
		this.Proveedor.HeaderText = "Proveedor";
		this.Proveedor.Name = "Proveedor";
		this.Proveedor.ReadOnly = true;
		this.Proveedor.Width = 220;
		this.groupBox3.Controls.Add(this.tabControl1);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 396);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(925, 140);
		this.groupBox3.TabIndex = 7;
		this.groupBox3.TabStop = false;
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(3, 16);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(919, 121);
		this.tabControl1.TabIndex = 0;
		this.tabPage2.Controls.Add(this.dgvPVenta);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(911, 95);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Precios";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dgvPVenta.AutoSizeRows = true;
		this.dgvPVenta.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvPVenta.Location = new System.Drawing.Point(3, 3);
		this.dgvPVenta.MasterTemplate.AllowAddNewRow = false;
		this.dgvPVenta.MasterTemplate.AllowDragToGroup = false;
		this.dgvPVenta.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn10.FieldName = "codunidadequivalente";
		gridViewTextBoxColumn10.HeaderText = "codunidadequivalente";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "codunidadequivalente";
		gridViewTextBoxColumn11.FieldName = "codunidadmedida";
		gridViewTextBoxColumn11.HeaderText = "codunidadmedida";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "codunidadmedida";
		gridViewTextBoxColumn12.FieldName = "descripcion";
		gridViewTextBoxColumn12.HeaderText = "Descripcion";
		gridViewTextBoxColumn12.Name = "descripcion";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 302;
		gridViewTextBoxColumn13.FieldName = "factor";
		gridViewTextBoxColumn13.HeaderText = "factor";
		gridViewTextBoxColumn13.IsVisible = false;
		gridViewTextBoxColumn13.Name = "factor";
		gridViewTextBoxColumn14.FieldName = "codundequi";
		gridViewTextBoxColumn14.HeaderText = "codundequi";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "codundequi";
		gridViewTextBoxColumn15.FieldName = "equivalente";
		gridViewTextBoxColumn15.HeaderText = "equivalente";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "equivalente";
		gridViewTextBoxColumn16.FieldName = "precio";
		gridViewTextBoxColumn16.HeaderText = "Precio";
		gridViewTextBoxColumn16.Name = "precio";
		gridViewTextBoxColumn16.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn16.Width = 302;
		gridViewTextBoxColumn17.FieldName = "codtipo";
		gridViewTextBoxColumn17.HeaderText = "codtipo";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "codtipo";
		gridViewTextBoxColumn18.FieldName = "tip";
		gridViewTextBoxColumn18.HeaderText = "Tipo";
		gridViewTextBoxColumn18.Name = "tip";
		gridViewTextBoxColumn18.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn18.Width = 301;
		this.dgvPVenta.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18);
		this.dgvPVenta.MasterTemplate.EnableGrouping = false;
		this.dgvPVenta.MasterTemplate.ShowFilteringRow = false;
		this.dgvPVenta.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvPVenta.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.dgvPVenta.Name = "dgvPVenta";
		this.dgvPVenta.ReadOnly = true;
		this.dgvPVenta.ShowGroupPanel = false;
		this.dgvPVenta.Size = new System.Drawing.Size(905, 89);
		this.dgvPVenta.TabIndex = 0;
		this.dgvPVenta.ThemeName = "Material";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(925, 536);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.Name = "frmStockAlmacenes";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Stock de Almacenes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmStockAlmacenes_Load);
		base.Shown += new System.EventHandler(frmStockAlmacenes_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnReporte).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnActualizar).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle_1).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvPVenta.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvPVenta).EndInit();
		base.ResumeLayout(false);
	}
}
