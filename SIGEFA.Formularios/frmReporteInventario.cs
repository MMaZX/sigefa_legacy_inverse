using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmReporteInventario : Office2007Form
{
	private clsAdmAlmacen AdmAlm = new clsAdmAlmacen();

	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto pro = new clsProducto();

	private clsAlmacen alma = new clsAlmacen();

	public int codArticulo1;

	public int codArticulo2;

	public int CodLista = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupPanel groupPanel3;

	private GroupPanel groupPanel4;

	private GroupPanel groupPanel1;

	private Button btnReporte;

	private Button btnSalir;

	private Label label2;

	private Label label1;

	private RadioButton rbRango;

	private RadioButton rbTodos;

	private CheckBox cbCosto;

	private ComboBox cmbAlmacen;

	private Label label3;

	private Label label4;

	private CheckBox cbActivos;

	private CheckBox cbCero;

	private CheckBox cbLoteSerie;

	private ComboBox cmbOrden;

	private GroupPanel groupPanel2;

	private CheckBox cbTipos;

	private CheckBox cbGrupos;

	private CheckBox cbLineas;

	private CheckBox cbFamilias;

	private CheckBox cbArticulos;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CustomValidator customValidator2;

	public TextBox txtFin;

	public TextBox txtInicio;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo1;

	private TextBox txtTipoCambioManual;

	private CheckBox chkTipoCambioManual;

	public frmReporteInventario()
	{
		InitializeComponent();
	}

	private void frmReporteInventario_Load(object sender, EventArgs e)
	{
		carga_almacen();
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
		rbTodos.Checked = true;
		cbArticulos.Checked = true;
		cmbOrden.SelectedIndex = 0;
	}

	private void carga_almacen()
	{
		DataTable aux = AdmAlm.CargaAlmacenes(frmLogin.iNivelUser, frmLogin.iCodEmpresa, frmLogin.iCodUser);
		aux.Rows.Add(-1, "TODOS LOS ALMACENES", "");
		cmbAlmacen.DataSource = aux;
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedIndex = -1;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			if (chkTipoCambioManual.Checked && txtTipoCambioManual.Text == "")
			{
				MessageBox.Show("Ingresa un tipo de cambio numerico, no puede ser vacio", "Tipo De Cambio Manual", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtTipoCambioManual.Focus();
				return;
			}
			rptInventario frm = new rptInventario();
			frm.alma = Convert.ToInt32(cmbAlmacen.SelectedValue);
			frm.costo = cbCosto.Checked;
			frm.art = cbArticulos.Checked;
			frm.fam = cbFamilias.Checked;
			frm.lin = cbLineas.Checked;
			frm.gru = cbGrupos.Checked;
			frm.tip = cbTipos.Checked;
			frm.todo = rbTodos.Checked;
			frm.art1 = codArticulo1;
			frm.art2 = codArticulo2;
			frm.cero = cbCero.Checked;
			frm.activos = cbActivos.Checked;
			if (chkTipoCambioManual.Checked)
			{
				frm.tipocambiomanual = Convert.ToDouble(txtTipoCambioManual.Text);
			}
			else
			{
				frm.tipocambiomanual = 0.0;
			}
			frm.orden = Convert.ToInt32(cmbOrden.SelectedIndex);
			frm.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Reporte_Inventario", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtInicio_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosListaReport frm = new frmProductosListaReport();
			frm.Proceso = 3;
			frm.Inicio = 0;
			frm.codAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
			frm.ShowDialog();
			codArticulo1 = frm.pro.CodProducto;
			txtInicio.Text = frm.pro.Referencia;
		}
	}

	private void rbRango_CheckedChanged(object sender, EventArgs e)
	{
		txtInicio.Enabled = rbRango.Checked;
		txtFin.Enabled = rbRango.Checked;
		label1.Enabled = rbRango.Checked;
		label2.Enabled = rbRango.Checked;
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Text != "")
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Text != "")
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Text != "")
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (codArticulo2 < codArticulo1)
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void txtFin_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosListaReport frm = new frmProductosListaReport();
			frm.Proceso = 3;
			frm.Inicio = codArticulo1;
			frm.codAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
			frm.ShowDialog();
			codArticulo2 = frm.pro.CodProducto;
			txtFin.Text = frm.pro.Referencia;
		}
	}

	private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtFin.Text != "")
		{
			if (BuscaProducto(txtFin.Text))
			{
				ProcessTabKey(forward: true);
				codArticulo2 = pro.CodProducto;
				txtFin.Text = pro.Referencia;
			}
			else
			{
				MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtInicio.Text != "")
		{
			if (BuscaProducto(txtInicio.Text))
			{
				ProcessTabKey(forward: true);
				codArticulo1 = pro.CodProducto;
				txtInicio.Text = pro.Referencia;
			}
			else
			{
				MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaProducto(string referencia)
	{
		pro = AdmPro.CargaProductoDetalleR(referencia, Convert.ToInt32(cmbAlmacen.SelectedValue), 2, CodLista);
		if (pro != null)
		{
			return true;
		}
		return false;
	}

	private void cbCosto_CheckedChanged(object sender, EventArgs e)
	{
		chkTipoCambioManual.Enabled = cbCosto.Checked;
		if (!cbCosto.Checked)
		{
			txtTipoCambioManual.Enabled = cbCosto.Checked;
			txtTipoCambioManual.Text = "";
			chkTipoCambioManual.Checked = cbCosto.Checked;
		}
	}

	private void chkTipoCambioManual_CheckedChanged(object sender, EventArgs e)
	{
		txtTipoCambioManual.Enabled = chkTipoCambioManual.Checked;
		if (!chkTipoCambioManual.Checked)
		{
			txtTipoCambioManual.Text = "";
		}
	}

	private void txtTipoCambioManual_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtTipoCambioManual.Text != "")
			{
				double aux = Convert.ToDouble(txtTipoCambioManual.Text ?? "0");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error En Tipo De Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			if (txtTipoCambioManual.Text.Length > 0)
			{
				txtTipoCambioManual.Text = txtTipoCambioManual.Text.Remove(txtTipoCambioManual.Text.Length - 1);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmReporteInventario));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.cbCosto = new System.Windows.Forms.CheckBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.cmbOrden = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.cbActivos = new System.Windows.Forms.CheckBox();
		this.cbCero = new System.Windows.Forms.CheckBox();
		this.cbLoteSerie = new System.Windows.Forms.CheckBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.txtFin = new System.Windows.Forms.TextBox();
		this.txtInicio = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.rbRango = new System.Windows.Forms.RadioButton();
		this.rbTodos = new System.Windows.Forms.RadioButton();
		this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.cbTipos = new System.Windows.Forms.CheckBox();
		this.cbGrupos = new System.Windows.Forms.CheckBox();
		this.cbLineas = new System.Windows.Forms.CheckBox();
		this.cbFamilias = new System.Windows.Forms.CheckBox();
		this.cbArticulos = new System.Windows.Forms.CheckBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.cachedCRCuotasPrestamo1 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.chkTipoCambioManual = new System.Windows.Forms.CheckBox();
		this.txtTipoCambioManual = new System.Windows.Forms.TextBox();
		this.groupPanel3.SuspendLayout();
		this.groupPanel4.SuspendLayout();
		this.groupPanel1.SuspendLayout();
		this.groupPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList1.Images.SetKeyName(6, "Custom-Icon-Design-Pretty-Office-6-Custom-reports.ico");
		this.imageList1.Images.SetKeyName(7, "Custom-Icon-Design-Pretty-Office-4-Report.ico");
		this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel3.Controls.Add(this.txtTipoCambioManual);
		this.groupPanel3.Controls.Add(this.chkTipoCambioManual);
		this.groupPanel3.Controls.Add(this.cbCosto);
		this.groupPanel3.Controls.Add(this.cmbAlmacen);
		this.groupPanel3.Controls.Add(this.label3);
		this.groupPanel3.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupPanel3.Location = new System.Drawing.Point(0, 0);
		this.groupPanel3.Name = "groupPanel3";
		this.groupPanel3.Size = new System.Drawing.Size(419, 125);
		this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.groupPanel3.Style.BackColorGradientAngle = 90;
		this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel3.Style.BorderBottomWidth = 1;
		this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel3.Style.BorderLeftWidth = 1;
		this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel3.Style.BorderRightWidth = 1;
		this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel3.Style.BorderTopWidth = 1;
		this.groupPanel3.Style.CornerDiameter = 4;
		this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
		this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
		this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
		this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel3.TabIndex = 3;
		this.groupPanel3.Text = "Tipo de Consulta";
		this.cbCosto.AutoSize = true;
		this.cbCosto.BackColor = System.Drawing.Color.Transparent;
		this.cbCosto.Location = new System.Drawing.Point(145, 39);
		this.cbCosto.Name = "cbCosto";
		this.cbCosto.Size = new System.Drawing.Size(74, 17);
		this.cbCosto.TabIndex = 2;
		this.cbCosto.Text = "Con costo";
		this.cbCosto.UseVisualStyleBackColor = false;
		this.cbCosto.CheckedChanged += new System.EventHandler(cbCosto_CheckedChanged);
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(145, 12);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(206, 21);
		this.cmbAlmacen.TabIndex = 1;
		this.superValidator1.SetValidator1(this.cmbAlmacen, this.customValidator1);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(52, 15);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(66, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "Por almacén";
		this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel4.Controls.Add(this.cmbOrden);
		this.groupPanel4.Controls.Add(this.label4);
		this.groupPanel4.Controls.Add(this.cbActivos);
		this.groupPanel4.Controls.Add(this.cbCero);
		this.groupPanel4.Controls.Add(this.cbLoteSerie);
		this.groupPanel4.Controls.Add(this.btnReporte);
		this.groupPanel4.Controls.Add(this.btnSalir);
		this.groupPanel4.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupPanel4.Location = new System.Drawing.Point(0, 284);
		this.groupPanel4.Name = "groupPanel4";
		this.groupPanel4.Size = new System.Drawing.Size(419, 80);
		this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.groupPanel4.Style.BackColorGradientAngle = 90;
		this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel4.Style.BorderBottomWidth = 1;
		this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel4.Style.BorderLeftWidth = 1;
		this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel4.Style.BorderRightWidth = 1;
		this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel4.Style.BorderTopWidth = 1;
		this.groupPanel4.Style.CornerDiameter = 4;
		this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
		this.groupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
		this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
		this.groupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel4.TabIndex = 6;
		this.cmbOrden.DisplayMember = "10,2,3";
		this.cmbOrden.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbOrden.FormattingEnabled = true;
		this.cmbOrden.Items.AddRange(new object[3] { "Código", "Nombre", "Stock" });
		this.cmbOrden.Location = new System.Drawing.Point(251, 1);
		this.cmbOrden.Name = "cmbOrden";
		this.cmbOrden.Size = new System.Drawing.Size(139, 21);
		this.cmbOrden.TabIndex = 26;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(173, 7);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(72, 13);
		this.label4.TabIndex = 25;
		this.label4.Text = "Ordenado por";
		this.cbActivos.AutoSize = true;
		this.cbActivos.BackColor = System.Drawing.Color.Transparent;
		this.cbActivos.Location = new System.Drawing.Point(9, 49);
		this.cbActivos.Name = "cbActivos";
		this.cbActivos.Size = new System.Drawing.Size(128, 17);
		this.cbActivos.TabIndex = 24;
		this.cbActivos.Text = "Solo artículos activos";
		this.cbActivos.UseVisualStyleBackColor = false;
		this.cbCero.AutoSize = true;
		this.cbCero.BackColor = System.Drawing.Color.Transparent;
		this.cbCero.Location = new System.Drawing.Point(9, 26);
		this.cbCero.Name = "cbCero";
		this.cbCero.Size = new System.Drawing.Size(101, 17);
		this.cbCero.TabIndex = 23;
		this.cbCero.Text = "Suprimir stock 0";
		this.cbCero.UseVisualStyleBackColor = false;
		this.cbLoteSerie.AutoSize = true;
		this.cbLoteSerie.BackColor = System.Drawing.Color.Transparent;
		this.cbLoteSerie.Location = new System.Drawing.Point(9, 3);
		this.cbLoteSerie.Name = "cbLoteSerie";
		this.cbLoteSerie.Size = new System.Drawing.Size(98, 17);
		this.cbLoteSerie.TabIndex = 22;
		this.cbLoteSerie.Text = "Con serie y lote";
		this.cbLoteSerie.UseVisualStyleBackColor = false;
		this.cbLoteSerie.Visible = false;
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.ImageIndex = 6;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(244, 36);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(79, 35);
		this.btnReporte.TabIndex = 21;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(329, 36);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 35);
		this.btnSalir.TabIndex = 2;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel1.Controls.Add(this.txtFin);
		this.groupPanel1.Controls.Add(this.txtInicio);
		this.groupPanel1.Controls.Add(this.label2);
		this.groupPanel1.Controls.Add(this.label1);
		this.groupPanel1.Controls.Add(this.rbRango);
		this.groupPanel1.Controls.Add(this.rbTodos);
		this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.groupPanel1.Location = new System.Drawing.Point(177, 125);
		this.groupPanel1.Name = "groupPanel1";
		this.groupPanel1.Size = new System.Drawing.Size(242, 159);
		this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.groupPanel1.Style.BackColorGradientAngle = 90;
		this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderBottomWidth = 1;
		this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderLeftWidth = 1;
		this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderRightWidth = 1;
		this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderTopWidth = 1;
		this.groupPanel1.Style.CornerDiameter = 4;
		this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
		this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
		this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
		this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel1.TabIndex = 8;
		this.groupPanel1.Text = "Por Articulos";
		this.txtFin.Enabled = false;
		this.txtFin.Location = new System.Drawing.Point(94, 99);
		this.txtFin.Name = "txtFin";
		this.txtFin.Size = new System.Drawing.Size(100, 20);
		this.txtFin.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtFin, this.customValidator3);
		this.superValidator1.SetValidator2(this.txtFin, this.customValidator4);
		this.txtFin.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFin_KeyDown);
		this.txtFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFin_KeyPress);
		this.txtInicio.Enabled = false;
		this.txtInicio.Location = new System.Drawing.Point(94, 73);
		this.txtInicio.Name = "txtInicio";
		this.txtInicio.Size = new System.Drawing.Size(100, 20);
		this.txtInicio.TabIndex = 4;
		this.superValidator1.SetValidator1(this.txtInicio, this.customValidator2);
		this.txtInicio.KeyDown += new System.Windows.Forms.KeyEventHandler(txtInicio_KeyDown);
		this.txtInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInicio_KeyPress);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Enabled = false;
		this.label2.Location = new System.Drawing.Point(61, 102);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(27, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Fin :";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Enabled = false;
		this.label1.Location = new System.Drawing.Point(50, 76);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(38, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Inicio :";
		this.rbRango.AutoSize = true;
		this.rbRango.BackColor = System.Drawing.Color.Transparent;
		this.rbRango.Location = new System.Drawing.Point(19, 39);
		this.rbRango.Name = "rbRango";
		this.rbRango.Size = new System.Drawing.Size(57, 17);
		this.rbRango.TabIndex = 1;
		this.rbRango.TabStop = true;
		this.rbRango.Text = "Rango";
		this.rbRango.UseVisualStyleBackColor = false;
		this.rbRango.CheckedChanged += new System.EventHandler(rbRango_CheckedChanged);
		this.rbTodos.AutoSize = true;
		this.rbTodos.BackColor = System.Drawing.Color.Transparent;
		this.rbTodos.Location = new System.Drawing.Point(19, 16);
		this.rbTodos.Name = "rbTodos";
		this.rbTodos.Size = new System.Drawing.Size(115, 17);
		this.rbTodos.TabIndex = 0;
		this.rbTodos.TabStop = true;
		this.rbTodos.Text = "Todos los artículos";
		this.rbTodos.UseVisualStyleBackColor = false;
		this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel2.Controls.Add(this.cbTipos);
		this.groupPanel2.Controls.Add(this.cbGrupos);
		this.groupPanel2.Controls.Add(this.cbLineas);
		this.groupPanel2.Controls.Add(this.cbFamilias);
		this.groupPanel2.Controls.Add(this.cbArticulos);
		this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.groupPanel2.Location = new System.Drawing.Point(0, 125);
		this.groupPanel2.Name = "groupPanel2";
		this.groupPanel2.Size = new System.Drawing.Size(171, 159);
		this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.groupPanel2.Style.BackColorGradientAngle = 90;
		this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderBottomWidth = 1;
		this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderLeftWidth = 1;
		this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderRightWidth = 1;
		this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderTopWidth = 1;
		this.groupPanel2.Style.CornerDiameter = 4;
		this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
		this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
		this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
		this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel2.TabIndex = 7;
		this.groupPanel2.Text = "Parametros";
		this.cbTipos.AutoSize = true;
		this.cbTipos.BackColor = System.Drawing.Color.Transparent;
		this.cbTipos.Location = new System.Drawing.Point(9, 108);
		this.cbTipos.Name = "cbTipos";
		this.cbTipos.Size = new System.Drawing.Size(106, 17);
		this.cbTipos.TabIndex = 4;
		this.cbTipos.Text = "Tipo de artículos";
		this.cbTipos.UseVisualStyleBackColor = false;
		this.cbGrupos.AutoSize = true;
		this.cbGrupos.BackColor = System.Drawing.Color.Transparent;
		this.cbGrupos.Location = new System.Drawing.Point(9, 85);
		this.cbGrupos.Name = "cbGrupos";
		this.cbGrupos.Size = new System.Drawing.Size(60, 17);
		this.cbGrupos.TabIndex = 3;
		this.cbGrupos.Text = "Grupos";
		this.cbGrupos.UseVisualStyleBackColor = false;
		this.cbLineas.AutoSize = true;
		this.cbLineas.BackColor = System.Drawing.Color.Transparent;
		this.cbLineas.Location = new System.Drawing.Point(9, 62);
		this.cbLineas.Name = "cbLineas";
		this.cbLineas.Size = new System.Drawing.Size(57, 17);
		this.cbLineas.TabIndex = 2;
		this.cbLineas.Text = "Lineas";
		this.cbLineas.UseVisualStyleBackColor = false;
		this.cbFamilias.AutoSize = true;
		this.cbFamilias.BackColor = System.Drawing.Color.Transparent;
		this.cbFamilias.Location = new System.Drawing.Point(9, 39);
		this.cbFamilias.Name = "cbFamilias";
		this.cbFamilias.Size = new System.Drawing.Size(63, 17);
		this.cbFamilias.TabIndex = 1;
		this.cbFamilias.Text = "Familias";
		this.cbFamilias.UseVisualStyleBackColor = false;
		this.cbArticulos.AutoSize = true;
		this.cbArticulos.BackColor = System.Drawing.Color.Transparent;
		this.cbArticulos.Enabled = false;
		this.cbArticulos.Location = new System.Drawing.Point(9, 16);
		this.cbArticulos.Name = "cbArticulos";
		this.cbArticulos.Size = new System.Drawing.Size(68, 17);
		this.cbArticulos.TabIndex = 0;
		this.cbArticulos.Text = "Artículos";
		this.cbArticulos.UseVisualStyleBackColor = false;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Debe elegir un almacén.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator3.ErrorMessage = "Debe elegir un artículo final.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator4.ErrorMessage = "Rango no valido.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator2.ErrorMessage = "Debe elegir un artículo inicial.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.chkTipoCambioManual.AutoSize = true;
		this.chkTipoCambioManual.BackColor = System.Drawing.Color.Transparent;
		this.chkTipoCambioManual.Enabled = false;
		this.chkTipoCambioManual.Location = new System.Drawing.Point(145, 73);
		this.chkTipoCambioManual.Name = "chkTipoCambioManual";
		this.chkTipoCambioManual.Size = new System.Drawing.Size(126, 17);
		this.chkTipoCambioManual.TabIndex = 3;
		this.chkTipoCambioManual.Text = "Tipo Cambio Manual:";
		this.chkTipoCambioManual.UseVisualStyleBackColor = false;
		this.chkTipoCambioManual.CheckedChanged += new System.EventHandler(chkTipoCambioManual_CheckedChanged);
		this.txtTipoCambioManual.Enabled = false;
		this.txtTipoCambioManual.Location = new System.Drawing.Point(277, 71);
		this.txtTipoCambioManual.Name = "txtTipoCambioManual";
		this.txtTipoCambioManual.Size = new System.Drawing.Size(74, 20);
		this.txtTipoCambioManual.TabIndex = 4;
		this.txtTipoCambioManual.TextChanged += new System.EventHandler(txtTipoCambioManual_TextChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(419, 364);
		base.Controls.Add(this.groupPanel1);
		base.Controls.Add(this.groupPanel2);
		base.Controls.Add(this.groupPanel4);
		base.Controls.Add(this.groupPanel3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmReporteInventario";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte Inventario";
		base.Load += new System.EventHandler(frmReporteInventario_Load);
		this.groupPanel3.ResumeLayout(false);
		this.groupPanel3.PerformLayout();
		this.groupPanel4.ResumeLayout(false);
		this.groupPanel4.PerformLayout();
		this.groupPanel1.ResumeLayout(false);
		this.groupPanel1.PerformLayout();
		this.groupPanel2.ResumeLayout(false);
		this.groupPanel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
