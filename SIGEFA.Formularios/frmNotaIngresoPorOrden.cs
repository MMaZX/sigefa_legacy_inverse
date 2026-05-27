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

public class frmNotaIngresoPorOrden : Office2007Form
{
	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsMoneda Mon = new clsMoneda();

	private clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();

	private clsOrdenCompra Ord = new clsOrdenCompra();

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

	private clsAdmAutorizado AdmAut = new clsAdmAutorizado();

	private clsAutorizado aut = new clsAutorizado();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsValidar ok = new clsValidar();

	private clsDetalleNotaIngreso detaSelec = new clsDetalleNotaIngreso();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto prodeta = new clsProducto();

	private clsValidar val = new clsValidar();

	private clsFactura fac = new clsFactura();

	private clsAdmFactura AdmFact = new clsAdmFactura();

	private clsDetalleFactura detaSelec1 = new clsDetalleFactura();

	public string codigosNota = "";

	private clsPago Pag = new clsPago();

	private clsAdmPago Admpag = new clsAdmPago();

	public List<int> config = new List<int>();

	public List<int> documento = new List<int>();

	public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();

	public List<clsDetalleFactura> detalle1 = new List<clsDetalleFactura>();

	public DataTable datoscarga2 = new DataTable();

	public string CodNota;

	public int CodTransaccion;

	public int CodProveedor;

	public int CodCliente;

	public int CodDocumento;

	public int CodOrdenCompra;

	public int CodAutorizado;

	private bool Validacion = true;

	public int Proceso = 0;

	public int Tipo;

	public int codOrdenCompra_nota = 0;

	public int codFac = 0;

	public int vOrigOC = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private TextBox txtedit = new TextBox();

	private decimal sumadet = default(decimal);

	private int estado;

	private int contP;

	private int contN;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Label label2;

	private Label label4;

	private TextBox txtNDocRef;

	private Label label6;

	private Label label5;

	private Label lbNombreTransaccion;

	private TextBox txtNumDoc;

	private Label label7;

	private TextBox txtComentario;

	private Label label9;

	private Label label8;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnGuardar;

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

	private Label label15;

	public TextBox txtCodProv;

	public TextBox txtNombreProv;

	public TextBox txtDocRef;

	public TextBox txtTransaccion;

	private Label label17;

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

	private CustomValidator customValidator8;

	private CustomValidator customValidator9;

	private CustomValidator customValidator10;

	public DataGridView dgvDetalle;

	public TextBox txtOrdenCompra;

	public TextBox txtCodProveedor;

	public TextBox txtCodNota;

	public ComboBox cmbFormaPago;

	public DateTimePicker dtpFechaPago;

	public TextBox txtTipoCambio;

	public ComboBox cmbMoneda;

	public Label label16;

	public TextBox txtFlete;

	public Label label19;

	private CheckBox cbDetraccion;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codnoting;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn fechaingreso;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn valorventaconflete;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn flete;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn pvconflete;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn bonificacion;

	private DataGridViewTextBoxColumn cantidadnueva;

	public frmNotaIngresoPorOrden()
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

