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
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmNotadeDebito : Office2007Form
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

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsNotaDebito notad = new clsNotaDebito();

	private clsAdmNotaDebito AdmNotaD = new clsAdmNotaDebito();

	public List<int> config = new List<int>();

	public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();

	public List<clsDetalleFacturaVenta> detalle1 = new List<clsDetalleFacturaVenta>();

	public string CodNota;

	public int CodNotaS;

	public int CodTransaccion;

	public int CodProveedor;

	public int CodCliente = 0;

	public int CodDocumento;

	public int CodOrdenCompra;

	public int CodAutorizado;

	public int Proceso = 0;

	public int Tipo;

	public int CodSerie;

	public int CodSerieG = 0;

	public int numG = 0;

	public int manual = 0;

	private DataTable dt1 = new DataTable();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private TextBox txtedit = new TextBox();

	private IContainer components = null;

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

	public DataGridView dgvDetalle;

	private GroupBox groupBox3;

	private Button btnNuevaGuia;

	private Button btnImprimir;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnEliminar;

	private GroupBox groupBox1;

	private Label label3;

	private ComboBox cmbMotivo;

	private Label label4;

	public TextBox txtDireccionCliente;

	public TextBox txtNombreCliente;

	private Label label18;

	private CheckBox cbValorVenta;

	private TextBox txtTipoCambio;

	private ComboBox cmbMoneda;

	private Label label16;

	private Label label15;

	private Button btnDetalle;

	private Label label9;

	private TextBox txtNumDoc;

	private Label label7;

	public TextBox txtDocRef;

	private Label label5;

	private Label lbNombreTransaccion;

	public TextBox txtTransaccion;

	private Label label2;

	private DateTimePicker dtpFecha;

	private Label label1;

	private ImageList imageList1;

	public TextBox txtComentario;

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

	private TextBox txtCodDocumento;

	private Label label37;

	public TextBox txtNumero;

	private TextBox txtSerie;

	private TextBox txtDocRefe;

	private Label label6;

	public TextBox txtCodCliente;

	public frmNotadeDebito()
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

	private void txtTransaccion_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			txtTransaccion.Focus();
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
		form.CodCliente = CodCliente;
		form.tipo = 2;
		form.ShowDialog();
		if (form.venta != null && form.venta.CodFacturaVenta != "")
		{
			venta = form.venta;
			CodNotaS = Convert.ToInt32(venta.CodFacturaVenta);
		}
		if (CodNotaS != 0)
		{
			CargaNotaSalida();
			ProcessTabKey(forward: true);
		}
	}

	private void txtDocRef_Leave(object sender, EventArgs e)
	{
	}

	private void CargaNotaSalida()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(CodNotaS);
			if (venta != null)
			{
				txtDocRef.Text = venta.SiglaDocumento + " - " + venta.Serie + " - " + venta.NumDoc;
				txtTipoCambio.Text = venta.TipoCambio.ToString();
				cmbMoneda.SelectedValue = venta.Moneda;
				if (txtCodCliente.Enabled)
				{
					CodCliente = venta.CodCliente;
					cli = AdmCli.MuestraCliente(CodCliente);
					txtCodCliente.Text = cli.RucDni;
					txtNombreCliente.Text = cli.Nombre;
				}
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

	private void CargaDetalleNota()
	{
		dt1.Clear();
		dt1 = AdmVenta.CargaDetalle(CodNotaS, frmLogin.iCodAlmacen, 0);
		dgvDetalle.DataSource = dt1;
		dgvDetalle.Columns["stockdisponible"].Visible = false;
		dgvDetalle.Columns["maxPorcDescto"].Visible = false;
		btnEliminar.Visible = false;
		btnGuardar.Enabled = true;
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

	private void BorrarNota()
	{
		try
		{
			CodNotaS = 0;
			notaS = new clsNotaSalida();
			txtDocRef.Text = "";
			DataTable dt = (DataTable)dgvDetalle.DataSource;
			dt.Clear();
		}
		catch (Exception)
		{
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

	private void frmNotadeDebito_Shown(object sender, EventArgs e)
	{
		txtTransaccion.Focus();
		txtTransaccion.Text = "NDV";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, ee);
		txtDocRefe.Text = "ND";
		KeyPressEventArgs ee2 = new KeyPressEventArgs('\r');
		txtDocRefe_Leave(txtDocRefe, ee2);
		ser = AdmSerie.BuscaSeriexDocumento(6, frmLogin.iCodAlmacen);
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

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (Proceso == 0)
		{
			return;
		}
		if (cmbMotivo.SelectedIndex == -1)
		{
			MessageBox.Show("Por favor seleccionar un motivo!", "Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			cmbMotivo.Focus();
		}
		else if (dgvDetalle.Rows.Count > 0)
		{
			notad.CodTipoTransaccion = tran.CodTransaccion;
			notad.CodAlmacen = frmLogin.iCodAlmacen;
			notad.CodCliente = cli.CodCliente;
			notad.CodTipoDocumento = 6;
			notad.CodSerie = ser.CodSerie;
			notad.Serie = ser.Serie;
			notad.DocumentoNotaDebito = ser.Numeracion.ToString();
			notad.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			notad.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
			notad.FechaRegistro = dtpFecha.Value.Date;
			notad.FormaPago = 0;
			notad.Motivo = cmbMotivo.SelectedItem.ToString();
			notad.Comentario = txtComentario.Text;
			notad.MontoBruto = Convert.ToDouble(txtValorVenta.Text);
			notad.MontoDscto = Convert.ToDouble(txtDscto.Text);
			notad.Igv = Convert.ToDouble(txtIGV.Text);
			notad.Total = Convert.ToDouble(txtPrecioVenta.Text);
			notad.CodUser = frmLogin.iCodUser;
			if (CodNotaS != 0)
			{
				notad.CodReferencia = Convert.ToInt32(CodNotaS.ToString());
			}
			if (notad.Total != 0.0)
			{
				if (AdmNotaD.insert(notad))
				{
					RecorreDetalle();
					MessageBox.Show("Los datos se guardaron correctamente", "Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					txtNumDoc.Text = venta.CodFacturaVenta.PadLeft(11, '0');
					sololectura(estado: true);
				}
			}
			else
			{
				MessageBox.Show("El valor ingresado no es correcto!", "Nota de Debito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			CargaNotaSalida();
		}
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 0;
	}

	private void frmNotadeDebito_Load(object sender, EventArgs e)
	{
		CargaMoneda();
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		if (Proceso == 1)
		{
			Bloqueabotones();
		}
		if (Proceso == 2)
		{
			CargaFacturaVenta();
		}
		else if (Proceso == 3)
		{
			CargaFacturaVenta();
			sololectura(estado: true);
		}
		else if (Proceso == 4)
		{
			CargaFacturaVenta();
			sololectura(estado: true);
		}
	}

	private void CargaFacturaVenta()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodNota));
			ser = AdmSerie.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
			if (venta != null)
			{
				if (venta.DocumentoReferencia != null)
				{
					notaS = AdmNotaS.CargaNotaSalidaDebitoVentas(Convert.ToInt32(venta.CodFacturaVenta));
				}
				txtNumDoc.Text = venta.CodFacturaVenta;
				CodNotaS = Convert.ToInt32(venta.CodFacturaVenta);
				CodTransaccion = venta.CodTipoTransaccion;
				CargaTransaccion();
				CodCliente = notaS.CodCliente;
				CargaCliente();
				dtpFecha.Value = venta.FechaSalida;
				cmbMoneda.SelectedValue = venta.Moneda;
				txtTipoCambio.Text = venta.TipoCambio.ToString();
				cmbMotivo.SelectedItem = venta.Motivo.ToString();
				txtComentario.Text = venta.Comentario.ToString();
				if (txtDocRef.Enabled)
				{
					CodDocumento = venta.CodTipoDocumento;
					txtDocRef.Text = notaS.SiglaDocumento + " " + notaS.Serie + "-" + notaS.NumDoc;
				}
				txtBruto.Text = $"{venta.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{venta.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{venta.Total - venta.Igv:#,##0.00}";
				txtIGV.Text = $"{venta.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{venta.Total:#,##0.00}";
				DetalleFacturaVenta();
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

	private void DetalleFacturaVenta()
	{
		dgvDetalle.DataSource = AdmVenta.CargaDetalleVentaCredito(CodNotaS, frmLogin.iCodAlmacen);
		dgvDetalle.Columns["stockdisponible"].Visible = false;
		dgvDetalle.Columns["maxPorcDescto"].Visible = false;
	}

	private void Bloqueabotones()
	{
		btnNuevo.Visible = false;
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
	}

	private void sololectura(bool estado)
	{
		txtTransaccion.ReadOnly = estado;
		dtpFecha.Enabled = !estado;
		cmbMoneda.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		txtCodCliente.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtDocRef.Enabled = !estado;
		txtComentario.ReadOnly = estado;
		txtComentario.Enabled = !estado;
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
		clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDecimal(fila.Cells[valorventa.Name].Value);
		deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
		deta.MontoDescuento = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.CantidadPendiente = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		deta.CodDetalleCotizacion = 0;
		detalle1.Add(deta);
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvDetalle.Focused)
			{
				pro = AdmPro.CargaProducto(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen);
				decimal cantidad1 = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
				decimal precio = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
				decimal bruto = cantidad1 * precio;
				decimal dsc1 = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[dscto1.Name].Value);
				decimal dsc2 = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[dscto2.Name].Value);
				decimal dsc3 = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[dscto3.Name].Value);
				decimal precioventa1 = bruto * (1m - dsc1 / 100m) * (1m - dsc2 / 100m) * (1m - dsc3 / 100m);
				decimal montodescuento = bruto - precioventa1;
				decimal valorventa1;
				if (pro.ConIgv)
				{
					decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa1 = precioventa1 / factorigv;
				}
				else
				{
					valorventa1 = precioventa1;
				}
				decimal igv1 = precioventa1 - valorventa1;
				dgvDetalle.CurrentRow.Cells[importe.Name].Value = bruto;
				dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = montodescuento;
				dgvDetalle.CurrentRow.Cells[valorventa.Name].Value = valorventa1;
				dgvDetalle.CurrentRow.Cells[igv.Name].Value = igv1;
				dgvDetalle.CurrentRow.Cells[precioventa.Name].Value = precioventa1;
				CalculaTotales();
				btnGuardar.Enabled = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0)
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			ser = AdmSerie.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
			ReportDocument rd = new ReportDocument();
			rd.Load("CRNotaDebitoVenta.rpt");
			CRNotaDebitoVenta rpt = new CRNotaDebitoVenta();
			rd.SetDataSource(ds.ReportNotaDebitoVenta(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen));
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
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Nota Débito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnNuevaGuia_Click(object sender, EventArgs e)
	{
		frmNotadeDebito form2 = new frmNotadeDebito();
		form2.MdiParent = base.MdiParent;
		form2.Proceso = 1;
		form2.Show();
		Close();
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == 11)
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
		if (e.KeyChar == '\r' && txtDocRefe.Text != "")
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

	private void txtDocRefe_Leave(object sender, EventArgs e)
	{
		BuscaTipoDocumento();
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
		}
		if (e.KeyChar != '\r')
		{
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

	private void txtCodCliente_Leave(object sender, EventArgs e)
	{
		if (CodCliente == 0)
		{
			txtCodCliente.Focus();
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1)
		{
			CalculaTotales();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotadeDebito));
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
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnNuevaGuia = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.txtCodDocumento = new System.Windows.Forms.TextBox();
		this.label37 = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtDocRefe = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbMotivo = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtDireccionCliente = new System.Windows.Forms.TextBox();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.cbValorVenta = new System.Windows.Forms.CheckBox();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
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
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
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
		this.groupBox2.Location = new System.Drawing.Point(0, 190);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(963, 248);
		this.groupBox2.TabIndex = 23;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(75, 199);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(846, 222);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(753, 225);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(846, 196);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(753, 199);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(247, 199);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(613, 196);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(520, 199);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(176, 202);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(31, 202);
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
		this.dgvDetalle.Size = new System.Drawing.Size(954, 161);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
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
		dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle41.Format = "N2";
		dataGridViewCellStyle41.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle41;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle42.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle42;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle43.Format = "N2";
		dataGridViewCellStyle43.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle43;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle44.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle44;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle45.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle45;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle46.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle46;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle47.Format = "N2";
		dataGridViewCellStyle47.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle47;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle48.Format = "N2";
		dataGridViewCellStyle48.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle48;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle49.Format = "N2";
		dataGridViewCellStyle49.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle49;
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
		dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle50.Format = "N2";
		dataGridViewCellStyle50.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle50;
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
		this.groupBox3.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox3.Controls.Add(this.btnNuevaGuia);
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 438);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(963, 48);
		this.groupBox3.TabIndex = 22;
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
		this.btnSalir.Location = new System.Drawing.Point(889, 10);
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
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(806, 10);
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
		this.groupBox1.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.txtCodDocumento);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtDocRefe);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.cmbMotivo);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtDireccionCliente);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.cbValorVenta);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.btnDetalle);
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
		this.groupBox1.Size = new System.Drawing.Size(963, 190);
		this.groupBox1.TabIndex = 21;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.txtCodCliente.Location = new System.Drawing.Point(102, 44);
		this.txtCodCliente.MaxLength = 11;
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(88, 20);
		this.txtCodCliente.TabIndex = 507;
		this.txtCodCliente.Tag = "1";
		this.txtCodCliente.Visible = false;
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.txtCodDocumento.Location = new System.Drawing.Point(559, 16);
		this.txtCodDocumento.Name = "txtCodDocumento";
		this.txtCodDocumento.Size = new System.Drawing.Size(39, 20);
		this.txtCodDocumento.TabIndex = 506;
		this.txtCodDocumento.Visible = false;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(473, 20);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(8, 12);
		this.label37.TabIndex = 505;
		this.label37.Text = "-";
		this.txtNumero.Enabled = false;
		this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumero.Location = new System.Drawing.Point(484, 18);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(69, 18);
		this.txtNumero.TabIndex = 503;
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.Location = new System.Drawing.Point(433, 18);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(38, 18);
		this.txtSerie.TabIndex = 502;
		this.txtSerie.Tag = "13";
		this.txtSerie.Visible = false;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.txtDocRefe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRefe.Enabled = false;
		this.txtDocRefe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRefe.Location = new System.Drawing.Point(399, 18);
		this.txtDocRefe.Name = "txtDocRefe";
		this.txtDocRefe.ReadOnly = true;
		this.txtDocRefe.Size = new System.Drawing.Size(28, 18);
		this.txtDocRefe.TabIndex = 501;
		this.txtDocRefe.Tag = "10";
		this.txtDocRefe.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRefe_KeyDown);
		this.txtDocRefe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRefe_KeyPress);
		this.txtDocRefe.Leave += new System.EventHandler(txtDocRefe_Leave);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(343, 21);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(45, 12);
		this.label6.TabIndex = 504;
		this.label6.Text = "Doc. Ref.";
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(102, 157);
		this.txtComentario.MaxLength = 500;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(451, 20);
		this.txtComentario.TabIndex = 493;
		this.txtComentario.Tag = "10";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(18, 105);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 13);
		this.label3.TabIndex = 492;
		this.label3.Tag = "21";
		this.label3.Text = "Motivo";
		this.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMotivo.FormattingEnabled = true;
		this.cmbMotivo.Items.AddRange(new object[3] { "FACTURADO DE MENOS", "POR EL COBOR DE INTERES", "FLETE U OTROS" });
		this.cmbMotivo.Location = new System.Drawing.Point(102, 99);
		this.cmbMotivo.Name = "cmbMotivo";
		this.cmbMotivo.Size = new System.Drawing.Size(242, 21);
		this.cmbMotivo.TabIndex = 3;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 78);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(55, 13);
		this.label4.TabIndex = 90;
		this.label4.Text = "Dirección:";
		this.txtDireccionCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccionCliente.Enabled = false;
		this.txtDireccionCliente.Location = new System.Drawing.Point(102, 71);
		this.txtDireccionCliente.Name = "txtDireccionCliente";
		this.txtDireccionCliente.ReadOnly = true;
		this.txtDireccionCliente.Size = new System.Drawing.Size(451, 20);
		this.txtDireccionCliente.TabIndex = 2;
		this.txtDireccionCliente.Tag = "21";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(196, 44);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(357, 20);
		this.txtNombreCliente.TabIndex = 1;
		this.txtNombreCliente.Tag = "2";
		this.txtNombreCliente.Visible = false;
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
		this.cbValorVenta.Location = new System.Drawing.Point(870, 99);
		this.cbValorVenta.Name = "cbValorVenta";
		this.cbValorVenta.Size = new System.Drawing.Size(81, 17);
		this.cbValorVenta.TabIndex = 15;
		this.cbValorVenta.Text = "Valor Venta";
		this.cbValorVenta.UseVisualStyleBackColor = true;
		this.cbValorVenta.Visible = false;
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(870, 71);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 11;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(824, 44);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(127, 21);
		this.cmbMoneda.TabIndex = 10;
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(790, 74);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 21;
		this.label16.Text = "Tipo/Cambio :";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(766, 47);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(52, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Moneda :";
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Enabled = false;
		this.btnDetalle.Location = new System.Drawing.Point(882, 152);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(75, 23);
		this.btnDetalle.TabIndex = 4;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Visible = false;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(17, 162);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(34, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Glosa";
		this.label9.Visible = false;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Location = new System.Drawing.Point(688, 18);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 20);
		this.txtNumDoc.TabIndex = 6;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(618, 21);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(102, 131);
		this.txtDocRef.MaxLength = 15;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(88, 20);
		this.txtDocRef.TabIndex = 4;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtDocRef.Leave += new System.EventHandler(txtDocRef_Leave);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(16, 135);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.label5.Visible = false;
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(136, 16);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(186, 20);
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
		this.txtTransaccion.TabIndex = 5;
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
		this.dtpFecha.Location = new System.Drawing.Point(870, 18);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 9;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(821, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(963, 486);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmNotadeDebito";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "NotadeDebito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotadeDebito_Load);
		base.Shown += new System.EventHandler(frmNotadeDebito_Shown);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
