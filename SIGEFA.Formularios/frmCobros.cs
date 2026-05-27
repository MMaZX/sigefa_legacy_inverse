using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SpreadsheetLight;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmCobros : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private clsAdmTipoDocumento admTipo = new clsAdmTipoDocumento();

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmNotaSalida admNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	private clsAdmLetra admLetra = new clsAdmLetra();

	private clsLetra let = new clsLetra();

	private clsPago pagoRp = new clsPago();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	public int muestraReten = 1;

	private int vencidas = 0;

	private int porvencer = 0;

	private int pendientes = 0;

	private bool band_checkbox = false;

	private List<int> ln = new List<int>();

	private CheckBox chkBoxCabecera = new CheckBox();

	private double porcentaje_ret = 0.03;

	private double porcentaje_det = 0.12;

	private int cbestado;

	private int tipocmb;

	private DataTable mySource;

	private IContainer components = null;

	private ImageList imageList1;

	private ButtonItem btnBuscar;

	private ButtonItem buttonItem1;

	private GroupBox groupBox1;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private ComboBox cmbEstado;

	private ComboBox cmbTipo;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private DataGridView dgvCobros;

	private Button btnReporte;

	private Button btnBusqueda;

	private Label label9;

	private ComboBox cmbAlmacen;

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

	private Label lblporvencer;

	private Label lblvencidos;

	private Label lblpendientes;

	private Label label11;

	private Label label8;

	private Label label12;

	private Button button1;

	private BackgroundWorker backCobros;

	private Label label14;

	private Label label13;

	private TextBox textBox2;

	private TextBox textBox1;

	private Label label15;

	private TextBox textBox3;

	private Label label16;

	private ComboBox cmbFormaPago;

	private Label lblResultadoFiltradoSoles;

	private Label lblTotalPendienteSoles;

	private Button btnPagoMultiple;

	private DataGridViewCheckBoxColumn chkSelector;

	private DataGridViewTextBoxColumn codnota;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewAutoFilterTextBoxColumn vendedor;

	private DataGridViewTextBoxColumn zona;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn numdocumento;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn codperso;

	private DataGridViewTextBoxColumn ruc_dni;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewAutoFilterTextBoxColumn formpago;

	private DataGridViewTextBoxColumn fechavenc;

	private DataGridViewTextBoxColumn fechacancelado;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn morosidad;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn pendiente;

	private DataGridViewTextBoxColumn banco;

	private DataGridViewTextBoxColumn numunico;

	private DataGridViewLinkColumn accion;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn contado;

	private DataGridViewTextBoxColumn credito;

	private DataGridViewTextBoxColumn xaprobars;

	private DataGridViewCheckBoxColumn chkRet;

	private DataGridViewCheckBoxColumn chkDet;

	private DataGridViewTextBoxColumn txtMontoEnCuenta;

	private DataGridViewTextBoxColumn txtMontoRet;

	private DataGridViewTextBoxColumn txtMontoDet;

	private DataGridViewTextBoxColumn txtTOTAL_R_D;

	private DataGridViewTextBoxColumn comentario;

	private DataGridViewTextBoxColumn bdMontoDetRet;

	private DataGridViewTextBoxColumn bdTipoDetRet;

	private DataGridViewTextBoxColumn codMoneda;

	private Label lblTotalPendienteDolares;

	private Label lblResultadoFiltradoDolares;

	private Label lblTotalPendienteConvertido;

	private Label lblResultadoFiltradoConvertido;

	private Label label17;

	private Label label18;

	private Label label19;

	private Label label20;

	private Label label21;

	private Label label22;

	private RadGridView rgvCobros;

	private CheckBox checkBox1;

	private Button btnexcel;

	public frmCobros()
	{
		InitializeComponent();
	}

	public void RecargarLista()
	{
		btnBusqueda.PerformClick();
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		ln.Clear();
		CargaLista();
		double aux_soles = calcularTotalPendiente(1);
		lblTotalPendienteSoles.Text = Math.Round(aux_soles, 2).ToString("## ### ##0.00");
		double aux_dolares = calcularTotalPendiente(2);
		lblTotalPendienteDolares.Text = Math.Round(aux_dolares, 2).ToString("## ### ##0.00");
		lblResultadoFiltradoDolares.Text = Math.Round(aux_dolares, 2).ToString("## ### ##0.00");
		double aux_convertido = convertirSumaDolaresASoles(aux_dolares, aux_soles);
		lblTotalPendienteConvertido.Text = Math.Round(aux_convertido, 2).ToString("## ### ##0.00");
		CalculodeFechasPagos();
		Cursor = Cursors.Default;
	}

	private double convertirSumaDolaresASoles(double monto_dolares, double monto_soles)
	{
		try
		{
			double tc_venta = mdi_Menu.tc_hoy;
			return monto_soles + monto_dolares * tc_venta;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return 0.0;
		}
	}

	private double calcularTotalPendiente(int moneda)
	{
		double total_pendiente = 0.0;
		if (rgvCobros.Rows.Count > 0)
		{
			foreach (GridViewRowInfo fila in rgvCobros.Rows)
			{
				if (moneda == Convert.ToInt32(fila.Cells[codMoneda.Name].Value))
				{
					total_pendiente += (double)(decimal)fila.Cells[pendiente.Name].Value;
				}
			}
		}
		return total_pendiente;
	}

	private async void CargaLista()
	{
		try
		{
			int codFormaPago = ((cmbFormaPago.SelectedIndex != 0) ? 6 : 7);
			rgvCobros.DataSource = data;
			data.DataSource = AdmVenta.MuestraCobrosVenta(cmbEstado.SelectedIndex, Convert.ToInt32(cmbAlmacen.SelectedValue), dtpFecha1.Value, dtpFecha2.Value, Convert.ToInt32(cmbTipo.SelectedValue), frmLogin.iCodSucursal, codFormaPago);
			data.Filter = string.Empty;
			filtro = string.Empty;
			rgvCobros.ClearSelection();
			if (muestraReten == 1)
			{
				mostrarDatosDetRet(rgvCobros);
			}
			muestraReten = 1;
			DarFormato();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show(ex2.ToString(), ex2.Message);
		}
	}

	private void inhabilitarCheckBoxDetRet(string text)
	{
		if (!(text == "CANCELADOS"))
		{
			return;
		}
		foreach (DataGridViewRow fila in (IEnumerable)dgvCobros.Rows)
		{
			((DataGridViewCheckBoxCell)fila.Cells[chkDet.Name]).ReadOnly = false;
			((DataGridViewCheckBoxCell)fila.Cells[chkRet.Name]).ReadOnly = true;
		}
	}

	private void mostrarDatosDetRet(RadGridView dataSource)
	{
		try
		{
			if (dataSource.Rows.Count <= 0)
			{
				return;
			}
			foreach (GridViewRowInfo fila in dataSource.Rows)
			{
				string text = fila.Cells[bdTipoDetRet.Name].Value.ToString();
				string text2 = text;
				if (!(text2 == "RET"))
				{
					if (text2 == "DET")
					{
						fila.Cells[chkDet.Name].Value = true;
						fila.Cells[txtMontoDet.Name].Value = fila.Cells[bdMontoDetRet.Name].Value;
						fila.Cells[txtMontoRet.Name].Value = 0.0;
					}
					else
					{
						fila.Cells[chkDet.Name].Value = false;
						fila.Cells[chkRet.Name].Value = false;
						fila.Cells[txtMontoEnCuenta.Name].Value = 0.0;
						fila.Cells[txtMontoDet.Name].Value = 0.0;
						fila.Cells[txtMontoRet.Name].Value = 0.0;
					}
				}
				else
				{
					fila.Cells[chkRet.Name].Value = true;
					fila.Cells[txtMontoRet.Name].Value = fila.Cells[bdMontoDetRet.Name].Value;
					fila.Cells[txtMontoDet.Name].Value = 0.0;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
	}

	private DateTime ObteniendoPrimeraFecha()
	{
		try
		{
			int codFormaPago = 0;
			codFormaPago = ((cmbFormaPago.SelectedIndex != 0) ? 6 : 7);
			string fecha = AdmVenta.MuestraFechaPrimerCobro(cmbEstado.SelectedIndex, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbTipo.SelectedValue), frmLogin.iCodSucursal, codFormaPago);
			if (fecha != null)
			{
				return Convert.ToDateTime(fecha);
			}
			throw new ArgumentNullException();
		}
		catch (Exception)
		{
			return DateTime.Now.AddDays(-30.0);
		}
	}

	private void DarFormato()
	{
		foreach (GridViewRowInfo row in rgvCobros.Rows)
		{
			if (row.Cells[morosidad.Name].Value != null && row.Cells[morosidad.Name].Value.ToString() != " - " && Convert.ToDouble(row.Cells[morosidad.Name].Value) <= 0.0 && cmbEstado.SelectedIndex == 0 && row.Index != -1)
			{
				row.Cells[morosidad.Name].Style.CustomizeFill = true;
				row.Cells[morosidad.Name].Style.BackColor = System.Drawing.Color.Red;
				row.Cells[morosidad.Name].Style.ForeColor = System.Drawing.Color.White;
			}
			if (row.Cells[xaprobars.Name].Value != null && Convert.ToInt32(row.Cells[xaprobars.Name].Value) > 0)
			{
				row.Cells[morosidad.Name].Style.CustomizeFill = true;
				row.Cells[xaprobars.Name].Style.BackColor = System.Drawing.Color.Gold;
			}
			if (row.Cells[numdocumento.Name].Value != null && row.Cells[numdocumento.Name].Value.ToString().Length <= 8)
			{
				row.Cells[morosidad.Name].Style.CustomizeFill = true;
				row.Cells[numdocumento.Name].Style.BackColor = System.Drawing.Color.Turquoise;
			}
		}
	}

	private void frmCobros_Load(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		CargaAlmacenes();
		CargaTipoDocumento();
		cmbEstado.SelectedIndex = 0;
		cmbTipo.SelectedIndex = 0;
		cmbFormaPago.SelectedIndex = 0;
		label7.Text = "Cliente";
		label6.Text = "razonsocial";
		EventArgs aven = new EventArgs();
		cmbEstado_SelectionChangeCommitted(cmbEstado, aven);
		EventArgs aventi = new EventArgs();
		cmbTipo_SelectionChangeCommitted(cmbTipo, aventi);
		dtpFecha1.Value = ObteniendoPrimeraFecha();
		btnBusqueda.PerformClick();
		Cursor = Cursors.Default;
	}

	private void añadiendoCheckBoxCabecera()
	{
		Point CeldaCabeceraLocacion = dgvCobros.GetCellDisplayRectangle(0, -1, cutOverflow: true).Location;
		chkBoxCabecera.Size = new Size(18, 18);
		chkBoxCabecera.Location = new Point(CeldaCabeceraLocacion.X + 5, CeldaCabeceraLocacion.Y + 2);
		dgvCobros.Controls.Add(chkBoxCabecera);
	}

	private void chkBox_Principal_Clicked()
	{
		try
		{
			rgvCobros.EndEdit();
			ln.Clear();
			double suma_soles = 0.0;
			double suma_dolares = 0.0;
			foreach (GridViewRowInfo fila in rgvCobros.ChildRows)
			{
				if (checkBox1.Checked)
				{
					fila.Cells["chkSelector"].Value = true;
					ln.Add(fila.Index);
					if (Convert.ToInt32(fila.Cells[codMoneda.Name].Value) == 1)
					{
						suma_soles += (double)(decimal)fila.Cells[pendiente.Name].Value;
					}
					else
					{
						suma_dolares += (double)(decimal)fila.Cells[pendiente.Name].Value;
					}
				}
				else
				{
					fila.Cells["chkSelector"].Value = false;
				}
			}
			lblResultadoFiltradoSoles.Text = suma_soles.ToString("## ### ##0.00");
			lblResultadoFiltradoDolares.Text = suma_dolares.ToString("## ### ##0.00");
			double suma_convertida = convertirSumaDolaresASoles(suma_dolares, suma_soles);
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
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - frmCobros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void CalculodeFechasPagos()
	{
		try
		{
			vencidas = 0;
			porvencer = 0;
			pendientes = 0;
			foreach (GridViewRowInfo row in rgvCobros.Rows)
			{
				if (Convert.ToDateTime(row.Cells[fechavenc.Name].Value).Date < Convert.ToDateTime(DateTime.Now).Date)
				{
					vencidas++;
				}
				else if (Convert.ToDateTime(row.Cells[fechavenc.Name].Value).Date == Convert.ToDateTime(DateTime.Now).Date)
				{
					porvencer++;
				}
				else
				{
					pendientes++;
				}
			}
			lblpendientes.Text = Convert.ToString(pendientes);
			lblporvencer.Text = Convert.ToString(porvencer);
			lblvencidos.Text = Convert.ToString(vencidas);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void frmCobros_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			double suma_soles = 0.0;
			double suma_dolares = 0.0;
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label6.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
				dgvCobros.DataSource = data;
				foreach (DataGridViewRow row in (IEnumerable)dgvCobros.Rows)
				{
					if (Convert.ToInt32(row.Cells[codMoneda.Name].Value) == 1)
					{
						suma_soles += (double)(decimal)row.Cells[pendiente.Name].Value;
					}
					else
					{
						suma_dolares += (double)(decimal)row.Cells[pendiente.Name].Value;
					}
				}
				todos_checkbox_columna(estado: true, dgvCobros);
			}
			else
			{
				data.Filter = string.Empty;
				dgvCobros.DataSource = data;
			}
			lblResultadoFiltradoSoles.Text = Math.Round(suma_soles, 2).ToString("## ### ##0.00");
			lblResultadoFiltradoDolares.Text = Math.Round(suma_dolares, 2).ToString("## ### ##0.00");
			double suma_convertido = convertirSumaDolaresASoles(suma_dolares, suma_soles);
			lblResultadoFiltradoConvertido.Text = Math.Round(suma_convertido, 2).ToString("## ### ##0.00");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error Filtrado - frmCobros");
		}
	}

	private void todos_checkbox_columna(bool estado, DataGridView dgv)
	{
		if (dgv.Rows.Count <= 0)
		{
			return;
		}
		ln.Clear();
		btnPagoMultiple.Enabled = false;
		foreach (DataGridViewRow fila in (IEnumerable)dgv.Rows)
		{
			fila.Cells[chkSelector.Name].Value = estado;
			if (!estado)
			{
				continue;
			}
			int n = fila.Index;
			double nro_soles = 0.0;
			double nro_dolares = 0.0;
			dgv.ClearSelection();
			if (!ln.Contains(n))
			{
				ln.Add(n);
				int i = ln.IndexOf(n);
				if (Convert.ToInt32(dgvCobros.Rows[i].Cells[xaprobars.Name].Value) <= 0 && !(dgvCobros.Rows[i].Cells[accion.Name].Value.ToString() == "Muestra Pagos"))
				{
					dgv.Rows[i].Cells[chkSelector.Name].Value = true;
					if (Convert.ToInt32(dgvCobros.Rows[i].Cells[codMoneda.Name].Value) == 1)
					{
						nro_soles += Convert.ToDouble(dgv.Rows[i].Cells[pendiente.Name].Value);
					}
					else
					{
						nro_dolares += Convert.ToDouble(dgv.Rows[i].Cells[pendiente.Name].Value);
					}
				}
			}
			lblResultadoFiltradoSoles.Text = Math.Round(nro_soles, 2).ToString("## ### ##0.00");
			lblResultadoFiltradoDolares.Text = Math.Round(nro_dolares, 2).ToString("## ### ##0.00");
			double nro_convertido = convertirSumaDolaresASoles(nro_dolares, nro_soles);
			lblResultadoFiltradoConvertido.Text = Math.Round(nro_convertido, 2).ToString("## ### ##0.00");
		}
		if (estado)
		{
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

	private void CargaAlmacenes()
	{
		cmbAlmacen.ValueMember = "cod";
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.DataSource = admAlm.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void CargaTipoDocumento()
	{
		cmbTipo.DataSource = admTipo.CargaTipoDocumentos();
		cmbTipo.DisplayMember = "descripcion";
		cmbTipo.ValueMember = "codTipoDocumento";
		cmbTipo.SelectedIndex = -1;
	}

	private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvCobros.Columns[e.ColumnIndex].Name == chkSelector.Name)
		{
			todos_checkbox_columna(band_checkbox, dgvCobros);
			return;
		}
		label7.Text = dgvCobros.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvCobros.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void dgvCobros_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void agregandoDatosDetRetAPago(frmCancelarPago form, GridViewCellEventArgs e)
	{
		if (Convert.ToBoolean(rgvCobros.Rows[e.RowIndex].Cells[chkRet.Name].Value ?? ((object)false)) && rgvCobros.Rows[e.RowIndex].Cells[txtMontoRet.Name].Value != null)
		{
			form.det_ret = Convert.ToDouble(rgvCobros.Rows[e.RowIndex].Cells[txtMontoRet.Name].Value);
			form.band_det_ret = "RET";
			form.monto_en_cuenta = Convert.ToDouble(rgvCobros.Rows[e.RowIndex].Cells[txtMontoEnCuenta.Name].Value);
		}
		if (Convert.ToBoolean(rgvCobros.Rows[e.RowIndex].Cells[chkDet.Name].Value ?? ((object)false)) && rgvCobros.Rows[e.RowIndex].Cells[txtMontoDet.Name].Value != null)
		{
			form.det_ret = Convert.ToDouble(rgvCobros.Rows[e.RowIndex].Cells[txtMontoDet.Name].Value);
			form.band_det_ret = "DET";
			form.monto_en_cuenta = Convert.ToDouble(rgvCobros.Rows[e.RowIndex].Cells[txtMontoEnCuenta.Name].Value);
		}
	}

	private void actualizacionCheckBoxCabecera(DataGridViewCellEventArgs e)
	{
		if (e.RowIndex < 0 || e.ColumnIndex != 0)
		{
			return;
		}
		bool isChecked = true;
		foreach (DataGridViewRow fila in (IEnumerable)dgvCobros.Rows)
		{
			if (!Convert.ToBoolean(fila.Cells[chkSelector.Name].EditedFormattedValue))
			{
				isChecked = false;
				break;
			}
		}
		chkBoxCabecera.Checked = isChecked;
	}

	private void calcularRetraccionDetraccion(GridViewCellEventArgs e)
	{
		GridViewRowInfo fila = rgvCobros.Rows[e.RowIndex];
		GridViewCellInfo celda = rgvCobros.Rows[e.RowIndex].Cells[e.ColumnIndex];
		GridViewColumn NomCol = rgvCobros.Columns[e.ColumnIndex];
		string nombre_de_columna = NomCol.Name;
		if (!(nombre_de_columna == chkRet.Name) && !(nombre_de_columna == chkDet.Name))
		{
			return;
		}
		if (celda.Value == null)
		{
			celda.Value = false;
		}
		if (nombre_de_columna == chkRet.Name)
		{
			if (rgvCobros.Rows[e.RowIndex].Cells[chkDet.Name].Value != null && (bool)rgvCobros.Rows[e.RowIndex].Cells[chkDet.Name].Value)
			{
				MessageBox.Show("No se puede seleccionar Retraccion mientras Detraccion esta seleccionado.", "Error al Seleccionar");
			}
			else
			{
				celda.Value = !(bool)celda.Value;
				if ((bool)celda.Value)
				{
					try
					{
						double aux_pendiente = Convert.ToDouble(fila.Cells[monto.Name].Value ?? ((object)0.0));
						fila.Cells[txtMontoEnCuenta.Name].Value = aux_pendiente * (1.0 - porcentaje_ret);
						fila.Cells[txtMontoRet.Name].Value = aux_pendiente * porcentaje_ret;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), ex.Message);
					}
				}
				else
				{
					fila.Cells[txtMontoEnCuenta.Name].Value = 0.0;
					fila.Cells[txtMontoRet.Name].Value = 0.0;
				}
			}
		}
		if (!(nombre_de_columna == chkDet.Name))
		{
			return;
		}
		if (rgvCobros.Rows[e.RowIndex].Cells[chkRet.Name].Value != null && (bool)rgvCobros.Rows[e.RowIndex].Cells[chkRet.Name].Value)
		{
			MessageBox.Show("No se puede seleccionar Detraccion mientras Retraccion esta seleccionado.", "Error al Seleccionar");
			return;
		}
		celda.Value = !(bool)celda.Value;
		if ((bool)celda.Value)
		{
			double aux_pendiente2 = Convert.ToDouble(fila.Cells[monto.Name].Value ?? ((object)0.0));
			fila.Cells[txtMontoEnCuenta.Name].Value = aux_pendiente2 * (1.0 - porcentaje_det);
			fila.Cells[txtMontoDet.Name].Value = aux_pendiente2 * porcentaje_det;
		}
		else
		{
			fila.Cells[txtMontoEnCuenta.Name].Value = 0.0;
			fila.Cells[txtMontoDet.Name].Value = 0.0;
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
			rgvCobros.ChildRows[n].Cells[e.ColumnIndex].Value = false;
			rgvCobros.ChildRows[n].IsSelected = false;
			rgvCobros.ClearSelection();
			if (ln.Contains(n))
			{
				ln.Remove(n);
				band_checkbox = true;
			}
			else if (rgvCobros.ChildRows[n].Cells[accion.Name].Value.ToString() == "Muestra Pagos")
			{
				MessageBox.Show("Imposible Seleccionar", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				ln.Add(n);
			}
			double nro_soles = 0.0;
			double nro_dolares = 0.0;
			foreach (int i in ln)
			{
				if (Convert.ToInt32(rgvCobros.ChildRows[i].Cells[xaprobars.Name].Value) > 0 || rgvCobros.ChildRows[i].Cells[accion.Name].Value.ToString() == "Muestra Pagos")
				{
					MessageBox.Show("Imposible Seleccionar", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					continue;
				}
				rgvCobros.ChildRows[i].Cells[e.ColumnIndex].Value = true;
				if (Convert.ToInt32(rgvCobros.ChildRows[i].Cells[codMoneda.Name].Value) == 1)
				{
					nro_soles += Convert.ToDouble(rgvCobros.ChildRows[i].Cells[pendiente.Name].Value ?? ((object)0));
				}
				else
				{
					nro_dolares += Convert.ToDouble(rgvCobros.ChildRows[i].Cells[pendiente.Name].Value ?? ((object)0));
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
			if (ln.Count == rgvCobros.ChildRows.Count)
			{
				band_checkbox = false;
				chkBoxCabecera.Checked = true;
			}
			lblResultadoFiltradoSoles.Text = Math.Round(nro_soles, 2).ToString("## ### ##0.00");
			lblResultadoFiltradoDolares.Text = Math.Round(nro_dolares, 2).ToString("## ### ##0.00");
			double nro_convertido = convertirSumaDolaresASoles(nro_dolares, nro_soles);
			lblResultadoFiltradoConvertido.Text = Math.Round(nro_convertido, 2).ToString("## ### ##0.00");
			if (ln.Count == rgvCobros.ChildRows.Count - 1)
			{
				chkBoxCabecera.Checked = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Seleccionar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void muestraPagosToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvCobros.SelectedRows[0];
		switch (Convert.ToInt32(Row.Cells[tipo.Name].Value))
		{
		case 3:
		{
			venta.CodFacturaVenta = Row.Cells[codnota.Name].Value.ToString();
			venta.Pendiente = Convert.ToDecimal(Row.Cells[pendiente.Name].Value.ToString());
			double totalM = Convert.ToDouble(Row.Cells[monto.Name].Value.ToString());
			frmMuestraPagos form2 = new frmMuestraPagos();
			form2.CodNota = Convert.ToInt32(venta.CodFacturaVenta);
			form2.Almacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
			if (cmbEstado.SelectedIndex == 0)
			{
				form2.montoTotal = Convert.ToDecimal(venta.Pendiente);
			}
			else if (cmbEstado.SelectedIndex == 1)
			{
				form2.montoTotal = Convert.ToDecimal(totalM);
			}
			form2.InOut = true;
			form2.tipo = 0;
			DialogResult dlgResult2 = form2.ShowDialog();
			break;
		}
		case 4:
		{
			let.CodLetra = Convert.ToInt32(Row.Cells[codnota.Name].Value);
			frmMuestraPagos form = new frmMuestraPagos();
			form.CodNota = let.CodLetra;
			form.InOut = true;
			form.tipo = 1;
			DialogResult dlgResult = form.ShowDialog();
			if (dlgResult != DialogResult.Yes)
			{
			}
			break;
		}
		}
	}

	private void canjearPorLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dgvCobros.SelectedRows.Count > 0)
		{
			DataGridViewRow Row = dgvCobros.CurrentRow;
			venta.CodFacturaVenta = Row.Cells[codnota.Name].Value.ToString();
			frmCanjearLetra form = new frmCanjearLetra();
			form.venta = venta;
			form.Procede = 2;
			form.ShowDialog();
		}
	}

	private void dgvCobros_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		dgvCobros.ContextMenuStrip = new ContextMenuStrip();
		if (e.RowIndex == -1)
		{
			return;
		}
		dgvCobros.Rows[e.RowIndex].Selected = true;
		if (e.Button != MouseButtons.Right || e.RowIndex == -1 || dgvCobros.SelectedCells.Count <= 0)
		{
			return;
		}
		dgvCobros.ContextMenuStrip = contextMenuStrip1;
		if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[tipo.Name].Value) == 3)
		{
			canjearPorLetraToolStripMenuItem.Enabled = true;
			modificarLetraToolStripMenuItem.Enabled = false;
			imprimirLetraToolStripMenuItem.Enabled = false;
			ingresoABancoToolStripMenuItem.Enabled = false;
			if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[cantidad.Name].Value) > 0)
			{
				muestraPagosToolStripMenuItem.Enabled = true;
			}
			else
			{
				muestraPagosToolStripMenuItem.Enabled = false;
			}
		}
		else if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[tipo.Name].Value) == 4)
		{
			canjearPorLetraToolStripMenuItem.Enabled = false;
			modificarLetraToolStripMenuItem.Enabled = true;
			imprimirLetraToolStripMenuItem.Enabled = true;
			ingresoABancoToolStripMenuItem.Enabled = true;
			if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[cantidad.Name].Value) > 0)
			{
				muestraPagosToolStripMenuItem.Enabled = true;
			}
			else
			{
				muestraPagosToolStripMenuItem.Enabled = false;
			}
		}
	}

	private void dgvCobros_Sorted(object sender, EventArgs e)
	{
		if (e is DataGridViewCellEventArgs eve)
		{
			Console.WriteLine("Sorted: " + eve.RowIndex + " - " + eve.ColumnIndex);
		}
		DarFormato();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		dt.Columns.Add("codnota", typeof(string));
		dt.Columns.Add("vendedor", typeof(string));
		dt.Columns.Add("fechaemision", typeof(DateTime));
		dt.Columns.Add("tipo", typeof(string));
		dt.Columns.Add("numdocumento", typeof(string));
		dt.Columns.Add("documento", typeof(string));
		dt.Columns.Add("codcliente", typeof(string));
		dt.Columns.Add("codperso", typeof(string));
		dt.Columns.Add("cliente", typeof(string));
		dt.Columns.Add("formapago", typeof(string));
		dt.Columns.Add("fechavenc", typeof(DateTime));
		dt.Columns.Add("morosidad", typeof(string));
		dt.Columns.Add("moneda", typeof(string));
		dt.Columns.Add("monto", typeof(double));
		dt.Columns.Add("pendiente", typeof(double));
		dt.Columns.Add("banco", typeof(string));
		dt.Columns.Add("nuunico", typeof(string));
		dt.Columns.Add("accion", typeof(string));
		dt.Columns.Add("cantidad", typeof(double));
		dt.Columns.Add("contado", typeof(double));
		dt.Columns.Add("credito", typeof(double));
		dt.Columns.Add("fecha1", typeof(DateTime));
		dt.Columns.Add("fecha2", typeof(DateTime));
		dt.Columns.Add("estado", typeof(string));
		dt.Columns.Add("tip", typeof(string));
		foreach (GridViewRowInfo dgv in rgvCobros.ChildRows)
		{
			dt.Rows.Add(dgv.Cells[codnota.Name].Value, dgv.Cells[vendedor.Name].Value, Convert.ToDateTime(dgv.Cells[fechaemision.Name].Value), dgv.Cells[tipo.Name].Value, dgv.Cells[numdocumento.Name].Value, dgv.Cells[documento.Name].Value, dgv.Cells[codcliente.Name].Value, dgv.Cells[codperso.Name].Value, dgv.Cells[cliente.Name].Value, dgv.Cells[formpago.Name].Value, Convert.ToDateTime(dgv.Cells[fechavenc.Name].Value), dgv.Cells[morosidad.Name].Value, dgv.Cells[moneda.Name].Value, Convert.ToDecimal(dgv.Cells[monto.Name].Value), Convert.ToDecimal(dgv.Cells[pendiente.Name].Value), dgv.Cells[banco.Name].Value, dgv.Cells[numunico.Name].Value, dgv.Cells[accion.Name].Value, Convert.ToDecimal(dgv.Cells[cantidad.Name].Value), Convert.ToDecimal(dgv.Cells[contado.Name].Value), Convert.ToDecimal(dgv.Cells[credito.Name].Value), dtpFecha1.Text, dtpFecha2.Text, cmbEstado.Text, cmbTipo.Text);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\CobrosGeneralRPT.xml", XmlWriteMode.WriteSchema);
		CRCobrosGeneral rpt = new CRCobrosGeneral();
		frmRptCobrosGeneral frm = new frmRptCobrosGeneral();
		rpt.SetDataSource(ds);
		frm.crvCobrosGeneral.ReportSource = rpt;
		frm.Show();
	}

	private void ingresoABancoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GridViewRowInfo Row = rgvCobros.CurrentRow;
		frmIngresoBanco form = new frmIngresoBanco();
		form.CodLetra = Convert.ToInt32(Row.Cells[codnota.Name].Value);
		form.Proceso = 1;
		form.ShowDialog();
	}

	private void dgvCobros_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F3)
		{
			return;
		}
		DataGridViewRow row = dgvCobros.CurrentRow;
		int itipo = Convert.ToInt32(row.Cells[tipo.Name].Value);
		if (!(row.Cells[accion.Name].Value.ToString() == "Ingresar Pago"))
		{
			return;
		}
		switch (itipo)
		{
		case 3:
		{
			venta.CodFacturaVenta = row.Cells[codnota.Name].Value.ToString();
			frmCancelarPago form2 = new frmCancelarPago();
			form2.CodNota = venta.CodFacturaVenta;
			form2.tipo = itipo;
			DialogResult dlgResult2 = form2.ShowDialog();
			if (dlgResult2 != DialogResult.Yes)
			{
			}
			break;
		}
		case 4:
		{
			let.CodLetra = Convert.ToInt32(row.Cells[codnota.Name].Value);
			frmCancelarPago form = new frmCancelarPago();
			form.CodLetra = let.CodLetra;
			form.tipo = itipo;
			DialogResult dlgResult = form.ShowDialog();
			if (dlgResult != DialogResult.Yes)
			{
			}
			break;
		}
		}
	}

	private void frmCobros_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
	}

	private void dgvCobros_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		Console.WriteLine("CellFormating: " + e.RowIndex + " - " + e.ColumnIndex);
		if (cmbEstado.Text == "PENDIENTES")
		{
			try
			{
				DarFormato();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), ex.Message);
			}
		}
	}

	private void dgvCobros_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	private void cmbTipo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		tipocmb = Convert.ToInt32(cmbTipo.SelectedValue);
	}

	private void cmbEstado_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cbestado = cmbEstado.SelectedIndex;
	}

	private void backCobros_DoWork(object sender, DoWorkEventArgs e)
	{
		if (backCobros.CancellationPending)
		{
			e.Cancel = true;
		}
		else
		{
			backCobrosProcessLogicMethod();
		}
	}

	private void backCobrosProcessLogicMethod()
	{
		Thread.Sleep(2000);
		try
		{
			if (backCobros == null)
			{
				return;
			}
			if (mySource != null)
			{
				mySource = null;
				return;
			}
			int codFormaPago = 0;
			codFormaPago = ((cmbFormaPago.SelectedIndex != 0) ? 6 : 7);
			mySource = AdmVenta.MuestraCobrosVenta(cbestado, Convert.ToInt32(cmbAlmacen.SelectedValue), dtpFecha1.Value, dtpFecha2.Value, tipocmb, frmLogin.iCodSucursal, codFormaPago);
			foreach (DataRow row in mySource.Rows)
			{
				backCobros.ReportProgress(mySource.Rows.IndexOf(row));
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void backCobros_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		DataTable mydatOld = null;
		DataTable mydataResult = null;
		if (dgvCobros.DataSource != null)
		{
			mydatOld = new DataTable();
			mydatOld = (DataTable)data.DataSource;
			mydataResult = new DataTable();
			mydataResult = getDifferentRecords(mydatOld, mySource);
			if (mydataResult != null && mydataResult.Rows.Count != 0)
			{
				dgvCobros.AutoGenerateColumns = false;
				dgvCobros.DataSource = data;
				data.DataSource = mySource;
				data.Filter = string.Empty;
				filtro = string.Empty;
				dgvCobros.ClearSelection();
				Console.WriteLine("BackWork-ProgresChanged-1: " + e.ProgressPercentage + " %");
				DarFormato();
			}
		}
		else
		{
			dgvCobros.AutoGenerateColumns = false;
			dgvCobros.DataSource = data;
			data.DataSource = mySource;
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvCobros.ClearSelection();
			Console.WriteLine("BackWork-ProgresChanged-2: " + e.ProgressPercentage + " %");
			DarFormato();
		}
	}

	private DataTable getDifferentRecords(DataTable mydatOld, DataTable mySource)
	{
		DataTable ResultDataTable = new DataTable("ResultDataTable");
		try
		{
			using (DataSet ds = new DataSet())
			{
				ds.Tables.AddRange(new DataTable[2]
				{
					mydatOld.Copy(),
					mySource.Copy()
				});
				DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
				for (int i = 0; i < firstColumns.Length; i++)
				{
					firstColumns[i] = ds.Tables[0].Columns[i];
				}
				DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
				for (int j = 0; j < secondColumns.Length; j++)
				{
					secondColumns[j] = ds.Tables[1].Columns[j];
				}
				DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, createConstraints: false);
				ds.Relations.Add(r1);
				DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, createConstraints: false);
				ds.Relations.Add(r2);
				for (int k = 0; k < mydatOld.Columns.Count; k++)
				{
					ResultDataTable.Columns.Add(mydatOld.Columns[k].ColumnName, mydatOld.Columns[k].DataType);
				}
				ResultDataTable.BeginLoadData();
				foreach (DataRow parentrow in ds.Tables[0].Rows)
				{
					DataRow[] childrows = parentrow.GetChildRows(r1);
					if (childrows == null || childrows.Length == 0)
					{
						ResultDataTable.LoadDataRow(parentrow.ItemArray, fAcceptChanges: true);
					}
				}
				foreach (DataRow parentrow2 in ds.Tables[1].Rows)
				{
					DataRow[] childrows2 = parentrow2.GetChildRows(r2);
					if (childrows2 == null || childrows2.Length == 0)
					{
						ResultDataTable.LoadDataRow(parentrow2.ItemArray, fAcceptChanges: true);
					}
				}
				ResultDataTable.EndLoadData();
			}
			return ResultDataTable;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return null;
		}
	}

	private void backCobros_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		if (backCobros == null)
		{
			Close();
		}
		else
		{
			backCobros.RunWorkerAsync();
		}
	}

	private void arrancaBack()
	{
		if (!backCobros.IsBusy)
		{
			backCobros.RunWorkerAsync();
		}
	}

	private void frmCobros_FormClosing(object sender, FormClosingEventArgs e)
	{
		KillMe();
	}

	private void KillMe()
	{
		backCobros.CancelAsync();
		backCobros.Dispose();
		backCobros = null;
		GC.Collect();
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
						int valor1 = Convert.ToInt32(rgvCobros.ChildRows[ln[i]].Cells[tipo.Name].Value.ToString());
						int valor2 = Convert.ToInt32(rgvCobros.ChildRows[ln[i + 1]].Cells[tipo.Name].Value.ToString());
						if (valor1 != valor2)
						{
							titulo_error = "Incoherencia de Tipo de Pagos";
							mensaje_error = "Error Ocurrido por que las filas seleccionadas no son todas del mismo tipo";
							n = ln[i + 1];
							bandTipo = false;
							break;
						}
						string valora = rgvCobros.ChildRows[ln[i]].Cells[moneda.Name].Value.ToString();
						string valorb = rgvCobros.ChildRows[ln[i + 1]].Cells[moneda.Name].Value.ToString();
						if (valora != valorb)
						{
							titulo_error = "Incoherencia de Tipo de Monedas";
							mensaje_error = "Error Ocurrido por que las filas seleccionadas no tienene la misma MONEDA";
							n = ln[i + 1];
							bandTipo = false;
							break;
						}
					}
				}
			}
			if (bandTipo)
			{
				if (Application.OpenForms["frmCancelarCobroMultiple"] != null)
				{
					Application.OpenForms["frmCancelarCobroMultiple"].Activate();
				}
				else
				{
					int aux_tipo = Convert.ToInt32(rgvCobros.ChildRows[ln[0]].Cells[tipo.Name].Value);
					int aux_moneda = Convert.ToInt32(rgvCobros.ChildRows[ln[0]].Cells[codMoneda.Name].Value ?? ((object)0));
					switch (aux_tipo)
					{
					case 3:
					{
						frmCancelarCobroMultiple form1 = new frmCancelarCobroMultiple();
						form1.tipo = aux_tipo;
						form1.mon = aux_moneda;
						asignaFilasADGVFormularioMultiple(form1.dataFacturas);
						form1.ShowDialog();
						muestraReten = 0;
						btnBusqueda.PerformClick();
						break;
					}
					case 4:
						MessageBox.Show("Error Cobro Multiple - Letra", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						break;
					}
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
					GridViewRowInfo fila = rgvCobros.ChildRows[i];
					string factura = fila.Cells[numdocumento.Name].Value.ToString();
					string _fechaemision = Convert.ToDateTime(fila.Cells[fechaemision.Name].Value.ToString()).ToString("dd/MM/yyyy");
					string razonsocial = fila.Cells[cliente.Name].Value.ToString();
					double _monto = Convert.ToDouble(fila.Cells[monto.Name].Value.ToString());
					double _pendiente = Convert.ToDouble(fila.Cells[pendiente.Name].Value.ToString());
					bool _chkret = Convert.ToBoolean(fila.Cells[chkRet.Name].Value ?? ((object)false));
					bool _chkdet = Convert.ToBoolean(fila.Cells[chkDet.Name].Value ?? ((object)false));
					double _montoencuenta = ((fila.Cells[txtMontoEnCuenta.Name].Value == null) ? Convert.ToDouble(fila.Cells[pendiente.Name].Value.ToString()) : Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value.ToString()));
					double montoret = ((fila.Cells[txtMontoRet.Name].Value == null) ? 0.0 : Convert.ToDouble(fila.Cells[txtMontoRet.Name].Value.ToString()));
					double montodet = ((fila.Cells[txtMontoDet.Name].Value == null) ? 0.0 : Convert.ToDouble(fila.Cells[txtMontoDet.Name].Value.ToString()));
					double totalrd = ((fila.Cells[txtTOTAL_R_D.Name].Value == null) ? 0.0 : Convert.ToDouble(fila.Cells[txtTOTAL_R_D.Name].Value.ToString()));
					double total2 = _pendiente;
					double pendiente2 = _pendiente - total2;
					int codigo = Convert.ToInt32(fila.Cells[codnota.Name].Value);
					bool flag = true;
					dataFacturas.Rows.Add(factura, _fechaemision, razonsocial, _monto, _pendiente, _chkret, _chkdet, _montoencuenta, montoret, montodet, total2.ToString("0.###"), pendiente2.ToString("0.###"), codigo);
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

	private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void dgvCobros_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		if (e != null)
		{
			MessageBox.Show(e.ToString() + "\n" + e.Exception, "Fila: " + e.RowIndex + " -  Columna: " + e.ColumnIndex + " : ");
			DataGridViewCell celda = dgvCobros.Rows[e.RowIndex].Cells[e.ColumnIndex];
			Console.WriteLine(celda.OwningColumn.ToString() + celda.Value.ToString());
		}
		else
		{
			MessageBox.Show("no se pudo frmCobros - linea 1245");
		}
	}

	private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void nuevaLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void label1_Click(object sender, EventArgs e)
	{
	}

	private void dtpFecha1_ValueChanged(object sender, EventArgs e)
	{
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void dtpFecha2_ValueChanged(object sender, EventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void rgvCobros_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F3)
		{
			return;
		}
		GridViewRowInfo row = rgvCobros.CurrentRow;
		int itipo = Convert.ToInt32(row.Cells[tipo.Name].Value);
		if (!(row.Cells[accion.Name].Value.ToString() == "Ingresar Pago"))
		{
			return;
		}
		switch (itipo)
		{
		case 3:
		{
			venta.CodFacturaVenta = row.Cells[codnota.Name].Value.ToString();
			frmCancelarPago form2 = new frmCancelarPago();
			form2.CodNota = venta.CodFacturaVenta;
			form2.tipo = itipo;
			DialogResult dlgResult2 = form2.ShowDialog();
			if (dlgResult2 != DialogResult.Yes)
			{
			}
			break;
		}
		case 4:
		{
			let.CodLetra = Convert.ToInt32(row.Cells[codnota.Name].Value);
			frmCancelarPago form = new frmCancelarPago();
			form.CodLetra = let.CodLetra;
			form.tipo = itipo;
			DialogResult dlgResult = form.ShowDialog();
			if (dlgResult != DialogResult.Yes)
			{
			}
			break;
		}
		}
	}

	private void rgvCobros_DataError(object sender, GridViewDataErrorEventArgs e)
	{
		if (e != null)
		{
			GridViewColumn col = rgvCobros.Columns[e.ColumnIndex];
			MessageBox.Show(e.ToString() + "\n" + e.Exception, "Fila: " + e.RowIndex + " -  Columna: " + e.ColumnIndex + " : ");
			GridViewCellInfo celda = rgvCobros.Rows[e.RowIndex].Cells[e.ColumnIndex];
			Console.WriteLine(col.Name.ToString() + celda.Value.ToString());
		}
		else
		{
			MessageBox.Show("no se pudo frmCobros - linea 1245");
		}
	}

	private void rgvCobros_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvCobros.ChildRows.Count < 1 || e.Row.Index == -1)
		{
			return;
		}
		if (cmbEstado.Text == "PENDIENTES")
		{
			calcularPendientesAlSeleccionar(e);
			calcularRetraccionDetraccion(e);
		}
		GridViewCellInfo celda = rgvCobros.ChildRows[e.Row.Index].Cells[e.ColumnIndex];
		int itipo = Convert.ToInt32(rgvCobros.ChildRows[e.Row.Index].Cells[tipo.Name].Value);
		if (celda.Value == null)
		{
			return;
		}
		if (celda.Value.ToString() == "Ingresar Pago")
		{
			switch (itipo)
			{
			case 3:
			{
				if (Convert.ToInt32(rgvCobros.ChildRows[e.Row.Index].Cells[xaprobars.Name].Value) > 0)
				{
					MessageBox.Show("PENDIENTE DE APROBACION", "IMPOSIBLE REGISTRAR NUEVO PAGO", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					break;
				}
				if (rgvCobros.ChildRows[e.Row.Index].Cells[numdocumento.Name].Value.ToString().Length <= 8)
				{
					MessageBox.Show("PENDIENTE DE IMPRESIÓN", "IMPOSIBLE REGISTRAR NUEVO PAGO", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					break;
				}
				DateTime fecharegistro = Convert.ToDateTime(rgvCobros.ChildRows[e.Row.Index].Cells[fechaemision.Name].Value);
				bool condicion = Convert.ToInt32(cmbEstado.SelectedIndex) == 0 && Convert.ToInt32(cmbFormaPago.SelectedIndex) == 1 && fecharegistro.Date == DateTime.Now.Date;
				venta.CodFacturaVenta = rgvCobros.ChildRows[e.Row.Index].Cells[codnota.Name].Value.ToString();
				venta.CodCliente = Convert.ToInt32(rgvCobros.ChildRows[e.Row.Index].Cells[codcliente.Name].Value.ToString());
				frmCancelarPago form2 = new frmCancelarPago();
				agregandoDatosDetRetAPago(form2, e);
				form2.CodNota = venta.CodFacturaVenta;
				form2.CodCliente = venta.CodCliente;
				form2.tipo = itipo;
				form2.VentComp = 1;
				form2.opcionSuma = (condicion ? 1 : 0);
				form2.vieneDe = "frmCobros";
				DialogResult dlgResult2 = form2.ShowDialog();
				muestraReten = 0;
				break;
			}
			case 4:
			{
				let.CodLetra = Convert.ToInt32(rgvCobros.ChildRows[e.Row.Index].Cells[codnota.Name].Value);
				frmCancelarPago form = new frmCancelarPago();
				agregandoDatosDetRetAPago(form, e);
				form.CodLetra = let.CodLetra;
				form.tipo = itipo;
				DialogResult dlgResult = form.ShowDialog();
				if (dlgResult != DialogResult.Yes)
				{
				}
				break;
			}
			}
			btnBusqueda.PerformClick();
		}
		else
		{
			if (!(celda.Value.ToString() == "Muestra Pagos"))
			{
				return;
			}
			switch (itipo)
			{
			case 3:
			{
				venta.CodFacturaVenta = rgvCobros.ChildRows[e.Row.Index].Cells[codnota.Name].Value.ToString();
				venta.Pendiente = Convert.ToDecimal(rgvCobros.ChildRows[e.Row.Index].Cells[pendiente.Name].Value.ToString());
				double totalM = Convert.ToDouble(rgvCobros.ChildRows[e.Row.Index].Cells[monto.Name].Value.ToString());
				frmMuestraPagos form4 = new frmMuestraPagos();
				form4.CodNota = Convert.ToInt32(venta.CodFacturaVenta);
				form4.Almacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
				if (cmbEstado.SelectedIndex == 0)
				{
					form4.montoTotal = Convert.ToDecimal(venta.Pendiente);
				}
				else if (cmbEstado.SelectedIndex == 1)
				{
					form4.montoTotal = Convert.ToDecimal(totalM);
				}
				form4.InOut = true;
				form4.tipo = 0;
				DialogResult dlgResult4 = form4.ShowDialog();
				break;
			}
			case 4:
			{
				let.CodLetra = Convert.ToInt32(rgvCobros.ChildRows[e.Row.Index].Cells[codnota.Name].Value);
				frmMuestraPagos form3 = new frmMuestraPagos();
				form3.CodNota = let.CodLetra;
				form3.InOut = true;
				form3.tipo = 1;
				DialogResult dlgResult3 = form3.ShowDialog();
				if (dlgResult3 != DialogResult.Yes)
				{
				}
				break;
			}
			}
		}
	}

	private void checkBox1_Click(object sender, EventArgs e)
	{
		chkBox_Principal_Clicked();
	}

	private void btnexcel_Click(object sender, EventArgs e)
	{
		decimal totalMorosidad = default(decimal);
		decimal totalMontoenCuenta = default(decimal);
		decimal totalMontoRet = default(decimal);
		decimal totalMontoDet = default(decimal);
		decimal totalMonto = default(decimal);
		decimal totalPendiente = default(decimal);
		int ColInicial = 0;
		int RowInicial = 2;
		SLDocument sl = new SLDocument();
		SLStyle styleCenter = sl.CreateStyle();
		styleCenter.Alignment.Horizontal = HorizontalAlignmentValues.Center;
		styleCenter.Font.FontSize = 10.0;
		styleCenter.Font.Bold = true;
		foreach (GridViewDataColumn column in rgvCobros.Columns)
		{
			if (column.IsVisible && column.HeaderText != string.Empty)
			{
				ColInicial++;
				sl.SetCellValue(1, 1, "Lista de Cobros");
				sl.SetCellStyle(1, 1, styleCenter);
				sl.MergeWorksheetCells("A1", "P1");
				sl.SetCellValue(2, ColInicial, column.HeaderText.ToString());
				sl.SetCellStyle(RowInicial, ColInicial, styleCenter);
			}
		}
		sl.SetColumnWidth(1, 22.0);
		sl.SetColumnWidth(2, 35.0);
		sl.SetColumnWidth(3, 20.0);
		sl.SetColumnWidth(4, 15.0);
		sl.SetColumnWidth(5, 15.0);
		sl.SetColumnWidth(6, 35.0);
		sl.SetColumnWidth(7, 25.0);
		sl.SetColumnWidth(8, 15.0);
		sl.SetColumnWidth(9, 16.0);
		sl.SetColumnWidth(10, 16.0);
		sl.SetColumnWidth(11, 15.0);
		sl.SetColumnWidth(12, 15.0);
		sl.SetColumnWidth(13, 15.0);
		sl.SetColumnWidth(14, 15.0);
		sl.SetColumnWidth(15, 0.1);
		sl.SetColumnWidth(16, 0.1);
		sl.SetColumnWidth(17, 0.1);
		sl.SetColumnWidth(18, 15.0);
		sl.SetColumnWidth(19, 15.0);
		sl.SetColumnWidth(20, 15.0);
		sl.SetColumnWidth(21, 45.0);
		foreach (GridViewRowInfo row in rgvCobros.ChildRows)
		{
			decimal morosidad = default(decimal);
			RowInicial++;
			sl.SetCellValue(RowInicial, 1, row.Cells["almacen"].Value.ToString());
			sl.SetCellValue(RowInicial, 2, row.Cells["vendedor"].Value.ToString());
			sl.SetCellValue(RowInicial, 3, row.Cells["fechaemision"].Value.ToString());
			sl.SetCellValue(RowInicial, 4, row.Cells["numdocumento"].Value.ToString());
			sl.SetCellValue(RowInicial, 5, row.Cells["codperso"].Value.ToString());
			sl.SetCellValue(RowInicial, 6, row.Cells["cliente"].Value.ToString());
			sl.SetCellValue(RowInicial, 7, row.Cells["formapago"].Value.ToString());
			sl.SetCellValue(RowInicial, 8, row.Cells["fechavenc"].Value.ToString());
			sl.SetCellValue(RowInicial, 9, row.Cells["fechacancelado"].Value.ToString());
			sl.SetCellValue(RowInicial, 10, row.Cells["fecharegistro"].Value.ToString());
			string Verificarmorosidad = row.Cells["morosidad"].Value.ToString();
			if (int.TryParse(Verificarmorosidad, out var _))
			{
				morosidad = decimal.Parse(row.Cells["morosidad"].Value.ToString());
				sl.SetCellValue(RowInicial, 11, morosidad);
			}
			else
			{
				sl.SetCellValue(RowInicial, 11, Verificarmorosidad);
			}
			sl.SetCellValue(RowInicial, 12, row.Cells["moneda"].Value.ToString());
			decimal monto = decimal.Parse(row.Cells["monto"].Value.ToString());
			sl.SetCellValue(RowInicial, 13, monto);
			decimal pendiente = decimal.Parse(row.Cells["pendiente"].Value.ToString());
			sl.SetCellValue(RowInicial, 14, pendiente);
			sl.SetCellValue(RowInicial, 15, row.Cells["accion"].Value.ToString());
			sl.SetCellValue(RowInicial, 16, row.Cells["chkRet"].Value.ToString());
			sl.SetCellValue(RowInicial, 17, row.Cells["chkDet"].Value.ToString());
			decimal txtMontoEnCuenta = decimal.Parse(row.Cells["txtMontoEnCuenta"].Value.ToString());
			sl.SetCellValue(RowInicial, 18, txtMontoEnCuenta);
			decimal txtMontoRet = decimal.Parse(row.Cells["txtMontoRet"].Value.ToString());
			sl.SetCellValue(RowInicial, 19, txtMontoRet);
			decimal txtMontoDet = decimal.Parse(row.Cells["txtMontoDet"].Value.ToString());
			sl.SetCellValue(RowInicial, 20, txtMontoDet);
			sl.SetCellValue(RowInicial, 21, row.Cells["comentario"].Value.ToString());
			totalMorosidad += morosidad;
			totalMonto += monto;
			totalPendiente += pendiente;
			totalMontoenCuenta += txtMontoEnCuenta;
			totalMontoRet += txtMontoRet;
			totalMontoDet += txtMontoDet;
		}
		sl.SetCellValue(RowInicial + 8, 11, totalMorosidad);
		sl.SetCellValue(RowInicial + 8, 13, totalMonto);
		sl.SetCellValue(RowInicial + 8, 14, totalPendiente);
		sl.SetCellValue(RowInicial + 8, 18, totalMontoenCuenta);
		sl.SetCellValue(RowInicial + 8, 19, totalMontoRet);
		sl.SetCellValue(RowInicial + 8, 20, totalMontoDet);
		SLStyle styleFormat = sl.CreateStyle();
		styleFormat.Alignment.Horizontal = HorizontalAlignmentValues.Right;
		styleFormat.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		styleFormat.Border.TopBorder.Color = System.Drawing.Color.Black;
		styleFormat.Font.Bold = true;
		styleFormat.FormatCode = "[Black]#,##0.00";
		sl.SetCellStyle(RowInicial + 8, 11, styleFormat);
		sl.SetCellStyle(RowInicial + 8, 13, styleFormat);
		sl.SetCellStyle(RowInicial + 8, 14, styleFormat);
		sl.SetCellStyle(RowInicial + 8, 18, styleFormat);
		sl.SetCellStyle(RowInicial + 8, 19, styleFormat);
		sl.SetCellStyle(RowInicial + 8, 20, styleFormat);
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
			sfd.FileName = "Exportacion_Listado_de_Cobros";
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

	private void rgvCobros_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCobros));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
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
		Telerik.WinControls.UI.GridViewHyperlinkColumn gridViewHyperlinkColumn1 = new Telerik.WinControls.UI.GridViewHyperlinkColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn3 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn27 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn28 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn29 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn30 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn31 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn32 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn33 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn34 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnexcel = new System.Windows.Forms.Button();
		this.label17 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.lblTotalPendienteDolares = new System.Windows.Forms.Label();
		this.lblTotalPendienteConvertido = new System.Windows.Forms.Label();
		this.lblResultadoFiltradoConvertido = new System.Windows.Forms.Label();
		this.lblResultadoFiltradoDolares = new System.Windows.Forms.Label();
		this.btnPagoMultiple = new System.Windows.Forms.Button();
		this.lblTotalPendienteSoles = new System.Windows.Forms.Label();
		this.lblResultadoFiltradoSoles = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.label16 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.lblporvencer = new System.Windows.Forms.Label();
		this.lblvencidos = new System.Windows.Forms.Label();
		this.lblpendientes = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.cmbTipo = new System.Windows.Forms.ComboBox();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label15 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.dgvCobros = new System.Windows.Forms.DataGridView();
		this.chkSelector = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.codnota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.vendedor = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codperso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc_dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formpago = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fechavenc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechacancelado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.morosidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numunico = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.accion = new System.Windows.Forms.DataGridViewLinkColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.credito = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.xaprobars = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.chkRet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.chkDet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.txtMontoEnCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtMontoRet = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtMontoDet = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtTOTAL_R_D = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.bdMontoDetRet = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.bdTipoDetRet = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.muestraPagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.canjearPorLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.nuevaLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modificarLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.imprimirLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ingresoABancoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.backCobros = new System.ComponentModel.BackgroundWorker();
		this.rgvCobros = new Telerik.WinControls.UI.RadGridView();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCobros).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvCobros).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvCobros.MasterTemplate).BeginInit();
		this.rgvCobros.SuspendLayout();
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
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.lblTotalPendienteDolares);
		this.groupBox1.Controls.Add(this.lblTotalPendienteConvertido);
		this.groupBox1.Controls.Add(this.lblResultadoFiltradoConvertido);
		this.groupBox1.Controls.Add(this.lblResultadoFiltradoDolares);
		this.groupBox1.Controls.Add(this.btnPagoMultiple);
		this.groupBox1.Controls.Add(this.lblTotalPendienteSoles);
		this.groupBox1.Controls.Add(this.lblResultadoFiltradoSoles);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.lblporvencer);
		this.groupBox1.Controls.Add(this.lblvencidos);
		this.groupBox1.Controls.Add(this.lblpendientes);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbEstado);
		this.groupBox1.Controls.Add(this.cmbTipo);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.textBox3);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.textBox2);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1370, 140);
		this.groupBox1.TabIndex = 7;
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.btnexcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnexcel.Location = new System.Drawing.Point(562, 44);
		this.btnexcel.Name = "btnexcel";
		this.btnexcel.Size = new System.Drawing.Size(86, 45);
		this.btnexcel.TabIndex = 82;
		this.btnexcel.Text = "Excel";
		this.btnexcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnexcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnexcel.UseVisualStyleBackColor = true;
		this.btnexcel.Click += new System.EventHandler(btnexcel_Click);
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.ForeColor = System.Drawing.Color.Green;
		this.label17.Location = new System.Drawing.Point(673, 78);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(112, 13);
		this.label17.TabIndex = 79;
		this.label17.Text = "Total Pendiente $:";
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.Location = new System.Drawing.Point(952, 59);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(119, 13);
		this.label18.TabIndex = 81;
		this.label18.Text = "Total Pendiente S/:";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(952, 78);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(106, 13);
		this.label19.TabIndex = 80;
		this.label19.Text = "Suma Filtrado S/:";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.ForeColor = System.Drawing.Color.Green;
		this.label20.Location = new System.Drawing.Point(673, 99);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(103, 13);
		this.label20.TabIndex = 78;
		this.label20.Text = "Suma Filtrado $: ";
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.ForeColor = System.Drawing.Color.Goldenrod;
		this.label21.Location = new System.Drawing.Point(673, 25);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(119, 13);
		this.label21.TabIndex = 77;
		this.label21.Text = "Total Pendiente S/:";
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.ForeColor = System.Drawing.Color.Goldenrod;
		this.label22.Location = new System.Drawing.Point(673, 47);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(110, 13);
		this.label22.TabIndex = 76;
		this.label22.Text = "Suma Filtrado S/: ";
		this.lblTotalPendienteDolares.AutoSize = true;
		this.lblTotalPendienteDolares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalPendienteDolares.ForeColor = System.Drawing.Color.Green;
		this.lblTotalPendienteDolares.Location = new System.Drawing.Point(798, 78);
		this.lblTotalPendienteDolares.Name = "lblTotalPendienteDolares";
		this.lblTotalPendienteDolares.Size = new System.Drawing.Size(32, 13);
		this.lblTotalPendienteDolares.TabIndex = 73;
		this.lblTotalPendienteDolares.Text = "0.00";
		this.lblTotalPendienteConvertido.AutoSize = true;
		this.lblTotalPendienteConvertido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalPendienteConvertido.Location = new System.Drawing.Point(1077, 59);
		this.lblTotalPendienteConvertido.Name = "lblTotalPendienteConvertido";
		this.lblTotalPendienteConvertido.Size = new System.Drawing.Size(32, 13);
		this.lblTotalPendienteConvertido.TabIndex = 75;
		this.lblTotalPendienteConvertido.Text = "0.00";
		this.lblResultadoFiltradoConvertido.AutoSize = true;
		this.lblResultadoFiltradoConvertido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblResultadoFiltradoConvertido.Location = new System.Drawing.Point(1077, 78);
		this.lblResultadoFiltradoConvertido.Name = "lblResultadoFiltradoConvertido";
		this.lblResultadoFiltradoConvertido.Size = new System.Drawing.Size(32, 13);
		this.lblResultadoFiltradoConvertido.TabIndex = 74;
		this.lblResultadoFiltradoConvertido.Text = "0.00";
		this.lblResultadoFiltradoDolares.AutoSize = true;
		this.lblResultadoFiltradoDolares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblResultadoFiltradoDolares.ForeColor = System.Drawing.Color.Green;
		this.lblResultadoFiltradoDolares.Location = new System.Drawing.Point(798, 99);
		this.lblResultadoFiltradoDolares.Name = "lblResultadoFiltradoDolares";
		this.lblResultadoFiltradoDolares.Size = new System.Drawing.Size(32, 13);
		this.lblResultadoFiltradoDolares.TabIndex = 72;
		this.lblResultadoFiltradoDolares.Text = "0.00";
		this.btnPagoMultiple.Enabled = false;
		this.btnPagoMultiple.Location = new System.Drawing.Point(562, 94);
		this.btnPagoMultiple.Name = "btnPagoMultiple";
		this.btnPagoMultiple.Size = new System.Drawing.Size(86, 33);
		this.btnPagoMultiple.TabIndex = 71;
		this.btnPagoMultiple.Text = "Pago Multiple";
		this.btnPagoMultiple.UseVisualStyleBackColor = true;
		this.btnPagoMultiple.Click += new System.EventHandler(btnPagoMultiple_Click);
		this.lblTotalPendienteSoles.AutoSize = true;
		this.lblTotalPendienteSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalPendienteSoles.ForeColor = System.Drawing.Color.Goldenrod;
		this.lblTotalPendienteSoles.Location = new System.Drawing.Point(798, 25);
		this.lblTotalPendienteSoles.Name = "lblTotalPendienteSoles";
		this.lblTotalPendienteSoles.Size = new System.Drawing.Size(32, 13);
		this.lblTotalPendienteSoles.TabIndex = 70;
		this.lblTotalPendienteSoles.Text = "0.00";
		this.lblResultadoFiltradoSoles.AutoSize = true;
		this.lblResultadoFiltradoSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblResultadoFiltradoSoles.ForeColor = System.Drawing.Color.Goldenrod;
		this.lblResultadoFiltradoSoles.Location = new System.Drawing.Point(798, 47);
		this.lblResultadoFiltradoSoles.Name = "lblResultadoFiltradoSoles";
		this.lblResultadoFiltradoSoles.Size = new System.Drawing.Size(32, 13);
		this.lblResultadoFiltradoSoles.TabIndex = 69;
		this.lblResultadoFiltradoSoles.Text = "0.00";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(12, 104);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 22;
		this.dtpFecha2.ValueChanged += new System.EventHandler(dtpFecha2_ValueChanged);
		this.label16.AutoSize = true;
		this.label16.BackColor = System.Drawing.Color.Transparent;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.ForeColor = System.Drawing.Color.SteelBlue;
		this.label16.Location = new System.Drawing.Point(165, 93);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(65, 12);
		this.label16.TabIndex = 68;
		this.label16.Text = "Forma Pago";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(9, 88);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 26;
		this.label2.Text = "Hasta";
		this.label2.Click += new System.EventHandler(label2_Click);
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Items.AddRange(new object[2] { "CREDITO", "CONTADO" });
		this.cmbFormaPago.Location = new System.Drawing.Point(167, 109);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(123, 20);
		this.cmbFormaPago.TabIndex = 67;
		this.cmbFormaPago.SelectedIndexChanged += new System.EventHandler(cmbFormaPago_SelectedIndexChanged);
		this.lblporvencer.AutoSize = true;
		this.lblporvencer.ForeColor = System.Drawing.Color.SeaGreen;
		this.lblporvencer.Location = new System.Drawing.Point(1331, 37);
		this.lblporvencer.Name = "lblporvencer";
		this.lblporvencer.Size = new System.Drawing.Size(13, 13);
		this.lblporvencer.TabIndex = 55;
		this.lblporvencer.Text = "0";
		this.lblvencidos.AutoSize = true;
		this.lblvencidos.ForeColor = System.Drawing.Color.Red;
		this.lblvencidos.Location = new System.Drawing.Point(1331, 54);
		this.lblvencidos.Name = "lblvencidos";
		this.lblvencidos.Size = new System.Drawing.Size(13, 13);
		this.lblvencidos.TabIndex = 54;
		this.lblvencidos.Text = "0";
		this.lblpendientes.AutoSize = true;
		this.lblpendientes.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.lblpendientes.Location = new System.Drawing.Point(1331, 20);
		this.lblpendientes.Name = "lblpendientes";
		this.lblpendientes.Size = new System.Drawing.Size(13, 13);
		this.lblpendientes.TabIndex = 53;
		this.lblpendientes.Text = "0";
		this.label11.AutoSize = true;
		this.label11.ForeColor = System.Drawing.Color.SeaGreen;
		this.label11.Location = new System.Drawing.Point(1245, 37);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(80, 13);
		this.label11.TabIndex = 52;
		this.label11.Text = "POR VENCER:";
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Red;
		this.label8.Location = new System.Drawing.Point(1260, 54);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(65, 13);
		this.label8.TabIndex = 51;
		this.label8.Text = "VENCIDOS:";
		this.label12.AutoSize = true;
		this.label12.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label12.Location = new System.Drawing.Point(1246, 20);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(79, 13);
		this.label12.TabIndex = 50;
		this.label12.Text = "PENDIENTES:";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label6.Location = new System.Drawing.Point(1184, 90);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 37;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(368, 47);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 36;
		this.label5.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(403, 47);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 35;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(336, 47);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 34;
		this.label10.Text = "Filtro";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(338, 62);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(179, 20);
		this.txtFiltro.TabIndex = 33;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(9, 3);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(49, 12);
		this.label9.TabIndex = 32;
		this.label9.Text = "Almacen";
		this.cmbAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(12, 19);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(121, 20);
		this.cmbAlmacen.TabIndex = 31;
		this.cmbAlmacen.SelectedIndexChanged += new System.EventHandler(cmbAlmacen_SelectedIndexChanged);
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList1;
		this.btnBusqueda.Location = new System.Drawing.Point(564, 6);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(78, 33);
		this.btnBusqueda.TabIndex = 30;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.btnReporte.ImageIndex = 7;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(566, 47);
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
		this.label4.Location = new System.Drawing.Point(165, 46);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 12);
		this.label4.TabIndex = 28;
		this.label4.Text = "Estado";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(166, 3);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(26, 12);
		this.label3.TabIndex = 27;
		this.label3.Text = "Tipo";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(8, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 25;
		this.label1.Text = "Desde";
		this.label1.Click += new System.EventHandler(label1_Click);
		this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[2] { "PENDIENTES", "CANCELADOS" });
		this.cmbEstado.Location = new System.Drawing.Point(168, 62);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(122, 20);
		this.cmbEstado.TabIndex = 24;
		this.cmbEstado.SelectionChangeCommitted += new System.EventHandler(cmbEstado_SelectionChangeCommitted);
		this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbTipo.FormattingEnabled = true;
		this.cmbTipo.Location = new System.Drawing.Point(169, 19);
		this.cmbTipo.Name = "cmbTipo";
		this.cmbTipo.Size = new System.Drawing.Size(121, 20);
		this.cmbTipo.TabIndex = 23;
		this.cmbTipo.SelectionChangeCommitted += new System.EventHandler(cmbTipo_SelectionChangeCommitted);
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(11, 61);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 21;
		this.dtpFecha1.ValueChanged += new System.EventHandler(dtpFecha1_ValueChanged);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(1350, 43);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(61, 9);
		this.label15.TabIndex = 66;
		this.label15.Text = "POR IMPRIMIR";
		this.label15.Visible = false;
		this.textBox3.BackColor = System.Drawing.Color.Turquoise;
		this.textBox3.Enabled = false;
		this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox3.Location = new System.Drawing.Point(1306, 38);
		this.textBox3.Name = "textBox3";
		this.textBox3.ReadOnly = true;
		this.textBox3.Size = new System.Drawing.Size(38, 17);
		this.textBox3.TabIndex = 65;
		this.textBox3.Visible = false;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(1350, 25);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(62, 9);
		this.label14.TabIndex = 64;
		this.label14.Text = "POR APROBAR";
		this.label14.Visible = false;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(1350, 9);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(54, 9);
		this.label13.TabIndex = 63;
		this.label13.Text = "MOROSIDAD";
		this.label13.Visible = false;
		this.textBox2.BackColor = System.Drawing.Color.Gold;
		this.textBox2.Enabled = false;
		this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox2.Location = new System.Drawing.Point(1306, 22);
		this.textBox2.Name = "textBox2";
		this.textBox2.ReadOnly = true;
		this.textBox2.Size = new System.Drawing.Size(38, 17);
		this.textBox2.TabIndex = 62;
		this.textBox2.Visible = false;
		this.textBox1.BackColor = System.Drawing.Color.Red;
		this.textBox1.Enabled = false;
		this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox1.Location = new System.Drawing.Point(1306, 6);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(38, 17);
		this.textBox1.TabIndex = 61;
		this.textBox1.Visible = false;
		this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button1.ImageKey = "g-icon-new-update.png";
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(1213, 12);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(87, 33);
		this.button1.TabIndex = 56;
		this.button1.Text = "Actualizar";
		this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.dgvCobros.AllowUserToAddRows = false;
		this.dgvCobros.AllowUserToDeleteRows = false;
		this.dgvCobros.AllowUserToResizeColumns = false;
		this.dgvCobros.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCobros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvCobros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCobros.Columns.AddRange(this.chkSelector, this.codnota, this.almacen, this.vendedor, this.zona, this.fechaemision, this.tipo, this.numdocumento, this.documento, this.codcliente, this.codperso, this.ruc_dni, this.cliente, this.formpago, this.fechavenc, this.fechacancelado, this.fecharegistro, this.morosidad, this.moneda, this.monto, this.pendiente, this.banco, this.numunico, this.accion, this.cantidad, this.contado, this.credito, this.xaprobars, this.chkRet, this.chkDet, this.txtMontoEnCuenta, this.txtMontoRet, this.txtMontoDet, this.txtTOTAL_R_D, this.comentario, this.bdMontoDetRet, this.bdTipoDetRet, this.codMoneda);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvCobros.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgvCobros.Location = new System.Drawing.Point(0, 140);
		this.dgvCobros.Name = "dgvCobros";
		this.dgvCobros.ReadOnly = true;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCobros.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgvCobros.RowHeadersVisible = false;
		this.dgvCobros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCobros.Size = new System.Drawing.Size(1370, 368);
		this.dgvCobros.TabIndex = 8;
		this.dgvCobros.Visible = false;
		this.dgvCobros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCobros_CellContentClick);
		this.dgvCobros.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvCobros_CellMouseDown);
		this.dgvCobros.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_ColumnHeaderMouseClick);
		this.dgvCobros.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvCobros_DataError);
		this.dgvCobros.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCobros_RowStateChanged);
		this.dgvCobros.Sorted += new System.EventHandler(dgvCobros_Sorted);
		this.dgvCobros.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvCobros_KeyDown);
		this.chkSelector.Frozen = true;
		this.chkSelector.HeaderText = "#";
		this.chkSelector.Name = "chkSelector";
		this.chkSelector.ReadOnly = true;
		this.chkSelector.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.chkSelector.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.chkSelector.Width = 25;
		this.codnota.DataPropertyName = "codigo";
		this.codnota.Frozen = true;
		this.codnota.HeaderText = "codnota";
		this.codnota.Name = "codnota";
		this.codnota.ReadOnly = true;
		this.codnota.Visible = false;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.Width = 150;
		this.vendedor.DataPropertyName = "vendedor";
		this.vendedor.HeaderText = "Vendedor";
		this.vendedor.Name = "vendedor";
		this.vendedor.ReadOnly = true;
		this.vendedor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.vendedor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.vendedor.Width = 80;
		this.zona.DataPropertyName = "zona";
		this.zona.HeaderText = "Zona";
		this.zona.Name = "zona";
		this.zona.ReadOnly = true;
		this.zona.Visible = false;
		this.fechaemision.DataPropertyName = "fechaemision";
		this.fechaemision.HeaderText = "Fec. Emi.";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechaemision.Width = 70;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "tipo";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Visible = false;
		this.numdocumento.DataPropertyName = "numdocumento";
		this.numdocumento.HeaderText = "N° Documento";
		this.numdocumento.Name = "numdocumento";
		this.numdocumento.ReadOnly = true;
		this.numdocumento.Width = 90;
		this.documento.DataPropertyName = "docref";
		this.documento.HeaderText = "Documento";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Visible = false;
		this.documento.Width = 90;
		this.codcliente.DataPropertyName = "codCliente";
		this.codcliente.HeaderText = "codcliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.ReadOnly = true;
		this.codcliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codcliente.Visible = false;
		this.codperso.DataPropertyName = "ruc_dni";
		this.codperso.HeaderText = "Codigo";
		this.codperso.Name = "codperso";
		this.codperso.ReadOnly = true;
		this.codperso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ruc_dni.DataPropertyName = "codigopersonalizado";
		this.ruc_dni.HeaderText = "Ruc_Dni";
		this.ruc_dni.Name = "ruc_dni";
		this.ruc_dni.ReadOnly = true;
		this.ruc_dni.Visible = false;
		this.cliente.DataPropertyName = "razonsocial";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Width = 160;
		this.formpago.DataPropertyName = "formapago";
		this.formpago.HeaderText = "Forma Pago";
		this.formpago.Name = "formpago";
		this.formpago.ReadOnly = true;
		this.formpago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.fechavenc.DataPropertyName = "fechavence";
		this.fechavenc.HeaderText = "Fec. Ven.";
		this.fechavenc.Name = "fechavenc";
		this.fechavenc.ReadOnly = true;
		this.fechavenc.Width = 70;
		this.fechacancelado.DataPropertyName = "fechacancelado";
		dataGridViewCellStyle7.NullValue = null;
		this.fechacancelado.DefaultCellStyle = dataGridViewCellStyle7;
		this.fechacancelado.HeaderText = "Fec. Can.";
		this.fechacancelado.Name = "fechacancelado";
		this.fechacancelado.ReadOnly = true;
		this.fechacancelado.Width = 70;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fec. Reg.";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.morosidad.DataPropertyName = "diasmora";
		this.morosidad.HeaderText = "Mora";
		this.morosidad.Name = "morosidad";
		this.morosidad.ReadOnly = true;
		this.morosidad.Width = 60;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Width = 70;
		this.monto.DataPropertyName = "total";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N2";
		dataGridViewCellStyle8.NullValue = null;
		this.monto.DefaultCellStyle = dataGridViewCellStyle8;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.Width = 70;
		this.pendiente.DataPropertyName = "pendiente";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N2";
		dataGridViewCellStyle9.NullValue = null;
		this.pendiente.DefaultCellStyle = dataGridViewCellStyle9;
		this.pendiente.HeaderText = "Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.Width = 70;
		this.banco.DataPropertyName = "banco";
		this.banco.HeaderText = "Banco";
		this.banco.Name = "banco";
		this.banco.ReadOnly = true;
		this.banco.Visible = false;
		this.banco.Width = 90;
		this.numunico.DataPropertyName = "num";
		this.numunico.HeaderText = "N° único";
		this.numunico.Name = "numunico";
		this.numunico.ReadOnly = true;
		this.numunico.Visible = false;
		this.numunico.Width = 80;
		this.accion.DataPropertyName = "accion";
		this.accion.HeaderText = "Acción";
		this.accion.Name = "accion";
		this.accion.ReadOnly = true;
		this.accion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.accion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.cantidad.DataPropertyName = "cantpagos";
		this.cantidad.HeaderText = "Cant. pagos";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Visible = false;
		this.contado.DataPropertyName = "contado";
		this.contado.HeaderText = "contado";
		this.contado.Name = "contado";
		this.contado.ReadOnly = true;
		this.contado.Visible = false;
		this.credito.DataPropertyName = "credito";
		this.credito.HeaderText = "credito";
		this.credito.Name = "credito";
		this.credito.ReadOnly = true;
		this.credito.Visible = false;
		this.xaprobars.DataPropertyName = "xaprobar";
		this.xaprobars.HeaderText = "Aprobado";
		this.xaprobars.Name = "xaprobars";
		this.xaprobars.ReadOnly = true;
		this.xaprobars.Visible = false;
		this.chkRet.HeaderText = "RET";
		this.chkRet.Name = "chkRet";
		this.chkRet.ReadOnly = true;
		this.chkRet.Width = 40;
		this.chkDet.HeaderText = "DET";
		this.chkDet.Name = "chkDet";
		this.chkDet.ReadOnly = true;
		this.chkDet.Width = 40;
		this.txtMontoEnCuenta.DataPropertyName = "montoEnCuenta";
		this.txtMontoEnCuenta.HeaderText = "Monto en Cuenta";
		this.txtMontoEnCuenta.Name = "txtMontoEnCuenta";
		this.txtMontoEnCuenta.ReadOnly = true;
		this.txtMontoRet.HeaderText = "Monto RET.";
		this.txtMontoRet.Name = "txtMontoRet";
		this.txtMontoRet.ReadOnly = true;
		this.txtMontoDet.HeaderText = "Monto DET.";
		this.txtMontoDet.Name = "txtMontoDet";
		this.txtMontoDet.ReadOnly = true;
		this.txtTOTAL_R_D.HeaderText = "TOTAL";
		this.txtTOTAL_R_D.Name = "txtTOTAL_R_D";
		this.txtTOTAL_R_D.ReadOnly = true;
		this.txtTOTAL_R_D.Visible = false;
		this.comentario.DataPropertyName = "comentario";
		this.comentario.HeaderText = "Coment. Pago";
		this.comentario.Name = "comentario";
		this.comentario.ReadOnly = true;
		this.bdMontoDetRet.DataPropertyName = "ctdadDetRet";
		this.bdMontoDetRet.HeaderText = "bdMontoDetRet";
		this.bdMontoDetRet.Name = "bdMontoDetRet";
		this.bdMontoDetRet.ReadOnly = true;
		this.bdMontoDetRet.Visible = false;
		this.bdTipoDetRet.DataPropertyName = "tipoDetRet";
		this.bdTipoDetRet.HeaderText = "bdTipoDetRet";
		this.bdTipoDetRet.Name = "bdTipoDetRet";
		this.bdTipoDetRet.ReadOnly = true;
		this.bdTipoDetRet.Visible = false;
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
		this.imprimirLetraToolStripMenuItem.Name = "imprimirLetraToolStripMenuItem";
		this.imprimirLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.imprimirLetraToolStripMenuItem.Text = "Imprimir Letra";
		this.ingresoABancoToolStripMenuItem.Name = "ingresoABancoToolStripMenuItem";
		this.ingresoABancoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.ingresoABancoToolStripMenuItem.Text = "Ingreso a banco";
		this.ingresoABancoToolStripMenuItem.Click += new System.EventHandler(ingresoABancoToolStripMenuItem_Click);
		this.backCobros.WorkerReportsProgress = true;
		this.backCobros.WorkerSupportsCancellation = true;
		this.backCobros.DoWork += new System.ComponentModel.DoWorkEventHandler(backCobros_DoWork);
		this.backCobros.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backCobros_ProgressChanged);
		this.backCobros.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backCobros_RunWorkerCompleted);
		this.rgvCobros.Controls.Add(this.checkBox1);
		this.rgvCobros.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvCobros.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
		this.rgvCobros.Location = new System.Drawing.Point(0, 140);
		this.rgvCobros.MasterTemplate.AllowAddNewRow = false;
		gridViewCheckBoxColumn1.AllowSort = false;
		gridViewCheckBoxColumn1.EnableHeaderCheckBox = true;
		gridViewCheckBoxColumn1.HeaderText = "";
		gridViewCheckBoxColumn1.Name = "chkSelector";
		gridViewCheckBoxColumn1.Width = 23;
		gridViewTextBoxColumn1.FieldName = "codigo";
		gridViewTextBoxColumn1.HeaderText = "codnota";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codnota";
		gridViewTextBoxColumn2.FieldName = "almacen";
		gridViewTextBoxColumn2.HeaderText = "Almacen";
		gridViewTextBoxColumn2.Name = "almacen";
		gridViewTextBoxColumn2.Width = 150;
		gridViewTextBoxColumn3.FieldName = "vendedor";
		gridViewTextBoxColumn3.HeaderText = "Vendedor";
		gridViewTextBoxColumn3.Name = "vendedor";
		gridViewTextBoxColumn3.Width = 90;
		gridViewTextBoxColumn4.FieldName = "zona";
		gridViewTextBoxColumn4.HeaderText = "Zona";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "zona";
		gridViewTextBoxColumn4.Width = 100;
		gridViewTextBoxColumn5.FieldName = "fechaemision";
		gridViewTextBoxColumn5.HeaderText = "Fec. Emi.";
		gridViewTextBoxColumn5.Name = "fechaemision";
		gridViewTextBoxColumn5.Width = 70;
		gridViewTextBoxColumn6.FieldName = "tipo";
		gridViewTextBoxColumn6.HeaderText = "tipo";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "tipo";
		gridViewTextBoxColumn6.Width = 100;
		gridViewTextBoxColumn7.FieldName = "numdocumento";
		gridViewTextBoxColumn7.HeaderText = "N° Documento";
		gridViewTextBoxColumn7.Name = "numdocumento";
		gridViewTextBoxColumn7.Width = 90;
		gridViewTextBoxColumn8.FieldName = "docref";
		gridViewTextBoxColumn8.HeaderText = "Documento";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "documento";
		gridViewTextBoxColumn8.Width = 90;
		gridViewTextBoxColumn9.FieldName = "codcliente";
		gridViewTextBoxColumn9.HeaderText = "codcliente";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "codcliente";
		gridViewTextBoxColumn9.Width = 100;
		gridViewTextBoxColumn10.FieldName = "ruc_dni";
		gridViewTextBoxColumn10.HeaderText = "Codigo";
		gridViewTextBoxColumn10.Name = "codperso";
		gridViewTextBoxColumn10.Width = 100;
		gridViewTextBoxColumn11.FieldName = "codigopersonalizado";
		gridViewTextBoxColumn11.HeaderText = "Ruc_Dni";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "ruc_dni";
		gridViewTextBoxColumn11.Width = 100;
		gridViewTextBoxColumn12.FieldName = "razonsocial";
		gridViewTextBoxColumn12.HeaderText = "Cliente";
		gridViewTextBoxColumn12.Name = "cliente";
		gridViewTextBoxColumn12.Width = 160;
		gridViewTextBoxColumn13.FieldName = "formapago";
		gridViewTextBoxColumn13.HeaderText = "Forma Pago";
		gridViewTextBoxColumn13.Name = "formapago";
		gridViewTextBoxColumn13.Width = 100;
		gridViewTextBoxColumn14.FieldName = "fechavence";
		gridViewTextBoxColumn14.HeaderText = "Fec. Ven.";
		gridViewTextBoxColumn14.Name = "fechavenc";
		gridViewTextBoxColumn14.Width = 70;
		gridViewTextBoxColumn15.FieldName = "fechacancelado";
		gridViewTextBoxColumn15.HeaderText = "Fec. Can.";
		gridViewTextBoxColumn15.Name = "fechacancelado";
		gridViewTextBoxColumn15.Width = 70;
		gridViewTextBoxColumn16.FieldName = "fecharegistro";
		gridViewTextBoxColumn16.HeaderText = "Fec. Reg.";
		gridViewTextBoxColumn16.Name = "fecharegistro";
		gridViewTextBoxColumn16.Width = 100;
		gridViewTextBoxColumn17.EnableExpressionEditor = false;
		gridViewTextBoxColumn17.FieldName = "diasmora";
		gridViewTextBoxColumn17.HeaderText = "Mora";
		gridViewTextBoxColumn17.Name = "morosidad";
		gridViewTextBoxColumn17.Width = 70;
		gridViewTextBoxColumn18.FieldName = "moneda";
		gridViewTextBoxColumn18.HeaderText = "Moneda";
		gridViewTextBoxColumn18.Name = "moneda";
		gridViewTextBoxColumn18.Width = 70;
		gridViewTextBoxColumn19.FieldName = "total";
		gridViewTextBoxColumn19.HeaderText = "Monto";
		gridViewTextBoxColumn19.Name = "monto";
		gridViewTextBoxColumn19.Width = 70;
		gridViewTextBoxColumn20.FieldName = "pendiente";
		gridViewTextBoxColumn20.HeaderText = "Pendiente";
		gridViewTextBoxColumn20.Name = "pendiente";
		gridViewTextBoxColumn20.Width = 70;
		gridViewTextBoxColumn21.FieldName = "banco";
		gridViewTextBoxColumn21.HeaderText = "Banco";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "banco";
		gridViewTextBoxColumn21.Width = 90;
		gridViewTextBoxColumn22.FieldName = "num";
		gridViewTextBoxColumn22.HeaderText = "N° único";
		gridViewTextBoxColumn22.IsVisible = false;
		gridViewTextBoxColumn22.Name = "numunico";
		gridViewTextBoxColumn22.Width = 80;
		gridViewHyperlinkColumn1.FieldName = "accion";
		gridViewHyperlinkColumn1.HeaderText = "Acción";
		gridViewHyperlinkColumn1.Name = "accion";
		gridViewHyperlinkColumn1.Width = 100;
		gridViewTextBoxColumn23.FieldName = "cantpagos";
		gridViewTextBoxColumn23.HeaderText = "Cant. pagos";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "cantidad";
		gridViewTextBoxColumn23.Width = 100;
		gridViewTextBoxColumn24.FieldName = "contado";
		gridViewTextBoxColumn24.HeaderText = "contado";
		gridViewTextBoxColumn24.IsVisible = false;
		gridViewTextBoxColumn24.Name = "contado";
		gridViewTextBoxColumn24.Width = 100;
		gridViewTextBoxColumn25.FieldName = "credito";
		gridViewTextBoxColumn25.HeaderText = "credito";
		gridViewTextBoxColumn25.IsVisible = false;
		gridViewTextBoxColumn25.Name = "credito";
		gridViewTextBoxColumn25.Width = 100;
		gridViewTextBoxColumn26.FieldName = "xaprobar";
		gridViewTextBoxColumn26.HeaderText = "Aprobado";
		gridViewTextBoxColumn26.IsVisible = false;
		gridViewTextBoxColumn26.Name = "xaprobars";
		gridViewTextBoxColumn26.Width = 100;
		gridViewCheckBoxColumn2.HeaderText = "RET";
		gridViewCheckBoxColumn2.Name = "chkRet";
		gridViewCheckBoxColumn2.Width = 40;
		gridViewCheckBoxColumn3.HeaderText = "DET";
		gridViewCheckBoxColumn3.Name = "chkDet";
		gridViewCheckBoxColumn3.Width = 40;
		gridViewTextBoxColumn27.FieldName = "montoEnCuenta";
		gridViewTextBoxColumn27.HeaderText = "Monto en Cuenta";
		gridViewTextBoxColumn27.Name = "txtMontoEnCuenta";
		gridViewTextBoxColumn27.Width = 100;
		gridViewTextBoxColumn28.HeaderText = "Monto RET.";
		gridViewTextBoxColumn28.Name = "txtMontoRet";
		gridViewTextBoxColumn28.Width = 100;
		gridViewTextBoxColumn29.HeaderText = "Monto DET.";
		gridViewTextBoxColumn29.Name = "txtMontoDet";
		gridViewTextBoxColumn29.Width = 100;
		gridViewTextBoxColumn30.HeaderText = "TOTAL";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "txtTOTAL_R_D";
		gridViewTextBoxColumn30.Width = 100;
		gridViewTextBoxColumn31.FieldName = "comentario";
		gridViewTextBoxColumn31.HeaderText = "Coment. Pago";
		gridViewTextBoxColumn31.Name = "comentario";
		gridViewTextBoxColumn31.Width = 100;
		gridViewTextBoxColumn32.FieldName = "ctdadDetRet";
		gridViewTextBoxColumn32.HeaderText = "bdMontoDetRet";
		gridViewTextBoxColumn32.IsVisible = false;
		gridViewTextBoxColumn32.Name = "bdMontoDetRet";
		gridViewTextBoxColumn32.Width = 100;
		gridViewTextBoxColumn33.FieldName = "tipoDetRet";
		gridViewTextBoxColumn33.HeaderText = "bdTipoDetRet";
		gridViewTextBoxColumn33.IsVisible = false;
		gridViewTextBoxColumn33.Name = "bdTipoDetRet";
		gridViewTextBoxColumn33.Width = 100;
		gridViewTextBoxColumn34.FieldName = "codMoneda";
		gridViewTextBoxColumn34.HeaderText = "codMoneda";
		gridViewTextBoxColumn34.IsVisible = false;
		gridViewTextBoxColumn34.Name = "codMoneda";
		gridViewTextBoxColumn34.Width = 100;
		this.rgvCobros.MasterTemplate.Columns.AddRange(gridViewCheckBoxColumn1, gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewHyperlinkColumn1, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewCheckBoxColumn2, gridViewCheckBoxColumn3, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32, gridViewTextBoxColumn33, gridViewTextBoxColumn34);
		this.rgvCobros.MasterTemplate.EnableFiltering = true;
		this.rgvCobros.MasterTemplate.EnableGrouping = false;
		this.rgvCobros.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvCobros.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvCobros.Name = "rgvCobros";
		this.rgvCobros.ReadOnly = true;
		this.rgvCobros.Size = new System.Drawing.Size(1370, 610);
		this.rgvCobros.TabIndex = 9;
		this.rgvCobros.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvCobros_CellClick);
		this.rgvCobros.DataError += new Telerik.WinControls.UI.GridViewDataErrorEventHandler(rgvCobros_DataError);
		this.rgvCobros.FilterChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(rgvCobros_FilterChanged);
		this.rgvCobros.KeyDown += new System.Windows.Forms.KeyEventHandler(rgvCobros_KeyDown);
		this.checkBox1.Location = new System.Drawing.Point(5, 8);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(15, 16);
		this.checkBox1.TabIndex = 2;
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.Click += new System.EventHandler(checkBox1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1370, 750);
		base.Controls.Add(this.rgvCobros);
		base.Controls.Add(this.dgvCobros);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.KeyPreview = true;
		base.Name = "frmCobros";
		base.ShowInTaskbar = false;
		this.Text = "Cobros";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmCobros_FormClosing);
		base.Load += new System.EventHandler(frmCobros_Load);
		base.Shown += new System.EventHandler(frmCobros_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCobros).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvCobros.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvCobros).EndInit();
		this.rgvCobros.ResumeLayout(false);
		this.rgvCobros.PerformLayout();
		base.ResumeLayout(false);
	}
}
