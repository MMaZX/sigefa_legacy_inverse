using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmRendicionesVigentes : Office2007Form
{
	public int tipocaja = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private DataGridView dgvLiquidaciones;

	private RibbonBar ribbonBar2;

	private ButtonItem biIngresarCaja;

	private ButtonItem biRencicionCaja;

	private Panel panel1;

	private Panel panel2;

	private RibbonBar ribbonBar1;

	private ButtonItem s;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn numDocumento;

	private DataGridViewTextBoxColumn montoLiquidado;

	private DataGridViewTextBoxColumn fechaRendicion;

	private DataGridViewTextBoxColumn fechaLiquidacion;

	private DataGridViewTextBoxColumn estatus;

	private DataGridViewTextBoxColumn responsable;

	public frmRendicionesVigentes()
	{
		InitializeComponent();
	}

	private void Listaliquidaciones()
	{
	}

	private void frmRendicionesVigentes_Load(object sender, EventArgs e)
	{
		Listaliquidaciones();
	}

	private void s_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvLiquidaciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvLiquidaciones.RowCount > 0)
		{
			biIngresarCaja.Enabled = true;
		}
		else
		{
			biIngresarCaja.Enabled = true;
		}
	}

	private void biIngresarCaja_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaChicaRegistro"] != null)
		{
			Application.OpenForms["frmCajaChicaRegistro"].Activate();
			return;
		}
		frmCajaChicaRegistro form = new frmCajaChicaRegistro();
		form.tipoCaja = tipocaja;
		form.Tipo = 1;
		form.tip = 2;
		form.Proceso = 1;
		form.AperturaCaja = 2;
		form.txtDocumento.Text = dgvLiquidaciones.SelectedRows[0].Cells[numDocumento.Name].Value.ToString();
		form.txtMonto.Text = dgvLiquidaciones.SelectedRows[0].Cells[montoLiquidado.Name].Value.ToString();
		form.txtMontoPendiente.Visible = false;
		form.label9.Visible = false;
		form.label6.Visible = false;
		form.txtRecibo.Visible = false;
		form.ShowDialog();
		Listaliquidaciones();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRendicionesVigentes));
		this.dgvLiquidaciones = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montoLiquidado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaRendicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaLiquidacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.biIngresarCaja = new DevComponents.DotNetBar.ButtonItem();
		this.biRencicionCaja = new DevComponents.DotNetBar.ButtonItem();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.s = new DevComponents.DotNetBar.ButtonItem();
		((System.ComponentModel.ISupportInitialize)this.dgvLiquidaciones).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.dgvLiquidaciones.AllowUserToAddRows = false;
		this.dgvLiquidaciones.AllowUserToDeleteRows = false;
		this.dgvLiquidaciones.AllowUserToResizeColumns = false;
		this.dgvLiquidaciones.AllowUserToResizeRows = false;
		this.dgvLiquidaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvLiquidaciones.Columns.AddRange(this.codigo, this.numDocumento, this.montoLiquidado, this.fechaRendicion, this.fechaLiquidacion, this.estatus, this.responsable);
		this.dgvLiquidaciones.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvLiquidaciones.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvLiquidaciones.Location = new System.Drawing.Point(0, 0);
		this.dgvLiquidaciones.MultiSelect = false;
		this.dgvLiquidaciones.Name = "dgvLiquidaciones";
		this.dgvLiquidaciones.RowHeadersVisible = false;
		this.dgvLiquidaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvLiquidaciones.Size = new System.Drawing.Size(766, 131);
		this.dgvLiquidaciones.TabIndex = 0;
		this.dgvLiquidaciones.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvLiquidaciones_RowStateChanged);
		this.codigo.DataPropertyName = "codRendicion";
		this.codigo.HeaderText = "codigo";
		this.codigo.Name = "codigo";
		this.codigo.Visible = false;
		this.numDocumento.DataPropertyName = "numCheque";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numDocumento.DefaultCellStyle = dataGridViewCellStyle1;
		this.numDocumento.HeaderText = "numDocumento";
		this.numDocumento.Name = "numDocumento";
		this.montoLiquidado.DataPropertyName = "montoLiquidado";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.montoLiquidado.DefaultCellStyle = dataGridViewCellStyle2;
		this.montoLiquidado.HeaderText = "montoLiquidado";
		this.montoLiquidado.Name = "montoLiquidado";
		this.fechaRendicion.DataPropertyName = "fechaRendicion";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.fechaRendicion.DefaultCellStyle = dataGridViewCellStyle3;
		this.fechaRendicion.HeaderText = "fechaRendicion";
		this.fechaRendicion.Name = "fechaRendicion";
		this.fechaRendicion.Width = 130;
		this.fechaLiquidacion.DataPropertyName = "fechaliquidacion";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.fechaLiquidacion.DefaultCellStyle = dataGridViewCellStyle4;
		this.fechaLiquidacion.HeaderText = "fechaLiquidacion";
		this.fechaLiquidacion.Name = "fechaLiquidacion";
		this.fechaLiquidacion.Width = 130;
		this.estatus.DataPropertyName = "estatus";
		this.estatus.HeaderText = "estatus";
		this.estatus.Name = "estatus";
		this.estatus.Visible = false;
		this.responsable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.responsable.DataPropertyName = "responsable";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
		this.responsable.DefaultCellStyle = dataGridViewCellStyle5;
		this.responsable.HeaderText = "responsable";
		this.responsable.Name = "responsable";
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar2.BackgroundMouseOverStyle.Class = "";
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.Class = "";
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biIngresarCaja, this.biRencicionCaja });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(701, 70);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 15;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.Class = "";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.Class = "";
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.biIngresarCaja.Enabled = false;
		this.biIngresarCaja.Image = (System.Drawing.Image)resources.GetObject("biIngresarCaja.Image");
		this.biIngresarCaja.ImagePaddingHorizontal = 20;
		this.biIngresarCaja.ImagePaddingVertical = 10;
		this.biIngresarCaja.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biIngresarCaja.Name = "biIngresarCaja";
		this.biIngresarCaja.SubItemsExpandWidth = 14;
		this.biIngresarCaja.Text = "Ingresar a Caja";
		this.biIngresarCaja.Click += new System.EventHandler(biIngresarCaja_Click);
		this.biRencicionCaja.Enabled = false;
		this.biRencicionCaja.Image = (System.Drawing.Image)resources.GetObject("biRencicionCaja.Image");
		this.biRencicionCaja.ImagePaddingHorizontal = 10;
		this.biRencicionCaja.ImagePaddingVertical = 10;
		this.biRencicionCaja.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRencicionCaja.Name = "biRencicionCaja";
		this.biRencicionCaja.SubItemsExpandWidth = 14;
		this.biRencicionCaja.Text = "Rendir   Caja Chica";
		this.panel1.Controls.Add(this.ribbonBar2);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 131);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(766, 70);
		this.panel1.TabIndex = 16;
		this.panel2.Controls.Add(this.ribbonBar1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(701, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(65, 70);
		this.panel2.TabIndex = 0;
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.Class = "";
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.Class = "";
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.s });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ribbonBar1.Size = new System.Drawing.Size(65, 70);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 16;
		this.ribbonBar1.Text = "ribbonBar1";
		this.ribbonBar1.TitleStyle.Class = "";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.Class = "";
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.s.Image = (System.Drawing.Image)resources.GetObject("s.Image");
		this.s.ImagePaddingHorizontal = 20;
		this.s.ImagePaddingVertical = 10;
		this.s.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.s.Name = "s";
		this.s.SubItemsExpandWidth = 14;
		this.s.Text = "Salir";
		this.s.Click += new System.EventHandler(s_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(766, 201);
		base.Controls.Add(this.dgvLiquidaciones);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(782, 240);
		base.Name = "frmRendicionesVigentes";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Rendiciones Vigentes";
		base.Load += new System.EventHandler(frmRendicionesVigentes_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvLiquidaciones).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
