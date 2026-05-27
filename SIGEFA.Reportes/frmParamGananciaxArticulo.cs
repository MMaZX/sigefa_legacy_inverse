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

namespace SIGEFA.Reportes;

public class frmParamGananciaxArticulo : Form
{
	private clsReporteGananciaxArticulo ds = new clsReporteGananciaxArticulo();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto productoSeleccionado = new clsProducto();

	private IContainer components = null;

	private GroupPanel groupPanel1;

	private LabelX labelX2;

	private Line line1;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private LabelX labelX4;

	private Line line3;

	private Line line2;

	private LabelX labelX3;

	private LabelX labelX1;

	private RadioButton rbProductoEspecifico;

	private RadioButton rbTodos;

	private LabelX labelX5;

	private TextBoxX txtNombreProducto;

	private TextBoxX txtBusquedaProducto;

	private ButtonX btnReporte;

	private ButtonX btnCancelar;

	public frmParamGananciaxArticulo()
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
		int codigoProducto = ((productoSeleccionado != null) ? productoSeleccionado.CodProducto : 0);
		CRGananciaxArticulo rpt = new CRGananciaxArticulo();
		frmRptGananciaxArticulo frm = new frmRptGananciaxArticulo();
		DataTable dt = ds.ReportGananciaxArticulo(codigoProducto, dtpDesde.Value, dtpHasta.Value).Tables[0];
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

