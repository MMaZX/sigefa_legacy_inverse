using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;

namespace SIGEFA.Formularios;

public class frmNotasCreditoAplicadas : RadForm
{
	private clsAdmNotaCredito AdmNota = new clsAdmNotaCredito();

	private clsNotaCredito nota = new clsNotaCredito();

	private clsAdmFactura admFac = new clsAdmFactura();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnSalir;

	private Button btnIrGuia;

	private Button btGenVenta;

	private ImageList imageList1;

	private Button btnAnular;

	private Label label3;

	private Label label4;

	private DataGridView dgvNotasCredito;

	private Label label1;

	private Label label5;

	private Label label7;

	private Label label2;

	private TextBox txtFiltro;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Label label6;

	private Button btnReporte;

	private GroupBox groupBox2;

	private RadLabel radLabel1;

	private Label txtNombreProducto;

	private Label label8;

	private Button btnBuscarProducto;

	private Label label9;

	private TextBox txtCodprod;

	private ComboBox cmbAlmacenes;

	public RadDropDownList cmbAlmacenes1;

	private DataGridViewTextBoxColumn codNotaCredito;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn doccli;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn Total;

	private DataGridViewTextBoxColumn tipo_pago;

	private DataGridViewTextBoxColumn codreferencia;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewTextBoxColumn fecha_doc_ref;

	private DataGridViewTextBoxColumn vendedor;

	private DataGridViewTextBoxColumn anulado;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewLinkColumn Operacion;

	private DataGridViewTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn CodNotaI;

	private RadGridView notasCredito;

