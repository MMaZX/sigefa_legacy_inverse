using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA;

public class frmCanjearCuota : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsPago Pag = new clsPago();

	private clsAdmPago Admpag = new clsAdmPago();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();

	public clsNotaIngreso notaI = new clsNotaIngreso();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsAdmPrestamoBancario AdmPreBan = new clsAdmPrestamoBancario();

	public clsPrestamoBancario preBan = new clsPrestamoBancario();

	private clsAdmCuota AdmCuota = new clsAdmCuota();

	private clsCuota cuota = new clsCuota();

	private bool bok = false;

	public int Procede = 0;

	private DateTimePicker selfecha = new DateTimePicker();

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha;

	private Label label8;

	private Label label7;

	private NumericUpDown nudCuotas;

	private TextBox txtTipoCambio;

	private Label label16;

	private Label label17;

	private TextBox txtDiasEntreCuo;

	private Label label5;

	private TextBox txtBanco;

	private Label label2;

	private TextBox txtCodPreBan;

	private Label label1;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private DataGridView dgvCuotas;

	private TextBox txtPrestamo;

	private Label label4;

	private Label label9;

	private Label label6;

	private TextBox txtMoneda;

	private TextBox txtTotal;

	private TextBox txtInteres;

	private TextBox txtNDEC;

	private DataGridViewTextBoxColumn codCuotaPrestamo;

	private DataGridViewTextBoxColumn codPrestamoBancario;

	private DataGridViewTextBoxColumn codMoneda;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn fechavence;

	private DataGridViewTextBoxColumn fechacancelado;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn tipoCambio;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn pendiente;

	private DataGridViewTextBoxColumn montoadicional;

	private DataGridViewTextBoxColumn cancelado;

	private DataGridViewTextBoxColumn cadCancelado;

	public frmCanjearCuota()
	{
		InitializeComponent();
	}

	private void frmCanjearCuota_Load(object sender, EventArgs e)
	{
		CargaCuentaxPagar();
		selfecha.Format = DateTimePickerFormat.Short;
		selfecha.Visible = false;
		selfecha.Width = 100;
		dgvCuotas.Controls.Add(selfecha);
		selfecha.ValueChanged += selfecha_ValueChanged;
		txtDiasEntreCuo.Text = "0";
		txtNDEC.Visible = false;
		if (Procede == 2)
		{
			txtDiasEntreCuo.Visible = false;
			label5.Visible = false;
			nudCuotas.Visible = false;
			txtNDEC.Visible = true;
			btnGuardar.Enabled = false;
			dgvCuotas.ReadOnly = true;
			cargaCuotas();
		}
	}

	private void CargaCuentaxPagar()
	{
		preBan = AdmPreBan.CargaPrestamoBancario(Convert.ToInt32(preBan.CodPrestamoBancario));
		txtCodPreBan.Text = preBan.CodPrestamoBancario.ToString();
		txtBanco.Text = preBan.DescBanco;
		txtPrestamo.Text = $"{preBan.Montoprestamo:#,##0.00}";
		txtInteres.Text = $"{preBan.Montointeres:#,##0.00}";
		txtTotal.Text = $"{preBan.Montodevolver:#,##0.00}";
		txtMoneda.Text = preBan.DescMoneda;
		txtTipoCambio.Text = $"{preBan.TipoCambio:#,##0.000}";
		dtpFecha.Value = Convert.ToDateTime(preBan.Fechaaprobacion);
		if (Procede == 2)
		{
			txtNDEC.Text = preBan.CantCuotas.ToString();
		}
	}

	private void cargaCuotas()
	{
		dgvCuotas.DataSource = data;
		data.DataSource = AdmCuota.MuestraListaCuotasPrestamo(preBan.CodPrestamoBancario);
		dgvCuotas.ClearSelection();
	}

	private void nudCuotas_ValueChanged(object sender, EventArgs e)
	{
		if (nudCuotas.Value >= 0m)
		{
			if (Convert.ToInt32(txtDiasEntreCuo.Text) > 0)
			{
				LlenarProgramaLetras();
			}
			else
			{
				MessageBox.Show("Ingrese Nro de días entre Cuotas", "Cuotas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		if (nudCuotas.Value > 0m)
		{
			LlenarProgramaLetras();
		}
	}

	private void LlenarProgramaLetras()
	{
		dgvCuotas.Rows.Clear();
		if (nudCuotas.Value != -1m && txtDiasEntreCuo.Text != "")
		{
			decimal cuota = default(decimal);
			cuota = preBan.Montodevolver / Convert.ToDecimal(nudCuotas.Value);
			for (int i = 1; (decimal)i <= nudCuotas.Value; i++)
			{
				dgvCuotas.Rows.Add("", preBan.CodPrestamoBancario, preBan.CodMoneda, dtpFecha.Value.Date.ToShortDateString(), CalcularFechaCuota().ToShortDateString(), "", preBan.DescMoneda, "", cuota, cuota, "", "", "");
			}
			btnGuardar.Enabled = true;
		}
	}

	private DateTime CalcularFechaCuota()
	{
		DateTime fechaanterior = default(DateTime);
		DateTime fechaactual = default(DateTime);
		return ((dgvCuotas.Rows.Count != 0) ? Convert.ToDateTime(dgvCuotas.Rows[dgvCuotas.Rows.Count - 1].Cells[fechavence.Name].Value) : dtpFecha.Value.Date).AddDays(Convert.ToInt32(txtDiasEntreCuo.Text));
	}

	private void selfecha_ValueChanged(object sender, EventArgs e)
	{
		dgvCuotas.CurrentCell.Value = selfecha.Value;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		int cuoNro = 0;
		foreach (DataGridViewRow row in (IEnumerable)dgvCuotas.Rows)
		{
			cuoNro++;
			cuota.CodPrestamoBancario = Convert.ToInt32(preBan.CodPrestamoBancario);
			cuota.NroCuota = cuoNro;
			cuota.CodMoneda = preBan.CodMoneda;
			cuota.FechaEmision = Convert.ToDateTime(row.Cells[fechaemision.Name].Value);
			cuota.FechaVencimiento = Convert.ToDateTime(row.Cells[fechavence.Name].Value);
			cuota.Monto = Convert.ToDecimal(row.Cells[monto.Name].Value);
			cuota.MontoPendiente = Convert.ToDecimal(row.Cells[pendiente.Name].Value);
			if (AdmCuota.insert(cuota))
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
			MessageBox.Show("Se generaron las cuotas correctamente", "Cuotas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void dgvCuotas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		try
		{
			if (dgvCuotas.Focused && e.ColumnIndex == dgvCuotas.Columns[fechavence.Name].Index)
			{
				selfecha.Location = dgvCuotas.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, cutOverflow: false).Location;
				selfecha.Visible = true;
				if (dgvCuotas.CurrentCell.Value != DBNull.Value)
				{
					selfecha.Value = Convert.ToDateTime(dgvCuotas.CurrentCell.Value);
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

	private void dgvCuotas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvCuotas.Focused && e.ColumnIndex == dgvCuotas.Columns[fechavence.Name].Index)
			{
				dgvCuotas.CurrentCell.Value = selfecha.Value.Date;
			}
			if (dgvCuotas.Focused && e.ColumnIndex == dgvCuotas.Columns[monto.Name].Index)
			{
				dgvCuotas.CurrentRow.Cells[pendiente.Name].Value = dgvCuotas.CurrentCell.Value;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtDiasEntreCuo_TextChanged(object sender, EventArgs e)
	{
		if (txtDiasEntreCuo.Text == "")
		{
			txtDiasEntreCuo.Text = "0.00";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.frmCanjearCuota));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtMoneda = new System.Windows.Forms.TextBox();
		this.txtTotal = new System.Windows.Forms.TextBox();
		this.txtInteres = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.nudCuotas = new System.Windows.Forms.NumericUpDown();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.txtDiasEntreCuo = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtPrestamo = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtBanco = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodPreBan = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtNDEC = new System.Windows.Forms.TextBox();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvCuotas = new System.Windows.Forms.DataGridView();
		this.codCuotaPrestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codPrestamoBancario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechavence = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechacancelado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoCambio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montoadicional = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cancelado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cadCancelado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudCuotas).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCuotas).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtMoneda);
		this.groupBox1.Controls.Add(this.txtTotal);
		this.groupBox1.Controls.Add(this.txtInteres);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.nudCuotas);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtDiasEntreCuo);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtPrestamo);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtBanco);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtCodPreBan);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtNDEC);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(781, 103);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos del cliente";
		this.txtMoneda.Location = new System.Drawing.Point(475, 45);
		this.txtMoneda.Name = "txtMoneda";
		this.txtMoneda.ReadOnly = true;
		this.txtMoneda.Size = new System.Drawing.Size(124, 20);
		this.txtMoneda.TabIndex = 59;
		this.txtTotal.Location = new System.Drawing.Point(347, 45);
		this.txtTotal.Name = "txtTotal";
		this.txtTotal.ReadOnly = true;
		this.txtTotal.Size = new System.Drawing.Size(66, 20);
		this.txtTotal.TabIndex = 58;
		this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtInteres.Location = new System.Drawing.Point(206, 45);
		this.txtInteres.Name = "txtInteres";
		this.txtInteres.ReadOnly = true;
		this.txtInteres.Size = new System.Drawing.Size(59, 20);
		this.txtInteres.TabIndex = 57;
		this.txtInteres.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(276, 49);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(65, 12);
		this.label9.TabIndex = 56;
		this.label9.Text = "Deuda Total";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label6.ForeColor = System.Drawing.Color.SteelBlue;
		this.label6.Location = new System.Drawing.Point(159, 49);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 12);
		this.label6.TabIndex = 55;
		this.label6.Text = "Interes";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(678, 19);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(91, 20);
		this.dtpFecha.TabIndex = 54;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(594, 23);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(88, 12);
		this.label8.TabIndex = 53;
		this.label8.Text = "Primera Aprob. :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(15, 77);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(57, 12);
		this.label7.TabIndex = 52;
		this.label7.Text = "N° Cuotas";
		this.nudCuotas.Location = new System.Drawing.Point(79, 75);
		this.nudCuotas.Maximum = new decimal(new int[4] { 300, 0, 0, 0 });
		this.nudCuotas.Name = "nudCuotas";
		this.nudCuotas.Size = new System.Drawing.Size(44, 20);
		this.nudCuotas.TabIndex = 7;
		this.nudCuotas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudCuotas.ValueChanged += new System.EventHandler(nudCuotas_ValueChanged);
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(699, 75);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(70, 20);
		this.txtTipoCambio.TabIndex = 8;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label16.ForeColor = System.Drawing.Color.SteelBlue;
		this.label16.Location = new System.Drawing.Point(623, 77);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(68, 12);
		this.label16.TabIndex = 49;
		this.label16.Text = "Tipo/Cambio";
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label17.ForeColor = System.Drawing.Color.SteelBlue;
		this.label17.Location = new System.Drawing.Point(424, 49);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(45, 12);
		this.label17.TabIndex = 48;
		this.label17.Text = "Moneda";
		this.txtDiasEntreCuo.Location = new System.Drawing.Point(720, 45);
		this.txtDiasEntreCuo.Name = "txtDiasEntreCuo";
		this.txtDiasEntreCuo.Size = new System.Drawing.Size(48, 20);
		this.txtDiasEntreCuo.TabIndex = 6;
		this.txtDiasEntreCuo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDiasEntreCuo.TextChanged += new System.EventHandler(txtDiasEntreCuo_TextChanged);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(623, 48);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(94, 12);
		this.label5.TabIndex = 8;
		this.label5.Text = "Días entre cuotas";
		this.txtPrestamo.Location = new System.Drawing.Point(76, 45);
		this.txtPrestamo.Name = "txtPrestamo";
		this.txtPrestamo.ReadOnly = true;
		this.txtPrestamo.Size = new System.Drawing.Size(72, 20);
		this.txtPrestamo.TabIndex = 3;
		this.txtPrestamo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(13, 49);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(53, 12);
		this.label4.TabIndex = 4;
		this.label4.Text = "Prestamo";
		this.txtBanco.Location = new System.Drawing.Point(224, 19);
		this.txtBanco.Name = "txtBanco";
		this.txtBanco.ReadOnly = true;
		this.txtBanco.Size = new System.Drawing.Size(349, 20);
		this.txtBanco.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(177, 23);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(36, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "Banco";
		this.txtCodPreBan.Location = new System.Drawing.Point(65, 19);
		this.txtCodPreBan.Name = "txtCodPreBan";
		this.txtCodPreBan.ReadOnly = true;
		this.txtCodPreBan.Size = new System.Drawing.Size(97, 20);
		this.txtCodPreBan.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(13, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(49, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "CODIGO";
		this.txtNDEC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtNDEC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNDEC.Location = new System.Drawing.Point(78, 75);
		this.txtNDEC.Name = "txtNDEC";
		this.txtNDEC.ReadOnly = true;
		this.txtNDEC.Size = new System.Drawing.Size(53, 20);
		this.txtNDEC.TabIndex = 60;
		this.txtNDEC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
		this.groupBox3.Location = new System.Drawing.Point(0, 416);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(781, 50);
		this.groupBox3.TabIndex = 26;
		this.groupBox3.TabStop = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(705, 12);
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
		this.btnGuardar.Location = new System.Drawing.Point(585, 12);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(114, 32);
		this.btnGuardar.TabIndex = 21;
		this.btnGuardar.Text = "Generar Cuotas";
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
		this.groupBox2.Controls.Add(this.dgvCuotas);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 103);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(781, 313);
		this.groupBox2.TabIndex = 27;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Letras Pendientes de pago";
		this.dgvCuotas.AllowUserToAddRows = false;
		this.dgvCuotas.AllowUserToDeleteRows = false;
		this.dgvCuotas.AllowUserToResizeRows = false;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCuotas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
		this.dgvCuotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCuotas.Columns.AddRange(this.codCuotaPrestamo, this.codPrestamoBancario, this.codMoneda, this.fechaemision, this.fechavence, this.fechacancelado, this.moneda, this.tipoCambio, this.monto, this.pendiente, this.montoadicional, this.cancelado, this.cadCancelado);
		this.dgvCuotas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvCuotas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvCuotas.Location = new System.Drawing.Point(3, 16);
		this.dgvCuotas.MultiSelect = false;
		this.dgvCuotas.Name = "dgvCuotas";
		this.dgvCuotas.RowHeadersVisible = false;
		this.dgvCuotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCuotas.Size = new System.Drawing.Size(775, 294);
		this.dgvCuotas.TabIndex = 9;
		this.dgvCuotas.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvCuotas_CellBeginEdit);
		this.dgvCuotas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCuotas_CellEndEdit);
		this.codCuotaPrestamo.DataPropertyName = "codCuotaPrestamo";
		this.codCuotaPrestamo.HeaderText = "codCuotaPrestamo";
		this.codCuotaPrestamo.Name = "codCuotaPrestamo";
		this.codCuotaPrestamo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codCuotaPrestamo.Visible = false;
		this.codPrestamoBancario.DataPropertyName = "codPrestamoBancario";
		this.codPrestamoBancario.HeaderText = "codPrestamoBancario";
		this.codPrestamoBancario.Name = "codPrestamoBancario";
		this.codPrestamoBancario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codPrestamoBancario.Visible = false;
		this.codMoneda.DataPropertyName = "codMoneda";
		this.codMoneda.HeaderText = "codMoneda";
		this.codMoneda.Name = "codMoneda";
		this.codMoneda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codMoneda.Visible = false;
		this.fechaemision.DataPropertyName = "fechaemision";
		this.fechaemision.HeaderText = "Fecha Emi.";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechaemision.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechaemision.Width = 80;
		this.fechavence.DataPropertyName = "fechavencimiento";
		dataGridViewCellStyle6.Format = "d";
		dataGridViewCellStyle6.NullValue = null;
		this.fechavence.DefaultCellStyle = dataGridViewCellStyle6;
		this.fechavence.HeaderText = "Fecha Venc.";
		this.fechavence.Name = "fechavence";
		this.fechavence.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.fechavence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechavence.Width = 80;
		this.fechacancelado.DataPropertyName = "fechacancelado";
		this.fechacancelado.HeaderText = "Fecha Canc.";
		this.fechacancelado.Name = "fechacancelado";
		this.fechacancelado.ReadOnly = true;
		this.fechacancelado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechacancelado.Width = 80;
		this.moneda.DataPropertyName = "descMoneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.tipoCambio.DataPropertyName = "tipoCambio";
		this.tipoCambio.HeaderText = "tipoCambio";
		this.tipoCambio.Name = "tipoCambio";
		this.tipoCambio.ReadOnly = true;
		this.tipoCambio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.tipoCambio.Width = 80;
		this.monto.DataPropertyName = "monto";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N2";
		this.monto.DefaultCellStyle = dataGridViewCellStyle7;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.monto.Width = 80;
		this.pendiente.DataPropertyName = "montopendiente";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N2";
		this.pendiente.DefaultCellStyle = dataGridViewCellStyle8;
		this.pendiente.HeaderText = "Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.pendiente.Width = 80;
		this.montoadicional.DataPropertyName = "montoadicional";
		this.montoadicional.HeaderText = "Adicional";
		this.montoadicional.Name = "montoadicional";
		this.montoadicional.ReadOnly = true;
		this.montoadicional.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montoadicional.Width = 80;
		this.cancelado.DataPropertyName = "cancelado";
		this.cancelado.HeaderText = "cancelado";
		this.cancelado.Name = "cancelado";
		this.cancelado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cancelado.Visible = false;
		this.cadCancelado.DataPropertyName = "cadCancelado";
		this.cadCancelado.HeaderText = "Estado";
		this.cadCancelado.Name = "cadCancelado";
		this.cadCancelado.ReadOnly = true;
		this.cadCancelado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(781, 466);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmCanjearCuota";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmCanjearCuota";
		base.Load += new System.EventHandler(frmCanjearCuota_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudCuotas).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvCuotas).EndInit();
		base.ResumeLayout(false);
	}
}
