using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace SIGEFA.Formularios;

public class frmNotadeCreditoCompra : Office2007Form
{
	private ClsNotasCreditoDebitoCompra ds = new ClsNotasCreditoDebitoCompra();

	private clsNotaCredito nota_credito = new clsNotaCredito();

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

	private clsAdmNotaCreditoCompra AdmNotaCompra = new clsAdmNotaCreditoCompra();

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

	public List<clsDetalleNotaSalida> detalleS = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaCredito> detalleNC = new List<clsDetalleNotaCredito>();

	public string CodNota;

	public int CodNotaS;

	public int CodNotaSD;

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

	private clsFactura facturSD = new clsFactura();

	public int CodFactura;

	public int CodFacturaSD;

	private clsAdmFactura AdmCompra = new clsAdmFactura();

	private DataTable dt1 = new DataTable();

	public DataTable dataOficial = new DataTable();

	private int cantprod = 0;

	private decimal precprod = default(decimal);

	private TextBox txtedit = new TextBox();

	private List<int> cantpr = new List<int>();

	private List<decimal> cantprec = new List<decimal>();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private bool bandRestringirEdicion = false;

	private bool bandRembolsoFlete = false;

	public DataTable dataSeleccionada = null;

	private Color fondo_celda;

	internal int CodNotaCC;

	internal bool generacion = false;

	internal frmGuiaRemisionCompra ventana = null;

	internal string codGuiaRemisionCompra = "";

	private bool tieneFlete = false;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Label label2;

	private Label label5;

	private Label lbNombreTransaccion;

	private TextBox txtNumDoc;

	private Label label7;

	private Button btnListadoProductos;

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

	private CheckBox cbAplicada;

	public TextBox txtNS;

	private Label label6;

	public TextBox txtFactAplicar;

	private Label label8;

	private CheckBox cbProductoDestruido;

	private TextBox txtSerieGRSP;

	private TextBox txtNumGRSP;

	private Label label17;

	private DateTimePicker dtpfechaemision;

	private Label label4;

	private TextBox txtComentario;

	private Label label9;

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

	private DataGridViewTextBoxColumn colTipoDetalle;

	public frmNotadeCreditoCompra()
	{
		InitializeComponent();
	}

