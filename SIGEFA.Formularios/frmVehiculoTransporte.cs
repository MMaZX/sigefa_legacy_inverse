using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmVehiculoTransporte : Office2007Form
{
	private clsAdmVehiculoTransporte AdmVT = new clsAdmVehiculoTransporte();

	private clsAdmMarcaTransporte admMT = new clsAdmMarcaTransporte();

	private clsAdmModeloTransporte admMoT = new clsAdmModeloTransporte();

	private clsValidar valida = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public clsVehiculoTransporte veh = new clsVehiculoTransporte();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvVehiculoTransporte;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnReporte;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private Label label4;

	private Label label5;

	private TextBox txtConstancia;

	private TextBox txtCodigo;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private ImageList imageList1;

	private Label label7;

	private TextBox txtPlaca;

	private CustomValidator customValidator2;

	private CustomValidator customValidator3;

	private Label label9;

	private TextBox txtAño;

	private ComboBox cmbModelo;

	private Label label8;

	private ComboBox cmbMarca;

	private Label label6;

	private CustomValidator customValidator6;

	private CustomValidator customValidator5;

	private CustomValidator customValidator1;

	private CustomValidator customValidator4;

	private Label lbSoat;

	private TextBox txtSoat;

	private Label lbConfVehicular;

	private TextBox txtConfVehicular;

	private TextBox txtmtc;

	private Label label10;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn placa;

	private DataGridViewTextBoxColumn codmarca;

	private DataGridViewTextBoxColumn marca;

	private DataGridViewTextBoxColumn codmodelo;

	private DataGridViewTextBoxColumn modelo;

	private DataGridViewTextBoxColumn constancia;

	private DataGridViewTextBoxColumn año;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn SOAT;

	private DataGridViewTextBoxColumn confVehicular;

	private DataGridViewTextBoxColumn mtc;

	public frmVehiculoTransporte()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvVehiculoTransporte.DataSource = data;
		data.DataSource = AdmVT.MuestraVehiculoTransportes();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvVehiculoTransporte.ClearSelection();
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
		txtCodigo.Text = "";
		txtConstancia.Text = "";
		superValidator1.Validate();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
		ext.limpiar(groupBox2.Controls);
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		ext.limpiar(groupBox2.Controls);
		CambiarEstados(Estado: false);
		groupBox2.Text = "Editar Registro";
		Proceso = 2;
		cargaVehiculoTransporte();
	}

	private void cargaVehiculoTransporte()
	{
		veh = AdmVT.MuestraVehiculoTransporte(veh.CodVehiculoTransporte);
		if (veh != null)
		{
			txtCodigo.Text = veh.CodVehiculoTransporte.ToString();
			txtAño.Text = veh.Año.ToString();
			txtPlaca.Text = veh.Placa;
			txtConstancia.Text = veh.ConstanciaInscripcion;
			cmbMarca.SelectedValue = veh.CodMarca;
			CargaModelos(veh.CodMarca);
			cmbModelo.Enabled = true;
			cmbModelo.SelectedValue = veh.CodModelo;
			txtSoat.Text = veh.Soat;
			txtConfVehicular.Text = veh.ConfVehicular;
			txtmtc.Text = veh.MTC;
		}
	}

	private void frmVehiculoTransporte_Load(object sender, EventArgs e)
	{
		CargaLista();
		CargaMarcas();
		label2.Text = "Codigo";
		label3.Text = "codVehiculoTransporte";
		if (Proceso == 3)
		{
			bloquearbotones();
		}
	}

	private void bloquearbotones()
	{
		btnNuevo.Visible = false;
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
		btnReporte.Visible = false;
		btnGuardar.Text = "Aceptar";
		btnGuardar.ImageIndex = 6;
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvVehiculoTransporte.CurrentRow.Index != -1 && veh.CodVehiculoTransporte != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "VehiculoTransporte", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmVT.delete(veh.CodVehiculoTransporte))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "VehiculoTransporte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (groupBox1.Visible)
		{
			Close();
			return;
		}
		Proceso = 0;
		CambiarEstados(Estado: true);
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0)
		{
			return;
		}
		veh.Placa = txtPlaca.Text;
		veh.ConstanciaInscripcion = txtConstancia.Text;
		veh.Año = Convert.ToInt32(txtAño.Text);
		veh.CodMarca = Convert.ToInt32(cmbMarca.SelectedValue);
		veh.CodModelo = Convert.ToInt32(cmbModelo.SelectedValue);
		veh.ConfVehicular = txtConfVehicular.Text;
		veh.Soat = txtSoat.Text;
		veh.MTC = txtmtc.Text;
		if (Proceso == 1)
		{
			veh.CodUser = frmLogin.iCodUser;
			if (AdmVT.insert(veh))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion VehiculoTransporte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
				Proceso = 0;
			}
		}
		else if (Proceso == 2 && AdmVT.update(veh))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion VehiculoTransporte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
			Proceso = 0;
		}
	}

	private void dgvVehiculoTransportes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvVehiculoTransporte.Rows.Count >= 1 && e.Row.Selected && e.Row != null)
		{
			veh.CodVehiculoTransporte = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvVehiculoTransporte.Rows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void dgvVehiculoTransportes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvVehiculoTransporte.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvVehiculoTransporte.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label3.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
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

	private void dgvVehiculoTransportes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			Close();
		}
	}

	private void dgvVehiculoTransportes_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 9;
		frm.ShowDialog();
	}

	private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.enteros(e);
	}

	private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
	{
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

	private void txtAño_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.enteros(e);
	}

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void cmbMarca_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaModelos(Convert.ToInt32(cmbMarca.SelectedValue));
		cmbModelo.Enabled = true;
	}

	private void CargaMarcas()
	{
		cmbMarca.DataSource = admMT.MuestraMarcaTransportes();
		cmbMarca.DisplayMember = "descripcion";
		cmbMarca.ValueMember = "codMarcaTransporte";
		cmbMarca.SelectedIndex = -1;
	}

	private void CargaModelos(int CodMar)
	{
		cmbModelo.DataSource = admMoT.MuestraModeloTransportes(CodMar);
		cmbModelo.DisplayMember = "descripcion";
		cmbModelo.ValueMember = "codModeloTransporte";
		cmbModelo.SelectedIndex = -1;
	}

	private void frmVehiculoTransporte_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVehiculoTransporte));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvVehiculoTransporte = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.placa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codmarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codmodelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.constancia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.año = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.SOAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.confVehicular = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.mtc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtmtc = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.lbSoat = new System.Windows.Forms.Label();
		this.txtSoat = new System.Windows.Forms.TextBox();
		this.lbConfVehicular = new System.Windows.Forms.Label();
		this.txtConfVehicular = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtAño = new System.Windows.Forms.TextBox();
		this.cmbModelo = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.cmbMarca = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtPlaca = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtConstancia = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVehiculoTransporte).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.dgvVehiculoTransporte);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 223);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Vehiculos Transporte";
		this.dgvVehiculoTransporte.AllowUserToAddRows = false;
		this.dgvVehiculoTransporte.AllowUserToDeleteRows = false;
		this.dgvVehiculoTransporte.AllowUserToOrderColumns = true;
		this.dgvVehiculoTransporte.AllowUserToResizeColumns = false;
		this.dgvVehiculoTransporte.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVehiculoTransporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvVehiculoTransporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvVehiculoTransporte.Columns.AddRange(this.codigo, this.placa, this.codmarca, this.marca, this.codmodelo, this.modelo, this.constancia, this.año, this.estado, this.coduser, this.fecha, this.SOAT, this.confVehicular, this.mtc);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvVehiculoTransporte.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvVehiculoTransporte.Location = new System.Drawing.Point(6, 45);
		this.dgvVehiculoTransporte.MultiSelect = false;
		this.dgvVehiculoTransporte.Name = "dgvVehiculoTransporte";
		this.dgvVehiculoTransporte.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVehiculoTransporte.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvVehiculoTransporte.RowHeadersVisible = false;
		this.dgvVehiculoTransporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVehiculoTransporte.Size = new System.Drawing.Size(464, 147);
		this.dgvVehiculoTransporte.TabIndex = 8;
		this.dgvVehiculoTransporte.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVehiculoTransportes_CellDoubleClick);
		this.dgvVehiculoTransporte.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvVehiculoTransportes_ColumnHeaderMouseClick);
		this.dgvVehiculoTransporte.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVehiculoTransportes_CellClick);
		this.dgvVehiculoTransporte.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvVehiculoTransportes_RowStateChanged);
		this.codigo.DataPropertyName = "codVehiculoTransporte";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 80;
		this.placa.DataPropertyName = "placa";
		this.placa.HeaderText = "Placa";
		this.placa.Name = "placa";
		this.placa.ReadOnly = true;
		this.codmarca.DataPropertyName = "codmarca";
		this.codmarca.HeaderText = "Cod. Marca";
		this.codmarca.Name = "codmarca";
		this.codmarca.ReadOnly = true;
		this.codmarca.Visible = false;
		this.marca.DataPropertyName = "marca";
		this.marca.HeaderText = "Marca";
		this.marca.Name = "marca";
		this.marca.ReadOnly = true;
		this.marca.Width = 120;
		this.codmodelo.DataPropertyName = "codmodelo";
		this.codmodelo.HeaderText = "Cod. Modelo";
		this.codmodelo.Name = "codmodelo";
		this.codmodelo.ReadOnly = true;
		this.codmodelo.Visible = false;
		this.modelo.DataPropertyName = "modelo";
		this.modelo.HeaderText = "Modelo";
		this.modelo.Name = "modelo";
		this.modelo.ReadOnly = true;
		this.modelo.Width = 120;
		this.constancia.DataPropertyName = "constanciainscripcion";
		this.constancia.HeaderText = "Constancia";
		this.constancia.Name = "constancia";
		this.constancia.ReadOnly = true;
		this.constancia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.constancia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.año.DataPropertyName = "añofabricacion";
		this.año.HeaderText = "A. Fab.";
		this.año.Name = "año";
		this.año.ReadOnly = true;
		this.año.Visible = false;
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
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coduser.Visible = false;
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaReg";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fecha.Visible = false;
		this.SOAT.DataPropertyName = "SOAT";
		this.SOAT.HeaderText = "SOAT";
		this.SOAT.Name = "SOAT";
		this.SOAT.ReadOnly = true;
		this.confVehicular.DataPropertyName = "confVehicular";
		this.confVehicular.HeaderText = "confVehicular";
		this.confVehicular.Name = "confVehicular";
		this.confVehicular.ReadOnly = true;
		this.confVehicular.Visible = false;
		this.mtc.DataPropertyName = "mtc";
		this.mtc.HeaderText = "mtc";
		this.mtc.Name = "mtc";
		this.mtc.ReadOnly = true;
		this.mtc.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(427, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(96, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(200, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Location = new System.Drawing.Point(12, 231);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(476, 48);
		this.groupBox3.TabIndex = 18;
		this.groupBox3.TabStop = false;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(403, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 10;
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
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
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
		this.btnGuardar.TabIndex = 11;
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
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.txtmtc);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.lbSoat);
		this.groupBox2.Controls.Add(this.txtSoat);
		this.groupBox2.Controls.Add(this.lbConfVehicular);
		this.groupBox2.Controls.Add(this.txtConfVehicular);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtAño);
		this.groupBox2.Controls.Add(this.cmbModelo);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.cmbMarca);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.txtPlaca);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtConstancia);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Location = new System.Drawing.Point(89, 21);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(327, 214);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.txtmtc.AcceptsReturn = true;
		this.txtmtc.Location = new System.Drawing.Point(6, 189);
		this.txtmtc.Name = "txtmtc";
		this.txtmtc.Size = new System.Drawing.Size(131, 20);
		this.txtmtc.TabIndex = 24;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(6, 173);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(33, 13);
		this.label10.TabIndex = 23;
		this.label10.Text = "MTC:";
		this.lbSoat.AutoSize = true;
		this.lbSoat.Location = new System.Drawing.Point(6, 134);
		this.lbSoat.Name = "lbSoat";
		this.lbSoat.Size = new System.Drawing.Size(39, 13);
		this.lbSoat.TabIndex = 22;
		this.lbSoat.Text = "SOAT:";
		this.txtSoat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSoat.Location = new System.Drawing.Point(9, 150);
		this.txtSoat.MaxLength = 11;
		this.txtSoat.Name = "txtSoat";
		this.txtSoat.Size = new System.Drawing.Size(128, 20);
		this.txtSoat.TabIndex = 19;
		this.superValidator1.SetValidator1(this.txtSoat, this.customValidator4);
		this.lbConfVehicular.AutoSize = true;
		this.lbConfVehicular.Location = new System.Drawing.Point(151, 134);
		this.lbConfVehicular.Name = "lbConfVehicular";
		this.lbConfVehicular.Size = new System.Drawing.Size(122, 13);
		this.lbConfVehicular.TabIndex = 21;
		this.lbConfVehicular.Text = "Configuración Vehicular:";
		this.txtConfVehicular.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtConfVehicular.Location = new System.Drawing.Point(154, 150);
		this.txtConfVehicular.Name = "txtConfVehicular";
		this.txtConfVehicular.Size = new System.Drawing.Size(128, 20);
		this.txtConfVehicular.TabIndex = 20;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(151, 16);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(90, 13);
		this.label9.TabIndex = 18;
		this.label9.Text = "Año Fabricación :";
		this.txtAño.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtAño.Location = new System.Drawing.Point(154, 32);
		this.txtAño.MaxLength = 4;
		this.txtAño.Name = "txtAño";
		this.txtAño.Size = new System.Drawing.Size(80, 20);
		this.txtAño.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtAño, this.customValidator1);
		this.txtAño.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtAño_KeyPress);
		this.cmbModelo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbModelo.Enabled = false;
		this.cmbModelo.FormattingEnabled = true;
		this.cmbModelo.Items.AddRange(new object[2] { "DNI", "RUC" });
		this.cmbModelo.Location = new System.Drawing.Point(154, 110);
		this.cmbModelo.Name = "cmbModelo";
		this.cmbModelo.Size = new System.Drawing.Size(128, 21);
		this.cmbModelo.TabIndex = 16;
		this.cmbModelo.Tag = "6";
		this.superValidator1.SetValidator1(this.cmbModelo, this.customValidator6);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(151, 94);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(42, 13);
		this.label8.TabIndex = 15;
		this.label8.Text = "Modelo";
		this.cmbMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbMarca.FormattingEnabled = true;
		this.cmbMarca.Items.AddRange(new object[2] { "DNI", "RUC" });
		this.cmbMarca.Location = new System.Drawing.Point(9, 110);
		this.cmbMarca.Name = "cmbMarca";
		this.cmbMarca.Size = new System.Drawing.Size(128, 21);
		this.cmbMarca.TabIndex = 5;
		this.cmbMarca.Tag = "5";
		this.superValidator1.SetValidator1(this.cmbMarca, this.customValidator5);
		this.cmbMarca.SelectionChangeCommitted += new System.EventHandler(cmbMarca_SelectionChangeCommitted);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 94);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(37, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Marca";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(6, 55);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(40, 13);
		this.label7.TabIndex = 11;
		this.label7.Text = "Placa :";
		this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtPlaca.Location = new System.Drawing.Point(9, 71);
		this.txtPlaca.MaxLength = 11;
		this.txtPlaca.Name = "txtPlaca";
		this.txtPlaca.Size = new System.Drawing.Size(128, 20);
		this.txtPlaca.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtPlaca, this.customValidator4);
		this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRuc_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(151, 55);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(134, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Constancia de inscripcion :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Código :";
		this.txtConstancia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtConstancia.Location = new System.Drawing.Point(154, 71);
		this.txtConstancia.Name = "txtConstancia";
		this.txtConstancia.Size = new System.Drawing.Size(128, 20);
		this.txtConstancia.TabIndex = 4;
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(9, 32);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(80, 20);
		this.txtCodigo.TabIndex = 1;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator4.ErrorMessage = "Ingrese la Placa.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese Año Fabricación.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator6.ErrorMessage = "Seleccione el modelo.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator5.ErrorMessage = "Seleccione la marca.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator2.ErrorMessage = "Ingrese la Licencia.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el DNI.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 291);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVehiculoTransporte";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Vehiculos Transporte";
		base.Load += new System.EventHandler(frmVehiculoTransporte_Load);
		base.Shown += new System.EventHandler(frmVehiculoTransporte_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVehiculoTransporte).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
