using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmCancelarPago : Office2007Form
{
	private clsValidar ok = new clsValidar();

	private clsAdmFactura Admfac = new clsAdmFactura();

	private clsFactura fac = new clsFactura();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	private clsNotaCredito notaCC = new clsNotaCredito();

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

	private clsNotaCredito ncredito = new clsNotaCredito();

	private clsAdmNotaCredito AdmNotaC = new clsAdmNotaCredito();

	private clsAdmCtaCte AdmCtaCte = new clsAdmCtaCte();

	private clsCtaCte CtaCte = new clsCtaCte();

	private clsNotaCredito notaC = new clsNotaCredito();

	private clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

	public int CodNotaC = 0;

	public string CodNota;

	public int CodLetra;

	public int tipo;

	public int tip = 0;

	public int pagoventa = 0;

	private bool tipopago;

	public int Procede = 0;

	public int mon = 0;

	public decimal Monto = default(decimal);

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

	public int montoPag = 1;

	public DateTime fechaDocumento = DateTime.Now;

	public bool xgenerar = false;

	private clsAdmCuota AdmCuoPreBan = new clsAdmCuota();

	private clsCuota cuoPreBan = new clsCuota();

	public int codCuota = 0;

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	public int tipocaja = 0;

	public bool continua_pago = false;

	private bool creoCorrelativo = false;

	public string vieneDe = "";

	public double det_ret = 0.0;

	public double monto_en_cuenta = 0.0;

	public string band_det_ret = "NAD";

	public int opcionSuma;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private Label label1;

	private GroupBox groupBox2;

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

	private TextBox txtParcial;

	private ComboBox cbovendedor;

	private TextBox txtMonedaCta;

	private Label label14;

	private Label label13;

	private Label label12;

	public TextBox txtMontoPendiente;

	public TextBox txtDocumento;

	public TextBox txtMontoPago;

	public ComboBox cmbMoneda;

	public TextBox txtTipoCambio;

	public TextBox txtMoneda;

	public ComboBox cboNumCta;

	public ComboBox cboBanco;

	public ComboBox cboTarjeta;

	private Button btnCancelar;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CustomValidator customValidator5;

	private CustomValidator customValidator6;

	private CustomValidator customValidator7;

	public TextBox txtMora;

	private Label lbMora;

	private TextBox txtDocRef;

	private TextBox txtNumero;

	public TextBox txtSerie;

	private Label label16;

	private TextBox txtNc;

	private Label label15;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	public DateTimePicker dtpFecha;

	private Button btnImprimir;

	public ComboBox cmbMetodoPago;

	public bool ventaRecibida { get; set; }

	public bool ventana_cobro { get; set; }

	public bool caja_aperturada { get; set; }

	public frmCancelarPago()
	{
		InitializeComponent();
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
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
			cboBanco.DataSource = AdmCtaCte.ListaBancoxMoneda(Convert.ToInt32(cmbMoneda.SelectedValue));
			cboBanco.DisplayMember = "descripcion";
			cboBanco.ValueMember = "codbanco";
			cboBanco.SelectedIndex = -1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void muestra_botones(bool activo)
	{
		label16.Visible = activo;
		txtSerie.Visible = activo;
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

	private void frmCancelarPago_Load(object sender, EventArgs e)
	{
		ventana_cobro = true;
		ventaRecibida = false;
		caja_aperturada = true;
		cargaMoneda();
		CargarBancos();
		CargarTarjetas();
		cboTarjeta.SelectedIndex = -1;
		cboBanco.SelectedIndex = -1;
		txtMora.Text = "0.00";
		if (tipo == 100)
		{
			CargaNotaCredito();
			Text = "DEVOLVER PAGO";
		}
		if (tipo == 10)
		{
			tc = mdi_Menu.clstc;
			if (tc != null)
			{
				txtTipoCambio.Text = tc.Venta.ToString();
			}
			else
			{
				txtTipoCambio.Text = "";
				txtTipoCambio.ReadOnly = false;
			}
		}
		if (tipo == 1)
		{
			CargaFactura();
			txtTipoCambio.Enabled = true;
			txtTipoCambio.ReadOnly = false;
			Text = "CANCELAR PAGO";
			muestra_botones(activo: true);
			posiciona_textbox();
		}
		else if (tipo == 2)
		{
			CargaLetra();
		}
		else if (tipo == 3)
		{
			CargaNotaSalida();
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
		else if (tipo == 5)
		{
			CargaCuota();
			txtTipoCambio.Enabled = true;
			txtTipoCambio.ReadOnly = false;
			Text = "CANCELAR PAGO";
			muestra_botones(activo: true);
			posiciona_textbox();
		}
		CargaMetodosPagos();
		cmbMetodoPago_SelectionChangeCommitted(cmbMetodoPago, null);
		Mon = AdmMoned.CargaMoneda(mon);
		if (tipo == 1 || tipo == 2 || tipo == 5)
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
		cmbMoneda.MouseWheel += cmbMoneda_MouseWheel;
	}

	private void CargaMetodosPagos()
	{
		cmbMetodoPago.DataSource = admMPago.CargaMetodoPagos();
		cmbMetodoPago.DisplayMember = "descripcion";
		cmbMetodoPago.ValueMember = "codMetodoPago";
	}

	private void CargaVendedores()
	{
		cbovendedor.DataSource = AdmVen.MuestraVendedoresDestaque();
		cbovendedor.DisplayMember = "apellido";
		cbovendedor.ValueMember = "codVendedor";
		cbovendedor.SelectedIndex = 0;
	}

	private void CargaNotaCredito()
	{
		try
		{
			ncredito = AdmNotaC.CargaNotaCredito(Convert.ToInt32(CodNota));
			if (ncredito != null)
			{
				txtDocumento.Text = ncredito.SiglaDocumento + "-" + ncredito.Serie + "-" + ncredito.DocumentoNotaCredito.PadLeft(8, '0');
				mon = ncredito.Moneda;
				fechaDocumento = ncredito.FechaIngreso;
				Mon = AdmMoned.CargaMoneda(mon);
				txtMoneda.Text = Mon.SDescripcion;
				txtMontoPendiente.Text = $"{ncredito.Pendiente.ToString():#,##0.00}";
				txtMontoPago.Text = $"{ncredito.Pendiente.ToString():#,##0.00}";
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaFactura()
	{
		try
		{
			fac = Admfac.CargaFactura(Convert.ToInt32(CodNota));
			if (fac != null)
			{
				txtDocumento.Text = fac.DocumentoFactura;
				mon = fac.Moneda;
				fechaDocumento = fac.FechaIngreso;
				txtMoneda.Text = Mon.SDescripcion;
				txtMontoPendiente.Text = $"{fac.Pendiente.ToString():#,##0.00}";
				txtMontoPago.Text = $"{fac.Pendiente.ToString():#,##0.00}";
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
				if (letra.CodNota > 0)
				{
					txtDocumento.Text = letra.DocumentoReferencia;
				}
				else
				{
					txtDocumento.Text = letra.NumDocumento;
				}
				if (letra.CodMoneda == 1)
				{
					txtMoneda.Text = "NUEVOS SOLES";
				}
				else
				{
					txtMoneda.Text = "DOLARES";
				}
				cmbMoneda.SelectedValue = letra.CodMoneda;
				mon = letra.CodMoneda;
				Mon = AdmMoned.CargaMoneda(mon);
				fechaDocumento = letra.FechaEmision;
				txtMontoPendiente.Text = $"{letra.MontoPendiente:#,##0.00}";
				txtMontoPago.Text = $"{letra.MontoPendiente:#,##0.00}";
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
			if (tipo != 3)
			{
				venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodNota));
			}
			if (Application.OpenForms["frmCobros"] != null && vieneDe == "frmCobros")
			{
				venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodNota));
			}
			Mon = AdmMoned.CargaMoneda(venta.Moneda);
			if (venta != null)
			{
				mon = venta.Moneda;
				if (venta.CodTipoDocumento == 1)
				{
					txtDocumento.Text = "B" + venta.Serie + " " + venta.NumDoc.ToString().PadLeft(8, '0');
				}
				else if (venta.CodTipoDocumento == 2)
				{
					txtDocumento.Text = "F" + venta.Serie + " " + venta.NumDoc.ToString().PadLeft(8, '0');
				}
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
				fechaDocumento = venta.FechaSalida;
				cmbMoneda.SelectedValue = venta.Moneda;
				txtMontoPendiente.Text = $"{venta.Pendiente:#,##0.00}";
				txtMontoPago.Text = $"{venta.Pendiente:#,##0.00}";
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
		if (!superValidator1.Validate())
		{
			return;
		}
		if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 12)
		{
			DialogResult validaPago = MessageBox.Show("Esta seguro de cobrar con esta método de pago?", "Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (validaPago != DialogResult.Yes)
			{
				MessageBox.Show("Operación cancelada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		if (VentComp != 2 && tipo != 1)
		{
			if (venta.CodFacturaVenta == null)
			{
				if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 12)
				{
					venta.Cancelado = 0;
				}
				if (!AdmVenta.insertComprobante(venta))
				{
					if ((tipo == 3 || tipo == 4) && Convert.ToDouble(txtMontoPendiente.Text) != 0.0)
					{
						ventana_cobro = false;
					}
					MessageBox.Show("EL PAGO NO SE HA REGISTRADO POR QUE LA VENTA NO SE GUARDO DE MANERA CORRECTA", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Close();
					Dispose();
					return;
				}
			}
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
			if (venta == null)
			{
				MessageBox.Show("No Se Guardo La Venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (venta.CodTipoDocumento == 1)
		{
			txtDocumento.Text = "B" + venta.Serie + " " + venta.NumDoc.ToString().PadLeft(8, '0');
		}
		else if (venta.CodTipoDocumento == 2)
		{
			txtDocumento.Text = "F" + venta.Serie + " " + venta.NumDoc.ToString().PadLeft(8, '0');
		}
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
		fechaDocumento = venta.FechaSalida;
		VerificaSaldoCaja();
		if (caja_aperturada)
		{
			Pag.BanderaRetDet = band_det_ret;
			Pag.RetDet = Convert.ToDecimal(det_ret);
			Pag.MontoEnCuenta = Convert.ToDecimal(monto_en_cuenta);
			Pag.OpcionSuma = opcionSuma;
			if (tipo == 100)
			{
				montoPag = 1;
				Pag.CodLetra = letra.CodLetra;
				if (letra.CodLetra > 0)
				{
					Pag.CodNota = AdmLetra.GetCodigoFactura(letra.CodNota).ToString();
				}
				else
				{
					Pag.CodNota = ncredito.CodNotaCredito.ToString();
				}
				Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue);
				Pag.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				Pag.Tipo = tipopago;
				Pag.IngresoEgreso = false;
				if (txtTipoCambio.Text == "")
				{
					Pag.TipoCambio = 0m;
				}
				else
				{
					Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
				}
				Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
				Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
				Pag.Vuelto = 0m;
				Pag.NOperacion = txtOperacion.Text;
				Pag.NCheque = txtCheque.Text;
				Pag.FechaPago = dtpFecha.Value;
				Pag.Observacion = txtObservacion.Text;
				Pag.CodUser = frmLogin.iCodUser;
				Pag.CodAlmacen = venta.CodAlmacen;
				Pag.CodSerie = CodSerie;
				Pag.Serie = txtSerie.Text;
				Pag.NumDoc = txtDocumento.Text;
				Pag.CodSucursal = frmLogin.iCodSucursal;
				Pag.CodDoc = CodDocumento;
				Pag.codCtaCte = Convert.ToInt32(cboNumCta.SelectedValue);
				Pag.CodBanco = Convert.ToInt32(cboBanco.SelectedValue);
				Pag.CodTarjeta = Convert.ToInt32(cboTarjeta.SelectedValue);
				decimal montoNC = default(decimal);
				decimal montpen = Convert.ToDecimal(txtMontoPendiente.Text);
				montoNC = Convert.ToDecimal(txtMontoPago.Text);
				if (Convert.ToDecimal(txtMontoPago.Text) > montpen)
				{
					montoNC = montpen;
				}
				if (Caja.Codcaja > 0)
				{
					Pag.Codcaja = Caja.Codcaja;
				}
				else
				{
					Pag.Codcaja = 0;
				}
				if (Pag.CodMoneda == 2)
				{
					Monto = Convert.ToDecimal(Pag.MontoCobrado) * Convert.ToDecimal(tc.Venta);
				}
				else
				{
					Monto = Convert.ToDecimal(Pag.MontoCobrado);
				}
				if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
				{
					CtaCte = AdmCtaCte.CargaTipoCuenta(Convert.ToInt32(cboNumCta.SelectedValue.ToString()), venta.CodAlmacen);
					if (CtaCte != null)
					{
						Caja.TotalDisponible = CtaCte.saldo;
					}
					else
					{
						Caja.TotalDisponible = 0m;
					}
				}
				if (Caja.TotalDisponible >= Convert.ToDecimal(Monto))
				{
					if (Admpag.insert(Pag))
					{
						MessageBox.Show("Los datos se guardaron correctamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Deshabilita_botones(Estado: false);
						btnImprimir.Visible = true;
					}
				}
				else
				{
					MessageBox.Show("El Monto a pagar excede al Saldo Disponible", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			if (tipo == 5)
			{
				montoPag = 1;
				Pag.CodNota = "0";
				Pag.CodCuotaPreBan = cuoPreBan.CodCuotaPrestamo;
				Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue);
				Pag.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				Pag.Tipo = tipopago;
				Pag.IngresoEgreso = false;
				if (txtTipoCambio.Text == "")
				{
					Pag.TipoCambio = 0m;
				}
				else
				{
					Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
				}
				Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
				Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
				Pag.Vuelto = 0m;
				Pag.NOperacion = txtOperacion.Text;
				Pag.NCheque = txtCheque.Text;
				Pag.FechaPago = dtpFecha.Value;
				Pag.Observacion = txtObservacion.Text;
				Pag.CodUser = frmLogin.iCodUser;
				Pag.CodAlmacen = venta.CodAlmacen;
				Pag.CodSerie = CodSerie;
				Pag.Serie = txtSerie.Text;
				Pag.NumDoc = txtNumero.Text;
				Pag.CodSucursal = frmLogin.iCodSucursal;
				Pag.CodDoc = CodDocumento;
				Pag.codCtaCte = Convert.ToInt32(cboNumCta.SelectedValue);
				Pag.CodBanco = Convert.ToInt32(cboBanco.SelectedValue);
				Pag.CodTarjeta = Convert.ToInt32(cboTarjeta.SelectedValue);
				Pag.Mora = Convert.ToDecimal(txtMora.Text);
				decimal montoNC2 = default(decimal);
				decimal montpen2 = Convert.ToDecimal(txtMontoPendiente.Text);
				montoNC2 = Convert.ToDecimal(txtMontoPago.Text);
				if (Convert.ToDecimal(txtMontoPago.Text) > montpen2)
				{
					montoNC2 = montpen2;
				}
				if (Caja.Codcaja > 0)
				{
					Pag.Codcaja = Caja.Codcaja;
				}
				else
				{
					Pag.Codcaja = 0;
				}
				if (Admpag.insert(Pag))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Deshabilita_botones(Estado: false);
				}
			}
			else
			{
				if (!(dtpFecha.Value.Date >= fechaDocumento.Date))
				{
					return;
				}
				if (tipo == 1 || tipo == 2)
				{
					montoPag = 1;
					Pag.CodLetra = letra.CodLetra;
					if (letra.CodLetra > 0)
					{
						Pag.CodNota = AdmLetra.GetCodigoFactura(letra.CodNota).ToString();
					}
					else
					{
						Pag.CodNota = fac.CodFactura.ToString();
					}
					Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue);
					Pag.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					Pag.Tipo = tipopago;
					Pag.IngresoEgreso = false;
					if (txtTipoCambio.Text == "")
					{
						Pag.TipoCambio = 0m;
					}
					else
					{
						Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
					}
					Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
					Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
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
					Pag.CodSucursal = frmLogin.iCodSucursal;
					Pag.CodDoc = CodDocumento;
					Pag.codCtaCte = Convert.ToInt32(cboNumCta.SelectedValue);
					Pag.CodBanco = Convert.ToInt32(cboBanco.SelectedValue);
					Pag.CodTarjeta = Convert.ToInt32(cboTarjeta.SelectedValue);
					decimal montoNC3 = default(decimal);
					decimal montpen3 = Convert.ToDecimal(txtMontoPendiente.Text);
					montoNC3 = Convert.ToDecimal(txtMontoPago.Text);
					if (Convert.ToDecimal(txtMontoPago.Text) > montpen3)
					{
						montoNC3 = montpen3;
					}
					if (Caja.Codcaja > 0)
					{
						Pag.Codcaja = Caja.Codcaja;
					}
					else
					{
						Pag.Codcaja = 0;
					}
					if (Pag.CodMoneda == 2)
					{
						Monto = Convert.ToDecimal(Pag.MontoCobrado) * Convert.ToDecimal(tc.Venta);
					}
					else
					{
						Monto = Convert.ToDecimal(Pag.MontoCobrado);
					}
					if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
					{
						CtaCte = AdmCtaCte.CargaTipoCuenta(Convert.ToInt32(cboNumCta.SelectedValue.ToString()), frmLogin.iCodAlmacen);
						if (CtaCte != null)
						{
							Caja.TotalDisponible = CtaCte.saldo;
						}
						else
						{
							Caja.TotalDisponible = 0m;
						}
					}
					if (!Admpag.insert(Pag))
					{
						return;
					}
					MessageBox.Show("Los datos se guardaron correctamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					if (VentComp == 2)
					{
						if (notaS.CodNotaSalida != "")
						{
							notaS.DocumentoReferencia = Convert.ToInt32(CodNota);
							AdmVenta.ActualizaPendienteCredito(montoNC3, Convert.ToInt32(notaS.CodNotaSalida), notaS.CodAlmacen, 2);
							AdmNotaS.ActualizaNCreditoCompraSinAplicar(notaS);
						}
						else
						{
							clsAdmNotaCreditoCompra admNotaCC = new clsAdmNotaCreditoCompra();
							admNotaCC.actualizaFormaPago(notaCC.CodNotaCredito, 1);
						}
					}
					Deshabilita_botones(Estado: false);
					btnImprimir.Visible = true;
				}
				else
				{
					if (tipo != 3 && tipo != 4)
					{
						return;
					}
					DialogResult dlgResult = DialogResult.None;
					if (montoPag == 0 && Convert.ToDecimal(txtMontoPendiente.Text) != Convert.ToDecimal(txtMontoPago.Text))
					{
						dlgResult = MessageBox.Show("El monto Ingresado No es Total, Desea Ingresar Pago Parcial?", "Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dlgResult != DialogResult.Yes)
						{
							return;
						}
						montoPag = 1;
						Pag.CodNota = venta.CodFacturaVenta.ToString();
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
						Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
						Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
						if (Caja.Codcaja > 0)
						{
							Pag.Codcaja = Caja.Codcaja;
						}
						else
						{
							Pag.Codcaja = 0;
						}
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
						Pag.CodSucursal = venta.CodSucursal;
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
						else
						{
							Pag.Serie = txtSerie.Text;
							Pag.NumDoc = txtNumero.Text;
							Pag.Referencia = txtNc.Text;
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
								return;
							}
							Pagar();
							btnImprimir.Visible = false;
						}
						else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8)
						{
							if (txtOperacion.Text.Trim() == "" || cboTarjeta.Text == "" || txtMontoPago.Text == "")
							{
								MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								Pagar();
							}
						}
						else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
						{
							if (txtCheque.Text.Trim() == "" || cboBanco.Text == "" || txtOperacion.Text.Trim() == "" || txtMontoPago.Text == "")
							{
								MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								return;
							}
							Pagar();
							btnImprimir.Visible = false;
						}
						else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 10)
						{
							if (txtNc.Text.Trim() == "" || txtMontoPago.Text == "")
							{
								MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								return;
							}
							Pag.NotaCredito = 1;
							Pag.CodNotaCredito = Convert.ToInt32(notaC.CodNotaCredito);
							decimal montoNC4 = default(decimal);
							decimal montpen4 = Convert.ToDecimal(txtMontoPendiente.Text);
							montoNC4 = Convert.ToDecimal(txtMontoPago.Text);
							if (Convert.ToDecimal(txtMontoPago.Text) > montpen4)
							{
								montoNC4 = montpen4;
							}
							Pagar();
							if (VentComp != 1)
							{
							}
						}
						else
						{
							Pagar();
						}
						return;
					}
					Pag.CodNota = venta.CodFacturaVenta.ToString();
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
					Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
					Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
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
					else
					{
						Pag.Serie = txtSerie.Text;
						Pag.NumDoc = txtNumero.Text;
						Pag.Referencia = txtNc.Text;
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
							return;
						}
						Pagar();
						btnImprimir.Visible = false;
						if (Pag.CodTipoPago != 12)
						{
							Admpag.insertPagoPendiente(Pag);
						}
					}
					else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8)
					{
						if (txtOperacion.Text.Trim() == "" || cboTarjeta.Text == "" || txtMontoPago.Text == "")
						{
							MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							return;
						}
						Pagar();
						if (Pag.CodTipoPago != 12)
						{
							Admpag.insertPagoPendiente(Pag);
						}
					}
					else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
					{
						if (txtCheque.Text.Trim() == "" || cboBanco.Text == "" || txtOperacion.Text.Trim() == "" || txtMontoPago.Text == "")
						{
							MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							return;
						}
						Pagar();
						btnImprimir.Visible = false;
						if (Pag.CodTipoPago != 12)
						{
							Admpag.insertPagoPendiente(Pag);
						}
					}
					else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 10)
					{
						if (txtNc.Text.Trim() == "" || txtMontoPago.Text == "")
						{
							MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							return;
						}
						Pag.NotaCredito = 1;
						Pag.CodNotaCredito = Convert.ToInt32(notaC.CodNotaCredito);
						decimal montoNC5 = default(decimal);
						decimal montpen5 = Convert.ToDecimal(txtMontoPendiente.Text);
						montoNC5 = Convert.ToDecimal(txtMontoPago.Text);
						if (Convert.ToDecimal(txtMontoPago.Text) > montpen5)
						{
							montoNC5 = montpen5;
						}
						Pagar();
						if (VentComp == 1)
						{
							notaI.CodReferencia = Convert.ToInt32(CodNota);
							AdmVenta.ActualizaPendienteCredito(montoNC5, Convert.ToInt32(notaI.CodNotaIngreso), notaI.CodAlmacen, 1);
							AdmNotaI.ActualizaNCreditoVentaSinAplicar(notaI);
						}
						if (Pag.CodTipoPago != 12)
						{
							Admpag.insertPagoPendiente(Pag);
						}
					}
					else
					{
						Pagar();
						if (Pag.CodTipoPago != 12 && pagoventa == 0)
						{
							Admpag.insertPagoPendiente(Pag);
						}
					}
				}
			}
		}
		else
		{
			Close();
		}
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
			txtMontoPago.Text = p.MontoPagado.ToString();
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
		txtMontoPago.Enabled = Estado;
		dtpFecha.Enabled = Estado;
		btnAceptar.Enabled = Estado;
		btnCancelar.Enabled = !Estado;
		txtSerie.Enabled = Estado;
		txtNumero.Enabled = Estado;
		txtNumero.Visible = !Estado;
	}

	private void Pagar()
	{
		try
		{
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
			if (Admpag.insert(Pag))
			{
				MessageBox.Show("Pago Realizado Correctamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				if (tip == 3)
				{
				}
				cargaPago(Pag);
			}
			txtMontoPendiente.Text = Convert.ToString(Convert.ToDecimal(txtMontoPendiente.Text) - Convert.ToDecimal(txtMontoPago.Text));
			if (Convert.ToDouble(txtMontoPendiente.Text) != 0.0)
			{
				DialogResult d = MessageBox.Show("Desea pagar el restante?", "Aviso", MessageBoxButtons.YesNo);
				if (d == DialogResult.Yes)
				{
					Deshabilita_botones(Estado: true);
					txtMontoPago.Text = txtMontoPendiente.Text;
					continua_pago = true;
				}
				else
				{
					Deshabilita_botones(Estado: false);
					Close();
				}
			}
			else
			{
				Deshabilita_botones(Estado: false);
				ventaRecibida = true;
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
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
		if ((tipo == 3 || tipo == 4) && Convert.ToDouble(txtMontoPendiente.Text) != 0.0)
		{
			ventana_cobro = false;
		}
		Close();
		Dispose();
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

	private void CargaCuota()
	{
		try
		{
			cuoPreBan = AdmCuoPreBan.CargaCuota(codCuota);
			if (cuoPreBan != null)
			{
				txtDocumento.Text = cuoPreBan.CodCuotaPrestamo.ToString();
				mon = cuoPreBan.CodMoneda;
				fechaDocumento = cuoPreBan.FechaEmision;
				Mon = AdmMoned.CargaMoneda(mon);
				txtMoneda.Text = Mon.SDescripcion;
				txtMontoPendiente.Text = $"{cuoPreBan.MontoPendiente.ToString():#,##0.00}";
				txtMontoPago.Text = $"{cuoPreBan.MontoPendiente.ToString():#,##0.00}";
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

	private void frmCancelarPago_Shown(object sender, EventArgs e)
	{
		if (tipo != 1)
		{
			if (tipo == 2)
			{
				CargaLetra();
			}
			else if (tipo != 3 && tipo != 4)
			{
			}
		}
		if (tipo == 5)
		{
			sigl = "RP";
			valida_serie(sigl);
		}
		cmbMoneda.Focus();
	}

	private void txtMontoPago_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtMontoPago.Text != "")
			{
				if (!(txtMontoPago.Text != "0"))
				{
					return;
				}
				if (Convert.ToDouble(txtMontoPago.Text) >= Convert.ToDouble(txtMontoPendiente.Text))
				{
					if (tipo == 1 || tipo == 5)
					{
						txtParcial.Text = "PAGO TOTAL";
					}
					else
					{
						txtParcial.Text = "COBRO TOTAL";
					}
					tipopago = true;
				}
				else
				{
					if (tipo == 1 || tipo == 5)
					{
						txtParcial.Text = "PAGO PARCIAL";
					}
					else
					{
						txtParcial.Text = "COBRO PARCIAL";
					}
					tipopago = false;
				}
			}
			else
			{
				txtParcial.Text = "";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void txtMontoPago_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			double box_double = 0.0;
			double.TryParse(txtMontoPago.Text, out box_double);
			if (box_double > Convert.ToDouble(txtMontoPendiente.Text) && txtMontoPago.Text != "" && tipo != 10)
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
				txtNc.Text = "";
				txtOperacion.Text = "";
				txtOperacion.Enabled = false;
				txtCheque.Enabled = false;
				txtMontoPago.Enabled = true;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
			{
				if (tipo == 1 && Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6)
				{
					cboTarjeta.Enabled = false;
					cboBanco.Enabled = true;
					CargarBancos();
					cboTarjeta.SelectedIndex = -1;
					txtCheque.Text = "";
					txtNc.Text = "";
					txtOperacion.Text = "";
					txtOperacion.Enabled = true;
					txtCheque.Enabled = false;
					txtMontoPago.Enabled = true;
					cboNumCta.Enabled = false;
					cboNumCta.SelectedIndex = -1;
				}
				else
				{
					cboTarjeta.Enabled = false;
					cboBanco.Enabled = true;
					CargarBancos();
					cboTarjeta.SelectedIndex = -1;
					cboBanco.Focus();
					txtCheque.Text = "";
					txtOperacion.Text = "";
					txtNc.Text = "";
					txtOperacion.Enabled = true;
					txtCheque.Enabled = false;
					txtMontoPago.Enabled = true;
					cboNumCta.Enabled = false;
					cboNumCta.SelectedIndex = -1;
				}
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = true;
				cboBanco.Focus();
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtOperacion.Text = "";
				txtNc.Text = "";
				txtCheque.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = true;
				txtMontoPago.Enabled = true;
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
				txtNc.Text = "";
				txtCheque.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = false;
				txtMontoPago.Enabled = true;
				cboNumCta.Enabled = true;
				cboNumCta.SelectedIndex = -1;
			}
			else
			{
				if (Convert.ToInt32(cmbMetodoPago.SelectedValue) != 10)
				{
					return;
				}
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = false;
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtOperacion.Text = "";
				txtCheque.Text = "";
				txtNc.Text = "";
				txtOperacion.Enabled = false;
				txtCheque.Enabled = false;
				txtNc.Enabled = false;
				cboNumCta.Enabled = false;
				txtMontoPago.Enabled = false;
				cboNumCta.SelectedIndex = -1;
				if (Application.OpenForms["frmListaNCreditosSinAplicar"] != null)
				{
					Application.OpenForms["frmListaNCreditosSinAplicar"].Activate();
					return;
				}
				frmListaNCreditosSinAplicar form = new frmListaNCreditosSinAplicar();
				form.CodCliente = ((CodCliente == 0) ? venta.CodCliente : CodCliente);
				form.VentComp = VentComp;
				form.venta = venta;
				form.ShowDialog();
				if (VentComp == 1)
				{
					if (form.notaC.CodNotaCredito != null)
					{
						notaC = form.notaC;
						txtNc.Text = notaC.DocumentoNotaCredito;
						if (notaC.Pendiente >= Convert.ToDouble(txtMontoPendiente.Text))
						{
							txtMontoPago.Text = txtMontoPendiente.Text;
						}
						else
						{
							txtMontoPago.Text = notaC.Pendiente.ToString();
						}
					}
				}
				else
				{
					notaS = form.notaS;
					notaCC = form.notaCC;
					txtNc.Text = notaS.Docref;
					txtMontoPago.Text = notaS.Total.ToString();
				}
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
			cboNumCta.DataSource = AdmCtaCte.ListaCtaCtexBancoxMoneda(Convert.ToInt32(cboBanco.SelectedValue), Convert.ToInt32(cmbMoneda.SelectedValue));
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
			if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
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

	private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
	{
		VentaEnMoneda();
		CargarBancos();
		cboBanco.SelectedIndex = -1;
		cboNumCta.SelectedIndex = -1;
	}

	private void VentaEnMoneda()
	{
		decimal TipoCambio = default(decimal);
		TipoCambio = Convert.ToDecimal(txtTipoCambio.Text.Trim());
		if (mon == 1)
		{
			if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
			{
				txtMontoPendiente.Text = $"{Convert.ToDecimal(txtMontoPendiente.Text) / TipoCambio:#,##0.00}";
				txtMontoPago.Text = $"{Convert.ToDecimal(txtMontoPago.Text) / TipoCambio:#,##0.00}";
			}
			else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
			{
				txtMontoPendiente.Text = $"{Convert.ToDecimal(txtMontoPendiente.Text) / TipoCambio:#,##0.00}";
				txtMontoPago.Text = $"{Convert.ToDecimal(txtMontoPago.Text) / TipoCambio:#,##0.00}";
			}
		}
		else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
		{
			txtMontoPendiente.Text = $"{Convert.ToDecimal(txtMontoPendiente.Text) / TipoCambio:#,##0.00}";
			txtMontoPago.Text = $"{Convert.ToDecimal(txtMontoPago.Text) / TipoCambio:#,##0.00}";
		}
		else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
		{
			txtMontoPendiente.Text = $"{Convert.ToDecimal(txtMontoPendiente.Text) * TipoCambio:#,##0.00}";
			txtMontoPago.Text = $"{Convert.ToDecimal(txtMontoPago.Text) * TipoCambio:#,##0.00}";
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

	private void frmCancelarPago_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (tipo == 3 && Convert.ToDouble(txtMontoPendiente.Text) != 0.0)
		{
			ventana_cobro = false;
		}
	}

	private void customValidator2_ValidateValue_1(object sender, ValidateValueEventArgs e)
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

	private void txtMora_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar == '\r' && txtMora.Text != "" && Convert.ToDecimal(txtMora.Text) > 0m)
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtMora_Leave(object sender, EventArgs e)
	{
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			if (tip == 3)
			{
				if ((Convert.ToInt32(cmbMetodoPago.SelectedValue) == 5 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8) && Convert.ToDecimal(txtMontoPendiente.Text) == 0m)
				{
					frmVenta form = (frmVenta)Application.OpenForms["frmVenta"];
					form.xgenerar = true;
					form.btnImprimir_Click(sender, e);
					form.Close();
					Close();
				}
				else
				{
					CRImpresionPago rpt = new CRImpresionPago();
					frmRptImpresionPago frm = new frmRptImpresionPago();
					PrintOptions rptoption = rpt.PrintOptions;
					rpt.SetDataSource(ds.ReporteImpresionPago(Pag.CodPago, frmLogin.iCodAlmacen));
					frm.cRVImpresionPago.ReportSource = rpt;
					frm.Show();
				}
			}
			else if (tip == 0)
			{
				CRImpresionCobro rpt2 = new CRImpresionCobro();
				frmRptImpresionPago frm2 = new frmRptImpresionPago();
				rpt2.SetDataSource(ds.ReporteImpresionCobro(Pag.CodPago, frmLogin.iCodAlmacen));
				frm2.cRVImpresionPago.ReportSource = rpt2;
				frm2.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtOperacion_TextChanged(object sender, EventArgs e)
	{
	}

	private void cboTarjeta_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargarBancos();
	}

	private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void txtMora_TextChanged(object sender, EventArgs e)
	{
		if (txtMora.Text == "")
		{
			txtMora.Text = "0.00";
		}
	}

	private void cmbMoneda_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void cmbMoneda_MouseWheel(object sender, MouseEventArgs e)
	{
		((HandledMouseEventArgs)e).Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCancelarPago));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtMoneda = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtMontoPendiente = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtDocumento = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtNc = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtMora = new System.Windows.Forms.TextBox();
		this.lbMora = new System.Windows.Forms.Label();
		this.txtMonedaCta = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.cboNumCta = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.cboBanco = new System.Windows.Forms.ComboBox();
		this.label12 = new System.Windows.Forms.Label();
		this.cboTarjeta = new System.Windows.Forms.ComboBox();
		this.cbovendedor = new System.Windows.Forms.ComboBox();
		this.txtParcial = new System.Windows.Forms.TextBox();
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
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtMoneda);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtMontoPendiente);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtDocumento);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Enabled = false;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(375, 63);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos del documento";
		this.txtMoneda.Location = new System.Drawing.Point(253, 32);
		this.txtMoneda.Name = "txtMoneda";
		this.txtMoneda.ReadOnly = true;
		this.txtMoneda.Size = new System.Drawing.Size(100, 20);
		this.txtMoneda.TabIndex = 3;
		this.txtMoneda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(250, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(46, 13);
		this.label2.TabIndex = 27;
		this.label2.Text = "Moneda";
		this.txtMontoPendiente.Location = new System.Drawing.Point(147, 32);
		this.txtMontoPendiente.Name = "txtMontoPendiente";
		this.txtMontoPendiente.ReadOnly = true;
		this.txtMontoPendiente.Size = new System.Drawing.Size(100, 20);
		this.txtMontoPendiente.TabIndex = 2;
		this.txtMontoPendiente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(144, 16);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "Monto Pend.";
		this.txtDocumento.Location = new System.Drawing.Point(21, 32);
		this.txtDocumento.Name = "txtDocumento";
		this.txtDocumento.ReadOnly = true;
		this.txtDocumento.Size = new System.Drawing.Size(120, 20);
		this.txtDocumento.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(18, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 13);
		this.label1.TabIndex = 6;
		this.label1.Text = "N° Documento Ref.";
		this.groupBox2.Controls.Add(this.txtNc);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.txtDocRef);
		this.groupBox2.Controls.Add(this.txtNumero);
		this.groupBox2.Controls.Add(this.txtSerie);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.txtMora);
		this.groupBox2.Controls.Add(this.lbMora);
		this.groupBox2.Controls.Add(this.txtMonedaCta);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.cboNumCta);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.cboBanco);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.cboTarjeta);
		this.groupBox2.Controls.Add(this.cbovendedor);
		this.groupBox2.Controls.Add(this.txtParcial);
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
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 63);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(375, 381);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Datos de Pago";
		this.txtNc.Enabled = false;
		this.txtNc.Location = new System.Drawing.Point(95, 228);
		this.txtNc.Name = "txtNc";
		this.txtNc.Size = new System.Drawing.Size(259, 20);
		this.txtNc.TabIndex = 13;
		this.superValidator1.SetValidator1(this.txtNc, this.customValidator6);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(20, 235);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(69, 13);
		this.label15.TabIndex = 95;
		this.label15.Text = "N° N. Credito";
		this.txtDocRef.Location = new System.Drawing.Point(94, 15);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(29, 20);
		this.txtDocRef.TabIndex = 1;
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.txtDocRef.Leave += new System.EventHandler(txtDocRef_Leave);
		this.txtNumero.Location = new System.Drawing.Point(186, 15);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 3;
		this.txtNumero.Visible = false;
		this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumero_KeyPress);
		this.txtNumero.Leave += new System.EventHandler(txtNumero_Leave);
		this.txtSerie.Location = new System.Drawing.Point(129, 15);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(51, 20);
		this.txtSerie.TabIndex = 2;
		this.txtSerie.Visible = false;
		this.txtSerie.TextChanged += new System.EventHandler(txtSerie_TextChanged);
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(19, 18);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(34, 13);
		this.label16.TabIndex = 92;
		this.label16.Text = "Serie:";
		this.label16.Visible = false;
		this.txtMora.Location = new System.Drawing.Point(94, 309);
		this.txtMora.Name = "txtMora";
		this.txtMora.Size = new System.Drawing.Size(118, 20);
		this.txtMora.TabIndex = 18;
		this.txtMora.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtMora.TextChanged += new System.EventHandler(txtMora_TextChanged);
		this.txtMora.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMora_KeyPress);
		this.txtMora.Leave += new System.EventHandler(txtMora_Leave);
		this.lbMora.AutoSize = true;
		this.lbMora.Location = new System.Drawing.Point(18, 312);
		this.lbMora.Name = "lbMora";
		this.lbMora.Size = new System.Drawing.Size(31, 13);
		this.lbMora.TabIndex = 84;
		this.lbMora.Text = "Mora";
		this.txtMonedaCta.Location = new System.Drawing.Point(270, 148);
		this.txtMonedaCta.Name = "txtMonedaCta";
		this.txtMonedaCta.ReadOnly = true;
		this.txtMonedaCta.Size = new System.Drawing.Size(83, 20);
		this.txtMonedaCta.TabIndex = 10;
		this.txtMonedaCta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtMonedaCta.Visible = false;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(18, 151);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(41, 13);
		this.label14.TabIndex = 82;
		this.label14.Text = "N° Cta:";
		this.cboNumCta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboNumCta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboNumCta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboNumCta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboNumCta.FormattingEnabled = true;
		this.cboNumCta.Location = new System.Drawing.Point(94, 147);
		this.cboNumCta.Name = "cboNumCta";
		this.cboNumCta.Size = new System.Drawing.Size(170, 21);
		this.cboNumCta.TabIndex = 9;
		this.superValidator1.SetValidator1(this.cboNumCta, this.customValidator5);
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(18, 123);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(41, 13);
		this.label13.TabIndex = 80;
		this.label13.Text = "Banco:";
		this.cboBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboBanco.FormattingEnabled = true;
		this.cboBanco.Location = new System.Drawing.Point(94, 120);
		this.cboBanco.Name = "cboBanco";
		this.cboBanco.Size = new System.Drawing.Size(259, 21);
		this.cboBanco.TabIndex = 8;
		this.superValidator1.SetValidator1(this.cboBanco, this.customValidator4);
		this.cboBanco.SelectionChangeCommitted += new System.EventHandler(cboBanco_SelectionChangeCommitted);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(18, 97);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(43, 13);
		this.label12.TabIndex = 78;
		this.label12.Text = "Tarjeta:";
		this.cboTarjeta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboTarjeta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboTarjeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboTarjeta.FormattingEnabled = true;
		this.cboTarjeta.Location = new System.Drawing.Point(94, 94);
		this.cboTarjeta.Name = "cboTarjeta";
		this.cboTarjeta.Size = new System.Drawing.Size(259, 21);
		this.cboTarjeta.TabIndex = 7;
		this.superValidator1.SetValidator1(this.cboTarjeta, this.customValidator3);
		this.cboTarjeta.SelectionChangeCommitted += new System.EventHandler(cboTarjeta_SelectionChangeCommitted);
		this.cbovendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbovendedor.FormattingEnabled = true;
		this.cbovendedor.Location = new System.Drawing.Point(193, 281);
		this.cbovendedor.Name = "cbovendedor";
		this.cbovendedor.Size = new System.Drawing.Size(160, 20);
		this.cbovendedor.TabIndex = 17;
		this.cbovendedor.Text = "Cobrado Por";
		this.cbovendedor.Visible = false;
		this.txtParcial.Enabled = false;
		this.txtParcial.Location = new System.Drawing.Point(218, 255);
		this.txtParcial.Name = "txtParcial";
		this.txtParcial.ReadOnly = true;
		this.txtParcial.Size = new System.Drawing.Size(135, 20);
		this.txtParcial.TabIndex = 15;
		this.dtpFecha.Checked = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(94, 281);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(93, 20);
		this.dtpFecha.TabIndex = 16;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(18, 334);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(67, 13);
		this.label11.TabIndex = 37;
		this.label11.Text = "Observación";
		this.txtObservacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtObservacion.Location = new System.Drawing.Point(94, 335);
		this.txtObservacion.Multiline = true;
		this.txtObservacion.Name = "txtObservacion";
		this.txtObservacion.Size = new System.Drawing.Size(259, 37);
		this.txtObservacion.TabIndex = 19;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(18, 287);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(37, 13);
		this.label10.TabIndex = 35;
		this.label10.Text = "Fecha";
		this.txtMontoPago.Location = new System.Drawing.Point(94, 255);
		this.txtMontoPago.Name = "txtMontoPago";
		this.txtMontoPago.Size = new System.Drawing.Size(118, 20);
		this.txtMontoPago.TabIndex = 14;
		this.txtMontoPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtMontoPago, this.customValidator1);
		this.txtMontoPago.TextChanged += new System.EventHandler(txtMontoPago_TextChanged);
		this.txtMontoPago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMontoPago_KeyPress);
		this.txtMontoPago.KeyUp += new System.Windows.Forms.KeyEventHandler(txtMontoPago_KeyUp);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(18, 258);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(37, 13);
		this.label9.TabIndex = 33;
		this.label9.Text = "Monto";
		this.txtCheque.Location = new System.Drawing.Point(94, 200);
		this.txtCheque.Name = "txtCheque";
		this.txtCheque.Size = new System.Drawing.Size(259, 20);
		this.txtCheque.TabIndex = 12;
		this.superValidator1.SetValidator1(this.txtCheque, this.customValidator6);
		this.txtCheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCheque_KeyPress);
		this.txtOperacion.Location = new System.Drawing.Point(94, 174);
		this.txtOperacion.Name = "txtOperacion";
		this.txtOperacion.Size = new System.Drawing.Size(259, 20);
		this.txtOperacion.TabIndex = 11;
		this.superValidator1.SetValidator1(this.txtOperacion, this.customValidator7);
		this.txtOperacion.TextChanged += new System.EventHandler(txtOperacion_TextChanged);
		this.txtOperacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtOperacion_KeyPress);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(18, 203);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(59, 13);
		this.label8.TabIndex = 30;
		this.label8.Text = "N° Cheque";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(18, 177);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(71, 13);
		this.label7.TabIndex = 29;
		this.label7.Text = "N° Operación";
		this.cmbMetodoPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbMetodoPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMetodoPago.FormattingEnabled = true;
		this.cmbMetodoPago.Location = new System.Drawing.Point(94, 68);
		this.cmbMetodoPago.Name = "cmbMetodoPago";
		this.cmbMetodoPago.Size = new System.Drawing.Size(259, 20);
		this.cmbMetodoPago.TabIndex = 6;
		this.cmbMetodoPago.SelectedIndexChanged += new System.EventHandler(cmbMetodoPago_SelectedIndexChanged);
		this.cmbMetodoPago.SelectionChangeCommitted += new System.EventHandler(cmbMetodoPago_SelectionChangeCommitted);
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(94, 41);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(106, 20);
		this.cmbMoneda.TabIndex = 4;
		this.cmbMoneda.SelectedIndexChanged += new System.EventHandler(cmbMoneda_SelectedIndexChanged);
		this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(cmbMoneda_SelectionChangeCommitted);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(18, 70);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(70, 13);
		this.label6.TabIndex = 26;
		this.label6.Text = "Tipo de pago";
		this.txtTipoCambio.Location = new System.Drawing.Point(270, 40);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(83, 20);
		this.txtTipoCambio.TabIndex = 5;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtTipoCambio, this.customValidator2);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(237, 43);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(27, 13);
		this.label4.TabIndex = 24;
		this.label4.Text = "T.C.";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(18, 43);
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
		this.customValidator6.ErrorMessage = "Ingrese el número de cheque.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
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
		this.customValidator7.ErrorMessage = "Ingrese el número de operación.";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese Tipo de Cambio";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue_1);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAceptar.Image = (System.Drawing.Image)resources.GetObject("btnAceptar.Image");
		this.btnAceptar.Location = new System.Drawing.Point(147, 455);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(104, 35);
		this.btnAceptar.TabIndex = 3;
		this.btnAceptar.Text = "ACEPTAR";
		this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Image = (System.Drawing.Image)resources.GetObject("btnCancelar.Image");
		this.btnCancelar.Location = new System.Drawing.Point(257, 455);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(106, 35);
		this.btnCancelar.TabIndex = 5;
		this.btnCancelar.Text = "CANCELAR";
		this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimir.Image = (System.Drawing.Image)resources.GetObject("btnImprimir.Image");
		this.btnImprimir.Location = new System.Drawing.Point(0, 455);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(118, 35);
		this.btnImprimir.TabIndex = 6;
		this.btnImprimir.Text = "IMPRIMIR";
		this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(375, 502);
		base.Controls.Add(this.btnImprimir);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCancelarPago";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Cancelar Pago";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmCancelarPago_FormClosing);
		base.Load += new System.EventHandler(frmCancelarPago_Load);
		base.Shown += new System.EventHandler(frmCancelarPago_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
