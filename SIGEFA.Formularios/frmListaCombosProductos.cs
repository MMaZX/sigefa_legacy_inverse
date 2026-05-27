using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListaCombosProductos : Form
{
	private clsAdmProducto AdmPro = new clsAdmProducto();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsProducto pro = new clsProducto();

	internal clsUsuario usuario_click = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadGridView dgvprods;

	private Button btnnuevo;

	private Button btneliminar;

	private Button btnactualizar;

	private Button btnEditar;

	private Button btnSalir;

	private MaterialTheme materialTheme1;

	private CheckBox chckinactivos;

	public frmListaCombosProductos()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnnuevo_Click(object sender, EventArgs e)
	{
		try
		{
			frmCombosProductos frm = new frmCombosProductos();
			frm.ShowDialog();
			CargaLista();
		}
		catch (Exception)
		{
		}
	}

	private void CargaLista()
	{
		try
		{
			dgvprods.DataSource = data;
			data.DataSource = AdmPro.CatalogoCombosProductos(!chckinactivos.Checked, 1);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvprods.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void frmListaCombosProductos_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (dgvprods.CurrentRow != null && dgvprods.CurrentRow.Index != -1)
		{
			frmCombosProductos frm = new frmCombosProductos();
			frm.Proceso = 2;
			frm.codcombo = pro.CodProducto;
			frm.ShowDialog();
			CargaLista();
		}
	}

	private void dgvprods_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (dgvprods.Rows.Count > 0)
			{
				pro.CodProducto = Convert.ToInt32(e.Row.Cells["codcombo"].Value);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnactualizar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btneliminar_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvprods.CurrentRow == null || dgvprods.CurrentRow.Index == -1)
			{
				return;
			}
			if (frmLogin.iNivelUser == 1)
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Inhabilitar combo", "Combos Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No)
				{
					if (AdmPro.deletecombo(pro.CodProducto))
					{
						MessageBox.Show("El combo se inabilito", "Combos Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						CargaLista();
					}
					else
					{
						MessageBox.Show("ubo un error al Inhabilitar Combo", "Combos Productos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				return;
			}
			DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea Inhabilitar combo", "Combos Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult2 == DialogResult.No)
			{
				return;
			}
			usuario_click = null;
			frmAutorizacion frm = new frmAutorizacion();
			frm.tipoAccion = 2;
			DialogResult dr = DialogResult.None;
			dr = frm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				if (AdmPro.deletecombo(pro.CodProducto))
				{
					MessageBox.Show("El combo se inabilito", "Combos Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaLista();
				}
				else
				{
					MessageBox.Show("Hubo un error al Inhabilitar Combo", "Combos Productos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void chckinactivos_CheckedChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvprods_RowFormatting(object sender, RowFormattingEventArgs e)
	{
		if (e.RowElement.RowInfo.Cells["nombreestado"].Value.ToString() == "Inactivo")
		{
			e.RowElement.BackColor = Color.Red;
			e.RowElement.ForeColor = Color.White;
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaCombosProductos));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvprods = new Telerik.WinControls.UI.RadGridView();
		this.btnnuevo = new System.Windows.Forms.Button();
		this.btneliminar = new System.Windows.Forms.Button();
		this.btnactualizar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.chckinactivos = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvprods).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvprods.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvprods);
		this.groupBox1.Location = new System.Drawing.Point(12, 67);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1415, 336);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "groupBox1";
		this.dgvprods.AutoScroll = true;
		this.dgvprods.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.dgvprods.Cursor = System.Windows.Forms.Cursors.Default;
		this.dgvprods.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvprods.EnableCustomDrawing = true;
		this.dgvprods.EnableHotTracking = false;
		this.dgvprods.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
		this.dgvprods.ForeColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.dgvprods.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.dgvprods.Location = new System.Drawing.Point(3, 16);
		this.dgvprods.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
		this.dgvprods.MasterTemplate.AllowAddNewRow = false;
		this.dgvprods.MasterTemplate.AllowEditRow = false;
		this.dgvprods.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "numero_item";
		gridViewTextBoxColumn1.HeaderText = "#";
		gridViewTextBoxColumn1.Name = "numero_item";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 96;
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.FieldName = "codcombo";
		gridViewTextBoxColumn2.HeaderText = "Referencia";
		gridViewTextBoxColumn2.Name = "codcombo";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 116;
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "Nombrecombo";
		gridViewTextBoxColumn3.HeaderText = "Descripcion";
		gridViewTextBoxColumn3.Multiline = true;
		gridViewTextBoxColumn3.Name = "Nombrecombo";
		gridViewTextBoxColumn3.Width = 413;
		gridViewTextBoxColumn3.WrapText = true;
		gridViewTextBoxColumn4.EnableExpressionEditor = false;
		gridViewTextBoxColumn4.FieldName = "total";
		gridViewTextBoxColumn4.HeaderText = "Total";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "total";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 123;
		gridViewTextBoxColumn5.EnableExpressionEditor = false;
		gridViewTextBoxColumn5.FieldName = "fechavencimiento";
		gridViewTextBoxColumn5.HeaderText = "Fecha Vecimiento";
		gridViewTextBoxColumn5.Name = "fechavencimiento";
		gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn5.Width = 187;
		gridViewTextBoxColumn6.EnableExpressionEditor = false;
		gridViewTextBoxColumn6.FieldName = "FechaRegistro";
		gridViewTextBoxColumn6.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn6.Name = "FechaRegistro";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 182;
		gridViewTextBoxColumn7.EnableExpressionEditor = false;
		gridViewTextBoxColumn7.FieldName = "usuario";
		gridViewTextBoxColumn7.HeaderText = "Usuario";
		gridViewTextBoxColumn7.Name = "usuario";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 233;
		gridViewTextBoxColumn8.EnableExpressionEditor = false;
		gridViewTextBoxColumn8.FieldName = "nombreestado";
		gridViewTextBoxColumn8.HeaderText = "Estado";
		gridViewTextBoxColumn8.Name = "nombreestado";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 182;
		this.dgvprods.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8);
		this.dgvprods.MasterTemplate.EnableFiltering = true;
		this.dgvprods.MasterTemplate.EnableGrouping = false;
		this.dgvprods.MasterTemplate.EnablePaging = true;
		this.dgvprods.MasterTemplate.PageSize = 50;
		this.dgvprods.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.dgvprods.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvprods.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvprods.Name = "dgvprods";
		this.dgvprods.ReadOnly = true;
		this.dgvprods.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.dgvprods.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 16, 240, 150);
		this.dgvprods.RootElement.FocusBorderColor = System.Drawing.Color.Yellow;
		this.dgvprods.ShowGroupPanel = false;
		this.dgvprods.Size = new System.Drawing.Size(1409, 317);
		this.dgvprods.TabIndex = 24;
		this.dgvprods.ThemeName = "Material";
		this.dgvprods.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(dgvprods_RowFormatting);
		this.dgvprods.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvprods_CellClick);
		this.btnnuevo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnnuevo.Image = SIGEFA.Properties.Resources.save;
		this.btnnuevo.Location = new System.Drawing.Point(892, 24);
		this.btnnuevo.Name = "btnnuevo";
		this.btnnuevo.Size = new System.Drawing.Size(96, 37);
		this.btnnuevo.TabIndex = 78;
		this.btnnuevo.Tag = "173";
		this.btnnuevo.Text = "Nuevo";
		this.btnnuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnnuevo.UseVisualStyleBackColor = true;
		this.btnnuevo.Click += new System.EventHandler(btnnuevo_Click);
		this.btneliminar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btneliminar.Image = (System.Drawing.Image)resources.GetObject("btneliminar.Image");
		this.btneliminar.Location = new System.Drawing.Point(994, 23);
		this.btneliminar.Name = "btneliminar";
		this.btneliminar.Size = new System.Drawing.Size(103, 37);
		this.btneliminar.TabIndex = 77;
		this.btneliminar.Tag = "174";
		this.btneliminar.Text = "Inhabilitar";
		this.btneliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btneliminar.UseVisualStyleBackColor = true;
		this.btneliminar.Click += new System.EventHandler(btneliminar_Click);
		this.btnactualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnactualizar.Image = (System.Drawing.Image)resources.GetObject("btnactualizar.Image");
		this.btnactualizar.Location = new System.Drawing.Point(1103, 24);
		this.btnactualizar.Name = "btnactualizar";
		this.btnactualizar.Size = new System.Drawing.Size(103, 37);
		this.btnactualizar.TabIndex = 76;
		this.btnactualizar.Text = "Actualizar";
		this.btnactualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnactualizar.UseVisualStyleBackColor = true;
		this.btnactualizar.Click += new System.EventHandler(btnactualizar_Click);
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnEditar.Image = SIGEFA.Properties.Resources.edit;
		this.btnEditar.Location = new System.Drawing.Point(1212, 24);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(103, 37);
		this.btnEditar.TabIndex = 75;
		this.btnEditar.Tag = "175";
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(1321, 24);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(103, 37);
		this.btnSalir.TabIndex = 74;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.chckinactivos.AutoSize = true;
		this.chckinactivos.Location = new System.Drawing.Point(779, 34);
		this.chckinactivos.Name = "chckinactivos";
		this.chckinactivos.Size = new System.Drawing.Size(107, 17);
		this.chckinactivos.TabIndex = 79;
		this.chckinactivos.Text = "Mostrar Inactivos";
		this.chckinactivos.UseVisualStyleBackColor = true;
		this.chckinactivos.CheckedChanged += new System.EventHandler(chckinactivos_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1440, 415);
		base.Controls.Add(this.chckinactivos);
		base.Controls.Add(this.btnnuevo);
		base.Controls.Add(this.btneliminar);
		base.Controls.Add(this.btnactualizar);
		base.Controls.Add(this.btnEditar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListaCombosProductos";
		this.Text = "frmListaCombosProductos";
		base.Load += new System.EventHandler(frmListaCombosProductos_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvprods.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvprods).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