	public frmNotasCreditoAplicadas()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		notasCredito.DataSource = data;
		data.DataSource = AdmNota.ListaNotasCreditoAplicadas(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal);
		notasCredito.ClearSelection();
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		if (notasCredito.Rows.Count >= 1 && notasCredito.CurrentRow != null)
		{
			frmNotadeCredito form = new frmNotadeCredito();
			form.MdiParent = base.MdiParent;
			form.CodNota = notasCredito.CurrentRow.Cells[codreferencia.Name].Value.ToString();
			form.CodNC = Convert.ToInt32(notasCredito.CurrentRow.Cells[codNotaCredito.Name].Value);
			form.CodNotaS = Convert.ToInt32(notasCredito.CurrentRow.Cells[CodNotaI.Name].Value);
			form.Proceso = 3;
			form.Show();
		}
	}

	private void frmPedidosPendientes_Load(object sender, EventArgs e)
	{
		btnAnular.Visible = true;
		cargaAlmacenes();
		dtpDesde.Value = dtpDesde.Value.AddDays(-90.0);
		CargaLista();
		label7.Text = "Cliente";
		label6.Text = "cliente";
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.DropDownStyle = ComboBoxStyle.DropDownList;
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (nota.CodNotaCredito != "")
		{
			if (Application.OpenForms["frmVenta"] != null)
			{
				Application.OpenForms["frmVenta"].Close();
				return;
			}
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.CodVenta = notasCredito.CurrentRow.Cells["codreferencia"].Value.ToString();
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (notasCredito.CurrentRow != null && nota.CodNotaCredito != "")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la nota seleccionada", "Notas de Credito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && int.TryParse(nota.CodNotaCredito, out var codNotaCredito) && AdmNota.anular(codNotaCredito))
			{
				MessageBox.Show("El documento ha sido anulado correctamente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label6.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			SLDocument sl = new SLDocument();
			if (notasCredito.Rows.Count > 0)
			{
				int i = 0;
				int fila_excel = 4;
				int fila_a_concatenar = 0;
				int fila_first_concat = 0;
				int contador = 1;
				string desde = dtpDesde.Value.ToString();
				string hasta = dtpHasta.Value.ToString();
				sl.AddWorksheet("Listado de notas creditos");
				formatearFilaPrincipalHoja(sl, desde, hasta);
				contador = 1;
				foreach (GridViewRowInfo fila in notasCredito.ChildRows)
				{
					SLStyle aux_style = sl.CreateStyle();
					asignarBordes(aux_style);
					sl.SetCellStyle("A" + fila_excel, aux_style);
					sl.SetCellStyle("B" + fila_excel, aux_style);
					dandoValoresaFilaVentasExcel(sl, fila_excel, fila, contador);
					fila_excel++;
					i++;
					contador++;
				}
			}
			Cursor = Cursors.Default;
			try
			{
				string cadenaGuardado = obtenerRutaParaGuardar("notas_creditos");
				if (cadenaGuardado != null)
				{
					sl.SaveAs(cadenaGuardado);
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Reporte Notas Crédito");
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Ocurrió un error al generar reporte de notas de crédito");
		}
	}

	private void dgvNotasCredito_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvNotasCredito.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvNotasCredito.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged_1(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvNotasCredito_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && !e.Row.Selected)
		{
		}
	}

	private void dgvNotasCredito_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmNotadeCredito form = new frmNotadeCredito();
			form.MdiParent = base.MdiParent;
			form.CodNota = dgvNotasCredito.CurrentRow.Cells[codreferencia.Name].Value.ToString();
			form.CodNC = Convert.ToInt32(dgvNotasCredito.CurrentRow.Cells[codNotaCredito.Name].Value);
			form.CodNotaS = Convert.ToInt32(dgvNotasCredito.CurrentRow.Cells[CodNotaI.Name].Value);
			form.Proceso = 3;
			form.MdiParent = base.MdiParent;
			form.Show();
		}
	}

	private void frmNotasCredito_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			dgvNotasCredito.Focus();
		}
	}

	private void dgvNotasCredito_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && e.RowIndex != -1)
		{
			DataGridViewCell celda = dgvNotasCredito.CurrentCell;
			if (celda.Value.ToString() == "DEVOLUCION DE DINERO")
			{
				nota.CodNotaCredito = dgvNotasCredito.Rows[e.RowIndex].Cells[CodNotaI.Name].Value.ToString();
				nota.CodCliente = Convert.ToInt32(dgvNotasCredito.Rows[e.RowIndex].Cells[codcliente.Name].Value.ToString());
				frmCancelarPago form = new frmCancelarPago();
				form.CodNota = nota.CodNotaCredito.ToString();
				form.tipo = 100;
				form.CodCliente = nota.CodCliente;
				form.ShowDialog();
			}
			if (celda.OwningColumn.Name == docref.Name)
			{
				frmVenta form2 = new frmVenta();
				form2.MdiParent = base.MdiParent;
				form2.CodVenta = dgvNotasCredito.Rows[e.RowIndex].Cells[codreferencia.Name].Value.ToString();
				form2.Proceso = 3;
				form2.Show();
			}
		}
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
	{
		CargaLista();
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 2;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			btnBuscarProducto_Click(null, null);
		}
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			CargaListaxProducto();
		}
		else
		{
			txtNombreProducto.Text = "---";
		}
	}

	private void CargaListaxProducto()
	{
		try
		{
			notasCredito.DataSource = data;
			data.DataSource = AdmNota.ListaNotasCreditoXProducto(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal, Convert.ToInt32(txtCodprod.Text));
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void notasCredito_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (notasCredito.Rows.Count >= 1 && e.RowIndex != -1)
		{
			if (e.Column.Name.ToString() == "docref")
			{
				frmVenta form = new frmVenta();
				form.MdiParent = base.MdiParent;
				form.CodVenta = notasCredito.Rows[e.RowIndex].Cells[codreferencia.Name].Value.ToString();
				form.Proceso = 3;
				form.Show();
			}
			else
			{
				frmNotadeCredito form2 = new frmNotadeCredito();
				form2.MdiParent = base.MdiParent;
				form2.CodNota = notasCredito.CurrentRow.Cells[codreferencia.Name].Value.ToString();
				form2.CodNC = Convert.ToInt32(notasCredito.CurrentRow.Cells[codNotaCredito.Name].Value);
				form2.CodNotaS = Convert.ToInt32(notasCredito.CurrentRow.Cells[CodNotaI.Name].Value);
				form2.Proceso = 3;
				form2.MdiParent = base.MdiParent;
				form2.Show();
			}
		}
	}

	private void notasCredito_CellFormatting(object sender, CellFormattingEventArgs e)
	{
		e.CellElement.BorderLeftWidth = 1f;
		e.CellElement.BorderRightWidth = 1f;
		e.CellElement.BorderTopWidth = 1f;
		e.CellElement.BorderBottomWidth = 1f;
	}

	private void notasCredito_ViewRowFormatting(object sender, RowFormattingEventArgs e)
	{
		if (e.RowElement is GridDataRowElement)
		{
			e.RowElement.RowInfo.Height = 35;
			e.RowElement.TextWrap = true;
		}
	}

	private void formatearFilaPrincipalHoja(SLDocument sl, string desde, string hasta)
	{
		sl.SetCellValue("A1", "REPORTE DE NOTAS DE CREDITO ");
		sl.MergeWorksheetCells("A1", "J1");
		sl.SetCellValue("A2", "DESDE: " + Convert.ToDateTime(desde).ToShortDateString() + " - HASTA: " + Convert.ToDateTime(hasta).ToShortDateString());
		sl.MergeWorksheetCells("A2", "J2");
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 3, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellValue("A3", "#");
		sl.SetColumnWidth(1, 10.0);
		sl.SetCellValue("B3", "N° DOCUMENTO");
		sl.SetColumnWidth(2, 18.0);
		sl.SetCellValue("C3", "F. EMISION");
		sl.SetColumnWidth(3, 18.0);
		sl.SetCellValue("D3", "N° DOC. CLIENTE");
		sl.SetColumnWidth(4, 20.0);
		sl.SetCellValue("E3", "CLIENTE");
		sl.SetColumnWidth(5, 35.0);
		sl.SetCellValue("F3", "TOTAL");
		sl.SetColumnWidth(6, 25.0);
		sl.SetCellValue("G3", "TIPO");
		sl.SetColumnWidth(7, 10.0);
		sl.SetCellValue("H3", "F. DOC. REFERENCIA");
		sl.SetColumnWidth(8, 20.0);
		sl.SetCellValue("I3", "DOC. REFERENCIA");
		sl.SetColumnWidth(9, 20.0);
		sl.SetCellValue("J3", "ESTADO SUNAT");
		sl.SetColumnWidth(10, 20.0);
		sl.SetCellValue("K3", "ESTADO");
		sl.SetColumnWidth(11, 12.0);
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

	private void dandoValoresaFilaVentasExcel(SLDocument sl, int fila_excel, GridViewRowInfo fila, int contador)
	{
		SLStyle style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellValue("A" + fila_excel, contador);
		sl.SetCellStyle("A" + fila_excel, style);
		sl.SetCellValue("B" + fila_excel, fila.Cells["numdoc"].Value.ToString());
		sl.SetCellStyle("B" + fila_excel, style);
		sl.SetCellValue("C" + fila_excel, Convert.ToDateTime(fila.Cells["fecha"].Value).ToShortDateString());
		sl.SetCellStyle("C" + fila_excel, style);
		sl.SetCellValue("D" + fila_excel, fila.Cells["doccli"].Value.ToString());
		sl.SetCellStyle("D" + fila_excel, style);
		sl.SetCellValue("E" + fila_excel, fila.Cells["cliente"].Value.ToString());
		sl.SetCellStyle("E" + fila_excel, style);
		sl.SetCellValue("F" + fila_excel, Convert.ToDouble(fila.Cells["Total"].Value));
		sl.SetCellStyle("F" + fila_excel, style);
		sl.SetCellValue("G" + fila_excel, fila.Cells["tipo_pago"].Value.ToString());
		sl.SetCellStyle("G" + fila_excel, style);
		sl.SetCellValue("H" + fila_excel, Convert.ToDateTime(fila.Cells["fecha_doc_ref"].Value).ToShortDateString());
		sl.SetCellStyle("H" + fila_excel, style);
		sl.SetCellValue("I" + fila_excel, fila.Cells["docref"].Value.ToString());
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, fila.Cells["enviados"].Value.ToString());
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, fila.Cells["anulado"].Value.ToString());
		sl.SetCellStyle("K" + fila_excel, style);
	}

	private string obtenerRutaParaGuardar(string namefile = "Ventas_diarias")
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotasCreditoAplicadas));
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.cmbAlmacenes1 = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.btnReporte = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.dgvNotasCredito = new System.Windows.Forms.DataGridView();
		this.codNotaCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.doccli = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo_pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codreferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha_doc_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.vendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Operacion = new System.Windows.Forms.DataGridViewLinkColumn();
		this.codcliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodNotaI = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.label8 = new System.Windows.Forms.Label();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrGuia = new System.Windows.Forms.Button();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.notasCredito = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvNotasCredito).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.notasCredito).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.notasCredito.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.cmbAlmacenes1);
		this.groupBox1.Controls.Add(this.radLabel1);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1305, 70);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Notas de Credito";
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Location = new System.Drawing.Point(386, 28);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(150, 21);
		this.cmbAlmacenes.TabIndex = 63;
		this.cmbAlmacenes.SelectedIndexChanged += new System.EventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.cmbAlmacenes1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
		this.cmbAlmacenes1.Enabled = false;
		this.cmbAlmacenes1.FormattingEnabled = false;
		this.cmbAlmacenes1.Location = new System.Drawing.Point(1091, 6);
		this.cmbAlmacenes1.Name = "cmbAlmacenes1";
		this.cmbAlmacenes1.Size = new System.Drawing.Size(168, 24);
		this.cmbAlmacenes1.TabIndex = 62;
		this.cmbAlmacenes1.ThemeName = "Fluent";
		this.cmbAlmacenes1.Visible = false;
		this.cmbAlmacenes1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(cmbAlmacenes_SelectedIndexChanged);
		((Telerik.WinControls.UI.RadDropDownListElement)this.cmbAlmacenes1.GetChildAt(0)).DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
		((Telerik.WinControls.Primitives.TextPrimitive)this.cmbAlmacenes1.GetChildAt(0).GetChildAt(2).GetChildAt(1)
			.GetChildAt(5)).DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
		((Telerik.WinControls.Primitives.TextPrimitive)this.cmbAlmacenes1.GetChildAt(0).GetChildAt(2).GetChildAt(1)
			.GetChildAt(5)).LineLimit = false;
		((Telerik.WinControls.Primitives.TextPrimitive)this.cmbAlmacenes1.GetChildAt(0).GetChildAt(2).GetChildAt(1)
			.GetChildAt(5)).UseCompatibleTextRendering = false;
		this.radLabel1.Location = new System.Drawing.Point(328, 28);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(52, 18);
		this.radLabel1.TabIndex = 61;
		this.radLabel1.Text = "Almacen:";
		this.btnReporte.Image = SIGEFA.Properties.Resources.printer;
		this.btnReporte.Location = new System.Drawing.Point(199, 15);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(93, 41);
		this.btnReporte.TabIndex = 60;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Visible = false;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.SteelBlue;
		this.label6.Location = new System.Drawing.Point(825, 37);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(11, 12);
		this.label6.TabIndex = 59;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(99, 33);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(81, 20);
		this.dtpHasta.TabIndex = 58;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged_1);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(12, 33);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(81, 20);
		this.dtpDesde.TabIndex = 57;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged_1);
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(612, 33);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 56;
		this.txtFiltro.Visible = false;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(10, 18);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 12);
		this.label2.TabIndex = 55;
		this.label2.Text = "Desde";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(610, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(32, 12);
		this.label1.TabIndex = 54;
		this.label1.Text = "Filtro";
		this.label1.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(648, 18);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 52;
		this.label5.Text = "Por :";
		this.label5.Visible = false;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(683, 18);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 51;
		this.label7.Text = "X";
		this.label7.Visible = false;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(97, 18);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(35, 12);
		this.label4.TabIndex = 49;
		this.label4.Text = "Hasta";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(23, 349);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(37, 12);
		this.label3.TabIndex = 48;
		this.label3.Text = "Desde";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.dgvNotasCredito.AllowUserToAddRows = false;
		this.dgvNotasCredito.AllowUserToDeleteRows = false;
		this.dgvNotasCredito.AllowUserToOrderColumns = true;
		this.dgvNotasCredito.AllowUserToResizeRows = false;
		this.dgvNotasCredito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvNotasCredito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvNotasCredito.Columns.AddRange(this.codNotaCredito, this.numdoc, this.fecha, this.doccli, this.cliente, this.Total, this.tipo_pago, this.codreferencia, this.docref, this.fecha_doc_ref, this.vendedor, this.anulado, this.responsable, this.Operacion, this.codcliente, this.CodNotaI);
		this.dgvNotasCredito.Location = new System.Drawing.Point(0, 70);
		this.dgvNotasCredito.MultiSelect = false;
		this.dgvNotasCredito.Name = "dgvNotasCredito";
		this.dgvNotasCredito.ReadOnly = true;
		this.dgvNotasCredito.RowHeadersVisible = false;
		this.dgvNotasCredito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNotasCredito.Size = new System.Drawing.Size(1305, 163);
		this.dgvNotasCredito.TabIndex = 52;
		this.dgvNotasCredito.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvNotasCredito_CellContentClick);
		this.dgvNotasCredito.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvNotasCredito_CellDoubleClick);
		this.dgvNotasCredito.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvNotasCredito_ColumnHeaderMouseClick);
		this.dgvNotasCredito.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvNotasCredito_RowStateChanged);
		this.codNotaCredito.DataPropertyName = "codNotaCredito";
		this.codNotaCredito.HeaderText = "Codigo";
		this.codNotaCredito.Name = "codNotaCredito";
		this.codNotaCredito.ReadOnly = true;
		this.codNotaCredito.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codNotaCredito.Visible = false;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N. Doc.";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "F. Emision";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.doccli.DataPropertyName = "doccli";
		this.doccli.HeaderText = "Documento";
		this.doccli.Name = "doccli";
		this.doccli.ReadOnly = true;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Total.DataPropertyName = "Total";
		this.Total.HeaderText = "Total";
		this.Total.Name = "Total";
		this.Total.ReadOnly = true;
		this.tipo_pago.DataPropertyName = "tipo_pago";
		this.tipo_pago.HeaderText = "Tipo";
		this.tipo_pago.Name = "tipo_pago";
		this.tipo_pago.ReadOnly = true;
		this.codreferencia.DataPropertyName = "codReferencia";
		this.codreferencia.HeaderText = "Cod. ref";
		this.codreferencia.Name = "codreferencia";
		this.codreferencia.ReadOnly = true;
		this.codreferencia.Visible = false;
		this.docref.DataPropertyName = "docref";
		this.docref.HeaderText = "Doc. Ref.";
		this.docref.Name = "docref";
		this.docref.ReadOnly = true;
		this.fecha_doc_ref.DataPropertyName = "fecha_doc_ref";
		this.fecha_doc_ref.HeaderText = "Fecha Doc. Ref";
		this.fecha_doc_ref.Name = "fecha_doc_ref";
		this.fecha_doc_ref.ReadOnly = true;
		this.vendedor.DataPropertyName = "vendedor";
		this.vendedor.HeaderText = "Vendedor";
		this.vendedor.Name = "vendedor";
		this.vendedor.ReadOnly = true;
		this.vendedor.Visible = false;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Operacion.DataPropertyName = "operacion";
		this.Operacion.HeaderText = "Operacion";
		this.Operacion.Name = "Operacion";
		this.Operacion.ReadOnly = true;
		this.Operacion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Operacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.codcliente.DataPropertyName = "codcliente";
		this.codcliente.HeaderText = "codcliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.ReadOnly = true;
		this.codcliente.Visible = false;
		this.CodNotaI.DataPropertyName = "CodNotaI";
		this.CodNotaI.HeaderText = "CodNotaI";
		this.CodNotaI.Name = "CodNotaI";
		this.CodNotaI.ReadOnly = true;
		this.CodNotaI.Visible = false;
		this.groupBox2.Controls.Add(this.txtNombreProducto);
		this.groupBox2.Controls.Add(this.btnAnular);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.btGenVenta);
		this.groupBox2.Controls.Add(this.btnIrGuia);
		this.groupBox2.Controls.Add(this.btnBuscarProducto);
		this.groupBox2.Controls.Add(this.txtCodprod);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 479);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1305, 56);
		this.groupBox2.TabIndex = 53;
		this.groupBox2.TabStop = false;
		this.groupBox2.Visible = false;
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(442, 29);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 58;
		this.txtNombreProducto.Text = "---";
		this.txtNombreProducto.Visible = false;
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(18, 13);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(102, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular Nota de Credito";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1224, 13);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(401, 29);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(35, 13);
		this.label8.TabIndex = 57;
		this.label8.Text = "Prod.:";
		this.label8.Visible = false;
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.ImageIndex = 2;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(1023, 13);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(96, 37);
		this.btGenVenta.TabIndex = 3;
		this.btGenVenta.Text = "Documento Referencia";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Visible = false;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnIrGuia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrGuia.ImageIndex = 1;
		this.btnIrGuia.ImageList = this.imageList1;
		this.btnIrGuia.Location = new System.Drawing.Point(1125, 13);
		this.btnIrGuia.Name = "btnIrGuia";
		this.btnIrGuia.Size = new System.Drawing.Size(93, 37);
		this.btnIrGuia.TabIndex = 2;
		this.btnIrGuia.Text = "Consultar";
		this.btnIrGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrGuia.UseVisualStyleBackColor = true;
		this.btnIrGuia.Visible = false;
		this.btnIrGuia.Click += new System.EventHandler(btnIrPedido_Click);
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(361, 19);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 56;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Visible = false;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(217, 29);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 54;
		this.txtCodprod.Visible = false;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(214, 13);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(112, 13);
		this.label9.TabIndex = 55;
		this.label9.Text = "Busqueda x Producto:";
		this.label9.Visible = false;
		this.notasCredito.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.notasCredito.Dock = System.Windows.Forms.DockStyle.Fill;
		this.notasCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.notasCredito.Location = new System.Drawing.Point(0, 70);
		this.notasCredito.MasterTemplate.AllowAddNewRow = false;
		this.notasCredito.MasterTemplate.AllowDragToGroup = false;
		this.notasCredito.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codNotaCredito";
		gridViewTextBoxColumn1.HeaderText = "Codigo";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codNotaCredito";
		gridViewTextBoxColumn1.Width = 150;
		gridViewTextBoxColumn2.FieldName = "f_emision_ref";
		gridViewTextBoxColumn2.HeaderText = "F. EMISION DOC APLICADO.";
		gridViewTextBoxColumn2.Name = "f_emision_ref";
		gridViewTextBoxColumn2.Width = 116;
		gridViewTextBoxColumn3.FieldName = "doc_referencia";
		gridViewTextBoxColumn3.HeaderText = "DOC. APLICADO";
		gridViewTextBoxColumn3.Name = "doc_referencia";
		gridViewTextBoxColumn3.Width = 115;
		gridViewTextBoxColumn4.FieldName = "monto_pago_ref";
		gridViewTextBoxColumn4.HeaderText = "MONTO PAGO APLICADO.";
		gridViewTextBoxColumn4.Name = "monto_pago_ref";
		gridViewTextBoxColumn4.Width = 114;
		gridViewTextBoxColumn5.FieldName = "f_emision";
		gridViewTextBoxColumn5.HeaderText = "F. EMISION NC";
		gridViewTextBoxColumn5.Name = "f_emision";
		gridViewTextBoxColumn5.Width = 114;
		gridViewTextBoxColumn6.FieldName = "documento";
		gridViewTextBoxColumn6.HeaderText = "# NC";
		gridViewTextBoxColumn6.Name = "documento";
		gridViewTextBoxColumn6.Width = 115;
		gridViewTextBoxColumn7.FieldName = "cliente";
		gridViewTextBoxColumn7.HeaderText = "CLIENTE";
		gridViewTextBoxColumn7.Name = "cliente";
		gridViewTextBoxColumn7.Width = 227;
		gridViewTextBoxColumn8.FieldName = "total";
		gridViewTextBoxColumn8.HeaderText = "TOTAL NC";
		gridViewTextBoxColumn8.Name = "total";
		gridViewTextBoxColumn8.Width = 114;
		gridViewTextBoxColumn9.FieldName = "disponible";
		gridViewTextBoxColumn9.HeaderText = "DISPONIBLE NC";
		gridViewTextBoxColumn9.Name = "disponible";
		gridViewTextBoxColumn9.Width = 114;
		gridViewTextBoxColumn10.FieldName = "tiponc";
		gridViewTextBoxColumn10.HeaderText = "TIPO NC";
		gridViewTextBoxColumn10.Name = "tiponc";
		gridViewTextBoxColumn10.Width = 166;
		gridViewTextBoxColumn11.FieldName = "estado";
		gridViewTextBoxColumn11.HeaderText = "ESTADO";
		gridViewTextBoxColumn11.Name = "etado";
		gridViewTextBoxColumn11.Width = 110;
		this.notasCredito.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11);
		this.notasCredito.MasterTemplate.EnableFiltering = true;
		this.notasCredito.MasterTemplate.EnablePaging = true;
		this.notasCredito.MasterTemplate.PageSize = 100;
		this.notasCredito.MasterTemplate.ShowHeaderCellButtons = true;
		this.notasCredito.MasterTemplate.ShowRowHeaderColumn = false;
		this.notasCredito.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.notasCredito.Name = "notasCredito";
		this.notasCredito.ReadOnly = true;
		this.notasCredito.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 70, 240, 150);
		this.notasCredito.ShowGroupPanel = false;
		this.notasCredito.ShowHeaderCellButtons = true;
		this.notasCredito.Size = new System.Drawing.Size(1305, 409);
		this.notasCredito.TabIndex = 54;
		this.notasCredito.ThemeName = "Material";
		this.notasCredito.ViewRowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(notasCredito_ViewRowFormatting);
		this.notasCredito.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(notasCredito_CellFormatting);
		this.notasCredito.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(notasCredito_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(1305, 535);
		base.Controls.Add(this.notasCredito);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.dgvNotasCredito);
		base.KeyPreview = true;
		base.Name = "frmNotasCreditoAplicadas";
		base.RootElement.ApplyShapeToControl = true;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Notas de Credito Aplicadas";
		base.ThemeName = "Fluent";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPedidosPendientes_Load);
		base.Shown += new System.EventHandler(frmNotasCredito_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvNotasCredito).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.notasCredito.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.notasCredito).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
