using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmHistoriaClinica : Office2007Form
{
	private clsPaciente Paciente;

	private clsAdmClinica AdmClin;

	private clsReporteHistoriaClinica Rpt;

	private clsHistoria HistoriaCab;

	private IContainer components = null;

	private PanelEx panelPaciente;

	private LabelX lblPaciente;

	private TextBoxX txtNombre;

	private LabelX lblEspecie;

	private TextBoxX txtEspecie;

	private TextBoxX txtRaza;

	private LabelX lblRaza;

	private TextBoxX txtPropietario;

	private LabelX lblProp;

	private TextBoxX txtDireccion;

	private LabelX labelX1;

	private TextBoxX txtSexo;

	private LabelX lblSexo;

	private TextBoxX txtEdad;

	private LabelX lblEdad;

	private ButtonX btnBuscar;

	private ButtonX btnImprimir;

	private PanelEx panelDetalle;

	private DataGridViewX dgvDetalle;

	private ContextMenuStrip cmsPrincipal;

	private ToolStripMenuItem verDetalleToolStripMenuItem;

	private ToolStripMenuItem eliminarToolStripMenuItem;

	private ButtonX btnAgregar;

	private LabelX lblNumero;

	private TextBoxX txtNumero;

	private ButtonX btnGuardar;

	public frmHistoriaClinica()
	{
		InitializeComponent();
		PostConstructor();
	}

	private void PostConstructor()
	{
		AdmClin = new clsAdmClinica();
		Rpt = new clsReporteHistoriaClinica();
	}

	private void txtNombre_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		frmPaciente Paciente = new frmPaciente(this);
		Paciente.ShowDialog(this);
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

	public void CargarOcurrencias()
	{
		try
		{
			if (HistoriaCab != null)
			{
				PrepareData(AdmClin.ListaDetalleHistorial(HistoriaCab.ID));
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("ERROR: " + ex.Message, "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrepareData(DataTable DataBD)
	{
		DataTable table = new DataTable();
		table.Columns.Add("ID", typeof(string));
		table.Columns.Add("FECHA/HORA", typeof(string));
		table.Columns.Add("TEMPERATURA (C°)", typeof(string));
		table.Columns.Add("PESO (KG)", typeof(string));
		table.Columns.Add("NOTAS", typeof(string));
		table.Columns.Add("TRATAMIENTOS", typeof(string));
		table.Columns.Add("FALLECIDO", typeof(string));
		foreach (DataRow row in DataBD.Rows)
		{
			table.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString().Equals("0") ? "NO" : "SI");
		}
		dgvDetalle.DataSource = table;
		dgvDetalle.Columns[0].Visible = false;
	}

	public void ObtenerDesdeBuscador(int CodPaciente)
	{
		Paciente = AdmClin.CargaPaciente(CodPaciente);
		txtEspecie.Text = Paciente.Especie;
		txtNombre.Text = Paciente.Nombre;
		txtRaza.Text = Paciente.Raza;
		txtPropietario.Text = Paciente.Propietario;
		txtDireccion.Text = Paciente.Direccion;
		txtEdad.Text = CalcularDiferenciaEntreFechas(Paciente.FechaNacimiento, DateTime.Now);
		txtSexo.Text = Paciente.Sexo;
	}

	private void btnAgregar_Click(object sender, EventArgs e)
	{
		bool res = false;
		if (dgvDetalle.RowCount > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				if (row.Cells[6].Value.ToString().Equals("SI"))
				{
					res = true;
				}
			}
		}
		if (HistoriaCab != null && !res)
		{
			frmNuevaHistoria frm = new frmNuevaHistoria(this, HistoriaCab);
			frm.ShowDialog(this);
		}
		else if (res)
		{
			MessageBox.Show("EL PACIENTE YA HA FALLECIDO :(. NO SE PUEDEN AGREGAR MAS OCURRENCIAS", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void panelPaciente_Click(object sender, EventArgs e)
	{
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!ValidarRegistro())
		{
			return;
		}
		try
		{
			HistoriaCab = new clsHistoria();
			HistoriaCab.Numero = txtNumero.Text;
			HistoriaCab.PacienteID = Paciente.ID;
			HistoriaCab.UsuarioID = frmLogin.iCodUser;
			HistoriaCab.FechaRegistro = DateTime.Now;
			if (AdmClin.InsertHistoriaCabecera(HistoriaCab))
			{
				MessageBox.Show("HISTORIA CLINICA REGISTRADA CORRECTAMENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				PostConstructor();
			}
			else
			{
				MessageBox.Show("ERROR AL REGISTRAR LA HISTORIA CLINICA", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("ERROR: " + ex.Message, "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool ValidarRegistro()
	{
		if (string.IsNullOrEmpty(txtNumero.Text))
		{
			MessageBox.Show("INGRESE EL NUMERO DE LA HISTORIA CLINICA", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNumero.Focus();
			return false;
		}
		if (string.IsNullOrEmpty(txtNombre.Text) || Paciente == null)
		{
			MessageBox.Show("SELECCIONE EL PACIENTE", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNombre.Focus();
			return false;
		}
		return true;
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(txtNumero.Text))
		{
			HistoriaCab = AdmClin.CargaHistoriaCabecera(txtNumero.Text);
			if (HistoriaCab != null)
			{
				Paciente = AdmClin.CargaPaciente(HistoriaCab.PacienteID);
				txtEspecie.Text = Paciente.Especie;
				txtNombre.Text = Paciente.Nombre;
				txtRaza.Text = Paciente.Raza;
				txtPropietario.Text = Paciente.Propietario;
				txtDireccion.Text = Paciente.Direccion;
				txtEdad.Text = CalcularDiferenciaEntreFechas(Paciente.FechaNacimiento, DateTime.Now);
				txtSexo.Text = Paciente.Sexo;
				CargarOcurrencias();
			}
			else
			{
				MessageBox.Show("NO SE ENCONTRO NINGUNA HISTORIA CON EL NUMERO INGRESADO", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		if (HistoriaCab != null)
		{
			try
			{
				CRHistoriaClinica rpt = new CRHistoriaClinica();
				frmRptHistoriaClinica frm = new frmRptHistoriaClinica();
				rpt.SetDataSource(Rpt.HistoriaClinica(HistoriaCab.ID));
				frm.crystalReportViewer1.ReportSource = rpt;
				frm.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR: " + ex.Message, "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmHistoriaClinica));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		this.panelPaciente = new DevComponents.DotNetBar.PanelEx();
		this.btnGuardar = new DevComponents.DotNetBar.ButtonX();
		this.txtNumero = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblNumero = new DevComponents.DotNetBar.LabelX();
		this.btnBuscar = new DevComponents.DotNetBar.ButtonX();
		this.txtSexo = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblSexo = new DevComponents.DotNetBar.LabelX();
		this.txtEdad = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblEdad = new DevComponents.DotNetBar.LabelX();
		this.txtDireccion = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.txtPropietario = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblProp = new DevComponents.DotNetBar.LabelX();
		this.txtRaza = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblRaza = new DevComponents.DotNetBar.LabelX();
		this.txtEspecie = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblEspecie = new DevComponents.DotNetBar.LabelX();
		this.txtNombre = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblPaciente = new DevComponents.DotNetBar.LabelX();
		this.btnImprimir = new DevComponents.DotNetBar.ButtonX();
		this.panelDetalle = new DevComponents.DotNetBar.PanelEx();
		this.dgvDetalle = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.cmsPrincipal = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.verDetalleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.btnAgregar = new DevComponents.DotNetBar.ButtonX();
		this.panelPaciente.SuspendLayout();
		this.panelDetalle.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.cmsPrincipal.SuspendLayout();
		base.SuspendLayout();
		this.panelPaciente.CanvasColor = System.Drawing.SystemColors.Control;
		this.panelPaciente.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.panelPaciente.Controls.Add(this.btnGuardar);
		this.panelPaciente.Controls.Add(this.txtNumero);
		this.panelPaciente.Controls.Add(this.lblNumero);
		this.panelPaciente.Controls.Add(this.btnBuscar);
		this.panelPaciente.Controls.Add(this.txtSexo);
		this.panelPaciente.Controls.Add(this.lblSexo);
		this.panelPaciente.Controls.Add(this.txtEdad);
		this.panelPaciente.Controls.Add(this.lblEdad);
		this.panelPaciente.Controls.Add(this.txtDireccion);
		this.panelPaciente.Controls.Add(this.labelX1);
		this.panelPaciente.Controls.Add(this.txtPropietario);
		this.panelPaciente.Controls.Add(this.lblProp);
		this.panelPaciente.Controls.Add(this.txtRaza);
		this.panelPaciente.Controls.Add(this.lblRaza);
		this.panelPaciente.Controls.Add(this.txtEspecie);
		this.panelPaciente.Controls.Add(this.lblEspecie);
		this.panelPaciente.Controls.Add(this.txtNombre);
		this.panelPaciente.Controls.Add(this.lblPaciente);
		this.panelPaciente.DisabledBackColor = System.Drawing.Color.Empty;
		this.panelPaciente.Location = new System.Drawing.Point(12, 12);
		this.panelPaciente.Name = "panelPaciente";
		this.panelPaciente.Size = new System.Drawing.Size(646, 185);
		this.panelPaciente.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.panelPaciente.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.panelPaciente.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.panelPaciente.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.panelPaciente.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.panelPaciente.Style.GradientAngle = 90;
		this.panelPaciente.TabIndex = 0;
		this.panelPaciente.Click += new System.EventHandler(panelPaciente_Click);
		this.btnGuardar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.Location = new System.Drawing.Point(431, 138);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(209, 23);
		this.btnGuardar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnGuardar.TabIndex = 18;
		this.btnGuardar.Tooltip = "Asocia éste número de guía al paciente seleccionado.";
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtNumero.BackColor = System.Drawing.Color.White;
		this.txtNumero.Border.Class = "TextBoxBorder";
		this.txtNumero.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNumero.ButtonCustom.Tooltip = "";
		this.txtNumero.ButtonCustom2.Tooltip = "";
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.DisabledBackColor = System.Drawing.Color.White;
		this.txtNumero.ForeColor = System.Drawing.Color.Black;
		this.txtNumero.Location = new System.Drawing.Point(73, 19);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.PreventEnterBeep = true;
		this.txtNumero.Size = new System.Drawing.Size(100, 20);
		this.txtNumero.TabIndex = 17;
		this.lblNumero.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblNumero.Location = new System.Drawing.Point(3, 12);
		this.lblNumero.Name = "lblNumero";
		this.lblNumero.Size = new System.Drawing.Size(50, 32);
		this.lblNumero.TabIndex = 16;
		this.lblNumero.Text = "<b>Número:</b>";
		this.btnBuscar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnBuscar.Image = (System.Drawing.Image)resources.GetObject("btnBuscar.Image");
		this.btnBuscar.Location = new System.Drawing.Point(191, 19);
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.Size = new System.Drawing.Size(94, 20);
		this.btnBuscar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnBuscar.TabIndex = 14;
		this.btnBuscar.Text = "<b>Buscar</b>";
		this.btnBuscar.Click += new System.EventHandler(btnBuscar_Click);
		this.txtSexo.BackColor = System.Drawing.Color.White;
		this.txtSexo.Border.Class = "TextBoxBorder";
		this.txtSexo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtSexo.ButtonCustom.Tooltip = "";
		this.txtSexo.ButtonCustom2.Tooltip = "";
		this.txtSexo.DisabledBackColor = System.Drawing.Color.White;
		this.txtSexo.Enabled = false;
		this.txtSexo.ForeColor = System.Drawing.Color.Black;
		this.txtSexo.Location = new System.Drawing.Point(431, 71);
		this.txtSexo.Name = "txtSexo";
		this.txtSexo.PreventEnterBeep = true;
		this.txtSexo.Size = new System.Drawing.Size(212, 20);
		this.txtSexo.TabIndex = 13;
		this.lblSexo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblSexo.Location = new System.Drawing.Point(361, 71);
		this.lblSexo.Name = "lblSexo";
		this.lblSexo.Size = new System.Drawing.Size(64, 20);
		this.lblSexo.TabIndex = 12;
		this.lblSexo.Text = "Sexo:";
		this.lblSexo.WordWrap = true;
		this.txtEdad.BackColor = System.Drawing.Color.White;
		this.txtEdad.Border.Class = "TextBoxBorder";
		this.txtEdad.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtEdad.ButtonCustom.Tooltip = "";
		this.txtEdad.ButtonCustom2.Tooltip = "";
		this.txtEdad.DisabledBackColor = System.Drawing.Color.White;
		this.txtEdad.Enabled = false;
		this.txtEdad.ForeColor = System.Drawing.Color.Black;
		this.txtEdad.Location = new System.Drawing.Point(431, 45);
		this.txtEdad.Name = "txtEdad";
		this.txtEdad.PreventEnterBeep = true;
		this.txtEdad.Size = new System.Drawing.Size(212, 20);
		this.txtEdad.TabIndex = 11;
		this.lblEdad.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblEdad.Location = new System.Drawing.Point(361, 45);
		this.lblEdad.Name = "lblEdad";
		this.lblEdad.Size = new System.Drawing.Size(64, 20);
		this.lblEdad.TabIndex = 10;
		this.lblEdad.Text = "Edad:";
		this.lblEdad.WordWrap = true;
		this.txtDireccion.BackColor = System.Drawing.Color.White;
		this.txtDireccion.Border.Class = "TextBoxBorder";
		this.txtDireccion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtDireccion.ButtonCustom.Tooltip = "";
		this.txtDireccion.ButtonCustom2.Tooltip = "";
		this.txtDireccion.DisabledBackColor = System.Drawing.Color.White;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.ForeColor = System.Drawing.Color.Black;
		this.txtDireccion.Location = new System.Drawing.Point(431, 13);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.PreventEnterBeep = true;
		this.txtDireccion.Size = new System.Drawing.Size(212, 20);
		this.txtDireccion.TabIndex = 9;
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Location = new System.Drawing.Point(361, 13);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(64, 20);
		this.labelX1.TabIndex = 8;
		this.labelX1.Text = "Dirección:";
		this.labelX1.WordWrap = true;
		this.txtPropietario.BackColor = System.Drawing.Color.White;
		this.txtPropietario.Border.Class = "TextBoxBorder";
		this.txtPropietario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtPropietario.ButtonCustom.Tooltip = "";
		this.txtPropietario.ButtonCustom2.Tooltip = "";
		this.txtPropietario.DisabledBackColor = System.Drawing.Color.White;
		this.txtPropietario.Enabled = false;
		this.txtPropietario.ForeColor = System.Drawing.Color.Black;
		this.txtPropietario.Location = new System.Drawing.Point(73, 141);
		this.txtPropietario.Name = "txtPropietario";
		this.txtPropietario.PreventEnterBeep = true;
		this.txtPropietario.Size = new System.Drawing.Size(212, 20);
		this.txtPropietario.TabIndex = 7;
		this.lblProp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblProp.Location = new System.Drawing.Point(3, 141);
		this.lblProp.Name = "lblProp";
		this.lblProp.Size = new System.Drawing.Size(64, 20);
		this.lblProp.TabIndex = 6;
		this.lblProp.Text = "Propietario";
		this.lblProp.WordWrap = true;
		this.txtRaza.BackColor = System.Drawing.Color.White;
		this.txtRaza.Border.Class = "TextBoxBorder";
		this.txtRaza.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtRaza.ButtonCustom.Tooltip = "";
		this.txtRaza.ButtonCustom2.Tooltip = "";
		this.txtRaza.DisabledBackColor = System.Drawing.Color.White;
		this.txtRaza.Enabled = false;
		this.txtRaza.ForeColor = System.Drawing.Color.Black;
		this.txtRaza.Location = new System.Drawing.Point(73, 115);
		this.txtRaza.Name = "txtRaza";
		this.txtRaza.PreventEnterBeep = true;
		this.txtRaza.Size = new System.Drawing.Size(212, 20);
		this.txtRaza.TabIndex = 5;
		this.lblRaza.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblRaza.Location = new System.Drawing.Point(3, 112);
		this.lblRaza.Name = "lblRaza";
		this.lblRaza.Size = new System.Drawing.Size(52, 23);
		this.lblRaza.TabIndex = 4;
		this.lblRaza.Text = "Raza:";
		this.txtEspecie.BackColor = System.Drawing.Color.White;
		this.txtEspecie.Border.Class = "TextBoxBorder";
		this.txtEspecie.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtEspecie.ButtonCustom.Tooltip = "";
		this.txtEspecie.ButtonCustom2.Tooltip = "";
		this.txtEspecie.DisabledBackColor = System.Drawing.Color.White;
		this.txtEspecie.Enabled = false;
		this.txtEspecie.ForeColor = System.Drawing.Color.Black;
		this.txtEspecie.Location = new System.Drawing.Point(73, 89);
		this.txtEspecie.Name = "txtEspecie";
		this.txtEspecie.PreventEnterBeep = true;
		this.txtEspecie.Size = new System.Drawing.Size(212, 20);
		this.txtEspecie.TabIndex = 3;
		this.lblEspecie.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblEspecie.Location = new System.Drawing.Point(3, 86);
		this.lblEspecie.Name = "lblEspecie";
		this.lblEspecie.Size = new System.Drawing.Size(52, 23);
		this.lblEspecie.TabIndex = 2;
		this.lblEspecie.Text = "Especie:";
		this.txtNombre.BackColor = System.Drawing.Color.White;
		this.txtNombre.Border.Class = "TextBoxBorder";
		this.txtNombre.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombre.ButtonCustom.Tooltip = "";
		this.txtNombre.ButtonCustom2.Tooltip = "";
		this.txtNombre.DisabledBackColor = System.Drawing.Color.White;
		this.txtNombre.ForeColor = System.Drawing.Color.Black;
		this.txtNombre.Location = new System.Drawing.Point(73, 60);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.PreventEnterBeep = true;
		this.txtNombre.ReadOnly = true;
		this.txtNombre.Size = new System.Drawing.Size(212, 20);
		this.txtNombre.TabIndex = 1;
		this.txtNombre.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
		this.txtNombre.WatermarkImage = (System.Drawing.Image)resources.GetObject("txtNombre.WatermarkImage");
		this.txtNombre.WatermarkText = "Doble clic para buscar al paciente...";
		this.txtNombre.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtNombre_MouseDoubleClick);
		this.lblPaciente.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblPaciente.Location = new System.Drawing.Point(3, 57);
		this.lblPaciente.Name = "lblPaciente";
		this.lblPaciente.Size = new System.Drawing.Size(52, 23);
		this.lblPaciente.TabIndex = 0;
		this.lblPaciente.Text = "Paciente:";
		this.btnImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
		this.btnImprimir.Image = (System.Drawing.Image)resources.GetObject("btnImprimir.Image");
		this.btnImprimir.Location = new System.Drawing.Point(523, 238);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(117, 23);
		this.btnImprimir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnImprimir.TabIndex = 15;
		this.btnImprimir.Text = "<b>Imprimir</b>";
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.panelDetalle.CanvasColor = System.Drawing.SystemColors.Control;
		this.panelDetalle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.panelDetalle.Controls.Add(this.btnImprimir);
		this.panelDetalle.Controls.Add(this.dgvDetalle);
		this.panelDetalle.Controls.Add(this.btnAgregar);
		this.panelDetalle.DisabledBackColor = System.Drawing.Color.Empty;
		this.panelDetalle.Location = new System.Drawing.Point(12, 200);
		this.panelDetalle.Name = "panelDetalle";
		this.panelDetalle.Size = new System.Drawing.Size(646, 269);
		this.panelDetalle.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.panelDetalle.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.panelDetalle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.panelDetalle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.panelDetalle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.panelDetalle.Style.GradientAngle = 90;
		this.panelDetalle.TabIndex = 1;
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dgvDetalle.BackgroundColor = System.Drawing.Color.FromArgb(239, 239, 242);
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.ContextMenuStrip = this.cmsPrincipal;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(239, 239, 242);
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgvDetalle.EnableHeadersVisualStyles = false;
		this.dgvDetalle.GridColor = System.Drawing.Color.FromArgb(155, 155, 157);
		this.dgvDetalle.Location = new System.Drawing.Point(3, 3);
		this.dgvDetalle.MultiSelect = false;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(640, 229);
		this.dgvDetalle.TabIndex = 0;
		this.cmsPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.verDetalleToolStripMenuItem, this.eliminarToolStripMenuItem });
		this.cmsPrincipal.Name = "cmsPrincipal";
		this.cmsPrincipal.Size = new System.Drawing.Size(130, 48);
		this.verDetalleToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("verDetalleToolStripMenuItem.Image");
		this.verDetalleToolStripMenuItem.Name = "verDetalleToolStripMenuItem";
		this.verDetalleToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
		this.verDetalleToolStripMenuItem.Text = "Ver Detalle";
		this.eliminarToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("eliminarToolStripMenuItem.Image");
		this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
		this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
		this.eliminarToolStripMenuItem.Text = "Eliminar";
		this.btnAgregar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnAgregar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
		this.btnAgregar.Image = (System.Drawing.Image)resources.GetObject("btnAgregar.Image");
		this.btnAgregar.Location = new System.Drawing.Point(3, 238);
		this.btnAgregar.Name = "btnAgregar";
		this.btnAgregar.Size = new System.Drawing.Size(170, 23);
		this.btnAgregar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnAgregar.TabIndex = 15;
		this.btnAgregar.Text = "Agregar Nueva Ocurrencia";
		this.btnAgregar.Click += new System.EventHandler(btnAgregar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(670, 481);
		base.Controls.Add(this.panelDetalle);
		base.Controls.Add(this.panelPaciente);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "frmHistoriaClinica";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.TitleText = "<b>Historia Clínica</b>";
		this.panelPaciente.ResumeLayout(false);
		this.panelDetalle.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.cmsPrincipal.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
