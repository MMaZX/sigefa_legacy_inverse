using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;

namespace SIGEFA.Formularios;

public class frmPagosDetraccion : Office2007Form
{
	private clsAdmPago Admpag = new clsAdmPago();

	private int Cod = 0;

	private int aprob = 0;

	private int cbestado;

	private DataTable mySource;

	private IContainer components = null;

	private ImageList imageList2;

	private ImageList imageList1;

	private Panel panel1;

	private RibbonBar ribbonBar1;

	private ButtonItem biAprobar;

	private ButtonItem biDesaprobar;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem tsmiRendicion;

	private DataGridView dgvDetalle;

	private Label label4;

	private Label label2;

	private Label label1;

	private ComboBox cmbEstado;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private Button btnBusqueda;

	private BackgroundWorker backAprobacion;

	private DataGridViewTextBoxColumn DOCUMENTO;

	private DataGridViewTextBoxColumn proveedor;

	private DataGridViewTextBoxColumn MONTOCOBRADO;

	private DataGridViewTextBoxColumn MONEDA;

	private DataGridViewTextBoxColumn OBSERVACION;

	private DataGridViewTextBoxColumn Aprobado1;

	private DataGridViewTextBoxColumn codPago;

	public frmPagosDetraccion()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cargaLista()
	{
		dgvDetalle.DataSource = Admpag.MuestraPagosPorAprobar(cmbEstado.SelectedIndex, dtpFecha1.Value, dtpFecha2.Value);
	}

