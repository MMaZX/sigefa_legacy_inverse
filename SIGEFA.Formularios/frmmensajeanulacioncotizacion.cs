using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Properties;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmmensajeanulacioncotizacion : RadForm
{
	public string mensaje = "";

	private IContainer components = null;

	private RadTextBox txtmensaje;

	private RadButton btnsalir;

	private RadButton btnguardar;

	private MaterialTheme materialTheme1;

	public frmmensajeanulacioncotizacion()
	{
		InitializeComponent();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnguardar_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(txtmensaje.Text))
		{
			MessageBox.Show("Agregar Motivo Anulación", "Anular Cotización", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtmensaje.Focus();
		}
		else
		{
			mensaje = txtmensaje.Text.Trim();
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
		this.txtmensaje = new Telerik.WinControls.UI.RadTextBox();
		this.btnsalir = new Telerik.WinControls.UI.RadButton();
		this.btnguardar = new Telerik.WinControls.UI.RadButton();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		((System.ComponentModel.ISupportInitialize)this.txtmensaje).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.txtmensaje.Location = new System.Drawing.Point(12, 12);
		this.txtmensaje.Name = "txtmensaje";
		this.txtmensaje.Size = new System.Drawing.Size(272, 36);
		this.txtmensaje.TabIndex = 0;
		this.txtmensaje.ThemeName = "Material";
		this.btnsalir.Image = SIGEFA.Properties.Resources.ganancia;
		this.btnsalir.Location = new System.Drawing.Point(164, 54);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(120, 36);
		this.btnsalir.TabIndex = 1;
		this.btnsalir.Text = "Salir";
		this.btnsalir.ThemeName = "Material";
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.btnguardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnguardar.Location = new System.Drawing.Point(38, 54);
		this.btnguardar.Name = "btnguardar";
		this.btnguardar.Size = new System.Drawing.Size(120, 36);
		this.btnguardar.TabIndex = 2;
		this.btnguardar.Text = "Guardar";
		this.btnguardar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnguardar.ThemeName = "Material";
		this.btnguardar.Click += new System.EventHandler(btnguardar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(296, 106);
		base.Controls.Add(this.btnguardar);
		base.Controls.Add(this.btnsalir);
		base.Controls.Add(this.txtmensaje);
		base.Name = "frmmensajeanulacioncotizacion";
		base.RootElement.ApplyShapeToControl = true;
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Mensaje Anulacion";
		base.ThemeName = "Material";
		((System.ComponentModel.ISupportInitialize)this.txtmensaje).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
