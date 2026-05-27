using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmConfiguracionOrdenDeVenta : Form
{
	private clsValidar valida = new clsValidar();

	private clsAdmNewConfiguracion newConfig = new clsAdmNewConfiguracion();

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox txtDiasOVConReq;

	private Label label1;

	private Label label3;

	private TextBox txtDiasOVSinReq;

	private Label label4;

	private Label label2;

	private Button btnGuardar;

	public frmConfiguracionOrdenDeVenta()
	{
		InitializeComponent();
	}

	private void frmConfiguracionOrdenDeVenta_Load(object sender, EventArgs e)
	{
		verificaRangoDiasOVConReq();
		verificaRangoDiasOVSinReq();
	}

	private void verificaRangoDiasOVSinReq()
	{
		int rpta = newConfig.getConfiguracion("DIASELIMINARSR", "ORDENDEVENTA");
		if (rpta != -666)
		{
			txtDiasOVSinReq.Text = rpta.ToString();
		}
	}

	private void verificaRangoDiasOVConReq()
	{
		int rpta = newConfig.getConfiguracion("DIASELIMINARCR", "ORDENDEVENTA");
		if (rpta != -666)
		{
			txtDiasOVConReq.Text = rpta.ToString();
		}
	}

	private void txtDiasOVConReq_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.SOLONumeros(sender, e);
	}

	private void txtDiasOVSinReq_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.SOLONumeros(sender, e);
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			bool rpta = procedimientoRangoDiasOVConReq();
			bool rpta2 = procedimientoRangoDiasOVSinReq();
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

	private bool procedimientoRangoDiasOVSinReq()
	{
		bool rpta = false;
		if (txtDiasOVSinReq.Text != "")
		{
			if (Convert.ToInt32(txtDiasOVSinReq.Text) <= 1)
			{
				MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta sin requerimiento no debe ser mayor a 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			int dato = newConfig.getConfiguracion("DIASELIMINARSR", "ORDENDEVENTA");
			if (dato != -666)
			{
				rpta = newConfig.actualizaConfiguracion("DIASELIMINARSR", "ORDENDEVENTA", txtDiasOVSinReq.Text);
				if (!rpta)
				{
					MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta sin requerimiento no se actualizo correctamente. Verifique en la bd", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				rpta = newConfig.guardaConfiguracion("DIASELIMINARSR", txtDiasOVSinReq.Text, "ORDENDEVENTA", "indica el max de dias hacia atras desde que se debe eliminar orden de venta con requerimiento");
				if (!rpta)
				{
					MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta sin requerimiento no se guardo correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		else
		{
			rpta = newConfig.eliminaConfiguracion("DIASELIMINARSR", "ORDENDEVENTA");
			if (!rpta)
			{
				MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta sin requerimiento no se pudo elimanr correctamente. Verifique en la bd", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return rpta;
	}

	private bool procedimientoRangoDiasOVConReq()
	{
		bool rpta = false;
		if (txtDiasOVConReq.Text != "")
		{
			if (Convert.ToInt32(txtDiasOVConReq.Text) <= 1)
			{
				MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta con requerimiento no debe ser mayor a 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			int dato = newConfig.getConfiguracion("DIASELIMINARCR", "ORDENDEVENTA");
			if (dato != -666)
			{
				rpta = newConfig.actualizaConfiguracion("DIASELIMINARCR", "ORDENDEVENTA", txtDiasOVConReq.Text);
				if (!rpta)
				{
					MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta con requerimiento no se actualizo correctamente. Verifique en la bd", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				rpta = newConfig.guardaConfiguracion("DIASELIMINARCR", txtDiasOVConReq.Text, "ORDENDEVENTA", "indica el max de dias hacia atras desde que se debe eliminar orden de venta con requerimiento");
				if (!rpta)
				{
					MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta con requerimiento no se guardo correctamenet", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		else
		{
			rpta = newConfig.eliminaConfiguracion("DIASELIMINARCR", "ORDENDEVENTA");
			if (!rpta)
			{
				MessageBox.Show("La configuracion correspondiente a dias para eliminar orden de venta con requerimiento no se pudo elimanr correctamente. Verifique en la bd", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		this.label3 = new System.Windows.Forms.Label();
		this.txtDiasOVSinReq = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtDiasOVConReq = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtDiasOVSinReq);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtDiasOVConReq);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(562, 100);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Definicion de Cantidad de Dias Para Eliminar Ordenes de Venta";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(498, 56);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(26, 13);
		this.label3.TabIndex = 5;
		this.label3.Text = "dias";
		this.txtDiasOVSinReq.Location = new System.Drawing.Point(392, 53);
		this.txtDiasOVSinReq.MaxLength = 11;
		this.txtDiasOVSinReq.Name = "txtDiasOVSinReq";
		this.txtDiasOVSinReq.Size = new System.Drawing.Size(100, 20);
		this.txtDiasOVSinReq.TabIndex = 4;
		this.txtDiasOVSinReq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDiasOVSinReq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiasOVSinReq_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(12, 56);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(374, 13);
		this.label4.TabIndex = 3;
		this.label4.Text = "Eliminar Ordenes de Venta sin Requerimietno de Almacen desde hace más de";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(503, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(26, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "dias";
		this.txtDiasOVConReq.Location = new System.Drawing.Point(397, 24);
		this.txtDiasOVConReq.MaxLength = 11;
		this.txtDiasOVConReq.Name = "txtDiasOVConReq";
		this.txtDiasOVConReq.Size = new System.Drawing.Size(100, 20);
		this.txtDiasOVConReq.TabIndex = 1;
		this.txtDiasOVConReq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDiasOVConReq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiasOVConReq_KeyPress);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 27);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(379, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Eliminar Ordenes de Venta con Requerimietno de Almacen desde hace más de";
		this.btnGuardar.Location = new System.Drawing.Point(474, 262);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(76, 31);
		this.btnGuardar.TabIndex = 1;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(562, 305);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		this.MaximumSize = new System.Drawing.Size(578, 344);
		this.MinimumSize = new System.Drawing.Size(578, 344);
		base.Name = "frmConfiguracionOrdenDeVenta";
		this.Text = "Configuracion de Orden De Venta";
		base.Load += new System.EventHandler(frmConfiguracionOrdenDeVenta_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
