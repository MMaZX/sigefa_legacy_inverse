using System.Collections.Generic;
using System.Collections.ObjectModel;

// Resultado del cierre de una venta en la ruta nueva.
// DTO inmutable: el servicio lo construye al confirmar el último bloque.
// Sin lógica de negocio ni acceso a datos.
namespace SIGEFA.Administradores.VentaCierre;

// Datos de la venta cerrada que necesita quien llama (impresión, SUNAT,
// despacho). Las colecciones se copian en el constructor para que el
// resultado no cambie si quien llama modifica las listas originales.
public class VentaCierreResultado
{
    // Crea el resultado. pagoIds puede estar vacía (venta al crédito);
    // duracionPorPasoMs guarda los milisegundos medidos en cada paso.
    public VentaCierreResultado(
        int facturaVentaId,
        string numeroDocumento,
        IList<int> pagoIds,
        IDictionary<VentaCierrePaso, long> duracionPorPasoMs)
    {
        this.facturaVentaId = facturaVentaId;
        this.numeroDocumento = numeroDocumento ?? string.Empty;
        // Copia defensiva: el resultado queda congelado aunque cambie la lista original.
        this.pagoIds = new ReadOnlyCollection<int>(new List<int>(pagoIds ?? new List<int>()));
        // Copia defensiva de las duraciones medidas por el servicio.
        this.duracionPorPasoMs = new ReadOnlyDictionary<VentaCierrePaso, long>(
            new Dictionary<VentaCierrePaso, long>(duracionPorPasoMs ?? new Dictionary<VentaCierrePaso, long>()));
    }

    // Identificador de la factura creada (salida newid de GuardaFacturaVenta).
    public int facturaVentaId { get; }

    // Número de documento asignado por la serie (salida numeraDoc).
    public string numeroDocumento { get; }

    // Identificadores de los pagos creados (salida newid de GuardaPago).
    public IReadOnlyList<int> pagoIds { get; }

    // Duración medida de cada paso, en milisegundos (sirve para diagnosticar
    // qué etapa es lenta sin ocultar el error original).
    public IReadOnlyDictionary<VentaCierrePaso, long> duracionPorPasoMs { get; }
}
