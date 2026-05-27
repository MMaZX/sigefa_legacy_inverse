using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmClientesLista : Office2007Form
{
	private clsAdmCliente AdmCli = new clsAdmCliente();

	public clsCliente cli = new clsCliente();

	public int Proceso = 0;

	public int Procede = 0;

	public int Tipo = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public bool exit = false;

	public string filtrocliente = "";

	internal frmGuiaRemisionCompra ventana = null;

	internal frmOrdenCompra ventana1 = null;

	private IContainer components = null;

	private ImageList imageList1;

	private Button btnAceptar;

	private Button btnSalir;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvCliente;

	private Button btnNuevo;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn codperso;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn razonsocial;

	private DataGridViewTextBoxColumn direccionlegal;

	public frmClientesLista()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		if (Proceso == 7)
		{
			dgvCliente.DataSource = data;
			data.DataSource = AdmCli.RelacionClientesFiltrada(filtrocliente);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvCliente.ClearSelection();
		}
		else
		{
			dgvCliente.DataSource = data;
			data.DataSource = AdmCli.RelacionClientes();
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvCliente.ClearSelection();
		}
	}

	private void frmClientesLista_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Razon Social";
		label3.Text = "Razonsocial";
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
	}

	private void dgvCliente_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvCliente.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvCliente.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Procede == 12)
		{
			ventana.CodEmpresaTransporte = cli.CodCliente;
			Close();
		}
		else if (Procede == 13)
		{
			ventana1.CodEmpresaTransporte = cli.CodCliente;
			Close();
		}
		Close();
	}

	private void dgvCliente_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		frmGestionCliente frm = new frmGestionCliente();
		frm.Proceso = 4;
		frm.ShowDialog();
		CargaLista();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (dgvCliente.SelectedRows.Count > 0)
		{
			Close();
		}
	}

	private void txtFiltro_TextChanged_1(object sender, EventArgs e)
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

	public int GetCodigoCliente()
	{
		return cli.CodCliente;
	}

	private void frmClientesLista_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (cli.CodCliente != 0)
		{
			base.DialogResult = DialogResult.OK;
		}
	}

	private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvCliente.Rows.Count >= 1 && e.RowIndex != -1 && dgvCliente.CurrentRow.Index == e.RowIndex)
			{
				DataGridViewRow Row = dgvCliente.Rows[e.RowIndex];
				cli.CodCliente = Convert.ToInt32(Row.Cells[codigo.Name].Value);
				cli.CodigoPersonalizado = Row.Cells[codperso.Name].Value.ToString();
				cli.Dni = Row.Cells[dni.Name].Value.ToString();
				cli.Ruc = Row.Cells[ruc.Name].Value.ToString();
				cli.RazonSocial = Row.Cells[razonsocial.Name].Value.ToString();
				cli.DireccionLegal = Row.Cells[direccionlegal.Name].Value.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			dgvCliente.Focus();
			dgvCliente.Rows[0].Selected = true;
		}
	}

	private void dgvCliente_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		try
		{
			if (dgvCliente.Rows.Count > 0)
			{
				int f = dgvCliente.CurrentRow.Index;
				cli.CodCliente = Convert.ToInt32(dgvCliente.Rows[f].Cells[codigo.Name].Value);
				cli.CodigoPersonalizado = dgvCliente.Rows[f].Cells[codperso.Name].Value.ToString();
				cli.Dni = dgvCliente.Rows[f].Cells[dni.Name].Value.ToString();
				cli.Ruc = dgvCliente.Rows[f].Cells[ruc.Name].Value.ToString();
				cli.RazonSocial = dgvCliente.Rows[f].Cells[razonsocial.Name].Value.ToString();
				cli.DireccionLegal = dgvCliente.Rows[f].Cells[direccionlegal.Name].Value.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		Close();
	}

	private void frmClientesLista_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
		dgvCliente.ClearSelection();
	}

	private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtFiltro.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltro.Text != "")
				{
					string filterCod = txtFiltro.Text;
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					if (cad.Count() > 1)
					{
						string[] array = cad;
						foreach (string c in array)
						{
							if (cont == 1)
							{
								queries.Add($"[{label3.Text.Trim()}] like '*{c}*'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"[{label3.Text.Trim()}] like '*{c}*'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"[{label3.Text.Trim()}] like '*{filterCod}*'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmClientesLista));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvCliente = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codperso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccionlegal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCliente).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(564, 324);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 17;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(647, 324);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 16;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvCliente);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(721, 318);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Seleccionar Cliente";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(393, 22);
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
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.dgvCliente.AllowUserToAddRows = false;
		this.dgvCliente.AllowUserToDeleteRows = false;
		this.dgvCliente.AllowUserToResizeColumns = false;
		this.dgvCliente.AllowUserToResizeRows = false;
		this.dgvCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCliente.Columns.AddRange(this.codigo, this.codperso, this.ruc, this.dni, this.razonsocial, this.direccionlegal);
		this.dgvCliente.Location = new System.Drawing.Point(6, 45);
		this.dgvCliente.MultiSelect = false;
		this.dgvCliente.Name = "dgvCliente";
		this.dgvCliente.ReadOnly = true;
		this.dgvCliente.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvCliente.RowHeadersVisible = false;
		this.dgvCliente.RowHeadersWidth = 40;
		this.dgvCliente.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvCliente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCliente.Size = new System.Drawing.Size(709, 267);
		this.dgvCliente.TabIndex = 2;
		this.dgvCliente.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCliente_CellClick);
		this.dgvCliente.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCliente_CellDoubleClick);
		this.dgvCliente.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvCliente_ColumnHeaderMouseClick);
		this.dgvCliente.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCliente_RowStateChanged);
		this.dgvCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvCliente_KeyDown);
		this.codigo.DataPropertyName = "codCliente";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.codperso.DataPropertyName = "codigopersonalizado";
		this.codperso.HeaderText = "Codigo P.";
		this.codperso.Name = "codperso";
		this.codperso.ReadOnly = true;
		this.codperso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "RUC";
		this.ruc.Name = "ruc";
		this.ruc.ReadOnly = true;
		this.ruc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ruc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dni.DataPropertyName = "dni";
		this.dni.HeaderText = "DNI";
		this.dni.Name = "dni";
		this.dni.ReadOnly = true;
		this.dni.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.HeaderText = "Razon Social";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.ReadOnly = true;
		this.razonsocial.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.razonsocial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.Width = 250;
		this.direccionlegal.DataPropertyName = "direccionlegal";
		this.direccionlegal.HeaderText = "Direccion";
		this.direccionlegal.Name = "direccionlegal";
		this.direccionlegal.ReadOnly = true;
		this.direccionlegal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.direccionlegal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccionlegal.Width = 250;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 324);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 18;
		this.btnNuevo.Text = "&Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		base.AcceptButton = this.btnAceptar;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(721, 362);
		base.Controls.Add(this.btnNuevo);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmClientesLista";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Clientes";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmClientesLista_FormClosing);
		base.Load += new System.EventHandler(frmClientesLista_Load);
		base.Shown += new System.EventHandler(frmClientesLista_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCliente).EndInit();
		base.ResumeLayout(false);
	}
}
