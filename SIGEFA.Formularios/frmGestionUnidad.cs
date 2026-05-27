using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionUnidad : Office2007Form
{
	public int Proceso = 0;

	private clsAdmUnidad admUni = new clsAdmUnidad();

	public clsUnidadMedida uni = new clsUnidadMedida();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label2;

	private Label label1;

	private TextBox txtDescripcion;

	private TextBox txtSigla;

	private ImageList imageList1;

	private Button btnCancelar;

	private Button btnGuardar;

	public frmGestionUnidad()
	{
		InitializeComponent();
	}

	private void frmGestionUnidad_Load(object sender, EventArgs e)
	{
		if (Proceso == 2)
		{
			txtSigla.Text = uni.Sigla;
			txtDescripcion.Text = uni.Descripcion;
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (Proceso == 0 || !(txtSigla.Text != "") || !(txtDescripcion.Text != ""))
		{
			return;
		}
		uni.Sigla = txtSigla.Text;
		uni.Descripcion = txtDescripcion.Text;
		if (Proceso == 1)
		{
			if (admUni.insert(uni))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Unidad", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		else if (Proceso == 2 && admUni.update(uni))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Unidad", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionUnidad));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtSigla = new System.Windows.Forms.TextBox();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtDescripcion);
		this.groupBox1.Controls.Add(this.txtSigla);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(279, 104);
		this.groupBox1.TabIndex = 4;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Nueva unidad de media";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 55);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(69, 13);
		this.label2.TabIndex = 7;
		this.label2.Text = "Descripcion :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(17, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(36, 13);
		this.label1.TabIndex = 6;
		this.label1.Text = "Sigla :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(20, 71);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(238, 20);
		this.txtDescripcion.TabIndex = 5;
		this.txtSigla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSigla.Location = new System.Drawing.Point(20, 32);
		this.txtSigla.Name = "txtSigla";
		this.txtSigla.Size = new System.Drawing.Size(71, 20);
		this.txtSigla.TabIndex = 4;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(1, "cross.png");
		this.btnCancelar.ImageIndex = 1;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(209, 122);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(82, 24);
		this.btnCancelar.TabIndex = 12;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnGuardar.ImageIndex = 0;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(121, 122);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(82, 24);
		this.btnGuardar.TabIndex = 13;
		this.btnGuardar.Text = " Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(303, 158);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionUnidad";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Gestion Unidad";
		base.Load += new System.EventHandler(frmGestionUnidad_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
