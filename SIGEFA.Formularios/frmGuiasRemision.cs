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

public class frmGuiasRemision : Office2007Form
{
	private clsAdmGuiaRemision AdmGuia = new clsAdmGuiaRemision();

	private clsGuiaRemision guia = new clsGuiaRemision();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private int numeroGuia = 0;

	private int codmovi;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvGuiasRemision;

	private Button btnSalir;

	private Button btnIrGuia;

	private Button btGenVenta;

	private ImageList imageList1;

	private Button btnAnular;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn factura;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn facturado;

	private DataGridViewTextBoxColumn estadofacturado;

	private DataGridViewTextBoxColumn codMotivo;

	private DataGridViewTextBoxColumn numeroguia;

	private GroupBox groupBox2;

	private Label label6;

	private Label label5;

	private Label label7;

	private Label label10;

	private TextBox txtFiltro;

	private Label label2;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private Label label3;

	public frmGuiasRemision()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		dgvGuiasRemision.DataSource = data;
		data.DataSource = AdmGuia.MuestraGuiaRemisiones(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvGuiasRemision.ClearSelection();
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		if (dgvGuiasRemision.Rows.Count >= 1 && dgvGuiasRemision.CurrentRow != null)
		{
			DataGridViewRow row = dgvGuiasRemision.CurrentRow;
			frmGuiaRemision form = new frmGuiaRemision();
			form.MdiParent = base.MdiParent;
			form.CodGuia = guia.CodGuiaRemision;
			form.CodTransaccion = guia.CodMotivo;
			form.numeroGuia = numeroGuia;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void frmPedidosPendientes_Load(object sender, EventArgs e)
	{
		dtpFecha1.Value = dtpFecha2.Value.AddDays(-30.0);
		label7.Text = "Cliente";
		label6.Text = "cliente";
		CargaLista();
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (dgvGuiasRemision.Rows.Count >= 1 && dgvGuiasRemision.CurrentRow != null && !Convert.ToBoolean(dgvGuiasRemision.CurrentRow.Cells[facturado.Name].Value) && guia.CodGuiaRemision != "")
		{
			if (Application.OpenForms["frmVenta"] != null)
			{
				Application.OpenForms["frmVenta"].Close();
				return;
			}
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.Proceso = 1;
			form.CodGuia = codmovi;
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			form.Show();
			form.txtGuias_KeyPress(sender, ee);
			form.txtCodCliente.ReadOnly = true;
		}
	}

	private void dgvPedidosPendientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvGuiasRemision.Rows.Count >= 1 && e.Row.Selected)
		{
			guia.CodGuiaRemision = e.Row.Cells[codigo.Name].Value.ToString();
			guia.CodMotivo = Convert.ToInt32(e.Row.Cells[codMotivo.Name].Value);
			numeroGuia = Convert.ToInt32(e.Row.Cells[numeroguia.Name].Value);
		}
	}

