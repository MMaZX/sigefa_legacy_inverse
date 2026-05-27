using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frminventario : Form
{
	private clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();

	private clsNotaIngreso notaI = new clsNotaIngreso();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private DataTable dt1 = new DataTable();

	private Microsoft.Office.Interop.Excel.Application excel;

	private object obt;

	private Workbook librotrabajo;

	private IContainer components = null;

	private Button btnAnular;

	private Button btnEliminar;

	private Button btnSalir;

	private Button btnIrNota;

	private GroupBox groupBox2;

	private DataGridView dgvDocumentos;

	private GroupBox groupBox1;

	private Label txtNombreProducto;

	private Label label7;

	private Button btnBuscarProducto;

	private Label label8;

	private TextBox txtCodprod;

	private Button btnReporte;

	private Button btnConsultar;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private ComboBox cmbTipoDocumento;

	private DataGridViewTextBoxColumn numero;

	private DataGridViewTextBoxColumn transaccion;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewTextBoxColumn numerodoc;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn bruto;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewTextBoxColumn anulado;

	public frminventario()
	{
		InitializeComponent();
	}

	private void frminventario_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		try
		{
			if (data.DataSource != null)
			{
				DataTable dt = (DataTable)data.DataSource;
				dt.Clear();
			}
			dgvDocumentos.DataSource = data;
			data.DataSource = AdmNotaI.reporteinventario(frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvDocumentos.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
	}

	private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count > 0 && e.RowIndex != -1)
		{
			notaI.CodNotaIngreso = dgvDocumentos.Rows[e.RowIndex].Cells[numero.Name].Value.ToString();
		}
	}

	private void btnIrNota_Click(object sender, EventArgs e)
	{
		if (dgvDocumentos.Rows.Count >= 1)
		{
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodNota = notaI.CodNotaIngreso;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		buscarpornumero();
	}

	public void buscarpornumero()
	{
		try
		{
			if (data.DataSource != null)
			{
				DataTable dt = (DataTable)data.DataSource;
				dt.Clear();
			}
			dgvDocumentos.DataSource = data;
			data.DataSource = AdmNotaI.busquedapornumero(frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date, txtCodprod.Text);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvDocumentos.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void txtCodprod_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtFiltro.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltro.Text != "")
				{
					string filterCod = txtFiltro.Text;
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					if (cad.Count() > 1)
					{
						string[] array = cad;
						foreach (string c in array)
						{
							if (cont == 1)
							{
								queries.Add($"[{label3.Text}] LIKE '%{c}%'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"[{label3.Text}] LIKE '%{c}%'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"[{label3.Text}] LIKE '%{filterCod}%'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
		}
	}

	private void dgvDocumentos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvDocumentos.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvDocumentos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtFiltro.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltro.Text != "")
				{
					string filterCod = txtFiltro.Text;
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					if (cad.Count() > 1)
					{
						string[] array = cad;
						foreach (string c in array)
						{
							if (cont == 1)
							{
								queries.Add($"[{label3.Text}] LIKE '%{c}%'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"[{label3.Text}] LIKE '%{c}%'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"[{label3.Text}] LIKE '%{filterCod}%'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
		}
	}

	public void ExportarDataGridViewExcel(DataGridView dgvDocumentos)
	{
		SaveFileDialog fichero = new SaveFileDialog();
		fichero.Filter = "Excel (*.xls)|*.xls";
		fichero.FileName = "R.Inventario - Emersson Torres";
		if (fichero.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		Microsoft.Office.Interop.Excel.Application aplicacion = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook libros_trabajo = aplicacion.Workbooks.Add(Type.Missing);
		Worksheet hoja_trabajo = (Worksheet)((dynamic)libros_trabajo.Worksheets)[1];
		for (int i = 1; i < dgvDocumentos.Columns.Count + 1; i++)
		{
			hoja_trabajo.Cells[1, i] = dgvDocumentos.Columns[i - 1].HeaderText;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Bold = true;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Name = "Calibri";
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Font.Size = 10;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Borders.LineStyle = XlLineStyle.xlContinuous;
			((dynamic)hoja_trabajo.Cells[1, i + 1]).Interior.Color = ColorTranslator.ToOle(Color.Yellow);
			((dynamic)hoja_trabajo.Cells[1, i + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		}
		for (int j = 0; j < dgvDocumentos.Rows.Count; j++)
		{
			for (int k = 0; k < dgvDocumentos.Columns.Count; k++)
			{
				hoja_trabajo.Cells[j + 3, k + 1] = dgvDocumentos.Rows[j].Cells[k].Value.ToString();
			}
		}
		libros_trabajo.SaveAs(fichero.FileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
		libros_trabajo.Close(true, Type.Missing, Type.Missing);
		aplicacion.Quit();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		ExportarDataGridViewExcel(dgvDocumentos);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frminventario));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnIrNota = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDocumentos = new System.Windows.Forms.DataGridView();
		this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.transaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numerodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.bruto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.btnConsultar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.cmbTipoDocumento = new System.Windows.Forms.ComboBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.Enabled = false;
		this.btnAnular.ImageIndex = 10;
		this.btnAnular.Location = new System.Drawing.Point(285, 583);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(98, 32);
		this.btnAnular.TabIndex = 20;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 9;
		this.btnEliminar.Location = new System.Drawing.Point(408, 583);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(84, 32);
		this.btnEliminar.TabIndex = 19;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Visible = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.BackColor = System.Drawing.Color.White;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnSalir.Location = new System.Drawing.Point(913, 552);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(92, 45);
		this.btnSalir.TabIndex = 17;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = false;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnIrNota.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrNota.BackColor = System.Drawing.Color.White;
		this.btnIrNota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnIrNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnIrNota.Image = (System.Drawing.Image)resources.GetObject("btnIrNota.Image");
		this.btnIrNota.Location = new System.Drawing.Point(772, 552);
		this.btnIrNota.Name = "btnIrNota";
		this.btnIrNota.Size = new System.Drawing.Size(101, 45);
		this.btnIrNota.TabIndex = 18;
		this.btnIrNota.Text = "Ver";
		this.btnIrNota.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrNota.UseVisualStyleBackColor = false;
		this.btnIrNota.Click += new System.EventHandler(btnIrNota_Click);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvDocumentos);
		this.groupBox2.Location = new System.Drawing.Point(64, 133);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(941, 375);
		this.groupBox2.TabIndex = 16;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Documentos";
		this.dgvDocumentos.AllowUserToAddRows = false;
		this.dgvDocumentos.AllowUserToDeleteRows = false;
		this.dgvDocumentos.AllowUserToResizeColumns = false;
		this.dgvDocumentos.AllowUserToResizeRows = false;
		this.dgvDocumentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDocumentos.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvDocumentos.Columns.AddRange(this.numero, this.transaccion, this.fecha, this.fecharegistro, this.docref, this.numerodoc, this.moneda, this.bruto, this.montodscto, this.igv, this.total, this.usuario, this.fechapago, this.anulado);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDocumentos.DefaultCellStyle = dataGridViewCellStyle7;
		this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDocumentos.Location = new System.Drawing.Point(3, 16);
		this.dgvDocumentos.Name = "dgvDocumentos";
		this.dgvDocumentos.ReadOnly = true;
		this.dgvDocumentos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentos.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.dgvDocumentos.RowHeadersVisible = false;
		this.dgvDocumentos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDocumentos.Size = new System.Drawing.Size(935, 356);
		this.dgvDocumentos.TabIndex = 0;
		this.dgvDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellClick);
		this.dgvDocumentos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDocumentos_ColumnHeaderMouseClick);
		this.numero.DataPropertyName = "codNota";
		dataGridViewCellStyle9.NullValue = null;
		this.numero.DefaultCellStyle = dataGridViewCellStyle9;
		this.numero.HeaderText = "Numero";
		this.numero.Name = "numero";
		this.numero.ReadOnly = true;
		this.transaccion.DataPropertyName = "movimiento";
		this.transaccion.HeaderText = "Trans.";
		this.transaccion.Name = "transaccion";
		this.transaccion.ReadOnly = true;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha NI/NS";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Registro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.docref.DataPropertyName = "documento";
		this.docref.HeaderText = "Doc. Ref.";
		this.docref.Name = "docref";
		this.docref.ReadOnly = true;
		this.numerodoc.DataPropertyName = "numdocumento";
		this.numerodoc.HeaderText = "Num. Doc. Ref.";
		this.numerodoc.Name = "numerodoc";
		this.numerodoc.ReadOnly = true;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.bruto.DataPropertyName = "bruto";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N2";
		this.bruto.DefaultCellStyle = dataGridViewCellStyle10;
		this.bruto.HeaderText = "Bruto";
		this.bruto.Name = "bruto";
		this.bruto.ReadOnly = true;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "N2";
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle11;
		this.montodscto.HeaderText = "Monto Dscto.";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle12.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle12;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.total.DataPropertyName = "total";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle13.Format = "N2";
		this.total.DefaultCellStyle = dataGridViewCellStyle13;
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Usuario";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "Fecha Pago";
		this.fechapago.Name = "fechapago";
		this.fechapago.ReadOnly = true;
		this.fechapago.Visible = false;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.groupBox1.Controls.Add(this.txtNombreProducto);
		this.groupBox1.Controls.Add(this.btnConsultar);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.btnBuscarProducto);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtCodprod);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.cmbTipoDocumento);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1063, 127);
		this.groupBox1.TabIndex = 21;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Buscar";
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(1029, 71);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 43;
		this.txtNombreProducto.Text = "---";
		this.txtNombreProducto.Visible = false;
		this.btnConsultar.BackColor = System.Drawing.Color.White;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.Color.Black;
		this.btnConsultar.Image = SIGEFA.Properties.Resources.buscar;
		this.btnConsultar.Location = new System.Drawing.Point(467, 15);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(105, 32);
		this.btnConsultar.TabIndex = 14;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(273, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Hasta :";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(988, 71);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(35, 13);
		this.label7.TabIndex = 42;
		this.label7.Text = "Prod.:";
		this.label7.Visible = false;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(100, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 12;
		this.label5.Text = "Desde :";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(810, 22);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(34, 13);
		this.label4.TabIndex = 11;
		this.label4.Text = "Tipo :";
		this.label4.Visible = false;
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.BackColor = System.Drawing.Color.White;
		this.btnBuscarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(948, 61);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 41;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = false;
		this.btnBuscarProducto.Visible = false;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(408, 82);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(171, 82);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 9;
		this.label2.Text = "X";
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(801, 55);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(112, 13);
		this.label8.TabIndex = 40;
		this.label8.Text = "Busqueda x Producto:";
		this.label8.Visible = false;
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(103, 101);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(317, 20);
		this.txtFiltro.TabIndex = 8;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(100, 84);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(56, 13);
		this.label1.TabIndex = 7;
		this.label1.Text = "Filtrar por :";
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(804, 71);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 39;
		this.txtCodprod.Visible = false;
		this.txtCodprod.KeyUp += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyUp);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(153, 19);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 4;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(320, 19);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 3;
		this.cmbTipoDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipoDocumento.FormattingEnabled = true;
		this.cmbTipoDocumento.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbTipoDocumento.Location = new System.Drawing.Point(850, 19);
		this.cmbTipoDocumento.Name = "cmbTipoDocumento";
		this.cmbTipoDocumento.Size = new System.Drawing.Size(171, 21);
		this.cmbTipoDocumento.TabIndex = 0;
		this.cmbTipoDocumento.Visible = false;
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.BackColor = System.Drawing.Color.White;
		this.btnReporte.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReporte.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.btnReporte.Image = (System.Drawing.Image)resources.GetObject("btnReporte.Image");
		this.btnReporte.Location = new System.Drawing.Point(611, 552);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(120, 45);
		this.btnReporte.TabIndex = 16;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = false;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		base.ClientSize = new System.Drawing.Size(1063, 625);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btnEliminar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnIrNota);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox2);
		base.Name = "frminventario";
		this.Text = "Reporte de Inventario Segun Nota de Ingreso";
		base.Load += new System.EventHandler(frminventario_Load);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
