using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIGEFA.Administradores;
using SIGEFA.Alertas;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SISTEMA_BUNIFU.Alertas;
using Telerik.WinControls;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;

namespace SIGEFA.Formularios;

public class frmCajaVentasMovimientos : Office2007Form
{
	public class PDFFooter : PdfPageEventHelper
	{
		public override void OnEndPage(PdfWriter writer, Document document)
		{
			PdfPTable tab = new PdfPTable(1);
			PdfPCell cell = new PdfPCell(new Phrase("Prueba de Pie de Página"));
			cell.Border = 0;
			tab.TotalWidth = 300f;
			tab.AddCell(cell);
			tab.WriteSelectedRows(0, -1, 300f, 30f, writer.DirectContent);
		}
	}

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsSerie ser = new clsSerie();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public List<int> seleccion = new List<int>();

	private clsAdmSeparacion AdmSepa = new clsAdmSeparacion();

	private clsAdmParametro admParametro = new clsAdmParametro();

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private clsAdmPago admpago = new clsAdmPago();

	private clsAdmTarjetaPago admtarjeta = new clsAdmTarjetaPago();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private List<int> listaPagosAgrupados = new List<int>();

	private decimal Saldo = default(decimal);

	private decimal Ingresos = default(decimal);

	private decimal Egresos = default(decimal);

	private decimal totalVenta = default(decimal);

	private decimal totalDisponible = default(decimal);

	private decimal totalIngresos = default(decimal);

	private int estado_caja = 1;

	private int FilasChequeadas = 0;

	private decimal MontoRendido = default(decimal);

	private DataTable tabla = new DataTable();

	internal clsUsuario usuario_click = null;

	private Success success = null;

	private Info info = null;

	private Errors errors = null;

	private readonly List<GridViewRowInfo> previouslySelectedRows = new List<GridViewRowInfo>();

	private IContainer components = null;

	private RibbonBar ribbonBar1;

	private ImageList imageList1;

	private ButtonItem biEditar;

	private ButtonItem biEliminar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimir;

	private ButtonItem biIngreso;

	private ButtonItem biEgreso;

	private ComboBox cboMovimientos;

	private Label label1;

	private Panel panel1;

	private Panel panel2;

	private ImageList imageList2;

	private Button btnExit;

	private Label label2;

	private Panel panel3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label lblIngresos;

	private Label lblSaldoCaja;

	private Label lblAperturaCaja;

	private Label lblEgresos;

	private RibbonBar ribbonBar2;

	private ButtonItem biRencicionCaja;

	private ButtonItem btnDetalleCajaVentas;

	private ButtonItem biHistorialRendiciones;

	private ButtonItem biRendicionesContables;

	private ExpandablePanel expandablePanel1;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label lblColumna;

	private TextBox txtFiltro;

	private Button btnclose;

	private Label lblProperty;

	public DateTimePicker dtpfecha1;

	private Label lbCheque;

	private Label lbDeposito;

	private Label label13;

	private ButtonItem btnCierreyArqueoCajaVentas;

	private ButtonItem biVerificarRendicion;

	private ButtonItem buttonItem1;

	private Label lblCajaSeparacion;

	private Label label12;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo1;

	public ButtonItem biActualizar;

	private Label label3;

	private Label label15;

	private Label label16;

	private RadDropDownList cmbAlmacenes;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private Label lblNC;

	private Label label17;

	private Label lblVC;

	private Label label19;

	private ButtonItem buttonItem15;

	private ButtonItem btnVerificacionCaja;

	private Label lblPendiente;

	private ButtonItem buttonItem2;

	private Label label18;

	private Label lblIngresoTransferencia;

	private Label label14;

	private Label lblIngreso;

	private Label label22;

	private Label label21;

	private Label lblITarjeta;

	private Label label20;

	private ButtonItem buttonItem3;

	private ButtonItem buttonItem4;

	private ButtonItem buttonItem5;

	private ButtonItem buttonItem7;

	private ButtonItem buttonItem6;

	private GroupBox groupBox1;

	private RadGridView rgvMovimientosCajaChica;

	private Label label11;

