using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListaProductoDespachar : Form
{
	private clsAdmFormulario admForm = new clsAdmFormulario();

	private clsAdmDespacho admDespacho = new clsAdmDespacho();

	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private RadGridView rgvProductos;

	private RadGridView rgvDocumentos;

	private Button btnBusqueda;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private ComboBox cmbTipoFecha;

	private Label label1;

	private Label label4;

	private ComboBox cmbAlmacenes;

	private Button btnReporteDetallado;

	private Button btnReporteResumen;

	public frmListaProductoDespachar()
	{
		InitializeComponent();
	}

	private void frmListaProductoDespachar_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		cargarListadoDeProductos();
		rgvDocumentos.DataSource = null;
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void cargarListadoDeProductos()
	{
		DateTime fecha1 = dtpDesde.Value;
		DateTime fecha2 = dtpHasta.Value;
		int tipoFecha = cmbTipoFecha.SelectedIndex;
		int codAlmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
		int codSucursal = frmLogin.iCodSucursal;
		DataTable data = admForm.cargaListadoProductosADespachar(tipoFecha, fecha1, fecha2, codAlmacen, codSucursal);
		rgvProductos.DataSource = data;
	}

	private void rgvProductos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codProducto = Convert.ToInt32(e.Row.Cells["colCodProducto"].Value);
			int codUnidad = Convert.ToInt32(e.Row.Cells["colCodUnidad"].Value);
			DataTable data = admForm.cargaDocumentosDeProductosADespachar(codProducto, codUnidad, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal);
			rgvDocumentos.DataSource = data;
		}
	}

	private void rgvDocumentos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
		{
			return;
		}
		try
		{
			string[] almacenes = e.Row.Cells["colCodAlmacenes"].Value.ToString().Split(',');
			bool ModoEdicion = false;
			if (almacenes.Contains(frmLogin.iCodAlmacen.ToString()))
			{
				ModoEdicion = true;
			}
			string name = e.Column.Name;
			string text = name;
			if (!(text == "colDescReqAlm"))
			{
				if (text == "colDescripDocumento")
				{
					int codDesp = Convert.ToInt32(e.Row.Cells["colCodDocumento"].Value);
					clsDespacho despacho = admDespacho.cargaDespacho(codDesp);
					if (despacho != null)
					{
						frmDespacho frm = new frmDespacho();
						frm.MdiParent = base.MdiParent;
						frm.Dock = DockStyle.Fill;
						frm.WindowState = FormWindowState.Maximized;
						frm.codDespacho = despacho.CodDespacho;
						frm.Proceso = ((!ModoEdicion) ? 2 : ((despacho.Estado == 1 && despacho.CodEstado != 18) ? 1 : 2));
						frm.Show();
					}
				}
			}
			else
			{
				int codReqAlm = Convert.ToInt32(e.Row.Cells["colCodReqAlm"].Value);
				frmReqAlmacen form = new frmReqAlmacen();
				form.MdiParent = base.MdiParent;
				form.codRequerimientoAlmacen = codReqAlm;
				form.Proceso = (ModoEdicion ? 1 : 2);
				form.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		cargarListadoDeProductos();
		rgvDocumentos.DataSource = null;
	}

	private void btnReporteResumen_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvProductos.RowCount > 0)
			{
				Cursor = Cursors.WaitCursor;
				SLDocument sl = new SLDocument();
				DateTime fecha1 = dtpDesde.Value;
				DateTime fecha2 = dtpHasta.Value;
				int tipoFecha = cmbTipoFecha.SelectedIndex;
				int codAlmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
				int codSucursal = frmLogin.iCodSucursal;
				DataTable data = admForm.cargaListadoProductosADespachar(tipoFecha, fecha1, fecha2, codAlmacen, codSucursal);
				if (data.Rows.Count <= 0)
				{
					return;
				}
				int i = 0;
				int fila_excel = 3;
				int fila_a_concatenar = 0;
				int fila_first_concat = 0;
				int contador = 1;
				DateTime aux_date = fecha1;
				string fecha3 = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
				aux_date = fecha2;
				fecha3 = fecha3 + "_A_" + aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
				fecha3 = ((tipoFecha == -1) ? "TODOS" : fecha3);
				sl.AddWorksheet(fecha3.ToString());
				formatearFilaPrincipalHoja1(sl, fecha3);
				contador = 1;
				foreach (DataRow fila in data.Rows)
				{
					sl.SetCellValue("A" + fila_excel, contador);
					sl.SetCellValue("B" + fila_excel, fila.Field<object>("refProducto").ToString());
					SLStyle aux_style = sl.CreateStyle();
					asignarBordes(aux_style);
					sl.SetCellStyle("A" + fila_excel, aux_style);
					sl.SetCellStyle("B" + fila_excel, aux_style);
					SLStyle style = sl.CreateStyle();
					style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
					style.SetVerticalAlignment(VerticalAlignmentValues.Center);
					asignarBordes(style);
					sl.SetCellStyle("A" + fila_excel, style);
					sl.SetCellStyle("B" + fila_excel, style);
					sl.SetCellStyle("D" + fila_excel, style);
					sl.SetCellValue("C" + fila_excel, fila.Field<object>("descripProducto").ToString());
					sl.SetCellValue("D" + fila_excel, fila.Field<object>("descripUnidad").ToString());
					sl.SetCellValue("E" + fila_excel, fila.Field<object>("fechaInicioDespachar").ToString());
					sl.SetCellStyle("E" + fila_excel, style);
					style = sl.CreateStyle();
					style.SetVerticalAlignment(VerticalAlignmentValues.Center);
					style.SetWrapText(IsWrapped: true);
					asignarBordes(style);
					sl.SetCellStyle("C" + fila_excel, style);
					sl.SetCellStyle("E" + fila_excel, style);
					sl.SetCellValue("F" + fila_excel, fila.Field<object>("fechaFinDespachar").ToString());
					style = sl.CreateStyle();
					asignarBordes(style);
					sl.SetCellStyle("F" + fila_excel, style);
					sl.SetCellValue("G" + fila_excel, fila.Field<object>("ctdadDespachar").ToString());
					style = sl.CreateStyle();
					style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
					style.SetVerticalAlignment(VerticalAlignmentValues.Center);
					asignarBordes(style);
					sl.SetCellStyle("G" + fila_excel, style);
					fila_excel++;
				}
				Cursor = Cursors.Default;
				try
				{
					DateTime _aux_date = DateTime.Now;
					string _fecha = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
					string cadenaGuardado = obtenerRutaParaGuardar("Exportacion_Reporte_Resumen_Productos_Pendientes_" + _fecha);
					if (cadenaGuardado != null)
					{
						sl.SaveAs(cadenaGuardado);
						Process.Start("explorer.exe", cadenaGuardado);
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Reporte Resumen Excel");
					return;
				}
			}
			MessageBox.Show("No hay datos que exportar", "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.ToString(), ex2.Message);
		}
	}

	private void formatearFilaPrincipalHoja(SLDocument sl, string fecha)
	{
		sl.SetCellValue("A1", "REPORTE RESUMEN PRODUCTOS PENDIENTES " + fecha);
		sl.MergeWorksheetCells("A1", "K1");
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 2, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellStyle("A1", "K1", style);
		sl.SetCellStyle("A2", "K2", style);
		SLStyle style_center = sl.CreateStyle();
		style_center.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style_center.SetVerticalAlignment(VerticalAlignmentValues.Center);
		SLStyle vertical_centrado = sl.CreateStyle();
		vertical_centrado.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("A2", "ITEM");
		sl.SetColumnWidth(1, 5.0);
		sl.SetCellValue("B2", "REFERENCIA");
		sl.SetColumnWidth(2, 14.0);
		sl.SetCellValue("C2", "DESCRIPCION");
		sl.SetColumnWidth(3, 39.0);
		sl.SetCellValue("D2", "UNIDAD");
		sl.SetColumnWidth(4, 11.0);
		sl.SetCellValue("E2", "DESPACHO");
		sl.SetColumnStyle(5, vertical_centrado);
		sl.SetColumnWidth(5, 22.0);
		sl.SetCellValue("F2", "REQUERIMIENTO");
		sl.SetColumnWidth(6, 20.0);
		sl.SetColumnStyle(6, style_center);
		sl.SetCellValue("G2", "DOC. RELACIONADO");
		sl.SetColumnWidth(7, 19.0);
		sl.SetColumnStyle(7, style_center);
		sl.SetCellValue("H2", "FECHA DOC. RELACIONADO");
		sl.SetColumnWidth(8, 25.5);
		sl.SetColumnStyle(8, vertical_centrado);
		sl.SetCellValue("I2", "CLIENTE");
		sl.SetColumnWidth(9, 39.0);
		sl.SetColumnStyle(9, vertical_centrado);
		sl.SetCellValue("J2", "ALMACEN DESPACHADOR");
		sl.SetColumnWidth(10, 24.0);
		sl.SetCellValue("K2", "CANTIDAD");
		sl.SetColumnWidth(11, 10.0);
		sl.SetColumnStyle(11, vertical_centrado);
	}

	private void formatearFilaPrincipalHoja1(SLDocument sl, string fecha)
	{
		sl.SetCellValue("A1", "REPORTE RESUMEN PRODUCTOS PENDIENTES " + fecha);
		sl.MergeWorksheetCells("A1", "G1");
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 2, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellStyle("A1", "G1", style);
		sl.SetCellStyle("A2", "G2", style);
		SLStyle style_center = sl.CreateStyle();
		style_center.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style_center.SetVerticalAlignment(VerticalAlignmentValues.Center);
		SLStyle vertical_centrado = sl.CreateStyle();
		vertical_centrado.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("A2", "ITEM");
		sl.SetColumnWidth(1, 5.0);
		sl.SetCellValue("B2", "REFERENCIA");
		sl.SetColumnWidth(2, 14.0);
		sl.SetCellValue("C2", "DESCRIPCION");
		sl.SetColumnWidth(3, 56.0);
		sl.SetCellValue("D2", "UNIDAD");
		sl.SetColumnWidth(4, 12.5);
		sl.SetCellValue("E2", "Fecha Primer Documento a Despachar");
		sl.SetColumnStyle(5, vertical_centrado);
		sl.SetColumnWidth(5, 35.0);
		sl.SetCellValue("F2", "Fecha Ultimo Documento a Despachar");
		sl.SetColumnWidth(6, 35.0);
		sl.SetColumnStyle(6, style_center);
		sl.SetCellValue("G2", "CANTIDAD");
		sl.SetColumnWidth(7, 10.0);
		sl.SetColumnStyle(7, vertical_centrado);
	}

	private void asignarBordes(SLStyle style)
	{
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.LeftBorder.Color = System.Drawing.Color.Black;
		style.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.TopBorder.Color = System.Drawing.Color.Black;
		style.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.RightBorder.Color = System.Drawing.Color.Black;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.BottomBorder.Color = System.Drawing.Color.Black;
	}

	private void btnReporteDetallado_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvProductos.RowCount > 0)
			{
				Cursor = Cursors.WaitCursor;
				SLDocument sl = new SLDocument();
				DateTime fecha1 = dtpDesde.Value;
				DateTime fecha2 = dtpHasta.Value;
				int tipoFecha = cmbTipoFecha.SelectedIndex;
				int codAlmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
				int codSucursal = frmLogin.iCodSucursal;
				DataTable data = admForm.reporteDetalladoListadoProductosADespachar(tipoFecha, fecha1, fecha2, codAlmacen, codSucursal);
				if (data.Rows.Count <= 0)
				{
					return;
				}
				int i = 0;
				int fila_excel = 3;
				int contador = 1;
				DateTime aux_date = fecha1;
				string fecha3 = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
				aux_date = fecha2;
				fecha3 = fecha3 + "_A_" + aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
				fecha3 = ((tipoFecha == -1) ? "TODOS" : fecha3);
				sl.AddWorksheet(fecha3.ToString());
				formatearFilaPrincipalHoja(sl, fecha3);
				contador = 1;
				foreach (DataRow fila in data.Rows)
				{
					sl.SetCellValue("A" + fila_excel, contador);
					sl.SetCellValue("B" + fila_excel, fila.Field<object>("refProducto").ToString());
					SLStyle aux_style = sl.CreateStyle();
					asignarBordes(aux_style);
					sl.SetCellStyle("A" + fila_excel, aux_style);
					sl.SetCellStyle("B" + fila_excel, aux_style);
					SLStyle style = sl.CreateStyle();
					style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
					style.SetVerticalAlignment(VerticalAlignmentValues.Center);
					asignarBordes(style);
					sl.SetCellStyle("A" + fila_excel, style);
					sl.SetCellStyle("B" + fila_excel, style);
					sl.SetCellStyle("D" + fila_excel, style);
					sl.SetCellValue("C" + fila_excel, fila.Field<object>("descripProducto").ToString());
					sl.SetCellValue("D" + fila_excel, fila.Field<object>("descripUnidad").ToString());
					sl.SetCellValue("E" + fila_excel, (fila.Field<object>("descripDocumento") == null) ? "" : fila.Field<object>("descripDocumento").ToString());
					sl.SetCellStyle("E" + fila_excel, style);
					style = sl.CreateStyle();
					style.SetVerticalAlignment(VerticalAlignmentValues.Center);
					style.SetWrapText(IsWrapped: true);
					asignarBordes(style);
					sl.SetCellStyle("C" + fila_excel, style);
					sl.SetCellStyle("E" + fila_excel, style);
					sl.SetCellValue("F" + fila_excel, fila.Field<object>("descripReqAlm").ToString());
					style = sl.CreateStyle();
					asignarBordes(style);
					sl.SetCellStyle("F" + fila_excel, style);
					sl.SetCellValue("G" + fila_excel, (fila.Field<object>("descripDocRelac") == null) ? "" : fila.Field<object>("descripDocRelac").ToString());
					style = sl.CreateStyle();
					style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
					style.SetVerticalAlignment(VerticalAlignmentValues.Center);
					asignarBordes(style);
					sl.SetCellStyle("G" + fila_excel, style);
					DateTime aux_date_1 = Convert.ToDateTime(fila.Field<object>("fechaDocumento").ToString()).Date;
					string fecha_1 = aux_date_1.Day.ToString().PadLeft(2, '0') + "-" + aux_date_1.Month.ToString().PadLeft(2, '0') + "-" + aux_date_1.Year;
					sl.SetCellValue("H" + fila_excel, fecha_1);
					sl.SetCellStyle("H" + fila_excel, style);
					style = sl.CreateStyle();
					asignarBordes(style);
					style.SetWrapText(IsWrapped: true);
					sl.SetCellStyle("H" + fila_excel, style);
					style = sl.CreateStyle();
					asignarBordes(style);
					sl.SetCellValue("I" + fila_excel, (fila.Field<object>("nombreCliente") == null) ? "" : fila.Field<object>("nombreCliente").ToString());
					sl.SetCellStyle("I" + fila_excel, style);
					sl.SetCellValue("J" + fila_excel, fila.Field<object>("descripAlmacen").ToString());
					sl.SetCellStyle("J" + fila_excel, style);
					style = sl.CreateStyle();
					style.FormatCode = "#,##0.00";
					asignarBordes(style);
					sl.SetCellValue("K" + fila_excel, fila.Field<object>("ctdadProducto").ToString());
					sl.SetCellStyle("K" + fila_excel, style);
					fila_excel++;
				}
				Cursor = Cursors.Default;
				try
				{
					DateTime _aux_date = DateTime.Now;
					string _fecha = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
					string cadenaGuardado = obtenerRutaParaGuardar("Exportacion_Reporte_Detallados_Productos_Pendientes_" + _fecha);
					if (cadenaGuardado != null)
					{
						sl.SaveAs(cadenaGuardado);
						Process.Start("explorer.exe", cadenaGuardado);
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Reporte Resumen Excel");
					return;
				}
			}
			MessageBox.Show("No hay datos que exportar", "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.ToString(), ex2.Message);
		}
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
	{
		btnBusqueda.PerformClick();
	}

	private string obtenerRutaParaGuardar(string namefile = "Exportacion_Reporte_Excel")
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
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Ventas Diarias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbTipoFecha = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvProductos = new Telerik.WinControls.UI.RadGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.rgvDocumentos = new Telerik.WinControls.UI.RadGridView();
		this.label4 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.btnReporteResumen = new System.Windows.Forms.Button();
		this.btnReporteDetallado = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvProductos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvProductos.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnReporteDetallado);
		this.groupBox1.Controls.Add(this.btnReporteResumen);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.cmbTipoFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1334, 97);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.cmbTipoFecha.FormattingEnabled = true;
		this.cmbTipoFecha.Items.AddRange(new object[2] { "Fecha Primer Documento Despachar", "Fecha Segundo Documento Despachar" });
		this.cmbTipoFecha.Location = new System.Drawing.Point(94, 19);
		this.cmbTipoFecha.Name = "cmbTipoFecha";
		this.cmbTipoFecha.Size = new System.Drawing.Size(219, 21);
		this.cmbTipoFecha.TabIndex = 53;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(79, 13);
		this.label1.TabIndex = 52;
		this.label1.Text = "Tipo de Fecha:";
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.cambio;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.Location = new System.Drawing.Point(716, 24);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(118, 36);
		this.btnBusqueda.TabIndex = 51;
		this.btnBusqueda.Text = "Recargar Lista";
		this.btnBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(515, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 50;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(319, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 49;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(372, 19);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 48;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(562, 18);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 47;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.rgvProductos);
		this.groupBox2.Location = new System.Drawing.Point(3, 103);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1331, 176);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.rgvProductos.AutoScroll = true;
		this.rgvProductos.AutoSizeRows = true;
		this.rgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvProductos.EnableGestures = false;
		this.rgvProductos.Location = new System.Drawing.Point(3, 16);
		this.rgvProductos.MasterTemplate.AllowAddNewRow = false;
		this.rgvProductos.MasterTemplate.AllowColumnChooser = false;
		this.rgvProductos.MasterTemplate.AllowColumnReorder = false;
		this.rgvProductos.MasterTemplate.AllowDeleteRow = false;
		this.rgvProductos.MasterTemplate.AllowDragToGroup = false;
		this.rgvProductos.MasterTemplate.AllowEditRow = false;
		this.rgvProductos.MasterTemplate.AutoGenerateColumns = false;
		this.rgvProductos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codProducto";
		gridViewTextBoxColumn1.HeaderText = "codProducto";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodProducto";
		gridViewTextBoxColumn1.Width = 1161;
		gridViewTextBoxColumn2.FieldName = "refProducto";
		gridViewTextBoxColumn2.HeaderText = "Referencia";
		gridViewTextBoxColumn2.Name = "colRefProducto";
		gridViewTextBoxColumn2.Width = 184;
		gridViewTextBoxColumn3.FieldName = "descripProducto";
		gridViewTextBoxColumn3.HeaderText = "Descripcion";
		gridViewTextBoxColumn3.Name = "colDescripProducto";
		gridViewTextBoxColumn3.Width = 424;
		gridViewTextBoxColumn4.FieldName = "codUnidad";
		gridViewTextBoxColumn4.HeaderText = "codUnidad";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "colCodUnidad";
		gridViewTextBoxColumn5.FieldName = "descripUnidad";
		gridViewTextBoxColumn5.HeaderText = "Unidad";
		gridViewTextBoxColumn5.Name = "colDescripUnidad";
		gridViewTextBoxColumn5.Width = 184;
		gridViewTextBoxColumn6.FieldName = "fechaInicioDespachar";
		gridViewTextBoxColumn6.HeaderText = "Fecha Primer Documento Despachar";
		gridViewTextBoxColumn6.Name = "colFechaInicioDespachar";
		gridViewTextBoxColumn6.Width = 198;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "fechaFinDespachar";
		gridViewTextBoxColumn7.HeaderText = "Fecha Ultimo Documento Despachar";
		gridViewTextBoxColumn7.Name = "colFechaFinDespachar";
		gridViewTextBoxColumn7.Width = 198;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "ctdadDespachar";
		gridViewTextBoxColumn8.HeaderText = "Cantidad a Despachar";
		gridViewTextBoxColumn8.Name = "colCtdadDespachar";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 141;
		gridViewTextBoxColumn8.WrapText = true;
		this.rgvProductos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8);
		this.rgvProductos.MasterTemplate.EnableFiltering = true;
		this.rgvProductos.MasterTemplate.EnableGrouping = false;
		this.rgvProductos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvProductos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvProductos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvProductos.Name = "rgvProductos";
		this.rgvProductos.ShowHeaderCellButtons = true;
		this.rgvProductos.Size = new System.Drawing.Size(1325, 157);
		this.rgvProductos.TabIndex = 1;
		this.rgvProductos.TitleText = "Listado de Productos a Despachar";
		this.rgvProductos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvProductos_CellClick);
		this.groupBox3.Controls.Add(this.rgvDocumentos);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 285);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1334, 198);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.rgvDocumentos.AutoScroll = true;
		this.rgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDocumentos.Location = new System.Drawing.Point(3, 16);
		this.rgvDocumentos.MasterTemplate.AllowAddNewRow = false;
		this.rgvDocumentos.MasterTemplate.AllowColumnReorder = false;
		this.rgvDocumentos.MasterTemplate.AllowDeleteRow = false;
		this.rgvDocumentos.MasterTemplate.AllowDragToGroup = false;
		this.rgvDocumentos.MasterTemplate.AllowEditRow = false;
		this.rgvDocumentos.MasterTemplate.AllowRowResize = false;
		this.rgvDocumentos.MasterTemplate.AutoGenerateColumns = false;
		this.rgvDocumentos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn9.FieldName = "codDocumento";
		gridViewTextBoxColumn9.HeaderText = "codDocumento";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "colCodDocumento";
		gridViewTextBoxColumn9.Width = 5;
		gridViewTextBoxColumn10.FieldName = "codTipoDocumento";
		gridViewTextBoxColumn10.HeaderText = "codTipoDocumento";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "colCodTipoDocumento";
		gridViewTextBoxColumn10.Width = 5;
		gridViewTextBoxColumn11.FieldName = "descripDocumento";
		gridViewTextBoxColumn11.HeaderText = "Despacho";
		gridViewTextBoxColumn11.Name = "colDescripDocumento";
		gridViewTextBoxColumn11.Width = 148;
		gridViewTextBoxColumn12.FieldName = "codReqAlm";
		gridViewTextBoxColumn12.HeaderText = "codReqAlm";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "colCodReqAlm";
		gridViewTextBoxColumn12.Width = 5;
		gridViewTextBoxColumn13.FieldName = "descripReqAlm";
		gridViewTextBoxColumn13.HeaderText = "Requerimiento";
		gridViewTextBoxColumn13.Name = "colDescReqAlm";
		gridViewTextBoxColumn13.Width = 139;
		gridViewTextBoxColumn14.FieldName = "codDocRelac";
		gridViewTextBoxColumn14.HeaderText = "codDocRelac";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "colCodDocRelac";
		gridViewTextBoxColumn14.Width = 5;
		gridViewTextBoxColumn15.FieldName = "descripDocRelac";
		gridViewTextBoxColumn15.HeaderText = "Doc. Relacionado";
		gridViewTextBoxColumn15.Name = "colDescDocRelac";
		gridViewTextBoxColumn15.Width = 131;
		gridViewTextBoxColumn16.FieldName = "fechaDocumento";
		gridViewTextBoxColumn16.HeaderText = "Fecha Doc. Relacionado";
		gridViewTextBoxColumn16.Name = "colFechaDocumento";
		gridViewTextBoxColumn16.Width = 158;
		gridViewTextBoxColumn17.FieldName = "nombreCliente";
		gridViewTextBoxColumn17.HeaderText = "Cliente";
		gridViewTextBoxColumn17.Name = "colNombreCliente";
		gridViewTextBoxColumn17.Width = 345;
		gridViewTextBoxColumn18.FieldName = "fechaRegistro";
		gridViewTextBoxColumn18.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn18.Name = "colFechaRegistro";
		gridViewTextBoxColumn18.Width = 134;
		gridViewTextBoxColumn19.FieldName = "descripAlmacen";
		gridViewTextBoxColumn19.HeaderText = "Almacen";
		gridViewTextBoxColumn19.Name = "colDescripAlmacen";
		gridViewTextBoxColumn19.Width = 127;
		gridViewTextBoxColumn19.WrapText = true;
		gridViewTextBoxColumn20.FieldName = "ctdadProducto";
		gridViewTextBoxColumn20.HeaderText = "Cantidad De Producto";
		gridViewTextBoxColumn20.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn20.Name = "colCtdadProducto";
		gridViewTextBoxColumn20.Width = 152;
		gridViewTextBoxColumn20.WrapText = true;
		gridViewTextBoxColumn21.FieldName = "codAlmacenes";
		gridViewTextBoxColumn21.HeaderText = "codAlmacenes";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "colCodAlmacenes";
		gridViewTextBoxColumn21.Width = 5;
		this.rgvDocumentos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21);
		this.rgvDocumentos.MasterTemplate.EnableFiltering = true;
		this.rgvDocumentos.MasterTemplate.EnableGrouping = false;
		this.rgvDocumentos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDocumentos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDocumentos.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvDocumentos.Name = "rgvDocumentos";
		this.rgvDocumentos.ReadOnly = true;
		this.rgvDocumentos.ShowHeaderCellButtons = true;
		this.rgvDocumentos.Size = new System.Drawing.Size(1328, 179);
		this.rgvDocumentos.TabIndex = 0;
		this.rgvDocumentos.TitleText = "Listado Documentos";
		this.rgvDocumentos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDocumentos_CellDoubleClick);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(20, 50);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(51, 13);
		this.label4.TabIndex = 58;
		this.label4.Text = "Almacen:";
		this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbAlmacenes.Location = new System.Drawing.Point(94, 46);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(182, 21);
		this.cmbAlmacenes.TabIndex = 57;
		this.cmbAlmacenes.SelectedIndexChanged += new System.EventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.btnReporteResumen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporteResumen.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporteResumen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporteResumen.Location = new System.Drawing.Point(913, 22);
		this.btnReporteResumen.Name = "btnReporteResumen";
		this.btnReporteResumen.Size = new System.Drawing.Size(117, 41);
		this.btnReporteResumen.TabIndex = 61;
		this.btnReporteResumen.Text = "Reporte Resumen";
		this.btnReporteResumen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporteResumen.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.btnReporteResumen.UseVisualStyleBackColor = true;
		this.btnReporteResumen.Click += new System.EventHandler(btnReporteResumen_Click);
		this.btnReporteDetallado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporteDetallado.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporteDetallado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporteDetallado.Location = new System.Drawing.Point(1036, 22);
		this.btnReporteDetallado.Name = "btnReporteDetallado";
		this.btnReporteDetallado.Size = new System.Drawing.Size(118, 41);
		this.btnReporteDetallado.TabIndex = 62;
		this.btnReporteDetallado.Text = "Reporte Detallado";
		this.btnReporteDetallado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporteDetallado.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.btnReporteDetallado.UseVisualStyleBackColor = true;
		this.btnReporteDetallado.Click += new System.EventHandler(btnReporteDetallado_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1334, 483);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListaProductoDespachar";
		this.Text = "Listado De Productos A Despachar";
		base.Load += new System.EventHandler(frmListaProductoDespachar_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvProductos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvProductos).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos).EndInit();
		base.ResumeLayout(false);
	}
}
