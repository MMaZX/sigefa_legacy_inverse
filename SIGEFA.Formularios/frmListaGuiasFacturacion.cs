using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.SunatFacElec;
using SpreadsheetLight;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListaGuiasFacturacion : Form
{
	private clsAdmGuiaFacturacion AdmGuia = new clsAdmGuiaFacturacion();

	private clsGuiaFacturacion guia = new clsGuiaFacturacion();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	internal clsUsuario usuario_click = null;

	public int CHECKTODOS = 0;

	private Facturacion con = new Facturacion();

	private clsAdmCliente AdmCliente = new clsAdmCliente();

	private clsCliente cliente = new clsCliente();

	private clsGuiaFacturacion guiafacturacion = new clsGuiaFacturacion();

	public List<clsDetalleGuiaFacturacion> detalle = new List<clsDetalleGuiaFacturacion>();

	private Timer timer;

	private Form notificationForm;

	public string mensaje = "";

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private GroupBox groupBox2;

	private RadGridView rgvdetalle2;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private Button btnSalir;

	private Button btnIrGuia;

	private Button btnactualizar;

	private Button btnanular;

	private Button btnexportar;

	private Button btnconsultaticket;

	private RadButton btnenviar;

	private RadCheckBox chktodos;

	private RadCheckBox chksinenviar;

	private RadCheckBox chkrespuestasunat;

	public frmListaGuiasFacturacion()
	{
		InitializeComponent();
	}

	private void Timer_Tick(object sender, EventArgs e)
	{
		timer.Stop();
		MostrarNotificacion("GRE Pendientes de Envió", mensaje);
	}

	private void MostrarNotificacion(string titulo, string mensaje)
	{
		notificationForm = new Form();
		notificationForm.FormBorderStyle = FormBorderStyle.None;
		notificationForm.StartPosition = FormStartPosition.Manual;
		notificationForm.ShowInTaskbar = false;
		notificationForm.BackColor = System.Drawing.Color.GreenYellow;
		notificationForm.Size = new Size(300, 100);
		notificationForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - notificationForm.Width, Screen.PrimaryScreen.WorkingArea.Height - notificationForm.Height);
		Label titleLabel = new Label();
		titleLabel.Text = titulo;
		titleLabel.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 12f, FontStyle.Bold);
		titleLabel.AutoSize = true;
		titleLabel.Location = new Point(10, 10);
		notificationForm.Controls.Add(titleLabel);
		Label messageLabel = new Label();
		messageLabel.Text = mensaje;
		messageLabel.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 10f, FontStyle.Regular);
		messageLabel.AutoSize = false;
		messageLabel.Size = new Size(280, 60);
		messageLabel.Location = new Point(10, 40);
		messageLabel.TextAlign = ContentAlignment.TopLeft;
		notificationForm.Controls.Add(messageLabel);
		Button closeButton = new Button();
		closeButton.Text = "X";
		closeButton.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Bold);
		closeButton.Size = new Size(20, 20);
		closeButton.Location = new Point(notificationForm.Width - closeButton.Width - 5, 5);
		closeButton.Click += delegate
		{
			notificationForm.Close();
		};
		notificationForm.Controls.Add(closeButton);
		notificationForm.Show();
	}

	private void frmListaGuiasFacturacion_Load(object sender, EventArgs e)
	{
		try
		{
			ListaGuias();
			HabilitarCheckt();
			PendientesEnvio();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void PendientesEnvio()
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		DataSet ds = new DataSet();
		ds = dBAccess.ExecuteDataSet("GuiasPendientesEnvio");
		if (ds.Tables[0].Rows.Count > 0)
		{
			DataRow primeraFila = ds.Tables[0].Rows[0];
			int CantidadPendiente = Convert.ToInt32(primeraFila["CantidadPendiente"]);
			DateTime fechainicio = Convert.ToDateTime(primeraFila["fechainicio"]);
			DateTime fechafin = Convert.ToDateTime(primeraFila["fechafin"]);
			if (CantidadPendiente > 0)
			{
				mensaje = "Tiene " + CantidadPendiente + " GRE sin enviar , por favor revice en las siguientes fechas : " + fechainicio.ToString("dd/MM/yyyy") + " hasta " + fechafin.ToString("dd/MM/yyyy");
				timer = new Timer();
				timer.Interval = 5000;
				timer.Tick += Timer_Tick;
				timer.Start();
			}
		}
	}

	private void HabilitarCheckt()
	{
		foreach (GridViewDataColumn column in rgvdetalle2.Columns)
		{
			if (column.Name != "chkEnvio")
			{
				column.ReadOnly = true;
			}
		}
		rgvdetalle2.Columns["chkEnvio"].ReadOnly = false;
	}

	private void ListaGuias()
	{
		try
		{
			data.DataSource = AdmGuia.ListaGuiasFacturacion(dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal, chksinenviar.Checked, chkrespuestasunat.Checked);
			rgvdetalle2.DataSource = data;
			data.Filter = string.Empty;
			rgvdetalle2.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			ListaGuias();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			ListaGuias();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnactualizar_Click(object sender, EventArgs e)
	{
		try
		{
			ListaGuias();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void rgvdetalle_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (!(e.Column.Name != "chkEnvio"))
		{
			return;
		}
		try
		{
			if (rgvdetalle2.Rows.Count >= 1)
			{
				guia.codGuia = Convert.ToInt32(e.Row.Cells["codguia"].Value);
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnIrGuia_Click(object sender, EventArgs e)
	{
		if (rgvdetalle2.Rows.Count >= 1 && rgvdetalle2.CurrentRow != null)
		{
			frmGuiaFacturacion form = new frmGuiaFacturacion();
			form.MdiParent = base.MdiParent;
			form.codguia = guia.codGuia;
			form.proceso = 2;
			form.Show();
		}
	}

	private void btnanular_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvdetalle2.CurrentRow == null || guia.codGuia == 0)
			{
				return;
			}
			if (frmLogin.iNivelUser == 1)
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular Guía", "Guías de Remisión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No)
				{
					if (AdmGuia.Anular(guia.codGuia, frmLogin.iCodUser))
					{
						MessageBox.Show("La guía ha sido anulada correctamente", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						ListaGuias();
					}
					else
					{
						MessageBox.Show("Error al Anular Guía", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				return;
			}
			DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea anular Guía", "Guías de Remisión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult2 == DialogResult.No)
			{
				return;
			}
			usuario_click = null;
			frmAutorizacion frm = new frmAutorizacion();
			frm.tipoAccion = 3;
			frm.tipoVentanaAAsignarUsuario = 13;
			frm.VentanaListaGuiaFacturacion = this;
			DialogResult dr = DialogResult.None;
			dr = frm.ShowDialog();
			if (dr == DialogResult.OK && usuario_click != null)
			{
				if (AdmGuia.Anular(guia.codGuia, usuario_click.CodUsuario))
				{
					MessageBox.Show("La guía ha sido anulada correctamente", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					ListaGuias();
				}
				else
				{
					MessageBox.Show("Error al Anular Guía", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnexportar_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			SLDocument sl = new SLDocument();
			if (rgvdetalle2.ChildRows.Count > 0)
			{
				int i = 0;
				int fila_excel = 4;
				int fila_a_concatenar = 0;
				int fila_first_concat = 0;
				int contador = 1;
				string desde = dtpDesde.Value.ToString();
				string hasta = dtpHasta.Value.ToString();
				sl.AddWorksheet("Listado de GRE");
				formatearFilaPrincipalHoja(sl, desde, hasta);
				contador = 1;
				DataTable table = new DataTable();
				foreach (GridViewDataColumn column in rgvdetalle2.Columns)
				{
					if (column.Name != "chkEnvio")
					{
						table.Columns.Add(column.Name, column.DataType);
					}
				}
				foreach (GridViewRowInfo row in rgvdetalle2.MasterTemplate.DataView)
				{
					DataRow dataRow = table.NewRow();
					for (int o = 1; o <= table.Columns.Count; o++)
					{
						dataRow[o - 1] = row.Cells[o].Value;
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
				MessageBox.Show(ex.Message, "ListaGuias");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, "Error");
		}
	}

	private string obtenerRutaParaGuardar(string namefile = "ListaGRE")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "ListaGRE";
			sfd.FileName = namefile + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Lista Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		sl.SetCellValue("A1", "LISTADO DE GRE ");
		sl.MergeWorksheetCells("A1", "N1");
		sl.SetCellValue("A2", "DESDE: " + Convert.ToDateTime(desde).ToShortDateString() + " - HASTA: " + Convert.ToDateTime(hasta).ToShortDateString());
		sl.MergeWorksheetCells("A2", "L2");
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 3, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellStyle("A3", style);
		sl.SetCellValue("A3", "ALMACEN");
		sl.SetColumnWidth(1, 20.0);
		sl.SetCellStyle("B3", style);
		sl.SetCellValue("B3", "FECHA DESPACHO");
		sl.SetColumnWidth(2, 20.0);
		sl.SetCellStyle("C3", style);
		sl.SetCellValue("C3", "N° G/R");
		sl.SetColumnWidth(3, 20.0);
		sl.SetCellStyle("D3", style);
		sl.SetCellValue("D3", "CLIENTE");
		sl.SetColumnWidth(4, 20.0);
		sl.SetCellStyle("E3", style);
		sl.SetCellValue("E3", "IMPORTE");
		sl.SetColumnWidth(5, 15.0);
		sl.SetCellStyle("F3", style);
		sl.SetCellValue("F3", "FLETE");
		sl.SetColumnWidth(6, 15.0);
		sl.SetCellStyle("G3", style);
		sl.SetCellValue("G3", "ESTADO");
		sl.SetColumnWidth(7, 15.0);
		sl.SetCellStyle("H3", style);
		sl.SetCellValue("H3", "FORMA DE PAGO");
		sl.SetColumnWidth(8, 25.0);
		sl.SetCellStyle("I3", style);
		sl.SetCellValue("I3", "N° FACTURA");
		sl.SetColumnWidth(9, 20.0);
		sl.SetCellStyle("J3", style);
		sl.SetCellValue("J3", "FECHA FACTURACIÓN");
		sl.SetColumnWidth(10, 25.0);
		sl.SetCellStyle("K3", style);
		sl.SetCellValue("K3", "O/C");
		sl.SetColumnWidth(11, 20.0);
		sl.SetCellStyle("L3", style);
		sl.SetCellValue("L3", "MOTIVO");
		sl.SetColumnWidth(12, 15.0);
		sl.SetCellStyle("M3", style);
		sl.SetCellValue("M3", "ESTADO SUNAT");
		sl.SetColumnWidth(13, 40.0);
		sl.SetCellStyle("N3", style);
		sl.SetCellValue("N3", "MENSAJE SUNAT");
		sl.SetColumnWidth(14, 40.0);
	}

	private void asignarBordes(SLStyle style)
	{
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.LeftBorder.Color = System.Drawing.Color.LightSkyBlue;
		style.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.TopBorder.Color = System.Drawing.Color.LightSkyBlue;
		style.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.RightBorder.Color = System.Drawing.Color.LightSkyBlue;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.BottomBorder.Color = System.Drawing.Color.LightSkyBlue;
		style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightBlue, System.Drawing.Color.Transparent);
	}

	private void dandoValoresaFilaVentasExcel(SLDocument sl, int fila_excel, DataRow fila)
	{
		SLStyle style = sl.CreateStyle();
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("A" + fila_excel, fila[0].ToString());
		sl.SetCellStyle("A" + fila_excel, style);
		sl.SetCellValue("B" + fila_excel, fila[1].ToString());
		sl.SetCellStyle("B" + fila_excel, style);
		sl.SetCellValue("C" + fila_excel, fila[2].ToString());
		sl.SetCellStyle("C" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("D" + fila_excel, fila[3].ToString());
		sl.SetCellStyle("D" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("E" + fila_excel, (fila[4] != DBNull.Value) ? Convert.ToDecimal(fila[4]) : Convert.ToDecimal(0));
		sl.SetCellStyle("E" + fila_excel, style);
		sl.SetCellValue("F" + fila_excel, (fila[5] != DBNull.Value) ? Convert.ToDecimal(fila[5]) : Convert.ToDecimal(0));
		sl.SetCellStyle("F" + fila_excel, style);
		sl.SetCellValue("G" + fila_excel, fila[6].ToString());
		sl.SetCellStyle("G" + fila_excel, style);
		sl.SetCellValue("H" + fila_excel, fila[7].ToString());
		sl.SetCellStyle("H" + fila_excel, style);
		sl.SetCellValue("I" + fila_excel, fila[8].ToString());
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, fila[9].ToString());
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, fila[10].ToString());
		sl.SetCellStyle("K" + fila_excel, style);
		sl.SetCellValue("L" + fila_excel, fila[11].ToString());
		sl.SetCellStyle("L" + fila_excel, style);
		sl.SetCellValue("M" + fila_excel, fila[12].ToString());
		sl.SetCellStyle("M" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("N" + fila_excel, fila[15].ToString());
		sl.SetCellStyle("N" + fila_excel, style);
	}

	private void rgvdetalle2_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvdetalle2.Rows.Count >= 1 && rgvdetalle2.CurrentRow != null)
		{
			frmGuiaFacturacion form = new frmGuiaFacturacion();
			form.MdiParent = base.MdiParent;
			form.codguia = guia.codGuia;
			form.proceso = 2;
			form.Show();
		}
	}

	private void rgvdetalle2_CellValueChanged(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.Column.Name == "chkEnvio" && CHECKTODOS == 0)
			{
				bool isChecked = (bool)e.Value;
				GridViewRowInfo row = e.Row;
				row.Cells["chkEnvio"].Value = isChecked;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void chktodos_CheckStateChanged(object sender, EventArgs e)
	{
		try
		{
			CHECKTODOS = 1;
			bool isChecked = chktodos.Checked;
			foreach (GridViewRowInfo fila in rgvdetalle2.ChildRows)
			{
				fila.Cells[0].Value = isChecked;
			}
			CHECKTODOS = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void chksinenviar_CheckStateChanged(object sender, EventArgs e)
	{
		ListaGuias();
		chktodos.Checked = chksinenviar.Checked;
	}

	private async void btnenviar_Click(object sender, EventArgs e)
	{
		try
		{
			if (chksinenviar.Checked)
			{
				foreach (GridViewRowInfo fila in rgvdetalle2.Rows)
				{
					if (!(bool)fila.Cells["chkEnvio"].Value)
					{
						continue;
					}
					detalle.Clear();
					int codGuia = Convert.ToInt32(fila.Cells["codguia"].Value);
					guiafacturacion = AdmGuia.ListaGuiaFacturacion(codGuia);
					cliente = AdmCliente.MuestraCliente(guiafacturacion.codCliente);
					new DataTable();
					DataTable DETALLEGUIA = AdmGuia.ListaDetalleGuia(codGuia);
					foreach (DataRow row in DETALLEGUIA.Rows)
					{
						clsDetalleGuiaFacturacion miObjeto = new clsDetalleGuiaFacturacion
						{
							codProducto = Convert.ToInt32(row["referencia"]),
							producto = Convert.ToString(row["producto"]),
							unidad = Convert.ToString(row["unidad"]),
							cantidad = Convert.ToDecimal(row["cantidad"])
						};
						detalle.Add(miObjeto);
					}
					await con.DatosGuiaRemision(cliente, guiafacturacion, detalle);
				}
				ListaGuias();
				if (rgvdetalle2.RowCount > 0)
				{
					chksinenviar.Checked = false;
					MessageBox.Show("Aun Tiene Documentos Pendientes de Envio", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					chksinenviar.Checked = true;
				}
				else
				{
					MessageBox.Show("Todos los Docuemntos Fueron Enviados", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					chksinenviar.Checked = false;
				}
			}
			else
			{
				MessageBox.Show("De clic en Sin Enviar, para flitrar guias que no esten en sunat ", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			Exception EX = ex;
			MessageBox.Show(EX.Message);
		}
	}

	private async void btnconsultaticket_Click(object sender, EventArgs e)
	{
		try
		{
			if (chkrespuestasunat.Checked)
			{
				foreach (GridViewRowInfo fila in rgvdetalle2.Rows)
				{
					if ((bool)fila.Cells["chkEnvio"].Value)
					{
						detalle.Clear();
						int codGuia = Convert.ToInt32(fila.Cells["codguia"].Value);
						guiafacturacion = AdmGuia.ListaGuiaFacturacion(codGuia);
						if (guiafacturacion.NroTicket != "")
						{
							await con.ConsultaticketGuia(guiafacturacion);
						}
					}
				}
				ListaGuias();
				if (rgvdetalle2.RowCount > 0)
				{
					chkrespuestasunat.Checked = false;
					MessageBox.Show("Aun Tiene Documentos Por Consultar", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					chkrespuestasunat.Checked = true;
				}
				else
				{
					MessageBox.Show("Todos los Docuemntos Fueron Enviados", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					chkrespuestasunat.Checked = false;
				}
			}
			else
			{
				MessageBox.Show("De clic en Sin Enviar, para flitrar guias que no esten en sunat ", "Lista Guías", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			Exception EX = ex;
			MessageBox.Show(EX.Message);
		}
	}

	private void chkrespuestasunat_CheckStateChanged(object sender, EventArgs e)
	{
		ListaGuias();
		chktodos.Checked = chkrespuestasunat.Checked;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaGuiasFacturacion));
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.chksinenviar = new Telerik.WinControls.UI.RadCheckBox();
		this.btnenviar = new Telerik.WinControls.UI.RadButton();
		this.btnconsultaticket = new System.Windows.Forms.Button();
		this.btnexportar = new System.Windows.Forms.Button();
		this.btnanular = new System.Windows.Forms.Button();
		this.btnactualizar = new System.Windows.Forms.Button();
		this.btnIrGuia = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvdetalle2 = new Telerik.WinControls.UI.RadGridView();
		this.chktodos = new Telerik.WinControls.UI.RadCheckBox();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.chkrespuestasunat = new Telerik.WinControls.UI.RadCheckBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chksinenviar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnenviar).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle2.MasterTemplate).BeginInit();
		this.rgvdetalle2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chktodos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkrespuestasunat).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.chkrespuestasunat);
		this.groupBox1.Controls.Add(this.chksinenviar);
		this.groupBox1.Controls.Add(this.btnenviar);
		this.groupBox1.Controls.Add(this.btnconsultaticket);
		this.groupBox1.Controls.Add(this.btnexportar);
		this.groupBox1.Controls.Add(this.btnanular);
		this.groupBox1.Controls.Add(this.btnactualizar);
		this.groupBox1.Controls.Add(this.btnIrGuia);
		this.groupBox1.Controls.Add(this.btnSalir);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Location = new System.Drawing.Point(8, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1180, 60);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.chksinenviar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chksinenviar.Location = new System.Drawing.Point(272, 9);
		this.chksinenviar.Name = "chksinenviar";
		this.chksinenviar.Size = new System.Drawing.Size(89, 19);
		this.chksinenviar.TabIndex = 76;
		this.chksinenviar.Text = "Sin Enviar";
		this.chksinenviar.ThemeName = "Material";
		this.chksinenviar.CheckStateChanged += new System.EventHandler(chksinenviar_CheckStateChanged);
		this.btnenviar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnenviar.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnenviar.Image = (System.Drawing.Image)resources.GetObject("btnenviar.Image");
		this.btnenviar.Location = new System.Drawing.Point(377, 10);
		this.btnenviar.Name = "btnenviar";
		this.btnenviar.Size = new System.Drawing.Size(110, 36);
		this.btnenviar.TabIndex = 75;
		this.btnenviar.Text = "Enviar";
		this.btnenviar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnenviar.ThemeName = "TelerikMetroTouch";
		this.btnenviar.Click += new System.EventHandler(btnenviar_Click);
		this.btnconsultaticket.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnconsultaticket.Location = new System.Drawing.Point(492, 9);
		this.btnconsultaticket.Name = "btnconsultaticket";
		this.btnconsultaticket.Size = new System.Drawing.Size(144, 37);
		this.btnconsultaticket.TabIndex = 74;
		this.btnconsultaticket.Text = "Consultar Ticket Sunat";
		this.btnconsultaticket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnconsultaticket.UseVisualStyleBackColor = true;
		this.btnconsultaticket.Click += new System.EventHandler(btnconsultaticket_Click);
		this.btnexportar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnexportar.Location = new System.Drawing.Point(642, 10);
		this.btnexportar.Name = "btnexportar";
		this.btnexportar.Size = new System.Drawing.Size(96, 37);
		this.btnexportar.TabIndex = 73;
		this.btnexportar.Text = "Exportar";
		this.btnexportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnexportar.UseVisualStyleBackColor = true;
		this.btnexportar.Click += new System.EventHandler(btnexportar_Click);
		this.btnanular.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnanular.Image = (System.Drawing.Image)resources.GetObject("btnanular.Image");
		this.btnanular.Location = new System.Drawing.Point(744, 9);
		this.btnanular.Name = "btnanular";
		this.btnanular.Size = new System.Drawing.Size(103, 37);
		this.btnanular.TabIndex = 72;
		this.btnanular.Text = "Anular";
		this.btnanular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnanular.UseVisualStyleBackColor = true;
		this.btnanular.Click += new System.EventHandler(btnanular_Click);
		this.btnactualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnactualizar.Image = (System.Drawing.Image)resources.GetObject("btnactualizar.Image");
		this.btnactualizar.Location = new System.Drawing.Point(853, 10);
		this.btnactualizar.Name = "btnactualizar";
		this.btnactualizar.Size = new System.Drawing.Size(103, 37);
		this.btnactualizar.TabIndex = 71;
		this.btnactualizar.Text = "Actualizar";
		this.btnactualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnactualizar.UseVisualStyleBackColor = true;
		this.btnactualizar.Click += new System.EventHandler(btnactualizar_Click);
		this.btnIrGuia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrGuia.Image = (System.Drawing.Image)resources.GetObject("btnIrGuia.Image");
		this.btnIrGuia.Location = new System.Drawing.Point(962, 10);
		this.btnIrGuia.Name = "btnIrGuia";
		this.btnIrGuia.Size = new System.Drawing.Size(103, 37);
		this.btnIrGuia.TabIndex = 55;
		this.btnIrGuia.Text = "Ir Guia";
		this.btnIrGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrGuia.UseVisualStyleBackColor = true;
		this.btnIrGuia.Click += new System.EventHandler(btnIrGuia_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(1071, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(103, 37);
		this.btnSalir.TabIndex = 54;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(137, 7);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 64;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 7);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 63;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(20, 24);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 62;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(140, 24);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 61;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.rgvdetalle2);
		this.groupBox2.Location = new System.Drawing.Point(7, 68);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1181, 336);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Guias";
		this.rgvdetalle2.BackColor = System.Drawing.SystemColors.Control;
		this.rgvdetalle2.Controls.Add(this.chktodos);
		this.rgvdetalle2.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvdetalle2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvdetalle2.Font = new System.Drawing.Font("Segoe UI", 8.25f);
		this.rgvdetalle2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvdetalle2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvdetalle2.Location = new System.Drawing.Point(3, 16);
		this.rgvdetalle2.MasterTemplate.AllowAddNewRow = false;
		gridViewCheckBoxColumn2.AllowFiltering = false;
		gridViewCheckBoxColumn2.FieldName = "chkEnvio";
		gridViewCheckBoxColumn2.HeaderText = "SELECT";
		gridViewCheckBoxColumn2.Name = "chkEnvio";
		gridViewCheckBoxColumn2.Width = 60;
		gridViewTextBoxColumn17.EnableExpressionEditor = false;
		gridViewTextBoxColumn17.FieldName = "almacen";
		gridViewTextBoxColumn17.HeaderText = "ALMACEN";
		gridViewTextBoxColumn17.Name = "almacen";
		gridViewTextBoxColumn17.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn17.Width = 250;
		gridViewTextBoxColumn18.EnableExpressionEditor = false;
		gridViewTextBoxColumn18.FieldName = "fechadespacho";
		gridViewTextBoxColumn18.HeaderText = "FECHA DESPACHO";
		gridViewTextBoxColumn18.Name = "fechadespacho";
		gridViewTextBoxColumn18.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn18.Width = 150;
		gridViewTextBoxColumn19.EnableExpressionEditor = false;
		gridViewTextBoxColumn19.FieldName = "numguia";
		gridViewTextBoxColumn19.HeaderText = "N° G/R";
		gridViewTextBoxColumn19.Name = "numguia";
		gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn19.Width = 150;
		gridViewTextBoxColumn20.EnableExpressionEditor = false;
		gridViewTextBoxColumn20.FieldName = "cliente";
		gridViewTextBoxColumn20.HeaderText = "CLIENTE";
		gridViewTextBoxColumn20.Name = "cliente";
		gridViewTextBoxColumn20.Width = 250;
		gridViewTextBoxColumn21.DataType = typeof(decimal);
		gridViewTextBoxColumn21.FieldName = "total";
		gridViewTextBoxColumn21.HeaderText = "IMPORTE";
		gridViewTextBoxColumn21.Name = "total";
		gridViewTextBoxColumn21.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn21.Width = 80;
		gridViewTextBoxColumn22.DataType = typeof(decimal);
		gridViewTextBoxColumn22.FieldName = "flete";
		gridViewTextBoxColumn22.HeaderText = "FLETE";
		gridViewTextBoxColumn22.Name = "flete";
		gridViewTextBoxColumn22.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn22.Width = 80;
		gridViewTextBoxColumn23.FieldName = "estado";
		gridViewTextBoxColumn23.HeaderText = "ESTADO";
		gridViewTextBoxColumn23.Name = "estado";
		gridViewTextBoxColumn23.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn23.Width = 100;
		gridViewTextBoxColumn24.FieldName = "formpago";
		gridViewTextBoxColumn24.HeaderText = "FORMA DE PAGO";
		gridViewTextBoxColumn24.Name = "formpago";
		gridViewTextBoxColumn24.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn24.Width = 150;
		gridViewTextBoxColumn25.FieldName = "nrocomprobante";
		gridViewTextBoxColumn25.HeaderText = "N° FACTURA";
		gridViewTextBoxColumn25.Name = "nrocomprobante";
		gridViewTextBoxColumn25.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn25.Width = 110;
		gridViewTextBoxColumn26.FieldName = "fechafactura";
		gridViewTextBoxColumn26.HeaderText = "FECHA DE FACTURACION";
		gridViewTextBoxColumn26.Name = "fechafactura";
		gridViewTextBoxColumn26.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn26.Width = 130;
		gridViewTextBoxColumn27.FieldName = "numerooc";
		gridViewTextBoxColumn27.HeaderText = "O/C";
		gridViewTextBoxColumn27.Name = "numerooc";
		gridViewTextBoxColumn27.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn27.Width = 110;
		gridViewTextBoxColumn28.FieldName = "motivo";
		gridViewTextBoxColumn28.HeaderText = "MOTIVO";
		gridViewTextBoxColumn28.Name = "motivo";
		gridViewTextBoxColumn28.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn28.Width = 120;
		gridViewTextBoxColumn29.FieldName = "estadosunat";
		gridViewTextBoxColumn29.HeaderText = "Estado Sunat";
		gridViewTextBoxColumn29.Name = "estadosunat";
		gridViewTextBoxColumn29.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn29.Width = 200;
		gridViewTextBoxColumn30.FieldName = "fguia";
		gridViewTextBoxColumn30.HeaderText = "FECHA";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "fguia";
		gridViewTextBoxColumn30.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn30.Width = 200;
		gridViewTextBoxColumn31.EnableExpressionEditor = false;
		gridViewTextBoxColumn31.FieldName = "codguia";
		gridViewTextBoxColumn31.HeaderText = "codguia";
		gridViewTextBoxColumn31.IsVisible = false;
		gridViewTextBoxColumn31.Name = "codguia";
		gridViewTextBoxColumn31.Width = 100;
		gridViewTextBoxColumn32.FieldName = "mensajesunat";
		gridViewTextBoxColumn32.HeaderText = "Mensaje Sunat";
		gridViewTextBoxColumn32.Name = "mensajesunat";
		gridViewTextBoxColumn32.Width = 300;
		this.rgvdetalle2.MasterTemplate.Columns.AddRange(gridViewCheckBoxColumn2, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32);
		this.rgvdetalle2.MasterTemplate.EnableFiltering = true;
		this.rgvdetalle2.MasterTemplate.EnableGrouping = false;
		this.rgvdetalle2.MasterTemplate.EnableSorting = false;
		this.rgvdetalle2.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvdetalle2.Name = "rgvdetalle2";
		this.rgvdetalle2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvdetalle2.ShowGroupPanel = false;
		this.rgvdetalle2.Size = new System.Drawing.Size(1175, 317);
		this.rgvdetalle2.TabIndex = 0;
		this.rgvdetalle2.ThemeName = "TelerikMetroTouch";
		this.rgvdetalle2.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle_CellClick);
		this.rgvdetalle2.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle2_CellDoubleClick);
		this.rgvdetalle2.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle2_CellValueChanged);
		this.chktodos.Location = new System.Drawing.Point(41, 46);
		this.chktodos.Name = "chktodos";
		this.chktodos.Size = new System.Drawing.Size(18, 18);
		this.chktodos.TabIndex = 2;
		this.chktodos.ThemeName = "Fluent";
		this.chktodos.CheckStateChanged += new System.EventHandler(chktodos_CheckStateChanged);
		this.chkrespuestasunat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chkrespuestasunat.Location = new System.Drawing.Point(272, 35);
		this.chkrespuestasunat.Name = "chkrespuestasunat";
		this.chkrespuestasunat.Size = new System.Drawing.Size(98, 19);
		this.chkrespuestasunat.TabIndex = 77;
		this.chkrespuestasunat.Text = "En Proceso";
		this.chkrespuestasunat.ThemeName = "Material";
		this.chkrespuestasunat.CheckStateChanged += new System.EventHandler(chkrespuestasunat_CheckStateChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1192, 432);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListaGuiasFacturacion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmListaGuiasFacturacion";
		base.Load += new System.EventHandler(frmListaGuiasFacturacion_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chksinenviar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnenviar).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle2.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle2).EndInit();
		this.rgvdetalle2.ResumeLayout(false);
		this.rgvdetalle2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chktodos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkrespuestasunat).EndInit();
		base.ResumeLayout(false);
	}
}
