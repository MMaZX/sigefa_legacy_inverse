using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;

namespace SIGEFA.Formularios;

public class frmCajaChicaRendicion : Office2007Form
{
	private clsAdmSucursal AdmSuc = new clsAdmSucursal();

	public List<int> seleccion = new List<int>();

	public static BindingSource data = new BindingSource();

	public static BindingSource data1 = new BindingSource();

	private string filtro = string.Empty;

	private string filtro1 = string.Empty;

	private int FilasChequeadas = 0;

	private decimal Total = default(decimal);

	private int NumFila = 0;

	private int Tipo = 0;

	private int CodRendicion = 0;

	public int tipocaja = 0;

	private IContainer components = null;

	private ImageList imageList2;

	private ImageList imageList1;

	private Panel panel1;

	private Panel panel2;

	private ComboBox cboSucursal;

	private Button btnSalir;

	private Label label1;

	private RibbonBar ribbonBar1;

	private ButtonItem biAprobar;

	private ButtonItem biDesaprobar;

	private DataGridView dgvDetalleRendiciones;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem tsmiRendicion;

	private Panel panel3;

	private Label lblTotal;

	private Label label4;

	private DataGridView dgvRendiciones;

	private Panel panel6;

	private Label label5;

	private Panel panel4;

	private TextBox txtComentario;

	private Label label3;

	private Panel panel7;

	private Label label8;

	private Label label9;

	private Label label6;

	private Label label7;

	private ButtonItem buttonItem1;

	private RibbonBar ribbonBar2;

	private ButtonItem biAceptar;

	private ButtonItem biRechazar;

	private Button btnCancelar;

	private DataGridViewCheckBoxColumn Item;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn codPersonalizado;

	private DataGridViewTextBoxColumn concepto;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn numDocumento;

	private DataGridViewTextBoxColumn numGuia;

	private DataGridViewTextBoxColumn numRecLiquidacion;

	private DataGridViewTextBoxColumn codTipoPagoCaja;

	private DataGridViewTextBoxColumn tipopagocaja;

	private DataGridViewTextBoxColumn cargadescarga;

	private DataGridViewTextBoxColumn tipoMovimiento;

	private DataGridViewTextBoxColumn toneladas;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn comentario;

	private DataGridViewTextBoxColumn status;

	private DataGridViewTextBoxColumn estatus_;

	private DataGridViewTextBoxColumn codigoRendicion;

	private DataGridViewTextBoxColumn numDoc;

	private DataGridViewTextBoxColumn montoRendido;

	private DataGridViewTextBoxColumn montoaprobado;

	private DataGridViewTextBoxColumn montodesaprobado;

	private DataGridViewTextBoxColumn fechaRendicion;

	private DataGridViewTextBoxColumn observacion;

	private DataGridViewTextBoxColumn estatus;

	private DataGridViewTextBoxColumn totalRendiciones;

	private DataGridViewTextBoxColumn aprobadas;

	private DataGridViewTextBoxColumn desaprobadas;

	public frmCajaChicaRendicion()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmCajaChicaRendicion_Load(object sender, EventArgs e)
	{
		CargaSucursales();
	}

	private void CargaSucursales()
	{
		cboSucursal.DataSource = AdmSuc.CargaSucursales(frmLogin.iCodEmpresa);
		cboSucursal.DisplayMember = "nombre";
		cboSucursal.ValueMember = "codSucursal";
		cboSucursal.SelectedIndex = 0;
	}

	private void ListaRendicionesXSucursal()
	{
	}

	private void ListaDetalleRendiciones()
	{
	}

	private void cboSucursal_SelectedValueChanged(object sender, EventArgs e)
	{
		try
		{
			ListaRendicionesXSucursal();
		}
		catch (Exception)
		{
		}
	}

	private void dgvRendiciones_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		if (dgvDetalleRendiciones.IsCurrentCellDirty)
		{
			dgvDetalleRendiciones.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
	}

