using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmCajaChicaRP : Office2007Form
{
	private clsAdmStatusCajaChica AdmSta = new clsAdmStatusCajaChica();

	private clsStatusCajaChica sta = new clsStatusCajaChica();

	private clsAdmAperturaCierre AdmApe = new clsAdmAperturaCierre();

	private clsCaja ape = new clsCaja();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private int StadoCaja = 0;

	private IContainer components = null;

	private GroupBox gbApertura;

	private GroupBox gbIngreso;

	private GroupBox gbEgresos;

	private GroupBox gbCierre;

	private GroupBox gbVentasCredito;

	private GroupBox gbComprasCredito;

	private Panel panel1;

	private Label label1;

	private Label lbAperturaCaja;

	private Panel panel2;

	private Label label3;

	private Label label4;

	private Panel panel3;

	private Label label6;

	private Label lbIngresos;

	private Label lbTotalVentas;

	private Label lbSubTotalIngresos;

	private Panel panel4;

	private Panel panel5;

	private Label lbSubTotalEgresos;

	private Label label12;

	private Label lbEgresos;

	private Label lbTotalPagos;

	private Label label9;

	private Label label8;

	private Label label7;

	private Panel panel6;

	private Label lbTotalCaja;

	private Label label2;

	private Panel panel7;

	private Label lbTotalVentasCredito;

	private Label label14;

	private Panel panel8;

	private Label lbTotalComprasCredito;

	private Label label16;

	private Label label5;

	private ImageList imageList1;

	private Button btnAnularCierre;

	private Button btnsalir;

	private Button btnCerrarCaja;

	private Button button1;

	private Label label22;

	private Label label21;

	private DateTimePicker dtpfechaHasta;

	private DateTimePicker dtpfechaDesde;

	public frmCajaChicaRP()
	{
		InitializeComponent();
	}

	private void CargaStatusFlujosCaja()
	{
		try
		{
			sta = AdmSta.CargaStatusFlujosCajaChica(dtpfechaDesde.Value, dtpfechaHasta.Value);
			if (sta != null)
			{
				lbAperturaCaja.Text = Convert.ToString(sta.AperturaCaja);
				lbTotalVentas.Text = Convert.ToString(sta.TotalVentas);
				lbIngresos.Text = Convert.ToString(sta.Ingresos);
				lbSubTotalIngresos.Text = Convert.ToString(sta.TotalVentas + sta.Ingresos);
				lbTotalPagos.Text = Convert.ToString(sta.TotalPagos);
				lbEgresos.Text = Convert.ToString(sta.Egresos);
				lbSubTotalEgresos.Text = Convert.ToString(sta.TotalPagos + sta.Egresos);
				lbTotalVentasCredito.Text = Convert.ToString(sta.PorPagar);
				lbTotalComprasCredito.Text = Convert.ToString(sta.PorCobrar);
				decimal Ingresos = Convert.ToDecimal(sta.TotalVentas + sta.Ingresos);
				decimal Egresos = Convert.ToDecimal(sta.TotalPagos + sta.Egresos);
				decimal Entradas = Convert.ToDecimal(sta.AperturaCaja + sta.TotalVentas + sta.Ingresos);
				decimal Salidas = Convert.ToDecimal(sta.TotalPagos + sta.Egresos);
				lbTotalCaja.Text = Convert.ToString(Entradas - Salidas + sta.SumaAperturasCaja - sta.SumaCierresCaja);
				lbTotalVentasCredito.Text = Convert.ToString(sta.PorCobrar);
				lbTotalComprasCredito.Text = Convert.ToString(sta.PorPagar);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void CargaStatusFlujosCaja_SP()
	{
		try
		{
			sta = AdmSta.CargaStatusFlujosCajaChica_SP(dtpfechaDesde.Value);
			if (sta != null)
			{
				lbAperturaCaja.Text = Convert.ToString(sta.AperturaCaja);
				lbTotalVentas.Text = Convert.ToString(sta.TotalVentas);
				lbIngresos.Text = Convert.ToString(sta.Ingresos);
				lbSubTotalIngresos.Text = Convert.ToString(sta.TotalVentas + sta.Ingresos);
				lbTotalPagos.Text = Convert.ToString(sta.TotalPagos);
				lbEgresos.Text = Convert.ToString(sta.Egresos);
				lbSubTotalEgresos.Text = Convert.ToString(sta.TotalPagos + sta.Egresos);
				lbTotalVentasCredito.Text = Convert.ToString(sta.PorPagar);
				lbTotalComprasCredito.Text = Convert.ToString(sta.PorCobrar);
				lbTotalCaja.Text = Convert.ToString(sta.AperturaCaja + (sta.TotalVentas + sta.Ingresos) - (sta.TotalPagos + sta.Egresos));
				lbTotalVentasCredito.Text = Convert.ToString(sta.PorCobrar);
				lbTotalComprasCredito.Text = Convert.ToString(sta.PorPagar);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void VerificarCierreCaja()
	{
		try
		{
			sta = AdmSta.VerificaStadoCaja();
			if (sta != null)
			{
				StadoCaja = Convert.ToInt32(sta.Cantidad);
			}
			if (StadoCaja == 0)
			{
				btnAnularCierre.Visible = false;
				btnCerrarCaja.Enabled = true;
				btnCerrarCaja.Visible = true;
			}
			else
			{
				btnAnularCierre.Visible = true;
				btnCerrarCaja.Enabled = false;
				btnCerrarCaja.Visible = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void frmCajaChica_Load(object sender, EventArgs e)
	{
		try
		{
			dtpfechaDesde.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
			dtpfechaHasta.MinDate = Convert.ToDateTime(DateTime.Now.ToString());
			gbApertura.Text = "  APERTURA DE CAJA  " + Convert.ToString(dtpfechaDesde.Text) + "  ";
			gbCierre.Text = "  CIERRE DE CAJA  " + Convert.ToString(dtpfechaHasta.Text) + "  ";
			CargaStatusFlujosCaja_SP();
			VerificarCierreCaja();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dtpfechaDesde_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			if (dtpfechaDesde.Value.Date == dtpfechaHasta.Value.Date)
			{
				CargaStatusFlujosCaja_SP();
			}
			else
			{
				CargaStatusFlujosCaja();
			}
			gbApertura.Text = "  APERTURA DE CAJA  " + Convert.ToString(dtpfechaDesde.Text) + "  ";
			dtpfechaHasta.MinDate = Convert.ToDateTime(dtpfechaDesde.Value);
			if (dtpfechaDesde.Value.Date == dtpfechaHasta.Value.Date)
			{
				VerificarCierreCaja();
				btnCerrarCaja.Visible = true;
				btnAnularCierre.Visible = true;
			}
			else
			{
				btnCerrarCaja.Enabled = false;
				btnCerrarCaja.Visible = false;
				btnAnularCierre.Visible = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dtpfechaHasta_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			if (dtpfechaDesde.Value.Date == dtpfechaHasta.Value.Date)
			{
				CargaStatusFlujosCaja_SP();
			}
			else
			{
				CargaStatusFlujosCaja();
			}
			VerificarCierreCaja();
			gbCierre.Text = "  CIERRE DE CAJA  " + Convert.ToString(dtpfechaHasta.Text) + "  ";
			dtpfechaDesde.MaxDate = Convert.ToDateTime(dtpfechaHasta.Value);
			if (dtpfechaDesde.Value.Date == dtpfechaHasta.Value.Date)
			{
				VerificarCierreCaja();
				btnCerrarCaja.Visible = true;
				btnAnularCierre.Visible = true;
			}
			else
			{
				btnCerrarCaja.Enabled = false;
				btnCerrarCaja.Visible = false;
				btnAnularCierre.Visible = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			dtpfechaDesde.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
			dtpfechaHasta.MinDate = Convert.ToDateTime(DateTime.Now.ToString());
			dtpfechaDesde.Value = Convert.ToDateTime(DateTime.Now.ToString());
			dtpfechaHasta.Value = Convert.ToDateTime(DateTime.Now.ToString());
			CargaStatusFlujosCaja_SP();
			VerificarCierreCaja();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnCerrarCaja_Click(object sender, EventArgs e)
	{
		try
		{
			ape.Montocierre = Convert.ToDecimal(lbTotalCaja.Text);
			ape.Codsucursal = frmLogin.iCodAlmacen;
			ape.CodUser = frmLogin.iCodUser;
			if (AdmApe.UpdateCierre(ape))
			{
				VerificarCierreCaja();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void btnAnularCierre_Click(object sender, EventArgs e)
	{
		try
		{
			if (AdmApe.AnularCierre(frmLogin.iCodAlmacen))
			{
				VerificarCierreCaja();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCajaChicaRP));
		this.gbApertura = new System.Windows.Forms.GroupBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lbAperturaCaja = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.gbIngreso = new System.Windows.Forms.GroupBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lbSubTotalIngresos = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.lbIngresos = new System.Windows.Forms.Label();
		this.lbTotalVentas = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.gbEgresos = new System.Windows.Forms.GroupBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.label7 = new System.Windows.Forms.Label();
		this.lbSubTotalEgresos = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.panel4 = new System.Windows.Forms.Panel();
		this.lbEgresos = new System.Windows.Forms.Label();
		this.lbTotalPagos = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.gbCierre = new System.Windows.Forms.GroupBox();
		this.panel6 = new System.Windows.Forms.Panel();
		this.lbTotalCaja = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.gbVentasCredito = new System.Windows.Forms.GroupBox();
		this.panel7 = new System.Windows.Forms.Panel();
		this.lbTotalVentasCredito = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.gbComprasCredito = new System.Windows.Forms.GroupBox();
		this.panel8 = new System.Windows.Forms.Panel();
		this.label5 = new System.Windows.Forms.Label();
		this.lbTotalComprasCredito = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAnularCierre = new System.Windows.Forms.Button();
		this.btnsalir = new System.Windows.Forms.Button();
		this.btnCerrarCaja = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.dtpfechaHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpfechaDesde = new System.Windows.Forms.DateTimePicker();
		this.gbApertura.SuspendLayout();
		this.panel1.SuspendLayout();
		this.gbIngreso.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel2.SuspendLayout();
		this.gbEgresos.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel4.SuspendLayout();
		this.gbCierre.SuspendLayout();
		this.panel6.SuspendLayout();
		this.gbVentasCredito.SuspendLayout();
		this.panel7.SuspendLayout();
		this.gbComprasCredito.SuspendLayout();
		this.panel8.SuspendLayout();
		base.SuspendLayout();
		this.gbApertura.Controls.Add(this.panel1);
		this.gbApertura.Location = new System.Drawing.Point(0, 52);
		this.gbApertura.Name = "gbApertura";
		this.gbApertura.Size = new System.Drawing.Size(313, 60);
		this.gbApertura.TabIndex = 0;
		this.gbApertura.TabStop = false;
		this.gbApertura.Text = "APERTURA DE CAJA";
		this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel1.Controls.Add(this.lbAperturaCaja);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Location = new System.Drawing.Point(7, 19);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(294, 36);
		this.panel1.TabIndex = 0;
		this.lbAperturaCaja.AutoSize = true;
		this.lbAperturaCaja.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbAperturaCaja.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbAperturaCaja.Location = new System.Drawing.Point(173, 9);
		this.lbAperturaCaja.Name = "lbAperturaCaja";
		this.lbAperturaCaja.Size = new System.Drawing.Size(104, 23);
		this.lbAperturaCaja.TabIndex = 1;
		this.lbAperturaCaja.Text = "00000000.00";
		this.lbAperturaCaja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.label1.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(62, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(85, 20);
		this.label1.TabIndex = 0;
		this.label1.Text = "MONTO:  S/.";
		this.gbIngreso.Controls.Add(this.panel3);
		this.gbIngreso.Controls.Add(this.panel2);
		this.gbIngreso.Location = new System.Drawing.Point(1, 118);
		this.gbIngreso.Name = "gbIngreso";
		this.gbIngreso.Size = new System.Drawing.Size(313, 133);
		this.gbIngreso.TabIndex = 1;
		this.gbIngreso.TabStop = false;
		this.gbIngreso.Text = "INGRESOS";
		this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel3.Controls.Add(this.lbSubTotalIngresos);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Location = new System.Drawing.Point(7, 90);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(294, 37);
		this.panel3.TabIndex = 1;
		this.lbSubTotalIngresos.AutoSize = true;
		this.lbSubTotalIngresos.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbSubTotalIngresos.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbSubTotalIngresos.Location = new System.Drawing.Point(173, 7);
		this.lbSubTotalIngresos.Name = "lbSubTotalIngresos";
		this.lbSubTotalIngresos.Size = new System.Drawing.Size(104, 23);
		this.lbSubTotalIngresos.TabIndex = 1;
		this.lbSubTotalIngresos.Text = "00000000.00";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(35, 7);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(112, 20);
		this.label6.TabIndex = 0;
		this.label6.Text = "SUB TOTAL:  S/.";
		this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel2.Controls.Add(this.lbIngresos);
		this.panel2.Controls.Add(this.lbTotalVentas);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.label3);
		this.panel2.Location = new System.Drawing.Point(7, 19);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(294, 65);
		this.panel2.TabIndex = 0;
		this.lbIngresos.AutoSize = true;
		this.lbIngresos.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbIngresos.Location = new System.Drawing.Point(173, 39);
		this.lbIngresos.Name = "lbIngresos";
		this.lbIngresos.Size = new System.Drawing.Size(104, 23);
		this.lbIngresos.TabIndex = 3;
		this.lbIngresos.Text = "00000000.00";
		this.lbTotalVentas.AutoSize = true;
		this.lbTotalVentas.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalVentas.Location = new System.Drawing.Point(173, 9);
		this.lbTotalVentas.Name = "lbTotalVentas";
		this.lbTotalVentas.Size = new System.Drawing.Size(104, 23);
		this.lbTotalVentas.TabIndex = 2;
		this.lbTotalVentas.Text = "00000000.00";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(41, 42);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(106, 20);
		this.label4.TabIndex = 1;
		this.label4.Text = "INGRESOS:  S/.";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(13, 12);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(134, 20);
		this.label3.TabIndex = 0;
		this.label3.Text = "TOTAL VENTAS:  S/.";
		this.gbEgresos.Controls.Add(this.panel5);
		this.gbEgresos.Controls.Add(this.panel4);
		this.gbEgresos.Location = new System.Drawing.Point(0, 257);
		this.gbEgresos.Name = "gbEgresos";
		this.gbEgresos.Size = new System.Drawing.Size(313, 131);
		this.gbEgresos.TabIndex = 1;
		this.gbEgresos.TabStop = false;
		this.gbEgresos.Text = "EGRESOS";
		this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel5.Controls.Add(this.label7);
		this.panel5.Controls.Add(this.lbSubTotalEgresos);
		this.panel5.Controls.Add(this.label12);
		this.panel5.Location = new System.Drawing.Point(7, 91);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(294, 34);
		this.panel5.TabIndex = 1;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.Red;
		this.label7.Location = new System.Drawing.Point(276, 1);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(15, 23);
		this.label7.TabIndex = 2;
		this.label7.Text = "-";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbSubTotalEgresos.AutoSize = true;
		this.lbSubTotalEgresos.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbSubTotalEgresos.ForeColor = System.Drawing.Color.Red;
		this.lbSubTotalEgresos.Location = new System.Drawing.Point(173, 1);
		this.lbSubTotalEgresos.Name = "lbSubTotalEgresos";
		this.lbSubTotalEgresos.Size = new System.Drawing.Size(104, 23);
		this.lbSubTotalEgresos.TabIndex = 1;
		this.lbSubTotalEgresos.Text = "00000000.00";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(30, 4);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(116, 20);
		this.label12.TabIndex = 0;
		this.label12.Text = "SUB TOTAL:   S/.";
		this.panel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel4.Controls.Add(this.lbEgresos);
		this.panel4.Controls.Add(this.lbTotalPagos);
		this.panel4.Controls.Add(this.label9);
		this.panel4.Controls.Add(this.label8);
		this.panel4.Location = new System.Drawing.Point(7, 20);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(294, 65);
		this.panel4.TabIndex = 0;
		this.lbEgresos.AutoSize = true;
		this.lbEgresos.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbEgresos.Location = new System.Drawing.Point(173, 37);
		this.lbEgresos.Name = "lbEgresos";
		this.lbEgresos.Size = new System.Drawing.Size(104, 23);
		this.lbEgresos.TabIndex = 3;
		this.lbEgresos.Text = "00000000.00";
		this.lbTotalPagos.AutoSize = true;
		this.lbTotalPagos.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalPagos.Location = new System.Drawing.Point(173, 10);
		this.lbTotalPagos.Name = "lbTotalPagos";
		this.lbTotalPagos.Size = new System.Drawing.Size(104, 23);
		this.lbTotalPagos.TabIndex = 2;
		this.lbTotalPagos.Text = "00000000.00";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(39, 40);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(107, 20);
		this.label9.TabIndex = 1;
		this.label9.Text = "EGRESOS:   S/.";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(17, 13);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(129, 20);
		this.label8.TabIndex = 0;
		this.label8.Text = "TOTAL PAGOS:  S/.";
		this.gbCierre.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.gbCierre.Controls.Add(this.panel6);
		this.gbCierre.Location = new System.Drawing.Point(1, 394);
		this.gbCierre.Name = "gbCierre";
		this.gbCierre.Size = new System.Drawing.Size(313, 57);
		this.gbCierre.TabIndex = 2;
		this.gbCierre.TabStop = false;
		this.gbCierre.Text = "CIERRE DE CAJA";
		this.panel6.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel6.Controls.Add(this.lbTotalCaja);
		this.panel6.Controls.Add(this.label2);
		this.panel6.Location = new System.Drawing.Point(7, 20);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(294, 31);
		this.panel6.TabIndex = 0;
		this.lbTotalCaja.AutoSize = true;
		this.lbTotalCaja.Font = new System.Drawing.Font("Arial Narrow", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalCaja.ForeColor = System.Drawing.Color.DarkSlateBlue;
		this.lbTotalCaja.Location = new System.Drawing.Point(172, 1);
		this.lbTotalCaja.Name = "lbTotalCaja";
		this.lbTotalCaja.Size = new System.Drawing.Size(105, 24);
		this.lbTotalCaja.TabIndex = 1;
		this.lbTotalCaja.Text = "00000000.00";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(9, 4);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(138, 20);
		this.label2.TabIndex = 0;
		this.label2.Text = "TOTAL EN CAJA:  S/.";
		this.gbVentasCredito.Controls.Add(this.panel7);
		this.gbVentasCredito.Location = new System.Drawing.Point(0, 457);
		this.gbVentasCredito.Name = "gbVentasCredito";
		this.gbVentasCredito.Size = new System.Drawing.Size(313, 53);
		this.gbVentasCredito.TabIndex = 1;
		this.gbVentasCredito.TabStop = false;
		this.gbVentasCredito.Text = "VENTAS A CREDITO";
		this.panel7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel7.Controls.Add(this.lbTotalVentasCredito);
		this.panel7.Controls.Add(this.label14);
		this.panel7.Location = new System.Drawing.Point(6, 12);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(294, 35);
		this.panel7.TabIndex = 0;
		this.lbTotalVentasCredito.AutoSize = true;
		this.lbTotalVentasCredito.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalVentasCredito.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbTotalVentasCredito.Location = new System.Drawing.Point(173, 1);
		this.lbTotalVentasCredito.Name = "lbTotalVentasCredito";
		this.lbTotalVentasCredito.Size = new System.Drawing.Size(104, 23);
		this.lbTotalVentasCredito.TabIndex = 1;
		this.lbTotalVentasCredito.Text = "00000000.00";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(68, 4);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(78, 20);
		this.label14.TabIndex = 0;
		this.label14.Text = "TOTAL:  S/.";
		this.gbComprasCredito.Controls.Add(this.panel8);
		this.gbComprasCredito.Location = new System.Drawing.Point(1, 516);
		this.gbComprasCredito.Name = "gbComprasCredito";
		this.gbComprasCredito.Size = new System.Drawing.Size(313, 61);
		this.gbComprasCredito.TabIndex = 3;
		this.gbComprasCredito.TabStop = false;
		this.gbComprasCredito.Text = "COMPRAS A CREDITO";
		this.panel8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel8.Controls.Add(this.label5);
		this.panel8.Controls.Add(this.lbTotalComprasCredito);
		this.panel8.Controls.Add(this.label16);
		this.panel8.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.panel8.Location = new System.Drawing.Point(7, 20);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(294, 35);
		this.panel8.TabIndex = 0;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.Red;
		this.label5.Location = new System.Drawing.Point(276, 4);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(15, 23);
		this.label5.TabIndex = 3;
		this.label5.Text = "-";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbTotalComprasCredito.AutoSize = true;
		this.lbTotalComprasCredito.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalComprasCredito.ForeColor = System.Drawing.Color.Red;
		this.lbTotalComprasCredito.Location = new System.Drawing.Point(172, 4);
		this.lbTotalComprasCredito.Name = "lbTotalComprasCredito";
		this.lbTotalComprasCredito.Size = new System.Drawing.Size(104, 23);
		this.lbTotalComprasCredito.TabIndex = 1;
		this.lbTotalComprasCredito.Text = "00000000.00";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(68, 7);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(78, 20);
		this.label16.TabIndex = 0;
		this.label16.Text = "TOTAL:  S/.";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(8, "close.png");
		this.btnAnularCierre.ImageIndex = 7;
		this.btnAnularCierre.ImageList = this.imageList1;
		this.btnAnularCierre.Location = new System.Drawing.Point(0, 583);
		this.btnAnularCierre.Name = "btnAnularCierre";
		this.btnAnularCierre.Size = new System.Drawing.Size(101, 32);
		this.btnAnularCierre.TabIndex = 25;
		this.btnAnularCierre.Text = "Anular Cierre";
		this.btnAnularCierre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnularCierre.UseVisualStyleBackColor = true;
		this.btnAnularCierre.Click += new System.EventHandler(btnAnularCierre_Click);
		this.btnsalir.ImageIndex = 5;
		this.btnsalir.ImageList = this.imageList1;
		this.btnsalir.Location = new System.Drawing.Point(234, 583);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(64, 32);
		this.btnsalir.TabIndex = 24;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnsalir.UseVisualStyleBackColor = true;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.btnCerrarCaja.ImageIndex = 8;
		this.btnCerrarCaja.ImageList = this.imageList1;
		this.btnCerrarCaja.Location = new System.Drawing.Point(116, 583);
		this.btnCerrarCaja.Name = "btnCerrarCaja";
		this.btnCerrarCaja.Size = new System.Drawing.Size(97, 32);
		this.btnCerrarCaja.TabIndex = 23;
		this.btnCerrarCaja.Text = "Cerrar Caja";
		this.btnCerrarCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCerrarCaja.UseVisualStyleBackColor = true;
		this.btnCerrarCaja.Click += new System.EventHandler(btnCerrarCaja_Click);
		this.button1.ImageIndex = 7;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(287, 12);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(27, 21);
		this.button1.TabIndex = 30;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(159, 17);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(14, 13);
		this.label22.TabIndex = 29;
		this.label22.Text = "Y";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(5, 17);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(47, 13);
		this.label21.TabIndex = 28;
		this.label21.Text = "ENTRE:";
		this.dtpfechaHasta.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
		this.dtpfechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfechaHasta.Location = new System.Drawing.Point(179, 12);
		this.dtpfechaHasta.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
		this.dtpfechaHasta.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtpfechaHasta.Name = "dtpfechaHasta";
		this.dtpfechaHasta.Size = new System.Drawing.Size(100, 21);
		this.dtpfechaHasta.TabIndex = 27;
		this.dtpfechaHasta.ValueChanged += new System.EventHandler(dtpfechaHasta_ValueChanged);
		this.dtpfechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpfechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfechaDesde.Location = new System.Drawing.Point(54, 12);
		this.dtpfechaDesde.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
		this.dtpfechaDesde.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtpfechaDesde.Name = "dtpfechaDesde";
		this.dtpfechaDesde.Size = new System.Drawing.Size(100, 21);
		this.dtpfechaDesde.TabIndex = 26;
		this.dtpfechaDesde.ValueChanged += new System.EventHandler(dtpfechaDesde_ValueChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.ClientSize = new System.Drawing.Size(313, 624);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.label22);
		base.Controls.Add(this.label21);
		base.Controls.Add(this.dtpfechaHasta);
		base.Controls.Add(this.dtpfechaDesde);
		base.Controls.Add(this.btnAnularCierre);
		base.Controls.Add(this.btnsalir);
		base.Controls.Add(this.btnCerrarCaja);
		base.Controls.Add(this.gbComprasCredito);
		base.Controls.Add(this.gbVentasCredito);
		base.Controls.Add(this.gbCierre);
		base.Controls.Add(this.gbEgresos);
		base.Controls.Add(this.gbIngreso);
		base.Controls.Add(this.gbApertura);
		this.DoubleBuffered = true;
		base.Name = "frmCajaChica";
		this.Text = "STATUS Y CONTROL DE CAJA";
		base.Load += new System.EventHandler(frmCajaChica_Load);
		this.gbApertura.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.gbIngreso.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.gbEgresos.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.gbCierre.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		this.gbVentasCredito.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		this.gbComprasCredito.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		this.panel8.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
