using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

public class frmNotadeDebitoCompra : Office2007Form
{
	private ClsNotasCreditoDebitoCompra ds = new ClsNotasCreditoDebitoCompra();

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

	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsNotaIngreso nota = new clsNotaIngreso();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

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

	public List<int> config = new List<int>();

	public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();

	public List<clsDetalleFactura> detallefact = new List<clsDetalleFactura>();

	public List<clsDetalleNotaSalida> detalleS = new List<clsDetalleNotaSalida>();

	public string CodNota;

	public int CodNotaS;

	public int CodNotaI;

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

	private clsAdmFactura AdmFactura = new clsAdmFactura();

	private clsFactura factur = new clsFactura();

	private DataTable dt1 = new DataTable();

	private clsAdmFactura AdmFact = new clsAdmFactura();

	public int CodFactura;

	public string DocRef;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Label label2;

	private Label label5;

	private Label lbNombreTransaccion;

	private TextBox txtNumDoc;

	private Label label7;

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

	public TextBox txtTransaccion;

	private CheckBox cbValorVenta;

	public TextBox txtNombreProveedor;

	public TextBox txtCodProveedor;

	private Label label18;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Button btnImprimir;

	private Button btnNuevaGuia;

	private TextBox txtSerie;

	private ComboBox cmbMotivo;

	private Label label3;

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

	private Label label4;

	private Label lblOtros;

	private TextBox txtOtros;

	public TextBox txtDocRef;

	public ComboBox cmbFormaPago;

	private Label label6;

	public frmNotadeDebitoCompra()
	{
		InitializeComponent();
	}

	private void frmNotadeDebitoCompra_Load(object sender, EventArgs e)
	{
		CargaMoneda();
		CargaFormaPagos();
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		if (Proceso == 1)
		{
			Bloqueabotones();
		}
		if (Proceso == 2)
		{
			CargaNotaSalida();
		}
		else if (Proceso == 3)
		{
			CargaNotaSalida();
			sololectura(estado: true);
		}
		else if (Proceso == 4)
		{
			CargaNotaSalida();
			sololectura(estado: true);
		}
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
	}

