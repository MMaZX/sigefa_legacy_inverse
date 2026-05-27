using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;
using Telerik.WinControls.UI;

namespace SIGEFA.Reportes;

public class frmParamGananciaxArticulo2 : Form
{
	private clsReporteGananciaxArticulo ds = new clsReporteGananciaxArticulo();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto productoSeleccionado = new clsProducto();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmCliente admcli = new clsAdmCliente();

	private clsAdmMarca admmarca = new clsAdmMarca();

	private clsAdmFamilia admfamilia = new clsAdmFamilia();

	private clsAdmUsuario admusuario = new clsAdmUsuario();

	private clsAdmProveedor admprov = new clsAdmProveedor();

	private clsAdmTecnico admtec = new clsAdmTecnico();

	private IContainer components = null;

	private RadioButton rbProductoEspecifico;

	private RadioButton rbTodos;

	private LabelX labelX5;

	private TextBoxX txtNombreProducto;

	private TextBoxX txtBusquedaProducto;

	private ButtonX btnCancelar;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private LabelX labelX2;

	private LabelX labelX1;

	private LabelX labelX3;

	private RadCheckedDropDownList cmbAlmacenes;

	private Line line4;

	private RadAutoCompleteBox txtProveedor;

	private LabelX labelX9;

	private Line line6;

	private Line line5;

	private RadAutoCompleteBox txtFamilia;

	private LabelX labelX7;

	private RadAutoCompleteBox txtMarca;

	private LabelX labelX6;

	private RadPageView radPageView1;

	private RadPageViewPage productoTab;

	private ButtonX btnReporte;

	private RadPageViewPage clienteTab;

	private ButtonX btnReporteCliente;

	private Line line8;

	private RadAutoCompleteBox txtCliente;

	private LabelX labelX4;

	private RadPageViewPage proveedorTab;

	private ButtonX btnReporteVendedor;

	private Line line7;

	private RadAutoCompleteBox txtVendedor;

	private LabelX labelX8;

	private RadAutoCompleteBox txtTecnico;

	private LabelX labelX10;

	public frmParamGananciaxArticulo2()
	{
		InitializeComponent();
	}

