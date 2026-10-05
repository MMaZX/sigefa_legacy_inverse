using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIGEFA.Administradores.VentaCierre;

// Diálogo modal tipo lista de tareas para el cierre de una venta en la ruta nueva.
// Ejecuta la orden en un hilo de fondo y reporta el avance paso por paso en la
// interfaz. No se puede cerrar hasta que la orden termina (éxito o error).
// Sin rediseño estético del resto del sistema.
namespace SIGEFA.Formularios;

// Ventana de progreso del cierre. Quien la usa crea el servicio y los bloques,
// la muestra con ShowDialog y al volver lee resultados y fueExitoso.
public partial class frmVentaCierreProgreso : Form
{
    // Servicio que ejecuta la orden (se recibe por constructor simple, sin contenedor).
    private readonly VentaCierreService servicio;

    // Bloques de la orden (un bloque = un almacén), en el orden a ejecutar.
    private readonly IList<VentaCierreDatosBloque> bloques;

    // Pasos en orden de ejecución, para pintar la lista de tareas.
    private readonly VentaCierrePaso[] pasosOrdenados;

    // Indica si la orden ya terminó (éxito o error). Mientras sea falso el
    // diálogo no se puede cerrar (cubre X, Alt+F4 y Close()).
    private bool terminado;

    // Último bloque reportado, para reiniciar la lista al cambiar de bloque.
    private int ultimoBloqueVisto;

    // Detalle del error tal cual se muestra y se copia al portapapeles.
    private string detalleError;

    // Resultados de los bloques confirmados (vacío si la orden falló).
    private IList<VentaCierreResultado> resultadosObtenidos;

    // Crea el diálogo. servicio y bloques son obligatorios.
    public frmVentaCierreProgreso(VentaCierreService servicio, IList<VentaCierreDatosBloque> bloques)
    {
        if (servicio == null)
        {
            throw new ArgumentNullException("servicio");
        }
        if (bloques == null || bloques.Count == 0)
        {
            throw new ArgumentException("La orden debe contener al menos un bloque de venta.", "bloques");
        }
        this.servicio = servicio;
        this.bloques = bloques;
        this.pasosOrdenados = new VentaCierrePaso[]
        {
            VentaCierrePaso.abrirTransaccion,
            VentaCierrePaso.bloquearSerie,
            VentaCierrePaso.bloquearStock,
            VentaCierrePaso.guardarCabecera,
            VentaCierrePaso.guardarDetalle,
            VentaCierrePaso.guardarPago,
            VentaCierrePaso.confirmar
        };
        this.terminado = false;
        this.ultimoBloqueVisto = 0;
        this.detalleError = string.Empty;
        this.resultadosObtenidos = new List<VentaCierreResultado>();
        InitializeComponent();
        // La lista de pasos se arma una vez; el estado de cada fila cambia con el avance.
        inicializarListaPasos();
        btnCerrar.Enabled = false;
        btnCopiar.Enabled = false;
        txtError.Visible = false;
    }

    // Resultados de la orden (un resultado por bloque confirmado).
    public IList<VentaCierreResultado> resultados
    {
        get { return resultadosObtenidos; }
    }

    // Indica si la orden terminó sin errores.
    public bool fueExitoso { get; private set; }

    // Arranca la orden al mostrarse el diálogo. Se ejecuta en un hilo de fondo
    // para no congelar la interfaz; el progreso se reporta con Progress<T>
    // creado en el hilo de UI para que las actualizaciones vuelvan solas a él.
    private async void alMostrar(object sender, EventArgs e)
    {
        Progress<VentaCierreProgreso> progreso = new Progress<VentaCierreProgreso>(alRecibirProgreso);
        try
        {
            IList<VentaCierreResultado> salida = await Task.Run(() => servicio.ejecutarOrden(bloques, progreso));
            resultadosObtenidos = salida;
            alTerminarConExito();
        }
        catch (Exception ex)
        {
            alTerminarConError(ex);
        }
    }

    // Recibe cada avance en el hilo de UI: actualiza el encabezado del bloque,
    // el estado de los pasos y la barra por ítem.
    private void alRecibirProgreso(VentaCierreProgreso avance)
    {
        if (avance == null)
        {
            return;
        }
        // Al cambiar de bloque se reinicia la lista de pasos para el nuevo almacén.
        if (avance.bloqueActual != ultimoBloqueVisto)
        {
            ultimoBloqueVisto = avance.bloqueActual;
            marcarTodosPasos("Pendiente");
        }
        lblEncabezado.Text = "Bloque " + avance.bloqueActual + "/" + avance.totalBloques
            + " (" + avance.almacenNombre + ")";
        lblAvance.Text = avance.mensaje;
        marcarPasosHasta(avance.paso);
        // La barra solo tiene sentido cuando el paso informa ítems. Se baja
        // Value a cero antes de cambiar Maximum porque WinForms no permite
        // un Value mayor que Maximum (pasa al cambiar de bloque con menos ítems).
        if (avance.totalItems > 0)
        {
            pbItems.Value = 0;
            pbItems.Maximum = avance.totalItems;
            pbItems.Value = Math.Min(Math.Max(avance.itemActual, 0), avance.totalItems);
            lblItems.Text = "Ítem " + avance.itemActual + " de " + avance.totalItems;
        }
        else
        {
            pbItems.Value = 0;
            lblItems.Text = string.Empty;
        }
    }

