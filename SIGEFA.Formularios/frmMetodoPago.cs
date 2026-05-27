using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmMetodoPago : Office2007Form
{
	private clsAdmMetodoPago AdmPago = new clsAdmMetodoPago();

	public clsMetodoPago pago = new clsMetodoPago();

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

	private TextBox txtDescripcion;

	private DataGridView dgvMetodoPagos;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private Label label5;

	private TextBox txtCodigo;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	public frmMetodoPago()
	{
		InitializeComponent();
	}

	private void frmMetodoPago_Load(object sender, EventArgs e)
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
		txtCodigo.Text = "";
		txtDescripcion.Text = "";
		superValidator1.Validate();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Editar Registro";
		Proceso = 2;
		txtCodigo.Text = pago.CodMetodoPago.ToString();
		txtDescripcion.Text = pago.Descripcion;
	}

	private void CargaLista()
	{
		dgvMetodoPagos.DataSource = data;
		data.DataSource = AdmPago.MuestraMetodoPagos();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvMetodoPagos.ClearSelection();
	}

	private void frmMetodoPago_Shown(object sender, EventArgs e)
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
			if (!superValidator1.Validate() || Proceso == 0 || !(txtDescripcion.Text != ""))
			{
				return;
			}
			pago.Descripcion = txtDescripcion.Text;
			if (Proceso == 1)
			{
				pago.CodUser = frmLogin.iCodUser;
				if (AdmPago.insert(pago))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Metodo de pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CambiarEstados(Estado: true);
					CargaLista();
				}
			}
			else if (Proceso == 2 && AdmPago.update(pago))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Metodo de pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		if (dgvMetodoPagos.Rows.Count >= 1 && e.Row.Selected)
		{
			pago.CodMetodoPago = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			pago.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			pago.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			pago.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
			if (Proceso == 3)
			{
				btnGuardar.Enabled = true;
			}
		}
		else if (dgvMetodoPagos.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void dgvDocumentos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvMetodoPagos.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvMetodoPagos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvMetodoPagos.CurrentRow.Index != -1 && pago.CodMetodoPago != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Metodo de Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmPago.delete(pago.CodMetodoPago))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Metodo de Pago", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 7;
		frm.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmMetodoPago));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvMetodoPagos = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.label5 = new System.Windows.Forms.Label();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvMetodoPagos).BeginInit();
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
		this.groupBox1.Controls.Add(this.dgvMetodoPagos);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 198);
		this.groupBox1.TabIndex = 17;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Metodo de Pago";
		this.dgvMetodoPagos.AllowUserToAddRows = false;
		this.dgvMetodoPagos.AllowUserToDeleteRows = false;
		this.dgvMetodoPagos.AllowUserToOrderColumns = true;
		this.dgvMetodoPagos.AllowUserToResizeColumns = false;
		this.dgvMetodoPagos.AllowUserToResizeRows = false;
		this.dgvMetodoPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvMetodoPagos.Columns.AddRange(this.codigo, this.descripcion, this.estado, this.coduser, this.fecha);
		this.dgvMetodoPagos.Location = new System.Drawing.Point(6, 45);
		this.dgvMetodoPagos.MultiSelect = false;
		this.dgvMetodoPagos.Name = "dgvMetodoPagos";
		this.dgvMetodoPagos.ReadOnly = true;
		this.dgvMetodoPagos.RowHeadersVisible = false;
		this.dgvMetodoPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvMetodoPagos.Size = new System.Drawing.Size(464, 147);
		this.dgvMetodoPagos.TabIndex = 8;
		this.dgvMetodoPagos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellDoubleClick);
		this.dgvMetodoPagos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDocumentos_ColumnHeaderMouseClick);
		this.dgvMetodoPagos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDocumentos_RowStateChanged);
		this.codigo.DataPropertyName = "codMetodoPago";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 50;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.descripcion.Width = 300;
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
		this.groupBox3.Location = new System.Drawing.Point(12, 211);
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
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Location = new System.Drawing.Point(107, 58);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(298, 104);
		this.groupBox2.TabIndex = 18;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Metodo Pago";
		this.groupBox2.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(20, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 9;
		this.label5.Text = "Código :";
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(23, 32);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(71, 20);
		this.txtCodigo.TabIndex = 8;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(20, 55);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Descripcion :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(23, 71);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(238, 20);
		this.txtDescripcion.TabIndex = 4;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator1);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Ingrese la descripción.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator2.ErrorMessage = "Ingrese los días.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 271);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmMetodoPago";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Metodo de Pago";
		base.Load += new System.EventHandler(frmMetodoPago_Load);
		base.Shown += new System.EventHandler(frmMetodoPago_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvMetodoPagos).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
