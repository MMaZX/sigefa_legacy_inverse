using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class FrmReporteAjustesInventario : Form
{
	private bool flgloadcboanalisis = false;

	private clsAdmTipoDocumento AdmDoc = new clsAdmTipoDocumento();

	public string valor;

	private IContainer components = null;

	private GroupBox groupBox3;

	private Label label4;

	private Label label7;

	public Label lblsobrante;

	private Label lblfaltante;

	private Label label11;

	private Label lbldiferencia;

	private GroupBox groupBox1;

	private RadCheckedDropDownList cmbDocumentos;

	private Label label2;

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

	private Label label3;

	private Label label10;

	public Label lblingresoporkardex;

	private Label lblsalidadkardex;

	private Label label14;

	private Label lbldaa;

	private CheckBox chkTodos;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	public FrmReporteAjustesInventario()
	{
		InitializeComponent();
	}

	private void FrmReporteAjustesInventario_Load(object sender, EventArgs e)
	{
		CargaAnalisis();
		flgloadcboanalisis = true;
		cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		CargaTipoDocumento();
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
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			dBAccess.AddParameter("codalma", cmbAlmacen.SelectedValue);
			dBAccess.AddParameter("fechaini", dtpDesde.Value);
			dBAccess.AddParameter("fechafin", dtpHasta.Value);
			dBAccess.AddParameter("documentos", valor);
			ds = dBAccess.ExecuteDataSet("ReporteAjustesInventario");
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
				sl.AddWorksheet("Listado de Ajustes Inventario");
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
				MessageBox.Show(ex.Message, "ReporteAjustesInventario");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, "Error");
		}
	}

	private string obtenerRutaParaGuardar(string namefile = "ReporteAjustesInventario")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "ReporteAjustesInventario";
			sfd.FileName = namefile + DateTime.Now.ToString("yyyy-MM-dd");
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Ajustes de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		sl.SetCellValue("A1", "REPORTE DE AJUTES INVENTARIO ");
		sl.MergeWorksheetCells("A1", "T1");
		sl.SetCellValue("A2", "DESDE: " + Convert.ToDateTime(desde).ToShortDateString() + " - HASTA: " + Convert.ToDateTime(hasta).ToShortDateString());
		sl.MergeWorksheetCells("A2", "T2");
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 3, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellStyle("A3", style);
		sl.SetCellValue("A3", "Nro. Item");
		sl.SetColumnWidth(1, 10.0);
		sl.SetCellStyle("B3", style);
		sl.SetCellValue("B3", "Día");
		sl.SetColumnWidth(2, 10.0);
		sl.SetCellStyle("C3", style);
		sl.SetCellValue("C3", "Mes");
		sl.SetColumnWidth(3, 10.0);
		sl.SetCellStyle("D3", style);
		sl.SetCellValue("D3", "Año");
		sl.SetColumnWidth(4, 15.0);
		sl.SetCellStyle("E3", style);
		sl.SetCellValue("E3", "F.Registro");
		sl.SetColumnWidth(5, 15.0);
		sl.SetCellStyle("F3", style);
		sl.SetCellValue("F3", "Almacén");
		sl.SetColumnWidth(6, 25.0);
		sl.SetCellStyle("G3", style);
		sl.SetCellValue("G3", "Cod. Referencia");
		sl.SetColumnWidth(7, 20.0);
		sl.SetCellStyle("H3", style);
		sl.SetCellValue("H3", "Descripción del Material");
		sl.SetColumnWidth(8, 30.0);
		sl.SetCellStyle("I3", style);
		sl.SetCellValue("I3", "Familia");
		sl.SetColumnWidth(9, 15.0);
		sl.SetCellStyle("J3", style);
		sl.SetCellValue("J3", "Linea");
		sl.SetColumnWidth(10, 15.0);
		sl.SetCellStyle("K3", style);
		sl.SetCellValue("K3", "Tipo de Documento");
		sl.SetColumnWidth(11, 20.0);
		sl.SetCellStyle("L3", style);
		sl.SetCellValue("L3", "N° de documento Asociado");
		sl.SetColumnWidth(12, 26.0);
		sl.SetCellStyle("M3", style);
		sl.SetCellValue("M3", "Cantidad");
		sl.SetColumnWidth(13, 13.0);
		sl.SetCellStyle("N3", style);
		sl.SetCellValue("N3", "Precio de Venta");
		sl.SetColumnWidth(14, 17.0);
		sl.SetCellStyle("O3", style);
		sl.SetCellValue("O3", "Subtotal del Desfase");
		sl.SetColumnWidth(15, 20.0);
		sl.SetCellStyle("P3", style);
		sl.SetCellValue("P3", "Usuario");
		sl.SetColumnWidth(16, 16.0);
		sl.SetCellStyle("Q3", style);
		sl.SetCellValue("Q3", "Glosa");
		sl.SetColumnWidth(17, 35.0);
		sl.SetCellStyle("R3", style);
		sl.SetCellValue("R3", "Unidad Medidad");
		sl.SetColumnWidth(18, 20.0);
		sl.SetCellStyle("S3", style);
		sl.SetCellValue("S3", "Vendedor");
		sl.SetColumnWidth(19, 22.0);
		sl.SetCellStyle("T3", style);
		sl.SetCellValue("T3", "Responsable");
		sl.SetColumnWidth(20, 25.0);
		sl.SetCellStyle("U3", style);
		sl.SetCellValue("U3", "Area");
		sl.SetColumnWidth(21, 20.0);
		sl.SetCellStyle("V3", style);
		sl.SetCellValue("V3", "F.Ingreso/Salida");
		sl.SetColumnWidth(22, 20.0);
		sl.SetCellStyle("W3", style);
		sl.SetCellValue("W3", "Cliente");
		sl.SetColumnWidth(23, 30.0);
		sl.SetCellStyle("X3", style);
		sl.SetCellValue("X3", "Hora");
		sl.SetColumnWidth(23, 30.0);
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
		sl.SetCellValue("D" + fila_excel, (fila[3] != DBNull.Value) ? Convert.ToInt32(fila[3]) : Convert.ToInt32(0));
		sl.SetCellStyle("D" + fila_excel, style);
		sl.SetCellValue("E" + fila_excel, (fila[4] != DBNull.Value) ? ((DateTime)fila[4]).ToString("MM-dd-yyyy") : ((DateTime)fila[4]).ToString("MM-dd-yyyy"));
		sl.SetCellStyle("E" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.General);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("F" + fila_excel, fila[5].ToString());
		sl.SetCellStyle("F" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("G" + fila_excel, (fila[6] != DBNull.Value) ? Convert.ToInt32(fila[6]) : Convert.ToInt32(0));
		sl.SetCellStyle("G" + fila_excel, style);
		sl.SetCellValue("H" + fila_excel, fila[7].ToString());
		sl.SetCellValue("I" + fila_excel, fila[8].ToString());
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, fila[9].ToString());
		sl.SetCellValue("K" + fila_excel, fila[10].ToString());
		sl.SetCellStyle("K" + fila_excel, style);
		sl.SetCellValue("L" + fila_excel, fila[11].ToString());
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
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("R" + fila_excel, fila[18].ToString());
		sl.SetCellStyle("R" + fila_excel, style);
		sl.SetCellValue("S" + fila_excel, fila[19].ToString());
		sl.SetCellStyle("S" + fila_excel, style);
		sl.SetCellValue("T" + fila_excel, fila[20].ToString());
		sl.SetCellStyle("T" + fila_excel, style);
		sl.SetCellValue("U" + fila_excel, fila[21].ToString());
		sl.SetCellStyle("U" + fila_excel, style);
		sl.SetCellValue("V" + fila_excel, (fila[22] != DBNull.Value) ? ((DateTime)fila[22]).ToString("MM-dd-yyyy") : ((DateTime)fila[22]).ToString("MM-dd-yyyy"));
		sl.SetCellStyle("V" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("W" + fila_excel, fila[23].ToString());
		sl.SetCellStyle("W" + fila_excel, style);
		sl.SetCellValue("X" + fila_excel, fila[24].ToString());
		sl.SetCellStyle("X" + fila_excel, style);
	}

	private void cbo_Analisis_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (flgloadcboanalisis)
		{
			cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		}
	}

	private void CargaTipoDocumento()
	{
		DataTable data = AdmDoc.MuestraTipoDocumentos();
		cmbDocumentos.DataSource = data;
		cmbDocumentos.ValueMember = "codTipoDocumento";
		cmbDocumentos.DisplayMember = "descripcion";
		List<string> codalmaceneslog = new List<string>();
		if (data != null && data.Rows.Count > 0)
		{
			codalmaceneslog = Enumerable.Select<DataRow, string>(data.Rows.Cast<DataRow>(), (Func<DataRow, string>)((DataRow x) => x.Field<object>("codTipoDocumento").ToString())).ToList();
		}
		foreach (RadCheckedListDataItem item in cmbDocumentos.Items)
		{
			if ((Convert.ToInt32(item.Value) == 47 || Convert.ToInt32(item.Value) == 48) && codalmaceneslog.Contains(item.Value.ToString()))
			{
				item.Checked = true;
			}
		}
	}

	private void cmbDocumentos_ItemCheckedChanged(object sender, RadCheckedListDataItemEventArgs e)
	{
		List<string> documentos = Enumerable.Select<RadCheckedListDataItem, string>(cmbDocumentos.CheckedItems.AsEnumerable(), (Func<RadCheckedListDataItem, string>)((RadCheckedListDataItem x) => x.Value.ToString())).ToList();
		int contador = 0;
		foreach (string alm in documentos)
		{
			contador++;
			if (contador >= 2)
			{
				valor = valor + "," + alm;
			}
			else
			{
				valor = alm;
			}
		}
	}

	public void Totales()
	{
		decimal totalsobrante = default(decimal);
		decimal totalfaltante = default(decimal);
		decimal diferencia = default(decimal);
		decimal daa = default(decimal);
		decimal ick = default(decimal);
		decimal sck = default(decimal);
		foreach (GridViewRowInfo row in rgvDetalle.Rows)
		{
			if (Convert.ToString(row.Cells["codtipodocumento"].Value) == "47")
			{
				totalfaltante += Convert.ToDecimal(row.Cells["cantidad"].Value);
			}
			else if (Convert.ToString(row.Cells["codtipodocumento"].Value) == "48")
			{
				totalsobrante += Convert.ToDecimal(row.Cells["cantidad"].Value);
			}
			else if (Convert.ToString(row.Cells["codtipodocumento"].Value) == "36")
			{
				sck += Convert.ToDecimal(row.Cells["cantidad"].Value);
			}
			else if (Convert.ToString(row.Cells["codtipodocumento"].Value) == "37")
			{
				daa += Convert.ToDecimal(row.Cells["cantidad"].Value);
			}
			else if (Convert.ToString(row.Cells["codtipodocumento"].Value) == "35")
			{
				ick += Convert.ToDecimal(row.Cells["cantidad"].Value);
			}
		}
		diferencia = totalfaltante - -1m * totalsobrante;
		lblsobrante.Text = $"{totalsobrante:#,##0.00}";
		lblfaltante.Text = $"{totalfaltante:#,##0.00}";
		lbldiferencia.Text = $"{diferencia:#,##0.00}";
		lbldaa.Text = $"{daa:#,##0.00}";
		lblingresoporkardex.Text = $"{ick:#,##0.00}";
		lblsalidadkardex.Text = $"{sck:#,##0.00}";
	}

	private void chkTodos_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (chkTodos.Checked)
			{
				foreach (RadCheckedListDataItem item in cmbDocumentos.Items)
				{
					item.Checked = true;
				}
				return;
			}
			foreach (RadCheckedListDataItem item2 in cmbDocumentos.Items)
			{
				if (Convert.ToInt32(item2.Value) == 47 || Convert.ToInt32(item2.Value) == 48)
				{
					item2.Checked = true;
				}
				else
				{
					item2.Checked = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.lblingresoporkardex = new System.Windows.Forms.Label();
		this.lblsalidadkardex = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.lbldaa = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.lblsobrante = new System.Windows.Forms.Label();
		this.lblfaltante = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.lbldiferencia = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.chkTodos = new System.Windows.Forms.CheckBox();
		this.cmbDocumentos = new Telerik.WinControls.UI.RadCheckedDropDownList();
		this.label2 = new System.Windows.Forms.Label();
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
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbDocumentos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.lblingresoporkardex);
		this.groupBox3.Controls.Add(this.lblsalidadkardex);
		this.groupBox3.Controls.Add(this.label14);
		this.groupBox3.Controls.Add(this.lbldaa);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.lblsobrante);
		this.groupBox3.Controls.Add(this.lblfaltante);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.lbldiferencia);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(0, 595);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1370, 34);
		this.groupBox3.TabIndex = 8;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Totales :";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(306, 10);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(91, 15);
		this.label3.TabIndex = 45;
		this.label3.Text = "T. SC KARDEX:";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(107, 9);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(86, 15);
		this.label10.TabIndex = 44;
		this.label10.Text = "T. IC KARDEX:";
		this.lblingresoporkardex.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblingresoporkardex.AutoSize = true;
		this.lblingresoporkardex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblingresoporkardex.ForeColor = System.Drawing.Color.Blue;
		this.lblingresoporkardex.Location = new System.Drawing.Point(192, 10);
		this.lblingresoporkardex.Name = "lblingresoporkardex";
		this.lblingresoporkardex.Size = new System.Drawing.Size(31, 15);
		this.lblingresoporkardex.TabIndex = 43;
		this.lblingresoporkardex.Text = "0.00";
		this.lblsalidadkardex.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblsalidadkardex.AutoSize = true;
		this.lblsalidadkardex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblsalidadkardex.ForeColor = System.Drawing.Color.Blue;
		this.lblsalidadkardex.Location = new System.Drawing.Point(397, 10);
		this.lblsalidadkardex.Name = "lblsalidadkardex";
		this.lblsalidadkardex.Size = new System.Drawing.Size(31, 15);
		this.lblsalidadkardex.TabIndex = 46;
		this.lblsalidadkardex.Text = "0.00";
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(515, 10);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(46, 15);
		this.label14.TabIndex = 47;
		this.label14.Text = "T. DAA:";
		this.lbldaa.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lbldaa.AutoSize = true;
		this.lbldaa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldaa.ForeColor = System.Drawing.Color.Blue;
		this.lbldaa.Location = new System.Drawing.Point(561, 10);
		this.lbldaa.Name = "lbldaa";
		this.lbldaa.Size = new System.Drawing.Size(31, 15);
		this.lbldaa.TabIndex = 48;
		this.lbldaa.Text = "0.00";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(894, 11);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(67, 15);
		this.label4.TabIndex = 38;
		this.label4.Text = "T. Faltante:";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(695, 10);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(73, 15);
		this.label7.TabIndex = 37;
		this.label7.Text = "T. Sobrante:";
		this.lblsobrante.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblsobrante.AutoSize = true;
		this.lblsobrante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblsobrante.ForeColor = System.Drawing.Color.Blue;
		this.lblsobrante.Location = new System.Drawing.Point(770, 10);
		this.lblsobrante.Name = "lblsobrante";
		this.lblsobrante.Size = new System.Drawing.Size(31, 15);
		this.lblsobrante.TabIndex = 36;
		this.lblsobrante.Text = "0.00";
		this.lblfaltante.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblfaltante.AutoSize = true;
		this.lblfaltante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblfaltante.ForeColor = System.Drawing.Color.Blue;
		this.lblfaltante.Location = new System.Drawing.Point(964, 11);
		this.lblfaltante.Name = "lblfaltante";
		this.lblfaltante.Size = new System.Drawing.Size(31, 15);
		this.lblfaltante.TabIndex = 39;
		this.lblfaltante.Text = "0.00";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(1103, 11);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(79, 15);
		this.label11.TabIndex = 41;
		this.label11.Text = "T. Diferencia:";
		this.lbldiferencia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lbldiferencia.AutoSize = true;
		this.lbldiferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldiferencia.ForeColor = System.Drawing.Color.Blue;
		this.lbldiferencia.Location = new System.Drawing.Point(1183, 12);
		this.lbldiferencia.Name = "lbldiferencia";
		this.lbldiferencia.Size = new System.Drawing.Size(31, 15);
		this.lbldiferencia.TabIndex = 42;
		this.lbldiferencia.Text = "0.00";
		this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox1.Controls.Add(this.chkTodos);
		this.groupBox1.Controls.Add(this.cmbDocumentos);
		this.groupBox1.Controls.Add(this.label2);
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
		this.groupBox1.Size = new System.Drawing.Size(1370, 88);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.chkTodos.AutoSize = true;
		this.chkTodos.Location = new System.Drawing.Point(1051, 37);
		this.chkTodos.Name = "chkTodos";
		this.chkTodos.Size = new System.Drawing.Size(56, 17);
		this.chkTodos.TabIndex = 91;
		this.chkTodos.Text = "Todos";
		this.chkTodos.UseVisualStyleBackColor = true;
		this.chkTodos.CheckedChanged += new System.EventHandler(chkTodos_CheckedChanged);
		this.cmbDocumentos.AutoScroll = true;
		this.cmbDocumentos.AutoSize = false;
		this.cmbDocumentos.Cursor = System.Windows.Forms.Cursors.Hand;
		this.cmbDocumentos.Location = new System.Drawing.Point(698, 24);
		this.cmbDocumentos.Multiline = true;
		this.cmbDocumentos.Name = "cmbDocumentos";
		this.cmbDocumentos.Size = new System.Drawing.Size(347, 38);
		this.cmbDocumentos.TabIndex = 90;
		this.cmbDocumentos.ThemeName = "ControlDefault";
		this.cmbDocumentos.ItemCheckedChanged += new Telerik.WinControls.UI.RadCheckedListDataItemEventHandler(cmbDocumentos_ItemCheckedChanged);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(615, 36);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(84, 13);
		this.label2.TabIndex = 89;
		this.label2.Text = "Documentos: ";
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
		this.btnReporte.Location = new System.Drawing.Point(1275, 24);
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
		gridViewTextBoxColumn1.HeaderText = "Nro. Item";
		gridViewTextBoxColumn1.Name = "numero_item";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 68;
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.FieldName = "dia";
		gridViewTextBoxColumn2.HeaderText = "Día";
		gridViewTextBoxColumn2.Name = "dia";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 75;
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "mes";
		gridViewTextBoxColumn3.HeaderText = "Mes";
		gridViewTextBoxColumn3.Name = "mes";
		gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn3.Width = 75;
		gridViewTextBoxColumn4.DataType = typeof(int);
		gridViewTextBoxColumn4.EnableExpressionEditor = false;
		gridViewTextBoxColumn4.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn4.FieldName = "año";
		gridViewTextBoxColumn4.HeaderText = "Año";
		gridViewTextBoxColumn4.Name = "año";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 65;
		gridViewTextBoxColumn4.WrapText = true;
		gridViewTextBoxColumn5.EnableExpressionEditor = false;
		gridViewTextBoxColumn5.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.GeneralDate;
		gridViewTextBoxColumn5.FieldName = "fecha_registro";
		gridViewTextBoxColumn5.HeaderText = "F.Registro";
		gridViewTextBoxColumn5.Name = "fecha_registro";
		gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn5.Width = 80;
		gridViewTextBoxColumn6.EnableExpressionEditor = false;
		gridViewTextBoxColumn6.FieldName = "almacen";
		gridViewTextBoxColumn6.HeaderText = "Almacén";
		gridViewTextBoxColumn6.Name = "almacen";
		gridViewTextBoxColumn6.Width = 130;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.DataType = typeof(int);
		gridViewTextBoxColumn7.EnableExpressionEditor = false;
		gridViewTextBoxColumn7.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn7.FieldName = "cod_referencia";
		gridViewTextBoxColumn7.HeaderText = "Cod. Referencia";
		gridViewTextBoxColumn7.Name = "cod_referencia";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 110;
		gridViewTextBoxColumn8.EnableExpressionEditor = false;
		gridViewTextBoxColumn8.FieldName = "descripcion";
		gridViewTextBoxColumn8.HeaderText = "Descripción del Material";
		gridViewTextBoxColumn8.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn8.Name = "descripcion";
		gridViewTextBoxColumn8.Width = 200;
		gridViewTextBoxColumn9.EnableExpressionEditor = false;
		gridViewTextBoxColumn9.FieldName = "familia";
		gridViewTextBoxColumn9.HeaderText = "Familia";
		gridViewTextBoxColumn9.Name = "familia";
		gridViewTextBoxColumn9.Width = 100;
		gridViewTextBoxColumn10.EnableExpressionEditor = false;
		gridViewTextBoxColumn10.FieldName = "linea";
		gridViewTextBoxColumn10.HeaderText = "Linea";
		gridViewTextBoxColumn10.Name = "linea";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 75;
		gridViewTextBoxColumn11.EnableExpressionEditor = false;
		gridViewTextBoxColumn11.FieldName = "tipodocumento";
		gridViewTextBoxColumn11.HeaderText = "Tipo de Documento";
		gridViewTextBoxColumn11.Name = "tipodocumento";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 120;
		gridViewTextBoxColumn12.EnableExpressionEditor = false;
		gridViewTextBoxColumn12.FieldName = "numerodoc_asociado";
		gridViewTextBoxColumn12.HeaderText = "N° de documento Asociado";
		gridViewTextBoxColumn12.Name = "numerodoc_asociado";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 120;
		gridViewTextBoxColumn13.DataType = typeof(double);
		gridViewTextBoxColumn13.EnableExpressionEditor = false;
		gridViewTextBoxColumn13.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn13.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn13.FieldName = "cantidad";
		gridViewTextBoxColumn13.HeaderText = "Cantidad";
		gridViewTextBoxColumn13.Name = "cantidad";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 75;
		gridViewTextBoxColumn14.EnableExpressionEditor = false;
		gridViewTextBoxColumn14.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn14.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn14.FieldName = "precio_venta";
		gridViewTextBoxColumn14.HeaderText = "Precio de Venta";
		gridViewTextBoxColumn14.Name = "precio_venta";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 85;
		gridViewTextBoxColumn15.DataType = typeof(double);
		gridViewTextBoxColumn15.EnableExpressionEditor = false;
		gridViewTextBoxColumn15.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn15.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn15.FieldName = "subtotal_desfase";
		gridViewTextBoxColumn15.HeaderText = "Subtotal del Desfase";
		gridViewTextBoxColumn15.Name = "subtotal_desfase";
		gridViewTextBoxColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn15.Width = 110;
		gridViewTextBoxColumn16.EnableExpressionEditor = false;
		gridViewTextBoxColumn16.FieldName = "usuario";
		gridViewTextBoxColumn16.HeaderText = "Usuario";
		gridViewTextBoxColumn16.Name = "usuario";
		gridViewTextBoxColumn16.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn16.Width = 91;
		gridViewTextBoxColumn17.EnableExpressionEditor = false;
		gridViewTextBoxColumn17.FieldName = "glosa";
		gridViewTextBoxColumn17.HeaderText = "Glosa";
		gridViewTextBoxColumn17.Name = "glosa";
		gridViewTextBoxColumn17.Width = 120;
		gridViewTextBoxColumn18.EnableExpressionEditor = false;
		gridViewTextBoxColumn18.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.None;
		gridViewTextBoxColumn18.FieldName = "codtipodocumento";
		gridViewTextBoxColumn18.HeaderText = "Cod. Tipo Documento";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "codtipodocumento";
		gridViewTextBoxColumn19.FieldName = "unidad";
		gridViewTextBoxColumn19.HeaderText = "Unidad";
		gridViewTextBoxColumn19.Name = "unidad";
		gridViewTextBoxColumn19.Width = 100;
		gridViewTextBoxColumn20.FieldName = "vendedor";
		gridViewTextBoxColumn20.HeaderText = "Vendedor";
		gridViewTextBoxColumn20.Name = "vendedor";
		gridViewTextBoxColumn20.Width = 100;
		gridViewTextBoxColumn21.FieldName = "responsable";
		gridViewTextBoxColumn21.HeaderText = "Responsable";
		gridViewTextBoxColumn21.Name = "responsable";
		gridViewTextBoxColumn21.Width = 120;
		gridViewTextBoxColumn22.FieldName = "area";
		gridViewTextBoxColumn22.HeaderText = "Area";
		gridViewTextBoxColumn22.Name = "area";
		gridViewTextBoxColumn22.Width = 100;
		gridViewTextBoxColumn23.FieldName = "fingreso";
		gridViewTextBoxColumn23.HeaderText = "F.Ingreso/salida";
		gridViewTextBoxColumn23.Name = "fingreso";
		gridViewTextBoxColumn23.Width = 60;
		gridViewTextBoxColumn24.FieldName = "cliente";
		gridViewTextBoxColumn24.HeaderText = "Cliente";
		gridViewTextBoxColumn24.Name = "cliente";
		gridViewTextBoxColumn24.Width = 120;
		gridViewTextBoxColumn25.FieldName = "hora";
		gridViewTextBoxColumn25.HeaderText = "Hora";
		gridViewTextBoxColumn25.Name = "hora";
		gridViewTextBoxColumn25.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn25.Width = 100;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.EnableGrouping = false;
		this.rgvDetalle.MasterTemplate.MultiSelect = true;
		this.rgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvDetalle.ShowHeaderCellButtons = true;
		this.rgvDetalle.Size = new System.Drawing.Size(1370, 507);
		this.rgvDetalle.TabIndex = 9;
		this.rgvDetalle.ThemeName = "TelerikMetroTouch";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(1370, 629);
		base.Controls.Add(this.rgvDetalle);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Name = "FrmReporteAjustesInventario";
		this.Text = "Reporte Ajustes Inventario";
		base.Load += new System.EventHandler(FrmReporteAjustesInventario_Load);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbDocumentos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
