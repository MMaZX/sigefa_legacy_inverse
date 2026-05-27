using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmBusqueda : Office2007Form
{
	private IContainer components = null;

	public TextBox txtFiltro;

	public Label label1;

	public Label label2;

	private Label label3;

	public frmBusqueda()
	{
		InitializeComponent();
	}

	private void frmBusqueda_Shown(object sender, EventArgs e)
	{
		EffectIn();
	}

	private void frmBusqueda_Load(object sender, EventArgs e)
	{
		base.Height = 0;
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				frmUsuarios.data.Filter = $"[{label2.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				frmUsuarios.data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void frmBusqueda_FormClosing(object sender, FormClosingEventArgs e)
	{
		EffectOut();
	}

	public void EffectOut()
	{
		for (int Effect = base.Height; Effect > 0; Effect--)
		{
			base.Height = Effect;
			Refresh();
		}
	}

	public void EffectIn()
	{
		int size = 93;
		for (int Effect = 0; Effect < size; Effect++)
		{
			base.Height = Effect;
			Refresh();
		}
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
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
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.txtFiltro.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFiltro.Location = new System.Drawing.Point(12, 37);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(201, 20);
		this.txtFiltro.TabIndex = 0;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(47, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(47, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "label1";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(178, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "label2";
		this.label2.Visible = false;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 9);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(29, 13);
		this.label3.TabIndex = 3;
		this.label3.Text = "Por :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.CaptionFont = new System.Drawing.Font("Lucida Sans", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.ClientSize = new System.Drawing.Size(225, 69);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtFiltro);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmBusqueda";
		base.Opacity = 0.8;
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Busqueda";
		base.TopMost = true;
		base.Load += new System.EventHandler(frmBusqueda_Load);
		base.Shown += new System.EventHandler(frmBusqueda_Shown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmBusqueda_FormClosing);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