	public void llenardetalle2()
	{
		data.DataSource = null;
		DataTable datoscarga = new DataTable();
		datoscarga = AdmNota.MuestraGuia(frmLogin.iCodAlmacen, frmLogin.iCodUser);
		if (datoscarga != null)
		{
			datoscarga2.Merge(datoscarga);
		}
		dgvDetalle.DataSource = datoscarga2;
		if (vOrigOC == 0)
		{
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
		}
		else
		{
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = true;
		}
		dgvDetalle.ClearSelection();
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(0);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = -1;
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

	private void CargaProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtCodProv.Text = prov.Ruc;
		txtNombreProv.Text = prov.RazonSocial;
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
		txtOrdenCompra.Visible = true;
		label8.Visible = true;
		CargaFormaPagos();
		cargaMoneda();
		if (txtTransaccion.Text.Equals("IOC"))
		{
			label17.Visible = true;
			cmbFormaPago.Visible = true;
			dtpFechaPago.Visible = true;
		}
		if (Proceso == 1)
		{
			Bloqueabotones();
		}
		if (Proceso == 2)
		{
			CargaNotaIngreso();
		}
		else if (Proceso == 3)
		{
			if (codFac != 0)
			{
				CargaFactura();
				sololectura(estado: true);
			}
			else
			{
				CargaNotaIngreso();
				sololectura(estado: true);
			}
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
		else if (Proceso == 6)
		{
			CargaNotaIngreso();
			sololectura(estado: true);
		}
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = -1;
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
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		txtFlete.ReadOnly = estado;
		btnEliminar.Visible = !estado;
		btnGuardar.Visible = !estado;
	}

	private void Bloqueabotones()
	{
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
				dtpFecha.Value = nota.FechaIngreso;
				cmbMoneda.SelectedValue = nota.Moneda;
				txtTipoCambio.Text = nota.TipoCambio.ToString();
				txtTipoCambio.Visible = true;
				label16.Visible = true;
				if (txtDocRef.Enabled)
				{
					CodDocumento = nota.CodTipoDocumento;
					txtDocRef.Text = nota.SiglaDocumento;
					txtNDocRef.Text = nota.NumDoc;
					BuscaTipoDocumento();
				}
				if (txtOrdenCompra.Enabled)
				{
					txtCodProv.Text = nota.RUCProveedor;
					txtCodProveedor.Text = nota.CodProveedor.ToString();
					txtNombreProv.Text = nota.RazonSocialProveedor;
					txtOrdenCompra.Text = nota.SDocumentoOrden;
					txtOrdenCompra.Visible = true;
					txtOrdenCompra.Enabled = false;
					label8.Visible = true;
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
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso Por Orden", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
			fac = AdmFact.CargaFactura(Convert.ToInt32(codFac));
			string guias = AdmFact.guiasPorFactura(fac.CodFactura);
			if (fac != null)
			{
				txtOrdenCompra.Text = guias;
				txtNumDoc.Text = fac.CodFactura.ToString().PadLeft(9, '0');
				CodTransaccion = fac.CodTipoTransaccion;
				CargaTransaccion();
				dtpFecha.Value = fac.FechaIngreso;
				cmbMoneda.SelectedValue = fac.Moneda;
				txtTipoCambio.Visible = true;
				label16.Visible = true;
				txtTipoCambio.Text = fac.TipoCambio.ToString();
				txtTipoCambio.Visible = true;
				label16.Visible = true;
				CodDocumento = fac.CodTipoDocumento;
				txtDocRef.Text = fac.DocumentoFactura.Substring(0, 2);
				txtNDocRef.Text = fac.DocumentoFactura.Substring(3);
				txtCodProv.Text = fac.RUCProveedor;
				txtCodProv.Visible = true;
				txtCodProveedor.Text = fac.CodProveedor.ToString();
				txtNombreProv.Text = fac.RazonSocialProveedor;
				txtNombreProv.Visible = true;
				txtOrdenCompra.Visible = false;
				label8.Visible = false;
				cmbFormaPago.SelectedValue = fac.FormaPago;
				dtpFechaPago.Value = fac.FechaPago;
				txtComentario.Text = fac.Comentario;
				txtBruto.Text = $"{fac.MontoBruto:#,##0.0000}";
				txtDscto.Text = $"{fac.MontoDscto:#,##0.0000}";
				txtFlete.Text = $"{fac.Flete:#,##0.0000}";
				txtValorVenta.Text = $"{fac.Total - fac.Igv:#,##0.0000}";
				txtIGV.Text = $"{fac.Igv:#,##0.0000}";
				txtPrecioVenta.Text = $"{fac.Total:#,##0.0000}";
				CargaDetalleFactura();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso Por Orden", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmNota.CargaDetalle(Convert.ToInt32(nota.CodNotaIngreso));
		RecorreDetalle();
		nota.Detalle = detalle;
	}

	private void CargaDetalleFactura()
	{
		dgvDetalle.DataSource = AdmFact.CargaDetalle(Convert.ToInt32(fac.CodFactura));
		valoreal.Visible = false;
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		if (Proceso == 1)
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
	}

	private void limpiar()
	{
		txtOrdenCompra.Text = "";
		txtCodProveedor.Text = "";
		txtNombreProv.Text = "";
		txtCodProv.Text = "";
	}

	private void txtCodProv_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		limpiar();
		if (Application.OpenForms["frmProveedoresLista"] != null)
		{
			Application.OpenForms["frmProveedoresLista"].Activate();
			return;
		}
		dgvDetalle.DataSource = null;
		data.DataSource = null;
		dgvDetalle.Refresh();
		frmProveedoresLista form = new frmProveedoresLista();
		form.Proceso = 3;
		form.Procede = 1;
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
		tran = AdmTran.MuestraTransaccion(14);
		txtTransaccion.Text = tran.Sigla;
		txtTransaccion.ReadOnly = true;
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, ee);
		if (tran.CodTransaccion == 14)
		{
			label17.Visible = true;
			cmbFormaPago.Visible = true;
			dtpFechaPago.Visible = true;
		}
		if (Proceso == 1)
		{
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			if (tc != null)
			{
				txtTipoCambio.Text = tc.Venta.ToString();
				txtTipoCambio.Visible = true;
				label16.Visible = true;
			}
			else
			{
				MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				dtpFecha.Value = DateTime.Now.Date;
				cmbMoneda.Focus();
				Close();
			}
			txtOrdenCompra.Focus();
		}
		if (Proceso == 3)
		{
			txtTipoCambio.Visible = true;
			label16.Visible = true;
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
			Application.OpenForms["frmDocumentos"].Activate();
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.Proceso = 3;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtDocRef.Text = doc.Sigla;
		if (CodDocumento != 0)
		{
			ProcessTabKey(forward: true);
		}
		else
		{
			txtDocRef.Text = "";
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
				MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1)
		{
			calculatotales();
		}
	}

	private void calculatotales()
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
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{igvt:#,##0.0000}";
		txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
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

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		AdmNota.deleteConsolidado(frmLogin.iCodAlmacen, frmLogin.iCodUser);
		try
		{
			if (!superValidator1.Validate())
			{
				return;
			}
			if (verificarCamposVacios() == 1)
			{
				MessageBox.Show("Debe completar Detalle de Nota, Datos Vacios");
			}
			else
			{
				if (Proceso == 0)
				{
					return;
				}
				if (txtFlete.Text != "" && Convert.ToDouble(txtFlete.Text) > 0.0)
				{
					prorrateodeflete();
					recalculadetalle();
					calculatotales();
				}
				fac.CodAlmacen = frmLogin.iCodAlmacen;
				fac.CodTipoTransaccion = tran.CodTransaccion;
				fac.CodProveedor = Convert.ToInt32(txtCodProveedor.Text);
				fac.CodTipoDocumento = doc.CodTipoDocumento;
				fac.DocumentoFactura = doc.Sigla + "-" + txtNDocRef.Text;
				fac.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				if (txtTipoCambio.Visible)
				{
					fac.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
				}
				fac.FechaIngreso = dtpFecha.Value.Date;
				fac.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
				fac.FechaPago = dtpFechaPago.Value.Date;
				if (fpago.Tipo)
				{
					fac.FechaCancelado = dtpFecha.Value.Date;
				}
				fac.Cancelado = 0;
				fac.Comentario = txtComentario.Text;
				fac.MontoBruto = Convert.ToDouble(txtBruto.Text);
				fac.MontoDscto = Convert.ToDouble(txtDscto.Text);
				if (txtFlete.Text != "")
				{
					fac.Flete = Convert.ToDouble(txtFlete.Text);
				}
				fac.Igv = Convert.ToDouble(txtIGV.Text);
				fac.Total = Convert.ToDouble(txtPrecioVenta.Text);
				fac.CodUser = frmLogin.iCodUser;
				fac.CodOrdenCompra = codOrdenCompra_nota;
				fac.Estado = 1;
				if (Proceso != 1)
				{
					return;
				}
				RecorreGrilla();
				if (estado != 1 || !AdmFact.insert(fac))
				{
					return;
				}
				RecorreDetalle();
				if (detalle1.Count > 0)
				{
					foreach (clsDetalleFactura det in detalle1)
					{
						int cont = 0;
						AdmFact.insertdetalle(det);
						for (int i = 0; i < det.CodNotaIngreso.Length; i++)
						{
							if (det.CodNotaIngreso.Substring(0, i).Contains(","))
							{
								cont++;
							}
						}
						if (cont > 1)
						{
							AdmNota.ActualizaCodNotaIngreso(det.Cantidad, det.CodProducto, det.CodDetalleFactura, 2);
						}
						else
						{
							AdmNota.ActualizaCodNotaIngreso(det.Cantidad, det.CodProducto, det.CodDetalleFactura, 1);
						}
					}
					if (VerificarDetracciones() && cbDetraccion.Checked)
					{
						DialogResult dlgResult = MessageBox.Show("La operación está afecta a detracción, Desea generar pago de detracción?", "Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dlgResult == DialogResult.No)
						{
							return;
						}
						GenerarPagoDetraccion();
					}
				}
				MessageBox.Show("Los datos se guardaron correctamente", "Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				if (fpago.Tipo)
				{
					ingresarpago();
				}
				txtNumDoc.Text = fac.CodFacturaNueva.ToString().PadLeft(11, '0');
				Close();
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void GenerarPagoDetraccion()
	{
		decimal porcentdet = 0.015m;
		try
		{
			Pag.CodNota = fac.CodFacturaNueva.ToString();
			Pag.CodMoneda = 1;
			Pag.Tipo = false;
			Pag.IngresoEgreso = false;
			if (txtTipoCambio.Text == "")
			{
				Pag.TipoCambio = 0m;
			}
			else
			{
				Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
			}
			Pag.MontoPagado = sumadet * porcentdet;
			Pag.MontoCobrado = sumadet * porcentdet;
			Pag.Vuelto = 0m;
			Pag.NOperacion = "";
			Pag.NCheque = "";
			Pag.FechaPago = dtpFecha.Value;
			Pag.Observacion = "";
			Pag.CodUser = frmLogin.iCodUser;
			Pag.CodAlmacen = frmLogin.iCodAlmacen;
			Pag.Provision = true;
			Pag.Pendiente = true;
			if (Admpag.insertpagodetraccion(Pag))
			{
				MessageBox.Show("El pago de detraccion se envio a Tesoreria", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("El pago de detraccion no pudo generarse", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		finally
		{
		}
	}

	private int verificarCamposVacios()
	{
		int valor = 1;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			string cant = "";
			string precio = "";
			string impor = "";
			string IG = "";
			string MontDes = "";
			string d1 = "";
			string d2 = "";
			string d3 = "";
			cant = Convert.ToString(Convert.ToInt32(row.Cells[cantidad.Name].Value));
			impor = Convert.ToString(row.Cells[importe.Name]);
			IG = Convert.ToString(row.Cells[igv.Name]);
			MontDes = Convert.ToString(row.Cells[montodscto.Name]);
			precio = Convert.ToString(row.Cells[preciounit.Name].Value);
			d1 = Convert.ToString(row.Cells[dscto1.Name].Value);
			d2 = Convert.ToString(row.Cells[dscto2.Name].Value);
			d3 = Convert.ToString(row.Cells[dscto3.Name].Value);
			if (d1 != "" || d2 != "" || d3 != "")
			{
				calculatotales();
			}
			valor = ((cant == "" || precio == "" || impor == "" || IG == "") ? 1 : 0);
		}
		return valor;
	}

	private void ingresarpago()
	{
		frmCancelarPago form = new frmCancelarPago();
		form.CodNota = fac.CodFacturaNueva.ToString();
		form.mon = nota.Moneda;
		form.tipo = 1;
		form.ShowDialog();
	}

	private void RecorreGrilla()
	{
		try
		{
			detalle.Clear();
			contP = 0;
			contN = 0;
			if (dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					if (Convert.ToInt32(row.Cells[bonificacion.Name].Value) == 1 && Convert.ToDouble(row.Cells[preciounit.Name].Value).Equals(0.0))
					{
						preciounit.ReadOnly = true;
						cantidad.ReadOnly = true;
						dscto1.ReadOnly = true;
						contP++;
						continue;
					}
					if (Convert.ToInt32(row.Cells[bonificacion.Name].Value) == 0 && Convert.ToDouble(row.Cells[preciounit.Name].Value).Equals(0.0))
					{
						MessageBox.Show("Modifique PrecioUnitario" + Environment.NewLine + " del Producto [" + row.Cells[referencia.Name].Value?.ToString() + "]" + Environment.NewLine + "No es una Bonificacion!!", "FacturaCompra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						contN++;
						break;
					}
					contP++;
				}
			}
			if (contP == dgvDetalle.Rows.Count)
			{
				estado = 1;
			}
			else
			{
				estado = 0;
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void RecorreDetalle()
	{
		try
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
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		try
		{
			clsDetalleFactura deta = new clsDetalleFactura();
			deta.CodFactura = fac.CodFacturaNueva;
			deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			deta.CodNotaIngreso = txtCodNota.Text;
			deta.CodAlmacen = frmLogin.iCodAlmacen;
			deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			deta.SerieLote = "0";
			deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
			deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
			deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
			deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
			deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
			deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
			deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
			deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
			deta.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
			deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
			deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
			deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
			deta.FechaIngreso = dtpFecha.Value;
			deta.CodUser = frmLogin.iCodUser;
			deta.CodProveedor = Convert.ToInt32(txtCodProveedor.Text);
			detalle1.Add(deta);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void CargaFilaDetalle(DataGridViewRow fila)
	{
		detaSelec1.CodFactura = fac.CodFacturaNueva;
		detaSelec1.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		detaSelec1.CodNotaIngreso = txtCodNota.Text;
		detaSelec1.CodAlmacen = frmLogin.iCodAlmacen;
		detaSelec1.Moneda = cmbMoneda.SelectedIndex;
		detaSelec1.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		detaSelec1.SerieLote = "0";
		detaSelec1.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		detaSelec1.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		detaSelec1.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		detaSelec1.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		detaSelec1.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		detaSelec1.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		detaSelec1.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		detaSelec1.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		detaSelec1.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
		detaSelec1.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		detaSelec1.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		detaSelec1.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		detaSelec1.FechaIngreso = dtpFecha.Value;
		detaSelec1.CodUser = frmLogin.iCodUser;
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
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	public void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso != 1)
		{
			return;
		}
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{bruto - descuen - valor:#,##0.0000}";
		txtPrecioVenta.Text = $"{bruto - descuen:#,##0.0000}";
		calculatotales();
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
		if (!(dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa") || (Proceso != 1 && Proceso != 2))
		{
			return;
		}
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
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{igvt:#,##0.0000}";
		txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
		calculatotales();
	}

	private void prorrateodeflete()
	{
		if (!(txtFlete.Text != "") || dgvDetalle.Rows.Count < 1)
		{
			return;
		}
		decimal precior = default(decimal);
		decimal percentr = default(decimal);
		decimal fleter = default(decimal);
		decimal totalr = default(decimal);
		decimal dflete = Convert.ToDecimal(txtFlete.Text);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			totalr += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
		}
		foreach (DataGridViewRow row2 in (IEnumerable)dgvDetalle.Rows)
		{
			precior = Convert.ToDecimal(row2.Cells[precioventa.Name].Value);
			percentr = precior / totalr;
			fleter = dflete * percentr;
			row2.Cells[flete.Name].Value = $"{fleter:#,##0.0000}";
		}
	}

	private void recalculadetalle()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[valorventaconflete.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) + Convert.ToDecimal(row.Cells[flete.Name].Value);
			row.Cells[pvconflete.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) + Convert.ToDecimal(row.Cells[flete.Name].Value);
			if (Convert.ToDecimal(row.Cells[flete.Name].Value) > 0.00m && row.Cells[flete.Name].Value.ToString() != "")
			{
				row.Cells[valoreal.Name].Value = Convert.ToDecimal(row.Cells[valorventaconflete.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				row.Cells[precioreal.Name].Value = Convert.ToDecimal(row.Cells[pvconflete.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			}
			else
			{
				row.Cells[valoreal.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				row.Cells[precioreal.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			}
		}
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (base.Visible && dgvDetalle.Rows.Count >= 1 && dgvDetalle.CurrentRow.Index == e.RowIndex && e.RowIndex != -1)
		{
			CargaFilaDetalle(dgvDetalle.CurrentRow);
			if (Convert.ToInt32(dgvDetalle.CurrentRow.Cells[bonificacion.Name].Value) == 1)
			{
				MessageBox.Show("Es Bonificación No Necesita Ingresar Montos");
				dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = true;
				dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = true;
				dgvDetalle.CurrentRow.Cells[dscto1.Name].ReadOnly = true;
			}
			if (!(Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value) != 0m))
			{
			}
		}
	}

	private void txtOrdenCompra_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			AdmNota.deleteConsolidado(frmLogin.iCodAlmacen, frmLogin.iCodUser);
			txtCodNota.Text = "";
			if (e.KeyCode == Keys.F1)
			{
				if (Application.OpenForms["frmNotaOrdenAlmacen"] != null)
				{
					Application.OpenForms["frmNotaOrdenAlmacen"].Activate();
					return;
				}
				frmNotaOrdenAlmacen form = new frmNotaOrdenAlmacen();
				form.proceso = 7;
				form.procede = 1;
				form.coddetallenota = documento;
				form.Cargaconsolidado();
				form.unir = "";
				form.ShowDialog();
				txtDocRef.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
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

	private void customValidator10_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (dgvDetalle.Rows.Count > 0)
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

	private void dgvDetalle_KeyDown(object sender, KeyEventArgs e)
	{
		calculatotales();
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == cantidad.Index)
		{
			val.SOLONumeros(sender, e);
		}
		if (dgvDetalle.CurrentCell.ColumnIndex == preciounit.Index)
		{
			val.SOLONumeros(sender, e);
		}
		if (dgvDetalle.CurrentCell.ColumnIndex == dscto1.Index)
		{
			val.SOLONumeros(sender, e);
		}
		if (dgvDetalle.CurrentCell.ColumnIndex == dscto2.Index)
		{
			val.SOLONumeros(sender, e);
		}
		if (dgvDetalle.CurrentCell.ColumnIndex == dscto3.Index)
		{
			val.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (Convert.ToDouble(txtedit.Text) > Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidadnueva.Name].Value) && dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "cantidad")
			{
				MessageBox.Show("Cantidad Debe Ser Menor o Igual que: " + dgvDetalle.CurrentRow.Cells[cantidadnueva.Name].Value);
				dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = dgvDetalle.CurrentRow.Cells[cantidadnueva.Name].Value;
			}
			else if (dgvDetalle.CurrentRow.Cells[cantidad.Name].Value.ToString() != "" && dgvDetalle.CurrentRow.Cells[preciounit.Name].Value.ToString() != "")
			{
				dgvDetalle.CurrentRow.Cells[importe.Name].Value = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value) * Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
				importes();
				calculatotales();
			}
			if ((dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "importe" || dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "preciounit") && dgvDetalle.CurrentRow.Cells[cantidad.Name].Value.ToString() != "" && dgvDetalle.CurrentRow.Cells[preciounit.Name].Value.ToString() != "")
			{
				dgvDetalle.CurrentRow.Cells[importe.Name].Value = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value) * Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
				importes();
				calculatotales();
			}
			if (dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "valorventa")
			{
				importes();
				calculatotales();
			}
			if (dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "montodscto" || dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "dscto1" || dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "dscto2" || dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "dscto3")
			{
				if (dgvDetalle.CurrentRow.Cells[dscto1.Name].Value.ToString() != "" || dgvDetalle.CurrentRow.Cells[dscto2.Name].Value.ToString() != "" || dgvDetalle.CurrentRow.Cells[dscto3.Name].Value.ToString() != "")
				{
					dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[importe.Name].Value) - Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[importe.Name].Value) * (1m - Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[dscto1.Name].Value) / 100m) * (1m - Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[dscto2.Name].Value) / 100m) * (1m - Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[dscto3.Name].Value) / 100m);
				}
				importes();
				calculatotales();
			}
			if (dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "montodscto")
			{
				if (dgvDetalle.CurrentRow.Cells[dscto1.Name].Value.ToString() == "" && dgvDetalle.CurrentRow.Cells[dscto2.Name].Value.ToString() == "" && dgvDetalle.CurrentRow.Cells[dscto3.Name].Value.ToString() == "")
				{
					dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = "";
				}
				importes();
				calculatotales();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex?.ToString() ?? "");
		}
	}

	private void importes()
	{
		dgvDetalle.CurrentRow.Cells[precioventa.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[importe.Name].Value) - Convert.ToDouble(dgvDetalle.CurrentRow.Cells[montodscto.Name].Value);
		dgvDetalle.CurrentRow.Cells[valorventa.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value) / Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
		dgvDetalle.CurrentRow.Cells[igv.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value) - Convert.ToDouble(dgvDetalle.CurrentRow.Cells[valorventa.Name].Value);
		recalculadetalle();
	}

	private void txtFlete_KeyUp(object sender, KeyEventArgs e)
	{
		if (verificarCamposVacios() == 1)
		{
			MessageBox.Show("Debe completar Datos Vacios");
		}
		else if (txtFlete.Text == "" || txtFlete.Text == "0.0000")
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				row.Cells[flete.Name].Value = $"{0:#,##0.0000}";
			}
			recalculadetalle();
			calculatotales();
		}
		else
		{
			prorrateodeflete();
			recalculadetalle();
			calculatotales();
		}
	}

	private void txtFlete_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar == '\r')
		{
			prorrateodeflete();
			recalculadetalle();
			calculatotales();
		}
	}

	private void dgvDetalle_Leave(object sender, EventArgs e)
	{
		try
		{
			if (dgvDetalle.Rows.Count > 0 && dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "preciounit" && txtedit.Text != "")
			{
				dgvDetalle.CurrentRow.Cells[importe.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value) * Convert.ToDouble(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
				importes();
				calculatotales();
				if (Convert.ToString(dgvDetalle.CurrentRow.Cells[dscto1.Name].Value) == "")
				{
					dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = "";
					calculatotales();
				}
				if (Convert.ToString(dgvDetalle.CurrentRow.Cells[dscto2.Name].Value) == "")
				{
					dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = "";
					calculatotales();
				}
				if (Convert.ToString(dgvDetalle.CurrentRow.Cells[dscto3.Name].Value) == "")
				{
					dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = "";
					calculatotales();
				}
				importes();
				calculatotales();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
		}
	}

	private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.Rows.Count >= 1 && dgvDetalle.CurrentRow.Index == e.RowIndex && e.RowIndex != -1 && Convert.ToInt32(dgvDetalle.CurrentRow.Cells[bonificacion.Name].Value) == 1)
		{
			MessageBox.Show("Es Bonificación No Necesita Ingresar Montos");
			dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = true;
			dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = true;
			dgvDetalle.CurrentRow.Cells[dscto1.Name].ReadOnly = true;
		}
		if (!(Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value) != 0m))
		{
		}
	}

	public bool VerificarDetracciones()
	{
		sumadet = default(decimal);
		if (CodDocumento == 2)
		{
			if (dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					if (Convert.ToInt32(row.Cells[igv.Name].Value) == 0)
					{
						sumadet += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
					}
				}
			}
			if (sumadet <= 700m)
			{
				return false;
			}
			return true;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotaIngresoPorOrden));
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbDetraccion = new System.Windows.Forms.CheckBox();
		this.txtCodNota = new System.Windows.Forms.TextBox();
		this.txtCodProveedor = new System.Windows.Forms.TextBox();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
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
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codnoting = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventaconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.flete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pvconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.bonificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidadnueva = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator9 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator8 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator10 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cbDetraccion);
		this.groupBox1.Controls.Add(this.txtCodNota);
		this.groupBox1.Controls.Add(this.txtCodProveedor);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label15);
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
		this.groupBox1.Size = new System.Drawing.Size(806, 171);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.cbDetraccion.AutoSize = true;
		this.cbDetraccion.Checked = true;
		this.cbDetraccion.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbDetraccion.Location = new System.Drawing.Point(639, 117);
		this.cbDetraccion.Name = "cbDetraccion";
		this.cbDetraccion.Size = new System.Drawing.Size(159, 17);
		this.cbDetraccion.TabIndex = 43;
		this.cbDetraccion.Text = "Generar pago de detracción";
		this.cbDetraccion.UseVisualStyleBackColor = true;
		this.cbDetraccion.Visible = false;
		this.txtCodNota.Enabled = false;
		this.txtCodNota.Location = new System.Drawing.Point(562, 62);
		this.txtCodNota.Name = "txtCodNota";
		this.txtCodNota.Size = new System.Drawing.Size(10, 20);
		this.txtCodNota.TabIndex = 2;
		this.txtCodNota.Visible = false;
		this.txtCodProveedor.Enabled = false;
		this.txtCodProveedor.Location = new System.Drawing.Point(562, 37);
		this.txtCodProveedor.Name = "txtCodProveedor";
		this.txtCodProveedor.Size = new System.Drawing.Size(10, 20);
		this.txtCodProveedor.TabIndex = 5;
		this.txtCodProveedor.Visible = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(254, 88);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 9;
		this.dtpFechaPago.Tag = "16";
		this.cmbFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(108, 88);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(140, 20);
		this.cmbFormaPago.TabIndex = 8;
		this.cmbFormaPago.Tag = "16";
		this.superValidator1.SetValidator3(this.cmbFormaPago, this.customValidator7);
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(23, 91);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(79, 13);
		this.label17.TabIndex = 41;
		this.label17.Tag = "16";
		this.label17.Text = "Forma de Pago";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Location = new System.Drawing.Point(716, 90);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 13;
		this.txtTipoCambio.Tag = "15";
		this.txtTipoCambio.Text = "0";
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(716, 63);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 21);
		this.cmbMoneda.TabIndex = 12;
		this.superValidator1.SetValidator1(this.cmbMoneda, this.customValidator9);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(636, 93);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 21;
		this.label16.Tag = "15";
		this.label16.Text = "Tipo/Cambio :";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(658, 66);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(52, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Moneda :";
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(108, 114);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(451, 51);
		this.txtComentario.TabIndex = 14;
		this.txtComentario.Tag = "21";
		this.txtComentario.Visible = false;
		this.txtComentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComentario_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(23, 117);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(34, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Glosa";
		this.label9.Visible = false;
		this.txtOrdenCompra.Location = new System.Drawing.Point(338, 62);
		this.txtOrdenCompra.Name = "txtOrdenCompra";
		this.txtOrdenCompra.Size = new System.Drawing.Size(218, 20);
		this.txtOrdenCompra.TabIndex = 1;
		this.txtOrdenCompra.Tag = "18";
		this.superValidator1.SetValidator1(this.txtOrdenCompra, this.customValidator8);
		this.txtOrdenCompra.KeyDown += new System.Windows.Forms.KeyEventHandler(txtOrdenCompra_KeyDown);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(298, 66);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(37, 13);
		this.label8.TabIndex = 15;
		this.label8.Tag = "18";
		this.label8.Text = "Guias:";
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Location = new System.Drawing.Point(105, 11);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 20);
		this.txtNumDoc.TabIndex = 2;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(25, 14);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.txtNDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNDocRef.Location = new System.Drawing.Point(203, 62);
		this.txtNDocRef.Name = "txtNDocRef";
		this.txtNDocRef.Size = new System.Drawing.Size(75, 20);
		this.txtNDocRef.TabIndex = 7;
		this.txtNDocRef.Tag = "11";
		this.superValidator1.SetValidator3(this.txtNDocRef, this.customValidator5);
		this.txtNDocRef.Visible = false;
		this.txtNDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNDocRef_KeyPress);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(142, 65);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(55, 13);
		this.label6.TabIndex = 10;
		this.label6.Tag = "11";
		this.label6.Text = "Num. Ref.";
		this.label6.Visible = false;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(108, 62);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 6;
		this.txtDocRef.Tag = "10";
		this.superValidator1.SetValidator3(this.txtDocRef, this.customValidator4);
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(23, 66);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 8;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.txtNombreProv.Enabled = false;
		this.txtNombreProv.Location = new System.Drawing.Point(200, 37);
		this.txtNombreProv.Name = "txtNombreProv";
		this.txtNombreProv.ReadOnly = true;
		this.txtNombreProv.Size = new System.Drawing.Size(356, 20);
		this.txtNombreProv.TabIndex = 4;
		this.txtNombreProv.Tag = "9";
		this.txtCodProv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodProv.Enabled = false;
		this.txtCodProv.Location = new System.Drawing.Point(105, 37);
		this.txtCodProv.Name = "txtCodProv";
		this.txtCodProv.Size = new System.Drawing.Size(89, 20);
		this.txtCodProv.TabIndex = 3;
		this.txtCodProv.Tag = "8";
		this.superValidator1.SetValidator3(this.txtCodProv, this.customValidator2);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(20, 40);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(56, 13);
		this.label4.TabIndex = 5;
		this.label4.Tag = "8";
		this.label4.Text = "Proveedor";
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(136, 177);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(248, 19);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.lbNombreTransaccion.Visible = false;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Location = new System.Drawing.Point(102, 174);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.Size = new System.Drawing.Size(28, 20);
		this.txtTransaccion.TabIndex = 1;
		this.superValidator1.SetValidator3(this.txtTransaccion, this.customValidator1);
		this.txtTransaccion.Visible = false;
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(20, 174);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.label2.Visible = false;
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(716, 37);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 11;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(616, 40);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(94, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha de Emisión:";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 489);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(806, 42);
		this.groupBox3.TabIndex = 19;
		this.groupBox3.TabStop = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(732, 4);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 10;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(649, 4);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 11;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(8, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.dgvDetalle);
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
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 171);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(806, 318);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codnoting, this.codproducto, this.referencia, this.descripcion, this.moneda, this.codunidad, this.unidad, this.serielote, this.cantidad, this.fechaingreso, this.coduser, this.fecharegistro, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.valorventaconflete, this.igv, this.flete, this.precioventa, this.pvconflete, this.precioreal, this.valoreal, this.bonificacion, this.cantidadnueva);
		this.dgvDetalle.Location = new System.Drawing.Point(6, 19);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(800, 205);
		this.dgvDetalle.TabIndex = 67;
		this.superValidator1.SetValidator1(this.dgvDetalle, this.customValidator10);
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellDoubleClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.dgvDetalle.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvDetalle_KeyDown);
		this.dgvDetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvDetalle_KeyPress);
		this.dgvDetalle.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvDetalle_KeyUp);
		this.dgvDetalle.Leave += new System.EventHandler(dgvDetalle_Leave);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codnoting.DataPropertyName = "codNota";
		this.codnoting.HeaderText = "codNotaIngreso";
		this.codnoting.Name = "codnoting";
		this.codnoting.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Width = 105;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 300;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.Visible = false;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 85;
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
		this.cantidad.MaxInputLength = 11;
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
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
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N4";
		dataGridViewCellStyle2.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle2;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.MaxInputLength = 30;
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N4";
		dataGridViewCellStyle3.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle3;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Width = 85;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N4";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle4;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto1.Width = 70;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N4";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle5;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto2.Width = 70;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N4";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle6;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.dscto3.Width = 70;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N4";
		dataGridViewCellStyle7.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle7;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.montodscto.Width = 80;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N4";
		dataGridViewCellStyle8.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle8;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Width = 80;
		this.valorventaconflete.HeaderText = "vvconflete";
		this.valorventaconflete.Name = "valorventaconflete";
		this.valorventaconflete.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N4";
		this.igv.DefaultCellStyle = dataGridViewCellStyle9;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Width = 80;
		this.flete.DataPropertyName = "flete";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.flete.DefaultCellStyle = dataGridViewCellStyle10;
		this.flete.HeaderText = "Flete";
		this.flete.Name = "flete";
		this.flete.ReadOnly = true;
		this.flete.Visible = false;
		this.flete.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "N4";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle11;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Width = 90;
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
		this.bonificacion.DataPropertyName = "bonificacion";
		this.bonificacion.HeaderText = "Bonificacion";
		this.bonificacion.Name = "bonificacion";
		this.bonificacion.Visible = false;
		this.cantidadnueva.DataPropertyName = "cantidadn";
		this.cantidadnueva.HeaderText = "CantidadNueva";
		this.cantidadnueva.Name = "cantidadnueva";
		this.cantidadnueva.Visible = false;
		this.txtFlete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFlete.Location = new System.Drawing.Point(403, 240);
		this.txtFlete.Name = "txtFlete";
		this.txtFlete.ReadOnly = true;
		this.txtFlete.Size = new System.Drawing.Size(75, 20);
		this.txtFlete.TabIndex = 65;
		this.txtFlete.Tag = "7";
		this.txtFlete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFlete_KeyPress);
		this.txtFlete.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFlete_KeyUp);
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(364, 242);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(33, 13);
		this.label19.TabIndex = 66;
		this.label19.Tag = "7";
		this.label19.Text = "Flete:";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(92, 240);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(689, 292);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(596, 295);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(689, 266);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(596, 269);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(264, 240);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(689, 240);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(596, 243);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(193, 243);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(48, 243);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator7.ErrorMessage = "Escoja la forma de pago.";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator9.ErrorMessage = "Escoja Moneda.";
		this.customValidator9.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator9.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator9_ValidateValue);
		this.customValidator8.ErrorMessage = "Ingrese Orden.";
		this.customValidator8.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator8.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator8_ValidateValue);
		this.customValidator5.ErrorMessage = "Ingrese el numero de documento.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator4.ErrorMessage = "Escoja un documento.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator2.ErrorMessage = "Escoja un proveedor.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Escoja la Transaccion.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator10.ErrorMessage = "Ingrese Detalle.";
		this.customValidator10.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator10.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator10_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator6.ErrorMessage = "Complete el campo requerido.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator3.ErrorMessage = "Escoja un cliente.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(806, 531);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmNotaIngresoPorOrden";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestión Factura de Compra";
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
