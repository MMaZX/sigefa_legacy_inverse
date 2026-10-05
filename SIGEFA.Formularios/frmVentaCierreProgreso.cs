using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIGEFA.Administradores.VentaCierre;
using SIGEFA.Entidades;

// Diálogo modal tipo lista de tareas para el cierre de una venta en la ruta nueva.
// Ejecuta la orden transaccional en segundo plano y luego las acciones posteriores
// en el hilo de interfaz de usuario.
// No se puede cerrar hasta que todos los procesos hayan finalizado.
namespace SIGEFA.Formularios;

public partial class frmVentaCierreProgreso : Form
{
    // Servicio que ejecuta la orden transaccional.
    private readonly VentaCierreService servicio;

    // Bloques de la orden (un bloque = un almacén), en el orden a ejecutar.
    private readonly IList<VentaCierreDatosBloque> bloques;

    // Acciones posteriores al guardado en base de datos (nombre + delegado asincrónico).
    private readonly IList<VentaCierrePostAccion> accionesPostCierre;

    // Pasos transaccionales en orden de ejecución.
    private readonly VentaCierrePaso[] pasosOrdenados;

    // Pasos posteriores al guardado en orden de ejecución.
    private readonly VentaCierrePostPaso[] postPasosOrdenados;

    // Acumulador de todos los errores ocurridos en las etapas posteriores.
    private readonly List<ErrorPaso> erroresPostCierre;

    // Color ámbar para resaltar pasos con advertencias en la interfaz.
    private static readonly Color ColorAmbar = Color.FromArgb(204, 102, 0);

    // Indica si todos los procesos terminaron (éxito, advertencia o error).
    private bool terminado;

    // Último bloque reportado, para reiniciar la lista transaccional al cambiar de almacén.
    private int ultimoBloqueVisto;

    // Detalle completo del error o advertencias para mostrar y copiar al portapapeles.
    private string detalleError;

    // Resultados de los bloques confirmados (vacío si la transacción falló).
    private IList<VentaCierreResultado> resultadosObtenidos;

    // Constructor que recibe servicio y bloques de venta (sin acciones posteriores).
    public frmVentaCierreProgreso(VentaCierreService servicio, IList<VentaCierreDatosBloque> bloques)
        : this(servicio, bloques, null)
    {
    }

    // Constructor que recibe servicio, bloques y la lista de acciones posteriores al guardado.
    public frmVentaCierreProgreso(
        VentaCierreService servicio,
        IList<VentaCierreDatosBloque> bloques,
        IList<VentaCierrePostAccion> accionesPostCierre)
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
        this.accionesPostCierre = accionesPostCierre != null
            ? new List<VentaCierrePostAccion>(accionesPostCierre)
            : new List<VentaCierrePostAccion>();

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

        this.postPasosOrdenados = new VentaCierrePostPaso[]
        {
            VentaCierrePostPaso.crearDespacho,
            VentaCierrePostPaso.guardarCodigoBarras,
            VentaCierrePostPaso.generarComprobanteElectronico,
            VentaCierrePostPaso.imprimirComprobante
        };

        this.erroresPostCierre = new List<ErrorPaso>();
        this.terminado = false;
        this.ultimoBloqueVisto = 0;
        this.detalleError = string.Empty;
        this.resultadosObtenidos = new List<VentaCierreResultado>();

