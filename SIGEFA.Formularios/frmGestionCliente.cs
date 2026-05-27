using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using FinalXML.Librerias;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Tesseract;

namespace SIGEFA.Formularios;

public class frmGestionCliente : Office2007Form
{
	public int Proceso = 0;

	private clsAdmCliente admCli = new clsAdmCliente();

	public clsCliente cli = new clsCliente();

	private bool Validacion = true;

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsLocalidad local = new clsLocalidad();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsAdmListaPrecio admLista = new clsAdmListaPrecio();

	private clsAdmZona AdmZona = new clsAdmZona();

	private clsZona zona = new clsZona();

	private clsAdmVendedor AdmVen = new clsAdmVendedor();

	private clsValidar ok = new clsValidar();

	private string CodPer = null;

	private clsMoneda moneda = new clsMoneda();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private Sunat MyInfoSunat;

	private Reniec MyInfoReniec;

	private IntRange red = new IntRange(0, 255);

	private IntRange green = new IntRange(0, 255);

	private IntRange blue = new IntRange(0, 255);

	private ReniecAPI reniecAPI = new ReniecAPI();

	private clsAdmCategoriaCliente admctgcliente = new clsAdmCategoriaCliente();

	private IContainer components = null;

	private ImageList imageList1;

	private System.Windows.Forms.TabControl tabControl1;

	private TabPage tabPage2;

	private TabPage tabPage1;

	private CheckBox cbActivo;

	private TextBox txtWeb;

	private Label label15;

	private TextBox txtEmail;

	private Label label13;

	private TextBox txtTelefono;

	private Label label14;

	private TextBox txtDireccionEntrega;

	private Label label12;

	private TextBox txtDireccionLegal;

	private Label label11;

	private ComboBox cbZona;

	private ComboBox cbDistrito;

	private ComboBox cbDepartamento;

	private ComboBox cbProvincia;

	private Label label10;

	private Label label9;

	private Label label8;

	private Label label7;

	private TextBox txtRazonSocial;

	private Label label4;

	private TextBox txtRUC;

	private TextBox txtCodigo;

	private Label label1;

	private TextBox txtFechaRegistro;

	private Label label17;

	private TextBox txtLineaCredito;

	private Label label18;

	private TextBox txtDscto;

	private Label label20;

	private Label label21;

	private ComboBox cbMoneda;

	private Label label22;

	private TextBox txtTelefonoContacto;

	private Label label24;

	private TextBox txtContacto;

	private Label label23;

	private TextBox txtComentario;

	private Label label25;

	private TextBox txtCtaCte;

	private Label label19;

	private TextBox txtBanco;

	private Label label16;

	private ComboBox cbCalificacion;

	private Label label27;

	private ComboBox cbFormaPago;

	private TextBox txtCodigoPersonalizado;

	private Label label28;

	private ComboBox cbListaPrecios;

	private Label label26;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private ComboBox cmbVendedores;

	private Label label5;

	private Label label29;

	private ComboBox cbPais;

	private Label label6;

	public CheckBox chbCliFacturasVencidas;

	private TextBox txttasa;

	private Label lbltasa;

	private Button btnCancelar;

	private Button btnAceptar;

	private CheckBox chkCliEspecial;

	private Label label2;

	private TextBox txtSunat_Capchat;

	private PictureBox pbCapchatS;

	private ComboBox cbctgcliente;

	private Label lblctgcliente;

