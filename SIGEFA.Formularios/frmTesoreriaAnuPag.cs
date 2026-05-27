using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmTesoreriaAnuPag : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private clsAdmTipoDocumento admTipo = new clsAdmTipoDocumento();

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmNotaSalida admNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	private clsAdmLetra admLetra = new clsAdmLetra();

	private clsLetra let = new clsLetra();

	private clsPago pagoRp = new clsPago();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private int vencidas = 0;

	private int porvencer = 0;

	private int pendientes = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private ButtonItem btnBuscar;

	private ButtonItem buttonItem1;

	private GroupBox groupBox1;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private ComboBox cmbEstado;

	private ComboBox cmbTipo;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private DataGridView dgvCobros;

	private Button btnReporte;

	private Button btnBusqueda;

	private Label label9;

	private ComboBox cmbAlmacen;

	private TextBox txtFiltro;

	private Label label5;

	private Label label7;

	private Label label10;

	private Label label6;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem muestraPagosToolStripMenuItem;

	private ToolStripMenuItem canjearPorLetraToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem nuevaLetraToolStripMenuItem;

	private ToolStripMenuItem modificarLetraToolStripMenuItem;

	private ToolStripMenuItem imprimirLetraToolStripMenuItem;

	private ToolStripMenuItem ingresoABancoToolStripMenuItem;

	private Label lblporvencer;

	private Label lblvencidos;

	private Label lblpendientes;

	private Label label11;

	private Label label8;

	private Label label12;

	private Button button1;

	private DataGridViewTextBoxColumn codnota;

	private DataGridViewAutoFilterTextBoxColumn vendedor;

	private DataGridViewTextBoxColumn zona;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn numdocumento;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn codperso;

	private DataGridViewTextBoxColumn ruc_dni;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewAutoFilterTextBoxColumn formpago;

	private DataGridViewTextBoxColumn fechavenc;

	private DataGridViewTextBoxColumn fechacancelado;

	private DataGridViewTextBoxColumn morosidad;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn pendiente;

	private DataGridViewTextBoxColumn banco;

	private DataGridViewTextBoxColumn numunico;

	private DataGridViewLinkColumn accion;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn contado;

	private DataGridViewTextBoxColumn credito;

	public frmTesoreriaAnuPag()
	{
		InitializeComponent();
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		dgvCobros.DataSource = data;
		data.DataSource = AdmVenta.MuestraCobrosVenta(cmbEstado.SelectedIndex, Convert.ToInt32(cmbAlmacen.SelectedValue), dtpFecha1.Value, dtpFecha2.Value, Convert.ToInt32(cmbTipo.SelectedValue), 0, 5);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvCobros.ClearSelection();
		DarFormato();
	}

	private void DarFormato()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvCobros.Rows)
		{
			if (row.Cells[morosidad.Name].Value.ToString() != " - " && Convert.ToDouble(row.Cells[morosidad.Name].Value) <= 0.0 && cmbEstado.SelectedIndex == 0 && row.Index != -1)
			{
				row.Cells[morosidad.Name].Style.BackColor = Color.Red;
				row.Cells[morosidad.Name].Style.ForeColor = Color.White;
			}
		}
	}

	private void frmTesoreriaAnuPag_Load(object sender, EventArgs e)
	{
		CargaAlmacenes();
		CargaTipoDocumento();
		dtpFecha1.Value = dtpFecha2.Value.AddDays(-30.0);
		cmbEstado.SelectedIndex = 1;
		cmbAlmacen.SelectedIndex = 0;
		cmbTipo.SelectedIndex = 0;
		label7.Text = "Cliente";
		label6.Text = "cliente";
	}

	private void CalculodeFechasPagos()
	{
		try
		{
			vencidas = 0;
			porvencer = 0;
			pendientes = 0;
			foreach (DataGridViewRow row in (IEnumerable)dgvCobros.Rows)
			{
				if (Convert.ToDateTime(row.Cells[fechavenc.Name].Value).Date < Convert.ToDateTime(DateTime.Now).Date)
				{
					vencidas++;
				}
				else if (Convert.ToDateTime(row.Cells[fechavenc.Name].Value).Date == Convert.ToDateTime(DateTime.Now).Date)
				{
					porvencer++;
				}
				else
				{
					pendientes++;
				}
			}
			lblpendientes.Text = Convert.ToString(pendientes);
			lblporvencer.Text = Convert.ToString(porvencer);
			lblvencidos.Text = Convert.ToString(vencidas);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void frmTesoreriaAnuPag_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label6.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void CargaAlmacenes()
	{
		cmbAlmacen.DataSource = admAlm.CargaAlmacenes(frmLogin.iNivelUser, frmLogin.iCodEmpresa, frmLogin.iCodUser);
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedIndex = -1;
	}

	private void CargaTipoDocumento()
	{
		cmbTipo.DataSource = admTipo.CargaTipoDocumentos();
		cmbTipo.DisplayMember = "descripcion";
		cmbTipo.ValueMember = "codTipoDocumento";
		cmbTipo.SelectedIndex = -1;
	}

	private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvCobros.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvCobros.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dgvCobros_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvCobros.Rows.Count < 1 || e.RowIndex == -1)
		{
			return;
		}
		DataGridViewCell celda = dgvCobros.Rows[e.RowIndex].Cells[e.ColumnIndex];
		int itipo = Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[tipo.Name].Value);
		if (celda.Value.ToString() == "Ingresar Pago")
		{
			switch (itipo)
			{
			case 3:
			{
				venta.CodFacturaVenta = dgvCobros.Rows[e.RowIndex].Cells[codnota.Name].Value.ToString();
				frmCancelarPago form2 = new frmCancelarPago();
				form2.CodNota = venta.CodFacturaVenta;
				form2.tipo = itipo;
				DialogResult dlgResult2 = form2.ShowDialog();
				if (dlgResult2 == DialogResult.Yes)
				{
					CargaLista();
				}
				break;
			}
			case 4:
			{
				let.CodLetra = Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[codnota.Name].Value);
				frmCancelarPago form = new frmCancelarPago();
				form.CodLetra = let.CodLetra;
				form.tipo = itipo;
				DialogResult dlgResult = form.ShowDialog();
				if (dlgResult == DialogResult.Yes)
				{
					CargaLista();
				}
				break;
			}
			}
		}
		else
		{
			if (!(celda.Value.ToString() == "Muestra Pagos"))
			{
				return;
			}
			switch (itipo)
			{
			case 3:
			{
				venta.CodFacturaVenta = dgvCobros.Rows[e.RowIndex].Cells[codnota.Name].Value.ToString();
				frmMuestraPagos2 form4 = new frmMuestraPagos2();
				form4.CodNota = Convert.ToInt32(venta.CodFacturaVenta);
				form4.InOut = true;
				form4.tipo = 0;
				DialogResult dlgResult4 = form4.ShowDialog();
				if (dlgResult4 == DialogResult.Yes)
				{
					CargaLista();
				}
				break;
			}
			case 4:
			{
				let.CodLetra = Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[codnota.Name].Value);
				frmMuestraPagos2 form3 = new frmMuestraPagos2();
				form3.CodNota = let.CodLetra;
				form3.InOut = true;
				form3.tipo = 1;
				DialogResult dlgResult3 = form3.ShowDialog();
				if (dlgResult3 == DialogResult.Yes)
				{
					CargaLista();
				}
				break;
			}
			}
		}
	}

	private void muestraPagosToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvCobros.SelectedRows[0];
		venta.CodFacturaVenta = Row.Cells[codnota.Name].Value.ToString();
		frmMuestraPagos2 form = new frmMuestraPagos2();
		form.CodNota = Convert.ToInt32(venta.CodFacturaVenta);
		form.InOut = true;
		form.ShowDialog();
		CargaLista();
	}

	private void canjearPorLetraToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dgvCobros.SelectedRows.Count > 0)
		{
			DataGridViewRow Row = dgvCobros.CurrentRow;
			venta.CodFacturaVenta = Row.Cells[codnota.Name].Value.ToString();
			frmCanjearLetra form = new frmCanjearLetra();
			form.venta = venta;
			form.Procede = 2;
			form.ShowDialog();
			CargaLista();
		}
	}

	private void dgvCobros_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		dgvCobros.ContextMenuStrip = new ContextMenuStrip();
		if (e.RowIndex == -1)
		{
			return;
		}
		dgvCobros.Rows[e.RowIndex].Selected = true;
		if (e.Button != MouseButtons.Right || e.RowIndex == -1 || dgvCobros.SelectedCells.Count <= 0)
		{
			return;
		}
		dgvCobros.ContextMenuStrip = contextMenuStrip1;
		if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[tipo.Name].Value) == 3)
		{
			canjearPorLetraToolStripMenuItem.Enabled = true;
			modificarLetraToolStripMenuItem.Enabled = false;
			imprimirLetraToolStripMenuItem.Enabled = false;
			ingresoABancoToolStripMenuItem.Enabled = false;
			if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[cantidad.Name].Value) > 0)
			{
				muestraPagosToolStripMenuItem.Enabled = true;
			}
			else
			{
				muestraPagosToolStripMenuItem.Enabled = false;
			}
		}
		else if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[tipo.Name].Value) == 4)
		{
			canjearPorLetraToolStripMenuItem.Enabled = false;
			modificarLetraToolStripMenuItem.Enabled = true;
			imprimirLetraToolStripMenuItem.Enabled = true;
			ingresoABancoToolStripMenuItem.Enabled = true;
			if (Convert.ToInt32(dgvCobros.Rows[e.RowIndex].Cells[cantidad.Name].Value) > 0)
			{
				muestraPagosToolStripMenuItem.Enabled = true;
			}
			else
			{
				muestraPagosToolStripMenuItem.Enabled = false;
			}
		}
	}

	private void dgvCobros_Sorted(object sender, EventArgs e)
	{
		DarFormato();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		dt.Columns.Add("codnota", typeof(string));
		dt.Columns.Add("vendedor", typeof(string));
		dt.Columns.Add("fechaemision", typeof(DateTime));
		dt.Columns.Add("tipo", typeof(string));
		dt.Columns.Add("numdocumento", typeof(string));
		dt.Columns.Add("documento", typeof(string));
		dt.Columns.Add("codcliente", typeof(string));
		dt.Columns.Add("codperso", typeof(string));
		dt.Columns.Add("cliente", typeof(string));
		dt.Columns.Add("formapago", typeof(string));
		dt.Columns.Add("fechavenc", typeof(DateTime));
		dt.Columns.Add("morosidad", typeof(string));
		dt.Columns.Add("moneda", typeof(string));
		dt.Columns.Add("monto", typeof(decimal));
		dt.Columns.Add("pendiente", typeof(decimal));
		dt.Columns.Add("banco", typeof(string));
		dt.Columns.Add("nuunico", typeof(string));
		dt.Columns.Add("accion", typeof(string));
		dt.Columns.Add("cantidad", typeof(decimal));
		dt.Columns.Add("contado", typeof(decimal));
		dt.Columns.Add("credito", typeof(decimal));
		dt.Columns.Add("fecha1", typeof(DateTime));
		dt.Columns.Add("fecha2", typeof(DateTime));
		dt.Columns.Add("estado", typeof(string));
		dt.Columns.Add("tip", typeof(string));
		foreach (DataGridViewRow dgv in (IEnumerable)dgvCobros.Rows)
		{
			dt.Rows.Add(dgv.Cells[codnota.Name].Value, dgv.Cells[vendedor.Name].Value, Convert.ToDateTime(dgv.Cells[fechaemision.Name].Value), dgv.Cells[tipo.Name].Value, dgv.Cells[numdocumento.Name].Value, dgv.Cells[documento.Name].Value, dgv.Cells[codcliente.Name].Value, dgv.Cells[codperso.Name].Value, dgv.Cells[cliente.Name].Value, dgv.Cells[formpago.Name].Value, Convert.ToDateTime(dgv.Cells[fechavenc.Name].Value), dgv.Cells[morosidad.Name].Value, dgv.Cells[moneda.Name].Value, Convert.ToDecimal(dgv.Cells[monto.Name].Value), Convert.ToDecimal(dgv.Cells[pendiente.Name].Value), dgv.Cells[banco.Name].Value, dgv.Cells[numunico.Name].Value, dgv.Cells[accion.Name].Value, Convert.ToDecimal(dgv.Cells[cantidad.Name].Value), Convert.ToDecimal(dgv.Cells[contado.Name].Value), Convert.ToDecimal(dgv.Cells[credito.Name].Value), dtpFecha1.Text, dtpFecha2.Text, cmbEstado.Text, cmbTipo.Text);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\CobrosGeneralRPT.xml", XmlWriteMode.WriteSchema);
		CRCobrosGeneral rpt = new CRCobrosGeneral();
		frmRptCobrosGeneral frm = new frmRptCobrosGeneral();
		rpt.SetDataSource(ds);
		frm.crvCobrosGeneral.ReportSource = rpt;
		frm.Show();
	}

	private void ingresoABancoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridViewRow Row = dgvCobros.CurrentRow;
		frmIngresoBanco form = new frmIngresoBanco();
		form.CodLetra = Convert.ToInt32(Row.Cells[codnota.Name].Value);
		form.Proceso = 1;
		form.ShowDialog();
		CargaLista();
	}

	private void dgvCobros_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F3)
		{
			return;
		}
		DataGridViewRow row = dgvCobros.CurrentRow;
		int itipo = Convert.ToInt32(row.Cells[tipo.Name].Value);
		if (!(row.Cells[accion.Name].Value.ToString() == "Ingresar Pago"))
		{
			return;
		}
		switch (itipo)
		{
		case 3:
		{
			venta.CodFacturaVenta = row.Cells[codnota.Name].Value.ToString();
			frmCancelarPago form2 = new frmCancelarPago();
			form2.CodNota = venta.CodFacturaVenta;
			form2.tipo = itipo;
			DialogResult dlgResult2 = form2.ShowDialog();
			if (dlgResult2 == DialogResult.Yes)
			{
				CargaLista();
			}
			break;
		}
		case 4:
		{
			let.CodLetra = Convert.ToInt32(row.Cells[codnota.Name].Value);
			frmCancelarPago form = new frmCancelarPago();
			form.CodLetra = let.CodLetra;
			form.tipo = itipo;
			DialogResult dlgResult = form.ShowDialog();
			if (dlgResult == DialogResult.Yes)
			{
				CargaLista();
			}
			break;
		}
		}
	}

	private void frmTesoreriaAnuPag_Shown(object sender, EventArgs e)
	{
		CargaLista();
		txtFiltro.Focus();
	}

	private void dgvCobros_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		if (cmbEstado.Text == "PENDIENTES")
		{
			DarFormato();
		}
	}

	private void dgvCobros_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		CalculodeFechasPagos();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		CargaLista();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTesoreriaAnuPag));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.lblporvencer = new System.Windows.Forms.Label();
		this.lblvencidos = new System.Windows.Forms.Label();
		this.lblpendientes = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.cmbTipo = new System.Windows.Forms.ComboBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.dgvCobros = new System.Windows.Forms.DataGridView();
		this.codnota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.vendedor = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codperso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc_dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formpago = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fechavenc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechacancelado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.morosidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numunico = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.accion = new System.Windows.Forms.DataGridViewLinkColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.credito = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.muestraPagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.canjearPorLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.nuevaLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modificarLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.imprimirLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ingresoABancoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCobros).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "folder_open (1).png");
		this.imageList1.Images.SetKeyName(17, "folder-open-icon.png");
		this.imageList1.Images.SetKeyName(18, "Glossy-Open-icon.png");
		this.imageList1.Images.SetKeyName(19, "Ocean Blue Open.png");
		this.imageList1.Images.SetKeyName(20, "Open (1).png");
		this.imageList1.Images.SetKeyName(21, "open_folder_green.png");
		this.btnBuscar.ImageIndex = 11;
		this.btnBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.SubItemsExpandWidth = 14;
		this.btnBuscar.Text = "Buscar";
		this.buttonItem1.ImageIndex = 11;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Buscar";
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.lblporvencer);
		this.groupBox1.Controls.Add(this.lblvencidos);
		this.groupBox1.Controls.Add(this.lblpendientes);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbEstado);
		this.groupBox1.Controls.Add(this.cmbTipo);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1363, 59);
		this.groupBox1.TabIndex = 7;
		this.groupBox1.TabStop = false;
		this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button1.ImageKey = "g-icon-new-update.png";
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(884, 17);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(87, 33);
		this.button1.TabIndex = 56;
		this.button1.Text = "Actualizar";
		this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.lblporvencer.AutoSize = true;
		this.lblporvencer.ForeColor = System.Drawing.Color.SeaGreen;
		this.lblporvencer.Location = new System.Drawing.Point(1262, 26);
		this.lblporvencer.Name = "lblporvencer";
		this.lblporvencer.Size = new System.Drawing.Size(13, 13);
		this.lblporvencer.TabIndex = 55;
		this.lblporvencer.Text = "0";
		this.lblporvencer.Visible = false;
		this.lblvencidos.AutoSize = true;
		this.lblvencidos.ForeColor = System.Drawing.Color.Red;
		this.lblvencidos.Location = new System.Drawing.Point(1262, 43);
		this.lblvencidos.Name = "lblvencidos";
		this.lblvencidos.Size = new System.Drawing.Size(13, 13);
		this.lblvencidos.TabIndex = 54;
		this.lblvencidos.Text = "0";
		this.lblvencidos.Visible = false;
		this.lblpendientes.AutoSize = true;
		this.lblpendientes.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.lblpendientes.Location = new System.Drawing.Point(1262, 9);
		this.lblpendientes.Name = "lblpendientes";
		this.lblpendientes.Size = new System.Drawing.Size(13, 13);
		this.lblpendientes.TabIndex = 53;
		this.lblpendientes.Text = "0";
		this.lblpendientes.Visible = false;
		this.label11.AutoSize = true;
		this.label11.ForeColor = System.Drawing.Color.SeaGreen;
		this.label11.Location = new System.Drawing.Point(1176, 26);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(80, 13);
		this.label11.TabIndex = 52;
		this.label11.Text = "POR VENCER:";
		this.label11.Visible = false;
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Red;
		this.label8.Location = new System.Drawing.Point(1191, 43);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(65, 13);
		this.label8.TabIndex = 51;
		this.label8.Text = "VENCIDOS:";
		this.label8.Visible = false;
		this.label12.AutoSize = true;
		this.label12.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label12.Location = new System.Drawing.Point(1177, 9);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(79, 13);
		this.label12.TabIndex = 50;
		this.label12.Text = "PENDIENTES:";
		this.label12.Visible = false;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label6.Location = new System.Drawing.Point(1177, 9);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 37;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(600, 9);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 36;
		this.label5.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(635, 9);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 35;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(562, 9);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 34;
		this.label10.Text = "Filtro";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(564, 24);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 33;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(6, 9);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(49, 12);
		this.label9.TabIndex = 32;
		this.label9.Text = "Almacen";
		this.cmbAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(9, 25);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(121, 20);
		this.cmbAlmacen.TabIndex = 31;
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList1;
		this.btnBusqueda.Location = new System.Drawing.Point(789, 17);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(78, 33);
		this.btnBusqueda.TabIndex = 30;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.btnReporte.ImageIndex = 7;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(1093, 16);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 33);
		this.btnReporte.TabIndex = 29;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Visible = false;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(434, 9);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 12);
		this.label4.TabIndex = 28;
		this.label4.Text = "Estado";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(307, 9);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(26, 12);
		this.label3.TabIndex = 27;
		this.label3.Text = "Tipo";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(220, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 26;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(133, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 25;
		this.label1.Text = "Desde";
		this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[2] { "PENDIENTES", "CANCELADOS" });
		this.cmbEstado.Location = new System.Drawing.Point(437, 25);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(121, 20);
		this.cmbEstado.TabIndex = 24;
		this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbTipo.FormattingEnabled = true;
		this.cmbTipo.Location = new System.Drawing.Point(310, 25);
		this.cmbTipo.Name = "cmbTipo";
		this.cmbTipo.Size = new System.Drawing.Size(121, 20);
		this.cmbTipo.TabIndex = 23;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(223, 25);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 22;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(136, 25);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 21;
		this.dgvCobros.AllowUserToAddRows = false;
		this.dgvCobros.AllowUserToDeleteRows = false;
		this.dgvCobros.AllowUserToResizeColumns = false;
		this.dgvCobros.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCobros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvCobros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCobros.Columns.AddRange(this.codnota, this.vendedor, this.zona, this.fechaemision, this.tipo, this.numdocumento, this.documento, this.codcliente, this.codperso, this.ruc_dni, this.cliente, this.formpago, this.fechavenc, this.fechacancelado, this.morosidad, this.moneda, this.monto, this.pendiente, this.banco, this.numunico, this.accion, this.cantidad, this.contado, this.credito);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvCobros.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgvCobros.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvCobros.Location = new System.Drawing.Point(0, 59);
		this.dgvCobros.MultiSelect = false;
		this.dgvCobros.Name = "dgvCobros";
		this.dgvCobros.ReadOnly = true;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCobros.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgvCobros.RowHeadersVisible = false;
		this.dgvCobros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCobros.Size = new System.Drawing.Size(1363, 415);
		this.dgvCobros.TabIndex = 8;
		this.dgvCobros.Sorted += new System.EventHandler(dgvCobros_Sorted);
		this.dgvCobros.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_ColumnHeaderMouseClick);
		this.dgvCobros.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvCobros_CellMouseDown);
		this.dgvCobros.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(dgvCobros_CellFormatting);
		this.dgvCobros.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvCobros_KeyDown);
		this.dgvCobros.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCobros_RowStateChanged);
		this.dgvCobros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCobros_CellContentClick);
		this.codnota.DataPropertyName = "codigo";
		this.codnota.HeaderText = "codnota";
		this.codnota.Name = "codnota";
		this.codnota.ReadOnly = true;
		this.codnota.Visible = false;
		this.vendedor.DataPropertyName = "vendedor";
		this.vendedor.HeaderText = "Vendedor";
		this.vendedor.Name = "vendedor";
		this.vendedor.ReadOnly = true;
		this.vendedor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.vendedor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.vendedor.Width = 80;
		this.zona.DataPropertyName = "zona";
		this.zona.HeaderText = "Zona";
		this.zona.Name = "zona";
		this.zona.ReadOnly = true;
		this.fechaemision.DataPropertyName = "fechaemision";
		this.fechaemision.HeaderText = "Fec. Emi.";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechaemision.Width = 70;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "tipo";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Visible = false;
		this.numdocumento.DataPropertyName = "numdocumento";
		this.numdocumento.HeaderText = "N° Documento";
		this.numdocumento.Name = "numdocumento";
		this.numdocumento.ReadOnly = true;
		this.numdocumento.Width = 90;
		this.documento.DataPropertyName = "docref";
		this.documento.HeaderText = "Documento";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Visible = false;
		this.documento.Width = 90;
		this.codcliente.DataPropertyName = "codCliente";
		this.codcliente.HeaderText = "codcliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.ReadOnly = true;
		this.codcliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codcliente.Visible = false;
		this.codperso.DataPropertyName = "ruc_dni";
		this.codperso.HeaderText = "Codigo";
		this.codperso.Name = "codperso";
		this.codperso.ReadOnly = true;
		this.codperso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ruc_dni.DataPropertyName = "codigopersonalizado";
		this.ruc_dni.HeaderText = "Ruc_Dni";
		this.ruc_dni.Name = "ruc_dni";
		this.ruc_dni.ReadOnly = true;
		this.ruc_dni.Visible = false;
		this.cliente.DataPropertyName = "razonsocial";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Width = 160;
		this.formpago.DataPropertyName = "formapago";
		this.formpago.HeaderText = "Forma Pago";
		this.formpago.Name = "formpago";
		this.formpago.ReadOnly = true;
		this.formpago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.formpago.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.fechavenc.DataPropertyName = "fechavence";
		this.fechavenc.HeaderText = "Fec. Ven.";
		this.fechavenc.Name = "fechavenc";
		this.fechavenc.ReadOnly = true;
		this.fechavenc.Width = 70;
		this.fechacancelado.DataPropertyName = "fechacancelado";
		dataGridViewCellStyle7.NullValue = null;
		this.fechacancelado.DefaultCellStyle = dataGridViewCellStyle7;
		this.fechacancelado.HeaderText = "Fec. Can.";
		this.fechacancelado.Name = "fechacancelado";
		this.fechacancelado.ReadOnly = true;
		this.fechacancelado.Width = 70;
		this.morosidad.DataPropertyName = "diasmora";
		this.morosidad.HeaderText = "Mora";
		this.morosidad.Name = "morosidad";
		this.morosidad.ReadOnly = true;
		this.morosidad.Width = 60;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Width = 70;
		this.monto.DataPropertyName = "total";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.monto.DefaultCellStyle = dataGridViewCellStyle8;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.Width = 70;
		this.pendiente.DataPropertyName = "pendiente";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.pendiente.DefaultCellStyle = dataGridViewCellStyle9;
		this.pendiente.HeaderText = "Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.Width = 70;
		this.banco.DataPropertyName = "banco";
		this.banco.HeaderText = "Banco";
		this.banco.Name = "banco";
		this.banco.ReadOnly = true;
		this.banco.Visible = false;
		this.banco.Width = 90;
		this.numunico.DataPropertyName = "num";
		this.numunico.HeaderText = "N° único";
		this.numunico.Name = "numunico";
		this.numunico.ReadOnly = true;
		this.numunico.Visible = false;
		this.numunico.Width = 80;
		this.accion.DataPropertyName = "accion";
		this.accion.HeaderText = "Acción";
		this.accion.Name = "accion";
		this.accion.ReadOnly = true;
		this.accion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.accion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.cantidad.DataPropertyName = "cantpagos";
		this.cantidad.HeaderText = "Cant. pagos";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Visible = false;
		this.contado.DataPropertyName = "contado";
		this.contado.HeaderText = "contado";
		this.contado.Name = "contado";
		this.contado.ReadOnly = true;
		this.contado.Visible = false;
		this.credito.DataPropertyName = "credito";
		this.credito.HeaderText = "credito";
		this.credito.Name = "credito";
		this.credito.ReadOnly = true;
		this.credito.Visible = false;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.muestraPagosToolStripMenuItem, this.canjearPorLetraToolStripMenuItem, this.toolStripSeparator1, this.nuevaLetraToolStripMenuItem, this.modificarLetraToolStripMenuItem, this.imprimirLetraToolStripMenuItem, this.ingresoABancoToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(165, 142);
		this.muestraPagosToolStripMenuItem.Name = "muestraPagosToolStripMenuItem";
		this.muestraPagosToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.muestraPagosToolStripMenuItem.Text = "Muestra Pagos";
		this.muestraPagosToolStripMenuItem.Click += new System.EventHandler(muestraPagosToolStripMenuItem_Click);
		this.canjearPorLetraToolStripMenuItem.Name = "canjearPorLetraToolStripMenuItem";
		this.canjearPorLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.canjearPorLetraToolStripMenuItem.Text = "Canjear por Letra";
		this.canjearPorLetraToolStripMenuItem.Click += new System.EventHandler(canjearPorLetraToolStripMenuItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
		this.nuevaLetraToolStripMenuItem.Name = "nuevaLetraToolStripMenuItem";
		this.nuevaLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.nuevaLetraToolStripMenuItem.Text = "Nueva Letra";
		this.modificarLetraToolStripMenuItem.Name = "modificarLetraToolStripMenuItem";
		this.modificarLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.modificarLetraToolStripMenuItem.Text = "Modificar Letra";
		this.imprimirLetraToolStripMenuItem.Name = "imprimirLetraToolStripMenuItem";
		this.imprimirLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.imprimirLetraToolStripMenuItem.Text = "Imprimir Letra";
		this.ingresoABancoToolStripMenuItem.Name = "ingresoABancoToolStripMenuItem";
		this.ingresoABancoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
		this.ingresoABancoToolStripMenuItem.Text = "Ingreso a banco";
		this.ingresoABancoToolStripMenuItem.Click += new System.EventHandler(ingresoABancoToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1363, 474);
		base.Controls.Add(this.dgvCobros);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.KeyPreview = true;
		base.Name = "frmTesoreriaAnuPag";
		base.ShowInTaskbar = false;
		this.Text = "Cobros";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmTesoreriaAnuPag_Load);
		base.Shown += new System.EventHandler(frmTesoreriaAnuPag_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCobros).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
