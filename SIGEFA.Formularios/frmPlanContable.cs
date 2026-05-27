using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;

namespace SIGEFA.Formularios;

public class frmPlanContable : Form
{
	private DataTable Arbol = new DataTable();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmPlanContable admplan = new clsAdmPlanContable();

	private int codplan = 0;

	private IContainer components = null;

	private GroupBox gbPlan;

	private TreeView tvClasificacion;

	public frmPlanContable()
	{
		InitializeComponent();
	}

	private int RetornaCodPlan()
	{
		DataTable dt = new DataTable();
		foreach (DataRow row in dt.Rows)
		{
			codplan = Convert.ToInt32(row[0]);
		}
		return codplan;
	}

	private void ConsultarArbol()
	{
		Arbol = admplan.ListaPlanContableArbol();
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
				nuevonodo.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
				tvClasificacion.Nodes.Add(nuevonodo);
			}
			else
			{
				nodoPadre.Nodes.Add(nuevonodo);
			}
			llenaarbol(nivel + 1, int.Parse(row["codigo"].ToString()), nuevonodo);
		}
	}

	private void frmPlanContable_Load(object sender, EventArgs e)
	{
		ConsultarArbol();
		llenaarbol(0, 0, null);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPlanContable));
		this.gbPlan = new System.Windows.Forms.GroupBox();
		this.tvClasificacion = new System.Windows.Forms.TreeView();
		this.gbPlan.SuspendLayout();
		base.SuspendLayout();
		this.gbPlan.Controls.Add(this.tvClasificacion);
		this.gbPlan.Location = new System.Drawing.Point(12, 12);
		this.gbPlan.Name = "gbPlan";
		this.gbPlan.Size = new System.Drawing.Size(747, 567);
		this.gbPlan.TabIndex = 0;
		this.gbPlan.TabStop = false;
		this.gbPlan.Text = "PCGE";
		this.tvClasificacion.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tvClasificacion.Location = new System.Drawing.Point(3, 16);
		this.tvClasificacion.Name = "tvClasificacion";
		this.tvClasificacion.Size = new System.Drawing.Size(741, 548);
		this.tvClasificacion.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(771, 591);
		base.Controls.Add(this.gbPlan);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmPlanContable";
		this.Text = "Plan Contable General Empresarial";
		base.Load += new System.EventHandler(frmPlanContable_Load);
		this.gbPlan.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
