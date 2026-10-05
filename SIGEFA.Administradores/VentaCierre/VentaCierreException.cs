using System;

// Error exacto del cierre de una venta en la ruta nueva.
// Guarda el paso, el procedimiento, el ítem y los datos originales de MySQL
// (Number, SqlState y mensaje) para mostrarlos tal cual en el diálogo,
// y conserva la causa como InnerException. Sin lógica de negocio ni acceso a datos.
namespace SIGEFA.Administradores.VentaCierre;

// Excepción con el contexto completo del fallo. itemIndice y productoId son
// nulos cuando el error ocurre fuera del detalle (por ejemplo al bloquear
// la serie). parametros registra los valores enviados, sin secretos.
public class VentaCierreException : Exception
{
    // Crea la excepción y arma el mensaje en español con los datos exactos
    // de MySQL. causa conserva la excepción original (MySqlException).
    public VentaCierreException(
        VentaCierrePaso paso,
        string procedimiento,
        int mysqlNumero,
        string sqlState,
        string mysqlMensaje,
        Exception causa,
        int? itemIndice,
        int? productoId,
        string parametros)
        : base(construirMensaje(paso, procedimiento, itemIndice, productoId, mysqlNumero, sqlState, mysqlMensaje), causa)
    {
        this.paso = paso;
        this.procedimiento = procedimiento ?? string.Empty;
        this.itemIndice = itemIndice;
        this.productoId = productoId;
        this.mysqlNumero = mysqlNumero;
        this.sqlState = sqlState ?? string.Empty;
        this.mysqlMensaje = mysqlMensaje ?? string.Empty;
        this.parametros = parametros ?? string.Empty;
    }

    // Paso del cierre donde ocurrió el error.
    public VentaCierrePaso paso { get; }

    // Procedimiento almacenado que falló (o sentencia de bloqueo).
    public string procedimiento { get; }

    // Posición del ítem dentro del detalle (base 1); nulo si no aplica.
    public int? itemIndice { get; }

    // Producto del ítem que falló; nulo si el error no es de un ítem.
    public int? productoId { get; }

    // Número de error de MySQL (MySqlException.Number), sin traducir.
    public int mysqlNumero { get; }

    // Código SQLSTATE de MySQL, sin traducir.
    public string sqlState { get; }

    // Mensaje original de MySQL, sin traducir.
    public string mysqlMensaje { get; }

    // Parámetros enviados al procedimiento, sin secretos (sin claves ni tokens).
    public string parametros { get; }

    // Arma el mensaje con paso, procedimiento, ítem y datos exactos de MySQL
    // para que el diálogo los muestre tal cual y se puedan copiar.
    private static string construirMensaje(
        VentaCierrePaso paso,
        string procedimiento,
        int? itemIndice,
        int? productoId,
        int mysqlNumero,
        string sqlState,
        string mysqlMensaje)
    {
        string detalleItem = itemIndice.HasValue
            ? " ítem " + itemIndice.Value.ToString()
                + (productoId.HasValue ? " (productoId " + productoId.Value.ToString() + ")" : string.Empty)
            : string.Empty;
        return "Error en el paso " + VentaCierrePasoTexto.obtenerNombre(paso)
            + " (" + procedimiento + ")" + detalleItem
            + ": MySQL " + mysqlNumero.ToString()
            + " [" + sqlState + "] " + mysqlMensaje;
    }
}
