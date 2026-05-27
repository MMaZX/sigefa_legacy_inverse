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

namespace SIGEFA.Formularios;

public class frmlistatranferencias : Form
{
	private clsAdmTransferencia admtrans = new clsAdmTransferencia();

	private clsTransferencia trans = new clsTransferencia();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private DataTable detalles = null;

	private DataTable ventas = null;

	private string filtro = string.Empty;

	private Microsoft.Office.Interop.Excel.Application excel;

	private object obt;

	private Workbook librotrabajo;

	private IContainer components = null;

	private Button button1;

	private Button button2;

	public DataGridView dgvTransferenciasPendientes;

	private Button button3;

	public DataGridView dg_detalle;

	private GroupBox groupBox1;

	private DataGridViewTextBoxColumn item;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn almaorigen;

	private DataGridViewTextBoxColumn almadestino;

	private DataGridViewTextBoxColumn codtranfe;

	private DataGridViewTextBoxColumn TDirecta;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn documentos;

	private DataGridViewTextBoxColumn origen;

	private DataGridViewTextBoxColumn destino;

	private DataGridViewTextBoxColumn rechazo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn fechaenvio;

	private DataGridViewTextBoxColumn fechaentrega;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn preciounitario;

	private DataGridViewTextBoxColumn importe;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Label label5;

	private Label label6;

	private Label label1;

	private ComboBox cbTipo;

	private Button btnBusqueda;

	public DataGridView grd { get; private set; }

	public frmlistatranferencias()
	{
		InitializeComponent();
	}

	private void frmlistatranferencias_Load(object sender, EventArgs e)
	{
		cbTipo.SelectedIndex = 0;
		CargaLista();
	}

