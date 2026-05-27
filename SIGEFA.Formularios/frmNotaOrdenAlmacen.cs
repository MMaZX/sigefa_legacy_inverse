using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmNotaOrdenAlmacen : Form
{
	private clsAdmOrdenCompra AdmOrden = new clsAdmOrdenCompra();

	private clsOrdenCompra Orden = new clsOrdenCompra();

	private clsAdmNotaIngreso admNotaIng = new clsAdmNotaIngreso();

	private clsNotaIngreso nota = new clsNotaIngreso();

	private clsDetalleNotaIngreso detaNota = new clsDetalleNotaIngreso();

	public clsProveedor deta = new clsProveedor();

	public clsCliente detaCli = new clsCliente();

	public clsNotaSalida salida = new clsNotaSalida();

	private clsAdmNotaSalida AdmSalida = new clsAdmNotaSalida();

	public DataTable notaingresoConsolidado = new DataTable();

	public List<int> coddetallenota = new List<int>();

	public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();

	public List<clsNotaIngreso> notaIn = new List<clsNotaIngreso>();

	public List<clsDetalleFacturaVenta> detalle1 = new List<clsDetalleFacturaVenta>();

	public List<int> coddetalleventa = new List<int>();

	public List<clsDetalleCotizacion> detalle2 = new List<clsDetalleCotizacion>();

	public List<int> coddetallecoti = new List<int>();

	public DataGridView dat = new DataGridView();

	public List<int> ltaCodListaPrecio = new List<int>();

	public List<int> ltacodnotasalida = new List<int>();

	public int OrdCom;

	public int CodSalida;

	public int CodProveedor;

	public int CodCli;

	public int CodDoc;

	public int tipomoneda;

	public int CodFac;

	public int codforma;

	public int codlista;

	public int CodOrdenCompra;

	public int codorden = 0;

	public int estadcheck;

	public int proceso = 0;

	public int procede = 0;

	public string unir = "";

	public string unir2 = "";

	public int Contador = 0;

	public double tipocambio;

	public string codigopersonalizado = "";

	public string nombreCliente = "";

	public string direccionCliente = "";

	public int codcli;

	private int estado = 0;

	public int tipo = 0;

	private int vOrigOC = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private Button btnAceptar;

	private Button btnSalir;

	private ImageList imageList1;

	private ImageList imageList2;

	private GroupBox groupBox2;

	public GroupBox groupBox1;

	private DataGridViewTextBoxColumn moneda;

	public TextBox textBox2;

	public TextBox textBox1;

	public TextBox textBox3;

	private TextBox textBox4;

	private Button btnconsultar;

	private ImageList imageList3;

	private DataGridView dgvDetalle;

	public DataGridView dgvDetalle2;

	private DataGridViewTextBoxColumn codnotasalida;

	private DataGridViewTextBoxColumn fechasalida;

	private DataGridViewTextBoxColumn tipodoc;

	private DataGridViewTextBoxColumn serie;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewCheckBoxColumn escoge;

	private DataGridViewTextBoxColumn codnoting;

	private DataGridViewTextBoxColumn codOrdenC;

	private DataGridViewTextBoxColumn codDocumento;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn razonSocial;

	private DataGridViewTextBoxColumn fecing;

	private DataGridViewTextBoxColumn fecreg;

	private DataGridViewTextBoxColumn codUsu;

	private DataGridViewTextBoxColumn docOrden;

	private DataGridViewTextBoxColumn almacen_1;

	private Label label2;

	private Label label1;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Button btnbuscar;

	private ImageList imageList4;

	private TextBox txtFiltro;

	private Label label3;

	private Label label7;

	private Label label10;

	private Label label4;

	public frmNotaOrdenAlmacen()
	{
		InitializeComponent();
	}

	private void NotaOrdenAlmacen_Load(object sender, EventArgs e)
	{
		btnbuscar_Click(sender, e);
	}

	private void CargaNotaAlmacen()
	{
		dgvDetalle2.DataSource = data;
		data.DataSource = AdmSalida.MuestraNotaAlmacen(frmLogin.iCodAlmacen, tipo);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalle2.ClearSelection();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void sololectura(bool estado)
	{
		btnAceptar.Visible = !estado;
	}

	public void Cargaconsolidado()
	{
		dgvDetalle.DataSource = data;
		data.DataSource = admNotaIng.MuestraNotaIngresoOrden(frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalle.ClearSelection();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (proceso == 7)
		{
			coddetallenota.Clear();
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				if (Convert.ToInt32(row.Cells[escoge.Name].Value) == 1 && Convert.ToInt32(row.Cells[codOrdenC.Name].Value) == Convert.ToInt32(textBox1.Text))
				{
					estado = 1;
				}
				else if (Convert.ToInt32(row.Cells[escoge.Name].Value) == 1 && Convert.ToInt32(row.Cells[codOrdenC.Name].Value) != Convert.ToInt32(textBox1.Text))
				{
					estado = 0;
					break;
				}
			}
			if (estado != 1)
			{
				MessageBox.Show("Escoja Ordenes Iguales");
				{
					foreach (DataGridViewRow row2 in (IEnumerable)dgvDetalle.Rows)
					{
						row2.Cells[escoge.Name].Value = 0;
					}
					return;
				}
			}
			foreach (DataGridViewRow row3 in (IEnumerable)dgvDetalle.Rows)
			{
				vOrigOC = Convert.ToInt32(row3.Cells[codOrdenC.Name].Value);
				if (Convert.ToInt32(row3.Cells[escoge.Name].Value) == 1 && Convert.ToInt32(row3.Cells[codOrdenC.Name].Value) == Convert.ToInt32(textBox1.Text))
				{
					admNotaIng.insertdetalleConsolidado(Convert.ToInt32(row3.Cells[codOrdenC.Name].Value), Convert.ToInt32(row3.Cells[codnoting.Name].Value), frmLogin.iCodAlmacen, frmLogin.iCodUser);
					detaNota.CodNotaIngreso = Convert.ToInt32(row3.Cells[codnoting.Name].Value.ToString());
					coddetallenota.Add(detaNota.CodNotaIngreso);
					unir = unir + row3.Cells[documento.Name].Value?.ToString() + ", ";
				}
			}
			frmNotaIngresoPorOrden form = (frmNotaIngresoPorOrden)Application.OpenForms["frmNotaIngresoPorOrden"];
			form.documento = coddetallenota;
			form.datoscarga2.Clear();
			string doc = "";
			foreach (int c in form.documento)
			{
				form.txtOrdenCompra.Text = unir;
				nota = admNotaIng.CargaNotaIngreso(c);
				form.cmbFormaPago.SelectedValue = nota.FormaPago;
				form.cmbFormaPago_SelectionChangeCommitted(form.cmbFormaPago, null);
				form.dtpFechaPago.Value = nota.FechaPago;
				form.cmbMoneda.SelectedValue = nota.Moneda;
				form.txtTipoCambio.Visible = true;
				form.label16.Visible = true;
				form.txtTipoCambio.Text = nota.TipoCambio.ToString();
				form.txtFlete.Text = nota.Flete.ToString();
				form.txtCodProv.Text = nota.RUCProveedor;
				form.txtNombreProv.Text = nota.RazonSocialProveedor;
				form.txtCodProveedor.Text = nota.CodProveedor.ToString();
				doc = doc + c + ",";
			}
			form.txtCodNota.Text = doc;
			form.vOrigOC = vOrigOC;
			form.llenardetalle2();
			Close();
		}
		else if (proceso == 11)
		{
			if (dgvDetalle2.Rows.Count > 0 && dgvDetalle2.SelectedRows != null)
			{
				salida.CodNotaSalida = dgvDetalle2.CurrentRow.Cells[codnotasalida.Name].Value.ToString();
			}
			Close();
		}
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (Convert.ToInt32(dgvDetalle.CurrentRow.Cells[escoge.Name].Value) == 1)
		{
			textBox1.Text = dgvDetalle.CurrentRow.Cells[codOrdenC.Name].Value.ToString();
		}
	}

	private void btnconsultar_Click(object sender, EventArgs e)
	{
		frmNotaIngreso form = new frmNotaIngreso();
		form.CodNota = nota.CodNotaIngreso;
		form.Proceso = 3;
		form.Tipo = 2;
		form.Show();
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
		{
			nota.CodNotaIngreso = Convert.ToString(e.Row.Cells[codnoting.Name].Value);
		}
	}

	private void dgvDetalle2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			salida.CodNotaSalida = dgvDetalle2.Rows[e.RowIndex].Cells[codnotasalida.Name].Value.ToString();
		}
		Close();
	}

	private void dgvDetalle2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		Close();
	}

	private void btnbuscar_Click(object sender, EventArgs e)
	{
		Cargaconsolidado();
		if (proceso == 7)
		{
			admNotaIng.deleteConsolidado(frmLogin.iCodAlmacen, frmLogin.iCodUser);
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					if (coddetallenota.Contains(Convert.ToInt32(row.Cells[codnoting.Name].Value)))
					{
						row.Cells[escoge.Name].Value = true;
						textBox1.Text = row.Cells[codOrdenC.Name].Value.ToString();
					}
				}
				return;
			}
		}
		if (proceso == 1)
		{
			Cargaconsolidado();
			btnAceptar.Visible = false;
			btnconsultar.Visible = true;
			escoge.Visible = false;
		}
	}

	private void dgvDetalle_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvDetalle.Columns[e.ColumnIndex].HeaderText;
		label4.Text = dgvDetalle.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label4.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			dgvDetalle.Focus();
		}
	}

	private void frmNotaOrdenAlmacen_Shown(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotaOrdenAlmacen));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.escoge = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.codnoting = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codOrdenC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecing = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecreg = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUsu = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacen_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvDetalle2 = new System.Windows.Forms.DataGridView();
		this.codnotasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnconsultar = new System.Windows.Forms.Button();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.btnbuscar = new System.Windows.Forms.Button();
		this.imageList4 = new System.Windows.Forms.ImageList(this.components);
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle2).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvDetalle);
		this.groupBox1.Controls.Add(this.dgvDetalle2);
		this.groupBox1.Location = new System.Drawing.Point(0, 77);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(733, 322);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToOrderColumns = true;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.escoge, this.codnoting, this.codOrdenC, this.codDocumento, this.documento, this.razonSocial, this.fecing, this.fecreg, this.codUsu, this.docOrden, this.almacen_1);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(727, 303);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDetalle_ColumnHeaderMouseClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.escoge.HeaderText = "Elige";
		this.escoge.Name = "escoge";
		this.escoge.Width = 50;
		this.codnoting.DataPropertyName = "codNota";
		this.codnoting.HeaderText = "Nota Ingreso";
		this.codnoting.Name = "codnoting";
		this.codnoting.Visible = false;
		this.codOrdenC.DataPropertyName = "ordenCompra";
		this.codOrdenC.HeaderText = "codOrdenCompra";
		this.codOrdenC.Name = "codOrdenC";
		this.codOrdenC.Visible = false;
		this.codDocumento.DataPropertyName = "codDocumento";
		this.codDocumento.HeaderText = "codDocumento";
		this.codDocumento.Name = "codDocumento";
		this.codDocumento.Visible = false;
		this.documento.DataPropertyName = "documento";
		this.documento.HeaderText = "Documento";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.razonSocial.DataPropertyName = "razonsocial";
		this.razonSocial.HeaderText = "Proveedor";
		this.razonSocial.Name = "razonSocial";
		this.razonSocial.Width = 200;
		this.fecing.DataPropertyName = "fechaingreso";
		this.fecing.HeaderText = "FechaIngreso";
		this.fecing.Name = "fecing";
		this.fecing.ReadOnly = true;
		this.fecreg.DataPropertyName = "fecharegistro";
		this.fecreg.HeaderText = "FechaRegistro";
		this.fecreg.Name = "fecreg";
		this.fecreg.ReadOnly = true;
		this.fecreg.Visible = false;
		this.codUsu.DataPropertyName = "codUser";
		this.codUsu.HeaderText = "codUsuario";
		this.codUsu.Name = "codUsu";
		this.codUsu.Visible = false;
		this.docOrden.DataPropertyName = "docOrden";
		this.docOrden.HeaderText = "Orden";
		this.docOrden.Name = "docOrden";
		this.docOrden.ReadOnly = true;
		this.docOrden.Width = 120;
		this.almacen_1.DataPropertyName = "almacen";
		this.almacen_1.HeaderText = "Almacen";
		this.almacen_1.Name = "almacen_1";
		this.almacen_1.Width = 150;
		this.dgvDetalle2.AllowUserToAddRows = false;
		this.dgvDetalle2.AllowUserToDeleteRows = false;
		this.dgvDetalle2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle2.Columns.AddRange(this.codnotasalida, this.fechasalida, this.tipodoc, this.serie, this.numdoc, this.almacen, this.docref);
		this.dgvDetalle2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle2.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle2.MultiSelect = false;
		this.dgvDetalle2.Name = "dgvDetalle2";
		this.dgvDetalle2.ReadOnly = true;
		this.dgvDetalle2.RowHeadersVisible = false;
		this.dgvDetalle2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle2.Size = new System.Drawing.Size(727, 303);
		this.dgvDetalle2.TabIndex = 3;
		this.dgvDetalle2.Visible = false;
		this.dgvDetalle2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle2_CellDoubleClick);
		this.dgvDetalle2.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDetalle2_CellMouseDoubleClick);
		this.codnotasalida.DataPropertyName = "codNotaSalida";
		this.codnotasalida.HeaderText = "CodNotaSalida";
		this.codnotasalida.Name = "codnotasalida";
		this.codnotasalida.ReadOnly = true;
		this.codnotasalida.Visible = false;
		this.fechasalida.DataPropertyName = "fechasalida";
		this.fechasalida.HeaderText = "Fecha Salida";
		this.fechasalida.Name = "fechasalida";
		this.fechasalida.ReadOnly = true;
		this.tipodoc.DataPropertyName = "sigla";
		this.tipodoc.HeaderText = "Tipo Doc.";
		this.tipodoc.Name = "tipodoc";
		this.tipodoc.ReadOnly = true;
		this.tipodoc.Width = 80;
		this.serie.DataPropertyName = "serie";
		this.serie.HeaderText = "Serie";
		this.serie.Name = "serie";
		this.serie.ReadOnly = true;
		this.serie.Width = 70;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N° Doc.";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.numdoc.Width = 90;
		this.almacen.DataPropertyName = "nomalmacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.Width = 135;
		this.docref.DataPropertyName = "documentoreferencia";
		this.docref.HeaderText = "Doc.Ref";
		this.docref.Name = "docref";
		this.docref.ReadOnly = true;
		this.docref.Visible = false;
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(12, 9);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(90, 30);
		this.btnAceptar.TabIndex = 1;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList2;
		this.btnSalir.Location = new System.Drawing.Point(643, 9);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(90, 30);
		this.btnSalir.TabIndex = 2;
		this.btnSalir.Text = "Salir";
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Write Document.png");
		this.imageList2.Images.SetKeyName(1, "New Document.png");
		this.imageList2.Images.SetKeyName(2, "Remove Document.png");
		this.imageList2.Images.SetKeyName(3, "document-print.png");
		this.imageList2.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList2.Images.SetKeyName(5, "exit.png");
		this.groupBox2.Controls.Add(this.btnconsultar);
		this.groupBox2.Controls.Add(this.textBox4);
		this.groupBox2.Controls.Add(this.textBox3);
		this.groupBox2.Controls.Add(this.textBox1);
		this.groupBox2.Controls.Add(this.textBox2);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.btnAceptar);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 406);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(745, 45);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.btnconsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnconsultar.ImageIndex = 1;
		this.btnconsultar.ImageList = this.imageList3;
		this.btnconsultar.Location = new System.Drawing.Point(190, 9);
		this.btnconsultar.Name = "btnconsultar";
		this.btnconsultar.Size = new System.Drawing.Size(90, 30);
		this.btnconsultar.TabIndex = 9;
		this.btnconsultar.Text = "Ir a Guia";
		this.btnconsultar.UseVisualStyleBackColor = true;
		this.btnconsultar.Visible = false;
		this.btnconsultar.Click += new System.EventHandler(btnconsultar_Click);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "exit.png");
		this.imageList3.Images.SetKeyName(1, "pedido.png");
		this.imageList3.Images.SetKeyName(2, "carrito.png");
		this.imageList3.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList3.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList3.Images.SetKeyName(5, "document_delete.png");
		this.textBox4.Enabled = false;
		this.textBox4.Location = new System.Drawing.Point(601, 15);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(36, 20);
		this.textBox4.TabIndex = 8;
		this.textBox4.Visible = false;
		this.textBox3.Enabled = false;
		this.textBox3.Location = new System.Drawing.Point(560, 15);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(35, 20);
		this.textBox3.TabIndex = 7;
		this.textBox3.Visible = false;
		this.textBox1.Enabled = false;
		this.textBox1.Location = new System.Drawing.Point(477, 15);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(36, 20);
		this.textBox1.TabIndex = 6;
		this.textBox1.Visible = false;
		this.textBox2.Enabled = false;
		this.textBox2.Location = new System.Drawing.Point(519, 15);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(35, 20);
		this.textBox2.TabIndex = 5;
		this.textBox2.Visible = false;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(99, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 40;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 39;
		this.label1.Text = "Desde";
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(102, 25);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(81, 20);
		this.dtpHasta.TabIndex = 38;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(15, 25);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(81, 20);
		this.dtpDesde.TabIndex = 37;
		this.btnbuscar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnbuscar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnbuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnbuscar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnbuscar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnbuscar.ImageIndex = 6;
		this.btnbuscar.ImageList = this.imageList4;
		this.btnbuscar.Location = new System.Drawing.Point(199, 20);
		this.btnbuscar.Name = "btnbuscar";
		this.btnbuscar.Size = new System.Drawing.Size(105, 35);
		this.btnbuscar.TabIndex = 41;
		this.btnbuscar.Text = " Consultar";
		this.btnbuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnbuscar.UseVisualStyleBackColor = false;
		this.btnbuscar.Click += new System.EventHandler(btnbuscar_Click);
		this.imageList4.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList4.ImageStream");
		this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList4.Images.SetKeyName(0, "Write Document.png");
		this.imageList4.Images.SetKeyName(1, "New Document.png");
		this.imageList4.Images.SetKeyName(2, "Remove Document.png");
		this.imageList4.Images.SetKeyName(3, "document-print.png");
		this.imageList4.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList4.Images.SetKeyName(5, "exit.png");
		this.imageList4.Images.SetKeyName(6, "search (1).png");
		this.imageList4.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList4.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList4.Images.SetKeyName(9, "document_delete.png");
		this.imageList4.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList4.Images.SetKeyName(11, "OK_Verde.png");
		this.imageList4.Images.SetKeyName(12, "Remove.png");
		this.txtFiltro.Location = new System.Drawing.Point(529, 29);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(204, 20);
		this.txtFiltro.TabIndex = 42;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(370, 33);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(29, 12);
		this.label3.TabIndex = 45;
		this.label3.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(405, 33);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 44;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(332, 33);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 43;
		this.label10.Text = "Filtro";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label4.Location = new System.Drawing.Point(385, 61);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(12, 13);
		this.label4.TabIndex = 46;
		this.label4.Text = "x";
		this.label4.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.ClientSize = new System.Drawing.Size(745, 451);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.txtFiltro);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.btnbuscar);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Name = "frmNotaOrdenAlmacen";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Guias de Compra";
		base.Load += new System.EventHandler(NotaOrdenAlmacen_Load);
		base.Shown += new System.EventHandler(frmNotaOrdenAlmacen_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle2).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