        InitializeComponent();
        inicializarListaPasos();
        btnCerrar.Enabled = false;
        btnCopiar.Enabled = false;
        txtError.Visible = false;
    }

    // Resultados de la orden (un resultado por cada bloque confirmado).
    public IList<VentaCierreResultado> resultados
    {
        get { return resultadosObtenidos; }
    }

    // Indica si la venta fue confirmada en base de datos. Las advertencias posteriores no alteran este valor.
    public bool fueExitoso { get; private set; }

    // Lista de incidencias acumuladas en los pasos posteriores.
    public IList<ErrorPaso> erroresPostCierreObtenidos
    {
        get { return erroresPostCierre.AsReadOnly(); }
    }

    // Permite registrar una acción posterior adicional antes de mostrar el diálogo.
    public void registrarAccionPostCierre(VentaCierrePostAccion accion)
    {
        if (accion != null)
        {
            this.accionesPostCierre.Add(accion);
        }
    }

    // Inicia la ejecución al mostrar el diálogo. La etapa transaccional corre en segundo plano;
    // luego, las acciones posteriores se ejecutan en el hilo de UI.
    private async void alMostrar(object sender, EventArgs e)
    {
        Progress<VentaCierreProgreso> progreso = new Progress<VentaCierreProgreso>(alRecibirProgreso);
        try
        {
            IList<VentaCierreResultado> salida = await Task.Run(() => servicio.ejecutarOrden(bloques, progreso));
            resultadosObtenidos = salida;
            fueExitoso = true;
            marcarPasosTransaccionales("Listo");

            if (pbItems.Maximum > 0)
            {
                pbItems.Value = pbItems.Maximum;
            }
            lblItems.Text = string.Empty;

            // Ejecuta los pasos posteriores en el hilo de interfaz sin bloquear la ventana
            await ejecutarAccionesPostCierre();

            alTerminarTodo();
        }
        catch (Exception ex)
        {
            alTerminarConError(ex);
        }
    }

    // Recibe el avance de la etapa transaccional en el hilo de UI.
    private void alRecibirProgreso(VentaCierreProgreso avance)
    {
        if (avance == null)
        {
            return;
        }

        if (avance.bloqueActual != ultimoBloqueVisto)
        {
            ultimoBloqueVisto = avance.bloqueActual;
            marcarPasosTransaccionales("Pendiente");
        }

        lblEncabezado.Text = "Bloque " + avance.bloqueActual + "/" + avance.totalBloques
            + " (" + avance.almacenNombre + ")";
        lblAvance.Text = avance.mensaje;
        marcarPasosHasta(avance.paso);

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

    // Ejecuta las 4 acciones posteriores en el hilo de UI de forma secuencial.
    // Un paso con incidencias queda en Advertencia y NUNCA interrumpe los pasos siguientes ni anula la venta.
    private async Task ejecutarAccionesPostCierre()
    {
        lblEncabezado.Text = "Procesando tareas posteriores al guardado...";

        for (int i = 0; i < postPasosOrdenados.Length; i++)
        {
            VentaCierrePostPaso paso = postPasosOrdenados[i];
            int filaIndice = pasosOrdenados.Length + i;
            ListViewItem fila = lvwPasos.Items[filaIndice];

            VentaCierrePostAccion accion = buscarAccionParaPaso(paso, i);
            if (accion == null || accion.ejecutar == null)
            {
                actualizarFilaPost(fila, "Omitido", null, Color.Gray);
                continue;
            }

            actualizarFilaPost(fila, "En curso", null, Color.Blue);
            lblAvance.Text = accion.nombre + "...";

            VentaCierrePostEjecucion ejecucion = new VentaCierrePostEjecucion(paso);
            inyectarContextoNegocio(ejecucion);

            try
            {
                await accion.ejecutar(ejecucion);
            }
            catch (Exception ex)
            {
                ejecucion.agregarError(ex);
            }

            if (ejecucion.omitido)
            {
                string detalle = !string.IsNullOrEmpty(ejecucion.detalleOmitido)
                    ? ejecucion.detalleOmitido
                    : "sin requerimiento";
                actualizarFilaPost(fila, "Omitido", detalle, Color.Gray);
            }
            else if (ejecucion.errores != null && ejecucion.errores.Count > 0)
            {
                // Registrar en log local y acumular para el botón Copiar detalle
                VentaCierreRegistroErrores.registrarErroresPaso(ejecucion.errores);
                erroresPostCierre.AddRange(ejecucion.errores);

                string primerErrorResumido = resumirPrimerError(ejecucion.errores[0]);
                actualizarFilaPost(fila, "Advertencia", primerErrorResumido, ColorAmbar);
            }
            else
            {
                actualizarFilaPost(fila, "Listo", null, Color.DarkGreen);
            }
        }
    }

    // Busca la acción asociada al paso posterior por enum o por índice.
    private VentaCierrePostAccion buscarAccionParaPaso(VentaCierrePostPaso paso, int indice)
    {
        if (accionesPostCierre == null || accionesPostCierre.Count == 0)
        {
            return null;
        }

        foreach (VentaCierrePostAccion accion in accionesPostCierre)
        {
            if (accion != null && accion.paso == paso)
            {
                return accion;
            }
        }

        if (indice < accionesPostCierre.Count && accionesPostCierre[indice] != null)
        {
            return accionesPostCierre[indice];
        }

        return null;
    }

    // Asocia datos de negocio conocidos al contexto del paso posterior.
    private void inyectarContextoNegocio(VentaCierrePostEjecucion ejecucion)
    {
        if (resultadosObtenidos != null && resultadosObtenidos.Count > 0)
        {
            VentaCierreResultado primerRes = resultadosObtenidos[0];
            ejecucion.facturaId = primerRes.facturaVentaId.ToString();
            ejecucion.serieNumero = primerRes.numeroDocumento;
            ejecucion.almacen = primerRes.almacenNombre;
        }

        if (bloques != null && bloques.Count > 0 && bloques[0].venta != null)
        {
            clsFacturaVenta v = bloques[0].venta;
            if (string.IsNullOrEmpty(ejecucion.pedido) && v.CodPedido > 0)
            {
                ejecucion.pedido = v.CodPedido.ToString();
            }
            if (string.IsNullOrEmpty(ejecucion.usuario) && v.CodUser > 0)
            {
                ejecucion.usuario = v.CodUser.ToString();
            }
        }
    }

    // Actualiza el estado visual, color y texto de detalle de una fila posterior.
    private void actualizarFilaPost(ListViewItem fila, string estado, string detalle, Color color)
    {
        fila.UseItemStyleForSubItems = false;
        fila.SubItems[1].Text = estado;
        fila.SubItems[1].ForeColor = color;
        fila.SubItems[2].Text = detalle ?? string.Empty;

        if (estado == "Advertencia")
        {
            fila.ForeColor = color;
            fila.SubItems[2].ForeColor = color;
        }
        else
        {
            fila.ForeColor = SystemColors.WindowText;
            fila.SubItems[2].ForeColor = SystemColors.WindowText;
        }
    }

    // Obtiene una síntesis en una sola línea del primer error de un paso.
    private static string resumirPrimerError(ErrorPaso error)
    {
        if (error == null)
        {
            return "Error";
        }

        string texto = !string.IsNullOrEmpty(error.mensaje) ? error.mensaje : error.tipoExcepcion;
        if (string.IsNullOrEmpty(texto))
        {
            return "Error desconocido";
        }

        texto = texto.Replace("\r", " ").Replace("\n", " ").Trim();
        if (texto.Length > 80)
        {
            texto = texto.Substring(0, 77) + "...";
        }

        return texto;
    }

    // Marca como listos los pasos anteriores al actual y el actual como en curso.
    private void marcarPasosHasta(VentaCierrePaso pasoActual)
    {
        for (int i = 0; i < pasosOrdenados.Length; i++)
        {
            string estado;
            Color color;
            if ((int)pasosOrdenados[i] < (int)pasoActual)
            {
                estado = "Listo";
                color = Color.DarkGreen;
            }
            else if (pasosOrdenados[i] == pasoActual)
            {
                estado = "En curso";
                color = Color.Blue;
            }
            else
            {
                estado = "Pendiente";
                color = SystemColors.WindowText;
            }

            lvwPasos.Items[i].UseItemStyleForSubItems = false;
            lvwPasos.Items[i].SubItems[1].Text = estado;
            lvwPasos.Items[i].SubItems[1].ForeColor = color;
        }
    }

    // Actualiza todos los pasos transaccionales al estado indicado.
    private void marcarPasosTransaccionales(string estado)
    {
        Color color = estado == "Listo" ? Color.DarkGreen : SystemColors.WindowText;
        for (int i = 0; i < pasosOrdenados.Length; i++)
        {
            lvwPasos.Items[i].UseItemStyleForSubItems = false;
            lvwPasos.Items[i].SubItems[1].Text = estado;
            lvwPasos.Items[i].SubItems[1].ForeColor = color;
        }
    }

    // Configura la lista dividida en dos secciones: Guardar en base de datos y Después de guardar.
    private void inicializarListaPasos()
    {
        lvwPasos.Items.Clear();
        lvwPasos.Groups.Clear();

        ListViewGroup grpTransaccional = new ListViewGroup("grpTransaccional", "Guardar en base de datos");
        ListViewGroup grpPostCierre = new ListViewGroup("grpPostCierre", "Después de guardar");
        lvwPasos.Groups.Add(grpTransaccional);
        lvwPasos.Groups.Add(grpPostCierre);

        foreach (VentaCierrePaso paso in pasosOrdenados)
        {
            ListViewItem fila = new ListViewItem(VentaCierrePasoTexto.obtenerNombre(paso));
            fila.Group = grpTransaccional;
            fila.SubItems.Add("Pendiente");
            fila.SubItems.Add(string.Empty);
            lvwPasos.Items.Add(fila);
        }

        foreach (VentaCierrePostPaso paso in postPasosOrdenados)
        {
            ListViewItem fila = new ListViewItem(VentaCierrePostPasoTexto.obtenerNombre(paso));
            fila.Group = grpPostCierre;
            fila.SubItems.Add("Pendiente");
            fila.SubItems.Add(string.Empty);
            lvwPasos.Items.Add(fila);
        }
    }

    // Finaliza todo el ciclo de cierre. Habilita Cerrar y, si hubo advertencias, Copiar detalle.
    private void alTerminarTodo()
    {
        terminado = true;
        btnCerrar.Enabled = true;

        if (erroresPostCierre.Count > 0)
        {
            lblEncabezado.Text = "Venta guardada con advertencias";
            lblAvance.Text = "La venta se guardó correctamente, pero hubo advertencias en los pasos posteriores.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== ADVERTENCIAS EN PASOS POSTERIORES AL GUARDADO ===");
            sb.AppendLine("La venta fue guardada y confirmada en la base de datos.");
            sb.AppendLine("Total de advertencias: " + erroresPostCierre.Count);
            sb.AppendLine();

            for (int i = 0; i < erroresPostCierre.Count; i++)
            {
                sb.AppendLine("--------------------------------------------------");
                sb.AppendLine(string.Format("Incidencia {0} de {1}:", i + 1, erroresPostCierre.Count));
                sb.Append(erroresPostCierre[i].obtenerDetalleFormateado());
            }

            detalleError = sb.ToString();
            txtError.Text = detalleError;
            txtError.Visible = true;
            btnCopiar.Enabled = true;
        }
        else
        {
            lblEncabezado.Text = "Venta cerrada correctamente";
            lblAvance.Text = "La orden terminó sin errores.";
            txtError.Visible = false;
            btnCopiar.Enabled = false;
        }

        btnCerrar.Focus();
    }

    // Cierra la orden ante un fallo en la etapa transaccional. Omite pasos posteriores y habilita Cerrar y Copiar.
    private void alTerminarConError(Exception ex)
    {
        fueExitoso = false;
        terminado = true;

        for (int i = 0; i < postPasosOrdenados.Length; i++)
        {
            int filaIndice = pasosOrdenados.Length + i;
            actualizarFilaPost(lvwPasos.Items[filaIndice], "Omitido", "Cancelado por error previo", Color.Gray);
        }

        VentaCierreException errorCierre = ex as VentaCierreException;
        if (errorCierre != null)
        {
            marcarPasoTransaccionalConError(errorCierre.paso);
            detalleError = "Paso: " + VentaCierrePasoTexto.obtenerNombre(errorCierre.paso) + "\r\n"
                + "Procedimiento: " + errorCierre.procedimiento + "\r\n"
                + "Ítem: " + (errorCierre.itemIndice.HasValue ? errorCierre.itemIndice.Value.ToString() : "-") + "\r\n"
                + "Producto: " + (errorCierre.productoId.HasValue ? errorCierre.productoId.Value.ToString() : "-") + "\r\n"
                + "Número MySQL: " + errorCierre.mysqlNumero + "\r\n"
                + "SqlState: " + errorCierre.sqlState + "\r\n"
                + "Mensaje MySQL: " + VentaCierreRegistroErrores.enmascararCredenciales(errorCierre.mysqlMensaje) + "\r\n"
                + "Parámetros: " + VentaCierreRegistroErrores.enmascararCredenciales(errorCierre.parametros);
            lblEncabezado.Text = "Error al cerrar la venta";
            lblAvance.Text = errorCierre.Message;
        }
        else
        {
            detalleError = ex != null ? VentaCierreRegistroErrores.enmascararCredenciales(ex.ToString()) : "Error desconocido.";
            lblEncabezado.Text = "Error al cerrar la venta";
            lblAvance.Text = ex != null ? ex.Message : "Error desconocido.";
        }

        txtError.Text = detalleError;
        txtError.Visible = true;
        btnCopiar.Enabled = true;
        btnCerrar.Enabled = true;
        btnCerrar.Focus();
    }

    // Marca como fallido el paso transaccional correspondiente.
    private void marcarPasoTransaccionalConError(VentaCierrePaso paso)
    {
        for (int i = 0; i < pasosOrdenados.Length; i++)
        {
            if (pasosOrdenados[i] == paso)
            {
                lvwPasos.Items[i].UseItemStyleForSubItems = false;
                lvwPasos.Items[i].SubItems[1].Text = "Error";
                lvwPasos.Items[i].SubItems[1].ForeColor = Color.Red;
                break;
            }
        }
    }

    // Copia al portapapeles el detalle completo de incidencias.
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
            // Si el portapapeles no está disponible, el texto permanece visible en pantalla
        }
    }

    // Cierra el diálogo una vez que todo el proceso ha finalizado.
    private void alCerrarDialogo(object sender, EventArgs e)
    {
        Close();
    }

    // Impide cerrar el formulario antes de que finalicen todas las etapas (cubre X, Alt+F4 y Close()).
    private void alIntentarCerrar(object sender, FormClosingEventArgs e)
    {
        if (!terminado)
        {
            e.Cancel = true;
        }
    }
}
