using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Formularios;

namespace SIGEFA.Reportes;

public class frmMenuReportes : Office2007Form
{
	private IContainer components = null;

	private TreeView tvRptFacturacion;

	private TreeView tvRptInventarios;

	private Label label1;

	private Label label2;

	public frmMenuReportes()
	{
		InitializeComponent();
	}

	private void tvRptFacturacion_DoubleClick(object sender, EventArgs e)
	{
		TreeNode node = tvRptFacturacion.SelectedNode;
		if (node.Name == "tvInfVentCC")
		{
			frmParamVentCredCont form = new frmParamVentCredCont();
			form.ShowDialog();
		}
		if (node.Name == "VentasSeparacion")
		{
			frmParamVentSeparacion form2 = new frmParamVentSeparacion();
			form2.ShowDialog();
		}
		if (node.Name == "tvInfVentasxClient")
		{
			frmParamVentxCliente2 form3 = new frmParamVentxCliente2();
			form3.ShowDialog();
		}
		if (node.Name == "tvVentasMesArticulo")
		{
			frmParamVentasMesArticulo form4 = new frmParamVentasMesArticulo();
			form4.criterio = 0;
			form4.ShowDialog();
		}
		if (node.Name == "tvInfVentVende")
		{
			frmParamVentxVendedor form5 = new frmParamVentxVendedor();
			form5.ShowDialog();
		}
		if (node.Name == "tvDespachoDocumento")
		{
			frmParamDespachoDocumento form6 = new frmParamDespachoDocumento();
			form6.ShowDialog();
		}
		if (node.Name == "tvCobros")
		{
			frmParamCobrosDia form7 = new frmParamCobrosDia();
			form7.ShowDialog();
		}
		if (node.Name == "tvPagosxDia")
		{
			frmParamPagosDia form8 = new frmParamPagosDia();
			form8.ShowDialog();
		}
		if (node.Name == "tvArticulo")
		{
			frmParamRankingxArticulo form9 = new frmParamRankingxArticulo();
			form9.ShowDialog();
		}
		if (node.Name == "tvCliente")
		{
			frmParamRankingxCliente form10 = new frmParamRankingxCliente();
			form10.ShowDialog();
		}
		if (node.Name == "tvVentasxArticulo")
		{
			frmParamVentxArticulo form11 = new frmParamVentxArticulo();
			form11.ShowDialog();
		}
		if (node.Name == "tvInfVentVendArt")
		{
			frmParamVentxArticuloxVendedor form12 = new frmParamVentxArticuloxVendedor();
			form12.ShowDialog();
		}
		if (node.Name == "tvInfVentCCS")
		{
			frmParamVentCredContSucursal form13 = new frmParamVentCredContSucursal();
			form13.ShowDialog();
		}
		if (node.Name == "tvCobranzas")
		{
			frmParamCobranzaSucursal form14 = new frmParamCobranzaSucursal();
			form14.ShowDialog();
		}
		if (node.Name == "tvVentasxClient")
		{
			frmParamVentasXClientes form15 = new frmParamVentasXClientes();
			form15.ShowDialog();
		}
		if (node.Name == "VentasUtilidad")
		{
			UtilidadVentas form16 = new UtilidadVentas();
			form16.ShowDialog();
		}
		if (node.Name == "ProductosUtilidad")
		{
			UtilidadProductos form17 = new UtilidadProductos();
			form17.ShowDialog();
		}
		if (node.Name == "ReporteCompras")
		{
			frmReporteCompras form18 = new frmReporteCompras();
			form18.ShowDialog();
		}
	}

	private void tvInventarios_DoubleClick(object sender, EventArgs e)
	{
		TreeNode node = tvRptInventarios.SelectedNode;
		if (node.Name == "tvInvStockArticulos")
		{
			frmReporteInventario form = new frmReporteInventario();
			form.ShowDialog();
		}
		if (node.Name == "tvInvStockArtMens")
		{
			FrmParamStockArticulosMensu form2 = new FrmParamStockArticulosMensu();
			form2.ShowDialog();
		}
		if (node.Name == "tvKardex")
		{
			frmParamKardexArticulo form3 = new frmParamKardexArticulo();
			form3.ShowDialog();
		}
		if (node.Name == "tvDespachoArticulo")
		{
			frmParamDespachoxArticulo form4 = new frmParamDespachoxArticulo();
			form4.ShowDialog();
		}
		if (node.Name == "tvMercaderiaAtender")
		{
			frmParamMercaderiaEntregar form5 = new frmParamMercaderiaEntregar();
			form5.ShowDialog();
		}
		if (node.Name == "tvInvAlmacenVendeMas")
		{
			frmAlmacenVendeMas form6 = new frmAlmacenVendeMas();
			form6.ShowDialog();
		}
	}

