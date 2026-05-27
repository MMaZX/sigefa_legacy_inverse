using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmCliVisitados : Form
{
	private clsAdmCliente AdmCliente = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	public static BindingSource data = new BindingSource();

	public int codEntConsExt;

	public int proceso = 0;

	private int flgCodCli = 0;

	private int codCliDel = 0;

	private IContainer components = null;

	private Button btnAgregar;

	private Button btnEliminar;

	private Label label2;

	private TextBox txtRazSocial;

	private Label label4;

	private TextBox txtDNIRUC;

	private DataGridView dgvClientes;

	private Label label1;

	private TextBox txtDireccion;

	private ImageList imageList1;

	private Button btnSalir;

	private DataGridViewTextBoxColumn codCliente;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn nameRUC;

	private DataGridViewTextBoxColumn razonsocial;

	private DataGridViewTextBoxColumn direccionlegal;

	public frmCliVisitados()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Dispose();
		Close();
	}

	private void CargaLista()
	{
		dgvClientes.DataSource = data;
		data.DataSource = AdmCliente.ListaClientesConsultor(codEntConsExt);
		dgvClientes.ClearSelection();
	}

	private void frmCliVisitados_Load(object sender, EventArgs e)
	{
		if (proceso == 4)
		{
			txtDNIRUC.Enabled = false;
			txtRazSocial.Enabled = false;
			txtDireccion.Enabled = false;
			btnAgregar.Enabled = false;
			btnEliminar.Enabled = false;
		}
		CargaLista();
	}

	private void txtDNIRUC_Leave(object sender, EventArgs e)
	{
		try
		{
			cli = AdmCliente.ConsultaCliente(txtDNIRUC.Text);
			txtRazSocial.Text = cli.RazonSocial;
			txtDireccion.Text = cli.DireccionLegal;
			flgCodCli = 1;
		}
		catch (NullReferenceException)
		{
			flgCodCli = 0;
			txtRazSocial.Focus();
		}
	}

	private void btnAgregar_Click(object sender, EventArgs e)
	{
		if (flgCodCli == 0)
		{
			cli = new clsCliente();
			if (txtDNIRUC.Text.Length > 8)
			{
				cli.Ruc = txtDNIRUC.Text;
				cli.RazonSocial = txtRazSocial.Text;
				cli.DireccionLegal = txtDireccion.Text;
			}
			else
			{
				cli.Dni = txtDNIRUC.Text;
				cli.Nombre = txtRazSocial.Text;
				cli.RazonSocial = txtRazSocial.Text;
				cli.DireccionLegal = txtDireccion.Text;
			}
			cli.FormaPago = 6;
			cli.CodUser = frmLogin.iCodUser;
			AdmCliente.insert(cli);
			cli.CodCliente = cli.CodClienteNuevo;
		}
		AdmCliente.insertConCli(codEntConsExt, cli.CodCliente);
		CargaLista();
		flgCodCli = 0;
		txtDNIRUC.Text = "";
		txtRazSocial.Text = "";
		txtDireccion.Text = "";
		txtDNIRUC.Focus();
	}

	private void dgvClientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvClientes.Rows.Count >= 1 && e.Row.Selected)
		{
			codCliDel = Convert.ToInt32(e.Row.Cells[codCliente.Name].Value);
			btnEliminar.Enabled = true;
		}
		else if (dgvClientes.SelectedRows.Count == 0)
		{
			btnEliminar.Enabled = false;
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvClientes.SelectedRows.Count == 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Clientes Asesorados", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmCliente.deleteConCli(codEntConsExt, codCliDel))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Clientes Asesorados", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
				codCliDel = 0;
			}
		}
		else
		{
			MessageBox.Show("Seleccione un Cliente", "Clientes Asesorados", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCliVisitados));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		this.btnAgregar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnEliminar = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.txtRazSocial = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtDNIRUC = new System.Windows.Forms.TextBox();
		this.dgvClientes = new System.Windows.Forms.DataGridView();
		this.codCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nameRUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccionlegal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label1 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.dgvClientes).BeginInit();
		base.SuspendLayout();
		this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnAgregar.ImageIndex = 7;
		this.btnAgregar.ImageList = this.imageList1;
		this.btnAgregar.Location = new System.Drawing.Point(640, 55);
		this.btnAgregar.Name = "btnAgregar";
		this.btnAgregar.Size = new System.Drawing.Size(77, 32);
		this.btnAgregar.TabIndex = 19;
		this.btnAgregar.Text = "Agregar";
		this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAgregar.UseVisualStyleBackColor = true;
		this.btnAgregar.Click += new System.EventHandler(btnAgregar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "format_increaseindent.png");
		this.imageList1.Images.SetKeyName(7, "+.gif");
		this.imageList1.Images.SetKeyName(8, "delete.gif");
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 8;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(723, 55);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(76, 32);
		this.btnEliminar.TabIndex = 20;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(166, 13);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(124, 13);
		this.label2.TabIndex = 18;
		this.label2.Text = "Nombre / Razon Social :";
		this.txtRazSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRazSocial.Location = new System.Drawing.Point(169, 29);
		this.txtRazSocial.Name = "txtRazSocial";
		this.txtRazSocial.Size = new System.Drawing.Size(300, 20);
		this.txtRazSocial.TabIndex = 17;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(13, 13);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(66, 13);
		this.label4.TabIndex = 16;
		this.label4.Text = "DNI / RUC :";
		this.txtDNIRUC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDNIRUC.Location = new System.Drawing.Point(16, 29);
		this.txtDNIRUC.Name = "txtDNIRUC";
		this.txtDNIRUC.Size = new System.Drawing.Size(120, 20);
		this.txtDNIRUC.TabIndex = 15;
		this.txtDNIRUC.Leave += new System.EventHandler(txtDNIRUC_Leave);
		this.dgvClientes.AllowUserToAddRows = false;
		this.dgvClientes.AllowUserToDeleteRows = false;
		this.dgvClientes.AllowUserToOrderColumns = true;
		this.dgvClientes.AllowUserToResizeColumns = false;
		this.dgvClientes.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvClientes.Columns.AddRange(this.codCliente, this.dni, this.nameRUC, this.razonsocial, this.direccionlegal);
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvClientes.DefaultCellStyle = dataGridViewCellStyle3;
		this.dgvClientes.Location = new System.Drawing.Point(17, 93);
		this.dgvClientes.MultiSelect = false;
		this.dgvClientes.Name = "dgvClientes";
		this.dgvClientes.ReadOnly = true;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvClientes.RowHeadersVisible = false;
		this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvClientes.Size = new System.Drawing.Size(801, 278);
		this.dgvClientes.TabIndex = 14;
		this.dgvClientes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvClientes_RowStateChanged);
		this.codCliente.DataPropertyName = "codCliente";
		dataGridViewCellStyle5.NullValue = null;
		this.codCliente.DefaultCellStyle = dataGridViewCellStyle5;
		this.codCliente.HeaderText = "Codigo";
		this.codCliente.Name = "codCliente";
		this.codCliente.ReadOnly = true;
		this.codCliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codCliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codCliente.Visible = false;
		this.dni.DataPropertyName = "dni";
		this.dni.HeaderText = "DNI";
		this.dni.Name = "dni";
		this.dni.ReadOnly = true;
		this.nameRUC.DataPropertyName = "ruc";
		this.nameRUC.HeaderText = "RUC";
		this.nameRUC.Name = "nameRUC";
		this.nameRUC.ReadOnly = true;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.HeaderText = "Nombre / Razon Social";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.ReadOnly = true;
		this.razonsocial.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.razonsocial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.Width = 250;
		this.direccionlegal.DataPropertyName = "direccionlegal";
		this.direccionlegal.HeaderText = "Dirección";
		this.direccionlegal.Name = "direccionlegal";
		this.direccionlegal.ReadOnly = true;
		this.direccionlegal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.direccionlegal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccionlegal.Width = 340;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(485, 13);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(58, 13);
		this.label1.TabIndex = 22;
		this.label1.Text = "Dirección :";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(488, 29);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(330, 20);
		this.txtDireccion.TabIndex = 18;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(744, 394);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 23;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(824, 433);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtDireccion);
		base.Controls.Add(this.btnAgregar);
		base.Controls.Add(this.btnEliminar);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.txtRazSocial);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.txtDNIRUC);
		base.Controls.Add(this.dgvClientes);
		base.Name = "frmCliVisitados";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Clientes Asesorados";
		base.Load += new System.EventHandler(frmCliVisitados_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvClientes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
