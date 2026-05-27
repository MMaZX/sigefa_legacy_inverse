using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using SIGEFA.Properties;

namespace SIGEFA.Alertas;

public class Success : Form
{
	private IContainer components = null;

	private BunifuElipse bunifuElipse1;

	private BunifuFlatButton btnOk;

	private Label lblMensaje;

	private PictureBox icon;

	private Timer icon_delay;

	public Success(string mensaje)
	{
		InitializeComponent();
		lblMensaje.Text = mensaje;
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Success_Load(object sender, EventArgs e)
	{
		icon_delay.Start();
		icon.Enabled = true;
	}

	private void Success_Shown(object sender, EventArgs e)
	{
		icon_delay.Stop();
		btnOk.Focus();
	}

	private void icon_delay_Tick(object sender, EventArgs e)
	{
		icon.Enabled = false;
		icon_delay.Stop();
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
		this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
		this.btnOk = new Bunifu.Framework.UI.BunifuFlatButton();
		this.lblMensaje = new System.Windows.Forms.Label();
		this.icon_delay = new System.Windows.Forms.Timer(this.components);
		this.icon = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.icon).BeginInit();
		base.SuspendLayout();
		this.bunifuElipse1.ElipseRadius = 5;
		this.bunifuElipse1.TargetControl = this;
		this.btnOk.Activecolor = System.Drawing.Color.FromArgb(46, 139, 87);
		this.btnOk.BackColor = System.Drawing.Color.FromArgb(119, 180, 63);
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
		this.btnOk.Location = new System.Drawing.Point(96, 174);
		this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.btnOk.Name = "btnOk";
		this.btnOk.Normalcolor = System.Drawing.Color.FromArgb(119, 180, 63);
		this.btnOk.OnHovercolor = System.Drawing.Color.FromArgb(119, 180, 63);
		this.btnOk.OnHoverTextColor = System.Drawing.Color.White;
		this.btnOk.selected = false;
		this.btnOk.Size = new System.Drawing.Size(224, 47);
		this.btnOk.TabIndex = 6;
		this.btnOk.Text = "OK";
		this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnOk.Textcolor = System.Drawing.Color.White;
		this.btnOk.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.lblMensaje.Font = new System.Drawing.Font("Lucida Sans", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMensaje.ForeColor = System.Drawing.Color.Gray;
		this.lblMensaje.Location = new System.Drawing.Point(0, 110);
		this.lblMensaje.Name = "lblMensaje";
		this.lblMensaje.Size = new System.Drawing.Size(400, 60);
		this.lblMensaje.TabIndex = 5;
		this.lblMensaje.Text = "Satisfactorio";
		this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.icon_delay.Interval = 1000;
		this.icon_delay.Tick += new System.EventHandler(icon_delay_Tick);
		this.icon.Enabled = false;
		this.icon.Image = SIGEFA.Properties.Resources.ok_100px;
		this.icon.Location = new System.Drawing.Point(146, 2);
		this.icon.Name = "icon";
		this.icon.Size = new System.Drawing.Size(103, 118);
		this.icon.TabIndex = 4;
		this.icon.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(400, 226);
		base.Controls.Add(this.btnOk);
		base.Controls.Add(this.lblMensaje);
		base.Controls.Add(this.icon);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "Success";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Success";
		base.Load += new System.EventHandler(Success_Load);
		base.Shown += new System.EventHandler(Success_Shown);
		((System.ComponentModel.ISupportInitialize)this.icon).EndInit();
		base.ResumeLayout(false);
	}
}
