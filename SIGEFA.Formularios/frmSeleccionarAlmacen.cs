using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmSeleccionarAlmacen : Office2007Form
{
	private clsAdmAlmacen admalm = new clsAdmAlmacen();

	private clsAdmSucursal AdmSuc = new clsAdmSucursal();

	private clsSucursal SucurLogin = new clsSucursal();

	public int tipo = 1;

	private int almacenanterior;

	private IContainer components = null;

	private ImageList imageList1;

	private Button btnCancelar;

	private Button btnAceptar;

	public DataGridView dgvAlmacenes;

	private Panel panel1;

	private DataGridViewTextBoxColumn codalmacen;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn ubicacion;

	private Panel panel2;

	private Label label1;

	private ComboBox cmbSucursal;

	public frmSeleccionarAlmacen()
	{
		InitializeComponent();
	}

	private void frmSeleccionarAlmacen_Load(object sender, EventArgs e)
	{
		if (frmLogin.iCodAlmacen == 0)
		{
			if (dgvAlmacenes.Rows.Count > 0)
			{
				btnCancelar.Enabled = false;
				base.ControlBox = false;
			}
			else
			{
				btnCancelar.Enabled = true;
			}
		}
		if (frmLogin.iNivelUser == 1)
		{
			CargaSucursales();
			CargaAlmacenes();
		}
		else if (tipo == 2)
		{
			CargaSucursales();
			CargaAlmacenes();
		}
		else
		{
			CargaSucursalesXusuario();
			CargaAlmacenes();
		}
		almacenanterior = frmLogin.iCodAlmacen;
	}

	private void CargaSucursales()
	{
		cmbSucursal.DataSource = AdmSuc.CargaSucursales(frmLogin.iCodEmpresa);
		cmbSucursal.DisplayMember = "nombre";
		cmbSucursal.ValueMember = "codSucursal";
	}

	private void CargaSucursalesXusuario()
	{
		cmbSucursal.DataSource = AdmSuc.CargaSucursalesXusuario(frmLogin.iCodEmpresa, frmLogin.iCodUser);
		cmbSucursal.DisplayMember = "nombre";
		cmbSucursal.ValueMember = "codSucursal";
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaAlmacenes()
	{
		dgvAlmacenes.DataSource = admalm.MuestraAlmacenesDisponibles(Convert.ToInt32(cmbSucursal.SelectedValue));
	}

	private void dgvAlmacenes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvAlmacenes.Rows.Count >= 1)
		{
			btnAceptar.Enabled = true;
		}
	}

	private void frmSeleccionarAlmacen_Shown(object sender, EventArgs e)
	{
		CargaAlmacenes();
		dgvAlmacenes.Focus();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (tipo == 1)
		{
			if (dgvAlmacenes.CurrentRow.Index != -1 && almacenanterior != Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value))
			{
				frmLogin.iCodAlmacen = Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value);
				frmLogin.AlmacenLogin = admalm.CargaAlmacen(frmLogin.iCodAlmacen);
				frmLogin.sAlmacen = frmLogin.AlmacenLogin.Nombre;
				frmLogin.iCodSucursal = Convert.ToInt32(cmbSucursal.SelectedValue);
				mdi_Menu.Cambio = true;
			}
		}
		else if (tipo == 2 && dgvAlmacenes.CurrentRow.Index != -1)
		{
			frmRequerimientosVigentes form = (frmRequerimientosVigentes)Application.OpenForms["frmRequerimientosVigentes"];
			form.almadest = Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value);
		}
		Close();
	}

	private void dgvAlmacenes_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvAlmacenes.Rows.Count >= 1 && e.RowIndex != -1)
			{
				btnAceptar.Enabled = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dgvAlmacenes_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void dgvAlmacenes_SelectionChanged(object sender, EventArgs e)
	{
		if (dgvAlmacenes.SelectedRows.Count > 0 && dgvAlmacenes.CanFocus)
		{
			btnAceptar.Enabled = true;
		}
	}

	private void dgvAlmacenes_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (dgvAlmacenes.SelectedRows.Count != 0)
		{
			if (almacenanterior != Convert.ToInt32(dgvAlmacenes.SelectedRows[0].Cells[codalmacen.Name].Value))
			{
				frmLogin.iCodAlmacen = Convert.ToInt32(dgvAlmacenes.SelectedRows[0].Cells[codalmacen.Name].Value);
				frmLogin.AlmacenLogin = admalm.CargaAlmacen(frmLogin.iCodAlmacen);
				frmLogin.sAlmacen = frmLogin.AlmacenLogin.Nombre;
				mdi_Menu.Cambio = true;
			}
			Close();
		}
		else
		{
			MessageBox.Show("Seleccione uno de los Almacenes disponibles", "SIGEFA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dgvAlmacenes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (tipo == 1)
			{
				if (dgvAlmacenes.CurrentRow.Index != -1 && almacenanterior != Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value))
				{
					frmLogin.iCodAlmacen = Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value);
					frmLogin.AlmacenLogin = admalm.CargaAlmacen(frmLogin.iCodAlmacen);
					frmLogin.sAlmacen = frmLogin.AlmacenLogin.Nombre;
					frmLogin.iCodSucursal = Convert.ToInt32(cmbSucursal.SelectedValue);
					mdi_Menu.Cambio = true;
				}
			}
			else if (tipo == 2 && dgvAlmacenes.CurrentRow.Index != -1)
			{
				frmRequerimientosVigentes form = (frmRequerimientosVigentes)Application.OpenForms["frmRequerimientosVigentes"];
				form.almadest = Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value);
			}
			Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cmbSucursal_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaAlmacenes();
	}

	private void dgvAlmacenes_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (tipo == 1)
		{
			if (dgvAlmacenes.CurrentRow.Index != -1 && almacenanterior != Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value))
			{
				frmLogin.iCodAlmacen = Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value);
				frmLogin.AlmacenLogin = admalm.CargaAlmacen(frmLogin.iCodAlmacen);
				frmLogin.sAlmacen = frmLogin.AlmacenLogin.Nombre;
				frmLogin.iCodSucursal = Convert.ToInt32(cmbSucursal.SelectedValue);
				mdi_Menu.Cambio = true;
			}
		}
		else if (tipo == 2 && dgvAlmacenes.CurrentRow.Index != -1)
		{
			frmRequerimientosVigentes form = (frmRequerimientosVigentes)Application.OpenForms["frmRequerimientosVigentes"];
			form.almadest = Convert.ToInt32(dgvAlmacenes.CurrentRow.Cells[codalmacen.Name].Value);
		}
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmSeleccionarAlmacen));
		this.dgvAlmacenes = new System.Windows.Forms.DataGridView();
		this.codalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ubicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbSucursal = new System.Windows.Forms.ComboBox();
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.dgvAlmacenes.AllowUserToAddRows = false;
		this.dgvAlmacenes.AllowUserToDeleteRows = false;
		this.dgvAlmacenes.AllowUserToResizeColumns = false;
		this.dgvAlmacenes.AllowUserToResizeRows = false;
		this.dgvAlmacenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvAlmacenes.Columns.AddRange(this.codalmacen, this.nombre, this.ubicacion);
		this.dgvAlmacenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvAlmacenes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvAlmacenes.Location = new System.Drawing.Point(0, 35);
		this.dgvAlmacenes.MultiSelect = false;
		this.dgvAlmacenes.Name = "dgvAlmacenes";
		this.dgvAlmacenes.ReadOnly = true;
		this.dgvAlmacenes.RowHeadersVisible = false;
		this.dgvAlmacenes.RowHeadersWidth = 20;
		this.dgvAlmacenes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dgvAlmacenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvAlmacenes.Size = new System.Drawing.Size(534, 161);
		this.dgvAlmacenes.TabIndex = 0;
		this.dgvAlmacenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvAlmacenes_CellDoubleClick);
		this.dgvAlmacenes.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(dgvAlmacenes_PreviewKeyDown);
		this.dgvAlmacenes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvAlmacenes_CellClick);
		this.dgvAlmacenes.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvAlmacenes_KeyDown);
		this.dgvAlmacenes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvAlmacenes_KeyPress);
		this.dgvAlmacenes.SelectionChanged += new System.EventHandler(dgvAlmacenes_SelectionChanged);
		this.dgvAlmacenes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvAlmacenes_RowStateChanged);
		this.codalmacen.DataPropertyName = "codAlmacen";
		this.codalmacen.HeaderText = "Código";
		this.codalmacen.Name = "codalmacen";
		this.codalmacen.ReadOnly = true;
		this.codalmacen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codalmacen.Width = 50;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 180;
		this.ubicacion.DataPropertyName = "ubicacion";
		this.ubicacion.HeaderText = "Ubicación";
		this.ubicacion.Name = "ubicacion";
		this.ubicacion.ReadOnly = true;
		this.ubicacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ubicacion.Width = 300;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(452, 3);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 19;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.Enabled = false;
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(363, 3);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 18;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.panel1.Controls.Add(this.btnCancelar);
		this.panel1.Controls.Add(this.btnAceptar);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 196);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(534, 30);
		this.panel1.TabIndex = 20;
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.cmbSucursal);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(534, 35);
		this.panel2.TabIndex = 21;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(36, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(157, 16);
		this.label1.TabIndex = 22;
		this.label1.Text = "SELECCION DE SUCURSAL:";
		this.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSucursal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmbSucursal.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbSucursal.FormattingEnabled = true;
		this.cmbSucursal.Location = new System.Drawing.Point(196, 5);
		this.cmbSucursal.Name = "cmbSucursal";
		this.cmbSucursal.Size = new System.Drawing.Size(331, 24);
		this.cmbSucursal.TabIndex = 22;
		this.cmbSucursal.SelectionChangeCommitted += new System.EventHandler(cmbSucursal_SelectionChangeCommitted);
		base.AcceptButton = this.btnAceptar;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancelar;
		base.ClientSize = new System.Drawing.Size(534, 226);
		base.Controls.Add(this.dgvAlmacenes);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmSeleccionarAlmacen";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Seleccionar Almacen";
		base.Load += new System.EventHandler(frmSeleccionarAlmacen_Load);
		base.Shown += new System.EventHandler(frmSeleccionarAlmacen_Shown);
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		base.ResumeLayout(false);
	}
}
