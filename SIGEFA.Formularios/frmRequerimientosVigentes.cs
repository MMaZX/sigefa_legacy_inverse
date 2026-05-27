using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmRequerimientosVigentes : Office2007Form
{
	private clsAlmacen alma = new clsAlmacen();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmRequerimiento AdmReq = new clsAdmRequerimiento();

	private clsRequerimiento Req = new clsRequerimiento();

	public int Proceso = 0;

	private clsAdmAlmacen Admalmac = new clsAdmAlmacen();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int tipo = 0;

	public int almadest = 0;

	private int tip = 0;

	private string AtendidoReq = "";

	public string comentario = "";

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvOrdenes;

	private Button btnSalir;

	private Button btnIrCotizacion;

	private ImageList imageList1;

	private Button btnAnular;

	private ImageList imageList2;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private ImageList imageList3;

	private Button btnConsultar;

	private Label lbalmacen;

	private ComboBox cmbAlmacen;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private Label label1;

	private Label label7;

	private Label label10;

	private TextBox txtFiltro;

	private Label label2;

	private Button btnEnviar;

	private Button btnrechazar;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn Documento;

	private DataGridViewTextBoxColumn serie;

	private DataGridViewTextBoxColumn numeracion;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn Almacen;

	private DataGridViewTextBoxColumn AlmacenD;

	private DataGridViewTextBoxColumn codAlmacen;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn Atendido;

	private DataGridViewTextBoxColumn Estado;

	private DataGridViewTextBoxColumn almacendestino;

	private DataGridViewTextBoxColumn ComentarioRechazado;

	private Button btnAtendido;

	public frmRequerimientosVigentes()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		dgvOrdenes.DataSource = data;
		data.DataSource = AdmReq.MuestraRequerimiento(frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	public void CargaListaHistorial(int alma)
	{
		dgvOrdenes.DataSource = data;
		data.DataSource = AdmReq.MuestraRequerimientoHistorial(alma, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void btnIrCotizacion_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && dgvOrdenes.CurrentRow != null)
		{
			DataGridViewRow row = dgvOrdenes.CurrentRow;
			if (Application.OpenForms["frmRequerimiento"] != null)
			{
				Application.OpenForms["frmRequerimiento"].Close();
				return;
			}
			frmRequerimiento form = new frmRequerimiento();
			form.MdiParent = base.MdiParent;
			form.CodRequer = Req.CodRequerimiento;
			form.Proceso = 3;
			form.Tipo = tipo;
			form.Show();
		}
	}

	private void dgvCotizaciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && e.Row.Selected)
		{
			Req.CodRequerimiento = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			Req.CodAlmacen = Convert.ToInt32(e.Row.Cells[codAlmacen.Name].Value);
			Req.codAlmaDestino = Convert.ToInt32(e.Row.Cells[almacendestino.Name].Value);
			AtendidoReq = e.Row.Cells[Atendido.Name].Value.ToString();
		}
	}

	private void dgvCotizaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && dgvOrdenes.CurrentRow != null)
		{
			DataGridViewRow row = dgvOrdenes.CurrentRow;
			if (Application.OpenForms["frmRequerimiento"] != null)
			{
				Application.OpenForms["frmRequerimiento"].Close();
				return;
			}
			frmRequerimiento form = new frmRequerimiento();
			form.MdiParent = base.MdiParent;
			form.CodRequer = Req.CodRequerimiento;
			form.Proceso = 3;
			form.Tipo = tipo;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.CurrentRow == null || Req.CodRequerimiento == 0)
		{
			return;
		}
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el Requerimiento seleccionado", "Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult != DialogResult.No && AdmReq.anular(Req.CodRequerimiento))
		{
			MessageBox.Show("El requerimiento ha sido anulado correctamente", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			if (tipo == 1)
			{
				CargaLista();
			}
			else if (tipo == 2)
			{
				CargaListaHistorial(frmLogin.iCodAlmacen);
			}
		}
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		if (tipo == 1)
		{
			CargaLista();
		}
		if (tipo == 2)
		{
			if (Convert.ToInt32(cmbAlmacen.SelectedValue) > 0)
			{
				CargaListaHistorial(frmLogin.iCodAlmacen);
			}
			else
			{
				CargaListaHistorial(frmLogin.iCodAlmacen);
			}
		}
		if (Req.codAlmaDestino == frmLogin.iCodAlmacen)
		{
			btnrechazar.Visible = true;
		}
		else
		{
			btnrechazar.Visible = false;
		}
	}

	private void frmRequerimientosVigentes_Load(object sender, EventArgs e)
	{
		if (tipo == 1)
		{
			Estado.Visible = false;
			Atendido.Visible = false;
			lbalmacen.Visible = false;
			cmbAlmacen.Visible = false;
			btnAtendido.Visible = false;
			btnrechazar.Visible = false;
			btnAnular.Visible = true;
			ComentarioRechazado.Visible = false;
			CargaLista();
		}
		else if (tipo == 2)
		{
			CargaListaHistorial(frmLogin.iCodAlmacen);
			cargaAlmacenes();
			Estado.Visible = true;
			Atendido.Visible = true;
			lbalmacen.Visible = true;
			cmbAlmacen.Visible = true;
			btnAtendido.Visible = true;
			btnAnular.Visible = false;
		}
	}

	private void cargaAlmacenes()
	{
		cmbAlmacen.DataSource = Admalmac.ListaAlmacen2();
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		alma = admalma.CargaAlmacen(frmLogin.iCodAlmacen);
		if (Convert.ToBoolean(alma.EstadoPrincipal))
		{
			cmbAlmacen.SelectedIndex = -1;
			cmbAlmacen.Enabled = false;
		}
		else
		{
			cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
			cmbAlmacen.Enabled = false;
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label2.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void dgvOrdenes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvOrdenes.Columns[e.ColumnIndex].HeaderText;
		label2.Text = dgvOrdenes.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (tipo == 1)
		{
			frmSeleccionarAlmacen alma = new frmSeleccionarAlmacen();
			alma.tipo = 2;
			alma.ShowDialog();
			Req.codAlmaDestino = almadest;
			if (dgvOrdenes.CurrentRow != null && Req.codAlmaDestino != 0 && Req.codAlmaDestino != frmLogin.iCodAlmacen)
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea enviar el Borrador Requerimiento seleccionado", "Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No && AdmReq.Envio(Req.codAlmaDestino, Req.CodRequerimiento))
				{
					MessageBox.Show("El Borrador de requerimiento ha envio correctamente", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaLista();
				}
			}
			else
			{
				MessageBox.Show("No Se Puede Enviar, verifique Almacen Destino", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			if (tipo != 2)
			{
				return;
			}
			if (Req.CodAlmacen != frmLogin.iCodAlmacen)
			{
				frmSeleccionarAlmacen alma2 = new frmSeleccionarAlmacen();
				alma2.tipo = 2;
				alma2.ShowDialog();
				Req.codAlmaDestino = almadest;
				if (dgvOrdenes.CurrentRow != null && Req.codAlmaDestino != 0 && Req.codAlmaDestino != frmLogin.iCodAlmacen && Req.codAlmaDestino != Req.CodAlmacen)
				{
					DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea enviar el Borrador Requerimiento seleccionado", "Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dlgResult2 != DialogResult.No && AdmReq.Envio(Req.codAlmaDestino, Req.CodRequerimiento))
					{
						MessageBox.Show("El Borrador de requerimiento ha envio correctamente", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						CargaLista();
					}
				}
				else
				{
					MessageBox.Show("No Se Puede Enviar, verifique Almacen Destino", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show("No Se Puede Enviar, Ya tiene un Almacen Destino", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void btnrechazar_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.CurrentRow != null && Req.codAlmaDestino == frmLogin.iCodAlmacen && AtendidoReq == "VIGENTE")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea rechazar el Requerimiento seleccionado", "Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No)
			{
				if (Application.OpenForms["frmVigenciaCotizacion"] != null)
				{
					Application.OpenForms["frmVigenciaCotizacion"].Close();
					return;
				}
				frmVigenciaCotizacion form = new frmVigenciaCotizacion();
				form.Proceso = 2;
				form.Procede = 1;
				form.groupBox2.Visible = true;
				form.groupBox1.Visible = false;
				form.CodRequerimiento = Req.CodRequerimiento;
				form.Show();
			}
		}
		else
		{
			MessageBox.Show("No se puede anular. Verifique");
		}
	}

	private void btnAtendido_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.CurrentRow != null && Req.codAlmaDestino == frmLogin.iCodAlmacen && AtendidoReq == "VIGENTE")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Atender el Requerimiento seleccionado", "Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmReq.Atender(Req.CodRequerimiento))
			{
				MessageBox.Show("El Borrador de requerimiento ha envio correctamente", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
		else
		{
			MessageBox.Show("No se puede Atender Requerimiento. Verifique");
		}
	}

	private void frmRequerimientosVigentes_Shown(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRequerimientosVigentes));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvOrdenes = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.AlmacenD = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Atendido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacendestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ComentarioRechazado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnIrCotizacion = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.btnConsultar = new System.Windows.Forms.Button();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.lbalmacen = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnAtendido = new System.Windows.Forms.Button();
		this.btnrechazar = new System.Windows.Forms.Button();
		this.btnEnviar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvOrdenes);
		this.groupBox1.Location = new System.Drawing.Point(1, 81);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(911, 360);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.dgvOrdenes.AllowUserToAddRows = false;
		this.dgvOrdenes.AllowUserToDeleteRows = false;
		this.dgvOrdenes.AllowUserToOrderColumns = true;
		this.dgvOrdenes.AllowUserToResizeRows = false;
		this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvOrdenes.Columns.AddRange(this.codigo, this.Documento, this.serie, this.numeracion, this.fecha, this.Almacen, this.AlmacenD, this.codAlmacen, this.responsable, this.Atendido, this.Estado, this.almacendestino, this.ComentarioRechazado);
		this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvOrdenes.Location = new System.Drawing.Point(3, 16);
		this.dgvOrdenes.MultiSelect = false;
		this.dgvOrdenes.Name = "dgvOrdenes";
		this.dgvOrdenes.ReadOnly = true;
		this.dgvOrdenes.RowHeadersVisible = false;
		this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvOrdenes.Size = new System.Drawing.Size(905, 341);
		this.dgvOrdenes.TabIndex = 0;
		this.dgvOrdenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCotizaciones_CellDoubleClick);
		this.dgvOrdenes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvOrdenes_ColumnHeaderMouseClick);
		this.dgvOrdenes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCotizaciones_RowStateChanged);
		this.codigo.DataPropertyName = "codRequer";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.codigo.Width = 150;
		this.Documento.DataPropertyName = "documento";
		this.Documento.HeaderText = "Documento";
		this.Documento.Name = "Documento";
		this.Documento.ReadOnly = true;
		this.serie.DataPropertyName = "serie";
		this.serie.HeaderText = "Serie";
		this.serie.Name = "serie";
		this.serie.ReadOnly = true;
		this.serie.Visible = false;
		this.numeracion.DataPropertyName = "numeracion";
		this.numeracion.HeaderText = "Numeracion";
		this.numeracion.Name = "numeracion";
		this.numeracion.ReadOnly = true;
		this.numeracion.Visible = false;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle2;
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 80;
		this.Almacen.DataPropertyName = "nombre";
		this.Almacen.HeaderText = "Almacen Origen";
		this.Almacen.Name = "Almacen";
		this.Almacen.ReadOnly = true;
		this.Almacen.Width = 150;
		this.AlmacenD.DataPropertyName = "nombrealmacenDestino";
		this.AlmacenD.HeaderText = "Almacen Destino";
		this.AlmacenD.Name = "AlmacenD";
		this.AlmacenD.ReadOnly = true;
		this.AlmacenD.Width = 150;
		this.codAlmacen.DataPropertyName = "codAlmacen";
		this.codAlmacen.HeaderText = "codAlmacen";
		this.codAlmacen.Name = "codAlmacen";
		this.codAlmacen.ReadOnly = true;
		this.codAlmacen.Visible = false;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable Origen";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 200;
		this.Atendido.DataPropertyName = "Atendido";
		this.Atendido.HeaderText = "Estado";
		this.Atendido.Name = "Atendido";
		this.Atendido.ReadOnly = true;
		this.Atendido.Width = 220;
		this.Estado.DataPropertyName = "Estado";
		this.Estado.HeaderText = "Anulado";
		this.Estado.Name = "Estado";
		this.Estado.ReadOnly = true;
		this.Estado.Width = 50;
		this.almacendestino.DataPropertyName = "almacendestino";
		this.almacendestino.HeaderText = "almacendestino";
		this.almacendestino.Name = "almacendestino";
		this.almacendestino.ReadOnly = true;
		this.almacendestino.Visible = false;
		this.ComentarioRechazado.DataPropertyName = "ComentarioRechazado";
		this.ComentarioRechazado.HeaderText = "Razón de Rechazado";
		this.ComentarioRechazado.Name = "ComentarioRechazado";
		this.ComentarioRechazado.ReadOnly = true;
		this.ComentarioRechazado.Width = 250;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(316, 11);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular ";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btnIrCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrCotizacion.ImageIndex = 1;
		this.btnIrCotizacion.ImageList = this.imageList1;
		this.btnIrCotizacion.Location = new System.Drawing.Point(418, 11);
		this.btnIrCotizacion.Name = "btnIrCotizacion";
		this.btnIrCotizacion.Size = new System.Drawing.Size(111, 37);
		this.btnIrCotizacion.TabIndex = 2;
		this.btnIrCotizacion.Text = "Ir Requerimiento";
		this.btnIrCotizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrCotizacion.UseVisualStyleBackColor = true;
		this.btnIrCotizacion.Click += new System.EventHandler(btnIrCotizacion_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(538, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList2.Images.SetKeyName(1, "Add.png");
		this.imageList2.Images.SetKeyName(2, "Remove.png");
		this.imageList2.Images.SetKeyName(3, "Write Document.png");
		this.imageList2.Images.SetKeyName(4, "New Document.png");
		this.imageList2.Images.SetKeyName(5, "Remove Document.png");
		this.imageList2.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList2.Images.SetKeyName(7, "document-print.png");
		this.imageList2.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList2.Images.SetKeyName(9, "refresh_256.png");
		this.imageList2.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList2.Images.SetKeyName(11, "search (1).png");
		this.imageList2.Images.SetKeyName(12, "search (5).png");
		this.imageList2.Images.SetKeyName(13, "search (6).png");
		this.imageList2.Images.SetKeyName(14, "search (8).png");
		this.imageList2.Images.SetKeyName(15, "search_top.png");
		this.imageList2.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList2.Images.SetKeyName(17, "Folder open.png");
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.SteelBlue;
		this.label6.Location = new System.Drawing.Point(494, 47);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(491, 18);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(559, 16);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 15;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(558, 42);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 14;
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Write Document.png");
		this.imageList3.Images.SetKeyName(1, "New Document.png");
		this.imageList3.Images.SetKeyName(2, "Remove Document.png");
		this.imageList3.Images.SetKeyName(3, "document-print.png");
		this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList3.Images.SetKeyName(5, "exit.png");
		this.imageList3.Images.SetKeyName(6, "search (1).png");
		this.imageList3.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList3.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList3.Images.SetKeyName(9, "document_delete.png");
		this.imageList3.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList3.Images.SetKeyName(11, "OK_Verde.png");
		this.imageList3.Images.SetKeyName(12, "Remove.png");
		this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnConsultar.ImageIndex = 6;
		this.btnConsultar.ImageList = this.imageList3;
		this.btnConsultar.Location = new System.Drawing.Point(717, 27);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(105, 35);
		this.btnConsultar.TabIndex = 19;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(218, 16);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(204, 21);
		this.cmbAlmacen.TabIndex = 1;
		this.lbalmacen.AutoSize = true;
		this.lbalmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbalmacen.ForeColor = System.Drawing.Color.SteelBlue;
		this.lbalmacen.Location = new System.Drawing.Point(15, 19);
		this.lbalmacen.Name = "lbalmacen";
		this.lbalmacen.Size = new System.Drawing.Size(59, 13);
		this.lbalmacen.TabIndex = 2;
		this.lbalmacen.Text = "Almacen:";
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.txtFiltro);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.lbalmacen);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.btnConsultar);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.cmbAlmacen);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.dtpHasta);
		this.groupBox2.Controls.Add(this.dtpDesde);
		this.groupBox2.Location = new System.Drawing.Point(12, 4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(886, 71);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label2.Location = new System.Drawing.Point(6, 55);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(12, 13);
		this.label2.TabIndex = 40;
		this.label2.Text = "x";
		this.label2.Visible = false;
		this.txtFiltro.Location = new System.Drawing.Point(218, 41);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(204, 20);
		this.txtFiltro.TabIndex = 1;
		this.txtFiltro.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(54, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 12);
		this.label1.TabIndex = 39;
		this.label1.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(89, 45);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 38;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(16, 45);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 37;
		this.label10.Text = "Filtro";
		this.groupBox3.Controls.Add(this.btnAtendido);
		this.groupBox3.Controls.Add(this.btnrechazar);
		this.groupBox3.Controls.Add(this.btnEnviar);
		this.groupBox3.Controls.Add(this.btnAnular);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnIrCotizacion);
		this.groupBox3.Location = new System.Drawing.Point(125, 447);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(619, 54);
		this.groupBox3.TabIndex = 5;
		this.groupBox3.TabStop = false;
		this.btnAtendido.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAtendido.ImageIndex = 11;
		this.btnAtendido.ImageList = this.imageList3;
		this.btnAtendido.Location = new System.Drawing.Point(10, 11);
		this.btnAtendido.Name = "btnAtendido";
		this.btnAtendido.Size = new System.Drawing.Size(96, 37);
		this.btnAtendido.TabIndex = 7;
		this.btnAtendido.Text = "Atender";
		this.btnAtendido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAtendido.UseVisualStyleBackColor = true;
		this.btnAtendido.Click += new System.EventHandler(btnAtendido_Click);
		this.btnrechazar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnrechazar.ImageIndex = 12;
		this.btnrechazar.ImageList = this.imageList3;
		this.btnrechazar.Location = new System.Drawing.Point(214, 11);
		this.btnrechazar.Name = "btnrechazar";
		this.btnrechazar.Size = new System.Drawing.Size(96, 37);
		this.btnrechazar.TabIndex = 6;
		this.btnrechazar.Text = "Rechazar";
		this.btnrechazar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnrechazar.UseVisualStyleBackColor = true;
		this.btnrechazar.Click += new System.EventHandler(btnrechazar_Click);
		this.btnEnviar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEnviar.ImageIndex = 17;
		this.btnEnviar.ImageList = this.imageList2;
		this.btnEnviar.Location = new System.Drawing.Point(112, 10);
		this.btnEnviar.Name = "btnEnviar";
		this.btnEnviar.Size = new System.Drawing.Size(96, 37);
		this.btnEnviar.TabIndex = 5;
		this.btnEnviar.Text = "Enviar ";
		this.btnEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEnviar.UseVisualStyleBackColor = true;
		this.btnEnviar.Click += new System.EventHandler(button1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(910, 506);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmRequerimientosVigentes";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Lista Requerimientos";
		base.Load += new System.EventHandler(frmRequerimientosVigentes_Load);
		base.Shown += new System.EventHandler(frmRequerimientosVigentes_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
