using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmVigenciaCotizacion : Office2007Form
{
	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	private clsValidar ok = new clsValidar();

	public int Proceso = 0;

	public int CodRequerimiento = 0;

	public int serie = 0;

	public int Procede = 0;

	public string numeracion;

	private clsAdmRequerimiento AdmReq = new clsAdmRequerimiento();

	private clsAdmNotaIngreso admNotaIngreso = new clsAdmNotaIngreso();

	private IContainer components = null;

	private TextBox txtDiasVigencia;

	private Label label1;

	private ImageList imageList1;

	private Button btnSalir;

	private Button btnGuardar;

	private Label label2;

	public TextBox txtComentario;

	public GroupBox groupBox2;

	public GroupBox groupBox1;

	private CheckBox chbfacturasVencidas;

	public frmVigenciaCotizacion()
	{
		InitializeComponent();
	}

	private void frmVigenciaCotizacion_Load(object sender, EventArgs e)
	{
		if (Proceso == 0)
		{
			CargaConfiguracion();
		}
	}

	private void CargaConfiguracion()
	{
		if (frmLogin.Configuracion != null)
		{
			txtDiasVigencia.Text = frmLogin.Configuracion.DiasVigencia.ToString();
			chbfacturasVencidas.Checked = frmLogin.Configuracion.FacturasVencidas;
		}
		else
		{
			frmLogin.Configuracion = new clsParametros();
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (Proceso == 0)
		{
			if (txtDiasVigencia.Text != "")
			{
				frmLogin.Configuracion.DiasVigencia = Convert.ToInt32(txtDiasVigencia.Text);
				frmLogin.Configuracion.FacturasVencidas = chbfacturasVencidas.Checked;
			}
			if (AdmEmp.UpdateConfiguracion(frmLogin.Configuracion))
			{
				frmLogin.Configuracion = AdmEmp.CargaConfiguracion();
				MessageBox.Show("Los datos se guardaron correctamente", "Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			Close();
		}
		else
		{
			if (Proceso != 2)
			{
				return;
			}
			if (Procede == 1)
			{
				if (txtComentario.Text != "")
				{
					if (txtComentario.Text != "" && AdmReq.rechazado(CodRequerimiento, txtComentario.Text))
					{
						MessageBox.Show("El requerimiento ha sido rechazado correctamente", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					Close();
					frmRequerimientosVigentes form = (frmRequerimientosVigentes)Application.OpenForms["frmRequerimientosVigentes"];
					form.CargaListaHistorial(frmLogin.iCodAlmacen);
				}
				else
				{
					MessageBox.Show("ingrese Comentario");
				}
			}
			else
			{
				if (Procede != 2)
				{
					return;
				}
				if (txtComentario.Text != "")
				{
					if (txtComentario.Text != "" && admNotaIngreso.anular(serie, numeracion, txtComentario.Text))
					{
						MessageBox.Show("La Transferencia ha sido anulada correctamente", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						MessageBox.Show("Verifique.");
					}
					Close();
				}
				else
				{
					MessageBox.Show("ingrese Comentario");
				}
			}
		}
	}

	private void txtDiasVigencia_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosEnteros(e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVigenciaCotizacion));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.chbfacturasVencidas = new System.Windows.Forms.CheckBox();
		this.txtDiasVigencia = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.groupBox1.Controls.Add(this.chbfacturasVencidas);
		this.groupBox1.Controls.Add(this.txtDiasVigencia);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(281, 85);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Vigencia de Cotizacion";
		this.chbfacturasVencidas.AutoSize = true;
		this.chbfacturasVencidas.Location = new System.Drawing.Point(28, 62);
		this.chbfacturasVencidas.Name = "chbfacturasVencidas";
		this.chbfacturasVencidas.Size = new System.Drawing.Size(188, 17);
		this.chbfacturasVencidas.TabIndex = 2;
		this.chbfacturasVencidas.Text = "Activar Ventas Con F/B Vencidas:";
		this.chbfacturasVencidas.UseVisualStyleBackColor = true;
		this.txtDiasVigencia.Location = new System.Drawing.Point(127, 22);
		this.txtDiasVigencia.MaxLength = 2;
		this.txtDiasVigencia.Name = "txtDiasVigencia";
		this.txtDiasVigencia.Size = new System.Drawing.Size(100, 20);
		this.txtDiasVigencia.TabIndex = 1;
		this.txtDiasVigencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiasVigencia_KeyPress);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(16, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(90, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Dias de Vigencia:";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(159, 103);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(69, 23);
		this.btnSalir.TabIndex = 21;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 1;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(78, 103);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(75, 23);
		this.btnGuardar.TabIndex = 20;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.groupBox2.Controls.Add(this.txtComentario);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Location = new System.Drawing.Point(12, 11);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(314, 85);
		this.groupBox2.TabIndex = 22;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Requerimiento";
		this.groupBox2.Visible = false;
		this.txtComentario.Location = new System.Drawing.Point(75, 8);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(233, 71);
		this.txtComentario.TabIndex = 1;
		this.label2.Location = new System.Drawing.Point(6, 23);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(63, 36);
		this.label2.TabIndex = 0;
		this.label2.Text = "Razón de Rechazo:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(338, 134);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVigenciaCotizacion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Parametros";
		base.Load += new System.EventHandler(frmVigenciaCotizacion_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
