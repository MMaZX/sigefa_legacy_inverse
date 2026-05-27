using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using Tesseract;

namespace SIGEFA.Formularios;

public class frmOrdenVenta : Office2007Form
{
	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	public List<clsDetallePedido> detalle = new List<clsDetallePedido>();

	private clsReporteCodigoBarras ds = new clsReporteCodigoBarras();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsPedido pedido = new clsPedido();

	private clsValidar ok = new clsValidar();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie AdmSerie = new clsAdmSerie();

	public int CodSerie;

	public int manual = 0;

	private Sunat MyInfoSunat;

	private Reniec MyInfoReniec;

	private IntRange red = new IntRange(0, 255);

	private IntRange green = new IntRange(0, 255);

	private IntRange blue = new IntRange(0, 255);

	private CheckBoxX lastChecked;

	public int CodCliente;

	public int CodDocumento;

	public int Tipo;

	public int Proceso = 0;

	public decimal montogratuitas;

	public decimal montogravadas;

	public decimal montoexoneradas = default(decimal);

	public decimal montoinafectas = default(decimal);

	public string NombreCliente;

	public string CodPedido;

	public bool banderagrabada;

	public bool banderaexonerada;

	public bool banderainafecta;

	public bool banderadelete = false;

	public bool bandera = false;

	public int cuentaForm = 0;

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	public clsUsuario vendedor;

	private clsAdmProducto admProducto = new clsAdmProducto();

	public decimal cantidadOriginalProducto;

	public mdi_Menu menu;

	private decimal cantidadanterior;

	private decimal cantidadnueva = default(decimal);

	private IContainer components = null;

	private GroupBox groupBox1;

	private Panel panel1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private Button btnEditaOV;

	private Button btnAnulaOV;

	private Button btnDeleteItem;

	private Button btnAddItem;

	private RadioButton chkFactura;

	private RadioButton chkBoleta;

	private Button btnInicioOV;

	private Button btnSalir;

	private Button btnGuardaOV;

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

	public CheckBoxX chkVentaGratuita;

	public CheckBoxX chkVentaDsctoGlobal;

	private TextBox txtNombreCliente;

	private TextBox txtCodCliente;

	public TextBox txtDireccion;

	private DateTimePicker dtpFechaPago;

	private ComboBox cmbFormaPago;

	private Label lbDocumento;

	public TextBox txtDocRef;

	private Label label12;

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

	private CustomValidator customValidator5;

	private CustomValidator customValidator6;

	private TextBox txtinafectas;

	private Label label26;

	private TextBox txtexoneradas;

	private Label label32;

	private TextBox txtgratuitas;

	private Label label19;

	private TextBox txtgravadas;

	private Label label22;

	private Button btnImprimirTicket;

	private ImageList imageList2;

	private TextBox txtCodigoBarras;

	private GroupBox groupBox6;

	private GroupBox groupBox5;

	private TextBox txttasa;

	private Label label30;

	private Label lbLineaCredito;

	private TextBox txtLineaCredito;

	private TextBox txtLineaCreditoUso;

	private Label label23;

	private Label label25;

	private TextBox txtLineaCreditoDisponible;

	private Button btnCancelarOV;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo1;

	private Label label14;

	private TextBox txtCodigoVendedor;

	private TextBox txtNombreVendedor;

	private BalloonTip btVendedor;

	public Label lblCantidadProductos;

	private RequiredFieldValidator requiredFieldValidator1;

	private RequiredFieldValidator requiredFieldValidator3;

	private RequiredFieldValidator requiredFieldValidator2;

	private RequiredFieldValidator requiredFieldValidator4;

	public CheckBoxX chkVentaSinStock;

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

	private DataGridViewTextBoxColumn CodEmpresa;

	private DataGridViewTextBoxColumn codlinea;

	private DataGridViewTextBoxColumn codfamilia;

	public frmOrdenVenta(mdi_Menu menu)
	{
		InitializeComponent();
		this.menu = menu;
	}

	public frmOrdenVenta()
	{
		InitializeComponent();
	}

	private void LimpiaForm()
	{
		foreach (Control c in base.Controls)
		{
			if (c is TextBox)
			{
				c.Text = "";
			}
		}
		dgvDetalle.Rows.Clear();
		calculatotales();
	}

