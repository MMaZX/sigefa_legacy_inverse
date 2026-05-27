using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmImportarExcelN1 : Form
{
	private int contador = 0;

	private int resume = 0;

	private int codproducto = 0;

	private int i = 0;

	private int Proceso = 0;

	private OpenFileDialog dialog = new OpenFileDialog();

	private List<DataRow> lista = new List<DataRow>();

	private DataTable tabla = new DataTable();

	private clsAdmProducto admprod = new clsAdmProducto();

	private clsAdmUnidad admunidad = new clsAdmUnidad();

	private clsUnidadEquivalente equi = new clsUnidadEquivalente();

	private DataTable dt_uniequi = new DataTable();

	private string compra_venta_Descripcion = "";

	private int compra_venta_Cod;

	private DataGridViewTextBoxColumn colum;

	private List<string> ltanombrecolumnas = new List<string>();

	private clsNotaIngreso notaingreso = new clsNotaIngreso();

	private clsDetalleNotaIngreso dtnotaingreso = new clsDetalleNotaIngreso();

	private clsAdmTipoCambio admtipocambio = new clsAdmTipoCambio();

	private clsAdmNotaIngreso admnotaingreso = new clsAdmNotaIngreso();

	private DataTable dt_notaingreso = new DataTable();

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox textBox1;

	private Label label1;

	private Label label12;

	private Label label13;

	private Button button1;

	private GroupBox groupBox2;

	private DataGridView dataGridView1;

	private Label label9;

	private ProgressBar progressBar1;

	private Button BtnProcesar;

	private BackgroundWorker backgroundWorker1;

	private Label label2;

	private Button button2;

	private Button button3;

	public frmImportarExcelN1()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			string hoja = "";
			dialog.Filter = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx;*.lis";
			dialog.Title = "Seleccione el Archivo";
			dialog.FileName = string.Empty;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = dialog.FileName;
				char delimiter = '.';
				string[] substrings = dialog.SafeFileName.Split(delimiter);
				hoja = substrings[0];
				LLenarGrid(textBox1.Text, hoja);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message.ToString());
		}
	}

	private void LLenarGrid(string archivo, string hoja)
	{
		OleDbConnection conexion = null;
		DataSet dataSet = null;
		OleDbDataAdapter dataAdapter = null;
		string consultaHojaExcel = "Select * from [" + hoja + "$]";
		string cadenaConexionArchivoExcel = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + archivo.Trim() + "';Extended Properties=Excel 8.0;";
		if (string.IsNullOrEmpty(hoja))
		{
			MessageBox.Show("No hay una hoja para leer");
			return;
		}
		try
		{
			dataGridView1.Columns.Clear();
			lista.Clear();
			conexion = new OleDbConnection(cadenaConexionArchivoExcel);
			conexion.Open();
			dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion);
			dataSet = new DataSet();
			dataAdapter.Fill(dataSet, hoja.Trim());
			tabla = dataSet.Tables[0];
			dataGridView1.DataSource = tabla;
			if (dataGridView1.Columns.Count == 5)
			{
				crearColumnas();
				movercolumnas();
				ocultarCeldasExcel2();
			}
			if (dataGridView1.Columns.Count == 8)
			{
				crearColumnas();
				moverColumnasUnidadesEquivalentes();
			}
			foreach (DataGridViewRow row in (IEnumerable)dataGridView1.Rows)
			{
				string producto = row.Cells[0].Value.ToString();
				codproducto = admprod.GetCodProducto_xDescripcion(producto);
				if (dataGridView1.Columns.Count == 13)
				{
					Proceso = 1;
					MostrarExcel(row);
					dataGridView1.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
				}
				else
				{
					Proceso = 2;
					completarCeldasExcel2(row);
					dataGridView1.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					dataGridView1.Columns["Precio_Unitario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					dataGridView1.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
				}
			}
			label12.Text = dataGridView1.Rows.Count + " Registros.";
			conexion.Close();
			dataGridView1.AllowUserToAddRows = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error : " + ex.Message.ToString(), "Advertencia");
		}
	}

	public void llenarLista()
	{
		ltanombrecolumnas.Add("codProducto");
		ltanombrecolumnas.Add("codNotaIngreso");
		ltanombrecolumnas.Add("codAlmacen");
		ltanombrecolumnas.Add("serielote");
		ltanombrecolumnas.Add("subtotal");
		ltanombrecolumnas.Add("descuento1");
		ltanombrecolumnas.Add("descuento2");
		ltanombrecolumnas.Add("descuento3");
		ltanombrecolumnas.Add("montodscto");
		ltanombrecolumnas.Add("igv");
		ltanombrecolumnas.Add("flete");
		ltanombrecolumnas.Add("importe");
		ltanombrecolumnas.Add("precioreal");
		ltanombrecolumnas.Add("valoreal");
		ltanombrecolumnas.Add("fechaingreso");
		ltanombrecolumnas.Add("estado");
		ltanombrecolumnas.Add("codUser");
		ltanombrecolumnas.Add("fecharegistro");
		ltanombrecolumnas.Add("anulado");
		ltanombrecolumnas.Add("valorrealsoles");
		ltanombrecolumnas.Add("coddetallerequerimiento");
		ltanombrecolumnas.Add("cantidadpendiente");
		ltanombrecolumnas.Add("bonificacion");
	}

	private void llenarListaUnidadesEquivalentes()
	{
		ltanombrecolumnas.Add("codProducto");
		ltanombrecolumnas.Add("codUser");
		ltanombrecolumnas.Add("fecharegistro");
		ltanombrecolumnas.Add("codAlmacen");
		ltanombrecolumnas.Add("PrecioFinal");
	}

	public void crearColumnas()
	{
		try
		{
			int index = 5;
			ltanombrecolumnas.Clear();
			if (dataGridView1.Columns.Count == 5)
			{
				llenarLista();
			}
			if (dataGridView1.Columns.Count == 8)
			{
				llenarListaUnidadesEquivalentes();
			}
			foreach (string dato in ltanombrecolumnas)
			{
				colum = new DataGridViewTextBoxColumn();
				colum.Name = dato;
				colum.DataPropertyName = dato;
				colum.HeaderText = dato.ToUpper();
				colum.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
				colum.DisplayIndex = index;
				colum.CellTemplate = new DataGridViewTextBoxCell();
				dataGridView1.Columns.Add(colum);
				index++;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message ?? "");
		}
	}

	public void movercolumnas()
	{
		dataGridView1.Columns["codProducto"].DisplayIndex = 1;
		dataGridView1.Columns["codNotaIngreso"].DisplayIndex = 2;
		dataGridView1.Columns["codAlmacen"].DisplayIndex = 3;
		dataGridView1.Columns["Codigo_Moneda"].DisplayIndex = 4;
		dataGridView1.Columns["serielote"].DisplayIndex = 6;
	}

	private void moverColumnasUnidadesEquivalentes()
	{
		dataGridView1.Columns["codProducto"].DisplayIndex = 1;
		dataGridView1.Columns["codUser"].DisplayIndex = 7;
		dataGridView1.Columns["fecharegistro"].DisplayIndex = 8;
		dataGridView1.Columns["codAlmacen"].DisplayIndex = 9;
		dataGridView1.Columns["PrecioFinal"].DisplayIndex = 12;
	}

	private void MostrarExcel(DataGridViewRow row)
	{
		row.Cells["codProducto"].Value = codproducto;
		row.Cells["codUser"].Value = frmLogin.iCodUser;
		row.Cells["fecharegistro"].Value = DateTime.Now;
		row.Cells["codAlmacen"].Value = frmLogin.iCodAlmacen;
		dataGridView1.Columns["codProducto"].Visible = false;
		dataGridView1.Columns["fecharegistro"].Visible = false;
		dataGridView1.Columns["codUser"].Visible = false;
		dataGridView1.Columns["codAlmacen"].Visible = false;
		dataGridView1.Columns["PrecioFinal"].Visible = false;
	}

	public void completarCeldasExcel2(DataGridViewRow row)
	{
		row.Cells["codProducto"].Value = codproducto;
		row.Cells["codNotaIngreso"].Value = 0;
		row.Cells["codAlmacen"].Value = frmLogin.iCodAlmacen;
		row.Cells["serielote"].Value = 0;
		row.Cells["subtotal"].Value = (decimal)Convert.ToInt32(row.Cells["Cantidad"].Value) * Convert.ToDecimal(row.Cells["Precio_Unitario"].Value);
		row.Cells["descuento1"].Value = 0;
		row.Cells["descuento2"].Value = 0;
		row.Cells["descuento3"].Value = 0;
		row.Cells["montodscto"].Value = 0;
		decimal preciosinigv = Convert.ToDecimal(row.Cells["Precio_Unitario"].Value) / Convert.ToDecimal(1.18);
		row.Cells["igv"].Value = Math.Round(Convert.ToDecimal(row.Cells["subtotal"].Value) - preciosinigv * (decimal)Convert.ToInt32(row.Cells["Cantidad"].Value), 3);
		row.Cells["flete"].Value = 0;
		row.Cells["importe"].Value = row.Cells["subtotal"].Value;
		row.Cells["precioreal"].Value = row.Cells["Precio_Unitario"].Value;
		row.Cells["valoreal"].Value = Math.Round(Convert.ToDecimal(row.Cells["precioreal"].Value) / Convert.ToDecimal(1.18), 3);
		row.Cells["fechaingreso"].Value = DateTime.Now;
		row.Cells["estado"].Value = 1;
		row.Cells["codUser"].Value = frmLogin.iCodUser;
		row.Cells["fecharegistro"].Value = DateTime.Now;
		row.Cells["anulado"].Value = 0;
		row.Cells["valorrealsoles"].Value = 0;
		row.Cells["coddetallerequerimiento"].Value = 0;
		row.Cells["cantidadpendiente"].Value = row.Cells["Cantidad"].Value;
		row.Cells["bonificacion"].Value = 0;
	}

	public void ocultarCeldasExcel2()
	{
		dataGridView1.Columns["codNotaIngreso"].Visible = false;
		dataGridView1.Columns["codProducto"].Visible = false;
		dataGridView1.Columns["codAlmacen"].Visible = false;
		dataGridView1.Columns["Codigo_Moneda"].Visible = false;
		dataGridView1.Columns["Unidad_Compra"].Visible = false;
		dataGridView1.Columns["serielote"].Visible = false;
		dataGridView1.Columns["descuento1"].Visible = false;
		dataGridView1.Columns["descuento2"].Visible = false;
		dataGridView1.Columns["descuento3"].Visible = false;
		dataGridView1.Columns["montodscto"].Visible = false;
		dataGridView1.Columns["fecharegistro"].Visible = false;
		dataGridView1.Columns["fechaingreso"].Visible = false;
		dataGridView1.Columns["codUser"].Visible = false;
		dataGridView1.Columns["estado"].Visible = false;
		dataGridView1.Columns["anulado"].Visible = false;
		dataGridView1.Columns["valorrealsoles"].Visible = false;
		dataGridView1.Columns["coddetallerequerimiento"].Visible = false;
		dataGridView1.Columns["bonificacion"].Visible = false;
	}

	private void BtnProcesar_Click(object sender, EventArgs e)
	{
		if (BtnProcesar.Text == "Procesar")
		{
			if (Proceso == 1)
			{
				GuardarUnidades();
			}
			if (Proceso == 2)
			{
				GuardarInventarioInicial();
			}
		}
		else
		{
			resume = 0;
			backgroundWorker1.RunWorkerAsync();
			progressBar1.Maximum = resume;
			contador = resume;
		}
	}

	private void GuardarUnidades()
	{
		int contadorErrores = 0;
		foreach (DataGridViewRow row in (IEnumerable)dataGridView1.Rows)
		{
			bool bandera = false;
			if (admprod.ValidaCodigoProducto(Convert.ToInt32(row.Cells["codProducto"].Value)) != 0 && admprod.ValidaCodigoUE(row.Cells["codUnidadMedida"].Value.ToString()) != 0 && admprod.ValidaCodigoMoneda(row.Cells["codMoneda"].Value.ToString()) != 0 && admprod.ValidaTipoPrecio(row.Cells["codTipo"].Value.ToString()) != 0)
			{
				compra_venta_Descripcion = row.Cells["compra_venta"].Value.ToString();
				if (compra_venta_Descripcion == "COMPRA")
				{
					compra_venta_Cod = 0;
				}
				else if (compra_venta_Descripcion == "VENTA")
				{
					compra_venta_Cod = 1;
				}
				else
				{
					compra_venta_Cod = 2;
				}
				equi.CodProducto = Convert.ToInt32(row.Cells["codProducto"].Value);
				equi.CodUnidad = admprod.GetCodUnidad(row.Cells["codUnidadMedida"].Value.ToString());
				if (compra_venta_Cod != 2)
				{
					equi.Factor = 0m;
					equi.CodEquivalente = 0;
				}
				else
				{
					equi.Factor = Convert.ToInt32(row.Cells["factor"].Value);
					equi.CodEquivalente = admprod.GetCodUnidad(row.Cells["codUndEqui"].Value.ToString());
				}
				equi.Tipo = admprod.GetCodTipoPrecio(row.Cells["codTipo"].Value.ToString());
				equi.Precio = Convert.ToDecimal(row.Cells["Precio"].Value);
				equi.CodUser = Convert.ToInt32(row.Cells["codUser"].Value);
				equi.FechaRegistro = Convert.ToDateTime(row.Cells["fecharegistro"].Value);
				equi.CodAlmacen = Convert.ToInt32(row.Cells["codAlmacen"].Value);
				equi.CompraVenta = compra_venta_Cod;
				equi.ICodMoneda = admprod.GetCodMoneda(row.Cells["codMoneda"].Value.ToString());
				dt_uniequi = admunidad.MuestraUnidadesEquivalentes();
				foreach (DataRow rows in dt_uniequi.Rows)
				{
					int codigoundequi = Convert.ToInt32(rows.ItemArray[0]);
					if (equi.CodProducto == Convert.ToInt32(rows.ItemArray[1]) && equi.CodUnidad == Convert.ToInt32(rows.ItemArray[2]) && equi.Factor == (decimal)Convert.ToInt32(rows.ItemArray[3]) && equi.CodEquivalente == Convert.ToInt32(rows.ItemArray[4]) && equi.Tipo == Convert.ToInt32(rows.ItemArray[5]) && equi.CodUser == Convert.ToInt32(rows.ItemArray[7]) && equi.CodAlmacen == Convert.ToInt32(rows.ItemArray[9]) && equi.CompraVenta == Convert.ToInt32(rows.ItemArray[10]))
					{
						if (Convert.ToInt32(rows.ItemArray[10]) != 2 && Convert.ToDecimal(rows.ItemArray[6]) != equi.Precio)
						{
							admprod.deleteunidadequivalente(codigoundequi);
						}
						else
						{
							bandera = true;
						}
						row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
						break;
					}
				}
				if (bandera)
				{
					continue;
				}
				if (admprod.insertunidadequivalente(equi, 0))
				{
					contador++;
					bool resultado = admunidad.ActualizaPrecioEnDolares();
					int resultado2 = admunidad.CantidadProductosDolares();
					if (!resultado && resultado2 > 0)
					{
						MessageBox.Show("Error");
					}
				}
				else
				{
					MessageBox.Show("Error al agregar datos", "Gestion de Importacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				contadorErrores++;
			}
		}
		if (contadorErrores > 0)
		{
			MessageBox.Show("Datos erroneos", "Gestion de Importacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		progressBar1.Maximum = contador;
		backgroundWorker1.RunWorkerAsync();
	}

	private void GuardarInventarioInicial()
	{
		int correlativo = 1;
		foreach (DataGridViewRow row in (IEnumerable)dataGridView1.Rows)
		{
			string producto = row.Cells["Producto"].Value.ToString();
			codproducto = admprod.GetCodProducto_xDescripcion(producto);
			if (admprod.ValidaUnidadEquivalente(codproducto) <= 0)
			{
				continue;
			}
			notaingreso.CodAlmacen = frmLogin.iCodAlmacen;
			notaingreso.CodTipoTransaccion = 4;
			notaingreso.CodTipoDocumento = 10;
			notaingreso.NumDoc = $"{correlativo:00000}";
			notaingreso.CodProveedor = 0;
			notaingreso.Moneda = admprod.GetCodMoneda(row.Cells["Codigo_Moneda"].Value.ToString());
			clsTipoCambio tipocambio = new clsTipoCambio();
			tipocambio = admtipocambio.CargaTipoCambio(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), 2);
			notaingreso.TipoCambio = tipocambio.Venta;
			notaingreso.FechaIngreso = DateTime.Now;
			notaingreso.Comentario = "INVENTARIO INICIAL";
			notaingreso.MontoBruto = Convert.ToDouble(row.Cells["subtotal"].Value);
			notaingreso.MontoDscto = Convert.ToDouble(row.Cells["montodscto"].Value);
			notaingreso.Igv = Convert.ToDouble(row.Cells["igv"].Value);
			notaingreso.Flete = Convert.ToDouble(row.Cells["flete"].Value);
			notaingreso.Total = Convert.ToDouble(row.Cells["subtotal"].Value);
			notaingreso.Abonado = 0.0;
			notaingreso.Pendiente = Convert.ToDouble(row.Cells["subtotal"].Value);
			notaingreso.Estado = 1;
			notaingreso.Recibido = 0;
			notaingreso.FormaPago = 0;
			notaingreso.FechaPago = DateTime.Now;
			notaingreso.Cancelado = 0;
			notaingreso.CodUser = frmLogin.iCodUser;
			notaingreso.FechaRegistro = DateTime.Now;
			notaingreso.CodSerie = 0;
			notaingreso.Serie = "";
			notaingreso.CodReferencia = 0;
			notaingreso.CodOrdenCompra = 0;
			notaingreso.Aceptado = 0;
			notaingreso.codalmacenemisor = 0;
			notaingreso.Aplicada = 0;
			notaingreso.CodAplicada = 0;
			notaingreso.Motivo = "";
			correlativo++;
			bool bandera2 = false;
			dt_notaingreso = admnotaingreso.ListarCodigoNotasSalida();
			foreach (DataRow x in dt_notaingreso.Rows)
			{
				if (Convert.ToInt32(row.Cells["codNotaIngreso"].Value) == Convert.ToInt32(x["codNotaIngreso"]))
				{
					MessageBox.Show("Esta nota de salida ya existe!", "Gestion de Importacion de Datos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					bandera2 = true;
					Proceso = 3;
				}
			}
			if (bandera2)
			{
				continue;
			}
			if (admnotaingreso.insert(notaingreso))
			{
				dtnotaingreso.CodProducto = codproducto;
				dtnotaingreso.CodNotaIngreso = Convert.ToInt32(notaingreso.CodNotaIngreso);
				dtnotaingreso.CodAlmacen = frmLogin.iCodAlmacen;
				dtnotaingreso.UnidadIngresada = admprod.GetCodUnidad(row.Cells["Unidad_Compra"].Value.ToString());
				dtnotaingreso.SerieLote = row.Cells["serielote"].Value.ToString();
				dtnotaingreso.Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value);
				dtnotaingreso.PrecioUnitario = Convert.ToDouble(row.Cells["Precio_Unitario"].Value);
				dtnotaingreso.Subtotal = Convert.ToDouble(row.Cells["subtotal"].Value);
				dtnotaingreso.Descuento1 = Convert.ToDouble(row.Cells["descuento1"].Value);
				dtnotaingreso.Descuento2 = Convert.ToDouble(row.Cells["descuento2"].Value);
				dtnotaingreso.Descuento3 = Convert.ToDouble(row.Cells["descuento3"].Value);
				dtnotaingreso.MontoDescuento = Convert.ToDouble(row.Cells["montodscto"].Value);
				dtnotaingreso.Igv = Convert.ToDouble(row.Cells["igv"].Value);
				dtnotaingreso.Flete = Convert.ToDouble(row.Cells["flete"].Value);
				dtnotaingreso.Importe = Convert.ToDouble(row.Cells["importe"].Value);
				dtnotaingreso.PrecioReal = Convert.ToDouble(row.Cells["precioreal"].Value);
				dtnotaingreso.ValoReal = Convert.ToDouble(row.Cells["valoreal"].Value);
				dtnotaingreso.FechaIngreso = Convert.ToDateTime(row.Cells["fechaingreso"].Value);
				dtnotaingreso.CodUser = Convert.ToInt32(row.Cells["codUser"].Value);
				dtnotaingreso.ValorrealSoles = Convert.ToDouble(row.Cells["valorrealsoles"].Value);
				dtnotaingreso.CodDetalleRequerimiento = Convert.ToInt32(row.Cells["coddetallerequerimiento"].Value);
				if (Convert.ToInt32(row.Cells["bonificacion"].Value) == 0)
				{
					dtnotaingreso.Bonificacion = true;
				}
				else
				{
					dtnotaingreso.Bonificacion = false;
				}
				if (!admnotaingreso.insertdetalle(dtnotaingreso))
				{
					MessageBox.Show("Error al agregar el detalle de la nota de salida", "Gestion de Importacion de Datos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					contador++;
				}
			}
			else
			{
				MessageBox.Show("Error al agregar la nota de salida", "Gestion de Importacion de Datos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		progressBar1.Maximum = contador;
		backgroundWorker1.RunWorkerAsync();
	}

	private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		if (e.Cancelled)
		{
			MessageBox.Show("Cancelado : " + i, "Importacion de datos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("Comparados : " + i + " Registros.", "Importacion de datos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		progressBar1.Value = e.ProgressPercentage;
		label2.Text = e.ProgressPercentage + " Registros.";
	}

	private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
		if (contador != 0)
		{
			for (int cont = 0; cont < contador; cont++)
			{
				if (BtnProcesar.Text == "Procesar")
				{
					i++;
				}
				else
				{
					i = resume;
					i++;
				}
				if (backgroundWorker1.CancellationPending)
				{
					e.Cancel = true;
					continue;
				}
				Thread.Sleep(100);
				backgroundWorker1.ReportProgress(i);
			}
		}
		else
		{
			MessageBox.Show("Datos repetidos...! Revisar formulario", "Gestion de Importacion de Datos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		backgroundWorker1.CancelAsync();
		if (contador > 0)
		{
			BtnProcesar.Text = "Continuar";
		}
	}

	private void button3_Click(object sender, EventArgs e)
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.BtnProcesar = new System.Windows.Forms.Button();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		this.button3 = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.button2);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.progressBar1);
		this.groupBox1.Controls.Add(this.BtnProcesar);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1032, 117);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Importar";
		this.button2.Location = new System.Drawing.Point(889, 25);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(137, 23);
		this.button2.TabIndex = 21;
		this.button2.Text = "Cancelar";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(820, 56);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(16, 13);
		this.label2.TabIndex = 20;
		this.label2.Text = "...";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(741, 56);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(73, 13);
		this.label9.TabIndex = 19;
		this.label9.Text = "Encontrados :";
		this.progressBar1.Location = new System.Drawing.Point(744, 78);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.RightToLeftLayout = true;
		this.progressBar1.Size = new System.Drawing.Size(282, 23);
		this.progressBar1.TabIndex = 18;
		this.BtnProcesar.Location = new System.Drawing.Point(744, 25);
		this.BtnProcesar.Name = "BtnProcesar";
		this.BtnProcesar.Size = new System.Drawing.Size(137, 23);
		this.BtnProcesar.TabIndex = 17;
		this.BtnProcesar.Text = "Procesar";
		this.BtnProcesar.UseVisualStyleBackColor = true;
		this.BtnProcesar.Click += new System.EventHandler(BtnProcesar_Click);
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(587, 78);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(19, 15);
		this.label12.TabIndex = 16;
		this.label12.Text = "...";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(466, 78);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(95, 15);
		this.label13.TabIndex = 15;
		this.label13.Text = "Encontrados :";
		this.button1.Location = new System.Drawing.Point(469, 25);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(137, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "Importar .xls";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.textBox1.Location = new System.Drawing.Point(75, 27);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.textBox1.Size = new System.Drawing.Size(372, 66);
		this.textBox1.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(44, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(36, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Ruta: ";
		this.groupBox2.Controls.Add(this.dataGridView1);
		this.groupBox2.Location = new System.Drawing.Point(12, 135);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1032, 298);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Datos";
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 16);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
		this.dataGridView1.Size = new System.Drawing.Size(1026, 279);
		this.dataGridView1.TabIndex = 0;
		this.backgroundWorker1.WorkerReportsProgress = true;
		this.backgroundWorker1.WorkerSupportsCancellation = true;
		this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
		this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
		this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
		this.button3.Location = new System.Drawing.Point(942, 444);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(99, 31);
		this.button3.TabIndex = 2;
		this.button3.Text = "SALIR";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1061, 487);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmImportarExcelN1";
		this.Text = "Importar  Excel 1";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
	}
}
