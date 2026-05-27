using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmIntermedioNotaCreditoDespacho : Form
{
	public List<string[]> almacenes = new List<string[]>();

	public DataTable data = null;

	public List<object[]> seleccion = new List<object[]>();

	public int codAlmacenProdSinDespacho = 0;

	public DataTable ultimaData = null;

	internal int Proceso = 1;

	private Color[] colores_columnas = new Color[4]
	{
		Color.FromArgb(0, 255, 179),
		Color.FromArgb(226, 255, 0),
		Color.FromArgb(255, 128, 0),
		Color.FromArgb(255, 0, 202)
	};

	private int i = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadGridView rgvListado;

	private GroupBox groupBox2;

	private Button btnCancelar;

	private Button btnGuardar;

	public frmIntermedioNotaCreditoDespacho()
	{
		InitializeComponent();
	}

	private void frmIntermedioNotaCreditoDespacho_Load(object sender, EventArgs e)
	{
		agregandoColumnas();
		rgvListado.DataSource = data;
		bloqueandoProductosSinDespacho();
		addFormatoColumna();
		if (Proceso == 2)
		{
			rgvListado.ReadOnly = true;
			btnGuardar.Visible = false;
		}
	}

	private void addFormatoColumna()
	{
		Color color = Color.FromArgb(185, 185, 185);
		ConditionalFormattingObject c1 = new ConditionalFormattingObject("Pintar de Plomo Productos De Solo Venta", ConditionTypes.Equal, "1", "1", applyToRow: true);
		BaseFormattingObject b1 = new BaseFormattingObject();
		b1.CellBackColor = color;
		b1.ApplyOnSelectedRows = false;
		b1.Enabled = true;
		c1.RowBackColor = color;
		c1.CellBackColor = color;
		rgvListado.Columns["colTipo"].ConditionalFormattingObjectList.Add(c1);
	}

	private void bloqueandoProductosSinDespacho()
	{
		try
		{
			foreach (GridViewRowInfo fila in rgvListado.Rows)
			{
				int tipo = Convert.ToInt32(fila.Cells["colTipo"].Value);
				if (tipo == 1)
				{
					if (codAlmacenProdSinDespacho == 0)
					{
						throw new Exception("No se especifico un codigo de almacen para los productos sin despacho");
					}
					foreach (string[] item in almacenes)
					{
						if (int.TryParse(item[0], out var codAlmacen))
						{
							fila.Cells[codAlmacen.ToString()].ReadOnly = true;
							if (codAlmacen == codAlmacenProdSinDespacho)
							{
								fila.Cells[codAlmacenProdSinDespacho.ToString()].Value = true;
								fila.Cells[item[1].ToString() + "1"].Value = fila.Cells["colCtdadNC"].Value.ToString();
							}
						}
					}
				}
				if (tipo != 2)
				{
					continue;
				}
				foreach (string[] item2 in almacenes)
				{
					if (int.TryParse(item2[0], out var codAlmacen2) && Convert.ToInt32(fila.Cells[codAlmacen2.ToString()].Value) == 1)
					{
						fila.Cells[codAlmacen2.ToString()].Value = false;
						GridViewCellEventArgs e = new GridViewCellEventArgs(fila, fila.Cells[codAlmacen2.ToString()].ColumnInfo, rgvListado.ActiveEditor);
						rgvListado_CellClick(new object(), e);
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error ocurrido en interfaz seleccionador", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			base.DialogResult = DialogResult.Cancel;
			Close();
		}
	}

	private void agregandoColumnas()
	{
		foreach (string[] item in almacenes)
		{
			if (int.TryParse(item[0], out var codAlmacen))
			{
				añadiendoColumnaDeStockAlmacen(codAlmacen, item[1]);
			}
			else
			{
				MessageBox.Show("No se pudo agregar la siguiente columna: " + item[0] + " - " + item[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void añadiendoColumnaDeStockAlmacen(int codAlmacen, string descripAlmacen)
	{
		ConditionalFormattingObject c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		BaseFormattingObject b1 = new BaseFormattingObject();
		b1.CellBackColor = colores_columnas[i];
		b1.ApplyOnSelectedRows = false;
		b1.Enabled = true;
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		GridViewCheckBoxColumn colCheckBox = new GridViewCheckBoxColumn();
		colCheckBox.HeaderText = "#";
		colCheckBox.FieldName = codAlmacen.ToString();
		colCheckBox.Name = codAlmacen.ToString();
		colCheckBox.Tag = descripAlmacen;
		colCheckBox.ReadOnly = true;
		colCheckBox.Width = 50;
		colCheckBox.AllowFiltering = false;
		colCheckBox.AllowReorder = false;
		colCheckBox.AllowSort = false;
		colCheckBox.ConditionalFormattingObjectList.Add(c1);
		colCheckBox.ConditionalFormattingObjectList.Add(c2);
		colCheckBox.ConditionalFormattingObjectList.Add(c3);
		GridViewDecimalColumn colText = new GridViewDecimalColumn();
		colText.HeaderText = "Cantidad Permitida";
		colText.WrapText = true;
		colText.FieldName = descripAlmacen;
		colText.Name = descripAlmacen;
		colText.Width = 100;
		colText.DecimalPlaces = 4;
		colText.FormatString = "{0:#.00}";
		colText.ReadOnly = true;
		colText.AllowFiltering = false;
		colText.AllowReorder = false;
		colText.AllowSort = false;
		colText.ConditionalFormattingObjectList.Add(c1);
		colText.ConditionalFormattingObjectList.Add(c2);
		colText.ConditionalFormattingObjectList.Add(c3);
		GridViewDecimalColumn colText2 = new GridViewDecimalColumn();
		colText2.HeaderText = descripAlmacen;
		colText2.FieldName = descripAlmacen + "1";
		colText2.Name = descripAlmacen + "1";
		colText2.Width = 100;
		colText2.DecimalPlaces = 4;
		colText2.FormatString = "{0:+ #.00}";
		colText2.ReadOnly = true;
		colText2.AllowFiltering = false;
		colText2.AllowReorder = false;
		colText2.AllowSort = false;
		colText2.ConditionalFormattingObjectList.Add(c1);
		colText2.ConditionalFormattingObjectList.Add(c2);
		colText2.ConditionalFormattingObjectList.Add(c3);
		rgvListado.Columns.Add(colText);
		rgvListado.Columns.Add(colCheckBox);
		rgvListado.Columns.Add(colText2);
		i++;
		if (i == 4)
		{
			i = 0;
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void rgvListado_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex < 0 || e.ColumnIndex <= 1 || !validaCheckBox(e))
			{
				return;
			}
			if (rgvListado.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
			{
				return;
			}
			GridViewRowInfo fila = rgvListado.Rows[e.RowIndex];
			string codAlmSel = e.Column.Name;
			List<string[]> encontrado = Enumerable.Where<string[]>(almacenes.AsEnumerable(), (Func<string[], bool>)((string[] x) => x[0] == codAlmSel)).ToList();
			if (encontrado.Count > 0)
			{
				string[] almsel = encontrado[0];
				double cantidadNC = Convert.ToDouble(fila.Cells["colCtdadNC"].Value.ToString());
				string cantidadSeleccionada = ((fila.Cells[almsel[1]].Value == null || fila.Cells[almsel[1]].Value == DBNull.Value) ? "" : fila.Cells[almsel[1]].Value.ToString());
				if (!double.TryParse(cantidadSeleccionada, out var cantidadAlmSel))
				{
					return;
				}
				object valor = fila.Cells[e.ColumnIndex].Value;
				bool check_val = Convert.ToBoolean((valor == DBNull.Value) ? ((object)false) : valor);
				rgvListado.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !check_val;
				if (!check_val)
				{
					string name_columna = getColumnaSeleccionada(e.RowIndex, e.ColumnIndex);
					if (name_columna != "")
					{
						rgvListado.Rows[e.RowIndex].Cells[name_columna].Value = false;
					}
				}
				if (cantidadAlmSel > cantidadNC)
				{
					fila.Cells[almsel[1].ToString() + "1"].Value = fila.Cells["colCtdadNC"].Value.ToString();
				}
				else
				{
					fila.Cells[almsel[1].ToString() + "1"].Value = cantidadAlmSel;
				}
				if (check_val)
				{
					fila.Cells[almsel[1].ToString() + "1"].Value = null;
				}
				List<string[]> encontrado2 = Enumerable.Where<string[]>(almacenes.AsEnumerable(), (Func<string[], bool>)((string[] x) => x[0] != codAlmSel)).ToList();
				if (encontrado2.Count > 0)
				{
					string[] almsec = encontrado2[0];
					string cantisadAlmSecObj = ((fila.Cells[almsec[1]].Value == null || fila.Cells[almsec[1]].Value == DBNull.Value) ? "" : fila.Cells[almsec[1]].Value.ToString());
					if (double.TryParse(cantisadAlmSecObj, out var _))
					{
						if (cantidadAlmSel > cantidadNC)
						{
							fila.Cells[almsec[1].ToString() + "1"].Value = null;
						}
						else
						{
							fila.Cells[almsec[1].ToString() + "1"].Value = cantidadNC - cantidadAlmSel;
						}
						if (check_val)
						{
							fila.Cells[almsec[1].ToString() + "1"].Value = null;
						}
					}
				}
				else if (almacenes.Count > 1)
				{
					throw new Exception("No se encontro almacen con codigo diferente de " + codAlmSel + " en el listado de almacenes de la interfaz seleccionador.\nRecargue la Ventana");
				}
				return;
			}
			throw new Exception("No se encontro almacen con codigo: " + codAlmSel + ".\nRecargue la Ventana");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private string getColumnaSeleccionada(int rowIndex, int columnIndex)
	{
		string name_columna = "";
		foreach (string[] elemento in almacenes)
		{
			if (rgvListado.Columns[columnIndex].Name != elemento[0])
			{
				object valor = rgvListado.Rows[rowIndex].Cells[elemento[0]].Value;
				if (Convert.ToBoolean((valor == DBNull.Value) ? ((object)false) : valor))
				{
					name_columna = elemento[0];
					break;
				}
			}
		}
		return name_columna;
	}

	private bool validaCheckBox(GridViewCellEventArgs e)
	{
		bool band = false;
		GridViewCellInfo aux = rgvListado.Rows[e.RowIndex].Cells[e.ColumnIndex];
		if (aux.ColumnInfo.GetType() == typeof(GridViewCheckBoxColumn))
		{
			band = true;
		}
		return band;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (verificarSeleccion())
			{
				foreach (GridViewRowInfo fila in rgvListado.Rows)
				{
					if (Convert.ToInt32(fila.Cells["colTipo"].Value) != 0)
					{
						continue;
					}
					foreach (string[] item in almacenes)
					{
						object valor = fila.Cells[item[0]].Value;
						if (Convert.ToBoolean((valor == DBNull.Value) ? ((object)false) : valor))
						{
							object[] obj = new object[3]
							{
								Convert.ToInt32(fila.Cells["colCodProducto"].Value),
								Convert.ToInt32(item[0]),
								Convert.ToDouble(fila.Cells[item[1] + "1"].Value)
							};
							seleccion.Add(obj);
						}
					}
				}
				ultimaData = (DataTable)rgvListado.DataSource;
				base.DialogResult = DialogResult.Yes;
			}
			else
			{
				MessageBox.Show("Debe seleccionar almacen de entrega en todos los productos", "Almacen no seleccionado para producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			base.DialogResult = DialogResult.Cancel;
			MessageBox.Show(ex.Message, "Ocurrio Un Error Al Guardar El Listado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool verificarSeleccion()
	{
		bool band = true;
		foreach (GridViewRowInfo fila in rgvListado.Rows)
		{
			foreach (string[] item in almacenes)
			{
				object valor = fila.Cells[item[0]].Value;
				band = Convert.ToBoolean((valor == DBNull.Value) ? ((object)false) : valor);
				if (band)
				{
					break;
				}
			}
			if (!band)
			{
				break;
			}
		}
		return band;
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvListado = new Telerik.WinControls.UI.RadGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvListado).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListado.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvListado);
		this.groupBox1.Location = new System.Drawing.Point(0, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(981, 281);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.rgvListado.AutoScroll = true;
		this.rgvListado.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvListado.Location = new System.Drawing.Point(3, 16);
		this.rgvListado.MasterTemplate.AllowAddNewRow = false;
		this.rgvListado.MasterTemplate.AllowColumnReorder = false;
		this.rgvListado.MasterTemplate.AllowDeleteRow = false;
		this.rgvListado.MasterTemplate.AllowDragToGroup = false;
		this.rgvListado.MasterTemplate.AllowEditRow = false;
		gridViewTextBoxColumn1.FieldName = "codigo";
		gridViewTextBoxColumn1.HeaderText = "codigo";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodigo";
		gridViewTextBoxColumn2.FieldName = "codProducto";
		gridViewTextBoxColumn2.HeaderText = "codProducto";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodProducto";
		gridViewTextBoxColumn3.FieldName = "referencia";
		gridViewTextBoxColumn3.HeaderText = "Referencia";
		gridViewTextBoxColumn3.Name = "colReferencia";
		gridViewTextBoxColumn3.Width = 80;
		gridViewTextBoxColumn4.FieldName = "descripcion";
		gridViewTextBoxColumn4.HeaderText = "Descripcion";
		gridViewTextBoxColumn4.Name = "colDescripcion";
		gridViewTextBoxColumn4.Width = 200;
		gridViewTextBoxColumn5.FieldName = "unidad";
		gridViewTextBoxColumn5.HeaderText = "Unidad";
		gridViewTextBoxColumn5.Name = "colUnidad";
		gridViewTextBoxColumn5.Width = 100;
		gridViewTextBoxColumn6.FieldName = "tipo";
		gridViewTextBoxColumn6.HeaderText = "tipo";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "colTipo";
		gridViewTextBoxColumn7.DataType = typeof(decimal);
		gridViewTextBoxColumn7.FieldName = "ctdadNC";
		gridViewTextBoxColumn7.FormatString = "{0:#.00}";
		gridViewTextBoxColumn7.HeaderText = "Cantidad NC";
		gridViewTextBoxColumn7.Name = "colCtdadNC";
		gridViewTextBoxColumn7.Width = 100;
		gridViewTextBoxColumn8.FieldName = "codUnidad";
		gridViewTextBoxColumn8.HeaderText = "codUnidad";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colCodUnidad";
		this.rgvListado.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8);
		this.rgvListado.MasterTemplate.EnableFiltering = true;
		this.rgvListado.MasterTemplate.EnableGrouping = false;
		this.rgvListado.MasterTemplate.MultiSelect = true;
		this.rgvListado.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvListado.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvListado.Name = "rgvListado";
		this.rgvListado.Size = new System.Drawing.Size(975, 262);
		this.rgvListado.TabIndex = 0;
		this.rgvListado.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvListado_CellClick);
		this.groupBox2.Controls.Add(this.btnGuardar);
		this.groupBox2.Controls.Add(this.btnCancelar);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 299);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(981, 69);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.BackColor = System.Drawing.Color.SteelBlue;
		this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Location = new System.Drawing.Point(800, 19);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(79, 36);
		this.btnGuardar.TabIndex = 1;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.UseVisualStyleBackColor = false;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.BackColor = System.Drawing.Color.White;
		this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Location = new System.Drawing.Point(885, 19);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(79, 36);
		this.btnCancelar.TabIndex = 0;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.UseVisualStyleBackColor = false;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(981, 368);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmIntermedioNotaCreditoDespacho";
		this.Text = "Seleccion Almacen Donde Regresar Productos:";
		base.Load += new System.EventHandler(frmIntermedioNotaCreditoDespacho_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvListado.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListado).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
