using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmTecnico : Form
{
	public int Proceso = 1;

	public int codTecnico = 0;

	private clsValidar valida = new clsValidar();

	public clsTecnico tecnico = null;

	private clsAdmOficio admofi = new clsAdmOficio();

	private clsAdmTecnico admtec = new clsAdmTecnico();

	private IContainer components = null;

	private GroupBox gbTecnico;

	private TextBox txtDni;

	private Label label2;

	private TextBox txtCelular;

	private Label label4;

	private Label label6;

	private Label label5;

	private Label label3;

	private TextBox txtNombre;

	private Label label1;

	private Button btnAñadirOficio;

	private RadCheckedDropDownList rcddlOficios;

	private Button btnSalir;

	private Button btnGuardar;

	private DateTimePicker dtpFechaNacimiento;

	private TextBox txtDireccion;

	private TextBox txtApellidos;

	private Label label7;

	public frmTecnico()
	{
		InitializeComponent();
	}

	private void frmTecnico_Load(object sender, EventArgs e)
	{
		definirLimitaciones();
		cargaComboOficios();
		if (Proceso == 2)
		{
			tecnico = admtec.cargaTecnico(codTecnico);
			setDatosAFormulario(tecnico);
			tecnico.Oficios = admofi.listaOficios(codTecnico);
			if (tecnico.Oficios != null)
			{
				asignarSeleccionadosAlComboOficios(tecnico.Oficios);
			}
		}
	}

	private void setDatosAFormulario(clsTecnico tecnico)
	{
		txtDni.Text = tecnico.Dni;
		txtNombre.Text = tecnico.Nombre;
		txtCelular.Text = tecnico.Celular;
		dtpFechaNacimiento.Value = tecnico.FechaNacimiento;
		txtDireccion.Text = tecnico.Direccion;
		txtApellidos.Text = tecnico.Apellidos;
	}

	private void asignarSeleccionadosAlComboOficios(List<clsOficio> oficios)
	{
		List<int> soloCodigos = Enumerable.Select<clsOficio, int>(oficios.AsEnumerable(), (Func<clsOficio, int>)((clsOficio x) => x.Id)).ToList();
		foreach (RadCheckedListDataItem item in rcddlOficios.Items)
		{
			if (soloCodigos.Contains(Convert.ToInt32(item.Value)))
			{
				item.Checked = true;
			}
		}
	}

	private void cargaComboOficios()
	{
		DataTable data = admofi.listaOficios();
		rcddlOficios.DataSource = data;
		rcddlOficios.ValueMember = "idOficio";
		rcddlOficios.DisplayMember = "descripcion";
	}

	private void definirLimitaciones()
	{
		txtDni.MaxLength = 8;
		txtCelular.MaxLength = 9;
		txtNombre.MaxLength = 200;
		txtApellidos.MaxLength = 200;
		txtDireccion.MaxLength = 255;
	}

	private void btnAñadirOficio_Click(object sender, EventArgs e)
	{
		try
		{
			clsOficio nuevo = new clsOficio();
			frmOficio form = new frmOficio();
			DialogResult rpta = form.ShowDialog();
			if (rpta == DialogResult.Yes)
			{
				nuevo = form.oficio;
				if (admofi.insert(nuevo))
				{
					cargaComboOficios();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (!validarDatosTecnico())
			{
				return;
			}
			clsTecnico tec_aux = obtenerTecnico();
			if (Proceso == 1)
			{
				if (admtec.insert(tec_aux))
				{
					bool band = admtec.eliminarOficios(tec_aux.Id);
					foreach (clsOficio item in tec_aux.Oficios)
					{
						if (!admtec.registraOficio(tec_aux.Id, item.Id))
						{
						}
					}
				}
				MessageBox.Show("Tecnico Registrado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				base.DialogResult = DialogResult.Yes;
				Close();
			}
			else if (Proceso == 2)
			{
				tec_aux.Id = tecnico.Id;
				if (admtec.update(tec_aux))
				{
					admtec.eliminarOficios(tec_aux.Id);
					foreach (clsOficio item2 in tec_aux.Oficios)
					{
						if (!admtec.registraOficio(tec_aux.Id, item2.Id))
						{
						}
					}
				}
				MessageBox.Show("Tecnico Actualizado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				base.DialogResult = DialogResult.Yes;
				Close();
			}
			else
			{
				MessageBox.Show("Proceso No Definido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool validarDatosTecnico()
	{
		if (txtDni.Text == "" || txtDni.Text.Length != 8)
		{
			MessageBox.Show("Debe indicar un numero de dni", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtDni.Focus();
			return false;
		}
		if (txtNombre.Text == "")
		{
			MessageBox.Show("Debe indicar un nombre de tecnico", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNombre.Focus();
			return false;
		}
		if (txtApellidos.Text == "")
		{
			MessageBox.Show("Debe indicar un apellido de tecnico", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtApellidos.Focus();
			return false;
		}
		if (txtCelular.Text == "" || txtCelular.Text.Length != 9)
		{
			MessageBox.Show("Debe indicar un numero de celular", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtCelular.Focus();
			return false;
		}
		_ = dtpFechaNacimiento.Value;
		if (false)
		{
			MessageBox.Show("Debe indicar una fecha de nacimiento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			dtpFechaNacimiento.Focus();
			return false;
		}
		if (dtpFechaNacimiento.Value > DateTime.Now.AddYears(-18))
		{
			MessageBox.Show("La fecha de cumpleaños debe ser para alguien mayor a 18años", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			dtpFechaNacimiento.Focus();
			return false;
		}
		if (rcddlOficios.CheckedItems.Count == 0)
		{
			MessageBox.Show("Debe seleccionar al menos un oficio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			rcddlOficios.Focus();
			return false;
		}
		return true;
	}

	private clsTecnico obtenerTecnico()
	{
		clsTecnico aux = new clsTecnico();
		aux.Celular = txtCelular.Text;
		aux.Nombre = txtNombre.Text;
		aux.Apellidos = txtApellidos.Text;
		aux.Dni = txtDni.Text;
		aux.FechaNacimiento = dtpFechaNacimiento.Value;
		aux.Direccion = txtDireccion.Text;
		aux.FechaRegistro = DateTime.Now;
		aux.CodUserRegistro = frmLogin.iCodUser;
		aux.Oficios = obtenerListadoOficiosSeleccionados();
		return aux;
	}

	private List<clsOficio> obtenerListadoOficiosSeleccionados()
	{
		List<clsOficio> listadoSeleccionados = new List<clsOficio>();
		foreach (RadCheckedListDataItem item in rcddlOficios.CheckedItems)
		{
			clsOficio aux = new clsOficio();
			aux.Id = Convert.ToInt32(item.Value);
			aux.Descripcion = item.Text;
			listadoSeleccionados.Add(aux);
		}
		return listadoSeleccionados;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (Proceso == 1)
		{
			DialogResult rpta = MessageBox.Show("Esta seguro de salir sin guardar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rpta != DialogResult.Yes)
			{
				return;
			}
		}
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.SOLONumeros(sender, e);
	}

	private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.SOLONumeros(sender, e);
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
		this.gbTecnico = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
		this.btnAñadirOficio = new System.Windows.Forms.Button();
		this.rcddlOficios = new Telerik.WinControls.UI.RadCheckedDropDownList();
		this.txtDni = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCelular = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtApellidos = new System.Windows.Forms.TextBox();
		this.gbTecnico.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rcddlOficios).BeginInit();
		base.SuspendLayout();
		this.gbTecnico.Controls.Add(this.btnSalir);
		this.gbTecnico.Controls.Add(this.btnGuardar);
		this.gbTecnico.Controls.Add(this.dtpFechaNacimiento);
		this.gbTecnico.Controls.Add(this.btnAñadirOficio);
		this.gbTecnico.Controls.Add(this.rcddlOficios);
		this.gbTecnico.Controls.Add(this.txtDni);
		this.gbTecnico.Controls.Add(this.label2);
		this.gbTecnico.Controls.Add(this.txtCelular);
		this.gbTecnico.Controls.Add(this.label4);
		this.gbTecnico.Controls.Add(this.label6);
		this.gbTecnico.Controls.Add(this.txtDireccion);
		this.gbTecnico.Controls.Add(this.label5);
		this.gbTecnico.Controls.Add(this.label3);
		this.gbTecnico.Controls.Add(this.txtApellidos);
		this.gbTecnico.Controls.Add(this.label7);
		this.gbTecnico.Controls.Add(this.txtNombre);
		this.gbTecnico.Controls.Add(this.label1);
		this.gbTecnico.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gbTecnico.Location = new System.Drawing.Point(0, 0);
		this.gbTecnico.Name = "gbTecnico";
		this.gbTecnico.Size = new System.Drawing.Size(433, 357);
		this.gbTecnico.TabIndex = 0;
		this.gbTecnico.TabStop = false;
		this.gbTecnico.Text = "Tecnico";
		this.btnSalir.BackColor = System.Drawing.Color.LightCoral;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.ForeColor = System.Drawing.Color.DarkRed;
		this.btnSalir.Location = new System.Drawing.Point(336, 320);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 23);
		this.btnSalir.TabIndex = 5;
		this.btnSalir.Text = "SALIR";
		this.btnSalir.UseVisualStyleBackColor = false;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.ForeColor = System.Drawing.Color.Blue;
		this.btnGuardar.Location = new System.Drawing.Point(249, 320);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(81, 23);
		this.btnGuardar.TabIndex = 5;
		this.btnGuardar.Text = "GUARDAR";
		this.btnGuardar.UseVisualStyleBackColor = false;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.dtpFechaNacimiento.Location = new System.Drawing.Point(129, 127);
		this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
		this.dtpFechaNacimiento.Size = new System.Drawing.Size(200, 20);
		this.dtpFechaNacimiento.TabIndex = 4;
		this.btnAñadirOficio.BackColor = System.Drawing.Color.White;
		this.btnAñadirOficio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAñadirOficio.ForeColor = System.Drawing.SystemColors.ControlText;
		this.btnAñadirOficio.Location = new System.Drawing.Point(337, 213);
		this.btnAñadirOficio.Name = "btnAñadirOficio";
		this.btnAñadirOficio.Size = new System.Drawing.Size(74, 24);
		this.btnAñadirOficio.TabIndex = 3;
		this.btnAñadirOficio.Text = "Añadir";
		this.btnAñadirOficio.UseVisualStyleBackColor = false;
		this.btnAñadirOficio.Click += new System.EventHandler(btnAñadirOficio_Click);
		this.rcddlOficios.AutoSize = false;
		this.rcddlOficios.Location = new System.Drawing.Point(129, 213);
		this.rcddlOficios.Multiline = true;
		this.rcddlOficios.Name = "rcddlOficios";
		this.rcddlOficios.Size = new System.Drawing.Size(202, 91);
		this.rcddlOficios.TabIndex = 2;
		this.rcddlOficios.ThemeName = "Material";
		this.txtDni.Location = new System.Drawing.Point(129, 23);
		this.txtDni.Name = "txtDni";
		this.txtDni.Size = new System.Drawing.Size(135, 20);
		this.txtDni.TabIndex = 1;
		this.txtDni.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtDni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDni_KeyPress);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 26);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(26, 13);
		this.label2.TabIndex = 0;
		this.label2.Text = "Dni:";
		this.txtCelular.Location = new System.Drawing.Point(129, 101);
		this.txtCelular.Name = "txtCelular";
		this.txtCelular.Size = new System.Drawing.Size(135, 20);
		this.txtCelular.TabIndex = 1;
		this.txtCelular.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCelular.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCelular_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(12, 104);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(42, 13);
		this.label4.TabIndex = 0;
		this.label4.Text = "Celular:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 216);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(37, 13);
		this.label6.TabIndex = 0;
		this.label6.Text = "Oficio:";
		this.txtDireccion.Location = new System.Drawing.Point(129, 153);
		this.txtDireccion.Multiline = true;
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(282, 54);
		this.txtDireccion.TabIndex = 1;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 156);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(55, 13);
		this.label5.TabIndex = 0;
		this.label5.Text = "Direccion:";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 130);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(111, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "Fecha de Nacimiento:";
		this.txtNombre.Location = new System.Drawing.Point(129, 49);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(282, 20);
		this.txtNombre.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 52);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(47, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Nombre:";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(12, 78);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(52, 13);
		this.label7.TabIndex = 0;
		this.label7.Text = "Apellidos:";
		this.txtApellidos.Location = new System.Drawing.Point(129, 75);
		this.txtApellidos.Name = "txtApellidos";
		this.txtApellidos.Size = new System.Drawing.Size(282, 20);
		this.txtApellidos.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(433, 357);
		base.Controls.Add(this.gbTecnico);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.MinimumSize = new System.Drawing.Size(449, 385);
		base.Name = "frmTecnico";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Tecnico";
		base.Load += new System.EventHandler(frmTecnico_Load);
		this.gbTecnico.ResumeLayout(false);
		this.gbTecnico.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.rcddlOficios).EndInit();
		base.ResumeLayout(false);
	}
}