	private void frmNotadeDebitoCompra_Shown(object sender, EventArgs e)
	{
		txtTransaccion.Focus();
		txtTransaccion.Text = "NDC";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, ee);
		ser = AdmSerie.BuscaSeriexDocumento(6, frmLogin.iCodAlmacen);
		cmbMotivo.Focus();
		if (txtOtros.Visible)
		{
			lblOtros.Visible = true;
		}
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
		doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
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
	}

	public void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtTransaccion.Text != "")
		{
			if (BuscaTransaccion())
			{
				ProcessTabKey(forward: true);
				cmbMotivo.Focus();
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

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedValue = 1;
	}

	private void sololectura(bool estado)
	{
		txtTransaccion.ReadOnly = estado;
		dtpFecha.Enabled = !estado;
		cmbMoneda.Enabled = !estado;
		txtCodProveedor.ReadOnly = estado;
		txtDocRef.ReadOnly = !estado;
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
		txtTransaccion.Enabled = !estado;
		cmbMotivo.Enabled = !estado;
		txtCodProveedor.Enabled = !estado;
		txtComentario.Enabled = !estado;
		txtDocRef.Enabled = !estado;
		txtSerie.Enabled = !estado;
		txtNumDoc.Enabled = !estado;
		if (txtOtros.Visible)
		{
			txtOtros.Enabled = !estado;
			lblOtros.Visible = true;
		}
	}

	private void Bloqueabotones()
	{
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
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

	private void CargaNotaSalida()
	{
		try
		{
			factur = AdmFact.CargaFactura(CodNotaS);
			if (factur != null)
			{
				txtTransaccion.Text = factur.SiglaTransaccion;
				cmbMotivo.SelectedItem = factur.Motivo;
				if (cmbMotivo.SelectedIndex == -1)
				{
					cmbMotivo.SelectedItem = "Otros";
					lblOtros.Visible = true;
					txtOtros.Visible = true;
					txtOtros.Text = factur.Motivo;
				}
				string[] A = factur.DocumentoFactura.Split('-');
				txtCodProveedor.Text = factur.RUCProveedor;
				txtNombreProveedor.Text = factur.RazonSocialProveedor;
				txtComentario.Text = factur.Comentario;
				txtDocRef.Text = DocRef;
				cmbMoneda.SelectedValue = factur.Moneda;
				dtpFecha.Value = factur.FechaIngreso;
				txtTipoCambio.Text = factur.TipoCambio.ToString();
				CargaDetalleNotaS();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	public void CargaDetalleNotaS()
	{
		dt1.Clear();
		dt1 = AdmFact.CargaDetalle(CodNotaS);
		dgvDetalle.DataSource = dt1;
		dgvDetalle.Columns["stockdisponible"].Visible = false;
		dgvDetalle.Columns["maxPorcDescto"].Visible = false;
		CalculaTotales();
	}

	private void CargaDetalleNota()
	{
		dt1.Clear();
		if (cmbMotivo.SelectedIndex == 0)
		{
			dt1 = AdmFactura.CargaDetalle(Convert.ToInt32(factur.CodFactura));
			dgvDetalle.DataSource = dt1;
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
			dgvDetalle.Columns["stockdisponible"].Visible = false;
			dgvDetalle.Columns["maxPorcDescto"].Visible = false;
			btnEliminar.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 1)
		{
			dt1 = AdmPro.BuscarProducto(1516);
			dgvDetalle.DataSource = dt1;
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
			dgvDetalle.Columns["stockdisponible"].Visible = false;
			dgvDetalle.Columns["maxPorcDescto"].Visible = false;
			btnEliminar.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 2)
		{
			dt1 = AdmPro.BuscarProducto(1519);
			dgvDetalle.DataSource = dt1;
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
			dgvDetalle.Columns["stockdisponible"].Visible = false;
			dgvDetalle.Columns["maxPorcDescto"].Visible = false;
			btnEliminar.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 3)
		{
			dt1 = AdmPro.BuscarProducto(1520);
			dgvDetalle.DataSource = dt1;
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
			dgvDetalle.Columns["stockdisponible"].Visible = false;
			dgvDetalle.Columns["maxPorcDescto"].Visible = false;
			btnEliminar.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 4)
		{
			dt1 = AdmPro.BuscarProducto(1521);
			dgvDetalle.DataSource = dt1;
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
			dgvDetalle.Columns["stockdisponible"].Visible = false;
			dgvDetalle.Columns["maxPorcDescto"].Visible = false;
			btnEliminar.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 5)
		{
			dt1 = AdmPro.BuscarProducto(1522);
			dgvDetalle.DataSource = dt1;
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
			dgvDetalle.Columns["stockdisponible"].Visible = false;
			dgvDetalle.Columns["maxPorcDescto"].Visible = false;
			btnEliminar.Visible = false;
		}
		CalculaTotales();
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1)
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
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
			igvt += Convert.ToDouble(row.Cells[igv.Name].Value);
			preciot += Convert.ToDouble(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{igvt:#,##0.00}";
		txtPrecioVenta.Text = $"{preciot:#,##0.00}";
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
		else if (CodTransaccion != 7)
		{
			cmbMotivo.Focus();
		}
	}

	private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
	{
		if (txtPrecioVenta.Text != "")
		{
			btnGuardar.Enabled = true;
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (Proceso == 0)
		{
			return;
		}
		if (cmbMotivo.SelectedIndex == -1)
		{
			MessageBox.Show("Por favor seleccionar un motivo!");
			cmbMotivo.Focus();
			return;
		}
		factur.CodAlmacen = frmLogin.iCodAlmacen;
		factur.CodProveedor = prov.CodProveedor;
		factur.CodTipoTransaccion = tran.CodTransaccion;
		factur.CodTipoDocumento = 6;
		factur.DocumentoFactura = "ND-" + txtSerie.Text.PadLeft(4, '0') + "-" + txtNumDoc.Text.PadLeft(4, '0');
		factur.CodSerie = ser.CodSerie;
		factur.Serie = txtSerie.Text.PadLeft(4, '0');
		factur.CodReferencia = CodFactura;
		factur.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		factur.FechaIngreso = dtpFecha.Value.Date;
		factur.FormaPago = 0;
		factur.Comentario = txtComentario.Text;
		if (txtTipoCambio.Visible)
		{
			factur.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
		}
		factur.MontoBruto = Convert.ToDouble(txtBruto.Text);
		factur.MontoDscto = Convert.ToDouble(txtDscto.Text);
		factur.Igv = Convert.ToDouble(txtIGV.Text);
		factur.Total = Convert.ToDouble(txtPrecioVenta.Text);
		factur.CodUser = frmLogin.iCodUser;
		factur.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
		factur.Motivo = cmbMotivo.SelectedItem.ToString();
		factur.Cancelado = 0;
		DocRef = txtDocRef.Text;
		if (txtOtros.Visible)
		{
			factur.Motivo = txtOtros.Text;
		}
		factur.Estado = 1;
		if (Proceso != 1)
		{
			return;
		}
		if (factur.Total != 0.0)
		{
			if (txtSerie.Text != "" && txtNumDoc.Text != "")
			{
				if (!AdmFactura.insert(factur))
				{
					return;
				}
				RecorreDetalleF();
				if (detallefact.Count > 0)
				{
					foreach (clsDetalleFactura det in detallefact)
					{
						AdmFact.insertdetalle(det);
					}
				}
				MessageBox.Show("Los datos se guardaron correctamente!", "Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				dgvDetalle.Enabled = false;
				CodNotaS = factur.CodFacturaNueva;
				CargaNotaSalida();
				sololectura(estado: true);
			}
			else
			{
				MessageBox.Show("Por favor ingrese Serie y Correlativo del Documento!", "Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtSerie.Focus();
			}
		}
		else
		{
			MessageBox.Show("El valor ingresado no es correcto!", "Nota de Debito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		clsDetalleNotaIngreso deta = new clsDetalleNotaIngreso();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
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
	}

	private void RecorreDetalleF()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleF(row);
		}
	}

	private void añadedetalleF(DataGridViewRow fila)
	{
		clsDetalleFactura detafac = new clsDetalleFactura();
		detafac.CodFactura = factur.CodFacturaNueva;
		detafac.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detafac.CodNotaIngreso = "0";
		detafac.CodAlmacen = frmLogin.iCodAlmacen;
		detafac.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detafac.SerieLote = "0";
		detafac.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue.ToString());
		detafac.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		detafac.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detafac.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detafac.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detafac.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detafac.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detafac.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detafac.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		detafac.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
		detafac.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		detafac.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		detafac.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		detafac.CodUser = frmLogin.iCodUser;
		detafac.CodProveedor = prov.CodProveedor;
		detallefact.Add(detafac);
	}

	private void CargaFilaDetalle(DataGridViewRow fila)
	{
		detaSelec.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detaSelec.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
		detaSelec.CodAlmacen = frmLogin.iCodAlmacen;
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
		if (dgvDetalle.Rows.Count > 0)
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
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
		if (Application.OpenForms["frmProveedoresLista"] != null)
		{
			Application.OpenForms["frmProveedoresLista"].Activate();
			return;
		}
		frmProveedoresLista form = new frmProveedoresLista();
		form.Proceso = 3;
		form.Procede = 7;
		form.ShowDialog();
		dt1.Clear();
		dgvDetalle.DataSource = dt1;
		if (CodProveedor != 0)
		{
			CargaProveedor();
			txtDocRef.Focus();
		}
		else
		{
			BorrarProveedor();
		}
	}

	private void CargaProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtCodProveedor.Text = prov.Ruc;
		txtNombreProveedor.Text = prov.RazonSocial;
	}

	private void BorrarProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtCodProveedor.Text = "";
		txtNombreProveedor.Text = "";
	}

	private bool BuscaProveedor()
	{
		prov = AdmProv.BuscaProveedor(txtCodProveedor.Text);
		if (prov != null)
		{
			txtNombreProveedor.Text = prov.RazonSocial;
			CodProveedor = prov.CodProveedor;
			return true;
		}
		txtNombreProveedor.Text = "";
		CodProveedor = 0;
		return false;
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvDetalle.Focused)
			{
				pro = AdmPro.CargaProducto(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen);
				double cantidad1 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
				double precio = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
				double bruto = cantidad1 * precio;
				double dsc1 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[dscto1.Name].Value);
				double dsc2 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[dscto2.Name].Value);
				double dsc3 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[dscto3.Name].Value);
				double precioventa1 = bruto * (1.0 - dsc1 / 100.0) * (1.0 - dsc2 / 100.0) * (1.0 - dsc3 / 100.0);
				double montodescuento = bruto - precioventa1;
				double valorventa1;
				if (pro.ConIgv)
				{
					double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
					valorventa1 = precioventa1 / factorigv;
				}
				else
				{
					valorventa1 = precioventa1;
				}
				double igv1 = precioventa1 - valorventa1;
				dgvDetalle.CurrentRow.Cells[importe.Name].Value = bruto;
				dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = montodescuento;
				dgvDetalle.CurrentRow.Cells[valorventa.Name].Value = valorventa1;
				dgvDetalle.CurrentRow.Cells[igv.Name].Value = igv1;
				dgvDetalle.CurrentRow.Cells[precioventa.Name].Value = precioventa1;
				CalculaTotales();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			ser = AdmSerie.MuestraSerie(factur.CodSerie, frmLogin.iCodAlmacen);
			ReportDocument rd = new ReportDocument();
			rd.Load("CRNotaDebito.rpt");
			CRNotaDebito rpt = new CRNotaDebito();
			rd.SetDataSource(ds.ReportNotaDebitoCompra(Convert.ToInt32(CodNotaS), frmLogin.iCodAlmacen));
			PrintOptions rptoption = rd.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(1100, 1850, 200, 1300));
			rd.PrintToPrinter(1, collated: false, 1, 1);
			rd.Close();
			rd.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema " + ex.Message, "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnNuevaGuia_Click(object sender, EventArgs e)
	{
		frmNotadeDebitoCompra form2 = new frmNotadeDebitoCompra();
		form2.MdiParent = base.MdiParent;
		form2.Proceso = 1;
		form2.Show();
		Close();
	}

	private void cmbMotivo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0)
		{
			CargaDetalleNota();
		}
		if (cmbMotivo.SelectedIndex == 0)
		{
			dgvDetalle.Columns[preciounit.Name].HeaderText = "P. Unit.";
			lblOtros.Visible = false;
			txtOtros.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 1)
		{
			dgvDetalle.Columns[preciounit.Name].HeaderText = "Gasto";
			lblOtros.Visible = false;
			txtOtros.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 2 || cmbMotivo.SelectedIndex == 3)
		{
			dgvDetalle.Columns[preciounit.Name].HeaderText = "Interés";
			lblOtros.Visible = false;
			txtOtros.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 4)
		{
			dgvDetalle.Columns[preciounit.Name].HeaderText = "Valor";
			lblOtros.Visible = false;
			txtOtros.Visible = false;
		}
		else if (cmbMotivo.SelectedIndex == 5)
		{
			dgvDetalle.Columns[preciounit.Name].HeaderText = "Valor";
			lblOtros.Visible = true;
			txtOtros.Visible = true;
			txtOtros.Focus();
		}
	}

	private void CargaFacturaGrid()
	{
		try
		{
			factur = AdmFactura.CargaFactura(CodFactura);
			if (factur != null)
			{
				txtDocRef.Text = factur.DocumentoFactura;
				txtTipoCambio.Text = factur.TipoCambio.ToString();
				cmbMoneda.SelectedValue = factur.Moneda;
				CargaDetalleNota();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmListaFacturasPorProveedor"] != null)
		{
			Application.OpenForms["frmListaFacturasPorProveedor"].Activate();
			return;
		}
		frmListaFacturasPorProveedor form = new frmListaFacturasPorProveedor();
		form.CodProveedor = CodProveedor;
		form.tipo = 2;
		form.ShowDialog();
		if (form.factura != null && form.factura.CodFactura != 0)
		{
			factur = form.factura;
			CodFactura = Convert.ToInt32(factur.CodFactura);
		}
		if (CodFactura != 0)
		{
			CargaFacturaGrid();
			txtComentario.Focus();
		}
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotadeDebitoCompra));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.txtOtros = new System.Windows.Forms.TextBox();
		this.lblOtros = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cmbMotivo = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtNombreProveedor = new System.Windows.Forms.TextBox();
		this.txtCodProveedor = new System.Windows.Forms.TextBox();
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
		this.label5 = new System.Windows.Forms.Label();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnNuevaGuia = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
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
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.txtOtros);
		this.groupBox1.Controls.Add(this.lblOtros);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.cmbMotivo);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtNombreProveedor);
		this.groupBox1.Controls.Add(this.txtCodProveedor);
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
		this.groupBox1.Size = new System.Drawing.Size(940, 155);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Enabled = false;
		this.cmbFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(299, 100);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(161, 20);
		this.cmbFormaPago.TabIndex = 66;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.Visible = false;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.label6.Location = new System.Drawing.Point(211, 102);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(79, 13);
		this.label6.TabIndex = 67;
		this.label6.Tag = "16";
		this.label6.Text = "Forma de Pago";
		this.label6.Visible = false;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(102, 99);
		this.txtDocRef.MaxLength = 11;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(88, 20);
		this.txtDocRef.TabIndex = 65;
		this.txtDocRef.Tag = "8";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtOtros.BackColor = System.Drawing.SystemColors.Control;
		this.txtOtros.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtOtros.Location = new System.Drawing.Point(401, 47);
		this.txtOtros.MaxLength = 500;
		this.txtOtros.Name = "txtOtros";
		this.txtOtros.Size = new System.Drawing.Size(287, 20);
		this.txtOtros.TabIndex = 3;
		this.txtOtros.Visible = false;
		this.lblOtros.AutoSize = true;
		this.lblOtros.Location = new System.Drawing.Point(363, 47);
		this.lblOtros.Name = "lblOtros";
		this.lblOtros.Size = new System.Drawing.Size(32, 13);
		this.lblOtros.TabIndex = 64;
		this.lblOtros.Tag = "90";
		this.lblOtros.Text = "Otros";
		this.lblOtros.Visible = false;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(666, 21);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 15);
		this.label4.TabIndex = 63;
		this.label4.Text = "ND";
		this.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMotivo.FormattingEnabled = true;
		this.cmbMotivo.Items.AddRange(new object[6] { "Ajuste por diferencia precio", "Gastos financieros", "Interes compensatorios", "Interes moratorio", "Anulacion", "Otros" });
		this.cmbMotivo.Location = new System.Drawing.Point(102, 44);
		this.cmbMotivo.Name = "cmbMotivo";
		this.cmbMotivo.Size = new System.Drawing.Size(242, 21);
		this.cmbMotivo.TabIndex = 2;
		this.cmbMotivo.SelectionChangeCommitted += new System.EventHandler(cmbMotivo_SelectionChangeCommitted);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(17, 47);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 13);
		this.label3.TabIndex = 49;
		this.label3.Tag = "21";
		this.label3.Text = "Motivo";
		this.txtSerie.BackColor = System.Drawing.SystemColors.Window;
		this.txtSerie.Location = new System.Drawing.Point(562, 18);
		this.txtSerie.MaxLength = 6;
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.Size = new System.Drawing.Size(35, 20);
		this.txtSerie.TabIndex = 8;
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtNombreProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreProveedor.Enabled = false;
		this.txtNombreProveedor.Location = new System.Drawing.Point(196, 73);
		this.txtNombreProveedor.Name = "txtNombreProveedor";
		this.txtNombreProveedor.ReadOnly = true;
		this.txtNombreProveedor.Size = new System.Drawing.Size(492, 20);
		this.txtNombreProveedor.TabIndex = 4;
		this.txtNombreProveedor.Tag = "9";
		this.txtNombreProveedor.Visible = false;
		this.txtCodProveedor.Location = new System.Drawing.Point(102, 73);
		this.txtCodProveedor.MaxLength = 11;
		this.txtCodProveedor.Name = "txtCodProveedor";
		this.txtCodProveedor.Size = new System.Drawing.Size(88, 20);
		this.txtCodProveedor.TabIndex = 3;
		this.txtCodProveedor.Tag = "8";
		this.txtCodProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtCodProveedor.Visible = false;
		this.txtCodProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(17, 76);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(56, 13);
		this.label18.TabIndex = 44;
		this.label18.Tag = "8";
		this.label18.Text = "Proveedor";
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
		this.txtTipoCambio.TabIndex = 10;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(800, 44);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(128, 21);
		this.cmbMoneda.TabIndex = 20;
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
		this.label15.Location = new System.Drawing.Point(742, 47);
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
		this.txtComentario.Location = new System.Drawing.Point(102, 125);
		this.txtComentario.MaxLength = 500;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(451, 20);
		this.txtComentario.TabIndex = 7;
		this.txtComentario.Tag = "21";
		this.txtComentario.Visible = false;
		this.txtComentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComentario_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(18, 128);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(34, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Glosa";
		this.label9.Visible = false;
		this.txtNumDoc.BackColor = System.Drawing.SystemColors.Window;
		this.txtNumDoc.Location = new System.Drawing.Point(601, 18);
		this.txtNumDoc.MaxLength = 6;
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.Size = new System.Drawing.Size(59, 20);
		this.txtNumDoc.TabIndex = 9;
		this.txtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumDoc_KeyPress);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(500, 21);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 103);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.label5.Visible = false;
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(136, 21);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(252, 15);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.lbNombreTransaccion.Visible = false;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Location = new System.Drawing.Point(102, 18);
		this.txtTransaccion.Name = "txtTransaccion";
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
		this.dtpFecha.TabIndex = 60;
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
		this.groupBox2.Location = new System.Drawing.Point(0, 155);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(940, 270);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(322, 192);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(823, 244);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(730, 247);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(823, 218);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(730, 221);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(494, 192);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(823, 192);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(730, 195);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(423, 195);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(278, 195);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.moneda, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.flete, this.precioventa, this.precioreal, this.valoreal, this.fechaingreso, this.coduser, this.fecharegistro);
		this.dgvDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(931, 170);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
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
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		dataGridViewCellStyle1.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle2;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle3;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle4;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle5;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle6;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N2";
		dataGridViewCellStyle7.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle7;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N2";
		dataGridViewCellStyle8.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle8;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle9;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.flete.DataPropertyName = "flete";
		this.flete.HeaderText = "Flete";
		this.flete.Name = "flete";
		this.flete.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle10;
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
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(940, 473);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmNotadeDebitoCompra";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Nota de Debito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotadeDebitoCompra_Load);
		base.Shown += new System.EventHandler(frmNotadeDebitoCompra_Shown);
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
