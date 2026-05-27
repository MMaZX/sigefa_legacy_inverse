using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SIGEFA.Administradores;

namespace SIGEFA.Reportes;

public class frmParamVentaTotalizadoResumido : OfficeForm
{
	private clsAdmFacturaVenta admFacturaVenta = new clsAdmFacturaVenta();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupPanel groupPanel1;

	private Label label2;

	private Label label1;

	private ButtonX btnBuscar;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Label lblCantidadRegistros;

	private Label lblTotalVentas;

	private DataGridView dgvVentasTotalizado;

	private ButtonX btnCopiar;

	private DataGridViewAutoFilterTextBoxColumn correlativo;

	private DataGridViewAutoFilterTextBoxColumn tipo_documento;

	private DataGridViewAutoFilterTextBoxColumn razonsocial;

	private DataGridViewAutoFilterTextBoxColumn numDocumento;

	private DataGridViewAutoFilterTextBoxColumn fechasalida;

	private DataGridViewTextBoxColumn total;

	public frmParamVentaTotalizadoResumido()
	{
		InitializeComponent();
	}

	private void frmParamVentaTotalizadoResumido_Load(object sender, EventArgs e)
	{
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		BuscarVentas();
		lblCantidadRegistros.Text = "Ventas Encontradas: " + dgvVentasTotalizado.RowCount;
		lblCantidadRegistros.Visible = true;
		CalcularTotalVentas();
	}

