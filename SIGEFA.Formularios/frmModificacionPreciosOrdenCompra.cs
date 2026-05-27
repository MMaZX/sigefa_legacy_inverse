using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmModificacionPreciosOrdenCompra : Form
{
	public int codOrdenCompra = 0;

	private clsAdmOrdenCompra admOrdenCompra = new clsAdmOrdenCompra();

	private clsAdmProducto admProducto = new clsAdmProducto();

	private clsAdmAcceso admAcces = new clsAdmAcceso();

	private Color[] colores_columnas = new Color[3]
	{
		Color.MediumAquamarine,
		Color.LightPink,
		Color.Gold
	};

	private int i = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadGridView rgvListado;

	private GroupBox groupBox2;

	public Button lblusuario;

	public Button btnUpdatePrecioCompra;

	public Button btnUpdateFleteNuevo;

	private Label lblOrdenCompra;

	public Label lblDescripcionOrdenCompra;

	public Button btnActualizarLIsta;

	private RadGridView radGridView1;

	public Button btnrevisado;

	public Button btnseteado;

	public frmModificacionPreciosOrdenCompra()
	{
		InitializeComponent();
	}

	private void frmModificacionPreciosOrdenCompra_Load(object sender, EventArgs e)
	{
		listar();
	}

	private void calcularValoresColumnas()
	{
		try
		{
			foreach (GridViewRowInfo fila in rgvListado.Rows)
			{
				double flete = obtenerDouble(fila.Cells["colFlete"].Value);
				double utilbruact = obtenerDouble(fila.Cells["colUtilidadBrutaActual"].Value);
				double utilbruactp = obtenerDouble(fila.Cells["colUtilidadBrutaActualP"].Value) * 100.0;
				double pcactsigv = obtenerDouble(fila.Cells["colPCActualSinIgv"].Value);
				double pcnuevo = obtenerDouble(fila.Cells["colPCNuevo"].Value);
				double pcactcigv = obtenerDouble(fila.Cells["colPCActualConIgv"].Value);
				double pvfinal = obtenerDouble(fila.Cells["colPVFinal"].Value);
				double fletenuevo = obtenerDouble(fila.Cells["colFleteNuevo"].Value);
				fila.Cells["colUtilOperActual"].Value = Math.Round(utilbruact - flete, 4);
				fila.Cells["colUtilOperActualP"].Value = Math.Round((utilbruact - flete) / pcactsigv, 4);
				fila.Cells["colPVSug"].Value = Math.Round(pcnuevo * utilbruactp, 4);
				fila.Cells["colVariacionPC"].Value = Math.Round(pcnuevo - pcactcigv, 4);
				if (pvfinal != 0.0 && pcnuevo != 0.0)
				{
					double utilidad_bruta = pvfinal / 1.18 - pcnuevo / 1.18;
					fila.Cells["colUtilBrutaFinal"].Value = Math.Round(utilidad_bruta, 4);
					fila.Cells["colUtilBrutaFinalP"].Value = Math.Round(pvfinal / pcnuevo, 4);
					fila.Cells["colUtilOperFinal"].Value = Math.Round(utilidad_bruta - fletenuevo, 4);
					fila.Cells["colUtilOperFinal%"].Value = Math.Round((utilidad_bruta - fletenuevo) / (pcnuevo / 1.18), 4);
				}
				else
				{
					fila.Cells["colUtilBrutaFinal"].Value = null;
					fila.Cells["colUtilBrutaFinalP"].Value = null;
					fila.Cells["colUtilOperFinal"].Value = null;
					fila.Cells["colUtilOperFinal%"].Value = null;
				}
				if (pvfinal != 0.0)
				{
					double pvactcigv = obtenerDouble(fila.Cells["colPVActualConIgv"].Value);
					fila.Cells["colVariacionPV"].Value = Math.Round(pvfinal - pvactcigv, 4);
				}
				else
				{
					fila.Cells["colVariacionPV"].Value = null;
				}
				if (pcnuevo != 0.0)
				{
					fila.Cells["colPVSug"].Value = Math.Round(pcnuevo * utilbruactp, 4);
					fila.Cells["colVariacionPC"].Value = Math.Round(pcnuevo - pcactcigv, 4);
				}
				else
				{
					fila.Cells["colPVSug"].Value = null;
					fila.Cells["colVariacionPC"].Value = null;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void calcularValoresColumnas1()
	{
		try
		{
			double pcnuevo = 0.0;
			foreach (GridViewRowInfo fila in radGridView1.Rows)
			{
				pcnuevo = obtenerDouble(fila.Cells["precio_compra_nuevo_sol"].Value);
				double pv_catalogo = obtenerDouble(fila.Cells["precio_venta_catalogo"].Value);
				double pvfinal = obtenerDouble(fila.Cells["precio_venta_final"].Value);
				double utilidad_ope_final_sol = obtenerDouble(fila.Cells["utilidad_ope_final_sol"].Value);
				double utilidad_ope_final_por = obtenerDouble(fila.Cells["utilidad_ope_final_porcentaje"].Value);
				double util_bruta_final_sol = obtenerDouble(fila.Cells["utilidad_bruta_sol"].Value);
				double util_bruta_final_por = obtenerDouble(fila.Cells["utilidad_bruta_porcen"].Value);
				double util_bruta_ant_sol = obtenerDouble(fila.Cells["ub_anterior_soles"].Value);
				double fletenuevo = obtenerDouble(fila.Cells["flete_nuevo"].Value);
				double desastivo_otros = obtenerDouble(fila.Cells["desestiva_otros"].Value);
				double variacion_PV = obtenerDouble(fila.Cells["variacion_pv"].Value);
				double pedido = obtenerDouble(fila.Cells["pedido"].Value);
				double flete_xped_sinigv = obtenerDouble(fila.Cells["flete_xpedido_sinigv"].Value);
				double flete_xped_conigv = obtenerDouble(fila.Cells["flete_xpedido_conigv"].Value);
				util_bruta_final_sol = pvfinal / 1.18 - pcnuevo / 1.18;
				utilidad_ope_final_sol = util_bruta_final_sol - fletenuevo - desastivo_otros;
				utilidad_ope_final_por = (double.IsNaN(utilidad_ope_final_sol / (pcnuevo / 1.18)) ? 0.0 : (utilidad_ope_final_sol / (pcnuevo / 1.18)));
				util_bruta_final_por = (double.IsNaN(util_bruta_final_sol / (pcnuevo / 1.18)) ? 0.0 : (util_bruta_final_sol / (pcnuevo / 1.18)));
				variacion_PV = pvfinal - pv_catalogo;
				flete_xped_sinigv = fletenuevo * pedido;
				flete_xped_conigv = Math.Round(flete_xped_sinigv * 1.18, 2);
				fila.Cells["precio_venta_final_sinigv"].Value = Math.Round(pvfinal / 1.18, 4);
				fila.Cells["utilidad_ope_final_sol"].Value = Math.Round(utilidad_ope_final_sol, 4);
				fila.Cells["utilidad_ope_final_porcentaje"].Value = Math.Round(utilidad_ope_final_por, 4);
				fila.Cells["utilidad_bruta_sol"].Value = Math.Round(util_bruta_final_sol, 4);
				fila.Cells["utilidad_bruta_porcen"].Value = Math.Round(util_bruta_final_por, 4);
				fila.Cells["variacion_pv"].Value = Math.Round(variacion_PV, 4);
				fila.Cells["flete_xpedido_sinigv"].Value = Math.Round(flete_xped_sinigv, 4);
				fila.Cells["flete_xpedido_conigv"].Value = Math.Round(flete_xped_conigv, 4);
				fila.Cells["prec_compra_nuevo_sinigv"].Value = Math.Round(pcnuevo / 1.18, 4);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private double obtenerDouble(object value, double defecto = 0.0)
	{
		if (value == DBNull.Value || value == null)
		{
			return defecto;
		}
		if (value.ToString().Trim() == "")
		{
			return defecto;
		}
		if (double.TryParse(value.ToString(), out var aux))
		{
			return aux;
		}
		return defecto;
	}

	private void rgvListado_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex >= 0 && (e.Column.Name == "colPVFinal" || e.Column.Name == "colPCNuevo" || e.Column.Name == "colFleteNuevo"))
			{
				double pvfinal = obtenerDouble(e.Row.Cells["colPVFinal"].Value);
				double pcnuevo = obtenerDouble(e.Row.Cells["colPCNuevo"].Value);
				double fletenuevo = obtenerDouble(e.Row.Cells["colFleteNuevo"].Value);
				if (pvfinal != 0.0 && pcnuevo != 0.0)
				{
					double utilidad_bruta = pvfinal / 1.18 - pcnuevo / 1.18;
					rgvListado.Rows[e.RowIndex].Cells["colUtilBrutaFinal"].Value = Math.Round(utilidad_bruta, 4);
					rgvListado.Rows[e.RowIndex].Cells["colUtilBrutaFinalP"].Value = Math.Round(pvfinal / pcnuevo, 4);
					rgvListado.Rows[e.RowIndex].Cells["colUtilOperFinal"].Value = Math.Round(utilidad_bruta - fletenuevo, 4);
					rgvListado.Rows[e.RowIndex].Cells["colUtilOperFinal%"].Value = Math.Round((utilidad_bruta - fletenuevo) / (pcnuevo / 1.18), 4);
				}
				else
				{
					rgvListado.Rows[e.RowIndex].Cells["colUtilBrutaFinal"].Value = null;
					rgvListado.Rows[e.RowIndex].Cells["colUtilBrutaFinalP"].Value = null;
					rgvListado.Rows[e.RowIndex].Cells["colUtilOperFinal"].Value = null;
					rgvListado.Rows[e.RowIndex].Cells["colUtilOperFinal%"].Value = null;
				}
				if (pvfinal != 0.0)
				{
					double pvactcigv = obtenerDouble(e.Row.Cells["colPVActualConIgv"].Value);
					rgvListado.Rows[e.RowIndex].Cells["colVariacionPV"].Value = Math.Round(pvfinal - pvactcigv, 4);
				}
				else
				{
					rgvListado.Rows[e.RowIndex].Cells["colVariacionPV"].Value = null;
				}
				if (pcnuevo != 0.0)
				{
					double utilbruactp = obtenerDouble(e.Row.Cells["colUtilidadBrutaActualP"].Value) * 100.0;
					double pcactcigv = obtenerDouble(e.Row.Cells["colPCActualConIgv"].Value);
					rgvListado.Rows[e.RowIndex].Cells["colPVSug"].Value = Math.Round(pcnuevo * utilbruactp, 4);
					rgvListado.Rows[e.RowIndex].Cells["colVariacionPC"].Value = Math.Round(pcnuevo - pcactcigv, 4);
				}
				else
				{
					rgvListado.Rows[e.RowIndex].Cells["colPVSug"].Value = null;
					rgvListado.Rows[e.RowIndex].Cells["colVariacionPC"].Value = null;
				}
				if (fletenuevo != 0.0 || pvfinal != 0.0 || pcnuevo != 0.0)
				{
					DetalleModificarPrecio obj = new DetalleModificarPrecio
					{
						codProducto = Convert.ToInt32(rgvListado.Rows[e.RowIndex].Cells["colCodProducto"].Value),
						codUnidadMedida = Convert.ToInt32(rgvListado.Rows[e.RowIndex].Cells["colCodUnidadMedida"].Value),
						codUnidadEquivalente = Convert.ToInt32(rgvListado.Rows[e.RowIndex].Cells["colCodUnidadEquivalente"].Value),
						fleteNuevo = fletenuevo,
						precioCompraNuevo = pcnuevo,
						precioVentaNuevo = pvfinal
					};
					admOrdenCompra.enviarDatoModificarPrecio(obj);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void pintarColumnas()
	{
		GridViewColumn col = rgvListado.Columns["colPCNuevo"];
		i = 1;
		ConditionalFormattingObject c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		col.ConditionalFormattingObjectList.Add(c1);
		col.ConditionalFormattingObjectList.Add(c2);
		col.ConditionalFormattingObjectList.Add(c3);
		col = rgvListado.Columns["colPVFinal"];
		i = 0;
		c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		col.ConditionalFormattingObjectList.Add(c1);
		col.ConditionalFormattingObjectList.Add(c2);
		col.ConditionalFormattingObjectList.Add(c3);
		i = 2;
		c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		col = rgvListado.Columns["colFleteNuevo"];
		col.ConditionalFormattingObjectList.Add(c1);
		col.ConditionalFormattingObjectList.Add(c2);
		col.ConditionalFormattingObjectList.Add(c3);
	}

	public void pintarColumnasa1()
	{
		GridViewColumn col = radGridView1.Columns["precio_compra_nuevo_sol"];
		i = 1;
		ConditionalFormattingObject c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		col.ConditionalFormattingObjectList.Add(c1);
		col.ConditionalFormattingObjectList.Add(c2);
		col.ConditionalFormattingObjectList.Add(c3);
		col = radGridView1.Columns["precio_venta_final"];
		i = 0;
		c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		col.ConditionalFormattingObjectList.Add(c1);
		col.ConditionalFormattingObjectList.Add(c2);
		col.ConditionalFormattingObjectList.Add(c3);
		i = 2;
		c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		col = radGridView1.Columns["flete_nuevo"];
		col.ConditionalFormattingObjectList.Add(c1);
		col.ConditionalFormattingObjectList.Add(c2);
		col.ConditionalFormattingObjectList.Add(c3);
	}

	private void btnUpdateFleteNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			DialogResult dr1 = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			dr1 = frm.ShowDialog();
			if (dr1 != DialogResult.OK)
			{
				return;
			}
			int contador = 0;
			foreach (GridViewRowInfo fila in radGridView1.Rows)
			{
				double valor = obtenerDouble(fila.Cells["flete_nuevo"].Value);
				if (valor != 0.0)
				{
					contador++;
					int codProd = Convert.ToInt32(fila.Cells["colCodProducto"].Value);
					admProducto.cambiarFleteDeProducto(codProd, valor);
				}
			}
			MessageBox.Show("Se actualizo el flete de " + contador + " de " + rgvListado.Rows.Count + " filas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnUpdatePrecioCompra_Click(object sender, EventArgs e)
	{
		try
		{
			DialogResult dr1 = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			dr1 = frm.ShowDialog();
			if (dr1 != DialogResult.OK)
			{
				return;
			}
			int contador = 0;
			foreach (GridViewRowInfo fila in radGridView1.Rows)
			{
				double valor = obtenerDouble(fila.Cells["precio_compra_nuevo_sol"].Value);
				if (valor != 0.0)
				{
					contador++;
					int codEquiv = Convert.ToInt32(fila.Cells["colCodUnidadEquiCompra"].Value);
					admProducto.updateunidadequivalente(codEquiv, Convert.ToDecimal(valor));
				}
			}
			MessageBox.Show("Se actualizo el precio de compra de " + contador + " de " + rgvListado.Rows.Count + " filas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnUpdatePrecioVenta_Click(object sender, EventArgs e)
	{
		try
		{
			DialogResult dr1 = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			dr1 = frm.ShowDialog();
			if (dr1 != DialogResult.OK)
			{
				return;
			}
			int contador = 0;
			foreach (GridViewRowInfo fila in radGridView1.Rows)
			{
				double valor = obtenerDouble(fila.Cells["precio_venta_final"].Value);
				if (valor != 0.0)
				{
					contador++;
					int codEquiv = Convert.ToInt32(fila.Cells["colCodUnidadEquivalente"].Value);
					admProducto.updateunidadequivalente(codEquiv, Convert.ToDecimal(valor));
				}
			}
			MessageBox.Show("Se actualizo el precio de venta de " + contador + " de " + rgvListado.Rows.Count + " filas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void rgvListado_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex >= 0 && e.Column.Name == "colRefereciasProd")
			{
				frmReferenciasExternasProducto frm = new frmReferenciasExternasProducto();
				frm.codProducto = Convert.ToInt32(e.Row.Cells["colCodProducto"].Value.ToString());
				frm.codUnidadMedida = Convert.ToInt32(e.Row.Cells["colCodUnidadMedida"].Value.ToString());
				frm.lblProducto.Text = e.Row.Cells["colReferencia"].Value.ToString() + " - " + e.Row.Cells["colDescripProducto"].Value.ToString();
				frm.lblUnidad.Text = e.Row.Cells["colDescripUnidadMedida"].Value.ToString();
				frm.ShowDialog();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnActualizarLIsta_Click(object sender, EventArgs e)
	{
		listar();
	}

	private void listar()
	{
		try
		{
			radGridView1.DataSource = admOrdenCompra.ListaProductosModificarPrecioA_1(codOrdenCompra, frmLogin.iCodEmpresa);
			pintarColumnasa1();
			calcularValoresColumnas1();
			radGridView1.Columns["skucompetencia"].ReadOnly = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void radGridView1_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex >= 0)
			{
				double pvfinal = obtenerDouble(e.Row.Cells["precio_venta_final"].Value);
				double pcnuevo = obtenerDouble(e.Row.Cells["precio_compra_nuevo_sol"].Value);
				double fletenuevo = obtenerDouble(e.Row.Cells["flete_nuevo"].Value);
				double pv_competencia = obtenerDouble(e.Row.Cells["precioventa_ref_competencia"].Value);
				string pv_sku = Convert.ToString(e.Row.Cells["skucompetencia"].Value);
				string link = Convert.ToString(e.Row.Cells["link"].Value);
				DetalleModificarPrecio obj = new DetalleModificarPrecio
				{
					codProducto = Convert.ToInt32(radGridView1.Rows[e.RowIndex].Cells["colCodProducto"].Value),
					codUnidadMedida = Convert.ToInt32(radGridView1.Rows[e.RowIndex].Cells["colCodUnidadMedida"].Value),
					codUnidadEquivalente = Convert.ToInt32(radGridView1.Rows[e.RowIndex].Cells["colCodUnidadEquivalente"].Value),
					codordencompra = Convert.ToInt32(lblDescripcionOrdenCompra.Text.Split('-')[1])
				};
				if (e.Column.Name == "precio_venta_final")
				{
					obj.precioVentaNuevo = pvfinal;
					admOrdenCompra.enviarDatoModificarPrecio_precioventanuevo(obj);
				}
				if (e.Column.Name == "precio_compra_nuevo_sol")
				{
					obj.precioCompraNuevo = pcnuevo;
					admOrdenCompra.enviarDatoModificarPrecio_preciocompranuevo(obj);
				}
				if (e.Column.Name == "flete_nuevo")
				{
					obj.fleteNuevo = fletenuevo;
					admOrdenCompra.enviarDatoModificarPrecio_fletenuevo(obj);
				}
				if (e.Column.Name == "precioventa_ref_competencia")
				{
					obj.precioVenta_competencia = pv_competencia;
					admOrdenCompra.enviarDatoModificarPrecio_precioventa_competencia(obj);
				}
				if (e.Column.Name == "skucompetencia")
				{
					obj.precioVenta_sku = pv_sku;
					admOrdenCompra.enviarDatoModificarPrecio_precioventa_SKU(obj);
				}
				if (e.Column.Name == "link")
				{
					obj.link_competencia = link;
					admOrdenCompra.enviarDatoModificarPrecio_precioventa_link(obj);
				}
				calcularValoresColumnas1();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnguardar_estado_Click(object sender, EventArgs e)
	{
		try
		{
			clsUsuario obj = new clsUsuario();
			DialogResult dr1 = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			dr1 = frm.ShowDialog();
			if (dr1 != DialogResult.OK)
			{
				return;
			}
			string coduser = frm.user.ToString();
			List<int> tablaa = admAcces.MuestraAccesos(Convert.ToInt32(coduser), frmLogin.iCodAlmacen);
			if (tablaa.Contains(173))
			{
				string str = lblDescripcionOrdenCompra.Text;
				str = str.Remove(0, 7);
				foreach (GridViewRowInfo row in radGridView1.Rows)
				{
					int codProd = Convert.ToInt32(row.Cells["colCodProducto"].Value.ToString());
					admOrdenCompra.ActualizaDetOrdenCompra_Estado(Convert.ToInt32(str), codProd, 19);
				}
				MessageBox.Show("La orden de compra cambio a estado revisado", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Question);
				admOrdenCompra.ActualizaOrdenCompra_Estado(Convert.ToInt32(str), 19);
			}
			else
			{
				MessageBox.Show("El usuario ingresado no tiene acceso para esta accion", "ACCESO", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
	{
	}

	private void radGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
	{
		try
		{
			if ((int)radGridView1.CurrentRow.Cells["Id_etiqueta"].Value == 20)
			{
				e.Cancel = true;
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnseteado_Click(object sender, EventArgs e)
	{
		try
		{
			clsUsuario obj = new clsUsuario();
			DialogResult dr1 = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			dr1 = frm.ShowDialog();
			if (dr1 != DialogResult.OK)
			{
				return;
			}
			string coduser = frm.user.ToString();
			List<int> tablaa = admAcces.MuestraAccesos(Convert.ToInt32(coduser), frmLogin.iCodAlmacen);
			if (tablaa.Contains(172))
			{
				string str = lblDescripcionOrdenCompra.Text;
				str = str.Remove(0, 7);
				foreach (GridViewRowInfo fila in radGridView1.Rows)
				{
					int codProd1 = Convert.ToInt32(fila.Cells["colCodProducto"].Value.ToString());
					admOrdenCompra.ActualizaDetOrdenCompra_Estado(Convert.ToInt32(str), codProd1, 20);
					admOrdenCompra.ActualizaOrdenCompra_Estado(Convert.ToInt32(str), 20);
				}
				MessageBox.Show("Los cambios de precios se realizaron correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("El usuario ingresado no tiene acceso para esta accion", "ACCESO", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn33 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn34 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn35 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn36 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn37 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn38 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn39 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn40 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn41 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn42 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn27 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn43 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn44 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn45 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn46 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn47 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn48 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn49 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn50 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn51 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn52 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn28 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn29 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn30 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn31 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn32 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn33 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn34 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn35 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn36 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn37 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn38 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn53 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn54 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn55 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn56 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn57 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn58 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn59 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn60 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn61 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn62 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn63 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn64 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn65 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn66 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn67 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn68 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn69 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn70 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn71 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn72 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn73 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn74 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn75 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn39 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn76 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn77 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn78 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn79 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn80 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn81 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn40 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn41 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewHyperlinkColumn gridViewHyperlinkColumn1 = new Telerik.WinControls.UI.GridViewHyperlinkColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn82 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn83 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn84 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn42 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn43 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvListado = new Telerik.WinControls.UI.RadGridView();
		this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnseteado = new System.Windows.Forms.Button();
		this.btnrevisado = new System.Windows.Forms.Button();
		this.btnActualizarLIsta = new System.Windows.Forms.Button();
		this.lblDescripcionOrdenCompra = new System.Windows.Forms.Label();
		this.lblOrdenCompra = new System.Windows.Forms.Label();
		this.btnUpdateFleteNuevo = new System.Windows.Forms.Button();
		this.lblusuario = new System.Windows.Forms.Button();
		this.btnUpdatePrecioCompra = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvListado).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListado.MasterTemplate).BeginInit();
		this.rgvListado.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radGridView1.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvListado);
		this.groupBox1.Location = new System.Drawing.Point(0, 89);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1503, 518);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.rgvListado.AutoScroll = true;
		this.rgvListado.AutoSizeRows = true;
		this.rgvListado.Controls.Add(this.radGridView1);
		this.rgvListado.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvListado.EnterKeyMode = Telerik.WinControls.UI.RadGridViewEnterKeyMode.EnterMovesToNextRow;
		this.rgvListado.Location = new System.Drawing.Point(3, 16);
		this.rgvListado.MasterTemplate.AllowAddNewRow = false;
		this.rgvListado.MasterTemplate.AllowColumnReorder = false;
		this.rgvListado.MasterTemplate.AllowDeleteRow = false;
		this.rgvListado.MasterTemplate.AllowDragToGroup = false;
		this.rgvListado.MasterTemplate.ClipboardCutMode = Telerik.WinControls.UI.GridViewClipboardCutMode.Disable;
		gridViewTextBoxColumn17.FieldName = "nroItem";
		gridViewTextBoxColumn17.HeaderText = "Item";
		gridViewTextBoxColumn17.IsPinned = true;
		gridViewTextBoxColumn17.Name = "colNroItem";
		gridViewTextBoxColumn17.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn17.ReadOnly = true;
		gridViewTextBoxColumn17.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn18.FieldName = "codProducto";
		gridViewTextBoxColumn18.HeaderText = "codProducto";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "colCodProducto";
		gridViewTextBoxColumn18.ReadOnly = true;
		gridViewTextBoxColumn19.FieldName = "referencia";
		gridViewTextBoxColumn19.HeaderText = "Referencia";
		gridViewTextBoxColumn19.IsPinned = true;
		gridViewTextBoxColumn19.Name = "colReferencia";
		gridViewTextBoxColumn19.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn19.ReadOnly = true;
		gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn19.Width = 74;
		gridViewTextBoxColumn20.FieldName = "descripcionProducto";
		gridViewTextBoxColumn20.HeaderText = "Producto";
		gridViewTextBoxColumn20.IsPinned = true;
		gridViewTextBoxColumn20.Name = "colDescripProducto";
		gridViewTextBoxColumn20.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn20.ReadOnly = true;
		gridViewTextBoxColumn20.Width = 340;
		gridViewTextBoxColumn20.WrapText = true;
		gridViewTextBoxColumn21.FieldName = "codUnidadMedida";
		gridViewTextBoxColumn21.HeaderText = "codUnidadMedida";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "colCodUnidadMedida";
		gridViewTextBoxColumn21.ReadOnly = true;
		gridViewTextBoxColumn22.FieldName = "descripcionUnidadMedida";
		gridViewTextBoxColumn22.HeaderText = "Unidad de Medida";
		gridViewTextBoxColumn22.IsPinned = true;
		gridViewTextBoxColumn22.Name = "colDescripUnidadMedida";
		gridViewTextBoxColumn22.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn22.ReadOnly = true;
		gridViewTextBoxColumn22.Width = 116;
		gridViewTextBoxColumn23.FieldName = "codUnidadEquivalente";
		gridViewTextBoxColumn23.HeaderText = "codUnidadEquivalente";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "colCodUnidadEquivalente";
		gridViewTextBoxColumn23.ReadOnly = true;
		gridViewTextBoxColumn24.FieldName = "descripcion_precio";
		gridViewTextBoxColumn24.HeaderText = "Tipo Precio";
		gridViewTextBoxColumn24.IsPinned = true;
		gridViewTextBoxColumn24.Name = "colTipoPrecio";
		gridViewTextBoxColumn24.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn24.ReadOnly = true;
		gridViewTextBoxColumn24.Width = 167;
		gridViewDecimalColumn33.FieldName = "presentacion";
		gridViewDecimalColumn33.HeaderText = "Presentacion";
		gridViewDecimalColumn33.Name = "colPresentacion";
		gridViewDecimalColumn33.ReadOnly = true;
		gridViewDecimalColumn33.Width = 84;
		gridViewTextBoxColumn25.FieldName = "codOrdenCompra";
		gridViewTextBoxColumn25.HeaderText = "codOrdenCompra";
		gridViewTextBoxColumn25.IsVisible = false;
		gridViewTextBoxColumn25.Name = "colCodOrdenCompra";
		gridViewTextBoxColumn25.ReadOnly = true;
		gridViewTextBoxColumn26.FieldName = "orden_compra";
		gridViewTextBoxColumn26.HeaderText = "Orden Compra";
		gridViewTextBoxColumn26.Name = "colDecOrdenCompra";
		gridViewTextBoxColumn26.ReadOnly = true;
		gridViewTextBoxColumn26.Width = 108;
		gridViewDecimalColumn34.DecimalPlaces = 4;
		gridViewDecimalColumn34.FieldName = "precio_compra_actual_con_igv";
		gridViewDecimalColumn34.HeaderText = "PC Actual Con IGV";
		gridViewDecimalColumn34.Name = "colPCActualConIgv";
		gridViewDecimalColumn34.ReadOnly = true;
		gridViewDecimalColumn34.Width = 70;
		gridViewDecimalColumn34.WrapText = true;
		gridViewDecimalColumn35.DecimalPlaces = 4;
		gridViewDecimalColumn35.FieldName = "precio_compra_actual_sin_igv";
		gridViewDecimalColumn35.HeaderText = "PC Actual Sin IGV";
		gridViewDecimalColumn35.IsVisible = false;
		gridViewDecimalColumn35.Name = "colPCActualSinIgv";
		gridViewDecimalColumn35.ReadOnly = true;
		gridViewDecimalColumn35.Width = 70;
		gridViewDecimalColumn35.WrapText = true;
		gridViewDecimalColumn36.DecimalPlaces = 4;
		gridViewDecimalColumn36.FieldName = "precio_venta_actual_con_igv";
		gridViewDecimalColumn36.HeaderText = "PV Actual Con IGV";
		gridViewDecimalColumn36.Name = "colPVActualConIgv";
		gridViewDecimalColumn36.ReadOnly = true;
		gridViewDecimalColumn36.Width = 70;
		gridViewDecimalColumn36.WrapText = true;
		gridViewDecimalColumn37.FieldName = "utilidad_bruta_actual_s";
		gridViewDecimalColumn37.HeaderText = "Utilidad Bruta Actual";
		gridViewDecimalColumn37.Name = "colUtilidadBrutaActual";
		gridViewDecimalColumn37.ReadOnly = true;
		gridViewDecimalColumn37.Width = 80;
		gridViewDecimalColumn37.WrapText = true;
		gridViewDecimalColumn38.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Percent;
		gridViewDecimalColumn38.FieldName = "utilidad_bruta_actual_p";
		gridViewDecimalColumn38.FormatString = "{0:#0.00%}";
		gridViewDecimalColumn38.HeaderText = "Utilidad Bruta Actual %";
		gridViewDecimalColumn38.Name = "colUtilidadBrutaActualP";
		gridViewDecimalColumn38.ReadOnly = true;
		gridViewDecimalColumn38.Width = 80;
		gridViewDecimalColumn38.WrapText = true;
		gridViewDecimalColumn39.DecimalPlaces = 4;
		gridViewDecimalColumn39.FieldName = "flete";
		gridViewDecimalColumn39.HeaderText = "Flete";
		gridViewDecimalColumn39.Name = "colFlete";
		gridViewDecimalColumn39.ReadOnly = true;
		gridViewDecimalColumn40.FieldName = "desestiva";
		gridViewDecimalColumn40.HeaderText = "Desestiva";
		gridViewDecimalColumn40.Name = "desestiva";
		gridViewDecimalColumn40.Width = 80;
		gridViewDecimalColumn41.HeaderText = "Utilidad Oper. Actual";
		gridViewDecimalColumn41.Name = "colUtilOperActual";
		gridViewDecimalColumn41.ReadOnly = true;
		gridViewDecimalColumn41.Width = 70;
		gridViewDecimalColumn41.WrapText = true;
		gridViewDecimalColumn42.FormatString = "{0:#0.00%}";
		gridViewDecimalColumn42.HeaderText = "Utilidad Oper. Actual %";
		gridViewDecimalColumn42.Name = "colUtilOperActualP";
		gridViewDecimalColumn42.ReadOnly = true;
		gridViewDecimalColumn42.Width = 70;
		gridViewDecimalColumn42.WrapText = true;
		gridViewTextBoxColumn27.FieldName = "codUnidadEquiCompra";
		gridViewTextBoxColumn27.HeaderText = "codUnidadEquiCompra";
		gridViewTextBoxColumn27.IsVisible = false;
		gridViewTextBoxColumn27.Name = "colCodUnidadEquiCompra";
		gridViewTextBoxColumn27.ReadOnly = true;
		gridViewDecimalColumn43.FieldName = "precio_compra_nuevo";
		gridViewDecimalColumn43.HeaderText = "PC Nuevo";
		gridViewDecimalColumn43.Name = "colPCNuevo";
		gridViewDecimalColumn43.Width = 70;
		gridViewDecimalColumn43.WrapText = true;
		gridViewDecimalColumn44.FieldName = "precio_venta_final";
		gridViewDecimalColumn44.HeaderText = "PV Final";
		gridViewDecimalColumn44.Name = "colPVFinal";
		gridViewDecimalColumn44.Width = 70;
		gridViewDecimalColumn45.HeaderText = "Utilidad Bruta Final";
		gridViewDecimalColumn45.Name = "colUtilBrutaFinal";
		gridViewDecimalColumn45.ReadOnly = true;
		gridViewDecimalColumn45.Width = 75;
		gridViewDecimalColumn45.WrapText = true;
		gridViewDecimalColumn46.FormatString = "{0:#0.00%}";
		gridViewDecimalColumn46.HeaderText = "Utilidad Bruta Final %";
		gridViewDecimalColumn46.Name = "colUtilBrutaFinalP";
		gridViewDecimalColumn46.ReadOnly = true;
		gridViewDecimalColumn46.Width = 75;
		gridViewDecimalColumn46.WrapText = true;
		gridViewDecimalColumn47.FieldName = "flete_nuevo";
		gridViewDecimalColumn47.HeaderText = "Flete Nuevo";
		gridViewDecimalColumn47.Name = "colFleteNuevo";
		gridViewDecimalColumn47.Width = 80;
		gridViewDecimalColumn48.HeaderText = "Utilidad Oper. Final";
		gridViewDecimalColumn48.Name = "colUtilOperFinal";
		gridViewDecimalColumn48.ReadOnly = true;
		gridViewDecimalColumn48.Width = 73;
		gridViewDecimalColumn48.WrapText = true;
		gridViewDecimalColumn49.FormatString = "{0:#0.00%}";
		gridViewDecimalColumn49.HeaderText = "Utilidad Oper. Final %";
		gridViewDecimalColumn49.Name = "colUtilOperFinal%";
		gridViewDecimalColumn49.ReadOnly = true;
		gridViewDecimalColumn49.Width = 73;
		gridViewDecimalColumn49.WrapText = true;
		gridViewDecimalColumn50.HeaderText = "PV Sug.";
		gridViewDecimalColumn50.Name = "colPVSug";
		gridViewDecimalColumn50.ReadOnly = true;
		gridViewDecimalColumn50.Width = 70;
		gridViewDecimalColumn51.HeaderText = "Variacion PC";
		gridViewDecimalColumn51.Name = "colVariacionPC";
		gridViewDecimalColumn51.ReadOnly = true;
		gridViewDecimalColumn51.Width = 70;
		gridViewDecimalColumn51.WrapText = true;
		gridViewDecimalColumn52.HeaderText = "Variacion PV";
		gridViewDecimalColumn52.Name = "colVariacionPV";
		gridViewDecimalColumn52.ReadOnly = true;
		gridViewDecimalColumn52.Width = 70;
		gridViewDecimalColumn52.WrapText = true;
		gridViewCommandColumn1.DefaultText = "Ver Referencias";
		gridViewCommandColumn1.HeaderText = "Referencias Producto";
		gridViewCommandColumn1.Name = "colRefereciasProd";
		gridViewCommandColumn1.UseDefaultText = true;
		gridViewCommandColumn1.Width = 91;
		gridViewCommandColumn1.WrapText = true;
		this.rgvListado.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewDecimalColumn33, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewDecimalColumn34, gridViewDecimalColumn35, gridViewDecimalColumn36, gridViewDecimalColumn37, gridViewDecimalColumn38, gridViewDecimalColumn39, gridViewDecimalColumn40, gridViewDecimalColumn41, gridViewDecimalColumn42, gridViewTextBoxColumn27, gridViewDecimalColumn43, gridViewDecimalColumn44, gridViewDecimalColumn45, gridViewDecimalColumn46, gridViewDecimalColumn47, gridViewDecimalColumn48, gridViewDecimalColumn49, gridViewDecimalColumn50, gridViewDecimalColumn51, gridViewDecimalColumn52, gridViewCommandColumn1);
		this.rgvListado.MasterTemplate.EnableFiltering = true;
		this.rgvListado.MasterTemplate.EnableGrouping = false;
		this.rgvListado.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.rgvListado.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvListado.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvListado.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvListado.Name = "rgvListado";
		this.rgvListado.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
		this.rgvListado.ShowGroupPanel = false;
		this.rgvListado.ShowHeaderCellButtons = true;
		this.rgvListado.Size = new System.Drawing.Size(1497, 499);
		this.rgvListado.TabIndex = 1;
		this.rgvListado.ThemeName = "ControlDefault";
		this.rgvListado.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvListado_CellEndEdit);
		this.rgvListado.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvListado_CellClick);
		this.radGridView1.AutoScroll = true;
		this.radGridView1.AutoSizeRows = true;
		this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.radGridView1.EnterKeyMode = Telerik.WinControls.UI.RadGridViewEnterKeyMode.EnterMovesToNextRow;
		this.radGridView1.Location = new System.Drawing.Point(0, 0);
		this.radGridView1.MasterTemplate.AllowAddNewRow = false;
		this.radGridView1.MasterTemplate.AllowColumnReorder = false;
		this.radGridView1.MasterTemplate.AllowDeleteRow = false;
		this.radGridView1.MasterTemplate.AllowDragToGroup = false;
		this.radGridView1.MasterTemplate.ClipboardCutMode = Telerik.WinControls.UI.GridViewClipboardCutMode.Disable;
		gridViewTextBoxColumn28.FieldName = "codDetalleOrden";
		gridViewTextBoxColumn28.HeaderText = "codDetalleOrden";
		gridViewTextBoxColumn28.Name = "codDetalleOrden";
		gridViewTextBoxColumn28.Width = 100;
		gridViewTextBoxColumn29.FieldName = "nroItem";
		gridViewTextBoxColumn29.HeaderText = "Item";
		gridViewTextBoxColumn29.IsPinned = true;
		gridViewTextBoxColumn29.IsVisible = false;
		gridViewTextBoxColumn29.Name = "colNroItem";
		gridViewTextBoxColumn29.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn29.ReadOnly = true;
		gridViewTextBoxColumn29.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn30.FieldName = "codProducto";
		gridViewTextBoxColumn30.HeaderText = "codProducto";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "colCodProducto";
		gridViewTextBoxColumn30.ReadOnly = true;
		gridViewTextBoxColumn31.FieldName = "referencia";
		gridViewTextBoxColumn31.HeaderText = "Referencia";
		gridViewTextBoxColumn31.IsPinned = true;
		gridViewTextBoxColumn31.Name = "colReferencia";
		gridViewTextBoxColumn31.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn31.ReadOnly = true;
		gridViewTextBoxColumn31.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn31.Width = 74;
		gridViewTextBoxColumn32.FieldName = "descripcionProducto";
		gridViewTextBoxColumn32.HeaderText = "Producto";
		gridViewTextBoxColumn32.IsPinned = true;
		gridViewTextBoxColumn32.Name = "colDescripProducto";
		gridViewTextBoxColumn32.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn32.ReadOnly = true;
		gridViewTextBoxColumn32.Width = 340;
		gridViewTextBoxColumn32.WrapText = true;
		gridViewTextBoxColumn33.FieldName = "codUnidadMedida";
		gridViewTextBoxColumn33.HeaderText = "codUnidadMedida";
		gridViewTextBoxColumn33.IsVisible = false;
		gridViewTextBoxColumn33.Name = "colCodUnidadMedida";
		gridViewTextBoxColumn33.ReadOnly = true;
		gridViewTextBoxColumn34.FieldName = "descripcionUnidadMedida";
		gridViewTextBoxColumn34.HeaderText = "Unidad de Medida";
		gridViewTextBoxColumn34.IsPinned = true;
		gridViewTextBoxColumn34.Name = "colDescripUnidadMedida";
		gridViewTextBoxColumn34.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn34.ReadOnly = true;
		gridViewTextBoxColumn34.Width = 116;
		gridViewTextBoxColumn35.FieldName = "tipo_precio";
		gridViewTextBoxColumn35.HeaderText = "Tipo Precio";
		gridViewTextBoxColumn35.Name = "tipo_precio";
		gridViewTextBoxColumn35.Width = 110;
		gridViewTextBoxColumn36.FieldName = "codUnidadEquivalente";
		gridViewTextBoxColumn36.HeaderText = "codUnidadEquivalente";
		gridViewTextBoxColumn36.Name = "colCodUnidadEquivalente";
		gridViewTextBoxColumn36.ReadOnly = true;
		gridViewTextBoxColumn37.FieldName = "codUnidadEquiCompra";
		gridViewTextBoxColumn37.HeaderText = "codUnidadEquiCompra";
		gridViewTextBoxColumn37.Name = "colCodUnidadEquiCompra";
		gridViewTextBoxColumn38.DataType = typeof(decimal);
		gridViewTextBoxColumn38.FieldName = "ultimo_precio_compra_dolares";
		gridViewTextBoxColumn38.HeaderText = "Ultimo Precio OC $.";
		gridViewTextBoxColumn38.Name = "ultimo_precio_oc_dolares";
		gridViewTextBoxColumn38.Width = 120;
		gridViewDecimalColumn53.FieldName = "tipCambioProv_ultCompra";
		gridViewDecimalColumn53.HeaderText = "Ultimo Tipo Cambio OC";
		gridViewDecimalColumn53.Name = "TipCaambio_ultima_oc";
		gridViewDecimalColumn53.Width = 140;
		gridViewDecimalColumn54.FieldName = "ultimo_precio_compra_soles";
		gridViewDecimalColumn54.HeaderText = "Ultimo Precio OC S/.";
		gridViewDecimalColumn54.Name = "ultimo_precio_oc_soles";
		gridViewDecimalColumn54.Width = 130;
		gridViewDecimalColumn55.FieldName = "precio_compra_catalogo";
		gridViewDecimalColumn55.HeaderText = "PC Catalogo";
		gridViewDecimalColumn55.Name = "precio_compra_catalogo";
		gridViewDecimalColumn55.Width = 90;
		gridViewDecimalColumn56.FieldName = "dif_uPcompra_pcatalogo";
		gridViewDecimalColumn56.HeaderText = "Dif. ultimo precio compra y precio catalogo";
		gridViewDecimalColumn56.Name = "dif_up_compra_preciocatalogo";
		gridViewDecimalColumn56.Width = 150;
		gridViewDecimalColumn57.FieldName = "precio_compra_catalogo_sin_igv";
		gridViewDecimalColumn57.HeaderText = "PC Catalogo sin igv";
		gridViewDecimalColumn57.Name = "precio_catalogo_sinigv";
		gridViewDecimalColumn57.Width = 140;
		gridViewDecimalColumn58.FieldName = "ubruta_ant_soles";
		gridViewDecimalColumn58.HeaderText = "UB anterior S/";
		gridViewDecimalColumn58.Name = "ub_anterior_soles";
		gridViewDecimalColumn58.Width = 100;
		gridViewDecimalColumn59.FieldName = "ubruta_ant_porcentaje";
		gridViewDecimalColumn59.HeaderText = "UB anterior %";
		gridViewDecimalColumn59.Name = "ub_anterior_porcentaje";
		gridViewDecimalColumn59.Width = 100;
		gridViewDecimalColumn60.FieldName = "flete";
		gridViewDecimalColumn60.HeaderText = "Flete";
		gridViewDecimalColumn60.Name = "flete";
		gridViewDecimalColumn60.Width = 90;
		gridViewDecimalColumn61.FieldName = "Desestiva";
		gridViewDecimalColumn61.HeaderText = "Desestiva";
		gridViewDecimalColumn61.Name = "desestiva";
		gridViewDecimalColumn61.Width = 80;
		gridViewDecimalColumn62.FieldName = "precio_compraN_dolares";
		gridViewDecimalColumn62.HeaderText = "PC nuevo dol";
		gridViewDecimalColumn62.Name = "precio_compraN_dolares";
		gridViewDecimalColumn62.Width = 100;
		gridViewDecimalColumn63.FieldName = "tipCambioProv";
		gridViewDecimalColumn63.HeaderText = "Tipo de cambio actual";
		gridViewDecimalColumn63.Name = "tipo_cambio_actual";
		gridViewDecimalColumn63.Width = 100;
		gridViewDecimalColumn64.FieldName = "precio_compraN_soles";
		gridViewDecimalColumn64.HeaderText = "PC nuevo sol";
		gridViewDecimalColumn64.Name = "precio_compra_nuevo_sol";
		gridViewDecimalColumn64.Width = 100;
		gridViewDecimalColumn65.FieldName = "precio_nuevo_sinigv";
		gridViewDecimalColumn65.HeaderText = "PC nuevo sin igv";
		gridViewDecimalColumn65.Name = "prec_compra_nuevo_sinigv";
		gridViewDecimalColumn65.Width = 100;
		gridViewDecimalColumn66.FieldName = "precio_venta_catalogo";
		gridViewDecimalColumn66.HeaderText = "PV catalogo";
		gridViewDecimalColumn66.Name = "precio_venta_catalogo";
		gridViewDecimalColumn66.Width = 100;
		gridViewDecimalColumn67.FieldName = "precio_venta_catalogo_sin_igv";
		gridViewDecimalColumn67.HeaderText = "PV catalogo sin igv";
		gridViewDecimalColumn67.Name = "precio_venta_catalogo_sin_igv";
		gridViewDecimalColumn67.Width = 100;
		gridViewDecimalColumn68.FieldName = "utilidad_ope_soles";
		gridViewDecimalColumn68.HeaderText = "Utilidad Ope. S/";
		gridViewDecimalColumn68.Name = "utilidad_ope_soles";
		gridViewDecimalColumn68.Width = 100;
		gridViewDecimalColumn69.FieldName = "utilidad_ope_porcentaje";
		gridViewDecimalColumn69.HeaderText = "Utilidad Ope. %";
		gridViewDecimalColumn69.Name = "utilidad_ope_porcentaje";
		gridViewDecimalColumn69.Width = 100;
		gridViewDecimalColumn70.FieldName = "precio_venta_final";
		gridViewDecimalColumn70.HeaderText = "PV final";
		gridViewDecimalColumn70.Name = "precio_venta_final";
		gridViewDecimalColumn70.Width = 100;
		gridViewDecimalColumn71.HeaderText = "PV final sin igv";
		gridViewDecimalColumn71.Name = "precio_venta_final_sinigv";
		gridViewDecimalColumn71.Width = 100;
		gridViewDecimalColumn72.HeaderText = "Utilidad Ope. Final S/";
		gridViewDecimalColumn72.Name = "utilidad_ope_final_sol";
		gridViewDecimalColumn72.Width = 100;
		gridViewDecimalColumn73.HeaderText = "Utilidad Ope. Final %";
		gridViewDecimalColumn73.Name = "utilidad_ope_final_porcentaje";
		gridViewDecimalColumn73.Width = 100;
		gridViewDecimalColumn74.HeaderText = "Utilidad bruta S/";
		gridViewDecimalColumn74.Name = "utilidad_bruta_sol";
		gridViewDecimalColumn74.Width = 100;
		gridViewDecimalColumn75.HeaderText = "Utilidad bruta %";
		gridViewDecimalColumn75.Name = "utilidad_bruta_porcen";
		gridViewDecimalColumn75.Width = 100;
		gridViewTextBoxColumn39.FieldName = "categorizacion";
		gridViewTextBoxColumn39.HeaderText = "Categorización";
		gridViewTextBoxColumn39.Name = "categorizacion";
		gridViewTextBoxColumn39.Width = 130;
		gridViewDecimalColumn76.FieldName = "flete_nuevo";
		gridViewDecimalColumn76.HeaderText = "Flete nuevo";
		gridViewDecimalColumn76.Name = "flete_nuevo";
		gridViewDecimalColumn76.Width = 100;
		gridViewDecimalColumn77.HeaderText = "Desestiva/Otros";
		gridViewDecimalColumn77.Name = "desestiva_otros";
		gridViewDecimalColumn77.Width = 100;
		gridViewDecimalColumn78.FieldName = "pv_sug_porcentaje";
		gridViewDecimalColumn78.HeaderText = "PV Sug. %";
		gridViewDecimalColumn78.Name = "pv_sug_porcentaje";
		gridViewDecimalColumn78.Width = 100;
		gridViewDecimalColumn79.FieldName = "pv_sug_soles";
		gridViewDecimalColumn79.HeaderText = "PV Sug. S/";
		gridViewDecimalColumn79.Name = "pv_sug_soles";
		gridViewDecimalColumn79.Width = 100;
		gridViewDecimalColumn80.FieldName = "variacion_pc";
		gridViewDecimalColumn80.HeaderText = "Variacion PC";
		gridViewDecimalColumn80.Name = "variacion_pc";
		gridViewDecimalColumn80.Width = 100;
		gridViewDecimalColumn81.HeaderText = "Variacion PV";
		gridViewDecimalColumn81.Name = "variacion_pv";
		gridViewDecimalColumn81.Width = 100;
		gridViewTextBoxColumn40.FieldName = "precio_venta_competencia";
		gridViewTextBoxColumn40.HeaderText = "PV competencia";
		gridViewTextBoxColumn40.Name = "precioventa_ref_competencia";
		gridViewTextBoxColumn40.Width = 100;
		gridViewTextBoxColumn41.FieldName = "precio_venta_SKU";
		gridViewTextBoxColumn41.HeaderText = "SKU";
		gridViewTextBoxColumn41.Name = "skucompetencia";
		gridViewTextBoxColumn41.Width = 80;
		gridViewHyperlinkColumn1.FieldName = "link";
		gridViewHyperlinkColumn1.HeaderText = "Link Competencia";
		gridViewHyperlinkColumn1.Name = "link";
		gridViewHyperlinkColumn1.ReadOnly = false;
		gridViewHyperlinkColumn1.Width = 140;
		gridViewDecimalColumn82.FieldName = "pedido";
		gridViewDecimalColumn82.HeaderText = "pedido";
		gridViewDecimalColumn82.Name = "pedido";
		gridViewDecimalColumn82.Width = 100;
		gridViewDecimalColumn83.HeaderText = "Flete x Ped. sin igv";
		gridViewDecimalColumn83.Name = "flete_xpedido_sinigv";
		gridViewDecimalColumn83.Width = 100;
		gridViewDecimalColumn84.HeaderText = "Flete x Ped. con igv";
		gridViewDecimalColumn84.Name = "flete_xpedido_conigv";
		gridViewDecimalColumn84.Width = 100;
		gridViewTextBoxColumn42.FieldName = "Id_etiqueta";
		gridViewTextBoxColumn42.HeaderText = "estadoOrden";
		gridViewTextBoxColumn42.IsVisible = false;
		gridViewTextBoxColumn42.Name = "Id_etiqueta";
		gridViewTextBoxColumn42.Width = 120;
		gridViewTextBoxColumn43.FieldName = "statuss";
		gridViewTextBoxColumn43.HeaderText = "Estado";
		gridViewTextBoxColumn43.Name = "statuss";
		gridViewTextBoxColumn43.Width = 130;
		this.radGridView1.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32, gridViewTextBoxColumn33, gridViewTextBoxColumn34, gridViewTextBoxColumn35, gridViewTextBoxColumn36, gridViewTextBoxColumn37, gridViewTextBoxColumn38, gridViewDecimalColumn53, gridViewDecimalColumn54, gridViewDecimalColumn55, gridViewDecimalColumn56, gridViewDecimalColumn57, gridViewDecimalColumn58, gridViewDecimalColumn59, gridViewDecimalColumn60, gridViewDecimalColumn61, gridViewDecimalColumn62, gridViewDecimalColumn63, gridViewDecimalColumn64, gridViewDecimalColumn65, gridViewDecimalColumn66, gridViewDecimalColumn67, gridViewDecimalColumn68, gridViewDecimalColumn69, gridViewDecimalColumn70, gridViewDecimalColumn71, gridViewDecimalColumn72, gridViewDecimalColumn73, gridViewDecimalColumn74, gridViewDecimalColumn75, gridViewTextBoxColumn39, gridViewDecimalColumn76, gridViewDecimalColumn77, gridViewDecimalColumn78, gridViewDecimalColumn79, gridViewDecimalColumn80, gridViewDecimalColumn81, gridViewTextBoxColumn40, gridViewTextBoxColumn41, gridViewHyperlinkColumn1, gridViewDecimalColumn82, gridViewDecimalColumn83, gridViewDecimalColumn84, gridViewTextBoxColumn42, gridViewTextBoxColumn43);
		this.radGridView1.MasterTemplate.EnableFiltering = true;
		this.radGridView1.MasterTemplate.EnableGrouping = false;
		this.radGridView1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.radGridView1.MasterTemplate.ShowHeaderCellButtons = true;
		this.radGridView1.MasterTemplate.ShowRowHeaderColumn = false;
		this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition3;
		this.radGridView1.Name = "radGridView1";
		this.radGridView1.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
		this.radGridView1.ShowGroupPanel = false;
		this.radGridView1.ShowHeaderCellButtons = true;
		this.radGridView1.Size = new System.Drawing.Size(1497, 499);
		this.radGridView1.TabIndex = 2;
		this.radGridView1.ThemeName = "ControlDefault";
		this.radGridView1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(radGridView1_CellFormatting);
		this.radGridView1.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(radGridView1_CellBeginEdit);
		this.radGridView1.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(radGridView1_CellEndEdit);
		this.groupBox2.Controls.Add(this.btnseteado);
		this.groupBox2.Controls.Add(this.btnrevisado);
		this.groupBox2.Controls.Add(this.btnActualizarLIsta);
		this.groupBox2.Controls.Add(this.lblDescripcionOrdenCompra);
		this.groupBox2.Controls.Add(this.lblOrdenCompra);
		this.groupBox2.Controls.Add(this.btnUpdateFleteNuevo);
		this.groupBox2.Controls.Add(this.lblusuario);
		this.groupBox2.Controls.Add(this.btnUpdatePrecioCompra);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1503, 83);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.btnseteado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnseteado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnseteado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnseteado.Image = SIGEFA.Properties.Resources.save;
		this.btnseteado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnseteado.Location = new System.Drawing.Point(517, 22);
		this.btnseteado.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnseteado.Name = "btnseteado";
		this.btnseteado.Size = new System.Drawing.Size(173, 43);
		this.btnseteado.TabIndex = 82;
		this.btnseteado.Text = "Guardar estado seteado";
		this.btnseteado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnseteado.UseVisualStyleBackColor = true;
		this.btnseteado.Click += new System.EventHandler(btnseteado_Click);
		this.btnrevisado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnrevisado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnrevisado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnrevisado.Image = SIGEFA.Properties.Resources.save;
		this.btnrevisado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnrevisado.Location = new System.Drawing.Point(698, 22);
		this.btnrevisado.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnrevisado.Name = "btnrevisado";
		this.btnrevisado.Size = new System.Drawing.Size(173, 43);
		this.btnrevisado.TabIndex = 81;
		this.btnrevisado.Text = "Guardar estado revisado";
		this.btnrevisado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnrevisado.UseVisualStyleBackColor = true;
		this.btnrevisado.Click += new System.EventHandler(btnguardar_estado_Click);
		this.btnActualizarLIsta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizarLIsta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnActualizarLIsta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnActualizarLIsta.Image = SIGEFA.Properties.Resources.cambio;
		this.btnActualizarLIsta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnActualizarLIsta.Location = new System.Drawing.Point(894, 21);
		this.btnActualizarLIsta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnActualizarLIsta.Name = "btnActualizarLIsta";
		this.btnActualizarLIsta.Size = new System.Drawing.Size(136, 43);
		this.btnActualizarLIsta.TabIndex = 80;
		this.btnActualizarLIsta.Text = "Actualizar Lista";
		this.btnActualizarLIsta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnActualizarLIsta.UseVisualStyleBackColor = true;
		this.btnActualizarLIsta.Click += new System.EventHandler(btnActualizarLIsta_Click);
		this.lblDescripcionOrdenCompra.AutoSize = true;
		this.lblDescripcionOrdenCompra.Location = new System.Drawing.Point(152, 29);
		this.lblDescripcionOrdenCompra.Name = "lblDescripcionOrdenCompra";
		this.lblDescripcionOrdenCompra.Size = new System.Drawing.Size(0, 13);
		this.lblDescripcionOrdenCompra.TabIndex = 79;
		this.lblOrdenCompra.Location = new System.Drawing.Point(27, 29);
		this.lblOrdenCompra.Name = "lblOrdenCompra";
		this.lblOrdenCompra.Size = new System.Drawing.Size(119, 13);
		this.lblOrdenCompra.TabIndex = 78;
		this.lblOrdenCompra.Text = "ORDEN DE COMPRA: ";
		this.btnUpdateFleteNuevo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnUpdateFleteNuevo.BackColor = System.Drawing.Color.Gold;
		this.btnUpdateFleteNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnUpdateFleteNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnUpdateFleteNuevo.ForeColor = System.Drawing.Color.White;
		this.btnUpdateFleteNuevo.Image = SIGEFA.Properties.Resources.data_transfer_38px;
		this.btnUpdateFleteNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnUpdateFleteNuevo.Location = new System.Drawing.Point(1038, 19);
		this.btnUpdateFleteNuevo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnUpdateFleteNuevo.Name = "btnUpdateFleteNuevo";
		this.btnUpdateFleteNuevo.Size = new System.Drawing.Size(142, 46);
		this.btnUpdateFleteNuevo.TabIndex = 77;
		this.btnUpdateFleteNuevo.Text = "Actualizar Flete Nuevo";
		this.btnUpdateFleteNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnUpdateFleteNuevo.UseVisualStyleBackColor = false;
		this.btnUpdateFleteNuevo.Click += new System.EventHandler(btnUpdateFleteNuevo_Click);
		this.lblusuario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblusuario.BackColor = System.Drawing.Color.MediumAquamarine;
		this.lblusuario.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.lblusuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblusuario.ForeColor = System.Drawing.Color.White;
		this.lblusuario.Image = SIGEFA.Properties.Resources.data_transfer_38px;
		this.lblusuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblusuario.Location = new System.Drawing.Point(1343, 19);
		this.lblusuario.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.lblusuario.Name = "lblusuario";
		this.lblusuario.Size = new System.Drawing.Size(147, 46);
		this.lblusuario.TabIndex = 76;
		this.lblusuario.Text = "Actualizar Precio Venta";
		this.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblusuario.UseVisualStyleBackColor = false;
		this.lblusuario.Click += new System.EventHandler(btnUpdatePrecioVenta_Click);
		this.btnUpdatePrecioCompra.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnUpdatePrecioCompra.BackColor = System.Drawing.Color.PaleVioletRed;
		this.btnUpdatePrecioCompra.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnUpdatePrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnUpdatePrecioCompra.ForeColor = System.Drawing.Color.White;
		this.btnUpdatePrecioCompra.Image = SIGEFA.Properties.Resources.data_transfer_38px;
		this.btnUpdatePrecioCompra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnUpdatePrecioCompra.Location = new System.Drawing.Point(1188, 19);
		this.btnUpdatePrecioCompra.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnUpdatePrecioCompra.Name = "btnUpdatePrecioCompra";
		this.btnUpdatePrecioCompra.Size = new System.Drawing.Size(147, 46);
		this.btnUpdatePrecioCompra.TabIndex = 75;
		this.btnUpdatePrecioCompra.Text = "Actualizar Precio Compra";
		this.btnUpdatePrecioCompra.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnUpdatePrecioCompra.UseVisualStyleBackColor = false;
		this.btnUpdatePrecioCompra.Click += new System.EventHandler(btnUpdatePrecioCompra_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1503, 606);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmModificacionPreciosOrdenCompra";
		this.Text = "Modificacion de Precios de OrdenCompra";
		base.Load += new System.EventHandler(frmModificacionPreciosOrdenCompra_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvListado.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListado).EndInit();
		this.rgvListado.ResumeLayout(false);
		this.rgvListado.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radGridView1.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radGridView1).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
