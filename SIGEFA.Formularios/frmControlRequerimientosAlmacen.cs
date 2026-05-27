using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmControlRequerimientosAlmacen : Form
{
	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private int codProdBuscar = 0;

	private clsAdmDespacho admdesp = new clsAdmDespacho();

	private IContainer components = null;

	private RadGridView rgvrequerimientos;

	private GroupBox groupBox1;

	private Button btnEditar;

	private Button btnVer;

	private Label txtNombreProducto;

	private Label label3;

	private Label label4;

	private TextBox txtCodprod;

	private ComboBox cmbTipoFecha;

	private Label label2;

	private Button btnBusqueda;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private GroupBox groupBox2;

	private Label label7;

	private ComboBox cmbEstado;

	private Label label1;

	private ComboBox cmbAlmacenes;

	public frmControlRequerimientosAlmacen()
	{
		InitializeComponent();
	}

	private void frmControlRequerimientosAlmacen_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		cmbEstado.SelectedIndex = 1;
		recargaLista();
	}

	private void recargaLista()
	{
		int tipoFecha = cmbTipoFecha.SelectedIndex;
		codProdBuscar = ((!(txtCodprod.Text.Trim() == "")) ? codProdBuscar : 0);
		rgvrequerimientos.DataSource = admreqalm.ListadoParaControlDeRequerimiento(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, tipoFecha, dtpDesde.Value, dtpHasta.Value, codProdBuscar, cmbEstado.SelectedIndex);
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		recargaLista();
	}

	private void rgvrequerimientos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			if (e.Column.Name == "colDocReqAlmacen")
			{
				int codReqAlmacen = Convert.ToInt32(e.Row.Cells["colCodReqAlmacen"].Value);
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
					form.Proceso = 2;
					form.Show();
				}
			}
			else if (e.Column.Name == "colRefDespacho")
			{
				int codDespacho = Convert.ToInt32(e.Row.Cells["colCodDespacho"].Value);
				if (codDespacho > 0)
				{
					clsDespacho desp = admdesp.cargaDespacho(codDespacho);
					int estado = desp.Estado;
					int anulado = desp.Anulado;
					frmDespacho form2 = new frmDespacho();
					form2.MdiParent = base.MdiParent;
					form2.Dock = DockStyle.Fill;
					form2.WindowState = FormWindowState.Maximized;
					form2.codDespacho = codDespacho;
					form2.Proceso = ((estado == 1 && anulado == 0) ? 1 : 2);
					form2.Show();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
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
				txtCodprod.Text = frm.GetCodigoProducto().ToString().PadLeft(9, '0');
				codProdBuscar = frm.GetCodigoProducto2();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
	}

	private void groupBox1_Enter(object sender, EventArgs e)
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.rgvrequerimientos = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnVer = new System.Windows.Forms.Button();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.cmbTipoFecha = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
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
		gridViewTextBoxColumn1.FieldName = "fechaRegistro";
		gridViewTextBoxColumn1.HeaderText = "Fecha Requerimiento";
		gridViewTextBoxColumn1.Name = "colFechaRegistro";
		gridViewTextBoxColumn1.Width = 126;
		gridViewTextBoxColumn1.WrapText = true;
		gridViewTextBoxColumn2.FieldName = "fechaRequerimiento";
		gridViewTextBoxColumn2.HeaderText = "Fecha Req Ocultar";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colFechaRequerimiento";
		gridViewTextBoxColumn2.Width = 132;
		gridViewTextBoxColumn2.WrapText = true;
		gridViewTextBoxColumn3.FieldName = "codReqAlmacen";
		gridViewTextBoxColumn3.HeaderText = "codReqAlmacen";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "colCodReqAlmacen";
		gridViewTextBoxColumn4.FieldName = "docReqAlmacen";
		gridViewTextBoxColumn4.HeaderText = "Requerimiento";
		gridViewTextBoxColumn4.Name = "colDocReqAlmacen";
		gridViewTextBoxColumn4.Width = 123;
		gridViewTextBoxColumn4.WrapText = true;
		gridViewTextBoxColumn5.FieldName = "codEstado";
		gridViewTextBoxColumn5.HeaderText = "codEstado";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "colCodEstado";
		gridViewTextBoxColumn6.FieldName = "descEstado";
		gridViewTextBoxColumn6.HeaderText = "Estado";
		gridViewTextBoxColumn6.Name = "colDescEstado";
		gridViewTextBoxColumn6.Width = 99;
		gridViewTextBoxColumn7.FieldName = "descTipoReq";
		gridViewTextBoxColumn7.HeaderText = "Tipo Req.";
		gridViewTextBoxColumn7.Name = "colTipoReq";
		gridViewTextBoxColumn7.Width = 114;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "codAlmacenSolicitante";
		gridViewTextBoxColumn8.HeaderText = "codAlmacenSolicitante";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colCodAlmacenSolicitante";
		gridViewTextBoxColumn9.FieldName = "descAlmacenSolicitante";
		gridViewTextBoxColumn9.HeaderText = "Almacen Solicitante";
		gridViewTextBoxColumn9.Name = "colDescAlmacenSolicitante";
		gridViewTextBoxColumn9.Width = 123;
		gridViewTextBoxColumn9.WrapText = true;
		gridViewTextBoxColumn10.FieldName = "codAlmacenDespachador";
		gridViewTextBoxColumn10.HeaderText = "codAlmacenDespachador";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "colCodAlmacenDespachador";
		gridViewTextBoxColumn11.FieldName = "descAlmacenDespachador";
		gridViewTextBoxColumn11.HeaderText = "Almacen Despachador";
		gridViewTextBoxColumn11.Name = "colDescAlmacenDespachador";
		gridViewTextBoxColumn11.Width = 141;
		gridViewTextBoxColumn11.WrapText = true;
		gridViewTextBoxColumn12.FieldName = "codDespacho";
		gridViewTextBoxColumn12.HeaderText = "CodDespacho";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "colCodDespacho";
		gridViewTextBoxColumn13.FieldName = "refDespacho";
		gridViewTextBoxColumn13.HeaderText = "Despacho Relac.";
		gridViewTextBoxColumn13.Name = "colRefDespacho";
		gridViewTextBoxColumn13.Width = 126;
		gridViewTextBoxColumn14.FieldName = "estadoDespacho";
		gridViewTextBoxColumn14.HeaderText = "Estado Despacho";
		gridViewTextBoxColumn14.Name = "colEstadoDespacho";
		gridViewTextBoxColumn14.Width = 143;
		gridViewTextBoxColumn15.FieldName = "observacion";
		gridViewTextBoxColumn15.HeaderText = "Observacion";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "colObservacion";
		gridViewTextBoxColumn15.Width = 118;
		gridViewTextBoxColumn16.FieldName = "comentarioDespacho";
		gridViewTextBoxColumn16.HeaderText = "Comentario Despacho";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "colComentarioDespacho";
		gridViewTextBoxColumn16.Width = 188;
		gridViewTextBoxColumn17.FieldName = "fechaDespacho";
		gridViewTextBoxColumn17.HeaderText = "Fecha Despacho";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "colFechaDespacho";
		gridViewTextBoxColumn17.Width = 136;
		gridViewTextBoxColumn17.WrapText = true;
		gridViewTextBoxColumn18.FieldName = "usuario";
		gridViewTextBoxColumn18.HeaderText = "Usuario";
		gridViewTextBoxColumn18.Name = "colUsuario";
		gridViewTextBoxColumn18.Width = 115;
		gridViewTextBoxColumn19.FieldName = "FechaPrimeraEntrega";
		gridViewTextBoxColumn19.HeaderText = "Primera Entrega";
		gridViewTextBoxColumn19.Name = "colFechaPriEntrega";
		gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn19.Width = 120;
		gridViewTextBoxColumn20.FieldName = "FechaUltimaEntrega";
		gridViewTextBoxColumn20.HeaderText = "Ultima Entrega";
		gridViewTextBoxColumn20.Name = "colFechaUltimaEntrega";
		gridViewTextBoxColumn20.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn20.Width = 122;
		this.rgvrequerimientos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20);
		this.rgvrequerimientos.MasterTemplate.EnableFiltering = true;
		this.rgvrequerimientos.MasterTemplate.EnableGrouping = false;
		this.rgvrequerimientos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvrequerimientos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvrequerimientos.Name = "rgvrequerimientos";
		this.rgvrequerimientos.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.Size = new System.Drawing.Size(1343, 274);
		this.rgvrequerimientos.TabIndex = 0;
		this.rgvrequerimientos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvrequerimientos_CellDoubleClick);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.cmbEstado);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.btnEditar);
		this.groupBox1.Controls.Add(this.btnVer);
		this.groupBox1.Controls.Add(this.txtNombreProducto);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtCodprod);
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
		this.groupBox1.Size = new System.Drawing.Size(1349, 132);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(541, 86);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(43, 13);
		this.label7.TabIndex = 60;
		this.label7.Text = "Estado:";
		this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[4] { "Todos", "Pendientes", "Atendidos Totalmente", "Anulados" });
		this.cmbEstado.Location = new System.Drawing.Point(590, 82);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(182, 21);
		this.cmbEstado.TabIndex = 59;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(229, 85);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(118, 13);
		this.label1.TabIndex = 58;
		this.label1.Text = "Almacen Despachador:";
		this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbAlmacenes.Location = new System.Drawing.Point(353, 82);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(182, 21);
		this.cmbAlmacenes.TabIndex = 57;
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEditar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btnEditar.Location = new System.Drawing.Point(1171, 87);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(80, 39);
		this.btnEditar.TabIndex = 48;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Visible = false;
		this.btnVer.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnVer.Image = SIGEFA.Properties.Resources.buscar;
		this.btnVer.Location = new System.Drawing.Point(1257, 87);
		this.btnVer.Name = "btnVer";
		this.btnVer.Size = new System.Drawing.Size(80, 39);
		this.btnVer.TabIndex = 47;
		this.btnVer.Text = "Ver";
		this.btnVer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnVer.UseVisualStyleBackColor = true;
		this.btnVer.Visible = false;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(53, 105);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 46;
		this.txtNombreProducto.Text = "---";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 105);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 45;
		this.label3.Text = "Prod.:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(12, 66);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(112, 13);
		this.label4.TabIndex = 43;
		this.label4.Text = "Busqueda x Producto:";
		this.txtCodprod.Location = new System.Drawing.Point(15, 82);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(171, 20);
		this.txtCodprod.TabIndex = 42;
		this.txtCodprod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.cmbTipoFecha.FormattingEnabled = true;
		this.cmbTipoFecha.Items.AddRange(new object[3] { "Fecha Requerimiento", "Fecha Despacho", "Fecha Registro" });
		this.cmbTipoFecha.Location = new System.Drawing.Point(85, 35);
		this.cmbTipoFecha.Name = "cmbTipoFecha";
		this.cmbTipoFecha.Size = new System.Drawing.Size(159, 21);
		this.cmbTipoFecha.TabIndex = 41;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 39);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(67, 13);
		this.label2.TabIndex = 40;
		this.label2.Text = "Tipo  Fecha:";
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.cambio;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.Location = new System.Drawing.Point(657, 26);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(118, 36);
		this.btnBusqueda.TabIndex = 39;
		this.btnBusqueda.Text = "Recargar Lista";
		this.btnBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(446, 38);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 36;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(250, 38);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 35;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(303, 35);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 34;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(493, 34);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 33;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.rgvrequerimientos);
		this.groupBox2.Location = new System.Drawing.Point(0, 138);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1349, 293);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1349, 431);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Name = "frmControlRequerimientosAlmacen";
		this.Text = "Control de Requerimientos Almacen";
		base.Load += new System.EventHandler(frmControlRequerimientosAlmacen_Load);
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