	private void CargaProducto(int Codigo)
	{
		productoSeleccionado = AdmPro.CargaProducto(Codigo, frmLogin.iCodAlmacen);
		txtBusquedaProducto.Text = productoSeleccionado.Referencia;
		txtNombreProducto.Text = productoSeleccionado.Descripcion;
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		if (cmbAlmacenes.CheckedItems.Count > 0)
		{
			int todos = 0;
			int cod1 = 0;
			int cod2 = 0;
			int cod3 = 0;
			int cod4 = 0;
			int todosmarca = 0;
			int marca1 = 0;
			int marca2 = 0;
			int marca3 = 0;
			int marca4 = 0;
			int marca5 = 0;
			int todosfam = 0;
			int fam1 = 0;
			int fam2 = 0;
			int fam3 = 0;
			int fam4 = 0;
			int fam5 = 0;
			int todosprov = 0;
			int prov1 = 0;
			int prov2 = 0;
			int prov3 = 0;
			int prov4 = 0;
			int prov5 = 0;
			if (txtMarca.Items.Count > 0 && txtMarca.Items.Count < 6)
			{
				marca1 = Convert.ToInt32((txtMarca.Items.Count >= 1) ? txtMarca.Items[0].Value : ((object)0));
				marca2 = Convert.ToInt32((txtMarca.Items.Count >= 2) ? txtMarca.Items[1].Value : ((object)0));
				marca3 = Convert.ToInt32((txtMarca.Items.Count >= 3) ? txtMarca.Items[2].Value : ((object)0));
				marca4 = Convert.ToInt32((txtMarca.Items.Count >= 4) ? txtMarca.Items[3].Value : ((object)0));
				marca5 = Convert.ToInt32((txtMarca.Items.Count >= 5) ? txtMarca.Items[4].Value : ((object)0));
			}
			else if (txtMarca.Items.Count == 0)
			{
				todosmarca = 1;
			}
			if (txtFamilia.Items.Count > 0 && txtFamilia.Items.Count < 6)
			{
				fam1 = Convert.ToInt32((txtFamilia.Items.Count >= 1) ? txtFamilia.Items[0].Value : ((object)0));
				fam2 = Convert.ToInt32((txtFamilia.Items.Count >= 2) ? txtFamilia.Items[1].Value : ((object)0));
				fam3 = Convert.ToInt32((txtFamilia.Items.Count >= 3) ? txtFamilia.Items[2].Value : ((object)0));
				fam4 = Convert.ToInt32((txtFamilia.Items.Count >= 4) ? txtFamilia.Items[3].Value : ((object)0));
				fam5 = Convert.ToInt32((txtFamilia.Items.Count >= 5) ? txtFamilia.Items[4].Value : ((object)0));
			}
			else if (txtFamilia.Items.Count == 0)
			{
				todosfam = 1;
			}
			if (txtProveedor.Items.Count > 0 && txtProveedor.Items.Count < 6)
			{
				prov1 = Convert.ToInt32((txtProveedor.Items.Count >= 1) ? txtProveedor.Items[0].Value : ((object)0));
				prov2 = Convert.ToInt32((txtProveedor.Items.Count >= 2) ? txtProveedor.Items[1].Value : ((object)0));
				prov3 = Convert.ToInt32((txtProveedor.Items.Count >= 3) ? txtProveedor.Items[2].Value : ((object)0));
				prov4 = Convert.ToInt32((txtProveedor.Items.Count >= 4) ? txtProveedor.Items[3].Value : ((object)0));
				prov5 = Convert.ToInt32((txtProveedor.Items.Count >= 5) ? txtProveedor.Items[4].Value : ((object)0));
			}
			else if (txtProveedor.Items.Count == 0)
			{
				todosprov = 1;
			}
			foreach (RadCheckedListDataItem a in cmbAlmacenes.CheckedItems)
			{
				if (Convert.ToInt32(a.Value) == 0)
				{
					todos = 1;
				}
				if (Convert.ToInt32(a.Value) == 1)
				{
					cod1 = 1;
				}
				if (Convert.ToInt32(a.Value) == 2)
				{
					cod2 = 2;
				}
				if (Convert.ToInt32(a.Value) == 3)
				{
					cod3 = 3;
				}
				if (Convert.ToInt32(a.Value) == 4)
				{
					cod4 = 4;
				}
			}
			int codigoProducto = ((productoSeleccionado != null) ? productoSeleccionado.CodProducto : 0);
			CRGananciaxArticulo2 rpt = new CRGananciaxArticulo2();
			frmRptGananciaxArticulo frm = new frmRptGananciaxArticulo();
			DataTable dt = ds.ReportGananciaxArticulo2(codigoProducto, dtpDesde.Value, dtpHasta.Value, todos, cod1, cod2, cod3, cod4, todosmarca, marca1, marca2, marca3, marca4, marca5, todosfam, fam1, fam2, fam3, fam4, fam5, todosprov, prov1, prov2, prov3, prov4, prov5).Tables[0];
			if (dt.Rows.Count > 0)
			{
				rpt.SetDataSource(dt);
				frm.crvRptGananciaxArticulo.ReportSource = rpt;
				frm.Show();
			}
			else
			{
				MessageBox.Show("No se encontraron resultados con los parámetros seleccionados", "Reporte de Ganancia por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			MessageBox.Show("Seleccione almacen...", "Reporte de Ganancia por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void frmParamGananciaxArticulo_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		cargaClientes();
		cargaMarcas();
		cargaFamilias();
		cargaVendedores();
		cargaProveedores();
		cargaTecnico();
	}

	private void cargaTecnico()
	{
		txtTecnico.AutoCompleteDataSource = admtec.listaTecnicos();
		txtTecnico.AutoCompleteValueMember = "idTecnico";
		txtTecnico.AutoCompleteDisplayMember = "nombreCompleto";
	}

	public void cargaClientes()
	{
		txtCliente.AutoCompleteDisplayMember = "razonsocial";
		txtCliente.AutoCompleteValueMember = "codcliente";
		txtCliente.AutoCompleteDataSource = admcli.MuestraClientes();
	}

	public void cargaMarcas()
	{
		txtMarca.AutoCompleteDisplayMember = "descripcion";
		txtMarca.AutoCompleteValueMember = "codmarca";
		txtMarca.AutoCompleteDataSource = admmarca.MuestraMarcas();
	}

	public void cargaFamilias()
	{
		txtFamilia.AutoCompleteDisplayMember = "descripcion";
		txtFamilia.AutoCompleteValueMember = "codfamilia";
		txtFamilia.AutoCompleteDataSource = admfamilia.MuestraFamilias();
	}

	public void cargaVendedores()
	{
		txtVendedor.AutoCompleteValueMember = "codusuario";
		txtVendedor.AutoCompleteDisplayMember = "nombre";
		txtVendedor.AutoCompleteDataSource = admusuario.MuestraUsuariosxNivel(2);
	}

	public void cargaProveedores()
	{
		txtProveedor.AutoCompleteDisplayMember = "razonsocial";
		txtProveedor.AutoCompleteValueMember = "codproveedor";
		txtProveedor.AutoCompleteDataSource = admprov.MuestraProveedores();
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "codalmacen";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.almacenReporte();
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void txtBusquedaProducto_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 15;
			if (frm.ShowDialog() == DialogResult.OK && frm.pro.CodProducto != 0)
			{
				CargaProducto(frm.pro.CodProducto);
			}
		}
	}

	private void rbProductoEspecifico_CheckedChanged(object sender, EventArgs e)
	{
		if (rbProductoEspecifico.Checked)
		{
			txtBusquedaProducto.Enabled = true;
			txtBusquedaProducto.Focus();
		}
	}

	private void rbTodos_CheckedChanged(object sender, EventArgs e)
	{
		if (rbTodos.Checked)
		{
			txtBusquedaProducto.Enabled = false;
			txtBusquedaProducto.Text = "";
			txtNombreProducto.Text = "";
			productoSeleccionado = null;
		}
	}

	private void line3_Click(object sender, EventArgs e)
	{
	}

	private void cmbAlmacenes_ItemCheckedChanged(object sender, RadCheckedListDataItemEventArgs e)
	{
	}

	private void groupPanel1_Click(object sender, EventArgs e)
	{
	}

	private void btnReporteCliente_Click(object sender, EventArgs e)
	{
		if (cmbAlmacenes.CheckedItems.Count > 0)
		{
			int todos = 0;
			int cod1 = 0;
			int cod2 = 0;
			int cod3 = 0;
			int cod4 = 0;
			int todoscli = 0;
			int cli1 = 0;
			int cli2 = 0;
			int cli3 = 0;
			int cli4 = 0;
			int cli5 = 0;
			int todostec = 0;
			int tec1 = 0;
			int tec2 = 0;
			int tec3 = 0;
			int tec4 = 0;
			int tec5 = 0;
			if (txtCliente.Items.Count > 0 && txtCliente.Items.Count < 6)
			{
				cli1 = Convert.ToInt32((txtCliente.Items.Count >= 1) ? txtCliente.Items[0].Value : ((object)0));
				cli2 = Convert.ToInt32((txtCliente.Items.Count >= 2) ? txtCliente.Items[1].Value : ((object)0));
				cli3 = Convert.ToInt32((txtCliente.Items.Count >= 3) ? txtCliente.Items[2].Value : ((object)0));
				cli4 = Convert.ToInt32((txtCliente.Items.Count >= 4) ? txtCliente.Items[3].Value : ((object)0));
				cli5 = Convert.ToInt32((txtCliente.Items.Count >= 5) ? txtCliente.Items[4].Value : ((object)0));
			}
			else if (txtCliente.Items.Count == 0)
			{
				todoscli = 1;
			}
			if (txtTecnico.Items.Count > 0 && txtTecnico.Items.Count < 6)
			{
				tec1 = Convert.ToInt32((txtTecnico.Items.Count >= 1) ? txtTecnico.Items[0].Value : ((object)0));
				tec2 = Convert.ToInt32((txtTecnico.Items.Count >= 2) ? txtTecnico.Items[1].Value : ((object)0));
				tec3 = Convert.ToInt32((txtTecnico.Items.Count >= 3) ? txtTecnico.Items[2].Value : ((object)0));
				tec4 = Convert.ToInt32((txtTecnico.Items.Count >= 4) ? txtTecnico.Items[3].Value : ((object)0));
				tec5 = Convert.ToInt32((txtTecnico.Items.Count >= 5) ? txtTecnico.Items[4].Value : ((object)0));
			}
			else if (txtTecnico.Items.Count == 0)
			{
				todostec = 1;
			}
			foreach (RadCheckedListDataItem a in cmbAlmacenes.CheckedItems)
			{
				if (Convert.ToInt32(a.Value) == 0)
				{
					todos = 1;
				}
				if (Convert.ToInt32(a.Value) == 1)
				{
					cod1 = 1;
				}
				if (Convert.ToInt32(a.Value) == 2)
				{
					cod2 = 2;
				}
				if (Convert.ToInt32(a.Value) == 3)
				{
					cod3 = 3;
				}
				if (Convert.ToInt32(a.Value) == 4)
				{
					cod4 = 4;
				}
			}
			int codigoProducto = ((productoSeleccionado != null) ? productoSeleccionado.CodProducto : 0);
			CRGananciaxArticulo2_Clientes rpt = new CRGananciaxArticulo2_Clientes();
			frmRptGananciaxArticulo frm = new frmRptGananciaxArticulo();
			DataTable dt = ds.ReportGananciaxArticulo2_Clientes(codigoProducto, dtpDesde.Value, dtpHasta.Value, todos, cod1, cod2, cod3, cod4, todoscli, cli1, cli2, cli3, cli4, cli5, todostec, tec1, tec2, tec3, tec4, tec5).Tables[0];
			if (dt.Rows.Count > 0)
			{
				rpt.SetDataSource(dt);
				frm.crvRptGananciaxArticulo.ReportSource = rpt;
				frm.Show();
			}
			else
			{
				MessageBox.Show("No se encontraron resultados con los parámetros seleccionados", "Reporte de Ganancia por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			MessageBox.Show("Seleccione almacen...", "Reporte de Ganancia por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnReporteVendedor_Click(object sender, EventArgs e)
	{
		if (cmbAlmacenes.CheckedItems.Count > 0)
		{
			int todos = 0;
			int cod1 = 0;
			int cod2 = 0;
			int cod3 = 0;
			int cod4 = 0;
			int todosusu = 0;
			int usu1 = 0;
			int usu2 = 0;
			int usu3 = 0;
			int usu4 = 0;
			int usu5 = 0;
			if (txtVendedor.Items.Count > 0 && txtVendedor.Items.Count < 6)
			{
				usu1 = Convert.ToInt32((txtVendedor.Items.Count >= 1) ? txtVendedor.Items[0].Value : ((object)0));
				usu2 = Convert.ToInt32((txtVendedor.Items.Count >= 2) ? txtVendedor.Items[1].Value : ((object)0));
				usu3 = Convert.ToInt32((txtVendedor.Items.Count >= 3) ? txtVendedor.Items[2].Value : ((object)0));
				usu4 = Convert.ToInt32((txtVendedor.Items.Count >= 4) ? txtVendedor.Items[3].Value : ((object)0));
				usu5 = Convert.ToInt32((txtVendedor.Items.Count >= 5) ? txtVendedor.Items[4].Value : ((object)0));
			}
			else if (txtVendedor.Items.Count == 0)
			{
				todosusu = 1;
			}
			foreach (RadCheckedListDataItem a in cmbAlmacenes.CheckedItems)
			{
				if (Convert.ToInt32(a.Value) == 0)
				{
					todos = 1;
				}
				if (Convert.ToInt32(a.Value) == 1)
				{
					cod1 = 1;
				}
				if (Convert.ToInt32(a.Value) == 2)
				{
					cod2 = 2;
				}
				if (Convert.ToInt32(a.Value) == 3)
				{
					cod3 = 3;
				}
				if (Convert.ToInt32(a.Value) == 4)
				{
					cod4 = 4;
				}
			}
			int codigoProducto = ((productoSeleccionado != null) ? productoSeleccionado.CodProducto : 0);
			CRGananciaxArticulo2_Vendedores rpt = new CRGananciaxArticulo2_Vendedores();
			frmRptGananciaxArticulo frm = new frmRptGananciaxArticulo();
			DataTable dt = ds.ReportGananciaxArticulo2_Vendedores(codigoProducto, dtpDesde.Value, dtpHasta.Value, todos, cod1, cod2, cod3, cod4, todosusu, usu1, usu2, usu3, usu4, usu5).Tables[0];
			if (dt.Rows.Count > 0)
			{
				rpt.SetDataSource(dt);
				frm.crvRptGananciaxArticulo.ReportSource = rpt;
				frm.Show();
			}
			else
			{
				MessageBox.Show("No se encontraron resultados con los parámetros seleccionados", "Reporte de Ganancia por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			MessageBox.Show("Seleccione almacen...", "Reporte de Ganancia por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		this.txtProveedor = new Telerik.WinControls.UI.RadAutoCompleteBox();
		this.labelX9 = new DevComponents.DotNetBar.LabelX();
		this.line6 = new DevComponents.DotNetBar.Controls.Line();
		this.line5 = new DevComponents.DotNetBar.Controls.Line();
		this.txtFamilia = new Telerik.WinControls.UI.RadAutoCompleteBox();
		this.labelX7 = new DevComponents.DotNetBar.LabelX();
		this.txtMarca = new Telerik.WinControls.UI.RadAutoCompleteBox();
		this.labelX6 = new DevComponents.DotNetBar.LabelX();
		this.line4 = new DevComponents.DotNetBar.Controls.Line();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadCheckedDropDownList();
		this.labelX3 = new DevComponents.DotNetBar.LabelX();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.labelX2 = new DevComponents.DotNetBar.LabelX();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.labelX5 = new DevComponents.DotNetBar.LabelX();
		this.txtNombreProducto = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtBusquedaProducto = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.rbProductoEspecifico = new System.Windows.Forms.RadioButton();
		this.rbTodos = new System.Windows.Forms.RadioButton();
		this.btnCancelar = new DevComponents.DotNetBar.ButtonX();
		this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
		this.productoTab = new Telerik.WinControls.UI.RadPageViewPage();
		this.btnReporte = new DevComponents.DotNetBar.ButtonX();
		this.clienteTab = new Telerik.WinControls.UI.RadPageViewPage();
		this.btnReporteCliente = new DevComponents.DotNetBar.ButtonX();
		this.line8 = new DevComponents.DotNetBar.Controls.Line();
		this.txtTecnico = new Telerik.WinControls.UI.RadAutoCompleteBox();
		this.labelX10 = new DevComponents.DotNetBar.LabelX();
		this.txtCliente = new Telerik.WinControls.UI.RadAutoCompleteBox();
		this.labelX4 = new DevComponents.DotNetBar.LabelX();
		this.proveedorTab = new Telerik.WinControls.UI.RadPageViewPage();
		this.btnReporteVendedor = new DevComponents.DotNetBar.ButtonX();
		this.line7 = new DevComponents.DotNetBar.Controls.Line();
		this.txtVendedor = new Telerik.WinControls.UI.RadAutoCompleteBox();
		this.labelX8 = new DevComponents.DotNetBar.LabelX();
		((System.ComponentModel.ISupportInitialize)this.txtProveedor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFamilia).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMarca).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radPageView1).BeginInit();
		this.radPageView1.SuspendLayout();
		this.productoTab.SuspendLayout();
		this.clienteTab.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTecnico).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCliente).BeginInit();
		this.proveedorTab.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtVendedor).BeginInit();
		base.SuspendLayout();
		this.txtProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtProveedor.Location = new System.Drawing.Point(135, 9);
		this.txtProveedor.Multiline = true;
		this.txtProveedor.Name = "txtProveedor";
		this.txtProveedor.Size = new System.Drawing.Size(739, 26);
		this.txtProveedor.TabIndex = 38;
		this.labelX9.BackColor = System.Drawing.Color.Transparent;
		this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX9.Location = new System.Drawing.Point(24, 10);
		this.labelX9.Name = "labelX9";
		this.labelX9.Size = new System.Drawing.Size(104, 23);
		this.labelX9.TabIndex = 37;
		this.labelX9.Text = "PROVEEEDOR:";
		this.line6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line6.ForeColor = System.Drawing.Color.Goldenrod;
		this.line6.Location = new System.Drawing.Point(8, 129);
		this.line6.Name = "line6";
		this.line6.Size = new System.Drawing.Size(866, 10);
		this.line6.TabIndex = 33;
		this.line6.Text = "line6";
		this.line6.Thickness = 2;
		this.line5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line5.ForeColor = System.Drawing.Color.Goldenrod;
		this.line5.Location = new System.Drawing.Point(8, 85);
		this.line5.Name = "line5";
		this.line5.Size = new System.Drawing.Size(869, 10);
		this.line5.TabIndex = 32;
		this.line5.Text = "line5";
		this.line5.Thickness = 2;
		this.txtFamilia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtFamilia.Location = new System.Drawing.Point(136, 99);
		this.txtFamilia.Multiline = true;
		this.txtFamilia.Name = "txtFamilia";
		this.txtFamilia.Size = new System.Drawing.Size(741, 26);
		this.txtFamilia.TabIndex = 31;
		this.labelX7.BackColor = System.Drawing.Color.Transparent;
		this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX7.Location = new System.Drawing.Point(52, 100);
		this.labelX7.Name = "labelX7";
		this.labelX7.Size = new System.Drawing.Size(67, 23);
		this.labelX7.TabIndex = 30;
		this.labelX7.Text = "FAMILIA:";
		this.txtMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtMarca.Location = new System.Drawing.Point(136, 53);
		this.txtMarca.Multiline = true;
		this.txtMarca.Name = "txtMarca";
		this.txtMarca.Size = new System.Drawing.Size(737, 26);
		this.txtMarca.TabIndex = 29;
		this.labelX6.BackColor = System.Drawing.Color.Transparent;
		this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX6.Location = new System.Drawing.Point(56, 55);
		this.labelX6.Name = "labelX6";
		this.labelX6.Size = new System.Drawing.Size(65, 23);
		this.labelX6.TabIndex = 28;
		this.labelX6.Text = "MARCA:";
		this.line4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line4.ForeColor = System.Drawing.Color.Goldenrod;
		this.line4.Location = new System.Drawing.Point(8, 41);
		this.line4.Name = "line4";
		this.line4.Size = new System.Drawing.Size(866, 10);
		this.line4.TabIndex = 27;
		this.line4.Text = "line4";
		this.line4.Thickness = 2;
		this.cmbAlmacenes.Location = new System.Drawing.Point(135, 41);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(738, 20);
		this.cmbAlmacenes.TabIndex = 24;
		this.cmbAlmacenes.ThemeName = "ControlDefault";
		this.cmbAlmacenes.ItemCheckedChanged += new Telerik.WinControls.UI.RadCheckedListDataItemEventHandler(cmbAlmacenes_ItemCheckedChanged);
		this.labelX3.BackColor = System.Drawing.Color.Transparent;
		this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX3.Location = new System.Drawing.Point(47, 38);
		this.labelX3.Name = "labelX3";
		this.labelX3.Size = new System.Drawing.Size(75, 23);
		this.labelX3.TabIndex = 23;
		this.labelX3.Text = "ALMACEN:";
		this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(565, 13);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(143, 22);
		this.dtpHasta.TabIndex = 21;
		this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(290, 13);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(143, 22);
		this.dtpDesde.TabIndex = 20;
		this.labelX2.BackColor = System.Drawing.Color.Transparent;
		this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX2.Location = new System.Drawing.Point(501, 12);
		this.labelX2.Name = "labelX2";
		this.labelX2.Size = new System.Drawing.Size(58, 23);
		this.labelX2.TabIndex = 19;
		this.labelX2.Text = "HASTA:";
		this.labelX1.BackColor = System.Drawing.Color.Transparent;
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX1.Location = new System.Drawing.Point(222, 12);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(62, 23);
		this.labelX1.TabIndex = 18;
		this.labelX1.Text = "DESDE:";
		this.labelX5.BackColor = System.Drawing.Color.Transparent;
		this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX5.Location = new System.Drawing.Point(255, 80);
		this.labelX5.Name = "labelX5";
		this.labelX5.Size = new System.Drawing.Size(75, 23);
		this.labelX5.TabIndex = 17;
		this.labelX5.Text = "PRESIONE F1";
		this.txtNombreProducto.BackColor = System.Drawing.Color.White;
		this.txtNombreProducto.Border.Class = "TextBoxBorder";
		this.txtNombreProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombreProducto.ButtonCustom.Tooltip = "";
		this.txtNombreProducto.ButtonCustom2.Tooltip = "";
		this.txtNombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreProducto.Location = new System.Drawing.Point(33, 109);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.PreventEnterBeep = true;
		this.txtNombreProducto.ReadOnly = true;
		this.txtNombreProducto.Size = new System.Drawing.Size(840, 24);
		this.txtNombreProducto.TabIndex = 16;
		this.txtBusquedaProducto.Border.Class = "TextBoxBorder";
		this.txtBusquedaProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtBusquedaProducto.ButtonCustom.Tooltip = "";
		this.txtBusquedaProducto.ButtonCustom2.Tooltip = "";
		this.txtBusquedaProducto.Enabled = false;
		this.txtBusquedaProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBusquedaProducto.Location = new System.Drawing.Point(34, 78);
		this.txtBusquedaProducto.Name = "txtBusquedaProducto";
		this.txtBusquedaProducto.PreventEnterBeep = true;
		this.txtBusquedaProducto.Size = new System.Drawing.Size(215, 24);
		this.txtBusquedaProducto.TabIndex = 15;
		this.txtBusquedaProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBusquedaProducto_KeyDown);
		this.rbProductoEspecifico.AutoSize = true;
		this.rbProductoEspecifico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbProductoEspecifico.Location = new System.Drawing.Point(366, 82);
		this.rbProductoEspecifico.Name = "rbProductoEspecifico";
		this.rbProductoEspecifico.Size = new System.Drawing.Size(165, 17);
		this.rbProductoEspecifico.TabIndex = 14;
		this.rbProductoEspecifico.Text = "ELEGIR UN PRODUCTO";
		this.rbProductoEspecifico.UseVisualStyleBackColor = true;
		this.rbProductoEspecifico.CheckedChanged += new System.EventHandler(rbProductoEspecifico_CheckedChanged);
		this.rbTodos.AutoSize = true;
		this.rbTodos.Checked = true;
		this.rbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbTodos.Location = new System.Drawing.Point(565, 80);
		this.rbTodos.Name = "rbTodos";
		this.rbTodos.Size = new System.Drawing.Size(177, 17);
		this.rbTodos.TabIndex = 13;
		this.rbTodos.TabStop = true;
		this.rbTodos.Text = "TODOS LOS PRODUCTOS";
		this.rbTodos.UseVisualStyleBackColor = true;
		this.rbTodos.CheckedChanged += new System.EventHandler(rbTodos_CheckedChanged);
		this.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Location = new System.Drawing.Point(490, 145);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(105, 35);
		this.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "CANCELAR";
		this.radPageView1.Controls.Add(this.productoTab);
		this.radPageView1.Controls.Add(this.clienteTab);
		this.radPageView1.Controls.Add(this.proveedorTab);
		this.radPageView1.DefaultPage = this.productoTab;
		this.radPageView1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.radPageView1.Location = new System.Drawing.Point(0, 152);
		this.radPageView1.Name = "radPageView1";
		this.radPageView1.SelectedPage = this.proveedorTab;
		this.radPageView1.Size = new System.Drawing.Size(898, 233);
		this.radPageView1.TabIndex = 3;
		this.productoTab.Controls.Add(this.btnCancelar);
		this.productoTab.Controls.Add(this.txtProveedor);
		this.productoTab.Controls.Add(this.btnReporte);
		this.productoTab.Controls.Add(this.labelX9);
		this.productoTab.Controls.Add(this.line6);
		this.productoTab.Controls.Add(this.line5);
		this.productoTab.Controls.Add(this.txtFamilia);
		this.productoTab.Controls.Add(this.labelX7);
		this.productoTab.Controls.Add(this.txtMarca);
		this.productoTab.Controls.Add(this.labelX6);
		this.productoTab.Controls.Add(this.line4);
		this.productoTab.ItemSize = new System.Drawing.SizeF(111f, 28f);
		this.productoTab.Location = new System.Drawing.Point(10, 37);
		this.productoTab.Name = "productoTab";
		this.productoTab.Size = new System.Drawing.Size(877, 185);
		this.productoTab.Text = "Filtro por Producto";
		this.btnReporte.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnReporte.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.Location = new System.Drawing.Point(379, 145);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(105, 35);
		this.btnReporte.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnReporte.TabIndex = 1;
		this.btnReporte.Text = "REPORTE";
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.clienteTab.Controls.Add(this.btnReporteCliente);
		this.clienteTab.Controls.Add(this.line8);
		this.clienteTab.Controls.Add(this.txtTecnico);
		this.clienteTab.Controls.Add(this.labelX10);
		this.clienteTab.Controls.Add(this.txtCliente);
		this.clienteTab.Controls.Add(this.labelX4);
		this.clienteTab.ItemSize = new System.Drawing.SizeF(100f, 28f);
		this.clienteTab.Location = new System.Drawing.Point(10, 37);
		this.clienteTab.Name = "clienteTab";
		this.clienteTab.Size = new System.Drawing.Size(877, 185);
		this.clienteTab.Text = "Filtro por Cliente";
		this.btnReporteCliente.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnReporteCliente.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnReporteCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporteCliente.Location = new System.Drawing.Point(386, 118);
		this.btnReporteCliente.Name = "btnReporteCliente";
		this.btnReporteCliente.Size = new System.Drawing.Size(105, 35);
		this.btnReporteCliente.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnReporteCliente.TabIndex = 41;
		this.btnReporteCliente.Text = "REPORTE";
		this.btnReporteCliente.Click += new System.EventHandler(btnReporteCliente_Click);
		this.line8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line8.ForeColor = System.Drawing.Color.Goldenrod;
		this.line8.Location = new System.Drawing.Point(3, 96);
		this.line8.Name = "line8";
		this.line8.Size = new System.Drawing.Size(866, 10);
		this.line8.TabIndex = 40;
		this.line8.Text = "line8";
		this.line8.Thickness = 2;
		this.txtTecnico.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtTecnico.Location = new System.Drawing.Point(122, 59);
		this.txtTecnico.Multiline = true;
		this.txtTecnico.Name = "txtTecnico";
		this.txtTecnico.Size = new System.Drawing.Size(741, 26);
		this.txtTecnico.TabIndex = 28;
		this.labelX10.BackColor = System.Drawing.Color.Transparent;
		this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX10.Location = new System.Drawing.Point(34, 60);
		this.labelX10.Name = "labelX10";
		this.labelX10.Size = new System.Drawing.Size(68, 23);
		this.labelX10.TabIndex = 27;
		this.labelX10.Text = "TECNICO:";
		this.txtCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtCliente.Location = new System.Drawing.Point(122, 22);
		this.txtCliente.Multiline = true;
		this.txtCliente.Name = "txtCliente";
		this.txtCliente.Size = new System.Drawing.Size(741, 26);
		this.txtCliente.TabIndex = 28;
		this.labelX4.BackColor = System.Drawing.Color.Transparent;
		this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX4.Location = new System.Drawing.Point(34, 23);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(68, 23);
		this.labelX4.TabIndex = 27;
		this.labelX4.Text = "CLIENTE:";
		this.proveedorTab.Controls.Add(this.btnReporteVendedor);
		this.proveedorTab.Controls.Add(this.line7);
		this.proveedorTab.Controls.Add(this.txtVendedor);
		this.proveedorTab.Controls.Add(this.labelX8);
		this.proveedorTab.ItemSize = new System.Drawing.SizeF(114f, 28f);
		this.proveedorTab.Location = new System.Drawing.Point(10, 37);
		this.proveedorTab.Name = "proveedorTab";
		this.proveedorTab.Size = new System.Drawing.Size(877, 185);
		this.proveedorTab.Text = "Filtro por Vendedor";
		this.btnReporteVendedor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnReporteVendedor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnReporteVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporteVendedor.Location = new System.Drawing.Point(386, 115);
		this.btnReporteVendedor.Name = "btnReporteVendedor";
		this.btnReporteVendedor.Size = new System.Drawing.Size(105, 35);
		this.btnReporteVendedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnReporteVendedor.TabIndex = 40;
		this.btnReporteVendedor.Text = "REPORTE";
		this.btnReporteVendedor.Click += new System.EventHandler(btnReporteVendedor_Click);
		this.line7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line7.ForeColor = System.Drawing.Color.Goldenrod;
		this.line7.Location = new System.Drawing.Point(6, 62);
		this.line7.Name = "line7";
		this.line7.Size = new System.Drawing.Size(866, 10);
		this.line7.TabIndex = 39;
		this.line7.Text = "line7";
		this.line7.Thickness = 2;
		this.txtVendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtVendedor.Location = new System.Drawing.Point(127, 18);
		this.txtVendedor.Multiline = true;
		this.txtVendedor.Name = "txtVendedor";
		this.txtVendedor.Size = new System.Drawing.Size(741, 26);
		this.txtVendedor.TabIndex = 38;
		this.labelX8.BackColor = System.Drawing.Color.Transparent;
		this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX8.Location = new System.Drawing.Point(29, 19);
		this.labelX8.Name = "labelX8";
		this.labelX8.Size = new System.Drawing.Size(90, 23);
		this.labelX8.TabIndex = 37;
		this.labelX8.Text = "VENDEDOR:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancelar;
		base.ClientSize = new System.Drawing.Size(898, 385);
		base.Controls.Add(this.radPageView1);
		base.Controls.Add(this.labelX1);
		base.Controls.Add(this.cmbAlmacenes);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.labelX2);
		base.Controls.Add(this.rbTodos);
		base.Controls.Add(this.labelX3);
		base.Controls.Add(this.rbProductoEspecifico);
		base.Controls.Add(this.labelX5);
		base.Controls.Add(this.txtNombreProducto);
		base.Controls.Add(this.txtBusquedaProducto);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamGananciaxArticulo2";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Utilidad Bruta x Producto";
		base.Load += new System.EventHandler(frmParamGananciaxArticulo_Load);
		((System.ComponentModel.ISupportInitialize)this.txtProveedor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFamilia).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMarca).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radPageView1).EndInit();
		this.radPageView1.ResumeLayout(false);
		this.productoTab.ResumeLayout(false);
		this.clienteTab.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtTecnico).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCliente).EndInit();
		this.proveedorTab.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtVendedor).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
