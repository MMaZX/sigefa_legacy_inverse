using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmRecibos : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsRecibos recibos = new clsRecibos();

	private clsAdmRecibo AdmRecibos = new clsAdmRecibo();

	public List<int> seleccion = new List<int>();

	private decimal xsustentar = default(decimal);

	private decimal sustentado = default(decimal);

	private decimal Saldo = default(decimal);

	public int tipocaja = 0;

	private IContainer components = null;

	private RibbonBar ribbonBar1;

	private ImageList imageList1;

	private ButtonItem biEditar;

	private ButtonItem biActualizar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimir;

	private DataGridView dgvRecibos;

	private Button btnSalir;

	private Panel panel1;

	private Panel panel2;

	private ImageList imageList2;

	private Button btnExit;

	private DateTimePicker dtpfecha1;

	private DateTimePicker dtpfecha2;

	private Label label2;

	private Label label3;

	private Panel panel4;

	private ExpandablePanel expandablePanel1;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label lblColumna;

	private TextBox txtFiltro;

	private Button btnclose;

	private Label lblProperty;

	private Panel panel6;

	private Panel panel3;

	private Label lblSaldoCaja;

	private Label lblAperturaCaja;

	private Label lblEgresos;

	private Label lblIngresos;

	private Label label6;

	private Label label7;

	private Label label5;

	private Label label4;

	private ButtonItem biAnular;

	private Label txtxsustentar;

	private Label txtsustentado;

	private Label label12;

	private Label label13;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn concepto;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn serie;

	private DataGridViewTextBoxColumn numeracion;

	private DataGridViewTextBoxColumn numDocumento;

	private DataGridViewTextBoxColumn montopendiente;

	private DataGridViewTextBoxColumn montorendido;

	private DataGridViewTextBoxColumn sustentado1;

	private DataGridViewTextBoxColumn anulado;

	private Label lblTotal;

	private Label label11;

	public frmRecibos()
	{
		InitializeComponent();
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void biActualizar_Click(object sender, EventArgs e)
	{
		VerificaSaldoCaja();
		RecibosFechas();
	}

	private void RecibosFechas()
	{
		dgvRecibos.DataSource = data;
		data.DataSource = AdmRecibos.ListaRecibos(frmLogin.iCodSucursal, dtpfecha1.Value.Date, dtpfecha2.Value.Date, tipocaja);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvRecibos.ClearSelection();
	}

	private void VerificaSaldoCaja()
	{
	}

	private void frmCajaChica_Load(object sender, EventArgs e)
	{
		RecibosFechas();
		VerificaSaldoCaja();
	}

	private void dtpfecha1_Leave(object sender, EventArgs e)
	{
		dtpfecha2.MinDate = dtpfecha1.Value;
	}

	private void dtpfecha2_Leave(object sender, EventArgs e)
	{
		dtpfecha1.MaxDate = dtpfecha2.Value;
	}

	private void dtpfecha1_ValueChanged(object sender, EventArgs e)
	{
		RecibosFechas();
	}

	private void dtpfecha2_ValueChanged(object sender, EventArgs e)
	{
		RecibosFechas();
	}

	private void biEditar_Click(object sender, EventArgs e)
	{
		if (dgvRecibos.Rows.Count > 0 && dgvRecibos.SelectedRows.Count > 0)
		{
			if (Convert.ToDecimal(dgvRecibos.SelectedRows[0].Cells[monto.Name].Value) == Convert.ToDecimal(dgvRecibos.SelectedRows[0].Cells[montopendiente.Name].Value.ToString()))
			{
				frmRecibos_CajaChica frm = new frmRecibos_CajaChica();
				frm.Proceso = 2;
				frm.txtDescripcion.Text = dgvRecibos.SelectedRows[0].Cells[concepto.Name].Value.ToString();
				frm.txtMonto.Text = dgvRecibos.SelectedRows[0].Cells[monto.Name].Value.ToString();
				frm.dtpFecha.Value = Convert.ToDateTime(dgvRecibos.SelectedRows[0].Cells[fecha.Name].Value.ToString());
				frm.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
				frm.tipocaja = tipocaja;
				frm.ShowDialog();
				RecibosFechas();
				VerificaSaldoCaja();
			}
			else
			{
				MessageBox.Show("No se Puede Editar, Ya tiene un Monto sustentado");
			}
		}
	}

	private void dgvMovimientosCajaChica_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			frmRecibos_CajaChica frm = new frmRecibos_CajaChica();
			frm.Proceso = 3;
			frm.txtDescripcion.Text = dgvRecibos.SelectedRows[0].Cells[concepto.Name].Value.ToString();
			frm.txtMonto.Text = dgvRecibos.SelectedRows[0].Cells[monto.Name].Value.ToString();
			frm.dtpFecha.Value = Convert.ToDateTime(dgvRecibos.SelectedRows[0].Cells[fecha.Name].Value.ToString());
			frm.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
			frm.ShowDialog();
		}
	}

	private void CalculoSaldo()
	{
		try
		{
			xsustentar = default(decimal);
			sustentado = default(decimal);
			foreach (DataGridViewRow row in (IEnumerable)dgvRecibos.Rows)
			{
				xsustentar += Convert.ToDecimal(row.Cells[montopendiente.Name].Value);
				sustentado += Convert.ToDecimal(row.Cells[montorendido.Name].Value);
			}
			txtxsustentar.Text = $"{xsustentar.ToString():#,##0.00}";
			txtsustentado.Text = $"{sustentado.ToString():#,##0.00}";
			lblTotal.Text = $"{(xsustentar + sustentado).ToString():#,##0.00}";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dgvMovimientosCajaChica_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		CalculoSaldo();
	}

	private void dgvMovimientosCajaChica_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		CalculoSaldo();
	}

	private void dgvMovimientosCajaChica_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		if (dgvRecibos.IsCurrentCellDirty)
		{
			dgvRecibos.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
	}

	private void biBuscar_Click(object sender, EventArgs e)
	{
		lblColumna.Text = "CONCEPTO";
		lblProperty.Text = "concepto";
		if (!expandablePanel1.Expanded)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
		else
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void btnclose_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
	}

	private void dgvMovimientosCajaChica_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvRecibos.Columns[e.ColumnIndex].Index > 0)
		{
			lblColumna.Text = dgvRecibos.Columns[e.ColumnIndex].HeaderText;
			lblProperty.Text = dgvRecibos.Columns[e.ColumnIndex].DataPropertyName;
			if (expandablePanel1.Expanded)
			{
				txtFiltro.Focus();
			}
		}
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{lblColumna.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void biImprimir_Click(object sender, EventArgs e)
	{
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
	}

	private void biAnular_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRecibos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.biAnular = new DevComponents.DotNetBar.ButtonItem();
		this.dgvRecibos = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montopendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montorendido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sustentado1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnSalir = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpfecha1 = new System.Windows.Forms.DateTimePicker();
		this.dtpfecha2 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.btnExit = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel4 = new System.Windows.Forms.Panel();
		this.txtxsustentar = new System.Windows.Forms.Label();
		this.txtsustentado = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lblSaldoCaja = new System.Windows.Forms.Label();
		this.lblAperturaCaja = new System.Windows.Forms.Label();
		this.lblEgresos = new System.Windows.Forms.Label();
		this.lblIngresos = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.lblProperty = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.lblColumna = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnclose = new System.Windows.Forms.Button();
		this.panel6 = new System.Windows.Forms.Panel();
		this.lblTotal = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dgvRecibos).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel3.SuspendLayout();
		this.expandablePanel1.SuspendLayout();
		base.SuspendLayout();
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[5] { this.biEditar, this.biBuscar, this.biImprimir, this.biActualizar, this.biAnular });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(591, 65);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 8;
		this.ribbonBar1.Text = "ribbonBar1";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
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
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList1.Images.SetKeyName(19, "egreso.png");
		this.imageList1.Images.SetKeyName(20, "ingreso.png");
		this.imageList1.Images.SetKeyName(21, "icon_shelfs.png");
		this.biEditar.ImageIndex = 3;
		this.biEditar.ImagePaddingHorizontal = 10;
		this.biEditar.ImagePaddingVertical = 15;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Editar";
		this.biEditar.Click += new System.EventHandler(biEditar_Click);
		this.biBuscar.ImageIndex = 11;
		this.biBuscar.ImagePaddingHorizontal = 10;
		this.biBuscar.ImagePaddingVertical = 15;
		this.biBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscar.Name = "biBuscar";
		this.biBuscar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlB);
		this.biBuscar.SubItemsExpandWidth = 14;
		this.biBuscar.Text = "Buscar";
		this.biBuscar.Click += new System.EventHandler(biBuscar_Click);
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingHorizontal = 10;
		this.biImprimir.ImagePaddingVertical = 15;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Visible = false;
		this.biImprimir.Click += new System.EventHandler(biImprimir_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingHorizontal = 10;
		this.biActualizar.ImagePaddingVertical = 15;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(biActualizar_Click);
		this.biAnular.ImageIndex = 21;
		this.biAnular.ImagePaddingHorizontal = 10;
		this.biAnular.ImagePaddingVertical = 15;
		this.biAnular.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAnular.Name = "biAnular";
		this.biAnular.SubItemsExpandWidth = 14;
		this.biAnular.Text = "Anular";
		this.biAnular.Visible = false;
		this.biAnular.Click += new System.EventHandler(biAnular_Click);
		this.dgvRecibos.AllowUserToAddRows = false;
		this.dgvRecibos.AllowUserToDeleteRows = false;
		this.dgvRecibos.AllowUserToResizeRows = false;
		this.dgvRecibos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvRecibos.Columns.AddRange(this.codigo, this.concepto, this.monto, this.fecha, this.serie, this.numeracion, this.numDocumento, this.montopendiente, this.montorendido, this.sustentado1, this.anulado);
		this.dgvRecibos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvRecibos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvRecibos.Location = new System.Drawing.Point(0, 65);
		this.dgvRecibos.MultiSelect = false;
		this.dgvRecibos.Name = "dgvRecibos";
		this.dgvRecibos.ReadOnly = true;
		this.dgvRecibos.RowHeadersVisible = false;
		this.dgvRecibos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvRecibos.Size = new System.Drawing.Size(1241, 276);
		this.dgvRecibos.TabIndex = 9;
		this.dgvRecibos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvMovimientosCajaChica_CellDoubleClick);
		this.dgvRecibos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvMovimientosCajaChica_ColumnHeaderMouseClick);
		this.dgvRecibos.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvMovimientosCajaChica_RowsAdded);
		this.dgvRecibos.CurrentCellDirtyStateChanged += new System.EventHandler(dgvMovimientosCajaChica_CurrentCellDirtyStateChanged);
		this.dgvRecibos.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvMovimientosCajaChica_RowsRemoved);
		this.codigo.DataPropertyName = "codRecibo";
		this.codigo.HeaderText = "COD";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.codigo.Width = 70;
		this.concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.concepto.DataPropertyName = "concepto";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.concepto.DefaultCellStyle = dataGridViewCellStyle1;
		this.concepto.HeaderText = "CONCEPTO";
		this.concepto.Name = "concepto";
		this.concepto.ReadOnly = true;
		this.concepto.Width = 400;
		this.monto.DataPropertyName = "monto";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
		dataGridViewCellStyle2.Format = "N2";
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.monto.DefaultCellStyle = dataGridViewCellStyle2;
		this.monto.HeaderText = "MONTO";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.Width = 80;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle3;
		this.fecha.HeaderText = "FECHA";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Width = 80;
		this.serie.DataPropertyName = "serie";
		this.serie.HeaderText = "SERIE";
		this.serie.Name = "serie";
		this.serie.ReadOnly = true;
		this.serie.Visible = false;
		this.numeracion.DataPropertyName = "numeracion";
		this.numeracion.HeaderText = "NUMERACIÓN";
		this.numeracion.Name = "numeracion";
		this.numeracion.ReadOnly = true;
		this.numeracion.Visible = false;
		this.numDocumento.DataPropertyName = "numerodocumento";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numDocumento.DefaultCellStyle = dataGridViewCellStyle4;
		this.numDocumento.HeaderText = "N° DOC.";
		this.numDocumento.Name = "numDocumento";
		this.numDocumento.ReadOnly = true;
		this.montopendiente.DataPropertyName = "montopendiente";
		this.montopendiente.HeaderText = "MONTO POR SUSTENTAR";
		this.montopendiente.Name = "montopendiente";
		this.montopendiente.ReadOnly = true;
		this.montorendido.DataPropertyName = "montorendido";
		this.montorendido.HeaderText = "MONTO SUSTENTADO";
		this.montorendido.Name = "montorendido";
		this.montorendido.ReadOnly = true;
		this.sustentado1.DataPropertyName = "sustentado";
		this.sustentado1.HeaderText = "SUSTENTADO";
		this.sustentado1.Name = "sustentado1";
		this.sustentado1.ReadOnly = true;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "ANULADO";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Location = new System.Drawing.Point(1013, 90);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 23);
		this.btnSalir.TabIndex = 10;
		this.btnSalir.Text = "Salir";
		this.btnSalir.UseVisualStyleBackColor = true;
		this.panel1.Controls.Add(this.ribbonBar1);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1241, 65);
		this.panel1.TabIndex = 12;
		this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel2.Controls.Add(this.label3);
		this.panel2.Controls.Add(this.dtpfecha1);
		this.panel2.Controls.Add(this.dtpfecha2);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.btnExit);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(591, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(650, 65);
		this.panel2.TabIndex = 9;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(243, 26);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(14, 13);
		this.label3.TabIndex = 37;
		this.label3.Text = "Y";
		this.dtpfecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha1.Location = new System.Drawing.Point(143, 22);
		this.dtpfecha1.Name = "dtpfecha1";
		this.dtpfecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpfecha1.TabIndex = 13;
		this.dtpfecha1.ValueChanged += new System.EventHandler(dtpfecha1_ValueChanged);
		this.dtpfecha1.Leave += new System.EventHandler(dtpfecha1_Leave);
		this.dtpfecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha2.Location = new System.Drawing.Point(260, 22);
		this.dtpfecha2.Name = "dtpfecha2";
		this.dtpfecha2.Size = new System.Drawing.Size(99, 20);
		this.dtpfecha2.TabIndex = 14;
		this.dtpfecha2.ValueChanged += new System.EventHandler(dtpfecha2_ValueChanged);
		this.dtpfecha2.Leave += new System.EventHandler(dtpfecha2_Leave);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(43, 26);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(94, 13);
		this.label2.TabIndex = 36;
		this.label2.Text = "BUSCAR ENTRE:";
		this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExit.ImageIndex = 0;
		this.btnExit.ImageList = this.imageList2;
		this.btnExit.Location = new System.Drawing.Point(568, 7);
		this.btnExit.Name = "btnExit";
		this.btnExit.Size = new System.Drawing.Size(70, 50);
		this.btnExit.TabIndex = 35;
		this.btnExit.Text = "Salir";
		this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExit.UseVisualStyleBackColor = true;
		this.btnExit.Click += new System.EventHandler(btnExit_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
		this.panel4.Controls.Add(this.txtxsustentar);
		this.panel4.Controls.Add(this.txtsustentado);
		this.panel4.Controls.Add(this.label12);
		this.panel4.Controls.Add(this.label13);
		this.panel4.Controls.Add(this.panel3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 351);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(1241, 70);
		this.panel4.TabIndex = 14;
		this.txtxsustentar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtxsustentar.BackColor = System.Drawing.Color.Transparent;
		this.txtxsustentar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.txtxsustentar.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtxsustentar.ForeColor = System.Drawing.Color.DarkRed;
		this.txtxsustentar.Location = new System.Drawing.Point(269, 40);
		this.txtxsustentar.Name = "txtxsustentar";
		this.txtxsustentar.Size = new System.Drawing.Size(100, 20);
		this.txtxsustentar.TabIndex = 48;
		this.txtxsustentar.Text = "0.000";
		this.txtxsustentar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtsustentado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtsustentado.BackColor = System.Drawing.Color.Transparent;
		this.txtsustentado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.txtsustentado.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtsustentado.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.txtsustentado.Location = new System.Drawing.Point(269, 14);
		this.txtsustentado.Name = "txtsustentado";
		this.txtsustentado.Size = new System.Drawing.Size(100, 20);
		this.txtsustentado.TabIndex = 47;
		this.txtsustentado.Text = "0.000";
		this.txtsustentado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Location = new System.Drawing.Point(103, 44);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(160, 13);
		this.label12.TabIndex = 46;
		this.label12.Text = "MONTO POR SUSTENTAR S/:";
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.Color.Transparent;
		this.label13.Location = new System.Drawing.Point(121, 18);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(142, 13);
		this.label13.TabIndex = 45;
		this.label13.Text = "MONTO SUSTENTADO S/:";
		this.panel3.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.panel3.Controls.Add(this.lblTotal);
		this.panel3.Controls.Add(this.label11);
		this.panel3.Controls.Add(this.lblSaldoCaja);
		this.panel3.Controls.Add(this.lblAperturaCaja);
		this.panel3.Controls.Add(this.lblEgresos);
		this.panel3.Controls.Add(this.lblIngresos);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.label7);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.label4);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel3.Location = new System.Drawing.Point(591, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(650, 70);
		this.panel3.TabIndex = 13;
		this.lblSaldoCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblSaldoCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblSaldoCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblSaldoCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSaldoCaja.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblSaldoCaja.Location = new System.Drawing.Point(543, 37);
		this.lblSaldoCaja.Name = "lblSaldoCaja";
		this.lblSaldoCaja.Size = new System.Drawing.Size(100, 20);
		this.lblSaldoCaja.TabIndex = 46;
		this.lblSaldoCaja.Text = "0.000";
		this.lblSaldoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblSaldoCaja.Visible = false;
		this.lblAperturaCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblAperturaCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblAperturaCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblAperturaCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAperturaCaja.ForeColor = System.Drawing.SystemColors.WindowText;
		this.lblAperturaCaja.Location = new System.Drawing.Point(543, 11);
		this.lblAperturaCaja.Name = "lblAperturaCaja";
		this.lblAperturaCaja.Size = new System.Drawing.Size(100, 20);
		this.lblAperturaCaja.TabIndex = 45;
		this.lblAperturaCaja.Text = "0.000";
		this.lblAperturaCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblAperturaCaja.Visible = false;
		this.lblEgresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblEgresos.BackColor = System.Drawing.Color.Transparent;
		this.lblEgresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblEgresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEgresos.ForeColor = System.Drawing.Color.DarkRed;
		this.lblEgresos.Location = new System.Drawing.Point(90, 36);
		this.lblEgresos.Name = "lblEgresos";
		this.lblEgresos.Size = new System.Drawing.Size(100, 20);
		this.lblEgresos.TabIndex = 44;
		this.lblEgresos.Text = "0.000";
		this.lblEgresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblEgresos.Visible = false;
		this.lblIngresos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblIngresos.BackColor = System.Drawing.Color.Transparent;
		this.lblIngresos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblIngresos.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblIngresos.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblIngresos.Location = new System.Drawing.Point(90, 10);
		this.lblIngresos.Name = "lblIngresos";
		this.lblIngresos.Size = new System.Drawing.Size(100, 20);
		this.lblIngresos.TabIndex = 43;
		this.lblIngresos.Text = "0.000";
		this.lblIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblIngresos.Visible = false;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Location = new System.Drawing.Point(7, 40);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(77, 13);
		this.label6.TabIndex = 42;
		this.label6.Text = "EGRESOS S/:";
		this.label6.Visible = false;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(3, 14);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(81, 13);
		this.label7.TabIndex = 41;
		this.label7.Text = "INGRESOS S/:";
		this.label7.Visible = false;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(476, 41);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(61, 13);
		this.label5.TabIndex = 40;
		this.label5.Text = "SALDO S/:";
		this.label5.Visible = false;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(424, 15);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(113, 13);
		this.label4.TabIndex = 37;
		this.label4.Text = "APERTURA CAJA S/:";
		this.label4.Visible = false;
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.lblProperty);
		this.expandablePanel1.Controls.Add(this.label8);
		this.expandablePanel1.Controls.Add(this.label9);
		this.expandablePanel1.Controls.Add(this.label10);
		this.expandablePanel1.Controls.Add(this.lblColumna);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.btnclose);
		this.expandablePanel1.ExpandButtonVisible = false;
		this.expandablePanel1.Expanded = false;
		this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(1004, 0, 231, 93);
		this.expandablePanel1.Location = new System.Drawing.Point(1004, 0);
		this.expandablePanel1.Name = "expandablePanel1";
		this.expandablePanel1.ShowFocusRectangle = true;
		this.expandablePanel1.Size = new System.Drawing.Size(231, 0);
		this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.Style.BackColor1.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.BackColor2.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarPopupBorder;
		this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
		this.expandablePanel1.Style.GradientAngle = 90;
		this.expandablePanel1.TabIndex = 19;
		this.expandablePanel1.TitleHeight = 0;
		this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
		this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.expandablePanel1.TitleStyle.GradientAngle = 90;
		this.expandablePanel1.TitleText = "Title Bar";
		this.expandablePanel1.Visible = false;
		this.lblProperty.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblProperty.AutoSize = true;
		this.lblProperty.ForeColor = System.Drawing.Color.LightBlue;
		this.lblProperty.Location = new System.Drawing.Point(210, -59);
		this.lblProperty.Name = "lblProperty";
		this.lblProperty.Size = new System.Drawing.Size(12, 13);
		this.lblProperty.TabIndex = 11;
		this.lblProperty.Text = "x";
		this.lblProperty.Visible = false;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(10, -59);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(29, 13);
		this.label8.TabIndex = 10;
		this.label8.Text = "Por :";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Lucida Sans", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(5, -89);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(62, 12);
		this.label9.TabIndex = 9;
		this.label9.Text = "Busqueda";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.ForeColor = System.Drawing.Color.LightBlue;
		this.label10.Location = new System.Drawing.Point(186, -59);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(12, 13);
		this.label10.TabIndex = 7;
		this.label10.Text = "x";
		this.label10.Visible = false;
		this.lblColumna.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblColumna.AutoSize = true;
		this.lblColumna.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblColumna.Location = new System.Drawing.Point(45, -59);
		this.lblColumna.Name = "lblColumna";
		this.lblColumna.Size = new System.Drawing.Size(15, 13);
		this.lblColumna.TabIndex = 6;
		this.lblColumna.Text = "X";
		this.txtFiltro.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFiltro.Location = new System.Drawing.Point(13, -38);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 5;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnclose.BackColor = System.Drawing.Color.Transparent;
		this.btnclose.FlatAppearance.BorderSize = 0;
		this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnclose.Location = new System.Drawing.Point(213, -93);
		this.btnclose.Margin = new System.Windows.Forms.Padding(1);
		this.btnclose.Name = "btnclose";
		this.btnclose.Size = new System.Drawing.Size(18, 22);
		this.btnclose.TabIndex = 3;
		this.btnclose.Text = "x";
		this.btnclose.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnclose.UseVisualStyleBackColor = false;
		this.btnclose.Click += new System.EventHandler(btnclose_Click);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 341);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(1241, 10);
		this.panel6.TabIndex = 20;
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTotal.BackColor = System.Drawing.Color.Transparent;
		this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblTotal.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblTotal.Location = new System.Drawing.Point(301, 14);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(100, 20);
		this.lblTotal.TabIndex = 48;
		this.lblTotal.Text = "0.000";
		this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.Location = new System.Drawing.Point(234, 18);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(60, 13);
		this.label11.TabIndex = 47;
		this.label11.Text = "TOTAL S/:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1241, 421);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.dgvRecibos);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.btnSalir);
		this.DoubleBuffered = true;
		base.Name = "frmRecibos";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Recibos";
		base.Load += new System.EventHandler(frmCajaChica_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvRecibos).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
