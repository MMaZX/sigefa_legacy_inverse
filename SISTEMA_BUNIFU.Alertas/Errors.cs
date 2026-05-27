using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bunifu.Framework.UI;

namespace SISTEMA_BUNIFU.Alertas;

public class Errors : Form
{
	private IContainer components = null;

	private BunifuFlatButton btnOk;

	private Label lblMensaje;

	private PictureBox icon;

	private BunifuElipse bunifuElipse1;

	public Errors(string Mensaje)
	{
		InitializeComponent();
		lblMensaje.Text = Mensaje;
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Errors_Load(object sender, EventArgs e)
	{
		btnOk.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SISTEMA_BUNIFU.Alertas.Errors));
		this.btnOk = new Bunifu.Framework.UI.BunifuFlatButton();
		this.lblMensaje = new System.Windows.Forms.Label();
		this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
		this.icon = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.icon).BeginInit();
		base.SuspendLayout();
		this.btnOk.Activecolor = System.Drawing.Color.FromArgb(122, 31, 26);
		this.btnOk.BackColor = System.Drawing.Color.FromArgb(206, 53, 44);
		this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnOk.BorderRadius = 5;
		this.btnOk.ButtonText = "OK";
		this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnOk.DisabledColor = System.Drawing.Color.Gray;
		this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
		this.btnOk.Iconcolor = System.Drawing.Color.Transparent;
		this.btnOk.Iconimage = null;
		this.btnOk.Iconimage_right = null;
		this.btnOk.Iconimage_right_Selected = null;
		this.btnOk.Iconimage_Selected = null;
		this.btnOk.IconMarginLeft = 0;
		this.btnOk.IconMarginRight = 0;
		this.btnOk.IconRightVisible = true;
		this.btnOk.IconRightZoom = 0.0;
		this.btnOk.IconVisible = true;
		this.btnOk.IconZoom = 90.0;
		this.btnOk.IsTab = false;
		this.btnOk.Location = new System.Drawing.Point(85, 169);
		this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.btnOk.Name = "btnOk";
		this.btnOk.Normalcolor = System.Drawing.Color.FromArgb(206, 53, 44);
		this.btnOk.OnHovercolor = System.Drawing.Color.FromArgb(206, 53, 44);
		this.btnOk.OnHoverTextColor = System.Drawing.Color.White;
		this.btnOk.selected = false;
		this.btnOk.Size = new System.Drawing.Size(224, 47);
		this.btnOk.TabIndex = 8;
		this.btnOk.Text = "OK";
		this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnOk.Textcolor = System.Drawing.Color.White;
		this.btnOk.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.lblMensaje.Font = new System.Drawing.Font("Lucida Sans", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMensaje.ForeColor = System.Drawing.Color.Gray;
		this.lblMensaje.Location = new System.Drawing.Point(1, 104);
		this.lblMensaje.Name = "lblMensaje";
		this.lblMensaje.Size = new System.Drawing.Size(397, 60);
		this.lblMensaje.TabIndex = 7;
		this.lblMensaje.Text = "Error";
		this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.bunifuElipse1.ElipseRadius = 5;
		this.bunifuElipse1.TargetControl = this;
		this.icon.BackColor = System.Drawing.Color.Transparent;
		this.icon.Enabled = false;
		this.icon.Image = (System.Drawing.Image)resources.GetObject("icon.Image");
		this.icon.Location = new System.Drawing.Point(67, 12);
		this.icon.Name = "icon";
		this.icon.Size = new System.Drawing.Size(262, 89);
		this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.icon.TabIndex = 6;
		this.icon.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(400, 226);
		base.Controls.Add(this.btnOk);
		base.Controls.Add(this.lblMensaje);
		base.Controls.Add(this.icon);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "Errors";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Errors";
		base.Load += new System.EventHandler(Errors_Load);
		((System.ComponentModel.ISupportInitialize)this.icon).EndInit();
		base.ResumeLayout(false);
	}
}
