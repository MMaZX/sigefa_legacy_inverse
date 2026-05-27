using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmCajaChica : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public List<int> seleccion = new List<int>();

	private clsCaja CajaChica = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	public int tipo = 0;

	private decimal Saldo = default(decimal);

	private decimal Ingresos = default(decimal);

	private decimal Egresos = default(decimal);

	public DateTime fechaactual = default(DateTime).Date;

	private int FilasChequeadas = 0;

	private decimal MontoRendido = default(decimal);

	private DataTable tabla = new DataTable();

	public int codcajachica = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private ButtonItem biEditar;

	private ButtonItem biEliminar;

	private ButtonItem biActualizar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimir;

	private ButtonItem biIngreso;

	private ButtonItem biEgreso;

	private ComboBox cboMovimientos;

	private Label label1;

	private Panel panel1;

	private Panel panel2;

	private Button btnCancelar;

	private ImageList imageList2;

	private Button btnExit;

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

	private Label lblEgresos;

	private Panel panel4;

	private RibbonBar ribbonBar2;

	private ButtonItem biRencicionCaja;

	private ButtonItem biBuscarRendicion;

	private ButtonItem biRendicionLiquidada;

	private ButtonItem biVerificarRendicion;

	private ButtonItem biHistorialRendiciones;

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

	private ButtonItem biRecibo;

	private ButtonItem biHistorialRecibos;

	private Label lblEgresoSinRecibo;

	private Label label14;

	public RibbonBar ribbonBar1;

	private DataGridView dgvMovimientosCajaChica;

	public DateTimePicker dtpfecha1;

	private DataGridViewTextBoxColumn codmovcaja;

	private DataGridViewTextBoxColumn codSucursal;

	private DataGridViewTextBoxColumn codcaja;

	private DataGridViewTextBoxColumn codpago;

	private DataGridViewTextBoxColumn concepto;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn CODTIPO;

	private DataGridViewTextBoxColumn tipopagocaja;

	private DataGridViewTextBoxColumn CODTIPOMOV;

	private DataGridViewTextBoxColumn tipoMovimiento;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn tipodocumento;

	private DataGridViewTextBoxColumn SIGLA;

	private DataGridViewTextBoxColumn codserie;

	private DataGridViewTextBoxColumn SERIE;

	private DataGridViewTextBoxColumn numerodocumento;

	private DataGridViewTextBoxColumn DOCREFERENCIA;

	private DataGridViewTextBoxColumn toneladas;

	private DataGridViewTextBoxColumn codTipoPagoCaja;

	private DataGridViewTextBoxColumn PAGOCAJA;

	private DataGridViewTextBoxColumn saldocaja;

	private DataGridViewTextBoxColumn NOMBRE;

	private DataGridViewTextBoxColumn DNI;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn CODMONEDA;

	private DataGridViewTextBoxColumn MONEDA;

	private DataGridViewTextBoxColumn TCVENTA;

	private Panel panel6;

	public frmCajaChica()
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
		ListaCajaChica();
	}

	private void ListaCajaChica()
	{
		dgvMovimientosCajaChica.Rows.Clear();
		tabla = AdmCaja.ListaCajaChica(frmLogin.iCodSucursal, dtpfecha1.Value.Date, CajaChica.Codcaja, frmLogin.iCodAlmacen);
		foreach (DataRow row in tabla.Rows)
		{
			dgvMovimientosCajaChica.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString(), row[26].ToString(), row[27].ToString(), row[28].ToString());
		}
		CalculoSaldo();
		darformato();
	}

	private void darformato()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvMovimientosCajaChica.Rows)
		{
			if (Convert.ToInt32(row.Cells[CODTIPO.Name].Value) == 1)
			{
				row.DefaultCellStyle.BackColor = Color.White;
				row.Cells[monto.Name].Style.ForeColor = Color.Blue;
				row.Cells[tipopagocaja.Name].Style.ForeColor = Color.Blue;
			}
			else if (Convert.ToInt32(row.Cells[CODTIPO.Name].Value) == 2)
			{
				row.DefaultCellStyle.BackColor = Color.White;
				row.Cells[monto.Name].Style.ForeColor = Color.Red;
				row.Cells[tipopagocaja.Name].Style.ForeColor = Color.Red;
			}
		}
	}

	private void CalculoSaldo()
	{
		try
		{
			decimal saldogrilla = default(decimal);
			foreach (DataGridViewRow row in (IEnumerable)dgvMovimientosCajaChica.Rows)
			{
				if (Convert.ToInt32(row.Cells[CODTIPO.Name].Value) == 1)
				{
					if (Convert.ToInt32(row.Cells[CODTIPOMOV.Name].Value) == 1)
					{
						if (Convert.ToInt32(row.Cells[CODMONEDA.Name].Value) == 2)
						{
							saldogrilla += Convert.ToDecimal(row.Cells[monto.Name].Value);
							row.Cells[saldocaja.Name].Value = $"{saldogrilla:#,##0.0000}";
							row.Cells[monto.Name].Value = $"{Convert.ToDecimal(row.Cells[monto.Name].Value) / Convert.ToDecimal(row.Cells[TCVENTA.Name].Value):#,##0.0000}";
						}
						else
						{
							saldogrilla += Convert.ToDecimal(row.Cells[monto.Name].Value);
							row.Cells[saldocaja.Name].Value = $"{saldogrilla:#,##0.0000}";
						}
					}
					if (Convert.ToInt32(row.Cells[CODTIPOMOV.Name].Value) != 2)
					{
						continue;
					}
					if (Convert.ToInt32(row.Cells[codTipoPagoCaja.Name].Value) == 5)
					{
						if (Convert.ToInt32(row.Cells[CODMONEDA.Name].Value) == 2)
						{
							saldogrilla += Convert.ToDecimal(row.Cells[monto.Name].Value);
							row.Cells[saldocaja.Name].Value = $"{saldogrilla:#,##0.0000}";
							row.Cells[monto.Name].Value = $"{Convert.ToDecimal(row.Cells[monto.Name].Value) / Convert.ToDecimal(row.Cells[TCVENTA.Name].Value):#,##0.0000}";
						}
						else
						{
							saldogrilla += Convert.ToDecimal(row.Cells[monto.Name].Value);
							row.Cells[saldocaja.Name].Value = $"{saldogrilla:#,##0.0000}";
						}
					}
					else
					{
						row.Cells[saldocaja.Name].Value = $"{saldogrilla:#,##0.0000}";
						row.DefaultCellStyle.BackColor = Color.Coral;
					}
				}
				else if (Convert.ToInt32(row.Cells[CODTIPO.Name].Value) == 2 && Convert.ToInt32(row.Cells[CODTIPOMOV.Name].Value) == 2)
				{
					if (Convert.ToInt32(row.Cells[CODMONEDA.Name].Value) == 2)
					{
						saldogrilla -= Convert.ToDecimal(row.Cells[monto.Name].Value);
						row.Cells[saldocaja.Name].Value = $"{saldogrilla:#,##0.0000}";
						row.Cells[monto.Name].Value = $"{Convert.ToDecimal(row.Cells[monto.Name].Value) / Convert.ToDecimal(row.Cells[TCVENTA.Name].Value):#,##0.0000}";
					}
					else
					{
						saldogrilla -= Convert.ToDecimal(row.Cells[monto.Name].Value);
						row.Cells[saldocaja.Name].Value = $"{saldogrilla:#,##0.0000}";
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void ListaCajaChicaFechas()
	{
	}

	private void VerificaSaldoCaja()
	{
		Saldo = default(decimal);
		CajaChica = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, dtpfecha1.Value, tipo, frmLogin.iCodAlmacen, frmLogin.iCodUser);
		if (CajaChica != null)
		{
			codcajachica = CajaChica.Codcaja;
			Saldo = CajaChica.TotalDisponible;
			lblIngresos.Text = $"{CajaChica.TotalIngreso.ToString():#,##0.00}";
			lblEgresos.Text = $"{CajaChica.TotalEgreso.ToString():#,##0.00}";
			lblAperturaCaja.Text = $"{CajaChica.Montoapertura.ToString():#,##0.00}";
			lblSaldoCaja.Text = $"{CajaChica.TotalDisponible.ToString():#,##0.00}";
		}
		else
		{
			Saldo = default(decimal);
			lblIngresos.Text = "0.000";
			lblEgresos.Text = "0.000";
			lblAperturaCaja.Text = "0.000";
			lblSaldoCaja.Text = "0.000";
			biIngreso.Enabled = false;
			biEgreso.Enabled = false;
		}
		if (Saldo > 0m)
		{
			biIngreso.Enabled = true;
			biEgreso.Enabled = true;
			biAperturaCajachica.Enabled = false;
		}
		else
		{
			biAperturaCajachica.Enabled = true;
			biIngreso.Enabled = false;
			biEgreso.Enabled = false;
		}
	}

	private void frmCajaChica_Load(object sender, EventArgs e)
	{
		fechaactual = DateTime.Now.Date;
		cboMovimientos.SelectedIndex = 0;
		VerificaSaldoCaja();
		ListaCajaChica();
	}

	private void biIngreso_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaChicaRegistro"] != null)
		{
			Application.OpenForms["frmCajaChicaRegistro"].Activate();
			return;
		}
		frmCajaChicaRegistro form = new frmCajaChicaRegistro();
		form.tipoCaja = tipo;
		form.Tipo = 1;
		form.Proceso = 1;
		form.ShowDialog();
		VerificaSaldoCaja();
		ListaCajaChica();
	}

	private void biHistorialRendiciones_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaChicaRendicionHistorial"] != null)
		{
			Application.OpenForms["frmCajaChicaRendicionHistorial"].Activate();
			return;
		}
		frmCajaChicaRendicionHistorial form = new frmCajaChicaRendicionHistorial();
		form.ShowDialog();
		ListaCajaChica();
	}

	private void biEgreso_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCajaChicaRegistro"] != null)
		{
			Application.OpenForms["frmCajaChicaRegistro"].Activate();
			return;
		}
		frmCajaChicaRegistro form = new frmCajaChicaRegistro();
		form.tipoCaja = tipo;
		form.Tipo = 2;
		form.Proceso = 1;
		form.SaldoCaja = Convert.ToDecimal(lblSaldoCaja.Text.Trim());
		form.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
		form.ShowDialog();
		VerificaSaldoCaja();
		ListaCajaChica();
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		cboMovimientos.SelectedIndex = 0;
		ListaCajaChica();
	}

	private void dtpfecha1_Leave(object sender, EventArgs e)
	{
		dtpfecha2.MinDate = dtpfecha1.Value;
	}

	private void dtpfecha2_Leave(object sender, EventArgs e)
	{
		dtpfecha1.MaxDate = dtpfecha2.Value;
	}

	private void dtpfecha1_ValueChanged(object sender, EventArgs e)
	{
		ListaCajaChicaFechas();
	}

	private void dtpfecha2_ValueChanged(object sender, EventArgs e)
	{
		ListaCajaChicaFechas();
	}

	private void biEditar_Click(object sender, EventArgs e)
	{
		CajaChica = AdmCaja.GetUltimaCajaVentas(frmLogin.iCodSucursal, tipo, frmLogin.iCodAlmacen);
		decimal ultimontocierre = AdmCaja.traersaldo();
		DialogResult dlgResult = MessageBox.Show(" SI => Saldo Actual: " + lblSaldoCaja.Text + " " + Environment.NewLine + "NO => Ultimo Monto Cierre: " + ultimontocierre + " ", "Caja Chica", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.No)
		{
			if (Application.OpenForms["frmArqueoFondoFijo"] != null)
			{
				Application.OpenForms["frmArqueoFondoFijo"].Activate();
				return;
			}
			frmArqueoFondoFijo form = new frmArqueoFondoFijo();
			form.Proceso = 1;
			form.monto = ultimontocierre;
			form.ShowDialog();
		}
		else if (Application.OpenForms["frmArqueoFondoFijo"] != null)
		{
			Application.OpenForms["frmArqueoFondoFijo"].Activate();
		}
		else
		{
			frmArqueoFondoFijo form2 = new frmArqueoFondoFijo();
			form2.Proceso = 1;
			form2.monto = Convert.ToDecimal(lblSaldoCaja.Text);
			form2.ShowDialog();
		}
	}

	private void dgvMovimientosCajaChica_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvMovimientosCajaChica.Rows.Count >= 1 && !e.Row.Selected)
		{
		}
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
		decimal montocierre = Convert.ToDecimal(lblSaldoCaja.Text);
		tipo = 1;
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Cerrar Caja" + Environment.NewLine + "Monto Cierre: " + lblSaldoCaja.Text + " ", "Caja Chica", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.Yes && AdmCaja.CerrarCajaVentas(frmLogin.iCodSucursal, dtpfecha1.Value.Date, CajaChica.Codcaja, frmLogin.iCodAlmacen))
		{
			MessageBox.Show("El cierre de caja se ha realizado correctamente", "Caja Chica", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			clsReporteCaja dso = new clsReporteCaja();
			CRCierreCajaChica rpt = new CRCierreCajaChica();
			frmRptCaja frm = new frmRptCaja();
			rpt.SetDataSource(dso.ReporteMovimientosCajaChica(frmLogin.iCodSucursal, dtpfecha1.Value.Date, CajaChica.Codcaja, frmLogin.iCodAlmacen).Tables[0]);
			frm.crvKardex.ReportSource = rpt;
			frm.Show();
			Close();
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
		if (Application.OpenForms["frmCajaChicaRendicion"] != null)
		{
			Application.OpenForms["frmCajaChicaRendicion"].Activate();
			return;
		}
		frmCajaChicaRendicion form = new frmCajaChicaRendicion();
		form.tipocaja = tipo;
		form.MdiParent = base.MdiParent;
		form.Show();
	}

	private void biRencicionCaja_Click(object sender, EventArgs e)
	{
	}

	private void biVerificarRendicion_Click(object sender, EventArgs e)
	{
		frmCajaChicaRendicionListado frm = new frmCajaChicaRendicionListado();
		frm.tipocaja = tipo;
		frm.ShowDialog();
		VerificaSaldoCaja();
		ListaCajaChica();
	}

	private void dgvMovimientosCajaChica_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		if (dgvMovimientosCajaChica.IsCurrentCellDirty)
		{
			dgvMovimientosCajaChica.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
	}

	private void biBuscar_Click(object sender, EventArgs e)
	{
		lblColumna.Text = "CODIGO";
		lblProperty.Text = "codPersonalizado";
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
		if (dgvMovimientosCajaChica.Columns[e.ColumnIndex].Index > 0)
		{
			lblColumna.Text = dgvMovimientosCajaChica.Columns[e.ColumnIndex].HeaderText;
			lblProperty.Text = dgvMovimientosCajaChica.Columns[e.ColumnIndex].DataPropertyName;
			if (expandablePanel1.Expanded)
			{
				txtFiltro.Focus();
			}
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
			form.tipoCaja = tipo;
			form.Tipo = 1;
			form.Proceso = 1;
			form.AperturaCaja = 1;
			form.ShowDialog();
			ListaCajaChica();
			VerificaSaldoCaja();
		}
	}

	private void biRendicionLiquidada_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRendicionesVigentes"] != null)
		{
			Application.OpenForms["frmRendicionesVigentes"].Activate();
			return;
		}
		frmRendicionesVigentes frm = new frmRendicionesVigentes();
		frm.tipocaja = tipo;
		frm.ShowDialog();
		VerificaSaldoCaja();
		ListaCajaChica();
	}

	private void biRecibo_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRecibos_CajaChica"] != null)
		{
			Application.OpenForms["frmRecibos_CajaChica"].Activate();
			return;
		}
		frmRecibos_CajaChica form = new frmRecibos_CajaChica();
		form.tipocaja = tipo;
		form.Proceso = 1;
		form.CodCaja = CajaChica.Codcaja;
		form.SaldoCaja = Convert.ToDecimal(lblSaldoCaja.Text.Trim());
		form.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
		form.ShowDialog();
		VerificaSaldoCaja();
		ListaCajaChica();
	}

	private void biHistorialRecibos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRecibos_CajaChica"] != null)
		{
			Application.OpenForms["frmRecibos_CajaChica"].Activate();
			return;
		}
		frmRecibos form = new frmRecibos();
		form.tipocaja = tipo;
		form.ShowDialog();
	}

	private void dgvMovimientosCajaChica_Sorted(object sender, EventArgs e)
	{
		darformato();
	}

	private void dgvMovimientosCajaChica_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		darformato();
	}

	private void biImprimir_Click(object sender, EventArgs e)
	{
		clsReporteCaja dso = new clsReporteCaja();
		CRReporteMovimientosCajaChica rpt = new CRReporteMovimientosCajaChica();
		frmReporteMovimientosCajaChica frm = new frmReporteMovimientosCajaChica();
		rpt.SetDataSource(dso.ReporteMovimientosCajaChica(frmLogin.iCodSucursal, dtpfecha1.Value, CajaChica.Codcaja, frmLogin.iCodAlmacen));
		frm.crvMovimientosdecajachica.ReportSource = rpt;
		frm.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCajaChica));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.biRecibo = new DevComponents.DotNetBar.ButtonItem();
		this.biHistorialRecibos = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biRendicionesContables = new DevComponents.DotNetBar.ButtonItem();
		this.biIngreso = new DevComponents.DotNetBar.ButtonItem();
		this.biEgreso = new DevComponents.DotNetBar.ButtonItem();
		this.cboMovimientos = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpfecha1 = new System.Windows.Forms.DateTimePicker();
		this.dtpfecha2 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.btnExit = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lblEgresoSinRecibo = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
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
		this.biVerificarRendicion = new DevComponents.DotNetBar.ButtonItem();
		this.biRendicionLiquidada = new DevComponents.DotNetBar.ButtonItem();
		this.biHistorialRendiciones = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscarRendicion = new DevComponents.DotNetBar.ButtonItem();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.lblProperty = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.lblColumna = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnclose = new System.Windows.Forms.Button();
		this.dgvMovimientosCajaChica = new System.Windows.Forms.DataGridView();
		this.codmovcaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codSucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codpago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CODTIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipopagocaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CODTIPOMOV = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipodocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.SIGLA = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codserie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.SERIE = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numerodocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DOCREFERENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.toneladas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codTipoPagoCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.PAGOCAJA = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.saldocaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.NOMBRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DNI = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CODMONEDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.MONEDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TCVENTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel4.SuspendLayout();
		this.expandablePanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvMovimientosCajaChica).BeginInit();
		base.SuspendLayout();
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar1.DragDropSupport = true;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[9] { this.biRecibo, this.biActualizar, this.biImprimir, this.biEliminar, this.biEditar, this.biBuscar, this.biRendicionesContables, this.biIngreso, this.biEgreso });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(547, 65);
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
		this.biRecibo.ImageIndex = 6;
		this.biRecibo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRecibo.Name = "biRecibo";
		this.biRecibo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biHistorialRecibos });
		this.biRecibo.SubItemsExpandWidth = 14;
		this.biRecibo.Text = "Recibos";
		this.biRecibo.Click += new System.EventHandler(biRecibo_Click);
		this.biHistorialRecibos.Name = "biHistorialRecibos";
		this.biHistorialRecibos.Text = "Historial Recibos";
		this.biHistorialRecibos.Click += new System.EventHandler(biHistorialRecibos_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingHorizontal = 10;
		this.biActualizar.ImagePaddingVertical = 15;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(biActualizar_Click);
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingHorizontal = 10;
		this.biImprimir.ImagePaddingVertical = 15;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Click += new System.EventHandler(biImprimir_Click);
		this.biEliminar.ImageIndex = 21;
		this.biEliminar.ImagePaddingHorizontal = 10;
		this.biEliminar.ImagePaddingVertical = 15;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Cerrar Caja";
		this.biEliminar.Click += new System.EventHandler(biEliminar_Click);
		this.biEditar.ImageIndex = 17;
		this.biEditar.ImagePaddingHorizontal = 10;
		this.biEditar.ImagePaddingVertical = 15;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Arqueo  de Caja Chica";
		this.biEditar.Click += new System.EventHandler(biEditar_Click);
		this.biBuscar.Enabled = false;
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
		this.biRendicionesContables.Enabled = false;
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
		this.biIngreso.Enabled = false;
		this.biIngreso.ImageIndex = 20;
		this.biIngreso.ImagePaddingHorizontal = 20;
		this.biIngreso.ImagePaddingVertical = 15;
		this.biIngreso.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biIngreso.Name = "biIngreso";
		this.biIngreso.SubItemsExpandWidth = 14;
		this.biIngreso.Text = "Ingreso";
		this.biIngreso.Visible = false;
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
		this.cboMovimientos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboMovimientos.Enabled = false;
		this.cboMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.cboMovimientos.FormattingEnabled = true;
		this.cboMovimientos.Items.AddRange(new object[3] { "TODOS LOS MOVIMIENTOS", "INGRESO", "EGRESO" });
		this.cboMovimientos.Location = new System.Drawing.Point(147, 7);
		this.cboMovimientos.Name = "cboMovimientos";
		this.cboMovimientos.Size = new System.Drawing.Size(216, 21);
		this.cboMovimientos.TabIndex = 11;
		this.cboMovimientos.Visible = false;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Enabled = false;
		this.label1.Location = new System.Drawing.Point(16, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(125, 13);
		this.label1.TabIndex = 11;
		this.label1.Text = "TIPO DE MOVIMIENTO:";
		this.label1.Visible = false;
		this.panel1.Controls.Add(this.ribbonBar1);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1107, 65);
		this.panel1.TabIndex = 12;
		this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel2.Controls.Add(this.label3);
		this.panel2.Controls.Add(this.dtpfecha1);
		this.panel2.Controls.Add(this.dtpfecha2);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.cboMovimientos);
		this.panel2.Controls.Add(this.btnExit);
		this.panel2.Controls.Add(this.btnCancelar);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(547, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(560, 65);
		this.panel2.TabIndex = 9;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Enabled = false;
		this.label3.Location = new System.Drawing.Point(247, 41);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(14, 13);
		this.label3.TabIndex = 37;
		this.label3.Text = "Y";
		this.label3.Visible = false;
		this.dtpfecha1.Enabled = false;
		this.dtpfecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha1.Location = new System.Drawing.Point(147, 37);
		this.dtpfecha1.Name = "dtpfecha1";
		this.dtpfecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpfecha1.TabIndex = 13;
		this.dtpfecha1.Visible = false;
		this.dtpfecha1.ValueChanged += new System.EventHandler(dtpfecha1_ValueChanged);
		this.dtpfecha1.Leave += new System.EventHandler(dtpfecha1_Leave);
		this.dtpfecha2.Enabled = false;
		this.dtpfecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha2.Location = new System.Drawing.Point(264, 37);
		this.dtpfecha2.Name = "dtpfecha2";
		this.dtpfecha2.Size = new System.Drawing.Size(99, 20);
		this.dtpfecha2.TabIndex = 14;
		this.dtpfecha2.Visible = false;
		this.dtpfecha2.ValueChanged += new System.EventHandler(dtpfecha2_ValueChanged);
		this.dtpfecha2.Leave += new System.EventHandler(dtpfecha2_Leave);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Enabled = false;
		this.label2.Location = new System.Drawing.Point(47, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(94, 13);
		this.label2.TabIndex = 36;
		this.label2.Text = "BUSCAR ENTRE:";
		this.label2.Visible = false;
		this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExit.ImageIndex = 0;
		this.btnExit.ImageList = this.imageList2;
		this.btnExit.Location = new System.Drawing.Point(484, 7);
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
		this.btnCancelar.ImageIndex = 3;
		this.btnCancelar.ImageList = this.imageList2;
		this.btnCancelar.Location = new System.Drawing.Point(369, 7);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(113, 50);
		this.btnCancelar.TabIndex = 34;
		this.btnCancelar.Text = "Cancelar Filtro";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Visible = false;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel3.Controls.Add(this.lblEgresoSinRecibo);
		this.panel3.Controls.Add(this.label14);
		this.panel3.Controls.Add(this.lblSaldoCaja);
		this.panel3.Controls.Add(this.lblAperturaCaja);
		this.panel3.Controls.Add(this.lblEgresos);
		this.panel3.Controls.Add(this.lblIngresos);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.label7);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.label4);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel3.Location = new System.Drawing.Point(591, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(516, 70);
		this.panel3.TabIndex = 13;
		this.lblEgresoSinRecibo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblEgresoSinRecibo.BackColor = System.Drawing.Color.Transparent;
		this.lblEgresoSinRecibo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblEgresoSinRecibo.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEgresoSinRecibo.ForeColor = System.Drawing.Color.DarkRed;
		this.lblEgresoSinRecibo.Location = new System.Drawing.Point(158, 2);
		this.lblEgresoSinRecibo.Name = "lblEgresoSinRecibo";
		this.lblEgresoSinRecibo.Size = new System.Drawing.Size(100, 20);
		this.lblEgresoSinRecibo.TabIndex = 53;
		this.lblEgresoSinRecibo.Text = "0.000";
		this.lblEgresoSinRecibo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblEgresoSinRecibo.Visible = false;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.BackColor = System.Drawing.Color.Transparent;
		this.label14.Location = new System.Drawing.Point(15, 6);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(137, 13);
		this.label14.TabIndex = 52;
		this.label14.Text = "EGRESO  SIN RECIBO S/:";
		this.label14.Visible = false;
		this.lblSaldoCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblSaldoCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblSaldoCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblSaldoCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSaldoCaja.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblSaldoCaja.Location = new System.Drawing.Point(409, 37);
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
		this.lblAperturaCaja.Location = new System.Drawing.Point(409, 11);
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
		this.lblEgresos.Location = new System.Drawing.Point(158, 24);
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
		this.lblIngresos.Location = new System.Drawing.Point(158, 47);
		this.lblIngresos.Name = "lblIngresos";
		this.lblIngresos.Size = new System.Drawing.Size(100, 20);
		this.lblIngresos.TabIndex = 43;
		this.lblIngresos.Text = "0.000";
		this.lblIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Location = new System.Drawing.Point(34, 28);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(108, 13);
		this.label6.TabIndex = 42;
		this.label6.Text = "EGRESO TOTAL S/:";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(30, 48);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(112, 13);
		this.label7.TabIndex = 41;
		this.label7.Text = "INGRESO TOTAL S/:";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(342, 41);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(61, 13);
		this.label5.TabIndex = 40;
		this.label5.Text = "SALDO S/:";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(290, 15);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(113, 13);
		this.label4.TabIndex = 37;
		this.label4.Text = "APERTURA CAJA S/:";
		this.panel4.Controls.Add(this.ribbonBar2);
		this.panel4.Controls.Add(this.panel3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 345);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(1107, 70);
		this.panel4.TabIndex = 14;
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[6] { this.biAperturaCajachica, this.biRencicionCaja, this.biVerificarRendicion, this.biRendicionLiquidada, this.biHistorialRendiciones, this.biBuscarRendicion });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(591, 70);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 14;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.biAperturaCajachica.Image = (System.Drawing.Image)resources.GetObject("biAperturaCajachica.Image");
		this.biAperturaCajachica.ImagePaddingHorizontal = 20;
		this.biAperturaCajachica.ImagePaddingVertical = 10;
		this.biAperturaCajachica.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAperturaCajachica.Name = "biAperturaCajachica";
		this.biAperturaCajachica.SubItemsExpandWidth = 14;
		this.biAperturaCajachica.Text = "Aperturar   Caja Chica";
		this.biAperturaCajachica.Click += new System.EventHandler(biAperturaCajachica_Click);
		this.biRencicionCaja.Enabled = false;
		this.biRencicionCaja.Image = (System.Drawing.Image)resources.GetObject("biRencicionCaja.Image");
		this.biRencicionCaja.ImagePaddingHorizontal = 10;
		this.biRencicionCaja.ImagePaddingVertical = 10;
		this.biRencicionCaja.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRencicionCaja.Name = "biRencicionCaja";
		this.biRencicionCaja.SubItemsExpandWidth = 14;
		this.biRencicionCaja.Text = "rendir Chica";
		this.biRencicionCaja.Visible = false;
		this.biRencicionCaja.Click += new System.EventHandler(biRencicionCaja_Click);
		this.biVerificarRendicion.Enabled = false;
		this.biVerificarRendicion.Image = (System.Drawing.Image)resources.GetObject("biVerificarRendicion.Image");
		this.biVerificarRendicion.ImagePaddingHorizontal = 20;
		this.biVerificarRendicion.ImagePaddingVertical = 10;
		this.biVerificarRendicion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biVerificarRendicion.Name = "biVerificarRendicion";
		this.biVerificarRendicion.SubItemsExpandWidth = 14;
		this.biVerificarRendicion.Text = "Verificar Rendiciones";
		this.biVerificarRendicion.Visible = false;
		this.biVerificarRendicion.Click += new System.EventHandler(biVerificarRendicion_Click);
		this.biRendicionLiquidada.Enabled = false;
		this.biRendicionLiquidada.Image = (System.Drawing.Image)resources.GetObject("biRendicionLiquidada.Image");
		this.biRendicionLiquidada.ImagePaddingHorizontal = 20;
		this.biRendicionLiquidada.ImagePaddingVertical = 10;
		this.biRendicionLiquidada.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRendicionLiquidada.Name = "biRendicionLiquidada";
		this.biRendicionLiquidada.SubItemsExpandWidth = 14;
		this.biRendicionLiquidada.Text = "Rendicion Liquidadas";
		this.biRendicionLiquidada.Visible = false;
		this.biRendicionLiquidada.Click += new System.EventHandler(biRendicionLiquidada_Click);
		this.biHistorialRendiciones.Enabled = false;
		this.biHistorialRendiciones.Image = (System.Drawing.Image)resources.GetObject("biHistorialRendiciones.Image");
		this.biHistorialRendiciones.ImagePaddingHorizontal = 20;
		this.biHistorialRendiciones.ImagePaddingVertical = 10;
		this.biHistorialRendiciones.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biHistorialRendiciones.Name = "biHistorialRendiciones";
		this.biHistorialRendiciones.SubItemsExpandWidth = 14;
		this.biHistorialRendiciones.Text = "Historial Rendiciones";
		this.biHistorialRendiciones.Visible = false;
		this.biHistorialRendiciones.Click += new System.EventHandler(biHistorialRendiciones_Click);
		this.biBuscarRendicion.Enabled = false;
		this.biBuscarRendicion.Image = (System.Drawing.Image)resources.GetObject("biBuscarRendicion.Image");
		this.biBuscarRendicion.ImagePaddingHorizontal = 20;
		this.biBuscarRendicion.ImagePaddingVertical = 10;
		this.biBuscarRendicion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscarRendicion.Name = "biBuscarRendicion";
		this.biBuscarRendicion.SubItemsExpandWidth = 14;
		this.biBuscarRendicion.Text = "Buscar Rendicion";
		this.biBuscarRendicion.Visible = false;
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
		this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.expandablePanel1.ExpandButtonVisible = false;
		this.expandablePanel1.Expanded = false;
		this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(1004, 0, 231, 93);
		this.expandablePanel1.Location = new System.Drawing.Point(870, 0);
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
		this.dgvMovimientosCajaChica.AllowUserToAddRows = false;
		this.dgvMovimientosCajaChica.AllowUserToDeleteRows = false;
		this.dgvMovimientosCajaChica.AllowUserToResizeRows = false;
		this.dgvMovimientosCajaChica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvMovimientosCajaChica.Columns.AddRange(this.codmovcaja, this.codSucursal, this.codcaja, this.codpago, this.concepto, this.monto, this.CODTIPO, this.tipopagocaja, this.CODTIPOMOV, this.tipoMovimiento, this.fecha, this.tipodocumento, this.SIGLA, this.codserie, this.SERIE, this.numerodocumento, this.DOCREFERENCIA, this.toneladas, this.codTipoPagoCaja, this.PAGOCAJA, this.saldocaja, this.NOMBRE, this.DNI, this.coduser, this.estado, this.fecharegistro, this.CODMONEDA, this.MONEDA, this.TCVENTA);
		this.dgvMovimientosCajaChica.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvMovimientosCajaChica.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvMovimientosCajaChica.Location = new System.Drawing.Point(0, 65);
		this.dgvMovimientosCajaChica.MultiSelect = false;
		this.dgvMovimientosCajaChica.Name = "dgvMovimientosCajaChica";
		this.dgvMovimientosCajaChica.RowHeadersVisible = false;
		this.dgvMovimientosCajaChica.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvMovimientosCajaChica.Size = new System.Drawing.Size(1107, 250);
		this.dgvMovimientosCajaChica.TabIndex = 22;
		this.codmovcaja.DataPropertyName = "codmovcaja";
		this.codmovcaja.HeaderText = "COD";
		this.codmovcaja.Name = "codmovcaja";
		this.codmovcaja.ReadOnly = true;
		this.codmovcaja.Visible = false;
		this.codmovcaja.Width = 70;
		this.codSucursal.DataPropertyName = "codSucursal";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.codSucursal.DefaultCellStyle = dataGridViewCellStyle1;
		this.codSucursal.HeaderText = "COD. SUCURSAL";
		this.codSucursal.Name = "codSucursal";
		this.codSucursal.ReadOnly = true;
		this.codSucursal.Visible = false;
		this.codSucursal.Width = 70;
		this.codcaja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.codcaja.DataPropertyName = "codcaja";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.codcaja.DefaultCellStyle = dataGridViewCellStyle2;
		this.codcaja.HeaderText = "COD. CAJA";
		this.codcaja.Name = "codcaja";
		this.codcaja.ReadOnly = true;
		this.codcaja.Visible = false;
		this.codcaja.Width = 300;
		this.codpago.DataPropertyName = "codpago";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.codpago.DefaultCellStyle = dataGridViewCellStyle3;
		this.codpago.HeaderText = "COD. PAGO";
		this.codpago.Name = "codpago";
		this.codpago.ReadOnly = true;
		this.codpago.Visible = false;
		this.concepto.DataPropertyName = "concepto";
		this.concepto.HeaderText = "CONCEPTO";
		this.concepto.Name = "concepto";
		this.concepto.ReadOnly = true;
		this.concepto.Width = 300;
		this.monto.DataPropertyName = "monto";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
		dataGridViewCellStyle4.Format = "N2";
		dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.monto.DefaultCellStyle = dataGridViewCellStyle4;
		this.monto.HeaderText = "MONTO";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.Width = 80;
		this.CODTIPO.DataPropertyName = "CODTIPO";
		this.CODTIPO.HeaderText = "CODTIPO";
		this.CODTIPO.Name = "CODTIPO";
		this.CODTIPO.Visible = false;
		this.tipopagocaja.DataPropertyName = "TipoPagoCaja";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.tipopagocaja.DefaultCellStyle = dataGridViewCellStyle5;
		this.tipopagocaja.HeaderText = "TIPO PAGO";
		this.tipopagocaja.Name = "tipopagocaja";
		this.tipopagocaja.ReadOnly = true;
		this.CODTIPOMOV.DataPropertyName = "CODTIPOMOV";
		this.CODTIPOMOV.HeaderText = "CODTIPOMOV";
		this.CODTIPOMOV.Name = "CODTIPOMOV";
		this.CODTIPOMOV.Visible = false;
		this.tipoMovimiento.DataPropertyName = "TipoMovimiento";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.tipoMovimiento.DefaultCellStyle = dataGridViewCellStyle6;
		this.tipoMovimiento.HeaderText = "TIPO MOV.";
		this.tipoMovimiento.Name = "tipoMovimiento";
		this.tipoMovimiento.ReadOnly = true;
		this.tipoMovimiento.Visible = false;
		this.tipoMovimiento.Width = 120;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle7;
		this.fecha.HeaderText = "FECHA";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Width = 120;
		this.tipodocumento.DataPropertyName = "tipodocumento";
		this.tipodocumento.HeaderText = "TIPO DOC.";
		this.tipodocumento.Name = "tipodocumento";
		this.tipodocumento.ReadOnly = true;
		this.tipodocumento.Visible = false;
		this.tipodocumento.Width = 40;
		this.SIGLA.HeaderText = "SIGLA";
		this.SIGLA.Name = "SIGLA";
		this.SIGLA.Visible = false;
		this.codserie.DataPropertyName = "codserie";
		this.codserie.HeaderText = "COD. SERIE";
		this.codserie.Name = "codserie";
		this.codserie.ReadOnly = true;
		this.codserie.Visible = false;
		this.SERIE.HeaderText = "SERIE";
		this.SERIE.Name = "SERIE";
		this.SERIE.Visible = false;
		this.SERIE.Width = 60;
		this.numerodocumento.DataPropertyName = "numerodocumento";
		this.numerodocumento.HeaderText = "Nª DOC.";
		this.numerodocumento.Name = "numerodocumento";
		this.DOCREFERENCIA.HeaderText = "DOC. REFERENCIA";
		this.DOCREFERENCIA.Name = "DOCREFERENCIA";
		this.DOCREFERENCIA.Visible = false;
		this.toneladas.DataPropertyName = "toneladas";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N2";
		dataGridViewCellStyle8.NullValue = null;
		dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.toneladas.DefaultCellStyle = dataGridViewCellStyle8;
		this.toneladas.HeaderText = "TN.";
		this.toneladas.Name = "toneladas";
		this.toneladas.ReadOnly = true;
		this.toneladas.Visible = false;
		this.toneladas.Width = 50;
		this.codTipoPagoCaja.DataPropertyName = "codTipoPagoCaja";
		this.codTipoPagoCaja.HeaderText = "COD. TIPO PAGO";
		this.codTipoPagoCaja.Name = "codTipoPagoCaja";
		this.codTipoPagoCaja.ReadOnly = true;
		this.codTipoPagoCaja.Visible = false;
		this.PAGOCAJA.HeaderText = "PAGO CAJA";
		this.PAGOCAJA.Name = "PAGOCAJA";
		this.PAGOCAJA.Visible = false;
		this.saldocaja.DataPropertyName = "saldocaja";
		this.saldocaja.HeaderText = "SALDO";
		this.saldocaja.Name = "saldocaja";
		this.NOMBRE.DataPropertyName = "nombre";
		this.NOMBRE.HeaderText = "NOMBRE";
		this.NOMBRE.Name = "NOMBRE";
		this.NOMBRE.Width = 300;
		this.DNI.DataPropertyName = "dni";
		this.DNI.HeaderText = "DNI";
		this.DNI.Name = "DNI";
		this.coduser.DataPropertyName = "coduser";
		this.coduser.HeaderText = "CODUSER";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "ESTADO";
		this.estado.Name = "estado";
		this.estado.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "FECHA REG.";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.CODMONEDA.DataPropertyName = "CODMONEDA";
		this.CODMONEDA.HeaderText = "CODMONEDA";
		this.CODMONEDA.Name = "CODMONEDA";
		this.CODMONEDA.Visible = false;
		this.MONEDA.DataPropertyName = "MONEDA";
		this.MONEDA.HeaderText = "MONEDA";
		this.MONEDA.Name = "MONEDA";
		this.TCVENTA.DataPropertyName = "TCVENTA";
		this.TCVENTA.HeaderText = "TC- VENTA";
		this.TCVENTA.Name = "TCVENTA";
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 315);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(1107, 30);
		this.panel6.TabIndex = 20;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1107, 415);
		base.Controls.Add(this.dgvMovimientosCajaChica);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmCajaChica";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Caja Chica";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmCajaChica_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvMovimientosCajaChica).EndInit();
		base.ResumeLayout(false);
	}
}
