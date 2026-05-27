using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoGuiaRemisionCompra : Form
{
	private BindingSource dataEnlace = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmGuiaRemisionCompra AdmGuiaRC = new clsAdmGuiaRemisionCompra();

	public int codOCAMostrar = 0;

	private IContainer components = null;

	private GroupBox groupBox2;

	private Label label6;

	private Label label2;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private GroupBox groupBox1;

	private GroupBox groupBox3;

	private Button btnAnular;

	private Button btGenVenta;

	private Button btnIrGuia;

	private Button btnSalir;

	private ImageList imageList1;

	private Button btnActualizar;

	private RadGridView rgvListadoGR;

	private RadioButton rbFechaTraslado;

	private RadioButton rbFechaRegistro;

	private RadioButton rbFechaEmision;

	private Label txtNombreProducto;

	private Label label9;

	private Button btnBuscarProducto;

	private Label label4;

	private TextBox txtCodprod;

	public frmListadoGuiaRemisionCompra()
	{
		InitializeComponent();
	}

	private void frmListadoGuiaRemisionCompra_Load(object sender, EventArgs e)
	{
		if (codOCAMostrar != 0)
		{
			int codigo = AdmGuiaRC.getCodigoPrimeraGRCGenerada(codOCAMostrar);
			if (codigo != 0)
			{
				clsGuiaRemision aux = AdmGuiaRC.CargaGuiaRemision(codigo);
				dtpFecha1.Value = aux.FechaRegistro;
			}
		}
		CargaLista();
		if (codOCAMostrar != 0)
		{
			dataEnlace.Filter = string.Format("[{0}] like '*{1}*'", "codOrdenCompra", codOCAMostrar.ToString().PadLeft(11, '0'));
			rgvListadoGR.DataSource = dataEnlace;
		}
	}

	private void CargaLista()
	{
		int tipo = (rbFechaEmision.Checked ? 1 : (rbFechaRegistro.Checked ? 2 : (rbFechaTraslado.Checked ? 3 : 0)));
		rgvListadoGR.DataSource = dataEnlace;
		dataEnlace.DataSource = AdmGuiaRC.MuestraGuiaRemisiones(0, frmLogin.iCodSucursal, tipo, dtpFecha1.Value, dtpFecha2.Value, Convert.ToInt32((txtCodprod.Text == "") ? "0" : txtCodprod.Text));
		dataEnlace.Filter = string.Empty;
		filtro = string.Empty;
		rgvListadoGR.ClearSelection();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnIrGuia_Click(object sender, EventArgs e)
	{
		if (rgvListadoGR.SelectedRows.Count == 0)
		{
			MessageBox.Show("Seleccione una fila", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		frmGuiaRemisionCompra form = buscarFrmGRC("frmGuiaRemisionCompra", Convert.ToInt32(rgvListadoGR.SelectedRows[0].Cells["codigo"].Value));
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmGuiaRemisionCompra();
		foreach (GridViewRowInfo fila in rgvListadoGR.SelectedRows)
		{
			form.label24.Text = fila.Cells["codProveedor"].Value.ToString();
			form.OrdenOC = Convert.ToInt32(fila.Cells["orden"].Value.ToString());
		}
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Editar = true;
		form.MdiParent = base.MdiParent;
		form.codGuiaRemisionCompraAEditar = Convert.ToInt32(rgvListadoGR.SelectedRows[0].Cells["codigo"].Value);
		form.Show();
	}

	private frmGuiaRemisionCompra buscarFrmGRC(string tipoFormulario, int codGRC)
	{
		frmGuiaRemisionCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmGuiaRemisionCompra)frm).codGuiaRemisionCompraAEditar == codGRC)
			{
				form = (frmGuiaRemisionCompra)frm;
				break;
			}
		}
		return form;
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error Filtrado - " + Text);
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvListadoGR.SelectedRows.Count == 1)
			{
				clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();
				GridViewRowInfo fila = rgvListadoGR.SelectedRows[0];
				int cod_g_r_c = Convert.ToInt32(fila.Cells["codigo"].Value);
				if (AdmGuiaRC.anularGuiaRemisionCompra(cod_g_r_c))
				{
					clsGuiaRemision grc = AdmGuiaRC.CargaGuiaRemision(cod_g_r_c);
					if (grc.ICodOrdenCompra != 0)
					{
						DataTable listadoProdNoAtendidos = AdmOrd.generarGuiaRemisionOrdenCompra(grc.ICodOrdenCompra);
						if (listadoProdNoAtendidos.Rows.Count > 0)
						{
							AdmOrd.actualizaestadocabeceraorden(grc.ICodOrdenCompra, 2);
						}
						else
						{
							AdmOrd.actualizaestadocabeceraorden(grc.ICodOrdenCompra, 3);
						}
					}
					MessageBox.Show("Guia de Remision de Compra Anulada con Exito", "Anulacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show("Ocurrio un problema al anular Guia de Remision de Compra", "Anulacion Guia Remision COmpra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Seleccione un fila para poder anular.");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error al intentar anular guia remision compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvListadoGR.SelectedRows.Count == 1)
			{
				clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();
				GridViewRowInfo fila = rgvListadoGR.SelectedRows[0];
				int cod_g_r_c = Convert.ToInt32(fila.Cells["codigo"].Value);
				frmListadoDocumentoRelacionadoGuiaRemisionCompra form = new frmListadoDocumentoRelacionadoGuiaRemisionCompra();
				form.codGRC = cod_g_r_c;
				form.Show();
			}
			else
			{
				MessageBox.Show("Seleccione un fila para poder anular.");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error al intentar anular guia remision compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		CargaLista();
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListadoGuiaRemisionCompra));
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rbFechaTraslado = new System.Windows.Forms.RadioButton();
		this.rbFechaRegistro = new System.Windows.Forms.RadioButton();
		this.rbFechaEmision = new System.Windows.Forms.RadioButton();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvListadoGR = new Telerik.WinControls.UI.RadGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnAnular = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrGuia = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGR).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGR.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox2.Controls.Add(this.rbFechaTraslado);
		this.groupBox2.Controls.Add(this.rbFechaRegistro);
		this.groupBox2.Controls.Add(this.rbFechaEmision);
		this.groupBox2.Controls.Add(this.txtNombreProducto);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.btnBuscarProducto);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.txtCodprod);
		this.groupBox2.Controls.Add(this.btnActualizar);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.dtpFecha2);
		this.groupBox2.Controls.Add(this.dtpFecha1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1034, 81);
		this.groupBox2.TabIndex = 9;
		this.groupBox2.TabStop = false;
		this.rbFechaTraslado.AutoSize = true;
		this.rbFechaTraslado.Location = new System.Drawing.Point(12, 17);
		this.rbFechaTraslado.Name = "rbFechaTraslado";
		this.rbFechaTraslado.Size = new System.Drawing.Size(99, 17);
		this.rbFechaTraslado.TabIndex = 79;
		this.rbFechaTraslado.Text = "Fecha Traslado";
		this.rbFechaTraslado.UseVisualStyleBackColor = true;
		this.rbFechaRegistro.AutoSize = true;
		this.rbFechaRegistro.Checked = true;
		this.rbFechaRegistro.Location = new System.Drawing.Point(215, 17);
		this.rbFechaRegistro.Name = "rbFechaRegistro";
		this.rbFechaRegistro.Size = new System.Drawing.Size(97, 17);
		this.rbFechaRegistro.TabIndex = 78;
		this.rbFechaRegistro.TabStop = true;
		this.rbFechaRegistro.Text = "Fecha Registro";
		this.rbFechaRegistro.UseVisualStyleBackColor = true;
		this.rbFechaEmision.AutoSize = true;
		this.rbFechaEmision.Location = new System.Drawing.Point(115, 17);
		this.rbFechaEmision.Name = "rbFechaEmision";
		this.rbFechaEmision.Size = new System.Drawing.Size(94, 17);
		this.rbFechaEmision.TabIndex = 77;
		this.rbFechaEmision.Text = "Fecha Emision";
		this.rbFechaEmision.UseVisualStyleBackColor = true;
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(552, 54);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 76;
		this.txtNombreProducto.Text = "---";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(511, 54);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(35, 13);
		this.label9.TabIndex = 75;
		this.label9.Text = "Prod.:";
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(471, 44);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 74;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(324, 26);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(112, 13);
		this.label4.TabIndex = 73;
		this.label4.Text = "Busqueda x Producto:";
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(327, 47);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 72;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.Image = SIGEFA.Properties.Resources.cambio;
		this.btnActualizar.Location = new System.Drawing.Point(939, 17);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(83, 37);
		this.btnActualizar.TabIndex = 39;
		this.btnActualizar.Text = "Buscar";
		this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnActualizar.UseVisualStyleBackColor = true;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.label6.Location = new System.Drawing.Point(921, 29);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 37;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(96, 38);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 26;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(9, 38);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 25;
		this.label1.Text = "Desde";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(99, 54);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 22;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(12, 54);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 21;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvListadoGR);
		this.groupBox1.Location = new System.Drawing.Point(0, 87);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1034, 397);
		this.groupBox1.TabIndex = 10;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Listado de Guias de Remision de Compras";
		this.rgvListadoGR.AutoScroll = true;
		this.rgvListadoGR.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvListadoGR.Location = new System.Drawing.Point(3, 16);
		this.rgvListadoGR.MasterTemplate.AllowAddNewRow = false;
		this.rgvListadoGR.MasterTemplate.AllowCellContextMenu = false;
		this.rgvListadoGR.MasterTemplate.AllowColumnChooser = false;
		this.rgvListadoGR.MasterTemplate.AllowColumnReorder = false;
		this.rgvListadoGR.MasterTemplate.AllowDeleteRow = false;
		this.rgvListadoGR.MasterTemplate.AllowDragToGroup = false;
		this.rgvListadoGR.MasterTemplate.AllowEditRow = false;
		this.rgvListadoGR.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codGuiaRemisionCompra";
		gridViewTextBoxColumn1.HeaderText = "Codigo";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codigo";
		gridViewTextBoxColumn1.Width = 71;
		gridViewTextBoxColumn2.FieldName = "numdocgrc";
		gridViewTextBoxColumn2.HeaderText = "Nº Guia R.";
		gridViewTextBoxColumn2.Name = "numdoc";
		gridViewTextBoxColumn2.Width = 85;
		gridViewTextBoxColumn3.FieldName = "facturaCompra";
		gridViewTextBoxColumn3.HeaderText = "Factura";
		gridViewTextBoxColumn3.Name = "factura";
		gridViewTextBoxColumn3.Width = 85;
		gridViewTextBoxColumn4.FieldName = "codOrdenCompra";
		gridViewTextBoxColumn4.HeaderText = "Nº Orden Compra";
		gridViewTextBoxColumn4.Name = "numOrdenCompra";
		gridViewTextBoxColumn4.Width = 71;
		gridViewTextBoxColumn5.FieldName = "proveedor";
		gridViewTextBoxColumn5.HeaderText = "Proveedor";
		gridViewTextBoxColumn5.Name = "cliente";
		gridViewTextBoxColumn5.Width = 85;
		gridViewTextBoxColumn6.FieldName = "fechaTraslado";
		gridViewTextBoxColumn6.HeaderText = "F. Traslado";
		gridViewTextBoxColumn6.Name = "fecha";
		gridViewTextBoxColumn6.Width = 85;
		gridViewTextBoxColumn7.FieldName = "fechaEmision";
		gridViewTextBoxColumn7.HeaderText = "F. Emision";
		gridViewTextBoxColumn7.Name = "fechaemision";
		gridViewTextBoxColumn7.Width = 85;
		gridViewTextBoxColumn8.FieldName = "fechaRegistro";
		gridViewTextBoxColumn8.HeaderText = "F. Registro";
		gridViewTextBoxColumn8.Name = "colFechaRegistro";
		gridViewTextBoxColumn8.Width = 71;
		gridViewTextBoxColumn9.FieldName = "responsable";
		gridViewTextBoxColumn9.HeaderText = "Responsable";
		gridViewTextBoxColumn9.Name = "responsable";
		gridViewTextBoxColumn9.Width = 85;
		gridViewTextBoxColumn10.FieldName = "estado";
		gridViewTextBoxColumn10.HeaderText = "Estado";
		gridViewTextBoxColumn10.Name = "colEstado";
		gridViewTextBoxColumn10.Width = 85;
		gridViewTextBoxColumn11.FieldName = "facturaFlete";
		gridViewTextBoxColumn11.HeaderText = "Factura Flete";
		gridViewTextBoxColumn11.Name = "colFacturaFlete";
		gridViewTextBoxColumn11.Width = 71;
		gridViewTextBoxColumn12.FieldName = "facturaVenta";
		gridViewTextBoxColumn12.HeaderText = "Factura Venta";
		gridViewTextBoxColumn12.Name = "colFacturaVenta";
		gridViewTextBoxColumn12.Width = 71;
		gridViewTextBoxColumn13.FieldName = "notaCredito";
		gridViewTextBoxColumn13.HeaderText = "Nota Credito";
		gridViewTextBoxColumn13.Name = "colNotaCredito";
		gridViewTextBoxColumn13.Width = 72;
		gridViewTextBoxColumn14.FieldName = "codProveedor";
		gridViewTextBoxColumn14.HeaderText = "codproveedor";
		gridViewTextBoxColumn14.Name = "codproveedor";
		gridViewTextBoxColumn14.Width = 43;
		gridViewTextBoxColumn15.FieldName = "orden";
		gridViewTextBoxColumn15.HeaderText = "orden";
		gridViewTextBoxColumn15.Name = "orden";
		gridViewTextBoxColumn15.Width = 46;
		this.rgvListadoGR.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15);
		this.rgvListadoGR.MasterTemplate.EnableFiltering = true;
		this.rgvListadoGR.MasterTemplate.EnableGrouping = false;
		this.rgvListadoGR.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvListadoGR.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvListadoGR.Name = "rgvListadoGR";
		this.rgvListadoGR.ReadOnly = true;
		this.rgvListadoGR.Size = new System.Drawing.Size(1028, 378);
		this.rgvListadoGR.TabIndex = 2;
		this.groupBox3.Controls.Add(this.btnAnular);
		this.groupBox3.Controls.Add(this.btGenVenta);
		this.groupBox3.Controls.Add(this.btnIrGuia);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 490);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1034, 100);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(48, 32);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 8;
		this.btnAnular.Text = "Anular Guia";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.ImageIndex = 2;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(739, 32);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(124, 37);
		this.btGenVenta.TabIndex = 7;
		this.btGenVenta.Text = "Ver Documentos Generados";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnIrGuia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrGuia.ImageIndex = 1;
		this.btnIrGuia.ImageList = this.imageList1;
		this.btnIrGuia.Location = new System.Drawing.Point(869, 32);
		this.btnIrGuia.Name = "btnIrGuia";
		this.btnIrGuia.Size = new System.Drawing.Size(72, 37);
		this.btnIrGuia.TabIndex = 6;
		this.btnIrGuia.Text = "Abrir";
		this.btnIrGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrGuia.UseVisualStyleBackColor = true;
		this.btnIrGuia.Click += new System.EventHandler(btnIrGuia_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(947, 32);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 5;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1034, 590);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Name = "frmListadoGuiaRemisionCompra";
		this.Text = "Listado de Guias de Remision Compra";
		base.Load += new System.EventHandler(frmListadoGuiaRemisionCompra_Load);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGR.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGR).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
