using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SpreadsheetLight;

namespace SIGEFA.Formularios;

public class frmImportaData : Form
{
	internal int CodPlantilla = 0;

	internal string TituloPlantilla = "";

	private clsAdmProducto admProd = new clsAdmProducto();

	private clsAdmPlantillaDeProductos admPlanProd = new clsAdmPlantillaDeProductos();

	internal int Proceso = 1;

	private IContainer components = null;

	private Label lblMensaje;

	private Button btnDescargaPlantilla;

	private Button btnImportarData;

	public frmImportaData()
	{
		InitializeComponent();
	}

	private void frmImportaData_Load(object sender, EventArgs e)
	{
		switch (Proceso)
		{
		case 1:
			lblMensaje.Text = "Esta interfaz permite importar data con la finalidad de modificar los campos de stock maximo, stock minimo y cantidad por paquete para los productos dentro de la plantilla de Orden de Compra: " + TituloPlantilla;
			break;
		case 2:
			lblMensaje.Text = "Esta interfaz permite importar data con la finalidad de modificar y asignar productos asociados." + TituloPlantilla;
			break;
		case 3:
			lblMensaje.Text = "Esta interfaz permite importar data con la finalidad de actualizar datos de los  productos ." + TituloPlantilla;
			break;
		case 4:
			lblMensaje.Text = "Esta interfaz permite importar data con la finalidad de actualizar el stock pendiente de los productos ." + TituloPlantilla;
			break;
		default:
			lblMensaje.Text = "No se ah definido una descripcion para el proceso: " + Proceso;
			break;
		}
	}

