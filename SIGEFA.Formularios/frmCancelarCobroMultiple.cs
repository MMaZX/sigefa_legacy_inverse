using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmCancelarCobroMultiple : Office2007Form
{
	private clsAdmFactura Admfac = new clsAdmFactura();

	private clsFactura fac = new clsFactura();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	private clsAdmLetra AdmLetra = new clsAdmLetra();

	private clsLetra letra = new clsLetra();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmMetodoPago admMPago = new clsAdmMetodoPago();

	private clsValidar val = new clsValidar();

	private clsPago Pag = new clsPago();

	private clsAdmPago Admpag = new clsAdmPago();

	private clsAdmVendedor AdmVen = new clsAdmVendedor();

	private clsMoneda Mon = new clsMoneda();

	private clsAdmMoneda AdmMoned = new clsAdmMoneda();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	public clsFacturaVenta venta = new clsFacturaVenta();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsAdmBanco AdmBan = new clsAdmBanco();

	private clsAdmTarjetaPago AdmTar = new clsAdmTarjetaPago();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsAdmCtaCte AdmCtaCte = new clsAdmCtaCte();

	private clsCtaCte CtaCte = new clsCtaCte();

	private clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

	public string CodNota;

	public int CodLetra;

	public int tipo;

	private bool tipopago;

	public int Procede = 0;

	public int mon = 0;

	public double Monto = 0.0;

	public int CodDocumento;

	public int CodSerie;

	public int CodCliente;

	private clsNotaIngreso notaI = new clsNotaIngreso();

	private clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();

	private clsUsuario clsentuser = new clsUsuario();

	private string sigl;

	private clsAdmUsuario admuser = new clsAdmUsuario();

	private bool aprobar = false;

	public int VentComp = 0;

	private clsCliente cli = new clsCliente();

	private clsAdmCliente Admcli = new clsAdmCliente();

	private bool creoCorrelativo = false;

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private clsCaja Caja = new clsCaja();

	private double porcentaje_ret = 0.03;

	private double porcentaje_det = 0.12;

	private double ultimo_monto_en_cuenta;

	private int contadorpagados;

	private int j = 0;

	private int scrollaux = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private GroupBox groupBox2;

	private DateTimePicker dtpFecha;

	private Label label11;

	private TextBox txtObservacion;

	private Label label10;

	private Label label9;

	private TextBox txtCheque;

	private TextBox txtOperacion;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label4;

	private Label label5;

	private Label label2;

	private ImageList imageList1;

	private Button btnAceptar;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Label label14;

	private Label label13;

	private Label label12;

	public TextBox txtMontoPendiente;

	public TextBox txtMontoPago;

	public ComboBox cmbMetodoPago;

	public ComboBox cmbMoneda;

	public TextBox txtTipoCambio;

	public TextBox txtMoneda;

	public ComboBox cboNumCta;

	public ComboBox cboBanco;

	public ComboBox cboTarjeta;

	private Button btnCancelar;

	private Button btnImprimir;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CustomValidator customValidator5;

	private CustomValidator customValidator6;

	private CustomValidator customValidator7;

	private TextBox txtDocRef;

	private TextBox txtNumero;

	public TextBox txtSerie;

	private CustomValidator customValidator1;

	public DataGridView dataFacturas;

	private TextBox txtSumaTotal1;

	private TextBox txtSumaPendiente1;

	private TextBox txtSumaDetraccion;

	private TextBox txtSumaRetencion;

	private TextBox txtSumaTotal2;

	private TextBox txtSumaPendiente2;

	private TextBox txtSumaMontoEnCuenta;

	private DataGridViewTextBoxColumn txtFactura;

	private DataGridViewTextBoxColumn txtFecha;

	private DataGridViewTextBoxColumn txtCliente;

	private DataGridViewTextBoxColumn txtTotal1;

	private DataGridViewTextBoxColumn txtPendiente1;

	private DataGridViewCheckBoxColumn chkRet;

	private DataGridViewCheckBoxColumn chkDet;

	private DataGridViewTextBoxColumn txtMontoEnCuenta;

	private DataGridViewTextBoxColumn txtRet;

	private DataGridViewTextBoxColumn txtDet;

	private DataGridViewTextBoxColumn txtTotal2;

	private DataGridViewTextBoxColumn txtPendiente2;

	private DataGridViewTextBoxColumn codigoFactura;

	private DataGridViewTextBoxColumn txtTipoPago;

	public bool caja_aperturada { get; private set; }

	public frmCancelarCobroMultiple()
	{
		InitializeComponent();
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = -1;
	}

	private void CargarTarjetas()
	{
		try
		{
			cboTarjeta.DataSource = AdmTar.MuestraTarjetas(frmLogin.iCodAlmacen);
			cboTarjeta.DisplayMember = "tipo";
			cboTarjeta.ValueMember = "codtarjeta";
			cboTarjeta.SelectedIndex = -1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void CargarBancos()
	{
		try
		{
			cboBanco.DataSource = AdmBan.MuestraBancos();
			cboBanco.DisplayMember = "descripcion";
			cboBanco.ValueMember = "codbanco";
			cboBanco.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void muestra_botones(bool activo)
	{
	}

	private void posiciona_textbox()
	{
		txtSerie.Location = new Point(94, 15);
		txtNumero.Location = new Point(151, 15);
	}

	private void valida_serie(string sigl)
	{
		txtDocRef.Text = sigl;
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtDocRef_KeyPress(txtDocRef, ee);
		txtSerie_KeyPress(txtDocRef, ee);
	}

	private void frmCancelarCobroMultiple_Load(object sender, EventArgs e)
	{
		caja_aperturada = true;
		configuracionDiseñoDataFacturas();
		cargaMoneda();
		CargarBancos();
		CargarTarjetas();
		cboTarjeta.SelectedIndex = -1;
		cboBanco.SelectedIndex = -1;
		if (tipo == 1)
		{
			CargaFactura();
			txtTipoCambio.Enabled = true;
			txtTipoCambio.ReadOnly = false;
			Text = "CANCELAR PAGO";
			sigl = "RP";
			valida_serie(sigl);
			muestra_botones(activo: true);
			posiciona_textbox();
		}
		else if (tipo == 2)
		{
			CargaLetra();
		}
		else if (tipo == 3)
		{
			sigl = "RC";
			valida_serie(sigl);
			muestra_botones(activo: true);
			posiciona_textbox();
			Text = "COBRANZA VENTAS";
		}
		else if (tipo == 4)
		{
			CargaLetra();
			if (letra == null)
			{
			}
		}
		CargaMetodosPagos();
		cmbMetodoPago_SelectionChangeCommitted(cmbMetodoPago, null);
		Mon = AdmMoned.CargaMoneda(mon);
		if (tipo == 1 || tipo == 2)
		{
			if (Mon != null)
			{
				txtMoneda.Text = Mon.SDescripcion;
				tc = AdmTc.CargaTipoCambio(DateTime.Now.Date, 2);
				if (tc != null)
				{
					txtTipoCambio.Text = tc.Venta.ToString();
				}
				else
				{
					txtTipoCambio.Text = "";
					txtTipoCambio.ReadOnly = false;
				}
				cmbMoneda.SelectedValue = Mon.IcodMoneda;
			}
		}
		else if ((tipo == 3 || tipo == 4) && Mon != null)
		{
			txtMoneda.Text = Mon.SDescripcion;
			tc = AdmTc.CargaTipoCambio(DateTime.Now.Date, 2);
			if (tc != null)
			{
				txtTipoCambio.Text = tc.Compra.ToString();
				txtTipoCambio.ReadOnly = true;
			}
			else
			{
				txtTipoCambio.Text = "";
				txtTipoCambio.ReadOnly = false;
			}
			cmbMoneda.SelectedValue = Mon.IcodMoneda;
		}
		mostrarSumaEnCasillas();
		cmbMoneda.Enabled = false;
		calcularSumaTotales();
	}

	private void configuracionDiseñoDataFacturas()
	{
		dataFacturas.Columns[txtFactura.Name].Width = 85;
		dataFacturas.Columns[txtFecha.Name].Width = 60;
		dataFacturas.Columns[txtCliente.Name].Width = 150;
		dataFacturas.Columns[txtTotal1.Name].Width = 65;
		dataFacturas.Columns[txtPendiente1.Name].Width = 65;
		dataFacturas.Columns[chkRet.Name].Width = 30;
		dataFacturas.Columns[chkDet.Name].Width = 30;
		dataFacturas.Columns[txtMontoEnCuenta.Name].Width = 65;
		dataFacturas.Columns[txtRet.Name].Width = 65;
		dataFacturas.Columns[txtDet.Name].Width = 65;
		dataFacturas.Columns[txtTotal2.Name].Width = 65;
		dataFacturas.Columns[txtPendiente2.Name].Width = 65;
		dataFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dataFacturas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
		dataFacturas.Columns[txtCliente.Name].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		foreach (DataGridViewColumn dgvc in dataFacturas.Columns)
		{
			dgvc.ReadOnly = true;
		}
		dataFacturas.Columns[txtMontoEnCuenta.Name].ReadOnly = false;
	}

	private void CargaMetodosPagos()
	{
		DataTable datos = admMPago.CargaMetodoPagos();
		foreach (DataRow fila in datos.Rows)
		{
			if (fila.Field<int>("codMetodoPago") == 10)
			{
				fila.Delete();
			}
		}
		cmbMetodoPago.DataSource = datos;
		cmbMetodoPago.DisplayMember = "descripcion";
		cmbMetodoPago.ValueMember = "codMetodoPago";
		cmbMetodoPago.SelectedValue = 9;
	}

	private void CargaFactura()
	{
		try
		{
			fac = Admfac.CargaFactura(Convert.ToInt32(CodNota));
			if (fac != null)
			{
				mon = fac.Moneda;
				Mon = AdmMoned.CargaMoneda(mon);
				txtMoneda.Text = Mon.SDescripcion;
				txtMontoPendiente.Text = $"{fac.Pendiente.ToString():#,##0.00}";
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Cancelar Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaLetra()
	{
		try
		{
			letra = AdmLetra.CargaLetra(CodLetra);
			if (letra != null)
			{
				if (letra.CodMoneda == 1)
				{
					txtMoneda.Text = "NUEVOS SOLES";
				}
				else
				{
					txtMoneda.Text = "DOLARES";
				}
				cmbMoneda.SelectedValue = letra.CodMoneda;
				txtMontoPendiente.Text = $"{letra.MontoPendiente:#,##0.00}";
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Cancelar Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaNotaSalida()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodNota));
			Mon = AdmMoned.CargaMoneda(venta.Moneda);
			if (venta != null)
			{
				mon = venta.Moneda;
				if (venta.Serie == "")
				{
				}
				if (venta.Moneda == 1)
				{
					txtMoneda.Text = "NUEVOS SOLES";
				}
				else
				{
					txtMoneda.Text = "DOLARES";
				}
				cmbMoneda.SelectedValue = venta.Moneda;
				txtMontoPendiente.Text = $"{venta.Pendiente:#,##0.00}";
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Cancelar Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		try
		{
			if (!superValidator1.Validate())
			{
				return;
			}
			bool band = true;
			contadorpagados = 0;
			VerificaSaldoCaja();
			if (!caja_aperturada)
			{
				return;
			}
			foreach (DataGridViewRow fila in (IEnumerable)dataFacturas.Rows)
			{
				clsFacturaVenta venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(fila.Cells[codigoFactura.Name].Value));
				clsPago Pag = new clsPago();
				double det_ret = 0.0;
				string band_det_ret = "NAD";
				double monto_en_cuenta = 0.0;
				if (Convert.ToBoolean(fila.Cells[chkRet.Name].Value ?? ((object)false)) && fila.Cells[txtRet.Name].Value != null)
				{
					det_ret = Convert.ToDouble(fila.Cells[txtRet.Name].Value);
					band_det_ret = "RET";
					monto_en_cuenta = Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value);
				}
				if (Convert.ToBoolean(fila.Cells[chkDet.Name].Value ?? ((object)false)) && fila.Cells[txtDet.Name].Value != null)
				{
					det_ret = Convert.ToDouble(fila.Cells[txtDet.Name].Value);
					band_det_ret = "DET";
					monto_en_cuenta = Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value);
				}
				Pag.BanderaRetDet = band_det_ret;
				Pag.RetDet = Convert.ToDecimal(det_ret);
				Pag.MontoEnCuenta = Convert.ToDecimal(monto_en_cuenta);
				if (tipo == 1 || tipo == 2)
				{
					Pag.CodNota = fila.Cells[codigoFactura.Name].Value.ToString();
					Pag.CodLetra = letra.CodLetra;
					Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue);
					Pag.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					if (fila.Cells[txtTipoPago.Name].Value == null)
					{
						if (Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value) >= Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value))
						{
							Pag.Tipo = true;
						}
						else
						{
							Pag.Tipo = false;
						}
					}
					else
					{
						Pag.Tipo = Convert.ToBoolean(fila.Cells[txtTipoPago.Name].Value);
					}
					Pag.IngresoEgreso = false;
					if (txtTipoCambio.Text == "")
					{
						Pag.TipoCambio = 0m;
					}
					else
					{
						Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
					}
					Pag.MontoPagado = Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value);
					Pag.MontoCobrado = Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value);
					Pag.Vuelto = 0m;
					Pag.NOperacion = txtOperacion.Text;
					Pag.NCheque = txtCheque.Text;
					Pag.FechaPago = dtpFecha.Value;
					Pag.Observacion = txtObservacion.Text;
					Pag.CodUser = frmLogin.iCodUser;
					Pag.CodAlmacen = frmLogin.iCodAlmacen;
					Pag.CodSerie = CodSerie;
					Pag.Serie = txtSerie.Text;
					Pag.NumDoc = txtNumero.Text;
					decimal montoNC = default(decimal);
					decimal montpen = Convert.ToDecimal(fila.Cells[txtPendiente2.Name].Value);
					montoNC = Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value);
					if (Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value) > montpen)
					{
						montoNC = montpen;
					}
					if (Admpag.insert(Pag))
					{
						MessageBox.Show("Los datos se guardaron correctamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						if (VentComp == 2)
						{
							notaS.DocumentoReferencia = Convert.ToInt32(CodNota);
							AdmVenta.ActualizaPendienteCredito(montoNC, Convert.ToInt32(notaS.CodNotaSalida), notaS.CodAlmacen, 2);
							AdmNotaS.ActualizaNCreditoCompraSinAplicar(notaS);
						}
						Deshabilita_botones(Estado: false);
					}
				}
				else if (tipo == 3 || tipo == 4)
				{
					Pag.CodNota = fila.Cells[codigoFactura.Name].Value.ToString();
					Pag.CodLetra = letra.CodLetra;
					Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue);
					Pag.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					Pag.CodCobrador = Convert.ToInt32(frmLogin.iCodUser);
					Pag.Tipo = tipopago;
					Pag.IngresoEgreso = true;
					if (txtTipoCambio.Text == "")
					{
						Pag.TipoCambio = 0m;
					}
					else
					{
						Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
					}
					Pag.MontoPagado = Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value);
					Pag.MontoCobrado = Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value);
					Pag.Vuelto = 0m;
					Pag.codCtaCte = Convert.ToInt32(cboNumCta.SelectedValue);
					Pag.CtaCte = Convert.ToString(cboNumCta.Text);
					Pag.NOperacion = txtOperacion.Text;
					Pag.NCheque = txtCheque.Text;
					Pag.FechaPago = dtpFecha.Value;
					Pag.Observacion = txtObservacion.Text;
					Pag.CodUser = frmLogin.iCodUser;
					Pag.CodAlmacen = venta.CodAlmacen;
					Pag.CodSerie = CodSerie;
					Pag.CodSucursal = frmLogin.iCodSucursal;
					Pag.CodDoc = CodDocumento;
					if (Caja.Codcaja > 0)
					{
						Pag.Codcaja = Caja.Codcaja;
					}
					else
					{
						Pag.Codcaja = 0;
					}
					if (aprobar)
					{
						Pag.Serie = "";
						Pag.NumDoc = "";
						Pag.Referencia = "";
					}
					Pag.CodBanco = Convert.ToInt32(cboBanco.SelectedValue);
					Pag.NOperacion = Convert.ToString(txtOperacion.Text.Trim());
					Pag.CodTarjeta = Convert.ToInt32(cboTarjeta.SelectedValue);
					Pag.NCheque = Convert.ToString(txtCheque.Text.Trim());
					if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
					{
						if (txtOperacion.Text.Trim() == "" || cboBanco.Text == "" || txtMontoPago.Text == "")
						{
							MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							if (Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value) > 0m)
							{
							}
							Pagar(Pag);
							contadorpagados++;
						}
					}
					else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8)
					{
						if (txtOperacion.Text.Trim() == "" || cboTarjeta.Text == "" || txtMontoPago.Text == "")
						{
							MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							if (Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value) > 0m)
							{
							}
							Pagar(Pag);
							contadorpagados++;
						}
					}
					else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
					{
						if (txtCheque.Text.Trim() == "" || cboBanco.Text == "" || txtOperacion.Text.Trim() == "" || txtMontoPago.Text == "")
						{
							MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							if (Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value) > 0m)
							{
							}
							Pagar(Pag);
							contadorpagados++;
						}
					}
					else
					{
						if (Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value) > 0m)
						{
						}
						Pagar(Pag);
						contadorpagados++;
					}
				}
				if (fila.Index == dataFacturas.Rows.Count - 1)
				{
					MessageBox.Show("Se realizaron " + ((contadorpagados > 0) ? contadorpagados : dataFacturas.Rows.Count) + " pagos correctos.", "Pago Multiple", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Close();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void VerificaSaldoCaja()
	{
		try
		{
			Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, venta.CodAlmacen, frmLogin.iCodUser);
			if (Caja == null)
			{
				MessageBox.Show("Debe de aperturar caja", "Pagos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				caja_aperturada = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void calcularSumaTotales()
	{
		decimal sumaDet = default(decimal);
		decimal sumaRet = default(decimal);
		decimal sumaPendiente1 = default(decimal);
		decimal sumaPendiente2 = default(decimal);
		decimal sumaTotal1 = default(decimal);
		decimal sumaTotal2 = default(decimal);
		decimal sumaMontoEnCuenta = default(decimal);
		foreach (DataGridViewRow fila in (IEnumerable)dataFacturas.Rows)
		{
			sumaDet += Convert.ToDecimal(fila.Cells[txtDet.Name].Value ?? ((object)0));
			sumaRet += Convert.ToDecimal(fila.Cells[txtRet.Name].Value ?? ((object)0));
			sumaPendiente1 += Convert.ToDecimal(fila.Cells[txtPendiente1.Name].Value ?? ((object)0));
			sumaPendiente2 += Convert.ToDecimal(fila.Cells[txtPendiente2.Name].Value ?? ((object)0));
			sumaTotal1 += Convert.ToDecimal(fila.Cells[txtTotal1.Name].Value ?? ((object)0));
			sumaTotal2 += Convert.ToDecimal(fila.Cells[txtTotal2.Name].Value ?? ((object)0));
			sumaMontoEnCuenta += Convert.ToDecimal(fila.Cells[txtMontoEnCuenta.Name].Value ?? ((object)0));
		}
		txtSumaDetraccion.Text = sumaDet.ToString();
		txtSumaRetencion.Text = sumaRet.ToString();
		txtSumaPendiente1.Text = sumaPendiente1.ToString();
		txtSumaPendiente2.Text = sumaPendiente2.ToString();
		txtSumaTotal1.Text = sumaTotal1.ToString();
		txtSumaTotal2.Text = sumaTotal2.ToString();
		txtSumaMontoEnCuenta.Text = sumaMontoEnCuenta.ToString();
	}

	private void cargaPago(clsPago p)
	{
		p = Admpag.MuestraPagoVenta(frmLogin.iCodAlmacen, Pag.CodPago);
		if (p != null)
		{
			cmbMetodoPago.SelectedValue = p.CodTipoPago;
			cboBanco.SelectedValue = p.CodBanco;
			cboTarjeta.SelectedValue = p.CodTarjeta;
			cboNumCta.SelectedValue = p.codCtaCte;
			txtTipoCambio.Text = p.TipoCambio.ToString();
			txtCheque.Text = p.NCheque;
			txtObservacion.Text = p.Observacion;
			txtOperacion.Text = p.NOperacion;
			txtMontoPago.Text = p.MontoCobrado.ToString();
			dtpFecha.Value = p.FechaPago;
			txtSerie.Text = p.Serie;
			txtNumero.Text = p.NumDoc;
		}
	}

	private void Deshabilita_botones(bool Estado)
	{
		cboBanco.Enabled = Estado;
		cboNumCta.Enabled = Estado;
		cboTarjeta.Enabled = Estado;
		txtCheque.Enabled = Estado;
		txtOperacion.Enabled = Estado;
		txtObservacion.Enabled = Estado;
		dtpFecha.Enabled = Estado;
		btnAceptar.Enabled = Estado;
		btnCancelar.Enabled = Estado;
		txtNumero.Enabled = Estado;
		txtNumero.Visible = !Estado;
	}

	private void Pagar(clsPago Pag)
	{
		try
		{
			Form auxq = Application.OpenForms["frmCobros"];
			if (Convert.ToInt32(cmbMetodoPago.SelectedValue) != 6 && Convert.ToInt32(cmbMetodoPago.SelectedValue) != 7 && Convert.ToInt32(cmbMetodoPago.SelectedValue) != 9)
			{
				Pag.Aprobado = 4;
			}
			else if (tipo == 3)
			{
				Pag.Aprobado = 4;
			}
			else
			{
				Pag.Aprobado = 1;
			}
			Admpag.insert(Pag);
			bool flag = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
	}

	private bool EnviaCorreo(clsPago Pag)
	{
		clsentuser = null;
		try
		{
			clsentuser = admuser.MuestraUsuario(frmLogin.iCodUser);
			DataTable correosTesoreria = new DataTable();
			string sRutaPrincipal = "";
			string sArchivo = "";
			string sRutaFinal = "";
			string sUserCredential = clsentuser.Email;
			string sPassCredential = clsentuser.ContraEmail;
			MailMessage correo = new MailMessage();
			SmtpClient smtp = new SmtpClient();
			string destino = "";
			string asunto = "";
			string cuerpo = "";
			string ccs = string.Empty;
			smtp.Host = clsentuser.Host;
			if (clsentuser.Host == "smtp.gmail.com" || clsentuser.Host == "smtp.live.com")
			{
				smtp.EnableSsl = true;
			}
			smtp.Credentials = new NetworkCredential(sUserCredential, sPassCredential);
			correo.From = new MailAddress(clsentuser.Email);
			correosTesoreria = admuser.correoTesoreria();
			if (correosTesoreria != null)
			{
				if (correosTesoreria.Rows.Count > 1)
				{
					for (int i = 0; i <= correosTesoreria.Rows.Count - 1; i++)
					{
						destino = destino + correosTesoreria.Rows[i]["email"].ToString() + ",";
					}
					destino = destino.TrimEnd(',');
					correo.To.Add(destino.Trim() + "," + admuser.MuestraUsuarioNivel().Email);
				}
				else if (correosTesoreria.Rows.Count == 1)
				{
					destino = correosTesoreria.Rows[0]["email"].ToString();
					correo.To.Add(destino.Trim() + "," + admuser.MuestraUsuarioNivel().Email);
				}
			}
			asunto = "PAGO POR APROBAR";
			cuerpo = "EL SIGUIENTE PAGO: N. " + Pag.Serie + "-" + Pag.NumDoc + " NECESITA APROBACION." + Environment.NewLine + "SUCURSAL: " + frmLogin.sAlmacen + Environment.NewLine + "USUARIO: " + frmLogin.sUsuario + Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine;
			correo.Subject = asunto;
			correo.Body = cuerpo;
			correo.IsBodyHtml = false;
			correo.Priority = MailPriority.Normal;
			smtp.Send(correo);
			MessageBox.Show("Correo Enviado Satisfactoriamente.");
			return true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		if (tc != null)
		{
			txtTipoCambio.Text = tc.Venta.ToString();
			txtTipoCambio.ReadOnly = true;
		}
		else
		{
			MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtTipoCambio.Text = "";
			txtTipoCambio.ReadOnly = false;
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtMontoPago_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Text != "")
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void frmCancelarPagoMultiple_Shown(object sender, EventArgs e)
	{
		if (txtSerie.Visible)
		{
			txtSerie.Focus();
		}
		else
		{
			txtMontoPago.Focus();
		}
	}

	private void txtMontoPago_TextChanged(object sender, EventArgs e)
	{
	}

	private void unSelected(decimal txtMontoPagoD, decimal txtMontoPendienteD, DataGridViewRowCollection dataGVRCollection)
	{
	}

	private void txtMontoPago_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			double box_double = 0.0;
			double.TryParse(txtMontoPago.Text, out box_double);
			if (box_double > Convert.ToDouble(txtMontoPendiente.Text) && txtMontoPago.Text != "")
			{
				txtMontoPago.Text = txtMontoPendiente.Text;
				txtMontoPago.Select(txtMontoPago.Text.Length, 0);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cmbMetodoPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			buscaAprobacion(Convert.ToInt32(cmbMetodoPago.SelectedValue));
			if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 5)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = false;
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtCheque.Text = "";
				txtOperacion.Text = "";
				txtOperacion.Enabled = false;
				txtCheque.Enabled = false;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = true;
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				cboBanco.Focus();
				txtCheque.Text = "";
				txtOperacion.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = false;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = true;
				cboBanco.Focus();
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtOperacion.Text = "";
				txtCheque.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = true;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8)
			{
				cboTarjeta.Enabled = true;
				cboBanco.Enabled = true;
				cboTarjeta.Focus();
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtOperacion.Text = "";
				txtCheque.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = false;
				cboNumCta.Enabled = true;
				cboNumCta.SelectedIndex = -1;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void buscaAprobacion(int p)
	{
		DataTable nuevaDataMetodo = new DataTable();
		try
		{
			nuevaDataMetodo = null;
			nuevaDataMetodo = (DataTable)cmbMetodoPago.DataSource;
			DataRow dataRow = Enumerable.FirstOrDefault<DataRow>((IEnumerable<DataRow>)nuevaDataMetodo.AsEnumerable(), (Func<DataRow, bool>)((DataRow r) => Convert.ToInt32(r["codMetodoPago"]) == p));
			if (dataRow != null)
			{
				if (Convert.ToInt32(dataRow["aprobacion"].ToString()) == 0)
				{
					aprobar = false;
				}
				else if (Convert.ToInt32(dataRow["aprobacion"].ToString()) == 1)
				{
					aprobar = true;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaCtaCte()
	{
		try
		{
			cboNumCta.DataSource = AdmCtaCte.ListaCtasBanco(Convert.ToInt32(cboBanco.SelectedValue), frmLogin.iCodAlmacen);
			cboNumCta.DisplayMember = "cuentaCorriente";
			cboNumCta.ValueMember = "codCuentaCorriente";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cboBanco_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			if (cmbMetodoPago.Text == "DEPOSITO" || cmbMetodoPago.Text == "TRANSFERENCIA")
			{
				cboNumCta.Enabled = true;
				CargaCtaCte();
			}
			else
			{
				cboNumCta.SelectedIndex = -1;
				cboNumCta.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void txtOperacion_KeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			if (char.IsDigit(e.KeyChar))
			{
				e.Handled = false;
			}
			else if (char.IsControl(e.KeyChar))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			if (char.IsDigit(e.KeyChar))
			{
				e.Handled = false;
			}
			else if (char.IsControl(e.KeyChar))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		if (tipo == 3)
		{
			CRImpresionPago rpt = new CRImpresionPago();
			frmRptImpresionPago frm = new frmRptImpresionPago();
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rpt.SetDataSource(ds.ReporteImpresionPago(Pag.CodPago, frmLogin.iCodAlmacen));
			frm.cRVImpresionPago.ReportSource = rpt;
			frm.Show();
		}
		else if (tipo == 1)
		{
			CRImpresionCobro rpt2 = new CRImpresionCobro();
			frmRptImpresionPago frm2 = new frmRptImpresionPago();
			PrintOptions rptoption2 = rpt2.PrintOptions;
			rptoption2.PrinterName = ser.NombreImpresora;
			rptoption2.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rpt2.SetDataSource(ds.ReporteImpresionCobro(Pag.CodPago, frmLogin.iCodAlmacen));
			frm2.cRVImpresionPago.ReportSource = rpt2;
			frm2.Show();
		}
	}

	private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
	{
		VentaEnMoneda();
	}

	private void VentaEnMoneda()
	{
		decimal TipoCambio = default(decimal);
		TipoCambio = Convert.ToDecimal(txtTipoCambio.Text.Trim());
		if (mon == 1)
		{
			if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
			{
				txtMontoPendiente.Text = $"{Convert.ToDecimal(venta.Pendiente) / TipoCambio:#,##0.00}";
			}
			else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
			{
				txtMontoPendiente.Text = $"{Convert.ToDecimal(venta.Pendiente):#,##0.00}";
			}
		}
		else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
		{
			txtMontoPendiente.Text = $"{Convert.ToDecimal(venta.Pendiente):#,##0.00}";
		}
		else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
		{
			txtMontoPendiente.Text = $"{Convert.ToDecimal(venta.Pendiente) * TipoCambio:#,##0.00}";
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (c.SelectedIndex != -1)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (c.SelectedIndex != -1)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (c.SelectedIndex != -1)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (c.SelectedIndex != -1)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Enabled)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator7_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Enabled)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmSerie"] != null)
		{
			Application.OpenForms["frmSerie"].Activate();
			return;
		}
		frmSerie form = new frmSerie();
		form.DocSeleccionado = CodDocumento;
		form.Sigla = sigl;
		form.Proceso = 3;
		form.ShowDialog();
		ser = form.ser;
		CodSerie = ser.CodSerie;
		txtSerie.Text = ser.Serie;
		if (CodSerie != 0)
		{
			ProcessTabKey(forward: true);
			if (txtSerie.Text == "")
			{
				txtSerie.Focus();
			}
		}
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.enteros(e);
		if (e.KeyChar == '\r' && BuscaSerie())
		{
			txtSerie.Text = ser.Serie;
			if (ser.PreImpreso)
			{
				txtNumero.Text = "";
				txtNumero.Visible = true;
				txtNumero.Focus();
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Visible = false;
				txtNumero.Text = ser.Numeracion.ToString();
			}
			ProcessTabKey(forward: true);
		}
	}

	private bool BuscaSerie()
	{
		ser = Admser.BuscaSeriexDocumento(CodDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void txtSerie_Leave(object sender, EventArgs e)
	{
		if (BuscaSerie2())
		{
			txtSerie.Text = ser.Serie;
			if (ser.PreImpreso)
			{
				txtNumero.Text = "";
				txtNumero.Visible = true;
				txtNumero.Focus();
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Visible = false;
				txtNumero.Text = ser.Numeracion.ToString();
			}
		}
	}

	private bool BuscaSerie2()
	{
		ser = Admser.MuestraSerie(CodSerie, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void txtSerie_TextChanged(object sender, EventArgs e)
	{
		txtNumero.Text = "";
		txtNumero.Visible = false;
	}

	private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.enteros(e);
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtNumero_Leave(object sender, EventArgs e)
	{
		if (txtNumero.Text == "" && txtNumero.Visible)
		{
			txtNumero.Focus();
		}
	}

	private void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtDocRef.Text != "")
		{
			if (BuscaTipoDocumento())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaTipoDocumento()
	{
		doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			txtDocRef.Text = doc.Sigla;
			return true;
		}
		CodDocumento = 0;
		txtDocRef.Text = "";
		return false;
	}

	private void txtDocRef_Leave(object sender, EventArgs e)
	{
		BuscaTipoDocumento();
	}

	private void customValidator1_ValidateValue_1(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Text != "")
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void txtCodClientes_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmClientesLista"] != null)
			{
				Application.OpenForms["frmClientesLista"].Activate();
				return;
			}
			frmClientesLista form = new frmClientesLista();
			form.Proceso = 3;
			form.ShowDialog();
			cli = form.cli;
			CodCliente = cli.CodCliente;
		}
	}

	private void CargaFacturasxPagar(int CodCliente)
	{
		try
		{
			if (dataFacturas.DataSource != null)
			{
				dataFacturas.DataSource = null;
				return;
			}
			dataFacturas.AutoGenerateColumns = false;
			dataFacturas.DataSource = AdmVenta.ListaFacturas_ventaCliente(CodCliente);
			dataFacturas.SelectAll();
			if (dataFacturas.DataSource != null)
			{
				CargaMontoPendiente(dataFacturas.SelectedRows);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaMontoPendiente(DataGridViewSelectedRowCollection dataGridViewSelectedRowCollection)
	{
		try
		{
			decimal montopendienteS = default(decimal);
			foreach (DataGridViewRow row in dataGridViewSelectedRowCollection)
			{
				montopendienteS += Convert.ToDecimal(row.Cells[txtPendiente1.Name].Value);
			}
			txtMontoPendiente.Text = montopendienteS.ToString();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtCodClientes_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void dataFacturas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dataFacturas.DataSource != null)
		{
			CargaMontoPendiente(dataFacturas.SelectedRows);
		}
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void dataFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		calcularRetencionDetraccion(e);
		mostrarSumaEnCasillas();
		calcularSumaTotales();
	}

	private void calcularRetencionDetraccion(DataGridViewCellEventArgs e)
	{
		DataGridViewRow fila = dataFacturas.Rows[e.RowIndex];
		DataGridViewCell celda = dataFacturas.Rows[e.RowIndex].Cells[e.ColumnIndex];
		string nombre_de_columna = celda.OwningColumn.Name;
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
			if (dataFacturas.Rows[e.RowIndex].Cells[chkDet.Name].Value != null && (bool)dataFacturas.Rows[e.RowIndex].Cells[chkDet.Name].Value)
			{
				MessageBox.Show("No se puede seleccionar Retencion mientras Detraccion esta seleccionado.", "Error al Seleccionar");
			}
			else
			{
				celda.Value = !(bool)celda.Value;
				if ((bool)celda.Value)
				{
					try
					{
						double aux_pendiente = Convert.ToDouble(fila.Cells[txtTotal1.Name].Value ?? ((object)0.0));
						fila.Cells[txtMontoEnCuenta.Name].Value = aux_pendiente * (1.0 - porcentaje_ret);
						fila.Cells[txtRet.Name].Value = aux_pendiente * porcentaje_ret;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), ex.Message);
					}
				}
				else
				{
					fila.Cells[txtMontoEnCuenta.Name].Value = fila.Cells[txtPendiente1.Name].Value;
					fila.Cells[txtRet.Name].Value = null;
					fila.Cells[txtTotal2.Name].Value = fila.Cells[txtPendiente1.Name].Value;
					fila.Cells[txtPendiente2.Name].Value = 0.0;
				}
			}
		}
		if (!(nombre_de_columna == chkDet.Name))
		{
			return;
		}
		if (dataFacturas.Rows[e.RowIndex].Cells[chkRet.Name].Value != null && (bool)dataFacturas.Rows[e.RowIndex].Cells[chkRet.Name].Value)
		{
			MessageBox.Show("No se puede seleccionar Detraccion mientras Retencion esta seleccionado.", "Error al Seleccionar");
			return;
		}
		celda.Value = !(bool)celda.Value;
		if ((bool)celda.Value)
		{
			try
			{
				double aux_pendiente2 = Convert.ToDouble(fila.Cells[txtTotal1.Name].Value ?? ((object)0.0));
				fila.Cells[txtMontoEnCuenta.Name].Value = aux_pendiente2 * (1.0 - porcentaje_det);
				fila.Cells[txtDet.Name].Value = aux_pendiente2 * porcentaje_det;
				return;
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.ToString(), ex2.Message);
				return;
			}
		}
		fila.Cells[txtMontoEnCuenta.Name].Value = fila.Cells[txtPendiente1.Name].Value;
		fila.Cells[txtDet.Name].Value = null;
		fila.Cells[txtTotal2.Name].Value = fila.Cells[txtPendiente1.Name].Value;
		fila.Cells[txtPendiente2.Name].Value = 0.0;
	}

	private void dataFacturas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		try
		{
			ultimo_monto_en_cuenta = Convert.ToDouble(dataFacturas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? ((object)0));
			if ((bool)dataFacturas.Rows[e.RowIndex].Cells[chkDet.Name].Value || (bool)dataFacturas.Rows[e.RowIndex].Cells[chkRet.Name].Value)
			{
				dataFacturas.Rows[e.RowIndex].Cells[chkDet.Name].Value = false;
				dataFacturas.Rows[e.RowIndex].Cells[chkRet.Name].Value = false;
				dataFacturas.Rows[e.RowIndex].Cells[txtDet.Name].Value = "";
				dataFacturas.Rows[e.RowIndex].Cells[txtRet.Name].Value = "";
				dataFacturas.Rows[e.RowIndex].Cells[txtTotal2.Name].Value = dataFacturas.Rows[e.RowIndex].Cells[txtPendiente1.Name].Value;
				dataFacturas.Rows[e.RowIndex].Cells[txtMontoEnCuenta.Name].Value = dataFacturas.Rows[e.RowIndex].Cells[txtPendiente1.Name].Value;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dataFacturas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			DataGridViewRow fila = dataFacturas.Rows[e.RowIndex];
			string cadena_celda = ((fila.Cells[e.ColumnIndex].Value != null) ? fila.Cells[e.ColumnIndex].Value.ToString() : "0");
			double dato_de_celda = Convert.ToDouble(cadena_celda);
			if (dato_de_celda > Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value))
			{
				MessageBox.Show("La cantidad ingresada no debe ser mayor que " + Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value), "Cantidad Ingresada Incorrecta");
				double aux_pendiente = Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value ?? ((object)0));
				DataGridViewCell dataGridViewCell = fila.Cells[txtMontoEnCuenta.Name];
				_ = ultimo_monto_en_cuenta;
				dataGridViewCell.Value = ((ultimo_monto_en_cuenta < aux_pendiente) ? ultimo_monto_en_cuenta : aux_pendiente);
				if (Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value) >= 0.0)
				{
					if (Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value) >= Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value))
					{
						fila.Cells[txtTipoPago.Name].Value = true;
					}
					else
					{
						fila.Cells[txtTipoPago.Name].Value = false;
					}
				}
				return;
			}
			fila.Cells[txtRet.Name].Value = null;
			fila.Cells[txtDet.Name].Value = null;
			fila.Cells[txtTotal2.Name].Value = null;
			fila.Cells[txtPendiente2.Name].Value = null;
			if (Convert.ToBoolean(fila.Cells[chkRet.Name].Value ?? ((object)false)))
			{
				fila.Cells[txtRet.Name].Value = dato_de_celda * porcentaje_ret;
				fila.Cells[txtTotal2.Name].Value = dato_de_celda + Convert.ToDouble(fila.Cells[txtRet.Name].Value);
				fila.Cells[txtPendiente2.Name].Value = Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value) - Convert.ToDouble(fila.Cells[txtTotal2.Name].Value);
			}
			if (Convert.ToBoolean(fila.Cells[chkDet.Name].Value ?? ((object)false)))
			{
				fila.Cells[txtDet.Name].Value = dato_de_celda * porcentaje_det;
				fila.Cells[txtTotal2.Name].Value = dato_de_celda + Convert.ToDouble(fila.Cells[txtDet.Name].Value);
				fila.Cells[txtPendiente2.Name].Value = Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value) - Convert.ToDouble(fila.Cells[txtTotal2.Name].Value);
			}
			if (!Convert.ToBoolean(fila.Cells[chkDet.Name].Value ?? ((object)false)) && !Convert.ToBoolean(fila.Cells[chkRet.Name].Value ?? ((object)false)))
			{
				fila.Cells[txtTotal2.Name].Value = dato_de_celda.ToString();
				fila.Cells[txtPendiente2.Name].Value = Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value) - Convert.ToDouble(fila.Cells[txtTotal2.Name].Value);
			}
			mostrarSumaEnCasillas();
			calcularSumaTotales();
			if (Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value) >= 0.0)
			{
				if (Convert.ToDouble(fila.Cells[txtMontoEnCuenta.Name].Value) >= Convert.ToDouble(fila.Cells[txtPendiente1.Name].Value))
				{
					fila.Cells[txtTipoPago.Name].Value = true;
				}
				else
				{
					fila.Cells[txtTipoPago.Name].Value = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
			double aux_pendiente2 = Convert.ToDouble(dataFacturas.Rows[e.RowIndex].Cells[txtPendiente1.Name].Value ?? ((object)0));
			DataGridViewCell dataGridViewCell2 = dataFacturas.Rows[e.RowIndex].Cells[e.ColumnIndex];
			_ = ultimo_monto_en_cuenta;
			dataGridViewCell2.Value = ((ultimo_monto_en_cuenta < aux_pendiente2) ? ultimo_monto_en_cuenta : aux_pendiente2);
			dataFacturas_CellEndEdit(sender, e);
		}
	}

	private void mostrarSumaEnCasillas()
	{
		double suma_monto_pago = 0.0;
		double suma_monto_pendiente = 0.0;
		foreach (DataGridViewRow fila in (IEnumerable)dataFacturas.Rows)
		{
			suma_monto_pago += Convert.ToDouble(fila.Cells[txtTotal2.Name].Value.ToString());
			suma_monto_pendiente += Convert.ToDouble(fila.Cells[txtPendiente2.Name].Value.ToString());
		}
		string datoMontoPago = Convert.ToString(suma_monto_pago.ToString("0.###"));
		string datoMontoPendiente = suma_monto_pendiente.ToString("0.###");
		txtMontoPago.Text = datoMontoPago;
		txtMontoPendiente.Text = datoMontoPendiente;
	}

	private void dataFacturas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
	{
		Console.WriteLine(j + 1 + ": dataFacturas_ColumnWidthChanged " + e.Column.Name);
		j++;
		foreach (DataGridViewColumn item in dataFacturas.Columns)
		{
			switcheable(item);
		}
	}

	private void switcheable(DataGridViewColumn Column)
	{
		int auxY = 0;
		int sumaColumn = 0;
		int ultimoX = base.Width - (20 + scrollaux);
		switch (Column.Name)
		{
		case "txtTotal1":
		{
			auxY = txtSumaTotal1.Location.Y;
			sumaColumn = dataFacturas.Columns[txtPendiente1.Name].Width + dataFacturas.Columns[chkRet.Name].Width + dataFacturas.Columns[chkDet.Name].Width + dataFacturas.Columns[txtMontoEnCuenta.Name].Width + dataFacturas.Columns[txtRet.Name].Width + dataFacturas.Columns[txtDet.Name].Width + dataFacturas.Columns[txtPendiente2.Name].Width + dataFacturas.Columns[txtTotal2.Name].Width;
			txtSumaTotal1.Width = Column.Width;
			ultimoX -= sumaColumn + Column.Width;
			Point p = new Point(ultimoX, auxY);
			txtSumaTotal1.Location = p;
			break;
		}
		case "txtPendiente1":
		{
			auxY = txtSumaPendiente1.Location.Y;
			sumaColumn = dataFacturas.Columns[chkRet.Name].Width + dataFacturas.Columns[chkDet.Name].Width + dataFacturas.Columns[txtMontoEnCuenta.Name].Width + dataFacturas.Columns[txtRet.Name].Width + dataFacturas.Columns[txtDet.Name].Width + dataFacturas.Columns[txtPendiente2.Name].Width + dataFacturas.Columns[txtTotal2.Name].Width;
			txtSumaPendiente1.Width = Column.Width;
			ultimoX -= sumaColumn + Column.Width;
			Point p = new Point(ultimoX, auxY);
			txtSumaPendiente1.Location = p;
			txtSumaPendiente1.Width = Column.Width;
			break;
		}
		case "txtMontoEnCuenta":
		{
			auxY = txtSumaMontoEnCuenta.Location.Y;
			sumaColumn = dataFacturas.Columns[txtRet.Name].Width + dataFacturas.Columns[txtDet.Name].Width + dataFacturas.Columns[txtPendiente2.Name].Width + dataFacturas.Columns[txtTotal2.Name].Width;
			txtSumaMontoEnCuenta.Width = Column.Width;
			ultimoX -= sumaColumn + Column.Width;
			Point p = new Point(ultimoX, auxY);
			txtSumaMontoEnCuenta.Location = p;
			txtSumaMontoEnCuenta.Width = Column.Width;
			break;
		}
		case "txtRet":
		{
			auxY = txtSumaRetencion.Location.Y;
			sumaColumn = dataFacturas.Columns[txtDet.Name].Width + dataFacturas.Columns[txtPendiente2.Name].Width + dataFacturas.Columns[txtTotal2.Name].Width;
			txtSumaRetencion.Width = Column.Width;
			ultimoX -= sumaColumn + Column.Width;
			Point p = new Point(ultimoX, auxY);
			txtSumaRetencion.Location = p;
			txtSumaRetencion.Width = Column.Width;
			break;
		}
		case "txtDet":
		{
			auxY = txtSumaDetraccion.Location.Y;
			sumaColumn = dataFacturas.Columns[txtPendiente2.Name].Width + dataFacturas.Columns[txtTotal2.Name].Width;
			txtSumaDetraccion.Width = Column.Width;
			ultimoX -= sumaColumn + Column.Width;
			Point p = new Point(ultimoX, auxY);
			txtSumaDetraccion.Location = p;
			txtSumaDetraccion.Width = Column.Width;
			break;
		}
		case "txtTotal2":
		{
			auxY = txtSumaTotal2.Location.Y;
			sumaColumn = dataFacturas.Columns[txtPendiente2.Name].Width;
			txtSumaTotal2.Width = Column.Width;
			ultimoX -= sumaColumn + Column.Width;
			Point p = new Point(ultimoX, auxY);
			txtSumaTotal2.Location = p;
			txtSumaTotal2.Width = Column.Width;
			break;
		}
		case "txtPendiente2":
		{
			auxY = txtSumaPendiente2.Location.Y;
			txtSumaPendiente2.Width = Column.Width;
			ultimoX -= sumaColumn + Column.Width;
			Point p = new Point(ultimoX, auxY);
			txtSumaPendiente2.Location = p;
			txtSumaPendiente2.Width = Column.Width;
			break;
		}
		}
	}

	private void dataFacturas_Scroll(object sender, ScrollEventArgs e)
	{
		scrollaux = 18;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCancelarCobroMultiple));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dataFacturas = new System.Windows.Forms.DataGridView();
		this.txtFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtTotal1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtPendiente1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.chkRet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.chkDet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.txtMontoEnCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtRet = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtDet = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtTotal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtPendiente2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigoFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtTipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtMoneda = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtMontoPendiente = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.cboNumCta = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.cboBanco = new System.Windows.Forms.ComboBox();
		this.label12 = new System.Windows.Forms.Label();
		this.cboTarjeta = new System.Windows.Forms.ComboBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label11 = new System.Windows.Forms.Label();
		this.txtObservacion = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtMontoPago = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtCheque = new System.Windows.Forms.TextBox();
		this.txtOperacion = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.txtSumaPendiente2 = new System.Windows.Forms.TextBox();
		this.txtSumaTotal2 = new System.Windows.Forms.TextBox();
		this.txtSumaRetencion = new System.Windows.Forms.TextBox();
		this.txtSumaDetraccion = new System.Windows.Forms.TextBox();
		this.txtSumaPendiente1 = new System.Windows.Forms.TextBox();
		this.txtSumaTotal1 = new System.Windows.Forms.TextBox();
		this.txtSumaMontoEnCuenta = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataFacturas).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dataFacturas);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(834, 191);
		this.groupBox1.TabIndex = 23;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos de Facturas";
		this.dataFacturas.AllowUserToAddRows = false;
		this.dataFacturas.AllowUserToDeleteRows = false;
		this.dataFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataFacturas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dataFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataFacturas.Columns.AddRange(this.txtFactura, this.txtFecha, this.txtCliente, this.txtTotal1, this.txtPendiente1, this.chkRet, this.chkDet, this.txtMontoEnCuenta, this.txtRet, this.txtDet, this.txtTotal2, this.txtPendiente2, this.codigoFactura, this.txtTipoPago);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataFacturas.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataFacturas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataFacturas.Location = new System.Drawing.Point(3, 16);
		this.dataFacturas.MultiSelect = false;
		this.dataFacturas.Name = "dataFacturas";
		this.dataFacturas.RowHeadersVisible = false;
		this.dataFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataFacturas.Size = new System.Drawing.Size(828, 172);
		this.dataFacturas.TabIndex = 28;
		this.dataFacturas.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dataFacturas_CellBeginEdit);
		this.dataFacturas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataFacturas_CellContentClick);
		this.dataFacturas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dataFacturas_CellEndEdit);
		this.dataFacturas.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(dataFacturas_ColumnWidthChanged);
		this.dataFacturas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dataFacturas_RowStateChanged);
		this.dataFacturas.Scroll += new System.Windows.Forms.ScrollEventHandler(dataFacturas_Scroll);
		this.txtFactura.HeaderText = "Factura";
		this.txtFactura.Name = "txtFactura";
		this.txtFecha.HeaderText = "Fecha";
		this.txtFecha.Name = "txtFecha";
		this.txtCliente.HeaderText = "Cliente";
		this.txtCliente.Name = "txtCliente";
		this.txtTotal1.HeaderText = "Total";
		this.txtTotal1.Name = "txtTotal1";
		this.txtPendiente1.HeaderText = "Pendiente";
		this.txtPendiente1.Name = "txtPendiente1";
		this.chkRet.HeaderText = "RET";
		this.chkRet.Name = "chkRet";
		this.chkDet.HeaderText = "DET";
		this.chkDet.Name = "chkDet";
		this.txtMontoEnCuenta.HeaderText = "Monto En Cuenta";
		this.txtMontoEnCuenta.Name = "txtMontoEnCuenta";
		this.txtRet.HeaderText = "Retencion";
		this.txtRet.Name = "txtRet";
		this.txtDet.HeaderText = "Detraccion";
		this.txtDet.Name = "txtDet";
		this.txtTotal2.HeaderText = "TOTAL";
		this.txtTotal2.Name = "txtTotal2";
		this.txtPendiente2.HeaderText = "PENDIENTE";
		this.txtPendiente2.Name = "txtPendiente2";
		this.codigoFactura.HeaderText = "Codigo Factura";
		this.codigoFactura.Name = "codigoFactura";
		this.codigoFactura.Visible = false;
		this.txtTipoPago.HeaderText = "txtTipoPago";
		this.txtTipoPago.Name = "txtTipoPago";
		this.txtTipoPago.Visible = false;
		this.txtMoneda.Location = new System.Drawing.Point(385, 179);
		this.txtMoneda.Name = "txtMoneda";
		this.txtMoneda.ReadOnly = true;
		this.txtMoneda.Size = new System.Drawing.Size(120, 20);
		this.txtMoneda.TabIndex = 3;
		this.txtMoneda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(382, 163);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(46, 13);
		this.label2.TabIndex = 27;
		this.label2.Text = "Moneda";
		this.txtMontoPendiente.Location = new System.Drawing.Point(385, 225);
		this.txtMontoPendiente.Name = "txtMontoPendiente";
		this.txtMontoPendiente.ReadOnly = true;
		this.txtMontoPendiente.Size = new System.Drawing.Size(120, 20);
		this.txtMontoPendiente.TabIndex = 2;
		this.txtMontoPendiente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(382, 209);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "Monto Pend.";
		this.groupBox2.Controls.Add(this.txtDocRef);
		this.groupBox2.Controls.Add(this.txtNumero);
		this.groupBox2.Controls.Add(this.txtSerie);
		this.groupBox2.Controls.Add(this.txtMoneda);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.txtMontoPendiente);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.cboNumCta);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.cboBanco);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.cboTarjeta);
		this.groupBox2.Controls.Add(this.dtpFecha);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.txtObservacion);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.txtMontoPago);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtCheque);
		this.groupBox2.Controls.Add(this.txtOperacion);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.cmbMetodoPago);
		this.groupBox2.Controls.Add(this.cmbMoneda);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.txtTipoCambio);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Location = new System.Drawing.Point(151, 223);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(519, 333);
		this.groupBox2.TabIndex = 24;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Datos de Pago";
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.txtDocRef.Location = new System.Drawing.Point(88, 16);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(29, 20);
		this.txtDocRef.TabIndex = 90;
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.txtDocRef.Leave += new System.EventHandler(txtDocRef_Leave);
		this.txtNumero.Location = new System.Drawing.Point(180, 16);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 91;
		this.txtNumero.Visible = false;
		this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumero_KeyPress);
		this.txtNumero.Leave += new System.EventHandler(txtNumero_Leave);
		this.txtSerie.Location = new System.Drawing.Point(123, 16);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(51, 20);
		this.txtSerie.TabIndex = 89;
		this.txtSerie.Visible = false;
		this.txtSerie.TextChanged += new System.EventHandler(txtSerie_TextChanged);
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(12, 152);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(41, 13);
		this.label14.TabIndex = 82;
		this.label14.Text = "N° Cta:";
		this.cboNumCta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboNumCta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboNumCta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboNumCta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboNumCta.FormattingEnabled = true;
		this.cboNumCta.Location = new System.Drawing.Point(88, 148);
		this.cboNumCta.Name = "cboNumCta";
		this.cboNumCta.Size = new System.Drawing.Size(259, 21);
		this.cboNumCta.TabIndex = 81;
		this.superValidator1.SetValidator1(this.cboNumCta, this.customValidator5);
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(12, 124);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(41, 13);
		this.label13.TabIndex = 80;
		this.label13.Text = "Banco:";
		this.cboBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboBanco.FormattingEnabled = true;
		this.cboBanco.Location = new System.Drawing.Point(88, 121);
		this.cboBanco.Name = "cboBanco";
		this.cboBanco.Size = new System.Drawing.Size(259, 21);
		this.cboBanco.TabIndex = 79;
		this.superValidator1.SetValidator1(this.cboBanco, this.customValidator4);
		this.cboBanco.SelectionChangeCommitted += new System.EventHandler(cboBanco_SelectionChangeCommitted);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(12, 98);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(43, 13);
		this.label12.TabIndex = 78;
		this.label12.Text = "Tarjeta:";
		this.cboTarjeta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboTarjeta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboTarjeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboTarjeta.FormattingEnabled = true;
		this.cboTarjeta.Location = new System.Drawing.Point(88, 95);
		this.cboTarjeta.Name = "cboTarjeta";
		this.cboTarjeta.Size = new System.Drawing.Size(259, 21);
		this.cboTarjeta.TabIndex = 77;
		this.superValidator1.SetValidator1(this.cboTarjeta, this.customValidator3);
		this.dtpFecha.Checked = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(88, 255);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(93, 20);
		this.dtpFecha.TabIndex = 10;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(12, 284);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(67, 13);
		this.label11.TabIndex = 37;
		this.label11.Text = "Observación";
		this.txtObservacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtObservacion.Location = new System.Drawing.Point(88, 285);
		this.txtObservacion.Multiline = true;
		this.txtObservacion.Name = "txtObservacion";
		this.txtObservacion.Size = new System.Drawing.Size(259, 37);
		this.txtObservacion.TabIndex = 11;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(12, 261);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(37, 13);
		this.label10.TabIndex = 35;
		this.label10.Text = "Fecha";
		this.txtMontoPago.Enabled = false;
		this.txtMontoPago.Location = new System.Drawing.Point(88, 229);
		this.txtMontoPago.Name = "txtMontoPago";
		this.txtMontoPago.Size = new System.Drawing.Size(259, 20);
		this.txtMontoPago.TabIndex = 9;
		this.txtMontoPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtMontoPago, this.customValidator1);
		this.txtMontoPago.TextChanged += new System.EventHandler(txtMontoPago_TextChanged);
		this.txtMontoPago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMontoPago_KeyPress);
		this.txtMontoPago.KeyUp += new System.Windows.Forms.KeyEventHandler(txtMontoPago_KeyUp);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(12, 232);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(37, 13);
		this.label9.TabIndex = 33;
		this.label9.Text = "Monto";
		this.txtCheque.Location = new System.Drawing.Point(88, 201);
		this.txtCheque.Name = "txtCheque";
		this.txtCheque.Size = new System.Drawing.Size(259, 20);
		this.txtCheque.TabIndex = 8;
		this.superValidator1.SetValidator1(this.txtCheque, this.customValidator6);
		this.txtCheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCheque_KeyPress);
		this.txtOperacion.Location = new System.Drawing.Point(88, 175);
		this.txtOperacion.Name = "txtOperacion";
		this.txtOperacion.Size = new System.Drawing.Size(259, 20);
		this.txtOperacion.TabIndex = 7;
		this.superValidator1.SetValidator1(this.txtOperacion, this.customValidator7);
		this.txtOperacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtOperacion_KeyPress);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(12, 204);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(59, 13);
		this.label8.TabIndex = 30;
		this.label8.Text = "N° Cheque";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(12, 178);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(71, 13);
		this.label7.TabIndex = 29;
		this.label7.Text = "N° Operación";
		this.cmbMetodoPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbMetodoPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMetodoPago.FormattingEnabled = true;
		this.cmbMetodoPago.Location = new System.Drawing.Point(88, 69);
		this.cmbMetodoPago.Name = "cmbMetodoPago";
		this.cmbMetodoPago.Size = new System.Drawing.Size(259, 20);
		this.cmbMetodoPago.TabIndex = 6;
		this.cmbMetodoPago.SelectionChangeCommitted += new System.EventHandler(cmbMetodoPago_SelectionChangeCommitted);
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(88, 42);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(118, 20);
		this.cmbMoneda.TabIndex = 4;
		this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(cmbMoneda_SelectionChangeCommitted);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 71);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(70, 13);
		this.label6.TabIndex = 26;
		this.label6.Text = "Tipo de pago";
		this.txtTipoCambio.Location = new System.Drawing.Point(264, 41);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(83, 20);
		this.txtTipoCambio.TabIndex = 5;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(231, 44);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(27, 13);
		this.label4.TabIndex = 24;
		this.label4.Text = "T.C.";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 44);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 23;
		this.label5.Text = "Moneda";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "DeleteRed.png");
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator5.ErrorMessage = "Seleccione la cuenta.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator4.ErrorMessage = "Seleccione el banco.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator3.ErrorMessage = "Seleccione la tarjeta.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese Monto.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue_1);
		this.customValidator6.ErrorMessage = "Ingrese el número de cheque.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator7.ErrorMessage = "Ingrese el número de operación.";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimir.ImageKey = "document-print.png";
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(-71, 616);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(72, 33);
		this.btnImprimir.TabIndex = 25;
		this.btnImprimir.Text = "Imprimir";
		this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(274, 562);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(82, 32);
		this.btnAceptar.TabIndex = 12;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageIndex = 7;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(376, 562);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(82, 32);
		this.btnCancelar.TabIndex = 13;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.txtSumaPendiente2.Enabled = false;
		this.txtSumaPendiente2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSumaPendiente2.Location = new System.Drawing.Point(760, 194);
		this.txtSumaPendiente2.Name = "txtSumaPendiente2";
		this.txtSumaPendiente2.Size = new System.Drawing.Size(68, 18);
		this.txtSumaPendiente2.TabIndex = 26;
		this.txtSumaTotal2.AccessibleDescription = "-";
		this.txtSumaTotal2.Enabled = false;
		this.txtSumaTotal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSumaTotal2.Location = new System.Drawing.Point(692, 194);
		this.txtSumaTotal2.Name = "txtSumaTotal2";
		this.txtSumaTotal2.Size = new System.Drawing.Size(68, 18);
		this.txtSumaTotal2.TabIndex = 26;
		this.txtSumaRetencion.AccessibleDescription = "-";
		this.txtSumaRetencion.Enabled = false;
		this.txtSumaRetencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSumaRetencion.Location = new System.Drawing.Point(556, 194);
		this.txtSumaRetencion.Name = "txtSumaRetencion";
		this.txtSumaRetencion.Size = new System.Drawing.Size(68, 18);
		this.txtSumaRetencion.TabIndex = 26;
		this.txtSumaDetraccion.AccessibleDescription = "-";
		this.txtSumaDetraccion.Enabled = false;
		this.txtSumaDetraccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSumaDetraccion.Location = new System.Drawing.Point(624, 194);
		this.txtSumaDetraccion.Name = "txtSumaDetraccion";
		this.txtSumaDetraccion.Size = new System.Drawing.Size(68, 18);
		this.txtSumaDetraccion.TabIndex = 26;
		this.txtSumaPendiente1.Enabled = false;
		this.txtSumaPendiente1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSumaPendiente1.Location = new System.Drawing.Point(365, 194);
		this.txtSumaPendiente1.Name = "txtSumaPendiente1";
		this.txtSumaPendiente1.Size = new System.Drawing.Size(68, 18);
		this.txtSumaPendiente1.TabIndex = 26;
		this.txtSumaTotal1.Enabled = false;
		this.txtSumaTotal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSumaTotal1.Location = new System.Drawing.Point(297, 194);
		this.txtSumaTotal1.Name = "txtSumaTotal1";
		this.txtSumaTotal1.Size = new System.Drawing.Size(68, 18);
		this.txtSumaTotal1.TabIndex = 26;
		this.txtSumaMontoEnCuenta.AccessibleDescription = "-";
		this.txtSumaMontoEnCuenta.Enabled = false;
		this.txtSumaMontoEnCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSumaMontoEnCuenta.Location = new System.Drawing.Point(488, 194);
		this.txtSumaMontoEnCuenta.Name = "txtSumaMontoEnCuenta";
		this.txtSumaMontoEnCuenta.Size = new System.Drawing.Size(68, 18);
		this.txtSumaMontoEnCuenta.TabIndex = 27;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(834, 620);
		base.Controls.Add(this.txtSumaMontoEnCuenta);
		base.Controls.Add(this.txtSumaTotal1);
		base.Controls.Add(this.txtSumaPendiente1);
		base.Controls.Add(this.txtSumaDetraccion);
		base.Controls.Add(this.txtSumaRetencion);
		base.Controls.Add(this.txtSumaTotal2);
		base.Controls.Add(this.txtSumaPendiente2);
		base.Controls.Add(this.btnImprimir);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCancelarCobroMultiple";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Cancelar Pago";
		base.Load += new System.EventHandler(frmCancelarCobroMultiple_Load);
		base.Shown += new System.EventHandler(frmCancelarPagoMultiple_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataFacturas).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
