using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmUnidadEquivalente : Office2007Form
{
	private clsAdmTipoPrecio admTp = new clsAdmTipoPrecio();

	private clsAdmUnidad admUni = new clsAdmUnidad();

	private clsAdmProducto admPro = new clsAdmProducto();

	private clsAdmMoneda admmoneda = new clsAdmMoneda();

	private clsAdmUnidad admunidad = new clsAdmUnidad();

	private clsUnidadEquivalente equi = new clsUnidadEquivalente();

	private clsValidar ok = new clsValidar();

	public clsProducto pro = new clsProducto();

	private TextBox txtedit = new TextBox();

	public int codProd;

	public int codunidad;

	public string UnidadB;

	public int interrupto = 0;

	private decimal precioIGV = default(decimal);

	private int codEquiVenta = 0;

	private int proceso = 0;

	private IContainer components = null;

	private Label label1;

	private ComboBox cmbUnidadBase;

	private Label label13;

	private GroupBox groupBox3;

	private TextBox txtPrecio;

	private Label label43;

	private Button btnCancel;

	private Label label16;

	private Button btnOkU1;

	private Label label15;

	private TextBox txtFactor1;

	private Label label14;

	private ComboBox cbUni1;

	private GroupBox groupBox4;

	private Button btnEliminarUnidad;

	private Button btnAñadirUnidad;

	private DataGridView dgvEquivalentes;

	private DataGridViewTextBoxColumn codunidadequivalente;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn factor;

	private DataGridViewTextBoxColumn Precios;

	private GroupBox groupBox6;

	private Button btnAgregar;

	private DataGridView dataGridView1;

	private ImageList imageList1;

	public TextBox txtCodProducto;

	private Button btnEliminar;

	private DataGridView dataGridView2;

	private Label label4;

	private Label label3;

	private Label label9;

	private DataGridView dataGridView3;

	private GroupBox groupBox5;

	private Label label7;

	private ComboBox cmbCompraVenta;

	private Label label2;

	private ComboBox cmbTipo;

	private TextBox txtPrecioUnidad;

	public ComboBox cmbUnidad;

	private Label lbPrecioCosto;

	private Button button1;

	private Label label6;

	private Button button2;

	private TextBox txtFactor2;

	private Label label8;

	private ComboBox cbUni2;

	private ImageList imageList2;

	private Button btnSalir;

	private DataGridViewTextBoxColumn codtabla;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private Label label5;

	private ComboBox comboBox1;

	private DataGridViewTextBoxColumn codi;

	private DataGridViewTextBoxColumn codigoUnd;

	private DataGridViewTextBoxColumn des;

	private DataGridViewTextBoxColumn fac;

	private DataGridViewTextBoxColumn codue;

	private DataGridViewTextBoxColumn equ;

	private DataGridViewTextBoxColumn pre;

	private DataGridViewTextBoxColumn ctip;

	private DataGridViewTextBoxColumn tipp;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn codigoundMed;

	private DataGridViewTextBoxColumn unidad1;

	private DataGridViewTextBoxColumn unidad11;

	private DataGridViewTextBoxColumn Factor1;

	private DataGridViewTextBoxColumn codUndEqui;

	private DataGridViewTextBoxColumn equivalente;

	private DataGridViewTextBoxColumn precios1;

	private DataGridViewTextBoxColumn codTipo;

	private DataGridViewTextBoxColumn tipo;

	public frmUnidadEquivalente()
	{
		InitializeComponent();
	}

	private void CargaUnidades(ComboBox combo)
	{
		combo.DataSource = admUni.MuestraUnidades();
		combo.DisplayMember = "descripcion";
		combo.ValueMember = "codUnidadMedida";
		combo.SelectedIndex = -1;
	}

	private void button4_Click(object sender, EventArgs e)
	{
		cmbUnidad.Enabled = true;
		groupBox5.Visible = true;
		groupBox6.Visible = false;
		cbUni2.Enabled = true;
		txtFactor2.Enabled = true;
		txtPrecioUnidad.Text = "0.00";
		txtFactor2.Text = "0.00";
		CargaUnidades(cbUni2);
		CargaUnidades(cmbUnidad);
	}

	private void CargaListaEquivalencias()
	{
		pro.CodProducto = codProd;
		dataGridView2.DataSource = admPro.MuestraUnidadesEquivalentesCompra(pro.CodProducto, frmLogin.iCodAlmacen);
		dataGridView1.DataSource = admPro.MuestraUnidadesEquivalentesVenta1(pro.CodProducto, frmLogin.iCodAlmacen);
		dataGridView3.DataSource = admPro.MuestraUnidadesEquivalentes(pro.CodProducto, frmLogin.iCodAlmacen);
		dataGridView1.ClearSelection();
		dataGridView2.ClearSelection();
		dataGridView3.ClearSelection();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		groupBox5.Visible = false;
		groupBox6.Visible = true;
		CargaUnidades(cbUni2);
		CargaUnidades(cmbUnidad);
		txtFactor2.Text = "";
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (proceso != 0)
		{
			return;
		}
		if (txtFactor2.Text.Trim() == "")
		{
			txtFactor2.Text = "0";
		}
		if (txtPrecioUnidad.Text.Trim() == "")
		{
			txtPrecioUnidad.Text = "0.00";
		}
		if (cmbCompraVenta.SelectedIndex == -1)
		{
			MessageBox.Show("Seleccione Disponibilidad!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			cmbCompraVenta.Focus();
		}
		else if (cmbCompraVenta.SelectedIndex == 0)
		{
			if (cbUni2.SelectedIndex == -1 || Convert.ToDecimal(txtPrecioUnidad.Text) == 0m || comboBox1.SelectedIndex == -1)
			{
				MessageBox.Show("Ingrese datos correctamente!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cbUni2.Focus();
				return;
			}
			equi.CodUnidad = Convert.ToInt32(cbUni2.SelectedValue);
			txtFactor2.Text = 0.ToString();
			equi.CodProducto = Convert.ToInt32(pro.CodProducto);
			equi.Precio = Convert.ToDecimal(txtPrecioUnidad.Text);
			equi.CodUser = frmLogin.iCodUser;
			equi.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
			equi.CodAlmacen = frmLogin.iCodAlmacen;
			equi.CompraVenta = cmbCompraVenta.SelectedIndex;
			equi.ICodMoneda = Convert.ToInt32(comboBox1.SelectedValue);
			btnSalir.Enabled = true;
			if (admPro.insertunidadequivalente(equi, 0))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaEquivalencias();
				groupBox5.Visible = false;
				groupBox6.Visible = true;
			}
		}
		else if (cmbCompraVenta.SelectedIndex == 1)
		{
			if (cbUni2.SelectedIndex == -1 || cmbTipo.SelectedIndex == -1 || comboBox1.SelectedIndex == -1 || Convert.ToDecimal(txtPrecioUnidad.Text) == 0m)
			{
				MessageBox.Show("Ingrese datos correctamente!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cbUni2.Focus();
				return;
			}
			equi.CodUnidad = Convert.ToInt32(cbUni2.SelectedValue);
			txtFactor2.Text = 0.ToString();
			equi.CodProducto = Convert.ToInt32(pro.CodProducto);
			equi.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
			equi.Precio = Convert.ToDecimal(txtPrecioUnidad.Text);
			equi.CodUser = frmLogin.iCodUser;
			equi.CodAlmacen = frmLogin.iCodAlmacen;
			equi.CompraVenta = cmbCompraVenta.SelectedIndex;
			equi.ICodMoneda = Convert.ToInt32(comboBox1.SelectedValue);
			btnSalir.Enabled = true;
			if (admPro.insertunidadequivalente(equi, 0))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				int validaMo = admunidad.ValidaMoneda();
				if (validaMo > 0 && !admunidad.ActualizaPrecioEnDolares())
				{
					MessageBox.Show("Error");
				}
				CargaListaEquivalencias();
				groupBox5.Visible = false;
				groupBox6.Visible = true;
			}
		}
		else
		{
			if (cmbCompraVenta.SelectedIndex != 2)
			{
				return;
			}
			if (cbUni2.SelectedIndex == -1 || Convert.ToDecimal(txtFactor2.Text) == 0m || cmbUnidad.SelectedIndex == -1 || comboBox1.SelectedIndex == -1)
			{
				MessageBox.Show("Ingrese datos correctamente!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cbUni2.Focus();
				return;
			}
			equi.CodProducto = Convert.ToInt32(pro.CodProducto);
			equi.CodEquivalente = Convert.ToInt32(cmbUnidad.SelectedValue);
			equi.Precio = 0m;
			equi.Factor = Convert.ToDecimal(txtFactor2.Text);
			equi.CodUser = frmLogin.iCodUser;
			equi.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
			equi.CodAlmacen = frmLogin.iCodAlmacen;
			equi.CompraVenta = cmbCompraVenta.SelectedIndex;
			equi.ICodMoneda = Convert.ToInt32(comboBox1.SelectedValue);
			btnSalir.Enabled = true;
			if (admPro.insertunidadequivalente(equi, 0))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaEquivalencias();
				groupBox5.Visible = false;
				groupBox6.Visible = true;
			}
		}
	}

	private int CodMonedaxDescrip(string descripcion)
	{
		int codigo = 0;
		try
		{
			return admmoneda.BuscamonedaXdescripcion(descripcion);
		}
		catch (Exception)
		{
			return 0;
		}
	}

	private void frmUnidadEquivalente_Load(object sender, EventArgs e)
	{
		CargaUnidades(cmbUnidad);
		cargaMoneda();
		CargaTipos();
		CargaListaEquivalencias();
		cmbTipo.Enabled = true;
		cmbUnidad.Enabled = true;
		cbUni2.Enabled = true;
		cmbCompraVenta.Enabled = true;
		cmbCompraVenta.SelectedIndex = -1;
		cbUni2.SelectedIndex = -1;
		cmbTipo.SelectedIndex = -1;
	}

	private void cargaMoneda()
	{
		comboBox1.DataSource = admmoneda.ListaMonedas();
		comboBox1.ValueMember = "codMoneda";
		comboBox1.DisplayMember = "descripcion";
		comboBox1.SelectedIndex = -1;
	}

	private void CargaTipos()
	{
		cmbTipo.DataSource = admTp.listaPrecios();
		cmbTipo.DisplayMember = "Descripcion";
		cmbTipo.ValueMember = "codT";
	}

	private void button5_Click(object sender, EventArgs e)
	{
	}

	private void button6_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Unidades", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && dataGridView1.RowCount > 0)
			{
				int cod = Convert.ToInt32(dataGridView1.CurrentRow.Cells[codigo.Name].Value);
				if (admPro.deleteunidadequivalente(cod))
				{
					MessageBox.Show("Los datos se Eliminaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaListaEquivalencias();
				}
			}
		}
		else if (dataGridView2.SelectedRows.Count > 0)
		{
			DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Unidades", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult2 != DialogResult.No && dataGridView2.RowCount > 0)
			{
				int cod2 = Convert.ToInt32(dataGridView2.CurrentRow.Cells[codi.Name].Value);
				if (admPro.deleteunidadequivalente(cod2))
				{
					MessageBox.Show("Los datos se Eliminaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaListaEquivalencias();
				}
			}
		}
		else
		{
			if (dataGridView3.SelectedRows.Count <= 0)
			{
				return;
			}
			DialogResult dlgResult3 = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Unidades", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult3 != DialogResult.No && dataGridView3.RowCount > 0)
			{
				int cod3 = Convert.ToInt32(dataGridView3.CurrentRow.Cells[codtabla.Name].Value);
				if (admPro.deleteunidadequivalente(cod3))
				{
					MessageBox.Show("Los datos se Eliminaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaListaEquivalencias();
				}
			}
		}
	}

	private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void txtFactor2_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosDecimales(e, txtFactor2);
	}

	private void dataGridView1_Click(object sender, EventArgs e)
	{
		dataGridView2.ClearSelection();
		dataGridView3.ClearSelection();
	}

	private void dataGridView2_Click(object sender, EventArgs e)
	{
		dataGridView1.ClearSelection();
		dataGridView3.ClearSelection();
	}

	private void cmbCompraVenta_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cmbCompraVenta.SelectedIndex == 0)
		{
			label8.Visible = false;
			txtFactor2.Visible = false;
			txtFactor2.Enabled = false;
			cmbUnidad.Visible = false;
			cmbUnidad.Enabled = false;
			cmbTipo.SelectedValue = 9;
			lbPrecioCosto.Text = "Costo con IGV";
			cmbTipo.Enabled = false;
			txtPrecioUnidad.Visible = true;
			lbPrecioCosto.Visible = true;
		}
		if (cmbCompraVenta.SelectedIndex == 1)
		{
			label8.Visible = false;
			txtFactor2.Visible = false;
			txtFactor2.Enabled = false;
			cmbUnidad.Visible = false;
			cmbUnidad.Enabled = false;
			cmbTipo.Enabled = true;
			cmbTipo.SelectedIndex = -1;
			lbPrecioCosto.Text = "Precio con IGV";
			txtPrecioUnidad.Visible = true;
			lbPrecioCosto.Visible = true;
		}
		if (cmbCompraVenta.SelectedIndex == 2)
		{
			label8.Visible = true;
			txtFactor2.Visible = true;
			txtFactor2.Enabled = true;
			cmbUnidad.Visible = true;
			cmbUnidad.Enabled = true;
			txtPrecioUnidad.Visible = false;
			cmbTipo.SelectedValue = 9;
			cmbTipo.Enabled = false;
			lbPrecioCosto.Visible = false;
		}
	}

	private void dataGridView3_Click(object sender, EventArgs e)
	{
		dataGridView1.ClearSelection();
		dataGridView2.ClearSelection();
	}

	private void cmbUnidad_SelectionChangeCommitted(object sender, EventArgs e)
	{
		int UnidCompra = 0;
		if (cbUni2.SelectedIndex >= 0)
		{
			UnidCompra = admPro.getUnidadCompra(codProd);
			equi.CodUnidad = Convert.ToInt32(cbUni2.SelectedValue);
			if (equi.CodUnidad == UnidCompra)
			{
				if (cmbUnidad.Text == UnidadB && interrupto == 0)
				{
					interrupto++;
				}
				else if (interrupto != 0)
				{
				}
			}
		}
		else
		{
			MessageBox.Show("Seleccione Unidad", "Advertencia");
			cbUni1.Focus();
		}
	}

	private void txtPrecioUnidad_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosDecimales(e, txtPrecioUnidad);
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		foreach (DataGridViewRow filaC in (IEnumerable)dataGridView2.Rows)
		{
			string Codigo = filaC.Cells["codi"].Value.ToString();
			string UndCompras = filaC.Cells["des"].Value.ToString();
			if (!Enumerable.Any<DataGridViewRow>(dataGridView3.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => x.Cells["dataGridViewTextBoxColumn3"].Value.ToString() == UndCompras)))
			{
				MessageBox.Show("Falta agregar la unidad" + UndCompras + " de la tabla compras a la tabla equivalentes.");
				if (MessageBox.Show("Deseas quitar la unidad " + UndCompras + " de la lista de unidades de compra", "AVISO", MessageBoxButtons.YesNo) == DialogResult.Yes && admPro.deleteunidadequivalente(Convert.ToInt32(Codigo)))
				{
					MessageBox.Show("Los datos se Eliminaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaListaEquivalencias();
				}
				return;
			}
		}
		foreach (DataGridViewRow filaC2 in (IEnumerable)dataGridView1.Rows)
		{
			string Codigo2 = filaC2.Cells["codigo"].Value.ToString();
			string UndVentas = filaC2.Cells["unidad11"].Value.ToString();
			if (!Enumerable.Any<DataGridViewRow>(dataGridView3.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => x.Cells["dataGridViewTextBoxColumn3"].Value.ToString() == UndVentas)))
			{
				MessageBox.Show("Falta agregar la unidad " + UndVentas + " de la tabla ventas a la tabla equivalentess");
				if (MessageBox.Show("Deseas quitar la unidad " + UndVentas + " de la lista de unidades de Venta", "AVISO", MessageBoxButtons.YesNo) == DialogResult.Yes && admPro.deleteunidadequivalente(Convert.ToInt32(Codigo2)))
				{
					MessageBox.Show("Los datos se Eliminaron correctamente", "Unidad Equivalencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaListaEquivalencias();
				}
				return;
			}
		}
		Close();
	}

	private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dataGridView1.CurrentCell.ColumnIndex == 6)
		{
			ok.SOLONumeros(sender, e);
		}
	}

	private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dataGridView1_KeyPress;
			txtedit.KeyPress += dataGridView1_KeyPress;
			txtedit.KeyUp -= dataGridView1_KeyUp;
			txtedit.KeyUp += dataGridView1_KeyUp;
			txtedit.Leave -= dataGridView1_Leave;
			txtedit.Leave += dataGridView1_Leave;
		}
	}

	private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dataGridView1_Leave(object sender, EventArgs e)
	{
	}

	private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.RowCount > 0)
		{
			if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[precios1.Name].Value) == 0m)
			{
				dataGridView1.CurrentRow.Cells[precios1.Name].Value = precioIGV;
			}
			else if (MessageBox.Show("¿Permitir modificar el precio de la unidad?", "Unidad Equivalencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				int codEquiv = Convert.ToInt32(dataGridView1.CurrentRow.Cells[codigo.Name].Value);
				precioIGV = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[precios1.Name].Value);
				admPro.updateunidadequivalente(codEquiv, precioIGV);
			}
			else
			{
				dataGridView1.CurrentRow.Cells[precios1.Name].Value = precioIGV;
			}
		}
	}

	private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.RowCount > 0)
		{
			precioIGV = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[precios1.Name].Value);
		}
	}

	private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dataGridView2.SelectedRows.Count > 0 && dataGridView2.RowCount > 0)
		{
			precioIGV = Convert.ToDecimal(dataGridView2.CurrentRow.Cells[pre.Name].Value);
		}
	}

	private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dataGridView2.SelectedRows.Count > 0 && dataGridView2.RowCount > 0)
		{
			if (Convert.ToDecimal(dataGridView2.CurrentRow.Cells[pre.Name].Value) == 0m)
			{
				dataGridView2.CurrentRow.Cells[pre.Name].Value = precioIGV;
			}
			else if (MessageBox.Show("¿Permitir modificar el precio de la unidad?", "Unidad Equivalencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				int codEquiv = Convert.ToInt32(dataGridView2.CurrentRow.Cells[codi.Name].Value);
				precioIGV = Convert.ToDecimal(dataGridView2.CurrentRow.Cells[pre.Name].Value);
				admPro.updateunidadequivalente(codEquiv, precioIGV);
			}
			else
			{
				dataGridView2.CurrentRow.Cells[pre.Name].Value = precioIGV;
			}
		}
	}

	private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dataGridView2_KeyPress;
			txtedit.KeyPress += dataGridView2_KeyPress;
		}
	}

	private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dataGridView2.CurrentCell.ColumnIndex == 6)
		{
			ok.SOLONumeros(sender, e);
		}
	}

	private void frmUnidadEquivalente_FormClosed(object sender, FormClosedEventArgs e)
	{
	}

	private void frmUnidadEquivalente_FormClosing(object sender, FormClosingEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmUnidadEquivalente));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.label9 = new System.Windows.Forms.Label();
		this.dataGridView3 = new System.Windows.Forms.DataGridView();
		this.codtabla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.codi = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigoUnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.des = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fac = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codue = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.equ = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ctip = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipp = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.btnAgregar = new System.Windows.Forms.Button();
		this.txtCodProducto = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbUnidadBase = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.txtPrecio = new System.Windows.Forms.TextBox();
		this.label43 = new System.Windows.Forms.Label();
		this.btnCancel = new System.Windows.Forms.Button();
		this.label16 = new System.Windows.Forms.Label();
		this.btnOkU1 = new System.Windows.Forms.Button();
		this.label15 = new System.Windows.Forms.Label();
		this.txtFactor1 = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.cbUni1 = new System.Windows.Forms.ComboBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btnEliminarUnidad = new System.Windows.Forms.Button();
		this.btnAñadirUnidad = new System.Windows.Forms.Button();
		this.dgvEquivalentes = new System.Windows.Forms.DataGridView();
		this.codunidadequivalente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.factor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Precios = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbCompraVenta = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbTipo = new System.Windows.Forms.ComboBox();
		this.txtPrecioUnidad = new System.Windows.Forms.TextBox();
		this.cmbUnidad = new System.Windows.Forms.ComboBox();
		this.lbPrecioCosto = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.button2 = new System.Windows.Forms.Button();
		this.txtFactor2 = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.cbUni2 = new System.Windows.Forms.ComboBox();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigoundMed = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Factor1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUndEqui = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.equivalente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precios1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEquivalentes).BeginInit();
		this.groupBox5.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.imageList1.Images.SetKeyName(3, "Donate.ico");
		this.imageList1.Images.SetKeyName(4, "Add.png");
		this.imageList1.Images.SetKeyName(5, "Remove.png");
		this.imageList1.Images.SetKeyName(6, "Write Document.png");
		this.imageList1.Images.SetKeyName(7, "Save-icon.png");
		this.imageList1.Images.SetKeyName(8, "images.png");
		this.groupBox6.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.groupBox6.Controls.Add(this.label9);
		this.groupBox6.Controls.Add(this.dataGridView3);
		this.groupBox6.Controls.Add(this.label4);
		this.groupBox6.Controls.Add(this.label3);
		this.groupBox6.Controls.Add(this.dataGridView2);
		this.groupBox6.Controls.Add(this.btnEliminar);
		this.groupBox6.Controls.Add(this.dataGridView1);
		this.groupBox6.Controls.Add(this.btnAgregar);
		this.groupBox6.ForeColor = System.Drawing.Color.Black;
		this.groupBox6.Location = new System.Drawing.Point(12, 34);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(589, 545);
		this.groupBox6.TabIndex = 5;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Unidades equivalentes";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(6, 369);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(145, 13);
		this.label9.TabIndex = 17;
		this.label9.Text = "TABLA DE EQUIVALENCIAS";
		this.dataGridView3.AllowUserToAddRows = false;
		this.dataGridView3.AllowUserToDeleteRows = false;
		this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView3.Columns.AddRange(this.codtabla, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6, this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn8, this.dataGridViewTextBoxColumn9);
		this.dataGridView3.Location = new System.Drawing.Point(9, 385);
		this.dataGridView3.Name = "dataGridView3";
		this.dataGridView3.ReadOnly = true;
		this.dataGridView3.RowHeadersVisible = false;
		this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView3.Size = new System.Drawing.Size(547, 149);
		this.dataGridView3.TabIndex = 16;
		this.dataGridView3.Click += new System.EventHandler(dataGridView3_Click);
		this.codtabla.DataPropertyName = "codUnidadEquivalente";
		this.codtabla.HeaderText = "codigo";
		this.codtabla.Name = "codtabla";
		this.codtabla.ReadOnly = true;
		this.codtabla.Visible = false;
		this.dataGridViewTextBoxColumn2.DataPropertyName = "codUnidadMedida";
		this.dataGridViewTextBoxColumn2.HeaderText = "codigoUnd";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Visible = false;
		this.dataGridViewTextBoxColumn3.DataPropertyName = "descripcion";
		this.dataGridViewTextBoxColumn3.HeaderText = "Unidad";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.ReadOnly = true;
		this.dataGridViewTextBoxColumn3.Width = 150;
		this.dataGridViewTextBoxColumn4.DataPropertyName = "factor";
		this.dataGridViewTextBoxColumn4.HeaderText = "Factor";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.ReadOnly = true;
		this.dataGridViewTextBoxColumn5.DataPropertyName = "codUndEqui";
		this.dataGridViewTextBoxColumn5.HeaderText = "codUndEqui";
		this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
		this.dataGridViewTextBoxColumn5.ReadOnly = true;
		this.dataGridViewTextBoxColumn5.Visible = false;
		this.dataGridViewTextBoxColumn6.DataPropertyName = "equivalente";
		this.dataGridViewTextBoxColumn6.HeaderText = "Equivalente";
		this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
		this.dataGridViewTextBoxColumn6.ReadOnly = true;
		this.dataGridViewTextBoxColumn7.DataPropertyName = "Precio";
		this.dataGridViewTextBoxColumn7.HeaderText = "precios";
		this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
		this.dataGridViewTextBoxColumn7.ReadOnly = true;
		this.dataGridViewTextBoxColumn7.Visible = false;
		this.dataGridViewTextBoxColumn8.DataPropertyName = "codTipo";
		this.dataGridViewTextBoxColumn8.HeaderText = "codTipo";
		this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
		this.dataGridViewTextBoxColumn8.ReadOnly = true;
		this.dataGridViewTextBoxColumn8.Visible = false;
		this.dataGridViewTextBoxColumn9.DataPropertyName = "tip";
		this.dataGridViewTextBoxColumn9.HeaderText = "Tipo Precio";
		this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
		this.dataGridViewTextBoxColumn9.ReadOnly = true;
		this.dataGridViewTextBoxColumn9.Visible = false;
		this.dataGridViewTextBoxColumn9.Width = 120;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(6, 15);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(131, 13);
		this.label4.TabIndex = 15;
		this.label4.Text = "UNIDADES DE COMPRA";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(6, 194);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(118, 13);
		this.label3.TabIndex = 14;
		this.label3.Text = "UNIDADES DE VENTA";
		this.dataGridView2.AllowUserToAddRows = false;
		this.dataGridView2.AllowUserToDeleteRows = false;
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView2.Columns.AddRange(this.codi, this.codigoUnd, this.des, this.fac, this.codue, this.equ, this.pre, this.ctip, this.tipp);
		this.dataGridView2.Location = new System.Drawing.Point(6, 31);
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.RowHeadersVisible = false;
		this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView2.Size = new System.Drawing.Size(547, 149);
		this.dataGridView2.TabIndex = 13;
		this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView2_CellClick);
		this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView2_CellEndEdit);
		this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dataGridView2_EditingControlShowing);
		this.dataGridView2.Click += new System.EventHandler(dataGridView2_Click);
		this.dataGridView2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dataGridView2_KeyPress);
		this.codi.DataPropertyName = "codUnidadEquivalente";
		this.codi.HeaderText = "codigo";
		this.codi.Name = "codi";
		this.codi.ReadOnly = true;
		this.codi.Visible = false;
		this.codigoUnd.DataPropertyName = "codUnidadMedida";
		this.codigoUnd.HeaderText = "codigoUnd";
		this.codigoUnd.Name = "codigoUnd";
		this.codigoUnd.ReadOnly = true;
		this.codigoUnd.Visible = false;
		this.des.DataPropertyName = "descripcion";
		this.des.HeaderText = "Unidad";
		this.des.Name = "des";
		this.des.ReadOnly = true;
		this.des.Width = 150;
		this.fac.DataPropertyName = "factor";
		this.fac.HeaderText = "factor";
		this.fac.Name = "fac";
		this.fac.ReadOnly = true;
		this.fac.Visible = false;
		this.codue.DataPropertyName = "codUndEqui";
		this.codue.HeaderText = "codUndEqui";
		this.codue.Name = "codue";
		this.codue.ReadOnly = true;
		this.codue.Visible = false;
		this.equ.DataPropertyName = "equivalente";
		this.equ.HeaderText = "Equivalente";
		this.equ.Name = "equ";
		this.equ.ReadOnly = true;
		this.equ.Visible = false;
		this.pre.DataPropertyName = "Precio";
		this.pre.HeaderText = "Costo con IGV";
		this.pre.Name = "pre";
		this.ctip.DataPropertyName = "codTipo";
		this.ctip.HeaderText = "codTipo";
		this.ctip.Name = "ctip";
		this.ctip.ReadOnly = true;
		this.ctip.Visible = false;
		this.tipp.DataPropertyName = "tip";
		this.tipp.HeaderText = "Tipo Precio";
		this.tipp.Name = "tipp";
		this.tipp.ReadOnly = true;
		this.tipp.Width = 120;
		this.btnEliminar.BackColor = System.Drawing.Color.White;
		this.btnEliminar.ForeColor = System.Drawing.Color.Black;
		this.btnEliminar.ImageKey = "Remove.png";
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(559, 60);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(24, 22);
		this.btnEliminar.TabIndex = 12;
		this.btnEliminar.UseVisualStyleBackColor = false;
		this.btnEliminar.Click += new System.EventHandler(button6_Click);
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.codigo, this.codigoundMed, this.unidad1, this.unidad11, this.Factor1, this.codUndEqui, this.equivalente, this.precios1, this.codTipo, this.tipo);
		this.dataGridView1.Location = new System.Drawing.Point(5, 210);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(547, 149);
		this.dataGridView1.TabIndex = 11;
		this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
		this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
		this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
		this.dataGridView1.Click += new System.EventHandler(dataGridView1_Click);
		this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dataGridView1_KeyPress);
		this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyUp);
		this.dataGridView1.Leave += new System.EventHandler(dataGridView1_Leave);
		this.btnAgregar.BackColor = System.Drawing.Color.White;
		this.btnAgregar.ForeColor = System.Drawing.Color.Black;
		this.btnAgregar.ImageIndex = 4;
		this.btnAgregar.ImageList = this.imageList1;
		this.btnAgregar.Location = new System.Drawing.Point(559, 31);
		this.btnAgregar.Name = "btnAgregar";
		this.btnAgregar.Size = new System.Drawing.Size(24, 24);
		this.btnAgregar.TabIndex = 9;
		this.btnAgregar.UseVisualStyleBackColor = false;
		this.btnAgregar.Click += new System.EventHandler(button4_Click);
		this.txtCodProducto.BackColor = System.Drawing.Color.White;
		this.txtCodProducto.Enabled = false;
		this.txtCodProducto.ForeColor = System.Drawing.Color.Black;
		this.txtCodProducto.Location = new System.Drawing.Point(63, 6);
		this.txtCodProducto.Name = "txtCodProducto";
		this.txtCodProducto.Size = new System.Drawing.Size(66, 22);
		this.txtCodProducto.TabIndex = 7;
		this.txtCodProducto.Visible = false;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(9, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(48, 13);
		this.label1.TabIndex = 6;
		this.label1.Text = "Código:";
		this.label1.Visible = false;
		this.cmbUnidadBase.FormattingEnabled = true;
		this.cmbUnidadBase.Location = new System.Drawing.Point(222, 18);
		this.cmbUnidadBase.Name = "cmbUnidadBase";
		this.cmbUnidadBase.Size = new System.Drawing.Size(121, 21);
		this.cmbUnidadBase.TabIndex = 1;
		this.cmbUnidadBase.Tag = "1";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(135, 21);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(81, 13);
		this.label13.TabIndex = 0;
		this.label13.Text = "Unidad Base * :";
		this.groupBox3.Controls.Add(this.txtPrecio);
		this.groupBox3.Controls.Add(this.label43);
		this.groupBox3.Controls.Add(this.btnCancel);
		this.groupBox3.Controls.Add(this.label16);
		this.groupBox3.Controls.Add(this.btnOkU1);
		this.groupBox3.Controls.Add(this.label15);
		this.groupBox3.Controls.Add(this.txtFactor1);
		this.groupBox3.Controls.Add(this.label14);
		this.groupBox3.Controls.Add(this.cbUni1);
		this.groupBox3.Location = new System.Drawing.Point(3, 80);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(499, 86);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Equivalencia";
		this.groupBox3.Visible = false;
		this.txtPrecio.Location = new System.Drawing.Point(69, 58);
		this.txtPrecio.Name = "txtPrecio";
		this.txtPrecio.Size = new System.Drawing.Size(53, 20);
		this.txtPrecio.TabIndex = 29;
		this.txtPrecio.Text = "0.00";
		this.label43.AutoSize = true;
		this.label43.Location = new System.Drawing.Point(22, 58);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(40, 13);
		this.label43.TabIndex = 28;
		this.label43.Text = "Precio:";
		this.btnCancel.ImageIndex = 0;
		this.btnCancel.Location = new System.Drawing.Point(469, 26);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(24, 24);
		this.btnCancel.TabIndex = 27;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(12, 32);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(42, 13);
		this.label16.TabIndex = 26;
		this.label16.Text = "Un(a) 1";
		this.btnOkU1.ImageIndex = 1;
		this.btnOkU1.Location = new System.Drawing.Point(438, 24);
		this.btnOkU1.Name = "btnOkU1";
		this.btnOkU1.Size = new System.Drawing.Size(24, 24);
		this.btnOkU1.TabIndex = 25;
		this.btnOkU1.UseVisualStyleBackColor = true;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(316, 32);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(41, 13);
		this.label15.TabIndex = 5;
		this.label15.Text = "Unidad";
		this.txtFactor1.Location = new System.Drawing.Point(275, 29);
		this.txtFactor1.Name = "txtFactor1";
		this.txtFactor1.Size = new System.Drawing.Size(35, 20);
		this.txtFactor1.TabIndex = 4;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(201, 32);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(59, 13);
		this.label14.TabIndex = 3;
		this.label14.Text = "equivale a ";
		this.cbUni1.FormattingEnabled = true;
		this.cbUni1.Location = new System.Drawing.Point(60, 29);
		this.cbUni1.Name = "cbUni1";
		this.cbUni1.Size = new System.Drawing.Size(121, 21);
		this.cbUni1.TabIndex = 2;
		this.groupBox4.Controls.Add(this.btnEliminarUnidad);
		this.groupBox4.Controls.Add(this.btnAñadirUnidad);
		this.groupBox4.Controls.Add(this.dgvEquivalentes);
		this.groupBox4.Location = new System.Drawing.Point(6, 48);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(499, 148);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Unidades equivalentes";
		this.btnEliminarUnidad.ImageIndex = 5;
		this.btnEliminarUnidad.Location = new System.Drawing.Point(466, 118);
		this.btnEliminarUnidad.Name = "btnEliminarUnidad";
		this.btnEliminarUnidad.Size = new System.Drawing.Size(24, 24);
		this.btnEliminarUnidad.TabIndex = 10;
		this.btnEliminarUnidad.UseVisualStyleBackColor = true;
		this.btnAñadirUnidad.ImageIndex = 4;
		this.btnAñadirUnidad.Location = new System.Drawing.Point(466, 2);
		this.btnAñadirUnidad.Name = "btnAñadirUnidad";
		this.btnAñadirUnidad.Size = new System.Drawing.Size(24, 24);
		this.btnAñadirUnidad.TabIndex = 9;
		this.btnAñadirUnidad.UseVisualStyleBackColor = true;
		this.dgvEquivalentes.AllowUserToAddRows = false;
		this.dgvEquivalentes.AllowUserToDeleteRows = false;
		this.dgvEquivalentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvEquivalentes.Columns.AddRange(this.codunidadequivalente, this.unidad, this.factor, this.Precios);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvEquivalentes.DefaultCellStyle = dataGridViewCellStyle7;
		this.dgvEquivalentes.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dgvEquivalentes.Location = new System.Drawing.Point(6, 19);
		this.dgvEquivalentes.Name = "dgvEquivalentes";
		this.dgvEquivalentes.ReadOnly = true;
		this.dgvEquivalentes.RowHeadersVisible = false;
		this.dgvEquivalentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvEquivalentes.Size = new System.Drawing.Size(457, 123);
		this.dgvEquivalentes.TabIndex = 0;
		this.codunidadequivalente.DataPropertyName = "codUnidadEquivalente";
		this.codunidadequivalente.HeaderText = "Codigo";
		this.codunidadequivalente.Name = "codunidadequivalente";
		this.codunidadequivalente.ReadOnly = true;
		this.codunidadequivalente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidadequivalente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidadequivalente.Visible = false;
		this.unidad.DataPropertyName = "descripcion";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 250;
		this.factor.DataPropertyName = "factor";
		this.factor.HeaderText = "Factor";
		this.factor.Name = "factor";
		this.factor.ReadOnly = true;
		this.factor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.factor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Precios.DataPropertyName = "Precio";
		this.Precios.HeaderText = "Precios";
		this.Precios.Name = "Precios";
		this.Precios.ReadOnly = true;
		this.groupBox5.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.groupBox5.Controls.Add(this.label5);
		this.groupBox5.Controls.Add(this.comboBox1);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Controls.Add(this.cmbCompraVenta);
		this.groupBox5.Controls.Add(this.label2);
		this.groupBox5.Controls.Add(this.cmbTipo);
		this.groupBox5.Controls.Add(this.txtPrecioUnidad);
		this.groupBox5.Controls.Add(this.cmbUnidad);
		this.groupBox5.Controls.Add(this.lbPrecioCosto);
		this.groupBox5.Controls.Add(this.button1);
		this.groupBox5.Controls.Add(this.label6);
		this.groupBox5.Controls.Add(this.button2);
		this.groupBox5.Controls.Add(this.txtFactor2);
		this.groupBox5.Controls.Add(this.label8);
		this.groupBox5.Controls.Add(this.cbUni2);
		this.groupBox5.ForeColor = System.Drawing.Color.Black;
		this.groupBox5.Location = new System.Drawing.Point(39, 211);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(534, 205);
		this.groupBox5.TabIndex = 9;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Equivalencia";
		this.groupBox5.Visible = false;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.label5.ForeColor = System.Drawing.Color.Black;
		this.label5.Location = new System.Drawing.Point(6, 122);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(82, 13);
		this.label5.TabIndex = 35;
		this.label5.Text = "Tipo Moneda :";
		this.comboBox1.BackColor = System.Drawing.Color.White;
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.ForeColor = System.Drawing.Color.Black;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(100, 114);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(132, 21);
		this.comboBox1.TabIndex = 34;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.label7.ForeColor = System.Drawing.Color.Black;
		this.label7.Location = new System.Drawing.Point(19, 41);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(71, 13);
		this.label7.TabIndex = 33;
		this.label7.Text = "Discponible:";
		this.cmbCompraVenta.BackColor = System.Drawing.Color.White;
		this.cmbCompraVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbCompraVenta.ForeColor = System.Drawing.Color.Black;
		this.cmbCompraVenta.FormattingEnabled = true;
		this.cmbCompraVenta.Items.AddRange(new object[3] { "Compra / Ingreso", "Venta / Salida", "Equivalencia" });
		this.cmbCompraVenta.Location = new System.Drawing.Point(100, 33);
		this.cmbCompraVenta.Name = "cmbCompraVenta";
		this.cmbCompraVenta.Size = new System.Drawing.Size(132, 21);
		this.cmbCompraVenta.TabIndex = 32;
		this.cmbCompraVenta.SelectionChangeCommitted += new System.EventHandler(cmbCompraVenta_SelectionChangeCommitted);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Location = new System.Drawing.Point(21, 95);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(70, 13);
		this.label2.TabIndex = 31;
		this.label2.Text = "Tipo Precio :";
		this.cmbTipo.BackColor = System.Drawing.Color.White;
		this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipo.ForeColor = System.Drawing.Color.Black;
		this.cmbTipo.FormattingEnabled = true;
		this.cmbTipo.Location = new System.Drawing.Point(100, 87);
		this.cmbTipo.Name = "cmbTipo";
		this.cmbTipo.Size = new System.Drawing.Size(132, 21);
		this.cmbTipo.TabIndex = 30;
		this.txtPrecioUnidad.BackColor = System.Drawing.Color.White;
		this.txtPrecioUnidad.ForeColor = System.Drawing.Color.Black;
		this.txtPrecioUnidad.Location = new System.Drawing.Point(100, 144);
		this.txtPrecioUnidad.Name = "txtPrecioUnidad";
		this.txtPrecioUnidad.Size = new System.Drawing.Size(132, 22);
		this.txtPrecioUnidad.TabIndex = 29;
		this.txtPrecioUnidad.Text = "0.00";
		this.txtPrecioUnidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtPrecioUnidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecioUnidad_KeyPress);
		this.cmbUnidad.BackColor = System.Drawing.Color.White;
		this.cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbUnidad.ForeColor = System.Drawing.Color.Black;
		this.cmbUnidad.FormattingEnabled = true;
		this.cmbUnidad.Location = new System.Drawing.Point(382, 60);
		this.cmbUnidad.Name = "cmbUnidad";
		this.cmbUnidad.Size = new System.Drawing.Size(132, 21);
		this.cmbUnidad.TabIndex = 3;
		this.cmbUnidad.Tag = "1";
		this.cmbUnidad.SelectionChangeCommitted += new System.EventHandler(cmbUnidad_SelectionChangeCommitted);
		this.lbPrecioCosto.AutoSize = true;
		this.lbPrecioCosto.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.lbPrecioCosto.ForeColor = System.Drawing.Color.Black;
		this.lbPrecioCosto.Location = new System.Drawing.Point(6, 153);
		this.lbPrecioCosto.Name = "lbPrecioCosto";
		this.lbPrecioCosto.Size = new System.Drawing.Size(84, 13);
		this.lbPrecioCosto.TabIndex = 28;
		this.lbPrecioCosto.Text = "Precio con IGV:";
		this.button1.BackColor = System.Drawing.Color.White;
		this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.button1.ImageIndex = 0;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(431, 159);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(83, 24);
		this.button1.TabIndex = 27;
		this.button1.Text = "Cerrar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.label6.ForeColor = System.Drawing.Color.Black;
		this.label6.Location = new System.Drawing.Point(47, 68);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(43, 13);
		this.label6.TabIndex = 26;
		this.label6.Text = "Un(a) 1";
		this.button2.BackColor = System.Drawing.Color.White;
		this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.button2.ImageIndex = 1;
		this.button2.ImageList = this.imageList1;
		this.button2.Location = new System.Drawing.Point(342, 159);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(83, 24);
		this.button2.TabIndex = 25;
		this.button2.Text = "Aceptar";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.txtFactor2.BackColor = System.Drawing.Color.White;
		this.txtFactor2.ForeColor = System.Drawing.Color.Black;
		this.txtFactor2.Location = new System.Drawing.Point(307, 60);
		this.txtFactor2.Name = "txtFactor2";
		this.txtFactor2.Size = new System.Drawing.Size(66, 22);
		this.txtFactor2.TabIndex = 4;
		this.txtFactor2.Text = "0";
		this.txtFactor2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtFactor2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFactor2_KeyPress);
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.label8.ForeColor = System.Drawing.Color.Black;
		this.label8.Location = new System.Drawing.Point(242, 63);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 3;
		this.label8.Text = "Equivale a ";
		this.cbUni2.BackColor = System.Drawing.Color.White;
		this.cbUni2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbUni2.ForeColor = System.Drawing.Color.Black;
		this.cbUni2.FormattingEnabled = true;
		this.cbUni2.Location = new System.Drawing.Point(100, 60);
		this.cbUni2.Name = "cbUni2";
		this.cbUni2.Size = new System.Drawing.Size(132, 21);
		this.cbUni2.TabIndex = 2;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Write Document.png");
		this.imageList2.Images.SetKeyName(1, "New Document.png");
		this.imageList2.Images.SetKeyName(2, "Remove Document.png");
		this.imageList2.Images.SetKeyName(3, "document-print.png");
		this.imageList2.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList2.Images.SetKeyName(5, "exit.png");
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList2;
		this.btnSalir.Location = new System.Drawing.Point(533, 585);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 11;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.codigo.DataPropertyName = "codUnidadEquivalente";
		this.codigo.HeaderText = "codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.codigoundMed.DataPropertyName = "codUnidadMedida";
		this.codigoundMed.HeaderText = "codigoundMed";
		this.codigoundMed.Name = "codigoundMed";
		this.codigoundMed.ReadOnly = true;
		this.codigoundMed.Visible = false;
		this.unidad1.DataPropertyName = "descripcion";
		this.unidad1.HeaderText = "Unidad";
		this.unidad1.Name = "unidad1";
		this.unidad1.ReadOnly = true;
		this.unidad1.Width = 150;
		this.unidad11.DataPropertyName = "unidad11";
		this.unidad11.HeaderText = "Unidad11";
		this.unidad11.Name = "unidad11";
		this.unidad11.Visible = false;
		this.Factor1.DataPropertyName = "factor";
		this.Factor1.HeaderText = "Factor";
		this.Factor1.Name = "Factor1";
		this.Factor1.ReadOnly = true;
		this.Factor1.Visible = false;
		this.codUndEqui.DataPropertyName = "codUndEqui";
		this.codUndEqui.HeaderText = "codUndEqui";
		this.codUndEqui.Name = "codUndEqui";
		this.codUndEqui.ReadOnly = true;
		this.codUndEqui.Visible = false;
		this.equivalente.DataPropertyName = "equivalente";
		this.equivalente.HeaderText = "Equivalente";
		this.equivalente.Name = "equivalente";
		this.equivalente.ReadOnly = true;
		this.equivalente.Visible = false;
		this.precios1.DataPropertyName = "Precio";
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.precios1.DefaultCellStyle = dataGridViewCellStyle8;
		this.precios1.HeaderText = "Precio con IGV";
		this.precios1.Name = "precios1";
		this.codTipo.DataPropertyName = "codTipo";
		this.codTipo.HeaderText = "codTipo";
		this.codTipo.Name = "codTipo";
		this.codTipo.ReadOnly = true;
		this.codTipo.Visible = false;
		this.tipo.DataPropertyName = "tip";
		this.tipo.HeaderText = "Tipo Precio";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Width = 120;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(617, 627);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.txtCodProducto);
		base.Controls.Add(this.label1);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmUnidadEquivalente";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Unidades Equivalentes";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmUnidadEquivalente_FormClosing);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmUnidadEquivalente_FormClosed);
		base.Load += new System.EventHandler(frmUnidadEquivalente_Load);
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvEquivalentes).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
