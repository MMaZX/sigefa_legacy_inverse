using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmListaCaja : Office2007Form
{
	private DataTable datosCarga = new DataTable();

	private DataTable datosAlmacena = new DataTable();

	private clsCtaCte Cta = new clsCtaCte();

	private clsAdmCtaCte AdmCta = new clsAdmCtaCte();

	private clsFlujoCaja flujo = new clsFlujoCaja();

	private clsAdmFlujoCaja admFlujo = new clsAdmFlujoCaja();

	private clsFlujoCaja flu = new clsFlujoCaja();

	private clsTipoCambio tcambio = new clsTipoCambio();

	private clsAdmTipoCambio admTipoCambio = new clsAdmTipoCambio();

	private decimal Saldo = default(decimal);

	private decimal Ingresos = default(decimal);

	public decimal Egresos = default(decimal);

	private decimal OtrosEgresos = default(decimal);

	public decimal MontoCierre = default(decimal);

	public DateTime fechaCierre;

	public decimal Soles = default(decimal);

	public decimal OtrosIngresos;

	public int Proceso = 0;

	private int rpta;

	private decimal MontoDeposito;

	private decimal TotalVentasDia = default(decimal);

	private int FilasChequeadas = 0;

	private decimal MontoRendido = default(decimal);

	private IContainer components = null;

	private RibbonBar ribbonBar1;

	private ImageList imageList1;

	private ButtonItem biEditar;

	private ButtonItem biEliminar;

	private ButtonItem biActualizar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimir;

	private Button btnSalir;

	private ComboBox cboMovimientos;

	private Label label1;

	private Panel panel1;

	private Panel panel2;

	private Button btnCancelar;

	private ImageList imageList2;

	private DateTimePicker dtpfecha1;

	private DateTimePicker dtpfecha2;

	private Label label2;

	private Label label3;

	private Panel panel3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label lblIngresos;

	private Label lblSaldoCaja;

	private Label lblAperturaCaja;

	private Panel panel4;

	private RibbonBar ribbonBar2;

	private ButtonItem biRencicionCaja;

	private Panel panel5;

	private ButtonItem biRendicionesContables;

	private ExpandablePanel expandablePanel1;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label lblColumna;

	private TextBox txtFiltro;

	private Button btnclose;

	private Label lblProperty;

	private ButtonItem biAperturaCajachica;

	private Panel panel6;

	private TextBox txtComentario;

	private Label label11;

	private DataGridView dgvMovimientosCaja;

	private Button btnExit;

	public Label lblEgresos;

	public ButtonItem biEgreso;

	private Label label12;

	public Label lblOtrosEgresos;

	private DataGridViewTextBoxColumn ndoc;

	private DataGridViewTextBoxColumn fechas;

	private DataGridViewTextBoxColumn codCli;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn doc;

	private DataGridViewTextBoxColumn mon;

	private DataGridViewTextBoxColumn formap;

	private DataGridViewTextBoxColumn sucursal;

	private DataGridViewTextBoxColumn codctactemov;

	private DataGridViewTextBoxColumn codban;

	private DataGridViewTextBoxColumn banco;

	private DataGridViewTextBoxColumn codctacte;

	private DataGridViewTextBoxColumn ctacte;

	private DataGridViewTextBoxColumn numoperacion;

	private DataGridViewTextBoxColumn fechamov;

	private DataGridViewTextBoxColumn tran;

	private DataGridViewTextBoxColumn egreso;

	private DataGridViewTextBoxColumn ingreso;

	private DataGridViewTextBoxColumn saldocta;

	private DataGridViewTextBoxColumn concepto;

	private DataGridViewTextBoxColumn tcventa;

	private DataGridViewTextBoxColumn tccompra;

	private DataGridViewTextBoxColumn documentoref;

	private DataGridViewTextBoxColumn tipoproc;

	private Label lblOtrosIngresos;

	private Label label14;

	private ButtonItem biDeposito;

	public ButtonItem biStatusCaja;

	public ButtonItem biIngreso;

	private ButtonItem biVerificarRendicion;

	private ButtonItem biRendicionLiquidada;

	private ButtonItem biHistorialRendiciones;

	private ButtonItem biBuscarRendicion;

	public frmListaCaja()
	{
		InitializeComponent();
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void biActualizar_Click(object sender, EventArgs e)
	{
		VerificaSaldoCaja();
		CalculaTotales();
		ListaIngresosCaja();
	}

	private void ListaIngresosCaja()
	{
		limpia_campos_grilla(estado: true);
		datosAlmacena.Clear();
		datosCarga = AdmCta.ListaCaja(frmLogin.iCodSucursal, dtpfecha1.Value);
		dgvMovimientosCaja.DataSource = datosCarga;
		dgvMovimientosCaja.ClearSelection();
	}

	private void ListaEgresosCaja()
	{
		limpia_campos_grilla(estado: false);
		datosCarga.Clear();
		datosAlmacena = AdmCta.ListaEgresosCaja(frmLogin.iCodSucursal, dtpfecha1.Value);
		dgvMovimientosCaja.DataSource = datosAlmacena;
		dgvMovimientosCaja.ClearSelection();
	}

	private void ListaCajaChicaFechas()
	{
	}

	public void VerificaSaldoCaja()
	{
		Saldo = default(decimal);
		flu = admFlujo.VerificaSaldoCaja(frmLogin.iCodSucursal);
		if (flu != null)
		{
			Saldo = flu.MontoDisponible;
			lblOtrosIngresos.Text = $"{flu.MontoIngresado:#, ##0.00}";
			lblEgresos.Text = $"{flu.MontoDepositado:#, ##0.00}";
			lblAperturaCaja.Text = $"{flu.MontoDisponible:#, ##0.00}";
			lblSaldoCaja.Text = $"{flu.MontoDisponible:#, ##0.00}";
		}
		else
		{
			Saldo = default(decimal);
			lblIngresos.Text = "0.000";
			lblEgresos.Text = "0.000";
			lblAperturaCaja.Text = "0.000";
			lblSaldoCaja.Text = "0.000";
			biDeposito.Enabled = false;
		}
		if (Saldo > 0m)
		{
			biDeposito.Enabled = true;
		}
		else
		{
			biDeposito.Enabled = false;
		}
	}

	public void CalculaTotales()
	{
		Ingresos = default(decimal);
		if (dgvMovimientosCaja.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvMovimientosCaja.Rows)
		{
			if (Convert.ToString(row.Cells[mon.Name].Value) == "DOLARES AMERICANOS")
			{
				Soles += Convert.ToDecimal(row.Cells[total.Name].Value) * Convert.ToDecimal(row.Cells[tcventa.Name].Value);
				Ingresos += Soles;
			}
			else
			{
				Ingresos += Convert.ToDecimal(row.Cells[total.Name].Value);
			}
		}
		TotalVentasDia = Ingresos;
		Soles = default(decimal);
		Cta = AdmCta.VerificaEgresoCaja(frmLogin.iCodSucursal, dtpfecha1.Value);
		if (Cta != null)
		{
			MontoDeposito = Cta.ingreso;
			Ingresos += flu.MontoIngresado;
		}
		else
		{
			MontoDeposito = Convert.ToDecimal(0.0);
		}
		lblEgresos.Text = $"{MontoDeposito:#,##0.000}";
		lblIngresos.Text = $"{Ingresos:#,##0.000}";
		Saldo = Saldo + Ingresos - Egresos;
		lblSaldoCaja.Text = $"{Saldo:#,##0.000}";
	}

	private void CalculaOtrosEgresos()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvMovimientosCaja.Rows)
		{
			if (Convert.ToInt32(row.Cells[tipoproc.Name].Value) == 2)
			{
				OtrosEgresos += Convert.ToDecimal(row.Cells[ingreso.Name].Value);
			}
		}
		lblOtrosEgresos.Text = $"{OtrosEgresos:#,##0.000}";
	}

	public void agrega()
	{
		lblSaldoCaja.Text = lblSaldoCaja.Text.Replace(" ", "").Trim();
		lblSaldoCaja.Text = lblSaldoCaja.Text.Replace(",", "").Trim();
		if (Convert.ToDecimal(lblSaldoCaja.Text.Trim()) > 0m)
		{
			flujo.FechaApertura = dtpfecha1.Value;
			flujo.CodSucursal = frmLogin.iCodSucursal;
			flujo.CodUser = frmLogin.iCodUser;
			admFlujo.Insert(flujo);
			VerificaSaldoCaja();
			CalculaTotales();
		}
	}

	private void frmListaCaja_Load(object sender, EventArgs e)
	{
		dtpfecha1.MinDate = Convert.ToDateTime(DateTime.Now.ToString());
		dtpfecha1.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
		dtpfecha2.MinDate = Convert.ToDateTime(DateTime.Now.ToString());
		dtpfecha2.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
		activa_elementos(activo: true);
		cboMovimientos.SelectedIndex = 0;
		VerificaSaldoCaja();
		agrega();
		ListaIngresosCaja();
		CalculaTotales();
		rpta = admFlujo.VerificaAperturaCaja(frmLogin.iCodSucursal);
		if (rpta > 0)
		{
			biStatusCaja.Visible = true;
		}
	}

	private void activa_elementos(bool activo)
	{
		label14.Visible = activo;
		label12.Visible = activo;
		lblOtrosIngresos.Visible = activo;
		lblOtrosEgresos.Visible = activo;
	}

	private void biIngreso_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmFlujoCajaRegistro"] != null)
		{
			Application.OpenForms["frmFlujoCajaRegistro"].Activate();
			return;
		}
		frmFlujoCajaRegistro form = new frmFlujoCajaRegistro();
		form.Proceso = 1;
		form.Procede = 2;
		form.Text = "APERTURA DE CAJA";
		form.Size = new Size(511, 281);
		form.ShowDialog();
		VerificaSaldoCaja();
		ListaIngresosCaja();
		CalculaTotales();
	}

	private void biHistorialRendiciones_Click(object sender, EventArgs e)
	{
	}

	private void biEgreso_Click(object sender, EventArgs e)
	{
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		cboMovimientos.SelectedIndex = 0;
		ListaIngresosCaja();
	}

	private void dtpfecha1_Leave(object sender, EventArgs e)
	{
	}

	private void dtpfecha2_Leave(object sender, EventArgs e)
	{
	}

	private void dtpfecha1_ValueChanged(object sender, EventArgs e)
	{
	}

	private void dtpfecha2_ValueChanged(object sender, EventArgs e)
	{
	}

	private void biEditar_Click(object sender, EventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
	}

	private void CalculoSaldo()
	{
		try
		{
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dgvMovimientosCajaChica_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
	}

	private void biRendicionesContables_Click(object sender, EventArgs e)
	{
	}

	private void biRencicionCaja_Click(object sender, EventArgs e)
	{
	}

	private void biVerificarRendicion_Click(object sender, EventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
	}

	private void dgvMovimientosCajaChica_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == -1)
		{
		}
	}

	private void biBuscar_Click(object sender, EventArgs e)
	{
		if (cboMovimientos.SelectedIndex == 0)
		{
			lblColumna.Text = "Num.Doc";
			lblProperty.Text = "ndoc";
		}
		else
		{
			lblColumna.Text = "Banco";
			lblProperty.Text = "codban";
		}
		if (!expandablePanel1.Expanded)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
		else
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void btnclose_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
	}

	private void dgvMovimientosCajaChica_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvMovimientosCaja.Columns[e.ColumnIndex].Index > 0)
		{
			lblColumna.Text = dgvMovimientosCaja.Columns[e.ColumnIndex].HeaderText;
			lblProperty.Text = dgvMovimientosCaja.Columns[e.ColumnIndex].DataPropertyName;
			if (expandablePanel1.Expanded)
			{
				txtFiltro.Focus();
			}
		}
	}

	private void frmListaCaja_Shown(object sender, EventArgs e)
	{
		lblAperturaCaja.Text = lblAperturaCaja.Text.Replace(" ", "").Trim();
		lblAperturaCaja.Text = lblAperturaCaja.Text.Replace(",", "").Trim();
		if (Saldo != 0m && Convert.ToDecimal(lblAperturaCaja.Text) != 0m)
		{
			biEgreso.Visible = false;
			biDeposito.Enabled = true;
			biStatusCaja.Visible = true;
		}
		else
		{
			lblAperturaCaja.Text = "0.000";
			biIngreso.Visible = true;
			biIngreso.Enabled = true;
			biDeposito.Enabled = false;
			if (Saldo == 0m || Convert.ToDecimal(lblAperturaCaja.Text) == 0m)
			{
				if (Application.OpenForms["frmFlujoCajaRegistro"] != null)
				{
					Application.OpenForms["frmFlujoCajaRegistro"].Activate();
				}
				else
				{
					frmFlujoCajaRegistro form = new frmFlujoCajaRegistro();
					form.Proceso = 1;
					form.Procede = 2;
					form.Text = "APERTURA DE CAJA";
					form.Size = new Size(511, 281);
					form.ShowDialog();
				}
			}
		}
		lblSaldoCaja.Text = lblSaldoCaja.Text.Replace(" ", "").Trim();
		lblSaldoCaja.Text = lblSaldoCaja.Text.Replace(",", "").Trim();
		if (Convert.ToDecimal(lblSaldoCaja.Text) > 0m || Convert.ToDecimal(lblAperturaCaja.Text) >= 0m)
		{
			biStatusCaja.Visible = true;
		}
	}

	private void biAperturaCajachica_Click(object sender, EventArgs e)
	{
		if (Saldo == 0m)
		{
			if (Application.OpenForms["frmCajaChicaRegistro"] != null)
			{
				Application.OpenForms["frmCajaChicaRegistro"].Activate();
				return;
			}
			frmCajaChicaRegistro form = new frmCajaChicaRegistro();
			form.Tipo = 1;
			form.Proceso = 1;
			form.AperturaCaja = 1;
			form.ShowDialog();
			ListaIngresosCaja();
			VerificaSaldoCaja();
		}
	}

	private void limpia_campos_grilla(bool estado)
	{
		dgvMovimientosCaja.Columns["ndoc"].Visible = estado;
		dgvMovimientosCaja.Columns["fechas"].Visible = estado;
		dgvMovimientosCaja.Columns["codCli"].Visible = estado;
		dgvMovimientosCaja.Columns["cliente"].Visible = estado;
		dgvMovimientosCaja.Columns["total"].Visible = estado;
		dgvMovimientosCaja.Columns["mon"].Visible = estado;
		dgvMovimientosCaja.Columns["tcventa"].Visible = estado;
		dgvMovimientosCaja.Columns["tcventa"].HeaderText = "TipoCambio";
		dgvMovimientosCaja.Columns["banco"].Visible = !estado;
		dgvMovimientosCaja.Columns["ctacte"].Visible = !estado;
		dgvMovimientosCaja.Columns["numoperacion"].Visible = !estado;
		dgvMovimientosCaja.Columns["fechamov"].Visible = !estado;
		dgvMovimientosCaja.Columns["ingreso"].Visible = !estado;
		dgvMovimientosCaja.Columns["concepto"].Visible = !estado;
		dgvMovimientosCaja.Columns["documentoref"].Visible = !estado;
	}

	private void biRendicionLiquidada_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRendicionesVigentes"] != null)
		{
			Application.OpenForms["frmRendicionesVigentes"].Activate();
			return;
		}
		frmRendicionesVigentes frm = new frmRendicionesVigentes();
		frm.ShowDialog();
		VerificaSaldoCaja();
		ListaIngresosCaja();
	}

	private void cboMovimientos_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cboMovimientos.SelectedIndex == 0)
		{
			datosAlmacena.Clear();
			ListaIngresosCaja();
			label12.Visible = false;
			lblOtrosEgresos.Visible = false;
		}
		else if (cboMovimientos.SelectedIndex == 1)
		{
			datosCarga.Clear();
			ListaEgresosCaja();
			CalculaOtrosEgresos();
		}
	}

	private void biStatusCaja_Click(object sender, EventArgs e)
	{
		frmCaja frm = new frmCaja();
		frm.ShowDialog();
	}

	private void biDeposito_Click(object sender, EventArgs e)
	{
		frmMovimientosControl frm = new frmMovimientosControl();
		frm.Proceso = 1;
		frm.Procede = 2;
		frm.Soles = TotalVentasDia;
		frm.Text = "Movimientos Bancarios";
		frm.totalv = TotalVentasDia;
		frm.label5.Visible = false;
		frm.cmbtipopagoser.Visible = false;
		frm.ShowDialog();
		VerificaSaldoCaja();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaCaja));
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.biIngreso = new DevComponents.DotNetBar.ButtonItem();
		this.biEgreso = new DevComponents.DotNetBar.ButtonItem();
		this.biDeposito = new DevComponents.DotNetBar.ButtonItem();
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.biRendicionesContables = new DevComponents.DotNetBar.ButtonItem();
		this.biStatusCaja = new DevComponents.DotNetBar.ButtonItem();
		this.btnSalir = new System.Windows.Forms.Button();
		this.cboMovimientos = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btnExit = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.label3 = new System.Windows.Forms.Label();
		this.dtpfecha1 = new System.Windows.Forms.DateTimePicker();
		this.dtpfecha2 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lblOtrosIngresos = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.lblOtrosEgresos = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.lblSaldoCaja = new System.Windows.Forms.Label();
		this.lblAperturaCaja = new System.Windows.Forms.Label();
		this.lblEgresos = new System.Windows.Forms.Label();
		this.lblIngresos = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.biAperturaCajachica = new DevComponents.DotNetBar.ButtonItem();
		this.biRencicionCaja = new DevComponents.DotNetBar.ButtonItem();
		this.panel5 = new System.Windows.Forms.Panel();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.lblProperty = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.lblColumna = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnclose = new System.Windows.Forms.Button();
		this.panel6 = new System.Windows.Forms.Panel();
		this.label11 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.dgvMovimientosCaja = new System.Windows.Forms.DataGridView();
		this.ndoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codCli = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.mon = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formap = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codctactemov = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codban = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codctacte = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ctacte = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numoperacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechamov = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tran = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.egreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.saldocta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tcventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tccompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documentoref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoproc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.biVerificarRendicion = new DevComponents.DotNetBar.ButtonItem();
		this.biRendicionLiquidada = new DevComponents.DotNetBar.ButtonItem();
		this.biHistorialRendiciones = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscarRendicion = new DevComponents.DotNetBar.ButtonItem();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel4.SuspendLayout();
		this.expandablePanel1.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvMovimientosCaja).BeginInit();
		base.SuspendLayout();
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[10] { this.biIngreso, this.biEgreso, this.biDeposito, this.biEditar, this.biEliminar, this.biActualizar, this.biBuscar, this.biImprimir, this.biRendicionesContables, this.biStatusCaja });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(591, 65);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 8;
		this.ribbonBar1.Text = "ribbonBar1";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList1.Images.SetKeyName(19, "egreso.png");
		this.imageList1.Images.SetKeyName(20, "ingreso.png");
		this.imageList1.Images.SetKeyName(21, "icon_shelfs.png");
		this.imageList1.Images.SetKeyName(22, "statuscaja.png");
		this.imageList1.Images.SetKeyName(23, "deposito2.png");
		this.biIngreso.Enabled = false;
		this.biIngreso.ImageIndex = 20;
		this.biIngreso.ImagePaddingHorizontal = 20;
		this.biIngreso.ImagePaddingVertical = 15;
		this.biIngreso.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biIngreso.Name = "biIngreso";
		this.biIngreso.SubItemsExpandWidth = 14;
		this.biIngreso.Text = "Apertura";
		this.biIngreso.Click += new System.EventHandler(biIngreso_Click);
		this.biEgreso.Enabled = false;
		this.biEgreso.ImageIndex = 19;
		this.biEgreso.ImagePaddingHorizontal = 20;
		this.biEgreso.ImagePaddingVertical = 15;
		this.biEgreso.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEgreso.Name = "biEgreso";
		this.biEgreso.SubItemsExpandWidth = 14;
		this.biEgreso.Text = "Egreso";
		this.biEgreso.Visible = false;
		this.biEgreso.Click += new System.EventHandler(biEgreso_Click);
		this.biDeposito.Enabled = false;
		this.biDeposito.ImageIndex = 23;
		this.biDeposito.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biDeposito.Name = "biDeposito";
		this.biDeposito.SubItemsExpandWidth = 14;
		this.biDeposito.Text = "Depositos";
		this.biDeposito.Click += new System.EventHandler(biDeposito_Click);
		this.biEditar.ImageIndex = 3;
		this.biEditar.ImagePaddingHorizontal = 10;
		this.biEditar.ImagePaddingVertical = 15;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Editar";
		this.biEditar.Visible = false;
		this.biEditar.Click += new System.EventHandler(biEditar_Click);
		this.biEliminar.ImageIndex = 21;
		this.biEliminar.ImagePaddingHorizontal = 10;
		this.biEliminar.ImagePaddingVertical = 15;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Visible = false;
		this.biEliminar.Click += new System.EventHandler(biEliminar_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingHorizontal = 10;
		this.biActualizar.ImagePaddingVertical = 15;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(biActualizar_Click);
		this.biBuscar.ImageIndex = 11;
		this.biBuscar.ImagePaddingHorizontal = 10;
		this.biBuscar.ImagePaddingVertical = 15;
		this.biBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscar.Name = "biBuscar";
		this.biBuscar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlB);
		this.biBuscar.SubItemsExpandWidth = 14;
		this.biBuscar.Text = "Buscar";
		this.biBuscar.Visible = false;
		this.biBuscar.Click += new System.EventHandler(biBuscar_Click);
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingHorizontal = 10;
		this.biImprimir.ImagePaddingVertical = 15;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Visible = false;
		this.biRendicionesContables.Image = (System.Drawing.Image)resources.GetObject("biRendicionesContables.Image");
		this.biRendicionesContables.ImageIndex = 7;
		this.biRendicionesContables.ImagePaddingHorizontal = 10;
		this.biRendicionesContables.ImagePaddingVertical = 15;
		this.biRendicionesContables.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRendicionesContables.Name = "biRendicionesContables";
		this.biRendicionesContables.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biRendicionesContables.SubItemsExpandWidth = 14;
		this.biRendicionesContables.Text = "Rendiociones";
		this.biRendicionesContables.Visible = false;
		this.biRendicionesContables.Click += new System.EventHandler(biRendicionesContables_Click);
		this.biStatusCaja.ImageIndex = 22;
		this.biStatusCaja.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biStatusCaja.Name = "biStatusCaja";
		this.biStatusCaja.SubItemsExpandWidth = 14;
		this.biStatusCaja.Text = "StatusCaja";
		this.biStatusCaja.Click += new System.EventHandler(biStatusCaja_Click);
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Location = new System.Drawing.Point(1013, 90);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 23);
		this.btnSalir.TabIndex = 10;
		this.btnSalir.Text = "Salir";
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.cboMovimientos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.cboMovimientos.FormattingEnabled = true;
		this.cboMovimientos.Items.AddRange(new object[2] { "INGRESO", "EGRESO" });
		this.cboMovimientos.Location = new System.Drawing.Point(147, 7);
		this.cboMovimientos.Name = "cboMovimientos";
		this.cboMovimientos.Size = new System.Drawing.Size(216, 21);
		this.cboMovimientos.TabIndex = 11;
		this.cboMovimientos.SelectionChangeCommitted += new System.EventHandler(cboMovimientos_SelectionChangeCommitted);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(16, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(125, 13);
		this.label1.TabIndex = 11;
		this.label1.Text = "TIPO DE MOVIMIENTO:";
		this.panel1.Controls.Add(this.ribbonBar1);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1241, 65);
		this.panel1.TabIndex = 12;
		this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel2.Controls.Add(this.btnExit);
		this.panel2.Controls.Add(this.label3);
		this.panel2.Controls.Add(this.dtpfecha1);
		this.panel2.Controls.Add(this.dtpfecha2);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.cboMovimientos);
		this.panel2.Controls.Add(this.btnCancelar);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(591, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(650, 65);
		this.panel2.TabIndex = 9;
		this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExit.ImageIndex = 0;
		this.btnExit.ImageList = this.imageList2;
		this.btnExit.Location = new System.Drawing.Point(573, 7);
		this.btnExit.Name = "btnExit";
		this.btnExit.Size = new System.Drawing.Size(70, 50);
		this.btnExit.TabIndex = 35;
		this.btnExit.Text = "Salir";
		this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExit.UseVisualStyleBackColor = true;
		this.btnExit.Click += new System.EventHandler(btnExit_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(247, 41);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(14, 13);
		this.label3.TabIndex = 37;
		this.label3.Text = "Y";
		this.dtpfecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha1.Location = new System.Drawing.Point(147, 37);
		this.dtpfecha1.Name = "dtpfecha1";
		this.dtpfecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpfecha1.TabIndex = 13;
		this.dtpfecha1.ValueChanged += new System.EventHandler(dtpfecha1_ValueChanged);
		this.dtpfecha1.Leave += new System.EventHandler(dtpfecha1_Leave);
		this.dtpfecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha2.Location = new System.Drawing.Point(264, 37);
		this.dtpfecha2.Name = "dtpfecha2";
		this.dtpfecha2.Size = new System.Drawing.Size(99, 20);
		this.dtpfecha2.TabIndex = 14;
		this.dtpfecha2.ValueChanged += new System.EventHandler(dtpfecha2_ValueChanged);
		this.dtpfecha2.Leave += new System.EventHandler(dtpfecha2_Leave);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(47, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(94, 13);
		this.label2.TabIndex = 36;
		this.label2.Text = "BUSCAR ENTRE:";
		this.btnCancelar.ImageIndex = 3;
		this.btnCancelar.ImageList = this.imageList2;
		this.btnCancelar.Location = new System.Drawing.Point(369, 7);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(93, 50);
		this.btnCancelar.TabIndex = 34;
		this.btnCancelar.Text = "Cancelar Filtro";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel3.Controls.Add(this.lblOtrosIngresos);
		this.panel3.Controls.Add(this.label14);
		this.panel3.Controls.Add(this.lblOtrosEgresos);
		this.panel3.Controls.Add(this.label12);
		this.panel3.Controls.Add(this.lblSaldoCaja);
		this.panel3.Controls.Add(this.lblAperturaCaja);
		this.panel3.Controls.Add(this.lblEgresos);
		this.panel3.Controls.Add(this.lblIngresos);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.label7);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.label4);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel3.Location = new System.Drawing.Point(510, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(731, 70);
		this.panel3.TabIndex = 13;
		this.lblOtrosIngresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblOtrosIngresos.BackColor = System.Drawing.Color.Transparent;
		this.lblOtrosIngresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblOtrosIngresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblOtrosIngresos.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblOtrosIngresos.Location = new System.Drawing.Point(130, 11);
		this.lblOtrosIngresos.Name = "lblOtrosIngresos";
		this.lblOtrosIngresos.Size = new System.Drawing.Size(100, 20);
		this.lblOtrosIngresos.TabIndex = 50;
		this.lblOtrosIngresos.Text = "0.000";
		this.lblOtrosIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblOtrosIngresos.Visible = false;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.BackColor = System.Drawing.Color.Transparent;
		this.label14.Location = new System.Drawing.Point(15, 15);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(113, 13);
		this.label14.TabIndex = 49;
		this.label14.Text = "POR DEPOSITAR S/:";
		this.label14.Visible = false;
		this.lblOtrosEgresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblOtrosEgresos.BackColor = System.Drawing.Color.Transparent;
		this.lblOtrosEgresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblOtrosEgresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblOtrosEgresos.ForeColor = System.Drawing.Color.DarkRed;
		this.lblOtrosEgresos.Location = new System.Drawing.Point(130, 37);
		this.lblOtrosEgresos.Name = "lblOtrosEgresos";
		this.lblOtrosEgresos.Size = new System.Drawing.Size(100, 20);
		this.lblOtrosEgresos.TabIndex = 48;
		this.lblOtrosEgresos.Text = "0.000";
		this.lblOtrosEgresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblOtrosEgresos.Visible = false;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(10, 41);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(118, 13);
		this.label12.TabIndex = 47;
		this.label12.Text = "OTROS EGRESOS S/:";
		this.label12.Visible = false;
		this.lblSaldoCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblSaldoCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblSaldoCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblSaldoCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSaldoCaja.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblSaldoCaja.Location = new System.Drawing.Point(624, 37);
		this.lblSaldoCaja.Name = "lblSaldoCaja";
		this.lblSaldoCaja.Size = new System.Drawing.Size(100, 20);
		this.lblSaldoCaja.TabIndex = 46;
		this.lblSaldoCaja.Text = "0.000";
		this.lblSaldoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblAperturaCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblAperturaCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblAperturaCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblAperturaCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAperturaCaja.ForeColor = System.Drawing.SystemColors.WindowText;
		this.lblAperturaCaja.Location = new System.Drawing.Point(624, 11);
		this.lblAperturaCaja.Name = "lblAperturaCaja";
		this.lblAperturaCaja.Size = new System.Drawing.Size(100, 20);
		this.lblAperturaCaja.TabIndex = 45;
		this.lblAperturaCaja.Text = "0.000";
		this.lblAperturaCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblEgresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblEgresos.BackColor = System.Drawing.Color.Transparent;
		this.lblEgresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblEgresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEgresos.ForeColor = System.Drawing.Color.DarkRed;
		this.lblEgresos.Location = new System.Drawing.Point(353, 37);
		this.lblEgresos.Name = "lblEgresos";
		this.lblEgresos.Size = new System.Drawing.Size(100, 20);
		this.lblEgresos.TabIndex = 44;
		this.lblEgresos.Text = "0.000";
		this.lblEgresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblIngresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblIngresos.BackColor = System.Drawing.Color.Transparent;
		this.lblIngresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblIngresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblIngresos.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblIngresos.Location = new System.Drawing.Point(353, 11);
		this.lblIngresos.Name = "lblIngresos";
		this.lblIngresos.Size = new System.Drawing.Size(100, 20);
		this.lblIngresos.TabIndex = 43;
		this.lblIngresos.Text = "0.000";
		this.lblIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Location = new System.Drawing.Point(270, 41);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(77, 13);
		this.label6.TabIndex = 42;
		this.label6.Text = "EGRESOS S/:";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(266, 15);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(81, 13);
		this.label7.TabIndex = 41;
		this.label7.Text = "INGRESOS S/:";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(557, 41);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(61, 13);
		this.label5.TabIndex = 40;
		this.label5.Text = "SALDO S/:";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(505, 15);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(113, 13);
		this.label4.TabIndex = 37;
		this.label4.Text = "APERTURA CAJA S/:";
		this.panel4.Controls.Add(this.ribbonBar2);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.panel3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 351);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(1241, 70);
		this.panel4.TabIndex = 14;
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biAperturaCajachica, this.biRencicionCaja });
		this.ribbonBar2.Location = new System.Drawing.Point(97, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(413, 70);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 14;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.ribbonBar2.Visible = false;
		this.biAperturaCajachica.Enabled = false;
		this.biAperturaCajachica.Image = (System.Drawing.Image)resources.GetObject("biAperturaCajachica.Image");
		this.biAperturaCajachica.ImagePaddingHorizontal = 20;
		this.biAperturaCajachica.ImagePaddingVertical = 10;
		this.biAperturaCajachica.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAperturaCajachica.Name = "biAperturaCajachica";
		this.biAperturaCajachica.SubItemsExpandWidth = 14;
		this.biAperturaCajachica.Text = "Aperturar   Caja Chica";
		this.biAperturaCajachica.Visible = false;
		this.biAperturaCajachica.Click += new System.EventHandler(biAperturaCajachica_Click);
		this.biRencicionCaja.Enabled = false;
		this.biRencicionCaja.Image = (System.Drawing.Image)resources.GetObject("biRencicionCaja.Image");
		this.biRencicionCaja.ImagePaddingHorizontal = 10;
		this.biRencicionCaja.ImagePaddingVertical = 10;
		this.biRencicionCaja.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRencicionCaja.Name = "biRencicionCaja";
		this.biRencicionCaja.SubItemsExpandWidth = 14;
		this.biRencicionCaja.Text = "Rendir   Caja Chica";
		this.biRencicionCaja.Click += new System.EventHandler(biRencicionCaja_Click);
		this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(97, 70);
		this.panel5.TabIndex = 15;
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.lblProperty);
		this.expandablePanel1.Controls.Add(this.label8);
		this.expandablePanel1.Controls.Add(this.label9);
		this.expandablePanel1.Controls.Add(this.label10);
		this.expandablePanel1.Controls.Add(this.lblColumna);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.btnclose);
		this.expandablePanel1.ExpandButtonVisible = false;
		this.expandablePanel1.Expanded = false;
		this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(1004, 0, 231, 93);
		this.expandablePanel1.Location = new System.Drawing.Point(1004, 0);
		this.expandablePanel1.Name = "expandablePanel1";
		this.expandablePanel1.ShowFocusRectangle = true;
		this.expandablePanel1.Size = new System.Drawing.Size(231, 0);
		this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.Style.BackColor1.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.BackColor2.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarPopupBorder;
		this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
		this.expandablePanel1.Style.GradientAngle = 90;
		this.expandablePanel1.TabIndex = 19;
		this.expandablePanel1.TitleHeight = 0;
		this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
		this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.expandablePanel1.TitleStyle.GradientAngle = 90;
		this.expandablePanel1.TitleText = "Title Bar";
		this.expandablePanel1.Visible = false;
		this.lblProperty.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblProperty.AutoSize = true;
		this.lblProperty.ForeColor = System.Drawing.Color.LightBlue;
		this.lblProperty.Location = new System.Drawing.Point(210, -59);
		this.lblProperty.Name = "lblProperty";
		this.lblProperty.Size = new System.Drawing.Size(12, 13);
		this.lblProperty.TabIndex = 11;
		this.lblProperty.Text = "x";
		this.lblProperty.Visible = false;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(10, -59);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(29, 13);
		this.label8.TabIndex = 10;
		this.label8.Text = "Por :";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Lucida Sans", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(5, -89);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(62, 12);
		this.label9.TabIndex = 9;
		this.label9.Text = "Busqueda";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.ForeColor = System.Drawing.Color.LightBlue;
		this.label10.Location = new System.Drawing.Point(186, -59);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(12, 13);
		this.label10.TabIndex = 7;
		this.label10.Text = "x";
		this.label10.Visible = false;
		this.lblColumna.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblColumna.AutoSize = true;
		this.lblColumna.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblColumna.Location = new System.Drawing.Point(45, -59);
		this.lblColumna.Name = "lblColumna";
		this.lblColumna.Size = new System.Drawing.Size(15, 13);
		this.lblColumna.TabIndex = 6;
		this.lblColumna.Text = "X";
		this.txtFiltro.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFiltro.Location = new System.Drawing.Point(13, -38);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 5;
		this.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnclose.BackColor = System.Drawing.Color.Transparent;
		this.btnclose.FlatAppearance.BorderSize = 0;
		this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnclose.Location = new System.Drawing.Point(213, -93);
		this.btnclose.Margin = new System.Windows.Forms.Padding(1);
		this.btnclose.Name = "btnclose";
		this.btnclose.Size = new System.Drawing.Size(18, 22);
		this.btnclose.TabIndex = 3;
		this.btnclose.Text = "x";
		this.btnclose.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnclose.UseVisualStyleBackColor = false;
		this.btnclose.Click += new System.EventHandler(btnclose_Click);
		this.panel6.Controls.Add(this.label11);
		this.panel6.Controls.Add(this.txtComentario);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 321);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(1241, 30);
		this.panel6.TabIndex = 20;
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.Location = new System.Drawing.Point(43, 8);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 37;
		this.label11.Text = "GLOSARIO:";
		this.txtComentario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtComentario.BackColor = System.Drawing.Color.PaleTurquoise;
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtComentario.ForeColor = System.Drawing.Color.FromArgb(255, 79, 79);
		this.txtComentario.Location = new System.Drawing.Point(114, 4);
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(1115, 22);
		this.txtComentario.TabIndex = 0;
		this.dgvMovimientosCaja.AllowUserToAddRows = false;
		this.dgvMovimientosCaja.AllowUserToDeleteRows = false;
		this.dgvMovimientosCaja.AllowUserToResizeRows = false;
		this.dgvMovimientosCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvMovimientosCaja.Columns.AddRange(this.ndoc, this.fechas, this.codCli, this.cliente, this.total, this.doc, this.mon, this.formap, this.sucursal, this.codctactemov, this.codban, this.banco, this.codctacte, this.ctacte, this.numoperacion, this.fechamov, this.tran, this.egreso, this.ingreso, this.saldocta, this.concepto, this.tcventa, this.tccompra, this.documentoref, this.tipoproc);
		this.dgvMovimientosCaja.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvMovimientosCaja.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvMovimientosCaja.Location = new System.Drawing.Point(0, 65);
		this.dgvMovimientosCaja.MultiSelect = false;
		this.dgvMovimientosCaja.Name = "dgvMovimientosCaja";
		this.dgvMovimientosCaja.ReadOnly = true;
		this.dgvMovimientosCaja.RowHeadersVisible = false;
		this.dgvMovimientosCaja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvMovimientosCaja.Size = new System.Drawing.Size(1241, 256);
		this.dgvMovimientosCaja.TabIndex = 9;
		this.dgvMovimientosCaja.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvMovimientosCajaChica_CellValueChanged);
		this.dgvMovimientosCaja.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvMovimientosCajaChica_CellDoubleClick);
		this.dgvMovimientosCaja.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvMovimientosCajaChica_ColumnHeaderMouseClick);
		this.dgvMovimientosCaja.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvMovimientosCajaChica_RowsAdded);
		this.dgvMovimientosCaja.CurrentCellDirtyStateChanged += new System.EventHandler(dgvMovimientosCajaChica_CurrentCellDirtyStateChanged);
		this.dgvMovimientosCaja.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvMovimientosCajaChica_RowsRemoved);
		this.dgvMovimientosCaja.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvMovimientosCajaChica_RowStateChanged);
		this.ndoc.DataPropertyName = "ndoc";
		this.ndoc.HeaderText = "Num.Doc";
		this.ndoc.Name = "ndoc";
		this.ndoc.ReadOnly = true;
		this.ndoc.Visible = false;
		this.fechas.DataPropertyName = "fechasalida";
		this.fechas.HeaderText = "Fecha Emision";
		this.fechas.Name = "fechas";
		this.fechas.ReadOnly = true;
		this.fechas.Visible = false;
		this.codCli.DataPropertyName = "dniruc_cli";
		this.codCli.HeaderText = "Ruc/Dni";
		this.codCli.Name = "codCli";
		this.codCli.ReadOnly = true;
		this.codCli.Visible = false;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Visible = false;
		this.cliente.Width = 300;
		this.total.DataPropertyName = "total_s";
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.total.Visible = false;
		this.doc.DataPropertyName = "doc";
		this.doc.HeaderText = "Documento";
		this.doc.Name = "doc";
		this.doc.ReadOnly = true;
		this.doc.Visible = false;
		this.doc.Width = 150;
		this.mon.DataPropertyName = "d_moneda";
		this.mon.HeaderText = "Moneda";
		this.mon.Name = "mon";
		this.mon.ReadOnly = true;
		this.mon.Visible = false;
		this.formap.DataPropertyName = "d_formap";
		this.formap.HeaderText = "FormaPago";
		this.formap.Name = "formap";
		this.formap.ReadOnly = true;
		this.formap.Visible = false;
		this.sucursal.DataPropertyName = "d_sucursal";
		this.sucursal.HeaderText = "Sucursal";
		this.sucursal.Name = "sucursal";
		this.sucursal.ReadOnly = true;
		this.sucursal.Visible = false;
		this.codctactemov.DataPropertyName = "codCtaCteMovimiento";
		this.codctactemov.HeaderText = "CodCtaCteMov";
		this.codctactemov.Name = "codctactemov";
		this.codctactemov.ReadOnly = true;
		this.codctactemov.Visible = false;
		this.codban.DataPropertyName = "codBanco";
		this.codban.HeaderText = "CodBanco";
		this.codban.Name = "codban";
		this.codban.ReadOnly = true;
		this.codban.Visible = false;
		this.banco.DataPropertyName = "NomBanco";
		this.banco.HeaderText = "Banco";
		this.banco.Name = "banco";
		this.banco.ReadOnly = true;
		this.banco.Visible = false;
		this.banco.Width = 200;
		this.codctacte.DataPropertyName = "codCuentaCorriente";
		this.codctacte.HeaderText = "CodCtaCte";
		this.codctacte.Name = "codctacte";
		this.codctacte.ReadOnly = true;
		this.codctacte.Visible = false;
		this.ctacte.DataPropertyName = "cuentacorriente";
		this.ctacte.HeaderText = "CtaCte";
		this.ctacte.Name = "ctacte";
		this.ctacte.ReadOnly = true;
		this.ctacte.Visible = false;
		this.ctacte.Width = 120;
		this.numoperacion.DataPropertyName = "NumTransaccion";
		this.numoperacion.HeaderText = "N° Operacion";
		this.numoperacion.Name = "numoperacion";
		this.numoperacion.ReadOnly = true;
		this.numoperacion.Visible = false;
		this.numoperacion.Width = 90;
		this.fechamov.DataPropertyName = "fechaMovimiento";
		this.fechamov.HeaderText = "Fecha Movimiento";
		this.fechamov.Name = "fechamov";
		this.fechamov.ReadOnly = true;
		this.fechamov.Visible = false;
		this.fechamov.Width = 130;
		this.tran.DataPropertyName = "Transaccion";
		this.tran.HeaderText = "Transaccion";
		this.tran.Name = "tran";
		this.tran.ReadOnly = true;
		this.tran.Visible = false;
		this.egreso.DataPropertyName = "egreso";
		this.egreso.HeaderText = "Egreso";
		this.egreso.Name = "egreso";
		this.egreso.ReadOnly = true;
		this.egreso.Visible = false;
		this.ingreso.DataPropertyName = "ingreso";
		this.ingreso.HeaderText = "Total";
		this.ingreso.Name = "ingreso";
		this.ingreso.ReadOnly = true;
		this.ingreso.Visible = false;
		this.ingreso.Width = 80;
		this.saldocta.DataPropertyName = "saldo";
		this.saldocta.HeaderText = "Saldo";
		this.saldocta.Name = "saldocta";
		this.saldocta.ReadOnly = true;
		this.saldocta.Visible = false;
		this.concepto.DataPropertyName = "descripcion";
		this.concepto.HeaderText = "Concepto";
		this.concepto.Name = "concepto";
		this.concepto.ReadOnly = true;
		this.concepto.Visible = false;
		this.concepto.Width = 350;
		this.tcventa.DataPropertyName = "tcventa";
		this.tcventa.HeaderText = "T.C.Venta";
		this.tcventa.Name = "tcventa";
		this.tcventa.ReadOnly = true;
		this.tcventa.Visible = false;
		this.tccompra.DataPropertyName = "tccompra";
		this.tccompra.HeaderText = "T.C.Compra";
		this.tccompra.Name = "tccompra";
		this.tccompra.ReadOnly = true;
		this.tccompra.Visible = false;
		this.documentoref.DataPropertyName = "documentoref";
		this.documentoref.HeaderText = "Documento Ref.";
		this.documentoref.Name = "documentoref";
		this.documentoref.ReadOnly = true;
		this.documentoref.Visible = false;
		this.documentoref.Width = 150;
		this.tipoproc.DataPropertyName = "tipo";
		this.tipoproc.HeaderText = "TipoProcedencia";
		this.tipoproc.Name = "tipoproc";
		this.tipoproc.ReadOnly = true;
		this.tipoproc.Visible = false;
		this.biVerificarRendicion.Image = (System.Drawing.Image)resources.GetObject("biVerificarRendicion.Image");
		this.biVerificarRendicion.ImagePaddingHorizontal = 20;
		this.biVerificarRendicion.ImagePaddingVertical = 10;
		this.biVerificarRendicion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biVerificarRendicion.Name = "biVerificarRendicion";
		this.biVerificarRendicion.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biRendicionLiquidada });
		this.biVerificarRendicion.SubItemsExpandWidth = 14;
		this.biVerificarRendicion.Text = "Verificar Rendiciones";
		this.biVerificarRendicion.Click += new System.EventHandler(biVerificarRendicion_Click);
		this.biRendicionLiquidada.Image = (System.Drawing.Image)resources.GetObject("biRendicionLiquidada.Image");
		this.biRendicionLiquidada.ImagePaddingHorizontal = 20;
		this.biRendicionLiquidada.ImagePaddingVertical = 10;
		this.biRendicionLiquidada.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRendicionLiquidada.Name = "biRendicionLiquidada";
		this.biRendicionLiquidada.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biHistorialRendiciones });
		this.biRendicionLiquidada.SubItemsExpandWidth = 14;
		this.biRendicionLiquidada.Text = "Rendicion Liquidadas";
		this.biRendicionLiquidada.Click += new System.EventHandler(biRendicionLiquidada_Click);
		this.biHistorialRendiciones.Image = (System.Drawing.Image)resources.GetObject("biHistorialRendiciones.Image");
		this.biHistorialRendiciones.ImagePaddingHorizontal = 20;
		this.biHistorialRendiciones.ImagePaddingVertical = 10;
		this.biHistorialRendiciones.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biHistorialRendiciones.Name = "biHistorialRendiciones";
		this.biHistorialRendiciones.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biBuscarRendicion });
		this.biHistorialRendiciones.SubItemsExpandWidth = 14;
		this.biHistorialRendiciones.Text = "Historial Rendiciones";
		this.biHistorialRendiciones.Visible = false;
		this.biHistorialRendiciones.Click += new System.EventHandler(biHistorialRendiciones_Click);
		this.biBuscarRendicion.Image = (System.Drawing.Image)resources.GetObject("biBuscarRendicion.Image");
		this.biBuscarRendicion.ImagePaddingHorizontal = 20;
		this.biBuscarRendicion.ImagePaddingVertical = 10;
		this.biBuscarRendicion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscarRendicion.Name = "biBuscarRendicion";
		this.biBuscarRendicion.SubItemsExpandWidth = 14;
		this.biBuscarRendicion.Text = "Buscar Rendicion";
		this.biBuscarRendicion.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1241, 421);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.dgvMovimientosCaja);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.btnSalir);
		this.DoubleBuffered = true;
		base.Name = "frmListaCaja";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Caja";
		base.Load += new System.EventHandler(frmListaCaja_Load);
		base.Shown += new System.EventHandler(frmListaCaja_Shown);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvMovimientosCaja).EndInit();
		base.ResumeLayout(false);
	}
}
