using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmCorreoElectronico : Form
{
	private clsAdmProveedor admproveedor = new clsAdmProveedor();

	private clsAdmUsuario admuser = new clsAdmUsuario();

	private clsUsuario user = new clsUsuario();

	private clsUsuario clsentuser = new clsUsuario();

	public int enviado = 0;

	public int codpro = 0;

	public int tipo = 0;

	private DataTable aux = new DataTable();

	private int Proceso = 0;

	private IContainer components = null;

	private Label label1;

	private GroupBox gbcabecera;

	private Button btnAceptar;

	private Button btnLimpiar;

	private TextBox txtasunto;

	private TextBox txtcc;

	private TextBox txtpara;

	private TextBox txtde;

	private Label label4;

	private Label label3;

	private Label label2;

	private Button btnSalir;

	private DataGridView dgvadjuntos;

	private GroupBox gbopciones;

	private GroupBox gbmensaje;

	private Label label5;

	private Button btnadjuntar;

	private ImageList imageList1;

	private Panel pdestinatarios;

	private Button btnCancelar;

	private CheckBox chbusuarios;

	private Button btnagregar;

	private DataGridView dgvcorreos;

	private DataGridViewCheckBoxColumn seleccion;

	private CheckBox chbproveedores;

	public LinkLabel link_adjunto;

	private LinkLabel link_adjunto1;

	public TextBox txtcuerpo;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	public frmCorreoElectronico()
	{
		InitializeComponent();
	}

	private void txtpara_Click(object sender, EventArgs e)
	{
		txtpara.Text = "";
		Proceso = 1;
		pdestinatarios.Enabled = true;
		pdestinatarios.Visible = true;
		gbcabecera.Enabled = false;
		gbmensaje.Enabled = false;
		dgvadjuntos.Enabled = false;
		gbopciones.Enabled = false;
		gbmensaje.SendToBack();
		pdestinatarios.BringToFront();
		if (tipo == 1)
		{
			chbproveedores.Visible = true;
		}
		else if (tipo == 2)
		{
			chbproveedores.Visible = false;
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		gbcabecera.Enabled = true;
		gbcabecera.Visible = true;
		gbmensaje.Enabled = true;
		gbmensaje.Visible = true;
		gbopciones.Enabled = true;
		gbopciones.Visible = true;
		pdestinatarios.Enabled = false;
		pdestinatarios.Visible = false;
	}

	private void GenerarAdjunto()
	{
	}

	private void btnadjuntar_Click(object sender, EventArgs e)
	{
		GenerarAdjunto();
		gbcabecera.Enabled = false;
		gbmensaje.Enabled = false;
		gbopciones.Enabled = false;
		dgvadjuntos.SendToBack();
		OpenFileDialog BuscarArchivo = new OpenFileDialog();
		BuscarArchivo.Filter = "Archivos |*.*";
		BuscarArchivo.FileName = "";
		BuscarArchivo.Title = "Titulo del Dialogo";
		if (tipo == 1)
		{
			BuscarArchivo.InitialDirectory = "C:\\Ordenes de Compra";
		}
		else if (tipo == 2)
		{
			BuscarArchivo.InitialDirectory = "C:\\Cotizaciones";
		}
		BuscarArchivo.Multiselect = true;
		if (BuscarArchivo.ShowDialog() == DialogResult.OK)
		{
			DataTable Dt = new DataTable();
			int idfile = 1;
			Dt.Columns.Add(new DataColumn("Id", typeof(int)));
			Dt.Columns.Add(new DataColumn("Archivo", typeof(string)));
			DataTable Dt2 = (DataTable)dgvadjuntos.DataSource;
			string[] fileNames = BuscarArchivo.FileNames;
			foreach (string Fi in fileNames)
			{
				Dt.Rows.Add(idfile, Fi.ToString());
			}
			string[] safeFileNames = BuscarArchivo.SafeFileNames;
			foreach (string Fi2 in safeFileNames)
			{
				LinkLabel linkLabel = link_adjunto1;
				linkLabel.Text = linkLabel.Text + Fi2 + "  ";
				link_adjunto1.LinkBehavior = LinkBehavior.NeverUnderline;
			}
			if (Dt2 == null)
			{
				dgvadjuntos.DataSource = Dt;
			}
			else if (Dt.Rows != null)
			{
				Dt2.Merge(Dt);
				dgvadjuntos.DataSource = Dt2;
			}
		}
		gbcabecera.Enabled = true;
		gbmensaje.Enabled = true;
		gbopciones.Enabled = true;
		Cursor = Cursors.Default;
	}

	private void btnagregar_Click(object sender, EventArgs e)
	{
		if (dgvcorreos.RowCount >= 1)
		{
			if (Proceso == 1)
			{
				Cursor = Cursors.WaitCursor;
				int iFila = 0;
				int iRow = 0;
				iRow = dgvcorreos.Rows.Count;
				for (iFila = 0; iFila < iRow; iFila++)
				{
					DataGridViewRow Row = dgvcorreos.Rows[iFila];
					if (Row.Cells[0].Value != null && Convert.ToBoolean(Row.Cells[0].Value.ToString()))
					{
						if (txtpara.Text.Trim() == "")
						{
							txtpara.Text = Row.Cells[2].Value.ToString().Trim();
						}
						else
						{
							txtpara.Text = txtpara.Text.Trim() + ", " + Row.Cells[2].Value.ToString().Trim();
						}
					}
				}
				Cursor = Cursors.Default;
			}
			else if (Proceso == 2)
			{
				Cursor = Cursors.WaitCursor;
				int iFila2 = 0;
				int iRow2 = 0;
				iRow2 = dgvcorreos.Rows.Count;
				for (iFila2 = 0; iFila2 < iRow2; iFila2++)
				{
					DataGridViewRow Row2 = dgvcorreos.Rows[iFila2];
					if (Row2.Cells[0].Value != null && Convert.ToBoolean(Row2.Cells[0].Value.ToString()))
					{
						if (txtcc.Text.Trim() == "")
						{
							txtcc.Text = Row2.Cells[2].Value.ToString().Trim();
						}
						else
						{
							txtcc.Text = txtcc.Text.Trim() + ", " + Row2.Cells[2].Value.ToString().Trim();
						}
					}
				}
				Cursor = Cursors.Default;
			}
		}
		pdestinatarios.Enabled = false;
		pdestinatarios.Visible = false;
		gbcabecera.Enabled = true;
		gbcabecera.Visible = true;
		gbmensaje.Enabled = true;
		gbmensaje.Visible = true;
		gbopciones.Enabled = true;
		gbopciones.Visible = true;
		Cursor = Cursors.Default;
		txtasunto.Focus();
	}

	private void btncancela_adjuntos_Click(object sender, EventArgs e)
	{
		gbcabecera.Enabled = true;
		gbcabecera.Visible = true;
		gbmensaje.Enabled = true;
		gbmensaje.Visible = true;
		gbopciones.Enabled = true;
		gbopciones.Visible = true;
	}

	private void frmCorreoElectronico_Load(object sender, EventArgs e)
	{
		clsentuser = admuser.MuestraUsuario(frmLogin.iCodUser);
		txtde.Text = clsentuser.Email;
		txtde.Enabled = false;
		pdestinatarios.Enabled = false;
		pdestinatarios.Visible = false;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		string sRutaPrincipal = "";
		string sArchivo = "";
		string sRutaFinal = "";
		string sUserCredential = clsentuser.Email;
		string sPassCredential = clsentuser.ContraEmail;
		MailMessage correo = new MailMessage();
		SmtpClient smtp = new SmtpClient();
		smtp.Host = clsentuser.Host;
		if (clsentuser.Host == "smtp.gmail.com" || clsentuser.Host == "smtp.live.com")
		{
			smtp.EnableSsl = true;
		}
		smtp.Credentials = new NetworkCredential(sUserCredential, sPassCredential);
		correo.From = new MailAddress(clsentuser.Email);
		if (txtpara.Text.Trim() != "")
		{
			correo.To.Add(txtpara.Text.Trim() + "," + admuser.MuestraUsuarioNivel().Email);
		}
		correo.Subject = txtasunto.Text;
		correo.Body = txtcuerpo.Text;
		correo.IsBodyHtml = false;
		correo.Priority = MailPriority.Normal;
		string arch_adj = "";
		if (tipo == 1)
		{
			arch_adj = "C:\\Ordenes de Compra\\" + link_adjunto.Text;
		}
		else if (tipo == 2)
		{
			arch_adj = "C:\\Cotizaciones\\" + link_adjunto.Text;
		}
		correo.Attachments.Add(new Attachment(arch_adj));
		if (dgvadjuntos.RowCount >= 1)
		{
			int iFila = 0;
			int iRow = 0;
			iRow = dgvadjuntos.Rows.Count;
			for (iFila = 0; iFila < iRow; iFila++)
			{
				DataGridViewRow Row = dgvadjuntos.Rows[iFila];
				sArchivo = Row.Cells[1].Value.ToString().Trim();
				sRutaFinal = sArchivo;
				correo.Attachments.Add(new Attachment(sRutaFinal));
			}
		}
		smtp.Send(correo);
		MessageBox.Show("Correo Enviado Satisfactoriamente a: " + txtpara.Text);
		enviado = 1;
		btnAceptar.Enabled = true;
	}

	private void txtcc_Click(object sender, EventArgs e)
	{
		txtcc.Text = "";
		Proceso = 2;
		pdestinatarios.Enabled = true;
		pdestinatarios.Visible = true;
		gbcabecera.Enabled = false;
		gbmensaje.Enabled = false;
		dgvadjuntos.Enabled = false;
		gbopciones.Enabled = false;
		gbmensaje.SendToBack();
		pdestinatarios.BringToFront();
		if (tipo == 1)
		{
			chbproveedores.Visible = true;
		}
		else if (tipo == 2)
		{
			chbproveedores.Visible = false;
		}
	}

	private void Verifica_CheckStateChanged(object sender, EventArgs e)
	{
		if (tipo == 1)
		{
			DataTable dt = new DataTable();
			aux.Clear();
			if (chbproveedores.CheckState == CheckState.Checked)
			{
				if (aux != null)
				{
					dt = admproveedor.ListaCorreosProveedores(codpro);
					aux.Merge(dt);
				}
				else
				{
					dt = admproveedor.ListaCorreosProveedores(codpro);
					aux = dt;
				}
			}
			if (chbusuarios.CheckState == CheckState.Checked)
			{
				if (aux != null)
				{
					dt = admuser.ListaCorreoUsuarios();
					aux.Merge(dt);
				}
				else
				{
					dt = admuser.ListaCorreoUsuarios();
					aux = dt;
				}
			}
			dgvcorreos.DataSource = aux;
		}
		else
		{
			if (tipo != 2)
			{
				return;
			}
			DataTable dt2 = new DataTable();
			aux.Clear();
			if (chbusuarios.CheckState == CheckState.Checked)
			{
				if (aux != null)
				{
					dt2 = admuser.ListaCorreoUsuarios();
					aux.Merge(dt2);
				}
				else
				{
					dt2 = admuser.ListaCorreoUsuarios();
					aux = dt2;
				}
			}
			dgvcorreos.DataSource = aux;
		}
	}

	private void chbproveedores_CheckStateChanged(object sender, EventArgs e)
	{
		Verifica_CheckStateChanged(sender, null);
	}

	private void chbusuarios_CheckStateChanged(object sender, EventArgs e)
	{
		Verifica_CheckStateChanged(sender, null);
	}

	private void link_adjunto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (tipo == 1)
		{
			string path = "C:\\Ordenes de Compra\\" + link_adjunto.Text;
			try
			{
				Process.Start(path);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al Mostrar el archivo", ex.Message);
				return;
			}
		}
		if (tipo == 2)
		{
			string path2 = "C:\\Cotizaciones\\" + link_adjunto.Text;
			try
			{
				Process.Start(path2);
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Error al Mostrar el archivo", ex2.Message);
			}
		}
	}

	private void btnLimpiar_Click(object sender, EventArgs e)
	{
		link_adjunto1.Text = "";
		DataTable dtx = new DataTable();
		dtx = (DataTable)dgvadjuntos.DataSource;
		dtx.Clear();
		dgvadjuntos.DataSource = dtx;
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Enabled)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Enabled)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCorreoElectronico));
		this.label1 = new System.Windows.Forms.Label();
		this.gbcabecera = new System.Windows.Forms.GroupBox();
		this.txtasunto = new System.Windows.Forms.TextBox();
		this.txtcc = new System.Windows.Forms.TextBox();
		this.txtpara = new System.Windows.Forms.TextBox();
		this.txtde = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnLimpiar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.dgvadjuntos = new System.Windows.Forms.DataGridView();
		this.gbopciones = new System.Windows.Forms.GroupBox();
		this.gbmensaje = new System.Windows.Forms.GroupBox();
		this.link_adjunto1 = new System.Windows.Forms.LinkLabel();
		this.link_adjunto = new System.Windows.Forms.LinkLabel();
		this.btnadjuntar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label5 = new System.Windows.Forms.Label();
		this.txtcuerpo = new System.Windows.Forms.TextBox();
		this.pdestinatarios = new System.Windows.Forms.Panel();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.chbusuarios = new System.Windows.Forms.CheckBox();
		this.btnagregar = new System.Windows.Forms.Button();
		this.dgvcorreos = new System.Windows.Forms.DataGridView();
		this.seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.chbproveedores = new System.Windows.Forms.CheckBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.gbcabecera.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvadjuntos).BeginInit();
		this.gbopciones.SuspendLayout();
		this.gbmensaje.SuspendLayout();
		this.pdestinatarios.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvcorreos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(27, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "De :";
		this.gbcabecera.Controls.Add(this.txtasunto);
		this.gbcabecera.Controls.Add(this.txtcc);
		this.gbcabecera.Controls.Add(this.txtpara);
		this.gbcabecera.Controls.Add(this.txtde);
		this.gbcabecera.Controls.Add(this.label4);
		this.gbcabecera.Controls.Add(this.label3);
		this.gbcabecera.Controls.Add(this.label2);
		this.gbcabecera.Controls.Add(this.label1);
		this.gbcabecera.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbcabecera.Location = new System.Drawing.Point(0, 0);
		this.gbcabecera.Name = "gbcabecera";
		this.gbcabecera.Size = new System.Drawing.Size(702, 98);
		this.gbcabecera.TabIndex = 1;
		this.gbcabecera.TabStop = false;
		this.gbcabecera.Text = "Datos";
		this.txtasunto.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtasunto.Location = new System.Drawing.Point(66, 69);
		this.txtasunto.Name = "txtasunto";
		this.txtasunto.Size = new System.Drawing.Size(559, 20);
		this.txtasunto.TabIndex = 3;
		this.txtcc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtcc.Location = new System.Drawing.Point(364, 43);
		this.txtcc.Name = "txtcc";
		this.txtcc.Size = new System.Drawing.Size(261, 20);
		this.txtcc.TabIndex = 2;
		this.txtcc.Click += new System.EventHandler(txtcc_Click);
		this.txtpara.Location = new System.Drawing.Point(66, 43);
		this.txtpara.Name = "txtpara";
		this.txtpara.Size = new System.Drawing.Size(261, 20);
		this.txtpara.TabIndex = 1;
		this.superValidator1.SetValidator1(this.txtpara, this.customValidator1);
		this.txtpara.Click += new System.EventHandler(txtpara_Click);
		this.txtde.Location = new System.Drawing.Point(66, 17);
		this.txtde.Name = "txtde";
		this.txtde.Size = new System.Drawing.Size(261, 20);
		this.txtde.TabIndex = 1;
		this.superValidator1.SetValidator1(this.txtde, this.customValidator2);
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(331, 46);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(27, 13);
		this.label4.TabIndex = 0;
		this.label4.Text = "CC :";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 72);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(46, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "Asunto :";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 46);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 13);
		this.label2.TabIndex = 0;
		this.label2.Text = "Para :";
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.Location = new System.Drawing.Point(463, 12);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 6;
		this.btnAceptar.Text = "Enviar";
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnLimpiar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnLimpiar.Location = new System.Drawing.Point(540, 12);
		this.btnLimpiar.Name = "btnLimpiar";
		this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
		this.btnLimpiar.TabIndex = 7;
		this.btnLimpiar.Text = "Limpiar";
		this.btnLimpiar.UseVisualStyleBackColor = true;
		this.btnLimpiar.Click += new System.EventHandler(btnLimpiar_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Location = new System.Drawing.Point(621, 12);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 23);
		this.btnSalir.TabIndex = 8;
		this.btnSalir.Text = "Salir";
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.dgvadjuntos.AllowUserToAddRows = false;
		this.dgvadjuntos.AllowUserToDeleteRows = false;
		this.dgvadjuntos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvadjuntos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
		this.dgvadjuntos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dgvadjuntos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvadjuntos.Location = new System.Drawing.Point(91, 226);
		this.dgvadjuntos.Name = "dgvadjuntos";
		this.dgvadjuntos.ReadOnly = true;
		this.dgvadjuntos.RowHeadersVisible = false;
		this.dgvadjuntos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvadjuntos.Size = new System.Drawing.Size(500, 0);
		this.dgvadjuntos.TabIndex = 4;
		this.dgvadjuntos.Visible = false;
		this.gbopciones.Controls.Add(this.btnSalir);
		this.gbopciones.Controls.Add(this.btnAceptar);
		this.gbopciones.Controls.Add(this.btnLimpiar);
		this.gbopciones.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.gbopciones.Location = new System.Drawing.Point(0, 374);
		this.gbopciones.Name = "gbopciones";
		this.gbopciones.Size = new System.Drawing.Size(702, 43);
		this.gbopciones.TabIndex = 5;
		this.gbopciones.TabStop = false;
		this.gbmensaje.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbmensaje.Controls.Add(this.link_adjunto1);
		this.gbmensaje.Controls.Add(this.link_adjunto);
		this.gbmensaje.Controls.Add(this.btnadjuntar);
		this.gbmensaje.Controls.Add(this.label5);
		this.gbmensaje.Controls.Add(this.txtcuerpo);
		this.gbmensaje.Controls.Add(this.dgvadjuntos);
		this.gbmensaje.Location = new System.Drawing.Point(0, 98);
		this.gbmensaje.Name = "gbmensaje";
		this.gbmensaje.Size = new System.Drawing.Size(702, 272);
		this.gbmensaje.TabIndex = 6;
		this.gbmensaje.TabStop = false;
		this.gbmensaje.Text = "Detalle";
		this.link_adjunto1.AutoSize = true;
		this.link_adjunto1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.link_adjunto1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
		this.link_adjunto1.Location = new System.Drawing.Point(64, 44);
		this.link_adjunto1.Name = "link_adjunto1";
		this.link_adjunto1.Size = new System.Drawing.Size(0, 12);
		this.link_adjunto1.TabIndex = 10;
		this.link_adjunto1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.link_adjunto.AutoSize = true;
		this.link_adjunto.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
		this.link_adjunto.Location = new System.Drawing.Point(63, 16);
		this.link_adjunto.Name = "link_adjunto";
		this.link_adjunto.Size = new System.Drawing.Size(55, 13);
		this.link_adjunto.TabIndex = 3;
		this.link_adjunto.TabStop = true;
		this.link_adjunto.Text = "linkLabel1";
		this.link_adjunto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(link_adjunto_LinkClicked);
		this.btnadjuntar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnadjuntar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnadjuntar.ImageIndex = 0;
		this.btnadjuntar.ImageList = this.imageList1;
		this.btnadjuntar.Location = new System.Drawing.Point(631, 33);
		this.btnadjuntar.Name = "btnadjuntar";
		this.btnadjuntar.Size = new System.Drawing.Size(65, 75);
		this.btnadjuntar.TabIndex = 5;
		this.btnadjuntar.Text = "Adjuntar Archivo";
		this.btnadjuntar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.btnadjuntar.UseVisualStyleBackColor = true;
		this.btnadjuntar.Click += new System.EventHandler(btnadjuntar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Empty;
		this.imageList1.Images.SetKeyName(0, "clip.png");
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 73);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 0;
		this.label5.Text = "Mensaje :";
		this.txtcuerpo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtcuerpo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtcuerpo.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcuerpo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.txtcuerpo.Location = new System.Drawing.Point(66, 73);
		this.txtcuerpo.Multiline = true;
		this.txtcuerpo.Name = "txtcuerpo";
		this.txtcuerpo.Size = new System.Drawing.Size(559, 186);
		this.txtcuerpo.TabIndex = 4;
		this.pdestinatarios.Controls.Add(this.btnCancelar);
		this.pdestinatarios.Controls.Add(this.chbusuarios);
		this.pdestinatarios.Controls.Add(this.btnagregar);
		this.pdestinatarios.Controls.Add(this.dgvcorreos);
		this.pdestinatarios.Controls.Add(this.chbproveedores);
		this.pdestinatarios.Location = new System.Drawing.Point(145, 42);
		this.pdestinatarios.Name = "pdestinatarios";
		this.pdestinatarios.Size = new System.Drawing.Size(436, 192);
		this.pdestinatarios.TabIndex = 7;
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.Location = new System.Drawing.Point(318, 166);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 3;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.chbusuarios.AutoSize = true;
		this.chbusuarios.Location = new System.Drawing.Point(105, 12);
		this.chbusuarios.Name = "chbusuarios";
		this.chbusuarios.Size = new System.Drawing.Size(67, 17);
		this.chbusuarios.TabIndex = 4;
		this.chbusuarios.Text = "Usuarios";
		this.chbusuarios.UseVisualStyleBackColor = true;
		this.chbusuarios.CheckStateChanged += new System.EventHandler(chbusuarios_CheckStateChanged);
		this.btnagregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnagregar.Location = new System.Drawing.Point(237, 166);
		this.btnagregar.Name = "btnagregar";
		this.btnagregar.Size = new System.Drawing.Size(75, 23);
		this.btnagregar.TabIndex = 3;
		this.btnagregar.Text = "Agregar";
		this.btnagregar.UseVisualStyleBackColor = true;
		this.btnagregar.Click += new System.EventHandler(btnagregar_Click);
		this.dgvcorreos.AllowUserToAddRows = false;
		this.dgvcorreos.AllowUserToDeleteRows = false;
		this.dgvcorreos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
		this.dgvcorreos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dgvcorreos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvcorreos.Columns.AddRange(this.seleccion);
		this.dgvcorreos.Location = new System.Drawing.Point(13, 35);
		this.dgvcorreos.Name = "dgvcorreos";
		this.dgvcorreos.RowHeadersVisible = false;
		this.dgvcorreos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvcorreos.Size = new System.Drawing.Size(417, 126);
		this.dgvcorreos.TabIndex = 0;
		this.seleccion.FillWeight = 50f;
		this.seleccion.HeaderText = "";
		this.seleccion.Name = "seleccion";
		this.seleccion.Width = 5;
		this.chbproveedores.AutoSize = true;
		this.chbproveedores.Location = new System.Drawing.Point(13, 12);
		this.chbproveedores.Name = "chbproveedores";
		this.chbproveedores.Size = new System.Drawing.Size(86, 17);
		this.chbproveedores.TabIndex = 4;
		this.chbproveedores.Text = "Proveedores";
		this.chbproveedores.UseVisualStyleBackColor = true;
		this.chbproveedores.CheckStateChanged += new System.EventHandler(chbproveedores_CheckStateChanged);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator1.ErrorMessage = "Ingrese el email de destino.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator2.ErrorMessage = "El usuario no tiene email configurado.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(702, 417);
		base.Controls.Add(this.pdestinatarios);
		base.Controls.Add(this.gbcabecera);
		base.Controls.Add(this.gbopciones);
		base.Controls.Add(this.gbmensaje);
		base.Name = "frmCorreoElectronico";
		this.Text = "Envío de Archivos a Correo Electrónicos";
		base.Load += new System.EventHandler(frmCorreoElectronico_Load);
		this.gbcabecera.ResumeLayout(false);
		this.gbcabecera.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvadjuntos).EndInit();
		this.gbopciones.ResumeLayout(false);
		this.gbmensaje.ResumeLayout(false);
		this.gbmensaje.PerformLayout();
		this.pdestinatarios.ResumeLayout(false);
		this.pdestinatarios.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvcorreos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
