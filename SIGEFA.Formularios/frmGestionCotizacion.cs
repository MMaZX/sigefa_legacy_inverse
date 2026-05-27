using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

public class frmGestionCotizacion : Office2007Form
{
	internal clsUsuario usuario_click = null;

	private clsAdmSucursal admsucu = new clsAdmSucursal();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsProveedor prov = new clsProveedor();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmAutorizado AdmAut = new clsAdmAutorizado();

	private clsAutorizado aut = new clsAutorizado();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsAdmCotizacion AdmCoti = new clsAdmCotizacion();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsCotizacion cotizacion = new clsCotizacion();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie AdmSerie = new clsAdmSerie();

	private clsValidar ok = new clsValidar();

	private clsAdmListaPrecio admLista = new clsAdmListaPrecio();

	private clsListaPrecio Listap = new clsListaPrecio();

	private int CodLista = 0;

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsParametro param = new clsParametro();

	private clsAdmParametro admParam = new clsAdmParametro();

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	public clsUsuario vendedor;

	private clsAdmEmpresa admEmpresa = new clsAdmEmpresa();

	private clsAdmAlmacen admAlmacen = new clsAdmAlmacen();

	private clsAlmacen almacen = new clsAlmacen();

	public List<int> config = new List<int>();

	public List<clsDetalleCotizacion> detalle = new List<clsDetalleCotizacion>();

	public List<clsDetalleCotizacion> detalle1 = new List<clsDetalleCotizacion>();

	public string CodCotizacion;

	public int CodProveedor;

	public int CodCliente;

	public int CodDocumento;

	public int CodAutorizado;

	public int Tipo;

	private bool Validacion = true;

	public int Proceso = 0;

	public int Actua = 0;

	private List<int> ls = new List<int>();

	public decimal montodescuento = default(decimal);

	private clsConsultasExternas ext = new clsConsultasExternas();

	public int aprobado = 0;

	private decimal TipoCambio = default(decimal);

	private string Moneda = "";

	public string referenciaSinRegistro = "";

	public string nombreArchivo = "";

	private clsDocumentosImpresos ds = new clsDocumentosImpresos();

	private clsAdmParametroDescuento AdmDes = new clsAdmParametroDescuento();

	private object valorInicial = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	public TextBox txtDireccion;

	private TextBox txtCotizacion;

	private TextBox txtTipoCambio;

	private ComboBox cmbMoneda;

	private Label label16;

	private Label label17;

	private TextBox txtNombreCliente;

	private Label label15;

	private TextBox txtComentario;

	private Label label9;

	private DateTimePicker dtpFecha;

	private Label label1;

	private GroupBox groupBox4;

	private Button btnSalir;

	private GroupBox groupBox2;

	public DataGridView dgvDetalle;

	private Button btnNuevo;

	private ImageList imageList1;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Button btnNewCotizacion;

	private DateTimePicker dtpVigencia;

	private Label label2;

	private Button btnImprimir;

	private ComboBox cbListaPrecios;

	private Label label26;

	private ComboBox cmbFormaPago;

	private Label label3;

	public CheckBox chbAprobado;

	private ImageList imageList2;

	public Button btnAceptar;

	private Button btnGuardar;

	public TextBox txtDocRef;

	public Button btnEditar;

	public Button btnEliminar;

	public TextBox txtCodCliente;

	private Label label4;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CustomValidator customValidator1;

	private Button btnEnviar;

	private ImageList imageList3;

	private TextBox txtlugarentrega;

	private Label label5;

	private GroupBox groupBox5;

	private GroupBox groupBox3;

	private GroupBox groupBox6;

	private TextBox txtValorVenta;

	private Label label12;

	private Label label13;

	private TextBox txtIGV;

	private TextBox txtBruto;

	private Label label14;

	public TextBox txtDscto;

	private TextBox txtPrecioVenta;

	private Label label10;

	private GroupBox groupBox7;

	private Label label6;

	private TextBox txtcodvendedor;

	private TextBox txtvendedor;

	private Label lblmensaje;

	private Label lblsolocotizacion;

	public CheckBox chkdescuento;

	private Label lblseleccion;

	public TextBox txtdescuento;

	private Label label7;

	private Button btnrecalcular;

	public TextBox txtgananciamonto;

	private Label lblmargenmon;

	private Label lblmargenpor;

	private Button btnimprimirbasico;

	public TextBox txtgananciaporcentaje;

	private CheckBox chkTodos;

	private CheckBox chbverganancia;

	private GroupBox groupBox8;

	public Button btndescuento;

	private DataGridViewCheckBoxColumn descuento;

	private DataGridViewCheckBoxColumn separarstock2;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn stockDisp;

	private DataGridViewTextBoxColumn dias;

	private DataGridViewTextBoxColumn GananciaPorcentaje;

	private DataGridViewTextBoxColumn GananciaMonto;

	private DataGridViewTextBoxColumn CostoTotal;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn dsctoMax;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn CodMarca;

	private DataGridViewTextBoxColumn cotiza;

	private DataGridViewTextBoxColumn codtipoprecio;

	private DataGridViewTextBoxColumn codempresa;

	private CheckBox chbafectacion;

