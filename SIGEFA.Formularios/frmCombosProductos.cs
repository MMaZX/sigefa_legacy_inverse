using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmCombosProductos : Form
{
	private clsAdmProducto AdmProducto = new clsAdmProducto();

	public clsProducto pro = new clsProducto();

	public clsComboProductos combo = new clsComboProductos();

	private clsUnidadEquivalente equi = new clsUnidadEquivalente();

	public int codcombo;

	private clsAdmUnidadEquivalente clsuniequ = new clsAdmUnidadEquivalente();

	public DataTable d = null;

	public List<clsDetalleCombo> detalle = new List<clsDetalleCombo>();

	public List<clsDetalleCombo> detalle1 = new List<clsDetalleCombo>();

	public int Proceso = 1;

	public int codproductoseleccionado;

	public int codalmacenseleccionado;

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadGridView rgvlistaproductos;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private GroupBox groupBox2;

	private DateTimePicker dtfechavencimiento;

	private Label label1;

	private Label label2;

	private RadButton btnguardar;

	private RadButton btnsalir;

	private TextBoxX txtnombre;

	private DataGridView dgvproductos;

	private TextBoxX txttotal;

	private Label label3;

	private TextBox txtCodProducto;

	private Label label4;

	private TextBoxX txtstockcombo;

	private Label label5;

	private TextBoxX txtstockcombodisponible;

	private Label label6;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn punitario;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn codalmacen;

	private DataGridViewButtonXColumn btnconsultastock;

	public DataTable unidadesequi { get; set; }

	public frmCombosProductos()
	{
		InitializeComponent();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmCombosProductos_Load(object sender, EventArgs e)
	{
		if (Proceso == 1)
		{
			CargaProductosSeleccionar();
			return;
		}
		if (Proceso == 2)
		{
			CargaProducto();
			CargaProductosSeleccionar();
			return;
		}
		CargaProducto();
		btnguardar.Enabled = false;
		txtnombre.Enabled = false;
		dtfechavencimiento.Enabled = false;
		txtstockcombo.Enabled = false;
	}

	private void CargaProductosSeleccionar()
	{
		rgvlistaproductos.DataSource = AdmProducto.CatalogoProductos();
		dgvdetalleEditable(editable: true);
		txtnombre.Focus();
		unidadesequi = clsuniequ.listar_unidad_equivalente(frmLogin.iCodAlmacen);
	}

	private void CargaProducto()
	{
		try
		{
			combo = AdmProducto.CargaCombo(codcombo);
			txtCodProducto.Text = combo.CodCombo.ToString();
			txtnombre.Text = combo.NombreCombo;
			dtfechavencimiento.Value = combo.FechaVencimiento.Date;
			txtstockcombo.Text = combo.stockcombo.ToString();
			txtstockcombodisponible.Text = combo.stockcombodisponible.ToString();
			CargaDetalle();
			calculatotales();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaDetalle()
	{
		DataTable newData = new DataTable();
		dgvproductos.Rows.Clear();
		try
		{
			newData = AdmProducto.CargaProductosCombo(codcombo);
			foreach (DataRow row in newData.Rows)
			{
				dgvproductos.Rows.Add(Convert.ToInt32(row[0]), row[1], row[2].ToString(), row[3], row[4], row[5], row[6]);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvdetalleEditable(bool editable)
	{
		foreach (GridViewDataColumn col in rgvlistaproductos.Columns)
		{
			col.ReadOnly = true;
			if (editable)
			{
				if (col.Name == "cantidad" || col.Name == "unidad2" || col.Name == "preciocombo")
				{
					col.ReadOnly = false;
				}
				else
				{
					col.ReadOnly = true;
				}
			}
		}
	}

	private void rgvlistaproductos_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (!((rgvlistaproductos.Rows.Count > 0) & (rgvlistaproductos.SelectedRows.Count > 0)))
			{
				return;
			}
			if (rgvlistaproductos.Columns[e.ColumnIndex].Name == "cantidad" && rgvlistaproductos.Rows[e.RowIndex].Cells[cantidad.Name].Value != null && rgvlistaproductos.Rows[e.RowIndex].Cells[codunidad.Name].Value != null && rgvlistaproductos.Rows[e.RowIndex].Cells[4].Value != null && Convert.ToInt32(rgvlistaproductos.Rows[e.RowIndex].Cells[4].Value) != 0)
			{
				if (rgvlistaproductos.Rows[e.RowIndex].Cells[codunidad.Name].Value != null)
				{
					int cod = Convert.ToInt32(rgvlistaproductos.Rows[e.RowIndex].Cells[referencia.Name].Value);
					bool existe = false;
					foreach (DataGridViewRow row in (IEnumerable)dgvproductos.Rows)
					{
						if (Convert.ToInt32(row.Cells[referencia.Name].Value) == cod)
						{
							existe = true;
							break;
						}
					}
					if (!existe)
					{
						foreach (GridViewRowInfo row2 in rgvlistaproductos.Rows)
						{
							try
							{
								if (row2.Index == rgvlistaproductos.CurrentCell.RowIndex)
								{
									DataGridViewRow fila = new DataGridViewRow();
									fila.CreateCells(dgvproductos);
									fila.Cells[0].Value = row2.Cells[0].Value;
									fila.Cells[1].Value = row2.Cells[1].Value;
									fila.Cells[2].Value = row2.Cells[2].Value;
									fila.Cells[3].Value = row2.Cells[4].Value;
									fila.Cells[4].Value = row2.Cells[5].Value;
									fila.Cells[5].Value = row2.Cells[6].Value;
									fila.Cells[6].Value = row2.Cells[8].Value;
									dgvproductos.Rows.Add(fila);
									calculatotales();
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.ToString(), "OrdenCompraCotización", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						return;
					}
					MessageBox.Show("El producto ya se encuentra agregado, primero elimine y vuelva a agregarlo");
				}
				else
				{
					MessageBox.Show("Error al agregar producto");
				}
			}
			else if (rgvlistaproductos.Columns[e.ColumnIndex].Name == "preciocombo")
			{
				int CodProducto = Convert.ToInt32(rgvlistaproductos.Rows[e.RowIndex].Cells[referencia.Name].Value);
				int Unidad = Convert.ToInt32(rgvlistaproductos.Rows[e.RowIndex].Cells[2].Value);
				decimal preciocombo = Convert.ToInt32(rgvlistaproductos.Rows[e.RowIndex].Cells[4].Value);
				decimal flete = default(decimal);
				decimal desestiva = default(decimal);
				decimal totalcosto = default(decimal);
				decimal preciocompra = default(decimal);
				DataTable costos = AdmProducto.CostoTotalProducto(CodProducto, Unidad);
				preciocompra = AdmProducto.UltimoPrecioCompraProducto(CodProducto, 0, Unidad);
				flete = Convert.ToDecimal(costos.Rows[0]["flete"]);
				desestiva = Convert.ToDecimal(costos.Rows[0]["desestiva"]);
				totalcosto = Math.Round(flete + desestiva + preciocompra, 2);
				if (totalcosto > preciocombo)
				{
					MessageBox.Show("EL PRECIO DE VENTA INGRESADO ES MENOR AL  COSTO DEL PRODUCTO", "COMBOS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					rgvlistaproductos.Rows[e.RowIndex].Cells[4].Value = 0;
				}
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}

	public void calculatotales()
	{
		double total = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvproductos.Rows)
		{
			total += Convert.ToDouble(row.Cells[cantidad.Name].Value) * Convert.ToDouble(row.Cells[punitario.Name].Value);
		}
		txttotal.Text = $"{total:#,##0.00}";
	}

	private void btnguardar_Click(object sender, EventArgs e)
	{
		try
		{
			combo.CodUsuario = frmLogin.iCodUser;
			if (string.IsNullOrEmpty(txtnombre.Text.Trim()))
			{
				MessageBox.Show("Debes Ingresar un Nombre");
				return;
			}
			combo.NombreCombo = txtnombre.Text.Trim();
			combo.Estado = true;
			combo.Total = Convert.ToDecimal(txttotal.Text);
			combo.FechaVencimiento = dtfechavencimiento.Value.Date;
			combo.FechaRegistro = DateTime.Now;
			combo.stockcombo = Convert.ToInt32(txtstockcombo.Text);
			combo.stockcombodisponible = Convert.ToInt32(txtstockcombo.Text);
			if (Proceso == 1)
			{
				if (!AdmProducto.insertcombo(combo))
				{
					return;
				}
				txtCodProducto.Text = combo.CodCombo.ToString();
				RecorreDetalle();
				if (detalle.Count > 0)
				{
					foreach (clsDetalleCombo det in detalle)
					{
						AdmProducto.insertdetallecombo(det);
					}
				}
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				btnguardar.Enabled = false;
			}
			else
			{
				if (!AdmProducto.updatecombo(combo))
				{
					return;
				}
				txtCodProducto.Text = combo.CodCombo.ToString();
				RecorreDetalle();
				if (detalle.Count > 0)
				{
					foreach (clsDetalleCombo det2 in detalle)
					{
						AdmProducto.insertdetallecombo(det2);
					}
				}
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				btnguardar.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		if (dgvproductos.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvproductos.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		try
		{
			clsDetalleCombo deta = new clsDetalleCombo();
			deta.codproducto = Convert.ToInt32(fila.Cells[referencia.Name].Value);
			deta.cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.codunidad = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			deta.precio = Convert.ToDecimal(fila.Cells[punitario.Name].Value);
			deta.codcombo = Convert.ToInt32(txtCodProducto.Text);
			deta.codalmacen = Convert.ToInt32(fila.Cells[codalmacen.Name].Value);
			detalle.Add(deta);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rgvlistaproductos_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (rgvlistaproductos.Columns[e.ColumnIndex].Name == "unidad2" && rgvlistaproductos.CurrentCell != null)
			{
				setUnidades();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void setUnidades()
	{
		d = null;
		if (rgvlistaproductos.CurrentRow != null)
		{
			GridViewRowInfo row = rgvlistaproductos.CurrentRow;
			GridViewComboBoxColumn unidad = rgvlistaproductos.Columns["unidad2"] as GridViewComboBoxColumn;
			var query = from x in unidadesequi.AsEnumerable()
				where x.Field<int>("codProducto") == Convert.ToInt32(row.Cells[referencia.Name].Value) && x.Field<int>("codstockalma") == frmLogin.iCodAlmacen
				select new
				{
					codUnidadEquivalente = x.Field<int>("codUnidadEquivalente"),
					codUnidadMedida = x.Field<int>("codUnidadMedida"),
					descripcion = x.Field<string>("descripcion"),
					precio = x.Field<decimal>("Precio")
				};
			var lista = query.ToList();
			unidad.DataSource = lista;
			unidad.DisplayMember = "descripcion";
			unidad.ValueMember = "codUnidadEquivalente";
			row.Cells["precioventa"].Value = decimal.Parse(lista[0].precio.ToString());
			row.Cells["codUnidad"].Value = lista[0].codUnidadMedida.ToString();
		}
	}

	private void dgvproductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		frmMuestraStock frm = new frmMuestraStock();
		frm.CodProducto = codproductoseleccionado;
		frm.CodAlmacen = codalmacenseleccionado;
		frm.ShowDialog();
	}

	private void dgvproductos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvproductos.Rows.Count >= 1)
			{
				codproductoseleccionado = Convert.ToInt32(dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells[referencia.Name].Value);
				codalmacenseleccionado = Convert.ToInt32(dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells[codalmacen.Name].Value);
			}
		}
		catch (Exception)
		{
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvlistaproductos = new Telerik.WinControls.UI.RadGridView();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvproductos = new System.Windows.Forms.DataGridView();
		this.dtfechavencimiento = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.btnguardar = new Telerik.WinControls.UI.RadButton();
		this.btnsalir = new Telerik.WinControls.UI.RadButton();
		this.txtnombre = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txttotal = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label3 = new System.Windows.Forms.Label();
		this.txtCodProducto = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtstockcombo = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label5 = new System.Windows.Forms.Label();
		this.txtstockcombodisponible = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label6 = new System.Windows.Forms.Label();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.punitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnconsultastock = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvlistaproductos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvlistaproductos.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvproductos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.rgvlistaproductos);
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(22, 73);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(934, 239);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Buscar Producto";
		this.rgvlistaproductos.BackColor = System.Drawing.SystemColors.Control;
		this.rgvlistaproductos.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvlistaproductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvlistaproductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.rgvlistaproductos.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvlistaproductos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvlistaproductos.Location = new System.Drawing.Point(3, 16);
		this.rgvlistaproductos.MasterTemplate.AllowAddNewRow = false;
		this.rgvlistaproductos.MasterTemplate.AllowColumnReorder = false;
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.FieldName = "referencia";
		gridViewTextBoxColumn1.HeaderText = "Cod. Ref";
		gridViewTextBoxColumn1.Name = "referencia";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.FieldName = "descripcion";
		gridViewTextBoxColumn2.HeaderText = "Descripción";
		gridViewTextBoxColumn2.Name = "descripcion";
		gridViewTextBoxColumn2.Width = 350;
		gridViewComboBoxColumn1.FieldName = "unidad2";
		gridViewComboBoxColumn1.HeaderText = "Unidad";
		gridViewComboBoxColumn1.Name = "unidad2";
		gridViewComboBoxColumn1.Width = 200;
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "precioventa";
		gridViewTextBoxColumn3.HeaderText = "P.Unitario Ref.";
		gridViewTextBoxColumn3.Name = "precioventa";
		gridViewTextBoxColumn3.Width = 100;
		gridViewTextBoxColumn4.FieldName = "preciocombo";
		gridViewTextBoxColumn4.HeaderText = "P.Unitario Combo";
		gridViewTextBoxColumn4.Name = "preciocombo";
		gridViewTextBoxColumn4.Width = 120;
		gridViewTextBoxColumn5.EnableExpressionEditor = false;
		gridViewTextBoxColumn5.FieldName = "cantidad";
		gridViewTextBoxColumn5.HeaderText = "Cantidad";
		gridViewTextBoxColumn5.Name = "cantidad";
		gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn5.Width = 100;
		gridViewTextBoxColumn6.FieldName = "codUnidad";
		gridViewTextBoxColumn6.HeaderText = "codUnidadMedida";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "codUnidad";
		gridViewTextBoxColumn6.Width = 120;
		gridViewTextBoxColumn7.FieldName = "codProducto";
		gridViewTextBoxColumn7.HeaderText = "codProducto";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "codProducto";
		gridViewTextBoxColumn8.FieldName = "codalmacen";
		gridViewTextBoxColumn8.HeaderText = "codalmacen";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "codalmacen";
		this.rgvlistaproductos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewComboBoxColumn1, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8);
		this.rgvlistaproductos.MasterTemplate.EnableFiltering = true;
		this.rgvlistaproductos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvlistaproductos.Name = "rgvlistaproductos";
		this.rgvlistaproductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvlistaproductos.ShowGroupPanel = false;
		this.rgvlistaproductos.Size = new System.Drawing.Size(928, 220);
		this.rgvlistaproductos.TabIndex = 0;
		this.rgvlistaproductos.ThemeName = "TelerikMetroTouch";
		this.rgvlistaproductos.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvlistaproductos_CellEndEdit);
		this.rgvlistaproductos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvlistaproductos_CellClick);
		this.groupBox2.Controls.Add(this.dgvproductos);
		this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox2.Location = new System.Drawing.Point(25, 318);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(931, 239);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Productos Seleccionados:";
		this.dgvproductos.AllowUserToAddRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.CornflowerBlue;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvproductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvproductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvproductos.Columns.AddRange(this.referencia, this.descripcion, this.unidad, this.punitario, this.cantidad, this.codunidad, this.codalmacen, this.btnconsultastock);
		this.dgvproductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvproductos.EnableHeadersVisualStyles = false;
		this.dgvproductos.Location = new System.Drawing.Point(3, 16);
		this.dgvproductos.Name = "dgvproductos";
		this.dgvproductos.ReadOnly = true;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvproductos.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dgvproductos.Size = new System.Drawing.Size(925, 220);
		this.dgvproductos.TabIndex = 0;
		this.dgvproductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvproductos_CellClick);
		this.dgvproductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvproductos_CellContentClick);
		this.dtfechavencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfechavencimiento.Location = new System.Drawing.Point(260, 38);
		this.dtfechavencimiento.Name = "dtfechavencimiento";
		this.dtfechavencimiento.Size = new System.Drawing.Size(102, 20);
		this.dtfechavencimiento.TabIndex = 2;
		this.label1.Location = new System.Drawing.Point(257, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(121, 20);
		this.label1.TabIndex = 3;
		this.label1.Text = "Fecha Vencimiento:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.Location = new System.Drawing.Point(19, 12);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(121, 20);
		this.label2.TabIndex = 4;
		this.label2.Text = "Nombre Combo:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnguardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnguardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnguardar.Location = new System.Drawing.Point(763, 563);
		this.btnguardar.Name = "btnguardar";
		this.btnguardar.Size = new System.Drawing.Size(99, 38);
		this.btnguardar.TabIndex = 7;
		this.btnguardar.Text = "Guardar";
		this.btnguardar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnguardar.ThemeName = "TelerikMetroTouch";
		this.btnguardar.Click += new System.EventHandler(btnguardar_Click);
		this.btnsalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnsalir.Image = SIGEFA.Properties.Resources.save;
		this.btnsalir.Location = new System.Drawing.Point(868, 563);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(74, 38);
		this.btnsalir.TabIndex = 6;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnsalir.ThemeName = "TelerikMetroTouch";
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.txtnombre.Border.Class = "TextBoxBorder";
		this.txtnombre.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtnombre.ButtonCustom.Tooltip = "";
		this.txtnombre.ButtonCustom2.Tooltip = "";
		this.txtnombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtnombre.Location = new System.Drawing.Point(22, 38);
		this.txtnombre.Name = "txtnombre";
		this.txtnombre.PreventEnterBeep = true;
		this.txtnombre.Size = new System.Drawing.Size(222, 20);
		this.txtnombre.TabIndex = 8;
		this.txttotal.Border.Class = "TextBoxBorder";
		this.txttotal.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txttotal.ButtonCustom.Tooltip = "";
		this.txttotal.ButtonCustom2.Tooltip = "";
		this.txttotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txttotal.Enabled = false;
		this.txttotal.Location = new System.Drawing.Point(391, 38);
		this.txttotal.Name = "txttotal";
		this.txttotal.PreventEnterBeep = true;
		this.txttotal.Size = new System.Drawing.Size(95, 20);
		this.txttotal.TabIndex = 9;
		this.txttotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txttotal.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		this.label3.Location = new System.Drawing.Point(423, 9);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 20);
		this.label3.TabIndex = 10;
		this.label3.Text = "Total :";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtCodProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodProducto.Enabled = false;
		this.txtCodProducto.Location = new System.Drawing.Point(782, 41);
		this.txtCodProducto.Name = "txtCodProducto";
		this.txtCodProducto.ReadOnly = true;
		this.txtCodProducto.Size = new System.Drawing.Size(108, 20);
		this.txtCodProducto.TabIndex = 139;
		this.txtCodProducto.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(738, 44);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(33, 13);
		this.label4.TabIndex = 140;
		this.label4.Text = "Item: ";
		this.label4.Visible = false;
		this.txtstockcombo.Border.Class = "TextBoxBorder";
		this.txtstockcombo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtstockcombo.ButtonCustom.Tooltip = "";
		this.txtstockcombo.ButtonCustom2.Tooltip = "";
		this.txtstockcombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtstockcombo.Location = new System.Drawing.Point(492, 38);
		this.txtstockcombo.Name = "txtstockcombo";
		this.txtstockcombo.PreventEnterBeep = true;
		this.txtstockcombo.Size = new System.Drawing.Size(71, 20);
		this.txtstockcombo.TabIndex = 142;
		this.txtstockcombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label5.Location = new System.Drawing.Point(492, 12);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(71, 20);
		this.label5.TabIndex = 141;
		this.label5.Text = "StockCombo:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtstockcombodisponible.Border.Class = "TextBoxBorder";
		this.txtstockcombodisponible.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtstockcombodisponible.ButtonCustom.Tooltip = "";
		this.txtstockcombodisponible.ButtonCustom2.Tooltip = "";
		this.txtstockcombodisponible.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtstockcombodisponible.Enabled = false;
		this.txtstockcombodisponible.Location = new System.Drawing.Point(585, 38);
		this.txtstockcombodisponible.Name = "txtstockcombodisponible";
		this.txtstockcombodisponible.PreventEnterBeep = true;
		this.txtstockcombodisponible.Size = new System.Drawing.Size(71, 20);
		this.txtstockcombodisponible.TabIndex = 144;
		this.txtstockcombodisponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label6.Location = new System.Drawing.Point(586, -1);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(71, 36);
		this.label6.TabIndex = 143;
		this.label6.Text = "StockCombo Disponible:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Cod. Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Width = 120;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripción";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 300;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.punitario.DataPropertyName = "punitario";
		this.punitario.HeaderText = "P.Unitario";
		this.punitario.Name = "punitario";
		this.punitario.ReadOnly = true;
		this.punitario.Width = 120;
		this.cantidad.DataPropertyName = "cantidad";
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Width = 120;
		this.codunidad.DataPropertyName = "codunidad";
		this.codunidad.HeaderText = "codunidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Visible = false;
		this.codalmacen.HeaderText = "codalmacen";
		this.codalmacen.Name = "codalmacen";
		this.codalmacen.ReadOnly = true;
		this.codalmacen.Visible = false;
		this.btnconsultastock.HeaderText = "Verifica Stock";
		this.btnconsultastock.Name = "btnconsultastock";
		this.btnconsultastock.ReadOnly = true;
		this.btnconsultastock.Text = null;
		this.btnconsultastock.Width = 150;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(956, 625);
		base.Controls.Add(this.txtstockcombodisponible);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.txtstockcombo);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.txtCodProducto);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.txttotal);
		base.Controls.Add(this.txtnombre);
		base.Controls.Add(this.btnguardar);
		base.Controls.Add(this.btnsalir);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dtfechavencimiento);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmCombosProductos";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmCombosProductos";
		base.Load += new System.EventHandler(frmCombosProductos_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvlistaproductos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvlistaproductos).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvproductos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
