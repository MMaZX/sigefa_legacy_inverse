using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.SunatFacElec;

namespace SIGEFA.Formularios;

public class frmNotadeCredito : Office2007Form
{
	private clsNotasCreditoDebitoVenta ds = new clsNotasCreditoDebitoVenta();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsProveedor prov = new clsProveedor();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsNotaIngreso nota = new clsNotaIngreso();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmAutorizado AdmAut = new clsAdmAutorizado();

	private clsAutorizado aut = new clsAutorizado();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsValidar ok = new clsValidar();

	private clsDetalleNotaIngreso detaSelec = new clsDetalleNotaIngreso();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmSerie AdmSerie = new clsAdmSerie();

	private clsSerie ser = new clsSerie();

	private clsProducto pro = new clsProducto();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsNotaCredito notc = new clsNotaCredito();

	private clsAdmNotaCredito AdmFact = new clsAdmNotaCredito();

	private clsAdmDocumentoIdentidad AdmDocumentoIdentidad = new clsAdmDocumentoIdentidad();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsPago pag = new clsPago();

	private clsEmpresa empre = new clsEmpresa();

	private clsAdmEmpresa admempre = new clsAdmEmpresa();

	private clsAdmAlmacen admAlmacen = new clsAdmAlmacen();

	private int codActualNC = 0;

	private List<ProdTieneDespacho> prodsBandDespacho = new List<ProdTieneDespacho>();

	public List<int> config = new List<int>();

	public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();

	public List<clsDetalleNotaCredito> detalleNotaCredito = new List<clsDetalleNotaCredito>();

	public string CodNota;

	public int CodNotaS;

	public int CodNC;

	public int CodTransaccion;

	public int CodProveedor;

	public int CodCliente;

	public int CodDocumento;

	public int CodOrdenCompra;

	public int CodAutorizado;

	private bool Validacion = true;

	public int Proceso = 0;

	public int Tipo;

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private decimal cantprod = default(decimal);

	private decimal precprod = default(decimal);

	private TextBox txtedit = new TextBox();

	private List<decimal> cantpr = new List<decimal>();

	private List<decimal> cantprec = new List<decimal>();

	public int CodSerie;

	public int CodSerieG = 0;

	public int numG = 0;

	public int manual = 0;

	private DataTable dtPagos = new DataTable();

	public decimal montogratuitas;

	public decimal montogravadas;

	public decimal montoexoneradas;

	public decimal montoinafectas = default(decimal);

	private Facturacion con = new Facturacion();

	private clsAdmPago admPago = new clsAdmPago();

	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsAdmSerie admSerie = new clsAdmSerie();

	private clsAdmTransferencia admTransferencia = new clsAdmTransferencia();

	private clsAdmDespacho admdespacho = new clsAdmDespacho();

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private bool bandera = true;

	private int codproducto_error = 0;

	private List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	private List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	internal clsUsuario usuario_click = null;

	private clsAdmSeleccionDespachoNC admseldesnc = new clsAdmSeleccionDespachoNC();

	private List<string[]> almacenes = new List<string[]>();

	private DataTable dataFormIntermedio = new DataTable();

	private bool DespachoTotalVenta = false;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Label label2;

	private Label label5;

	private Label lbNombreTransaccion;

	private TextBox txtComentario;

	private Label label9;

	private Button btnDetalle;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private TextBox txtPrecioVenta;

	private Label label14;

	private TextBox txtIGV;

	private Label label13;

	private TextBox txtDscto;

	private TextBox txtValorVenta;

	private Label label12;

	private Label label11;

	private Label label10;

	private TextBox txtBruto;

	private Label label16;

	private Label label15;

	private TextBox txtTipoCambio;

	private ComboBox cmbMoneda;

	public DataGridView dgvDetalle;

	public TextBox txtDocRef;

	public TextBox txtTransaccion;

	private CheckBox cbValorVenta;

	public TextBox txtNombreCliente;

	public TextBox txtCodCliente;

	private Label label18;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Button btnImprimir;

	private Button btnNuevaGuia;

	private Label label4;

	public TextBox txtDireccionCliente;

	private ComboBox cmbMotivo;

	private Label label3;

	private CheckBox cbAplicada;

	private TextBox txtNumDoc;

	private Label label7;

	private Label label37;

	public TextBox txtNumero;

	private TextBox txtSerie;

	private TextBox txtDocRefe;

	private Label label6;

	private TextBox txtCodDocumento;

	private DateTimePicker dtpFechaPago;

	private ComboBox cmbFormaPago;

	private Label label17;

	private RequiredFieldValidator requiredFieldValidator1;

	private CustomValidator customValidator1;

	private ComboBox cmbMovimiento;

	private TextBox txticbper;

	private Label label8;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn flete;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn fechaingreso;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn tipoimpuesto;

	private DataGridViewTextBoxColumn icbper;

	private DataGridViewTextBoxColumn icbper_band;

	private Button button1;

	private Button btnProductosSeleccionadoDespacho;

	private CheckBox cbSaldoaFavor;

	public byte[] firmadigital { get; set; }

	public frmNotadeCredito()
	{
		InitializeComponent();
	}

	private void txtTransaccion_KeyDown(object sender, KeyEventArgs e)
	{
		if (txtTransaccion.ReadOnly || e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmTransacciones"] != null)
		{
			Application.OpenForms["frmTransacciones"].Activate();
			return;
		}
		frmTransacciones form = new frmTransacciones();
		form.Proceso = 3;
		form.ShowDialog();
		tran = form.tran;
		CodTransaccion = tran.CodTransaccion;
		txtTransaccion.Text = tran.Sigla;
		if (CodTransaccion != 0)
		{
			CargaTransaccion();
			ProcessTabKey(forward: true);
		}
		else
		{
			BorrarTransaccion();
		}
	}

	private void CargaTransaccion()
	{
		tran = AdmTran.MuestraTransaccion(CodTransaccion);
		tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
		txtTransaccion.Text = tran.Sigla;
		lbNombreTransaccion.Text = tran.Descripcion;
		lbNombreTransaccion.Visible = true;
		foreach (Control t in groupBox1.Controls)
		{
			if (t.Tag != null)
			{
				int con = Convert.ToInt32(t.Tag);
				if (tran.Configuracion.Contains(con))
				{
					t.Visible = true;
				}
				else
				{
					t.Visible = false;
				}
			}
		}
	}

	private void BorrarTransaccion()
	{
		txtTransaccion.Text = "";
		lbNombreTransaccion.Text = "";
		lbNombreTransaccion.Visible = false;
		foreach (Control t in groupBox1.Controls)
		{
			if (t.Tag != null)
			{
				t.Visible = false;
			}
		}
	}

	private bool BuscaTransaccion()
	{
		tran = AdmTran.MuestraTransaccionS(txtTransaccion.Text, 0);
		if (tran != null)
		{
			CodTransaccion = tran.CodTransaccion;
			tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
			txtTransaccion.Text = tran.Sigla;
			lbNombreTransaccion.Text = tran.Descripcion;
			lbNombreTransaccion.Visible = true;
			foreach (Control t in groupBox1.Controls)
			{
				if (t.Tag != null)
				{
					int con = Convert.ToInt32(t.Tag);
					if (tran.Configuracion.Contains(con))
					{
						t.Visible = true;
					}
					else
					{
						t.Visible = false;
					}
				}
			}
			return true;
		}
		lbNombreTransaccion.Text = "";
		lbNombreTransaccion.Visible = false;
		foreach (Control t2 in groupBox1.Controls)
		{
			if (t2.Tag != null)
			{
				t2.Visible = false;
			}
		}
		return false;
	}

	private bool BuscaTipoDocumento()
	{
		doc = Admdoc.BuscaTipoDocumento(txtDocRefe.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			return true;
		}
		CodDocumento = 0;
		return false;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
		Dispose();
	}

	public void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtTransaccion.Text != "")
		{
			if (BuscaTransaccion())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Codigo de transacción no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDetalleIngreso"] != null)
		{
			Application.OpenForms["frmDetalleIngreso"].Activate();
			return;
		}
		frmDetalleIngreso form = new frmDetalleIngreso();
		form.Procede = 7;
		form.Proceso = 1;
		form.bvalorventa = cbValorVenta.Checked;
		form.ShowDialog();
	}

	private void VerificarCabecera()
	{
		if (CodTransaccion == 0 || CodDocumento == 0)
		{
			Validacion = false;
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmDetalleIngreso"] != null)
			{
				Application.OpenForms["frmDetalleIngreso"].Activate();
				return;
			}
			frmDetalleIngreso form = new frmDetalleIngreso();
			form.Procede = 7;
			form.Proceso = 1;
			form.bvalorventa = cbValorVenta.Checked;
			form.ShowDialog();
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void frmNotaIngreso_Load(object sender, EventArgs e)
	{
		button1.Visible = false;
		cbSaldoaFavor.Checked = false;
		CargaMoneda();
		CargaFormaPagos();
		cargatipoNC();
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		if (Proceso == 1)
		{
			Bloqueabotones();
		}
		if (Proceso == 2)
		{
			CargaNotaCredito();
		}
		else if (Proceso == 3)
		{
			CargaNotaCredito();
			sololectura(estado: true);
		}
		else if (Proceso == 4)
		{
			CargaNotaCredito();
			sololectura(estado: true);
		}
		else if (Proceso == 7)
		{
			CargaNotaSalida();
		}
		if (notc.CodNotaCredito != null)
		{
			codActualNC = admNI.obtenerCodNCsegun(notc.CodNotaCredito);
			if (admseldesnc.tieneDataSeleccionada(codActualNC.ToString()))
			{
				btnProductosSeleccionadoDespacho.Visible = true;
			}
		}
	}

	private void cargatipoNC()
	{
		cmbMotivo.DataSource = AdmPro.MuestratipoNC();
		cmbMotivo.DisplayMember = "denominacion";
		cmbMotivo.ValueMember = "codigosunat";
		cmbMotivo.SelectedIndex = -1;
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(0);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = -1;
	}

	private void sololectura(bool estado)
	{
		txtTransaccion.ReadOnly = estado;
		dtpFecha.Enabled = !estado;
		cmbMoneda.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		txtCodCliente.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtSerie.Enabled = !estado;
		cmbMovimiento.Enabled = !estado;
		txtDocRef.Enabled = !estado;
		txtComentario.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnNuevo.Visible = !estado;
		btnEditar.Visible = !estado;
		btnEliminar.Visible = !estado;
		btnGuardar.Visible = !estado;
		btnImprimir.Visible = estado;
		btnNuevaGuia.Visible = estado;
		cmbMotivo.Enabled = !estado;
		cbAplicada.Enabled = !estado;
		cbSaldoaFavor.Enabled = !estado;
	}

	private void Bloqueabotones()
	{
		btnNuevo.Visible = false;
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
	}

	private void CargaNotaCredito()
	{
		try
		{
			if (Proceso == 3)
			{
				notc = AdmFact.CargaNotaCredito(Convert.ToInt32(CodNotaS));
			}
			else
			{
				notc = AdmFact.CargaNotaCredito(Convert.ToInt32(CodNota));
			}
			ser = AdmSerie.MuestraSerie(notc.CodSerie, notc.CodAlmacen);
			if (notc != null)
			{
				if (notc.CodReferencia != 0)
				{
					notaS = AdmNotaS.CargaNotaSalidaCreditoVentas(Convert.ToInt32(notc.CodReferencia));
				}
				txtNumDoc.Text = notc.CodNotaCredito.ToString();
				CodTransaccion = notc.CodTipoTransaccion;
				CargaTransaccion();
				CodCliente = notaS.CodCliente;
				CargaCliente();
				cmbFormaPago.SelectedValue = notc.FormaPago;
				dtpFecha.Value = notc.FechaIngreso;
				cmbMoneda.SelectedValue = notc.Moneda;
				txtTipoCambio.Text = notc.TipoCambio.ToString();
				txtComentario.Text = notc.Comentario;
				txtComentario.Text = notc.Comentario;
				cmbMotivo.SelectedIndex = Convert.ToInt32(notc.Motivo);
				cmbMovimiento.SelectedIndex = Convert.ToInt32(notc.MovimientoNC.ToString());
				txtSerie.Text = notc.Serie;
				txtNumero.Text = notc.DocumentoNotaCredito;
				if (txtDocRef.Enabled)
				{
					CodDocumento = notc.CodTipoDocumento;
					txtDocRef.Text = notaS.SiglaDocumento + " " + notaS.Serie + " " + notaS.NumDoc;
				}
				txtBruto.Text = $"{notc.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{notc.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{notc.Total - notc.Igv:#,##0.00}";
				txtIGV.Text = $"{notc.Igv:#,##0.00}";
				txticbper.Text = $"{notc.icbper:#,##0.00}";
				txtPrecioVenta.Text = $"{notc.Total:#,##0.00}";
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmFact.CargaDetalle(Convert.ToInt32(notc.CodNotaCredito));
		RecorreDetalle();
		nota.Detalle = detalle;
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		if (txtTipoCambio.Visible)
		{
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			if (tc != null)
			{
				txtTipoCambio.Text = tc.Venta.ToString();
				return;
			}
			MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			dtpFecha.Value = DateTime.Now.Date;
			dtpFecha.Focus();
		}
	}

	private void frmNotaIngreso_Shown(object sender, EventArgs e)
	{
		txtTransaccion.Focus();
		txtTransaccion.Text = "NCV";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, ee);
		txtDocRefe.Text = "NC";
		KeyPressEventArgs ee2 = new KeyPressEventArgs('\r');
		txtDocRefe_Leave(txtDocRefe, ee2);
		ser = AdmSerie.BuscaSeriexDocumento(4, frmLogin.iCodAlmacen);
		txtCodCliente.Focus();
		if (Proceso == 1 && txtTipoCambio.Visible)
		{
			if (tc == null)
			{
				MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				txtTipoCambio.Text = tc.Venta.ToString();
			}
		}
	}

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmListaDocumentosPorCliente"] != null)
		{
			Application.OpenForms["frmListaDocumentosPorCliente"].Activate();
			return;
		}
		frmListaDocumentosPorCliente form = new frmListaDocumentosPorCliente();
		form.Text = "Documentos";
		form.tipo = 1;
		form.CodCliente = cli.CodCliente;
		form.ShowDialog();
		if (form.venta != null && form.venta.CodFacturaVenta != null)
		{
			venta = form.venta;
			CodNotaS = Convert.ToInt32(venta.CodFacturaVenta);
			if (CodNotaS != 0)
			{
				CargaNotaSalida();
				ProcessTabKey(forward: true);
			}
		}
	}

	private void CargaNotaSalida()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(CodNotaS);
			if (AdmVenta.VerificaEstadoEnvioDocumentoElectronico(venta.CodEmpresa, venta.CodSucursal, venta.CodAlmacen, CodNotaS) || venta.CodTipoDocumento == 8)
			{
				clsDocumentoIdentidad documentoIdentidad = AdmDocumentoIdentidad.ObtenerDocumentoIdentidadDeVenta(CodNotaS);
				if (venta != null)
				{
					txtDocRef.Text = venta.SiglaDocumento + " - " + venta.Serie + " - " + venta.NumDoc;
					ser = AdmSerie.MuestraSeriePorDocumentoAsociado(4, venta.CodAlmacen, venta.CodTipoDocumento);
					CodSerie = ser.CodSerie;
					txtSerie.Focus();
					manual = Convert.ToInt32(ser.PreImpreso);
					if (CodSerie != 0)
					{
						txtSerie.Text = ser.Serie;
						txtNumero.Text = ser.Numeracion.ToString();
					}
					txtTipoCambio.Text = venta.TipoCambio.ToString();
					cmbMoneda.SelectedValue = venta.Moneda;
					if (txtCodCliente.Enabled)
					{
						CodCliente = venta.CodCliente;
						cli = AdmCli.MuestraCliente(CodCliente);
						cli.DocumentoIdentidad = documentoIdentidad;
						txtCodCliente.Text = cli.RucDni;
						txtNombreCliente.Text = cli.Nombre;
						txtDireccionCliente.Text = cli.DireccionLegal;
					}
					CargaDetalleNota();
				}
				else
				{
					MessageBox.Show("El documento solicitado no existe", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show("El documento al que le quiere registrar una Nota de Credito aun no se envia a Sunat", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void BorrarNota()
	{
		try
		{
			CodNotaS = 0;
			notaS = new clsNotaSalida();
			txtDocRef.Text = "";
			((DataTable)dgvDetalle.DataSource)?.Clear();
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalleNota()
	{
		dgvDetalle.DataSource = AdmVenta.CargaDetalleVentaCredito(CodNotaS, venta.CodAlmacen);
		dgvDetalle.Columns["stockdisponible"].Visible = false;
		dgvDetalle.Columns["maxPorcDescto"].Visible = false;
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		cantpr = new List<decimal>();
		cantprec = new List<decimal>();
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			cantpr.Add(Convert.ToDecimal(row.Cells[cantidad.Name].Value));
			cantprec.Add(Convert.ToDecimal(row.Cells[preciounit.Name].Value));
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1 || Proceso == 7)
		{
			CalculaTotales();
		}
	}

	private void CalculaTotales()
	{
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		double igvt = 0.0;
		double preciot = 0.0;
		double total_icbper = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
			igvt += Convert.ToDouble(row.Cells[igv.Name].Value);
			preciot += Convert.ToDouble(row.Cells[precioventa.Name].Value);
			total_icbper += Convert.ToDouble(row.Cells[icbper.Name].Value);
		}
		txtBruto.Text = $"{bruto + total_icbper:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{igvt:#,##0.00}";
		txtPrecioVenta.Text = $"{preciot + total_icbper:#,##0.00}";
		txticbper.Text = $"{total_icbper:#,##0.00}";
	}

	private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void dtpFecha_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			dtpFecha.Focus();
		}
	}

	private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void cmbMoneda_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			cmbMoneda.Focus();
		}
	}

	private void txtNDocRef_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtTransaccion_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			txtTransaccion.Focus();
		}
	}