	private void frmParamGananciaxArticulo_Load(object sender, EventArgs e)
	{
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
		this.labelX5 = new DevComponents.DotNetBar.LabelX();
		this.txtNombreProducto = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtBusquedaProducto = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.rbProductoEspecifico = new System.Windows.Forms.RadioButton();
		this.rbTodos = new System.Windows.Forms.RadioButton();
		this.labelX4 = new DevComponents.DotNetBar.LabelX();
		this.line3 = new DevComponents.DotNetBar.Controls.Line();
		this.line2 = new DevComponents.DotNetBar.Controls.Line();
		this.labelX3 = new DevComponents.DotNetBar.LabelX();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.labelX2 = new DevComponents.DotNetBar.LabelX();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.line1 = new DevComponents.DotNetBar.Controls.Line();
		this.btnReporte = new DevComponents.DotNetBar.ButtonX();
		this.btnCancelar = new DevComponents.DotNetBar.ButtonX();
		this.groupPanel1.SuspendLayout();
		base.SuspendLayout();
		this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel1.Controls.Add(this.labelX5);
		this.groupPanel1.Controls.Add(this.txtNombreProducto);
		this.groupPanel1.Controls.Add(this.txtBusquedaProducto);
		this.groupPanel1.Controls.Add(this.rbProductoEspecifico);
		this.groupPanel1.Controls.Add(this.rbTodos);
		this.groupPanel1.Controls.Add(this.labelX4);
		this.groupPanel1.Controls.Add(this.line3);
		this.groupPanel1.Controls.Add(this.line2);
		this.groupPanel1.Controls.Add(this.labelX3);
		this.groupPanel1.Controls.Add(this.dtpHasta);
		this.groupPanel1.Controls.Add(this.dtpDesde);
		this.groupPanel1.Controls.Add(this.labelX2);
		this.groupPanel1.Controls.Add(this.labelX1);
		this.groupPanel1.Controls.Add(this.line1);
		this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel1.Location = new System.Drawing.Point(12, 12);
		this.groupPanel1.Name = "groupPanel1";
		this.groupPanel1.Size = new System.Drawing.Size(541, 293);
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
		this.groupPanel1.TabIndex = 0;
		this.groupPanel1.Text = "FILTROS DEL REPORTE";
		this.labelX5.BackColor = System.Drawing.Color.Transparent;
		this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX5.Location = new System.Drawing.Point(124, 202);
		this.labelX5.Name = "labelX5";
		this.labelX5.Size = new System.Drawing.Size(75, 23);
		this.labelX5.TabIndex = 17;
		this.labelX5.Text = "PRESIONE F1";
		this.txtNombreProducto.Border.Class = "TextBoxBorder";
		this.txtNombreProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreProducto.Location = new System.Drawing.Point(17, 231);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.PreventEnterBeep = true;
		this.txtNombreProducto.ReadOnly = true;
		this.txtNombreProducto.Size = new System.Drawing.Size(488, 24);
		this.txtNombreProducto.TabIndex = 16;
		this.txtBusquedaProducto.Border.Class = "TextBoxBorder";
		this.txtBusquedaProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtBusquedaProducto.Enabled = false;
		this.txtBusquedaProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBusquedaProducto.Location = new System.Drawing.Point(18, 200);
		this.txtBusquedaProducto.Name = "txtBusquedaProducto";
		this.txtBusquedaProducto.PreventEnterBeep = true;
		this.txtBusquedaProducto.Size = new System.Drawing.Size(100, 24);
		this.txtBusquedaProducto.TabIndex = 15;
		this.txtBusquedaProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBusquedaProducto_KeyDown);
		this.rbProductoEspecifico.AutoSize = true;
		this.rbProductoEspecifico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbProductoEspecifico.Location = new System.Drawing.Point(206, 166);
		this.rbProductoEspecifico.Name = "rbProductoEspecifico";
		this.rbProductoEspecifico.Size = new System.Drawing.Size(165, 17);
		this.rbProductoEspecifico.TabIndex = 14;
		this.rbProductoEspecifico.Text = "ELEGIR UN PRODUCTO";
		this.rbProductoEspecifico.UseVisualStyleBackColor = true;
		this.rbProductoEspecifico.CheckedChanged += new System.EventHandler(rbProductoEspecifico_CheckedChanged);
		this.rbTodos.AutoSize = true;
		this.rbTodos.Checked = true;
		this.rbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbTodos.Location = new System.Drawing.Point(18, 166);
		this.rbTodos.Name = "rbTodos";
		this.rbTodos.Size = new System.Drawing.Size(177, 17);
		this.rbTodos.TabIndex = 13;
		this.rbTodos.TabStop = true;
		this.rbTodos.Text = "TODOS LOS PRODUCTOS";
		this.rbTodos.UseVisualStyleBackColor = true;
		this.rbTodos.CheckedChanged += new System.EventHandler(rbTodos_CheckedChanged);
		this.labelX4.BackColor = System.Drawing.Color.Transparent;
		this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX4.Location = new System.Drawing.Point(17, 113);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(177, 23);
		this.labelX4.TabIndex = 12;
		this.labelX4.Text = "FILTROS DE PRODUCTOS";
		this.line3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line3.ForeColor = System.Drawing.Color.Goldenrod;
		this.line3.Location = new System.Drawing.Point(-4, 142);
		this.line3.Name = "line3";
		this.line3.Size = new System.Drawing.Size(543, 10);
		this.line3.TabIndex = 11;
		this.line3.Text = "line3";
		this.line3.Thickness = 2;
		this.line2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line2.ForeColor = System.Drawing.Color.Goldenrod;
		this.line2.Location = new System.Drawing.Point(-4, 103);
		this.line2.Name = "line2";
		this.line2.Size = new System.Drawing.Size(543, 10);
		this.line2.TabIndex = 10;
		this.line2.Text = "line2";
		this.line2.Thickness = 2;
		this.labelX3.BackColor = System.Drawing.Color.Transparent;
		this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX3.Location = new System.Drawing.Point(17, 3);
		this.labelX3.Name = "labelX3";
		this.labelX3.Size = new System.Drawing.Size(141, 23);
		this.labelX3.TabIndex = 9;
		this.labelX3.Text = "FILTROS DE FECHAS";
		this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(362, 65);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(143, 22);
		this.dtpHasta.TabIndex = 8;
		this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(98, 65);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(143, 22);
		this.dtpDesde.TabIndex = 7;
		this.labelX2.BackColor = System.Drawing.Color.Transparent;
		this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX2.Location = new System.Drawing.Point(296, 64);
		this.labelX2.Name = "labelX2";
		this.labelX2.Size = new System.Drawing.Size(75, 23);
		this.labelX2.TabIndex = 6;
		this.labelX2.Text = "HASTA:";
		this.labelX1.BackColor = System.Drawing.Color.Transparent;
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX1.Location = new System.Drawing.Point(17, 64);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(75, 23);
		this.labelX1.TabIndex = 5;
		this.labelX1.Text = "DESDE:";
		this.line1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line1.ForeColor = System.Drawing.Color.Goldenrod;
		this.line1.Location = new System.Drawing.Point(-5, 32);
		this.line1.Name = "line1";
		this.line1.Size = new System.Drawing.Size(543, 10);
		this.line1.TabIndex = 4;
		this.line1.Text = "line1";
		this.line1.Thickness = 2;
		this.btnReporte.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnReporte.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.Location = new System.Drawing.Point(338, 311);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(105, 35);
		this.btnReporte.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnReporte.TabIndex = 1;
		this.btnReporte.Text = "REPORTE";
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Location = new System.Drawing.Point(449, 311);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(105, 35);
		this.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "CANCELAR";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancelar;
		base.ClientSize = new System.Drawing.Size(565, 354);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupPanel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamGananciaxArticulo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ganancia por Producto";
		base.Load += new System.EventHandler(frmParamGananciaxArticulo_Load);
		this.groupPanel1.ResumeLayout(false);
		this.groupPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
