using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmNotaIngreso : Office2007Form
{
	private clsFactura fac = new clsFactura();

	private clsAdmFactura AdmFact = new clsAdmFactura();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	public clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsProveedor prov = new clsProveedor();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsNotaIngreso nota = new clsNotaIngreso();

	private clsAdmAutorizado AdmAut = new clsAdmAutorizado();

	private clsAdmUsuario AdmUsu = new clsAdmUsuario();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie AdmSerie = new clsAdmSerie();

	private clsAutorizado aut = new clsAutorizado();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsValidar ok = new clsValidar();

	private clsDetalleNotaIngreso detaSelec = new clsDetalleNotaIngreso();

	private clsAdmProducto Admpro = new clsAdmProducto();

	private clsProducto prod = new clsProducto();

	private clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();

	private clsOrdenCompra Orde = new clsOrdenCompra();

	private clsAdmNotaIngresoIgv AdmNotaIngresoIgv = new clsAdmNotaIngresoIgv();

	private clsNotaIngresoIgv notaIngresoIgv = new clsNotaIngresoIgv();

	private decimal Qnueva = default(decimal);

	private decimal QIngresado = default(decimal);

	private decimal QPorAtender = default(decimal);

	public List<int> codProd = new List<int>();

	public List<int> config = new List<int>();

	public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();

	public List<clsDetalleFactura> detalleFactura = new List<clsDetalleFactura>();

	public string CodNota;

	public int CodTransaccion;

	public int codOrdenCompra_nota = 0;

	public int CodAlmacenOrden = 0;

	public int CodProveedor;

	public int CodCliente;

	public int CodDocumento;

	public int CodOrdenCompra;

	public int CodAutorizado;

	public int CodOrdenCompraNew = 0;

	private bool Validacion = true;

	private bool EsCompraDirecta = true;

	public int Proceso = 0;

	public int Tipo;

	private int proce = 0;

	private TextBox txtedit = new TextBox();

	private clsValidar val = new clsValidar();

	public DataTable data = new DataTable();

	private decimal total;

	private decimal montoflete;

	internal int CodGuiaRemisionCompra = 0;

	internal frmGuiaRemisionCompra ventana_grc = null;

	internal bool generadaGRC = false;

	internal bool generadaGRCparaFlete = false;

	private clsGuiaRemision grc = new clsGuiaRemision();

	private clsAdmGuiaRemisionCompra admgrc = new clsAdmGuiaRemisionCompra();

	internal int compra = 0;

	public clsGuiaRemision grc_anadir = null;

	private List<clsGuiaRemision> listadoGRC = new List<clsGuiaRemision>();

	private clsAdmOrdenCompra admOrdenCompra = new clsAdmOrdenCompra();

	public DataTable facturagenerada = null;

	public DataTable facturageneradaa_1 = null;

	private Color fondo_celda;

	private TextBox txtedit1 = new TextBox();

	private clsValidar ok1 = new clsValidar();

	private double cantidad_previa1 = double.NaN;

	private double valor_venta = double.NaN;

	private double valor_compra = double.NaN;

	public clsUsuario usuario_click = null;

	private DataTable copiafacturagenerada = new DataTable();

	public Dictionary<string, string> preciosGRC = new Dictionary<string, string>();

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Label label2;

	private Label label4;

	private Label label6;

	private Label label5;

	private TextBox txtNumDoc;

	private Label label7;

	private TextBox txtComentario;

	private Label label9;

	private TextBox txtOrdenCompra;

	private Label label8;

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

	private TextBox txtAutorizacion;

	private Label label3;

	private Label label16;

	private Label label15;

	private TextBox txtTipoCambio;

	public DataGridView dgvDetalle;

	public TextBox txtCodProv;

	public TextBox txtNombreProv;

	public TextBox txtDocRef;

	public TextBox txtTransaccion;

	private Label lbAutorizado;

	private CheckBox cbValorVenta;

	public TextBox txtNombreCliente;

	public TextBox txtCodCliente;

	private Label label18;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator7;

	private CustomValidator customValidator6;

	private CustomValidator customValidator5;

	private CustomValidator customValidator4;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private TextBox txtFlete;

	private Label label19;

	private DataGridViewTextBoxColumn flete;

	private ComboBox cmbFormaPago;

	private DateTimePicker dtpFechaPago;

	private Label label17;

	public ComboBox cmbMoneda;

	public DataGridView dgvDetalle2;

	private TextBox txtCodProveedor;

	private DataGridViewTextBoxColumn codetord;

	private DataGridViewTextBoxColumn coprod;

	private DataGridViewTextBoxColumn codref;

	private DataGridViewTextBoxColumn desc;

	private DataGridViewTextBoxColumn uni;

	private DataGridViewTextBoxColumn coduni;

	private DataGridViewTextBoxColumn cant;

	private DataGridViewTextBoxColumn stock;

	private DataGridViewTextBoxColumn cantn;

	private DataGridViewTextBoxColumn preciounitario;

	private DataGridViewTextBoxColumn subtotal;

	private DataGridViewTextBoxColumn descuento1;

	private DataGridViewTextBoxColumn descuento2;

	private DataGridViewTextBoxColumn descuento3;

	private DataGridViewTextBoxColumn montodscto1;

	private DataGridViewTextBoxColumn valorventa1;

	private DataGridViewTextBoxColumn igv1;

	private DataGridViewTextBoxColumn importe1;

	private DataGridViewTextBoxColumn precioreal1;

	private DataGridViewTextBoxColumn valoreal1;

	private DataGridViewTextBoxColumn flete1;

	private DataGridViewTextBoxColumn SaldoIngresado;

	private DataGridViewTextBoxColumn cantidadPendiente;

	private DataGridViewTextBoxColumn SaldoIngresado1;

	private DataGridViewTextBoxColumn cantidadPendiente1;

	private DataGridViewTextBoxColumn Bonificacion;

	private CustomValidator customValidator8;

	private CustomValidator customValidator9;

	private Label lblCantidadProductos;

	private Button btnVerificar;

	private BalloonTip btVerificar;

	public Label lbNombreTransaccion;

	public Button btnDetalle;

	public Button btnModificarFlete;

	public Button btnPreListadodeProductos;

	public Button btnAgregarGRC;

	private DataGridView dgvDocumentosRelacionados;

	private DataGridViewTextBoxColumn colItem;

	private DataGridViewTextBoxColumn colCodDoc;

	private DataGridViewTextBoxColumn colDocRelacionado;

	private DataGridViewTextBoxColumn colTipoDocRel;

	private DataGridViewTextBoxColumn colMonto;

	private Label lblmensaje;

	private Label lblresponsable;

	private Label lblareaencargado;

	public ComboBox cmbarea;

	private ComboBox cmbUsuario;

	public TextBox txtNDocRef;

	public TextBox txtSerieDocRef;

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

	private DataGridViewTextBoxColumn valorventaconflete;

	private DataGridViewTextBoxColumn flete0;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn pvconflete;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn fechaingreso;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn UltimoPrecioCompra;

	private DataGridViewTextBoxColumn EstadoDeLaOrden;

	private DataGridViewTextBoxColumn ProductoSolicitado;

	private DataGridViewTextBoxColumn cantpendiente;

	private DataGridViewTextBoxColumn codDetalleGRC;

	private DataGridViewTextBoxColumn stado;

	private DataGridViewTextBoxColumn totalinicial;

	public frmNotaIngreso()
	{
		InitializeComponent();
	}

	private void txtTransaccion_KeyDown(object sender, KeyEventArgs e)
	{
		if (txtTransaccion.ReadOnly)
		{
			return;
		}
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmTransacciones"] != null)
			{
				Application.OpenForms["frmTransacciones"].Activate();
			}
			else
			{
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
		}
		if (txtTransaccion.Text.Equals("IOC"))
		{
			bloqueaBotones2();
			proce = 1;
			txtDocRef.Focus();
			btnDetalle.Visible = true;
		}
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(0);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = -1;
	}

	public void MostrarOcultar(bool proceso)
	{
		lblmensaje.Visible = proceso;
		lblresponsable.Visible = proceso;
		lblareaencargado.Visible = proceso;
		cmbUsuario.Visible = proceso;
		cmbarea.Visible = proceso;
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
		btnVerificar.Visible = true;
		EsCompraDirecta = false;
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

	private void CargaProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtCodProv.Text = prov.Ruc;
		txtNombreProv.Text = prov.RazonSocial;
		txtCodProveedor.Text = prov.CodProveedor.ToString();
	}

	private void BorrarProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtCodProv.Text = "";
		txtNombreProv.Text = "";
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
			btnVerificar.Visible = true;
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

	private bool BuscaProveedor()
	{
		prov = AdmProv.BuscaProveedor(txtCodProv.Text);
		if (prov != null)
		{
			txtNombreProv.Text = prov.RazonSocial;
			CodProveedor = prov.CodProveedor;
			return true;
		}
		txtNombreProv.Text = "";
		CodProveedor = 0;
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

	private void bloqueaBotones2()
	{
		label15.Visible = false;
		label16.Visible = false;
		label17.Visible = false;
		label19.Visible = false;
		label14.Visible = false;
		label13.Visible = false;
		label12.Visible = false;
		label11.Visible = false;
		label10.Visible = false;
		cmbFormaPago.Visible = false;
		cmbMoneda.Visible = false;
		txtDscto.Visible = false;
		txtTipoCambio.Visible = false;
		cbValorVenta.Visible = false;
		txtValorVenta.Visible = false;
		txtPrecioVenta.Visible = false;
		txtBruto.Visible = false;
		txtFlete.Visible = false;
		txtIGV.Visible = false;
		dtpFechaPago.Visible = false;
		txtOrdenCompra.Focus();
		dgvDetalle.Visible = false;
		dgvDetalle2.Visible = true;
		btnNuevo.Enabled = false;
		btnEditar.Enabled = false;
		btnGuardar.Enabled = true;
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
		try
		{
			RecorreDetalle();
			if (proce == 1)
			{
				if (txtCodProveedor.Text != "")
				{
					if (Application.OpenForms["frmDetalleGuia"] != null)
					{
						Application.OpenForms["frmDetalleGuia"].Activate();
						return;
					}
					frmDetalleGuia form = new frmDetalleGuia();
					form.Procede = 10;
					form.Proceso = 1;
					form.codproveedor = Convert.ToInt32(txtCodProveedor.Text);
					form.ShowDialog();
				}
				else
				{
					MessageBox.Show("Ingrese Proveedor");
					txtCodProv.Focus();
				}
			}
			else if (txtTransaccion.Text.Equals("IN"))
			{
				if (Application.OpenForms["frmDetalleIngreso"] != null)
				{
					Application.OpenForms["frmDetalleIngreso"].Activate();
					return;
				}
				frmDetalleIngreso form2 = new frmDetalleIngreso();
				form2.Procede = 6;
				form2.Proceso = 1;
				form2.codproveedor = 0;
				form2.bvalorventa = cbValorVenta.Checked;
				form2.productoscargados = detalle;
				form2.ShowDialog();
				serielote.Visible = false;
				lblCantidadProductos.Text = "Productos Agregados :" + dgvDetalle.RowCount;
			}
			else if (txtCodProveedor.Text != "")
			{
				codProd.Clear();
				if (dgvDetalle.Rows.Count > 0)
				{
					foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
					{
						codProd.Add(Convert.ToInt32(row.Cells[codproducto.Name].Value));
					}
				}
				else
				{
					codProd.Add(0);
				}
				if (Application.OpenForms["frmDetalleIngreso"] != null)
				{
					Application.OpenForms["frmDetalleIngreso"].Activate();
					return;
				}
				frmDetalleIngreso form3 = new frmDetalleIngreso();
				form3.Procede = 6;
				form3.Proceso = 1;
				form3.codproveedor = Convert.ToInt32(txtCodProveedor.Text);
				form3.bvalorventa = cbValorVenta.Checked;
				form3.ShowDialog();
				serielote.Visible = false;
				lblCantidadProductos.Text = "Productos Agregados :" + dgvDetalle.RowCount;
			}
			else
			{
				MessageBox.Show("Ingrese Proveedor");
				txtCodProv.Focus();
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Error! Por favor ingrese un código correcto", "Selección de Productos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void recalculaFilaConPrecioUnitarioCambiado(int filaIndex)
	{
		clsProducto pro = Admpro.CargaProductoDetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen, 1, 0, 0);
		DataGridViewRow filaActualDGV = dgvDetalle.Rows[filaIndex];
		double txtcantidadxd = Convert.ToDouble(filaActualDGV.Cells[cantidad.Name].Value);
		double txtprecioxd = Convert.ToDouble(filaActualDGV.Cells[preciounit.Name].Value);
		object vdscto1 = filaActualDGV.Cells[dscto1.Name].Value;
		double dsc1 = ((vdscto1 != DBNull.Value && vdscto1.ToString() != "") ? Convert.ToDouble(vdscto1) : 0.0);
		object vdscto2 = filaActualDGV.Cells[dscto2.Name].Value;
		double dsc2 = ((vdscto2 != DBNull.Value && vdscto2 != null) ? Convert.ToDouble(vdscto2) : 0.0);
		object vdscto3 = filaActualDGV.Cells[dscto1.Name].Value;
		double dsc3 = ((vdscto3 != DBNull.Value && vdscto3.ToString() != "") ? Convert.ToDouble(vdscto3) : 0.0);
		double txtprecionetoxd = txtprecioxd * txtcantidadxd * (1.0 - dsc1 / 100.0) * (1.0 - dsc2 / 100.0) * (1.0 - dsc3 / 100.0);
		double bruto;
		double montodescuento;
		double precioventa;
		double precioreal;
		double valorreal;
		double igv;
		double valorventa;
		if (!cbValorVenta.Checked)
		{
			bruto = Math.Round(txtprecioxd * txtcantidadxd, 2, MidpointRounding.AwayFromZero);
			montodescuento = bruto - txtprecionetoxd;
			if (pro.TipoImpuesto == 1)
			{
				precioventa = txtprecionetoxd;
				double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
				valorventa = precioventa / factorigv;
			}
			else
			{
				valorventa = txtprecionetoxd;
				precioventa = valorventa;
			}
			precioreal = precioventa / txtcantidadxd;
			valorreal = valorventa / txtcantidadxd;
			igv = precioventa - valorventa;
		}
		else
		{
			bruto = txtprecioxd * txtcantidadxd;
			montodescuento = bruto - txtprecionetoxd;
			if (pro.TipoImpuesto == 1)
			{
				valorventa = txtprecionetoxd;
				double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
				precioventa = valorventa * factorigv;
			}
			else
			{
				valorventa = txtprecionetoxd;
				precioventa = valorventa;
			}
			valorventa = txtprecionetoxd;
			precioreal = precioventa / txtcantidadxd;
			valorreal = valorventa / txtcantidadxd;
			igv = precioventa - valorventa;
		}
		int iunidad = Convert.ToInt32(filaActualDGV.Cells[codunidad.Name].Value);
		string sunidad = filaActualDGV.Cells[unidad.Name].Value.ToString();
		object auxultpreciocompra = ((filaActualDGV.Cells[UltimoPrecioCompra.Name].Value == null) ? ((object)0) : filaActualDGV.Cells[UltimoPrecioCompra.Name].Value);
		double ultpricecompra = Convert.ToDouble((auxultpreciocompra.ToString() != "" && auxultpreciocompra != DBNull.Value) ? auxultpreciocompra : ((object)0));
		string _estado = Convert.ToString(filaActualDGV.Cells["stado"].Value.ToString());
		string scodDetalleGRC = "";
		if (generadaGRC)
		{
			scodDetalleGRC = filaActualDGV.Cells[codDetalleGRC.Name].Value.ToString();
		}
		dgvDetalle.Rows.RemoveAt(filaIndex);
		dgvDetalle.Rows.Insert(filaIndex, "", pro.CodProducto, pro.Referencia, pro.Descripcion, iunidad, cmbMoneda.SelectedValue, sunidad, "0", txtcantidadxd, txtprecioxd, bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, "0.00", igv, precioventa, precioventa, precioreal, valorreal, "", "", "", ultpricecompra, "", "", txtcantidadxd, scodDetalleGRC, _estado);
	}

	public void recalculaFilaConPrecioUnitarioCambiado1(int filaIndex)
	{
		clsProducto pro = Admpro.CargaProductoDetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen, 1, 0, 0);
		DataGridViewRow filaActualDGV = dgvDetalle.Rows[filaIndex];
		double valorventa = 0.0;
		double igv = 0.0;
		double txtprecioxd = 0.0;
		double precioreal = 0.0;
		double valorreal = 0.0;
		double txtcantidadxd = Convert.ToDouble(filaActualDGV.Cells[cantidad.Name].Value);
		double totalbase = Math.Round(Convert.ToDouble((filaActualDGV.Cells[totalinicial.Name].Value == null) ? ((object)0) : filaActualDGV.Cells[totalinicial.Name].Value), 4);
		double txtprecioventa = Convert.ToDouble(filaActualDGV.Cells[precioventa.Name].Value);
		object vdscto1 = filaActualDGV.Cells[dscto1.Name].Value;
		double dsc1 = ((vdscto1 != DBNull.Value && vdscto1.ToString() != "") ? Convert.ToDouble(vdscto1) : 0.0);
		object vdscto2 = filaActualDGV.Cells[dscto2.Name].Value;
		double dsc2 = ((vdscto2 != DBNull.Value && vdscto2 != null) ? Convert.ToDouble(vdscto2) : 0.0);
		object vdscto3 = filaActualDGV.Cells[dscto1.Name].Value;
		double dsc3 = ((vdscto3 != DBNull.Value && vdscto3.ToString() != "") ? Convert.ToDouble(vdscto3) : 0.0);
		double txtprecionetoxd = txtprecioxd * txtcantidadxd * (1.0 - dsc1 / 100.0) * (1.0 - dsc2 / 100.0) * (1.0 - dsc3 / 100.0);
		double bruto;
		double montodescuento;
		if (!cbValorVenta.Checked)
		{
			bruto = txtprecioventa;
			montodescuento = 0.0;
			if (pro.TipoImpuesto == 1)
			{
				txtprecioxd = txtprecioventa / txtcantidadxd;
				valorventa = txtprecioventa / 1.18;
			}
			precioreal = txtprecioventa / txtcantidadxd;
			valorreal = valorventa / txtcantidadxd;
			igv = valorventa * 0.18;
		}
		else
		{
			bruto = txtprecioxd * txtcantidadxd;
			montodescuento = 0.0;
			if (pro.TipoImpuesto == 1)
			{
				valorventa = txtprecionetoxd;
				double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
			}
			else
			{
				valorventa = txtprecionetoxd;
			}
			valorventa = txtprecionetoxd;
		}
		int iunidad = Convert.ToInt32(filaActualDGV.Cells[codunidad.Name].Value);
		string sunidad = filaActualDGV.Cells[unidad.Name].Value.ToString();
		object auxultpreciocompra = ((filaActualDGV.Cells[UltimoPrecioCompra.Name].Value == null) ? ((object)0) : filaActualDGV.Cells[UltimoPrecioCompra.Name].Value);
		double ultpricecompra = Convert.ToDouble((auxultpreciocompra.ToString() != "" && auxultpreciocompra != DBNull.Value) ? auxultpreciocompra : ((object)0));
		string _estado = Convert.ToString(filaActualDGV.Cells["stado"].Value.ToString());
		string scodDetalleGRC = "";
		if (generadaGRC)
		{
			scodDetalleGRC = filaActualDGV.Cells[codDetalleGRC.Name].Value.ToString();
		}
		dgvDetalle.Rows.RemoveAt(filaIndex);
		dgvDetalle.Rows.Insert(filaIndex, "", pro.CodProducto, pro.Referencia, pro.Descripcion, iunidad, cmbMoneda.SelectedValue, sunidad, "0", txtcantidadxd, txtprecioxd, bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, "0.00", igv, txtprecioventa, txtprecioventa, precioreal, valorreal, "", "", "", ultpricecompra, "", "", txtcantidadxd, scodDetalleGRC, _estado, totalbase);
	}

	public void recalculaFilaConPrecioUnitarioCambiado2(int filaIndex)
	{
		clsProducto pro = Admpro.CargaProductoDetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen, 1, 0, 0);
		DataGridViewRow filaActualDGV = dgvDetalle.Rows[filaIndex];
		double txtprecioventa = 0.0;
		double igv = 0.0;
		double txtprecioxd = 0.0;
		double precioreal = 0.0;
		double valorreal = 0.0;
		double txtcantidadxd = Convert.ToDouble(filaActualDGV.Cells[cantidad.Name].Value);
		double txtvalorventa = Convert.ToDouble(filaActualDGV.Cells[valorventa.Name].Value);
		object vdscto1 = filaActualDGV.Cells[dscto1.Name].Value;
		double dsc1 = ((vdscto1 != DBNull.Value && vdscto1.ToString() != "") ? Convert.ToDouble(vdscto1) : 0.0);
		object vdscto2 = filaActualDGV.Cells[dscto2.Name].Value;
		double dsc2 = ((vdscto2 != DBNull.Value && vdscto2 != null) ? Convert.ToDouble(vdscto2) : 0.0);
		object vdscto3 = filaActualDGV.Cells[dscto1.Name].Value;
		double dsc3 = ((vdscto3 != DBNull.Value && vdscto3.ToString() != "") ? Convert.ToDouble(vdscto3) : 0.0);
		double txtprecionetoxd = txtprecioxd * txtcantidadxd * (1.0 - dsc1 / 100.0) * (1.0 - dsc2 / 100.0) * (1.0 - dsc3 / 100.0);
		double bruto;
		double montodescuento;
		if (!cbValorVenta.Checked)
		{
			bruto = txtprecioventa;
			igv = txtvalorventa * 0.18;
			montodescuento = 0.0;
			if (pro.TipoImpuesto == 1)
			{
				txtprecioventa = txtvalorventa + igv;
				txtprecioxd = txtprecioventa / txtcantidadxd;
			}
			precioreal = txtprecioventa / txtcantidadxd;
		}
		else
		{
			bruto = txtprecioxd * txtcantidadxd;
			montodescuento = 0.0;
			if (pro.TipoImpuesto == 1)
			{
				txtvalorventa = txtprecionetoxd;
				double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
			}
			else
			{
				txtvalorventa = txtprecionetoxd;
			}
			txtvalorventa = txtprecionetoxd;
		}
		int iunidad = Convert.ToInt32(filaActualDGV.Cells[codunidad.Name].Value);
		string sunidad = filaActualDGV.Cells[unidad.Name].Value.ToString();
		object auxultpreciocompra = ((filaActualDGV.Cells[UltimoPrecioCompra.Name].Value == null) ? ((object)0) : filaActualDGV.Cells[UltimoPrecioCompra.Name].Value);
		double ultpricecompra = Convert.ToDouble((auxultpreciocompra.ToString() != "" && auxultpreciocompra != DBNull.Value) ? auxultpreciocompra : ((object)0));
		string _estado = Convert.ToString(filaActualDGV.Cells["stado"].Value.ToString());
		string scodDetalleGRC = "";
		if (generadaGRC)
		{
			scodDetalleGRC = filaActualDGV.Cells[codDetalleGRC.Name].Value.ToString();
		}
		dgvDetalle.Rows.RemoveAt(filaIndex);
		dgvDetalle.Rows.Insert(filaIndex, "", pro.CodProducto, pro.Referencia, pro.Descripcion, iunidad, cmbMoneda.SelectedValue, sunidad, "0", txtcantidadxd, txtprecioxd, bruto, dsc1, dsc2, dsc3, montodescuento, txtvalorventa, txtvalorventa, "0.00", igv, txtprecioventa, txtprecioventa, precioreal, valorreal, "", "", "", ultpricecompra, "", "", txtcantidadxd, scodDetalleGRC, _estado);
	}

	private void VerificarCabecera()
	{
		Validacion = true;
		if (CodTransaccion == 0)
		{
			Validacion = false;
		}
		if (txtCodProv.Visible && CodProveedor == 0)
		{
			Validacion = false;
		}
		if (txtOrdenCompra.Visible && CodOrdenCompra == 0)
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
			form.Procede = 6;
			form.Proceso = 1;
			form.bvalorventa = cbValorVenta.Checked;
			form.productoscargados = detalle;
			form.ShowDialog();
		}
		catch (Exception)
		{
		}
	}

	private void txtCodProv_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && txtCodProv.Text != "")
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

	private void frmNotaIngreso_Load(object sender, EventArgs e)
	{
		if (compra == 1)
		{
			btnModificarFlete.Visible = true;
		}
		cargaMoneda();
		CargaFormaPagos();
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		txtTipoCambio.Text = tc.Venta.ToString();
		if (Proceso == 1)
		{
			Bloqueabotones();
		}
		if (Proceso == 2)
		{
			CargaNotaIngreso();
			sololectura(estado: false);
		}
		else if (Proceso == 3)
		{
			CargaNotaIngreso();
			sololectura(estado: false);
		}
		else if (Proceso == 4)
		{
			CargaNotaIngreso();
			sololectura(estado: true);
		}
		else if (Proceso == 5)
		{
			CargaNotaIngreso();
			sololectura(estado: true);
		}
		else if (Proceso == 7)
		{
			txtOrdenCompra.Text = CodOrdenCompra.ToString();
			CargaOrden();
			txtOrdenCompra.Visible = true;
			label8.Visible = true;
			btnGuardar.Enabled = true;
			Proceso = 1;
			btnEditar.Visible = false;
			btnDetalle.Visible = false;
			BuscaTipoDocumento();
		}
		if (generadaGRC)
		{
			btnPreListadodeProductos.Visible = true;
			btnAgregarGRC.Visible = true;
			grc = admgrc.CargaGuiaRemision(CodGuiaRemisionCompra);
			CodProveedor = grc.CodProveedor;
			facturagenerada = admgrc.generaFacturaCompraDeGRC(Convert.ToInt32(grc.CodGuiaRemision), (grc.OpcionFlete != 2) ? grc.OpcionFlete : 0);
			facturageneradaa_1 = admgrc.generaFacturaCompraDeGRC_1(Convert.ToInt32(grc.CodGuiaRemision), (grc.OpcionFlete != 2) ? grc.OpcionFlete : 0);
			copiafacturagenerada = admgrc.generaFacturaCompraDeGRC(Convert.ToInt32(grc.CodGuiaRemision), (grc.OpcionFlete != 2) ? grc.OpcionFlete : 0);
			if (CodProveedor != 0)
			{
				CargaProveedor();
				ProcessTabKey(forward: true);
			}
			else
			{
				BorrarProveedor();
			}
			btnGuardar.Enabled = false;
		}
		else if (generadaGRCparaFlete)
		{
			btnAgregarGRC.Visible = true;
			grc = admgrc.CargaGuiaRemision(CodGuiaRemisionCompra);
			CodProveedor = grc.CodEmpresaTransporte;
			facturagenerada = admgrc.generaFleteDeGRC(Convert.ToInt32(grc.CodGuiaRemision));
			copiafacturagenerada = admgrc.generaFleteDeGRC(Convert.ToInt32(grc.CodGuiaRemision));
			CargaDetalleDeGRC(facturagenerada);
			if (CodProveedor != 0)
			{
				CargaProveedor();
				ProcessTabKey(forward: true);
			}
			else
			{
				BorrarProveedor();
			}
		}
		if (generadaGRC && grc.ICodOrdenCompra != 0)
		{
			Orde = AdmOrd.CargaOrdenCompra(grc.ICodOrdenCompra);
			cmbMoneda.SelectedValue = Orde.Moneda;
			cmbFormaPago.SelectedValue = Orde.FormaPago;
			cmbFormaPago_SelectionChangeCommitted(new object(), new EventArgs());
		}
		if (generadaGRC || generadaGRCparaFlete)
		{
			btnEliminar.Visible = false;
			listadoGRC.Add(grc);
		}
		lblCantidadProductos.Text = "Productos Agregados: " + dgvDetalle.RowCount;
		if (Proceso == 1)
		{
			habilitarEdicionDGV();
		}
		muestreoDeTablaRelacionadaAGRC();
	}

	private void muestreoDeTablaRelacionadaAGRC()
	{
		try
		{
			int valor = admgrc.getTipoFaturaRelacionadoAGRC(Convert.ToInt32(nota.CodNotaIngreso));
			switch (valor)
			{
			case 1:
			{
				dgvDocumentosRelacionados.Visible = true;
				dgvDocumentosRelacionados.Width = 175;
				dgvDocumentosRelacionados.Columns[colMonto.Name].Visible = false;
				DataTable dataDR = admgrc.getDatosDocRelacGrillaFacturaCompra(1, Convert.ToInt32(nota.CodNotaIngreso));
				dgvDocumentosRelacionados.DataSource = dataDR;
				break;
			}
			case 2:
			{
				dgvDocumentosRelacionados.Visible = true;
				DataTable dataDR2 = admgrc.getDatosDocRelacGrillaFacturaCompra(2, Convert.ToInt32(nota.CodNotaIngreso));
				dgvDocumentosRelacionados.DataSource = dataDR2;
				break;
			}
			default:
				if (valor == -1)
				{
					MessageBox.Show("Ocurrio un error");
				}
				break;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void conviertiendoDGVaData()
	{
		DataTable data = new DataTable();
		foreach (DataGridViewColumn col in dgvDetalle.Columns)
		{
			if (col.DataPropertyName != "")
			{
				data.Columns.Add(col.DataPropertyName);
			}
		}
	}

	private int obtenerCodProveedordeCliente(int codCliente)
	{
		return 0;
	}

	private void habilitarEdicionDGV()
	{
		try
		{
			foreach (DataGridViewColumn col in dgvDetalle.Columns)
			{
				col.ReadOnly = true;
				if (col.Name == preciounit.Name)
				{
					col.ReadOnly = false;
				}
				if (col.Name == valorventa.Name)
				{
					col.ReadOnly = false;
				}
				if (col.Name == precioventa.Name)
				{
					col.ReadOnly = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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

	private void dgvDetalle_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		fondo_celda = dgvDetalle.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
		if (e.ColumnIndex == dgvDetalle.Columns[preciounit.Name].Index && Proceso == 1)
		{
			dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(0, 192, 0);
		}
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedValue = 1;
	}

	private void sololectura(bool estado)
	{
		txtTransaccion.ReadOnly = estado;
		dtpFecha.Enabled = !estado;
		txtCodProv.ReadOnly = estado;
		cmbMoneda.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtNDocRef.ReadOnly = estado;
		txtOrdenCompra.ReadOnly = estado;
		txtComentario.ReadOnly = estado;
		txtAutorizacion.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		txtFlete.ReadOnly = estado;
		btnNuevo.Visible = !estado;
		btnEditar.Visible = !estado;
		btnEliminar.Visible = !estado;
		btnGuardar.Visible = !estado;
	}

	private void Bloqueabotones()
	{
		dgvDetalle.Columns[28].Visible = false;
		dgvDetalle.Columns[29].Visible = false;
	}

	private void CargaNotaIngreso()
	{
		try
		{
			nota = AdmNota.CargaNotaIngreso(Convert.ToInt32(CodNota));
			if (nota != null)
			{
				txtNumDoc.Text = nota.CodNotaIngreso;
				CodTransaccion = nota.CodTipoTransaccion;
				CargaTransaccion();
				if (txtCodProv.Enabled)
				{
					CodProveedor = nota.CodProveedor;
					txtCodProv.Text = nota.RUCProveedor;
					txtNombreProv.Text = nota.RazonSocialProveedor;
					BuscaProveedor();
				}
				dtpFecha.Value = nota.FechaIngreso;
				cmbMoneda.SelectedValue = nota.Moneda;
				txtTipoCambio.Text = nota.TipoCambio.ToString();
				txtTipoCambio.Visible = true;
				label16.Visible = true;
				if (txtAutorizacion.Enabled)
				{
				}
				if (txtDocRef.Enabled)
				{
					CodDocumento = nota.CodTipoDocumento;
					txtDocRef.Text = nota.SiglaDocumento;
					string serie = nota.NumDoc.Substring(0, 3);
					string nro = nota.NumDoc.Substring(4, 8);
					txtSerieDocRef.Text = serie;
					txtNDocRef.Text = nro;
					BuscaTipoDocumento();
				}
				if (txtOrdenCompra.Enabled)
				{
				}
				cmbFormaPago.SelectedValue = nota.FormaPago;
				dtpFechaPago.Value = nota.FechaPago;
				txtComentario.Text = nota.Comentario;
				txtBruto.Text = $"{nota.MontoBruto:#,##0.0000}";
				txtDscto.Text = $"{nota.MontoDscto:#,##0.0000}";
				txtFlete.Text = $"{nota.Flete:#,##0.0000}";
				txtValorVenta.Text = $"{nota.Total - nota.Igv:#,##0.0000}";
				txtIGV.Text = $"{nota.Igv:#,##0.0000}";
				txtPrecioVenta.Text = $"{nota.Total:#,##0.0000}";
				if (CodTransaccion == 14)
				{
					bloqueaBotones2();
					bloquearBotonesOIC();
				}
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

	private void bloquearBotonesOIC()
	{
		dgvDetalle2.Visible = false;
		dgvDetalle.Visible = true;
		txtOrdenCompra.Visible = false;
		label8.Visible = false;
		serielote.Visible = false;
		preciounit.Visible = false;
		importe.Visible = false;
		montodscto.Visible = false;
		valorventa.Visible = false;
		igv.Visible = false;
		flete.Visible = false;
		precioventa.Visible = false;
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmNota.CargaDetalleSinEstado(Convert.ToInt32(nota.CodNotaIngreso));
		RecorreDetalle();
		nota.Detalle = detalle;
		if (tran.CodTransaccion == 15)
		{
			dgvDetalle.Columns["serielote"].Visible = false;
			dgvDetalle.Columns["preciounit"].Visible = false;
			dgvDetalle.Columns["importe"].Visible = false;
			dgvDetalle.Columns["montodscto"].Visible = false;
			dgvDetalle.Columns["valorventa"].Visible = false;
			dgvDetalle.Columns["igv"].Visible = false;
			dgvDetalle.Columns["flete0"].Visible = false;
			dgvDetalle.Columns["precioventa"].Visible = false;
			label15.Visible = false;
			label16.Visible = false;
			txtTipoCambio.Visible = false;
			cmbMoneda.Visible = false;
			cbValorVenta.Visible = false;
			label10.Visible = false;
			txtBruto.Visible = false;
			label11.Visible = false;
			txtDscto.Visible = false;
			label19.Visible = false;
			txtFlete.Visible = false;
			label12.Visible = false;
			label13.Visible = false;
			label14.Visible = false;
			txtValorVenta.Visible = false;
			txtIGV.Visible = false;
			txtPrecioVenta.Visible = false;
		}
	}

	private void CargaDetalleDeGRC(DataTable data)
	{
		asignarDataADGV(data);
		RecorreDetalle();
		nota.Detalle = detalle;
		if (tran.CodTransaccion == 15)
		{
			dgvDetalle.Columns["serielote"].Visible = false;
			dgvDetalle.Columns["preciounit"].Visible = false;
			dgvDetalle.Columns["importe"].Visible = false;
			dgvDetalle.Columns["montodscto"].Visible = false;
			dgvDetalle.Columns["valorventa"].Visible = false;
			dgvDetalle.Columns["igv"].Visible = false;
			dgvDetalle.Columns["flete"].Visible = false;
			dgvDetalle.Columns["precioventa"].Visible = false;
			label15.Visible = false;
			label16.Visible = false;
			txtTipoCambio.Visible = false;
			cmbMoneda.Visible = false;
			cbValorVenta.Visible = false;
			label10.Visible = false;
			txtBruto.Visible = false;
			label11.Visible = false;
			txtDscto.Visible = false;
			label19.Visible = false;
			txtFlete.Visible = false;
			label12.Visible = false;
			label13.Visible = false;
			label14.Visible = false;
			txtValorVenta.Visible = false;
			txtIGV.Visible = false;
			txtPrecioVenta.Visible = false;
		}
	}

	private void asignarDataADGV(DataTable data)
	{
		foreach (DataRow filadt in data.Rows)
		{
			object[] valoresFila = new object[dgvDetalle.Columns.Count];
			int i = 0;
			foreach (DataGridViewColumn coldgv in dgvDetalle.Columns)
			{
				object valor = "";
				foreach (DataColumn coldt in data.Columns)
				{
					if (coldgv.DataPropertyName == coldt.ColumnName)
					{
						valor = filadt.Field<object>(coldt.ColumnName);
						break;
					}
				}
				valoresFila[i] = valor;
				i++;
			}
			dgvDetalle.Rows.Add(valoresFila);
		}
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		if (tc != null)
		{
			txtTipoCambio.Text = tc.Venta.ToString();
			dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
		}
		else
		{
			MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			dtpFecha.Value = DateTime.Now.Date;
			dtpFecha.Focus();
		}
	}

	private void txtCodProv_KeyDown(object sender, KeyEventArgs e)
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
		form.Procede = 4;
		form.ShowDialog();
		if (CodProveedor != 0)
		{
			CargaProveedor();
			ProcessTabKey(forward: true);
		}
		else
		{
			BorrarProveedor();
		}
	}

	private void frmNotaIngreso_Shown(object sender, EventArgs e)
	{
		if (Proceso == 1 && txtTransaccion.Text == "FT")
		{
			if (tc == null)
			{
				MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				txtTipoCambio.Text = tc.Venta.ToString();
				txtTipoCambio.Visible = true;
				label16.Visible = true;
				txtTipoCambio.ReadOnly = false;
			}
		}
		if (txtTransaccion.Text == "FT")
		{
			cmbFormaPago.Visible = true;
			label17.Visible = true;
			dtpFechaPago.Visible = true;
		}
		if (generadaGRC)
		{
			btnPreListadodeProductos.PerformClick();
		}
	}

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Close();
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.Proceso = 3;
		form.Procedencia = 1;
		form.Transaccion = txtTransaccion.Text;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtDocRef.Text = doc.Sigla;
		if (CodDocumento != 0 && CodDocumento != 47)
		{
			if (CodDocumento == 48)
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
				MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void txtDocRef_Leave(object sender, EventArgs e)
	{
		BuscaTipoDocumento();
		VerificarCabecera();
		if (Validacion)
		{
			btnDetalle.Enabled = true;
		}
		if (CodDocumento != 0)
		{
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1 || Proceso == 7)
		{
			calculatotales();
		}
	}

	private void calculatotales()
	{
		if (proce == 1)
		{
			decimal bruto = default(decimal);
			decimal descuen = default(decimal);
			decimal valor = default(decimal);
			decimal igvt = default(decimal);
			decimal preciot = default(decimal);
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle2.Rows)
			{
				if (Convert.ToDecimal(row.Cells[cantn.Name].Value) != 0m)
				{
					bruto += Convert.ToDecimal(row.Cells[subtotal.Name].Value);
					valor += Convert.ToDecimal(row.Cells[valorventa1.Name].Value);
					igvt += Convert.ToDecimal(row.Cells[igv1.Name].Value);
					preciot += Convert.ToDecimal(row.Cells[importe1.Name].Value);
				}
			}
			txtBruto.Text = $"{bruto:#,##0.0000}";
			txtValorVenta.Text = $"{valor:#,##0.0000}";
			txtIGV.Text = $"{igvt:#,##0.0000}";
			if (txtFlete.Text.Length > 0)
			{
				if (Convert.ToDecimal(txtFlete.Text) > 0m)
				{
					txtPrecioVenta.Text = $"{preciot + Convert.ToDecimal(txtFlete.Text):#,##0.0000}";
				}
				else
				{
					txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
				}
			}
			else
			{
				txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
			}
			return;
		}
		decimal bruto2 = default(decimal);
		decimal descuen2 = default(decimal);
		decimal valor2 = default(decimal);
		decimal igvt2 = default(decimal);
		decimal preciot2 = default(decimal);
		foreach (DataGridViewRow row2 in (IEnumerable)dgvDetalle.Rows)
		{
			bruto2 += Convert.ToDecimal(row2.Cells[importe.Name].Value);
			descuen2 += Convert.ToDecimal(row2.Cells[montodscto.Name].Value);
			valor2 += Convert.ToDecimal(row2.Cells[valorventa.Name].Value);
			igvt2 += Convert.ToDecimal(row2.Cells[igv.Name].Value);
			preciot2 += Convert.ToDecimal(row2.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto2:#,##0.0000}";
		txtDscto.Text = $"{descuen2:#,##0.0000}";
		txtValorVenta.Text = $"{valor2:#,##0.0000}";
		txtIGV.Text = $"{igvt2:#,##0.0000}";
		if (txtFlete.Text.Length > 0)
		{
			if (Convert.ToDecimal(txtFlete.Text) > 0m)
			{
				txtPrecioVenta.Text = $"{preciot2 + Convert.ToDecimal(txtFlete.Text):#,##0.0000}";
			}
			else
			{
				txtPrecioVenta.Text = $"{preciot2:#,##0.0000}";
			}
		}
		else
		{
			txtPrecioVenta.Text = $"{preciot2:#,##0.0000}";
		}
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

	private void txtCodProv_Leave(object sender, EventArgs e)
	{
		if (CodProveedor == 0)
		{
			txtCodProv.Focus();
		}
		else
		{
			VerificarCabecera();
			if (Validacion)
			{
				btnDetalle.Visible = true;
			}
		}
		if (generadaGRC)
		{
			btnDetalle.Visible = false;
		}
	}

	private void txtNDocRef_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtNDocRef_Leave(object sender, EventArgs e)
	{
		if (!(txtNDocRef.Text == ""))
		{
			VerificarCabecera();
			if (Validacion)
			{
				btnDetalle.Enabled = true;
			}
		}
	}

	private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtOrdenCompra_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void txtAutorizacion_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void txtTransaccion_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
		}
		if ((txtTransaccion.Text.Equals("FT") || txtTransaccion.Text.Equals("IN")) && Proceso != 3)
		{
			btnDetalle.Visible = true;
		}
		if (generadaGRC)
		{
			btnDetalle.Visible = false;
		}
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
		if (proce == 1)
		{
			int valor = 0;
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle2.Rows)
			{
				string canti = "";
				string cantin = "";
				canti = Convert.ToString(Convert.ToDecimal(row.Cells[this.cant.Name].Value));
				cantin = Convert.ToString(Convert.ToDecimal(row.Cells[cantn.Name].Value));
				if (Convert.ToInt32(row.Cells[codetord.Name].Value) != 0 && (canti == "" || cantin == "" || cantin == "0.00"))
				{
					valor = 1;
				}
			}
			return valor;
		}
		int valor2 = 1;
		foreach (DataGridViewRow row2 in (IEnumerable)dgvDetalle.Rows)
		{
			string cant = "";
			string precio = "";
			string impor = "";
			string IG = "";
			string MontDes = "";
			string d1 = "";
			string d2 = "";
			string d3 = "";
			cant = Convert.ToString(Convert.ToDecimal(row2.Cells[cantidad.Name].Value));
			impor = Convert.ToString(row2.Cells[importe.Name]);
			IG = Convert.ToString(row2.Cells[igv.Name]);
			MontDes = Convert.ToString(row2.Cells[montodscto.Name]);
			precio = Convert.ToString(row2.Cells[preciounit.Name].Value);
			d1 = Convert.ToString(row2.Cells[dscto1.Name].Value);
			d2 = Convert.ToString(row2.Cells[dscto2.Name].Value);
			d3 = Convert.ToString(row2.Cells[dscto3.Name].Value);
			if (d1 != "" || d2 != "" || d3 != "")
			{
				calculatotales();
			}
			valor2 = ((cant == "" || precio == "" || impor == "" || IG == "" || cant == "0") ? 1 : 0);
		}
		return valor2;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (CodDocumento == 48 && string.IsNullOrEmpty(txtComentario.Text))
			{
				MessageBox.Show("Ingresar un Comentario.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			bool num;
			if (!EsCompraDirecta)
			{
				if (CodDocumento != 0 && txtSerieDocRef.Text.Trim() != "")
				{
					num = txtNDocRef.Text.Trim() != "";
					goto IL_00d6;
				}
			}
			else if (CodDocumento != 0 && txtSerieDocRef.Text.Trim() != "" && txtNDocRef.Text.Trim() != "")
			{
				num = prov != null;
				goto IL_00d6;
			}
			goto IL_15f4;
			IL_00d6:
			if (num)
			{
				int codigoProveedor = (EsCompraDirecta ? prov.CodProveedor : 0);
				if (AdmNota.ValidarCompraNotaIngreso(CodDocumento, txtSerieDocRef.Text.Trim(), txtNDocRef.Text.Trim(), codigoProveedor))
				{
					MessageBox.Show("Ya EXISTE un documento con los datos ingresados", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				bool rpta = false;
				if (!superValidator1.Validate())
				{
					return;
				}
				if (verificarCamposVacios() == 1)
				{
					MessageBox.Show("Debe completar Detalle de Nota, Datos Vacios");
				}
				else if (proce == 1)
				{
					if (Proceso == 0)
					{
						return;
					}
					nota.CodAlmacen = frmLogin.iCodAlmacen;
					nota.CodTipoTransaccion = tran.CodTransaccion;
					nota.CodProveedor = prov.CodProveedor;
					nota.CodTipoDocumento = doc.CodTipoDocumento;
					nota.NumDoc = txtSerieDocRef.Text + "-" + txtNDocRef.Text;
					nota.FechaIngreso = dtpFecha.Value.Date;
					nota.Comentario = txtComentario.Text;
					nota.CodUser = frmLogin.iCodUser;
					nota.CodOrdenCompra = codOrdenCompra_nota;
					nota.codalmacenemisor = 0;
					if (txtFlete.Text == "")
					{
						nota.Flete = 0.0;
					}
					else
					{
						nota.Flete = Convert.ToDouble(txtFlete.Text);
					}
					if (codOrdenCompra_nota == 0)
					{
						nota.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
						if (txtTipoCambio.Text != "")
						{
							nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
						}
						else
						{
							nota.TipoCambio = 0.0;
						}
						if (cmbFormaPago.SelectedValue == null)
						{
							nota.FormaPago = 0;
						}
						else
						{
							nota.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
						}
						nota.FechaPago = dtpFecha.Value.Date;
					}
					nota.MontoBruto = Convert.ToDouble(txtBruto.Text);
					if (txtDscto.Text != "")
					{
						nota.MontoDscto = Convert.ToDouble(txtDscto.Text);
					}
					else
					{
						nota.MontoDscto = 0.0;
					}
					nota.Igv = Convert.ToDouble(txtIGV.Text);
					nota.Total = Convert.ToDouble(txtPrecioVenta.Text);
					nota.CodUser = frmLogin.iCodUser;
					nota.Estado = 1;
					nota.Codtransferencia = 0;
					if (Proceso == 1)
					{
						if (!AdmNota.insert(nota))
						{
							return;
						}
						RecorreDetalle();
						if (detalle.Count > 0)
						{
							foreach (clsDetalleNotaIngreso det in detalle)
							{
								AdmNota.insertdetalle(det);
								AdmNota.ActualizaCantidadPendiente(det.Cantidad, det.CodProducto, codOrdenCompra_nota, det.CoddetalleOrden);
								if (CodAlmacenOrden != frmLogin.iCodAlmacen && txtTransaccion.Text == "IOC")
								{
									AdmNota.ActualizaCantidadPendiente2(det.Cantidad, det.CodProducto, CodAlmacenOrden, frmLogin.iCodUser);
								}
							}
						}
						MessageBox.Show("Los datos se guardaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Close();
					}
					else
					{
						if (Proceso != 2 || !AdmNota.update(nota))
						{
							return;
						}
						RecorreDetalle();
						foreach (clsDetalleNotaIngreso det2 in nota.Detalle)
						{
							foreach (clsDetalleNotaIngreso det3 in detalle)
							{
								if (det2.CodDetalleIngreso == det3.CodDetalleIngreso)
								{
									AdmNota.updatedetalle(det3);
								}
							}
							AdmNota.deletedetalle(det2.CodDetalleIngreso);
						}
						foreach (clsDetalleNotaIngreso deta in detalle)
						{
							if (deta.CodDetalleIngreso == 0)
							{
								AdmNota.insertdetalle(deta);
							}
						}
						MessageBox.Show("Los datos se actualizaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Close();
					}
				}
				else
				{
					if (Proceso == 0)
					{
						return;
					}
					nota.CodAlmacen = frmLogin.iCodAlmacen;
					fac.CodAlmacen = frmLogin.iCodAlmacen;
					nota.CodTipoTransaccion = tran.CodTransaccion;
					fac.CodTipoTransaccion = tran.CodTransaccion;
					nota.CodProveedor = prov.CodProveedor;
					fac.CodProveedor = prov.CodProveedor;
					nota.CodTipoDocumento = doc.CodTipoDocumento;
					fac.CodTipoDocumento = doc.CodTipoDocumento;
					nota.Serie = txtSerieDocRef.Text;
					fac.Serie = txtSerieDocRef.Text;
					nota.NumDoc = txtNDocRef.Text;
					fac.NumFac = txtNDocRef.Text;
					nota.NumDoc = txtSerieDocRef.Text + "-" + txtNDocRef.Text;
					fac.DocumentoFactura = doc.Sigla + "-" + txtSerieDocRef.Text + "-" + txtNDocRef.Text;
					nota.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					fac.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					if (txtTipoCambio.Visible)
					{
						fac.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
						nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
					}
					nota.FechaIngreso = dtpFecha.Value.Date;
					fac.FechaIngreso = dtpFecha.Value.Date;
					nota.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
					fac.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
					nota.FechaPago = dtpFechaPago.Value.Date;
					fac.FechaPago = dtpFechaPago.Value.Date;
					if (fpago.Dias == 0)
					{
						nota.FechaCancelado = dtpFecha.Value.Date;
						fac.FechaCancelado = dtpFecha.Value.Date;
					}
					nota.Cancelado = 0;
					fac.Cancelado = 0;
					fac.Comentario = txtComentario.Text;
					fac.MontoBruto = Convert.ToDouble(txtBruto.Text);
					fac.MontoDscto = Convert.ToDouble(txtDscto.Text);
					nota.Comentario = txtComentario.Text;
					nota.MontoBruto = Convert.ToDouble(txtBruto.Text);
					nota.MontoDscto = Convert.ToDouble(txtDscto.Text);
					if (txtFlete.Text != "")
					{
						nota.Flete = Convert.ToDouble(txtFlete.Text);
						fac.Flete = Convert.ToDouble(txtFlete.Text);
					}
					fac.Igv = Convert.ToDouble(txtIGV.Text);
					fac.Total = Convert.ToDouble(txtPrecioVenta.Text);
					fac.CodUser = frmLogin.iCodUser;
					nota.Igv = Convert.ToDouble(txtIGV.Text);
					nota.Total = Convert.ToDouble(txtPrecioVenta.Text);
					nota.CodUser = frmLogin.iCodUser;
					nota.codalmacenemisor = 0;
					nota.Estado = 1;
					fac.Estado = 1;
					nota.Codtransferencia = 0;
					nota.responsable = Convert.ToInt32(cmbUsuario.SelectedValue);
					nota.area = cmbarea.Text;
					if (txtOrdenCompra.Text != "" && txtOrdenCompra.Text != ".")
					{
						nota.CodOrdenCompra = Convert.ToInt32(txtOrdenCompra.Text);
					}
					else
					{
						nota.CodOrdenCompra = 0;
					}
					bool CompraDirecta = ((nota.FormaPago != 0) ? true : false);
					if (Proceso == 1)
					{
						if (txtTransaccion.Text.Equals("IN"))
						{
							RecorreDetalle();
							nota.Detalle = detalle;
							if (!AdmNota.insertarNota(nota))
							{
								MessageBox.Show("Ocurrió un error al registrar la operación", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
							MessageBox.Show("Los datos se guardaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							if (fpago.Dias == 0 && nota.CodTipoTransaccion == 1)
							{
								ingresarpago();
							}
							Close();
						}
						else
						{
							RecorreDetalle();
							nota.Detalle = detalle;
							fac.Detalle = detalleFactura;
							if (!AdmNota.insertarNotayFactura(nota, fac))
							{
								MessageBox.Show("Ocurrió un error al registrar la operación", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
							MessageBox.Show("Los datos se guardaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							notaIngresoIgv.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
							notaIngresoIgv.IncluyeIgv = !cbValorVenta.Checked;
							AdmNotaIngresoIgv.insert(notaIngresoIgv);
							foreach (clsDetalleNotaIngreso det4 in detalle)
							{
								if (!CompraDirecta)
								{
									continue;
								}
								double precioUnitarioProducto = ((nota.Moneda == 2) ? (det4.PrecioUnitario * Convert.ToDouble(txtTipoCambio.Text.Trim())) : det4.PrecioUnitario);
								if (CodOrdenCompraNew == 0)
								{
									if (precioUnitarioProducto != det4.UltimoPrecioCompra)
									{
										string mensaje = "El Precio de compra del producto " + det4.DescripcionProducto.ToUpper() + " ha";
										bool tipoModificacion = true;
										if (precioUnitarioProducto > det4.UltimoPrecioCompra)
										{
											mensaje = mensaje + " SUBIDO (↑ S/." + (precioUnitarioProducto - det4.UltimoPrecioCompra) + "), se sugiere modificar el precio de venta del producto ¿Desea Modificarlo?";
										}
										else
										{
											mensaje = mensaje + " DISMINUIDO (↓ S/." + (det4.UltimoPrecioCompra - precioUnitarioProducto) + "), se sugiere modificar el precio de venta del producto ¿Desea Modificarlo?";
											tipoModificacion = false;
										}
										DialogResult dr = MessageBox.Show(mensaje, "Alerta de cambio de precio de producto", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
										if (dr == DialogResult.Yes)
										{
											frmModificarPrecioVentaProducto form = new frmModificarPrecioVentaProducto(det4.CodProducto, tipoModificacion, det4.UnidadIngresada, nota.Moneda, Convert.ToDouble(txtTipoCambio.Text.Trim()));
											form.ShowDialog();
										}
									}
									continue;
								}
								DataTable datos = admOrdenCompra.ListaProductosModificarPrecioA_1(CodOrdenCompraNew, frmLogin.iCodEmpresa);
								double flete = Convert.ToDouble(datos.Rows[0]["flete_nuevo"]);
								double pc = Convert.ToDouble(datos.Rows[0]["precio_compraN_soles"]);
								double pv = Convert.ToDouble(datos.Rows[0]["precio_venta_final"]);
								if (flete != 0.0)
								{
									int codProd = Convert.ToInt32(det4.CodProducto);
									Admpro.cambiarFleteDeProducto(codProd, flete);
								}
								if (pc != 0.0)
								{
									int codEquiv = Convert.ToInt32(datos.Rows[0]["codUnidadEquiCompra"]);
									Admpro.updateunidadequivalente(codEquiv, Convert.ToDecimal(pc));
								}
								if (pv != 0.0)
								{
									int codEquiv2 = Convert.ToInt32(datos.Rows[0]["codUnidadEquivalente"]);
									Admpro.updateunidadequivalente(codEquiv2, Convert.ToDecimal(pv));
								}
							}
							if (fpago.Dias == 0 && nota.CodTipoTransaccion == 1)
							{
								ingresarpago();
							}
							Close();
						}
					}
					else if (Proceso == 2)
					{
						foreach (DataGridViewRow fila2 in (IEnumerable)dgvDocumentosRelacionados.Rows)
						{
							string _cod = fila2.Cells["colDocRelacionado"].Value.ToString();
							string _cod2 = _cod.Substring(0, 2);
							if (_cod2 == "OC")
							{
								string _cod3 = _cod.Substring(3, 11);
								nota.ordenOC = Convert.ToInt32(_cod3);
							}
						}
						if (AdmNota.update(nota))
						{
							RecorreDetalle();
							foreach (clsDetalleNotaIngreso det5 in nota.Detalle)
							{
								foreach (clsDetalleNotaIngreso det6 in detalle)
								{
									if (det5.CodDetalleIngreso == det6.CodDetalleIngreso)
									{
										AdmNota.updatedetalle(det6);
									}
								}
								AdmNota.deletedetalle(det5.CodDetalleIngreso);
							}
							foreach (clsDetalleNotaIngreso deta2 in detalle)
							{
								if (deta2.CodDetalleIngreso == 0)
								{
									AdmNota.insertdetalle(deta2);
								}
							}
							MessageBox.Show("Los datos se actualizaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							Close();
						}
					}
					bool no_generada = false;
					if (generadaGRC)
					{
						if (nota != null)
						{
							if (nota.CodNotaIngreso != "")
							{
								int codnotaingreso = Convert.ToInt32(nota.CodNotaIngreso);
								bool rptadef = false;
								foreach (clsGuiaRemision grc1 in listadoGRC)
								{
									if (!admgrc.setCodFacturaCompra(Convert.ToInt32(grc1.CodGuiaRemision), Convert.ToInt32(nota.CodNotaIngreso)))
									{
										MessageBox.Show("No se actualizo la guia de remision con la factura generada,\nVerifique BD:\n>Nota de Ingreso: " + nota.CodNotaIngreso + "\n>Guia de Remision Compra: " + grc1.CodGuiaRemision);
										if (grc1.CodGuiaRemision == grc.CodGuiaRemision)
										{
											rptadef = true;
										}
									}
								}
								ventana_grc.DespuesDeGenerarFactura(rptadef, null);
							}
							else
							{
								no_generada = true;
							}
						}
						else
						{
							no_generada = true;
						}
					}
					else if (generadaGRCparaFlete)
					{
						if (nota != null)
						{
							if (nota.CodNotaIngreso != "")
							{
								foreach (clsGuiaRemision grc2 in listadoGRC)
								{
									clsGuiaRemisionCompraDocumentoRelacionado nuevo = new clsGuiaRemisionCompraDocumentoRelacionado();
									nuevo.CodGuiaRemisionCompra = Convert.ToInt32(grc2.CodGuiaRemision);
									nuevo.CodDocumentoRelacionado = Convert.ToInt32(nota.CodNotaIngreso);
									nuevo.CodTipoDocumento = nota.CodTipoDocumento;
									nuevo.TipoGRCDR = 4;
									nuevo.Anulado = 0;
									if (!admgrc.insertarDocumentoRelacionado(nuevo) && grc2.CodGuiaRemision == grc.CodGuiaRemision)
									{
										no_generada = true;
									}
								}
							}
							else
							{
								no_generada = true;
							}
						}
						else
						{
							no_generada = true;
						}
					}
					if (no_generada)
					{
						MessageBox.Show("Factura Compra no asignada a Guia de Remision de Compra", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					if (ventana_grc != null)
					{
						ventana_grc.guardarsinmostrarmensaje();
					}
				}
				return;
			}
			goto IL_15f4;
			IL_15f4:
			MessageBox.Show("Verifique los datos del formulario", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrio un error: " + ex.Message, "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void ingresarpago()
	{
		frmCancelarPago form = new frmCancelarPago();
		form.CodNota = fac.CodFacturaNueva.ToString();
		form.tipo = 1;
		form.ShowDialog();
	}

	private void RecorreDetalle()
	{
		if (proce == 1)
		{
			detalle.Clear();
			if (dgvDetalle2.Rows.Count <= 0)
			{
				return;
			}
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle2.Rows)
				{
					añadedetalle(row);
				}
				return;
			}
		}
		detalle.Clear();
		detalleFactura.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row2 in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalle(row2);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		if (proce == 1)
		{
			clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
			deta1.CodProducto = Convert.ToInt32(fila.Cells[coprod.Name].Value);
			deta1.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
			deta1.CodAlmacen = frmLogin.iCodAlmacen;
			deta1.UnidadIngresada = Convert.ToInt32(fila.Cells[coduni.Name].Value);
			deta1.SerieLote = "0";
			deta1.Cantidad = Convert.ToDouble(fila.Cells[cantn.Name].Value);
			deta1.FechaIngreso = dtpFecha.Value;
			deta1.CodUser = frmLogin.iCodUser;
			deta1.CodProveedor = Convert.ToInt32(txtCodProveedor.Text);
			deta1.CoddetalleOrden = Convert.ToInt32(fila.Cells[codetord.Name].Value);
			deta1.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounitario.Name].Value);
			deta1.Subtotal = Convert.ToDouble(fila.Cells[subtotal.Name].Value);
			deta1.Descuento1 = Convert.ToDouble(fila.Cells[descuento1.Name].Value);
			deta1.Descuento2 = Convert.ToDouble(fila.Cells[descuento2.Name].Value);
			deta1.Descuento3 = Convert.ToDouble(fila.Cells[descuento3.Name].Value);
			deta1.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto1.Name].Value);
			deta1.Igv = Convert.ToDouble(fila.Cells[igv1.Name].Value);
			deta1.Importe = Convert.ToDouble(fila.Cells[importe1.Name].Value);
			deta1.PrecioReal = Convert.ToDouble(fila.Cells[precioreal1.Name].Value);
			deta1.ValoReal = Convert.ToDouble(fila.Cells[valoreal1.Name].Value);
			deta1.Flete = Convert.ToDouble(fila.Cells[flete1.Name].Value);
			deta1.Bonificacion = Convert.ToBoolean(fila.Cells[Bonificacion.Name].Value);
			deta1.stado = Convert.ToString(fila.Cells["stado"].Value?.ToString());
			deta1.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			deta1.DescripcionProducto = fila.Cells[descripcion.Name].Value.ToString();
			detalle.Add(deta1);
			return;
		}
		clsDetalleNotaIngreso deta2 = new clsDetalleNotaIngreso();
		clsDetalleFactura detafac = new clsDetalleFactura();
		detafac.CodFactura = fac.CodFacturaNueva;
		deta2.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detafac.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta2.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
		detafac.CodNotaIngreso = nota.CodNotaIngreso;
		deta2.CodAlmacen = frmLogin.iCodAlmacen;
		detafac.CodAlmacen = frmLogin.iCodAlmacen;
		deta2.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detafac.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta2.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		detafac.SerieLote = "0";
		deta2.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		detafac.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta2.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detafac.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta2.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detafac.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta2.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detafac.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta2.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detafac.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta2.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detafac.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta2.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detafac.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		deta2.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		detafac.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta2.Flete = Convert.ToDouble(fila.Cells[flete0.Name].Value);
		detafac.Flete = Convert.ToDouble(fila.Cells[flete0.Name].Value);
		deta2.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		detafac.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta2.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		detafac.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta2.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		detafac.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta2.FechaIngreso = dtpFecha.Value;
		detafac.FechaIngreso = dtpFecha.Value;
		deta2.Bonificacion = false;
		deta2.stado = Convert.ToString(fila.Cells["stado"].Value?.ToString());
		deta2.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		detafac.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		deta2.CodUser = frmLogin.iCodUser;
		detafac.CodUser = frmLogin.iCodUser;
		if (txtCodProveedor.Text != "")
		{
			detafac.CodProveedor = Convert.ToInt32(txtCodProveedor.Text);
		}
		else
		{
			detafac.CodProveedor = 0;
		}
		object auxultpreciocompra = fila.Cells[UltimoPrecioCompra.Name].Value;
		deta2.UltimoPrecioCompra = Convert.ToDouble((auxultpreciocompra != "" && auxultpreciocompra != DBNull.Value && auxultpreciocompra != null) ? auxultpreciocompra : ((object)0));
		deta2.DescripcionProducto = fila.Cells[descripcion.Name].Value.ToString();
		deta2.EstadoDeLaOrden = Convert.ToString(fila.Cells[EstadoDeLaOrden.Name].Value);
		deta2.ProductoSolicitado = Convert.ToString(fila.Cells[ProductoSolicitado.Name].Value);
		object auxctdadpendiente = fila.Cells[cantpendiente.Name].Value;
		deta2.cantidadpendiente = Convert.ToDecimal((auxctdadpendiente != "" && auxctdadpendiente != DBNull.Value) ? auxctdadpendiente : ((object)0));
		detalle.Add(deta2);
		detalleFactura.Add(detafac);
	}

	private void CargaFilaDetalle(DataGridViewRow fila)
	{
		if (proce == 1)
		{
			detaSelec.CodProducto = Convert.ToInt32(fila.Cells[coprod.Name].Value);
			detaSelec.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
			detaSelec.CodAlmacen = frmLogin.iCodAlmacen;
			detaSelec.UnidadIngresada = Convert.ToInt32(fila.Cells[coduni.Name].Value);
			detaSelec.Flete = Convert.ToDouble(fila.Cells[flete1.Name].Value);
			if (fila.Cells[cantn.Name].Value != DBNull.Value)
			{
				detaSelec.Cantidad = Convert.ToDouble(fila.Cells[cantn.Name].Value);
			}
			else
			{
				detaSelec.Cantidad = 0.0;
			}
			detaSelec.FechaIngreso = dtpFecha.Value;
			detaSelec.CodUser = frmLogin.iCodUser;
			return;
		}
		detaSelec.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detaSelec.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
		detaSelec.CodAlmacen = frmLogin.iCodAlmacen;
		detaSelec.Moneda = cmbMoneda.SelectedIndex;
		detaSelec.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detaSelec.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		if (fila.Cells[cantidad.Name].Value != DBNull.Value)
		{
			detaSelec.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		}
		else
		{
			detaSelec.Cantidad = 0.0;
		}
		detaSelec.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detaSelec.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detaSelec.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detaSelec.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detaSelec.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detaSelec.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detaSelec.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		if (fila.Cells[flete0.Name].Value != DBNull.Value)
		{
			detaSelec.Flete = Convert.ToDouble(fila.Cells[flete0.Name].Value);
		}
		else
		{
			detaSelec.Flete = 0.0;
		}
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
			form.Procede = 6;
			form.bvalorventa = cbValorVenta.Checked;
			form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
			form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
			form.txtReferencia.ReadOnly = true;
			form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
			form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
			form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
			form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
			form.txtDscto2.Text = row.Cells[dscto2.Name].Value.ToString();
			form.txtDscto3.Text = row.Cells[dscto3.Name].Value.ToString();
			form.txtPrecioNeto.Text = row.Cells[importe.Name].Value.ToString();
			form.txtCantidad.Focus();
			form.ShowDialog();
		}
	}

	private void txtAutorizacion_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmAutorizado"] != null)
		{
			Application.OpenForms["frmAutorizado"].Activate();
			return;
		}
		frmAutorizado form = new frmAutorizado();
		form.Proceso = 3;
		form.ShowDialog();
		aut = form.aut;
		CodAutorizado = aut.CodAutorizado;
		if (CodAutorizado != 0)
		{
			CargaAutorizado();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaAutorizado()
	{
		aut = AdmAut.MuestraAutorizado(CodAutorizado);
		txtAutorizacion.Text = aut.CodAutorizado.ToString();
		lbAutorizado.Text = aut.Nombre;
	}

	private void txtAutorizacion_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && txtAutorizacion.Text != "")
		{
			if (BuscaAutorizado())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("El codigo no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaAutorizado()
	{
		aut = AdmAut.MuestraAutorizado(Convert.ToInt32(txtAutorizacion.Text));
		if (aut != null)
		{
			lbAutorizado.Text = aut.Nombre;
			CodAutorizado = aut.CodAutorizado;
			return true;
		}
		lbAutorizado.Text = "";
		CodAutorizado = 0;
		return false;
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
		{
			detaSelec.CodProducto = Convert.ToInt32(e.Row.Cells[codproducto.Name].Value);
		}
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cmbFormaPago.SelectedValue != null)
		{
			fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
			dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.SelectedRows.Count > 0)
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
			codProd.Remove(detaSelec.CodProducto);
		}
		if (dgvDetalle2.SelectedRows.Count > 0)
		{
			dgvDetalle2.Rows.Remove(dgvDetalle2.CurrentRow);
		}
		if (dgvDetalle2.Rows.Count == 0)
		{
			data.Clear();
		}
		dgvDetalle2.Refresh();
		lblCantidadProductos.Text = "Productos Agregados :" + dgvDetalle.RowCount;
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso == 1)
		{
			int hola = dgvDetalle.Rows.Count;
			calculatotales();
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

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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
		if (Proceso != 0 && e.ControlToValidate.Visible)
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
		if (Proceso != 0 && e.ControlToValidate.Visible)
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
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0 && c.Visible)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa" && Proceso == 1)
		{
			calculatotales();
		}
	}

	private void txtPDescuento_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar == '\r')
		{
			prorrateodeflete();
			recalculadetalle();
			calculatotales();
		}
	}

	private void prorrateodeflete()
	{
		if (!(txtFlete.Text != "") || dgvDetalle.Rows.Count < 1)
		{
			return;
		}
		double precior = 0.0;
		double factorflete = 0.0;
		double fleter = 0.0;
		double totalr = 0.0;
		double dflete = Convert.ToDouble(txtFlete.Text);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			totalr += Convert.ToDouble(row.Cells[precioventa.Name].Value);
		}
		factorflete = dflete / totalr;
		foreach (DataGridViewRow row2 in (IEnumerable)dgvDetalle.Rows)
		{
			precior = Convert.ToDouble(row2.Cells[precioventa.Name].Value);
			fleter = precior * factorflete;
			row2.Cells[flete0.Name].Value = $"{fleter:#,##0.0000}";
		}
	}

	private void recalculadetalle()
	{
		if (proce == 1)
		{
			decimal vvflete = default(decimal);
			decimal pvflete = default(decimal);
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle2.Rows)
				{
					if (Convert.ToDecimal(row.Cells[cantn.Name].Value) != 0m)
					{
						vvflete = Convert.ToDecimal(row.Cells[valorventa1.Name].Value) + Convert.ToDecimal(row.Cells[flete1.Name].Value);
						pvflete = Convert.ToDecimal(row.Cells[importe1.Name].Value) + Convert.ToDecimal(row.Cells[flete1.Name].Value);
						if (Convert.ToDouble(row.Cells[flete1.Name].Value) > 0.0 && row.Cells[flete1.Name].Value.ToString() != "")
						{
							row.Cells[valoreal1.Name].Value = vvflete / Convert.ToDecimal(row.Cells[cantn.Name].Value);
							row.Cells[precioreal1.Name].Value = pvflete / Convert.ToDecimal(row.Cells[cantn.Name].Value);
						}
						else
						{
							row.Cells[valoreal1.Name].Value = Convert.ToDecimal(row.Cells[valorventa1.Name].Value) / Convert.ToDecimal(row.Cells[cantn.Name].Value);
							row.Cells[precioreal1.Name].Value = Convert.ToDecimal(row.Cells[importe1.Name].Value) / Convert.ToDecimal(row.Cells[cantn.Name].Value);
						}
					}
				}
				return;
			}
		}
		foreach (DataGridViewRow row2 in (IEnumerable)dgvDetalle.Rows)
		{
			row2.Cells[valorventaconflete.Name].Value = (Convert.ToDouble(row2.Cells[valorventa.Name].Value) + Convert.ToDouble(row2.Cells[flete0.Name].Value)).ToString("###.####");
			row2.Cells[pvconflete.Name].Value = (Convert.ToDouble(row2.Cells[precioventa.Name].Value) + Convert.ToDouble(row2.Cells[flete0.Name].Value)).ToString("###.####");
			if (Convert.ToDouble(row2.Cells[flete0.Name].Value) > 0.0 && row2.Cells[flete0.Name].Value.ToString() != "")
			{
				row2.Cells[valoreal.Name].Value = (Convert.ToDouble(row2.Cells[valorventaconflete.Name].Value) / Convert.ToDouble(row2.Cells[cantidad.Name].Value)).ToString("###.####");
				row2.Cells[precioreal.Name].Value = (Convert.ToDouble(row2.Cells[pvconflete.Name].Value) / Convert.ToDouble(row2.Cells[cantidad.Name].Value)).ToString("###.####");
			}
			else
			{
				row2.Cells[valoreal.Name].Value = (Convert.ToDouble(row2.Cells[valorventa.Name].Value) / Convert.ToDouble(row2.Cells[cantidad.Name].Value)).ToString("###.####");
				row2.Cells[precioreal.Name].Value = (Convert.ToDouble(row2.Cells[precioventa.Name].Value) / Convert.ToDouble(row2.Cells[cantidad.Name].Value)).ToString("###.####");
			}
		}
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (base.Visible && dgvDetalle.Rows.Count >= 1 && dgvDetalle.CurrentRow.Index == e.RowIndex && e.RowIndex != -1)
		{
			CargaFilaDetalle(dgvDetalle.CurrentRow);
		}
	}

	private void txtOrdenCompra_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void CargaOrden()
	{
		try
		{
			Orde = AdmOrd.CargaOrdenCompra(Convert.ToInt32(CodOrdenCompra));
			if (Orde != null)
			{
				txtOrdenCompra.Text = Orde.CodOrdenCompra.ToString().PadLeft(6, '0');
				CodTransaccion = 1;
				CargaTransaccion();
				if (txtCodProv.Enabled)
				{
					CodProveedor = Orde.CodProveedor;
					if (txtCodProv.Enabled)
					{
						CodProveedor = Orde.CodProveedor;
						prov = AdmProv.MuestraProveedor(CodProveedor);
						txtCodProv.Text = prov.Ruc;
						txtNombreProv.Text = prov.RazonSocial;
						txtCodProveedor.Text = prov.CodProveedor.ToString();
						cmbFormaPago.SelectedIndex = 2;
						if (Convert.ToInt32(cmbFormaPago.SelectedValue) != 0)
						{
							EventArgs ee = new EventArgs();
							cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
						}
						else
						{
							dtpFechaPago.Value = DateTime.Today;
						}
					}
				}
				txtComentario.Text = Orde.Comentario;
				cmbMoneda.SelectedValue = Orde.Moneda;
				cmbFormaPago.SelectedValue = Orde.FormaPago;
				cmbFormaPago_SelectionChangeCommitted(new object(), new EventArgs());
				CargaDetalleOrden();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaDetalleOrden()
	{
		DataTable newData = new DataTable();
		dgvDetalle.Rows.Clear();
		try
		{
			newData = AdmOrd.CargaDetalle_Factura_Venta(Convert.ToInt32(Orde.CodOrdenCompra));
			foreach (DataRow row in newData.Rows)
			{
				dgvDetalle.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), Convert.ToDateTime(row[24].ToString()), row[25].ToString(), Convert.ToDateTime(row[26].ToString()), 0, Convert.ToBoolean(row[28].ToString()), Convert.ToBoolean(row[29].ToString()));
				dgvDetalle.Columns[28].Visible = false;
				dgvDetalle.Columns[29].Visible = false;
				btnDetalle.Enabled = false;
			}
			dgvDetalle.ClearSelection();
			RecorreDetalle();
			nota.Detalle = detalle;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvDetalle2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvDetalle2_KeyPress;
			txtedit.KeyPress += dgvDetalle2_KeyPress;
			txtedit.KeyUp -= dgvDetalle2_KeyUp;
			txtedit.KeyUp += dgvDetalle2_KeyUp;
		}
	}

	private void dgvDetalle2_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle2.CurrentCell.ColumnIndex == 7)
		{
			val.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle2_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dgvDetalle2_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (base.Visible && dgvDetalle2.Rows.Count >= 1 && dgvDetalle2.CurrentRow.Index == e.RowIndex && e.RowIndex != -1)
		{
			CargaFilaDetalle(dgvDetalle2.CurrentRow);
		}
	}

	private void dgvDetalle2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			QIngresado = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[SaldoIngresado1.Name].Value);
			QPorAtender = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantidadPendiente1.Name].Value);
			if (dgvDetalle2.Columns[dgvDetalle2.CurrentCell.ColumnIndex].Name == "cantn" && txtedit.Text != "" && Convert.ToInt32(dgvDetalle2.CurrentRow.Cells[codetord.Name].Value) != 0)
			{
				if (Convert.ToDouble(txtedit.Text) > Convert.ToDouble(dgvDetalle2.CurrentRow.Cells[cantidadPendiente1.Name].Value))
				{
					MessageBox.Show("Cantidad Nueva Debe Ser Menor o Igual que la Cantidad de la Orden");
					dgvDetalle2.CurrentRow.Cells[cantn.Name].Value = 0.0;
					return;
				}
				Qnueva = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantn.Name].Value);
				dgvDetalle2.CurrentRow.Cells[SaldoIngresado.Name].Value = QIngresado + Qnueva;
				dgvDetalle2.CurrentRow.Cells[cantidadPendiente.Name].Value = QPorAtender - Qnueva;
				dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantn.Name].Value) * Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[preciounitario.Name].Value);
				importes();
				calculatotales();
			}
			else if (dgvDetalle2.Columns[dgvDetalle2.CurrentCell.ColumnIndex].Name == "cantn" && txtedit.Text != "" && Convert.ToInt32(dgvDetalle2.CurrentRow.Cells[codetord.Name].Value) == 0)
			{
				dgvDetalle2.CurrentRow.Cells[SaldoIngresado.Name].Value = txtedit.Text;
				dgvDetalle2.CurrentRow.Cells[SaldoIngresado1.Name].Value = txtedit.Text;
				dgvDetalle2.CurrentRow.Cells[cant.Name].Value = txtedit.Text;
				dgvDetalle2.CurrentRow.Cells[cantn.Name].Value = txtedit.Text;
				dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantn.Name].Value) * Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[preciounitario.Name].Value);
				importes();
				calculatotales();
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void importes()
	{
		decimal precio = default(decimal);
		decimal valor = default(decimal);
		decimal Igv = default(decimal);
		prod = Admpro.CargaProductoDetalle(Convert.ToInt32(dgvDetalle2.CurrentRow.Cells[coprod.Name].Value), frmLogin.iCodAlmacen, 1, 0, 0);
		if (prod.ConIgv)
		{
			precio = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value);
			valor = precio / Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			Igv = precio - valor;
		}
		else
		{
			precio = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value);
			valor = precio;
			Igv = precio - valor;
		}
		dgvDetalle2.CurrentRow.Cells[importe1.Name].Value = precio;
		dgvDetalle2.CurrentRow.Cells[valorventa1.Name].Value = valor;
		dgvDetalle2.CurrentRow.Cells[igv1.Name].Value = Igv;
	}

	private void dgvDetalle2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvDetalle2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void txtDocRef_TextChanged(object sender, EventArgs e)
	{
	}

	private void dgvDetalle2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1)
		{
			calculatotales();
		}
	}

	private void dgvDetalle2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso == 1)
		{
			calculatotales();
		}
	}

	private void customValidator8_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void customValidator9_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0 && c.Visible)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void btnVerificar_Click(object sender, EventArgs e)
	{
		bool num;
		if (!EsCompraDirecta)
		{
			if (CodDocumento != 0 && txtSerieDocRef.Text.Trim() != "")
			{
				num = txtNDocRef.Text.Trim() != "";
				goto IL_0098;
			}
		}
		else if (CodDocumento != 0 && txtSerieDocRef.Text.Trim() != "" && txtNDocRef.Text.Trim() != "")
		{
			num = prov != null;
			goto IL_0098;
		}
		goto IL_0122;
		IL_0122:
		MessageBox.Show("Verifique los datos del formulario", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		return;
		IL_0098:
		if (num)
		{
			int codigoProveedor = (EsCompraDirecta ? prov.CodProveedor : 0);
			if (AdmNota.ValidarCompraNotaIngreso(CodDocumento, txtSerieDocRef.Text.Trim(), txtNDocRef.Text.Trim(), codigoProveedor))
			{
				MessageBox.Show("Ya EXISTE un documento con los datos ingresados", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show("Los datos del documento NO han sido utilizados", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return;
		}
		goto IL_0122;
	}

	private void dgvDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit1 = e.Control as TextBox;
		if (txtedit1 != null)
		{
			txtedit1.KeyPress -= dgvDetalle_celltextbox_KeyPress;
			txtedit1.KeyPress += dgvDetalle_celltextbox_KeyPress;
		}
	}

	private void dgvDetalle_celltextbox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.OwningColumn.Name == preciounit.Name)
		{
			ok1.NumerosDecimales(e, sender as TextBox);
		}
	}

	private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		cantidad_previa1 = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[preciounit.Name].Value);
		valor_venta = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[valorventa.Name].Value);
		valor_compra = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[precioventa.Name].Value);
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (!generadaGRC)
			{
				return;
			}
			string sctdadInicial = preciosGRC[dgvDetalle.Rows[e.RowIndex].Cells[codDetalleGRC.Name].Value.ToString()];
			double ctdadInicial = Convert.ToDouble(sctdadInicial);
			double nueva_cantidad = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[preciounit.Name].Value);
			double nueva_VC = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[valorventa.Name].Value);
			double nueva_PC = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[precioventa.Name].Value);
			double importe = Math.Round(Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells["importe"].Value) + 0.1, 4);
			double totalbase = Math.Round(Convert.ToDouble((dgvDetalle.Rows[e.RowIndex].Cells["totalinicial"].Value == null) ? ((object)0) : dgvDetalle.Rows[e.RowIndex].Cells["totalinicial"].Value), 4);
			double comprartotal = 0.0;
			comprartotal = ((totalbase != 0.0) ? totalbase : importe);
			if (e.ColumnIndex == dgvDetalle.Columns["preciounit"].Index && nueva_cantidad != ctdadInicial)
			{
				if (nueva_cantidad > importe)
				{
					usuario_click = null;
					frmAutorizacion frm = new frmAutorizacion();
					frm.tipoAccion = 2;
					int codPermiso = new clsAdmFormulario().getPermisoAumentarPreciodeCompraFacturaGeneradaGR();
					frm.permiso = codPermiso;
					frm.PermitirAdministradores = true;
					frm.tipoVentanaAAsignarUsuario = 11;
					frm.ventanaNotaIngreso = this;
					DialogResult dr = frm.ShowDialog();
					if (dr != DialogResult.OK || usuario_click == null)
					{
						dgvDetalle.Rows[e.RowIndex].Cells[preciounit.Name].Value = cantidad_previa1;
					}
				}
				recalculaFilaConPrecioUnitarioCambiado(e.RowIndex);
			}
			if (e.ColumnIndex == dgvDetalle.Columns["valorventa"].Index && nueva_VC != ctdadInicial)
			{
				if (nueva_VC > ctdadInicial)
				{
					usuario_click = null;
					frmAutorizacion frm2 = new frmAutorizacion();
					frm2.tipoAccion = 2;
					int codPermiso2 = new clsAdmFormulario().getPermisoAumentarPreciodeCompraFacturaGeneradaGR();
					frm2.permiso = codPermiso2;
					frm2.PermitirAdministradores = true;
					frm2.tipoVentanaAAsignarUsuario = 11;
					frm2.ventanaNotaIngreso = this;
					DialogResult dr2 = frm2.ShowDialog();
					if (dr2 != DialogResult.OK || usuario_click == null)
					{
						dgvDetalle.Rows[e.RowIndex].Cells[valorventa.Name].Value = valor_venta;
					}
				}
				recalculaFilaConPrecioUnitarioCambiado2(e.RowIndex);
			}
			if (e.ColumnIndex != dgvDetalle.Columns["precioventa"].Index || nueva_PC == ctdadInicial)
			{
				return;
			}
			if (nueva_PC > comprartotal)
			{
				usuario_click = null;
				frmAutorizacion frm3 = new frmAutorizacion();
				frm3.tipoAccion = 2;
				int codPermiso3 = new clsAdmFormulario().getPermisoAumentarPreciodeCompraFacturaGeneradaGR();
				frm3.permiso = codPermiso3;
				frm3.PermitirAdministradores = false;
				frm3.tipoVentanaAAsignarUsuario = 11;
				frm3.ventanaNotaIngreso = this;
				DialogResult dr3 = frm3.ShowDialog();
				if (dr3 != DialogResult.OK || usuario_click == null)
				{
					dgvDetalle.Rows[e.RowIndex].Cells[precioventa.Name].Value = valor_compra;
				}
			}
			else
			{
				dgvDetalle.Rows[e.RowIndex].Cells[totalinicial.Name].Value = comprartotal;
			}
			recalculaFilaConPrecioUnitarioCambiado1(e.RowIndex);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnModificarFlete_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0)
		{
			frmListadoProductoFlete form = new frmListadoProductoFlete();
			form.data = obtenerDatatableDeDGVParaFletes();
			form.ShowDialog();
			return;
		}
		frmMostrarMensaje form2 = new frmMostrarMensaje();
		form2.Text = "Advertencia de Guardado";
		form2.colorTexto = Color.White;
		form2.textoColor = "Se necesita agregar productos a la lista para poder mostrar flete";
		form2.lblTextoColor.BackColor = Color.Yellow;
		form2.Height -= form2.lblTextoNegro.Height;
		form2.lblTextoNegro.Height = 0;
		form2.Ok = true;
		form2.ShowDialog();
	}

	private DataTable obtenerDatatableDeDGVParaFletes()
	{
		DataTable aux = new DataTable();
		aux.Columns.Add("codProducto");
		aux.Columns.Add("refProducto");
		aux.Columns.Add("descripProducto");
		aux.Columns.Add("codUnidad");
		aux.Columns.Add("unidad");
		aux.Columns.Add("cantidad");
		aux.Columns.Add("flete");
		aux.Columns.Add("subtotal");
		foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
		{
			int codprod = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			string refprod = fila.Cells[referencia.Name].Value.ToString();
			string descprod = fila.Cells[descripcion.Name].Value.ToString();
			int codund = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			string und = fila.Cells[unidad.Name].Value.ToString();
			double ctdad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
			prod = Admpro.CargaProducto(codprod, frmLogin.iCodAlmacen);
			double flet = Admpro.obtenerFleteDeProducto(codprod, 1, codund, 1.0);
			double fletst = Admpro.obtenerFleteDeProducto(codprod, 1, codund, ctdad);
			aux.Rows.Add(codprod, refprod, descprod, codund, und, ctdad, flet, fletst);
		}
		return aux;
	}

	private void btnPreListadodeProductos_Click(object sender, EventArgs e)
	{
		frmPreListadoGRCaFC form = new frmPreListadoGRCaFC();
		form.data = facturagenerada;
		form.ventana = this;
		DialogResult rpta = form.ShowDialog();
		if (rpta == DialogResult.Yes && facturagenerada != null)
		{
			if (facturagenerada.Rows.Count > 0)
			{
				btnGuardar.Enabled = false;
				btnPreListadodeProductos.PerformClick();
			}
			else
			{
				btnGuardar.Enabled = true;
			}
		}
	}

	private void btnAgregarGRC_Click(object sender, EventArgs e)
	{
		frmListadoGuiaRemisionCompraDeOrdenCompra form = new frmListadoGuiaRemisionCompraDeOrdenCompra();
		form.ventana = this;
		form.codOC = grc.ICodOrdenCompra;
		if (form.ShowDialog() != DialogResult.Yes)
		{
			return;
		}
		if (grc_anadir == null)
		{
			MessageBox.Show("Ocurrio un error al intentar cargar la Guia de Remision de Compra.\nIntene nuevamente.", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		List<clsGuiaRemision> grc_existe = Enumerable.Where<clsGuiaRemision>(listadoGRC.AsEnumerable(), (Func<clsGuiaRemision, bool>)((clsGuiaRemision x) => x.CodGuiaRemision == grc_anadir.CodGuiaRemision)).ToList();
		if (grc_existe.Count > 0)
		{
			MessageBox.Show("Guia de Remision de Compra Ya Esta Añadida A Esta Factura.\nIntene nuevamente.", "Error de Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			grc_anadir = null;
			return;
		}
		if (generadaGRC)
		{
			if (admgrc.getEstadoDocumentoRelacionado(Convert.ToInt32(grc_anadir.CodGuiaRemision), 1) == 2)
			{
				DataTable grc_nueva_generada = admgrc.generaFacturaCompraDeGRC(Convert.ToInt32(grc_anadir.CodGuiaRemision), (grc_anadir.OpcionFlete != 2) ? grc_anadir.OpcionFlete : 0);
				foreach (DataRow fila in grc_nueva_generada.Rows)
				{
					facturagenerada.Rows.Add(fila.ItemArray);
				}
				listadoGRC.Add(grc_anadir);
				MessageBox.Show("Factura Compra Agregada Con Exito.\nVerifique los productos en el Listado de Productos", "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Guia de Remision de Compra Ya tiene Asignado una Factura Compra", "Guia De Remision Ya Seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		if (generadaGRCparaFlete)
		{
			if (admgrc.getEstadoDocumentoRelacionado(Convert.ToInt32(grc_anadir.CodGuiaRemision), 4) == 2)
			{
				DataTable grc_nueva_generada2 = admgrc.generaFleteDeGRC(Convert.ToInt32(grc_anadir.CodGuiaRemision));
				CargaDetalleDeGRC(grc_nueva_generada2);
				listadoGRC.Add(grc_anadir);
			}
			else
			{
				MessageBox.Show("Guia de Remision de Compra Ya tiene Asignado una Factura Flete", "Guia De Remision Ya Seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		grc_anadir = null;
	}

	private void dgvDocumentosRelacionados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
		{
			return;
		}
		switch (Convert.ToInt32(dgvDocumentosRelacionados.Rows[e.RowIndex].Cells[colTipoDocRel.Name].Value))
		{
		case 1:
		{
			frmOrdenCompra form3 = buscarFrmOC("frmOrdenCompra", Convert.ToInt32(dgvDocumentosRelacionados.Rows[e.RowIndex].Cells[colCodDoc.Name].Value));
			if (form3 != null)
			{
				form3.Activate();
				break;
			}
			form3 = new frmOrdenCompra();
			form3.MdiParent = base.MdiParent;
			form3.CodOrdenCompra = Convert.ToInt32(dgvDocumentosRelacionados.Rows[e.RowIndex].Cells[colCodDoc.Name].Value);
			form3.Proceso = 3;
			form3.Show();
			break;
		}
		case 2:
		{
			frmGuiaRemisionCompra form2 = buscarFrmGRC("frmGuiaRemisionCompra", Convert.ToInt32(dgvDocumentosRelacionados.Rows[e.RowIndex].Cells[colCodDoc.Name].Value));
			if (form2 != null)
			{
				form2.Activate();
				break;
			}
			form2 = new frmGuiaRemisionCompra();
			form2.Dock = DockStyle.Fill;
			form2.WindowState = FormWindowState.Maximized;
			form2.Editar = true;
			form2.MdiParent = base.MdiParent;
			form2.codGuiaRemisionCompraAEditar = Convert.ToInt32(dgvDocumentosRelacionados.Rows[e.RowIndex].Cells[colCodDoc.Name].Value);
			form2.Show();
			break;
		}
		case 3:
		{
			frmNotadeCreditoCompra form1 = new frmNotadeCreditoCompra();
			form1.MdiParent = base.MdiParent;
			form1.CodNotaCC = Convert.ToInt32(dgvDocumentosRelacionados.Rows[e.RowIndex].Cells[colCodDoc.Name].Value);
			form1.Proceso = 2;
			form1.Show();
			break;
		}
		}
	}

	private frmGuiaRemisionCompra buscarFrmGRC(string tipoFormulario, int codGRC)
	{
		frmGuiaRemisionCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmGuiaRemisionCompra)frm).codGuiaRemisionCompraAEditar == codGRC)
			{
				form = (frmGuiaRemisionCompra)frm;
				break;
			}
		}
		return form;
	}

	private frmOrdenCompra buscarFrmOC(string tipoFormulario, int codOC)
	{
		frmOrdenCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmOrdenCompra)frm).CodOrdenCompra == codOC)
			{
				form = (frmOrdenCompra)frm;
				break;
			}
		}
		return form;
	}

	private void dgvDetalle_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
	{
		if (!generadaGRC)
		{
			return;
		}
		try
		{
			object valor = e.Row.Cells[codDetalleGRC.Name].Value;
			if (valor != null && valor != DBNull.Value)
			{
				List<DataRow> aux = (from x in copiafacturagenerada.AsEnumerable()
					where x.Field<object>("codDetalle").ToString() == valor.ToString()
					select x).ToList();
				if (aux.Count == 1)
				{
					facturagenerada.Rows.Add(aux[0].ItemArray);
					preciosGRC.Remove(valor.ToString());
				}
				else
				{
					e.Cancel = true;
					MessageBox.Show("No se pudo detectar la fila a eliminar.\nError: Se encontraron " + aux.Count + " filas parecidas a la fila a eliminar", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				e.Cancel = true;
				MessageBox.Show("No se pudo detectar la fila a eliminar", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtSerieDocRef_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(txtSerieDocRef.Text) && int.TryParse(txtSerieDocRef.Text, out var numero))
		{
			string numeroConCeros = numero.ToString().PadLeft(3, '0');
			txtSerieDocRef.Text = numeroConCeros;
			txtSerieDocRef.Select(txtSerieDocRef.Text.Length, 0);
		}
	}

	private void txtNDocRef_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(txtNDocRef.Text) && int.TryParse(txtNDocRef.Text, out var numero))
		{
			string numeroConCeros = numero.ToString().PadLeft(8, '0');
			txtNDocRef.Text = numeroConCeros;
			txtNDocRef.Select(txtNDocRef.Text.Length, 0);
		}
	}

	private void txtOrdenCompra_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtFlete_Leave(object sender, EventArgs e)
	{
		if (txtFlete.Text != "" && !(Convert.ToDouble(txtFlete.Text) > 0.0))
		{
		}
	}

	private void RecorreDetalle1()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalle1(row);
		}
	}

	private void añadedetalle1(DataGridViewRow fila)
	{
		try
		{
			decimal fleteprod = default(decimal);
			decimal Subtotal = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
			fleteprod = montoflete / total * Subtotal;
			fila.Cells[flete0.Name].Value = fleteprod.ToString("#.###.####");
			fila.Cells[valorventaconflete.Name].Value = (Convert.ToDecimal(fila.Cells[valorventa.Name].Value) + fleteprod).ToString("###.####");
			fila.Cells[pvconflete.Name].Value = (Convert.ToDecimal(fila.Cells[precioventa.Name].Value) + fleteprod).ToString("###.##");
			calculatotales();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtSerieDocRef_Leave(object sender, EventArgs e)
	{
	}

	private void txtSerieDocRef_KeyPress(object sender, KeyPressEventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotaIngreso));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbUsuario = new System.Windows.Forms.ComboBox();
		this.lblmensaje = new System.Windows.Forms.Label();
		this.lblresponsable = new System.Windows.Forms.Label();
		this.lblareaencargado = new System.Windows.Forms.Label();
		this.cmbarea = new System.Windows.Forms.ComboBox();
		this.btnAgregarGRC = new System.Windows.Forms.Button();
		this.dgvDocumentosRelacionados = new System.Windows.Forms.DataGridView();
		this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDocRelacionado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colTipoDocRel = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnPreListadodeProductos = new System.Windows.Forms.Button();
		this.btnModificarFlete = new System.Windows.Forms.Button();
		this.btnVerificar = new System.Windows.Forms.Button();
		this.txtSerieDocRef = new System.Windows.Forms.TextBox();
		this.txtCodProveedor = new System.Windows.Forms.TextBox();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.cbValorVenta = new System.Windows.Forms.CheckBox();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.lbAutorizado = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.txtAutorizacion = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtOrdenCompra = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtNDocRef = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNombreProv = new System.Windows.Forms.TextBox();
		this.txtCodProv = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.lblCantidadProductos = new System.Windows.Forms.Label();
		this.txtFlete = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
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
		this.valorventaconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.flete0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pvconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.UltimoPrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.EstadoDeLaOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ProductoSolicitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantpendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codDetalleGRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalinicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvDetalle2 = new System.Windows.Forms.DataGridView();
		this.codetord = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.uni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descuento1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descuento2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descuento3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.flete1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.SaldoIngresado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidadPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.SaldoIngresado1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidadPendiente1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Bonificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator9 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator8 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.btVerificar = new DevComponents.DotNetBar.BalloonTip();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentosRelacionados).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cmbUsuario);
		this.groupBox1.Controls.Add(this.lblmensaje);
		this.groupBox1.Controls.Add(this.lblresponsable);
		this.groupBox1.Controls.Add(this.lblareaencargado);
		this.groupBox1.Controls.Add(this.cmbarea);
		this.groupBox1.Controls.Add(this.btnAgregarGRC);
		this.groupBox1.Controls.Add(this.dgvDocumentosRelacionados);
		this.groupBox1.Controls.Add(this.btnPreListadodeProductos);
		this.groupBox1.Controls.Add(this.btnModificarFlete);
		this.groupBox1.Controls.Add(this.btnVerificar);
		this.groupBox1.Controls.Add(this.txtSerieDocRef);
		this.groupBox1.Controls.Add(this.txtCodProveedor);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.cbValorVenta);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.lbAutorizado);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.txtAutorizacion);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtOrdenCompra);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtNumDoc);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtNDocRef);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtNombreProv);
		this.groupBox1.Controls.Add(this.txtCodProv);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.lbNombreTransaccion);
		this.groupBox1.Controls.Add(this.txtTransaccion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1171, 209);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbUsuario.FormattingEnabled = true;
		this.cmbUsuario.Location = new System.Drawing.Point(447, 168);
		this.cmbUsuario.Margin = new System.Windows.Forms.Padding(4);
		this.cmbUsuario.Name = "cmbUsuario";
		this.cmbUsuario.Size = new System.Drawing.Size(250, 24);
		this.cmbUsuario.TabIndex = 118;
		this.cmbUsuario.Visible = false;
		this.lblmensaje.AutoSize = true;
		this.lblmensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblmensaje.ForeColor = System.Drawing.Color.Red;
		this.lblmensaje.Location = new System.Drawing.Point(99, 151);
		this.lblmensaje.Name = "lblmensaje";
		this.lblmensaje.Size = new System.Drawing.Size(157, 13);
		this.lblmensaje.TabIndex = 117;
		this.lblmensaje.Text = "Indicar Motivo del Ajuste *";
		this.lblmensaje.Visible = false;
		this.lblresponsable.AutoSize = true;
		this.lblresponsable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblresponsable.ForeColor = System.Drawing.Color.Red;
		this.lblresponsable.Location = new System.Drawing.Point(448, 151);
		this.lblresponsable.Name = "lblresponsable";
		this.lblresponsable.Size = new System.Drawing.Size(89, 13);
		this.lblresponsable.TabIndex = 116;
		this.lblresponsable.Text = "Responsable *";
		this.lblresponsable.Visible = false;
		this.lblareaencargado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblareaencargado.ForeColor = System.Drawing.Color.Red;
		this.lblareaencargado.Location = new System.Drawing.Point(286, 151);
		this.lblareaencargado.Name = "lblareaencargado";
		this.lblareaencargado.Size = new System.Drawing.Size(117, 18);
		this.lblareaencargado.TabIndex = 114;
		this.lblareaencargado.Text = "Area Encargada* :";
		this.lblareaencargado.Visible = false;
		this.cmbarea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbarea.Cursor = System.Windows.Forms.Cursors.Hand;
		this.cmbarea.FormattingEnabled = true;
		this.cmbarea.Items.AddRange(new object[8] { "ALMACEN", "DESPACHO", "LOGISTICA", "TODOS", "VENTAS", "INGRESAR", "", "" });
		this.cmbarea.Location = new System.Drawing.Point(289, 172);
		this.cmbarea.Name = "cmbarea";
		this.cmbarea.Size = new System.Drawing.Size(151, 21);
		this.cmbarea.TabIndex = 113;
		this.cmbarea.Visible = false;
		this.btnAgregarGRC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAgregarGRC.BackColor = System.Drawing.Color.White;
		this.btnAgregarGRC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnAgregarGRC.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAgregarGRC.Image = SIGEFA.Properties.Resources.agregar;
		this.btnAgregarGRC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAgregarGRC.Location = new System.Drawing.Point(704, 157);
		this.btnAgregarGRC.Name = "btnAgregarGRC";
		this.btnAgregarGRC.Size = new System.Drawing.Size(146, 37);
		this.btnAgregarGRC.TabIndex = 77;
		this.btnAgregarGRC.Text = "Agregar Otra Guia de Remision de Compra";
		this.btnAgregarGRC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAgregarGRC.UseVisualStyleBackColor = false;
		this.btnAgregarGRC.Visible = false;
		this.btnAgregarGRC.Click += new System.EventHandler(btnAgregarGRC_Click);
		this.dgvDocumentosRelacionados.AllowUserToAddRows = false;
		this.dgvDocumentosRelacionados.AllowUserToDeleteRows = false;
		this.dgvDocumentosRelacionados.AllowUserToResizeColumns = false;
		this.dgvDocumentosRelacionados.AllowUserToResizeRows = false;
		this.dgvDocumentosRelacionados.BackgroundColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.dgvDocumentosRelacionados.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentosRelacionados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDocumentosRelacionados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDocumentosRelacionados.Columns.AddRange(this.colItem, this.colCodDoc, this.colDocRelacionado, this.colTipoDocRel, this.colMonto);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDocumentosRelacionados.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvDocumentosRelacionados.Location = new System.Drawing.Point(755, 12);
		this.dgvDocumentosRelacionados.MultiSelect = false;
		this.dgvDocumentosRelacionados.Name = "dgvDocumentosRelacionados";
		this.dgvDocumentosRelacionados.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentosRelacionados.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvDocumentosRelacionados.RowHeadersVisible = false;
		this.dgvDocumentosRelacionados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDocumentosRelacionados.Size = new System.Drawing.Size(237, 103);
		this.dgvDocumentosRelacionados.TabIndex = 76;
		this.dgvDocumentosRelacionados.Visible = false;
		this.dgvDocumentosRelacionados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentosRelacionados_CellDoubleClick);
		this.colItem.DataPropertyName = "nroItem";
		this.colItem.HeaderText = "Item";
		this.colItem.Name = "colItem";
		this.colItem.ReadOnly = true;
		this.colItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colItem.Width = 35;
		this.colCodDoc.DataPropertyName = "codDocRel";
		this.colCodDoc.HeaderText = "colCodDoc";
		this.colCodDoc.Name = "colCodDoc";
		this.colCodDoc.ReadOnly = true;
		this.colCodDoc.Visible = false;
		this.colDocRelacionado.DataPropertyName = "docRel";
		this.colDocRelacionado.HeaderText = "Documento";
		this.colDocRelacionado.Name = "colDocRelacionado";
		this.colDocRelacionado.ReadOnly = true;
		this.colDocRelacionado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colDocRelacionado.Width = 120;
		this.colTipoDocRel.DataPropertyName = "tipoDocRel";
		this.colTipoDocRel.HeaderText = "colTipoDocRel";
		this.colTipoDocRel.Name = "colTipoDocRel";
		this.colTipoDocRel.ReadOnly = true;
		this.colTipoDocRel.Visible = false;
		this.colMonto.DataPropertyName = "monto";
		this.colMonto.HeaderText = "Monto";
		this.colMonto.Name = "colMonto";
		this.colMonto.ReadOnly = true;
		this.colMonto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colMonto.Width = 80;
		this.btnPreListadodeProductos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnPreListadodeProductos.BackColor = System.Drawing.Color.White;
		this.btnPreListadodeProductos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnPreListadodeProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnPreListadodeProductos.Image = SIGEFA.Properties.Resources.agregar;
		this.btnPreListadodeProductos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnPreListadodeProductos.Location = new System.Drawing.Point(1032, 166);
		this.btnPreListadodeProductos.Name = "btnPreListadodeProductos";
		this.btnPreListadodeProductos.Size = new System.Drawing.Size(112, 37);
		this.btnPreListadodeProductos.TabIndex = 75;
		this.btnPreListadodeProductos.Text = "LISTADO DE PRODUCTOS";
		this.btnPreListadodeProductos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnPreListadodeProductos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnPreListadodeProductos.UseVisualStyleBackColor = false;
		this.btnPreListadodeProductos.Visible = false;
		this.btnPreListadodeProductos.Click += new System.EventHandler(btnPreListadodeProductos_Click);
		this.btnModificarFlete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnModificarFlete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnModificarFlete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModificarFlete.Image = SIGEFA.Properties.Resources.ganancia;
		this.btnModificarFlete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnModificarFlete.Location = new System.Drawing.Point(857, 151);
		this.btnModificarFlete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnModificarFlete.Name = "btnModificarFlete";
		this.btnModificarFlete.Size = new System.Drawing.Size(137, 46);
		this.btnModificarFlete.TabIndex = 74;
		this.btnModificarFlete.Text = "Modificar Flete";
		this.btnModificarFlete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnModificarFlete.UseVisualStyleBackColor = true;
		this.btnModificarFlete.Visible = false;
		this.btnModificarFlete.Click += new System.EventHandler(btnModificarFlete_Click);
		this.btVerificar.SetBalloonCaption(this.btnVerificar, "Botón Verificar");
		this.btVerificar.SetBalloonText(this.btnVerificar, "Verifica si ya existe un documento (Orden de Compra, Nota de Ingreso), con los datos ingresados en el formulario");
		this.btnVerificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnVerificar.Image = (System.Drawing.Image)resources.GetObject("btnVerificar.Image");
		this.btnVerificar.Location = new System.Drawing.Point(632, 70);
		this.btnVerificar.Name = "btnVerificar";
		this.btnVerificar.Size = new System.Drawing.Size(117, 55);
		this.btnVerificar.TabIndex = 11;
		this.btnVerificar.Text = "Verificar";
		this.btnVerificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnVerificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnVerificar.UseVisualStyleBackColor = true;
		this.btnVerificar.Visible = false;
		this.btnVerificar.Click += new System.EventHandler(btnVerificar_Click);
		this.txtSerieDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerieDocRef.Location = new System.Drawing.Point(197, 97);
		this.txtSerieDocRef.Name = "txtSerieDocRef";
		this.txtSerieDocRef.Size = new System.Drawing.Size(45, 20);
		this.txtSerieDocRef.TabIndex = 5;
		this.txtSerieDocRef.Tag = "11";
		this.superValidator1.SetValidator3(this.txtSerieDocRef, this.customValidator5);
		this.txtSerieDocRef.Visible = false;
		this.txtSerieDocRef.TextChanged += new System.EventHandler(txtSerieDocRef_TextChanged);
		this.txtSerieDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerieDocRef_KeyPress);
		this.txtSerieDocRef.Leave += new System.EventHandler(txtSerieDocRef_Leave);
		this.txtCodProveedor.Location = new System.Drawing.Point(559, 46);
		this.txtCodProveedor.Name = "txtCodProveedor";
		this.txtCodProveedor.Size = new System.Drawing.Size(67, 20);
		this.txtCodProveedor.TabIndex = 45;
		this.txtCodProveedor.Visible = false;
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(196, 70);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(357, 20);
		this.txtNombreCliente.TabIndex = 9;
		this.txtNombreCliente.Tag = "2";
		this.txtNombreCliente.Visible = false;
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Location = new System.Drawing.Point(102, 70);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(88, 20);
		this.txtCodCliente.TabIndex = 3;
		this.txtCodCliente.Tag = "1";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator3(this.txtCodCliente, this.customValidator3);
		this.txtCodCliente.Visible = false;
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(17, 73);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(39, 13);
		this.label18.TabIndex = 44;
		this.label18.Tag = "1";
		this.label18.Text = "Cliente";
		this.label18.Visible = false;
		this.cbValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cbValorVenta.AutoSize = true;
		this.cbValorVenta.Location = new System.Drawing.Point(1078, 98);
		this.cbValorVenta.Name = "cbValorVenta";
		this.cbValorVenta.Size = new System.Drawing.Size(81, 17);
		this.cbValorVenta.TabIndex = 20;
		this.cbValorVenta.Text = "Valor Venta";
		this.cbValorVenta.UseVisualStyleBackColor = true;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(248, 122);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 8;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(102, 122);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(140, 20);
		this.cmbFormaPago.TabIndex = 7;
		this.cmbFormaPago.Tag = "16";
		this.superValidator1.SetValidator3(this.cmbFormaPago, this.customValidator7);
		this.cmbFormaPago.Visible = false;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(17, 125);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(79, 13);
		this.label17.TabIndex = 41;
		this.label17.Tag = "16";
		this.label17.Text = "Forma de Pago";
		this.label17.Visible = false;
		this.lbAutorizado.AutoSize = true;
		this.lbAutorizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbAutorizado.Location = new System.Drawing.Point(559, 125);
		this.lbAutorizado.Name = "lbAutorizado";
		this.lbAutorizado.Size = new System.Drawing.Size(67, 13);
		this.lbAutorizado.TabIndex = 39;
		this.lbAutorizado.Tag = "22";
		this.lbAutorizado.Text = "Autorizado";
		this.lbAutorizado.Visible = false;
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Location = new System.Drawing.Point(1078, 70);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 5;
		this.txtTipoCambio.Tag = "15";
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtTipoCambio.Visible = false;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(1054, 43);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(105, 21);
		this.cmbMoneda.TabIndex = 19;
		this.superValidator1.SetValidator1(this.cmbMoneda, this.customValidator9);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(998, 73);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 21;
		this.label16.Tag = "15";
		this.label16.Text = "Tipo/Cambio :";
		this.label16.Visible = false;
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(998, 46);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(52, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Moneda :";
		this.txtAutorizacion.Location = new System.Drawing.Point(478, 121);
		this.txtAutorizacion.Name = "txtAutorizacion";
		this.txtAutorizacion.Size = new System.Drawing.Size(75, 20);
		this.txtAutorizacion.TabIndex = 9;
		this.txtAutorizacion.Tag = "22";
		this.superValidator1.SetValidator3(this.txtAutorizacion, this.customValidator6);
		this.txtAutorizacion.Visible = false;
		this.txtAutorizacion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtAutorizacion_KeyDown);
		this.txtAutorizacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtAutorizacion_KeyPress);
		this.txtAutorizacion.Leave += new System.EventHandler(txtAutorizacion_Leave);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(404, 124);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 13);
		this.label3.TabIndex = 19;
		this.label3.Tag = "22";
		this.label3.Text = "Autorizacion:";
		this.label3.Visible = false;
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDetalle.Image = (System.Drawing.Image)resources.GetObject("btnDetalle.Image");
		this.btnDetalle.Location = new System.Drawing.Point(1001, 121);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(158, 39);
		this.btnDetalle.TabIndex = 12;
		this.btnDetalle.Text = "AGREGAR PRODUCTO";
		this.btnDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Visible = false;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(102, 171);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(178, 32);
		this.txtComentario.TabIndex = 10;
		this.txtComentario.Tag = "21";
		this.txtComentario.Visible = false;
		this.txtComentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComentario_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(17, 151);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(34, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Glosa";
		this.label9.Visible = false;
		this.txtOrdenCompra.Location = new System.Drawing.Point(478, 96);
		this.txtOrdenCompra.Name = "txtOrdenCompra";
		this.txtOrdenCompra.ReadOnly = true;
		this.txtOrdenCompra.Size = new System.Drawing.Size(75, 20);
		this.txtOrdenCompra.TabIndex = 4;
		this.txtOrdenCompra.Tag = "18";
		this.superValidator1.SetValidator1(this.txtOrdenCompra, this.customValidator8);
		this.txtOrdenCompra.Visible = false;
		this.txtOrdenCompra.KeyDown += new System.Windows.Forms.KeyEventHandler(txtOrdenCompra_KeyDown);
		this.txtOrdenCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtOrdenCompra_KeyPress);
		this.txtOrdenCompra.Leave += new System.EventHandler(txtOrdenCompra_Leave);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(394, 100);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(78, 13);
		this.label8.TabIndex = 15;
		this.label8.Tag = "18";
		this.label8.Text = "Orden Compra:";
		this.label8.Visible = false;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Location = new System.Drawing.Point(464, 17);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 20);
		this.txtNumDoc.TabIndex = 2;
		this.txtNumDoc.Visible = false;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(400, 20);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.label7.Visible = false;
		this.txtNDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNDocRef.Location = new System.Drawing.Point(248, 97);
		this.txtNDocRef.Name = "txtNDocRef";
		this.txtNDocRef.Size = new System.Drawing.Size(89, 20);
		this.txtNDocRef.TabIndex = 6;
		this.txtNDocRef.Tag = "11";
		this.superValidator1.SetValidator3(this.txtNDocRef, this.customValidator5);
		this.txtNDocRef.Visible = false;
		this.txtNDocRef.TextChanged += new System.EventHandler(txtNDocRef_TextChanged);
		this.txtNDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNDocRef_KeyPress);
		this.txtNDocRef.Leave += new System.EventHandler(txtNDocRef_Leave);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(136, 99);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(55, 13);
		this.label6.TabIndex = 2;
		this.label6.Tag = "11";
		this.label6.Text = "Num. Ref.";
		this.label6.Visible = false;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(102, 96);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 4;
		this.txtDocRef.Tag = "10";
		this.superValidator1.SetValidator3(this.txtDocRef, this.customValidator4);
		this.txtDocRef.Visible = false;
		this.txtDocRef.TextChanged += new System.EventHandler(txtDocRef_TextChanged);
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.txtDocRef.Leave += new System.EventHandler(txtDocRef_Leave);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 100);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.label5.Visible = false;
		this.txtNombreProv.Enabled = false;
		this.txtNombreProv.Location = new System.Drawing.Point(197, 44);
		this.txtNombreProv.Name = "txtNombreProv";
		this.txtNombreProv.ReadOnly = true;
		this.txtNombreProv.Size = new System.Drawing.Size(356, 20);
		this.txtNombreProv.TabIndex = 7;
		this.txtNombreProv.Tag = "9";
		this.txtNombreProv.Visible = false;
		this.txtCodProv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodProv.Location = new System.Drawing.Point(102, 44);
		this.txtCodProv.Name = "txtCodProv";
		this.txtCodProv.Size = new System.Drawing.Size(89, 20);
		this.txtCodProv.TabIndex = 2;
		this.txtCodProv.Tag = "8";
		this.superValidator1.SetValidator3(this.txtCodProv, this.customValidator2);
		this.txtCodProv.Visible = false;
		this.txtCodProv.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodProv_KeyDown);
		this.txtCodProv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodProv_KeyPress);
		this.txtCodProv.Leave += new System.EventHandler(txtCodProv_Leave);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 47);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(56, 13);
		this.label4.TabIndex = 5;
		this.label4.Tag = "8";
		this.label4.Text = "Proveedor";
		this.label4.Visible = false;
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(136, 20);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(258, 15);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.lbNombreTransaccion.Visible = false;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Location = new System.Drawing.Point(102, 17);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.Size = new System.Drawing.Size(28, 20);
		this.txtTransaccion.TabIndex = 1;
		this.superValidator1.SetValidator3(this.txtTransaccion, this.customValidator1);
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(1078, 17);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 18;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(1029, 20);
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
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 532);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1171, 48);
		this.groupBox3.TabIndex = 19;
		this.groupBox3.TabStop = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1087, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(78, 32);
		this.btnSalir.TabIndex = 14;
		this.btnSalir.Text = "SALIR";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 6);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 15;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Visible = false;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.Location = new System.Drawing.Point(979, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(102, 32);
		this.btnGuardar.TabIndex = 13;
		this.btnGuardar.Text = "GUARDAR";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 6);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 16;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Visible = false;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnEliminar.Image = (System.Drawing.Image)resources.GetObject("btnEliminar.Image");
		this.btnEliminar.Location = new System.Drawing.Point(155, 6);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(169, 32);
		this.btnEliminar.TabIndex = 17;
		this.btnEliminar.Text = "QUITAR PRODUCTO";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.lblCantidadProductos);
		this.groupBox2.Controls.Add(this.txtFlete);
		this.groupBox2.Controls.Add(this.label19);
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
		this.groupBox2.Controls.Add(this.dgvDetalle2);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 209);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1171, 323);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.lblCantidadProductos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblCantidadProductos.AutoSize = true;
		this.lblCantidadProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadProductos.Location = new System.Drawing.Point(6, 249);
		this.lblCantidadProductos.Name = "lblCantidadProductos";
		this.lblCantidadProductos.Size = new System.Drawing.Size(175, 16);
		this.lblCantidadProductos.TabIndex = 69;
		this.lblCantidadProductos.Text = "Productos Agregados: 0";
		this.txtFlete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFlete.Location = new System.Drawing.Point(856, 245);
		this.txtFlete.Name = "txtFlete";
		this.txtFlete.Size = new System.Drawing.Size(75, 20);
		this.txtFlete.TabIndex = 23;
		this.txtFlete.Tag = "7";
		this.txtFlete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPDescuento_KeyPress);
		this.txtFlete.Leave += new System.EventHandler(txtFlete_Leave);
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(817, 247);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(33, 13);
		this.label19.TabIndex = 66;
		this.label19.Tag = "7";
		this.label19.Text = "Flete:";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(545, 245);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 21;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioVenta.Location = new System.Drawing.Point(1054, 297);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(961, 300);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(64, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(1054, 271);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(961, 274);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(717, 245);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 22;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(1054, 245);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(961, 248);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(646, 248);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(501, 248);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.moneda, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.valorventaconflete, this.flete0, this.igv, this.precioventa, this.pvconflete, this.precioreal, this.valoreal, this.fechaingreso, this.coduser, this.fecharegistro, this.UltimoPrecioCompra, this.EstadoDeLaOrden, this.ProductoSolicitado, this.cantpendiente, this.codDetalleGRC, this.stado, this.totalinicial);
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1165, 223);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvDetalle_CellBeginEdit);
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEnter);
		this.dgvDetalle.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellLeave);
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.dgvDetalle.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(dgvDetalle_UserDeletingRow);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "Código de Producto";
		this.codproducto.Name = "codproducto";
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Código de Producto";
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
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		dataGridViewCellStyle4.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle4;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N4";
		dataGridViewCellStyle5.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle5;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N4";
		dataGridViewCellStyle6.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle6;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N4";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle7;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N4";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle8;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N4";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle9;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N4";
		dataGridViewCellStyle10.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle10;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "N4";
		dataGridViewCellStyle11.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle11;
		this.valorventa.HeaderText = "V. Compra";
		this.valorventa.Name = "valorventa";
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventaconflete.DataPropertyName = "vvconflete";
		this.valorventaconflete.HeaderText = "vvconflete";
		this.valorventaconflete.Name = "valorventaconflete";
		this.valorventaconflete.Visible = false;
		this.flete0.DataPropertyName = "flete";
		this.flete0.HeaderText = "Flete";
		this.flete0.Name = "flete0";
		this.flete0.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle12.Format = "N4";
		this.igv.DefaultCellStyle = dataGridViewCellStyle12;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle13.Format = "N4";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle13;
		this.precioventa.HeaderText = "P. Compra";
		this.precioventa.Name = "precioventa";
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.pvconflete.DataPropertyName = "pvconflete";
		this.pvconflete.HeaderText = "pvconflete";
		this.pvconflete.Name = "pvconflete";
		this.pvconflete.Visible = false;
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
		this.fechaingreso.DataPropertyName = "fechaingreso";
		this.fechaingreso.HeaderText = "FechaIngre";
		this.fechaingreso.Name = "fechaingreso";
		this.fechaingreso.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.UltimoPrecioCompra.DataPropertyName = "UltimoPrecioCompra";
		this.UltimoPrecioCompra.HeaderText = "Ultimo Precio de Compra";
		this.UltimoPrecioCompra.Name = "UltimoPrecioCompra";
		this.UltimoPrecioCompra.Visible = false;
		this.EstadoDeLaOrden.DataPropertyName = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.HeaderText = "EstadoOrden";
		this.EstadoDeLaOrden.Name = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.Visible = false;
		this.ProductoSolicitado.DataPropertyName = "ProductoSolicitado";
		this.ProductoSolicitado.HeaderText = "ProductoSolicitado";
		this.ProductoSolicitado.Name = "ProductoSolicitado";
		this.ProductoSolicitado.Visible = false;
		this.cantpendiente.DataPropertyName = "cantpendiente";
		this.cantpendiente.HeaderText = "cantpendiente";
		this.cantpendiente.Name = "cantpendiente";
		this.cantpendiente.Visible = false;
		this.codDetalleGRC.HeaderText = "codDetalleGRC";
		this.codDetalleGRC.Name = "codDetalleGRC";
		this.codDetalleGRC.Visible = false;
		this.stado.DataPropertyName = "stado";
		this.stado.HeaderText = "stado";
		this.stado.Name = "stado";
		this.totalinicial.DataPropertyName = "totalinicial";
		this.totalinicial.HeaderText = "totalinicial";
		this.totalinicial.Name = "totalinicial";
		this.totalinicial.Visible = false;
		this.dgvDetalle2.AllowUserToAddRows = false;
		this.dgvDetalle2.AllowUserToOrderColumns = true;
		this.dgvDetalle2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle2.Columns.AddRange(this.codetord, this.coprod, this.codref, this.desc, this.uni, this.coduni, this.cant, this.stock, this.cantn, this.preciounitario, this.subtotal, this.descuento1, this.descuento2, this.descuento3, this.montodscto1, this.valorventa1, this.igv1, this.importe1, this.precioreal1, this.valoreal1, this.flete1, this.SaldoIngresado, this.cantidadPendiente, this.SaldoIngresado1, this.cantidadPendiente1, this.Bonificacion);
		this.dgvDetalle2.Location = new System.Drawing.Point(3, 19);
		this.dgvDetalle2.Name = "dgvDetalle2";
		this.dgvDetalle2.RowHeadersVisible = false;
		this.dgvDetalle2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle2.Size = new System.Drawing.Size(1162, 220);
		this.dgvDetalle2.TabIndex = 68;
		this.dgvDetalle2.Visible = false;
		this.dgvDetalle2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle2_CellClick);
		this.dgvDetalle2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle2_CellEndEdit);
		this.dgvDetalle2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle2_CellValueChanged);
		this.dgvDetalle2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle2_EditingControlShowing);
		this.dgvDetalle2.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle2_RowsAdded);
		this.dgvDetalle2.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle2_RowsRemoved);
		this.dgvDetalle2.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle2_RowStateChanged);
		this.dgvDetalle2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvDetalle2_KeyPress);
		this.dgvDetalle2.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvDetalle2_KeyUp);
		this.codetord.DataPropertyName = "codDetalleOrden";
		this.codetord.HeaderText = "codDetalle";
		this.codetord.Name = "codetord";
		this.codetord.Visible = false;
		this.coprod.DataPropertyName = "codProducto";
		this.coprod.HeaderText = "codProducto";
		this.coprod.Name = "coprod";
		this.coprod.ReadOnly = true;
		this.coprod.Visible = false;
		this.codref.DataPropertyName = "referencia";
		this.codref.HeaderText = "Referencia";
		this.codref.Name = "codref";
		this.codref.ReadOnly = true;
		this.codref.Width = 150;
		this.desc.DataPropertyName = "producto";
		this.desc.HeaderText = "Descripcion";
		this.desc.Name = "desc";
		this.desc.ReadOnly = true;
		this.desc.Width = 300;
		this.uni.DataPropertyName = "unidad";
		this.uni.HeaderText = "Unidad";
		this.uni.Name = "uni";
		this.uni.ReadOnly = true;
		this.coduni.DataPropertyName = "codUnidadMedida";
		this.coduni.HeaderText = "codUnidad";
		this.coduni.Name = "coduni";
		this.coduni.Visible = false;
		this.cant.DataPropertyName = "cantidad";
		dataGridViewCellStyle14.Format = "N2";
		dataGridViewCellStyle14.NullValue = null;
		this.cant.DefaultCellStyle = dataGridViewCellStyle14;
		this.cant.HeaderText = "Q Original";
		this.cant.Name = "cant";
		this.cant.ReadOnly = true;
		this.stock.DataPropertyName = "codControlStock";
		dataGridViewCellStyle15.Format = "N2";
		dataGridViewCellStyle15.NullValue = null;
		this.stock.DefaultCellStyle = dataGridViewCellStyle15;
		this.stock.HeaderText = "Stock";
		this.stock.Name = "stock";
		this.stock.Visible = false;
		this.cantn.DataPropertyName = "cantidadn";
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.cantn.DefaultCellStyle = dataGridViewCellStyle16;
		this.cantn.HeaderText = "Q Ingreso";
		this.cantn.MaxInputLength = 11;
		this.cantn.Name = "cantn";
		this.preciounitario.DataPropertyName = "preciounitario";
		this.preciounitario.HeaderText = "preciounitario";
		this.preciounitario.Name = "preciounitario";
		this.preciounitario.Visible = false;
		this.subtotal.DataPropertyName = "subtotal";
		dataGridViewCellStyle17.Format = "N4";
		dataGridViewCellStyle17.NullValue = null;
		this.subtotal.DefaultCellStyle = dataGridViewCellStyle17;
		this.subtotal.HeaderText = "subtotal";
		this.subtotal.Name = "subtotal";
		this.subtotal.Visible = false;
		this.descuento1.DataPropertyName = "descuento1";
		this.descuento1.HeaderText = "descuento1";
		this.descuento1.Name = "descuento1";
		this.descuento1.Visible = false;
		this.descuento2.DataPropertyName = "descuento2";
		this.descuento2.HeaderText = "descuento2";
		this.descuento2.Name = "descuento2";
		this.descuento2.Visible = false;
		this.descuento3.DataPropertyName = "descuento3";
		this.descuento3.HeaderText = "descuento3";
		this.descuento3.Name = "descuento3";
		this.descuento3.Visible = false;
		this.montodscto1.DataPropertyName = "montodscto1";
		this.montodscto1.HeaderText = "montodscto1";
		this.montodscto1.Name = "montodscto1";
		this.montodscto1.Visible = false;
		this.valorventa1.DataPropertyName = "valorventa1";
		dataGridViewCellStyle18.Format = "N4";
		dataGridViewCellStyle18.NullValue = null;
		this.valorventa1.DefaultCellStyle = dataGridViewCellStyle18;
		this.valorventa1.HeaderText = "valorventa1";
		this.valorventa1.Name = "valorventa1";
		this.valorventa1.Visible = false;
		this.igv1.DataPropertyName = "igv1";
		dataGridViewCellStyle19.Format = "N4";
		dataGridViewCellStyle19.NullValue = null;
		this.igv1.DefaultCellStyle = dataGridViewCellStyle19;
		this.igv1.HeaderText = "igv1";
		this.igv1.Name = "igv1";
		this.igv1.Visible = false;
		this.importe1.DataPropertyName = "importe1";
		dataGridViewCellStyle20.Format = "N4";
		dataGridViewCellStyle20.NullValue = null;
		this.importe1.DefaultCellStyle = dataGridViewCellStyle20;
		this.importe1.HeaderText = "importe1";
		this.importe1.Name = "importe1";
		this.importe1.Visible = false;
		this.precioreal1.DataPropertyName = "precioreal1";
		dataGridViewCellStyle21.Format = "N4";
		dataGridViewCellStyle21.NullValue = null;
		this.precioreal1.DefaultCellStyle = dataGridViewCellStyle21;
		this.precioreal1.HeaderText = "precioreal1";
		this.precioreal1.Name = "precioreal1";
		this.precioreal1.Visible = false;
		this.valoreal1.DataPropertyName = "valoreal1";
		dataGridViewCellStyle22.Format = "N4";
		dataGridViewCellStyle22.NullValue = null;
		this.valoreal1.DefaultCellStyle = dataGridViewCellStyle22;
		this.valoreal1.HeaderText = "valoreal1";
		this.valoreal1.Name = "valoreal1";
		this.valoreal1.Visible = false;
		this.flete1.DataPropertyName = "flete1";
		dataGridViewCellStyle23.Format = "N4";
		dataGridViewCellStyle23.NullValue = null;
		this.flete1.DefaultCellStyle = dataGridViewCellStyle23;
		this.flete1.HeaderText = "flete1";
		this.flete1.Name = "flete1";
		this.flete1.Visible = false;
		this.SaldoIngresado.DataPropertyName = "SaldoIngresado";
		dataGridViewCellStyle24.Format = "N2";
		dataGridViewCellStyle24.NullValue = null;
		this.SaldoIngresado.DefaultCellStyle = dataGridViewCellStyle24;
		this.SaldoIngresado.HeaderText = "Q Acumulada";
		this.SaldoIngresado.MaxInputLength = 11;
		this.SaldoIngresado.Name = "SaldoIngresado";
		this.SaldoIngresado.ReadOnly = true;
		this.cantidadPendiente.DataPropertyName = "cantidadPendiente";
		dataGridViewCellStyle25.Format = "N2";
		dataGridViewCellStyle25.NullValue = null;
		this.cantidadPendiente.DefaultCellStyle = dataGridViewCellStyle25;
		this.cantidadPendiente.HeaderText = "Q Por Atender";
		this.cantidadPendiente.Name = "cantidadPendiente";
		this.cantidadPendiente.ReadOnly = true;
		this.SaldoIngresado1.DataPropertyName = "SaldoIngresado1";
		dataGridViewCellStyle26.Format = "N2";
		dataGridViewCellStyle26.NullValue = null;
		this.SaldoIngresado1.DefaultCellStyle = dataGridViewCellStyle26;
		this.SaldoIngresado1.HeaderText = "SaldoIngresado1";
		this.SaldoIngresado1.Name = "SaldoIngresado1";
		this.SaldoIngresado1.Visible = false;
		this.cantidadPendiente1.DataPropertyName = "cantidadPendiente1";
		dataGridViewCellStyle27.Format = "N2";
		dataGridViewCellStyle27.NullValue = null;
		this.cantidadPendiente1.DefaultCellStyle = dataGridViewCellStyle27;
		this.cantidadPendiente1.HeaderText = "cantidadPendiente1";
		this.cantidadPendiente1.Name = "cantidadPendiente1";
		this.cantidadPendiente1.Visible = false;
		this.Bonificacion.DataPropertyName = "Bonificacion";
		dataGridViewCellStyle28.NullValue = "0";
		this.Bonificacion.DefaultCellStyle = dataGridViewCellStyle28;
		this.Bonificacion.HeaderText = "Bonificacion";
		this.Bonificacion.Name = "Bonificacion";
		this.Bonificacion.Visible = false;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator5.ErrorMessage = "Ingrese el numero de documento.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator3.ErrorMessage = "Escoja un cliente.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator7.ErrorMessage = "Escoja la forma de pago.";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator9.ErrorMessage = "Moneda.";
		this.customValidator9.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator9.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator9_ValidateValue);
		this.customValidator6.ErrorMessage = "Complete el campo requerido.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator8.ErrorMessage = "Ingrese Orden Compra";
		this.customValidator8.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator8.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator8_ValidateValue);
		this.customValidator4.ErrorMessage = "Escoja un documento.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator2.ErrorMessage = "Escoja un proveedor.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Escoja la Transaccion.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.btVerificar.InitialDelay = 0;
		this.btVerificar.MinimumBalloonWidth = 250;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1171, 580);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmNotaIngreso";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Nota de Ingreso";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotaIngreso_Load);
		base.Shown += new System.EventHandler(frmNotaIngreso_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentosRelacionados).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
