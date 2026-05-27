using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SpreadsheetLight;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmPagos : Office2007Form
{
	private clsReportePagos ds = new clsReportePagos();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmFactura admfac = new clsAdmFactura();

	private clsFactura fac = new clsFactura();

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmTipoDocumento admTipo = new clsAdmTipoDocumento();

	private clsAdmLetra admLetra = new clsAdmLetra();

	private clsLetra let = new clsLetra();

	private clsPago pagoRp = new clsPago();

	public clsFacturaVenta venta = new clsFacturaVenta();

	public int tipocaja = 0;

	private bool band_checkbox = false;

	private RadContextMenu contextMenu;

	private CheckBox chkBoxCabecera = new CheckBox();

	private List<int> ln = new List<int>();

	private int tipodocumento = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private ButtonItem btnBuscar;

	private ButtonItem buttonItem1;

	private GroupBox groupBox1;

	private Label label4;

	private Label label2;

	private Label label1;

	private ComboBox cmbEstado;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private DataGridView dgvPagos;

	private Button btnReporte;

	private Button btnBusqueda;

	private Label label9;

	private ComboBox cmbEmpresa;

	private TextBox txtFiltro;

	private Label label5;

	private Label label7;

	private Label label10;

	private Label label6;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem muestraPagosToolStripMenuItem;

	private ToolStripMenuItem canjearPorLetraToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem nuevaLetraToolStripMenuItem;

	private ToolStripMenuItem modificarLetraToolStripMenuItem;

	private ToolStripMenuItem imprimirLetraToolStripMenuItem;

	private ToolStripMenuItem ingresoABancoToolStripMenuItem;

	private RadioButton rbFechaVencimiento;

	private RadioButton rbFechaEmision;

	private GroupBox gbRadioSelector;

	private Button btnPagoMultiple;

	private Label lblTotalPendienteSoles;

	private Label lblResultadoFiltradoSoles;

	private Label lblTotalPendienteDolares;

	private Label lblResultadoFiltradoDolares;

	private Label lblTotalPendienteConvertido;

	private Label lblResultadoFiltradoConvertido;

	private Label label13;

	private Label label14;

	private Label label11;

	private Label label12;

	private Label label3;

	private Label label8;

	private RadGridView rgvPagos;

	private DataGridViewCheckBoxColumn chkSelector;

	private DataGridViewTextBoxColumn codnota;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn numdocumento;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewTextBoxColumn codproveedore;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn proveedor;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn fechavenc;

	private DataGridViewTextBoxColumn fechacancelado;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn pendiente;

	private DataGridViewTextBoxColumn banco;

	private DataGridViewTextBoxColumn numcuenta;

	private DataGridViewTextBoxColumn numunico;

	private DataGridViewLinkColumn accion;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn codNotaIngreso;

	private DataGridViewTextBoxColumn codMoneda;

	private CheckBox checkBox1;

	private Button btnexcel;

	public frmPagos()
	{
		InitializeComponent();
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		CargaLista();
		decimal aux_soles = calcularTotalPendiente(1);
		lblTotalPendienteSoles.Text = Math.Round(aux_soles, 2).ToString("## ### ##0.00");
		decimal aux_dolares = calcularTotalPendiente(2);
		lblTotalPendienteDolares.Text = Math.Round(aux_dolares, 2).ToString("## ### ##0.00");
		decimal aux_convertido = convertirSumaDolaresASoles(aux_dolares, aux_soles);
		lblTotalPendienteConvertido.Text = Math.Round(aux_convertido, 2).ToString("## ### ##0.00");
		ln.Clear();
		chkBoxCabecera.Checked = false;
		lblResultadoFiltradoSoles.Text = "0.00";
		lblResultadoFiltradoDolares.Text = "0.00";
		lblResultadoFiltradoConvertido.Text = "0.00";
	}

	private decimal calcularTotalPendiente(int moneda)
	{
		decimal total_pendiente = default(decimal);
		if (rgvPagos.Rows.Count > 0)
		{
			foreach (GridViewRowInfo fila in rgvPagos.Rows)
			{
				if (moneda == Convert.ToInt32(fila.Cells[codMoneda.Name].Value))
				{
					total_pendiente += (decimal)fila.Cells[pendiente.Name].Value;
				}
			}
		}
		return total_pendiente;
	}

	private void CargaLista()
	{
		rgvPagos.DataSource = data;
		if (rbFechaVencimiento.Checked)
		{
			data.DataSource = admfac.MuestraPagosFactura(cmbEstado.SelectedIndex, Convert.ToInt32(cmbEmpresa.SelectedValue), dtpFecha1.Value, dtpFecha2.Value, 2);
		}
		else
		{
			data.DataSource = admfac.MuestraPagosFactura(cmbEstado.SelectedIndex, Convert.ToInt32(cmbEmpresa.SelectedValue), dtpFecha1.Value, dtpFecha2.Value, 1);
		}
		data.Filter = string.Empty;
		filtro = string.Empty;
		rgvPagos.ClearSelection();
	}

	private void frmPagos_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		dtpFecha1.Value = dtpFecha2.Value.AddDays(-90.0);
		cmbEstado.SelectedIndex = 0;
		label7.Text = proveedor.HeaderText;
		label6.Text = proveedor.DataPropertyName;
	}

	private void todos_checkbox_columna(bool estado, RadGridView dgv)
	{
		if (dgv.Rows.Count <= 0)
		{
			return;
		}
		ln.Clear();
		btnPagoMultiple.Enabled = false;
		foreach (GridViewRowInfo row in dgv.Rows)
		{
			row.Cells[chkSelector.Name].Value = estado;
		}
		if (estado)
		{
			foreach (GridViewRowInfo fila in dgv.Rows)
			{
				int n = fila.Index;
				dgv.ClearSelection();
				if (!ln.Contains(n))
				{
					ln.Add(n);
				}
				string valor = "Suma Seleccionado: ";
				decimal nro_soles = default(decimal);
				decimal nro_dolares = default(decimal);
				foreach (int i in ln)
				{
					if (!(dgv.Rows[i].Cells[accion.Name].Value.ToString() == "Muestra Pagos"))
					{
						dgv.Rows[i].Cells[chkSelector.Name].Value = true;
						dgv.Rows[i].IsSelected = true;
						if (Convert.ToInt32(dgv.Rows[i].Cells[codMoneda.Name].Value) == 1)
						{
							nro_soles += Convert.ToDecimal(dgv.Rows[i].Cells[pendiente.Name].Value);
						}
						else
						{
							nro_dolares += Convert.ToDecimal(dgv.Rows[i].Cells[pendiente.Name].Value);
						}
					}
				}
				lblResultadoFiltradoSoles.Text = Math.Round(nro_soles, 2).ToString("## ### ##0.00");
				lblResultadoFiltradoDolares.Text = Math.Round(nro_dolares, 2).ToString("## ### ##0.00");
				decimal monto_convertido = convertirSumaDolaresASoles(nro_dolares, nro_soles);
				lblResultadoFiltradoConvertido.Text = Math.Round(monto_convertido, 2).ToString("## ### ##0.00");
			}
			if (ln.Count > 0 && cmbEstado.Text == "PENDIENTES")
			{
				btnPagoMultiple.Enabled = true;
			}
		}
		else
		{
			lblResultadoFiltradoSoles.Text = "0.00";
			lblResultadoFiltradoDolares.Text = "0.00";
			lblResultadoFiltradoConvertido.Text = "0.00";
		}
		band_checkbox = !estado;
		chkBoxCabecera.Checked = estado;
	}

	private decimal convertirSumaDolaresASoles(decimal monto_dolares, decimal monto_soles)
	{
		double tc_venta = mdi_Menu.tc_hoy;
		return monto_soles + monto_dolares * Convert.ToDecimal(tc_venta);
	}

	private void añadiendoCheckBoxCabecera()
	{
		Point CeldaCabeceraLocacion = dgvPagos.GetCellDisplayRectangle(0, -1, cutOverflow: true).Location;
		chkBoxCabecera.Size = new Size(18, 18);
		chkBoxCabecera.Location = new Point(CeldaCabeceraLocacion.X + 5, CeldaCabeceraLocacion.Y + 2);
		dgvPagos.Controls.Add(chkBoxCabecera);
	}

	private void chkBox_Principal_Clicked()
	{
		if (cmbEstado.Text == "PENDIENTES")
		{
			rgvPagos.EndEdit();
			ln.Clear();
			decimal suma_soles = default(decimal);
			decimal suma_dolares = default(decimal);
			foreach (GridViewRowInfo fila in rgvPagos.ChildRows)
			{
				if (!(fila.Cells[accion.Name].Value.ToString() != "Muestra Pagos"))
				{
					continue;
				}
				if (checkBox1.Checked)
				{
					fila.Cells["chkSelector"].Value = true;
					ln.Add(fila.Index);
					if (Convert.ToInt32(fila.Cells[codMoneda.Name].Value) == 1)
					{
						suma_soles += (decimal)fila.Cells[pendiente.Name].Value;
					}
					else
					{
						suma_dolares += (decimal)fila.Cells[pendiente.Name].Value;
					}
				}
				else
				{
					fila.Cells["chkSelector"].Value = false;
				}
			}
			lblResultadoFiltradoSoles.Text = suma_soles.ToString("## ### ##0.00");
			lblResultadoFiltradoDolares.Text = suma_dolares.ToString("## ### ##0.00");
			decimal suma_convertida = convertirSumaDolaresASoles(suma_dolares, suma_soles);
			lblResultadoFiltradoConvertido.Text = suma_convertida.ToString("## ### ##0.00");
			if (ln.Count > 0)
			{
				btnPagoMultiple.Enabled = true;
			}
			else
			{
				btnPagoMultiple.Enabled = false;
			}
		}
		else
		{
			checkBox1.Checked = !checkBox1.Checked;
		}
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
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

	private void CargaEmpresas()
	{
		cmbEmpresa.DataSource = admEmp.CargaEmpresas();
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
		cmbEmpresa.SelectedValue = frmLogin.iCodEmpresa;
	}

	private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvPagos.Columns[e.ColumnIndex].Name != chkSelector.Name)
		{
			label7.Text = dgvPagos.Columns[e.ColumnIndex].HeaderText;
			label6.Text = dgvPagos.Columns[e.ColumnIndex].DataPropertyName;
			txtFiltro.Focus();
		}
	}

	private void dgvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvPagos.Rows.Count < 1 || e.RowIndex == -1 || e.ColumnIndex == -1)
		{
			return;
		}
		DataGridViewCell celda = dgvPagos.CurrentCell;
		int itipo = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[tipo.Name].Value);
		if (celda.OwningColumn.Name == numdocumento.Name)
		{
			return;
		}
		if (celda.OwningColumn.Name == chkSelector.Name)
		{
			if (cmbEstado.Text == "PENDIENTES")
			{
				celda.Value = !Convert.ToBoolean(celda.Value ?? ((object)false));
			}
		}
		else if (pagoRp.accion == "Ingresar Pago")
		{
			switch (itipo)
			{
			case 1:
			{
				fac.CodFactura = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codnota.Name].Value);
				fac.CodProveedor = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codproveedore.Name].Value.ToString());
				frmCancelarPago form2 = new frmCancelarPago();
				form2.CodNota = fac.CodFactura.ToString();
				form2.tipo = itipo;
				form2.VentComp = 2;
				form2.CodCliente = fac.CodProveedor;
				form2.ShowDialog();
				CargaLista();
				break;
			}
			case 2:
			{
				let.CodLetra = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codnota.Name].Value);
				frmCancelarPago form = new frmCancelarPago();
				form.CodLetra = let.CodLetra;
				form.tipo = itipo;
				form.ShowDialog();
				CargaLista();
				break;
			}
			}
		}
		else if (celda.Value.ToString() == "Muestra Pagos")
		{
			switch (itipo)
			{
			case 1:
			{
				fac.CodFactura = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codnota.Name].Value);
				frmMuestraPagos form4 = new frmMuestraPagos();
				form4.CodNota = fac.CodFactura;
				form4.InOut = false;
				form4.tipo = 0;
				form4.pagocompra = 19;
				form4.Almacen = frmLogin.iCodAlmacen;
				form4.ShowDialog();
				CargaLista();
				break;
			}
			case 2:
			{
				let.CodLetra = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codnota.Name].Value);
				frmMuestraPagos form3 = new frmMuestraPagos();
				form3.CodNota = let.CodLetra;
				form3.InOut = false;
				form3.tipo = 1;
				form3.ShowDialog();
				CargaLista();
				break;
			}
			}
		}
	}

	private void dgvPagos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		dgvPagos.ContextMenuStrip = new ContextMenuStrip();
		if (e.RowIndex == -1)
		{
			return;
		}
		dgvPagos.Rows[e.RowIndex].Selected = true;
		if (e.Button != MouseButtons.Right || e.RowIndex == -1 || Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[estado.Name].Value) != 0 || dgvPagos.SelectedCells.Count <= 0)
		{
			return;
		}
		dgvPagos.ContextMenuStrip = contextMenuStrip1;
		if (Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[tipo.Name].Value) == 1)
		{
			canjearPorLetraToolStripMenuItem.Enabled = true;
			modificarLetraToolStripMenuItem.Enabled = false;
			imprimirLetraToolStripMenuItem.Enabled = false;
			ingresoABancoToolStripMenuItem.Enabled = false;
			if (Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[cantidad.Name].Value) > 0)
			{
				muestraPagosToolStripMenuItem.Enabled = true;
			}
			else
			{
				muestraPagosToolStripMenuItem.Enabled = false;
			}
		}
		else
		{
			canjearPorLetraToolStripMenuItem.Enabled = false;
			modificarLetraToolStripMenuItem.Enabled = true;
			imprimirLetraToolStripMenuItem.Enabled = true;
			ingresoABancoToolStripMenuItem.Enabled = true;
			if (Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[cantidad.Name].Value) > 0)
			{
				muestraPagosToolStripMenuItem.Enabled = true;
			}
			else
			{
				muestraPagosToolStripMenuItem.Enabled = false;
			}
		}
	}

	private void muestraPagosToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GridViewRowInfo Row = rgvPagos.SelectedRows[0];
		fac.CodFactura = Convert.ToInt32(Row.Cells[codnota.Name].Value);
		tipodocumento = Convert.ToInt32(Row.Cells[tipo.Name].Value);
		frmMuestraPagosProveedor form = new frmMuestraPagosProveedor();
		form.CodNota = fac.CodFactura;
		form.tipodocumento = tipodocumento;
		form.InOut = false;
		form.ShowDialog();
		CargaLista();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("Pagos");
		foreach (GridViewDataColumn column in rgvPagos.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < rgvPagos.Rows.Count; i++)
		{
			GridViewRowInfo row = rgvPagos.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < rgvPagos.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\PagoRPT.xml", XmlWriteMode.WriteSchema);
		CRReportePagos rpt = new CRReportePagos();
		frmRptPagos frm = new frmRptPagos();
		rpt.SetDataSource(ds);
		frm.crvReportePagos.ReportSource = rpt;
		frm.Show();
	}

	private void canjearPorLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvPagos.SelectedRows[0];
		fac.CodFactura = Convert.ToInt32(Row.Cells[codnota.Name].Value);
		frmCanjearLetra form = new frmCanjearLetra();
		form.notaI = fac;
		form.Procede = 1;
		form.ShowDialog();
		CargaLista();
	}

	private void nuevaLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmGestionLetra form = new frmGestionLetra();
		form.Proceso = 1;
		form.ShowDialog();
		CargaLista();
	}

	private void modificarLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvPagos.SelectedRows[0];
		let.CodLetra = Convert.ToInt32(Row.Cells[codnota.Name].Value);
		frmGestionLetra form = new frmGestionLetra();
		form.letra = let;
		form.Proceso = 2;
		form.ShowDialog();
		CargaLista();
	}

	private void ingresoABancoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvPagos.CurrentRow;
		frmIngresoBanco form = new frmIngresoBanco();
		form.CodLetra = Convert.ToInt32(Row.Cells[codnota.Name].Value);
		form.Proceso = 1;
		form.ShowDialog();
		CargaLista();
	}

	private void frmPagos_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
	}

	private void canjearPorCuotasToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void dgvPagos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			pagoRp.accion = Convert.ToString(dgvPagos.Rows[e.RowIndex].Cells[accion.Name].Value);
			venta.CodFacturaVenta = Convert.ToString(dgvPagos.Rows[e.RowIndex].Cells[codnota.Name].Value);
		}
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void dgvPagos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvPagos.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == numdocumento.Name && dgvPagos.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodNota = dgvPagos.Rows[e.RowIndex].Cells[codNotaIngreso.Name].Value.ToString();
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnPagoMultiple_Click(object sender, EventArgs e)
	{
		try
		{
			bool bandTipo = true;
			string mensaje_error = "Ocurrio un error. [frmPagos]";
			string titulo_error = "ERROR";
			int n = -100;
			if (ln.Count > 1)
			{
				for (int i = 0; i < ln.Count; i++)
				{
					if (i + 1 != ln.Count)
					{
						int itipo1 = Convert.ToInt32(rgvPagos.ChildRows[ln[i]].Cells[tipo.Name].Value.ToString());
						int itipo2 = Convert.ToInt32(rgvPagos.ChildRows[ln[i + 1]].Cells[tipo.Name].Value.ToString());
						if (itipo1 != itipo2)
						{
							n = ln[i + 1];
							titulo_error = "Incoherencia de Tipo de Pagos";
							mensaje_error = "Error Ocurrido por que las filas seleccionadas no son todas del mismo tipo";
							bandTipo = false;
							break;
						}
						string monedaA = rgvPagos.ChildRows[ln[i]].Cells[moneda.Name].Value.ToString();
						string monedaB = rgvPagos.ChildRows[ln[i + 1]].Cells[moneda.Name].Value.ToString();
						if (monedaA != monedaB)
						{
							n = ln[i + 1];
							titulo_error = "Incoherencia de Tipo de Monedas";
							mensaje_error = "Error Ocurrido por que las filas seleccionadas no tienene la misma MONEDA";
							bandTipo = false;
							break;
						}
					}
				}
			}
			if (bandTipo)
			{
				if (Application.OpenForms["frmCancelarPagoMultiple"] != null)
				{
					Application.OpenForms["frmCancelarPagoMultiple"].Activate();
				}
				else
				{
					int aux_tipo = Convert.ToInt32(rgvPagos.ChildRows[ln[0]].Cells[tipo.Name].Value);
					int aux_moneda = Convert.ToInt32(rgvPagos.ChildRows[ln[0]].Cells[codMoneda.Name].Value ?? ((object)0));
					switch (aux_tipo)
					{
					case 1:
					{
						frmCancelarPagoMultiple form1 = new frmCancelarPagoMultiple();
						form1.tipo = aux_tipo;
						form1.mon = aux_moneda;
						form1.VentComp = 2;
						asignaFilasADGVFormularioMultiple(form1.dataFacturas);
						form1.ShowDialog();
						break;
					}
					case 2:
						MessageBox.Show("Error Pago Multiple - Letra", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						break;
					}
					btnBusqueda.PerformClick();
				}
			}
			if (!bandTipo)
			{
				MessageBox.Show(mensaje_error + ((n != -100) ? ("\nFila Comprometida Nro: " + (n + 1)) : ""), titulo_error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void asignaFilasADGVFormularioMultiple(DataGridView dataFacturas)
	{
		try
		{
			if (ln.Count > 0)
			{
				foreach (int i in ln)
				{
					GridViewRowInfo fila = rgvPagos.ChildRows[i];
					string factura = fila.Cells[numdocumento.Name].Value.ToString();
					string _fechaemision = Convert.ToDateTime(fila.Cells[fechaemision.Name].Value.ToString()).ToString("dd/MM/yyyy");
					string _fechavencimiento = Convert.ToDateTime(fila.Cells[fechavenc.Name].Value.ToString()).ToString("dd/MM/yyyy");
					string _razonsocial = fila.Cells[proveedor.Name].Value.ToString();
					double _monto = Convert.ToDouble(fila.Cells[monto.Name].Value.ToString());
					double _pendiente = Convert.ToDouble(fila.Cells[pendiente.Name].Value.ToString());
					double _montoencuenta = _pendiente;
					double total2 = _pendiente;
					double pendiente2 = _pendiente - total2;
					int codigo = Convert.ToInt32(fila.Cells[codnota.Name].Value);
					bool flag = true;
					dataFacturas.Rows.Add(factura, _fechaemision, _fechavencimiento, _razonsocial, _monto, _pendiente, _montoencuenta, total2.ToString("0.####"), pendiente2.ToString("0.####"), codigo);
				}
				return;
			}
			MessageBox.Show("Ocurrio Un Error Al Enviar Facturas a Pago Multiple", "ERROR");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
	}

	private void lblResultadoFiltradoSoles_Click(object sender, EventArgs e)
	{
	}

	private void rgvPagos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.Row.Index != -1)
		{
			pagoRp.accion = Convert.ToString(rgvPagos.ChildRows[e.Row.Index].Cells[accion.Name].Value);
			venta.CodFacturaVenta = Convert.ToString(rgvPagos.ChildRows[e.Row.Index].Cells[codnota.Name].Value);
		}
		if (rgvPagos.ChildRows.Count < 1 || e.Row.Index == -1 || e.ColumnIndex == -1)
		{
			return;
		}
		GridDataCellElement celda = rgvPagos.CurrentCell;
		GridViewColumn col = rgvPagos.Columns[e.ColumnIndex];
		int itipo = Convert.ToInt32(rgvPagos.ChildRows[e.Row.Index].Cells[tipo.Name].Value);
		if (col.Name == numdocumento.Name)
		{
			return;
		}
		if (col.Name == chkSelector.Name)
		{
			if (cmbEstado.Text == "PENDIENTES")
			{
				celda.Value = !Convert.ToBoolean(celda.Value ?? ((object)false));
				calcularPendientesAlSeleccionar(e);
			}
		}
		else if (pagoRp.accion == "Ingresar Pago")
		{
			switch (itipo)
			{
			case 1:
			{
				fac.CodFactura = Convert.ToInt32(rgvPagos.ChildRows[e.Row.Index].Cells[codnota.Name].Value);
				fac.CodProveedor = Convert.ToInt32(rgvPagos.ChildRows[e.Row.Index].Cells[codproveedore.Name].Value.ToString());
				frmCancelarPago form2 = new frmCancelarPago();
				form2.CodNota = fac.CodFactura.ToString();
				form2.tipo = itipo;
				form2.VentComp = 2;
				form2.CodCliente = fac.CodProveedor;
				form2.ShowDialog();
				CargaLista();
				break;
			}
			case 2:
			{
				let.CodLetra = Convert.ToInt32(rgvPagos.ChildRows[e.Row.Index].Cells[codnota.Name].Value);
				frmCancelarPago form = new frmCancelarPago();
				form.CodLetra = let.CodLetra;
				form.tipo = itipo;
				form.ShowDialog();
				CargaLista();
				break;
			}
			}
		}
		else if (celda.Value.ToString() == "Muestra Pagos")
		{
			switch (itipo)
			{
			case 1:
			{
				fac.CodFactura = Convert.ToInt32(rgvPagos.ChildRows[e.Row.Index].Cells[codnota.Name].Value);
				frmMuestraPagos form4 = new frmMuestraPagos();
				form4.CodNota = fac.CodFactura;
				form4.InOut = false;
				form4.tipo = 0;
				form4.pagocompra = 19;
				form4.Almacen = frmLogin.iCodAlmacen;
				form4.ShowDialog();
				CargaLista();
				break;
			}
			case 2:
			{
				let.CodLetra = Convert.ToInt32(rgvPagos.ChildRows[e.Row.Index].Cells[codnota.Name].Value);
				frmMuestraPagos form3 = new frmMuestraPagos();
				form3.CodNota = let.CodLetra;
				form3.InOut = false;
				form3.tipo = 1;
				form3.ShowDialog();
				CargaLista();
				break;
			}
			}
		}
	}

	private void calcularPendientesAlSeleccionar(GridViewCellEventArgs e)
	{
		try
		{
			if (e.ColumnIndex != 0)
			{
				return;
			}
			int n = e.Row.Index;
			rgvPagos.ChildRows[n].Cells[e.ColumnIndex].Value = false;
			rgvPagos.ChildRows[n].IsSelected = false;
			rgvPagos.ClearSelection();
			if (ln.Contains(n))
			{
				ln.Remove(n);
				band_checkbox = true;
			}
			else if (rgvPagos.ChildRows[n].Cells[accion.Name].Value.ToString() == "Muestra Pagos")
			{
				MessageBox.Show("Imposible Seleccionar", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				ln.Add(n);
			}
			decimal nro_soles = default(decimal);
			decimal nro_dolares = default(decimal);
			foreach (int i in ln)
			{
				if (rgvPagos.ChildRows[i].Cells[accion.Name].Value.ToString() == "Muestra Pagos")
				{
					MessageBox.Show("Imposible Seleccionar", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					continue;
				}
				rgvPagos.ChildRows[i].Cells[e.ColumnIndex].Value = true;
				rgvPagos.ChildRows[i].IsSelected = true;
				if (Convert.ToInt32(rgvPagos.ChildRows[i].Cells[codMoneda.Name].Value) == 1)
				{
					nro_soles += Convert.ToDecimal(rgvPagos.ChildRows[i].Cells[pendiente.Name].Value ?? ((object)0));
				}
				else
				{
					nro_dolares += Convert.ToDecimal(rgvPagos.ChildRows[i].Cells[pendiente.Name].Value ?? ((object)0));
				}
			}
			if (ln.Count > 0)
			{
				btnPagoMultiple.Enabled = true;
			}
			else
			{
				btnPagoMultiple.Enabled = false;
			}
			if (ln.Count == rgvPagos.ChildRows.Count)
			{
				band_checkbox = false;
				chkBoxCabecera.Checked = true;
			}
			lblResultadoFiltradoSoles.Text = Math.Round(nro_soles, 2).ToString("## ### ##0.00");
			lblResultadoFiltradoDolares.Text = Math.Round(nro_dolares, 2).ToString("## ### ##0.00");
			decimal nro_convertido = convertirSumaDolaresASoles(nro_dolares, nro_soles);
			lblResultadoFiltradoConvertido.Text = Math.Round(nro_convertido, 2).ToString("## ### ##0.00");
			if (ln.Count == rgvPagos.ChildRows.Count - 1)
			{
				chkBoxCabecera.Checked = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Seleccionar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void rgvPagos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvPagos.Columns[e.ColumnIndex].Name == numdocumento.Name && rgvPagos.ChildRows.Count >= 1 && e.Row.Index != -1)
		{
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodNota = rgvPagos.ChildRows[e.Row.Index].Cells[codNotaIngreso.Name].Value.ToString();
			form.Proceso = 3;
			form.Show();
		}
	}

	private void checkBox1_Click(object sender, EventArgs e)
	{
		chkBox_Principal_Clicked();
	}

	private void rgvPagos_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
	{
	}

	private void btnexcel_Click(object sender, EventArgs e)
	{
		decimal totalMonto = default(decimal);
		decimal totalPendiente = default(decimal);
		int ColInicial = 0;
		int RowInicial = 2;
		SLDocument sl = new SLDocument();
		SLStyle styleCenter = sl.CreateStyle();
		styleCenter.Alignment.Horizontal = HorizontalAlignmentValues.Center;
		styleCenter.Font.FontSize = 10.0;
		styleCenter.Font.Bold = true;
		foreach (GridViewDataColumn column in rgvPagos.Columns)
		{
			if (column.IsVisible && column.HeaderText != string.Empty)
			{
				ColInicial++;
				sl.SetCellValue(1, 1, "Lista de Pagos");
				sl.SetCellStyle(1, 1, styleCenter);
				sl.MergeWorksheetCells("A1", "P1");
				sl.SetCellValue(2, ColInicial, column.HeaderText.ToString());
				sl.SetCellStyle(RowInicial, ColInicial, styleCenter);
			}
		}
		sl.SetColumnWidth(1, 20.0);
		sl.SetColumnWidth(2, 10.0);
		sl.SetColumnWidth(3, 35.0);
		sl.SetColumnWidth(4, 20.0);
		sl.SetColumnWidth(5, 20.0);
		sl.SetColumnWidth(6, 20.0);
		sl.SetColumnWidth(7, 15.0);
		sl.SetColumnWidth(8, 15.0);
		sl.SetColumnWidth(9, 16.0);
		sl.SetColumnWidth(10, 16.0);
		sl.SetColumnWidth(11, 15.0);
		sl.SetColumnWidth(12, 15.0);
		sl.SetColumnWidth(13, 15.0);
		sl.SetColumnWidth(14, 0.1);
		sl.SetColumnWidth(15, 40.0);
		sl.SetColumnWidth(16, 35.0);
		foreach (GridViewRowInfo row in rgvPagos.ChildRows)
		{
			RowInicial++;
			sl.SetCellValue(RowInicial, 1, row.Cells["numdocumento"].Value.ToString());
			sl.SetCellValue(RowInicial, 2, row.Cells["docref"].Value.ToString());
			sl.SetCellValue(RowInicial, 3, row.Cells["proveedor"].Value.ToString());
			sl.SetCellValue(RowInicial, 4, row.Cells["fechaemision"].Value.ToString());
			sl.SetCellValue(RowInicial, 5, row.Cells["fechavenc"].Value.ToString());
			sl.SetCellValue(RowInicial, 6, row.Cells["fechacancelado"].Value.ToString());
			sl.SetCellValue(RowInicial, 7, row.Cells["moneda"].Value.ToString());
			sl.SetCellValue(RowInicial, 8, row.Cells["formapago"].Value.ToString());
			decimal monto = decimal.Parse(row.Cells["monto"].Value.ToString());
			sl.SetCellValue(RowInicial, 9, monto);
			decimal pendiente = decimal.Parse(row.Cells["pendiente"].Value.ToString());
			sl.SetCellValue(RowInicial, 10, pendiente);
			sl.SetCellValue(RowInicial, 11, row.Cells["banco"].Value.ToString());
			sl.SetCellValue(RowInicial, 12, row.Cells["numcuenta"].Value.ToString());
			sl.SetCellValue(RowInicial, 13, row.Cells["numunico"].Value.ToString());
			sl.SetCellValue(RowInicial, 14, row.Cells["accion"].Value.ToString());
			sl.SetCellValue(RowInicial, 15, row.Cells["comenfac"].Value.ToString());
			sl.SetCellValue(RowInicial, 16, row.Cells["comenpago"].Value.ToString());
			totalMonto += monto;
			totalPendiente += pendiente;
		}
		sl.SetCellValue(RowInicial + 8, 9, totalMonto);
		sl.SetCellValue(RowInicial + 8, 10, totalPendiente);
		SLStyle styleFormat = sl.CreateStyle();
		styleFormat.Alignment.Horizontal = HorizontalAlignmentValues.Right;
		styleFormat.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		styleFormat.Font.Bold = true;
		styleFormat.FormatCode = "[Black]#,##0.00";
		sl.SetCellStyle(RowInicial + 8, 9, styleFormat);
		styleFormat.Border.TopBorder.Color = System.Drawing.Color.Black;
		sl.SetCellStyle(RowInicial + 8, 10, styleFormat);
		SLStyle styleColor = sl.CreateStyle();
		styleColor.SetFontColor(System.Drawing.Color.White);
		styleColor.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(115, 172, 173), System.Drawing.Color.Brown);
		sl.SetCellStyle(1, 1, 1, ColInicial, styleColor);
		SLStyle styleBorder = sl.CreateStyle();
		asignarBordes(styleBorder);
		sl.SetCellStyle(1, 1, 2, ColInicial, styleBorder);
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
			MessageBox.Show(ex.Message, base.Name + " - Line 177");
		}
	}

	private string obtenerRutaParaGuardar()
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "Guardando Archivo de Excel";
			sfd.FileName = "Exportacion_Listado_de_Pagos";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPagos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn3 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn47 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn48 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn49 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn50 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn51 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn52 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn53 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn54 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn55 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn56 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn57 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn58 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn59 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn60 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn61 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn62 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn63 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewHyperlinkColumn gridViewHyperlinkColumn3 = new Telerik.WinControls.UI.GridViewHyperlinkColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn64 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn65 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn66 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn67 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn68 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn69 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnexcel = new System.Windows.Forms.Button();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.btnPagoMultiple = new System.Windows.Forms.Button();
		this.lblResultadoFiltradoConvertido = new System.Windows.Forms.Label();
		this.lblTotalPendienteSoles = new System.Windows.Forms.Label();
		this.lblTotalPendienteDolares = new System.Windows.Forms.Label();
		this.lblResultadoFiltradoDolares = new System.Windows.Forms.Label();
		this.lblResultadoFiltradoSoles = new System.Windows.Forms.Label();
		this.gbRadioSelector = new System.Windows.Forms.GroupBox();
		this.rbFechaVencimiento = new System.Windows.Forms.RadioButton();
		this.rbFechaEmision = new System.Windows.Forms.RadioButton();
		this.lblTotalPendienteConvertido = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.dgvPagos = new System.Windows.Forms.DataGridView();
		this.chkSelector = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.codnota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproveedore = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechavenc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechacancelado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numcuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numunico = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.accion = new System.Windows.Forms.DataGridViewLinkColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codNotaIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.muestraPagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.canjearPorLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.nuevaLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modificarLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.imprimirLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ingresoABancoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.rgvPagos = new Telerik.WinControls.UI.RadGridView();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		this.gbRadioSelector.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvPagos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvPagos.MasterTemplate).BeginInit();
		this.rgvPagos.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "folder_open (1).png");
		this.imageList1.Images.SetKeyName(17, "folder-open-icon.png");
		this.imageList1.Images.SetKeyName(18, "Glossy-Open-icon.png");
		this.imageList1.Images.SetKeyName(19, "Ocean Blue Open.png");
		this.imageList1.Images.SetKeyName(20, "Open (1).png");
		this.imageList1.Images.SetKeyName(21, "open_folder_green.png");
		this.btnBuscar.ImageIndex = 11;
		this.btnBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.SubItemsExpandWidth = 14;
		this.btnBuscar.Text = "Buscar";
		this.buttonItem1.ImageIndex = 11;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Buscar";
		this.groupBox1.Controls.Add(this.btnexcel);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.btnPagoMultiple);
		this.groupBox1.Controls.Add(this.lblResultadoFiltradoConvertido);
		this.groupBox1.Controls.Add(this.lblTotalPendienteSoles);
		this.groupBox1.Controls.Add(this.lblTotalPendienteDolares);
		this.groupBox1.Controls.Add(this.lblResultadoFiltradoDolares);
		this.groupBox1.Controls.Add(this.lblResultadoFiltradoSoles);
		this.groupBox1.Controls.Add(this.gbRadioSelector);
		this.groupBox1.Controls.Add(this.lblTotalPendienteConvertido);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbEmpresa);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbEstado);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1390, 109);
		this.groupBox1.TabIndex = 7;
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.btnexcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnexcel.Location = new System.Drawing.Point(516, 9);
		this.btnexcel.Name = "btnexcel";
		this.btnexcel.Size = new System.Drawing.Size(101, 50);
		this.btnexcel.TabIndex = 2;
		this.btnexcel.Text = "Excel";
		this.btnexcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnexcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnexcel.UseVisualStyleBackColor = true;
		this.btnexcel.Click += new System.EventHandler(btnexcel_Click);
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(989, 51);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(110, 13);
		this.label13.TabIndex = 84;
		this.label13.Text = "Suma Filtrado S/: ";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(988, 36);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(119, 13);
		this.label14.TabIndex = 85;
		this.label14.Text = "Total Pendiente S/:";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.Color.Green;
		this.label11.Location = new System.Drawing.Point(736, 60);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(112, 13);
		this.label11.TabIndex = 83;
		this.label11.Text = "Total Pendiente $:";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.ForeColor = System.Drawing.Color.Green;
		this.label12.Location = new System.Drawing.Point(736, 80);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(103, 13);
		this.label12.TabIndex = 82;
		this.label12.Text = "Suma Filtrado $: ";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Goldenrod;
		this.label3.Location = new System.Drawing.Point(735, 13);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(123, 13);
		this.label3.TabIndex = 81;
		this.label3.Text = "Total Pendiente S/.:";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.Goldenrod;
		this.label8.Location = new System.Drawing.Point(735, 33);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(110, 13);
		this.label8.TabIndex = 80;
		this.label8.Text = "Suma Filtrado S/: ";
		this.btnPagoMultiple.Enabled = false;
		this.btnPagoMultiple.Location = new System.Drawing.Point(627, 35);
		this.btnPagoMultiple.Name = "btnPagoMultiple";
		this.btnPagoMultiple.Size = new System.Drawing.Size(86, 33);
		this.btnPagoMultiple.TabIndex = 74;
		this.btnPagoMultiple.Text = "Pago Multiple";
		this.btnPagoMultiple.UseVisualStyleBackColor = true;
		this.btnPagoMultiple.Click += new System.EventHandler(btnPagoMultiple_Click);
		this.lblResultadoFiltradoConvertido.AutoSize = true;
		this.lblResultadoFiltradoConvertido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblResultadoFiltradoConvertido.Location = new System.Drawing.Point(1115, 53);
		this.lblResultadoFiltradoConvertido.Name = "lblResultadoFiltradoConvertido";
		this.lblResultadoFiltradoConvertido.Size = new System.Drawing.Size(32, 13);
		this.lblResultadoFiltradoConvertido.TabIndex = 78;
		this.lblResultadoFiltradoConvertido.Text = "0.00";
		this.lblResultadoFiltradoConvertido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblTotalPendienteSoles.AutoSize = true;
		this.lblTotalPendienteSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalPendienteSoles.ForeColor = System.Drawing.Color.Goldenrod;
		this.lblTotalPendienteSoles.Location = new System.Drawing.Point(864, 13);
		this.lblTotalPendienteSoles.Name = "lblTotalPendienteSoles";
		this.lblTotalPendienteSoles.Size = new System.Drawing.Size(32, 13);
		this.lblTotalPendienteSoles.TabIndex = 73;
		this.lblTotalPendienteSoles.Text = "0.00";
		this.lblTotalPendienteSoles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblTotalPendienteDolares.AutoSize = true;
		this.lblTotalPendienteDolares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalPendienteDolares.ForeColor = System.Drawing.Color.Green;
		this.lblTotalPendienteDolares.Location = new System.Drawing.Point(864, 63);
		this.lblTotalPendienteDolares.Name = "lblTotalPendienteDolares";
		this.lblTotalPendienteDolares.Size = new System.Drawing.Size(32, 13);
		this.lblTotalPendienteDolares.TabIndex = 77;
		this.lblTotalPendienteDolares.Text = "0.00";
		this.lblTotalPendienteDolares.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblResultadoFiltradoDolares.AutoSize = true;
		this.lblResultadoFiltradoDolares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblResultadoFiltradoDolares.ForeColor = System.Drawing.Color.Green;
		this.lblResultadoFiltradoDolares.Location = new System.Drawing.Point(864, 80);
		this.lblResultadoFiltradoDolares.Name = "lblResultadoFiltradoDolares";
		this.lblResultadoFiltradoDolares.Size = new System.Drawing.Size(32, 13);
		this.lblResultadoFiltradoDolares.TabIndex = 76;
		this.lblResultadoFiltradoDolares.Text = "0.00";
		this.lblResultadoFiltradoDolares.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblResultadoFiltradoSoles.AutoSize = true;
		this.lblResultadoFiltradoSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblResultadoFiltradoSoles.ForeColor = System.Drawing.Color.Goldenrod;
		this.lblResultadoFiltradoSoles.Location = new System.Drawing.Point(864, 33);
		this.lblResultadoFiltradoSoles.Name = "lblResultadoFiltradoSoles";
		this.lblResultadoFiltradoSoles.Size = new System.Drawing.Size(32, 13);
		this.lblResultadoFiltradoSoles.TabIndex = 72;
		this.lblResultadoFiltradoSoles.Text = "0.00";
		this.lblResultadoFiltradoSoles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblResultadoFiltradoSoles.Click += new System.EventHandler(lblResultadoFiltradoSoles_Click);
		this.gbRadioSelector.Controls.Add(this.rbFechaVencimiento);
		this.gbRadioSelector.Controls.Add(this.rbFechaEmision);
		this.gbRadioSelector.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.gbRadioSelector.Location = new System.Drawing.Point(10, 44);
		this.gbRadioSelector.Name = "gbRadioSelector";
		this.gbRadioSelector.Size = new System.Drawing.Size(120, 53);
		this.gbRadioSelector.TabIndex = 40;
		this.gbRadioSelector.TabStop = false;
		this.rbFechaVencimiento.AutoSize = true;
		this.rbFechaVencimiento.Location = new System.Drawing.Point(6, 30);
		this.rbFechaVencimiento.Name = "rbFechaVencimiento";
		this.rbFechaVencimiento.Size = new System.Drawing.Size(116, 17);
		this.rbFechaVencimiento.TabIndex = 39;
		this.rbFechaVencimiento.Text = "Fecha Vencimiento";
		this.rbFechaVencimiento.UseVisualStyleBackColor = true;
		this.rbFechaEmision.AutoSize = true;
		this.rbFechaEmision.Checked = true;
		this.rbFechaEmision.Location = new System.Drawing.Point(6, 15);
		this.rbFechaEmision.Name = "rbFechaEmision";
		this.rbFechaEmision.Size = new System.Drawing.Size(94, 17);
		this.rbFechaEmision.TabIndex = 38;
		this.rbFechaEmision.TabStop = true;
		this.rbFechaEmision.Text = "Fecha Emision";
		this.rbFechaEmision.UseVisualStyleBackColor = true;
		this.lblTotalPendienteConvertido.AutoSize = true;
		this.lblTotalPendienteConvertido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalPendienteConvertido.Location = new System.Drawing.Point(1115, 36);
		this.lblTotalPendienteConvertido.Name = "lblTotalPendienteConvertido";
		this.lblTotalPendienteConvertido.Size = new System.Drawing.Size(32, 13);
		this.lblTotalPendienteConvertido.TabIndex = 79;
		this.lblTotalPendienteConvertido.Text = "0.00";
		this.lblTotalPendienteConvertido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label6.Location = new System.Drawing.Point(559, 4);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 37;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(314, 56);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 36;
		this.label5.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(349, 56);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 35;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(276, 56);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 34;
		this.label10.Text = "Filtro";
		this.txtFiltro.Location = new System.Drawing.Point(278, 71);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 33;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(6, 5);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(49, 12);
		this.label9.TabIndex = 32;
		this.label9.Text = "Empresa";
		this.cmbEmpresa.Enabled = false;
		this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(9, 21);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(121, 20);
		this.cmbEmpresa.TabIndex = 31;
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList1;
		this.btnBusqueda.Location = new System.Drawing.Point(528, 64);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(78, 33);
		this.btnBusqueda.TabIndex = 30;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.btnReporte.ImageIndex = 7;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(528, 13);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 33);
		this.btnReporte.TabIndex = 29;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(275, 5);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 12);
		this.label4.TabIndex = 28;
		this.label4.Text = "Estado";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(158, 54);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 26;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(158, 7);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 25;
		this.label1.Text = "Desde";
		this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[3] { "PENDIENTES", "CANCELADOS", "TODOS" });
		this.cmbEstado.Location = new System.Drawing.Point(278, 21);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(121, 20);
		this.cmbEstado.TabIndex = 24;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(161, 70);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 22;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(161, 23);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 21;
		this.dgvPagos.AllowUserToAddRows = false;
		this.dgvPagos.AllowUserToDeleteRows = false;
		this.dgvPagos.AllowUserToResizeRows = false;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvPagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
		this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvPagos.Columns.AddRange(this.chkSelector, this.codnota, this.tipo, this.numdocumento, this.docref, this.codproveedore, this.ruc, this.proveedor, this.fechaemision, this.fechavenc, this.fechacancelado, this.moneda, this.monto, this.pendiente, this.banco, this.numcuenta, this.numunico, this.accion, this.cantidad, this.estado, this.codNotaIngreso, this.codMoneda);
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvPagos.DefaultCellStyle = dataGridViewCellStyle20;
		this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvPagos.Location = new System.Drawing.Point(0, 109);
		this.dgvPagos.MultiSelect = false;
		this.dgvPagos.Name = "dgvPagos";
		this.dgvPagos.ReadOnly = true;
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvPagos.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
		this.dgvPagos.RowHeadersVisible = false;
		this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPagos.Size = new System.Drawing.Size(1390, 663);
		this.dgvPagos.TabIndex = 8;
		this.dgvPagos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPagos_CellClick);
		this.dgvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPagos_CellContentClick);
		this.dgvPagos.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPagos_CellContentDoubleClick);
		this.dgvPagos.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvPagos_CellMouseDown);
		this.dgvPagos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_ColumnHeaderMouseClick);
		this.chkSelector.Frozen = true;
		this.chkSelector.HeaderText = "#";
		this.chkSelector.Name = "chkSelector";
		this.chkSelector.ReadOnly = true;
		this.chkSelector.Width = 25;
		this.codnota.DataPropertyName = "codigo";
		this.codnota.HeaderText = "CodNota";
		this.codnota.Name = "codnota";
		this.codnota.ReadOnly = true;
		this.codnota.Visible = false;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "Tipo";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Visible = false;
		this.numdocumento.DataPropertyName = "numdocumento";
		this.numdocumento.HeaderText = "N° Documento";
		this.numdocumento.Name = "numdocumento";
		this.numdocumento.ReadOnly = true;
		this.numdocumento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.numdocumento.Width = 90;
		this.docref.DataPropertyName = "docref";
		this.docref.HeaderText = "Doc. Ref.";
		this.docref.Name = "docref";
		this.docref.ReadOnly = true;
		this.docref.Width = 90;
		this.codproveedore.DataPropertyName = "codProveedor";
		this.codproveedore.HeaderText = "Cod Prov";
		this.codproveedore.Name = "codproveedore";
		this.codproveedore.ReadOnly = true;
		this.codproveedore.Visible = false;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "RUC";
		this.ruc.Name = "ruc";
		this.ruc.ReadOnly = true;
		this.ruc.Visible = false;
		this.proveedor.DataPropertyName = "razonsocial";
		this.proveedor.HeaderText = "Proveedor";
		this.proveedor.Name = "proveedor";
		this.proveedor.ReadOnly = true;
		this.proveedor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.proveedor.Width = 200;
		this.fechaemision.DataPropertyName = "fechaemision";
		this.fechaemision.HeaderText = "Fecha Emi.";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechaemision.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechaemision.Width = 80;
		this.fechavenc.DataPropertyName = "fechavence";
		dataGridViewCellStyle22.Format = "d";
		dataGridViewCellStyle22.NullValue = null;
		this.fechavenc.DefaultCellStyle = dataGridViewCellStyle22;
		this.fechavenc.HeaderText = "Fecha Venc.";
		this.fechavenc.Name = "fechavenc";
		this.fechavenc.ReadOnly = true;
		this.fechavenc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechavenc.Width = 80;
		this.fechacancelado.DataPropertyName = "fechacancelado";
		this.fechacancelado.HeaderText = "Fec. Canc.";
		this.fechacancelado.Name = "fechacancelado";
		this.fechacancelado.ReadOnly = true;
		this.fechacancelado.Width = 80;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.moneda.Width = 60;
		this.monto.DataPropertyName = "total";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.monto.DefaultCellStyle = dataGridViewCellStyle23;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.monto.Width = 70;
		this.pendiente.DataPropertyName = "pendiente";
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.pendiente.DefaultCellStyle = dataGridViewCellStyle24;
		this.pendiente.HeaderText = "Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.pendiente.Width = 70;
		this.banco.DataPropertyName = "banco";
		this.banco.HeaderText = "Banco";
		this.banco.Name = "banco";
		this.banco.ReadOnly = true;
		this.banco.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.banco.Width = 120;
		this.numcuenta.DataPropertyName = "ctacte";
		this.numcuenta.HeaderText = "N° Cuenta";
		this.numcuenta.Name = "numcuenta";
		this.numcuenta.ReadOnly = true;
		this.numcuenta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.numcuenta.Width = 120;
		this.numunico.DataPropertyName = "num";
		this.numunico.HeaderText = "N° Unico";
		this.numunico.Name = "numunico";
		this.numunico.ReadOnly = true;
		this.numunico.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.numunico.Width = 80;
		this.accion.DataPropertyName = "accion";
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.accion.DefaultCellStyle = dataGridViewCellStyle25;
		this.accion.HeaderText = "Acción";
		this.accion.Name = "accion";
		this.accion.ReadOnly = true;
		this.accion.Text = "";
		this.cantidad.DataPropertyName = "cantpagos";
		this.cantidad.HeaderText = "Cant. Pagos";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.cantidad.Visible = false;
		this.estado.DataPropertyName = "cancelado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.codNotaIngreso.DataPropertyName = "codNotaI";
		this.codNotaIngreso.HeaderText = "codNotaIngreso";
		this.codNotaIngreso.Name = "codNotaIngreso";
		this.codNotaIngreso.ReadOnly = true;
		this.codNotaIngreso.Visible = false;
		this.codMoneda.DataPropertyName = "codMoneda";
		this.codMoneda.HeaderText = "codMoneda";
		this.codMoneda.Name = "codMoneda";
		this.codMoneda.ReadOnly = true;
		this.codMoneda.Visible = false;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.muestraPagosToolStripMenuItem, this.canjearPorLetraToolStripMenuItem, this.toolStripSeparator1, this.nuevaLetraToolStripMenuItem, this.modificarLetraToolStripMenuItem, this.imprimirLetraToolStripMenuItem, this.ingresoABancoToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(165, 142);
		this.muestraPagosToolStripMenuItem.Name = "muestraPagosToolStripMenuItem";
		this.muestraPagosToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.muestraPagosToolStripMenuItem.Text = "Muestra Pagos";
		this.muestraPagosToolStripMenuItem.Click += new System.EventHandler(muestraPagosToolStripMenuItem_Click);
		this.canjearPorLetraToolStripMenuItem.Name = "canjearPorLetraToolStripMenuItem";
		this.canjearPorLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.canjearPorLetraToolStripMenuItem.Text = "Canjear por Letra";
		this.canjearPorLetraToolStripMenuItem.Click += new System.EventHandler(canjearPorLetraToolStripMenuItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
		this.nuevaLetraToolStripMenuItem.Name = "nuevaLetraToolStripMenuItem";
		this.nuevaLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.nuevaLetraToolStripMenuItem.Text = "Nueva Letra";
		this.nuevaLetraToolStripMenuItem.Click += new System.EventHandler(nuevaLetraToolStripMenuItem_Click);
		this.modificarLetraToolStripMenuItem.Name = "modificarLetraToolStripMenuItem";
		this.modificarLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.modificarLetraToolStripMenuItem.Text = "Modificar Letra";
		this.modificarLetraToolStripMenuItem.Click += new System.EventHandler(modificarLetraToolStripMenuItem_Click);
		this.imprimirLetraToolStripMenuItem.Name = "imprimirLetraToolStripMenuItem";
		this.imprimirLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.imprimirLetraToolStripMenuItem.Text = "Imprimir Letra";
		this.ingresoABancoToolStripMenuItem.Name = "ingresoABancoToolStripMenuItem";
		this.ingresoABancoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.ingresoABancoToolStripMenuItem.Text = "Ingreso a banco";
		this.ingresoABancoToolStripMenuItem.Click += new System.EventHandler(ingresoABancoToolStripMenuItem_Click);
		this.rgvPagos.Controls.Add(this.checkBox1);
		this.rgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvPagos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.rgvPagos.Location = new System.Drawing.Point(0, 109);
		this.rgvPagos.MasterTemplate.AllowAddNewRow = false;
		this.rgvPagos.MasterTemplate.AllowDragToGroup = false;
		gridViewCheckBoxColumn3.AllowSort = false;
		gridViewCheckBoxColumn3.EnableExpressionEditor = false;
		gridViewCheckBoxColumn3.HeaderText = "";
		gridViewCheckBoxColumn3.Name = "chkSelector";
		gridViewCheckBoxColumn3.Width = 23;
		gridViewTextBoxColumn47.FieldName = "codigo";
		gridViewTextBoxColumn47.HeaderText = "CodNota";
		gridViewTextBoxColumn47.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn47.IsVisible = false;
		gridViewTextBoxColumn47.Name = "codnota";
		gridViewTextBoxColumn47.Width = 100;
		gridViewTextBoxColumn48.FieldName = "tipo";
		gridViewTextBoxColumn48.HeaderText = "Tipo";
		gridViewTextBoxColumn48.IsVisible = false;
		gridViewTextBoxColumn48.Name = "tipo";
		gridViewTextBoxColumn48.Width = 100;
		gridViewTextBoxColumn49.FieldName = "numdocumento";
		gridViewTextBoxColumn49.HeaderText = "N° Documento";
		gridViewTextBoxColumn49.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn49.Name = "numdocumento";
		gridViewTextBoxColumn49.Width = 90;
		gridViewTextBoxColumn50.FieldName = "docref";
		gridViewTextBoxColumn50.HeaderText = "Doc. Ref.";
		gridViewTextBoxColumn50.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn50.Name = "docref";
		gridViewTextBoxColumn50.Width = 90;
		gridViewTextBoxColumn51.FieldName = "codProveedor";
		gridViewTextBoxColumn51.HeaderText = "Cod Prov";
		gridViewTextBoxColumn51.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn51.IsVisible = false;
		gridViewTextBoxColumn51.Name = "codproveedore";
		gridViewTextBoxColumn51.Width = 100;
		gridViewTextBoxColumn52.FieldName = "ruc";
		gridViewTextBoxColumn52.HeaderText = "RUC";
		gridViewTextBoxColumn52.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn52.IsVisible = false;
		gridViewTextBoxColumn52.Name = "ruc";
		gridViewTextBoxColumn52.Width = 100;
		gridViewTextBoxColumn53.FieldName = "razonsocial";
		gridViewTextBoxColumn53.HeaderText = "Proveedor";
		gridViewTextBoxColumn53.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn53.Name = "proveedor";
		gridViewTextBoxColumn53.Width = 200;
		gridViewTextBoxColumn54.FieldName = "fechaemision";
		gridViewTextBoxColumn54.HeaderText = "Fecha Emi.";
		gridViewTextBoxColumn54.Name = "fechaemision";
		gridViewTextBoxColumn54.Width = 80;
		gridViewTextBoxColumn55.FieldName = "fechavence";
		gridViewTextBoxColumn55.HeaderText = "Fecha Venc.";
		gridViewTextBoxColumn55.Name = "fechavenc";
		gridViewTextBoxColumn55.Width = 80;
		gridViewTextBoxColumn56.FieldName = "fechacancelado";
		gridViewTextBoxColumn56.HeaderText = "Fec. Canc.";
		gridViewTextBoxColumn56.Name = "fechacancelado";
		gridViewTextBoxColumn56.Width = 80;
		gridViewTextBoxColumn57.FieldName = "moneda";
		gridViewTextBoxColumn57.HeaderText = "Moneda";
		gridViewTextBoxColumn57.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn57.Name = "moneda";
		gridViewTextBoxColumn57.Width = 60;
		gridViewTextBoxColumn58.FieldName = "formapago";
		gridViewTextBoxColumn58.HeaderText = "Forma Pago";
		gridViewTextBoxColumn58.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn58.Name = "formapago";
		gridViewTextBoxColumn58.Width = 120;
		gridViewTextBoxColumn59.FieldName = "total";
		gridViewTextBoxColumn59.HeaderText = "Monto";
		gridViewTextBoxColumn59.Name = "monto";
		gridViewTextBoxColumn59.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn59.Width = 70;
		gridViewTextBoxColumn60.FieldName = "pendiente";
		gridViewTextBoxColumn60.HeaderText = "Pendiente";
		gridViewTextBoxColumn60.Name = "pendiente";
		gridViewTextBoxColumn60.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn60.Width = 70;
		gridViewTextBoxColumn61.FieldName = "banco";
		gridViewTextBoxColumn61.HeaderText = "Banco";
		gridViewTextBoxColumn61.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn61.Name = "banco";
		gridViewTextBoxColumn61.Width = 120;
		gridViewTextBoxColumn62.FieldName = "ctacte";
		gridViewTextBoxColumn62.HeaderText = "N° Cuenta";
		gridViewTextBoxColumn62.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn62.Name = "numcuenta";
		gridViewTextBoxColumn62.Width = 120;
		gridViewTextBoxColumn63.FieldName = "num";
		gridViewTextBoxColumn63.HeaderText = "N° Unico";
		gridViewTextBoxColumn63.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn63.Name = "numunico";
		gridViewTextBoxColumn63.Width = 80;
		gridViewHyperlinkColumn3.FieldName = "accion";
		gridViewHyperlinkColumn3.HeaderText = "Acción";
		gridViewHyperlinkColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewHyperlinkColumn3.Name = "accion";
		gridViewHyperlinkColumn3.Width = 100;
		gridViewTextBoxColumn64.FieldName = "cantpagos";
		gridViewTextBoxColumn64.HeaderText = "Cant. Pagos";
		gridViewTextBoxColumn64.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn64.IsVisible = false;
		gridViewTextBoxColumn64.Name = "cantidad";
		gridViewTextBoxColumn64.Width = 100;
		gridViewTextBoxColumn65.FieldName = "cancelado";
		gridViewTextBoxColumn65.HeaderText = "Estado";
		gridViewTextBoxColumn65.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn65.IsVisible = false;
		gridViewTextBoxColumn65.Name = "estado";
		gridViewTextBoxColumn65.Width = 100;
		gridViewTextBoxColumn66.FieldName = "codNotaI";
		gridViewTextBoxColumn66.HeaderText = "codNotaIngreso";
		gridViewTextBoxColumn66.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn66.IsVisible = false;
		gridViewTextBoxColumn66.Name = "codNotaIngreso";
		gridViewTextBoxColumn66.Width = 100;
		gridViewTextBoxColumn67.FieldName = "codMoneda";
		gridViewTextBoxColumn67.HeaderText = "codMoneda";
		gridViewTextBoxColumn67.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn67.IsVisible = false;
		gridViewTextBoxColumn67.Name = "codMoneda";
		gridViewTextBoxColumn67.Width = 100;
		gridViewTextBoxColumn68.FieldName = "comenfactura";
		gridViewTextBoxColumn68.HeaderText = "Comentario de Factura";
		gridViewTextBoxColumn68.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn68.Name = "comenfac";
		gridViewTextBoxColumn68.Width = 340;
		gridViewTextBoxColumn69.FieldName = "comnpago";
		gridViewTextBoxColumn69.HeaderText = "Comentario de Pago";
		gridViewTextBoxColumn69.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		gridViewTextBoxColumn69.Name = "comenpago";
		gridViewTextBoxColumn69.Width = 250;
		this.rgvPagos.MasterTemplate.Columns.AddRange(gridViewCheckBoxColumn3, gridViewTextBoxColumn47, gridViewTextBoxColumn48, gridViewTextBoxColumn49, gridViewTextBoxColumn50, gridViewTextBoxColumn51, gridViewTextBoxColumn52, gridViewTextBoxColumn53, gridViewTextBoxColumn54, gridViewTextBoxColumn55, gridViewTextBoxColumn56, gridViewTextBoxColumn57, gridViewTextBoxColumn58, gridViewTextBoxColumn59, gridViewTextBoxColumn60, gridViewTextBoxColumn61, gridViewTextBoxColumn62, gridViewTextBoxColumn63, gridViewHyperlinkColumn3, gridViewTextBoxColumn64, gridViewTextBoxColumn65, gridViewTextBoxColumn66, gridViewTextBoxColumn67, gridViewTextBoxColumn68, gridViewTextBoxColumn69);
		this.rgvPagos.MasterTemplate.EnableFiltering = true;
		this.rgvPagos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvPagos.MasterTemplate.ViewDefinition = tableViewDefinition3;
		this.rgvPagos.Name = "rgvPagos";
		this.rgvPagos.ReadOnly = true;
		this.rgvPagos.ShowGroupPanel = false;
		this.rgvPagos.Size = new System.Drawing.Size(1390, 663);
		this.rgvPagos.TabIndex = 9;
		this.rgvPagos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvPagos_CellClick);
		this.rgvPagos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvPagos_CellDoubleClick);
		this.rgvPagos.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(rgvPagos_ContextMenuOpening);
		this.checkBox1.Location = new System.Drawing.Point(6, 8);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(15, 16);
		this.checkBox1.TabIndex = 1;
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.Click += new System.EventHandler(checkBox1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1390, 772);
		base.Controls.Add(this.rgvPagos);
		base.Controls.Add(this.dgvPagos);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.KeyPreview = true;
		base.Name = "frmPagos";
		base.ShowInTaskbar = false;
		this.Text = "Pagos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPagos_Load);
		base.Shown += new System.EventHandler(frmPagos_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.gbRadioSelector.ResumeLayout(false);
		this.gbRadioSelector.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvPagos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvPagos).EndInit();
		this.rgvPagos.ResumeLayout(false);
		this.rgvPagos.PerformLayout();
		base.ResumeLayout(false);
	}
}
