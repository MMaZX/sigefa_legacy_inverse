using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Base.WinForm;
using SIGEFA.Data;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoReqPendienteSegunAnalisis : Form
{
	private Atriform objfrm;

	private bool flgloadcboanalisis = false;

	private DataSet dsAlmacen;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label7;

	private ComboBox cmbEstado;

	private Label label1;

	private ComboBox cmbAlmacenes;

	private Button btnEditar;

	private Button btnVer;

	private Label txtNombreProducto;

	private Label label3;

	private Label label4;

	private TextBox txtCodprod;

	private ComboBox cmbTipoFecha;

	private Label label2;

	private Button btnBusqueda;

	private GroupBox groupBox2;

	private RadGridView rgvrequerimientos;

	private ComboBox cbo_Analisis;

	private Label label9;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private ComboBox cboAlmacen;

	private Label label8;

	public frmListadoReqPendienteSegunAnalisis()
	{
		InitializeComponent();
	}

	private void frmListadoReqPendienteSegunAnalisis_Load(object sender, EventArgs e)
	{
		objfrm = new Atriform();
		objfrm = (Atriform)base.Tag;
		CargaAnalisis();
		flgloadcboanalisis = true;
		CargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		string sSelAlmacen = "";
		foreach (DataRow item in dsAlmacen.Tables[0].Rows)
		{
			if (item["Adicional1"].ToString().Equals(objfrm.codAlmacen.ToString()) && item["Adicional2"].ToString().Equals("A"))
			{
				sSelAlmacen = item["codigo"].ToString();
				break;
			}
		}
		if (sSelAlmacen.Trim().Length > 0)
		{
			cboAlmacen.SelectedValue = sSelAlmacen;
		}
	}

	private void CargaAnalisis()
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		DataSet ds = new DataSet();
		dBAccess.AddParameter("pcodigotabla", "002");
		ds = dBAccess.ExecuteDataSet("sp_get_tablas");
		cbo_Analisis.DataSource = ds.Tables[0];
		cbo_Analisis.DisplayMember = "DescTablaDetalle";
		cbo_Analisis.ValueMember = "codigo";
		string sSelEmpresa = "";
		foreach (DataRow item in ds.Tables[0].Rows)
		{
			if (item["Adicional1"].ToString().Equals(objfrm.codEmpresa.ToString()) && item["Adicional2"].ToString().Equals("E"))
			{
				sSelEmpresa = item["codigo"].ToString();
				break;
			}
		}
		if (sSelEmpresa.Trim().Length > 0)
		{
			cbo_Analisis.SelectedValue = sSelEmpresa;
		}
	}

	private void CargaAlmacenes(string codigodtabla)
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		dsAlmacen = new DataSet();
		dBAccess.AddParameter("pparentcodigo", codigodtabla);
		dsAlmacen = dBAccess.ExecuteDataSet("sp_get_tablasparents");
		cboAlmacen.DataSource = dsAlmacen.Tables[0];
		cboAlmacen.DisplayMember = "DescTablaDetalle";
		cboAlmacen.ValueMember = "codigo";
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn27 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn28 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn29 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn30 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn31 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn32 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn33 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn34 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn35 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn36 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn37 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn38 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn39 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn40 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbo_Analisis = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.cmbTipoFecha = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvrequerimientos = new Telerik.WinControls.UI.RadGridView();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnVer = new System.Windows.Forms.Button();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.cboAlmacen = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cboAlmacen);
		this.groupBox1.Controls.Add(this.cbo_Analisis);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label9);
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
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1309, 127);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.cbo_Analisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbo_Analisis.FormattingEnabled = true;
		this.cbo_Analisis.Location = new System.Drawing.Point(304, 81);
		this.cbo_Analisis.Name = "cbo_Analisis";
		this.cbo_Analisis.Size = new System.Drawing.Size(183, 21);
		this.cbo_Analisis.TabIndex = 67;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(247, 84);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(59, 13);
		this.label9.TabIndex = 66;
		this.label9.Text = "Análisis : ";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(890, 61);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(43, 13);
		this.label7.TabIndex = 60;
		this.label7.Text = "Estado:";
		this.label7.Visible = false;
		this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[4] { "Todos", "Pendientes", "Atendidos Totalmente", "Anulados" });
		this.cmbEstado.Location = new System.Drawing.Point(939, 57);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(182, 21);
		this.cmbEstado.TabIndex = 59;
		this.cmbEstado.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(815, 33);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(118, 13);
		this.label1.TabIndex = 58;
		this.label1.Text = "Almacen Despachador:";
		this.label1.Visible = false;
		this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbAlmacenes.Location = new System.Drawing.Point(939, 30);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(182, 21);
		this.cmbAlmacenes.TabIndex = 57;
		this.cmbAlmacenes.Visible = false;
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
		this.groupBox2.Controls.Add(this.rgvrequerimientos);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 127);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1309, 323);
		this.groupBox2.TabIndex = 4;
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
		gridViewTextBoxColumn21.FieldName = "fechaRegistro";
		gridViewTextBoxColumn21.HeaderText = "Fecha Requerimiento";
		gridViewTextBoxColumn21.Name = "colFechaRegistro";
		gridViewTextBoxColumn21.Width = 121;
		gridViewTextBoxColumn21.WrapText = true;
		gridViewTextBoxColumn22.FieldName = "fechaRequerimiento";
		gridViewTextBoxColumn22.HeaderText = "Fecha Req Ocultar";
		gridViewTextBoxColumn22.IsVisible = false;
		gridViewTextBoxColumn22.Name = "colFechaRequerimiento";
		gridViewTextBoxColumn22.Width = 132;
		gridViewTextBoxColumn22.WrapText = true;
		gridViewTextBoxColumn23.FieldName = "codReqAlmacen";
		gridViewTextBoxColumn23.HeaderText = "codReqAlmacen";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "colCodReqAlmacen";
		gridViewTextBoxColumn24.FieldName = "docReqAlmacen";
		gridViewTextBoxColumn24.HeaderText = "Requerimiento";
		gridViewTextBoxColumn24.Name = "colDocReqAlmacen";
		gridViewTextBoxColumn24.Width = 118;
		gridViewTextBoxColumn24.WrapText = true;
		gridViewTextBoxColumn25.FieldName = "codEstado";
		gridViewTextBoxColumn25.HeaderText = "codEstado";
		gridViewTextBoxColumn25.IsVisible = false;
		gridViewTextBoxColumn25.Name = "colCodEstado";
		gridViewTextBoxColumn26.FieldName = "descEstado";
		gridViewTextBoxColumn26.HeaderText = "Estado";
		gridViewTextBoxColumn26.Name = "colDescEstado";
		gridViewTextBoxColumn26.Width = 95;
		gridViewTextBoxColumn27.FieldName = "descTipoReq";
		gridViewTextBoxColumn27.HeaderText = "Tipo Req.";
		gridViewTextBoxColumn27.Name = "colTipoReq";
		gridViewTextBoxColumn27.Width = 110;
		gridViewTextBoxColumn27.WrapText = true;
		gridViewTextBoxColumn28.FieldName = "codAlmacenSolicitante";
		gridViewTextBoxColumn28.HeaderText = "codAlmacenSolicitante";
		gridViewTextBoxColumn28.IsVisible = false;
		gridViewTextBoxColumn28.Name = "colCodAlmacenSolicitante";
		gridViewTextBoxColumn29.FieldName = "descAlmacenSolicitante";
		gridViewTextBoxColumn29.HeaderText = "Almacen Solicitante";
		gridViewTextBoxColumn29.Name = "colDescAlmacenSolicitante";
		gridViewTextBoxColumn29.Width = 118;
		gridViewTextBoxColumn29.WrapText = true;
		gridViewTextBoxColumn30.FieldName = "codAlmacenDespachador";
		gridViewTextBoxColumn30.HeaderText = "codAlmacenDespachador";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "colCodAlmacenDespachador";
		gridViewTextBoxColumn31.FieldName = "descAlmacenDespachador";
		gridViewTextBoxColumn31.HeaderText = "Almacen Despachador";
		gridViewTextBoxColumn31.Name = "colDescAlmacenDespachador";
		gridViewTextBoxColumn31.Width = 135;
		gridViewTextBoxColumn31.WrapText = true;
		gridViewTextBoxColumn32.FieldName = "codDespacho";
		gridViewTextBoxColumn32.HeaderText = "CodDespacho";
		gridViewTextBoxColumn32.IsVisible = false;
		gridViewTextBoxColumn32.Name = "colCodDespacho";
		gridViewTextBoxColumn33.FieldName = "refDespacho";
		gridViewTextBoxColumn33.HeaderText = "Despacho Relac.";
		gridViewTextBoxColumn33.Name = "colRefDespacho";
		gridViewTextBoxColumn33.Width = 121;
		gridViewTextBoxColumn34.FieldName = "estadoDespacho";
		gridViewTextBoxColumn34.HeaderText = "Estado Despacho";
		gridViewTextBoxColumn34.Name = "colEstadoDespacho";
		gridViewTextBoxColumn34.Width = 138;
		gridViewTextBoxColumn35.FieldName = "observacion";
		gridViewTextBoxColumn35.HeaderText = "Observacion";
		gridViewTextBoxColumn35.IsVisible = false;
		gridViewTextBoxColumn35.Name = "colObservacion";
		gridViewTextBoxColumn35.Width = 118;
		gridViewTextBoxColumn36.FieldName = "comentarioDespacho";
		gridViewTextBoxColumn36.HeaderText = "Comentario Despacho";
		gridViewTextBoxColumn36.IsVisible = false;
		gridViewTextBoxColumn36.Name = "colComentarioDespacho";
		gridViewTextBoxColumn36.Width = 188;
		gridViewTextBoxColumn37.FieldName = "fechaDespacho";
		gridViewTextBoxColumn37.HeaderText = "Fecha Despacho";
		gridViewTextBoxColumn37.IsVisible = false;
		gridViewTextBoxColumn37.Name = "colFechaDespacho";
		gridViewTextBoxColumn37.Width = 136;
		gridViewTextBoxColumn37.WrapText = true;
		gridViewTextBoxColumn38.FieldName = "usuario";
		gridViewTextBoxColumn38.HeaderText = "Usuario";
		gridViewTextBoxColumn38.Name = "colUsuario";
		gridViewTextBoxColumn38.Width = 111;
		gridViewTextBoxColumn39.FieldName = "FechaPrimeraEntrega";
		gridViewTextBoxColumn39.HeaderText = "Primera Entrega";
		gridViewTextBoxColumn39.Name = "colFechaPriEntrega";
		gridViewTextBoxColumn39.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn39.Width = 116;
		gridViewTextBoxColumn40.FieldName = "FechaUltimaEntrega";
		gridViewTextBoxColumn40.HeaderText = "Ultima Entrega";
		gridViewTextBoxColumn40.Name = "colFechaUltimaEntrega";
		gridViewTextBoxColumn40.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn40.Width = 118;
		this.rgvrequerimientos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32, gridViewTextBoxColumn33, gridViewTextBoxColumn34, gridViewTextBoxColumn35, gridViewTextBoxColumn36, gridViewTextBoxColumn37, gridViewTextBoxColumn38, gridViewTextBoxColumn39, gridViewTextBoxColumn40);
		this.rgvrequerimientos.MasterTemplate.EnableFiltering = true;
		this.rgvrequerimientos.MasterTemplate.EnableGrouping = false;
		this.rgvrequerimientos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvrequerimientos.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvrequerimientos.Name = "rgvrequerimientos";
		this.rgvrequerimientos.ShowHeaderCellButtons = true;
		this.rgvrequerimientos.Size = new System.Drawing.Size(1303, 304);
		this.rgvrequerimientos.TabIndex = 0;
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEditar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btnEditar.Location = new System.Drawing.Point(1131, 82);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(80, 39);
		this.btnEditar.TabIndex = 48;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Visible = false;
		this.btnVer.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnVer.Image = SIGEFA.Properties.Resources.buscar;
		this.btnVer.Location = new System.Drawing.Point(1217, 82);
		this.btnVer.Name = "btnVer";
		this.btnVer.Size = new System.Drawing.Size(80, 39);
		this.btnVer.TabIndex = 47;
		this.btnVer.Text = "Ver";
		this.btnVer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnVer.UseVisualStyleBackColor = true;
		this.btnVer.Visible = false;
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
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(493, 34);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 33;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(303, 35);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 34;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(250, 38);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 35;
		this.label5.Text = "Desde :";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(446, 38);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 36;
		this.label6.Text = "Hasta :";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(508, 84);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 65;
		this.label8.Text = "Almacén: ";
		this.cboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboAlmacen.FormattingEnabled = true;
		this.cboAlmacen.Location = new System.Drawing.Point(568, 81);
		this.cboAlmacen.Name = "cboAlmacen";
		this.cboAlmacen.Size = new System.Drawing.Size(183, 21);
		this.cboAlmacen.TabIndex = 68;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1309, 450);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Name = "frmListadoReqPendienteSegunAnalisis";
		this.Text = "Listado Requerimientos Pendiente Segun Analisis";
		base.Load += new System.EventHandler(frmListadoReqPendienteSegunAnalisis_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvrequerimientos).EndInit();
		base.ResumeLayout(false);
	}
}