	private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
	{
		if (txtPrecioVenta.Text != "")
		{
			btnGuardar.Enabled = true;
		}
	}

	private async void btnGuardar_Click(object sender, EventArgs e)
	{
		DataTable dataGuardarSeleccionado = null;
		clsDespacho despachoAConfirmar1 = null;
		List<clsDetalleDespacho> detalleDespachoAConfirmar1 = null;
		if (Proceso == 0)
		{
			return;
		}
		if (txtNumero.Text != "")
		{
			if (cmbMotivo.SelectedIndex == -1)
			{
				MessageBox.Show("Por favor seleccionar un motivo!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				cmbMotivo.Focus();
			}
			else if (cmbMovimiento.SelectedIndex == -1 && cmbMotivo.SelectedIndex != 1)
			{
				MessageBox.Show("Por favor seleccionar un movimiento!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				cmbMovimiento.Focus();
			}
			else
			{
				if (dgvDetalle.Rows.Count <= 0)
				{
					return;
				}
				bool nctotal = false;
				bool ncparcial = false;
				bool seEliminoProdTieneDespacho = false;
				if (Convert.ToString(cmbMotivo.SelectedValue) == "01")
				{
					nctotal = true;
					switch (admdespacho.VerificaEntregasActivasRespectoADespacho(1, venta.CodFacturaVenta))
					{
					case 1:
						MessageBox.Show("La Venta: " + txtDocRef.Text + " no se puede ANULAR, por que tiene entregas de despacho activas", "Venta tiene Entregas de Desacho", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					case -1:
						MessageBox.Show("La Venta: " + txtDocRef.Text + " no se puede ANULAR, por que ocurrio un error al intentar verificar entregas de despacho activas.", "Error en Verificacion de Entregas de Desacho", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
				else if (Convert.ToString(cmbMotivo.SelectedValue) == "07")
				{
					ncparcial = true;
					if (!validaDatosReqDespParaNCParcial())
					{
						return;
					}
					if (dataFormIntermedio.Rows.Count > 0)
					{
						despachoAConfirmar1 = admdespacho.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
						detalleDespachoAConfirmar1 = admdespacho.ListaDetalleDespacho(despachoAConfirmar1.CodDespacho);
						frmIntermedioNotaCreditoDespacho form = new frmIntermedioNotaCreditoDespacho
						{
							almacenes = almacenes,
							data = dataFormIntermedio,
							codAlmacenProdSinDespacho = venta.CodAlmacen
						};
						DialogResult rpta = form.ShowDialog();
						List<object[]> seleccionados = form.seleccion;
						dataGuardarSeleccionado = form.ultimaData;
						if (rpta != DialogResult.Yes)
						{
							MessageBox.Show("No se permitio realizar la nota credito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						DataTable almacenesRelacionadosLogin = admAlmacen.listaAlmacenxNombre(frmLogin.iCodAlmacen);
						if (almacenesRelacionadosLogin != null)
						{
							if (almacenesRelacionadosLogin.Rows.Count > 0)
							{
								almacenesRelacionadosLogin.Rows.RemoveAt(0);
							}
						}
						else
						{
							almacenesRelacionadosLogin = new DataTable();
						}
						for (int i = 0; i < seleccionados.Count; i++)
						{
							int _codProducto = Convert.ToInt32(seleccionados[i][0]);
							int _codAlmacenSeleccionado = Convert.ToInt32(seleccionados[i][1]);
							double cantidadSeleccionada = Convert.ToDouble(seleccionados[i][2]);
							List<ProdTieneDespacho> obtener = Enumerable.Where<ProdTieneDespacho>(prodsBandDespacho.AsEnumerable(), (Func<ProdTieneDespacho, bool>)((ProdTieneDespacho g) => g.codProducto == _codProducto)).ToList();
							if (obtener.Count <= 0)
							{
								continue;
							}
							for (int j = 0; j < obtener.Count; j++)
							{
								ProdTieneDespacho item = obtener[j];
								int index = prodsBandDespacho.IndexOf(item);
								List<DataRow> encontrado = (from g in almacenesRelacionadosLogin.AsEnumerable()
									where Convert.ToInt32(g.Field<object>("cod")) == _codAlmacenSeleccionado
									select g).ToList();
								if (encontrado.Count > 0)
								{
									double cantidadrestante = item.cantidad - cantidadSeleccionada;
									if (cantidadrestante <= 0.0)
									{
										item.cantidadAlmacen = item.cantidad;
										item.cantidad = 0.0;
										prodsBandDespacho[index] = item;
									}
									else
									{
										item.cantidadAlmacen = cantidadSeleccionada;
										item.cantidad = cantidadrestante;
										prodsBandDespacho[index] = item;
									}
								}
								else
								{
									item.cantidadAlmacen = item.cantidad - cantidadSeleccionada;
									item.cantidad = cantidadSeleccionada;
									prodsBandDespacho[index] = item;
								}
							}
						}
					}
				}
				usuario_click = null;
				frmAutorizacion frm = new frmAutorizacion
				{
					tipoAccion = 2
				};
				int codPermiso = new clsAdmFormulario().getPermisoAnularVentas();
				frm.permiso = codPermiso;
				frm.tipoVentanaAAsignarUsuario = 7;
				frm.ventanaNotaCredito = this;
				frm.PermitirAdministradores = true;
				DialogResult dr = frm.ShowDialog();
				if (dr != DialogResult.OK || usuario_click == null)
				{
					return;
				}
				if (despachoAConfirmar1 != null)
				{
					clsDespacho despachoAConfirmar2 = admdespacho.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
					List<clsDetalleDespacho> detalleDespachoAConfirmar2 = admdespacho.ListaDetalleDespacho(despachoAConfirmar2.CodDespacho);
					if (despachoAConfirmar1.CodEstado != despachoAConfirmar2.CodEstado)
					{
						MessageBox.Show("El despacho a cambiado de estado durante la creacion de esta nota de credito. \nConsultar sobre el DESP " + despachoAConfirmar1.Serie + "-" + despachoAConfirmar1.Numeracion, "DESPACHO A SIDO MODIFICADO", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					if (!verificarDetalleDespacho(detalleDespachoAConfirmar1, detalleDespachoAConfirmar2))
					{
						MessageBox.Show("El despacho a sido modificado durante la creacion de esta nota de credito. \nConsultar sobre el DESP " + despachoAConfirmar1.Serie + "-" + despachoAConfirmar1.Numeracion, "DESPACHO A SIDO MODIFICADO", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
				cargarTotalesSunat();
				notc.CodAlmacen = venta.CodAlmacen;
				notc.CodTipoTransaccion = tran.CodTransaccion;
				notc.CodTipoDocumento = ((venta.CodTipoDocumento == 8) ? 8 : 4);
				notc.DocumentoNotaCredito = txtNumero.Text;
				notc.NumFac = txtNumero.Text;
				notc.FechaIngreso = dtpFecha.Value.Date;
				notc.Cancelado = 0;
				notc.Comentario = txtComentario.Text;
				notc.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				notc.MontoBruto = Convert.ToDouble(txtValorVenta.Text);
				notc.MontoDscto = Convert.ToDouble(txtDscto.Text);
				notc.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
				notc.Igv = Convert.ToDouble(txtIGV.Text);
				notc.Total = Convert.ToDouble(txtPrecioVenta.Text);
				notc.icbper = Convert.ToDecimal(txticbper.Text);
				notc.Estado = 1;
				notc.CodUser = frmLogin.iCodUser;
				notc.CodSerie = CodSerie;
				notc.Serie = txtSerie.Text;
				notc.CodReferencia = Convert.ToInt32(venta.CodFacturaVenta);
				notc.CodCliente = CodCliente;
				notc.Motivo = cmbMotivo.SelectedValue.ToString();
				if (cmbMotivo.SelectedIndex == 1)
				{
					notc.MovimientoNC = 0;
				}
				notc.MovimientoNC = 1;
				notc.FormaPago = venta.FormaPago;
				notc.FechaPago = venta.FechaPago;
				notc.Gratuitas = montogratuitas;
				notc.Exoneradas = montoexoneradas;
				notc.Gravadas = montogravadas;
				notc.Inafectas = montoinafectas;
				notc.Tipofacturacion = venta.Tipoventa;
				notc.Cancelado = Convert.ToInt32(cbSaldoaFavor.Checked);
				nota = new clsNotaIngreso();
				nota.CodAlmacen = venta.CodAlmacen;
				nota.NumDoc = txtNumero.Text;
				nota.CodTipoTransaccion = tran.CodTransaccion;
				nota.CodTipoDocumento = ((venta.CodTipoDocumento == 8) ? 8 : 4);
				nota.CodSerie = CodSerie;
				nota.Serie = txtSerie.Text;
				if (CodNotaS != 0)
				{
					nota.CodReferencia = CodNotaS;
				}
				nota.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
				nota.FechaIngreso = dtpFecha.Value.Date;
				if (fpago.Dias == 0)
				{
					nota.FechaCancelado = dtpFecha.Value.Date;
					nota.Cancelado = 1;
				}
				nota.Aplicada = 0;
				if (cbAplicada.Checked)
				{
					nota.Aplicada = 1;
					nota.CodAplicada = nota.CodReferencia;
				}
				nota.FormaPago = 0;
				nota.Motivo = cmbMotivo.SelectedValue.ToString();
				nota.MontoBruto = Convert.ToDouble(txtValorVenta.Text);
				nota.MontoDscto = Convert.ToDouble(txtDscto.Text);
				nota.Igv = Convert.ToDouble(txtIGV.Text);
				nota.Total = Convert.ToDouble(txtPrecioVenta.Text);
				nota.CodUser = frmLogin.iCodUser;
				nota.Estado = 1;
				if (cmbMotivo.SelectedIndex == 1)
				{
					nota.MovimientoNC = 0;
				}
				else
				{
					nota.MovimientoNC = 1;
				}
				if (Proceso != 1 && Proceso != 7)
				{
					return;
				}
				if (nota.Total != 0.0)
				{
					AdmNota.VerificarNCVentaAplicada(nota);
					if (nota.Comentario.Equals("0") && !nota.Comentario.Equals("0"))
					{
						return;
					}
					nota.Aplicada = 0;
					nota.Comentario = txtComentario.Text;
					if (Convert.ToString(cmbMotivo.SelectedValue) == "04" || Convert.ToString(cmbMotivo.SelectedValue) == "05" || Convert.ToString(cmbMotivo.SelectedValue) == "09")
					{
						notc.CodNotaIngreso = 0;
						if (!AdmFact.insert(notc))
						{
							return;
						}
						RecorreDetalle();
						if (detalleNotaCredito.Count > 0)
						{
							foreach (clsDetalleNotaCredito det in detalleNotaCredito)
							{
								if (Convert.ToString(cmbMotivo.SelectedValue) == "04")
								{
									det.CodNotaIngreso = "0";
								}
								AdmFact.insertdetalle(det);
							}
						}
						MessageBox.Show("Los datos se guardaron correctamente", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						CodNota = notc.CodNotaCreditoNueva.ToString();
						if (venta.CodTipoDocumento != 8)
						{
							await con.DatosNCredito(cli, notc, detalleNotaCredito);
						}
						dtPagos = admPago.GetPagosVenta(venta.CodAlmacen, Convert.ToInt32(venta.CodFacturaVenta));
						pag = admPago.MuestraPagoVenta(venta.CodAlmacen, Convert.ToInt32(venta.CodFacturaVenta));
						if (!AdmVenta.ValidaAnulacionVenta(Convert.ToInt32(venta.CodFacturaVenta)))
						{
							if (AdmVenta.anular(Convert.ToInt32(venta.CodFacturaVenta)))
							{
								MessageBox.Show("El documento ha sido anulado correctamente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								foreach (DataRow fila in dtPagos.Rows)
								{
									admPago.AnularPago(Convert.ToInt32(fila[0]));
								}
							}
						}
						else
						{
							MessageBox.Show("La Venta ya esta Anulada", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						string tipoNC = (nctotal ? "T" : (ncparcial ? "P" : "N"));
						AdmFact.actualizarCodNotaCreditoFV(Convert.ToInt32(venta.CodFacturaVenta), Convert.ToInt32(CodNota), tipoNC);
						AdmFact.anularFactura_venta(Convert.ToInt32(venta.CodFacturaVenta));
						sololectura(estado: true);
					}
					else
					{
						if (!AdmNota.insert(nota))
						{
							return;
						}
						notc.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
						AdmFact.insert(notc);
						RecorreDetalle();
						if (this.detalle.Count > 0)
						{
							foreach (clsDetalleNotaIngreso det2 in this.detalle)
							{
								AdmNota.insertdetalle(det2);
							}
						}
						if (detalleNotaCredito.Count > 0)
						{
							foreach (clsDetalleNotaCredito det3 in detalleNotaCredito)
							{
								AdmFact.insertdetalle(det3);
							}
						}
						CodNota = notc.CodNotaCreditoNueva.ToString();
						string tipoNC2 = (nctotal ? "T" : (ncparcial ? "P" : "N"));
						AdmFact.actualizarCodNotaCreditoFV(Convert.ToInt32(venta.CodFacturaVenta), Convert.ToInt32(CodNota), tipoNC2);
						if (venta.CodTipoDocumento != 8)
						{
							await con.DatosNCredito(cli, notc, detalleNotaCredito);
						}
						dtPagos = admPago.GetPagosVenta(venta.CodAlmacen, Convert.ToInt32(venta.CodFacturaVenta));
						pag = admPago.MuestraPagoVenta(venta.CodAlmacen, Convert.ToInt32(venta.CodFacturaVenta));
						foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
						{
							AdmVenta.AnulaDetalleVenta(Convert.ToInt32(row.Cells[coddetalle.Name].Value), Convert.ToInt32(row.Cells[codproducto.Name].Value));
						}
						MessageBox.Show("Los datos se guardaron correctamente", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						sololectura(estado: true);
						printNC();
						venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
						clsDespacho despacho = admdespacho.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
						if (despacho == null)
						{
							return;
						}
						try
						{
							if (nctotal)
							{
								if (admdespacho.anular(despacho))
								{
									clsRequerimientoAlmacen req_alm = admreqalm.CargaRequerimientosSegun(venta.CodPedido, venta.CodAlmacen, -1);
									if (req_alm != null)
									{
										int codReqAlm = req_alm.Codigo;
										DataTable listadoCodTrans = admreqalm.cargaTransferenciasAprobadas(codReqAlm);
										if (listadoCodTrans != null)
										{
											if (listadoCodTrans.Rows.Count > 1)
											{
												MessageBox.Show("Documento de Extornacion No Creado.\nUn Requerimiento de Almacen de Tipo Venta no puede tener mas de una Transferencia Activa.\ncodReq: " + codReqAlm, "Error Requerimiento No Anulado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
											if (listadoCodTrans.Rows.Count == 1)
											{
												admreqalm.anular(codReqAlm, frmLogin.iCodUser);
												int codTransDir = Convert.ToInt32(listadoCodTrans.Rows[0].Field<object>(0));
												clsTipoDocumento tipodoc = Admdoc.BuscaTipoDocumento("DET");
												clsTransferencia transf = admTransferencia.CargaTransferencia(codTransDir);
												clsTransferencia extornacion = new clsTransferencia
												{
													codTransAExtornar = codTransDir,
													CodAlmacenOrigen = req_alm.CodAlmacenSolicitante,
													CodAlmacenDestino = req_alm.CodAlmacenDespacho,
													CodTipoDocumento = tipodoc.CodTipoDocumento,
													FechaEnvio = DateTime.Now,
													FechaEntrega = DateTime.Now,
													FormaPago = 0,
													FechaPago = DateTime.Now.Date,
													CodListaPrecio = 0
												};
												string comentario = "Documento de Extornacion para Transf: " + codTransDir;
												extornacion.Comentario = comentario;
												extornacion.DescripcionRechazo = "";
												extornacion.CodUser = frmLogin.iCodUser;
												extornacion.Estado = 1;
												extornacion.Codserie = transf.Codserie;
												extornacion.Serie = transf.Serie;
												extornacion.Numerodocumento = transf.Numerodocumento;
												extornacion.Moneda = 1;
												List<clsDetalleTransferencia> detalle = obtenerDetalleParaTransferencia(req_alm, extornacion);
												if (detalle.Count > 0 && admTransferencia.insert(extornacion))
												{
													foreach (clsDetalleTransferencia det4 in detalle)
													{
														det4.CodTransDir = Convert.ToInt32(extornacion.CodTransDir);
														admTransferencia.insertdetalle(det4);
													}
													apruebaTransferencia(extornacion, detalle);
												}
											}
										}
									}
									else
									{
										MessageBox.Show("Requerimiento de Almacen No A Sido Anulado", "Requerimiento No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									}
								}
								else
								{
									MessageBox.Show("Despacho No Anulado", "Anulacion Despacho Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
							if (!ncparcial)
							{
								return;
							}
							if (prodsBandDespacho.Count > 0)
							{
								List<clsDetalleDespacho> detalleDespacho = admdespacho.ListaDetalleDespacho(despacho.CodDespacho);
								List<clsDetalleDespacho> detalleDespachoUpd = new List<clsDetalleDespacho>();
								DataTable almacenesRelacionadosLogin2 = admAlmacen.listaAlmacenxNombre(frmLogin.iCodAlmacen);
								if (almacenesRelacionadosLogin2 != null)
								{
									if (almacenesRelacionadosLogin2.Rows.Count > 0)
									{
										almacenesRelacionadosLogin2.Rows.RemoveAt(0);
									}
								}
								else
								{
									almacenesRelacionadosLogin2 = new DataTable();
								}
								foreach (ProdTieneDespacho item2 in prodsBandDespacho)
								{
									List<clsDetalleDespacho> busqueda = Enumerable.Where<clsDetalleDespacho>(detalleDespacho.Cast<clsDetalleDespacho>(), (Func<clsDetalleDespacho, bool>)((clsDetalleDespacho z) => z.CodProducto == item2.codProducto && z.CodUnidad == item2.codUnidad)).ToList();
									if (busqueda.Count <= 0)
									{
										continue;
									}
									double cantidadDisminuir = (item2.tieneDespachoAlmacen ? (item2.cantidad + item2.cantidadAlmacen) : item2.cantidad);
									foreach (clsDetalleDespacho dd in busqueda)
									{
										clsDetalleDespacho aux = new clsDetalleDespacho
										{
											CodDetalleDespacho = dd.CodDetalleDespacho,
											CodAlmacenEntregar = dd.CodAlmacenEntregar,
											Estado = 1
										};
										if (item2.tieneDespachoAlmacen)
										{
											List<DataRow> encontrado2 = (from g in almacenesRelacionadosLogin2.AsEnumerable()
												where Convert.ToInt32(g.Field<object>("cod")) == dd.CodAlmacenEntregar
												select g).ToList();
											if (encontrado2.Count > 0)
											{
												aux.Cantidad = dd.Cantidad - item2.cantidadAlmacen;
												aux.CantidadPendiente = dd.CantidadPendiente - item2.cantidadAlmacen;
											}
											else
											{
												aux.Cantidad = dd.Cantidad - item2.cantidad;
												aux.CantidadPendiente = dd.CantidadPendiente - item2.cantidad;
											}
										}
										else if (dd.CantidadPendiente < cantidadDisminuir)
										{
											cantidadDisminuir -= dd.CantidadPendiente;
											aux.Cantidad = dd.Cantidad - dd.CantidadPendiente;
											aux.CantidadPendiente = 0.0;
										}
										else
										{
											aux.Cantidad = dd.Cantidad - cantidadDisminuir;
											aux.CantidadPendiente = dd.CantidadPendiente - cantidadDisminuir;
										}
										detalleDespachoUpd.Add(aux);
									}
								}
								int codEstado = 0;
								if (detalleDespachoUpd.Count > 0)
								{
									foreach (clsDetalleDespacho item3 in detalleDespachoUpd)
									{
										admdespacho.updateDetalle(item3);
									}
									codEstado = admdespacho.obtenerCodEstado(despacho.CodDespacho);
									if (codEstado != -1)
									{
										admdespacho.cambioEstado(despacho.CodDespacho, codEstado);
									}
									if (despacho.codReqAlmRelacionado > 0 && codEstado == 16)
									{
										admreqalm.actualizaEstadoReqAlmacen(despacho.codReqAlmRelacionado, 11);
									}
									if (despacho.codReqAlmRelacionado > 0 && codEstado == 15)
									{
										admreqalm.actualizaEstadoReqAlmacen(despacho.codReqAlmRelacionado, 10);
									}
								}
								clsRequerimientoAlmacen req_alm2 = admreqalm.CargaRequerimientosSegun(venta.CodPedido, venta.CodAlmacen, -1);
								if (req_alm2 != null)
								{
									int codReqAlm2 = req_alm2.Codigo;
									DataTable listadoCodTrans2 = admreqalm.cargaTransferenciasAprobadas(codReqAlm2);
									if (listadoCodTrans2 != null)
									{
										if (listadoCodTrans2.Rows.Count > 1)
										{
											MessageBox.Show("Documento de Extornacion No Creado.\nUn Requerimiento de Almacen de Tipo Venta no puede tener mas de una Transferencia Activa.\ncodReq: " + codReqAlm2, "Error Requerimiento No Anulado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
										if (listadoCodTrans2.Rows.Count == 1)
										{
											int codTransDir2 = Convert.ToInt32(listadoCodTrans2.Rows[0].Field<object>(0));
											clsTipoDocumento tipodoc2 = Admdoc.BuscaTipoDocumento("DET");
											clsTransferencia transf2 = admTransferencia.CargaTransferencia(codTransDir2);
											clsTransferencia extornacion2 = new clsTransferencia
											{
												codTransAExtornar = codTransDir2,
												CodAlmacenOrigen = req_alm2.CodAlmacenSolicitante,
												CodAlmacenDestino = req_alm2.CodAlmacenDespacho,
												CodTipoDocumento = tipodoc2.CodTipoDocumento,
												FechaEnvio = DateTime.Now,
												FechaEntrega = DateTime.Now,
												FormaPago = 0,
												FechaPago = DateTime.Now.Date,
												CodListaPrecio = 0
											};
											string comentario2 = "Documento de Extornacion para Transf: " + codTransDir2;
											extornacion2.Comentario = comentario2;
											extornacion2.DescripcionRechazo = "";
											extornacion2.CodUser = frmLogin.iCodUser;
											extornacion2.Estado = 1;
											extornacion2.Codserie = transf2.Codserie;
											extornacion2.Serie = transf2.Serie;
											extornacion2.Numerodocumento = transf2.Numerodocumento;
											extornacion2.Moneda = 1;
											List<clsDetalleTransferencia> detalle2 = obtenerDetalleParaTransferencia_NCParcial(req_alm2, extornacion2);
											if (detalle2.Count > 0 && admTransferencia.insert(extornacion2))
											{
												foreach (clsDetalleTransferencia det5 in detalle2)
												{
													det5.CodTransDir = Convert.ToInt32(extornacion2.CodTransDir);
													admTransferencia.insertdetalle(det5);
												}
												apruebaTransferencia(extornacion2, detalle2);
											}
										}
									}
								}
								if (despacho.codReqAlmRelacionado > 0 && codEstado == 16 && admdespacho.VerificaEntregasActivasDeDespacho(despacho.CodDespacho) == 0)
								{
									admdespacho.anular(despacho);
									admreqalm.anular(despacho.codReqAlmRelacionado, frmLogin.iCodUser);
								}
							}
							else if (!seEliminoProdTieneDespacho)
							{
								MessageBox.Show("No se detectaron productos de despacho para retornar a otro almacen en Nota de Credito de Factura Venta.\n No se termino el proceso de anulacion parcial por nota de credito a despacho", "Procedimiento de Validacion Despacho - Nota de Credito Parcial Fallida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							}
							if (dataGuardarSeleccionado == null)
							{
								return;
							}
							bool bandMostrarVentanaSeleccionadorDespachoNC = false;
							if (detalleNotaCredito.Count > 0)
							{
								foreach (DataRow fila2 in dataGuardarSeleccionado.Rows)
								{
									int codNC = 0;
									int codDNC = 0;
									List<clsDetalleNotaCredito> encontrado3 = Enumerable.Where<clsDetalleNotaCredito>(detalleNotaCredito.AsEnumerable(), (Func<clsDetalleNotaCredito, bool>)((clsDetalleNotaCredito x1) => x1.CodProducto.ToString() == fila2.Field<object>("codProducto").ToString() && x1.UnidadIngresada.ToString() == fila2.Field<object>("codUnidad").ToString())).ToList();
									if (encontrado3.Count > 0)
									{
										clsDetalleNotaCredito dnc = encontrado3[0];
										codNC = dnc.CodNotaCredito;
										codDNC = dnc.CodDetalleNotaCredito;
									}
									foreach (string[] item4 in almacenes)
									{
										object oseleccionado = fila2.Field<object>(item4[0].ToString());
										oseleccionado = ((oseleccionado == null || oseleccionado == DBNull.Value) ? ((object)false) : oseleccionado);
										bool seleccionado = Convert.ToBoolean(oseleccionado);
										object octdadpermitida = fila2.Field<object>(item4[1].ToString());
										octdadpermitida = ((octdadpermitida == null || octdadpermitida == DBNull.Value) ? ((object)0) : octdadpermitida);
										double ctdadPermitida = Convert.ToDouble(octdadpermitida);
										clsSeleccionDespachoNC oSelec = new clsSeleccionDespachoNC
										{
											CodAlmacen = Convert.ToInt32(item4[0]),
											CodDetalleNotaCredito = codDNC,
											CodNotaCredito = codNC,
											CtdadPermitida = ctdadPermitida,
											CtdadSeleccionada = 0.0,
											Seleccionado = seleccionado
										};
										admseldesnc.insert(oSelec);
										bandMostrarVentanaSeleccionadorDespachoNC = true;
									}
								}
							}
							btnProductosSeleccionadoDespacho.Visible = bandMostrarVentanaSeleccionadorDespachoNC;
							if (bandMostrarVentanaSeleccionadorDespachoNC)
							{
								codActualNC = Convert.ToInt32(CodNota);
							}
						}
						catch (Exception ex)
						{
							Exception ex2 = ex;
							MessageBox.Show(ex2.Message, "Error Procedimiento de Despacho - Req. - Transf. DET", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
				else
				{
					MessageBox.Show("Ingrese valor correctamente!", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		else
		{
			MessageBox.Show("Por favor ingrese numero de Nota Credito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private bool verificarDetalleDespacho(List<clsDetalleDespacho> detalleDespachoAConfirmar21, List<clsDetalleDespacho> detalleDespachoAConfirmar22)
	{
		if (detalleDespachoAConfirmar21.Count != detalleDespachoAConfirmar22.Count)
		{
			return false;
		}
		detalleDespachoAConfirmar21 = Enumerable.OrderBy<clsDetalleDespacho, int>((IEnumerable<clsDetalleDespacho>)detalleDespachoAConfirmar21, (Func<clsDetalleDespacho, int>)((clsDetalleDespacho x) => x.CodDetalleDespacho)).ToList();
		detalleDespachoAConfirmar22 = Enumerable.OrderBy<clsDetalleDespacho, int>((IEnumerable<clsDetalleDespacho>)detalleDespachoAConfirmar22, (Func<clsDetalleDespacho, int>)((clsDetalleDespacho x) => x.CodDetalleDespacho)).ToList();
		for (int i = 0; i < detalleDespachoAConfirmar21.Count; i++)
		{
			clsDetalleDespacho item1 = detalleDespachoAConfirmar21[i];
			clsDetalleDespacho item2 = detalleDespachoAConfirmar22[i];
			if (item1.Estado != item2.Estado || item1.Cantidad != item2.Cantidad || item1.CantidadPendiente != item2.CantidadPendiente || item1.CodDetalleDespacho != item2.CodDetalleDespacho)
			{
				return false;
			}
		}
		return true;
	}

	private bool validaDatosReqDespParaNCParcial()
	{
		bool rpta = false;
		bool bandAddProdConSelecc = false;
		bool bandAddProdSinSelecc = false;
		bool bandVentaTieneDespacho = false;
		string cadena = "Ocurrio un problema por incoherencia no se asigno un valor verdadero para la verificacion.";
		almacenes = new List<string[]>();
		dataFormIntermedio = new DataTable();
		prodsBandDespacho.Clear();
		venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
		DataTable detalleVenta = AdmVenta.CargaDetalle(Convert.ToInt32(venta.CodFacturaVenta), venta.CodAlmacen, 0);
		if (detalleVenta.Rows.Count > 0)
		{
			foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
			{
				int codProductoF = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
				int codUnidadF = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
				double cantidadF = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
				string refProductoF = fila.Cells[referencia.Name].Value.ToString();
				bool tieneDespacho = false;
				bool tieneDespachoAlmacen = false;
				bool tieneReq = false;
				rpta = false;
				List<DataRow> busqueda = (from f in detalleVenta.AsEnumerable()
					where Convert.ToInt32(f.Field<object>("codProducto")) == codProductoF && Convert.ToInt32(f.Field<object>("codUnidadMedida")) == codUnidadF
					select f).ToList();
				if (busqueda.Count == 1)
				{
					clsRequerimientoAlmacen reqalm = admreqalm.CargaRequerimientosSegun(Convert.ToInt32(venta.CodFacturaVenta));
					DataRow detalle = busqueda[0];
					if (reqalm != null)
					{
						DataTable detalleReq = admreqalm.ListaDetalleRequerimiento(reqalm.Codigo);
						List<DataRow> busqueda2 = (from f in detalleReq.AsEnumerable()
							where Convert.ToInt32(f.Field<object>("codProducto")) == codProductoF && Convert.ToInt32(f.Field<object>("codUnidad")) == codUnidadF
							select f).ToList();
						if (busqueda2.Count == 1)
						{
							tieneReq = true;
						}
					}
					clsDespacho despacho = admdespacho.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
					double ctdadPendienteDespacho = 0.0;
					if (despacho != null)
					{
						bandVentaTieneDespacho = true;
						DataTable almacenesRelacionadosLogin = admAlmacen.listaAlmacenxNombre(frmLogin.iCodAlmacen);
						if (almacenesRelacionadosLogin != null)
						{
							if (almacenesRelacionadosLogin.Rows.Count > 0)
							{
								almacenesRelacionadosLogin.Rows.RemoveAt(0);
							}
						}
						else
						{
							almacenesRelacionadosLogin = new DataTable();
						}
						DataTable detalleDesp = admdespacho.ListaDetalleDespacho2(despacho.CodDespacho);
						List<DataRow> busqueda3 = (from f in detalleDesp.AsEnumerable()
							where Convert.ToInt32(f.Field<object>("codProducto")) == codProductoF && Convert.ToInt32(f.Field<object>("codUnidad")) == codUnidadF
							select f).ToList();
						if (busqueda3.Count > 0)
						{
							tieneDespacho = true;
							foreach (DataRow detalle3 in busqueda3)
							{
								if (almacenesRelacionadosLogin.Rows.Count > 0)
								{
									List<DataRow> encontrado = (from g in almacenesRelacionadosLogin.AsEnumerable()
										where Convert.ToInt32(g.Field<object>("cod")) == Convert.ToInt32(detalle3.Field<object>("codAlmacen").ToString())
										select g).ToList();
									if (encontrado.Count == 0)
									{
										ctdadPendienteDespacho += Convert.ToDouble(detalle3.Field<object>("ctdadPendiente"));
										continue;
									}
									tieneDespachoAlmacen = true;
									if (!tieneReq)
									{
										ctdadPendienteDespacho += Convert.ToDouble(detalle3.Field<object>("ctdadPendiente"));
									}
								}
								else
								{
									ctdadPendienteDespacho += Convert.ToDouble(detalle3.Field<object>("ctdadPendiente"));
								}
							}
						}
					}
					ProdTieneDespacho aux = new ProdTieneDespacho
					{
						codProducto = codProductoF,
						codUnidad = codUnidadF,
						tieneDespacho = tieneDespacho,
						tieneDespachoAlmacen = tieneDespachoAlmacen,
						tieneRequerimiento = tieneReq,
						cantidad = cantidadF
					};
					prodsBandDespacho.Add(aux);
					if (!tieneDespacho)
					{
						rpta = agregarProductoDataFormIntermedio(aux);
					}
					else if (cantidadF <= ctdadPendienteDespacho)
					{
						if (tieneReq)
						{
							DataTable detalleDespParaVerificar = admdespacho.DetalleParaVerificarRetornoDeProductos(despacho.CodDespacho, frmLogin.iCodAlmacen);
							List<DataRow> busqueda4 = (from f in detalleDespParaVerificar.AsEnumerable()
								where Convert.ToInt32(f.Field<object>("codProducto")) == codProductoF && Convert.ToInt32(f.Field<object>("codUnidad")) == codUnidadF
								select f).ToList();
							if (busqueda4.Count >= 1)
							{
								rpta = agregarProductoARetornarEnVariosAlmacenes(busqueda4);
								bandAddProdSinSelecc = true;
							}
							else
							{
								rpta = false;
								cadena = "La cantidad del producto " + refProductoF + " es mayor a la permitida por la cantidad pendiente en el Despacho: DESP. " + despacho.Serie + " - " + despacho.Numeracion + ".";
							}
						}
						else
						{
							DataTable detalleDespParaVerificar2 = admdespacho.DetalleParaVerificarRetornoDeProductos(despacho.CodDespacho, frmLogin.iCodAlmacen);
							List<DataRow> busqueda5 = (from f in detalleDespParaVerificar2.AsEnumerable()
								where Convert.ToInt32(f.Field<object>("codProducto")) == codProductoF && Convert.ToInt32(f.Field<object>("codUnidad")) == codUnidadF
								select f).ToList();
							if (busqueda5.Count >= 1)
							{
								rpta = agregarProductoARetornarEnVariosAlmacenes(busqueda5);
								bandAddProdSinSelecc = true;
							}
							if (tieneDespachoAlmacen && aux.cantidad > 0.0 && aux.cantidadAlmacen == 0.0)
							{
								aux.cantidadAlmacen = aux.cantidad;
								aux.cantidad = 0.0;
							}
						}
					}
					else
					{
						DataTable detalleDespParaVerificar3 = admdespacho.DetalleParaVerificarRetornoDeProductos(despacho.CodDespacho, frmLogin.iCodAlmacen);
						List<DataRow> busqueda6 = (from f in detalleDespParaVerificar3.AsEnumerable()
							where Convert.ToInt32(f.Field<object>("codProducto")) == codProductoF && Convert.ToInt32(f.Field<object>("codUnidad")) == codUnidadF
							select f).ToList();
						if (busqueda6.Count > 1)
						{
							double resultadoSuma = Enumerable.Sum<DataRow>(busqueda6.AsEnumerable(), (Func<DataRow, double>)((DataRow x) => Convert.ToDouble(x.Field<object>("ctdadPendiente").ToString())));
							if (cantidadF <= resultadoSuma)
							{
								rpta = agregarProductoARetornarEnVariosAlmacenes(busqueda6);
								bandAddProdConSelecc = true;
							}
							else
							{
								rpta = false;
								cadena = "La cantidad del producto " + refProductoF + " es mayor a la permitida por la cantidad pendiente en el Despacho: DESP. " + despacho.Serie + " - " + despacho.Numeracion + " y/o la cantidad entregada en la venta.";
							}
						}
						else
						{
							rpta = false;
							cadena = "La cantidad del producto " + refProductoF + " es mayor a la permitida por la cantidad pendiente en el Despacho: DESP. " + despacho.Serie + " - " + despacho.Numeracion + ".";
						}
					}
				}
				else
				{
					rpta = false;
					cadena = ((busqueda.Count <= 0) ? ("No se encontro el Producto " + refProductoF + " en la venta.\nSe recomienda informar a sistemas verificar la venta") : ("El Producto " + refProductoF + " se encontro duplicado en venta.\nSe recomienda informar a sistemas verificar la venta"));
				}
				if (!rpta)
				{
					break;
				}
			}
		}
		else
		{
			rpta = false;
			cadena = "Ocurrio un error al cargar la venta";
		}
		if (cadena != "" && !rpta)
		{
			MessageBox.Show(cadena, "Error de Verificacion de Despacho Para Anulacion Parcial", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (!bandVentaTieneDespacho)
		{
			dataFormIntermedio = new DataTable();
		}
		return rpta;
	}

	private bool agregarProductoDataFormIntermedio(ProdTieneDespacho aux, int tipo = 1)
	{
		try
		{
			inicializaDataForIntermedio();
			DataRow fila = dataFormIntermedio.NewRow();
			clsProducto prod = AdmPro.CargaProducto(aux.codProducto, frmLogin.iCodAlmacen);
			clsUnidadMedida und = new clsAdmUnidad().CargaUnidad(aux.codUnidad);
			clsAlmacen alm = admAlmacen.CargaAlmacen(venta.CodAlmacen);
			List<DataGridViewRow> encontrado = Enumerable.Where<DataGridViewRow>(dgvDetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => x.Cells[codproducto.Name].Value.ToString() == prod.CodProducto.ToString())).ToList();
			if (encontrado.Count == 1)
			{
				double cantidadNC = Convert.ToDouble(encontrado[0].Cells[cantidad.Name].Value.ToString());
				fila.SetField("codigo", "");
				fila.SetField("codProducto", prod.CodProducto);
				fila.SetField("referencia", prod.Referencia);
				fila.SetField("descripcion", prod.Descripcion);
				fila.SetField("codUnidad", encontrado[0].Cells[codunidad.Name].Value.ToString());
				fila.SetField("unidad", und.Descripcion);
				fila.SetField("tipo", tipo);
				fila.SetField("ctdadNC", cantidadNC.ToString());
				string codAlmacen = alm.CodAlmacen.ToString();
				string descAlmacen = alm.Descripcion;
				if (!dataFormIntermedio.Columns.Contains(codAlmacen))
				{
					almacenes.Add(new string[2] { codAlmacen, descAlmacen });
					dataFormIntermedio.Columns.Add(codAlmacen);
					dataFormIntermedio.Columns.Add(descAlmacen);
					fila.SetField(descAlmacen, aux.cantidad.ToString());
				}
				else
				{
					fila.SetField(descAlmacen, aux.cantidad.ToString());
				}
				dataFormIntermedio.Rows.Add(fila);
				return true;
			}
			throw new Exception("No se especifico o no se encontro la cantidad de nota de credito para el producto: " + prod.CodProducto);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error de Verificacion de Despacho Para Anulacion Parcial", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
	}

	private bool agregarProductoARetornarEnVariosAlmacenes(List<DataRow> busqueda3)
	{
		try
		{
			inicializaDataForIntermedio();
			int i = 0;
			DataRow fila = dataFormIntermedio.NewRow();
			foreach (DataRow item in busqueda3)
			{
				if (i == 0)
				{
					DataRow detalle = busqueda3[0];
					List<DataGridViewRow> encontrado = Enumerable.Where<DataGridViewRow>(dgvDetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => x.Cells[codproducto.Name].Value.ToString() == detalle.Field<object>("codProducto").ToString())).ToList();
					if (encontrado.Count() != 1)
					{
						throw new Exception("No se especifico o no se encontro la cantidad de nota de credito para el producto: " + detalle.Field<object>("codProducto").ToString());
					}
					double cantidadNC = Convert.ToDouble(encontrado[0].Cells[cantidad.Name].Value.ToString());
					fila.SetField("codigo", detalle.Field<object>("codDetalleDespacho").ToString());
					fila.SetField("codProducto", detalle.Field<object>("codProducto").ToString());
					fila.SetField("referencia", detalle.Field<object>("referencia").ToString());
					fila.SetField("descripcion", detalle.Field<object>("descProducto").ToString());
					fila.SetField("codUnidad", encontrado[0].Cells[codunidad.Name].Value.ToString());
					fila.SetField("unidad", detalle.Field<object>("descUnidad").ToString());
					fila.SetField("tipo", 0);
					fila.SetField("ctdadNC", cantidadNC.ToString());
				}
				string codAlmacen = item.Field<object>("codAlmacen").ToString();
				string descAlmacen = item.Field<object>("descAlmacen").ToString();
				if (!dataFormIntermedio.Columns.Contains(codAlmacen))
				{
					almacenes.Add(new string[2] { codAlmacen, descAlmacen });
					dataFormIntermedio.Columns.Add(codAlmacen);
					dataFormIntermedio.Columns.Add(descAlmacen);
					fila.SetField(descAlmacen, item.Field<object>("ctdadPendiente").ToString());
				}
				else
				{
					fila.SetField(descAlmacen, item.Field<object>("ctdadPendiente").ToString());
				}
				i++;
			}
			dataFormIntermedio.Rows.Add(fila);
			return true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error de Verificacion de Despacho Para Anulacion Parcial", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
	}

	private void inicializaDataForIntermedio()
	{
		try
		{
			if (dataFormIntermedio.Rows.Count <= 0)
			{
				dataFormIntermedio.Columns.Add("codigo");
				dataFormIntermedio.Columns.Add("codProducto");
				dataFormIntermedio.Columns.Add("referencia");
				dataFormIntermedio.Columns.Add("descripcion");
				dataFormIntermedio.Columns.Add("codUnidad");
				dataFormIntermedio.Columns.Add("unidad");
				dataFormIntermedio.Columns.Add("ctdadNC");
				dataFormIntermedio.Columns.Add("tipo");
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private List<clsDetalleTransferencia> obtenerDetalleParaTransferencia(clsRequerimientoAlmacen req_alm, clsTransferencia extornacion)
	{
		List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();
		DataTable detalleTransf = admTransferencia.CargaDetalle(extornacion.codTransAExtornar);
		foreach (DataRow fila in detalleTransf.Rows)
		{
			clsDetalleTransferencia deta = new clsDetalleTransferencia();
			double _cantidad = Convert.ToDouble(fila.Field<object>("cantidad"));
			deta.CodProducto = Convert.ToInt32(fila.Field<object>("codProducto"));
			deta.CodAlmacenOrigen = req_alm.CodAlmacenSolicitante;
			deta.CodAlmacenDestino = req_alm.CodAlmacenDespacho;
			deta.UnidadIngresada = Convert.ToInt32(fila.Field<object>("codUnidadMedida"));
			deta.SerieLote = "";
			deta.Cantidad = _cantidad;
			deta.CantidadPendiente = _cantidad;
			double ult_pre = (deta.PrecioUnitario = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(deta.CodProducto, deta.UnidadIngresada, 0)));
			deta.Subtotal = ult_pre * deta.Cantidad;
			deta.Descuento1 = 0.0;
			deta.Descuento2 = 0.0;
			deta.Descuento3 = 0.0;
			deta.MontoDescuento = 0.0;
			bool flag = true;
			deta.PrecioVenta = deta.Subtotal;
			double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			deta.ValorVenta = deta.PrecioVenta / factorigv;
			deta.PrecioReal = deta.PrecioVenta / deta.Cantidad;
			deta.ValoReal = deta.ValorVenta / deta.Cantidad;
			deta.Igv = deta.PrecioVenta - deta.ValorVenta;
			deta.Importe = deta.Subtotal;
			deta.Valorpromedio = Convert.ToDecimal(deta.PrecioUnitario);
			deta.CodUser = frmLogin.iCodUser;
			detalle.Add(deta);
			extornacion.MontoBruto += Convert.ToDecimal(deta.Importe);
			extornacion.MontoDscto += Convert.ToDecimal(deta.MontoDescuento);
			extornacion.Igv += Convert.ToDecimal(deta.Igv);
			extornacion.Total += Convert.ToDecimal(deta.Subtotal);
		}
		return detalle;
	}

	private List<clsDetalleTransferencia> obtenerDetalleParaTransferencia_NCParcial(clsRequerimientoAlmacen req_alm, clsTransferencia extornacion)
	{
		List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();
		foreach (ProdTieneDespacho item in prodsBandDespacho)
		{
			if (item.tieneRequerimiento && item.cantidad != 0.0)
			{
				clsDetalleTransferencia deta = new clsDetalleTransferencia();
				double _cantidad = item.cantidad;
				deta.CodProducto = item.codProducto;
				deta.CodAlmacenOrigen = req_alm.CodAlmacenSolicitante;
				deta.CodAlmacenDestino = req_alm.CodAlmacenDespacho;
				deta.UnidadIngresada = item.codUnidad;
				deta.SerieLote = "";
				deta.Cantidad = _cantidad;
				deta.CantidadPendiente = _cantidad;
				double ult_pre = (deta.PrecioUnitario = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(deta.CodProducto, deta.UnidadIngresada, 0)));
				deta.Subtotal = ult_pre * deta.Cantidad;
				deta.Descuento1 = 0.0;
				deta.Descuento2 = 0.0;
				deta.Descuento3 = 0.0;
				deta.MontoDescuento = 0.0;
				bool flag = true;
				deta.PrecioVenta = deta.Subtotal;
				double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
				deta.ValorVenta = deta.PrecioVenta / factorigv;
				deta.PrecioReal = deta.PrecioVenta / deta.Cantidad;
				deta.ValoReal = deta.ValorVenta / deta.Cantidad;
				deta.Igv = deta.PrecioVenta - deta.ValorVenta;
				deta.Importe = deta.Subtotal;
				deta.Valorpromedio = Convert.ToDecimal(deta.PrecioUnitario);
				deta.CodUser = frmLogin.iCodUser;
				detalle.Add(deta);
				extornacion.MontoBruto += Convert.ToDecimal(deta.Importe);
				extornacion.MontoDscto += Convert.ToDecimal(deta.MontoDescuento);
				extornacion.Igv += Convert.ToDecimal(deta.Igv);
				extornacion.Total += Convert.ToDecimal(deta.Subtotal);
			}
		}
		return detalle;
	}

	private void apruebaTransferencia(clsTransferencia extornacion, List<clsDetalleTransferencia> detalle)
	{
		try
		{
			clsTipoDocumento doc = new clsTipoDocumento();
			clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();
			tran = AdmTran.MuestraTransaccion(15);
			doc = admtd.BuscaTipoDocumento("DET");
			NS = new clsNotaSalida();
			NS.NumDoc = extornacion.Numerodocumento;
			NS.CodAlmacen = extornacion.CodAlmacenOrigen;
			NS.CodCliente = 0;
			NS.CodNotaCredito = 0;
			NS.CodSucursal = extornacion.CodAlmacenOrigen;
			NS.RazonSocialCliente = "";
			NS.CodAutorizado = 0;
			NS.FechaSalida = DateTime.Now.Date;
			NS.DocumentoReferencia = 0;
			NS.CodTipoTransaccion = tran.CodTransaccion;
			NS.CodTipoDocumento = doc.CodTipoDocumento;
			NS.CodSerie = extornacion.Codserie;
			NS.Serie = extornacion.Serie;
			NS.Moneda = 1;
			NS.FechaSalida = DateTime.Now.Date;
			NS.FormaPago = 0;
			NS.FechaPago = DateTime.Now.Date;
			NS.Comentario = "";
			NS.MontoBruto = Convert.ToDouble(extornacion.MontoBruto);
			NS.MontoDscto = 0.0;
			NS.Igv = 0.0;
			NS.Total = Convert.ToDouble(extornacion.Total);
			NS.CodUser = extornacion.CodUser;
			NS.Estado = 1;
			NS.Codtransferencia = Convert.ToInt32(extornacion.CodTransDir);
			using (TransactionScope Scope = new TransactionScope())
			{
				if (admNS.insert(NS))
				{
					RecorreDetalleNS(extornacion, detalle);
					if (detalleNS.Count > 0)
					{
						foreach (clsDetalleNotaSalida det in detalleNS)
						{
							if (!admNS.insertdetalle(det))
							{
								bandera = false;
								codproducto_error = det.CodProducto;
								Transaction.Current.Rollback();
								Scope.Dispose();
								break;
							}
						}
						if (bandera)
						{
							Scope.Complete();
							Scope.Dispose();
						}
					}
				}
				else
				{
					Transaction.Current.Rollback();
					Scope.Dispose();
					MessageBox.Show("Hubo un error al registrar la salida de producto ", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (bandera)
			{
				NI = new clsNotaIngreso();
				NI.NumDoc = NS.NumDoc;
				NI.CodAlmacen = extornacion.CodAlmacenDestino;
				NI.CodAutorizado = 0;
				NI.CodReferencia = 0;
				NI.CodTipoTransaccion = tran.CodTransaccion;
				NI.CodTipoDocumento = doc.CodTipoDocumento;
				NI.CodSerie = NS.CodSerie;
				NI.Serie = NS.Serie;
				NI.Moneda = 1;
				NI.FechaIngreso = DateTime.Now.Date;
				NI.FormaPago = 0;
				NI.FechaPago = DateTime.Now.Date;
				NI.Comentario = "";
				NS.MontoBruto = Convert.ToDouble(extornacion.MontoBruto);
				NI.MontoDscto = 0.0;
				NI.Igv = 0.0;
				NI.Total = Convert.ToDouble(extornacion.Total);
				NI.CodUser = extornacion.CodUser;
				NI.Estado = 1;
				NI.Codtransferencia = Convert.ToInt32(extornacion.CodTransDir);
				using (TransactionScope Scope2 = new TransactionScope())
				{
					if (admNI.insert(NI))
					{
						RecorreDetalleNI(extornacion, detalle);
						if (detalleNI.Count > 0)
						{
							foreach (clsDetalleNotaIngreso det2 in detalleNI)
							{
								if (!admNI.insertdetalle(det2))
								{
									bandera = false;
									codproducto_error = det2.CodProducto;
									Transaction.Current.Rollback();
									Scope2.Dispose();
									break;
								}
							}
						}
						if (bandera)
						{
							Scope2.Complete();
							Scope2.Dispose();
						}
					}
					else
					{
						Transaction.Current.Rollback();
						Scope2.Dispose();
						MessageBox.Show("Hubo un error al registrar el ingreso de productos", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				if (bandera)
				{
					admTransferencia.Aprobar(Convert.ToInt32(extornacion.CodTransDir));
				}
				else
				{
					MessageBox.Show("Hubo un error al guardar el Documento de Extornacion", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error, "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void RecorreDetalleNS(clsTransferencia extornacion, List<clsDetalleTransferencia> detalle)
	{
		detalleNS.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNS(row, extornacion);
		}
	}

	private void añadedetalleNS(clsDetalleTransferencia fila, clsTransferencia extornacion)
	{
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		deta.CodProducto = fila.CodProducto;
		deta.CodNotaSalida = Convert.ToInt32(NS.CodNotaSalida);
		deta.CodAlmacen = extornacion.CodAlmacenOrigen;
		deta.UnidadIngresada = fila.UnidadIngresada;
		deta.SerieLote = "0";
		deta.Cantidad = fila.Cantidad;
		deta.PrecioUnitario = fila.PrecioUnitario;
		deta.Subtotal = fila.Subtotal;
		deta.Descuento1 = fila.Descuento1;
		deta.Descuento2 = fila.Descuento2;
		deta.Descuento3 = fila.Descuento3;
		deta.Igv = fila.Igv;
		deta.Importe = fila.PrecioVenta;
		deta.PrecioReal = fila.PrecioReal;
		deta.ValoReal = fila.ValoReal;
		deta.ValorPromedio = Convert.ToDouble(fila.Valorpromedio);
		deta.CodUser = frmLogin.iCodUser;
		detalleNS.Add(deta);
	}

	private void RecorreDetalleNI(clsTransferencia extornacion, List<clsDetalleTransferencia> detalle)
	{
		detalleNI.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNI(row, extornacion);
		}
	}

	private void añadedetalleNI(clsDetalleTransferencia fila, clsTransferencia extornacion)
	{
		clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
		deta1.CodProducto = fila.CodProducto;
		deta1.CodNotaIngreso = Convert.ToInt32(NI.CodNotaIngreso);
		deta1.CodAlmacen = extornacion.CodAlmacenDestino;
		deta1.UnidadIngresada = fila.UnidadIngresada;
		deta1.SerieLote = "0";
		deta1.Cantidad = fila.Cantidad;
		deta1.PrecioUnitario = fila.PrecioUnitario;
		deta1.Subtotal = fila.Subtotal;
		deta1.Descuento1 = fila.Descuento1;
		deta1.Descuento2 = fila.Descuento2;
		deta1.Descuento3 = fila.Descuento3;
		deta1.MontoDescuento = 0.0;
		deta1.ValoReal = deta1.PrecioUnitario / 1.18;
		deta1.Igv = deta1.ValoReal * 0.18;
		deta1.PrecioReal = deta1.ValoReal * 1.18;
		deta1.CodUser = frmLogin.iCodUser;
		deta1.Importe = deta1.PrecioUnitario * deta1.Cantidad;
		deta1.Subtotal = deta1.Importe;
		deta1.PrecioReal = fila.PrecioUnitario;
		deta1.ValoReal = fila.ValoReal;
		deta1.CodProveedor = 0;
		deta1.FechaIngreso = DateTime.Now;
		detalleNI.Add(deta1);
	}

	private void cargarTotalesSunat()
	{
		montogratuitas = default(decimal);
		montogravadas = default(decimal);
		montoexoneradas = default(decimal);
		montoinafectas = default(decimal);
		if (dgvDetalle.RowCount > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				if (Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "10" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "11" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "12" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "13" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "14" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "15" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "16" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "17")
				{
					montogravadas += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
				}
				if (Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "20")
				{
					montoexoneradas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				}
				if (Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "30" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "31" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "32" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "33" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "34" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "35" || Convert.ToString(row.Cells[tipoimpuesto.Name].Value) == "36")
				{
					montoinafectas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				}
			}
			return;
		}
		montogratuitas = default(decimal);
		montoexoneradas = default(decimal);
		montogravadas = default(decimal);
		montoinafectas = default(decimal);
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		detalleNotaCredito.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleNotaIngreso deta = new clsDetalleNotaIngreso();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		if (Proceso == 3)
		{
			deta.CodNotaIngreso = Convert.ToInt32(notc.CodNotaCredito);
		}
		else
		{
			deta.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
		}
		deta.CodAlmacen = notc.CodAlmacen;
		deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[valorventa.Name].Value);
		deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.FechaIngreso = dtpFecha.Value;
		deta.CodUser = frmLogin.iCodUser;
		detalle.Add(deta);
		clsDetalleNotaCredito detafac = new clsDetalleNotaCredito();
		if (Proceso == 3)
		{
			detafac.CodNotaCredito = CodNC;
		}
		else
		{
			detafac.CodNotaCredito = notc.CodNotaCreditoNueva;
		}
		detafac.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detafac.CodNotaIngreso = nota.CodNotaIngreso;
		detafac.CodAlmacen = notc.CodAlmacen;
		detafac.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detafac.SerieLote = "0";
		detafac.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		detafac.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detafac.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detafac.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detafac.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detafac.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detafac.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detafac.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		detafac.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		detafac.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		detafac.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		detafac.FechaIngreso = dtpFecha.Value;
		detafac.DescripcionNC = Convert.ToString(fila.Cells[descripcion.Name].Value);
		detafac.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		detafac.CodUser = frmLogin.iCodUser;
		detafac.TipoImpuesto = "10";
		detafac.icbper = Convert.ToDecimal(fila.Cells[icbper.Name].Value);
		detafac.icbper_band = Convert.ToBoolean(fila.Cells[icbper_band.Name].Value);
		detalleNotaCredito.Add(detafac);
	}

	private void CargaFilaDetalle(DataGridViewRow fila)
	{
		detaSelec.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		if (Proceso == 3)
		{
			detaSelec.CodNotaIngreso = Convert.ToInt32(notc.CodNotaCredito);
		}
		else
		{
			detaSelec.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
		}
		detaSelec.CodAlmacen = notc.CodAlmacen;
		detaSelec.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		detaSelec.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detaSelec.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		detaSelec.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		detaSelec.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detaSelec.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detaSelec.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detaSelec.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detaSelec.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detaSelec.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detaSelec.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		detaSelec.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		detaSelec.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		detaSelec.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		detaSelec.FechaIngreso = dtpFecha.Value;
		detaSelec.CodUser = frmLogin.iCodUser;
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			DataGridViewRow row = dgvDetalle.SelectedRows[0];
			if (Application.OpenForms["frmDetalleIngreso"] != null)
			{
				Application.OpenForms["frmDetalleIngreso"].Activate();
				return;
			}
			frmDetalleIngreso form = new frmDetalleIngreso();
			form.Proceso = 2;
			form.Procede = 7;
			form.bvalorventa = cbValorVenta.Checked;
			form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
			form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
			form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
			form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
			form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
			form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
			form.txtDscto2.Text = row.Cells[dscto2.Name].Value.ToString();
			form.txtDscto3.Text = row.Cells[dscto3.Name].Value.ToString();
			form.txtPrecioNeto.Text = row.Cells[importe.Name].Value.ToString();
			form.ShowDialog();
		}
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (base.Visible && dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
		{
			CargaFilaDetalle(e.Row);
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		cantpr = new List<decimal>();
		cantprec = new List<decimal>();
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			cantpr.Add(Convert.ToInt32(row.Cells[cantidad.Name].Value));
			cantprec.Add(Convert.ToDecimal(row.Cells[preciounit.Name].Value));
		}
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso == 1)
		{
			CalculaTotales();
		}
	}

	private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
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
		if (CodCliente != 0)
		{
			CargaCliente();
			BorrarNota();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		if (cli != null)
		{
			txtCodCliente.Text = cli.CodigoPersonalizado;
			txtNombreCliente.Text = cli.RazonSocial;
			txtDireccionCliente.Text = cli.DireccionLegal;
		}
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtCodCliente.Text != "")
		{
			if (BuscaCliente())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("El Cliente no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE SALIDA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaCliente()
	{
		cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
		if (cli != null)
		{
			txtCodCliente.Text = cli.CodigoPersonalizado;
			txtNombreCliente.Text = cli.RazonSocial;
			CodCliente = cli.CodCliente;
			return true;
		}
		txtNombreCliente.Text = "";
		CodCliente = 0;
		return false;
	}

	private void txtCodCliente_Leave(object sender, EventArgs e)
	{
		if (CodCliente == 0)
		{
			txtCodCliente.Focus();
		}
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			double igvsis = 0.0;
			double impor = 0.0;
			double imptotal = 0.0;
			double cant = 0.0;
			double preuni = 0.0;
			double valorven = 0.0;
			double igvT = 0.0;
			double total_icbper = 0.0;
			if (Convert.ToString(cmbMotivo.SelectedValue) == "04")
			{
				DataGridViewRow row = dgvDetalle.Rows[e.RowIndex];
				impor = Convert.ToDouble(row.Cells[importe.Name].Value);
				if (Convert.ToInt32(dgvDetalle.CurrentRow.Cells[tipoimpuesto.Name].Value) == 10)
				{
					igvsis = frmLogin.Configuracion.IGV;
					imptotal = impor / (1.0 + igvsis / 100.0);
				}
				else
				{
					imptotal = impor;
				}
				row.Cells[valorventa.Name].Value = imptotal;
				row.Cells[igv.Name].Value = impor - imptotal;
				row.Cells[precioventa.Name].Value = impor;
			}
			else if (Convert.ToString(cmbMotivo.SelectedValue) == "05" || Convert.ToString(cmbMotivo.SelectedValue) == "07" || Convert.ToString(cmbMotivo.SelectedValue) == "09")
			{
				DataGridViewRow row2 = dgvDetalle.Rows[e.RowIndex];
				cant = Convert.ToDouble(row2.Cells[cantidad.Name].Value);
				preuni = Convert.ToDouble(row2.Cells[preciounit.Name].Value);
				impor = cant * preuni;
				row2.Cells[importe.Name].Value = impor;
				if (Convert.ToInt32(dgvDetalle.CurrentRow.Cells[tipoimpuesto.Name].Value) == 10)
				{
					igvsis = frmLogin.Configuracion.IGV;
					valorven = impor / (1.0 + igvsis / 100.0);
				}
				else
				{
					valorven = impor;
				}
				if (Convert.ToDecimal(row2.Cells[icbper.Name].Value) > 0m && Convert.ToBoolean(row2.Cells[importe.Name].Value))
				{
					row2.Cells[icbper.Name].Value = cant * Convert.ToDouble(frmLogin.Configuracion.Icbper);
				}
				igvT = impor - valorven;
				row2.Cells[valorventa.Name].Value = impor - igvT;
				row2.Cells[igv.Name].Value = igvT;
				row2.Cells[precioventa.Name].Value = impor;
			}
			CalculaTotales();
		}
		catch (Exception)
		{
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		printNC();
	}

	public void printNC()
	{
		try
		{
			if (Proceso == 3)
			{
				ser = AdmSerie.MuestraSerie(notc.CodSerie, notc.CodAlmacen);
			}
			else
			{
				ser = AdmSerie.MuestraSerie(nota.CodSerie, nota.CodAlmacen);
			}
			DataSet jes = new DataSet();
			frmRptNotaCredito form = new frmRptNotaCredito();
			CRNotaCreditoVentaTicket rpt = new CRNotaCreditoVentaTicket();
			jes = ((Proceso != 3) ? ds.ReportNotaCreditoVenta(Convert.ToInt32(CodNota), nota.CodAlmacen) : ds.ReportNotaCreditoVenta(Convert.ToInt32(CodNC), notaS.CodAlmacen));
			string nombrearchivo = "";
			if (Proceso == 3)
			{
				venta = AdmVenta.BuscaFacturaVenta(Convert.ToInt32(CodNota), notaS.CodAlmacen);
				notc = AdmFact.CargaNotaCredito(CodNotaS);
			}
			else
			{
				venta = AdmVenta.BuscaFacturaVenta(Convert.ToInt32(CodNotaS), nota.CodAlmacen);
				notc = AdmFact.CargaNotaCredito(Convert.ToInt32(nota.CodNotaIngreso));
			}
			empre = admempre.CargaEmpresa(venta.CodEmpresa);
			if (venta.CodTipoDocumento == 1)
			{
				nombrearchivo = empre.Ruc + "-07-B" + notc.Serie + "-" + notc.DocumentoNotaCredito.PadLeft(8, '0');
			}
			else if (venta.CodTipoDocumento == 2)
			{
				nombrearchivo = empre.Ruc + "-07-F" + notc.Serie + "-" + notc.DocumentoNotaCredito.PadLeft(8, '0');
			}
			firmadigital = CargarImagen("C:\\DOCUMENTOS-" + empre.Ruc + "\\CERTIFIK\\QR\\" + nombrearchivo + ".jpeg");
			foreach (DataTable mel in jes.Tables)
			{
				foreach (DataRow changesRow in mel.Rows)
				{
					changesRow["firma"] = firmadigital;
				}
				if (!mel.HasErrors)
				{
					continue;
				}
				foreach (DataRow changesRow2 in mel.Rows)
				{
					if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
					{
						changesRow2.RejectChanges();
						changesRow2.ClearErrors();
					}
				}
			}
			rpt.SetDataSource(jes);
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(0, 0, 0, 0));
			rpt.PrintToPrinter(1, collated: false, 1, 1);
			rpt.Close();
			rpt.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public static byte[] CargarImagen(string rutaArchivo)
	{
		if (rutaArchivo != "")
		{
			try
			{
				FileStream Archivo = new FileStream(rutaArchivo, FileMode.Open);
				BinaryReader binRead = new BinaryReader(Archivo);
				byte[] imagenEnBytes = new byte[Archivo.Length];
				binRead.Read(imagenEnBytes, 0, (int)Archivo.Length);
				binRead.Close();
				Archivo.Close();
				return imagenEnBytes;
			}
			catch
			{
				return new byte[0];
			}
		}
		return new byte[0];
	}

	private void btnNuevaGuia_Click(object sender, EventArgs e)
	{
		btnGuardar.Enabled = true;
		frmNotadeCredito form2 = new frmNotadeCredito();
		form2.MdiParent = base.MdiParent;
		form2.Proceso = 1;
		form2.Show();
		Close();
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0 && Proceso != 3)
		{
			if (Convert.ToString(cmbMotivo.SelectedValue) != "05" && Convert.ToString(cmbMotivo.SelectedValue) != "07" && Convert.ToString(cmbMotivo.SelectedValue) != "09")
			{
				dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = true;
				dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = true;
			}
			int fila = dgvDetalle.CurrentRow.Index;
			cantprod = cantpr[fila];
			precprod = cantprec[fila];
		}
	}

	private void cmbMotivo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		object algo = cmbMotivo.SelectedValue;
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		if (Convert.ToString(cmbMotivo.SelectedValue) == "05" || Convert.ToString(cmbMotivo.SelectedValue) == "07" || Convert.ToString(cmbMotivo.SelectedValue) == "09")
		{
			if (Convert.ToString(cmbMotivo.SelectedValue) == "07")
			{
				dgvDetalle.Columns[preciounit.Name].HeaderText = "P. Unit.";
				dgvDetalle.Columns[preciounit.Name].ReadOnly = true;
				dgvDetalle.Columns[preciounit.Name].DefaultCellStyle.BackColor = Color.White;
				dgvDetalle.Columns[cantidad.Name].DefaultCellStyle.BackColor = Color.PeachPuff;
				dgvDetalle.Columns[cantidad.Name].ReadOnly = false;
				dgvDetalle.Columns[importe.Name].DefaultCellStyle.BackColor = Color.White;
				dgvDetalle.Columns[descripcion.Name].ReadOnly = true;
				dgvDetalle.Columns[cantidad.Name].ReadOnly = true;
			}
			else
			{
				dgvDetalle.Columns[preciounit.Name].HeaderText = "P. Unit.";
				dgvDetalle.Columns[preciounit.Name].ReadOnly = false;
				dgvDetalle.Columns[preciounit.Name].DefaultCellStyle.BackColor = Color.PeachPuff;
				dgvDetalle.Columns[cantidad.Name].DefaultCellStyle.BackColor = Color.White;
				dgvDetalle.Columns[importe.Name].DefaultCellStyle.BackColor = Color.White;
				dgvDetalle.Columns[descripcion.Name].ReadOnly = true;
				dgvDetalle.Columns[cantidad.Name].ReadOnly = true;
			}
			btnEliminar.Visible = false;
			cmbMovimiento.Visible = true;
			txtComentario.Text = cmbMotivo.GetItemText(cmbMotivo.SelectedItem);
			dgvDetalle.Columns["unidad"].Visible = true;
			dgvDetalle.Columns["cantidad"].Visible = true;
			dgvDetalle.Columns["preciounit"].Visible = true;
			btnEliminar.Visible = true;
		}
		else if (Convert.ToString(cmbMotivo.SelectedValue) == "04")
		{
			try
			{
				DataTable dt = (DataTable)dgvDetalle.DataSource;
				dt.Clear();
				dt.Rows.Add(0, 0, "00", "DESCUENTO GLOBAL", 0, "", "", "0", Convert.ToDouble(0), Convert.ToDouble(0), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, frmLogin.iCodUser, DateTime.Now, "10");
				dgvDetalle.DataSource = dt;
				dgvDetalle.Columns[importe.Name].DefaultCellStyle.BackColor = Color.PeachPuff;
				dgvDetalle.Columns["unidad"].Visible = false;
				dgvDetalle.Columns["cantidad"].Visible = false;
				dgvDetalle.Columns["importe"].ReadOnly = false;
				dgvDetalle.Columns["preciounit"].Visible = false;
				txtComentario.Text = cmbMotivo.GetItemText(cmbMotivo.SelectedItem);
				btnEliminar.Visible = false;
				cmbMovimiento.Visible = true;
				dgvDetalle.ClearSelection();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		else if (Convert.ToString(cmbMotivo.SelectedValue) == "01" || Convert.ToString(cmbMotivo.SelectedValue) == "02" || Convert.ToString(cmbMotivo.SelectedValue) == "08" || Convert.ToString(cmbMotivo.SelectedValue) == "10" || Convert.ToString(cmbMotivo.SelectedValue) == "06" || Convert.ToString(cmbMotivo.SelectedValue) == "03")
		{
			dgvDetalle.Columns[preciounit.Name].HeaderText = "P. Unit.";
			dgvDetalle.Columns[cantidad.Name].DefaultCellStyle.BackColor = Color.White;
			dgvDetalle.Columns[descripcion.Name].DefaultCellStyle.BackColor = Color.White;
			dgvDetalle.Columns[importe.Name].DefaultCellStyle.BackColor = Color.White;
			dgvDetalle.Columns[preciounit.Name].DefaultCellStyle.BackColor = Color.White;
			dgvDetalle.Columns["descripcion"].ReadOnly = true;
			dgvDetalle.Columns["importe"].ReadOnly = true;
			btnEliminar.Visible = false;
			cmbMovimiento.Visible = true;
			txtComentario.Text = cmbMotivo.GetItemText(cmbMotivo.SelectedItem);
			dgvDetalle.Columns["unidad"].Visible = true;
			dgvDetalle.Columns["cantidad"].Visible = true;
			dgvDetalle.Columns["preciounit"].Visible = true;
		}
		if (!(Convert.ToString(cmbMotivo.SelectedValue) != "04"))
		{
		}
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
	}

	private void dgvDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvDetalle_KeyPress;
			txtedit.KeyPress += dgvDetalle_KeyPress;
			txtedit.KeyUp -= dgvDetalle_KeyUp;
			txtedit.KeyUp += dgvDetalle_KeyUp;
			txtedit.Leave -= dgvDetalle_Leave;
			txtedit.Leave += dgvDetalle_Leave;
		}
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == 10 || dgvDetalle.CurrentCell.ColumnIndex == 11)
		{
			ok.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
		CalculaTotales();
	}

	private void dgvDetalle_Leave(object sender, EventArgs e)
	{
		CalculaTotales();
	}

	private void txtDocRefe_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Activate();
		}
		else if (cli.Ruc != "")
		{
			frmDocumentos form = new frmDocumentos();
			form.Proceso = 3;
			form.ShowDialog();
			doc = form.doc;
			CodDocumento = doc.CodTipoDocumento;
			txtCodDocumento.Text = CodDocumento.ToString();
			txtDocRefe.Text = doc.Sigla;
			if (CodDocumento != 0)
			{
				ProcessTabKey(forward: true);
			}
		}
	}

	private void txtDocRefe_KeyPress(object sender, KeyPressEventArgs e)
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

	private void txtSerie_Leave(object sender, EventArgs e)
	{
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

	private void button1_Click(object sender, EventArgs e)
	{
		notc.NumFac = notc.DocumentoNotaCredito;
		notc.FormaPago = 7;
		notc.CodNotaCreditoNueva = Convert.ToInt32(notc.CodNotaCredito);
		con.DatosNCredito(cli, notc, detalleNotaCredito);
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void btnProductosSeleccionadoDespacho_Click(object sender, EventArgs e)
	{
		try
		{
			DataTable dtAlmSelecc = admseldesnc.cargaAlmacenesDeSeleccionDeNC(codActualNC);
			List<string[]> _almacenes = convertirDatableEnListadoAlmacenes(dtAlmSelecc);
			DataTable _dataFormIntermedio = admseldesnc.cargaDataParaInterfazSeleccionador(codActualNC);
			frmIntermedioNotaCreditoDespacho form = new frmIntermedioNotaCreditoDespacho();
			form.almacenes = _almacenes;
			form.data = _dataFormIntermedio;
			form.Proceso = 2;
			form.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ocurrio Un Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private List<string[]> convertirDatableEnListadoAlmacenes(DataTable dtAlmSelecc)
	{
		List<string[]> aux = new List<string[]>();
		foreach (DataRow fila in dtAlmSelecc.Rows)
		{
			string codAlmacen = fila.Field<object>("codAlmacen").ToString();
			string descAlmacen = fila.Field<object>("nombre").ToString();
			aux.Add(new string[2] { codAlmacen, descAlmacen });
		}
		return aux;
	}

	private void txtDocRefe_Leave(object sender, EventArgs e)
	{
		BuscaTipoDocumento();
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && BuscaSerie())
		{
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumero.Visible = true;
				txtNumero.Enabled = false;
				txtNumero.Focus();
				txtNumero.Text = "";
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Enabled = false;
				txtNumero.Text = ser.Numeracion.ToString();
			}
			ProcessTabKey(forward: true);
		}
		if (e.KeyChar != '\r')
		{
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

	private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		dgvDetalle.ClearSelection();
		if (dgvDetalle.Rows.Count > 0 && Proceso != 3)
		{
			if (Convert.ToString(cmbMotivo.SelectedValue) == "05" || Convert.ToString(cmbMotivo.SelectedValue) == "09")
			{
				dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = true;
				dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
			}
			else if (Convert.ToString(cmbMotivo.SelectedValue) == "07")
			{
				dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = false;
				dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = true;
			}
			int fila = dgvDetalle.CurrentRow.Index;
			cantprod = cantpr[fila];
			precprod = cantprec[fila];
		}
	}

	private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotadeCredito));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnProductosSeleccionadoDespacho = new System.Windows.Forms.Button();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtCodDocumento = new System.Windows.Forms.TextBox();
		this.label37 = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtDocRefe = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.cmbMovimiento = new System.Windows.Forms.ComboBox();
		this.cbAplicada = new System.Windows.Forms.CheckBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbMotivo = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtDireccionCliente = new System.Windows.Forms.TextBox();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.cbValorVenta = new System.Windows.Forms.CheckBox();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.btnNuevaGuia = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txticbper = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtPrecioVenta = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.flete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoimpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.icbper = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.icbper_band = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.requiredFieldValidator1 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.cbSaldoaFavor = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox1.Controls.Add(this.cbSaldoaFavor);
		this.groupBox1.Controls.Add(this.btnProductosSeleccionadoDespacho);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtCodDocumento);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtDocRefe);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.cmbMovimiento);
		this.groupBox1.Controls.Add(this.cbAplicada);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.cmbMotivo);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtDireccionCliente);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.cbValorVenta);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtNumDoc);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.lbNombreTransaccion);
		this.groupBox1.Controls.Add(this.txtTransaccion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(940, 190);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.btnProductosSeleccionadoDespacho.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnProductosSeleccionadoDespacho.BackColor = System.Drawing.Color.SteelBlue;
		this.btnProductosSeleccionadoDespacho.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnProductosSeleccionadoDespacho.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnProductosSeleccionadoDespacho.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnProductosSeleccionadoDespacho.ForeColor = System.Drawing.SystemColors.Control;
		this.btnProductosSeleccionadoDespacho.Location = new System.Drawing.Point(686, 109);
		this.btnProductosSeleccionadoDespacho.Name = "btnProductosSeleccionadoDespacho";
		this.btnProductosSeleccionadoDespacho.Size = new System.Drawing.Size(88, 36);
		this.btnProductosSeleccionadoDespacho.TabIndex = 504;
		this.btnProductosSeleccionadoDespacho.Text = "Detalle de Producto NC";
		this.btnProductosSeleccionadoDespacho.UseVisualStyleBackColor = false;
		this.btnProductosSeleccionadoDespacho.Visible = false;
		this.btnProductosSeleccionadoDespacho.Click += new System.EventHandler(btnProductosSeleccionadoDespacho_Click);
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(472, 101);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 502;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(326, 101);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(140, 20);
		this.cmbFormaPago.TabIndex = 501;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.Visible = false;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(241, 104);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(79, 13);
		this.label17.TabIndex = 503;
		this.label17.Tag = "16";
		this.label17.Text = "Forma de Pago";
		this.label17.Visible = false;
		this.txtCodDocumento.Location = new System.Drawing.Point(558, 14);
		this.txtCodDocumento.Name = "txtCodDocumento";
		this.txtCodDocumento.Size = new System.Drawing.Size(39, 20);
		this.txtCodDocumento.TabIndex = 500;
		this.txtCodDocumento.Visible = false;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(472, 18);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(8, 12);
		this.label37.TabIndex = 499;
		this.label37.Text = "-";
		this.txtNumero.Enabled = false;
		this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumero.Location = new System.Drawing.Point(483, 16);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(69, 18);
		this.txtNumero.TabIndex = 497;
		this.superValidator1.SetValidator1(this.txtNumero, this.customValidator1);
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.Location = new System.Drawing.Point(432, 16);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(38, 18);
		this.txtSerie.TabIndex = 496;
		this.txtSerie.Tag = "13";
		this.txtSerie.Visible = false;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.txtDocRefe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRefe.Enabled = false;
		this.txtDocRefe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRefe.Location = new System.Drawing.Point(398, 16);
		this.txtDocRefe.Name = "txtDocRefe";
		this.txtDocRefe.ReadOnly = true;
		this.txtDocRefe.Size = new System.Drawing.Size(28, 18);
		this.txtDocRefe.TabIndex = 495;
		this.txtDocRefe.Tag = "10";
		this.txtDocRefe.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRefe_KeyDown);
		this.txtDocRefe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRefe_KeyPress);
		this.txtDocRefe.Leave += new System.EventHandler(txtDocRefe_Leave);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(342, 19);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(45, 12);
		this.label6.TabIndex = 498;
		this.label6.Text = "Doc. Ref.";
		this.cmbMovimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMovimiento.FormattingEnabled = true;
		this.cmbMovimiento.Items.AddRange(new object[1] { "Devolucion Dinero" });
		this.cmbMovimiento.Location = new System.Drawing.Point(686, 158);
		this.cmbMovimiento.Name = "cmbMovimiento";
		this.cmbMovimiento.Size = new System.Drawing.Size(242, 21);
		this.cmbMovimiento.TabIndex = 494;
		this.cbAplicada.AutoSize = true;
		this.cbAplicada.Checked = true;
		this.cbAplicada.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbAplicada.Location = new System.Drawing.Point(530, 153);
		this.cbAplicada.Name = "cbAplicada";
		this.cbAplicada.Size = new System.Drawing.Size(67, 17);
		this.cbAplicada.TabIndex = 493;
		this.cbAplicada.Text = "Aplicada";
		this.cbAplicada.UseVisualStyleBackColor = true;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(22, 161);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 13);
		this.label3.TabIndex = 492;
		this.label3.Tag = "21";
		this.label3.Text = "Motivo";
		this.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMotivo.FormattingEnabled = true;
		this.cmbMotivo.Location = new System.Drawing.Point(103, 153);
		this.cmbMotivo.Name = "cmbMotivo";
		this.cmbMotivo.Size = new System.Drawing.Size(242, 21);
		this.cmbMotivo.TabIndex = 491;
		this.cmbMotivo.SelectedIndexChanged += new System.EventHandler(cmbMotivo_SelectionChangeCommitted);
		this.cmbMotivo.SelectionChangeCommitted += new System.EventHandler(cmbMotivo_SelectionChangeCommitted);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 74);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(55, 13);
		this.label4.TabIndex = 90;
		this.label4.Text = "Dirección:";
		this.txtDireccionCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccionCliente.Enabled = false;
		this.txtDireccionCliente.Location = new System.Drawing.Point(101, 71);
		this.txtDireccionCliente.Name = "txtDireccionCliente";
		this.txtDireccionCliente.ReadOnly = true;
		this.txtDireccionCliente.Size = new System.Drawing.Size(452, 20);
		this.txtDireccionCliente.TabIndex = 89;
		this.txtDireccionCliente.Tag = "21";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(196, 44);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(357, 20);
		this.txtNombreCliente.TabIndex = 10;
		this.txtNombreCliente.Tag = "2";
		this.txtNombreCliente.Visible = false;
		this.txtCodCliente.Location = new System.Drawing.Point(102, 44);
		this.txtCodCliente.MaxLength = 11;
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(88, 20);
		this.txtCodCliente.TabIndex = 9;
		this.txtCodCliente.Tag = "1";
		this.txtCodCliente.Visible = false;
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(17, 47);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(39, 13);
		this.label18.TabIndex = 44;
		this.label18.Tag = "1";
		this.label18.Text = "Cliente";
		this.label18.Visible = false;
		this.cbValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cbValorVenta.AutoSize = true;
		this.cbValorVenta.Checked = true;
		this.cbValorVenta.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbValorVenta.Location = new System.Drawing.Point(847, 99);
		this.cbValorVenta.Name = "cbValorVenta";
		this.cbValorVenta.Size = new System.Drawing.Size(81, 17);
		this.cbValorVenta.TabIndex = 15;
		this.cbValorVenta.Text = "Valor Venta";
		this.cbValorVenta.UseVisualStyleBackColor = true;
		this.cbValorVenta.Visible = false;
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(847, 71);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 6;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(847, 44);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 21);
		this.cmbMoneda.TabIndex = 5;
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(767, 74);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 21;
		this.label16.Text = "Tipo/Cambio :";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(789, 47);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(52, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Moneda :";
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Enabled = false;
		this.btnDetalle.Location = new System.Drawing.Point(853, 122);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(75, 23);
		this.btnDetalle.TabIndex = 18;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Visible = false;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.txtComentario.BackColor = System.Drawing.SystemColors.Control;
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(103, 127);
		this.txtComentario.MaxLength = 500;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(451, 20);
		this.txtComentario.TabIndex = 17;
		this.txtComentario.Tag = "21";
		this.txtComentario.Visible = false;
		this.txtComentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComentario_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(18, 130);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(34, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Glosa";
		this.label9.Visible = false;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Location = new System.Drawing.Point(686, 18);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 20);
		this.txtNumDoc.TabIndex = 2;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(616, 21);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(103, 99);
		this.txtDocRef.MaxLength = 15;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(88, 20);
		this.txtDocRef.TabIndex = 11;
		this.txtDocRef.Tag = "10";
		this.superValidator1.SetValidator1(this.txtDocRef, this.requiredFieldValidator1);
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 103);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.label5.Visible = false;
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(136, 18);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(200, 18);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.lbNombreTransaccion.Visible = false;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Enabled = false;
		this.txtTransaccion.Location = new System.Drawing.Point(102, 18);
		this.txtTransaccion.MaxLength = 5;
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.ReadOnly = true;
		this.txtTransaccion.Size = new System.Drawing.Size(28, 20);
		this.txtTransaccion.TabIndex = 1;
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(847, 18);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 4;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(798, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox3.Controls.Add(this.button1);
		this.groupBox3.Controls.Add(this.btnNuevaGuia);
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 425);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(940, 48);
		this.groupBox3.TabIndex = 19;
		this.groupBox3.TabStop = false;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(615, 10);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(78, 32);
		this.button1.TabIndex = 29;
		this.button1.Text = "regenerar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnNuevaGuia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevaGuia.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevaGuia.ImageIndex = 1;
		this.btnNuevaGuia.ImageList = this.imageList1;
		this.btnNuevaGuia.Location = new System.Drawing.Point(6, 10);
		this.btnNuevaGuia.Name = "btnNuevaGuia";
		this.btnNuevaGuia.Size = new System.Drawing.Size(105, 32);
		this.btnNuevaGuia.TabIndex = 28;
		this.btnNuevaGuia.Text = "Nueva Nota";
		this.btnNuevaGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevaGuia.UseVisualStyleBackColor = true;
		this.btnNuevaGuia.Visible = false;
		this.btnNuevaGuia.Click += new System.EventHandler(btnNuevaGuia_Click);
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(699, 10);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 32);
		this.btnImprimir.TabIndex = 24;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(866, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 10;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(157, 10);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Visible = false;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(783, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 11;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(234, 10);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 7;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Visible = false;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(306, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Visible = false;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox2.Controls.Add(this.txticbper);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.txtBruto);
		this.groupBox2.Controls.Add(this.txtPrecioVenta);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.txtIGV);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.txtDscto);
		this.groupBox2.Controls.Add(this.txtValorVenta);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 190);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(940, 235);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.txticbper.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txticbper.Location = new System.Drawing.Point(590, 212);
		this.txticbper.Name = "txticbper";
		this.txticbper.ReadOnly = true;
		this.txticbper.Size = new System.Drawing.Size(111, 20);
		this.txticbper.TabIndex = 25;
		this.txticbper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(497, 215);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(52, 13);
		this.label8.TabIndex = 24;
		this.label8.Text = "ICBPER :";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(52, 186);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(823, 209);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(730, 212);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(823, 183);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(730, 186);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(224, 186);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(590, 183);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(497, 186);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(153, 189);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(8, 189);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.moneda, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.flete, this.precioventa, this.precioreal, this.valoreal, this.fechaingreso, this.coduser, this.fecharegistro, this.tipoimpuesto, this.icbper, this.icbper_band);
		this.dgvDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(931, 161);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentClick);
		this.dgvDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellDoubleClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.dgvDetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvDetalle_KeyPress);
		this.dgvDetalle.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvDetalle_KeyUp);
		this.dgvDetalle.Leave += new System.EventHandler(dgvDetalle_Leave);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 90;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 250;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 80;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.ReadOnly = true;
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "N2";
		dataGridViewCellStyle11.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle11;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle12.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle12;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle13.Format = "N2";
		dataGridViewCellStyle13.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle13;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle14;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle15;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle16;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		dataGridViewCellStyle17.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle17;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		dataGridViewCellStyle18.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle18;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle19.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle19;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.flete.DataPropertyName = "flete";
		this.flete.HeaderText = "Flete";
		this.flete.Name = "flete";
		this.flete.ReadOnly = true;
		this.flete.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle20.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle20;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.DataPropertyName = "precioreal";
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.ReadOnly = true;
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.valoreal.DataPropertyName = "valoreal";
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.ReadOnly = true;
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.fechaingreso.DataPropertyName = "fechaingreso";
		this.fechaingreso.HeaderText = "FechaIngre";
		this.fechaingreso.Name = "fechaingreso";
		this.fechaingreso.ReadOnly = true;
		this.fechaingreso.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		this.tipoimpuesto.DataPropertyName = "tipoimpuesto";
		this.tipoimpuesto.HeaderText = "TipoImpouesto";
		this.tipoimpuesto.Name = "tipoimpuesto";
		this.icbper.DataPropertyName = "icbper";
		this.icbper.HeaderText = "icbper";
		this.icbper.Name = "icbper";
		this.icbper.Visible = false;
		this.icbper_band.DataPropertyName = "icbper_band";
		this.icbper_band.HeaderText = "icbper_band";
		this.icbper_band.Name = "icbper_band";
		this.icbper_band.Visible = false;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Your error message here.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.requiredFieldValidator1.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.cbSaldoaFavor.AutoSize = true;
		this.cbSaldoaFavor.Checked = true;
		this.cbSaldoaFavor.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbSaldoaFavor.Location = new System.Drawing.Point(403, 153);
		this.cbSaldoaFavor.Name = "cbSaldoaFavor";
		this.cbSaldoaFavor.Size = new System.Drawing.Size(110, 17);
		this.cbSaldoaFavor.TabIndex = 505;
		this.cbSaldoaFavor.Text = "Sin Saldo a Favor";
		this.cbSaldoaFavor.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(940, 473);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MinimizeBox = false;
		base.Name = "frmNotadeCredito";
		base.ShowInTaskbar = false;
		this.Text = "Nota de Credito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotaIngreso_Load);
		base.Shown += new System.EventHandler(frmNotaIngreso_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
