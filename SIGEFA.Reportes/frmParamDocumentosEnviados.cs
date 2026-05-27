using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SIGEFA.Administradores;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamDocumentosEnviados : Office2007Form
{
	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmTipoDocumento admTipoDocumento = new clsAdmTipoDocumento();

	private clsReporteDocumentosEnviados ds = new clsReporteDocumentosEnviados();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupPanel groupPanel1;

	private LabelX labelX3;

	private LabelX labelX2;

	private LabelX labelX1;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private ComboBox cboTipoDocumento;

	private ButtonX btnReporte;

	private ButtonX btnCancelar;

	private ComboBox cmbAlmacenes;

	private LabelX labelX4;

	public frmParamDocumentosEnviados()
	{
		InitializeComponent();
	}

	private void frmParamDocumentosEnviados_Load(object sender, EventArgs e)
	{
		CargaTipoDocumentosElectronicos();
		cargaAlmacenes();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		BuscarVentas();
	}

	private void CargaTipoDocumentosElectronicos()
	{
		cboTipoDocumento.DataSource = admTipoDocumento.MuestraTipoDocumentosElectronicos();
		cboTipoDocumento.DisplayMember = "descripcion";
		cboTipoDocumento.ValueMember = "codTipoDocumento";
		cboTipoDocumento.SelectedIndex = 0;
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		DataTable dat = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		dat.Rows.RemoveAt(0);
		cmbAlmacenes.DataSource = dat;
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void BuscarVentas()
	{
		CRDocumentosEnviados rpt = new CRDocumentosEnviados();
		frmRptDocumentosEnviados frm = new frmRptDocumentosEnviados();
		DataTable dt = ds.DocumentosEnviados(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cboTipoDocumento.SelectedValue)).Tables[0];
		if (dt.Rows.Count > 0)
		{
			rpt.SetDataSource(dt);
			frm.crvRptDocumentosEnviados.ReportSource = rpt;
			frm.Show();
		}
		else
		{
			MessageBox.Show("No se ha encontrado resultados con los filtros seleccionados", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void groupPanel1_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmParamDocumentosEnviados));
		this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.labelX4 = new DevComponents.DotNetBar.LabelX();
		this.cboTipoDocumento = new System.Windows.Forms.ComboBox();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.labelX3 = new DevComponents.DotNetBar.LabelX();
		this.labelX2 = new DevComponents.DotNetBar.LabelX();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.btnCancelar = new DevComponents.DotNetBar.ButtonX();
		this.btnReporte = new DevComponents.DotNetBar.ButtonX();
		this.groupPanel1.SuspendLayout();
		base.SuspendLayout();
		this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel1.Controls.Add(this.cmbAlmacenes);
		this.groupPanel1.Controls.Add(this.labelX4);
		this.groupPanel1.Controls.Add(this.cboTipoDocumento);
		this.groupPanel1.Controls.Add(this.dtpHasta);
		this.groupPanel1.Controls.Add(this.dtpDesde);
		this.groupPanel1.Controls.Add(this.labelX3);
		this.groupPanel1.Controls.Add(this.labelX2);
		this.groupPanel1.Controls.Add(this.labelX1);
		this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel1.Location = new System.Drawing.Point(12, 12);
		this.groupPanel1.Name = "groupPanel1";
		this.groupPanel1.Size = new System.Drawing.Size(447, 145);
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
		this.groupPanel1.Click += new System.EventHandler(groupPanel1_Click);
		this.cmbAlmacenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Location = new System.Drawing.Point(238, 82);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(188, 24);
		this.cmbAlmacenes.TabIndex = 7;
		this.labelX4.BackColor = System.Drawing.Color.Transparent;
		this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX4.Location = new System.Drawing.Point(238, 51);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(68, 25);
		this.labelX4.TabIndex = 6;
		this.labelX4.Text = "ALMACEN:";
		this.cboTipoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboTipoDocumento.FormattingEnabled = true;
		this.cboTipoDocumento.Location = new System.Drawing.Point(16, 82);
		this.cboTipoDocumento.Name = "cboTipoDocumento";
		this.cboTipoDocumento.Size = new System.Drawing.Size(188, 24);
		this.cboTipoDocumento.TabIndex = 5;
		this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(309, 12);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(117, 22);
		this.dtpHasta.TabIndex = 4;
		this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(75, 12);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(117, 22);
		this.dtpDesde.TabIndex = 3;
		this.labelX3.BackColor = System.Drawing.Color.Transparent;
		this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX3.Location = new System.Drawing.Point(16, 53);
		this.labelX3.Name = "labelX3";
		this.labelX3.Size = new System.Drawing.Size(131, 23);
		this.labelX3.TabIndex = 2;
		this.labelX3.Text = "TIPO DE DOCUMENTO:";
		this.labelX2.BackColor = System.Drawing.Color.Transparent;
		this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX2.Location = new System.Drawing.Point(249, 12);
		this.labelX2.Name = "labelX2";
		this.labelX2.Size = new System.Drawing.Size(71, 23);
		this.labelX2.TabIndex = 1;
		this.labelX2.Text = "HASTA:";
		this.labelX1.BackColor = System.Drawing.Color.Transparent;
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelX1.Location = new System.Drawing.Point(16, 12);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(75, 23);
		this.labelX1.TabIndex = 0;
		this.labelX1.Text = "DESDE:";
		this.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnCancelar.Image = (System.Drawing.Image)resources.GetObject("btnCancelar.Image");
		this.btnCancelar.Location = new System.Drawing.Point(370, 172);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(89, 28);
		this.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "CANCELAR";
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnReporte.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnReporte.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnReporte.Image = (System.Drawing.Image)resources.GetObject("btnReporte.Image");
		this.btnReporte.Location = new System.Drawing.Point(264, 172);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(100, 28);
		this.btnReporte.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnReporte.TabIndex = 1;
		this.btnReporte.Text = "REPORTE";
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(471, 213);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupPanel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamDocumentosEnviados";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte Documentos Electrónicos";
		base.Load += new System.EventHandler(frmParamDocumentosEnviados_Load);
		this.groupPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
