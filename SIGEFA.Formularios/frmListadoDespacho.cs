using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoDespacho : Form
{
	private clsAdmDespacho admdespacho = new clsAdmDespacho();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private int CodCliente = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private RadGridView rgvDetalle;

	private ComboBox cmbTipoFecha;

	private Label label2;

	private Button btnBusqueda;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Label lblNombreCliente;

	private Label label1;

	private TextBox txtRucCliente;

	private Label label4;

	private ComboBox cmbAlmacenes;

	private Label label3;

	private ComboBox cmbTipoListado;

	private Label label7;

	private ComboBox cmbEstado;

	private GroupBox groupBox3;

	private DataGridView dgvEntregas;

	private DataGridViewTextBoxColumn colCodEntrega;

	private DataGridViewTextBoxColumn colTituloEntrega;

	private DataGridViewTextBoxColumn colFecha;

	private DataGridViewTextBoxColumn colEstado;

	public frmListadoDespacho()
	{
		InitializeComponent();
	}

	private void frmListadoDespacho_Load(object sender, EventArgs e)
	{
		dgvEntregas.AutoGenerateColumns = false;
		cargaAlmacenes();
		cmbEstado.SelectedIndex = 1;
		cmbTipoListado.SelectedIndex = 0;
		rgvDetalle.DataSource = admdespacho.listaDespacho(0, frmLogin.iCodSucursal, -1, dtpDesde.Value, dtpHasta.Value, 0, cmbTipoListado.SelectedIndex, cmbEstado.SelectedIndex);
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void rgvDetalle_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codDespacho = Convert.ToInt32(e.Row.Cells["colCodDespacho"].Value);
			int estado = Convert.ToInt32(e.Row.Cells["colCodEstado"].Value);
			int anulado = Convert.ToInt32(e.Row.Cells["colCodAnulado"].Value);
			frmDespacho form = new frmDespacho();
			form.MdiParent = base.MdiParent;
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.codDespacho = codDespacho;
			form.Proceso = ((estado == 1 && anulado == 0) ? 1 : 2);
			form.Show();
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		int tipoFecha = cmbTipoFecha.SelectedIndex;
		rgvDetalle.DataSource = admdespacho.listaDespacho(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, tipoFecha, dtpDesde.Value, dtpHasta.Value, CodCliente, cmbTipoListado.SelectedIndex, cmbEstado.SelectedIndex);
	}

	private void txtRucCliente_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.F1)
			{
				CodCliente = 0;
				txtRucCliente.Text = "";
				lblNombreCliente.Text = "";
				if (Application.OpenForms["frmClientesLista"] != null)
				{
					Application.OpenForms["frmClientesLista"].Activate();
				}
				else
				{
					frmClientesLista form = new frmClientesLista();
					form.Proceso = 3;
					form.ShowDialog();
					CodCliente = form.cli.CodCliente;
					if (CodCliente != 0)
					{
						txtRucCliente.Text = ((form.cli.Ruc == "") ? "00000000" : form.cli.Ruc);
						lblNombreCliente.Text = form.cli.RazonSocial;
					}
				}
			}
			if (txtRucCliente.Text.ToString().Trim().Length == 0)
			{
				CodCliente = 0;
				txtRucCliente.Text = "";
				lblNombreCliente.Text = "";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cmbTipoListado_SelectedIndexChanged(object sender, EventArgs e)
	{
		btnBusqueda.PerformClick();
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
	{
		btnBusqueda.PerformClick();
	}

	private void rgvDetalle_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codDespacho = Convert.ToInt32(e.Row.Cells["colCodDespacho"].Value);
			dgvEntregas.DataSource = admdespacho.listaEntregas(codDespacho);
		}
	}

	private void dgvEntregas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codEntrega = Convert.ToInt32(dgvEntregas.Rows[e.RowIndex].Cells[colCodEntrega.Name].Value);
			clsEntrega entrega = admdespacho.cargaEntrega(codEntrega);
			frmEntrega form = new frmEntrega();
			form.codEntrega = codEntrega;
			form.ShowDialog();
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbTipoListado = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.lblNombreCliente = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtRucCliente = new System.Windows.Forms.TextBox();
		this.cmbTipoFecha = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgvEntregas = new System.Windows.Forms.DataGridView();
		this.colCodEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colTituloEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEntregas).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.cmbEstado);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.cmbTipoListado);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.lblNombreCliente);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtRucCliente);
		this.groupBox1.Controls.Add(this.cmbTipoFecha);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1261, 80);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(869, 15);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(43, 13);
		this.label7.TabIndex = 56;
		this.label7.Text = "Estado:";
		this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[4] { "Todos", "Pendientes", "Atendidos Totalmente", "Anulados" });
		this.cmbEstado.Location = new System.Drawing.Point(918, 11);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(182, 21);
		this.cmbEstado.TabIndex = 55;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(607, 49);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 13);
		this.label3.TabIndex = 54;
		this.label3.Text = "Tipo Listado:";
		this.cmbTipoListado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipoListado.FormattingEnabled = true;
		this.cmbTipoListado.Items.AddRange(new object[2] { "Despachos Por Atender", "Despachos Según Ventas Locales" });
		this.cmbTipoListado.Location = new System.Drawing.Point(681, 45);
		this.cmbTipoListado.Name = "cmbTipoListado";
		this.cmbTipoListado.Size = new System.Drawing.Size(204, 21);
		this.cmbTipoListado.TabIndex = 53;
		this.cmbTipoListado.SelectedIndexChanged += new System.EventHandler(cmbTipoListado_SelectedIndexChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(607, 15);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(51, 13);
		this.label4.TabIndex = 52;
		this.label4.Text = "Almacen:";
		this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbAlmacenes.Location = new System.Drawing.Point(681, 11);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(182, 21);
		this.cmbAlmacenes.TabIndex = 51;
		this.cmbAlmacenes.SelectedIndexChanged += new System.EventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.lblNombreCliente.BackColor = System.Drawing.Color.White;
		this.lblNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lblNombreCliente.Location = new System.Drawing.Point(287, 46);
		this.lblNombreCliente.Name = "lblNombreCliente";
		this.lblNombreCliente.Size = new System.Drawing.Size(314, 20);
		this.lblNombreCliente.TabIndex = 50;
		this.lblNombreCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 49);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(111, 13);
		this.label1.TabIndex = 50;
		this.label1.Text = "Busqueda por Cliente:";
		this.txtRucCliente.Location = new System.Drawing.Point(131, 46);
		this.txtRucCliente.Name = "txtRucCliente";
		this.txtRucCliente.Size = new System.Drawing.Size(150, 20);
		this.txtRucCliente.TabIndex = 49;
		this.txtRucCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRucCliente_KeyDown);
		this.cmbTipoFecha.FormattingEnabled = true;
		this.cmbTipoFecha.Items.AddRange(new object[2] { "Fecha Registro", "Fecha Despacho" });
		this.cmbTipoFecha.Location = new System.Drawing.Point(85, 12);
		this.cmbTipoFecha.Name = "cmbTipoFecha";
		this.cmbTipoFecha.Size = new System.Drawing.Size(146, 21);
		this.cmbTipoFecha.TabIndex = 48;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(67, 13);
		this.label2.TabIndex = 47;
		this.label2.Text = "Tipo  Fecha:";
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.cambio;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.Location = new System.Drawing.Point(1137, 19);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(118, 36);
		this.btnBusqueda.TabIndex = 46;
		this.btnBusqueda.Text = "Recargar Lista";
		this.btnBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(433, 16);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 45;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(237, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 44;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(290, 13);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 43;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(480, 12);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 42;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.rgvDetalle);
		this.groupBox2.Location = new System.Drawing.Point(0, 80);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1261, 289);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.rgvDetalle.AutoScroll = true;
		this.rgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.rgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.rgvDetalle.MasterTemplate.AllowColumnReorder = false;
		this.rgvDetalle.MasterTemplate.AllowDeleteRow = false;
		this.rgvDetalle.MasterTemplate.AllowDragToGroup = false;
		this.rgvDetalle.MasterTemplate.AllowEditRow = false;
		this.rgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codDespacho";
		gridViewTextBoxColumn1.HeaderText = "codDespacho";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodDespacho";
		gridViewTextBoxColumn2.FieldName = "refDespacho";
		gridViewTextBoxColumn2.HeaderText = "Despacho";
		gridViewTextBoxColumn2.Name = "colRefDespacho";
		gridViewTextBoxColumn2.Width = 173;
		gridViewTextBoxColumn3.FieldName = "tituloReqAlmacen";
		gridViewTextBoxColumn3.HeaderText = "Req Almacen";
		gridViewTextBoxColumn3.Name = "colTituloReqAlmacen";
		gridViewTextBoxColumn3.Width = 174;
		gridViewTextBoxColumn4.FieldName = "fechaDespacho";
		gridViewTextBoxColumn4.HeaderText = "Fecha Despacho";
		gridViewTextBoxColumn4.Name = "colFechaDespacho";
		gridViewTextBoxColumn4.Width = 196;
		gridViewTextBoxColumn5.FieldName = "fechaRegistro";
		gridViewTextBoxColumn5.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn5.Name = "colFechaRegistro";
		gridViewTextBoxColumn5.Width = 196;
		gridViewTextBoxColumn6.FieldName = "codAlmacenRegistro";
		gridViewTextBoxColumn6.HeaderText = "codAlmacenRegistro";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "colCodAlmacenRegistro";
		gridViewTextBoxColumn7.FieldName = "descripAlmacenRegistro";
		gridViewTextBoxColumn7.HeaderText = "Almacen Registro";
		gridViewTextBoxColumn7.Name = "colDescAlmacenRegistro";
		gridViewTextBoxColumn7.Width = 174;
		gridViewTextBoxColumn8.FieldName = "codTablaDocRelacionada";
		gridViewTextBoxColumn8.HeaderText = "codTablaDocRelacionado";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colCodTablaDocRelacionado";
		gridViewTextBoxColumn9.FieldName = "codDocRelacionado";
		gridViewTextBoxColumn9.HeaderText = "codDocRelacionado";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "colCodDocRelacionado";
		gridViewTextBoxColumn10.FieldName = "tituloDocumento";
		gridViewTextBoxColumn10.HeaderText = "Documento Relacionado";
		gridViewTextBoxColumn10.Name = "colTituloDocumento";
		gridViewTextBoxColumn10.Width = 174;
		gridViewTextBoxColumn11.FieldName = "estado";
		gridViewTextBoxColumn11.HeaderText = "Estado";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "colEstado";
		gridViewTextBoxColumn11.Width = 151;
		gridViewTextBoxColumn12.FieldName = "codEstado";
		gridViewTextBoxColumn12.HeaderText = "colCodEstado";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "colCodEstado";
		gridViewTextBoxColumn12.Width = 48;
		gridViewTextBoxColumn13.FieldName = "codAnulado";
		gridViewTextBoxColumn13.HeaderText = "colCodAnulado";
		gridViewTextBoxColumn13.IsVisible = false;
		gridViewTextBoxColumn13.Name = "colCodAnulado";
		gridViewTextBoxColumn13.Width = 48;
		gridViewTextBoxColumn14.FieldName = "descripEstado";
		gridViewTextBoxColumn14.HeaderText = "Estado";
		gridViewTextBoxColumn14.Name = "colDescripEstado";
		gridViewTextBoxColumn14.Width = 173;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.EnableGrouping = false;
		this.rgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.ReadOnly = true;
		this.rgvDetalle.ShowHeaderCellButtons = true;
		this.rgvDetalle.Size = new System.Drawing.Size(1255, 270);
		this.rgvDetalle.TabIndex = 0;
		this.rgvDetalle.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDetalle_CellClick);
		this.rgvDetalle.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDetalle_CellDoubleClick);
		this.groupBox3.Controls.Add(this.dgvEntregas);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 359);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1261, 163);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.dgvEntregas.AllowUserToAddRows = false;
		this.dgvEntregas.AllowUserToDeleteRows = false;
		this.dgvEntregas.AllowUserToResizeColumns = false;
		this.dgvEntregas.AllowUserToResizeRows = false;
		this.dgvEntregas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvEntregas.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvEntregas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvEntregas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvEntregas.Columns.AddRange(this.colCodEntrega, this.colTituloEntrega, this.colFecha, this.colEstado);
		this.dgvEntregas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvEntregas.Location = new System.Drawing.Point(3, 16);
		this.dgvEntregas.Name = "dgvEntregas";
		this.dgvEntregas.ReadOnly = true;
		this.dgvEntregas.RowHeadersVisible = false;
		this.dgvEntregas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvEntregas.Size = new System.Drawing.Size(1255, 144);
		this.dgvEntregas.TabIndex = 11;
		this.dgvEntregas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvEntregas_CellDoubleClick);
		this.colCodEntrega.DataPropertyName = "codEntrega";
		this.colCodEntrega.HeaderText = "codEntrega";
		this.colCodEntrega.Name = "colCodEntrega";
		this.colCodEntrega.ReadOnly = true;
		this.colCodEntrega.Visible = false;
		this.colTituloEntrega.DataPropertyName = "tituloEntrega";
		this.colTituloEntrega.HeaderText = "Entrega";
		this.colTituloEntrega.Name = "colTituloEntrega";
		this.colTituloEntrega.ReadOnly = true;
		this.colFecha.DataPropertyName = "fecha";
		this.colFecha.HeaderText = "Fecha";
		this.colFecha.Name = "colFecha";
		this.colFecha.ReadOnly = true;
		this.colEstado.DataPropertyName = "estado";
		this.colEstado.HeaderText = "Estado";
		this.colEstado.Name = "colEstado";
		this.colEstado.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1261, 522);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListadoDespacho";
		this.Text = "Listado de Despachos";
		base.Load += new System.EventHandler(frmListadoDespacho_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvEntregas).EndInit();
		base.ResumeLayout(false);
	}
}
