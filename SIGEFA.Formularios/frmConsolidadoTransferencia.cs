using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmConsolidadoTransferencia : Office2007Form
{
	private clsAdmRequerimiento AdmReq = new clsAdmRequerimiento();

	private clsRequerimiento Req = new clsRequerimiento();

	private clsAdmOrdenCompra AdmOrden = new clsAdmOrdenCompra();

	private clsOrdenCompra Orden = new clsOrdenCompra();

	private clsDetalleConsolidado dtaConsolidado = new clsDetalleConsolidado();

	private clsValidar val = new clsValidar();

	public DataTable productosConsolidado = new DataTable();

	public List<int> coddetallerequerimientos = new List<int>();

	public List<clsDetalleConsolidado> detalle = new List<clsDetalleConsolidado>();

	public List<int> seleccion = new List<int>();

	public int CodProveedor;

	public int CodOrdenCompra;

	public int codorden = 0;

	public int estadcheck;

	public int proceso = 0;

	public int proce = 0;

	public int Alm = 0;

	public int Almaori = 0;

	public int Contador = 0;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto prod = new clsProducto();

	public static BindingSource data = new BindingSource();

	public static BindingSource data_detalle = new BindingSource();

	private string filtro = string.Empty;

	public DataTable DtAuxiliar = new DataTable();

	public DataTable DtAuxiliar_pasados = new DataTable();

	private int posicion_dgdetalle = -1;

	private IContainer components = null;

	private DataGridView dgvOrdenes;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn Documento;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn Comentario;

	private DataGridViewTextBoxColumn Atendido;

	private DataGridViewTextBoxColumn Estado;

	private GroupBox groupBox3;

	private Button btnCerrarDetalle;

	private Button btnVerDetalle;

	private Button BtnAnular;

	private Button btnSalir;

	public Button btnGuardar;

	private ImageList imageList1;

	private ErrorProvider errorProvider1;

	private ImageList imageList2;

	public DataGridView dgvDetalle;

	private GroupBox groupBox2;

	private Button btnConsultar;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private GroupBox groupBox1;

	private ImageList imageList3;

	private TextBox txtComentario;

	private Label label2;

	private TextBox txtCantidad;

	private Label label1;

	public Button btnConfirmar;

	private GroupBox groupBox4;

	private DataGridViewCheckBoxColumn escoje;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn FechaRequerida;

	private DataGridViewTextBoxColumn codTipoDocumento;

	private DataGridViewTextBoxColumn serie;

	private DataGridViewTextBoxColumn numeracion;

	private DataGridViewTextBoxColumn codAlmacen;

	private DataGridViewTextBoxColumn Almacen;

	private DataGridViewTextBoxColumn codProducto;

	private DataGridViewTextBoxColumn Referencia;

	private DataGridViewTextBoxColumn Producto;

	private DataGridViewTextBoxColumn Unidad;

	private DataGridViewTextBoxColumn Cantidad;

	private DataGridViewTextBoxColumn stockgeneral;

	private DataGridViewTextBoxColumn StockActual;

	private DataGridViewTextBoxColumn codUnidadMedida;

	private DataGridViewTextBoxColumn comentario_usu;

	private DataGridViewTextBoxColumn estado_vigente;

	private DataGridViewTextBoxColumn codrequerimiento;

	public frmConsolidadoTransferencia()
	{
		InitializeComponent();
	}

	private void frmConsolidadoTransferencia_Load(object sender, EventArgs e)
	{
		agregafilasDT();
	}

	private void agregafilasDT()
	{
		DtAuxiliar.Clear();
		DtAuxiliar_pasados.Clear();
		DtAuxiliar.Columns.Add("coddetalle", typeof(int));
		DtAuxiliar.Columns.Add("codProducto", typeof(int));
		DtAuxiliar.Columns.Add("Referencia", typeof(string));
		DtAuxiliar.Columns.Add("codUnidadMedida", typeof(int));
		DtAuxiliar.Columns.Add("Producto", typeof(string));
		DtAuxiliar.Columns.Add("Unidad", typeof(string));
		DtAuxiliar.Columns.Add("Cantidad", typeof(decimal));
		DtAuxiliar.Columns.Add("comentario_usu", typeof(string));
		DtAuxiliar.Columns.Add("codrequerimiento", typeof(decimal));
		DtAuxiliar_pasados.Columns.Add("coddetalle", typeof(int));
		DtAuxiliar_pasados.Columns.Add("codProducto", typeof(int));
		DtAuxiliar_pasados.Columns.Add("Referencia", typeof(string));
		DtAuxiliar_pasados.Columns.Add("codUnidadMedida", typeof(int));
		DtAuxiliar_pasados.Columns.Add("Producto", typeof(string));
		DtAuxiliar_pasados.Columns.Add("Unidad", typeof(string));
		DtAuxiliar_pasados.Columns.Add("Cantidad", typeof(decimal));
		DtAuxiliar_pasados.Columns.Add("comentario_usu", typeof(string));
		DtAuxiliar_pasados.Columns.Add("codrequerimiento", typeof(decimal));
	}

	private void LlenadoAuxiliar()
	{
		foreach (DataGridViewRow rowGrid in (IEnumerable)dgvDetalle.Rows)
		{
			DataRow row = DtAuxiliar.NewRow();
			row["coddetalle"] = rowGrid.Cells[coddetalle.Name].Value;
			row["codProducto"] = rowGrid.Cells[codProducto.Name].Value;
			row["Producto"] = rowGrid.Cells[Referencia.Name].Value;
			row["Referencia"] = rowGrid.Cells[Producto.Name].Value;
			row["Unidad"] = rowGrid.Cells[Unidad.Name].Value;
			row["Cantidad"] = rowGrid.Cells[Cantidad.Name].Value;
			row["codUnidadMedida"] = rowGrid.Cells[codUnidadMedida.Name].Value;
			row["comentario_usu"] = rowGrid.Cells[comentario_usu.Name].Value;
			row["codrequerimiento"] = rowGrid.Cells[codrequerimiento.Name].Value;
			DtAuxiliar.Rows.Add(row);
		}
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		CargaListaHistorial(Almaori, Alm, 1);
	}

	private void CargaListaHistorial(int alma, int almades, int tip)
	{
		dgvDetalle.Visible = false;
		dgvOrdenes.Visible = true;
		dgvOrdenes.DataSource = data;
		data.DataSource = AdmReq.ListaRequerimientoHistorial_x_almacen(alma, almades, dtpDesde.Value.Date, dtpHasta.Value.Date, tip);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvOrdenes.ClearSelection();
		btnVerDetalle.Enabled = true;
	}

	private void btnVerDetalle_Click(object sender, EventArgs e)
	{
		DtAuxiliar.Clear();
		DtAuxiliar_pasados.Clear();
		if (dgvOrdenes.SelectedRows.Count > 0)
		{
			btnCerrarDetalle.Enabled = true;
			CargaRequerimientosTotales(Alm, Almaori);
			dgvDetalle.Visible = true;
			dgvOrdenes.Visible = false;
			btnGuardar.Visible = true;
			LlenadoAuxiliar();
			btnGuardar.Visible = false;
		}
		else
		{
			MessageBox.Show("Debe seleccionar un valor para ver su detalle.");
		}
	}

	private void dgvOrdenes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && e.Row.Selected)
		{
			Req.CodRequerimiento = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			Req.CodAlmacen = Alm;
		}
	}

	public void CargaRequerimientosTotales(int almac, int almaori)
	{
		dgvDetalle.DataSource = data_detalle;
		data_detalle.DataSource = AdmReq.cargaRequerimientosTotales_x_requerimiento(Req.CodRequerimiento);
		data_detalle.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalle.ClearSelection();
	}

	private void btnCerrarDetalle_Click(object sender, EventArgs e)
	{
		btnCerrarDetalle.Enabled = false;
		dgvDetalle.Visible = false;
		dgvOrdenes.Visible = true;
		btnGuardar.Visible = false;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		bool cantidad_checks = false;
		if (cuenta_cheks())
		{
			frmTranferenciaDirecta form = (frmTranferenciaDirecta)Application.OpenForms["frmTranferenciaDirecta"];
			if (dgvDetalle.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				if (Convert.ToBoolean(row.Cells[escoje.Name].Value))
				{
					Close();
					form.txtAlmacenDestino.Text = Convert.ToString(row.Cells[numeracion.Name].Value);
					form.txtReq.Text = Req.CodRequerimiento.ToString();
					prod = AdmPro.MuestraProductosTransferencia_nuevo(Convert.ToInt32(row.Cells[codProducto.Name].Value), Almaori);
					if (proce == 1)
					{
						if (prod.Cantidad == 0)
						{
							form.dgvDetalle.Rows.Add(row.Cells[coddetalle.Name].Value, row.Cells[codProducto.Name].Value, row.Cells[Referencia.Name].Value, row.Cells[Producto.Name].Value, row.Cells[codUnidadMedida.Name].Value, row.Cells[Unidad.Name].Value, row.Cells[Cantidad.Name].Value, 0, 0, 0, row.Cells[stockgeneral.Name].Value, row.Cells[comentario_usu.Name].Value, row.Cells[codrequerimiento.Name].Value);
						}
						else
						{
							form.dgvDetalle.Rows.Add(row.Cells[coddetalle.Name].Value, row.Cells[codProducto.Name].Value, row.Cells[Referencia.Name].Value, row.Cells[Producto.Name].Value, row.Cells[codUnidadMedida.Name].Value, row.Cells[Unidad.Name].Value, row.Cells[Cantidad.Name].Value, prod.ValorProm, prod.ValorPromsoles, prod.PrecioProm, row.Cells[stockgeneral.Name].Value, row.Cells[comentario_usu.Name].Value, row.Cells[codrequerimiento.Name].Value);
						}
						DataRow row_aux = DtAuxiliar_pasados.NewRow();
						row_aux["coddetalle"] = Convert.ToInt32(row.Cells[coddetalle.Name].Value);
						row_aux["codProducto"] = Convert.ToInt32(row.Cells[codProducto.Name].Value);
						row_aux["Referencia"] = Convert.ToString(row.Cells[Referencia.Name].Value);
						row_aux["Producto"] = Convert.ToString(row.Cells[Producto.Name].Value);
						row_aux["codUnidadMedida"] = Convert.ToInt32(row.Cells[codUnidadMedida.Name].Value);
						row_aux["Unidad"] = Convert.ToString(row.Cells[Unidad.Name].Value);
						row_aux["Cantidad"] = Convert.ToDecimal(row.Cells[Cantidad.Name].Value);
						row_aux["comentario_usu"] = Convert.ToString(row.Cells[comentario_usu.Name].Value);
						row_aux["codrequerimiento"] = Convert.ToInt32(row.Cells[codrequerimiento.Name].Value);
						DtAuxiliar_pasados.Rows.Add(row_aux);
					}
					else if (proce == 2)
					{
						form.dgvDetalle.CurrentRow.SetValues(row.Cells[coddetalle.Name].Value, row.Cells[codProducto.Name].Value, row.Cells[Referencia.Name].Value, row.Cells[Producto.Name].Value, row.Cells[codUnidadMedida.Name].Value, row.Cells[Unidad.Name].Value, row.Cells[Cantidad.Name].Value, prod.ValorProm, prod.ValorPromsoles, prod.PrecioProm, prod.StockActual);
					}
				}
				int can = DtAuxiliar_pasados.Rows.Count;
			}
			form.DtAuxiliar_pr = DtAuxiliar;
			form.DtAuxiliar_pasados_pr = DtAuxiliar_pasados;
		}
		else
		{
			MessageBoxEx.Show("Debe validar todo el detalle del requerimiento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private bool cuenta_cheks()
	{
		int cantidad_elemento = dgvDetalle.Rows.Count;
		int veces = 0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToBoolean(row.Cells[escoje.Name].Value))
			{
				veces++;
			}
		}
		return (veces == cantidad_elemento) ? true : false;
	}

	private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		dgvDetalle.Enabled = false;
		txtCantidad.Text = "";
		txtComentario.Text = "";
		DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
		ch1 = (DataGridViewCheckBoxCell)dgvDetalle.Rows[dgvDetalle.CurrentRow.Index].Cells[0];
		if (ch1.Value == null)
		{
			ch1.Value = false;
		}
		string text = ch1.Value.ToString();
		string text2 = text;
		if (!(text2 == "True"))
		{
			if (text2 == "False")
			{
				ch1.Value = true;
			}
		}
		else
		{
			ch1.Value = false;
		}
		if (ch1.Value.ToString().Equals("True"))
		{
			posicion_dgdetalle = dgvDetalle.CurrentRow.Index;
			groupBox4.Visible = true;
			return;
		}
		posicion_dgdetalle = dgvDetalle.CurrentRow.Index;
		dgvDetalle.Rows[posicion_dgdetalle].Cells["comentario_usu"].Value = "";
		for (int i = 0; i <= DtAuxiliar.Rows.Count; i++)
		{
			if (i == posicion_dgdetalle)
			{
				dgvDetalle.Rows[posicion_dgdetalle].Cells["Cantidad"].Value = DtAuxiliar.Rows[i]["Cantidad"].ToString();
			}
		}
	}

	private void btnConfirmar_Click(object sender, EventArgs e)
	{
		accion_envia();
	}

	private void accion_envia()
	{
		int pos = posicion_dgdetalle;
		decimal cantidad_ingresada = default(decimal);
		decimal cantidad_celda = default(decimal);
		decimal cantidad_almacen = default(decimal);
		cantidad_celda = Convert.ToDecimal(dgvDetalle.Rows[pos].Cells["Cantidad"].Value.ToString());
		cantidad_almacen = Convert.ToDecimal(dgvDetalle.Rows[pos].Cells["stockgeneral"].Value.ToString());
		if (txtCantidad.Text != "")
		{
			cantidad_ingresada = Convert.ToDecimal(txtCantidad.Text);
			if (cantidad_almacen >= cantidad_ingresada)
			{
				if (cantidad_ingresada < cantidad_celda)
				{
					if (txtComentario.Text == "")
					{
						MessageBox.Show("Como la cantidad ingresada es menor a la solicitada, debe ingresar un comentario");
						return;
					}
					dgvDetalle.Rows[pos].Cells["Cantidad"].Value = cantidad_ingresada;
					dgvDetalle.Rows[pos].Cells["comentario_usu"].Value = txtComentario.Text;
					MessageBox.Show("Se cargaron los datos correctamente");
					dgvDetalle.Enabled = true;
					groupBox4.Visible = false;
					btnGuardar.Visible = (cuenta_cheks() ? true : false);
				}
				else
				{
					dgvDetalle.Rows[pos].Cells["Cantidad"].Value = cantidad_ingresada;
					dgvDetalle.Rows[pos].Cells["comentario_usu"].Value = txtComentario.Text;
					MessageBox.Show("Se cargaron los datos correctamente");
					dgvDetalle.Enabled = true;
					groupBox4.Visible = false;
					btnGuardar.Visible = (cuenta_cheks() ? true : false);
				}
			}
			else
			{
				MessageBox.Show("No tiene cantidad suficiente para cumplir con el requerimiento");
			}
		}
		else
		{
			MessageBox.Show("Debe ingresar un cantidad");
		}
	}

	private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmConsolidadoTransferencia));
		this.dgvOrdenes = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Atendido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnCerrarDetalle = new System.Windows.Forms.Button();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.btnVerDetalle = new System.Windows.Forms.Button();
		this.BtnAnular = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnGuardar = new System.Windows.Forms.Button();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.escoje = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.FechaRequerida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codTipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockgeneral = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.StockActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUnidadMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comentario_usu = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado_vigente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codrequerimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnConsultar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCantidad = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.btnConfirmar = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.dgvOrdenes.AllowUserToAddRows = false;
		this.dgvOrdenes.AllowUserToDeleteRows = false;
		this.dgvOrdenes.AllowUserToOrderColumns = true;
		this.dgvOrdenes.AllowUserToResizeRows = false;
		this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvOrdenes.Columns.AddRange(this.codigo, this.Documento, this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.fecha, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.responsable, this.Comentario, this.Atendido, this.Estado);
		this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvOrdenes.Location = new System.Drawing.Point(3, 16);
		this.dgvOrdenes.MultiSelect = false;
		this.dgvOrdenes.Name = "dgvOrdenes";
		this.dgvOrdenes.ReadOnly = true;
		this.dgvOrdenes.RowHeadersVisible = false;
		this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvOrdenes.Size = new System.Drawing.Size(976, 391);
		this.dgvOrdenes.TabIndex = 20;
		this.dgvOrdenes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvOrdenes_RowStateChanged);
		this.codigo.DataPropertyName = "codRequer";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.codigo.Width = 150;
		this.Documento.DataPropertyName = "documento";
		this.Documento.HeaderText = "Documento";
		this.Documento.Name = "Documento";
		this.Documento.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.DataPropertyName = "serie";
		this.dataGridViewTextBoxColumn1.HeaderText = "Serie";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Visible = false;
		this.dataGridViewTextBoxColumn2.DataPropertyName = "numeracion";
		this.dataGridViewTextBoxColumn2.HeaderText = "Numeracion";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Visible = false;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle3;
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 80;
		this.dataGridViewTextBoxColumn3.DataPropertyName = "nombre";
		this.dataGridViewTextBoxColumn3.HeaderText = "Almacen";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.ReadOnly = true;
		this.dataGridViewTextBoxColumn3.Width = 150;
		this.dataGridViewTextBoxColumn4.DataPropertyName = "codAlmacen";
		this.dataGridViewTextBoxColumn4.HeaderText = "codAlmacen";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.ReadOnly = true;
		this.dataGridViewTextBoxColumn4.Visible = false;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 200;
		this.Comentario.DataPropertyName = "comentario";
		this.Comentario.HeaderText = "Comentario";
		this.Comentario.Name = "Comentario";
		this.Comentario.ReadOnly = true;
		this.Comentario.Visible = false;
		this.Atendido.DataPropertyName = "Atendido";
		this.Atendido.HeaderText = "Estado";
		this.Atendido.Name = "Atendido";
		this.Atendido.ReadOnly = true;
		this.Atendido.Width = 220;
		this.Estado.DataPropertyName = "Estado";
		this.Estado.HeaderText = "Anulado";
		this.Estado.Name = "Estado";
		this.Estado.ReadOnly = true;
		this.groupBox3.Controls.Add(this.btnCerrarDetalle);
		this.groupBox3.Controls.Add(this.btnVerDetalle);
		this.groupBox3.Controls.Add(this.BtnAnular);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 587);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(982, 53);
		this.groupBox3.TabIndex = 21;
		this.groupBox3.TabStop = false;
		this.btnCerrarDetalle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnCerrarDetalle.Enabled = false;
		this.btnCerrarDetalle.ImageIndex = 9;
		this.btnCerrarDetalle.ImageList = this.imageList3;
		this.btnCerrarDetalle.Location = new System.Drawing.Point(87, 10);
		this.btnCerrarDetalle.Name = "btnCerrarDetalle";
		this.btnCerrarDetalle.Size = new System.Drawing.Size(75, 37);
		this.btnCerrarDetalle.TabIndex = 7;
		this.btnCerrarDetalle.Text = "Cerrar Detalle";
		this.btnCerrarDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCerrarDetalle.UseVisualStyleBackColor = true;
		this.btnCerrarDetalle.Click += new System.EventHandler(btnCerrarDetalle_Click);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Write Document.png");
		this.imageList3.Images.SetKeyName(1, "New Document.png");
		this.imageList3.Images.SetKeyName(2, "Remove Document.png");
		this.imageList3.Images.SetKeyName(3, "document-print.png");
		this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList3.Images.SetKeyName(5, "exit.png");
		this.imageList3.Images.SetKeyName(6, "search (1).png");
		this.imageList3.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList3.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList3.Images.SetKeyName(9, "document_delete.png");
		this.imageList3.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList3.Images.SetKeyName(11, "OK_Verde.png");
		this.imageList3.Images.SetKeyName(12, "Remove.png");
		this.btnVerDetalle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnVerDetalle.Enabled = false;
		this.btnVerDetalle.ImageIndex = 6;
		this.btnVerDetalle.ImageList = this.imageList3;
		this.btnVerDetalle.Location = new System.Drawing.Point(6, 10);
		this.btnVerDetalle.Name = "btnVerDetalle";
		this.btnVerDetalle.Size = new System.Drawing.Size(75, 37);
		this.btnVerDetalle.TabIndex = 6;
		this.btnVerDetalle.Text = "Ver Detalle";
		this.btnVerDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnVerDetalle.UseVisualStyleBackColor = true;
		this.btnVerDetalle.Click += new System.EventHandler(btnVerDetalle_Click);
		this.BtnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnAnular.ImageIndex = 4;
		this.BtnAnular.ImageList = this.imageList2;
		this.BtnAnular.Location = new System.Drawing.Point(646, 15);
		this.BtnAnular.Name = "BtnAnular";
		this.BtnAnular.Size = new System.Drawing.Size(125, 32);
		this.BtnAnular.TabIndex = 4;
		this.BtnAnular.Text = "Anular";
		this.BtnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.BtnAnular.UseVisualStyleBackColor = true;
		this.BtnAnular.Visible = false;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "exit.png");
		this.imageList2.Images.SetKeyName(1, "pedido.png");
		this.imageList2.Images.SetKeyName(2, "carrito.png");
		this.imageList2.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList2.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList2.Images.SetKeyName(5, "document_delete.png");
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(908, 15);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(777, 15);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(125, 32);
		this.btnGuardar.TabIndex = 2;
		this.btnGuardar.Text = "Transferencia";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Visible = false;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToOrderColumns = true;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.escoje, this.coddetalle, this.FechaRequerida, this.codTipoDocumento, this.serie, this.numeracion, this.codAlmacen, this.Almacen, this.codProducto, this.Referencia, this.Producto, this.Unidad, this.Cantidad, this.stockgeneral, this.StockActual, this.codUnidadMedida, this.comentario_usu, this.estado_vigente, this.codrequerimiento);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(976, 391);
		this.dgvDetalle.TabIndex = 22;
		this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentClick);
		this.escoje.FillWeight = 26f;
		this.escoje.HeaderText = "Escoje";
		this.escoje.Name = "escoje";
		this.escoje.ReadOnly = true;
		this.coddetalle.DataPropertyName = "coddetalle";
		this.coddetalle.HeaderText = "coddetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Visible = false;
		this.FechaRequerida.DataPropertyName = "fecharequerida";
		this.FechaRequerida.FillWeight = 66.81472f;
		this.FechaRequerida.HeaderText = "Fecha Requerida";
		this.FechaRequerida.Name = "FechaRequerida";
		this.FechaRequerida.ReadOnly = true;
		this.codTipoDocumento.DataPropertyName = "codTipoDocumento";
		this.codTipoDocumento.HeaderText = "codTipoDocumento";
		this.codTipoDocumento.Name = "codTipoDocumento";
		this.codTipoDocumento.ReadOnly = true;
		this.codTipoDocumento.Visible = false;
		this.serie.DataPropertyName = "codserie";
		this.serie.HeaderText = "codserie";
		this.serie.Name = "serie";
		this.serie.ReadOnly = true;
		this.serie.Visible = false;
		this.numeracion.DataPropertyName = "numeracion";
		this.numeracion.FillWeight = 66.81472f;
		this.numeracion.HeaderText = "numeracion";
		this.numeracion.Name = "numeracion";
		this.numeracion.ReadOnly = true;
		this.codAlmacen.DataPropertyName = "codAlmacen";
		this.codAlmacen.HeaderText = "codAlmacen";
		this.codAlmacen.Name = "codAlmacen";
		this.codAlmacen.ReadOnly = true;
		this.codAlmacen.Visible = false;
		this.Almacen.DataPropertyName = "descripcion";
		this.Almacen.HeaderText = "Tienda Requerida";
		this.Almacen.Name = "Almacen";
		this.Almacen.ReadOnly = true;
		this.Almacen.Visible = false;
		this.codProducto.DataPropertyName = "codProducto";
		this.codProducto.HeaderText = "codProducto";
		this.codProducto.Name = "codProducto";
		this.codProducto.ReadOnly = true;
		this.codProducto.Visible = false;
		this.Referencia.DataPropertyName = "referencia";
		this.Referencia.FillWeight = 66.81472f;
		this.Referencia.HeaderText = "Referencia";
		this.Referencia.Name = "Referencia";
		this.Referencia.ReadOnly = true;
		this.Producto.DataPropertyName = "producto";
		this.Producto.FillWeight = 66.81472f;
		this.Producto.HeaderText = "Producto";
		this.Producto.Name = "Producto";
		this.Producto.ReadOnly = true;
		this.Unidad.DataPropertyName = "unidad";
		this.Unidad.FillWeight = 66.81472f;
		this.Unidad.HeaderText = "Unidad";
		this.Unidad.Name = "Unidad";
		this.Unidad.ReadOnly = true;
		this.Unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Cantidad.DataPropertyName = "cantidad";
		this.Cantidad.FillWeight = 66.81472f;
		this.Cantidad.HeaderText = "Cantidad Solicitada";
		this.Cantidad.Name = "Cantidad";
		this.Cantidad.ReadOnly = true;
		this.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.stockgeneral.DataPropertyName = "stockgeneral";
		this.stockgeneral.FillWeight = 66.81472f;
		this.stockgeneral.HeaderText = "Stock Tienda ";
		this.stockgeneral.Name = "stockgeneral";
		this.stockgeneral.ReadOnly = true;
		this.StockActual.DataPropertyName = "stockactual";
		this.StockActual.FillWeight = 66.81472f;
		this.StockActual.HeaderText = "Stock Tienda Destino";
		this.StockActual.Name = "StockActual";
		this.StockActual.ReadOnly = true;
		this.codUnidadMedida.DataPropertyName = "codUnidadMedida";
		this.codUnidadMedida.HeaderText = "codUnidadMedida";
		this.codUnidadMedida.Name = "codUnidadMedida";
		this.codUnidadMedida.ReadOnly = true;
		this.codUnidadMedida.Visible = false;
		this.comentario_usu.HeaderText = "Comentario de Usuario";
		this.comentario_usu.Name = "comentario_usu";
		this.comentario_usu.ReadOnly = true;
		this.estado_vigente.HeaderText = "estado_vigente";
		this.estado_vigente.Name = "estado_vigente";
		this.estado_vigente.ReadOnly = true;
		this.estado_vigente.Visible = false;
		this.codrequerimiento.DataPropertyName = "codrequerimiento";
		this.codrequerimiento.HeaderText = "codrequerimiento";
		this.codrequerimiento.Name = "codrequerimiento";
		this.codrequerimiento.ReadOnly = true;
		this.codrequerimiento.Visible = false;
		this.groupBox2.Controls.Add(this.btnConsultar);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.dtpHasta);
		this.groupBox2.Controls.Add(this.dtpDesde);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(982, 71);
		this.groupBox2.TabIndex = 23;
		this.groupBox2.TabStop = false;
		this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnConsultar.ImageIndex = 6;
		this.btnConsultar.ImageList = this.imageList3;
		this.btnConsultar.Location = new System.Drawing.Point(343, 23);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(105, 35);
		this.btnConsultar.TabIndex = 19;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.SteelBlue;
		this.label6.Location = new System.Drawing.Point(12, 45);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(12, 19);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(69, 45);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(254, 20);
		this.dtpHasta.TabIndex = 14;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(69, 13);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(254, 20);
		this.dtpDesde.TabIndex = 15;
		this.groupBox1.Controls.Add(this.dgvDetalle);
		this.groupBox1.Controls.Add(this.dgvOrdenes);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 71);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(982, 410);
		this.groupBox1.TabIndex = 24;
		this.groupBox1.TabStop = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 48);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(63, 13);
		this.label2.TabIndex = 27;
		this.label2.Text = "Comentario:";
		this.txtCantidad.Location = new System.Drawing.Point(81, 19);
		this.txtCantidad.Name = "txtCantidad";
		this.txtCantidad.Size = new System.Drawing.Size(394, 20);
		this.txtCantidad.TabIndex = 26;
		this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCantidad_KeyPress);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(52, 13);
		this.label1.TabIndex = 25;
		this.label1.Text = "Cantidad:";
		this.txtComentario.Location = new System.Drawing.Point(81, 45);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(394, 56);
		this.txtComentario.TabIndex = 28;
		this.btnConfirmar.ImageIndex = 11;
		this.btnConfirmar.ImageList = this.imageList3;
		this.btnConfirmar.Location = new System.Drawing.Point(481, 19);
		this.btnConfirmar.Name = "btnConfirmar";
		this.btnConfirmar.Size = new System.Drawing.Size(102, 49);
		this.btnConfirmar.TabIndex = 29;
		this.btnConfirmar.Text = "Confirmar";
		this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConfirmar.UseVisualStyleBackColor = true;
		this.btnConfirmar.Click += new System.EventHandler(btnConfirmar_Click);
		this.groupBox4.Controls.Add(this.btnConfirmar);
		this.groupBox4.Controls.Add(this.label2);
		this.groupBox4.Controls.Add(this.txtComentario);
		this.groupBox4.Controls.Add(this.label1);
		this.groupBox4.Controls.Add(this.txtCantidad);
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox4.Location = new System.Drawing.Point(0, 481);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(982, 106);
		this.groupBox4.TabIndex = 30;
		this.groupBox4.TabStop = false;
		this.groupBox4.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(982, 640);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmConsolidadoTransferencia";
		this.Text = "frmConsolidadoTransferencia";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmConsolidadoTransferencia_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
