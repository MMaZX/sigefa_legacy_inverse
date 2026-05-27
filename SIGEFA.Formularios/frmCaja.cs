using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmCaja : Office2007Form
{
	private clsAdmStatusCajaChica AdmSta = new clsAdmStatusCajaChica();

	private clsStatusCajaChica sta = new clsStatusCajaChica();

	private clsAdmAperturaCierre AdmApe = new clsAdmAperturaCierre();

	private clsCaja ape = new clsCaja();

	private clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

	private clsFlujoCaja flu = new clsFlujoCaja();

	private clsAdmFlujoCaja admFlu = new clsAdmFlujoCaja();

	private clsTipoCambio tcambio = new clsTipoCambio();

	private clsAdmTipoCambio admTipoCambio = new clsAdmTipoCambio();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private int StadoCaja = 0;

	private double Ingresado = 0.0;

	private double Soles = 0.0;

	public bool depo;

	public DateTime fecha_Depo;

	private decimal TotalVentasMesSoles = default(decimal);

	private decimal TotalVentasMesDolar = default(decimal);

	private IContainer components = null;

	private GroupBox gbApertura;

	private GroupBox gbIngreso;

	private GroupBox gbCierre;

	private GroupBox gbVentasCredito;

	private GroupBox gbComprasCredito;

	private Panel panel1;

	private Label label1;

	private Label lbAperturaCaja;

	private Panel panel6;

	private Label lbTotalCaja;

	private Panel panel7;

	private Label lbTotalVentasCredito_S;

	private Label label14;

	private Panel panel8;

	private Label lbTotalComprasCredito_S;

	private Label label16;

	private ImageList imageList1;

	private Button btnAnularCierre;

	private Button btnsalir;

	private Button btnCerrarCaja;

	private Button button1;

	private Label label22;

	private Label label21;

	private DateTimePicker dtpfechaHasta;

	private DateTimePicker dtpfechaDesde;

	private Button btnImprimir;

	private Panel panel9;

	private Label lbDeposito_S;

	private Label label10;

	private Label lbDeposito_D;

	private GroupBox groupBox2;

	private Panel panel17;

	private Label label44;

	private GroupBox groupBox3;

	private Panel panel3;

	private Label lbTarjeta_D;

	private Label lbTarjeta_S;

	private Label label9;

	private GroupBox groupBox1;

	private Panel panel2;

	private Label lbCheque_D;

	private Label lbCheque_S;

	private Label label15;

	private GroupBox groupBox4;

	private Panel panel5;

	private Label lb_Transferencia_D;

	private Label lbTransferencia_S;

	private Label label20;

	private Label lbVentasCredito_D;

	private Label lbComprasCredito_D;

	private GroupBox groupBox5;

	private Panel panel10;

	private Label lbEfectivo_D;

	private Label lbEfectivo_S;

	private Label label28;

	private Label label27;

	private Label label47;

	private Panel panel11;

	private Label lbSubTotalIngresos_D;

	private Label lbSubTotalIngresos_S;

	private Label label11;

	private Label lbTotalVentas_D;

	private Label lbTotalVentas_S;

	private Label label13;

	private GroupBox groupBox6;

	private Panel panel4;

	private Label lbSubtotalEgresos_D;

	private Label lbSubTotalEgresos_S;

	private Label label26;

	private Panel panel12;

	private Label lbMontoDepositado_D;

	private Label lbMontoDepositado_S;

	private Label label31;

	private Label label32;

	private Label lbIngresos_D;

	private Label lbIngresos_S;

	private Label lbEgresos_D;

	private Label lbEgresos_S;

	private Label label37;

	private Label label38;

	private GroupBox groupBox7;

	private Label label65;

	private Label label66;

	private Label lbTotalMes_D;

	private Label lbTotalMes_S;

	private Label label64;

	private Label lbPorCobrarMes_D;

	private Label lbPorCobrarMes_S;

	private Label label41;

	private Label lbTotalVentasMes_D;

	private Label lbTotalVentasMes_S;

	private Label label61;

	private Label lbTransferenciaMes_D;

	private Label lbTransferenciaMes_S;

	private Label label55;

	private Label lbChequeMes_D;

	private Label lbChequeMes_S;

	private Label label52;

	private Label lbTarjetaMes_D;

	private Label lbTarjetaMes_S;

	private Label label49;

	private Label lbDepositoMes_D;

	private Label lbDepositoMes_S;

	private Label label45;

	private Label lbEfectivoMes_D;

	private Label lbEfectivoMes_S;

	private Label label58;

	private Label lbTotalCobranzas_D;

	private Label lbTotalCobranzas_S;

	private Label label5;

	private Label lbTotalCajaDolares;

	private Label label2;

	public frmCaja()
	{
		InitializeComponent();
	}

	private void CargaStatusFlujosCaja()
	{
		try
		{
			sta = AdmSta.CargaStatusFlujosCaja(dtpfechaDesde.Value, dtpfechaHasta.Value, frmLogin.iCodSucursal);
			if (sta != null)
			{
				lbAperturaCaja.Text = Convert.ToString(sta.AperturaCaja);
				lbTotalVentas_S.Text = Convert.ToString(sta.TotalVentas);
				lbTotalVentas_D.Text = Convert.ToString(sta.TotalVentasDolar);
				lbIngresos_S.Text = Convert.ToString(sta.Ingresos);
				lbSubTotalIngresos_S.Text = Convert.ToString(sta.TotalVentas + sta.Ingresos + sta.CobranzaSolesTotal);
				lbSubTotalIngresos_D.Text = Convert.ToString(sta.TotalVentasDolar + sta.CobranzaDolaresTotal);
				lbMontoDepositado_S.Text = Convert.ToString(sta.MontoDepositado);
				lbEgresos_S.Text = Convert.ToString(sta.Egresos);
				lbSubTotalEgresos_S.Text = Convert.ToString(sta.MontoDepositado + sta.Egresos);
				lbTotalVentasCredito_S.Text = Convert.ToString(sta.PorCobrar);
				lbVentasCredito_D.Text = Convert.ToString(sta.PorCobrarDolar);
				lbTotalComprasCredito_S.Text = Convert.ToString(sta.PorPagar);
				lbComprasCredito_D.Text = Convert.ToString(sta.PorPagarDolar);
				lbEfectivo_S.Text = Convert.ToString(sta.SumaVentasEfectivoDia);
				lbEfectivo_D.Text = Convert.ToString(sta.SumaVentasEfectivoDiaDolar);
				lbDeposito_S.Text = Convert.ToString(sta.SumaVentasDepositoDia);
				lbDeposito_D.Text = Convert.ToString(sta.SumaVentasDepositoDiaDolar);
				lbTarjeta_S.Text = Convert.ToString(sta.SumaVentasTarjetaDia);
				lbTarjeta_D.Text = Convert.ToString(sta.SumaVentasTarjetaDiaDolar);
				lbCheque_S.Text = Convert.ToString(sta.SumaVentasChequeDia);
				lbCheque_D.Text = Convert.ToString(sta.SumaVentasChequeDiaDolar);
				lbTransferencia_S.Text = Convert.ToString(sta.SumaVentasTransferenciaDia);
				lb_Transferencia_D.Text = Convert.ToString(sta.SumaVentasTransferenciaDiaDolar);
				lbTotalCaja.Text = Convert.ToString(sta.AperturaCaja + (sta.TotalVentas + sta.Ingresos + sta.CobranzaSolesTotal) - (sta.MontoDepositado + sta.Egresos));
				lbTotalCajaDolares.Text = Convert.ToString(sta.TotalVentasDolar + sta.IngresosDolar + sta.CobranzaDolaresTotal - sta.EgresosDolar);
				lbEfectivoMes_S.Text = Convert.ToString(sta.SumaVentasEfectivoMes);
				lbEfectivoMes_D.Text = Convert.ToString(sta.SumaVentasEfectivoMesDolar);
				lbDepositoMes_S.Text = Convert.ToString(sta.SumaVentasDepositoMes);
				lbDepositoMes_D.Text = Convert.ToString(sta.SumaVentasDepositoMesDolar);
				lbTarjetaMes_S.Text = Convert.ToString(sta.SumaVentasTarjetaMes);
				lbTarjetaMes_D.Text = Convert.ToString(sta.SumaVentasTarjetaMesDolar);
				lbChequeMes_S.Text = Convert.ToString(sta.SumaVentasChequeMes);
				lbChequeMes_D.Text = Convert.ToString(sta.SumaVentasChequeMesDolar);
				lbTransferenciaMes_S.Text = Convert.ToString(sta.SumaVentasTransferenciaMes);
				lbTransferenciaMes_D.Text = Convert.ToString(sta.SumaVentasTransferenciaMesDolar);
				TotalVentasMesSoles = sta.SumaVentasEfectivoMes + sta.SumaVentasDepositoMes + sta.SumaVentasTarjetaMes + sta.SumaVentasChequeMes + sta.SumaVentasTransferenciaMes;
				TotalVentasMesDolar = sta.SumaVentasEfectivoMesDolar + sta.SumaVentasDepositoMesDolar + sta.SumaVentasTarjetaMesDolar + sta.SumaVentasChequeMesDolar + sta.SumaVentasTransferenciaMesDolar;
				lbTotalVentasMes_S.Text = Convert.ToString(TotalVentasMesSoles);
				lbTotalVentasMes_D.Text = Convert.ToString(TotalVentasMesDolar);
				lbPorCobrarMes_S.Text = Convert.ToString(sta.PorCobrarMes);
				lbPorCobrarMes_D.Text = Convert.ToString(sta.PorCobrarMesDolar);
				lbTotalMes_S.Text = Convert.ToString(sta.TotalVentasMes + sta.PorCobrarMes);
				lbTotalMes_D.Text = Convert.ToString(sta.TotalVentasMesDolar + sta.PorCobrarMesDolar);
				lbTotalCobranzas_S.Text = Convert.ToString(sta.CobranzaSolesTotal);
				lbTotalCobranzas_D.Text = Convert.ToString(sta.CobranzaDolaresTotal);
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
			sta = AdmSta.CargaStatusFlujosCaja_SP(dtpfechaDesde.Value, frmLogin.iCodSucursal);
			if (sta != null)
			{
				lbAperturaCaja.Text = Convert.ToString(sta.AperturaCaja);
				lbTotalVentas_S.Text = Convert.ToString(sta.TotalVentas);
				lbTotalVentas_D.Text = Convert.ToString(sta.TotalVentasDolar);
				lbIngresos_S.Text = Convert.ToString(sta.Ingresos);
				lbSubTotalIngresos_S.Text = Convert.ToString(sta.TotalVentas + sta.Ingresos + sta.CobranzaSolesTotal);
				lbSubTotalIngresos_D.Text = Convert.ToString(sta.TotalVentasDolar + sta.CobranzaDolaresTotal);
				lbMontoDepositado_S.Text = Convert.ToString(sta.MontoDepositado);
				lbEgresos_S.Text = Convert.ToString(sta.Egresos);
				lbSubTotalEgresos_S.Text = Convert.ToString(sta.MontoDepositado + sta.Egresos);
				lbTotalVentasCredito_S.Text = Convert.ToString(sta.PorCobrar);
				lbVentasCredito_D.Text = Convert.ToString(sta.PorCobrarDolar);
				lbTotalComprasCredito_S.Text = Convert.ToString(sta.PorPagar);
				lbComprasCredito_D.Text = Convert.ToString(sta.PorPagarDolar);
				lbEfectivo_S.Text = Convert.ToString(sta.SumaVentasEfectivoDia);
				lbEfectivo_D.Text = Convert.ToString(sta.SumaVentasEfectivoDiaDolar);
				lbDeposito_S.Text = Convert.ToString(sta.SumaVentasDepositoDia);
				lbDeposito_D.Text = Convert.ToString(sta.SumaVentasDepositoDiaDolar);
				lbTarjeta_S.Text = Convert.ToString(sta.SumaVentasTarjetaDia);
				lbTarjeta_D.Text = Convert.ToString(sta.SumaVentasTarjetaDiaDolar);
				lbCheque_S.Text = Convert.ToString(sta.SumaVentasChequeDia);
				lbCheque_D.Text = Convert.ToString(sta.SumaVentasChequeDiaDolar);
				lbTransferencia_S.Text = Convert.ToString(sta.SumaVentasTransferenciaDia);
				lb_Transferencia_D.Text = Convert.ToString(sta.SumaVentasTransferenciaDiaDolar);
				lbTotalCaja.Text = Convert.ToString(sta.AperturaCaja + (sta.TotalVentas + sta.Ingresos + sta.CobranzaSolesTotal) - (sta.MontoDepositado + sta.Egresos));
				lbTotalCajaDolares.Text = Convert.ToString(sta.TotalVentasDolar + sta.IngresosDolar + sta.CobranzaDolaresTotal - sta.EgresosDolar);
				lbEfectivoMes_S.Text = Convert.ToString(sta.SumaVentasEfectivoMes);
				lbEfectivoMes_D.Text = Convert.ToString(sta.SumaVentasEfectivoMesDolar);
				lbDepositoMes_S.Text = Convert.ToString(sta.SumaVentasDepositoMes);
				lbDepositoMes_D.Text = Convert.ToString(sta.SumaVentasDepositoMesDolar);
				lbTarjetaMes_S.Text = Convert.ToString(sta.SumaVentasTarjetaMes);
				lbTarjetaMes_D.Text = Convert.ToString(sta.SumaVentasTarjetaMesDolar);
				lbChequeMes_S.Text = Convert.ToString(sta.SumaVentasChequeMes);
				lbChequeMes_D.Text = Convert.ToString(sta.SumaVentasChequeMesDolar);
				lbTransferenciaMes_S.Text = Convert.ToString(sta.SumaVentasTransferenciaMes);
				lbTransferenciaMes_D.Text = Convert.ToString(sta.SumaVentasTransferenciaMesDolar);
				TotalVentasMesSoles = sta.SumaVentasEfectivoMes + sta.SumaVentasDepositoMes + sta.SumaVentasTarjetaMes + sta.SumaVentasChequeMes + sta.SumaVentasTransferenciaMes;
				TotalVentasMesDolar = sta.SumaVentasEfectivoMesDolar + sta.SumaVentasDepositoMesDolar + sta.SumaVentasTarjetaMesDolar + sta.SumaVentasChequeMesDolar + sta.SumaVentasTransferenciaMesDolar;
				lbTotalVentasMes_S.Text = Convert.ToString(TotalVentasMesSoles);
				lbTotalVentasMes_D.Text = Convert.ToString(TotalVentasMesDolar);
				lbPorCobrarMes_S.Text = Convert.ToString(sta.PorCobrarMes);
				lbPorCobrarMes_D.Text = Convert.ToString(sta.PorCobrarMesDolar);
				lbTotalMes_S.Text = Convert.ToString(sta.TotalVentasMes + sta.PorCobrarMes);
				lbTotalMes_D.Text = Convert.ToString(sta.TotalVentasMesDolar + sta.PorCobrarMesDolar);
				lbTotalCobranzas_S.Text = Convert.ToString(sta.CobranzaSolesTotal);
				lbTotalCobranzas_D.Text = Convert.ToString(sta.CobranzaDolaresTotal);
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
			dtpfechaHasta.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
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
			dtpfechaHasta.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
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
			tcambio = admTipoCambio.CargaTipoCambio(dtpfechaDesde.Value, 2);
			flu.MontoCierre = Convert.ToDecimal(lbTotalCaja.Text);
			flu.CodAlmacen = frmLogin.iCodAlmacen;
			flu.CodSucursal = frmLogin.iCodSucursal;
			flu.FechaCierre = dtpfechaHasta.Value;
			flu.MontoDepositado = Convert.ToDecimal(lbMontoDepositado_S.Text);
			flu.MontoIngresado = Convert.ToDecimal(lbEfectivo_S.Text) + Convert.ToDecimal(lbEfectivo_D.Text) * Convert.ToDecimal(tcambio.Venta);
			flu.MontoDisponible = Convert.ToDecimal(lbAperturaCaja.Text) + (Convert.ToDecimal(lbEfectivo_S.Text) + Convert.ToDecimal(lbEfectivo_D.Text)) - Convert.ToDecimal(lbMontoDepositado_S.Text);
			flu.CodUser = frmLogin.iCodUser;
			flu.Deposito = depo;
			flu.FechaDeposito = fecha_Depo;
			if (admFlu.Update(flu))
			{
				VerificarCierreCaja();
			}
			btnImprimir.Visible = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void btnAnularCierre_Click(object sender, EventArgs e)
	{
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		CRLiquidacion rpt1 = new CRLiquidacion();
		frmRptLiquidacionCaja frm1 = new frmRptLiquidacionCaja();
		rpt1.SetDataSource(ds.ReportLiquidacion(frmLogin.iCodSucursal, dtpfechaDesde.Value).Tables[0]);
		frm1.cRVLiquidacionCaja.ReportSource = rpt1;
		frm1.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCaja));
		this.gbApertura = new System.Windows.Forms.GroupBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lbAperturaCaja = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.gbIngreso = new System.Windows.Forms.GroupBox();
		this.panel11 = new System.Windows.Forms.Panel();
		this.lbSubTotalIngresos_D = new System.Windows.Forms.Label();
		this.lbSubTotalIngresos_S = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.panel17 = new System.Windows.Forms.Panel();
		this.lbTotalCobranzas_D = new System.Windows.Forms.Label();
		this.lbTotalCobranzas_S = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.lbIngresos_D = new System.Windows.Forms.Label();
		this.lbIngresos_S = new System.Windows.Forms.Label();
		this.lbTotalVentas_D = new System.Windows.Forms.Label();
		this.lbTotalVentas_S = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label44 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.panel9 = new System.Windows.Forms.Panel();
		this.lbDeposito_D = new System.Windows.Forms.Label();
		this.lbDeposito_S = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.gbCierre = new System.Windows.Forms.GroupBox();
		this.panel6 = new System.Windows.Forms.Panel();
		this.lbTotalCaja = new System.Windows.Forms.Label();
		this.gbVentasCredito = new System.Windows.Forms.GroupBox();
		this.panel7 = new System.Windows.Forms.Panel();
		this.lbVentasCredito_D = new System.Windows.Forms.Label();
		this.lbTotalVentasCredito_S = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.gbComprasCredito = new System.Windows.Forms.GroupBox();
		this.panel8 = new System.Windows.Forms.Panel();
		this.lbComprasCredito_D = new System.Windows.Forms.Label();
		this.lbTotalComprasCredito_S = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.dtpfechaHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpfechaDesde = new System.Windows.Forms.DateTimePicker();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lbTarjeta_D = new System.Windows.Forms.Label();
		this.lbTarjeta_S = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.lbCheque_D = new System.Windows.Forms.Label();
		this.lbCheque_S = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.lb_Transferencia_D = new System.Windows.Forms.Label();
		this.lbTransferencia_S = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.panel10 = new System.Windows.Forms.Panel();
		this.lbEfectivo_D = new System.Windows.Forms.Label();
		this.lbEfectivo_S = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.btnAnularCierre = new System.Windows.Forms.Button();
		this.btnsalir = new System.Windows.Forms.Button();
		this.btnCerrarCaja = new System.Windows.Forms.Button();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.lbSubtotalEgresos_D = new System.Windows.Forms.Label();
		this.lbSubTotalEgresos_S = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.panel12 = new System.Windows.Forms.Panel();
		this.lbEgresos_D = new System.Windows.Forms.Label();
		this.lbEgresos_S = new System.Windows.Forms.Label();
		this.lbMontoDepositado_D = new System.Windows.Forms.Label();
		this.lbMontoDepositado_S = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.label65 = new System.Windows.Forms.Label();
		this.label66 = new System.Windows.Forms.Label();
		this.lbTotalMes_D = new System.Windows.Forms.Label();
		this.lbTotalMes_S = new System.Windows.Forms.Label();
		this.label64 = new System.Windows.Forms.Label();
		this.lbPorCobrarMes_D = new System.Windows.Forms.Label();
		this.lbPorCobrarMes_S = new System.Windows.Forms.Label();
		this.label41 = new System.Windows.Forms.Label();
		this.lbTotalVentasMes_D = new System.Windows.Forms.Label();
		this.lbTotalVentasMes_S = new System.Windows.Forms.Label();
		this.label61 = new System.Windows.Forms.Label();
		this.lbTransferenciaMes_D = new System.Windows.Forms.Label();
		this.lbTransferenciaMes_S = new System.Windows.Forms.Label();
		this.label55 = new System.Windows.Forms.Label();
		this.lbChequeMes_D = new System.Windows.Forms.Label();
		this.lbChequeMes_S = new System.Windows.Forms.Label();
		this.label52 = new System.Windows.Forms.Label();
		this.lbTarjetaMes_D = new System.Windows.Forms.Label();
		this.lbTarjetaMes_S = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.lbDepositoMes_D = new System.Windows.Forms.Label();
		this.lbDepositoMes_S = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.lbEfectivoMes_D = new System.Windows.Forms.Label();
		this.lbEfectivoMes_S = new System.Windows.Forms.Label();
		this.label58 = new System.Windows.Forms.Label();
		this.lbTotalCajaDolares = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.gbApertura.SuspendLayout();
		this.panel1.SuspendLayout();
		this.gbIngreso.SuspendLayout();
		this.panel11.SuspendLayout();
		this.panel17.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.panel9.SuspendLayout();
		this.gbCierre.SuspendLayout();
		this.panel6.SuspendLayout();
		this.gbVentasCredito.SuspendLayout();
		this.panel7.SuspendLayout();
		this.gbComprasCredito.SuspendLayout();
		this.panel8.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.panel3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.panel5.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.panel10.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel12.SuspendLayout();
		this.groupBox7.SuspendLayout();
		base.SuspendLayout();
		this.gbApertura.Controls.Add(this.panel1);
		this.gbApertura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.gbApertura.Location = new System.Drawing.Point(1, 44);
		this.gbApertura.Name = "gbApertura";
		this.gbApertura.Size = new System.Drawing.Size(371, 60);
		this.gbApertura.TabIndex = 0;
		this.gbApertura.TabStop = false;
		this.gbApertura.Text = "APERTURA DE CAJA";
		this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel1.Controls.Add(this.lbAperturaCaja);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Location = new System.Drawing.Point(7, 19);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(358, 35);
		this.panel1.TabIndex = 0;
		this.lbAperturaCaja.AutoSize = true;
		this.lbAperturaCaja.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbAperturaCaja.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbAperturaCaja.Location = new System.Drawing.Point(130, 6);
		this.lbAperturaCaja.Name = "lbAperturaCaja";
		this.lbAperturaCaja.Size = new System.Drawing.Size(104, 23);
		this.lbAperturaCaja.TabIndex = 1;
		this.lbAperturaCaja.Text = "00000000.00";
		this.lbAperturaCaja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.label1.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(10, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(85, 20);
		this.label1.TabIndex = 0;
		this.label1.Text = "MONTO:  S/.";
		this.gbIngreso.Controls.Add(this.panel11);
		this.gbIngreso.Controls.Add(this.panel17);
		this.gbIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.gbIngreso.Location = new System.Drawing.Point(10, 137);
		this.gbIngreso.Name = "gbIngreso";
		this.gbIngreso.Size = new System.Drawing.Size(370, 173);
		this.gbIngreso.TabIndex = 1;
		this.gbIngreso.TabStop = false;
		this.gbIngreso.Text = "INGRESOS";
		this.panel11.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel11.Controls.Add(this.lbSubTotalIngresos_D);
		this.panel11.Controls.Add(this.lbSubTotalIngresos_S);
		this.panel11.Controls.Add(this.label11);
		this.panel11.Location = new System.Drawing.Point(7, 124);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(358, 31);
		this.panel11.TabIndex = 1;
		this.lbSubTotalIngresos_D.AutoSize = true;
		this.lbSubTotalIngresos_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbSubTotalIngresos_D.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbSubTotalIngresos_D.Location = new System.Drawing.Point(277, 2);
		this.lbSubTotalIngresos_D.Name = "lbSubTotalIngresos_D";
		this.lbSubTotalIngresos_D.Size = new System.Drawing.Size(68, 23);
		this.lbSubTotalIngresos_D.TabIndex = 2;
		this.lbSubTotalIngresos_D.Text = "0000.00";
		this.lbSubTotalIngresos_S.AutoSize = true;
		this.lbSubTotalIngresos_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbSubTotalIngresos_S.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbSubTotalIngresos_S.Location = new System.Drawing.Point(175, 2);
		this.lbSubTotalIngresos_S.Name = "lbSubTotalIngresos_S";
		this.lbSubTotalIngresos_S.Size = new System.Drawing.Size(68, 23);
		this.lbSubTotalIngresos_S.TabIndex = 1;
		this.lbSubTotalIngresos_S.Text = "0000.00";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(55, 5);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(87, 20);
		this.label11.TabIndex = 0;
		this.label11.Text = "SUBTOTAL: ";
		this.panel17.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel17.Controls.Add(this.lbTotalCobranzas_D);
		this.panel17.Controls.Add(this.lbTotalCobranzas_S);
		this.panel17.Controls.Add(this.label5);
		this.panel17.Controls.Add(this.lbIngresos_D);
		this.panel17.Controls.Add(this.lbIngresos_S);
		this.panel17.Controls.Add(this.lbTotalVentas_D);
		this.panel17.Controls.Add(this.lbTotalVentas_S);
		this.panel17.Controls.Add(this.label13);
		this.panel17.Controls.Add(this.label44);
		this.panel17.Location = new System.Drawing.Point(7, 19);
		this.panel17.Name = "panel17";
		this.panel17.Size = new System.Drawing.Size(357, 85);
		this.panel17.TabIndex = 0;
		this.lbTotalCobranzas_D.AutoSize = true;
		this.lbTotalCobranzas_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalCobranzas_D.Location = new System.Drawing.Point(272, 31);
		this.lbTotalCobranzas_D.Name = "lbTotalCobranzas_D";
		this.lbTotalCobranzas_D.Size = new System.Drawing.Size(68, 23);
		this.lbTotalCobranzas_D.TabIndex = 10;
		this.lbTotalCobranzas_D.Text = "0000.00";
		this.lbTotalCobranzas_S.AutoSize = true;
		this.lbTotalCobranzas_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalCobranzas_S.Location = new System.Drawing.Point(170, 31);
		this.lbTotalCobranzas_S.Name = "lbTotalCobranzas_S";
		this.lbTotalCobranzas_S.Size = new System.Drawing.Size(68, 23);
		this.lbTotalCobranzas_S.TabIndex = 9;
		this.lbTotalCobranzas_S.Text = "0000.00";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(11, 34);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(159, 20);
		this.label5.TabIndex = 8;
		this.label5.Text = "TOTAL DE COBRANZAS:";
		this.lbIngresos_D.AutoSize = true;
		this.lbIngresos_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbIngresos_D.Location = new System.Drawing.Point(272, 53);
		this.lbIngresos_D.Name = "lbIngresos_D";
		this.lbIngresos_D.Size = new System.Drawing.Size(68, 23);
		this.lbIngresos_D.TabIndex = 7;
		this.lbIngresos_D.Text = "0000.00";
		this.lbIngresos_S.AutoSize = true;
		this.lbIngresos_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbIngresos_S.Location = new System.Drawing.Point(170, 53);
		this.lbIngresos_S.Name = "lbIngresos_S";
		this.lbIngresos_S.Size = new System.Drawing.Size(68, 23);
		this.lbIngresos_S.TabIndex = 6;
		this.lbIngresos_S.Text = "0000.00";
		this.lbTotalVentas_D.AutoSize = true;
		this.lbTotalVentas_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalVentas_D.Location = new System.Drawing.Point(275, 2);
		this.lbTotalVentas_D.Name = "lbTotalVentas_D";
		this.lbTotalVentas_D.Size = new System.Drawing.Size(68, 23);
		this.lbTotalVentas_D.TabIndex = 5;
		this.lbTotalVentas_D.Text = "0000.00";
		this.lbTotalVentas_S.AutoSize = true;
		this.lbTotalVentas_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalVentas_S.Location = new System.Drawing.Point(173, 2);
		this.lbTotalVentas_S.Name = "lbTotalVentas_S";
		this.lbTotalVentas_S.Size = new System.Drawing.Size(68, 23);
		this.lbTotalVentas_S.TabIndex = 4;
		this.lbTotalVentas_S.Text = "0000.00";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(55, 56);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(85, 20);
		this.label13.TabIndex = 3;
		this.label13.Text = "INGRESOS: ";
		this.label44.AutoSize = true;
		this.label44.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label44.Location = new System.Drawing.Point(8, 5);
		this.label44.Name = "label44";
		this.label44.Size = new System.Drawing.Size(135, 20);
		this.label44.TabIndex = 0;
		this.label44.Text = "TOTAL DE VENTAS: ";
		this.groupBox2.Controls.Add(this.panel9);
		this.groupBox2.Location = new System.Drawing.Point(391, 95);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(371, 54);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "VENTAS / COBRANZA POR DEPOSITO";
		this.panel9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel9.Controls.Add(this.lbDeposito_D);
		this.panel9.Controls.Add(this.lbDeposito_S);
		this.panel9.Controls.Add(this.label10);
		this.panel9.Location = new System.Drawing.Point(11, 19);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(354, 29);
		this.panel9.TabIndex = 1;
		this.lbDeposito_D.AutoSize = true;
		this.lbDeposito_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDeposito_D.Location = new System.Drawing.Point(270, 3);
		this.lbDeposito_D.Name = "lbDeposito_D";
		this.lbDeposito_D.Size = new System.Drawing.Size(68, 23);
		this.lbDeposito_D.TabIndex = 3;
		this.lbDeposito_D.Text = "0000.00";
		this.lbDeposito_S.AutoSize = true;
		this.lbDeposito_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDeposito_S.Location = new System.Drawing.Point(164, 3);
		this.lbDeposito_S.Name = "lbDeposito_S";
		this.lbDeposito_S.Size = new System.Drawing.Size(68, 23);
		this.lbDeposito_S.TabIndex = 2;
		this.lbDeposito_S.Text = "0000.00";
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(12, 6);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(53, 20);
		this.label10.TabIndex = 0;
		this.label10.Text = "TOTAL:";
		this.gbCierre.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.gbCierre.Controls.Add(this.panel6);
		this.gbCierre.Location = new System.Drawing.Point(8, 432);
		this.gbCierre.Name = "gbCierre";
		this.gbCierre.Size = new System.Drawing.Size(372, 57);
		this.gbCierre.TabIndex = 2;
		this.gbCierre.TabStop = false;
		this.gbCierre.Text = "CIERRE DE CAJA";
		this.panel6.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel6.Controls.Add(this.lbTotalCajaDolares);
		this.panel6.Controls.Add(this.lbTotalCaja);
		this.panel6.Controls.Add(this.label2);
		this.panel6.Location = new System.Drawing.Point(7, 20);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(357, 31);
		this.panel6.TabIndex = 0;
		this.lbTotalCaja.AutoSize = true;
		this.lbTotalCaja.Font = new System.Drawing.Font("Arial Narrow", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalCaja.ForeColor = System.Drawing.Color.DarkSlateBlue;
		this.lbTotalCaja.Location = new System.Drawing.Point(135, 6);
		this.lbTotalCaja.Name = "lbTotalCaja";
		this.lbTotalCaja.Size = new System.Drawing.Size(105, 24);
		this.lbTotalCaja.TabIndex = 1;
		this.lbTotalCaja.Text = "00000000.00";
		this.gbVentasCredito.Controls.Add(this.panel7);
		this.gbVentasCredito.Location = new System.Drawing.Point(2, 509);
		this.gbVentasCredito.Name = "gbVentasCredito";
		this.gbVentasCredito.Size = new System.Drawing.Size(373, 41);
		this.gbVentasCredito.TabIndex = 1;
		this.gbVentasCredito.TabStop = false;
		this.gbVentasCredito.Text = "VENTAS A CREDITO";
		this.panel7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel7.Controls.Add(this.lbVentasCredito_D);
		this.panel7.Controls.Add(this.lbTotalVentasCredito_S);
		this.panel7.Controls.Add(this.label14);
		this.panel7.Location = new System.Drawing.Point(6, 12);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(359, 25);
		this.panel7.TabIndex = 0;
		this.lbVentasCredito_D.AutoSize = true;
		this.lbVentasCredito_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbVentasCredito_D.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbVentasCredito_D.Location = new System.Drawing.Point(277, 1);
		this.lbVentasCredito_D.Name = "lbVentasCredito_D";
		this.lbVentasCredito_D.Size = new System.Drawing.Size(68, 23);
		this.lbVentasCredito_D.TabIndex = 2;
		this.lbVentasCredito_D.Text = "0000.00";
		this.lbTotalVentasCredito_S.AutoSize = true;
		this.lbTotalVentasCredito_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalVentasCredito_S.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbTotalVentasCredito_S.Location = new System.Drawing.Point(172, 1);
		this.lbTotalVentasCredito_S.Name = "lbTotalVentasCredito_S";
		this.lbTotalVentasCredito_S.Size = new System.Drawing.Size(68, 23);
		this.lbTotalVentasCredito_S.TabIndex = 1;
		this.lbTotalVentasCredito_S.Text = "0000.00";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(68, 4);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(61, 20);
		this.label14.TabIndex = 0;
		this.label14.Text = "TOTAL:  ";
		this.gbComprasCredito.Controls.Add(this.panel8);
		this.gbComprasCredito.Location = new System.Drawing.Point(2, 509);
		this.gbComprasCredito.Name = "gbComprasCredito";
		this.gbComprasCredito.Size = new System.Drawing.Size(373, 41);
		this.gbComprasCredito.TabIndex = 3;
		this.gbComprasCredito.TabStop = false;
		this.gbComprasCredito.Text = "COMPRAS A CREDITO";
		this.gbComprasCredito.Visible = false;
		this.panel8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel8.Controls.Add(this.lbComprasCredito_D);
		this.panel8.Controls.Add(this.lbTotalComprasCredito_S);
		this.panel8.Controls.Add(this.label16);
		this.panel8.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.panel8.Location = new System.Drawing.Point(7, 20);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(357, 35);
		this.panel8.TabIndex = 0;
		this.panel8.Visible = false;
		this.lbComprasCredito_D.AutoSize = true;
		this.lbComprasCredito_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbComprasCredito_D.ForeColor = System.Drawing.Color.Red;
		this.lbComprasCredito_D.Location = new System.Drawing.Point(275, 4);
		this.lbComprasCredito_D.Name = "lbComprasCredito_D";
		this.lbComprasCredito_D.Size = new System.Drawing.Size(68, 23);
		this.lbComprasCredito_D.TabIndex = 2;
		this.lbComprasCredito_D.Text = "0000.00";
		this.lbTotalComprasCredito_S.AutoSize = true;
		this.lbTotalComprasCredito_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalComprasCredito_S.ForeColor = System.Drawing.Color.Red;
		this.lbTotalComprasCredito_S.Location = new System.Drawing.Point(169, 4);
		this.lbTotalComprasCredito_S.Name = "lbTotalComprasCredito_S";
		this.lbTotalComprasCredito_S.Size = new System.Drawing.Size(68, 23);
		this.lbTotalComprasCredito_S.TabIndex = 1;
		this.lbTotalComprasCredito_S.Text = "0000.00";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(66, 7);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(61, 20);
		this.label16.TabIndex = 0;
		this.label16.Text = "TOTAL:  ";
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
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(166, 17);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(14, 13);
		this.label22.TabIndex = 29;
		this.label22.Text = "Y";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(12, 17);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(47, 13);
		this.label21.TabIndex = 28;
		this.label21.Text = "ENTRE:";
		this.dtpfechaHasta.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
		this.dtpfechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfechaHasta.Location = new System.Drawing.Point(186, 12);
		this.dtpfechaHasta.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
		this.dtpfechaHasta.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtpfechaHasta.Name = "dtpfechaHasta";
		this.dtpfechaHasta.Size = new System.Drawing.Size(100, 21);
		this.dtpfechaHasta.TabIndex = 27;
		this.dtpfechaHasta.ValueChanged += new System.EventHandler(dtpfechaHasta_ValueChanged);
		this.dtpfechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpfechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfechaDesde.Location = new System.Drawing.Point(61, 12);
		this.dtpfechaDesde.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
		this.dtpfechaDesde.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtpfechaDesde.Name = "dtpfechaDesde";
		this.dtpfechaDesde.Size = new System.Drawing.Size(100, 21);
		this.dtpfechaDesde.TabIndex = 26;
		this.dtpfechaDesde.ValueChanged += new System.EventHandler(dtpfechaDesde_ValueChanged);
		this.groupBox3.Controls.Add(this.panel3);
		this.groupBox3.Location = new System.Drawing.Point(392, 155);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(370, 51);
		this.groupBox3.TabIndex = 34;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "VENTAS / COBRANZA POR TARJETA";
		this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel3.Controls.Add(this.lbTarjeta_D);
		this.panel3.Controls.Add(this.lbTarjeta_S);
		this.panel3.Controls.Add(this.label9);
		this.panel3.Location = new System.Drawing.Point(11, 19);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(353, 29);
		this.panel3.TabIndex = 1;
		this.lbTarjeta_D.AutoSize = true;
		this.lbTarjeta_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTarjeta_D.Location = new System.Drawing.Point(270, 3);
		this.lbTarjeta_D.Name = "lbTarjeta_D";
		this.lbTarjeta_D.Size = new System.Drawing.Size(68, 23);
		this.lbTarjeta_D.TabIndex = 3;
		this.lbTarjeta_D.Text = "0000.00";
		this.lbTarjeta_S.AutoSize = true;
		this.lbTarjeta_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTarjeta_S.Location = new System.Drawing.Point(164, 3);
		this.lbTarjeta_S.Name = "lbTarjeta_S";
		this.lbTarjeta_S.Size = new System.Drawing.Size(68, 23);
		this.lbTarjeta_S.TabIndex = 2;
		this.lbTarjeta_S.Text = "0000.00";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(12, 6);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 20);
		this.label9.TabIndex = 0;
		this.label9.Text = "TOTAL:";
		this.groupBox1.Controls.Add(this.panel2);
		this.groupBox1.Location = new System.Drawing.Point(394, 212);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(368, 52);
		this.groupBox1.TabIndex = 35;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "VENTAS / COBRANZA POR CHEQUE";
		this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel2.Controls.Add(this.lbCheque_D);
		this.panel2.Controls.Add(this.lbCheque_S);
		this.panel2.Controls.Add(this.label15);
		this.panel2.Location = new System.Drawing.Point(11, 19);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(353, 29);
		this.panel2.TabIndex = 1;
		this.lbCheque_D.AutoSize = true;
		this.lbCheque_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbCheque_D.Location = new System.Drawing.Point(270, 3);
		this.lbCheque_D.Name = "lbCheque_D";
		this.lbCheque_D.Size = new System.Drawing.Size(68, 23);
		this.lbCheque_D.TabIndex = 3;
		this.lbCheque_D.Text = "0000.00";
		this.lbCheque_S.AutoSize = true;
		this.lbCheque_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbCheque_S.Location = new System.Drawing.Point(164, 3);
		this.lbCheque_S.Name = "lbCheque_S";
		this.lbCheque_S.Size = new System.Drawing.Size(68, 23);
		this.lbCheque_S.TabIndex = 2;
		this.lbCheque_S.Text = "0000.00";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(12, 6);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(53, 20);
		this.label15.TabIndex = 0;
		this.label15.Text = "TOTAL:";
		this.groupBox4.Controls.Add(this.panel5);
		this.groupBox4.Location = new System.Drawing.Point(394, 277);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(368, 54);
		this.groupBox4.TabIndex = 36;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "VENTAS / COBRANZA POR TRANSFERENCIA";
		this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel5.Controls.Add(this.lb_Transferencia_D);
		this.panel5.Controls.Add(this.lbTransferencia_S);
		this.panel5.Controls.Add(this.label20);
		this.panel5.Location = new System.Drawing.Point(11, 19);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(353, 29);
		this.panel5.TabIndex = 1;
		this.lb_Transferencia_D.AutoSize = true;
		this.lb_Transferencia_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lb_Transferencia_D.Location = new System.Drawing.Point(270, 3);
		this.lb_Transferencia_D.Name = "lb_Transferencia_D";
		this.lb_Transferencia_D.Size = new System.Drawing.Size(68, 23);
		this.lb_Transferencia_D.TabIndex = 3;
		this.lb_Transferencia_D.Text = "0000.00";
		this.lbTransferencia_S.AutoSize = true;
		this.lbTransferencia_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTransferencia_S.Location = new System.Drawing.Point(164, 3);
		this.lbTransferencia_S.Name = "lbTransferencia_S";
		this.lbTransferencia_S.Size = new System.Drawing.Size(68, 23);
		this.lbTransferencia_S.TabIndex = 2;
		this.lbTransferencia_S.Text = "0000.00";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.Location = new System.Drawing.Point(12, 6);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(53, 20);
		this.label20.TabIndex = 0;
		this.label20.Text = "TOTAL:";
		this.groupBox5.Controls.Add(this.panel10);
		this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox5.Location = new System.Drawing.Point(391, 33);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(371, 56);
		this.groupBox5.TabIndex = 37;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "VENTAS/ COBRANZA EN EFECTIVO";
		this.panel10.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel10.Controls.Add(this.lbEfectivo_D);
		this.panel10.Controls.Add(this.lbEfectivo_S);
		this.panel10.Controls.Add(this.label28);
		this.panel10.Location = new System.Drawing.Point(11, 19);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(354, 29);
		this.panel10.TabIndex = 1;
		this.lbEfectivo_D.AutoSize = true;
		this.lbEfectivo_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbEfectivo_D.Location = new System.Drawing.Point(270, 3);
		this.lbEfectivo_D.Name = "lbEfectivo_D";
		this.lbEfectivo_D.Size = new System.Drawing.Size(68, 23);
		this.lbEfectivo_D.TabIndex = 3;
		this.lbEfectivo_D.Text = "0000.00";
		this.lbEfectivo_S.AutoSize = true;
		this.lbEfectivo_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbEfectivo_S.Location = new System.Drawing.Point(164, 3);
		this.lbEfectivo_S.Name = "lbEfectivo_S";
		this.lbEfectivo_S.Size = new System.Drawing.Size(68, 23);
		this.lbEfectivo_S.TabIndex = 2;
		this.lbEfectivo_S.Text = "0000.00";
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label28.Location = new System.Drawing.Point(12, 6);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(53, 20);
		this.label28.TabIndex = 0;
		this.label28.Text = "TOTAL:";
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label27.Location = new System.Drawing.Point(697, 10);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(20, 20);
		this.label27.TabIndex = 40;
		this.label27.Text = "$.";
		this.label47.AutoSize = true;
		this.label47.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label47.Location = new System.Drawing.Point(586, 10);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(30, 20);
		this.label47.TabIndex = 39;
		this.label47.Text = " S/.";
		this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(245, 556);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(75, 32);
		this.btnImprimir.TabIndex = 31;
		this.btnImprimir.Text = "Imprimir";
		this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.button1.ImageIndex = 7;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(294, 12);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(27, 21);
		this.button1.TabIndex = 30;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnAnularCierre.ImageIndex = 7;
		this.btnAnularCierre.ImageList = this.imageList1;
		this.btnAnularCierre.Location = new System.Drawing.Point(326, 556);
		this.btnAnularCierre.Name = "btnAnularCierre";
		this.btnAnularCierre.Size = new System.Drawing.Size(101, 32);
		this.btnAnularCierre.TabIndex = 25;
		this.btnAnularCierre.Text = "Anular Cierre";
		this.btnAnularCierre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnularCierre.UseVisualStyleBackColor = true;
		this.btnAnularCierre.Click += new System.EventHandler(btnAnularCierre_Click);
		this.btnsalir.ImageIndex = 5;
		this.btnsalir.ImageList = this.imageList1;
		this.btnsalir.Location = new System.Drawing.Point(536, 556);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(64, 32);
		this.btnsalir.TabIndex = 24;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnsalir.UseVisualStyleBackColor = true;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.btnCerrarCaja.ImageIndex = 8;
		this.btnCerrarCaja.ImageList = this.imageList1;
		this.btnCerrarCaja.Location = new System.Drawing.Point(433, 556);
		this.btnCerrarCaja.Name = "btnCerrarCaja";
		this.btnCerrarCaja.Size = new System.Drawing.Size(97, 32);
		this.btnCerrarCaja.TabIndex = 23;
		this.btnCerrarCaja.Text = "Cerrar Caja";
		this.btnCerrarCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCerrarCaja.UseVisualStyleBackColor = true;
		this.btnCerrarCaja.Click += new System.EventHandler(btnCerrarCaja_Click);
		this.groupBox6.Controls.Add(this.panel4);
		this.groupBox6.Controls.Add(this.panel12);
		this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox6.Location = new System.Drawing.Point(10, 316);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(372, 114);
		this.groupBox6.TabIndex = 41;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "EGRESOS";
		this.panel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel4.Controls.Add(this.lbSubtotalEgresos_D);
		this.panel4.Controls.Add(this.lbSubTotalEgresos_S);
		this.panel4.Controls.Add(this.label26);
		this.panel4.Location = new System.Drawing.Point(7, 77);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(357, 31);
		this.panel4.TabIndex = 1;
		this.lbSubtotalEgresos_D.AutoSize = true;
		this.lbSubtotalEgresos_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbSubtotalEgresos_D.ForeColor = System.Drawing.Color.Red;
		this.lbSubtotalEgresos_D.Location = new System.Drawing.Point(281, 5);
		this.lbSubtotalEgresos_D.Name = "lbSubtotalEgresos_D";
		this.lbSubtotalEgresos_D.Size = new System.Drawing.Size(68, 23);
		this.lbSubtotalEgresos_D.TabIndex = 2;
		this.lbSubtotalEgresos_D.Text = "0000.00";
		this.lbSubTotalEgresos_S.AutoSize = true;
		this.lbSubTotalEgresos_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbSubTotalEgresos_S.ForeColor = System.Drawing.Color.Red;
		this.lbSubTotalEgresos_S.Location = new System.Drawing.Point(175, 5);
		this.lbSubTotalEgresos_S.Name = "lbSubTotalEgresos_S";
		this.lbSubTotalEgresos_S.Size = new System.Drawing.Size(68, 23);
		this.lbSubTotalEgresos_S.TabIndex = 1;
		this.lbSubTotalEgresos_S.Text = "0000.00";
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label26.Location = new System.Drawing.Point(55, 5);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(87, 20);
		this.label26.TabIndex = 0;
		this.label26.Text = "SUBTOTAL: ";
		this.panel12.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel12.Controls.Add(this.lbEgresos_D);
		this.panel12.Controls.Add(this.lbEgresos_S);
		this.panel12.Controls.Add(this.lbMontoDepositado_D);
		this.panel12.Controls.Add(this.lbMontoDepositado_S);
		this.panel12.Controls.Add(this.label31);
		this.panel12.Controls.Add(this.label32);
		this.panel12.Location = new System.Drawing.Point(9, 17);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(357, 54);
		this.panel12.TabIndex = 0;
		this.lbEgresos_D.AutoSize = true;
		this.lbEgresos_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbEgresos_D.Location = new System.Drawing.Point(279, 22);
		this.lbEgresos_D.Name = "lbEgresos_D";
		this.lbEgresos_D.Size = new System.Drawing.Size(68, 23);
		this.lbEgresos_D.TabIndex = 7;
		this.lbEgresos_D.Text = "0000.00";
		this.lbEgresos_S.AutoSize = true;
		this.lbEgresos_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbEgresos_S.Location = new System.Drawing.Point(173, 22);
		this.lbEgresos_S.Name = "lbEgresos_S";
		this.lbEgresos_S.Size = new System.Drawing.Size(68, 23);
		this.lbEgresos_S.TabIndex = 6;
		this.lbEgresos_S.Text = "0000.00";
		this.lbMontoDepositado_D.AutoSize = true;
		this.lbMontoDepositado_D.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbMontoDepositado_D.Location = new System.Drawing.Point(279, 3);
		this.lbMontoDepositado_D.Name = "lbMontoDepositado_D";
		this.lbMontoDepositado_D.Size = new System.Drawing.Size(68, 23);
		this.lbMontoDepositado_D.TabIndex = 5;
		this.lbMontoDepositado_D.Text = "0000.00";
		this.lbMontoDepositado_S.AutoSize = true;
		this.lbMontoDepositado_S.Font = new System.Drawing.Font("Arial Narrow", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbMontoDepositado_S.Location = new System.Drawing.Point(173, 3);
		this.lbMontoDepositado_S.Name = "lbMontoDepositado_S";
		this.lbMontoDepositado_S.Size = new System.Drawing.Size(68, 23);
		this.lbMontoDepositado_S.TabIndex = 4;
		this.lbMontoDepositado_S.Text = "0000.00";
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label31.Location = new System.Drawing.Point(61, 26);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(82, 20);
		this.label31.TabIndex = 3;
		this.label31.Text = "EGRESOS: ";
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label32.Location = new System.Drawing.Point(8, 6);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(134, 20);
		this.label32.TabIndex = 0;
		this.label32.Text = "MONTO DEPOSITO: ";
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(312, 118);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(20, 20);
		this.label37.TabIndex = 43;
		this.label37.Text = "$.";
		this.label38.AutoSize = true;
		this.label38.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label38.Location = new System.Drawing.Point(201, 118);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(30, 20);
		this.label38.TabIndex = 42;
		this.label38.Text = " S/.";
		this.groupBox7.Controls.Add(this.label65);
		this.groupBox7.Controls.Add(this.label66);
		this.groupBox7.Controls.Add(this.lbTotalMes_D);
		this.groupBox7.Controls.Add(this.lbTotalMes_S);
		this.groupBox7.Controls.Add(this.label64);
		this.groupBox7.Controls.Add(this.lbPorCobrarMes_D);
		this.groupBox7.Controls.Add(this.lbPorCobrarMes_S);
		this.groupBox7.Controls.Add(this.label41);
		this.groupBox7.Controls.Add(this.lbTotalVentasMes_D);
		this.groupBox7.Controls.Add(this.lbTotalVentasMes_S);
		this.groupBox7.Controls.Add(this.label61);
		this.groupBox7.Controls.Add(this.lbTransferenciaMes_D);
		this.groupBox7.Controls.Add(this.lbTransferenciaMes_S);
		this.groupBox7.Controls.Add(this.label55);
		this.groupBox7.Controls.Add(this.lbChequeMes_D);
		this.groupBox7.Controls.Add(this.lbChequeMes_S);
		this.groupBox7.Controls.Add(this.label52);
		this.groupBox7.Controls.Add(this.lbTarjetaMes_D);
		this.groupBox7.Controls.Add(this.lbTarjetaMes_S);
		this.groupBox7.Controls.Add(this.label49);
		this.groupBox7.Controls.Add(this.lbDepositoMes_D);
		this.groupBox7.Controls.Add(this.lbDepositoMes_S);
		this.groupBox7.Controls.Add(this.label45);
		this.groupBox7.Controls.Add(this.lbEfectivoMes_D);
		this.groupBox7.Controls.Add(this.lbEfectivoMes_S);
		this.groupBox7.Controls.Add(this.label58);
		this.groupBox7.Location = new System.Drawing.Point(394, 341);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(368, 209);
		this.groupBox7.TabIndex = 44;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "Ventas Mensuales";
		this.label65.AutoSize = true;
		this.label65.Font = new System.Drawing.Font("Arial Narrow", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label65.Location = new System.Drawing.Point(279, 16);
		this.label65.Name = "label65";
		this.label65.Size = new System.Drawing.Size(16, 16);
		this.label65.TabIndex = 56;
		this.label65.Text = "$.";
		this.label66.AutoSize = true;
		this.label66.Font = new System.Drawing.Font("Arial Narrow", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label66.Location = new System.Drawing.Point(159, 16);
		this.label66.Name = "label66";
		this.label66.Size = new System.Drawing.Size(24, 16);
		this.label66.TabIndex = 55;
		this.label66.Text = " S/.";
		this.lbTotalMes_D.AutoSize = true;
		this.lbTotalMes_D.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalMes_D.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbTotalMes_D.Location = new System.Drawing.Point(253, 190);
		this.lbTotalMes_D.Name = "lbTotalMes_D";
		this.lbTotalMes_D.Size = new System.Drawing.Size(60, 13);
		this.lbTotalMes_D.TabIndex = 54;
		this.lbTotalMes_D.Text = "00000.00";
		this.lbTotalMes_D.Visible = false;
		this.lbTotalMes_S.AutoSize = true;
		this.lbTotalMes_S.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalMes_S.ForeColor = System.Drawing.Color.LightSeaGreen;
		this.lbTotalMes_S.Location = new System.Drawing.Point(141, 190);
		this.lbTotalMes_S.Name = "lbTotalMes_S";
		this.lbTotalMes_S.Size = new System.Drawing.Size(60, 13);
		this.lbTotalMes_S.TabIndex = 53;
		this.lbTotalMes_S.Text = "00000.00";
		this.lbTotalMes_S.Visible = false;
		this.label64.AutoSize = true;
		this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label64.Location = new System.Drawing.Point(86, 190);
		this.label64.Name = "label64";
		this.label64.Size = new System.Drawing.Size(47, 13);
		this.label64.TabIndex = 52;
		this.label64.Text = "TOTAL";
		this.label64.Visible = false;
		this.lbPorCobrarMes_D.AutoSize = true;
		this.lbPorCobrarMes_D.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbPorCobrarMes_D.Location = new System.Drawing.Point(252, 168);
		this.lbPorCobrarMes_D.Name = "lbPorCobrarMes_D";
		this.lbPorCobrarMes_D.Size = new System.Drawing.Size(60, 13);
		this.lbPorCobrarMes_D.TabIndex = 51;
		this.lbPorCobrarMes_D.Text = "00000.00";
		this.lbPorCobrarMes_S.AutoSize = true;
		this.lbPorCobrarMes_S.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbPorCobrarMes_S.Location = new System.Drawing.Point(140, 168);
		this.lbPorCobrarMes_S.Name = "lbPorCobrarMes_S";
		this.lbPorCobrarMes_S.Size = new System.Drawing.Size(60, 13);
		this.lbPorCobrarMes_S.TabIndex = 50;
		this.lbPorCobrarMes_S.Text = "00000.00";
		this.label41.AutoSize = true;
		this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label41.Location = new System.Drawing.Point(11, 168);
		this.label41.Name = "label41";
		this.label41.Size = new System.Drawing.Size(122, 13);
		this.label41.TabIndex = 49;
		this.label41.Text = "TOTAL V. CREDITO";
		this.lbTotalVentasMes_D.AutoSize = true;
		this.lbTotalVentasMes_D.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalVentasMes_D.Location = new System.Drawing.Point(252, 148);
		this.lbTotalVentasMes_D.Name = "lbTotalVentasMes_D";
		this.lbTotalVentasMes_D.Size = new System.Drawing.Size(60, 13);
		this.lbTotalVentasMes_D.TabIndex = 48;
		this.lbTotalVentasMes_D.Text = "00000.00";
		this.lbTotalVentasMes_S.AutoSize = true;
		this.lbTotalVentasMes_S.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalVentasMes_S.Location = new System.Drawing.Point(140, 148);
		this.lbTotalVentasMes_S.Name = "lbTotalVentasMes_S";
		this.lbTotalVentasMes_S.Size = new System.Drawing.Size(60, 13);
		this.lbTotalVentasMes_S.TabIndex = 47;
		this.lbTotalVentasMes_S.Text = "00000.00";
		this.label61.AutoSize = true;
		this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label61.Location = new System.Drawing.Point(6, 148);
		this.label61.Name = "label61";
		this.label61.Size = new System.Drawing.Size(127, 13);
		this.label61.TabIndex = 46;
		this.label61.Text = "TOTAL V. CONTADO";
		this.lbTransferenciaMes_D.AutoSize = true;
		this.lbTransferenciaMes_D.Location = new System.Drawing.Point(260, 117);
		this.lbTransferenciaMes_D.Name = "lbTransferenciaMes_D";
		this.lbTransferenciaMes_D.Size = new System.Drawing.Size(52, 13);
		this.lbTransferenciaMes_D.TabIndex = 45;
		this.lbTransferenciaMes_D.Text = "00000.00";
		this.lbTransferenciaMes_S.AutoSize = true;
		this.lbTransferenciaMes_S.Location = new System.Drawing.Point(147, 117);
		this.lbTransferenciaMes_S.Name = "lbTransferenciaMes_S";
		this.lbTransferenciaMes_S.Size = new System.Drawing.Size(52, 13);
		this.lbTransferenciaMes_S.TabIndex = 44;
		this.lbTransferenciaMes_S.Text = "00000.00";
		this.label55.AutoSize = true;
		this.label55.Location = new System.Drawing.Point(7, 117);
		this.label55.Name = "label55";
		this.label55.Size = new System.Drawing.Size(97, 13);
		this.label55.TabIndex = 43;
		this.label55.Text = "TRANSFERENCIA";
		this.lbChequeMes_D.AutoSize = true;
		this.lbChequeMes_D.Location = new System.Drawing.Point(260, 101);
		this.lbChequeMes_D.Name = "lbChequeMes_D";
		this.lbChequeMes_D.Size = new System.Drawing.Size(52, 13);
		this.lbChequeMes_D.TabIndex = 42;
		this.lbChequeMes_D.Text = "00000.00";
		this.lbChequeMes_S.AutoSize = true;
		this.lbChequeMes_S.Location = new System.Drawing.Point(147, 101);
		this.lbChequeMes_S.Name = "lbChequeMes_S";
		this.lbChequeMes_S.Size = new System.Drawing.Size(52, 13);
		this.lbChequeMes_S.TabIndex = 41;
		this.lbChequeMes_S.Text = "00000.00";
		this.label52.AutoSize = true;
		this.label52.Location = new System.Drawing.Point(52, 101);
		this.label52.Name = "label52";
		this.label52.Size = new System.Drawing.Size(52, 13);
		this.label52.TabIndex = 40;
		this.label52.Text = "CHEQUE";
		this.lbTarjetaMes_D.AutoSize = true;
		this.lbTarjetaMes_D.Location = new System.Drawing.Point(260, 76);
		this.lbTarjetaMes_D.Name = "lbTarjetaMes_D";
		this.lbTarjetaMes_D.Size = new System.Drawing.Size(52, 13);
		this.lbTarjetaMes_D.TabIndex = 39;
		this.lbTarjetaMes_D.Text = "00000.00";
		this.lbTarjetaMes_S.AutoSize = true;
		this.lbTarjetaMes_S.Location = new System.Drawing.Point(147, 76);
		this.lbTarjetaMes_S.Name = "lbTarjetaMes_S";
		this.lbTarjetaMes_S.Size = new System.Drawing.Size(52, 13);
		this.lbTarjetaMes_S.TabIndex = 38;
		this.lbTarjetaMes_S.Text = "00000.00";
		this.label49.AutoSize = true;
		this.label49.Location = new System.Drawing.Point(49, 76);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(55, 13);
		this.label49.TabIndex = 37;
		this.label49.Text = "TARJETA";
		this.lbDepositoMes_D.AutoSize = true;
		this.lbDepositoMes_D.Location = new System.Drawing.Point(260, 58);
		this.lbDepositoMes_D.Name = "lbDepositoMes_D";
		this.lbDepositoMes_D.Size = new System.Drawing.Size(52, 13);
		this.lbDepositoMes_D.TabIndex = 36;
		this.lbDepositoMes_D.Text = "00000.00";
		this.lbDepositoMes_S.AutoSize = true;
		this.lbDepositoMes_S.Location = new System.Drawing.Point(147, 58);
		this.lbDepositoMes_S.Name = "lbDepositoMes_S";
		this.lbDepositoMes_S.Size = new System.Drawing.Size(52, 13);
		this.lbDepositoMes_S.TabIndex = 35;
		this.lbDepositoMes_S.Text = "00000.00";
		this.label45.AutoSize = true;
		this.label45.Location = new System.Drawing.Point(42, 58);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(62, 13);
		this.label45.TabIndex = 34;
		this.label45.Text = "DEPOSITO";
		this.lbEfectivoMes_D.AutoSize = true;
		this.lbEfectivoMes_D.Location = new System.Drawing.Point(260, 38);
		this.lbEfectivoMes_D.Name = "lbEfectivoMes_D";
		this.lbEfectivoMes_D.Size = new System.Drawing.Size(52, 13);
		this.lbEfectivoMes_D.TabIndex = 33;
		this.lbEfectivoMes_D.Text = "00000.00";
		this.lbEfectivoMes_S.AutoSize = true;
		this.lbEfectivoMes_S.Location = new System.Drawing.Point(147, 38);
		this.lbEfectivoMes_S.Name = "lbEfectivoMes_S";
		this.lbEfectivoMes_S.Size = new System.Drawing.Size(52, 13);
		this.lbEfectivoMes_S.TabIndex = 32;
		this.lbEfectivoMes_S.Text = "00000.00";
		this.label58.AutoSize = true;
		this.label58.Location = new System.Drawing.Point(42, 38);
		this.label58.Name = "label58";
		this.label58.Size = new System.Drawing.Size(59, 13);
		this.label58.TabIndex = 31;
		this.label58.Text = "EFECTIVO";
		this.lbTotalCajaDolares.AutoSize = true;
		this.lbTotalCajaDolares.Font = new System.Drawing.Font("Arial Narrow", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTotalCajaDolares.ForeColor = System.Drawing.Color.DarkSlateBlue;
		this.lbTotalCajaDolares.Location = new System.Drawing.Point(249, 6);
		this.lbTotalCajaDolares.Name = "lbTotalCajaDolares";
		this.lbTotalCajaDolares.Size = new System.Drawing.Size(105, 24);
		this.lbTotalCajaDolares.TabIndex = 2;
		this.lbTotalCajaDolares.Text = "00000000.00";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(9, 4);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(113, 20);
		this.label2.TabIndex = 0;
		this.label2.Text = "TOTAL EN CAJA:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.ClientSize = new System.Drawing.Size(768, 588);
		base.Controls.Add(this.groupBox7);
		base.Controls.Add(this.label37);
		base.Controls.Add(this.label38);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.label27);
		base.Controls.Add(this.label47);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.btnImprimir);
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
		base.Controls.Add(this.gbIngreso);
		base.Controls.Add(this.gbApertura);
		this.DoubleBuffered = true;
		base.Name = "frmCaja";
		this.Text = "STATUS Y CONTROL DE CAJA DIARIO";
		base.Load += new System.EventHandler(frmCajaChica_Load);
		this.gbApertura.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.gbIngreso.ResumeLayout(false);
		this.panel11.ResumeLayout(false);
		this.panel11.PerformLayout();
		this.panel17.ResumeLayout(false);
		this.panel17.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.panel9.PerformLayout();
		this.gbCierre.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		this.gbVentasCredito.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		this.gbComprasCredito.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		this.panel8.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.panel10.ResumeLayout(false);
		this.panel10.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.panel12.ResumeLayout(false);
		this.panel12.PerformLayout();
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
