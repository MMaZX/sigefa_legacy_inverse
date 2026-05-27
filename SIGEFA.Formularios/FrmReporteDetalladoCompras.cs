using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.Export;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class FrmReporteDetalladoCompras : Form
{
	private bool flgloadcboanalisis = false;

	private clsAdmTipoDocumento AdmDoc = new clsAdmTipoDocumento();

	public string valor;

	private IContainer components = null;

	private RadGridView rgvDetalle;

	private GroupBox groupBox3;

	private Label label4;

	private Label label7;

	public Label lblsubtotal;

	private Label lbltotal;

	private GroupBox groupBox1;

	private ComboBox cbo_Analisis;

	private Label label9;

	private Button btnReporte;

	private ComboBox cmbAlmacen;

	private Label label8;

	private Button btnBusqueda;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private Button btnexcel;

	private RadioButton rbFechaRegistro;

	private RadioButton rbFecha;

	public FrmReporteDetalladoCompras()
	{
		InitializeComponent();
	}

	private void FrmReporteDetalladoCompras_Load(object sender, EventArgs e)
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
		Totales();
	}

	private void cargaLista()
	{
		try
		{
			int tipoFecha = (rbFecha.Checked ? 2 : (rbFechaRegistro.Checked ? 1 : 0));
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			dBAccess.AddParameter("codalma", cmbAlmacen.SelectedValue);
			dBAccess.AddParameter("fechaini", dtpDesde.Value);
			dBAccess.AddParameter("fechafin", dtpHasta.Value);
			dBAccess.AddParameter("documentos", valor);
			dBAccess.AddParameter("tipoFecha", tipoFecha);
			ds = dBAccess.ExecuteDataSet("ReporteDetalladoCompras");
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

	public void Totales()
	{
		try
		{
			double total = 0.0;
			foreach (GridViewRowInfo row in rgvDetalle.Rows)
			{
				total += Convert.ToDouble(row.Cells["total"].Value);
			}
			lblsubtotal.Text = $"{total / 1.18:#,##0.00}";
			lbltotal.Text = $"{total:#,##0.00}";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
				string cadenaGuardado = obtenerRutaParaGuardar("ReporteDetalladoCompras-");
				if (cadenaGuardado != null)
				{
					spreadStreamExport.RunExport(cadenaGuardado, new SpreadStreamExportRenderer());
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Reporte Detallado Compras");
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
			sfd.Title = "ReporteDetalladoCompra";
			sfd.FileName = namefile + DateTime.Now.ToString("yyyy-MM-dd");
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion detallado de compras", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private string obtenerRutaParaGuardar1()
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "ReporteDetalladoCompra";
			sfd.FileName = "Reporte_Det_Compra_" + DateTime.Now.ToString("yyyy-MM-dd");
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Listado de Plantilla de Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void btnexcel_Click(object sender, EventArgs e)
	{
		int ColInicial = 0;
		int RowInicial = 1;
		SLDocument sl = new SLDocument();
		SLStyle styleCenter = sl.CreateStyle();
		styleCenter.Alignment.Horizontal = HorizontalAlignmentValues.Center;
		styleCenter.Font.FontSize = 10.0;
		styleCenter.Font.Bold = true;
		foreach (GridViewDataColumn column in rgvDetalle.Columns)
		{
			if (column.IsVisible && column.HeaderText != string.Empty)
			{
				ColInicial++;
				sl.SetCellValue(RowInicial, ColInicial, column.HeaderText.ToString());
				sl.SetCellStyle(RowInicial, ColInicial, styleCenter);
			}
		}
		sl.SetColumnWidth(1, 10.0);
		sl.SetColumnWidth(2, 15.0);
		sl.SetColumnWidth(3, 15.0);
		sl.SetColumnWidth(4, 10.0);
		sl.SetColumnWidth(5, 10.0);
		sl.SetColumnWidth(6, 90.0);
		sl.SetColumnWidth(7, 15.0);
		sl.SetColumnWidth(8, 20.0);
		sl.SetColumnWidth(9, 20.0);
		sl.SetColumnWidth(10, 16.0);
		sl.SetColumnWidth(11, 20.0);
		sl.SetColumnWidth(12, 20.0);
		sl.SetColumnWidth(13, 20.0);
		sl.SetColumnWidth(14, 20.0);
		sl.SetColumnWidth(15, 50.0);
		sl.SetColumnWidth(16, 20.0);
		sl.SetColumnWidth(17, 22.0);
		sl.SetColumnWidth(18, 20.0);
		sl.SetColumnWidth(19, 10.0);
		sl.SetColumnWidth(20, 15.0);
		sl.SetColumnWidth(21, 15.0);
		sl.SetColumnWidth(22, 15.0);
		sl.SetColumnWidth(23, 20.0);
		sl.SetColumnWidth(24, 20.0);
		sl.SetColumnWidth(25, 20.0);
		sl.SetColumnWidth(26, 20.0);
		sl.SetColumnWidth(27, 15.0);
		foreach (GridViewRowInfo row in rgvDetalle.ChildRows)
		{
			RowInicial++;
			sl.SetCellValue(RowInicial, 1, row.Cells["numero_item"].Value.ToString());
			string cod_unico = row.Cells["cod_unico"].Value.ToString().Replace(".1", "");
			sl.SetCellValue(RowInicial, 2, cod_unico);
			sl.SetCellValue(RowInicial, 3, row.Cells["cod_referencia"].Value.ToString());
			SLStyle styleFormat = sl.CreateStyle();
			styleFormat.Alignment.Horizontal = HorizontalAlignmentValues.Center;
			styleFormat.FormatCode = "[Black]#";
			SLStyle styleFormatDecimal = sl.CreateStyle();
			styleFormatDecimal.Alignment.Horizontal = HorizontalAlignmentValues.Center;
			styleFormatDecimal.FormatCode = "[Black]#,##0.00";
			decimal cantidad = decimal.Parse(row.Cells["cantidad"].Value.ToString());
			sl.SetCellValue(RowInicial, 4, cantidad);
			sl.SetCellStyle(RowInicial, 4, styleFormat);
			sl.SetCellValue(RowInicial, 5, row.Cells["unidad"].Value.ToString());
			sl.SetCellValue(RowInicial, 6, row.Cells["descripcion"].Value.ToString());
			sl.SetCellValue(RowInicial, 7, row.Cells["moneda"].Value.ToString());
			decimal punitarioigv = decimal.Parse(row.Cells["punitarioigv"].Value.ToString());
			sl.SetCellValue(RowInicial, 8, punitarioigv);
			sl.SetCellStyle(RowInicial, 8, styleFormatDecimal);
			decimal totalcompraigv = decimal.Parse(row.Cells["totalcompraigv"].Value.ToString());
			sl.SetCellValue(RowInicial, 9, totalcompraigv);
			sl.SetCellStyle(RowInicial, 9, styleFormatDecimal);
			sl.SetCellValue(RowInicial, 10, row.Cells["tipocambio"].Value.ToString());
			decimal precio_unitario = decimal.Parse(row.Cells["precio_unitario"].Value.ToString());
			sl.SetCellValue(RowInicial, 11, precio_unitario);
			sl.SetCellStyle(RowInicial, 11, styleFormatDecimal);
			decimal total = decimal.Parse(row.Cells["total"].Value.ToString());
			sl.SetCellValue(RowInicial, 12, total);
			sl.SetCellStyle(RowInicial, 12, styleFormatDecimal);
			decimal punitariosoles = decimal.Parse(row.Cells["punitariosoles"].Value.ToString());
			sl.SetCellValue(RowInicial, 13, punitariosoles);
			sl.SetCellStyle(RowInicial, 13, styleFormatDecimal);
			decimal totalsoles = decimal.Parse(row.Cells["totalsoles"].Value.ToString());
			sl.SetCellValue(RowInicial, 14, totalsoles);
			sl.SetCellStyle(RowInicial, 14, styleFormatDecimal);
			sl.SetCellValue(RowInicial, 15, row.Cells["proveedor"].Value.ToString());
			sl.SetCellValue(RowInicial, 16, row.Cells["fecha"].Value.ToString());
			sl.SetCellValue(RowInicial, 17, row.Cells["tipodocumento"].Value.ToString());
			sl.SetCellValue(RowInicial, 18, row.Cells["numerocompra"].Value.ToString());
			sl.SetCellValue(RowInicial, 19, row.Cells["serie"].Value.ToString());
			string correlativo = row.Cells["correlativo"].Value.ToString().Replace(".1", "");
			sl.SetCellValue(RowInicial, 20, correlativo);
			sl.SetCellValue(RowInicial, 21, row.Cells["estado"].Value.ToString());
			sl.SetCellValue(RowInicial, 22, row.Cells["estadopago"].Value.ToString());
			sl.SetCellValue(RowInicial, 23, row.Cells["almacen"].Value.ToString());
			sl.SetCellValue(RowInicial, 24, row.Cells["fecharegistro"].Value.ToString());
			sl.SetCellValue(RowInicial, 25, row.Cells["horaregistro"].Value.ToString());
			sl.SetCellValue(RowInicial, 26, row.Cells["fechaven"].Value.ToString());
			sl.SetCellValue(RowInicial, 27, row.Cells["usuario"].Value.ToString());
		}
		try
		{
			string cadenaGuardado = obtenerRutaParaGuardar1();
			if (cadenaGuardado != null)
			{
				sl.SaveAs(cadenaGuardado);
				Process.Start("explorer.exe", cadenaGuardado);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, base.Name + " - Line 177");
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn45 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn46 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn47 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn48 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn49 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn50 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn51 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn52 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn53 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn54 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.rgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.lblsubtotal = new System.Windows.Forms.Label();
		this.lbltotal = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnexcel = new System.Windows.Forms.Button();
		this.cbo_Analisis = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.btnReporte = new System.Windows.Forms.Button();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.rbFechaRegistro = new System.Windows.Forms.RadioButton();
		this.rbFecha = new System.Windows.Forms.RadioButton();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
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
		gridViewTextBoxColumn28.EnableExpressionEditor = false;
		gridViewTextBoxColumn28.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn28.FieldName = "numero_item";
		gridViewTextBoxColumn28.HeaderText = "Nro. Item";
		gridViewTextBoxColumn28.Name = "numero_item";
		gridViewTextBoxColumn28.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn28.Width = 100;
		gridViewTextBoxColumn29.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn29.FieldName = "cod_unico";
		gridViewTextBoxColumn29.HeaderText = "Cod. Unico";
		gridViewTextBoxColumn29.Name = "cod_unico";
		gridViewTextBoxColumn29.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn29.Width = 110;
		gridViewTextBoxColumn30.EnableExpressionEditor = false;
		gridViewTextBoxColumn30.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn30.FieldName = "cod_referencia";
		gridViewTextBoxColumn30.HeaderText = "Cod. Referencia";
		gridViewTextBoxColumn30.Name = "cod_referencia";
		gridViewTextBoxColumn30.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn30.Width = 120;
		gridViewTextBoxColumn31.DataType = typeof(double);
		gridViewTextBoxColumn31.EnableExpressionEditor = false;
		gridViewTextBoxColumn31.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn31.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn31.FieldName = "cantidad";
		gridViewTextBoxColumn31.HeaderText = "Cantidad";
		gridViewTextBoxColumn31.Name = "cantidad";
		gridViewTextBoxColumn31.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn31.Width = 150;
		gridViewTextBoxColumn32.EnableExpressionEditor = false;
		gridViewTextBoxColumn32.FieldName = "unidad";
		gridViewTextBoxColumn32.HeaderText = "Unidad";
		gridViewTextBoxColumn32.Name = "unidad";
		gridViewTextBoxColumn32.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn32.Width = 80;
		gridViewTextBoxColumn33.EnableExpressionEditor = false;
		gridViewTextBoxColumn33.FieldName = "descripcion";
		gridViewTextBoxColumn33.HeaderText = "Descripción";
		gridViewTextBoxColumn33.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn33.Name = "descripcion";
		gridViewTextBoxColumn33.Width = 250;
		gridViewTextBoxColumn34.FieldName = "moneda";
		gridViewTextBoxColumn34.HeaderText = "Moneda";
		gridViewTextBoxColumn34.Name = "moneda";
		gridViewTextBoxColumn34.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn34.Width = 100;
		gridViewTextBoxColumn35.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn35.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn35.FieldName = "punitarioigv";
		gridViewTextBoxColumn35.HeaderText = "P. Unitario $ Inc IGV";
		gridViewTextBoxColumn35.Name = "punitarioigv";
		gridViewTextBoxColumn35.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn35.Width = 170;
		gridViewTextBoxColumn36.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn36.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn36.FieldName = "totalcompraigv";
		gridViewTextBoxColumn36.HeaderText = "Total Compra $ Inc IGV";
		gridViewTextBoxColumn36.Name = "totalcompraigv";
		gridViewTextBoxColumn36.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn36.Width = 170;
		gridViewTextBoxColumn37.ExcelExportFormatString = "0.000";
		gridViewTextBoxColumn37.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn37.FieldName = "tipocambio";
		gridViewTextBoxColumn37.HeaderText = "Tipo Cambio";
		gridViewTextBoxColumn37.Name = "tipocambio";
		gridViewTextBoxColumn37.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn37.Width = 150;
		gridViewTextBoxColumn38.EnableExpressionEditor = false;
		gridViewTextBoxColumn38.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn38.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn38.FieldName = "precio_unitario";
		gridViewTextBoxColumn38.HeaderText = "P. Unitario S/ Inc IGV";
		gridViewTextBoxColumn38.Name = "precio_unitario";
		gridViewTextBoxColumn38.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn38.Width = 170;
		gridViewTextBoxColumn39.DataType = typeof(double);
		gridViewTextBoxColumn39.EnableExpressionEditor = false;
		gridViewTextBoxColumn39.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn39.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn39.FieldName = "total";
		gridViewTextBoxColumn39.HeaderText = "Total Compra Inc. IGV";
		gridViewTextBoxColumn39.Name = "total";
		gridViewTextBoxColumn39.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn39.Width = 170;
		gridViewTextBoxColumn40.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn40.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn40.FieldName = "punitariosoles";
		gridViewTextBoxColumn40.HeaderText = "P.Unitario S/";
		gridViewTextBoxColumn40.Name = "punitariosoles";
		gridViewTextBoxColumn40.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn40.Width = 150;
		gridViewTextBoxColumn41.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn41.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn41.FieldName = "totalsoles";
		gridViewTextBoxColumn41.HeaderText = "TotalSoles";
		gridViewTextBoxColumn41.Name = "totalsoles";
		gridViewTextBoxColumn41.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn41.Width = 100;
		gridViewTextBoxColumn42.FieldName = "proveedor";
		gridViewTextBoxColumn42.HeaderText = "Proveedor";
		gridViewTextBoxColumn42.Name = "proveedor";
		gridViewTextBoxColumn42.Width = 250;
		gridViewTextBoxColumn43.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.MediumDate;
		gridViewTextBoxColumn43.FieldName = "fecha";
		gridViewTextBoxColumn43.HeaderText = "Fecha Emisión";
		gridViewTextBoxColumn43.Name = "fecha";
		gridViewTextBoxColumn43.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn43.Width = 120;
		gridViewTextBoxColumn44.EnableExpressionEditor = false;
		gridViewTextBoxColumn44.FieldName = "tipodocumento";
		gridViewTextBoxColumn44.HeaderText = "Tipo de Documento";
		gridViewTextBoxColumn44.Name = "tipodocumento";
		gridViewTextBoxColumn44.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn44.Width = 140;
		gridViewTextBoxColumn45.FieldName = "numerocompra";
		gridViewTextBoxColumn45.HeaderText = "Comprobante";
		gridViewTextBoxColumn45.Name = "numerocompra";
		gridViewTextBoxColumn45.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn45.Width = 140;
		gridViewTextBoxColumn46.FieldName = "serie";
		gridViewTextBoxColumn46.HeaderText = "Serie";
		gridViewTextBoxColumn46.Name = "serie";
		gridViewTextBoxColumn46.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn46.Width = 100;
		gridViewTextBoxColumn47.ExcelExportFormatString = "0";
		gridViewTextBoxColumn47.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn47.FieldName = "correlativo";
		gridViewTextBoxColumn47.HeaderText = "Correlativo";
		gridViewTextBoxColumn47.Name = "correlativo";
		gridViewTextBoxColumn47.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn47.Width = 100;
		gridViewTextBoxColumn48.EnableExpressionEditor = false;
		gridViewTextBoxColumn48.FieldName = "estado";
		gridViewTextBoxColumn48.HeaderText = "Estado Compra";
		gridViewTextBoxColumn48.Name = "estado";
		gridViewTextBoxColumn48.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn48.Width = 100;
		gridViewTextBoxColumn49.EnableExpressionEditor = false;
		gridViewTextBoxColumn49.FieldName = "estadopago";
		gridViewTextBoxColumn49.HeaderText = "Estado Pago";
		gridViewTextBoxColumn49.Name = "estadopago";
		gridViewTextBoxColumn49.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn49.Width = 100;
		gridViewTextBoxColumn50.EnableExpressionEditor = false;
		gridViewTextBoxColumn50.FieldName = "almacen";
		gridViewTextBoxColumn50.HeaderText = "Almacén";
		gridViewTextBoxColumn50.Name = "almacen";
		gridViewTextBoxColumn50.Width = 130;
		gridViewTextBoxColumn50.WrapText = true;
		gridViewTextBoxColumn51.FieldName = "fecharegistro";
		gridViewTextBoxColumn51.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn51.Name = "fecharegistro";
		gridViewTextBoxColumn51.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn51.Width = 140;
		gridViewTextBoxColumn52.FieldName = "horaregistro";
		gridViewTextBoxColumn52.HeaderText = "Hora Registro";
		gridViewTextBoxColumn52.Name = "horaregistro";
		gridViewTextBoxColumn52.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn52.Width = 110;
		gridViewTextBoxColumn53.FieldName = "fechaven";
		gridViewTextBoxColumn53.HeaderText = "Fecha Vencimiento";
		gridViewTextBoxColumn53.Name = "fechaven";
		gridViewTextBoxColumn53.Width = 140;
		gridViewTextBoxColumn54.EnableExpressionEditor = false;
		gridViewTextBoxColumn54.FieldName = "usuario";
		gridViewTextBoxColumn54.HeaderText = "Usuario";
		gridViewTextBoxColumn54.Name = "usuario";
		gridViewTextBoxColumn54.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn54.Width = 150;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32, gridViewTextBoxColumn33, gridViewTextBoxColumn34, gridViewTextBoxColumn35, gridViewTextBoxColumn36, gridViewTextBoxColumn37, gridViewTextBoxColumn38, gridViewTextBoxColumn39, gridViewTextBoxColumn40, gridViewTextBoxColumn41, gridViewTextBoxColumn42, gridViewTextBoxColumn43, gridViewTextBoxColumn44, gridViewTextBoxColumn45, gridViewTextBoxColumn46, gridViewTextBoxColumn47, gridViewTextBoxColumn48, gridViewTextBoxColumn49, gridViewTextBoxColumn50, gridViewTextBoxColumn51, gridViewTextBoxColumn52, gridViewTextBoxColumn53, gridViewTextBoxColumn54);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.EnableGrouping = false;
		this.rgvDetalle.MasterTemplate.MultiSelect = true;
		this.rgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvDetalle.ShowHeaderCellButtons = true;
		this.rgvDetalle.Size = new System.Drawing.Size(1354, 346);
		this.rgvDetalle.TabIndex = 12;
		this.rgvDetalle.ThemeName = "TelerikMetroTouch";
		this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.lblsubtotal);
		this.groupBox3.Controls.Add(this.lbltotal);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(0, 434);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1354, 34);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Totales :";
		this.groupBox3.Visible = false;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(304, 11);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 15);
		this.label4.TabIndex = 38;
		this.label4.Text = "Sub. Total:";
		this.label4.Visible = false;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(555, 10);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(37, 15);
		this.label7.TabIndex = 37;
		this.label7.Text = "Total:";
		this.label7.Visible = false;
		this.lblsubtotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblsubtotal.AutoSize = true;
		this.lblsubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblsubtotal.ForeColor = System.Drawing.Color.Blue;
		this.lblsubtotal.Location = new System.Drawing.Point(375, 10);
		this.lblsubtotal.Name = "lblsubtotal";
		this.lblsubtotal.Size = new System.Drawing.Size(36, 17);
		this.lblsubtotal.TabIndex = 36;
		this.lblsubtotal.Text = "0.00";
		this.lblsubtotal.Visible = false;
		this.lbltotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lbltotal.AutoSize = true;
		this.lbltotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbltotal.ForeColor = System.Drawing.Color.Blue;
		this.lbltotal.Location = new System.Drawing.Point(598, 9);
		this.lbltotal.Name = "lbltotal";
		this.lbltotal.Size = new System.Drawing.Size(36, 17);
		this.lbltotal.TabIndex = 39;
		this.lbltotal.Text = "0.00";
		this.lbltotal.Visible = false;
		this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox1.Controls.Add(this.rbFechaRegistro);
		this.groupBox1.Controls.Add(this.rbFecha);
		this.groupBox1.Controls.Add(this.btnexcel);
		this.groupBox1.Controls.Add(this.cbo_Analisis);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1354, 88);
		this.groupBox1.TabIndex = 10;
		this.groupBox1.TabStop = false;
		this.btnexcel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnexcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnexcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnexcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnexcel.Location = new System.Drawing.Point(1073, 24);
		this.btnexcel.Name = "btnexcel";
		this.btnexcel.Size = new System.Drawing.Size(86, 36);
		this.btnexcel.TabIndex = 89;
		this.btnexcel.Text = "Exportar";
		this.btnexcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnexcel.UseVisualStyleBackColor = true;
		this.btnexcel.Click += new System.EventHandler(btnexcel_Click);
		this.cbo_Analisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbo_Analisis.FormattingEnabled = true;
		this.cbo_Analisis.Location = new System.Drawing.Point(421, 13);
		this.cbo_Analisis.Name = "cbo_Analisis";
		this.cbo_Analisis.Size = new System.Drawing.Size(183, 21);
		this.cbo_Analisis.TabIndex = 88;
		this.cbo_Analisis.SelectedIndexChanged += new System.EventHandler(cbo_Analisis_SelectedIndexChanged);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label9.Location = new System.Drawing.Point(366, 17);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(59, 13);
		this.label9.TabIndex = 87;
		this.label9.Text = "Análisis : ";
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
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(363, 49);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 81;
		this.label8.Text = "Almacén: ";
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
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(186, 43);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 78;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(6, 43);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 77;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(59, 40);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 76;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(236, 40);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 75;
		this.rbFechaRegistro.AutoSize = true;
		this.rbFechaRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbFechaRegistro.Location = new System.Drawing.Point(134, 11);
		this.rbFechaRegistro.Name = "rbFechaRegistro";
		this.rbFechaRegistro.Size = new System.Drawing.Size(122, 19);
		this.rbFechaRegistro.TabIndex = 91;
		this.rbFechaRegistro.Text = "Fecha Registro";
		this.rbFechaRegistro.UseVisualStyleBackColor = true;
		this.rbFecha.AutoSize = true;
		this.rbFecha.Checked = true;
		this.rbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbFecha.Location = new System.Drawing.Point(12, 11);
		this.rbFecha.Name = "rbFecha";
		this.rbFecha.Size = new System.Drawing.Size(118, 19);
		this.rbFecha.TabIndex = 90;
		this.rbFecha.TabStop = true;
		this.rbFecha.Text = "Fecha Compra";
		this.rbFecha.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1354, 468);
		base.Controls.Add(this.rgvDetalle);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Name = "FrmReporteDetalladoCompras";
		this.Text = "FrmReporteDetalladoCompras";
		base.Load += new System.EventHandler(FrmReporteDetalladoCompras_Load);
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
