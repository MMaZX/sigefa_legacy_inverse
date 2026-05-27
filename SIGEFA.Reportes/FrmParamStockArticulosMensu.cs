using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Formularios;

namespace SIGEFA.Reportes;

public class FrmParamStockArticulosMensu : Office2007Form
{
	private clsAdmAlmacen AdmAlm = new clsAdmAlmacen();

	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto pro = new clsProducto();

	private clsAlmacen alma = new clsAlmacen();

	public int codArticulo1;

	public int codArticulo2;

	private IContainer components = null;

	private GroupPanel groupPanel1;

	public TextBox txtFin;

	public TextBox txtInicio;

	private Label label2;

	private Label label1;

	private RadioButton rbRango;

	private RadioButton rbTodos;

	private GroupPanel groupPanel2;

	private CheckBox cbFamilias;

	private CheckBox cbArticulos;

	private Button btnReporte;

	private Button btnSalir;

	private GroupPanel groupPanel3;

	private ComboBox cmbAlmacen;

	private Label label3;

	private Label label5;

	private ComboBox cbMeses;

	private ComboBox comboBox1;

	private Label label6;

	private GroupPanel groupPanel4;

	public FrmParamStockArticulosMensu()
	{
		InitializeComponent();
	}

	private void FrmParamStockArticulosMensu_Load(object sender, EventArgs e)
	{
		carga_almacen();
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
		rbTodos.Checked = true;
		cbArticulos.Checked = true;
	}

	private void rbRango_CheckedChanged(object sender, EventArgs e)
	{
		txtInicio.Enabled = rbRango.Checked;
		txtFin.Enabled = rbRango.Checked;
		label1.Enabled = rbRango.Checked;
		label2.Enabled = rbRango.Checked;
	}

	private void carga_almacen()
	{
		cmbAlmacen.DataSource = AdmAlm.CargaAlmacenes(frmLogin.iNivelUser, frmLogin.iCodEmpresa, frmLogin.iCodUser);
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedIndex = -1;
	}

