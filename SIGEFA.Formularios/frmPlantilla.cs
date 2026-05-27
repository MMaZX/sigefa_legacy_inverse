using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmPlantilla : Office2007Form
{
	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar2;

	private ButtonItem buttonItem16;

	private ButtonItem buttonItem6;

	private ButtonItem buttonItem8;

	private ButtonItem buttonItem3;

	private ButtonItem buttonItem4;

	private ButtonItem buttonItem5;

	private ButtonItem buttonItem9;

	private DataGridView dataGridView1;

	public frmPlantilla()
	{
		InitializeComponent();
	}

	private void frmPlantilla_Load(object sender, EventArgs e)
	{
	}

	private void buttonItem16_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPlantilla));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
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
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[7] { this.buttonItem16, this.buttonItem6, this.buttonItem8, this.buttonItem3, this.buttonItem4, this.buttonItem5, this.buttonItem9 });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(778, 55);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 3;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.buttonItem16.ImageIndex = 4;
		this.buttonItem16.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem16.Name = "buttonItem16";
		this.buttonItem16.SubItemsExpandWidth = 14;
		this.buttonItem16.Text = "Nuevo";
		this.buttonItem16.Click += new System.EventHandler(buttonItem16_Click);
		this.buttonItem6.ImageIndex = 3;
		this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem6.Name = "buttonItem6";
		this.buttonItem6.SubItemsExpandWidth = 14;
		this.buttonItem6.Text = "Modificar";
		this.buttonItem8.ImageIndex = 5;
		this.buttonItem8.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem8.Name = "buttonItem8";
		this.buttonItem8.SubItemsExpandWidth = 14;
		this.buttonItem8.Text = "Eliminar";
		this.buttonItem3.ImageIndex = 6;
		this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem3.Name = "buttonItem3";
		this.buttonItem3.SubItemsExpandWidth = 14;
		this.buttonItem3.Text = "Copiar";
		this.buttonItem4.ImageIndex = 8;
		this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem4.Name = "buttonItem4";
		this.buttonItem4.SubItemsExpandWidth = 14;
		this.buttonItem4.Text = "Actualizar";
		this.buttonItem5.ImageIndex = 11;
		this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem5.Name = "buttonItem5";
		this.buttonItem5.SubItemsExpandWidth = 14;
		this.buttonItem5.Text = "Buscar";
		this.buttonItem9.ImageIndex = 7;
		this.buttonItem9.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem9.Name = "buttonItem9";
		this.buttonItem9.SubItemsExpandWidth = 14;
		this.buttonItem9.Text = "Imprimir";
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(0, 55);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(778, 419);
		this.dataGridView1.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(778, 474);
		base.Controls.Add(this.dataGridView1);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.Name = "frmPlantilla";
		base.Load += new System.EventHandler(frmPlantilla_Load);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
	}
}
