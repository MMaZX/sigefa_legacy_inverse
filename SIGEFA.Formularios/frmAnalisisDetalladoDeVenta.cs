using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Properties;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmAnalisisDetalladoDeVenta : Form
{
	private bool flgloadcboanalisis = false;

	private clsAdmFacturaVenta admventa = new clsAdmFacturaVenta();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private RadGridView rgvDetalle;

	private ComboBox cbo_Analisis;

	private Label label9;

	private Button btnReporte;

	private ComboBox cmbAlmacen;

	private Label label8;

	private Button btnBusqueda;

	private Label label1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	public frmAnalisisDetalladoDeVenta()
	{
		InitializeComponent();
	}

	private void frmAnalisisDetalladoDeVenta_Load(object sender, EventArgs e)
	{
		CargaAnalisis();
		flgloadcboanalisis = true;
		cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		cargaLista();
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

	private void cargaLista()
	{
		int usuario = frmLogin.iNivelUser;
		string buscar = ((usuario == 1) ? "ReporteAnalisisDetalladoVenta" : "ReporteAnalisisDetalladoVentaLimitado");
		DataTable data = admventa.ReporteAnalisisDetalladoVenta(buscar, dtpDesde.Value, dtpHasta.Value, cbo_Analisis.SelectedValue.ToString(), cmbAlmacen.SelectedValue.ToString());
		rgvDetalle.DataSource = data;
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		cargaLista();
		string sms = "";
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(rgvDetalle)
			{
				HiddenColumnOption = HiddenOption.DoNotExport,
				HiddenRowOption = HiddenOption.DoNotExport,
				ExportVisualSettings = true,
				SummariesExportOption = SummariesOption.DoNotExport
			};
			spreadStreamExport.ExportVisualSettings = true;
			spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
			try
			{
				string cadenaGuardado = obtenerRutaParaGuardar("analisisDetalladoDeVentas.xlsx");
				if (cadenaGuardado != null)
				{
					spreadStreamExport.RunExport(cadenaGuardado, new SpreadStreamExportRenderer());
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Reporte Analisis Detallado de Ventas");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, "Error");
		}
	}

	private string obtenerRutaParaGuardar(string namefile)
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "Guardando Archivo Excel de Exportacion";
			sfd.FileName = namefile;
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Analisis Detallado de Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.ConditionalFormattingObject conditionalFormattingObject1 = new Telerik.WinControls.UI.ConditionalFormattingObject();
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn41 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn42 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn43 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn44 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbo_Analisis = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.btnReporte = new System.Windows.Forms.Button();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cbo_Analisis);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1110, 71);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.cbo_Analisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbo_Analisis.FormattingEnabled = true;
		this.cbo_Analisis.Location = new System.Drawing.Point(418, 31);
		this.cbo_Analisis.Name = "cbo_Analisis";
		this.cbo_Analisis.Size = new System.Drawing.Size(183, 21);
		this.cbo_Analisis.TabIndex = 86;
		this.cbo_Analisis.SelectedIndexChanged += new System.EventHandler(cbo_Analisis_SelectedIndexChanged);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label9.Location = new System.Drawing.Point(363, 35);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(59, 13);
		this.label9.TabIndex = 85;
		this.label9.Text = "Análisis : ";
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnReporte.Location = new System.Drawing.Point(888, 19);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(86, 36);
		this.btnReporte.TabIndex = 83;
		this.btnReporte.Text = "Exportar";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(666, 31);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(183, 21);
		this.cmbAlmacen.TabIndex = 82;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(607, 34);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 81;
		this.label8.Text = "Almacén: ";
		this.btnBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.cambio;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnBusqueda.Location = new System.Drawing.Point(980, 19);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(118, 36);
		this.btnBusqueda.TabIndex = 80;
		this.btnBusqueda.Text = "Recargar Lista";
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
		this.label6.BackColor = System.Drawing.SystemColors.Control;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(186, 35);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 78;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.SystemColors.Control;
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
		this.groupBox2.Controls.Add(this.rgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 71);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1110, 379);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.rgvDetalle.AllowDrop = true;
		this.rgvDetalle.AutoScroll = true;
		this.rgvDetalle.AutoSizeRows = true;
		this.rgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.rgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.rgvDetalle.MasterTemplate.AllowColumnReorder = false;
		this.rgvDetalle.MasterTemplate.AllowDeleteRow = false;
		this.rgvDetalle.MasterTemplate.AllowEditRow = false;
		gridViewTextBoxColumn1.FieldName = "nroItem";
		gridViewTextBoxColumn1.HeaderText = "Nro. Item";
		gridViewTextBoxColumn1.Name = "colNroItem";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 68;
		gridViewTextBoxColumn2.FieldName = "_local";
		gridViewTextBoxColumn2.HeaderText = "Local";
		gridViewTextBoxColumn2.Name = "colLocal";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 75;
		gridViewTextBoxColumn3.FieldName = "almacen";
		gridViewTextBoxColumn3.HeaderText = "Almacen";
		gridViewTextBoxColumn3.Name = "colAlmacen";
		gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn3.Width = 75;
		gridViewTextBoxColumn4.FieldName = "razonsocial";
		gridViewTextBoxColumn4.HeaderText = "Razon Social";
		gridViewTextBoxColumn4.Name = "colRazonSocial";
		gridViewTextBoxColumn4.Width = 149;
		gridViewTextBoxColumn4.WrapText = true;
		gridViewTextBoxColumn5.FieldName = "familia";
		gridViewTextBoxColumn5.HeaderText = "Familia";
		gridViewTextBoxColumn5.Name = "colFamilia";
		gridViewTextBoxColumn5.Width = 152;
		gridViewTextBoxColumn6.FieldName = "linea";
		gridViewTextBoxColumn6.HeaderText = "Linea";
		gridViewTextBoxColumn6.Name = "linea";
		gridViewTextBoxColumn6.Width = 100;
		gridViewTextBoxColumn7.FieldName = "vendedor";
		gridViewTextBoxColumn7.HeaderText = "Vendedor";
		gridViewTextBoxColumn7.Name = "colVendedor";
		gridViewTextBoxColumn7.Width = 100;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "canal";
		gridViewTextBoxColumn8.HeaderText = "Canal";
		gridViewTextBoxColumn8.Name = "colCanal";
		gridViewTextBoxColumn8.Width = 75;
		gridViewTextBoxColumn9.FieldName = "tecnico";
		gridViewTextBoxColumn9.HeaderText = "Tecnico";
		gridViewTextBoxColumn9.Name = "colTecnico";
		gridViewTextBoxColumn9.Width = 75;
		gridViewTextBoxColumn10.FieldName = "zona";
		gridViewTextBoxColumn10.HeaderText = "Zona";
		gridViewTextBoxColumn10.Name = "colZona";
		gridViewTextBoxColumn10.Width = 75;
		gridViewTextBoxColumn11.FieldName = "anio";
		gridViewTextBoxColumn11.HeaderText = "Año";
		gridViewTextBoxColumn11.Name = "colAnio";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 75;
		gridViewTextBoxColumn12.FieldName = "mes";
		gridViewTextBoxColumn12.HeaderText = "Mes";
		gridViewTextBoxColumn12.Name = "colMes";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 75;
		gridViewTextBoxColumn13.FieldName = "nro_semana";
		gridViewTextBoxColumn13.HeaderText = "Semana";
		gridViewTextBoxColumn13.Name = "colNroSemana";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 75;
		gridViewTextBoxColumn14.FieldName = "dia";
		gridViewTextBoxColumn14.HeaderText = "Dia";
		gridViewTextBoxColumn14.Name = "colDia";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 75;
		gridViewTextBoxColumn15.FieldName = "fecha";
		gridViewTextBoxColumn15.HeaderText = "Fecha";
		gridViewTextBoxColumn15.Name = "colFecha";
		gridViewTextBoxColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn15.Width = 75;
		gridViewTextBoxColumn16.FieldName = "documento";
		gridViewTextBoxColumn16.HeaderText = "Doc";
		gridViewTextBoxColumn16.Name = "colDocumento";
		gridViewTextBoxColumn16.Width = 110;
		gridViewTextBoxColumn17.FieldName = "nro_documento";
		gridViewTextBoxColumn17.HeaderText = "Nº Doc";
		gridViewTextBoxColumn17.Name = "colNroDocumento";
		gridViewTextBoxColumn17.Width = 91;
		gridViewTextBoxColumn18.FieldName = "doc_referencia";
		gridViewTextBoxColumn18.HeaderText = "Doc Referencia";
		gridViewTextBoxColumn18.Name = "colDocReferencia";
		gridViewTextBoxColumn18.Width = 96;
		gridViewTextBoxColumn19.FieldName = "cliente";
		gridViewTextBoxColumn19.HeaderText = "Cliente";
		gridViewTextBoxColumn19.Name = "colCliente";
		gridViewTextBoxColumn19.Width = 241;
		gridViewTextBoxColumn19.WrapText = true;
		gridViewTextBoxColumn20.FieldName = "formapago";
		gridViewTextBoxColumn20.HeaderText = "Forma de Pago";
		gridViewTextBoxColumn20.Name = "colFormaPago";
		gridViewTextBoxColumn20.Width = 97;
		gridViewTextBoxColumn21.DataType = typeof(int);
		gridViewTextBoxColumn21.ExcelExportFormatString = "0";
		gridViewTextBoxColumn21.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn21.FieldName = "referencia";
		gridViewTextBoxColumn21.HeaderText = "Referencia";
		gridViewTextBoxColumn21.Name = "colReferencia";
		gridViewTextBoxColumn21.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn21.Width = 73;
		gridViewTextBoxColumn22.ExcelExportFormatString = "0.00##";
		gridViewTextBoxColumn22.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn22.FieldName = "cantidad";
		gridViewTextBoxColumn22.HeaderText = "Cant.";
		gridViewTextBoxColumn22.Name = "colCantidad";
		gridViewTextBoxColumn22.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn22.Width = 64;
		gridViewTextBoxColumn23.FieldName = "unidad";
		gridViewTextBoxColumn23.HeaderText = "Unidad";
		gridViewTextBoxColumn23.Name = "colUnidad";
		gridViewTextBoxColumn23.Width = 75;
		gridViewTextBoxColumn24.FieldName = "descripcion";
		gridViewTextBoxColumn24.HeaderText = "Descripcion";
		gridViewTextBoxColumn24.Name = "colDescripcion";
		gridViewTextBoxColumn24.Width = 329;
		gridViewTextBoxColumn24.WrapText = true;
		conditionalFormattingObject1.ApplyToRow = true;
		conditionalFormattingObject1.CellBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
		conditionalFormattingObject1.CellForeColor = System.Drawing.Color.Red;
		conditionalFormattingObject1.ConditionType = Telerik.WinControls.UI.ConditionTypes.Less;
		conditionalFormattingObject1.Name = "PUNegativo";
		conditionalFormattingObject1.RowBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
		conditionalFormattingObject1.RowForeColor = System.Drawing.Color.Red;
		conditionalFormattingObject1.TValue1 = "0";
		gridViewTextBoxColumn25.ConditionalFormattingObjectList.Add(conditionalFormattingObject1);
		gridViewTextBoxColumn25.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn25.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn25.FieldName = "preciounitario";
		gridViewTextBoxColumn25.HeaderText = "P.U.";
		gridViewTextBoxColumn25.Name = "colPrecioUnitario";
		gridViewTextBoxColumn25.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn25.Width = 64;
		gridViewTextBoxColumn26.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn26.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn26.FieldName = "subtotal";
		gridViewTextBoxColumn26.HeaderText = "Sub Total";
		gridViewTextBoxColumn26.Name = "colSubTotal";
		gridViewTextBoxColumn26.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn26.Width = 68;
		gridViewTextBoxColumn27.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn27.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn27.FieldName = "preciocompra";
		gridViewTextBoxColumn27.HeaderText = "C.U. Producto";
		gridViewTextBoxColumn27.Name = "colPrecioCompra";
		gridViewTextBoxColumn27.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn27.Width = 90;
		gridViewTextBoxColumn28.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn28.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn28.FieldName = "flete";
		gridViewTextBoxColumn28.HeaderText = "C.U. Flete";
		gridViewTextBoxColumn28.Name = "colFlete";
		gridViewTextBoxColumn28.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn28.Width = 68;
		gridViewTextBoxColumn29.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn29.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn29.FieldName = "desestiva";
		gridViewTextBoxColumn29.HeaderText = "C.U. Desestiva";
		gridViewTextBoxColumn29.Name = "colDesestiva";
		gridViewTextBoxColumn29.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn29.Width = 91;
		gridViewTextBoxColumn30.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn30.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn30.FieldName = "costo_unitario_total";
		gridViewTextBoxColumn30.HeaderText = "Costo Unitario Total";
		gridViewTextBoxColumn30.Name = "colCostoUnitarioTotal";
		gridViewTextBoxColumn30.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn30.Width = 121;
		gridViewTextBoxColumn31.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn31.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn31.FieldName = "costo_subtotal";
		gridViewTextBoxColumn31.HeaderText = "Costo Subtotal";
		gridViewTextBoxColumn31.Name = "colCostoSubtotal";
		gridViewTextBoxColumn31.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn31.Width = 95;
		gridViewTextBoxColumn32.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn32.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn32.FieldName = "margen_unitario";
		gridViewTextBoxColumn32.HeaderText = "Margen Unitario";
		gridViewTextBoxColumn32.Name = "colMargenUnitario";
		gridViewTextBoxColumn32.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn32.Width = 103;
		gridViewTextBoxColumn33.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn33.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn33.FieldName = "margen_unitario_sin_igv";
		gridViewTextBoxColumn33.HeaderText = "Margen Unitario s/ IGV";
		gridViewTextBoxColumn33.Name = "colMargenUnitarioSinIgv";
		gridViewTextBoxColumn33.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn33.Width = 136;
		gridViewTextBoxColumn34.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn34.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn34.FieldName = "margen_total";
		gridViewTextBoxColumn34.HeaderText = "Margen Total";
		gridViewTextBoxColumn34.Name = "colMargenTotal";
		gridViewTextBoxColumn34.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn34.Width = 88;
		gridViewTextBoxColumn35.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn35.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn35.FieldName = "margen_total_sin_igv";
		gridViewTextBoxColumn35.HeaderText = "Margen Total s/ IGV";
		gridViewTextBoxColumn35.Name = "colMargenTotalSinIgv";
		gridViewTextBoxColumn35.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn35.Width = 121;
		gridViewTextBoxColumn36.DataType = typeof(double);
		gridViewTextBoxColumn36.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Percent;
		gridViewTextBoxColumn36.FieldName = "porcentaje_margen";
		gridViewTextBoxColumn36.FormatString = "{0:#0.00%}";
		gridViewTextBoxColumn36.HeaderText = "% Margen Producto";
		gridViewTextBoxColumn36.Name = "colPorcentajeMargen";
		gridViewTextBoxColumn36.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn36.Width = 121;
		gridViewTextBoxColumn37.DataType = typeof(double);
		gridViewTextBoxColumn37.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Percent;
		gridViewTextBoxColumn37.FieldName = "porcentaje_flete";
		gridViewTextBoxColumn37.FormatString = "{0:#0.00%}";
		gridViewTextBoxColumn37.HeaderText = "% Flete";
		gridViewTextBoxColumn37.Name = "colPorcentajeFlete";
		gridViewTextBoxColumn37.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn37.Width = 75;
		gridViewTextBoxColumn38.DataType = typeof(double);
		gridViewTextBoxColumn38.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Percent;
		gridViewTextBoxColumn38.FieldName = "porcentaje_desestiva";
		gridViewTextBoxColumn38.FormatString = "{0:#0.00%}";
		gridViewTextBoxColumn38.HeaderText = "% Desestiva";
		gridViewTextBoxColumn38.Name = "colPorcentajeDesestiva";
		gridViewTextBoxColumn38.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn38.Width = 81;
		gridViewTextBoxColumn39.FieldName = "fecharegistro";
		gridViewTextBoxColumn39.HeaderText = "horaregistro";
		gridViewTextBoxColumn39.Name = "colfecharegistro";
		gridViewTextBoxColumn39.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn39.Width = 100;
		gridViewTextBoxColumn40.FieldName = "codAlmacen";
		gridViewTextBoxColumn40.HeaderText = "codalmacen";
		gridViewTextBoxColumn40.IsVisible = false;
		gridViewTextBoxColumn40.Name = "colCodAlmacen";
		gridViewTextBoxColumn41.FieldName = "ctgcliente";
		gridViewTextBoxColumn41.HeaderText = "Ctg.Cliente";
		gridViewTextBoxColumn41.Name = "ctgcliente";
		gridViewTextBoxColumn41.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn41.Width = 100;
		gridViewTextBoxColumn42.FieldName = "rangohorario";
		gridViewTextBoxColumn42.HeaderText = "RangoHorario";
		gridViewTextBoxColumn42.Name = "rangohorario";
		gridViewTextBoxColumn42.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn42.Width = 100;
		gridViewTextBoxColumn43.FieldName = "doc_cliente";
		gridViewTextBoxColumn43.HeaderText = "Ruc/Dni";
		gridViewTextBoxColumn43.Name = "doc_cliente";
		gridViewTextBoxColumn43.Width = 100;
		gridViewTextBoxColumn44.FieldName = "order1";
		gridViewTextBoxColumn44.HeaderText = "orden";
		gridViewTextBoxColumn44.IsVisible = false;
		gridViewTextBoxColumn44.Name = "order1";
		gridViewTextBoxColumn44.Width = 100;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32, gridViewTextBoxColumn33, gridViewTextBoxColumn34, gridViewTextBoxColumn35, gridViewTextBoxColumn36, gridViewTextBoxColumn37, gridViewTextBoxColumn38, gridViewTextBoxColumn39, gridViewTextBoxColumn40, gridViewTextBoxColumn41, gridViewTextBoxColumn42, gridViewTextBoxColumn43, gridViewTextBoxColumn44);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.MultiSelect = true;
		this.rgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.ShowHeaderCellButtons = true;
		this.rgvDetalle.Size = new System.Drawing.Size(1104, 360);
		this.rgvDetalle.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1110, 450);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmAnalisisDetalladoDeVenta";
		this.Text = "Analisis Detallado De Venta";
		base.Load += new System.EventHandler(frmAnalisisDetalladoDeVenta_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