	private void biAprobar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.CurrentRow != null && dgvDetalle.Rows.Count >= 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Aprobar el Pago", "Aprobación de Pagos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No)
			{
				Admpag.AprobarPago(Cod, 2);
				MessageBox.Show("El Pago se Aprobado Satisfactoriamente", "Aprobación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void biDesaprobar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.CurrentRow != null && dgvDetalle.Rows.Count >= 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Aprobar el Pago", "Aprobación de Pagos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No)
			{
				Admpag.AprobarPago(Cod, 3);
				MessageBox.Show("El Pago se Aprobado Satisfactoriamente", "Aprobación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		try
		{
			if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
			{
				aprob = Convert.ToInt32(e.Row.Cells[Aprobado1.Name].Value);
				Cod = Convert.ToInt32(e.Row.Cells[codPago.Name].Value);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "dgvDetalle_RowStateChanged");
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		arrancaBack();
	}

	private void frmAprobacionPagos_Load(object sender, EventArgs e)
	{
		cmbEstado.SelectedIndex = 1;
		EventArgs evar = new EventArgs();
		cmbEstado_SelectionChangeCommitted(cmbEstado, evar);
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (aprob == 1)
		{
			biAprobar.Enabled = true;
			biDesaprobar.Enabled = true;
		}
		else
		{
			biAprobar.Enabled = false;
			biDesaprobar.Enabled = false;
		}
	}

	private void arrancaBack()
	{
		if (!backAprobacion.IsBusy)
		{
			backAprobacion.RunWorkerAsync();
		}
	}

	private void backAprobacion_DoWork(object sender, DoWorkEventArgs e)
	{
		backAprobacionProcessLogicMethod();
	}

	private void backAprobacion_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		DataTable mydatOld = null;
		DataTable mydataResult = null;
		if (dgvDetalle.DataSource != null)
		{
			mydatOld = new DataTable();
			mydatOld = (DataTable)dgvDetalle.DataSource;
			mydataResult = new DataTable();
			mydataResult = getDifferentRecords(mydatOld, mySource);
			if (mydataResult != null && mydataResult.Rows.Count != 0)
			{
				dgvDetalle.AutoGenerateColumns = false;
				dgvDetalle.DataSource = mySource;
				dgvDetalle.ClearSelection();
			}
		}
		else
		{
			dgvDetalle.AutoGenerateColumns = false;
			dgvDetalle.DataSource = mySource;
			dgvDetalle.ClearSelection();
		}
	}

	private DataTable getDifferentRecords(DataTable mydatOld, DataTable mySource)
	{
		DataTable ResultDataTable = new DataTable("ResultDataTable");
		try
		{
			using (DataSet ds = new DataSet())
			{
				ds.Tables.AddRange(new DataTable[2]
				{
					mydatOld.Copy(),
					mySource.Copy()
				});
				DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
				for (int i = 0; i < firstColumns.Length; i++)
				{
					firstColumns[i] = ds.Tables[0].Columns[i];
				}
				DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
				for (int j = 0; j < secondColumns.Length; j++)
				{
					secondColumns[j] = ds.Tables[1].Columns[j];
				}
				DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, createConstraints: false);
				ds.Relations.Add(r1);
				DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, createConstraints: false);
				ds.Relations.Add(r2);
				for (int k = 0; k < mydatOld.Columns.Count; k++)
				{
					ResultDataTable.Columns.Add(mydatOld.Columns[k].ColumnName, mydatOld.Columns[k].DataType);
				}
				ResultDataTable.BeginLoadData();
				foreach (DataRow parentrow in ds.Tables[0].Rows)
				{
					DataRow[] childrows = parentrow.GetChildRows(r1);
					if (childrows == null || childrows.Length == 0)
					{
						ResultDataTable.LoadDataRow(parentrow.ItemArray, fAcceptChanges: true);
					}
				}
				foreach (DataRow parentrow2 in ds.Tables[1].Rows)
				{
					DataRow[] childrows2 = parentrow2.GetChildRows(r2);
					if (childrows2 == null || childrows2.Length == 0)
					{
						ResultDataTable.LoadDataRow(parentrow2.ItemArray, fAcceptChanges: true);
					}
				}
				ResultDataTable.EndLoadData();
			}
			return ResultDataTable;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return null;
		}
	}

	private void backAprobacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		backAprobacion.RunWorkerAsync();
	}

	private void backAprobacionProcessLogicMethod()
	{
		Thread.Sleep(4000);
		mySource = Admpag.MuestraPagosDetraccion(cbestado, dtpFecha1.Value, dtpFecha2.Value);
		foreach (DataRow row in mySource.Rows)
		{
			backAprobacion.ReportProgress(mySource.Rows.IndexOf(row));
		}
	}

	private void cmbEstado_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cbestado = cmbEstado.SelectedIndex;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPagosDetraccion));
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.biAprobar = new DevComponents.DotNetBar.ButtonItem();
		this.biDesaprobar = new DevComponents.DotNetBar.ButtonItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.tsmiRendicion = new System.Windows.Forms.ToolStripMenuItem();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.DOCUMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.MONTOCOBRADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.MONEDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.OBSERVACION = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Aprobado1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.backAprobacion = new System.ComponentModel.BackgroundWorker();
		this.panel1.SuspendLayout();
		this.ribbonBar1.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		base.SuspendLayout();
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
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
		this.imageList1.Images.SetKeyName(22, "EXIT2.png");
		this.panel1.Controls.Add(this.ribbonBar1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1164, 77);
		this.panel1.TabIndex = 13;
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Controls.Add(this.btnBusqueda);
		this.ribbonBar1.Controls.Add(this.label4);
		this.ribbonBar1.Controls.Add(this.label2);
		this.ribbonBar1.Controls.Add(this.label1);
		this.ribbonBar1.Controls.Add(this.cmbEstado);
		this.ribbonBar1.Controls.Add(this.dtpFecha2);
		this.ribbonBar1.Controls.Add(this.dtpFecha1);
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biAprobar, this.biDesaprobar });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(1164, 77);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 8;
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList1;
		this.btnBusqueda.Location = new System.Drawing.Point(676, 28);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(98, 33);
		this.btnBusqueda.TabIndex = 47;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(501, 20);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 12);
		this.label4.TabIndex = 42;
		this.label4.Text = "Estado";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(414, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 41;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(327, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 40;
		this.label1.Text = "Desde";
		this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[2] { "CANCELADOS", "PENDIENTES" });
		this.cmbEstado.Location = new System.Drawing.Point(504, 36);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(121, 20);
		this.cmbEstado.TabIndex = 39;
		this.cmbEstado.SelectionChangeCommitted += new System.EventHandler(cmbEstado_SelectionChangeCommitted);
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(417, 36);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 38;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(330, 36);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 37;
		this.biAprobar.Enabled = false;
		this.biAprobar.Image = (System.Drawing.Image)resources.GetObject("biAprobar.Image");
		this.biAprobar.ImageIndex = 20;
		this.biAprobar.ImagePaddingHorizontal = 30;
		this.biAprobar.ImagePaddingVertical = 15;
		this.biAprobar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAprobar.Name = "biAprobar";
		this.biAprobar.SubItemsExpandWidth = 14;
		this.biAprobar.Text = "Aprobar";
		this.biAprobar.Click += new System.EventHandler(biAprobar_Click);
		this.biDesaprobar.Enabled = false;
		this.biDesaprobar.Image = (System.Drawing.Image)resources.GetObject("biDesaprobar.Image");
		this.biDesaprobar.ImageIndex = 19;
		this.biDesaprobar.ImagePaddingHorizontal = 20;
		this.biDesaprobar.ImagePaddingVertical = 15;
		this.biDesaprobar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biDesaprobar.Name = "biDesaprobar";
		this.biDesaprobar.SubItemsExpandWidth = 14;
		this.biDesaprobar.Text = "Desaprobar";
		this.biDesaprobar.Click += new System.EventHandler(biDesaprobar_Click);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.tsmiRendicion });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(174, 26);
		this.tsmiRendicion.Image = (System.Drawing.Image)resources.GetObject("tsmiRendicion.Image");
		this.tsmiRendicion.Name = "tsmiRendicion";
		this.tsmiRendicion.Size = new System.Drawing.Size(173, 22);
		this.tsmiRendicion.Text = "Eliminar Rendicion";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvDetalle.Columns.AddRange(this.DOCUMENTO, this.proveedor, this.MONTOCOBRADO, this.MONEDA, this.OBSERVACION, this.Aprobado1, this.codPago);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvDetalle.Location = new System.Drawing.Point(0, 77);
		this.dgvDetalle.MultiSelect = false;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1164, 315);
		this.dgvDetalle.TabIndex = 18;
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.DOCUMENTO.DataPropertyName = "documento";
		this.DOCUMENTO.HeaderText = "DOCUMENTO";
		this.DOCUMENTO.Name = "DOCUMENTO";
		this.DOCUMENTO.ReadOnly = true;
		this.proveedor.DataPropertyName = "razonsocial";
		this.proveedor.HeaderText = "PROVEEDOR";
		this.proveedor.Name = "proveedor";
		this.proveedor.ReadOnly = true;
		this.proveedor.Width = 300;
		this.MONTOCOBRADO.DataPropertyName = "montopagado";
		this.MONTOCOBRADO.HeaderText = "MONTO";
		this.MONTOCOBRADO.Name = "MONTOCOBRADO";
		this.MONTOCOBRADO.ReadOnly = true;
		this.MONEDA.DataPropertyName = "moneda";
		this.MONEDA.HeaderText = "MONEDA";
		this.MONEDA.Name = "MONEDA";
		this.MONEDA.ReadOnly = true;
		this.OBSERVACION.DataPropertyName = "observacion";
		this.OBSERVACION.HeaderText = "OBSERVACION";
		this.OBSERVACION.Name = "OBSERVACION";
		this.OBSERVACION.ReadOnly = true;
		this.OBSERVACION.Width = 200;
		this.Aprobado1.DataPropertyName = "Aprobado";
		this.Aprobado1.HeaderText = "Aprobado1";
		this.Aprobado1.Name = "Aprobado1";
		this.Aprobado1.ReadOnly = true;
		this.Aprobado1.Visible = false;
		this.codPago.DataPropertyName = "codPago";
		this.codPago.HeaderText = "codPago";
		this.codPago.Name = "codPago";
		this.codPago.ReadOnly = true;
		this.codPago.Visible = false;
		this.backAprobacion.WorkerReportsProgress = true;
		this.backAprobacion.WorkerSupportsCancellation = true;
		this.backAprobacion.DoWork += new System.ComponentModel.DoWorkEventHandler(backAprobacion_DoWork);
		this.backAprobacion.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backAprobacion_RunWorkerCompleted);
		this.backAprobacion.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backAprobacion_ProgressChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1164, 392);
		base.Controls.Add(this.dgvDetalle);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmPagosDetraccion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Aprobacion Pagos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmAprobacionPagos_Load);
		this.panel1.ResumeLayout(false);
		this.ribbonBar1.ResumeLayout(false);
		this.ribbonBar1.PerformLayout();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
