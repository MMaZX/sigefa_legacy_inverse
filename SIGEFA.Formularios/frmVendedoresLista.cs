using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmVendedoresLista : Office2007Form
{
	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsUsuario usuario;

	public int proc = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvVendedores;

	private Button btnSeleccionar;

	private Button btnCancelar;

	private Label label1;

	private TextBox txtBusqueda;

	private DataGridViewTextBoxColumn codUsuario;

	private DataGridViewTextBoxColumn vendedor;

	public frmVendedoresLista()
	{
		InitializeComponent();
	}

	private void frmVendedoresLista_Load(object sender, EventArgs e)
	{
		dgvVendedores.DefaultCellStyle.Font = new Font("Verdana", 10f);
		dgvVendedores.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
		dgvVendedores.AutoGenerateColumns = false;
		CargarUsuariosVendedores();
		base.ActiveControl = txtBusqueda;
	}

	private void CargarUsuariosVendedores()
	{
		dgvVendedores.DataSource = data;
		data.DataSource = admUsuario.CargaUsuarios();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvVendedores.ClearSelection();
	}

	private void txtBusqueda_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtBusqueda.Text.Length >= 1)
			{
				data.Filter = string.Format("[{0}] like '*{1}*'", "vendedor", txtBusqueda.Text.Trim());
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

	private void seleccionarCliente()
	{
		try
		{
			if (proc == 2)
			{
				frmVenta2019 frmVenta2020 = Application.OpenForms.OfType<frmVenta2019>().Single();
				int codigoVendedor = Convert.ToInt32(dgvVendedores.CurrentRow.Cells[codUsuario.Name].Value);
				usuario = admUsuario.MuestraUsuarioSinAdmin(codigoVendedor);
				if (usuario != null)
				{
					frmVenta2020.vendedor = usuario;
					Close();
				}
				else
				{
					MessageBox.Show("No se encontró el vendedor", "Vendedores", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				frmOrdenVenta frmOrdenVenta2 = Application.OpenForms.OfType<frmOrdenVenta>().Single();
				int codigoVendedor2 = Convert.ToInt32(dgvVendedores.CurrentRow.Cells[codUsuario.Name].Value);
				usuario = admUsuario.MuestraUsuarioSinAdmin(codigoVendedor2);
				if (usuario != null)
				{
					frmOrdenVenta2.vendedor = usuario;
					Close();
				}
				else
				{
					MessageBox.Show("No se encontró el vendedor", "Vendedores", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception)
		{
			Close();
		}
	}

	private void btnSeleccionar_Click(object sender, EventArgs e)
	{
		seleccionarCliente();
	}

	private void dgvVendedores_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		seleccionarCliente();
	}

	private void dgvVendedores_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Return)
			{
				seleccionarCliente();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Return)
			{
				dgvVendedores.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVendedoresLista));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvVendedores = new System.Windows.Forms.DataGridView();
		this.codUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.vendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnSeleccionar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtBusqueda = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVendedores).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvVendedores);
		this.groupBox1.Location = new System.Drawing.Point(12, 54);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(713, 221);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "LISTA DE VENDEDORES";
		this.dgvVendedores.AllowUserToAddRows = false;
		this.dgvVendedores.AllowUserToDeleteRows = false;
		this.dgvVendedores.AllowUserToResizeColumns = false;
		this.dgvVendedores.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVendedores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvVendedores.Columns.AddRange(this.codUsuario, this.vendedor);
		this.dgvVendedores.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVendedores.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvVendedores.Location = new System.Drawing.Point(3, 16);
		this.dgvVendedores.MultiSelect = false;
		this.dgvVendedores.Name = "dgvVendedores";
		this.dgvVendedores.ReadOnly = true;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVendedores.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dgvVendedores.RowHeadersVisible = false;
		this.dgvVendedores.Size = new System.Drawing.Size(707, 202);
		this.dgvVendedores.TabIndex = 0;
		this.dgvVendedores.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVendedores_CellContentDoubleClick);
		this.dgvVendedores.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvVendedores_KeyDown);
		this.codUsuario.DataPropertyName = "codUsuario";
		this.codUsuario.HeaderText = "Código";
		this.codUsuario.Name = "codUsuario";
		this.codUsuario.ReadOnly = true;
		this.vendedor.DataPropertyName = "vendedor";
		this.vendedor.HeaderText = "Nombre del Vendedor";
		this.vendedor.Name = "vendedor";
		this.vendedor.ReadOnly = true;
		this.vendedor.Width = 600;
		this.btnSeleccionar.Image = (System.Drawing.Image)resources.GetObject("btnSeleccionar.Image");
		this.btnSeleccionar.Location = new System.Drawing.Point(499, 278);
		this.btnSeleccionar.Name = "btnSeleccionar";
		this.btnSeleccionar.Size = new System.Drawing.Size(116, 35);
		this.btnSeleccionar.TabIndex = 1;
		this.btnSeleccionar.Text = "SELECCIONAR";
		this.btnSeleccionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSeleccionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSeleccionar.UseVisualStyleBackColor = true;
		this.btnSeleccionar.Click += new System.EventHandler(btnSeleccionar_Click);
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.Image = (System.Drawing.Image)resources.GetObject("btnCancelar.Image");
		this.btnCancelar.Location = new System.Drawing.Point(621, 278);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(101, 35);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "CANCELAR";
		this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(12, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(180, 16);
		this.label1.TabIndex = 3;
		this.label1.Text = "FILTRAR POR NOMBRE:";
		this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBusqueda.Location = new System.Drawing.Point(198, 18);
		this.txtBusqueda.Name = "txtBusqueda";
		this.txtBusqueda.Size = new System.Drawing.Size(524, 22);
		this.txtBusqueda.TabIndex = 4;
		this.txtBusqueda.TextChanged += new System.EventHandler(txtBusqueda_TextChanged);
		this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBusqueda_KeyDown);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancelar;
		base.ClientSize = new System.Drawing.Size(737, 326);
		base.Controls.Add(this.txtBusqueda);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnSeleccionar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVendedoresLista";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Lista Vendedores";
		base.Load += new System.EventHandler(frmVendedoresLista_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvVendedores).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