	private void frmMenuReportes_Load(object sender, EventArgs e)
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
		System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Ventas Contado/Crédito");
		System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Ventas por Separación");
		System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Ventas por Vendedor");
		System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Ventas por Vendedor por Articulo");
		System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Ventas por Cliente");
		System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Ventas x Articulo");
		System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Despacho x N° Documento");
		System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Cobros x Día");
		System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Pagos x Día");
		System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Ventas Utilidad");
		System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Productos Uilidad");
		System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Informes Diarios", new System.Windows.Forms.TreeNode[11]
		{
			treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7, treeNode8, treeNode9, treeNode10,
			treeNode11
		});
		System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Ventas Mensuales por Artículo");
		System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Ventas Mensuales", new System.Windows.Forms.TreeNode[1] { treeNode13 });
		System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Ventas Contado/Crédito");
		System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Cobranzas");
		System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Informes Por Sucursal", new System.Windows.Forms.TreeNode[2] { treeNode15, treeNode16 });
		System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Reportes General");
		System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Stock de Artículos");
		System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Almacen Que Vende Más");
		System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Kardex de Artículos");
		System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Inventario en Unidades", new System.Windows.Forms.TreeNode[3] { treeNode19, treeNode20, treeNode21 });
		System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Despacho x Artículo");
		System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("MercaderiaPorEntregar");
		System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Documentos de Almacen", new System.Windows.Forms.TreeNode[2] { treeNode23, treeNode24 });
		this.tvRptFacturacion = new System.Windows.Forms.TreeView();
		this.tvRptInventarios = new System.Windows.Forms.TreeView();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.tvRptFacturacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.tvRptFacturacion.Cursor = System.Windows.Forms.Cursors.Default;
		this.tvRptFacturacion.Location = new System.Drawing.Point(0, 29);
		this.tvRptFacturacion.Name = "tvRptFacturacion";
		treeNode1.Name = "tvInfVentCC";
		treeNode1.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode1.SelectedImageIndex = -2;
		treeNode1.Text = "Ventas Contado/Crédito";
		treeNode2.Name = "VentasSeparacion";
		treeNode2.Text = "Ventas por Separación";
		treeNode3.Name = "tvInfVentVende";
		treeNode3.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode3.Text = "Ventas por Vendedor";
		treeNode4.Name = "tvInfVentVendArt";
		treeNode4.NodeFont = new System.Drawing.Font("Courier New", 9.75f);
		treeNode4.Text = "Ventas por Vendedor por Articulo";
		treeNode5.Name = "tvInfVentasxClient";
		treeNode5.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode5.Text = "Ventas por Cliente";
		treeNode6.Name = "tvVentasxArticulo";
		treeNode6.NodeFont = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode6.Text = "Ventas x Articulo";
		treeNode7.Name = "tvDespachoDocumento";
		treeNode7.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode7.Text = "Despacho x N° Documento";
		treeNode8.Name = "tvCobros";
		treeNode8.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode8.Text = "Cobros x Día";
		treeNode9.Name = "tvPagosxDia";
		treeNode9.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode9.Text = "Pagos x Día";
		treeNode10.Name = "VentasUtilidad";
		treeNode10.Text = "Ventas Utilidad";
		treeNode11.Name = "ProductosUtilidad";
		treeNode11.Text = "Productos Uilidad";
		treeNode12.Name = "tvInformes";
		treeNode12.NodeFont = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode12.Text = "Informes Diarios";
		treeNode13.Name = "tvVentasMesArticulo";
		treeNode13.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode13.Text = "Ventas Mensuales por Artículo";
		treeNode14.Name = "Node19";
		treeNode14.NodeFont = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode14.Text = "Ventas Mensuales";
		treeNode15.Name = "tvInfVentCCS";
		treeNode15.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode15.Text = "Ventas Contado/Crédito";
		treeNode16.Name = "tvCobranzas";
		treeNode16.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode16.Text = "Cobranzas";
		treeNode17.Name = "tvInformesSucursal";
		treeNode17.NodeFont = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode17.Text = "Informes Por Sucursal";
		treeNode18.Name = "ReporteCompras";
		treeNode18.NodeFont = new System.Drawing.Font("Courier New", 11.25f);
		treeNode18.Text = "Reportes General";
		this.tvRptFacturacion.Nodes.AddRange(new System.Windows.Forms.TreeNode[4] { treeNode12, treeNode14, treeNode17, treeNode18 });
		this.tvRptFacturacion.Size = new System.Drawing.Size(322, 190);
		this.tvRptFacturacion.TabIndex = 0;
		this.tvRptFacturacion.DoubleClick += new System.EventHandler(tvRptFacturacion_DoubleClick);
		this.tvRptInventarios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.tvRptInventarios.Cursor = System.Windows.Forms.Cursors.Default;
		this.tvRptInventarios.Location = new System.Drawing.Point(328, 29);
		this.tvRptInventarios.Name = "tvRptInventarios";
		treeNode19.Name = "tvInvStockArticulos";
		treeNode19.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode19.SelectedImageIndex = -2;
		treeNode19.Text = "Stock de Artículos";
		treeNode20.Name = "tvInvAlmacenVendeMas";
		treeNode20.NodeFont = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode20.Text = "Almacen Que Vende Más";
		treeNode21.Name = "tvKardex";
		treeNode21.NodeFont = new System.Drawing.Font("Courier New", 11.25f);
		treeNode21.Text = "Kardex de Artículos";
		treeNode22.Name = "tvInformes";
		treeNode22.NodeFont = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode22.Text = "Inventario en Unidades";
		treeNode23.Name = "tvDespachoArticulo";
		treeNode23.NodeFont = new System.Drawing.Font("Courier New", 11.25f);
		treeNode23.Text = "Despacho x Artículo";
		treeNode24.Name = "tvMercaderiaAtender";
		treeNode24.NodeFont = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode24.Text = "MercaderiaPorEntregar";
		treeNode25.Name = "Node28";
		treeNode25.NodeFont = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		treeNode25.Text = "Documentos de Almacen";
		this.tvRptInventarios.Nodes.AddRange(new System.Windows.Forms.TreeNode[2] { treeNode22, treeNode25 });
		this.tvRptInventarios.Size = new System.Drawing.Size(327, 190);
		this.tvRptInventarios.TabIndex = 1;
		this.tvRptInventarios.DoubleClick += new System.EventHandler(tvInventarios_DoubleClick);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(108, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(94, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "FACTURACIÓN";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(438, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(91, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "INVENTARIOS";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(654, 222);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.tvRptInventarios);
		base.Controls.Add(this.tvRptFacturacion);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmMenuReportes";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reportes";
		base.Load += new System.EventHandler(frmMenuReportes_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