	public void CargaLista()
	{
		dgvTransferenciasPendientes.AutoGenerateColumns = false;
		dgvTransferenciasPendientes.DataSource = data;
		data.DataSource = admtrans.listatodastranferencias(Convert.ToInt32(cbTipo.SelectedIndex), frmLogin.iCodUser, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		if (cbTipo.SelectedIndex == 0)
		{
			dgvTransferenciasPendientes.Columns["rechazo"].Visible = false;
			for (int i = 0; i < dgvTransferenciasPendientes.RowCount; i++)
			{
				dgvTransferenciasPendientes["TDirecta", i].Style.BackColor = Color.FromArgb(255, 255, 183);
			}
		}
		else if (cbTipo.SelectedIndex == 1)
		{
			dgvTransferenciasPendientes.Columns["rechazo"].Visible = false;
			for (int j = 0; j < dgvTransferenciasPendientes.RowCount; j++)
			{
				dgvTransferenciasPendientes["TDirecta", j].Style.BackColor = Color.FromArgb(228, 255, 187);
			}
		}
		else if (cbTipo.SelectedIndex == 2)
		{
			dgvTransferenciasPendientes.Columns["rechazo"].Visible = true;
			for (int k = 0; k < dgvTransferenciasPendientes.RowCount; k++)
			{
				dgvTransferenciasPendientes["TDirecta", k].Style.BackColor = Color.FromArgb(255, 139, 139);
			}
		}
		else if (cbTipo.SelectedIndex == 3)
		{
			dgvTransferenciasPendientes.Columns["rechazo"].Visible = true;
			for (int l = 0; l < dgvTransferenciasPendientes.RowCount; l++)
			{
				dgvTransferenciasPendientes["TDirecta", l].Style.BackColor = Color.FromArgb(195, 240, 246);
			}
		}
		dgvTransferenciasPendientes.ClearSelection();
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void cbTipo_SelectedValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		ExportarDataGridViewExcel(dgvTransferenciasPendientes);
	}

	public void ExportarDataGridViewExcel(DataGridView dgvTransferenciasPendientes)
	{
		SaveFileDialog fichero = new SaveFileDialog();
		fichero.Filter = "Excel (*.xls)|*.xls";
		fichero.FileName = "T.Directas - Emersson Torres";
		if (fichero.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		Microsoft.Office.Interop.Excel.Application aplicacion = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook libros_trabajo = aplicacion.Workbooks.Add(Type.Missing);
		Worksheet hoja_trabajo = (Worksheet)((dynamic)libros_trabajo.Worksheets)[1];
		for (int i = 1; i < dgvTransferenciasPendientes.Columns.Count + 1; i++)
		{
			hoja_trabajo.Cells[1, i] = dgvTransferenciasPendientes.Columns[i - 1].HeaderText;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Bold = true;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Name = "Calibri";
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Size = 10;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Borders.LineStyle = XlLineStyle.xlContinuous;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
			((dynamic)hoja_trabajo.Cells[1, i + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		}
		for (int j = 0; j < dgvTransferenciasPendientes.Rows.Count; j++)
		{
			for (int k = 0; k < dgvTransferenciasPendientes.Columns.Count; k++)
			{
				hoja_trabajo.Cells[j + 3, k + 1] = dgvTransferenciasPendientes.Rows[j].Cells[k].Value.ToString();
			}
		}
		libros_trabajo.SaveAs(fichero.FileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
		libros_trabajo.Close(true, Type.Missing, Type.Missing);
		aplicacion.Quit();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		exportar_excel();
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
				for (j = 0; j < dgvTransferenciasPendientes.Columns.Count; j++)
				{
					((dynamic)hoja1.Cells[1, j + 1]).RowHeight = 20;
					hoja1.Cells[1, j + 1] = dgvTransferenciasPendientes.Columns[j].Name.ToUpper();
					((dynamic)hoja1.Cells[1, j + 1]).Font.Bold = true;
					((dynamic)hoja1.Cells[1, j + 1]).Font.Name = "Calibri";
					((dynamic)hoja1.Cells[1, j + 1]).Font.Size = 10;
					((dynamic)hoja1.Cells[1, j + 1]).Borders.LineStyle = XlLineStyle.xlContinuous;
					((dynamic)hoja1.Cells[1, j + 1]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
					((dynamic)hoja1.Cells[1, j + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
				}
				hoja1.Columns.AutoFit();
				fila++;
				for (i = 0; i < dgvTransferenciasPendientes.Rows.Count; i++)
				{
					fila = fila3 + 2;
					listardetalleventa(i);
					for (j = 0; j < dgvTransferenciasPendientes.Columns.Count; j++)
					{
						((dynamic)hoja1.Cells[fila, j + 1]).NumberFormat = "@";
						hoja1.Cells[fila, j + 1] = dgvTransferenciasPendientes.Rows[i].Cells[j].Value.ToString();
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

	public void listardetalleventa(int index)
	{
		try
		{
			detalles = null;
			dg_detalle.DataSource = null;
			if (index != -1)
			{
				detalles = admtrans.CargaDetalleTrans(new clsTransferencia
				{
					codtrans = int.Parse(dgvTransferenciasPendientes.Rows[index].Cells["codtranfe"].Value.ToString()),
					CodAlmacenOrigen = frmLogin.iCodAlmacen
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

	private void dgvTransferenciasPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvTransferenciasPendientes.Rows.Count > 0 && e.RowIndex != -1)
		{
			listardetalleventa(e.RowIndex);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmlistatranferencias));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dgvTransferenciasPendientes = new System.Windows.Forms.DataGridView();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.dg_detalle = new System.Windows.Forms.DataGridView();
		this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almaorigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almadestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cbTipo = new System.Windows.Forms.ComboBox();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.codtranfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TDirecta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documentos = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.destino = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.rechazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaenvio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaentrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvTransferenciasPendientes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dg_detalle).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.dgvTransferenciasPendientes.AllowUserToAddRows = false;
		this.dgvTransferenciasPendientes.AllowUserToDeleteRows = false;
		this.dgvTransferenciasPendientes.AllowUserToOrderColumns = true;
		this.dgvTransferenciasPendientes.AllowUserToResizeRows = false;
		this.dgvTransferenciasPendientes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvTransferenciasPendientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvTransferenciasPendientes.BackgroundColor = System.Drawing.Color.WhiteSmoke;
		this.dgvTransferenciasPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvTransferenciasPendientes.Columns.AddRange(this.codtranfe, this.TDirecta, this.usuario, this.documentos, this.origen, this.destino, this.rechazo, this.descripcion, this.fechaenvio, this.fechaentrega, this.fecharegistro, this.referencia, this.unidad, this.cantidad, this.preciounitario, this.importe);
		this.dgvTransferenciasPendientes.Location = new System.Drawing.Point(85, 12);
		this.dgvTransferenciasPendientes.MultiSelect = false;
		this.dgvTransferenciasPendientes.Name = "dgvTransferenciasPendientes";
		this.dgvTransferenciasPendientes.ReadOnly = true;
		this.dgvTransferenciasPendientes.RowHeadersVisible = false;
		this.dgvTransferenciasPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTransferenciasPendientes.Size = new System.Drawing.Size(989, 185);
		this.dgvTransferenciasPendientes.TabIndex = 1;
		this.dgvTransferenciasPendientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTransferenciasPendientes_CellClick);
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.button1.BackColor = System.Drawing.Color.SeaShell;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		this.button1.Location = new System.Drawing.Point(906, 618);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(103, 38);
		this.button1.TabIndex = 34;
		this.button1.Text = "Reporte";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.button2.BackColor = System.Drawing.Color.SeaShell;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button2.Image = SIGEFA.Properties.Resources.x_button;
		this.button2.Location = new System.Drawing.Point(1061, 618);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(80, 33);
		this.button2.TabIndex = 35;
		this.button2.Text = "Salir";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.button3.BackColor = System.Drawing.Color.SeaShell;
		this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button3.Image = (System.Drawing.Image)resources.GetObject("button3.Image");
		this.button3.Location = new System.Drawing.Point(671, 618);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(153, 38);
		this.button3.TabIndex = 36;
		this.button3.Text = "Reporte Detallado";
		this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button3.UseVisualStyleBackColor = false;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.dg_detalle.AllowUserToAddRows = false;
		this.dg_detalle.AllowUserToDeleteRows = false;
		this.dg_detalle.AllowUserToResizeRows = false;
		this.dg_detalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dg_detalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dg_detalle.BackgroundColor = System.Drawing.Color.WhiteSmoke;
		this.dg_detalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dg_detalle.Columns.AddRange(this.item, this.coddetalle, this.codproducto, this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.codunidad, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.preciounit, this.dataGridViewTextBoxColumn5, this.igv, this.precioventa, this.dataGridViewTextBoxColumn7, this.almaorigen, this.almadestino);
		this.dg_detalle.Location = new System.Drawing.Point(29, 19);
		this.dg_detalle.Name = "dg_detalle";
		this.dg_detalle.ReadOnly = true;
		this.dg_detalle.RowHeadersVisible = false;
		this.dg_detalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dg_detalle.Size = new System.Drawing.Size(814, 180);
		this.dg_detalle.TabIndex = 37;
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
		this.dataGridViewTextBoxColumn1.DataPropertyName = "referencia";
		this.dataGridViewTextBoxColumn1.HeaderText = "Codigo";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.DataPropertyName = "producto";
		this.dataGridViewTextBoxColumn2.HeaderText = "Descripcion";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.dataGridViewTextBoxColumn3.DataPropertyName = "unidad";
		this.dataGridViewTextBoxColumn3.HeaderText = "Unidad";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.ReadOnly = true;
		this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn4.DataPropertyName = "cantidad";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		dataGridViewCellStyle14.NullValue = null;
		this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle14;
		this.dataGridViewTextBoxColumn4.HeaderText = "Cantidad";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.ReadOnly = true;
		this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N2";
		dataGridViewCellStyle15.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle15;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn5.DataPropertyName = "subtotal";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle16;
		this.dataGridViewTextBoxColumn5.HeaderText = "Importe";
		this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
		this.dataGridViewTextBoxColumn5.ReadOnly = true;
		this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		dataGridViewCellStyle17.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle17;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		dataGridViewCellStyle18.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle18;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn7.DataPropertyName = "fecharegistro";
		this.dataGridViewTextBoxColumn7.HeaderText = "Fecha Reg";
		this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
		this.dataGridViewTextBoxColumn7.ReadOnly = true;
		this.dataGridViewTextBoxColumn7.Visible = false;
		this.almaorigen.DataPropertyName = "almaorigen";
		this.almaorigen.HeaderText = "Almacen Origen";
		this.almaorigen.Name = "almaorigen";
		this.almaorigen.ReadOnly = true;
		this.almadestino.DataPropertyName = "almadestino";
		this.almadestino.HeaderText = "Almacen Destino";
		this.almadestino.Name = "almadestino";
		this.almadestino.ReadOnly = true;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dg_detalle);
		this.groupBox1.Location = new System.Drawing.Point(140, 375);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(869, 215);
		this.groupBox1.TabIndex = 38;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle";
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(418, 275);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 21;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged);
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(251, 275);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 22;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(184, 276);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(62, 16);
		this.label5.TabIndex = 23;
		this.label5.Text = "Desde :";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(358, 276);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(57, 16);
		this.label6.TabIndex = 24;
		this.label6.Text = "Hasta :";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(551, 276);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(48, 16);
		this.label1.TabIndex = 25;
		this.label1.Text = "Tipo :";
		this.cbTipo.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.cbTipo.FormattingEnabled = true;
		this.cbTipo.Items.AddRange(new object[4] { "Pendientes", "Aprobadas", "Rechazadas", "Eniviadas" });
		this.cbTipo.Location = new System.Drawing.Point(611, 274);
		this.cbTipo.Name = "cbTipo";
		this.cbTipo.Size = new System.Drawing.Size(121, 21);
		this.cbTipo.TabIndex = 26;
		this.cbTipo.SelectedValueChanged += new System.EventHandler(cbTipo_SelectedValueChanged);
		this.btnBusqueda.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.btnBusqueda.BackColor = System.Drawing.Color.SeaShell;
		this.btnBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBusqueda.Location = new System.Drawing.Point(818, 267);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(80, 33);
		this.btnBusqueda.TabIndex = 33;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = false;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.codtranfe.DataPropertyName = "codtranfe";
		this.codtranfe.HeaderText = "Codigo";
		this.codtranfe.Name = "codtranfe";
		this.codtranfe.ReadOnly = true;
		this.codtranfe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codtranfe.Visible = false;
		this.TDirecta.DataPropertyName = "TDirecta";
		this.TDirecta.HeaderText = "TDirecta";
		this.TDirecta.Name = "TDirecta";
		this.TDirecta.ReadOnly = true;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Usuario";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.documentos.DataPropertyName = "documentos";
		this.documentos.HeaderText = "Nro. Documento";
		this.documentos.Name = "documentos";
		this.documentos.ReadOnly = true;
		this.origen.DataPropertyName = "origen";
		this.origen.HeaderText = "Almacen Origen";
		this.origen.Name = "origen";
		this.origen.ReadOnly = true;
		this.destino.DataPropertyName = "destino";
		this.destino.HeaderText = "Almacen Destino";
		this.destino.Name = "destino";
		this.destino.ReadOnly = true;
		this.rechazo.DataPropertyName = "rechazo";
		this.rechazo.HeaderText = "DecripcionRechazo";
		this.rechazo.Name = "rechazo";
		this.rechazo.ReadOnly = true;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.fechaenvio.DataPropertyName = "fechaenvio";
		this.fechaenvio.HeaderText = "Fecha Transf.";
		this.fechaenvio.Name = "fechaenvio";
		this.fechaenvio.ReadOnly = true;
		this.fechaentrega.DataPropertyName = "fechaentrega";
		this.fechaentrega.HeaderText = "Fecha Acepta/Rechaza";
		this.fechaentrega.Name = "fechaentrega";
		this.fechaentrega.ReadOnly = true;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Registro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.referencia.DataPropertyName = "referencia";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.referencia.DefaultCellStyle = dataGridViewCellStyle19;
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "U.Medida";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.cantidad.DataPropertyName = "cantidad";
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.preciounitario.DataPropertyName = "preciounitario";
		this.preciounitario.HeaderText = "Precio unitario";
		this.preciounitario.Name = "preciounitario";
		this.preciounitario.ReadOnly = true;
		this.importe.DataPropertyName = "importe";
		this.importe.HeaderText = "Total";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.LightSteelBlue;
		base.ClientSize = new System.Drawing.Size(1181, 663);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnBusqueda);
		base.Controls.Add(this.cbTipo);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.dgvTransferenciasPendientes);
		base.Name = "frmlistatranferencias";
		this.Text = "frmlistatranferencias";
		base.Load += new System.EventHandler(frmlistatranferencias_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvTransferenciasPendientes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dg_detalle).EndInit();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
