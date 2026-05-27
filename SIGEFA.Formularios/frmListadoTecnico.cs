using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoTecnico : Form
{
	private clsAdmTecnico admtec = new clsAdmTecnico();

	public int Proceso = 1;

	internal int codTecnicoCreado;

	private int codTecnico = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Button btnAñadirTecnico;

	private RadGridView rgvTecnicos;

	private Button btnCancelar;

	private Button btnActualizar;

	private Button btnAdministrarOficio;

	public frmListadoTecnico()
	{
		InitializeComponent();
	}

	private void frmListadoTecnico_Load(object sender, EventArgs e)
	{
		cargaListado();
		if (Proceso == 2)
		{
			btnCancelar.Visible = true;
		}
	}

	private void cargaListado()
	{
		rgvTecnicos.DataSource = admtec.listaTecnicos();
	}

	private void btnAñadirTecnico_Click(object sender, EventArgs e)
	{
		frmTecnico form = mdi_Menu.buscarFrmTecnico("frmTecnico", 1);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmTecnico();
		form.Proceso = 1;
		DialogResult rpta = form.ShowDialog();
		string mens = "";
		foreach (GridViewDataColumn item in rgvTecnicos.Columns)
		{
			mens = mens + "\n" + item.Name + " - " + item.Width;
		}
		if (rpta == DialogResult.Yes)
		{
			cargaListado();
		}
	}

	private void rgvTecnicos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			if (Proceso == 2)
			{
				codTecnicoCreado = Convert.ToInt32(e.Row.Cells["colId"].Value);
				base.DialogResult = DialogResult.Yes;
				Close();
			}
			else
			{
				if (Proceso != 1)
				{
					return;
				}
				frmTecnico form = mdi_Menu.buscarFrmTecnico("frmTecnico", 2);
				if (form != null)
				{
					form.Activate();
					return;
				}
				form = new frmTecnico();
				form.Proceso = 2;
				form.codTecnico = Convert.ToInt32(e.Row.Cells["colId"].Value);
				DialogResult rpta = form.ShowDialog();
				if (rpta == DialogResult.Yes)
				{
					cargaListado();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void rgvTecnicos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			codTecnico = Convert.ToInt32(e.Row.Cells["colId"].Value);
		}
		else
		{
			codTecnico = 0;
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		if (codTecnico != 0)
		{
			frmTecnico form = mdi_Menu.buscarFrmTecnico("frmTecnico", 2);
			if (form != null)
			{
				form.Activate();
				return;
			}
			form = new frmTecnico();
			form.Proceso = 2;
			form.codTecnico = codTecnico;
			DialogResult rpta = form.ShowDialog();
			cargaListado();
		}
		else
		{
			MessageBox.Show("Seleccione un tecnico para actualizar datos.", "Infomacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnAdministrarOficio_Click(object sender, EventArgs e)
	{
		frmListadoOficios frm = new frmListadoOficios();
		frm.ShowDialog();
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnAñadirTecnico = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvTecnicos = new Telerik.WinControls.UI.RadGridView();
		this.btnAdministrarOficio = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvTecnicos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTecnicos.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnAdministrarOficio);
		this.groupBox1.Controls.Add(this.btnActualizar);
		this.groupBox1.Controls.Add(this.btnCancelar);
		this.groupBox1.Controls.Add(this.btnAñadirTecnico);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1344, 60);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.btnActualizar.BackColor = System.Drawing.Color.White;
		this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlText;
		this.btnActualizar.Location = new System.Drawing.Point(145, 12);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(151, 30);
		this.btnActualizar.TabIndex = 7;
		this.btnActualizar.Text = "Actualizar Tecnico";
		this.btnActualizar.UseVisualStyleBackColor = false;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.btnCancelar.BackColor = System.Drawing.Color.LightCoral;
		this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.ForeColor = System.Drawing.Color.DarkRed;
		this.btnCancelar.Location = new System.Drawing.Point(855, 12);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(110, 30);
		this.btnCancelar.TabIndex = 6;
		this.btnCancelar.Text = "CANCELAR";
		this.btnCancelar.UseVisualStyleBackColor = false;
		this.btnCancelar.Visible = false;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnAñadirTecnico.BackColor = System.Drawing.Color.White;
		this.btnAñadirTecnico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAñadirTecnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAñadirTecnico.ForeColor = System.Drawing.SystemColors.ControlText;
		this.btnAñadirTecnico.Location = new System.Drawing.Point(12, 12);
		this.btnAñadirTecnico.Name = "btnAñadirTecnico";
		this.btnAñadirTecnico.Size = new System.Drawing.Size(127, 30);
		this.btnAñadirTecnico.TabIndex = 4;
		this.btnAñadirTecnico.Text = "Agregar Tecnico";
		this.btnAñadirTecnico.UseVisualStyleBackColor = false;
		this.btnAñadirTecnico.Click += new System.EventHandler(btnAñadirTecnico_Click);
		this.groupBox2.Controls.Add(this.rgvTecnicos);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 60);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1344, 376);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Listado de Tecnicos";
		this.rgvTecnicos.AutoScroll = true;
		this.rgvTecnicos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvTecnicos.EnableGestures = false;
		this.rgvTecnicos.Location = new System.Drawing.Point(3, 16);
		this.rgvTecnicos.MasterTemplate.AllowAddNewRow = false;
		this.rgvTecnicos.MasterTemplate.AllowColumnReorder = false;
		this.rgvTecnicos.MasterTemplate.AllowDeleteRow = false;
		this.rgvTecnicos.MasterTemplate.AllowDragToGroup = false;
		this.rgvTecnicos.MasterTemplate.AllowEditRow = false;
		this.rgvTecnicos.MasterTemplate.AutoGenerateColumns = false;
		this.rgvTecnicos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "idTecnico";
		gridViewTextBoxColumn1.HeaderText = "ID";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colId";
		gridViewTextBoxColumn1.Width = 49;
		gridViewTextBoxColumn2.FieldName = "codigoTecnico";
		gridViewTextBoxColumn2.HeaderText = "CODIGO";
		gridViewTextBoxColumn2.Name = "colCodTecnico";
		gridViewTextBoxColumn2.Width = 82;
		gridViewTextBoxColumn3.FieldName = "dni";
		gridViewTextBoxColumn3.HeaderText = "DNI";
		gridViewTextBoxColumn3.Name = "colDni";
		gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn3.Width = 86;
		gridViewTextBoxColumn4.FieldName = "nombreCompleto";
		gridViewTextBoxColumn4.HeaderText = "NOMBRE COMPLETO";
		gridViewTextBoxColumn4.Name = "colNombre";
		gridViewTextBoxColumn4.Width = 248;
		gridViewTextBoxColumn5.FieldName = "fechaNacimiento";
		gridViewTextBoxColumn5.HeaderText = "FECHA DE NACIMIENTO";
		gridViewTextBoxColumn5.Name = "colFechaNacimiento";
		gridViewTextBoxColumn5.Width = 141;
		gridViewTextBoxColumn6.FieldName = "celular";
		gridViewTextBoxColumn6.HeaderText = "CELULAR";
		gridViewTextBoxColumn6.Name = "colCelular";
		gridViewTextBoxColumn6.Width = 95;
		gridViewTextBoxColumn7.FieldName = "direccion";
		gridViewTextBoxColumn7.HeaderText = "DIRECCION";
		gridViewTextBoxColumn7.Name = "colDireccion";
		gridViewTextBoxColumn7.Width = 212;
		gridViewTextBoxColumn8.FieldName = "oficiosSeleccionados";
		gridViewTextBoxColumn8.HeaderText = "OFICIOS";
		gridViewTextBoxColumn8.Name = "colOficiosSeleccionados";
		gridViewTextBoxColumn8.Width = 265;
		gridViewTextBoxColumn9.FieldName = "fechaRegistro";
		gridViewTextBoxColumn9.HeaderText = "FECHA REGISTRO";
		gridViewTextBoxColumn9.Name = "colFechaRegistro";
		gridViewTextBoxColumn9.Width = 111;
		gridViewTextBoxColumn10.FieldName = "horaRegistro";
		gridViewTextBoxColumn10.HeaderText = "HORA REGISTRO";
		gridViewTextBoxColumn10.Name = "colHoraRegistro";
		gridViewTextBoxColumn10.Width = 105;
		this.rgvTecnicos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10);
		this.rgvTecnicos.MasterTemplate.EnableFiltering = true;
		this.rgvTecnicos.MasterTemplate.EnableGrouping = false;
		this.rgvTecnicos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvTecnicos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvTecnicos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvTecnicos.Name = "rgvTecnicos";
		this.rgvTecnicos.ReadOnly = true;
		this.rgvTecnicos.ShowHeaderCellButtons = true;
		this.rgvTecnicos.Size = new System.Drawing.Size(1338, 357);
		this.rgvTecnicos.TabIndex = 0;
		this.rgvTecnicos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvTecnicos_CellClick);
		this.rgvTecnicos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvTecnicos_CellDoubleClick);
		this.btnAdministrarOficio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAdministrarOficio.BackColor = System.Drawing.Color.White;
		this.btnAdministrarOficio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAdministrarOficio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAdministrarOficio.ForeColor = System.Drawing.SystemColors.ControlText;
		this.btnAdministrarOficio.Location = new System.Drawing.Point(1181, 12);
		this.btnAdministrarOficio.Name = "btnAdministrarOficio";
		this.btnAdministrarOficio.Size = new System.Drawing.Size(151, 30);
		this.btnAdministrarOficio.TabIndex = 8;
		this.btnAdministrarOficio.Text = "Administrar Oficios";
		this.btnAdministrarOficio.UseVisualStyleBackColor = false;
		this.btnAdministrarOficio.Click += new System.EventHandler(btnAdministrarOficio_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1344, 436);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListadoTecnico";
		this.Text = "Listado de Tecnicos";
		base.Load += new System.EventHandler(frmListadoTecnico_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvTecnicos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTecnicos).EndInit();
		base.ResumeLayout(false);
	}
}
