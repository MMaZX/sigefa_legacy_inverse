using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmEntrega : Form
{
	internal int codEntrega = -1;

	private clsAdmDespacho admdesp = new clsAdmDespacho();

	private clsEntrega entrega = null;

	internal clsUsuario usuario_click = null;

	private clsAdmSerie admSerie = new clsAdmSerie();

	internal int codReqAlm = 0;

	private clsAdmRequerimientoAlmacen admreq = new clsAdmRequerimientoAlmacen();

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvDetalle;

	private GroupBox groupBox2;

	private TextBox txtNombreUsuario;

	private Label label3;

	private DateTimePicker dtpFechaEntrega;

	private Label label4;

	private TextBox txtRefEntrega;

	private Label label1;

	private DataGridViewTextBoxColumn colCodDetalleEntrega;

	private DataGridViewTextBoxColumn colCodDetalleDespacho;

	private DataGridViewTextBoxColumn colCodAlmacen;

	private DataGridViewTextBoxColumn colDescAlmacen;

	private DataGridViewTextBoxColumn colCodProducto;

	private DataGridViewTextBoxColumn colReferencia;

	private DataGridViewTextBoxColumn colDescProducto;

	private DataGridViewTextBoxColumn colCodUnidad;

	private DataGridViewTextBoxColumn colDescUnidad;

	private DataGridViewTextBoxColumn colCantidad;

	private DataGridViewTextBoxColumn colCantidadPendiente;

	private Button btnAnular;

	private TextBox txtAnulador;

	private Label lblanular;

	private DateTimePicker dtpAnulacion;

	private Label lblFechaAnulador;

	private Label lblAnulado;

	private Button btnImprimirTicket;

	private Button btnImprimirPDF;

	public frmEntrega()
	{
		InitializeComponent();
	}

	private void label1_Click(object sender, EventArgs e)
	{
	}

	private void frmEntrega_Load(object sender, EventArgs e)
	{
		if (codEntrega != -1)
		{
			entrega = admdesp.cargaEntrega(codEntrega);
			if (entrega.Estado == 1)
			{
				btnAnular.Visible = entrega.Anulado == 0;
				lblAnulado.Visible = entrega.Anulado != 0;
				visibleAnulacion(entrega.Anulado != 0);
			}
			dgvDetalle.DataSource = admdesp.listaDetalleEntrega(codEntrega);
			cargaEntregaEnFormulario();
		}
	}

	private void cargaEntregaEnFormulario()
	{
		txtNombreUsuario.Text = entrega.NombreUsuario;
		txtRefEntrega.Text = entrega.Serie + " - " + entrega.Numeracion;
		dtpFechaEntrega.Value = entrega.FechaEntrega;
		txtAnulador.Text = entrega.NombreUsuarioAnulacion;
		dtpAnulacion.Value = ((entrega.FechaAnulacion == DateTime.MinValue) ? dtpAnulacion.MinDate : entrega.FechaAnulacion);
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (entrega.Estado != 1 || entrega.Anulado != 0)
		{
			return;
		}
		if (entrega.CodAlmacenRegistro != frmLogin.iCodAlmacen)
		{
			MessageBox.Show("No puede ANULAR entregas de otro almacen", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		int codEntrega = entrega.CodEntrega;
		usuario_click = null;
		frmAutorizacion frm = new frmAutorizacion();
		frm.tipoAccion = 2;
		int codPermiso = new clsAdmFormulario().getPermisoAnularEntrega();
		frm.permiso = codPermiso;
		frm.PermitirAdministradores = true;
		frm.tipoVentanaAAsignarUsuario = 4;
		frm.ventanaEntrega = this;
		DialogResult dr = frm.ShowDialog();
		if (dr == DialogResult.OK && usuario_click != null)
		{
			bool rpta = false;
			using TransactionScope Scope = new TransactionScope();
			try
			{
				rpta = admdesp.anularEntrega(entrega.CodEntrega, usuario_click.CodUsuario, DateTime.Now);
				if (rpta)
				{
					rpta = admdesp.actualizaCantidadPendienteDespacho(entrega.CodDespacho);
				}
				if (rpta)
				{
					Scope.Complete();
					Scope.Dispose();
					MessageBox.Show("Entrega Nro: " + txtRefEntrega.Text + " Anulado Con Exito", "Anulacion de Entrega Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					visibleAnulacion(valor: true);
					entrega = admdesp.cargaEntrega(entrega.CodEntrega);
					cargaEntregaEnFormulario();
					lblAnulado.Visible = true;
					int codEstado = admdesp.obtenerCodEstado(entrega.CodDespacho);
					if (codEstado == -1)
					{
						return;
					}
					admdesp.cambioEstado(entrega.CodDespacho, codEstado);
					if (codEstado == 14)
					{
						if (codReqAlm > 0)
						{
							admreq.actualizaEstadoReqAlmacen(codReqAlm, 17);
						}
					}
					else if (codReqAlm > 0)
					{
						admreq.actualizaCantidadPendienteAprobadaReqAlmacen(codReqAlm, movimientostock: false);
					}
				}
				else
				{
					Transaction.Current.Rollback();
					Scope.Dispose();
					MessageBox.Show("Hubo un error al anular la entrega ", "Anulacion Denegada de Entrega", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				return;
			}
			catch (Exception ex)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				MessageBox.Show(ex.Message, "Anulacion de Entrega No Concluida", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		if (dr == DialogResult.OK)
		{
			MessageBox.Show("Ocurrio un problema al obtener el usuario que anulara la entrega", "Anulacion de Entrega Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void visibleAnulacion(bool valor)
	{
		lblanular.Visible = valor;
		txtAnulador.Visible = valor;
		lblFechaAnulador.Visible = valor;
		dtpAnulacion.Visible = valor;
		btnAnular.Visible = !valor;
	}

	private void btnImprimirPDF_Click(object sender, EventArgs e)
	{
		try
		{
			string ruta = "C:\\tmp\\Entregas";
			string nombreArchivo = "ENTREGA NRO -" + entrega.CodEntrega.ToString().PadLeft(8, '0');
			CREntrega rpt = new CREntrega();
			rpt.SetDataSource(admdesp.ReporteImprimirEntrega(Convert.ToInt32(entrega.CodEntrega)).Tables[0]);
			Directory.CreateDirectory(ruta);
			rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta + "\\" + nombreArchivo + ".pdf");
			Process p = new Process();
			p.StartInfo.FileName = ruta + "\\" + nombreArchivo + ".pdf";
			p.Start();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnImprimirTicket_Click(object sender, EventArgs e)
	{
		try
		{
			clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();
			clsTipoDocumento doc = admtd.BuscaTipoDocumento("ENTREGA");
			clsSerie ser = admSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
			clsConsultasExternas ext = new clsConsultasExternas();
			CREntregaFormatoContinuo rpt1 = new CREntregaFormatoContinuo();
			rpt1.SetDataSource(admdesp.ReporteImprimirEntrega(Convert.ToInt32(entrega.CodEntrega)).Tables[0]);
			PrintOptions rptoption = rpt1.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(40, 5, 0, 10));
			rpt1.PrintToPrinter(1, collated: false, 1, 1);
			rpt1.Close();
			rpt1.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmEntrega));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.colCodDetalleEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodDetalleDespacho = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidadPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.lblAnulado = new System.Windows.Forms.Label();
		this.txtAnulador = new System.Windows.Forms.TextBox();
		this.lblanular = new System.Windows.Forms.Label();
		this.dtpAnulacion = new System.Windows.Forms.DateTimePicker();
		this.lblFechaAnulador = new System.Windows.Forms.Label();
		this.btnAnular = new System.Windows.Forms.Button();
		this.txtNombreUsuario = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.txtRefEntrega = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnImprimirTicket = new System.Windows.Forms.Button();
		this.btnImprimirPDF = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvDetalle);
		this.groupBox1.Location = new System.Drawing.Point(0, 112);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(890, 275);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Control;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.colCodDetalleEntrega, this.colCodDetalleDespacho, this.colCodAlmacen, this.colDescAlmacen, this.colCodProducto, this.colReferencia, this.colDescProducto, this.colCodUnidad, this.colDescUnidad, this.colCantidad, this.colCantidadPendiente);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.Size = new System.Drawing.Size(884, 256);
		this.dgvDetalle.TabIndex = 1;
		this.colCodDetalleEntrega.DataPropertyName = "codDetalleEntrega";
		this.colCodDetalleEntrega.HeaderText = "codDetalleEntrega";
		this.colCodDetalleEntrega.Name = "colCodDetalleEntrega";
		this.colCodDetalleEntrega.Visible = false;
		this.colCodDetalleDespacho.DataPropertyName = "codDetalleDespacho";
		this.colCodDetalleDespacho.HeaderText = "codDetalleDespacho";
		this.colCodDetalleDespacho.Name = "colCodDetalleDespacho";
		this.colCodDetalleDespacho.Visible = false;
		this.colCodAlmacen.DataPropertyName = "codAlmacen";
		this.colCodAlmacen.HeaderText = "codAlmacen";
		this.colCodAlmacen.Name = "colCodAlmacen";
		this.colCodAlmacen.Visible = false;
		this.colDescAlmacen.DataPropertyName = "descAlmacen";
		this.colDescAlmacen.FillWeight = 71.06599f;
		this.colDescAlmacen.HeaderText = "Almacen";
		this.colDescAlmacen.Name = "colDescAlmacen";
		this.colDescAlmacen.ReadOnly = true;
		this.colCodProducto.DataPropertyName = "codProducto";
		this.colCodProducto.HeaderText = "codProducto";
		this.colCodProducto.Name = "colCodProducto";
		this.colCodProducto.ReadOnly = true;
		this.colCodProducto.Visible = false;
		this.colReferencia.DataPropertyName = "referencia";
		this.colReferencia.FillWeight = 71.06599f;
		this.colReferencia.HeaderText = "Referencia";
		this.colReferencia.Name = "colReferencia";
		this.colReferencia.ReadOnly = true;
		this.colDescProducto.DataPropertyName = "descProducto";
		this.colDescProducto.FillWeight = 215.736f;
		this.colDescProducto.HeaderText = "Descripcion";
		this.colDescProducto.Name = "colDescProducto";
		this.colDescProducto.ReadOnly = true;
		this.colCodUnidad.DataPropertyName = "codUnidad";
		this.colCodUnidad.HeaderText = "codUnidad";
		this.colCodUnidad.Name = "colCodUnidad";
		this.colCodUnidad.ReadOnly = true;
		this.colCodUnidad.Visible = false;
		this.colDescUnidad.DataPropertyName = "descUnidad";
		this.colDescUnidad.FillWeight = 71.06599f;
		this.colDescUnidad.HeaderText = "Unidad";
		this.colDescUnidad.Name = "colDescUnidad";
		this.colDescUnidad.ReadOnly = true;
		this.colCantidad.DataPropertyName = "cantidad";
		this.colCantidad.FillWeight = 71.06599f;
		this.colCantidad.HeaderText = "Cantidad";
		this.colCantidad.Name = "colCantidad";
		this.colCantidad.ReadOnly = true;
		this.colCantidadPendiente.DataPropertyName = "ctdadPendiente";
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.colCantidadPendiente.DefaultCellStyle = dataGridViewCellStyle1;
		this.colCantidadPendiente.HeaderText = "Cantidad Pendiente";
		this.colCantidadPendiente.Name = "colCantidadPendiente";
		this.colCantidadPendiente.ReadOnly = true;
		this.colCantidadPendiente.Visible = false;
		this.groupBox2.Controls.Add(this.btnImprimirTicket);
		this.groupBox2.Controls.Add(this.btnImprimirPDF);
		this.groupBox2.Controls.Add(this.lblAnulado);
		this.groupBox2.Controls.Add(this.txtAnulador);
		this.groupBox2.Controls.Add(this.lblanular);
		this.groupBox2.Controls.Add(this.dtpAnulacion);
		this.groupBox2.Controls.Add(this.lblFechaAnulador);
		this.groupBox2.Controls.Add(this.btnAnular);
		this.groupBox2.Controls.Add(this.txtNombreUsuario);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.dtpFechaEntrega);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.txtRefEntrega);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(890, 106);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.lblAnulado.AutoSize = true;
		this.lblAnulado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAnulado.ForeColor = System.Drawing.Color.Red;
		this.lblAnulado.Location = new System.Drawing.Point(296, 21);
		this.lblAnulado.Name = "lblAnulado";
		this.lblAnulado.Size = new System.Drawing.Size(71, 15);
		this.lblAnulado.TabIndex = 21;
		this.lblAnulado.Text = "ANULADO";
		this.txtAnulador.Location = new System.Drawing.Point(104, 71);
		this.txtAnulador.Name = "txtAnulador";
		this.txtAnulador.ReadOnly = true;
		this.txtAnulador.Size = new System.Drawing.Size(335, 20);
		this.txtAnulador.TabIndex = 20;
		this.txtAnulador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtAnulador.Visible = false;
		this.lblanular.AutoSize = true;
		this.lblanular.Location = new System.Drawing.Point(13, 75);
		this.lblanular.Name = "lblanular";
		this.lblanular.Size = new System.Drawing.Size(52, 13);
		this.lblanular.TabIndex = 19;
		this.lblanular.Text = "Anulador:";
		this.lblanular.Visible = false;
		this.dtpAnulacion.Enabled = false;
		this.dtpAnulacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpAnulacion.Location = new System.Drawing.Point(498, 71);
		this.dtpAnulacion.Name = "dtpAnulacion";
		this.dtpAnulacion.Size = new System.Drawing.Size(100, 20);
		this.dtpAnulacion.TabIndex = 18;
		this.dtpAnulacion.Visible = false;
		this.lblFechaAnulador.AutoSize = true;
		this.lblFechaAnulador.Location = new System.Drawing.Point(452, 74);
		this.lblFechaAnulador.Name = "lblFechaAnulador";
		this.lblFechaAnulador.Size = new System.Drawing.Size(40, 13);
		this.lblFechaAnulador.TabIndex = 17;
		this.lblFechaAnulador.Text = "Fecha:";
		this.lblFechaAnulador.Visible = false;
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAnular.Image = SIGEFA.Properties.Resources.x_button;
		this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAnular.Location = new System.Drawing.Point(809, 19);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(69, 33);
		this.btnAnular.TabIndex = 16;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.txtNombreUsuario.Location = new System.Drawing.Point(104, 45);
		this.txtNombreUsuario.Name = "txtNombreUsuario";
		this.txtNombreUsuario.ReadOnly = true;
		this.txtNombreUsuario.Size = new System.Drawing.Size(335, 20);
		this.txtNombreUsuario.TabIndex = 15;
		this.txtNombreUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(13, 49);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(64, 13);
		this.label3.TabIndex = 14;
		this.label3.Text = "Registrador:";
		this.dtpFechaEntrega.Enabled = false;
		this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaEntrega.Location = new System.Drawing.Point(498, 45);
		this.dtpFechaEntrega.Name = "dtpFechaEntrega";
		this.dtpFechaEntrega.Size = new System.Drawing.Size(100, 20);
		this.dtpFechaEntrega.TabIndex = 13;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(452, 48);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 13);
		this.label4.TabIndex = 12;
		this.label4.Text = "Fecha:";
		this.txtRefEntrega.Location = new System.Drawing.Point(104, 19);
		this.txtRefEntrega.Name = "txtRefEntrega";
		this.txtRefEntrega.ReadOnly = true;
		this.txtRefEntrega.Size = new System.Drawing.Size(100, 20);
		this.txtRefEntrega.TabIndex = 11;
		this.txtRefEntrega.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(67, 13);
		this.label1.TabIndex = 10;
		this.label1.Text = "Entrega Nro:";
		this.label1.Click += new System.EventHandler(label1_Click);
		this.btnImprimirTicket.FlatAppearance.BorderColor = System.Drawing.Color.White;
		this.btnImprimirTicket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
		this.btnImprimirTicket.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnImprimirTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimirTicket.Image = (System.Drawing.Image)resources.GetObject("btnImprimirTicket.Image");
		this.btnImprimirTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimirTicket.Location = new System.Drawing.Point(645, 58);
		this.btnImprimirTicket.Name = "btnImprimirTicket";
		this.btnImprimirTicket.Size = new System.Drawing.Size(106, 43);
		this.btnImprimirTicket.TabIndex = 179;
		this.btnImprimirTicket.Text = "IMPRIMIR";
		this.btnImprimirTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimirTicket.UseVisualStyleBackColor = true;
		this.btnImprimirTicket.Click += new System.EventHandler(btnImprimirTicket_Click);
		this.btnImprimirPDF.FlatAppearance.BorderColor = System.Drawing.Color.White;
		this.btnImprimirPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
		this.btnImprimirPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnImprimirPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimirPDF.Image = (System.Drawing.Image)resources.GetObject("btnImprimirPDF.Image");
		this.btnImprimirPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimirPDF.Location = new System.Drawing.Point(627, 12);
		this.btnImprimirPDF.Name = "btnImprimirPDF";
		this.btnImprimirPDF.Size = new System.Drawing.Size(137, 43);
		this.btnImprimirPDF.TabIndex = 178;
		this.btnImprimirPDF.Text = "IMPRIMIR PDF";
		this.btnImprimirPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimirPDF.UseVisualStyleBackColor = true;
		this.btnImprimirPDF.Click += new System.EventHandler(btnImprimirPDF_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(890, 387);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmEntrega";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Entrega";
		base.Load += new System.EventHandler(frmEntrega_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
