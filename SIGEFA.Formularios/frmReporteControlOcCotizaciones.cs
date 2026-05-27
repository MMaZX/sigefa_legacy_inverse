using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Data;
using SIGEFA.Properties;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmReporteControlOcCotizaciones : Form
{
	private bool flgloadcboanalisis = false;

	private IContainer components = null;

	private GroupBox groupBox1;

	private ComboBox cbo_Analisis;

	private Label label9;

	private ComboBox cmbAlmacen;

	private Label label8;

	private Button btnReporte;

	private Button btnBusqueda;

	private Label label1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private RadGridView rgvDetalle;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	public frmReporteControlOcCotizaciones()
	{
		InitializeComponent();
	}

	private void frmReporteControlOcCotizaciones_Load(object sender, EventArgs e)
	{
		CargaAnalisis();
		flgloadcboanalisis = true;
		cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
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
		string sSelEmpresa = "002001";
		foreach (DataRow fila in ds.Tables[0].Rows)
		{
			object valor = fila.Field<object>("Adicional1");
			valor = ((valor == null) ? "" : valor);
			string almacen = frmLogin.sAlmacen.Substring(8, 3);
			if (valor.ToString() == almacen)
			{
				sSelEmpresa = fila.Field<object>("codigo").ToString();
				break;
			}
		}
		if (sSelEmpresa.Trim().Length > 0)
		{
			cbo_Analisis.SelectedValue = sSelEmpresa;
		}
	}

	private void cargaAlmacenes(string codigodtabla)
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		DataSet dsAlmacen = new DataSet();
		dBAccess.AddParameter("pparentcodigo", codigodtabla);
		dsAlmacen = dBAccess.ExecuteDataSet("sp_get_tablasparents");
		cmbAlmacen.DataSource = dsAlmacen.Tables[0];
		cmbAlmacen.DisplayMember = "DescTablaDetalle";
		cmbAlmacen.ValueMember = "codigo";
	}

	private void cbo_Analisis_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (flgloadcboanalisis)
		{
			cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		try
		{
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			dBAccess.AddParameter("codalma", cmbAlmacen.SelectedValue);
			dBAccess.AddParameter("fechaini", dtpDesde.Value);
			dBAccess.AddParameter("fechafin", dtpHasta.Value);
			ds = dBAccess.ExecuteDataSet("AnalisisDetalladoOcCotizaciones");
			rgvDetalle.DataSource = ds.Tables[0];
			if (rgvDetalle.Rows.Count <= 0)
			{
				MessageBox.Show("No se encontraron registros para mostrar.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error : " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbo_Analisis = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.rgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox1.Controls.Add(this.cbo_Analisis);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1370, 88);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.cbo_Analisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbo_Analisis.FormattingEnabled = true;
		this.cbo_Analisis.Location = new System.Drawing.Point(427, 12);
		this.cbo_Analisis.Name = "cbo_Analisis";
		this.cbo_Analisis.Size = new System.Drawing.Size(183, 21);
		this.cbo_Analisis.TabIndex = 93;
		this.cbo_Analisis.SelectedIndexChanged += new System.EventHandler(cbo_Analisis_SelectedIndexChanged);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label9.Location = new System.Drawing.Point(372, 16);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(59, 13);
		this.label9.TabIndex = 92;
		this.label9.Text = "Análisis : ";
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(428, 45);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(183, 21);
		this.cmbAlmacen.TabIndex = 91;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(369, 48);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 90;
		this.label8.Text = "Almacén: ";
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnReporte.Location = new System.Drawing.Point(1275, 24);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(86, 36);
		this.btnReporte.TabIndex = 83;
		this.btnReporte.Text = "Exportar";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnBusqueda.Location = new System.Drawing.Point(1181, 24);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(88, 36);
		this.btnBusqueda.TabIndex = 80;
		this.btnBusqueda.Text = "Consultar";
		this.btnBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(6, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(115, 13);
		this.label1.TabIndex = 79;
		this.label1.Text = "Fecha de Registro:";
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(186, 35);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 78;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(6, 35);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 77;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(59, 32);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 76;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(236, 32);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 75;
		this.rgvDetalle.AllowDrop = true;
		this.rgvDetalle.AutoScroll = true;
		this.rgvDetalle.AutoSizeRows = true;
		this.rgvDetalle.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.rgvDetalle.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDetalle.Font = new System.Drawing.Font("Segoe UI", 8.25f);
		this.rgvDetalle.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvDetalle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvDetalle.Location = new System.Drawing.Point(0, 88);
		this.rgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.rgvDetalle.MasterTemplate.AllowColumnReorder = false;
		this.rgvDetalle.MasterTemplate.AllowDeleteRow = false;
		this.rgvDetalle.MasterTemplate.AllowEditRow = false;
		gridViewTextBoxColumn1.DataType = typeof(int);
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn1.FieldName = "numero_item";
		gridViewTextBoxColumn1.HeaderText = "N°";
		gridViewTextBoxColumn1.Name = "numero_item";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 68;
		gridViewTextBoxColumn2.FieldName = "fecha_oc";
		gridViewTextBoxColumn2.HeaderText = "FECHA OC";
		gridViewTextBoxColumn2.Name = "fecha_oc";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 120;
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "almacen";
		gridViewTextBoxColumn3.HeaderText = "ALMACÉN";
		gridViewTextBoxColumn3.Name = "almacen";
		gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn3.Width = 130;
		gridViewTextBoxColumn4.FieldName = "num_oc";
		gridViewTextBoxColumn4.HeaderText = "N° OC CLIENTE";
		gridViewTextBoxColumn4.Name = "num_oc";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 120;
		gridViewTextBoxColumn5.EnableExpressionEditor = false;
		gridViewTextBoxColumn5.FieldName = "cliente";
		gridViewTextBoxColumn5.HeaderText = "CLIENTE";
		gridViewTextBoxColumn5.Name = "cliente";
		gridViewTextBoxColumn5.Width = 250;
		gridViewTextBoxColumn6.FieldName = "categoria";
		gridViewTextBoxColumn6.HeaderText = "CATEGORÍA";
		gridViewTextBoxColumn6.Name = "categoria";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 120;
		gridViewTextBoxColumn7.FieldName = "fechacotizacion";
		gridViewTextBoxColumn7.HeaderText = "FECHA DE COT";
		gridViewTextBoxColumn7.Name = "fechacotizacion";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 150;
		gridViewTextBoxColumn8.EnableExpressionEditor = false;
		gridViewTextBoxColumn8.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn8.FieldName = "num_cotizacion";
		gridViewTextBoxColumn8.HeaderText = "N° COT - FINAL";
		gridViewTextBoxColumn8.Name = "num_cotizacion";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 120;
		gridViewTextBoxColumn8.WrapText = true;
		gridViewTextBoxColumn9.DataType = typeof(int);
		gridViewTextBoxColumn9.EnableExpressionEditor = false;
		gridViewTextBoxColumn9.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn9.FieldName = "fecha_comprobante";
		gridViewTextBoxColumn9.HeaderText = "FECHA COMPROBANTE";
		gridViewTextBoxColumn9.Name = "fecha_comprobante";
		gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
		gridViewTextBoxColumn9.Width = 150;
		gridViewTextBoxColumn10.FieldName = "num_comprobante";
		gridViewTextBoxColumn10.HeaderText = "N° COMPROBANTE";
		gridViewTextBoxColumn10.Name = "num_comprobante";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 160;
		gridViewTextBoxColumn11.EnableExpressionEditor = false;
		gridViewTextBoxColumn11.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.GeneralDate;
		gridViewTextBoxColumn11.FieldName = "num_despacho";
		gridViewTextBoxColumn11.HeaderText = "N° DESPACHO";
		gridViewTextBoxColumn11.Name = "num_despacho";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 150;
		gridViewTextBoxColumn12.FieldName = "fechaguia";
		gridViewTextBoxColumn12.HeaderText = "FECHA DE GUÍA DE REMISIÓN";
		gridViewTextBoxColumn12.Name = "fechaguia";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 120;
		gridViewTextBoxColumn13.FieldName = "num_guia";
		gridViewTextBoxColumn13.HeaderText = "N° GR";
		gridViewTextBoxColumn13.Name = "num_guia";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 100;
		gridViewTextBoxColumn14.EnableExpressionEditor = false;
		gridViewTextBoxColumn14.FieldName = "cantidad";
		gridViewTextBoxColumn14.HeaderText = "CANTIDAD SOLICITADA";
		gridViewTextBoxColumn14.Name = "cantidad";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 150;
		gridViewTextBoxColumn15.EnableExpressionEditor = false;
		gridViewTextBoxColumn15.FieldName = "fecha_despacho";
		gridViewTextBoxColumn15.HeaderText = "FECHA DESPACHO";
		gridViewTextBoxColumn15.Name = "fecha_despacho";
		gridViewTextBoxColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn15.Width = 150;
		gridViewTextBoxColumn15.WrapText = true;
		gridViewTextBoxColumn16.EnableExpressionEditor = false;
		gridViewTextBoxColumn16.FieldName = "producto";
		gridViewTextBoxColumn16.HeaderText = "DESCRIPCIÓN";
		gridViewTextBoxColumn16.Name = "producto";
		gridViewTextBoxColumn16.Width = 300;
		gridViewTextBoxColumn17.EnableExpressionEditor = false;
		gridViewTextBoxColumn17.FieldName = "cantidad_atendida";
		gridViewTextBoxColumn17.HeaderText = "CANTIDAD ATENDIDA";
		gridViewTextBoxColumn17.Name = "cantidad_atendida";
		gridViewTextBoxColumn17.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn17.Width = 200;
		gridViewTextBoxColumn18.FieldName = "cantidad_noatendida";
		gridViewTextBoxColumn18.HeaderText = "CANTIDAD PENDIENTE";
		gridViewTextBoxColumn18.Name = "cantidad_noatendida";
		gridViewTextBoxColumn18.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn18.Width = 200;
		gridViewTextBoxColumn19.FieldName = "preciounitario";
		gridViewTextBoxColumn19.HeaderText = "P. UNITARIO";
		gridViewTextBoxColumn19.Name = "preciounitario";
		gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn19.Width = 120;
		gridViewTextBoxColumn20.FieldName = "subtotal_atendido";
		gridViewTextBoxColumn20.HeaderText = "SUBTOTAL ATENDIDO";
		gridViewTextBoxColumn20.Name = "subtotal_atendido";
		gridViewTextBoxColumn20.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn20.Width = 150;
		gridViewTextBoxColumn21.FieldName = "subtotal_pendiente";
		gridViewTextBoxColumn21.HeaderText = "SUBTOTAL PENDIENTE";
		gridViewTextBoxColumn21.Name = "subtotal_pendiente";
		gridViewTextBoxColumn21.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn21.Width = 150;
		gridViewTextBoxColumn22.FieldName = "vendedor";
		gridViewTextBoxColumn22.HeaderText = "VENDEDOR";
		gridViewTextBoxColumn22.Name = "vendedor";
		gridViewTextBoxColumn22.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn22.Width = 120;
		gridViewTextBoxColumn23.FieldName = "familia";
		gridViewTextBoxColumn23.HeaderText = "FAMILIA";
		gridViewTextBoxColumn23.Name = "familia";
		gridViewTextBoxColumn23.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn23.Width = 100;
		gridViewTextBoxColumn24.FieldName = "linea";
		gridViewTextBoxColumn24.HeaderText = "LINEA";
		gridViewTextBoxColumn24.Name = "linea";
		gridViewTextBoxColumn24.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn24.Width = 100;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.EnableGrouping = false;
		this.rgvDetalle.MasterTemplate.MultiSelect = true;
		this.rgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvDetalle.ShowHeaderCellButtons = true;
		this.rgvDetalle.Size = new System.Drawing.Size(1370, 541);
		this.rgvDetalle.TabIndex = 11;
		this.rgvDetalle.ThemeName = "TelerikMetroTouch";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1370, 629);
		base.Controls.Add(this.rgvDetalle);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmReporteControlOcCotizaciones";
		this.Text = "frmReporteControlOcCotizaciones";
		base.Load += new System.EventHandler(frmReporteControlOcCotizaciones_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