	public frmGestionCliente()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		try
		{
			if (superValidator1.Validate())
			{
				if (cbFormaPago.SelectedIndex != 0 && Convert.ToDouble(txtLineaCredito.Text) == 0.0)
				{
					MessageBox.Show("Debe Ingresar linea de Credito", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					txtLineaCredito.Focus();
					return;
				}
				clsCliente cli = new clsCliente();
				cli.CodigoPersonalizado = txtCodigoPersonalizado.Text;
				if (txtRUC.Text.Length == 8)
				{
					cli.Dni = txtRUC.Text;
					cli.Ruc = null;
				}
				else if (txtRUC.Text.Length == 11)
				{
					cli.Ruc = txtRUC.Text;
					cli.Dni = null;
				}
				cli.RazonSocial = txtRazonSocial.Text;
				cli.Nombre = txtRazonSocial.Text;
				cli.DireccionLegal = txtDireccionLegal.Text;
				cli.DireccionEntrega = txtDireccionEntrega.Text;
				cli.Telefono = txtTelefono.Text;
				cli.Email = txtEmail.Text;
				cli.Web = txtWeb.Text;
				if (cbctgcliente.SelectedIndex != -1)
				{
					cli.codCategoriaCliente = Convert.ToInt32(cbctgcliente.SelectedValue);
				}
				if (cbDepartamento.SelectedIndex != -1)
				{
					cli.Departamento = cbDepartamento.SelectedValue.ToString();
				}
				if (cbProvincia.SelectedIndex != -1)
				{
					cli.Provincia = cbProvincia.SelectedValue.ToString();
				}
				if (cbDistrito.SelectedIndex != -1)
				{
					cli.Distrito = cbDistrito.SelectedValue.ToString();
				}
				if (cbZona.SelectedIndex != -1)
				{
					cli.Zona = Convert.ToInt32(cbZona.SelectedValue);
				}
				cli.Estado = cbActivo.Checked;
				if (txtDscto.Text != "")
				{
					cli.Descuento = Convert.ToDecimal(txtDscto.Text);
				}
				if (cbListaPrecios.SelectedIndex != -1)
				{
					cli.CodListaPrecio = Convert.ToInt32(cbListaPrecios.SelectedValue);
				}
				if (cmbVendedores.SelectedIndex != -1)
				{
					cli.CodVendedor = Convert.ToInt32(cmbVendedores.SelectedValue);
				}
				if (cbFormaPago.SelectedIndex != -1)
				{
					cli.FormaPago = Convert.ToInt32(cbFormaPago.SelectedValue);
				}
				if (cbMoneda.SelectedIndex != -1)
				{
					cli.Moneda = Convert.ToInt32(cbMoneda.SelectedValue);
				}
				if (txtLineaCredito.Text != "")
				{
					cli.LineaCredito = Convert.ToDecimal(txtLineaCredito.Text);
				}
				cli.Banco = txtBanco.Text;
				cli.CtaCte = txtCtaCte.Text;
				cli.Contacto = txtContacto.Text;
				cli.TelefonoContacto = txtTelefonoContacto.Text;
				if (cbCalificacion.SelectedIndex != -1)
				{
					cli.Calificacion = Convert.ToInt32(cbCalificacion.SelectedValue);
				}
				cli.Comentario = txtComentario.Text;
				cli.CodUser = frmLogin.iCodUser;
				cli.ClienteFacturasVencidas = chbCliFacturasVencidas.Checked;
				if (txttasa.Text != "")
				{
					cli.Tasa = Convert.ToInt32(txttasa.Text);
				}
				else
				{
					cli.Tasa = 0m;
				}
				cli.CliEspecial = chkCliEspecial.Checked;
				if (Proceso == 1 || Proceso == 3 || Proceso == 4)
				{
					if (admCli.insert(cli))
					{
						MessageBox.Show("Los datos se guardaron correctamente", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						if (Proceso == 1)
						{
							muestralista();
						}
						Close();
					}
				}
				else if (Proceso == 2)
				{
					cli.CodCliente = this.cli.CodCliente;
					cli.Habilitado = this.cli.Habilitado;
					if (admCli.update(cli))
					{
						MessageBox.Show("Los datos se guardaron correctamente", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						muestralista();
						Close();
					}
				}
			}
			else
			{
				MessageBox.Show("Debe completar todos los campos requeridos(*)", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void ValidarDatos(Control.ControlCollection Coleccion)
	{
		Validacion = true;
		foreach (Control c in Coleccion)
		{
			if (Convert.ToInt32(c.Tag) == 1 && c.Enabled && c.Text == "")
			{
				c.BackColor = Color.LightPink;
				c.Focus();
				Validacion = false;
			}
			if (c.HasChildren)
			{
				ValidarDatos(c.Controls);
			}
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmGestionCliente_Load(object sender, EventArgs e)
	{
		CargaMoneda();
		CargaFormaPagos();
		CargaListaPrecios();
		CargaZonas();
		cargaCombocategoriaclientes();
		CargaVendedores();
		CargaLocalidades(cbDepartamento, "000000", 1);
		if (Proceso == 1 || Proceso == 4)
		{
			CodPer = admCli.CodigoPersonalizado();
			generaCodPer(CodPer);
			cbActivo.Visible = false;
			if (Proceso == 4)
			{
				tabControl1.Controls.Remove(tabPage2);
			}
		}
		else if (Proceso == 2)
		{
			CargaCliente(cli.CodCliente);
		}
		else if (Proceso == 3)
		{
			CargaCliente(cli.CodCliente);
			ext.sololectura(tabControl1.Controls);
			btnAceptar.Visible = false;
			btnCancelar.Text = "Aceptar";
			btnCancelar.ImageIndex = 1;
		}
	}

	private void CargaMoneda()
	{
		cbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cbMoneda.DisplayMember = "descripcion";
		cbMoneda.ValueMember = "codMoneda";
		cbMoneda.SelectedIndex = 0;
	}

	private void generaCodPer(string codper)
	{
		if (char.IsLetter(codper.FirstOrDefault()))
		{
			codper = codper.Remove(0, 1);
		}
		string nuevocod = (Convert.ToInt32(codper) + 1).ToString().PadLeft(6, '0');
		txtCodigoPersonalizado.Text = "C" + nuevocod;
	}

	private void CargaVendedores()
	{
		cmbVendedores.DataSource = AdmVen.MuestraVendedoresDestaque();
		cmbVendedores.DisplayMember = "apellido";
		cmbVendedores.ValueMember = "codUsuario";
		cmbVendedores.SelectedIndex = -1;
	}

	private void CargaFormaPagos()
	{
		cbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cbFormaPago.DisplayMember = "descripcion";
		cbFormaPago.ValueMember = "codFormaPago";
		cbFormaPago.SelectedIndex = 0;
	}

	private void CargaZonas()
	{
		cbZona.DataSource = AdmZona.MuestraZonas();
		cbZona.DisplayMember = "descripcion";
		cbZona.ValueMember = "codZona";
		cbZona.SelectedIndex = -1;
	}

	public void cargaCombocategoriaclientes()
	{
		DataTable aux = admctgcliente.MuestraCategoriasCliente();
		cbctgcliente.DataSource = aux;
		cbctgcliente.DisplayMember = "descripcion";
		cbctgcliente.ValueMember = "codCategoriaCliente";
		cbctgcliente.SelectedIndex = -1;
	}

	private void CargaListaPrecios()
	{
	}

	private void CargaLocalidades(ComboBox Combo, string Padre, int Nivel)
	{
		Combo.DataSource = local.CargaLocalidades(Padre, Nivel);
		Combo.DisplayMember = "nombre";
		Combo.ValueMember = "codLocalidad";
		Combo.SelectedIndex = -1;
	}

	private void CargaCliente(int codCliente)
	{
		try
		{
			cli = admCli.MuestraCliente(codCliente);
			txtCodigo.Text = cli.CodCliente.ToString();
			txtCodigoPersonalizado.Text = cli.CodigoPersonalizado;
			txtRUC.Text = cli.RucDni;
			txtRazonSocial.Text = cli.RazonSocial;
			txtDireccionLegal.Text = cli.DireccionLegal;
			txtDireccionEntrega.Text = cli.DireccionEntrega;
			txtTelefono.Text = cli.Telefono;
			txtEmail.Text = cli.Email;
			txtWeb.Text = cli.Web;
			cbPais.SelectedValue = cli.Pais;
			cbDepartamento.SelectedValue = cli.Departamento;
			if (cli.codCategoriaCliente != 0)
			{
				cbctgcliente.SelectedValue = cli.codCategoriaCliente;
			}
			if (cli.Departamento != "")
			{
				cbDepartamento.SelectedValue = cli.Departamento;
				CargaLocalidades(cbProvincia, cli.Departamento.ToString(), 2);
				cbProvincia.Enabled = true;
				if (cli.Provincia != "")
				{
					cbProvincia.SelectedValue = cli.Provincia;
					CargaLocalidades(cbDistrito, cli.Provincia.ToString(), 3);
					cbDistrito.Enabled = true;
					cbDistrito.SelectedValue = cli.Distrito;
				}
			}
			cbZona.SelectedValue = cli.Zona;
			cbActivo.Checked = cli.Estado;
			txtDscto.Text = cli.Descuento.ToString();
			cbFormaPago.SelectedValue = cli.FormaPago;
			cbMoneda.SelectedValue = cli.Moneda;
			CargaListaPrecios();
			cmbVendedores.SelectedValue = cli.CodVendedor;
			txtLineaCredito.Text = cli.LineaCredito.ToString();
			txtBanco.Text = cli.Banco;
			txtCtaCte.Text = cli.CtaCte;
			txtContacto.Text = cli.Contacto;
			txtTelefonoContacto.Text = cli.TelefonoContacto;
			cbCalificacion.SelectedIndex = cli.Calificacion;
			txtComentario.Text = cli.Comentario;
			txtFechaRegistro.Text = cli.FechaRegistro.Date.ToShortDateString();
			if (cli.Habilitado)
			{
				label29.Visible = false;
			}
			else
			{
				label29.Visible = true;
			}
			chbCliFacturasVencidas.Checked = cli.ClienteFacturasVencidas;
			if (cli.Tasa != 0m)
			{
				txttasa.Text = cli.Tasa.ToString();
			}
			if (cli.CliEspecial)
			{
				chkCliEspecial.Checked = true;
			}
			else
			{
				chkCliEspecial.Checked = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void muestralista()
	{
		if (Application.OpenForms["frmClientesCompletos"] != null)
		{
			frmClientesCompletos form = (frmClientesCompletos)Application.OpenForms["frmClientesCompletos"];
			form.Activate();
			form.CargaLista();
		}
		else
		{
			frmClientesCompletos form2 = new frmClientesCompletos();
			form2.MdiParent = Application.OpenForms["mdi_Menu"];
			form2.Show();
		}
	}

	private void cbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLocalidades(cbProvincia, cbDepartamento.SelectedValue.ToString(), 2);
		cbProvincia.Enabled = true;
		cbProvincia.Focus();
	}

	private void cbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLocalidades(cbDistrito, cbProvincia.SelectedValue.ToString(), 3);
		cbDistrito.Enabled = true;
		cbDistrito.Focus();
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

	private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar != '\r')
		{
			return;
		}
		try
		{
			switch (txtRUC.Text.Length)
			{
			case 1:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo un digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 2:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo dos digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 3:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo tres digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 4:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cuatro digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 5:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cinco digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 6:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo seis digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 7:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo siete digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 8:
				cli = admCli.ConsultaCliente(txtRUC.Text);
				if (cli != null && Proceso != 2)
				{
					MessageBox.Show("El Numero de Documento Ingresado" + Environment.NewLine + "Ya se Encuentra Registrado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					CargaDNI(txtRUC.Text);
				}
				break;
			case 9:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso nueve digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			case 10:
				MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso diez digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUC.SelectAll();
				txtRUC.Focus();
				break;
			case 11:
				cli = admCli.ConsultaCliente(txtRUC.Text);
				if (cli != null && Proceso != 2)
				{
					MessageBox.Show("El Numero de Documento Ingresado" + Environment.NewLine + "Ya se Encuentra Registrado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				}
				CargarImagenSunat();
				valida_ruc(txtRUC.Text);
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

	public void valida_ruc(string ruc)
	{
		try
		{
			reniecAPI = new ReniecAPI();
			string result = reniecAPI.GetInfoRuc(ruc);
			string[] array = result.Split('|');
			txtRazonSocial.Text = array[0];
			txtDireccionLegal.Text = array[1];
			txtDireccionLegal.Focus();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void ValidaLongitud()
	{
		if (txtRUC.Text.Length == 0)
		{
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ningun digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (txtRUC.Text.Length > 11)
		{
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ha Ingresado " + txtRUC.Text.Length + " Digitos", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtRUC.SelectAll();
			txtRUC.Focus();
		}
	}

	public void CargaDNI(string dni)
	{
		try
		{
			ReniecAPI reniecAPI = new ReniecAPI();
			string result = reniecAPI.GetInfo(dni);
			string[] array = result.Split('|');
			txtRazonSocial.Text = ((array.Length != 0) ? array[0] : "");
			txtDireccionLegal.Text = ((1 < array.Length) ? array[1] : "");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "ERROR API RENIEC - frmVenta2019");
		}
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

	public void CargarImagenReniec()
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
		txtRazonSocial.Text = "";
		txtSunat_Capchat.Text = string.Empty;
	}

	private void BloqueaDatos()
	{
		txtDireccionLegal.ReadOnly = true;
		txtRazonSocial.ReadOnly = true;
	}

	private void txtLineaCredito_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void cbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaListaPrecios();
	}

	private void frmGestionCliente_Shown(object sender, EventArgs e)
	{
		txtRazonSocial.Focus();
	}

	private void txtRazonSocial_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtDireccionLegal.Focus();
		}
	}

	private void txtDireccionEntrega_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cbDepartamento.Focus();
		}
	}

	private void txtDni_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtTelefono.Focus();
		}
	}

	private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtEmail.Focus();
		}
	}

	private void txtEmail_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtWeb.Focus();
		}
	}

	private void txtWeb_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cbZona.Focus();
		}
	}

	private void cbZona_SelectionChangeCommitted(object sender, EventArgs e)
	{
		btnAceptar.Focus();
	}

	private void cbPais_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cbDepartamento.Focus();
	}

	private void cbDistrito_SelectionChangeCommitted(object sender, EventArgs e)
	{
		txtRUC.Focus();
	}

	private void txtDireccionLegal_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtDireccionEntrega.Focus();
		}
	}

