using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using DevComponents.Editors.DateTimeAdv;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmPaciente : Office2007Form
{
	private clsAdmClinica AdmClinica;

	private clsPaciente Paciente;

	private new Form ParentForm;

	private IContainer components = null;

	private PanelEx panelCabecera;

	private LabelX lblTituloCab;

	private PanelEx panelDetalle;

	private Line line1;

	private LabelX lblEspecie;

	private ComboBoxEx cmbEspecie;

	private ButtonX btnNuevo;

	private ButtonX btnCancelar;

	private ButtonX btnEliminar;

	private ButtonX btnGuardar;

	private ButtonX btnModificar;

	private Line line2;

	private LabelX lblTituloDet;

	private TextBoxX txtNombre;

	private LabelX lblNombre;

	private DataGridViewX dgvDetalle;

	private DateTimeInput dtiFechaNacimiento;

	private LabelX lblFechaNac;

	private TextBoxX txtRaza;

	private LabelX lblRaza;

	private TextBoxX txtEdad;

	private LabelX lblPropietario;

	private ComboBoxEx cmbSexo;

	private LabelX lblSexo;

	private TextBoxX txtPropietario;

	private TextBoxX txtDireccion;

	private LabelX labelX1;

	public frmPaciente()
	{
		InitializeComponent();
		PostConstructor();
	}

	public frmPaciente(Form _parentForm)
	{
		InitializeComponent();
		PostConstructor();
		ParentForm = _parentForm;
	}

	private void PostConstructor()
	{
		txtNombre.Enabled = false;
		txtNombre.Text = string.Empty;
		txtPropietario.Enabled = false;
		txtPropietario.Text = string.Empty;
		txtEdad.Text = string.Empty;
		txtRaza.Enabled = false;
		txtRaza.Text = string.Empty;
		txtDireccion.Enabled = false;
		txtDireccion.Text = string.Empty;
		cmbEspecie.Enabled = false;
		cmbEspecie.Items.Clear();
		cmbSexo.Enabled = false;
		cmbSexo.Items.Clear();
		dtiFechaNacimiento.Enabled = false;
		dtiFechaNacimiento.Value = DateTimePicker.MinimumDateTime;
		AdmClinica = new clsAdmClinica();
		Paciente = null;
		dtiFechaNacimiento.MaxDate = DateTime.Now;
		btnNuevo.Enabled = true;
		btnModificar.Enabled = false;
		btnGuardar.Enabled = false;
		btnEliminar.Enabled = false;
		btnCancelar.Enabled = false;
		CargarData();
		if (dgvDetalle.RowCount > 0)
		{
			btnModificar.Enabled = true;
		}
	}

	private void CargarData()
	{
		try
		{
			PrepareData(AdmClinica.ListaPacientes());
		}
		catch (Exception ex)
		{
			MessageBox.Show("ERROR: " + ex.Message, "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrepareData(DataTable DataBD)
	{
		DataTable table = new DataTable();
		table.Columns.Add("id", typeof(string));
		table.Columns.Add("NOMBRE", typeof(string));
		table.Columns.Add("FECHA NACIMIENTO", typeof(string));
		table.Columns.Add("PROPIETARIO", typeof(string));
		table.Columns.Add("ESPECIE", typeof(string));
		table.Columns.Add("RAZA", typeof(string));
		table.Columns.Add("SEXO", typeof(string));
		table.Columns.Add("DIRECCION", typeof(string));
		table.Columns.Add("USUARIO", typeof(string));
		table.Columns.Add("FECHA REGISTRO", typeof(string));
		table.Columns.Add("ESTADO", typeof(string));
		foreach (DataRow row in DataBD.Rows)
		{
			table.Rows.Add(row[0].ToString(), row[1].ToString(), Convert.ToDateTime(row[2].ToString()).ToShortDateString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString().Equals("1") ? "ACTIVO" : (row[10].ToString().Equals("0") ? "INACTIVO" : "FALLECIDO"));
		}
		dgvDetalle.DataSource = table;
		dgvDetalle.Columns[0].Visible = false;
		dgvDetalle.Columns[8].Visible = false;
		dgvDetalle.Columns[9].Visible = false;
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		txtNombre.Enabled = true;
		txtNombre.Text = string.Empty;
		txtPropietario.Enabled = true;
		txtPropietario.Text = string.Empty;
		txtEdad.Text = string.Empty;
		txtRaza.Enabled = true;
		txtRaza.Text = string.Empty;
		txtDireccion.Enabled = true;
		txtDireccion.Text = string.Empty;
		cmbEspecie.Enabled = true;
		cmbSexo.Enabled = true;
		dtiFechaNacimiento.Enabled = true;
		CargarEspecies();
		CargarSexo();
		btnNuevo.Enabled = false;
		btnModificar.Enabled = false;
		btnGuardar.Enabled = true;
		btnEliminar.Enabled = false;
		btnCancelar.Enabled = true;
	}

	private void CargarEspecies()
	{
		cmbEspecie.Items.Add("CANINO");
		cmbEspecie.Items.Add("FELINO");
		cmbEspecie.Items.Add("AVE");
		cmbEspecie.Items.Add("REPTIL");
	}

	private void CargarSexo()
	{
		cmbSexo.Items.Add("MACHO");
		cmbSexo.Items.Add("HEMBRA");
		cmbSexo.Items.Add("NO DEIFINIDO");
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		PostConstructor();
	}

	private string CalcularDiferenciaEntreFechas(DateTime inicio, DateTime fin)
	{
		DateTime date1 = inicio;
		DateTime date2 = fin;
		int oldMonth = date2.Month;
		while (oldMonth == date2.Month)
		{
			date1 = date1.AddDays(-1.0);
			date2 = date2.AddDays(-1.0);
		}
		int years = 0;
		int months = 0;
		int days = 0;
		while (date2.CompareTo(date1) >= 0)
		{
			years++;
			date2 = date2.AddYears(-1);
		}
		date2 = date2.AddYears(1);
		years--;
		oldMonth = date2.Month;
		while (date2.CompareTo(date1) >= 0)
		{
			days++;
			date2 = date2.AddDays(-1.0);
			if (date2.CompareTo(date1) >= 0 && oldMonth != date2.Month)
			{
				months++;
				days = 0;
				oldMonth = date2.Month;
			}
		}
		date2 = date2.AddDays(1.0);
		days--;
		TimeSpan difference = date2.Subtract(date1);
		return years + " años, " + months + " meses, " + days + " días";
	}

	private void dtiFechaNacimiento_ValueChanged(object sender, EventArgs e)
	{
		txtEdad.Text = CalcularDiferenciaEntreFechas(dtiFechaNacimiento.Value, DateTime.Now);
		if (dtiFechaNacimiento.Value == DateTimePicker.MinimumDateTime)
		{
			dtiFechaNacimiento.Value = DateTime.Now;
			dtiFechaNacimiento.Format = eDateTimePickerFormat.Custom;
			dtiFechaNacimiento.CustomFormat = " ";
		}
		else
		{
			dtiFechaNacimiento.Format = eDateTimePickerFormat.Short;
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!ValidarRegistro())
		{
			return;
		}
		if (Paciente == null)
		{
			try
			{
				Paciente = new clsPaciente();
				Paciente.Nombre = txtNombre.Text;
				Paciente.FechaNacimiento = dtiFechaNacimiento.Value;
				Paciente.Raza = txtRaza.Text;
				Paciente.Especie = cmbEspecie.SelectedItem.ToString();
				Paciente.Propietario = txtPropietario.Text;
				Paciente.Direccion = txtDireccion.Text;
				Paciente.Sexo = cmbSexo.SelectedItem.ToString();
				Paciente.Estado = 1;
				Paciente.FechaRegistro = DateTime.Now;
				Paciente.UsuarioID = frmLogin.iCodUser;
				if (AdmClinica.InsertPaciente(Paciente))
				{
					MessageBox.Show("PACIENTE REGISTRADO CORRECTAMENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					PostConstructor();
				}
				else
				{
					MessageBox.Show("ERROR AL REGISTRAR EL PACIENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR: " + ex.Message, "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		try
		{
			Paciente.Nombre = txtNombre.Text;
			Paciente.FechaNacimiento = dtiFechaNacimiento.Value;
			Paciente.Raza = txtRaza.Text;
			Paciente.Especie = cmbEspecie.SelectedItem.ToString();
			Paciente.Propietario = txtPropietario.Text;
			Paciente.Direccion = txtDireccion.Text;
			Paciente.Sexo = cmbSexo.SelectedItem.ToString();
			Paciente.Estado = 1;
			Paciente.FechaRegistro = DateTime.Now;
			Paciente.UsuarioID = frmLogin.iCodUser;
			if (AdmClinica.UpdatePaciente(Paciente))
			{
				MessageBox.Show("PACIENTE ACTUALIZADO CORRECTAMENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				PostConstructor();
			}
			else
			{
				MessageBox.Show("ERROR AL ACTUALIZAR EL PACIENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show("ERROR: " + ex2.Message, "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool ValidarRegistro()
	{
		if (string.IsNullOrEmpty(txtNombre.Text))
		{
			MessageBox.Show("INGRESE EL NOMBRE DEL PACIENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNombre.Focus();
			return false;
		}
		if (string.IsNullOrEmpty(txtPropietario.Text))
		{
			MessageBox.Show("INGRESE EL NOMBRE DEL PROPIETARIO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPropietario.Focus();
			return false;
		}
		if (string.IsNullOrEmpty(txtDireccion.Text))
		{
			MessageBox.Show("INGRESE LA DIRECCION", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtDireccion.Focus();
			return false;
		}
		if (string.IsNullOrEmpty(txtRaza.Text))
		{
			MessageBox.Show("INGRESE LA RAZA", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtRaza.Focus();
			return false;
		}
		if (cmbSexo.SelectedIndex == -1)
		{
			MessageBox.Show("SELECCIONE EL SEXO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			cmbSexo.Focus();
			return false;
		}
		if (cmbEspecie.SelectedIndex == -1)
		{
			MessageBox.Show("SELECCIONE LA ESPECIE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			cmbEspecie.Focus();
			return false;
		}
		_ = dtiFechaNacimiento.Value;
		if (false)
		{
			MessageBox.Show("INGRESE LA FECHA DE NACIMIENTO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			dtiFechaNacimiento.Focus();
			return false;
		}
		return true;
	}

	private void btnModificar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0 && dgvDetalle.CurrentRow != null)
		{
			int IDPaciente = Convert.ToInt32(dgvDetalle.CurrentRow.Cells[0].Value.ToString());
			Paciente = AdmClinica.CargaPaciente(IDPaciente);
			if (Paciente.Estado != -1)
			{
				CargarEspecies();
				CargarSexo();
				txtNombre.Text = Paciente.Nombre;
				txtPropietario.Text = Paciente.Propietario;
				txtRaza.Text = Paciente.Raza;
				dtiFechaNacimiento.Value = Paciente.FechaNacimiento;
				txtDireccion.Text = Paciente.Direccion;
				cmbEspecie.SelectedItem = Paciente.Especie;
				cmbSexo.SelectedItem = Paciente.Sexo;
				txtNombre.Enabled = true;
				txtPropietario.Enabled = true;
				txtRaza.Enabled = true;
				txtDireccion.Enabled = true;
				cmbEspecie.Enabled = true;
				cmbSexo.Enabled = true;
				dtiFechaNacimiento.Enabled = true;
				btnNuevo.Enabled = false;
				btnGuardar.Enabled = true;
				btnEliminar.Enabled = true;
				btnCancelar.Enabled = true;
			}
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count <= 0 || dgvDetalle.CurrentRow == null)
		{
			return;
		}
		int IDPaciente = Convert.ToInt32(dgvDetalle.CurrentRow.Cells[0].Value.ToString());
		string NombreMascota = dgvDetalle.CurrentRow.Cells[1].Value.ToString();
		DialogResult dResult = MessageBox.Show("¿ESTÁ SEGURO DE ELIMINAR A " + NombreMascota + "?", "SGE SYSTEM'S", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dResult == DialogResult.Yes)
		{
			if (AdmClinica.DeletePaciente(IDPaciente))
			{
				MessageBox.Show("PACIENTE ELIMINADO CORRECTAMENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				PostConstructor();
			}
			else
			{
				MessageBox.Show("ERROR AL ELIMINAR EL PACIENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
	}

	private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.RowCount <= 0 || ParentForm == null || dgvDetalle.CurrentRow == null)
		{
			return;
		}
		if (dgvDetalle.CurrentRow.Cells[10].Value.ToString().Equals("ACTIVO"))
		{
			if (ParentForm.Name.Equals("frmHistoriaClinica"))
			{
				frmHistoriaClinica frm = (frmHistoriaClinica)ParentForm;
				frm.ObtenerDesdeBuscador(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[0].Value.ToString()));
				Close();
			}
		}
		else
		{
			MessageBox.Show("NO PUEDES SELECCIONAR UN PACIENTE QUE SE ENCUENTRA INACTIVO / FALLECIDO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPaciente));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.panelCabecera = new DevComponents.DotNetBar.PanelEx();
		this.txtDireccion = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.txtPropietario = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblPropietario = new DevComponents.DotNetBar.LabelX();
		this.cmbSexo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
		this.lblSexo = new DevComponents.DotNetBar.LabelX();
		this.txtEdad = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.dtiFechaNacimiento = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
		this.lblFechaNac = new DevComponents.DotNetBar.LabelX();
		this.txtRaza = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblRaza = new DevComponents.DotNetBar.LabelX();
		this.txtNombre = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblNombre = new DevComponents.DotNetBar.LabelX();
		this.btnCancelar = new DevComponents.DotNetBar.ButtonX();
		this.btnEliminar = new DevComponents.DotNetBar.ButtonX();
		this.btnGuardar = new DevComponents.DotNetBar.ButtonX();
		this.btnModificar = new DevComponents.DotNetBar.ButtonX();
		this.btnNuevo = new DevComponents.DotNetBar.ButtonX();
		this.cmbEspecie = new DevComponents.DotNetBar.Controls.ComboBoxEx();
		this.line1 = new DevComponents.DotNetBar.Controls.Line();
		this.lblEspecie = new DevComponents.DotNetBar.LabelX();
		this.lblTituloCab = new DevComponents.DotNetBar.LabelX();
		this.panelDetalle = new DevComponents.DotNetBar.PanelEx();
		this.dgvDetalle = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.line2 = new DevComponents.DotNetBar.Controls.Line();
		this.lblTituloDet = new DevComponents.DotNetBar.LabelX();
		this.panelCabecera.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtiFechaNacimiento).BeginInit();
		this.panelDetalle.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		base.SuspendLayout();
		this.panelCabecera.CanvasColor = System.Drawing.SystemColors.Control;
		this.panelCabecera.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.panelCabecera.Controls.Add(this.txtDireccion);
		this.panelCabecera.Controls.Add(this.labelX1);
		this.panelCabecera.Controls.Add(this.txtPropietario);
		this.panelCabecera.Controls.Add(this.lblPropietario);
		this.panelCabecera.Controls.Add(this.cmbSexo);
		this.panelCabecera.Controls.Add(this.lblSexo);
		this.panelCabecera.Controls.Add(this.txtEdad);
		this.panelCabecera.Controls.Add(this.dtiFechaNacimiento);
		this.panelCabecera.Controls.Add(this.lblFechaNac);
		this.panelCabecera.Controls.Add(this.txtRaza);
		this.panelCabecera.Controls.Add(this.lblRaza);
		this.panelCabecera.Controls.Add(this.txtNombre);
		this.panelCabecera.Controls.Add(this.lblNombre);
		this.panelCabecera.Controls.Add(this.btnCancelar);
		this.panelCabecera.Controls.Add(this.btnEliminar);
		this.panelCabecera.Controls.Add(this.btnGuardar);
		this.panelCabecera.Controls.Add(this.btnModificar);
		this.panelCabecera.Controls.Add(this.btnNuevo);
		this.panelCabecera.Controls.Add(this.cmbEspecie);
		this.panelCabecera.Controls.Add(this.line1);
		this.panelCabecera.Controls.Add(this.lblEspecie);
		this.panelCabecera.Controls.Add(this.lblTituloCab);
		this.panelCabecera.DisabledBackColor = System.Drawing.Color.Empty;
		this.panelCabecera.Location = new System.Drawing.Point(12, 12);
		this.panelCabecera.Name = "panelCabecera";
		this.panelCabecera.Size = new System.Drawing.Size(927, 178);
		this.panelCabecera.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.panelCabecera.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.panelCabecera.Style.Border = DevComponents.DotNetBar.eBorderType.DoubleLine;
		this.panelCabecera.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.panelCabecera.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.panelCabecera.Style.GradientAngle = 90;
		this.panelCabecera.TabIndex = 0;
		this.txtDireccion.BackColor = System.Drawing.Color.White;
		this.txtDireccion.Border.Class = "TextBoxBorder";
		this.txtDireccion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtDireccion.ButtonCustom.Tooltip = "";
		this.txtDireccion.ButtonCustom2.Tooltip = "";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.DisabledBackColor = System.Drawing.Color.White;
		this.txtDireccion.ForeColor = System.Drawing.Color.Black;
		this.txtDireccion.Location = new System.Drawing.Point(652, 90);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.PreventEnterBeep = true;
		this.txtDireccion.Size = new System.Drawing.Size(272, 20);
		this.txtDireccion.TabIndex = 21;
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Location = new System.Drawing.Point(586, 87);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(73, 23);
		this.labelX1.TabIndex = 20;
		this.labelX1.Text = "Dirección:";
		this.txtPropietario.BackColor = System.Drawing.Color.White;
		this.txtPropietario.Border.Class = "TextBoxBorder";
		this.txtPropietario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtPropietario.ButtonCustom.Tooltip = "";
		this.txtPropietario.ButtonCustom2.Tooltip = "";
		this.txtPropietario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtPropietario.DisabledBackColor = System.Drawing.Color.White;
		this.txtPropietario.ForeColor = System.Drawing.Color.Black;
		this.txtPropietario.Location = new System.Drawing.Point(652, 52);
		this.txtPropietario.Name = "txtPropietario";
		this.txtPropietario.PreventEnterBeep = true;
		this.txtPropietario.Size = new System.Drawing.Size(272, 20);
		this.txtPropietario.TabIndex = 19;
		this.lblPropietario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblPropietario.Location = new System.Drawing.Point(586, 49);
		this.lblPropietario.Name = "lblPropietario";
		this.lblPropietario.Size = new System.Drawing.Size(60, 23);
		this.lblPropietario.TabIndex = 18;
		this.lblPropietario.Text = "Propietario:";
		this.cmbSexo.DisplayMember = "Text";
		this.cmbSexo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.cmbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSexo.FormattingEnabled = true;
		this.cmbSexo.ItemHeight = 14;
		this.cmbSexo.Location = new System.Drawing.Point(486, 90);
		this.cmbSexo.Name = "cmbSexo";
		this.cmbSexo.Size = new System.Drawing.Size(94, 20);
		this.cmbSexo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.cmbSexo.TabIndex = 17;
		this.lblSexo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblSexo.Location = new System.Drawing.Point(453, 90);
		this.lblSexo.Name = "lblSexo";
		this.lblSexo.Size = new System.Drawing.Size(36, 23);
		this.lblSexo.TabIndex = 16;
		this.lblSexo.Text = "Sexo:";
		this.txtEdad.BackColor = System.Drawing.Color.White;
		this.txtEdad.Border.Class = "TextBoxBorder";
		this.txtEdad.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtEdad.ButtonCustom.Tooltip = "";
		this.txtEdad.ButtonCustom2.Tooltip = "";
		this.txtEdad.DisabledBackColor = System.Drawing.Color.White;
		this.txtEdad.Enabled = false;
		this.txtEdad.ForeColor = System.Drawing.Color.Black;
		this.txtEdad.Location = new System.Drawing.Point(453, 52);
		this.txtEdad.Name = "txtEdad";
		this.txtEdad.PreventEnterBeep = true;
		this.txtEdad.Size = new System.Drawing.Size(127, 20);
		this.txtEdad.TabIndex = 15;
		this.txtEdad.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
		this.txtEdad.WatermarkText = "EDAD";
		this.dtiFechaNacimiento.BackgroundStyle.Class = "DateTimeInputBackground";
		this.dtiFechaNacimiento.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtiFechaNacimiento.ButtonClear.Tooltip = "";
		this.dtiFechaNacimiento.ButtonCustom.Tooltip = "";
		this.dtiFechaNacimiento.ButtonCustom2.Tooltip = "";
		this.dtiFechaNacimiento.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
		this.dtiFechaNacimiento.ButtonDropDown.Tooltip = "";
		this.dtiFechaNacimiento.ButtonDropDown.Visible = true;
		this.dtiFechaNacimiento.ButtonFreeText.Tooltip = "";
		this.dtiFechaNacimiento.IsPopupCalendarOpen = false;
		this.dtiFechaNacimiento.Location = new System.Drawing.Point(332, 52);
		this.dtiFechaNacimiento.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
		this.dtiFechaNacimiento.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtiFechaNacimiento.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
		this.dtiFechaNacimiento.MonthCalendar.ClearButtonVisible = true;
		this.dtiFechaNacimiento.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
		this.dtiFechaNacimiento.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
		this.dtiFechaNacimiento.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
		this.dtiFechaNacimiento.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.dtiFechaNacimiento.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
		this.dtiFechaNacimiento.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
		this.dtiFechaNacimiento.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtiFechaNacimiento.MonthCalendar.DisplayMonth = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
		this.dtiFechaNacimiento.MonthCalendar.MarkedDates = new System.DateTime[0];
		this.dtiFechaNacimiento.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
		this.dtiFechaNacimiento.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.dtiFechaNacimiento.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
		this.dtiFechaNacimiento.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.dtiFechaNacimiento.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtiFechaNacimiento.MonthCalendar.TodayButtonVisible = true;
		this.dtiFechaNacimiento.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
		this.dtiFechaNacimiento.Name = "dtiFechaNacimiento";
		this.dtiFechaNacimiento.Size = new System.Drawing.Size(115, 20);
		this.dtiFechaNacimiento.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.dtiFechaNacimiento.TabIndex = 14;
		this.dtiFechaNacimiento.ValueChanged += new System.EventHandler(dtiFechaNacimiento_ValueChanged);
		this.lblFechaNac.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblFechaNac.Location = new System.Drawing.Point(225, 49);
		this.lblFechaNac.Name = "lblFechaNac";
		this.lblFechaNac.Size = new System.Drawing.Size(101, 23);
		this.lblFechaNac.TabIndex = 13;
		this.lblFechaNac.Text = "Fecha Nacimiento:";
		this.txtRaza.BackColor = System.Drawing.Color.White;
		this.txtRaza.Border.Class = "TextBoxBorder";
		this.txtRaza.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtRaza.ButtonCustom.Tooltip = "";
		this.txtRaza.ButtonCustom2.Tooltip = "";
		this.txtRaza.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRaza.DisabledBackColor = System.Drawing.Color.White;
		this.txtRaza.ForeColor = System.Drawing.Color.Black;
		this.txtRaza.Location = new System.Drawing.Point(259, 90);
		this.txtRaza.Name = "txtRaza";
		this.txtRaza.PreventEnterBeep = true;
		this.txtRaza.Size = new System.Drawing.Size(188, 20);
		this.txtRaza.TabIndex = 12;
		this.lblRaza.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblRaza.Location = new System.Drawing.Point(225, 90);
		this.lblRaza.Name = "lblRaza";
		this.lblRaza.Size = new System.Drawing.Size(37, 23);
		this.lblRaza.TabIndex = 11;
		this.lblRaza.Text = "Raza:";
		this.txtNombre.BackColor = System.Drawing.Color.White;
		this.txtNombre.Border.Class = "TextBoxBorder";
		this.txtNombre.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombre.ButtonCustom.Tooltip = "";
		this.txtNombre.ButtonCustom2.Tooltip = "";
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.DisabledBackColor = System.Drawing.Color.White;
		this.txtNombre.ForeColor = System.Drawing.Color.Black;
		this.txtNombre.Location = new System.Drawing.Point(50, 52);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.PreventEnterBeep = true;
		this.txtNombre.Size = new System.Drawing.Size(163, 20);
		this.txtNombre.TabIndex = 10;
		this.lblNombre.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblNombre.Location = new System.Drawing.Point(3, 49);
		this.lblNombre.Name = "lblNombre";
		this.lblNombre.Size = new System.Drawing.Size(75, 23);
		this.lblNombre.TabIndex = 9;
		this.lblNombre.Text = "Nombre:";
		this.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnCancelar.Image = (System.Drawing.Image)resources.GetObject("btnCancelar.Image");
		this.btnCancelar.Location = new System.Drawing.Point(804, 152);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(120, 23);
		this.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnCancelar.TabIndex = 8;
		this.btnCancelar.Text = "<b>CANCELAR</b>";
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnEliminar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnEliminar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnEliminar.Image = (System.Drawing.Image)resources.GetObject("btnEliminar.Image");
		this.btnEliminar.Location = new System.Drawing.Point(594, 152);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(120, 23);
		this.btnEliminar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnEliminar.TabIndex = 7;
		this.btnEliminar.Text = "<b>ELIMINAR</b>";
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnGuardar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnGuardar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.Location = new System.Drawing.Point(387, 152);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(120, 23);
		this.btnGuardar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnGuardar.TabIndex = 6;
		this.btnGuardar.Text = "<b>GUARDAR</b>";
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnModificar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnModificar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnModificar.Image = (System.Drawing.Image)resources.GetObject("btnModificar.Image");
		this.btnModificar.Location = new System.Drawing.Point(197, 152);
		this.btnModificar.Name = "btnModificar";
		this.btnModificar.Size = new System.Drawing.Size(120, 23);
		this.btnModificar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnModificar.TabIndex = 5;
		this.btnModificar.Text = "<b>MODIFICAR</b>";
		this.btnModificar.Click += new System.EventHandler(btnModificar_Click);
		this.btnNuevo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnNuevo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnNuevo.Image = (System.Drawing.Image)resources.GetObject("btnNuevo.Image");
		this.btnNuevo.Location = new System.Drawing.Point(3, 152);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(120, 23);
		this.btnNuevo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnNuevo.TabIndex = 4;
		this.btnNuevo.Text = "<b>NUEVO</b>";
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.cmbEspecie.DisplayMember = "Text";
		this.cmbEspecie.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.cmbEspecie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbEspecie.FormattingEnabled = true;
		this.cmbEspecie.ItemHeight = 14;
		this.cmbEspecie.Location = new System.Drawing.Point(50, 90);
		this.cmbEspecie.Name = "cmbEspecie";
		this.cmbEspecie.Size = new System.Drawing.Size(163, 20);
		this.cmbEspecie.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.cmbEspecie.TabIndex = 3;
		this.line1.ForeColor = System.Drawing.Color.DarkCyan;
		this.line1.Location = new System.Drawing.Point(0, 24);
		this.line1.Name = "line1";
		this.line1.Size = new System.Drawing.Size(927, 10);
		this.line1.TabIndex = 2;
		this.line1.Text = "line1";
		this.line1.Thickness = 2;
		this.lblEspecie.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblEspecie.Location = new System.Drawing.Point(3, 90);
		this.lblEspecie.Name = "lblEspecie";
		this.lblEspecie.Size = new System.Drawing.Size(50, 23);
		this.lblEspecie.TabIndex = 1;
		this.lblEspecie.Text = "Especie:";
		this.lblTituloCab.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblTituloCab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTituloCab.Location = new System.Drawing.Point(3, 3);
		this.lblTituloCab.Name = "lblTituloCab";
		this.lblTituloCab.Size = new System.Drawing.Size(156, 23);
		this.lblTituloCab.TabIndex = 0;
		this.lblTituloCab.Text = "<b>INGRESE LOS DATOS:</b>";
		this.panelDetalle.CanvasColor = System.Drawing.SystemColors.Control;
		this.panelDetalle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.panelDetalle.Controls.Add(this.dgvDetalle);
		this.panelDetalle.Controls.Add(this.line2);
		this.panelDetalle.Controls.Add(this.lblTituloDet);
		this.panelDetalle.DisabledBackColor = System.Drawing.Color.Empty;
		this.panelDetalle.Location = new System.Drawing.Point(12, 196);
		this.panelDetalle.Name = "panelDetalle";
		this.panelDetalle.Size = new System.Drawing.Size(927, 306);
		this.panelDetalle.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.panelDetalle.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.panelDetalle.Style.Border = DevComponents.DotNetBar.eBorderType.DoubleLine;
		this.panelDetalle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.panelDetalle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.panelDetalle.Style.GradientAngle = 90;
		this.panelDetalle.TabIndex = 1;
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDetalle.GridColor = System.Drawing.Color.FromArgb(155, 155, 157);
		this.dgvDetalle.Location = new System.Drawing.Point(3, 87);
		this.dgvDetalle.MultiSelect = false;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(921, 216);
		this.dgvDetalle.TabIndex = 5;
		this.dgvDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellDoubleClick);
		this.dgvDetalle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(dgvDetalle_CellFormatting);
		this.line2.ForeColor = System.Drawing.Color.DarkCyan;
		this.line2.Location = new System.Drawing.Point(0, 31);
		this.line2.Name = "line2";
		this.line2.Size = new System.Drawing.Size(927, 10);
		this.line2.TabIndex = 4;
		this.line2.Text = "line2";
		this.line2.Thickness = 2;
		this.lblTituloDet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblTituloDet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTituloDet.Location = new System.Drawing.Point(3, 10);
		this.lblTituloDet.Name = "lblTituloDet";
		this.lblTituloDet.Size = new System.Drawing.Size(189, 23);
		this.lblTituloDet.TabIndex = 3;
		this.lblTituloDet.Text = "<b>BUSQUEDA DE PACIENTES:</b>";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(951, 514);
		base.Controls.Add(this.panelDetalle);
		base.Controls.Add(this.panelCabecera);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "frmPaciente";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.TitleText = "<b>Pacientes</b>";
		this.panelCabecera.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dtiFechaNacimiento).EndInit();
		this.panelDetalle.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