	private void BuscarVentas()
	{
		dgvVentasTotalizado.DataSource = data;
		data.DataSource = admFacturaVenta.ReporteVentasResumido(dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvVentasTotalizado.ClearSelection();
		if (dgvVentasTotalizado.Rows.Count == 0)
		{
			MessageBox.Show("No se encontraron resultados con los parámetros seleccionados", "Reporte de Ventas Resumido", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void CalcularTotalVentas()
	{
		if (dgvVentasTotalizado.Rows.Count > 0)
		{
			lblTotalVentas.Text = "TOTAL: S/." + string.Format("{0:#,##0.00}", Enumerable.Select<DataGridViewRow, decimal>(dgvVentasTotalizado.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, decimal>)((DataGridViewRow x) => decimal.Parse(x.Cells["total"].Value.ToString()))).Sum().ToString());
		}
		else
		{
			lblTotalVentas.Text = "0.00";
		}
		lblTotalVentas.Visible = true;
	}

	private void btnCopiar_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvVentasTotalizado.Rows.Count > 0)
			{
				dgvVentasTotalizado.MultiSelect = true;
				dgvVentasTotalizado.SelectAll();
				dgvVentasTotalizado.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
				DataObject dataObj = dgvVentasTotalizado.GetClipboardContent();
				if (dataObj != null)
				{
					Clipboard.SetDataObject(dataObj);
				}
				dgvVentasTotalizado.MultiSelect = false;
				MessageBox.Show("Puede copiarlo a cualquier editor de texto...", "Información");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dtpDesde_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnBuscar.PerformClick();
		}
	}

	private void dtpHasta_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnBuscar.PerformClick();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmParamVentaTotalizadoResumido));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.btnBuscar = new DevComponents.DotNetBar.ButtonX();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvVentasTotalizado = new System.Windows.Forms.DataGridView();
		this.lblCantidadRegistros = new System.Windows.Forms.Label();
		this.lblTotalVentas = new System.Windows.Forms.Label();
		this.btnCopiar = new DevComponents.DotNetBar.ButtonX();
		this.correlativo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.tipo_documento = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.razonsocial = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.numDocumento = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fechasalida = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentasTotalizado).BeginInit();
		base.SuspendLayout();
		this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel1.Controls.Add(this.btnBuscar);
		this.groupPanel1.Controls.Add(this.dtpHasta);
		this.groupPanel1.Controls.Add(this.dtpDesde);
		this.groupPanel1.Controls.Add(this.label2);
		this.groupPanel1.Controls.Add(this.label1);
		this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel1.Location = new System.Drawing.Point(12, 12);
		this.groupPanel1.Name = "groupPanel1";
		this.groupPanel1.Size = new System.Drawing.Size(1035, 80);
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
		this.groupPanel1.Text = "FILTROS";
		this.btnBuscar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnBuscar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBuscar.Image = (System.Drawing.Image)resources.GetObject("btnBuscar.Image");
		this.btnBuscar.Location = new System.Drawing.Point(681, 9);
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.Size = new System.Drawing.Size(114, 32);
		this.btnBuscar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnBuscar.TabIndex = 4;
		this.btnBuscar.Text = "BUSCAR";
		this.btnBuscar.Click += new System.EventHandler(btnBuscar_Click);
		this.dtpHasta.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(518, 12);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(136, 26);
		this.dtpHasta.TabIndex = 3;
		this.dtpHasta.KeyDown += new System.Windows.Forms.KeyEventHandler(dtpHasta_KeyDown);
		this.dtpDesde.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(282, 12);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(124, 26);
		this.dtpDesde.TabIndex = 2;
		this.dtpDesde.KeyDown += new System.Windows.Forms.KeyEventHandler(dtpDesde_KeyDown);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(439, 17);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(73, 20);
		this.label2.TabIndex = 1;
		this.label2.Text = "HASTA:";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(200, 17);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(76, 20);
		this.label1.TabIndex = 0;
		this.label1.Text = "DESDE:";
		this.dgvVentasTotalizado.AllowUserToAddRows = false;
		this.dgvVentasTotalizado.AllowUserToDeleteRows = false;
		this.dgvVentasTotalizado.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVentasTotalizado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvVentasTotalizado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvVentasTotalizado.Columns.AddRange(this.correlativo, this.tipo_documento, this.razonsocial, this.numDocumento, this.fechasalida, this.total);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVentasTotalizado.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvVentasTotalizado.Location = new System.Drawing.Point(12, 154);
		this.dgvVentasTotalizado.MultiSelect = false;
		this.dgvVentasTotalizado.Name = "dgvVentasTotalizado";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Chocolate;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVentasTotalizado.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvVentasTotalizado.RowHeadersVisible = false;
		this.dgvVentasTotalizado.Size = new System.Drawing.Size(1035, 245);
		this.dgvVentasTotalizado.TabIndex = 1;
		this.lblCantidadRegistros.AutoSize = true;
		this.lblCantidadRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadRegistros.Location = new System.Drawing.Point(12, 118);
		this.lblCantidadRegistros.Name = "lblCantidadRegistros";
		this.lblCantidadRegistros.Size = new System.Drawing.Size(119, 24);
		this.lblCantidadRegistros.TabIndex = 2;
		this.lblCantidadRegistros.Text = "Nº registros";
		this.lblCantidadRegistros.Visible = false;
		this.lblTotalVentas.AutoSize = true;
		this.lblTotalVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalVentas.Location = new System.Drawing.Point(766, 118);
		this.lblTotalVentas.Name = "lblTotalVentas";
		this.lblTotalVentas.Size = new System.Drawing.Size(56, 24);
		this.lblTotalVentas.TabIndex = 3;
		this.lblTotalVentas.Text = "Total";
		this.lblTotalVentas.Visible = false;
		this.btnCopiar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnCopiar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnCopiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCopiar.Image = (System.Drawing.Image)resources.GetObject("btnCopiar.Image");
		this.btnCopiar.Location = new System.Drawing.Point(12, 405);
		this.btnCopiar.Name = "btnCopiar";
		this.btnCopiar.Size = new System.Drawing.Size(98, 33);
		this.btnCopiar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnCopiar.TabIndex = 4;
		this.btnCopiar.Text = "COPIAR";
		this.btnCopiar.Click += new System.EventHandler(btnCopiar_Click);
		this.correlativo.DataPropertyName = "correlativo";
		this.correlativo.Frozen = true;
		this.correlativo.HeaderText = "Correlativo del Documento";
		this.correlativo.Name = "correlativo";
		this.correlativo.ReadOnly = true;
		this.correlativo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.correlativo.Width = 150;
		this.tipo_documento.DataPropertyName = "tipo_documento";
		this.tipo_documento.Frozen = true;
		this.tipo_documento.HeaderText = "Tipo de Documento";
		this.tipo_documento.Name = "tipo_documento";
		this.tipo_documento.ReadOnly = true;
		this.tipo_documento.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.tipo_documento.Width = 180;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.Frozen = true;
		this.razonsocial.HeaderText = "Cliente";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.razonsocial.Width = 380;
		this.numDocumento.DataPropertyName = "numDocumento";
		this.numDocumento.Frozen = true;
		this.numDocumento.HeaderText = "RUC/DNI";
		this.numDocumento.Name = "numDocumento";
		this.numDocumento.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.numDocumento.Width = 120;
		this.fechasalida.DataPropertyName = "fechasalida";
		this.fechasalida.Frozen = true;
		this.fechasalida.HeaderText = "Fecha de Emisión";
		this.fechasalida.Name = "fechasalida";
		this.fechasalida.ReadOnly = true;
		this.fechasalida.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.total.DataPropertyName = "total";
		this.total.Frozen = true;
		this.total.HeaderText = "Monto Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1060, 450);
		base.Controls.Add(this.btnCopiar);
		base.Controls.Add(this.lblTotalVentas);
		base.Controls.Add(this.lblCantidadRegistros);
		base.Controls.Add(this.dgvVentasTotalizado);
		base.Controls.Add(this.groupPanel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamVentaTotalizadoResumido";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Venta Totalizada Resumida";
		base.Load += new System.EventHandler(frmParamVentaTotalizadoResumido_Load);
		this.groupPanel1.ResumeLayout(false);
		this.groupPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentasTotalizado).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
