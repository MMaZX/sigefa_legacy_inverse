using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmProductos : Office2007Form
{
	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto pro = new clsProducto();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private TreeNode nodoselect = new TreeNode();

	private DataTable Arbol = new DataTable();

	public double tc_hoy = 0.0;

	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar2;

	private ButtonItem buttonItem16;

	private ButtonItem buttonItem6;

	private ButtonItem buttonItem8;

	private ButtonItem buttonItem3;

	private ButtonItem buttonItem5;

	private ButtonItem buttonItem9;

	private TreeView tvClasificacion;

	private GroupBox groupBox1;

	private DataGridView dgvProductos;

	private ButtonItem buttonItem1;

	private System.Windows.Forms.TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TabPage tabPage3;

	private DataGridView dgvNotas;

	private DataGridView dgvCaracteristicas;

	private DataGridViewTextBoxColumn codCaract;

	private DataGridViewTextBoxColumn caracteristica;

	private DataGridViewTextBoxColumn valor;

	private TextBox txtNombre;

	private Label label3;

	private TextBox txtReferencia;

	private Label label2;

	private TextBox txtCodProducto;

	private Label label1;

	private Label label39;

	private TextBox txtValorCompra;

	private DataGridViewTextBoxColumn codnotaproducto;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn nota;

	private ExpandablePanel expandablePanel1;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private TextBox txtFiltro;

	private Button btnSalir;

	private Label label8;

	private TextBox txtValorCompraSoles;

	private ButtonItem buttonItem2;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn marca;

	private DataGridViewTextBoxColumn modelo;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn Proveedor;

	private DataGridViewTextBoxColumn stock;

	private DataGridViewTextBoxColumn pedido;

	private DataGridViewTextBoxColumn precioprom;

	private DataGridViewTextBoxColumn valorpromsoles;

	private DataGridViewTextBoxColumn valorizado;

	private DataGridViewTextBoxColumn ultprecio;

	private DataGridViewTextBoxColumn stockmin;

	private DataGridViewTextBoxColumn stockmax;

	private DataGridViewTextBoxColumn stockrepo;

	private DataGridViewTextBoxColumn control;

	public ButtonItem buttonItem4;

	public frmProductos()
	{
		InitializeComponent();
	}

	private void frmProductos_Load(object sender, EventArgs e)
	{
		ConsultaArbol();
		llenaarbol(0, 0, null);
		label7.Text = "Referencia";
		label6.Text = "referencia";
	}

	private void buttonItem16_Click(object sender, EventArgs e)
	{
		frmRegistroProducto frm = new frmRegistroProducto();
		frm.Proceso = 1;
		frm.ShowDialog();
		nodoselect = tvClasificacion.SelectedNode;
		tvClasificacion.Nodes.Clear();
		ConsultaArbol();
		llenaarbol(0, 0, null);
		if (tvClasificacion.SelectedNode != null)
		{
			CargaLista(nodoselect);
		}
	}

	private void frmProductos_Shown(object sender, EventArgs e)
	{
	}

	private void CargaLista(TreeNode nodoseleccionado)
	{
		try
		{
			if (data.DataSource != null)
			{
				DataTable dt = (DataTable)data.DataSource;
				dt.Clear();
			}
			dgvProductos.DataSource = data;
			data.DataSource = AdmPro.MuestraProductos(nodoseleccionado.Level, Convert.ToInt32(nodoseleccionado.Tag), frmLogin.iCodAlmacen);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvProductos.ClearSelection();
			DarFormato();
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
		}
	}

	private void ConsultaArbol()
	{
		Arbol = AdmPro.ArbolProductos();
	}

	private void llenaarbol(int nivel, int indicePadre, TreeNode nodoPadre)
	{
		DataView hijos = new DataView(Arbol);
		hijos.RowFilter = Arbol.Columns["codpadre"].ColumnName + " = " + indicePadre;
		DataView dataView = hijos;
		dataView.RowFilter = dataView.RowFilter + " AND " + Arbol.Columns["nivel"].ColumnName + " = " + nivel;
		foreach (DataRowView row in hijos)
		{
			TreeNode nuevonodo = new TreeNode();
			nuevonodo.Text = row["descripcion"].ToString();
			nuevonodo.Tag = row["codigo"].ToString();
			if (nodoPadre == null)
			{
				tvClasificacion.Nodes.Add(nuevonodo);
			}
			else
			{
				nodoPadre.Nodes.Add(nuevonodo);
			}
			llenaarbol(nivel + 1, int.Parse(row["codigo"].ToString()), nuevonodo);
		}
	}

	private void tvClasificacion_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		try
		{
			CargaLista(e.Node);
		}
		catch (Exception)
		{
		}
	}

	private void buttonItem6_Click(object sender, EventArgs e)
	{
		if (dgvProductos.SelectedRows.Count > 0)
		{
			frmGestionProducto frm = new frmGestionProducto();
			frm.Proceso = 2;
			frm.pro = pro;
			frm.ShowDialog();
			nodoselect = tvClasificacion.SelectedNode;
			tvClasificacion.Nodes.Clear();
			ConsultaArbol();
			llenaarbol(0, 0, null);
			if (tvClasificacion.SelectedNode != null)
			{
				CargaLista(nodoselect);
			}
		}
	}

	private void dgvProductos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvProductos.Rows.Count >= 1 && e.Row.Selected)
		{
			pro.CodProducto = Convert.ToInt32(e.Row.Cells[codproducto.Name].Value);
		}
	}

	private void DarFormato()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvProductos.Rows)
		{
			if (Convert.ToDouble(row.Cells[stock.Name].Value) <= Convert.ToDouble(row.Cells[stockmin.Name].Value))
			{
				row.DefaultCellStyle.BackColor = Color.Red;
				row.DefaultCellStyle.ForeColor = Color.White;
			}
			else if (Convert.ToDouble(row.Cells[stock.Name].Value) <= Convert.ToDouble(row.Cells[stockrepo.Name].Value))
			{
				row.DefaultCellStyle.BackColor = Color.Orange;
				row.DefaultCellStyle.ForeColor = Color.White;
			}
			else if (Convert.ToDouble(row.Cells[stock.Name].Value) >= Convert.ToDouble(row.Cells[stockmax.Name].Value) && Convert.ToDouble(row.Cells[stockmax.Name].Value) > 0.0)
			{
				row.DefaultCellStyle.BackColor = Color.Green;
				row.DefaultCellStyle.ForeColor = Color.White;
			}
		}
	}

	private void CargaListaCaracteristicas()
	{
		dgvCaracteristicas.DataSource = AdmPro.MuestraCaracteristicas(pro.CodProducto);
		dgvCaracteristicas.ClearSelection();
	}

	private void CargaListaNotas()
	{
		dgvNotas.DataSource = AdmPro.MuestraNotas(pro.CodProducto);
		dgvNotas.ClearSelection();
	}

	private void buttonItem1_Click(object sender, EventArgs e)
	{
		if (dgvProductos.SelectedRows.Count > 0)
		{
			frmGestionProducto frm = new frmGestionProducto();
			frm.pro = pro;
			frm.ShowDialog();
			if (tvClasificacion.SelectedNode != null)
			{
				CargaLista(tvClasificacion.SelectedNode);
			}
		}
	}

	private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		double var = 0.0;
		try
		{
			if (pro.CodProducto != 0 && e.RowIndex != -1)
			{
				DataGridViewRow Row = dgvProductos.Rows[e.RowIndex];
				txtCodProducto.Text = Row.Cells[codproducto.Name].Value.ToString();
				txtReferencia.Text = Row.Cells[referencia.Name].Value.ToString();
				txtNombre.Text = Row.Cells[nombre.Name].Value.ToString();
				txtValorCompra.Text = $"{Row.Cells[precioprom.Name].Value.ToString():#,##0.00}";
				txtValorCompraSoles.Text = $"{Row.Cells[valorpromsoles.Name].Value:#,##0.00}";
				CargaListaCaracteristicas();
				CargaListaNotas();
			}
		}
		catch (Exception)
		{
		}
	}

	private void buttonItem9_Click(object sender, EventArgs e)
	{
		DataTable dt = new DataTable("Productos");
		foreach (DataGridViewColumn column in dgvProductos.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvProductos.Rows.Count; i++)
		{
			DataGridViewRow row = dgvProductos.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvProductos.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmProductosRP frm = new frmProductosRP();
		frm.DTable = dt;
		frm.Show();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label6.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void buttonItem5_Click(object sender, EventArgs e)
	{
		if (!expandablePanel1.Expanded)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
		else
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void frmProductos_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B && e.Control)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
	}

	private void dgvProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvProductos.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvProductos.Columns[e.ColumnIndex].DataPropertyName;
		if (expandablePanel1.Expanded)
		{
			txtFiltro.Focus();
		}
	}

	public void buttonItem4_Click(object sender, EventArgs e)
	{
		if (tvClasificacion.SelectedNode != null)
		{
			CargaLista(tvClasificacion.SelectedNode);
		}
	}

	private void buttonItem3_Click(object sender, EventArgs e)
	{
		if (dgvProductos.SelectedRows.Count > 0)
		{
			frmGestionProducto frm = new frmGestionProducto();
			frm.Funcion = 3;
			frm.pro = pro;
			frm.ShowDialog();
		}
	}

	private void buttonItem8_Click(object sender, EventArgs e)
	{
	}

	private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvProductos.SelectedRows.Count > 0)
		{
			frmGestionProducto frm = new frmGestionProducto();
			frm.Funcion = 3;
			frm.pro = pro;
			frm.ShowDialog();
		}
	}

	private void dgvProductos_Sorted(object sender, EventArgs e)
	{
		DarFormato();
	}

	private void buttonItem2_Click(object sender, EventArgs e)
	{
		CRVarolizacion rpt = new CRVarolizacion();
		frmRptKardex frm = new frmRptKardex();
		clsReportProductos cls = new clsReportProductos();
		DataTable nuevo = new DataTable();
		rpt.SetDataSource(cls.ReporteValorizacion(frmLogin.iCodAlmacen));
		frm.crvKardex.ReportSource = rpt;
		frm.Show();
	}

	private void tvClasificacion_AfterSelect(object sender, TreeViewEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.tvClasificacion = new System.Windows.Forms.TreeView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.label8 = new System.Windows.Forms.Label();
		this.txtValorCompraSoles = new System.Windows.Forms.TextBox();
		this.label39 = new System.Windows.Forms.Label();
		this.txtValorCompra = new System.Windows.Forms.TextBox();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodProducto = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dgvCaracteristicas = new System.Windows.Forms.DataGridView();
		this.codCaract = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.caracteristica = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.dgvNotas = new System.Windows.Forms.DataGridView();
		this.codnotaproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvProductos = new System.Windows.Forms.DataGridView();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioprom = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromsoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorizado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ultprecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockmax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockrepo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.control = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCaracteristicas).BeginInit();
		this.tabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNotas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).BeginInit();
		this.expandablePanel1.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "iconos_icono-leasing-habitacional.png");
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[9] { this.buttonItem16, this.buttonItem6, this.buttonItem8, this.buttonItem3, this.buttonItem4, this.buttonItem5, this.buttonItem9, this.buttonItem1, this.buttonItem2 });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(983, 55);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 3;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.buttonItem16.ImageIndex = 4;
		this.buttonItem16.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem16.Name = "buttonItem16";
		this.buttonItem16.SubItemsExpandWidth = 14;
		this.buttonItem16.Text = "Nuevo";
		this.buttonItem16.Visible = false;
		this.buttonItem16.Click += new System.EventHandler(buttonItem16_Click);
		this.buttonItem6.ImageIndex = 3;
		this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem6.Name = "buttonItem6";
		this.buttonItem6.SubItemsExpandWidth = 14;
		this.buttonItem6.Text = "Modificar";
		this.buttonItem6.Click += new System.EventHandler(buttonItem6_Click);
		this.buttonItem8.ImageIndex = 5;
		this.buttonItem8.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem8.Name = "buttonItem8";
		this.buttonItem8.SubItemsExpandWidth = 14;
		this.buttonItem8.Text = "Eliminar";
		this.buttonItem8.Visible = false;
		this.buttonItem8.Click += new System.EventHandler(buttonItem8_Click);
		this.buttonItem3.ImageIndex = 17;
		this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem3.Name = "buttonItem3";
		this.buttonItem3.SubItemsExpandWidth = 14;
		this.buttonItem3.Text = "Consultar";
		this.buttonItem3.Visible = false;
		this.buttonItem3.Click += new System.EventHandler(buttonItem3_Click);
		this.buttonItem4.ImageIndex = 8;
		this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem4.Name = "buttonItem4";
		this.buttonItem4.SubItemsExpandWidth = 14;
		this.buttonItem4.Text = "Actualizar";
		this.buttonItem4.Click += new System.EventHandler(buttonItem4_Click);
		this.buttonItem5.ImageIndex = 11;
		this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem5.Name = "buttonItem5";
		this.buttonItem5.SubItemsExpandWidth = 14;
		this.buttonItem5.Text = "Buscar";
		this.buttonItem5.Click += new System.EventHandler(buttonItem5_Click);
		this.buttonItem9.ImageIndex = 7;
		this.buttonItem9.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem9.Name = "buttonItem9";
		this.buttonItem9.SubItemsExpandWidth = 14;
		this.buttonItem9.Text = "Imprimir";
		this.buttonItem9.Click += new System.EventHandler(buttonItem9_Click);
		this.buttonItem1.ImageIndex = 16;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Configurar";
		this.buttonItem1.Click += new System.EventHandler(buttonItem1_Click);
		this.buttonItem2.ImageIndex = 18;
		this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.SubItemsExpandWidth = 14;
		this.buttonItem2.Text = "Valorizacion";
		this.buttonItem2.Click += new System.EventHandler(buttonItem2_Click);
		this.tvClasificacion.Dock = System.Windows.Forms.DockStyle.Left;
		this.tvClasificacion.Location = new System.Drawing.Point(0, 55);
		this.tvClasificacion.Name = "tvClasificacion";
		this.tvClasificacion.Size = new System.Drawing.Size(178, 419);
		this.tvClasificacion.TabIndex = 11;
		this.tvClasificacion.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tvClasificacion_AfterSelect);
		this.tvClasificacion.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvClasificacion_NodeMouseClick);
		this.groupBox1.Controls.Add(this.tabControl1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(178, 345);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(805, 129);
		this.groupBox1.TabIndex = 13;
		this.groupBox1.TabStop = false;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(3, 16);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(799, 110);
		this.tabControl1.TabIndex = 0;
		this.tabPage1.Controls.Add(this.label8);
		this.tabPage1.Controls.Add(this.txtValorCompraSoles);
		this.tabPage1.Controls.Add(this.label39);
		this.tabPage1.Controls.Add(this.txtValorCompra);
		this.tabPage1.Controls.Add(this.txtNombre);
		this.tabPage1.Controls.Add(this.label3);
		this.tabPage1.Controls.Add(this.txtReferencia);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.txtCodProducto);
		this.tabPage1.Controls.Add(this.label1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(791, 84);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Detalle Producto";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(637, 12);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(108, 13);
		this.label8.TabIndex = 39;
		this.label8.Text = "Precio Promedio(S/.):";
		this.txtValorCompraSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtValorCompraSoles.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtValorCompraSoles.Location = new System.Drawing.Point(640, 28);
		this.txtValorCompraSoles.Name = "txtValorCompraSoles";
		this.txtValorCompraSoles.ReadOnly = true;
		this.txtValorCompraSoles.Size = new System.Drawing.Size(105, 20);
		this.txtValorCompraSoles.TabIndex = 38;
		this.txtValorCompraSoles.Tag = "1";
		this.txtValorCompraSoles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label39.AutoSize = true;
		this.label39.Location = new System.Drawing.Point(532, 12);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(99, 13);
		this.label39.TabIndex = 37;
		this.label39.Text = "Precio Promedio($):";
		this.txtValorCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtValorCompra.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtValorCompra.Location = new System.Drawing.Point(535, 28);
		this.txtValorCompra.Name = "txtValorCompra";
		this.txtValorCompra.ReadOnly = true;
		this.txtValorCompra.Size = new System.Drawing.Size(96, 20);
		this.txtValorCompra.TabIndex = 36;
		this.txtValorCompra.Tag = "1";
		this.txtValorCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombre.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtNombre.Location = new System.Drawing.Point(197, 28);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.ReadOnly = true;
		this.txtNombre.Size = new System.Drawing.Size(332, 20);
		this.txtNombre.TabIndex = 11;
		this.txtNombre.Tag = "1";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(194, 12);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(50, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "Nombre :";
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtReferencia.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtReferencia.Location = new System.Drawing.Point(91, 28);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.ReadOnly = true;
		this.txtReferencia.Size = new System.Drawing.Size(100, 20);
		this.txtReferencia.TabIndex = 9;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(88, 12);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(62, 13);
		this.label2.TabIndex = 8;
		this.label2.Text = "Referencia:";
		this.txtCodProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodProducto.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtCodProducto.Location = new System.Drawing.Point(19, 28);
		this.txtCodProducto.Name = "txtCodProducto";
		this.txtCodProducto.ReadOnly = true;
		this.txtCodProducto.Size = new System.Drawing.Size(66, 20);
		this.txtCodProducto.TabIndex = 7;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(16, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 6;
		this.label1.Text = "Código:";
		this.tabPage2.Controls.Add(this.dgvCaracteristicas);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(791, 84);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Caracteristicas";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dgvCaracteristicas.AllowUserToAddRows = false;
		this.dgvCaracteristicas.AllowUserToDeleteRows = false;
		this.dgvCaracteristicas.AllowUserToResizeColumns = false;
		this.dgvCaracteristicas.AllowUserToResizeRows = false;
		this.dgvCaracteristicas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCaracteristicas.Columns.AddRange(this.codCaract, this.caracteristica, this.valor);
		this.dgvCaracteristicas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvCaracteristicas.Enabled = false;
		this.dgvCaracteristicas.Location = new System.Drawing.Point(3, 3);
		this.dgvCaracteristicas.Name = "dgvCaracteristicas";
		this.dgvCaracteristicas.ReadOnly = true;
		this.dgvCaracteristicas.RowHeadersVisible = false;
		this.dgvCaracteristicas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCaracteristicas.Size = new System.Drawing.Size(785, 78);
		this.dgvCaracteristicas.TabIndex = 4;
		this.codCaract.DataPropertyName = "codCaracteristicaProducto";
		this.codCaract.HeaderText = "Codigo";
		this.codCaract.Name = "codCaract";
		this.codCaract.ReadOnly = true;
		this.codCaract.Visible = false;
		this.caracteristica.DataPropertyName = "descripcion";
		this.caracteristica.HeaderText = "Caracteristica";
		this.caracteristica.Name = "caracteristica";
		this.caracteristica.ReadOnly = true;
		this.caracteristica.Width = 200;
		this.valor.DataPropertyName = "valor";
		this.valor.HeaderText = "Valor";
		this.valor.Name = "valor";
		this.valor.ReadOnly = true;
		this.valor.Width = 280;
		this.tabPage3.Controls.Add(this.dgvNotas);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(791, 84);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "Notas";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.dgvNotas.AllowUserToAddRows = false;
		this.dgvNotas.AllowUserToDeleteRows = false;
		this.dgvNotas.AllowUserToResizeColumns = false;
		this.dgvNotas.AllowUserToResizeRows = false;
		this.dgvNotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvNotas.Columns.AddRange(this.codnotaproducto, this.usuario, this.nota);
		this.dgvNotas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvNotas.Enabled = false;
		this.dgvNotas.Location = new System.Drawing.Point(3, 3);
		this.dgvNotas.Name = "dgvNotas";
		this.dgvNotas.ReadOnly = true;
		this.dgvNotas.RowHeadersVisible = false;
		this.dgvNotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNotas.Size = new System.Drawing.Size(785, 78);
		this.dgvNotas.TabIndex = 5;
		this.codnotaproducto.DataPropertyName = "codNota";
		this.codnotaproducto.HeaderText = "Codigo";
		this.codnotaproducto.Name = "codnotaproducto";
		this.codnotaproducto.ReadOnly = true;
		this.codnotaproducto.Visible = false;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Usuario";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.usuario.Width = 200;
		this.nota.DataPropertyName = "nota";
		this.nota.HeaderText = "Nota";
		this.nota.Name = "nota";
		this.nota.ReadOnly = true;
		this.nota.Width = 600;
		this.dgvProductos.AllowUserToAddRows = false;
		this.dgvProductos.AllowUserToDeleteRows = false;
		this.dgvProductos.AllowUserToResizeColumns = false;
		this.dgvProductos.AllowUserToResizeRows = false;
		this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProductos.Columns.AddRange(this.codproducto, this.referencia, this.nombre, this.marca, this.modelo, this.unidad, this.Proveedor, this.stock, this.pedido, this.precioprom, this.valorpromsoles, this.valorizado, this.ultprecio, this.stockmin, this.stockmax, this.stockrepo, this.control);
		this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvProductos.Location = new System.Drawing.Point(178, 55);
		this.dgvProductos.MultiSelect = false;
		this.dgvProductos.Name = "dgvProductos";
		this.dgvProductos.ReadOnly = true;
		this.dgvProductos.RowHeadersVisible = false;
		this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProductos.Size = new System.Drawing.Size(805, 290);
		this.dgvProductos.TabIndex = 14;
		this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProductos_CellClick);
		this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProductos_CellDoubleClick);
		this.dgvProductos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvProductos_ColumnHeaderMouseClick);
		this.dgvProductos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvProductos_RowStateChanged);
		this.dgvProductos.Sorted += new System.EventHandler(dgvProductos_Sorted);
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "Código";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Visible = false;
		this.codproducto.Width = 90;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Width = 80;
		this.nombre.DataPropertyName = "descripcion";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Width = 300;
		this.marca.DataPropertyName = "nmarca";
		this.marca.HeaderText = "Marca";
		this.marca.Name = "marca";
		this.marca.ReadOnly = true;
		this.marca.Width = 150;
		this.modelo.DataPropertyName = "modelo";
		this.modelo.HeaderText = "Modelo";
		this.modelo.Name = "modelo";
		this.modelo.ReadOnly = true;
		this.modelo.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Width = 150;
		this.Proveedor.DataPropertyName = "Proveedor";
		this.Proveedor.HeaderText = "Proveedor";
		this.Proveedor.Name = "Proveedor";
		this.Proveedor.ReadOnly = true;
		this.Proveedor.Visible = false;
		this.Proveedor.Width = 300;
		this.stock.DataPropertyName = "stockactual";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.NullValue = null;
		this.stock.DefaultCellStyle = dataGridViewCellStyle1;
		this.stock.HeaderText = "Stock";
		this.stock.Name = "stock";
		this.stock.ReadOnly = true;
		this.stock.Width = 65;
		this.pedido.DataPropertyName = "pedido";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		this.pedido.DefaultCellStyle = dataGridViewCellStyle2;
		this.pedido.HeaderText = "Pedido";
		this.pedido.Name = "pedido";
		this.pedido.ReadOnly = true;
		this.pedido.Visible = false;
		this.pedido.Width = 80;
		this.precioprom.DataPropertyName = "valorpromedio";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "n4";
		this.precioprom.DefaultCellStyle = dataGridViewCellStyle3;
		this.precioprom.HeaderText = "V. Compra Prom.($.)";
		this.precioprom.Name = "precioprom";
		this.precioprom.ReadOnly = true;
		this.precioprom.Visible = false;
		this.precioprom.Width = 90;
		this.valorpromsoles.DataPropertyName = "valorpromsoles";
		this.valorpromsoles.HeaderText = "V. Compra Prom.(S/.)";
		this.valorpromsoles.Name = "valorpromsoles";
		this.valorpromsoles.ReadOnly = true;
		this.valorpromsoles.Width = 90;
		this.valorizado.DataPropertyName = "valorizado";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N4";
		this.valorizado.DefaultCellStyle = dataGridViewCellStyle4;
		this.valorizado.HeaderText = "Valorizado($.)";
		this.valorizado.Name = "valorizado";
		this.valorizado.ReadOnly = true;
		this.valorizado.Width = 90;
		this.ultprecio.DataPropertyName = "ultprecio";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N4";
		this.ultprecio.DefaultCellStyle = dataGridViewCellStyle5;
		this.ultprecio.HeaderText = "Ult. valor compra(S/. Inc. IGV)";
		this.ultprecio.Name = "ultprecio";
		this.ultprecio.ReadOnly = true;
		this.ultprecio.Width = 90;
		this.stockmin.DataPropertyName = "smin";
		this.stockmin.HeaderText = "S. Min";
		this.stockmin.Name = "stockmin";
		this.stockmin.ReadOnly = true;
		this.stockmin.Visible = false;
		this.stockmax.DataPropertyName = "smax";
		this.stockmax.HeaderText = "S. Max";
		this.stockmax.Name = "stockmax";
		this.stockmax.ReadOnly = true;
		this.stockmax.Visible = false;
		this.stockrepo.DataPropertyName = "srepo";
		this.stockrepo.HeaderText = "S. Repo";
		this.stockrepo.Name = "stockrepo";
		this.stockrepo.ReadOnly = true;
		this.stockrepo.Visible = false;
		this.control.DataPropertyName = "control";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N2";
		this.control.DefaultCellStyle = dataGridViewCellStyle6;
		this.control.HeaderText = "Control Stock";
		this.control.Name = "control";
		this.control.ReadOnly = true;
		this.control.Visible = false;
		this.control.Width = 70;
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.label4);
		this.expandablePanel1.Controls.Add(this.label5);
		this.expandablePanel1.Controls.Add(this.label6);
		this.expandablePanel1.Controls.Add(this.label7);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.btnSalir);
		this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.expandablePanel1.ExpandButtonVisible = false;
		this.expandablePanel1.Expanded = false;
		this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(603, 0, 231, 93);
		this.expandablePanel1.Location = new System.Drawing.Point(752, 0);
		this.expandablePanel1.Name = "expandablePanel1";
		this.expandablePanel1.ShowFocusRectangle = true;
		this.expandablePanel1.Size = new System.Drawing.Size(231, 0);
		this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.Style.BackColor1.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.BackColor2.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarPopupBorder;
		this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
		this.expandablePanel1.Style.GradientAngle = 90;
		this.expandablePanel1.TabIndex = 18;
		this.expandablePanel1.TitleHeight = 0;
		this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
		this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.expandablePanel1.TitleStyle.GradientAngle = 90;
		this.expandablePanel1.TitleText = "Title Bar";
		this.expandablePanel1.Visible = false;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(10, -59);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 13);
		this.label4.TabIndex = 10;
		this.label4.Text = "Por :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Lucida Sans", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(5, -89);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(62, 12);
		this.label5.TabIndex = 9;
		this.label5.Text = "Busqueda";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.ForeColor = System.Drawing.Color.LightBlue;
		this.label6.Location = new System.Drawing.Point(186, -59);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 7;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(45, -59);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(15, 13);
		this.label7.TabIndex = 6;
		this.label7.Text = "X";
		this.txtFiltro.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFiltro.Location = new System.Drawing.Point(13, -38);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 5;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.BackColor = System.Drawing.Color.Transparent;
		this.btnSalir.FlatAppearance.BorderSize = 0;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Location = new System.Drawing.Point(213, -93);
		this.btnSalir.Margin = new System.Windows.Forms.Padding(1);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(18, 22);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "x";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnSalir.UseVisualStyleBackColor = false;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(983, 474);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.dgvProductos);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.tvClasificacion);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.Name = "frmProductos";
		this.Text = "Productos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmProductos_Load);
		base.Shown += new System.EventHandler(frmProductos_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmProductos_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvCaracteristicas).EndInit();
		this.tabPage3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvNotas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).EndInit();
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
