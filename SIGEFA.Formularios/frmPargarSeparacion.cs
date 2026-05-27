using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmPargarSeparacion : OfficeForm
{
	public int codSeparacion;

	private clsAdmSeparacion admSepa = new clsAdmSeparacion();

	private clsSeparacion sepa = new clsSeparacion();

	private clsCuotasSeparacion cuotasepa = new clsCuotasSeparacion();

	private clsAdmCuotaSeparacion admcuotas = new clsAdmCuotaSeparacion();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsAdmTipoDocumento admDoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	public int Proceso = 0;

	private double SumAbono = 0.0;

	private int idValor = 0;

	public int CodDocumento = 0;

	private clsValidar ok = new clsValidar();

	public int CodSerie = 0;

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsSerie ser = new clsSerie();

	public int numSerie = 0;

	private IContainer components = null;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Label label2;

	private TextBox txtTotal;

	private TextBox txtCliente;

	private Label label3;

	private TextBox txtDocumento;

	private Label label4;

	private TextBox txtSerie;

	private TextBox txtNumDocumento;

	private Button btnGuardar;

	private ImageList imageList1;

	private DataGridView dgvCuotas;

	private TextBox txtAbonar;

	private Label label5;

	public ComboBox cmbMoneda;

	private Label label6;

	private TextBox txtCodDocumento;

	private DataGridViewTextBoxColumn codAbono;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn Documento;

	public frmPargarSeparacion()
	{
		InitializeComponent();
	}

	private void frmPargarSeparacion_Load(object sender, EventArgs e)
	{
		if (codSeparacion != 0)
		{
			CargaMoneda();
			CargarDatos();
			DataTable dt = CargaTablaDatos();
			DataRow row = dt.NewRow();
			dt.Rows.Add(row);
			dgvCuotas.AutoGenerateColumns = false;
			dgvCuotas.DataSource = dt;
			DataGridViewRow rowtotal = dgvCuotas.Rows[dgvCuotas.Rows.Count - 1];
			rowtotal.Cells["Monto"].Value = 0;
			CalcularTotal();
		}
	}

	private void CargaMoneda()
	{
		dtpFecha.MaxDate = DateTime.Today.Date;
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 0;
	}

	private void CalcularTotal()
	{
		double resul = Enumerable.Sum<DataGridViewRow>(dgvCuotas.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, double>)((DataGridViewRow x) => Convert.ToDouble(x.Cells["Monto"].Value)));
		DataGridViewRow rowtotal = dgvCuotas.Rows[dgvCuotas.Rows.Count - 1];
		rowtotal.Cells["Monto"].Value = resul;
		SumAbono = resul;
		if (resul == Convert.ToDouble(txtTotal.Text))
		{
			btnGuardar.Enabled = false;
			rowtotal.Cells["Monto"].Style.BackColor = Color.Red;
		}
		else
		{
			rowtotal.Cells["Monto"].Style.BackColor = Color.LightCyan;
		}
	}

	private DataTable CargaTablaDatos()
	{
		DataTable dt = new DataTable();
		return admcuotas.CargaCuotas(codSeparacion);
	}

	private void CargarDatos()
	{
		sepa = admSepa.BuscarSeparacion(codSeparacion, frmLogin.iCodAlmacen);
		txtDocumento.Text = sepa.Sigla;
		txtNumDocumento.Text = sepa.NumDocumento;
		txtSerie.Text = sepa.Serie;
		txtCliente.Text = sepa.NomCliente;
		txtTotal.Text = sepa.Total.ToString();
		CargarAbonos(codSeparacion);
	}

	private void CargarAbonos(int codSeparacion)
	{
		dgvCuotas.DataSource = admcuotas.CargaCuotas(codSeparacion);
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			double total = 0.0;
			double pendiente = 0.0;
			DialogResult boton = MessageBox.Show("Desea Guardar abono?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (boton != DialogResult.OK || Proceso != 1)
			{
				return;
			}
			if (txtAbonar.Text != "")
			{
				total = Convert.ToDouble(txtTotal.Text);
				pendiente = total - SumAbono;
				if (Convert.ToDouble(txtAbonar.Text) <= pendiente)
				{
					cuotasepa.CodSeparacion = codSeparacion;
					cuotasepa.FechaRegistro = dtpFecha.Value;
					cuotasepa.Monto = Convert.ToDecimal(txtAbonar.Text);
					cuotasepa.CodUsuario = frmLogin.iCodUser;
					cuotasepa.Total = Convert.ToDecimal(txtTotal.Text);
					cuotasepa.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					cuotasepa.TipoCambio = Convert.ToDecimal(tc.Venta);
					cuotasepa.CodAlmacen = frmLogin.iCodAlmacen;
					if (txtSerie.Text != "" && txtDocumento.Text != "")
					{
						cuotasepa.Serie = txtSerie.Text;
						cuotasepa.NumDocumento = txtNumDocumento.Text;
						cuotasepa.CodTipoDocumento = Convert.ToInt32(txtCodDocumento.Text);
						cuotasepa.Desdocumento = txtDocumento.Text;
						cuotasepa.CodSerie = CodSerie;
						if (admcuotas.insert(cuotasepa))
						{
							MessageBoxEx.Show("Se Guardo de forma correcta: ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							Close();
						}
						else
						{
							MessageBoxEx.Show("Ha ocurrido un problema: ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							Close();
						}
					}
					else
					{
						MessageBoxEx.Show("Debe Expecificar El documento: ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				else
				{
					MessageBox.Show("El monto abonado supera al total: ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBoxEx.Show("Ingrese el monto a Abonar ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvCuotas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		DialogResult boton = MessageBox.Show("Desea Elimnar abono?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
		if (boton == DialogResult.OK)
		{
			idValor = Convert.ToInt32(dgvCuotas.Rows[e.RowIndex].Cells["CodAbono"].Value.ToString());
			if (admcuotas.delete(idValor))
			{
				MessageBoxEx.Show("Se elimino de forma correcta: ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
				CargarAbonos(cuotasepa.CodSeparacion);
			}
		}
	}

	private void txtDocumento_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Activate();
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.Proceso = 3;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtCodDocumento.Text = CodDocumento.ToString();
		txtDocumento.Text = doc.Sigla;
		if (CodDocumento != 0)
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtDocumento.Text != "")
		{
			if (BuscaTipoDocumento())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaTipoDocumento()
	{
		doc = admDoc.BuscaTipoDocumento(txtDocumento.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			txtCodDocumento.Text = CodDocumento.ToString();
			return true;
		}
		CodDocumento = 0;
		txtCodDocumento.Text = CodDocumento.ToString();
		return false;
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && BuscaSerie())
		{
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumDocumento.Visible = true;
				txtNumDocumento.Enabled = false;
				txtNumDocumento.Focus();
				txtNumDocumento.Text = "";
			}
			else
			{
				txtNumDocumento.Text = "";
				txtNumDocumento.Enabled = false;
				txtNumDocumento.Text = ser.Numeracion.ToString();
			}
			ProcessTabKey(forward: true);
		}
	}

	private bool BuscaSerie()
	{
		ser = Admser.BuscaSeriexDocumento(CodDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
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
		if (CodSerie != 0)
		{
			txtSerie.Text = ser.Serie;
			txtNumDocumento.Text = ser.Numeracion.ToString();
		}
		if (CodSerie != 0)
		{
			ProcessTabKey(forward: true);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPargarSeparacion));
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtTotal = new System.Windows.Forms.TextBox();
		this.txtCliente = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtDocumento = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtNumDocumento = new System.Windows.Forms.TextBox();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.dgvCuotas = new System.Windows.Forms.DataGridView();
		this.txtAbonar = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtCodDocumento = new System.Windows.Forms.TextBox();
		this.codAbono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvCuotas).BeginInit();
		base.SuspendLayout();
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(154, 6);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(106, 20);
		this.dtpFecha.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(29, 13);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(88, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Fecha De Abono";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(29, 90);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(64, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Monto Total";
		this.txtTotal.Location = new System.Drawing.Point(154, 87);
		this.txtTotal.Name = "txtTotal";
		this.txtTotal.ReadOnly = true;
		this.txtTotal.Size = new System.Drawing.Size(100, 20);
		this.txtTotal.TabIndex = 3;
		this.txtCliente.Location = new System.Drawing.Point(154, 61);
		this.txtCliente.Name = "txtCliente";
		this.txtCliente.ReadOnly = true;
		this.txtCliente.Size = new System.Drawing.Size(196, 20);
		this.txtCliente.TabIndex = 4;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(29, 61);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 13);
		this.label3.TabIndex = 5;
		this.label3.Text = "Cliente";
		this.txtDocumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocumento.Location = new System.Drawing.Point(154, 33);
		this.txtDocumento.Name = "txtDocumento";
		this.txtDocumento.Size = new System.Drawing.Size(39, 20);
		this.txtDocumento.TabIndex = 6;
		this.txtDocumento.Text = "RC";
		this.txtDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocumento_KeyDown);
		this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocumento_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(29, 39);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(62, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Documento";
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Location = new System.Drawing.Point(201, 32);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.Size = new System.Drawing.Size(59, 20);
		this.txtSerie.TabIndex = 8;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtNumDocumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumDocumento.Location = new System.Drawing.Point(266, 32);
		this.txtNumDocumento.Name = "txtNumDocumento";
		this.txtNumDocumento.Size = new System.Drawing.Size(42, 20);
		this.txtNumDocumento.TabIndex = 9;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(32, 178);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(85, 31);
		this.btnGuardar.TabIndex = 10;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.dgvCuotas.AllowUserToAddRows = false;
		this.dgvCuotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCuotas.Columns.AddRange(this.codAbono, this.fecha, this.monto, this.Documento);
		this.dgvCuotas.Location = new System.Drawing.Point(22, 224);
		this.dgvCuotas.Name = "dgvCuotas";
		this.dgvCuotas.Size = new System.Drawing.Size(325, 225);
		this.dgvCuotas.TabIndex = 11;
		this.dgvCuotas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCuotas_CellContentDoubleClick);
		this.txtAbonar.Location = new System.Drawing.Point(154, 113);
		this.txtAbonar.Name = "txtAbonar";
		this.txtAbonar.Size = new System.Drawing.Size(100, 20);
		this.txtAbonar.TabIndex = 13;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(29, 116);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(41, 13);
		this.label5.TabIndex = 12;
		this.label5.Text = "Abonar";
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(154, 139);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(89, 20);
		this.cmbMoneda.TabIndex = 32;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(29, 146);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(46, 13);
		this.label6.TabIndex = 34;
		this.label6.Text = "Moneda";
		this.txtCodDocumento.Location = new System.Drawing.Point(314, 32);
		this.txtCodDocumento.Name = "txtCodDocumento";
		this.txtCodDocumento.Size = new System.Drawing.Size(39, 20);
		this.txtCodDocumento.TabIndex = 105;
		this.txtCodDocumento.Visible = false;
		this.codAbono.DataPropertyName = "codAbono";
		this.codAbono.HeaderText = "CodAbono";
		this.codAbono.Name = "codAbono";
		this.codAbono.Visible = false;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.monto.DataPropertyName = "monto";
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.Documento.DataPropertyName = "documento";
		this.Documento.HeaderText = "Documento";
		this.Documento.Name = "Documento";
		base.ClientSize = new System.Drawing.Size(362, 461);
		base.Controls.Add(this.txtCodDocumento);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.cmbMoneda);
		base.Controls.Add(this.txtAbonar);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dgvCuotas);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.txtNumDocumento);
		base.Controls.Add(this.txtSerie);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.txtDocumento);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.txtCliente);
		base.Controls.Add(this.txtTotal);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dtpFecha);
		this.DoubleBuffered = true;
		base.Name = "frmPargarSeparacion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Abonando Separacion";
		base.Load += new System.EventHandler(frmPargarSeparacion_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvCuotas).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
