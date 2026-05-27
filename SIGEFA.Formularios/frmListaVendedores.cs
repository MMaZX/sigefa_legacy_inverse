using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmListaVendedores : Office2007Form
{
	private clsAdmVendedor admVen = new clsAdmVendedor();

	public clsVendedor ven = new clsVendedor();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox2;

	private DataGridView dgvVendedores;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn apellido;

	private DataGridViewTextBoxColumn fechanac;

	private DataGridViewTextBoxColumn direccion;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewTextBoxColumn celular;

	private DataGridViewTextBoxColumn email;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private Label label8;

	private Label label9;

	private TextBox txtFiltro;

	private Label label10;

	public frmListaVendedores()
	{
		InitializeComponent();
	}

	public int GetCodigoVendedor()
	{
		return ven.CodVendedor = Convert.ToInt32(dgvVendedores.CurrentRow.Cells[0].Value);
	}

	private void frmListaVendedores_Load(object sender, EventArgs e)
	{
		CargaLista();
		label9.Text = "Codigo";
		label8.Text = "codVendedor";
	}

	private void CargaLista()
	{
		dgvVendedores.DataSource = data;
		data.DataSource = admVen.MuestraVendedores();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvVendedores.ClearSelection();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label8.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void dgvVendedores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		frmConsultorExt form = (frmConsultorExt)Application.OpenForms["frmConsultorExt"];
		form.CodVendedor = int.Parse(dgvVendedores.CurrentRow.Cells["codigo"].Value.ToString());
		form.txtCodCliente.Text = dgvVendedores.CurrentRow.Cells["dni"].Value.ToString();
		form.txtNombreCliente.Text = dgvVendedores.CurrentRow.Cells["apellido"].Value.ToString();
		Close();
	}

	private void dgvVendedores_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label9.Text = dgvVendedores.Columns[e.ColumnIndex].HeaderText;
		label8.Text = dgvVendedores.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void frmListaVendedores_FormClosing(object sender, FormClosingEventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void frmListaVendedores_Shown(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvVendedores = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechanac = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.celular = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVendedores).BeginInit();
		base.SuspendLayout();
		this.groupBox2.AccessibleDescription = "";
		this.groupBox2.Controls.Add(this.dgvVendedores);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtFiltro);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Location = new System.Drawing.Point(6, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(556, 207);
		this.groupBox2.TabIndex = 21;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Conductor";
		this.dgvVendedores.AllowUserToAddRows = false;
		this.dgvVendedores.AllowUserToDeleteRows = false;
		this.dgvVendedores.AllowUserToOrderColumns = true;
		this.dgvVendedores.AllowUserToResizeColumns = false;
		this.dgvVendedores.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVendedores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvVendedores.Columns.AddRange(this.codigo, this.dni, this.nombre, this.apellido, this.fechanac, this.direccion, this.telefono, this.celular, this.email, this.estado, this.coduser, this.fecha);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvVendedores.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvVendedores.Location = new System.Drawing.Point(6, 45);
		this.dgvVendedores.MultiSelect = false;
		this.dgvVendedores.Name = "dgvVendedores";
		this.dgvVendedores.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVendedores.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvVendedores.RowHeadersVisible = false;
		this.dgvVendedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVendedores.Size = new System.Drawing.Size(544, 156);
		this.dgvVendedores.TabIndex = 8;
		this.dgvVendedores.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVendedores_CellDoubleClick);
		this.dgvVendedores.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvVendedores_ColumnHeaderMouseClick);
		this.codigo.DataPropertyName = "codVendedor";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 60;
		this.dni.DataPropertyName = "dni";
		this.dni.HeaderText = "DNI";
		this.dni.Name = "dni";
		this.dni.ReadOnly = true;
		this.dni.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 150;
		this.apellido.DataPropertyName = "apellido";
		this.apellido.HeaderText = "Nombre Completo";
		this.apellido.Name = "apellido";
		this.apellido.ReadOnly = true;
		this.apellido.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.apellido.Width = 150;
		this.fechanac.DataPropertyName = "fechanac";
		this.fechanac.HeaderText = "F. Nac.";
		this.fechanac.Name = "fechanac";
		this.fechanac.ReadOnly = true;
		this.fechanac.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fechanac.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccion.DataPropertyName = "direccion";
		this.direccion.HeaderText = "Direccion";
		this.direccion.Name = "direccion";
		this.direccion.ReadOnly = true;
		this.direccion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.direccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccion.Visible = false;
		this.telefono.DataPropertyName = "telefono";
		this.telefono.HeaderText = "Telefono";
		this.telefono.Name = "telefono";
		this.telefono.ReadOnly = true;
		this.telefono.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.telefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.telefono.Visible = false;
		this.celular.DataPropertyName = "celular";
		this.celular.HeaderText = "Celular";
		this.celular.Name = "celular";
		this.celular.ReadOnly = true;
		this.celular.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.celular.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.email.DataPropertyName = "email";
		this.email.HeaderText = "E-mail";
		this.email.Name = "email";
		this.email.ReadOnly = true;
		this.email.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(427, 22);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(12, 13);
		this.label8.TabIndex = 6;
		this.label8.Text = "x";
		this.label8.Visible = false;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(96, 20);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(17, 16);
		this.label9.TabIndex = 5;
		this.label9.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(200, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(25, 22);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(65, 13);
		this.label10.TabIndex = 3;
		this.label10.Text = "Buscar Por :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(574, 227);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmListaVendedores";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmListaVendedores";
		base.Load += new System.EventHandler(frmListaVendedores_Load);
		base.Shown += new System.EventHandler(frmListaVendedores_Shown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmListaVendedores_FormClosing);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVendedores).EndInit();
		base.ResumeLayout(false);
	}
}
