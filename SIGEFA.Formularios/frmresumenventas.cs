using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using Microsoft.Office.Interop.Excel;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmresumenventas : Office2007Form
{
	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsAdmTransferencia admTransferencia = new clsAdmTransferencia();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmTipoDocumento tipo = new clsAdmTipoDocumento();

	private DataTable detalles = null;

	private DataTable ventas = null;

	private Microsoft.Office.Interop.Excel.Application excel;

	private object obt;

	private Workbook librotrabajo;

	private IContainer components = null;

	private GroupBox groupBox2;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private ComboBox cb_tipocomprobante;

	private Label label4;

	private ComboBox cmbalmacen;

	private Label label1;

	private Button button1;

	public DataGridView dg_detalle;

	private RadGridView dgvVentas1;

	private GroupBox groupBox3;

	private DataGridView dgvVentas;

	private Button btn_exportar;

	private Button button2;

	private DataGridViewTextBoxColumn item;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewAutoFilterTextBoxColumn fecha;

	private DataGridViewAutoFilterTextBoxColumn documento;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewAutoFilterTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn icbper;

	private DataGridViewAutoFilterTextBoxColumn formapago;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewAutoFilterTextBoxColumn anulado;

	private DataGridViewAutoFilterTextBoxColumn impreso;

	private DataGridViewTextBoxColumn NotaDeCredito;

	private DataGridViewTextBoxColumn enviadoS;

	public frmresumenventas()
	{
		InitializeComponent();
	}

	private void frmresumenventas_Load(object sender, EventArgs e)
	{
		listaventas();
		cargaalmacenes();
		listatipocomprobante();
	}

	public void listaventas()
	{
		try
		{
			dgvVentas1.DataSource = AdmVenta.Ventas(Convert.ToInt32(cmbalmacen.SelectedValue), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal, 0);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void cargaalmacenes()
	{
		cmbalmacen.ValueMember = "cod";
		cmbalmacen.DisplayMember = "nombre";
		cmbalmacen.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	public void listatipocomprobante()
	{
		cb_tipocomprobante.ValueMember = "codTipoDocumento";
		cb_tipocomprobante.DisplayMember = "descripcion";
		cb_tipocomprobante.DataSource = tipo.CargaTipoDocumentoBolFacNotCredito();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		listaventas();
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		listaventas();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		listaventas();
	}

	private void cb_tipocomprobante_SelectedValueChanged(object sender, EventArgs e)
	{
		if (cb_tipocomprobante.SelectedIndex == 0)
		{
			ventasboletasFacturasNc();
		}
		if (cb_tipocomprobante.SelectedIndex == 1)
		{
			ventasboletasFacturasNc();
		}
		if (cb_tipocomprobante.SelectedIndex == 2)
		{
			ventasboletasFacturasNc();
		}
	}

	public void ventasboletasFacturasNc()
	{
		try
		{
			dgvVentas1.DataSource = AdmVenta.Ventasboletas(Convert.ToInt32(cmbalmacen.SelectedValue), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal, Convert.ToInt32(cb_tipocomprobante.SelectedValue));
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	public void listardetalleventa(int index)
	{
		try
		{
			detalles = null;
			dg_detalle.DataSource = null;
			if (index != -1)
			{
				detalles = AdmVenta.CargaDetalleCodventa(new clsFacturaVenta
				{
					CodVenta = int.Parse(dgvVentas1.Rows[index].Cells["codigo"].Value.ToString()),
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

	private void dgvVentas1_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (dgvVentas1.Rows.Count > 0 && e.RowIndex != -1)
		{
			listardetalleventa(e.RowIndex);
		}
	}

	private void dgvVentas1_Click(object sender, EventArgs e)
	{
	}

	private void btn_exportar_Click(object sender, EventArgs e)
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
				hoja1.Name = "Rep.Ventas--Aut.Emersson Torres";
				hoja1.Activate();
				excel.Visible = true;
				for (j = 0; j < dgvVentas.Columns.Count; j++)
				{
					((dynamic)hoja1.Cells[1, j + 1]).RowHeight = 20;
					hoja1.Cells[1, j + 1] = dgvVentas.Columns[j].Name.ToUpper();
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
					for (j = 0; j < dgvVentas.Columns.Count; j++)
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
						((dynamic)hoja1.Cells[fila2, h + 1]).RowHeight = 17;
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
					if (i < dgvVentas.Rows.Count - 1)
					{
						dgvVentas.CurrentCell = dgvVentas.Rows[i + 1].Cells[0];
					}
				}
			}
			else
			{
				MessageBox.Show("SELECIONE UNA VENTA", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dgvVentas_CurrentCellChanged(object sender, EventArgs e)
	{
		try
		{
			if (dgvVentas.Rows.Count > 0 && dgvVentas.CurrentCell != null && dgvVentas.CurrentCell.RowIndex != -1)
			{
				listardetalleventa(dgvVentas.CurrentCell.RowIndex);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void button2_Click(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmresumenventas));
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dg_detalle = new System.Windows.Forms.DataGridView();
		this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.cb_tipocomprobante = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.cmbalmacen = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.dgvVentas1 = new Telerik.WinControls.UI.RadGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgvVentas = new System.Windows.Forms.DataGridView();
		this.btn_exportar = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.documento = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.icbper = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formapago = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.impreso = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.NotaDeCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.enviadoS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dg_detalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).BeginInit();
		base.SuspendLayout();
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dg_detalle);
		this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox2.Location = new System.Drawing.Point(82, 369);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1030, 277);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle de Venta";
		this.dg_detalle.AllowUserToAddRows = false;
		this.dg_detalle.AllowUserToDeleteRows = false;
		this.dg_detalle.AllowUserToResizeRows = false;
		this.dg_detalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dg_detalle.BackgroundColor = System.Drawing.Color.WhiteSmoke;
		this.dg_detalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dg_detalle.Columns.AddRange(this.item, this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.preciounit, this.dataGridViewTextBoxColumn1, this.igv, this.precioventa, this.nombre, this.fecharegistro);
		this.dg_detalle.Location = new System.Drawing.Point(15, 19);
		this.dg_detalle.Name = "dg_detalle";
		this.dg_detalle.ReadOnly = true;
		this.dg_detalle.RowHeadersVisible = false;
		this.dg_detalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dg_detalle.Size = new System.Drawing.Size(995, 252);
		this.dg_detalle.TabIndex = 3;
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
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 400;
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
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		dataGridViewCellStyle1.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 70;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		dataGridViewCellStyle2.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle2;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 75;
		this.dataGridViewTextBoxColumn1.DataPropertyName = "subtotal";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridViewTextBoxColumn1.HeaderText = "Importe";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 85;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		dataGridViewCellStyle4.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle4;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N2";
		dataGridViewCellStyle5.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle5;
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
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(252, 337);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 21;
		this.label6.Text = "Hasta :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(79, 337);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 20;
		this.label5.Text = "Desde :";
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(132, 334);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 19;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(299, 334);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 18;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged);
		this.cb_tipocomprobante.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cb_tipocomprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cb_tipocomprobante.FormattingEnabled = true;
		this.cb_tipocomprobante.Location = new System.Drawing.Point(634, 334);
		this.cb_tipocomprobante.Name = "cb_tipocomprobante";
		this.cb_tipocomprobante.Size = new System.Drawing.Size(202, 21);
		this.cb_tipocomprobante.TabIndex = 48;
		this.cb_tipocomprobante.SelectedValueChanged += new System.EventHandler(cb_tipocomprobante_SelectedValueChanged);
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(573, 338);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(55, 13);
		this.label4.TabIndex = 47;
		this.label4.Text = "T. Com :";
		this.cmbalmacen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmbalmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbalmacen.FormattingEnabled = true;
		this.cmbalmacen.Location = new System.Drawing.Point(937, 334);
		this.cmbalmacen.Name = "cmbalmacen";
		this.cmbalmacen.Size = new System.Drawing.Size(195, 21);
		this.cmbalmacen.TabIndex = 49;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(866, 338);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(63, 13);
		this.label1.TabIndex = 50;
		this.label1.Text = "Almacen :";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.button1.BackColor = System.Drawing.Color.GhostWhite;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		this.button1.Location = new System.Drawing.Point(434, 322);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(107, 37);
		this.button1.TabIndex = 51;
		this.button1.Text = "Actualizar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.dgvVentas1.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.dgvVentas1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVentas1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dgvVentas1.Location = new System.Drawing.Point(3, 16);
		this.dgvVentas1.MasterTemplate.AllowAddNewRow = false;
		this.dgvVentas1.MasterTemplate.AllowDragToGroup = false;
		this.dgvVentas1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codFactura";
		gridViewTextBoxColumn1.HeaderText = "Codigo";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codigo";
		gridViewTextBoxColumn2.FieldName = "fecha";
		gridViewTextBoxColumn2.HeaderText = "Fecha";
		gridViewTextBoxColumn2.Name = "fecha";
		gridViewTextBoxColumn2.Width = 100;
		gridViewTextBoxColumn3.FieldName = "documento";
		gridViewTextBoxColumn3.HeaderText = "T. Doc.";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "documento";
		gridViewTextBoxColumn4.FieldName = "numdoc";
		gridViewTextBoxColumn4.HeaderText = "N° Documento";
		gridViewTextBoxColumn4.Name = "numdoc";
		gridViewTextBoxColumn4.Width = 100;
		gridViewTextBoxColumn5.FieldName = "codcliente";
		gridViewTextBoxColumn5.HeaderText = "codcliente";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "codcliente";
		gridViewTextBoxColumn6.FieldName = "cliente";
		gridViewTextBoxColumn6.HeaderText = "Cliente";
		gridViewTextBoxColumn6.Multiline = true;
		gridViewTextBoxColumn6.Name = "cliente";
		gridViewTextBoxColumn6.Width = 157;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "moneda";
		gridViewTextBoxColumn7.HeaderText = "Moneda";
		gridViewTextBoxColumn7.Name = "moneda";
		gridViewTextBoxColumn7.Width = 57;
		gridViewTextBoxColumn8.FieldName = "total";
		gridViewTextBoxColumn8.HeaderText = "Importe";
		gridViewTextBoxColumn8.Name = "importe";
		gridViewTextBoxColumn8.Width = 100;
		gridViewTextBoxColumn9.FieldName = "icbper";
		gridViewTextBoxColumn9.HeaderText = "ICBPER";
		gridViewTextBoxColumn9.Name = "icbper";
		gridViewTextBoxColumn9.Width = 41;
		gridViewTextBoxColumn10.FieldName = "formapago";
		gridViewTextBoxColumn10.HeaderText = "F. Pago";
		gridViewTextBoxColumn10.Name = "formapago";
		gridViewTextBoxColumn10.Width = 100;
		gridViewTextBoxColumn11.FieldName = "fechapago";
		gridViewTextBoxColumn11.HeaderText = "Fech. Pago";
		gridViewTextBoxColumn11.Name = "fechapago";
		gridViewTextBoxColumn11.Width = 100;
		gridViewTextBoxColumn12.FieldName = "anulado";
		gridViewTextBoxColumn12.HeaderText = "Estado";
		gridViewTextBoxColumn12.Name = "estado";
		gridViewTextBoxColumn12.Width = 64;
		gridViewTextBoxColumn13.FieldName = "impreso";
		gridViewTextBoxColumn13.HeaderText = "Impreso";
		gridViewTextBoxColumn13.IsVisible = false;
		gridViewTextBoxColumn13.Name = "impreso";
		gridViewTextBoxColumn13.Width = 115;
		gridViewTextBoxColumn14.FieldName = "NotaDeCredito";
		gridViewTextBoxColumn14.HeaderText = "NotaCredito";
		gridViewTextBoxColumn14.Name = "NotaCredito";
		gridViewTextBoxColumn14.Width = 100;
		gridViewTextBoxColumn15.FieldName = "enviadoS";
		gridViewTextBoxColumn15.HeaderText = "Enviado Sunat";
		gridViewTextBoxColumn15.Name = "enviadoS";
		gridViewTextBoxColumn15.Width = 133;
		gridViewTextBoxColumn16.FieldName = "codtipodocumento";
		gridViewTextBoxColumn16.HeaderText = "CodTipoDoc";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "codtipodocumento";
		this.dgvVentas1.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16);
		this.dgvVentas1.MasterTemplate.EnableFiltering = true;
		this.dgvVentas1.MasterTemplate.EnableGrouping = false;
		this.dgvVentas1.MasterTemplate.EnablePaging = true;
		this.dgvVentas1.MasterTemplate.PageSize = 100;
		this.dgvVentas1.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvVentas1.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvVentas1.Name = "dgvVentas1";
		this.dgvVentas1.ReadOnly = true;
		this.dgvVentas1.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 16, 240, 150);
		this.dgvVentas1.ShowGroupPanel = false;
		this.dgvVentas1.Size = new System.Drawing.Size(1052, 282);
		this.dgvVentas1.TabIndex = 1;
		this.dgvVentas1.ThemeName = "Material";
		this.dgvVentas1.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvVentas1_CellClick);
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.dgvVentas1);
		this.groupBox3.Controls.Add(this.dgvVentas);
		this.groupBox3.Location = new System.Drawing.Point(74, 12);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1058, 301);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Ventas";
		this.dgvVentas.AllowUserToAddRows = false;
		this.dgvVentas.AllowUserToDeleteRows = false;
		this.dgvVentas.AllowUserToOrderColumns = true;
		this.dgvVentas.AllowUserToResizeRows = false;
		this.dgvVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvVentas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
		this.dgvVentas.Columns.AddRange(this.codigo, this.fecha, this.documento, this.numdoc, this.codcliente, this.cliente, this.moneda, this.importe, this.icbper, this.formapago, this.fechapago, this.anulado, this.impreso, this.NotaDeCredito, this.enviadoS);
		this.dgvVentas.Location = new System.Drawing.Point(3, 16);
		this.dgvVentas.MultiSelect = false;
		this.dgvVentas.Name = "dgvVentas";
		this.dgvVentas.ReadOnly = true;
		this.dgvVentas.RowHeadersVisible = false;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvVentas.RowsDefaultCellStyle = dataGridViewCellStyle8;
		this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVentas.Size = new System.Drawing.Size(58, 323);
		this.dgvVentas.TabIndex = 0;
		this.dgvVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVentas_CellClick);
		this.dgvVentas.CurrentCellChanged += new System.EventHandler(dgvVentas_CurrentCellChanged);
		this.dgvVentas.Click += new System.EventHandler(dgvVentas1_Click);
		this.btn_exportar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btn_exportar.BackColor = System.Drawing.Color.White;
		this.btn_exportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn_exportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btn_exportar.Image = SIGEFA.Properties.Resources.ganancia;
		this.btn_exportar.Location = new System.Drawing.Point(1124, 486);
		this.btn_exportar.Name = "btn_exportar";
		this.btn_exportar.Size = new System.Drawing.Size(106, 43);
		this.btn_exportar.TabIndex = 63;
		this.btn_exportar.Text = "Exportar";
		this.btn_exportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btn_exportar.UseVisualStyleBackColor = false;
		this.btn_exportar.Click += new System.EventHandler(btn_exportar_Click);
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button2.BackColor = System.Drawing.Color.White;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button2.Image = SIGEFA.Properties.Resources.x_button;
		this.button2.Location = new System.Drawing.Point(1124, 592);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(106, 43);
		this.button2.TabIndex = 64;
		this.button2.Text = "Salir";
		this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.codigo.DataPropertyName = "codFactura";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Visible = false;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.DataPropertyName = "documento";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle9;
		this.documento.HeaderText = "T. doc.";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Visible = false;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N° Documento";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.codcliente.DataPropertyName = "codcliente";
		this.codcliente.HeaderText = "Cod. Cliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.ReadOnly = true;
		this.codcliente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.codcliente.Visible = false;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle10;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.icbper.DataPropertyName = "icbper";
		this.icbper.HeaderText = "Icbper";
		this.icbper.Name = "icbper";
		this.icbper.ReadOnly = true;
		this.formapago.DataPropertyName = "formapago";
		this.formapago.HeaderText = "F. pago";
		this.formapago.Name = "formapago";
		this.formapago.ReadOnly = true;
		this.formapago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "Fech. Pago";
		this.fechapago.Name = "fechapago";
		this.fechapago.ReadOnly = true;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.anulado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.impreso.DataPropertyName = "impreso";
		this.impreso.HeaderText = "Impreso";
		this.impreso.Name = "impreso";
		this.impreso.ReadOnly = true;
		this.NotaDeCredito.DataPropertyName = "NotaDeCredito";
		this.NotaDeCredito.HeaderText = "NotaCredito";
		this.NotaDeCredito.Name = "NotaDeCredito";
		this.NotaDeCredito.ReadOnly = true;
		this.enviadoS.DataPropertyName = "enviadoS";
		this.enviadoS.HeaderText = "Enviado";
		this.enviadoS.Name = "enviadoS";
		this.enviadoS.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		base.ClientSize = new System.Drawing.Size(1251, 658);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.btn_exportar);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.cmbalmacen);
		base.Controls.Add(this.cb_tipocomprobante);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.Name = "frmresumenventas";
		this.Text = "frmresumenventas";
		base.Load += new System.EventHandler(frmresumenventas_Load);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dg_detalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
