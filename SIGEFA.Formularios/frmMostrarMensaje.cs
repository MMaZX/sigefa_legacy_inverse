using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEFA.Formularios;

public class frmMostrarMensaje : Form
{
	private DialogResult rpta = DialogResult.Cancel;

	public bool SiNo = false;

	public bool Ok = false;

	public string textoColor = "";

	public Color colorTexto = Color.Red;

	public string textoNegro = "";

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnOk;

	private Button btnSi;

	private Button btnNo;

	public Label lblTextoColor;

	public Label lblTextoNegro;

	public string TextBtnSi { get; internal set; }

	public string TextBtnNo { get; internal set; }

	public frmMostrarMensaje()
	{
		InitializeComponent();
	}

	private void frmMostrarMensaje_Load(object sender, EventArgs e)
	{
		lblTextoColor.Text = textoColor;
		lblTextoColor.BackColor = colorTexto;
		lblTextoNegro.Text = textoNegro;
		if (SiNo)
		{
			btnSi.Visible = true;
			btnSi.Text = TextBtnSi;
			btnNo.Text = TextBtnNo;
			btnNo.Visible = true;
		}
		if (Ok)
		{
			btnOk.Visible = true;
		}
	}

	private void frmMostrarMensaje_FormClosing(object sender, FormClosingEventArgs e)
	{
		base.DialogResult = rpta;
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		rpta = DialogResult.OK;
		Close();
	}

	private void btnSi_Click(object sender, EventArgs e)
	{
		rpta = DialogResult.Yes;
		Close();
	}

	private void btnNo_Click(object sender, EventArgs e)
	{
		rpta = DialogResult.No;
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
		this.lblTextoColor = new System.Windows.Forms.Label();
		this.lblTextoNegro = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnOk = new System.Windows.Forms.Button();
		this.btnSi = new System.Windows.Forms.Button();
		this.btnNo = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.lblTextoColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblTextoColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lblTextoColor.Font = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTextoColor.ForeColor = System.Drawing.Color.Red;
		this.lblTextoColor.Location = new System.Drawing.Point(12, 9);
		this.lblTextoColor.Name = "lblTextoColor";
		this.lblTextoColor.Size = new System.Drawing.Size(651, 143);
		this.lblTextoColor.TabIndex = 0;
		this.lblTextoColor.Text = "MENSAJE DE COLOR";
		this.lblTextoColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblTextoNegro.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblTextoNegro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lblTextoNegro.Font = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTextoNegro.Location = new System.Drawing.Point(12, 152);
		this.lblTextoNegro.Name = "lblTextoNegro";
		this.lblTextoNegro.Size = new System.Drawing.Size(651, 114);
		this.lblTextoNegro.TabIndex = 1;
		this.lblTextoNegro.Text = "MENSAJE EN NEGRO";
		this.lblTextoNegro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.btnOk);
		this.groupBox1.Controls.Add(this.btnSi);
		this.groupBox1.Controls.Add(this.btnNo);
		this.groupBox1.Location = new System.Drawing.Point(12, 269);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(651, 57);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.btnOk.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOk.Location = new System.Drawing.Point(289, 19);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(75, 23);
		this.btnOk.TabIndex = 2;
		this.btnOk.Text = "OK";
		this.btnOk.UseVisualStyleBackColor = false;
		this.btnOk.Visible = false;
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.btnSi.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnSi.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnSi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnSi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSi.Location = new System.Drawing.Point(425, 19);
		this.btnSi.Name = "btnSi";
		this.btnSi.Size = new System.Drawing.Size(107, 23);
		this.btnSi.TabIndex = 1;
		this.btnSi.Text = "SI";
		this.btnSi.UseVisualStyleBackColor = false;
		this.btnSi.Visible = false;
		this.btnSi.Click += new System.EventHandler(btnSi_Click);
		this.btnNo.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnNo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNo.Location = new System.Drawing.Point(538, 19);
		this.btnNo.Name = "btnNo";
		this.btnNo.Size = new System.Drawing.Size(107, 23);
		this.btnNo.TabIndex = 0;
		this.btnNo.Text = "NO";
		this.btnNo.UseVisualStyleBackColor = false;
		this.btnNo.Visible = false;
		this.btnNo.Click += new System.EventHandler(btnNo_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(675, 338);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.lblTextoNegro);
		base.Controls.Add(this.lblTextoColor);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmMostrarMensaje";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmMostrarMensaje";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmMostrarMensaje_FormClosing);
		base.Load += new System.EventHandler(frmMostrarMensaje_Load);
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
