using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmProveedoresLista : Office2007Form
{
	private clsAdmProveedor AdmPro = new clsAdmProveedor();

	private clsProveedor pro = new clsProveedor();

	public int Proceso = 0;

	public int Procede = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int codProv;

	internal frmGuiaRemisionCompra ventana = null;

	internal frmOrdenCompra ventana1 = null;

	internal frmPropuestaDeOrdenCompra ventana2 = null;

	internal frmNotadeCreditoCompra ventana3 = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvProveedor;

	private ImageList imageList1;

	private Button btnAceptar;

	private Button btnSalir;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn razonsocial;

	private Button btnNuevo;

	public frmProveedoresLista()
	{
		InitializeComponent();
	}

	public int GetCodProveeder()
	{
		return Convert.ToInt32(dgvProveedor.CurrentRow.Cells[codigo.Name].Value);
	}

	private void CargaLista()
	{
		dgvProveedor.DataSource = data;
		data.DataSource = AdmPro.RelacionProveedores();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvProveedor.ClearSelection();
		dgvProveedor.Focus();
	}

	private void DepurarLista()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvProveedor.Rows)
		{
			if (Convert.ToInt32(row.Cells[codigo.Name].Value) == codProv)
			{
				dgvProveedor.Rows.Remove(row);
			}
		}
	}

	private void CargaLista2()
	{
		dgvProveedor.DataSource = data;
		data.DataSource = AdmPro.RelacionProveedores();
		DepurarLista();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvProveedor.ClearSelection();
		dgvProveedor.Focus();
	}

	private void frmProveedoresLista_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Razon Social";
		label3.Text = "razonsocial";
		if (Procede == 9)
		{
			CargaLista2();
		}
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
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			if (ee.KeyChar != '(')
			{
				dgvProveedor.ClearSelection();
			}
		}
		catch (Exception)
		{
		}
	}

	private void dgvProveedor_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvProveedor.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvProveedor.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Procede == 1)
		{
			frmNotaIngresoPorOrden form = (frmNotaIngresoPorOrden)Application.OpenForms["frmNotaIngresoPorOrden"];
			form.CodProveedor = pro.CodProveedor;
			form.txtCodProv.Text = pro.Ruc;
			form.txtNombreProv.Text = pro.RazonSocial;
			form.txtCodProveedor.Text = pro.CodProveedor.ToString();
			Close();
		}
		else if (Procede == 2)
		{
			frmGestionLetra form2 = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
			form2.CodProveedor = pro.CodProveedor;
			Close();
		}
		else if (Procede == 3)
		{
			frmOrdenCompra form3 = (frmOrdenCompra)Application.OpenForms["frmOrdenCompra"];
			form3.CodProveedor = pro.CodProveedor;
			form3.txtCodProv.Text = pro.Ruc;
			form3.txtNombreProv.Text = pro.RazonSocial;
			base.DialogResult = DialogResult.OK;
			Close();
		}
		if (Procede == 4)
		{
			frmNotaIngreso form4 = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
			form4.CodProveedor = pro.CodProveedor;
			form4.txtCodProv.Text = pro.Ruc;
			form4.txtNombreProv.Text = pro.RazonSocial;
			Close();
		}
		if (Procede == 5)
		{
			frmListaPreciosProductos form5 = (frmListaPreciosProductos)Application.OpenForms["frmListaPreciosProductos"];
			if (pro.RazonSocial == null)
			{
				form5.txtProveedorNomb.Focus();
			}
			else
			{
				form5.txtProveedorCod.Text = pro.CodProveedor.ToString();
				form5.txtProveedorNomb.Text = pro.RazonSocial;
			}
			Close();
		}
		else if (Procede == 6)
		{
			frmNotadeCreditoCompra form6 = new frmNotadeCreditoCompra();
			form6 = ((ventana3 == null) ? ((frmNotadeCreditoCompra)Application.OpenForms["frmNotadeCreditoCompra"]) : ventana3);
			form6.CodProveedor = pro.CodProveedor;
			form6.txtCodProveedor.Text = pro.Ruc;
			form6.txtNombreProveedor.Text = pro.RazonSocial;
			Close();
		}
		else if (Procede == 7)
		{
			frmNotadeDebitoCompra form7 = (frmNotadeDebitoCompra)Application.OpenForms["frmNotadeDebitoCompra"];
			form7.CodProveedor = pro.CodProveedor;
			form7.txtCodProveedor.Text = pro.Ruc;
			form7.txtNombreProveedor.Text = pro.RazonSocial;
			Close();
		}
		else if (Procede == 8)
		{
			frmNotaSalida form8 = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
			form8.CodProveedor = pro.CodProveedor;
			form8.txtCodCliente.Text = pro.Ruc;
			form8.txtNombreCliente.Text = pro.RazonSocial;
			form8.btnDetalle.Enabled = true;
			Close();
		}
		else if (Procede == 9)
		{
			frmCambioProveedor form9 = (frmCambioProveedor)Application.OpenForms["frmCambioProveedor"];
			form9.CodProv = pro.CodProveedor;
			form9.txtCodProv2.Text = form9.CodProv.ToString();
			Close();
		}
		else if (Procede == 10)
		{
			frmPropuestaDeOrdenCompra form10 = new frmPropuestaDeOrdenCompra();
			form10 = ((ventana2 != null) ? ventana2 : ((frmPropuestaDeOrdenCompra)Application.OpenForms["frmPropuestaDeOrdenCompra"]));
			form10.CodProv = pro.CodProveedor;
			form10.txtProveedor.Text = pro.RazonSocial;
			form10.txtProveedor.Enabled = false;
			Close();
		}
		else if (Procede == 11)
		{
			frmGuiaRemisionCompra form11 = (frmGuiaRemisionCompra)Application.OpenForms["frmGuiaRemisionCompra"];
			form11.CodProv = pro.CodProveedor;
			form11.txtRazonSocialProveedor.Text = pro.RazonSocial;
			form11.txtRazonSocialProveedor.Enabled = false;
			form11.txtDireccionProveedor.Text = pro.Direccion;
			Close();
		}
		else if (Procede == 12)
		{
			ventana.CodEmpresaTransporte = pro.CodProveedor;
			base.DialogResult = DialogResult.OK;
			Close();
		}
		else if (Procede == 13)
		{
			ventana1.CodEmpresaTransporte = pro.CodProveedor;
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void dgvProveedor_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (dgvProveedor.SelectedRows.Count <= 0)
		{
			return;
		}
		if (Procede == 1)
		{
			frmNotaIngresoPorOrden form = (frmNotaIngresoPorOrden)Application.OpenForms["frmNotaIngresoPorOrden"];
			form.CodProveedor = pro.CodProveedor;
			form.txtCodProv.Text = pro.Ruc;
			form.txtNombreProv.Text = pro.RazonSocial;
			form.txtCodProveedor.Text = pro.CodProveedor.ToString();
			Close();
		}
		else if (Procede == 2)
		{
			frmGestionLetra form2 = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
			form2.CodProveedor = pro.CodProveedor;
			Close();
		}
		else if (Procede == 3)
		{
			frmOrdenCompra form3 = (frmOrdenCompra)Application.OpenForms["frmOrdenCompra"];
			form3.CodProveedor = pro.CodProveedor;
			form3.txtCodProv.Text = pro.Ruc;
			form3.txtNombreProv.Text = pro.RazonSocial;
			Close();
		}
		if (Procede == 4)
		{
			frmNotaIngreso form4 = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
			form4.CodProveedor = pro.CodProveedor;
			form4.txtCodProv.Text = pro.Ruc;
			form4.txtNombreProv.Text = pro.RazonSocial;
			Close();
		}
		if (Procede == 5)
		{
			frmListaPreciosProductos form5 = (frmListaPreciosProductos)Application.OpenForms["frmListaPreciosProductos"];
			if (pro.RazonSocial == null)
			{
				form5.txtProveedorNomb.Focus();
			}
			else
			{
				form5.txtProveedorCod.Text = pro.CodProveedor.ToString();
				form5.txtProveedorNomb.Text = pro.RazonSocial;
			}
			Close();
		}
		else if (Procede == 6)
		{
			frmNotadeCreditoCompra form6 = new frmNotadeCreditoCompra();
			form6 = ((ventana3 == null) ? ((frmNotadeCreditoCompra)Application.OpenForms["frmNotadeCreditoCompra"]) : ventana3);
			form6.CodProveedor = pro.CodProveedor;
			form6.txtCodProveedor.Text = pro.Ruc;
			form6.txtNombreProveedor.Text = pro.RazonSocial;
			Close();
		}
		else if (Procede == 7)
		{
			frmNotadeDebitoCompra form7 = (frmNotadeDebitoCompra)Application.OpenForms["frmNotadeDebitoCompra"];
			form7.CodProveedor = pro.CodProveedor;
			form7.txtCodProveedor.Text = pro.Ruc;
			form7.txtNombreProveedor.Text = pro.RazonSocial;
			Close();
		}
		else if (Procede == 8)
		{
			frmNotaSalida form8 = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
			form8.CodProveedor = pro.CodProveedor;
			form8.txtCodCliente.Text = pro.Ruc;
			form8.txtNombreCliente.Text = pro.RazonSocial;
			form8.btnDetalle.Enabled = true;
			Close();
		}
		else if (Procede == 9)
		{
			frmCambioProveedor form9 = (frmCambioProveedor)Application.OpenForms["frmCambioProveedor"];
			form9.CodProv = pro.CodProveedor;
			form9.txtCodProv2.Text = form9.CodProv.ToString();
			Close();
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		frmGestionProveedor frm = new frmGestionProveedor();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargaLista();
	}

	private void dgvProveedor_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvProveedor.Rows.Count >= 1 && e.RowIndex != -1 && dgvProveedor.CurrentRow.Index == e.RowIndex)
			{
				DataGridViewRow Row = dgvProveedor.Rows[e.RowIndex];
				pro.CodProveedor = Convert.ToInt32(Row.Cells[codigo.Name].Value);
				pro.Ruc = Row.Cells[ruc.Name].Value.ToString();
				pro.RazonSocial = Row.Cells[razonsocial.Name].Value.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		if (Proceso == 3)
		{
			btnAceptar.Enabled = true;
		}
	}

	private void dgvProveedor_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void dgvProveedor_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			try
			{
				if (dgvProveedor.Rows.Count > 0)
				{
					int f = dgvProveedor.CurrentRow.Index;
					pro.CodProveedor = Convert.ToInt32(dgvProveedor.Rows[f].Cells[codigo.Name].Value);
					pro.Ruc = dgvProveedor.Rows[f].Cells[ruc.Name].Value.ToString();
					pro.RazonSocial = dgvProveedor.Rows[f].Cells[razonsocial.Name].Value.ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			if (Proceso == 3)
			{
				btnAceptar.Enabled = true;
			}
		}
		if (e.KeyCode == Keys.Return && dgvProveedor.SelectedRows.Count > 0)
		{
			if (Procede == 1)
			{
				frmNotaIngresoPorOrden form = (frmNotaIngresoPorOrden)Application.OpenForms["frmNotaIngresoPorOrden"];
				form.CodProveedor = pro.CodProveedor;
				form.txtCodProv.Text = pro.Ruc;
				form.txtNombreProv.Text = pro.RazonSocial;
				Close();
			}
			else if (Procede == 2)
			{
				frmGestionLetra form2 = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
				form2.CodProveedor = pro.CodProveedor;
				Close();
			}
			else if (Procede == 3)
			{
				frmOrdenCompra form3 = (frmOrdenCompra)Application.OpenForms["frmOrdenCompra"];
				form3.CodProveedor = pro.CodProveedor;
				form3.txtCodProv.Text = pro.Ruc;
				form3.txtNombreProv.Text = pro.RazonSocial;
				Close();
			}
			if (Procede == 4)
			{
				frmNotaIngreso form4 = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
				form4.CodProveedor = pro.CodProveedor;
				form4.txtCodProv.Text = pro.Ruc;
				form4.txtNombreProv.Text = pro.RazonSocial;
				Close();
			}
		}
	}

	private void frmProveedoresLista_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			dgvProveedor.Focus();
		}
		dgvProveedor.ClearSelection();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProveedoresLista));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvProveedor = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProveedor).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvProveedor);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(393, 393);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Seleccionar Proveedor";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(369, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(77, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(16, 15);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(172, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(215, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.dgvProveedor.AllowUserToAddRows = false;
		this.dgvProveedor.AllowUserToDeleteRows = false;
		this.dgvProveedor.AllowUserToResizeColumns = false;
		this.dgvProveedor.AllowUserToResizeRows = false;
		this.dgvProveedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProveedor.Columns.AddRange(this.codigo, this.ruc, this.razonsocial);
		this.dgvProveedor.Location = new System.Drawing.Point(6, 45);
		this.dgvProveedor.Name = "dgvProveedor";
		this.dgvProveedor.ReadOnly = true;
		this.dgvProveedor.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvProveedor.RowHeadersVisible = false;
		this.dgvProveedor.RowHeadersWidth = 40;
		this.dgvProveedor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvProveedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProveedor.Size = new System.Drawing.Size(381, 342);
		this.dgvProveedor.TabIndex = 2;
		this.dgvProveedor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProveedor_CellDoubleClick);
		this.dgvProveedor.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvProveedor_ColumnHeaderMouseClick);
		this.dgvProveedor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProveedor_CellClick);
		this.dgvProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvProveedor_KeyDown);
		this.dgvProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvProveedor_KeyPress);
		this.dgvProveedor.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvProveedor_RowStateChanged);
		this.codigo.DataPropertyName = "codProveedor";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "RUC";
		this.ruc.Name = "ruc";
		this.ruc.ReadOnly = true;
		this.ruc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ruc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.HeaderText = "Razon Social";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.ReadOnly = true;
		this.razonsocial.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.razonsocial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.razonsocial.Width = 400;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.btnAceptar.Enabled = false;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(230, 399);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 14;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(313, 399);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 13;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 399);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 19;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(393, 440);
		base.Controls.Add(this.btnNuevo);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmProveedoresLista";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Proveedores";
		base.Load += new System.EventHandler(frmProveedoresLista_Load);
		base.Shown += new System.EventHandler(frmProveedoresLista_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProveedor).EndInit();
		base.ResumeLayout(false);
	}
}
