using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace SIGEFA.Formularios;

public class frmNotaSalida : Office2007Form
{
	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsSerie ser = new clsSerie();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago pag = new clsFormaPago();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmNotaSalida AdmNota = new clsAdmNotaSalida();

	private clsNotaSalida nota = new clsNotaSalida();

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsMoneda mon = new clsMoneda();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmFacturaVenta admVenta = new clsAdmFacturaVenta();

	private clsAdmUsuario AdmUsu = new clsAdmUsuario();

	private clsValidar val = new clsValidar();

	public List<int> config = new List<int>();

	public List<int> documento = new List<int>();

	public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();

	public static BindingSource data = new BindingSource();

	private TextBox txtedit = new TextBox();

	public string CodNota;

	public int CodTransaccion;

	public int CodProveedor;

	public int CodNotaI;

	public int CodCliente;

	public int CodDocumento;

	public int tipomoneda;

	public int CodPedido;

	public int CodSerie;

	public int CodSerieG = 0;

	public int numG = 0;

	public int Tipo;

	public int Manual;

	private bool Validacion = true;

	public int Proceso = 0;

	public int Proce = 0;

	public int Procede = 1;

	private DataTable datosAlmacena = new DataTable();

	private int cont1 = 0;

	private decimal Qnueva = default(decimal);

	private decimal QOriginal = default(decimal);

	private decimal QPorDespachar = default(decimal);

	private decimal QDespachada = default(decimal);

	private decimal QPorDespachar2 = default(decimal);

	private decimal QDespachada2 = default(decimal);

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsProveedor prov = new clsProveedor();

	private clsAdmGuiaRemision AdmGuia = new clsAdmGuiaRemision();

	private clsGuiaRemision guia = new clsGuiaRemision();

	private clsGuia ds = new clsGuia();

	public int CodEmpresaTransporte;

	private clsVehiculoTransporte vehiculotransporte = new clsVehiculoTransporte();

	private clsAdmVehiculoTransporte admVehiculoTransporte = new clsAdmVehiculoTransporte();

	private clsConductor conductor = new clsConductor();

	private clsAdmConductor admConductor = new clsAdmConductor();

	private clsAdmEmpresaTransporte AdmET = new clsAdmEmpresaTransporte();

	private clsEmpresaTransporte empT = new clsEmpresaTransporte();

	public List<clsDetalleGuiaRemision> detalleg = new List<clsDetalleGuiaRemision>();

	private clsAdmSerie admSerie = new clsAdmSerie();

	private int cantprod = 0;

	private List<int> cantpr = new List<int>();

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox txtComentario;

	private Label label9;

	private TextBox txtNumDoc;

	private Label label7;

	private TextBox txtNumero;

	private Label label5;

	private TextBox txtTransaccion;

	private Label label2;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Label label15;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private TextBox txtBruto;

	private TextBox txtPrecioVenta;

	private Label label14;

	private TextBox txtIGV;

	private Label label13;

	private TextBox txtDscto;

	private TextBox txtValorVenta;

	private Label label12;

	private Label label11;

	private Label label10;

	private Label label16;

	private Label label17;

	private TextBox txtAutorizacion;

	private Label label18;

	private TextBox txtSerie;

	private Label lbNombreTransaccion;

	public DataGridView dgvDetalle;

	private Label label3;

	private Label label4;

	private Label label6;

	public TextBox txtCodCliente;

	public TextBox txtNombreCliente;

	public TextBox txtDocRef;

	public TextBox txtDireccion;

	private Label label19;

	private Button btnImprimir;

	public TextBox txtCodigoCli;

	public TextBox txtFactura;

	public ComboBox cmbFormaPago;

	public DateTimePicker dtpFechaPago;

	private Label label8;

	public TextBox txtTipoCambio;

	public ComboBox cmbMoneda;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CheckBox ckbguia;

	private GroupBox groupBox5;

	public TextBox txtSerieG;

	private TextBox txtcodserie;

	private TextBox txtNumeroG;

	private Label label35;

	public TextBox txtRazonSocialTransporte;

	private Label label34;

	private ComboBox cmbTransportista;

	private ComboBox cmbVehiculo;

	private Label label32;

	private Label label33;

	public Button btnDetalle;

	private DataGridViewTextBoxColumn codVenta;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn maxpdscto;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn cantnueva;

	private DataGridViewTextBoxColumn cantdespachada;

	private DataGridViewTextBoxColumn cantpordespachar;

	private DataGridViewTextBoxColumn cantdespachada2;

	private DataGridViewTextBoxColumn cantpordespachar2;

	private Label lblmensaje;

	public TextBox txtCodFac;

	public TextBox txtCodDoc;

	private TextBox txtPorcDescuento;

	private ComboBox cmbUsuario;

	private Label lblresponsable;

	private Label lblareaencargado;

	public ComboBox cmbarea;

	public frmNotaSalida()
	{
		InitializeComponent();
	}

	private void txtTransaccion_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmTransacciones"] != null)
		{
			Application.OpenForms["frmTransacciones"].Activate();
			return;
		}
		frmTransacciones form = new frmTransacciones();
		form.Proceso = 4;
		form.ShowDialog();
		tran = form.tran;
		CodTransaccion = tran.CodTransaccion;
		txtTransaccion.Text = tran.Sigla;
		dgvDetalle.Rows.Clear();
		if (CodTransaccion != 0)
		{
			CargaTransaccion();
			ProcessTabKey(forward: true);
			if (tran.CodTransaccion == 12)
			{
				label5.Visible = false;
				txtDocRef.Visible = false;
				txtNumero.Visible = false;
				ckbguia.Visible = true;
				groupBox5.Visible = true;
				txtCodCliente.Visible = true;
				txtNombreCliente.Visible = true;
				label15.Visible = true;
				label15.Text = "Proveedor";
				btnDetalle.Enabled = false;
				txtCodCliente.Focus();
			}
			else
			{
				label15.Text = "Cliente";
				groupBox5.Visible = false;
				ckbguia.Visible = false;
			}
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
		if (tran.CodTransaccion == 7)
		{
			desactiva_botones(band: false);
		}
	}

	private void desactiva_botones(bool band)
	{
		if (Proceso == 1)
		{
			txtDocRef.ReadOnly = !band;
			txtSerie.ReadOnly = !band;
			txtCodCliente.ReadOnly = !band;
			cantnueva.Visible = !band;
			cantdespachada.ReadOnly = !band;
		}
		txtCodCliente.Enabled = band;
		label3.Visible = band;
		label17.Visible = band;
		label19.Visible = band;
		txtTipoCambio.Visible = band;
		label16.Visible = band;
		cmbFormaPago.Visible = band;
		dtpFechaPago.Visible = band;
		cmbMoneda.Visible = band;
		serielote.Visible = band;
		preciounit.Visible = band;
		importe.Visible = band;
		montodscto.Visible = band;
		valorventa.Visible = band;
		igv.Visible = band;
		precioventa.Visible = band;
		label10.Visible = band;
		txtBruto.Visible = band;
		label11.Visible = band;
		txtDscto.Visible = band;
		label12.Visible = band;
		txtValorVenta.Visible = band;
		label13.Visible = band;
		txtIGV.Visible = band;
		label14.Visible = band;
		txtPrecioVenta.Visible = band;
		btnNuevo.Visible = band;
		btnEditar.Visible = band;
		btnEliminar.Visible = !band;
		btnImprimir.Visible = band;
		btnDetalle.Visible = band;
		cantdespachada.Visible = !band;
		cantpordespachar.Visible = !band;
	}