	private void dgvRendiciones_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1 && dgvDetalleRendiciones.Columns[e.ColumnIndex].Name == Item.Name)
		{
			DataGridViewRow row = dgvDetalleRendiciones.Rows[e.RowIndex];
			DataGridViewCheckBoxCell cellSelecion = row.Cells[Item.Name] as DataGridViewCheckBoxCell;
			if (Convert.ToBoolean(cellSelecion.Value))
			{
				FilasChequeadas++;
				seleccion.Add(Convert.ToInt32(row.Cells[codigo.Name].Value));
			}
			else
			{
				FilasChequeadas--;
				seleccion.Remove(Convert.ToInt32(row.Cells[codigo.Name].Value));
			}
			if (FilasChequeadas >= 1)
			{
				biAprobar.Enabled = true;
				biDesaprobar.Enabled = true;
			}
			else
			{
				biAprobar.Enabled = false;
				biDesaprobar.Enabled = false;
			}
		}
	}

	private void CalcularRendicion()
	{
		try
		{
			Total = default(decimal);
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalleRendiciones.Rows)
			{
				Total += Convert.ToDecimal(row.Cells[monto.Name].Value);
			}
			lblTotal.Text = Total.ToString();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void txtComentario_KeyUp(object sender, KeyEventArgs e)
	{
		dgvDetalleRendiciones.Rows[NumFila].Cells[comentario.Name].Value = txtComentario.Text.Trim();
	}

	private void dgvDetalleRendiciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvDetalleRendiciones.Rows.Count >= 1 && e.Row.Selected)
		{
			txtComentario.Text = e.Row.Cells[comentario.Name].Value.ToString();
			NumFila = dgvDetalleRendiciones.SelectedRows[0].Index;
		}
	}

	private void dgvRendiciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvRendiciones.RowCount > 0)
		{
			CodRendicion = Convert.ToInt32(e.Row.Cells[codigoRendicion.Name].Value);
			if (Convert.ToDecimal(e.Row.Cells[totalRendiciones.Name].Value) > 0m)
			{
				if (Convert.ToDecimal(e.Row.Cells[aprobadas.Name].Value) > 0m)
				{
					if (Convert.ToDecimal(e.Row.Cells[totalRendiciones.Name].Value) == Convert.ToDecimal(e.Row.Cells[aprobadas.Name].Value) + Convert.ToDecimal(e.Row.Cells[desaprobadas.Name].Value))
					{
						if (Convert.ToDecimal(e.Row.Cells[aprobadas.Name].Value) > 0m)
						{
							biAceptar.Enabled = true;
							biRechazar.Enabled = true;
						}
						else
						{
							biAceptar.Enabled = false;
							biRechazar.Enabled = true;
						}
					}
					else
					{
						biAceptar.Enabled = false;
						biRechazar.Enabled = false;
					}
				}
				else
				{
					biAceptar.Enabled = false;
					biRechazar.Enabled = true;
				}
			}
			else
			{
				biAceptar.Enabled = false;
				biRechazar.Enabled = false;
			}
		}
		else
		{
			CodRendicion = 0;
			biAceptar.Enabled = false;
			biRechazar.Enabled = false;
		}
		ListaDetalleRendiciones();
		seleccion.Clear();
		FilasChequeadas = 0;
		biAprobar.Enabled = false;
		biDesaprobar.Enabled = false;
	}

	private void GestionaRendicion()
	{
	}

	private void biAprobar_Click(object sender, EventArgs e)
	{
	}

	private void biDesaprobar_Click(object sender, EventArgs e)
	{
		if (dgvDetalleRendiciones.RowCount <= 0)
		{
			return;
		}
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Rechazar Las Rendiciones Seleccionadas", "Gestion de Rendiciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.No)
		{
			return;
		}
		foreach (int Cod in seleccion)
		{
		}
		MessageBox.Show("Tarea Realizada Satisfactoriamente", "Gestion de Rendiciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		ListaRendicionesXSucursal();
		ListaDetalleRendiciones();
	}

	private void GestionaRendicion(int Codigo, string Observacion, int Tipo)
	{
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea enviar a Tesoreria la Rendicion", "Gestion de Rendiciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult != DialogResult.No)
		{
		}
	}

	private void biAceptar_Click(object sender, EventArgs e)
	{
		if (dgvDetalleRendiciones.RowCount > 0)
		{
			int Codigo_ = Convert.ToInt32(dgvRendiciones.SelectedRows[0].Cells[codigoRendicion.Name].Value.ToString());
			string Observacion_ = dgvRendiciones.SelectedRows[0].Cells[observacion.Name].Value.ToString();
			GestionaRendicion(Codigo_, Observacion_, 1);
		}
	}

	private void biRechazar_Click(object sender, EventArgs e)
	{
		int Codigo_ = Convert.ToInt32(dgvRendiciones.SelectedRows[0].Cells[codigoRendicion.Name].Value.ToString());
		string Observacion_ = dgvRendiciones.SelectedRows[0].Cells[observacion.Name].Value.ToString();
		GestionaRendicion(Codigo_, Observacion_, 2);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCajaChicaRendicion));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.biAprobar = new DevComponents.DotNetBar.ButtonItem();
		this.biDesaprobar = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.biAceptar = new DevComponents.DotNetBar.ButtonItem();
		this.biRechazar = new DevComponents.DotNetBar.ButtonItem();
		this.panel2 = new System.Windows.Forms.Panel();
		this.cboSucursal = new System.Windows.Forms.ComboBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvDetalleRendiciones = new System.Windows.Forms.DataGridView();
		this.Item = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codPersonalizado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numGuia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numRecLiquidacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codTipoPagoCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipopagocaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cargadescarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.toneladas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estatus_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.tsmiRendicion = new System.Windows.Forms.ToolStripMenuItem();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.lblTotal = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.dgvRendiciones = new System.Windows.Forms.DataGridView();
		this.codigoRendicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montoRendido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montoaprobado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodesaprobado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaRendicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.observacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalRendiciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.aprobadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.desaprobadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.panel6 = new System.Windows.Forms.Panel();
		this.label5 = new System.Windows.Forms.Label();
		this.panel7 = new System.Windows.Forms.Panel();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalleRendiciones).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRendiciones).BeginInit();
		this.panel6.SuspendLayout();
		this.panel7.SuspendLayout();
		base.SuspendLayout();
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList1.Images.SetKeyName(19, "egreso.png");
		this.imageList1.Images.SetKeyName(20, "ingreso.png");
		this.imageList1.Images.SetKeyName(21, "icon_shelfs.png");
		this.imageList1.Images.SetKeyName(22, "EXIT2.png");
		this.panel1.Controls.Add(this.ribbonBar1);
		this.panel1.Controls.Add(this.ribbonBar2);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1164, 77);
		this.panel1.TabIndex = 13;
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.biAprobar, this.biDesaprobar, this.buttonItem1 });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(354, 77);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 8;
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.biAprobar.Enabled = false;
		this.biAprobar.Image = (System.Drawing.Image)resources.GetObject("biAprobar.Image");
		this.biAprobar.ImageIndex = 20;
		this.biAprobar.ImagePaddingHorizontal = 30;
		this.biAprobar.ImagePaddingVertical = 15;
		this.biAprobar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAprobar.Name = "biAprobar";
		this.biAprobar.SubItemsExpandWidth = 14;
		this.biAprobar.Text = "Aprobar";
		this.biAprobar.Click += new System.EventHandler(biAprobar_Click);
		this.biDesaprobar.Enabled = false;
		this.biDesaprobar.Image = (System.Drawing.Image)resources.GetObject("biDesaprobar.Image");
		this.biDesaprobar.ImageIndex = 19;
		this.biDesaprobar.ImagePaddingHorizontal = 20;
		this.biDesaprobar.ImagePaddingVertical = 15;
		this.biDesaprobar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biDesaprobar.Name = "biDesaprobar";
		this.biDesaprobar.SubItemsExpandWidth = 14;
		this.biDesaprobar.Text = "Desaprobar";
		this.biDesaprobar.Click += new System.EventHandler(biDesaprobar_Click);
		this.buttonItem1.Enabled = false;
		this.buttonItem1.Image = (System.Drawing.Image)resources.GetObject("buttonItem1.Image");
		this.buttonItem1.ImageIndex = 19;
		this.buttonItem1.ImagePaddingHorizontal = 20;
		this.buttonItem1.ImagePaddingVertical = 15;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Deshacer";
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Right;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biAceptar, this.biRechazar });
		this.ribbonBar2.Location = new System.Drawing.Point(354, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(160, 77);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 10;
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.biAceptar.Enabled = false;
		this.biAceptar.Image = (System.Drawing.Image)resources.GetObject("biAceptar.Image");
		this.biAceptar.ImageIndex = 20;
		this.biAceptar.ImagePaddingHorizontal = 30;
		this.biAceptar.ImagePaddingVertical = 15;
		this.biAceptar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAceptar.Name = "biAceptar";
		this.biAceptar.SubItemsExpandWidth = 14;
		this.biAceptar.Text = "Aceptar Rendicion";
		this.biAceptar.Click += new System.EventHandler(biAceptar_Click);
		this.biRechazar.Enabled = false;
		this.biRechazar.Image = (System.Drawing.Image)resources.GetObject("biRechazar.Image");
		this.biRechazar.ImageIndex = 19;
		this.biRechazar.ImagePaddingHorizontal = 20;
		this.biRechazar.ImagePaddingVertical = 15;
		this.biRechazar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRechazar.Name = "biRechazar";
		this.biRechazar.SubItemsExpandWidth = 14;
		this.biRechazar.Text = "Rechazar Rendicion";
		this.biRechazar.Click += new System.EventHandler(biRechazar_Click);
		this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel2.Controls.Add(this.cboSucursal);
		this.panel2.Controls.Add(this.btnSalir);
		this.panel2.Controls.Add(this.btnCancelar);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(514, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(650, 77);
		this.panel2.TabIndex = 9;
		this.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboSucursal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.cboSucursal.FormattingEnabled = true;
		this.cboSucursal.Location = new System.Drawing.Point(147, 18);
		this.cboSucursal.Name = "cboSucursal";
		this.cboSucursal.Size = new System.Drawing.Size(216, 21);
		this.cboSucursal.TabIndex = 11;
		this.cboSucursal.SelectedValueChanged += new System.EventHandler(cboSucursal_SelectedValueChanged);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.ImageIndex = 22;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(568, 7);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(70, 42);
		this.btnSalir.TabIndex = 35;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnCancelar.Enabled = false;
		this.btnCancelar.ImageIndex = 3;
		this.btnCancelar.ImageList = this.imageList2;
		this.btnCancelar.Location = new System.Drawing.Point(369, 7);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(113, 42);
		this.btnCancelar.TabIndex = 34;
		this.btnCancelar.Text = "Cancelar Filtro";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(73, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 13);
		this.label1.TabIndex = 11;
		this.label1.Text = "SUCURSAL:";
		this.dgvDetalleRendiciones.AllowUserToAddRows = false;
		this.dgvDetalleRendiciones.AllowUserToDeleteRows = false;
		this.dgvDetalleRendiciones.AllowUserToResizeRows = false;
		this.dgvDetalleRendiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvDetalleRendiciones.Columns.AddRange(this.Item, this.codigo, this.codPersonalizado, this.concepto, this.fecha, this.numDocumento, this.numGuia, this.numRecLiquidacion, this.codTipoPagoCaja, this.tipopagocaja, this.cargadescarga, this.tipoMovimiento, this.toneladas, this.monto, this.comentario, this.status, this.estatus_);
		this.dgvDetalleRendiciones.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalleRendiciones.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvDetalleRendiciones.Location = new System.Drawing.Point(0, 217);
		this.dgvDetalleRendiciones.MultiSelect = false;
		this.dgvDetalleRendiciones.Name = "dgvDetalleRendiciones";
		this.dgvDetalleRendiciones.RowHeadersVisible = false;
		this.dgvDetalleRendiciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalleRendiciones.Size = new System.Drawing.Size(1164, 114);
		this.dgvDetalleRendiciones.TabIndex = 16;
		this.dgvDetalleRendiciones.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvRendiciones_CellValueChanged);
		this.dgvDetalleRendiciones.CurrentCellDirtyStateChanged += new System.EventHandler(dgvRendiciones_CurrentCellDirtyStateChanged);
		this.dgvDetalleRendiciones.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalleRendiciones_RowStateChanged);
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.NullValue = false;
		dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
		this.Item.DefaultCellStyle = dataGridViewCellStyle1;
		this.Item.HeaderText = "ITEM";
		this.Item.Name = "Item";
		this.Item.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.Item.Width = 40;
		this.codigo.DataPropertyName = "codDetalleRendicion";
		this.codigo.HeaderText = "COD";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.codigo.Width = 70;
		this.codPersonalizado.DataPropertyName = "codPersonalizado";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.codPersonalizado.DefaultCellStyle = dataGridViewCellStyle2;
		this.codPersonalizado.HeaderText = "CODIGO";
		this.codPersonalizado.Name = "codPersonalizado";
		this.codPersonalizado.ReadOnly = true;
		this.codPersonalizado.Width = 70;
		this.concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.concepto.DataPropertyName = "concepto";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.concepto.DefaultCellStyle = dataGridViewCellStyle3;
		this.concepto.HeaderText = "CONCEPTO";
		this.concepto.Name = "concepto";
		this.concepto.ReadOnly = true;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle4;
		this.fecha.HeaderText = "FECHA";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.numDocumento.DataPropertyName = "NumDocumento";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numDocumento.DefaultCellStyle = dataGridViewCellStyle5;
		this.numDocumento.HeaderText = "N° DOC.";
		this.numDocumento.Name = "numDocumento";
		this.numDocumento.ReadOnly = true;
		this.numDocumento.Width = 80;
		this.numGuia.DataPropertyName = "numGuia";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numGuia.DefaultCellStyle = dataGridViewCellStyle6;
		this.numGuia.HeaderText = "N° GUIA";
		this.numGuia.Name = "numGuia";
		this.numGuia.ReadOnly = true;
		this.numGuia.Width = 80;
		this.numRecLiquidacion.DataPropertyName = "numRecLiquidacion";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numRecLiquidacion.DefaultCellStyle = dataGridViewCellStyle7;
		this.numRecLiquidacion.HeaderText = "N° REC. LIQ.";
		this.numRecLiquidacion.Name = "numRecLiquidacion";
		this.numRecLiquidacion.ReadOnly = true;
		this.numRecLiquidacion.Width = 80;
		this.codTipoPagoCaja.DataPropertyName = "codTipoPagoCaja";
		this.codTipoPagoCaja.HeaderText = "COD. TIPO PAGO";
		this.codTipoPagoCaja.Name = "codTipoPagoCaja";
		this.codTipoPagoCaja.ReadOnly = true;
		this.codTipoPagoCaja.Visible = false;
		this.tipopagocaja.DataPropertyName = "TipoPagoCaja";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.tipopagocaja.DefaultCellStyle = dataGridViewCellStyle8;
		this.tipopagocaja.HeaderText = "TIPO PAGO";
		this.tipopagocaja.Name = "tipopagocaja";
		this.tipopagocaja.ReadOnly = true;
		this.tipopagocaja.Width = 200;
		this.cargadescarga.DataPropertyName = "cargadescarga";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.cargadescarga.DefaultCellStyle = dataGridViewCellStyle9;
		this.cargadescarga.HeaderText = "CARGA/DESCARGA";
		this.cargadescarga.Name = "cargadescarga";
		this.cargadescarga.ReadOnly = true;
		this.cargadescarga.Width = 120;
		this.tipoMovimiento.DataPropertyName = "TipoMovimiento";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.tipoMovimiento.DefaultCellStyle = dataGridViewCellStyle10;
		this.tipoMovimiento.HeaderText = "TIPO MOV.";
		this.tipoMovimiento.Name = "tipoMovimiento";
		this.tipoMovimiento.ReadOnly = true;
		this.tipoMovimiento.Width = 80;
		this.toneladas.DataPropertyName = "toneladas";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "N2";
		dataGridViewCellStyle11.NullValue = null;
		dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.toneladas.DefaultCellStyle = dataGridViewCellStyle11;
		this.toneladas.HeaderText = "TONELADAS";
		this.toneladas.Name = "toneladas";
		this.toneladas.ReadOnly = true;
		this.toneladas.Width = 80;
		this.monto.DataPropertyName = "monto";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Navy;
		dataGridViewCellStyle12.Format = "N2";
		dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.monto.DefaultCellStyle = dataGridViewCellStyle12;
		this.monto.HeaderText = "MONTO";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.comentario.DataPropertyName = "comentario";
		this.comentario.HeaderText = "COMENTARIO";
		this.comentario.Name = "comentario";
		this.comentario.Visible = false;
		this.status.DataPropertyName = "estatus";
		this.status.HeaderText = "COD. STATUS";
		this.status.Name = "status";
		this.status.Visible = false;
		this.estatus_.DataPropertyName = "status";
		this.estatus_.HeaderText = "STATUS";
		this.estatus_.Name = "estatus_";
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.tsmiRendicion });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(174, 26);
		this.tsmiRendicion.Image = (System.Drawing.Image)resources.GetObject("tsmiRendicion.Image");
		this.tsmiRendicion.Name = "tsmiRendicion";
		this.tsmiRendicion.Size = new System.Drawing.Size(173, 22);
		this.tsmiRendicion.Text = "Eliminar Rendicion";
		this.panel3.Controls.Add(this.panel4);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 331);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1164, 35);
		this.panel3.TabIndex = 17;
		this.panel4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.panel4.Controls.Add(this.txtComentario);
		this.panel4.Controls.Add(this.label3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(1164, 35);
		this.panel4.TabIndex = 48;
		this.txtComentario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtComentario.BackColor = System.Drawing.Color.FromArgb(200, 255, 200);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(100, 8);
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(1052, 20);
		this.txtComentario.TabIndex = 48;
		this.txtComentario.KeyUp += new System.Windows.Forms.KeyEventHandler(txtComentario_KeyUp);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(12, 12);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(82, 13);
		this.label3.TabIndex = 47;
		this.label3.Text = "COMENTARIO:";
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTotal.BackColor = System.Drawing.Color.Gainsboro;
		this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblTotal.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.ForeColor = System.Drawing.Color.Red;
		this.lblTotal.Location = new System.Drawing.Point(1053, 3);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(100, 20);
		this.lblTotal.TabIndex = 47;
		this.lblTotal.Text = "0.000";
		this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(902, 7);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(145, 13);
		this.label4.TabIndex = 46;
		this.label4.Text = "TOTAL DESAPROBADO S/:";
		this.dgvRendiciones.AllowUserToAddRows = false;
		this.dgvRendiciones.AllowUserToDeleteRows = false;
		this.dgvRendiciones.AllowUserToResizeRows = false;
		this.dgvRendiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvRendiciones.Columns.AddRange(this.codigoRendicion, this.numDoc, this.montoRendido, this.montoaprobado, this.montodesaprobado, this.fechaRendicion, this.observacion, this.estatus, this.totalRendiciones, this.aprobadas, this.desaprobadas);
		this.dgvRendiciones.Dock = System.Windows.Forms.DockStyle.Top;
		this.dgvRendiciones.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvRendiciones.Location = new System.Drawing.Point(0, 77);
		this.dgvRendiciones.MultiSelect = false;
		this.dgvRendiciones.Name = "dgvRendiciones";
		this.dgvRendiciones.RowHeadersVisible = false;
		this.dgvRendiciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvRendiciones.Size = new System.Drawing.Size(1164, 115);
		this.dgvRendiciones.TabIndex = 18;
		this.dgvRendiciones.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvRendiciones_RowStateChanged);
		this.codigoRendicion.DataPropertyName = "codRendicion";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.codigoRendicion.DefaultCellStyle = dataGridViewCellStyle13;
		this.codigoRendicion.HeaderText = "COD. RENDICION";
		this.codigoRendicion.Name = "codigoRendicion";
		this.codigoRendicion.ReadOnly = true;
		this.codigoRendicion.Visible = false;
		this.codigoRendicion.Width = 120;
		this.numDoc.DataPropertyName = "numDocumento";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numDoc.DefaultCellStyle = dataGridViewCellStyle14;
		this.numDoc.HeaderText = "N° DOCUMENTO";
		this.numDoc.Name = "numDoc";
		this.numDoc.ReadOnly = true;
		this.montoRendido.DataPropertyName = "montoRendido";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
		this.montoRendido.DefaultCellStyle = dataGridViewCellStyle15;
		this.montoRendido.HeaderText = "MONTO RENDIDO";
		this.montoRendido.Name = "montoRendido";
		this.montoRendido.ReadOnly = true;
		this.montoRendido.Width = 140;
		this.montoaprobado.DataPropertyName = "montoAprobado";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
		this.montoaprobado.DefaultCellStyle = dataGridViewCellStyle16;
		this.montoaprobado.HeaderText = "MONTO APROBADO";
		this.montoaprobado.Name = "montoaprobado";
		this.montoaprobado.ReadOnly = true;
		this.montoaprobado.Width = 140;
		this.montodesaprobado.DataPropertyName = "montoDesaprobado";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
		this.montodesaprobado.DefaultCellStyle = dataGridViewCellStyle17;
		this.montodesaprobado.HeaderText = "MONTO DESAPROBADO";
		this.montodesaprobado.Name = "montodesaprobado";
		this.montodesaprobado.ReadOnly = true;
		this.montodesaprobado.Width = 140;
		this.fechaRendicion.DataPropertyName = "fechaRendicion";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.fechaRendicion.DefaultCellStyle = dataGridViewCellStyle18;
		this.fechaRendicion.HeaderText = "FECHA RENDICION";
		this.fechaRendicion.Name = "fechaRendicion";
		this.fechaRendicion.ReadOnly = true;
		this.fechaRendicion.Width = 130;
		this.observacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.observacion.DataPropertyName = "observacion";
		this.observacion.HeaderText = "OBSERVACION";
		this.observacion.MaxInputLength = 50;
		this.observacion.Name = "observacion";
		this.estatus.DataPropertyName = "estatus";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle19.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
		this.estatus.DefaultCellStyle = dataGridViewCellStyle19;
		this.estatus.HeaderText = "STATUS";
		this.estatus.Name = "estatus";
		this.estatus.ReadOnly = true;
		this.estatus.Width = 80;
		this.totalRendiciones.DataPropertyName = "TotalRendiciones";
		this.totalRendiciones.HeaderText = "RENDICIONES";
		this.totalRendiciones.Name = "totalRendiciones";
		this.totalRendiciones.ReadOnly = true;
		this.totalRendiciones.Visible = false;
		this.aprobadas.DataPropertyName = "Aprobadas";
		this.aprobadas.HeaderText = "APROBADAS";
		this.aprobadas.Name = "aprobadas";
		this.aprobadas.ReadOnly = true;
		this.aprobadas.Visible = false;
		this.desaprobadas.DataPropertyName = "Desaprobadas";
		this.desaprobadas.HeaderText = "DESAPROBADAS";
		this.desaprobadas.Name = "desaprobadas";
		this.desaprobadas.ReadOnly = true;
		this.desaprobadas.Visible = false;
		this.panel6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel6.Controls.Add(this.label5);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 192);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(1164, 25);
		this.panel6.TabIndex = 49;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(12, 3);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(155, 13);
		this.label5.TabIndex = 47;
		this.label5.Text = "DETALLE DE LA RENDICION:";
		this.panel7.Controls.Add(this.label8);
		this.panel7.Controls.Add(this.label9);
		this.panel7.Controls.Add(this.label6);
		this.panel7.Controls.Add(this.label7);
		this.panel7.Controls.Add(this.lblTotal);
		this.panel7.Controls.Add(this.label4);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 366);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(1164, 26);
		this.panel7.TabIndex = 50;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.BackColor = System.Drawing.Color.Gainsboro;
		this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label8.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.MidnightBlue;
		this.label8.Location = new System.Drawing.Point(308, 3);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(100, 20);
		this.label8.TabIndex = 51;
		this.label8.Text = "0.000";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Location = new System.Drawing.Point(189, 7);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(113, 13);
		this.label9.TabIndex = 50;
		this.label9.Text = "TOTAL RENDIDO S/:";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.BackColor = System.Drawing.Color.Gainsboro;
		this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label6.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.RoyalBlue;
		this.label6.Location = new System.Drawing.Point(766, 3);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(100, 20);
		this.label6.TabIndex = 49;
		this.label6.Text = "0.000";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(637, 7);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(123, 13);
		this.label7.TabIndex = 48;
		this.label7.Text = "TOTAL APROBADO S/:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1164, 392);
		base.Controls.Add(this.dgvDetalleRendiciones);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.dgvRendiciones);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel7);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmCajaChicaRendicion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Rendicion Caja Chica";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmCajaChicaRendicion_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalleRendiciones).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRendiciones).EndInit();
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		base.ResumeLayout(false);
	}
}
