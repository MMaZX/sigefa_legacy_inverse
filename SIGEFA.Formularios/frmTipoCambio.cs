using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmTipoCambio : Office2007Form
{
	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio Tc = new clsTipoCambio();

	private clsValidar ok = new clsValidar();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsMoneda Mon = new clsMoneda();

	private clsAdmUnidad admunidad = new clsAdmUnidad();

	public int Proceso = 0;

	private DataTable TipoCam = new DataTable();

	private DataTable tb_unidadequivalente = new DataTable();

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox1;

	private DataGridView dgvTipoCambio;

	private GroupBox groupBox3;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnReporte;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private DateTimePicker dtpFecha;

	private Label label6;

	private TextBox txtVenta;

	private Label label4;

	private Label label5;

	private TextBox txtCompra;

	private ComboBox cmbMes;

	private Label label1;

	private ComboBox cmbAño;

	private Label label2;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private Label lblMoneda;

	private ComboBox cmbMoneda;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn Moneda;

	private DataGridViewTextBoxColumn codmoneda;

	private DataGridViewTextBoxColumn compra;

	private DataGridViewTextBoxColumn venta;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private Button button1;

	private Label label3;

	private CustomValidator customValidator3;

	public Button btnSalir;

	public frmTipoCambio()
	{
		InitializeComponent();
	}

	public void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		cargaMoneda();
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		txtCompra.Text = "";
		txtVenta.Text = "";
		dtpFecha.Value = DateTime.Now.Date;
		superValidator1.Validate();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (dgvTipoCambio.Rows.Count >= 1 && dgvTipoCambio.SelectedRows.Count > 0)
		{
			CambiarEstados(Estado: false);
			cargaMoneda();
			groupBox2.Text = "Editar Registro";
			Proceso = 2;
			dtpFecha.Value = Tc.Fecha.Date;
			cmbMoneda.SelectedValue = Tc.ICodMoneda;
			txtCompra.Text = Tc.Compra.ToString();
			txtVenta.Text = Tc.Venta.ToString();
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvTipoCambio.Rows.Count >= 1 && dgvTipoCambio.SelectedRows.Count > 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Tipo de cambio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmTc.delete(Tc.CodTipoCambio))
			{
				MessageBox.Show("El Tipo de Cambio ha sido eliminado", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void CargaLista()
	{
		dgvTipoCambio.DataSource = AdmTc.MuestraTipoCambios(Convert.ToInt32(cmbMes.SelectedValue), Convert.ToInt32(cmbAño.SelectedValue));
		dgvTipoCambio.ClearSelection();
	}

	private void llenacombos()
	{
		DataTable dt = new DataTable("Tabla");
		dt.Columns.Add("Codigo");
		dt.Columns.Add("Descripcion");
		DataRow dr = dt.NewRow();
		dr[0] = "1";
		dr[1] = "ENERO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "2";
		dr[1] = "FEBRERO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "3";
		dr[1] = "MARZO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "4";
		dr[1] = "ABRIL";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "5";
		dr[1] = "MAYO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "6";
		dr[1] = "JUNIO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "7";
		dr[1] = "JULIO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "8";
		dr[1] = "AGOSTO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "9";
		dr[1] = "SETIEMBRE";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "10";
		dr[1] = "OCTUBRE";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "11";
		dr[1] = "NOVIEMBRE";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "12";
		dr[1] = "DICIEMBRE";
		dt.Rows.Add(dr);
		cmbMes.DataSource = dt;
		cmbMes.ValueMember = "Codigo";
		cmbMes.DisplayMember = "Descripcion";
		DataTable dt2 = new DataTable("Tabla1");
		dt2.Columns.Add("Codigo");
		dt2.Columns.Add("Descripcion");
		DataRow dr2 = dt2.NewRow();
		dr2[0] = "2013";
		dr2[1] = "2013";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2014";
		dr2[1] = "2014";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2015";
		dr2[1] = "2015";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2016";
		dr2[1] = "2016";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2017";
		dr2[1] = "2017";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2018";
		dr2[1] = "2018";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2019";
		dr2[1] = "2019";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2020";
		dr2[1] = "2020";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2021";
		dr2[1] = "2021";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2022";
		dr2[1] = "2022";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2023";
		dr2[1] = "2023";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2024";
		dr2[1] = "2024";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2025";
		dr2[1] = "2025";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2026";
		dr2[1] = "2026";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2027";
		dr2[1] = "2027";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2028";
		dr2[1] = "2028";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2029";
		dr2[1] = "2029";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2030";
		dr2[1] = "2030";
		dt2.Rows.Add(dr2);
		cmbAño.DataSource = dt2;
		cmbAño.ValueMember = "Codigo";
		cmbAño.DisplayMember = "Descripcion";
	}

	private void frmTipoCambio_Load(object sender, EventArgs e)
	{
		llenacombos();
		cmbMes.SelectedValue = DateTime.Now.Month;
		cmbAño.SelectedValue = DateTime.Now.Year;
		CargaLista();
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (groupBox1.Visible)
		{
			Dispose();
			return;
		}
		Proceso = 0;
		CambiarEstados(Estado: true);
	}

	private void dgvTipoCambio_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvTipoCambio.Rows.Count >= 1 && e.Row.Selected)
		{
			Tc.CodTipoCambio = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			Tc.Fecha = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			Tc.Compra = Convert.ToDouble(e.Row.Cells[compra.Name].Value);
			Tc.Venta = Convert.ToDouble(e.Row.Cells[venta.Name].Value);
			Tc.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			Tc.ICodMoneda = Convert.ToInt32(e.Row.Cells[codmoneda.Name].Value);
			Tc.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecharegistro.Name].Value);
			btnEditar.Enabled = true;
		}
		else if (dgvTipoCambio.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtCompra.Text != "") || !(txtVenta.Text != ""))
		{
			return;
		}
		Tc.Fecha = dtpFecha.Value.Date;
		Tc.Compra = Convert.ToDouble(txtCompra.Text);
		Tc.Venta = Convert.ToDouble(txtVenta.Text);
		if (label3.Text != "")
		{
			Tc.ICodMoneda = 2;
		}
		else
		{
			Tc.ICodMoneda = 2;
		}
		if (Proceso == 1)
		{
			Tc.CodUser = frmLogin.iCodUser;
			if (!AdmTc.VerificaTCFecha(dtpFecha.Value.Date))
			{
				if (AdmTc.insert(Tc))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					int validaMo = admunidad.ValidaMoneda();
					if (validaMo > 0 && !admunidad.ActualizaPrecioEnDolares())
					{
						MessageBox.Show("Error");
					}
					CambiarEstados(Estado: true);
					CargaLista();
				}
			}
			else
			{
				MessageBox.Show("Ya existe un Tipo de cambio registrado en esta fecha", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else if (Proceso == 2 && AdmTc.update(Tc))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
		}
		Proceso = 0;
	}

	private void cmbMes_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLista();
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

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataTable dt = new DataTable("TipoCambio");
		foreach (DataGridViewColumn column in dgvTipoCambio.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvTipoCambio.Rows.Count; i++)
		{
			DataGridViewRow row = dgvTipoCambio.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvTipoCambio.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmTipoCambioRP frm = new frmTipoCambioRP();
		frm.DTable = dt;
		frm.Show();
	}

	private void txtCompra_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtVenta_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		consultartipocambio(dtpFecha.Value);
	}

	private void consultartipocambio(DateTime Fechabuscada)
	{
		clsConsultasExternas ext = new clsConsultasExternas();
		TipoCam = ext.ConsultaTCSunat(Fechabuscada);
		if (TipoCam == null)
		{
			return;
		}
		string cadenabusqueda = "[Día] like '*" + Fechabuscada.Date.Day + "*'";
		DataRow[] foundRows = TipoCam.Select(cadenabusqueda);
		if (foundRows.Length != 0)
		{
			foreach (DataRow r in TipoCam.Rows)
			{
				if (Convert.ToInt32(r[0]) == Fechabuscada.Date.Day)
				{
					txtCompra.Text = r[1].ToString();
					txtVenta.Text = r[2].ToString();
					label3.Text = Fechabuscada.ToShortDateString();
					Mon = AdmMon.Buscamoneda("DOLARES AMERICANOS");
					cmbMoneda.SelectedValue = Mon.IcodMoneda;
				}
			}
			return;
		}
		consultartipocambio(Fechabuscada.AddDays(-1.0));
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0 && c.Visible)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTipoCambio));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbAño = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbMes = new System.Windows.Forms.ComboBox();
		this.dgvTipoCambio = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codmoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.compra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.lblMoneda = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label6 = new System.Windows.Forms.Label();
		this.txtVenta = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtCompra = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTipoCambio).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.cmbAño);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cmbMes);
		this.groupBox1.Controls.Add(this.dgvTipoCambio);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 10);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 306);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Tipo de Cambio";
		this.cmbAño.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAño.FormattingEnabled = true;
		this.cmbAño.Location = new System.Drawing.Point(111, 18);
		this.cmbAño.Name = "cmbAño";
		this.cmbAño.Size = new System.Drawing.Size(121, 21);
		this.cmbAño.TabIndex = 11;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(72, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(32, 13);
		this.label2.TabIndex = 10;
		this.label2.Text = "Año :";
		this.cmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMes.FormattingEnabled = true;
		this.cmbMes.Location = new System.Drawing.Point(277, 18);
		this.cmbMes.Name = "cmbMes";
		this.cmbMes.Size = new System.Drawing.Size(121, 21);
		this.cmbMes.TabIndex = 9;
		this.cmbMes.SelectionChangeCommitted += new System.EventHandler(cmbMes_SelectionChangeCommitted);
		this.dgvTipoCambio.AllowUserToAddRows = false;
		this.dgvTipoCambio.AllowUserToDeleteRows = false;
		this.dgvTipoCambio.AllowUserToResizeColumns = false;
		this.dgvTipoCambio.AllowUserToResizeRows = false;
		this.dgvTipoCambio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTipoCambio.Columns.AddRange(this.codigo, this.fecha, this.Moneda, this.codmoneda, this.compra, this.venta, this.estado, this.coduser, this.fecharegistro);
		this.dgvTipoCambio.Location = new System.Drawing.Point(0, 45);
		this.dgvTipoCambio.MultiSelect = false;
		this.dgvTipoCambio.Name = "dgvTipoCambio";
		this.dgvTipoCambio.ReadOnly = true;
		this.dgvTipoCambio.RowHeadersVisible = false;
		this.dgvTipoCambio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTipoCambio.Size = new System.Drawing.Size(470, 255);
		this.dgvTipoCambio.TabIndex = 8;
		this.dgvTipoCambio.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvTipoCambio_RowStateChanged);
		this.codigo.DataPropertyName = "codTipoCambio";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.Format = "d";
		dataGridViewCellStyle1.NullValue = null;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle1;
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Moneda.DataPropertyName = "descripcion";
		this.Moneda.HeaderText = "Moneda";
		this.Moneda.Name = "Moneda";
		this.Moneda.ReadOnly = true;
		this.Moneda.Width = 150;
		this.codmoneda.DataPropertyName = "codMoneda";
		this.codmoneda.HeaderText = "codmoneda";
		this.codmoneda.Name = "codmoneda";
		this.codmoneda.ReadOnly = true;
		this.codmoneda.Visible = false;
		this.compra.DataPropertyName = "compra";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N3";
		this.compra.DefaultCellStyle = dataGridViewCellStyle2;
		this.compra.HeaderText = "Compra";
		this.compra.Name = "compra";
		this.compra.ReadOnly = true;
		this.compra.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.compra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.venta.DataPropertyName = "venta";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N3";
		this.venta.DefaultCellStyle = dataGridViewCellStyle3;
		this.venta.HeaderText = "Venta";
		this.venta.Name = "venta";
		this.venta.ReadOnly = true;
		this.venta.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.venta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "FechaReg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecharegistro.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.fecharegistro.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(238, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(33, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Mes :";
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Location = new System.Drawing.Point(12, 317);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(476, 48);
		this.groupBox3.TabIndex = 18;
		this.groupBox3.TabStop = false;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(403, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 11;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 10);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(320, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 10;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.Enabled = false;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 10);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 7;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(236, 10);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 9;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(155, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Visible = false;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.button1);
		this.groupBox2.Controls.Add(this.lblMoneda);
		this.groupBox2.Controls.Add(this.cmbMoneda);
		this.groupBox2.Controls.Add(this.dtpFecha);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.txtVenta);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtCompra);
		this.groupBox2.Location = new System.Drawing.Point(96, 72);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(314, 238);
		this.groupBox2.TabIndex = 21;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Tipo de Cambio";
		this.groupBox2.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
		this.label3.Location = new System.Drawing.Point(130, 210);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(0, 24);
		this.label3.TabIndex = 13;
		this.button1.Location = new System.Drawing.Point(103, 171);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(127, 23);
		this.button1.TabIndex = 12;
		this.button1.Text = "Consulta Dólares";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.lblMoneda.AutoSize = true;
		this.lblMoneda.Location = new System.Drawing.Point(81, 22);
		this.lblMoneda.Name = "lblMoneda";
		this.lblMoneda.Size = new System.Drawing.Size(52, 13);
		this.lblMoneda.TabIndex = 11;
		this.lblMoneda.Text = "Moneda :";
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(148, 19);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(127, 21);
		this.cmbMoneda.TabIndex = 10;
		this.superValidator1.SetValidator1(this.cmbMoneda, this.customValidator3);
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(148, 60);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(127, 20);
		this.dtpFecha.TabIndex = 1;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(81, 137);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 9;
		this.label6.Text = "Venta :";
		this.txtVenta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtVenta.Location = new System.Drawing.Point(148, 134);
		this.txtVenta.Name = "txtVenta";
		this.txtVenta.Size = new System.Drawing.Size(127, 20);
		this.txtVenta.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtVenta, this.customValidator2);
		this.txtVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtVenta_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(81, 97);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(49, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Compra :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(81, 64);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(43, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Fecha :";
		this.txtCompra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCompra.Location = new System.Drawing.Point(148, 97);
		this.txtCompra.Name = "txtCompra";
		this.txtCompra.Size = new System.Drawing.Size(127, 20);
		this.txtCompra.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtCompra, this.customValidator1);
		this.txtCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCompra_KeyPress);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator3.ErrorMessage = "Seleccione Moneda.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese el valor venta.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese el valor compra.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(540, 397);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTipoCambio";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Tipo de Cambio";
		base.Load += new System.EventHandler(frmTipoCambio_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTipoCambio).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
