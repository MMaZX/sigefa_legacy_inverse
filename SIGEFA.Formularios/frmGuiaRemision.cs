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

public class frmGuiaRemision : Office2007Form
{
	private clsGuia ds = new clsGuia();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsSerie ser = new clsSerie();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmEmpresaTransporte AdmET = new clsAdmEmpresaTransporte();

	private clsEmpresaTransporte empT = new clsEmpresaTransporte();

	private clsAdmConductor AdmCond = new clsAdmConductor();

	private clsConductor cond = new clsConductor();

	private clsAdmVehiculoTransporte AdmVeh = new clsAdmVehiculoTransporte();

	private clsVehiculoTransporte veh = new clsVehiculoTransporte();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProveedor prov = new clsProveedor();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmGuiaRemision AdmGuia = new clsAdmGuiaRemision();

	private clsGuiaRemision guia = new clsGuiaRemision();

	private clsDetalleGuiaRemision dtguia = new clsDetalleGuiaRemision();

	private clsDetalleOrdenCompra dtorco = new clsDetalleOrdenCompra();

	private clsPedido pedido = new clsPedido();

	private clsAdmPedido Admped = new clsAdmPedido();

	private clsValidar ok = new clsValidar();

	private clsNotaIngreso nota = new clsNotaIngreso();

	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsAdmNotaSalida AdmNotas = new clsAdmNotaSalida();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsTransferencia clstran = new clsTransferencia();

	private clsAdmTransferencia AdmTransf = new clsAdmTransferencia();

	private DataTable datosCarga = new DataTable();

	private DataTable datosCarga2 = new DataTable();

	private DataTable datosAlmacena = new DataTable();

	public static BindingSource data = new BindingSource();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsFacturaVenta facturav = new clsFacturaVenta();

	private clsAdmOrdenCompra AdmOrden = new clsAdmOrdenCompra();

	private clsOrdenCompra Ord = new clsOrdenCompra();

	private clsDetalleOrdenCompra or = new clsDetalleOrdenCompra();

	private clsDetalleGuiaRemision detalleguia = new clsDetalleGuiaRemision();

	private frmOrdenCompra frmorden = new frmOrdenCompra();

	private TextBox txtedit = new TextBox();

	private bool prosoli = true;

	public int CodOrdenCompra;

	public clsNotaIngreso notaI = new clsNotaIngreso();

	public List<int> config = new List<int>();

	public List<clsDetalleGuiaRemision> detalle = new List<clsDetalleGuiaRemision>();

	public List<clsDetalleNotaIngreso> detallei = new List<clsDetalleNotaIngreso>();

	public string CodGuia;

	public int CodVehiculo;

	public int CodConductor;

	public int CodEmpresaTransporte;

	public int CodTransaccion;

	public int numeroGuia;

	public int CodProveedor;

	public int CodCliente;

	public int CodDocumento;

	public int CodPedido;

	public int CodTranferencia;

	public int CodSerie;

	public int Tipo;

	public int CodNota;

	public int CodVenta;

	private bool Validacion = true;

	public int Proceso = 0;

	public int salir = 1;

	public int recorre = 200;

	public string rpta = "";

	public string rpta2 = "";

	private string dat = "";

	private string dat2 = "";

	public List<int> lta = new List<int>();

	private int codmovi = 0;

	private DialogResult o = DialogResult.None;

	private int i = 0;

	private string vbcrlf;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnDetalle;

	private TextBox txtComentario;

	private Label label9;

	private Label label7;

	private DateTimePicker dtpFecha;

	private Label label1;

	private ImageList imageList1;

	private GroupBox groupBox2;

	private TextBox txtPedido;

	private Label label8;

	public DataGridView dgvDetalle;

	private Label label4;

	public TextBox txtCodCliente;

	public TextBox txtNombreCliente;

	public TextBox txtDireccion;

	private GroupBox groupBox4;

	private Label label3;

	private Label label10;

	private Label label6;

	private TextBox txtConstancia;

	private Label label11;

	private TextBox txtLicencia;

	private Label label12;

	private GroupBox groupBox5;

	public TextBox txtDireccionTransporte;

	private Label label16;

	public TextBox txtRazonSocialTransporte;

	private Label label14;

	private Label label13;

	public TextBox txtRUCTransporte;

	private DateTimePicker dtpFechaTransporte;

	private Label label17;

	private ComboBox cmbMotivo;

	private Label label18;

	private TextBox txtFactura;

	private Label label2;

	private TextBox txtMarcaVehiculo;

	public TextBox txtNumDoc;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private ComboBox cmbVehiculos;

	private ComboBox cmbConductor;

	private Label label5;

	private TextBox txtNumero;

	public TextBox txtSerie;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CustomValidator customValidator5;

	private TextBox txtTranferencia;

	private Label label19;

	public TextBox txtnumeroOc;

	public Label label20;

	public Label label15;

	private DateTimePicker dtfecharegistro;

	private Label label22;

	private DateTimePicker dtfechaingresoalmacen;

	private Label label21;

	private Button btnSalir;

	private GroupBox groupBox3;

	private Button btGenVenta;

	private Button btnNuevaGuia;

	private Button btnImprimir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnEliminar;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn cantidadnueva;

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

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn cantidadpendiente;

	private DataGridViewTextBoxColumn peso;

	private DataGridViewTextBoxColumn stockPend;

	private DataGridViewTextBoxColumn maxPorcDes;

	private DataGridViewTextBoxColumn codigoventa;

	private DataGridViewTextBoxColumn numorden;

	private DataGridViewTextBoxColumn productosolicitado;

	private DataGridViewTextBoxColumn EstadoDeLaOrden;

	private DataGridViewComboBoxColumn etiqueta;

	public bool ProdSolicitado { get; set; }

	public ComboBox sender { get; private set; }

