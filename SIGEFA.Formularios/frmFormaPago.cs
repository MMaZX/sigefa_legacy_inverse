using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmFormaPago : Office2007Form
{
	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	public clsFormaPago pago = new clsFormaPago();

	public int Proceso = 0;

	public int Procedencia = 0;

	private clsValidar ok = new clsValidar();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox1;

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

	private TextBox txtDescripcion;

	private TextBox txtDias;

	private DataGridView dgvFormaPagos;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private ComboBox cmbTipo;

	private Label label6;

	private CustomValidator customValidator3;

	private ComboBox cmbTipoAccion;

	private Label label7;

	private CustomValidator customValidator4;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn dias;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn tipoa;

	private DataGridViewTextBoxColumn codtipoaccion;

	public frmFormaPago()
	{
		InitializeComponent();
	}

	private void frmFormaPago_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Descripcion";
		label3.Text = "descripcion";
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

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
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
		txtDias.Text = "";
		txtDescripcion.Text = "";
		cmbTipo.SelectedIndex = -1;
		superValidator1.Validate();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Editar Registro";
		Proceso = 2;
		txtDias.Text = pago.Dias.ToString();
		txtDescripcion.Text = pago.Descripcion;
		cmbTipo.SelectedIndex = Convert.ToInt32(pago.Tipo);
		cmbTipoAccion.SelectedIndex = Convert.ToInt32(pago.Tipoaccion);
	}

	private void CargaLista()
	{
		dgvFormaPagos.DataSource = data;
		data.DataSource = AdmPago.MuestraFormaPagos();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvFormaPagos.ClearSelection();
	}

	private void frmFormaPago_Shown(object sender, EventArgs e)
	{
		CargaLista();
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
		if (btnGuardar.Text == "Guardar")
		{
			if (!superValidator1.Validate() || Proceso == 0 || !(txtDias.Text != "") || !(txtDescripcion.Text != ""))
			{
				return;
			}
			pago.Dias = Convert.ToInt32(txtDias.Text);
			pago.Descripcion = txtDescripcion.Text;
			pago.Tipo = Convert.ToBoolean(cmbTipo.SelectedIndex);
			pago.Tipoaccion = Convert.ToBoolean(cmbTipoAccion.SelectedIndex);
			if (Proceso == 1)
			{
				pago.CodUser = frmLogin.iCodUser;
				if (AdmPago.insert(pago))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CambiarEstados(Estado: true);
					CargaLista();
				}
			}
			else if (Proceso == 2 && AdmPago.update(pago))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
			}
			Proceso = 0;
		}
		else if (btnGuardar.Text == "Aceptar" && Proceso == 3)
		{
			Close();
		}
	}

	private void dgvDocumentos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvFormaPagos.Rows.Count >= 1 && e.Row.Selected)
		{
			pago.CodFormaPago = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			pago.Dias = Convert.ToInt32(e.Row.Cells[dias.Name].Value);
			pago.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			pago.Tipo = Convert.ToBoolean(e.Row.Cells[tipo.Name].Value);
			pago.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			pago.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			pago.Tipoaccion = Convert.ToBoolean(e.Row.Cells[codtipoaccion.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
			if (Proceso == 3)
			{
				btnGuardar.Enabled = true;
			}
		}
		else if (dgvFormaPagos.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void dgvDocumentos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvFormaPagos.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvFormaPagos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvFormaPagos.CurrentRow.Index != -1 && pago.CodFormaPago != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Forma de Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmPago.delete(pago.CodFormaPago))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Forma de Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			Close();
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

	private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmFormaPagoRP frm = new frmFormaPagoRP();
		frm.ShowDialog();
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmFormaPago));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvFormaPagos = new System.Windows.Forms.DataGridView();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cmbTipoAccion = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbTipo = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtDias = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dias = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codtipoaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvFormaPagos).BeginInit();
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
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvFormaPagos);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(8, 17);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(492, 198);
		this.groupBox1.TabIndex = 17;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Forma de Pago";
		this.dgvFormaPagos.AllowUserToAddRows = false;
		this.dgvFormaPagos.AllowUserToDeleteRows = false;
		this.dgvFormaPagos.AllowUserToOrderColumns = true;
		this.dgvFormaPagos.AllowUserToResizeColumns = false;
		this.dgvFormaPagos.AllowUserToResizeRows = false;
		this.dgvFormaPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvFormaPagos.Columns.AddRange(this.codigo, this.descripcion, this.dias, this.tipo, this.estado, this.coduser, this.fecha, this.tipoa, this.codtipoaccion);
		this.dgvFormaPagos.Location = new System.Drawing.Point(0, 45);
		this.dgvFormaPagos.MultiSelect = false;
		this.dgvFormaPagos.Name = "dgvFormaPagos";
		this.dgvFormaPagos.ReadOnly = true;
		this.dgvFormaPagos.RowHeadersVisible = false;
		this.dgvFormaPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvFormaPagos.Size = new System.Drawing.Size(484, 147);
		this.dgvFormaPagos.TabIndex = 8;
		this.dgvFormaPagos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellDoubleClick);
		this.dgvFormaPagos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDocumentos_ColumnHeaderMouseClick);
		this.dgvFormaPagos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDocumentos_RowStateChanged);
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
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Location = new System.Drawing.Point(8, 211);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(476, 48);
		this.groupBox3.TabIndex = 16;
		this.groupBox3.TabStop = false;
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
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.cmbTipoAccion);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.cmbTipo);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.txtDias);
		this.groupBox2.Location = new System.Drawing.Point(67, 39);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(310, 156);
		this.groupBox2.TabIndex = 18;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Forma Pago";
		this.groupBox2.Visible = false;
		this.cmbTipoAccion.FormattingEnabled = true;
		this.cmbTipoAccion.Items.AddRange(new object[2] { "Compra", "Venta" });
		this.cmbTipoAccion.Location = new System.Drawing.Point(132, 71);
		this.cmbTipoAccion.Name = "cmbTipoAccion";
		this.cmbTipoAccion.Size = new System.Drawing.Size(128, 21);
		this.cmbTipoAccion.TabIndex = 11;
		this.superValidator1.SetValidator1(this.cmbTipoAccion, this.customValidator4);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(129, 55);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(31, 13);
		this.label7.TabIndex = 10;
		this.label7.Text = "Tipo:";
		this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbTipo.FormattingEnabled = true;
		this.cmbTipo.Items.AddRange(new object[2] { "CREDITO", "CONTADO" });
		this.cmbTipo.Location = new System.Drawing.Point(21, 111);
		this.cmbTipo.Name = "cmbTipo";
		this.cmbTipo.Size = new System.Drawing.Size(238, 20);
		this.cmbTipo.TabIndex = 9;
		this.superValidator1.SetValidator1(this.cmbTipo, this.customValidator3);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(18, 95);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(74, 13);
		this.label6.TabIndex = 8;
		this.label6.Text = "Tipo de Pago:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(18, 16);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Descripcion :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(18, 55);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(36, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Días :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(21, 32);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(238, 20);
		this.txtDescripcion.TabIndex = 4;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator1);
		this.txtDias.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDias.Location = new System.Drawing.Point(21, 71);
		this.txtDias.Name = "txtDias";
		this.txtDias.Size = new System.Drawing.Size(71, 20);
		this.txtDias.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtDias, this.customValidator2);
		this.txtDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDias_KeyPress);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator3.ErrorMessage = "Seleccione un tipo";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese la descripción.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese los días.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator4.ErrorMessage = "Ingrese Dato.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.codigo.DataPropertyName = "codFormaPago";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.codigo.Width = 50;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.descripcion.Width = 250;
		this.dias.DataPropertyName = "dias";
		this.dias.HeaderText = "Días";
		this.dias.Name = "dias";
		this.dias.ReadOnly = true;
		this.dias.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dias.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dias.Width = 80;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "Tipo de Pago";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Visible = false;
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
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaReg";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.fecha.Visible = false;
		this.tipoa.DataPropertyName = "tipoaccion";
		this.tipoa.HeaderText = "Tipo";
		this.tipoa.Name = "tipoa";
		this.tipoa.ReadOnly = true;
		this.codtipoaccion.DataPropertyName = "codtipoaccion";
		this.codtipoaccion.HeaderText = "codtipoaccion";
		this.codtipoaccion.Name = "codtipoaccion";
		this.codtipoaccion.ReadOnly = true;
		this.codtipoaccion.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(496, 271);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmFormaPago";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Forma de Pago";
		base.Load += new System.EventHandler(frmFormaPago_Load);
		base.Shown += new System.EventHandler(frmFormaPago_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvFormaPagos).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
