using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmCuentasCte : Office2007Form
{
	private clsCtaCte cta = new clsCtaCte();

	private clsAdmCtaCte AdmCta = new clsAdmCtaCte();

	private clsMoneda mon = new clsMoneda();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsBanco ban = new clsBanco();

	private clsAdmBanco AdmBan = new clsAdmBanco();

	private clsValidar ok = new clsValidar();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private Label label3;

	private TextBox txtFiltro;

	private Label label2;

	private DataGridView dgvCtaCte;

	private DataGridViewTextBoxColumn codCtaCte;

	private DataGridViewTextBoxColumn codBanco;

	private DataGridViewTextBoxColumn banco;

	private DataGridViewTextBoxColumn ctacte;

	private DataGridViewTextBoxColumn tipoCuenta;

	private DataGridViewTextBoxColumn codMoneda;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn codUser;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn codAlmacen;

	private ErrorProvider errorProvider1;

	private SuperValidator superValidator1;

	private Highlighter highlighter1;

	private ImageList imageList1;

	private GroupBox groupBox2;

	private TextBox txtTipoCta;

	private TextBox txtCtaCte;

	private ComboBox cmbMoneda;

	private ComboBox cmbBanco;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label4;

	private GroupBox groupBox3;

	private Button btnGuardar;

	private Button btnReporte;

	private Button btnEliminar;

	private Button btnEditar;

	private Button btnNuevo;

	private Button btnSalir;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator4;

	private CustomValidator customValidator1;

	public frmCuentasCte()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvCtaCte.DataSource = data;
		data.DataSource = AdmCta.ListaCtaCte(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvCtaCte.ClearSelection();
	}

	private void CargaBancos()
	{
		cmbBanco.DataSource = AdmBan.MuestraBancos();
		cmbBanco.DisplayMember = "descripcion";
		cmbBanco.ValueMember = "codBanco";
		cmbBanco.SelectedIndex = -1;
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 0;
	}

	private void frmCuentasCte_Load(object sender, EventArgs e)
	{
		CargaBancos();
		CargaMoneda();
		CargaLista();
		label2.Text = "Banco";
		label3.Text = "banco";
		groupBox1.Height = 317;
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label3.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		cmbBanco.SelectedIndex = -1;
		cmbMoneda.SelectedIndex = -1;
		txtCtaCte.Text = "";
		txtTipoCta.Text = "";
		superValidator1.Validate();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Editar Registro";
		Proceso = 2;
		cmbBanco.SelectedValue = cta.CodBanco.ToString();
		txtCtaCte.Text = cta.CtaCte;
		txtTipoCta.Text = cta.TipoCuenta;
		cmbMoneda.SelectedValue = cta.Moneda;
	}

	private void frmCuentasCte_Shown(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (groupBox1.Visible)
		{
			Close();
			return;
		}
		Proceso = 0;
		CambiarEstados(Estado: true);
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(cmbBanco.SelectedValue.ToString() != "") || !(txtCtaCte.Text != "") || !(txtTipoCta.Text != "") || !(cmbMoneda.SelectedValue.ToString() != ""))
		{
			return;
		}
		cta.CodBanco = Convert.ToInt32(cmbBanco.SelectedValue);
		cta.CtaCte = txtCtaCte.Text;
		cta.TipoCuenta = txtTipoCta.Text;
		cta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		cta.Coduser = frmLogin.iCodUser;
		cta.CodAlmacen = frmLogin.iCodAlmacen;
		if (Proceso == 1)
		{
			if (AdmCta.Insert(cta))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion CtaCte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
			}
		}
		else if (Proceso == 2 && AdmCta.Update(cta))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Familia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
		}
		Proceso = 0;
	}

	private void dgvCtaCte_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvCtaCte.Rows.Count >= 1 && e.Row.Selected)
		{
			cta.CodCtaCte = Convert.ToInt32(e.Row.Cells[codCtaCte.Name].Value);
			cta.CodBanco = Convert.ToInt32(e.Row.Cells[codBanco.Name].Value);
			cta.CtaCte = e.Row.Cells[ctacte.Name].Value.ToString();
			cta.TipoCuenta = e.Row.Cells[tipoCuenta.Name].Value.ToString();
			cta.Moneda = Convert.ToInt32(e.Row.Cells[codMoneda.Name].Value);
			cta.Coduser = Convert.ToInt32(e.Row.Cells[codUser.Name].Value);
			cta.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecharegistro.Name].Value);
			cta.Estado = Convert.ToBoolean(e.Row.Cells[estado.Name].Value);
			cta.CodAlmacen = Convert.ToInt32(e.Row.Cells[codAlmacen.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvCtaCte.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void dgvCtaCte_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvCtaCte.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvCtaCte.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvCtaCte.CurrentRow.Index != -1 && cta.CodCtaCte != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "CtaCte", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmCta.Delete(cta.CodCtaCte, frmLogin.iCodAlmacen))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "CtaCte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("CuentasCte");
		foreach (DataGridViewColumn column in dgvCtaCte.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvCtaCte.Rows.Count; i++)
		{
			DataGridViewRow row = dgvCtaCte.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvCtaCte.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\CuentasCteRPT.xml", XmlWriteMode.WriteSchema);
		CRCtasCte rpt = new CRCtasCte();
		frmCuentasCorrienteRP frm = new frmCuentasCorrienteRP();
		rpt.SetDataSource(ds);
		frm.CRVCtasCte.ReportSource = rpt;
		frm.Show();
	}

	private void txtTipoCta_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.letras(e);
	}

	private void txtCtaCte_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosEnteros(e);
		if (e.KeyChar == '-')
		{
			e.Handled = false;
		}
		else if (char.IsDigit(e.KeyChar))
		{
			e.Handled = false;
		}
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
			{
				if (c.SelectedIndex != -1)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
			{
				if (c.SelectedIndex != -1)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCuentasCte));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvCtaCte = new System.Windows.Forms.DataGridView();
		this.codCtaCte = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codBanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ctacte = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtTipoCta = new System.Windows.Forms.TextBox();
		this.txtCtaCte = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.cmbBanco = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCtaCte).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvCtaCte);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(763, 98);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cuentas Corriente";
		this.dgvCtaCte.AllowUserToAddRows = false;
		this.dgvCtaCte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCtaCte.Columns.AddRange(this.codCtaCte, this.codBanco, this.banco, this.ctacte, this.tipoCuenta, this.codMoneda, this.moneda, this.fecharegistro, this.codUser, this.estado, this.codAlmacen);
		this.dgvCtaCte.Location = new System.Drawing.Point(10, 64);
		this.dgvCtaCte.Name = "dgvCtaCte";
		this.dgvCtaCte.ReadOnly = true;
		this.dgvCtaCte.RowHeadersVisible = false;
		this.dgvCtaCte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCtaCte.Size = new System.Drawing.Size(741, 234);
		this.dgvCtaCte.TabIndex = 4;
		this.dgvCtaCte.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvCtaCte_ColumnHeaderMouseClick);
		this.dgvCtaCte.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCtaCte_RowStateChanged);
		this.codCtaCte.DataPropertyName = "codCuentaCorriente";
		this.codCtaCte.HeaderText = "CodCtaCte";
		this.codCtaCte.Name = "codCtaCte";
		this.codCtaCte.ReadOnly = true;
		this.codCtaCte.Visible = false;
		this.codBanco.DataPropertyName = "codBanco";
		this.codBanco.HeaderText = "codBanco";
		this.codBanco.Name = "codBanco";
		this.codBanco.ReadOnly = true;
		this.codBanco.Visible = false;
		this.banco.DataPropertyName = "banco";
		this.banco.HeaderText = "Banco";
		this.banco.Name = "banco";
		this.banco.ReadOnly = true;
		this.banco.Width = 200;
		this.ctacte.DataPropertyName = "cuentaCorriente";
		this.ctacte.HeaderText = "Cuenta Corriente";
		this.ctacte.Name = "ctacte";
		this.ctacte.ReadOnly = true;
		this.ctacte.Width = 200;
		this.tipoCuenta.DataPropertyName = "tipoCuenta";
		this.tipoCuenta.HeaderText = "Tipo Cuenta";
		this.tipoCuenta.Name = "tipoCuenta";
		this.tipoCuenta.ReadOnly = true;
		this.tipoCuenta.Width = 200;
		this.codMoneda.DataPropertyName = "codMoneda";
		this.codMoneda.HeaderText = "codMoneda";
		this.codMoneda.Name = "codMoneda";
		this.codMoneda.ReadOnly = true;
		this.codMoneda.Visible = false;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Visible = false;
		this.moneda.Width = 150;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Registro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Width = 130;
		this.codUser.DataPropertyName = "codUser";
		this.codUser.HeaderText = "codUser";
		this.codUser.Name = "codUser";
		this.codUser.ReadOnly = true;
		this.codUser.Visible = false;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.codAlmacen.DataPropertyName = "codAlmacen";
		this.codAlmacen.HeaderText = "codAlmacen";
		this.codAlmacen.Name = "codAlmacen";
		this.codAlmacen.ReadOnly = true;
		this.codAlmacen.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(520, 30);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 3;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.txtFiltro.Location = new System.Drawing.Point(196, 27);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(318, 20);
		this.txtFiltro.TabIndex = 2;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(78, 28);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 1;
		this.label2.Text = "X";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(7, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Buscar Por: ";
		this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox2.Controls.Add(this.txtTipoCta);
		this.groupBox2.Controls.Add(this.txtCtaCte);
		this.groupBox2.Controls.Add(this.cmbMoneda);
		this.groupBox2.Controls.Add(this.cmbBanco);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Location = new System.Drawing.Point(264, 117);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(342, 153);
		this.groupBox2.TabIndex = 5;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.txtTipoCta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoCta.Location = new System.Drawing.Point(86, 96);
		this.txtTipoCta.Name = "txtTipoCta";
		this.txtTipoCta.Size = new System.Drawing.Size(231, 20);
		this.txtTipoCta.TabIndex = 7;
		this.superValidator1.SetValidator1(this.txtTipoCta, this.customValidator3);
		this.txtTipoCta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTipoCta_KeyPress);
		this.txtCtaCte.Location = new System.Drawing.Point(86, 58);
		this.txtCtaCte.Name = "txtCtaCte";
		this.txtCtaCte.Size = new System.Drawing.Size(231, 20);
		this.txtCtaCte.TabIndex = 6;
		this.superValidator1.SetValidator1(this.txtCtaCte, this.customValidator2);
		this.txtCtaCte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCtaCte_KeyPress);
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(86, 125);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(231, 21);
		this.cmbMoneda.TabIndex = 5;
		this.superValidator1.SetValidator1(this.cmbMoneda, this.customValidator4);
		this.cmbBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbBanco.FormattingEnabled = true;
		this.cmbBanco.Location = new System.Drawing.Point(86, 27);
		this.cmbBanco.Name = "cmbBanco";
		this.cmbBanco.Size = new System.Drawing.Size(231, 21);
		this.cmbBanco.TabIndex = 4;
		this.superValidator1.SetValidator1(this.cmbBanco, this.customValidator1);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(7, 128);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(49, 13);
		this.label7.TabIndex = 3;
		this.label7.Text = "Moneda:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(7, 99);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(68, 13);
		this.label6.TabIndex = 2;
		this.label6.Text = "Tipo Cuenta:";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(7, 61);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(63, 13);
		this.label5.TabIndex = 1;
		this.label5.Text = "Cuenta Cte:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(7, 30);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(41, 13);
		this.label4.TabIndex = 0;
		this.label4.Text = "Banco:";
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator3.ErrorMessage = "Ingrese Tipo Cta";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese Cta Cte";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator4.ErrorMessage = "Seleccione Moneda";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator1.ErrorMessage = "Seleccione un Banco";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Location = new System.Drawing.Point(13, 336);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(761, 48);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(538, 14);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 5;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(455, 14);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 4;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(370, 14);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 3;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(289, 14);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 2;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(217, 14);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 1;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(140, 14);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 0;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(786, 396);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmCuentasCte";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmCuentasCte";
		base.Load += new System.EventHandler(frmCuentasCte_Load);
		base.Shown += new System.EventHandler(frmCuentasCte_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCtaCte).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