	public frmGuiaRemision()
	{
		InitializeComponent();
	}

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		if (cli != null)
		{
			txtCodCliente.Text = cli.RucDni;
			txtNombreCliente.Text = cli.RazonSocial;
			if (cli.DireccionEntrega.Trim() != "")
			{
				txtDireccion.Text = cli.DireccionEntrega;
			}
			else
			{
				txtDireccion.Text = cli.DireccionLegal;
			}
		}
	}

	private bool BuscaCliente()
	{
		cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
		if (cli != null)
		{
			txtNombreCliente.Text = cli.RazonSocial;
			CodCliente = cli.CodCliente;
			if (cli.DireccionEntrega != "")
			{
				txtDireccion.Text = cli.DireccionEntrega;
			}
			else
			{
				txtDireccion.Text = cli.DireccionLegal;
			}
			return true;
		}
		txtNombreCliente.Text = "";
		CodCliente = 0;
		txtDireccion.Text = "";
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
			ProcessTabKey(forward: true);
		}
	}

	private void txtCodCliente_Leave(object sender, EventArgs e)
	{
		if (CodCliente == 0)
		{
			txtCodCliente.Focus();
		}
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnDetalle.Enabled = true;
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
				MessageBox.Show("El Cliente no existe, Presione F1 para consultar la tabla de ayuda", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
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

	private void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
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
		if (txtCodCliente.Visible && CodCliente == 0)
		{
			Validacion = false;
		}
		if ((cond.CodConductor == 0 || veh.CodVehiculoTransporte == 0) && empT.CodEmpresaTranporte == 0)
		{
			Validacion = false;
		}
		if (cmbMotivo.SelectedIndex == -1)
		{
			Validacion = false;
		}
	}

	private void frmGuiaRemision_Load(object sender, EventArgs e)
	{
		CargaDeDatosColumnaComboBox();
		CargaVehiculosTransporte();
		CargaConductores();
		EventArgs eee = new EventArgs();
		cmbConductor_SelectionChangeCommitted(cmbConductor, eee);
		cmbVehiculos_SelectionChangeCommitted(cmbVehiculos, eee);
		if (Proceso == 1)
		{
			cmbMotivo.SelectedIndex = 0;
		}
		activamenu(valor: false);
		if (Proceso == 2)
		{
			CargaGuiaRemision();
			activamenu(valor: true);
		}
		else if (Proceso == 3)
		{
			CargaGuiaRemision();
			sololectura(estado: true);
			activamenu(valor: false);
		}
		else if (Proceso == 4)
		{
			CargaGuiaRemision();
			sololectura(estado: true);
			activamenu(valor: false);
		}
	}

	private void CargaDeDatosColumnaComboBox()
	{
		etiqueta.DataSource = AdmPro.cargaetiquetauna(frmLogin.iCodAlmacen);
		etiqueta.DisplayMember = "descripcion";
		etiqueta.ValueMember = "Id_etiqueta";
	}

	private void activamenu(bool valor)
	{
		btnNuevo.Visible = valor;
		btnEditar.Visible = valor;
		btnEliminar.Visible = valor;
		llenarcabecera();
		if (Ord != null)
		{
			label15.Text = "Proveedor";
			btnEliminar.Visible = true;
			btnEliminar.Enabled = true;
		}
		else
		{
			btnEliminar.Visible = true;
			btnEliminar.Enabled = true;
		}
	}

	public void llenarcabecera()
	{
		Ord = AdmOrden.CargaOrdenCompra(Convert.ToInt32(CodOrdenCompra));
		if (Ord != null)
		{
			txtnumeroOc.Text = Convert.ToString(Ord.CodOrdenCompra);
			txtCodCliente.Text = Ord.RUCProveedor;
			txtNombreCliente.Text = Ord.RazonSocialProveedor;
			btnDetalle.Enabled = true;
			guia.CodProveedor = Ord.CodProveedor;
			guia.CodSerie = Ord.CodSerie;
			txtSerie.Text = Ord.Serie;
			guia.CodGuiaRemision = CodGuia;
			CodOrdenCompra = Ord.CodOrdenCompra;
			frmorden.Ord.CodOrdenCompra = CodOrdenCompra;
			CargaDetalleOrden();
			if (or.cantidadpendiente <= 0m)
			{
				btnGuardar.Visible = true;
			}
			if (Proceso == 1)
			{
				dgvDetalle.Columns["EstadoDeLaOrden"].Visible = false;
				dgvDetalle.Columns["etiqueta"].Visible = true;
				dgvDetalle.Columns["cantidadpendiente"].Visible = true;
			}
		}
	}

	public void CargaDetalleOrden()
	{
		DataTable data = new DataTable();
		data = AdmOrden.CargaDetalleOrden(Convert.ToInt32(Ord.CodOrdenCompra));
		foreach (DataRow r in data.Rows)
		{
			dgvDetalle.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), Convert.ToDecimal(r[8].ToString()), Convert.ToDecimal(r[9].ToString()), Convert.ToDecimal(r[10].ToString()), Convert.ToDecimal(r[11].ToString()), Convert.ToDecimal(r[12].ToString()), Convert.ToDecimal(r[13].ToString()), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), r[18].ToString(), r[19].ToString(), r[20].ToString(), Convert.ToDateTime(r[21].ToString()), r[22].ToString(), r[23].ToString(), r[24].ToString(), r[25].ToString(), r[26].ToString(), r[27].ToString(), Convert.ToBoolean(r[28].ToString()), Convert.ToBoolean(r[29].ToString()));
		}
	}

	public void traeserie()
	{
		ser = Admser.MuestraSerie(guia.CodSerie, frmLogin.iCodAlmacen);
	}

	private void CargaVehiculosTransporte()
	{
		cmbVehiculos.DataSource = AdmVeh.CargaVehiculoTransportes();
		cmbVehiculos.DisplayMember = "placa";
		cmbVehiculos.ValueMember = "codVehiculoTransporte";
	}

	private void CargaConductores()
	{
		cmbConductor.DataSource = AdmCond.CargaConductores();
		cmbConductor.DisplayMember = "nombre";
		cmbConductor.ValueMember = "codConductor";
	}

	public void CargaGuiaRemision()
	{
		try
		{
			guia = AdmGuia.CargaGuiaRemision(Convert.ToInt32(CodGuia));
			ser = Admser.MuestraSerie(guia.CodSerie, frmLogin.iCodAlmacen);
			if (guia != null)
			{
				txtNumDoc.Text = guia.CodGuiaRemision;
				txtnumeroOc.Text = guia.numeroOc;
				if (txtCodCliente.Enabled)
				{
					if (guia.CodCliente != 0)
					{
						CodCliente = guia.CodCliente;
						if (guia.RUCCliente != "")
						{
							txtCodCliente.Text = guia.RUCCliente;
						}
						else
						{
							txtCodCliente.Text = guia.DNI;
						}
						txtNombreCliente.Text = guia.RazonSocialCliente;
						txtDireccion.Text = guia.Direccion;
					}
					else
					{
						CodCliente = guia.CodAlmacenDestino;
						txtCodCliente.Text = guia.RUCCliente;
						txtNombreCliente.Text = guia.NomAlmacenDestino;
						txtDireccion.Text = guia.UbicacionAlmacenDest;
					}
				}
				dtpFecha.Value = guia.FechaEmision;
				dtpFechaTransporte.Value = guia.FechaTraslado;
				cmbMotivo.SelectedIndex = guia.CodMotivo;
				if (guia.CodPedido != 0)
				{
					pedido = Admped.CargaPedido(Convert.ToInt32(guia.CodPedido));
					txtPedido.Text = pedido.CodPedido;
				}
				if (guia.CodVehiculoTransporte != 0)
				{
					cmbVehiculos.SelectedValue = guia.CodVehiculoTransporte;
					txtMarcaVehiculo.Text = guia.Marca;
					txtConstancia.Text = guia.ConstanciaInscripcion;
				}
				if (guia.CodConductor != 0)
				{
					cmbConductor.SelectedValue = guia.CodConductor;
					txtLicencia.Text = guia.Licencia;
				}
				if (guia.CodEmpresaTransporte != 0)
				{
					txtRUCTransporte.Text = guia.RUCEmpresaTransporte;
					txtRazonSocialTransporte.Text = guia.RazonSocialTransporte;
					txtDireccionTransporte.Text = guia.DireccionTransporte;
				}
				txtSerie.Text = guia.Serie;
				txtNumero.Text = guia.NumDoc;
				if (txtPedido.Enabled)
				{
				}
				txtComentario.Text = guia.Comentario;
				datosAlmacena = AdmGuia.CargaFacturasGuia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen);
				if (datosAlmacena != null && datosAlmacena.Rows.Count > 0)
				{
					while (i < datosAlmacena.Rows.Count)
					{
						int codigo = Convert.ToInt32(datosAlmacena.Rows[i]["codFactura"]);
						int almacen = Convert.ToInt32(datosAlmacena.Rows[i]["codAlmacen"]);
						if (codigo != 0)
						{
							facturav = AdmVenta.BuscaFacturaVenta(codigo, almacen);
							if (!(dat == ""))
							{
								dat = facturav.SiglaDocumento + " " + facturav.Serie + "-" + facturav.NumDoc;
							}
							dat2 = txtFactura.Text;
							if (txtFactura.Text == "")
							{
								txtFactura.Text = dat;
							}
							else if (txtFactura.Text.Equals(dat))
							{
								txtFactura.Text = dat;
							}
							else
							{
								txtFactura.Text = dat2 + "," + dat;
							}
						}
						else
						{
							int codigoT = Convert.ToInt32(datosAlmacena.Rows[i]["codTransferencia"]);
							clstran = AdmTransf.BuscaTransferencia(Convert.ToString(codigoT), almacen);
							if (!(dat == ""))
							{
								dat = clstran.SiglaDocumento + " " + clstran.Serie + "-" + clstran.CodTransDir;
							}
							dat2 = txtTranferencia.Text;
							if (txtFactura.Text == "")
							{
								txtTranferencia.Text = dat;
							}
							else if (txtFactura.Text.Equals(dat))
							{
								txtTranferencia.Text = dat;
							}
							else
							{
								txtFactura.Text = dat2 + "," + dat;
							}
						}
						i++;
					}
				}
				else
				{
					dat = guia.SiglaDocumento + " " + guia.Serie + "-" + guia.NumDoc;
					dat2 = txtFactura.Text;
					if (txtFactura.Text == "")
					{
						txtFactura.Text = dat;
					}
				}
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void sololectura(bool estado)
	{
		txtNumero.Enabled = estado;
		txtSerie.Enabled = estado;
		dtpFecha.Enabled = !estado;
		dtpFechaTransporte.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		cmbMotivo.Enabled = !estado;
		txtPedido.ReadOnly = estado;
		txtFactura.ReadOnly = estado;
		txtComentario.ReadOnly = estado;
		ext.sololectura(groupBox4.Controls);
		ext.sololectura(groupBox5.Controls);
		btnGuardar.Visible = !estado;
		btnImprimir.Visible = estado;
		btnNuevaGuia.Visible = estado;
	}

	public void CargaDetalle()
	{
		dgvDetalle.Rows.Clear();
		DataTable data = new DataTable();
		data = AdmGuia.CargaDetalle(Convert.ToInt32(guia.CodGuiaRemision));
		foreach (DataRow r in data.Rows)
		{
			dgvDetalle.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), Convert.ToDecimal(r[8].ToString()), Convert.ToDecimal(r[9].ToString()), Convert.ToDecimal(r[10].ToString()), Convert.ToDecimal(r[11].ToString()), Convert.ToDecimal(r[12].ToString()), Convert.ToDecimal(r[13].ToString()), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), r[18].ToString(), r[19].ToString(), Convert.ToInt32(r[20].ToString()), Convert.ToDateTime(r[21].ToString()), Convert.ToDecimal(r[22].ToString()), r[23].ToString(), r[24].ToString(), r[25].ToString(), r[26].ToString(), r[27].ToString(), r[28].ToString(), r[29].ToString(), Convert.ToInt32(r[30].ToString()));
		}
		cargaetiquetasolola3();
	}

	public List<clsDetalleGuiaRemision> listadetalleguia()
	{
		return AdmGuia.listaDetalleGuiaRemision(Convert.ToString(guia.CodGuiaRemision));
	}

	private void frmNotaSalida_Shown(object sender, EventArgs e)
	{
	}

	private void txtDocRef_Leave(object sender, EventArgs e)
	{
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
		form.DocSeleccionado = 11;
		form.ShowDialog();
		ser = form.ser;
		CodSerie = ser.CodSerie;
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
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Visible = false;
				txtNumero.Text = ser.Numeracion.ToString();
			}
			ProcessTabKey(forward: true);
		}
		else
		{
			MessageBox.Show("Serie no existe, Presione F1 para consultar la tabla de ayuda", "Facturación Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void txtSerie_Leave(object sender, EventArgs e)
	{
		if (BuscaSerie())
		{
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumero.Text = "";
				txtNumero.Visible = true;
				txtNumero.Focus();
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Visible = false;
				txtNumero.Text = ser.Numeracion.ToString();
			}
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
		if (txtNumero.Text == "")
		{
			txtNumero.Focus();
		}
		else
		{
			VerificarCabecera();
		}
	}

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDetalleGuia"] != null)
		{
			Application.OpenForms["frmDetalleGuia"].Activate();
			return;
		}
		frmDetalleGuia form = new frmDetalleGuia();
		form.Procede = 5;
		form.Proceso = 1;
		form.ShowDialog();
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (e.RowCount > 0)
		{
			btnGuardar.Enabled = true;
		}
		try
		{
			cargaetiquetasolola3();
			if (e.RowIndex == -1 || Convert.ToInt32(dgvDetalle.Rows[e.RowIndex].Cells[etiqueta.Name].Value) != 3)
			{
				return;
			}
			dgvDetalle.Rows[e.RowIndex].Cells[etiqueta.Name].ReadOnly = true;
			foreach (DataGridViewRow dgr in (IEnumerable)dgvDetalle.Rows)
			{
				if (dgr.Cells[codproducto.Name].Value.ToString() == dgvDetalle.Rows[e.RowIndex].Cells[codproducto.Name].Value.ToString())
				{
					decimal cantpend = Convert.ToDecimal(dgr.Cells[cantidadpendiente.Name].Value);
					decimal cant_nueva = Convert.ToDecimal(dgvDetalle.Rows[e.RowIndex].Cells[cantidad.Name].Value);
					dgr.Cells[cantidadpendiente.Name].Value = cantpend - cant_nueva;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void btnSalir_Click(object sender, EventArgs e)
	{
		if (salir == 1)
		{
			Close();
		}
	}

	private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
	{
	}

	public List<clsDetalleOrdenCompra> detalleorden2()
	{
		return AdmOrden.cargadetalleorden(CodOrdenCompra, frmLogin.iCodAlmacen);
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (Proceso == 0)
			{
				return;
			}
			guia.CodAlmacen = frmLogin.iCodAlmacen;
			guia.CodTipoDocumento = 11;
			guia.CodSerie = Convert.ToInt32(txtSerie.Text);
			if (txtNumDoc.Text == "")
			{
				guia.NumDoc = txtNumero.Text;
			}
			else
			{
				guia.NumDoc = txtNumero.Text;
			}
			if (txtnumeroOc.Text != null && !(txtnumeroOc.Text == ""))
			{
				guia.numeroOc = Ord.CodOrdenCompra.ToString();
			}
			guia.CodMotivo = cmbMotivo.SelectedIndex;
			if (CodPedido != 0)
			{
				guia.CodPedido = CodPedido;
			}
			guia.FechaEmision = dtpFecha.Value;
			guia.fecharegistro1 = dtfecharegistro.Value;
			guia.fechaingresoalmacen = dtfechaingresoalmacen.Value;
			guia.FechaTraslado = dtpFechaTransporte.Value;
			guia.CodCliente = cli.CodCliente;
			_ = guia.CodProveedor;
			if (false)
			{
				guia.CodProveedor = 0;
			}
			if (CodEmpresaTransporte != 0)
			{
				guia.CodEmpresaTransporte = empT.CodEmpresaTranporte;
			}
			if (CodVehiculo != 0)
			{
				guia.CodVehiculoTransporte = Convert.ToInt32(cmbVehiculos.SelectedValue);
			}
			if (CodConductor != 0)
			{
				guia.CodConductor = Convert.ToInt32(cmbConductor.SelectedValue);
			}
			if (txtFactura.Text != "")
			{
				guia.Facturado = 1;
			}
			guia.CodFactura = CodVenta;
			guia.Comentario = txtComentario.Text;
			guia.CodUser = frmLogin.iCodUser;
			guia.Estado = 1;
			if (txtTranferencia.Text == "" || txtTranferencia.Text == " ")
			{
				txtTranferencia.Text = "0";
			}
			guia.CodTransferencia = Convert.ToInt32(txtTranferencia.Text);
			if (Proceso == 1)
			{
				if (dgvDetalle.Rows.Count < 0)
				{
					MessageBox.Show("Debe Ingresar algun tipo de docuemento para realizar la guia", "Guia de Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				if (AdmGuia.insert(guia))
				{
					RecorreDetalle();
				}
				if (detalle.Count <= 0)
				{
					return;
				}
				foreach (clsDetalleGuiaRemision det in detalle)
				{
					AdmGuia.insertdetalle(det);
					AdmNotas.ActualizaCantidadPendienteVenta(det.Cantidad, det.CodProducto, det.CodVenta);
					AdmGuia.insertrelacionguia(Convert.ToInt32(guia.CodGuiaRemision), det.CodVenta, frmLogin.iCodAlmacen, frmLogin.iCodUser, guia.CodTransferencia);
				}
				MessageBox.Show("Los datos se guardaron correctamente", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CodGuia = guia.CodGuiaRemision;
				btnImprimir.Visible = true;
				sololectura(estado: true);
				CargaDetalle();
			}
			else
			{
				if (Proceso != 2 || !AdmGuia.update(guia))
				{
					return;
				}
				RecorreDetalle();
				foreach (clsDetalleGuiaRemision det2 in guia.Detalle)
				{
					foreach (clsDetalleGuiaRemision det3 in detalle)
					{
						if (det2.Equals(det3))
						{
							AdmGuia.updatedetalle(det3);
							return;
						}
					}
					AdmGuia.deletedetalle(det2.CodGuiaRemision);
				}
				foreach (clsDetalleGuiaRemision deta in detalle)
				{
					if (deta.CodGuiaRemision == 0)
					{
						AdmGuia.insertdetalle(deta);
					}
				}
				MessageBox.Show("Los datos se actualizaron correctamente", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public bool validaciones(int codproducto, double cantidad)
	{
		foreach (clsDetalleOrdenCompra deta in detalleorden())
		{
			if (codproducto == deta.CodProducto)
			{
				if (cantidad > Convert.ToDouble(deta.cantidadpendiente))
				{
					MessageBox.Show("La Cantidad Tiene Que Ser Menor A la Cantidad Restante De La Orden", "Guia de Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Proceso = 1;
					return false;
				}
				continue;
			}
			int contador = 0;
			foreach (clsDetalleOrdenCompra deta2 in detalleorden())
			{
				if (deta2.CodProducto == codproducto)
				{
					contador++;
				}
			}
			if (contador == 0)
			{
				DialogResult dr = DialogResult.None;
				dr = MessageBox.Show("Este Producto Es Diferente a cualquier Producto De La Orden, Desea Guardarlo En La Guia?", "Guia de Remision", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
				if (dr == DialogResult.No)
				{
					return false;
				}
				DialogResult dre = DialogResult.None;
				frmAutorizacion frm = new frmAutorizacion();
				frm.StartPosition = FormStartPosition.CenterScreen;
				dre = frm.ShowDialog();
				if (dre == DialogResult.OK)
				{
					ProdSolicitado = false;
					return true;
				}
				return false;
			}
			ProdSolicitado = true;
			return true;
		}
		ProdSolicitado = true;
		EstadoDeLaOrden.DisplayIndex = 1;
		return true;
	}

	public void validaCantidadesOrden()
	{
		List<clsDetalleOrdenCompra> detalleOrde = detalleorden();
		foreach (clsDetalleOrdenCompra o in detalleOrde)
		{
			foreach (clsDetalleGuiaRemision g in detalle)
			{
				if (o.CodProducto != g.CodProducto)
				{
					continue;
				}
				if (Convert.ToDouble(g.Cantidad) == Convert.ToDouble(o.Cantidad) || Convert.ToDouble(o.cantidadpendiente) == Convert.ToDouble(g.Cantidad))
				{
					actualizaestadorden(o.CodDetalleOrdenCompra, 3, g.CodDetalleGuiaRemision);
					actualizaestadocabeceraorden(o.CodOrdenCompra, 3);
				}
				foreach (clsDetalleGuiaRemision gu in detalle)
				{
				}
				if (Convert.ToDouble(g.Cantidad) < Convert.ToDouble(o.cantidadpendiente))
				{
					actualizaestadorden(o.CodDetalleOrdenCompra, 2, g.CodDetalleGuiaRemision);
					actualizaestadocabeceraorden(o.CodOrdenCompra, 2);
				}
			}
		}
		int contadorTotal = 0;
	}

	public void guardaNI()
	{
		notaI.codsucu = frmLogin.iCodSucursal;
		notaI.CodAlmacen = frmLogin.iCodAlmacen;
		notaI.CodTipoTransaccion = tran.CodTransaccion;
		notaI.CodTipoDocumento = doc.CodTipoDocumento;
		notaI.CodSerie = Convert.ToInt32(txtSerie.Text);
		notaI.NumDoc = txtnumeroOc.Text.ToString();
		notaI.CodProveedor = Convert.ToInt32(txtCodCliente.Text);
		notaI.RazonSocialProveedor = txtNombreCliente.Text;
		notaI.Moneda = 1;
		notaI.Comentario = txtComentario.Text;
		notaI.FechaIngreso = dtpFecha.Value.Date;
		notaI.fechaingresoalmacen = dtfechaingresoalmacen.Value.Date;
		notaI.Comentario = txtComentario.Text;
		nota.MontoBruto = 0.0;
		if (nota.MontoDscto == 0.0)
		{
			nota.MontoDscto = 0.0;
		}
		else
		{
			nota.MontoDscto = 0.0;
		}
		nota.Igv = 0.0;
		nota.Total = 0.0;
		nota.Estado = 1;
		notaI.CodUser = frmLogin.iCodUser;
		notaI.CodOrdenCompra = Convert.ToInt32(txtnumeroOc.Text);
		notaI.codalmacenemisor = 0;
		nota.Codtransferencia = 0;
		if (!AdmNota.insertingresoguia(nota))
		{
			return;
		}
		RecorreDetalle();
		if (detalle.Count > 0)
		{
			foreach (clsDetalleNotaIngreso deta in detallei)
			{
				AdmNota.insertdetalle(deta);
				AdmNota.ActualizaCantidadPendiente(deta.Cantidad, deta.CodProducto, notaI.CodOrdenCompra, deta.CoddetalleOrden);
				if (notaI.CodAlmacen != frmLogin.iCodAlmacen)
				{
					AdmNota.ActualizaCantidadPendiente2(deta.Cantidad, deta.CodProducto, notaI.CodAlmacen, frmLogin.iCodUser);
				}
			}
		}
		MessageBox.Show("Los datos se guardaron correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Close();
	}

	public void actualizaestadorden(int CodDetalleOrdenCompra, int estado, int Codguia)
	{
		bool hola = AdmOrden.actualizaestadorden(CodDetalleOrdenCompra, estado, Codguia);
	}

	public void actualizacantidadpendiente(int CodDetalleOrdenCompra, int estado, int CodGuia)
	{
		bool hola = AdmOrden.actualizacantidadpendiente(CodDetalleOrdenCompra, estado, CodGuia);
	}

	public void actualizaestadocabeceraorden(int codOrdenCompra, int estado)
	{
		bool hola = AdmOrden.actualizaestadocabeceraorden(codOrdenCompra, estado);
	}

	public List<clsDetalleOrdenCompra> detalleorden()
	{
		return AdmOrden.cargadetalleorden(CodOrdenCompra, frmLogin.iCodAlmacen);
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
		clsDetalleGuiaRemision deta = new clsDetalleGuiaRemision();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodGuiaRemision = Convert.ToInt32(guia.CodGuiaRemision);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		if (deta.codDtalleOrden == 0)
		{
			deta.codDtalleOrden = 0;
		}
		else
		{
			deta.codDtalleOrden = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		}
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		if (deta.SerieLote != null)
		{
			deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		}
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
		deta.precio = Convert.ToDecimal((fila.Cells[preciounit.Name].Value == "") ? ((object)0) : fila.Cells[preciounit.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		if (deta.CodVenta != 0)
		{
			deta.CodVenta = Convert.ToInt32(fila.Cells[codigoventa.Name].Value);
		}
		deta.productosolicitado = Convert.ToBoolean(fila.Cells[productosolicitado.Name].Value);
		deta.estadoOrden = Convert.ToBoolean(fila.Cells[EstadoDeLaOrden.Name].Value);
		deta.etiqueta = Convert.ToInt32(fila.Cells[etiqueta.Name].Value);
		if (deta.etiqueta == 1)
		{
			dgvDetalle.Rows[fila.Index].Cells[etiqueta.Name].Value = 1;
		}
		else if (deta.etiqueta == 3)
		{
			dgvDetalle.Rows[fila.Index].Cells[etiqueta.Name].Value = 3;
		}
		else if (deta.etiqueta == 4)
		{
			dgvDetalle.Rows[fila.Index].Cells[etiqueta.Name].Value = 4;
		}
		else if (deta.etiqueta == 5)
		{
			dgvDetalle.Rows[fila.Index].Cells[etiqueta.Name].Value = 5;
		}
		else if (deta.etiqueta == 6)
		{
			dgvDetalle.Rows[fila.Index].Cells[etiqueta.Name].Value = 6;
		}
		else if (deta.etiqueta == 7)
		{
			dgvDetalle.Rows[fila.Index].Cells[etiqueta.Name].Value = 7;
		}
		detalle.Add(deta);
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmDetalleGuia"] != null)
			{
				Application.OpenForms["frmDetalleGuia"].Activate();
				return;
			}
			frmDetalleGuia form = new frmDetalleGuia();
			form.Procede = 5;
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

	private void cmbVehiculos_SelectionChangeCommitted(object sender, EventArgs e)
	{
		veh = AdmVeh.MuestraVehiculoTransporte(Convert.ToInt32(cmbVehiculos.SelectedValue));
		if (veh != null)
		{
			CodVehiculo = veh.CodVehiculoTransporte;
			txtMarcaVehiculo.Text = veh.Marca;
			txtConstancia.Text = veh.ConstanciaInscripcion;
		}
		else
		{
			CodVehiculo = 0;
			txtMarcaVehiculo.Text = "";
			txtConstancia.Text = "";
		}
	}

	private void cmbConductor_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cond = AdmCond.MuestraConductor(Convert.ToInt32(cmbConductor.SelectedValue));
		if (cond != null)
		{
			CodConductor = cond.CodConductor;
			txtLicencia.Text = cond.Licencia;
		}
		else
		{
			CodConductor = 0;
			txtLicencia.Text = "";
		}
	}

	private void txtRUCTransporte_KeyDown(object sender, KeyEventArgs e)
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
			txtRUCTransporte.Text = empT.Ruc;
			txtRazonSocialTransporte.Text = empT.RazonSocial;
			txtDireccionTransporte.Text = empT.Direccion;
		}
		else
		{
			txtRUCTransporte.Text = "";
			txtRazonSocialTransporte.Text = "";
			txtDireccionTransporte.Text = "";
		}
	}

	private void txtRUCTransporte_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && txtRUCTransporte.Text != "")
		{
			if (BuscaEmpresaTransporte())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("La empresa no existe, Presione F1 para consultar la tabla de ayuda", "Guia Remisión", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaEmpresaTransporte()
	{
		empT = AdmET.BuscaEmpresaTransporte(txtRUCTransporte.Text);
		if (empT != null)
		{
			txtRazonSocialTransporte.Text = empT.RazonSocial;
			txtDireccionTransporte.Text = empT.Direccion;
			CodEmpresaTransporte = empT.CodEmpresaTranporte;
			return true;
		}
		txtRazonSocialTransporte.Text = "";
		txtDireccionTransporte.Text = "";
		CodEmpresaTransporte = 0;
		return false;
	}

	private void cmbVehiculos_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void cmbConductor_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void txtRUCTransporte_Leave(object sender, EventArgs e)
	{
		VerificarCabecera();
		if (Validacion && Proceso == 1)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtPedido.Text != "")
		{
			if (BuscaPedido())
			{
				CargaPedido();
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Pedido no existe, Presione F1 para consultar la tabla de ayuda", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaPedido()
	{
		pedido = Admped.BuscaPedido(txtPedido.Text, frmLogin.iCodAlmacen);
		if (pedido != null)
		{
			CodPedido = Convert.ToInt32(pedido.CodPedido);
			return true;
		}
		CodPedido = 0;
		return false;
	}

	private void CargaPedido()
	{
		try
		{
			pedido = Admped.CargaPedido(Convert.ToInt32(CodPedido));
			if (pedido != null)
			{
				txtPedido.Text = pedido.Numeracion.ToString();
				if (txtCodCliente.Enabled)
				{
					CodCliente = pedido.CodCliente;
					cli = AdmCli.MuestraCliente(CodCliente);
					txtCodCliente.Text = pedido.CodigoPersonalizado;
					txtNombreCliente.Text = pedido.Nombre;
					txtDireccion.Text = pedido.Direccion;
				}
				txtComentario.Text = pedido.Comentario;
				CargaDetallePedido();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetallePedido()
	{
		dgvDetalle.DataSource = Admped.CargaDetalleGuia(Convert.ToInt32(pedido.CodPedido));
	}

	private void CargaDetalleNota()
	{
		datosCarga = AdmVenta.MuestraDetalleVentaGuia(Convert.ToInt32(venta.CodFacturaVenta), venta.CodAlmacen);
		if (datosCarga != null)
		{
			datosCarga2.Merge(datosCarga);
		}
		dgvDetalle.DataSource = datosCarga2;
	}

	private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private bool BuscaNotaSalida()
	{
		pedido = Admped.BuscaPedido(txtPedido.Text, frmLogin.iCodAlmacen);
		if (pedido != null)
		{
			CodPedido = Convert.ToInt32(pedido.CodPedido);
			return true;
		}
		CodPedido = 0;
		return false;
	}

	private void CargaNotaSalida()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodVenta));
			ser = Admser.MuestraSerie(venta.CodSerie, venta.CodAlmacen);
			if (venta != null)
			{
				rpta = venta.SiglaDocumento + " " + ser.Serie + "-" + venta.NumDoc;
				rpta2 = txtFactura.Text;
				if (txtFactura.Text == "")
				{
					txtFactura.Text = rpta;
				}
				else
				{
					txtFactura.Text = rpta2 + "," + rpta;
				}
				if (txtCodCliente.Enabled)
				{
					CodCliente = venta.CodCliente;
					cli = AdmCli.MuestraCliente(CodCliente);
					txtCodCliente.Text = cli.RucDni;
					txtNombreCliente.Text = cli.Nombre;
					if (cli.DireccionEntrega != null && cli.DireccionEntrega.Trim() != "")
					{
						txtDireccion.Text = cli.DireccionEntrega;
					}
					else
					{
						txtDireccion.Text = cli.DireccionLegal;
					}
				}
				txtComentario.Text = venta.Comentario;
				CargaDetalleNota();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	public void fnImprimir()
	{
		ser = Admser.MuestraSerie(guia.CodSerie, frmLogin.iCodAlmacen);
		if (txtCodCliente.Text == "")
		{
			ReportDocument rd = new ReportDocument();
			rd.Load("CRGuiaRemision.rpt");
			CRGuiaRemision rpt = new CRGuiaRemision();
			rd.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen, CodTransaccion));
			PrintOptions rptoption = rd.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rd.PrintToPrinter(1, collated: false, 1, 1);
			rd.Close();
			rd.Dispose();
		}
		else if (frmLogin.iCodAlmacen == 20 || frmLogin.iCodAlmacen == 21)
		{
			ReportDocument rd2 = new ReportDocument();
			rd2.Load("CRGuiaRemionVentaNewFormat.rpt");
			CRGuiaRemionVentaNewFormat rpt2 = new CRGuiaRemionVentaNewFormat();
			rd2.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen, CodTransaccion));
			PrintOptions rptoption2 = rd2.PrintOptions;
			rptoption2.PrinterName = ser.NombreImpresora;
			rptoption2.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rd2.PrintToPrinter(1, collated: false, 1, 1);
			rd2.Close();
			rd2.Dispose();
		}
		else
		{
			ReportDocument rd3 = new ReportDocument();
			rd3.Load("CRGuiaRemisionVenta.rpt");
			CRGuiaRemisionVenta rpt3 = new CRGuiaRemisionVenta();
			rd3.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen, CodTransaccion));
			PrintOptions rptoption3 = rd3.PrintOptions;
			rptoption3.PrinterName = ser.NombreImpresora;
			rptoption3.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rd3.PrintToPrinter(1, collated: false, 1, 1);
			rd3.Close();
			rd3.Dispose();
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			ser = Admser.MuestraSerie(guia.CodSerie, frmLogin.iCodAlmacen);
			if (CodTransaccion == 5)
			{
				if (frmLogin.iCodAlmacen == 20 || frmLogin.iCodAlmacen == 21)
				{
					CodTransaccion = 15;
					ReportDocument rd = new ReportDocument();
					rd.Load("CRGuiaRemisionNewFormat.rpt");
					CRGuiaRemisionNewFormat rpt = new CRGuiaRemisionNewFormat();
					rd.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen, CodTransaccion));
					PrintOptions rptoption = rd.PrintOptions;
					rptoption.PrinterName = ser.NombreImpresora;
					rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
					rd.PrintToPrinter(1, collated: false, 1, 1);
					rd.Close();
					rd.Dispose();
				}
				else
				{
					CodTransaccion = 15;
					ReportDocument rd2 = new ReportDocument();
					rd2.Load("CRGuiaRemision.rpt");
					CRGuiaRemision rpt2 = new CRGuiaRemision();
					rd2.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen, CodTransaccion));
					PrintOptions rptoption2 = rd2.PrintOptions;
					rptoption2.PrinterName = ser.NombreImpresora;
					rptoption2.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
					rd2.PrintToPrinter(1, collated: false, 1, 1);
					rd2.Close();
					rd2.Dispose();
				}
			}
			else if (frmLogin.iCodAlmacen == 20 || frmLogin.iCodAlmacen == 21)
			{
				ReportDocument rd3 = new ReportDocument();
				rd3.Load("CRGuiaRemionVentaNewFormat.rpt");
				CRGuiaRemionVentaNewFormat rpt3 = new CRGuiaRemionVentaNewFormat();
				if (Proceso == 3 && CodTransaccion == 4)
				{
					CodTransaccion = 12;
				}
				rd3.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen, CodTransaccion));
				PrintOptions rptoption3 = rd3.PrintOptions;
				rptoption3.PrinterName = ser.NombreImpresora;
				rptoption3.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rd3.PrintToPrinter(1, collated: false, 1, 1);
				rd3.Close();
				rd3.Dispose();
			}
			else
			{
				ReportDocument rd4 = new ReportDocument();
				rd4.Load("CRGuiaRemisionVenta.rpt");
				CRGuiaRemisionVenta rpt4 = new CRGuiaRemisionVenta();
				if (Proceso == 3 && CodTransaccion == 4)
				{
					CodTransaccion = 12;
				}
				rd4.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen, CodTransaccion));
				PrintOptions rptoption4 = rd4.PrintOptions;
				rptoption4.PrinterName = ser.NombreImpresora;
				rd4.PrintToPrinter(1, collated: false, 1, 1);
				rd4.Close();
				rd4.Dispose();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void txtFactura_Leave(object sender, EventArgs e)
	{
		if (txtFactura.Text != "")
		{
			btnDetalle.Visible = false;
		}
		else
		{
			btnDetalle.Visible = true;
		}
	}

	private void txtFactura_KeyDown(object sender, KeyEventArgs e)
	{
		if (txtCodCliente.Text != "")
		{
			if (e.KeyCode != Keys.F1)
			{
				return;
			}
			txtFactura.Text = "";
			if (Application.OpenForms["frmListaDocumentosSinGuia"] == null)
			{
				frmListaDocumentosSinGuia form = new frmListaDocumentosSinGuia();
				form.CodCliente = CodCliente;
				if (txtNumero.Visible)
				{
					form.Tipo = 2;
				}
				else
				{
					form.Tipo = 1;
				}
				form.ShowDialog();
				lta = form.ltaCod;
				datosCarga2.Clear();
				{
					foreach (int c in lta)
					{
						if (c != 0)
						{
							CodVenta = c;
						}
						if (CodVenta != 0)
						{
							CargaNotaSalida();
							ProcessTabKey(forward: true);
							btnEliminar.Visible = true;
						}
					}
					return;
				}
			}
			Application.OpenForms["frmListaDocumentosSinGuia"].Activate();
		}
		else
		{
			MessageBox.Show("Debe elegir un Cliente", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnNuevaGuia_Click(object sender, EventArgs e)
	{
		frmGuiaRemision form2 = new frmGuiaRemision();
		form2.MdiParent = base.MdiParent;
		form2.Proceso = 1;
		form2.Show();
		Close();
	}

	private void txtSerie_TextChanged(object sender, EventArgs e)
	{
		txtNumero.Text = "";
		txtNumero.Visible = false;
	}

	public void actualizacantidad(int CodDetalleOrdenCompra, int estado)
	{
		bool hola = AdmOrden.actualizacantidad(CodDetalleOrdenCompra, estado);
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (txtedit != null)
		{
			if ((Convert.ToDecimal(txtedit.Text) > Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidadnueva.Name].Value) || Convert.ToDecimal(txtedit.Text) == 0m) && dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "cantidad")
			{
				MessageBox.Show("CANTIDAD DEBE SER MENOR O IGUAL QUE : " + dgvDetalle.CurrentRow.Cells[cantidadpendiente.Name].Value, "ADVERTNCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = dgvDetalle.CurrentRow.Cells[cantidadpendiente.Name].Value;
			}
			else
			{
				dgvDetalle.CurrentRow.Cells[cantidadnueva.Name].Value = dgvDetalle.CurrentRow.Cells[cantidadpendiente.Name].Value;
			}
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
		if (e.Control is DataGridViewComboBoxEditingControl dgvCombo)
		{
			dgvCombo.SelectedIndexChanged -= dvgCombo_SelectedIndexChanged;
			dgvCombo.SelectedIndexChanged += dvgCombo_SelectedIndexChanged;
		}
		if (!(e.Control is ComboBox))
		{
			DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
			dText.KeyPress -= dText_KeyPress;
			dText.KeyPress += dText_KeyPress;
		}
	}

	public void dText_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
		if (e.KeyChar == '-')
		{
			e.Handled = true;
		}
		if (e.KeyChar == ' ')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.Length == 0)
		{
			e.Handled = true;
		}
	}

	public void dvgCombo_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox combo = sender as ComboBox;
		try
		{
			if (combo.SelectedValue != null && combo.SelectedIndex != -1 && combo.SelectedValue.ToString() != "System.Data.DataRowView" && dgvDetalle.CurrentCell != null && etiqueta.GetType().ToString().Contains(combo.SelectedValue.GetType().ToString()))
			{
				dgvDetalle.Rows[dgvDetalle.CurrentCell.RowIndex].Cells[etiqueta.Index].Value = combo.SelectedValue.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == cantidad.Index)
		{
			ok.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dgvDetalle_Leave(object sender, EventArgs e)
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
			{
				if (txtRUCTransporte.Text == "")
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
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
			{
				if (txtRUCTransporte.Text == "")
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
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void txtTranferencia_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtTranferencia.Text != "")
		{
			if (BuscaTranferencia())
			{
				CargaTransferencia();
			}
			else
			{
				MessageBox.Show("Transferencia no existe, Presione F1 para consultar la tabla de ayuda", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		frmGuiaRemision form1 = new frmGuiaRemision();
		form1.MdiParent = base.MdiParent;
		form1.Proceso = 1;
		form1.CodOrdenCompra = codmovi;
		form1.Show();
	}

	public void cargaetiquetasolola3()
	{
		etiqueta.DataSource = AdmPro.cargaetiquetas(frmLogin.iCodAlmacen);
		etiqueta.DisplayMember = "descripcion";
		etiqueta.ValueMember = "Id_etiqueta";
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvDetalle.Rows[e.RowIndex].Cells[etiqueta.Name].ColumnIndex != e.ColumnIndex || dgvDetalle.CurrentCell != null)
			{
			}
			etiqueta.DataSource = AdmPro.cargaetiquetas(frmLogin.iCodAlmacen);
			etiqueta.DisplayMember = "descripcion";
			etiqueta.ValueMember = "Id_etiqueta";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		if (e.Exception.Message == "DataGridViewComboBoxCell Value is not valid")
		{
			object value = dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			if (!((DataGridViewComboBoxColumn)dgvDetalle.Columns[e.ColumnIndex]).Items.Contains(value))
			{
				((DataGridViewComboBoxColumn)dgvDetalle.Columns[e.ColumnIndex]).Items.Add(value);
				e.ThrowException = false;
			}
		}
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
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
	}

	private void dgvDetalle_Enter(object sender, EventArgs e)
	{
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		frmOrdenesVigentes frmov = new frmOrdenesVigentes();
		if (dgvDetalle.Rows.Count >= 1 && dgvDetalle.CurrentRow != null)
		{
			DataGridViewRow row = dgvDetalle.CurrentRow;
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodOrdenCompra = Convert.ToInt32(txtNumDoc.Text);
			form.Proceso = 7;
			form.txtDocRef.Text = "FT";
			form.Show();
		}
	}

	private void CargaTransferencia()
	{
		try
		{
			clstran = AdmTransf.CargaTransferencia(Convert.ToInt32(txtTranferencia.Text));
			if (clstran != null)
			{
				txtTranferencia.Text = clstran.CodTransDir;
				txtComentario.Text = clstran.Comentario;
				CargaDetalleTranferencia();
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalleTranferencia()
	{
		dgvDetalle.DataSource = AdmTransf.CargaDetalleGuiaT(clstran.CodTransDir);
	}

	private bool BuscaTranferencia()
	{
		clstran = AdmTransf.CargaTransferencia(Convert.ToInt32(txtTranferencia.Text));
		if (clstran != null)
		{
			CodTranferencia = Convert.ToInt32(clstran.CodTransDir);
			return true;
		}
		CodTranferencia = 0;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGuiaRemision));
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
		this.dtfecharegistro = new System.Windows.Forms.DateTimePicker();
		this.label22 = new System.Windows.Forms.Label();
		this.dtfechaingresoalmacen = new System.Windows.Forms.DateTimePicker();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.txtnumeroOc = new System.Windows.Forms.TextBox();
		this.txtTranferencia = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtFactura = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbMotivo = new System.Windows.Forms.ComboBox();
		this.label18 = new System.Windows.Forms.Label();
		this.dtpFechaTransporte = new System.Windows.Forms.DateTimePicker();
		this.label17 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtDireccionTransporte = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtRazonSocialTransporte = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtRUCTransporte = new System.Windows.Forms.TextBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.cmbConductor = new System.Windows.Forms.ComboBox();
		this.cmbVehiculos = new System.Windows.Forms.ComboBox();
		this.txtMarcaVehiculo = new System.Windows.Forms.TextBox();
		this.txtLicencia = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtConstancia = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtPedido = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidadnueva = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidadpendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockPend = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.maxPorcDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigoventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numorden = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.productosolicitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.EstadoDeLaOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.etiqueta = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnNuevaGuia = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dtfecharegistro);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.dtfechaingresoalmacen);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.txtnumeroOc);
		this.groupBox1.Controls.Add(this.txtTranferencia);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtFactura);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cmbMotivo);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.dtpFechaTransporte);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.groupBox5);
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtPedido);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtNumDoc);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1202, 380);
		this.groupBox1.TabIndex = 21;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.dtfecharegistro.Enabled = false;
		this.dtfecharegistro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfecharegistro.Location = new System.Drawing.Point(114, 81);
		this.dtfecharegistro.Name = "dtfecharegistro";
		this.dtfecharegistro.Size = new System.Drawing.Size(115, 20);
		this.dtfecharegistro.TabIndex = 57;
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(37, 84);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(64, 13);
		this.label22.TabIndex = 56;
		this.label22.Text = "F. Registro :";
		this.dtfechaingresoalmacen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfechaingresoalmacen.Location = new System.Drawing.Point(114, 109);
		this.dtfechaingresoalmacen.Name = "dtfechaingresoalmacen";
		this.dtfechaingresoalmacen.Size = new System.Drawing.Size(115, 20);
		this.dtfechaingresoalmacen.TabIndex = 55;
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(-1, 112);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(104, 13);
		this.label21.TabIndex = 54;
		this.label21.Text = "F. Ingreso Almacen :";
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(258, 28);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(86, 13);
		this.label20.TabIndex = 53;
		this.label20.Text = "N.Orden Compra";
		this.txtnumeroOc.Enabled = false;
		this.txtnumeroOc.Location = new System.Drawing.Point(350, 24);
		this.txtnumeroOc.Name = "txtnumeroOc";
		this.txtnumeroOc.Size = new System.Drawing.Size(49, 20);
		this.txtnumeroOc.TabIndex = 52;
		this.txtTranferencia.Location = new System.Drawing.Point(742, 131);
		this.txtTranferencia.Name = "txtTranferencia";
		this.txtTranferencia.Size = new System.Drawing.Size(191, 20);
		this.txtTranferencia.TabIndex = 50;
		this.txtTranferencia.Tag = "20";
		this.txtTranferencia.Text = " ";
		this.txtTranferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTranferencia_KeyPress);
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(671, 134);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(67, 13);
		this.label19.TabIndex = 51;
		this.label19.Tag = "20";
		this.label19.Text = "Tranferencia";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(417, 27);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(55, 13);
		this.label5.TabIndex = 49;
		this.label5.Text = "Num. Doc";
		this.txtNumero.Location = new System.Drawing.Point(154, 24);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(49, 20);
		this.txtNumero.TabIndex = 2;
		this.txtNumero.Visible = false;
		this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumero_KeyPress);
		this.txtNumero.Leave += new System.EventHandler(txtNumero_Leave);
		this.txtSerie.BackColor = System.Drawing.SystemColors.Info;
		this.txtSerie.Location = new System.Drawing.Point(115, 24);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(33, 20);
		this.txtSerie.TabIndex = 1;
		this.superValidator1.SetValidator1(this.txtSerie, this.customValidator5);
		this.txtSerie.TextChanged += new System.EventHandler(txtSerie_TextChanged);
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.txtFactura.BackColor = System.Drawing.Color.Bisque;
		this.txtFactura.Location = new System.Drawing.Point(742, 75);
		this.txtFactura.Name = "txtFactura";
		this.txtFactura.ReadOnly = true;
		this.txtFactura.Size = new System.Drawing.Size(191, 20);
		this.txtFactura.TabIndex = 6;
		this.txtFactura.Tag = "20";
		this.txtFactura.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFactura_KeyDown);
		this.txtFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFactura_KeyPress);
		this.txtFactura.Leave += new System.EventHandler(txtFactura_Leave);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(664, 78);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(60, 13);
		this.label2.TabIndex = 43;
		this.label2.Tag = "20";
		this.label2.Text = "Fact. / Bol.";
		this.cmbMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMotivo.FormattingEnabled = true;
		this.cmbMotivo.Items.AddRange(new object[14]
		{
			"Venta", "Venta sujeta a confirmación del comprador", "Compra", "Consignación", "Devolución", "Traslado entre Establec. de la misma empresa", "Traslado de bienes para trasnformación", "Recojo de Bienes", "Traslado por bienes itinerante de comprob. de pago", "Traslado zona primaria",
			"Importacion", "Exportacion", "Venta con entrega a terceros", "Otros"
		});
		this.cmbMotivo.Location = new System.Drawing.Point(742, 25);
		this.cmbMotivo.Name = "cmbMotivo";
		this.cmbMotivo.Size = new System.Drawing.Size(192, 20);
		this.cmbMotivo.TabIndex = 4;
		this.cmbMotivo.Tag = "5";
		this.superValidator1.SetValidator1(this.cmbMotivo, this.customValidator2);
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(664, 28);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(72, 13);
		this.label18.TabIndex = 40;
		this.label18.Text = "Motivo Trans.";
		this.dtpFechaTransporte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaTransporte.Location = new System.Drawing.Point(498, 51);
		this.dtpFechaTransporte.Name = "dtpFechaTransporte";
		this.dtpFechaTransporte.Size = new System.Drawing.Size(115, 20);
		this.dtpFechaTransporte.TabIndex = 3;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(399, 54);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(73, 13);
		this.label17.TabIndex = 38;
		this.label17.Text = "F. Transporte:";
		this.groupBox5.Controls.Add(this.txtDireccionTransporte);
		this.groupBox5.Controls.Add(this.label16);
		this.groupBox5.Controls.Add(this.txtRazonSocialTransporte);
		this.groupBox5.Controls.Add(this.label14);
		this.groupBox5.Controls.Add(this.label13);
		this.groupBox5.Controls.Add(this.txtRUCTransporte);
		this.groupBox5.Location = new System.Drawing.Point(461, 265);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(445, 100);
		this.groupBox5.TabIndex = 37;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Empresa de Tranportes";
		this.txtDireccionTransporte.Location = new System.Drawing.Point(84, 71);
		this.txtDireccionTransporte.Name = "txtDireccionTransporte";
		this.txtDireccionTransporte.ReadOnly = true;
		this.txtDireccionTransporte.Size = new System.Drawing.Size(343, 20);
		this.txtDireccionTransporte.TabIndex = 20;
		this.txtDireccionTransporte.Tag = "5";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(17, 74);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(52, 13);
		this.label16.TabIndex = 16;
		this.label16.Text = "Dirección";
		this.txtRazonSocialTransporte.Location = new System.Drawing.Point(84, 45);
		this.txtRazonSocialTransporte.Name = "txtRazonSocialTransporte";
		this.txtRazonSocialTransporte.ReadOnly = true;
		this.txtRazonSocialTransporte.Size = new System.Drawing.Size(343, 20);
		this.txtRazonSocialTransporte.TabIndex = 19;
		this.txtRazonSocialTransporte.Tag = "5";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(17, 22);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(30, 13);
		this.label14.TabIndex = 14;
		this.label14.Text = "RUC";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(17, 48);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(61, 13);
		this.label13.TabIndex = 13;
		this.label13.Text = "Raz. Social";
		this.txtRUCTransporte.BackColor = System.Drawing.SystemColors.Info;
		this.txtRUCTransporte.Location = new System.Drawing.Point(84, 19);
		this.txtRUCTransporte.Name = "txtRUCTransporte";
		this.txtRUCTransporte.Size = new System.Drawing.Size(147, 20);
		this.txtRUCTransporte.TabIndex = 18;
		this.txtRUCTransporte.Tag = "5";
		this.txtRUCTransporte.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRUCTransporte_KeyDown);
		this.txtRUCTransporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRUCTransporte_KeyPress);
		this.txtRUCTransporte.Leave += new System.EventHandler(txtRUCTransporte_Leave);
		this.groupBox4.Controls.Add(this.cmbConductor);
		this.groupBox4.Controls.Add(this.cmbVehiculos);
		this.groupBox4.Controls.Add(this.txtMarcaVehiculo);
		this.groupBox4.Controls.Add(this.txtLicencia);
		this.groupBox4.Controls.Add(this.label12);
		this.groupBox4.Controls.Add(this.txtConstancia);
		this.groupBox4.Controls.Add(this.label11);
		this.groupBox4.Controls.Add(this.label10);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Location = new System.Drawing.Point(12, 265);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(444, 100);
		this.groupBox4.TabIndex = 36;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Datos del Transporte / Conductor";
		this.cmbConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbConductor.FormattingEnabled = true;
		this.cmbConductor.Items.AddRange(new object[5] { "EXCELENTE", "BUENO", "REGULAR", "MALO", "MOROSO" });
		this.cmbConductor.Location = new System.Drawing.Point(77, 46);
		this.cmbConductor.Name = "cmbConductor";
		this.cmbConductor.Size = new System.Drawing.Size(354, 20);
		this.cmbConductor.TabIndex = 65;
		this.superValidator1.SetValidator1(this.cmbConductor, this.customValidator4);
		this.cmbConductor.SelectionChangeCommitted += new System.EventHandler(cmbConductor_SelectionChangeCommitted);
		this.cmbConductor.Leave += new System.EventHandler(cmbConductor_Leave);
		this.cmbVehiculos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbVehiculos.FormattingEnabled = true;
		this.cmbVehiculos.Items.AddRange(new object[5] { "EXCELENTE", "BUENO", "REGULAR", "MALO", "MOROSO" });
		this.cmbVehiculos.Location = new System.Drawing.Point(77, 20);
		this.cmbVehiculos.Name = "cmbVehiculos";
		this.cmbVehiculos.Size = new System.Drawing.Size(135, 20);
		this.cmbVehiculos.TabIndex = 64;
		this.superValidator1.SetValidator1(this.cmbVehiculos, this.customValidator3);
		this.cmbVehiculos.SelectionChangeCommitted += new System.EventHandler(cmbVehiculos_SelectionChangeCommitted);
		this.cmbVehiculos.Leave += new System.EventHandler(cmbVehiculos_Leave);
		this.txtMarcaVehiculo.Location = new System.Drawing.Point(278, 19);
		this.txtMarcaVehiculo.Name = "txtMarcaVehiculo";
		this.txtMarcaVehiculo.ReadOnly = true;
		this.txtMarcaVehiculo.Size = new System.Drawing.Size(153, 20);
		this.txtMarcaVehiculo.TabIndex = 14;
		this.txtMarcaVehiculo.Tag = "20";
		this.txtLicencia.Location = new System.Drawing.Point(296, 73);
		this.txtLicencia.Name = "txtLicencia";
		this.txtLicencia.ReadOnly = true;
		this.txtLicencia.Size = new System.Drawing.Size(135, 20);
		this.txtLicencia.TabIndex = 17;
		this.txtLicencia.Tag = "20";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(226, 76);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(64, 13);
		this.label12.TabIndex = 35;
		this.label12.Tag = "20";
		this.label12.Text = "N° Lic. Con.";
		this.txtConstancia.Location = new System.Drawing.Point(77, 73);
		this.txtConstancia.Name = "txtConstancia";
		this.txtConstancia.ReadOnly = true;
		this.txtConstancia.Size = new System.Drawing.Size(135, 20);
		this.txtConstancia.TabIndex = 16;
		this.txtConstancia.Tag = "20";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(15, 76);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(54, 13);
		this.label11.TabIndex = 33;
		this.label11.Tag = "20";
		this.label11.Text = "Cons. Ins.";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(15, 49);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(56, 13);
		this.label10.TabIndex = 14;
		this.label10.Text = "Conductor";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(235, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(37, 13);
		this.label6.TabIndex = 12;
		this.label6.Text = "Marca";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(15, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(49, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "N° Placa";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(101, 161);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(524, 20);
		this.txtDireccion.TabIndex = 7;
		this.txtDireccion.Tag = "21";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(29, 164);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(52, 13);
		this.label4.TabIndex = 35;
		this.label4.Tag = "21";
		this.label4.Text = "Direccion";
		this.txtPedido.Location = new System.Drawing.Point(742, 51);
		this.txtPedido.Name = "txtPedido";
		this.txtPedido.Size = new System.Drawing.Size(191, 20);
		this.txtPedido.TabIndex = 5;
		this.txtPedido.Tag = "20";
		this.txtPedido.Text = " ";
		this.txtPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPedido_KeyPress);
		this.txtPedido.Leave += new System.EventHandler(txtPedido_Leave);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(664, 54);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(40, 13);
		this.label8.TabIndex = 31;
		this.label8.Tag = "20";
		this.label8.Text = "Pedido";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(196, 137);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(429, 20);
		this.txtNombreCliente.TabIndex = 6;
		this.txtNombreCliente.Tag = "3";
		this.txtCodCliente.BackColor = System.Drawing.Color.PeachPuff;
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.ForeColor = System.Drawing.Color.Black;
		this.txtCodCliente.Location = new System.Drawing.Point(101, 137);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(89, 20);
		this.txtCodCliente.TabIndex = 3;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.customValidator1);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(29, 140);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(39, 13);
		this.label15.TabIndex = 20;
		this.label15.Tag = "5";
		this.label15.Text = "Cliente";
		this.btnDetalle.Enabled = false;
		this.btnDetalle.Location = new System.Drawing.Point(859, 100);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(75, 23);
		this.btnDetalle.TabIndex = 12;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(101, 186);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(524, 73);
		this.txtComentario.TabIndex = 11;
		this.txtComentario.Tag = "21";
		this.txtComentario.Leave += new System.EventHandler(txtComentario_Leave);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(29, 189);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(60, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Comentario";
		this.txtNumDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Location = new System.Drawing.Point(524, 25);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 20);
		this.txtNumDoc.TabIndex = 1;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(43, 28);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(57, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Guia Rem.";
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(114, 54);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(115, 20);
		this.dtpFecha.TabIndex = 2;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(41, 57);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "F. Emision :";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.AutoSize = true;
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Location = new System.Drawing.Point(0, 380);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1202, 179);
		this.groupBox2.TabIndex = 26;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.cantidadnueva, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.coduser, this.fecharegistro, this.cantidadpendiente, this.peso, this.stockPend, this.maxPorcDes, this.codigoventa, this.numorden, this.productosolicitado, this.EstadoDeLaOrden, this.etiqueta);
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle12;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.MultiSelect = false;
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
		this.dgvDetalle.Size = new System.Drawing.Size(1020, 141);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellDoubleClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetalle_DataError);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.dgvDetalle.Enter += new System.EventHandler(dgvDetalle_Enter);
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
		this.referencia.Width = 170;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 171;
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
		this.unidad.Width = 170;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		dataGridViewCellStyle14.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle14;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 171;
		this.cantidadnueva.DataPropertyName = "cantidadpendiente";
		this.cantidadnueva.HeaderText = "Cantidad Nueva";
		this.cantidadnueva.Name = "cantidadnueva";
		this.cantidadnueva.Visible = false;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle15;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Visible = false;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle16;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Visible = false;
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
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle21.Format = "N2";
		dataGridViewCellStyle21.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle21;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle22.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle22;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle23.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle23;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Visible = false;
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
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.cantidadpendiente.DataPropertyName = "cantidadpendiente";
		this.cantidadpendiente.HeaderText = "Cant.Pend.Orden";
		this.cantidadpendiente.Name = "cantidadpendiente";
		this.cantidadpendiente.Visible = false;
		this.cantidadpendiente.Width = 170;
		this.peso.DataPropertyName = "peso";
		this.peso.HeaderText = "peso";
		this.peso.Name = "peso";
		this.peso.Visible = false;
		this.stockPend.DataPropertyName = "stockdisponible";
		this.stockPend.HeaderText = "StockDisponible";
		this.stockPend.Name = "stockPend";
		this.stockPend.Visible = false;
		this.maxPorcDes.DataPropertyName = "maxPorcDescto";
		this.maxPorcDes.HeaderText = "MaxPorcDes";
		this.maxPorcDes.Name = "maxPorcDes";
		this.maxPorcDes.Visible = false;
		this.codigoventa.DataPropertyName = "codventa";
		this.codigoventa.HeaderText = "CodVenta";
		this.codigoventa.Name = "codigoventa";
		this.codigoventa.Visible = false;
		this.numorden.DataPropertyName = "numorden";
		this.numorden.HeaderText = "NumRelaOrdCompra";
		this.numorden.Name = "numorden";
		this.numorden.Visible = false;
		this.productosolicitado.DataPropertyName = "productosolicitado";
		this.productosolicitado.HeaderText = "ProdSolicitado";
		this.productosolicitado.Name = "productosolicitado";
		this.productosolicitado.Visible = false;
		this.EstadoDeLaOrden.DataPropertyName = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.HeaderText = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.Name = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.Visible = false;
		this.EstadoDeLaOrden.Width = 171;
		this.etiqueta.DataPropertyName = "etiqueta";
		this.etiqueta.HeaderText = "Estado";
		this.etiqueta.Name = "etiqueta";
		this.etiqueta.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.etiqueta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.etiqueta.Width = 170;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator5.ErrorMessage = "Seleccione una Serie";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator2.ErrorMessage = "Seleccione el motivo.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator4.ErrorMessage = "Seleccione el conductor.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator3.ErrorMessage = "Seleccione un vehículo.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator1.ErrorMessage = "Seleccione el cliente.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.groupBox3.Controls.Add(this.btGenVenta);
		this.groupBox3.Controls.Add(this.btnNuevaGuia);
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 537);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1202, 66);
		this.groupBox3.TabIndex = 24;
		this.groupBox3.TabStop = false;
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.BackColor = System.Drawing.Color.White;
		this.btGenVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btGenVenta.Image = (System.Drawing.Image)resources.GetObject("btGenVenta.Image");
		this.btGenVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btGenVenta.Location = new System.Drawing.Point(651, 28);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(96, 38);
		this.btGenVenta.TabIndex = 28;
		this.btGenVenta.Text = "Generar F.Compra";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = false;
		this.btGenVenta.Visible = false;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnNuevaGuia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevaGuia.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevaGuia.ImageIndex = 1;
		this.btnNuevaGuia.ImageList = this.imageList1;
		this.btnNuevaGuia.Location = new System.Drawing.Point(6, 28);
		this.btnNuevaGuia.Name = "btnNuevaGuia";
		this.btnNuevaGuia.Size = new System.Drawing.Size(105, 32);
		this.btnNuevaGuia.TabIndex = 27;
		this.btnNuevaGuia.Text = "Nueva Guia";
		this.btnNuevaGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevaGuia.UseVisualStyleBackColor = true;
		this.btnNuevaGuia.Visible = false;
		this.btnNuevaGuia.Click += new System.EventHandler(btnNuevaGuia_Click);
		this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(788, 28);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 32);
		this.btnImprimir.TabIndex = 26;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(955, 28);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 22;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(159, 28);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 23;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(872, 28);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 21;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(236, 28);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 24;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(308, 28);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 25;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1202, 603);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGuiaRemision";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Guia de Remisión";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmGuiaRemision_Load);
		base.Shown += new System.EventHandler(frmNotaSalida_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
