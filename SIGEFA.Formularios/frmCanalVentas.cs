using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmCanalVentas : Form
{
	private clsAdmFormulario admform = new clsAdmFormulario();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	public string CanalSeleccionado = "0";

	public int CodVenta;

	private IContainer components = null;

	private RadButton btnguardar;

	private TelerikMetroTheme telerikMetroTheme1;

	private ComboBox cmbCanalVenta;

	public frmCanalVentas()
	{
		InitializeComponent();
	}

	private void frmCanalVentas_Load(object sender, EventArgs e)
	{
		try
		{
			cmbCanalVenta.DataSource = admform.listadoTotalCanalesVenta();
			cmbCanalVenta.DisplayMember = "descripcion";
			cmbCanalVenta.ValueMember = "codigo";
			cmbCanalVenta.SelectedValue = CanalSeleccionado;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnguardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (AdmVenta.actualizaCanalVenta(cmbCanalVenta.SelectedValue.ToString(), CodVenta))
			{
				MessageBox.Show("Canal Venta Actualizado", "CANAL VENTA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Dispose();
			}
			else
			{
				MessageBox.Show("Canal Venta No Actualizado", "CANAL VENTA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		this.btnguardar = new Telerik.WinControls.UI.RadButton();
		this.telerikMetroTheme1 = new Telerik.WinControls.Themes.TelerikMetroTheme();
		this.cmbCanalVenta = new System.Windows.Forms.ComboBox();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).BeginInit();
		base.SuspendLayout();
		this.btnguardar.Location = new System.Drawing.Point(227, 7);
		this.btnguardar.Name = "btnguardar";
		this.btnguardar.Size = new System.Drawing.Size(97, 24);
		this.btnguardar.TabIndex = 1;
		this.btnguardar.Text = "Guardar";
		this.btnguardar.ThemeName = "TelerikMetro";
		this.btnguardar.Click += new System.EventHandler(btnguardar_Click);
		this.cmbCanalVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbCanalVenta.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.cmbCanalVenta.FormattingEnabled = true;
		this.cmbCanalVenta.Location = new System.Drawing.Point(12, 10);
		this.cmbCanalVenta.Name = "cmbCanalVenta";
		this.cmbCanalVenta.Size = new System.Drawing.Size(209, 21);
		this.cmbCanalVenta.TabIndex = 187;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(336, 47);
		base.Controls.Add(this.cmbCanalVenta);
		base.Controls.Add(this.btnguardar);
		base.Name = "frmCanalVentas";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmCanalVentas";
		base.Load += new System.EventHandler(frmCanalVentas_Load);
		((System.ComponentModel.ISupportInitialize)this.btnguardar).EndInit();
		base.ResumeLayout(false);
	}
}