	public frmCajaVentasMovimientos()
	{
		InitializeComponent();
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void biActualizar_Click(object sender, EventArgs e)
	{
		ListaCajaChicaDiaria();
		VerificaSaldoCajaVentas();
		VerificaSaldoCajaVentas1();
	}

	private void VerificaSaldoCajaVentas1()
	{
		double totPendiente = 0.0;
		foreach (GridViewRowInfo fila in rgvMovimientosCajaChica.Rows)
		{
			if (Convert.ToString(fila.Cells["PAGOCAJA"].Value) == "PENDIENTE")
			{
				totPendiente += Convert.ToDouble((fila.Cells["monto"].Value == DBNull.Value) ? ((object)0) : fila.Cells["monto"].Value);
			}
		}
		label11.Text = totPendiente.ToString("## ### ##0.00");
		lblIngresos.Text = (Convert.ToDouble(lblIngresos.Text) + Convert.ToDouble(label11.Text)).ToString("## ### ##0.00");
	}

	private void ListaCajaChicaDiaria()
	{
		tabla = null;
		rgvMovimientosCajaChica.Rows.Clear();
		if (Convert.ToInt32(cmbAlmacenes.SelectedValue) != 0)
		{
			if (Caja != null)
			{
				tabla = AdmCaja.ListaCajaDiaria(Caja.Codsucursal, dtpfecha1.Value.Date, Caja.Codcaja, Convert.ToInt32(cmbAlmacenes.SelectedValue), estado_caja);
			}
			if (tabla == null)
			{
				return;
			}
			int fila = 0;
			foreach (DataRow row in tabla.Rows)
			{
				int check = ((row[30].ToString() != "") ? 1 : 0);
				rgvMovimientosCajaChica.Rows.Add(check, row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString(), row[26].ToString(), row[27].ToString(), row[28].ToString(), row[29].ToString(), row[30].ToString(), row[31].ToString(), row[32].ToString());
				fila++;
			}
			CalculoSaldo();
			darformato();
		}
		else
		{
			if (Convert.ToInt32(cmbAlmacenes.SelectedValue) != 0)
			{
				return;
			}
			if (Caja != null)
			{
				tabla = AdmCaja.ListaCajaDiaria(Caja.Codsucursal, dtpfecha1.Value.Date, 0, Convert.ToInt32(cmbAlmacenes.SelectedValue), estado_caja);
			}
			if (tabla == null)
			{
				return;
			}
			int fila2 = 0;
			foreach (DataRow row2 in tabla.Rows)
			{
				int check2 = ((row2[30].ToString() != "") ? 1 : 0);
				rgvMovimientosCajaChica.Rows.Add(check2, row2[0].ToString(), row2[1].ToString(), row2[2].ToString(), row2[3].ToString(), row2[4].ToString(), row2[5].ToString(), row2[6].ToString(), row2[7].ToString(), row2[8].ToString(), row2[9].ToString(), row2[10].ToString(), row2[11].ToString(), row2[12].ToString(), row2[13].ToString(), row2[14].ToString(), row2[15].ToString(), row2[16].ToString(), row2[17].ToString(), row2[18].ToString(), row2[19].ToString(), row2[20].ToString(), row2[21].ToString(), row2[22].ToString(), row2[23].ToString(), row2[24].ToString(), row2[25].ToString(), row2[26].ToString(), row2[27].ToString(), row2[28].ToString(), row2[29].ToString(), row2[30].ToString(), row2[31].ToString(), row2[32].ToString());
				fila2++;
			}
			CalculoSaldo();
			darformato();
		}
	}

	private void RecalcilaTotales()
	{
		decimal tingresos = default(decimal);
		decimal tegresos = default(decimal);
		decimal tsaldo = default(decimal);
		foreach (GridViewRowInfo row in rgvMovimientosCajaChica.Rows)
		{
			if (Convert.ToInt32(row.Cells["CODTIPO"].Value) == 1)
			{
				tsaldo += Convert.ToDecimal(row.Cells["monto"].Value);
				if (Convert.ToInt32(row.Cells["CODTIPO"].Value) == 1 && Convert.ToInt32(row.Cells["CODTIPOMOV"].Value) == 2)
				{
					tingresos += Convert.ToDecimal(row.Cells["monto"].Value);
				}
			}
			else if (Convert.ToInt32(row.Cells["CODTIPO"].Value) == 2)
			{
				tegresos += Convert.ToDecimal(row.Cells["monto"].Value);
			}
		}
		lblIngresos.Text = $"{tingresos:#,##0.00}";
		lblEgresos.Text = $"{tegresos:#,##0.00}";
		lblSaldoCaja.Text = $"{tsaldo:#,##0.00}";
	}

	private void darformato()
	{
		foreach (GridViewRowInfo row in rgvMovimientosCajaChica.Rows)
		{
			if (Convert.ToInt32(row.Cells["CODTIPO"].Value) == 1)
			{
				row.Cells["monto"].Style.ForeColor = Color.Blue;
				row.Cells["tipopagocaja"].Style.ForeColor = Color.Blue;
			}
			else if (Convert.ToInt32(row.Cells["CODTIPO"].Value) == 2)
			{
				row.Cells["monto"].Style.ForeColor = Color.Red;
				row.Cells["tipopagocaja"].Style.ForeColor = Color.Red;
			}
			if (Convert.ToInt32(row.Cells["codTipoPagoCaja"].Value) == 12)
			{
				row.Cells["monto"].Style.ForeColor = Color.DarkOrange;
				row.Cells["concepto"].Style.ForeColor = Color.DarkOrange;
				row.Cells["tipopagocaja"].Style.ForeColor = Color.DarkOrange;
				row.Cells["fecha"].Style.ForeColor = Color.DarkOrange;
				row.Cells["TipoMovimiento"].Style.ForeColor = Color.DarkOrange;
				row.Cells["PAGOCAJA"].Style.ForeColor = Color.DarkOrange;
			}
		}
	}

	private void VerificaSaldoCajaVentas()
	{
		cambiaEstadoBotones(v: true);
		Saldo = default(decimal);
		CalculoSaldo();
		Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, dtpfecha1.Value.Date, 1, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodUser);
		if (Caja != null)
		{
			estado_caja = 1;
			double visa = 0.0;
			double master = 0.0;
			if (Convert.ToInt32(cmbAlmacenes.SelectedValue) == 0)
			{
				visa = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), frmLogin.iCodSucursal, 1, frmLogin.iCodSucursal, 0);
				master = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), frmLogin.iCodSucursal, 3, frmLogin.iCodSucursal, 0);
				totalVenta = AdmCaja.SumaVentaEfectivoCaja(frmLogin.iCodSucursal, dtpfecha1.Value.Date, 0);
			}
			else
			{
				visa = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), Convert.ToInt32(cmbAlmacenes.SelectedValue), 1, frmLogin.iCodSucursal, Caja.Codcaja);
				master = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), Convert.ToInt32(cmbAlmacenes.SelectedValue), 3, frmLogin.iCodSucursal, Caja.Codcaja);
				totalVenta = AdmCaja.SumaVentaEfectivoCaja(Caja.Codsucursal, dtpfecha1.Value.Date, Caja.Codcaja);
				decimal montoNC = Enumerable.Sum<GridViewRowInfo>(Enumerable.Where<GridViewRowInfo>(rgvMovimientosCajaChica.Rows.Cast<GridViewRowInfo>().AsEnumerable(), (Func<GridViewRowInfo, bool>)((GridViewRowInfo x) => Convert.ToInt32(x.Cells["codTipoPagoCaja"].Value) == 10 && Convert.ToInt32(x.Cells["codSucursal"].Value) == Convert.ToInt32(cmbAlmacenes.SelectedValue))), (Func<GridViewRowInfo, decimal>)((GridViewRowInfo x) => Convert.ToDecimal(x.Cells["monto"].Value)));
				if (montoNC > 0m)
				{
					lblNC.Text = $"{montoNC:#,##0.00}";
				}
				else
				{
					lblNC.Text = $"{0.0:#,##0.00}";
				}
			}
			totalDisponible = Caja.TotalDisponible;
			totalIngresos = Caja.TotalIngreso - Caja.IngresoEfectivo - Caja.IngresoTransferencia - Caja.IngresoTarjeta;
			lblIngresos.Text = $"{totalIngresos:#,##0.00}";
			lblEgresos.Text = $"{Caja.TotalEgreso:#,##0.00}";
			lblAperturaCaja.Text = $"{Caja.Montoapertura:#,##0.00}";
			lblSaldoCaja.Text = $"{totalDisponible:#,##0.00}";
			lbDeposito.Text = $"{Caja.Totaldeposito:#,##0.00}";
			lbCheque.Text = $"{Caja.Totaltarnsferencia - Caja.IngresoTransferencia:#,##0.00}";
			lblCajaSeparacion.Text = $"{visa + master:#,##0.00}";
			label3.Text = $"{master:#,##0.00}";
			lblVC.Text = $"{Caja.Totalventacredito:#,##0.00}";
			lblPendiente.Text = $"{Caja.TotalPendiente:#,##0.00}";
			lblIngreso.Text = $"{Caja.IngresoEfectivo:#,##0.00}";
			lblITarjeta.Text = $"{Caja.IngresoTarjeta:#,##0.00}";
			lblIngresoTransferencia.Text = $"{Caja.IngresoTransferencia:#,##0.00}";
			Saldo = totalDisponible;
			if (Saldo > 0m)
			{
				biIngreso.Enabled = true;
				biEgreso.Enabled = true;
			}
			else
			{
				biIngreso.Enabled = true;
				biEgreso.Enabled = false;
			}
			return;
		}
		DateTime fechaCaja = dtpfecha1.Value.Date;
		if (DateTime.Now.Date > fechaCaja.Date)
		{
			Caja = AdmCaja.GetCaja(frmLogin.iCodSucursal, dtpfecha1.Value.Date, 1, Convert.ToInt32(cmbAlmacenes.SelectedValue));
			if (Caja != null)
			{
				estado_caja = 0;
				double visa2 = 0.0;
				double master2 = 0.0;
				if (Convert.ToInt32(cmbAlmacenes.SelectedValue) == 0)
				{
					visa2 = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), frmLogin.iCodSucursal, 1, frmLogin.iCodSucursal, 0);
					master2 = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), frmLogin.iCodSucursal, 3, frmLogin.iCodSucursal, 0);
					totalVenta = AdmCaja.SumaVentaEfectivoCaja(frmLogin.iCodSucursal, dtpfecha1.Value.Date, 0);
				}
				else
				{
					visa2 = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), Convert.ToInt32(cmbAlmacenes.SelectedValue), 1, frmLogin.iCodSucursal, Caja.Codcaja);
					master2 = admtarjeta.SumaTotalTarjetas(dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), dtpfecha1.Value.Date.ToString("yyyy/MM/dd"), Convert.ToInt32(cmbAlmacenes.SelectedValue), 3, frmLogin.iCodSucursal, Caja.Codcaja);
					totalVenta = AdmCaja.SumaVentaEfectivoCaja(Caja.Codsucursal, dtpfecha1.Value.Date, Caja.Codcaja);
				}
				totalDisponible = Caja.TotalDisponible;
				totalIngresos = Caja.TotalIngreso - Caja.IngresoEfectivo - Caja.IngresoTransferencia - Caja.IngresoTarjeta;
				lblIngresos.Text = $"{totalIngresos:#,##0.00}";
				lblEgresos.Text = $"{Caja.TotalEgreso:#,##0.00}";
				lblAperturaCaja.Text = $"{Caja.Montoapertura:#,##0.00}";
				lblSaldoCaja.Text = $"{totalDisponible:#,##0.00}";
				lbDeposito.Text = $"{Caja.Totaldeposito:#,##0.00}";
				lbCheque.Text = $"{Caja.Totaltarnsferencia - Caja.IngresoTransferencia:#,##0.00}";
				lblCajaSeparacion.Text = $"{visa2 + master2:#,##0.00}";
				label3.Text = $"{master2:#,##0.00}";
				lblVC.Text = $"{Caja.Totalventacredito:#,##0.00}";
				lblPendiente.Text = $"{Caja.TotalPendiente:#,##0.00}";
				lblIngreso.Text = $"{Caja.IngresoEfectivo:#,##0.00}";
				lblITarjeta.Text = $"{Caja.IngresoTarjeta:#,##0.00}";
				lblIngresoTransferencia.Text = $"{Caja.IngresoTransferencia:#,##0.00}";
				Saldo = totalDisponible;
				cambiaEstadoBotones(v: false);
			}
			else
			{
				MessageBox.Show("No se pudo obtener caja.\nCaja No Existe", "Error al encontrar caja", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Saldo = default(decimal);
				lblIngresos.Text = "0.00";
				lblEgresos.Text = "0.00";
				lblAperturaCaja.Text = "0.00";
				lblSaldoCaja.Text = "0.00";
				lblVC.Text = "0.00";
				lbDeposito.Text = "0.00";
				lbCheque.Text = "0.00";
				biIngreso.Enabled = false;
				biEgreso.Enabled = false;
			}
		}
		else
		{
			MessageBox.Show("Caja no aperturada");
			Saldo = default(decimal);
			lblIngresos.Text = "0.00";
			lblEgresos.Text = "0.00";
			lblAperturaCaja.Text = "0.00";
			lblSaldoCaja.Text = "0.00";
			lblVC.Text = "0.00";
			lbDeposito.Text = "0.00";
			lbCheque.Text = "0.00";
			biIngreso.Enabled = false;
			biEgreso.Enabled = false;
		}
	}

	private void cambiaEstadoBotones(bool v)
	{
		biIngreso.Enabled = v;
		biEgreso.Enabled = v;
		biEliminar.Enabled = v;
		btnCierreyArqueoCajaVentas.Enabled = v;
		btnDetalleCajaVentas.Enabled = v;
	}

	private void biIngreso_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaDiariaRegistro"] != null)
		{
			Application.OpenForms["frmCajaDiariaRegistro"].Activate();
			return;
		}
		frmCajaDiariaRegistro form = new frmCajaDiariaRegistro();
		form.Text = "INGRESO A CAJA CHICA";
		form.Tipo = true;
		form.codigocaja = Caja.Codcaja;
		form.Proceso = 1;
		form.direccioncaja = 1;
		form.SaldoCaja = Convert.ToDecimal(lblSaldoCaja.Text.Trim());
		form.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
		form.opcionSuma = 1;
		form.fechaRegistro = Convert.ToDateTime(dtpfecha1.Value);
		form.ShowDialog();
		VerificaSaldoCajaVentas();
		ListaCajaChicaDiaria();
	}

	private void biHistorialRendiciones_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaChicaRendicionHistorial"] != null)
		{
			Application.OpenForms["frmCajaChicaRendicionHistorial"].Activate();
			return;
		}
		frmCajaChicaRendicionHistorial form = new frmCajaChicaRendicionHistorial();
		form.ShowDialog();
		ListaCajaChicaDiaria();
	}

	private void biEgreso_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaDiariaRegistro"] != null)
		{
			Application.OpenForms["frmCajaDiariaRegistro"].Activate();
			return;
		}
		frmCajaDiariaRegistro form = new frmCajaDiariaRegistro();
		form.Text = "EGRESO A CAJA CHICA";
		form.Tipo = false;
		form.codigocaja = Caja.Codcaja;
		form.Proceso = 1;
		form.direccioncaja = 1;
		form.SaldoCaja = Convert.ToDecimal(lblSaldoCaja.Text.Trim());
		form.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
		form.opcionSuma = 1;
		form.fechaRegistro = Convert.ToDateTime(dtpfecha1.Value);
		form.ShowDialog();
		VerificaSaldoCajaVentas();
		ListaCajaChicaDiaria();
	}

	private void rgvMovimientosCajaChica_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			frmCajaChicaRegistro frm = new frmCajaChicaRegistro();
			if (rgvMovimientosCajaChica.SelectedRows[0].Cells["tipoMovimiento"].Value.ToString() == "INGRESO")
			{
				frm.Tipo = 1;
			}
			else if (rgvMovimientosCajaChica.SelectedRows[0].Cells["tipoMovimiento"].Value.ToString() == "EGRESO")
			{
				frm.Tipo = 2;
			}
			frm.Proceso = 3;
			frm.ShowDialog();
		}
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
		DialogResult dr = DialogResult.None;
		frmAutorizacion frm = new frmAutorizacion();
		dr = frm.ShowDialog();
		if (dr == DialogResult.OK)
		{
			Cursor = Cursors.WaitCursor;
			if (rgvMovimientosCajaChica.Rows.Count > 0 && rgvMovimientosCajaChica.CurrentRow.Index != -1)
			{
				if (Convert.ToInt32(rgvMovimientosCajaChica.CurrentRow.Cells["tipo_descripcion_ingreso"].Value) > 0)
				{
					if (admpago.AnularPago(Convert.ToInt32(rgvMovimientosCajaChica.CurrentRow.Cells["codpago"].Value)))
					{
						MessageBox.Show("Registro anulado correctamente");
						VerificaSaldoCajaVentas();
						ListaCajaChicaDiaria();
					}
					else
					{
						MessageBox.Show("Error al anular pago");
					}
				}
				else
				{
					MessageBox.Show("No está permitido eliminar pagos, por favor cambiar a pendiente de cobro");
				}
			}
			Cursor = Cursors.Default;
		}
		else
		{
			MessageBox.Show("Acceso incorrecto");
		}
	}

	private void CalculoSaldo()
	{
		try
		{
			decimal saldogrilla = default(decimal);
			decimal montoNC = Enumerable.Sum<GridViewRowInfo>(Enumerable.Where<GridViewRowInfo>(rgvMovimientosCajaChica.Rows.Cast<GridViewRowInfo>().AsEnumerable(), (Func<GridViewRowInfo, bool>)((GridViewRowInfo x) => Convert.ToInt32(x.Cells["codTipoPagoCaja"].Value) == 10)), (Func<GridViewRowInfo, decimal>)((GridViewRowInfo x) => Convert.ToDecimal(x.Cells["monto"].Value)));
			if (montoNC > 0m)
			{
				lblNC.Text = $"{montoNC:#,##0.00}";
			}
			else
			{
				lblNC.Text = $"{0.0:#,##0.00}";
			}
			foreach (GridViewRowInfo row in rgvMovimientosCajaChica.Rows)
			{
				if (Convert.ToInt32(row.Cells["CODTIPO"].Value) == 1)
				{
					if (Convert.ToInt32(row.Cells["CODTIPOMOV"].Value) == 1)
					{
						if (Convert.ToInt32(row.Cells["CODMONEDA"].Value) == 2)
						{
							saldogrilla += Convert.ToDecimal(row.Cells["monto"].Value);
							row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
							row.Cells["monto"].Value = string.Format("{0:#,##0.00}", Convert.ToDecimal(row.Cells["monto"].Value) / Convert.ToDecimal(row.Cells["TCVENTA"].Value));
						}
						else
						{
							saldogrilla += Convert.ToDecimal(row.Cells["monto"].Value);
							row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
						}
					}
					if (Convert.ToInt32(row.Cells["CODTIPOMOV"].Value) != 2)
					{
						continue;
					}
					if (Convert.ToInt32(row.Cells["codTipoPagoCaja"].Value) == 5)
					{
						if (Convert.ToInt32(row.Cells["CODMONEDA"].Value) == 2)
						{
							int codPago = Convert.ToInt32(row.Cells["codpago"].Value);
							if (admpago.obtenerOpcionDeSumaCaja(codPago) == 1)
							{
								saldogrilla += Convert.ToDecimal(row.Cells["monto"].Value);
							}
							row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
							row.Cells["monto"].Value = string.Format("{0:#,##0.00}", Convert.ToDecimal(row.Cells["monto"].Value) / Convert.ToDecimal(row.Cells["TCVENTA"].Value));
						}
						else
						{
							int codPago2 = Convert.ToInt32(row.Cells["codpago"].Value);
							if (admpago.obtenerOpcionDeSumaCaja(codPago2) == 1)
							{
								saldogrilla += Convert.ToDecimal(row.Cells["monto"].Value);
							}
							row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
						}
					}
					else
					{
						row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
					}
				}
				else
				{
					if (Convert.ToInt32(row.Cells["CODTIPO"].Value) != 2 || Convert.ToInt32(row.Cells["CODTIPOMOV"].Value) != 2)
					{
						continue;
					}
					if (Convert.ToInt32(row.Cells["codTipoPagoCaja"].Value) == 5)
					{
						if (Convert.ToInt32(row.Cells["CODMONEDA"].Value) == 2)
						{
							int codPago3 = Convert.ToInt32(row.Cells["codpago"].Value);
							if (admpago.obtenerOpcionDeSumaCaja(codPago3) == 1)
							{
								saldogrilla -= Convert.ToDecimal(row.Cells["monto"].Value);
							}
							row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
							row.Cells["monto"].Value = string.Format("{0:#,##0.00}", Convert.ToDecimal(row.Cells["monto"].Value) / Convert.ToDecimal(row.Cells["TCVENTA"].Value));
						}
						else
						{
							int codPago4 = Convert.ToInt32(row.Cells["codpago"].Value);
							if (admpago.obtenerOpcionDeSumaCaja(codPago4) == 1)
							{
								saldogrilla -= Convert.ToDecimal(row.Cells["monto"].Value);
							}
							row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
						}
					}
					else
					{
						row.Cells["saldocaja"].Value = $"{saldogrilla:#,##0.00}";
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dgvMovimientosCajaChica_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
	}

	private void biRendicionesContables_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaChicaRendicion"] != null)
		{
			Application.OpenForms["frmCajaChicaRendicion"].Activate();
			return;
		}
		frmCajaChicaRendicion form = new frmCajaChicaRendicion();
		form.MdiParent = base.MdiParent;
		form.Show();
	}

	private void biRencicionCaja_Click(object sender, EventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void biBuscar_Click(object sender, EventArgs e)
	{
		lblColumna.Text = "CODIGO";
		lblProperty.Text = "codPersonalizado";
		if (!expandablePanel1.Expanded)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
		else
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void btnclose_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
	}

	private void dgvMovimientosCajaChica_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (rgvMovimientosCajaChica.Columns[e.ColumnIndex].Index > 0)
		{
			lblColumna.Text = rgvMovimientosCajaChica.Columns[e.ColumnIndex].HeaderText;
			lblProperty.Text = rgvMovimientosCajaChica.Columns[e.ColumnIndex].FieldName;
			if (expandablePanel1.Expanded)
			{
				txtFiltro.Focus();
			}
		}
	}

	private void biAperturaCajachica_Click(object sender, EventArgs e)
	{
		if (Saldo == 0m)
		{
			if (Application.OpenForms["frmCajaChicaRegistro"] != null)
			{
				Application.OpenForms["frmCajaChicaRegistro"].Activate();
				return;
			}
			frmCajaChicaRegistro form = new frmCajaChicaRegistro();
			form.Tipo = 1;
			form.Proceso = 1;
			form.AperturaCaja = 1;
			form.ShowDialog();
			ListaCajaChicaDiaria();
			VerificaSaldoCajaVentas();
		}
	}

	private void cboMovimientos_SelectedValueChanged(object sender, EventArgs e)
	{
	}

	private void biRendicionLiquidada_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRendicionesVigentes"] != null)
		{
			Application.OpenForms["frmRendicionesVigentes"].Activate();
			return;
		}
		frmRendicionesVigentes frm = new frmRendicionesVigentes();
		frm.ShowDialog();
		VerificaSaldoCajaVentas();
		ListaCajaChicaDiaria();
	}

	private void frmCajaVentasMovimientos_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		cboMovimientos.SelectedIndex = 0;
		ListaCajaChicaDiaria();
		VerificaSaldoCajaVentas();
		VerificaSaldoCajaVentas1();
		clsAdmAcceso AdmAcce = new clsAdmAcceso();
		int permiso = new clsAdmFormulario().getPermisoPasarPendiente();
		List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
		bool band = accesos.Contains(permiso) || frmLogin.iNivelUser == 1;
		buttonItem2.Visible = band;
		rgvMovimientosCajaChica.ClearSelection();
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void VerificaCajaSeparacion()
	{
		double saldoseparacion = AdmSepa.CargarTotalSeparacion(Convert.ToInt32(cmbAlmacenes.SelectedValue));
		lblCajaSeparacion.Text = $"{saldoseparacion:#,##0.00}";
	}

	private void frmCajaVentasMovimientos_Shown(object sender, EventArgs e)
	{
		rgvMovimientosCajaChica.ClearSelection();
	}

	private void btnCierreyArqueoCajaVentas_Click(object sender, EventArgs e)
	{
		VerificaSaldoCajaVentas();
		ListaCajaChicaDiaria();
		if (Convert.ToDecimal(lblPendiente.Text) > 0m)
		{
			MessageBox.Show("Existen pagos pendientes, por favor verificar listado de Caja/Ventas", "Caja Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (Convert.ToInt32(cmbAlmacenes.SelectedValue) != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Desea cerrar caja de: " + cmbAlmacenes.SelectedItem.ToString() + "?", "Caja Chica", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult == DialogResult.No)
			{
				return;
			}
			if (Convert.ToInt32(cmbAlmacenes.SelectedValue) == frmLogin.iCodAlmacen)
			{
				int cont = 0;
				foreach (RadListDataItem item in cmbAlmacenes.Items)
				{
					if (Convert.ToInt32(item.Value) != frmLogin.iCodAlmacen && Convert.ToInt32(item.Value) != 0)
					{
						clsCaja aux = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, dtpfecha1.Value.Date, 1, Convert.ToInt32(item.Value), frmLogin.iCodUser);
						if (aux != null)
						{
							cont++;
						}
					}
				}
				if (cont > 0)
				{
					MessageBox.Show("Hay una segunda caja abierta, proceda a cerrarla para poder continuar", "Caja Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
			}
			if (AdmCaja.CerrarCajaVentas(Caja.Codsucursal, dtpfecha1.Value.Date, Caja.Codcaja, Convert.ToInt32(cmbAlmacenes.SelectedValue)))
			{
				MessageBox.Show("El cierre de caja se ha realizado correctamente", "Caja Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				clsReporteCaja dso = new clsReporteCaja();
				CRCierre rpt = new CRCierre();
				frmRptCaja frm = new frmRptCaja();
				rpt.SetDataSource(dso.RptMuestraCierreCaja(Caja.Codsucursal, dtpfecha1.Value.Date, Caja.Codcaja, Convert.ToInt32(cmbAlmacenes.SelectedValue)).Tables[0]);
				frm.crvKardex.ReportSource = rpt;
				frm.Show();
				Saldo = default(decimal);
				lblIngresos.Text = "0.00";
				lblEgresos.Text = "0.00";
				lblAperturaCaja.Text = "0.00";
				lblSaldoCaja.Text = "0.00";
				biIngreso.Enabled = false;
				biEgreso.Enabled = false;
				ListaCajaChicaDiaria();
			}
			else
			{
				MessageBox.Show("No se puede cerrar caja ", "Caja Chica", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else if (Convert.ToInt32(cmbAlmacenes.SelectedValue) == 0)
		{
			MessageBox.Show("Seleccione una caja para cerrar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private bool procedimientoVerificacionAntesCerrarCaja(clsCaja caja, int codAlmacen, DateTime fechaCaja)
	{
		try
		{
			bool todoCorrecto = true;
			clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();
			double totalIngresos = Convert.ToDouble(lblIngresos.Text ?? "0");
			double totalVentasCredito = Convert.ToDouble(lblVC.Text ?? "0");
			DataTable listadoVentas = AdmVenta.Ventas(codAlmacen, fechaCaja, fechaCaja, frmLogin.iCodSucursal, 0);
			double totalVentas = 0.0;
			foreach (DataRow row in listadoVentas.Rows)
			{
				if (!(Convert.ToString(row.Field<object>("anulado")) == "ANULADO"))
				{
					totalVentas += Convert.ToDouble(row.Field<object>("total"));
				}
			}
			todoCorrecto = totalIngresos == totalVentas - totalVentasCredito;
			if (!todoCorrecto)
			{
				DataTable data = obtenerFacturasQueEstanEnVentasYNOEnCajaMovimientos(caja, fechaCaja, codAlmacen);
				string ventas = "";
				if (data.Rows.Count > 0)
				{
					foreach (DataRow fila in data.Rows)
					{
						ventas = ventas + "\n" + fila.Field<object>("numdoc").ToString() + " - " + fila.Field<object>("formapago").ToString() + " - " + fila.Field<object>("total").ToString() + " - " + fila.Field<object>("descripAnulado").ToString();
					}
					ventas = "Las siguientes ventas no se encuentran en caja movimientos:" + ventas;
				}
				else
				{
					ventas = "NO SE ENCONTRARON VENTAS QUE NO ESTEN EN CAJA MOVIMIENTOS";
				}
				MessageBox.Show("No Coincide las cantidades a tener en cuenta en la siguiente formula:\nTotal Ingresos (" + $"{totalIngresos:#,##0.00}" + ") = Total Ventas (" + $"{totalVentas:#,##0.00}" + ") - Total Ventas Credito (" + $"{totalVentasCredito:#,##0.00}" + ")\n" + ventas, "Error En Verificacion de Cierre Caja", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return todoCorrecto;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ocurrio el siguiente error: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
	}

	private DataTable obtenerFacturasQueEstanEnVentasYNOEnCajaMovimientos(clsCaja caja, DateTime fechaCaja, int codAlmacen)
	{
		DataTable data = AdmCaja.getVentasNoEstanEnCajaMovimientos(fechaCaja, codAlmacen, frmLogin.iCodSucursal, (codAlmacen != 0) ? caja.Codcaja : 0);
		return (data == null) ? new DataTable() : data;
	}

	private void btnDetalleCajaVentas_Click(object sender, EventArgs e)
	{
		try
		{
			clsReporteCaja ds = new clsReporteCaja();
			CRDetalleCaja rpt = new CRDetalleCaja();
			frmRptCaja frm = new frmRptCaja();
			rpt.SetDataSource(ds.ReporteMovimientosCajaVentas(frmLogin.iCodSucursal, dtpfecha1.Value, Caja.Codcaja, Convert.ToInt32(cmbAlmacenes.SelectedValue)));
			frm.crvKardex.ReportSource = rpt;
			frm.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void radButton1_Click(object sender, EventArgs e)
	{
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
	{
		VerificaSaldoCajaVentas();
		ListaCajaChicaDiaria();
	}

	private void label19_Click(object sender, EventArgs e)
	{
	}

	private void label18_Click(object sender, EventArgs e)
	{
	}

	private void label3_Click(object sender, EventArgs e)
	{
	}

	private void btnVerificacionCaja_Click(object sender, EventArgs e)
	{
		if (procedimientoVerificacionAntesCerrarCaja(Caja, Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpfecha1.Value.Date))
		{
			MessageBox.Show("Las Cantidades para el cierre de caja coinciden", "Verificacion Completa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void buttonItem2_Click(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(rgvMovimientosCajaChica.CurrentRow.Cells["PAGOCAJA"].Value) != "PENDIENTE")
			{
				usuario_click = null;
				frmAutorizacion frm = new frmAutorizacion();
				frm.tipoAccion = 2;
				int codPermiso = new clsAdmFormulario().getPermisoPasarPendiente();
				frm.permiso = codPermiso;
				frm.PermitirAdministradores = true;
				frm.tipoVentanaAAsignarUsuario = 10;
				frm.ventanaCajaMovimientos = this;
				DialogResult dr = frm.ShowDialog();
				if (dr == DialogResult.OK)
				{
					Cursor = Cursors.WaitCursor;
					if (rgvMovimientosCajaChica.Rows.Count > 0 && rgvMovimientosCajaChica.CurrentRow.Index != -1)
					{
						if (Convert.ToInt32(rgvMovimientosCajaChica.CurrentRow.Cells["tipo_descripcion_ingreso"].Value) > 0)
						{
							MessageBox.Show("Los ingresos a caja chica no se pueden cambiar a pendiente");
							return;
						}
						if (admpago.AnularPagoPendiente(Convert.ToInt32(rgvMovimientosCajaChica.CurrentRow.Cells["codpago"].Value)))
						{
							MessageBox.Show("Se anulo el pago correctamente, Pago pendiente actualizado!");
							VerificaSaldoCajaVentas();
							ListaCajaChicaDiaria();
						}
						else
						{
							MessageBox.Show("Error al anular pago");
						}
					}
					Cursor = Cursors.Default;
				}
				else
				{
					MessageBox.Show("Acceso incorrecto");
				}
			}
			else
			{
				MessageBox.Show("Pago ya se Encuentra con estado Pendiente", "PAGO PENDIENTE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void buttonItem3_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			DateTime fecha_reporte = Convert.ToDateTime(dtpfecha1.Value).Date;
			string fecha_reporte2 = fecha_reporte.Day.ToString().PadLeft(2, '0') + "-" + fecha_reporte.Month.ToString().PadLeft(2, '0') + "-" + fecha_reporte.Year;
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.FileName = "Venta_diaria_" + fecha_reporte2;
			sfd.Filter = "Pdf File |*.pdf";
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				Document doc = new Document(PageSize.A4.Rotate(), 0f, 0f, 10f, 0f);
				PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
				doc.Open();
				PdfPTable tab = new PdfPTable(10)
				{
					WidthPercentage = 100f
				};
				iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9f);
				PdfPCell cell = new PdfPCell(new Phrase("REPORTE DE VENTAS DEL DIA " + fecha_reporte2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f)))
				{
					GrayFill = 0.95f
				};
				cell.Colspan = 10;
				cell.HorizontalAlignment = 1;
				cell.BorderColor = new BaseColor(Color.AliceBlue);
				cell.Border = 1;
				cell.BorderWidthBottom = 3f;
				tab.AddCell(cell);
				tab.SetWidths(new float[10] { 4f, 4f, 5f, 6f, 5f, 2f, 9f, 3f, 3f, 3f });
				DateTime aux_fecha = dtpfecha1.Value;
				clsReporteVentas rv = new clsReporteVentas();
				clsReporteNotaCredito rnc = new clsReporteNotaCredito();
				DataTable aux_ventas = rv.ReporteDiarioPDF(aux_fecha, frmLogin.iCodAlmacen);
				int contador = 1;
				double totalVentas = 0.0;
				foreach (DataRow fila in aux_ventas.Rows)
				{
					if (Convert.ToString(fila.ItemArray[12]) != "ANULADO")
					{
						totalVentas += Convert.ToDouble(fila.ItemArray[8] ?? ((object)0.0));
					}
				}
				tab.AddCell(new PdfPCell(new Phrase("TOTAL VENTAS:  "))
				{
					VerticalAlignment = 5,
					HorizontalAlignment = 2,
					Colspan = 9,
					GrayFill = 0.95f
				});
				tab.AddCell(new PdfPCell(new Phrase("S/ " + $"{totalVentas:#,##0.00}"))
				{
					GrayFill = 0.95f
				});
				cell = new PdfPCell();
				cell.Colspan = 10;
				tab.AddCell(cell);
				tab.AddCell(new PdfPCell(new Phrase("NOTAS DE CREDITO"))
				{
					VerticalAlignment = 5,
					HorizontalAlignment = 0,
					Colspan = 10,
					GrayFill = 0.95f
				});
				tab.AddCell("Fecha NC");
				tab.AddCell("N° NC");
				tab.AddCell("F. Doc. REF.");
				tab.AddCell("DOC. REF");
				tab.AddCell("Tipo NC");
				tab.AddCell("Cant.");
				tab.AddCell("Descripción");
				tab.AddCell("P.U");
				tab.AddCell("Sub Total");
				tab.AddCell("Total");
				DataTable aux_notas_creditos = rnc.ReporteNotaCreditoDiaria(aux_fecha, frmLogin.iCodAlmacen);
				if (aux_notas_creditos.Rows.Count > 0)
				{
					foreach (DataRow fila_nc in aux_notas_creditos.Rows)
					{
						DateTime aux_date_1 = Convert.ToDateTime(fila_nc.ItemArray[0].ToString()).Date;
						string fecha_1 = aux_date_1.Day.ToString().PadLeft(2, '0') + "-" + aux_date_1.Month.ToString().PadLeft(2, '0') + "-" + aux_date_1.Year;
						DateTime aux_date = Convert.ToDateTime(fila_nc.ItemArray[2].ToString()).Date;
						string fecha = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
						tab.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(fecha_1).ToShortDateString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[1].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(fecha).ToShortDateString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[3].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[4].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_nc.ItemArray[5] ?? ((object)0.0)):#,##0.00}", font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[6].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_nc.ItemArray[7] ?? ((object)0.0)):#,##0.00}", font)));
						tab.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_nc.ItemArray[8] ?? ((object)0.0)):#,##0.00}", font)));
						tab.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_nc.ItemArray[9] ?? ((object)0.0)):#,##0.00}", font)));
					}
				}
				PdfPTable tab2 = new PdfPTable(7);
				tab2.SpacingBefore = 10f;
				tab2.WidthPercentage = 80f;
				tab2.SetWidths(new float[7] { 6f, 4f, 10f, 4f, 4f, 4f, 4f });
				tab2.HorizontalAlignment = 2;
				tab2.AddCell(new PdfPCell(new Phrase("1", font)));
				tab2.AddCell(new PdfPCell(new Phrase("", font)));
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL VENTAS"))
				{
					GrayFill = 0.95f
				});
				DataTable aux_ventas_agrup = rv.ReporteDiarioAgrupadosTotal(aux_fecha, frmLogin.iCodAlmacen);
				DataTable almacenes = rv.AlmacenXUbicacion(frmLogin.iCodAlmacen);
				for (int i = 0; i < almacenes.Rows.Count; i++)
				{
					tab2.AddCell(new PdfPCell(new Phrase(almacenes.Rows[i].ItemArray[1].ToString()))
					{
						GrayFill = 0.95f
					});
				}
				int idalmacen1 = Convert.ToInt32(almacenes.Rows[0].ItemArray[0]);
				int idalmacen2 = Convert.ToInt32(almacenes.Rows[1].ItemArray[0]);
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				double sumatoria_ventas_alm1 = 0.0;
				double sumatoria_ventas_alm2 = 0.0;
				double sumatoria_ventas_total = 0.0;
				foreach (DataRow fila_v in aux_ventas_agrup.Rows)
				{
					if (!(fila_v.ItemArray[11].ToString() == "ANULADO"))
					{
						if (Convert.ToInt32(fila_v.ItemArray[10] ?? ((object)0)) == idalmacen1)
						{
							sumatoria_ventas_alm1 += Convert.ToDouble(fila_v.ItemArray[8] ?? ((object)0));
						}
						else
						{
							sumatoria_ventas_alm2 += Convert.ToDouble(fila_v.ItemArray[8] ?? ((object)0));
						}
					}
				}
				sumatoria_ventas_total = Convert.ToDouble(sumatoria_ventas_alm1) + Convert.ToDouble(sumatoria_ventas_alm2);
				tab2.AddCell(new PdfPCell(new Phrase("", font)));
				tab2.AddCell(new PdfPCell(new Phrase("", font)));
				tab2.AddCell(new PdfPCell(new Phrase("", font)));
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sumatoria_ventas_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sumatoria_ventas_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{sumatoria_ventas_total:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				double _sumatoria_ventas_pen_alm1 = 0.0;
				double _sumatoria_ventas_pen_alm2 = 0.0;
				double _sumatoria_ventas_total_pendiente = 0.0;
				foreach (GridViewRowInfo _row in rgvMovimientosCajaChica.Rows)
				{
					double total1 = 0.0;
					double total2 = 0.0;
					if (_row.Cells["PAGOCAJA"].Value.ToString() == "PENDIENTE")
					{
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase(_row.Cells["DOCREFERENCIA"].Value.ToString(), font)));
						if (Convert.ToInt32(_row.Cells["codSucursal"].Value) == idalmacen1)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + string.Format("{0:#,##0.00}", Convert.ToDouble(_row.Cells["monto"].Value.ToString())), font))
							{
								HorizontalAlignment = 2
							});
							_sumatoria_ventas_pen_alm1 += Convert.ToDouble(_row.Cells["monto"].Value);
							total1 = Convert.ToDouble(_row.Cells["monto"].Value);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						if (Convert.ToInt32(_row.Cells["codSucursal"].Value) == idalmacen2)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + string.Format("{0:#,##0.00}", Convert.ToDouble(_row.Cells["monto"].Value.ToString())), font))
							{
								HorizontalAlignment = 2
							});
							_sumatoria_ventas_pen_alm2 += Convert.ToDouble(_row.Cells["monto"].Value);
							total1 = Convert.ToDouble(_row.Cells["monto"].Value);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total1 + total2:#,##0.00}", font))
						{
							HorizontalAlignment = 2
						});
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
					}
				}
				_sumatoria_ventas_total_pendiente = Convert.ToDouble(_sumatoria_ventas_pen_alm1) + Convert.ToDouble(_sumatoria_ventas_pen_alm2);
				tab2.AddCell(new PdfPCell(new Phrase("2", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL PENDIENTES", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(_sumatoria_ventas_pen_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(_sumatoria_ventas_pen_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{_sumatoria_ventas_total_pendiente:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font)));
				DataTable pagos_tarjeta = rv.ReporteDiarioAgrupadoTotalPagos(aux_fecha, frmLogin.iCodAlmacen, 8);
				double sum_pagos_tarjeta_alm1 = 0.0;
				double sum_pagos_tarjeta_alm2 = 0.0;
				double pagos_tarjeta_total = 0.0;
				if (pagos_tarjeta.Rows.Count > 0)
				{
					foreach (DataRow fila_v2 in pagos_tarjeta.Rows)
					{
						double total3 = 0.0;
						double total4 = 0.0;
						if (!(fila_v2.ItemArray[12].ToString() == "ANULADO"))
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
							tab2.AddCell(new PdfPCell(new Phrase(fila_v2.ItemArray[2].ToString(), font)));
							if (Convert.ToInt32(fila_v2.ItemArray[10] ?? ((object)0)) == idalmacen1)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v2.ItemArray[11] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_pagos_tarjeta_alm1 += Convert.ToDouble(fila_v2.ItemArray[11]);
								total3 = Convert.ToDouble(fila_v2.ItemArray[11]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							if (Convert.ToInt32(fila_v2.ItemArray[10] ?? ((object)0)) == idalmacen2)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v2.ItemArray[11] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_pagos_tarjeta_alm2 += Convert.ToDouble(fila_v2.ItemArray[11]);
								total3 = Convert.ToDouble(fila_v2.ItemArray[11]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total3 + total4:#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{(total3 + total4) / Convert.ToDouble(0.96):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
						}
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				pagos_tarjeta_total = sum_pagos_tarjeta_alm1 + sum_pagos_tarjeta_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("3", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL VENTAS CON TARJETA", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_pagos_tarjeta_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_pagos_tarjeta_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{pagos_tarjeta_total:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{pagos_tarjeta_total / 0.96:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				DataTable pagos_transferencia = rv.ReporteDiarioAgrupadoTotalPagos(aux_fecha, frmLogin.iCodAlmacen, 9);
				double sum_pagos_transfe_alm1 = 0.0;
				double sum_pagos_transfe_alm2 = 0.0;
				double pagos_transfe_total = 0.0;
				if (pagos_transferencia.Rows.Count > 0)
				{
					foreach (DataRow fila_v3 in pagos_transferencia.Rows)
					{
						double total5 = 0.0;
						double total6 = 0.0;
						if (!(fila_v3.ItemArray[12].ToString() == "ANULADO"))
						{
							tab2.AddCell(new PdfPCell(new Phrase(fila_v3.ItemArray[14].ToString(), font)));
							tab2.AddCell(new PdfPCell(new Phrase(fila_v3.ItemArray[13].ToString(), font)));
							tab2.AddCell(new PdfPCell(new Phrase(fila_v3.ItemArray[2].ToString(), font)));
							if (Convert.ToInt32(fila_v3.ItemArray[10] ?? ((object)0)) == idalmacen1)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v3.ItemArray[11] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_pagos_transfe_alm1 += Convert.ToDouble(fila_v3.ItemArray[11]);
								total5 = Convert.ToDouble(fila_v3.ItemArray[11]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							if (Convert.ToInt32(fila_v3.ItemArray[10] ?? ((object)0)) == idalmacen2)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v3.ItemArray[11] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_pagos_transfe_alm2 += Convert.ToDouble(fila_v3.ItemArray[11]);
								total5 = Convert.ToDouble(fila_v3.ItemArray[11]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total5 + total6:#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							tab2.AddCell(new PdfPCell(new Phrase("")));
						}
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase(""))
					{
						GrayFill = 0.95f
					});
					tab2.AddCell(new PdfPCell(new Phrase(""))
					{
						GrayFill = 0.95f
					});
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5,
						GrayFill = 0.95f
					});
				}
				pagos_transfe_total = sum_pagos_transfe_alm1 + sum_pagos_transfe_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("4", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL VENTAS CON TRANSFERENCIA", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_pagos_transfe_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_pagos_transfe_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{pagos_transfe_total:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				DataTable pagos_deposito = rv.ReporteDiarioAgrupadoTotalPagos(aux_fecha, frmLogin.iCodAlmacen, 6);
				double sum_pagos_deposito_alm1 = 0.0;
				double sum_pagos_deposito_alm2 = 0.0;
				double pagos_deposito_total = 0.0;
				if (pagos_deposito.Rows.Count > 0)
				{
					foreach (DataRow fila_v4 in pagos_deposito.Rows)
					{
						double total7 = 0.0;
						double total8 = 0.0;
						if (!(fila_v4.ItemArray[12].ToString() == "ANULADO"))
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
							tab2.AddCell(new PdfPCell(new Phrase(fila_v4.ItemArray[2].ToString(), font)));
							if (Convert.ToInt32(fila_v4.ItemArray[10] ?? ((object)0)) == idalmacen1)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v4.ItemArray[11] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_pagos_deposito_alm1 += Convert.ToDouble(fila_v4.ItemArray[11]);
								total7 = Convert.ToDouble(fila_v4.ItemArray[11]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							if (Convert.ToInt32(fila_v4.ItemArray[10] ?? ((object)0)) == idalmacen2)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v4.ItemArray[11] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_pagos_deposito_alm2 += Convert.ToDouble(fila_v4.ItemArray[11]);
								total7 = Convert.ToDouble(fila_v4.ItemArray[11]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total7 + total8:#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							tab2.AddCell(new PdfPCell(new Phrase("")));
						}
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				pagos_deposito_total = sum_pagos_deposito_alm1 + sum_pagos_deposito_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("5", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL DESCUENTOS TRANSPORTISTA", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_pagos_deposito_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_pagos_deposito_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{pagos_deposito_total:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				DataTable ventas_credito = rv.ReporteDiarioVentasCredito(aux_fecha, frmLogin.iCodAlmacen);
				double sum_venta_credito_alm1 = 0.0;
				double sum_venta_credito_alm2 = 0.0;
				double venta_credito_total = 0.0;
				if (ventas_credito.Rows.Count > 0)
				{
					foreach (DataRow fila_v5 in ventas_credito.Rows)
					{
						double total9 = 0.0;
						double total10 = 0.0;
						if (!(fila_v5.ItemArray[14].ToString() == "ANULADO"))
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
							tab2.AddCell(new PdfPCell(new Phrase(fila_v5.ItemArray[2].ToString(), font)));
							if (Convert.ToInt32(fila_v5.ItemArray[10] ?? ((object)0)) == idalmacen1)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v5.ItemArray[8] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_venta_credito_alm1 += Convert.ToDouble(fila_v5.ItemArray[8]);
								total9 = Convert.ToDouble(fila_v5.ItemArray[8]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							if (Convert.ToInt32(fila_v5.ItemArray[10] ?? ((object)0)) == idalmacen2)
							{
								tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v5.ItemArray[8] ?? ((object)0)):#,##0.00}", font))
								{
									HorizontalAlignment = 2
								});
								sum_venta_credito_alm2 += Convert.ToDouble(fila_v5.ItemArray[8]);
								total9 = Convert.ToDouble(fila_v5.ItemArray[8]);
							}
							else
							{
								tab2.AddCell(new PdfPCell(new Phrase("", font)));
							}
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total9 + total10:#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							tab2.AddCell(new PdfPCell(new Phrase("")));
						}
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				venta_credito_total = sum_venta_credito_alm1 + sum_venta_credito_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("6", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL DESCUENTOS A CREDITO", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_venta_credito_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_venta_credito_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{venta_credito_total:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				double total_venta_contado1 = Convert.ToDouble(sumatoria_ventas_alm1) - Convert.ToDouble(sum_venta_credito_alm1);
				double total_venta_contado2 = Convert.ToDouble(sumatoria_ventas_alm2) - Convert.ToDouble(sum_venta_credito_alm2);
				double total_venta_contado3 = total_venta_contado1 + total_venta_contado2;
				tab2.AddCell(new PdfPCell(new Phrase("7", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL V. CONTADO (1-6)", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(total_venta_contado1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(total_venta_contado2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_venta_contado3:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				DataTable egresos_caja_chica = rv.ListaCajaEgresos(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
				double sum_egresos_caja_alm1 = 0.0;
				double sum_egresos_caja_alm2 = 0.0;
				double total_egresos_caja = 0.0;
				if (egresos_caja_chica.Rows.Count > 0)
				{
					foreach (DataRow fila_v6 in egresos_caja_chica.Rows)
					{
						double total11 = 0.0;
						double total12 = 0.0;
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase(fila_v6.ItemArray[6].ToString() + " - " + fila_v6.ItemArray[5].ToString(), font)));
						if (Convert.ToInt32(fila_v6.ItemArray[22] ?? ((object)0)) == idalmacen1)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v6.ItemArray[7] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_egresos_caja_alm1 += Convert.ToDouble(fila_v6.ItemArray[7]);
							total11 = Convert.ToDouble(fila_v6.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						if (Convert.ToInt32(fila_v6.ItemArray[22] ?? ((object)0)) == idalmacen2)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v6.ItemArray[7] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_egresos_caja_alm2 += Convert.ToDouble(fila_v6.ItemArray[7]);
							total11 = Convert.ToDouble(fila_v6.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total11 + total12:#,##0.00}", font))
						{
							HorizontalAlignment = 2
						});
						tab2.AddCell(new PdfPCell(new Phrase("")));
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				total_egresos_caja = sum_egresos_caja_alm1 + sum_egresos_caja_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("8", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL EGRESOS CAJA", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_egresos_caja_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_egresos_caja_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_egresos_caja:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				DataTable notas_credito = rv.ListaNotasCredito(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
				double sum_nc_cobradas_alm1 = 0.0;
				double sum_nc_cobradas_alm2 = 0.0;
				double total_nc_cobradas = 0.0;
				if (notas_credito.Rows.Count > 0)
				{
					foreach (DataRow fila_v7 in notas_credito.Rows)
					{
						double total13 = 0.0;
						double total14 = 0.0;
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase(fila_v7.ItemArray[15].ToString() + " - " + fila_v7.ItemArray[16].ToString(), font)));
						if (Convert.ToInt32(fila_v7.ItemArray[21] ?? ((object)0)) == idalmacen1)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v7.ItemArray[6] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_nc_cobradas_alm1 += Convert.ToDouble(fila_v7.ItemArray[6]);
							total13 = Convert.ToDouble(fila_v7.ItemArray[6]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						if (Convert.ToInt32(fila_v7.ItemArray[21] ?? ((object)0)) == idalmacen2)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v7.ItemArray[6] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_nc_cobradas_alm2 += Convert.ToDouble(fila_v7.ItemArray[6]);
							total13 = Convert.ToDouble(fila_v7.ItemArray[6]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total13 + total14:#,##0.00}", font))
						{
							HorizontalAlignment = 2
						});
						tab2.AddCell(new PdfPCell(new Phrase("")));
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				total_nc_cobradas = sum_nc_cobradas_alm1 + sum_nc_cobradas_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("9", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("NOTAS DE CREDITO COBRADAS", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_nc_cobradas_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_nc_cobradas_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_nc_cobradas:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				DataTable ingresos_caja_chica = rv.ListaCajaIngresos(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
				double sum_ingreso_caja_alm1 = 0.0;
				double sum_ingreso_caja_alm2 = 0.0;
				double total_ingreso_caja = 0.0;
				if (ingresos_caja_chica.Rows.Count > 0)
				{
					foreach (DataRow fila_v8 in ingresos_caja_chica.Rows)
					{
						double total15 = 0.0;
						double total16 = 0.0;
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase(fila_v8.ItemArray[6].ToString() + " - " + fila_v8.ItemArray[5].ToString(), font)));
						if (Convert.ToInt32(fila_v8.ItemArray[22] ?? ((object)0)) == idalmacen1)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v8.ItemArray[7] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_ingreso_caja_alm1 += Convert.ToDouble(fila_v8.ItemArray[7]);
							total15 = Convert.ToDouble(fila_v8.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						if (Convert.ToInt32(fila_v8.ItemArray[22] ?? ((object)0)) == idalmacen2)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v8.ItemArray[7] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_ingreso_caja_alm2 += Convert.ToDouble(fila_v8.ItemArray[7]);
							total15 = Convert.ToDouble(fila_v8.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total15 + total16:#,##0.00}", font))
						{
							HorizontalAlignment = 2
						});
						tab2.AddCell(new PdfPCell(new Phrase("")));
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				total_ingreso_caja = sum_ingreso_caja_alm1 + sum_ingreso_caja_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("10", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL INGRESOS EFECTIVO", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_ingreso_caja_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_ingreso_caja_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_ingreso_caja:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				double total_depositar1 = total_venta_contado1 - _sumatoria_ventas_pen_alm1 - sum_pagos_tarjeta_alm1 - sum_pagos_transfe_alm1 - sum_pagos_deposito_alm1 - sum_egresos_caja_alm1 - sum_nc_cobradas_alm1 + sum_ingreso_caja_alm1;
				double total_depositar2 = total_venta_contado2 - _sumatoria_ventas_pen_alm2 - sum_pagos_tarjeta_alm2 - sum_pagos_transfe_alm2 - sum_pagos_deposito_alm2 - sum_egresos_caja_alm2 - sum_nc_cobradas_alm2 + sum_ingreso_caja_alm2;
				double total_venta_depositar = total_depositar1 + total_depositar2;
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL A DEPOSITAR (7-2-3-4-5-8-9+10)", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(total_depositar1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(total_depositar2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_venta_depositar:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					VerticalAlignment = 5,
					HorizontalAlignment = 0,
					Colspan = 7
				});
				DataTable ingresos_tarjeta = rv.ListaCajaIngresosTarjeta(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
				double sum_ingreso_tarjeta_alm1 = 0.0;
				double sum_ingreso_tarjeta_alm2 = 0.0;
				double total_ingreso_tarjeta = 0.0;
				if (ingresos_tarjeta.Rows.Count > 0)
				{
					foreach (DataRow fila_v9 in ingresos_tarjeta.Rows)
					{
						double total17 = 0.0;
						double total18 = 0.0;
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase(fila_v9.ItemArray[6].ToString() + " - " + fila_v9.ItemArray[5].ToString(), font)));
						if (Convert.ToInt32(fila_v9.ItemArray[22] ?? ((object)0)) == idalmacen1)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v9.ItemArray[7] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_ingreso_tarjeta_alm1 += Convert.ToDouble(fila_v9.ItemArray[7]);
							total17 = Convert.ToDouble(fila_v9.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						if (Convert.ToInt32(fila_v9.ItemArray[22] ?? ((object)0)) == idalmacen2)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v9.ItemArray[6] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_ingreso_tarjeta_alm2 += Convert.ToDouble(fila_v9.ItemArray[7]);
							total17 = Convert.ToDouble(fila_v9.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total17 + total18:#,##0.00}", font))
						{
							HorizontalAlignment = 2
						});
						tab2.AddCell(new PdfPCell(new Phrase("")));
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				total_ingreso_tarjeta = sum_ingreso_tarjeta_alm1 + sum_ingreso_tarjeta_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL INGRESOS TARJETA", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_ingreso_tarjeta_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_ingreso_tarjeta_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_ingreso_tarjeta:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_ingreso_tarjeta / 0.96:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				double total_ventas_tarjetas = pagos_tarjeta_total / 0.96;
				double total_in_tarjetas = total_ingreso_tarjeta / 0.96;
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					Colspan = 5
				});
				tab2.AddCell(new PdfPCell(new Phrase("POS")));
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_ventas_tarjetas + total_in_tarjetas:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					VerticalAlignment = 5,
					HorizontalAlignment = 0,
					Colspan = 7
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					VerticalAlignment = 5,
					HorizontalAlignment = 0,
					Colspan = 7
				});
				DataTable ingresos_transferencia = rv.ListaCajaIngresosTransferencia(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
				double sum_ingreso_transf_alm1 = 0.0;
				double sum_ingreso_transf_alm2 = 0.0;
				double total_ingreso_transf = 0.0;
				if (ingresos_transferencia.Rows.Count > 0)
				{
					foreach (DataRow fila_v10 in ingresos_transferencia.Rows)
					{
						double total19 = 0.0;
						double total20 = 0.0;
						tab2.AddCell(new PdfPCell(new Phrase("", font)));
						tab2.AddCell(new PdfPCell(new Phrase(fila_v10.ItemArray[23].ToString(), font)));
						tab2.AddCell(new PdfPCell(new Phrase(fila_v10.ItemArray[6].ToString() + " - " + fila_v10.ItemArray[5].ToString(), font)));
						if (Convert.ToInt32(fila_v10.ItemArray[22] ?? ((object)0)) == idalmacen1)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v10.ItemArray[7] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_ingreso_transf_alm1 += Convert.ToDouble(fila_v10.ItemArray[7]);
							total19 = Convert.ToDouble(fila_v10.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						if (Convert.ToInt32(fila_v10.ItemArray[22] ?? ((object)0)) == idalmacen2)
						{
							tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(fila_v10.ItemArray[7] ?? ((object)0)):#,##0.00}", font))
							{
								HorizontalAlignment = 2
							});
							sum_ingreso_transf_alm2 += Convert.ToDouble(fila_v10.ItemArray[7]);
							total19 = Convert.ToDouble(fila_v10.ItemArray[7]);
						}
						else
						{
							tab2.AddCell(new PdfPCell(new Phrase("", font)));
						}
						tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total19 + total20:#,##0.00}", font))
						{
							HorizontalAlignment = 2
						});
						tab2.AddCell(new PdfPCell(new Phrase("")));
					}
				}
				else
				{
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("")));
					tab2.AddCell(new PdfPCell(new Phrase("NO APLICA"))
					{
						VerticalAlignment = 5,
						HorizontalAlignment = 0,
						Colspan = 5
					});
				}
				total_ingreso_transf = sum_ingreso_transf_alm1 + sum_ingreso_transf_alm2;
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("TOTAL INGRESOS TRANSFERENCIA", font))
				{
					GrayFill = 0.95f
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_ingreso_transf_alm1):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{Convert.ToDouble(sum_ingreso_transf_alm2):#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase("S/ " + $"{total_ingreso_transf:#,##0.00}"))
				{
					GrayFill = 0.95f,
					HorizontalAlignment = 2
				});
				tab2.AddCell(new PdfPCell(new Phrase(""))
				{
					GrayFill = 0.95f
				});
				doc.Add(tab);
				doc.Add(tab2);
				doc.Close();
			}
			success = new Success("Reporte generado correctamente!");
			success.ShowDialog();
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			errors = new Errors("Error al generar reporte");
			MessageBox.Show(ex.Message);
			Cursor = Cursors.Default;
		}
	}

	private void buttonItem4_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			DateTime fecha_reporte = Convert.ToDateTime(dtpfecha1.Value).Date;
			string fecha_reporte2 = fecha_reporte.Day.ToString().PadLeft(2, '0') + "-" + fecha_reporte.Month.ToString().PadLeft(2, '0') + "-" + fecha_reporte.Year;
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.FileName = "Resumen_ventas" + fecha_reporte2;
			sfd.Filter = "Pdf File |*.pdf";
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				Document doc = new Document(PageSize.A4.Rotate(), 0f, 0f, 10f, 0f);
				PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
				doc.Open();
				PdfPTable tab = new PdfPTable(10)
				{
					WidthPercentage = 100f
				};
				iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9f);
				PdfPCell cell = new PdfPCell(new Phrase("REPORTE DE VENTAS DEL DIA " + fecha_reporte2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f)))
				{
					GrayFill = 0.95f
				};
				cell.Colspan = 10;
				cell.HorizontalAlignment = 1;
				cell.BorderColor = new BaseColor(Color.AliceBlue);
				cell.Border = 1;
				cell.BorderWidthBottom = 3f;
				tab.AddCell(cell);
				tab.SetWidths(new float[10] { 4f, 4f, 5f, 9f, 5f, 2f, 9f, 3f, 3f, 3f });
				DateTime aux_fecha = dtpfecha1.Value;
				clsReporteVentas rv = new clsReporteVentas();
				clsReporteNotaCredito rnc = new clsReporteNotaCredito();
				DataTable aux_ventas = rv.ReporteDiarioPDF(aux_fecha, frmLogin.iCodAlmacen);
				tab.AddCell("Fecha");
				tab.AddCell("Documento");
				tab.AddCell("N° Doc");
				tab.AddCell("Cliente");
				tab.AddCell("M. Pago");
				tab.AddCell("Cant.");
				tab.AddCell("Descripción");
				tab.AddCell("P.U");
				tab.AddCell("Sub Total");
				tab.AddCell("Total");
				int contador = 1;
				double totalVentas = 0.0;
				foreach (DataRow fila in aux_ventas.Rows)
				{
					tab.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(fila[0]).ToShortDateString(), font)));
					tab.AddCell(new PdfPCell(new Phrase(fila.ItemArray[1].ToString(), font)));
					tab.AddCell(new PdfPCell(new Phrase(fila.ItemArray[2].ToString(), font)));
					tab.AddCell(new PdfPCell(new Phrase(fila.ItemArray[3].ToString(), font)));
					if (fila.ItemArray[12].ToString() == "ANULADO")
					{
						tab.AddCell(new PdfPCell(new Phrase(fila.ItemArray[12].ToString(), font)));
					}
					else
					{
						tab.AddCell(new PdfPCell(new Phrase(fila.ItemArray[9].ToString(), font)));
					}
					tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila.ItemArray[4] ?? ((object)0.0)):#,##0.00}", font)));
					tab.AddCell(new PdfPCell(new Phrase(fila.ItemArray[5].ToString(), font)));
					tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila.ItemArray[6] ?? ((object)0.0)):#,##0.00}", font)));
					tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila.ItemArray[7] ?? ((object)0.0)):#,##0.00}", font)));
					if (fila.ItemArray[12].ToString() == "ANULADO")
					{
						tab.AddCell($"{Convert.ToDouble(0.0):#,##0.00}");
					}
					else
					{
						tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila.ItemArray[8] ?? ((object)0.0)):#,##0.00}", font)));
					}
					if (Convert.ToString(fila.ItemArray[12]) != "ANULADO")
					{
						totalVentas += Convert.ToDouble(fila.ItemArray[8] ?? ((object)0.0));
					}
				}
				tab.AddCell(new PdfPCell(new Phrase("TOTAL VENTAS:  "))
				{
					VerticalAlignment = 5,
					HorizontalAlignment = 2,
					Colspan = 9,
					GrayFill = 0.95f
				});
				tab.AddCell(new PdfPCell(new Phrase($"{totalVentas:#,##0.00}"))
				{
					GrayFill = 0.95f
				});
				cell = new PdfPCell();
				cell.Colspan = 10;
				tab.AddCell(cell);
				tab.AddCell(new PdfPCell(new Phrase("NOTAS DE CREDITO"))
				{
					VerticalAlignment = 5,
					HorizontalAlignment = 0,
					Colspan = 10,
					GrayFill = 0.95f
				});
				tab.AddCell("Fecha NC");
				tab.AddCell("N° NC");
				tab.AddCell("F. Doc. REF.");
				tab.AddCell("DOC. REF");
				tab.AddCell("Tipo NC");
				tab.AddCell("Cant.");
				tab.AddCell("Descripción");
				tab.AddCell("P.U");
				tab.AddCell("Sub Total");
				tab.AddCell("Total");
				DataTable aux_notas_creditos = rnc.ReporteNotaCreditoDiaria(aux_fecha, frmLogin.iCodAlmacen);
				if (aux_notas_creditos.Rows.Count > 0)
				{
					foreach (DataRow fila_nc in aux_notas_creditos.Rows)
					{
						DateTime aux_date_1 = Convert.ToDateTime(fila_nc.ItemArray[0].ToString()).Date;
						string fecha_1 = aux_date_1.Day.ToString().PadLeft(2, '0') + "-" + aux_date_1.Month.ToString().PadLeft(2, '0') + "-" + aux_date_1.Year;
						DateTime aux_date = Convert.ToDateTime(fila_nc.ItemArray[2].ToString()).Date;
						string fecha = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
						tab.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(fecha_1).ToShortDateString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[1].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(fecha).ToShortDateString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[3].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[4].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila_nc.ItemArray[5] ?? ((object)0.0)):#,##0.00}", font)));
						tab.AddCell(new PdfPCell(new Phrase(fila_nc.ItemArray[6].ToString(), font)));
						tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila_nc.ItemArray[7] ?? ((object)0.0)):#,##0.00}", font)));
						tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila_nc.ItemArray[8] ?? ((object)0.0)):#,##0.00}", font)));
						tab.AddCell(new PdfPCell(new Phrase($"{Convert.ToDouble(fila_nc.ItemArray[9] ?? ((object)0.0)):#,##0.00}", font)));
					}
				}
				doc.Add(tab);
				doc.Close();
			}
			success = new Success("Reporte generado correctamente!");
			success.ShowDialog();
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rgvMovimientosCajaChica_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (rgvMovimientosCajaChica.Rows.Count < 1 || e.RowIndex == -1)
			{
				return;
			}
			GridDataCellElement btn = (GridDataCellElement)sender;
			int cod = Convert.ToInt32(e.Row.Cells["codpago"].Value);
			if (Convert.ToBoolean(rgvMovimientosCajaChica.Rows[e.RowIndex].Cells["CheckBox"].Value))
			{
				if (Convert.ToString(e.Row.Cells["color"].Value) != "")
				{
					listaPagosAgrupados.Add(cod);
				}
				else
				{
					listaPagosAgrupados.Remove(cod);
				}
				rgvMovimientosCajaChica.Rows[e.RowIndex].Cells["CheckBox"].Value = 0;
			}
			else
			{
				listaPagosAgrupados.Add(cod);
				rgvMovimientosCajaChica.Rows[e.RowIndex].Cells["CheckBox"].Value = 1;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void buttonItem7_Click(object sender, EventArgs e)
	{
		try
		{
			if (listaPagosAgrupados.Count >= 1)
			{
				foreach (int codigo in listaPagosAgrupados)
				{
					if (!admpago.ActualizarSeleccion(Convert.ToInt32(codigo), ""))
					{
						MessageBox.Show("Error al desagrupar registros");
					}
				}
			}
			else
			{
				MessageBox.Show("Selecionar 2 o más Items!");
			}
			listaPagosAgrupados.Clear();
			success = new Success("Registros desagrupados");
			success.ShowDialog();
			VerificaSaldoCajaVentas();
			ListaCajaChicaDiaria();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void buttonItem5_Click(object sender, EventArgs e)
	{
		try
		{
			if (listaPagosAgrupados.Count >= 1)
			{
				Random rnd = new Random();
				Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
				foreach (int codigo in listaPagosAgrupados)
				{
					string nuevoColor = randomColor.A + "," + randomColor.R + "," + randomColor.G + "," + randomColor.B;
					if (!admpago.ActualizarSeleccion(Convert.ToInt32(codigo), nuevoColor))
					{
						MessageBox.Show("Error al agrupar registros");
					}
				}
			}
			else
			{
				MessageBox.Show("Selecionar 2 o más Items!");
			}
			listaPagosAgrupados.Clear();
			success = new Success("Registros agrupados");
			success.ShowDialog();
			VerificaSaldoCajaVentas();
			ListaCajaChicaDiaria();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rgvMovimientosCajaChica_CellValuePushed(object sender, GridViewCellValueEventArgs e)
	{
		try
		{
			HandleGridSelectionChanged(sender, e);
		}
		catch (Exception)
		{
		}
	}

	private void HandleGridSelectionChanged(object sender, EventArgs e)
	{
		UnCheckPreviouslySelectedRows();
		CheckCurrentlySelectedRows();
		StoreCurrentlySelectedRows();
	}

	private void UnCheckPreviouslySelectedRows()
	{
		foreach (GridViewRowInfo row in previouslySelectedRows)
		{
			row.Cells["Select"].Value = false;
		}
	}

	private void CheckCurrentlySelectedRows()
	{
		IEnumerable<GridViewCellInfo> cells = Enumerable.Select<GridViewRowInfo, GridViewCellInfo>((IEnumerable<GridViewRowInfo>)rgvMovimientosCajaChica.SelectedRows, (Func<GridViewRowInfo, GridViewCellInfo>)((GridViewRowInfo x) => x.Cells["Select"]));
		foreach (GridViewCellInfo cell in cells)
		{
			cell.Value = true;
		}
	}

	private void StoreCurrentlySelectedRows()
	{
		previouslySelectedRows.Clear();
		previouslySelectedRows.AddRange(rgvMovimientosCajaChica.SelectedRows);
	}

	private void rgvMovimientosCajaChica_CellFormatting(object sender, CellFormattingEventArgs e)
	{
	}

	private void rgvMovimientosCajaChica_RowFormatting(object sender, RowFormattingEventArgs e)
	{
		if (Convert.ToString(e.RowElement.RowInfo.Cells["color"].Value) != "")
		{
			string[] r = e.RowElement.RowInfo.Cells["color"].Value.ToString().Split(',');
			e.RowElement.DrawFill = true;
			e.RowElement.GradientStyle = GradientStyles.Solid;
			e.RowElement.BackColor = Color.FromArgb(Convert.ToInt32(r[0]), Convert.ToInt32(r[1]), Convert.ToInt32(r[2]));
		}
		else
		{
			e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
			e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
			e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCajaVentasMovimientos));
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn33 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.biIngreso = new DevComponents.DotNetBar.ButtonItem();
		this.biEgreso = new DevComponents.DotNetBar.ButtonItem();
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.biRendicionesContables = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.cboMovimientos = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label16 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.dtpfecha1 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.btnExit = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel3 = new System.Windows.Forms.Panel();
		this.lblIngresoTransferencia = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.lblIngreso = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.lblITarjeta = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.btnVerificacionCaja = new DevComponents.DotNetBar.ButtonItem();
		this.btnCierreyArqueoCajaVentas = new DevComponents.DotNetBar.ButtonItem();
		this.btnDetalleCajaVentas = new DevComponents.DotNetBar.ButtonItem();
		this.biRencicionCaja = new DevComponents.DotNetBar.ButtonItem();
		this.biHistorialRendiciones = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
		this.label18 = new System.Windows.Forms.Label();
		this.lblNC = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.lblPendiente = new System.Windows.Forms.Label();
		this.lblVC = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.lblCajaSeparacion = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.lbCheque = new System.Windows.Forms.Label();
		this.lbDeposito = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.lblSaldoCaja = new System.Windows.Forms.Label();
		this.lblAperturaCaja = new System.Windows.Forms.Label();
		this.lblEgresos = new System.Windows.Forms.Label();
		this.lblIngresos = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.lblProperty = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.lblColumna = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnclose = new System.Windows.Forms.Button();
		this.biVerificarRendicion = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.cachedCRCuotasPrestamo1 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvMovimientosCajaChica = new Telerik.WinControls.UI.RadGridView();
		this.label11 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		this.panel3.SuspendLayout();
		this.expandablePanel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvMovimientosCajaChica).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvMovimientosCajaChica.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar1.DragDropSupport = true;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[13]
		{
			this.biIngreso, this.biEgreso, this.biEditar, this.biEliminar, this.biBuscar, this.biImprimir, this.biRendicionesContables, this.buttonItem2, this.buttonItem3, this.buttonItem4,
			this.buttonItem5, this.buttonItem7, this.biActualizar
		});
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(703, 65);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 8;
		this.ribbonBar1.Text = "ribbonBar1";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
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
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList1.Images.SetKeyName(19, "egreso.png");
		this.imageList1.Images.SetKeyName(20, "ingreso.png");
		this.imageList1.Images.SetKeyName(21, "icon_shelfs.png");
		this.imageList1.Images.SetKeyName(22, "cierre.png");
		this.biIngreso.ImageIndex = 20;
		this.biIngreso.ImagePaddingHorizontal = 20;
		this.biIngreso.ImagePaddingVertical = 15;
		this.biIngreso.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biIngreso.Name = "biIngreso";
		this.biIngreso.SubItemsExpandWidth = 14;
		this.biIngreso.Text = "Ingreso";
		this.biIngreso.Click += new System.EventHandler(biIngreso_Click);
		this.biEgreso.ImageIndex = 19;
		this.biEgreso.ImagePaddingHorizontal = 20;
		this.biEgreso.ImagePaddingVertical = 15;
		this.biEgreso.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEgreso.Name = "biEgreso";
		this.biEgreso.SubItemsExpandWidth = 14;
		this.biEgreso.Text = "Egreso";
		this.biEgreso.Click += new System.EventHandler(biEgreso_Click);
		this.biEditar.Enabled = false;
		this.biEditar.ImageIndex = 3;
		this.biEditar.ImagePaddingHorizontal = 10;
		this.biEditar.ImagePaddingVertical = 15;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Editar";
		this.biEditar.Visible = false;
		this.biEliminar.ImageIndex = 21;
		this.biEliminar.ImagePaddingHorizontal = 10;
		this.biEliminar.ImagePaddingVertical = 15;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Click += new System.EventHandler(biEliminar_Click);
		this.biBuscar.Enabled = false;
		this.biBuscar.ImageIndex = 11;
		this.biBuscar.ImagePaddingHorizontal = 10;
		this.biBuscar.ImagePaddingVertical = 15;
		this.biBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscar.Name = "biBuscar";
		this.biBuscar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlB);
		this.biBuscar.SubItemsExpandWidth = 14;
		this.biBuscar.Text = "Buscar";
		this.biBuscar.Visible = false;
		this.biBuscar.Click += new System.EventHandler(biBuscar_Click);
		this.biImprimir.Image = SIGEFA.Properties.Resources.searcg;
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingHorizontal = 10;
		this.biImprimir.ImagePaddingVertical = 15;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Visible = false;
		this.biRendicionesContables.Enabled = false;
		this.biRendicionesContables.Image = (System.Drawing.Image)resources.GetObject("biRendicionesContables.Image");
		this.biRendicionesContables.ImageIndex = 7;
		this.biRendicionesContables.ImagePaddingHorizontal = 10;
		this.biRendicionesContables.ImagePaddingVertical = 15;
		this.biRendicionesContables.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRendicionesContables.Name = "biRendicionesContables";
		this.biRendicionesContables.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biRendicionesContables.SubItemsExpandWidth = 14;
		this.biRendicionesContables.Text = "Rendiociones";
		this.biRendicionesContables.Visible = false;
		this.biRendicionesContables.Click += new System.EventHandler(biRendicionesContables_Click);
		this.buttonItem2.Image = SIGEFA.Properties.Resources.edit_property_32px;
		this.buttonItem2.ImageIndex = 22;
		this.buttonItem2.ImagePaddingHorizontal = 10;
		this.buttonItem2.ImagePaddingVertical = 15;
		this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.SubItemsExpandWidth = 14;
		this.buttonItem2.Text = "Cambio a Pendiente";
		this.buttonItem2.Click += new System.EventHandler(buttonItem2_Click);
		this.buttonItem3.Image = SIGEFA.Properties.Resources.pdf;
		this.buttonItem3.ImageIndex = 22;
		this.buttonItem3.ImagePaddingHorizontal = 10;
		this.buttonItem3.ImagePaddingVertical = 15;
		this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem3.Name = "buttonItem3";
		this.buttonItem3.SubItemsExpandWidth = 14;
		this.buttonItem3.Text = "Cuadre de caja";
		this.buttonItem3.Click += new System.EventHandler(buttonItem3_Click);
		this.buttonItem4.Image = SIGEFA.Properties.Resources.pdf;
		this.buttonItem4.ImageIndex = 22;
		this.buttonItem4.ImagePaddingHorizontal = 10;
		this.buttonItem4.ImagePaddingVertical = 15;
		this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem4.Name = "buttonItem4";
		this.buttonItem4.SubItemsExpandWidth = 14;
		this.buttonItem4.Text = "Resumen ventas";
		this.buttonItem4.Click += new System.EventHandler(buttonItem4_Click);
		this.buttonItem5.Image = SIGEFA.Properties.Resources._checked;
		this.buttonItem5.ImageIndex = 22;
		this.buttonItem5.ImagePaddingHorizontal = 10;
		this.buttonItem5.ImagePaddingVertical = 15;
		this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem5.Name = "buttonItem5";
		this.buttonItem5.SubItemsExpandWidth = 14;
		this.buttonItem5.Text = "Agupar Selección";
		this.buttonItem5.Click += new System.EventHandler(buttonItem5_Click);
		this.buttonItem7.Image = SIGEFA.Properties.Resources._unchecked;
		this.buttonItem7.ImageIndex = 22;
		this.buttonItem7.ImagePaddingHorizontal = 10;
		this.buttonItem7.ImagePaddingVertical = 15;
		this.buttonItem7.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem7.Name = "buttonItem7";
		this.buttonItem7.SubItemsExpandWidth = 14;
		this.buttonItem7.Text = "Eliminar Seleccion";
		this.buttonItem7.Click += new System.EventHandler(buttonItem7_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingHorizontal = 10;
		this.biActualizar.ImagePaddingVertical = 15;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(biActualizar_Click);
		this.cboMovimientos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.cboMovimientos.FormattingEnabled = true;
		this.cboMovimientos.Items.AddRange(new object[3] { "TODOS LOS MOVIMIENTOS", "INGRESO", "EGRESO" });
		this.cboMovimientos.Location = new System.Drawing.Point(147, 7);
		this.cboMovimientos.Name = "cboMovimientos";
		this.cboMovimientos.Size = new System.Drawing.Size(178, 21);
		this.cboMovimientos.TabIndex = 11;
		this.cboMovimientos.SelectedValueChanged += new System.EventHandler(cboMovimientos_SelectedValueChanged);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(16, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(125, 13);
		this.label1.TabIndex = 11;
		this.label1.Text = "TIPO DE MOVIMIENTO:";
		this.panel1.Controls.Add(this.ribbonBar1);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1353, 65);
		this.panel1.TabIndex = 12;
		this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel2.Controls.Add(this.label16);
		this.panel2.Controls.Add(this.cmbAlmacenes);
		this.panel2.Controls.Add(this.dtpfecha1);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.cboMovimientos);
		this.panel2.Controls.Add(this.btnExit);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(703, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(650, 65);
		this.panel2.TabIndex = 9;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(308, 43);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(61, 13);
		this.label16.TabIndex = 38;
		this.label16.Text = "ALMACEN:";
		this.cmbAlmacenes.Location = new System.Drawing.Point(371, 37);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(125, 24);
		this.cmbAlmacenes.TabIndex = 37;
		this.cmbAlmacenes.ThemeName = "Fluent";
		this.cmbAlmacenes.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.dtpfecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha1.Location = new System.Drawing.Point(147, 37);
		this.dtpfecha1.Name = "dtpfecha1";
		this.dtpfecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpfecha1.TabIndex = 13;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(16, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(45, 13);
		this.label2.TabIndex = 36;
		this.label2.Text = "FECHA:";
		this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExit.ImageIndex = 0;
		this.btnExit.ImageList = this.imageList2;
		this.btnExit.Location = new System.Drawing.Point(568, 7);
		this.btnExit.Name = "btnExit";
		this.btnExit.Size = new System.Drawing.Size(70, 50);
		this.btnExit.TabIndex = 35;
		this.btnExit.Text = "Salir";
		this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExit.UseVisualStyleBackColor = true;
		this.btnExit.Click += new System.EventHandler(btnExit_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
		this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel3.Controls.Add(this.label11);
		this.panel3.Controls.Add(this.lblIngresoTransferencia);
		this.panel3.Controls.Add(this.label14);
		this.panel3.Controls.Add(this.lblIngreso);
		this.panel3.Controls.Add(this.label22);
		this.panel3.Controls.Add(this.label21);
		this.panel3.Controls.Add(this.lblITarjeta);
		this.panel3.Controls.Add(this.label20);
		this.panel3.Controls.Add(this.ribbonBar2);
		this.panel3.Controls.Add(this.label18);
		this.panel3.Controls.Add(this.lblNC);
		this.panel3.Controls.Add(this.label17);
		this.panel3.Controls.Add(this.lblPendiente);
		this.panel3.Controls.Add(this.lblVC);
		this.panel3.Controls.Add(this.label19);
		this.panel3.Controls.Add(this.label3);
		this.panel3.Controls.Add(this.label15);
		this.panel3.Controls.Add(this.lblCajaSeparacion);
		this.panel3.Controls.Add(this.label12);
		this.panel3.Controls.Add(this.lbCheque);
		this.panel3.Controls.Add(this.lbDeposito);
		this.panel3.Controls.Add(this.label13);
		this.panel3.Controls.Add(this.lblSaldoCaja);
		this.panel3.Controls.Add(this.lblAperturaCaja);
		this.panel3.Controls.Add(this.lblEgresos);
		this.panel3.Controls.Add(this.lblIngresos);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.label7);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.label4);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 450);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1353, 121);
		this.panel3.TabIndex = 13;
		this.lblIngresoTransferencia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblIngresoTransferencia.BackColor = System.Drawing.Color.Transparent;
		this.lblIngresoTransferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblIngresoTransferencia.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblIngresoTransferencia.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblIngresoTransferencia.Location = new System.Drawing.Point(860, 42);
		this.lblIngresoTransferencia.Name = "lblIngresoTransferencia";
		this.lblIngresoTransferencia.Size = new System.Drawing.Size(100, 20);
		this.lblIngresoTransferencia.TabIndex = 70;
		this.lblIngresoTransferencia.Text = "0.00";
		this.lblIngresoTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.BackColor = System.Drawing.Color.Transparent;
		this.label14.Location = new System.Drawing.Point(775, 45);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(77, 13);
		this.label14.TabIndex = 69;
		this.label14.Text = "I. TRANSF S/:";
		this.lblIngreso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblIngreso.BackColor = System.Drawing.Color.Transparent;
		this.lblIngreso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblIngreso.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblIngreso.ForeColor = System.Drawing.Color.DarkRed;
		this.lblIngreso.Location = new System.Drawing.Point(1049, 45);
		this.lblIngreso.Name = "lblIngreso";
		this.lblIngreso.Size = new System.Drawing.Size(100, 20);
		this.lblIngreso.TabIndex = 68;
		this.lblIngreso.Text = "0.00";
		this.lblIngreso.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.BackColor = System.Drawing.Color.Transparent;
		this.label22.Location = new System.Drawing.Point(966, 50);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(81, 13);
		this.label22.TabIndex = 67;
		this.label22.Text = "INGRESOS S/:";
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.BackColor = System.Drawing.Color.Transparent;
		this.label21.Location = new System.Drawing.Point(572, 75);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(86, 13);
		this.label21.TabIndex = 65;
		this.label21.Text = "V. TARJETA S/:";
		this.lblITarjeta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblITarjeta.BackColor = System.Drawing.Color.Transparent;
		this.lblITarjeta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblITarjeta.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblITarjeta.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblITarjeta.Location = new System.Drawing.Point(656, 42);
		this.lblITarjeta.Name = "lblITarjeta";
		this.lblITarjeta.Size = new System.Drawing.Size(99, 20);
		this.lblITarjeta.TabIndex = 64;
		this.lblITarjeta.Text = "0.00";
		this.lblITarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.BackColor = System.Drawing.Color.Transparent;
		this.label20.Location = new System.Drawing.Point(572, 45);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(82, 13);
		this.label20.TabIndex = 63;
		this.label20.Text = "I. TARJETA S/:";
		this.ribbonBar2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[5] { this.btnVerificacionCaja, this.btnCierreyArqueoCajaVentas, this.btnDetalleCajaVentas, this.biRencicionCaja, this.biHistorialRendiciones });
		this.ribbonBar2.Location = new System.Drawing.Point(3, 3);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(262, 115);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 14;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.btnVerificacionCaja.Image = SIGEFA.Properties.Resources.buscar;
		this.btnVerificacionCaja.ImagePaddingHorizontal = 15;
		this.btnVerificacionCaja.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnVerificacionCaja.Name = "btnVerificacionCaja";
		this.btnVerificacionCaja.SubItemsExpandWidth = 14;
		this.btnVerificacionCaja.Text = "Verificacion De Caja";
		this.btnVerificacionCaja.Click += new System.EventHandler(btnVerificacionCaja_Click);
		this.btnCierreyArqueoCajaVentas.ImageIndex = 22;
		this.btnCierreyArqueoCajaVentas.ImagePaddingHorizontal = 20;
		this.btnCierreyArqueoCajaVentas.ImagePaddingVertical = 10;
		this.btnCierreyArqueoCajaVentas.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnCierreyArqueoCajaVentas.Name = "btnCierreyArqueoCajaVentas";
		this.btnCierreyArqueoCajaVentas.SubItemsExpandWidth = 14;
		this.btnCierreyArqueoCajaVentas.Text = "Cierre y Arqueo Caja";
		this.btnCierreyArqueoCajaVentas.Click += new System.EventHandler(btnCierreyArqueoCajaVentas_Click);
		this.btnDetalleCajaVentas.Image = (System.Drawing.Image)resources.GetObject("btnDetalleCajaVentas.Image");
		this.btnDetalleCajaVentas.ImagePaddingHorizontal = 20;
		this.btnDetalleCajaVentas.ImagePaddingVertical = 10;
		this.btnDetalleCajaVentas.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnDetalleCajaVentas.Name = "btnDetalleCajaVentas";
		this.btnDetalleCajaVentas.SubItemsExpandWidth = 14;
		this.btnDetalleCajaVentas.Text = "Ver Detalle Caja";
		this.btnDetalleCajaVentas.Click += new System.EventHandler(btnDetalleCajaVentas_Click);
		this.biRencicionCaja.Enabled = false;
		this.biRencicionCaja.Image = (System.Drawing.Image)resources.GetObject("biRencicionCaja.Image");
		this.biRencicionCaja.ImagePaddingHorizontal = 10;
		this.biRencicionCaja.ImagePaddingVertical = 10;
		this.biRencicionCaja.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRencicionCaja.Name = "biRencicionCaja";
		this.biRencicionCaja.SubItemsExpandWidth = 14;
		this.biRencicionCaja.Text = "Rendir   Caja Chica";
		this.biRencicionCaja.Visible = false;
		this.biRencicionCaja.Click += new System.EventHandler(biRencicionCaja_Click);
		this.biHistorialRendiciones.Enabled = false;
		this.biHistorialRendiciones.Image = (System.Drawing.Image)resources.GetObject("biHistorialRendiciones.Image");
		this.biHistorialRendiciones.ImagePaddingHorizontal = 20;
		this.biHistorialRendiciones.ImagePaddingVertical = 10;
		this.biHistorialRendiciones.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biHistorialRendiciones.Name = "biHistorialRendiciones";
		this.biHistorialRendiciones.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.buttonItem15 });
		this.biHistorialRendiciones.SubItemsExpandWidth = 14;
		this.biHistorialRendiciones.Text = "Historial Rendiciones";
		this.biHistorialRendiciones.Visible = false;
		this.biHistorialRendiciones.Click += new System.EventHandler(biHistorialRendiciones_Click);
		this.buttonItem15.Name = "buttonItem15";
		this.buttonItem15.Text = "buttonItem15";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.BackColor = System.Drawing.Color.Transparent;
		this.label18.Location = new System.Drawing.Point(368, 78);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(94, 13);
		this.label18.TabIndex = 62;
		this.label18.Text = "PENDIENTES S/:";
		this.lblNC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblNC.BackColor = System.Drawing.Color.Transparent;
		this.lblNC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblNC.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblNC.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblNC.Location = new System.Drawing.Point(468, 14);
		this.lblNC.Name = "lblNC";
		this.lblNC.Size = new System.Drawing.Size(100, 20);
		this.lblNC.TabIndex = 58;
		this.lblNC.Text = "0.00";
		this.lblNC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.BackColor = System.Drawing.Color.Transparent;
		this.label17.Location = new System.Drawing.Point(372, 16);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(87, 13);
		this.label17.TabIndex = 57;
		this.label17.Text = "N. CREDITO S/:";
		this.lblPendiente.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblPendiente.BackColor = System.Drawing.Color.Transparent;
		this.lblPendiente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblPendiente.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblPendiente.ForeColor = System.Drawing.Color.OrangeRed;
		this.lblPendiente.Location = new System.Drawing.Point(468, 72);
		this.lblPendiente.Name = "lblPendiente";
		this.lblPendiente.Size = new System.Drawing.Size(100, 20);
		this.lblPendiente.TabIndex = 61;
		this.lblPendiente.Text = "0.00";
		this.lblPendiente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblVC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblVC.BackColor = System.Drawing.Color.Transparent;
		this.lblVC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblVC.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblVC.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblVC.Location = new System.Drawing.Point(468, 43);
		this.lblVC.Name = "lblVC";
		this.lblVC.Size = new System.Drawing.Size(100, 20);
		this.lblVC.TabIndex = 60;
		this.lblVC.Text = "0.00";
		this.lblVC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblVC.Click += new System.EventHandler(label18_Click);
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.BackColor = System.Drawing.Color.Transparent;
		this.label19.Location = new System.Drawing.Point(376, 46);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(86, 13);
		this.label19.TabIndex = 59;
		this.label19.Text = "V. CREDITO S/:";
		this.label19.Click += new System.EventHandler(label19_Click);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label3.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label3.Location = new System.Drawing.Point(468, 14);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 20);
		this.label3.TabIndex = 56;
		this.label3.Text = "0.00";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label3.Click += new System.EventHandler(label3_Click);
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.BackColor = System.Drawing.Color.Transparent;
		this.label15.Location = new System.Drawing.Point(381, 18);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(70, 13);
		this.label15.TabIndex = 55;
		this.label15.Text = "MASTER S/:";
		this.lblCajaSeparacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblCajaSeparacion.BackColor = System.Drawing.Color.Transparent;
		this.lblCajaSeparacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblCajaSeparacion.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCajaSeparacion.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblCajaSeparacion.Location = new System.Drawing.Point(656, 71);
		this.lblCajaSeparacion.Name = "lblCajaSeparacion";
		this.lblCajaSeparacion.Size = new System.Drawing.Size(99, 20);
		this.lblCajaSeparacion.TabIndex = 54;
		this.lblCajaSeparacion.Text = "0.00";
		this.lblCajaSeparacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Location = new System.Drawing.Point(969, 23);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(80, 13);
		this.label12.TabIndex = 53;
		this.label12.Text = "DEPOSITO S/:";
		this.lbCheque.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lbCheque.BackColor = System.Drawing.Color.Transparent;
		this.lbCheque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbCheque.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbCheque.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lbCheque.Location = new System.Drawing.Point(860, 72);
		this.lbCheque.Name = "lbCheque";
		this.lbCheque.Size = new System.Drawing.Size(100, 20);
		this.lbCheque.TabIndex = 50;
		this.lbCheque.Text = "0.00";
		this.lbCheque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbDeposito.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lbDeposito.BackColor = System.Drawing.Color.Transparent;
		this.lbDeposito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbDeposito.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDeposito.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lbDeposito.Location = new System.Drawing.Point(1049, 18);
		this.lbDeposito.Name = "lbDeposito";
		this.lbDeposito.Size = new System.Drawing.Size(100, 20);
		this.lbDeposito.TabIndex = 49;
		this.lbDeposito.Text = "0.00";
		this.lbDeposito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.Color.Transparent;
		this.label13.Location = new System.Drawing.Point(775, 75);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(81, 13);
		this.label13.TabIndex = 48;
		this.label13.Text = "V. TRANSF S/:";
		this.lblSaldoCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblSaldoCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblSaldoCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblSaldoCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSaldoCaja.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblSaldoCaja.Location = new System.Drawing.Point(1241, 72);
		this.lblSaldoCaja.Name = "lblSaldoCaja";
		this.lblSaldoCaja.Size = new System.Drawing.Size(100, 20);
		this.lblSaldoCaja.TabIndex = 46;
		this.lblSaldoCaja.Text = "0.00";
		this.lblSaldoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblAperturaCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblAperturaCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblAperturaCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblAperturaCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAperturaCaja.ForeColor = System.Drawing.SystemColors.WindowText;
		this.lblAperturaCaja.Location = new System.Drawing.Point(1241, 46);
		this.lblAperturaCaja.Name = "lblAperturaCaja";
		this.lblAperturaCaja.Size = new System.Drawing.Size(100, 20);
		this.lblAperturaCaja.TabIndex = 45;
		this.lblAperturaCaja.Text = "0.00";
		this.lblAperturaCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblEgresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblEgresos.BackColor = System.Drawing.Color.Transparent;
		this.lblEgresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblEgresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEgresos.ForeColor = System.Drawing.Color.DarkRed;
		this.lblEgresos.Location = new System.Drawing.Point(1049, 75);
		this.lblEgresos.Name = "lblEgresos";
		this.lblEgresos.Size = new System.Drawing.Size(100, 20);
		this.lblEgresos.TabIndex = 44;
		this.lblEgresos.Text = "0.00";
		this.lblEgresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblIngresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblIngresos.BackColor = System.Drawing.Color.Transparent;
		this.lblIngresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblIngresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblIngresos.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblIngresos.Location = new System.Drawing.Point(1241, 18);
		this.lblIngresos.Name = "lblIngresos";
		this.lblIngresos.Size = new System.Drawing.Size(96, 20);
		this.lblIngresos.TabIndex = 43;
		this.lblIngresos.Text = "0.00";
		this.lblIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Location = new System.Drawing.Point(966, 79);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(77, 13);
		this.label6.TabIndex = 42;
		this.label6.Text = "EGRESOS S/:";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(1149, 22);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(91, 13);
		this.label7.TabIndex = 41;
		this.label7.Text = "V. CONTADO S/:";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(1163, 79);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(77, 13);
		this.label5.TabIndex = 40;
		this.label5.Text = "EFECTIVO S/:";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(1156, 53);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(84, 13);
		this.label4.TabIndex = 37;
		this.label4.Text = "APERTURA S/:";
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.lblProperty);
		this.expandablePanel1.Controls.Add(this.label8);
		this.expandablePanel1.Controls.Add(this.label9);
		this.expandablePanel1.Controls.Add(this.label10);
		this.expandablePanel1.Controls.Add(this.lblColumna);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.btnclose);
		this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.expandablePanel1.ExpandButtonVisible = false;
		this.expandablePanel1.Expanded = false;
		this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(1004, 0, 231, 93);
		this.expandablePanel1.Location = new System.Drawing.Point(1116, 0);
		this.expandablePanel1.Name = "expandablePanel1";
		this.expandablePanel1.ShowFocusRectangle = true;
		this.expandablePanel1.Size = new System.Drawing.Size(231, 0);
		this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.Style.BackColor1.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.BackColor2.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarPopupBorder;
		this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
		this.expandablePanel1.Style.GradientAngle = 90;
		this.expandablePanel1.TabIndex = 19;
		this.expandablePanel1.TitleHeight = 0;
		this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
		this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.expandablePanel1.TitleStyle.GradientAngle = 90;
		this.expandablePanel1.TitleText = "Title Bar";
		this.expandablePanel1.Visible = false;
		this.lblProperty.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblProperty.AutoSize = true;
		this.lblProperty.ForeColor = System.Drawing.Color.LightBlue;
		this.lblProperty.Location = new System.Drawing.Point(210, -59);
		this.lblProperty.Name = "lblProperty";
		this.lblProperty.Size = new System.Drawing.Size(12, 13);
		this.lblProperty.TabIndex = 11;
		this.lblProperty.Text = "x";
		this.lblProperty.Visible = false;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(10, -59);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(29, 13);
		this.label8.TabIndex = 10;
		this.label8.Text = "Por :";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(5, -89);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(63, 13);
		this.label9.TabIndex = 9;
		this.label9.Text = "Busqueda";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.ForeColor = System.Drawing.Color.LightBlue;
		this.label10.Location = new System.Drawing.Point(186, -59);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(12, 13);
		this.label10.TabIndex = 7;
		this.label10.Text = "x";
		this.label10.Visible = false;
		this.lblColumna.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblColumna.AutoSize = true;
		this.lblColumna.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblColumna.Location = new System.Drawing.Point(45, -59);
		this.lblColumna.Name = "lblColumna";
		this.lblColumna.Size = new System.Drawing.Size(15, 13);
		this.lblColumna.TabIndex = 6;
		this.lblColumna.Text = "X";
		this.txtFiltro.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFiltro.Location = new System.Drawing.Point(13, -38);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 5;
		this.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnclose.BackColor = System.Drawing.Color.Transparent;
		this.btnclose.FlatAppearance.BorderSize = 0;
		this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnclose.Location = new System.Drawing.Point(213, -93);
		this.btnclose.Margin = new System.Windows.Forms.Padding(1);
		this.btnclose.Name = "btnclose";
		this.btnclose.Size = new System.Drawing.Size(18, 22);
		this.btnclose.TabIndex = 3;
		this.btnclose.Text = "x";
		this.btnclose.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnclose.UseVisualStyleBackColor = false;
		this.btnclose.Click += new System.EventHandler(btnclose_Click);
		this.biVerificarRendicion.ImageIndex = 22;
		this.biVerificarRendicion.ImagePaddingHorizontal = 20;
		this.biVerificarRendicion.ImagePaddingVertical = 10;
		this.biVerificarRendicion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biVerificarRendicion.Name = "biVerificarRendicion";
		this.biVerificarRendicion.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.buttonItem1 });
		this.biVerificarRendicion.SubItemsExpandWidth = 14;
		this.biVerificarRendicion.Text = "Cierre y Arque de  Caja";
		this.buttonItem1.ImageIndex = 22;
		this.buttonItem1.ImagePaddingHorizontal = 20;
		this.buttonItem1.ImagePaddingVertical = 10;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Cierre y Arqueo Caja";
		this.buttonItem6.Image = SIGEFA.Properties.Resources.pdf;
		this.buttonItem6.ImageIndex = 22;
		this.buttonItem6.ImagePaddingHorizontal = 10;
		this.buttonItem6.ImagePaddingVertical = 15;
		this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem6.Name = "buttonItem6";
		this.buttonItem6.SubItemsExpandWidth = 14;
		this.buttonItem6.Text = "Resumen ventas";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvMovimientosCajaChica);
		this.groupBox1.Location = new System.Drawing.Point(0, 67);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1353, 380);
		this.groupBox1.TabIndex = 20;
		this.groupBox1.TabStop = false;
		this.rgvMovimientosCajaChica.AutoScroll = true;
		this.rgvMovimientosCajaChica.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvMovimientosCajaChica.EnableGestures = false;
		this.rgvMovimientosCajaChica.Location = new System.Drawing.Point(3, 16);
		this.rgvMovimientosCajaChica.MasterTemplate.AllowAddNewRow = false;
		this.rgvMovimientosCajaChica.MasterTemplate.AllowColumnReorder = false;
		this.rgvMovimientosCajaChica.MasterTemplate.AllowDeleteRow = false;
		this.rgvMovimientosCajaChica.MasterTemplate.AllowDragToGroup = false;
		this.rgvMovimientosCajaChica.MasterTemplate.AllowEditRow = false;
		this.rgvMovimientosCajaChica.MasterTemplate.AllowRowResize = false;
		this.rgvMovimientosCajaChica.MasterTemplate.ClipboardCutMode = Telerik.WinControls.UI.GridViewClipboardCutMode.Disable;
		this.rgvMovimientosCajaChica.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
		gridViewCheckBoxColumn1.AllowFiltering = false;
		gridViewCheckBoxColumn1.EnableHeaderCheckBox = true;
		gridViewCheckBoxColumn1.HeaderText = "CheckBox";
		gridViewCheckBoxColumn1.Name = "checkbox";
		gridViewTextBoxColumn1.FieldName = "codmovcaja";
		gridViewTextBoxColumn1.HeaderText = "COD";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codmovcaja";
		gridViewTextBoxColumn2.FieldName = "codSucursal";
		gridViewTextBoxColumn2.HeaderText = "COD. SUCURSAL";
		gridViewTextBoxColumn2.Name = "codSucursal";
		gridViewTextBoxColumn2.Width = 100;
		gridViewTextBoxColumn3.FieldName = "codcaja";
		gridViewTextBoxColumn3.HeaderText = "COD. CAJA";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "codcaja";
		gridViewTextBoxColumn4.FieldName = "codpago";
		gridViewTextBoxColumn4.HeaderText = "COD. PAGO";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "codpago";
		gridViewTextBoxColumn5.FieldName = "concepto";
		gridViewTextBoxColumn5.HeaderText = "CONCEPTO";
		gridViewTextBoxColumn5.Name = "concepto";
		gridViewTextBoxColumn5.Width = 272;
		gridViewTextBoxColumn6.FieldName = "monto";
		gridViewTextBoxColumn6.HeaderText = "MONTO";
		gridViewTextBoxColumn6.Name = "monto";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn6.Width = 86;
		gridViewTextBoxColumn7.FieldName = "CODTIPO";
		gridViewTextBoxColumn7.HeaderText = "CODTIPO";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "CODTIPO";
		gridViewTextBoxColumn8.FieldName = "TipoPagoCaja";
		gridViewTextBoxColumn8.HeaderText = "TIPO PAGO";
		gridViewTextBoxColumn8.Name = "tipopagocaja";
		gridViewTextBoxColumn8.Width = 92;
		gridViewTextBoxColumn9.FieldName = "CODTIPOMOV";
		gridViewTextBoxColumn9.HeaderText = "CODTIPOMOV";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "CODTIPOMOV";
		gridViewTextBoxColumn10.FieldName = "TipoMovimiento";
		gridViewTextBoxColumn10.HeaderText = "TIPO MOV.";
		gridViewTextBoxColumn10.Name = "tipoMovimiento";
		gridViewTextBoxColumn10.Width = 98;
		gridViewTextBoxColumn11.FieldName = "fecha";
		gridViewTextBoxColumn11.HeaderText = "FECHA";
		gridViewTextBoxColumn11.Name = "fecha";
		gridViewTextBoxColumn11.Width = 115;
		gridViewTextBoxColumn12.FieldName = "tipodocumento";
		gridViewTextBoxColumn12.HeaderText = "TIPO DOC.";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "tipodocumento";
		gridViewTextBoxColumn12.Width = 82;
		gridViewTextBoxColumn13.HeaderText = "SIGLA";
		gridViewTextBoxColumn13.IsVisible = false;
		gridViewTextBoxColumn13.Name = "SIGLA";
		gridViewTextBoxColumn13.Width = 82;
		gridViewTextBoxColumn14.FieldName = "codserie";
		gridViewTextBoxColumn14.HeaderText = "COD. SERIE";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "codserie";
		gridViewTextBoxColumn15.HeaderText = "SERIE";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "SERIE";
		gridViewTextBoxColumn16.FieldName = "numerodocumento";
		gridViewTextBoxColumn16.HeaderText = "Nº DOC.";
		gridViewTextBoxColumn16.Name = "numerodocumento";
		gridViewTextBoxColumn16.Width = 119;
		gridViewTextBoxColumn17.FieldName = "nomcli";
		gridViewTextBoxColumn17.HeaderText = "CLIENTE";
		gridViewTextBoxColumn17.Name = "nomcli";
		gridViewTextBoxColumn17.Width = 193;
		gridViewTextBoxColumn18.FieldName = "doccli";
		gridViewTextBoxColumn18.HeaderText = "DOC CLI";
		gridViewTextBoxColumn18.Name = "doccli";
		gridViewTextBoxColumn18.Width = 93;
		gridViewTextBoxColumn19.HeaderText = "DOC. REFERENCIA";
		gridViewTextBoxColumn19.Name = "DOCREFERENCIA";
		gridViewTextBoxColumn19.Width = 126;
		gridViewTextBoxColumn20.FieldName = "toneladas";
		gridViewTextBoxColumn20.HeaderText = "TN.";
		gridViewTextBoxColumn20.IsVisible = false;
		gridViewTextBoxColumn20.Name = "toneladas";
		gridViewTextBoxColumn21.FieldName = "codTipoPagoCaja";
		gridViewTextBoxColumn21.HeaderText = "COD. TIPO PAGO";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "codTipoPagoCaja";
		gridViewTextBoxColumn22.HeaderText = "PAGO CAJA";
		gridViewTextBoxColumn22.Name = "PAGOCAJA";
		gridViewTextBoxColumn22.Width = 110;
		gridViewTextBoxColumn23.FieldName = "saldocaja";
		gridViewTextBoxColumn23.HeaderText = "SALDO";
		gridViewTextBoxColumn23.Name = "saldocaja";
		gridViewTextBoxColumn23.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn23.Width = 96;
		gridViewTextBoxColumn24.FieldName = "coduser";
		gridViewTextBoxColumn24.HeaderText = "CODUSER";
		gridViewTextBoxColumn24.IsVisible = false;
		gridViewTextBoxColumn24.Name = "coduser";
		gridViewTextBoxColumn25.FieldName = "estado";
		gridViewTextBoxColumn25.HeaderText = "ESTADO";
		gridViewTextBoxColumn25.Name = "otep";
		gridViewTextBoxColumn25.Width = 100;
		gridViewTextBoxColumn26.FieldName = "fecharegistro";
		gridViewTextBoxColumn26.HeaderText = "FECHA REG.";
		gridViewTextBoxColumn26.IsVisible = false;
		gridViewTextBoxColumn26.Name = "fecharegistro";
		gridViewTextBoxColumn27.FieldName = "CODMONEDA";
		gridViewTextBoxColumn27.HeaderText = "CODMONEDA";
		gridViewTextBoxColumn27.IsVisible = false;
		gridViewTextBoxColumn27.Name = "CODMONEDA";
		gridViewTextBoxColumn28.FieldName = "MONEDA";
		gridViewTextBoxColumn28.HeaderText = "MONEDA";
		gridViewTextBoxColumn28.Name = "MONEDA";
		gridViewTextBoxColumn28.Width = 89;
		gridViewTextBoxColumn29.FieldName = "TCVENTA";
		gridViewTextBoxColumn29.HeaderText = "TC-VENTA";
		gridViewTextBoxColumn29.Name = "TCVENTA";
		gridViewTextBoxColumn29.Width = 86;
		gridViewTextBoxColumn30.FieldName = "tipo_descripcion_ingreso";
		gridViewTextBoxColumn30.HeaderText = "TIPO_DESCRIPCION_INGRESO";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "tipo_descripcion_ingreso";
		gridViewTextBoxColumn31.FieldName = "color";
		gridViewTextBoxColumn31.HeaderText = "color";
		gridViewTextBoxColumn31.IsVisible = false;
		gridViewTextBoxColumn31.Name = "color";
		gridViewTextBoxColumn32.FieldName = "cuenta";
		gridViewTextBoxColumn32.HeaderText = "CUENTA";
		gridViewTextBoxColumn32.Name = "cuenta";
		gridViewTextBoxColumn32.Width = 150;
		gridViewTextBoxColumn33.FieldName = "numcuenta";
		gridViewTextBoxColumn33.HeaderText = "NUMCUENTA";
		gridViewTextBoxColumn33.Name = "numcuenta";
		gridViewTextBoxColumn33.Width = 150;
		this.rgvMovimientosCajaChica.MasterTemplate.Columns.AddRange(gridViewCheckBoxColumn1, gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32, gridViewTextBoxColumn33);
		this.rgvMovimientosCajaChica.MasterTemplate.EnableFiltering = true;
		this.rgvMovimientosCajaChica.MasterTemplate.EnableGrouping = false;
		this.rgvMovimientosCajaChica.MasterTemplate.MultiSelect = true;
		this.rgvMovimientosCajaChica.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.rgvMovimientosCajaChica.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvMovimientosCajaChica.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvMovimientosCajaChica.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvMovimientosCajaChica.Name = "rgvMovimientosCajaChica";
		this.rgvMovimientosCajaChica.ShowHeaderCellButtons = true;
		this.rgvMovimientosCajaChica.Size = new System.Drawing.Size(1347, 361);
		this.rgvMovimientosCajaChica.TabIndex = 23;
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label11.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.Color.OrangeRed;
		this.label11.Location = new System.Drawing.Point(468, 73);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(100, 20);
		this.label11.TabIndex = 71;
		this.label11.Text = "0.00";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1353, 571);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmCajaVentasMovimientos";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Caja Ventas Movimientos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmCajaVentasMovimientos_Load);
		base.Shown += new System.EventHandler(frmCajaVentasMovimientos_Shown);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvMovimientosCajaChica.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvMovimientosCajaChica).EndInit();
		base.ResumeLayout(false);
	}
}
