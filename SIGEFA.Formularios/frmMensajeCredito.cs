using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmMensajeCredito : RadForm
{
	private IContainer components = null;

	private RadLabel radLabel1;

	private MaterialTheme materialTheme1;

	private RadLabel radLabel2;

	private RadButton radButton1;

	public frmMensajeCredito()
	{
		InitializeComponent();
	}

	private void radButton1_Click(object sender, EventArgs e)
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
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
		this.radButton1 = new Telerik.WinControls.UI.RadButton();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.radLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f);
		this.radLabel1.Location = new System.Drawing.Point(53, 38);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(362, 27);
		this.radLabel1.TabIndex = 0;
		this.radLabel1.Text = "Los datos se guardaron correctamente";
		this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.radLabel1.ThemeName = "Material";
		this.radLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 35f);
		this.radLabel2.ForeColor = System.Drawing.Color.Red;
		this.radLabel2.Location = new System.Drawing.Point(12, 92);
		this.radLabel2.Name = "radLabel2";
		this.radLabel2.Size = new System.Drawing.Size(452, 61);
		this.radLabel2.TabIndex = 1;
		this.radLabel2.Text = "VENTA A CREDITO";
		this.radLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.radLabel2.ThemeName = "Material";
		this.radButton1.Location = new System.Drawing.Point(168, 183);
		this.radButton1.Name = "radButton1";
		this.radButton1.Size = new System.Drawing.Size(120, 25);
		this.radButton1.TabIndex = 2;
		this.radButton1.Text = "Aceptar";
		this.radButton1.ThemeName = "Fluent";
		this.radButton1.Click += new System.EventHandler(radButton1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(465, 225);
		base.Controls.Add(this.radButton1);
		base.Controls.Add(this.radLabel2);
		base.Controls.Add(this.radLabel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmMensajeCredito";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ventas";
		base.ThemeName = "Fluent";
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