    // Marca como listos los pasos anteriores al actual y el actual como en curso.
    private void marcarPasosHasta(VentaCierrePaso pasoActual)
    {
        for (int i = 0; i < pasosOrdenados.Length; i++)
        {
            string estado;
            if ((int)pasosOrdenados[i] < (int)pasoActual)
            {
                estado = "Listo";
            }
            else if (pasosOrdenados[i] == pasoActual)
            {
                estado = "En curso";
            }
            else
            {
                estado = "Pendiente";
            }
            lvwPasos.Items[i].SubItems[1].Text = estado;
        }
    }

    // Pone todas las filas de la lista en el estado indicado.
    private void marcarTodosPasos(string estado)
    {
        foreach (ListViewItem fila in lvwPasos.Items)
        {
            fila.SubItems[1].Text = estado;
        }
    }

    // Crea una fila por paso con su nombre legible y estado inicial pendiente.
    private void inicializarListaPasos()
    {
        lvwPasos.Items.Clear();
        foreach (VentaCierrePaso paso in pasosOrdenados)
        {
            ListViewItem fila = new ListViewItem(VentaCierrePasoTexto.obtenerNombre(paso));
            fila.SubItems.Add("Pendiente");
            lvwPasos.Items.Add(fila);
        }
    }

    // Cierra la orden con éxito: todos los pasos quedan listos y se habilita Cerrar.
    private void alTerminarConExito()
    {
        fueExitoso = true;
        terminado = true;
        marcarTodosPasos("Listo");
        pbItems.Value = pbItems.Maximum > 0 ? pbItems.Maximum : 0;
        lblEncabezado.Text = "Venta cerrada correctamente";
        lblAvance.Text = "La orden terminó sin errores.";
        lblItems.Text = string.Empty;
        btnCerrar.Enabled = true;
        btnCerrar.Focus();
    }

    // Cierra la orden con error: muestra tal cual paso, procedimiento, ítem,
    // número y mensaje de MySQL, y habilita copiar el detalle y cerrar.
    private void alTerminarConError(Exception ex)
    {
        fueExitoso = false;
        terminado = true;
        VentaCierreException errorCierre = ex as VentaCierreException;
        if (errorCierre != null)
        {
            marcarPasoConError(errorCierre.paso);
            detalleError = "Paso: " + VentaCierrePasoTexto.obtenerNombre(errorCierre.paso) + "\r\n"
                + "Procedimiento: " + errorCierre.procedimiento + "\r\n"
                + "Ítem: " + (errorCierre.itemIndice.HasValue ? errorCierre.itemIndice.Value.ToString() : "-") + "\r\n"
                + "Producto: " + (errorCierre.productoId.HasValue ? errorCierre.productoId.Value.ToString() : "-") + "\r\n"
                + "Número MySQL: " + errorCierre.mysqlNumero + "\r\n"
                + "SqlState: " + errorCierre.sqlState + "\r\n"
                + "Mensaje MySQL: " + errorCierre.mysqlMensaje + "\r\n"
                + "Parámetros: " + errorCierre.parametros;
            lblEncabezado.Text = "Error al cerrar la venta";
            lblAvance.Text = errorCierre.Message;
        }
        else
        {
            // Error fuera del flujo esperado: se muestra el mensaje sin traducir.
            detalleError = ex != null ? ex.ToString() : "Error desconocido.";
            lblEncabezado.Text = "Error al cerrar la venta";
            lblAvance.Text = ex != null ? ex.Message : "Error desconocido.";
        }
        txtError.Text = detalleError;
        txtError.Visible = true;
        btnCopiar.Enabled = true;
        btnCerrar.Enabled = true;
        btnCerrar.Focus();
    }

    // Marca la fila del paso que falló como error (las anteriores ya quedaron listas).
    private void marcarPasoConError(VentaCierrePaso paso)
    {
        for (int i = 0; i < pasosOrdenados.Length; i++)
        {
            if (pasosOrdenados[i] == paso)
            {
                lvwPasos.Items[i].SubItems[1].Text = "Error";
                break;
            }
        }
    }

    // Copia el detalle del error tal cual al portapapeles.
    private void alCopiarDetalle(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(detalleError))
        {
            return;
        }
        try
        {
            Clipboard.SetText(detalleError);
        }
        catch
        {
            // Si el portapapeles está ocupado no se pierde nada: el detalle
            // sigue visible para copiarlo a mano.
        }
    }

    // Cierra el diálogo (solo llega aquí cuando ya terminó).
    private void alCerrarDialogo(object sender, EventArgs e)
    {
        Close();
    }

    // Impide cerrar el diálogo mientras la orden no terminó (cubre X, Alt+F4 y Close()).
    private void alIntentarCerrar(object sender, FormClosingEventArgs e)
    {
        if (!terminado)
        {
            e.Cancel = true;
        }
    }
}
