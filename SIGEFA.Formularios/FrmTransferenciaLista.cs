using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class FrmTransferenciaLista : Office2007Form
{
	private clsAdmTransferencia admtrans = new clsAdmTransferencia();

	private clsTransferencia trans = new clsTransferencia();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private int enviados;

	private clsReporteTransferencia ds2 = new clsReporteTransferencia();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private DataGridView dgvTransferenciasPendientes;

	private Label label1;

	private ComboBox cbTipo;

	private ImageList imageList1;

	private ImageList imageList2;

	private Button btnBusqueda;

	private Button btnSalir;

	private Label label2;

	private TextBox txtCodprod;

	private Button btnBuscarProducto;

	private Label label3;

	private Label txtNombreProducto;

	private CheckBox ckbenviados;

	private DataGridViewTextBoxColumn TDirecta;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn numerodoc;

	private DataGridViewTextBoxColumn AlmacenO;

	private DataGridViewTextBoxColumn AlmacenOrigen;

	private DataGridViewTextBoxColumn codAlmacenDestino;

	private DataGridViewTextBoxColumn almacendestino;

	private DataGridViewTextBoxColumn DecripcionRechazo;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn CodTDoc;

	private DataGridViewTextBoxColumn Sigla;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn Moneda;

	private DataGridViewTextBoxColumn TipoCambio;

	private DataGridViewTextBoxColumn montodes;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewTextBoxColumn codUsuario;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn codAutorizado;

	private DataGridViewTextBoxColumn codListaPrecio;

	private DataGridViewTextBoxColumn formapago;

	private DataGridViewTextBoxColumn comentario;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn fechaentrega;

	private DataGridViewTextBoxColumn Bruto;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn Total;

	public FrmTransferenciaLista()
	{
		InitializeComponent();
	}

	private void FrmTransferenciaLista_Load(object sender, EventArgs e)
	{
		cbTipo.SelectedIndex = 0;
		dtpDesde.Value = DateTime.Now.AddDays(-6.0);
		CargaLista();
	}

	public void CargaLista()
	{
		dgvTransferenciasPendientes.AutoGenerateColumns = false;
		dgvTransferenciasPendientes.DataSource = data;
		data.DataSource = admtrans.MuestraTranferencias2(Convert.ToInt32(cbTipo.SelectedIndex), frmLogin.iCodUser, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		if (cbTipo.SelectedIndex == 0)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int i = 0; i < dgvTransferenciasPendientes.RowCount; i++)
			{
				dgvTransferenciasPendientes["TDirecta", i].Style.BackColor = Color.FromArgb(255, 255, 183);
			}
		}
		else if (cbTipo.SelectedIndex == 1)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int j = 0; j < dgvTransferenciasPendientes.RowCount; j++)
			{
				dgvTransferenciasPendientes["TDirecta", j].Style.BackColor = Color.FromArgb(228, 255, 187);
			}
		}
		else if (cbTipo.SelectedIndex == 2)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int k = 0; k < dgvTransferenciasPendientes.RowCount; k++)
			{
				dgvTransferenciasPendientes["TDirecta", k].Style.BackColor = Color.FromArgb(208, 255, 136);
			}
		}
		else if (cbTipo.SelectedIndex == 3)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int l = 0; l < dgvTransferenciasPendientes.RowCount; l++)
			{
				dgvTransferenciasPendientes["TDirecta", l].Style.BackColor = Color.FromArgb(20, 110, 255);
			}
		}
		else if (cbTipo.SelectedIndex == 4)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int m = 0; m < dgvTransferenciasPendientes.RowCount; m++)
			{
				dgvTransferenciasPendientes["TDirecta", m].Style.BackColor = Color.FromArgb(0, 180, 204);
			}
		}
		else if (cbTipo.SelectedIndex == 5)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = true;
			for (int n = 0; n < dgvTransferenciasPendientes.RowCount; n++)
			{
				dgvTransferenciasPendientes["TDirecta", n].Style.BackColor = Color.FromArgb(255, 139, 139);
			}
		}
		dgvTransferenciasPendientes.ClearSelection();
	}

	public void CargaListaEnviados()
	{
		dgvTransferenciasPendientes.AutoGenerateColumns = false;
		dgvTransferenciasPendientes.DataSource = data;
		data.DataSource = admtrans.MuestraTranferenciasEnviadas(Convert.ToInt32(cbTipo.SelectedIndex), frmLogin.iCodUser, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		if (cbTipo.SelectedIndex == 0)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int i = 0; i < dgvTransferenciasPendientes.RowCount; i++)
			{
				dgvTransferenciasPendientes["TDirecta", i].Style.BackColor = Color.FromArgb(255, 255, 183);
			}
		}
		else if (cbTipo.SelectedIndex == 1)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int j = 0; j < dgvTransferenciasPendientes.RowCount; j++)
			{
				dgvTransferenciasPendientes["TDirecta", j].Style.BackColor = Color.FromArgb(228, 255, 187);
			}
		}
		else if (cbTipo.SelectedIndex == 2)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int k = 0; k < dgvTransferenciasPendientes.RowCount; k++)
			{
				dgvTransferenciasPendientes["TDirecta", k].Style.BackColor = Color.FromArgb(208, 255, 136);
			}
		}
		else if (cbTipo.SelectedIndex == 3)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int l = 0; l < dgvTransferenciasPendientes.RowCount; l++)
			{
				dgvTransferenciasPendientes["TDirecta", l].Style.BackColor = Color.FromArgb(20, 110, 255);
			}
		}
		else if (cbTipo.SelectedIndex == 4)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int m = 0; m < dgvTransferenciasPendientes.RowCount; m++)
			{
				dgvTransferenciasPendientes["TDirecta", m].Style.BackColor = Color.FromArgb(0, 180, 204);
			}
		}
		else if (cbTipo.SelectedIndex == 5)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = true;
			for (int n = 0; n < dgvTransferenciasPendientes.RowCount; n++)
			{
				dgvTransferenciasPendientes["TDirecta", n].Style.BackColor = Color.FromArgb(255, 139, 139);
			}
		}
		dgvTransferenciasPendientes.ClearSelection();
	}

	private void dgvTransferenciasPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvTransferenciasPendientes.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmRTransferenciaEstado form = new frmRTransferenciaEstado();
			form.MdiParent = base.MdiParent;
			form.CodTransDirecta = Convert.ToInt32(dgvTransferenciasPendientes.CurrentRow.Cells[codigo.Name].Value);
			form.Proceso = 3;
			form.caso = cbTipo.SelectedIndex;
			form.estadotransfer = Convert.ToString(dgvTransferenciasPendientes.CurrentRow.Cells[TDirecta.Name].Value);
			form.enviados = enviados;
			form.Show();
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		if (ckbenviados.Checked)
		{
			CargaListaEnviados();
			enviados = 1;
		}
		else
		{
			CargaLista();
			enviados = 0;
		}
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		if (ckbenviados.Checked)
		{
			CargaListaEnviados();
			enviados = 1;
		}
		else
		{
			CargaLista();
			enviados = 0;
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (ckbenviados.Checked)
		{
			CargaListaEnviados();
			enviados = 1;
		}
		else
		{
			CargaLista();
			enviados = 0;
		}
	}

	private void cbTipo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (ckbenviados.Checked)
		{
			CargaListaEnviados();
			enviados = 1;
		}
		else
		{
			CargaLista();
			enviados = 0;
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		DataTable nuevo = new DataTable();
		CRTransferencia rpt = new CRTransferencia();
		FrmRptTransferencia frm = new FrmRptTransferencia();
		nuevo = ds2.RptTransferencia(Convert.ToDateTime(dtpDesde.Value), Convert.ToDateTime(dtpHasta.Value), Convert.ToInt32(frmLogin.iCodAlmacen)).Tables[0];
		rpt.SetDataSource(nuevo);
		frm.crvreportetrasferencia.ReportSource = rpt;
		frm.ShowDialog();
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			CargaListaxProducto();
		}
		else
		{
			txtNombreProducto.Text = "---";
		}
	}

	public void CargaListaxProducto()
	{
		int codprod = Convert.ToInt32(txtCodprod.Text.Trim());
		dgvTransferenciasPendientes.AutoGenerateColumns = false;
		dgvTransferenciasPendientes.DataSource = data;
		data.DataSource = admtrans.MuestraTranferenciasxProducto(Convert.ToInt32(cbTipo.SelectedIndex), frmLogin.iCodUser, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date, codprod);
		data.Filter = string.Empty;
		filtro = string.Empty;
		if (cbTipo.SelectedIndex == 0)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int i = 0; i < dgvTransferenciasPendientes.RowCount; i++)
			{
				dgvTransferenciasPendientes["TDirecta", i].Style.BackColor = Color.FromArgb(255, 255, 183);
			}
		}
		else if (cbTipo.SelectedIndex == 1)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = false;
			for (int j = 0; j < dgvTransferenciasPendientes.RowCount; j++)
			{
				dgvTransferenciasPendientes["TDirecta", j].Style.BackColor = Color.FromArgb(228, 255, 187);
			}
		}
		else if (cbTipo.SelectedIndex == 2)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = true;
			for (int k = 0; k < dgvTransferenciasPendientes.RowCount; k++)
			{
				dgvTransferenciasPendientes["TDirecta", k].Style.BackColor = Color.FromArgb(255, 139, 139);
			}
		}
		else if (cbTipo.SelectedIndex == 3)
		{
			dgvTransferenciasPendientes.Columns["DecripcionRechazo"].Visible = true;
			for (int l = 0; l < dgvTransferenciasPendientes.RowCount; l++)
			{
				dgvTransferenciasPendientes["TDirecta", l].Style.BackColor = Color.FromArgb(195, 240, 246);
			}
		}
		dgvTransferenciasPendientes.ClearSelection();
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 2;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			btnBuscarProducto_Click(null, null);
		}
	}

	private void ckbenviados_CheckedChanged(object sender, EventArgs e)
	{
		if (ckbenviados.Checked)
		{
			CargaListaEnviados();
			enviados = 1;
		}
		else
		{
			CargaLista();
			enviados = 0;
		}
	}

	private void dgvTransferenciasPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex == dgvTransferenciasPendientes.Columns["numerodoc"].Index && e.RowIndex >= 0)
		{
			frmDetalleDespachos form = new frmDetalleDespachos();
			form.CodTransDirecta = Convert.ToInt32(dgvTransferenciasPendientes.CurrentRow.Cells[codigo.Name].Value);
			form.Proceso = 1;
			form.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.FrmTransferenciaLista));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ckbenviados = new System.Windows.Forms.CheckBox();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.cbTipo = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dgvTransferenciasPendientes = new System.Windows.Forms.DataGridView();
		this.TDirecta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numerodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.AlmacenO = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.AlmacenOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacenDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacendestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DecripcionRechazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodTDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TipoCambio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodes = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAutorizado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codListaPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaentrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Bruto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTransferenciasPendientes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.ckbenviados);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.cbTipo);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.dgvTransferenciasPendientes);
		this.groupBox1.Location = new System.Drawing.Point(-2, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1143, 466);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.ckbenviados.AutoSize = true;
		this.ckbenviados.Location = new System.Drawing.Point(734, 20);
		this.ckbenviados.Name = "ckbenviados";
		this.ckbenviados.Size = new System.Drawing.Size(81, 17);
		this.ckbenviados.TabIndex = 33;
		this.ckbenviados.Text = "ENVIADOS";
		this.ckbenviados.UseVisualStyleBackColor = true;
		this.ckbenviados.CheckedChanged += new System.EventHandler(ckbenviados_CheckedChanged);
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList1;
		this.btnBusqueda.Location = new System.Drawing.Point(551, 11);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(80, 33);
		this.btnBusqueda.TabIndex = 32;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
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
		this.imageList1.Images.SetKeyName(22, "img_transferencia.png");
		this.cbTipo.FormattingEnabled = true;
		this.cbTipo.Items.AddRange(new object[7] { "Pendiente", "Aprobado", "Facturado", "Entregado Parcial", "Entregado Total", "Anulado", "Todos" });
		this.cbTipo.Location = new System.Drawing.Point(402, 17);
		this.cbTipo.Name = "cbTipo";
		this.cbTipo.Size = new System.Drawing.Size(121, 21);
		this.cbTipo.TabIndex = 20;
		this.cbTipo.SelectionChangeCommitted += new System.EventHandler(cbTipo_SelectionChangeCommitted);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(365, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(34, 13);
		this.label1.TabIndex = 19;
		this.label1.Text = "Tipo :";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(197, 21);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(24, 21);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(77, 18);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 15;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(244, 18);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 14;
		this.dgvTransferenciasPendientes.AllowUserToAddRows = false;
		this.dgvTransferenciasPendientes.AllowUserToDeleteRows = false;
		this.dgvTransferenciasPendientes.AllowUserToOrderColumns = true;
		this.dgvTransferenciasPendientes.AllowUserToResizeRows = false;
		this.dgvTransferenciasPendientes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvTransferenciasPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvTransferenciasPendientes.Columns.AddRange(this.TDirecta, this.codigo, this.numerodoc, this.AlmacenO, this.AlmacenOrigen, this.codAlmacenDestino, this.almacendestino, this.DecripcionRechazo, this.usuario, this.CodTDoc, this.Sigla, this.documento, this.Moneda, this.TipoCambio, this.montodes, this.igv, this.estado, this.fechapago, this.codUsuario, this.fecharegistro, this.codAutorizado, this.codListaPrecio, this.formapago, this.comentario, this.fecha, this.fechaentrega, this.Bruto, this.importe, this.Total);
		this.dgvTransferenciasPendientes.Location = new System.Drawing.Point(3, 56);
		this.dgvTransferenciasPendientes.MultiSelect = false;
		this.dgvTransferenciasPendientes.Name = "dgvTransferenciasPendientes";
		this.dgvTransferenciasPendientes.ReadOnly = true;
		this.dgvTransferenciasPendientes.RowHeadersVisible = false;
		this.dgvTransferenciasPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTransferenciasPendientes.Size = new System.Drawing.Size(1140, 407);
		this.dgvTransferenciasPendientes.TabIndex = 0;
		this.dgvTransferenciasPendientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTransferenciasPendientes_CellContentClick);
		this.dgvTransferenciasPendientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTransferenciasPendientes_CellDoubleClick);
		this.TDirecta.DataPropertyName = "TDirecta";
		this.TDirecta.HeaderText = "TDirecta";
		this.TDirecta.Name = "TDirecta";
		this.TDirecta.ReadOnly = true;
		this.codigo.DataPropertyName = "codTransDir";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Width = 80;
		this.numerodoc.DataPropertyName = "numerodoc";
		this.numerodoc.HeaderText = "Nro. Documento";
		this.numerodoc.Name = "numerodoc";
		this.numerodoc.ReadOnly = true;
		this.AlmacenO.DataPropertyName = "codAlmacenOrigen";
		this.AlmacenO.HeaderText = "CodAlmacenOrigen";
		this.AlmacenO.Name = "AlmacenO";
		this.AlmacenO.ReadOnly = true;
		this.AlmacenO.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.AlmacenO.Visible = false;
		this.AlmacenO.Width = 270;
		this.AlmacenOrigen.DataPropertyName = "almacenorigen";
		this.AlmacenOrigen.HeaderText = "AlmacenOrigen";
		this.AlmacenOrigen.Name = "AlmacenOrigen";
		this.AlmacenOrigen.ReadOnly = true;
		this.AlmacenOrigen.Width = 200;
		this.codAlmacenDestino.DataPropertyName = "codAlmacenDestino";
		this.codAlmacenDestino.HeaderText = "codAlmacenDestino";
		this.codAlmacenDestino.Name = "codAlmacenDestino";
		this.codAlmacenDestino.ReadOnly = true;
		this.codAlmacenDestino.Visible = false;
		this.almacendestino.DataPropertyName = "almacendestino";
		this.almacendestino.HeaderText = "Almacen Destino";
		this.almacendestino.Name = "almacendestino";
		this.almacendestino.ReadOnly = true;
		this.almacendestino.Width = 200;
		this.DecripcionRechazo.DataPropertyName = "decripcionRechazo";
		this.DecripcionRechazo.HeaderText = "DecripcionRechazo";
		this.DecripcionRechazo.Name = "DecripcionRechazo";
		this.DecripcionRechazo.ReadOnly = true;
		this.DecripcionRechazo.Width = 220;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Usuario";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.CodTDoc.DataPropertyName = "codTipoDocumento";
		this.CodTDoc.HeaderText = "CodTDoc";
		this.CodTDoc.Name = "CodTDoc";
		this.CodTDoc.ReadOnly = true;
		this.CodTDoc.Visible = false;
		this.Sigla.DataPropertyName = "sigla";
		this.Sigla.HeaderText = "Sigla";
		this.Sigla.Name = "Sigla";
		this.Sigla.ReadOnly = true;
		this.Sigla.Visible = false;
		this.Sigla.Width = 50;
		this.documento.DataPropertyName = "descripcion";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle1;
		this.documento.HeaderText = "TipoDoc.";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Visible = false;
		this.Moneda.DataPropertyName = "moneda";
		this.Moneda.HeaderText = "Moneda";
		this.Moneda.Name = "Moneda";
		this.Moneda.ReadOnly = true;
		this.Moneda.Visible = false;
		this.TipoCambio.DataPropertyName = "tipocambio";
		this.TipoCambio.HeaderText = "TipoCambio";
		this.TipoCambio.Name = "TipoCambio";
		this.TipoCambio.ReadOnly = true;
		this.TipoCambio.Visible = false;
		this.montodes.DataPropertyName = "montodscto";
		this.montodes.HeaderText = "montodes";
		this.montodes.Name = "montodes";
		this.montodes.ReadOnly = true;
		this.montodes.Visible = false;
		this.igv.DataPropertyName = "igv";
		this.igv.HeaderText = "igv";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Visible = false;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "fechapago";
		this.fechapago.Name = "fechapago";
		this.fechapago.ReadOnly = true;
		this.fechapago.Visible = false;
		this.codUsuario.DataPropertyName = "codUsuario";
		this.codUsuario.HeaderText = "codUsuario";
		this.codUsuario.Name = "codUsuario";
		this.codUsuario.ReadOnly = true;
		this.codUsuario.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Registro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.codAutorizado.DataPropertyName = "codAutorizado";
		this.codAutorizado.HeaderText = "codAutorizado";
		this.codAutorizado.Name = "codAutorizado";
		this.codAutorizado.ReadOnly = true;
		this.codAutorizado.Visible = false;
		this.codListaPrecio.DataPropertyName = "codListaPrecio";
		this.codListaPrecio.HeaderText = "codListaPrecio";
		this.codListaPrecio.Name = "codListaPrecio";
		this.codListaPrecio.ReadOnly = true;
		this.codListaPrecio.Visible = false;
		this.formapago.DataPropertyName = "formapago";
		this.formapago.HeaderText = "formapago";
		this.formapago.Name = "formapago";
		this.formapago.ReadOnly = true;
		this.formapago.Visible = false;
		this.comentario.DataPropertyName = "comentario";
		this.comentario.HeaderText = "comentario";
		this.comentario.Name = "comentario";
		this.comentario.ReadOnly = true;
		this.comentario.Visible = false;
		this.fecha.DataPropertyName = "fechaenvio";
		this.fecha.HeaderText = "Fecha Transf.";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 90;
		this.fechaentrega.DataPropertyName = "fechaentrega";
		this.fechaentrega.HeaderText = "Fecha Acepta/Rechaza";
		this.fechaentrega.Name = "fechaentrega";
		this.fechaentrega.ReadOnly = true;
		this.fechaentrega.Width = 150;
		this.Bruto.DataPropertyName = "bruto";
		this.Bruto.HeaderText = "Bruto";
		this.Bruto.Name = "Bruto";
		this.Bruto.ReadOnly = true;
		this.Bruto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Bruto.Width = 80;
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle2;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.Width = 80;
		this.Total.DataPropertyName = "total";
		this.Total.HeaderText = "Total";
		this.Total.Name = "Total";
		this.Total.ReadOnly = true;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "exit.png");
		this.imageList2.Images.SetKeyName(1, "pedido.png");
		this.imageList2.Images.SetKeyName(2, "carrito.png");
		this.imageList2.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList2.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList2.Images.SetKeyName(5, "document_delete.png");
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(1, 490);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(310, 20);
		this.txtCodprod.TabIndex = 34;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(-2, 474);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(112, 13);
		this.label2.TabIndex = 35;
		this.label2.Text = "Busqueda x Producto:";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(357, 490);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 37;
		this.label3.Text = "Prod.:";
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(398, 490);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 38;
		this.txtNombreProducto.Text = "---";
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.ImageIndex = 11;
		this.btnBuscarProducto.ImageList = this.imageList1;
		this.btnBuscarProducto.Location = new System.Drawing.Point(317, 480);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 36;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList2;
		this.btnSalir.Location = new System.Drawing.Point(1052, 474);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 33;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1139, 520);
		base.Controls.Add(this.txtNombreProducto);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.btnBuscarProducto);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.txtCodprod);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "FrmTransferenciaLista";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Transferencias";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(FrmTransferenciaLista_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTransferenciasPendientes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