	private void btnSalir_Click(object sender, EventArgs e)
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
		this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.txtFin = new System.Windows.Forms.TextBox();
		this.txtInicio = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.rbRango = new System.Windows.Forms.RadioButton();
		this.rbTodos = new System.Windows.Forms.RadioButton();
		this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.cbFamilias = new System.Windows.Forms.CheckBox();
		this.cbArticulos = new System.Windows.Forms.CheckBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.cbMeses = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.groupPanel1.SuspendLayout();
		this.groupPanel2.SuspendLayout();
		this.groupPanel3.SuspendLayout();
		this.groupPanel4.SuspendLayout();
		base.SuspendLayout();
		this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel1.Controls.Add(this.txtFin);
		this.groupPanel1.Controls.Add(this.txtInicio);
		this.groupPanel1.Controls.Add(this.label2);
		this.groupPanel1.Controls.Add(this.label1);
		this.groupPanel1.Controls.Add(this.rbRango);
		this.groupPanel1.Controls.Add(this.rbTodos);
		this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.groupPanel1.Location = new System.Drawing.Point(175, 113);
		this.groupPanel1.Name = "groupPanel1";
		this.groupPanel1.Size = new System.Drawing.Size(242, 109);
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
		this.groupPanel1.TabIndex = 12;
		this.groupPanel1.Text = "Por Articulos";
		this.txtFin.Enabled = false;
		this.txtFin.Location = new System.Drawing.Point(127, 56);
		this.txtFin.Name = "txtFin";
		this.txtFin.Size = new System.Drawing.Size(100, 20);
		this.txtFin.TabIndex = 5;
		this.txtInicio.Enabled = false;
		this.txtInicio.Location = new System.Drawing.Point(127, 30);
		this.txtInicio.Name = "txtInicio";
		this.txtInicio.Size = new System.Drawing.Size(100, 20);
		this.txtInicio.TabIndex = 4;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Enabled = false;
		this.label2.Location = new System.Drawing.Point(94, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(27, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Fin :";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Enabled = false;
		this.label1.Location = new System.Drawing.Point(83, 33);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(38, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Inicio :";
		this.rbRango.AutoSize = true;
		this.rbRango.BackColor = System.Drawing.Color.Transparent;
		this.rbRango.Location = new System.Drawing.Point(20, 28);
		this.rbRango.Name = "rbRango";
		this.rbRango.Size = new System.Drawing.Size(57, 17);
		this.rbRango.TabIndex = 1;
		this.rbRango.TabStop = true;
		this.rbRango.Text = "Rango";
		this.rbRango.UseVisualStyleBackColor = false;
		this.rbRango.CheckedChanged += new System.EventHandler(rbRango_CheckedChanged);
		this.rbTodos.AutoSize = true;
		this.rbTodos.BackColor = System.Drawing.Color.Transparent;
		this.rbTodos.Location = new System.Drawing.Point(20, 5);
		this.rbTodos.Name = "rbTodos";
		this.rbTodos.Size = new System.Drawing.Size(115, 17);
		this.rbTodos.TabIndex = 0;
		this.rbTodos.TabStop = true;
		this.rbTodos.Text = "Todos los artículos";
		this.rbTodos.UseVisualStyleBackColor = false;
		this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel2.Controls.Add(this.cbFamilias);
		this.groupPanel2.Controls.Add(this.cbArticulos);
		this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.groupPanel2.Location = new System.Drawing.Point(0, 113);
		this.groupPanel2.Name = "groupPanel2";
		this.groupPanel2.Size = new System.Drawing.Size(171, 109);
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
		this.groupPanel2.TabIndex = 11;
		this.groupPanel2.Text = "Parametros";
		this.cbFamilias.AutoSize = true;
		this.cbFamilias.BackColor = System.Drawing.Color.Transparent;
		this.cbFamilias.Location = new System.Drawing.Point(9, 29);
		this.cbFamilias.Name = "cbFamilias";
		this.cbFamilias.Size = new System.Drawing.Size(63, 17);
		this.cbFamilias.TabIndex = 1;
		this.cbFamilias.Text = "Familias";
		this.cbFamilias.UseVisualStyleBackColor = false;
		this.cbArticulos.AutoSize = true;
		this.cbArticulos.BackColor = System.Drawing.Color.Transparent;
		this.cbArticulos.Enabled = false;
		this.cbArticulos.Location = new System.Drawing.Point(9, 6);
		this.cbArticulos.Name = "cbArticulos";
		this.cbArticulos.Size = new System.Drawing.Size(68, 17);
		this.cbArticulos.TabIndex = 0;
		this.cbArticulos.Text = "Artículos";
		this.cbArticulos.UseVisualStyleBackColor = false;
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.ImageIndex = 6;
		this.btnReporte.Location = new System.Drawing.Point(242, 8);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(79, 35);
		this.btnReporte.TabIndex = 21;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.Location = new System.Drawing.Point(327, 8);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 35);
		this.btnSalir.TabIndex = 2;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel3.Controls.Add(this.comboBox1);
		this.groupPanel3.Controls.Add(this.label6);
		this.groupPanel3.Controls.Add(this.cbMeses);
		this.groupPanel3.Controls.Add(this.label5);
		this.groupPanel3.Controls.Add(this.cmbAlmacen);
		this.groupPanel3.Controls.Add(this.label3);
		this.groupPanel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupPanel3.Location = new System.Drawing.Point(0, 0);
		this.groupPanel3.Name = "groupPanel3";
		this.groupPanel3.Size = new System.Drawing.Size(417, 113);
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
		this.groupPanel3.TabIndex = 9;
		this.groupPanel3.Text = "Tipo de Consulta";
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.Enabled = false;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(146, 36);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(206, 21);
		this.cmbAlmacen.TabIndex = 1;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(89, 39);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(48, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "Almacen";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(40, 12);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(97, 13);
		this.label5.TabIndex = 3;
		this.label5.Text = "Meses de Consulta";
		this.cbMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbMeses.FormattingEnabled = true;
		this.cbMeses.Location = new System.Drawing.Point(146, 9);
		this.cbMeses.Name = "cbMeses";
		this.cbMeses.Size = new System.Drawing.Size(206, 21);
		this.cbMeses.TabIndex = 4;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Location = new System.Drawing.Point(94, 66);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(46, 13);
		this.label6.TabIndex = 5;
		this.label6.Text = "Moneda";
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(146, 63);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(206, 21);
		this.comboBox1.TabIndex = 6;
		this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel4.Controls.Add(this.btnReporte);
		this.groupPanel4.Controls.Add(this.btnSalir);
		this.groupPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupPanel4.Location = new System.Drawing.Point(0, 222);
		this.groupPanel4.Name = "groupPanel4";
		this.groupPanel4.Size = new System.Drawing.Size(417, 48);
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
		this.groupPanel4.TabIndex = 10;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(417, 270);
		base.Controls.Add(this.groupPanel1);
		base.Controls.Add(this.groupPanel2);
		base.Controls.Add(this.groupPanel4);
		base.Controls.Add(this.groupPanel3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmParamStockArticulosMensu";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FrmParamStockArticulosMensu";
		base.Load += new System.EventHandler(FrmParamStockArticulosMensu_Load);
		this.groupPanel1.ResumeLayout(false);
		this.groupPanel1.PerformLayout();
		this.groupPanel2.ResumeLayout(false);
		this.groupPanel2.PerformLayout();
		this.groupPanel3.ResumeLayout(false);
		this.groupPanel3.PerformLayout();
		this.groupPanel4.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
