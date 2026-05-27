using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmCambioProveedor : Form
{
	private clsProveedor prov = new clsProveedor();

	private clsAdmProveedor admProv = new clsAdmProveedor();

	public int Procede = 0;

	public int Proceso = 0;

	public int CodProveedor;

	public int CodProducto;

	public int CodProv;

	public bool estado;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private TextBox txtCodigoProv2;

	private Label label2;

	private TextBox txtDireccionProv2;

	private TextBox txtProveedor2;

	private ImageList imageList1;

	private GroupBox groupBox2;

	private Button btnCancelar;

	private Button btnAceptar;

	private TextBox txtCodigoProv1;

	private TextBox txtDireccionProv1;

	private TextBox txtProveedor1;

	private TextBox txtCodProv1;

	public TextBox txtCodProv2;

	public frmCambioProveedor()
	{
		InitializeComponent();
	}

	private void CargaProveedor(int codprov, int caso)
	{
		try
		{
			prov = admProv.MuestraProveedor(codprov);
			if (prov != null)
			{
				if (caso == 1)
				{
					txtCodProv1.Text = CodProveedor.ToString();
					txtCodigoProv1.Text = prov.Ruc;
					txtProveedor1.Text = prov.RazonSocial;
					txtDireccionProv1.Text = prov.Direccion;
				}
				else
				{
					txtCodProv2.Text = CodProv.ToString();
					txtCodigoProv2.Text = prov.Ruc;
					txtProveedor2.Text = prov.RazonSocial;
					txtDireccionProv2.Text = prov.Direccion;
				}
			}
			else if (caso == 1)
			{
				txtCodProv1.Text = "";
				txtCodigoProv1.Text = "";
				txtProveedor1.Text = "";
				txtDireccionProv1.Text = "";
			}
			else
			{
				txtCodProv2.Text = "";
				txtCodigoProv2.Text = "";
				txtProveedor2.Text = "";
				txtDireccionProv2.Text = "";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error : " + ex.Message);
		}
	}

	private void frmCambioProveedor_Load(object sender, EventArgs e)
	{
		CargaProveedor(CodProveedor, 1);
	}

	private void BorrarProveedor()
	{
		prov = admProv.MuestraProveedor(CodProveedor);
		txtCodProv1.Text = "";
		txtCodigoProv1.Text = "";
		txtProveedor1.Text = "";
		txtDireccionProv1.Text = "";
	}

	private void txtCodigoProv2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmProveedoresLista"] != null)
		{
			Application.OpenForms["frmProveedoresLista"].Activate();
			return;
		}
		frmProveedoresLista form = new frmProveedoresLista();
		form.Proceso = 3;
		form.Procede = 9;
		form.codProv = CodProveedor;
		form.ShowDialog();
		if (CodProv != 0)
		{
			CargaProveedor(CodProv, 2);
			ProcessTabKey(forward: true);
		}
		else
		{
			BorrarProveedor();
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (Proceso != 0 && Proceso == 2)
		{
			estado = admProv.CambiaProveedor(CodProducto, CodProveedor, CodProv);
			if (estado)
			{
				MessageBox.Show("Los datos se Actualizaron Correctamente", "Cambio de Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmGestionProducto frm = (frmGestionProducto)Application.OpenForms["frmGestionProducto"];
				frm.CargaProductosProveedor();
				Close();
			}
			else
			{
				MessageBox.Show("Error : Los datos no se Actualizaron", "Cambio de Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCambioProveedor));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtCodProv1 = new System.Windows.Forms.TextBox();
		this.txtCodProv2 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtDireccionProv2 = new System.Windows.Forms.TextBox();
		this.txtProveedor2 = new System.Windows.Forms.TextBox();
		this.txtDireccionProv1 = new System.Windows.Forms.TextBox();
		this.txtCodigoProv2 = new System.Windows.Forms.TextBox();
		this.txtProveedor1 = new System.Windows.Forms.TextBox();
		this.txtCodigoProv1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtCodProv1);
		this.groupBox1.Controls.Add(this.txtCodProv2);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtDireccionProv2);
		this.groupBox1.Controls.Add(this.txtProveedor2);
		this.groupBox1.Controls.Add(this.txtDireccionProv1);
		this.groupBox1.Controls.Add(this.txtCodigoProv2);
		this.groupBox1.Controls.Add(this.txtProveedor1);
		this.groupBox1.Controls.Add(this.txtCodigoProv1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(582, 145);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos Generales";
		this.txtCodProv1.Enabled = false;
		this.txtCodProv1.Location = new System.Drawing.Point(526, 27);
		this.txtCodProv1.Name = "txtCodProv1";
		this.txtCodProv1.ReadOnly = true;
		this.txtCodProv1.Size = new System.Drawing.Size(49, 20);
		this.txtCodProv1.TabIndex = 10;
		this.txtCodProv1.Visible = false;
		this.txtCodProv2.Enabled = false;
		this.txtCodProv2.Location = new System.Drawing.Point(526, 91);
		this.txtCodProv2.Name = "txtCodProv2";
		this.txtCodProv2.ReadOnly = true;
		this.txtCodProv2.Size = new System.Drawing.Size(49, 20);
		this.txtCodProv2.TabIndex = 9;
		this.txtCodProv2.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 94);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(92, 13);
		this.label2.TabIndex = 7;
		this.label2.Text = "Proveedor Actual:";
		this.txtDireccionProv2.Enabled = false;
		this.txtDireccionProv2.Location = new System.Drawing.Point(107, 117);
		this.txtDireccionProv2.Name = "txtDireccionProv2";
		this.txtDireccionProv2.ReadOnly = true;
		this.txtDireccionProv2.Size = new System.Drawing.Size(413, 20);
		this.txtDireccionProv2.TabIndex = 6;
		this.txtProveedor2.Enabled = false;
		this.txtProveedor2.Location = new System.Drawing.Point(224, 91);
		this.txtProveedor2.Name = "txtProveedor2";
		this.txtProveedor2.ReadOnly = true;
		this.txtProveedor2.Size = new System.Drawing.Size(296, 20);
		this.txtProveedor2.TabIndex = 5;
		this.txtDireccionProv1.Enabled = false;
		this.txtDireccionProv1.Location = new System.Drawing.Point(107, 53);
		this.txtDireccionProv1.Name = "txtDireccionProv1";
		this.txtDireccionProv1.ReadOnly = true;
		this.txtDireccionProv1.Size = new System.Drawing.Size(413, 20);
		this.txtDireccionProv1.TabIndex = 4;
		this.txtCodigoProv2.Location = new System.Drawing.Point(107, 91);
		this.txtCodigoProv2.Name = "txtCodigoProv2";
		this.txtCodigoProv2.ReadOnly = true;
		this.txtCodigoProv2.Size = new System.Drawing.Size(111, 20);
		this.txtCodigoProv2.TabIndex = 3;
		this.txtCodigoProv2.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodigoProv2_KeyDown);
		this.txtProveedor1.Enabled = false;
		this.txtProveedor1.Location = new System.Drawing.Point(224, 27);
		this.txtProveedor1.Name = "txtProveedor1";
		this.txtProveedor1.ReadOnly = true;
		this.txtProveedor1.Size = new System.Drawing.Size(296, 20);
		this.txtProveedor1.TabIndex = 2;
		this.txtCodigoProv1.Enabled = false;
		this.txtCodigoProv1.Location = new System.Drawing.Point(107, 27);
		this.txtCodigoProv1.Name = "txtCodigoProv1";
		this.txtCodigoProv1.ReadOnly = true;
		this.txtCodigoProv1.Size = new System.Drawing.Size(111, 20);
		this.txtCodigoProv1.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(7, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(98, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Proveedor Anterior:";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.imageList1.Images.SetKeyName(3, "Donate.ico");
		this.imageList1.Images.SetKeyName(4, "Add.png");
		this.imageList1.Images.SetKeyName(5, "Remove.png");
		this.imageList1.Images.SetKeyName(6, "Write Document.png");
		this.imageList1.Images.SetKeyName(7, "Save-icon.png");
		this.groupBox2.Controls.Add(this.btnCancelar);
		this.groupBox2.Controls.Add(this.btnAceptar);
		this.groupBox2.Location = new System.Drawing.Point(193, 164);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(178, 50);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageKey = "cross.png";
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(90, 19);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 1;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageKey = "tick.png";
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(15, 19);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(69, 23);
		this.btnAceptar.TabIndex = 0;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(607, 222);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmCambioProveedor";
		this.Text = "Cambio de Proveedor";
		base.Load += new System.EventHandler(frmCambioProveedor_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
