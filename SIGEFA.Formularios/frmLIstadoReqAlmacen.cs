using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmLIstadoReqAlmacen : Office2007Form
{
	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private int codReqAlmacen = 0;

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private RadGridView rgvrequerimientos;

	private ComboBox comboBox1;

	private Label label2;

	private Button btnBusqueda;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnEditar;

	private Button btnVer;

	private Label txtNombreProducto;

	private Label label3;

	private Label label4;

	private TextBox txtCodprod;

	private Label label1;

	private ComboBox cmbAlmacenes;

	public frmLIstadoReqAlmacen()
	{
		InitializeComponent();
	}

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void rgvrequerimientos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex <= -1)
		{
			return;
		}
		codReqAlmacen = Convert.ToInt32(e.Row.Cells["colCodReqAlmacen"].Value);
		int estado = Convert.ToInt32(e.Row.Cells["colCodEstado"].Value);
		if (estado != 12 && estado != 11)
		{
			int codAlmacen = Convert.ToInt32(e.Row.Cells["colCodAlmacenDespachador"].Value);
			DataTable almacenes = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
			List<DataRow> encontrado = (from x in almacenes.AsEnumerable()
				where Convert.ToInt32(x.Field<object>("cod")) == codAlmacen
				select x).ToList();
			btnEditar.Visible = encontrado.Count > 0;
		}
		else
		{
			btnEditar.Visible = false;
			codReqAlmacen = 0;
		}
		if (btnEditar.Visible)
		{
			clsAdmAcceso AdmAcce = new clsAdmAcceso();
			int permiso = new clsAdmFormulario().getPermisoEditarReqReposStock();
			List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
			bool band = accesos.Contains(permiso) || frmLogin.iNivelUser == 1;
			btnEditar.Visible = band;
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		int opcionFecha = 1;
		int codProducto = 0;
		int.TryParse(txtCodprod.Text, out codProducto);
		int codAlmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
		rgvrequerimientos.DataSource = admreqalm.ListadoRequerimientos(codAlmacen, frmLogin.iCodSucursal, opcionFecha, dtpDesde.Value, dtpHasta.Value, codProducto, 1);
	}

	private void btnVer_Click(object sender, EventArgs e)
	{
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

	private void btnEditar_Click(object sender, EventArgs e)
	{
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
			form.Proceso = 1;
			form.bandEditar = true;
			form.Show();
		}
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 2;
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

	private void frmLIstadoReqAlmacen_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		int opcionFecha = -1;
		int codAlmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
		rgvrequerimientos.DataSource = admreqalm.ListadoRequerimientos(codAlmacen, frmLogin.iCodSucursal, opcionFecha, dtpDesde.Value, dtpHasta.Value, 0, 1);
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void frmLIstadoReqAlmacen_Shown(object sender, EventArgs e)
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnVer = new System.Windows.Forms.Button();
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
		this.label1 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.btnEditar);
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.btnVer);
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
		this.groupBox1.Size = new System.Drawing.Size(1251, 111);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEditar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btnEditar.Location = new System.Drawing.Point(1073, 66);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(80, 39);
		this.btnEditar.TabIndex = 48;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Visible = false;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnVer.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnVer.Image = SIGEFA.Properties.Resources.buscar;
		this.btnVer.Location = new System.Drawing.Point(1159, 66);
		this.btnVer.Name = "btnVer";
		this.btnVer.Size = new System.Drawing.Size(80, 39);
		this.btnVer.TabIndex = 47;
		this.btnVer.Text = "Ver";
		this.btnVer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnVer.UseVisualStyleBackColor = true;
		this.btnVer.Click += new System.EventHandler(btnVer_Click);
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(53, 86);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 46;
		this.txtNombreProducto.Text = "---";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 86);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 45;
		this.label3.Text = "Prod.:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(12, 47);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(112, 13);
		this.label4.TabIndex = 43;
		this.label4.Text = "Busqueda x Producto:";
		this.txtCodprod.Location = new System.Drawing.Point(15, 63);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(171, 20);
		this.txtCodprod.TabIndex = 42;
		this.txtCodprod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Items.AddRange(new object[7] { "Pendiente", "Aprobado", "Facturado", "Entregado Parcial", "Entregado Total", "Anulado", "Todos" });
		this.comboBox1.Location = new System.Drawing.Point(1092, 16);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(121, 21);
		this.comboBox1.TabIndex = 41;
		this.comboBox1.Visible = false;
		this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(1019, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(67, 13);
		this.label2.TabIndex = 40;
		this.label2.Text = "Tipo  Fecha:";
		this.label2.Visible = false;
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.cambio;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.Location = new System.Drawing.Point(687, 10);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(118, 36);
		this.btnBusqueda.TabIndex = 39;
		this.btnBusqueda.Text = "Recargar Lista";
		this.btnBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(208, 20);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 36;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 20);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 35;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(65, 17);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 34;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(255, 16);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 33;
		this.groupBox2.Controls.Add(this.rgvrequerimientos);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 111);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1251, 372);
		this.groupBox2.TabIndex = 0;
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
		gridViewTextBoxColumn2.Width = 127;
		gridViewTextBoxColumn3.FieldName = "codAlmacenSolicitante";
		gridViewTextBoxColumn3.HeaderText = "codAlmacenSolicitante";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "colCodAlmacenSolicitante";
		gridViewTextBoxColumn4.FieldName = "descAlmacenSolicitante";
		gridViewTextBoxColumn4.HeaderText = "Almacen Solicitante";
		gridViewTextBoxColumn4.Name = "colDescAlmacenSolicitante";
		gridViewTextBoxColumn4.Width = 152;
		gridViewTextBoxColumn5.FieldName = "codAlmacenDespachador";
		gridViewTextBoxColumn5.HeaderText = "codAlmacenDespachador";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "colCodAlmacenDespachador";
		gridViewTextBoxColumn6.FieldName = "descAlmacenDespachador";
		gridViewTextBoxColumn6.HeaderText = "Almacen Despachador";
		gridViewTextBoxColumn6.Name = "colDescAlmacenDespachador";
		gridViewTextBoxColumn6.Width = 152;
		gridViewTextBoxColumn7.FieldName = "comentarioDespacho";
		gridViewTextBoxColumn7.HeaderText = "Comentario Despacho";
		gridViewTextBoxColumn7.Name = "colComentarioDespacho";
		gridViewTextBoxColumn7.Width = 209;
		gridViewTextBoxColumn8.FieldName = "usuario";
		gridViewTextBoxColumn8.HeaderText = "Usuario";
		gridViewTextBoxColumn8.Name = "colUsuario";
		gridViewTextBoxColumn8.Width = 136;
		gridViewTextBoxColumn9.FieldName = "fechaRegistro";
		gridViewTextBoxColumn9.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn9.Name = "colFechaRegistro";
		gridViewTextBoxColumn9.Width = 148;
		gridViewTextBoxColumn10.FieldName = "codEstado";
		gridViewTextBoxColumn10.HeaderText = "codEstado";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "colCodEstado";
		gridViewTextBoxColumn11.FieldName = "descEstado";
		gridViewTextBoxColumn11.HeaderText = "Estado";
		gridViewTextBoxColumn11.Name = "colDescEstado";
		gridViewTextBoxColumn11.Width = 158;
		gridViewTextBoxColumn12.FieldName = "descTipoReq";
		gridViewTextBoxColumn12.HeaderText = "Tipo Req.";
		gridViewTextBoxColumn12.Name = "colTipoReq";
		gridViewTextBoxColumn12.Width = 169;
		this.rgvrequerimientos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12);
		this.rgvrequerimientos.MasterTemplate.EnableFiltering = true;
		this.rgvrequerimientos.MasterTemplate.EnableGrouping = false;
		this.rgvrequerimientos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvrequerimientos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvrequerimientos.Name = "rgvrequerimientos";
		this.rgvrequerimientos.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.Size = new System.Drawing.Size(1245, 353);
		this.rgvrequerimientos.TabIndex = 0;
		this.rgvrequerimientos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvrequerimientos_CellClick);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(400, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(51, 13);
		this.label1.TabIndex = 60;
		this.label1.Text = "Almacen:";
		this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbAlmacenes.Location = new System.Drawing.Point(474, 16);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(182, 21);
		this.cmbAlmacenes.TabIndex = 59;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1251, 483);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmLIstadoReqAlmacen";
		this.Text = "Atencion Req Reposicion de Stock";
		base.Load += new System.EventHandler(frmLIstadoReqAlmacen_Load);
		base.Shown += new System.EventHandler(frmLIstadoReqAlmacen_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).EndInit();
		base.ResumeLayout(false);
	}
}
