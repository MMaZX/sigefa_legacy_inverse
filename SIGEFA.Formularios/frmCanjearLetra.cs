using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmCanjearLetra : Office2007Form
{
	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsPago Pag = new clsPago();

	private clsAdmPago Admpag = new clsAdmPago();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsAdmFactura Admfac = new clsAdmFactura();

	public clsFactura notaI = new clsFactura();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	public clsNotaSalida notaS = new clsNotaSalida();

	private clsAdmLetra Admletra = new clsAdmLetra();

	private clsLetra letra = new clsLetra();

	public clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private bool bok = false;

	public int Procede = 0;

	private DateTimePicker selfecha = new DateTimePicker();

	private int CodDocumento = 9;

	private string documento = "";

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox txtDireccionCliente;

	private Label label3;

	private TextBox txtZonaCliente;

	private Label label4;

	private TextBox txtNombreProveedor;

	private Label label2;

	private TextBox txtCodCliente;

	private Label label1;

	private TextBox txtMonto;

	private Label label5;

	private ComboBox cmbFormaPago;

	private Label label6;

	private TextBox txtTipoCambio;

	private ComboBox cmbMoneda;

	private Label label16;

	private Label label17;

	private Label label7;

	private NumericUpDown nudCuotas;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private DataGridView dgvLetras;

	private Label label8;

	private DateTimePicker dtpFecha;

	private DataGridViewTextBoxColumn numeroletra;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn fechavence;

	private DataGridViewTextBoxColumn numdocumento;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn pendiente;

	public frmCanjearLetra()
	{
		InitializeComponent();
	}

	private void frmCanjearLetra_Load(object sender, EventArgs e)
	{
		cargaMoneda();
		if (Procede == 1)
		{
			CargaFormaPagos(0);
			CargaCuentaxPagar();
		}
		else if (Procede == 2)
		{
			CargaFormaPagos(1);
			CargaCuentaxCobrar();
		}
		selfecha.Format = DateTimePickerFormat.Short;
		selfecha.Visible = false;
		selfecha.Width = 100;
		dgvLetras.Controls.Add(selfecha);
		selfecha.ValueChanged += selfecha_ValueChanged;
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedValue = 1;
	}

	private void CargaFormaPagos(int tipo)
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(tipo);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = -1;
	}

	private void CargaCuentaxPagar()
	{
		notaI = Admfac.CargaFactura(Convert.ToInt32(notaI.CodFactura));
		txtCodCliente.Text = notaI.RUCProveedor;
		txtNombreProveedor.Text = notaI.RazonSocialProveedor;
		cmbFormaPago.SelectedValue = notaI.FormaPago;
		fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		txtMonto.Text = $"{notaI.Pendiente:#,##0.00}";
		cmbMoneda.SelectedValue = notaI.Moneda;
		txtTipoCambio.Text = notaI.TipoCambio.ToString();
	}

	private void CargaCuentaxCobrar()
	{
		venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
		txtCodCliente.Text = venta.DNI;
		txtNombreProveedor.Text = venta.RazonSocialCliente;
		cmbFormaPago.SelectedValue = venta.FormaPago;
		fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		txtMonto.Text = $"{venta.Total:#,##0.00}";
		cmbMoneda.SelectedValue = venta.Moneda;
		txtTipoCambio.Text = venta.TipoCambio.ToString();
		documento = venta.SiglaDocumento + "-" + venta.Serie + "-" + venta.NumDoc;
	}

	private void nudCuotas_ValueChanged(object sender, EventArgs e)
	{
		LlenarProgramaLetras();
	}

	private void LlenarProgramaLetras()
	{
		dgvLetras.Rows.Clear();
		if (nudCuotas.Value != -1m && txtMonto.Text != "" && Convert.ToInt32(cmbFormaPago.SelectedValue) != -1)
		{
			double cuota = 0.0;
			if (Procede == 1)
			{
				cuota = notaI.Pendiente / Convert.ToDouble(nudCuotas.Value);
			}
			else if (Procede == 2)
			{
				cuota = Convert.ToDouble(venta.Total) / Convert.ToDouble(nudCuotas.Value);
			}
			for (int i = 1; (decimal)i <= nudCuotas.Value; i++)
			{
				dgvLetras.Rows.Add("LT - " + i, dtpFecha.Value.Date.ToShortDateString(), CalcularFechaCuota().ToShortDateString(), documento, cmbMoneda.Text, cuota, cuota);
			}
			btnGuardar.Enabled = true;
		}
	}

	private DateTime CalcularFechaCuota()
	{
		DateTime fechaanterior = default(DateTime);
		DateTime fechaactual = default(DateTime);
		int dias = 0;
		dias = fpago.Dias;
		dias /= Convert.ToInt32(nudCuotas.Value);
		fechaanterior = ((dgvLetras.Rows.Count != 0) ? Convert.ToDateTime(dgvLetras.Rows[dgvLetras.Rows.Count - 1].Cells[fechavence.Name].Value) : dtpFecha.Value.Date);
		if (Convert.ToInt32(cmbFormaPago.SelectedValue) != -1 && fpago != null)
		{
			return fechaanterior.AddDays(dias);
		}
		return fechaactual;
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		LlenarProgramaLetras();
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		LlenarProgramaLetras();
	}

	private void selfecha_ValueChanged(object sender, EventArgs e)
	{
		dgvLetras.CurrentCell.Value = selfecha.Value;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (Procede == 1)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvLetras.Rows)
			{
				letra.CodAlmacen = frmLogin.iCodAlmacen;
				letra.CodDocumento = CodDocumento;
				letra.NumDocumento = Convert.ToString(row.Cells[numdocumento.Name].Value);
				letra.CodNota = Convert.ToInt32(notaI.CodNotaIngreso);
				letra.Codfactura = notaI.CodFactura;
				letra.Codfacturaventa = 0;
				letra.DocumentoReferencia = notaI.DocumentoFactura;
				letra.CodProveedor = notaI.CodProveedor;
				letra.FechaEmision = Convert.ToDateTime(row.Cells[fechaemision.Name].Value);
				letra.FechaVencimiento = Convert.ToDateTime(row.Cells[fechavence.Name].Value);
				letra.IngresoEgreso = false;
				letra.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				letra.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
				letra.Monto = Convert.ToDouble(row.Cells[monto.Name].Value);
				letra.MontoPendiente = Convert.ToDouble(row.Cells[monto.Name].Value);
				if (letra.NumDocumento != "")
				{
					if (Admletra.insert(letra))
					{
						bok = true;
					}
					else
					{
						bok = false;
					}
				}
				else
				{
					bok = false;
					MessageBox.Show("Ingrese numero de documentos de las letras", "Letras", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			if (bok)
			{
				MessageBox.Show("Se generarón las letras correctamente", "Letras", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				btnGuardar.Enabled = false;
			}
		}
		else
		{
			if (Procede != 2)
			{
				return;
			}
			foreach (DataGridViewRow row2 in (IEnumerable)dgvLetras.Rows)
			{
				letra.CodAlmacen = frmLogin.iCodAlmacen;
				letra.CodDocumento = CodDocumento;
				letra.CodSerie = 11;
				letra.CodNota = Convert.ToInt32(venta.CodFacturaVenta);
				letra.DocumentoReferencia = venta.SiglaDocumento + "-" + venta.Serie + "-" + venta.NumDoc;
				letra.CodLiberado = venta.CodCliente;
				letra.FechaEmision = Convert.ToDateTime(row2.Cells[fechaemision.Name].Value);
				letra.FechaVencimiento = Convert.ToDateTime(row2.Cells[fechavence.Name].Value);
				letra.IngresoEgreso = true;
				letra.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				letra.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
				letra.Monto = Convert.ToDouble(row2.Cells[monto.Name].Value);
				letra.MontoPendiente = Convert.ToDouble(row2.Cells[pendiente.Name].Value);
				if (Admletra.insert(letra))
				{
					bok = true;
				}
				else
				{
					bok = false;
				}
			}
			if (bok)
			{
				MessageBox.Show("Se generarón las letras correctamente", "Letras", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				dgvLetras.DataSource = Admletra.MuestraListaLetrasNota(Convert.ToInt32(venta.CodFacturaVenta));
				btnGuardar.Enabled = false;
			}
		}
	}

	private void dgvLetras_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		try
		{
			if (dgvLetras.Focused && e.ColumnIndex == dgvLetras.Columns[fechavence.Name].Index)
			{
				selfecha.Location = dgvLetras.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, cutOverflow: false).Location;
				selfecha.Visible = true;
				if (dgvLetras.CurrentCell.Value != DBNull.Value)
				{
					selfecha.Value = Convert.ToDateTime(dgvLetras.CurrentCell.Value);
				}
				else
				{
					selfecha.Value = DateTime.Today;
				}
			}
			else
			{
				selfecha.Visible = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvLetras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvLetras.Focused && e.ColumnIndex == dgvLetras.Columns[fechavence.Name].Index)
			{
				dgvLetras.CurrentCell.Value = selfecha.Value.Date;
			}
			if (dgvLetras.Focused && e.ColumnIndex == dgvLetras.Columns[monto.Name].Index)
			{
				dgvLetras.CurrentRow.Cells[pendiente.Name].Value = dgvLetras.CurrentCell.Value;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCanjearLetra));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.nudCuotas = new System.Windows.Forms.NumericUpDown();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.txtMonto = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDireccionCliente = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtZonaCliente = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtNombreProveedor = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvLetras = new System.Windows.Forms.DataGridView();
		this.numeroletra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechavence = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudCuotas).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvLetras).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.nudCuotas);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtMonto);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtDireccionCliente);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtZonaCliente);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtNombreProveedor);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(838, 103);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos del cliente";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(697, 19);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(91, 20);
		this.dtpFecha.TabIndex = 54;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(655, 23);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(36, 12);
		this.label8.TabIndex = 53;
		this.label8.Text = "Fecha";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(406, 72);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(18, 12);
		this.label7.TabIndex = 52;
		this.label7.Text = "N°";
		this.nudCuotas.Location = new System.Drawing.Point(431, 71);
		this.nudCuotas.Maximum = new decimal(new int[4] { 300, 0, 0, 0 });
		this.nudCuotas.Name = "nudCuotas";
		this.nudCuotas.Size = new System.Drawing.Size(44, 20);
		this.nudCuotas.TabIndex = 7;
		this.nudCuotas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudCuotas.ValueChanged += new System.EventHandler(nudCuotas_ValueChanged);
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(99, 72);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(150, 20);
		this.cmbFormaPago.TabIndex = 5;
		this.cmbFormaPago.Tag = "16";
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label6.ForeColor = System.Drawing.Color.SteelBlue;
		this.label6.Location = new System.Drawing.Point(13, 76);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(80, 12);
		this.label6.TabIndex = 50;
		this.label6.Tag = "16";
		this.label6.Text = "Forma de Pago";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(579, 72);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(70, 20);
		this.txtTipoCambio.TabIndex = 8;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(707, 72);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 20);
		this.cmbMoneda.TabIndex = 9;
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label16.ForeColor = System.Drawing.Color.SteelBlue;
		this.label16.Location = new System.Drawing.Point(505, 74);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(68, 12);
		this.label16.TabIndex = 49;
		this.label16.Text = "Tipo/Cambio";
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label17.ForeColor = System.Drawing.Color.SteelBlue;
		this.label17.Location = new System.Drawing.Point(655, 74);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(45, 12);
		this.label17.TabIndex = 48;
		this.label17.Text = "Moneda";
		this.txtMonto.Location = new System.Drawing.Point(300, 71);
		this.txtMonto.Name = "txtMonto";
		this.txtMonto.ReadOnly = true;
		this.txtMonto.Size = new System.Drawing.Size(100, 20);
		this.txtMonto.TabIndex = 6;
		this.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(257, 74);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(37, 12);
		this.label5.TabIndex = 8;
		this.label5.Text = "Monto";
		this.txtDireccionCliente.Location = new System.Drawing.Point(225, 45);
		this.txtDireccionCliente.Name = "txtDireccionCliente";
		this.txtDireccionCliente.ReadOnly = true;
		this.txtDireccionCliente.Size = new System.Drawing.Size(539, 20);
		this.txtDireccionCliente.TabIndex = 4;
		this.txtDireccionCliente.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(162, 49);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 6;
		this.label3.Text = "Dirección";
		this.label3.Visible = false;
		this.txtZonaCliente.Location = new System.Drawing.Point(59, 45);
		this.txtZonaCliente.Name = "txtZonaCliente";
		this.txtZonaCliente.ReadOnly = true;
		this.txtZonaCliente.Size = new System.Drawing.Size(97, 20);
		this.txtZonaCliente.TabIndex = 3;
		this.txtZonaCliente.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(13, 49);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(30, 12);
		this.label4.TabIndex = 4;
		this.label4.Text = "Zona";
		this.label4.Visible = false;
		this.txtNombreProveedor.Location = new System.Drawing.Point(225, 19);
		this.txtNombreProveedor.Name = "txtNombreProveedor";
		this.txtNombreProveedor.ReadOnly = true;
		this.txtNombreProveedor.Size = new System.Drawing.Size(400, 20);
		this.txtNombreProveedor.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(162, 23);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(57, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "Proveedor";
		this.txtCodCliente.Location = new System.Drawing.Point(59, 19);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.ReadOnly = true;
		this.txtCodCliente.Size = new System.Drawing.Size(97, 20);
		this.txtCodCliente.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(13, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "RUC";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 480);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(838, 50);
		this.groupBox3.TabIndex = 25;
		this.groupBox3.TabStop = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(762, 12);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 22;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 12);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 23;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Visible = false;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(642, 12);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(114, 32);
		this.btnGuardar.TabIndex = 21;
		this.btnGuardar.Text = "Generar Letras";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 12);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 24;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Visible = false;
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(155, 12);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 25;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Visible = false;
		this.groupBox2.Controls.Add(this.dgvLetras);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 103);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(838, 377);
		this.groupBox2.TabIndex = 26;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Letras Pendientes de pago";
		this.dgvLetras.AllowUserToAddRows = false;
		this.dgvLetras.AllowUserToDeleteRows = false;
		this.dgvLetras.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvLetras.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvLetras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvLetras.Columns.AddRange(this.numeroletra, this.fechaemision, this.fechavence, this.numdocumento, this.moneda, this.monto, this.pendiente);
		this.dgvLetras.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvLetras.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvLetras.Location = new System.Drawing.Point(3, 16);
		this.dgvLetras.MultiSelect = false;
		this.dgvLetras.Name = "dgvLetras";
		this.dgvLetras.RowHeadersVisible = false;
		this.dgvLetras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvLetras.Size = new System.Drawing.Size(832, 358);
		this.dgvLetras.TabIndex = 9;
		this.dgvLetras.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvLetras_CellBeginEdit);
		this.dgvLetras.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvLetras_CellEndEdit);
		this.numeroletra.HeaderText = "Nº Letar";
		this.numeroletra.Name = "numeroletra";
		this.numeroletra.Width = 50;
		this.fechaemision.DataPropertyName = "fechaemision";
		this.fechaemision.HeaderText = "Fecha Emi.";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechavence.DataPropertyName = "fechavencimiento";
		dataGridViewCellStyle2.Format = "d";
		dataGridViewCellStyle2.NullValue = null;
		this.fechavence.DefaultCellStyle = dataGridViewCellStyle2;
		this.fechavence.HeaderText = "Fecha Venc.";
		this.fechavence.Name = "fechavence";
		this.numdocumento.DataPropertyName = "numdocumento";
		this.numdocumento.HeaderText = "N° Documento";
		this.numdocumento.Name = "numdocumento";
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.monto.DataPropertyName = "monto";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		this.monto.DefaultCellStyle = dataGridViewCellStyle3;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.pendiente.DataPropertyName = "montopendiente";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		this.pendiente.DefaultCellStyle = dataGridViewCellStyle4;
		this.pendiente.HeaderText = "Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(838, 530);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCanjearLetra";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Canjear Letra";
		base.Load += new System.EventHandler(frmCanjearLetra_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudCuotas).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvLetras).EndInit();
		base.ResumeLayout(false);
	}
}