	private void txtRUC_TextChanged(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionCliente));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.label2 = new System.Windows.Forms.Label();
		this.chkCliEspecial = new System.Windows.Forms.CheckBox();
		this.txtCodigoPersonalizado = new System.Windows.Forms.TextBox();
		this.label28 = new System.Windows.Forms.Label();
		this.cbActivo = new System.Windows.Forms.CheckBox();
		this.txtWeb = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtEmail = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtDireccionEntrega = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtDireccionLegal = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.cbZona = new System.Windows.Forms.ComboBox();
		this.cbDistrito = new System.Windows.Forms.ComboBox();
		this.cbDepartamento = new System.Windows.Forms.ComboBox();
		this.cbProvincia = new System.Windows.Forms.ComboBox();
		this.cbPais = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.txtRazonSocial = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtRUC = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.txttasa = new System.Windows.Forms.TextBox();
		this.lbltasa = new System.Windows.Forms.Label();
		this.chbCliFacturasVencidas = new System.Windows.Forms.CheckBox();
		this.cmbVendedores = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.cbListaPrecios = new System.Windows.Forms.ComboBox();
		this.label26 = new System.Windows.Forms.Label();
		this.cbFormaPago = new System.Windows.Forms.ComboBox();
		this.cbCalificacion = new System.Windows.Forms.ComboBox();
		this.label27 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label25 = new System.Windows.Forms.Label();
		this.txtCtaCte = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.txtBanco = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtTelefonoContacto = new System.Windows.Forms.TextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.txtContacto = new System.Windows.Forms.TextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.cbMoneda = new System.Windows.Forms.ComboBox();
		this.label22 = new System.Windows.Forms.Label();
		this.txtFechaRegistro = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtLineaCredito = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.label29 = new System.Windows.Forms.Label();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.txtSunat_Capchat = new System.Windows.Forms.TextBox();
		this.pbCapchatS = new System.Windows.Forms.PictureBox();
		this.cbctgcliente = new System.Windows.Forms.ComboBox();
		this.lblctgcliente = new System.Windows.Forms.Label();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.imageList1.Images.SetKeyName(3, "sunat (1).png");
		this.imageList1.Images.SetKeyName(4, "sunat.png");
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(12, 12);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(633, 275);
		this.tabControl1.TabIndex = 1;
		this.tabPage1.Controls.Add(this.cbctgcliente);
		this.tabPage1.Controls.Add(this.lblctgcliente);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.chkCliEspecial);
		this.tabPage1.Controls.Add(this.txtCodigoPersonalizado);
		this.tabPage1.Controls.Add(this.label28);
		this.tabPage1.Controls.Add(this.cbActivo);
		this.tabPage1.Controls.Add(this.txtWeb);
		this.tabPage1.Controls.Add(this.label15);
		this.tabPage1.Controls.Add(this.txtEmail);
		this.tabPage1.Controls.Add(this.label13);
		this.tabPage1.Controls.Add(this.txtTelefono);
		this.tabPage1.Controls.Add(this.label14);
		this.tabPage1.Controls.Add(this.txtDireccionEntrega);
		this.tabPage1.Controls.Add(this.label12);
		this.tabPage1.Controls.Add(this.txtDireccionLegal);
		this.tabPage1.Controls.Add(this.label11);
		this.tabPage1.Controls.Add(this.cbZona);
		this.tabPage1.Controls.Add(this.cbDistrito);
		this.tabPage1.Controls.Add(this.cbDepartamento);
		this.tabPage1.Controls.Add(this.cbProvincia);
		this.tabPage1.Controls.Add(this.cbPais);
		this.tabPage1.Controls.Add(this.label10);
		this.tabPage1.Controls.Add(this.label9);
		this.tabPage1.Controls.Add(this.label8);
		this.tabPage1.Controls.Add(this.label7);
		this.tabPage1.Controls.Add(this.label6);
		this.tabPage1.Controls.Add(this.txtRazonSocial);
		this.tabPage1.Controls.Add(this.label4);
		this.tabPage1.Controls.Add(this.txtRUC);
		this.tabPage1.Controls.Add(this.txtCodigo);
		this.tabPage1.Controls.Add(this.label1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(625, 249);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Datos Generales";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(428, 46);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(60, 13);
		this.label2.TabIndex = 34;
		this.label2.Text = "RUC/DNI :";
		this.chkCliEspecial.AutoSize = true;
		this.chkCliEspecial.Location = new System.Drawing.Point(18, 216);
		this.chkCliEspecial.Name = "chkCliEspecial";
		this.chkCliEspecial.Size = new System.Drawing.Size(101, 17);
		this.chkCliEspecial.TabIndex = 33;
		this.chkCliEspecial.Text = "Cliente Especial";
		this.chkCliEspecial.UseVisualStyleBackColor = true;
		this.txtCodigoPersonalizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigoPersonalizado.Location = new System.Drawing.Point(294, 17);
		this.txtCodigoPersonalizado.Name = "txtCodigoPersonalizado";
		this.txtCodigoPersonalizado.Size = new System.Drawing.Size(100, 20);
		this.txtCodigoPersonalizado.TabIndex = 2;
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(173, 20);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(115, 13);
		this.label28.TabIndex = 32;
		this.label28.Text = "Codigo Personalizado :";
		this.cbActivo.AutoSize = true;
		this.cbActivo.Checked = true;
		this.cbActivo.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbActivo.Location = new System.Drawing.Point(489, 19);
		this.cbActivo.Name = "cbActivo";
		this.cbActivo.Size = new System.Drawing.Size(73, 17);
		this.cbActivo.TabIndex = 1;
		this.cbActivo.Text = "Habilitado";
		this.cbActivo.UseVisualStyleBackColor = true;
		this.cbActivo.Visible = false;
		this.txtWeb.Location = new System.Drawing.Point(489, 120);
		this.txtWeb.Name = "txtWeb";
		this.txtWeb.Size = new System.Drawing.Size(121, 20);
		this.txtWeb.TabIndex = 14;
		this.txtWeb.KeyDown += new System.Windows.Forms.KeyEventHandler(txtWeb_KeyDown);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(428, 123);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(36, 13);
		this.label15.TabIndex = 28;
		this.label15.Text = "Web :";
		this.txtEmail.Location = new System.Drawing.Point(489, 94);
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.Size = new System.Drawing.Size(121, 20);
		this.txtEmail.TabIndex = 13;
		this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(txtEmail_KeyDown);
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(428, 97);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(41, 13);
		this.label13.TabIndex = 26;
		this.label13.Text = "E-mail :";
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(489, 68);
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(121, 20);
		this.txtTelefono.TabIndex = 12;
		this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTelefono_KeyDown);
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(428, 71);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(55, 13);
		this.label14.TabIndex = 24;
		this.label14.Text = "Teléfono :";
		this.txtDireccionEntrega.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccionEntrega.Location = new System.Drawing.Point(118, 121);
		this.txtDireccionEntrega.Name = "txtDireccionEntrega";
		this.txtDireccionEntrega.Size = new System.Drawing.Size(276, 20);
		this.txtDireccionEntrega.TabIndex = 5;
		this.txtDireccionEntrega.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDireccionEntrega_KeyDown);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(14, 124);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(98, 13);
		this.label12.TabIndex = 22;
		this.label12.Text = "Dirección Entrega :";
		this.txtDireccionLegal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccionLegal.Location = new System.Drawing.Point(118, 95);
		this.txtDireccionLegal.Name = "txtDireccionLegal";
		this.txtDireccionLegal.Size = new System.Drawing.Size(276, 20);
		this.txtDireccionLegal.TabIndex = 4;
		this.txtDireccionLegal.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDireccionLegal_KeyDown);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(14, 98);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(87, 13);
		this.label11.TabIndex = 20;
		this.label11.Text = "Dirección Legal :";
		this.cbZona.FormattingEnabled = true;
		this.cbZona.Location = new System.Drawing.Point(489, 147);
		this.cbZona.Name = "cbZona";
		this.cbZona.Size = new System.Drawing.Size(121, 21);
		this.cbZona.TabIndex = 15;
		this.cbZona.SelectionChangeCommitted += new System.EventHandler(cbZona_SelectionChangeCommitted);
		this.cbDistrito.Enabled = false;
		this.cbDistrito.FormattingEnabled = true;
		this.cbDistrito.Location = new System.Drawing.Point(273, 174);
		this.cbDistrito.Name = "cbDistrito";
		this.cbDistrito.Size = new System.Drawing.Size(121, 21);
		this.cbDistrito.TabIndex = 9;
		this.cbDistrito.SelectionChangeCommitted += new System.EventHandler(cbDistrito_SelectionChangeCommitted);
		this.cbDepartamento.FormattingEnabled = true;
		this.cbDepartamento.Location = new System.Drawing.Point(273, 147);
		this.cbDepartamento.Name = "cbDepartamento";
		this.cbDepartamento.Size = new System.Drawing.Size(121, 21);
		this.cbDepartamento.TabIndex = 8;
		this.cbDepartamento.SelectionChangeCommitted += new System.EventHandler(cbDepartamento_SelectionChangeCommitted);
		this.cbProvincia.Enabled = false;
		this.cbProvincia.FormattingEnabled = true;
		this.cbProvincia.Location = new System.Drawing.Point(77, 174);
		this.cbProvincia.Name = "cbProvincia";
		this.cbProvincia.Size = new System.Drawing.Size(99, 21);
		this.cbProvincia.TabIndex = 7;
		this.cbProvincia.SelectionChangeCommitted += new System.EventHandler(cbProvincia_SelectionChangeCommitted);
		this.cbPais.FormattingEnabled = true;
		this.cbPais.Location = new System.Drawing.Point(77, 147);
		this.cbPais.Name = "cbPais";
		this.cbPais.Size = new System.Drawing.Size(99, 21);
		this.cbPais.TabIndex = 6;
		this.cbPais.Visible = false;
		this.cbPais.SelectionChangeCommitted += new System.EventHandler(cbPais_SelectionChangeCommitted);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(428, 150);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 14;
		this.label10.Text = "Zona :";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(187, 177);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(45, 13);
		this.label9.TabIndex = 13;
		this.label9.Text = "Distrito :";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(187, 150);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(80, 13);
		this.label8.TabIndex = 12;
		this.label8.Text = "Departamento :";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(14, 177);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(57, 13);
		this.label7.TabIndex = 11;
		this.label7.Text = "Provincia :";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(14, 150);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(33, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Pais :";
		this.label6.Visible = false;
		this.txtRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRazonSocial.Location = new System.Drawing.Point(118, 43);
		this.txtRazonSocial.Multiline = true;
		this.txtRazonSocial.Name = "txtRazonSocial";
		this.txtRazonSocial.Size = new System.Drawing.Size(276, 42);
		this.txtRazonSocial.TabIndex = 3;
		this.txtRazonSocial.Tag = "1";
		this.superValidator1.SetValidator1(this.txtRazonSocial, this.customValidator1);
		this.txtRazonSocial.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRazonSocial_KeyDown);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(15, 46);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(98, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Nombre/R. Social :";
		this.txtRUC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRUC.Location = new System.Drawing.Point(489, 43);
		this.txtRUC.MaxLength = 11;
		this.txtRUC.Name = "txtRUC";
		this.txtRUC.Size = new System.Drawing.Size(121, 20);
		this.txtRUC.TabIndex = 10;
		this.txtRUC.Tag = "1";
		this.txtRUC.TextChanged += new System.EventHandler(txtRUC_TextChanged);
		this.txtRUC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRUC_KeyPress);
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(67, 17);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(100, 20);
		this.txtCodigo.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Codigo :";
		this.tabPage2.Controls.Add(this.txttasa);
		this.tabPage2.Controls.Add(this.lbltasa);
		this.tabPage2.Controls.Add(this.chbCliFacturasVencidas);
		this.tabPage2.Controls.Add(this.cmbVendedores);
		this.tabPage2.Controls.Add(this.label5);
		this.tabPage2.Controls.Add(this.cbListaPrecios);
		this.tabPage2.Controls.Add(this.label26);
		this.tabPage2.Controls.Add(this.cbFormaPago);
		this.tabPage2.Controls.Add(this.cbCalificacion);
		this.tabPage2.Controls.Add(this.label27);
		this.tabPage2.Controls.Add(this.txtComentario);
		this.tabPage2.Controls.Add(this.label25);
		this.tabPage2.Controls.Add(this.txtCtaCte);
		this.tabPage2.Controls.Add(this.label19);
		this.tabPage2.Controls.Add(this.txtBanco);
		this.tabPage2.Controls.Add(this.label16);
		this.tabPage2.Controls.Add(this.txtTelefonoContacto);
		this.tabPage2.Controls.Add(this.label24);
		this.tabPage2.Controls.Add(this.txtContacto);
		this.tabPage2.Controls.Add(this.label23);
		this.tabPage2.Controls.Add(this.cbMoneda);
		this.tabPage2.Controls.Add(this.label22);
		this.tabPage2.Controls.Add(this.txtFechaRegistro);
		this.tabPage2.Controls.Add(this.label17);
		this.tabPage2.Controls.Add(this.txtLineaCredito);
		this.tabPage2.Controls.Add(this.label18);
		this.tabPage2.Controls.Add(this.txtDscto);
		this.tabPage2.Controls.Add(this.label20);
		this.tabPage2.Controls.Add(this.label21);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(625, 249);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Datos Adicionales";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.txttasa.Location = new System.Drawing.Point(337, 146);
		this.txttasa.Name = "txttasa";
		this.txttasa.Size = new System.Drawing.Size(96, 20);
		this.txttasa.TabIndex = 12;
		this.lbltasa.AutoSize = true;
		this.lbltasa.Location = new System.Drawing.Point(264, 150);
		this.lbltasa.Name = "lbltasa";
		this.lbltasa.Size = new System.Drawing.Size(45, 13);
		this.lbltasa.TabIndex = 62;
		this.lbltasa.Text = "Tasa %:";
		this.chbCliFacturasVencidas.AutoSize = true;
		this.chbCliFacturasVencidas.Location = new System.Drawing.Point(26, 146);
		this.chbCliFacturasVencidas.Name = "chbCliFacturasVencidas";
		this.chbCliFacturasVencidas.Size = new System.Drawing.Size(179, 17);
		this.chbCliFacturasVencidas.TabIndex = 61;
		this.chbCliFacturasVencidas.Text = "Activar Venta con F/B Vencidas";
		this.chbCliFacturasVencidas.UseVisualStyleBackColor = true;
		this.cmbVendedores.FormattingEnabled = true;
		this.cmbVendedores.Items.AddRange(new object[2] { "DNI", "RUC" });
		this.cmbVendedores.Location = new System.Drawing.Point(512, 120);
		this.cmbVendedores.Name = "cmbVendedores";
		this.cmbVendedores.Size = new System.Drawing.Size(96, 21);
		this.cmbVendedores.TabIndex = 11;
		this.cmbVendedores.Tag = "5";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(443, 123);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 60;
		this.label5.Text = "Vendedor";
		this.cbListaPrecios.FormattingEnabled = true;
		this.cbListaPrecios.Items.AddRange(new object[5] { "EXCELENTE", "BUENO", "REGULAR", "MALO", "MOROSO" });
		this.cbListaPrecios.Location = new System.Drawing.Point(512, 94);
		this.cbListaPrecios.Name = "cbListaPrecios";
		this.cbListaPrecios.Size = new System.Drawing.Size(96, 21);
		this.cbListaPrecios.TabIndex = 9;
		this.cbListaPrecios.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(443, 97);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(63, 13);
		this.label26.TabIndex = 58;
		this.label26.Text = "Lista Prec. :";
		this.label26.Visible = false;
		this.cbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbFormaPago.FormattingEnabled = true;
		this.cbFormaPago.Location = new System.Drawing.Point(129, 40);
		this.cbFormaPago.Name = "cbFormaPago";
		this.cbFormaPago.Size = new System.Drawing.Size(79, 20);
		this.cbFormaPago.TabIndex = 1;
		this.cbFormaPago.Tag = "16";
		this.cbFormaPago.SelectionChangeCommitted += new System.EventHandler(cbFormaPago_SelectionChangeCommitted);
		this.cbCalificacion.FormattingEnabled = true;
		this.cbCalificacion.Items.AddRange(new object[5] { "EXCELENTE", "BUENO", "REGULAR", "MALO", "MOROSO" });
		this.cbCalificacion.Location = new System.Drawing.Point(337, 120);
		this.cbCalificacion.Name = "cbCalificacion";
		this.cbCalificacion.Size = new System.Drawing.Size(96, 21);
		this.cbCalificacion.TabIndex = 10;
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(264, 123);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(67, 13);
		this.label27.TabIndex = 55;
		this.label27.Text = "Calificacion :";
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(129, 170);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(479, 73);
		this.txtComentario.TabIndex = 13;
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(23, 173);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(66, 13);
		this.label25.TabIndex = 53;
		this.label25.Text = "Comentario :";
		this.txtCtaCte.Location = new System.Drawing.Point(337, 40);
		this.txtCtaCte.Name = "txtCtaCte";
		this.txtCtaCte.Size = new System.Drawing.Size(271, 20);
		this.txtCtaCte.TabIndex = 6;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(264, 43);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(53, 13);
		this.label19.TabIndex = 51;
		this.label19.Text = "Cta. cte. :";
		this.txtBanco.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtBanco.Location = new System.Drawing.Point(337, 14);
		this.txtBanco.Name = "txtBanco";
		this.txtBanco.Size = new System.Drawing.Size(271, 20);
		this.txtBanco.TabIndex = 5;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(264, 17);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(44, 13);
		this.label16.TabIndex = 49;
		this.label16.Text = "Banco :";
		this.txtTelefonoContacto.Location = new System.Drawing.Point(337, 94);
		this.txtTelefonoContacto.Name = "txtTelefonoContacto";
		this.txtTelefonoContacto.Size = new System.Drawing.Size(96, 20);
		this.txtTelefonoContacto.TabIndex = 8;
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(264, 97);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(55, 13);
		this.label24.TabIndex = 47;
		this.label24.Text = "Teléfono :";
		this.txtContacto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtContacto.Location = new System.Drawing.Point(337, 67);
		this.txtContacto.Name = "txtContacto";
		this.txtContacto.Size = new System.Drawing.Size(271, 20);
		this.txtContacto.TabIndex = 7;
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(264, 70);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(56, 13);
		this.label23.TabIndex = 45;
		this.label23.Text = "Contacto :";
		this.cbMoneda.FormattingEnabled = true;
		this.cbMoneda.Location = new System.Drawing.Point(129, 67);
		this.cbMoneda.Name = "cbMoneda";
		this.cbMoneda.Size = new System.Drawing.Size(79, 21);
		this.cbMoneda.TabIndex = 2;
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(23, 70);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(52, 13);
		this.label22.TabIndex = 43;
		this.label22.Text = "Moneda :";
		this.txtFechaRegistro.Location = new System.Drawing.Point(129, 120);
		this.txtFechaRegistro.Name = "txtFechaRegistro";
		this.txtFechaRegistro.ReadOnly = true;
		this.txtFechaRegistro.Size = new System.Drawing.Size(121, 20);
		this.txtFechaRegistro.TabIndex = 4;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(23, 123);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(100, 13);
		this.label17.TabIndex = 38;
		this.label17.Text = "Fecha de Registro :";
		this.txtLineaCredito.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLineaCredito.Location = new System.Drawing.Point(129, 94);
		this.txtLineaCredito.Name = "txtLineaCredito";
		this.txtLineaCredito.Size = new System.Drawing.Size(79, 20);
		this.txtLineaCredito.TabIndex = 3;
		this.txtLineaCredito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtLineaCredito_KeyPress);
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(23, 97);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(94, 13);
		this.label18.TabIndex = 36;
		this.label18.Text = "Línea  de crédito :";
		this.txtDscto.Location = new System.Drawing.Point(129, 14);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.Size = new System.Drawing.Size(79, 20);
		this.txtDscto.TabIndex = 0;
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(23, 17);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(52, 13);
		this.label20.TabIndex = 32;
		this.label20.Text = "% Dscto :";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(23, 43);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(85, 13);
		this.label21.TabIndex = 30;
		this.label21.Text = "Forma de Pago :";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Ingrese nombre o razon social.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.label29.AutoSize = true;
		this.label29.Font = new System.Drawing.Font("Verdana", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label29.ForeColor = System.Drawing.Color.Red;
		this.label29.Location = new System.Drawing.Point(52, 294);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(143, 18);
		this.label29.TabIndex = 21;
		this.label29.Text = "INHABILITADO";
		this.label29.Visible = false;
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(488, 293);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 3;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(569, 293);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 4;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.txtSunat_Capchat.Location = new System.Drawing.Point(388, 293);
		this.txtSunat_Capchat.Name = "txtSunat_Capchat";
		this.txtSunat_Capchat.Size = new System.Drawing.Size(37, 20);
		this.txtSunat_Capchat.TabIndex = 124;
		this.txtSunat_Capchat.Visible = false;
		this.pbCapchatS.Location = new System.Drawing.Point(334, 289);
		this.pbCapchatS.Name = "pbCapchatS";
		this.pbCapchatS.Size = new System.Drawing.Size(48, 34);
		this.pbCapchatS.TabIndex = 125;
		this.pbCapchatS.TabStop = false;
		this.pbCapchatS.Visible = false;
		this.cbctgcliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbctgcliente.FormattingEnabled = true;
		this.cbctgcliente.Location = new System.Drawing.Point(489, 177);
		this.cbctgcliente.Name = "cbctgcliente";
		this.cbctgcliente.Size = new System.Drawing.Size(121, 21);
		this.cbctgcliente.TabIndex = 36;
		this.lblctgcliente.AutoSize = true;
		this.lblctgcliente.Location = new System.Drawing.Point(428, 180);
		this.lblctgcliente.Name = "lblctgcliente";
		this.lblctgcliente.Size = new System.Drawing.Size(58, 13);
		this.lblctgcliente.TabIndex = 35;
		this.lblctgcliente.Text = "Categoria :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(656, 328);
		base.Controls.Add(this.txtSunat_Capchat);
		base.Controls.Add(this.pbCapchatS);
		base.Controls.Add(this.label29);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionCliente";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Gestion Cliente";
		base.Load += new System.EventHandler(frmGestionCliente_Load);
		base.Shown += new System.EventHandler(frmGestionCliente_Shown);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
