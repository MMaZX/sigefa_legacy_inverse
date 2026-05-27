using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using MySql.Data.MySqlClient;
using SIGEFA.Administradores;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoDeCajas : OfficeForm
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private int codCaja = 0;

	private DateTime FechaCaja;

	private IContainer components = null;

	private Panel panel1;

	private DataGridView dgvCarjas;

	private DataGridViewAutoFilterTextBoxColumn codigo;

	private DataGridViewTextBoxColumn montoapertura;

	private DataGridViewTextBoxColumn fechaapertura;

	private DataGridViewTextBoxColumn montocierre;

	private DataGridViewTextBoxColumn fechacierre;

	private DataGridViewTextBoxColumn totalingreso;

	private DataGridViewTextBoxColumn totalsalida;

	private DataGridViewTextBoxColumn totalventaefectivo;

	private ImageList imageList1;

	private ImageList imageList2;

	private Button btnIrPedido;

	private Button btnSalir;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button button1;

	private RadDropDownList cmbAlmacenes;

	private RadLabel radLabel1;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private GroupBox groupBox1;

	private Button btnGeneraDetallePorUsuario;

	private ComboBox cmbUsuarioParaDetalleCajporUsuario;

	private Label label1;

	private MySqlDataAdapter mySqlDataAdapter1;

	public frmListadoDeCajas()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmListadoDeCajas_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		CargaLista();
	}

	private void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admAlm.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		cmbAlmacenes.Items.RemoveAt(0);
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
	}

	public void cargaUsuarios()
	{
		clsReporteCaja dso = new clsReporteCaja();
		cmbUsuarioParaDetalleCajporUsuario.ValueMember = "codUser";
		cmbUsuarioParaDetalleCajporUsuario.DisplayMember = "nombreusuario";
		cmbUsuarioParaDetalleCajporUsuario.DataSource = dso.ListadoUsuariosCierreCaja(Convert.ToInt32(cmbAlmacenes.SelectedValue), FechaCaja, codCaja, Convert.ToInt32(cmbAlmacenes.SelectedValue));
		btnGeneraDetallePorUsuario.Enabled = true;
	}

	private void CargaLista()
	{
		dgvCarjas.DataSource = data;
		data.DataSource = AdmCaja.ConsultaCajas(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvCarjas.ClearSelection();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		btnGeneraDetallePorUsuario.Enabled = false;
		CargaLista();
	}

	private void dgvCarjas_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvCarjas.Rows.Count >= 1 && e.RowIndex != -1)
		{
			codCaja = Convert.ToInt32(dgvCarjas.Rows[e.RowIndex].Cells[codigo.Name].Value);
			FechaCaja = Convert.ToDateTime(dgvCarjas.Rows[e.RowIndex].Cells[fechaapertura.Name].Value);
			cargaUsuarios();
		}
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		clsReporteCaja dso = new clsReporteCaja();
		CRCierre rpt = new CRCierre();
		frmRptCaja frm = new frmRptCaja();
		rpt.SetDataSource(dso.RptMuestraCierreCaja(Convert.ToInt32(cmbAlmacenes.SelectedValue), FechaCaja, codCaja, Convert.ToInt32(cmbAlmacenes.SelectedValue)).Tables[0]);
		frm.crvKardex.ReportSource = rpt;
		frm.Show();
	}

	private void PaintBorderlessGroupBox(object sender, PaintEventArgs e)
	{
	}

	private void btnGeneraDetallePorUsuario_Click(object sender, EventArgs e)
	{
		try
		{
			clsReporteCaja dso = new clsReporteCaja();
			frmReporteCajaCierrePorUsuario frm = new frmReporteCajaCierrePorUsuario();
			DataSet data = dso.RptMuestraCierreCajaPorUsuario(Convert.ToInt32(cmbAlmacenes.SelectedValue), FechaCaja, codCaja, Convert.ToInt32(cmbAlmacenes.SelectedValue), Convert.ToInt32(cmbUsuarioParaDetalleCajporUsuario.SelectedValue));
			frm.data_cierre = data.Tables[0];
			frm.data_sucursal = data.Tables[1];
			frm.data_totales = data.Tables[2];
			frm.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Listado de Cajas dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListadoDeCajas));
		this.panel1 = new System.Windows.Forms.Panel();
		this.dgvCarjas = new System.Windows.Forms.DataGridView();
		this.codigo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.montoapertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaapertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montocierre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechacierre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalsalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalventaefectivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnIrPedido = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.button1 = new System.Windows.Forms.Button();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnGeneraDetallePorUsuario = new System.Windows.Forms.Button();
		this.cmbUsuarioParaDetalleCajporUsuario = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.mySqlDataAdapter1 = new MySql.Data.MySqlClient.MySqlDataAdapter();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCarjas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.dgvCarjas);
		this.panel1.Location = new System.Drawing.Point(12, 12);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(717, 296);
		this.panel1.TabIndex = 0;
		this.dgvCarjas.AllowUserToAddRows = false;
		this.dgvCarjas.AllowUserToDeleteRows = false;
		this.dgvCarjas.AllowUserToOrderColumns = true;
		this.dgvCarjas.AllowUserToResizeRows = false;
		this.dgvCarjas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvCarjas.Columns.AddRange(this.codigo, this.montoapertura, this.fechaapertura, this.montocierre, this.fechacierre, this.totalingreso, this.totalsalida, this.totalventaefectivo);
		this.dgvCarjas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvCarjas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvCarjas.GridColor = System.Drawing.SystemColors.ActiveBorder;
		this.dgvCarjas.Location = new System.Drawing.Point(0, 0);
		this.dgvCarjas.MultiSelect = false;
		this.dgvCarjas.Name = "dgvCarjas";
		this.dgvCarjas.RowHeadersVisible = false;
		this.dgvCarjas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dgvCarjas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCarjas.Size = new System.Drawing.Size(717, 296);
		this.dgvCarjas.TabIndex = 1;
		this.dgvCarjas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCarjas_CellClick);
		this.codigo.DataPropertyName = "codcaja";
		this.codigo.HeaderText = "Codigo Caja";
		this.codigo.Name = "codigo";
		this.codigo.Visible = false;
		this.montoapertura.DataPropertyName = "montoapertura";
		this.montoapertura.HeaderText = "Monto apertura";
		this.montoapertura.Name = "montoapertura";
		this.fechaapertura.DataPropertyName = "fechaapertura";
		this.fechaapertura.HeaderText = "Fecha de Apertura";
		this.fechaapertura.Name = "fechaapertura";
		this.montocierre.DataPropertyName = "montocierre";
		this.montocierre.HeaderText = "Monto de Cierre";
		this.montocierre.Name = "montocierre";
		this.fechacierre.DataPropertyName = "fechacierre";
		this.fechacierre.HeaderText = "Fecha de Cierre";
		this.fechacierre.Name = "fechacierre";
		this.totalingreso.DataPropertyName = "totalIngreso";
		this.totalingreso.HeaderText = "Total Ingresos";
		this.totalingreso.Name = "totalingreso";
		this.totalsalida.DataPropertyName = "totalEgreso";
		this.totalsalida.HeaderText = "Total Salida";
		this.totalsalida.Name = "totalsalida";
		this.totalventaefectivo.DataPropertyName = "totalVentaEfectivo";
		this.totalventaefectivo.HeaderText = "Venta de Efectivo";
		this.totalventaefectivo.Name = "totalventaefectivo";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "document_print.png");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList2.Images.SetKeyName(1, "Add.png");
		this.imageList2.Images.SetKeyName(2, "Remove.png");
		this.imageList2.Images.SetKeyName(3, "Write Document.png");
		this.imageList2.Images.SetKeyName(4, "New Document.png");
		this.imageList2.Images.SetKeyName(5, "Remove Document.png");
		this.imageList2.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList2.Images.SetKeyName(7, "document-print.png");
		this.imageList2.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList2.Images.SetKeyName(9, "refresh_256.png");
		this.imageList2.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList2.Images.SetKeyName(11, "search (1).png");
		this.imageList2.Images.SetKeyName(12, "search (5).png");
		this.imageList2.Images.SetKeyName(13, "search (6).png");
		this.imageList2.Images.SetKeyName(14, "search (8).png");
		this.imageList2.Images.SetKeyName(15, "search_top.png");
		this.imageList2.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList2.Images.SetKeyName(17, "Folder open.png");
		this.imageList2.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList2.Images.SetKeyName(19, "img_visto.jpg");
		this.btnIrPedido.ImageIndex = 1;
		this.btnIrPedido.ImageList = this.imageList1;
		this.btnIrPedido.Location = new System.Drawing.Point(535, 333);
		this.btnIrPedido.Name = "btnIrPedido";
		this.btnIrPedido.Size = new System.Drawing.Size(93, 37);
		this.btnIrPedido.TabIndex = 4;
		this.btnIrPedido.Text = "Consulta";
		this.btnIrPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrPedido.UseVisualStyleBackColor = true;
		this.btnIrPedido.Click += new System.EventHandler(btnIrPedido_Click);
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(634, 333);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(182, 322);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 21;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(9, 322);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 20;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(62, 319);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 19;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(229, 319);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 18;
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.button1.ImageIndex = 8;
		this.button1.ImageList = this.imageList2;
		this.button1.Location = new System.Drawing.Point(362, 333);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(93, 37);
		this.button1.TabIndex = 32;
		this.button1.Text = "Actualizar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.cmbAlmacenes.Location = new System.Drawing.Point(156, 350);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(125, 24);
		this.cmbAlmacenes.TabIndex = 33;
		this.cmbAlmacenes.ThemeName = "TelerikMetroBlue";
		this.radLabel1.Location = new System.Drawing.Point(94, 354);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(52, 18);
		this.radLabel1.TabIndex = 34;
		this.radLabel1.Text = "Almacen:";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.groupBox1.Controls.Add(this.btnGeneraDetallePorUsuario);
		this.groupBox1.Controls.Add(this.cmbUsuarioParaDetalleCajporUsuario);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.groupBox1.Location = new System.Drawing.Point(735, 124);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
		this.groupBox1.Size = new System.Drawing.Size(220, 120);
		this.groupBox1.TabIndex = 35;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle de Caja Por Usuario";
		this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(PaintBorderlessGroupBox);
		this.btnGeneraDetallePorUsuario.Enabled = false;
		this.btnGeneraDetallePorUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGeneraDetallePorUsuario.Image = SIGEFA.Properties.Resources.regeneracion;
		this.btnGeneraDetallePorUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGeneraDetallePorUsuario.Location = new System.Drawing.Point(10, 65);
		this.btnGeneraDetallePorUsuario.Name = "btnGeneraDetallePorUsuario";
		this.btnGeneraDetallePorUsuario.Size = new System.Drawing.Size(202, 47);
		this.btnGeneraDetallePorUsuario.TabIndex = 2;
		this.btnGeneraDetallePorUsuario.Text = "Detalle Caja Por Usuario";
		this.btnGeneraDetallePorUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGeneraDetallePorUsuario.UseVisualStyleBackColor = true;
		this.btnGeneraDetallePorUsuario.Click += new System.EventHandler(btnGeneraDetallePorUsuario_Click);
		this.cmbUsuarioParaDetalleCajporUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbUsuarioParaDetalleCajporUsuario.FormattingEnabled = true;
		this.cmbUsuarioParaDetalleCajporUsuario.Location = new System.Drawing.Point(10, 37);
		this.cmbUsuarioParaDetalleCajporUsuario.Name = "cmbUsuarioParaDetalleCajporUsuario";
		this.cmbUsuarioParaDetalleCajporUsuario.Size = new System.Drawing.Size(202, 21);
		this.cmbUsuarioParaDetalleCajporUsuario.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Usuario:";
		this.mySqlDataAdapter1.DeleteCommand = null;
		this.mySqlDataAdapter1.InsertCommand = null;
		this.mySqlDataAdapter1.SelectCommand = null;
		this.mySqlDataAdapter1.UpdateCommand = null;
		base.ClientSize = new System.Drawing.Size(983, 436);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.radLabel1);
		base.Controls.Add(this.cmbAlmacenes);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.btnIrPedido);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmListadoDeCajas";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Listado De Cajas";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmListadoDeCajas_Load);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvCarjas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
