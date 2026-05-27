using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmRegistroComprasLE : Office2007Form
{
	private clsValidar val = new clsValidar();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsAdmFactura admCompras = new clsAdmFactura();

	private List<string> listalibros = new List<string>();

	private clsAdmLibrosElectronicos admLE = new clsAdmLibrosElectronicos();

	private DataGridViewTextBoxColumn colum;

	private DataTable dt_ventas = new DataTable();

	private DataTable dt_compras = new DataTable();

	public int tipoLibroRecibido = 0;

	public int tipoRegistroRecibido = 0;

	public string Periodo = "";

	public int MesPeriodo = 0;

	private string AnalizarRuc = "";

	private int codTipoDoc = 0;

	private decimal BI = default(decimal);

	private decimal DBI = default(decimal);

	public int contenidoLibro;

	private IContainer components = null;

	private ImageList imageList2;

	private ImageList imageList1;

	private Label label1;

	private Panel panel3;

	private Button btnSalir;

	public TextBox txtnombrelibro;

	private GroupBox groupBox1;

	private DataGridView dgvVentas;

	private Label label2;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	public Button btnGuardar;

	private ButtonItem buttonItem1;

	public Button btnExit;

	public frmRegistroComprasLE()
	{
		InitializeComponent();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!(txtnombrelibro.Text != "") || !(txtnombrelibro.Text != "."))
		{
			return;
		}
		SaveFileDialog Save = new SaveFileDialog();
		StreamWriter myStreamWriter = null;
		Save.FileName = txtnombrelibro.Text;
		Save.Filter = "Text (*.txt)|*.txt|HTML(*.html*)|*.html|All files(*.*)|*.*";
		Save.CheckPathExists = true;
		Save.Title = "Guardar como";
		Save.ShowDialog(this);
		try
		{
			myStreamWriter = File.AppendText(Save.FileName);
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("ListaGuias");
			foreach (DataGridViewColumn column in dgvVentas.Columns)
			{
				DataColumn dc = new DataColumn(column.Name.ToString());
				dt.Columns.Add(dc);
			}
			for (int i = 0; i < dgvVentas.Rows.Count; i++)
			{
				DataGridViewRow row = dgvVentas.Rows[i];
				DataRow dr = dt.NewRow();
				for (int j = 0; j < dgvVentas.Columns.Count; j++)
				{
					dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString().Trim());
				}
				dt.Rows.Add(dr);
			}
			ds.Tables.Add(dt);
			string cad = "";
			Type t = null;
			for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
			{
				for (int l = 0; l < ds.Tables[0].Columns.Count; l++)
				{
					if ((ds.Tables[0].Columns[l].ColumnName == "Fecha Comp." || ds.Tables[0].Columns[l].ColumnName == "Fecha Venc/Pago") && ds.Tables[0].Rows[k][l].ToString() != "")
					{
						ds.Tables[0].Rows[k][l] = $"{Convert.ToDateTime(ds.Tables[0].Rows[k][l]):dd/MM/yyyy}";
					}
					if (l == ds.Tables[0].Columns.Count - 1)
					{
						cad = cad + ds.Tables[0].Rows[k][l].ToString() + "\t";
						myStreamWriter.WriteLine(cad);
						cad = "";
					}
					else
					{
						cad = cad + ds.Tables[0].Rows[k][l].ToString() + "|";
					}
				}
			}
			myStreamWriter.Flush();
		}
		catch (Exception)
		{
		}
	}

	private void txtMontoLiquidar_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void frmRegistroComprasLE_Load(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (tipoLibroRecibido == 13)
			{
				if (tipoRegistroRecibido == 47)
				{
					LLenarLista_ColumnasLVentas();
					crearColumnas(listalibros);
					if (contenidoLibro != 0)
					{
						CargarGrillaVentas();
					}
				}
				if (tipoRegistroRecibido == 48)
				{
					LLenarLista_ColumnasVentasSimplificado();
					if (contenidoLibro != 0)
					{
						CargarGrillaVentasSimplificado();
					}
				}
			}
			else if (tipoLibroRecibido == 8)
			{
				if (tipoRegistroRecibido == 36)
				{
					LLenarLista_ColumnasLCompras();
					crearColumnas(listalibros);
					if (contenidoLibro != 0)
					{
						cargarGrillaCompras("FacturasComprasLE");
					}
				}
				else if (tipoRegistroRecibido == 37)
				{
					LLenarLista_ColumnasCompras_NoDomiciliados();
					crearColumnas(listalibros);
					if (contenidoLibro != 0)
					{
						cargarGrillaComprasNoDomiciliadas("FacturasComprasOPnoDomicLE");
					}
				}
				else if (tipoRegistroRecibido == 38)
				{
					LLenarLista_ColumnasComprasSimplificado();
					if (contenidoLibro != 0)
					{
						cargarGrillaComprasSimplificadas("FacturasComprasSimplificadoLE");
					}
				}
			}
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message);
		}
	}

	public void LLenarLista_ColumnasLVentas()
	{
		listalibros.Clear();
		listalibros.Add("Periodo");
		listalibros.Add("CUO");
		listalibros.Add("Estado CUO");
		listalibros.Add("Fecha Comp.");
		listalibros.Add("Fecha Venc/Pago");
		listalibros.Add("Tipo Doc. Pago");
		listalibros.Add("Nro. Serie");
		listalibros.Add("Nro. Comprob.");
		listalibros.Add("Consolidacion");
		listalibros.Add("Tipo Doc. Iden.");
		listalibros.Add("Documento Iden.");
		listalibros.Add("Razon Social");
		listalibros.Add("Valor Export.");
		listalibros.Add("Base Imponible");
		listalibros.Add("Dscto Base Impo.");
		listalibros.Add("IGV");
		listalibros.Add("Dscto IGV");
		listalibros.Add("OP. Exhon.");
		listalibros.Add("OP. Inafecta");
		listalibros.Add("ISC");
		listalibros.Add("IGV Arroz Pilado");
		listalibros.Add("Imp. Ventas Arroz Pilado");
		listalibros.Add("Otros Tributos");
		listalibros.Add("Importe Total Ope.");
		listalibros.Add("Moneda");
		listalibros.Add("Tipo Cambio");
		listalibros.Add("Fecha Emision Comprob.");
		listalibros.Add("Tipo Comprob.");
		listalibros.Add("Nro Serie Comprob.");
		listalibros.Add("Nro Comprob. Pago");
		listalibros.Add("Proyecto");
		listalibros.Add("Error Tipo 1");
		listalibros.Add("Medios de Pago");
		listalibros.Add("Estado");
		listalibros.Add("CampoLibre");
	}

	public void LLenarLista_ColumnasVentasSimplificado()
	{
		listalibros.Clear();
		listalibros.Add("Periodo");
		listalibros.Add("CUO");
		listalibros.Add("Estado CUO");
		listalibros.Add("Fecha Comp.");
		listalibros.Add("Fecha Venc/Pago");
		listalibros.Add("Tipo Doc. Pago");
		listalibros.Add("Nro. Serie");
		listalibros.Add("Nro. Comprob.");
		listalibros.Add("Consolidacion");
		listalibros.Add("Tipo Doc. Iden.");
		listalibros.Add("Documento Iden.");
		listalibros.Add("Razon Social");
		listalibros.Add("Base Imponible");
		listalibros.Add("IGV");
		listalibros.Add("Otros Tributos");
		listalibros.Add("Importe Total Ope.");
		listalibros.Add("Moneda");
		listalibros.Add("Tipo Cambio");
		listalibros.Add("Fecha Emision Comprob.");
		listalibros.Add("Tipo Comprob.");
		listalibros.Add("Nro Serie Comprob.");
		listalibros.Add("Nro Comprob. Pago");
		listalibros.Add("Error Tipo 1");
		listalibros.Add("Medios de Pago");
		listalibros.Add("Estado");
		listalibros.Add("CampoLibre");
	}

	public void LLenarLista_ColumnasLCompras()
	{
		listalibros.Clear();
		listalibros.Add("Periodo");
		listalibros.Add("CUO");
		listalibros.Add("Estado CUO");
		listalibros.Add("Fecha Comp.");
		listalibros.Add("Fecha Venc/Pago");
		listalibros.Add("Tipo Doc. Pago");
		listalibros.Add("Nro. Serie");
		listalibros.Add("Año Emision");
		listalibros.Add("Nro. Comprob.");
		listalibros.Add("Consolidacion");
		listalibros.Add("Tipo Doc. Proveedor");
		listalibros.Add("Documento Iden.");
		listalibros.Add("Razon Social");
		listalibros.Add("Base Imponible 1");
		listalibros.Add("IGV 1");
		listalibros.Add("Base Imponible 2");
		listalibros.Add("IGV 2");
		listalibros.Add("Base Imponible 3");
		listalibros.Add("IGV 3");
		listalibros.Add("Valor Adquisicion");
		listalibros.Add("Monto Impuesto Selectivo");
		listalibros.Add("Otros Conceptos");
		listalibros.Add("Importe Total Ope.");
		listalibros.Add("Moneda");
		listalibros.Add("Tipo Cambio");
		listalibros.Add("Fecha Emision Comprob.");
		listalibros.Add("Tipo Comprob. Modifi.");
		listalibros.Add("Nro Serie Comprob. Modifi.");
		listalibros.Add("Codigo Aduana");
		listalibros.Add("Nro Comprob. Pago Modifi.");
		listalibros.Add("Fecha Constan. Detrac.");
		listalibros.Add("Numero Constan. Detrac.");
		listalibros.Add("Marca Comprob. Pago");
		listalibros.Add("Clasificacion Serv.");
		listalibros.Add("Proyecto");
		listalibros.Add("Error Tipo 1");
		listalibros.Add("Error Tipo 2");
		listalibros.Add("Error Tipo 3");
		listalibros.Add("Error Tipo 4");
		listalibros.Add("Indicador Compr. Pago");
		listalibros.Add("Estado");
		listalibros.Add("CampoLibre");
	}

	public void LLenarLista_ColumnasCompras_NoDomiciliados()
	{
		listalibros.Clear();
		listalibros.Add("Periodo");
		listalibros.Add("CUO");
		listalibros.Add("Estado CUO");
		listalibros.Add("Fecha Comp.");
		listalibros.Add("Tipo Doc. Pago");
		listalibros.Add("Nro. Serie");
		listalibros.Add("Nro. Comprob.");
		listalibros.Add("Valor Adquisicion");
		listalibros.Add("Otros Conceptos");
		listalibros.Add("Importe Total Ope.");
		listalibros.Add("Tipo Comprob. Modifi.");
		listalibros.Add("Nro Serie Comprob. Modifi.");
		listalibros.Add("Año Emision del Comprob.");
		listalibros.Add("Nro Comprob. Pago Modifi.");
		listalibros.Add("Monto Retencion");
		listalibros.Add("Moneda");
		listalibros.Add("Tipo Cambio");
		listalibros.Add("Pais Residencia");
		listalibros.Add("Razon Social");
		listalibros.Add("Domicilio_Extranjero");
		listalibros.Add("Identificacion");
		listalibros.Add("Identificacion_Bancaria");
		listalibros.Add("Beneficiario");
		listalibros.Add("Pais_Beneficiario");
		listalibros.Add("Vinculo_contrib_Benef");
		listalibros.Add("Renta_Bruta");
		listalibros.Add("Deduccion");
		listalibros.Add("Renta_neta");
		listalibros.Add("Tasa_Reten");
		listalibros.Add("Impuesto_Reten");
		listalibros.Add("Convenios");
		listalibros.Add("Exoneracion");
		listalibros.Add("Tipo_Renta");
		listalibros.Add("Modalidad_Serv");
		listalibros.Add("AplicaImpRenta");
		listalibros.Add("Estado");
		listalibros.Add("CampoLibre");
	}

	public void LLenarLista_ColumnasComprasSimplificado()
	{
		listalibros.Add("Periodo");
		listalibros.Add("CUO");
		listalibros.Add("Estado CUO");
		listalibros.Add("Fecha Comp.");
		listalibros.Add("Fecha Venc/Pago");
		listalibros.Add("Tipo Doc. Pago");
		listalibros.Add("Nro. Serie");
		listalibros.Add("Nro. Comprob.");
		listalibros.Add("Operaciones_Diarias");
		listalibros.Add("Tipo Doc. Proveedor");
		listalibros.Add("Documento Iden.");
		listalibros.Add("Razon Social");
		listalibros.Add("Base Imponible 1");
		listalibros.Add("IGV 1");
		listalibros.Add("Otros Conceptos");
		listalibros.Add("Importe Total Ope.");
		listalibros.Add("Moneda");
		listalibros.Add("Tipo Cambio");
		listalibros.Add("Fecha Emision Comprob.");
		listalibros.Add("Tipo Comprob. Modifi.");
		listalibros.Add("Nro Serie Comprob. Modifi.");
		listalibros.Add("Nro Comprob. Pago Modifi.");
		listalibros.Add("Fecha Constan. Detrac.");
		listalibros.Add("Numero Constan. Detrac.");
		listalibros.Add("Marca Comprob. Pago");
		listalibros.Add("Clasificacion Serv.");
		listalibros.Add("Error Tipo 1");
		listalibros.Add("Error Tipo 2");
		listalibros.Add("Error Tipo 3");
		listalibros.Add("Indicador Compr. Pago");
		listalibros.Add("Estado");
		listalibros.Add("CampoLibre");
	}

	private void crearColumnas(List<string> ltaV)
	{
		List<string> ltaTrabajada = new List<string>();
		ltaTrabajada = ltaV;
		int index = 0;
		dgvVentas.Columns.Clear();
		foreach (string dato in ltaTrabajada)
		{
			colum = new DataGridViewTextBoxColumn();
			colum.Name = dato;
			colum.DataPropertyName = dato;
			colum.HeaderText = dato.ToUpper();
			colum.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			colum.DisplayIndex = index;
			colum.CellTemplate = new DataGridViewTextBoxCell();
			dgvVentas.Columns.Add(colum);
			if (colum.Name == "Fecha Comp." || colum.Name == "Fecha Venc/Pago")
			{
				colum.DefaultCellStyle.Format = "dd/MM/yyyy";
			}
			index++;
		}
	}

	private void CargarGrillaVentas()
	{
		dt_ventas.Clear();
		dt_ventas = admLE.GetVentas_Mes_LEV(MesPeriodo);
		dgvVentas.Rows.Clear();
		foreach (DataRow row in dt_ventas.Rows)
		{
			AnalizarRuc = row[6].ToString();
			BI = Convert.ToDecimal(row[8]);
			DBI = Convert.ToDecimal(row[9]);
			dgvVentas.Rows.Add(Periodo, row[0], "M - " + row[0], Convert.ToDateTime(row[1]).Date, Convert.ToDateTime(row[2]).Date, row[3], row[4], row[5], "", AnalizarTipoDocumento(AnalizarRuc), row[6], row[7], "", row[8], row[9], row[10], AnalizarDCTO_IGV(BI, DBI), "0", "0", "0", "0", "0", "0", row[11], row[12], row[13], "", "", "", "", "", "", "", "1", row[14]);
		}
	}

	private void CargarGrillaVentasSimplificado()
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			dt_ventas.Clear();
			dt_ventas = admLE.GetVentas_Mes_LEV2(MesPeriodo, Periodo);
			if (dt_ventas != null)
			{
				dgvVentas.Rows.Clear();
				dgvVentas.DataSource = dt_ventas;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message);
		}
	}

	private void cargarGrillaCompras(string cadena)
	{
		dt_compras.Clear();
		dt_compras = admLE.FacturasComprasLE(MesPeriodo, frmLogin.iCodAlmacen, cadena, Periodo);
		dgvVentas.Rows.Clear();
		foreach (DataRow row in dt_compras.Rows)
		{
			dgvVentas.Rows.Add(Periodo, row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17], row[18], row[19], row[20], row[21], row[22], row[23], row[24], row[25], row[26], row[27], row[28], row[29], row[30], row[31], row[32], row[33], row[34], row[35], row[36], row[37], row[38], row[39], row[40], row[41]);
		}
		dgvVentas.Columns["CampoLibre"].Visible = false;
	}

	private void cargarGrillaComprasNoDomiciliadas(string cadena)
	{
		dt_compras.Clear();
		dgvVentas.Rows.Clear();
		foreach (DataRow row in dt_compras.Rows)
		{
			dgvVentas.Rows.Add(Periodo, row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17], row[18], row[19], row[20], row[21], row[22], row[23], row[24], row[25], row[26], row[27], row[28], row[29], row[30], row[31], row[32], row[33], row[34], row[35], row[36]);
		}
		dgvVentas.Columns["CampoLibre"].Visible = false;
	}

	private void cargarGrillaComprasSimplificadas(string cadena)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			dt_compras.Clear();
			dt_compras = admLE.FacturasComprasLE(MesPeriodo, frmLogin.iCodAlmacen, cadena, Periodo);
			if (dt_compras != null)
			{
				dgvVentas.Rows.Clear();
				dgvVentas.DataSource = dt_compras;
				dgvVentas.Columns["CampoLibre"].Visible = false;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message);
		}
	}

	private int AnalizarTipoDocumento(string nrodocu)
	{
		if (nrodocu.Trim().Length == 8)
		{
			codTipoDoc = 1;
		}
		else if (nrodocu.Trim().Length == 11)
		{
			codTipoDoc = 6;
		}
		return codTipoDoc;
	}

	private decimal AnalizarDCTO_IGV(decimal BaseImponible, decimal DesctoBI)
	{
		decimal DctoIGV = default(decimal);
		decimal MontoDscto = default(decimal);
		MontoDscto = BaseImponible - DesctoBI;
		if (DesctoBI != 0m)
		{
			return MontoDscto * 18m / 100m;
		}
		return DctoIGV;
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRegistroComprasLE));
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.txtnombrelibro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.btnExit = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvVentas = new System.Windows.Forms.DataGridView();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.panel3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).BeginInit();
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
		this.txtnombrelibro.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.txtnombrelibro.Enabled = false;
		this.txtnombrelibro.Font = new System.Drawing.Font("Arial", 9.75f);
		this.txtnombrelibro.Location = new System.Drawing.Point(351, 19);
		this.txtnombrelibro.Multiline = true;
		this.txtnombrelibro.Name = "txtnombrelibro";
		this.txtnombrelibro.ReadOnly = true;
		this.txtnombrelibro.Size = new System.Drawing.Size(355, 32);
		this.txtnombrelibro.TabIndex = 36;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(244, 32);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(101, 13);
		this.label1.TabIndex = 11;
		this.label1.Text = "Nombre de Archivo:";
		this.panel3.BackColor = System.Drawing.Color.Lavender;
		this.panel3.Controls.Add(this.btnExit);
		this.panel3.Controls.Add(this.label2);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.btnGuardar);
		this.panel3.Controls.Add(this.dtpDesde);
		this.panel3.Controls.Add(this.dtpHasta);
		this.panel3.Controls.Add(this.label1);
		this.panel3.Controls.Add(this.txtnombrelibro);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 363);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1064, 78);
		this.panel3.TabIndex = 16;
		this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.btnExit.ImageIndex = 0;
		this.btnExit.ImageList = this.imageList2;
		this.btnExit.Location = new System.Drawing.Point(965, 12);
		this.btnExit.Name = "btnExit";
		this.btnExit.Size = new System.Drawing.Size(84, 45);
		this.btnExit.TabIndex = 52;
		this.btnExit.Text = "Salir";
		this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExit.UseVisualStyleBackColor = true;
		this.btnExit.Click += new System.EventHandler(btnExit_Click);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 49);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 13);
		this.label2.TabIndex = 51;
		this.label2.Text = "Hasta :";
		this.label2.Visible = false;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 21);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 50;
		this.label5.Text = "Desde :";
		this.label5.Visible = false;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 3;
		this.btnGuardar.ImageList = this.imageList2;
		this.btnGuardar.Location = new System.Drawing.Point(727, 12);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(84, 45);
		this.btnGuardar.TabIndex = 34;
		this.btnGuardar.Text = "Guardar Registro";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(59, 18);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 49;
		this.dtpDesde.Visible = false;
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(59, 46);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 48;
		this.dtpHasta.Visible = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Enabled = false;
		this.btnSalir.ImageIndex = 3;
		this.btnSalir.ImageList = this.imageList2;
		this.btnSalir.Location = new System.Drawing.Point(965, 357);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(84, 68);
		this.btnSalir.TabIndex = 46;
		this.btnSalir.Text = "Guardar Registro";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.groupBox1.Controls.Add(this.dgvVentas);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1064, 363);
		this.groupBox1.TabIndex = 47;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Ventas";
		this.dgvVentas.AllowUserToAddRows = false;
		this.dgvVentas.AllowUserToDeleteRows = false;
		this.dgvVentas.AllowUserToOrderColumns = true;
		this.dgvVentas.AllowUserToResizeRows = false;
		this.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvVentas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVentas.Location = new System.Drawing.Point(3, 16);
		this.dgvVentas.MultiSelect = false;
		this.dgvVentas.Name = "dgvVentas";
		this.dgvVentas.ReadOnly = true;
		this.dgvVentas.RowHeadersVisible = false;
		this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVentas.Size = new System.Drawing.Size(1058, 344);
		this.dgvVentas.TabIndex = 0;
		this.buttonItem1.Image = (System.Drawing.Image)resources.GetObject("buttonItem1.Image");
		this.buttonItem1.ImageIndex = 20;
		this.buttonItem1.ImagePaddingHorizontal = 30;
		this.buttonItem1.ImagePaddingVertical = 15;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "  Salir";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1064, 441);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.btnSalir);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.MinimumSize = new System.Drawing.Size(1070, 470);
		base.Name = "frmRegistroComprasLE";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Libros Electronicos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRegistroComprasLE_Load);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).EndInit();
		base.ResumeLayout(false);
	}
}
