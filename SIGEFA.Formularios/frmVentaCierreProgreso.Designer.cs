using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// Parte generada del diálogo de progreso del cierre de venta: creación y
// posición de los controles. La lógica vive en frmVentaCierreProgreso.cs.
// No se asigna CancelButton a propósito: el diálogo solo se cierra con el
// botón Cerrar cuando la orden ya terminó.
namespace SIGEFA.Formularios;

public partial class frmVentaCierreProgreso
{
    // Contenedor de componentes para liberar recursos al cerrar.
    private IContainer components = null;

    // Encabezado con el bloque actual: "Bloque k/n (almacén)".
    private Label lblEncabezado;

    // Lista de pasos con su estado (pendiente, en curso, listo, error).
    private ListView lvwPasos;

    // Encabezado de la columna de pasos.
    private ColumnHeader colPaso;

    // Encabezado de la columna de estados.
    private ColumnHeader colEstado;

    // Mensaje del avance actual que reporta el servicio.
    private Label lblAvance;

    // Texto del avance por ítem ("Ítem x de y").
    private Label lblItems;

    // Barra del avance por ítem.
    private ProgressBar pbItems;

    // Detalle del error con los datos exactos de MySQL (visible solo al error).
    private TextBox txtError;

    // Copia el detalle del error al portapapeles (habilitado solo al error).
    private Button btnCopiar;

    // Cierra el diálogo (habilitado solo al terminar, éxito o error).
    private Button btnCerrar;

    // Libera los componentes del diálogo.
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    // Crea y ubica los controles del diálogo.
    private void InitializeComponent()
    {
        this.lblEncabezado = new Label();
        this.lvwPasos = new ListView();
        this.colPaso = new ColumnHeader();
        this.colEstado = new ColumnHeader();
        this.lblAvance = new Label();
        this.lblItems = new Label();
        this.pbItems = new ProgressBar();
        this.txtError = new TextBox();
        this.btnCopiar = new Button();
        this.btnCerrar = new Button();
        this.SuspendLayout();
        //
        // lblEncabezado
        //
        this.lblEncabezado.Location = new Point(12, 9);
        this.lblEncabezado.Name = "lblEncabezado";
        this.lblEncabezado.Size = new Size(456, 20);
        this.lblEncabezado.TabIndex = 0;
        this.lblEncabezado.Text = "Cerrando venta...";
        //
        // lvwPasos
        //
        this.lvwPasos.Columns.AddRange(new ColumnHeader[] {
        this.colPaso,
        this.colEstado});
        this.lvwPasos.FullRowSelect = true;
        this.lvwPasos.GridLines = true;
        this.lvwPasos.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        this.lvwPasos.HideSelection = false;
        this.lvwPasos.Location = new Point(12, 32);
        this.lvwPasos.MultiSelect = false;
        this.lvwPasos.Name = "lvwPasos";
        this.lvwPasos.Size = new Size(456, 163);
        this.lvwPasos.TabIndex = 1;
        this.lvwPasos.UseCompatibleStateImageBehavior = false;
        this.lvwPasos.View = View.Details;
        //
        // colPaso
        //
        this.colPaso.Text = "Paso";
        this.colPaso.Width = 300;
        //
        // colEstado
        //
        this.colEstado.Text = "Estado";
        this.colEstado.Width = 150;
        //
        // lblAvance
        //
        this.lblAvance.Location = new Point(12, 202);
        this.lblAvance.Name = "lblAvance";
        this.lblAvance.Size = new Size(456, 20);
        this.lblAvance.TabIndex = 2;
        this.lblAvance.Text = string.Empty;
        //
        // lblItems
        //
        this.lblItems.Location = new Point(12, 224);
        this.lblItems.Name = "lblItems";
        this.lblItems.Size = new Size(456, 20);
        this.lblItems.TabIndex = 3;
        this.lblItems.Text = string.Empty;
        //
        // pbItems
        //
        this.pbItems.Location = new Point(12, 247);
        this.pbItems.Name = "pbItems";
        this.pbItems.Size = new Size(456, 23);
        this.pbItems.TabIndex = 4;
        //
        // txtError
        //
        this.txtError.Location = new Point(12, 276);
        this.txtError.Multiline = true;
        this.txtError.Name = "txtError";
        this.txtError.ReadOnly = true;
        this.txtError.ScrollBars = ScrollBars.Vertical;
        this.txtError.Size = new Size(456, 96);
        this.txtError.TabIndex = 5;
        this.txtError.Visible = false;
        //
        // btnCopiar
        //
        this.btnCopiar.Enabled = false;
        this.btnCopiar.Location = new Point(12, 378);
        this.btnCopiar.Name = "btnCopiar";
        this.btnCopiar.Size = new Size(120, 30);
        this.btnCopiar.TabIndex = 6;
        this.btnCopiar.Text = "Copiar detalle";
        this.btnCopiar.UseVisualStyleBackColor = true;
        this.btnCopiar.Click += new System.EventHandler(this.alCopiarDetalle);
        //
        // btnCerrar
        //
        this.btnCerrar.Enabled = false;
        this.btnCerrar.Location = new Point(348, 378);
        this.btnCerrar.Name = "btnCerrar";
        this.btnCerrar.Size = new Size(120, 30);
        this.btnCerrar.TabIndex = 7;
        this.btnCerrar.Text = "Cerrar";
        this.btnCerrar.UseVisualStyleBackColor = true;
        this.btnCerrar.Click += new System.EventHandler(this.alCerrarDialogo);
        //
        // frmVentaCierreProgreso
        //
        this.AutoScaleDimensions = new SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(480, 420);
        this.ControlBox = false;
        this.Controls.Add(this.btnCerrar);
        this.Controls.Add(this.btnCopiar);
        this.Controls.Add(this.txtError);
        this.Controls.Add(this.pbItems);
        this.Controls.Add(this.lblItems);
        this.Controls.Add(this.lblAvance);
        this.Controls.Add(this.lvwPasos);
        this.Controls.Add(this.lblEncabezado);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "frmVentaCierreProgreso";
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Cierre de venta";
        this.Shown += new System.EventHandler(this.alMostrar);
        this.FormClosing += new FormClosingEventHandler(this.alIntentarCerrar);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
