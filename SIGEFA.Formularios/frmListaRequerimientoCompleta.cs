using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListaRequerimientoCompleta : Form
{
	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private RadGridView rgvrequerimientos;

	private RadGridView rgvTransferencias;

	private Label txtNombreProducto;

	private Label label3;

	private Label label4;

	private TextBox txtCodprod;

	private ComboBox comboBox1;

	private Label label2;

	private Button btnBusqueda;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Label label1;

	private ComboBox cmbAlmacenes;

	public frmListaRequerimientoCompleta()
	{
		InitializeComponent();
	}

	private void frmListaRequerimientoCompleta_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		int opcionFecha = 1;
		int codProducto = 0;
		int.TryParse(txtCodprod.Text, out codProducto);
		rgvrequerimientos.DataSource = admreqalm.ListadoRequerimientos(frmLogin.iCodAlmacen, frmLogin.iCodSucursal, opcionFecha, dtpDesde.Value, dtpHasta.Value, codProducto);
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void rgvrequerimientos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codigoreq = Convert.ToInt32(e.Row.Cells["colCodReqAlmacen"].Value);
			rgvTransferencias.DataSource = admreqalm.listadoTransferenciasGeneradas(codigoreq, frmLogin.iCodAlmacen);
		}
	}

	private void rgvrequerimientos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
		{
			return;
		}
		if (e.Column.Name == "colDocReqAlmacen")
		{
			int codReqAlmacen = Convert.ToInt32(e.Row.Cells["colCodReqAlmacen"].Value);
			clsRequerimientoAlmacen req = admreqalm.CargaRequerimiento(codReqAlmacen);
			if (codReqAlmacen != 0)
			{
				frmReqAlmacen form = mdi_Menu.buscarFrmReqAlmacen("frmReqAlmacen", codReqAlmacen, 2);
				if (form != null)
				{
					form.Activate();
					return;
				}
				form = new frmReqAlmacen();
				form.MdiParent = base.MdiParent;
				form.codRequerimientoAlmacen = codReqAlmacen;
				form.TipoReq = req.Tipo;
				form.verificarAnulacion = req.CodAlmacenSolicitante == frmLogin.iCodAlmacen && req.Tipo == 1 && (req.IEstado == 7 || req.IEstado == 8);
				form.Proceso = 2;
				form.Show();
			}
		}
		else if (e.Column.Name == "colTituloDocVenta")
		{
			int codFacturaVenta = Convert.ToInt32(e.Row.Cells["colCodigoDocVenta"].Value);
			if (codFacturaVenta > 0)
			{
				frmVenta form2 = new frmVenta();
				form2.MdiParent = base.MdiParent;
				form2.CodVenta = codFacturaVenta.ToString();
				form2.Proceso = 3;
				form2.Show();
			}
		}
	}

	private void rgvTransferencias_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0 && e.Column.Name == "colDocTrans")
		{
			int coTrans = Convert.ToInt32(e.Row.Cells["colCodTransDir"].Value);
			int caso = Convert.ToInt32(e.Row.Cells["colCaso"].Value);
			F2TransferenciaEntreAlmacenes form = new F2TransferenciaEntreAlmacenes();
			form.MdiParent = base.MdiParent;
			form.CodTransDirecta = coTrans;
			form.Proceso = 3;
			form.caso = caso;
			form.Show();
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		int opcionFecha = 1;
		int codProducto = 0;
		int.TryParse(txtCodprod.Text, out codProducto);
		rgvrequerimientos.DataSource = admreqalm.ListadoRequerimientos(0, frmLogin.iCodSucursal, opcionFecha, dtpDesde.Value, dtpHasta.Value, codProducto);
		rgvTransferencias.DataSource = null;
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 6;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void label1_Click(object sender, EventArgs e)
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvrequerimientos = new Telerik.WinControls.UI.RadGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.rgvTransferencias = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvTransferencias).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTransferencias.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.txtNombreProducto);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtCodprod);
		this.groupBox1.Controls.Add(this.comboBox1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1280, 92);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(888, 50);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(51, 13);
		this.label1.TabIndex = 62;
		this.label1.Text = "Almacen:";
		this.label1.Visible = false;
		this.label1.Click += new System.EventHandler(label1_Click);
		this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbAlmacenes.Location = new System.Drawing.Point(962, 46);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(182, 21);
		this.cmbAlmacenes.TabIndex = 61;
		this.cmbAlmacenes.Visible = false;
		this.cmbAlmacenes.SelectedIndexChanged += new System.EventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(609, 57);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 57;
		this.txtNombreProducto.Text = "---";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(568, 57);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 56;
		this.label3.Text = "Prod.:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(568, 18);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(112, 13);
		this.label4.TabIndex = 55;
		this.label4.Text = "Busqueda x Producto:";
		this.txtCodprod.Location = new System.Drawing.Point(571, 34);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(171, 20);
		this.txtCodprod.TabIndex = 54;
		this.txtCodprod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Items.AddRange(new object[7] { "Pendiente", "Aprobado", "Facturado", "Entregado Parcial", "Entregado Total", "Anulado", "Todos" });
		this.comboBox1.Location = new System.Drawing.Point(962, 19);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(121, 21);
		this.comboBox1.TabIndex = 53;
		this.comboBox1.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(889, 23);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(67, 13);
		this.label2.TabIndex = 52;
		this.label2.Text = "Tipo  Fecha:";
		this.label2.Visible = false;
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.cambio;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.Location = new System.Drawing.Point(431, 26);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(118, 36);
		this.btnBusqueda.TabIndex = 51;
		this.btnBusqueda.Text = "Recargar Lista";
		this.btnBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(213, 38);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 50;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 38);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 49;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(70, 35);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 48;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(260, 34);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 47;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.rgvrequerimientos);
		this.groupBox2.Location = new System.Drawing.Point(3, 98);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1277, 247);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.rgvrequerimientos.AutoScroll = true;
		this.rgvrequerimientos.AutoSizeRows = true;
		this.rgvrequerimientos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvrequerimientos.EnableGestures = false;
		this.rgvrequerimientos.Location = new System.Drawing.Point(3, 16);
		this.rgvrequerimientos.MasterTemplate.AllowAddNewRow = false;
		this.rgvrequerimientos.MasterTemplate.AllowColumnChooser = false;
		this.rgvrequerimientos.MasterTemplate.AllowColumnReorder = false;
		this.rgvrequerimientos.MasterTemplate.AllowDeleteRow = false;
		this.rgvrequerimientos.MasterTemplate.AllowDragToGroup = false;
		this.rgvrequerimientos.MasterTemplate.AllowEditRow = false;
		this.rgvrequerimientos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codReqAlmacen";
		gridViewTextBoxColumn1.HeaderText = "codReqAlmacen";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodReqAlmacen";
		gridViewTextBoxColumn2.FieldName = "docReqAlmacen";
		gridViewTextBoxColumn2.HeaderText = "Requerimiento";
		gridViewTextBoxColumn2.Name = "colDocReqAlmacen";
		gridViewTextBoxColumn2.Width = 118;
		gridViewTextBoxColumn3.FieldName = "codAlmacenSolicitante";
		gridViewTextBoxColumn3.HeaderText = "codAlmacenSolicitante";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "colCodAlmacenSolicitante";
		gridViewTextBoxColumn4.FieldName = "descAlmacenSolicitante";
		gridViewTextBoxColumn4.HeaderText = "Almacen Solicitante";
		gridViewTextBoxColumn4.Name = "colDescAlmacenSolicitante";
		gridViewTextBoxColumn4.Width = 141;
		gridViewTextBoxColumn5.FieldName = "codAlmacenDespachador";
		gridViewTextBoxColumn5.HeaderText = "codAlmacenDespachador";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "colCodAlmacenDespachador";
		gridViewTextBoxColumn6.FieldName = "descAlmacenDespachador";
		gridViewTextBoxColumn6.HeaderText = "Almacen Despachador";
		gridViewTextBoxColumn6.Name = "colDescAlmacenDespachador";
		gridViewTextBoxColumn6.Width = 141;
		gridViewTextBoxColumn7.FieldName = "tituloDocumentoVenta";
		gridViewTextBoxColumn7.HeaderText = "Factura Venta";
		gridViewTextBoxColumn7.Name = "colTituloDocVenta";
		gridViewTextBoxColumn7.Width = 110;
		gridViewTextBoxColumn8.FieldName = "comentarioDespacho";
		gridViewTextBoxColumn8.HeaderText = "Comentario Despacho";
		gridViewTextBoxColumn8.Name = "colComentarioDespacho";
		gridViewTextBoxColumn8.Width = 195;
		gridViewTextBoxColumn9.FieldName = "usuario";
		gridViewTextBoxColumn9.HeaderText = "Usuario";
		gridViewTextBoxColumn9.Name = "colUsuario";
		gridViewTextBoxColumn9.Width = 127;
		gridViewTextBoxColumn10.FieldName = "fechaRegistro";
		gridViewTextBoxColumn10.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn10.Name = "colFechaRegistro";
		gridViewTextBoxColumn10.Width = 137;
		gridViewTextBoxColumn11.FieldName = "codEstado";
		gridViewTextBoxColumn11.HeaderText = "codEstado";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "colCodEstado";
		gridViewTextBoxColumn12.FieldName = "descEstado";
		gridViewTextBoxColumn12.HeaderText = "Estado";
		gridViewTextBoxColumn12.Name = "colDescEstado";
		gridViewTextBoxColumn12.Width = 147;
		gridViewTextBoxColumn13.FieldName = "descTipoReq";
		gridViewTextBoxColumn13.HeaderText = "Tipo Req.";
		gridViewTextBoxColumn13.Name = "colTipoReq";
		gridViewTextBoxColumn13.Width = 162;
		gridViewTextBoxColumn14.FieldName = "codigoDocumentoVenta";
		gridViewTextBoxColumn14.HeaderText = "Codigo Doc Venta";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "colCodigoDocVenta";
		gridViewTextBoxColumn14.Width = 49;
		this.rgvrequerimientos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14);
		this.rgvrequerimientos.MasterTemplate.EnableFiltering = true;
		this.rgvrequerimientos.MasterTemplate.EnableGrouping = false;
		this.rgvrequerimientos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvrequerimientos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvrequerimientos.Name = "rgvrequerimientos";
		this.rgvrequerimientos.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.Size = new System.Drawing.Size(1271, 228);
		this.rgvrequerimientos.TabIndex = 1;
		this.rgvrequerimientos.TitleText = "Listado Requerimientos";
		this.rgvrequerimientos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvrequerimientos_CellClick);
		this.rgvrequerimientos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvrequerimientos_CellDoubleClick);
		this.groupBox3.Controls.Add(this.rgvTransferencias);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 351);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1280, 198);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.rgvTransferencias.AutoScroll = true;
		this.rgvTransferencias.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvTransferencias.Location = new System.Drawing.Point(3, 16);
		this.rgvTransferencias.MasterTemplate.AllowAddNewRow = false;
		this.rgvTransferencias.MasterTemplate.AllowColumnReorder = false;
		this.rgvTransferencias.MasterTemplate.AllowDeleteRow = false;
		this.rgvTransferencias.MasterTemplate.AllowDragToGroup = false;
		this.rgvTransferencias.MasterTemplate.AllowEditRow = false;
		this.rgvTransferencias.MasterTemplate.AllowRowResize = false;
		this.rgvTransferencias.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn15.FieldName = "docTrans";
		gridViewTextBoxColumn15.HeaderText = "Transferencia";
		gridViewTextBoxColumn15.Name = "colDocTrans";
		gridViewTextBoxColumn15.Width = 133;
		gridViewTextBoxColumn16.FieldName = "estado";
		gridViewTextBoxColumn16.HeaderText = "Estado";
		gridViewTextBoxColumn16.Name = "colEstado";
		gridViewTextBoxColumn16.Width = 144;
		gridViewTextBoxColumn17.FieldName = "codTransDir";
		gridViewTextBoxColumn17.HeaderText = "CodTransDir";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "colCodTransDir";
		gridViewTextBoxColumn18.FieldName = "caso";
		gridViewTextBoxColumn18.HeaderText = "caso";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "colCaso";
		gridViewTextBoxColumn19.FieldName = "codAlmacenOrigen";
		gridViewTextBoxColumn19.HeaderText = "codAlmacenOrigen";
		gridViewTextBoxColumn19.IsVisible = false;
		gridViewTextBoxColumn19.Name = "colCodAlmacenOrigen";
		gridViewTextBoxColumn20.FieldName = "almacenorigen";
		gridViewTextBoxColumn20.HeaderText = "Almacen Origen";
		gridViewTextBoxColumn20.Name = "colAlmacenOrigen";
		gridViewTextBoxColumn20.Width = 144;
		gridViewTextBoxColumn21.FieldName = "codAlmacenDestino";
		gridViewTextBoxColumn21.HeaderText = "codAlmacenDestino";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "colCodAlmacenDestino";
		gridViewTextBoxColumn22.FieldName = "almacendestino";
		gridViewTextBoxColumn22.HeaderText = "Almacen Destino";
		gridViewTextBoxColumn22.Name = "colAlmacenDestino";
		gridViewTextBoxColumn22.Width = 144;
		gridViewTextBoxColumn23.FieldName = "fecharegistro";
		gridViewTextBoxColumn23.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn23.Name = "colFechaRegistro";
		gridViewTextBoxColumn23.Width = 157;
		gridViewTextBoxColumn24.FieldName = "fechaenvio";
		gridViewTextBoxColumn24.HeaderText = "Fecha Envio";
		gridViewTextBoxColumn24.Name = "colFechaEnvio";
		gridViewTextBoxColumn24.Width = 157;
		gridViewTextBoxColumn25.FieldName = "fechaentrega";
		gridViewTextBoxColumn25.HeaderText = "Fecha Entrega";
		gridViewTextBoxColumn25.Name = "colFechaEntrega";
		gridViewTextBoxColumn25.Width = 157;
		gridViewTextBoxColumn26.FieldName = "decripcionRechazo";
		gridViewTextBoxColumn26.HeaderText = "Comentario Rechazo";
		gridViewTextBoxColumn26.Name = "colComentarioRechazo";
		gridViewTextBoxColumn26.Width = 244;
		this.rgvTransferencias.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26);
		this.rgvTransferencias.MasterTemplate.EnableFiltering = true;
		this.rgvTransferencias.MasterTemplate.EnableGrouping = false;
		this.rgvTransferencias.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvTransferencias.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvTransferencias.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvTransferencias.Name = "rgvTransferencias";
		this.rgvTransferencias.ReadOnly = true;
		this.rgvTransferencias.ShowHeaderCellButtons = true;
		this.rgvTransferencias.Size = new System.Drawing.Size(1274, 179);
		this.rgvTransferencias.TabIndex = 0;
		this.rgvTransferencias.ThemeName = "TelerikMetroBlue";
		this.rgvTransferencias.TitleText = "Listado Transferencias";
		this.rgvTransferencias.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvTransferencias_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1280, 549);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListaRequerimientoCompleta";
		this.Text = "Listado Requerimientos Con Transferencias";
		base.Load += new System.EventHandler(frmListaRequerimientoCompleta_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvTransferencias.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTransferencias).EndInit();
		base.ResumeLayout(false);
	}
}
