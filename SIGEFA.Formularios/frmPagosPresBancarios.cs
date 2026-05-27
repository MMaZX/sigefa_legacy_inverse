using System;
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

public class frmPagosPresBancarios : Office2007Form
{
	private clsReportePagos ds = new clsReportePagos();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmFactura admfac = new clsAdmFactura();

	private clsAdmPrestamoBancario admPreBan = new clsAdmPrestamoBancario();

	private clsCuota cuoPreBan = new clsCuota();

	private clsFactura fac = new clsFactura();

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmTipoDocumento admTipo = new clsAdmTipoDocumento();

	private clsAdmLetra admLetra = new clsAdmLetra();

	private clsLetra let = new clsLetra();

	private clsPago pagoRp = new clsPago();

	private IContainer components = null;

	private ImageList imageList1;

	private ButtonItem btnBuscar;

	private ButtonItem buttonItem1;

	private GroupBox groupBox1;

	private Label label4;

	private Label label2;

	private Label label1;

	private ComboBox cmbEstado;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private DataGridView dgvPagos;

	private Button btnReporte;

	private Button btnBusqueda;

	private Label label9;

	private ComboBox cmbEmpresa;

	private TextBox txtFiltro;

	private Label label5;

	private Label label7;

	private Label label10;

	private Label label6;

	private DataGridViewTextBoxColumn codnota;

	private DataGridViewTextBoxColumn codPrestamo;

	private DataGridViewTextBoxColumn nroCuota;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn CodBanco;

	private DataGridViewTextBoxColumn descBanco;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn fechavenc;

	private DataGridViewTextBoxColumn fechacancelado;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn pendiente;

	private DataGridViewLinkColumn accion;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn estado;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem muestraPagosToolStripMenuItem;

	public frmPagosPresBancarios()
	{
		InitializeComponent();
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		dgvPagos.DataSource = data;
		data.DataSource = admPreBan.MuestraPagosPrestamo(cmbEstado.SelectedIndex, Convert.ToInt32(cmbEmpresa.SelectedValue), dtpFecha1.Value, dtpFecha2.Value);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvPagos.ClearSelection();
	}

