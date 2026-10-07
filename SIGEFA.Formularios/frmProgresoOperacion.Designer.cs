using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEFA.Formularios;

public partial class frmProgresoOperacion
{
    private IContainer components = null;

    private Label lblTexto;
    private ProgressBar pbMarquee;
    private ListView lvwPasos;
    private ColumnHeader colPaso;
    private ColumnHeader colEstado;
    private ColumnHeader colDetalle;
    private TextBox txtDetalle;
    private Button btnCopiar;
    private Button btnCerrar;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.lblTexto = new Label();
        this.pbMarquee = new ProgressBar();
        this.lvwPasos = new ListView();
        this.colPaso = new ColumnHeader();
        this.colEstado = new ColumnHeader();
        this.colDetalle = new ColumnHeader();
        this.txtDetalle = new TextBox();
        this.btnCopiar = new Button();
        this.btnCerrar = new Button();
        this.SuspendLayout();
        // 
        // lblTexto
        // 
        this.lblTexto.Location = new Point(16, 16);
        this.lblTexto.Name = "lblTexto";
        this.lblTexto.Size = new Size(488, 22);
        this.lblTexto.TabIndex = 0;
        this.lblTexto.Text = "Procesando operación...";
        // 
        // pbMarquee
        // 
        this.pbMarquee.Location = new Point(16, 44);
        this.pbMarquee.Name = "pbMarquee";
        this.pbMarquee.Size = new Size(488, 23);
        this.pbMarquee.Style = ProgressBarStyle.Marquee;
        this.pbMarquee.TabIndex = 1;
        // 
        // lvwPasos
        // 
        this.lvwPasos.Columns.AddRange(new ColumnHeader[] {
            this.colPaso,
            this.colEstado,
            this.colDetalle
        });
        this.lvwPasos.FullRowSelect = true;
        this.lvwPasos.GridLines = true;
        this.lvwPasos.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        this.lvwPasos.HideSelection = false;
        this.lvwPasos.Location = new Point(16, 16);
        this.lvwPasos.MultiSelect = false;
        this.lvwPasos.Name = "lvwPasos";
        this.lvwPasos.Size = new Size(528, 250);
        this.lvwPasos.TabIndex = 2;
        this.lvwPasos.UseCompatibleStateImageBehavior = false;
        this.lvwPasos.View = View.Details;
        // 
        // colPaso
        // 
        this.colPaso.Text = "Paso";
        this.colPaso.Width = 200;
        // 
        // colEstado
        // 
        this.colEstado.Text = "Estado";
        this.colEstado.Width = 95;
        // 
        // colDetalle
        // 
        this.colDetalle.Text = "Detalle";
        this.colDetalle.Width = 225;
        // 
        // txtDetalle
        // 
        this.txtDetalle.Location = new Point(16, 276);
        this.txtDetalle.Multiline = true;
        this.txtDetalle.Name = "txtDetalle";
        this.txtDetalle.ReadOnly = true;
        this.txtDetalle.ScrollBars = ScrollBars.Vertical;
        this.txtDetalle.Size = new Size(528, 140);
        this.txtDetalle.TabIndex = 3;
        this.txtDetalle.Visible = false;
        // 
        // btnCopiar
        // 
        this.btnCopiar.Enabled = false;
        this.btnCopiar.Location = new Point(16, 426);
        this.btnCopiar.Name = "btnCopiar";
        this.btnCopiar.Size = new Size(120, 30);
        this.btnCopiar.TabIndex = 4;
        this.btnCopiar.Text = "Copiar detalle";
        this.btnCopiar.UseVisualStyleBackColor = true;
        this.btnCopiar.Visible = false;
        this.btnCopiar.Click += new System.EventHandler(this.alCopiarDetalle);
        // 
        // btnCerrar
        // 
        this.btnCerrar.Enabled = false;
        this.btnCerrar.Location = new Point(424, 426);
        this.btnCerrar.Name = "btnCerrar";
        this.btnCerrar.Size = new Size(120, 30);
        this.btnCerrar.TabIndex = 5;
        this.btnCerrar.Text = "Cerrar";
        this.btnCerrar.UseVisualStyleBackColor = true;
        this.btnCerrar.Visible = false;
        this.btnCerrar.Click += new System.EventHandler(this.alCerrar);
        // 
        // frmProgresoOperacion
        // 
        this.AutoScaleDimensions = new SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(560, 470);
        this.ControlBox = false;
        this.Controls.Add(this.btnCerrar);
        this.Controls.Add(this.btnCopiar);
        this.Controls.Add(this.txtDetalle);
        this.Controls.Add(this.lvwPasos);
        this.Controls.Add(this.pbMarquee);
        this.Controls.Add(this.lblTexto);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "frmProgresoOperacion";
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Operación en curso";
        this.Shown += new System.EventHandler(this.alMostrar);
        this.FormClosing += new FormClosingEventHandler(this.alIntentarCerrar);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
