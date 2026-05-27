using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoOficios : Form
{
	private clsAdmOficio admoficio = new clsAdmOficio();

	private clsOficio oficio = new clsOficio();

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadGridView rgvListadoOficios;

	private Button btnActualizar;

	private Button btnCrear;

	public frmListadoOficios()
	{
		InitializeComponent();
	}

	private void frmListadoOficios_Load(object sender, EventArgs e)
	{
		rgvListadoOficios.DataSource = admoficio.listaOficios();
	}

	private void btnCrear_Click(object sender, EventArgs e)
	{
		try
		{
			clsOficio nuevo = new clsOficio();
			frmOficio form = new frmOficio();
			DialogResult rpta = form.ShowDialog();
			if (rpta == DialogResult.Yes)
			{
				nuevo = form.oficio;
				if (admoficio.insert(nuevo))
				{
					rgvListadoOficios.DataSource = admoficio.listaOficios();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		if (oficio.Id == 0)
		{
			MessageBox.Show("Escoga un elemento de la lista para editarlo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		try
		{
			clsOficio nuevo = new clsOficio();
			frmOficio form = new frmOficio();
			form.proceso = 1;
			form.oficio = oficio;
			DialogResult rpta = form.ShowDialog();
			if (rpta == DialogResult.Yes)
			{
				nuevo = form.oficio;
				if (admoficio.update(nuevo))
				{
					rgvListadoOficios.DataSource = admoficio.listaOficios();
					oficio = new clsOficio();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void rgvListadoOficios_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codOficio = Convert.ToInt32(rgvListadoOficios.CurrentRow.Cells["colIdOficio"].Value);
			string oficio = rgvListadoOficios.CurrentRow.Cells["colDescripcion"].Value.ToString();
			this.oficio.Id = codOficio;
			this.oficio.Descripcion = oficio;
		}
	}

	private void rgvListadoOficios_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void rgvListadoOficios_KeyDown(object sender, KeyEventArgs e)
	{
		if (rgvListadoOficios.CurrentRow == null)
		{
			return;
		}
		try
		{
			if (e.KeyCode != Keys.Delete)
			{
				return;
			}
			int codOIficio = Convert.ToInt32(rgvListadoOficios.CurrentRow.Cells["colIdOficio"].Value);
			string oficio = rgvListadoOficios.CurrentRow.Cells["colDescripcion"].Value.ToString();
			DialogResult rpta = MessageBox.Show("Estas seguro de eliminar el oficio \"" + oficio + "\"?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rpta != DialogResult.Yes)
			{
				return;
			}
			if (admoficio.getSiOficioUtilizado(codOIficio))
			{
				MessageBox.Show("No se puede eliminar este oficio por que esta siendo utilizado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (!admoficio.elimina(codOIficio))
			{
				MessageBox.Show("Ocurrio un error al eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MessageBox.Show("Se elimino el oficio exitosamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			rgvListadoOficios.DataSource = admoficio.listaOficios();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvListadoOficios = new Telerik.WinControls.UI.RadGridView();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.btnCrear = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoOficios).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoOficios.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.rgvListadoOficios);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(464, 321);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.rgvListadoOficios.AutoScroll = true;
		this.rgvListadoOficios.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvListadoOficios.EnableGestures = false;
		this.rgvListadoOficios.Location = new System.Drawing.Point(3, 16);
		this.rgvListadoOficios.MasterTemplate.AllowAddNewRow = false;
		this.rgvListadoOficios.MasterTemplate.AllowColumnReorder = false;
		this.rgvListadoOficios.MasterTemplate.AllowColumnResize = false;
		this.rgvListadoOficios.MasterTemplate.AllowDeleteRow = false;
		this.rgvListadoOficios.MasterTemplate.AllowDragToGroup = false;
		this.rgvListadoOficios.MasterTemplate.AllowEditRow = false;
		this.rgvListadoOficios.MasterTemplate.AllowRowResize = false;
		this.rgvListadoOficios.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "idOficio";
		gridViewTextBoxColumn1.HeaderText = "idOficio";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colIdOficio";
		gridViewTextBoxColumn2.FieldName = "descripcion";
		gridViewTextBoxColumn2.HeaderText = "Oficio";
		gridViewTextBoxColumn2.Name = "colDescripcion";
		gridViewTextBoxColumn2.Width = 457;
		this.rgvListadoOficios.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2);
		this.rgvListadoOficios.MasterTemplate.EnableFiltering = true;
		this.rgvListadoOficios.MasterTemplate.EnableGrouping = false;
		this.rgvListadoOficios.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvListadoOficios.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvListadoOficios.Name = "rgvListadoOficios";
		this.rgvListadoOficios.Size = new System.Drawing.Size(458, 302);
		this.rgvListadoOficios.TabIndex = 0;
		this.rgvListadoOficios.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvListadoOficios_CellClick);
		this.rgvListadoOficios.KeyDown += new System.Windows.Forms.KeyEventHandler(rgvListadoOficios_KeyDown);
		this.rgvListadoOficios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(rgvListadoOficios_KeyPress);
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.BackColor = System.Drawing.Color.White;
		this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnActualizar.Location = new System.Drawing.Point(365, 328);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(87, 38);
		this.btnActualizar.TabIndex = 1;
		this.btnActualizar.Text = "Actualizar";
		this.btnActualizar.UseVisualStyleBackColor = false;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.btnCrear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCrear.BackColor = System.Drawing.Color.White;
		this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCrear.Location = new System.Drawing.Point(284, 328);
		this.btnCrear.Name = "btnCrear";
		this.btnCrear.Size = new System.Drawing.Size(75, 38);
		this.btnCrear.TabIndex = 1;
		this.btnCrear.Text = "Nuevo";
		this.btnCrear.UseVisualStyleBackColor = false;
		this.btnCrear.Click += new System.EventHandler(btnCrear_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(464, 378);
		base.Controls.Add(this.btnCrear);
		base.Controls.Add(this.btnActualizar);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListadoOficios";
		this.Text = "frmListadoOficios";
		base.Load += new System.EventHandler(frmListadoOficios_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvListadoOficios.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoOficios).EndInit();
		base.ResumeLayout(false);
	}
}
