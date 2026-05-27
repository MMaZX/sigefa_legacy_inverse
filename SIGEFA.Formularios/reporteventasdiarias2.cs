using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIGEFA.Reportes.clsReportes;
using SpreadsheetLight;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class reporteventasdiarias2 : RadForm
{
	public class PDFFooter : PdfPageEventHelper
	{
		public override void OnEndPage(PdfWriter writer, Document document)
		{
			PdfPTable tab = new PdfPTable(1);
			PdfPCell cell = new PdfPCell(new Phrase("Prueba de Pie de Página"));
			cell.Border = 0;
			tab.TotalWidth = 300f;
			tab.AddCell(cell);
			tab.WriteSelectedRows(0, -1, 300f, 30f, writer.DirectContent);
		}
	}

	private IContainer components = null;

	private Button btnReporte;

	private Label label1;

	private Label label2;

	private RadDateTimePicker dtpinicio;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private RadDateTimePicker dtpfinal;

	public reporteventasdiarias2()
	{
		InitializeComponent();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			if (validaInputs())
			{
				Cursor = Cursors.WaitCursor;
				DateTime aux_fecha = dtpinicio.Value;
				SLDocument sl = new SLDocument();
				while (aux_fecha.Date <= dtpfinal.Value.Date)
				{
					clsReporteVentas rv = new clsReporteVentas();
					clsReporteNotaCredito rnc = new clsReporteNotaCredito();
					DataTable aux_ventas = rv.ReporteDiario(aux_fecha, frmLogin.iCodAlmacen);
					DataTable aux_notas_creditos = rnc.ReporteNotaCreditoDiaria(aux_fecha, frmLogin.iCodAlmacen);
					if (aux_ventas.Rows.Count > 0)
					{
						int i = 0;
						int fila_excel = 3;
						int fila_a_concatenar = 0;
						int fila_first_concat = 0;
						int contador = 1;
						DateTime aux_date = Convert.ToDateTime(aux_ventas.Rows[0].ItemArray[0].ToString()).Date;
						string fecha = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
						sl.AddWorksheet(fecha.ToString());
						formatearFilaPrincipalHoja(sl, fecha);
						contador = 1;
						foreach (DataRow fila in aux_ventas.Rows)
						{
							sl.SetCellValue("A" + fila_excel, contador);
							DateTime aux_date_1 = Convert.ToDateTime(fila.ItemArray[0].ToString()).Date;
							string fecha_1 = aux_date_1.Day.ToString().PadLeft(2, '0') + "-" + aux_date_1.Month.ToString().PadLeft(2, '0') + "-" + aux_date_1.Year;
							sl.SetCellValue("B" + fila_excel, fecha_1);
							SLStyle aux_style = sl.CreateStyle();
							asignarBordes(aux_style);
							sl.SetCellStyle("A" + fila_excel, aux_style);
							sl.SetCellStyle("B" + fila_excel, aux_style);
							dandoValoresaFilaVentasExcel(sl, fila_excel, fila);
							if (i + 1 != aux_ventas.Rows.Count)
							{
								if (aux_ventas.Rows[i + 1].ItemArray[2].ToString() == fila.ItemArray[2].ToString())
								{
									if (fila_a_concatenar == 0)
									{
										fila_first_concat = fila_excel;
									}
									fila_a_concatenar++;
								}
								else if (fila_a_concatenar > 0 && fila_first_concat > 0)
								{
									string[] arr_letra = new string[6] { "B", "C", "D", "E", "F", "K" };
									concatenarColumnasV(sl, arr_letra, fila_first_concat, fila_a_concatenar);
									contador = concatenarContadorPrincipalV(sl, "A", fila_first_concat, contador, fila_a_concatenar);
									fila_a_concatenar = 0;
									fila_first_concat = 0;
								}
								DateTime aux_date_2 = Convert.ToDateTime(aux_ventas.Rows[i + 1].ItemArray[0].ToString()).Date;
								string fecha_2 = aux_date_2.Day.ToString().PadLeft(2, '0') + "-" + aux_date_2.Month.ToString().PadLeft(2, '0') + "-" + aux_date_2.Year;
								if (fecha_1 != fecha_2)
								{
									sl.AddWorksheet(fecha_2);
									formatearFilaPrincipalHoja(sl, fecha_2);
									fila_excel = 2;
								}
							}
							else if (fila_a_concatenar > 0 && fila_first_concat > 0)
							{
								string[] arr_letra2 = new string[6] { "B", "C", "D", "E", "F", "K" };
								concatenarColumnasV(sl, arr_letra2, fila_first_concat, fila_a_concatenar);
								contador = concatenarContadorPrincipalV(sl, "A", fila_first_concat, contador, fila_a_concatenar);
								fila_a_concatenar = 0;
								fila_first_concat = 0;
							}
							fila_excel++;
							i++;
							contador++;
						}
						Totalizar(sl, "I", "J", "K", 3, fila_excel, "TOTAL VENTAS");
						fila_excel++;
						formatearFilaSecundariaNotaCreditoHoja(sl, fila_excel);
						fila_excel += 2;
						int fila_inicial = fila_excel;
						if (aux_notas_creditos.Rows.Count > 0)
						{
							int j = 0;
							fila_a_concatenar = 0;
							fila_first_concat = 0;
							contador = 1;
							foreach (DataRow fila_nc in aux_notas_creditos.Rows)
							{
								sl.SetCellValue("A" + fila_excel, contador);
								DateTime aux_date_3 = Convert.ToDateTime(fila_nc.ItemArray[0].ToString()).Date;
								string fecha_3 = aux_date_3.Day.ToString().PadLeft(2, '0') + "-" + aux_date_3.Month.ToString().PadLeft(2, '0') + "-" + aux_date_3.Year;
								sl.SetCellValue("B" + fila_excel, fecha_3);
								dandoValoresaFilaNotaCreditoExcel(sl, fila_excel, fila_nc);
								if (j + 1 != aux_notas_creditos.Rows.Count)
								{
									if (aux_notas_creditos.Rows[j + 1].ItemArray[1].ToString() == fila_nc.ItemArray[1].ToString())
									{
										if (fila_a_concatenar == 0)
										{
											fila_first_concat = fila_excel;
										}
										fila_a_concatenar++;
									}
									else if (fila_a_concatenar > 0 && fila_first_concat > 0)
									{
										string[] arr_letra3 = new string[6] { "B", "C", "D", "E", "F", "K" };
										concatenarColumnasNC(sl, arr_letra3, fila_first_concat, fila_a_concatenar);
										contador = concatenarContadorPrincipalNC(sl, "A", fila_first_concat, contador, fila_a_concatenar);
										fila_a_concatenar = 0;
										fila_first_concat = 0;
									}
								}
								else if (fila_a_concatenar > 0 && fila_first_concat > 0)
								{
									string[] arr_letra4 = new string[6] { "B", "C", "D", "E", "F", "K" };
									concatenarColumnasNC(sl, arr_letra4, fila_first_concat, fila_a_concatenar);
									contador = concatenarContadorPrincipalNC(sl, "A", fila_first_concat, contador, fila_a_concatenar);
									fila_a_concatenar = 0;
									fila_first_concat = 0;
								}
								fila_excel++;
								j++;
								contador++;
							}
							Totalizar(sl, "I", "J", "K", fila_inicial, fila_excel, "TOTAL NC");
						}
						fila_excel++;
						fila_excel++;
						añadiendoSumatoria(sl, fila_excel, aux_fecha, frmLogin.iCodAlmacen);
					}
					aux_fecha = aux_fecha.AddDays(1.0);
				}
				Cursor = Cursors.Default;
				try
				{
					string cadenaGuardado = obtenerRutaParaGuardar();
					if (cadenaGuardado != null)
					{
						sl.SaveAs(cadenaGuardado);
						Process.Start("explorer.exe", cadenaGuardado);
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Reporte Ventas Diarias - Line 43");
					return;
				}
			}
			MessageBox.Show("Datos Invalidos", "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.ToString(), ex2.Message);
		}
	}

	private int añadiendoSumatoria(SLDocument sl, int fila_excel, DateTime aux_fecha, int codalma)
	{
		int posIngresos = 0;
		int posEgresos = 0;
		int posNC = 0;
		int posSaldoEfectivo = 0;
		int posSaldoCajaSistema = 0;
		clsReporteVentas rv = new clsReporteVentas();
		DataTable aux_ventas_agrup = rv.ReporteDiarioAgrupadosTotal(aux_fecha, codalma);
		SLStyle style_con_bordes = sl.CreateStyle();
		asignarBordes(style_con_bordes);
		SLStyle style = sl.CreateStyle();
		int ifila_primer_total_venta = fila_excel;
		sl.SetCellValue("F" + fila_excel, 1);
		style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(166, 166, 166), System.Drawing.Color.Blue);
		sl.SetCellStyle("F" + fila_excel, style);
		sl.SetCellValue("H" + fila_excel, "TOTAL VENTAS");
		sl.SetCellStyle("H" + fila_excel, style);
		int icol = 8;
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
		DataTable almacenes = rv.AlmacenXUbicacion(codalma);
		for (int i = 0; i < almacenes.Rows.Count; i++)
		{
			sl.SetCellValue(fila_excel, icol + i + 1, almacenes.Rows[i].ItemArray[1].ToString());
			style = sl.CreateStyle();
			style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
			style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(166, 166, 166), System.Drawing.Color.Blue);
			style.SetFontBold(IsBold: true);
			sl.SetCellStyle(fila_excel, icol + i + 1, style);
		}
		int idalmacen1 = Convert.ToInt32(almacenes.Rows[0].ItemArray[0]);
		int idalmacen2 = Convert.ToInt32(almacenes.Rows[1].ItemArray[0]);
		sl.SetCellValue(fila_excel, icol + almacenes.Rows.Count + 1, "TOTAL");
		style = sl.CreateStyle();
		style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(166, 166, 166), System.Drawing.Color.Blue);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetFontBold(IsBold: true);
		sl.SetCellStyle(fila_excel, icol + almacenes.Rows.Count + 1, style);
		fila_excel++;
		double sumatoria_ventas_alm1 = 0.0;
		double sumatoria_ventas_alm2 = 0.0;
		foreach (DataRow fila_v in aux_ventas_agrup.Rows)
		{
			if (!(fila_v.ItemArray[11].ToString() == "ANULADO"))
			{
				if (Convert.ToInt32(fila_v.ItemArray[10] ?? ((object)0)) == idalmacen1)
				{
					sumatoria_ventas_alm1 += Convert.ToDouble(fila_v.ItemArray[8] ?? ((object)0));
				}
				else
				{
					sumatoria_ventas_alm2 += Convert.ToDouble(fila_v.ItemArray[8] ?? ((object)0));
				}
			}
		}
		style = sl.CreateStyle();
		style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(166, 166, 166), System.Drawing.Color.Blue);
		style.FormatCode = "\"S/\" #,##0.00";
		sl.SetCellValue("I" + fila_excel, sumatoria_ventas_alm1);
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, sumatoria_ventas_alm2);
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		int ifila_total_ventas = fila_excel;
		SLStyle style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(166, 166, 166), System.Drawing.Color.Blue);
		sl.SetCellStyle("F" + ifila_primer_total_venta, "F" + ifila_total_ventas, style_bg);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + ifila_primer_total_venta, "K" + ifila_total_ventas, style_bg);
		int espacio = 1;
		fila_excel += espacio;
		SLStyle estilo = sl.CreateStyle();
		estilo.FormatCode = "\"S/\" #,##0.00";
		icol = 8;
		int ifila_primer_Pendiente = fila_excel;
		if (aux_ventas_agrup.Rows.Count > 0)
		{
			foreach (DataRow fila in aux_ventas_agrup.Rows)
			{
				if (fila.ItemArray[9].ToString() == "PENDIENTE")
				{
					sl.SetCellValue("H" + fila_excel, fila.ItemArray[2].ToString());
					if (Convert.ToInt32(fila.ItemArray[10] ?? ((object)0)) == idalmacen1)
					{
						sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila.ItemArray[8] ?? ((object)0)));
						sl.SetCellStyle("I" + fila_excel, estilo);
					}
					else
					{
						sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila.ItemArray[8] ?? ((object)0)));
						sl.SetCellStyle("J" + fila_excel, estilo);
					}
					sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
					sl.SetCellStyle("K" + fila_excel, estilo);
					fila_excel++;
				}
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		if (ifila_primer_Pendiente == fila_excel)
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		sl.SetCellValue("F" + fila_excel, 2);
		sl.SetCellValue("H" + fila_excel, "TOTAL PENDIENTES");
		sl.SetCellValue("I" + fila_excel, "=SUM(I" + ifila_primer_Pendiente + ":I" + (fila_excel - 1) + ")");
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, "=SUM(J" + ifila_primer_Pendiente + ":J" + (fila_excel - 1) + ")");
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		int ifilaTotalpendiente = fila_excel;
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(255, 229, 204), System.Drawing.Color.Beige);
		sl.SetCellStyle("H" + ifila_primer_Pendiente, "K" + ifilaTotalpendiente, style_con_bordes);
		sl.SetCellStyle("F" + ifilaTotalpendiente, style_bg);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + ifilaTotalpendiente, "K" + ifilaTotalpendiente, style_bg);
		fila_excel++;
		DataTable pagos_tarjeta = rv.ReporteDiarioAgrupadoTotalPagos(aux_fecha, codalma, 8);
		style = sl.CreateStyle();
		style.FormatCode = "\"S/\" #,##0.00";
		icol = 8;
		int ifila_primer_tarjeta = fila_excel;
		if (pagos_tarjeta.Rows.Count > 0)
		{
			foreach (DataRow fila_v2 in pagos_tarjeta.Rows)
			{
				if (!(fila_v2.ItemArray[12].ToString() == "ANULADO"))
				{
					sl.SetCellValue("H" + fila_excel, fila_v2.ItemArray[2].ToString());
					if (Convert.ToInt32(fila_v2.ItemArray[10] ?? ((object)0)) == idalmacen1)
					{
						sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v2.ItemArray[11] ?? ((object)0)));
						sl.SetCellStyle("I" + fila_excel, style);
					}
					else
					{
						sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v2.ItemArray[11] ?? ((object)0)));
						sl.SetCellStyle("J" + fila_excel, style);
					}
					sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
					sl.SetCellStyle("K" + fila_excel, style);
					sl.SetCellValue("L" + fila_excel, "=K" + fila_excel + "/0.96");
					sl.SetCellStyle("L" + fila_excel, style);
					fila_excel++;
				}
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		if (ifila_primer_tarjeta == fila_excel)
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		sl.SetCellValue("F" + fila_excel, 3);
		sl.SetCellValue("H" + fila_excel, "TOTAL DE VENTAS CON TARJETA ");
		sl.SetCellValue("I" + fila_excel, "=SUM(I" + ifila_primer_tarjeta + ":I" + (fila_excel - 1) + ")");
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, "=SUM(J" + ifila_primer_tarjeta + ":J" + (fila_excel - 1) + ")");
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		sl.SetCellValue("L" + fila_excel, "=K" + fila_excel + "/0.96");
		sl.SetCellStyle("L" + fila_excel, style);
		int ifilaTotalTarjeta = fila_excel;
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(189, 215, 238), System.Drawing.Color.Blue);
		sl.SetCellStyle("H" + ifila_primer_tarjeta, "K" + ifilaTotalTarjeta, style_con_bordes);
		sl.SetCellStyle("F" + ifilaTotalTarjeta, style_bg);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + ifilaTotalTarjeta, "K" + ifilaTotalTarjeta, style_bg);
		fila_excel += espacio;
		DataTable pagos_transferencia = rv.ReporteDiarioAgrupadoTotalPagos(aux_fecha, codalma, 9);
		style = sl.CreateStyle();
		style.FormatCode = "\"S/\" #,##0.00";
		icol = 8;
		int ifila_primer_transferencia = fila_excel;
		if (pagos_transferencia.Rows.Count > 0)
		{
			foreach (DataRow fila_v3 in pagos_transferencia.Rows)
			{
				if (!(fila_v3.ItemArray[12].ToString() == "ANULADO"))
				{
					sl.SetCellStyle("G" + fila_excel, "K" + fila_excel, style_con_bordes);
					asignarBordes(style_bg);
					sl.SetCellValue("F" + fila_excel, fila_v3.ItemArray[14].ToString());
					sl.SetCellValue("G" + fila_excel, fila_v3.ItemArray[13].ToString());
					sl.SetCellValue("H" + fila_excel, fila_v3.ItemArray[2].ToString());
					if (Convert.ToInt32(fila_v3.ItemArray[10] ?? ((object)0)) == idalmacen1)
					{
						sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v3.ItemArray[11] ?? ((object)0)));
						sl.SetCellStyle("I" + fila_excel, style);
					}
					else
					{
						sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v3.ItemArray[11] ?? ((object)0)));
						sl.SetCellStyle("J" + fila_excel, style);
					}
					sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
					sl.SetCellStyle("K" + fila_excel, style);
					fila_excel++;
				}
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		if (ifila_primer_transferencia == fila_excel)
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		sl.SetCellValue("F" + fila_excel, 4);
		sl.SetCellValue("H" + fila_excel, "TOTAL DE VENTAS CON TRANSFERENCIA ");
		sl.SetCellValue("I" + fila_excel, "=SUM(I" + ifila_primer_transferencia + ":I" + (fila_excel - 1) + ")");
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, "=SUM(J" + ifila_primer_transferencia + ":J" + (fila_excel - 1) + ")");
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		int ifilaTotalTransferencia = fila_excel;
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(198, 224, 180), System.Drawing.Color.Blue);
		sl.SetCellStyle("H" + ifila_primer_transferencia, "K" + ifilaTotalTransferencia, style_con_bordes);
		sl.SetCellStyle("F" + ifilaTotalTransferencia, "F" + ifilaTotalTransferencia, style_bg);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + ifilaTotalTransferencia, "K" + ifilaTotalTransferencia, style_bg);
		fila_excel += espacio;
		DataTable pagos_deposito = rv.ReporteDiarioAgrupadoTotalPagos(aux_fecha, codalma, 6);
		style = sl.CreateStyle();
		style.FormatCode = "\"S/\" #,##0.00";
		icol = 8;
		int ifila_primer_deposito = fila_excel;
		if (pagos_deposito.Rows.Count > 0)
		{
			foreach (DataRow fila_v4 in pagos_deposito.Rows)
			{
				if (!(fila_v4.ItemArray[12].ToString() == "ANULADO"))
				{
					sl.SetCellValue("H" + fila_excel, fila_v4.ItemArray[2].ToString());
					if (Convert.ToInt32(fila_v4.ItemArray[10] ?? ((object)0)) == idalmacen1)
					{
						sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v4.ItemArray[11] ?? ((object)0)));
						sl.SetCellStyle("I" + fila_excel, style);
					}
					else
					{
						sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v4.ItemArray[11] ?? ((object)0)));
						sl.SetCellStyle("J" + fila_excel, style);
					}
					sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
					sl.SetCellStyle("K" + fila_excel, style);
					fila_excel++;
				}
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		if (ifila_primer_deposito == fila_excel)
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		sl.SetCellValue("F" + fila_excel, 5);
		sl.SetCellValue("H" + fila_excel, "TOTAL DESCUENTOS A TRANSPORTISTA ");
		sl.SetCellValue("I" + fila_excel, "=SUM(I" + ifila_primer_deposito + ":I" + (fila_excel - 1) + ")");
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, "=SUM(J" + ifila_primer_deposito + ":J" + (fila_excel - 1) + ")");
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		int ifilaTotalDeposito = fila_excel;
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(244, 176, 132), System.Drawing.Color.Blue);
		sl.SetCellStyle("H" + ifila_primer_deposito, "K" + ifilaTotalDeposito, style_con_bordes);
		sl.SetCellStyle("F" + ifilaTotalDeposito, "F" + ifilaTotalDeposito, style_bg);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + ifilaTotalDeposito, "K" + ifilaTotalDeposito, style_bg);
		fila_excel += espacio;
		DataTable ventas_credito = rv.ReporteDiarioVentasCredito(aux_fecha, codalma);
		style = sl.CreateStyle();
		style.FormatCode = "\"S/\" #,##0.00";
		icol = 8;
		int ifila_primer_venta_credito = fila_excel;
		if (ventas_credito.Rows.Count > 0)
		{
			foreach (DataRow fila_v5 in ventas_credito.Rows)
			{
				if (!(fila_v5.ItemArray[14].ToString() == "ANULADO"))
				{
					sl.SetCellValue("H" + fila_excel, fila_v5.ItemArray[2].ToString());
					if (Convert.ToInt32(fila_v5.ItemArray[10] ?? ((object)0)) == idalmacen1)
					{
						sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v5.ItemArray[8] ?? ((object)0)));
						sl.SetCellStyle("I" + fila_excel, style);
					}
					else
					{
						sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v5.ItemArray[8] ?? ((object)0)));
						sl.SetCellStyle("J" + fila_excel, style);
					}
					sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
					sl.SetCellStyle("K" + fila_excel, style);
					fila_excel++;
				}
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		if (ifila_primer_venta_credito == fila_excel)
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		sl.SetCellValue("F" + fila_excel, 6);
		sl.SetCellValue("H" + fila_excel, "TOTAL VENTAS A CREDITO ");
		sl.SetCellValue("I" + fila_excel, "=SUM(I" + ifila_primer_venta_credito + ":I" + (fila_excel - 1) + ")");
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, "=SUM(J" + ifila_primer_venta_credito + ":J" + (fila_excel - 1) + ")");
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		int ifilaTotalVentaCredito = fila_excel;
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(255, 217, 102), System.Drawing.Color.Blue);
		sl.SetCellStyle("H" + ifila_primer_venta_credito, "K" + ifilaTotalVentaCredito, style_con_bordes);
		sl.SetCellStyle("F" + ifilaTotalVentaCredito, "F" + ifilaTotalVentaCredito, style_bg);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + ifilaTotalVentaCredito, "K" + ifilaTotalVentaCredito, style_bg);
		fila_excel++;
		sl.SetCellValue("F" + fila_excel, 7);
		sl.SetCellValue("H" + fila_excel, "TOTAL V. CONTADO (1-6)");
		sl.SetCellValue("I" + fila_excel, "=I" + ifila_total_ventas + "-I" + ifilaTotalVentaCredito);
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, "=J" + ifila_total_ventas + "-J" + ifilaTotalVentaCredito);
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		int ifilaTotalIngreso = fila_excel;
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(174, 170, 170), System.Drawing.Color.Blue);
		sl.SetCellStyle("F" + ifilaTotalIngreso, style_bg);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + ifilaTotalIngreso, "K" + ifilaTotalIngreso, style_bg);
		DataTable egresos_caja_chica = rv.ListaCajaEgresos(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
		fila_excel++;
		double total1 = 0.0;
		double total2 = 0.0;
		if (egresos_caja_chica.Rows.Count > 0)
		{
			foreach (DataRow fila_v6 in egresos_caja_chica.Rows)
			{
				style_bg = sl.CreateStyle();
				sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
				asignarBordes(style_bg);
				sl.SetCellValue("H" + fila_excel, fila_v6.ItemArray[6].ToString() + " - " + fila_v6.ItemArray[5].ToString());
				if (Convert.ToInt32(fila_v6.ItemArray[22] ?? ((object)0)) == idalmacen1)
				{
					sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v6.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("I" + fila_excel, style);
					total1 += Convert.ToDouble(fila_v6.ItemArray[7] ?? ((object)0));
				}
				else
				{
					sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v6.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("J" + fila_excel, style);
					total2 += Convert.ToDouble(fila_v6.ItemArray[7] ?? ((object)0));
				}
				sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
				sl.SetCellStyle("K" + fila_excel, style);
				fila_excel++;
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
			posEgresos = fila_excel;
		}
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(255, 64, 0), System.Drawing.Color.Blue);
		sl.SetCellStyle("F" + fila_excel, "F" + fila_excel, style_bg);
		sl.SetCellValue("F" + fila_excel, 8);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_bg);
		sl.SetCellValue("H" + fila_excel, "TOTAL EGRESOS CAJA ");
		sl.SetCellValue("I" + fila_excel, Convert.ToDouble(total1));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, Convert.ToDouble(total2));
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		posEgresos = fila_excel;
		DataTable notas_credito = rv.ListaNotasCredito(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
		fila_excel++;
		double total3 = 0.0;
		double total4 = 0.0;
		if (notas_credito.Rows.Count > 0)
		{
			foreach (DataRow fila_v7 in notas_credito.Rows)
			{
				style_bg = sl.CreateStyle();
				sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
				asignarBordes(style_bg);
				sl.SetCellValue("H" + fila_excel, fila_v7.ItemArray[15].ToString() + " - " + fila_v7.ItemArray[16].ToString());
				if (Convert.ToInt32(fila_v7.ItemArray[21] ?? ((object)0)) == idalmacen1)
				{
					sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v7.ItemArray[6] ?? ((object)0)));
					sl.SetCellStyle("I" + fila_excel, style);
					total3 += Convert.ToDouble(fila_v7.ItemArray[6] ?? ((object)0));
				}
				else
				{
					sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v7.ItemArray[6] ?? ((object)0)));
					sl.SetCellStyle("J" + fila_excel, style);
					total4 += Convert.ToDouble(fila_v7.ItemArray[6] ?? ((object)0));
				}
				sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
				sl.SetCellStyle("K" + fila_excel, style);
				fila_excel++;
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
			posNC = fila_excel;
		}
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(65, 105, 225), System.Drawing.Color.Blue);
		sl.SetCellStyle("F" + fila_excel, "F" + fila_excel, style_bg);
		sl.SetCellValue("F" + fila_excel, 9);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_bg);
		sl.SetCellValue("H" + fila_excel, "NOTAS DE CREDITO COBRADAS");
		sl.SetCellValue("I" + fila_excel, Convert.ToDouble(total3));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, Convert.ToDouble(total4));
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		posNC = fila_excel;
		DataTable ingresos_caja_chica = rv.ListaCajaIngresos(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
		fila_excel++;
		double total5 = 0.0;
		double total6 = 0.0;
		if (ingresos_caja_chica.Rows.Count > 0)
		{
			foreach (DataRow fila_v8 in ingresos_caja_chica.Rows)
			{
				if (fila_v8.ItemArray[23].ToString() != "")
				{
					string[] r = fila_v8.ItemArray[23].ToString().Split(',');
					SLStyle style2 = sl.CreateStyle();
					style2.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(Convert.ToInt32(r[0]), Convert.ToInt32(r[1]), Convert.ToInt32(r[2])), System.Drawing.Color.FromArgb(Convert.ToInt32(r[0]), Convert.ToInt32(r[1]), Convert.ToInt32(r[2])));
					sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style2);
				}
				sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
				asignarBordes(style_bg);
				sl.SetCellValue("H" + fila_excel, fila_v8.ItemArray[6].ToString() + " - " + fila_v8.ItemArray[5].ToString());
				if (Convert.ToInt32(fila_v8.ItemArray[22] ?? ((object)0)) == idalmacen1)
				{
					sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v8.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("I" + fila_excel, style);
					total5 += Convert.ToDouble(fila_v8.ItemArray[7] ?? ((object)0));
				}
				else
				{
					sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v8.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("J" + fila_excel, style);
					total6 += Convert.ToDouble(fila_v8.ItemArray[7] ?? ((object)0));
				}
				sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
				sl.SetCellStyle("K" + fila_excel, style);
				fila_excel++;
			}
		}
		else
		{
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
			posIngresos = fila_excel;
		}
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(199, 250, 179), System.Drawing.Color.Blue);
		sl.SetCellStyle("F" + fila_excel, "F" + fila_excel, style_bg);
		sl.SetCellValue("F" + fila_excel, 10);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_bg);
		sl.SetCellValue("H" + fila_excel, "TOTAL INGRESOS EFECTIVO");
		sl.SetCellValue("I" + fila_excel, Convert.ToDouble(total5));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, Convert.ToDouble(total6));
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		posIngresos = fila_excel;
		fila_excel++;
		asignarBordes(style_bg);
		style.SetFontBold(IsBold: true);
		sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
		sl.SetCellValue("H" + fila_excel, "TOTAL A DEPOSITAR (7-2-3-4-5-8-9+10)");
		sl.SetCellValue("I" + fila_excel, "=SUM(I" + ifilaTotalIngreso + "-I" + ifilaTotalpendiente + "-I" + ifilaTotalTarjeta + "-I" + ifilaTotalTransferencia + "-I" + ifilaTotalDeposito + "-I" + posEgresos + "-I" + posNC + "+I" + posIngresos + ")");
		sl.SetCellValue("J" + fila_excel, "=SUM(J" + ifilaTotalIngreso + "-J" + ifilaTotalpendiente + "-J" + ifilaTotalTarjeta + "-J" + ifilaTotalTransferencia + "-J" + ifilaTotalDeposito + "-J" + posEgresos + "-J" + posNC + "+J" + posIngresos + ")");
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + "+J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		fila_excel++;
		DataTable ingresos_tarjeta = rv.ListaCajaIngresosTarjeta(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
		fila_excel++;
		style.SetFontBold(IsBold: false);
		double total7 = 0.0;
		double total8 = 0.0;
		if (ingresos_tarjeta.Rows.Count > 0)
		{
			foreach (DataRow fila_v9 in ingresos_tarjeta.Rows)
			{
				if (fila_v9.ItemArray[24].ToString() != "")
				{
					string[] r2 = fila_v9.ItemArray[24].ToString().Split(',');
					SLStyle style3 = sl.CreateStyle();
					style3.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(Convert.ToInt32(r2[0]), Convert.ToInt32(r2[1]), Convert.ToInt32(r2[2])), System.Drawing.Color.FromArgb(Convert.ToInt32(r2[0]), Convert.ToInt32(r2[1]), Convert.ToInt32(r2[2])));
					sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style3);
				}
				sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
				asignarBordes(style_bg);
				sl.SetCellValue("G" + fila_excel, fila_v9.ItemArray[23].ToString());
				sl.SetCellValue("H" + fila_excel, fila_v9.ItemArray[6].ToString() + " - " + fila_v9.ItemArray[5].ToString());
				if (Convert.ToInt32(fila_v9.ItemArray[22] ?? ((object)0)) == idalmacen1)
				{
					sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v9.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("I" + fila_excel, style);
					total7 += Convert.ToDouble(fila_v9.ItemArray[7] ?? ((object)0));
				}
				else
				{
					sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v9.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("J" + fila_excel, style);
					total8 += Convert.ToDouble(fila_v9.ItemArray[7] ?? ((object)0));
				}
				sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
				sl.SetCellStyle("K" + fila_excel, style);
				sl.SetCellValue("L" + fila_excel, "=K" + fila_excel + "/0.96");
				sl.SetCellStyle("L" + fila_excel, style);
				fila_excel++;
			}
		}
		else
		{
			sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(251, 201, 254), System.Drawing.Color.Blue);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_bg);
		sl.SetCellValue("H" + fila_excel, "TOTAL INGRESOS TARJETA");
		sl.SetCellValue("I" + fila_excel, Convert.ToDouble(total7));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, Convert.ToDouble(total8));
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		sl.SetCellValue("K" + (fila_excel + 1), "POS");
		sl.SetCellValue("L" + fila_excel, "=K" + fila_excel + "/0.96");
		Thread.Sleep(1000);
		sl.SetCellValue("L" + (fila_excel + 1), "=REDONDEAR(L" + ifilaTotalTarjeta + "+L" + fila_excel + "," + 2 + ")");
		sl.SetCellStyle("L" + fila_excel, style);
		DataTable ingresos_transferencia = rv.ListaCajaIngresosTransferencia(aux_fecha, frmLogin.iCodAlmacen, frmLogin.iCodSucursal);
		fila_excel += 3;
		style.SetFontBold(IsBold: false);
		double total9 = 0.0;
		double total10 = 0.0;
		if (ingresos_transferencia.Rows.Count > 0)
		{
			foreach (DataRow fila_v10 in ingresos_transferencia.Rows)
			{
				if (fila_v10.ItemArray[24].ToString() != "")
				{
					string[] r3 = fila_v10.ItemArray[24].ToString().Split(',');
					SLStyle style4 = sl.CreateStyle();
					style4.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(Convert.ToInt32(r3[0]), Convert.ToInt32(r3[1]), Convert.ToInt32(r3[2])), System.Drawing.Color.FromArgb(Convert.ToInt32(r3[0]), Convert.ToInt32(r3[1]), Convert.ToInt32(r3[2])));
					sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style4);
				}
				sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
				asignarBordes(style_bg);
				sl.SetCellValue("G" + fila_excel, fila_v10.ItemArray[23].ToString());
				sl.SetCellValue("H" + fila_excel, fila_v10.ItemArray[6].ToString() + " - " + fila_v10.ItemArray[5].ToString());
				if (Convert.ToInt32(fila_v10.ItemArray[22] ?? ((object)0)) == idalmacen1)
				{
					sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila_v10.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("I" + fila_excel, style);
					total9 += Convert.ToDouble(fila_v10.ItemArray[7] ?? ((object)0));
				}
				else
				{
					sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila_v10.ItemArray[7] ?? ((object)0)));
					sl.SetCellStyle("J" + fila_excel, style);
					total10 += Convert.ToDouble(fila_v10.ItemArray[7] ?? ((object)0));
				}
				sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
				sl.SetCellStyle("K" + fila_excel, style);
				fila_excel++;
			}
		}
		else
		{
			sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_con_bordes);
			sl.SetCellValue("H" + fila_excel, "NO APLICA");
			fila_excel++;
		}
		style_bg = sl.CreateStyle();
		style_bg.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(254, 202, 201), System.Drawing.Color.Blue);
		asignarBordes(style_bg);
		sl.SetCellStyle("H" + fila_excel, "K" + fila_excel, style_bg);
		sl.SetCellValue("H" + fila_excel, "TOTAL INGRESOS TRANSFERENCIA");
		sl.SetCellValue("I" + fila_excel, Convert.ToDouble(total9));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, Convert.ToDouble(total10));
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, "=SUM(I" + fila_excel + ":J" + fila_excel + ")");
		sl.SetCellStyle("K" + fila_excel, style);
		return fila_excel;
	}

	private void Totalizar(SLDocument sl, string letColIn, string letColFin, string letColMonto, int iFilaIni, int iFilaFin, string sms)
	{
		sl.SetCellValue(letColIn + iFilaFin, sms);
		sl.MergeWorksheetCells(letColIn + iFilaFin, letColFin + iFilaFin);
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		sl.SetCellStyle(letColIn + iFilaFin, style);
		sl.SetCellValue(letColMonto + iFilaFin, "=SUM(" + letColMonto + iFilaIni + ":" + letColMonto + (iFilaFin - 1) + ")");
		style = sl.CreateStyle();
		style.FormatCode = "#,##0.00";
		sl.SetCellStyle(letColMonto + iFilaFin, style);
	}

	private void concatenarColumnasNC(SLDocument sl, string[] arr_letra, int fila_first_concat, int fila_a_concatenar)
	{
		for (int i = 1; i <= fila_a_concatenar; i++)
		{
			foreach (string letra in arr_letra)
			{
				sl.SetCellValue(letra + (fila_first_concat + i), 0);
			}
		}
		foreach (string letra2 in arr_letra)
		{
			sl.MergeWorksheetCells(letra2 + fila_first_concat, letra2 + (fila_first_concat + fila_a_concatenar));
		}
	}

	private int concatenarContadorPrincipalNC(SLDocument sl, string letra_de_fila, int fila_first_concat, int contador, int fila_a_concatenar)
	{
		sl.SetCellValue(letra_de_fila + fila_first_concat, contador);
		if (fila_a_concatenar == 1)
		{
			sl.SetCellValue(letra_de_fila + (fila_first_concat + 1), 0);
		}
		else
		{
			for (int i = 1; i < fila_a_concatenar; i++)
			{
				sl.SetCellValue(letra_de_fila + (fila_first_concat + i), 0);
			}
		}
		sl.MergeWorksheetCells(letra_de_fila + fila_first_concat, letra_de_fila + (fila_first_concat + fila_a_concatenar));
		sl.SetCellValue("A" + fila_first_concat, contador - fila_a_concatenar);
		Console.WriteLine("variable: " + (contador - fila_a_concatenar));
		return contador - fila_a_concatenar;
	}

	private void dandoValoresaFilaVentasExcel(SLDocument sl, int fila_excel, DataRow fila)
	{
		SLStyle style = sl.CreateStyle();
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		if (fila.ItemArray[13].ToString() != "")
		{
			string[] r = fila.ItemArray[13].ToString().Split(',');
			style = sl.CreateStyle();
			style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(Convert.ToInt32(r[0]), Convert.ToInt32(r[1]), Convert.ToInt32(r[2])), System.Drawing.Color.FromArgb(Convert.ToInt32(r[0]), Convert.ToInt32(r[1]), Convert.ToInt32(r[2])));
		}
		sl.SetCellStyle("A" + fila_excel, "K" + fila_excel, style);
		asignarBordes(style);
		sl.SetCellStyle("A" + fila_excel, style);
		sl.SetCellStyle("B" + fila_excel, style);
		sl.SetCellStyle("C" + fila_excel, style);
		sl.SetCellStyle("D" + fila_excel, style);
		sl.SetCellValue("C" + fila_excel, fila.ItemArray[1].ToString());
		sl.SetCellValue("D" + fila_excel, fila.ItemArray[2].ToString());
		sl.SetCellValue("E" + fila_excel, fila.ItemArray[3].ToString());
		style = sl.CreateStyle();
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		style.SetWrapText(IsWrapped: true);
		asignarBordes(style);
		sl.SetCellStyle("E" + fila_excel, style);
		sl.SetCellValue("F" + fila_excel, fila.ItemArray[9].ToString());
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellStyle("F" + fila_excel, style);
		sl.SetCellValue("G" + fila_excel, Convert.ToDouble(fila.ItemArray[4] ?? ((object)0.0)));
		style = sl.CreateStyle();
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		asignarBordes(style);
		sl.SetCellStyle("G" + fila_excel, style);
		sl.SetCellValue("H" + fila_excel, fila.ItemArray[5].ToString());
		style = sl.CreateStyle();
		asignarBordes(style);
		style.SetWrapText(IsWrapped: true);
		sl.SetCellStyle("H" + fila_excel, style);
		style = sl.CreateStyle();
		style.FormatCode = "#,##0.00";
		asignarBordes(style);
		sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila.ItemArray[6] ?? ((object)0.0)));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila.ItemArray[7] ?? ((object)0.0)));
		sl.SetCellStyle("J" + fila_excel, style);
		if (fila.ItemArray[12].ToString() == "ANULADO")
		{
			sl.SetCellValue("K" + fila_excel, Convert.ToDouble(0.0));
			sl.SetCellValue("F" + fila_excel, fila.ItemArray[12].ToString());
			style = sl.CreateStyle();
			style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Pink, System.Drawing.Color.Blue);
			style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
			style.SetVerticalAlignment(VerticalAlignmentValues.Center);
			asignarBordes(style);
			sl.SetCellStyle("F" + fila_excel, style);
		}
		else
		{
			sl.SetCellValue("K" + fila_excel, Convert.ToDouble(fila.ItemArray[8] ?? ((object)0.0)));
		}
		style = sl.CreateStyle();
		style.FormatCode = "#,##0.00";
		asignarBordes(style);
		sl.SetCellStyle("K" + fila_excel, style);
		if (fila.ItemArray[12].ToString() == "ANULADO")
		{
			style = sl.CreateStyle();
			style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Pink, System.Drawing.Color.Blue);
			sl.SetCellStyle("A" + fila_excel, "K" + fila_excel, style);
		}
	}

	private void dandoValoresaFilaNotaCreditoExcel(SLDocument sl, int fila_excel, DataRow fila)
	{
		SLStyle format_fecha = sl.CreateStyle();
		asignarBordes(format_fecha);
		format_fecha.FormatCode = "dd/mm/yyyy";
		format_fecha.SetVerticalAlignment(VerticalAlignmentValues.Center);
		SLStyle style = sl.CreateStyle();
		asignarBordes(style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellStyle("A" + fila_excel, style);
		sl.SetCellStyle("B" + fila_excel, style);
		sl.SetCellStyle("C" + fila_excel, style);
		sl.SetCellValue("C" + fila_excel, fila.ItemArray[1].ToString());
		DateTime aux_date = Convert.ToDateTime(fila.ItemArray[2].ToString()).Date;
		string fecha = aux_date.Day.ToString().PadLeft(2, '0') + "-" + aux_date.Month.ToString().PadLeft(2, '0') + "-" + aux_date.Year;
		sl.SetCellValue("D" + fila_excel, fecha);
		sl.SetCellStyle("D" + fila_excel, style);
		sl.SetCellValue("E" + fila_excel, fila.ItemArray[3].ToString());
		sl.SetCellStyle("E" + fila_excel, style);
		sl.SetCellValue("F" + fila_excel, fila.ItemArray[4].ToString());
		sl.SetCellStyle("F" + fila_excel, style);
		sl.SetCellValue("G" + fila_excel, Convert.ToDouble(fila.ItemArray[5] ?? ((object)0.0)));
		sl.SetCellStyle("G" + fila_excel, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		style.SetWrapText(IsWrapped: true);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		style.FormatCode = "#,##0.00";
		sl.SetCellValue("H" + fila_excel, fila.ItemArray[6].ToString());
		sl.SetCellStyle("H" + fila_excel, style);
		sl.SetCellValue("I" + fila_excel, Convert.ToDouble(fila.ItemArray[7] ?? ((object)0.0)));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, Convert.ToDouble(fila.ItemArray[8] ?? ((object)0.0)));
		sl.SetCellStyle("J" + fila_excel, style);
		sl.SetCellValue("K" + fila_excel, Convert.ToDouble(fila.ItemArray[9] ?? ((object)0.0)));
		sl.SetCellStyle("K" + fila_excel, style);
	}

	private int concatenarContadorPrincipalV(SLDocument sl, string letra_de_fila, int fila_first_concat, int contador, int fila_a_concatenar)
	{
		sl.SetCellValue(letra_de_fila + fila_first_concat, contador);
		if (fila_a_concatenar == 1)
		{
			sl.SetCellValue(letra_de_fila + (fila_first_concat + 1), 0);
		}
		else
		{
			for (int i = 1; i < fila_a_concatenar; i++)
			{
				sl.SetCellValue(letra_de_fila + (fila_first_concat + i), 0);
			}
		}
		sl.MergeWorksheetCells(letra_de_fila + fila_first_concat, letra_de_fila + (fila_first_concat + fila_a_concatenar));
		sl.SetCellValue("A" + fila_first_concat, contador - fila_a_concatenar);
		return contador - fila_a_concatenar;
	}

	private void concatenarColumnasV(SLDocument sl, string[] arr_letra, int fila_first_concat, int fila_a_concatenar)
	{
		for (int i = 1; i <= fila_a_concatenar; i++)
		{
			foreach (string letra in arr_letra)
			{
				sl.SetCellValue(letra + (fila_first_concat + i), 0);
			}
		}
		foreach (string letra2 in arr_letra)
		{
			sl.MergeWorksheetCells(letra2 + fila_first_concat, letra2 + (fila_first_concat + fila_a_concatenar));
		}
	}

	private void formatearFilaPrincipalHoja(SLDocument sl, string fecha)
	{
		sl.SetCellValue("A1", "REPORTE DE VENTAS DEL DIA " + fecha);
		sl.MergeWorksheetCells("A1", "K1");
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 2, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellStyle("A1", "K1", style);
		sl.SetCellStyle("A2", "K2", style);
		SLStyle style_center = sl.CreateStyle();
		style_center.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style_center.SetVerticalAlignment(VerticalAlignmentValues.Center);
		SLStyle vertical_centrado = sl.CreateStyle();
		vertical_centrado.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("A2", "ITEM");
		sl.SetColumnWidth(1, 5.0);
		sl.SetCellValue("B2", "FECHA");
		sl.SetColumnWidth(2, 14.0);
		sl.SetCellValue("C2", "DOC");
		sl.SetColumnWidth(3, 15.5);
		sl.SetCellValue("D2", "N° DOC");
		sl.SetColumnWidth(4, 15.2);
		sl.SetCellValue("E2", "CLIENTE");
		sl.SetColumnStyle(5, vertical_centrado);
		sl.SetColumnWidth(5, 25.0);
		sl.SetCellValue("F2", "METODO PAGO");
		sl.SetColumnWidth(6, 20.0);
		sl.SetColumnStyle(6, style_center);
		sl.SetCellValue("G2", "CANT.");
		sl.SetColumnWidth(7, 8.0);
		sl.SetColumnStyle(7, style_center);
		sl.SetCellValue("H2", "DESCRIPCION");
		sl.SetColumnWidth(8, 45.0);
		sl.SetCellValue("I2", "P.U");
		sl.SetColumnWidth(9, 12.0);
		sl.SetCellValue("J2", "SUB. TOTAL");
		sl.SetColumnWidth(10, 12.0);
		sl.SetCellValue("K2", "TOTAL");
		sl.SetColumnWidth(11, 12.0);
		sl.SetColumnStyle(11, vertical_centrado);
	}

	private void asignarBordes(SLStyle style)
	{
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.LeftBorder.Color = System.Drawing.Color.Black;
		style.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.TopBorder.Color = System.Drawing.Color.Black;
		style.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.RightBorder.Color = System.Drawing.Color.Black;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.BottomBorder.Color = System.Drawing.Color.Black;
	}

	private void formatearFilaSecundariaNotaCreditoHoja(SLDocument sl, int nro_fila_iniciador)
	{
		sl.SetCellValue("A" + nro_fila_iniciador, "NOTAS DE CREDITO");
		sl.MergeWorksheetCells("A" + nro_fila_iniciador, "C" + nro_fila_iniciador);
		SLStyle style = sl.CreateStyle();
		asignarBordes(style);
		style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(166, 166, 166), System.Drawing.Color.Yellow);
		sl.SetCellStyle("A" + nro_fila_iniciador, "K" + nro_fila_iniciador, style);
		SLStyle alineacion = sl.CreateStyle();
		alineacion.SetFontBold(IsBold: true);
		alineacion.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		alineacion.SetVerticalAlignment(VerticalAlignmentValues.Center);
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		asignarBordes(style);
		sl.SetCellStyle("B" + (nro_fila_iniciador + 1), style);
		sl.SetCellStyle("C" + (nro_fila_iniciador + 1), style);
		sl.SetCellStyle("D" + (nro_fila_iniciador + 1), style);
		sl.SetRowHeight(nro_fila_iniciador + 1, 33.0);
		sl.SetRowStyle(nro_fila_iniciador + 1, alineacion);
		SLStyle ajustar_a_texto = sl.CreateStyle();
		asignarBordes(ajustar_a_texto);
		ajustar_a_texto.SetWrapText(IsWrapped: true);
		ajustar_a_texto.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		ajustar_a_texto.SetVerticalAlignment(VerticalAlignmentValues.Center);
		SLStyle style2 = sl.CreateStyle();
		asignarBordes(style2);
		sl.SetCellValue("A" + (nro_fila_iniciador + 1), "ITEM");
		sl.SetCellStyle("A" + (nro_fila_iniciador + 1), style2);
		sl.SetCellValue("B" + (nro_fila_iniciador + 1), "FECHA DE NOTA CREDITO");
		sl.SetCellStyle("B" + (nro_fila_iniciador + 1), ajustar_a_texto);
		sl.SetCellValue("C" + (nro_fila_iniciador + 1), "N° NOTA DE CREDITO");
		sl.SetCellStyle("C" + (nro_fila_iniciador + 1), ajustar_a_texto);
		sl.SetCellValue("D" + (nro_fila_iniciador + 1), "FECHA DE DOC. REFERENCIA");
		sl.SetCellStyle("D" + (nro_fila_iniciador + 1), ajustar_a_texto);
		sl.SetCellValue("E" + (nro_fila_iniciador + 1), "DOC. REFERENCIA");
		sl.SetCellStyle("E" + (nro_fila_iniciador + 1), style2);
		sl.SetCellValue("F" + (nro_fila_iniciador + 1), "TIPO DE NOTA DE CREDITO");
		sl.SetCellStyle("F" + (nro_fila_iniciador + 1), ajustar_a_texto);
		sl.SetCellValue("G" + (nro_fila_iniciador + 1), "CANT.");
		sl.SetCellStyle("G" + (nro_fila_iniciador + 1), style2);
		sl.SetCellValue("H" + (nro_fila_iniciador + 1), "DESCRIPCION");
		style2.SetWrapText(IsWrapped: true);
		style2.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style2.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellStyle("H" + (nro_fila_iniciador + 1), style2);
		style2 = sl.CreateStyle();
		asignarBordes(style2);
		sl.SetCellValue("I" + (nro_fila_iniciador + 1), "P.U");
		sl.SetCellStyle("I" + (nro_fila_iniciador + 1), style2);
		sl.SetCellValue("J" + (nro_fila_iniciador + 1), "SUB. TOTAL");
		sl.SetCellStyle("J" + (nro_fila_iniciador + 1), style2);
		sl.SetCellValue("K" + (nro_fila_iniciador + 1), "TOTAL");
		sl.SetCellStyle("K" + (nro_fila_iniciador + 1), style2);
	}

	private string obtenerRutaParaGuardar(string namefile = "Exportacion_Reporte_Venta_Diaria")
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
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Ventas Diarias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private bool validaInputs()
	{
		bool band = true;
		_ = dtpinicio.Value;
		if (false)
		{
			band = false;
		}
		_ = dtpfinal.Value;
		if (false)
		{
			band = false;
		}
		if (dtpinicio.Value.Date > dtpfinal.Value.Date)
		{
			band = false;
		}
		return band;
	}

	private void reporteventasdiarias_Load(object sender, EventArgs e)
	{
		try
		{
			dtpinicio.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
			dtpfinal.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public float[] GetHeaderWidths(iTextSharp.text.Font font, params string[] headers)
	{
		int total = 0;
		int columns = headers.Length;
		int[] widths = new int[columns];
		for (int i = 0; i < columns; i++)
		{
			int w = font.GetCalculatedBaseFont(specialEncoding: true).GetWidth(headers[i]);
			total += w;
			widths[i] = w;
		}
		float[] result = new float[columns];
		for (int j = 0; j < columns; j++)
		{
			result[j] = (float)widths[j] / (float)total * 100f;
		}
		return result;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.reporteventasdiarias2));
		this.btnReporte = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpinicio = new Telerik.WinControls.UI.RadDateTimePicker();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.dtpfinal = new Telerik.WinControls.UI.RadDateTimePicker();
		((System.ComponentModel.ISupportInitialize)this.dtpinicio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpfinal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnReporte.BackColor = System.Drawing.Color.White;
		this.btnReporte.Image = (System.Drawing.Image)resources.GetObject("btnReporte.Image");
		this.btnReporte.Location = new System.Drawing.Point(160, 136);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(97, 37);
		this.btnReporte.TabIndex = 3;
		this.btnReporte.Text = "Generar Excel";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(59, 34);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 13);
		this.label1.TabIndex = 54;
		this.label1.Text = "Fecha Inicio:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(292, 34);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 13);
		this.label2.TabIndex = 54;
		this.label2.Text = "Fecha Final:";
		this.dtpinicio.CalendarSize = new System.Drawing.Size(300, 300);
		this.dtpinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpinicio.Location = new System.Drawing.Point(12, 66);
		this.dtpinicio.Name = "dtpinicio";
		this.dtpinicio.Size = new System.Drawing.Size(183, 32);
		this.dtpinicio.TabIndex = 56;
		this.dtpinicio.TabStop = false;
		this.dtpinicio.Text = "11/07/2022";
		this.dtpinicio.ThemeName = "TelerikMetroTouch";
		this.dtpinicio.Value = new System.DateTime(2022, 7, 11, 19, 32, 10, 469);
		this.dtpfinal.CalendarSize = new System.Drawing.Size(300, 300);
		this.dtpfinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfinal.Location = new System.Drawing.Point(238, 66);
		this.dtpfinal.Name = "dtpfinal";
		this.dtpfinal.Size = new System.Drawing.Size(183, 32);
		this.dtpfinal.TabIndex = 57;
		this.dtpfinal.TabStop = false;
		this.dtpfinal.Text = "11/07/2022";
		this.dtpfinal.ThemeName = "TelerikMetroTouch";
		this.dtpfinal.Value = new System.DateTime(2022, 7, 11, 19, 32, 10, 469);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(426, 199);
		base.Controls.Add(this.dtpfinal);
		base.Controls.Add(this.dtpinicio);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnReporte);
		base.Name = "reporteventasdiarias2";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte Venta Diaria";
		base.ThemeName = "TelerikMetroTouch";
		base.Load += new System.EventHandler(reporteventasdiarias_Load);
		((System.ComponentModel.ISupportInitialize)this.dtpinicio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpfinal).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
