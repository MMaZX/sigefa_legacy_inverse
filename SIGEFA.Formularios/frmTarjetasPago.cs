using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmTarjetasPago : Office2007Form
{
	private clsTarjetaPago tarjeta = new clsTarjetaPago();

	private clsAdmTarjetaPago admTar = new clsAdmTarjetaPago();

	private clsValidar ok = new clsValidar();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvTarjetas;

	private GroupBox groupBox2;

	private TextBox txtDescripcion;

	private TextBox txtTipoTarjeta;

	private Label label2;

	private Label label1;

	private GroupBox groupBox3;

	private ImageList imageList1;

	private ErrorProvider errorProvider1;

	private SuperValidator superValidator1;

	private Highlighter highlighter1;

	private Button btnSalir;

	private Button btnGuardar;

	private Button btnEliminar;

	private Button btnEditar;

	private Button btnNuevo;

	private TextBox txtFiltro;

	private Label label5;

	private Label label4;

	private Label label3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private DataGridViewTextBoxColumn codTarjeta;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn porComision;

	private DataGridViewTextBoxColumn alquiler;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn codUser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn codAlmacen;

	private Button btnReporte;

	public frmTarjetasPago()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvTarjetas.DataSource = data;
		data.DataSource = admTar.MuestraTarjetas(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvTarjetas.ClearSelection();
	}

	private void frmTarjetasPago_Load(object sender, EventArgs e)
	{
		groupBox1.Height = 192;
		CargaLista();
		label4.Text = "Tipo Tarjeta";
		label5.Text = "tipotarjeta";
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label5.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		txtDescripcion.Text = "";
		txtTipoTarjeta.Text = "";
		superValidator1.Validate();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Editar Registro";
		Proceso = 2;
		txtTipoTarjeta.Text = tarjeta.Tipo;
		txtDescripcion.Text = tarjeta.Descripcion;
	}

	private void frmTarjetasPago_Shown(object sender, EventArgs e)
	{
		CargaLista();
		txtFiltro.Focus();
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

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtDescripcion.Text != "") || !(txtTipoTarjeta.Text != ""))
		{
			return;
		}
		tarjeta.Tipo = txtTipoTarjeta.Text;
		tarjeta.Descripcion = txtDescripcion.Text;
		tarjeta.Coduser = frmLogin.iCodUser;
		tarjeta.CodAlmacen = frmLogin.iCodAlmacen;
		if (Proceso == 1)
		{
			if (admTar.Insert(tarjeta))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion CtaCte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
			}
		}
		else if (Proceso == 2 && admTar.Update(tarjeta))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Familia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
		}
		Proceso = 0;
	}

	private void dgvTarjetas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvTarjetas.Rows.Count >= 1 && e.Row.Selected)
		{
			tarjeta.CodTarjeta = Convert.ToInt32(e.Row.Cells[codTarjeta.Name].Value);
			tarjeta.Tipo = e.Row.Cells[tipo.Name].Value.ToString();
			tarjeta.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			tarjeta.PorcComision = Convert.ToDouble(e.Row.Cells[porComision.Name].Value);
			tarjeta.AlquilerEquipo = Convert.ToDouble(e.Row.Cells[alquiler.Name].Value);
			tarjeta.Estado = Convert.ToBoolean(e.Row.Cells[estado.Name].Value);
			tarjeta.Coduser = Convert.ToInt32(e.Row.Cells[codUser.Name].Value);
			tarjeta.Fecharegistro = Convert.ToDateTime(e.Row.Cells[fecharegistro.Name].Value);
			tarjeta.CodAlmacen = Convert.ToInt32(e.Row.Cells[codAlmacen.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvTarjetas.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void dgvTarjetas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label4.Text = dgvTarjetas.Columns[e.ColumnIndex].HeaderText;
		label5.Text = dgvTarjetas.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvTarjetas.CurrentRow.Index != -1 && tarjeta.CodTarjeta != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "CtaCte", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admTar.Delete(tarjeta.CodTarjeta, frmLogin.iCodAlmacen))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "CtaCte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
	}

	private void txtTipoTarjeta_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.letras(e);
	}

	private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.letras(e);
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

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 15;
		frm.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTarjetasPago));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.dgvTarjetas = new System.Windows.Forms.DataGridView();
		this.codTarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.porComision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.alquiler = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtTipoTarjeta = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTarjetas).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.dgvTarjetas);
		this.groupBox1.Location = new System.Drawing.Point(13, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(520, 80);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "TARJETA DE PAGO";
		this.txtFiltro.Location = new System.Drawing.Point(185, 26);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(231, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(422, 30);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(12, 13);
		this.label5.TabIndex = 3;
		this.label5.Text = "x";
		this.label5.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(77, 27);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(17, 16);
		this.label4.TabIndex = 2;
		this.label4.Text = "X";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 29);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 13);
		this.label3.TabIndex = 1;
		this.label3.Text = "Buscar Por: ";
		this.dgvTarjetas.AllowUserToAddRows = false;
		this.dgvTarjetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTarjetas.Columns.AddRange(this.codTarjeta, this.tipo, this.descripcion, this.porComision, this.alquiler, this.estado, this.codUser, this.fecharegistro, this.codAlmacen);
		this.dgvTarjetas.Location = new System.Drawing.Point(6, 66);
		this.dgvTarjetas.Name = "dgvTarjetas";
		this.dgvTarjetas.ReadOnly = true;
		this.dgvTarjetas.RowHeadersVisible = false;
		this.dgvTarjetas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTarjetas.Size = new System.Drawing.Size(507, 104);
		this.dgvTarjetas.TabIndex = 0;
		this.dgvTarjetas.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvTarjetas_ColumnHeaderMouseClick);
		this.dgvTarjetas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvTarjetas_RowStateChanged);
		this.codTarjeta.DataPropertyName = "codtarjeta";
		this.codTarjeta.HeaderText = "codTarjeta";
		this.codTarjeta.Name = "codTarjeta";
		this.codTarjeta.ReadOnly = true;
		this.codTarjeta.Visible = false;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "Tipo Tarjeta";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Width = 200;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 300;
		this.porComision.DataPropertyName = "porcComision";
		this.porComision.HeaderText = "% Comision";
		this.porComision.Name = "porComision";
		this.porComision.ReadOnly = true;
		this.porComision.Visible = false;
		this.alquiler.DataPropertyName = "alquilerEquipo";
		this.alquiler.HeaderText = "Alquiler Equipo";
		this.alquiler.Name = "alquiler";
		this.alquiler.ReadOnly = true;
		this.alquiler.Visible = false;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.codUser.DataPropertyName = "coduser";
		this.codUser.HeaderText = "codUser";
		this.codUser.Name = "codUser";
		this.codUser.ReadOnly = true;
		this.codUser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Registro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		this.fecharegistro.Width = 150;
		this.codAlmacen.DataPropertyName = "codAlmacen";
		this.codAlmacen.HeaderText = "codAlmacen";
		this.codAlmacen.Name = "codAlmacen";
		this.codAlmacen.ReadOnly = true;
		this.codAlmacen.Visible = false;
		this.groupBox2.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.txtTipoTarjeta);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Location = new System.Drawing.Point(125, 98);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(307, 100);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(78, 60);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(211, 20);
		this.txtDescripcion.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator2);
		this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDescripcion_KeyPress);
		this.txtTipoTarjeta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoTarjeta.Location = new System.Drawing.Point(79, 27);
		this.txtTipoTarjeta.Name = "txtTipoTarjeta";
		this.txtTipoTarjeta.Size = new System.Drawing.Size(211, 20);
		this.txtTipoTarjeta.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtTipoTarjeta, this.customValidator1);
		this.txtTipoTarjeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTipoTarjeta_KeyPress);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 63);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Descripcion:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(67, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Tipo Tarjeta:";
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Location = new System.Drawing.Point(13, 204);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(520, 46);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(258, 8);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 12;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(425, 7);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 11;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(342, 7);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 10;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(176, 7);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(104, 7);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 7;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(27, 7);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator2.ErrorMessage = "Ingrese Descripcion";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese Tipo Tarjeta";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(537, 262);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTarjetasPago";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Tarjetas Pago";
		base.Load += new System.EventHandler(frmTarjetasPago_Load);
		base.Shown += new System.EventHandler(frmTarjetasPago_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTarjetas).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