	private void frmNotaIngreso_Load(object sender, EventArgs e)
	{
		try
		{
			CargaMoneda();
			if (!generacion)
			{
				CargaMotivos();
			}
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			txtComentario.Visible = true;
			if (!generacion)
			{
				inicializaDataOficial();
			}
			switch (Proceso)
			{
			case 1:
				Bloqueabotones();
				if (generacion)
				{
					if (!bandRembolsoFlete && tieneFlete)
					{
						DataRow fila = dataOficial.NewRow();
						clsProducto rem_flete = AdmPro.CargaProducto(6930, frmLogin.iCodAlmacen);
						fila.SetField(codproducto.DataPropertyName, (object)rem_flete.CodProducto);
						fila.SetField(referencia.DataPropertyName, (object)rem_flete.Referencia);
						fila.SetField(descripcion.DataPropertyName, (object)rem_flete.Descripcion);
						fila.SetField(codunidad.DataPropertyName, (object)rem_flete.CodUnidadMedida);
						fila.SetField(moneda.DataPropertyName, cmbMoneda.SelectedValue);
						fila.SetField(unidad.DataPropertyName, (object)"UNIDAD");
						fila.SetField(cantidad.DataPropertyName, 1.0);
						fila.SetField(preciounit.DataPropertyName, 0.0);
						fila.SetField(importe.DataPropertyName, 0.0);
						fila.SetField(dscto1.DataPropertyName, 0.0);
						fila.SetField(dscto2.DataPropertyName, 0.0);
						fila.SetField(dscto3.DataPropertyName, 0.0);
						fila.SetField(montodscto.DataPropertyName, 0.0);
						fila.SetField(valorventa.DataPropertyName, 0.0);
						fila.SetField(igv.DataPropertyName, 0.0);
						fila.SetField(flete.DataPropertyName, 0.0);
						fila.SetField(precioventa.DataPropertyName, 0.0);
						fila.SetField(precioreal.DataPropertyName, 0.0);
						fila.SetField(valoreal.DataPropertyName, 0.0);
						fila.SetField(colTipoDetalle.DataPropertyName, (object)2);
						dataOficial.Rows.Add(fila);
					}
					dgvDetalle.DataSource = dataOficial;
				}
				break;
			case 2:
				cargaNotaCreditoCompra();
				DesactivarControles();
				dataOficial = AdmNotaCompra.cargaDetalleNCC(nota_credito.CodNotaCredito);
				dgvDetalle.DataSource = dataOficial;
				break;
			case 3:
				bandRestringirEdicion = true;
				CargaNotaSalida();
				sololectura(estado: true);
				break;
			default:
				MessageBox.Show("Se ah definido un proceso no especificado.", "Error de Proceso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void cargarDatosDeGeneracion(int _codProveedor, int _codFactura)
	{
		CodTransaccion = 19;
		CargaTransaccion();
		CodProveedor = _codProveedor;
		CargaProveedor();
		cbProductoDestruido.Visible = false;
		CargaMotivos();
		cmbMotivo.SelectedValue = 7;
		CodFactura = _codFactura;
		CargaFacturaGrid();
	}

	private void DesactivarControles()
	{
		foreach (Control item in base.Controls)
		{
			if (!(item.GetType() == typeof(GroupBox)))
			{
				continue;
			}
			foreach (Control item_2 in ((GroupBox)item).Controls)
			{
				item_2.Enabled = false;
			}
		}
		if (nota_credito.Estado == 1)
		{
			label7.Enabled = true;
			txtSerie.Enabled = true;
			txtNumDoc.Enabled = true;
			btnGuardar.Enabled = true;
			btnSalir.Enabled = true;
			btnImprimir.Visible = false;
			btnNuevaGuia.Visible = false;
		}
	}

	private void cargaNotaCreditoCompra()
	{
		nota_credito = AdmNotaCompra.cargaNotaCredito(CodNotaCC);
		if (nota_credito != null)
		{
			CodTransaccion = nota_credito.CodTipoTransaccion;
			CargaTransaccion();
			CodProveedor = nota_credito.CodTipoDocumento;
			CargaProveedor();
			txtFactAplicar.Text = nota_credito.NumFac;
			cbProductoDestruido.Checked = nota_credito.ProductoDestruido;
			if (!nota_credito.ProductoDestruido)
			{
				txtSerieGRSP.Text = nota_credito.serieGRSP;
				txtNumGRSP.Text = nota_credito.numdocGRSP;
			}
			dtpfechaemision.Value = nota_credito.FechaEmision;
			dtpFecha.Value = nota_credito.FechaRegistro;
			cmbMoneda.SelectedValue = nota_credito.Moneda;
			txtTipoCambio.Text = nota_credito.TipoCambio.ToString();
			txtComentario.Text = nota_credito.Comentario;
			cmbMotivo.SelectedValue = nota_credito.Motivo;
			txtNS.Text = nota_credito.codNotaSalida;
			txtSerie.Text = nota_credito.Serie;
			txtNumDoc.Text = nota_credito.DocumentoNotaCredito;
		}
		else
		{
			MessageBox.Show("Error al cargar nota de credito de compra");
		}
	}

	private void inicializaDataOficial()
	{
		foreach (DataGridViewColumn col in dgvDetalle.Columns)
		{
			dataOficial.Columns.Add(col.DataPropertyName);
		}
	}

	private void CargaMotivos()
	{
		cmbMotivo.DataSource = AdmNotaCompra.cargaTipoNCC();
		cmbMotivo.DisplayMember = "descripcion";
		cmbMotivo.ValueMember = "codTipoNotaCredito";
		cmbMotivo.SelectedIndex = -1;
	}

	private void frmNotaIngreso_Shown(object sender, EventArgs e)
	{
		txtTransaccion.Focus();
		txtTransaccion.Text = "NCC";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, ee);
		ser = AdmSerie.BuscaSeriexDocumento(6, frmLogin.iCodAlmacen);
		cmbMotivo.Focus();
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
		label3.Visible = true;
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
			label3.Visible = true;
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
		txtDocRef.Enabled = !estado;
		cbAplicada.Enabled = !estado;
		txtSerie.Enabled = !estado;
		txtNumDoc.Enabled = !estado;
		txtCodProveedor.Enabled = !estado;
		txtNS.Enabled = !estado;
		txtFactAplicar.Enabled = !estado;
		dgvDetalle.Enabled = !estado;
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

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmListaFacturasPorProveedor"] != null)
			{
				Application.OpenForms["frmListaFacturasPorProveedor"].Activate();
			}
			else if (CodNotaS != 0)
			{
				frmListaFacturasPorProveedor form = new frmListaFacturasPorProveedor();
				form.CodProveedor = CodProveedor;
				form.tipo = 3;
				form.ShowDialog();
				facturSD = form.factura;
				CodFacturaSD = Convert.ToInt32(facturSD.CodFactura);
				txtDocRef.Text = facturSD.DocumentoFactura;
				txtFactAplicar.Focus();
			}
			else
			{
				txtNS.Focus();
			}
		}
	}

	private void CargaFacturaGrid()
	{
		try
		{
			factur = AdmFactura.CargaFactura(CodFactura);
			if (factur != null)
			{
				txtFactAplicar.Text = factur.DocumentoFactura;
				txtTipoCambio.Text = factur.TipoCambio.ToString();
				cmbMoneda.SelectedValue = factur.Moneda;
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

	private DataTable CargaDetalleFactura()
	{
		dt1.Clear();
		dt1 = AdmFactura.CargaDetalle(Convert.ToInt32(factur.CodFactura));
		tieneFlete = AdmFactura.verificaFleteEnFactura(Convert.ToInt32(factur.CodFactura));
		btnEliminar.Visible = false;
		if (dgvDetalle.Rows.Count > 0)
		{
			dt1 = getDataRestanteDeFactura();
		}
		return dt1;
	}

	private DataTable getDataRestanteDeFactura()
	{
		DataTable aux = AdmFactura.CargaDetalle(Convert.ToInt32(factur.CodFactura));
		foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToInt32(fila.Cells[colTipoDetalle.Name].Value) == 1)
			{
				List<DataRow> filadata = (from x in aux.AsEnumerable()
					where x.Field<object>(referencia.DataPropertyName).ToString() == fila.Cells[referencia.Name].Value.ToString() && x.Field<object>(preciounit.DataPropertyName).ToString() == fila.Cells[preciounit.Name].Value.ToString()
					select x).ToList();
				if (filadata.Count > 0)
				{
					DataRow nueva = filadata[0];
					aux.Rows.Remove(nueva);
				}
			}
		}
		return aux;
	}

	private void CargaNotaSalida()
	{
		try
		{
			notaS = AdmNotaS.CargaNotaSalidaCredito(CodNotaS);
			if (notaS != null)
			{
				txtDocRef.Text = notaS.Docref;
				txtNS.Text = notaS.DocumentoNotaSalida;
				txtFactAplicar.Text = notaS.DocumentoFacturaAplicada;
				txtSerie.Text = notaS.Serie;
				txtNumDoc.Text = notaS.NumDoc;
				cmbMoneda.SelectedValue = notaS.Moneda;
				txtTipoCambio.Text = notaS.TipoCambio.ToString();
				dtpFecha.Value = notaS.FechaSalida;
				txtCodProveedor.Text = notaS.RUCCliente;
				txtNombreProveedor.Text = notaS.RazonSocialCliente;
				cmbMotivo.SelectedItem = notaS.Motivo;
				txtComentario.Text = notaS.Comentario;
				cbAplicada.Checked = Convert.ToBoolean(notaS.Aplicada);
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

	private void CargaNotaIngresoGrid()
	{
		try
		{
			nota = AdmNota.CargaNotaIngreso(CodNotaI);
			if (nota != null)
			{
				txtDocRef.Text = nota.SiglaDocumento + " - " + nota.NumDoc;
				if (txtCodProveedor.Enabled)
				{
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

	private void BorrarNota()
	{
		try
		{
			CodNotaS = 0;
			notaS = new clsNotaSalida();
			txtDocRef.Text = "";
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalleNota()
	{
		dataOficial = AdmNotaS.CargaDetalle(Convert.ToInt32(notaS.CodNotaSalida));
		dgvDetalle.DataSource = dataOficial;
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

	private void txtAutorizacion_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnListadoProductos.Enabled = true;
		}
	}

	private void txtTransaccion_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			txtTransaccion.Focus();
		}
		else if (CodTransaccion == 7)
		{
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
		try
		{
			if (Proceso == 0 || !verificarGuardado())
			{
				return;
			}
			if (dgvDetalle.Rows.Count > 0)
			{
				if (Proceso == 1)
				{
					nota_credito.CodAlmacen = ((factur.CodAlmacen == 0) ? frmLogin.iCodAlmacen : factur.CodAlmacen);
					nota_credito.CodTipoTransaccion = tran.CodTransaccion;
					nota_credito.CodTipoDocumento = 4;
					nota_credito.DocumentoNotaCredito = txtNumDoc.Text;
					nota_credito.NumFac = txtFactAplicar.Text;
					nota_credito.FechaIngreso = dtpFecha.Value.Date;
					nota_credito.Cancelado = 0;
					nota_credito.Comentario = txtComentario.Text;
					nota_credito.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					nota_credito.MontoBruto = Convert.ToDouble(txtValorVenta.Text);
					nota_credito.MontoDscto = Convert.ToDouble(txtDscto.Text);
					nota_credito.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
					nota_credito.Igv = Convert.ToDouble(txtIGV.Text);
					nota_credito.Total = Convert.ToDouble(txtPrecioVenta.Text);
					if (txtSerie.Text != "" && txtNumDoc.Text != "")
					{
						nota_credito.Estado = 2;
					}
					else
					{
						nota_credito.Estado = 1;
					}
					nota_credito.CodUser = frmLogin.iCodUser;
					nota_credito.Serie = txtSerie.Text;
					nota_credito.CodReferencia = Convert.ToInt32(factur.CodFactura);
					nota_credito.CodProveedor = CodProveedor;
					nota_credito.Motivo = cmbMotivo.SelectedValue.ToString();
					nota_credito.FechaRegistro = dtpFecha.Value;
					nota_credito.FechaEmision = dtpfechaemision.Value;
					nota_credito.ProductoDestruido = cbProductoDestruido.Checked;
					nota_credito.serieGRSP = txtSerieGRSP.Text;
					nota_credito.numdocGRSP = txtNumGRSP.Text;
					notaS.CodAlmacen = frmLogin.iCodAlmacen;
					notaS.CodSucursal = frmLogin.iCodSucursal;
					notaS.CodTipoTransaccion = tran.CodTransaccion;
					notaS.CodTipoDocumento = 4;
					if (txtSerie.Text != "" && txtNumDoc.Text != "")
					{
						notaS.Serie = txtSerie.Text;
						notaS.NumDoc = txtNumDoc.Text.PadLeft(8, '0');
					}
					else if (nota_credito.ProductoDestruido)
					{
						notaS.Serie = "SIN";
						notaS.NumDoc = "Especif.";
					}
					else
					{
						notaS.Serie = txtSerieGRSP.Text.PadLeft(4, '0');
						notaS.NumDoc = txtNumGRSP.Text.PadLeft(4, '0');
					}
					notaS.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					if (txtTipoCambio.Visible)
					{
						notaS.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
					}
					notaS.FechaSalida = dtpFecha.Value.Date;
					notaS.FormaPago = 0;
					notaS.Comentario = txtComentario.Text;
					notaS.Motivo = cmbMotivo.SelectedValue.ToString();
					notaS.MontoBruto = Convert.ToDouble(txtBruto.Text);
					notaS.MontoDscto = Convert.ToDouble(txtDscto.Text);
					notaS.Igv = Convert.ToDouble(txtIGV.Text);
					notaS.Total = Convert.ToDouble(txtPrecioVenta.Text);
					notaS.CodUser = frmLogin.iCodUser;
					notaS.Estado = 1;
					notaS.CodProveedor = CodProveedor;
				}
				bool rpta = true;
				using (TransactionScope Scope = new TransactionScope())
				{
					if (Proceso == 1)
					{
						if (notaS.Total == 0.0)
						{
							MessageBox.Show("Ingrese valor correctamente!", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							Transaction.Current.Rollback();
							Scope.Dispose();
							dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
							return;
						}
						if (AdmNotaCompra.insert(nota_credito))
						{
							RecorreDetalleNC();
							if (detalleNC.Count > 0)
							{
								foreach (clsDetalleNotaCredito detNC in detalleNC)
								{
									detNC.CodNotaCredito = nota_credito.CodNotaCreditoNueva;
									rpta = AdmNotaCompra.insertdetalle(detNC);
								}
							}
							RecorreDetalleS();
							if (detalleS.Count > 0)
							{
								int seleccionado = ((cmbMotivo.SelectedItem != null) ? Convert.ToInt32(cmbMotivo.SelectedValue) : 0);
								int valor = AdmNotaCompra.getAccionSegunTipoSeleccionado(seleccionado);
								if (valor == 1)
								{
									notaS.DocumentoReferencia = nota_credito.CodNotaCreditoNueva;
									if (AdmNotaS.insert(notaS))
									{
										foreach (clsDetalleNotaSalida detS in detalleS)
										{
											detS.CodNotaSalida = Convert.ToInt32(notaS.CodNotaSalida);
											rpta = AdmNotaS.insertdetalle(detS);
										}
										AdmNotaCompra.setCodNotaSalida(notaS.CodNotaSalida, nota_credito.CodNotaCreditoNueva);
									}
									else
									{
										rpta = false;
									}
								}
							}
						}
						else
						{
							rpta = false;
						}
						if (rpta)
						{
							dgvDetalle.Enabled = false;
							MessageBox.Show("Los datos se guardaron correctamente!", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							if (generacion)
							{
								clsAdmGuiaRemisionCompra admgrc = new clsAdmGuiaRemisionCompra();
								clsGuiaRemisionCompraDocumentoRelacionado nuevo = new clsGuiaRemisionCompraDocumentoRelacionado();
								nuevo.Anulado = 0;
								nuevo.CodDocumentoRelacionado = nota_credito.CodNotaCreditoNueva;
								nuevo.CodGuiaRemisionCompra = Convert.ToInt32(codGuiaRemisionCompra);
								nuevo.CodTipoDocumento = nota_credito.CodTipoDocumento;
								nuevo.TipoGRCDR = 3;
								admgrc.insertarDocumentoRelacionado(nuevo);
							}
						}
						else
						{
							MessageBox.Show("Ocurrio un problema al registrar la nota de credito de compra", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else if (Proceso == 2)
					{
						if (txtSerie.Text != "" && txtNumDoc.Text != "")
						{
							AdmNotaCompra.actualizaSerieyCorrelativo(nota_credito.CodNotaCredito, txtSerie.Text, txtNumDoc.Text);
							AdmNotaCompra.actualizaAsignador(nota_credito.CodNotaCredito, frmLogin.iCodUser, DateTime.Now);
							AdmNotaCompra.actualizaEstado(nota_credito.CodNotaCredito, 2);
							btnGuardar.Enabled = false;
							MessageBox.Show("Asignacion de Documento Guardado", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							MessageBox.Show("Por favor ingrese Serie y Correlativo del Documento", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							txtSerie.Focus();
						}
					}
					if (!rpta)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
					}
					else
					{
						Scope.Complete();
						Scope.Dispose();
					}
				}
				if (generacion)
				{
					if (ventana != null)
					{
						ventana.mostrarMensajeGuardado = false;
						ventana.btnGuardar.PerformClick();
					}
					Close();
				}
			}
			else
			{
				MessageBox.Show("La Tabla no contiene información!", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void RecorreDetalleNC()
	{
		detalleNC.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleNC(row);
		}
	}

	private bool verificarGuardado()
	{
		bool band = true;
		string mensaje = "";
		if (Proceso == 1)
		{
			if (cmbMotivo.SelectedIndex == -1)
			{
				mensaje = "Seleccione un motivo de nota de credito";
				band = false;
			}
			else if (factur == null)
			{
				mensaje = "Seleccione una factura a aplicar";
				band = false;
			}
			else if (txtSerieGRSP.Visible && txtSerieGRSP.Text == "")
			{
				mensaje = "Indique la serie de guia de remision de salida de producto";
				band = false;
			}
			else if (txtNumGRSP.Visible && txtNumGRSP.Text == "")
			{
				mensaje = "Indique el numero de documento de guia de remision de salida de producto";
				band = false;
			}
			else if (txtComentario.Text == "")
			{
				mensaje = "Indicar un comentario es obligatorio";
				band = false;
			}
		}
		else if (Proceso == 3)
		{
			if (txtSerie.Text == "")
			{
				mensaje = "Registre la Serie de la Nota de Credito";
				band = false;
			}
			else if (txtNumDoc.Text == "")
			{
				mensaje = "Registre el numero de documento de la Nota de Credito";
				band = false;
			}
		}
		if (!band)
		{
			MessageBox.Show(mensaje, "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return band;
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

	private void RecorreDetalleS()
	{
		detalleS.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			int tipoDetalle = Convert.ToInt32(row.Cells[colTipoDetalle.Name].Value);
			if (tipoDetalle == 1)
			{
				añadedetalleS(row);
			}
		}
	}

	private void añadedetalleS(DataGridViewRow fila)
	{
		clsDetalleNotaSalida detaS = new clsDetalleNotaSalida();
		detaS.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detaS.CodNotaSalida = Convert.ToInt32(notaS.CodNotaSalida);
		detaS.CodAlmacen = frmLogin.iCodAlmacen;
		detaS.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detaS.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		detaS.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		detaS.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detaS.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detaS.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detaS.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detaS.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detaS.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detaS.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		detaS.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		detaS.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		detaS.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		detaS.FechaSalida = dtpFecha.Value;
		detaS.CodUser = frmLogin.iCodUser;
		detalleS.Add(detaS);
	}

	private void añadedetalleNC(DataGridViewRow fila)
	{
		clsDetalleNotaCredito detaNC = new clsDetalleNotaCredito();
		detaNC.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detaNC.Descripcion = fila.Cells[descripcion.Name].Value.ToString();
		detaNC.CodNotaSalida = Convert.ToInt32(notaS.CodNotaSalida);
		detaNC.CodAlmacen = frmLogin.iCodAlmacen;
		detaNC.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detaNC.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		detaNC.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		detaNC.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detaNC.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detaNC.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detaNC.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detaNC.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detaNC.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detaNC.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		detaNC.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		detaNC.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		detaNC.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		detaNC.FechaRegistro = dtpFecha.Value;
		detaNC.CodUser = frmLogin.iCodUser;
		detaNC.TipoDetalle = Convert.ToInt32(fila.Cells[colTipoDetalle.Name].Value);
		detaNC.valorCompra = Convert.ToDouble(fila.Cells[valorventa.Name].Value);
		detalleNC.Add(detaNC);
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
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0)
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
			recordarValoresDeFilaDGV();
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
		form.ventana3 = this;
		form.Proceso = 3;
		form.Procede = 6;
		form.ShowDialog();
		dataOficial.Clear();
		if (CodProveedor != 0)
		{
			CargaProveedor();
			txtSerie.Focus();
		}
		else
		{
			BorrarProveedor();
		}
		bandRembolsoFlete = false;
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

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		if (cli != null)
		{
			txtCodProveedor.Text = cli.CodigoPersonalizado;
			txtNombreProveedor.Text = cli.RazonSocial;
		}
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && txtCodProveedor.Text != "")
		{
			if (BuscaProveedor())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("El proveedor no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
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

	private bool BuscaCliente()
	{
		cli = AdmCli.BuscaCliente(txtCodProveedor.Text, Tipo);
		if (cli != null)
		{
			txtCodProveedor.Text = cli.CodigoPersonalizado;
			txtNombreProveedor.Text = cli.RazonSocial;
			CodCliente = cli.CodCliente;
			return true;
		}
		txtNombreProveedor.Text = "";
		CodCliente = 0;
		return false;
	}

	private void txtCodProveedor_Leave(object sender, EventArgs e)
	{
		if (bandRestringirEdicion && CodProveedor == 0)
		{
			txtCodProveedor.Focus();
		}
	}

	private void dgvDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvDetalle.Focused)
			{
				calcularTotalesDeFilaDGv(dgvDetalle.Rows[e.RowIndex]);
				CalculaTotales();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void calcularTotalesDeFilaDGv(DataGridViewRow fila)
	{
		bool bandVVenta = Convert.ToDecimal(fila.Cells[importe.Name].Value) - Convert.ToDecimal(fila.Cells[valorventa.Name].Value) < 0.1m;
		if (fila.Cells[colTipoDetalle.Name].Value.ToString() == "2")
		{
			bandVVenta = cbValorVenta.Checked;
		}
		pro = AdmPro.CargaProducto(Convert.ToInt32(fila.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen);
		double cantidad1 = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		double precio = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		double bruto = cantidad1 * precio;
		double dsc1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		double dsc2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		double dsc3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
		double valorventa1;
		double montodescuento;
		double precioventa1;
		if (bandVVenta)
		{
			valorventa1 = bruto * (1.0 - dsc1 / 100.0) * (1.0 - dsc2 / 100.0) * (1.0 - dsc3 / 100.0);
			montodescuento = bruto - valorventa1;
			decimal aux = (decimal)valorventa1 * (decimal)factorigv;
			precioventa1 = Math.Round(Convert.ToDouble(aux), 3);
		}
		else
		{
			precioventa1 = bruto * (1.0 - dsc1 / 100.0) * (1.0 - dsc2 / 100.0) * (1.0 - dsc3 / 100.0);
			montodescuento = bruto - precioventa1;
			decimal aux2 = (decimal)precioventa1 / (decimal)factorigv;
			valorventa1 = Math.Round(Convert.ToDouble(aux2), 3);
		}
		decimal aux_igv = (decimal)precioventa1 - (decimal)valorventa1;
		double igv1 = Math.Round(Convert.ToDouble(aux_igv), 3);
		fila.Cells[importe.Name].Value = bruto;
		fila.Cells[montodscto.Name].Value = montodescuento;
		fila.Cells[valorventa.Name].Value = valorventa1;
		fila.Cells[igv.Name].Value = igv1;
		fila.Cells[precioventa.Name].Value = precioventa1;
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			ser = AdmSerie.MuestraSerie(notaS.CodSerie, frmLogin.iCodAlmacen);
			ReportDocument rd = new ReportDocument();
			rd.Load("CRNotaCredito.rpt");
			CRNotaCredito rpt = new CRNotaCredito();
			rd.SetDataSource(ds.ReportNotaCreditoCompra(Convert.ToInt32(CodNotaS), frmLogin.iCodAlmacen));
			PrintOptions rptoption = rd.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(530, 2900, 70, 500));
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
		frmNotadeCreditoCompra form2 = new frmNotadeCreditoCompra();
		form2.MdiParent = base.MdiParent;
		form2.Proceso = 1;
		form2.Show();
		Close();
	}

	private void txtSerie_Leave(object sender, EventArgs e)
	{
		if (txtSerie.Text != "")
		{
			txtNumDoc.Focus();
		}
		else if (bandRestringirEdicion)
		{
			txtSerie.Focus();
		}
	}

	private void txtNumDoc_Leave(object sender, EventArgs e)
	{
		if (txtNumDoc.Text != "")
		{
			txtNumDoc.Text = Convert.ToInt32(txtNumDoc.Text).ToString().PadLeft(8, '0');
		}
		else if (bandRestringirEdicion)
		{
			txtNumDoc.Focus();
		}
	}

	private void cmbMotivo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		switch (cmbMotivo.SelectedIndex)
		{
		}
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.Rows.Count <= 0)
		{
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

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == 10 || dgvDetalle.CurrentCell.ColumnIndex == 11)
		{
			ok.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle_Leave(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void txtNS_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmListaNotaSalidaNDC"] != null)
		{
			Application.OpenForms["frmListaNotaSalidaNDC"].Activate();
		}
		else if (CodProveedor != 0)
		{
			frmListaNotaSalidaNDC form = new frmListaNotaSalidaNDC();
			form.CodProveedor = CodProveedor;
			form.ShowDialog();
			if (form.CodNotaS != 0)
			{
				CodNotaS = form.CodNotaS;
				txtNS.Text = form.documento;
				CargaNotaSalidaNC();
				txtDocRef.Focus();
			}
		}
		else
		{
			txtCodProveedor.Focus();
		}
	}

	private void CargaNotaSalidaNC()
	{
		try
		{
			notaS = AdmNotaS.CargaNotaSalida(CodNotaS);
			if (notaS != null)
			{
				cmbMoneda.SelectedValue = notaS.Moneda;
				CargaDetalleNotaNC();
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

	private void CargaDetalleNotaNC()
	{
		dataOficial.Clear();
		if (cmbMotivo.SelectedIndex < 2)
		{
			dataOficial = AdmNotaS.CargaDetalleNotaSalidaNDC(CodNotaS, frmLogin.iCodAlmacen);
			dgvDetalle.DataSource = dataOficial;
			dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = true;
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = true;
			dgvDetalle.Columns["stockdisponible"].Visible = false;
			dgvDetalle.Columns["maxPorcDescto"].Visible = false;
			btnEliminar.Visible = false;
			recordarValoresDeFilaDGV();
		}
		CalculaTotales();
	}

	private void recordarValoresDeFilaDGV()
	{
		cantpr = new List<int>();
		cantprec = new List<decimal>();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			cantpr.Add(Convert.ToInt32(row.Cells[cantidad.Name].Value));
			cantprec.Add(Convert.ToDecimal(row.Cells[preciounit.Name].Value));
		}
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtFactAplicar_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (cmbMotivo.SelectedIndex <= -1)
		{
			MessageBox.Show("Seleccione un motivo de nota de credito de compra", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			cmbMotivo.Focus();
		}
		else if (Application.OpenForms["frmListaFacturasPorProveedor"] != null)
		{
			Application.OpenForms["frmListaFacturasPorProveedor"].Activate();
		}
		else if (CodProveedor != 0)
		{
			frmListaFacturasPorProveedor form = new frmListaFacturasPorProveedor();
			form.CodProveedor = CodProveedor;
			form.tipo = 1;
			form.ShowDialog();
			if (form.factura != null && form.factura.CodFactura != 0)
			{
				factur = form.factura;
				CodFactura = Convert.ToInt32(factur.CodFactura);
			}
			if (CodFactura != 0)
			{
				if (!AdmNotaCompra.verificarCodFacturaTieneNotaCredito(CodFactura))
				{
					AccionAlSeleccionarMotivo();
					txtComentario.Focus();
				}
				else
				{
					MessageBox.Show("Esta Factura ya tiene una Nota de Credito Activa", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtFactAplicar.Text = "";
					CodFactura = 0;
				}
				bandRembolsoFlete = false;
			}
		}
		else
		{
			txtCodProveedor.Focus();
		}
	}

	private void AccionAlSeleccionarMotivo()
	{
		int seleccionado = ((cmbMotivo.SelectedItem != null) ? Convert.ToInt32(cmbMotivo.SelectedValue) : 0);
		switch (AdmNotaCompra.getAccionSegunTipoSeleccionado(seleccionado))
		{
		case 1:
			btnListadoProductos.Visible = true;
			CargaFacturaGrid();
			btnListadoProductos.PerformClick();
			break;
		case 2:
		{
			CargaFacturaGrid();
			DataRow fila = dataOficial.NewRow();
			fila.SetField(codproducto.DataPropertyName, (object)0);
			fila.SetField(referencia.DataPropertyName, (object)0);
			fila.SetField(descripcion.DataPropertyName, (object)"Indique descripcion...");
			fila.SetField(codunidad.DataPropertyName, (object)0);
			fila.SetField(moneda.DataPropertyName, (object)0);
			fila.SetField(unidad.DataPropertyName, (object)"");
			fila.SetField(cantidad.DataPropertyName, 1.0);
			fila.SetField(preciounit.DataPropertyName, 0.0);
			fila.SetField(importe.DataPropertyName, 0.0);
			fila.SetField(dscto1.DataPropertyName, 0.0);
			fila.SetField(dscto2.DataPropertyName, 0.0);
			fila.SetField(dscto3.DataPropertyName, 0.0);
			fila.SetField(montodscto.DataPropertyName, 0.0);
			fila.SetField(valorventa.DataPropertyName, 0.0);
			fila.SetField(igv.DataPropertyName, 0.0);
			fila.SetField(flete.DataPropertyName, 0.0);
			fila.SetField(precioventa.DataPropertyName, 0.0);
			fila.SetField(precioreal.DataPropertyName, 0.0);
			fila.SetField(valoreal.DataPropertyName, 0.0);
			fila.SetField(colTipoDetalle.DataPropertyName, (object)2);
			dataOficial.Rows.Add(fila);
			dgvDetalle.DataSource = dataOficial;
			foreach (DataGridViewRow fila_dgv in (IEnumerable)dgvDetalle.Rows)
			{
				calcularTotalesDeFilaDGv(fila_dgv);
			}
			CalculaTotales();
			break;
		}
		default:
			if (seleccionado == 0)
			{
				MessageBox.Show("Seleccione un motivo de nota de credito de compra", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cmbMotivo.Focus();
			}
			else
			{
				MessageBox.Show("No se ah especificado una accion para este tipo de nota de credito seleccionado", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			break;
		}
	}

	private void btnAñadirMotivo_Click(object sender, EventArgs e)
	{
	}

	private void btnSalidaProductos_Click(object sender, EventArgs e)
	{
	}

	private void cbProductoDestruido_CheckedChanged(object sender, EventArgs e)
	{
		label17.Visible = !cbProductoDestruido.Checked;
		txtSerieGRSP.Visible = !cbProductoDestruido.Checked;
		txtNumGRSP.Visible = !cbProductoDestruido.Checked;
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void btnListadoProductos_Click(object sender, EventArgs e)
	{
		try
		{
			frmListadoProductosFacturaNotaCredito form = new frmListadoProductosFacturaNotaCredito();
			form.dataProductos = CargaDetalleFactura();
			form.ventana1 = this;
			form.ShowDialog();
			if (dataSeleccionada == null)
			{
				return;
			}
			foreach (DataRow fila_dt in dataSeleccionada.Rows)
			{
				DataRow fila = dataOficial.NewRow();
				foreach (DataColumn col_dt in dataSeleccionada.Columns)
				{
					string dataPName = col_dt.ColumnName;
					string nameColumn = obtenerNombreColumna(dataPName);
					if (!(nameColumn != ""))
					{
						continue;
					}
					foreach (DataColumn col_dt2 in dataOficial.Columns)
					{
						if (col_dt2.ColumnName == dataPName)
						{
							fila.SetField(dataPName, fila_dt.Field<object>(dataPName));
							break;
						}
					}
				}
				fila.SetField(colTipoDetalle.DataPropertyName, 1);
				dataOficial.Rows.Add(fila);
			}
			if (!bandRembolsoFlete && tieneFlete)
			{
				DataRow fila2 = dataOficial.NewRow();
				clsProducto rem_flete = AdmPro.CargaProducto(6930, frmLogin.iCodAlmacen);
				fila2.SetField(codproducto.DataPropertyName, (object)rem_flete.CodProducto);
				fila2.SetField(referencia.DataPropertyName, (object)rem_flete.Referencia);
				fila2.SetField(descripcion.DataPropertyName, (object)rem_flete.Descripcion);
				fila2.SetField(codunidad.DataPropertyName, (object)rem_flete.CodUnidadMedida);
				fila2.SetField(moneda.DataPropertyName, cmbMoneda.SelectedValue);
				fila2.SetField(unidad.DataPropertyName, (object)"UNIDAD");
				fila2.SetField(cantidad.DataPropertyName, 1.0);
				fila2.SetField(preciounit.DataPropertyName, 0.0);
				fila2.SetField(importe.DataPropertyName, 0.0);
				fila2.SetField(dscto1.DataPropertyName, 0.0);
				fila2.SetField(dscto2.DataPropertyName, 0.0);
				fila2.SetField(dscto3.DataPropertyName, 0.0);
				fila2.SetField(montodscto.DataPropertyName, 0.0);
				fila2.SetField(valorventa.DataPropertyName, 0.0);
				fila2.SetField(igv.DataPropertyName, 0.0);
				fila2.SetField(flete.DataPropertyName, 0.0);
				fila2.SetField(precioventa.DataPropertyName, 0.0);
				fila2.SetField(precioreal.DataPropertyName, 0.0);
				fila2.SetField(valoreal.DataPropertyName, 0.0);
				fila2.SetField(colTipoDetalle.DataPropertyName, (object)2);
				dataOficial.Rows.Add(fila2);
				bandRembolsoFlete = true;
			}
			dgvDetalle.DataSource = dataOficial;
			foreach (DataGridViewRow fila3 in (IEnumerable)dgvDetalle.Rows)
			{
				calcularTotalesDeFilaDGv(fila3);
			}
			CalculaTotales();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ocurrio un error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private string obtenerNombreColumna(string dataPName)
	{
		foreach (DataGridViewColumn col in dgvDetalle.Columns)
		{
			if (col.DataPropertyName == dataPName)
			{
				return col.Name;
			}
		}
		return "";
	}

	private void dgvDetalle_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		switch (Convert.ToInt32(dgvDetalle.Rows[e.RowIndex].Cells[colTipoDetalle.Name].Value))
		{
		case 1:
			fondo_celda = dgvDetalle.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
			if ((e.ColumnIndex == dgvDetalle.Columns[cantidad.Name].Index || e.ColumnIndex == dgvDetalle.Columns[preciounit.Name].Index) && tieneFlete && dgvDetalle.Rows[e.RowIndex].Cells[codproducto.Name].Value.ToString() == "6930")
			{
				dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(0, 192, 0);
				dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
			}
			break;
		case 2:
			fondo_celda = dgvDetalle.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
			if (e.ColumnIndex == dgvDetalle.Columns[descripcion.Name].Index || e.ColumnIndex == dgvDetalle.Columns[preciounit.Name].Index)
			{
				dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(0, 192, 0);
				dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
			}
			break;
		default:
			dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
			break;
		}
	}

	private void dgvDetalle_CellLeave(object sender, DataGridViewCellEventArgs e)
	{
		_ = fondo_celda;
		if (false)
		{
			fondo_celda = dgvDetalle.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
		}
		dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = fondo_celda;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotadeCreditoCompra));
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
		this.label9 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.dtpfechaemision = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.cbProductoDestruido = new System.Windows.Forms.CheckBox();
		this.txtSerieGRSP = new System.Windows.Forms.TextBox();
		this.txtNumGRSP = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtFactAplicar = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtNS = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.cbAplicada = new System.Windows.Forms.CheckBox();
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
		this.btnListadoProductos = new System.Windows.Forms.Button();
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
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
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
		this.colTipoDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.dtpfechaemision);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.cbProductoDestruido);
		this.groupBox1.Controls.Add(this.txtSerieGRSP);
		this.groupBox1.Controls.Add(this.txtNumGRSP);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtFactAplicar);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtNS);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.cbAplicada);
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
		this.groupBox1.Controls.Add(this.btnListadoProductos);
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
		this.groupBox1.Size = new System.Drawing.Size(1094, 185);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(14, 128);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(60, 13);
		this.label9.TabIndex = 24;
		this.label9.Text = "Comentario";
		this.txtComentario.Location = new System.Drawing.Point(128, 125);
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(484, 20);
		this.txtComentario.TabIndex = 24;
		this.dtpfechaemision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfechaemision.Location = new System.Drawing.Point(531, 45);
		this.dtpfechaemision.Name = "dtpfechaemision";
		this.dtpfechaemision.Size = new System.Drawing.Size(81, 20);
		this.dtpfechaemision.TabIndex = 501;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(439, 47);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(79, 13);
		this.label4.TabIndex = 500;
		this.label4.Text = "Fecha Emision:";
		this.cbProductoDestruido.AutoSize = true;
		this.cbProductoDestruido.Location = new System.Drawing.Point(491, 99);
		this.cbProductoDestruido.Name = "cbProductoDestruido";
		this.cbProductoDestruido.Size = new System.Drawing.Size(117, 17);
		this.cbProductoDestruido.TabIndex = 3;
		this.cbProductoDestruido.Text = "Producto Destruido";
		this.cbProductoDestruido.UseVisualStyleBackColor = true;
		this.cbProductoDestruido.CheckedChanged += new System.EventHandler(cbProductoDestruido_CheckedChanged);
		this.txtSerieGRSP.BackColor = System.Drawing.SystemColors.Window;
		this.txtSerieGRSP.Location = new System.Drawing.Point(388, 98);
		this.txtSerieGRSP.MaxLength = 4;
		this.txtSerieGRSP.Name = "txtSerieGRSP";
		this.txtSerieGRSP.Size = new System.Drawing.Size(35, 20);
		this.txtSerieGRSP.TabIndex = 4;
		this.txtNumGRSP.BackColor = System.Drawing.SystemColors.Window;
		this.txtNumGRSP.Location = new System.Drawing.Point(426, 98);
		this.txtNumGRSP.MaxLength = 6;
		this.txtNumGRSP.Name = "txtNumGRSP";
		this.txtNumGRSP.Size = new System.Drawing.Size(59, 20);
		this.txtNumGRSP.TabIndex = 5;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(242, 100);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(140, 13);
		this.label17.TabIndex = 499;
		this.label17.Text = "Num. GR. Salida Productos:";
		this.txtFactAplicar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFactAplicar.Location = new System.Drawing.Point(128, 97);
		this.txtFactAplicar.MaxLength = 20;
		this.txtFactAplicar.Name = "txtFactAplicar";
		this.txtFactAplicar.ReadOnly = true;
		this.txtFactAplicar.Size = new System.Drawing.Size(100, 20);
		this.txtFactAplicar.TabIndex = 6;
		this.txtFactAplicar.Tag = "10";
		this.txtFactAplicar.Visible = false;
		this.txtFactAplicar.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFactAplicar_KeyDown);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(14, 101);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(109, 13);
		this.label8.TabIndex = 498;
		this.label8.Tag = "10";
		this.label8.Text = "Factura Relacionada:";
		this.label8.Visible = false;
		this.txtNS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNS.Location = new System.Drawing.Point(524, 151);
		this.txtNS.MaxLength = 20;
		this.txtNS.Name = "txtNS";
		this.txtNS.ReadOnly = true;
		this.txtNS.Size = new System.Drawing.Size(88, 20);
		this.txtNS.TabIndex = 5;
		this.txtNS.Tag = "10";
		this.txtNS.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNS_KeyDown);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(456, 155);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(62, 13);
		this.label6.TabIndex = 496;
		this.label6.Tag = "10";
		this.label6.Text = "Nota Salida";
		this.cbAplicada.AutoSize = true;
		this.cbAplicada.Checked = true;
		this.cbAplicada.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbAplicada.Location = new System.Drawing.Point(618, 127);
		this.cbAplicada.Name = "cbAplicada";
		this.cbAplicada.Size = new System.Drawing.Size(67, 17);
		this.cbAplicada.TabIndex = 494;
		this.cbAplicada.Text = "Aplicada";
		this.cbAplicada.UseVisualStyleBackColor = true;
		this.cbAplicada.Visible = false;
		this.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMotivo.FormattingEnabled = true;
		this.cmbMotivo.Location = new System.Drawing.Point(128, 153);
		this.cmbMotivo.Name = "cmbMotivo";
		this.cmbMotivo.Size = new System.Drawing.Size(241, 21);
		this.cmbMotivo.TabIndex = 2;
		this.cmbMotivo.SelectionChangeCommitted += new System.EventHandler(cmbMotivo_SelectionChangeCommitted);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(14, 156);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 13);
		this.label3.TabIndex = 490;
		this.label3.Tag = "210";
		this.label3.Text = "Motivo";
		this.txtSerie.BackColor = System.Drawing.SystemColors.Window;
		this.txtSerie.Location = new System.Drawing.Point(128, 44);
		this.txtSerie.MaxLength = 6;
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.Size = new System.Drawing.Size(43, 20);
		this.txtSerie.TabIndex = 48;
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.txtNombreProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreProveedor.Enabled = false;
		this.txtNombreProveedor.Location = new System.Drawing.Point(234, 71);
		this.txtNombreProveedor.Name = "txtNombreProveedor";
		this.txtNombreProveedor.ReadOnly = true;
		this.txtNombreProveedor.Size = new System.Drawing.Size(378, 20);
		this.txtNombreProveedor.TabIndex = 4;
		this.txtNombreProveedor.Tag = "9";
		this.txtNombreProveedor.Visible = false;
		this.txtCodProveedor.Location = new System.Drawing.Point(128, 71);
		this.txtCodProveedor.MaxLength = 11;
		this.txtCodProveedor.Name = "txtCodProveedor";
		this.txtCodProveedor.Size = new System.Drawing.Size(100, 20);
		this.txtCodProveedor.TabIndex = 1;
		this.txtCodProveedor.Tag = "8";
		this.txtCodProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtCodProveedor.Visible = false;
		this.txtCodProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodProveedor.Leave += new System.EventHandler(txtCodProveedor_Leave);
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(14, 74);
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
		this.cbValorVenta.Location = new System.Drawing.Point(1001, 99);
		this.cbValorVenta.Name = "cbValorVenta";
		this.cbValorVenta.Size = new System.Drawing.Size(81, 17);
		this.cbValorVenta.TabIndex = 15;
		this.cbValorVenta.Text = "Valor Venta";
		this.cbValorVenta.UseVisualStyleBackColor = true;
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(1001, 71);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 6;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(954, 44);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(128, 21);
		this.cmbMoneda.TabIndex = 5;
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(921, 74);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 21;
		this.label16.Text = "Tipo/Cambio :";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(896, 47);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(52, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Moneda :";
		this.btnListadoProductos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnListadoProductos.Location = new System.Drawing.Point(954, 122);
		this.btnListadoProductos.Name = "btnListadoProductos";
		this.btnListadoProductos.Size = new System.Drawing.Size(128, 23);
		this.btnListadoProductos.TabIndex = 18;
		this.btnListadoProductos.Text = "Listado De Productos";
		this.btnListadoProductos.UseVisualStyleBackColor = true;
		this.btnListadoProductos.Visible = false;
		this.btnListadoProductos.Click += new System.EventHandler(btnListadoProductos_Click);
		this.txtNumDoc.BackColor = System.Drawing.SystemColors.Window;
		this.txtNumDoc.Location = new System.Drawing.Point(177, 45);
		this.txtNumDoc.MaxLength = 6;
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.Size = new System.Drawing.Size(68, 20);
		this.txtNumDoc.TabIndex = 2;
		this.txtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumDoc_KeyPress);
		this.txtNumDoc.Leave += new System.EventHandler(txtNumDoc_Leave);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(14, 47);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(705, 159);
		this.txtDocRef.MaxLength = 20;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(88, 20);
		this.txtDocRef.TabIndex = 6;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(647, 163);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.label5.Visible = false;
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(162, 21);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(252, 15);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.lbNombreTransaccion.Visible = false;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Location = new System.Drawing.Point(128, 18);
		this.txtTransaccion.MaxLength = 5;
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.Size = new System.Drawing.Size(28, 20);
		this.txtTransaccion.TabIndex = 1;
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(14, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(1001, 18);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 4;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(952, 21);
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
		this.groupBox3.Location = new System.Drawing.Point(0, 492);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1094, 48);
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
		this.btnSalir.Location = new System.Drawing.Point(1020, 10);
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
		this.btnGuardar.Location = new System.Drawing.Point(937, 10);
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
		this.groupBox2.Location = new System.Drawing.Point(0, 185);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1094, 307);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(476, 229);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(977, 281);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(884, 284);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(977, 255);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(884, 258);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(648, 229);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(977, 229);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(884, 232);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(577, 232);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(432, 232);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.moneda, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.flete, this.precioventa, this.precioreal, this.valoreal, this.colTipoDetalle);
		this.dgvDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1085, 207);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvDetalle_CellBeginEdit);
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEnter);
		this.dgvDetalle.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellLeave);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.dgvDetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvDetalle_KeyPress);
		this.dgvDetalle.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvDetalle_KeyUp);
		this.dgvDetalle.Leave += new System.EventHandler(dgvDetalle_Leave);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 90;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 250;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 80;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		dataGridViewCellStyle1.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
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
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N2";
		dataGridViewCellStyle8.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle8;
		this.valorventa.HeaderText = "V. Compra";
		this.valorventa.Name = "valorventa";
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle9;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
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
		this.precioventa.HeaderText = "P. Compra";
		this.precioventa.Name = "precioventa";
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.DataPropertyName = "precioreal";
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.valoreal.DataPropertyName = "valoreal";
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.colTipoDetalle.DataPropertyName = "tipoDetalle";
		this.colTipoDetalle.HeaderText = "ProductooDinero";
		this.colTipoDetalle.Name = "colTipoDetalle";
		this.colTipoDetalle.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1094, 540);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmNotadeCreditoCompra";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