	private void txtTransaccion_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			txtTransaccion.Focus();
		}
	}

	private void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
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

	private bool BuscaTransaccion()
	{
		tran = AdmTran.MuestraTransaccionS(txtTransaccion.Text, 1);
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

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		if (cli != null)
		{
			txtCodigoCli.Text = cli.CodCliente.ToString();
			txtCodCliente.Text = cli.CodigoPersonalizado;
			txtNombreCliente.Text = cli.RazonSocial;
			txtDireccion.Text = cli.DireccionLegal;
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
			txtDireccion.Text = cli.DireccionLegal;
			cmbFormaPago.SelectedValue = cli.FormaPago;
			txtPorcDescuento.Text = cli.Descuento.ToString();
			return true;
		}
		txtNombreCliente.Text = "";
		CodCliente = 0;
		return false;
	}

	private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
	{
		if (tran.CodTransaccion != 12)
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
				ProcessTabKey(forward: true);
			}
		}
		else if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmProveedoresLista"] != null)
			{
				Application.OpenForms["frmProveedoresLista"].Activate();
				return;
			}
			frmProveedoresLista form2 = new frmProveedoresLista();
			form2.Proceso = 3;
			form2.Procede = 8;
			form2.ShowDialog();
			CodCliente = 0;
		}
	}

	private void txtCodCliente_Leave(object sender, EventArgs e)
	{
		if (tran.CodTransaccion != 12 && CodCliente == 0)
		{
			txtCodCliente.Focus();
		}
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
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

	private void dtpFecha_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			dtpFecha.Focus();
		}
	}

	private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
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
				MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE SALIDA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaTipoDocumento()
	{
		doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			return true;
		}
		CodDocumento = 0;
		return false;
	}

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Activate();
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.Proceso = 4;
		form.Procedencia = 2;
		form.Transaccion = txtTransaccion.Text;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtDocRef.Text = doc.Sigla;
		if (CodDocumento != 0 && CodDocumento != 48)
		{
			if (CodDocumento == 47)
			{
				MostrarOcultar(proceso: true);
				cargaUsuario();
			}
			else
			{
				MostrarOcultar(proceso: false);
			}
			ProcessTabKey(forward: true);
		}
		else
		{
			MessageBox.Show("Tipo de documento no Valido para esta Operacíon.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			MostrarOcultar(proceso: false);
			txtDocRef.Text = "";
		}
	}

	private void cargaUsuario()
	{
		cmbUsuario.DataSource = AdmUsu.CargaUsuarios();
		cmbUsuario.DisplayMember = "vendedor";
		cmbUsuario.ValueMember = "codUsuario";
		cmbUsuario.SelectedIndex = 0;
	}

	public void MostrarOcultar(bool proceso)
	{
		lblmensaje.Visible = proceso;
		lblresponsable.Visible = proceso;
		lblareaencargado.Visible = proceso;
		cmbUsuario.Visible = proceso;
		cmbarea.Visible = proceso;
	}

	private void txtPedido_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void VerificarCabecera()
	{
		Validacion = true;
		if (CodTransaccion == 0 || CodDocumento == 0)
		{
			Validacion = false;
		}
		if (txtCodCliente.Visible && CodCliente == 0)
		{
			Validacion = false;
		}
		if (txtFactura.Visible && CodPedido == 0)
		{
			Validacion = false;
		}
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
	}

	private void frmNotaSalida_Load(object sender, EventArgs e)
	{
		CargaFormaPagos();
		CargaMoneda();
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		CargaTransportista();
		CargaVehiculoTrasnporte();
		if (Proceso == 2)
		{
			CargaNotaSalida();
		}
		else if (Proceso == 3)
		{
			CargaNotaSalida();
			sololectura(estado: true);
			deshabilita_botones(activo: false);
			desactiva_botones(band: false);
		}
		else if (Proceso == 4)
		{
			CargaNotaSalida();
			sololectura(estado: true);
		}
	}

	private void deshabilita_botones(bool activo)
	{
		btnGuardar.Visible = !activo;
		btnGuardar.Enabled = activo;
		label8.Visible = !activo;
		txtFactura.Visible = !activo;
		label10.Visible = activo;
		txtBruto.Visible = activo;
		label11.Visible = activo;
		txtDscto.Visible = activo;
		label12.Visible = activo;
		txtValorVenta.Visible = activo;
		label13.Visible = activo;
		txtIGV.Visible = activo;
		label14.Visible = activo;
		txtPrecioVenta.Visible = activo;
		btnNuevo.Visible = activo;
		btnEditar.Visible = activo;
		btnEliminar.Visible = activo;
		label7.Visible = !activo;
		txtNumDoc.Visible = !activo;
		txtNumDoc.Enabled = activo;
		txtNumero.Visible = activo;
		groupBox5.Enabled = !activo;
		ckbguia.Enabled = !activo;
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
	}

	private void CargaNotaSalida()
	{
		try
		{
			nota = AdmNota.CargaNotaSalida(Convert.ToInt32(CodNota));
			venta = admVenta.CargaFacturaVenta(Convert.ToInt32(nota.DocumentoReferencia));
			if (nota != null)
			{
				if (Proceso != 1)
				{
					dtpFecha.Value = nota.FechaSalida;
					txtNumDoc.Text = nota.CodNotaSalida;
				}
				CodTransaccion = nota.CodTipoTransaccion;
				if (Proceso != 1)
				{
					CargaTransaccion();
				}
				if (txtCodCliente.Enabled)
				{
					cli = AdmCli.MuestraCliente(nota.CodCliente);
					if (cli != null)
					{
						CodCliente = nota.CodCliente;
						if (nota.RUCCliente != "")
						{
							txtCodCliente.Text = nota.RUCCliente;
						}
						else
						{
							txtCodCliente.Text = nota.DNI;
						}
						txtNombreCliente.Text = nota.RazonSocialCliente;
						txtDireccion.Text = nota.Direccion;
					}
				}
				cmbFormaPago.SelectedValue = nota.FormaPago;
				dtpFechaPago.Value = nota.FechaPago;
				cmbMoneda.SelectedValue = nota.Moneda;
				txtTipoCambio.Text = nota.TipoCambio.ToString();
				if (txtAutorizacion.Enabled)
				{
				}
				if (txtDocRef.Enabled && Proceso != 1)
				{
					CodDocumento = nota.CodTipoDocumento;
					txtDocRef.Text = nota.SiglaDocumento;
					if (CodTransaccion == 7)
					{
						txtSerie.Text = nota.NumDoc;
					}
					else
					{
						txtSerie.Text = nota.Serie;
						txtNumero.Text = nota.NumDoc;
					}
					BuscaTipoDocumento();
				}
				if (txtFactura.Enabled && venta != null)
				{
					txtCodFac.Text = nota.DocumentoReferencia.ToString();
					txtFactura.Text = venta.SiglaDocumento + "-" + venta.Serie + "-" + venta.NumDoc;
				}
				txtComentario.Text = nota.Comentario;
				txtBruto.Text = $"{nota.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{nota.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{nota.Total - nota.Igv:#,##0.00}";
				txtIGV.Text = $"{nota.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{nota.Total:#,##0.00}";
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

	private void sololectura(bool estado)
	{
		txtTransaccion.ReadOnly = estado;
		dtpFecha.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		cmbMoneda.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtSerie.ReadOnly = estado;
		txtNumero.ReadOnly = estado;
		txtNumero.Visible = estado;
		txtFactura.ReadOnly = estado;
		txtComentario.ReadOnly = estado;
		txtAutorizacion.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnNuevo.Visible = !estado;
		btnEditar.Visible = !estado;
		btnEliminar.Visible = !estado;
		btnGuardar.Visible = !estado;
		btnImprimir.Visible = !estado;
		cmbFormaPago.Enabled = !estado;
		dtpFechaPago.Visible = estado;
		dtpFechaPago.Enabled = !estado;
		txtNumDoc.Visible = estado;
		txtNumDoc.ReadOnly = estado;
		label7.Visible = estado;
		ext.sololectura(groupBox1.Controls);
		txtDocRef.Enabled = !estado;
		txtNumero.Enabled = !estado;
		txtCodCliente.Enabled = !estado;
		txtTransaccion.Enabled = !estado;
		if (groupBox5.Visible)
		{
			groupBox5.Enabled = !estado;
		}
	}

	private void CargaDetalle()
	{
		if (CodTransaccion == 7)
		{
			dgvDetalle.DataSource = AdmNota.CargaDetalleNotaSalida(Convert.ToInt32(nota.CodNotaSalida), Proceso);
			RecorreDetalle();
			nota.Detalle = detalle;
		}
		else
		{
			dgvDetalle.DataSource = AdmNota.CargaDetalle(Convert.ToInt32(nota.CodNotaSalida));
			RecorreDetalle();
			nota.Detalle = detalle;
		}
		if (Proceso != 3 || CodTransaccion != 7)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToDecimal(row.Cells[cantidad.Name].Value) == Convert.ToDecimal(row.Cells[cantdespachada.Name].Value))
			{
				cont1++;
			}
		}
		if (cont1 == dgvDetalle.Rows.Count)
		{
			if (CodDocumento == 1 || CodDocumento == 2)
			{
				dgvDetalle.Columns["cantdespachada"].ReadOnly = false;
			}
			else
			{
				dgvDetalle.Columns["cantdespachada"].ReadOnly = true;
			}
		}
		else
		{
			dgvDetalle.Columns["cantdespachada"].ReadOnly = true;
		}
	}

	private void frmNotaSalida_Shown(object sender, EventArgs e)
	{
		if (Proceso == 1 && txtTipoCambio.Visible)
		{
			if (tc == null)
			{
				MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				txtTipoCambio.Text = tc.Compra.ToString();
			}
		}
	}

	private void txtDocRef_Leave(object sender, EventArgs e)
	{
		BuscaTipoDocumento();
		if (CodDocumento == 0)
		{
			txtDocRef.Focus();
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
		form.DocSeleccionado = doc.CodTipoDocumento;
		form.Sigla = doc.Sigla;
		form.Proceso = 3;
		form.ShowDialog();
		ser = form.ser;
		CodSerie = ser.CodSerie;
		txtSerie.Text = ser.Serie;
		if (CodSerie != 0)
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (BuscaSerie())
		{
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumero.Text = "";
				txtNumero.Visible = true;
				txtNumero.Focus();
				Manual = 2;
			}
			else if (CodTransaccion == 7)
			{
				txtNumero.Visible = false;
				txtNumero.Text = Convert.ToString(ser.Numeracion + 1);
				Manual = 1;
			}
			ProcessTabKey(forward: true);
		}
		else
		{
			MessageBox.Show("Serie no existe, Presione F1 para consultar la tabla de ayuda", "Facturación Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private bool BuscaSerie()
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

	private void txtSerie_Leave(object sender, EventArgs e)
	{
		if (CodDocumento == 0)
		{
			txtDocRef.Focus();
		}
		if (BuscaSerie())
		{
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumero.Text = "";
				txtNumero.Visible = true;
				txtNumero.Focus();
				Manual = 2;
			}
			else if (CodTransaccion == 7)
			{
				txtNumero.Visible = false;
				txtNumero.Text = Convert.ToString(ser.Numeracion + 1);
				Manual = 1;
			}
		}
	}

	private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtNumero_Leave(object sender, EventArgs e)
	{
		if (txtNumero.Visible && txtNumero.Text == "")
		{
			txtNumero.Focus();
			return;
		}
		txtFactura.Focus();
		VerificarCabecera();
	}

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		try
		{
			if (tran.CodTransaccion != 12)
			{
				if (Application.OpenForms["frmDetalleSalida"] != null)
				{
					Application.OpenForms["frmDetalleSalida"].Activate();
					return;
				}
				frmDetalleSalida form = new frmDetalleSalida();
				form.Procede = 1;
				form.Proceso = 1;
				form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				form.tc = Convert.ToDouble(txtTipoCambio.Text);
				form.ShowDialog();
				dgvDetalle.Columns[cantidad.Name].ReadOnly = true;
				return;
			}
			if (Application.OpenForms["frmMuestraNotasIngresoProveedor"] != null)
			{
				Application.OpenForms["frmMuestraNotasIngresoProveedor"].Activate();
				return;
			}
			if (CodProveedor != 0)
			{
				frmMuestraNotasIngresoProveedor form2 = new frmMuestraNotasIngresoProveedor();
				form2.CodProveedor = CodProveedor;
				form2.ShowDialog();
				dgvDetalle.Columns[cantidad.Name].ReadOnly = false;
				cantpr = new List<int>();
				{
					foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
					{
						cantpr.Add(Convert.ToInt32(row.Cells[cantidad.Name].Value));
					}
					return;
				}
			}
			txtCodCliente.Focus();
		}
		catch (Exception)
		{
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso != 1 || CodTransaccion == 7)
		{
			return;
		}
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal igvt = default(decimal);
		decimal preciot = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
			preciot += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{igvt:#,##0.0000}";
		txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
	{
		if (txtPrecioVenta.Text != "")
		{
			btnGuardar.Enabled = true;
		}
	}

	private int verificarCamposVacios()
	{
		int valor = 0;
		if (CodTransaccion == 7)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				string canti = "";
				canti = Convert.ToString(Convert.ToDecimal(row.Cells[cantnueva.Name].Value));
				if (canti == "" || canti == "0.00" || canti == "0")
				{
					valor = 1;
				}
			}
		}
		return valor;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (CodDocumento == 47 && string.IsNullOrEmpty(txtComentario.Text))
			{
				MessageBox.Show("Ingresar un Comentario.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			bool rpta = false;
			if (Proceso == 1 && tran.CodTransaccion == 7)
			{
				datosAlmacena = (DataTable)dgvDetalle.DataSource;
			}
			if (Proceso == 0)
			{
				return;
			}
			if (verificarCamposVacios() == 1)
			{
				MessageBox.Show("Debe completar Detalle de Nota, Datos Vacios");
				return;
			}
			nota.CodSucursal = frmLogin.iCodSucursal;
			nota.CodAlmacen = frmLogin.iCodAlmacen;
			nota.CodTipoTransaccion = tran.CodTransaccion;
			if (Proceso == 3 && nota.CodTipoTransaccion == 7)
			{
				Proceso = 2;
			}
			if (nota.CodTipoTransaccion == 12)
			{
				nota.CodTipoDocumento = 10;
			}
			else
			{
				nota.CodTipoDocumento = doc.CodTipoDocumento;
				nota.CodSerie = CodSerie;
			}
			nota.CodCliente = cli.CodCliente;
			if (nota.CodTipoTransaccion == 7 && Proceso == 2)
			{
				nota.NumDoc = txtSerie.Text;
			}
			else if (nota.CodTipoTransaccion == 7)
			{
				nota.NumDoc = txtSerie.Text + "-" + txtNumero.Text;
			}
			else
			{
				nota.Serie = txtSerie.Text;
				nota.NumDoc = txtNumero.Text;
			}
			if (txtFactura.Visible)
			{
				nota.DocumentoReferencia = Convert.ToInt32(txtCodFac.Text);
			}
			nota.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			if (txtTipoCambio.Visible)
			{
				nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
			}
			nota.FechaSalida = dtpFecha.Value.Date;
			nota.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
			nota.FechaPago = dtpFechaPago.Value.Date;
			nota.Comentario = txtComentario.Text;
			nota.MontoBruto = Convert.ToDouble(txtBruto.Text);
			nota.MontoDscto = Convert.ToDouble(txtDscto.Text);
			nota.Igv = Convert.ToDouble(txtIGV.Text);
			nota.Total = Convert.ToDouble(txtPrecioVenta.Text);
			nota.CodUser = frmLogin.iCodUser;
			nota.Estado = 1;
			nota.codVehiculoTransporte = 0;
			nota.codConductor = 0;
			nota.codalmacenreceptor = 0;
			nota.Codtransferencia = 0;
			nota.responsable = Convert.ToInt32(cmbUsuario.SelectedValue);
			nota.area = cmbarea.Text;
			if (CodProveedor != 0)
			{
				nota.CodProveedor = CodProveedor;
			}
			if (Proceso == 1)
			{
				using (TransactionScope scope = new TransactionScope())
				{
					rpta = AdmNota.insert(nota);
					if (rpta)
					{
						RecorreDetalle();
						if (detalle.Count > 0)
						{
							foreach (clsDetalleNotaSalida det in detalle)
							{
								rpta = AdmNota.insertdetalle(det);
								if (!rpta)
								{
									break;
								}
							}
						}
						if (rpta)
						{
							CodNota = nota.CodNotaSalida;
							txtNumDoc.Text = nota.CodNotaSalida.PadLeft(11, '0');
							CargaNotaSalida();
							if (Proceso == 1 && tran.CodTransaccion == 7)
							{
								dgvDetalle.DataSource = datosAlmacena;
							}
							sololectura(estado: true);
							MessageBox.Show("Los datos se guardaron correctamente", "Nota de Salida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							scope.Complete();
						}
						else
						{
							MessageBox.Show("Ocurrió un error al registrar la operación", "Nota de Salida", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Transaction.Current.Rollback();
						}
					}
				}
				if (!ckbguia.Checked || txtcodserie.Text.Equals(""))
				{
					return;
				}
				guia.CodAlmacen = frmLogin.iCodAlmacen;
				guia.CodTipoDocumento = 11;
				guia.CodSerie = Convert.ToInt32(txtcodserie.Text);
				guia.CodMotivo = 4;
				guia.FechaEmision = dtpFecha.Value;
				guia.FechaTraslado = nota.FechaSalida;
				guia.CodProveedor = Convert.ToInt32(CodProveedor);
				if (cmbVehiculo.SelectedValue == null)
				{
					guia.CodVehiculoTransporte = 0;
				}
				else
				{
					guia.CodVehiculoTransporte = Convert.ToInt32(cmbVehiculo.SelectedValue);
				}
				if (cmbTransportista.SelectedValue == null)
				{
					guia.CodConductor = 0;
				}
				else
				{
					guia.CodConductor = Convert.ToInt32(cmbTransportista.SelectedValue.ToString());
				}
				guia.CodEmpresaTransporte = CodEmpresaTransporte;
				guia.Facturado = 0;
				if (CodNotaI > 0)
				{
					guia.CodFactura = CodNotaI;
				}
				else
				{
					guia.CodFactura = Convert.ToInt32(nota.CodNotaSalida);
				}
				guia.Comentario = txtComentario.Text;
				guia.CodUser = frmLogin.iCodUser;
				guia.Estado = 1;
				if (!AdmGuia.insert(guia))
				{
					return;
				}
				RecorreDetalleGuia();
				if (detalleg.Count <= 0)
				{
					return;
				}
				foreach (clsDetalleGuiaRemision detg in detalleg)
				{
					AdmGuia.insertdetalle(detg);
					AdmGuia.insertrelacionguia(Convert.ToInt32(guia.CodGuiaRemision), guia.CodFactura, frmLogin.iCodAlmacen, frmLogin.iCodUser, 0);
				}
				MessageBox.Show("Se ha generado la guia de remision correspondiente!", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				if (Proceso != 2 || !AdmNota.update(nota))
				{
					return;
				}
				RecorreDetalle();
				foreach (clsDetalleNotaSalida det2 in nota.Detalle)
				{
					foreach (clsDetalleNotaSalida det3 in detalle)
					{
						if (det2.Equals(det3))
						{
							AdmNota.updatedetalle(det3);
						}
					}
					AdmNota.deletedetalle(det2.CodDetalleSalida);
				}
				foreach (clsDetalleNotaSalida deta in detalle)
				{
					if (deta.CodDetalleSalida == 0)
					{
						AdmNota.insertdetalle(deta);
					}
				}
				MessageBox.Show("Los datos se actualizaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrio un error: " + ex.Message, "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
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
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		if (Proceso == 2)
		{
			deta.CodDetalleSalida = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		}
		if (Proceso == 2 && CodTransaccion == 7)
		{
			deta.Cantidad = Convert.ToDouble(fila.Cells[cantdespachada.Name].Value);
			deta.CantidadPendiente = Convert.ToDouble(fila.Cells[cantpordespachar.Name].Value);
		}
		else if (Proceso == 1 && CodTransaccion == 7)
		{
			deta.Cantidad = Convert.ToDouble(fila.Cells[cantnueva.Name].Value);
			deta.CantidadPendiente = Convert.ToDouble(fila.Cells[cantpordespachar.Name].Value);
		}
		else
		{
			deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		}
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodNotaSalida = Convert.ToInt32(nota.CodNotaSalida);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		detalle.Add(deta);
	}

	public void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		pag = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		dtpFechaPago.Value = dtpFecha.Value.AddDays(pag.Dias);
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (tran.CodTransaccion != 12 && ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0)))
		{
			DataGridViewRow row = dgvDetalle.SelectedRows[0];
			if (Application.OpenForms["frmDetalleSalida"] != null)
			{
				Application.OpenForms["frmDetalleSalida"].Activate();
				return;
			}
			frmDetalleSalida form = new frmDetalleSalida();
			form.Proceso = 2;
			form.Procede = 1;
			form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			form.tc = Convert.ToDouble(txtTipoCambio.Text);
			form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
			form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
			form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
			form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
			form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
			form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
			form.txtUltimoPrecioCompra.Text = row.Cells[maxpdscto.Name].Value.ToString();
			form.ShowDialog();
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (Proceso == 1)
		{
			if (dgvDetalle.CurrentRow.Index == -1)
			{
				return;
			}
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
			if (tran.CodTransaccion != 12)
			{
				return;
			}
			cantpr = new List<int>();
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					cantpr.Add(Convert.ToInt32(row.Cells[cantidad.Name].Value));
				}
				return;
			}
		}
		if (Proceso == 2 && dgvDetalle.CurrentRow.Index != -1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Detalle Nota Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmNota.deletedetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[coddetalle.Name].Value)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Detalle Nota Salida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaDetalle();
			}
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			RecorreDetalle();
			if (Application.OpenForms["frmDetalleSalida"] != null)
			{
				Application.OpenForms["frmDetalleSalida"].Activate();
				return;
			}
			frmDetalleSalida form = new frmDetalleSalida();
			form.Procede = 1;
			form.Proceso = 1;
			if (tran.CodTransaccion == 12)
			{
				form.Tipo = 3;
				form.codTran = tran.CodTransaccion;
			}
			form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			form.tc = Convert.ToDouble(txtTipoCambio.Text);
			form.productosNotaSalida = detalle;
			form.codProveedor = CodProveedor;
			form.codTipodoc = doc.CodTipoDocumento;
			form.ShowDialog();
		}
		catch (Exception)
		{
		}
	}

	private void txtComentario_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		if (tran.CodTransaccion == 12 && Convert.ToInt32(guia.CodGuiaRemision) != 0)
		{
			ser = admSerie.MuestraSerie(Convert.ToInt32(txtcodserie.Text), frmLogin.iCodAlmacen);
			ReportDocument rd = new ReportDocument();
			rd.Load("CRGuiaRemisionSD.rpt");
			CRGuiaRemisionSD rpt = new CRGuiaRemisionSD();
			rd.SetDataSource(ds.GuiaRemisionSD(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen));
			PrintOptions rptoption = rd.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(512, 850, 30, 500));
			rd.PrintToPrinter(1, collated: false, 1, 1);
			rd.Close();
			rd.Dispose();
		}
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso != 1)
		{
			return;
		}
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal igvt = default(decimal);
		decimal preciot = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
			preciot += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{igvt:#,##0.0000}";
		txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
	}

	private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (!(dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa") || Proceso != 1 || CodTransaccion == 7)
		{
			return;
		}
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal igvt = default(decimal);
		decimal preciot = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
			preciot += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{igvt:#,##0.0000}";
		txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
	}

	private void txtFactura_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmMercaderiaEntregar"] != null)
			{
				Application.OpenForms["frmMercaderiaEntregar"].Activate();
				return;
			}
			frmMercaderiaEntregar form = new frmMercaderiaEntregar();
			form.proceso = 11;
			form.tipo = Manual;
			form.ShowDialog();
			if (form.salida != null && form.salida.CodNotaSalida != "")
			{
				nota = form.salida;
				CodNota = nota.CodNotaSalida;
			}
			if (Convert.ToInt32(CodNota) != 0)
			{
				CargaNotaSalida();
				ProcessTabKey(forward: true);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex.Message);
		}
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
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

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
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

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
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

	private void recalculadetalle()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (Proceso == 3)
			{
				row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[cantdespachada.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
				row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[cantdespachada.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
			}
			else if (Proceso == 1)
			{
				if (tran.CodTransaccion != 12)
				{
					row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[cantnueva.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
					row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[cantnueva.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
				}
				else
				{
					row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[cantidad.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
					row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[cantidad.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
				}
			}
			row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) / Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) - Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			row.Cells[precioreal.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			row.Cells[valoreal.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
		}
	}

	private void calculatotales()
	{
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal igvt = default(decimal);
		decimal preciot = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
			preciot += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{igvt:#,##0.0000}";
		txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvDetalle.Rows.Count <= 0 || !dgvDetalle.CurrentRow.Selected)
			{
				return;
			}
			QPorDespachar = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantpordespachar2.Name].Value);
			QOriginal = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
			QDespachada = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantdespachada2.Name].Value);
			if (Proceso == 3)
			{
				if (dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "cantdespachada" && txtedit.Text != "")
				{
					Qnueva = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantdespachada.Name].Value);
					if (Qnueva > QOriginal || Qnueva == 0m)
					{
						MessageBox.Show("Cantidad Debe Ser Menor o Igual que: " + QOriginal);
						dgvDetalle.CurrentRow.Cells[cantdespachada.Name].Value = QOriginal;
					}
					else
					{
						dgvDetalle.CurrentRow.Cells[cantpordespachar.Name].Value = QOriginal - Qnueva;
					}
				}
				btnGuardar.Enabled = true;
			}
			else if (Proceso == 1)
			{
				if (dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "cantnueva" && txtedit.Text != "")
				{
					Qnueva = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantnueva.Name].Value);
					if ((Qnueva > QOriginal && Qnueva > QPorDespachar) || Qnueva > QPorDespachar || Qnueva == 0m)
					{
						MessageBox.Show("Cantidad Debe Ser Menor o Igual que: " + QPorDespachar);
						dgvDetalle.CurrentRow.Cells[cantnueva.Name].Value = 0;
					}
					else
					{
						dgvDetalle.CurrentRow.Cells[cantpordespachar.Name].Value = QOriginal - Qnueva - QDespachada;
						dgvDetalle.CurrentRow.Cells[cantdespachada.Name].Value = QDespachada + Qnueva;
					}
				}
				if (tran.CodTransaccion == 12 && cantprod < Convert.ToInt32(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value))
				{
					MessageBox.Show("La cantidad del producto debe ser menor", "Nota de Salida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = Convert.ToString(cantprod);
				}
			}
			recalculadetalle();
			calculatotales();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message);
		}
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == cantdespachada.Index)
		{
			ok.SOLONumeros(sender, e);
		}
		else if (dgvDetalle.CurrentCell.ColumnIndex == cantnueva.Index)
		{
			ok.SOLONumeros(sender, e);
		}
		else if (dgvDetalle.CurrentCell.ColumnIndex == cantidad.Index)
		{
			ok.SOLONumeros(sender, e);
		}
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

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dgvDetalle_Leave(object sender, EventArgs e)
	{
	}

	private void txtSerie_TextChanged(object sender, EventArgs e)
	{
		if (CodTransaccion == 7)
		{
			txtNumero.Text = "";
			txtNumero.Visible = false;
		}
	}

	private void txtRazonSocialTransporte_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmEmpresaTransporte"] != null)
		{
			Application.OpenForms["frmEmpresaTransporte"].Activate();
			return;
		}
		frmEmpresaTransporte form = new frmEmpresaTransporte();
		form.Proceso = 3;
		form.ShowDialog();
		empT = form.emp;
		CodEmpresaTransporte = empT.CodEmpresaTranporte;
		if (CodEmpresaTransporte != 0)
		{
			CargaEmpresaTransporte();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaEmpresaTransporte()
	{
		empT = AdmET.MuestraEmpresaTranporte(empT.CodEmpresaTranporte);
		if (empT != null)
		{
			txtRazonSocialTransporte.Text = empT.RazonSocial;
		}
		else
		{
			txtRazonSocialTransporte.Text = "";
		}
	}

	private void txtSerieG_KeyDown(object sender, KeyEventArgs e)
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
		form.DocSeleccionado = 11;
		form.Proceso = 3;
		form.ShowDialog();
		ser = form.ser;
		CodSerieG = ser.CodSerie;
		numG = ser.Numeracion;
		if (CodSerieG != 0)
		{
			if (ser.PreImpreso)
			{
				CodSerieG = ser.CodSerie;
				txtSerieG.Text = ser.Serie;
				txtcodserie.Text = ser.CodSerie.ToString();
				txtNumeroG.Visible = true;
				txtNumeroG.Enabled = true;
				txtNumeroG.Text = "";
			}
			else
			{
				CodSerieG = ser.CodSerie;
				txtSerieG.Text = ser.Serie;
				txtcodserie.Text = ser.CodSerie.ToString();
				txtNumeroG.Visible = false;
			}
		}
		if (CodSerieG != 0)
		{
			ProcessTabKey(forward: true);
		}
	}

	private void ckbguia_CheckedChanged(object sender, EventArgs e)
	{
		if (ckbguia.Checked)
		{
			groupBox5.Visible = true;
		}
		else
		{
			groupBox5.Visible = false;
		}
	}

	private void CargaTransportista()
	{
		cmbTransportista.DataSource = admConductor.CargaConductores();
		cmbTransportista.DisplayMember = "nombre";
		cmbTransportista.ValueMember = "codConductor";
		cmbTransportista.SelectedIndex = -1;
	}

	private void CargaVehiculoTrasnporte()
	{
		cmbVehiculo.DataSource = admVehiculoTransporte.CargaVehiculoTransportes();
		cmbVehiculo.DisplayMember = "placa";
		cmbVehiculo.ValueMember = "codVehiculoTransporte";
		cmbVehiculo.SelectedIndex = -1;
	}

	private void RecorreDetalleGuia()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleguia(row);
		}
	}

	private void añadedetalleguia(DataGridViewRow fila)
	{
		clsDetalleGuiaRemision deta = new clsDetalleGuiaRemision();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodGuiaRemision = Convert.ToInt32(guia.CodGuiaRemision);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		if (Convert.ToBoolean(guia.Facturado))
		{
			deta.CantidadPendiente = 0.0;
			deta.Pendiente = false;
		}
		else
		{
			deta.CantidadPendiente = deta.Cantidad;
			deta.Pendiente = true;
		}
		deta.CodUser = frmLogin.iCodUser;
		detalleg.Add(deta);
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void txtTipoCambio_TextChanged(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotaSalida));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblmensaje = new System.Windows.Forms.Label();
		this.ckbguia = new System.Windows.Forms.CheckBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtSerieG = new System.Windows.Forms.TextBox();
		this.txtcodserie = new System.Windows.Forms.TextBox();
		this.txtNumeroG = new System.Windows.Forms.TextBox();
		this.label35 = new System.Windows.Forms.Label();
		this.txtRazonSocialTransporte = new System.Windows.Forms.TextBox();
		this.label34 = new System.Windows.Forms.Label();
		this.cmbTransportista = new System.Windows.Forms.ComboBox();
		this.cmbVehiculo = new System.Windows.Forms.ComboBox();
		this.label32 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtCodigoCli = new System.Windows.Forms.TextBox();
		this.txtFactura = new System.Windows.Forms.TextBox();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.label19 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.txtAutorizacion = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.codVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.maxpdscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantnueva = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantdespachada = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantpordespachar = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantdespachada2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantpordespachar2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.cmbUsuario = new System.Windows.Forms.ComboBox();
		this.lblresponsable = new System.Windows.Forms.Label();
		this.lblareaencargado = new System.Windows.Forms.Label();
		this.cmbarea = new System.Windows.Forms.ComboBox();
		this.txtCodFac = new System.Windows.Forms.TextBox();
		this.txtCodDoc = new System.Windows.Forms.TextBox();
		this.txtPorcDescuento = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtCodFac);
		this.groupBox1.Controls.Add(this.txtCodDoc);
		this.groupBox1.Controls.Add(this.txtPorcDescuento);
		this.groupBox1.Controls.Add(this.cmbUsuario);
		this.groupBox1.Controls.Add(this.lblresponsable);
		this.groupBox1.Controls.Add(this.lblareaencargado);
		this.groupBox1.Controls.Add(this.cmbarea);
		this.groupBox1.Controls.Add(this.lblmensaje);
		this.groupBox1.Controls.Add(this.ckbguia);
		this.groupBox1.Controls.Add(this.groupBox5);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtCodigoCli);
		this.groupBox1.Controls.Add(this.txtFactura);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.lbNombreTransaccion);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtAutorizacion);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtNumDoc);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtTransaccion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(943, 240);
		this.groupBox1.TabIndex = 21;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.lblmensaje.AutoSize = true;
		this.lblmensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblmensaje.ForeColor = System.Drawing.Color.Red;
		this.lblmensaje.Location = new System.Drawing.Point(100, 115);
		this.lblmensaje.Name = "lblmensaje";
		this.lblmensaje.Size = new System.Drawing.Size(157, 13);
		this.lblmensaje.TabIndex = 105;
		this.lblmensaje.Text = "Indicar Motivo del Ajuste *";
		this.lblmensaje.Visible = false;
		this.ckbguia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ckbguia.AutoSize = true;
		this.ckbguia.Checked = true;
		this.ckbguia.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ckbguia.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ckbguia.Location = new System.Drawing.Point(674, 68);
		this.ckbguia.Name = "ckbguia";
		this.ckbguia.Size = new System.Drawing.Size(78, 16);
		this.ckbguia.TabIndex = 103;
		this.ckbguia.Text = "Generar Guia";
		this.ckbguia.UseVisualStyleBackColor = true;
		this.ckbguia.Visible = false;
		this.ckbguia.CheckedChanged += new System.EventHandler(ckbguia_CheckedChanged);
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox5.Controls.Add(this.txtSerieG);
		this.groupBox5.Controls.Add(this.txtcodserie);
		this.groupBox5.Controls.Add(this.txtNumeroG);
		this.groupBox5.Controls.Add(this.label35);
		this.groupBox5.Controls.Add(this.txtRazonSocialTransporte);
		this.groupBox5.Controls.Add(this.label34);
		this.groupBox5.Controls.Add(this.cmbTransportista);
		this.groupBox5.Controls.Add(this.cmbVehiculo);
		this.groupBox5.Controls.Add(this.label32);
		this.groupBox5.Controls.Add(this.label33);
		this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.groupBox5.Location = new System.Drawing.Point(18, 159);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(651, 75);
		this.groupBox5.TabIndex = 104;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Datos de Guia";
		this.groupBox5.Visible = false;
		this.txtSerieG.Location = new System.Drawing.Point(85, 15);
		this.txtSerieG.Name = "txtSerieG";
		this.txtSerieG.ReadOnly = true;
		this.txtSerieG.Size = new System.Drawing.Size(61, 20);
		this.txtSerieG.TabIndex = 81;
		this.txtSerieG.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerieG_KeyDown);
		this.txtcodserie.Location = new System.Drawing.Point(226, 15);
		this.txtcodserie.Name = "txtcodserie";
		this.txtcodserie.Size = new System.Drawing.Size(16, 20);
		this.txtcodserie.TabIndex = 84;
		this.txtcodserie.Visible = false;
		this.txtNumeroG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumeroG.Enabled = false;
		this.txtNumeroG.Location = new System.Drawing.Point(154, 15);
		this.txtNumeroG.Name = "txtNumeroG";
		this.txtNumeroG.Size = new System.Drawing.Size(65, 20);
		this.txtNumeroG.TabIndex = 82;
		this.txtNumeroG.Tag = "";
		this.label35.AutoSize = true;
		this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.label35.Location = new System.Drawing.Point(8, 17);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(34, 13);
		this.label35.TabIndex = 83;
		this.label35.Text = "Serie:";
		this.txtRazonSocialTransporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtRazonSocialTransporte.Location = new System.Drawing.Point(396, 44);
		this.txtRazonSocialTransporte.Name = "txtRazonSocialTransporte";
		this.txtRazonSocialTransporte.ReadOnly = true;
		this.txtRazonSocialTransporte.Size = new System.Drawing.Size(244, 18);
		this.txtRazonSocialTransporte.TabIndex = 80;
		this.txtRazonSocialTransporte.Tag = "5";
		this.txtRazonSocialTransporte.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRazonSocialTransporte_KeyDown);
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.label34.Location = new System.Drawing.Point(329, 48);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(61, 13);
		this.label34.TabIndex = 79;
		this.label34.Text = "Raz. Social";
		this.cmbTransportista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTransportista.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbTransportista.FormattingEnabled = true;
		this.cmbTransportista.Location = new System.Drawing.Point(84, 43);
		this.cmbTransportista.Name = "cmbTransportista";
		this.cmbTransportista.Size = new System.Drawing.Size(241, 20);
		this.cmbTransportista.TabIndex = 75;
		this.cmbVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbVehiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbVehiculo.FormattingEnabled = true;
		this.cmbVehiculo.Location = new System.Drawing.Point(396, 13);
		this.cmbVehiculo.Name = "cmbVehiculo";
		this.cmbVehiculo.Size = new System.Drawing.Size(244, 20);
		this.cmbVehiculo.TabIndex = 76;
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.label32.Location = new System.Drawing.Point(337, 18);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(51, 13);
		this.label32.TabIndex = 78;
		this.label32.Text = "Vehiculo:";
		this.label33.AutoSize = true;
		this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.label33.Location = new System.Drawing.Point(8, 44);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(71, 13);
		this.label33.TabIndex = 77;
		this.label33.Text = "Transportista:";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(670, 211);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 46;
		this.label8.Tag = "20";
		this.label8.Text = "Cotiz./Fact.";
		this.label8.Visible = false;
		this.txtCodigoCli.Enabled = false;
		this.txtCodigoCli.Location = new System.Drawing.Point(674, 63);
		this.txtCodigoCli.Name = "txtCodigoCli";
		this.txtCodigoCli.Size = new System.Drawing.Size(22, 20);
		this.txtCodigoCli.TabIndex = 41;
		this.txtCodigoCli.Visible = false;
		this.txtFactura.Location = new System.Drawing.Point(738, 207);
		this.txtFactura.Name = "txtFactura";
		this.txtFactura.ReadOnly = true;
		this.txtFactura.Size = new System.Drawing.Size(199, 20);
		this.txtFactura.TabIndex = 3;
		this.txtFactura.Tag = "20";
		this.superValidator1.SetValidator1(this.txtFactura, this.customValidator4);
		this.txtFactura.Visible = false;
		this.txtFactura.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFactura_KeyDown);
		this.dtpFechaPago.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(850, 111);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 7;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(773, 114);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(71, 13);
		this.label19.TabIndex = 38;
		this.label19.Tag = "16";
		this.label19.Text = "Fecha Pago :";
		this.label19.Visible = false;
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(102, 87);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(567, 20);
		this.txtDireccion.TabIndex = 12;
		this.txtDireccion.Tag = "6";
		this.txtDireccion.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 90);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(52, 13);
		this.label4.TabIndex = 35;
		this.label4.Tag = "6";
		this.label4.Text = "Direccion";
		this.label4.Visible = false;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(647, 142);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(70, 13);
		this.label6.TabIndex = 37;
		this.label6.Tag = "7";
		this.label6.Text = "% Descuento";
		this.label6.Visible = false;
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(797, 169);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(140, 21);
		this.cmbFormaPago.TabIndex = 16;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.Visible = false;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(712, 172);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(79, 13);
		this.label3.TabIndex = 33;
		this.label3.Tag = "16";
		this.label3.Text = "Forma de Pago";
		this.label3.Visible = false;
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(136, 17);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(224, 18);
		this.lbNombreTransaccion.TabIndex = 29;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.lbNombreTransaccion.Visible = false;
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Location = new System.Drawing.Point(139, 40);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.Size = new System.Drawing.Size(52, 20);
		this.txtSerie.TabIndex = 2;
		this.txtSerie.Tag = "13";
		this.txtSerie.Visible = false;
		this.txtSerie.TextChanged += new System.EventHandler(txtSerie_TextChanged);
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(850, 63);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 13;
		this.txtTipoCambio.Tag = "15";
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtTipoCambio.TextChanged += new System.EventHandler(txtTipoCambio_TextChanged);
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(850, 37);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 21);
		this.cmbMoneda.TabIndex = 6;
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(770, 66);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 28;
		this.label16.Tag = "15";
		this.label16.Text = "Tipo/Cambio :";
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(792, 40);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(52, 13);
		this.label17.TabIndex = 27;
		this.label17.Text = "Moneda :";
		this.txtAutorizacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtAutorizacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtAutorizacion.Location = new System.Drawing.Point(850, 87);
		this.txtAutorizacion.Name = "txtAutorizacion";
		this.txtAutorizacion.Size = new System.Drawing.Size(81, 20);
		this.txtAutorizacion.TabIndex = 13;
		this.txtAutorizacion.Tag = "22";
		this.txtAutorizacion.Visible = false;
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(773, 90);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(71, 13);
		this.label18.TabIndex = 26;
		this.label18.Tag = "22";
		this.label18.Text = "Autorizacion :";
		this.label18.Visible = false;
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(197, 64);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(472, 20);
		this.txtNombreCliente.TabIndex = 12;
		this.txtNombreCliente.Tag = "3";
		this.txtNombreCliente.Visible = false;
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Location = new System.Drawing.Point(102, 64);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.ReadOnly = true;
		this.txtCodCliente.Size = new System.Drawing.Size(89, 20);
		this.txtCodCliente.TabIndex = 4;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.customValidator3);
		this.txtCodCliente.Visible = false;
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(17, 66);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(39, 13);
		this.label15.TabIndex = 20;
		this.label15.Tag = "5";
		this.label15.Text = "Cliente";
		this.label15.Visible = false;
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Location = new System.Drawing.Point(856, 134);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(75, 23);
		this.btnDetalle.TabIndex = 18;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(102, 137);
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(258, 20);
		this.txtComentario.TabIndex = 17;
		this.txtComentario.Tag = "21";
		this.txtComentario.Visible = false;
		this.txtComentario.Leave += new System.EventHandler(txtComentario_Leave);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(17, 138);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(60, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Comentario";
		this.label9.Visible = false;
		this.txtNumDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Location = new System.Drawing.Point(496, 37);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(173, 20);
		this.txtNumDoc.TabIndex = 2;
		this.txtNumDoc.Visible = false;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(432, 40);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.label7.Visible = false;
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.Location = new System.Drawing.Point(197, 41);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(60, 20);
		this.txtNumero.TabIndex = 7;
		this.txtNumero.Tag = "11";
		this.txtNumero.Visible = false;
		this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumero_KeyPress);
		this.txtNumero.Leave += new System.EventHandler(txtNumero_Leave);
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(102, 40);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 1;
		this.txtDocRef.Tag = "10";
		this.superValidator1.SetValidator1(this.txtDocRef, this.customValidator1);
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.txtDocRef.Leave += new System.EventHandler(txtDocRef_Leave);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 41);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.label5.Visible = false;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Location = new System.Drawing.Point(102, 16);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.Size = new System.Drawing.Size(28, 20);
		this.txtTransaccion.TabIndex = 0;
		this.superValidator1.SetValidator1(this.txtTransaccion, this.customValidator2);
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 17);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(850, 14);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 5;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(801, 18);
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
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 483);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(943, 50);
		this.groupBox3.TabIndex = 24;
		this.groupBox3.TabStop = false;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(534, 12);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 32);
		this.btnImprimir.TabIndex = 22;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(869, 12);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 11;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 12);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Visible = false;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(786, 12);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 0;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 12);
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
		this.btnEliminar.Location = new System.Drawing.Point(155, 12);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.dgvDetalle);
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
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 240);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(943, 243);
		this.groupBox2.TabIndex = 26;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.codVenta, this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.moneda, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.maxpdscto, this.coduser, this.fecharegistro, this.cantnueva, this.cantdespachada, this.cantpordespachar, this.cantdespachada2, this.cantpordespachar2);
		this.dgvDetalle.Location = new System.Drawing.Point(3, 19);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(937, 162);
		this.dgvDetalle.TabIndex = 24;
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvDetalle_KeyPress);
		this.dgvDetalle.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvDetalle_KeyUp);
		this.dgvDetalle.Leave += new System.EventHandler(dgvDetalle_Leave);
		this.codVenta.DataPropertyName = "codVenta";
		this.codVenta.HeaderText = "codVenta";
		this.codVenta.Name = "codVenta";
		this.codVenta.ReadOnly = true;
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
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle16;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Visible = false;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle17;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		dataGridViewCellStyle18.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle18;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle19.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle19;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle20.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle20;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle21.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle21;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle22.Format = "N2";
		dataGridViewCellStyle22.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle22;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle23.Format = "N2";
		dataGridViewCellStyle23.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle23;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle24.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle24;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle25.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle25;
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
		this.maxpdscto.DataPropertyName = "maxPorcDescto";
		this.maxpdscto.HeaderText = "MaximoDscto";
		this.maxpdscto.Name = "maxpdscto";
		this.maxpdscto.ReadOnly = true;
		this.maxpdscto.Visible = false;
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
		this.cantnueva.DataPropertyName = "cantingreso";
		dataGridViewCellStyle26.Format = "N2";
		dataGridViewCellStyle26.NullValue = null;
		this.cantnueva.DefaultCellStyle = dataGridViewCellStyle26;
		this.cantnueva.HeaderText = "Cant.Ingreso";
		this.cantnueva.Name = "cantnueva";
		this.cantnueva.Visible = false;
		this.cantdespachada.DataPropertyName = "cantdespachada";
		dataGridViewCellStyle27.Format = "N2";
		dataGridViewCellStyle27.NullValue = null;
		this.cantdespachada.DefaultCellStyle = dataGridViewCellStyle27;
		this.cantdespachada.HeaderText = "Cant.Despachada";
		this.cantdespachada.Name = "cantdespachada";
		this.cantdespachada.Visible = false;
		this.cantpordespachar.DataPropertyName = "cantpordespachar";
		dataGridViewCellStyle28.Format = "N2";
		dataGridViewCellStyle28.NullValue = null;
		this.cantpordespachar.DefaultCellStyle = dataGridViewCellStyle28;
		this.cantpordespachar.HeaderText = "Cant.PorDespachar";
		this.cantpordespachar.Name = "cantpordespachar";
		this.cantpordespachar.ReadOnly = true;
		this.cantpordespachar.Visible = false;
		this.cantpordespachar.Width = 110;
		this.cantdespachada2.DataPropertyName = "cantdespachada2";
		dataGridViewCellStyle29.Format = "N2";
		dataGridViewCellStyle29.NullValue = null;
		this.cantdespachada2.DefaultCellStyle = dataGridViewCellStyle29;
		this.cantdespachada2.HeaderText = "Cant.Despachada2";
		this.cantdespachada2.Name = "cantdespachada2";
		this.cantdespachada2.ReadOnly = true;
		this.cantdespachada2.Visible = false;
		this.cantpordespachar2.DataPropertyName = "cantpordespachar2";
		dataGridViewCellStyle30.Format = "N2";
		dataGridViewCellStyle30.NullValue = null;
		this.cantpordespachar2.DefaultCellStyle = dataGridViewCellStyle30;
		this.cantpordespachar2.HeaderText = "Cant.PorDespachar2";
		this.cantpordespachar2.Name = "cantpordespachar2";
		this.cantpordespachar2.ReadOnly = true;
		this.cantpordespachar2.Visible = false;
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(125, 187);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(826, 217);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(756, 224);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(826, 191);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(770, 194);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(297, 189);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(598, 191);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(531, 194);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(226, 192);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(77, 190);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator4.ErrorMessage = "Ingrese Documento";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese Cliente";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese Doc. Referencia";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese Transaccion";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbUsuario.FormattingEnabled = true;
		this.cmbUsuario.Location = new System.Drawing.Point(522, 131);
		this.cmbUsuario.Margin = new System.Windows.Forms.Padding(4);
		this.cmbUsuario.Name = "cmbUsuario";
		this.cmbUsuario.Size = new System.Drawing.Size(250, 24);
		this.cmbUsuario.TabIndex = 122;
		this.cmbUsuario.Visible = false;
		this.lblresponsable.AutoSize = true;
		this.lblresponsable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblresponsable.ForeColor = System.Drawing.Color.Red;
		this.lblresponsable.Location = new System.Drawing.Point(523, 114);
		this.lblresponsable.Name = "lblresponsable";
		this.lblresponsable.Size = new System.Drawing.Size(89, 13);
		this.lblresponsable.TabIndex = 121;
		this.lblresponsable.Text = "Responsable *";
		this.lblresponsable.Visible = false;
		this.lblareaencargado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblareaencargado.ForeColor = System.Drawing.Color.Red;
		this.lblareaencargado.Location = new System.Drawing.Point(361, 114);
		this.lblareaencargado.Name = "lblareaencargado";
		this.lblareaencargado.Size = new System.Drawing.Size(117, 18);
		this.lblareaencargado.TabIndex = 120;
		this.lblareaencargado.Text = "Area Encargada* :";
		this.lblareaencargado.Visible = false;
		this.cmbarea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbarea.Cursor = System.Windows.Forms.Cursors.Hand;
		this.cmbarea.FormattingEnabled = true;
		this.cmbarea.Items.AddRange(new object[8] { "ALMACEN", "DESPACHO", "LOGISTICA", "TODOS", "VENTAS", "INGRESAR", "", "" });
		this.cmbarea.Location = new System.Drawing.Point(364, 135);
		this.cmbarea.Name = "cmbarea";
		this.cmbarea.Size = new System.Drawing.Size(151, 21);
		this.cmbarea.TabIndex = 119;
		this.cmbarea.Visible = false;
		this.txtCodFac.Enabled = false;
		this.txtCodFac.Location = new System.Drawing.Point(816, 133);
		this.txtCodFac.Name = "txtCodFac";
		this.txtCodFac.Size = new System.Drawing.Size(34, 20);
		this.txtCodFac.TabIndex = 125;
		this.txtCodFac.Visible = false;
		this.txtCodDoc.Enabled = false;
		this.txtCodDoc.Location = new System.Drawing.Point(788, 133);
		this.txtCodDoc.Name = "txtCodDoc";
		this.txtCodDoc.Size = new System.Drawing.Size(22, 20);
		this.txtCodDoc.TabIndex = 124;
		this.txtCodDoc.Visible = false;
		this.txtPorcDescuento.Location = new System.Drawing.Point(719, 104);
		this.txtPorcDescuento.Name = "txtPorcDescuento";
		this.txtPorcDescuento.Size = new System.Drawing.Size(48, 20);
		this.txtPorcDescuento.TabIndex = 123;
		this.txtPorcDescuento.Tag = "7";
		this.txtPorcDescuento.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(943, 533);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmNotaSalida";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Nota de Salida";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotaSalida_Load);
		base.Shown += new System.EventHandler(frmNotaSalida_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
