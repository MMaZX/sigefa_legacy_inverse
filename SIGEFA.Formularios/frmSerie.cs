using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmSerie : Office2007Form
{
	private clsAdmSerie Admser = new clsAdmSerie();

	public clsSerie ser = new clsSerie();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento td = new clsTipoDocumento();

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	public int DocSeleccionado;

	public string Sigla;

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsValidar ok = new clsValidar();

	public int CodAlma = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnReporte;

	private Button btnEliminar;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvSeries;

	private GroupBox groupBox2;

	private Label label4;

	private Label label5;

	private TextBox txtDescripcion;

	private TextBox txtCodigo;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Label label6;

	private TextBox txtTipoDoc;

	private Label label10;

	private TextBox txtCorrelativo;

	private Label label9;

	private TextBox txtFinal;

	private Label label8;

	private TextBox txtInicio;

	private Label label7;

	private TextBox txtSerie;

	private CustomValidator customValidator4;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private ComboBox cmbAlmacen;

	private ComboBox cmbEmpresa;

	private Label label15;

	private Label label14;

	private ComboBox cbImpresoras;

	private ComboBox cbDocumentoImpresora;

	private Label label11;

	private Label label12;

	private Label label13;

	private TextBox txtSerieImpresora;

	private CustomValidator customValidator6;

	private CheckBox chbManual;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn coddocumento;

	private DataGridViewTextBoxColumn Documento;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn serie;

	private DataGridViewTextBoxColumn numeracion;

	private DataGridViewTextBoxColumn inicio;

	private DataGridViewTextBoxColumn fin;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn nombreimpresora;

	private DataGridViewTextBoxColumn papel;

	private DataGridViewTextBoxColumn serie_impresora;

	private DataGridViewTextBoxColumn codalmacen;

	private DataGridViewTextBoxColumn manual;

	public frmSerie()
	{
		InitializeComponent();
	}

	private void frmSerie_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		cmbEmpresa.SelectedValue = frmLogin.iCodEmpresa;
		CargaAlmacenes();
		CargaImpresoras();
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
		CargaLista();
		td = Admdoc.BuscaTipoDocumento(Sigla);
		label2.Text = "Codigo";
		label3.Text = "codSerie";
		if (Proceso == 3)
		{
			bloquearbotones();
		}
	}

	private void CargaImpresoras()
	{
		foreach (string printname in PrinterSettings.InstalledPrinters)
		{
			cbImpresoras.Items.Add(printname);
		}
	}

	private void CargaPaperSize()
	{
		cbDocumentoImpresora.Items.Clear();
		PrintDocument printdoc = new PrintDocument();
		printdoc.PrinterSettings.PrinterName = cbImpresoras.SelectedItem.ToString();
		foreach (PaperSize psize in printdoc.PrinterSettings.PaperSizes)
		{
			cbDocumentoImpresora.Items.Add(psize.PaperName);
		}
	}

	private void bloquearbotones()
	{
		btnNuevo.Visible = false;
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
		btnReporte.Visible = false;
		btnGuardar.Text = "Aceptar";
		btnGuardar.ImageIndex = 6;
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
		txtTipoDoc.Text = td.Sigla;
		txtDescripcion.Text = td.Descripcion;
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		Proceso = 2;
		groupBox2.Text = "Editar Registro";
		txtCodigo.Text = ser.CodSerie.ToString();
		txtTipoDoc.Text = td.Sigla;
		txtDescripcion.Text = td.Descripcion;
		txtSerie.Text = ser.Serie;
		txtInicio.Text = ser.Inicio.ToString();
		txtFinal.Text = ser.Fin.ToString();
		txtCorrelativo.Text = ser.Numeracion.ToString();
		cbImpresoras.Text = ser.NombreImpresora;
		cbDocumentoImpresora.Text = ser.PaperSize;
		txtSerieImpresora.Text = ser.SerieImpresora;
		if (ser.PreImpreso)
		{
			chbManual.Visible = true;
			chbManual.Enabled = false;
		}
		else
		{
			chbManual.Visible = false;
		}
		chbManual.Checked = ser.PreImpreso;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (btnGuardar.Text == "Guardar")
			{
				if (!superValidator1.Validate() || Proceso == 0 || !(txtSerie.Text != ""))
				{
					return;
				}
				ser.CodDocumento = DocSeleccionado;
				ser.CodEmpresa = Convert.ToInt32(cmbEmpresa.SelectedValue);
				ser.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
				ser.Serie = txtSerie.Text;
				ser.Inicio = Convert.ToInt32(txtInicio.Text);
				ser.Fin = Convert.ToInt32(txtFinal.Text);
				if (chbManual.Checked)
				{
					txtCorrelativo.Text = "";
					txtCorrelativo.ReadOnly = true;
					ser.Numeracion = 0;
				}
				else if (!chbManual.Checked && txtCorrelativo.Text == "")
				{
					MessageBox.Show("Debe Ingresar Número Correlativo", "Series", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					txtCorrelativo.Focus();
					return;
				}
				if (!chbManual.Checked)
				{
					ser.Numeracion = Convert.ToInt32(txtCorrelativo.Text);
				}
				ser.NombreImpresora = cbImpresoras.SelectedItem.ToString();
				ser.PaperSize = cbDocumentoImpresora.SelectedItem.ToString();
				ser.SerieImpresora = txtSerieImpresora.Text;
				ser.PreImpreso = chbManual.Checked;
				if (Proceso == 1)
				{
					ser.CodUser = frmLogin.iCodUser;
					if (Admser.insert(ser))
					{
						MessageBox.Show("Los datos se guardaron correctamente", "Gestion Serie", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						CambiarEstados(Estado: true);
						CargaLista();
					}
				}
				else if (Proceso == 2 && Admser.update(ser))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Gestion Serie", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CambiarEstados(Estado: true);
					CargaLista();
				}
				Proceso = 0;
			}
			else if (btnGuardar.Text == "Aceptar" && Proceso == 3)
			{
				Close();
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Debe Elegir una impresora");
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (groupBox1.Visible)
		{
			Close();
			return;
		}
		Proceso = 0;
		CambiarEstados(Estado: true);
	}

	private void CargaEmpresas()
	{
		cmbEmpresa.DataSource = admEmp.CargaEmpresas();
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
		cmbEmpresa.SelectedIndex = -1;
	}

	private void CargaAlmacenes()
	{
		cmbAlmacen.DataSource = admAlm.CargaAlmacenes(frmLogin.iNivelUser, Convert.ToInt32(cmbEmpresa.SelectedValue), frmLogin.iCodUser);
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedIndex = -1;
	}

	private void CargaLista()
	{
		if (CodAlma != 0)
		{
			dgvSeries.DataSource = data;
			data.DataSource = Admser.MuestraSeries(DocSeleccionado, CodAlma);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvSeries.ClearSelection();
		}
		else
		{
			dgvSeries.DataSource = data;
			data.DataSource = Admser.MuestraSeries(DocSeleccionado, frmLogin.iCodAlmacen);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvSeries.ClearSelection();
		}
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		txtCodigo.Text = "";
		txtDescripcion.Text = "";
		txtTipoDoc.Text = "";
		txtSerie.Text = "";
		txtInicio.Text = "";
		txtFinal.Text = "";
		txtCorrelativo.Text = "";
		superValidator1.Validate();
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
		}
		catch (Exception)
		{
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvSeries.CurrentRow.Index != -1 && ser.CodSerie != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Serie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && Admser.delete(ser.CodSerie))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Serie", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void dgvSeries_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvSeries.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvSeries.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void dgvSeries_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvSeries.Rows.Count >= 1 && e.Row.Selected)
		{
			ser.CodSerie = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			ser.CodDocumento = Convert.ToInt32(e.Row.Cells[coddocumento.Name].Value);
			ser.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			ser.Serie = e.Row.Cells[serie.Name].Value.ToString();
			ser.Inicio = Convert.ToInt32(e.Row.Cells[inicio.Name].Value);
			ser.Fin = Convert.ToInt32(e.Row.Cells[fin.Name].Value);
			ser.Numeracion = Convert.ToInt32(e.Row.Cells[numeracion.Name].Value);
			ser.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			ser.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			ser.NombreImpresora = e.Row.Cells[nombreimpresora.Name].Value.ToString();
			ser.PaperSize = e.Row.Cells[papel.Name].Value.ToString();
			ser.SerieImpresora = e.Row.Cells[serie_impresora.Name].Value.ToString();
			if (e.Row.Cells[manual.Name].Value.ToString() == "MANUAL")
			{
				ser.PreImpreso = true;
			}
			else
			{
				ser.PreImpreso = false;
			}
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvSeries.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
	}

	private void txtCorrelativo_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (chbManual.Checked)
		{
			txtCorrelativo.Text = "";
		}
	}

	private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
	}

	private void txtFinal_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
	}

	private void dgvSeries_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void dgvSeries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			txtSerie.Text = dgvSeries.CurrentRow.Cells[serie.Name].Value.ToString();
			Close();
		}
	}

	private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaAlmacenes();
	}

	private void cbImpresoras_SelectedValueChanged(object sender, EventArgs e)
	{
		CargaPaperSize();
	}

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void chbManual_CheckedChanged(object sender, EventArgs e)
	{
		if (chbManual.Checked)
		{
			txtCorrelativo.Text = "";
			txtCorrelativo.ReadOnly = true;
		}
	}

	private void frmSerie_Shown(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmSerie));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvSeries = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coddocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.inicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fin = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombreimpresora = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.papel = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie_impresora = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.manual = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.chbManual = new System.Windows.Forms.CheckBox();
		this.txtSerieImpresora = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.cbDocumentoImpresora = new System.Windows.Forms.ComboBox();
		this.cbImpresoras = new System.Windows.Forms.ComboBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtCorrelativo = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtFinal = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtInicio = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtTipoDoc = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvSeries).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
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
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Location = new System.Drawing.Point(12, 245);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(476, 48);
		this.groupBox3.TabIndex = 15;
		this.groupBox3.TabStop = false;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(403, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 16;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 10);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 11;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnEditar.Enabled = false;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 10);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 12;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(236, 10);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 14;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(155, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 13;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(320, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 15;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvSeries);
		this.groupBox1.Location = new System.Drawing.Point(7, 31);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 175);
		this.groupBox1.TabIndex = 16;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Series";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(427, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(96, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(200, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.dgvSeries.AllowUserToAddRows = false;
		this.dgvSeries.AllowUserToDeleteRows = false;
		this.dgvSeries.AllowUserToOrderColumns = true;
		this.dgvSeries.AllowUserToResizeColumns = false;
		this.dgvSeries.AllowUserToResizeRows = false;
		this.dgvSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvSeries.Columns.AddRange(this.codigo, this.coddocumento, this.Documento, this.descripcion, this.serie, this.numeracion, this.inicio, this.fin, this.estado, this.coduser, this.fecha, this.nombreimpresora, this.papel, this.serie_impresora, this.codalmacen, this.manual);
		this.dgvSeries.Location = new System.Drawing.Point(3, 58);
		this.dgvSeries.MultiSelect = false;
		this.dgvSeries.Name = "dgvSeries";
		this.dgvSeries.ReadOnly = true;
		this.dgvSeries.RowHeadersVisible = false;
		this.dgvSeries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvSeries.Size = new System.Drawing.Size(470, 117);
		this.dgvSeries.TabIndex = 2;
		this.dgvSeries.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvSeries_CellDoubleClick);
		this.dgvSeries.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvSeries_ColumnHeaderMouseClick);
		this.dgvSeries.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvSeries_CellClick);
		this.dgvSeries.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvSeries_RowStateChanged);
		this.codigo.DataPropertyName = "codSerie";
		dataGridViewCellStyle1.NullValue = null;
		this.codigo.DefaultCellStyle = dataGridViewCellStyle1;
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.coddocumento.DataPropertyName = "codTipoDocumento";
		this.coddocumento.HeaderText = "codDoc";
		this.coddocumento.Name = "coddocumento";
		this.coddocumento.ReadOnly = true;
		this.coddocumento.Visible = false;
		this.Documento.DataPropertyName = "sigla";
		this.Documento.HeaderText = "TD";
		this.Documento.Name = "Documento";
		this.Documento.ReadOnly = true;
		this.Documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Documento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Documento.Width = 40;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.descripcion.Width = 250;
		this.serie.DataPropertyName = "serie";
		this.serie.HeaderText = "Serie";
		this.serie.Name = "serie";
		this.serie.ReadOnly = true;
		this.serie.Width = 80;
		this.numeracion.DataPropertyName = "numeracion";
		this.numeracion.HeaderText = "Nro. Actual";
		this.numeracion.Name = "numeracion";
		this.numeracion.ReadOnly = true;
		this.inicio.DataPropertyName = "inicio";
		this.inicio.HeaderText = "Inicio";
		this.inicio.Name = "inicio";
		this.inicio.ReadOnly = true;
		this.inicio.Visible = false;
		this.fin.DataPropertyName = "fin";
		this.fin.HeaderText = "Fin";
		this.fin.Name = "fin";
		this.fin.ReadOnly = true;
		this.fin.Visible = false;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.coduser.Visible = false;
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaReg";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.fecha.Visible = false;
		this.nombreimpresora.DataPropertyName = "nombreimpresora";
		this.nombreimpresora.HeaderText = "Impresora";
		this.nombreimpresora.Name = "nombreimpresora";
		this.nombreimpresora.ReadOnly = true;
		this.papel.DataPropertyName = "papersize";
		this.papel.HeaderText = "PaperSize";
		this.papel.Name = "papel";
		this.papel.ReadOnly = true;
		this.serie_impresora.DataPropertyName = "serie_impresora";
		this.serie_impresora.HeaderText = "Serie Impresora";
		this.serie_impresora.Name = "serie_impresora";
		this.serie_impresora.ReadOnly = true;
		this.codalmacen.DataPropertyName = "codalmacen";
		this.codalmacen.HeaderText = "codalmacen";
		this.codalmacen.Name = "codalmacen";
		this.codalmacen.ReadOnly = true;
		this.codalmacen.Visible = false;
		this.manual.DataPropertyName = "preimpreso";
		this.manual.HeaderText = "Tipo";
		this.manual.Name = "manual";
		this.manual.ReadOnly = true;
		this.groupBox2.Controls.Add(this.chbManual);
		this.groupBox2.Controls.Add(this.txtSerieImpresora);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.cbDocumentoImpresora);
		this.groupBox2.Controls.Add(this.cbImpresoras);
		this.groupBox2.Controls.Add(this.cmbAlmacen);
		this.groupBox2.Controls.Add(this.cmbEmpresa);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.txtCorrelativo);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtFinal);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.txtInicio);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.txtSerie);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.txtTipoDoc);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Location = new System.Drawing.Point(18, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(442, 227);
		this.groupBox2.TabIndex = 19;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.chbManual.AutoSize = true;
		this.chbManual.Location = new System.Drawing.Point(352, 22);
		this.chbManual.Name = "chbManual";
		this.chbManual.Size = new System.Drawing.Size(61, 17);
		this.chbManual.TabIndex = 29;
		this.chbManual.Text = "Manual";
		this.chbManual.UseVisualStyleBackColor = true;
		this.chbManual.CheckedChanged += new System.EventHandler(chbManual_CheckedChanged);
		this.txtSerieImpresora.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerieImpresora.Location = new System.Drawing.Point(98, 195);
		this.txtSerieImpresora.Name = "txtSerieImpresora";
		this.txtSerieImpresora.Size = new System.Drawing.Size(116, 20);
		this.txtSerieImpresora.TabIndex = 27;
		this.superValidator1.SetValidator1(this.txtSerieImpresora, this.customValidator6);
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(9, 198);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(83, 13);
		this.label13.TabIndex = 26;
		this.label13.Text = "Serie Impresora:";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(15, 166);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(59, 13);
		this.label11.TabIndex = 25;
		this.label11.Text = "Impresora :";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(242, 166);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(40, 13);
		this.label12.TabIndex = 24;
		this.label12.Text = "Papel :";
		this.cbDocumentoImpresora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbDocumentoImpresora.FormattingEnabled = true;
		this.cbDocumentoImpresora.Location = new System.Drawing.Point(290, 163);
		this.cbDocumentoImpresora.Name = "cbDocumentoImpresora";
		this.cbDocumentoImpresora.Size = new System.Drawing.Size(123, 21);
		this.cbDocumentoImpresora.TabIndex = 23;
		this.cbImpresoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbImpresoras.FormattingEnabled = true;
		this.cbImpresoras.Location = new System.Drawing.Point(81, 163);
		this.cbImpresoras.Name = "cbImpresoras";
		this.cbImpresoras.Size = new System.Drawing.Size(133, 21);
		this.cbImpresoras.TabIndex = 22;
		this.cbImpresoras.SelectedValueChanged += new System.EventHandler(cbImpresoras_SelectedValueChanged);
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(290, 84);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(123, 21);
		this.cmbAlmacen.TabIndex = 21;
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(81, 84);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(133, 21);
		this.cmbEmpresa.TabIndex = 20;
		this.cmbEmpresa.SelectionChangeCommitted += new System.EventHandler(cmbEmpresa_SelectionChangeCommitted);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(228, 87);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(54, 13);
		this.label15.TabIndex = 19;
		this.label15.Text = "Almacen :";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(21, 87);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 18;
		this.label14.Text = "Empresa :";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(9, 140);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(66, 13);
		this.label10.TabIndex = 17;
		this.label10.Text = "Nro. Correl. :";
		this.txtCorrelativo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCorrelativo.Location = new System.Drawing.Point(81, 137);
		this.txtCorrelativo.Name = "txtCorrelativo";
		this.txtCorrelativo.Size = new System.Drawing.Size(71, 20);
		this.txtCorrelativo.TabIndex = 10;
		this.txtCorrelativo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCorrelativo_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(247, 140);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(35, 13);
		this.label9.TabIndex = 15;
		this.label9.Text = "Final :";
		this.txtFinal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFinal.Location = new System.Drawing.Point(290, 137);
		this.txtFinal.Name = "txtFinal";
		this.txtFinal.Size = new System.Drawing.Size(71, 20);
		this.txtFinal.TabIndex = 9;
		this.superValidator1.SetValidator1(this.txtFinal, this.customValidator4);
		this.txtFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFinal_KeyPress);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(244, 114);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(38, 13);
		this.label8.TabIndex = 13;
		this.label8.Text = "Inicio :";
		this.txtInicio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtInicio.Location = new System.Drawing.Point(290, 111);
		this.txtInicio.Name = "txtInicio";
		this.txtInicio.Size = new System.Drawing.Size(71, 20);
		this.txtInicio.TabIndex = 8;
		this.superValidator1.SetValidator1(this.txtInicio, this.customValidator3);
		this.txtInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInicio_KeyPress);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(37, 114);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(37, 13);
		this.label7.TabIndex = 11;
		this.label7.Text = "Serie :";
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Location = new System.Drawing.Point(81, 111);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.Size = new System.Drawing.Size(71, 20);
		this.txtSerie.TabIndex = 7;
		this.superValidator1.SetValidator1(this.txtSerie, this.customValidator2);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(21, 42);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(57, 13);
		this.label6.TabIndex = 9;
		this.label6.Text = "Tipo Doc :";
		this.txtTipoDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoDoc.Location = new System.Drawing.Point(19, 58);
		this.txtTipoDoc.Name = "txtTipoDoc";
		this.txtTipoDoc.ReadOnly = true;
		this.txtTipoDoc.Size = new System.Drawing.Size(52, 20);
		this.txtTipoDoc.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtTipoDoc, this.customValidator1);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(83, 42);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Descripción :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(21, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Código :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(81, 58);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.ReadOnly = true;
		this.txtDescripcion.Size = new System.Drawing.Size(238, 20);
		this.txtDescripcion.TabIndex = 6;
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(81, 19);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(71, 20);
		this.txtCodigo.TabIndex = 4;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator6.ErrorMessage = "Ingrese Serie de Impresora.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator4.ErrorMessage = "Ingrese el final de la numeración.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el inicio de la numeración.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese la Serie.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Seleccione el tipo de Documento.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 297);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmSerie";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Series";
		base.Load += new System.EventHandler(frmSerie_Load);
		base.Shown += new System.EventHandler(frmSerie_Shown);
		this.groupBox3.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvSeries).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
