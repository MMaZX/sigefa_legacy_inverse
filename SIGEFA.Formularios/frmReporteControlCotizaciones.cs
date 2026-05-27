using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Data;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmReporteControlCotizaciones : Form
{
	private bool flgloadcboanalisis = false;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnReporte;

	private Button btnBusqueda;

	private Label label1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private RadGridView rgvDetalle;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private ComboBox cbo_Analisis;

	private Label label9;

	private ComboBox cmbAlmacen;

	private Label label8;

	public frmReporteControlCotizaciones()
	{
		InitializeComponent();
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

	private void frmReporteControlCotizaciones_Load(object sender, EventArgs e)
	{
		CargaAnalisis();
		flgloadcboanalisis = true;
		cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
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
			ds = dBAccess.ExecuteDataSet("AnalisisDetalladoCotizaciones");
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
		try
		{
			Cursor = Cursors.WaitCursor;
			SLDocument sl = new SLDocument();
			if (rgvDetalle.ChildRows.Count > 0)
			{
				int i = 0;
				int fila_excel = 4;
				int fila_a_concatenar = 0;
				int fila_first_concat = 0;
				int contador = 1;
				string desde = dtpDesde.Value.ToString();
				string hasta = dtpHasta.Value.ToString();
				sl.AddWorksheet("Listado de Control de Cotizaciones");
				formatearFilaPrincipalHoja(sl, desde, hasta);
				contador = 1;
				DataTable table = new DataTable();
				foreach (GridViewDataColumn column in rgvDetalle.Columns)
				{
					table.Columns.Add(column.Name, column.DataType);
				}
				foreach (GridViewRowInfo row in rgvDetalle.MasterTemplate.DataView)
				{
					DataRow dataRow = table.NewRow();
					for (int o = 0; o < table.Columns.Count; o++)
					{
						dataRow[o] = row.Cells[o].Value;
					}
					table.Rows.Add(dataRow);
				}
				foreach (DataRow fila in table.Rows)
				{
					dandoValoresaFilaVentasExcel(sl, fila_excel, fila);
					fila_excel++;
					i++;
					contador++;
				}
			}
			Cursor = Cursors.Default;
			try
			{
				string cadenaGuardado = obtenerRutaParaGuardar();
				if (cadenaGuardado != null)
				{
					sl.SaveAs(cadenaGuardado);
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ReporteControlCotizaciones");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, "Error");
		}
	}

	private string obtenerRutaParaGuardar(string namefile = "ReporteControlCotizaciones")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "ReporteControlCotizaciones";
			sfd.FileName = namefile + DateTime.Now.ToString("yyyy-MM-dd");
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Control Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void formatearFilaPrincipalHoja(SLDocument sl, string desde, string hasta)
	{
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 3, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellStyle("A1", style);
		sl.SetCellValue("A1", "N°");
		sl.SetColumnWidth(1, 10.0);
		sl.SetCellStyle("B1", style);
		sl.SetCellValue("B1", "F.Cotización");
		sl.SetColumnWidth(2, 13.0);
		sl.SetCellStyle("C1", style);
		sl.SetCellValue("C1", "Almacen");
		sl.SetColumnWidth(3, 20.0);
		sl.SetCellStyle("D1", style);
		sl.SetCellValue("D1", "#Cotización");
		sl.SetColumnWidth(4, 18.0);
		sl.SetCellStyle("E1", style);
		sl.SetCellValue("E1", "Cliente");
		sl.SetColumnWidth(5, 45.0);
		sl.SetCellStyle("F1", style);
		sl.SetCellValue("F1", "F.OrdenCompra");
		sl.SetColumnWidth(6, 20.0);
		sl.SetCellStyle("G1", style);
		sl.SetCellValue("G1", "#OrdenCompra");
		sl.SetColumnWidth(7, 20.0);
		sl.SetCellStyle("H1", style);
		sl.SetCellValue("H1", "Producto");
		sl.SetColumnWidth(8, 50.0);
		sl.SetCellStyle("I1", style);
		sl.SetCellValue("I1", "Cantidad_Cotizada");
		sl.SetColumnWidth(9, 20.0);
		sl.SetCellStyle("J1", style);
		sl.SetCellValue("J1", "Cantidad Orden Compra");
		sl.SetColumnWidth(10, 25.0);
		sl.SetCellStyle("K1", style);
		sl.SetCellValue("K1", "Cantidad No Atendida");
		sl.SetColumnWidth(11, 25.0);
		sl.SetCellStyle("L1", style);
		sl.SetCellValue("L1", "Precio Unitario");
		sl.SetColumnWidth(12, 25.0);
		sl.SetCellStyle("M1", style);
		sl.SetCellValue("M1", "Importe Cotizado");
		sl.SetColumnWidth(13, 25.0);
		sl.SetCellStyle("N1", style);
		sl.SetCellValue("N1", "Importe Atendido");
		sl.SetColumnWidth(14, 25.0);
		sl.SetCellStyle("O1", style);
		sl.SetCellValue("O1", "Importe No Atendido");
		sl.SetColumnWidth(15, 25.0);
		sl.SetCellStyle("P1", style);
		sl.SetCellValue("P1", "Motivo");
		sl.SetColumnWidth(16, 15.0);
		sl.SetCellStyle("Q1", style);
		sl.SetCellValue("Q1", "Estado");
		sl.SetColumnWidth(17, 17.0);
		sl.SetCellStyle("R1", style);
		sl.SetCellValue("R1", "Vendedor");
		sl.SetColumnWidth(18, 20.0);
	}

	private void asignarBordes(SLStyle style)
	{
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.LeftBorder.Color = System.Drawing.Color.LightSkyBlue;
		style.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.TopBorder.Color = System.Drawing.Color.LightSkyBlue;
		style.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.RightBorder.Color = System.Drawing.Color.LightSkyBlue;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.BottomBorder.Color = System.Drawing.Color.LightSkyBlue;
	}

	private void dandoValoresaFilaVentasExcel(SLDocument sl, int fila_excel, DataRow fila)
	{
		SLStyle style = sl.CreateStyle();
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("A" + fila_excel, (fila[0] != DBNull.Value) ? Convert.ToInt32(fila[0]) : Convert.ToInt32(0));
		sl.SetCellStyle("A" + fila_excel, style);
		sl.SetCellValue("B" + fila_excel, fila[1].ToString());
		sl.SetCellStyle("B" + fila_excel, style);
		sl.SetCellValue("C" + fila_excel, fila[2].ToString());
		sl.SetCellStyle("C" + fila_excel, style);
		sl.SetCellValue("D" + fila_excel, fila[3].ToString());
		sl.SetCellStyle("D" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("E" + fila_excel, fila[4].ToString());
		sl.SetCellStyle("E" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("F" + fila_excel, fila[5].ToString());
		sl.SetCellStyle("F" + fila_excel, style);
		sl.SetCellValue("G" + fila_excel, fila[6].ToString());
		sl.SetCellStyle("G" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("H" + fila_excel, fila[7].ToString());
		sl.SetCellStyle("H" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("I" + fila_excel, (fila[8] != DBNull.Value) ? Convert.ToDouble(fila[8]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, (fila[9] != DBNull.Value) ? Convert.ToDouble(fila[9]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, (fila[10] != DBNull.Value) ? Convert.ToDouble(fila[10]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("K" + fila_excel, style);
		sl.SetCellValue("L" + fila_excel, (fila[11] != DBNull.Value) ? Convert.ToDouble(fila[11]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("L" + fila_excel, style);
		sl.SetCellValue("M" + fila_excel, (fila[12] != DBNull.Value) ? Convert.ToDouble(fila[12]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("M" + fila_excel, style);
		sl.SetCellValue("N" + fila_excel, (fila[13] != DBNull.Value) ? Convert.ToDouble(fila[13]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("N" + fila_excel, style);
		sl.SetCellValue("O" + fila_excel, (fila[14] != DBNull.Value) ? Convert.ToDouble(fila[14]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("O" + fila_excel, style);
		sl.SetCellValue("P" + fila_excel, fila[15].ToString());
		sl.SetCellStyle("P" + fila_excel, style);
		sl.SetCellValue("Q" + fila_excel, fila[16].ToString());
		sl.SetCellStyle("Q" + fila_excel, style);
		sl.SetCellValue("R" + fila_excel, fila[17].ToString());
		sl.SetCellStyle("R" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
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
		this.groupBox1.TabIndex = 2;
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
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn2.FieldName = "fecha_cotizacion";
		gridViewTextBoxColumn2.HeaderText = "F.Cotización";
		gridViewTextBoxColumn2.Name = "fecha_cotizacion";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 120;
		gridViewTextBoxColumn2.WrapText = true;
		gridViewTextBoxColumn3.FieldName = "almacen";
		gridViewTextBoxColumn3.HeaderText = "Almacen";
		gridViewTextBoxColumn3.Name = "almacen";
		gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn3.Width = 150;
		gridViewTextBoxColumn4.FieldName = "cotizacion";
		gridViewTextBoxColumn4.HeaderText = "#Cotizacion";
		gridViewTextBoxColumn4.Name = "cotizacion";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 150;
		gridViewTextBoxColumn5.FieldName = "cliente";
		gridViewTextBoxColumn5.HeaderText = "Cliente";
		gridViewTextBoxColumn5.Name = "cliente";
		gridViewTextBoxColumn5.Width = 200;
		gridViewTextBoxColumn6.EnableExpressionEditor = false;
		gridViewTextBoxColumn6.FieldName = "fecha_orden";
		gridViewTextBoxColumn6.HeaderText = "F.OrdenCompra";
		gridViewTextBoxColumn6.Name = "fecha_orden";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 150;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "ordencompra";
		gridViewTextBoxColumn7.HeaderText = "#OrdeCompra";
		gridViewTextBoxColumn7.Name = "ordencompra";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 120;
		gridViewTextBoxColumn8.EnableExpressionEditor = false;
		gridViewTextBoxColumn8.FieldName = "producto";
		gridViewTextBoxColumn8.HeaderText = "Producto";
		gridViewTextBoxColumn8.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn8.Name = "producto";
		gridViewTextBoxColumn8.Width = 200;
		gridViewTextBoxColumn9.EnableExpressionEditor = false;
		gridViewTextBoxColumn9.FieldName = "cantidad_cotizada";
		gridViewTextBoxColumn9.HeaderText = "Cantidad.Cotizada";
		gridViewTextBoxColumn9.Name = "cantidad_cotizada";
		gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn9.Width = 160;
		gridViewTextBoxColumn10.EnableExpressionEditor = false;
		gridViewTextBoxColumn10.FieldName = "cantidad_ordec";
		gridViewTextBoxColumn10.HeaderText = "Cantidad_OrdenC";
		gridViewTextBoxColumn10.Name = "cantidad_ordec";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 160;
		gridViewTextBoxColumn11.FieldName = "cantidad_noatendida";
		gridViewTextBoxColumn11.HeaderText = "CantidadNoAtendida";
		gridViewTextBoxColumn11.Name = "cantidad_noatendida";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 160;
		gridViewTextBoxColumn12.FieldName = "preciounitario";
		gridViewTextBoxColumn12.HeaderText = "P.Unitario";
		gridViewTextBoxColumn12.Name = "preciounitario";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 120;
		gridViewTextBoxColumn13.FieldName = "importecotizado";
		gridViewTextBoxColumn13.HeaderText = "ImporteCotizado";
		gridViewTextBoxColumn13.Name = "importecotizado";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 120;
		gridViewTextBoxColumn14.FieldName = "importeatendido";
		gridViewTextBoxColumn14.HeaderText = "ImporteAtendido";
		gridViewTextBoxColumn14.Name = "importeatendido";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 120;
		gridViewTextBoxColumn15.FieldName = "importenoatendido";
		gridViewTextBoxColumn15.HeaderText = "ImporteNoAtendido";
		gridViewTextBoxColumn15.Name = "importenoatendido";
		gridViewTextBoxColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn15.Width = 120;
		gridViewTextBoxColumn16.FieldName = "motivo";
		gridViewTextBoxColumn16.HeaderText = "Motivo";
		gridViewTextBoxColumn16.Name = "motivo";
		gridViewTextBoxColumn16.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn16.Width = 100;
		gridViewTextBoxColumn17.FieldName = "estado";
		gridViewTextBoxColumn17.HeaderText = "Estado";
		gridViewTextBoxColumn17.Name = "estado";
		gridViewTextBoxColumn17.Width = 100;
		gridViewTextBoxColumn18.EnableExpressionEditor = false;
		gridViewTextBoxColumn18.FieldName = "vendedor";
		gridViewTextBoxColumn18.HeaderText = "Vendedor";
		gridViewTextBoxColumn18.Name = "vendedor";
		gridViewTextBoxColumn18.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn18.Width = 100;
		gridViewTextBoxColumn19.EnableExpressionEditor = false;
		gridViewTextBoxColumn19.FieldName = "empresa";
		gridViewTextBoxColumn19.HeaderText = "Empresa";
		gridViewTextBoxColumn19.IsVisible = false;
		gridViewTextBoxColumn19.Name = "empresa";
		gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn19.Width = 150;
		gridViewTextBoxColumn20.FieldName = "hora";
		gridViewTextBoxColumn20.HeaderText = "Hora";
		gridViewTextBoxColumn20.IsVisible = false;
		gridViewTextBoxColumn20.Name = "hora";
		gridViewTextBoxColumn20.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn20.Width = 100;
		gridViewTextBoxColumn21.EnableExpressionEditor = false;
		gridViewTextBoxColumn21.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.GeneralDate;
		gridViewTextBoxColumn21.FieldName = "mes";
		gridViewTextBoxColumn21.HeaderText = "Mes";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "mes";
		gridViewTextBoxColumn21.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn21.Width = 80;
		gridViewTextBoxColumn22.DataType = typeof(int);
		gridViewTextBoxColumn22.EnableExpressionEditor = false;
		gridViewTextBoxColumn22.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn22.FieldName = "codProducto";
		gridViewTextBoxColumn22.HeaderText = "Cod";
		gridViewTextBoxColumn22.IsVisible = false;
		gridViewTextBoxColumn22.Name = "codProducto";
		gridViewTextBoxColumn22.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn22.Width = 98;
		gridViewTextBoxColumn23.FieldName = "semana";
		gridViewTextBoxColumn23.HeaderText = "Semana";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "semana";
		gridViewTextBoxColumn23.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn23.Width = 100;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23);
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
		this.rgvDetalle.TabIndex = 10;
		this.rgvDetalle.ThemeName = "TelerikMetroTouch";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1370, 629);
		base.Controls.Add(this.rgvDetalle);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmReporteControlCotizaciones";
		this.Text = "frmReporteControlCotizaciones";
		base.Load += new System.EventHandler(frmReporteControlCotizaciones_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
