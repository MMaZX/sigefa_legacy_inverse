using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.SunatFacElec;
using Tesseract;

namespace SIGEFA.Formularios;

public class frmGeneraVenta : Office2007Form
{
	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsPago Pag = new clsPago();

	private clsAdmPago AdmPagos = new clsAdmPago();

	private Facturacion conex = new Facturacion();

	private clsReporteFactura ds1 = new clsReporteFactura();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();

	public List<clsDetalleFacturaVenta> detalle1 = new List<clsDetalleFacturaVenta>();

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private clsAdmVendedor AdmVen = new clsAdmVendedor();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private List<clsNotaCredito> ncredito = new List<clsNotaCredito>();

	private clsAdmNotaCredito admNotac = new clsAdmNotaCredito();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsTransaccion tran = new clsTransaccion();

	private List<clsFacturaVenta> ltaventa = new List<clsFacturaVenta>();

	private clsReporteCodigoBarras ds = new clsReporteCodigoBarras();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	private clsValidar ok = new clsValidar();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie AdmSerie = new clsAdmSerie();

	private clsEmpresa empress = new clsEmpresa();

	private clsAdmEmpresa admempress = new clsAdmEmpresa();

	private clsAdmUsuario AdmUs = new clsAdmUsuario();

	private clsAdmProducto admprod = new clsAdmProducto();

	private clsAdmDocumentoIdentidad AdmDocumentoIdentidad = new clsAdmDocumentoIdentidad();

	public int CodSerie;

	public int manual = 0;

	private Sunat MyInfoSunat;

	private Reniec MyInfoReniec;

	private IntRange red = new IntRange(0, 255);

	private IntRange green = new IntRange(0, 255);

	private IntRange blue = new IntRange(0, 255);

	private DataTable NuevaTabla = new DataTable();

	private int item = 1;

	private int facturarsinmoverstock;

	private clsAdmParametro admParametro = new clsAdmParametro();

	private List<clsPedido> PedidosIngresados = new List<clsPedido>();

	public int impresion;

	public int CodCliente;

	public int CodDocumento;

	public int Tipo;

	public int CodPedido;

	public int CodTransaccion;

	public int CodigoCaja;

	public int Procede;

	public int Proceso = 0;

	public int ActivaCabecera = 0;

	public decimal montogratuitas;

	public decimal montogravadas;

	public decimal montoexoneradas = default(decimal);

	public decimal montoinafectas = default(decimal);

	public string CodVenta;

	public string NombreCliente;

	public bool banderagrabada;

	public bool banderaexonerada;

	public bool banderainafecta;

	public bool banderadelete = false;

	public bool bandera = false;

	private DateTime time = DateTime.Now;

	private bool banderaCancelarIngresoSerie = false;

	private List<int> ListaEmpresa = new List<int>();

	private List<int> ListaCantDoc = new List<int>();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Panel panel1;

	private GroupBox groupBox2;

	private Button btnSalir;

	private Button btnGuardaVenta;

	private GroupBox groupBox4;

	private DateTimePicker dtpFecha;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Panel panel2;

	private Label label10;

	private Label label9;

	private Label label8;

	private Panel panel3;

	private Label label11;

	public CheckBox chkVentaGratuita;

	public CheckBox chkVentaDsctoGlobal;

	private TextBox txtNombreCliente;

	private TextBox txtCodCliente;

	public TextBox txtDireccion;

	private DateTimePicker dtpFechaPago;

	private ComboBox cmbFormaPago;

	public TextBox txtDocRef;

	private TextBox txtSunat_Capchat;

	private PictureBox pbCapchatS;

	public DataGridView dgvDetalle;

	private TextBox txtPrecioVenta;

	private TextBox txtIGV;

	private TextBox txtValorVenta;

	private TextBox txtPDescuento;

	private TextBox txtBruto;

	private TextBox txtDscto;

	private Label label2;

	private Label label13;

	private ImageList imageList1;

	private Label label1;

	private TextBox txtSerie;

	private TextBox txtPedido;

	private Highlighter highlighter1;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private CustomValidator customValidator4;

	private CustomValidator customValidator6;

	private TextBox txtinafectas;

	private Label label26;

	private TextBox txtexoneradas;

	private Label label32;

	private TextBox txtgratuitas;

	private Label label19;

	private TextBox txtgravadas;

	private Label label22;

	private ImageList imageList2;

	private TextBox txtCodigoBarras;

	private TextBox txtDelOV;

	private Label label14;

	private TextBox txtComentario;

	private GroupBox groupBox3;

	private TextBox txttasa;

	private Label label30;

	private Label lbLineaCredito;

	private TextBox txtLineaCredito;

	private TextBox txtLineaCreditoUso;

	private Label label23;

	private Label label25;

	private TextBox txtLineaCreditoDisponible;

	private TextBox txtAddOV;

	private Button btnDeleteItem;

	private Button btnAddOV;

	private Label label24;

	private ComboBox cbovendedor;

	private RadioButton rbtnPendiente;

	public TextBox txtTransaccion;

	private Label lbNombreTransaccion;

	private Label label15;

	private TextBox txtNumDoc;

	private Button btnImprimir;

	private Button btnInicioOV;

	public CheckBox chkVentaEspecial;

	private Button button1;

	private Label lblCantidadProductos;

	private ComboBoxEx cmbDocumentoIdentidad;

	public RadioButton chkTicket;

	public RadioButton chkFactura;

	public RadioButton chkBoleta;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

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

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn precioconigv;

	private DataGridViewTextBoxColumn valorpromedio;

	private DataGridViewTextBoxColumn Tipoarticulo;

	private DataGridViewTextBoxColumn Tipoimpuesto;

	private DataGridViewTextBoxColumn codalmacen;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn TipoUnidad;

	private DataGridViewTextBoxColumn CodigoEmpresa;

	private DataGridViewTextBoxColumn descripcion_venta;

	private DataGridViewTextBoxColumn serie_motor;

	private DataGridViewTextBoxColumn nrochasis;

	private DataGridViewTextBoxColumn modelo;

	private DataGridViewTextBoxColumn Marca;

	private DataGridViewTextBoxColumn color;

	private DataGridViewTextBoxColumn codFamilia;

	private DataGridViewTextBoxColumn venta_tickeck;

	private DataGridViewTextBoxColumn codigoproductosunat;

	private DataGridViewTextBoxColumn codli;

	private DataGridViewTextBoxColumn codfa;

	public byte[] firmadigital { get; set; }

	public frmGeneraVenta()
	{
		InitializeComponent();
		PedidosIngresados = new List<clsPedido>();
	}