	private void btnDescargaPlantilla_Click(object sender, EventArgs e)
	{
		SLDocument sl = new SLDocument();
		switch (Proceso)
		{
		case 1:
			descargaPlantilladeProductosAgrupados(sl);
			break;
		case 2:
			descargaPlantilladeProductosAsociados(sl);
			break;
		case 3:
			descargaPlantilladeProductos(sl);
			break;
		case 4:
			descargaPlantilladeActualizaStock(sl);
			break;
		default:
			MessageBox.Show("Error no definida plantilla a descargar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			break;
		}
		try
		{
			string cadenaGuardado = obtenerRutaParaGuardar();
			if (cadenaGuardado != null)
			{
				sl.SaveAs(cadenaGuardado);
				Process.Start("explorer.exe", cadenaGuardado);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, base.Name);
		}
	}

	private void descargaPlantilladeProductosAsociados(SLDocument sl)
	{
		int ctdad = admProd.getCantidadMaximaAsociadosXProducto();
		int indFilaInicial = 1;
		sl.SetCellValue(indFilaInicial, 1, "Plantilla de Productos Asociados");
		sl.MergeWorksheetCells("A1", "E1");
		SLStyle estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 115, 55), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 5, estilo);
		indFilaInicial++;
		sl.SetCellValue(indFilaInicial, 1, "Cod Referencia");
		sl.SetColumnWidth(1, 21.0);
		if (ctdad == 0)
		{
			sl.SetCellValue(indFilaInicial, 2, "Cod Item Alternativo 1");
			sl.SetCellValue(indFilaInicial, 3, "Cod Item Alternativo 2");
			sl.SetCellValue(indFilaInicial, 4, "Cod Item Alternativo 3");
			sl.SetCellValue(indFilaInicial, 5, "Cod Item Alternativo 4");
			sl.SetColumnWidth(2, 21.0);
			sl.SetColumnWidth(3, 21.0);
			sl.SetColumnWidth(4, 21.0);
			sl.SetColumnWidth(5, 21.0);
		}
		else
		{
			for (int i = 0; i < ctdad; i++)
			{
				sl.SetCellValue(indFilaInicial, i + 2, "Cod Item Alternativo " + (i + 1));
				sl.SetColumnWidth(i + 2, 21.0);
			}
		}
		estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 255), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, (ctdad == 0) ? 5 : (ctdad + 1), estilo);
		rellenarFilasProductosAsociados(sl, indFilaInicial);
	}

	private void rellenarFilasProductosAsociados(SLDocument sl, int indFilaInicial)
	{
		DataTable aux = admProd.cargaProductosAsociados();
		if (aux == null)
		{
			return;
		}
		foreach (DataRow fila in aux.Rows)
		{
			indFilaInicial++;
			int indColumna = 1;
			sl.SetCellValue(indFilaInicial, indColumna, fila.Field<object>("producto").ToString());
			for (int i = 1; i < aux.Columns.Count; i++)
			{
				indColumna++;
				object valor = fila.Field<object>("prod_asociado_" + (indColumna - 1));
				valor = ((valor == DBNull.Value || valor == null) ? "" : valor);
				sl.SetCellValue(indFilaInicial, indColumna, valor.ToString());
			}
		}
	}

	private void descargaPlantilladeProductos(SLDocument sl)
	{
		int indFilaInicial = 1;
		sl.SetCellValue(indFilaInicial, 1, "Plantilla de Productos");
		sl.MergeWorksheetCells("A1", "L1");
		SLStyle estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 115, 55), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, estilo);
		indFilaInicial++;
		sl.SetCellValue(indFilaInicial, 1, "Cod Referencia");
		sl.SetColumnWidth(1, 21.0);
		sl.SetCellValue(indFilaInicial, 2, "Descripción");
		sl.SetColumnWidth(2, 80.0);
		sl.SetCellValue(indFilaInicial, 3, "Stock Minimo");
		sl.SetColumnWidth(3, 21.0);
		sl.SetCellValue(indFilaInicial, 4, "Stock Maximo");
		sl.SetColumnWidth(4, 21.0);
		sl.SetCellValue(indFilaInicial, 5, "Descontinuado");
		sl.SetColumnWidth(5, 21.0);
		sl.SetCellValue(indFilaInicial, 6, "situacion1");
		sl.SetColumnWidth(6, 21.0);
		sl.SetCellValue(indFilaInicial, 7, "situacion2");
		sl.SetColumnWidth(7, 21.0);
		sl.SetCellValue(indFilaInicial, 8, "situacion3");
		sl.SetColumnWidth(8, 21.0);
		sl.SetCellValue(indFilaInicial, 9, "categorizacion1");
		sl.SetColumnWidth(9, 21.0);
		sl.SetCellValue(indFilaInicial, 10, "categorizacion2");
		sl.SetColumnWidth(10, 21.0);
		sl.SetCellValue(indFilaInicial, 11, "categorizacion3");
		sl.SetColumnWidth(11, 21.0);
		sl.SetCellValue(indFilaInicial, 12, "categorizacion4");
		sl.SetColumnWidth(12, 21.0);
		estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 255), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 12, estilo);
		rellenarFilasProductos(sl, indFilaInicial);
	}

	private void rellenarFilasProductos(SLDocument sl, int indFilaInicial)
	{
		try
		{
			DataTable aux = admProd.cargaProductos();
			if (aux == null)
			{
				return;
			}
			foreach (DataRow fila in aux.Rows)
			{
				indFilaInicial++;
				SLStyle style = sl.CreateStyle();
				style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
				style.SetVerticalAlignment(VerticalAlignmentValues.Center);
				sl.SetCellValue("A" + indFilaInicial, (fila[0] != DBNull.Value) ? Convert.ToInt32(fila[0]) : Convert.ToInt32(0));
				sl.SetCellStyle("A" + indFilaInicial, style);
				style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
				style.SetVerticalAlignment(VerticalAlignmentValues.Center);
				sl.SetCellValue("B" + indFilaInicial, fila[1].ToString());
				sl.SetCellStyle("B" + indFilaInicial, style);
				style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
				style.SetVerticalAlignment(VerticalAlignmentValues.Center);
				sl.SetCellValue("C" + indFilaInicial, (fila[2] != DBNull.Value) ? Convert.ToDouble(fila[2]) : Convert.ToDouble(0.0));
				sl.SetCellStyle("C" + indFilaInicial, style);
				sl.SetCellValue("D" + indFilaInicial, (fila[3] != DBNull.Value) ? Convert.ToDouble(fila[3]) : Convert.ToDouble(0.0));
				sl.SetCellStyle("D" + indFilaInicial, style);
				sl.SetCellValue("E" + indFilaInicial, fila[4].ToString());
				sl.SetCellStyle("E" + indFilaInicial, style);
				sl.SetCellValue("F" + indFilaInicial, (fila[5] != DBNull.Value) ? Convert.ToDouble(fila[5]) : Convert.ToDouble(0.0));
				sl.SetCellStyle("F" + indFilaInicial, style);
				sl.SetCellValue("G" + indFilaInicial, (fila[6] != DBNull.Value) ? Convert.ToDouble(fila[6]) : Convert.ToDouble(0.0));
				sl.SetCellStyle("G" + indFilaInicial, style);
				sl.SetCellValue("H" + indFilaInicial, (fila[7] != DBNull.Value) ? Convert.ToDouble(fila[7]) : Convert.ToDouble(0.0));
				sl.SetCellStyle("H" + indFilaInicial, style);
				sl.SetCellValue("I" + indFilaInicial, (fila[8] != DBNull.Value) ? Convert.ToDecimal(fila[8]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("I" + indFilaInicial, style);
				sl.SetCellValue("J" + indFilaInicial, (fila[9] != DBNull.Value) ? Convert.ToDecimal(fila[9]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("J" + indFilaInicial, style);
				sl.SetCellValue("K" + indFilaInicial, (fila[10] != DBNull.Value) ? Convert.ToDecimal(fila[10]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("K" + indFilaInicial, style);
				sl.SetCellValue("L" + indFilaInicial, (fila[11] != DBNull.Value) ? Convert.ToDecimal(fila[11]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("L" + indFilaInicial, style);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void descargaPlantilladeActualizaStock(SLDocument sl)
	{
		int indFilaInicial = 1;
		sl.SetCellValue(indFilaInicial, 1, "Plantilla de stock Productos");
		sl.MergeWorksheetCells("A1", "N1");
		SLStyle estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 115, 55), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, estilo);
		indFilaInicial++;
		sl.SetCellValue(indFilaInicial, 1, "REFERENCIA");
		sl.SetColumnWidth(1, 21.0);
		sl.SetCellValue(indFilaInicial, 2, "DESCRIPCIÓN");
		sl.SetColumnWidth(2, 80.0);
		sl.SetCellValue(indFilaInicial, 3, "DISPONIBLE(D36-FR)");
		sl.SetColumnWidth(3, 21.0);
		sl.SetCellValue(indFilaInicial, 4, "KARDEX(D36-FR)");
		sl.SetColumnWidth(4, 21.0);
		sl.SetCellValue(indFilaInicial, 5, "SEPARADO(D36-FR)");
		sl.SetColumnWidth(5, 21.0);
		sl.SetCellValue(indFilaInicial, 6, "DISPONIBLE(G17-FR)");
		sl.SetColumnWidth(6, 21.0);
		sl.SetCellValue(indFilaInicial, 7, "KARDEX(G17-FR)");
		sl.SetColumnWidth(7, 21.0);
		sl.SetCellValue(indFilaInicial, 8, "SEPARADO(G17-FR)");
		sl.SetColumnWidth(8, 21.0);
		sl.SetCellValue(indFilaInicial, 9, "DISPONIBLE(G17-LM)");
		sl.SetColumnWidth(9, 21.0);
		sl.SetCellValue(indFilaInicial, 10, "KARDEX(G17-LM)");
		sl.SetColumnWidth(10, 21.0);
		sl.SetCellValue(indFilaInicial, 11, "SEPARADO(G17-LM)");
		sl.SetColumnWidth(11, 21.0);
		sl.SetCellValue(indFilaInicial, 12, "DISPONIBLE(D36-LM)");
		sl.SetColumnWidth(12, 21.0);
		sl.SetCellValue(indFilaInicial, 13, "KARDEX(D36-LM)");
		sl.SetColumnWidth(13, 21.0);
		sl.SetCellValue(indFilaInicial, 14, "SEPARADO(D36-LM)");
		sl.SetColumnWidth(14, 21.0);
		estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 255), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 14, estilo);
		rellenarFilasProductosStock(sl, indFilaInicial);
	}

	private void rellenarFilasProductosStock(SLDocument sl, int indFilaInicial)
	{
		try
		{
			DataTable aux = admProd.cargaStockProductos();
			if (aux == null)
			{
				return;
			}
			foreach (DataRow fila in aux.Rows)
			{
				indFilaInicial++;
				SLStyle style = sl.CreateStyle();
				style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
				style.SetVerticalAlignment(VerticalAlignmentValues.Center);
				sl.SetCellValue("A" + indFilaInicial, (fila[0] != DBNull.Value) ? Convert.ToInt32(fila[0]) : Convert.ToInt32(0));
				sl.SetCellStyle("A" + indFilaInicial, style);
				style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
				style.SetVerticalAlignment(VerticalAlignmentValues.Center);
				sl.SetCellValue("B" + indFilaInicial, fila[1].ToString());
				sl.SetCellStyle("B" + indFilaInicial, style);
				style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
				style.SetVerticalAlignment(VerticalAlignmentValues.Center);
				sl.SetCellValue("C" + indFilaInicial, (fila[2] != DBNull.Value) ? Convert.ToDecimal(fila[2]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("C" + indFilaInicial, style);
				sl.SetCellValue("D" + indFilaInicial, (fila[3] != DBNull.Value) ? Convert.ToDecimal(fila[3]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("D" + indFilaInicial, style);
				sl.SetCellValue("E" + indFilaInicial, (fila[4] != DBNull.Value) ? Convert.ToDecimal(fila[4]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("E" + indFilaInicial, style);
				sl.SetCellValue("F" + indFilaInicial, (fila[5] != DBNull.Value) ? Convert.ToDecimal(fila[5]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("F" + indFilaInicial, style);
				sl.SetCellValue("G" + indFilaInicial, (fila[6] != DBNull.Value) ? Convert.ToDecimal(fila[6]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("G" + indFilaInicial, style);
				sl.SetCellValue("H" + indFilaInicial, (fila[7] != DBNull.Value) ? Convert.ToDecimal(fila[7]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("H" + indFilaInicial, style);
				sl.SetCellValue("I" + indFilaInicial, (fila[8] != DBNull.Value) ? Convert.ToDecimal(fila[8]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("I" + indFilaInicial, style);
				sl.SetCellValue("J" + indFilaInicial, (fila[9] != DBNull.Value) ? Convert.ToDecimal(fila[9]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("J" + indFilaInicial, style);
				sl.SetCellValue("K" + indFilaInicial, (fila[10] != DBNull.Value) ? Convert.ToDecimal(fila[10]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("K" + indFilaInicial, style);
				sl.SetCellValue("L" + indFilaInicial, (fila[11] != DBNull.Value) ? Convert.ToDecimal(fila[11]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("L" + indFilaInicial, style);
				sl.SetCellValue("M" + indFilaInicial, (fila[12] != DBNull.Value) ? Convert.ToDecimal(fila[12]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("M" + indFilaInicial, style);
				sl.SetCellValue("N" + indFilaInicial, (fila[13] != DBNull.Value) ? Convert.ToDecimal(fila[13]) : Convert.ToDecimal(0.0));
				sl.SetCellStyle("N" + indFilaInicial, style);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void descargaPlantilladeProductosAgrupados(SLDocument sl)
	{
		int indFilaInicial = 1;
		sl.SetCellValue(indFilaInicial, 1, "Plantilla: " + TituloPlantilla);
		sl.MergeWorksheetCells("A1", "G1");
		SLStyle estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 115, 55), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 7, estilo);
		indFilaInicial++;
		sl.SetCellValue(indFilaInicial, 1, "CodDetalle");
		sl.SetCellValue(indFilaInicial, 2, "Referencia");
		sl.SetCellValue(indFilaInicial, 3, "Descripcion");
		sl.SetCellValue(indFilaInicial, 4, "Unidad");
		sl.SetCellValue(indFilaInicial, 5, "Stock Minimo");
		sl.SetCellValue(indFilaInicial, 6, "Stock Maximo");
		sl.SetCellValue(indFilaInicial, 7, "Cantidad Por Paquete");
		sl.SetColumnWidth(1, 0.1);
		sl.SetColumnWidth(2, 15.0);
		sl.SetColumnWidth(3, 42.0);
		sl.SetColumnWidth(4, 15.0);
		sl.SetColumnWidth(5, 13.0);
		sl.SetColumnWidth(6, 13.0);
		sl.SetColumnWidth(7, 20.0);
		estilo = sl.CreateStyle();
		estilo.SetFontColor(System.Drawing.Color.White);
		estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 255), System.Drawing.Color.Blue);
		sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 7, estilo);
		rellenarFilasDetallePlantilla(sl, indFilaInicial);
	}

	private void rellenarFilasDetallePlantilla(SLDocument sl, int indFilaInicial)
	{
		DataTable aux = admPlanProd.cargadetalleproductosagrupados(CodPlantilla);
		foreach (DataRow fila in aux.Rows)
		{
			indFilaInicial++;
			sl.SetCellValue(indFilaInicial, 1, fila.Field<object>("codigoDetallePlantilla").ToString());
			sl.SetCellValue(indFilaInicial, 2, fila.Field<object>("referencias").ToString());
			sl.SetCellValue(indFilaInicial, 3, fila.Field<object>("descripcionn").ToString());
			sl.SetCellValue(indFilaInicial, 4, fila.Field<object>("unidadd").ToString());
			object valor = fila.Field<object>("stockminimo");
			valor = ((valor == null) ? "" : valor.ToString());
			sl.SetCellValue(indFilaInicial, 5, valor.ToString());
			valor = fila.Field<object>("stockmaximo");
			valor = ((valor == null) ? "" : valor.ToString());
			sl.SetCellValue(indFilaInicial, 6, valor.ToString());
			valor = fila.Field<object>("cantidad");
			valor = ((valor == null) ? "" : valor.ToString());
			sl.SetCellValue(indFilaInicial, 7, valor.ToString());
		}
	}

	private void btnImportarData_Click(object sender, EventArgs e)
	{
		int iRow = 3;
		string contenidoError = "";
		try
		{
			string path = obtenerRutaParaImportar();
			if (path == "")
			{
				MessageBox.Show("No se ah seleccionado ninguna ruta", base.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			Cursor = Cursors.WaitCursor;
			SLDocument sl = new SLDocument(path);
			switch (Proceso)
			{
			case 1:
				uploadDataProductosAgrupados(sl, ref iRow, ref contenidoError);
				break;
			case 2:
				uploadDataProductosAsociados(sl, ref iRow, ref contenidoError);
				break;
			case 3:
				uploadDataProductos(sl, ref iRow, ref contenidoError);
				break;
			case 4:
				uploadDataStockProductos(sl, ref iRow, ref contenidoError);
				break;
			default:
				MessageBox.Show("Error no definido proceso de subidad de datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			}
			Cursor = Cursors.Default;
			MessageBox.Show("Se actualizo la data con exito de " + (iRow - 3) + " filas", "Actualizacion de Data Importada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			if (contenidoError != "")
			{
				MessageBox.Show(contenidoError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			contenidoError = ((contenidoError == "") ? "" : ("\n" + contenidoError));
			string contenido = ((iRow == 2) ? "" : ("\nSolo se trabajo hasta la fila: " + (iRow - 2)));
			MessageBox.Show(ex.Message + contenido + contenidoError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void uploadDataProductosAsociados(SLDocument sl, ref int iRow, ref string contenidoError)
	{
		while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
		{
			string refProducto = sl.GetCellValueAsString(iRow, 1);
			if (int.TryParse(refProducto, out var codRefProd))
			{
				for (int icol = 2; !string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, icol)); icol++)
				{
					string refAsociado = sl.GetCellValueAsString(iRow, icol);
					if (int.TryParse(refAsociado, out var codRefAsociado))
					{
						if (icol == 2)
						{
							admProd.deleteAsociadosDeProducto(codRefProd, 0);
						}
						if (!admProd.insertaProductoAsociado(codRefProd, codRefAsociado))
						{
							contenidoError = contenidoError + "> Ocurrio un error al configurar datos de producto: " + refProducto + " de la fila excel: " + iRow + "\n";
						}
					}
					else
					{
						contenidoError = contenidoError + "> Codigo Asociado No Encontrado para Producto: " + refProducto + ", Ascociado: " + refAsociado + " en la fila excel: " + iRow + "\n";
					}
				}
			}
			else
			{
				contenidoError = contenidoError + "> Codigo Producto No Encontrado: " + refProducto + " de la fila excel: " + iRow + "\n";
			}
			iRow++;
		}
	}

	private void uploadDataProductosAgrupados(SLDocument sl, ref int iRow, ref string contenidoError)
	{
		while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
		{
			string codDetalle = sl.GetCellValueAsString(iRow, 1);
			string codProducto = sl.GetCellValueAsString(iRow, 2);
			if (int.TryParse(codDetalle, out var codDetalleProd))
			{
				string stockMinimo = sl.GetCellValueAsString(iRow, 5);
				string stockMaximo = sl.GetCellValueAsString(iRow, 6);
				string CtdadXPaquete = sl.GetCellValueAsString(iRow, 7);
				clsDetallePlantillaDeProductos aux = new clsDetallePlantillaDeProductos();
				aux.Codigo = codDetalleProd;
				aux.StockMaximo = ((stockMaximo == "") ? (-1) : Convert.ToInt32(stockMaximo));
				aux.StockMinimo = ((stockMinimo == "") ? (-1) : Convert.ToInt32(stockMinimo));
				aux.Cantidad = ((CtdadXPaquete == "") ? (-1) : Convert.ToInt32(CtdadXPaquete));
				if (!admPlanProd.actualizaStockdeDetallePlantilla(aux))
				{
					contenidoError = contenidoError + "> Ocurrio un error al configurar datos de producto: " + codProducto + " de la fila excel: " + iRow + "\n";
				}
			}
			else
			{
				contenidoError = contenidoError + "> Codigo Producto No Encontrado: " + codProducto + " de la fila excel: " + iRow + "\n";
			}
			iRow++;
		}
	}

	private void uploadDataProductos(SLDocument sl, ref int iRow, ref string contenidoError)
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		DataSet ds = new DataSet();
		ds = dBAccess.ExecuteDataSet("LimpiarSituaciones");
		while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
		{
			string codProducto = sl.GetCellValueAsString(iRow, 1);
			if (int.TryParse(codProducto, out var codpro))
			{
				string stockMinimo = sl.GetCellValueAsString(iRow, 3);
				string stockMaximo = sl.GetCellValueAsString(iRow, 4);
				string descontinuado = sl.GetCellValueAsString(iRow, 5);
				clsProducto producto = new clsProducto();
				producto.CodProducto = codpro;
				producto.StockMinimo = ((stockMinimo == "") ? 0.0 : Convert.ToDouble(stockMinimo));
				producto.StockMaximo = ((stockMaximo == "") ? 0.0 : Convert.ToDouble(stockMaximo));
				producto.descontinuado = ((descontinuado == "SI") ? Convert.ToBoolean(1) : Convert.ToBoolean(0));
				string hast1 = sl.GetCellValueAsString(iRow, 6);
				string hast2 = sl.GetCellValueAsString(iRow, 7);
				string hast3 = sl.GetCellValueAsString(iRow, 8);
				string cat1 = sl.GetCellValueAsString(iRow, 9);
				string cat2 = sl.GetCellValueAsString(iRow, 10);
				string cat3 = sl.GetCellValueAsString(iRow, 11);
				string cat4 = sl.GetCellValueAsString(iRow, 12);
				if (!admPlanProd.actualizadatosproducto(producto))
				{
					contenidoError = contenidoError + "> Ocurrio un error al configurar datos de producto: " + codProducto + " de la fila excel: " + iRow + "\n";
				}
				for (int o = 1; o <= 3; o++)
				{
					switch (o)
					{
					case 1:
						if (hast1 != "")
						{
							admProd.GuardaSituacion(codpro, "0", hast1, "quiebre de stock");
						}
						break;
					case 2:
						if (hast2 != "")
						{
							admProd.GuardaSituacion(codpro, hast1, hast2, "casi quiebre");
						}
						break;
					case 3:
						if (hast3 != "")
						{
							admProd.GuardaSituacion(codpro, hast2, hast3, "zona de pedido");
						}
						break;
					default:
						MessageBox.Show("Error");
						break;
					}
				}
				for (int i = 1; i <= 4; i++)
				{
					switch (i)
					{
					case 1:
						if (cat1 != "")
						{
							admProd.GuardaCategorizacion(codpro, "0", cat1, "Rotación nula");
						}
						break;
					case 2:
						if (cat2 != "")
						{
							admProd.GuardaCategorizacion(codpro, cat1, cat2, "Rotación baja");
						}
						break;
					case 3:
						if (cat3 != "")
						{
							admProd.GuardaCategorizacion(codpro, cat2, cat3, "Rotación media");
						}
						break;
					case 4:
						if (cat4 != "")
						{
							admProd.GuardaCategorizacion(codpro, cat3, cat4, "Rotación alta");
						}
						break;
					default:
						MessageBox.Show("Error");
						break;
					}
				}
			}
			else
			{
				contenidoError = contenidoError + "> Codigo Producto No Encontrado: " + codProducto + " de la fila excel: " + iRow + "\n";
			}
			iRow++;
		}
	}

	private void uploadDataStockProductos(SLDocument sl, ref int iRow, ref string contenidoError)
	{
		while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
		{
			string codProducto = sl.GetCellValueAsString(iRow, 1);
			if (int.TryParse(codProducto, out var codpro))
			{
				string DISPONIBLE_D36_FR = sl.GetCellValueAsString(iRow, 3);
				string DISPONIBLE_G17_FR = sl.GetCellValueAsString(iRow, 6);
				string DISPONIBLE_G17_LM = sl.GetCellValueAsString(iRow, 9);
				string DISPONIBLE_D36_LM = sl.GetCellValueAsString(iRow, 12);
				for (int o = 1; o <= 4; o++)
				{
					switch (o)
					{
					case 1:
						if (DISPONIBLE_D36_FR != "")
						{
							admProd.ActualizaStockDisponible(codpro, 1, Convert.ToDecimal(DISPONIBLE_D36_FR));
						}
						break;
					case 2:
						if (DISPONIBLE_G17_FR != "")
						{
							admProd.ActualizaStockDisponible(codpro, 2, Convert.ToDecimal(DISPONIBLE_G17_FR));
						}
						break;
					case 3:
						if (DISPONIBLE_G17_LM != "")
						{
							admProd.ActualizaStockDisponible(codpro, 3, Convert.ToDecimal(DISPONIBLE_G17_LM));
						}
						break;
					case 4:
						if (DISPONIBLE_D36_LM != "")
						{
							admProd.ActualizaStockDisponible(codpro, 4, Convert.ToDecimal(DISPONIBLE_D36_LM));
						}
						break;
					default:
						MessageBox.Show("Error");
						break;
					}
				}
			}
			else
			{
				contenidoError = contenidoError + "> Codigo Producto No Encontrado: " + codProducto + " de la fila excel: " + iRow + "\n";
			}
			iRow++;
		}
	}

	private string obtenerRutaParaGuardar(string namefile = "Plantilla_Importacion")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "Guardando Archivo Excel de Exportacion";
			sfd.FileName = namefile;
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo el guardado de la plantilla", "Exportacion de Listado de Plantilla de Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private string obtenerRutaParaImportar()
	{
		OpenFileDialog openFileDialog1 = new OpenFileDialog
		{
			Title = "Seleccione un archivo excel a importar",
			CheckFileExists = true,
			CheckPathExists = true,
			DefaultExt = "xls",
			Filter = "Excel files (*.xlsx)|*.xlsx",
			FilterIndex = 2,
			RestoreDirectory = true,
			ReadOnlyChecked = true,
			ShowReadOnly = true
		};
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			return openFileDialog1.FileName;
		}
		return "";
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
		this.lblMensaje = new System.Windows.Forms.Label();
		this.btnImportarData = new System.Windows.Forms.Button();
		this.btnDescargaPlantilla = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.lblMensaje.Font = new System.Drawing.Font("Mongolian Baiti", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMensaje.Location = new System.Drawing.Point(12, 9);
		this.lblMensaje.Name = "lblMensaje";
		this.lblMensaje.Size = new System.Drawing.Size(392, 78);
		this.lblMensaje.TabIndex = 0;
		this.btnImportarData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnImportarData.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnImportarData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnImportarData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImportarData.Image = SIGEFA.Properties.Resources.upload_32x32;
		this.btnImportarData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImportarData.Location = new System.Drawing.Point(278, 120);
		this.btnImportarData.Name = "btnImportarData";
		this.btnImportarData.Size = new System.Drawing.Size(126, 32);
		this.btnImportarData.TabIndex = 80;
		this.btnImportarData.Text = "Importar Data";
		this.btnImportarData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImportarData.UseVisualStyleBackColor = false;
		this.btnImportarData.Click += new System.EventHandler(btnImportarData_Click);
		this.btnDescargaPlantilla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDescargaPlantilla.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnDescargaPlantilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDescargaPlantilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDescargaPlantilla.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnDescargaPlantilla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnDescargaPlantilla.Location = new System.Drawing.Point(15, 120);
		this.btnDescargaPlantilla.Name = "btnDescargaPlantilla";
		this.btnDescargaPlantilla.Size = new System.Drawing.Size(159, 32);
		this.btnDescargaPlantilla.TabIndex = 79;
		this.btnDescargaPlantilla.Text = "Descargar Plantilla";
		this.btnDescargaPlantilla.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnDescargaPlantilla.UseVisualStyleBackColor = false;
		this.btnDescargaPlantilla.Click += new System.EventHandler(btnDescargaPlantilla_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(416, 177);
		base.Controls.Add(this.btnImportarData);
		base.Controls.Add(this.btnDescargaPlantilla);
		base.Controls.Add(this.lblMensaje);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.MaximumSize = new System.Drawing.Size(432, 216);
		this.MinimumSize = new System.Drawing.Size(432, 216);
		base.Name = "frmImportaData";
		this.Text = "Importacion de Data";
		base.Load += new System.EventHandler(frmImportaData_Load);
		base.ResumeLayout(false);
	}
}