	private void dgvPedidosPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvGuiasRemision.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmGuiaRemision form = new frmGuiaRemision();
			form.MdiParent = base.MdiParent;
			form.CodGuia = guia.CodGuiaRemision;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvGuiasRemision.CurrentRow != null && guia.CodGuiaRemision != "")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la guia seleccionada", "Guias Remision", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmGuia.delete(Convert.ToInt32(guia.CodGuiaRemision)))
			{
				MessageBox.Show("La guia ha sido anulada correctamente", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void dgvGuiasRemision_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex != -1)
			{
				codmovi = Convert.ToInt32(dgvGuiasRemision.Rows[e.RowIndex].Cells["codigo"].Value);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
	}

	public void CargaListaBusqueda()
	{
		dgvGuiasRemision.DataSource = data;
		data.DataSource = AdmGuia.MuestraGuiasBusqueda(dtpFecha1.Value, dtpFecha2.Value, txtFiltro.Text);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvGuiasRemision.ClearSelection();
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
								queries.Add($"[{label3.Text}] LIKE '%{c}%'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"[{label3.Text}] LIKE '%{c}%'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"[{label3.Text}] LIKE '%{filterCod}%'";
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

	private void dgvGuiasRemision_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
	}

	private void dgvGuiasRemision_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvGuiasRemision.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvGuiasRemision.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGuiasRemision));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvGuiasRemision = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.facturado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estadofacturado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codMotivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numeroguia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAnular = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrGuia = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvGuiasRemision).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvGuiasRemision);
		this.groupBox1.Location = new System.Drawing.Point(0, 65);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1204, 415);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Listado de guias";
		this.dgvGuiasRemision.AllowUserToAddRows = false;
		this.dgvGuiasRemision.AllowUserToDeleteRows = false;
		this.dgvGuiasRemision.AllowUserToOrderColumns = true;
		this.dgvGuiasRemision.AllowUserToResizeRows = false;
		this.dgvGuiasRemision.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvGuiasRemision.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvGuiasRemision.Columns.AddRange(this.codigo, this.numdoc, this.factura, this.cliente, this.fecha, this.fechaemision, this.responsable, this.facturado, this.estadofacturado, this.codMotivo, this.numeroguia);
		this.dgvGuiasRemision.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvGuiasRemision.Location = new System.Drawing.Point(3, 16);
		this.dgvGuiasRemision.MultiSelect = false;
		this.dgvGuiasRemision.Name = "dgvGuiasRemision";
		this.dgvGuiasRemision.ReadOnly = true;
		this.dgvGuiasRemision.RowHeadersVisible = false;
		this.dgvGuiasRemision.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvGuiasRemision.Size = new System.Drawing.Size(1198, 396);
		this.dgvGuiasRemision.TabIndex = 0;
		this.dgvGuiasRemision.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvGuiasRemision_CellClick);
		this.dgvGuiasRemision.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPedidosPendientes_CellDoubleClick);
		this.dgvGuiasRemision.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvGuiasRemision_ColumnHeaderMouseClick);
		this.dgvGuiasRemision.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvPedidosPendientes_RowStateChanged);
		this.codigo.DataPropertyName = "codGuiaRemision";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N° Doc.";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.factura.DataPropertyName = "factura";
		this.factura.HeaderText = "Boleta/Factura";
		this.factura.Name = "factura";
		this.factura.ReadOnly = true;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "F. Traslado";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fechaemision.DataPropertyName = "fechaemision";
		this.fechaemision.HeaderText = "F. Emision";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechaemision.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.facturado.DataPropertyName = "facturado";
		this.facturado.HeaderText = "facturado";
		this.facturado.Name = "facturado";
		this.facturado.ReadOnly = true;
		this.facturado.Visible = false;
		this.estadofacturado.DataPropertyName = "estadofacturado";
		this.estadofacturado.HeaderText = "Estado";
		this.estadofacturado.Name = "estadofacturado";
		this.estadofacturado.ReadOnly = true;
		this.codMotivo.DataPropertyName = "codMotivo";
		this.codMotivo.HeaderText = "codMotivo";
		this.codMotivo.Name = "codMotivo";
		this.codMotivo.ReadOnly = true;
		this.codMotivo.Visible = false;
		this.numeroguia.DataPropertyName = "numeroguia";
		this.numeroguia.HeaderText = "numeroguia";
		this.numeroguia.Name = "numeroguia";
		this.numeroguia.ReadOnly = true;
		this.numeroguia.Visible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(77, 486);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular Guia";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.ImageIndex = 2;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(916, 486);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(96, 37);
		this.btGenVenta.TabIndex = 3;
		this.btGenVenta.Text = "Generar Venta";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnIrGuia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrGuia.ImageIndex = 1;
		this.btnIrGuia.ImageList = this.imageList1;
		this.btnIrGuia.Location = new System.Drawing.Point(1018, 486);
		this.btnIrGuia.Name = "btnIrGuia";
		this.btnIrGuia.Size = new System.Drawing.Size(93, 37);
		this.btnIrGuia.TabIndex = 2;
		this.btnIrGuia.Text = "Consultar";
		this.btnIrGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrGuia.UseVisualStyleBackColor = true;
		this.btnIrGuia.Click += new System.EventHandler(btnIrPedido_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1117, 486);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.txtFiltro);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.dtpFecha2);
		this.groupBox2.Controls.Add(this.dtpFecha1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1204, 59);
		this.groupBox2.TabIndex = 8;
		this.groupBox2.TabStop = false;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(399, 34);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 12);
		this.label3.TabIndex = 38;
		this.label3.Text = "X";
		this.label3.Visible = false;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label6.Location = new System.Drawing.Point(1006, 8);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 37;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(222, 15);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 36;
		this.label5.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(257, 15);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 35;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(184, 15);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 34;
		this.label10.Text = "Filtro";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(186, 30);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 33;
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(96, 14);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 26;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(9, 14);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 25;
		this.label1.Text = "Desde";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(99, 30);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 22;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(12, 30);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 21;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1204, 535);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btGenVenta);
		base.Controls.Add(this.btnIrGuia);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGuiasRemision";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Guias Remision";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPedidosPendientes_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvGuiasRemision).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
