using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmListaNotaSalidaNDC : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	public clsFactura factura = new clsFactura();

	public int CodProveedor = 0;

	public int CodNotaS = 0;

	public string documento = "";

	private IContainer components = null;

	private GroupBox groupBox3;

	private DataGridView dgvDocumentos;

	private GroupBox groupBox2;

	private Button btnAceptar;

	private ImageList imageList1;

	private DataGridViewTextBoxColumn codNotaSalida;

	private DataGridViewTextBoxColumn Documen;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn razonsocials;

	private DataGridViewTextBoxColumn codProveedors;

	private DataGridViewTextBoxColumn fechasalida;

	private DataGridViewTextBoxColumn responsables;

	public frmListaNotaSalidaNDC()
	{
		InitializeComponent();
	}

	private void frmListaNotaSalidaNDC_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		dgvDocumentos.DataSource = data;
		data.DataSource = AdmNotaS.ListarNotaSalidaParaNDCompra(frmLogin.iCodAlmacen, CodProveedor);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDocumentos.ClearSelection();
	}

	private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count > 0 && e.RowIndex > -1)
		{
			CodNotaS = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells[codNotaSalida.Name].Value.ToString());
			documento = dgvDocumentos.Rows[e.RowIndex].Cells[Documen.Name].Value.ToString();
		}
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count > 0 && e.RowIndex > -1)
		{
			CodNotaS = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells[codNotaSalida.Name].Value.ToString());
			documento = dgvDocumentos.Rows[e.RowIndex].Cells[Documen.Name].Value.ToString();
			Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaNotaSalidaNDC));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgvDocumentos = new System.Windows.Forms.DataGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.codNotaSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Documen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocials = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codProveedors = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsables = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox3.Controls.Add(this.dgvDocumentos);
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(703, 305);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Notas de Salida";
		this.dgvDocumentos.AllowUserToAddRows = false;
		this.dgvDocumentos.AllowUserToDeleteRows = false;
		this.dgvDocumentos.AllowUserToResizeColumns = false;
		this.dgvDocumentos.AllowUserToResizeRows = false;
		this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDocumentos.Columns.AddRange(this.codNotaSalida, this.Documen, this.ruc, this.razonsocials, this.codProveedors, this.fechasalida, this.responsables);
		this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDocumentos.Location = new System.Drawing.Point(3, 16);
		this.dgvDocumentos.MultiSelect = false;
		this.dgvDocumentos.Name = "dgvDocumentos";
		this.dgvDocumentos.ReadOnly = true;
		this.dgvDocumentos.RowHeadersVisible = false;
		this.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDocumentos.Size = new System.Drawing.Size(697, 286);
		this.dgvDocumentos.TabIndex = 10;
		this.dgvDocumentos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellDoubleClick);
		this.dgvDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellClick);
		this.groupBox2.Controls.Add(this.btnAceptar);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 302);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(703, 46);
		this.groupBox2.TabIndex = 12;
		this.groupBox2.TabStop = false;
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(598, 10);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(90, 32);
		this.btnAceptar.TabIndex = 5;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.codNotaSalida.DataPropertyName = "codNotaSalida";
		this.codNotaSalida.HeaderText = "codNotaSalida";
		this.codNotaSalida.Name = "codNotaSalida";
		this.codNotaSalida.ReadOnly = true;
		this.codNotaSalida.Visible = false;
		this.Documen.DataPropertyName = "documento";
		this.Documen.HeaderText = "Documento";
		this.Documen.Name = "Documen";
		this.Documen.ReadOnly = true;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "RUC";
		this.ruc.Name = "ruc";
		this.ruc.ReadOnly = true;
		this.ruc.Visible = false;
		this.razonsocials.DataPropertyName = "razonsocial";
		this.razonsocials.HeaderText = "Razon Social";
		this.razonsocials.Name = "razonsocials";
		this.razonsocials.ReadOnly = true;
		this.razonsocials.Width = 150;
		this.codProveedors.DataPropertyName = "codProveedor";
		this.codProveedors.HeaderText = "codProveedor";
		this.codProveedors.Name = "codProveedors";
		this.codProveedors.ReadOnly = true;
		this.codProveedors.Visible = false;
		this.fechasalida.DataPropertyName = "fecha";
		dataGridViewCellStyle1.Format = "d";
		dataGridViewCellStyle1.NullValue = null;
		this.fechasalida.DefaultCellStyle = dataGridViewCellStyle1;
		this.fechasalida.HeaderText = "Fecha";
		this.fechasalida.Name = "fechasalida";
		this.fechasalida.ReadOnly = true;
		this.responsables.DataPropertyName = "responsable";
		this.responsables.HeaderText = "Responsable";
		this.responsables.Name = "responsables";
		this.responsables.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(703, 348);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmListaNotaSalidaNDC";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Notas de Salida";
		base.Load += new System.EventHandler(frmListaNotaSalidaNDC_Load);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
