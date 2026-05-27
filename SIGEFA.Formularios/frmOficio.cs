using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmOficio : Form
{
	public clsOficio oficio = new clsOficio();

	public int proceso = 0;

	private IContainer components = null;

	private TextBox txtDescripcion;

	private Label label1;

	private Button btnGuardar;

	private Button btnSalir;

	public frmOficio()
	{
		InitializeComponent();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtDescripcion.Text == "")
			{
				MessageBox.Show("Tiene que indicar un oficio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtDescripcion.Focus();
			}
			else
			{
				oficio.Descripcion = txtDescripcion.Text;
				base.DialogResult = DialogResult.Yes;
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void frmOficio_Load(object sender, EventArgs e)
	{
		if (proceso == 1)
		{
			txtDescripcion.Text = oficio.Descripcion;
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
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.txtDescripcion.Location = new System.Drawing.Point(84, 6);
		this.txtDescripcion.MaxLength = 255;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(282, 20);
		this.txtDescripcion.TabIndex = 3;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(66, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Descripcion:";
		this.btnGuardar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.ForeColor = System.Drawing.Color.Blue;
		this.btnGuardar.Location = new System.Drawing.Point(183, 32);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(81, 23);
		this.btnGuardar.TabIndex = 6;
		this.btnGuardar.Text = "GUARDAR";
		this.btnGuardar.UseVisualStyleBackColor = false;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnSalir.BackColor = System.Drawing.Color.LightCoral;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.ForeColor = System.Drawing.Color.DarkRed;
		this.btnSalir.Location = new System.Drawing.Point(270, 32);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(96, 23);
		this.btnSalir.TabIndex = 7;
		this.btnSalir.Text = "CANCELAR";
		this.btnSalir.UseVisualStyleBackColor = false;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(387, 65);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.txtDescripcion);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.MinimumSize = new System.Drawing.Size(403, 104);
		base.Name = "frmOficio";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Oficio";
		base.Load += new System.EventHandler(frmOficio_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
