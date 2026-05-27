using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmConsultorExt : Office2007Form
{
	private clsFacturaVenta fv = new clsFacturaVenta();

	private clsAdmProducto AdmProd = new clsAdmProducto();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsProveedor prov = new clsProveedor();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmAutorizado AdmAut = new clsAdmAutorizado();

	private clsAutorizado aut = new clsAutorizado();

	private clsAdmNotaSalida AdmNota = new clsAdmNotaSalida();

	private clsNotaSalida nota = new clsNotaSalida();

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsValidar ok = new clsValidar();

	private clsAdmListaPrecio admLista = new clsAdmListaPrecio();

	private clsListaPrecio Listap = new clsListaPrecio();

	private clsAdmCotizacion AdmCot = new clsAdmCotizacion();

	private clsCotizacion coti = new clsCotizacion();

	private int CodLista = 1;

	public List<int> listNotas = new List<int>();

	public List<int> config = new List<int>();

	public List<clsDetallePedido> detalle = new List<clsDetallePedido>();

	public string CodPedido;

	public int CodCotizacion;

	public int CodProveedor;

	public int CodCliente;

	public int CodDocumento;

	public int CodAutorizado;

	public int Tipo;

	private bool Validacion = true;

	public int Proceso = 0;

	private clsConsultasExternas ext = new clsConsultasExternas();

	public int CodVendedor;

	private IContainer components = null;

	private GroupBox groupBox1;

	public TextBox txtDireccion;

	private TextBox txtPedido;

	private Label label8;

	private TextBox txtTipoCambio;

	private ComboBox cmbMoneda;

	private Label label16;

	private Label label17;

	private Label label15;

	private TextBox txtComentario;

	private Label label9;

	private Label label5;

	private DateTimePicker dtpFecha;

	private Label label1;

	private GroupBox groupBox4;

	private Button btnSalir;

	private Button btnGuardar;

	private GroupBox groupBox2;

	private Button btnNuevo;

	private Button btnEditar;

	private Button btnEliminar;

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

	private ImageList imageList1;

	public TextBox txtDocRef;

	private Label lbDocumento;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private Button btnNewPedido;

	private DateTimePicker dtpFechaEntrega;

	private Label label2;

	private DateTimePicker dtpFechaPago;

	private ComboBox cmbFormaPago;

	private Label label3;

	private ComboBox cbListaPrecios;

	private Label label26;

	public TextBox txtCotizacion;

	private Label label4;

	public TextBox txtNombreCliente;

	public TextBox txtCodCliente;

	private Button button2;

	private Button button1;

	private Panel panel1;

	private TextBox textBox1;

	private Label label6;

	public DataGridView dgvDetalle;

	private Button button3;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn cantidadvend;

	private DataGridViewTextBoxColumn cantidaddev;

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

	private DataGridViewTextBoxColumn importevend;

	private DataGridViewTextBoxColumn importedev;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	public frmConsultorExt()
	{
		InitializeComponent();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDetalleSalida"] != null)
		{
			Application.OpenForms["frmDetalleSalida"].Activate();
			return;
		}
		frmDetalleSalida form = new frmDetalleSalida();
		form.Procede = 41;
		form.Proceso = 1;
		form.Moneda = 1;
		form.consultorext = false;
		form.Codlista = CodLista;
		form.ShowDialog();
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			dgvDetalle.Rows.Remove(dgvDetalle.SelectedRows[0]);
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (!((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0)))
		{
			return;
		}
		DataGridViewRow row = dgvDetalle.SelectedRows[0];
		if (Application.OpenForms["frmDetalleSalida"] != null)
		{
			Application.OpenForms["frmDetalleSalida"].Activate();
			return;
		}
		frmDetalleSalida form = new frmDetalleSalida();
		if (Proceso == 1)
		{
			form.Proceso = 2;
		}
		if (Proceso == 2)
		{
			form.Proceso = 3;
			form.codDetalle = int.Parse(row.Cells[coddetalle.Name].Value.ToString());
		}
		form.Procede = 41;
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

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		txtCodCliente.Text = cli.CodigoPersonalizado;
		txtNombreCliente.Text = cli.RazonSocial;
		txtDireccion.Text = cli.DireccionLegal;
		cbListaPrecios.SelectedValue = cli.CodListaPrecio;
		if (cli.CodListaPrecio != 0)
		{
			EventArgs ee = new EventArgs();
			cbListaPrecios_SelectionChangeCommitted(cbListaPrecios, ee);
		}
		else
		{
			CodLista = 0;
		}
	}

	private bool BuscaCliente()
	{
		cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
		if (cli != null)
		{
			txtNombreCliente.Text = cli.RazonSocial;
			CodCliente = cli.CodCliente;
			txtDireccion.Text = cli.DireccionLegal;
			cbListaPrecios.SelectedValue = cli.CodListaPrecio;
			return true;
		}
		txtNombreCliente.Text = "";
		CodCliente = 0;
		txtDireccion.Text = "";
		cbListaPrecios.SelectedIndex = -1;
		return false;
	}

	private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmListaVendedores"] != null)
		{
			Application.OpenForms["frmListaVendedores"].Activate();
			return;
		}
		frmListaVendedores form = new frmListaVendedores();
		form.ShowDialog();
		if (txtCodCliente.Text != "" && txtNombreCliente.Text != "")
		{
			btnGuardar.Enabled = true;
			txtComentario.Focus();
		}
		txtComentario.Focus();
	}

	private void txtCodCliente_Leave(object sender, EventArgs e)
	{
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		Console.WriteLine("x");
		if (!txtTipoCambio.Visible)
		{
			return;
		}
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, Convert.ToInt32(cmbMoneda.SelectedValue));
		if (Proceso == 1)
		{
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
			lbDocumento.Text = doc.Descripcion;
			lbDocumento.Visible = true;
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
		form.Proceso = 3;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtDocRef.Text = doc.Sigla;
		lbDocumento.Text = doc.Descripcion;
		lbDocumento.Visible = true;
		if (CodDocumento != 0)
		{
			ProcessTabKey(forward: true);
		}
	}

	private void VerificarCabecera()
	{
		Validacion = true;
		if (CodDocumento == 0)
		{
			Validacion = false;
		}
		if (txtCodCliente.Visible && CodCliente == 0)
		{
			Validacion = false;
		}
		if (Validacion && Proceso == 1)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void sololectura(bool estado)
	{
		dtpFecha.Enabled = !estado;
		dtpFechaEntrega.Enabled = !estado;
		cbListaPrecios.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		txtCotizacion.Enabled = !estado;
		cmbMoneda.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtPedido.ReadOnly = estado;
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
		btnNewPedido.Visible = estado;
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmPedido.CargaDetalleEntrega(Convert.ToInt32(pedido.CodPedido));
	}

	private void txtDocRef_Leave(object sender, EventArgs e)
	{
		BuscaTipoDocumento();
		if (CodDocumento == 0)
		{
			txtDocRef.Focus();
		}
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(0);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 2;
	}

	private void frmConsultorExt_Load(object sender, EventArgs e)
	{
		cmbMoneda.SelectedIndex = 0;
		CargaListaPrecios();
		CargaFormaPagos();
		cbListaPrecios.SelectedIndex = 0;
		dtpFecha.MaxDate = DateTime.Today.Date;
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, Convert.ToInt32(cmbMoneda.SelectedValue));
		if (Proceso == 1)
		{
			button3.Visible = false;
		}
		else if (Proceso == 2)
		{
			CargaPedido();
			button1.Visible = false;
			label6.Visible = true;
			textBox1.Visible = true;
		}
		else if (Proceso == 3)
		{
			CargaPedido();
			sololectura(estado: true);
			dgvDetalle.Columns["montodscto"].Visible = false;
			dgvDetalle.Columns["valorventa"].Visible = false;
			dgvDetalle.Columns["igv"].Visible = false;
			dgvDetalle.Columns["precioventa"].Visible = false;
			dgvDetalle.Columns["cantidadvend"].Visible = true;
			dgvDetalle.Columns["cantidaddev"].Visible = true;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			btnNewPedido.Visible = false;
			label6.Visible = true;
			textBox1.Visible = true;
			panel1.Visible = false;
		}
		else if (Proceso == 4)
		{
			CargaPedido();
			sololectura(estado: true);
			btnNewPedido.Visible = false;
			label6.Visible = true;
			textBox1.Visible = true;
			panel1.Visible = false;
			dgvDetalle.Columns["montodscto"].Visible = false;
			dgvDetalle.Columns["valorventa"].Visible = false;
			dgvDetalle.Columns["igv"].Visible = false;
			dgvDetalle.Columns["precioventa"].Visible = false;
			dgvDetalle.Columns["cantidadvend"].Visible = true;
			dgvDetalle.Columns["cantidaddev"].Visible = true;
		}
		txtDocRef.Text = "SD";
	}

	private void CargaListaPrecios()
	{
		cbListaPrecios.DataSource = admLista.MuestraListas(frmLogin.iCodAlmacen);
		cbListaPrecios.DisplayMember = "nombre";
		cbListaPrecios.ValueMember = "codListaPrecio";
		cbListaPrecios.SelectedIndex = -1;
	}

	private void frmConsultorExt_Shown(object sender, EventArgs e)
	{
		btnNuevo.Focus();
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

	private void CargaPedido()
	{
		try
		{
			pedido = AdmPedido.CargaEntrega(Convert.ToInt32(CodPedido));
			if (pedido != null)
			{
				txtPedido.Text = pedido.CodPedido;
				if (pedido.CodCotizacion != 0)
				{
					coti = AdmCot.CargaCotizacion(pedido.CodCotizacion, frmLogin.iCodAlmacen);
					txtCotizacion.Text = coti.CodCotizacion;
				}
				if (txtCodCliente.Enabled)
				{
					CodVendedor = pedido.CodCliente;
					txtCodCliente.Text = pedido.CodigoPersonalizado;
					txtNombreCliente.Text = pedido.Nombre;
					txtDireccion.Text = pedido.Direccion;
				}
				dtpFecha.Value = pedido.FechaPedido;
				cmbMoneda.SelectedIndex = pedido.Moneda;
				txtTipoCambio.Text = pedido.TipoCambio.ToString();
				cbListaPrecios.SelectedValue = pedido.CodListaPrecio;
				if (txtDocRef.Enabled)
				{
					CodDocumento = pedido.CodTipoDocumento;
					txtDocRef.Text = pedido.SiglaDocumento;
					lbDocumento.Text = pedido.DescripcionDocumento;
				}
				txtComentario.Text = pedido.Comentario;
				txtBruto.Text = $"{pedido.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{pedido.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{pedido.Total - pedido.Igv:#,##0.00}";
				txtIGV.Text = $"{pedido.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{pedido.Total:#,##0.00}";
				textBox1.Text = pedido.SEntregado;
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

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1)
		{
			actualizaimportes();
		}
	}

	private void actualizaimportes()
	{
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{bruto - descuen - valor:#,##0.00}";
		txtPrecioVenta.Text = $"{bruto - descuen:#,##0.00}";
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0)
		{
			return;
		}
		pedido.CodAlmacen = frmLogin.iCodAlmacen;
		pedido.CodCliente = CodVendedor;
		pedido.CodTipoDocumento = 46;
		pedido.CodCotizacion = CodCotizacion;
		pedido.Moneda = cmbMoneda.SelectedIndex;
		if (txtTipoCambio.Visible)
		{
			pedido.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
		}
		pedido.FechaPedido = dtpFecha.Value.Date;
		pedido.FechaEntrega = dtpFechaEntrega.Value.Date;
		pedido.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
		pedido.FechaPago = dtpFecha.Value.AddDays(fpago.Dias);
		pedido.CodListaPrecio = Convert.ToInt32(cbListaPrecios.SelectedValue);
		if (fpago.Dias == 0)
		{
			nota.FechaCancelado = dtpFecha.Value.Date;
			nota.Cancelado = 1;
		}
		pedido.Comentario = txtComentario.Text;
		pedido.CodAutorizado = CodAutorizado;
		pedido.MontoBruto = Convert.ToDecimal(txtBruto.Text);
		pedido.MontoDscto = Convert.ToDecimal(txtDscto.Text);
		pedido.Igv = Convert.ToDecimal(txtIGV.Text);
		pedido.Total = Convert.ToDecimal(txtPrecioVenta.Text);
		pedido.CodUser = frmLogin.iCodUser;
		pedido.Estado = 1;
		if (Proceso == 1)
		{
			if (!AdmPedido.insertEntConsExt(pedido))
			{
				return;
			}
			RecorreDetalle();
			if (detalle.Count > 0)
			{
				foreach (clsDetallePedido det in detalle)
				{
					AdmPedido.insertdetalleconsultor(det);
				}
			}
			MessageBox.Show("Los datos se guardaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CodPedido = pedido.CodPedido;
			sololectura(estado: true);
		}
		else
		{
			if ((Proceso != 2 && Proceso != 3) || !AdmPedido.update(pedido))
			{
				return;
			}
			RecorreDetalle();
			foreach (clsDetallePedido det2 in detalle)
			{
				AdmPedido.updatedetallesalidaconsultext(det2);
			}
			MessageBox.Show("Los datos se actualizaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
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
		clsDetallePedido deta = new clsDetallePedido();
		deta.CodDetallePedido = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodPedido = Convert.ToInt32(pedido.CodPedido);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDecimal(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDecimal(fila.Cells[dscto3.Name].Value);
		deta.MontoDescuento = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.DCantidadVendida = Convert.ToDecimal(fila.Cells[cantidadvend.Name].Value);
		deta.DCantidadDevuelta = Convert.ToDecimal(fila.Cells[cantidaddev.Name].Value);
		deta.IImpDevuelto = Convert.ToDecimal(fila.Cells[importedev.Name].Value);
		deta.IImpVend = Convert.ToDecimal(fila.Cells[importevend.Name].Value);
		detalle.Add(deta);
	}

	private void txtPedido_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso == 1)
		{
			actualizaimportes();
		}
	}

	private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
	{
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
		DataGridView tabla = (DataGridView)e.ControlToValidate;
		if (Proceso != 0)
		{
			if (tabla.Rows.Count >= 1)
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

	private void btnNewPedido_Click(object sender, EventArgs e)
	{
		frmConsultorExt form = new frmConsultorExt();
		form.MdiParent = base.MdiParent;
		form.Proceso = 1;
		form.txtDocRef.Focus();
		form.Show();
		Close();
	}

	private void txtComentario_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
	}

	private void cbListaPrecios_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CodLista = Convert.ToInt32(cbListaPrecios.SelectedValue);
		actualizaprecios();
		calculatotales();
	}

	private void actualizaprecios()
	{
		int codProduct = 0;
		DataTable precios = admLista.CargaListaPrecios(Convert.ToInt32(cbListaPrecios.SelectedValue));
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			codProduct = Convert.ToInt32(row.Cells[codproducto.Name].Value);
			foreach (DataRow r in precios.Rows)
			{
				if (codProduct == Convert.ToInt32(r["codProducto"].ToString()))
				{
					double precioa = Convert.ToDouble(r["precio"]);
					row.Cells[preciounit.Name].Value = string.Format("{0:#,##0.00}", r["precio"]);
					double cantidada = Convert.ToDouble(row.Cells[cantidad.Name].Value);
					double brutoa = cantidada * precioa;
					row.Cells[importe.Name].Value = $"{brutoa:#,##0.00}";
					double precioventaa = brutoa * (1.0 - Convert.ToDouble(row.Cells[dscto1.Name].Value) / 100.0) * (1.0 - Convert.ToDouble(row.Cells[dscto2.Name].Value) / 100.0) * (1.0 - Convert.ToDouble(row.Cells[dscto3.Name].Value) / 100.0);
					double montodescuentoa = brutoa - precioventaa;
					row.Cells[montodscto.Name].Value = $"{montodescuentoa:#,##0.00}";
					double valorventaa;
					if (r["precioneto"].ToString().Equals(r["precio"].ToString()))
					{
						valorventaa = precioventaa;
					}
					else
					{
						double factorigva = frmLogin.Configuracion.IGV / 100.0 + 1.0;
						valorventaa = precioventaa / factorigva;
					}
					double igva = precioventaa - valorventaa;
					double precioreala = precioventaa / cantidada;
					double valorreala = valorventaa / cantidada;
					row.Cells[precioventa.Name].Value = $"{precioventaa:#,##0.00}";
					row.Cells[valorventa.Name].Value = $"{valorventaa:#,##0.00}";
					row.Cells[precioreal.Name].Value = $"{precioreala:#,##0.00}";
					row.Cells[valoreal.Name].Value = $"{valorreala:#,##0.00}";
					row.Cells[igv.Name].Value = $"{igva:#,##0.00}";
				}
			}
		}
	}

	private void calculatotales()
	{
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{bruto - descuen - valor:#,##0.00}";
		txtPrecioVenta.Text = $"{bruto - descuen:#,##0.00}";
	}

	private void txtCotizacion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtCotizacion.Text != "")
		{
			if (BuscaCotizacion())
			{
				CargaCotizacion();
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Serie no existe, Presione F1 para consultar la tabla de ayuda", "Pedido", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaCotizacion()
	{
		coti = AdmCot.BuscaCotizacion(txtCotizacion.Text, frmLogin.iCodAlmacen);
		if (coti != null)
		{
			CodCotizacion = Convert.ToInt32(coti.CodCotizacion);
			return true;
		}
		CodCotizacion = 0;
		return false;
	}

	private void CargaCotizacion()
	{
		try
		{
			coti = AdmCot.CargaCotizacion(Convert.ToInt32(CodCotizacion), frmLogin.iCodAlmacen);
			if (coti != null)
			{
				txtCotizacion.Text = coti.CodCotizacion;
				if (txtCodCliente.Enabled)
				{
					CodCliente = coti.CodCliente;
					cli = AdmCli.MuestraCliente(CodCliente);
					txtCodCliente.Text = coti.CodigoPersonalizado;
					txtNombreCliente.Text = coti.Nombre;
					txtDireccion.Text = coti.Direccion;
				}
				cmbMoneda.SelectedIndex = coti.Moneda;
				txtTipoCambio.Text = coti.TipoCambio.ToString();
				cbListaPrecios.SelectedValue = coti.CodListaPrecio;
				txtComentario.Text = coti.Comentario;
				txtBruto.Text = $"{coti.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{coti.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{coti.Total - coti.Igv:#,##0.00}";
				txtIGV.Text = $"{coti.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{coti.Total:#,##0.00}";
				CargaDetalleCotizacion();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Pedido", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalleCotizacion()
	{
		dgvDetalle.DataSource = AdmCot.CargaDetalle(Convert.ToInt32(coti.CodCotizacion), frmLogin.iCodAlmacen);
	}

	private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.ColumnIndex == dgvDetalle.Columns[importe.Name].Index)
			{
				actualizaimportes();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		DialogResult dlgResult = MessageBox.Show("¿ Al liquidar la entrega esta será cerrada y no podra hacer operaciones sobre ella, Desea liquidar la entrega del dia ?", "Confirmar Liquidación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.No)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToInt32(row.Cells[cantidadvend.Name].Value) == 0)
			{
				row.Cells[cantidaddev.Name].Value = $"{Convert.ToDouble(row.Cells[cantidad.Name].Value):#,##0.00}";
			}
		}
		RecorreDetalle();
		foreach (clsDetallePedido det1 in detalle)
		{
			AdmPedido.updatedetallesalidaconsultext(det1);
		}
		foreach (int x in listNotas)
		{
			fv = AdmVenta.CargaFacturaVenta(x);
			fv.Codsalidaconsulext = int.Parse(CodPedido);
			AdmVenta.updatensconsultext(fv);
		}
		if (AdmPedido.liquidar(Convert.ToInt32(CodPedido)))
		{
			MessageBox.Show("La entrega ha sido liquidada", "Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmVentasDiarias"] != null)
		{
			Application.OpenForms["frmVentasDiarias"].Activate();
			return;
		}
		frmVentasDiarias form = new frmVentasDiarias();
		form.codVendedor = CodVendedor;
		form.ShowDialog();
		foreach (int x in listNotas)
		{
			List<clsProducto> prodvendidos = AdmProd.VentasProductosCount(x);
			foreach (clsProducto prod in prodvendidos)
			{
				if (prodvendidos == null || prodvendidos.Count <= 0)
				{
					return;
				}
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					bool encontro = false;
					if (prod.CodProducto == Convert.ToInt32(row.Cells[codproducto.Name].Value))
					{
						row.Cells[cantidadvend.Name].Value = $"{Convert.ToDouble(row.Cells[cantidadvend.Name].Value) + Convert.ToDouble(prod.StockActual):#,##0.00}";
						row.Cells[cantidaddev.Name].Value = $"{Convert.ToDouble(row.Cells[cantidad.Name].Value) - Convert.ToDouble(row.Cells[cantidadvend.Name].Value):#,##0.00}";
						encontro = true;
					}
					if (!encontro)
					{
						row.Cells[cantidaddev.Name].Value = $"{Convert.ToDouble(row.Cells[cantidad.Name].Value) - Convert.ToDouble(row.Cells[cantidadvend.Name].Value):#,##0.00}";
					}
				}
			}
		}
		button1.Enabled = true;
		panel1.Visible = false;
	}

	private void button3_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCliVisitados"] != null)
		{
			Application.OpenForms["frmCliVisitados"].Activate();
			return;
		}
		frmCliVisitados form = new frmCliVisitados();
		form.codEntConsExt = Convert.ToInt32(CodPedido);
		form.proceso = Proceso;
		form.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmConsultorExt));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.cbListaPrecios = new System.Windows.Forms.ComboBox();
		this.label26 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.lbDocumento = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.txtPedido = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.txtCotizacion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnNewPedido = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.button3 = new System.Windows.Forms.Button();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidadvend = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidaddev = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.importevend = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importedev = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtPrecioVenta = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.button2 = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.cbListaPrecios);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.dtpFechaEntrega);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.lbDocumento);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.txtPedido);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(0, 300);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(894, 152);
		this.groupBox1.TabIndex = 28;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.textBox1.Enabled = false;
		this.textBox1.Location = new System.Drawing.Point(494, 22);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(100, 20);
		this.textBox1.TabIndex = 65;
		this.textBox1.Tag = "20";
		this.textBox1.Visible = false;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(447, 26);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(40, 13);
		this.label6.TabIndex = 66;
		this.label6.Tag = "20";
		this.label6.Text = "Estado";
		this.label6.Visible = false;
		this.cbListaPrecios.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbListaPrecios.FormattingEnabled = true;
		this.cbListaPrecios.Items.AddRange(new object[5] { "EXCELENTE", "BUENO", "REGULAR", "MALO", "MOROSO" });
		this.cbListaPrecios.Location = new System.Drawing.Point(442, 100);
		this.cbListaPrecios.Name = "cbListaPrecios";
		this.cbListaPrecios.Size = new System.Drawing.Size(152, 20);
		this.cbListaPrecios.TabIndex = 63;
		this.cbListaPrecios.Visible = false;
		this.cbListaPrecios.SelectionChangeCommitted += new System.EventHandler(cbListaPrecios_SelectionChangeCommitted);
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(370, 103);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(57, 13);
		this.label26.TabIndex = 64;
		this.label26.Text = "Lista Prec.";
		this.label26.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(18, 101);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(79, 13);
		this.label3.TabIndex = 44;
		this.label3.Tag = "16";
		this.label3.Text = "Forma de Pago";
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(249, 100);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 43;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.Enabled = false;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(103, 100);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(140, 20);
		this.cmbFormaPago.TabIndex = 42;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.SelectedIndexChanged += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.dtpFechaEntrega.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaEntrega.Location = new System.Drawing.Point(791, 46);
		this.dtpFechaEntrega.Name = "dtpFechaEntrega";
		this.dtpFechaEntrega.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaEntrega.TabIndex = 41;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(699, 49);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(86, 13);
		this.label2.TabIndex = 40;
		this.label2.Text = "Fecha  Entrega :";
		this.lbDocumento.AutoSize = true;
		this.lbDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDocumento.Location = new System.Drawing.Point(137, 25);
		this.lbDocumento.Name = "lbDocumento";
		this.lbDocumento.Size = new System.Drawing.Size(0, 13);
		this.lbDocumento.TabIndex = 39;
		this.lbDocumento.Tag = "22";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(103, 74);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(491, 20);
		this.txtDireccion.TabIndex = 14;
		this.txtDireccion.Tag = "21";
		this.txtDireccion.Visible = false;
		this.txtPedido.Enabled = false;
		this.txtPedido.Location = new System.Drawing.Point(338, 23);
		this.txtPedido.Name = "txtPedido";
		this.txtPedido.ReadOnly = true;
		this.txtPedido.Size = new System.Drawing.Size(100, 20);
		this.txtPedido.TabIndex = 8;
		this.txtPedido.Tag = "20";
		this.txtPedido.Visible = false;
		this.txtPedido.Leave += new System.EventHandler(txtPedido_Leave);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(274, 27);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(58, 13);
		this.label8.TabIndex = 34;
		this.label8.Tag = "20";
		this.label8.Text = "Num. Doc.";
		this.label8.Visible = false;
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(791, 98);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 10;
		this.txtTipoCambio.Tag = "15";
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtTipoCambio.Visible = false;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(791, 72);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 21);
		this.cmbMoneda.TabIndex = 9;
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(711, 101);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 32;
		this.label16.Tag = "15";
		this.label16.Text = "Tipo/Cambio :";
		this.label16.Visible = false;
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(733, 75);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(52, 13);
		this.label17.TabIndex = 31;
		this.label17.Text = "Moneda :";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(198, 49);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(396, 20);
		this.txtNombreCliente.TabIndex = 13;
		this.txtNombreCliente.Tag = "3";
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Location = new System.Drawing.Point(103, 48);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(89, 20);
		this.txtCodCliente.TabIndex = 12;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.customValidator2);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(18, 52);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(53, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Vendedor";
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(103, 126);
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(491, 20);
		this.txtComentario.TabIndex = 16;
		this.txtComentario.Tag = "21";
		this.txtComentario.Leave += new System.EventHandler(txtComentario_Leave);
		this.txtComentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComentario_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(18, 129);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(60, 13);
		this.label9.TabIndex = 17;
		this.label9.Text = "Comentario";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(103, 23);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 5;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.Visible = false;
		this.txtDocRef.WordWrap = false;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(18, 25);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Doc. Ref.";
		this.label5.Visible = false;
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(791, 20);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 4;
		this.dtpFecha.Visible = false;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(706, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(79, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha Pedido :";
		this.label1.Visible = false;
		this.txtCotizacion.Location = new System.Drawing.Point(442, 115);
		this.txtCotizacion.Name = "txtCotizacion";
		this.txtCotizacion.Size = new System.Drawing.Size(89, 20);
		this.txtCotizacion.TabIndex = 65;
		this.txtCotizacion.Tag = "20";
		this.txtCotizacion.Visible = false;
		this.txtCotizacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCotizacion_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(380, 118);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(56, 13);
		this.label4.TabIndex = 66;
		this.label4.Tag = "20";
		this.label4.Text = "Cotización";
		this.label4.Visible = false;
		this.groupBox4.Controls.Add(this.button1);
		this.groupBox4.Controls.Add(this.btnNewPedido);
		this.groupBox4.Controls.Add(this.btnSalir);
		this.groupBox4.Controls.Add(this.btnGuardar);
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox4.Location = new System.Drawing.Point(0, 452);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(894, 48);
		this.groupBox4.TabIndex = 29;
		this.groupBox4.TabStop = false;
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 0;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(623, 10);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(91, 32);
		this.button1.TabIndex = 23;
		this.button1.Text = "Liquidar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "aim_protocol.png");
		this.btnNewPedido.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNewPedido.ImageIndex = 1;
		this.btnNewPedido.ImageList = this.imageList1;
		this.btnNewPedido.Location = new System.Drawing.Point(8, 10);
		this.btnNewPedido.Name = "btnNewPedido";
		this.btnNewPedido.Size = new System.Drawing.Size(71, 32);
		this.btnNewPedido.TabIndex = 19;
		this.btnNewPedido.Text = "&Nuevo";
		this.btnNewPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNewPedido.UseVisualStyleBackColor = true;
		this.btnNewPedido.Visible = false;
		this.btnNewPedido.Click += new System.EventHandler(btnNewPedido_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(820, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 18;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(737, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 17;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.button3);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Controls.Add(this.panel1);
		this.groupBox2.Controls.Add(this.button2);
		this.groupBox2.Controls.Add(this.btnNuevo);
		this.groupBox2.Controls.Add(this.btnEditar);
		this.groupBox2.Controls.Add(this.btnEliminar);
		this.groupBox2.Controls.Add(this.txtCotizacion);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(894, 300);
		this.groupBox2.TabIndex = 27;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.button3.ImageIndex = 6;
		this.button3.ImageList = this.imageList1;
		this.button3.Location = new System.Drawing.Point(8, 197);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(135, 32);
		this.button3.TabIndex = 67;
		this.button3.Text = "Clientes Asesorados";
		this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.cantidadvend, this.cantidaddev, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.importevend, this.importedev, this.coduser, this.fecharegistro);
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle12;
		this.dgvDetalle.Location = new System.Drawing.Point(6, 19);
		this.dgvDetalle.Name = "dgvDetalle";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(885, 172);
		this.dgvDetalle.TabIndex = 1;
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
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
		this.referencia.HeaderText = "Referencia";
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
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		dataGridViewCellStyle14.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle14;
		this.cantidad.HeaderText = "Cantidad Entreg.";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.cantidadvend.DataPropertyName = "cantidadvend";
		this.cantidadvend.HeaderText = "Cantidad Vend.";
		this.cantidadvend.Name = "cantidadvend";
		this.cantidadvend.ReadOnly = true;
		this.cantidadvend.Visible = false;
		this.cantidaddev.DataPropertyName = "cantidaddev";
		this.cantidaddev.HeaderText = "Cantidad Dev.";
		this.cantidaddev.Name = "cantidaddev";
		this.cantidaddev.ReadOnly = true;
		this.cantidaddev.Visible = false;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle15;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle16;
		this.importe.HeaderText = "Importe Entregado";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle17;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle18;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle19.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle19;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle20.Format = "N2";
		dataGridViewCellStyle20.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle20;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle21.Format = "N2";
		dataGridViewCellStyle21.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle21;
		this.valorventa.HeaderText = "V.Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle22.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle22;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle23.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle23;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
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
		this.importevend.DataPropertyName = "importevend";
		this.importevend.HeaderText = "ImporteVendido";
		this.importevend.Name = "importevend";
		this.importevend.Visible = false;
		this.importedev.DataPropertyName = "importedev";
		this.importedev.HeaderText = "ImporteDevuelto";
		this.importedev.Name = "importedev";
		this.importedev.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.Controls.Add(this.txtDscto);
		this.panel1.Controls.Add(this.label10);
		this.panel1.Controls.Add(this.label11);
		this.panel1.Controls.Add(this.txtBruto);
		this.panel1.Controls.Add(this.txtPrecioVenta);
		this.panel1.Controls.Add(this.label12);
		this.panel1.Controls.Add(this.txtValorVenta);
		this.panel1.Controls.Add(this.label14);
		this.panel1.Controls.Add(this.label13);
		this.panel1.Controls.Add(this.txtIGV);
		this.panel1.Location = new System.Drawing.Point(343, 199);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(545, 87);
		this.panel1.TabIndex = 24;
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(233, 9);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 2;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(24, 13);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(162, 12);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(75, 10);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 1;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(422, 61);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 5;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(362, 12);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(422, 9);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 3;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(362, 64);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(362, 38);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(422, 35);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 4;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.button2.ImageIndex = 5;
		this.button2.ImageList = this.imageList1;
		this.button2.Location = new System.Drawing.Point(145, 197);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(91, 32);
		this.button2.TabIndex = 22;
		this.button2.Text = "Ver Ventas";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(8, 234);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "&Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(85, 234);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 7;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(157, 234);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator2.ErrorMessage = "Ingrese un cliente.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator3.ErrorMessage = "Personal autorizado.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(894, 500);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmConsultorExt";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Entrega Artículos Vendedor";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmConsultorExt_Load);
		base.Shown += new System.EventHandler(frmConsultorExt_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
