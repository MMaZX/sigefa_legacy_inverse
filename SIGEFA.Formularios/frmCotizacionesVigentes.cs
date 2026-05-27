using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SpreadsheetLight;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmCotizacionesVigentes : Office2007Form
{
	public string mensajeanular = "";

	private bool flgloadcboanalisis = false;

	private clsAdmCotizacion AdmCotizacion = new clsAdmCotizacion();

	private clsCotizacion cotizacion = new clsCotizacion();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsCliente cli = new clsCliente();

	internal clsUsuario usuario_click = null;

	private clsDocumentosImpresos ds = new clsDocumentosImpresos();

	public string nombreArchivo = "";

	private IContainer components = null;

	private ImageList imageList1;

	private ImageList imageList2;

	private ImageList imageList3;

	private ImageList imageList4;

	private Button btnEnviar;

	private Label label4;

	private Label label8;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private Button btnduplicar;

	private Button btnactualizar;

	private Button btnvalidar;

	private Label label7;

	private ComboBox cmbVigente;

	private Label label3;

	private Label label1;

	private Label label2;

	private Label label10;

	private TextBox txtFiltro;

	private Button btnConsultar;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnAnular;

	private Button btGenVenta;

	private Button btnIrCotizacion;

	private Button btnSalir;

	private RadGridView rgvDatos;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private GroupBox Acciones;

	private ComboBox cbo_Analisis;

	private Label label9;

	private ComboBox cmbAlmacen;

	private Label label11;

	private GroupBox groupBox1;

	private RadGridView rgvDatosLita;

	public frmCotizacionesVigentes()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		rgvDatosLita.DataSource = data;
		data.DataSource = AdmCotizacion.MuestraCotizaciones(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
		rgvDatosLita.ClearSelection();
	}

	private void btnIrCotizacion_Click(object sender, EventArgs e)
	{
		if (rgvDatosLita.Rows.Count >= 1 && rgvDatosLita.CurrentRow != null)
		{
			frmGestionCotizacion form = new frmGestionCotizacion();
			form.MdiParent = base.MdiParent;
			form.CodCotizacion = cotizacion.CodCotizacion;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void frmCotizacionesVigentes_Load(object sender, EventArgs e)
	{
		label7.Text = "Cliente";
		label1.Text = "Cliente";
		cmbVigente.SelectedIndex = 1;
		int usuario = frmLogin.iNivelUser;
		if (usuario != 1)
		{
			btnvalidar.Visible = false;
		}
		else
		{
			btnvalidar.Visible = true;
		}
		CargaAnalisis();
		flgloadcboanalisis = true;
		cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		consultar();
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

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (rgvDatosLita.Rows.Count >= 1 && rgvDatosLita.CurrentRow != null)
		{
			frmOrdenCompraCotizacion form = new frmOrdenCompraCotizacion();
			form.MdiParent = base.MdiParent;
			form.CodCotizacion = cotizacion.CodCotizacion;
			form.Proceso = 1;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvDatosLita.CurrentRow == null || !(cotizacion.CodCotizacion != ""))
			{
				return;
			}
			if (frmLogin.iNivelUser == 100)
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la cotizacion seleccionada", "Cotizaciones Vigentes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.No)
				{
					return;
				}
				MensajeAnular();
				if (mensajeanular != "")
				{
					if (AdmCotizacion.delete(Convert.ToInt32(cotizacion.CodCotizacion), frmLogin.iCodUser, mensajeanular))
					{
						MessageBox.Show("La cotizacion ha sido anulada correctamente", "Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						consultar();
					}
				}
				else
				{
					MessageBox.Show("La cotizacion no ha sido anulada", "Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					consultar();
				}
				return;
			}
			DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea anular la cotizacion seleccionada", "Cotizaciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult2 == DialogResult.No)
			{
				return;
			}
			usuario_click = null;
			frmAutorizacion frm = new frmAutorizacion();
			frm.tipoAccion = 3;
			frm.tipoVentanaAAsignarUsuario = 14;
			frm.VentanaListaCotizaciones = this;
			DialogResult dr = DialogResult.None;
			dr = frm.ShowDialog();
			if (dr != DialogResult.OK || usuario_click == null)
			{
				return;
			}
			MensajeAnular();
			if (mensajeanular != "")
			{
				if (AdmCotizacion.delete(Convert.ToInt32(cotizacion.CodCotizacion), usuario_click.CodUsuario, mensajeanular))
				{
					MessageBox.Show("La cotizacion ha sido anulada correctamente", "Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					consultar();
				}
				else
				{
					MessageBox.Show("Error al Anular Cotizacion", "Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show("La cotizacion no ha sido anulada", "Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				consultar();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void MensajeAnular()
	{
		frmmensajeanulacioncotizacion frm = new frmmensajeanulacioncotizacion();
		frm.ShowDialog();
		mensajeanular = frm.mensaje;
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label1.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void consultar()
	{
		rgvDatosLita.DataSource = data;
		data.DataSource = AdmCotizacion.MuestraCotizacionesxVigente(Convert.ToInt32(cmbAlmacen.SelectedValue), cmbVigente.SelectedIndex, dtpDesde.Value, dtpHasta.Value);
		data.Filter = string.Empty;
		filtro = string.Empty;
		rgvDatosLita.ClearSelection();
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		consultar();
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		consultar();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		consultar();
	}

	private void cmbVigente_SelectionChangeCommitted(object sender, EventArgs e)
	{
		consultar();
		if (cmbVigente.SelectedIndex == 1)
		{
			btnAnular.Visible = true;
			btnvalidar.Visible = true;
			btGenVenta.Visible = true;
			btnAnular.Enabled = true;
			btnvalidar.Enabled = true;
			btGenVenta.Enabled = true;
			btnduplicar.Enabled = false;
		}
		else if (cmbVigente.SelectedIndex == 2)
		{
			btnAnular.Enabled = false;
			btnvalidar.Enabled = false;
			btGenVenta.Enabled = false;
			btnduplicar.Enabled = true;
		}
		else if (cmbVigente.SelectedIndex == 2)
		{
			btnduplicar.Visible = true;
			btnduplicar.Enabled = false;
			btnAnular.Enabled = false;
			btnvalidar.Enabled = false;
			btGenVenta.Enabled = false;
			btnduplicar.Enabled = false;
		}
		else
		{
			btnAnular.Enabled = false;
			btnvalidar.Enabled = false;
			btGenVenta.Enabled = false;
			btnduplicar.Enabled = false;
		}
	}

	private void btnEnviar_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvDatosLita.CurrentRow == null || cmbVigente.SelectedIndex != 1)
			{
				return;
			}
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea enviar la Cotización", "Cotización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult == DialogResult.No)
			{
				return;
			}
			PdfRtfWordFormatOptions crformattype = new PdfRtfWordFormatOptions();
			DiskFileDestinationOptions dfoption = new DiskFileDestinationOptions();
			dfoption.DiskFileName = "C:\\Cotizaciones\\Cotizacion_" + cotizacion.CodCotizacion + ".pdf";
			ReportDocument document = new ReportDocument();
			CRCotizacion cot = new CRCotizacion();
			ExportOptions objexport = cot.ExportOptions;
			objexport.ExportDestinationType = ExportDestinationType.DiskFile;
			objexport.ExportFormatType = ExportFormatType.PortableDocFormat;
			objexport.DestinationOptions = dfoption;
			objexport.FormatOptions = crformattype;
			cot.Export();
			DirectoryInfo Dir = new DirectoryInfo("C:\\Cotizaciones");
			FileInfo[] files = Dir.GetFiles();
			foreach (FileInfo Fi in files)
			{
				if (Fi.Name.Contains(cotizacion.CodCotizacion))
				{
					nombreArchivo = Fi.Name;
				}
			}
			if (Application.OpenForms["frmCorreoElectronico"] != null)
			{
				Application.OpenForms["frmCorreoElectronico"].Activate();
				return;
			}
			frmCorreoElectronico form = new frmCorreoElectronico();
			form.link_adjunto.Text = nombreArchivo;
			form.txtcuerpo.Text = "ESTIMADOS SRs.: " + cotizacion.RazonSocialCliente + Environment.NewLine + Environment.NewLine + "\t LES ADJUNTO LA COTIZACIÓN.  N- " + cotizacion.CodCotizacion + "." + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "\t\t\t \t\t  ATT. " + Environment.NewLine + "\t\t\t\t" + frmLogin.sApellidoUSer + ", " + frmLogin.sNombreUser;
			form.tipo = 2;
			form.ShowDialog();
			if (form.enviado == 1)
			{
				MessageBox.Show("La Cotización ha envio correctamente", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				consultar();
			}
			else
			{
				MessageBox.Show("La Cotización, No se Pudo enviar, Verifique!", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				consultar();
			}
		}
		catch (Exception)
		{
			MessageBox.Show("No se Pudo Enviar la Cotización", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			consultar();
		}
	}

	private void frmCotizacionesVigentes_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
	}

	private void btnvalidar_Click(object sender, EventArgs e)
	{
		if (rgvDatosLita.Rows.Count < 1 || Convert.ToInt32(rgvDatosLita.CurrentRow.Cells["codigo"].Value) <= 0)
		{
			return;
		}
		int valida = Convert.ToInt32(rgvDatosLita.CurrentRow.Cells["validadescuentos"].Value);
		if (valida == 1)
		{
			MessageBox.Show("Cotización ya se encuentra Validada", "Validar Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		int codCotizacion = Convert.ToInt32(rgvDatosLita.CurrentRow.Cells["codigo"].Value);
		if (AdmCotizacion.ValidarDescuento(codCotizacion))
		{
			MessageBox.Show("Se realizo Validación", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		consultar();
	}

	private void btnactualizar_Click(object sender, EventArgs e)
	{
		consultar();
	}

	private void dgvCotizaciones_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void btnduplicar_Click(object sender, EventArgs e)
	{
		try
		{
			int codCotizacion = Convert.ToInt32(rgvDatosLita.CurrentRow.Cells["codCotizacion"].Value);
			if (AdmCotizacion.DuplicarCotizacion(codCotizacion))
			{
				MessageBox.Show("Cotización Duplicada ", "Listado de Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Cotización No Duplicada ", "Listado de Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			consultar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnConsultar_Click_1(object sender, EventArgs e)
	{
	}

	private void rgvDatosLita_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvDatosLita.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmGestionCotizacion form = new frmGestionCotizacion();
			form.MdiParent = base.MdiParent;
			form.CodCotizacion = cotizacion.CodCotizacion;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void rgvDatosLita_CellFormatting(object sender, CellFormattingEventArgs e)
	{
		try
		{
			if (rgvDatosLita.Columns[e.ColumnIndex].Name == "codigo")
			{
				double valida = Convert.ToDouble(rgvDatosLita.Rows[e.RowIndex].Cells["validadescuentos"].Value ?? ((object)0));
				if (valida == 0.0)
				{
					e.CellElement.BackColor = System.Drawing.Color.IndianRed;
				}
				else
				{
					e.CellElement.BackColor = System.Drawing.Color.LightGreen;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rgvDatosLita_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (rgvDatosLita.Rows.Count >= 1)
			{
				cotizacion.CodCotizacion = e.Row.Cells["codCotizacion"].Value.ToString();
				cotizacion.RazonSocialCliente = e.Row.Cells["cliente"].Value.ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			SLDocument sl = new SLDocument();
			if (rgvDatosLita.ChildRows.Count > 0)
			{
				int i = 0;
				int fila_excel = 4;
				int fila_a_concatenar = 0;
				int fila_first_concat = 0;
				int contador = 1;
				string desde = dtpDesde.Value.ToString();
				string hasta = dtpHasta.Value.ToString();
				sl.AddWorksheet("Listado de Cotizaciones");
				formatearFilaPrincipalHoja(sl, desde, hasta);
				contador = 1;
				DataTable table = new DataTable();
				foreach (GridViewDataColumn column in rgvDatosLita.Columns)
				{
					table.Columns.Add(column.Name, column.DataType);
				}
				foreach (GridViewRowInfo row in rgvDatosLita.MasterTemplate.DataView)
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
				MessageBox.Show(ex.Message, "ReporteCotizaciones");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, "Error");
		}
	}

	private string obtenerRutaParaGuardar(string namefile = "ReporteCotizaciones")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "ReporteCotizaciones";
			sfd.FileName = namefile + DateTime.Now.ToString("yyyy-MM-dd");
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		sl.SetCellValue("A1", "LISTA DE COTIZACIONES  ");
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
		sl.SetCellValue("B3", "Cotización");
		sl.SetColumnWidth(2, 20.0);
		sl.SetCellStyle("C3", style);
		sl.SetCellValue("C3", "Cliente");
		sl.SetColumnWidth(3, 30.0);
		sl.SetCellStyle("D3", style);
		sl.SetCellValue("D3", "SubTotal");
		sl.SetColumnWidth(4, 20.0);
		sl.SetCellStyle("E3", style);
		sl.SetCellValue("E3", "Igv");
		sl.SetColumnWidth(5, 20.0);
		sl.SetCellStyle("F3", style);
		sl.SetCellValue("F3", "Total Cotización");
		sl.SetColumnWidth(6, 25.0);
		sl.SetCellStyle("G3", style);
		sl.SetCellValue("G3", "Fecha Emisión");
		sl.SetColumnWidth(7, 20.0);
		sl.SetCellStyle("H3", style);
		sl.SetCellValue("H3", "Vig .Hasta");
		sl.SetColumnWidth(8, 20.0);
		sl.SetCellStyle("I3", style);
		sl.SetCellValue("I3", "Estado Proceso");
		sl.SetColumnWidth(9, 20.0);
		sl.SetCellStyle("J3", style);
		sl.SetCellValue("J3", "Cotización Duplicada");
		sl.SetColumnWidth(10, 15.0);
		sl.SetCellStyle("K3", style);
		sl.SetCellValue("K3", "Orden Compra Ref.");
		sl.SetColumnWidth(11, 20.0);
		sl.SetCellStyle("L3", style);
		sl.SetCellValue("L3", "Usuario");
		sl.SetColumnWidth(11, 20.0);
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
		sl.SetCellValue("B" + fila_excel, fila[2].ToString());
		sl.SetCellStyle("B" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("C" + fila_excel, fila[3].ToString());
		sl.SetCellStyle("C" + fila_excel, style);
		sl.SetCellValue("D" + fila_excel, (fila[4] != DBNull.Value) ? Convert.ToDecimal(fila[4]) : Convert.ToDecimal(0));
		sl.SetCellStyle("D" + fila_excel, style);
		sl.SetCellValue("E" + fila_excel, (fila[5] != DBNull.Value) ? Convert.ToDecimal(fila[5]) : Convert.ToDecimal(0));
		sl.SetCellStyle("E" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.General);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("F" + fila_excel, (fila[6] != DBNull.Value) ? Convert.ToDecimal(fila[6]) : Convert.ToDecimal(0));
		sl.SetCellStyle("F" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("G" + fila_excel, fila[7].ToString());
		sl.SetCellStyle("G" + fila_excel, style);
		sl.SetCellValue("H" + fila_excel, fila[8].ToString());
		sl.SetCellValue("I" + fila_excel, fila[9].ToString());
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, fila[10].ToString());
		sl.SetCellValue("K" + fila_excel, fila[11].ToString());
		sl.SetCellStyle("K" + fila_excel, style);
		sl.SetCellValue("L" + fila_excel, fila[12].ToString());
		sl.SetCellStyle("L" + fila_excel, style);
	}

	private void cbo_Analisis_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (flgloadcboanalisis)
		{
			cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
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
		this.components = new System.ComponentModel.Container();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCotizacionesVigentes));
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.rgvDatos = new Telerik.WinControls.UI.RadGridView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.imageList4 = new System.Windows.Forms.ImageList(this.components);
		this.btnEnviar = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.cbo_Analisis = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbVigente = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnConsultar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnduplicar = new System.Windows.Forms.Button();
		this.btnactualizar = new System.Windows.Forms.Button();
		this.btnvalidar = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrCotizacion = new System.Windows.Forms.Button();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.Acciones = new System.Windows.Forms.GroupBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvDatosLita = new Telerik.WinControls.UI.RadGridView();
		((System.ComponentModel.ISupportInitialize)this.rgvDatos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDatos.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.Acciones.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDatosLita).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDatosLita.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.rgvDatos.Location = new System.Drawing.Point(0, 0);
		this.rgvDatos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDatos.Name = "rgvDatos";
		this.rgvDatos.Size = new System.Drawing.Size(240, 150);
		this.rgvDatos.TabIndex = 0;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList2.Images.SetKeyName(1, "Add.png");
		this.imageList2.Images.SetKeyName(2, "Remove.png");
		this.imageList2.Images.SetKeyName(3, "Write Document.png");
		this.imageList2.Images.SetKeyName(4, "New Document.png");
		this.imageList2.Images.SetKeyName(5, "Remove Document.png");
		this.imageList2.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList2.Images.SetKeyName(7, "document-print.png");
		this.imageList2.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList2.Images.SetKeyName(9, "refresh_256.png");
		this.imageList2.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList2.Images.SetKeyName(11, "search (1).png");
		this.imageList2.Images.SetKeyName(12, "search (5).png");
		this.imageList2.Images.SetKeyName(13, "search (6).png");
		this.imageList2.Images.SetKeyName(14, "search (8).png");
		this.imageList2.Images.SetKeyName(15, "search_top.png");
		this.imageList2.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList2.Images.SetKeyName(17, "Folder open.png");
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Write Document.png");
		this.imageList3.Images.SetKeyName(1, "New Document.png");
		this.imageList3.Images.SetKeyName(2, "Remove Document.png");
		this.imageList3.Images.SetKeyName(3, "document-print.png");
		this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList3.Images.SetKeyName(5, "exit.png");
		this.imageList3.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList4.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList4.ImageStream");
		this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList4.Images.SetKeyName(0, "Write Document.png");
		this.imageList4.Images.SetKeyName(1, "New Document.png");
		this.imageList4.Images.SetKeyName(2, "Remove Document.png");
		this.imageList4.Images.SetKeyName(3, "document-print.png");
		this.imageList4.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList4.Images.SetKeyName(5, "exit.png");
		this.imageList4.Images.SetKeyName(6, "search (1).png");
		this.imageList4.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList4.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList4.Images.SetKeyName(9, "document_delete.png");
		this.imageList4.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList4.Images.SetKeyName(11, "OK_Verde.png");
		this.btnEnviar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnEnviar.Enabled = false;
		this.btnEnviar.ImageIndex = 17;
		this.btnEnviar.ImageList = this.imageList2;
		this.btnEnviar.Location = new System.Drawing.Point(894, 12);
		this.btnEnviar.Name = "btnEnviar";
		this.btnEnviar.Size = new System.Drawing.Size(22, 10);
		this.btnEnviar.TabIndex = 46;
		this.btnEnviar.Text = "Enviar";
		this.btnEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEnviar.UseVisualStyleBackColor = true;
		this.btnEnviar.Visible = false;
		this.btnEnviar.Click += new System.EventHandler(btnEnviar_Click);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Lime;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.label4.Location = new System.Drawing.Point(4, 3);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(149, 16);
		this.label4.TabIndex = 48;
		this.label4.Text = "Descuento Validado";
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Red;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.SystemColors.Control;
		this.label8.Location = new System.Drawing.Point(182, 3);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(147, 16);
		this.label8.TabIndex = 49;
		this.label8.Text = "Descuento x Validar";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.btnEnviar);
		this.groupBox2.Location = new System.Drawing.Point(10, 412);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1418, 22);
		this.groupBox2.TabIndex = 50;
		this.groupBox2.TabStop = false;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.cmbAlmacen);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.cbo_Analisis);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.cmbVigente);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.label1);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.txtFiltro);
		this.groupBox3.Controls.Add(this.btnConsultar);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Controls.Add(this.dtpDesde);
		this.groupBox3.Controls.Add(this.dtpHasta);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Location = new System.Drawing.Point(7, 1);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1424, 54);
		this.groupBox3.TabIndex = 51;
		this.groupBox3.TabStop = false;
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(1059, 21);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(181, 21);
		this.cmbAlmacen.TabIndex = 92;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label11.Location = new System.Drawing.Point(1000, 24);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(62, 13);
		this.label11.TabIndex = 91;
		this.label11.Text = "Almacén: ";
		this.cbo_Analisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbo_Analisis.FormattingEnabled = true;
		this.cbo_Analisis.Location = new System.Drawing.Point(773, 19);
		this.cbo_Analisis.Name = "cbo_Analisis";
		this.cbo_Analisis.Size = new System.Drawing.Size(163, 21);
		this.cbo_Analisis.TabIndex = 90;
		this.cbo_Analisis.SelectedIndexChanged += new System.EventHandler(cbo_Analisis_SelectedIndexChanged);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label9.Location = new System.Drawing.Point(718, 23);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(59, 13);
		this.label9.TabIndex = 89;
		this.label9.Text = "Análisis : ";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(445, 14);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(14, 13);
		this.label7.TabIndex = 68;
		this.label7.Text = "X";
		this.cmbVigente.FormattingEnabled = true;
		this.cmbVigente.Items.AddRange(new object[5] { "TODOS", "VIGENTE", "VENCIDA", "DUPLICADO", "CON OC." });
		this.cmbVigente.Location = new System.Drawing.Point(247, 29);
		this.cmbVigente.Name = "cmbVigente";
		this.cmbVigente.Size = new System.Drawing.Size(121, 21);
		this.cmbVigente.TabIndex = 67;
		this.cmbVigente.SelectionChangeCommitted += new System.EventHandler(cmbVigente_SelectionChangeCommitted);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(244, 13);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(46, 13);
		this.label3.TabIndex = 66;
		this.label3.Text = "Estado :";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label1.Location = new System.Drawing.Point(574, 14);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(12, 13);
		this.label1.TabIndex = 65;
		this.label1.Text = "x";
		this.label1.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label2.Location = new System.Drawing.Point(410, 13);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(29, 13);
		this.label2.TabIndex = 64;
		this.label2.Text = "Por :";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label10.Location = new System.Drawing.Point(375, 13);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(29, 13);
		this.label10.TabIndex = 63;
		this.label10.Text = "Filtro";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(378, 29);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 62;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnConsultar.Image = (System.Drawing.Image)resources.GetObject("btnConsultar.Image");
		this.btnConsultar.Location = new System.Drawing.Point(597, 15);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(111, 31);
		this.btnConsultar.TabIndex = 61;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Visible = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click_1);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(124, 13);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 60;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(4, 13);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 59;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(7, 30);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 58;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(127, 30);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 57;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(1313, 9);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(103, 37);
		this.btnSalir.TabIndex = 53;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnduplicar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnduplicar.BackColor = System.Drawing.Color.Transparent;
		this.btnduplicar.Enabled = false;
		this.btnduplicar.Image = (System.Drawing.Image)resources.GetObject("btnduplicar.Image");
		this.btnduplicar.Location = new System.Drawing.Point(6, 194);
		this.btnduplicar.Name = "btnduplicar";
		this.btnduplicar.Size = new System.Drawing.Size(103, 37);
		this.btnduplicar.TabIndex = 71;
		this.btnduplicar.Text = "Duplicar";
		this.btnduplicar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnduplicar.UseVisualStyleBackColor = false;
		this.btnduplicar.Click += new System.EventHandler(btnduplicar_Click);
		this.btnactualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnactualizar.Image = (System.Drawing.Image)resources.GetObject("btnactualizar.Image");
		this.btnactualizar.Location = new System.Drawing.Point(6, 65);
		this.btnactualizar.Name = "btnactualizar";
		this.btnactualizar.Size = new System.Drawing.Size(103, 37);
		this.btnactualizar.TabIndex = 70;
		this.btnactualizar.Text = "Actualizar";
		this.btnactualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnactualizar.UseVisualStyleBackColor = true;
		this.btnactualizar.Click += new System.EventHandler(btnactualizar_Click);
		this.btnvalidar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnvalidar.Image = (System.Drawing.Image)resources.GetObject("btnvalidar.Image");
		this.btnvalidar.Location = new System.Drawing.Point(6, 151);
		this.btnvalidar.Name = "btnvalidar";
		this.btnvalidar.Size = new System.Drawing.Size(103, 37);
		this.btnvalidar.TabIndex = 69;
		this.btnvalidar.Text = " Validar Descuento";
		this.btnvalidar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnvalidar.UseVisualStyleBackColor = true;
		this.btnvalidar.Visible = false;
		this.btnvalidar.Click += new System.EventHandler(btnvalidar_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.Image = (System.Drawing.Image)resources.GetObject("btnAnular.Image");
		this.btnAnular.Location = new System.Drawing.Point(6, 237);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(103, 37);
		this.btnAnular.TabIndex = 56;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.Image = (System.Drawing.Image)resources.GetObject("btGenVenta.Image");
		this.btGenVenta.Location = new System.Drawing.Point(6, 108);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(103, 37);
		this.btGenVenta.TabIndex = 55;
		this.btGenVenta.Text = "Generar O.Compra";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnIrCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrCotizacion.Image = (System.Drawing.Image)resources.GetObject("btnIrCotizacion.Image");
		this.btnIrCotizacion.Location = new System.Drawing.Point(6, 19);
		this.btnIrCotizacion.Name = "btnIrCotizacion";
		this.btnIrCotizacion.Size = new System.Drawing.Size(103, 37);
		this.btnIrCotizacion.TabIndex = 54;
		this.btnIrCotizacion.Text = "Ir Cotizacion";
		this.btnIrCotizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrCotizacion.UseVisualStyleBackColor = true;
		this.btnIrCotizacion.Click += new System.EventHandler(btnIrCotizacion_Click);
		this.Acciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.Acciones.Controls.Add(this.btGenVenta);
		this.Acciones.Controls.Add(this.btnduplicar);
		this.Acciones.Controls.Add(this.btnvalidar);
		this.Acciones.Controls.Add(this.btnAnular);
		this.Acciones.Controls.Add(this.btnactualizar);
		this.Acciones.Controls.Add(this.btnIrCotizacion);
		this.Acciones.Location = new System.Drawing.Point(1314, 57);
		this.Acciones.Name = "Acciones";
		this.Acciones.Size = new System.Drawing.Size(116, 352);
		this.Acciones.TabIndex = 52;
		this.Acciones.TabStop = false;
		this.Acciones.Text = "Acciones";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvDatosLita);
		this.groupBox1.Location = new System.Drawing.Point(7, 57);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1301, 352);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Vigentes";
		this.rgvDatosLita.BackColor = System.Drawing.Color.White;
		this.rgvDatosLita.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvDatosLita.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDatosLita.Font = new System.Drawing.Font("Segoe UI", 8.25f);
		this.rgvDatosLita.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvDatosLita.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvDatosLita.Location = new System.Drawing.Point(3, 16);
		this.rgvDatosLita.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.FieldName = "item";
		gridViewTextBoxColumn1.HeaderText = "N°";
		gridViewTextBoxColumn1.Name = "item";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.AllowReorder = false;
		gridViewTextBoxColumn2.AllowSort = false;
		gridViewTextBoxColumn2.FieldName = "fecha";
		gridViewTextBoxColumn2.HeaderText = "FECHA";
		gridViewTextBoxColumn2.Name = "fecha";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 100;
		gridViewTextBoxColumn3.FieldName = "almacen";
		gridViewTextBoxColumn3.HeaderText = "ALMACÉN";
		gridViewTextBoxColumn3.Name = "almacen";
		gridViewTextBoxColumn3.Width = 120;
		gridViewTextBoxColumn4.FieldName = "Cotizacion";
		gridViewTextBoxColumn4.HeaderText = "COTIZACIÓN";
		gridViewTextBoxColumn4.Name = "Cotizacion";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 110;
		gridViewTextBoxColumn5.FieldName = "cliente";
		gridViewTextBoxColumn5.HeaderText = "CLIENTE";
		gridViewTextBoxColumn5.Name = "cliente";
		gridViewTextBoxColumn5.Width = 300;
		gridViewTextBoxColumn6.FieldName = "subtotal";
		gridViewTextBoxColumn6.HeaderText = "SUB TOTAL";
		gridViewTextBoxColumn6.Name = "subtotal";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 100;
		gridViewTextBoxColumn7.FieldName = "igv";
		gridViewTextBoxColumn7.HeaderText = "IGV";
		gridViewTextBoxColumn7.Name = "igv";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 100;
		gridViewTextBoxColumn8.FieldName = "total";
		gridViewTextBoxColumn8.HeaderText = "TOTAL";
		gridViewTextBoxColumn8.Name = "total";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 110;
		gridViewTextBoxColumn9.FieldName = "fechavigencia";
		gridViewTextBoxColumn9.HeaderText = "FECHA VIGENCIA";
		gridViewTextBoxColumn9.Name = "fechavigencia";
		gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn9.Width = 150;
		gridViewTextBoxColumn10.FieldName = "estado";
		gridViewTextBoxColumn10.HeaderText = "ESTADO DE PROCESO";
		gridViewTextBoxColumn10.Name = "estado";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 150;
		gridViewTextBoxColumn11.FieldName = "estado_comercial";
		gridViewTextBoxColumn11.HeaderText = "ESTADO COMERCIAL";
		gridViewTextBoxColumn11.Name = "estado_comercial";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 135;
		gridViewTextBoxColumn12.FieldName = "CotizacionAsociada";
		gridViewTextBoxColumn12.HeaderText = "COTIZACION DUPLICADA";
		gridViewTextBoxColumn12.Name = "CotizacionAsociada";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 160;
		gridViewTextBoxColumn13.FieldName = "DocRefe";
		gridViewTextBoxColumn13.HeaderText = "ORDEN COMPRA REFERENCIA";
		gridViewTextBoxColumn13.Name = "DocRefe";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 150;
		gridViewTextBoxColumn14.FieldName = "responsable";
		gridViewTextBoxColumn14.HeaderText = "VENDEDOR";
		gridViewTextBoxColumn14.Name = "responsable";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 150;
		gridViewTextBoxColumn15.FieldName = "codCotizacion";
		gridViewTextBoxColumn15.HeaderText = "N° Cotización";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "codCotizacion";
		gridViewTextBoxColumn15.ReadOnly = true;
		gridViewTextBoxColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn15.Width = 150;
		gridViewTextBoxColumn16.FieldName = "validadescuentos";
		gridViewTextBoxColumn16.HeaderText = "validadescuentos";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "validadescuentos";
		gridViewTextBoxColumn17.FieldName = "documento";
		gridViewTextBoxColumn17.HeaderText = "T. doc.";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "documento";
		this.rgvDatosLita.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17);
		this.rgvDatosLita.MasterTemplate.EnableFiltering = true;
		this.rgvDatosLita.MasterTemplate.EnableGrouping = false;
		this.rgvDatosLita.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvDatosLita.Name = "rgvDatosLita";
		this.rgvDatosLita.ReadOnly = true;
		this.rgvDatosLita.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvDatosLita.Size = new System.Drawing.Size(1295, 333);
		this.rgvDatosLita.TabIndex = 1;
		this.rgvDatosLita.ThemeName = "TelerikMetroTouch";
		this.rgvDatosLita.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(rgvDatosLita_CellFormatting);
		this.rgvDatosLita.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDatosLita_CellClick);
		this.rgvDatosLita.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDatosLita_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(1439, 432);
		base.Controls.Add(this.Acciones);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		base.MinimizeBox = false;
		base.Name = "frmCotizacionesVigentes";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Historial de Cotizaciones";
		base.Load += new System.EventHandler(frmCotizacionesVigentes_Load);
		base.Shown += new System.EventHandler(frmCotizacionesVigentes_Shown);
		((System.ComponentModel.ISupportInitialize)this.rgvDatos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDatos).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.Acciones.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDatosLita.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDatosLita).EndInit();
		base.ResumeLayout(false);
	}
}