	public frmGestionCotizacion()
	{
		InitializeComponent();
		dtpVigencia.Value = dtpFecha.Value.AddDays(frmLogin.Configuracion.DiasVigencia);
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmDetalleSalida"] != null)
			{
				Application.OpenForms["frmDetalleSalida"].Activate();
				return;
			}
			frmDetalleSalida form = new frmDetalleSalida();
			form.Procede = 4;
			form.Proceso = 1;
			form.Tipo = 1;
			form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			form.Codlista = Convert.ToInt32(cbListaPrecios.SelectedValue);
			form.tc = tc.Compra;
			form.productoscotizados = detalle1;
			form.CodCliente = CodCliente;
			form.ShowDialog();
		}
		catch (Exception)
		{
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			dgvDetalle.Rows.Remove(dgvDetalle.SelectedRows[0]);
			calculatotales();
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		btnNuevo.Enabled = true;
		btnEliminar.Enabled = true;
		btnGuardar.Enabled = true;
		Actua = 1;
		chkdescuento.Enabled = true;
		txtdescuento.Enabled = true;
		btnrecalcular.Enabled = true;
		chkTodos.Enabled = true;
		btndescuento.Enabled = true;
	}

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		txtCodCliente.Text = cli.RucDni;
		txtNombreCliente.Text = cli.RazonSocial;
		txtDireccion.Text = cli.DireccionLegal;
	}

	private bool BuscaCliente()
	{
		cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
		if (cli != null)
		{
			txtCodCliente.Text = ((cli.Dni == "") ? cli.Ruc : cli.Dni);
			txtNombreCliente.Text = cli.RazonSocial;
			CodCliente = cli.CodCliente;
			txtDireccion.Text = cli.DireccionLegal;
			cbListaPrecios.SelectedValue = cli.CodListaPrecio;
			btnNuevo.Enabled = true;
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
		}
		txtlugarentrega.Focus();
	}

	private void txtCodCliente_Leave(object sender, EventArgs e)
	{
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

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		if (!txtTipoCambio.Visible)
		{
			return;
		}
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		if (Proceso == 1)
		{
			if (tc != null)
			{
				txtTipoCambio.Text = tc.Compra.ToString();
				return;
			}
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

	private void VerificarCabecera()
	{
		Validacion = true;
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
		dtpVigencia.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtCodCliente.Enabled = !estado;
		cmbMoneda.Enabled = !estado;
		txtCotizacion.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		txtgananciamonto.ReadOnly = estado;
		txtgananciaporcentaje.ReadOnly = estado;
		txtComentario.Enabled = !estado;
		cbListaPrecios.Enabled = !estado;
		btnNuevo.Enabled = !estado;
		txtdescuento.Enabled = !estado;
		btnEditar.Visible = estado;
		btnEditar.Enabled = estado;
		btnEliminar.Enabled = !estado;
		btnGuardar.Enabled = !estado;
		btndescuento.Enabled = !estado;
		chkTodos.Enabled = !estado;
		chkdescuento.Checked = !estado;
		btnImprimir.Visible = estado;
		btnimprimirbasico.Visible = estado;
		btnNewCotizacion.Visible = estado;
	}

	private void CargaDetalle()
	{
		DataTable newData = new DataTable();
		dgvDetalle.Rows.Clear();
		try
		{
			newData = AdmCoti.CargaDetalle(Convert.ToInt32(cotizacion.CodCotizacion), frmLogin.iCodAlmacen);
			foreach (DataRow row in newData.Rows)
			{
				dgvDetalle.Rows.Add(Convert.ToInt32(row[0]), row[1], row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString(), row[26].ToString(), row[27].ToString(), row[28].ToString(), row[29].ToString(), Convert.ToInt32(row[30]), row[31].ToString(), row[32].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
		cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null);
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedValue = 1;
	}

	private void frmGestionCotizacion_Load(object sender, EventArgs e)
	{
		if (aprobado == 0)
		{
			chbAprobado.Checked = false;
		}
		else
		{
			chbAprobado.Checked = true;
		}
		dtpFecha.MaxDate = DateTime.Today.Date;
		CargaMoneda();
		CargaFormaPagos();
		cargaVendedor();
		if (frmLogin.iNivelUser == 1)
		{
			chbverganancia.Checked = true;
		}
		else
		{
			chbverganancia.Checked = false;
		}
		if (Proceso == 1)
		{
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			lblmensaje.Visible = false;
			lblsolocotizacion.Visible = false;
			dgvdetalleEditable(editable: true);
		}
		else if (Proceso == 2)
		{
			lblmensaje.Visible = false;
			CargaCotizacion();
		}
		else if (Proceso == 3)
		{
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			CargaCotizacion();
			sololectura(estado: true);
		}
		txtCodCliente.Select();
	}

	private void CargaListaPreciosXFormaPago()
	{
		cbListaPrecios.DataSource = admLista.MuestraListaPrecioxFormaPago(frmLogin.iCodSucursal, Convert.ToInt32(cmbFormaPago.SelectedValue));
		cbListaPrecios.DisplayMember = "nombre";
		cbListaPrecios.ValueMember = "codListaPrecio";
		if (cbListaPrecios.Items.Count > 0)
		{
			cbListaPrecios.SelectedIndex = 0;
		}
	}

	private void frmGestionCotizacion_Shown(object sender, EventArgs e)
	{
		if ((Proceso == 1 || Proceso == 3) && txtTipoCambio.Visible && tc != null)
		{
			txtTipoCambio.Text = tc.Compra.ToString();
			TipoCambio = Convert.ToDecimal(txtTipoCambio.Text.Trim());
		}
		CargaListaPreciosXFormaPago();
		Moneda = cmbMoneda.Text;
	}

	private void CargaCotizacion()
	{
		try
		{
			cotizacion = AdmCoti.CargaCotizacion(Convert.ToInt32(CodCotizacion), frmLogin.iCodAlmacen);
			if (cotizacion != null)
			{
				doc = Admdoc.CargaTipoDocumento(cotizacion.DocRef);
				txtDocRef.Text = doc.Sigla + cotizacion.serie;
				txtCotizacion.Text = cotizacion.correlativo.ToString();
				if (txtCodCliente.Enabled)
				{
					CodCliente = cotizacion.CodCliente;
					txtCodCliente.Text = cotizacion.RUCCliente;
					txtNombreCliente.Text = cotizacion.Nombre;
					txtDireccion.Text = cotizacion.Direccion;
				}
				dtpFecha.Value = cotizacion.FechaCotizacion;
				dtpVigencia.Value = cotizacion.FechaVigencia;
				cmbMoneda.SelectedValue = cotizacion.Moneda;
				txtTipoCambio.Text = cotizacion.TipoCambio.ToString();
				cbListaPrecios.SelectedValue = cotizacion.CodListaPrecio;
				CargaVendedor(cotizacion.CodUser);
				txtComentario.Text = cotizacion.Comentario;
				txtlugarentrega.Text = cotizacion.SLugarEntrega;
				txtBruto.Text = $"{cotizacion.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{cotizacion.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{cotizacion.Total - cotizacion.Igv:#,##0.00}";
				txtIGV.Text = $"{cotizacion.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{cotizacion.Total:#,##0.00}";
				txtgananciamonto.Text = $"{cotizacion.Margendegananciamonto:#,##0.00}";
				txtgananciaporcentaje.Text = $"{cotizacion.Margendegananciaporcentaje:#,##0.00}";
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
			if (dgvDetalle.RowCount > 0)
			{
				int Indice = 0;
				Indice = dgvDetalle.RowCount - 1;
			}
		}
	}

	private void dgvdetalleEditable(bool editable)
	{
		foreach (DataGridViewColumn col in dgvDetalle.Columns)
		{
			col.ReadOnly = true;
			if (editable)
			{
				if (col.Name == cantidad.Name || col.Name == descuento.Name || col.Name == separarstock2.Name || col.Name == preciounit.Name)
				{
					col.ReadOnly = false;
				}
				else
				{
					col.ReadOnly = true;
				}
			}
		}
	}

	public void actualizaimportes()
	{
		if (Proceso == 0)
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
			preciovent += Convert.ToDecimal(row.Cells[total.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{igvt:#,##0.00}";
		txtPrecioVenta.Text = $"{preciovent:#,##0.00}";
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		switch (MessageBox.Show("¿Guardar los cambios en  Cotización?", "Emitir Cotización", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
		{
		case DialogResult.Yes:
			if (Proceso == 1)
			{
				if (txtcodvendedor.Text != "" && txtCodCliente.Text != "")
				{
					btnGuardar.PerformClick();
					Close();
				}
				else
				{
					MessageBox.Show("INGRESE UN CLIENTE PARA REGISTRAR COTIZACIÓN", "GUARDAR COTIZACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				btnGuardar.PerformClick();
				Close();
			}
			break;
		case DialogResult.No:
			Close();
			break;
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (!superValidator1.Validate())
			{
				return;
			}
			if (Proceso != 0 && txtcodvendedor.Text != "")
			{
				ser = AdmSerie.CargaSerieEmpresa(frmLogin.iCodAlmacen, doc.CodTipoDocumento);
				if (ser == null)
				{
					throw new Exception("\tError Serie de Venta\nNo se encontro una serie para registrar la venta.\nError: AdmSerie.CargaSerieEmpresa(" + frmLogin.iCodAlmacen + ", " + doc.CodTipoDocumento + ");");
				}
				cotizacion.codserie = ser.CodSerie;
				cotizacion.serie = ser.Serie;
				cotizacion.correlativo = ser.Numeracion;
				cotizacion.CodSucursal = frmLogin.iCodSucursal;
				cotizacion.CodAlmacen = frmLogin.iCodAlmacen;
				cotizacion.CodCliente = CodCliente;
				cotizacion.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				cotizacion.DocRef = 16;
				cotizacion.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
				cotizacion.FechaCotizacion = dtpFecha.Value.Date;
				cotizacion.FechaVigencia = dtpVigencia.Value.Date;
				cotizacion.Vigencia = dtpVigencia.Value.Day - dtpFecha.Value.Day;
				cotizacion.Comentario = txtComentario.Text;
				cotizacion.SLugarEntrega = txtlugarentrega.Text;
				cotizacion.CodListaPrecio = Convert.ToInt32(cbListaPrecios.SelectedValue);
				cotizacion.MontoBruto = Convert.ToDecimal(txtPrecioVenta.Text);
				cotizacion.MontoDscto = Convert.ToDecimal(txtDscto.Text);
				cotizacion.Igv = Convert.ToDecimal(txtIGV.Text);
				cotizacion.Total = Convert.ToDecimal(txtPrecioVenta.Text);
				cotizacion.CodUser = Convert.ToInt32(txtcodvendedor.Text);
				cotizacion.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
				cotizacion.Margendegananciamonto = Convert.ToDecimal(txtgananciamonto.Text);
				cotizacion.Margendegananciaporcentaje = ((txtgananciaporcentaje.Text == "∞") ? 100m : Convert.ToDecimal(txtgananciaporcentaje.Text));
				cotizacion.Estado = 1;
				if (cotizacion.MontoDscto > 0m)
				{
					if (frmLogin.iNivelUser == 1)
					{
						cotizacion.ValidaDescuentos1 = true;
					}
					else
					{
						cotizacion.ValidaDescuentos1 = false;
					}
				}
				else
				{
					cotizacion.ValidaDescuentos1 = true;
				}
				if (Proceso == 1 && Actua == 0)
				{
					List<object> alma = Enumerable.Select<DataGridViewRow, object>(dgvDetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, object>)((DataGridViewRow z) => z.Cells[codempresa.Name].Value)).Distinct().ToList();
					if (alma.Count > 1)
					{
						foreach (object em in alma)
						{
							ArmaCabecera(Convert.ToInt32(em));
							if (!AdmCoti.insert(cotizacion))
							{
								continue;
							}
							RecorreDetalleEmpresa(Convert.ToInt32(em));
							if (detalle.Count > 0)
							{
								foreach (clsDetalleCotizacion det in detalle)
								{
									AdmCoti.insertdetalle(det);
								}
							}
							almacen = admAlmacen.CargaAlmacen(Convert.ToInt32(em));
							MessageBox.Show("Los datos se guardaron correctamente " + cotizacion.serie + "-" + cotizacion.correlativo.ToString().PadLeft(5, '0') + " de " + almacen.Descripcion, "Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						Close();
					}
					else
					{
						if (!AdmCoti.insert(cotizacion))
						{
							return;
						}
						RecorreDetalle();
						if (detalle.Count > 0)
						{
							foreach (clsDetalleCotizacion det2 in detalle)
							{
								AdmCoti.insertdetalle(det2);
							}
						}
						MessageBox.Show("Los datos se guardaron correctamente", "Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						CodCotizacion = cotizacion.CodCotizacion;
						Proceso = 0;
						CargaCotizacion();
						sololectura(estado: true);
					}
				}
				else
				{
					if (Actua != 1 || !AdmCoti.update(cotizacion))
					{
						return;
					}
					RecorreDetalle();
					AdmCoti.deletedetalle(Convert.ToInt32(cotizacion.CodCotizacion));
					if (detalle.Count > 0)
					{
						foreach (clsDetalleCotizacion det3 in detalle)
						{
							AdmCoti.insertdetalle(det3);
						}
					}
					MessageBox.Show("Los datos se actualizaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show("Seleccionar un Vendedor", "Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void ArmaCabecera(int Codalmacen)
	{
		ser = AdmSerie.CargaSerieEmpresa(Codalmacen, doc.CodTipoDocumento);
		if (ser == null)
		{
			throw new Exception("\tError Serie de Venta\nNo se encontro una serie para registrar la venta.\nError: AdmSerie.CargaSerieEmpresa(" + Codalmacen + ", " + doc.CodTipoDocumento + ");");
		}
		cotizacion.codserie = ser.CodSerie;
		cotizacion.serie = doc.Sigla + ser.Serie;
		cotizacion.correlativo = ser.Numeracion;
		int codsucu = admsucu.sucursalxalmacen(Codalmacen);
		cotizacion.CodSucursal = codsucu;
		cotizacion.CodAlmacen = Codalmacen;
		cotizacion.CodCliente = CodCliente;
		cotizacion.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		cotizacion.DocRef = 16;
		cotizacion.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
		cotizacion.FechaCotizacion = dtpFecha.Value.Date;
		cotizacion.FechaVigencia = dtpVigencia.Value.Date;
		cotizacion.Vigencia = dtpVigencia.Value.Day - dtpFecha.Value.Day;
		cotizacion.Comentario = txtComentario.Text;
		cotizacion.SLugarEntrega = txtlugarentrega.Text;
		cotizacion.CodListaPrecio = Convert.ToInt32(cbListaPrecios.SelectedValue);
		cotizacion.CodUser = Convert.ToInt32(txtcodvendedor.Text);
		cotizacion.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
		cotizacion.Estado = 1;
		calculatotalesEmpresa(Codalmacen);
	}

	public void calculatotalesEmpresa(int Codalmacen)
	{
		if (Proceso == 0)
		{
			return;
		}
		double totalventa = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		double gananciaporcentaje = 0.0;
		double gananciamonto = 0.0;
		double preciocompra = 0.0;
		double totalgasto = 0.0;
		double totalprecioventa = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToInt32(row.Cells[codempresa.Name].Value) == Codalmacen)
			{
				totalventa += Convert.ToDouble(row.Cells[total.Name].Value);
				descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
				valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
				gananciamonto += Convert.ToDouble(row.Cells[GananciaMonto.Name].Value);
				totalgasto += Convert.ToDouble(row.Cells[CostoTotal.Name].Value);
				totalprecioventa += Convert.ToDouble(row.Cells[preciounit.Name].Value);
				preciocompra += Convert.ToDouble(row.Cells[dsctoMax.Name].Value);
			}
		}
		double Ganancia = (totalprecioventa - totalgasto) / 1.18;
		gananciaporcentaje = ((totalventa == 0.0) ? 0.0 : Math.Round(Ganancia / (preciocompra / 1.18) * 100.0, 2));
		cotizacion.MontoBruto = Convert.ToDecimal(totalventa);
		cotizacion.MontoDscto = Convert.ToDecimal(descuen);
		cotizacion.Igv = Convert.ToDecimal(valor * 0.18);
		cotizacion.Total = Convert.ToDecimal(totalventa);
		cotizacion.Margendegananciamonto = Convert.ToDecimal(gananciamonto);
		cotizacion.Margendegananciaporcentaje = Convert.ToDecimal(gananciaporcentaje);
		if (cotizacion.MontoDscto > 0m)
		{
			if (frmLogin.iNivelUser == 1)
			{
				cotizacion.ValidaDescuentos1 = true;
			}
			else
			{
				cotizacion.ValidaDescuentos1 = false;
			}
		}
		else
		{
			cotizacion.ValidaDescuentos1 = true;
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

	private void RecorreDetalleEmpresa(int Codalmacen)
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleEmpresa(row, Codalmacen);
		}
	}

	private void añadedetalleEmpresa(DataGridViewRow fila, int Codalmacen)
	{
		try
		{
			clsDetalleCotizacion deta = new clsDetalleCotizacion();
			if (Convert.ToInt32(fila.Cells[codempresa.Name].Value) == Codalmacen)
			{
				deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
				deta.Referencia = fila.Cells[referencia.Name].Value.ToString();
				if (deta.CodProducto == 1514)
				{
					deta.Descripcion = fila.Cells[descripcion.Name].Value.ToString();
				}
				else
				{
					deta.Descripcion = "";
				}
				deta.CodCotizacion = Convert.ToInt32(cotizacion.CodCotizacion);
				deta.CodAlmacen = frmLogin.iCodAlmacen;
				deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
				deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
				deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
				deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
				deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
				deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
				deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
				deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
				deta.Importe = Convert.ToDouble(fila.Cells[total.Name].Value);
				deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
				deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
				deta.CodUser = frmLogin.iCodUser;
				deta.CantidadPendiente = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
				deta.Stockdisponible = Convert.ToDecimal(fila.Cells[stockDisp.Name].Value);
				deta.Diasentrega = Convert.ToString(fila.Cells[dias.Name].Value);
				deta.Codmarca = Convert.ToInt32(fila.Cells[CodMarca.Name].Value);
				deta.Validadescuento = Convert.ToBoolean(fila.Cells[descuento.Name].Value);
				deta.Stockseparado = Convert.ToBoolean(fila.Cells[separarstock2.Name].Value);
				deta.Ganciamonto = Convert.ToDecimal(fila.Cells[GananciaMonto.Name].Value);
				deta.Ganciaporcentaje = Convert.ToDecimal(fila.Cells[GananciaPorcentaje.Name].Value);
				deta.Totalcosto = Convert.ToDecimal(fila.Cells[CostoTotal.Name].Value);
				deta.Cotizacion = Convert.ToBoolean(fila.Cells[cotiza.Name].Value);
				deta.codtipoprecio = Convert.ToInt32(fila.Cells[codtipoprecio.Name].Value);
				deta.preciocompra = Convert.ToInt32(fila.Cells[dsctoMax.Name].Value);
				detalle.Add(deta);
				if (deta.CodProducto != 1514)
				{
					detalle1.Add(deta);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		try
		{
			clsDetalleCotizacion deta = new clsDetalleCotizacion();
			deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			deta.Referencia = fila.Cells[referencia.Name].Value.ToString();
			if (deta.CodProducto == 1514)
			{
				deta.Descripcion = fila.Cells[descripcion.Name].Value.ToString();
			}
			else
			{
				deta.Descripcion = "";
			}
			deta.CodCotizacion = Convert.ToInt32(cotizacion.CodCotizacion);
			deta.CodAlmacen = frmLogin.iCodAlmacen;
			deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
			deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
			deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
			deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
			deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
			deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
			deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
			deta.Importe = Convert.ToDouble(fila.Cells[total.Name].Value);
			deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
			deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
			deta.CodUser = frmLogin.iCodUser;
			deta.CantidadPendiente = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
			deta.Stockdisponible = Convert.ToDecimal(fila.Cells[stockDisp.Name].Value);
			deta.Diasentrega = Convert.ToString(fila.Cells[dias.Name].Value);
			deta.Codmarca = Convert.ToInt32(fila.Cells[CodMarca.Name].Value);
			deta.Validadescuento = Convert.ToBoolean(fila.Cells[descuento.Name].Value);
			deta.Stockseparado = Convert.ToBoolean(fila.Cells[separarstock2.Name].Value);
			deta.Ganciamonto = Convert.ToDecimal(fila.Cells[GananciaMonto.Name].Value);
			deta.Ganciaporcentaje = Convert.ToDecimal(fila.Cells[GananciaPorcentaje.Name].Value);
			deta.Totalcosto = Convert.ToDecimal(fila.Cells[CostoTotal.Name].Value);
			deta.Cotizacion = Convert.ToBoolean(fila.Cells[cotiza.Name].Value);
			deta.codtipoprecio = Convert.ToInt32(fila.Cells[codtipoprecio.Name].Value);
			deta.preciocompra = Convert.ToDecimal(fila.Cells[dsctoMax.Name].Value);
			detalle.Add(deta);
			if (deta.CodProducto != 1514)
			{
				detalle1.Add(deta);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtCotizacion_Leave(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso == 1)
		{
			actualizaimportes();
		}
	}

	private void btnNewCotizacion_Click(object sender, EventArgs e)
	{
		Proceso = 1;
		sololectura(estado: false);
		txtCodCliente.Text = "";
		txtNombreCliente.Text = "";
		txtDireccion.Text = "";
	}

	private void txtComentario_Leave(object sender, EventArgs e)
	{
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		if (cotizacion.ValidaDescuentos1 || frmLogin.iNivelUser == 1)
		{
			frmrptCotizacion frm = new frmrptCotizacion();
			frm.CodCotizacion = Convert.ToInt32(cotizacion.CodCotizacion);
			frm.afectacion = chbafectacion.Checked;
			frm.tipo = 1;
			frm.formato = 0;
			frm.ShowDialog();
		}
		else
		{
			MessageBox.Show("No tiene Autorización , Solicite Validar Descuento", "COTIZACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void cbListaPrecios_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CodLista = Convert.ToInt32(cbListaPrecios.SelectedValue);
		actualizaprecios();
		calculatotales();
		activa_botones();
	}

	private void activa_botones()
	{
		btnEliminar.Enabled = true;
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
				if (codProduct != Convert.ToInt32(r["codProducto"].ToString()))
				{
					continue;
				}
				double precioa;
				double cantidada;
				double brutoa;
				double precioventaa;
				double montodescuentoa;
				double valorventaa;
				double igva;
				double precioreala;
				double valorreala;
				if (cmbMoneda.SelectedIndex == 1)
				{
					precioa = Convert.ToDouble(r["precio"]);
					row.Cells[preciounit.Name].Value = string.Format("{0:#,##0.00}", r["precio"]);
					cantidada = Convert.ToDouble(row.Cells[cantidad.Name].Value);
					brutoa = cantidada * precioa;
					row.Cells[importe.Name].Value = $"{brutoa:#,##0.00}";
					precioventaa = brutoa * (1.0 - Convert.ToDouble(row.Cells[dscto1.Name].Value) / 100.0) * (1.0 - Convert.ToDouble(row.Cells[dscto2.Name].Value) / 100.0) * (1.0 - Convert.ToDouble(row.Cells[dscto3.Name].Value) / 100.0);
					montodescuentoa = brutoa - precioventaa;
					row.Cells[montodscto.Name].Value = $"{montodescuentoa:#,##0.00}";
					if (r["precioneto"].ToString().Equals(r["precio"].ToString()))
					{
						valorventaa = precioventaa;
					}
					else
					{
						double factorigva = frmLogin.Configuracion.IGV / 100.0 + 1.0;
						valorventaa = precioventaa / factorigva;
					}
					igva = precioventaa - valorventaa;
					precioreala = precioventaa / cantidada;
					valorreala = valorventaa / cantidada;
					row.Cells[total.Name].Value = $"{precioventaa:#,##0.00}";
					row.Cells[valorventa.Name].Value = $"{valorventaa:#,##0.00}";
					row.Cells[precioreal.Name].Value = $"{precioreala:#,##0.00}";
					row.Cells[valoreal.Name].Value = $"{valorreala:#,##0.00}";
					continue;
				}
				precioa = Convert.ToDouble(r["precio"]) * Convert.ToDouble(txtTipoCambio.Text);
				row.Cells[preciounit.Name].Value = $"{precioa:#,##0.00}";
				cantidada = Convert.ToDouble(row.Cells[cantidad.Name].Value);
				brutoa = cantidada * precioa;
				row.Cells[importe.Name].Value = $"{brutoa:#,##0.00}";
				precioventaa = brutoa * (1.0 - Convert.ToDouble(row.Cells[dscto1.Name].Value) / 100.0) * (1.0 - Convert.ToDouble(row.Cells[dscto2.Name].Value) / 100.0) * (1.0 - Convert.ToDouble(row.Cells[dscto3.Name].Value) / 100.0);
				montodescuentoa = brutoa - precioventaa;
				row.Cells[montodscto.Name].Value = $"{montodescuentoa:#,##0.00}";
				if (r["precioneto"].ToString().Equals(r["precio"].ToString()))
				{
					valorventaa = precioventaa;
				}
				else
				{
					double factorigva = frmLogin.Configuracion.IGV / 100.0 + 1.0;
					valorventaa = precioventaa / factorigva;
				}
				igva = precioventaa - valorventaa;
				precioreala = precioventaa / cantidada;
				valorreala = valorventaa / cantidada;
				row.Cells[total.Name].Value = $"{precioventaa:#,##0.00}";
				row.Cells[valorventa.Name].Value = $"{valorventaa:#,##0.00}";
				row.Cells[precioreal.Name].Value = $"{precioreala:#,##0.00}";
				row.Cells[valoreal.Name].Value = $"{valorreala:#,##0.00}";
				row.Cells[igv.Name].Value = $"{igva:#,##0.00}";
			}
		}
	}

	public void calculatotales()
	{
		if (Proceso == 0)
		{
			return;
		}
		double totalventa = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		double gananciaporcentaje = 0.0;
		double gananciamonto = 0.0;
		double preciocompra = 0.0;
		double totalgasto = 0.0;
		double totalprecioventa = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			totalventa += Convert.ToDouble(row.Cells[total.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
			gananciamonto += Convert.ToDouble(row.Cells[GananciaMonto.Name].Value);
			totalgasto += Convert.ToDouble(row.Cells[CostoTotal.Name].Value);
			totalprecioventa += Convert.ToDouble(row.Cells[preciounit.Name].Value);
			preciocompra += Convert.ToDouble(row.Cells[dsctoMax.Name].Value);
		}
		double Ganancia = (totalprecioventa - totalgasto) / 1.18;
		gananciaporcentaje = ((totalventa == 0.0) ? 0.0 : Math.Round(Ganancia / (preciocompra / 1.18) * 100.0, 2));
		txtBruto.Text = $"{total:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{valor * 0.18:#,##0.00}";
		txtPrecioVenta.Text = $"{totalventa:#,##0.00}";
		txtgananciaporcentaje.Text = $"{gananciaporcentaje:#,##0.00}";
		txtgananciamonto.Text = $"{gananciamonto:#,##0.00}";
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

	private void dgvDetalle_Resize(object sender, EventArgs e)
	{
		int Tamaño = dgvDetalle.Size.Width - 1083;
		dgvDetalle.Columns["descripcion"].Width = 250 + Tamaño;
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaListaPreciosXFormaPago();
		EventArgs eeee = new EventArgs();
		cbListaPrecios_SelectionChangeCommitted(cbListaPrecios, eeee);
	}

	private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cmbMoneda.Text != Moneda && dgvDetalle.RowCount > 0)
		{
			CotizaEnMoneda();
		}
		Moneda = cmbMoneda.Text;
		actualizaimportes();
	}

	private void CotizaEnMoneda()
	{
		decimal TipoCambio = default(decimal);
		TipoCambio = Convert.ToDecimal(txtTipoCambio.Text.Trim());
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (cmbMoneda.SelectedIndex == 1)
			{
				row.Cells[preciounit.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) / TipoCambio;
				row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) / TipoCambio;
				row.Cells[montodscto.Name].Value = Convert.ToDecimal(row.Cells[montodscto.Name].Value) / TipoCambio;
				row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / TipoCambio;
				row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[igv.Name].Value) / TipoCambio;
				row.Cells[total.Name].Value = Convert.ToDecimal(row.Cells[total.Name].Value) / TipoCambio;
			}
			else if (cmbMoneda.SelectedIndex == 0)
			{
				row.Cells[preciounit.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) * TipoCambio;
				row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) * TipoCambio;
				row.Cells[montodscto.Name].Value = Convert.ToDecimal(row.Cells[montodscto.Name].Value) * TipoCambio;
				row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) * TipoCambio;
				row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[igv.Name].Value) * TipoCambio;
				row.Cells[total.Name].Value = Convert.ToDecimal(row.Cells[total.Name].Value) * TipoCambio;
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

	public void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
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

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		e.IsValid = true;
	}

	private void btnEnviar_Click(object sender, EventArgs e)
	{
		try
		{
			if (cotizacion.CodCotizacion == null)
			{
				return;
			}
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea enviar la Cotización", "Cotización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult == DialogResult.No)
			{
				return;
			}
			PdfRtfWordFormatOptions crformattype = new PdfRtfWordFormatOptions();
			DiskFileDestinationOptions dfoption = new DiskFileDestinationOptions();
			dfoption.DiskFileName = "C:\\Cotizaciones\\Cotizacion_" + cotizacion.CodCotizacion + ".pdf";
			ReportDocument document = new ReportDocument();
			CRCotizacion cot = new CRCotizacion();
			ExportOptions objexport = cot.ExportOptions;
			objexport.ExportDestinationType = ExportDestinationType.DiskFile;
			objexport.ExportFormatType = ExportFormatType.PortableDocFormat;
			objexport.DestinationOptions = dfoption;
			objexport.FormatOptions = crformattype;
			cot.Export();
			DirectoryInfo Dir = new DirectoryInfo("C:\\Cotizaciones");
			FileInfo[] files = Dir.GetFiles();
			foreach (FileInfo Fi in files)
			{
				if (Fi.Name.Contains(cotizacion.CodCotizacion))
				{
					nombreArchivo = Fi.Name;
				}
			}
			if (Application.OpenForms["frmCorreoElectronico"] != null)
			{
				Application.OpenForms["frmCorreoElectronico"].Activate();
				return;
			}
			frmCorreoElectronico form = new frmCorreoElectronico();
			form.link_adjunto.Text = nombreArchivo;
			form.txtcuerpo.Text = "ESTIMADOS SRs.: " + cotizacion.RazonSocialCliente + Environment.NewLine + Environment.NewLine + "\t LES ADJUNTO LA COTIZACIÓN.  N- " + cotizacion.CodCotizacion + "." + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "\t\t\t \t\t  ATT. " + Environment.NewLine + "\t\t\t\t" + frmLogin.sApellidoUSer + ", " + frmLogin.sNombreUser;
			form.tipo = 2;
			form.ShowDialog();
			if (form.enviado == 1)
			{
				MessageBox.Show("La Cotización ha envio correctamente", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("La Cotización, No se Pudo enviar, Verifique!", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
			MessageBox.Show("No se Pudo Enviar la Cotización", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			try
			{
				valorInicial = dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error al Editar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
		{
			return;
		}
		try
		{
			if (dgvDetalle.Columns[e.ColumnIndex].Name == "cantidad")
			{
				object valor = dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
				if (double.TryParse(valor.ToString(), out var nuevaCantidad))
				{
					if (nuevaCantidad < 0.0)
					{
						MessageBox.Show("La nueva cantidad debe ser mayor a cero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						if (valorInicial != null)
						{
							dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
							valorInicial = null;
						}
						return;
					}
					int codProd = Convert.ToInt32(dgvDetalle.Rows[e.RowIndex].Cells[codproducto.Name].Value);
					int codUnidad = Convert.ToInt32(dgvDetalle.Rows[e.RowIndex].Cells[codunidad.Name].Value);
					clsProducto product = AdmPro.ListaTotalprod2(codProd, frmLogin.iCodAlmacen, codUnidad);
					double totalprod = product.StockMaximo;
					if (nuevaCantidad != Convert.ToDouble((valorInicial == null) ? ((object)0) : valorInicial))
					{
						double cantidad = 0.0;
						double total = 0.0;
						double gananciamonto = 0.0;
						double preciocompra = 0.0;
						double gananciaporcentaje = 0.0;
						double totalcosto = 0.0;
						double precioUnitario = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[preciounit.Name].Value);
						preciocompra = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[dsctoMax.Name].Value);
						total = Math.Round(Convert.ToDouble(nuevaCantidad) * precioUnitario, 2);
						cantidad = Convert.ToDouble(nuevaCantidad);
						double bruto = total;
						totalcosto = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[CostoTotal.Name].Value);
						double Ganancia = Math.Round((Convert.ToDouble(precioUnitario) - Convert.ToDouble(totalcosto)) / 1.18, 2);
						gananciamonto = Math.Round(Convert.ToDouble(Ganancia) * cantidad, 2);
						gananciaporcentaje = Math.Round(Ganancia / (preciocompra / 1.18) * 100.0, 2);
						double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
						double valorventa = Math.Round(total / factorigv, 2);
						double igv = Math.Round(total - valorventa, 2);
						DataGridViewRow fila = dgvDetalle.Rows[e.RowIndex];
						fila.Cells[importe.Name].Value = bruto;
						fila.Cells[this.valorventa.Name].Value = valorventa;
						fila.Cells[this.igv.Name].Value = igv;
						fila.Cells[this.total.Name].Value = total;
						fila.Cells[GananciaPorcentaje.Name].Value = gananciaporcentaje;
						fila.Cells[GananciaMonto.Name].Value = gananciamonto;
						calculatotales();
					}
				}
				else
				{
					MessageBox.Show("El valor tiene que ser un numero", "Error de edicion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					if (valorInicial != null)
					{
						dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
						valorInicial = null;
					}
				}
			}
			else
			{
				if (!(dgvDetalle.Columns[e.ColumnIndex].Name == "preciounit"))
				{
					return;
				}
				double precioUnitario2 = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[preciounit.Name].Value);
				if (precioUnitario2 > 0.0 && Convert.ToString(precioUnitario2) != "")
				{
					double descuento = 0.0;
					double cantidadre = 0.0;
					double precioventabase = 0.0;
					double total2 = 0.0;
					double gananciamonto2 = 0.0;
					double gananciaporcentaje2 = 0.0;
					double preciocompra2 = 0.0;
					double totalcosto2 = 0.0;
					totalcosto2 = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[CostoTotal.Name].Value);
					cantidadre = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[this.cantidad.Name].Value);
					precioventabase = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[precioreal.Name].Value);
					preciocompra2 = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[dsctoMax.Name].Value);
					double Ganancia2 = Math.Round((Convert.ToDouble(precioUnitario2) - Convert.ToDouble(totalcosto2)) / 1.18, 2);
					gananciamonto2 = Math.Round(Convert.ToDouble(Ganancia2) * cantidadre, 2);
					gananciaporcentaje2 = Math.Round(Ganancia2 / (preciocompra2 / 1.18) * 100.0, 2);
					precioventabase = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[precioreal.Name].Value);
					total2 = Math.Round(cantidadre * precioUnitario2, 2);
					double bruto2 = total2;
					if (precioventabase < precioUnitario2)
					{
						descuento = 0.0;
					}
					else
					{
						param = admParam.ObtenerParametro(5);
						decimal descuentopermitido = Convert.ToDecimal(param.valor);
						if (!(descuentopermitido <= Convert.ToDecimal(txtPrecioVenta.Text)))
						{
							MessageBox.Show("No se puede Modificar a este precio de venta , no supera o es igual al precio total de cotización configurado", "Descuento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							DataGridViewRow fila2 = dgvDetalle.Rows[e.RowIndex];
							fila2.Cells[preciounit.Name].Value = precioventabase;
							return;
						}
						descuento = Math.Round(precioventabase - precioUnitario2, 2);
						if (!(Convert.ToDecimal(dgvDetalle.Rows[e.RowIndex].Cells[GananciaMonto.Name].Value) > Convert.ToDecimal(descuento)))
						{
							MessageBox.Show("No se puede Modificar a este precio de venta, Descuento de precio a realizar supera el margen de Ganancia.", "Descuento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							DataGridViewRow fila3 = dgvDetalle.Rows[e.RowIndex];
							fila3.Cells[preciounit.Name].Value = precioventabase;
							return;
						}
					}
					double factorigv2 = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					double valorventa2 = Math.Round(total2 / factorigv2, 2);
					double igv2 = Math.Round(total2 - valorventa2, 2);
					DataGridViewRow fila4 = dgvDetalle.Rows[e.RowIndex];
					fila4.Cells[this.valorventa.Name].Value = valorventa2;
					fila4.Cells[this.igv.Name].Value = igv2;
					fila4.Cells[this.total.Name].Value = total2;
					fila4.Cells[GananciaMonto.Name].Value = gananciamonto2;
					fila4.Cells[GananciaPorcentaje.Name].Value = gananciaporcentaje2;
					fila4.Cells[montodscto.Name].Value = descuento;
					calculatotales();
				}
				else
				{
					MessageBox.Show("Verifique precio Unitario", "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error al Editar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtcodvendedor_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.F1)
			{
				frmVendedoresLista frmDialog = new frmVendedoresLista();
				frmDialog.proc = 3;
				frmDialog.ShowDialog();
				if (vendedor != null)
				{
					txtcodvendedor.Text = vendedor.CodUsuario.ToString();
					txtvendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
				}
				else
				{
					txtcodvendedor.Text = "";
					txtvendedor.Text = "";
					MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error..." + ex.Message);
		}
	}

	private void cargaVendedor()
	{
		int codigoUsuario = Convert.ToInt32(frmLogin.iCodUser);
		vendedor = admUsuario.MuestraUsuarioSinAdmin(codigoUsuario);
		if (vendedor != null)
		{
			txtcodvendedor.Text = vendedor.CodUsuario.ToString();
			txtvendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
		}
		else
		{
			txtcodvendedor.Text = "";
			txtvendedor.Text = "";
			MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtcodvendedor_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Return && txtcodvendedor.Text.Trim() != "")
			{
				int codigoUsuario = Convert.ToInt32(txtcodvendedor.Text);
				vendedor = admUsuario.MuestraUsuarioSinAdmin(codigoUsuario);
				if (vendedor != null)
				{
					txtvendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
					return;
				}
				txtcodvendedor.Text = "";
				txtvendedor.Text = "";
				MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void CargaVendedor(int codvendedor)
	{
		vendedor = admUsuario.MuestraUsuarioSinAdmin(codvendedor);
		if (vendedor != null)
		{
			txtvendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
			txtcodvendedor.Text = codvendedor.ToString();
		}
	}

	private void dgvDetalle_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			double coti = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[cotiza.Name].Value ?? ((object)0));
			if (coti == 1.0)
			{
				dgvDetalle.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
				dgvDetalle.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
				lblsolocotizacion.Visible = true;
			}
		}
	}

	private void dgvDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		if (dgvDetalle.Columns[e.ColumnIndex].Name == "stockDisp" || dgvDetalle.Columns[e.ColumnIndex].Name == "cantidad")
		{
			double stockdisponible = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[stockDisp.Name].Value ?? ((object)0));
			double cantidadagregada = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[cantidad.Name].Value ?? ((object)0));
			if (stockdisponible < cantidadagregada)
			{
				e.CellStyle.BackColor = Color.Red;
				lblmensaje.Visible = true;
			}
		}
	}

	private void chkdescuento_Click(object sender, EventArgs e)
	{
		if (chkdescuento.Checked)
		{
			chkdescuento.Text = "Descuento (%)";
		}
		else
		{
			chkdescuento.Text = "Descuento (M)";
		}
	}

	private void calcularPendientesAlSeleccionar(DataGridViewCellEventArgs e)
	{
		try
		{
			if (!(dgvDetalle.Columns[e.ColumnIndex].Name == "descuento"))
			{
				return;
			}
			montodescuento = default(decimal);
			int n = e.RowIndex;
			dgvDetalle.ClearSelection();
			if (ls.Contains(n))
			{
				ls.Remove(n);
			}
			else
			{
				ls.Add(n);
			}
			foreach (int i in ls)
			{
				if (Convert.ToInt32(dgvDetalle.Rows[i].Cells[descuento.Name].Value) == 1)
				{
					montodescuento += Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value ?? ((object)0));
				}
			}
			lblseleccion.Text = montodescuento.ToString();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Seleccionar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtdescuento_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			int cantidaseleccionada = 0;
			decimal montoadescontar = default(decimal);
			decimal descuentopermitido = default(decimal);
			if (e.KeyCode != Keys.Return || !(txtDscto.Text.Trim() != ""))
			{
				return;
			}
			ls.Clear();
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				if (Convert.ToInt32(row.Cells["descuento"].Value) == 1)
				{
					ls.Add(row.Index);
				}
			}
			cantidaseleccionada = ls.Count;
			if (cantidaseleccionada > 0)
			{
				foreach (int i in ls)
				{
					if (Convert.ToDecimal(dgvDetalle.Rows[i].Cells[codtipoprecio.Name].Value) == 10m || Convert.ToDecimal(dgvDetalle.Rows[i].Cells[codtipoprecio.Name].Value) == 12m)
					{
						montoadescontar = default(decimal);
						montoadescontar = ((!chkdescuento.Checked) ? Convert.ToDecimal(txtdescuento.Text) : Math.Round(Convert.ToDecimal(txtdescuento.Text) * Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) / 100m, 2));
						montoadescontar = Math.Round(montoadescontar / Convert.ToDecimal(1.18), 2);
						param = admParam.ObtenerParametro(5);
						descuentopermitido = Convert.ToDecimal(param.valor);
						if (descuentopermitido <= Convert.ToDecimal(txtPrecioVenta.Text))
						{
							DataTable porcentajedescuento = new DataTable();
							decimal montoadescontarmaximo = default(decimal);
							int codemp = admEmpresa.empresaxalmacen(Convert.ToInt32(dgvDetalle.Rows[i].Cells[codempresa.Name].Value));
							porcentajedescuento = AdmDes.ListadoParametroDescuento(codemp, Convert.ToInt32(dgvDetalle.Rows[i].Cells[GananciaPorcentaje.Name].Value));
							montoadescontarmaximo = Math.Round(Convert.ToDecimal(porcentajedescuento.Rows[0]["Valor"]) * Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) / 100m, 2);
							if (montoadescontarmaximo > montoadescontar)
							{
								if (Convert.ToDecimal(dgvDetalle.Rows[i].Cells[GananciaMonto.Name].Value) > montoadescontar)
								{
									dgvDetalle.Rows[i].Cells[preciounit.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) - montoadescontar;
									dgvDetalle.Rows[i].Cells[importe.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) * Convert.ToDecimal(dgvDetalle.Rows[i].Cells[cantidad.Name].Value);
									dgvDetalle.Rows[i].Cells[valorventa.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[importe.Name].Value) / Convert.ToDecimal(1.18);
									dgvDetalle.Rows[i].Cells[total.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[importe.Name].Value);
									dgvDetalle.Rows[i].Cells[montodscto.Name].Value = montoadescontar * Convert.ToDecimal(dgvDetalle.Rows[i].Cells[cantidad.Name].Value);
									double Ganancia = (Convert.ToDouble(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) - Convert.ToDouble(dgvDetalle.Rows[i].Cells[CostoTotal.Name].Value)) / 1.18;
									dgvDetalle.Rows[i].Cells[GananciaMonto.Name].Value = Math.Round(Convert.ToDouble(dgvDetalle.Rows[i].Cells[cantidad.Name].Value) * Ganancia, 2);
									dgvDetalle.Rows[i].Cells[GananciaPorcentaje.Name].Value = Math.Round(Ganancia / (Convert.ToDouble(dgvDetalle.Rows[i].Cells[dsctoMax.Name].Value) / 1.18) * 100.0, 2);
								}
								else
								{
									MessageBox.Show("Ganancia es Menor al monto a descontar  del producto : " + Convert.ToString(dgvDetalle.Rows[i].Cells[descripcion.Name].Value), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
							}
							else
							{
								MessageBox.Show("Monto a descontar  es Mayor al monto Configurado del producto : " + Convert.ToString(dgvDetalle.Rows[i].Cells[descripcion.Name].Value), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
						else
						{
							MessageBox.Show("Monto Total de Venta es Menor a Monto configurado , Monto Total de venta Minimo para descuento es : " + param.valor.ToString(), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						calculatotales();
					}
					else
					{
						MessageBox.Show("Tipo Precio Venta del producto  : " + Convert.ToString(dgvDetalle.Rows[i].Cells[descripcion.Name].Value) + "  No Esta Permitido , Solo (PUBLICO Y CREDITO).", "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				return;
			}
			MessageBox.Show("Seleccionar Productos Para Descuento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnrecalcular_Click(object sender, EventArgs e)
	{
		DataTable newData = new DataTable();
		try
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				int codproduct = 0;
				int codunidad2 = 0;
				decimal precio = default(decimal);
				decimal stock = default(decimal);
				decimal valorvent = default(decimal);
				decimal cantidad2 = default(decimal);
				decimal totalventa = default(decimal);
				codproduct = Convert.ToInt32(row.Cells[codproducto.Name].Value);
				codunidad2 = Convert.ToInt32(row.Cells[codunidad.Name].Value);
				cantidad2 = Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				newData = AdmCoti.CargaDatosRecalcular(codproduct, codunidad2, frmLogin.iCodAlmacen);
				precio = Math.Round(Convert.ToDecimal(newData.Rows[0]["precio_venta"].ToString()), 2);
				stock = Math.Round(Convert.ToDecimal(newData.Rows[0]["stockdisponible"].ToString()), 2);
				valorvent = Math.Round(precio / Convert.ToDecimal(1.18), 2);
				totalventa = Math.Round(precio * cantidad2, 2);
				dgvDetalle.Rows[row.Index].Cells[preciounit.Name].Value = precio;
				dgvDetalle.Rows[row.Index].Cells[importe.Name].Value = totalventa;
				dgvDetalle.Rows[row.Index].Cells[valorventa.Name].Value = valorvent;
				dgvDetalle.Rows[row.Index].Cells[total.Name].Value = totalventa;
				dgvDetalle.Rows[row.Index].Cells[descuento.Name].Value = 0;
				dgvDetalle.Rows[row.Index].Cells[stockDisp.Name].Value = stock;
			}
			calculatotales();
			MessageBox.Show("Datos Actualizados", "Recalcular Precio y Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnimprimirbasico_Click(object sender, EventArgs e)
	{
		if (cotizacion.ValidaDescuentos1 || frmLogin.iNivelUser == 1)
		{
			frmrptCotizacion frm = new frmrptCotizacion();
			frm.CodCotizacion = Convert.ToInt32(cotizacion.CodCotizacion);
			frm.afectacion = chbafectacion.Checked;
			frm.tipo = 1;
			frm.formato = 1;
			frm.ShowDialog();
		}
		else
		{
			MessageBox.Show("No tiene Autorización , Solicite Validar Descuento", "COTIZACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void chkTodos_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataGridViewRow dr in (IEnumerable)dgvDetalle.Rows)
		{
			dr.Cells[0].Value = chkTodos.Checked;
		}
	}

	private void chbverganancia_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (chbverganancia.Checked && frmLogin.iNivelUser != 1)
			{
				usuario_click = null;
				frmAutorizacion frm = new frmAutorizacion();
				frm.tipoAccion = 2;
				int codPermiso = 140;
				frm.permiso = codPermiso;
				frm.PermitirAdministradores = true;
				frm.tipoVentanaAAsignarUsuario = 12;
				DialogResult dr = frm.ShowDialog();
				if (dr != DialogResult.OK || usuario_click == null)
				{
					return;
				}
				lblmargenmon.Visible = true;
				lblmargenpor.Visible = true;
				txtgananciamonto.Visible = true;
				txtgananciaporcentaje.Visible = true;
				{
					foreach (DataGridViewColumn col in dgvDetalle.Columns)
					{
						if (col.Name == GananciaPorcentaje.Name || col.Name == GananciaMonto.Name || col.Name == CostoTotal.Name)
						{
							col.Visible = true;
						}
					}
					return;
				}
			}
			if (chbverganancia.Checked && frmLogin.iNivelUser == 1)
			{
				lblmargenmon.Visible = true;
				lblmargenpor.Visible = true;
				txtgananciamonto.Visible = true;
				txtgananciaporcentaje.Visible = true;
				{
					foreach (DataGridViewColumn col2 in dgvDetalle.Columns)
					{
						if (col2.Name == GananciaPorcentaje.Name || col2.Name == GananciaMonto.Name || col2.Name == CostoTotal.Name)
						{
							col2.Visible = true;
						}
					}
					return;
				}
			}
			lblmargenmon.Visible = false;
			lblmargenpor.Visible = false;
			txtgananciamonto.Visible = false;
			txtgananciaporcentaje.Visible = false;
			foreach (DataGridViewColumn col3 in dgvDetalle.Columns)
			{
				if (col3.Name == GananciaPorcentaje.Name || col3.Name == GananciaMonto.Name || col3.Name == CostoTotal.Name)
				{
					col3.Visible = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btndescuento_Click(object sender, EventArgs e)
	{
		try
		{
			int cantidaseleccionada = 0;
			decimal montoadescontar = default(decimal);
			decimal descuentopermitido = default(decimal);
			DataTable porcentajedescuento = new DataTable();
			ls.Clear();
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				if (Convert.ToInt32(row.Cells["descuento"].Value) == 1)
				{
					ls.Add(row.Index);
				}
			}
			cantidaseleccionada = ls.Count;
			if (cantidaseleccionada > 0)
			{
				foreach (int i in ls)
				{
					if (Convert.ToDecimal(dgvDetalle.Rows[i].Cells[codtipoprecio.Name].Value) == 10m || Convert.ToDecimal(dgvDetalle.Rows[i].Cells[codtipoprecio.Name].Value) == 12m)
					{
						montoadescontar = default(decimal);
						int codemp = admEmpresa.empresaxalmacen(Convert.ToInt32(dgvDetalle.Rows[i].Cells[codempresa.Name].Value));
						porcentajedescuento = AdmDes.ListadoParametroDescuento(codemp, Convert.ToInt32(dgvDetalle.Rows[i].Cells[GananciaPorcentaje.Name].Value));
						montoadescontar = Math.Round(Convert.ToDecimal(porcentajedescuento.Rows[0]["Valor"]) * Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) / 100m, 2);
						param = admParam.ObtenerParametro(5);
						descuentopermitido = Convert.ToDecimal(param.valor);
						if (descuentopermitido <= Convert.ToDecimal(txtPrecioVenta.Text))
						{
							if (Convert.ToDecimal(dgvDetalle.Rows[i].Cells[GananciaMonto.Name].Value) > montoadescontar)
							{
								dgvDetalle.Rows[i].Cells[preciounit.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) - montoadescontar;
								dgvDetalle.Rows[i].Cells[importe.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) * Convert.ToDecimal(dgvDetalle.Rows[i].Cells[cantidad.Name].Value);
								dgvDetalle.Rows[i].Cells[valorventa.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[importe.Name].Value) / Convert.ToDecimal(1.18);
								dgvDetalle.Rows[i].Cells[total.Name].Value = Convert.ToDecimal(dgvDetalle.Rows[i].Cells[importe.Name].Value);
								dgvDetalle.Rows[i].Cells[montodscto.Name].Value = montoadescontar * Convert.ToDecimal(dgvDetalle.Rows[i].Cells[cantidad.Name].Value);
								double Ganancia = (Convert.ToDouble(dgvDetalle.Rows[i].Cells[preciounit.Name].Value) - Convert.ToDouble(dgvDetalle.Rows[i].Cells[CostoTotal.Name].Value)) / 1.18;
								dgvDetalle.Rows[i].Cells[GananciaMonto.Name].Value = Math.Round(Convert.ToDouble(dgvDetalle.Rows[i].Cells[cantidad.Name].Value) * Ganancia, 2);
								dgvDetalle.Rows[i].Cells[GananciaPorcentaje.Name].Value = Math.Round(Ganancia / (Convert.ToDouble(dgvDetalle.Rows[i].Cells[dsctoMax.Name].Value) / 1.18) * 100.0, 2);
							}
							else
							{
								MessageBox.Show("Ganancia es Menor al monto a descontar  del producto : " + Convert.ToString(dgvDetalle.Rows[i].Cells[descripcion.Name].Value), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
						else
						{
							MessageBox.Show("Monto Total de Venta es Menor a Monto configurado , Monto Total de venta Minimo para descuento es : " + param.valor.ToString(), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						calculatotales();
					}
					else
					{
						MessageBox.Show("Tipo Precio Venta del producto  : " + Convert.ToString(dgvDetalle.Rows[i].Cells[descripcion.Name].Value) + "  No Esta Permitido , Solo Precio (PUBLICO Y CREDITO).", "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				return;
			}
			MessageBox.Show("Seleccionar Productos Para Descuento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionCotizacion));
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.txtCotizacion = new System.Windows.Forms.TextBox();
		this.txtlugarentrega = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.chbAprobado = new System.Windows.Forms.CheckBox();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.cbListaPrecios = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.dtpVigencia = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.chbafectacion = new System.Windows.Forms.CheckBox();
		this.chbverganancia = new System.Windows.Forms.CheckBox();
		this.btnimprimirbasico = new System.Windows.Forms.Button();
		this.btnrecalcular = new System.Windows.Forms.Button();
		this.lblsolocotizacion = new System.Windows.Forms.Label();
		this.btnEditar = new System.Windows.Forms.Button();
		this.lblmensaje = new System.Windows.Forms.Label();
		this.btnEnviar = new System.Windows.Forms.Button();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.btnNuevo = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnImprimir = new System.Windows.Forms.Button();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.btnNewCotizacion = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.label10 = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.descuento = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.separarstock2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockDisp = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dias = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GananciaPorcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GananciaMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dsctoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodMarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cotiza = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codtipoprecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codempresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.txtgananciaporcentaje = new System.Windows.Forms.TextBox();
		this.txtgananciamonto = new System.Windows.Forms.TextBox();
		this.lblmargenmon = new System.Windows.Forms.Label();
		this.lblmargenpor = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.lblseleccion = new System.Windows.Forms.Label();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.txtPrecioVenta = new System.Windows.Forms.TextBox();
		this.chkTodos = new System.Windows.Forms.CheckBox();
		this.txtdescuento = new System.Windows.Forms.TextBox();
		this.chkdescuento = new System.Windows.Forms.CheckBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.txtcodvendedor = new System.Windows.Forms.TextBox();
		this.txtvendedor = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.btndescuento = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox7.SuspendLayout();
		this.groupBox8.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.BackColor = System.Drawing.Color.White;
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.txtCotizacion);
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(1016, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(431, 39);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "CABECERA";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.txtDocRef.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtDocRef.Enabled = false;
		this.txtDocRef.Location = new System.Drawing.Point(129, 12);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(76, 20);
		this.txtDocRef.TabIndex = 1;
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.txtCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCotizacion.Enabled = false;
		this.txtCotizacion.Location = new System.Drawing.Point(211, 12);
		this.txtCotizacion.Name = "txtCotizacion";
		this.txtCotizacion.ReadOnly = true;
		this.txtCotizacion.Size = new System.Drawing.Size(89, 20);
		this.txtCotizacion.TabIndex = 2;
		this.txtCotizacion.Tag = "20";
		this.txtCotizacion.Leave += new System.EventHandler(txtCotizacion_Leave);
		this.txtlugarentrega.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtlugarentrega.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtlugarentrega.Location = new System.Drawing.Point(94, 90);
		this.txtlugarentrega.Name = "txtlugarentrega";
		this.txtlugarentrega.Size = new System.Drawing.Size(326, 20);
		this.txtlugarentrega.TabIndex = 6;
		this.txtlugarentrega.Tag = "";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(9, 93);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(87, 13);
		this.label5.TabIndex = 67;
		this.label5.Text = "Lugar Entrega";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(9, 68);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 13);
		this.label4.TabIndex = 22;
		this.label4.Text = "Dirección:";
		this.chbAprobado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chbAprobado.AutoSize = true;
		this.chbAprobado.Location = new System.Drawing.Point(362, 21);
		this.chbAprobado.Name = "chbAprobado";
		this.chbAprobado.Size = new System.Drawing.Size(72, 17);
		this.chbAprobado.TabIndex = 25;
		this.chbAprobado.Text = "Aprobado";
		this.chbAprobado.UseVisualStyleBackColor = true;
		this.chbAprobado.Visible = false;
		this.cmbFormaPago.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmbFormaPago.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(279, 27);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(142, 22);
		this.cmbFormaPago.TabIndex = 11;
		this.cmbFormaPago.Tag = "";
		this.superValidator1.SetValidator1(this.cmbFormaPago, this.customValidator3);
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.cbListaPrecios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cbListaPrecios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbListaPrecios.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cbListaPrecios.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbListaPrecios.FormattingEnabled = true;
		this.cbListaPrecios.Location = new System.Drawing.Point(279, 56);
		this.cbListaPrecios.Name = "cbListaPrecios";
		this.cbListaPrecios.Size = new System.Drawing.Size(142, 22);
		this.cbListaPrecios.TabIndex = 13;
		this.superValidator1.SetValidator1(this.cbListaPrecios, this.customValidator4);
		this.cbListaPrecios.Visible = false;
		this.cbListaPrecios.SelectionChangeCommitted += new System.EventHandler(cbListaPrecios_SelectionChangeCommitted);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(174, 30);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(96, 13);
		this.label3.TabIndex = 62;
		this.label3.Text = "Forma de Pago:";
		this.label26.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(174, 59);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(72, 13);
		this.label26.TabIndex = 62;
		this.label26.Text = "Lista Prec.:";
		this.label26.Visible = false;
		this.dtpVigencia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpVigencia.Location = new System.Drawing.Point(302, 88);
		this.dtpVigencia.Name = "dtpVigencia";
		this.dtpVigencia.Size = new System.Drawing.Size(107, 20);
		this.dtpVigencia.TabIndex = 1;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(196, 91);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(95, 13);
		this.label2.TabIndex = 39;
		this.label2.Text = "Vigente Hasta :";
		this.txtDireccion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(94, 64);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(326, 20);
		this.txtDireccion.TabIndex = 5;
		this.txtDireccion.Tag = "0";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTipoCambio.Location = new System.Drawing.Point(84, 55);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(84, 21);
		this.txtTipoCambio.TabIndex = 12;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Arial", 8.25f);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(84, 27);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(84, 22);
		this.cmbMoneda.TabIndex = 10;
		this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(cmbMoneda_SelectionChangeCommitted);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(-2, 60);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(87, 13);
		this.label16.TabIndex = 32;
		this.label16.Text = "Tipo/Cambio :";
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(3, 30);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(60, 13);
		this.label17.TabIndex = 31;
		this.label17.Text = "Moneda :";
		this.txtNombreCliente.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(188, 32);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(232, 20);
		this.txtNombreCliente.TabIndex = 13;
		this.txtNombreCliente.Tag = "4";
		this.txtCodCliente.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Location = new System.Drawing.Point(95, 32);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(88, 20);
		this.txtCodCliente.TabIndex = 0;
		this.txtCodCliente.Tag = "3";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.customValidator1);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(9, 35);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(46, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Cliente";
		this.txtComentario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(95, 119);
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(325, 20);
		this.txtComentario.TabIndex = 7;
		this.txtComentario.Tag = "";
		this.txtComentario.Leave += new System.EventHandler(txtComentario_Leave);
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(10, 122);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(70, 13);
		this.label9.TabIndex = 17;
		this.label9.Text = "Comentario";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(84, 86);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(106, 20);
		this.dtpFecha.TabIndex = 4;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 88);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(50, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox4.BackColor = System.Drawing.Color.White;
		this.groupBox4.Controls.Add(this.chbafectacion);
		this.groupBox4.Controls.Add(this.chbverganancia);
		this.groupBox4.Controls.Add(this.btnimprimirbasico);
		this.groupBox4.Controls.Add(this.btnrecalcular);
		this.groupBox4.Controls.Add(this.lblsolocotizacion);
		this.groupBox4.Controls.Add(this.btnEditar);
		this.groupBox4.Controls.Add(this.lblmensaje);
		this.groupBox4.Controls.Add(this.btnEnviar);
		this.groupBox4.Controls.Add(this.btnNuevo);
		this.groupBox4.Controls.Add(this.btnAceptar);
		this.groupBox4.Controls.Add(this.btnImprimir);
		this.groupBox4.Controls.Add(this.txtBruto);
		this.groupBox4.Controls.Add(this.chbAprobado);
		this.groupBox4.Controls.Add(this.btnNewCotizacion);
		this.groupBox4.Controls.Add(this.btnEliminar);
		this.groupBox4.Controls.Add(this.label10);
		this.groupBox4.Controls.Add(this.btnSalir);
		this.groupBox4.Controls.Add(this.btnGuardar);
		this.groupBox4.Location = new System.Drawing.Point(3, 568);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(1444, 59);
		this.groupBox4.TabIndex = 0;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "ACCIONES";
		this.chbafectacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chbafectacion.AutoSize = true;
		this.chbafectacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chbafectacion.Location = new System.Drawing.Point(1004, 31);
		this.chbafectacion.Name = "chbafectacion";
		this.chbafectacion.Size = new System.Drawing.Size(89, 17);
		this.chbafectacion.TabIndex = 55;
		this.chbafectacion.Text = "Ver Sin Igv";
		this.chbafectacion.UseVisualStyleBackColor = true;
		this.chbverganancia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chbverganancia.AutoSize = true;
		this.chbverganancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chbverganancia.Location = new System.Drawing.Point(630, 31);
		this.chbverganancia.Name = "chbverganancia";
		this.chbverganancia.Size = new System.Drawing.Size(124, 17);
		this.chbverganancia.TabIndex = 54;
		this.chbverganancia.Text = "Ver (%) Ganancia";
		this.chbverganancia.UseVisualStyleBackColor = true;
		this.chbverganancia.CheckedChanged += new System.EventHandler(chbverganancia_CheckedChanged);
		this.btnimprimirbasico.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnimprimirbasico.Image = (System.Drawing.Image)resources.GetObject("btnimprimirbasico.Image");
		this.btnimprimirbasico.Location = new System.Drawing.Point(760, 21);
		this.btnimprimirbasico.Name = "btnimprimirbasico";
		this.btnimprimirbasico.Size = new System.Drawing.Size(116, 32);
		this.btnimprimirbasico.TabIndex = 53;
		this.btnimprimirbasico.Text = "Imprimir Simple";
		this.btnimprimirbasico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnimprimirbasico.UseVisualStyleBackColor = true;
		this.btnimprimirbasico.Visible = false;
		this.btnimprimirbasico.Click += new System.EventHandler(btnimprimirbasico_Click);
		this.btnrecalcular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnrecalcular.Image = (System.Drawing.Image)resources.GetObject("btnrecalcular.Image");
		this.btnrecalcular.Location = new System.Drawing.Point(1101, 22);
		this.btnrecalcular.Name = "btnrecalcular";
		this.btnrecalcular.Size = new System.Drawing.Size(96, 32);
		this.btnrecalcular.TabIndex = 52;
		this.btnrecalcular.Text = "Recalcular";
		this.btnrecalcular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnrecalcular.UseVisualStyleBackColor = true;
		this.btnrecalcular.Visible = false;
		this.btnrecalcular.Click += new System.EventHandler(btnrecalcular_Click);
		this.lblsolocotizacion.AutoSize = true;
		this.lblsolocotizacion.BackColor = System.Drawing.Color.Gray;
		this.lblsolocotizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblsolocotizacion.ForeColor = System.Drawing.SystemColors.Control;
		this.lblsolocotizacion.Location = new System.Drawing.Point(172, 40);
		this.lblsolocotizacion.Name = "lblsolocotizacion";
		this.lblsolocotizacion.Size = new System.Drawing.Size(183, 13);
		this.lblsolocotizacion.TabIndex = 50;
		this.lblsolocotizacion.Text = "Productos solo para Cotización";
		this.lblsolocotizacion.Visible = false;
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEditar.Enabled = false;
		this.btnEditar.Image = (System.Drawing.Image)resources.GetObject("btnEditar.Image");
		this.btnEditar.Location = new System.Drawing.Point(1203, 22);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(78, 32);
		this.btnEditar.TabIndex = 7;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Visible = false;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.lblmensaje.AutoSize = true;
		this.lblmensaje.BackColor = System.Drawing.Color.Red;
		this.lblmensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblmensaje.ForeColor = System.Drawing.SystemColors.Control;
		this.lblmensaje.Location = new System.Drawing.Point(172, 21);
		this.lblmensaje.Name = "lblmensaje";
		this.lblmensaje.Size = new System.Drawing.Size(184, 13);
		this.lblmensaje.TabIndex = 49;
		this.lblmensaje.Text = "Productos sin Stock Disponible";
		this.lblmensaje.Visible = false;
		this.btnEnviar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEnviar.ImageIndex = 17;
		this.btnEnviar.ImageList = this.imageList3;
		this.btnEnviar.Location = new System.Drawing.Point(430, 19);
		this.btnEnviar.Name = "btnEnviar";
		this.btnEnviar.Size = new System.Drawing.Size(17, 32);
		this.btnEnviar.TabIndex = 24;
		this.btnEnviar.Text = "Enviar";
		this.btnEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEnviar.UseVisualStyleBackColor = true;
		this.btnEnviar.Visible = false;
		this.btnEnviar.Click += new System.EventHandler(btnEnviar_Click);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList3.Images.SetKeyName(1, "Add.png");
		this.imageList3.Images.SetKeyName(2, "Remove.png");
		this.imageList3.Images.SetKeyName(3, "Write Document.png");
		this.imageList3.Images.SetKeyName(4, "New Document.png");
		this.imageList3.Images.SetKeyName(5, "Remove Document.png");
		this.imageList3.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList3.Images.SetKeyName(7, "document-print.png");
		this.imageList3.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList3.Images.SetKeyName(9, "refresh_256.png");
		this.imageList3.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList3.Images.SetKeyName(11, "search (1).png");
		this.imageList3.Images.SetKeyName(12, "search (5).png");
		this.imageList3.Images.SetKeyName(13, "search (6).png");
		this.imageList3.Images.SetKeyName(14, "search (8).png");
		this.imageList3.Images.SetKeyName(15, "search_top.png");
		this.imageList3.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList3.Images.SetKeyName(17, "Folder open.png");
		this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(13, 21);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "&Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList2;
		this.btnAceptar.Location = new System.Drawing.Point(453, 19);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(17, 31);
		this.btnAceptar.TabIndex = 21;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Visible = false;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "cross.png");
		this.imageList2.Images.SetKeyName(1, "tick.png");
		this.imageList2.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnImprimir.Image = (System.Drawing.Image)resources.GetObject("btnImprimir.Image");
		this.btnImprimir.Location = new System.Drawing.Point(882, 22);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(116, 32);
		this.btnImprimir.TabIndex = 23;
		this.btnImprimir.Text = "Imprimir Formal";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(471, 28);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(47, 20);
		this.txtBruto.TabIndex = 17;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtBruto.Visible = false;
		this.btnNewCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnNewCotizacion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNewCotizacion.ImageIndex = 1;
		this.btnNewCotizacion.ImageList = this.imageList1;
		this.btnNewCotizacion.Location = new System.Drawing.Point(527, 19);
		this.btnNewCotizacion.Name = "btnNewCotizacion";
		this.btnNewCotizacion.Size = new System.Drawing.Size(72, 32);
		this.btnNewCotizacion.TabIndex = 22;
		this.btnNewCotizacion.Text = "&Nuevo";
		this.btnNewCotizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNewCotizacion.UseVisualStyleBackColor = true;
		this.btnNewCotizacion.Visible = false;
		this.btnNewCotizacion.Click += new System.EventHandler(btnNewCotizacion_Click);
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(91, 22);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(480, 12);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.label10.Visible = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1370, 21);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 19;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(1287, 21);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 20;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.BackColor = System.Drawing.Color.White;
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1014, 562);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.HighlightText;
		this.dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
		this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(81, 124, 210);
		dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.descuento, this.separarstock2, this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.precioreal, this.preciounit, this.montodscto, this.valorventa, this.igv, this.total, this.importe, this.stockDisp, this.dias, this.GananciaPorcentaje, this.GananciaMonto, this.CostoTotal, this.serielote, this.dscto1, this.dscto2, this.dscto3, this.valoreal, this.dsctoMax, this.coduser, this.fecharegistro, this.CodMarca, this.cotiza, this.codtipoprecio, this.codempresa);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1008, 543);
		this.dgvDetalle.TabIndex = 1;
		this.dgvDetalle.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvDetalle_CellBeginEdit);
		this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(dgvDetalle_CellFormatting);
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetalle_DataError);
		this.dgvDetalle.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvDetalle_RowPostPaint);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.Resize += new System.EventHandler(dgvDetalle_Resize);
		this.descuento.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.descuento.HeaderText = "descuento";
		this.descuento.Name = "descuento";
		this.descuento.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.descuento.Width = 71;
		this.separarstock2.HeaderText = "Separar Stock";
		this.separarstock2.Name = "separarstock2";
		this.separarstock2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.separarstock2.Visible = false;
		this.coddetalle.DataPropertyName = "codDetalleCotizacion";
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
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.referencia.DefaultCellStyle = dataGridViewCellStyle18;
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 80;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 120;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 71;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle19.Format = "N2";
		dataGridViewCellStyle19.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle19;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 72;
		this.precioreal.DataPropertyName = "precioreal";
		dataGridViewCellStyle20.Format = "N4";
		this.precioreal.DefaultCellStyle = dataGridViewCellStyle20;
		this.precioreal.HeaderText = "P. Venta";
		this.precioreal.Name = "precioreal";
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle21.Format = "N4";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle21;
		this.preciounit.HeaderText = "Pv. Final";
		this.preciounit.Name = "preciounit";
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 71;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle22.Format = "N4";
		dataGridViewCellStyle22.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle22;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle23.Format = "N4";
		dataGridViewCellStyle23.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle23;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Width = 71;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle24.Format = "N4";
		this.igv.DefaultCellStyle = dataGridViewCellStyle24;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Width = 71;
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle25.Format = "N4";
		this.total.DefaultCellStyle = dataGridViewCellStyle25;
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.total.Width = 71;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle26.Format = "N4";
		dataGridViewCellStyle26.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle26;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Visible = false;
		this.importe.Width = 71;
		this.stockDisp.DataPropertyName = "stockdisponible";
		this.stockDisp.HeaderText = "StockDisponible";
		this.stockDisp.Name = "stockDisp";
		this.stockDisp.Width = 72;
		this.dias.DataPropertyName = "dias";
		this.dias.HeaderText = "Tiempo Entrega";
		this.dias.Name = "dias";
		this.dias.Width = 80;
		this.GananciaPorcentaje.DataPropertyName = "GananciaPorcentaje";
		this.GananciaPorcentaje.HeaderText = "GananciaPorcentaje";
		this.GananciaPorcentaje.Name = "GananciaPorcentaje";
		this.GananciaPorcentaje.Width = 120;
		this.GananciaMonto.DataPropertyName = "GananciaMonto";
		this.GananciaMonto.HeaderText = "GananciaMonto";
		this.GananciaMonto.Name = "GananciaMonto";
		this.GananciaMonto.Width = 110;
		this.CostoTotal.HeaderText = "CostoTotal";
		this.CostoTotal.Name = "CostoTotal";
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle27.Format = "N4";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle27;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle28.Format = "N4";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle28;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle29.Format = "N4";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle29;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.valoreal.DataPropertyName = "valoreal";
		dataGridViewCellStyle30.Format = "N4";
		this.valoreal.DefaultCellStyle = dataGridViewCellStyle30;
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.dsctoMax.DataPropertyName = "maxPorcDescto";
		this.dsctoMax.HeaderText = "%DsctoMax";
		this.dsctoMax.Name = "dsctoMax";
		this.dsctoMax.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.CodMarca.HeaderText = "CodMarca";
		this.CodMarca.Name = "CodMarca";
		this.CodMarca.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.CodMarca.Visible = false;
		this.cotiza.DataPropertyName = "cotiza";
		this.cotiza.HeaderText = "cotiza";
		this.cotiza.Name = "cotiza";
		this.cotiza.Visible = false;
		this.codtipoprecio.HeaderText = "codtipoprecio";
		this.codtipoprecio.Name = "codtipoprecio";
		this.codtipoprecio.Visible = false;
		this.codempresa.DataPropertyName = "codempresa";
		this.codempresa.HeaderText = "CodAlmacen";
		this.codempresa.Name = "codempresa";
		this.codempresa.Visible = false;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator3.ErrorMessage = "Seleccione forma de pago.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator4.ErrorMessage = "Seleccione lista de precios.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese el cliente.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.txtgananciaporcentaje);
		this.groupBox3.Controls.Add(this.txtgananciamonto);
		this.groupBox3.Controls.Add(this.lblmargenmon);
		this.groupBox3.Controls.Add(this.lblmargenpor);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.lblseleccion);
		this.groupBox3.Controls.Add(this.txtValorVenta);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Controls.Add(this.txtIGV);
		this.groupBox3.Controls.Add(this.label14);
		this.groupBox3.Controls.Add(this.txtDscto);
		this.groupBox3.Controls.Add(this.txtPrecioVenta);
		this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(1016, 389);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(434, 91);
		this.groupBox3.TabIndex = 0;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "TOTALES";
		this.txtgananciaporcentaje.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgananciaporcentaje.Location = new System.Drawing.Point(315, 70);
		this.txtgananciaporcentaje.Name = "txtgananciaporcentaje";
		this.txtgananciaporcentaje.ReadOnly = true;
		this.txtgananciaporcentaje.Size = new System.Drawing.Size(112, 20);
		this.txtgananciaporcentaje.TabIndex = 30;
		this.txtgananciaporcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtgananciamonto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgananciamonto.Location = new System.Drawing.Point(96, 65);
		this.txtgananciamonto.Name = "txtgananciamonto";
		this.txtgananciamonto.ReadOnly = true;
		this.txtgananciamonto.Size = new System.Drawing.Size(109, 20);
		this.txtgananciamonto.TabIndex = 29;
		this.txtgananciamonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lblmargenmon.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblmargenmon.AutoSize = true;
		this.lblmargenmon.Location = new System.Drawing.Point(-1, 70);
		this.lblmargenmon.Name = "lblmargenmon";
		this.lblmargenmon.Size = new System.Drawing.Size(97, 13);
		this.lblmargenmon.TabIndex = 28;
		this.lblmargenmon.Text = "M.Ganancia(M):";
		this.lblmargenpor.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblmargenpor.AutoSize = true;
		this.lblmargenpor.Location = new System.Drawing.Point(218, 72);
		this.lblmargenpor.Name = "lblmargenpor";
		this.lblmargenpor.Size = new System.Drawing.Size(96, 13);
		this.lblmargenpor.TabIndex = 26;
		this.lblmargenpor.Text = "M.Ganancia(%):";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(217, 43);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(92, 13);
		this.label7.TabIndex = 24;
		this.label7.Text = "T. Descuento :";
		this.lblseleccion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblseleccion.AutoSize = true;
		this.lblseleccion.Location = new System.Drawing.Point(324, 75);
		this.lblseleccion.Name = "lblseleccion";
		this.lblseleccion.Size = new System.Drawing.Size(0, 13);
		this.lblseleccion.TabIndex = 23;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.ForeColor = System.Drawing.SystemColors.InfoText;
		this.txtValorVenta.Location = new System.Drawing.Point(94, 17);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 14;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(23, 21);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(64, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(39, 45);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(48, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.ForeColor = System.Drawing.SystemColors.InfoText;
		this.txtIGV.Location = new System.Drawing.Point(94, 42);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 15;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(265, 17);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(44, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "Total :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(315, 40);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(111, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.ForeColor = System.Drawing.SystemColors.InfoText;
		this.txtPrecioVenta.Location = new System.Drawing.Point(315, 14);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 16;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.chkTodos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.chkTodos.AutoSize = true;
		this.chkTodos.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.chkTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkTodos.Location = new System.Drawing.Point(0, 25);
		this.chkTodos.Name = "chkTodos";
		this.chkTodos.Size = new System.Drawing.Size(46, 31);
		this.chkTodos.TabIndex = 11;
		this.chkTodos.Text = "Todos";
		this.chkTodos.UseVisualStyleBackColor = true;
		this.chkTodos.CheckedChanged += new System.EventHandler(chkTodos_CheckedChanged);
		this.txtdescuento.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtdescuento.Location = new System.Drawing.Point(51, 42);
		this.txtdescuento.Name = "txtdescuento";
		this.txtdescuento.Size = new System.Drawing.Size(114, 20);
		this.txtdescuento.TabIndex = 25;
		this.txtdescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtdescuento.KeyDown += new System.Windows.Forms.KeyEventHandler(txtdescuento_KeyDown);
		this.chkdescuento.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.chkdescuento.AutoSize = true;
		this.chkdescuento.Location = new System.Drawing.Point(52, 19);
		this.chkdescuento.Name = "chkdescuento";
		this.chkdescuento.Size = new System.Drawing.Size(113, 17);
		this.chkdescuento.TabIndex = 22;
		this.chkdescuento.Text = "Descuento (M):";
		this.chkdescuento.UseVisualStyleBackColor = true;
		this.chkdescuento.Click += new System.EventHandler(chkdescuento_Click);
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox5.Controls.Add(this.txtlugarentrega);
		this.groupBox5.Controls.Add(this.label5);
		this.groupBox5.Controls.Add(this.txtNombreCliente);
		this.groupBox5.Controls.Add(this.label15);
		this.groupBox5.Controls.Add(this.label4);
		this.groupBox5.Controls.Add(this.txtCodCliente);
		this.groupBox5.Controls.Add(this.txtDireccion);
		this.groupBox5.Controls.Add(this.label9);
		this.groupBox5.Controls.Add(this.txtComentario);
		this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox5.Location = new System.Drawing.Point(1017, 40);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(430, 152);
		this.groupBox5.TabIndex = 0;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "DATOS DEL CLIENTE";
		this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox6.Controls.Add(this.label3);
		this.groupBox6.Controls.Add(this.dtpVigencia);
		this.groupBox6.Controls.Add(this.label2);
		this.groupBox6.Controls.Add(this.label26);
		this.groupBox6.Controls.Add(this.cmbFormaPago);
		this.groupBox6.Controls.Add(this.dtpFecha);
		this.groupBox6.Controls.Add(this.txtTipoCambio);
		this.groupBox6.Controls.Add(this.label1);
		this.groupBox6.Controls.Add(this.cmbMoneda);
		this.groupBox6.Controls.Add(this.label16);
		this.groupBox6.Controls.Add(this.cbListaPrecios);
		this.groupBox6.Controls.Add(this.label17);
		this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox6.Location = new System.Drawing.Point(1017, 248);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(431, 135);
		this.groupBox6.TabIndex = 0;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "CONDICIONES";
		this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox7.Controls.Add(this.txtcodvendedor);
		this.groupBox7.Controls.Add(this.txtvendedor);
		this.groupBox7.Controls.Add(this.label6);
		this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox7.Location = new System.Drawing.Point(1016, 198);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(427, 44);
		this.groupBox7.TabIndex = 0;
		this.groupBox7.TabStop = false;
		this.txtcodvendedor.Location = new System.Drawing.Point(94, 16);
		this.txtcodvendedor.Name = "txtcodvendedor";
		this.txtcodvendedor.Size = new System.Drawing.Size(49, 20);
		this.txtcodvendedor.TabIndex = 70;
		this.txtcodvendedor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtcodvendedor_KeyDown);
		this.txtcodvendedor.KeyUp += new System.Windows.Forms.KeyEventHandler(txtcodvendedor_KeyUp);
		this.txtvendedor.Location = new System.Drawing.Point(149, 16);
		this.txtvendedor.Name = "txtvendedor";
		this.txtvendedor.Size = new System.Drawing.Size(161, 20);
		this.txtvendedor.TabIndex = 69;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(13, 19);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 13);
		this.label6.TabIndex = 68;
		this.label6.Text = "Vendedor:";
		this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox8.Controls.Add(this.btndescuento);
		this.groupBox8.Controls.Add(this.chkTodos);
		this.groupBox8.Controls.Add(this.txtdescuento);
		this.groupBox8.Controls.Add(this.chkdescuento);
		this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox8.Location = new System.Drawing.Point(1017, 486);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(427, 76);
		this.groupBox8.TabIndex = 71;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "Aplicar Descuento";
		this.btndescuento.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btndescuento.Image = (System.Drawing.Image)resources.GetObject("btndescuento.Image");
		this.btndescuento.Location = new System.Drawing.Point(188, 30);
		this.btndescuento.Name = "btndescuento";
		this.btndescuento.Size = new System.Drawing.Size(120, 32);
		this.btndescuento.TabIndex = 55;
		this.btndescuento.Text = "Descuento";
		this.btndescuento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btndescuento.UseVisualStyleBackColor = true;
		this.btndescuento.Click += new System.EventHandler(btndescuento_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1450, 639);
		base.Controls.Add(this.groupBox8);
		base.Controls.Add(this.groupBox7);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox4);
		this.DoubleBuffered = true;
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MinimizeBox = false;
		base.Name = "frmGestionCotizacion";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Cotizacion";
		base.Load += new System.EventHandler(frmGestionCotizacion_Load);
		base.Shown += new System.EventHandler(frmGestionCotizacion_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		this.groupBox8.ResumeLayout(false);
		this.groupBox8.PerformLayout();
		base.ResumeLayout(false);
	}
}
