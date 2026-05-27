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
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmRptDetalladoProductos : Form
{
	private bool flgloadcboanalisis = false;

	private clsAdmTipoDocumento AdmDoc = new clsAdmTipoDocumento();

	public string valor;

	private IContainer components = null;

	private GroupBox groupBox1;

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

	private RadGridView rgvDetalle;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	public frmRptDetalladoProductos()
	{
		InitializeComponent();
	}

	private void frmRptDetalladoProductos_Load(object sender, EventArgs e)
	{
		CargaAnalisis();
		flgloadcboanalisis = true;
		cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		rgvDetalle.MasterView.TableHeaderRow.MinHeight = 45;
		rgvDetalle.AutoSizeRows = true;
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

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		cargaLista();
	}

	private void cargaLista()
	{
		try
		{
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			ds = dBAccess.ExecuteDataSet("ReporteDetalladoPreciosProductos");
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

	private void cbo_Analisis_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (flgloadcboanalisis)
		{
			cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		}
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
			spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
			try
			{
				string cadenaGuardado = obtenerRutaParaGuardar("ListaPreciosCompra-");
				if (cadenaGuardado != null)
				{
					spreadStreamExport.RunExport(cadenaGuardado, new SpreadStreamExportRenderer());
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ListaPreciosCompra");
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
			sfd.Title = "ListaPreciosCompra";
			sfd.FileName = namefile + DateTime.Now.ToString("yyyy-MM-dd");
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion ListaPreciosCompra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		this.rgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
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
		this.groupBox1.Size = new System.Drawing.Size(1354, 88);
		this.groupBox1.TabIndex = 11;
		this.groupBox1.TabStop = false;
		this.cbo_Analisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbo_Analisis.FormattingEnabled = true;
		this.cbo_Analisis.Location = new System.Drawing.Point(421, 13);
		this.cbo_Analisis.Name = "cbo_Analisis";
		this.cbo_Analisis.Size = new System.Drawing.Size(183, 21);
		this.cbo_Analisis.TabIndex = 88;
		this.cbo_Analisis.Visible = false;
		this.cbo_Analisis.SelectedIndexChanged += new System.EventHandler(cbo_Analisis_SelectedIndexChanged);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label9.Location = new System.Drawing.Point(366, 17);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(59, 13);
		this.label9.TabIndex = 87;
		this.label9.Text = "Análisis : ";
		this.label9.Visible = false;
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnReporte.Location = new System.Drawing.Point(1259, 24);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(86, 36);
		this.btnReporte.TabIndex = 83;
		this.btnReporte.Text = "Exportar";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(422, 46);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(183, 21);
		this.cmbAlmacen.TabIndex = 82;
		this.cmbAlmacen.Visible = false;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(363, 49);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 81;
		this.label8.Text = "Almacén: ";
		this.label8.Visible = false;
		this.btnBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnBusqueda.Location = new System.Drawing.Point(1165, 24);
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
		this.label1.Visible = false;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(186, 35);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 78;
		this.label6.Text = "Hasta :";
		this.label6.Visible = false;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(6, 35);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 77;
		this.label5.Text = "Desde :";
		this.label5.Visible = false;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(59, 32);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 76;
		this.dtpDesde.Visible = false;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(236, 32);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 75;
		this.dtpHasta.Visible = false;
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
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn1.FieldName = "numero_item";
		gridViewTextBoxColumn1.HeaderText = "Nro. Item";
		gridViewTextBoxColumn1.Name = "numero_item";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn2.FieldName = "cod_referencia";
		gridViewTextBoxColumn2.HeaderText = "Cod. Referencia";
		gridViewTextBoxColumn2.Name = "cod_referencia";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 120;
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "descripcion";
		gridViewTextBoxColumn3.HeaderText = "Descripción";
		gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn3.Name = "descripcion";
		gridViewTextBoxColumn3.Width = 250;
		gridViewTextBoxColumn4.EnableExpressionEditor = false;
		gridViewTextBoxColumn4.FieldName = "unidad";
		gridViewTextBoxColumn4.HeaderText = "Unidad";
		gridViewTextBoxColumn4.Name = "unidad";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 80;
		gridViewTextBoxColumn5.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn5.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn5.FieldName = "ultimopcdol";
		gridViewTextBoxColumn5.HeaderText = "Ult. P. Unitario $ Inc. Igv";
		gridViewTextBoxColumn5.Name = "ultimopcdol";
		gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn5.Width = 170;
		gridViewTextBoxColumn6.ExcelExportFormatString = "0.000";
		gridViewTextBoxColumn6.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn6.FieldName = "tipocambio";
		gridViewTextBoxColumn6.HeaderText = "Tipo Cambio";
		gridViewTextBoxColumn6.Name = "tipocambio";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 150;
		gridViewTextBoxColumn7.EnableExpressionEditor = false;
		gridViewTextBoxColumn7.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn7.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn7.FieldName = "ultimopcsol";
		gridViewTextBoxColumn7.HeaderText = "Ult. P. Unitario s/. Inc. Igv";
		gridViewTextBoxColumn7.Name = "ultimopcsol";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 170;
		gridViewTextBoxColumn8.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.MediumDate;
		gridViewTextBoxColumn8.FieldName = "ultimafecha";
		gridViewTextBoxColumn8.HeaderText = "Fecha Registro Ult. Compra";
		gridViewTextBoxColumn8.Name = "ultimafecha";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 120;
		gridViewTextBoxColumn9.FieldName = "ultimahora";
		gridViewTextBoxColumn9.HeaderText = "Hora Registro Ult. Compra";
		gridViewTextBoxColumn9.Name = "ultimahora";
		gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn9.Width = 110;
		gridViewTextBoxColumn10.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn10.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn10.FieldName = "ultimopccatalogo";
		gridViewTextBoxColumn10.HeaderText = "P. Unit. Catálogo";
		gridViewTextBoxColumn10.Name = "ultimopccatalogo";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 150;
		gridViewTextBoxColumn11.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn11.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn11.FieldName = "diferencia";
		gridViewTextBoxColumn11.HeaderText = "Dif. P. Unit s/. Vs P. Unit Cat";
		gridViewTextBoxColumn11.Name = "diferencia";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 170;
		gridViewTextBoxColumn12.FieldName = "familia";
		gridViewTextBoxColumn12.HeaderText = "Familia";
		gridViewTextBoxColumn12.Name = "familia";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 200;
		gridViewTextBoxColumn13.FieldName = "linea";
		gridViewTextBoxColumn13.HeaderText = "Linea";
		gridViewTextBoxColumn13.Name = "linea";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 200;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.EnableGrouping = false;
		this.rgvDetalle.MasterTemplate.MultiSelect = true;
		this.rgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvDetalle.ShowHeaderCellButtons = true;
		this.rgvDetalle.Size = new System.Drawing.Size(1354, 380);
		this.rgvDetalle.TabIndex = 13;
		this.rgvDetalle.ThemeName = "TelerikMetroTouch";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1354, 468);
		base.Controls.Add(this.rgvDetalle);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmRptDetalladoProductos";
		this.Text = "frmRptDetalladoProductos";
		base.Load += new System.EventHandler(frmRptDetalladoProductos_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
