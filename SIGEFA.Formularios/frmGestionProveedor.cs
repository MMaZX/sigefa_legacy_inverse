using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionProveedor : Office2007Form
{
	public int Proceso = 0;

	private clsAdmProveedor admProv = new clsAdmProveedor();

	public clsProveedor prov = new clsProveedor();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsAdmListaPrecio AdmLista = new clsAdmListaPrecio();

	private clsListaPrecio lista = new clsListaPrecio();

	private bool margechange = false;

	private clsLocalidad local = new clsLocalidad();

	private IContainer components = null;

	private GroupBox groupBox1;

	private CheckBox cbActivo;

	private TextBox txtRepresentante;

	private Label label7;

	private TextBox txtFax;

	private Label label6;

	private TextBox txtTelefono;

	private Label label5;

	private TextBox txtDireccion;

	private Label label4;

	private TextBox txtRUC;

	private Label label3;

	private TextBox txtRazonSocial;

	private Label label2;

	private ImageList imageList1;

	private Button btnCancelar;

	private Button btnAceptar;

	private TextBox txtComentario;

	private Label label9;

	private TextBox txtVisita;

	private Label label8;

	private TextBox txtTelCon;

	private Label label1;

	private TextBox txtCtaCte;

	private Label label19;

	private TextBox txtBanco;

	private Label label16;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private TextBox txtContacto;

	private Label label10;

	private Label label33;

	private TextBox txtRecargo;

	private Label label11;

	private ComboBox cbDistrito;

	private ComboBox cbDepartamento;

	private ComboBox cbProvincia;

	private Label label12;

	private Label label13;

	private Label label14;

	private TextBox txtmail;

	private Label label15;

	public frmGestionProveedor()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (Proceso == 0 || !(txtRUC.Text != ""))
		{
			return;
		}
		prov.Ruc = txtRUC.Text;
		prov.RazonSocial = txtRazonSocial.Text;
		prov.Direccion = txtDireccion.Text;
		prov.Telefono = txtTelefono.Text;
		prov.Fax = txtFax.Text;
		prov.Representante = txtRepresentante.Text;
		prov.Mail = txtmail.Text;
		prov.Contacto = txtContacto.Text;
		prov.TelefonoContacto = txtTelCon.Text;
		if (txtVisita.Text != "")
		{
			prov.FrecuenciaVisita = Convert.ToInt32(txtVisita.Text);
		}
		if (txtRecargo.Text != "")
		{
			prov.Margen = Convert.ToDouble(txtRecargo.Text);
		}
		else
		{
			prov.Margen = 0.0;
		}
		prov.Banco = txtBanco.Text;
		prov.CtaCte = txtCtaCte.Text;
		prov.Comentario = txtComentario.Text;
		prov.CodUser = frmLogin.iCodUser;
		prov.Estado = cbActivo.Checked;
		if (Proceso == 1 || Proceso == 3)
		{
			if (admProv.insert(prov))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		else
		{
			if (Proceso != 2 || !admProv.update(prov))
			{
				return;
			}
			if (margechange)
			{
				DialogResult dlgResult = MessageBox.Show("Desea recalcular la listas de precios con el margen actual", "Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.No)
				{
					return;
				}
				foreach (int codlista in AdmLista.MuestraListasProveedor(frmLogin.iCodAlmacen))
				{
					lista = AdmLista.CargaListaPrecio(codlista);
					if (AdmLista.GeneraListaProveedor(lista.CodListaPrecio, frmLogin.iCodAlmacen, lista.Decimales, prov.CodProveedor))
					{
						MessageBox.Show("Se actualizo la lista " + lista.Nombre + " ", "Gestion Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmGestionProveedor_Load(object sender, EventArgs e)
	{
		CargaLocalidades(cbDepartamento, "000000", 1);
		if (Proceso == 2)
		{
			cargaproveedor();
		}
		else if (Proceso == 3)
		{
			cargaproveedor();
			ext.sololectura(groupBox1.Controls);
			btnAceptar.Visible = false;
			btnCancelar.Text = "Aceptar";
			btnCancelar.ImageIndex = 1;
		}
	}

	private void cbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLocalidades(cbProvincia, cbDepartamento.SelectedValue.ToString(), 2);
		cbProvincia.Enabled = true;
		cbProvincia.Focus();
	}

	private void cbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLocalidades(cbDistrito, cbProvincia.SelectedValue.ToString(), 3);
		cbDistrito.Enabled = true;
		cbDistrito.Focus();
	}

	private void CargaLocalidades(ComboBox Combo, string Padre, int Nivel)
	{
		Combo.DataSource = local.CargaLocalidades(Padre, Nivel);
		Combo.DisplayMember = "nombre";
		Combo.ValueMember = "codLocalidad";
		Combo.SelectedIndex = -1;
	}

	private void cargaproveedor()
	{
		prov = admProv.MuestraProveedor(prov.CodProveedor);
		txtRUC.Text = prov.Ruc;
		txtRazonSocial.Text = prov.RazonSocial;
		txtDireccion.Text = prov.Direccion;
		txtTelefono.Text = prov.Telefono;
		txtFax.Text = prov.Fax;
		txtRepresentante.Text = prov.Representante;
		txtmail.Text = prov.Mail;
		txtContacto.Text = prov.Contacto;
		txtTelCon.Text = prov.TelefonoContacto;
		txtVisita.Text = prov.FrecuenciaVisita.ToString();
		txtRecargo.Text = prov.Margen.ToString();
		txtBanco.Text = prov.Banco;
		txtCtaCte.Text = prov.CtaCte;
		txtComentario.Text = prov.Comentario;
		cbActivo.Checked = prov.Estado;
		cbDepartamento.SelectedValue = prov.Departamento;
		if (prov.Departamento != "")
		{
			cbDepartamento.SelectedValue = prov.Departamento;
			CargaLocalidades(cbProvincia, prov.Departamento.ToString(), 2);
			cbProvincia.Enabled = true;
			if (prov.Provincia != "")
			{
				cbProvincia.SelectedValue = prov.Provincia;
				CargaLocalidades(cbDistrito, prov.Provincia.ToString(), 3);
				cbDistrito.Enabled = true;
				cbDistrito.SelectedValue = prov.Distrito;
			}
		}
	}

	private void txtRUC_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F2)
		{
			if (ext.rucsunat(txtRUC.Text))
			{
				txtRazonSocial.Text = ext.RazonSocial;
				txtDireccion.Text = ext.DireccionLegal;
			}
			else
			{
				ext.limpiar(base.Controls);
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			txtRazonSocial.Focus();
		}
	}

	private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtRecargo_TextChanged(object sender, EventArgs e)
	{
		margechange = true;
	}

	private void frmGestionProveedor_Shown(object sender, EventArgs e)
	{
		margechange = false;
		txtRUC.Focus();
	}

	private void txtRazonSocial_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtDireccion.Focus();
		}
	}

	private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cbDepartamento.Focus();
		}
	}

	private void cbDistrito_SelectionChangeCommitted(object sender, EventArgs e)
	{
		txtTelefono.Focus();
	}

	private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtFax.Focus();
		}
	}

	private void txtFax_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtRepresentante.Focus();
		}
	}

	private void txtRepresentante_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtmail.Focus();
		}
	}

	private void txtmail_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtContacto.Focus();
		}
	}

	private void txtContacto_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtTelCon.Focus();
		}
	}

	private void txtTelCon_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtVisita.Focus();
		}
	}

	private void txtVisita_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtRecargo.Focus();
		}
	}

	private void txtRecargo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtBanco.Focus();
		}
	}

	private void txtBanco_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtCtaCte.Focus();
		}
	}

	private void txtCtaCte_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtComentario.Focus();
		}
	}

	private void txtComentario_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnAceptar.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionProveedor));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbDistrito = new System.Windows.Forms.ComboBox();
		this.cbDepartamento = new System.Windows.Forms.ComboBox();
		this.cbProvincia = new System.Windows.Forms.ComboBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.txtRecargo = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtContacto = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtCtaCte = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.txtBanco = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtVisita = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtTelCon = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cbActivo = new System.Windows.Forms.CheckBox();
		this.txtmail = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtRepresentante = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtFax = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtRUC = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtRazonSocial = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.cbDistrito);
		this.groupBox1.Controls.Add(this.cbDepartamento);
		this.groupBox1.Controls.Add(this.cbProvincia);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.label33);
		this.groupBox1.Controls.Add(this.txtRecargo);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.txtContacto);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtCtaCte);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.txtBanco);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtVisita);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtTelCon);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cbActivo);
		this.groupBox1.Controls.Add(this.txtmail);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.txtRepresentante);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtFax);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtTelefono);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtRUC);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtRazonSocial);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Location = new System.Drawing.Point(12, 9);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(483, 427);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle de la Empresa";
		this.cbDistrito.Enabled = false;
		this.cbDistrito.FormattingEnabled = true;
		this.cbDistrito.Location = new System.Drawing.Point(110, 129);
		this.cbDistrito.Name = "cbDistrito";
		this.cbDistrito.Size = new System.Drawing.Size(150, 21);
		this.cbDistrito.TabIndex = 6;
		this.cbDistrito.SelectionChangeCommitted += new System.EventHandler(cbDistrito_SelectionChangeCommitted);
		this.cbDepartamento.FormattingEnabled = true;
		this.cbDepartamento.Location = new System.Drawing.Point(110, 101);
		this.cbDepartamento.Name = "cbDepartamento";
		this.cbDepartamento.Size = new System.Drawing.Size(149, 21);
		this.cbDepartamento.TabIndex = 4;
		this.cbDepartamento.SelectionChangeCommitted += new System.EventHandler(cbDepartamento_SelectionChangeCommitted);
		this.cbProvincia.Enabled = false;
		this.cbProvincia.FormattingEnabled = true;
		this.cbProvincia.Location = new System.Drawing.Point(323, 101);
		this.cbProvincia.Name = "cbProvincia";
		this.cbProvincia.Size = new System.Drawing.Size(149, 21);
		this.cbProvincia.TabIndex = 5;
		this.cbProvincia.SelectionChangeCommitted += new System.EventHandler(cbProvincia_SelectionChangeCommitted);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(15, 132);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(45, 13);
		this.label12.TabIndex = 64;
		this.label12.Text = "Distrito :";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(15, 104);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(80, 13);
		this.label13.TabIndex = 63;
		this.label13.Text = "Departamento :";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(265, 104);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(57, 13);
		this.label14.TabIndex = 62;
		this.label14.Text = "Provincia :";
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(219, 290);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(15, 13);
		this.label33.TabIndex = 60;
		this.label33.Text = "%";
		this.txtRecargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRecargo.Location = new System.Drawing.Point(110, 287);
		this.txtRecargo.Name = "txtRecargo";
		this.txtRecargo.Size = new System.Drawing.Size(103, 20);
		this.txtRecargo.TabIndex = 14;
		this.txtRecargo.Tag = "1";
		this.txtRecargo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtRecargo.TextChanged += new System.EventHandler(txtRecargo_TextChanged);
		this.txtRecargo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRecargo_KeyDown);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(15, 290);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(81, 13);
		this.label11.TabIndex = 59;
		this.label11.Text = "Margen Ganc. :";
		this.txtContacto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtContacto.Location = new System.Drawing.Point(110, 235);
		this.txtContacto.Name = "txtContacto";
		this.txtContacto.Size = new System.Drawing.Size(362, 20);
		this.txtContacto.TabIndex = 11;
		this.txtContacto.KeyDown += new System.Windows.Forms.KeyEventHandler(txtContacto_KeyDown);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(15, 238);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(56, 13);
		this.label10.TabIndex = 58;
		this.label10.Text = "Contacto :";
		this.txtCtaCte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCtaCte.Location = new System.Drawing.Point(110, 339);
		this.txtCtaCte.Name = "txtCtaCte";
		this.txtCtaCte.Size = new System.Drawing.Size(362, 20);
		this.txtCtaCte.TabIndex = 16;
		this.txtCtaCte.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCtaCte_KeyDown);
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(15, 342);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(53, 13);
		this.label19.TabIndex = 55;
		this.label19.Text = "Cta. cte. :";
		this.txtBanco.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtBanco.Location = new System.Drawing.Point(110, 313);
		this.txtBanco.Name = "txtBanco";
		this.txtBanco.Size = new System.Drawing.Size(362, 20);
		this.txtBanco.TabIndex = 15;
		this.txtBanco.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBanco_KeyDown);
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(15, 316);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(44, 13);
		this.label16.TabIndex = 53;
		this.label16.Text = "Banco :";
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(110, 365);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(362, 57);
		this.txtComentario.TabIndex = 17;
		this.txtComentario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtComentario_KeyDown);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(15, 368);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(66, 13);
		this.label9.TabIndex = 18;
		this.label9.Text = "Comentario :";
		this.txtVisita.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtVisita.Location = new System.Drawing.Point(301, 261);
		this.txtVisita.Name = "txtVisita";
		this.txtVisita.Size = new System.Drawing.Size(118, 20);
		this.txtVisita.TabIndex = 13;
		this.txtVisita.KeyDown += new System.Windows.Forms.KeyEventHandler(txtVisita_KeyDown);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(233, 264);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 16;
		this.label8.Text = "Frec. Visita:";
		this.txtTelCon.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelCon.Location = new System.Drawing.Point(110, 261);
		this.txtTelCon.Name = "txtTelCon";
		this.txtTelCon.Size = new System.Drawing.Size(103, 20);
		this.txtTelCon.TabIndex = 12;
		this.txtTelCon.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTelCon_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 264);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 13);
		this.label1.TabIndex = 14;
		this.label1.Text = "Telef. Contac. :";
		this.cbActivo.AutoSize = true;
		this.cbActivo.Checked = true;
		this.cbActivo.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbActivo.Location = new System.Drawing.Point(298, 25);
		this.cbActivo.Name = "cbActivo";
		this.cbActivo.Size = new System.Drawing.Size(56, 17);
		this.cbActivo.TabIndex = 0;
		this.cbActivo.Text = "Activo";
		this.cbActivo.UseVisualStyleBackColor = true;
		this.txtmail.Location = new System.Drawing.Point(110, 209);
		this.txtmail.Name = "txtmail";
		this.txtmail.Size = new System.Drawing.Size(362, 20);
		this.txtmail.TabIndex = 10;
		this.txtmail.KeyDown += new System.Windows.Forms.KeyEventHandler(txtmail_KeyDown);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(15, 212);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(39, 13);
		this.label15.TabIndex = 12;
		this.label15.Text = "E-Mail:";
		this.txtRepresentante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRepresentante.Location = new System.Drawing.Point(110, 183);
		this.txtRepresentante.Name = "txtRepresentante";
		this.txtRepresentante.Size = new System.Drawing.Size(362, 20);
		this.txtRepresentante.TabIndex = 9;
		this.txtRepresentante.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRepresentante_KeyDown);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(15, 186);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(80, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Representante:";
		this.txtFax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFax.Location = new System.Drawing.Point(323, 157);
		this.txtFax.Name = "txtFax";
		this.txtFax.Size = new System.Drawing.Size(149, 20);
		this.txtFax.TabIndex = 8;
		this.txtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFax_KeyDown);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(295, 160);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(27, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Fax:";
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(110, 157);
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(150, 20);
		this.txtTelefono.TabIndex = 7;
		this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTelefono_KeyDown);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(15, 160);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(52, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Telefono:";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(110, 75);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(362, 20);
		this.txtDireccion.TabIndex = 3;
		this.txtDireccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDireccion_KeyDown);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(15, 78);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(55, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Dirección:";
		this.txtRUC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRUC.Location = new System.Drawing.Point(110, 23);
		this.txtRUC.Name = "txtRUC";
		this.txtRUC.Size = new System.Drawing.Size(103, 20);
		this.txtRUC.TabIndex = 1;
		this.txtRUC.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRUC_KeyDown);
		this.txtRUC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRUC_KeyPress);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(15, 26);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(45, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "R.U.C. :";
		this.txtRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRazonSocial.Location = new System.Drawing.Point(110, 49);
		this.txtRazonSocial.Name = "txtRazonSocial";
		this.txtRazonSocial.Size = new System.Drawing.Size(362, 20);
		this.txtRazonSocial.TabIndex = 2;
		this.txtRazonSocial.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRazonSocial_KeyDown);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 52);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(73, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Razon Social:";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(420, 442);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(336, 442);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 1;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(507, 477);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionProveedor";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Gestion Proovedor";
		base.Load += new System.EventHandler(frmGestionProveedor_Load);
		base.Shown += new System.EventHandler(frmGestionProveedor_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
