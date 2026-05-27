using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmAgregaInformacionProducto : Office2007Form
{
	private int codigoDetPedido;

	private int codigoFamilia;

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	public bool cancelado = false;

	private IContainer components = null;

	private Label lblSerie;

	private TextBox txtSerie;

	private TextBox txtNChasis;

	private Label lblNroChasis;

	private TextBox txtModelo;

	private Label lblModelo;

	private TextBox txtMarca;

	private Label lblMarca;

	private TextBox txtColor;

	private Label lblColor;

	private Button btnGuardar;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Button btnCancelar;

	public frmAgregaInformacionProducto(int CodDetallePedido, int CodFamilia)
	{
		codigoDetPedido = CodDetallePedido;
		codigoFamilia = CodFamilia;
		InitializeComponent();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			clsDetallePedido detallePedido = new clsDetallePedido
			{
				CodDetallePedido = codigoDetPedido,
				SerieMotor = txtSerie.Text.Trim(),
				NroChasis = txtNChasis.Text.Trim(),
				Modelo = txtModelo.Text.Trim(),
				Marca = txtMarca.Text.Trim(),
				Color = txtColor.Text.Trim()
			};
			if (AdmPedido.updatedetalleadicional(detallePedido))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Detalle de Pedido", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				MessageBox.Show("Ocurrió un problema al realizar la operación", "Detalle de Pedido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message.ToString());
		}
	}

	private void txtSerie_Validating(object sender, CancelEventArgs e)
	{
		if (codigoFamilia == 302 || codigoFamilia == 414)
		{
			if (string.IsNullOrEmpty(txtSerie.Text))
			{
				e.Cancel = true;
				errorProvider1.SetError(txtSerie, "Este campo es requerido");
				highlighter1.SetHighlightColor(txtSerie, eHighlightColor.Red);
			}
			else
			{
				errorProvider1.SetError(txtSerie, "");
			}
		}
	}

	private void txtNChasis_Validating(object sender, CancelEventArgs e)
	{
		if (codigoFamilia == 544)
		{
			if (string.IsNullOrEmpty(txtNChasis.Text))
			{
				e.Cancel = true;
				errorProvider1.SetError(txtNChasis, "Este campo es requerido");
				highlighter1.SetHighlightColor(txtNChasis, eHighlightColor.Red);
			}
			else
			{
				errorProvider1.SetError(txtNChasis, "");
			}
		}
	}

	private void txtModelo_Validating(object sender, CancelEventArgs e)
	{
		if (codigoFamilia == 544)
		{
			if (string.IsNullOrEmpty(txtModelo.Text))
			{
				e.Cancel = true;
				errorProvider1.SetError(txtModelo, "Este campo es requerido");
				highlighter1.SetHighlightColor(txtModelo, eHighlightColor.Red);
			}
			else
			{
				errorProvider1.SetError(txtModelo, "");
			}
		}
	}

	private void txtMarca_Validating(object sender, CancelEventArgs e)
	{
		if (codigoFamilia == 544)
		{
			if (string.IsNullOrEmpty(txtMarca.Text))
			{
				e.Cancel = true;
				errorProvider1.SetError(txtMarca, "Este campo es requerido");
				highlighter1.SetHighlightColor(txtMarca, eHighlightColor.Red);
			}
			else
			{
				errorProvider1.SetError(txtMarca, "");
			}
		}
	}

	private void txtColor_Validating(object sender, CancelEventArgs e)
	{
		if (codigoFamilia == 544)
		{
			if (string.IsNullOrEmpty(txtColor.Text))
			{
				e.Cancel = true;
				errorProvider1.SetError(txtColor, "Este campo es requerido");
				highlighter1.SetHighlightColor(txtColor, eHighlightColor.Red);
			}
			else
			{
				errorProvider1.SetError(txtColor, "");
			}
		}
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnGuardar.PerformClick();
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		cancelado = true;
		Close();
	}

	private void frmAgregaInformacionProducto_Load(object sender, EventArgs e)
	{
		switch (codigoFamilia)
		{
		case 302:
			lblNroChasis.Enabled = false;
			txtNChasis.Enabled = false;
			lblModelo.Enabled = false;
			txtModelo.Enabled = false;
			lblMarca.Enabled = false;
			txtMarca.Enabled = false;
			lblColor.Enabled = false;
			txtColor.Enabled = false;
			txtSerie.Focus();
			break;
		case 414:
			lblNroChasis.Enabled = false;
			txtNChasis.Enabled = false;
			lblModelo.Enabled = false;
			txtModelo.Enabled = false;
			lblMarca.Enabled = false;
			txtMarca.Enabled = false;
			lblColor.Enabled = false;
			txtColor.Enabled = false;
			txtSerie.Focus();
			break;
		case 544:
			lblSerie.Enabled = false;
			txtSerie.Enabled = false;
			txtNChasis.Focus();
			break;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmAgregaInformacionProducto));
		this.lblSerie = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtNChasis = new System.Windows.Forms.TextBox();
		this.lblNroChasis = new System.Windows.Forms.Label();
		this.txtModelo = new System.Windows.Forms.TextBox();
		this.lblModelo = new System.Windows.Forms.Label();
		this.txtMarca = new System.Windows.Forms.TextBox();
		this.lblMarca = new System.Windows.Forms.Label();
		this.txtColor = new System.Windows.Forms.TextBox();
		this.lblColor = new System.Windows.Forms.Label();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.lblSerie.AutoSize = true;
		this.lblSerie.Location = new System.Drawing.Point(22, 17);
		this.lblSerie.Name = "lblSerie";
		this.lblSerie.Size = new System.Drawing.Size(156, 13);
		this.lblSerie.TabIndex = 0;
		this.lblSerie.Text = "SERIE DE MOTOR O CHASIS:";
		this.txtSerie.Location = new System.Drawing.Point(25, 33);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.Size = new System.Drawing.Size(310, 20);
		this.txtSerie.TabIndex = 1;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.Validating += new System.ComponentModel.CancelEventHandler(txtSerie_Validating);
		this.txtNChasis.Location = new System.Drawing.Point(25, 80);
		this.txtNChasis.Name = "txtNChasis";
		this.txtNChasis.Size = new System.Drawing.Size(310, 20);
		this.txtNChasis.TabIndex = 3;
		this.txtNChasis.Validating += new System.ComponentModel.CancelEventHandler(txtNChasis_Validating);
		this.lblNroChasis.AutoSize = true;
		this.lblNroChasis.Location = new System.Drawing.Point(22, 64);
		this.lblNroChasis.Name = "lblNroChasis";
		this.lblNroChasis.Size = new System.Drawing.Size(82, 13);
		this.lblNroChasis.TabIndex = 2;
		this.lblNroChasis.Text = "Nº DE CHASIS:";
		this.txtModelo.Location = new System.Drawing.Point(25, 130);
		this.txtModelo.Name = "txtModelo";
		this.txtModelo.Size = new System.Drawing.Size(310, 20);
		this.txtModelo.TabIndex = 5;
		this.txtModelo.Validating += new System.ComponentModel.CancelEventHandler(txtModelo_Validating);
		this.lblModelo.AutoSize = true;
		this.lblModelo.Location = new System.Drawing.Point(22, 114);
		this.lblModelo.Name = "lblModelo";
		this.lblModelo.Size = new System.Drawing.Size(56, 13);
		this.lblModelo.TabIndex = 4;
		this.lblModelo.Text = "MODELO:";
		this.txtMarca.Location = new System.Drawing.Point(25, 177);
		this.txtMarca.Name = "txtMarca";
		this.txtMarca.Size = new System.Drawing.Size(310, 20);
		this.txtMarca.TabIndex = 7;
		this.txtMarca.Validating += new System.ComponentModel.CancelEventHandler(txtMarca_Validating);
		this.lblMarca.AutoSize = true;
		this.lblMarca.Location = new System.Drawing.Point(22, 161);
		this.lblMarca.Name = "lblMarca";
		this.lblMarca.Size = new System.Drawing.Size(48, 13);
		this.lblMarca.TabIndex = 6;
		this.lblMarca.Text = "MARCA:";
		this.txtColor.Location = new System.Drawing.Point(25, 224);
		this.txtColor.Name = "txtColor";
		this.txtColor.Size = new System.Drawing.Size(310, 20);
		this.txtColor.TabIndex = 9;
		this.txtColor.Validating += new System.ComponentModel.CancelEventHandler(txtColor_Validating);
		this.lblColor.AutoSize = true;
		this.lblColor.Location = new System.Drawing.Point(22, 208);
		this.lblColor.Name = "lblColor";
		this.lblColor.Size = new System.Drawing.Size(47, 13);
		this.lblColor.TabIndex = 8;
		this.lblColor.Text = "COLOR:";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = SIGEFA.Properties.Resources.save;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.Location = new System.Drawing.Point(139, 262);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(93, 28);
		this.btnGuardar.TabIndex = 10;
		this.btnGuardar.Text = "GUARDAR";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Image = SIGEFA.Properties.Resources.x_button;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.Location = new System.Drawing.Point(238, 262);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(97, 28);
		this.btnCancelar.TabIndex = 11;
		this.btnCancelar.Text = "CANCELAR";
		this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(368, 312);
		base.ControlBox = false;
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.txtColor);
		base.Controls.Add(this.lblColor);
		base.Controls.Add(this.txtMarca);
		base.Controls.Add(this.lblMarca);
		base.Controls.Add(this.txtModelo);
		base.Controls.Add(this.lblModelo);
		base.Controls.Add(this.txtNChasis);
		base.Controls.Add(this.lblNroChasis);
		base.Controls.Add(this.txtSerie);
		base.Controls.Add(this.lblSerie);
		this.DoubleBuffered = true;
		base.Name = "frmAgregaInformacionProducto";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "AGREGUE INFORMACIÓN DEL PRODUCTO";
		base.Load += new System.EventHandler(frmAgregaInformacionProducto_Load);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
