using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class ventasdiarias : Form
{
	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsAdmTransaccion admTrans = new clsAdmTransaccion();

	private clsAdmNotaIngreso AdmIngreso = new clsAdmNotaIngreso();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsAdmFamilia admFam = new clsAdmFamilia();

	private clsAdmLinea admLinea = new clsAdmLinea();

	private clsAdmMarca admmarca = new clsAdmMarca();

	private clsFamilia fam = new clsFamilia();

	private Microsoft.Office.Interop.Excel.Application excel;

	private object obt;

	private Workbook librotrabajo;

	private DataTable detalles = null;

	private DataTable ventas = null;

	private IContainer components = null;

	private RadDropDownList cmbAlmacenes;

	private Button button1;

	private Button btnReporte;

	private Button btnSalir;

	private ComboBox cmbalmacen;

	private ComboBox cmbfamilia;

	private ComboBox cmblinea;

	private Label label1;

	private Label label2;

	private Button btnfiltrar;

	private DataGridView dgvVentas1;

	public DataGridView dg_detalle;

	private DataGridViewTextBoxColumn item;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn fecharegistro;

	private GroupBox groupBox1;

	private Button button2;

	private GroupBox groupBox2;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private DataGridViewTextBoxColumn codFactura;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn linea;

	private DataGridViewTextBoxColumn familia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewTextBoxColumn anulado;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn preciounitario;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precio_compra;

	private DataGridViewTextBoxColumn utilidad_bruta;

	private DataGridViewTextBoxColumn utilidad_neta;

	public ventasdiarias()
	{
		InitializeComponent();
	}

	private void ventasdiarias_Load(object sender, EventArgs e)
	{
		cargaalmacenes();
		CargaFamilia();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void cargaalmacenes()
	{
		cmbalmacen.ValueMember = "cod";
		cmbalmacen.DisplayMember = "nombre";
		cmbalmacen.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void CargaFamilia()
	{
		cmbfamilia.DataSource = admFam.MuestraFamilias();
		cmbfamilia.DisplayMember = "descripcion";
		cmbfamilia.ValueMember = "codFamilia";
		cmbfamilia.SelectedIndex = -1;
	}

	private void Cargalinea()
	{
		if (Convert.ToInt32(cmbfamilia.SelectedValue) != -1)
		{
			cmblinea.DataSource = admLinea.MuestraLineas(Convert.ToInt32(cmbfamilia.SelectedValue));
			cmblinea.DisplayMember = "descripcion";
			cmblinea.ValueMember = "codLinea";
			cmblinea.SelectedIndex = 0;
		}
	}

	public void CargaLista()
	{
		try
		{
			dgvVentas1.DataSource = AdmVenta.VentasCodlineaCodfamilia(Convert.ToInt32(cmbalmacen.SelectedValue), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void btnfiltrar_Click(object sender, EventArgs e)
	{
		int linea = Convert.ToInt32(cmblinea.SelectedValue);
		int familia = Convert.ToInt32(cmbfamilia.SelectedValue);
		dgvVentas1.DataSource = admmarca.listaproductoslineafamilia(linea, frmLogin.iCodAlmacen, dtpDesde.Value, dtpHasta.Value, familia, frmLogin.iCodSucursal);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	public void btnReporte_Click(object sender, EventArgs e)
	{
		ExportarDataGridViewExcel(dgvVentas1);
	}

	public void ExportarDataGridViewExcel(DataGridView dgvVentas1)
	{
		SaveFileDialog fichero = new SaveFileDialog();
		fichero.Filter = "Excel (*.xls)|*.xls";
		fichero.FileName = "Rep.Vantas - Emersson Torres";
		if (fichero.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		Microsoft.Office.Interop.Excel.Application aplicacion = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook libros_trabajo = aplicacion.Workbooks.Add(Type.Missing);
		Worksheet hoja_trabajo = (Worksheet)((dynamic)libros_trabajo.Worksheets)[1];
		for (int i = 1; i < dgvVentas1.Columns.Count + 1; i++)
		{
			hoja_trabajo.Cells[1, i] = dgvVentas1.Columns[i - 1].HeaderText;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Bold = true;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Name = "Calibri";
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Size = 10;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Borders.LineStyle = XlLineStyle.xlContinuous;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
			((dynamic)hoja_trabajo.Cells[1, i + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		}
		for (int j = 0; j < dgvVentas1.Rows.Count; j++)
		{
			for (int k = 0; k < dgvVentas1.Columns.Count; k++)
			{
				hoja_trabajo.Cells[j + 3, k + 1] = dgvVentas1.Rows[j].Cells[k].Value.ToString();
			}
		}
		string rango = "H" + (dgvVentas1.Rows.Count + 2);
		string rango4 = "N" + (dgvVentas1.Rows.Count + 2);
		string rango5 = "O" + (dgvVentas1.Rows.Count + 2);
		string rango6 = "R" + (dgvVentas1.Rows.Count + 2);
		string rango7 = "S" + (dgvVentas1.Rows.Count + 2);
		string rango8 = "T" + (dgvVentas1.Rows.Count + 2);
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "G"]).Formula = "TOTALES:";
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "G"]).Font.Bold = true;
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "G"]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "H"]).Formula = "=SUM(H3:" + rango + ")";
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "H"]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "N"]).Formula = "=SUM(N3:" + rango4 + ")";
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "N"]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "O"]).Formula = "=SUM(O3:" + rango5 + ")";
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "O"]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "R"]).Formula = "=SUM(R3:" + rango6 + ")";
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "R"]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "S"]).Formula = "=SUM(S3:" + rango7 + ")";
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "S"]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "T"]).Formula = "=SUM(T3:" + rango8 + ")";
		((dynamic)hoja_trabajo.Cells[dgvVentas1.Rows.Count + 3, "T"]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
		libros_trabajo.SaveAs(fichero.FileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
		libros_trabajo.Close(true, Type.Missing, Type.Missing);
		aplicacion.Quit();
	}

	public void dgvVentas1_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvVentas1.Rows.Count > 0 && e.RowIndex != -1)
		{
			listardetalleventa(e.RowIndex);
		}
	}

	public void listardetalleventa(int index)
	{
		try
		{
			detalles = null;
			dg_detalle.DataSource = null;
			if (index != -1)
			{
				detalles = AdmVenta.CargaDetalleCodventaxLineaFamilia(new clsFacturaVenta
				{
					CodVenta = int.Parse(dgvVentas1.Rows[index].Cells["codFactura"].Value.ToString()),
					CodAlmacen = frmLogin.iCodAlmacen
				});
				if (detalles != null && detalles.Rows.Count > 0)
				{
					dg_detalle.DataSource = detalles;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (dgvVentas1.RowCount > 0)
		{
			exportar_excel();
		}
	}

	public void exportar_excel()
	{
		int j = 1;
		int i = 1;
		int fila = 1;
		int fila2 = 1;
		int fila3 = 1;
		int h = 1;
		int k = 1;
		int r = 1;
		try
		{
			if (detalles != null)
			{
				if (detalles.Rows.Count <= 0)
				{
					return;
				}
				excel = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
				obt = Type.Missing;
				librotrabajo = excel.Workbooks.Add(obt);
				Worksheet hoja1 = (Worksheet)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00020820-0000-0000-C000-000000000046")));
				hoja1 = (Worksheet)((dynamic)librotrabajo.Sheets)[1];
				hoja1.Name = "T.Directas--Aut.Emersson Torres";
				hoja1.Activate();
				excel.Visible = true;
				for (j = 0; j < dgvVentas1.Columns.Count; j++)
				{
					((dynamic)hoja1.Cells[1, j + 1]).RowHeight = 20;
					hoja1.Cells[1, j + 1] = dgvVentas1.Columns[j].Name.ToUpper();
					((dynamic)hoja1.Cells[1, j + 1]).Font.Bold = true;
					((dynamic)hoja1.Cells[1, j + 1]).Font.Name = "Calibri";
					((dynamic)hoja1.Cells[1, j + 1]).Font.Size = 10;
					((dynamic)hoja1.Cells[1, j + 1]).Borders.LineStyle = XlLineStyle.xlContinuous;
					((dynamic)hoja1.Cells[1, j + 1]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
					((dynamic)hoja1.Cells[1, j + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
				}
				hoja1.Columns.AutoFit();
				fila++;
				for (i = 0; i < dgvVentas1.Rows.Count; i++)
				{
					fila = fila3 + 2;
					listardetalleventa(i);
					for (j = 0; j < dgvVentas1.Columns.Count; j++)
					{
						((dynamic)hoja1.Cells[fila, j + 1]).NumberFormat = "@";
						hoja1.Cells[fila, j + 1] = dgvVentas1.Rows[i].Cells[j].Value.ToString();
						((dynamic)hoja1.Cells[fila, j + 1]).Font.Name = "Calibri";
						((dynamic)hoja1.Cells[fila, j + 1]).Font.Size = 10;
						((dynamic)hoja1.Cells[fila, j + 1]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
						((dynamic)hoja1.Cells[fila, j + 1]).Interior.Color = ColorTranslator.ToOle(Color.LightCyan);
						((dynamic)hoja1.Cells[fila, j + 1]).Borders.LineStyle = XlLineStyle.xlContinuous;
					}
					fila2 = fila + 1;
					for (h = 0; h < dg_detalle.Columns.Count; h++)
					{
						((dynamic)hoja1.Cells[fila2, h + 1]).RowHeight = 20;
						hoja1.Cells[fila2, h + 1] = dg_detalle.Columns[h].Name.ToUpper();
						((dynamic)hoja1.Cells[fila2, h + 1]).Font.Bold = true;
						((dynamic)hoja1.Cells[fila2, h + 1]).Font.Name = "Calibri";
						((dynamic)hoja1.Cells[fila2, h + 1]).Font.Size = 10;
						((dynamic)hoja1.Cells[fila2, h + 1]).Borders.LineStyle = XlLineStyle.xlContinuous;
						((dynamic)hoja1.Cells[fila2, h + 1]).Interior.Color = ColorTranslator.ToOle(Color.LightCoral);
						((dynamic)hoja1.Cells[fila2, h + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
					}
					fila3 = fila2 + 1;
					for (k = 0; k < dg_detalle.Rows.Count; k++)
					{
						for (r = 0; r < dg_detalle.Columns.Count; r++)
						{
							((dynamic)hoja1.Cells[fila3, r + 1]).NumberFormat = "@";
							hoja1.Cells[fila3, r + 1] = dg_detalle.Rows[k].Cells[r].Value.ToString();
							((dynamic)hoja1.Cells[fila3, r + 1]).Font.Name = "Calibri";
							((dynamic)hoja1.Cells[fila3, r + 1]).Font.Size = 10;
							((dynamic)hoja1.Cells[fila3, r + 1]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
						}
						fila3++;
					}
					fila3++;
					if (i < dg_detalle.Rows.Count - 1)
					{
						dg_detalle.CurrentCell = dg_detalle.Rows[i + 1].Cells[0];
					}
				}
			}
			else
			{
				MessageBox.Show("SELECIONE UNA TRANSFERENCIA", "TRANFERENCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cmbfamilia_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void cmbfamilia_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			if (cmbfamilia.SelectedIndex != -1)
			{
				Cargalinea();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.ventasdiarias));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.button1 = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.cmbalmacen = new System.Windows.Forms.ComboBox();
		this.cmbfamilia = new System.Windows.Forms.ComboBox();
		this.cmblinea = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.btnfiltrar = new System.Windows.Forms.Button();
		this.dgvVentas1 = new System.Windows.Forms.DataGridView();
		this.dg_detalle = new System.Windows.Forms.DataGridView();
		this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.codFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.familia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precio_compra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.utilidad_bruta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.utilidad_neta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dg_detalle).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.cmbAlmacenes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmbAlmacenes.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.cmbAlmacenes.Location = new System.Drawing.Point(629, 433);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.RootElement.ControlBounds = new System.Drawing.Rectangle(629, 433, 125, 20);
		this.cmbAlmacenes.RootElement.StretchVertically = true;
		this.cmbAlmacenes.Size = new System.Drawing.Size(125, 24);
		this.cmbAlmacenes.TabIndex = 40;
		this.cmbAlmacenes.ThemeName = "TelerikMetroBlue";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		this.button1.Location = new System.Drawing.Point(311, 670);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(105, 37);
		this.button1.TabIndex = 53;
		this.button1.Text = "Actualizar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnReporte.Image = (System.Drawing.Image)resources.GetObject("btnReporte.Image");
		this.btnReporte.Location = new System.Drawing.Point(800, 664);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(97, 37);
		this.btnReporte.TabIndex = 52;
		this.btnReporte.Text = "Excel";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnSalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnSalir.Location = new System.Drawing.Point(1214, 664);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(88, 37);
		this.btnSalir.TabIndex = 45;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.cmbalmacen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmbalmacen.FormattingEnabled = true;
		this.cmbalmacen.Location = new System.Drawing.Point(474, 673);
		this.cmbalmacen.Name = "cmbalmacen";
		this.cmbalmacen.Size = new System.Drawing.Size(121, 21);
		this.cmbalmacen.TabIndex = 63;
		this.cmbfamilia.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.cmbfamilia.FormattingEnabled = true;
		this.cmbfamilia.Location = new System.Drawing.Point(178, 24);
		this.cmbfamilia.Name = "cmbfamilia";
		this.cmbfamilia.Size = new System.Drawing.Size(251, 21);
		this.cmbfamilia.TabIndex = 64;
		this.cmbfamilia.SelectedIndexChanged += new System.EventHandler(cmbfamilia_SelectedIndexChanged);
		this.cmbfamilia.SelectionChangeCommitted += new System.EventHandler(cmbfamilia_SelectionChangeCommitted);
		this.cmblinea.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.cmblinea.FormattingEnabled = true;
		this.cmblinea.Location = new System.Drawing.Point(546, 19);
		this.cmblinea.Name = "cmblinea";
		this.cmblinea.Size = new System.Drawing.Size(280, 21);
		this.cmblinea.TabIndex = 65;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(489, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(51, 13);
		this.label1.TabIndex = 66;
		this.label1.Text = "LINEA :";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(108, 28);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(63, 13);
		this.label2.TabIndex = 67;
		this.label2.Text = "FAMILIA :";
		this.btnfiltrar.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.btnfiltrar.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.btnfiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnfiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnfiltrar.Image = SIGEFA.Properties.Resources.buscar;
		this.btnfiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnfiltrar.Location = new System.Drawing.Point(861, 24);
		this.btnfiltrar.Name = "btnfiltrar";
		this.btnfiltrar.Size = new System.Drawing.Size(97, 34);
		this.btnfiltrar.TabIndex = 68;
		this.btnfiltrar.Text = "Filtrar";
		this.btnfiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnfiltrar.UseVisualStyleBackColor = false;
		this.btnfiltrar.Click += new System.EventHandler(btnfiltrar_Click);
		this.dgvVentas1.AllowUserToAddRows = false;
		this.dgvVentas1.AllowUserToDeleteRows = false;
		this.dgvVentas1.AllowUserToOrderColumns = true;
		this.dgvVentas1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvVentas1.BackgroundColor = System.Drawing.Color.White;
		this.dgvVentas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvVentas1.Columns.AddRange(this.codFactura, this.fecha, this.documento, this.numdoc, this.linea, this.familia, this.descripcion, this.cantidad, this.codcliente, this.cliente, this.moneda, this.fechapago, this.anulado, this.total, this.valorventa, this.preciounitario, this.igv, this.precio_compra, this.utilidad_bruta, this.utilidad_neta);
		this.dgvVentas1.Location = new System.Drawing.Point(31, 126);
		this.dgvVentas1.Name = "dgvVentas1";
		this.dgvVentas1.ReadOnly = true;
		this.dgvVentas1.Size = new System.Drawing.Size(1322, 458);
		this.dgvVentas1.TabIndex = 69;
		this.dgvVentas1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVentas1_CellClick);
		this.dg_detalle.AllowUserToAddRows = false;
		this.dg_detalle.AllowUserToDeleteRows = false;
		this.dg_detalle.AllowUserToResizeRows = false;
		this.dg_detalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dg_detalle.BackgroundColor = System.Drawing.Color.White;
		this.dg_detalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dg_detalle.Columns.AddRange(this.item, this.coddetalle, this.codproducto, this.referencia, this.dataGridViewTextBoxColumn1, this.codunidad, this.unidad, this.dataGridViewTextBoxColumn2, this.preciounit, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.precioventa, this.nombre, this.fecharegistro);
		this.dg_detalle.Location = new System.Drawing.Point(12, 20);
		this.dg_detalle.Name = "dg_detalle";
		this.dg_detalle.ReadOnly = true;
		this.dg_detalle.RowHeadersVisible = false;
		this.dg_detalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dg_detalle.Size = new System.Drawing.Size(1041, 17);
		this.dg_detalle.TabIndex = 70;
		this.dg_detalle.Visible = false;
		this.item.DataPropertyName = "item";
		this.item.HeaderText = "item";
		this.item.Name = "item";
		this.item.ReadOnly = true;
		this.item.Visible = false;
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 87;
		this.dataGridViewTextBoxColumn1.DataPropertyName = "producto";
		this.dataGridViewTextBoxColumn1.HeaderText = "Descripcion";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 400;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 75;
		this.dataGridViewTextBoxColumn2.DataPropertyName = "cantidad";
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle26.Format = "N2";
		dataGridViewCellStyle26.NullValue = null;
		this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle26;
		this.dataGridViewTextBoxColumn2.HeaderText = "Cantidad";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.Width = 70;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle27.Format = "N2";
		dataGridViewCellStyle27.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle27;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 75;
		this.dataGridViewTextBoxColumn3.DataPropertyName = "subtotal";
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle28.Format = "N2";
		dataGridViewCellStyle28.NullValue = null;
		this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle28;
		this.dataGridViewTextBoxColumn3.HeaderText = "Importe";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.ReadOnly = true;
		this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn3.Width = 85;
		this.dataGridViewTextBoxColumn4.DataPropertyName = "igv";
		dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle29.Format = "N2";
		dataGridViewCellStyle29.NullValue = null;
		this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle29;
		this.dataGridViewTextBoxColumn4.HeaderText = "IGV";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.ReadOnly = true;
		this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn4.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle30.Format = "N2";
		dataGridViewCellStyle30.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle30;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Width = 85;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Usuario";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dg_detalle);
		this.groupBox1.Location = new System.Drawing.Point(119, 590);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1063, 50);
		this.groupBox1.TabIndex = 71;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle";
		this.groupBox1.Visible = false;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.button2.Image = (System.Drawing.Image)resources.GetObject("button2.Image");
		this.button2.Location = new System.Drawing.Point(638, 664);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(116, 37);
		this.button2.TabIndex = 72;
		this.button2.Text = "Excel Detallado";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.btnfiltrar);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.dtpDesde);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.dtpHasta);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.cmblinea);
		this.groupBox2.Controls.Add(this.cmbfamilia);
		this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.groupBox2.Location = new System.Drawing.Point(174, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(977, 106);
		this.groupBox2.TabIndex = 73;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Filtros de búsqueda";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(499, 68);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 77;
		this.label6.Text = "Hasta :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(326, 68);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 76;
		this.label5.Text = "Desde :";
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(379, 65);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 75;
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(546, 65);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 74;
		this.codFactura.DataPropertyName = "codFactura";
		this.codFactura.HeaderText = "codFactura";
		this.codFactura.Name = "codFactura";
		this.codFactura.ReadOnly = true;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha Registro";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.documento.DataPropertyName = "documento";
		this.documento.HeaderText = "Documento";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Visible = false;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "T.Documento";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.linea.DataPropertyName = "linea";
		this.linea.HeaderText = "Linea";
		this.linea.Name = "linea";
		this.linea.ReadOnly = true;
		this.familia.DataPropertyName = "familia";
		this.familia.HeaderText = "Familia";
		this.familia.Name = "familia";
		this.familia.ReadOnly = true;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Producto";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.cantidad.DataPropertyName = "cantidad";
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.codcliente.DataPropertyName = "codcliente";
		this.codcliente.HeaderText = "Num.Cliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.ReadOnly = true;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "F.Pago";
		this.fechapago.Name = "fechapago";
		this.fechapago.ReadOnly = true;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.total.DataPropertyName = "total";
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.valorventa.DataPropertyName = "valorventa";
		this.valorventa.HeaderText = "V.Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.preciounitario.DataPropertyName = "preciounitario";
		this.preciounitario.HeaderText = "P.Unitario";
		this.preciounitario.Name = "preciounitario";
		this.preciounitario.ReadOnly = true;
		this.igv.DataPropertyName = "igv";
		this.igv.HeaderText = "Igv";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.precio_compra.DataPropertyName = "precio_compra";
		this.precio_compra.HeaderText = "Precio Compra";
		this.precio_compra.Name = "precio_compra";
		this.precio_compra.ReadOnly = true;
		this.utilidad_bruta.DataPropertyName = "utilidad_bruta";
		this.utilidad_bruta.HeaderText = "Utilidad Bruta";
		this.utilidad_bruta.Name = "utilidad_bruta";
		this.utilidad_bruta.ReadOnly = true;
		this.utilidad_neta.DataPropertyName = "utilidad_neta";
		this.utilidad_neta.HeaderText = "Utilidad Neta";
		this.utilidad_neta.Name = "utilidad_neta";
		this.utilidad_neta.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.ClientSize = new System.Drawing.Size(1386, 733);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.dgvVentas1);
		base.Controls.Add(this.cmbalmacen);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.btnSalir);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "ventasdiarias";
		this.Text = "ventasdiarias";
		base.Load += new System.EventHandler(ventasdiarias_Load);
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dg_detalle).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
