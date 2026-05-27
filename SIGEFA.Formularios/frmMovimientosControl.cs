using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmMovimientosControl : Office2007Form
{
	private Thread p1;

	private clsAdmBanco admban = new clsAdmBanco();

	private clsAdmCtaCte admcta = new clsAdmCtaCte();

	private clsCtaCte cta = new clsCtaCte();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private DataTable dt = new DataTable();

	private clsFlujoCaja flujo = new clsFlujoCaja();

	private clsAdmFlujoCaja AdmFlu = new clsAdmFlujoCaja();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsValidar ok = new clsValidar();

	public static BindingSource data = new BindingSource();

	public int Proceso = 0;

	public int CodMovimiento = 0;

	public string tipo;

	public int Procede = 0;

	public int TipoProcedencia = 0;

	public decimal totalv = default(decimal);

	public decimal Soles;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private GroupBox groupBox3;

	private Label label11;

	private Label label10;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label12;

	private GroupBox groupBox4;

	private Button btnSalir;

	private ImageList imageList1;

	private Button btnGuardar2;

	private Button btnNuevo2;

	private Button btnReporte2;

	private Button btnEditar2;

	private Button btnEliminar2;

	private Label label13;

	public TextBox txtTipoCta;

	public ComboBox cmbtipopagoser;

	public ComboBox cmbCuenta;

	public ComboBox cmbBanco;

	public ComboBox cmbTipo;

	public TextBox txtTotalCuenta;

	public TextBox txtmonto;

	public TextBox txtCambioVen;

	public TextBox txtCambioCom;

	public TextBox txtDescripcion;

	public TextBox txtCodTransaccion;

	public ComboBox cmbMoneda;

	public DateTimePicker dtpFecha;

	private SuperValidator superValidator1;

	private CustomValidator customValidator7;

	private CustomValidator customValidator6;

	private CustomValidator customValidator4;

	private CustomValidator customValidator5;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	public Label label5;

	private DateTimePicker dtpFechaCierre;

	private Label label14;

	private GroupBox groupBox5;

	private Label label15;

	public TextBox txtDni;

	private Label label16;

	public TextBox txtDireccion;

	private Label label17;

	public TextBox txtNombre;

	private TextBox txtdoc;

	private Label label18;

	public frmMovimientosControl()
	{
		InitializeComponent();
	}

	private void carga(int tipo)
	{
		cmbtipopagoser.DataSource = AdmFlu.ListaPagoCobro(tipo);
		cmbtipopagoser.DisplayMember = "descripcion";
		cmbtipopagoser.ValueMember = "codTipoPagoCaja";
		cmbtipopagoser.SelectedIndex = -1;
	}

	private void CargaBancos()
	{
		cmbBanco.DataSource = admban.MuestraBancos();
		cmbBanco.DisplayMember = "descripcion";
		cmbBanco.ValueMember = "codBanco";
		cmbBanco.SelectedIndex = -1;
	}

	public void CargaCtaCte()
	{
		cmbCuenta.DataSource = admcta.ListaCtasBanco(Convert.ToInt32(cmbBanco.SelectedValue), frmLogin.iCodAlmacen);
		cmbCuenta.DisplayMember = "cuentaCorriente";
		cmbCuenta.ValueMember = "codCuentaCorriente";
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = -1;
	}

	private void Cargartiposcuenta()
	{
		cta = admcta.CargaTipoCuenta(Convert.ToInt32(cmbCuenta.SelectedValue.ToString()), frmLogin.iCodAlmacen);
		if (cta != null)
		{
			txtTipoCta.Text = cta.TipoCuenta;
			if (cta.Moneda > 0)
			{
				cmbMoneda.SelectedValue = cta.Moneda;
			}
			else
			{
				cmbMoneda.SelectedIndex = -1;
				txtTipoCta.Text = "";
			}
			if (Proceso == 2)
			{
				txtDescripcion.Text = cta.descripcion;
			}
			if (cta.saldo != 0m)
			{
				txtTotalCuenta.Text = cta.saldo.ToString();
			}
			else
			{
				txtTotalCuenta.Text = 0.ToString();
			}
		}
	}

	private void CargaTipoCambio()
	{
		tc = AdmTc.CargaTipoCambio(DateTime.Today, 2);
		if (tc != null)
		{
			txtCambioCom.Text = Convert.ToString(tc.Compra);
			txtCambioVen.Text = Convert.ToString(tc.Venta);
		}
	}

	public void frmMovimientosControl_Load(object sender, EventArgs e)
	{
		CargaMoneda();
		CargaBancos();
		CargaTipoCambio();
		cmbBanco.SelectedIndex = -1;
		cmbTipo.SelectedIndex = -1;
		dtpFechaCierre.MaxDate = DateTime.Now;
		Control.CheckForIllegalCrossThreadCalls = false;
		if (Proceso == 1)
		{
			HabilitaControles(Estado: true);
		}
		else if (Proceso == 2)
		{
			HabilitaControles(Estado: true);
			Carga_Datos();
		}
		else if (Proceso == 3)
		{
			HabilitaControles(Estado: false);
			Carga_Datos();
			btnReporte2.Visible = true;
		}
		if (Procede == 2)
		{
			cmbTipo.SelectedIndex = 0;
			cmbTipo.Enabled = false;
			TipoProcedencia = 1;
			btnGuardar2.Enabled = false;
			cmbMoneda.Enabled = false;
		}
	}

	private void Carga_Datos()
	{
		cta = admcta.BuscaMovimiento(CodMovimiento, frmLogin.iCodAlmacen);
		if (cta != null)
		{
			cmbBanco.SelectedValue = cta.CodBanco;
			CargaCtaCte();
			cmbCuenta.SelectedValue = cta.CodCtaCte;
			txtTipoCta.Text = cta.TipoCuenta;
			cmbMoneda.SelectedValue = cta.Moneda;
			txtDescripcion.Text = cta.descripcion;
			if (tipo == "INGRESO")
			{
				cta.Tipo = 1;
			}
			else
			{
				cta.Tipo = 0;
			}
			if (cta.Tipo == 1)
			{
				cmbTipo.Text = "INGRESO";
				cmbTipo.SelectedIndex = 0;
				cmbTipo_SelectionChangeCommitted(new object(), new EventArgs());
				txtmonto.Text = cta.ingreso.ToString();
			}
			else
			{
				cmbTipo.Text = "EGRESO";
				cmbTipo.SelectedIndex = 1;
				cmbTipo_SelectionChangeCommitted(new object(), new EventArgs());
				txtmonto.Text = cta.egreso.ToString();
			}
			cmbtipopagoser.SelectedValue = cta.CodTipoPagoServicio;
			txtCambioCom.Text = cta.tipocambio.ToString();
			txtCambioVen.Text = cta.TipoCVenta.ToString();
			txtCodTransaccion.Text = cta.NumTransaccion;
			txtTotalCuenta.Text = cta.saldo.ToString();
			txtNombre.Text = cta.Nombre;
			txtDireccion.Text = cta.Direccion;
			txtDni.Text = cta.Dni;
			txtdoc.Text = cta.Correlativo.ToString().PadLeft(9, '0');
		}
	}

	private void Cuentas()
	{
		CargaCtaCte();
		cmbCuenta.Enabled = true;
		if (cmbCuenta.Items.Count > 0 && cmbCuenta.Text != "")
		{
			Cargartiposcuenta();
			return;
		}
		txtTipoCta.Text = "";
		cmbMoneda.SelectedIndex = -1;
		cmbCuenta.Enabled = false;
	}

	public void cmbBanco_SelectionChangeCommitted(object sender, EventArgs e)
	{
		Cuentas();
		btnGuardar2.Enabled = true;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cmbCuenta_SelectionChangeCommitted(object sender, EventArgs e)
	{
		Cargartiposcuenta();
	}

	private void Limpiar()
	{
		cmbBanco.SelectedIndex = -1;
		cmbCuenta.SelectedIndex = -1;
		cmbTipo.SelectedIndex = -1;
		cmbMoneda.SelectedIndex = -1;
		txtmonto.Text = "";
		txtDescripcion.Text = "";
		txtTipoCta.Text = "";
		txtTotalCuenta.Text = "";
		txtCodTransaccion.Text = "";
	}

	private void HabilitaControles(bool Estado)
	{
		txtDescripcion.Enabled = Estado;
		txtmonto.Enabled = Estado;
		dtpFecha.Enabled = Estado;
		cmbTipo.Enabled = Estado;
		btnGuardar2.Enabled = Estado;
		cmbtipopagoser.Enabled = Estado;
		cmbBanco.Enabled = Estado;
		cmbMoneda.Enabled = Estado;
		cmbCuenta.Enabled = Estado;
		txtCodTransaccion.Enabled = Estado;
		txtTotalCuenta.Enabled = Estado;
		txtNombre.Enabled = Estado;
		txtDireccion.Enabled = Estado;
		txtDni.Enabled = Estado;
		txtdoc.Enabled = Estado;
	}

	private void btnGuardar2_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtCodTransaccion.Text != "")
			{
				if ((Convert.ToDecimal(txtmonto.Text) <= Convert.ToDecimal(txtTotalCuenta.Text) && cmbTipo.Text == "EGRESO") || (Convert.ToDecimal(txtmonto.Text) >= Convert.ToDecimal(txtTotalCuenta.Text) && cmbTipo.Text == "INGRESO") || (Convert.ToDecimal(txtmonto.Text) <= Convert.ToDecimal(txtTotalCuenta.Text) && cmbTipo.Text == "INGRESO"))
				{
					cta.CodBanco = Convert.ToInt32(cmbBanco.SelectedValue);
					cta.CodCtaCte = Convert.ToInt32(cmbCuenta.SelectedValue);
					cta.NumTransaccion = txtCodTransaccion.Text.Trim();
					cta.descripcion = txtDescripcion.Text.Trim();
					cta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					cta.TipoCuenta = txtTipoCta.Text.Trim();
					if (cmbTipo.Text == "INGRESO")
					{
						cta.Tipo = 1;
					}
					else
					{
						cta.Tipo = 0;
					}
					cta.tipocambio = Convert.ToDecimal(txtCambioCom.Text.Trim());
					cta.TipoCVenta = Convert.ToDecimal(txtCambioVen.Text.Trim());
					cta.Dmonto = Convert.ToDecimal(txtmonto.Text);
					cta.Coduser = frmLogin.iCodUser;
					cta.CodTipoPagoServicio = Convert.ToInt32(cmbtipopagoser.SelectedValue);
					cta.FechaMovimiento = dtpFecha.Value;
					cta.CodAlmacen = frmLogin.iCodAlmacen;
					cta.CodSucursal = frmLogin.iCodSucursal;
					cta.TipoProcedencia = TipoProcedencia;
					cta.FechaCierreCaja = dtpFechaCierre.Value;
					cta.Nombre = txtNombre.Text;
					cta.Direccion = txtDireccion.Text;
					cta.Dni = txtDni.Text;
					cta.Correlativo = Convert.ToInt32(txtdoc.Text);
					if (cmbTipo.SelectedIndex == 0)
					{
						cta.Igresoegreso = 1;
					}
					else if (cmbTipo.SelectedIndex == 1)
					{
						cta.Igresoegreso = 0;
					}
					if (Proceso == 1)
					{
						if (admcta.InsertMovi(cta))
						{
							MessageBox.Show("Los datos se Guardaron Correctamente", "CONTROL DE MOVIMIENTOS DE BANCO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							CodMovimiento = cta.CodCtaCteNuevo;
						}
						else
						{
							MessageBox.Show("Ocurrio un Error al Guardar los Datos", "CONTROL DE MOVIMIENTOS DE BANCO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else if (Proceso == 2)
					{
						cta.CodMovi = CodMovimiento;
						if (admcta.UpdateMovi(cta))
						{
							MessageBox.Show("Los datos se Actualizaron Correctamente", "CONTROL DE MOVIMIENTOS DE BANCO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							MessageBox.Show("Ocurrio un Error al Actualizar los Datos", "CONTROL DE MOVIMIENTOS DE BANCO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
				}
				else
				{
					MessageBox.Show("EL MONTO INGRESADO EXCEDE DEL TOTAL DE LA CUENTA", "CONTROL DE MOVIMIENTOS DE BANCO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				Proceso = 0;
				Carga_Datos();
				HabilitaControles(Estado: false);
				btnReporte2.Visible = true;
			}
			else
			{
				MessageBox.Show("INGRESE CÓDIGO DE TRANSACCIÓN", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCodTransaccion.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnEditar2_Click(object sender, EventArgs e)
	{
	}

	private void btnNuevo2_Click(object sender, EventArgs e)
	{
		Limpiar();
		CargaBancos();
		cmbCuenta.Enabled = false;
	}

	private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		txtDescripcion.Enabled = true;
		txtmonto.Text = "";
		btnEliminar2.Enabled = true;
		btnEditar2.Enabled = true;
		btnGuardar2.Enabled = false;
	}

	private void btnEliminar2_Click(object sender, EventArgs e)
	{
	}

	private void txtCodTransaccion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void cmbTipo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cmbTipo.Text == "INGRESO")
		{
			cmbtipopagoser.Enabled = true;
			carga(1);
		}
		else if (cmbTipo.Text == "EGRESO")
		{
			cmbtipopagoser.Enabled = true;
			carga(0);
		}
	}

	private void btnReporte2_Click(object sender, EventArgs e)
	{
		try
		{
			int tip = Convert.ToInt32(cmbtipopagoser.SelectedValue);
			switch (tip)
			{
			case 1:
			{
				clsReporteCaja dso7 = new clsReporteCaja();
				CRRecibodeEgresos rpt7 = new CRRecibodeEgresos();
				frmReporteReciboCajaChicaRPT frm7 = new frmReporteReciboCajaChicaRPT();
				rpt7.SetDataSource(dso7.ReciboDietasyEstimulo(tip, CodMovimiento));
				frm7.crvReciboCajaChica.ReportSource = rpt7;
				frm7.Show();
				break;
			}
			case 2:
			{
				clsReporteCaja dso6 = new clsReporteCaja();
				CRReciboEgresosMovilidadAgazajosFestividades rpt6 = new CRReciboEgresosMovilidadAgazajosFestividades();
				frmReporteReciboCajaChicaRPT frm6 = new frmReporteReciboCajaChicaRPT();
				rpt6.SetDataSource(dso6.ReciboDietasyEstimulo(tip, CodMovimiento));
				frm6.crvReciboCajaChica.ReportSource = rpt6;
				frm6.Show();
				break;
			}
			case 3:
			{
				clsReporteCaja dso5 = new clsReporteCaja();
				CRReciboEgresosMovilidsd rpt5 = new CRReciboEgresosMovilidsd();
				frmReporteReciboCajaChicaRPT frm5 = new frmReporteReciboCajaChicaRPT();
				rpt5.SetDataSource(dso5.ReciboDietasyEstimulo(tip, CodMovimiento));
				frm5.crvReciboCajaChica.ReportSource = rpt5;
				frm5.Show();
				break;
			}
			case 4:
			{
				clsReporteCaja dso4 = new clsReporteCaja();
				CRReciboEgresosPorTerceros rpt4 = new CRReciboEgresosPorTerceros();
				frmReporteReciboCajaChicaRPT frm4 = new frmReporteReciboCajaChicaRPT();
				rpt4.SetDataSource(dso4.ReciboDietasyEstimulo(tip, CodMovimiento));
				frm4.crvReciboCajaChica.ReportSource = rpt4;
				frm4.Show();
				break;
			}
			case 5:
			{
				clsReporteCaja dso3 = new clsReporteCaja();
				CRReciboEgresoDietasyEstimulos rpt3 = new CRReciboEgresoDietasyEstimulos();
				frmReporteReciboCajaChicaRPT frm3 = new frmReporteReciboCajaChicaRPT();
				rpt3.SetDataSource(dso3.ReciboDietasyEstimulo(tip, CodMovimiento));
				frm3.crvReciboCajaChica.ReportSource = rpt3;
				frm3.Show();
				break;
			}
			case 11:
			{
				clsReporteCaja dso2 = new clsReporteCaja();
				CRRecibodeIngresos rpt2 = new CRRecibodeIngresos();
				frmReporteReciboCajaChicaRPT frm2 = new frmReporteReciboCajaChicaRPT();
				rpt2.SetDataSource(dso2.ReciboDietasyEstimulo(tip, CodMovimiento).Tables[0]);
				frm2.crvReciboCajaChica.ReportSource = rpt2;
				frm2.Show();
				break;
			}
			case 12:
			{
				clsReporteCaja dso = new clsReporteCaja();
				CRRecibodeIngresos rpt = new CRRecibodeIngresos();
				frmReporteReciboCajaChicaRPT frm = new frmReporteReciboCajaChicaRPT();
				rpt.SetDataSource(dso.ReciboDietasyEstimulo(tip, CodMovimiento).Tables[0]);
				frm.crvReciboCajaChica.ReportSource = rpt;
				frm.Show();
				break;
			}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se presento el siguiente error" + ex.ToString(), "Cierre", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			if (cmbMoneda.Items.Count > 0)
			{
				if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
				{
					txtmonto.Text = Convert.ToString(Soles);
				}
				else
				{
					txtmonto.Text = Convert.ToString(Soles / Convert.ToDecimal(txtCambioCom.Text));
				}
			}
			CargaTipoCambio();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void dtpFechaCierre_ValueChanged(object sender, EventArgs e)
	{
	}

	private void txtmonto_TextChanged(object sender, EventArgs e)
	{
		if (Procede == 2 && txtmonto.Text != "")
		{
			if (Convert.ToDecimal(txtmonto.Text) > 0m)
			{
				btnGuardar2.Enabled = true;
			}
			else
			{
				btnGuardar2.Enabled = false;
			}
		}
	}

	private void cmbMoneda_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void cmbtipopagoser_SelectionChangeCommitted(object sender, EventArgs e)
	{
		int numeracion = admcta.Correlativo(Convert.ToInt32(cmbtipopagoser.SelectedValue));
		txtdoc.Text = numeracion.ToString().PadLeft(9, '0');
	}

	private void txtTotalCuenta_TextChanged(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmMovimientosControl));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtdoc = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.txtDni = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dtpFechaCierre = new System.Windows.Forms.DateTimePicker();
		this.label14 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label13 = new System.Windows.Forms.Label();
		this.txtCodTransaccion = new System.Windows.Forms.TextBox();
		this.txtTotalCuenta = new System.Windows.Forms.TextBox();
		this.txtmonto = new System.Windows.Forms.TextBox();
		this.txtCambioVen = new System.Windows.Forms.TextBox();
		this.txtCambioCom = new System.Windows.Forms.TextBox();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.cmbTipo = new System.Windows.Forms.ComboBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.txtTipoCta = new System.Windows.Forms.TextBox();
		this.cmbtipopagoser = new System.Windows.Forms.ComboBox();
		this.cmbCuenta = new System.Windows.Forms.ComboBox();
		this.cmbBanco = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnGuardar2 = new System.Windows.Forms.Button();
		this.btnNuevo2 = new System.Windows.Forms.Button();
		this.btnReporte2 = new System.Windows.Forms.Button();
		this.btnEditar2 = new System.Windows.Forms.Button();
		this.btnEliminar2 = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.groupBox1.Controls.Add(this.groupBox5);
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(2, 1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(789, 319);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox5.Controls.Add(this.txtdoc);
		this.groupBox5.Controls.Add(this.label18);
		this.groupBox5.Controls.Add(this.label15);
		this.groupBox5.Controls.Add(this.txtDni);
		this.groupBox5.Controls.Add(this.label16);
		this.groupBox5.Controls.Add(this.txtDireccion);
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.txtNombre);
		this.groupBox5.Location = new System.Drawing.Point(6, 225);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(774, 81);
		this.groupBox5.TabIndex = 2;
		this.groupBox5.TabStop = false;
		this.txtdoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtdoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtdoc.ForeColor = System.Drawing.Color.Red;
		this.txtdoc.Location = new System.Drawing.Point(595, 43);
		this.txtdoc.MaxLength = 50;
		this.txtdoc.Name = "txtdoc";
		this.txtdoc.Size = new System.Drawing.Size(134, 20);
		this.txtdoc.TabIndex = 34;
		this.txtdoc.Text = ".";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(592, 19);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(83, 13);
		this.label18.TabIndex = 33;
		this.label18.Text = "N° Documento :";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(409, 48);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(26, 13);
		this.label15.TabIndex = 32;
		this.label15.Text = "Dni:";
		this.txtDni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtDni.Location = new System.Drawing.Point(444, 45);
		this.txtDni.MaxLength = 8;
		this.txtDni.Name = "txtDni";
		this.txtDni.Size = new System.Drawing.Size(118, 20);
		this.txtDni.TabIndex = 31;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(26, 50);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(55, 13);
		this.label16.TabIndex = 30;
		this.label16.Text = "Direccion:";
		this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(92, 45);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(287, 20);
		this.txtDireccion.TabIndex = 29;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(26, 23);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(47, 13);
		this.label17.TabIndex = 28;
		this.label17.Text = "Nombre:";
		this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Location = new System.Drawing.Point(92, 19);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(470, 20);
		this.txtNombre.TabIndex = 27;
		this.groupBox3.Controls.Add(this.dtpFechaCierre);
		this.groupBox3.Controls.Add(this.label14);
		this.groupBox3.Controls.Add(this.dtpFecha);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Controls.Add(this.txtCodTransaccion);
		this.groupBox3.Controls.Add(this.txtTotalCuenta);
		this.groupBox3.Controls.Add(this.txtmonto);
		this.groupBox3.Controls.Add(this.txtCambioVen);
		this.groupBox3.Controls.Add(this.txtCambioCom);
		this.groupBox3.Controls.Add(this.txtDescripcion);
		this.groupBox3.Controls.Add(this.cmbTipo);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.label8);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Location = new System.Drawing.Point(349, 11);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(431, 210);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "DATOS DE LOS MOVIMIENTOS";
		this.dtpFechaCierre.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaCierre.Location = new System.Drawing.Point(116, 144);
		this.dtpFechaCierre.Name = "dtpFechaCierre";
		this.dtpFechaCierre.Size = new System.Drawing.Size(117, 20);
		this.dtpFechaCierre.TabIndex = 5;
		this.dtpFechaCierre.ValueChanged += new System.EventHandler(dtpFechaCierre_ValueChanged);
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(10, 149);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(109, 13);
		this.label14.TabIndex = 16;
		this.label14.Text = "Fecha de Cierre Caja:";
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(116, 115);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(117, 20);
		this.dtpFecha.TabIndex = 3;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(15, 119);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(100, 13);
		this.label13.TabIndex = 14;
		this.label13.Text = "Fecha de Deposito:";
		this.txtCodTransaccion.Location = new System.Drawing.Point(116, 179);
		this.txtCodTransaccion.Name = "txtCodTransaccion";
		this.txtCodTransaccion.Size = new System.Drawing.Size(117, 20);
		this.txtCodTransaccion.TabIndex = 7;
		this.superValidator1.SetValidator1(this.txtCodTransaccion, this.customValidator7);
		this.txtCodTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodTransaccion_KeyPress);
		this.txtTotalCuenta.Location = new System.Drawing.Point(324, 179);
		this.txtTotalCuenta.Name = "txtTotalCuenta";
		this.txtTotalCuenta.ReadOnly = true;
		this.txtTotalCuenta.Size = new System.Drawing.Size(100, 20);
		this.txtTotalCuenta.TabIndex = 8;
		this.txtTotalCuenta.TextChanged += new System.EventHandler(txtTotalCuenta_TextChanged);
		this.txtmonto.Location = new System.Drawing.Point(325, 146);
		this.txtmonto.MaxLength = 15;
		this.txtmonto.Name = "txtmonto";
		this.txtmonto.Size = new System.Drawing.Size(100, 20);
		this.txtmonto.TabIndex = 6;
		this.superValidator1.SetValidator1(this.txtmonto, this.customValidator6);
		this.txtmonto.TextChanged += new System.EventHandler(txtmonto_TextChanged);
		this.txtmonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtmonto_KeyPress);
		this.txtCambioVen.Location = new System.Drawing.Point(324, 116);
		this.txtCambioVen.Name = "txtCambioVen";
		this.txtCambioVen.Size = new System.Drawing.Size(100, 20);
		this.txtCambioVen.TabIndex = 4;
		this.txtCambioCom.Location = new System.Drawing.Point(325, 85);
		this.txtCambioCom.Name = "txtCambioCom";
		this.txtCambioCom.Size = new System.Drawing.Size(100, 20);
		this.txtCambioCom.TabIndex = 2;
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(76, 23);
		this.txtDescripcion.Multiline = true;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(348, 51);
		this.txtDescripcion.TabIndex = 0;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator4);
		this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDescripcion_KeyPress);
		this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipo.FormattingEnabled = true;
		this.cmbTipo.Items.AddRange(new object[2] { "INGRESO", "EGRESO" });
		this.cmbTipo.Location = new System.Drawing.Point(76, 85);
		this.cmbTipo.Name = "cmbTipo";
		this.cmbTipo.Size = new System.Drawing.Size(157, 21);
		this.cmbTipo.TabIndex = 1;
		this.superValidator1.SetValidator1(this.cmbTipo, this.customValidator5);
		this.cmbTipo.SelectionChangeCommitted += new System.EventHandler(cmbTipo_SelectionChangeCommitted);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(242, 182);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(77, 13);
		this.label12.TabIndex = 6;
		this.label12.Text = "Total - Cuenta:";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(21, 182);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(94, 13);
		this.label11.TabIndex = 5;
		this.label11.Text = "Nro de Operación:";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(279, 149);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(40, 13);
		this.label10.TabIndex = 4;
		this.label10.Text = "Monto:";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(239, 120);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(80, 13);
		this.label9.TabIndex = 3;
		this.label9.Text = "T. Cambio Ven:";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(239, 88);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(79, 13);
		this.label8.TabIndex = 2;
		this.label8.Text = "T.Cambio Com:";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(10, 88);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(31, 13);
		this.label7.TabIndex = 1;
		this.label7.Text = "Tipo:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 26);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(66, 13);
		this.label6.TabIndex = 0;
		this.label6.Text = "Descripción:";
		this.groupBox2.Controls.Add(this.cmbMoneda);
		this.groupBox2.Controls.Add(this.txtTipoCta);
		this.groupBox2.Controls.Add(this.cmbtipopagoser);
		this.groupBox2.Controls.Add(this.cmbCuenta);
		this.groupBox2.Controls.Add(this.cmbBanco);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Location = new System.Drawing.Point(6, 11);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(337, 210);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "DATOS FINANCIEROS";
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(110, 114);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(210, 21);
		this.cmbMoneda.TabIndex = 3;
		this.superValidator1.SetValidator1(this.cmbMoneda, this.customValidator3);
		this.cmbMoneda.SelectedIndexChanged += new System.EventHandler(cmbMoneda_SelectedIndexChanged);
		this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(cmbMoneda_SelectionChangeCommitted);
		this.txtTipoCta.Location = new System.Drawing.Point(109, 84);
		this.txtTipoCta.Name = "txtTipoCta";
		this.txtTipoCta.ReadOnly = true;
		this.txtTipoCta.Size = new System.Drawing.Size(211, 20);
		this.txtTipoCta.TabIndex = 2;
		this.cmbtipopagoser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbtipopagoser.Enabled = false;
		this.cmbtipopagoser.FormattingEnabled = true;
		this.cmbtipopagoser.Location = new System.Drawing.Point(109, 147);
		this.cmbtipopagoser.Name = "cmbtipopagoser";
		this.cmbtipopagoser.Size = new System.Drawing.Size(211, 21);
		this.cmbtipopagoser.TabIndex = 4;
		this.cmbtipopagoser.SelectionChangeCommitted += new System.EventHandler(cmbtipopagoser_SelectionChangeCommitted);
		this.cmbCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbCuenta.FormattingEnabled = true;
		this.cmbCuenta.Location = new System.Drawing.Point(110, 53);
		this.cmbCuenta.Name = "cmbCuenta";
		this.cmbCuenta.Size = new System.Drawing.Size(210, 21);
		this.cmbCuenta.TabIndex = 1;
		this.superValidator1.SetValidator1(this.cmbCuenta, this.customValidator2);
		this.cmbCuenta.SelectionChangeCommitted += new System.EventHandler(cmbCuenta_SelectionChangeCommitted);
		this.cmbBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbBanco.FormattingEnabled = true;
		this.cmbBanco.Location = new System.Drawing.Point(110, 23);
		this.cmbBanco.Name = "cmbBanco";
		this.cmbBanco.Size = new System.Drawing.Size(210, 21);
		this.cmbBanco.TabIndex = 0;
		this.superValidator1.SetValidator1(this.cmbBanco, this.customValidator1);
		this.cmbBanco.SelectionChangeCommitted += new System.EventHandler(cmbBanco_SelectionChangeCommitted);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 150);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(100, 13);
		this.label5.TabIndex = 9;
		this.label5.Text = "Tipo Pago Servicio:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(7, 117);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(49, 13);
		this.label4.TabIndex = 8;
		this.label4.Text = "Moneda:";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 88);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(50, 13);
		this.label3.TabIndex = 7;
		this.label3.Text = "Tipo Cta:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 56);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(74, 13);
		this.label2.TabIndex = 6;
		this.label2.Text = "Cta. Corriente:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 26);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 13);
		this.label1.TabIndex = 5;
		this.label1.Text = "Banco:";
		this.groupBox4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.groupBox4.Controls.Add(this.btnSalir);
		this.groupBox4.Controls.Add(this.btnGuardar2);
		this.groupBox4.Controls.Add(this.btnNuevo2);
		this.groupBox4.Controls.Add(this.btnReporte2);
		this.groupBox4.Controls.Add(this.btnEditar2);
		this.groupBox4.Controls.Add(this.btnEliminar2);
		this.groupBox4.Location = new System.Drawing.Point(2, 313);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(789, 52);
		this.groupBox4.TabIndex = 1;
		this.groupBox4.TabStop = false;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(700, 13);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(61, 32);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
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
		this.btnGuardar2.Enabled = false;
		this.btnGuardar2.ImageIndex = 4;
		this.btnGuardar2.ImageList = this.imageList1;
		this.btnGuardar2.Location = new System.Drawing.Point(595, 13);
		this.btnGuardar2.Name = "btnGuardar2";
		this.btnGuardar2.Size = new System.Drawing.Size(86, 32);
		this.btnGuardar2.TabIndex = 0;
		this.btnGuardar2.Text = "Guardar";
		this.btnGuardar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar2.UseVisualStyleBackColor = true;
		this.btnGuardar2.Click += new System.EventHandler(btnGuardar2_Click);
		this.btnNuevo2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo2.ImageIndex = 1;
		this.btnNuevo2.ImageList = this.imageList1;
		this.btnNuevo2.Location = new System.Drawing.Point(8, 13);
		this.btnNuevo2.Name = "btnNuevo2";
		this.btnNuevo2.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo2.TabIndex = 2;
		this.btnNuevo2.Text = "Nuevo";
		this.btnNuevo2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo2.UseVisualStyleBackColor = true;
		this.btnNuevo2.Visible = false;
		this.btnNuevo2.Click += new System.EventHandler(btnNuevo2_Click);
		this.btnReporte2.ImageIndex = 3;
		this.btnReporte2.ImageList = this.imageList1;
		this.btnReporte2.Location = new System.Drawing.Point(373, 13);
		this.btnReporte2.Name = "btnReporte2";
		this.btnReporte2.Size = new System.Drawing.Size(78, 32);
		this.btnReporte2.TabIndex = 59;
		this.btnReporte2.Text = "Reporte";
		this.btnReporte2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte2.UseVisualStyleBackColor = true;
		this.btnReporte2.Visible = false;
		this.btnReporte2.Click += new System.EventHandler(btnReporte2_Click);
		this.btnEditar2.Enabled = false;
		this.btnEditar2.ImageIndex = 0;
		this.btnEditar2.ImageList = this.imageList1;
		this.btnEditar2.Location = new System.Drawing.Point(85, 13);
		this.btnEditar2.Name = "btnEditar2";
		this.btnEditar2.Size = new System.Drawing.Size(66, 32);
		this.btnEditar2.TabIndex = 57;
		this.btnEditar2.Text = "Editar";
		this.btnEditar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar2.UseVisualStyleBackColor = true;
		this.btnEditar2.Visible = false;
		this.btnEditar2.Click += new System.EventHandler(btnEditar2_Click);
		this.btnEliminar2.Enabled = false;
		this.btnEliminar2.ImageIndex = 2;
		this.btnEliminar2.ImageList = this.imageList1;
		this.btnEliminar2.Location = new System.Drawing.Point(157, 13);
		this.btnEliminar2.Name = "btnEliminar2";
		this.btnEliminar2.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar2.TabIndex = 58;
		this.btnEliminar2.Text = "Eliminar";
		this.btnEliminar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar2.UseVisualStyleBackColor = true;
		this.btnEliminar2.Visible = false;
		this.btnEliminar2.Click += new System.EventHandler(btnEliminar2_Click);
		this.customValidator7.ErrorMessage = "Ingrese Nro Operación";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator6.ErrorMessage = "Ingrese Monto";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator4.ErrorMessage = "Ingrese Descripcion";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator5.ErrorMessage = "Seleccione Tipo";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator3.ErrorMessage = "Seleccione Moneda";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Seleccione Cuenta";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Seleccione Banco";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.ClientSize = new System.Drawing.Size(791, 369);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmMovimientosControl";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmMovimientosControl";
		base.Load += new System.EventHandler(frmMovimientosControl_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
