using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using SIGEFA.Formularios;
using SIGEFA.Properties;

namespace SIGEFA.Alertas;

public class Notification : Form
{
	private int interval = 0;

	private IContainer components = null;

	private Label label1;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private BunifuElipse bunifuElipse1;

	private Panel panel1;

	private BunifuDragControl bunifuDragControl1;

	private Timer timer1;

	private Timer timer2;

	private Timer timer3;

	public Notification()
	{
		InitializeComponent();
	}

	private void Notification_Load(object sender, EventArgs e)
	{
		base.Top = -1 * base.Height;
		base.Left = Screen.PrimaryScreen.Bounds.Width - base.Width - 60;
		timer2.Start();
	}

	private void pictureBox2_Click(object sender, EventArgs e)
	{
		timer3.Start();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		timer3.Start();
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		if (base.Top < 60)
		{
			base.Top += interval;
			interval += 2;
		}
		else
		{
			timer2.Stop();
		}
	}

	private void timer3_Tick(object sender, EventArgs e)
	{
		Close();
	}

	private void Notification_Shown(object sender, EventArgs e)
	{
		timer3.Start();
	}

	private void label1_MouseHover(object sender, EventArgs e)
	{
		label1.Text = "Enviar a SUNAT..!";
	}

	private void label1_Click(object sender, EventArgs e)
	{
		frmEnvioSunat form = new frmEnvioSunat();
		form.Show();
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
		this.label1 = new System.Windows.Forms.Label();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
		this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.timer3 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.FromArgb(55, 159, 192);
		this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.label1.Font = new System.Drawing.Font("Century", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(33, 33);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(305, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "TIENE COMPROBANTES PENDIENTES DE ENVIO";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label1.Click += new System.EventHandler(label1_Click);
		this.label1.MouseHover += new System.EventHandler(label1_MouseHover);
		this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(91, 192, 222);
		this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.pictureBox2.Image = SIGEFA.Properties.Resources.multiply_40px;
		this.pictureBox2.Location = new System.Drawing.Point(328, 0);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(19, 17);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox2.TabIndex = 2;
		this.pictureBox2.TabStop = false;
		this.pictureBox2.Click += new System.EventHandler(pictureBox2_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(55, 159, 192);
		this.pictureBox1.Image = SIGEFA.Properties.Resources.checked_40;
		this.pictureBox1.Location = new System.Drawing.Point(3, 21);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(27, 37);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 1;
		this.pictureBox1.TabStop = false;
		this.bunifuElipse1.ElipseRadius = 5;
		this.bunifuElipse1.TargetControl = this;
		this.bunifuDragControl1.Fixed = true;
		this.bunifuDragControl1.Horizontal = true;
		this.bunifuDragControl1.TargetControl = this.panel1;
		this.bunifuDragControl1.Vertical = true;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(55, 159, 192);
		this.panel1.Controls.Add(this.pictureBox2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(350, 78);
		this.panel1.TabIndex = 3;
		this.timer1.Interval = 5000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.timer2.Interval = 50;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		this.timer3.Interval = 4000;
		this.timer3.Tick += new System.EventHandler(timer3_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(37, 127, 88);
		base.ClientSize = new System.Drawing.Size(350, 78);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "Notification";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Notification";
		base.Load += new System.EventHandler(Notification_Load);
		base.Shown += new System.EventHandler(Notification_Shown);
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
