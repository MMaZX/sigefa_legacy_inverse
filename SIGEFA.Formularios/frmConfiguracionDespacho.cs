using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmConfiguracionDespacho : Form
{
	private clsValidar valida = new clsValidar();

	private clsAdmNewConfiguracion newConfig = new clsAdmNewConfiguracion();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private Button btnGuardar;

	private Label label2;

	private TextBox txtNroDiasRango;

	public frmConfiguracionDespacho()
	{
		InitializeComponent();
	}

	private void frmConfiguracionDespacho_Load(object sender, EventArgs e)
	{
		verificaRangoDias();
	}

	private void verificaRangoDias()
	{
		int rpta = newConfig.getConfiguracion("DIASCREAR", "DESPACHO");
		if (rpta != -666)
		{
			txtNroDiasRango.Text = rpta.ToString();
		}
	}

	private void txtNroDiasRango_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.SOLONumeros(sender, e);
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			bool rpta = procedimientoRangoDias();
			bool rpta2 = nuevoprocedimiento();
			if (rpta && rpta2)
			{
				MessageBox.Show("Guardado Correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Todo lo demas se guardo correctamente.", "informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool nuevoprocedimiento()
	{
		return true;
	}

	private bool procedimientoRangoDias()
	{
		bool rpta = false;
		if (txtNroDiasRango.Text != "")
		{
			if (Convert.ToInt32(txtNroDiasRango.Text) <= 0)
			{
				MessageBox.Show("El rango de dias para crear un despacho debe ser mayor a cero", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			int dato = newConfig.getConfiguracion("DIASCREAR", "DESPACHO");
			if (dato != -666)
			{
				rpta = newConfig.actualizaConfiguracion("DIASCREAR", "DESPACHO", txtNroDiasRango.Text);
				if (!rpta)
				{
					MessageBox.Show("La configuracion correspondiente a dias para crear despacho no se actualizo correctamente. Verifique en la bd", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				rpta = newConfig.guardaConfiguracion("DIASCREAR", txtNroDiasRango.Text, "DESPACHO", "indica el max de dias hacia atras desde que se puede crear un despacho");
				if (!rpta)
				{
					MessageBox.Show("La configuracion correspondiente a dias para crear despacho no se guardo correctamenet", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		else
		{
			rpta = newConfig.eliminaConfiguracion("DIASCREAR", "DESPACHO");
			if (!rpta)
			{
				MessageBox.Show("La configuracion correspondiente a dias para crear despacho no se pudo elimanr correctamente. Verifique en la bd", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return rpta;
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtNroDiasRango = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtNroDiasRango);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(479, 50);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(308, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Indicar cantidad de dias desde que se puede crear despachos: ";
		this.btnGuardar.Location = new System.Drawing.Point(383, 206);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(84, 36);
		this.btnGuardar.TabIndex = 1;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtNroDiasRango.Location = new System.Drawing.Point(320, 12);
		this.txtNroDiasRango.MaxLength = 10;
		this.txtNroDiasRango.Name = "txtNroDiasRango";
		this.txtNroDiasRango.Size = new System.Drawing.Size(100, 20);
		this.txtNroDiasRango.TabIndex = 1;
		this.txtNroDiasRango.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtNroDiasRango.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNroDiasRango_KeyPress);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(426, 15);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(29, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "dias.";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(479, 254);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.MaximumSize = new System.Drawing.Size(495, 293);
		this.MinimumSize = new System.Drawing.Size(495, 293);
		base.Name = "frmConfiguracionDespacho";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Configuracion de Despacho";
		base.Load += new System.EventHandler(frmConfiguracionDespacho_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
