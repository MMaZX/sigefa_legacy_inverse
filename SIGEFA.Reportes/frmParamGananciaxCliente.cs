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

public class frmParamGananciaxCliente : Form
{
	private clsReporteGananciaxCliente ds = new clsReporteGananciaxCliente();

	private clsAdmCliente AdmCliente = new clsAdmCliente();

	private clsCliente clienteSeleccionado = new clsCliente();

	private IContainer components = null;

	private ButtonX btnCancelar;

	private ButtonX btnReporte;

	private GroupPanel groupPanel1;

	private LabelX labelX5;

	private TextBoxX txtNombreCliente;

	private TextBoxX txtBusquedaCliente;

	private RadioButton rbClienteEspecifico;

	private RadioButton rbTodos;

	private LabelX labelX4;

	private Line line3;

	private Line line2;

	private LabelX labelX3;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private LabelX labelX2;

	private LabelX labelX1;

	private Line line1;

	public frmParamGananciaxCliente()
	{
		InitializeComponent();
	}

	private void frmParamGananciaxCliente_Load(object sender, EventArgs e)
	{
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		int codigoCliente = ((clienteSeleccionado != null) ? clienteSeleccionado.CodCliente : 0);
		CRGananciaxCliente rpt = new CRGananciaxCliente();
		frmRptGananciaxCliente frm = new frmRptGananciaxCliente();
		DataTable dt = ds.ReportGananciaxCliente(codigoCliente, dtpDesde.Value, dtpHasta.Value).Tables[0];
		if (dt.Rows.Count > 0)
		{
			rpt.SetDataSource(dt);
			frm.crvRptGananciaxCliente.ReportSource = rpt;
			frm.Show();
		}
		else
		{
			MessageBox.Show("No se encontraron resultados con los parámetros seleccionados", "Reporte de Ganancia por Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void CargaCliente(int Codigo)
	{
		clienteSeleccionado = AdmCliente.MuestraCliente(Codigo);
		txtBusquedaCliente.Text = clienteSeleccionado.RucDni;
		txtNombreCliente.Text = clienteSeleccionado.RazonSocial;
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtBusquedaCliente_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmClientesLista frm = new frmClientesLista();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				CargaCliente(frm.GetCodigoCliente());
			}
		}
	}

	private void rbClienteEspecifico_CheckedChanged(object sender, EventArgs e)
	{
		if (rbClienteEspecifico.Checked)
		{
			txtBusquedaCliente.Enabled = true;
			txtBusquedaCliente.Focus();
		}
	}

	private void rbTodos_CheckedChanged(object sender, EventArgs e)
	{
		if (rbTodos.Checked)
		{
			txtBusquedaCliente.Enabled = false;
			txtBusquedaCliente.Text = "";
			txtNombreCliente.Text = "";
			clienteSeleccionado = null;
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
		this.btnCancelar = new DevComponents.DotNetBar.ButtonX();
		this.btnReporte = new DevComponents.DotNetBar.ButtonX();
		this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.labelX5 = new DevComponents.DotNetBar.LabelX();
		this.txtNombreCliente = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtBusquedaCliente = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.rbClienteEspecifico = new System.Windows.Forms.RadioButton();
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
		this.groupPanel1.SuspendLayout();
		base.SuspendLayout();
		this.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Location = new System.Drawing.Point(449, 311);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(105, 35);
		this.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnCancelar.TabIndex = 5;
		this.btnCancelar.Text = "CANCELAR";
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnReporte.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnReporte.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.Location = new System.Drawing.Point(338, 311);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(105, 35);
		this.btnReporte.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnReporte.TabIndex = 4;
		this.btnReporte.Text = "REPORTE";
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel1.Controls.Add(this.labelX5);
		this.groupPanel1.Controls.Add(this.txtNombreCliente);
		this.groupPanel1.Controls.Add(this.txtBusquedaCliente);
		this.groupPanel1.Controls.Add(this.rbClienteEspecifico);
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
		this.groupPanel1.TabIndex = 3;
		this.groupPanel1.Text = "FILTROS DEL REPORTE";
		this.labelX5.BackColor = System.Drawing.Color.Transparent;
		this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX5.Location = new System.Drawing.Point(124, 202);
		this.labelX5.Name = "labelX5";
		this.labelX5.Size = new System.Drawing.Size(75, 23);
		this.labelX5.TabIndex = 17;
		this.labelX5.Text = "PRESIONE F1";
		this.txtNombreCliente.Border.Class = "TextBoxBorder";
		this.txtNombreCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreCliente.Location = new System.Drawing.Point(17, 231);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.PreventEnterBeep = true;
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(488, 24);
		this.txtNombreCliente.TabIndex = 16;
		this.txtBusquedaCliente.Border.Class = "TextBoxBorder";
		this.txtBusquedaCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtBusquedaCliente.Enabled = false;
		this.txtBusquedaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBusquedaCliente.Location = new System.Drawing.Point(18, 200);
		this.txtBusquedaCliente.Name = "txtBusquedaCliente";
		this.txtBusquedaCliente.PreventEnterBeep = true;
		this.txtBusquedaCliente.Size = new System.Drawing.Size(100, 24);
		this.txtBusquedaCliente.TabIndex = 15;
		this.txtBusquedaCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBusquedaCliente_KeyDown);
		this.rbClienteEspecifico.AutoSize = true;
		this.rbClienteEspecifico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbClienteEspecifico.Location = new System.Drawing.Point(206, 166);
		this.rbClienteEspecifico.Name = "rbClienteEspecifico";
		this.rbClienteEspecifico.Size = new System.Drawing.Size(148, 17);
		this.rbClienteEspecifico.TabIndex = 14;
		this.rbClienteEspecifico.Text = "ELEGIR UN CLIENTE";
		this.rbClienteEspecifico.UseVisualStyleBackColor = true;
		this.rbClienteEspecifico.CheckedChanged += new System.EventHandler(rbClienteEspecifico_CheckedChanged);
		this.rbTodos.AutoSize = true;
		this.rbTodos.Checked = true;
		this.rbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbTodos.Location = new System.Drawing.Point(18, 166);
		this.rbTodos.Name = "rbTodos";
		this.rbTodos.Size = new System.Drawing.Size(160, 17);
		this.rbTodos.TabIndex = 13;
		this.rbTodos.TabStop = true;
		this.rbTodos.Text = "TODOS LOS CLIENTES";
		this.rbTodos.UseVisualStyleBackColor = true;
		this.rbTodos.CheckedChanged += new System.EventHandler(rbTodos_CheckedChanged);
		this.labelX4.BackColor = System.Drawing.Color.Transparent;
		this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX4.Location = new System.Drawing.Point(17, 113);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(177, 23);
		this.labelX4.TabIndex = 12;
		this.labelX4.Text = "FILTROS DE CLIENTES";
		this.line3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line3.ForeColor = System.Drawing.Color.Goldenrod;
		this.line3.Location = new System.Drawing.Point(-4, 142);
		this.line3.Name = "line3";
		this.line3.Size = new System.Drawing.Size(537, 10);
		this.line3.TabIndex = 11;
		this.line3.Text = "line3";
		this.line3.Thickness = 2;
		this.line2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.line2.ForeColor = System.Drawing.Color.Goldenrod;
		this.line2.Location = new System.Drawing.Point(-4, 103);
		this.line2.Name = "line2";
		this.line2.Size = new System.Drawing.Size(537, 10);
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
		this.line1.Size = new System.Drawing.Size(537, 10);
		this.line1.TabIndex = 4;
		this.line1.Text = "line1";
		this.line1.Thickness = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancelar;
		base.ClientSize = new System.Drawing.Size(566, 355);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupPanel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamGananciaxCliente";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ganancia por Cliente";
		base.Load += new System.EventHandler(frmParamGananciaxCliente_Load);
		this.groupPanel1.ResumeLayout(false);
		this.groupPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
