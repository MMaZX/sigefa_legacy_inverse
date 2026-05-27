using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmRegistroProducto : Office2007Form
{
	public int Proceso = 0;

	public int Cotizacion = 0;

	public int Coti = 0;

	private clsAdmProducto admPro = new clsAdmProducto();

	private clsAdmTipoArticulo admTip = new clsAdmTipoArticulo();

	private clsAdmFamilia admFam = new clsAdmFamilia();

	private clsFamilia fam = new clsFamilia();

	private clsAdmLinea admLin = new clsAdmLinea();

	private clsLinea lin = new clsLinea();

	private clsAdmGrupo admGru = new clsAdmGrupo();

	private clsGrupo gru = new clsGrupo();

	private clsAdmMarca admMar = new clsAdmMarca();

	private clsAdmUnidad admUni = new clsAdmUnidad();

	private clsAdmEmpresa admEmpresa = new clsAdmEmpresa();

	public clsProducto pro = new clsProducto();

	private clsParametros parametro = new clsParametros();

	private bool Validacion = true;

	private clsValidar val = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsUnidadEquivalente equi = new clsUnidadEquivalente();

	public static int codtipo;

	public int tipoimpuestosunat = 1;

	private int codigoUnidadBase;

	private clsAdmParametro admParametro = new clsAdmParametro();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmProveedor admprov = new clsAdmProveedor();

	private int codcategorizacion = 0;

	private int codsituacion = 0;

	private int Accion = 1;

	private int Realizar = 1;

	private List<DataTable> listatablas = new List<DataTable>();

	private DataTable databi = new DataTable();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnCancelar;

	private Button btnGuardar;

	private CheckBox cbEstado;

	private TextBox txtNombre;

	private Label label3;

	private TextBox txtReferencia;

	private Label label2;

	private TextBox txtCodProducto;

	private Label label1;

	private ComboBox cbTipoArticulo;

	private Label label36;

	private ComboBox cbMarca;

	private Label label10;

	private ComboBox cbGrupo;

	private Label label6;

	private ComboBox cbLinea;

	private Label label5;

	private ComboBox cbFamilia;

	private Label label4;

	private ComboBox cmbUnidadBase;

	private Label label13;

	private Label label38;

	private ComboBox cbControlStock;

	private ImageList imageList1;

	private Button btnUnidad;

	private Button btnMarca;

	private Button btnGrupo;

	private Button btnLinea;

	private Button btnFamilia;

	private Button btnTipoArticulo;

	private CheckBox cbDetraccion;

	private Label label8;

	private TextBox txtComision;

	private Label label7;

	private TextBox txtPrecioCata;

	private Label label9;

	private TextBox txtMaxPorcDesc;

	private Label label31;

	private TextBox txtPeso;

	private Label label12;

	private LinkLabel linkConfiguraUnidadesEquivalentes;

	private Label label14;

	private TextBox txtPrecioCom;

	private Label lbLabelCompra;

	private TextBox txtPrecioVen;

	private Label lbPrecioVenta;

	private Label label15;

	private RadioButton rdtExonerado;

	private RadioButton rdtInafecto;

	private RadioButton rdtGravado;

	private TextBox txt_procentaje_retencion;

	private Label lb_procentaje_retencion;

	private TextBox txtUbicacion;

	private Label label16;

	private TextBox txtCodigoUniversal;

	private Label label11;

	private TextBox txtStockMinimo;

	private Label label17;

	public CheckBoxX ckbVentaTicket;

	private RadPageView radPageView1;

	private RadPageViewPage tab1;

	private RadPageViewPage tab2;

	private RadLabel radLabel1;

	private RadTextBox txtStockmax;

	private RadGridView dgvStocks;

	private MaterialTheme materialTheme1;

	private RadDropDownList cmbAlmacenes;

	private RadLabel radLabel6;

	private RadLabel lblnombreprod;

	private RadLabel radLabel4;

	private RadLabel radLabel3;

	private RadTextBox txtCapmax;

	private RadLabel radLabel2;

	private RadTextBox txtStockmin;

	private RadButton btnEliminaStock;

	private RadButton btnGuardastock;

	private RadDropDownList cmbUbicacion;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private RequiredFieldValidator requiredFieldValidator1;

	private RadPageViewPage tab3;

	private RadPageViewPage tab4;

	private RadTextBox txtGastosadic;

	private RadTextBox txtComi3;

	private RadLabel radLabel12;

	private RadLabel radLabel11;

	private RadTextBox txtGastosadmin;

	private RadLabel radLabel13;

	private RadTextBox txtComi2;

	private RadLabel radLabel10;

	private RadTextBox txtComi1;

	private RadLabel radLabel9;

	private RadTextBox txtEstiva;

	private RadLabel radLabel8;

	private RadLabel radLabel7;

	private RadLabel radLabel5;

	private RadTextBox txtDesestiva;

	private RadTextBox txtFlete;

	private RequiredFieldValidator requiredFieldValidator2;

	private RadLabel radLabel16;

	private RadLabel radLabel15;

	private RadLabel radLabel14;

	private RadDropDownList cmbProveedor3;

	private RadDropDownList cmbProveedor2;

	private RadDropDownList cmbProveedor1;

	private CheckBox chkicbper;

	private RadPageViewPage tab5;

	private RadGridView dgvListaPrecios;

	private RadButton btnRecargaControlPrecio;

	private RadButton btnEliminaControlPrecio;

	private RadButton btnGuardaControlPrecio;

	private RadSpinEditor minimo;

	private RadSpinEditor maximo;

	private RadDropDownList cmbPreciosV;

	private RadDropDownList cmbUnidadM;

	private RadLabel radLabel20;

	private RadLabel radLabel19;

	private RadLabel radLabel18;

	private RadLabel radLabel17;

	private RadPageViewPage tab6;

	private RadButton btnactualizarcategorizacion;

	private RadLabel radLabel21;

	private RadButton btguardacategorizacion;

	private RadButton btneliminarcategorizacion;

	private RadTextBox txtdescripcion;

	private RadTextBox txtcondicion;

	private RadLabel radLabel22;

	private GroupBox groupBox2;

	private RadGridView rgvcategorizacion;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private RadPageViewPage tab7;

	private GroupBox groupBox3;

	private RadGridView rgvsituacion;

	private RadButton btnguardarsituacion;

	private RadButton btneliminarsituacion;

	private RadTextBox txtdescripcionsituacion;

	private RadTextBox txtdesdesituacion;

	private RadLabel radLabel23;

	private RadButton btnactualizarsituacion;

	private RadLabel radLabel24;

	private RadTextBox txthastasituacion;

	private RadLabel lblhasta;

	private CheckBox cbdescontinuado;

	private TextBox txtStockMaximo;

	private Label label18;

	private int Codstockprod { get; set; }

	private int codpreciocantidad { get; set; }

	public frmRegistroProducto()
	{
		InitializeComponent();
	}

	private void frmRegistroProducto_Load(object sender, EventArgs e)
	{
		CargaTipoArticulos();
		CargaFamilias();
		CargaUnidades();
		CargaMarcas();
		cargaProveedores();
		CargaCartegorizacion(pro.CodProducto);
		CargaSituacion(pro.CodProducto);
		cmbUbicacion.SelectedIndex = -1;
		ckbVentaTicket.Visible = admParametro.consultarParametroVenta(1);
		codpreciocantidad = 0;
		if (frmLogin.iNivelUser != 1 && frmLogin.iNivelUser != 5)
		{
			tab4.Enabled = false;
			tab3.Enabled = false;
		}
		if (Proceso == 2)
		{
			CargaProducto();
			tab2.Enabled = true;
			cargaStocks();
			habilita_tab2();
			if (cbLinea.SelectedIndex != -1)
			{
				btnLinea.Enabled = true;
			}
			if (cbGrupo.SelectedIndex != -1)
			{
				btnGrupo.Enabled = true;
			}
			txtPrecioCom.Visible = false;
			txtPrecioVen.Visible = false;
			lbLabelCompra.Visible = false;
			lbPrecioVenta.Visible = false;
			tab5.Enabled = true;
			cargaUnidadesPrecio();
		}
		else if (Proceso == 3)
		{
			CargaProducto();
			cargaStocks();
			tab2.Enabled = true;
			sololectura();
			txtPrecioCom.Visible = false;
			txtPrecioVen.Visible = false;
			lbLabelCompra.Visible = false;
			lbPrecioVenta.Visible = false;
			tab5.Enabled = true;
			cargaUnidadesPrecio();
		}
		else if (Proceso == 1)
		{
			cbdescontinuado.Checked = false;
		}
	}

	public void habilita_tab2()
	{
		txtCapmax.Text = "";
		txtStockmax.Text = "";
		txtStockmin.Text = "";
		lblnombreprod.Text = pro.Descripcion;
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.ValueMember = "codalmacen";
		cmbAlmacenes.DataSource = admalma.ListaAlmacen2();
		btnGuardastock.Enabled = true;
		btnEliminaStock.Enabled = true;
		Codstockprod = 0;
	}

	public void cargaProveedores()
	{
		DataTable proveedores1 = new DataTable();
		proveedores1 = admprov.MuestraProveedores();
		DataTable proveedores2 = admprov.MuestraProveedores();
		DataTable proveedores3 = admprov.MuestraProveedores();
		cmbProveedor1.DisplayMember = "razonsocial";
		cmbProveedor1.ValueMember = "codproveedor";
		cmbProveedor1.DataSource = proveedores1;
		cmbProveedor1.SelectedIndex = -1;
		cmbProveedor2.DisplayMember = "razonsocial";
		cmbProveedor2.ValueMember = "codproveedor";
		cmbProveedor2.DataSource = proveedores2;
		cmbProveedor2.SelectedIndex = -1;
		cmbProveedor3.DisplayMember = "razonsocial";
		cmbProveedor3.ValueMember = "codproveedor";
		cmbProveedor3.DataSource = proveedores3;
		cmbProveedor3.SelectedIndex = -1;
	}

	public void cargaStocks()
	{
		dgvStocks.ClearSelection();
		dgvStocks.DataSource = admPro.listarStocksProducto(pro.CodProducto);
	}

	private void cargaUnidadesPrecio()
	{
		cmbUnidadM.DisplayMember = "descripcion";
		cmbUnidadM.ValueMember = "codunidadmedida";
		cmbUnidadM.DataSource = admPro.MuestraUnidadesEquivalentes(pro.CodProducto, frmLogin.iCodAlmacen);
		cmbUnidadM.SelectedIndex = -1;
		cmbPreciosV.DataSource = null;
		dgvListaPrecios.DataSource = admPro.listaPreciosCantidad(pro.CodProducto);
		dgvListaPrecios.ClearSelection();
		maximo.Value = 1m;
		minimo.Value = 0m;
	}

	private void sololectura()
	{
		ext.sololectura(groupBox1.Controls);
		btnGuardar.Visible = false;
		txtFlete.Enabled = false;
		txtEstiva.Enabled = false;
		txtDesestiva.Enabled = false;
		txtComi1.Enabled = false;
		txtComi2.Enabled = false;
		txtComi3.Enabled = false;
		txtGastosadic.Enabled = false;
		txtGastosadmin.Enabled = false;
		cmbProveedor1.Enabled = false;
		cmbProveedor2.Enabled = false;
		cmbProveedor3.Enabled = false;
	}

	private void CargaProducto()
	{
		try
		{
			if (Cotizacion == 1)
			{
				pro = admPro.CargaProductoCotizacion(pro.CodProducto, frmLogin.iCodAlmacen);
				txtReferencia.Text = "";
				txtCodProducto.Text = pro.CodProducto.ToString();
			}
			else
			{
				pro = admPro.CargaProducto(pro.CodProducto, frmLogin.iCodAlmacen);
				txtReferencia.Text = pro.Referencia;
				txtCodProducto.Text = pro.CodProducto.ToString();
			}
			txtNombre.Text = pro.Descripcion;
			if (pro.Estado)
			{
				cbEstado.Visible = false;
				cbEstado.Checked = pro.Estado;
			}
			else if (frmLogin.iNivelUser == 5 || frmLogin.iNivelUser == 1)
			{
				cbEstado.Visible = true;
				cbEstado.Checked = pro.Estado;
			}
			else
			{
				cbEstado.Visible = false;
				cbEstado.Checked = pro.Estado;
			}
			cbdescontinuado.Checked = pro.descontinuado;
			cbTipoArticulo.SelectedValue = pro.CodTipoArticulo;
			cbFamilia.SelectedValue = pro.CodFamilia;
			CargaLineas(Convert.ToInt32(cbFamilia.SelectedValue));
			cbLinea.Enabled = true;
			cbLinea.SelectedValue = pro.CodLinea;
			CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
			cbGrupo.Enabled = true;
			cbGrupo.SelectedValue = pro.CodGrupo;
			cbMarca.SelectedValue = pro.CodMarca;
			cmbUnidadBase.SelectedValue = pro.CodUnidadMedida;
			codigoUnidadBase = pro.CodUnidadMedida;
			cbControlStock.SelectedIndex = pro.CodControlStock - 1;
			txtCodigoUniversal.Text = pro.SCodUniversal;
			cmbUbicacion.Text = pro.SUbicacion;
			chkicbper.Checked = pro.ICBPER;
			switch (pro.TipoImpuesto)
			{
			case 1:
				rdtGravado.Checked = true;
				rdtExonerado.Checked = false;
				rdtInafecto.Checked = false;
				break;
			case 2:
				rdtGravado.Checked = false;
				rdtExonerado.Checked = true;
				rdtInafecto.Checked = false;
				break;
			case 3:
				rdtGravado.Checked = false;
				rdtExonerado.Checked = false;
				rdtInafecto.Checked = true;
				break;
			}
			cbDetraccion.Checked = pro.Detraccion;
			txtComision.Text = Convert.ToString(pro.Comision);
			txtPrecioCata.Text = pro.PrecioCatalogo.ToString();
			txtMaxPorcDesc.Text = pro.MaxPorcDesc.ToString();
			txtPeso.Text = pro.Peso.ToString();
			txt_procentaje_retencion.Text = pro.Porcentajerentencion.ToString();
			txtStockMinimo.Text = pro.StockMinimo.ToString();
			ckbVentaTicket.Checked = pro.VentaConTicket;
			txtFlete.Text = pro.Flete_estimado.ToString();
			txtDesestiva.Text = pro.Desestiva.ToString();
			txtEstiva.Text = pro.Estiva.ToString();
			txtComi1.Text = pro.Comision1.ToString();
			txtComi2.Text = pro.Comision2.ToString();
			txtComi3.Text = pro.Comision3.ToString();
			txtGastosadmin.Text = pro.GastosAdmin.ToString();
			txtGastosadic.Text = pro.GastosAdic.ToString();
			cmbProveedor1.SelectedValue = pro.Proveedor1;
			cmbProveedor2.SelectedValue = pro.Proveedor2;
			cmbProveedor3.SelectedValue = pro.Proveedor3;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaTipoArticulos()
	{
		cbTipoArticulo.DataSource = admTip.MuestraTipoArticulos();
		cbTipoArticulo.DisplayMember = "descripcion";
		cbTipoArticulo.ValueMember = "codTipoArticulo";
		cbTipoArticulo.SelectedIndex = 0;
	}

	private void CargaFamilias()
	{
		cbFamilia.DataSource = admFam.MuestraFamilias();
		cbFamilia.DisplayMember = "descripcion";
		cbFamilia.ValueMember = "codFamilia";
		cbFamilia.SelectedIndex = -1;
	}

	private void CargaLineas(int codFami)
	{
		cbLinea.DataSource = admLin.MuestraLineas(codFami);
		cbLinea.DisplayMember = "descripcion";
		cbLinea.ValueMember = "codLinea";
		cbLinea.SelectedIndex = -1;
	}

	private void CargaGrupos(int codLine)
	{
		cbGrupo.DataSource = admGru.MuestraGrupos(codLine);
		cbGrupo.DisplayMember = "descripcion";
		cbGrupo.ValueMember = "codGrupo";
		cbGrupo.SelectedIndex = -1;
	}

	private void CargaMarcas()
	{
		cbMarca.DataSource = admMar.MuestraMarcas();
		cbMarca.DisplayMember = "descripcion";
		cbMarca.ValueMember = "codMarca";
		cbMarca.SelectedIndex = -1;
	}

	private void CargaUnidades()
	{
		cmbUnidadBase.DataSource = admUni.MuestraUnidades();
		cmbUnidadBase.DisplayMember = "descripcion";
		cmbUnidadBase.ValueMember = "codUnidadMedida";
		cmbUnidadBase.SelectedIndex = -1;
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

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		try
		{
			if (!superValidator1.Validate() || Proceso == 0 || !(txtNombre.Text != ""))
			{
				return;
			}
			ValidarDatos(base.Controls);
			if (Validacion)
			{
				pro.CodUsuario = frmLogin.iCodUser;
				pro.CodTipoArticulo = Convert.ToInt32(cbTipoArticulo.SelectedValue);
				pro.CodFamilia = Convert.ToInt32(cbFamilia.SelectedValue);
				pro.CodLinea = Convert.ToInt32(cbLinea.SelectedValue);
				pro.CodGrupo = Convert.ToInt32(cbGrupo.SelectedValue);
				pro.CodMarca = Convert.ToInt32(cbMarca.SelectedValue);
				pro.CodUnidadMedida = Convert.ToInt32(cmbUnidadBase.SelectedValue);
				pro.CodControlStock = Convert.ToInt32(cbControlStock.SelectedIndex + 1);
				pro.Referencia = txtReferencia.Text.Trim();
				pro.Descripcion = txtNombre.Text.Trim();
				pro.Estado = cbEstado.Checked;
				pro.descontinuado = cbdescontinuado.Checked;
				pro.ICBPER = Convert.ToBoolean(chkicbper.Checked);
				pro.Cotizacion = Convert.ToBoolean(Coti);
				pro.Porcentajerentencion = decimal.Round(decimal.Parse(txt_procentaje_retencion.Text), 2);
				if (rdtGravado.Checked && !rdtExonerado.Checked && !rdtInafecto.Checked)
				{
					pro.TipoImpuesto = 1;
					pro.CodSunat = "10";
				}
				else if (!rdtGravado.Checked && rdtExonerado.Checked && !rdtInafecto.Checked)
				{
					pro.TipoImpuesto = 2;
					pro.CodSunat = "20";
				}
				else if (!rdtGravado.Checked && !rdtExonerado.Checked && rdtInafecto.Checked)
				{
					pro.TipoImpuesto = 3;
					pro.CodSunat = "30";
				}
				if (cbDetraccion.Checked)
				{
					pro.Detraccion = true;
				}
				else
				{
					pro.Detraccion = false;
				}
				if (txtComision.Text != "")
				{
					pro.Comision = Convert.ToDecimal(txtComision.Text);
				}
				else
				{
					pro.Comision = 0m;
				}
				if (txtPrecioCata.Text != "")
				{
					pro.PrecioCatalogo = Convert.ToDecimal(txtPrecioCata.Text);
				}
				else
				{
					pro.PrecioCatalogo = 0m;
				}
				if (txtMaxPorcDesc.Text != "")
				{
					pro.MaxPorcDesc = Convert.ToDecimal(txtMaxPorcDesc.Text.Trim());
				}
				pro.Peso = Convert.ToDecimal(txtPeso.Text);
				pro.SCodUniversal = txtCodigoUniversal.Text.Trim();
				if (string.IsNullOrEmpty(cmbUbicacion.Text.Trim()))
				{
					MessageBox.Show("Debes seleccionar una empresa");
					return;
				}
				pro.SUbicacion = cmbUbicacion.Text.Trim();
				if (txtStockMinimo.Text != "")
				{
					pro.StockMinimo = Convert.ToDouble(txtStockMinimo.Text.Trim());
				}
				pro.VentaConTicket = ckbVentaTicket.Checked;
				pro.Flete_estimado = (txtFlete.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtFlete.Text));
				pro.Desestiva = (txtDesestiva.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtDesestiva.Text));
				pro.Estiva = (txtEstiva.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtEstiva.Text));
				pro.Comision1 = (txtComi1.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtComi1.Text));
				pro.Comision2 = (txtComi2.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtComi2.Text));
				pro.Comision3 = (txtComi3.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtComi3.Text));
				pro.GastosAdmin = (txtGastosadmin.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtGastosadmin.Text));
				pro.GastosAdic = (txtGastosadic.Text.Equals("") ? 0.00m : Convert.ToDecimal(txtGastosadic.Text));
				pro.Proveedor1 = ((cmbProveedor1.SelectedIndex != -1) ? Convert.ToInt32(cmbProveedor1.SelectedValue) : 0);
				pro.Proveedor2 = ((cmbProveedor2.SelectedIndex != -1) ? Convert.ToInt32(cmbProveedor2.SelectedValue) : 0);
				pro.Proveedor3 = ((cmbProveedor3.SelectedIndex != -1) ? Convert.ToInt32(cmbProveedor3.SelectedValue) : 0);
				pro.StockMaximo = Convert.ToDouble(txtStockMaximo.Text);
				if (Proceso == 1 || Cotizacion == 1)
				{
					if (admPro.insert(pro))
					{
						txtCodProducto.Text = pro.CodProducto.ToString();
						MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						GuardaUnidadesCompra();
						GuardaUnidadesVenta();
						GuardaUnidadesEquivalencia();
						if (Coti != 1)
						{
							CreaProductosAlmacenes(pro);
						}
						limpiarformulario(groupBox1.Controls);
						if (Cotizacion == 1)
						{
							base.DialogResult = DialogResult.OK;
							Dispose();
						}
						else
						{
							Close();
						}
					}
				}
				else
				{
					if (Proceso != 2)
					{
						return;
					}
					if (codigoUnidadBase == pro.CodUnidadMedida)
					{
						if (admPro.update(pro))
						{
							MessageBox.Show("Los datos se guardaron correctamente", "Gestión Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							Close();
						}
						return;
					}
					string mensajeValidacion = "";
					int numeroUE = admPro.GetUnidadesEquivalentesPorUnidadBase(pro.CodProducto);
					int registroPA = admPro.VerificaProductoAlmacen(pro.CodProducto);
					if (registroPA > 0 || numeroUE > 0)
					{
						if (registroPA > 0)
						{
							mensajeValidacion = mensajeValidacion + "El producto " + pro.Descripcion.ToUpper() + " ya se ha ingresado a almacén con otra UNIDAD BASE.";
						}
						if (numeroUE > 0)
						{
							mensajeValidacion = mensajeValidacion + "Las UNIDADES EQUIVALENTES del producto " + pro.Descripcion.ToUpper() + " ya han sido configuradas, su modificación provocaría anomalías en el stock";
						}
						MessageBox.Show(mensajeValidacion + ". NO SE PERMITE LA MODIFICACIÓN.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else if (registroPA < 0 || numeroUE < 0)
					{
						MessageBox.Show("Ocurrió un error al realizar la operación", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else if (admPro.update(pro))
					{
						MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Close();
					}
				}
			}
			else
			{
				MessageBox.Show("Debe completar todos los campos requeridos(*)", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CreaProductosAlmacenes(clsProducto pro)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (admPro.creaProductoAlmacenMasivo(pro.CodProducto, frmLogin.iCodUser))
			{
				Cursor = DefaultCursor;
				return;
			}
			Cursor = DefaultCursor;
			MessageBox.Show("Ocurrio Un error al registrar producto almacen para producto con los siguientes datos:\n> CodProducto: " + pro.CodProducto + "\n> Usuario: " + frmLogin.iCodAlmacen, "Error Registro Producto Almacen", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex)
		{
			Cursor = DefaultCursor;
			MessageBox.Show(ex.Message, "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void GuardaUnidadesEquivalencia()
	{
		equi.CodProducto = Convert.ToInt32(txtCodProducto.Text);
		equi.CodEquivalente = Convert.ToInt32(cmbUnidadBase.SelectedValue);
		equi.Precio = 0m;
		equi.Factor = 1m;
		equi.CodUser = frmLogin.iCodUser;
		equi.Tipo = Convert.ToInt32(9);
		equi.CodAlmacen = frmLogin.iCodAlmacen;
		equi.CompraVenta = 2;
		equi.ICodMoneda = Convert.ToInt32(1);
		if (!admPro.insertunidadequivalente(equi, Coti))
		{
		}
	}

	private void GuardaUnidadesVenta()
	{
		equi.CodUnidad = Convert.ToInt32(cmbUnidadBase.SelectedValue);
		equi.CodProducto = Convert.ToInt32(txtCodProducto.Text);
		equi.Tipo = Convert.ToInt32(10);
		equi.Precio = Convert.ToDecimal(txtPrecioVen.Text);
		equi.CodUser = frmLogin.iCodUser;
		equi.CodAlmacen = frmLogin.iCodAlmacen;
		equi.CompraVenta = 1;
		equi.ICodMoneda = Convert.ToInt32(1);
		if (!admPro.insertunidadequivalente(equi, Coti))
		{
		}
	}

	private void GuardaUnidadesCompra()
	{
		equi.CodUnidad = Convert.ToInt32(cmbUnidadBase.SelectedValue);
		equi.CodProducto = Convert.ToInt32(txtCodProducto.Text);
		equi.Precio = Convert.ToDecimal(txtPrecioCom.Text);
		equi.CodUser = frmLogin.iCodUser;
		equi.Tipo = 9;
		equi.CodAlmacen = frmLogin.iCodAlmacen;
		equi.CompraVenta = 0;
		equi.ICodMoneda = 1;
		if (!admPro.insertunidadequivalente(equi, Coti))
		{
		}
	}

	private void limpiarformulario(Control.ControlCollection Coleccion)
	{
		foreach (Control c in Coleccion)
		{
			if (c is TextBox || c is ComboBox)
			{
				c.Text = "";
			}
			if (c.HasChildren)
			{
				limpiarformulario(c.Controls);
			}
		}
	}

	private void cbFamilia_SelectionChangeCommitted(object sender, EventArgs e)
	{
		fam = admFam.CargaFamilia(Convert.ToInt32(cbFamilia.SelectedValue));
		CargaLineas(Convert.ToInt32(cbFamilia.SelectedValue));
		CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
		cbGrupo.Text = "";
		if (cbFamilia.SelectedIndex != -1)
		{
			cbLinea.Enabled = true;
			btnLinea.Enabled = true;
		}
		else
		{
			cbLinea.Enabled = false;
			btnLinea.Enabled = false;
		}
	}

	private void cbLinea_SelectionChangeCommitted(object sender, EventArgs e)
	{
		lin = admLin.CargaLinea(Convert.ToInt32(cbLinea.SelectedValue));
		CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
		if (cbLinea.SelectedIndex != -1)
		{
			cbGrupo.Enabled = true;
			btnGrupo.Enabled = true;
		}
		else
		{
			cbGrupo.Enabled = false;
			btnGrupo.Enabled = false;
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
	}

	private void btnTipoArticulo_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmTipoArticulos"] != null)
		{
			Application.OpenForms["frmTipoArticulos"].Activate();
			return;
		}
		frmTipoArticulos form = new frmTipoArticulos();
		form.ShowDialog();
		CargaTipoArticulos();
		cbTipoArticulo.SelectedValue = codtipo;
	}

	private void btnFamilia_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmFamilias"] != null)
		{
			Application.OpenForms["frmFamilias"].Activate();
			return;
		}
		frmFamilias form = new frmFamilias();
		form.ShowDialog();
		CargaFamilias();
	}

	private void btnLinea_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmLineas"] != null)
		{
			Application.OpenForms["frmLineas"].Activate();
			return;
		}
		frmLineas frm = new frmLineas();
		frm.fam = fam;
		frm.ShowDialog();
		CargaLineas(Convert.ToInt32(cbFamilia.SelectedValue));
	}

	private void btnGrupo_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmGrupo"] != null)
		{
			Application.OpenForms["frmGrupo"].Activate();
			return;
		}
		frmGrupo frm = new frmGrupo();
		frm.lin = lin;
		frm.ShowDialog();
		CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
	}

	private void btnMarca_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmMarcas"] != null)
		{
			Application.OpenForms["frmMarcas"].Activate();
			return;
		}
		frmMarcas form = new frmMarcas();
		form.ShowDialog();
		CargaMarcas();
	}

	private void btnUnidad_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmUnidades"] != null)
		{
			Application.OpenForms["frmUnidades"].Activate();
			return;
		}
		frmUnidades form = new frmUnidades();
		form.ShowDialog();
		CargaMarcas();
	}

	private void cbFamilia_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			fam = admFam.CargaFamilia(Convert.ToInt32(cbFamilia.SelectedValue));
			CargaLineas(Convert.ToInt32(cbFamilia.SelectedValue));
			CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
			cbGrupo.Text = "";
			if (cbFamilia.SelectedIndex != -1)
			{
				cbLinea.Enabled = true;
				btnLinea.Enabled = true;
			}
			else
			{
				cbLinea.Enabled = false;
				btnLinea.Enabled = false;
			}
		}
	}

	private void cbLinea_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			lin = admLin.CargaLinea(Convert.ToInt32(cbLinea.SelectedValue));
			CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
			if (cbLinea.SelectedIndex != -1)
			{
				cbGrupo.Enabled = true;
				btnGrupo.Enabled = true;
			}
			else
			{
				cbGrupo.Enabled = false;
				btnGrupo.Enabled = false;
			}
		}
	}

	private void txtComision_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void txtPrecioCata_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void frmRegistroProducto_Shown(object sender, EventArgs e)
	{
		txtNombre.Focus();
	}

	private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void linkConfiguraUnidadesEquivalentes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		frmUnidadEquivalente frm = new frmUnidadEquivalente();
		frm.codProd = pro.CodProducto;
		frm.ShowDialog();
	}

	private void rdtGravado_CheckedChanged(object sender, EventArgs e)
	{
		if (rdtGravado.Checked)
		{
			tipoimpuestosunat = 1;
			rdtExonerado.Checked = false;
			rdtInafecto.Checked = false;
		}
	}

	private void rdtExonerado_CheckedChanged(object sender, EventArgs e)
	{
		if (rdtExonerado.Checked)
		{
			tipoimpuestosunat = 2;
			rdtGravado.Checked = false;
			rdtInafecto.Checked = false;
		}
	}

	private void rdtInafecto_CheckedChanged(object sender, EventArgs e)
	{
		if (rdtInafecto.Checked)
		{
			tipoimpuestosunat = 3;
			rdtGravado.Checked = false;
			rdtExonerado.Checked = false;
		}
	}

	private void txtPrecioVen_Leave(object sender, EventArgs e)
	{
		txtPrecioCata.Text = txtPrecioVen.Text;
	}

	private void txt_procentaje_retencion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
		if (!char.IsControl(e.KeyChar))
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text.IndexOf('.') > -1 && textBox.Text.Substring(textBox.Text.IndexOf('.')).Length >= 3)
			{
				e.Handled = true;
			}
		}
	}

	private void cbTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cbTipoArticulo.Items.Count > 0 && cbTipoArticulo.SelectedIndex != -1)
		{
			if (cbTipoArticulo.Text.IndexOf("SERVICIO") > -1)
			{
				lb_procentaje_retencion.Visible = true;
				txt_procentaje_retencion.Visible = true;
				txt_procentaje_retencion.Text = "0.00";
			}
			else
			{
				lb_procentaje_retencion.Visible = false;
				txt_procentaje_retencion.Visible = false;
				txt_procentaje_retencion.Text = "0.00";
			}
		}
	}

	private void txt_procentaje_retencion_Leave(object sender, EventArgs e)
	{
		if (txt_procentaje_retencion.Text.Length == 0)
		{
			txt_procentaje_retencion.Text = "0.00";
		}
	}

	private void txtPeso_TextChanged(object sender, EventArgs e)
	{
	}

	private void label11_Click(object sender, EventArgs e)
	{
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtPrecioVen_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtMaxPorcDesc_Leave(object sender, EventArgs e)
	{
		if (Convert.ToDouble(txtMaxPorcDesc.Text.Trim()) > parametro.Valor || Convert.ToDouble(txtMaxPorcDesc.Text.Trim()) > 100.0)
		{
			MessageBox.Show("El máximo porcentaje de dscto es : " + parametro.Valor + " %", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			txtMaxPorcDesc.Focus();
		}
	}

	private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (Application.OpenForms["frmProductoSunat"] != null)
		{
			Application.OpenForms["frmProductoSunat"].Activate();
			return;
		}
		frmProductoSunat form = new frmProductoSunat();
		DataTable jes = ListaCoincidenciasProducto(txtNombre.Text.ToString());
		foreach (DataRow row in jes.Rows)
		{
			form.dgvProductoSunat.Rows.Add(row[2].ToString(), row[3].ToString());
		}
		form.ShowDialog();
	}

	private DataTable ListaCoincidenciasProducto(string cajatexto)
	{
		try
		{
			databi = admPro.CargaProductoSunat();
			if (cajatexto.Length > 0)
			{
				listatablas.Clear();
				string[] AUnidad = cajatexto.Split(' ');
				DataTable datamel = null;
				int i;
				for (i = 0; i < AUnidad.Length; i++)
				{
					datamel = null;
					if (i == 0)
					{
						if ((from r in databi.AsEnumerable()
							where r.Field<string>("descripcionproducto").Contains(AUnidad[i].Trim() + " ")
							select r).Any())
						{
							datamel = (from r in databi.AsEnumerable()
								where r.Field<string>("descripcionproducto").Contains(AUnidad[i].Trim() + " ")
								select r).CopyToDataTable();
							listatablas.Add(datamel);
						}
						else
						{
							datamel = (from r in databi.AsEnumerable()
								where r.Field<string>("descripcionproducto").Contains(AUnidad[i].Trim())
								select r).CopyToDataTable();
							listatablas.Add(datamel);
						}
						continue;
					}
					for (int j = 0; j < listatablas.Count; j++)
					{
						if (listatablas.Count <= i && (from r in listatablas[j].AsEnumerable()
							where r.Field<string>("descripcionproducto").Contains(AUnidad[i].Trim() + " ")
							select r).Any())
						{
							datamel = (from r in listatablas[j].AsEnumerable()
								where r.Field<string>("descripcionproducto").Contains(AUnidad[i].Trim() + " ")
								select r).CopyToDataTable();
							listatablas.Add(datamel);
							break;
						}
					}
				}
				return listatablas[listatablas.Count - 1];
			}
			return databi;
		}
		catch (Exception)
		{
			return databi;
		}
	}

	private void btnGuardastock_Click(object sender, EventArgs e)
	{
		try
		{
			decimal stockmin = Convert.ToDecimal(txtStockmin.Text);
			decimal stockmax = Convert.ToDecimal(txtStockmax.Text);
			decimal capmax = Convert.ToDecimal(txtCapmax.Text);
			int codalma = Convert.ToInt32(cmbAlmacenes.SelectedValue);
			int codpro = pro.CodProducto;
			if (stockmin > stockmax)
			{
				MessageBox.Show("El Stock minimo no puede ser mayor que el stock maximo");
				return;
			}
			if (stockmin > capmax)
			{
				MessageBox.Show("El Stock minimo no puede ser mayor que la capacidad maxima");
				return;
			}
			if (stockmax > capmax)
			{
				MessageBox.Show("El Stock max no puede ser mayor que la capacidad maxima");
				return;
			}
			if (Codstockprod == 0)
			{
				if (!admPro.insertaStocksProducto(codpro, codalma, stockmin, stockmax, capmax))
				{
					MessageBox.Show("Error al registrar");
				}
			}
			else if (Codstockprod != 0 && !admPro.updateStocksProducto(codpro, codalma, stockmin, stockmax, capmax))
			{
				MessageBox.Show("Error al actualizar");
			}
			cargaStocks();
			txtCapmax.Text = "";
			txtStockmin.Text = "";
			txtStockmax.Text = "";
			cmbAlmacenes.SelectedIndex = -1;
			Codstockprod = 0;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void btnEliminaStock_Click(object sender, EventArgs e)
	{
		if (Codstockprod != 0)
		{
			int codpro = Convert.ToInt32(dgvStocks.CurrentRow.Cells["idproducto"].Value);
			int codalma = Convert.ToInt32(dgvStocks.CurrentRow.Cells["idalmacen"].Value);
			if (!admPro.eliminaStocksProducto(codpro, codalma))
			{
				MessageBox.Show("Error al eliminar");
			}
			cargaStocks();
			txtCapmax.Text = "";
			txtStockmin.Text = "";
			txtStockmax.Text = "";
			cmbAlmacenes.SelectedIndex = -1;
			Codstockprod = 0;
		}
	}

	private void dgvStocks_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (dgvStocks.Rows.Count > 0 && e.RowIndex != -1)
		{
			cmbAlmacenes.SelectedValue = e.Row.Cells["idalmacen"].Value;
			txtStockmax.Text = e.Row.Cells["stock_max"].Value.ToString();
			txtStockmin.Text = e.Row.Cells["stock_min"].Value.ToString();
			txtCapmax.Text = e.Row.Cells["cap_max"].Value.ToString();
			Codstockprod = Convert.ToInt32(e.Row.Cells["id"].Value);
		}
	}

	private void btnGuardarPrecios_Click(object sender, EventArgs e)
	{
	}

	private void btnGuardarProveedores_Click(object sender, EventArgs e)
	{
	}

	private void cmbUnidadM_SelectedValueChanged(object sender, EventArgs e)
	{
		if (cmbUnidadM.SelectedIndex != -1 && cmbUnidadM.Text != "")
		{
			cmbPreciosV.DisplayMember = "tip";
			cmbPreciosV.ValueMember = "codUnidadEquivalente";
			cmbPreciosV.DataSource = admPro.PrecioVentaProductoPorUnidad(pro.CodProducto, Convert.ToInt32(cmbUnidadM.SelectedValue));
		}
	}

	private void btnGuardaControlPrecio_Click(object sender, EventArgs e)
	{
		if (cmbUnidadM.SelectedIndex != -1 && cmbPreciosV.SelectedIndex != -1 && maximo.Value > minimo.Value)
		{
			decimal cantmax = maximo.Value;
			decimal cantmin = minimo.Value;
			int codueq = Convert.ToInt32(cmbPreciosV.SelectedValue);
			if (admPro.GuardaPrecioCantidad(codueq, cantmax, cantmin, frmLogin.iCodUser))
			{
				MessageBox.Show("Se guardo correctamente");
			}
			else
			{
				MessageBox.Show("Hubo un error");
			}
			cargaUnidadesPrecio();
		}
		else
		{
			MessageBox.Show("Revise los datos a guardar");
		}
	}

	private void btnEliminaControlPrecio_Click(object sender, EventArgs e)
	{
		if (codpreciocantidad != 0)
		{
			if (admPro.EliminaPrecioCantidad(codpreciocantidad))
			{
				MessageBox.Show("eliminado correctamente");
			}
			else
			{
				MessageBox.Show("Hubo un error");
			}
			cargaUnidadesPrecio();
		}
		else
		{
			MessageBox.Show("Seleccione una fila de la tabla");
		}
	}

	private void btnRecargaControlPrecio_Click(object sender, EventArgs e)
	{
		cargaUnidadesPrecio();
	}

	private void dgvListaPrecios_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (dgvListaPrecios.Rows.Count > 0 && e.RowIndex != -1)
		{
			codpreciocantidad = Convert.ToInt32(e.Row.Cells["id"].Value);
		}
	}

	private void btguardacategorizacion_Click(object sender, EventArgs e)
	{
		if (txtcondicion.Text == "")
		{
			MessageBox.Show("Ingrese Información", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtcondicion.Focus();
			return;
		}
		if (txtdescripcion.Text == "")
		{
			MessageBox.Show("Ingrese Información", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtdescripcion.Focus();
			return;
		}
		if (Accion != 1)
		{
			if (admPro.ActualizaCategorizacion(codcategorizacion, txtcondicion.Text, txtdescripcion.Text))
			{
				MessageBox.Show("Se guardo correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaCartegorizacion(pro.CodProducto);
				txtcondicion.Text = "";
				txtdescripcion.Text = "";
				Accion = 1;
			}
			else
			{
				MessageBox.Show("Hubo un error", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		cargaUnidadesPrecio();
	}

	private void CargaCartegorizacion(int codproducto)
	{
		rgvcategorizacion.DataSource = admPro.CargaCategorizacion(codproducto);
	}

	private void CargaSituacion(int codproducto)
	{
		rgvsituacion.DataSource = admPro.CargaSituacion(codproducto);
	}

	private void btnactualizarcategorizacion_Click(object sender, EventArgs e)
	{
		CargaCartegorizacion(pro.CodProducto);
		txtcondicion.Text = "";
		txtdescripcion.Text = "";
		Accion = 1;
	}

	private void rgvcategorizacion_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvcategorizacion.Rows.Count >= 1 && e.RowIndex != -1)
		{
			codcategorizacion = Convert.ToInt32(e.Row.Cells["id"].Value.ToString());
			txtcondicion.Text = e.Row.Cells["condicion"].Value.ToString();
			txtdescripcion.Text = e.Row.Cells["descripcion"].Value.ToString();
			Accion = 2;
		}
	}

	private void btneliminarcategorizacion_Click(object sender, EventArgs e)
	{
		if (admPro.EliminaCategorizacion(codcategorizacion))
		{
			MessageBox.Show("Se elimino correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CargaCartegorizacion(pro.CodProducto);
			txtcondicion.Text = "";
			txtdescripcion.Text = "";
			Accion = 1;
		}
		else
		{
			MessageBox.Show("Hubo un error", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnguardarsituacion_Click(object sender, EventArgs e)
	{
		if (txtdesdesituacion.Text == "")
		{
			MessageBox.Show("Ingrese Información", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtdesdesituacion.Focus();
			return;
		}
		if (txthastasituacion.Text == "")
		{
			MessageBox.Show("Ingrese Información", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txthastasituacion.Focus();
			return;
		}
		if (txtdescripcionsituacion.Text == "")
		{
			MessageBox.Show("Ingrese Información", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtdescripcionsituacion.Focus();
			return;
		}
		if (Realizar == 1)
		{
			if (admPro.GuardaSituacion(pro.CodProducto, txtdesdesituacion.Text, txthastasituacion.Text, txtdescripcionsituacion.Text))
			{
				MessageBox.Show("Se guardo correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaSituacion(pro.CodProducto);
				txtdesdesituacion.Text = "";
				txthastasituacion.Text = "";
				txtdescripcionsituacion.Text = "";
			}
			else
			{
				MessageBox.Show("Hubo un error", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		cargaUnidadesPrecio();
	}

	private void btneliminarsituacion_Click(object sender, EventArgs e)
	{
		if (admPro.EliminaSituacion(codsituacion))
		{
			MessageBox.Show("Se elimino correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CargaSituacion(pro.CodProducto);
			txtdesdesituacion.Text = "";
			txtdescripcionsituacion.Text = "";
			Realizar = 1;
		}
		else
		{
			MessageBox.Show("Hubo un error", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnactualizarsituacion_Click(object sender, EventArgs e)
	{
		CargaSituacion(pro.CodProducto);
		txtdesdesituacion.Text = "";
		txtdescripcionsituacion.Text = "";
		Realizar = 1;
	}

	private void rgvsituacion_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (rgvcategorizacion.Rows.Count >= 1 && e.RowIndex != -1)
			{
				txtdesdesituacion.Text = e.Row.Cells["desde"].Value.ToString();
				txthastasituacion.Text = e.Row.Cells["hasta"].Value.ToString();
				txtdescripcionsituacion.Text = e.Row.Cells["descripcion"].Value.ToString();
				Realizar = 2;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
		Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRegistroProducto));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition4 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtStockMaximo = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.cbdescontinuado = new System.Windows.Forms.CheckBox();
		this.chkicbper = new System.Windows.Forms.CheckBox();
		this.cmbUbicacion = new Telerik.WinControls.UI.RadDropDownList();
		this.ckbVentaTicket = new DevComponents.DotNetBar.Controls.CheckBoxX();
		this.txtStockMinimo = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.txtCodigoUniversal = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txt_procentaje_retencion = new System.Windows.Forms.TextBox();
		this.lb_procentaje_retencion = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.rdtExonerado = new System.Windows.Forms.RadioButton();
		this.rdtInafecto = new System.Windows.Forms.RadioButton();
		this.rdtGravado = new System.Windows.Forms.RadioButton();
		this.lbPrecioVenta = new System.Windows.Forms.Label();
		this.lbLabelCompra = new System.Windows.Forms.Label();
		this.txtPrecioCom = new System.Windows.Forms.TextBox();
		this.txtPrecioVen = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.linkConfiguraUnidadesEquivalentes = new System.Windows.Forms.LinkLabel();
		this.txtPeso = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnGuardar = new System.Windows.Forms.Button();
		this.label31 = new System.Windows.Forms.Label();
		this.txtMaxPorcDesc = new System.Windows.Forms.TextBox();
		this.txtPrecioCata = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtComision = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cbDetraccion = new System.Windows.Forms.CheckBox();
		this.btnUnidad = new System.Windows.Forms.Button();
		this.btnMarca = new System.Windows.Forms.Button();
		this.btnGrupo = new System.Windows.Forms.Button();
		this.btnLinea = new System.Windows.Forms.Button();
		this.btnFamilia = new System.Windows.Forms.Button();
		this.btnTipoArticulo = new System.Windows.Forms.Button();
		this.label38 = new System.Windows.Forms.Label();
		this.cbControlStock = new System.Windows.Forms.ComboBox();
		this.cmbUnidadBase = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.cbTipoArticulo = new System.Windows.Forms.ComboBox();
		this.label36 = new System.Windows.Forms.Label();
		this.cbMarca = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this.cbGrupo = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.cbLinea = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.cbFamilia = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.cbEstado = new System.Windows.Forms.CheckBox();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodProducto = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtUbicacion = new System.Windows.Forms.TextBox();
		this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
		this.tab1 = new Telerik.WinControls.UI.RadPageViewPage();
		this.tab3 = new Telerik.WinControls.UI.RadPageViewPage();
		this.txtGastosadic = new Telerik.WinControls.UI.RadTextBox();
		this.txtComi3 = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel12 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
		this.txtGastosadmin = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel13 = new Telerik.WinControls.UI.RadLabel();
		this.txtComi2 = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel10 = new Telerik.WinControls.UI.RadLabel();
		this.txtComi1 = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel9 = new Telerik.WinControls.UI.RadLabel();
		this.txtEstiva = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
		this.txtDesestiva = new Telerik.WinControls.UI.RadTextBox();
		this.txtFlete = new Telerik.WinControls.UI.RadTextBox();
		this.tab4 = new Telerik.WinControls.UI.RadPageViewPage();
		this.cmbProveedor3 = new Telerik.WinControls.UI.RadDropDownList();
		this.cmbProveedor2 = new Telerik.WinControls.UI.RadDropDownList();
		this.cmbProveedor1 = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel16 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel14 = new Telerik.WinControls.UI.RadLabel();
		this.tab2 = new Telerik.WinControls.UI.RadPageViewPage();
		this.btnEliminaStock = new Telerik.WinControls.UI.RadButton();
		this.btnGuardastock = new Telerik.WinControls.UI.RadButton();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
		this.lblnombreprod = new Telerik.WinControls.UI.RadLabel();
		this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
		this.txtCapmax = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
		this.txtStockmin = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.txtStockmax = new Telerik.WinControls.UI.RadTextBox();
		this.dgvStocks = new Telerik.WinControls.UI.RadGridView();
		this.tab5 = new Telerik.WinControls.UI.RadPageViewPage();
		this.dgvListaPrecios = new Telerik.WinControls.UI.RadGridView();
		this.btnRecargaControlPrecio = new Telerik.WinControls.UI.RadButton();
		this.btnEliminaControlPrecio = new Telerik.WinControls.UI.RadButton();
		this.btnGuardaControlPrecio = new Telerik.WinControls.UI.RadButton();
		this.minimo = new Telerik.WinControls.UI.RadSpinEditor();
		this.maximo = new Telerik.WinControls.UI.RadSpinEditor();
		this.cmbPreciosV = new Telerik.WinControls.UI.RadDropDownList();
		this.cmbUnidadM = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel20 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel19 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel18 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel17 = new Telerik.WinControls.UI.RadLabel();
		this.tab6 = new Telerik.WinControls.UI.RadPageViewPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvcategorizacion = new Telerik.WinControls.UI.RadGridView();
		this.btguardacategorizacion = new Telerik.WinControls.UI.RadButton();
		this.btneliminarcategorizacion = new Telerik.WinControls.UI.RadButton();
		this.txtdescripcion = new Telerik.WinControls.UI.RadTextBox();
		this.txtcondicion = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel22 = new Telerik.WinControls.UI.RadLabel();
		this.btnactualizarcategorizacion = new Telerik.WinControls.UI.RadButton();
		this.radLabel21 = new Telerik.WinControls.UI.RadLabel();
		this.tab7 = new Telerik.WinControls.UI.RadPageViewPage();
		this.txthastasituacion = new Telerik.WinControls.UI.RadTextBox();
		this.lblhasta = new Telerik.WinControls.UI.RadLabel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.rgvsituacion = new Telerik.WinControls.UI.RadGridView();
		this.btnguardarsituacion = new Telerik.WinControls.UI.RadButton();
		this.btneliminarsituacion = new Telerik.WinControls.UI.RadButton();
		this.txtdescripcionsituacion = new Telerik.WinControls.UI.RadTextBox();
		this.txtdesdesituacion = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel23 = new Telerik.WinControls.UI.RadLabel();
		this.btnactualizarsituacion = new Telerik.WinControls.UI.RadButton();
		this.radLabel24 = new Telerik.WinControls.UI.RadLabel();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.requiredFieldValidator1 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.requiredFieldValidator2 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbUbicacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radPageView1).BeginInit();
		this.radPageView1.SuspendLayout();
		this.tab1.SuspendLayout();
		this.tab3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtGastosadic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtComi3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel12).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel11).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGastosadmin).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel13).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtComi2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel10).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtComi1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel9).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEstiva).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDesestiva).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFlete).BeginInit();
		this.tab4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbProveedor3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbProveedor2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbProveedor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel16).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel15).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel14).BeginInit();
		this.tab2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnEliminaStock).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardastock).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lblnombreprod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCapmax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStockmin).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStockmax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvStocks).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvStocks.MasterTemplate).BeginInit();
		this.tab5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvListaPrecios).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvListaPrecios.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnRecargaControlPrecio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnEliminaControlPrecio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardaControlPrecio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.minimo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.maximo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbPreciosV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbUnidadM).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel20).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel19).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel18).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel17).BeginInit();
		this.tab6.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvcategorizacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvcategorizacion.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btguardacategorizacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btneliminarcategorizacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtdescripcion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtcondicion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel22).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnactualizarcategorizacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel21).BeginInit();
		this.tab7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txthastasituacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lblhasta).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvsituacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvsituacion.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardarsituacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btneliminarsituacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtdescripcionsituacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtdesdesituacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel23).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnactualizarsituacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel24).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.txtStockMaximo);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.cbdescontinuado);
		this.groupBox1.Controls.Add(this.chkicbper);
		this.groupBox1.Controls.Add(this.cmbUbicacion);
		this.groupBox1.Controls.Add(this.ckbVentaTicket);
		this.groupBox1.Controls.Add(this.txtStockMinimo);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.txtCodigoUniversal);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.txt_procentaje_retencion);
		this.groupBox1.Controls.Add(this.lb_procentaje_retencion);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.rdtExonerado);
		this.groupBox1.Controls.Add(this.rdtInafecto);
		this.groupBox1.Controls.Add(this.rdtGravado);
		this.groupBox1.Controls.Add(this.lbPrecioVenta);
		this.groupBox1.Controls.Add(this.lbLabelCompra);
		this.groupBox1.Controls.Add(this.txtPrecioCom);
		this.groupBox1.Controls.Add(this.txtPrecioVen);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.linkConfiguraUnidadesEquivalentes);
		this.groupBox1.Controls.Add(this.txtPeso);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.btnCancelar);
		this.groupBox1.Controls.Add(this.btnGuardar);
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.txtMaxPorcDesc);
		this.groupBox1.Controls.Add(this.txtPrecioCata);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtComision);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.cbDetraccion);
		this.groupBox1.Controls.Add(this.btnUnidad);
		this.groupBox1.Controls.Add(this.btnMarca);
		this.groupBox1.Controls.Add(this.btnGrupo);
		this.groupBox1.Controls.Add(this.btnLinea);
		this.groupBox1.Controls.Add(this.btnFamilia);
		this.groupBox1.Controls.Add(this.btnTipoArticulo);
		this.groupBox1.Controls.Add(this.label38);
		this.groupBox1.Controls.Add(this.cbControlStock);
		this.groupBox1.Controls.Add(this.cmbUnidadBase);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.cbTipoArticulo);
		this.groupBox1.Controls.Add(this.label36);
		this.groupBox1.Controls.Add(this.cbMarca);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.cbGrupo);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.cbLinea);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.cbFamilia);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.cbEstado);
		this.groupBox1.Controls.Add(this.txtNombre);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtReferencia);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtCodProducto);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(603, 418);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Nuevo Producto";
		this.txtStockMaximo.Location = new System.Drawing.Point(172, 341);
		this.txtStockMaximo.Name = "txtStockMaximo";
		this.txtStockMaximo.Size = new System.Drawing.Size(115, 20);
		this.txtStockMaximo.TabIndex = 140;
		this.txtStockMaximo.Text = "0.00";
		this.txtStockMaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(15, 344);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(99, 17);
		this.label18.TabIndex = 139;
		this.label18.Text = "Stock Maximo:";
		this.cbdescontinuado.AutoSize = true;
		this.cbdescontinuado.Checked = true;
		this.cbdescontinuado.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbdescontinuado.Location = new System.Drawing.Point(482, 19);
		this.cbdescontinuado.Name = "cbdescontinuado";
		this.cbdescontinuado.Size = new System.Drawing.Size(121, 21);
		this.cbdescontinuado.TabIndex = 138;
		this.cbdescontinuado.Text = "Descontinuado";
		this.cbdescontinuado.UseVisualStyleBackColor = true;
		this.chkicbper.AutoSize = true;
		this.chkicbper.Location = new System.Drawing.Point(172, 363);
		this.chkicbper.Name = "chkicbper";
		this.chkicbper.Size = new System.Drawing.Size(75, 21);
		this.chkicbper.TabIndex = 137;
		this.chkicbper.Text = "ICBPER";
		this.chkicbper.UseVisualStyleBackColor = true;
		radListDataItem1.Text = "F. RICARDO";
		radListDataItem2.Text = "CORP. LM";
		this.cmbUbicacion.Items.Add(radListDataItem1);
		this.cmbUbicacion.Items.Add(radListDataItem2);
		this.cmbUbicacion.Location = new System.Drawing.Point(376, 41);
		this.cmbUbicacion.Name = "cmbUbicacion";
		this.cmbUbicacion.Size = new System.Drawing.Size(143, 20);
		this.cmbUbicacion.TabIndex = 136;
		this.superValidator1.SetValidator1(this.cmbUbicacion, this.requiredFieldValidator1);
		this.ckbVentaTicket.BackColor = System.Drawing.Color.Transparent;
		this.ckbVentaTicket.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ckbVentaTicket.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ckbVentaTicket.Location = new System.Drawing.Point(16, 363);
		this.ckbVentaTicket.Name = "ckbVentaTicket";
		this.ckbVentaTicket.Size = new System.Drawing.Size(138, 23);
		this.ckbVentaTicket.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.ckbVentaTicket.TabIndex = 135;
		this.ckbVentaTicket.Text = "VENTA CON TICKET";
		this.txtStockMinimo.Location = new System.Drawing.Point(172, 318);
		this.txtStockMinimo.Name = "txtStockMinimo";
		this.txtStockMinimo.Size = new System.Drawing.Size(115, 20);
		this.txtStockMinimo.TabIndex = 131;
		this.txtStockMinimo.Text = "0.00";
		this.txtStockMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(15, 321);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(95, 17);
		this.label17.TabIndex = 130;
		this.label17.Text = "Stock Mínimo:";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(373, 26);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(76, 17);
		this.label16.TabIndex = 128;
		this.label16.Text = "Rzn. Social";
		this.txtCodigoUniversal.Location = new System.Drawing.Point(196, 41);
		this.txtCodigoUniversal.Name = "txtCodigoUniversal";
		this.txtCodigoUniversal.Size = new System.Drawing.Size(174, 20);
		this.txtCodigoUniversal.TabIndex = 127;
		this.txtCodigoUniversal.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(193, 25);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(95, 17);
		this.label11.TabIndex = 126;
		this.label11.Text = "Código Sunat:";
		this.label11.Click += new System.EventHandler(label11_Click);
		this.txt_procentaje_retencion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txt_procentaje_retencion.Location = new System.Drawing.Point(440, 325);
		this.txt_procentaje_retencion.MaxLength = 1000;
		this.txt_procentaje_retencion.Name = "txt_procentaje_retencion";
		this.txt_procentaje_retencion.Size = new System.Drawing.Size(144, 20);
		this.txt_procentaje_retencion.TabIndex = 125;
		this.txt_procentaje_retencion.Text = "0.00";
		this.txt_procentaje_retencion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txt_procentaje_retencion.Visible = false;
		this.txt_procentaje_retencion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_procentaje_retencion_KeyPress);
		this.txt_procentaje_retencion.Leave += new System.EventHandler(txt_procentaje_retencion_Leave);
		this.lb_procentaje_retencion.AutoSize = true;
		this.lb_procentaje_retencion.Location = new System.Drawing.Point(295, 328);
		this.lb_procentaje_retencion.Name = "lb_procentaje_retencion";
		this.lb_procentaje_retencion.Size = new System.Drawing.Size(166, 17);
		this.lb_procentaje_retencion.TabIndex = 124;
		this.lb_procentaje_retencion.Text = "Porcentaje de Detracción:";
		this.lb_procentaje_retencion.Visible = false;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(295, 124);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(168, 17);
		this.label15.TabIndex = 123;
		this.label15.Text = "Producto se define como :";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(569, 157);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(18, 17);
		this.label8.TabIndex = 51;
		this.label8.Text = "%";
		this.label8.Visible = false;
		this.rdtExonerado.AutoSize = true;
		this.rdtExonerado.Location = new System.Drawing.Point(304, 163);
		this.rdtExonerado.Name = "rdtExonerado";
		this.rdtExonerado.Size = new System.Drawing.Size(108, 21);
		this.rdtExonerado.TabIndex = 122;
		this.rdtExonerado.Text = "EXONERADO";
		this.rdtExonerado.UseVisualStyleBackColor = true;
		this.rdtExonerado.CheckedChanged += new System.EventHandler(rdtExonerado_CheckedChanged);
		this.rdtInafecto.AutoSize = true;
		this.rdtInafecto.Location = new System.Drawing.Point(304, 185);
		this.rdtInafecto.Name = "rdtInafecto";
		this.rdtInafecto.Size = new System.Drawing.Size(92, 21);
		this.rdtInafecto.TabIndex = 121;
		this.rdtInafecto.Text = "INAFECTO";
		this.rdtInafecto.UseVisualStyleBackColor = true;
		this.rdtInafecto.CheckedChanged += new System.EventHandler(rdtInafecto_CheckedChanged);
		this.rdtGravado.AutoSize = true;
		this.rdtGravado.Checked = true;
		this.rdtGravado.Location = new System.Drawing.Point(305, 141);
		this.rdtGravado.Name = "rdtGravado";
		this.rdtGravado.Size = new System.Drawing.Size(89, 21);
		this.rdtGravado.TabIndex = 120;
		this.rdtGravado.TabStop = true;
		this.rdtGravado.Text = "GRAVADO";
		this.rdtGravado.UseVisualStyleBackColor = true;
		this.rdtGravado.CheckedChanged += new System.EventHandler(rdtGravado_CheckedChanged);
		this.lbPrecioVenta.AutoSize = true;
		this.lbPrecioVenta.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.lbPrecioVenta.ForeColor = System.Drawing.Color.Black;
		this.lbPrecioVenta.Location = new System.Drawing.Point(12, 295);
		this.lbPrecioVenta.Name = "lbPrecioVenta";
		this.lbPrecioVenta.Size = new System.Drawing.Size(142, 17);
		this.lbPrecioVenta.TabIndex = 112;
		this.lbPrecioVenta.Text = "Precio Venta con IGV:";
		this.lbLabelCompra.AutoSize = true;
		this.lbLabelCompra.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.lbLabelCompra.ForeColor = System.Drawing.Color.Black;
		this.lbLabelCompra.Location = new System.Drawing.Point(11, 269);
		this.lbLabelCompra.Name = "lbLabelCompra";
		this.lbLabelCompra.Size = new System.Drawing.Size(155, 17);
		this.lbLabelCompra.TabIndex = 114;
		this.lbLabelCompra.Text = "Precio Compra con IGV:";
		this.txtPrecioCom.BackColor = System.Drawing.Color.White;
		this.txtPrecioCom.ForeColor = System.Drawing.Color.Black;
		this.txtPrecioCom.Location = new System.Drawing.Point(172, 266);
		this.txtPrecioCom.Name = "txtPrecioCom";
		this.txtPrecioCom.Size = new System.Drawing.Size(116, 20);
		this.txtPrecioCom.TabIndex = 115;
		this.txtPrecioCom.Text = "0.00";
		this.txtPrecioCom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtPrecioVen.BackColor = System.Drawing.Color.White;
		this.txtPrecioVen.ForeColor = System.Drawing.Color.Black;
		this.txtPrecioVen.Location = new System.Drawing.Point(172, 292);
		this.txtPrecioVen.Name = "txtPrecioVen";
		this.txtPrecioVen.Size = new System.Drawing.Size(115, 20);
		this.txtPrecioVen.TabIndex = 113;
		this.txtPrecioVen.Text = "0.00";
		this.txtPrecioVen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtPrecioVen.TextChanged += new System.EventHandler(txtPrecioVen_TextChanged);
		this.txtPrecioVen.Leave += new System.EventHandler(txtPrecioVen_Leave);
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(548, 273);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(18, 17);
		this.label14.TabIndex = 59;
		this.label14.Text = "%";
		this.label14.Visible = false;
		this.linkConfiguraUnidadesEquivalentes.AutoSize = true;
		this.linkConfiguraUnidadesEquivalentes.Location = new System.Drawing.Point(37, 389);
		this.linkConfiguraUnidadesEquivalentes.Name = "linkConfiguraUnidadesEquivalentes";
		this.linkConfiguraUnidadesEquivalentes.Size = new System.Drawing.Size(277, 17);
		this.linkConfiguraUnidadesEquivalentes.TabIndex = 58;
		this.linkConfiguraUnidadesEquivalentes.TabStop = true;
		this.linkConfiguraUnidadesEquivalentes.Text = "Configurar Unidades Equivalentes y Precios";
		this.linkConfiguraUnidadesEquivalentes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkConfiguraUnidadesEquivalentes_LinkClicked);
		this.txtPeso.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtPeso.Location = new System.Drawing.Point(500, 121);
		this.txtPeso.Name = "txtPeso";
		this.txtPeso.Size = new System.Drawing.Size(84, 20);
		this.txtPeso.TabIndex = 56;
		this.txtPeso.Text = "0.00";
		this.txtPeso.Visible = false;
		this.txtPeso.TextChanged += new System.EventHandler(txtPeso_TextChanged);
		this.txtPeso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPeso_KeyPress);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(437, 124);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(75, 17);
		this.label12.TabIndex = 57;
		this.label12.Text = "Peso (Kg) :";
		this.label12.Visible = false;
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnCancelar.ImageIndex = 5;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(474, 374);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(84, 32);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "Salir";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(363, 374);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(105, 32);
		this.btnGuardar.TabIndex = 1;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnAceptar_Click);
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(295, 272);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(135, 17);
		this.label31.TabIndex = 16;
		this.label31.Text = "Máximo Porc. dscto:";
		this.label31.Visible = false;
		this.txtMaxPorcDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtMaxPorcDesc.Location = new System.Drawing.Point(440, 269);
		this.txtMaxPorcDesc.MaxLength = 3;
		this.txtMaxPorcDesc.Name = "txtMaxPorcDesc";
		this.txtMaxPorcDesc.Size = new System.Drawing.Size(102, 20);
		this.txtMaxPorcDesc.TabIndex = 18;
		this.txtMaxPorcDesc.Text = "0.00";
		this.txtMaxPorcDesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtMaxPorcDesc.Visible = false;
		this.txtMaxPorcDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
		this.txtMaxPorcDesc.Leave += new System.EventHandler(txtMaxPorcDesc_Leave);
		this.txtPrecioCata.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtPrecioCata.Location = new System.Drawing.Point(440, 295);
		this.txtPrecioCata.MaxLength = 9;
		this.txtPrecioCata.Name = "txtPrecioCata";
		this.txtPrecioCata.Size = new System.Drawing.Size(144, 20);
		this.txtPrecioCata.TabIndex = 17;
		this.txtPrecioCata.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtPrecioCata.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecioCata_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(295, 299);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(113, 17);
		this.label9.TabIndex = 52;
		this.label9.Text = "Precio Catálogo :";
		this.txtComision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComision.Location = new System.Drawing.Point(500, 154);
		this.txtComision.Name = "txtComision";
		this.txtComision.Size = new System.Drawing.Size(63, 20);
		this.txtComision.TabIndex = 9;
		this.txtComision.Text = "0.00";
		this.txtComision.Visible = false;
		this.txtComision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComision_KeyPress);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(437, 156);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(72, 17);
		this.label7.TabIndex = 49;
		this.label7.Text = "Comisión :";
		this.label7.Visible = false;
		this.cbDetraccion.AutoSize = true;
		this.cbDetraccion.Location = new System.Drawing.Point(440, 184);
		this.cbDetraccion.Name = "cbDetraccion";
		this.cbDetraccion.Size = new System.Drawing.Size(149, 21);
		this.cbDetraccion.TabIndex = 13;
		this.cbDetraccion.Text = "Afecto a Detraccion";
		this.cbDetraccion.UseVisualStyleBackColor = true;
		this.cbDetraccion.Visible = false;
		this.btnUnidad.Location = new System.Drawing.Point(411, 214);
		this.btnUnidad.Name = "btnUnidad";
		this.btnUnidad.Size = new System.Drawing.Size(23, 23);
		this.btnUnidad.TabIndex = 14;
		this.btnUnidad.Text = ">";
		this.btnUnidad.UseVisualStyleBackColor = true;
		this.btnUnidad.Click += new System.EventHandler(btnUnidad_Click);
		this.btnMarca.Location = new System.Drawing.Point(100, 232);
		this.btnMarca.Name = "btnMarca";
		this.btnMarca.Size = new System.Drawing.Size(23, 23);
		this.btnMarca.TabIndex = 40;
		this.btnMarca.Text = ">";
		this.btnMarca.UseVisualStyleBackColor = true;
		this.btnMarca.Click += new System.EventHandler(btnMarca_Click);
		this.btnGrupo.Enabled = false;
		this.btnGrupo.Location = new System.Drawing.Point(100, 205);
		this.btnGrupo.Name = "btnGrupo";
		this.btnGrupo.Size = new System.Drawing.Size(23, 23);
		this.btnGrupo.TabIndex = 39;
		this.btnGrupo.Text = ">";
		this.btnGrupo.UseVisualStyleBackColor = true;
		this.btnGrupo.Click += new System.EventHandler(btnGrupo_Click);
		this.btnLinea.Enabled = false;
		this.btnLinea.Location = new System.Drawing.Point(100, 179);
		this.btnLinea.Name = "btnLinea";
		this.btnLinea.Size = new System.Drawing.Size(23, 23);
		this.btnLinea.TabIndex = 38;
		this.btnLinea.Text = ">";
		this.btnLinea.UseVisualStyleBackColor = true;
		this.btnLinea.Click += new System.EventHandler(btnLinea_Click);
		this.btnFamilia.Location = new System.Drawing.Point(100, 151);
		this.btnFamilia.Name = "btnFamilia";
		this.btnFamilia.Size = new System.Drawing.Size(23, 23);
		this.btnFamilia.TabIndex = 37;
		this.btnFamilia.Text = ">";
		this.btnFamilia.UseVisualStyleBackColor = true;
		this.btnFamilia.Click += new System.EventHandler(btnFamilia_Click);
		this.btnTipoArticulo.Location = new System.Drawing.Point(100, 123);
		this.btnTipoArticulo.Name = "btnTipoArticulo";
		this.btnTipoArticulo.Size = new System.Drawing.Size(23, 23);
		this.btnTipoArticulo.TabIndex = 36;
		this.btnTipoArticulo.Text = ">";
		this.btnTipoArticulo.UseVisualStyleBackColor = true;
		this.btnTipoArticulo.Click += new System.EventHandler(btnTipoArticulo_Click);
		this.label38.AutoSize = true;
		this.label38.Location = new System.Drawing.Point(295, 245);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(98, 17);
		this.label38.TabIndex = 35;
		this.label38.Text = "Control Stock :";
		this.cbControlStock.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cbControlStock.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cbControlStock.DisplayMember = "1,2,3,4";
		this.cbControlStock.FormattingEnabled = true;
		this.cbControlStock.Items.AddRange(new object[1] { "LIBRE" });
		this.cbControlStock.Location = new System.Drawing.Point(440, 242);
		this.cbControlStock.Name = "cbControlStock";
		this.cbControlStock.Size = new System.Drawing.Size(144, 21);
		this.cbControlStock.TabIndex = 16;
		this.cmbUnidadBase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbUnidadBase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbUnidadBase.FormattingEnabled = true;
		this.cmbUnidadBase.Location = new System.Drawing.Point(440, 216);
		this.cmbUnidadBase.Name = "cmbUnidadBase";
		this.cmbUnidadBase.Size = new System.Drawing.Size(144, 21);
		this.cmbUnidadBase.TabIndex = 15;
		this.cmbUnidadBase.Tag = "1";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(295, 219);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(101, 17);
		this.label13.TabIndex = 26;
		this.label13.Text = "Unidad Base * :";
		this.cbTipoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cbTipoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cbTipoArticulo.FormattingEnabled = true;
		this.cbTipoArticulo.Location = new System.Drawing.Point(129, 125);
		this.cbTipoArticulo.Name = "cbTipoArticulo";
		this.cbTipoArticulo.Size = new System.Drawing.Size(159, 21);
		this.cbTipoArticulo.TabIndex = 4;
		this.cbTipoArticulo.Tag = "1";
		this.cbTipoArticulo.SelectedIndexChanged += new System.EventHandler(cbTipoArticulo_SelectedIndexChanged);
		this.label36.AutoSize = true;
		this.label36.Location = new System.Drawing.Point(15, 128);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(101, 17);
		this.label36.TabIndex = 24;
		this.label36.Text = "Tipo Artículo * :";
		this.cbMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cbMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cbMarca.FormattingEnabled = true;
		this.cbMarca.Location = new System.Drawing.Point(129, 234);
		this.cbMarca.Name = "cbMarca";
		this.cbMarca.Size = new System.Drawing.Size(159, 21);
		this.cbMarca.TabIndex = 8;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(15, 237);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(54, 17);
		this.label10.TabIndex = 22;
		this.label10.Text = "Marca :";
		this.cbGrupo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cbGrupo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cbGrupo.Enabled = false;
		this.cbGrupo.FormattingEnabled = true;
		this.cbGrupo.Location = new System.Drawing.Point(129, 207);
		this.cbGrupo.Name = "cbGrupo";
		this.cbGrupo.Size = new System.Drawing.Size(159, 21);
		this.cbGrupo.TabIndex = 7;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(15, 210);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(60, 17);
		this.label6.TabIndex = 20;
		this.label6.Text = "Modelo :";
		this.cbLinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cbLinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cbLinea.Enabled = false;
		this.cbLinea.FormattingEnabled = true;
		this.cbLinea.Location = new System.Drawing.Point(129, 180);
		this.cbLinea.Name = "cbLinea";
		this.cbLinea.Size = new System.Drawing.Size(159, 21);
		this.cbLinea.TabIndex = 6;
		this.cbLinea.SelectionChangeCommitted += new System.EventHandler(cbLinea_SelectionChangeCommitted);
		this.cbLinea.KeyDown += new System.Windows.Forms.KeyEventHandler(cbLinea_KeyDown);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(15, 183);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(75, 17);
		this.label5.TabIndex = 18;
		this.label5.Text = "Categoria :";
		this.cbFamilia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cbFamilia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cbFamilia.FormattingEnabled = true;
		this.cbFamilia.Location = new System.Drawing.Point(129, 153);
		this.cbFamilia.Name = "cbFamilia";
		this.cbFamilia.Size = new System.Drawing.Size(159, 21);
		this.cbFamilia.TabIndex = 5;
		this.cbFamilia.Tag = "1";
		this.cbFamilia.SelectionChangeCommitted += new System.EventHandler(cbFamilia_SelectionChangeCommitted);
		this.cbFamilia.KeyDown += new System.Windows.Forms.KeyEventHandler(cbFamilia_KeyDown);
		this.cbFamilia.Leave += new System.EventHandler(cbFamilia_SelectionChangeCommitted);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(15, 157);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(68, 17);
		this.label4.TabIndex = 16;
		this.label4.Text = "Familia * :";
		this.cbEstado.AutoSize = true;
		this.cbEstado.Checked = true;
		this.cbEstado.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbEstado.Location = new System.Drawing.Point(537, -2);
		this.cbEstado.Name = "cbEstado";
		this.cbEstado.Size = new System.Drawing.Size(66, 21);
		this.cbEstado.TabIndex = 1;
		this.cbEstado.Text = "Activo";
		this.cbEstado.UseVisualStyleBackColor = true;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Location = new System.Drawing.Point(18, 83);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(553, 20);
		this.txtNombre.TabIndex = 3;
		this.txtNombre.Tag = "1";
		this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNombre_KeyPress);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(15, 67);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(73, 17);
		this.label3.TabIndex = 11;
		this.label3.Text = "Nombre * :";
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Location = new System.Drawing.Point(90, 41);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.Size = new System.Drawing.Size(100, 20);
		this.txtReferencia.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(87, 25);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(58, 17);
		this.label2.TabIndex = 9;
		this.label2.Text = "Código: ";
		this.txtCodProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodProducto.Enabled = false;
		this.txtCodProducto.Location = new System.Drawing.Point(18, 41);
		this.txtCodProducto.Name = "txtCodProducto";
		this.txtCodProducto.ReadOnly = true;
		this.txtCodProducto.Size = new System.Drawing.Size(66, 20);
		this.txtCodProducto.TabIndex = 0;
		this.txtCodProducto.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(42, 17);
		this.label1.TabIndex = 7;
		this.label1.Text = "Item: ";
		this.label1.Visible = false;
		this.txtUbicacion.Location = new System.Drawing.Point(220, 414);
		this.txtUbicacion.Name = "txtUbicacion";
		this.txtUbicacion.Size = new System.Drawing.Size(58, 20);
		this.txtUbicacion.TabIndex = 129;
		this.txtUbicacion.Visible = false;
		this.radPageView1.Controls.Add(this.tab1);
		this.radPageView1.Controls.Add(this.tab3);
		this.radPageView1.Controls.Add(this.tab4);
		this.radPageView1.Controls.Add(this.tab2);
		this.radPageView1.Controls.Add(this.tab5);
		this.radPageView1.Controls.Add(this.tab6);
		this.radPageView1.Controls.Add(this.tab7);
		this.radPageView1.DefaultPage = this.tab1;
		this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.radPageView1.Location = new System.Drawing.Point(0, 0);
		this.radPageView1.Name = "radPageView1";
		this.radPageView1.SelectedPage = this.tab7;
		this.radPageView1.Size = new System.Drawing.Size(621, 501);
		this.radPageView1.TabIndex = 136;
		this.radPageView1.ThemeName = "Material";
		this.tab1.BackColor = System.Drawing.Color.Gainsboro;
		this.tab1.Controls.Add(this.groupBox1);
		this.tab1.Controls.Add(this.txtUbicacion);
		this.tab1.ItemSize = new System.Drawing.SizeF(110f, 49f);
		this.tab1.Location = new System.Drawing.Point(6, 55);
		this.tab1.Name = "tab1";
		this.tab1.Size = new System.Drawing.Size(609, 440);
		this.tab1.Text = "Propiedades";
		this.tab3.BackColor = System.Drawing.Color.Gainsboro;
		this.tab3.Controls.Add(this.txtGastosadic);
		this.tab3.Controls.Add(this.txtComi3);
		this.tab3.Controls.Add(this.radLabel12);
		this.tab3.Controls.Add(this.radLabel11);
		this.tab3.Controls.Add(this.txtGastosadmin);
		this.tab3.Controls.Add(this.radLabel13);
		this.tab3.Controls.Add(this.txtComi2);
		this.tab3.Controls.Add(this.radLabel10);
		this.tab3.Controls.Add(this.txtComi1);
		this.tab3.Controls.Add(this.radLabel9);
		this.tab3.Controls.Add(this.txtEstiva);
		this.tab3.Controls.Add(this.radLabel8);
		this.tab3.Controls.Add(this.radLabel7);
		this.tab3.Controls.Add(this.radLabel5);
		this.tab3.Controls.Add(this.txtDesestiva);
		this.tab3.Controls.Add(this.txtFlete);
		this.tab3.ItemSize = new System.Drawing.SizeF(126f, 49f);
		this.tab3.Location = new System.Drawing.Point(6, 55);
		this.tab3.Name = "tab3";
		this.tab3.Size = new System.Drawing.Size(609, 440);
		this.tab3.Text = "Admin. Precios";
		this.txtGastosadic.Location = new System.Drawing.Point(443, 85);
		this.txtGastosadic.Name = "txtGastosadic";
		this.txtGastosadic.Size = new System.Drawing.Size(148, 36);
		this.txtGastosadic.TabIndex = 9;
		this.txtGastosadic.ThemeName = "Material";
		this.txtComi3.Location = new System.Drawing.Point(138, 312);
		this.txtComi3.Name = "txtComi3";
		this.txtComi3.Size = new System.Drawing.Size(148, 36);
		this.txtComi3.TabIndex = 7;
		this.txtComi3.ThemeName = "Material";
		this.radLabel12.Location = new System.Drawing.Point(339, 93);
		this.radLabel12.Name = "radLabel12";
		this.radLabel12.Size = new System.Drawing.Size(89, 21);
		this.radLabel12.TabIndex = 11;
		this.radLabel12.Text = "Gastos Adic:";
		this.radLabel12.ThemeName = "Material";
		this.radLabel11.Location = new System.Drawing.Point(32, 319);
		this.radLabel11.Name = "radLabel11";
		this.radLabel11.Size = new System.Drawing.Size(84, 21);
		this.radLabel11.TabIndex = 8;
		this.radLabel11.Text = "Comision 3:";
		this.radLabel11.ThemeName = "Material";
		this.txtGastosadmin.Location = new System.Drawing.Point(443, 32);
		this.txtGastosadmin.Name = "txtGastosadmin";
		this.txtGastosadmin.Size = new System.Drawing.Size(148, 36);
		this.txtGastosadmin.TabIndex = 10;
		this.txtGastosadmin.ThemeName = "Material";
		this.radLabel13.Location = new System.Drawing.Point(326, 39);
		this.radLabel13.Name = "radLabel13";
		this.radLabel13.Size = new System.Drawing.Size(102, 21);
		this.radLabel13.TabIndex = 12;
		this.radLabel13.Text = "Gastos Admin:";
		this.radLabel13.ThemeName = "Material";
		this.txtComi2.Location = new System.Drawing.Point(138, 253);
		this.txtComi2.Name = "txtComi2";
		this.txtComi2.Size = new System.Drawing.Size(148, 36);
		this.txtComi2.TabIndex = 7;
		this.txtComi2.ThemeName = "Material";
		this.radLabel10.Location = new System.Drawing.Point(32, 260);
		this.radLabel10.Name = "radLabel10";
		this.radLabel10.Size = new System.Drawing.Size(84, 21);
		this.radLabel10.TabIndex = 8;
		this.radLabel10.Text = "Comision 2:";
		this.radLabel10.ThemeName = "Material";
		this.txtComi1.Location = new System.Drawing.Point(138, 197);
		this.txtComi1.Name = "txtComi1";
		this.txtComi1.Size = new System.Drawing.Size(148, 36);
		this.txtComi1.TabIndex = 5;
		this.txtComi1.ThemeName = "Material";
		this.radLabel9.Location = new System.Drawing.Point(32, 204);
		this.radLabel9.Name = "radLabel9";
		this.radLabel9.Size = new System.Drawing.Size(84, 21);
		this.radLabel9.TabIndex = 6;
		this.radLabel9.Text = "Comision 1:";
		this.radLabel9.ThemeName = "Material";
		this.txtEstiva.Location = new System.Drawing.Point(136, 141);
		this.txtEstiva.Name = "txtEstiva";
		this.txtEstiva.Size = new System.Drawing.Size(148, 36);
		this.txtEstiva.TabIndex = 3;
		this.txtEstiva.ThemeName = "Material";
		this.radLabel8.Location = new System.Drawing.Point(68, 149);
		this.radLabel8.Name = "radLabel8";
		this.radLabel8.Size = new System.Drawing.Size(50, 21);
		this.radLabel8.TabIndex = 4;
		this.radLabel8.Text = "Estiva:";
		this.radLabel8.ThemeName = "Material";
		this.radLabel7.Location = new System.Drawing.Point(44, 93);
		this.radLabel7.Name = "radLabel7";
		this.radLabel7.Size = new System.Drawing.Size(74, 21);
		this.radLabel7.TabIndex = 3;
		this.radLabel7.Text = "Desestiva:";
		this.radLabel7.ThemeName = "Material";
		this.radLabel5.Location = new System.Drawing.Point(11, 41);
		this.radLabel5.Name = "radLabel5";
		this.radLabel5.Size = new System.Drawing.Size(107, 21);
		this.radLabel5.TabIndex = 1;
		this.radLabel5.Text = "Flete Estimado:";
		this.radLabel5.ThemeName = "Material";
		this.txtDesestiva.Location = new System.Drawing.Point(138, 85);
		this.txtDesestiva.Name = "txtDesestiva";
		this.txtDesestiva.Size = new System.Drawing.Size(148, 36);
		this.txtDesestiva.TabIndex = 2;
		this.txtDesestiva.ThemeName = "Material";
		this.txtFlete.Location = new System.Drawing.Point(138, 32);
		this.txtFlete.Name = "txtFlete";
		this.txtFlete.Size = new System.Drawing.Size(148, 36);
		this.txtFlete.TabIndex = 0;
		this.txtFlete.ThemeName = "Material";
		this.tab4.BackColor = System.Drawing.Color.Gainsboro;
		this.tab4.Controls.Add(this.cmbProveedor3);
		this.tab4.Controls.Add(this.cmbProveedor2);
		this.tab4.Controls.Add(this.cmbProveedor1);
		this.tab4.Controls.Add(this.radLabel16);
		this.tab4.Controls.Add(this.radLabel15);
		this.tab4.Controls.Add(this.radLabel14);
		this.tab4.ItemSize = new System.Drawing.SizeF(110f, 49f);
		this.tab4.Location = new System.Drawing.Point(6, 55);
		this.tab4.Name = "tab4";
		this.tab4.Size = new System.Drawing.Size(609, 440);
		this.tab4.Text = "Proveedores";
		this.cmbProveedor3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbProveedor3.Location = new System.Drawing.Point(214, 149);
		this.cmbProveedor3.Name = "cmbProveedor3";
		this.cmbProveedor3.Size = new System.Drawing.Size(309, 36);
		this.cmbProveedor3.TabIndex = 5;
		this.cmbProveedor3.ThemeName = "Material";
		this.cmbProveedor2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbProveedor2.Location = new System.Drawing.Point(214, 99);
		this.cmbProveedor2.Name = "cmbProveedor2";
		this.cmbProveedor2.Size = new System.Drawing.Size(309, 36);
		this.cmbProveedor2.TabIndex = 4;
		this.cmbProveedor2.ThemeName = "Material";
		this.cmbProveedor1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbProveedor1.Location = new System.Drawing.Point(214, 46);
		this.cmbProveedor1.Name = "cmbProveedor1";
		this.cmbProveedor1.Size = new System.Drawing.Size(309, 36);
		this.cmbProveedor1.TabIndex = 3;
		this.cmbProveedor1.ThemeName = "Material";
		this.radLabel16.Location = new System.Drawing.Point(108, 156);
		this.radLabel16.Name = "radLabel16";
		this.radLabel16.Size = new System.Drawing.Size(89, 21);
		this.radLabel16.TabIndex = 2;
		this.radLabel16.Text = "Proveedor 3:";
		this.radLabel16.ThemeName = "Material";
		this.radLabel15.Location = new System.Drawing.Point(108, 106);
		this.radLabel15.Name = "radLabel15";
		this.radLabel15.Size = new System.Drawing.Size(89, 21);
		this.radLabel15.TabIndex = 1;
		this.radLabel15.Text = "Proveedor 2:";
		this.radLabel15.ThemeName = "Material";
		this.radLabel14.Location = new System.Drawing.Point(108, 53);
		this.radLabel14.Name = "radLabel14";
		this.radLabel14.Size = new System.Drawing.Size(89, 21);
		this.radLabel14.TabIndex = 0;
		this.radLabel14.Text = "Proveedor 1:";
		this.radLabel14.ThemeName = "Material";
		this.tab2.BackColor = System.Drawing.Color.Gainsboro;
		this.tab2.Controls.Add(this.btnEliminaStock);
		this.tab2.Controls.Add(this.btnGuardastock);
		this.tab2.Controls.Add(this.cmbAlmacenes);
		this.tab2.Controls.Add(this.radLabel6);
		this.tab2.Controls.Add(this.lblnombreprod);
		this.tab2.Controls.Add(this.radLabel4);
		this.tab2.Controls.Add(this.radLabel3);
		this.tab2.Controls.Add(this.txtCapmax);
		this.tab2.Controls.Add(this.radLabel2);
		this.tab2.Controls.Add(this.txtStockmin);
		this.tab2.Controls.Add(this.radLabel1);
		this.tab2.Controls.Add(this.txtStockmax);
		this.tab2.Controls.Add(this.dgvStocks);
		this.tab2.Enabled = false;
		this.tab2.ItemSize = new System.Drawing.SizeF(116f, 49f);
		this.tab2.Location = new System.Drawing.Point(6, 55);
		this.tab2.Name = "tab2";
		this.tab2.Size = new System.Drawing.Size(609, 440);
		this.tab2.Text = "Control Stock";
		this.btnEliminaStock.Enabled = false;
		this.btnEliminaStock.Location = new System.Drawing.Point(450, 53);
		this.btnEliminaStock.Name = "btnEliminaStock";
		this.btnEliminaStock.Size = new System.Drawing.Size(120, 36);
		this.btnEliminaStock.TabIndex = 9;
		this.btnEliminaStock.Text = "Eliminar";
		this.btnEliminaStock.ThemeName = "Material";
		this.btnEliminaStock.Click += new System.EventHandler(btnEliminaStock_Click);
		this.btnGuardastock.Enabled = false;
		this.btnGuardastock.Location = new System.Drawing.Point(450, 13);
		this.btnGuardastock.Name = "btnGuardastock";
		this.btnGuardastock.Size = new System.Drawing.Size(120, 36);
		this.btnGuardastock.TabIndex = 8;
		this.btnGuardastock.Text = "Guardar";
		this.btnGuardastock.ThemeName = "Material";
		this.btnGuardastock.Click += new System.EventHandler(btnGuardastock_Click);
		this.cmbAlmacenes.Location = new System.Drawing.Point(157, 73);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(213, 36);
		this.cmbAlmacenes.TabIndex = 7;
		this.cmbAlmacenes.ThemeName = "Material";
		this.radLabel6.Location = new System.Drawing.Point(65, 81);
		this.radLabel6.Name = "radLabel6";
		this.radLabel6.Size = new System.Drawing.Size(68, 21);
		this.radLabel6.TabIndex = 4;
		this.radLabel6.Text = "Almacen:";
		this.radLabel6.ThemeName = "Material";
		this.lblnombreprod.Location = new System.Drawing.Point(157, 26);
		this.lblnombreprod.Name = "lblnombreprod";
		this.lblnombreprod.Size = new System.Drawing.Size(111, 21);
		this.lblnombreprod.TabIndex = 4;
		this.lblnombreprod.Text = "----------------------";
		this.lblnombreprod.ThemeName = "Material";
		this.radLabel4.Location = new System.Drawing.Point(67, 25);
		this.radLabel4.Name = "radLabel4";
		this.radLabel4.Size = new System.Drawing.Size(74, 21);
		this.radLabel4.TabIndex = 3;
		this.radLabel4.Text = "Producto: ";
		this.radLabel4.ThemeName = "Material";
		this.radLabel3.Location = new System.Drawing.Point(376, 137);
		this.radLabel3.Name = "radLabel3";
		this.radLabel3.Size = new System.Drawing.Size(131, 21);
		this.radLabel3.TabIndex = 6;
		this.radLabel3.Text = "Capacidad Maxima";
		this.radLabel3.ThemeName = "Material";
		this.txtCapmax.Location = new System.Drawing.Point(376, 164);
		this.txtCapmax.Name = "txtCapmax";
		this.txtCapmax.Size = new System.Drawing.Size(117, 36);
		this.txtCapmax.TabIndex = 5;
		this.txtCapmax.ThemeName = "Material";
		this.radLabel2.Location = new System.Drawing.Point(223, 137);
		this.radLabel2.Name = "radLabel2";
		this.radLabel2.Size = new System.Drawing.Size(100, 21);
		this.radLabel2.TabIndex = 4;
		this.radLabel2.Text = "Stock Maximo";
		this.radLabel2.ThemeName = "Material";
		this.txtStockmin.Location = new System.Drawing.Point(58, 164);
		this.txtStockmin.Name = "txtStockmin";
		this.txtStockmin.Size = new System.Drawing.Size(111, 36);
		this.txtStockmin.TabIndex = 3;
		this.txtStockmin.ThemeName = "Material";
		this.radLabel1.Location = new System.Drawing.Point(62, 137);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(96, 21);
		this.radLabel1.TabIndex = 2;
		this.radLabel1.Text = "Stock Minimo";
		this.radLabel1.ThemeName = "Material";
		this.txtStockmax.Location = new System.Drawing.Point(220, 164);
		this.txtStockmax.Name = "txtStockmax";
		this.txtStockmax.Size = new System.Drawing.Size(107, 36);
		this.txtStockmax.TabIndex = 1;
		this.txtStockmax.ThemeName = "Material";
		this.dgvStocks.AutoSizeRows = true;
		this.dgvStocks.Location = new System.Drawing.Point(16, 230);
		this.dgvStocks.MasterTemplate.AllowAddNewRow = false;
		this.dgvStocks.MasterTemplate.AllowDragToGroup = false;
		this.dgvStocks.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "id";
		gridViewTextBoxColumn1.HeaderText = "id";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "id";
		gridViewTextBoxColumn2.FieldName = "idproducto";
		gridViewTextBoxColumn2.HeaderText = "idproducto";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "idproducto";
		gridViewTextBoxColumn3.FieldName = "idalmacen";
		gridViewTextBoxColumn3.HeaderText = "idalmacen";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "idalmacen";
		gridViewTextBoxColumn4.FieldName = "almacen";
		gridViewTextBoxColumn4.HeaderText = "Almacen";
		gridViewTextBoxColumn4.Name = "almacen";
		gridViewTextBoxColumn4.Width = 138;
		gridViewTextBoxColumn5.FieldName = "stock_min";
		gridViewTextBoxColumn5.HeaderText = "Stock Minimo";
		gridViewTextBoxColumn5.Name = "stock_min";
		gridViewTextBoxColumn5.Width = 138;
		gridViewTextBoxColumn6.FieldName = "stock_max";
		gridViewTextBoxColumn6.HeaderText = "Stock Maximo";
		gridViewTextBoxColumn6.Name = "stock_max";
		gridViewTextBoxColumn6.Width = 138;
		gridViewTextBoxColumn7.FieldName = "cap_max";
		gridViewTextBoxColumn7.HeaderText = "Capacidad Maxima";
		gridViewTextBoxColumn7.Name = "cap_max";
		gridViewTextBoxColumn7.Width = 139;
		this.dgvStocks.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7);
		this.dgvStocks.MasterTemplate.EnableGrouping = false;
		this.dgvStocks.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvStocks.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvStocks.Name = "dgvStocks";
		this.dgvStocks.ReadOnly = true;
		this.dgvStocks.Size = new System.Drawing.Size(553, 172);
		this.dgvStocks.TabIndex = 0;
		this.dgvStocks.ThemeName = "Material";
		this.dgvStocks.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvStocks_CellClick);
		this.tab5.BackColor = System.Drawing.Color.Gainsboro;
		this.tab5.Controls.Add(this.dgvListaPrecios);
		this.tab5.Controls.Add(this.btnRecargaControlPrecio);
		this.tab5.Controls.Add(this.btnEliminaControlPrecio);
		this.tab5.Controls.Add(this.btnGuardaControlPrecio);
		this.tab5.Controls.Add(this.minimo);
		this.tab5.Controls.Add(this.maximo);
		this.tab5.Controls.Add(this.cmbPreciosV);
		this.tab5.Controls.Add(this.cmbUnidadM);
		this.tab5.Controls.Add(this.radLabel20);
		this.tab5.Controls.Add(this.radLabel19);
		this.tab5.Controls.Add(this.radLabel18);
		this.tab5.Controls.Add(this.radLabel17);
		this.tab5.Enabled = false;
		this.tab5.ItemSize = new System.Drawing.SizeF(128f, 49f);
		this.tab5.Location = new System.Drawing.Point(6, 55);
		this.tab5.Name = "tab5";
		this.tab5.Size = new System.Drawing.Size(609, 440);
		this.tab5.Text = "Control Precios";
		this.dgvListaPrecios.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.dgvListaPrecios.Location = new System.Drawing.Point(0, 196);
		this.dgvListaPrecios.MasterTemplate.AllowAddNewRow = false;
		this.dgvListaPrecios.MasterTemplate.AllowDragToGroup = false;
		this.dgvListaPrecios.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn8.FieldName = "id";
		gridViewTextBoxColumn8.HeaderText = "codigo";
		gridViewTextBoxColumn8.Name = "id";
		gridViewTextBoxColumn8.Width = 112;
		gridViewTextBoxColumn9.FieldName = "unidad";
		gridViewTextBoxColumn9.HeaderText = "Unidad Medida";
		gridViewTextBoxColumn9.Name = "unidad";
		gridViewTextBoxColumn9.Width = 112;
		gridViewTextBoxColumn10.FieldName = "tipo";
		gridViewTextBoxColumn10.HeaderText = "Tipo";
		gridViewTextBoxColumn10.Name = "tipo";
		gridViewTextBoxColumn10.Width = 112;
		gridViewTextBoxColumn11.FieldName = "min";
		gridViewTextBoxColumn11.HeaderText = "Minimo";
		gridViewTextBoxColumn11.Name = "min";
		gridViewTextBoxColumn11.Width = 112;
		gridViewTextBoxColumn12.FieldName = "max";
		gridViewTextBoxColumn12.HeaderText = "Maximo";
		gridViewTextBoxColumn12.Name = "max";
		gridViewTextBoxColumn12.Width = 113;
		this.dgvListaPrecios.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12);
		this.dgvListaPrecios.MasterTemplate.EnableGrouping = false;
		this.dgvListaPrecios.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.dgvListaPrecios.Name = "dgvListaPrecios";
		this.dgvListaPrecios.ReadOnly = true;
		this.dgvListaPrecios.ShowGroupPanel = false;
		this.dgvListaPrecios.Size = new System.Drawing.Size(609, 244);
		this.dgvListaPrecios.TabIndex = 9;
		this.dgvListaPrecios.ThemeName = "Material";
		this.dgvListaPrecios.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvListaPrecios_CellClick);
		this.btnRecargaControlPrecio.Location = new System.Drawing.Point(446, 145);
		this.btnRecargaControlPrecio.Name = "btnRecargaControlPrecio";
		this.btnRecargaControlPrecio.Size = new System.Drawing.Size(120, 36);
		this.btnRecargaControlPrecio.TabIndex = 8;
		this.btnRecargaControlPrecio.Text = "Recargar";
		this.btnRecargaControlPrecio.ThemeName = "Material";
		this.btnRecargaControlPrecio.Click += new System.EventHandler(btnRecargaControlPrecio_Click);
		this.btnEliminaControlPrecio.Location = new System.Drawing.Point(176, 145);
		this.btnEliminaControlPrecio.Name = "btnEliminaControlPrecio";
		this.btnEliminaControlPrecio.Size = new System.Drawing.Size(120, 36);
		this.btnEliminaControlPrecio.TabIndex = 8;
		this.btnEliminaControlPrecio.Text = "Eliminar";
		this.btnEliminaControlPrecio.ThemeName = "Material";
		this.btnEliminaControlPrecio.Click += new System.EventHandler(btnEliminaControlPrecio_Click);
		this.btnGuardaControlPrecio.Location = new System.Drawing.Point(42, 145);
		this.btnGuardaControlPrecio.Name = "btnGuardaControlPrecio";
		this.btnGuardaControlPrecio.Size = new System.Drawing.Size(120, 36);
		this.btnGuardaControlPrecio.TabIndex = 7;
		this.btnGuardaControlPrecio.Text = "Guardar";
		this.btnGuardaControlPrecio.ThemeName = "Material";
		this.btnGuardaControlPrecio.Click += new System.EventHandler(btnGuardaControlPrecio_Click);
		this.minimo.Location = new System.Drawing.Point(456, 87);
		this.minimo.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.minimo.Name = "minimo";
		this.minimo.Size = new System.Drawing.Size(110, 36);
		this.minimo.TabIndex = 6;
		this.minimo.TabStop = false;
		this.minimo.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
		this.minimo.ThemeName = "Material";
		this.maximo.Location = new System.Drawing.Point(456, 34);
		this.maximo.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.maximo.Name = "maximo";
		this.maximo.Size = new System.Drawing.Size(110, 36);
		this.maximo.TabIndex = 5;
		this.maximo.TabStop = false;
		this.maximo.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
		this.maximo.ThemeName = "Material";
		this.cmbPreciosV.Location = new System.Drawing.Point(145, 87);
		this.cmbPreciosV.Name = "cmbPreciosV";
		this.cmbPreciosV.Size = new System.Drawing.Size(172, 36);
		this.cmbPreciosV.TabIndex = 4;
		this.cmbPreciosV.ThemeName = "Material";
		this.cmbUnidadM.Location = new System.Drawing.Point(145, 33);
		this.cmbUnidadM.Name = "cmbUnidadM";
		this.cmbUnidadM.Size = new System.Drawing.Size(172, 36);
		this.cmbUnidadM.TabIndex = 3;
		this.cmbUnidadM.ThemeName = "Material";
		this.cmbUnidadM.SelectedValueChanged += new System.EventHandler(cmbUnidadM_SelectedValueChanged);
		this.radLabel20.Location = new System.Drawing.Point(353, 94);
		this.radLabel20.Name = "radLabel20";
		this.radLabel20.Size = new System.Drawing.Size(93, 21);
		this.radLabel20.TabIndex = 2;
		this.radLabel20.Text = "Cant. Minima";
		this.radLabel20.ThemeName = "Material";
		this.radLabel19.Location = new System.Drawing.Point(353, 40);
		this.radLabel19.Name = "radLabel19";
		this.radLabel19.Size = new System.Drawing.Size(97, 21);
		this.radLabel19.TabIndex = 1;
		this.radLabel19.Text = "Cant. Maxima";
		this.radLabel19.ThemeName = "Material";
		this.radLabel18.Location = new System.Drawing.Point(42, 94);
		this.radLabel18.Name = "radLabel18";
		this.radLabel18.Size = new System.Drawing.Size(90, 21);
		this.radLabel18.TabIndex = 1;
		this.radLabel18.Text = "Precio Venta";
		this.radLabel18.ThemeName = "Material";
		this.radLabel17.Location = new System.Drawing.Point(42, 40);
		this.radLabel17.Name = "radLabel17";
		this.radLabel17.Size = new System.Drawing.Size(52, 21);
		this.radLabel17.TabIndex = 0;
		this.radLabel17.Text = "Unidad";
		this.radLabel17.ThemeName = "Material";
		this.tab6.Controls.Add(this.groupBox2);
		this.tab6.Controls.Add(this.btguardacategorizacion);
		this.tab6.Controls.Add(this.btneliminarcategorizacion);
		this.tab6.Controls.Add(this.txtdescripcion);
		this.tab6.Controls.Add(this.txtcondicion);
		this.tab6.Controls.Add(this.radLabel22);
		this.tab6.Controls.Add(this.btnactualizarcategorizacion);
		this.tab6.Controls.Add(this.radLabel21);
		this.tab6.ItemSize = new System.Drawing.SizeF(126f, 49f);
		this.tab6.Location = new System.Drawing.Point(6, 55);
		this.tab6.Name = "tab6";
		this.tab6.Size = new System.Drawing.Size(609, 440);
		this.tab6.Text = "Categorización";
		this.groupBox2.Controls.Add(this.rgvcategorizacion);
		this.groupBox2.Location = new System.Drawing.Point(-6, 94);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(621, 352);
		this.groupBox2.TabIndex = 17;
		this.groupBox2.TabStop = false;
		this.rgvcategorizacion.BackColor = System.Drawing.Color.White;
		this.rgvcategorizacion.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvcategorizacion.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvcategorizacion.EnableCustomFiltering = true;
		this.rgvcategorizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5f);
		this.rgvcategorizacion.ForeColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.rgvcategorizacion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvcategorizacion.Location = new System.Drawing.Point(3, 20);
		this.rgvcategorizacion.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn13.EnableExpressionEditor = false;
		gridViewTextBoxColumn13.FieldName = "id";
		gridViewTextBoxColumn13.HeaderText = "ID";
		gridViewTextBoxColumn13.Name = "id";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 60;
		gridViewTextBoxColumn14.EnableExpressionEditor = false;
		gridViewTextBoxColumn14.FieldName = "condicion";
		gridViewTextBoxColumn14.HeaderText = "Condicion";
		gridViewTextBoxColumn14.Name = "condicion";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 200;
		gridViewTextBoxColumn15.EnableExpressionEditor = false;
		gridViewTextBoxColumn15.FieldName = "descripcion";
		gridViewTextBoxColumn15.HeaderText = "Descripción";
		gridViewTextBoxColumn15.Name = "descripcion";
		gridViewTextBoxColumn15.Width = 200;
		this.rgvcategorizacion.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15);
		this.rgvcategorizacion.MasterTemplate.EnableCustomFiltering = true;
		this.rgvcategorizacion.MasterTemplate.EnableFiltering = true;
		this.rgvcategorizacion.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvcategorizacion.MasterTemplate.ViewDefinition = tableViewDefinition3;
		this.rgvcategorizacion.Name = "rgvcategorizacion";
		this.rgvcategorizacion.ReadOnly = true;
		this.rgvcategorizacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvcategorizacion.ShowGroupPanel = false;
		this.rgvcategorizacion.Size = new System.Drawing.Size(615, 329);
		this.rgvcategorizacion.TabIndex = 18;
		this.rgvcategorizacion.ThemeName = "TelerikMetroTouch";
		this.rgvcategorizacion.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvcategorizacion_CellClick);
		this.btguardacategorizacion.Location = new System.Drawing.Point(196, 52);
		this.btguardacategorizacion.Name = "btguardacategorizacion";
		this.btguardacategorizacion.Size = new System.Drawing.Size(120, 36);
		this.btguardacategorizacion.TabIndex = 15;
		this.btguardacategorizacion.Text = "Guardar";
		this.btguardacategorizacion.ThemeName = "Material";
		this.btguardacategorizacion.Click += new System.EventHandler(btguardacategorizacion_Click);
		this.btneliminarcategorizacion.Location = new System.Drawing.Point(321, 52);
		this.btneliminarcategorizacion.Name = "btneliminarcategorizacion";
		this.btneliminarcategorizacion.Size = new System.Drawing.Size(120, 36);
		this.btneliminarcategorizacion.TabIndex = 14;
		this.btneliminarcategorizacion.Text = "Eliminar";
		this.btneliminarcategorizacion.ThemeName = "Material";
		this.btneliminarcategorizacion.Click += new System.EventHandler(btneliminarcategorizacion_Click);
		this.txtdescripcion.Location = new System.Drawing.Point(322, 16);
		this.txtdescripcion.Name = "txtdescripcion";
		this.txtdescripcion.Size = new System.Drawing.Size(245, 20);
		this.txtdescripcion.TabIndex = 16;
		this.txtcondicion.Location = new System.Drawing.Point(101, 17);
		this.txtcondicion.Name = "txtcondicion";
		this.txtcondicion.Size = new System.Drawing.Size(118, 20);
		this.txtcondicion.TabIndex = 14;
		this.txtcondicion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.radLabel22.Location = new System.Drawing.Point(225, 16);
		this.radLabel22.Name = "radLabel22";
		this.radLabel22.Size = new System.Drawing.Size(91, 21);
		this.radLabel22.TabIndex = 15;
		this.radLabel22.Text = "Descripción :";
		this.radLabel22.ThemeName = "Material";
		this.btnactualizarcategorizacion.Location = new System.Drawing.Point(447, 52);
		this.btnactualizarcategorizacion.Name = "btnactualizarcategorizacion";
		this.btnactualizarcategorizacion.Size = new System.Drawing.Size(120, 36);
		this.btnactualizarcategorizacion.TabIndex = 13;
		this.btnactualizarcategorizacion.Text = "Actualizar";
		this.btnactualizarcategorizacion.ThemeName = "Material";
		this.btnactualizarcategorizacion.Click += new System.EventHandler(btnactualizarcategorizacion_Click);
		this.radLabel21.Location = new System.Drawing.Point(15, 17);
		this.radLabel21.Name = "radLabel21";
		this.radLabel21.Size = new System.Drawing.Size(80, 21);
		this.radLabel21.TabIndex = 11;
		this.radLabel21.Text = "Condición :";
		this.radLabel21.ThemeName = "Material";
		this.tab7.Controls.Add(this.txthastasituacion);
		this.tab7.Controls.Add(this.lblhasta);
		this.tab7.Controls.Add(this.groupBox3);
		this.tab7.Controls.Add(this.btnguardarsituacion);
		this.tab7.Controls.Add(this.btneliminarsituacion);
		this.tab7.Controls.Add(this.txtdescripcionsituacion);
		this.tab7.Controls.Add(this.txtdesdesituacion);
		this.tab7.Controls.Add(this.radLabel23);
		this.tab7.Controls.Add(this.btnactualizarsituacion);
		this.tab7.Controls.Add(this.radLabel24);
		this.tab7.ItemSize = new System.Drawing.SizeF(89f, 49f);
		this.tab7.Location = new System.Drawing.Point(6, 55);
		this.tab7.Name = "tab7";
		this.tab7.Size = new System.Drawing.Size(609, 440);
		this.tab7.Text = "Situación";
		this.txthastasituacion.Enabled = false;
		this.txthastasituacion.Location = new System.Drawing.Point(406, 12);
		this.txthastasituacion.Name = "txthastasituacion";
		this.txthastasituacion.Size = new System.Drawing.Size(118, 20);
		this.txthastasituacion.TabIndex = 23;
		this.txthastasituacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lblhasta.Enabled = false;
		this.lblhasta.Location = new System.Drawing.Point(320, 12);
		this.lblhasta.Name = "lblhasta";
		this.lblhasta.Size = new System.Drawing.Size(50, 21);
		this.lblhasta.TabIndex = 22;
		this.lblhasta.Text = "hasta :";
		this.lblhasta.ThemeName = "Material";
		this.groupBox3.Controls.Add(this.rgvsituacion);
		this.groupBox3.Location = new System.Drawing.Point(-6, 131);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(621, 304);
		this.groupBox3.TabIndex = 25;
		this.groupBox3.TabStop = false;
		this.rgvsituacion.BackColor = System.Drawing.Color.White;
		this.rgvsituacion.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvsituacion.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvsituacion.EnableCustomFiltering = true;
		this.rgvsituacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5f);
		this.rgvsituacion.ForeColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.rgvsituacion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvsituacion.Location = new System.Drawing.Point(3, 20);
		this.rgvsituacion.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn16.EnableExpressionEditor = false;
		gridViewTextBoxColumn16.FieldName = "desde";
		gridViewTextBoxColumn16.HeaderText = "Desde";
		gridViewTextBoxColumn16.Name = "desde";
		gridViewTextBoxColumn16.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn16.Width = 150;
		gridViewTextBoxColumn17.FieldName = "hasta";
		gridViewTextBoxColumn17.HeaderText = "Hasta";
		gridViewTextBoxColumn17.Name = "hasta";
		gridViewTextBoxColumn17.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn17.Width = 150;
		gridViewTextBoxColumn18.EnableExpressionEditor = false;
		gridViewTextBoxColumn18.FieldName = "descripcion";
		gridViewTextBoxColumn18.HeaderText = "Descripción";
		gridViewTextBoxColumn18.Name = "descripcion";
		gridViewTextBoxColumn18.Width = 220;
		this.rgvsituacion.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18);
		this.rgvsituacion.MasterTemplate.EnableCustomFiltering = true;
		this.rgvsituacion.MasterTemplate.EnableFiltering = true;
		this.rgvsituacion.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvsituacion.MasterTemplate.ViewDefinition = tableViewDefinition4;
		this.rgvsituacion.Name = "rgvsituacion";
		this.rgvsituacion.ReadOnly = true;
		this.rgvsituacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvsituacion.ShowGroupPanel = false;
		this.rgvsituacion.Size = new System.Drawing.Size(615, 281);
		this.rgvsituacion.TabIndex = 18;
		this.rgvsituacion.ThemeName = "TelerikMetroTouch";
		this.rgvsituacion.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvsituacion_CellClick);
		this.btnguardarsituacion.Enabled = false;
		this.btnguardarsituacion.Location = new System.Drawing.Point(155, 77);
		this.btnguardarsituacion.Name = "btnguardarsituacion";
		this.btnguardarsituacion.Size = new System.Drawing.Size(120, 36);
		this.btnguardarsituacion.TabIndex = 22;
		this.btnguardarsituacion.Text = "Guardar";
		this.btnguardarsituacion.ThemeName = "Material";
		this.btnguardarsituacion.Click += new System.EventHandler(btnguardarsituacion_Click);
		this.btneliminarsituacion.Enabled = false;
		this.btneliminarsituacion.Location = new System.Drawing.Point(280, 77);
		this.btneliminarsituacion.Name = "btneliminarsituacion";
		this.btneliminarsituacion.Size = new System.Drawing.Size(120, 36);
		this.btneliminarsituacion.TabIndex = 20;
		this.btneliminarsituacion.Text = "Eliminar";
		this.btneliminarsituacion.ThemeName = "Material";
		this.btneliminarsituacion.Click += new System.EventHandler(btneliminarsituacion_Click);
		this.txtdescripcionsituacion.Enabled = false;
		this.txtdescripcionsituacion.Location = new System.Drawing.Point(136, 39);
		this.txtdescripcionsituacion.Name = "txtdescripcionsituacion";
		this.txtdescripcionsituacion.Size = new System.Drawing.Size(388, 20);
		this.txtdescripcionsituacion.TabIndex = 24;
		this.txtdesdesituacion.Enabled = false;
		this.txtdesdesituacion.Location = new System.Drawing.Point(136, 12);
		this.txtdesdesituacion.Name = "txtdesdesituacion";
		this.txtdesdesituacion.Size = new System.Drawing.Size(118, 20);
		this.txtdesdesituacion.TabIndex = 21;
		this.txtdesdesituacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.radLabel23.Enabled = false;
		this.radLabel23.Location = new System.Drawing.Point(39, 39);
		this.radLabel23.Name = "radLabel23";
		this.radLabel23.Size = new System.Drawing.Size(91, 21);
		this.radLabel23.TabIndex = 23;
		this.radLabel23.Text = "Descripción :";
		this.radLabel23.ThemeName = "Material";
		this.btnactualizarsituacion.Location = new System.Drawing.Point(406, 77);
		this.btnactualizarsituacion.Name = "btnactualizarsituacion";
		this.btnactualizarsituacion.Size = new System.Drawing.Size(120, 36);
		this.btnactualizarsituacion.TabIndex = 19;
		this.btnactualizarsituacion.Text = "Actualizar";
		this.btnactualizarsituacion.ThemeName = "Material";
		this.btnactualizarsituacion.Click += new System.EventHandler(btnactualizarsituacion_Click);
		this.radLabel24.Enabled = false;
		this.radLabel24.Location = new System.Drawing.Point(50, 12);
		this.radLabel24.Name = "radLabel24";
		this.radLabel24.Size = new System.Drawing.Size(54, 21);
		this.radLabel24.TabIndex = 18;
		this.radLabel24.Text = "desde :";
		this.radLabel24.ThemeName = "Material";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.requiredFieldValidator1.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.requiredFieldValidator2.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancelar;
		base.ClientSize = new System.Drawing.Size(621, 501);
		base.Controls.Add(this.radPageView1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmRegistroProducto";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Registro Producto";
		base.Load += new System.EventHandler(frmRegistroProducto_Load);
		base.Shown += new System.EventHandler(frmRegistroProducto_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbUbicacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radPageView1).EndInit();
		this.radPageView1.ResumeLayout(false);
		this.tab1.ResumeLayout(false);
		this.tab1.PerformLayout();
		this.tab3.ResumeLayout(false);
		this.tab3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtGastosadic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtComi3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel12).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel11).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGastosadmin).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel13).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtComi2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel10).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtComi1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel9).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEstiva).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDesestiva).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFlete).EndInit();
		this.tab4.ResumeLayout(false);
		this.tab4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbProveedor3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbProveedor2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbProveedor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel16).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel15).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel14).EndInit();
		this.tab2.ResumeLayout(false);
		this.tab2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnEliminaStock).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardastock).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lblnombreprod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCapmax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStockmin).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStockmax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvStocks.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvStocks).EndInit();
		this.tab5.ResumeLayout(false);
		this.tab5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvListaPrecios.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvListaPrecios).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnRecargaControlPrecio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnEliminaControlPrecio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardaControlPrecio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.minimo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.maximo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbPreciosV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbUnidadM).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel20).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel19).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel18).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel17).EndInit();
		this.tab6.ResumeLayout(false);
		this.tab6.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvcategorizacion.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvcategorizacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btguardacategorizacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btneliminarcategorizacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtdescripcion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtcondicion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel22).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnactualizarcategorizacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel21).EndInit();
		this.tab7.ResumeLayout(false);
		this.tab7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txthastasituacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lblhasta).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvsituacion.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvsituacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardarsituacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btneliminarsituacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtdescripcionsituacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtdesdesituacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel23).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnactualizarsituacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel24).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
