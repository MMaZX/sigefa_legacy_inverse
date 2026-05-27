using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmConductores : Office2007Form
{
	private clsAdmConductor AdmCon = new clsAdmConductor();

	private clsValidar valida = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public clsConductor cond = new clsConductor();

	public int Proceso = 0;

	public int provieneguias = 0;

	public int tipodocumento = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvConductors;

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

	private TextBox txNombreApellido;

	private TextBox txtCodigo;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private ImageList imageList1;

	private Label label9;

	private TextBox txtLicencia;

	private Label label8;

	private TextBox txtDireccion;

	private Label label7;

	private TextBox txtRuc;

	private Label label6;

	private TextBox txtDni;

	private Label label10;

	private TextBox txtTelefono;

	private CustomValidator customValidator2;

	private CustomValidator customValidator3;

	private CustomValidator customValidator1;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn licencia;

	private DataGridViewTextBoxColumn direccion;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewTextBoxColumn documento;

	public frmConductores()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		if (provieneguias == 0)
		{
			tipodocumento = 2;
		}
		dgvConductors.DataSource = data;
		data.DataSource = AdmCon.MuestraConductores(tipodocumento);
		data.Filter = string.Empty;
		filtro = string.Empty;
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
		txNombreApellido.Text = "";
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
		cargaconductor();
	}

	private void cargaconductor()
	{
		cond = AdmCon.MuestraConductor(cond.CodConductor);
		if (cond != null)
		{
			txtCodigo.Text = cond.CodConductor.ToString();
			txtDni.Text = cond.Dni;
			txtRuc.Text = cond.Ruc;
			txNombreApellido.Text = cond.Nombre;
			txtLicencia.Text = cond.Licencia;
			txtTelefono.Text = cond.Telefono;
			txtDireccion.Text = cond.Direccion;
		}
	}

	private void frmConductor_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Codigo";
		label3.Text = "codConductor";
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
		if (dgvConductors.CurrentRow.Index != -1 && cond.CodConductor != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Conductor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmCon.delete(cond.CodConductor))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Conductor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		cond.Nombre = txNombreApellido.Text;
		cond.Dni = txtDni.Text;
		cond.Ruc = txtRuc.Text;
		cond.Nombre = txNombreApellido.Text;
		cond.Licencia = txtLicencia.Text;
		cond.Telefono = txtTelefono.Text;
		cond.Direccion = txtDireccion.Text;
		if (Proceso == 1)
		{
			cond.CodUser = frmLogin.iCodUser;
			if (AdmCon.insert(cond))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Conductor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
				Proceso = 0;
			}
		}
		else if (Proceso == 2 && AdmCon.update(cond))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Conductor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
			Proceso = 0;
		}
	}

	private void dgvConductors_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvConductors.Rows.Count >= 1 && e.Row.Selected)
		{
			cond.CodConductor = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvConductors.Rows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void dgvConductors_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvConductors.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvConductors.Columns[e.ColumnIndex].DataPropertyName;
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

	private void dgvConductors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			Close();
		}
		Close();
	}

	private void dgvConductors_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnGuardar.Enabled = true;
		}
		if (provieneguias != 1)
		{
			return;
		}
		try
		{
			if (dgvConductors.Rows.Count >= 1 && e.RowIndex != -1 && dgvConductors.CurrentRow.Index == e.RowIndex)
			{
				DataGridViewRow Row = dgvConductors.Rows[e.RowIndex];
				cond.CodConductor = Convert.ToInt32(Row.Cells[codigo.Name].Value);
				cond.Nombre = Row.Cells[nombre.Name].Value.ToString();
				cond.Dni = Row.Cells[dni.Name].Value.ToString();
				cond.Ruc = Row.Cells[ruc.Name].Value.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 10;
		frm.ShowDialog();
	}

	private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.enteros(e);
	}

	private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.enteros(e);
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void frmConductores_Shown(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmConductores));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvConductors = new System.Windows.Forms.DataGridView();
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
		this.label10 = new System.Windows.Forms.Label();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtLicencia = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtRuc = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtDni = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txNombreApellido = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.licencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvConductors).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.dgvConductors);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 198);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Conductor";
		this.dgvConductors.AllowUserToAddRows = false;
		this.dgvConductors.AllowUserToDeleteRows = false;
		this.dgvConductors.AllowUserToOrderColumns = true;
		this.dgvConductors.AllowUserToResizeColumns = false;
		this.dgvConductors.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvConductors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvConductors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvConductors.Columns.AddRange(this.codigo, this.nombre, this.estado, this.coduser, this.fecha, this.dni, this.ruc, this.licencia, this.direccion, this.telefono, this.documento);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvConductors.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvConductors.Location = new System.Drawing.Point(6, 45);
		this.dgvConductors.MultiSelect = false;
		this.dgvConductors.Name = "dgvConductors";
		this.dgvConductors.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvConductors.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvConductors.RowHeadersVisible = false;
		this.dgvConductors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvConductors.Size = new System.Drawing.Size(464, 147);
		this.dgvConductors.TabIndex = 8;
		this.dgvConductors.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvConductors_CellClick);
		this.dgvConductors.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvConductors_CellDoubleClick);
		this.dgvConductors.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvConductors_ColumnHeaderMouseClick);
		this.dgvConductors.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvConductors_RowStateChanged);
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
		this.groupBox3.Location = new System.Drawing.Point(12, 211);
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
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.txtTelefono);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtLicencia);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.txtDireccion);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.txtRuc);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.txtDni);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txNombreApellido);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Location = new System.Drawing.Point(47, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(400, 198);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(193, 100);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(55, 13);
		this.label10.TabIndex = 17;
		this.label10.Text = "Teléfono :";
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(196, 116);
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(128, 20);
		this.txtTelefono.TabIndex = 6;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(17, 100);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 13);
		this.label9.TabIndex = 15;
		this.label9.Text = "Licencia :";
		this.txtLicencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLicencia.Location = new System.Drawing.Point(20, 116);
		this.txtLicencia.Name = "txtLicencia";
		this.txtLicencia.Size = new System.Drawing.Size(128, 20);
		this.txtLicencia.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtLicencia, this.customValidator2);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(17, 139);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(58, 13);
		this.label8.TabIndex = 13;
		this.label8.Text = "Dirección :";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(20, 155);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(340, 20);
		this.txtDireccion.TabIndex = 7;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(229, 22);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(45, 13);
		this.label7.TabIndex = 11;
		this.label7.Text = "R.U.C. :";
		this.txtRuc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRuc.Location = new System.Drawing.Point(232, 38);
		this.txtRuc.MaxLength = 11;
		this.txtRuc.Name = "txtRuc";
		this.txtRuc.Size = new System.Drawing.Size(128, 20);
		this.txtRuc.TabIndex = 3;
		this.txtRuc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRuc_KeyPress);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(95, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(32, 13);
		this.label6.TabIndex = 9;
		this.label6.Text = "DNI :";
		this.txtDni.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDni.Location = new System.Drawing.Point(98, 38);
		this.txtDni.MaxLength = 8;
		this.txtDni.Name = "txtDni";
		this.txtDni.Size = new System.Drawing.Size(128, 20);
		this.txtDni.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtDni, this.customValidator3);
		this.txtDni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDni_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 61);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(108, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Nombres y Apellidos :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Código :";
		this.txNombreApellido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txNombreApellido.Location = new System.Drawing.Point(20, 77);
		this.txNombreApellido.Name = "txNombreApellido";
		this.txNombreApellido.Size = new System.Drawing.Size(340, 20);
		this.txNombreApellido.TabIndex = 4;
		this.superValidator1.SetValidator1(this.txNombreApellido, this.customValidator1);
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(20, 38);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(71, 20);
		this.txtCodigo.TabIndex = 1;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator2.ErrorMessage = "Ingrese la Licencia.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el DNI.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese el Nombre y Apellido.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.codigo.DataPropertyName = "codConductor";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 250;
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
		this.dni.DataPropertyName = "dni";
		this.dni.HeaderText = "DNI";
		this.dni.Name = "dni";
		this.dni.ReadOnly = true;
		this.dni.Visible = false;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "RUC";
		this.ruc.Name = "ruc";
		this.ruc.ReadOnly = true;
		this.ruc.Visible = false;
		this.licencia.DataPropertyName = "licencia";
		this.licencia.HeaderText = "Licencia";
		this.licencia.Name = "licencia";
		this.licencia.ReadOnly = true;
		this.licencia.Visible = false;
		this.direccion.DataPropertyName = "direccion";
		this.direccion.HeaderText = "Direccion";
		this.direccion.Name = "direccion";
		this.direccion.ReadOnly = true;
		this.direccion.Visible = false;
		this.telefono.DataPropertyName = "telefono";
		this.telefono.HeaderText = "Telefono";
		this.telefono.Name = "telefono";
		this.telefono.ReadOnly = true;
		this.telefono.Visible = false;
		this.documento.DataPropertyName = "documento";
		this.documento.HeaderText = "Documento";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 271);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmConductores";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Conductor";
		base.Load += new System.EventHandler(frmConductor_Load);
		base.Shown += new System.EventHandler(frmConductores_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvConductors).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