	private void CerrarOV()
	{
		if (dgvDetalle.Rows.Count > 0 && txtPedido.Text == "")
		{
			if (MessageBox.Show("Existen productos pendientes por vender..! \n Desea cancelar el pedido?", "Pedido Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Close();
			}
		}
		else
		{
			Close();
		}
	}

	private void CargaNumeracionOV()
	{
		try
		{
			ser = AdmSerie.CargaSerieOV(frmLogin.iCodEmpresa, frmLogin.iCodAlmacen);
			if (ser != null)
			{
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
			else
			{
				MessageBox.Show("No existe numeración para la orden de venta", "Orden de venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void frmOrdenVenta_KeyDown(object sender, KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
		case Keys.F6:
			activaPaneles(estado: true);
			sololectura(estado: false);
			Proceso = 1;
			CargaFormaPagos();
			if (Proceso == 1)
			{
				txtDocRef.Text = "OV";
				txtCodCliente.Text = "C000001";
				KeyPressEventArgs ee = new KeyPressEventArgs('\r');
				txtDocRef_KeyPress(txtDocRef, ee);
				BuscaCliente();
			}
			LimpiaForm();
			CargaNumeracionOV();
			break;
		case Keys.F3:
			if (bandera)
			{
				LlamarAddDetalle();
				dgvDetalle.Focus();
			}
			break;
		case Keys.F2:
			if (bandera)
			{
				btnDeleteItem_Click(sender, new EventArgs());
			}
			break;
		case Keys.F1:
			if (bandera && base.ActiveControl is TextBox)
			{
				TextBox campoTexto = base.ActiveControl as TextBox;
				if (campoTexto.Name == "txtCodigoVendedor")
				{
					txtCodigoVendedor_KeyUp(sender, e);
				}
				if (campoTexto.Name == "txtCodCliente")
				{
					txtCodCliente_KeyUp(sender, e);
				}
			}
			break;
		case Keys.F8:
			btnGuardaOV_Click(sender, e);
			break;
		case Keys.F9:
			Close();
			break;
		case Keys.F4:
			sololectura2(estado: false);
			activaPaneles(estado: true);
			Proceso = 2;
			Recalcular();
			break;
		case Keys.F5:
		case Keys.F7:
			break;
		}
	}

	private void LlamarAddDetalle()
	{
		if (Application.OpenForms["frmDetalleSalida"] != null)
		{
			Application.OpenForms["frmDetalleSalida"].Activate();
			return;
		}
		frmDetalleSalida form = new frmDetalleSalida();
		form.Procede = 3;
		form.Proceso = 1;
		form.Moneda = 1;
		form.codTipodoc = doc.CodTipoDocumento;
		form.ventasinafectaciondestock = chkVentaSinStock.Checked;
		form.ShowDialog(this);
	}

	public void ChangeTextBoxText(clsDetalleNotaSalida detalle)
	{
		try
		{
			if (detalle != null)
			{
				montogratuitas = detalle.Vmontogratuitas;
				montogravadas = detalle.Vmontogravadas;
				montoexoneradas = detalle.Vmontoexoneradas;
				montoinafectas = detalle.Vmontoinafectas;
				dgvDetalle.Rows.Add("0", detalle.CodProducto, detalle.Referencia, detalle.Descripcion, detalle.CodUnidad, detalle.Unidad, Convert.ToDouble(detalle.Cantidad), Convert.ToDouble(detalle.PrecioUnitario), detalle.Vbruto, 0, 0, 0, detalle.Vmontodescuento, detalle.Vvalorventa, detalle.Vigv, detalle.Vprecioventa, detalle.Vvalorreal, detalle.Vprecioreal, detalle.Vprecioventa, 0, detalle.TipoArticulo, detalle.TipoImpuesto, detalle.CodAlmacen, detalle.DescripcionAlmacen, detalle.TipoUnidad, detalle.CodEmpresa);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void activaPaneles(bool estado)
	{
		groupBox2.Enabled = estado;
		btnAddItem.Enabled = estado;
		btnDeleteItem.Enabled = estado;
		btnEditaOV.Enabled = !estado;
		btnSalir.Enabled = estado;
		btnGuardaOV.Enabled = estado;
		groupBox4.Enabled = estado;
		panel2.Enabled = estado;
		btnInicioOV.Enabled = !estado;
		bandera = estado;
	}

	private void CargaCliente()
	{
		try
		{
			cli = AdmCli.MuestraCliente(CodCliente);
			if (cli.RucDni.Length == 8)
			{
				chkBoleta.Checked = true;
			}
			else if (cli.RucDni.Length == 11)
			{
				chkFactura.Checked = true;
			}
			txtCodCliente.Text = cli.RucDni;
			txtNombreCliente.Text = cli.RazonSocial;
			txtDireccion.Text = cli.DireccionLegal;
			btnAddItem.Focus();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
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
				label12.Visible = false;
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
			lbDocumento.Text = doc.Descripcion;
			lbDocumento.Visible = true;
			label1.Visible = true;
			return true;
		}
		CodDocumento = 0;
		return false;
	}

	private void frmOrdenVenta_Shown(object sender, EventArgs e)
	{
		CargaFormaPagos();
		if (Proceso == 1)
		{
			txtDocRef.Text = "OV";
			txtCodCliente.Text = "C000001";
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			txtDocRef_KeyPress(txtDocRef, ee);
			BuscaCliente();
		}
		else if (Proceso == 3)
		{
			btnSalir.Visible = true;
			btnSalir.Enabled = true;
			btnImprimirTicket.Enabled = true;
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
						cmbFormaPago.SelectedValue = 6;
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
		if (Proceso == 1 || Proceso == 3 || Proceso == 2)
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
					banderadelete = false;
				}
				montosventa();
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
		if (Proceso != 0)
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

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
	}

	private void btnAddItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (bandera)
			{
				LlamarAddDetalle();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnDeleteItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvDetalle.Rows.Count > 0 && dgvDetalle.CurrentCell.RowIndex > -1)
			{
				if (Convert.ToInt32(dgvDetalle.CurrentRow.Cells[coddetalle.Name].Value) == 0)
				{
					banderadelete = true;
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
				lblCantidadProductos.Text = "Productos Agregados: " + dgvDetalle.RowCount;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		CerrarOV();
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

	private void btnGuardaOV_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor.Current = Cursors.WaitCursor;
			if (!verificarPrecioVenta())
			{
				return;
			}
			if (dgvDetalle.RowCount == 0)
			{
				MessageBox.Show("No se puedo Guardar, no existen productos en el pedido!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				btnAddItem.Focus();
			}
			else
			{
				if (!superValidator1.Validate() || Proceso == 0)
				{
					return;
				}
				pedido.CodAlmacen = frmLogin.iCodAlmacen;
				if (CodCliente == 0)
				{
					ValidaCliente(txtCodCliente.Text);
				}
				pedido.CodCliente = CodCliente;
				pedido.CodTipoDocumento = doc.CodTipoDocumento;
				if (txtCodCliente.Text.ToString() == "00000000")
				{
					pedido.Nombrecliente = txtNombreCliente.Text;
				}
				else
				{
					pedido.Nombrecliente = txtNombreCliente.Text;
				}
				pedido.Moneda = 1;
				if (mdi_Menu.tc_hoy > 0.0)
				{
					pedido.TipoCambio = mdi_Menu.tc_hoy;
				}
				pedido.FechaPedido = dtpFecha.Value.Date;
				pedido.FechaEntrega = dtpFecha.Value.Date;
				pedido.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
				pedido.FechaPago = dtpFecha.Value.AddDays(fpago.Dias);
				pedido.CodSerie = CodSerie;
				pedido.Comentario = "";
				pedido.SerieDoc = txtSerie.Text;
				pedido.MontoBruto = Convert.ToDecimal(txtBruto.Text);
				pedido.MontoDscto = Convert.ToDecimal(txtDscto.Text);
				pedido.Igv = Convert.ToDecimal(txtIGV.Text);
				pedido.Total = Convert.ToDecimal(txtPrecioVenta.Text);
				pedido.CodVendedor = vendedor.CodUsuario;
				pedido.CodUser = vendedor.CodUsuario;
				pedido.Estado = 1;
				pedido.Gratuitas = montogratuitas;
				pedido.Exoneradas = montoexoneradas;
				pedido.Gravadas = montogravadas;
				pedido.Inafectas = montoinafectas;
				if (chkBoleta.Checked)
				{
					pedido.Boletafactura = 1;
				}
				else if (chkFactura.Checked)
				{
					pedido.Boletafactura = 2;
				}
				pedido.CodEmpresa = frmLogin.iCodEmpresa;
				pedido.CodigoBarras = "";
				pedido.CodigoBarrasCifrado = "";
				pedido.ventasinstock = Convert.ToInt32(chkVentaSinStock.Checked);
				banderagrabada = false;
				banderaexonerada = false;
				banderainafecta = false;
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					switch (Convert.ToInt32(row.Cells[Tipoimpuesto.Name].Value))
					{
					case 10:
						banderagrabada = true;
						break;
					case 11:
						banderagrabada = true;
						break;
					case 12:
						banderagrabada = true;
						break;
					case 13:
						banderagrabada = true;
						break;
					case 14:
						banderagrabada = true;
						break;
					case 15:
						banderagrabada = true;
						break;
					case 16:
						banderagrabada = true;
						break;
					case 17:
						banderagrabada = true;
						break;
					case 20:
						banderaexonerada = true;
						break;
					case 30:
						banderainafecta = true;
						break;
					case 31:
						banderainafecta = true;
						break;
					case 32:
						banderainafecta = true;
						break;
					case 33:
						banderainafecta = true;
						break;
					case 34:
						banderainafecta = true;
						break;
					case 35:
						banderainafecta = true;
						break;
					case 36:
						banderainafecta = true;
						break;
					}
				}
				if (chkVentaGratuita.Checked)
				{
					pedido.Tipoventa = 4;
				}
				else if (chkVentaDsctoGlobal.Checked)
				{
					pedido.Tipoventa = 5;
				}
				else if (banderagrabada && !banderaexonerada && !banderainafecta)
				{
					pedido.Tipoventa = 1;
				}
				else if (!banderagrabada && banderaexonerada && !banderainafecta)
				{
					pedido.Tipoventa = 2;
				}
				else if (!banderagrabada && !banderaexonerada && banderainafecta)
				{
					pedido.Tipoventa = 3;
				}
				else if (banderagrabada && banderaexonerada && !banderainafecta)
				{
					pedido.Tipoventa = 6;
				}
				else if (!banderagrabada && banderaexonerada && banderainafecta)
				{
					pedido.Tipoventa = 7;
				}
				if (Proceso == 1)
				{
					RecorreDetalle();
					pedido.Detalle = detalle;
					if (AdmPedido.insertarOrdenVenta(pedido))
					{
						CodPedido = pedido.CodPedido;
						if (CodPedido != "")
						{
							Proceso = 3;
							frmOrdenVenta_Load(new object(), new EventArgs());
						}
						MessageBox.Show("Los datos se guardaron correctamente!\nPedido: " + Convert.ToInt32(pedido.Numeracion) + "\nTotal: " + pedido.Total + " Soles.", "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						btnGuardaOV.Enabled = false;
						btnAddItem.Enabled = false;
						btnEditaOV.Enabled = false;
						btnDeleteItem.Enabled = false;
						txtBruto.Enabled = false;
						txtDscto.Enabled = false;
						sololectura(estado: true);
						Close();
						menu.biOrdenVenta_Click(sender, e);
					}
					else
					{
						MessageBox.Show("Ocurrió un error al registrar la operación", "Orden de Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					if (Proceso != 2 || !AdmPedido.update(pedido))
					{
						return;
					}
					RecorreDetalle();
					foreach (clsDetallePedido det in detalle)
					{
						foreach (clsDetallePedido det2 in detalle)
						{
							if (det.CodDetallePedido == det2.CodDetallePedido && det2.CodDetallePedido != 0)
							{
								AdmPedido.updatedetalle(det2);
							}
						}
					}
					foreach (clsDetallePedido deta in detalle)
					{
						if (deta.CodDetallePedido == 0)
						{
							AdmPedido.insertdetalle(deta);
						}
					}
					CodPedido = pedido.CodPedido;
					CargaPedido();
					MessageBox.Show("Los datos se guardaron correctamente!\nPedido: " + Convert.ToInt32(pedido.Numeracion) + "\nTotal: " + pedido.Total + " Soles.", "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					sololectura(estado: true);
					((frmPedidosPendientes)Application.OpenForms["frmPedidosPendientes"])?.CargaLista();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message.ToString());
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
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
				doc.CodTipoDocumento = pedido.CodTipoDocumento;
				pedido.CodCotizacion = 0;
				txtSerie.Text = pedido.SerieDoc;
				CodSerie = pedido.CodSerie;
				txtPedido.Text = pedido.Numeracion.ToString().PadLeft(8, '0');
				txtCodigoBarras.Text = pedido.CodigoBarrasCifrado;
				if (pedido.Boletafactura == 1)
				{
					CodCliente = pedido.CodCliente;
					cli.CodCliente = CodCliente;
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
				}
				else
				{
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
				}
				dtpFecha.Value = pedido.FechaPedido;
				if (txtDocRef.Enabled)
				{
					CodDocumento = pedido.CodTipoDocumento;
					txtDocRef.Text = pedido.SiglaDocumento;
					lbDocumento.Text = pedido.DescripcionDocumento;
				}
				txtBruto.Text = $"{pedido.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{pedido.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{pedido.Total - pedido.Igv:#,##0.00}";
				txtIGV.Text = $"{pedido.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{pedido.Total:#,##0.00}";
				montogravadas = pedido.Gravadas;
				montoexoneradas = pedido.Exoneradas;
				montoinafectas = pedido.Inafectas;
				montogratuitas = pedido.Gratuitas;
				txtPedido.Text = pedido.Numeracion.ToString().PadLeft(8, '0');
				label11.Text = txtPrecioVenta.Text;
				montosventa();
				CargaDetalle();
				if (Proceso == 2)
				{
					pedido.Detalle = new List<clsDetallePedido>();
					RecorreDetallePedido();
				}
				txtCodigoVendedor.Text = pedido.CodUser.ToString();
				txtCodigoVendedor.Focus();
				KeyEventArgs arg = new KeyEventArgs(Keys.Return);
				txtCodigoVendedor_KeyDown(new object(), arg);
				lblCantidadProductos.Text = "Productos Agregados: " + dgvDetalle.RowCount;
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

	private void RecorreDetallePedido()
	{
		pedido.Detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetallePedido(row);
		}
	}

	private void añadedetallePedido(DataGridViewRow fila)
	{
		clsDetallePedido deta = new clsDetallePedido();
		deta.CodDetallePedido = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodPedido = Convert.ToInt32(pedido.CodPedido);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDecimal(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDecimal(fila.Cells[dscto3.Name].Value);
		deta.Descuento3 = Convert.ToDecimal(fila.Cells[dscto3.Name].Value);
		deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.Precioigv = Convert.ToBoolean(Convert.ToInt32(fila.Cells[precioconigv.Name].Value));
		deta.Valorpromedio = Convert.ToDecimal(fila.Cells[valorpromedio.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		pedido.Detalle.Add(deta);
	}

	private void CargaDetalle()
	{
		DataTable newData = new DataTable();
		dgvDetalle.Rows.Clear();
		try
		{
			newData = AdmPedido.CargaDetalle(Convert.ToInt32(pedido.CodPedido));
			foreach (DataRow row in newData.Rows)
			{
				dgvDetalle.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), "", row[37].ToString(), row[38].ToString());
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
		deta.CodAlmacen = Convert.ToInt32(fila.Cells[codalmacen.Name].Value);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = 0m;
		deta.Descuento3 = 0m;
		deta.MontoDescuento = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.Precioigv = Convert.ToBoolean(Convert.ToInt32(fila.Cells[precioconigv.Name].Value));
		deta.Valorpromedio = Convert.ToDecimal(fila.Cells[valorpromedio.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.Tipoimpuesto = fila.Cells[Tipoimpuesto.Name].Value.ToString();
		deta.codlinea = Convert.ToInt32(fila.Cells[codlinea.Name].Value);
		deta.codfamilia = Convert.ToInt32(fila.Cells[codfamilia.Name].Value);
		detalle.Add(deta);
	}

	public bool verificarPrecioVenta()
	{
		bool valor = false;
		if (mdi_Menu.MontoTopeBoleta > 0)
		{
			if (Convert.ToDecimal(txtPrecioVenta.Text) >= (decimal)mdi_Menu.MontoTopeBoleta && CodCliente == 1)
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
		chkBoleta.Checked = true;
		chkFactura.Checked = false;
	}

	private void chkFactura_Click(object sender, EventArgs e)
	{
		chkBoleta.Checked = false;
		chkFactura.Checked = true;
	}

	private void frmOrdenVenta_Load(object sender, EventArgs e)
	{
		if (Proceso == 3)
		{
			CargaPedido();
			sololectura(estado: true);
			label1.Visible = true;
		}
		if (Proceso == 2)
		{
			CargaPedido();
			sololectura2(estado: true);
			label1.Visible = true;
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
		btnInicioOV.Enabled = estado;
		btnAddItem.Enabled = !estado;
		btnDeleteItem.Enabled = !estado;
		btnGuardaOV.Enabled = !estado;
		btnEditaOV.Enabled = estado;
		btnAnulaOV.Enabled = estado;
		lbDocumento.Visible = estado;
		btnSalir.Visible = estado;
		btnSalir.Enabled = estado;
	}

	private void sololectura2(bool estado)
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
		btnInicioOV.Enabled = false;
		btnAddItem.Enabled = !estado;
		btnDeleteItem.Enabled = !estado;
		btnGuardaOV.Enabled = !estado;
		btnEditaOV.Enabled = estado;
		btnAnulaOV.Enabled = estado;
		lbDocumento.Visible = estado;
		btnSalir.Visible = estado;
		btnSalir.Enabled = estado;
		btnImprimirTicket.Visible = estado;
		btnCancelarOV.Visible = !estado;
		btnCancelarOV.Enabled = !estado;
	}

	private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			if (e.KeyChar != '\r')
			{
				return;
			}
			if (Application.OpenForms["frmClientesLista"] != null)
			{
				Application.OpenForms["frmClientesLista"].Activate();
				return;
			}
			frmClientesLista form = new frmClientesLista();
			form.Proceso = 7;
			form.filtrocliente = txtNombreCliente.Text;
			if (form.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CodCliente = form.GetCodigoCliente();
			CargaCliente();
			if (cli == null)
			{
				return;
			}
			CodCliente = cli.CodCliente;
			txtNombreCliente.Text = cli.RazonSocial;
			txtDireccion.Text = cli.DireccionLegal;
			txtCodCliente.Text = cli.RucDni;
			cli = AdmCli.ConsultaCliente(cli.RucDni);
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
				cmbFormaPago.SelectedValue = 6;
				EventArgs ee = new EventArgs();
				cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
			}
			else
			{
				dtpFechaPago.Value = DateTime.Today;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtPDescuento_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtCodigoVendedor_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Return && txtCodigoVendedor.Text.Trim() != "")
			{
				int codigoUsuario = Convert.ToInt32(txtCodigoVendedor.Text);
				vendedor = admUsuario.MuestraUsuarioSinAdmin(codigoUsuario);
				if (vendedor != null)
				{
					txtNombreVendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
					return;
				}
				txtCodigoVendedor.Text = "";
				txtNombreVendedor.Text = "";
				MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtCodigoVendedor_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.F1)
			{
				frmVendedoresLista frmDialog = new frmVendedoresLista();
				frmDialog.ShowDialog();
				if (vendedor != null)
				{
					txtCodigoVendedor.Text = vendedor.CodUsuario.ToString();
					txtNombreVendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.Rows.Count >= 1)
		{
			cantidadOriginalProducto = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[6].Value);
		}
	}

	private void btnAnulaOV_Click(object sender, EventArgs e)
	{
	}

	private void panel3_Paint(object sender, PaintEventArgs e)
	{
	}

	private void btnInicioOV_Click(object sender, EventArgs e)
	{
		activaPaneles(estado: true);
		CargaNumeracionOV();
	}

	private void btnImprimirTicket_Click(object sender, EventArgs e)
	{
		try
		{
			CRCodigodeBarras rpt = new CRCodigodeBarras();
			frmCodigobarra frm = new frmCodigobarra();
			rpt.SetDataSource(ds.CodigoBarras(Convert.ToInt32(CodPedido), frmLogin.iCodAlmacen));
			frm.crystalReportViewer1.ReportSource = rpt;
			frm.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnEditaOV_Click(object sender, EventArgs e)
	{
		sololectura2(estado: false);
		bandera = true;
		calculatotales();
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
			if (CodCliente == 0)
			{
				return;
			}
			txtCodCliente.Text = "";
			txtDireccion.Text = "";
			txtNombreCliente.Text = "";
			NombreCliente = cli.Nombre;
			CargaCliente();
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
				cmbFormaPago.SelectedValue = 6;
				EventArgs ee = new EventArgs();
				cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
			}
			else
			{
				dtpFechaPago.Value = DateTime.Today;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void groupBox3_Enter(object sender, EventArgs e)
	{
	}

	private void chkVentaGratuita_Click(object sender, EventArgs e)
	{
	}

	private void chkVentaDsctoGlobal_Click(object sender, EventArgs e)
	{
	}

	private void chkVentaGratuita_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaDsctoGlobal.Checked)
		{
			chkVentaDsctoGlobal.Checked = false;
		}
	}

	private void chkVentaDsctoGlobal_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaGratuita.Checked)
		{
			chkVentaGratuita.Checked = false;
		}
	}

	private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.RowCount > 0)
		{
			dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = false;
			dgvDetalle.CurrentRow.Cells[cantidad.Name].Style.BackColor = Color.PeachPuff;
			dgvDetalle.CurrentRow.Cells[cantidad.Name].Selected = true;
			cantidadanterior = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
		}
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.RowCount <= 0 || e.ColumnIndex != 6)
		{
			return;
		}
		MessageBox.Show("cantidad original" + cantidadOriginalProducto, "asd");
		cantidadnueva = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
		clsProducto productoBuscado = admProducto.CargaProducto(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen);
		if (Convert.ToDouble(cantidadnueva) <= productoBuscado.StockActual)
		{
			if (cantidadnueva > 0m && cantidadanterior > 0m && cantidadnueva != cantidadanterior)
			{
				dgvDetalle.CurrentRow.Cells[precioventa.Name].Value = $"{cantidadnueva * Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value):#,##0.00}";
				dgvDetalle.CurrentRow.Cells[valorventa.Name].Value = $"{Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value) * (1.0 - frmLogin.Configuracion.IGV / 100.0):#,##0.00}";
				dgvDetalle.CurrentRow.Cells[igv.Name].Value = $"{Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value) - Convert.ToDouble(dgvDetalle.CurrentRow.Cells[valorventa.Name].Value):#,##0.00}";
				dgvDetalle.CurrentRow.Cells[importe.Name].Value = $"{Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value):#,##0.00}";
				Recalcular();
				return;
			}
			int codigoDetallePedido = Convert.ToInt32(dgvDetalle.CurrentRow.Cells[coddetalle.Name].Value);
			decimal cantidadOriginal = default(decimal);
			foreach (clsDetallePedido detalle_ in pedido.Detalle)
			{
				if (detalle_.CodDetallePedido == codigoDetallePedido)
				{
					cantidadOriginal = Convert.ToDecimal(detalle_.Cantidad);
					break;
				}
			}
			dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = $"{Convert.ToDouble(cantidadOriginal):#,##0.00}";
			dgvDetalle.CurrentRow.Cells[precioventa.Name].Value = $"{cantidadOriginal * Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value):#,##0.00}";
			dgvDetalle.CurrentRow.Cells[valorventa.Name].Value = $"{Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value) * (1.0 - frmLogin.Configuracion.IGV / 100.0):#,##0.00}";
			dgvDetalle.CurrentRow.Cells[igv.Name].Value = $"{Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value) - Convert.ToDouble(dgvDetalle.CurrentRow.Cells[valorventa.Name].Value):#,##0.00}";
			dgvDetalle.CurrentRow.Cells[importe.Name].Value = $"{Convert.ToDouble(dgvDetalle.CurrentRow.Cells[precioventa.Name].Value):#,##0.00}";
			Recalcular();
			MessageBox.Show("Cantidad Inválida!! Se tomará la cantidad anterior para el producto editado!", "Orden de venta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MessageBox.Show("Cantidad Inválida!! SOBREPASA EL STOCK ACTUAL DEL PRODUCTO!, SE TOMARÁ LA CANTIDAD ANTERIOR PARA EL PRODUCTO EDITADO", "Orden de venta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = $"{Convert.ToDouble(cantidadOriginalProducto):#,##0.00}";
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmOrdenVenta));
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnEditaOV = new System.Windows.Forms.Button();
		this.btnAddItem = new System.Windows.Forms.Button();
		this.btnDeleteItem = new System.Windows.Forms.Button();
		this.btnAnulaOV = new System.Windows.Forms.Button();
		this.btnInicioOV = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.chkFactura = new System.Windows.Forms.RadioButton();
		this.chkBoleta = new System.Windows.Forms.RadioButton();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnGuardaOV = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txtNombreVendedor = new System.Windows.Forms.TextBox();
		this.txtCodigoVendedor = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.btnImprimirTicket = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.txtPDescuento = new System.Windows.Forms.TextBox();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.pbCapchatS = new System.Windows.Forms.PictureBox();
		this.txtSunat_Capchat = new System.Windows.Forms.TextBox();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.chkVentaDsctoGlobal = new DevComponents.DotNetBar.Controls.CheckBoxX();
		this.chkVentaGratuita = new DevComponents.DotNetBar.Controls.CheckBoxX();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
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
		this.lbDocumento = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtPedido = new System.Windows.Forms.TextBox();
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.requiredFieldValidator1 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("DEBE SELECCIONAR UN VENDEDOR");
		this.requiredFieldValidator4 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.requiredFieldValidator3 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("SELECCIONE UN CLIENTE");
		this.requiredFieldValidator2 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("SELECCIONE UN CLIENTE");
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.txtCodigoBarras = new System.Windows.Forms.TextBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.btnCancelarOV = new System.Windows.Forms.Button();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txttasa = new System.Windows.Forms.TextBox();
		this.label30 = new System.Windows.Forms.Label();
		this.lbLineaCredito = new System.Windows.Forms.Label();
		this.txtLineaCredito = new System.Windows.Forms.TextBox();
		this.txtLineaCreditoUso = new System.Windows.Forms.TextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.txtLineaCreditoDisponible = new System.Windows.Forms.TextBox();
		this.btVendedor = new DevComponents.DotNetBar.BalloonTip();
		this.lblCantidadProductos = new System.Windows.Forms.Label();
		this.cachedCRCuotasPrestamo1 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.chkVentaSinStock = new DevComponents.DotNetBar.Controls.CheckBoxX();
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
		this.CodEmpresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codlinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codfamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.panel1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).BeginInit();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox6.SuspendLayout();
		this.groupBox5.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvDetalle);
		this.groupBox1.Location = new System.Drawing.Point(12, 41);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(748, 268);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle Orden Venta";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.valoreal, this.precioreal, this.precioconigv, this.valorpromedio, this.Tipoarticulo, this.Tipoimpuesto, this.codalmacen, this.almacen, this.TipoUnidad, this.CodEmpresa, this.codlinea, this.codfamilia);
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle14;
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(742, 249);
		this.dgvDetalle.TabIndex = 2;
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellDoubleClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.groupBox3);
		this.panel1.Controls.Add(this.groupBox2);
		this.panel1.Location = new System.Drawing.Point(764, 57);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(258, 190);
		this.panel1.TabIndex = 2;
		this.groupBox3.Controls.Add(this.btnEditaOV);
		this.groupBox3.Controls.Add(this.btnAddItem);
		this.groupBox3.Controls.Add(this.btnDeleteItem);
		this.groupBox3.Controls.Add(this.btnAnulaOV);
		this.groupBox3.Controls.Add(this.btnInicioOV);
		this.groupBox3.Location = new System.Drawing.Point(3, 2);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(254, 158);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Opciones";
		this.groupBox3.Enter += new System.EventHandler(groupBox3_Enter);
		this.btnEditaOV.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnEditaOV.FlatAppearance.BorderSize = 2;
		this.btnEditaOV.Location = new System.Drawing.Point(94, 93);
		this.btnEditaOV.Name = "btnEditaOV";
		this.btnEditaOV.Size = new System.Drawing.Size(90, 47);
		this.btnEditaOV.TabIndex = 5;
		this.btnEditaOV.Text = "F4 - EDITA OV";
		this.btnEditaOV.UseVisualStyleBackColor = true;
		this.btnEditaOV.Click += new System.EventHandler(btnEditaOV_Click);
		this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.btnAddItem.Enabled = false;
		this.btnAddItem.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnAddItem.FlatAppearance.BorderSize = 2;
		this.btnAddItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
		this.btnAddItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
		this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAddItem.ForeColor = System.Drawing.Color.Black;
		this.btnAddItem.Image = (System.Drawing.Image)resources.GetObject("btnAddItem.Image");
		this.btnAddItem.Location = new System.Drawing.Point(25, 19);
		this.btnAddItem.Name = "btnAddItem";
		this.btnAddItem.Size = new System.Drawing.Size(206, 33);
		this.btnAddItem.TabIndex = 1;
		this.btnAddItem.Text = "AGREGAR PRODUCTO (F3)";
		this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAddItem.UseVisualStyleBackColor = true;
		this.btnAddItem.Click += new System.EventHandler(btnAddItem_Click);
		this.btnDeleteItem.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.btnDeleteItem.Enabled = false;
		this.btnDeleteItem.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnDeleteItem.FlatAppearance.BorderSize = 2;
		this.btnDeleteItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDeleteItem.Image = (System.Drawing.Image)resources.GetObject("btnDeleteItem.Image");
		this.btnDeleteItem.Location = new System.Drawing.Point(25, 55);
		this.btnDeleteItem.Name = "btnDeleteItem";
		this.btnDeleteItem.Size = new System.Drawing.Size(206, 33);
		this.btnDeleteItem.TabIndex = 2;
		this.btnDeleteItem.Text = "ELIMINAR PRODUCTO (F2)";
		this.btnDeleteItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnDeleteItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnDeleteItem.UseVisualStyleBackColor = true;
		this.btnDeleteItem.Click += new System.EventHandler(btnDeleteItem_Click);
		this.btnAnulaOV.Enabled = false;
		this.btnAnulaOV.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnAnulaOV.FlatAppearance.BorderSize = 2;
		this.btnAnulaOV.Location = new System.Drawing.Point(192, 94);
		this.btnAnulaOV.Name = "btnAnulaOV";
		this.btnAnulaOV.Size = new System.Drawing.Size(56, 46);
		this.btnAnulaOV.TabIndex = 4;
		this.btnAnulaOV.Text = "ANULA \r\n OV";
		this.btnAnulaOV.UseVisualStyleBackColor = true;
		this.btnAnulaOV.Click += new System.EventHandler(btnAnulaOV_Click);
		this.btnInicioOV.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnInicioOV.FlatAppearance.BorderSize = 2;
		this.btnInicioOV.Location = new System.Drawing.Point(2, 93);
		this.btnInicioOV.Name = "btnInicioOV";
		this.btnInicioOV.Size = new System.Drawing.Size(86, 47);
		this.btnInicioOV.TabIndex = 0;
		this.btnInicioOV.Text = "F6 INICIA OV";
		this.btnInicioOV.UseVisualStyleBackColor = true;
		this.btnInicioOV.Click += new System.EventHandler(btnInicioOV_Click);
		this.groupBox2.Controls.Add(this.chkFactura);
		this.groupBox2.Controls.Add(this.chkBoleta);
		this.groupBox2.Enabled = false;
		this.groupBox2.Location = new System.Drawing.Point(3, 148);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(253, 33);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.chkFactura.AutoSize = true;
		this.chkFactura.Enabled = false;
		this.chkFactura.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
		this.chkFactura.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkFactura.Location = new System.Drawing.Point(133, 9);
		this.chkFactura.Name = "chkFactura";
		this.chkFactura.Size = new System.Drawing.Size(81, 18);
		this.chkFactura.TabIndex = 1;
		this.chkFactura.Text = "FACTURA";
		this.chkFactura.UseVisualStyleBackColor = true;
		this.chkFactura.Click += new System.EventHandler(chkFactura_Click);
		this.chkBoleta.AutoSize = true;
		this.chkBoleta.Checked = true;
		this.chkBoleta.Enabled = false;
		this.chkBoleta.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
		this.chkBoleta.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkBoleta.Location = new System.Drawing.Point(37, 9);
		this.chkBoleta.Name = "chkBoleta";
		this.chkBoleta.Size = new System.Drawing.Size(73, 18);
		this.chkBoleta.TabIndex = 0;
		this.chkBoleta.TabStop = true;
		this.chkBoleta.Text = "BOLETA";
		this.chkBoleta.UseVisualStyleBackColor = true;
		this.chkBoleta.Click += new System.EventHandler(chkBoleta_Click);
		this.btnSalir.Enabled = false;
		this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnSalir.FlatAppearance.BorderSize = 2;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(109, 121);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(100, 43);
		this.btnSalir.TabIndex = 6;
		this.btnSalir.Text = "CERRAR OV F9";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnGuardaOV.Enabled = false;
		this.btnGuardaOV.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnGuardaOV.FlatAppearance.BorderSize = 2;
		this.btnGuardaOV.Image = (System.Drawing.Image)resources.GetObject("btnGuardaOV.Image");
		this.btnGuardaOV.Location = new System.Drawing.Point(4, 122);
		this.btnGuardaOV.Name = "btnGuardaOV";
		this.btnGuardaOV.Size = new System.Drawing.Size(99, 43);
		this.btnGuardaOV.TabIndex = 7;
		this.btnGuardaOV.Text = "GUARDA OV F8";
		this.btnGuardaOV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardaOV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardaOV.UseVisualStyleBackColor = true;
		this.btnGuardaOV.Click += new System.EventHandler(btnGuardaOV_Click);
		this.groupBox4.Controls.Add(this.txtNombreVendedor);
		this.groupBox4.Controls.Add(this.txtCodigoVendedor);
		this.groupBox4.Controls.Add(this.label14);
		this.groupBox4.Controls.Add(this.btnImprimirTicket);
		this.groupBox4.Controls.Add(this.txtPDescuento);
		this.groupBox4.Controls.Add(this.txtBruto);
		this.groupBox4.Controls.Add(this.txtDscto);
		this.groupBox4.Controls.Add(this.label2);
		this.groupBox4.Controls.Add(this.label13);
		this.groupBox4.Controls.Add(this.pbCapchatS);
		this.groupBox4.Controls.Add(this.txtSunat_Capchat);
		this.groupBox4.Controls.Add(this.dtpFechaPago);
		this.groupBox4.Controls.Add(this.cmbFormaPago);
		this.groupBox4.Controls.Add(this.txtDireccion);
		this.groupBox4.Controls.Add(this.txtNombreCliente);
		this.groupBox4.Controls.Add(this.txtCodCliente);
		this.groupBox4.Controls.Add(this.chkVentaDsctoGlobal);
		this.groupBox4.Controls.Add(this.chkVentaGratuita);
		this.groupBox4.Controls.Add(this.dtpFecha);
		this.groupBox4.Controls.Add(this.label7);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Controls.Add(this.label5);
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Enabled = false;
		this.groupBox4.Location = new System.Drawing.Point(15, 312);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(528, 183);
		this.groupBox4.TabIndex = 4;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Datos del Cliente";
		this.txtNombreVendedor.Location = new System.Drawing.Point(195, 99);
		this.txtNombreVendedor.Name = "txtNombreVendedor";
		this.txtNombreVendedor.ReadOnly = true;
		this.txtNombreVendedor.Size = new System.Drawing.Size(320, 20);
		this.txtNombreVendedor.TabIndex = 131;
		this.txtNombreVendedor.Text = "<--  SELECCIONE UN VENDEDOR";
		this.superValidator1.SetValidator1(this.txtNombreVendedor, this.requiredFieldValidator1);
		this.txtCodigoVendedor.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.btVendedor.SetBalloonCaption(this.txtCodigoVendedor, "BUSCA DE VENDEDOR");
		this.btVendedor.SetBalloonText(this.txtCodigoVendedor, "PRESIONE F1 PARA BUSCAR VENDEDORES O DIGITE EL CÓDIGO DEL VENDEDOR Y PRESIONE ENTER");
		this.txtCodigoVendedor.Location = new System.Drawing.Point(134, 99);
		this.txtCodigoVendedor.Name = "txtCodigoVendedor";
		this.txtCodigoVendedor.Size = new System.Drawing.Size(39, 20);
		this.txtCodigoVendedor.TabIndex = 130;
		this.superValidator1.SetValidator1(this.txtCodigoVendedor, this.requiredFieldValidator4);
		this.txtCodigoVendedor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodigoVendedor_KeyDown);
		this.txtCodigoVendedor.KeyUp += new System.Windows.Forms.KeyEventHandler(txtCodigoVendedor_KeyUp);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(17, 102);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(110, 13);
		this.label14.TabIndex = 129;
		this.label14.Text = "VENDEDOR (F1) :";
		this.btnImprimirTicket.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnImprimirTicket.ImageIndex = 7;
		this.btnImprimirTicket.ImageList = this.imageList2;
		this.btnImprimirTicket.Location = new System.Drawing.Point(440, 7);
		this.btnImprimirTicket.Name = "btnImprimirTicket";
		this.btnImprimirTicket.Size = new System.Drawing.Size(75, 31);
		this.btnImprimirTicket.TabIndex = 128;
		this.btnImprimirTicket.Text = "Tickets";
		this.btnImprimirTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimirTicket.UseVisualStyleBackColor = true;
		this.btnImprimirTicket.Visible = false;
		this.btnImprimirTicket.Click += new System.EventHandler(btnImprimirTicket_Click);
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
		this.txtPDescuento.Location = new System.Drawing.Point(479, 157);
		this.txtPDescuento.Name = "txtPDescuento";
		this.txtPDescuento.Size = new System.Drawing.Size(46, 20);
		this.txtPDescuento.TabIndex = 126;
		this.txtPDescuento.Tag = "7";
		this.txtPDescuento.Visible = false;
		this.txtPDescuento.TextChanged += new System.EventHandler(txtPDescuento_TextChanged);
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(415, 125);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(62, 20);
		this.txtBruto.TabIndex = 122;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtBruto, this.customValidator6);
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(414, 157);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(63, 20);
		this.txtDscto.TabIndex = 123;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(376, 161);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(36, 13);
		this.label2.TabIndex = 125;
		this.label2.Text = "Dcto :";
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(376, 130);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(38, 13);
		this.label13.TabIndex = 124;
		this.label13.Text = "Bruto :";
		this.pbCapchatS.Location = new System.Drawing.Point(496, 124);
		this.pbCapchatS.Name = "pbCapchatS";
		this.pbCapchatS.Size = new System.Drawing.Size(19, 21);
		this.pbCapchatS.TabIndex = 121;
		this.pbCapchatS.TabStop = false;
		this.pbCapchatS.Visible = false;
		this.txtSunat_Capchat.Location = new System.Drawing.Point(479, 125);
		this.txtSunat_Capchat.Name = "txtSunat_Capchat";
		this.txtSunat_Capchat.Size = new System.Drawing.Size(16, 20);
		this.txtSunat_Capchat.TabIndex = 120;
		this.txtSunat_Capchat.Visible = false;
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(290, 156);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 119;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(134, 156);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(150, 20);
		this.cmbFormaPago.TabIndex = 118;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.Visible = false;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(134, 68);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(381, 20);
		this.txtDireccion.TabIndex = 117;
		this.txtDireccion.Tag = "21";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Enabled = false;
		this.txtNombreCliente.Location = new System.Drawing.Point(134, 41);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.Size = new System.Drawing.Size(381, 20);
		this.txtNombreCliente.TabIndex = 116;
		this.txtNombreCliente.Tag = "3";
		this.superValidator1.SetValidator1(this.txtNombreCliente, this.requiredFieldValidator3);
		this.txtNombreCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNombreCliente_KeyPress);
		this.txtCodCliente.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Location = new System.Drawing.Point(134, 14);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(109, 20);
		this.txtCodCliente.TabIndex = 115;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.requiredFieldValidator2);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyUp);
		this.chkVentaDsctoGlobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaDsctoGlobal.AutoSize = true;
		this.chkVentaDsctoGlobal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.chkVentaDsctoGlobal.Location = new System.Drawing.Point(336, 18);
		this.chkVentaDsctoGlobal.Name = "chkVentaDsctoGlobal";
		this.chkVentaDsctoGlobal.Size = new System.Drawing.Size(98, 15);
		this.chkVentaDsctoGlobal.TabIndex = 114;
		this.chkVentaDsctoGlobal.Text = "Vta. Descuento";
		this.chkVentaDsctoGlobal.CheckedChanged += new System.EventHandler(chkVentaDsctoGlobal_CheckedChanged);
		this.chkVentaDsctoGlobal.Click += new System.EventHandler(chkVentaDsctoGlobal_Click);
		this.chkVentaGratuita.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaGratuita.AutoSize = true;
		this.chkVentaGratuita.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.chkVentaGratuita.Location = new System.Drawing.Point(246, 18);
		this.chkVentaGratuita.Name = "chkVentaGratuita";
		this.chkVentaGratuita.Size = new System.Drawing.Size(84, 15);
		this.chkVentaGratuita.TabIndex = 111;
		this.chkVentaGratuita.Text = "Vta. Gratuita";
		this.chkVentaGratuita.CheckedChanged += new System.EventHandler(chkVentaGratuita_CheckedChanged);
		this.chkVentaGratuita.Click += new System.EventHandler(chkVentaGratuita_Click);
		this.dtpFecha.Location = new System.Drawing.Point(134, 128);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(237, 20);
		this.dtpFecha.TabIndex = 8;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(17, 159);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(108, 13);
		this.label7.TabIndex = 4;
		this.label7.Text = "FORMA PAGO    :";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(17, 131);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(109, 13);
		this.label6.TabIndex = 3;
		this.label6.Text = "FECHA DOC.      :";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(17, 17);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(113, 13);
		this.label5.TabIndex = 2;
		this.label5.Text = "RUC/DNI Nº (F1) :";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(17, 71);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(107, 13);
		this.label4.TabIndex = 1;
		this.label4.Text = "DIRECCION       :";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(17, 44);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(107, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "CLIENTE           :";
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
		this.panel2.Location = new System.Drawing.Point(764, 252);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(258, 175);
		this.panel2.TabIndex = 8;
		this.txtinafectas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtinafectas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtinafectas.Location = new System.Drawing.Point(173, 36);
		this.txtinafectas.Name = "txtinafectas";
		this.txtinafectas.ReadOnly = true;
		this.txtinafectas.Size = new System.Drawing.Size(67, 18);
		this.txtinafectas.TabIndex = 46;
		this.txtinafectas.Tag = "7";
		this.txtinafectas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label26.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label26.Location = new System.Drawing.Point(139, 39);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(24, 12);
		this.label26.TabIndex = 44;
		this.label26.Tag = "7";
		this.label26.Text = "Ifts :";
		this.txtexoneradas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtexoneradas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtexoneradas.Location = new System.Drawing.Point(173, 8);
		this.txtexoneradas.Name = "txtexoneradas";
		this.txtexoneradas.ReadOnly = true;
		this.txtexoneradas.Size = new System.Drawing.Size(67, 18);
		this.txtexoneradas.TabIndex = 45;
		this.txtexoneradas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label32.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label32.Location = new System.Drawing.Point(139, 11);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(31, 12);
		this.label32.TabIndex = 43;
		this.label32.Text = "Exds :";
		this.txtgratuitas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgratuitas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgratuitas.Location = new System.Drawing.Point(52, 37);
		this.txtgratuitas.Name = "txtgratuitas";
		this.txtgratuitas.ReadOnly = true;
		this.txtgratuitas.Size = new System.Drawing.Size(67, 18);
		this.txtgratuitas.TabIndex = 42;
		this.txtgratuitas.Tag = "7";
		this.txtgratuitas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(17, 40);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(33, 12);
		this.label19.TabIndex = 40;
		this.label19.Tag = "7";
		this.label19.Text = "Grtas :";
		this.txtgravadas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgravadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgravadas.Location = new System.Drawing.Point(52, 9);
		this.txtgravadas.Name = "txtgravadas";
		this.txtgravadas.ReadOnly = true;
		this.txtgravadas.Size = new System.Drawing.Size(67, 18);
		this.txtgravadas.TabIndex = 41;
		this.txtgravadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(14, 12);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(33, 12);
		this.label22.TabIndex = 39;
		this.label22.Text = "Gvds :";
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(105, 141);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(122, 20);
		this.txtPrecioVenta.TabIndex = 8;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtPrecioVenta, this.customValidator5);
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(105, 105);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(122, 20);
		this.txtIGV.TabIndex = 7;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(105, 70);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(122, 20);
		this.txtValorVenta.TabIndex = 6;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(41, 143);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(65, 16);
		this.label10.TabIndex = 2;
		this.label10.Text = "TOTAL :";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(63, 107);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(41, 16);
		this.label9.TabIndex = 1;
		this.label9.Text = "IGV :";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(9, 72);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(96, 16);
		this.label8.TabIndex = 0;
		this.label8.Text = "SUBTOTAL :";
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.label11);
		this.panel3.Location = new System.Drawing.Point(764, 432);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(258, 55);
		this.panel3.TabIndex = 9;
		this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(panel3_Paint);
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(5, 7);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(117, 42);
		this.label11.TabIndex = 10;
		this.label11.Text = "00.00";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbDocumento.AutoSize = true;
		this.lbDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDocumento.Location = new System.Drawing.Point(120, 11);
		this.lbDocumento.Name = "lbDocumento";
		this.lbDocumento.Size = new System.Drawing.Size(145, 29);
		this.lbDocumento.TabIndex = 42;
		this.lbDocumento.Tag = "22";
		this.lbDocumento.Text = "Documento";
		this.lbDocumento.Visible = false;
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(58, 12);
		this.txtDocRef.Multiline = true;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(46, 28);
		this.txtDocRef.TabIndex = 40;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.Text = "L";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(5, 20);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(53, 13);
		this.label12.TabIndex = 41;
		this.label12.Text = "Doc. Ref.";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(838, 7);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(31, 42);
		this.label1.TabIndex = 43;
		this.label1.Tag = "22";
		this.label1.Text = "-";
		this.label1.Visible = false;
		this.txtSerie.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtSerie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.ForeColor = System.Drawing.Color.Red;
		this.txtSerie.Location = new System.Drawing.Point(773, 12);
		this.txtSerie.Margin = new System.Windows.Forms.Padding(5);
		this.txtSerie.Multiline = true;
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(65, 37);
		this.txtSerie.TabIndex = 45;
		this.txtSerie.Text = "000";
		this.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtPedido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPedido.ForeColor = System.Drawing.Color.Red;
		this.txtPedido.Location = new System.Drawing.Point(865, 12);
		this.txtPedido.Multiline = true;
		this.txtPedido.Name = "txtPedido";
		this.txtPedido.ReadOnly = true;
		this.txtPedido.Size = new System.Drawing.Size(152, 37);
		this.txtPedido.TabIndex = 46;
		this.txtPedido.Text = "00000000";
		this.txtPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.highlighter1.ContainerControl = this;
		this.highlighter1.FocusHighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.requiredFieldValidator1.ErrorMessage = "DEBE SELECCIONAR UN VENDEDOR";
		this.requiredFieldValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.requiredFieldValidator4.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ErrorMessage = "Your error message here.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.requiredFieldValidator3.ErrorMessage = "SELECCIONE UN CLIENTE";
		this.requiredFieldValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.requiredFieldValidator2.ErrorMessage = "SELECCIONE UN CLIENTE";
		this.requiredFieldValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ErrorMessage = "Your error message here.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.txtCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodigoBarras.Location = new System.Drawing.Point(617, 12);
		this.txtCodigoBarras.Multiline = true;
		this.txtCodigoBarras.Name = "txtCodigoBarras";
		this.txtCodigoBarras.ReadOnly = true;
		this.txtCodigoBarras.Size = new System.Drawing.Size(42, 37);
		this.txtCodigoBarras.TabIndex = 129;
		this.txtCodigoBarras.Text = "l";
		this.txtCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.groupBox6.Controls.Add(this.btnSalir);
		this.groupBox6.Controls.Add(this.btnCancelarOV);
		this.groupBox6.Controls.Add(this.groupBox5);
		this.groupBox6.Controls.Add(this.btnGuardaOV);
		this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox6.Location = new System.Drawing.Point(545, 325);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(215, 170);
		this.groupBox6.TabIndex = 130;
		this.groupBox6.TabStop = false;
		this.btnCancelarOV.Enabled = false;
		this.btnCancelarOV.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnCancelarOV.FlatAppearance.BorderSize = 2;
		this.btnCancelarOV.Image = (System.Drawing.Image)resources.GetObject("btnCancelarOV.Image");
		this.btnCancelarOV.Location = new System.Drawing.Point(108, 121);
		this.btnCancelarOV.Name = "btnCancelarOV";
		this.btnCancelarOV.Size = new System.Drawing.Size(100, 43);
		this.btnCancelarOV.TabIndex = 133;
		this.btnCancelarOV.Text = "CANCELA OV F9";
		this.btnCancelarOV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancelarOV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelarOV.UseVisualStyleBackColor = true;
		this.btnCancelarOV.Visible = false;
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox5.BackColor = System.Drawing.SystemColors.ScrollBar;
		this.groupBox5.Controls.Add(this.txttasa);
		this.groupBox5.Controls.Add(this.label30);
		this.groupBox5.Controls.Add(this.lbLineaCredito);
		this.groupBox5.Controls.Add(this.txtLineaCredito);
		this.groupBox5.Controls.Add(this.txtLineaCreditoUso);
		this.groupBox5.Controls.Add(this.label23);
		this.groupBox5.Controls.Add(this.label25);
		this.groupBox5.Controls.Add(this.txtLineaCreditoDisponible);
		this.groupBox5.Enabled = false;
		this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox5.Location = new System.Drawing.Point(4, 10);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(208, 102);
		this.groupBox5.TabIndex = 132;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Condiciones de Crédito:";
		this.txttasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txttasa.Location = new System.Drawing.Point(104, 82);
		this.txttasa.Name = "txttasa";
		this.txttasa.ReadOnly = true;
		this.txttasa.Size = new System.Drawing.Size(88, 18);
		this.txttasa.TabIndex = 106;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label30.Location = new System.Drawing.Point(30, 85);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(71, 12);
		this.label30.TabIndex = 105;
		this.label30.Text = "Tasa de Interés:";
		this.lbLineaCredito.AutoSize = true;
		this.lbLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbLineaCredito.Location = new System.Drawing.Point(7, 20);
		this.lbLineaCredito.Name = "lbLineaCredito";
		this.lbLineaCredito.Size = new System.Drawing.Size(94, 12);
		this.lbLineaCredito.TabIndex = 85;
		this.lbLineaCredito.Text = "Línea de Crédito (S/.):";
		this.txtLineaCredito.Enabled = false;
		this.txtLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCredito.Location = new System.Drawing.Point(104, 17);
		this.txtLineaCredito.Name = "txtLineaCredito";
		this.txtLineaCredito.ReadOnly = true;
		this.txtLineaCredito.Size = new System.Drawing.Size(88, 18);
		this.txtLineaCredito.TabIndex = 84;
		this.txtLineaCreditoUso.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoUso.Location = new System.Drawing.Point(104, 61);
		this.txtLineaCreditoUso.Name = "txtLineaCreditoUso";
		this.txtLineaCreditoUso.ReadOnly = true;
		this.txtLineaCreditoUso.Size = new System.Drawing.Size(88, 18);
		this.txtLineaCreditoUso.TabIndex = 98;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.Location = new System.Drawing.Point(6, 40);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(95, 12);
		this.label23.TabIndex = 95;
		this.label23.Text = "Línea Disponible (S/.):";
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label25.Location = new System.Drawing.Point(8, 64);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(93, 12);
		this.label25.TabIndex = 97;
		this.label25.Text = "Línea C. en Uso (S/.):";
		this.txtLineaCreditoDisponible.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoDisponible.Location = new System.Drawing.Point(104, 40);
		this.txtLineaCreditoDisponible.Name = "txtLineaCreditoDisponible";
		this.txtLineaCreditoDisponible.ReadOnly = true;
		this.txtLineaCreditoDisponible.Size = new System.Drawing.Size(88, 18);
		this.txtLineaCreditoDisponible.TabIndex = 96;
		this.btVendedor.DefaultBalloonWidth = 300;
		this.btVendedor.InitialDelay = 20;
		this.lblCantidadProductos.AutoSize = true;
		this.lblCantidadProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadProductos.Location = new System.Drawing.Point(545, 312);
		this.lblCantidadProductos.Name = "lblCantidadProductos";
		this.lblCantidadProductos.Size = new System.Drawing.Size(175, 16);
		this.lblCantidadProductos.TabIndex = 131;
		this.lblCantidadProductos.Text = "Productos Agregados: 0";
		this.chkVentaSinStock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaSinStock.AutoSize = true;
		this.chkVentaSinStock.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.chkVentaSinStock.Location = new System.Drawing.Point(664, 17);
		this.chkVentaSinStock.Name = "chkVentaSinStock";
		this.chkVentaSinStock.Size = new System.Drawing.Size(91, 15);
		this.chkVentaSinStock.TabIndex = 132;
		this.chkVentaSinStock.Text = "Vta. Sin Stock";
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "Código de Producto_";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.codproducto.Width = 80;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Código de Producto";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 80;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripción";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 300;
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
		this.unidad.Width = 130;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle16;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle17;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		dataGridViewCellStyle18.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle18;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Visible = false;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle19.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle19;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle20.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle20;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle21.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle21;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle22.Format = "N2";
		dataGridViewCellStyle22.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle22;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle23.Format = "N2";
		dataGridViewCellStyle23.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle23;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle24.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle24;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle25.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle25;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Width = 80;
		this.valoreal.DataPropertyName = "valoreal";
		dataGridViewCellStyle26.Format = "N2";
		dataGridViewCellStyle26.NullValue = null;
		this.valoreal.DefaultCellStyle = dataGridViewCellStyle26;
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.ReadOnly = true;
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.precioreal.DataPropertyName = "precioreal";
		dataGridViewCellStyle27.Format = "N2";
		dataGridViewCellStyle27.NullValue = null;
		this.precioreal.DefaultCellStyle = dataGridViewCellStyle27;
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
		this.TipoUnidad.HeaderText = "TipoUnidad";
		this.TipoUnidad.Name = "TipoUnidad";
		this.TipoUnidad.ReadOnly = true;
		this.TipoUnidad.Visible = false;
		this.CodEmpresa.DataPropertyName = "CodEmpresa";
		this.CodEmpresa.HeaderText = "CodEmpresa";
		this.CodEmpresa.Name = "CodEmpresa";
		this.CodEmpresa.ReadOnly = true;
		this.CodEmpresa.Visible = false;
		this.codlinea.DataPropertyName = "codlinea";
		this.codlinea.HeaderText = "codlinea";
		this.codlinea.Name = "codlinea";
		this.codfamilia.DataPropertyName = "codfamilia";
		this.codfamilia.HeaderText = "codfamilia";
		this.codfamilia.Name = "codfamilia";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1025, 491);
		base.Controls.Add(this.chkVentaSinStock);
		base.Controls.Add(this.lblCantidadProductos);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.txtCodigoBarras);
		base.Controls.Add(this.txtPedido);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSerie);
		base.Controls.Add(this.lbDocumento);
		base.Controls.Add(this.txtDocRef);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.Name = "frmOrdenVenta";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "     ";
		base.Load += new System.EventHandler(frmOrdenVenta_Load);
		base.Shown += new System.EventHandler(frmOrdenVenta_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmOrdenVenta_KeyDown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.panel1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox6.ResumeLayout(false);
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