	private void frmPagos_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		dtpFecha1.Value = dtpFecha2.Value.AddDays(-90.0);
		dtpFecha2.Value = dtpFecha2.Value.AddDays(30.0);
		cmbEstado.SelectedIndex = 0;
		cmbEmpresa.SelectedIndex = 0;
		label7.Text = "Banco";
		label6.Text = "proveedor";
	}

	private void frmPagos_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
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

	private void CargaEmpresas()
	{
		cmbEmpresa.DataSource = admEmp.CargaEmpresas();
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
		cmbEmpresa.SelectedIndex = 0;
	}

	private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvPagos.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvPagos.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void dgvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvPagos.Rows.Count < 1 || e.RowIndex == -1 || e.ColumnIndex == -1)
		{
			return;
		}
		DataGridViewCell celda = dgvPagos.CurrentCell;
		int itipo = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[tipo.Name].Value);
		if (celda.Value.ToString() == "Ingresar Pago")
		{
			if (itipo == 5)
			{
				cuoPreBan.CodCuotaPrestamo = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codnota.Name].Value);
				frmCancelarPago form = new frmCancelarPago();
				form.codCuota = cuoPreBan.CodCuotaPrestamo;
				form.tipo = itipo;
				form.ShowDialog();
				CargaLista();
			}
		}
		else if (celda.Value.ToString() == "Muestra Pagos" && itipo == 5)
		{
			cuoPreBan.CodCuotaPrestamo = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codnota.Name].Value);
			frmMuestraPagos form2 = new frmMuestraPagos();
			form2.codCuota = cuoPreBan.CodCuotaPrestamo;
			form2.InOut = false;
			form2.tipo = itipo;
			form2.ShowDialog();
			CargaLista();
		}
	}

	private void dgvPagos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		dgvPagos.ContextMenuStrip = new ContextMenuStrip();
		if (e.RowIndex == -1)
		{
			return;
		}
		dgvPagos.Rows[e.RowIndex].Selected = true;
		if (e.Button == MouseButtons.Right && e.RowIndex != -1 && dgvPagos.SelectedCells.Count > 0)
		{
			dgvPagos.ContextMenuStrip = contextMenuStrip1;
			if (Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[cantidad.Name].Value) > 0)
			{
				muestraPagosToolStripMenuItem.Enabled = true;
			}
			else
			{
				muestraPagosToolStripMenuItem.Enabled = false;
			}
		}
	}

	private void muestraPagosToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("Pagos");
		foreach (DataGridViewColumn column in dgvPagos.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvPagos.Rows.Count; i++)
		{
			DataGridViewRow row = dgvPagos.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvPagos.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\PagoRPT.xml", XmlWriteMode.WriteSchema);
		CRReportePagos rpt = new CRReportePagos();
		frmRptPagos frm = new frmRptPagos();
		rpt.SetDataSource(ds);
		frm.crvReportePagos.ReportSource = rpt;
		frm.Show();
	}

	private void canjearPorLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void nuevaLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void modificarLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void ingresoABancoToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void canjearPorCuotasToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void muestraPagosToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvPagos.SelectedRows[0];
		int itipo = Convert.ToInt32(Row.Cells[tipo.Name].Value);
		cuoPreBan.CodCuotaPrestamo = Convert.ToInt32(Row.Cells[codnota.Name].Value);
		frmMuestraPagos form = new frmMuestraPagos();
		form.codCuota = cuoPreBan.CodCuotaPrestamo;
		form.InOut = false;
		form.tipo = itipo;
		form.ShowDialog();
		CargaLista();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPagosPresBancarios));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.dgvPagos = new System.Windows.Forms.DataGridView();
		this.codnota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codPrestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nroCuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodBanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descBanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechavenc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechacancelado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.accion = new System.Windows.Forms.DataGridViewLinkColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.muestraPagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
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
		this.imageList1.Images.SetKeyName(16, "folder_open (1).png");
		this.imageList1.Images.SetKeyName(17, "folder-open-icon.png");
		this.imageList1.Images.SetKeyName(18, "Glossy-Open-icon.png");
		this.imageList1.Images.SetKeyName(19, "Ocean Blue Open.png");
		this.imageList1.Images.SetKeyName(20, "Open (1).png");
		this.imageList1.Images.SetKeyName(21, "open_folder_green.png");
		this.btnBuscar.ImageIndex = 11;
		this.btnBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.SubItemsExpandWidth = 14;
		this.btnBuscar.Text = "Buscar";
		this.buttonItem1.ImageIndex = 11;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Buscar";
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbEmpresa);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbEstado);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(957, 59);
		this.groupBox1.TabIndex = 7;
		this.groupBox1.TabStop = false;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label6.Location = new System.Drawing.Point(632, 8);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 37;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(473, 9);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 36;
		this.label5.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(508, 9);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 35;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(435, 9);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 34;
		this.label10.Text = "Filtro";
		this.txtFiltro.Location = new System.Drawing.Point(437, 24);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 33;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(6, 9);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(49, 12);
		this.label9.TabIndex = 32;
		this.label9.Text = "Empresa";
		this.cmbEmpresa.Enabled = false;
		this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(9, 25);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(121, 20);
		this.cmbEmpresa.TabIndex = 31;
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList1;
		this.btnBusqueda.Location = new System.Drawing.Point(789, 17);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(78, 33);
		this.btnBusqueda.TabIndex = 30;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.btnReporte.ImageIndex = 7;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(873, 17);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 33);
		this.btnReporte.TabIndex = 29;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(307, 9);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 12);
		this.label4.TabIndex = 28;
		this.label4.Text = "Estado";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(220, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 26;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(133, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 25;
		this.label1.Text = "Desde";
		this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[2] { "PENDIENTES", "CANCELADOS" });
		this.cmbEstado.Location = new System.Drawing.Point(310, 25);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(121, 20);
		this.cmbEstado.TabIndex = 24;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(223, 25);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 22;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(136, 25);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 21;
		this.dgvPagos.AllowUserToAddRows = false;
		this.dgvPagos.AllowUserToDeleteRows = false;
		this.dgvPagos.AllowUserToResizeRows = false;
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvPagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
		this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvPagos.Columns.AddRange(this.codnota, this.codPrestamo, this.nroCuota, this.tipo, this.CodBanco, this.descBanco, this.fechaemision, this.fechavenc, this.fechacancelado, this.moneda, this.monto, this.pendiente, this.accion, this.cantidad, this.estado);
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvPagos.DefaultCellStyle = dataGridViewCellStyle26;
		this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvPagos.Location = new System.Drawing.Point(0, 59);
		this.dgvPagos.MultiSelect = false;
		this.dgvPagos.Name = "dgvPagos";
		this.dgvPagos.ReadOnly = true;
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvPagos.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
		this.dgvPagos.RowHeadersVisible = false;
		this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPagos.Size = new System.Drawing.Size(957, 415);
		this.dgvPagos.TabIndex = 8;
		this.dgvPagos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_ColumnHeaderMouseClick);
		this.dgvPagos.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvPagos_CellMouseDown);
		this.dgvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPagos_CellContentClick);
		this.codnota.DataPropertyName = "codigo";
		this.codnota.HeaderText = "CodCuota";
		this.codnota.Name = "codnota";
		this.codnota.ReadOnly = true;
		this.codnota.Visible = false;
		this.codPrestamo.DataPropertyName = "codPrestamo";
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.codPrestamo.DefaultCellStyle = dataGridViewCellStyle28;
		this.codPrestamo.HeaderText = "Cod. Prestamo";
		this.codPrestamo.Name = "codPrestamo";
		this.codPrestamo.ReadOnly = true;
		this.nroCuota.DataPropertyName = "nroCuota";
		dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.nroCuota.DefaultCellStyle = dataGridViewCellStyle29;
		this.nroCuota.HeaderText = "# Cuota";
		this.nroCuota.Name = "nroCuota";
		this.nroCuota.ReadOnly = true;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "tipo";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Visible = false;
		this.CodBanco.DataPropertyName = "CodBanco";
		this.CodBanco.HeaderText = "CodBanco";
		this.CodBanco.Name = "CodBanco";
		this.CodBanco.ReadOnly = true;
		this.CodBanco.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.CodBanco.Visible = false;
		this.CodBanco.Width = 90;
		this.descBanco.DataPropertyName = "descBanco";
		this.descBanco.HeaderText = "Banco";
		this.descBanco.Name = "descBanco";
		this.descBanco.ReadOnly = true;
		this.descBanco.Width = 250;
		this.fechaemision.DataPropertyName = "fechaemision";
		this.fechaemision.HeaderText = "fechaemision";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechaemision.Visible = false;
		this.fechavenc.DataPropertyName = "fechavence";
		dataGridViewCellStyle30.Format = "d";
		dataGridViewCellStyle30.NullValue = null;
		this.fechavenc.DefaultCellStyle = dataGridViewCellStyle30;
		this.fechavenc.HeaderText = "Fecha Venc.";
		this.fechavenc.Name = "fechavenc";
		this.fechavenc.ReadOnly = true;
		this.fechavenc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechavenc.Width = 90;
		this.fechacancelado.DataPropertyName = "fechacancelado";
		this.fechacancelado.HeaderText = "Fec. Canc.";
		this.fechacancelado.Name = "fechacancelado";
		this.fechacancelado.ReadOnly = true;
		this.fechacancelado.Width = 90;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.moneda.Width = 150;
		this.monto.DataPropertyName = "total";
		dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.monto.DefaultCellStyle = dataGridViewCellStyle31;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.monto.Width = 80;
		this.pendiente.DataPropertyName = "pendiente";
		dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.pendiente.DefaultCellStyle = dataGridViewCellStyle32;
		this.pendiente.HeaderText = "Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.pendiente.Width = 80;
		this.accion.DataPropertyName = "accion";
		dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.accion.DefaultCellStyle = dataGridViewCellStyle33;
		this.accion.HeaderText = "Acción";
		this.accion.Name = "accion";
		this.accion.ReadOnly = true;
		this.accion.Text = "";
		this.accion.Width = 120;
		this.cantidad.DataPropertyName = "cantpagos";
		this.cantidad.HeaderText = "Cant. Pagos";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.cantidad.Visible = false;
		this.estado.DataPropertyName = "cancelado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.muestraPagosToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
		this.muestraPagosToolStripMenuItem.Name = "muestraPagosToolStripMenuItem";
		this.muestraPagosToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.muestraPagosToolStripMenuItem.Text = "Muestra Pagos";
		this.muestraPagosToolStripMenuItem.Click += new System.EventHandler(muestraPagosToolStripMenuItem_Click_1);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(957, 474);
		base.Controls.Add(this.dgvPagos);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.KeyPreview = true;
		base.Name = "frmPagosPresBancarios";
		base.ShowInTaskbar = false;
		this.Text = "Pagos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPagos_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
