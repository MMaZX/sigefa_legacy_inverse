using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RikTheVeggie;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmAccesosSucursal : Office2007Form
{
	private clsAdmAccesoSucursales AdmAcceSuc = new clsAdmAccesoSucursales();

	private clsAccesosSucursales AcceSuc = new clsAccesosSucursales();

	private clsAdmSucursal AdmSuc = new clsAdmSucursal();

	private clsSucursal suc = new clsSucursal();

	private clsAdmFormulario admForm = new clsAdmFormulario();

	public clsUsuario usu = new clsUsuario();

	private DataTable Arbol = new DataTable();

	public List<int> codigos = new List<int>();

	private IContainer components = null;

	private Panel panel1;

	private Panel panel2;

	private Button btnSalir;

	private ImageList imageList1;

	private Button btnGuardar;

	private Label label1;

	private TriStateTreeView tvSeleccionAlmacenes;

	public frmAccesosSucursal()
	{
		InitializeComponent();
	}

	private void ConsultaArbol()
	{
		Arbol = AdmSuc.CargaSucursalesSeleccion(frmLogin.iCodEmpresa);
	}

	private void llenaarbol(int nivel, int indicePadre, TreeNode nodoPadre)
	{
		DataView hijos = new DataView(Arbol);
		hijos.RowFilter = Arbol.Columns["codpadre"].ColumnName + " = " + indicePadre;
		DataView dataView = hijos;
		dataView.RowFilter = dataView.RowFilter + " AND " + Arbol.Columns["nivel"].ColumnName + " = " + nivel;
		foreach (DataRowView row in hijos)
		{
			TreeNode nuevonodo = new TreeNode();
			nuevonodo.Text = row["descripcion"].ToString();
			nuevonodo.Tag = row["codigo"].ToString();
			if (nodoPadre == null)
			{
				tvSeleccionAlmacenes.Nodes.Add(nuevonodo);
			}
			else
			{
				nodoPadre.Nodes.Add(nuevonodo);
			}
			llenaarbol(nivel + 1, int.Parse(row["codigo"].ToString()), nuevonodo);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void RecorreArbol()
	{
		codigos.Clear();
		if (tvSeleccionAlmacenes.Enabled)
		{
			foreach (TreeNode nod in tvSeleccionAlmacenes.Nodes)
			{
				añadenodos(nod);
			}
		}
		suc.CodigosForm = codigos;
	}

	private void añadenodos(TreeNode nod)
	{
		if (nod.StateImageIndex == 1)
		{
			codigos.Add(Convert.ToInt32(nod.Tag));
		}
		if (nod.Nodes.Count <= 0)
		{
			return;
		}
		foreach (TreeNode tn in nod.Nodes)
		{
			añadenodos(tn);
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		RecorreArbol();
		AdmAcceSuc.LimpiarAccesos(usu.CodUsuario, frmLogin.iCodEmpresa);
		if (codigos.Count > 0)
		{
			AcceSuc.CodUsuario = usu.CodUsuario;
			AcceSuc.CodEmpresa = frmLogin.iCodEmpresa;
			AcceSuc.CodUser = frmLogin.iCodUser;
			foreach (int formu in suc.CodigosForm)
			{
				AcceSuc.CodSucursal = formu;
				AdmAcceSuc.insert(AcceSuc);
			}
		}
		MessageBox.Show("Los datos se guardaron correctamente", "Otorga Accesos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		btnGuardar.Enabled = false;
	}

	private void tvSeleccionAlmacenes_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		btnGuardar.Enabled = true;
	}

	private void CargaAccesos()
	{
		codigos = AdmAcceSuc.MuestraAccesosSucursales(usu.CodUsuario, frmLogin.iCodEmpresa);
		foreach (TreeNode nod in tvSeleccionAlmacenes.Nodes)
		{
			marcanodo(nod);
		}
	}

	private void marcanodo(TreeNode nod)
	{
		int codi = Convert.ToInt32(nod.Tag);
		if (codigos.Contains(codi))
		{
			nod.Checked = true;
		}
		else
		{
			nod.Checked = false;
		}
		if (nod.Nodes.Count <= 0)
		{
			return;
		}
		foreach (TreeNode tn in nod.Nodes)
		{
			marcanodo(tn);
		}
	}

	private void frmAccesosSucursal_Load(object sender, EventArgs e)
	{
	}

	private void frmAccesosSucursal_Shown(object sender, EventArgs e)
	{
		ConsultaArbol();
		llenaarbol(0, 0, null);
		tvSeleccionAlmacenes.ExpandAll();
		CargaAccesos();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmAccesosSucursal));
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnGuardar = new System.Windows.Forms.Button();
		this.tvSeleccionAlmacenes = new RikTheVeggie.TriStateTreeView();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.label1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(412, 29);
		this.panel1.TabIndex = 13;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(113, 13);
		this.label1.TabIndex = 15;
		this.label1.Text = "Seleccion de Accesos";
		this.panel2.Controls.Add(this.btnSalir);
		this.panel2.Controls.Add(this.btnGuardar);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 388);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(412, 37);
		this.panel2.TabIndex = 14;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(331, 6);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(69, 23);
		this.btnSalir.TabIndex = 19;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 1;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(242, 6);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(75, 23);
		this.btnGuardar.TabIndex = 18;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.tvSeleccionAlmacenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tvSeleccionAlmacenes.Location = new System.Drawing.Point(0, 29);
		this.tvSeleccionAlmacenes.Name = "tvSeleccionAlmacenes";
		this.tvSeleccionAlmacenes.Size = new System.Drawing.Size(412, 359);
		this.tvSeleccionAlmacenes.TabIndex = 22;
		this.tvSeleccionAlmacenes.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Standard;
		this.tvSeleccionAlmacenes.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvSeleccionAlmacenes_NodeMouseClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(412, 425);
		base.Controls.Add(this.tvSeleccionAlmacenes);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAccesosSucursal";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Accesos Sucursal";
		base.Load += new System.EventHandler(frmAccesosSucursal_Load);
		base.Shown += new System.EventHandler(frmAccesosSucursal_Shown);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
