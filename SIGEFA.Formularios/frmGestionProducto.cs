using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionProducto : Office2007Form
{
	public int Proceso = 0;

	public int Funcion = 0;

	private clsAdmProducto admPro = new clsAdmProducto();

	private clsAdmTipoArticulo admTip = new clsAdmTipoArticulo();

	private clsAdmFamilia admFam = new clsAdmFamilia();

	private clsAdmLinea admLin = new clsAdmLinea();

	private clsAdmGrupo admGru = new clsAdmGrupo();

	private clsAdmMarca admMar = new clsAdmMarca();

	private clsAdmUnidad admUni = new clsAdmUnidad();

	private clsAdmCaracteristica admCar = new clsAdmCaracteristica();

	private clsAdmVariante admVar = new clsAdmVariante();

	private clsAdmUsuario admUsu = new clsAdmUsuario();

	public clsProducto pro = new clsProducto();

	public clsUsuario usu = new clsUsuario();

	private clsCaracteristicaProducto carpro = new clsCaracteristicaProducto();

	private clsNotaProducto notapro = new clsNotaProducto();

	private clsUnidadEquivalente equi = new clsUnidadEquivalente();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private bool Validacion = true;

	private double IGV = frmLogin.Configuracion.IGV;

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox txtReferencia;

	private Label label2;

	private TextBox txtCodProducto;

	private Label label1;

	private CheckBox cbEstado;

	private TextBox txtNombre;

	private Label label3;

	private ImageList imageList1;

	private Button btnCancelar;

	private Button btnAceptar;

	private TabPage tabPage9;

	private DataGridView dgvAlmacenes;

	private TabPage tabPage7;

	private Label label29;

	private Label label28;

	private Label label27;

	private DataGridView dgvCaracteristicas;

	private TextBox txtVariante;

	private ComboBox cbVariante;

	private ComboBox cbCaracteristica;

	private TabPage tabPage6;

	private TabPage tabPage5;

	private DataGridView dgvCodigos;

	private DataGridViewTextBoxColumn code;

	private DataGridViewTextBoxColumn barcode;

	private DataGridViewTextBoxColumn descripcion;

	private TabPage tabPage4;

	private TextBox txtStockFuturo;

	private TextBox txtPorRecibir;

	private TextBox txtStockDisponible;

	private TextBox txtPorEntregar;

	private TextBox txtStockActual;

	private TextBox txtStockRep;

	private TextBox txtStockMax;

	private TextBox txtStockMin;

	private Label label26;

	private Label label25;

	private Label label24;

	private Label label23;

	private Label label22;

	private CheckBox cbStockRep;

	private CheckBox cbStockMax;

	private CheckBox cbStockMin;

	private TabPage tabPage3;

	private GroupBox groupBox2;

	private Label label15;

	private TextBox txtFactor1;

	private Label label14;

	private ComboBox cbUni1;

	private ComboBox cmbUnidadBase;

	private Label label13;

	private TabPage tabPage2;

	private TabPage tabPage1;

	private CheckBox cbDetraccion;

	private TextBox txtValorVenta;

	private TextBox txtRecargo;

	private Label label12;

	private Label label11;

	private System.Windows.Forms.TabControl tabControl1;

	private TextBox txtDscto;

	private Label label31;

	private TextBox txtPrecioOferta;

	private CheckBox cbOferta;

	private TextBox txtPfinal;

	private Label label32;

	private Label label34;

	private Label label33;

	private TextBox txtPrecioCompra;

	private Label label35;

	private CheckBox cbPVariable;

	private Label label38;

	private ComboBox cbControlStock;

	private Button btnadd;

	private Button btnremove;

	private DataGridViewTextBoxColumn codCaract;

	private DataGridViewTextBoxColumn caracteristica;

	private DataGridViewTextBoxColumn valor;

	private TextBox txtMontoDscto;

	private TextBox txtPorcDscto;

	private Label label40;

	private TextBox txtGanancia;

	private Label label41;

	private TextBox txtValorCompra;

	private TextBox txtFlete;

	private Label label42;

	private Label label39;

	private Button btnOkU1;

	private GroupBox groupBox3;

	private DataGridView dgvEquivalentes;

	private Button btnEliminarUnidad;

	private Button btnAñadirUnidad;

	private Label label16;

	private Button btnCancel;

	private DataGridViewTextBoxColumn codunidadequivalente;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn factor;

	private TextBox txtDescripcionNota;

	private DataGridView dgvNotas;

	private Button btnSaveNota;

	private Button btnDeleteNota;

	private Button btnAddNota;

	private DataGridViewTextBoxColumn codnotaproducto;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn nota;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn stock;

	private DataGridViewTextBoxColumn entregar;

	private DataGridViewTextBoxColumn disponible;

	private DataGridViewTextBoxColumn recibir;

	private DataGridViewTextBoxColumn futuro;

	private DataGridViewTextBoxColumn minimo;

	private DataGridViewTextBoxColumn maximo;

	private DataGridViewTextBoxColumn reposicion;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn precio;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn pneto;

	private LinkLabel lnkListasPrecios;

	private Button btnCambioProv;

	public DataGridView dgvProxProducto;

	private TabPage tabPage11;

	private TextBox txtPrecioCata;

	private Label label19;

	private Label label17;

	private TextBox txtComision;

	private Label label18;

	private ComboBox cbTipoArticulo;

	private Label label36;

	private ComboBox cbMarca;

	private Label label10;

	private TextBox txtUsuario;

	private TextBox txtModificacion;

	private TextBox txtFechaReg;

	private Label label9;

	private Label label8;

	private Label label7;

	private ComboBox cbGrupo;

	private Label label6;

	private ComboBox cbLinea;

	private Label label5;

	private ComboBox cbFamilia;

	private Label label4;

	private TabPage tabPage8;

	private Label label30;

	private CheckedListBox clbTipoOperacion;

	private Label label20;

	private RadioButton rdtExonerado;

	private RadioButton rdtInafecto;

	private RadioButton rdtGravado;

	public frmGestionProducto()
	{
		InitializeComponent();
	}

	private void frmGestionProducto_Load(object sender, EventArgs e)
	{
		CargaTipoArticulos();
		CargaFamilias();
		CargaUnidades(cmbUnidadBase);
		CargaMarcas();
		CargaCaracteristicas();
		CargaListaCaracteristicas();
		CargaListaEquivalencias();
		CargaProducto();
		CargaListaNotas();
		CargaStockProducto();
		CargaProductosProveedor();
		if (Funcion == 3)
		{
			sololectura();
		}
	}

	private void sololectura()
	{
		ext.sololectura(tabPage1.Controls);
		ext.sololectura(tabPage2.Controls);
		ext.sololectura(tabPage3.Controls);
		ext.sololectura(tabPage4.Controls);
		ext.sololectura(tabPage5.Controls);
		ext.sololectura(tabPage6.Controls);
		ext.sololectura(tabPage7.Controls);
		ext.sololectura(tabPage8.Controls);
		ext.sololectura(tabPage9.Controls);
		ext.sololectura(tabPage11.Controls);
		ext.sololectura(groupBox1.Controls);
		btnAceptar.Visible = false;
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (Proceso == 0 || !(txtNombre.Text != ""))
		{
			return;
		}
		Validacion = true;
		ValidarDatos(base.Controls);
		if (Validacion)
		{
			pro.CodUsuario = frmLogin.iCodUser;
			if (rdtGravado.Checked && !rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				pro.TipoImpuesto = 1;
				pro.CodSunat = "10";
			}
			else if (!rdtGravado.Checked && rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				pro.TipoImpuesto = 2;
				pro.CodSunat = "20";
			}
			else if (!rdtGravado.Checked && !rdtExonerado.Checked && rdtInafecto.Checked)
			{
				pro.TipoImpuesto = 3;
				pro.CodSunat = "30";
			}
			pro.Detraccion = cbDetraccion.Checked;
			pro.ValorVenta = Convert.ToDouble(txtValorVenta.Text);
			pro.PrecioProm = Convert.ToDecimal(txtPrecioCompra.Text);
			pro.Recargo = Convert.ToDouble(txtRecargo.Text);
			pro.PrecioVenta = Convert.ToDouble(txtPfinal.Text);
			pro.Oferta = cbOferta.Checked;
			if (txtPorcDscto.Text != "")
			{
				pro.PDescuento = Convert.ToDouble(txtPorcDscto.Text);
			}
			if (txtMontoDscto.Text != "")
			{
				pro.MontoDscto = Convert.ToDouble(txtMontoDscto.Text);
			}
			if (txtPrecioOferta.Text != "")
			{
				pro.PrecioOferta = Convert.ToDouble(txtPrecioOferta.Text);
			}
			pro.PrecioVariable = cbPVariable.Checked;
			if (txtDscto.Text != "")
			{
				pro.MaximoDscto = Convert.ToDouble(txtDscto.Text);
			}
			if (txtStockMin.Text != "")
			{
				pro.StockMinimo = Convert.ToDouble(txtStockMin.Text);
			}
			if (txtStockMax.Text != "")
			{
				pro.StockMaximo = Convert.ToDouble(txtStockMax.Text);
			}
			if (txtStockRep.Text != "")
			{
				pro.StockReposicion = Convert.ToDouble(txtStockRep.Text);
			}
			if (txtComision.Text != "")
			{
				pro.Comision = Convert.ToDecimal(txtComision.Text);
			}
			else
			{
				pro.Comision = 0m;
			}
			if (Proceso == 1)
			{
				if (admPro.insertproductoalmacen(pro))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Close();
				}
				else
				{
					MessageBox.Show("Error al guardar los datos", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else if (Proceso == 2)
			{
				if (admPro.updateproductoalmacen(pro))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Close();
				}
				else
				{
					MessageBox.Show("Error al actualizar los datos", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		else
		{
			MessageBox.Show("Debe completar todos los campos requeridos(*)", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void ValidarDatos(Control.ControlCollection Coleccion)
	{
		foreach (Control c in Coleccion)
		{
			if (Convert.ToInt32(c.Tag) == 1 && c.Enabled && c.Text == "")
			{
				c.BackColor = Color.LightPink;
				c.Focus();
				Validacion = false;
			}
			if (c.HasChildren)
			{
				ValidarDatos(c.Controls);
			}
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaProducto()
	{
		pro = admPro.CargaProducto(pro.CodProducto, frmLogin.iCodAlmacen);
		if (pro.PrecioProm == 0m)
		{
			txtPrecioCompra.Enabled = true;
			Proceso = 1;
		}
		else
		{
			txtPrecioCompra.Enabled = false;
			Proceso = 2;
		}
		txtCodProducto.Text = pro.CodProducto.ToString();
		txtReferencia.Text = pro.Referencia;
		txtNombre.Text = pro.Descripcion;
		txtFechaReg.Text = pro.FechaRegistro.ToShortDateString();
		txtModificacion.Text = pro.UltimaModificacion.ToShortDateString();
		usu = admUsu.MuestraUsuario(pro.CodUsuario);
		txtUsuario.Text = usu.Nombre;
		cbEstado.Checked = pro.Estado;
		cbTipoArticulo.SelectedValue = pro.CodTipoArticulo;
		cbFamilia.SelectedValue = pro.CodFamilia;
		CargaLineas(Convert.ToInt32(cbFamilia.SelectedValue));
		cbLinea.SelectedValue = pro.CodLinea;
		CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
		cbGrupo.SelectedValue = pro.CodGrupo;
		cbMarca.SelectedValue = pro.CodMarca;
		cmbUnidadBase.SelectedValue = pro.CodUnidadMedida;
		cbControlStock.SelectedIndex = pro.CodControlStock - 1;
		switch (pro.TipoImpuesto)
		{
		case 1:
			rdtGravado.Checked = true;
			rdtExonerado.Checked = false;
			rdtInafecto.Checked = false;
			break;
		case 2:
			rdtGravado.Checked = false;
			rdtExonerado.Checked = true;
			rdtInafecto.Checked = false;
			break;
		case 3:
			rdtGravado.Checked = false;
			rdtExonerado.Checked = false;
			rdtInafecto.Checked = true;
			break;
		}
		cbDetraccion.Checked = pro.Detraccion;
		txtComision.Text = Convert.ToString(pro.Comision);
		txtPrecioCata.Text = pro.PrecioCatalogo.ToString();
		txtPorRecibir.Text = pro.StockPorRecibir.ToString();
		txtStockFuturo.Text = pro.StockFuturo.ToString();
		txtStockActual.Text = pro.StockActual.ToString();
		if (Proceso == 2)
		{
			txtPrecioCompra.Text = pro.PrecioProm.ToString();
			txtValorCompra.Text = pro.ValorProm.ToString();
			txtRecargo.Text = pro.Recargo.ToString();
			txtValorVenta.Text = pro.ValorVenta.ToString();
			txtPfinal.Text = pro.PrecioVenta.ToString();
			cbOferta.Checked = pro.Oferta;
			if (pro.Oferta)
			{
				txtPorcDscto.Text = pro.PDescuento.ToString();
				txtMontoDscto.Text = pro.MontoDscto.ToString();
				txtPrecioOferta.Text = pro.PrecioOferta.ToString();
			}
			txtDscto.Text = pro.MaximoDscto.ToString();
			cbPVariable.Checked = pro.PrecioVariable;
			txtStockDisponible.Text = pro.StockDisponible.ToString();
			if (pro.StockMaximo != 0.0)
			{
				cbStockMax.Checked = true;
				txtStockMax.Text = pro.StockMaximo.ToString();
			}
			else
			{
				cbStockMax.Checked = false;
			}
			if (pro.StockMinimo != 0.0)
			{
				cbStockMin.Checked = true;
				txtStockMin.Text = pro.StockMinimo.ToString();
			}
			else
			{
				cbStockMin.Checked = false;
			}
			if (pro.StockReposicion != 0.0)
			{
				cbStockRep.Checked = true;
				txtStockRep.Text = pro.StockReposicion.ToString();
			}
			else
			{
				cbStockRep.Checked = false;
			}
		}
	}

	private void CargaTipoArticulos()
	{
		cbTipoArticulo.DataSource = admTip.MuestraTipoArticulos();
		cbTipoArticulo.DisplayMember = "descripcion";
		cbTipoArticulo.ValueMember = "codTipoArticulo";
		cbTipoArticulo.SelectedIndex = -1;
	}

	private void CargaFamilias()
	{
		cbFamilia.DataSource = admFam.MuestraFamilias();
		cbFamilia.DisplayMember = "descripcion";
		cbFamilia.ValueMember = "codFamilia";
		cbFamilia.SelectedIndex = -1;
	}

	private void CargaLineas(int codFami)
	{
		cbLinea.DataSource = admLin.MuestraLineas(codFami);
		cbLinea.DisplayMember = "descripcion";
		cbLinea.ValueMember = "codLinea";
		cbLinea.SelectedIndex = -1;
	}

	private void CargaGrupos(int codLine)
	{
		cbGrupo.DataSource = admGru.MuestraGrupos(codLine);
		cbGrupo.DisplayMember = "descripcion";
		cbGrupo.ValueMember = "codGrupo";
		cbGrupo.SelectedIndex = -1;
	}

	private void CargaMarcas()
	{
		cbMarca.DataSource = admMar.MuestraMarcas();
		cbMarca.DisplayMember = "descripcion";
		cbMarca.ValueMember = "codMarca";
		cbMarca.SelectedIndex = -1;
	}

	private void CargaUnidades(ComboBox combo)
	{
		combo.DataSource = admUni.MuestraUnidades();
		combo.DisplayMember = "descripcion";
		combo.ValueMember = "codUnidadMedida";
		combo.SelectedIndex = -1;
	}

	private void CargaCaracteristicas()
	{
		cbCaracteristica.DataSource = admCar.MuestraCaracteristicas();
		cbCaracteristica.DisplayMember = "descripcion";
		cbCaracteristica.ValueMember = "codCaracteristica";
		cbCaracteristica.SelectedIndex = -1;
	}

	public void CargaProductosProveedor()
	{
		dgvProxProducto.DataSource = admPro.MuestraProductosProveedor(pro.CodProducto, pro.CodAlmacen);
	}

	private void CargaVariantes(int codCara)
	{
		cbVariante.DataSource = admVar.MuestraVariantes(codCara);
		cbVariante.DisplayMember = "descripcion";
		cbVariante.ValueMember = "codVariante";
		cbVariante.SelectedIndex = -1;
	}

	private void cbFamilia_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLineas(Convert.ToInt32(cbFamilia.SelectedValue));
		cbLinea.Enabled = true;
	}

	private void cbLinea_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaGrupos(Convert.ToInt32(cbLinea.SelectedValue));
		cbGrupo.Enabled = true;
	}

	private void txtRecargo_TextChanged(object sender, EventArgs e)
	{
		if (!(txtRecargo.Text != ""))
		{
		}
	}

	private void txtRecargo_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (txtRecargo.Text != "" && txtPrecioCompra.Text != "")
		{
			if (rdtGravado.Checked && !rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				txtPfinal.Text = $"{Convert.ToDouble(txtValorVenta.Text) * (1.0 + IGV / 100.0):#,##0.00}";
				txtValorVenta.Text = $"{Convert.ToDouble(txtPrecioCompra.Text) / (1.0 + frmLogin.Configuracion.IGV / 100.0) * (1.0 + Convert.ToDouble(txtRecargo.Text) / 100.0):#,##0.00}";
			}
			else if (!rdtGravado.Checked && rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				txtPfinal.Text = $"{Convert.ToDouble(txtValorVenta.Text):#,##0.00}";
				txtValorVenta.Text = $"{Convert.ToDouble(txtPrecioCompra.Text) * (1.0 + Convert.ToDouble(txtRecargo.Text) / 100.0):#,##0.00}";
			}
			else if (!rdtGravado.Checked && !rdtExonerado.Checked && rdtInafecto.Checked)
			{
				txtPfinal.Text = $"{Convert.ToDouble(txtValorVenta.Text):#,##0.00}";
				txtValorVenta.Text = $"{Convert.ToDouble(txtPrecioCompra.Text) * (1.0 + Convert.ToDouble(txtRecargo.Text) / 100.0):#,##0.00}";
			}
		}
		ProcessTabKey(forward: true);
	}

	private void txtPsinIgv_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (txtValorVenta.Text != "" && txtPrecioCompra.Text != "")
		{
			if (rdtGravado.Checked && !rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				txtPfinal.Text = $"{Convert.ToDouble(txtValorVenta.Text) * (1.0 + IGV / 100.0):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) * (1.0 + IGV / 100.0) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
			else if (!rdtGravado.Checked && rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				txtPfinal.Text = $"{Convert.ToDouble(txtValorVenta.Text):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
			else if (!rdtGravado.Checked && !rdtExonerado.Checked && rdtInafecto.Checked)
			{
				txtPfinal.Text = $"{Convert.ToDouble(txtValorVenta.Text):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
		}
		ProcessTabKey(forward: true);
	}

	private void txtPfinal_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (txtPfinal.Text != "" && txtPrecioCompra.Text != "")
		{
			if (rdtGravado.Checked && !rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				txtValorVenta.Text = $"{Convert.ToDouble(txtPfinal.Text) / (1.0 + IGV / 100.0):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) * (1.0 + IGV / 100.0) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
			else if (!rdtGravado.Checked && rdtExonerado.Checked && !rdtInafecto.Checked)
			{
				txtValorVenta.Text = $"{Convert.ToDouble(txtPfinal.Text):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
			else if (!rdtGravado.Checked && !rdtExonerado.Checked && rdtInafecto.Checked)
			{
				txtValorVenta.Text = $"{Convert.ToDouble(txtPfinal.Text):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
		}
		ProcessTabKey(forward: true);
	}

	private void cbOferta_CheckedChanged(object sender, EventArgs e)
	{
		if (cbOferta.Checked)
		{
			txtPrecioOferta.Enabled = true;
			txtPorcDscto.Enabled = true;
			txtMontoDscto.Enabled = true;
			return;
		}
		txtPrecioOferta.Enabled = false;
		txtPrecioOferta.Text = "";
		txtPorcDscto.Enabled = false;
		txtPorcDscto.Text = "";
		txtMontoDscto.Enabled = false;
		txtMontoDscto.Text = "";
	}

	private void cmbUnidadBase_SelectionChangeCommitted(object sender, EventArgs e)
	{
		ComboBox c = (ComboBox)sender;
		c.GetItemText(c.SelectedItem);
		label15.Text = c.GetItemText(c.SelectedItem) + "(S)";
	}

	private void cbCaracteristica_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaVariantes(Convert.ToInt32(cbCaracteristica.SelectedValue));
		if (cbCaracteristica.SelectedIndex != -1)
		{
			if (cbVariante.Items.Count >= 1)
			{
				cbVariante.Enabled = true;
				txtVariante.Enabled = false;
			}
			else
			{
				txtVariante.Enabled = true;
				cbVariante.Enabled = false;
			}
		}
		else
		{
			cbVariante.Enabled = false;
			txtVariante.Enabled = false;
		}
	}

	private void btnadd_Click(object sender, EventArgs e)
	{
		if (cbCaracteristica.SelectedIndex != -1 && (cbVariante.SelectedIndex != -1 || txtVariante.Text != ""))
		{
			carpro.CodCaracteristica = Convert.ToInt32(cbCaracteristica.SelectedValue);
			carpro.CodProducto = Convert.ToInt32(pro.CodProducto);
			carpro.Valor = cbVariante.Text;
			carpro.CodUser = frmLogin.iCodUser;
			if (admPro.insertcaracteristica(carpro))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaCaracteristicas();
			}
		}
	}

	private void CargaListaCaracteristicas()
	{
		dgvCaracteristicas.DataSource = admPro.MuestraCaracteristicas(pro.CodProducto);
	}

	private void CargaListaNotas()
	{
		dgvNotas.DataSource = admPro.MuestraNotas(pro.CodProducto);
		dgvNotas.ClearSelection();
	}

	private void CargaStockProducto()
	{
		dgvAlmacenes.DataSource = admPro.StockProductoAlmacenes(frmLogin.iCodEmpresa, pro.CodProducto);
	}

	private void CargaListaEquivalencias()
	{
		dgvEquivalentes.DataSource = admPro.MuestraUnidadesEquivalentes(pro.CodProducto, frmLogin.iCodAlmacen);
	}

	private void btnremove_Click(object sender, EventArgs e)
	{
		if (dgvCaracteristicas.CurrentRow.Index != -1 && carpro.CodCaracteristicaProducto != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Caracteristicas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admPro.deletecaracteristica(carpro.CodCaracteristicaProducto))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Caracteristicas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaCaracteristicas();
			}
		}
	}

	private void txtPrecioOferta_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			if (txtPfinal.Text != "" && txtPrecioOferta.Text != "")
			{
				txtPorcDscto.Text = $"{(1.0 - Convert.ToDouble(txtPrecioOferta.Text) / Convert.ToDouble(txtPfinal.Text)) * 100.0:#,##0.00}";
				txtMontoDscto.Text = $"{Convert.ToDouble(txtPrecioOferta.Text) - Convert.ToDouble(txtPfinal.Text):#,##0.00}";
			}
			ProcessTabKey(forward: true);
		}
	}

	private void txtPorcDscto_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			if (txtPfinal.Text != "" && txtPorcDscto.Text != "")
			{
				txtPrecioOferta.Text = $"{Convert.ToDouble(txtPfinal.Text) * (1.0 - Convert.ToDouble(txtPorcDscto.Text) / 100.0):#,##0.00}";
				txtMontoDscto.Text = $"{Convert.ToDouble(txtPrecioOferta.Text) - Convert.ToDouble(txtPfinal.Text):#,##0.00}";
			}
			ProcessTabKey(forward: true);
		}
	}

	private void txtPrecioOferta_Leave(object sender, EventArgs e)
	{
		if (txtPfinal.Text != "" && txtPrecioOferta.Text != "")
		{
			txtPorcDscto.Text = $"{(1.0 - Convert.ToDouble(txtPrecioOferta.Text) / Convert.ToDouble(txtPfinal.Text)) * 100.0:#,##0.00}";
			txtMontoDscto.Text = $"{Convert.ToDouble(txtPrecioOferta.Text) - Convert.ToDouble(txtPfinal.Text):#,##0.00}";
		}
	}

	private void cbStockMin_CheckedChanged(object sender, EventArgs e)
	{
		if (cbStockMin.Checked)
		{
			txtStockMin.Enabled = true;
		}
		else
		{
			txtStockMin.Enabled = false;
		}
	}

	private void cbStockMax_CheckedChanged(object sender, EventArgs e)
	{
		if (cbStockMax.Checked)
		{
			txtStockMax.Enabled = true;
		}
		else
		{
			txtStockMax.Enabled = false;
		}
	}

	private void cbStockRep_CheckedChanged(object sender, EventArgs e)
	{
		if (cbStockRep.Checked)
		{
			txtStockRep.Enabled = true;
		}
		else
		{
			txtStockRep.Enabled = false;
		}
	}

	private void txtMontoDscto_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtPfinal.Text != "" && txtMontoDscto.Text != "")
		{
			txtPrecioOferta.Text = $"{Convert.ToDouble(txtPfinal.Text) - Convert.ToDouble(txtMontoDscto.Text):#,##0.00}";
			txtPorcDscto.Text = $"{(1.0 - Convert.ToDouble(txtPrecioOferta.Text) / Convert.ToDouble(txtPfinal.Text)) * 100.0:#,##0.00}";
		}
	}

	private void txtPorcDscto_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtDscto_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtDscto.Text != "" && Convert.ToDouble(txtPorcDscto.Text) > Convert.ToDouble(txtDscto.Text))
		{
			txtPorcDscto.Text = txtDscto.Text;
		}
	}

	private void txtPrecioCompra_TextChanged(object sender, EventArgs e)
	{
	}

	private void CalculaGanancia()
	{
		txtGanancia.Text = $"{(Convert.ToDouble(txtValorVenta.Text) - Convert.ToDouble(txtValorCompra.Text)) * 0.98:#,##0.00}";
	}

	private void txtFlete_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			if (txtValorCompra.Text != "" && txtFlete.Text == "")
			{
				txtFlete.Text = "0.00";
			}
			ProcessTabKey(forward: true);
		}
	}

	private void txtValorCompra_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			if (txtValorCompra.Text != "" && txtFlete.Text == "")
			{
				txtFlete.Text = "0.00";
			}
			ProcessTabKey(forward: true);
		}
	}

	private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r' || !(txtPrecioCompra.Text != ""))
		{
			return;
		}
		if (txtFlete.Text == "")
		{
			txtFlete.Text = "0.00";
		}
		if (rdtGravado.Checked)
		{
			txtValorCompra.Text = $"{Convert.ToDouble(txtPrecioCompra.Text) / (1.0 + IGV / 100.0) - Convert.ToDouble(txtFlete.Text):#,##0.00}";
		}
		else
		{
			txtValorCompra.Text = $"{Convert.ToDouble(txtPrecioCompra.Text) - Convert.ToDouble(txtFlete.Text):#,##0.00}";
		}
		if (txtPfinal.Text != "")
		{
			if (rdtGravado.Checked)
			{
				txtValorVenta.Text = $"{Convert.ToDouble(txtPfinal.Text) / (1.0 + IGV / 100.0):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) * (1.0 + IGV / 100.0) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
			else
			{
				txtValorVenta.Text = $"{Convert.ToDouble(txtPfinal.Text):#,##0.00}";
				txtRecargo.Text = $"{(Convert.ToDouble(txtValorVenta.Text) / Convert.ToDouble(txtPrecioCompra.Text) - 1.0) * 100.0:#,##0.00}";
			}
		}
		ProcessTabKey(forward: true);
	}

	private void btnOkU1_Click(object sender, EventArgs e)
	{
		if (cbUni1.SelectedIndex == -1)
		{
			MessageBox.Show("Seleccione Unidad", "Advertencia");
			cbUni1.Focus();
		}
		else if (txtFactor1.Text.Equals(""))
		{
			MessageBox.Show("Ingrese factor de conversión", "Advertencia");
			txtFactor1.Focus();
		}
		else if (cbUni1.SelectedIndex != -1 && txtFactor1.Text != "")
		{
			equi.CodProducto = Convert.ToInt32(pro.CodProducto);
			equi.CodUnidad = Convert.ToInt32(cbUni1.SelectedValue);
			equi.Factor = Convert.ToDecimal(txtFactor1.Text);
			equi.CodUser = frmLogin.iCodUser;
			if (admPro.insertunidadequivalente(equi, 0))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaEquivalencias();
				btnCancel.PerformClick();
			}
		}
	}

	private void btnAñadirUnidad_Click(object sender, EventArgs e)
	{
		groupBox2.Visible = true;
		groupBox3.Visible = false;
		CargaUnidades(cbUni1);
		label15.Text = cmbUnidadBase.Text + "(S)";
	}

	private void btnEliminarUnidad_Click(object sender, EventArgs e)
	{
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		groupBox2.Visible = false;
		groupBox3.Visible = true;
		CargaUnidades(cbUni1);
		txtFactor1.Text = "";
		label15.Text = "Unidad";
	}

	private void btnAddNota_Click(object sender, EventArgs e)
	{
		txtDescripcionNota.Text = "";
		txtDescripcionNota.ReadOnly = false;
		btnSaveNota.Enabled = true;
	}

	private void btnDeleteNota_Click(object sender, EventArgs e)
	{
		if (dgvNotas.CurrentRow.Index != -1 && notapro.CodNotaProducto != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admPro.deletenota(notapro.CodNotaProducto))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Caracteristicas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaNotas();
				txtDescripcionNota.Text = "";
				txtDescripcionNota.ReadOnly = true;
				notapro.CodNotaProducto = 0;
				btnSaveNota.Enabled = false;
			}
		}
	}

	private void dgvNotas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvNotas.Rows.Count >= 1 && e.Row.Selected)
		{
			notapro.CodNotaProducto = Convert.ToInt32(e.Row.Cells[codnotaproducto.Name].Value);
			txtDescripcionNota.Text = e.Row.Cells[nota.Name].Value.ToString();
		}
	}

	private void dgvCaracteristicas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvCaracteristicas.Rows.Count >= 1 && e.Row.Selected)
		{
			carpro.CodCaracteristicaProducto = Convert.ToInt32(e.Row.Cells[codCaract.Name].Value);
		}
	}

	private void btnSaveNota_Click(object sender, EventArgs e)
	{
		if (txtDescripcionNota.Text != "")
		{
			notapro.CodProducto = Convert.ToInt32(pro.CodProducto);
			notapro.Nota = txtDescripcionNota.Text;
			notapro.CodUser = frmLogin.iCodUser;
			if (admPro.insertnota(notapro))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaNotas();
				txtDescripcionNota.Text = "";
				txtDescripcionNota.ReadOnly = true;
				notapro.CodNotaProducto = 0;
				btnSaveNota.Enabled = false;
			}
		}
	}

	private void frmGestionProducto_Shown(object sender, EventArgs e)
	{
		tabControl1.Controls.Remove(tabPage5);
		tabControl1.Controls.Remove(tabPage3);
		tabControl1.Controls.Remove(tabPage8);
	}

	private void lnkListasPrecios_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (!(txtStockActual.Text == ""))
		{
			frmUnidadEquivalente frm = new frmUnidadEquivalente();
			frm.codProd = pro.CodProducto;
			frm.ShowDialog();
		}
	}

	private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tabControl1.SelectedTab == tabPage2)
		{
			btnCambioProv.Visible = true;
		}
		else
		{
			btnCambioProv.Visible = false;
		}
	}

	private void btnCambioProv_Click(object sender, EventArgs e)
	{
		frmCambioProveedor frm = new frmCambioProveedor();
		frm.CodProveedor = Convert.ToInt32(dgvProxProducto.CurrentRow.Cells[codigo.Name].Value);
		frm.CodProducto = Convert.ToInt32(txtCodProducto.Text);
		frm.Procede = 1;
		frm.Proceso = 2;
		frm.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionProducto));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbEstado = new System.Windows.Forms.CheckBox();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodProducto = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.tabPage9 = new System.Windows.Forms.TabPage();
		this.dgvAlmacenes = new System.Windows.Forms.DataGridView();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.entregar = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.disponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recibir = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.futuro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.minimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.maximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.reposicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage7 = new System.Windows.Forms.TabPage();
		this.btnremove = new System.Windows.Forms.Button();
		this.btnadd = new System.Windows.Forms.Button();
		this.label29 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.dgvCaracteristicas = new System.Windows.Forms.DataGridView();
		this.codCaract = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.caracteristica = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtVariante = new System.Windows.Forms.TextBox();
		this.cbVariante = new System.Windows.Forms.ComboBox();
		this.cbCaracteristica = new System.Windows.Forms.ComboBox();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.btnSaveNota = new System.Windows.Forms.Button();
		this.btnDeleteNota = new System.Windows.Forms.Button();
		this.btnAddNota = new System.Windows.Forms.Button();
		this.dgvNotas = new System.Windows.Forms.DataGridView();
		this.codnotaproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtDescripcionNota = new System.Windows.Forms.TextBox();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.dgvCodigos = new System.Windows.Forms.DataGridView();
		this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.label38 = new System.Windows.Forms.Label();
		this.cbControlStock = new System.Windows.Forms.ComboBox();
		this.txtStockFuturo = new System.Windows.Forms.TextBox();
		this.txtPorRecibir = new System.Windows.Forms.TextBox();
		this.txtStockDisponible = new System.Windows.Forms.TextBox();
		this.txtPorEntregar = new System.Windows.Forms.TextBox();
		this.txtStockActual = new System.Windows.Forms.TextBox();
		this.txtStockRep = new System.Windows.Forms.TextBox();
		this.txtStockMax = new System.Windows.Forms.TextBox();
		this.txtStockMin = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.cbStockRep = new System.Windows.Forms.CheckBox();
		this.cbStockMax = new System.Windows.Forms.CheckBox();
		this.cbStockMin = new System.Windows.Forms.CheckBox();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.cmbUnidadBase = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnCancel = new System.Windows.Forms.Button();
		this.label16 = new System.Windows.Forms.Label();
		this.btnOkU1 = new System.Windows.Forms.Button();
		this.label15 = new System.Windows.Forms.Label();
		this.txtFactor1 = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.cbUni1 = new System.Windows.Forms.ComboBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnEliminarUnidad = new System.Windows.Forms.Button();
		this.btnAñadirUnidad = new System.Windows.Forms.Button();
		this.dgvEquivalentes = new System.Windows.Forms.DataGridView();
		this.codunidadequivalente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.factor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dgvProxProducto = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pneto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.lnkListasPrecios = new System.Windows.Forms.LinkLabel();
		this.label39 = new System.Windows.Forms.Label();
		this.txtFlete = new System.Windows.Forms.TextBox();
		this.label42 = new System.Windows.Forms.Label();
		this.txtValorCompra = new System.Windows.Forms.TextBox();
		this.label41 = new System.Windows.Forms.Label();
		this.txtGanancia = new System.Windows.Forms.TextBox();
		this.txtMontoDscto = new System.Windows.Forms.TextBox();
		this.txtPorcDscto = new System.Windows.Forms.TextBox();
		this.label40 = new System.Windows.Forms.Label();
		this.txtPrecioCompra = new System.Windows.Forms.TextBox();
		this.label35 = new System.Windows.Forms.Label();
		this.cbPVariable = new System.Windows.Forms.CheckBox();
		this.label34 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.txtPfinal = new System.Windows.Forms.TextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.txtPrecioOferta = new System.Windows.Forms.TextBox();
		this.cbOferta = new System.Windows.Forms.CheckBox();
		this.cbDetraccion = new System.Windows.Forms.CheckBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.txtRecargo = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage11 = new System.Windows.Forms.TabPage();
		this.txtPrecioCata = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.txtComision = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.cbTipoArticulo = new System.Windows.Forms.ComboBox();
		this.label36 = new System.Windows.Forms.Label();
		this.cbMarca = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtUsuario = new System.Windows.Forms.TextBox();
		this.txtModificacion = new System.Windows.Forms.TextBox();
		this.txtFechaReg = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.cbGrupo = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.cbLinea = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.cbFamilia = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tabPage8 = new System.Windows.Forms.TabPage();
		this.label30 = new System.Windows.Forms.Label();
		this.clbTipoOperacion = new System.Windows.Forms.CheckedListBox();
		this.btnCambioProv = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.label20 = new System.Windows.Forms.Label();
		this.rdtExonerado = new System.Windows.Forms.RadioButton();
		this.rdtInafecto = new System.Windows.Forms.RadioButton();
		this.rdtGravado = new System.Windows.Forms.RadioButton();
		this.groupBox1.SuspendLayout();
		this.tabPage9.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).BeginInit();
		this.tabPage7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCaracteristicas).BeginInit();
		this.tabPage6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNotas).BeginInit();
		this.tabPage5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCodigos).BeginInit();
		this.tabPage4.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEquivalentes).BeginInit();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProxProducto).BeginInit();
		this.tabPage1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage11.SuspendLayout();
		this.tabPage8.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cbEstado);
		this.groupBox1.Controls.Add(this.txtNombre);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtReferencia);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtCodProducto);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(519, 67);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos Generales";
		this.cbEstado.AutoSize = true;
		this.cbEstado.Enabled = false;
		this.cbEstado.Location = new System.Drawing.Point(454, 34);
		this.cbEstado.Name = "cbEstado";
		this.cbEstado.Size = new System.Drawing.Size(56, 17);
		this.cbEstado.TabIndex = 6;
		this.cbEstado.Text = "Activo";
		this.cbEstado.UseVisualStyleBackColor = true;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Enabled = false;
		this.txtNombre.Location = new System.Drawing.Point(187, 32);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(261, 20);
		this.txtNombre.TabIndex = 5;
		this.txtNombre.Tag = "1";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(184, 16);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(57, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Nombre * :";
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Enabled = false;
		this.txtReferencia.Location = new System.Drawing.Point(81, 32);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.Size = new System.Drawing.Size(100, 20);
		this.txtReferencia.TabIndex = 3;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(78, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(62, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Referencia:";
		this.txtCodProducto.Enabled = false;
		this.txtCodProducto.Location = new System.Drawing.Point(9, 32);
		this.txtCodProducto.Name = "txtCodProducto";
		this.txtCodProducto.Size = new System.Drawing.Size(66, 20);
		this.txtCodProducto.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Código:";
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
		this.tabPage9.Controls.Add(this.dgvAlmacenes);
		this.tabPage9.Location = new System.Drawing.Point(4, 42);
		this.tabPage9.Name = "tabPage9";
		this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage9.Size = new System.Drawing.Size(511, 202);
		this.tabPage9.TabIndex = 8;
		this.tabPage9.Text = "Almacenes";
		this.tabPage9.UseVisualStyleBackColor = true;
		this.dgvAlmacenes.AllowUserToAddRows = false;
		this.dgvAlmacenes.AllowUserToDeleteRows = false;
		this.dgvAlmacenes.AllowUserToResizeRows = false;
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvAlmacenes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
		this.dgvAlmacenes.Columns.AddRange(this.almacen, this.stock, this.entregar, this.disponible, this.recibir, this.futuro, this.minimo, this.maximo, this.reposicion);
		this.dgvAlmacenes.Location = new System.Drawing.Point(4, 6);
		this.dgvAlmacenes.Name = "dgvAlmacenes";
		this.dgvAlmacenes.ReadOnly = true;
		this.dgvAlmacenes.RowHeadersVisible = false;
		this.dgvAlmacenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvAlmacenes.Size = new System.Drawing.Size(502, 192);
		this.dgvAlmacenes.TabIndex = 1;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.almacen.Width = 200;
		this.stock.DataPropertyName = "stockactual";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.stock.DefaultCellStyle = dataGridViewCellStyle18;
		this.stock.HeaderText = "Stock Actual";
		this.stock.Name = "stock";
		this.stock.ReadOnly = true;
		this.stock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.stock.Width = 80;
		this.entregar.DataPropertyName = "entregar";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.entregar.DefaultCellStyle = dataGridViewCellStyle19;
		this.entregar.HeaderText = "P. Entregar";
		this.entregar.Name = "entregar";
		this.entregar.ReadOnly = true;
		this.entregar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.entregar.Width = 80;
		this.disponible.DataPropertyName = "stockdisponible";
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.disponible.DefaultCellStyle = dataGridViewCellStyle20;
		this.disponible.HeaderText = "Stock Disp.";
		this.disponible.Name = "disponible";
		this.disponible.ReadOnly = true;
		this.disponible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.disponible.Width = 80;
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.recibir.DefaultCellStyle = dataGridViewCellStyle21;
		this.recibir.HeaderText = "P. Recibir";
		this.recibir.Name = "recibir";
		this.recibir.ReadOnly = true;
		this.recibir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.recibir.Width = 80;
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.futuro.DefaultCellStyle = dataGridViewCellStyle22;
		this.futuro.HeaderText = "Stock Futuro";
		this.futuro.Name = "futuro";
		this.futuro.ReadOnly = true;
		this.futuro.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.futuro.Width = 80;
		this.minimo.DataPropertyName = "stockminimo";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.minimo.DefaultCellStyle = dataGridViewCellStyle23;
		this.minimo.HeaderText = "Stock Minimo";
		this.minimo.Name = "minimo";
		this.minimo.ReadOnly = true;
		this.minimo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.maximo.DataPropertyName = "stockmaximo";
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.maximo.DefaultCellStyle = dataGridViewCellStyle24;
		this.maximo.HeaderText = "Stock Maximo";
		this.maximo.Name = "maximo";
		this.maximo.ReadOnly = true;
		this.maximo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.reposicion.DataPropertyName = "stockreposicion";
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.reposicion.DefaultCellStyle = dataGridViewCellStyle25;
		this.reposicion.HeaderText = "Stock Reposicion";
		this.reposicion.Name = "reposicion";
		this.reposicion.ReadOnly = true;
		this.reposicion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.tabPage7.Controls.Add(this.btnremove);
		this.tabPage7.Controls.Add(this.btnadd);
		this.tabPage7.Controls.Add(this.label29);
		this.tabPage7.Controls.Add(this.label28);
		this.tabPage7.Controls.Add(this.label27);
		this.tabPage7.Controls.Add(this.dgvCaracteristicas);
		this.tabPage7.Controls.Add(this.txtVariante);
		this.tabPage7.Controls.Add(this.cbVariante);
		this.tabPage7.Controls.Add(this.cbCaracteristica);
		this.tabPage7.Location = new System.Drawing.Point(4, 42);
		this.tabPage7.Name = "tabPage7";
		this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage7.Size = new System.Drawing.Size(511, 202);
		this.tabPage7.TabIndex = 6;
		this.tabPage7.Text = "Caracteristicas";
		this.tabPage7.UseVisualStyleBackColor = true;
		this.btnremove.ImageIndex = 5;
		this.btnremove.ImageList = this.imageList1;
		this.btnremove.Location = new System.Drawing.Point(402, 36);
		this.btnremove.Name = "btnremove";
		this.btnremove.Size = new System.Drawing.Size(24, 24);
		this.btnremove.TabIndex = 8;
		this.btnremove.UseVisualStyleBackColor = true;
		this.btnremove.Click += new System.EventHandler(btnremove_Click);
		this.btnadd.ImageIndex = 4;
		this.btnadd.ImageList = this.imageList1;
		this.btnadd.Location = new System.Drawing.Point(402, 9);
		this.btnadd.Name = "btnadd";
		this.btnadd.Size = new System.Drawing.Size(24, 24);
		this.btnadd.TabIndex = 7;
		this.btnadd.UseVisualStyleBackColor = true;
		this.btnadd.Click += new System.EventHandler(btnadd_Click);
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(64, 42);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(37, 13);
		this.label29.TabIndex = 6;
		this.label29.Text = "Valor :";
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(213, 15);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(58, 13);
		this.label28.TabIndex = 5;
		this.label28.Text = "Varieante :";
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(24, 15);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(77, 13);
		this.label27.TabIndex = 4;
		this.label27.Text = "Caracteristica :";
		this.dgvCaracteristicas.AllowUserToAddRows = false;
		this.dgvCaracteristicas.AllowUserToDeleteRows = false;
		this.dgvCaracteristicas.AllowUserToResizeColumns = false;
		this.dgvCaracteristicas.AllowUserToResizeRows = false;
		this.dgvCaracteristicas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCaracteristicas.Columns.AddRange(this.codCaract, this.caracteristica, this.valor);
		this.dgvCaracteristicas.Location = new System.Drawing.Point(6, 65);
		this.dgvCaracteristicas.Name = "dgvCaracteristicas";
		this.dgvCaracteristicas.ReadOnly = true;
		this.dgvCaracteristicas.RowHeadersVisible = false;
		this.dgvCaracteristicas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCaracteristicas.Size = new System.Drawing.Size(499, 133);
		this.dgvCaracteristicas.TabIndex = 3;
		this.dgvCaracteristicas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCaracteristicas_RowStateChanged);
		this.codCaract.DataPropertyName = "codCaracteristicaProducto";
		this.codCaract.HeaderText = "Codigo";
		this.codCaract.Name = "codCaract";
		this.codCaract.ReadOnly = true;
		this.codCaract.Visible = false;
		this.caracteristica.DataPropertyName = "descripcion";
		this.caracteristica.HeaderText = "Caracteristica";
		this.caracteristica.Name = "caracteristica";
		this.caracteristica.ReadOnly = true;
		this.caracteristica.Width = 200;
		this.valor.DataPropertyName = "valor";
		this.valor.HeaderText = "Valor";
		this.valor.Name = "valor";
		this.valor.ReadOnly = true;
		this.valor.Width = 280;
		this.txtVariante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtVariante.Enabled = false;
		this.txtVariante.Location = new System.Drawing.Point(107, 39);
		this.txtVariante.Name = "txtVariante";
		this.txtVariante.Size = new System.Drawing.Size(270, 20);
		this.txtVariante.TabIndex = 2;
		this.cbVariante.Enabled = false;
		this.cbVariante.FormattingEnabled = true;
		this.cbVariante.Location = new System.Drawing.Point(277, 12);
		this.cbVariante.Name = "cbVariante";
		this.cbVariante.Size = new System.Drawing.Size(100, 21);
		this.cbVariante.TabIndex = 1;
		this.cbCaracteristica.FormattingEnabled = true;
		this.cbCaracteristica.Location = new System.Drawing.Point(107, 12);
		this.cbCaracteristica.Name = "cbCaracteristica";
		this.cbCaracteristica.Size = new System.Drawing.Size(100, 21);
		this.cbCaracteristica.TabIndex = 0;
		this.cbCaracteristica.SelectionChangeCommitted += new System.EventHandler(cbCaracteristica_SelectionChangeCommitted);
		this.tabPage6.Controls.Add(this.btnSaveNota);
		this.tabPage6.Controls.Add(this.btnDeleteNota);
		this.tabPage6.Controls.Add(this.btnAddNota);
		this.tabPage6.Controls.Add(this.dgvNotas);
		this.tabPage6.Controls.Add(this.txtDescripcionNota);
		this.tabPage6.Location = new System.Drawing.Point(4, 42);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage6.Size = new System.Drawing.Size(511, 202);
		this.tabPage6.TabIndex = 5;
		this.tabPage6.Text = "Notas";
		this.tabPage6.UseVisualStyleBackColor = true;
		this.btnSaveNota.BackColor = System.Drawing.Color.Transparent;
		this.btnSaveNota.Enabled = false;
		this.btnSaveNota.ImageIndex = 7;
		this.btnSaveNota.ImageList = this.imageList1;
		this.btnSaveNota.Location = new System.Drawing.Point(481, 131);
		this.btnSaveNota.Name = "btnSaveNota";
		this.btnSaveNota.Size = new System.Drawing.Size(24, 24);
		this.btnSaveNota.TabIndex = 12;
		this.btnSaveNota.UseVisualStyleBackColor = false;
		this.btnSaveNota.Click += new System.EventHandler(btnSaveNota_Click);
		this.btnDeleteNota.BackColor = System.Drawing.Color.Transparent;
		this.btnDeleteNota.ImageIndex = 5;
		this.btnDeleteNota.ImageList = this.imageList1;
		this.btnDeleteNota.Location = new System.Drawing.Point(481, 36);
		this.btnDeleteNota.Name = "btnDeleteNota";
		this.btnDeleteNota.Size = new System.Drawing.Size(24, 24);
		this.btnDeleteNota.TabIndex = 10;
		this.btnDeleteNota.UseVisualStyleBackColor = false;
		this.btnDeleteNota.Click += new System.EventHandler(btnDeleteNota_Click);
		this.btnAddNota.BackColor = System.Drawing.Color.Transparent;
		this.btnAddNota.ImageIndex = 4;
		this.btnAddNota.ImageList = this.imageList1;
		this.btnAddNota.Location = new System.Drawing.Point(481, 6);
		this.btnAddNota.Name = "btnAddNota";
		this.btnAddNota.Size = new System.Drawing.Size(24, 24);
		this.btnAddNota.TabIndex = 9;
		this.btnAddNota.UseVisualStyleBackColor = false;
		this.btnAddNota.Click += new System.EventHandler(btnAddNota_Click);
		this.dgvNotas.AllowUserToAddRows = false;
		this.dgvNotas.AllowUserToDeleteRows = false;
		this.dgvNotas.AllowUserToResizeColumns = false;
		this.dgvNotas.AllowUserToResizeRows = false;
		this.dgvNotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvNotas.Columns.AddRange(this.codnotaproducto, this.usuario, this.nota);
		this.dgvNotas.Location = new System.Drawing.Point(6, 6);
		this.dgvNotas.Name = "dgvNotas";
		this.dgvNotas.ReadOnly = true;
		this.dgvNotas.RowHeadersVisible = false;
		this.dgvNotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNotas.Size = new System.Drawing.Size(469, 119);
		this.dgvNotas.TabIndex = 4;
		this.dgvNotas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvNotas_RowStateChanged);
		this.codnotaproducto.DataPropertyName = "codNota";
		this.codnotaproducto.HeaderText = "Codigo";
		this.codnotaproducto.Name = "codnotaproducto";
		this.codnotaproducto.ReadOnly = true;
		this.codnotaproducto.Visible = false;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Usuario";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.nota.DataPropertyName = "nota";
		this.nota.HeaderText = "Nota";
		this.nota.Name = "nota";
		this.nota.ReadOnly = true;
		this.nota.Width = 400;
		this.txtDescripcionNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcionNota.Location = new System.Drawing.Point(5, 131);
		this.txtDescripcionNota.MaxLength = 500;
		this.txtDescripcionNota.Multiline = true;
		this.txtDescripcionNota.Name = "txtDescripcionNota";
		this.txtDescripcionNota.ReadOnly = true;
		this.txtDescripcionNota.Size = new System.Drawing.Size(470, 65);
		this.txtDescripcionNota.TabIndex = 0;
		this.tabPage5.Controls.Add(this.dgvCodigos);
		this.tabPage5.Location = new System.Drawing.Point(4, 42);
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage5.Size = new System.Drawing.Size(511, 202);
		this.tabPage5.TabIndex = 4;
		this.tabPage5.Text = "Códigos";
		this.tabPage5.UseVisualStyleBackColor = true;
		this.dgvCodigos.AllowUserToAddRows = false;
		this.dgvCodigos.AllowUserToDeleteRows = false;
		this.dgvCodigos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCodigos.Columns.AddRange(this.code, this.barcode, this.descripcion);
		this.dgvCodigos.Location = new System.Drawing.Point(19, 16);
		this.dgvCodigos.Name = "dgvCodigos";
		this.dgvCodigos.ReadOnly = true;
		this.dgvCodigos.RowHeadersVisible = false;
		this.dgvCodigos.Size = new System.Drawing.Size(309, 166);
		this.dgvCodigos.TabIndex = 0;
		this.code.HeaderText = "Codigo";
		this.code.Name = "code";
		this.code.ReadOnly = true;
		this.barcode.HeaderText = "Codigo Barras";
		this.barcode.Name = "barcode";
		this.barcode.ReadOnly = true;
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.tabPage4.Controls.Add(this.label38);
		this.tabPage4.Controls.Add(this.cbControlStock);
		this.tabPage4.Controls.Add(this.txtStockFuturo);
		this.tabPage4.Controls.Add(this.txtPorRecibir);
		this.tabPage4.Controls.Add(this.txtStockDisponible);
		this.tabPage4.Controls.Add(this.txtPorEntregar);
		this.tabPage4.Controls.Add(this.txtStockActual);
		this.tabPage4.Controls.Add(this.txtStockRep);
		this.tabPage4.Controls.Add(this.txtStockMax);
		this.tabPage4.Controls.Add(this.txtStockMin);
		this.tabPage4.Controls.Add(this.label26);
		this.tabPage4.Controls.Add(this.label25);
		this.tabPage4.Controls.Add(this.label24);
		this.tabPage4.Controls.Add(this.label23);
		this.tabPage4.Controls.Add(this.label22);
		this.tabPage4.Controls.Add(this.cbStockRep);
		this.tabPage4.Controls.Add(this.cbStockMax);
		this.tabPage4.Controls.Add(this.cbStockMin);
		this.tabPage4.Location = new System.Drawing.Point(4, 42);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage4.Size = new System.Drawing.Size(511, 202);
		this.tabPage4.TabIndex = 3;
		this.tabPage4.Text = "Stock";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.label38.AutoSize = true;
		this.label38.Location = new System.Drawing.Point(17, 117);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(77, 13);
		this.label38.TabIndex = 22;
		this.label38.Text = "Control Stock :";
		this.cbControlStock.FormattingEnabled = true;
		this.cbControlStock.Items.AddRange(new object[4] { "Libre", "Por Lote", "Por Serie", "Servicio" });
		this.cbControlStock.Location = new System.Drawing.Point(100, 114);
		this.cbControlStock.Name = "cbControlStock";
		this.cbControlStock.Size = new System.Drawing.Size(121, 21);
		this.cbControlStock.TabIndex = 7;
		this.txtStockFuturo.Location = new System.Drawing.Point(351, 119);
		this.txtStockFuturo.Name = "txtStockFuturo";
		this.txtStockFuturo.ReadOnly = true;
		this.txtStockFuturo.Size = new System.Drawing.Size(100, 20);
		this.txtStockFuturo.TabIndex = 19;
		this.txtPorRecibir.Location = new System.Drawing.Point(351, 93);
		this.txtPorRecibir.Name = "txtPorRecibir";
		this.txtPorRecibir.ReadOnly = true;
		this.txtPorRecibir.Size = new System.Drawing.Size(100, 20);
		this.txtPorRecibir.TabIndex = 17;
		this.txtStockDisponible.Location = new System.Drawing.Point(351, 67);
		this.txtStockDisponible.Name = "txtStockDisponible";
		this.txtStockDisponible.ReadOnly = true;
		this.txtStockDisponible.Size = new System.Drawing.Size(100, 20);
		this.txtStockDisponible.TabIndex = 15;
		this.txtPorEntregar.Location = new System.Drawing.Point(351, 41);
		this.txtPorEntregar.Name = "txtPorEntregar";
		this.txtPorEntregar.ReadOnly = true;
		this.txtPorEntregar.Size = new System.Drawing.Size(100, 20);
		this.txtPorEntregar.TabIndex = 13;
		this.txtStockActual.Location = new System.Drawing.Point(351, 15);
		this.txtStockActual.Name = "txtStockActual";
		this.txtStockActual.ReadOnly = true;
		this.txtStockActual.Size = new System.Drawing.Size(100, 20);
		this.txtStockActual.TabIndex = 11;
		this.txtStockRep.Enabled = false;
		this.txtStockRep.Location = new System.Drawing.Point(128, 67);
		this.txtStockRep.Name = "txtStockRep";
		this.txtStockRep.Size = new System.Drawing.Size(63, 20);
		this.txtStockRep.TabIndex = 6;
		this.txtStockRep.Tag = "1";
		this.txtStockMax.Enabled = false;
		this.txtStockMax.Location = new System.Drawing.Point(128, 41);
		this.txtStockMax.Name = "txtStockMax";
		this.txtStockMax.Size = new System.Drawing.Size(63, 20);
		this.txtStockMax.TabIndex = 4;
		this.txtStockMax.Tag = "1";
		this.txtStockMin.Enabled = false;
		this.txtStockMin.Location = new System.Drawing.Point(128, 15);
		this.txtStockMin.Name = "txtStockMin";
		this.txtStockMin.Size = new System.Drawing.Size(63, 20);
		this.txtStockMin.TabIndex = 2;
		this.txtStockMin.Tag = "1";
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(271, 122);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(71, 13);
		this.label26.TabIndex = 18;
		this.label26.Text = "Stock futuro :";
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(267, 96);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(75, 13);
		this.label25.TabIndex = 16;
		this.label25.Text = "( + ) P. recibir :";
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(251, 70);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(91, 13);
		this.label24.TabIndex = 14;
		this.label24.Text = "Stock disponible :";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(259, 43);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(83, 13);
		this.label23.TabIndex = 12;
		this.label23.Text = "( - ) P. entregar :";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(269, 18);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(73, 13);
		this.label22.TabIndex = 10;
		this.label22.Text = "Stock actual :";
		this.cbStockRep.AutoSize = true;
		this.cbStockRep.Location = new System.Drawing.Point(20, 69);
		this.cbStockRep.Name = "cbStockRep";
		this.cbStockRep.Size = new System.Drawing.Size(105, 17);
		this.cbStockRep.TabIndex = 5;
		this.cbStockRep.Text = "Stock reposición";
		this.cbStockRep.UseVisualStyleBackColor = true;
		this.cbStockRep.CheckedChanged += new System.EventHandler(cbStockRep_CheckedChanged);
		this.cbStockMax.AutoSize = true;
		this.cbStockMax.Location = new System.Drawing.Point(20, 43);
		this.cbStockMax.Name = "cbStockMax";
		this.cbStockMax.Size = new System.Drawing.Size(92, 17);
		this.cbStockMax.TabIndex = 3;
		this.cbStockMax.Text = "Stock máximo";
		this.cbStockMax.UseVisualStyleBackColor = true;
		this.cbStockMax.CheckedChanged += new System.EventHandler(cbStockMax_CheckedChanged);
		this.cbStockMin.AutoSize = true;
		this.cbStockMin.Location = new System.Drawing.Point(20, 17);
		this.cbStockMin.Name = "cbStockMin";
		this.cbStockMin.Size = new System.Drawing.Size(89, 17);
		this.cbStockMin.TabIndex = 1;
		this.cbStockMin.Text = "Stock minimo";
		this.cbStockMin.UseVisualStyleBackColor = true;
		this.cbStockMin.CheckedChanged += new System.EventHandler(cbStockMin_CheckedChanged);
		this.tabPage3.Controls.Add(this.cmbUnidadBase);
		this.tabPage3.Controls.Add(this.label13);
		this.tabPage3.Controls.Add(this.groupBox2);
		this.tabPage3.Controls.Add(this.groupBox3);
		this.tabPage3.Location = new System.Drawing.Point(4, 42);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(511, 202);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "Unidades";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.cmbUnidadBase.FormattingEnabled = true;
		this.cmbUnidadBase.Location = new System.Drawing.Point(222, 18);
		this.cmbUnidadBase.Name = "cmbUnidadBase";
		this.cmbUnidadBase.Size = new System.Drawing.Size(121, 21);
		this.cmbUnidadBase.TabIndex = 1;
		this.cmbUnidadBase.Tag = "1";
		this.cmbUnidadBase.SelectionChangeCommitted += new System.EventHandler(cmbUnidadBase_SelectionChangeCommitted);
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(135, 21);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(81, 13);
		this.label13.TabIndex = 0;
		this.label13.Text = "Unidad Base * :";
		this.groupBox2.Controls.Add(this.btnCancel);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.btnOkU1);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.txtFactor1);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.cbUni1);
		this.groupBox2.Location = new System.Drawing.Point(6, 66);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(499, 75);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Equivalencia";
		this.groupBox2.Visible = false;
		this.btnCancel.ImageIndex = 0;
		this.btnCancel.ImageList = this.imageList1;
		this.btnCancel.Location = new System.Drawing.Point(469, 26);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(24, 24);
		this.btnCancel.TabIndex = 27;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(12, 32);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(42, 13);
		this.label16.TabIndex = 26;
		this.label16.Text = "Un(a) 1";
		this.btnOkU1.ImageIndex = 1;
		this.btnOkU1.ImageList = this.imageList1;
		this.btnOkU1.Location = new System.Drawing.Point(439, 26);
		this.btnOkU1.Name = "btnOkU1";
		this.btnOkU1.Size = new System.Drawing.Size(24, 24);
		this.btnOkU1.TabIndex = 25;
		this.btnOkU1.UseVisualStyleBackColor = true;
		this.btnOkU1.Click += new System.EventHandler(btnOkU1_Click);
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
		this.groupBox3.Controls.Add(this.btnEliminarUnidad);
		this.groupBox3.Controls.Add(this.btnAñadirUnidad);
		this.groupBox3.Controls.Add(this.dgvEquivalentes);
		this.groupBox3.Location = new System.Drawing.Point(6, 48);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(499, 148);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Unidades equivalentes";
		this.btnEliminarUnidad.ImageIndex = 5;
		this.btnEliminarUnidad.ImageList = this.imageList1;
		this.btnEliminarUnidad.Location = new System.Drawing.Point(469, 71);
		this.btnEliminarUnidad.Name = "btnEliminarUnidad";
		this.btnEliminarUnidad.Size = new System.Drawing.Size(24, 24);
		this.btnEliminarUnidad.TabIndex = 10;
		this.btnEliminarUnidad.UseVisualStyleBackColor = true;
		this.btnEliminarUnidad.Click += new System.EventHandler(btnEliminarUnidad_Click);
		this.btnAñadirUnidad.ImageIndex = 4;
		this.btnAñadirUnidad.ImageList = this.imageList1;
		this.btnAñadirUnidad.Location = new System.Drawing.Point(469, 44);
		this.btnAñadirUnidad.Name = "btnAñadirUnidad";
		this.btnAñadirUnidad.Size = new System.Drawing.Size(24, 24);
		this.btnAñadirUnidad.TabIndex = 9;
		this.btnAñadirUnidad.UseVisualStyleBackColor = true;
		this.btnAñadirUnidad.Click += new System.EventHandler(btnAñadirUnidad_Click);
		this.dgvEquivalentes.AllowUserToAddRows = false;
		this.dgvEquivalentes.AllowUserToDeleteRows = false;
		this.dgvEquivalentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvEquivalentes.Columns.AddRange(this.codunidadequivalente, this.unidad, this.factor);
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvEquivalentes.DefaultCellStyle = dataGridViewCellStyle26;
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
		this.tabPage2.Controls.Add(this.dgvProxProducto);
		this.tabPage2.Location = new System.Drawing.Point(4, 42);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(511, 202);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Proveedores";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dgvProxProducto.AllowUserToAddRows = false;
		this.dgvProxProducto.AllowUserToDeleteRows = false;
		this.dgvProxProducto.AllowUserToResizeColumns = false;
		this.dgvProxProducto.AllowUserToResizeRows = false;
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvProxProducto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
		this.dgvProxProducto.Columns.AddRange(this.codigo, this.nombre, this.precio, this.dscto1, this.dscto2, this.dscto3, this.pneto);
		this.dgvProxProducto.Location = new System.Drawing.Point(6, 6);
		this.dgvProxProducto.Name = "dgvProxProducto";
		this.dgvProxProducto.ReadOnly = true;
		this.dgvProxProducto.RowHeadersVisible = false;
		this.dgvProxProducto.Size = new System.Drawing.Size(499, 192);
		this.dgvProxProducto.TabIndex = 0;
		this.codigo.DataPropertyName = "codProveedor";
		this.codigo.HeaderText = "Cod. Prov.";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.codigo.Width = 60;
		this.nombre.DataPropertyName = "razonsocial";
		this.nombre.HeaderText = "Proveedor";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 200;
		this.precio.DataPropertyName = "precioofrecido";
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle28.Format = "N2";
		this.precio.DefaultCellStyle = dataGridViewCellStyle28;
		this.precio.HeaderText = "Precio";
		this.precio.Name = "precio";
		this.precio.ReadOnly = true;
		this.precio.Width = 65;
		this.dscto1.DataPropertyName = "dscto1";
		dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle29.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle29;
		this.dscto1.HeaderText = "Dscto. 1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Width = 60;
		this.dscto2.DataPropertyName = "dscto2";
		dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle30.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle30;
		this.dscto2.HeaderText = "Dscto. 2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Width = 60;
		this.dscto3.DataPropertyName = "dscto3";
		dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle31.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle31;
		this.dscto3.HeaderText = "Dscto. 3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Width = 60;
		this.pneto.DataPropertyName = "precio";
		dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle32.Format = "N2";
		this.pneto.DefaultCellStyle = dataGridViewCellStyle32;
		this.pneto.HeaderText = "P. Neto";
		this.pneto.Name = "pneto";
		this.pneto.ReadOnly = true;
		this.pneto.Width = 65;
		this.tabPage1.Controls.Add(this.label20);
		this.tabPage1.Controls.Add(this.rdtExonerado);
		this.tabPage1.Controls.Add(this.rdtInafecto);
		this.tabPage1.Controls.Add(this.rdtGravado);
		this.tabPage1.Controls.Add(this.lnkListasPrecios);
		this.tabPage1.Controls.Add(this.label39);
		this.tabPage1.Controls.Add(this.txtFlete);
		this.tabPage1.Controls.Add(this.label42);
		this.tabPage1.Controls.Add(this.txtValorCompra);
		this.tabPage1.Controls.Add(this.label41);
		this.tabPage1.Controls.Add(this.txtGanancia);
		this.tabPage1.Controls.Add(this.txtMontoDscto);
		this.tabPage1.Controls.Add(this.txtPorcDscto);
		this.tabPage1.Controls.Add(this.label40);
		this.tabPage1.Controls.Add(this.txtPrecioCompra);
		this.tabPage1.Controls.Add(this.label35);
		this.tabPage1.Controls.Add(this.cbPVariable);
		this.tabPage1.Controls.Add(this.label34);
		this.tabPage1.Controls.Add(this.label33);
		this.tabPage1.Controls.Add(this.txtPfinal);
		this.tabPage1.Controls.Add(this.label32);
		this.tabPage1.Controls.Add(this.txtDscto);
		this.tabPage1.Controls.Add(this.label31);
		this.tabPage1.Controls.Add(this.txtPrecioOferta);
		this.tabPage1.Controls.Add(this.cbOferta);
		this.tabPage1.Controls.Add(this.cbDetraccion);
		this.tabPage1.Controls.Add(this.txtValorVenta);
		this.tabPage1.Controls.Add(this.txtRecargo);
		this.tabPage1.Controls.Add(this.label12);
		this.tabPage1.Controls.Add(this.label11);
		this.tabPage1.Location = new System.Drawing.Point(4, 42);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(511, 202);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Precios";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.lnkListasPrecios.AutoSize = true;
		this.lnkListasPrecios.Location = new System.Drawing.Point(269, 177);
		this.lnkListasPrecios.Name = "lnkListasPrecios";
		this.lnkListasPrecios.Size = new System.Drawing.Size(207, 13);
		this.lnkListasPrecios.TabIndex = 36;
		this.lnkListasPrecios.TabStop = true;
		this.lnkListasPrecios.Text = "Configurar Unidaes Equivalentes y Precios";
		this.lnkListasPrecios.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkListasPrecios_LinkClicked);
		this.label39.AutoSize = true;
		this.label39.Location = new System.Drawing.Point(41, 18);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(83, 13);
		this.label39.TabIndex = 35;
		this.label39.Text = "Valor Compra * :";
		this.txtFlete.Location = new System.Drawing.Point(412, 18);
		this.txtFlete.Name = "txtFlete";
		this.txtFlete.Size = new System.Drawing.Size(64, 20);
		this.txtFlete.TabIndex = 11;
		this.txtFlete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtFlete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFlete_KeyPress);
		this.label42.AutoSize = true;
		this.label42.Location = new System.Drawing.Point(370, 21);
		this.label42.Name = "label42";
		this.label42.Size = new System.Drawing.Size(36, 13);
		this.label42.TabIndex = 33;
		this.label42.Text = "Flete :";
		this.txtValorCompra.Location = new System.Drawing.Point(134, 15);
		this.txtValorCompra.Name = "txtValorCompra";
		this.txtValorCompra.Size = new System.Drawing.Size(64, 20);
		this.txtValorCompra.TabIndex = 10;
		this.txtValorCompra.Tag = "1";
		this.txtValorCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtValorCompra_KeyPress);
		this.label41.AutoSize = true;
		this.label41.Location = new System.Drawing.Point(274, 129);
		this.label41.Name = "label41";
		this.label41.Size = new System.Drawing.Size(98, 13);
		this.label41.TabIndex = 26;
		this.label41.Text = "Monto Descuento :";
		this.label41.Visible = false;
		this.txtGanancia.Location = new System.Drawing.Point(134, 171);
		this.txtGanancia.Name = "txtGanancia";
		this.txtGanancia.Size = new System.Drawing.Size(72, 20);
		this.txtGanancia.TabIndex = 31;
		this.txtGanancia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtGanancia.Visible = false;
		this.txtMontoDscto.Enabled = false;
		this.txtMontoDscto.Location = new System.Drawing.Point(277, 145);
		this.txtMontoDscto.Name = "txtMontoDscto";
		this.txtMontoDscto.Size = new System.Drawing.Size(72, 20);
		this.txtMontoDscto.TabIndex = 19;
		this.txtMontoDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtMontoDscto.Visible = false;
		this.txtMontoDscto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMontoDscto_KeyPress);
		this.txtPorcDscto.Enabled = false;
		this.txtPorcDscto.Location = new System.Drawing.Point(207, 145);
		this.txtPorcDscto.Name = "txtPorcDscto";
		this.txtPorcDscto.Size = new System.Drawing.Size(64, 20);
		this.txtPorcDscto.TabIndex = 18;
		this.txtPorcDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPorcDscto.TextChanged += new System.EventHandler(txtPorcDscto_TextChanged);
		this.txtPorcDscto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPorcDscto_KeyPress);
		this.label40.AutoSize = true;
		this.label40.Location = new System.Drawing.Point(204, 129);
		this.label40.Name = "label40";
		this.label40.Size = new System.Drawing.Size(52, 13);
		this.label40.TabIndex = 21;
		this.label40.Text = "% Dscto :";
		this.label40.Visible = false;
		this.txtPrecioCompra.Location = new System.Drawing.Point(134, 41);
		this.txtPrecioCompra.Name = "txtPrecioCompra";
		this.txtPrecioCompra.Size = new System.Drawing.Size(64, 20);
		this.txtPrecioCompra.TabIndex = 12;
		this.txtPrecioCompra.Tag = "1";
		this.txtPrecioCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioCompra.TextChanged += new System.EventHandler(txtPrecioCompra_TextChanged);
		this.txtPrecioCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecioCompra_KeyPress);
		this.label35.AutoSize = true;
		this.label35.Location = new System.Drawing.Point(35, 44);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(89, 13);
		this.label35.TabIndex = 16;
		this.label35.Text = "Precio Compra * :";
		this.cbPVariable.AutoSize = true;
		this.cbPVariable.Location = new System.Drawing.Point(373, 47);
		this.cbPVariable.Name = "cbPVariable";
		this.cbPVariable.Size = new System.Drawing.Size(97, 17);
		this.cbPVariable.TabIndex = 24;
		this.cbPVariable.Text = "Precio Variable";
		this.cbPVariable.UseVisualStyleBackColor = true;
		this.label34.AutoSize = true;
		this.label34.Location = new System.Drawing.Point(450, 148);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(15, 13);
		this.label34.TabIndex = 14;
		this.label34.Text = "%";
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(204, 70);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(15, 13);
		this.label33.TabIndex = 13;
		this.label33.Text = "%";
		this.label33.Visible = false;
		this.txtPfinal.Location = new System.Drawing.Point(134, 119);
		this.txtPfinal.Name = "txtPfinal";
		this.txtPfinal.Size = new System.Drawing.Size(64, 20);
		this.txtPfinal.TabIndex = 15;
		this.txtPfinal.Tag = "1";
		this.txtPfinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPfinal.Visible = false;
		this.txtPfinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPfinal_KeyPress);
		this.label32.AutoSize = true;
		this.label32.Location = new System.Drawing.Point(22, 122);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(102, 13);
		this.label32.TabIndex = 11;
		this.label32.Text = "Precio venta final * :";
		this.label32.Visible = false;
		this.txtDscto.Location = new System.Drawing.Point(379, 145);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.Size = new System.Drawing.Size(64, 20);
		this.txtDscto.TabIndex = 20;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDscto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDscto_KeyPress);
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(378, 129);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(102, 13);
		this.label31.TabIndex = 9;
		this.label31.Text = "Máximo porc. dscto.";
		this.txtPrecioOferta.Enabled = false;
		this.txtPrecioOferta.Location = new System.Drawing.Point(134, 145);
		this.txtPrecioOferta.Name = "txtPrecioOferta";
		this.txtPrecioOferta.Size = new System.Drawing.Size(64, 20);
		this.txtPrecioOferta.TabIndex = 17;
		this.txtPrecioOferta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioOferta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecioOferta_KeyPress);
		this.txtPrecioOferta.Leave += new System.EventHandler(txtPrecioOferta_Leave);
		this.cbOferta.AutoSize = true;
		this.cbOferta.Location = new System.Drawing.Point(30, 147);
		this.cbOferta.Name = "cbOferta";
		this.cbOferta.Size = new System.Drawing.Size(94, 17);
		this.cbOferta.TabIndex = 16;
		this.cbOferta.Text = "Precio Oferta :";
		this.cbOferta.UseVisualStyleBackColor = true;
		this.cbOferta.CheckedChanged += new System.EventHandler(cbOferta_CheckedChanged);
		this.cbDetraccion.AutoSize = true;
		this.cbDetraccion.Location = new System.Drawing.Point(373, 70);
		this.cbDetraccion.Name = "cbDetraccion";
		this.cbDetraccion.Size = new System.Drawing.Size(121, 17);
		this.cbDetraccion.TabIndex = 22;
		this.cbDetraccion.Text = "Afecto a Detraccion";
		this.cbDetraccion.UseVisualStyleBackColor = true;
		this.cbDetraccion.Visible = false;
		this.txtValorVenta.Location = new System.Drawing.Point(134, 93);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.Size = new System.Drawing.Size(64, 20);
		this.txtValorVenta.TabIndex = 14;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Visible = false;
		this.txtValorVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPsinIgv_KeyPress);
		this.txtRecargo.Location = new System.Drawing.Point(134, 67);
		this.txtRecargo.Name = "txtRecargo";
		this.txtRecargo.Size = new System.Drawing.Size(64, 20);
		this.txtRecargo.TabIndex = 13;
		this.txtRecargo.Tag = "1";
		this.txtRecargo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtRecargo.Visible = false;
		this.txtRecargo.TextChanged += new System.EventHandler(txtRecargo_TextChanged);
		this.txtRecargo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRecargo_KeyPress);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(60, 96);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(64, 13);
		this.label12.TabIndex = 4;
		this.label12.Text = "Valor venta:";
		this.label12.Visible = false;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(63, 70);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(61, 13);
		this.label11.TabIndex = 2;
		this.label11.Text = "Recargo * :";
		this.label11.Visible = false;
		this.tabControl1.Controls.Add(this.tabPage11);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.tabPage4);
		this.tabControl1.Controls.Add(this.tabPage5);
		this.tabControl1.Controls.Add(this.tabPage6);
		this.tabControl1.Controls.Add(this.tabPage7);
		this.tabControl1.Controls.Add(this.tabPage8);
		this.tabControl1.Controls.Add(this.tabPage9);
		this.tabControl1.ImageList = this.imageList1;
		this.tabControl1.Location = new System.Drawing.Point(12, 85);
		this.tabControl1.Multiline = true;
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(519, 248);
		this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
		this.tabControl1.TabIndex = 1;
		this.tabControl1.SelectedIndexChanged += new System.EventHandler(tabControl1_SelectedIndexChanged);
		this.tabPage11.Controls.Add(this.txtPrecioCata);
		this.tabPage11.Controls.Add(this.label19);
		this.tabPage11.Controls.Add(this.label17);
		this.tabPage11.Controls.Add(this.txtComision);
		this.tabPage11.Controls.Add(this.label18);
		this.tabPage11.Controls.Add(this.cbTipoArticulo);
		this.tabPage11.Controls.Add(this.label36);
		this.tabPage11.Controls.Add(this.cbMarca);
		this.tabPage11.Controls.Add(this.label10);
		this.tabPage11.Controls.Add(this.txtUsuario);
		this.tabPage11.Controls.Add(this.txtModificacion);
		this.tabPage11.Controls.Add(this.txtFechaReg);
		this.tabPage11.Controls.Add(this.label9);
		this.tabPage11.Controls.Add(this.label8);
		this.tabPage11.Controls.Add(this.label7);
		this.tabPage11.Controls.Add(this.cbGrupo);
		this.tabPage11.Controls.Add(this.label6);
		this.tabPage11.Controls.Add(this.cbLinea);
		this.tabPage11.Controls.Add(this.label5);
		this.tabPage11.Controls.Add(this.cbFamilia);
		this.tabPage11.Controls.Add(this.label4);
		this.tabPage11.Location = new System.Drawing.Point(4, 42);
		this.tabPage11.Name = "tabPage11";
		this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage11.Size = new System.Drawing.Size(511, 202);
		this.tabPage11.TabIndex = 10;
		this.tabPage11.Text = "Datos";
		this.tabPage11.UseVisualStyleBackColor = true;
		this.txtPrecioCata.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtPrecioCata.Enabled = false;
		this.txtPrecioCata.Location = new System.Drawing.Point(382, 135);
		this.txtPrecioCata.Name = "txtPrecioCata";
		this.txtPrecioCata.Size = new System.Drawing.Size(61, 20);
		this.txtPrecioCata.TabIndex = 56;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(288, 138);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(0, 13);
		this.label19.TabIndex = 55;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(447, 112);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(0, 13);
		this.label17.TabIndex = 54;
		this.txtComision.Enabled = false;
		this.txtComision.Location = new System.Drawing.Point(382, 109);
		this.txtComision.Name = "txtComision";
		this.txtComision.Size = new System.Drawing.Size(62, 20);
		this.txtComision.TabIndex = 53;
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(321, 112);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(0, 13);
		this.label18.TabIndex = 52;
		this.cbTipoArticulo.Enabled = false;
		this.cbTipoArticulo.FormattingEnabled = true;
		this.cbTipoArticulo.Location = new System.Drawing.Point(97, 28);
		this.cbTipoArticulo.Name = "cbTipoArticulo";
		this.cbTipoArticulo.Size = new System.Drawing.Size(121, 21);
		this.cbTipoArticulo.TabIndex = 1;
		this.cbTipoArticulo.Tag = "1";
		this.label36.AutoSize = true;
		this.label36.Location = new System.Drawing.Point(10, 31);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(0, 13);
		this.label36.TabIndex = 14;
		this.cbMarca.Enabled = false;
		this.cbMarca.FormattingEnabled = true;
		this.cbMarca.Location = new System.Drawing.Point(97, 137);
		this.cbMarca.Name = "cbMarca";
		this.cbMarca.Size = new System.Drawing.Size(121, 21);
		this.cbMarca.TabIndex = 5;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(51, 140);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(0, 13);
		this.label10.TabIndex = 12;
		this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUsuario.Enabled = false;
		this.txtUsuario.Location = new System.Drawing.Point(382, 83);
		this.txtUsuario.Name = "txtUsuario";
		this.txtUsuario.Size = new System.Drawing.Size(100, 20);
		this.txtUsuario.TabIndex = 8;
		this.txtModificacion.Enabled = false;
		this.txtModificacion.Location = new System.Drawing.Point(382, 56);
		this.txtModificacion.Name = "txtModificacion";
		this.txtModificacion.Size = new System.Drawing.Size(100, 20);
		this.txtModificacion.TabIndex = 7;
		this.txtFechaReg.Enabled = false;
		this.txtFechaReg.Location = new System.Drawing.Point(382, 29);
		this.txtFechaReg.Name = "txtFechaReg";
		this.txtFechaReg.Size = new System.Drawing.Size(100, 20);
		this.txtFechaReg.TabIndex = 6;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(330, 86);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(0, 13);
		this.label9.TabIndex = 10;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(274, 59);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(0, 13);
		this.label8.TabIndex = 8;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(294, 32);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(0, 13);
		this.label7.TabIndex = 6;
		this.cbGrupo.Enabled = false;
		this.cbGrupo.FormattingEnabled = true;
		this.cbGrupo.Location = new System.Drawing.Point(97, 110);
		this.cbGrupo.Name = "cbGrupo";
		this.cbGrupo.Size = new System.Drawing.Size(121, 21);
		this.cbGrupo.TabIndex = 4;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(52, 113);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(0, 13);
		this.label6.TabIndex = 4;
		this.cbLinea.Enabled = false;
		this.cbLinea.FormattingEnabled = true;
		this.cbLinea.Location = new System.Drawing.Point(97, 83);
		this.cbLinea.Name = "cbLinea";
		this.cbLinea.Size = new System.Drawing.Size(121, 21);
		this.cbLinea.TabIndex = 3;
		this.cbLinea.SelectionChangeCommitted += new System.EventHandler(cbLinea_SelectionChangeCommitted);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(55, 86);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(0, 13);
		this.label5.TabIndex = 2;
		this.cbFamilia.Enabled = false;
		this.cbFamilia.FormattingEnabled = true;
		this.cbFamilia.Location = new System.Drawing.Point(97, 56);
		this.cbFamilia.Name = "cbFamilia";
		this.cbFamilia.Size = new System.Drawing.Size(121, 21);
		this.cbFamilia.TabIndex = 2;
		this.cbFamilia.Tag = "1";
		this.cbFamilia.SelectionChangeCommitted += new System.EventHandler(cbFamilia_SelectionChangeCommitted);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(39, 59);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(0, 13);
		this.label4.TabIndex = 0;
		this.tabPage8.Controls.Add(this.label30);
		this.tabPage8.Controls.Add(this.clbTipoOperacion);
		this.tabPage8.Location = new System.Drawing.Point(4, 42);
		this.tabPage8.Name = "tabPage8";
		this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage8.Size = new System.Drawing.Size(511, 202);
		this.tabPage8.TabIndex = 7;
		this.tabPage8.Text = "Operaciones";
		this.tabPage8.UseVisualStyleBackColor = true;
		this.label30.Location = new System.Drawing.Point(268, 14);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(237, 55);
		this.label30.TabIndex = 1;
		this.clbTipoOperacion.FormattingEnabled = true;
		this.clbTipoOperacion.Location = new System.Drawing.Point(17, 14);
		this.clbTipoOperacion.Name = "clbTipoOperacion";
		this.clbTipoOperacion.Size = new System.Drawing.Size(245, 169);
		this.clbTipoOperacion.TabIndex = 0;
		this.btnCambioProv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCambioProv.ImageKey = "Remove.png";
		this.btnCambioProv.ImageList = this.imageList1;
		this.btnCambioProv.Location = new System.Drawing.Point(248, 339);
		this.btnCambioProv.Name = "btnCambioProv";
		this.btnCambioProv.Size = new System.Drawing.Size(121, 23);
		this.btnCambioProv.TabIndex = 28;
		this.btnCambioProv.Text = "Cambiar Proveedor";
		this.btnCambioProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCambioProv.UseVisualStyleBackColor = true;
		this.btnCambioProv.Visible = false;
		this.btnCambioProv.Click += new System.EventHandler(btnCambioProv_Click);
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(456, 339);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 26;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(375, 339);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 25;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(222, 18);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(131, 13);
		this.label20.TabIndex = 127;
		this.label20.Text = "Producto se define como :";
		this.rdtExonerado.AutoSize = true;
		this.rdtExonerado.Location = new System.Drawing.Point(244, 57);
		this.rdtExonerado.Name = "rdtExonerado";
		this.rdtExonerado.Size = new System.Drawing.Size(93, 17);
		this.rdtExonerado.TabIndex = 126;
		this.rdtExonerado.Text = "EXONERADO";
		this.rdtExonerado.UseVisualStyleBackColor = true;
		this.rdtInafecto.AutoSize = true;
		this.rdtInafecto.Location = new System.Drawing.Point(244, 78);
		this.rdtInafecto.Name = "rdtInafecto";
		this.rdtInafecto.Size = new System.Drawing.Size(78, 17);
		this.rdtInafecto.TabIndex = 125;
		this.rdtInafecto.Text = "INAFECTO";
		this.rdtInafecto.UseVisualStyleBackColor = true;
		this.rdtGravado.AutoSize = true;
		this.rdtGravado.Checked = true;
		this.rdtGravado.Location = new System.Drawing.Point(244, 37);
		this.rdtGravado.Name = "rdtGravado";
		this.rdtGravado.Size = new System.Drawing.Size(78, 17);
		this.rdtGravado.TabIndex = 124;
		this.rdtGravado.TabStop = true;
		this.rdtGravado.Text = "GRAVADO";
		this.rdtGravado.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(542, 370);
		base.Controls.Add(this.btnCambioProv);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionProducto";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestión Producto";
		base.Load += new System.EventHandler(frmGestionProducto_Load);
		base.Shown += new System.EventHandler(frmGestionProducto_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.tabPage9.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).EndInit();
		this.tabPage7.ResumeLayout(false);
		this.tabPage7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCaracteristicas).EndInit();
		this.tabPage6.ResumeLayout(false);
		this.tabPage6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNotas).EndInit();
		this.tabPage5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvCodigos).EndInit();
		this.tabPage4.ResumeLayout(false);
		this.tabPage4.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvEquivalentes).EndInit();
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvProxProducto).EndInit();
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage11.ResumeLayout(false);
		this.tabPage11.PerformLayout();
		this.tabPage8.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
