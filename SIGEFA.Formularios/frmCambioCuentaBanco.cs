using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmCambioCuentaBanco : Form
{
	private clsAdmCtaCte AdmCtaCte = new clsAdmCtaCte();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	public int CodVenta;

	private IContainer components = null;

	private Label label13;

	public ComboBox cboBanco;

	private Label label14;

	public ComboBox cboNumCta;

	private RadButton btnguardar;

	public frmCambioCuentaBanco()
	{
		InitializeComponent();
	}

	private void frmCambioCuentaBanco_Load(object sender, EventArgs e)
	{
		try
		{
			cboBanco.DataSource = AdmCtaCte.ListaBancoxMoneda(Convert.ToInt32(1));
			cboBanco.DisplayMember = "descripcion";
			cboBanco.ValueMember = "codbanco";
			cboBanco.SelectedIndex = -1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cboBanco_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			cboNumCta.Enabled = true;
			CargaCtaCte();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void CargaCtaCte()
	{
		try
		{
			cboNumCta.DataSource = AdmCtaCte.ListaCtaCtexBancoxMoneda(Convert.ToInt32(cboBanco.SelectedValue), Convert.ToInt32(1));
			cboNumCta.DisplayMember = "cuentaCorriente";
			cboNumCta.ValueMember = "codCuentaCorriente";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void btnguardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (AdmVenta.actualizacuentabanco(Convert.ToInt32(cboBanco.SelectedValue), Convert.ToInt32(cboNumCta.SelectedValue), cboNumCta.Text, CodVenta))
			{
				MessageBox.Show("Datos Actualizados", "CAMBIO BANCO Y NUMERO CUENTA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Dispose();
			}
			else
			{
				MessageBox.Show("Datos No Actualizados", "CAMBIO BANCO Y NUMERO CUENTA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		this.label13 = new System.Windows.Forms.Label();
		this.cboBanco = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.cboNumCta = new System.Windows.Forms.ComboBox();
		this.btnguardar = new Telerik.WinControls.UI.RadButton();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).BeginInit();
		base.SuspendLayout();
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(13, 18);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(41, 13);
		this.label13.TabIndex = 82;
		this.label13.Text = "Banco:";
		this.cboBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboBanco.FormattingEnabled = true;
		this.cboBanco.Location = new System.Drawing.Point(58, 15);
		this.cboBanco.Name = "cboBanco";
		this.cboBanco.Size = new System.Drawing.Size(259, 21);
		this.cboBanco.TabIndex = 81;
		this.cboBanco.SelectionChangeCommitted += new System.EventHandler(cboBanco_SelectionChangeCommitted);
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(13, 57);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(41, 13);
		this.label14.TabIndex = 84;
		this.label14.Text = "N° Cta:";
		this.cboNumCta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboNumCta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboNumCta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboNumCta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboNumCta.FormattingEnabled = true;
		this.cboNumCta.Location = new System.Drawing.Point(58, 53);
		this.cboNumCta.Name = "cboNumCta";
		this.cboNumCta.Size = new System.Drawing.Size(259, 21);
		this.cboNumCta.TabIndex = 83;
		this.btnguardar.Location = new System.Drawing.Point(325, 33);
		this.btnguardar.Name = "btnguardar";
		this.btnguardar.Size = new System.Drawing.Size(97, 24);
		this.btnguardar.TabIndex = 85;
		this.btnguardar.Text = "Guardar";
		this.btnguardar.ThemeName = "TelerikMetro";
		this.btnguardar.Click += new System.EventHandler(btnguardar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(434, 92);
		base.Controls.Add(this.btnguardar);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.cboNumCta);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.cboBanco);
		base.Name = "frmCambioCuentaBanco";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Cambio Cuenta Pago";
		base.Load += new System.EventHandler(frmCambioCuentaBanco_Load);
		((System.ComponentModel.ISupportInitialize)this.btnguardar).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
