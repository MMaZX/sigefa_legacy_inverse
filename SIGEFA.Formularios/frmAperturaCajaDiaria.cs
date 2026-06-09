using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmAperturaCajaDiaria : Office2007Form
{
	private clsAdmAperturaCierre AdmApe = new clsAdmAperturaCierre();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAlmacen almacen = new clsAlmacen();

	private clsCaja caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	public int tipocaja = 1;

	public int Proceso = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private Button btnaceptar;

	private Button btnsalir;

	private ImageList imageList1;

	private System.Windows.Forms.ToolTip toolTip1;

	public TextBox txtmonto;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	public RadDropDownList cmbAlmacenes;

	public int Codalma { get; set; }

	public frmAperturaCajaDiaria()
	{
		InitializeComponent();
	}

	private void AperturaCaja_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		VerificaSaldoCaja();
		if (Proceso == 0)
		{
			CargarCaja();
		}
		toolTip1.SetToolTip(btnaceptar, "Pulse Aqui Para Aperturar Caja del Día , con el Monto Ingresado");
		toolTip1.SetToolTip(txtmonto, "Ingrese Monto Para la Apertura de Caja Actual");
	}

	private void cargaAlmacenes()
	{
		DataTable aux = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		aux.Rows.RemoveAt(0);
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = aux;
	}

	private void CargarCaja()
	{
		try
		{
			caja = AdmApe.CargaCierreAnterior(frmLogin.iCodSucursal, tipocaja);
			if (caja != null)
			{
				txtmonto.Text = caja.Montocierre.ToString("###.####");
				return;
			}
			caja = new clsCaja();
			txtmonto.Text = "00.00";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
			if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
			{
				e.Handled = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private async void btnaceptar_Click(object sender, EventArgs e)
	{
		btnaceptar.Enabled = false;
		btnaceptar.Text = "Guardando...";
		try
		{
			caja.Codsucursal = frmLogin.iCodSucursal;
			caja.Tipo = tipocaja;
			caja.Montoapertura = Convert.ToDecimal(txtmonto.Text);
			caja.Montocierre = 0m;
			caja.Fechaapertura = Convert.ToDateTime(DateTime.Now.ToString());
			caja.TotalIngreso = 0m;
			caja.TotalEgreso = 0m;
			caja.TotalVentaEfectivo = 0m;
			caja.TotalDisponible = Convert.ToDecimal(txtmonto.Text);
			caja.CodUser = frmLogin.iCodUser;
			caja.Codalmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
			var ok = await Task.Run(() => AdmApe.InsertAperturaCaja(caja));
			if (ok)
			{
				MessageBox.Show("Los datos se guardaron correctamente", "APERTURA DE CAJA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
		finally
		{
			if (!IsDisposed)
			{
				btnaceptar.Enabled = true;
				btnaceptar.Text = "Aceptar";
			}
		}
	}

	private void VerificaSaldoCaja()
	{
		clsCaja c = new clsCaja();
		c = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodUser);
		if (c == null)
		{
			c = AdmCaja.GetUltimaCajaVentas(frmLogin.iCodSucursal, 1, Convert.ToInt32(cmbAlmacenes.SelectedValue));
			if (c != null)
			{
				if (c.Estado)
				{
					Proceso = 1;
					btnaceptar.Enabled = true;
					txtmonto.Enabled = true;
				}
				else
				{
					Proceso = 1;
					tipocaja = 1;
					txtmonto.Text = "0.00";
					btnaceptar.Enabled = true;
					txtmonto.Enabled = true;
				}
				return;
			}
			c = AdmCaja.CargaCierreAnterior(frmLogin.iCodSucursal, 1);
			if (c == null)
			{
				Proceso = 1;
				tipocaja = 1;
				txtmonto.Text = "0.00";
				btnaceptar.Enabled = true;
				txtmonto.Enabled = true;
			}
			else if (c != null)
			{
				Proceso = 1;
				tipocaja = 1;
				txtmonto.Text = $"{c.Montocierre:#,##0.00}";
				btnaceptar.Enabled = true;
				txtmonto.Enabled = true;
			}
		}
		else
		{
			txtmonto.Text = "Caja Aperturada";
			txtmonto.Enabled = false;
			btnaceptar.Enabled = false;
		}
	}

	private void cmbAlmacenes_SelectedValueChanged(object sender, EventArgs e)
	{
		if (cmbAlmacenes.SelectedValue != null && cmbAlmacenes.SelectedValue.ToString() != "System.Data.DataRowView")
		{
			VerificaSaldoCaja();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmAperturaCajaDiaria));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnaceptar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnsalir = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtmonto = new System.Windows.Forms.TextBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.groupBox1.Controls.Add(this.btnaceptar);
		this.groupBox1.Controls.Add(this.btnsalir);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtmonto);
		this.groupBox1.Location = new System.Drawing.Point(12, 36);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(282, 100);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "  Apertura de Caja";
		this.btnaceptar.ImageIndex = 4;
		this.btnaceptar.ImageList = this.imageList1;
		this.btnaceptar.Location = new System.Drawing.Point(89, 59);
		this.btnaceptar.Name = "btnaceptar";
		this.btnaceptar.Size = new System.Drawing.Size(80, 32);
		this.btnaceptar.TabIndex = 3;
		this.btnaceptar.Text = "Aceptar";
		this.btnaceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnaceptar.UseVisualStyleBackColor = true;
		this.btnaceptar.Click += new System.EventHandler(btnaceptar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnsalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnsalir.ImageIndex = 5;
		this.btnsalir.ImageList = this.imageList1;
		this.btnsalir.Location = new System.Drawing.Point(188, 59);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(62, 32);
		this.btnsalir.TabIndex = 2;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnsalir.UseVisualStyleBackColor = true;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(26, 32);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(98, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Monto de Apertura:";
		this.txtmonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtmonto.Location = new System.Drawing.Point(130, 27);
		this.txtmonto.Name = "txtmonto";
		this.txtmonto.Size = new System.Drawing.Size(120, 23);
		this.txtmonto.TabIndex = 0;
		this.txtmonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
		this.toolTip1.AutomaticDelay = 200;
		this.toolTip1.AutoPopDelay = 2000;
		this.toolTip1.InitialDelay = 100;
		this.toolTip1.ReshowDelay = 40;
		this.cmbAlmacenes.Location = new System.Drawing.Point(82, 6);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(146, 24);
		this.cmbAlmacenes.TabIndex = 1;
		this.cmbAlmacenes.Text = "Seleccione Almacen...";
		this.cmbAlmacenes.ThemeName = "Fluent";
		this.cmbAlmacenes.SelectedValueChanged += new System.EventHandler(cmbAlmacenes_SelectedValueChanged);
		base.AcceptButton = this.btnaceptar;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.CancelButton = this.btnsalir;
		base.ClientSize = new System.Drawing.Size(308, 136);
		base.Controls.Add(this.cmbAlmacenes);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmAperturaCajaDiaria";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "APERTURA DE CAJA";
		base.Load += new System.EventHandler(AperturaCaja_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