	private void frmGeneraVenta_KeyDown(object sender, KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
		case Keys.F6:
			activaPaneles(estado: true);
			break;
		case Keys.F3:
			if (bandera)
			{
				if (txtAddOV.Text != "")
				{
					btnAddOV_Click(sender, new EventArgs());
					break;
				}
				txtAddOV.Focus();
				txtAddOV.SelectAll();
			}
			break;
		case Keys.F2:
			if (bandera)
			{
				btnDeleteItem_Click(sender, new EventArgs());
			}
			break;
		case Keys.F1:
			if (bandera)
			{
				txtCodCliente_KeyDown(sender, e);
			}
			break;
		case Keys.F8:
			btnGuardaVenta_Click(sender, e);
			break;
		case Keys.F9:
			Close();
			break;
		case Keys.F4:
		case Keys.F5:
		case Keys.F7:
			break;
		}
	}

	private void activaPaneles(bool estado)
	{
		groupBox2.Enabled = estado;
		btnAddOV.Enabled = estado;
		btnDeleteItem.Enabled = estado;
		btnSalir.Enabled = estado;
		btnGuardaVenta.Enabled = estado;
		groupBox4.Enabled = estado;
		panel2.Enabled = estado;
		btnInicioOV.Enabled = !estado;
		bandera = estado;
	}

	private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
	{
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
		if (txtCodCliente.Text != "")
		{
			cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
			if (cli != null)
			{
				txtNombreCliente.Text = cli.RazonSocial;
				CodCliente = cli.CodCliente;
				txtDireccion.Text = cli.DireccionLegal;
				txtCodCliente.Text = cli.RucDni;
				if (cli.RucDni == "00000000")
				{
					txtNombreCliente.Enabled = true;
				}
				txtDocRef.Visible = false;
				return true;
			}
			txtNombreCliente.Text = "";
			CodCliente = 0;
			txtDireccion.Text = "";
			return false;
		}
		return false;
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
		cmbFormaPago_SelectionChangeCommitted(new object(), new EventArgs());
		cmbFormaPago.Visible = true;
		dtpFechaPago.Visible = true;
	}

	private bool BuscaTipoDocumento()
	{
		doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			label1.Visible = true;
			return true;
		}
		CodDocumento = 0;
		return false;
	}

	private void frmGeneraVenta_Shown(object sender, EventArgs e)
	{
		txtTransaccion.Focus();
		txtTransaccion.Text = "FT";
		KeyPressEventArgs e2 = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, e2);
		if (Proceso == 1)
		{
			txtDocRef.Text = "BV";
			txtCodCliente.Text = "C000001";
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			txtDocRef_KeyPress(txtDocRef, ee);
			txtSerie_KeyPress(txtDocRef, ee);
			BuscaCliente();
			cargadatoscliente(txtCodCliente.Text);
			calculatotales();
			if (admParametro.consultarParametroVenta(2))
			{
				chkTicket.Visible = true;
			}
		}
		else if (Proceso == 3)
		{
			btnSalir.Visible = true;
			btnSalir.Enabled = true;
		}
		else if (Proceso == 4 && admParametro.consultarParametroVenta(2))
		{
			chkTicket.Visible = true;
		}
	}

	private void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtDocRef.Text != "" && !BuscaTipoDocumento())
		{
			MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE SALIDA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cmbFormaPago.SelectedIndex != -1)
		{
			fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
			dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
		}
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar != '\r')
		{
			return;
		}
		try
		{
			switch (txtCodCliente.Text.Length)
			{
			case 1:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo un digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 2:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo dos digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 3:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo tres digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 4:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cuatro digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 5:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cinco digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 6:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo seis digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 7:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo siete digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 8:
				cli = AdmCli.ConsultaCliente(txtCodCliente.Text);
				if (cli != null)
				{
					CodCliente = cli.CodCliente;
					txtNombreCliente.Text = cli.Nombre;
					txtDireccion.Text = cli.DireccionLegal;
					if (cli.Moneda == 1)
					{
						txtLineaCredito.Text = $"{cli.LineaCredito:#,##0.00}";
						txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible:#,##0.00}";
						txtLineaCreditoUso.Text = $"{cli.LineaCreditoUsado:#,##0.00}";
						txttasa.Text = $"{cli.Tasa:#,##0.00}";
						lbLineaCredito.Text = "Línea de Crédito (S/.):";
						label23.Text = "Línea Disponible (S/.):";
						label25.Text = "Línea C. en Uso (S/.):";
						if (cli.LineaCredito > 0m)
						{
							cmbFormaPago.Enabled = true;
						}
						else
						{
							cmbFormaPago.Enabled = false;
						}
					}
					else
					{
						txtLineaCredito.Text = $"{cli.LineaCredito / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
						txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
						TextBox textBox2 = txtLineaCreditoUso;
						string text = (Text = $"{cli.LineaCreditoUsado / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}");
						textBox2.Text = text;
						lbLineaCredito.Text = "Línea de Crédito ($.):";
						label23.Text = "Línea Disponible ($.):";
						label25.Text = "Línea C. en Uso ($.):";
						if (cli.LineaCredito > 0m)
						{
							cmbFormaPago.Enabled = true;
						}
						else
						{
							cmbFormaPago.Enabled = false;
						}
					}
					if (cli.FormaPago != 0)
					{
						cmbFormaPago.SelectedValue = cli.FormaPago;
						EventArgs ee2 = new EventArgs();
						cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee2);
					}
					else
					{
						dtpFechaPago.Value = DateTime.Today;
					}
				}
				else
				{
					CargarImagenReniec();
					CargaDNI();
					CodCliente = 0;
				}
				chkBoleta.Checked = true;
				break;
			case 9:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso nueve digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			case 10:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso diez digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCodCliente.SelectAll();
				txtCodCliente.Focus();
				break;
			case 11:
				cli = AdmCli.ConsultaCliente(txtCodCliente.Text);
				if (cli != null)
				{
					CodCliente = cli.CodCliente;
					txtNombreCliente.Text = cli.RazonSocial;
					txtDireccion.Text = cli.DireccionLegal;
					if (cli.Moneda == 1)
					{
						txtLineaCredito.Text = $"{cli.LineaCredito:#,##0.00}";
						txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible:#,##0.00}";
						txtLineaCreditoUso.Text = $"{cli.LineaCreditoUsado:#,##0.00}";
						txttasa.Text = $"{cli.Tasa:#,##0.00}";
						lbLineaCredito.Text = "Línea de Crédito (S/.):";
						label23.Text = "Línea Disponible (S/.):";
						label25.Text = "Línea C. en Uso (S/.):";
						if (cli.LineaCredito > 0m)
						{
							cmbFormaPago.Enabled = true;
						}
						else
						{
							cmbFormaPago.Enabled = false;
						}
					}
					else
					{
						txtLineaCredito.Text = $"{cli.LineaCredito / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
						txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
						TextBox textBox = txtLineaCreditoUso;
						string text = (Text = $"{cli.LineaCreditoUsado / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}");
						textBox.Text = text;
						lbLineaCredito.Text = "Línea de Crédito ($.):";
						label23.Text = "Línea Disponible ($.):";
						label25.Text = "Línea C. en Uso ($.):";
						if (cli.LineaCredito > 0m)
						{
							cmbFormaPago.Enabled = true;
						}
						else
						{
							cmbFormaPago.Enabled = false;
						}
					}
					if (cli.FormaPago != 0)
					{
						cmbFormaPago.SelectedValue = cli.FormaPago;
						EventArgs ee = new EventArgs();
						cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
					}
					else
					{
						dtpFechaPago.Value = DateTime.Today;
					}
				}
				else
				{
					CargarImagenSunat();
					CargaRUC();
					CodCliente = 0;
				}
				chkFactura.Checked = true;
				break;
			default:
				ValidaLongitud();
				break;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			CargarImagenSunat();
		}
	}

	private void ValidaLongitud()
	{
		if (txtCodCliente.Text.Length == 0)
		{
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ningun digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (txtCodCliente.Text.Length > 11)
		{
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ha Ingresado " + txtCodCliente.Text.Length + " Digitos", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtCodCliente.SelectAll();
			txtCodCliente.Focus();
		}
	}

	private void CargaDNI()
	{
		MyInfoReniec.GetInfo(txtCodCliente.Text, txtSunat_Capchat.Text);
		switch (MyInfoReniec.GetResul)
		{
		case Reniec.Resul.Ok:
		{
			limpiarSunat();
			txtCodCliente.Text = MyInfoReniec.Dni;
			string apellidos = MyInfoReniec.ApePaterno + " " + MyInfoReniec.ApeMaterno;
			txtNombreCliente.Text = MyInfoReniec.Nombres + " " + apellidos;
			txtDireccion.Text = "S/D";
			break;
		}
		case Reniec.Resul.NoResul:
			limpiarSunat();
			MessageBox.Show("No Existe DNI");
			break;
		case Reniec.Resul.ErrorCapcha:
			limpiarSunat();
			MessageBox.Show("Ingrese imagen correctamente");
			break;
		default:
			MessageBox.Show("Error Desconocido");
			break;
		}
		CargarImagenReniec();
	}

	private void AplicacionFiltros()
	{
		Bitmap bmp = new Bitmap(pbCapchatS.Image);
		FiltroInvertir(bmp);
		ColorFiltros();
		Bitmap bmp2 = new Bitmap(pbCapchatS.Image);
		FiltroInvertir(bmp2);
		Bitmap bmp3 = new Bitmap(pbCapchatS.Image);
		FiltroSharpen(bmp3);
	}

	private void FiltroInvertir(Bitmap bmp)
	{
		IFilter Filtro = new Invert();
		Bitmap XImage = Filtro.Apply(bmp);
		pbCapchatS.Image = XImage;
	}

	private void ColorFiltros()
	{
		red.Min = Math.Min(red.Max, byte.Parse("229"));
		red.Max = Math.Max(red.Min, byte.Parse("255"));
		green.Min = Math.Min(green.Max, byte.Parse("0"));
		green.Max = Math.Max(green.Min, byte.Parse("255"));
		blue.Min = Math.Min(blue.Max, byte.Parse("0"));
		blue.Max = Math.Max(blue.Min, byte.Parse("130"));
		ActualizarFiltro();
	}

	private void ActualizarFiltro()
	{
		ColorFiltering FiltroColor = new ColorFiltering();
		FiltroColor.Red = red;
		FiltroColor.Green = green;
		FiltroColor.Blue = blue;
		IFilter Filtro = FiltroColor;
		Bitmap bmp = new Bitmap(pbCapchatS.Image);
		Bitmap XImage = Filtro.Apply(bmp);
		pbCapchatS.Image = XImage;
	}

	private void FiltroSharpen(Bitmap bmp)
	{
		IFilter Filtro = new Sharpen();
		Bitmap XImage = Filtro.Apply(bmp);
		pbCapchatS.Image = XImage;
	}

	private void CargarImagenReniec()
	{
		try
		{
			if (MyInfoReniec == null)
			{
				MyInfoReniec = new Reniec();
			}
			pbCapchatS.Image = MyInfoReniec.GetCapcha;
			AplicacionFiltros();
			LeerCaptchaReniec();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void LeerCaptchaReniec()
	{
		using TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
		using Bitmap image = new Bitmap(pbCapchatS.Image);
		using Pix pix = PixConverter.ToPix(image);
		using Page page = engine.Process(pix);
		string Porcentaje = $"{page.GetMeanConfidence():P}";
		string CaptchaTexto = page.GetText();
		char[] eliminarChars = new char[2] { '\n', ' ' };
		CaptchaTexto = CaptchaTexto.TrimEnd(eliminarChars);
		CaptchaTexto = CaptchaTexto.Replace(" ", string.Empty);
		CaptchaTexto = Regex.Replace(CaptchaTexto, "[^a-zA-Z0-9]+", string.Empty);
		if ((CaptchaTexto != string.Empty) & (CaptchaTexto.Length == 4))
		{
			txtSunat_Capchat.Text = CaptchaTexto.ToUpper();
		}
	}

	private void CargaRUC()
	{
		if (txtCodCliente.Text.Length == 11)
		{
			LeerDatos();
		}
	}

	private void LeerDatos()
	{
		MyInfoSunat.GetInfo(txtCodCliente.Text, txtSunat_Capchat.Text);
		switch (MyInfoSunat.GetResul)
		{
		case Sunat.Resul.Ok:
			limpiarSunat();
			txtCodCliente.Text = MyInfoSunat.Ruc;
			txtDireccion.Text = MyInfoSunat.Direcion;
			txtNombreCliente.Text = MyInfoSunat.RazonSocial;
			Ciudad(MyInfoSunat.Direcion);
			BloqueaDatos();
			break;
		case Sunat.Resul.NoResul:
			limpiarSunat();
			MessageBox.Show("No Existe RUC");
			break;
		case Sunat.Resul.ErrorCapcha:
			limpiarSunat();
			MessageBox.Show("Ingrese imagen correctamente");
			break;
		default:
			MessageBox.Show("Error Desconocido");
			break;
		}
	}

	private void CargarImagenSunat()
	{
		try
		{
			if (MyInfoSunat == null)
			{
				MyInfoSunat = new Sunat();
			}
			pbCapchatS.Image = MyInfoSunat.GetCapcha;
			LeerCaptchaSunat();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void LeerCaptchaSunat()
	{
		try
		{
			using TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
			using Bitmap image = new Bitmap(pbCapchatS.Image);
			using Pix pix = PixConverter.ToPix(image);
			using Page page = engine.Process(pix);
			string Porcentaje = $"{page.GetMeanConfidence():P}";
			string CaptchaTexto = page.GetText();
			char[] eliminarChars = new char[2] { '\n', ' ' };
			CaptchaTexto = CaptchaTexto.TrimEnd(eliminarChars);
			CaptchaTexto = CaptchaTexto.Replace(" ", string.Empty);
			CaptchaTexto = Regex.Replace(CaptchaTexto, "[^a-zA-Z]+", string.Empty);
			if ((CaptchaTexto != string.Empty) & (CaptchaTexto.Length == 4))
			{
				txtSunat_Capchat.Text = CaptchaTexto.ToUpper();
			}
			else
			{
				CargarImagenSunat();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void Ciudad(string Direccion)
	{
		string[] array = Direccion.Split('-');
		if (array.Length > 1)
		{
			int a = array.Length;
			string DirTemp = array[a - 3].Trim();
			DirTemp = DirTemp.TrimEnd(' ');
			string[] ArrayDir = DirTemp.Split(' ');
			int i = ArrayDir.Length;
		}
	}

	private void limpiarSunat()
	{
		txtNombreCliente.Text = "";
		txtSunat_Capchat.Text = string.Empty;
	}

	private void BloqueaDatos()
	{
		txtDireccion.ReadOnly = true;
		txtNombreCliente.ReadOnly = true;
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1)
		{
			Recalcular();
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
			if (row.Index == dgvDetalle.CurrentRow.Index)
			{
				if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "21" && banderadelete)
				{
					montogratuitas -= Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
					montosventa();
					banderadelete = false;
				}
				if ((Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "10" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "11" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "12" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "13" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "14" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "15" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "16" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "17" && banderadelete))
				{
					montogravadas -= Convert.ToDecimal(row.Cells[valorventa.Name].Value);
					montosventa();
					banderadelete = false;
				}
				if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "20" && banderadelete)
				{
					montoexoneradas -= Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
					montosventa();
					banderadelete = false;
				}
				if ((Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "30" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "31" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "32" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "33" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "34" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "35" && banderadelete) || (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "36" && banderadelete))
				{
					montoinafectas -= Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
					montosventa();
					banderadelete = false;
				}
			}
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{bruto - descuen - valor:#,##0.00}";
		txtPrecioVenta.Text = $"{bruto - descuen:#,##0.00}";
		txtPrecioVenta.Text = txtBruto.Text;
		label11.Text = txtBruto.Text;
		montosventa();
	}

	public void montosventa()
	{
		if (Proceso != 0 && Proceso != 3)
		{
			if (montogravadas > 0m)
			{
				txtgravadas.Clear();
				txtgravadas.Text = $"{montogravadas:#,##0.00}";
			}
			else
			{
				txtgravadas.Text = $"{0:#,##0.00}";
			}
			if (montogratuitas > 0m)
			{
				txtgratuitas.Clear();
				txtgratuitas.Text = $"{montogratuitas:#,##0.00}";
			}
			else
			{
				txtgratuitas.Text = $"{0:#,##0.00}";
			}
			if (montoexoneradas > 0m)
			{
				txtexoneradas.Clear();
				txtexoneradas.Text = $"{montoexoneradas:#,##0.00}";
			}
			else
			{
				txtexoneradas.Text = $"{0:#,##0.00}";
			}
			if (montoinafectas > 0m)
			{
				txtinafectas.Clear();
				txtinafectas.Text = $"{montoinafectas:#,##0.00}";
			}
			else
			{
				txtinafectas.Text = $"{0:#,##0.00}";
			}
		}
	}

	private void Recalcular()
	{
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		double figv = 0.0;
		double pven = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
			figv += Convert.ToDouble(row.Cells[igv.Name].Value);
			pven += Convert.ToDouble(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{figv:#,##0.00}";
		txtPrecioVenta.Text = $"{pven:#,##0.00}";
		label11.Text = txtBruto.Text;
		montosventa();
	}

	private void txtCantidadPago_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtVuelto_Leave(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		btnDeleteItem_Click(sender, new EventArgs());
	}

	private void btnDeleteItem_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.SelectedRows.Count > 0 && dgvDetalle.Rows.Count > 0)
		{
			if (Convert.ToInt32(dgvDetalle.CurrentRow.Cells[coddetalle.Name].Value) == 0)
			{
				banderadelete = true;
				calculatotales();
				dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
				calculatotales();
			}
			else if (MessageBox.Show("¿Realmente desea eliminar el item seleccionado?", "Pedido Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && AdmPedido.deletedetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[coddetalle.Name].Value)))
			{
				MessageBox.Show("El detalle se eliminó correctamente", "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				banderadelete = true;
				calculatotales();
				dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
				calculatotales();
			}
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
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
		if (CodSerie == 0)
		{
		}
	}

	private async void btnGuardaVenta_Click(object sender, EventArgs e)
	{
		try
		{
			clsDocumentoIdentidad documentoIdentidadSeleccionado = AdmDocumentoIdentidad.MuestraDocumentoIdentidad(Convert.ToInt32(cmbDocumentoIdentidad.SelectedValue));
			if (documentoIdentidadSeleccionado == null)
			{
				MessageBox.Show("Por favor seleccione un tipo de documento", "Genera Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				btnGuardaVenta.Enabled = true;
			}
			else
			{
				if (!superValidator1.Validate())
				{
					return;
				}
				double totalsoles = ((Convert.ToInt32(cli.Moneda) != 1) ? (Convert.ToDouble(txtPrecioVenta.Text) * mdi_Menu.tc_hoy) : Convert.ToDouble(txtPrecioVenta.Text));
				if (totalsoles > Convert.ToDouble(txtLineaCreditoDisponible.Text) && Convert.ToInt32(cmbFormaPago.SelectedValue) != 6)
				{
					MessageBox.Show("El Monto Excede a la Línea de Crédito");
					return;
				}
				if (Proceso != 0)
				{
					GeneraListaEnpresas();
					venta.CodSucursal = frmLogin.iCodSucursal;
					venta.CodAlmacen = frmLogin.iCodAlmacen;
					venta.CodTipoTransaccion = tran.CodTransaccion;
					venta.CodCliente = CodCliente;
					venta.NumeroDocumentoCliente = txtCodCliente.Text;
					venta.CodTipoDocumento = doc.CodTipoDocumento;
					venta.Detallecomentario = "";
					venta.CodCotizacion = 0;
					if (chkBoleta.Checked)
					{
						venta.Boletafactura = 1;
					}
					else if (chkFactura.Checked)
					{
						venta.Boletafactura = 2;
					}
					venta.NumDoc = txtPedido.Text;
					venta.Estado = 1;
					venta.Consultorext = false;
					venta.Codsalidaconsulext = 0;
					venta.CodPedido = Convert.ToInt32(pedido.CodPedido);
					venta.Moneda = 1;
					venta.TipoCambio = mdi_Menu.tc_hoy;
					venta.FechaSalida = dtpFecha.Value;
					venta.FechaPago = dtpFechaPago.Value;
					venta.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
					venta.CodListaPrecio = 0;
					venta.CodVendedor = Convert.ToInt32(cbovendedor.SelectedValue);
					venta.Comentario = txtComentario.Text;
					venta.CodUser = frmLogin.iCodUser;
					venta.Entregado = Convert.ToInt32(rbtnPendiente.Checked);
					venta.CodigoBarras = DateTime.Today.Year.ToString().Substring(2, 2) + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0') + DateTime.Now.ToShortTimeString().Substring(0, 2) + DateTime.Now.ToShortTimeString().Substring(3, 2) + venta.CodSerie.ToString().PadLeft(3, '0') + CodCliente;
					venta.CodigoBarrasCifrado = "";
					venta.ventasinstock = facturarsinmoverstock;
					foreach (clsPedido ped in PedidosIngresados)
					{
						ped.CodigoBarras = venta.CodigoBarras;
						ped.CodigoBarrasCifrado = venta.CodigoBarrasCifrado;
					}
					if (txtCodCliente.Text.ToString() == "00000000")
					{
						venta.Nombre = txtNombreCliente.Text;
					}
					else
					{
						venta.Nombre = "";
					}
					new clsFacturaVenta();
					clsFacturaVenta factura = AdmVenta.FechaCorrelativoAnterior(venta.CodSerie);
					foreach (int lista in ListaEmpresa)
					{
						VerificaDocumentosPorEmpresa(lista);
						foreach (int listadoc in ListaCantDoc)
						{
							if (mdi_Menu.MontoTopeBoleta > 0 && chkVentaEspecial.Checked)
							{
								CrearTablaTemporal();
								OrdenarTablaTemporal();
								CreaBoletas(lista);
							}
							else
							{
								ArmaCabecera(lista, listadoc);
								if (ActivaCabecera == 0)
								{
									return;
								}
							}
							venta.CodEmpresa = lista;
							if (Proceso != 4 && Proceso != 1)
							{
								continue;
							}
							if (factura.FechaSalida > venta.FechaSalida.Date)
							{
								MessageBox.Show("Error No se puede Registrar los Datos. Verifique Fecha");
							}
							else
							{
								if (!VerificarDetracciones())
								{
									continue;
								}
								if (mdi_Menu.MontoTopeBoleta > 0 && chkVentaEspecial.Checked)
								{
									for (int i = 1; i <= item; i++)
									{
										NuevaCabecera(i);
										if (AdmVenta.insert(venta))
										{
											RecorreDetalleEspecial(i, lista);
											ImprimeEspecial(lista);
										}
									}
									continue;
								}
								RecorreDetalle(lista, listadoc);
								venta.Detalle = detalle1;
								venta.DocumentoIdentidad = documentoIdentidadSeleccionado;
								if (detalle1.Count <= 0)
								{
									continue;
								}
								if (AdmVenta.insertComprobante(venta))
								{
									foreach (clsDetalleFacturaVenta det in venta.Detalle)
									{
										int unidadbase = admprod.UnidadBase(det.CodProducto, venta.CodAlmacen);
										int productofacturado = admprod.GetProductoFacturado(det.CodProducto);
										decimal factor = admprod.FactorProducto(det.CodProducto, det.UnidadIngresada, unidadbase, 2);
										decimal valorpromediosoles = admprod.GetValorPromedioSoles(det.CodProducto, venta.CodAlmacen);
										if (venta.ventasinstock == 0 && productofacturado == 0 && venta.CodTipoDocumento == 7)
										{
											if (admprod.ExisteProductoSinFacturar(det.CodProducto) > 0)
											{
												AdmVenta.ActualizaStockSinFacturar(det, unidadbase, factor, 1);
												continue;
											}
											decimal stock = Convert.ToDecimal(det.Cantidad) * factor;
											decimal soles = stock * valorpromediosoles;
											AdmVenta.InsertaProductoSinFacturar(det, valorpromediosoles, stock, soles, unidadbase);
										}
										else if (venta.ventasinstock == 1 && productofacturado == 0 && admprod.ExisteProductoSinFacturar(det.CodProducto) > 0)
										{
											AdmVenta.ActualizaStockSinFacturar(det, unidadbase, factor, 2);
										}
									}
									MessageBox.Show("Los datos se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									txtNumDoc.Text = venta.CodFacturaVenta.PadLeft(11, '0');
									ltaventa.Add(venta);
									if (ncredito.Count > 0)
									{
										frmCancelarPago form = new frmCancelarPago();
										form.CodNota = venta.CodFacturaVenta;
										form.VentComp = 1;
										form.tipo = 3;
										form.CodCliente = cli.CodCliente;
										form.ShowDialog();
									}
									else
									{
										if (fpago.Dias == 0 && venta.CodTipoTransaccion == 7)
										{
											ingresarpago();
										}
										CodVenta = venta.CodFacturaVenta;
										if (venta.FormaPago != 6)
										{
											btnImprimir.Visible = true;
										}
									}
									if (listadoc == 0 && !chkTicket.Checked)
									{
										await conex.GeneraDocumento(cli, venta, detalle1, 0);
										firmadigital = conex.LogoEmp;
									}
									fnImprimir();
								}
								else
								{
									MessageBox.Show("Ocurrió un problema al registrar la venta.", "Registro de Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
						}
					}
					foreach (clsPedido ped2 in PedidosIngresados)
					{
						AdmPedido.GuardaCodigoBarras(ped2);
					}
				}
				PedidosIngresados = new List<clsPedido>();
				Close();
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show("Error: " + ex2.Message.ToString());
		}
	}

	private void Form_Closed(object sender, FormClosedEventArgs e)
	{
		frmAgregaInformacionProducto frm = (frmAgregaInformacionProducto)sender;
		banderaCancelarIngresoSerie = frm.cancelado;
	}

	private async void ImprimeEspecial(int lista)
	{
		if (detalle1.Count > 0)
		{
			foreach (clsDetalleFacturaVenta det in detalle1)
			{
				AdmVenta.insertdetalle(det);
				if (det.CodDetalleVenta == 0)
				{
					MessageBox.Show("Error No se puede Registrar los Datos. Falta Stock de Productos");
					AdmVenta.rollback(Convert.ToInt32(venta.CodFacturaVenta), 0);
					return;
				}
			}
		}
		MessageBox.Show("Los datos se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		txtNumDoc.Text = venta.CodFacturaVenta.PadLeft(11, '0');
		ltaventa.Add(venta);
		if (ncredito.Count > 0)
		{
			frmCancelarPago form = new frmCancelarPago();
			form.CodNota = venta.CodFacturaVenta;
			form.VentComp = 1;
			form.tipo = 3;
			form.CodCliente = cli.CodCliente;
			form.ShowDialog();
		}
		else
		{
			if (fpago.Dias == 0 && venta.CodTipoTransaccion == 7)
			{
				ingresarpago();
			}
			CodVenta = venta.CodFacturaVenta;
			if (venta.FormaPago != 6)
			{
				btnImprimir.Visible = true;
			}
		}
		if (lista != 3)
		{
			await conex.GeneraDocumento(cli, venta, detalle1, 0);
			firmadigital = conex.LogoEmp;
		}
		fnImprimir();
	}

	private void RecorreDetalleEspecial(int codigo, int codEmpresa)
	{
		detalle.Clear();
		detalle1.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in NuevaTabla.Rows)
		{
			AñadeDetalleEspecial(row, codigo, codEmpresa);
		}
	}

	private void AñadeDetalleEspecial(DataRow row, int codigo, int codEmpresa)
	{
		if (codigo == Convert.ToInt32(row[0]) && codEmpresa == Convert.ToInt32(row[25]))
		{
			clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
			deta.CodProducto = Convert.ToInt32(row[1]);
			deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
			deta.CodAlmacen = Convert.ToInt32(row[22]);
			deta.UnidadIngresada = Convert.ToInt32(row[4]);
			deta.SerieLote = "";
			deta.Cantidad = Convert.ToDecimal(row[6]);
			deta.PrecioUnitario = Convert.ToDecimal(row[7]);
			deta.Subtotal = Convert.ToDecimal(row[8]);
			deta.Descuento1 = Convert.ToDecimal(row[9]);
			deta.MontoDescuento = Convert.ToDecimal(row[12]);
			deta.Igv = Convert.ToDecimal(row[14]);
			deta.Importe = Convert.ToDecimal(row[15]);
			deta.PrecioReal = Convert.ToDecimal(row[17]);
			deta.ValoReal = Convert.ToDecimal(row[16]);
			deta.CodUser = frmLogin.iCodUser;
			deta.CantidadPendiente = Convert.ToDecimal(row[6]);
			deta.Moneda = 1;
			deta.Descripcion = row[3].ToString();
			deta.CodTipoArticulo = Convert.ToInt32(row[20]);
			deta.Tipoimpuesto = row[21].ToString();
			deta.Entregado = rbtnPendiente.Checked;
			deta.TipoUnidad = Convert.ToInt32(row[24]);
			deta.CodDetalleCotizacion = 0;
			deta.CodDetallePedido = 0;
			detalle1.Add(deta);
		}
	}

	private void NuevaCabecera(int codbol)
	{
		try
		{
			ser = AdmSerie.CargaSerieEmpresa(venta.CodEmpresa, doc.CodTipoDocumento);
			if (ser == null)
			{
				MessageBox.Show("Faltan series para algunas empresas, Por favor verifique..!");
				return;
			}
			venta.CodSerie = ser.CodSerie;
			venta.Serie = ser.Serie;
			venta.NumDoc = ser.Numeracion.ToString().PadLeft(8, '0');
			if (chkVentaGratuita.Checked)
			{
				venta.Tipoventa = 4;
			}
			else if (chkVentaDsctoGlobal.Checked)
			{
				venta.Tipoventa = 5;
			}
			detalle.Clear();
			detalle1.Clear();
			decimal bruto = default(decimal);
			decimal Dscto = default(decimal);
			decimal igv = default(decimal);
			decimal valor = default(decimal);
			montogratuitas = default(decimal);
			montoexoneradas = default(decimal);
			montogravadas = default(decimal);
			montoinafectas = default(decimal);
			banderagrabada = false;
			banderaexonerada = false;
			banderainafecta = false;
			if (NuevaTabla.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in NuevaTabla.Rows)
			{
				if (Convert.ToInt32(row[0]) == codbol && venta.CodEmpresa == Convert.ToInt32(row[25]))
				{
					bruto += Convert.ToDecimal(row[15]);
					Dscto += Convert.ToDecimal(row[12]);
					valor += Convert.ToDecimal(row[13]);
					if (Convert.ToString(row[21]) == "21")
					{
						montogratuitas += Convert.ToDecimal(row[7]) * Convert.ToDecimal(row[6]);
					}
					if (Convert.ToString(row[21]) == "10" || Convert.ToString(row[21]) == "11" || Convert.ToString(row[21]) == "12" || Convert.ToString(row[21]) == "13" || Convert.ToString(row[21]) == "14" || Convert.ToString(row[21]) == "15" || Convert.ToString(row[21]) == "16" || Convert.ToString(row[21]) == "17")
					{
						montogravadas += Convert.ToDecimal(row[13]);
						banderagrabada = true;
					}
					if (Convert.ToString(row[21]) == "20")
					{
						montoexoneradas += Convert.ToDecimal(row[7]) * Convert.ToDecimal(row[6]) - Convert.ToDecimal(row[12]);
						banderaexonerada = true;
					}
					if (Convert.ToString(row[21]) == "30" || Convert.ToString(row[21]) == "31" || Convert.ToString(row[21]) == "32" || Convert.ToString(row[21]) == "33" || Convert.ToString(row[21]) == "34" || Convert.ToString(row[21]) == "35" || Convert.ToString(row[21]) == "36")
					{
						montoinafectas += Convert.ToDecimal(row[7]) * Convert.ToDecimal(row[6]) - Convert.ToDecimal(row[12]);
						banderainafecta = true;
					}
				}
			}
			venta.Gratuitas = montogratuitas;
			venta.Exoneradas = montoexoneradas;
			venta.Gravadas = montogravadas;
			venta.Inafectas = montoinafectas;
			if (chkVentaGratuita.Checked)
			{
				venta.Tipoventa = 4;
			}
			else if (chkVentaDsctoGlobal.Checked)
			{
				venta.Tipoventa = 5;
			}
			else if (banderagrabada && !banderaexonerada && !banderainafecta)
			{
				venta.Tipoventa = 1;
			}
			else if (!banderagrabada && banderaexonerada && !banderainafecta)
			{
				venta.Tipoventa = 2;
			}
			else if (!banderagrabada && !banderaexonerada && banderainafecta)
			{
				venta.Tipoventa = 3;
			}
			else if (banderagrabada && banderaexonerada && !banderainafecta)
			{
				venta.Tipoventa = 6;
			}
			else if (banderagrabada && !banderaexonerada && banderainafecta)
			{
				venta.Tipoventa = 7;
			}
			string montodescuento = $"{Dscto:#,##0.00}";
			string montoBruto = $"{bruto:#,##0.00}";
			string montovv = $"{valor:#,##0.00}";
			string montoigv = $"{bruto - Dscto - valor:#,##0.00}";
			string montotal = $"{bruto - Dscto:#,##0.00}";
			venta.MontoBruto = Convert.ToDecimal(montoBruto);
			venta.MontoDscto = Convert.ToDecimal(montodescuento);
			venta.Igv = Convert.ToDecimal(montoigv);
			venta.Total = Convert.ToDecimal(montotal);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void OrdenarTablaTemporal()
	{
		DataTable t1 = new DataTable();
		t1 = new DataTable("Tabla1");
		foreach (DataGridViewColumn column in dgvDetalle.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			t1.Columns.Add(dc);
		}
		foreach (int cod in ListaEmpresa)
		{
			foreach (DataRow row in NuevaTabla.Rows)
			{
				if (Convert.ToInt32(row[25]) == cod)
				{
					DataRow dr = t1.NewRow();
					for (int i = 0; i < NuevaTabla.Columns.Count; i++)
					{
						dr[i] = row[i];
					}
					t1.Rows.Add(dr);
				}
			}
		}
		NuevaTabla.Clear();
		NuevaTabla = t1;
	}

	private void CrearTablaTemporal()
	{
		NuevaTabla = new DataTable("TablaDetalle");
		foreach (DataGridViewColumn column in dgvDetalle.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			NuevaTabla.Columns.Add(dc);
		}
		for (int i = 0; i < dgvDetalle.Rows.Count; i++)
		{
			DataGridViewRow row = dgvDetalle.Rows[i];
			DataRow dr = NuevaTabla.NewRow();
			for (int j = 0; j < dgvDetalle.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			NuevaTabla.Rows.Add(dr);
		}
	}

	private void GeneraListaEnpresas()
	{
		bool bandera = false;
		ListaEmpresa.Clear();
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bandera = false;
			if (ListaEmpresa.Count == 0)
			{
				ListaEmpresa.Add(Convert.ToInt32(row.Cells[CodigoEmpresa.Name].Value));
				continue;
			}
			foreach (int lista in ListaEmpresa)
			{
				if (lista == Convert.ToInt32(row.Cells[CodigoEmpresa.Name].Value))
				{
					bandera = true;
				}
			}
			if (!bandera)
			{
				ListaEmpresa.Add(Convert.ToInt32(row.Cells[CodigoEmpresa.Name].Value));
			}
		}
	}

	private void CreaBoletas(int lista)
	{
		try
		{
			DataTable dt1 = new DataTable();
			decimal bruto = default(decimal);
			decimal Ncantidad = default(decimal);
			decimal Nimporte = default(decimal);
			decimal valorV = default(decimal);
			item = 1;
			foreach (DataGridViewColumn column in dgvDetalle.Columns)
			{
				DataColumn dc = new DataColumn(column.Name.ToString());
				dt1.Columns.Add(dc);
			}
			foreach (DataRow row in NuevaTabla.Rows)
			{
				if (Convert.ToInt32(row[25]) != lista)
				{
					continue;
				}
				bruto += Convert.ToDecimal(row[15]);
				if (bruto <= (decimal)mdi_Menu.MontoTopeBoleta)
				{
					DataRow dr = dt1.NewRow();
					for (int i = 0; i < NuevaTabla.Columns.Count; i++)
					{
						if (i == 0)
						{
							dr[i] = item;
						}
						else
						{
							dr[i] = row[i];
						}
					}
					dt1.Rows.Add(dr);
					continue;
				}
				bruto -= Convert.ToDecimal(row[15]);
				decimal ValorRestante = (decimal)mdi_Menu.MontoTopeBoleta - bruto;
				decimal cantidad = Math.Truncate(ValorRestante / Convert.ToDecimal(row[7]));
				decimal cantidadRestante = Convert.ToDecimal(row[6]) - cantidad;
				DataRow dr2 = dt1.NewRow();
				valorV = Convert.ToDecimal(row[7]) * (1m - Convert.ToDecimal(frmLogin.Configuracion.IGV) / 100m);
				bruto += Convert.ToDecimal(row[7]) * cantidad;
				for (int j = 0; j < NuevaTabla.Columns.Count; j++)
				{
					switch (j)
					{
					case 0:
						dr2[j] = item;
						break;
					case 6:
						dr2[j] = cantidad;
						break;
					case 8:
						dr2[j] = cantidad * Convert.ToDecimal(dr2[7]);
						break;
					case 13:
						dr2[j] = Math.Round(cantidad * valorV, 4);
						break;
					case 14:
						dr2[j] = cantidad * Convert.ToDecimal(dr2[7]) - Convert.ToDecimal(dr2[13]);
						break;
					case 15:
						dr2[j] = cantidad * Convert.ToDecimal(dr2[7]);
						break;
					default:
						dr2[j] = row[j];
						break;
					}
				}
				item++;
				bruto = default(decimal);
				dt1.Rows.Add(dr2);
				if (!(cantidadRestante > 0m))
				{
					continue;
				}
				decimal NuevaCantidad = default(decimal);
				decimal ultimacantida = default(decimal);
				int filas = 0;
				if (cantidadRestante * Convert.ToDecimal(dr2[7]) > (decimal)mdi_Menu.MontoTopeBoleta)
				{
					NuevaCantidad = Math.Truncate((decimal)mdi_Menu.MontoTopeBoleta / Convert.ToDecimal(row[7]));
					filas = Convert.ToInt32(Math.Truncate(cantidadRestante / NuevaCantidad));
					if (cantidadRestante > NuevaCantidad * (decimal)filas)
					{
						ultimacantida = cantidadRestante - NuevaCantidad * (decimal)filas;
						filas++;
					}
					else
					{
						ultimacantida = NuevaCantidad;
					}
				}
				for (int k = 1; k <= filas; k++)
				{
					DataRow dr3 = dt1.NewRow();
					if (k == filas)
					{
						NuevaCantidad = ultimacantida;
					}
					bruto += NuevaCantidad * Convert.ToDecimal(dr2[7]);
					for (int l = 0; l < NuevaTabla.Columns.Count; l++)
					{
						switch (l)
						{
						case 0:
							dr3[l] = item;
							break;
						case 6:
							dr3[l] = NuevaCantidad;
							break;
						case 8:
							dr3[l] = NuevaCantidad * Convert.ToDecimal(dr2[7]);
							break;
						case 13:
							dr3[l] = NuevaCantidad * valorV;
							break;
						case 14:
							dr3[l] = NuevaCantidad * Convert.ToDecimal(dr2[7]) - Convert.ToDecimal(dr3[13]);
							break;
						case 15:
							dr3[l] = NuevaCantidad * Convert.ToDecimal(dr2[7]);
							break;
						default:
							dr3[l] = row[l];
							break;
						}
					}
					dt1.Rows.Add(dr3);
					if (k != filas)
					{
						item++;
						bruto = default(decimal);
					}
				}
			}
			NuevaTabla.Clear();
			NuevaTabla = dt1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void ArmaCabecera(int CodigoE, int VTickeckt)
	{
		try
		{
			if (VTickeckt == 1 || chkTicket.Checked)
			{
				doc.CodTipoDocumento = 7;
				venta.CodTipoDocumento = 7;
			}
			else
			{
				doc.CodTipoDocumento = CodDocumento;
				venta.CodTipoDocumento = CodDocumento;
			}
			ser = AdmSerie.CargaSerieEmpresa(CodigoE, doc.CodTipoDocumento);
			if (ser == null)
			{
				MessageBox.Show("Registre Serie y Correlativos para las Empresas");
				return;
			}
			venta.CodSerie = ser.CodSerie;
			venta.Serie = ser.Serie;
			venta.NumDoc = ser.Numeracion.ToString().PadLeft(8, '0');
			if (chkVentaGratuita.Checked)
			{
				venta.Tipoventa = 4;
			}
			else if (chkVentaDsctoGlobal.Checked)
			{
				venta.Tipoventa = 5;
			}
			detalle.Clear();
			detalle1.Clear();
			decimal bruto = default(decimal);
			decimal Dscto = default(decimal);
			decimal igv = default(decimal);
			decimal valor = default(decimal);
			montogratuitas = default(decimal);
			montoexoneradas = default(decimal);
			montogravadas = default(decimal);
			montoinafectas = default(decimal);
			banderagrabada = false;
			banderaexonerada = false;
			banderainafecta = false;
			if (dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					if ((CodigoE == Convert.ToInt32(row.Cells[CodigoEmpresa.Name].Value) && VTickeckt == Convert.ToInt32(row.Cells[venta_tickeck.Name].Value)) || chkTicket.Checked)
					{
						bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
						Dscto += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
						valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
						if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "21")
						{
							montogratuitas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
						}
						if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "10" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "11" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "12" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "13" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "14" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "15" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "16" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "17")
						{
							montogravadas += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
							banderagrabada = true;
						}
						if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "20")
						{
							montoexoneradas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value) - Convert.ToDecimal(row.Cells[montodscto.Name].Value);
							banderaexonerada = true;
						}
						if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "30" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "31" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "32" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "33" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "34" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "35" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "36")
						{
							montoinafectas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value) - Convert.ToDecimal(row.Cells[montodscto.Name].Value);
							banderainafecta = true;
						}
					}
				}
				venta.Gratuitas = montogratuitas;
				venta.Exoneradas = montoexoneradas;
				venta.Gravadas = montogravadas;
				venta.Inafectas = montoinafectas;
				if (chkVentaGratuita.Checked)
				{
					venta.Tipoventa = 4;
				}
				else if (chkVentaDsctoGlobal.Checked)
				{
					venta.Tipoventa = 5;
				}
				else if (banderagrabada && !banderaexonerada && !banderainafecta)
				{
					venta.Tipoventa = 1;
				}
				else if (!banderagrabada && banderaexonerada && !banderainafecta)
				{
					venta.Tipoventa = 2;
				}
				else if (!banderagrabada && !banderaexonerada && banderainafecta)
				{
					venta.Tipoventa = 3;
				}
				else if (banderagrabada && banderaexonerada && !banderainafecta)
				{
					venta.Tipoventa = 6;
				}
				else if (banderagrabada && !banderaexonerada && banderainafecta)
				{
					venta.Tipoventa = 7;
				}
				string montodescuento = $"{Dscto:#,##0.00}";
				string montoBruto = $"{bruto:#,##0.00}";
				string montovv = $"{valor:#,##0.00}";
				string montoigv = $"{bruto - Dscto - valor:#,##0.00}";
				string montotal = $"{bruto - Dscto:#,##0.00}";
				venta.MontoBruto = Convert.ToDecimal(montoBruto);
				venta.MontoDscto = Convert.ToDecimal(montodescuento);
				venta.Igv = Convert.ToDecimal(montoigv);
				venta.Total = Convert.ToDecimal(montotal);
			}
			ActivaCabecera = 1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void VerificaDocumentosPorEmpresa(int codigoE)
	{
		try
		{
			bool bandera = false;
			ListaCantDoc.Clear();
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				bandera = false;
				if (ListaCantDoc.Count == 0)
				{
					ListaCantDoc.Add(Convert.ToInt32(row.Cells[venta_tickeck.Name].Value));
					continue;
				}
				foreach (int lista in ListaCantDoc)
				{
					if (lista == Convert.ToInt32(row.Cells[venta_tickeck.Name].Value))
					{
						bandera = true;
					}
				}
				if (!bandera)
				{
					ListaCantDoc.Add(Convert.ToInt32(row.Cells[venta_tickeck.Name].Value));
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void fnImprimir()
	{
		try
		{
			impresion = AdmVenta.chekeaImpresion(Convert.ToInt32(venta.CodFacturaVenta));
			empress = admempress.CargaEmpresa(venta.CodEmpresa);
			if (impresion == 0)
			{
				if (venta.CodTipoDocumento == 7)
				{
					printaDocumentoxxx();
				}
				else
				{
					PrintaDocumento();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void printaDocumentoxxx()
	{
		try
		{
			if (frmLogin.iCodAlmacen != 0)
			{
				DataSet jes = new DataSet();
				frmRptFactura frm = new frmRptFactura();
				CRFacturaXXX rpt = new CRFacturaXXX();
				rpt.Load("CRNotaCreditoVenta.rpt");
				jes = ds1.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta));
				rpt.SetDataSource(jes);
				PrintOptions rptoption = rpt.PrintOptions;
				rptoption.PrinterName = ser.NombreImpresora;
				rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rptoption.ApplyPageMargins(new PageMargins(0, 0, 0, 0));
				rpt.PrintToPrinter(1, collated: false, 1, 1);
				rpt.Close();
				rpt.Dispose();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void PrintaDocumento()
	{
		try
		{
			if (frmLogin.iCodAlmacen == 0)
			{
				return;
			}
			DataSet jes = new DataSet();
			frmRptFactura frm = new frmRptFactura();
			CRFacturaFomatoContinuo rpt = new CRFacturaFomatoContinuo();
			rpt.Load("CRNotaCreditoVenta.rpt");
			jes = ds1.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta));
			foreach (DataTable mel in jes.Tables)
			{
				foreach (DataRow changesRow in mel.Rows)
				{
					changesRow["firma"] = firmadigital;
				}
				if (!mel.HasErrors)
				{
					continue;
				}
				foreach (DataRow changesRow2 in mel.Rows)
				{
					if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
					{
						changesRow2.RejectChanges();
						changesRow2.ClearErrors();
					}
				}
			}
			rpt.SetDataSource(jes);
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(50, 5, 0, 10));
			rpt.PrintToPrinter(1, collated: false, 1, 1);
			rpt.Close();
			rpt.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private bool BuscaSerie2()
	{
		ser = AdmSerie.MuestraSerie(CodSerie, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void ingresarpago()
	{
		VerificaSaldoCaja();
		if (CodigoCaja != 0)
		{
			Pag.CodNota = venta.CodFacturaVenta.ToString();
			Pag.CodLetra = 0;
			Pag.CodTipoPago = 5;
			Pag.CodMoneda = venta.Moneda;
			Pag.CodCobrador = Convert.ToInt32(frmLogin.iCodUser);
			Pag.Tipo = true;
			Pag.IngresoEgreso = true;
			Pag.TipoCambio = Convert.ToDecimal(venta.TipoCambio);
			Pag.MontoPagado = Convert.ToDecimal(venta.Total);
			Pag.MontoCobrado = Convert.ToDecimal(venta.Total);
			Pag.Vuelto = 0m;
			Pag.codCtaCte = 0;
			Pag.CtaCte = "";
			Pag.NOperacion = "";
			Pag.NCheque = "";
			Pag.FechaPago = venta.FechaPago.Date;
			Pag.Observacion = "";
			Pag.CodUser = frmLogin.iCodUser;
			Pag.CodAlmacen = frmLogin.iCodAlmacen;
			Pag.CodSucursal = frmLogin.iCodSucursal;
			Pag.CodDoc = venta.CodTipoDocumento;
			Pag.Serie = "";
			Pag.NumDoc = "";
			Pag.Referencia = "";
			Pag.Codcaja = CodigoCaja;
			Pag.CodBanco = 0;
			Pag.CodTarjeta = 0;
			Pag.Aprobado = 4;
			if (!AdmPagos.insert(Pag))
			{
			}
		}
	}

	private void VerificaSaldoCaja()
	{
		try
		{
			Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, frmLogin.iCodAlmacen, frmLogin.iCodUser);
			if (Caja == null)
			{
				MessageBox.Show("Aperture caja, el pago no se a registrado", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CodigoCaja = Caja.Codcaja;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public bool VerificarDetracciones()
	{
		bool grav = false;
		decimal sumadet = default(decimal);
		if (CodDocumento == 2)
		{
			if (dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					if (Convert.ToDecimal(row.Cells[igv.Name].Value) != 0m)
					{
						grav = true;
					}
					else if (Convert.ToDecimal(row.Cells[precioventa.Name].Value) == 0m)
					{
						grav = true;
					}
					else
					{
						sumadet += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
					}
				}
			}
			if (sumadet <= 700m)
			{
				return true;
			}
			if (grav)
			{
				MessageBox.Show("Operacion no permitida, por estar afecta a detracción!", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			return true;
		}
		return true;
	}

	private void ValidaCliente(string rucdni)
	{
		cli = AdmCli.ConsultaCliente(rucdni);
		if (cli == null)
		{
			cli = new clsCliente();
			int id = AdmCli.GetUltimoId() + 1;
			if (rucdni.Length == 8)
			{
				cli.Dni = rucdni;
				cli.DireccionEntrega = "S/D";
				cli.DireccionLegal = "S/D";
			}
			else if (rucdni.Length == 11)
			{
				cli.Ruc = rucdni;
				cli.DireccionEntrega = txtDireccion.Text;
				cli.DireccionLegal = txtDireccion.Text;
			}
			cli.Nombre = txtNombreCliente.Text;
			cli.RazonSocial = txtNombreCliente.Text;
			cli.CodigoPersonalizado = "C" + id.ToString().PadLeft(6, '0').Trim();
			cli.FormaPago = 6;
			cli.Moneda = 1;
			cli.LineaCredito = 0m;
			cli.LineaCreditoDisponible = 0m;
			cli.Habilitado = true;
			cli.CodUser = frmLogin.iCodUser;
			if (AdmCli.insert(cli))
			{
				CodCliente = cli.CodClienteNuevo;
			}
		}
	}

	private void CargaPedido()
	{
		try
		{
			pedido = AdmPedido.CargaPedido(Convert.ToInt32(CodPedido));
			if (pedido != null)
			{
				txtCodigoBarras.Text = pedido.CodigoBarrasCifrado;
				facturarsinmoverstock = pedido.ventasinstock;
				if (pedido.Boletafactura == 1)
				{
					if (admParametro.consultarParametroVenta(1) && admParametro.consultarParametroVenta(2))
					{
						chkBoleta.Checked = false;
						chkFactura.Checked = false;
						chkTicket.Checked = false;
					}
					else
					{
						chkBoleta.Checked = true;
						chkFactura.Checked = false;
					}
					CodCliente = pedido.CodCliente;
					txtCodCliente.Text = pedido.DNI;
					if (pedido.Nombrecliente != "")
					{
						txtNombreCliente.Text = pedido.Nombrecliente;
					}
					else
					{
						txtNombreCliente.Text = pedido.Nombre;
					}
					txtDireccion.Text = pedido.Direccion;
					cargadatoscliente(pedido.DNI);
				}
				else
				{
					if (admParametro.consultarParametroVenta(1) && admParametro.consultarParametroVenta(2))
					{
						chkBoleta.Checked = false;
						chkFactura.Checked = false;
						chkTicket.Checked = false;
					}
					else
					{
						chkBoleta.Checked = false;
						chkFactura.Checked = true;
					}
					CodCliente = pedido.CodCliente;
					cli.CodCliente = CodCliente;
					txtCodCliente.Text = pedido.RUCCliente;
					if (pedido.RazonSocialCliente != "")
					{
						txtNombreCliente.Text = pedido.RazonSocialCliente;
					}
					else
					{
						txtNombreCliente.Text = pedido.Nombre;
					}
					txtDireccion.Text = pedido.Direccion;
					cargadatoscliente(pedido.RUCCliente);
				}
				dtpFecha.Value = pedido.FechaPedido;
				txtBruto.Text = $"{pedido.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{pedido.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{pedido.Total - pedido.Igv:#,##0.00}";
				txtIGV.Text = $"{pedido.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{pedido.Total:#,##0.00}";
				montogravadas = pedido.Gravadas;
				montoexoneradas = pedido.Exoneradas;
				montoinafectas = pedido.Inafectas;
				montogratuitas = pedido.Gratuitas;
				label11.Text = txtPrecioVenta.Text;
				cbovendedor.SelectedValue = pedido.CodVendedor;
				montosventa();
				if (pedido.Tipoventa == 4)
				{
					chkVentaGratuita.Checked = true;
				}
				else if (pedido.Tipoventa == 5)
				{
					chkVentaDsctoGlobal.Checked = true;
				}
				CargaDetallePedido(Convert.ToInt32(pedido.CodPedido));
				PedidosIngresados.Add(pedido);
				lblCantidadProductos.Text = "Productos Agregados : " + dgvDetalle.RowCount;
				cmbFormaPago.SelectedValue = pedido.FormaPago;
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

	private void CargaDetallePedido(int CodPedido)
	{
		DataTable newData = new DataTable();
		dgvDetalle.Rows.Clear();
		try
		{
			newData = AdmPedido.CargaDetalle(CodPedido);
			foreach (DataRow row in newData.Rows)
			{
				if (Convert.ToInt32(row[26].ToString()) == 1 || Convert.ToInt32(row[26].ToString()) == 0)
				{
					dgvDetalle.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString(), row[26].ToString(), row[27].ToString(), row[28].ToString(), row[29].ToString(), row[30].ToString(), row[31].ToString(), row[32].ToString(), row[33].ToString(), row[34].ToString(), row[37].ToString(), row[38].ToString());
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void RecorreDetalle(int codigo, int VTicket)
	{
		detalle.Clear();
		detalle1.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalle(row, codigo, VTicket);
		}
	}

	private void añadedetalle(DataGridViewRow fila, int codEmp, int ticket)
	{
		if ((codEmp == Convert.ToInt32(fila.Cells[CodigoEmpresa.Name].Value) && ticket == Convert.ToInt32(fila.Cells[venta_tickeck.Name].Value)) || chkTicket.Checked)
		{
			clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
			deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
			deta.CodAlmacen = frmLogin.iCodAlmacen;
			deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			deta.SerieLote = "";
			deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
			deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
			deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
			deta.MontoDescuento = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
			deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
			deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
			deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
			deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
			deta.CodUser = frmLogin.iCodUser;
			deta.CantidadPendiente = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.Moneda = 1;
			deta.Descripcion = fila.Cells[descripcion.Name].Value.ToString();
			deta.Tipoimpuesto = fila.Cells[Tipoimpuesto.Name].Value.ToString();
			deta.Entregado = rbtnPendiente.Checked;
			deta.TipoUnidad = Convert.ToInt32(fila.Cells[TipoUnidad.Name].Value);
			deta.CodDetalleCotizacion = 0;
			deta.ProductoVentaTicket = Convert.ToInt32(fila.Cells[venta_tickeck.Name].Value);
			deta.CodigoProductoSunat = fila.Cells[codigoproductosunat.Name].Value.ToString();
			deta.CodDetallePedido = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
			deta.CodFamilia = Convert.ToInt32(fila.Cells[codFamilia.Name].Value);
			deta.SerieMotor = fila.Cells[serie_motor.Name].Value.ToString();
			deta.NroChasis = fila.Cells[nrochasis.Name].Value.ToString();
			deta.Modelo = fila.Cells[modelo.Name].Value.ToString();
			deta.Marca = fila.Cells[Marca.Name].Value.ToString();
			deta.Color = fila.Cells[color.Name].Value.ToString();
			deta.codlinea = Convert.ToInt32(fila.Cells[codli.Name].Value);
			deta.codfamilia = Convert.ToInt32(fila.Cells[codfa.Name].Value);
			detalle1.Add(deta);
		}
	}

	public bool verificarPrecioVenta()
	{
		bool valor = false;
		if (Convert.ToDecimal(txtPrecioVenta.Text) >= 700m && CodCliente == 1)
		{
			MessageBox.Show("Precio venta > S/. 700, registrar(DNI, datos completos del cliente) o seleccionar cliente para guardar pedido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			if (Application.OpenForms["frmClientesLista"] != null)
			{
				Application.OpenForms["frmClientesLista"].Activate();
			}
			else
			{
				frmClientesLista form = new frmClientesLista();
				form.Proceso = 3;
				form.ShowDialog();
				txtCodCliente.Text = "";
				txtDireccion.Text = "";
				txtNombreCliente.Text = "";
				if (form.exit)
				{
					cli = form.cli;
					CodCliente = cli.CodCliente;
					if (CodCliente != 0)
					{
						NombreCliente = cli.Nombre;
						CargaCliente();
						valor = true;
						ProcessTabKey(forward: true);
					}
				}
				else
				{
					txtCodCliente.Focus();
					valor = false;
				}
			}
		}
		else
		{
			valor = true;
		}
		return valor;
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

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator7_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void chkBoleta_Click(object sender, EventArgs e)
	{
		if (!valdoc() || !admParametro.consultarParametroVenta(2))
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[venta_tickeck.Name].Value = 0;
		}
	}

	public bool valdoc()
	{
		if (txtCodCliente.Text.Length == 8)
		{
			chkBoleta.Checked = true;
			chkFactura.Checked = false;
			return true;
		}
		if (txtCodCliente.Text.Length == 11)
		{
			chkBoleta.Checked = false;
			chkFactura.Checked = true;
			return true;
		}
		return false;
	}

	private void chkFactura_Click(object sender, EventArgs e)
	{
		if (!valdoc() || !admParametro.consultarParametroVenta(2))
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[venta_tickeck.Name].Value = 0;
		}
	}

	private void frmGeneraVenta_Load(object sender, EventArgs e)
	{
		CargaVendedores();
		CargaFormaPagos();
		if (Proceso == 4)
		{
			CargaPedido();
			CargaDocumentoIdentidad();
			btnInicioOV.Focus();
			btnInicioOV.Select();
		}
		else if (Proceso == 3)
		{
			CargaVenta();
			sololectura(estado: true);
			groupBox3.Visible = false;
			btnImprimir.Visible = true;
			button1.Visible = true;
		}
	}

	private void CargaVendedores()
	{
		cbovendedor.DataSource = AdmUs.CargaUsuarios();
		cbovendedor.DisplayMember = "vendedor";
		cbovendedor.ValueMember = "codUsuario";
		cbovendedor.SelectedIndex = -1;
	}

	private void CargaDocumentoIdentidad()
	{
		int codigoTipoDocumento = ((pedido.Boletafactura == 1) ? 1 : 2);
		cmbDocumentoIdentidad.DataSource = AdmDocumentoIdentidad.ListaDocumentoIdentidad(codigoTipoDocumento);
		cmbDocumentoIdentidad.DisplayMember = "descripcion";
		cmbDocumentoIdentidad.ValueMember = "codDocumentoIdentidad";
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void chkTicket_Click(object sender, EventArgs e)
	{
		chkTicket.Checked = true;
		chkBoleta.Checked = false;
		chkFactura.Checked = false;
		if (!admParametro.consultarParametroVenta(1) || !admParametro.consultarParametroVenta(2))
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[venta_tickeck.Name].Value = 1;
		}
	}

	private void CargaVenta()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodVenta));
			ser = AdmSerie.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
			if (venta != null)
			{
				txtNumDoc.Text = venta.CodFacturaVenta;
				CodTransaccion = venta.CodTipoTransaccion;
				CargaTransaccion();
				if (txtCodCliente.Enabled)
				{
					CodCliente = venta.CodCliente;
					cli = AdmCli.MuestraCliente(CodCliente);
					txtCodCliente.Text = venta.DNI;
					txtNombreCliente.Text = venta.RazonSocialCliente;
					txtDireccion.Text = venta.Direccion;
					txtLineaCredito.Text = cli.LineaCredito.ToString();
					txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
					txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
				}
				dtpFecha.Value = venta.FechaSalida;
				CodDocumento = venta.CodTipoDocumento;
				txtDocRef.Text = venta.SiglaDocumento;
				txtSerie.Text = venta.Serie;
				if (Procede != 4)
				{
					txtPedido.Text = venta.NumDoc;
				}
				if (cbovendedor.Enabled && venta.CodVendedor != 0)
				{
					cbovendedor.SelectedValue = venta.CodVendedor;
				}
				cmbFormaPago.SelectedValue = venta.FormaPago;
				cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null);
				dtpFechaPago.Value = venta.FechaPago;
				txtComentario.Text = venta.Comentario;
				txtBruto.Text = $"{venta.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{venta.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{venta.Total - venta.Igv:#,##0.00}";
				txtIGV.Text = $"{venta.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{venta.Total:#,##0.00}";
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmVenta.CargaDetalle(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen, 0);
		btnImprimir.Visible = true;
		btnImprimir.Focus();
		btnImprimir.Select();
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

	private void sololectura(bool estado)
	{
		dtpFecha.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		txtDocRef.ReadOnly = estado;
		txtPedido.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnInicioOV.Visible = !estado;
		btnAddOV.Visible = !estado;
		btnDeleteItem.Visible = !estado;
		btnGuardaVenta.Visible = !estado;
		btnSalir.Visible = estado;
		btnSalir.Enabled = estado;
		groupBox4.Enabled = true;
	}

	private void btnInicioOV_Click(object sender, EventArgs e)
	{
		activaPaneles(estado: true);
		txtSerie.Focus();
		txtSerie.SelectAll();
	}

	private void cargadatoscliente(string rucdni)
	{
		if (rucdni.Length == 11)
		{
			txtDocRef.Text = "FT";
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			txtDocRef_KeyPress(txtDocRef, ee);
			txtSerie_KeyPress(txtDocRef, ee);
		}
		else if (rucdni.Length == 8)
		{
			txtDocRef.Text = "BV";
			KeyPressEventArgs ee2 = new KeyPressEventArgs('\r');
			txtDocRef_KeyPress(txtDocRef, ee2);
			txtSerie_KeyPress(txtDocRef, ee2);
		}
		cli = AdmCli.ConsultaCliente(rucdni);
		cli.RucDni = rucdni;
		cli = AdmCli.MuestraCliente(CodCliente);
		if (cli.Moneda == 1)
		{
			txtLineaCredito.Text = $"{cli.LineaCredito:#,##0.00}";
			txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible:#,##0.00}";
			txtLineaCreditoUso.Text = $"{cli.LineaCreditoUsado:#,##0.00}";
			txttasa.Text = $"{cli.Tasa:#,##0.00}";
			lbLineaCredito.Text = "Línea de Crédito (S/.):";
			label23.Text = "Línea Disponible (S/.):";
			label25.Text = "Línea C. en Uso (S/.):";
			if (cli.LineaCredito > 0m)
			{
				cmbFormaPago.Enabled = true;
			}
			else
			{
				cmbFormaPago.Enabled = false;
			}
		}
		else
		{
			txtLineaCredito.Text = $"{cli.LineaCredito / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
			txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
			TextBox textBox = txtLineaCreditoUso;
			string text = (Text = $"{cli.LineaCreditoUsado / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}");
			textBox.Text = text;
			lbLineaCredito.Text = "Línea de Crédito ($.):";
			label23.Text = "Línea Disponible ($.):";
			label25.Text = "Línea C. en Uso ($.):";
			if (cli.LineaCredito > 0m)
			{
				cmbFormaPago.Enabled = true;
			}
			else
			{
				cmbFormaPago.Enabled = false;
			}
		}
		if (cli.FormaPago != 0)
		{
			if (Proceso == 4)
			{
				cmbFormaPago.SelectedValue = pedido.FormaPago;
			}
			else
			{
				cmbFormaPago.SelectedValue = cli.FormaPago;
			}
			EventArgs ee3 = new EventArgs();
			cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee3);
		}
		else
		{
			dtpFechaPago.Value = DateTime.Today;
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
				txtPedido.Visible = true;
				txtPedido.Enabled = false;
				txtPedido.Focus();
				txtPedido.Text = "";
			}
			else
			{
				txtPedido.Text = "";
				txtPedido.Enabled = false;
				txtPedido.Text = ser.Numeracion.ToString().PadLeft(8, '0');
				txtPedido.Visible = false;
			}
		}
		if (e.KeyChar == '\r')
		{
			cmbFormaPago.Focus();
		}
	}

	private bool BuscaSerie()
	{
		ser = AdmSerie.BuscaSeriexDocumento(CodDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtTransaccion.Text != "")
		{
			if (BuscaTransaccion())
			{
				btnInicioOV.Focus();
				btnInicioOV.Select();
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

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			if (frmLogin.iCodAlmacen == 0)
			{
				return;
			}
			DataSet jes = new DataSet();
			frmRptFactura frm = new frmRptFactura();
			CRFacturaFomatoContinuo rpt = new CRFacturaFomatoContinuo();
			string nombrearchivo = "";
			venta = AdmVenta.BuscaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen);
			if (venta.CodTipoDocumento == 1)
			{
				nombrearchivo = frmLogin.RUC + "-03-B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
			}
			else if (venta.CodTipoDocumento == 2)
			{
				nombrearchivo = frmLogin.RUC + "-01-F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
			}
			if (venta.CodTipoDocumento == 7)
			{
				printaDocumentoxxx();
				return;
			}
			firmadigital = CargarImagen("C:\\DOCUMENTOS-" + frmLogin.RUC + "\\CERTIFIK\\QR\\" + nombrearchivo + ".jpeg");
			rpt.Load("CRFacturaFomatoContinuo.rpt");
			jes = ds1.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta));
			foreach (DataTable mel in jes.Tables)
			{
				foreach (DataRow changesRow in mel.Rows)
				{
					changesRow["firma"] = firmadigital;
				}
				if (!mel.HasErrors)
				{
					continue;
				}
				foreach (DataRow changesRow2 in mel.Rows)
				{
					if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
					{
						changesRow2.RejectChanges();
						changesRow2.ClearErrors();
					}
				}
			}
			rpt.SetDataSource(jes);
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(50, 5, 0, 0));
			rpt.PrintToPrinter(1, collated: false, 1, 1);
			rpt.Close();
			rpt.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public static byte[] CargarImagen(string rutaArchivo)
	{
		if (rutaArchivo != "")
		{
			try
			{
				FileStream Archivo = new FileStream(rutaArchivo, FileMode.Open);
				BinaryReader binRead = new BinaryReader(Archivo);
				byte[] imagenEnBytes = new byte[Archivo.Length];
				binRead.Read(imagenEnBytes, 0, (int)Archivo.Length);
				binRead.Close();
				Archivo.Close();
				return imagenEnBytes;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return new byte[0];
			}
		}
		return new byte[0];
	}

	private void btnAddOV_Click(object sender, EventArgs e)
	{
		if (!bandera)
		{
			return;
		}
		if (!string.IsNullOrEmpty(txtAddOV.Text))
		{
			string CodPedido = txtAddOV.Text;
			clsPedido PedidoObtenido = AdmPedido.CargaPedido(Convert.ToInt32(CodPedido));
			pedido = PedidoObtenido;
			if (PedidoObtenido != null)
			{
				txtAddOV.Text = string.Empty;
				if (PedidoObtenido.Pendiente == 1)
				{
					clsCliente ClienteDeVenta = AdmCli.ConsultaCliente(txtCodCliente.Text);
					if (PedidoObtenido.CodCliente == ClienteDeVenta.CodCliente)
					{
						if (PedidosIngresados.Count > 0)
						{
							clsPedido PedidoEncontrado = Enumerable.Where<clsPedido>((IEnumerable<clsPedido>)PedidosIngresados, (Func<clsPedido, bool>)((clsPedido clsPedido2) => clsPedido2.CodPedido.Equals(PedidoObtenido.CodPedido))).SingleOrDefault();
							if (PedidoEncontrado != null)
							{
								PedidosIngresados.Remove(PedidoEncontrado);
							}
						}
						PedidosIngresados.Add(PedidoObtenido);
						foreach (clsPedido ped in PedidosIngresados)
						{
							montogravadas += ped.Gravadas;
							montoexoneradas += ped.Exoneradas;
							montoinafectas += ped.Inafectas;
							montogratuitas += ped.Gratuitas;
							montosventa();
							if (ped.Tipoventa == 4)
							{
								chkVentaGratuita.Checked = true;
							}
							else if (ped.Tipoventa == 5)
							{
								chkVentaDsctoGlobal.Checked = true;
							}
							CargaDetallePedido(Convert.ToInt32(ped.CodPedido));
						}
						calculatotales();
						MessageBox.Show("PEDIDO AGREGADO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						MessageBox.Show("ESTE PEDIDO NO CORRESPONDE A ESTE USUARIO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("ESTE PEDIDO DE VENTA YA HA SIDO FACTURADO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					txtAddOV.Text = string.Empty;
				}
			}
			else
			{
				MessageBox.Show("EL PEDIDO INGRESADO NO EXISTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("INGRESE EL CODIGO DEL PEDIDO DE VENTA", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			txtAddOV.Focus();
		}
	}

	private void txtCodCliente_KeyUp(object sender, KeyEventArgs e)
	{
		try
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
				txtCodCliente.Text = "";
				txtDireccion.Text = "";
				txtNombreCliente.Text = "";
				NombreCliente = cli.Nombre;
				CargaCliente();
				LimpiarDataGridView(dgvDetalle);
				PedidosIngresados = new List<clsPedido>();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void LimpiarDataGridView(DataGridView DGVALimpiar)
	{
		if (DGVALimpiar.DataSource != null)
		{
			DGVALimpiar.DataSource = null;
		}
		else
		{
			DGVALimpiar.Rows.Clear();
		}
	}

	private void btnInicioOV_Click_1(object sender, EventArgs e)
	{
		activaPaneles(estado: true);
	}

	private void btnDeleteItem_Click_1(object sender, EventArgs e)
	{
		btnDeleteItem_Click(sender, e);
	}

	private void txtAddOV_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGeneraVenta));
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtDelOV = new System.Windows.Forms.TextBox();
		this.txtAddOV = new System.Windows.Forms.TextBox();
		this.btnDeleteItem = new System.Windows.Forms.Button();
		this.btnAddOV = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.chkTicket = new System.Windows.Forms.RadioButton();
		this.chkFactura = new System.Windows.Forms.RadioButton();
		this.chkBoleta = new System.Windows.Forms.RadioButton();
		this.chkVentaGratuita = new System.Windows.Forms.CheckBox();
		this.chkVentaDsctoGlobal = new System.Windows.Forms.CheckBox();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.cmbDocumentoIdentidad = new DevComponents.DotNetBar.Controls.ComboBoxEx();
		this.lblCantidadProductos = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.rbtnPendiente = new System.Windows.Forms.RadioButton();
		this.label24 = new System.Windows.Forms.Label();
		this.cbovendedor = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.txttasa = new System.Windows.Forms.TextBox();
		this.label30 = new System.Windows.Forms.Label();
		this.lbLineaCredito = new System.Windows.Forms.Label();
		this.txtLineaCredito = new System.Windows.Forms.TextBox();
		this.txtLineaCreditoUso = new System.Windows.Forms.TextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.txtLineaCreditoDisponible = new System.Windows.Forms.TextBox();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.txtSunat_Capchat = new System.Windows.Forms.TextBox();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtPDescuento = new System.Windows.Forms.TextBox();
		this.pbCapchatS = new System.Windows.Forms.PictureBox();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.txtinafectas = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.txtexoneradas = new System.Windows.Forms.TextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.txtgratuitas = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.txtgravadas = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.txtPrecioVenta = new System.Windows.Forms.TextBox();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label11 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtPedido = new System.Windows.Forms.TextBox();
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.txtCodigoBarras = new System.Windows.Forms.TextBox();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.chkVentaEspecial = new System.Windows.Forms.CheckBox();
		this.button1 = new System.Windows.Forms.Button();
		this.btnInicioOV = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardaVenta = new System.Windows.Forms.Button();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioconigv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Tipoarticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Tipoimpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TipoUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodigoEmpresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie_motor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nrochasis = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.venta_tickeck = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigoproductosunat = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codli = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codfa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.panel1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).BeginInit();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvDetalle);
		this.groupBox1.Location = new System.Drawing.Point(-1, 47);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(752, 241);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle Venta";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.valoreal, this.precioreal, this.precioconigv, this.valorpromedio, this.Tipoarticulo, this.Tipoimpuesto, this.codalmacen, this.almacen, this.TipoUnidad, this.CodigoEmpresa, this.descripcion_venta, this.serie_motor, this.nrochasis, this.modelo, this.Marca, this.color, this.codFamilia, this.venta_tickeck, this.codigoproductosunat, this.codli, this.codfa);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(746, 222);
		this.dgvDetalle.TabIndex = 2;
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.txtDelOV);
		this.panel1.Controls.Add(this.txtAddOV);
		this.panel1.Controls.Add(this.btnDeleteItem);
		this.panel1.Controls.Add(this.btnAddOV);
		this.panel1.Controls.Add(this.groupBox2);
		this.panel1.Controls.Add(this.chkVentaGratuita);
		this.panel1.Controls.Add(this.chkVentaDsctoGlobal);
		this.panel1.Location = new System.Drawing.Point(753, 63);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(229, 135);
		this.panel1.TabIndex = 2;
		this.txtDelOV.Location = new System.Drawing.Point(11, 41);
		this.txtDelOV.Name = "txtDelOV";
		this.txtDelOV.Size = new System.Drawing.Size(109, 20);
		this.txtDelOV.TabIndex = 131;
		this.txtDelOV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtAddOV.Location = new System.Drawing.Point(11, 13);
		this.txtAddOV.Name = "txtAddOV";
		this.txtAddOV.Size = new System.Drawing.Size(109, 20);
		this.txtAddOV.TabIndex = 130;
		this.txtAddOV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtAddOV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtAddOV_KeyPress);
		this.btnDeleteItem.Enabled = false;
		this.btnDeleteItem.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnDeleteItem.FlatAppearance.BorderSize = 2;
		this.btnDeleteItem.Location = new System.Drawing.Point(127, 41);
		this.btnDeleteItem.Name = "btnDeleteItem";
		this.btnDeleteItem.Size = new System.Drawing.Size(94, 20);
		this.btnDeleteItem.TabIndex = 129;
		this.btnDeleteItem.Text = "F2 - DEL OV";
		this.btnDeleteItem.UseVisualStyleBackColor = true;
		this.btnDeleteItem.Click += new System.EventHandler(btnDeleteItem_Click_1);
		this.btnAddOV.Enabled = false;
		this.btnAddOV.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnAddOV.FlatAppearance.BorderSize = 2;
		this.btnAddOV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
		this.btnAddOV.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
		this.btnAddOV.Location = new System.Drawing.Point(127, 13);
		this.btnAddOV.Name = "btnAddOV";
		this.btnAddOV.Size = new System.Drawing.Size(94, 20);
		this.btnAddOV.TabIndex = 128;
		this.btnAddOV.Text = "F3 - ADD OV";
		this.btnAddOV.UseVisualStyleBackColor = true;
		this.btnAddOV.Click += new System.EventHandler(btnAddOV_Click);
		this.groupBox2.Controls.Add(this.chkTicket);
		this.groupBox2.Controls.Add(this.chkFactura);
		this.groupBox2.Controls.Add(this.chkBoleta);
		this.groupBox2.Enabled = false;
		this.groupBox2.Location = new System.Drawing.Point(6, 93);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(216, 36);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.chkTicket.AutoSize = true;
		this.chkTicket.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
		this.chkTicket.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkTicket.Location = new System.Drawing.Point(66, 20);
		this.chkTicket.Name = "chkTicket";
		this.chkTicket.Size = new System.Drawing.Size(75, 18);
		this.chkTicket.TabIndex = 2;
		this.chkTicket.Text = "TICKET";
		this.chkTicket.UseVisualStyleBackColor = true;
		this.chkTicket.Visible = false;
		this.chkTicket.Click += new System.EventHandler(chkTicket_Click);
		this.chkFactura.AutoSize = true;
		this.chkFactura.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
		this.chkFactura.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkFactura.Location = new System.Drawing.Point(111, 1);
		this.chkFactura.Name = "chkFactura";
		this.chkFactura.Size = new System.Drawing.Size(88, 18);
		this.chkFactura.TabIndex = 1;
		this.chkFactura.Text = "FACTURA";
		this.chkFactura.UseVisualStyleBackColor = true;
		this.chkFactura.Click += new System.EventHandler(chkFactura_Click);
		this.chkBoleta.AutoSize = true;
		this.chkBoleta.Checked = true;
		this.chkBoleta.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
		this.chkBoleta.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkBoleta.Location = new System.Drawing.Point(24, 1);
		this.chkBoleta.Name = "chkBoleta";
		this.chkBoleta.Size = new System.Drawing.Size(79, 18);
		this.chkBoleta.TabIndex = 0;
		this.chkBoleta.TabStop = true;
		this.chkBoleta.Text = "BOLETA";
		this.chkBoleta.UseVisualStyleBackColor = true;
		this.chkBoleta.Click += new System.EventHandler(chkBoleta_Click);
		this.chkVentaGratuita.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaGratuita.AutoSize = true;
		this.chkVentaGratuita.Location = new System.Drawing.Point(12, 70);
		this.chkVentaGratuita.Name = "chkVentaGratuita";
		this.chkVentaGratuita.Size = new System.Drawing.Size(98, 17);
		this.chkVentaGratuita.TabIndex = 111;
		this.chkVentaGratuita.Text = "Vta. Gratuita";
		this.chkVentaGratuita.UseVisualStyleBackColor = true;
		this.chkVentaDsctoGlobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaDsctoGlobal.AutoSize = true;
		this.chkVentaDsctoGlobal.Location = new System.Drawing.Point(114, 70);
		this.chkVentaDsctoGlobal.Name = "chkVentaDsctoGlobal";
		this.chkVentaDsctoGlobal.Size = new System.Drawing.Size(114, 17);
		this.chkVentaDsctoGlobal.TabIndex = 114;
		this.chkVentaDsctoGlobal.Text = "Vta. Descuento";
		this.chkVentaDsctoGlobal.UseVisualStyleBackColor = true;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "boton-inicio.png");
		this.groupBox4.Controls.Add(this.cmbDocumentoIdentidad);
		this.groupBox4.Controls.Add(this.lblCantidadProductos);
		this.groupBox4.Controls.Add(this.txtNumDoc);
		this.groupBox4.Controls.Add(this.rbtnPendiente);
		this.groupBox4.Controls.Add(this.label24);
		this.groupBox4.Controls.Add(this.cbovendedor);
		this.groupBox4.Controls.Add(this.label14);
		this.groupBox4.Controls.Add(this.txtComentario);
		this.groupBox4.Controls.Add(this.groupBox3);
		this.groupBox4.Controls.Add(this.txtBruto);
		this.groupBox4.Controls.Add(this.txtDscto);
		this.groupBox4.Controls.Add(this.label2);
		this.groupBox4.Controls.Add(this.label13);
		this.groupBox4.Controls.Add(this.dtpFechaPago);
		this.groupBox4.Controls.Add(this.cmbFormaPago);
		this.groupBox4.Controls.Add(this.txtSunat_Capchat);
		this.groupBox4.Controls.Add(this.txtDireccion);
		this.groupBox4.Controls.Add(this.txtNombreCliente);
		this.groupBox4.Controls.Add(this.txtCodCliente);
		this.groupBox4.Controls.Add(this.dtpFecha);
		this.groupBox4.Controls.Add(this.label7);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Controls.Add(this.label5);
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Enabled = false;
		this.groupBox4.Location = new System.Drawing.Point(-1, 294);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(752, 187);
		this.groupBox4.TabIndex = 4;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Datos del Cliente";
		this.cmbDocumentoIdentidad.DisplayMember = "Text";
		this.cmbDocumentoIdentidad.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.cmbDocumentoIdentidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbDocumentoIdentidad.FormattingEnabled = true;
		this.cmbDocumentoIdentidad.ItemHeight = 14;
		this.cmbDocumentoIdentidad.Location = new System.Drawing.Point(304, 21);
		this.cmbDocumentoIdentidad.Name = "cmbDocumentoIdentidad";
		this.cmbDocumentoIdentidad.Size = new System.Drawing.Size(197, 20);
		this.cmbDocumentoIdentidad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.cmbDocumentoIdentidad.TabIndex = 143;
		this.lblCantidadProductos.AutoSize = true;
		this.lblCantidadProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadProductos.Location = new System.Drawing.Point(571, 0);
		this.lblCantidadProductos.Name = "lblCantidadProductos";
		this.lblCantidadProductos.Size = new System.Drawing.Size(175, 16);
		this.lblCantidadProductos.TabIndex = 142;
		this.lblCantidadProductos.Text = "Productos Agregados: 0";
		this.txtNumDoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumDoc.Location = new System.Drawing.Point(131, 23);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(92, 18);
		this.txtNumDoc.TabIndex = 136;
		this.rbtnPendiente.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.rbtnPendiente.AutoSize = true;
		this.rbtnPendiente.ForeColor = System.Drawing.Color.OliveDrab;
		this.rbtnPendiente.Location = new System.Drawing.Point(343, 24);
		this.rbtnPendiente.Name = "rbtnPendiente";
		this.rbtnPendiente.Size = new System.Drawing.Size(161, 17);
		this.rbtnPendiente.TabIndex = 112;
		this.rbtnPendiente.Text = "Pendiente de Despacho";
		this.rbtnPendiente.UseVisualStyleBackColor = true;
		this.rbtnPendiente.Visible = false;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.Location = new System.Drawing.Point(20, 158);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(108, 13);
		this.label24.TabIndex = 135;
		this.label24.Text = "VENDEDOR       :";
		this.cbovendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbovendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbovendedor.FormattingEnabled = true;
		this.cbovendedor.Items.AddRange(new object[1] { "Sin Vendedor" });
		this.cbovendedor.Location = new System.Drawing.Point(130, 155);
		this.cbovendedor.Name = "cbovendedor";
		this.cbovendedor.Size = new System.Drawing.Size(279, 20);
		this.cbovendedor.TabIndex = 134;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(503, 4);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(48, 13);
		this.label14.TabIndex = 133;
		this.label14.Text = "Notas :";
		this.txtComentario.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtComentario.Location = new System.Drawing.Point(506, 21);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(218, 41);
		this.txtComentario.TabIndex = 132;
		this.txtComentario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.BackColor = System.Drawing.SystemColors.ScrollBar;
		this.groupBox3.Controls.Add(this.txttasa);
		this.groupBox3.Controls.Add(this.label30);
		this.groupBox3.Controls.Add(this.lbLineaCredito);
		this.groupBox3.Controls.Add(this.txtLineaCredito);
		this.groupBox3.Controls.Add(this.txtLineaCreditoUso);
		this.groupBox3.Controls.Add(this.label23);
		this.groupBox3.Controls.Add(this.label25);
		this.groupBox3.Controls.Add(this.txtLineaCreditoDisponible);
		this.groupBox3.Enabled = false;
		this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(506, 66);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(232, 109);
		this.groupBox3.TabIndex = 131;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Condiciones de Crédito:";
		this.txttasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txttasa.Location = new System.Drawing.Point(126, 82);
		this.txttasa.Name = "txttasa";
		this.txttasa.ReadOnly = true;
		this.txttasa.Size = new System.Drawing.Size(92, 18);
		this.txttasa.TabIndex = 106;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label30.Location = new System.Drawing.Point(40, 85);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(71, 12);
		this.label30.TabIndex = 105;
		this.label30.Text = "Tasa de Interés:";
		this.lbLineaCredito.AutoSize = true;
		this.lbLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbLineaCredito.Location = new System.Drawing.Point(13, 20);
		this.lbLineaCredito.Name = "lbLineaCredito";
		this.lbLineaCredito.Size = new System.Drawing.Size(94, 12);
		this.lbLineaCredito.TabIndex = 85;
		this.lbLineaCredito.Text = "Línea de Crédito (S/.):";
		this.txtLineaCredito.Enabled = false;
		this.txtLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCredito.Location = new System.Drawing.Point(126, 17);
		this.txtLineaCredito.Name = "txtLineaCredito";
		this.txtLineaCredito.ReadOnly = true;
		this.txtLineaCredito.Size = new System.Drawing.Size(92, 18);
		this.txtLineaCredito.TabIndex = 84;
		this.txtLineaCreditoUso.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoUso.Location = new System.Drawing.Point(126, 61);
		this.txtLineaCreditoUso.Name = "txtLineaCreditoUso";
		this.txtLineaCreditoUso.ReadOnly = true;
		this.txtLineaCreditoUso.Size = new System.Drawing.Size(92, 18);
		this.txtLineaCreditoUso.TabIndex = 98;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.Location = new System.Drawing.Point(12, 40);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(95, 12);
		this.label23.TabIndex = 95;
		this.label23.Text = "Línea Disponible (S/.):";
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label25.Location = new System.Drawing.Point(14, 64);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(93, 12);
		this.label25.TabIndex = 97;
		this.label25.Text = "Línea C. en Uso (S/.):";
		this.txtLineaCreditoDisponible.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoDisponible.Location = new System.Drawing.Point(126, 40);
		this.txtLineaCreditoDisponible.Name = "txtLineaCreditoDisponible";
		this.txtLineaCreditoDisponible.ReadOnly = true;
		this.txtLineaCreditoDisponible.Size = new System.Drawing.Size(92, 18);
		this.txtLineaCreditoDisponible.TabIndex = 96;
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtBruto.Location = new System.Drawing.Point(415, 115);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(86, 20);
		this.txtBruto.TabIndex = 122;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtBruto, this.customValidator6);
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtDscto.Location = new System.Drawing.Point(415, 154);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(86, 20);
		this.txtDscto.TabIndex = 123;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(412, 138);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(76, 13);
		this.label2.TabIndex = 125;
		this.label2.Text = "Descuento :";
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(412, 99);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(45, 13);
		this.label13.TabIndex = 124;
		this.label13.Text = "Bruto :";
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(315, 129);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(94, 20);
		this.dtpFechaPago.TabIndex = 119;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(130, 129);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(175, 20);
		this.cmbFormaPago.TabIndex = 118;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.Visible = false;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.txtSunat_Capchat.Location = new System.Drawing.Point(315, 155);
		this.txtSunat_Capchat.Name = "txtSunat_Capchat";
		this.txtSunat_Capchat.Size = new System.Drawing.Size(84, 20);
		this.txtSunat_Capchat.TabIndex = 120;
		this.txtSunat_Capchat.Visible = false;
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(131, 75);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(370, 20);
		this.txtDireccion.TabIndex = 117;
		this.txtDireccion.Tag = "21";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(130, 48);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.Size = new System.Drawing.Size(371, 20);
		this.txtNombreCliente.TabIndex = 116;
		this.txtNombreCliente.Tag = "3";
		this.superValidator1.SetValidator1(this.txtNombreCliente, this.customValidator2);
		this.txtCodCliente.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Location = new System.Drawing.Point(227, 21);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(71, 20);
		this.txtCodCliente.TabIndex = 115;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.customValidator1);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyUp);
		this.dtpFecha.Location = new System.Drawing.Point(131, 103);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(278, 20);
		this.dtpFecha.TabIndex = 8;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(20, 132);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(108, 13);
		this.label7.TabIndex = 4;
		this.label7.Text = "FORMA PAGO    :";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(20, 104);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(109, 13);
		this.label6.TabIndex = 3;
		this.label6.Text = "FECHA DOC.      :";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(20, 24);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(113, 13);
		this.label5.TabIndex = 2;
		this.label5.Text = "RUC/DNI Nº (F1) :";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(20, 78);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(107, 13);
		this.label4.TabIndex = 1;
		this.label4.Text = "DIRECCION       :";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(20, 51);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(107, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "CLIENTE           :";
		this.txtPDescuento.Location = new System.Drawing.Point(855, 461);
		this.txtPDescuento.Name = "txtPDescuento";
		this.txtPDescuento.Size = new System.Drawing.Size(88, 20);
		this.txtPDescuento.TabIndex = 126;
		this.txtPDescuento.Tag = "7";
		this.txtPDescuento.Visible = false;
		this.pbCapchatS.Location = new System.Drawing.Point(756, 446);
		this.pbCapchatS.Name = "pbCapchatS";
		this.pbCapchatS.Size = new System.Drawing.Size(53, 35);
		this.pbCapchatS.TabIndex = 121;
		this.pbCapchatS.TabStop = false;
		this.pbCapchatS.Visible = false;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Write Document.png");
		this.imageList2.Images.SetKeyName(1, "New Document.png");
		this.imageList2.Images.SetKeyName(2, "Remove Document.png");
		this.imageList2.Images.SetKeyName(3, "document-print.png");
		this.imageList2.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList2.Images.SetKeyName(5, "exit.png");
		this.imageList2.Images.SetKeyName(6, "barcode-29485.jpg");
		this.imageList2.Images.SetKeyName(7, "images.png");
		this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.txtinafectas);
		this.panel2.Controls.Add(this.label26);
		this.panel2.Controls.Add(this.txtexoneradas);
		this.panel2.Controls.Add(this.label32);
		this.panel2.Controls.Add(this.txtgratuitas);
		this.panel2.Controls.Add(this.label19);
		this.panel2.Controls.Add(this.txtgravadas);
		this.panel2.Controls.Add(this.label22);
		this.panel2.Controls.Add(this.txtPrecioVenta);
		this.panel2.Controls.Add(this.txtIGV);
		this.panel2.Controls.Add(this.txtValorVenta);
		this.panel2.Controls.Add(this.label10);
		this.panel2.Controls.Add(this.label9);
		this.panel2.Controls.Add(this.label8);
		this.panel2.Enabled = false;
		this.panel2.Location = new System.Drawing.Point(753, 204);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(229, 145);
		this.panel2.TabIndex = 8;
		this.txtinafectas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtinafectas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtinafectas.Location = new System.Drawing.Point(43, 30);
		this.txtinafectas.Name = "txtinafectas";
		this.txtinafectas.ReadOnly = true;
		this.txtinafectas.Size = new System.Drawing.Size(69, 18);
		this.txtinafectas.TabIndex = 46;
		this.txtinafectas.Tag = "7";
		this.txtinafectas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label26.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label26.Location = new System.Drawing.Point(14, 33);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(24, 12);
		this.label26.TabIndex = 44;
		this.label26.Tag = "7";
		this.label26.Text = "Ifts :";
		this.txtexoneradas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtexoneradas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtexoneradas.Location = new System.Drawing.Point(149, 6);
		this.txtexoneradas.Name = "txtexoneradas";
		this.txtexoneradas.ReadOnly = true;
		this.txtexoneradas.Size = new System.Drawing.Size(69, 18);
		this.txtexoneradas.TabIndex = 45;
		this.txtexoneradas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label32.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label32.Location = new System.Drawing.Point(117, 9);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(31, 12);
		this.label32.TabIndex = 43;
		this.label32.Text = "Exds :";
		this.txtgratuitas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgratuitas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgratuitas.Location = new System.Drawing.Point(149, 30);
		this.txtgratuitas.Name = "txtgratuitas";
		this.txtgratuitas.ReadOnly = true;
		this.txtgratuitas.Size = new System.Drawing.Size(69, 18);
		this.txtgratuitas.TabIndex = 42;
		this.txtgratuitas.Tag = "7";
		this.txtgratuitas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(120, 33);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(28, 12);
		this.label19.TabIndex = 40;
		this.label19.Tag = "7";
		this.label19.Text = "Grts :";
		this.txtgravadas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgravadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgravadas.Location = new System.Drawing.Point(43, 6);
		this.txtgravadas.Name = "txtgravadas";
		this.txtgravadas.ReadOnly = true;
		this.txtgravadas.Size = new System.Drawing.Size(69, 18);
		this.txtgravadas.TabIndex = 41;
		this.txtgravadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(6, 9);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(33, 12);
		this.label22.TabIndex = 39;
		this.label22.Text = "Gvds :";
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.txtPrecioVenta.Location = new System.Drawing.Point(101, 115);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(88, 20);
		this.txtPrecioVenta.TabIndex = 8;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.txtIGV.Location = new System.Drawing.Point(101, 89);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(88, 20);
		this.txtIGV.TabIndex = 7;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtIGV, this.customValidator4);
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.txtValorVenta.Location = new System.Drawing.Point(101, 63);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(88, 20);
		this.txtValorVenta.TabIndex = 6;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(50, 118);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(49, 12);
		this.label10.TabIndex = 2;
		this.label10.Text = "TOTAL :";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(68, 92);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(31, 12);
		this.label9.TabIndex = 1;
		this.label9.Text = "IGV :";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(27, 66);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(72, 12);
		this.label8.TabIndex = 0;
		this.label8.Text = "SUBTOTAL :";
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.label11);
		this.panel3.Location = new System.Drawing.Point(753, 354);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(229, 76);
		this.panel3.TabIndex = 9;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(6, 21);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(117, 42);
		this.label11.TabIndex = 10;
		this.label11.Text = "00.00";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(686, 10);
		this.txtDocRef.Multiline = true;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(59, 37);
		this.txtDocRef.TabIndex = 40;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(810, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(31, 42);
		this.label1.TabIndex = 43;
		this.label1.Tag = "22";
		this.label1.Text = "-";
		this.label1.Visible = false;
		this.txtSerie.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtSerie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.ForeColor = System.Drawing.Color.Red;
		this.txtSerie.Location = new System.Drawing.Point(756, 10);
		this.txtSerie.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
		this.txtSerie.Multiline = true;
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(52, 37);
		this.txtSerie.TabIndex = 45;
		this.txtSerie.Text = "000";
		this.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtPedido.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPedido.ForeColor = System.Drawing.Color.Red;
		this.txtPedido.Location = new System.Drawing.Point(837, 11);
		this.txtPedido.Multiline = true;
		this.txtPedido.Name = "txtPedido";
		this.txtPedido.ReadOnly = true;
		this.txtPedido.Size = new System.Drawing.Size(143, 37);
		this.txtPedido.TabIndex = 46;
		this.txtPedido.Text = "00000000";
		this.txtPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator6.ErrorMessage = "Your error message here.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator2.ErrorMessage = "Datos Cliente";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese numero RUC o DNI";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator4.ErrorMessage = "Your error message here.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.txtCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodigoBarras.Location = new System.Drawing.Point(572, 12);
		this.txtCodigoBarras.Multiline = true;
		this.txtCodigoBarras.Name = "txtCodigoBarras";
		this.txtCodigoBarras.ReadOnly = true;
		this.txtCodigoBarras.Size = new System.Drawing.Size(108, 36);
		this.txtCodigoBarras.TabIndex = 129;
		this.txtCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(59, 19);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(349, 32);
		this.lbNombreTransaccion.TabIndex = 137;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Enabled = false;
		this.txtTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTransaccion.Location = new System.Drawing.Point(2, 18);
		this.txtTransaccion.Multiline = true;
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.ReadOnly = true;
		this.txtTransaccion.Size = new System.Drawing.Size(53, 27);
		this.txtTransaccion.TabIndex = 138;
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(-2, 3);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(55, 12);
		this.label15.TabIndex = 136;
		this.label15.Text = "Transacción";
		this.chkVentaEspecial.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaEspecial.AutoSize = true;
		this.chkVentaEspecial.Location = new System.Drawing.Point(512, -87);
		this.chkVentaEspecial.Name = "chkVentaEspecial";
		this.chkVentaEspecial.Size = new System.Drawing.Size(78, 17);
		this.chkVentaEspecial.TabIndex = 132;
		this.chkVentaEspecial.Text = "Vta. Esp.";
		this.chkVentaEspecial.UseVisualStyleBackColor = true;
		this.button1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.ImageIndex = 3;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(620, 479);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(103, 35);
		this.button1.TabIndex = 141;
		this.button1.Text = "IMPRIMIR DESPACHO";
		this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnInicioOV.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnInicioOV.Image = (System.Drawing.Image)resources.GetObject("btnInicioOV.Image");
		this.btnInicioOV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnInicioOV.Location = new System.Drawing.Point(5, 481);
		this.btnInicioOV.Name = "btnInicioOV";
		this.btnInicioOV.Size = new System.Drawing.Size(136, 36);
		this.btnInicioOV.TabIndex = 140;
		this.btnInicioOV.Text = "F6 INICIAR VENTA";
		this.btnInicioOV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnInicioOV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnInicioOV.UseVisualStyleBackColor = true;
		this.btnInicioOV.Click += new System.EventHandler(btnInicioOV_Click_1);
		this.btnImprimir.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimir.Image = (System.Drawing.Image)resources.GetObject("btnImprimir.Image");
		this.btnImprimir.Location = new System.Drawing.Point(460, 480);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(154, 35);
		this.btnImprimir.TabIndex = 139;
		this.btnImprimir.Text = "F10 IMPRIMIR VENTA";
		this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Enabled = false;
		this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnSalir.FlatAppearance.BorderSize = 2;
		this.btnSalir.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(305, 481);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(149, 35);
		this.btnSalir.TabIndex = 6;
		this.btnSalir.Text = " F9 CERRAR VENTA ";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardaVenta.Enabled = false;
		this.btnGuardaVenta.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnGuardaVenta.FlatAppearance.BorderSize = 2;
		this.btnGuardaVenta.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardaVenta.Image = (System.Drawing.Image)resources.GetObject("btnGuardaVenta.Image");
		this.btnGuardaVenta.Location = new System.Drawing.Point(147, 481);
		this.btnGuardaVenta.Name = "btnGuardaVenta";
		this.btnGuardaVenta.Size = new System.Drawing.Size(152, 35);
		this.btnGuardaVenta.TabIndex = 7;
		this.btnGuardaVenta.Text = "F8 GUARDA VENTA ";
		this.btnGuardaVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardaVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardaVenta.UseVisualStyleBackColor = true;
		this.btnGuardaVenta.Click += new System.EventHandler(btnGuardaVenta_Click);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.coddetalle.Width = 80;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "Código Producto_";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.codproducto.Width = 70;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Código Producto";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 70;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
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
		this.preciounit.ReadOnly = true;
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
		this.importe.Visible = false;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle4;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle5;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle6;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
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
		this.montodscto.Visible = false;
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
		this.valorventa.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle9;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle10;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.DataPropertyName = "valoreal";
		dataGridViewCellStyle11.Format = "N2";
		dataGridViewCellStyle11.NullValue = null;
		this.valoreal.DefaultCellStyle = dataGridViewCellStyle11;
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.ReadOnly = true;
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.precioreal.DataPropertyName = "precioreal";
		dataGridViewCellStyle12.Format = "N2";
		dataGridViewCellStyle12.NullValue = null;
		this.precioreal.DefaultCellStyle = dataGridViewCellStyle12;
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.ReadOnly = true;
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.precioconigv.DataPropertyName = "precioigv";
		this.precioconigv.HeaderText = "precioconigv";
		this.precioconigv.Name = "precioconigv";
		this.precioconigv.ReadOnly = true;
		this.precioconigv.Visible = false;
		this.valorpromedio.DataPropertyName = "valorpromedio";
		this.valorpromedio.HeaderText = "valorpromedio";
		this.valorpromedio.Name = "valorpromedio";
		this.valorpromedio.ReadOnly = true;
		this.valorpromedio.Visible = false;
		this.Tipoarticulo.DataPropertyName = "Tipoarticulo";
		this.Tipoarticulo.HeaderText = "Tipoarticulo";
		this.Tipoarticulo.Name = "Tipoarticulo";
		this.Tipoarticulo.ReadOnly = true;
		this.Tipoarticulo.Visible = false;
		this.Tipoimpuesto.DataPropertyName = "Tipoimpuesto";
		this.Tipoimpuesto.HeaderText = "Tipoimpuesto";
		this.Tipoimpuesto.Name = "Tipoimpuesto";
		this.Tipoimpuesto.ReadOnly = true;
		this.Tipoimpuesto.Visible = false;
		this.codalmacen.DataPropertyName = "codalmacen";
		this.codalmacen.HeaderText = "codalmacen";
		this.codalmacen.Name = "codalmacen";
		this.codalmacen.ReadOnly = true;
		this.codalmacen.Visible = false;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.Visible = false;
		this.TipoUnidad.DataPropertyName = "TipoUnidad";
		this.TipoUnidad.HeaderText = "T. Und";
		this.TipoUnidad.Name = "TipoUnidad";
		this.TipoUnidad.ReadOnly = true;
		this.TipoUnidad.Visible = false;
		this.CodigoEmpresa.DataPropertyName = "CodigoEmpresa";
		this.CodigoEmpresa.HeaderText = "CodigoEmpresa";
		this.CodigoEmpresa.Name = "CodigoEmpresa";
		this.CodigoEmpresa.ReadOnly = true;
		this.CodigoEmpresa.Visible = false;
		this.descripcion_venta.DataPropertyName = "descripcion_venta";
		this.descripcion_venta.HeaderText = "descripcion_venta";
		this.descripcion_venta.Name = "descripcion_venta";
		this.descripcion_venta.ReadOnly = true;
		this.descripcion_venta.Visible = false;
		this.serie_motor.DataPropertyName = "serie_motor";
		this.serie_motor.HeaderText = "Serie de Motor";
		this.serie_motor.Name = "serie_motor";
		this.serie_motor.ReadOnly = true;
		this.serie_motor.Visible = false;
		this.nrochasis.DataPropertyName = "nrochasis";
		this.nrochasis.HeaderText = "Nº de Chasis";
		this.nrochasis.Name = "nrochasis";
		this.nrochasis.ReadOnly = true;
		this.nrochasis.Visible = false;
		this.modelo.DataPropertyName = "modelo";
		this.modelo.HeaderText = "Modelo";
		this.modelo.Name = "modelo";
		this.modelo.ReadOnly = true;
		this.modelo.Visible = false;
		this.Marca.DataPropertyName = "marca";
		this.Marca.HeaderText = "Marca";
		this.Marca.Name = "Marca";
		this.Marca.ReadOnly = true;
		this.Marca.Visible = false;
		this.color.DataPropertyName = "color";
		this.color.HeaderText = "Color";
		this.color.Name = "color";
		this.color.ReadOnly = true;
		this.color.Visible = false;
		this.codFamilia.DataPropertyName = "codFamilia";
		this.codFamilia.HeaderText = "codFamilia";
		this.codFamilia.Name = "codFamilia";
		this.codFamilia.ReadOnly = true;
		this.codFamilia.Visible = false;
		this.venta_tickeck.HeaderText = "venta_tickeck";
		this.venta_tickeck.Name = "venta_tickeck";
		this.venta_tickeck.ReadOnly = true;
		this.codigoproductosunat.DataPropertyName = "codigoproductosunat";
		this.codigoproductosunat.HeaderText = "CodigoProductoSunat";
		this.codigoproductosunat.Name = "codigoproductosunat";
		this.codigoproductosunat.ReadOnly = true;
		this.codli.DataPropertyName = "codli";
		this.codli.HeaderText = "codlinea";
		this.codli.Name = "codli";
		this.codli.ReadOnly = true;
		this.codfa.DataPropertyName = "codfa";
		this.codfa.HeaderText = "codfamilia";
		this.codfa.Name = "codfa";
		this.codfa.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(982, 516);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.chkVentaEspecial);
		base.Controls.Add(this.btnInicioOV);
		base.Controls.Add(this.btnImprimir);
		base.Controls.Add(this.txtTransaccion);
		base.Controls.Add(this.lbNombreTransaccion);
		base.Controls.Add(this.txtPDescuento);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardaVenta);
		base.Controls.Add(this.txtCodigoBarras);
		base.Controls.Add(this.txtPedido);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSerie);
		base.Controls.Add(this.txtDocRef);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.pbCapchatS);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.Name = "frmGeneraVenta";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "     ";
		base.Load += new System.EventHandler(frmGeneraVenta_Load);
		base.Shown += new System.EventHandler(frmGeneraVenta_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmGeneraVenta_KeyDown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
