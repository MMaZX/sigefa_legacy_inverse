using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmVentaSeparacionAr : OfficeForm
{
	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmVendedor AdmVen = new clsAdmVendedor();

	private clsMoneda moneda = new clsMoneda();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsAdmAlmacen Admalmac = new clsAdmAlmacen();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsSerie ser = new clsSerie();

	private clsValidar ok = new clsValidar();

	private clsFormaPago fpago = new clsFormaPago();

	private clsSeparacion sepa = new clsSeparacion();

	private clsAdmSeparacion admSepa = new clsAdmSeparacion();

	public int CodDocumento;

	public int CodTransaccion;

	public int Proceso = 0;

	public int Procede = 0;

	public int CodCliente;

	public int CodVendedor;

	public int codFormaPago;

	public int CodSerie;

	public int CodSerieG = 0;

	public int numG = 0;

	public int manual = 0;

	private decimal TipoCambio = default(decimal);

	private decimal ret = default(decimal);

	public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();

	public List<clsDetalleSeparacionVenta> detalle1 = new List<clsDetalleSeparacionVenta>();

	public List<clsDetalleGuiaRemision> detalleg = new List<clsDetalleGuiaRemision>();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox4;

	private Button btnNuevaVenta;

	private Button btnImprimir;

	private TextBox textBox1;

	private Button btnSalir;

	public Button btnGuardar;

	public TextBox txtDireccion;

	private ComboBox cmbAlmacen;

	private Label lblAlmacen;

	private TextBox txtCodDocumento;

	public TextBox txtTransaccion;

	private Label label37;

	public CheckBox checkBox1;

	private TextBox txtDireccionCliente;

	private Label label16;

	private TextBox txtCodigoCli;

	public TextBox txtNumero;

	private Label label8;

	public TextBox txtTipoCambio;

	private Label label24;

	private ComboBox cbovendedor;

	private DateTimePicker dtpFechaPago;

	public ComboBox cmbFormaPago;

	private Label label3;

	private TextBox txtSerie;

	public ComboBox cmbMoneda;

	private Label label17;

	private TextBox txtNombreCliente;

	public TextBox txtCodCliente;

	private Label label15;

	private TextBox txtComentario;

	private Label label9;

	private TextBox txtNumDoc;

	private Label label7;

	private TextBox txtDocRef;

	private Label label5;

	private Label lbNombreTransaccion;

	private Label label2;

	private Label label1;

	private GroupBox groupBox2;

	private Label label36;

	private TextBox txtDetalle;

	private TextBox txtDsctoGobal;

	private Label label21;

	public Button btnNuevo;

	public Button btnEditar;

	public Button btnEliminar;

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

	public DataGridView dgvDetalle;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codSalida1;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

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

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn stockPend;

	private DataGridViewTextBoxColumn dsctoMax;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private ImageList imageList1;

	private SuperValidator superValidator1;

	private RequiredFieldValidator requiredFieldValidator3;

	private RequiredFieldValidator requiredFieldValidator1;

	private RequiredFieldValidator requiredFieldValidator2;

	public DateTimePicker dtpFecha;

	private GroupBox groupBox3;

	public frmVentaSeparacionAr()
	{
		InitializeComponent();
	}

	private void frmVentaSeparacionAr_Load(object sender, EventArgs e)
	{
		inicializarPagina();
	}

	private void inicializarPagina()
	{
		CargaMoneda();
		dtpFecha.MaxDate = DateTime.Today.Date;
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		CargaVendedores();
		CargaFormaPagos();
		btnNuevo.Focus();
		cargaAlmacenes();
	}

	private void cargaAlmacenes()
	{
		cmbAlmacen.DataSource = Admalmac.CargaAlmacen2(frmLogin.iCodEmpresa);
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagosCuotas();
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
	}

	private void CargaVendedores()
	{
		cbovendedor.DataSource = AdmVen.MuestraVendedoresDestaque();
		cbovendedor.DisplayMember = "apellido";
		cbovendedor.ValueMember = "codVendedor";
		cbovendedor.SelectedIndex = 0;
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 0;
	}

	private void frmVentaSeparacionAr_Shown(object sender, EventArgs e)
	{
		txtTransaccion.Focus();
		txtTransaccion.Text = "FT";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, ee);
		cmbAlmacen.Visible = true;
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

	private void txtTransaccion_KeyPress(TextBox txtTransaccion, KeyPressEventArgs ee)
	{
		if (ee.KeyChar == '\r' && txtTransaccion.Text != "")
		{
			if (BuscaTransaccion())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Codigo de transacción no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
				if (t.Tag != null && t.Tag != "")
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
			btnNuevo.Enabled = true;
			ProcessTabKey(forward: true);
			txtDocRef.Focus();
		}
	}

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		cli = AdmCli.CargaDeuda(cli);
		if (cli.Cantidad > 0)
		{
			DialogResult dlgResult = MessageBox.Show("El cliente selecionado presenta" + Environment.NewLine + "Facturas pendientes = " + cli.Cantidad + Environment.NewLine + "Deuda Total = " + cli.Deuda + " soles" + Environment.NewLine + "Linea de crédito = " + cli.LineaCredito + Environment.NewLine + " Desea continuar con la venta?", "Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult == DialogResult.No)
			{
				ret = 1m;
				return;
			}
			cargadatoscliente();
			ret = default(decimal);
		}
		else
		{
			cargadatoscliente();
			ret = default(decimal);
		}
	}

	private void cargadatoscliente()
	{
		txtCodCliente.Text = cli.RucDni;
		txtNombreCliente.Text = cli.RazonSocial;
		txtDireccionCliente.Text = cli.DireccionLegal;
		txtCodigoCli.Text = cli.CodCliente.ToString();
		cmbFormaPago.SelectedIndex = 0;
		if (cli.FormaPago != 0)
		{
			EventArgs ee = new EventArgs();
		}
		else
		{
			dtpFechaPago.Value = DateTime.Today;
		}
		if (cli.CodVendedor != 0)
		{
			cbovendedor.SelectedValue = cli.CodVendedor;
		}
		cmbMoneda.SelectedValue = cli.Moneda;
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
		}
		else if (cli.Ruc != "")
		{
			frmDocumentos form = new frmDocumentos();
			form.Proceso = 3;
			form.ShowDialog();
			doc = form.doc;
			CodDocumento = doc.CodTipoDocumento;
			txtCodDocumento.Text = CodDocumento.ToString();
			txtDocRef.Text = doc.Sigla;
			if (CodDocumento != 0)
			{
				ProcessTabKey(forward: true);
			}
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
			txtCodDocumento.Text = CodDocumento.ToString();
			return true;
		}
		CodDocumento = 0;
		txtCodDocumento.Text = CodDocumento.ToString();
		return false;
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
		form.Proceso = 3;
		form.DocSeleccionado = CodDocumento;
		form.ShowDialog();
		ser = form.ser;
		CodSerie = ser.CodSerie;
		manual = Convert.ToInt32(ser.PreImpreso);
		if (CodSerie != 0)
		{
			txtSerie.Text = ser.Serie;
		}
		if (CodSerie != 0)
		{
			ProcessTabKey(forward: true);
		}
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
			cmbFormaPago.Focus();
		}
		if (e.KeyChar == '\r')
		{
			cmbFormaPago.Focus();
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
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumero.Visible = true;
				txtNumero.Text = "";
				txtNumero.Focus();
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Visible = true;
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

	private bool BuscaSerie3(int codDocumento)
	{
		ser = Admser.MuestraSerie(codDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtDetalle.Text == "")
			{
				RecorreDetalle();
				if (Application.OpenForms["frmDetalleSalida"] != null)
				{
					Application.OpenForms["frmDetalleSalida"].Activate();
					return;
				}
				frmDetalleSalida form = new frmDetalleSalida();
				form.Procede = 5;
				form.Proceso = 1;
				form.consultorext = checkBox1.Checked;
				if (checkBox1.Checked)
				{
					form.CodVendedor = CodVendedor;
					form.Procede = 42;
					form.Proceso = 1;
					form.consultorext = checkBox1.Checked;
				}
				form.Tipo = 2;
				form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				form.tc = tc.Compra;
				form.alma = Convert.ToInt32(cmbAlmacen.SelectedValue);
				form.ShowDialog();
			}
			else
			{
				MessageBox.Show("No Puede Seguir Agregando más Detalles", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		detalle1.Clear();
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
		clsDetalleSeparacionVenta deta = new clsDetalleSeparacionVenta();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodSeparacion = Convert.ToInt32(sepa.CodSeparacion);
		deta.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		detalle1.Add(deta);
	}

	public void calculatotales()
	{
		if (Proceso == 0 || Procede == 3)
		{
			return;
		}
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal preciovent = default(decimal);
		decimal igvt = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			preciovent += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{igvt:#,##0.00}";
		txtPrecioVenta.Text = $"{preciovent:#,##0.00}";
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (Proceso == 0)
			{
				return;
			}
			sepa.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
			sepa.CodTipoTransaccion = tran.CodTransaccion;
			sepa.CodCliente = Convert.ToInt32(txtCodigoCli.Text);
			sepa.CodTipoDocumento = doc.CodTipoDocumento;
			sepa.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			sepa.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
			sepa.FechaRegistro = dtpFecha.Value;
			sepa.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
			sepa.CodVendedor = Convert.ToInt32(cbovendedor.SelectedValue);
			sepa.Comentario = txtComentario.Text;
			sepa.Bruto = Convert.ToDecimal(txtBruto.Text);
			sepa.MontoDescuento = Convert.ToDecimal(txtDscto.Text);
			sepa.Igv = Convert.ToDecimal(txtIGV.Text);
			sepa.Total = Convert.ToDecimal(txtPrecioVenta.Text);
			sepa.CodUsuario = frmLogin.iCodUser;
			sepa.CodSerie = ser.CodSerie;
			sepa.Serie = txtSerie.Text;
			sepa.NumDocumento = txtNumero.Text;
			if (dgvDetalle.Rows.Count > 0)
			{
				if (admSepa.insert(sepa))
				{
					RecorreDetalle();
					if (detalle1.Count <= 0)
					{
						return;
					}
					{
						foreach (clsDetalleSeparacionVenta det in detalle1)
						{
							admSepa.InsertarDetalleSepa(det);
							if (det.CodDetalleSeparacion != 0)
							{
								btnGuardar.Enabled = false;
								MessageBox.Show("Los datos se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								MessageBox.Show("Verifique su Stock", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						return;
					}
				}
				MessageBox.Show("Los datos no se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MessageBox.Show("Agregue Productos Para la venta", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
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
		form.Proceso = 4;
		form.ShowDialog();
		if (CodTransaccion != 0)
		{
			CargaTransaccion();
			ProcessTabKey(forward: true);
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
			if (t.Tag != null && t.Tag != "")
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
				MessageBox.Show("Codigo de transacción no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
	}

	private void btnNuevaVenta_Click(object sender, EventArgs e)
	{
	}

	private void txtTransaccion_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			txtTransaccion.Focus();
		}
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		int codProducto = 0;
		if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
		{
			codProducto = Convert.ToInt32(e.Row.Cells[codproducto.Name].Value.ToString());
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
			calculatotales();
		}
	}

	private void txtDocRef_KeyDown_1(object sender, KeyEventArgs e)
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
		form.Proceso = 3;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtCodDocumento.Text = CodDocumento.ToString();
		txtDocRef.Text = doc.Sigla;
		if (CodDocumento != 0)
		{
			ProcessTabKey(forward: true);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVentaSeparacionAr));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btnNuevaVenta = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnImprimir = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.lblAlmacen = new System.Windows.Forms.Label();
		this.txtCodDocumento = new System.Windows.Forms.TextBox();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label37 = new System.Windows.Forms.Label();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.txtDireccionCliente = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtCodigoCli = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.cbovendedor = new System.Windows.Forms.ComboBox();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codSalida1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockPend = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dsctoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label36 = new System.Windows.Forms.Label();
		this.txtDetalle = new System.Windows.Forms.TextBox();
		this.txtDsctoGobal = new System.Windows.Forms.TextBox();
		this.label21 = new System.Windows.Forms.Label();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
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
		this.requiredFieldValidator3 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
		this.requiredFieldValidator1 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
		this.requiredFieldValidator2 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.lblAlmacen);
		this.groupBox1.Controls.Add(this.txtCodDocumento);
		this.groupBox1.Controls.Add(this.txtTransaccion);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Controls.Add(this.txtDireccionCliente);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.txtCodigoCli);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.cbovendedor);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtNumDoc);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.lbNombreTransaccion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 434);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1015, 277);
		this.groupBox1.TabIndex = 6;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Moneda:";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpFecha.Location = new System.Drawing.Point(917, 50);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(89, 18);
		this.dtpFecha.TabIndex = 108;
		this.groupBox4.Controls.Add(this.btnNuevaVenta);
		this.groupBox4.Controls.Add(this.btnImprimir);
		this.groupBox4.Controls.Add(this.textBox1);
		this.groupBox4.Controls.Add(this.btnSalir);
		this.groupBox4.Controls.Add(this.btnGuardar);
		this.groupBox4.Controls.Add(this.txtDireccion);
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox4.Location = new System.Drawing.Point(3, 237);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(1009, 37);
		this.groupBox4.TabIndex = 107;
		this.groupBox4.TabStop = false;
		this.btnNuevaVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevaVenta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevaVenta.ImageIndex = 1;
		this.btnNuevaVenta.ImageList = this.imageList1;
		this.btnNuevaVenta.Location = new System.Drawing.Point(8, 0);
		this.btnNuevaVenta.Name = "btnNuevaVenta";
		this.btnNuevaVenta.Size = new System.Drawing.Size(105, 32);
		this.btnNuevaVenta.TabIndex = 25;
		this.btnNuevaVenta.Text = "Nueva Venta";
		this.btnNuevaVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevaVenta.UseVisualStyleBackColor = true;
		this.btnNuevaVenta.Visible = false;
		this.btnNuevaVenta.Click += new System.EventHandler(btnNuevaVenta_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(653, 3);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 32);
		this.btnImprimir.TabIndex = 24;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.textBox1.Location = new System.Drawing.Point(308, 15);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(39, 20);
		this.textBox1.TabIndex = 19;
		this.textBox1.Visible = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(935, 1);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 26;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(852, 2);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 23;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(357, 15);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(66, 20);
		this.txtDireccion.TabIndex = 14;
		this.txtDireccion.Tag = "21";
		this.txtDireccion.Visible = false;
		this.cmbAlmacen.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(714, 148);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(289, 20);
		this.cmbAlmacen.TabIndex = 105;
		this.cmbAlmacen.Tag = "0";
		this.lblAlmacen.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblAlmacen.AutoSize = true;
		this.lblAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAlmacen.Location = new System.Drawing.Point(629, 151);
		this.lblAlmacen.Name = "lblAlmacen";
		this.lblAlmacen.Size = new System.Drawing.Size(72, 12);
		this.lblAlmacen.TabIndex = 106;
		this.lblAlmacen.Tag = "";
		this.lblAlmacen.Text = "Almacen Venta:";
		this.txtCodDocumento.Location = new System.Drawing.Point(360, 199);
		this.txtCodDocumento.Name = "txtCodDocumento";
		this.txtCodDocumento.Size = new System.Drawing.Size(39, 20);
		this.txtCodDocumento.TabIndex = 104;
		this.txtCodDocumento.Visible = false;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTransaccion.Location = new System.Drawing.Point(76, 26);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.Size = new System.Drawing.Size(28, 18);
		this.txtTransaccion.TabIndex = 17;
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(411, 32);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(8, 12);
		this.label37.TabIndex = 103;
		this.label37.Text = "-";
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(249, 137);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(90, 17);
		this.checkBox1.TabIndex = 102;
		this.checkBox1.Text = "Venta Normal";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.Visible = false;
		this.txtDireccionCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDireccionCliente.Location = new System.Drawing.Point(76, 82);
		this.txtDireccionCliente.Name = "txtDireccionCliente";
		this.txtDireccionCliente.ReadOnly = true;
		this.txtDireccionCliente.Size = new System.Drawing.Size(422, 18);
		this.txtDireccionCliente.TabIndex = 3;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(6, 82);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(44, 12);
		this.label16.TabIndex = 89;
		this.label16.Text = "Direccion";
		this.txtCodigoCli.Enabled = false;
		this.txtCodigoCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodigoCli.Location = new System.Drawing.Point(505, 56);
		this.txtCodigoCli.Name = "txtCodigoCli";
		this.txtCodigoCli.ReadOnly = true;
		this.txtCodigoCli.Size = new System.Drawing.Size(19, 18);
		this.txtCodigoCli.TabIndex = 88;
		this.txtCodigoCli.Visible = false;
		this.txtNumero.Enabled = false;
		this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumero.Location = new System.Drawing.Point(422, 30);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(69, 18);
		this.txtNumero.TabIndex = 6;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(852, 101);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(59, 12);
		this.label8.TabIndex = 80;
		this.label8.Text = "Tipo Cambio:";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTipoCambio.Location = new System.Drawing.Point(917, 98);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(90, 18);
		this.txtTipoCambio.TabIndex = 13;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.Location = new System.Drawing.Point(6, 133);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(48, 12);
		this.label24.TabIndex = 75;
		this.label24.Text = "Vendedor:";
		this.cbovendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbovendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbovendedor.FormattingEnabled = true;
		this.cbovendedor.Items.AddRange(new object[1] { "Sin Vendedor" });
		this.cbovendedor.Location = new System.Drawing.Point(76, 134);
		this.cbovendedor.Name = "cbovendedor";
		this.cbovendedor.Size = new System.Drawing.Size(161, 20);
		this.cbovendedor.TabIndex = 10;
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpFechaPago.Location = new System.Drawing.Point(242, 108);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 18);
		this.dtpFechaPago.TabIndex = 8;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(76, 108);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(161, 20);
		this.cmbFormaPago.TabIndex = 7;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(6, 107);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(67, 12);
		this.label3.TabIndex = 44;
		this.label3.Tag = "16";
		this.label3.Text = "Forma de Pago";
		this.label3.Visible = false;
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.Location = new System.Drawing.Point(371, 30);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(38, 18);
		this.txtSerie.TabIndex = 5;
		this.txtSerie.Tag = "13";
		this.superValidator1.SetValidator1(this.txtSerie, this.requiredFieldValidator3);
		this.txtSerie.Visible = false;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(917, 74);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(89, 20);
		this.cmbMoneda.TabIndex = 12;
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.Location = new System.Drawing.Point(867, 77);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(44, 12);
		this.label17.TabIndex = 31;
		this.label17.Text = "Moneda :";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreCliente.Location = new System.Drawing.Point(182, 56);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(316, 18);
		this.txtNombreCliente.TabIndex = 2;
		this.txtNombreCliente.Tag = "3";
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodCliente.Location = new System.Drawing.Point(76, 56);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(100, 18);
		this.txtCodCliente.TabIndex = 1;
		this.txtCodCliente.Tag = "5";
		this.superValidator1.SetValidator1(this.txtCodCliente, this.requiredFieldValidator1);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(6, 58);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(34, 12);
		this.label15.TabIndex = 20;
		this.label15.Text = "Cliente";
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtComentario.Location = new System.Drawing.Point(76, 160);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(533, 37);
		this.txtComentario.TabIndex = 15;
		this.txtComentario.Tag = "21";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(17, 160);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 12);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Comentario";
		this.txtNumDoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumDoc.Location = new System.Drawing.Point(917, 29);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 18);
		this.txtNumDoc.TabIndex = 14;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(861, 32);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(50, 12);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(337, 30);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(28, 18);
		this.txtDocRef.TabIndex = 4;
		this.txtDocRef.Tag = "10";
		this.superValidator1.SetValidator1(this.txtDocRef, this.requiredFieldValidator2);
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown_1);
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(278, 33);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(45, 12);
		this.label5.TabIndex = 8;
		this.label5.Text = "Doc. Ref.";
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(110, 32);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(160, 13);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(6, 30);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(55, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(875, 56);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(36, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Location = new System.Drawing.Point(4, 1);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1011, 333);
		this.groupBox2.TabIndex = 5;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codSalida1, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.stockPend, this.dsctoMax, this.coduser, this.fecharegistro);
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle25;
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1005, 314);
		this.dgvDetalle.TabIndex = 2;
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codSalida1.HeaderText = "CodSalida";
		this.codSalida1.Name = "codSalida1";
		this.codSalida1.ReadOnly = true;
		this.codSalida1.Visible = false;
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
		this.referencia.Width = 87;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 400;
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
		this.unidad.Width = 75;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.ReadOnly = true;
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle27.Format = "N2";
		dataGridViewCellStyle27.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle27;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 70;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle28.Format = "N2";
		dataGridViewCellStyle28.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle28;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 75;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle29.Format = "N2";
		dataGridViewCellStyle29.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle29;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Width = 85;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle30.Format = "N2";
		dataGridViewCellStyle30.NullValue = null;
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle30;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle31.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle31;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle32.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle32;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle33.Format = "N2";
		dataGridViewCellStyle33.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle33;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Width = 80;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle34.Format = "N2";
		dataGridViewCellStyle34.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle34;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Width = 85;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle35.Format = "N2";
		dataGridViewCellStyle35.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle35;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle36.Format = "N2";
		dataGridViewCellStyle36.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle36;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Width = 85;
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
		this.stockPend.DataPropertyName = "stockdisponible";
		this.stockPend.HeaderText = "StockDisponible";
		this.stockPend.Name = "stockPend";
		this.stockPend.ReadOnly = true;
		this.stockPend.Visible = false;
		this.dsctoMax.DataPropertyName = "maxPorcDescto";
		this.dsctoMax.HeaderText = "%DsctoMax";
		this.dsctoMax.Name = "dsctoMax";
		this.dsctoMax.ReadOnly = true;
		this.dsctoMax.Visible = false;
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
		this.label36.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label36.AutoSize = true;
		this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label36.Location = new System.Drawing.Point(232, 11);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(37, 12);
		this.label36.TabIndex = 51;
		this.label36.Text = "Detalle:";
		this.txtDetalle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtDetalle.BackColor = System.Drawing.SystemColors.Window;
		this.txtDetalle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDetalle.Location = new System.Drawing.Point(234, 23);
		this.txtDetalle.Multiline = true;
		this.txtDetalle.Name = "txtDetalle";
		this.txtDetalle.Size = new System.Drawing.Size(375, 53);
		this.txtDetalle.TabIndex = 50;
		this.txtDsctoGobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDsctoGobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDsctoGobal.Location = new System.Drawing.Point(723, 34);
		this.txtDsctoGobal.Name = "txtDsctoGobal";
		this.txtDsctoGobal.ReadOnly = true;
		this.txtDsctoGobal.Size = new System.Drawing.Size(75, 18);
		this.txtDsctoGobal.TabIndex = 46;
		this.txtDsctoGobal.Tag = "7";
		this.txtDsctoGobal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(643, 37);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(63, 12);
		this.label21.TabIndex = 43;
		this.label21.Tag = "7";
		this.label21.Text = "Dscto Global :";
		this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevo.Enabled = false;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(4, 26);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 39;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEditar.Enabled = false;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(81, 26);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 41;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(153, 26);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 42;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBruto.Location = new System.Drawing.Point(720, 58);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 18);
		this.txtBruto.TabIndex = 44;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioVenta.Location = new System.Drawing.Point(877, 54);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 18);
		this.txtPrecioVenta.TabIndex = 49;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(824, 57);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(46, 12);
		this.label14.TabIndex = 40;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtIGV.Location = new System.Drawing.Point(877, 31);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 18);
		this.txtIGV.TabIndex = 48;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(833, 37);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(36, 12);
		this.label13.TabIndex = 38;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDscto.Location = new System.Drawing.Point(723, 8);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 18);
		this.txtDscto.TabIndex = 45;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtValorVenta.Location = new System.Drawing.Point(877, 8);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 18);
		this.txtValorVenta.TabIndex = 47;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(824, 11);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(47, 12);
		this.label12.TabIndex = 37;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(652, 11);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(55, 12);
		this.label11.TabIndex = 36;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(672, 61);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 35;
		this.label10.Text = "Bruto :";
		this.requiredFieldValidator3.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.requiredFieldValidator1.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.requiredFieldValidator2.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.groupBox3.Controls.Add(this.txtDetalle);
		this.groupBox3.Controls.Add(this.label36);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.txtDsctoGobal);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.label21);
		this.groupBox3.Controls.Add(this.txtValorVenta);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.txtDscto);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.txtIGV);
		this.groupBox3.Controls.Add(this.txtBruto);
		this.groupBox3.Controls.Add(this.label14);
		this.groupBox3.Controls.Add(this.txtPrecioVenta);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 353);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1015, 81);
		this.groupBox3.TabIndex = 52;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Buscar Detalle";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(1015, 711);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmVentaSeparacionAr";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Venta Por Separacion";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmVentaSeparacionAr_Load);
		base.Shown += new System.EventHandler(frmVentaSeparacionAr_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
	}
}
