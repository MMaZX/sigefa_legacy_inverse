using System.Collections.Generic;
using System.Linq;
using SIGEFA.Entidades;

namespace SIGEFA.Administradores.PlantillaCambios;

// Lleva la cuenta de lo que cambió en el detalle de una plantilla ya guardada:
// qué filas se insertan, actualizan o eliminan al presionar Guardar.
// Es la única fuente de esas tres listas; los handlers del formulario solo
// avisan aquí. Sin acceso a datos ni a WinForms.
public class PlantillaDetalleCambios
{
    private readonly List<clsDetallePlantillaDeProductos> insertar = new();
    private readonly List<clsDetallePlantillaDeProductos> actualizar = new();
    private readonly List<clsDetallePlantillaDeProductos> eliminar = new();

    public IReadOnlyList<clsDetallePlantillaDeProductos> Insertar => insertar;

    public IReadOnlyList<clsDetallePlantillaDeProductos> Actualizar => actualizar;

    public IReadOnlyList<clsDetallePlantillaDeProductos> Eliminar => eliminar;

    // Hay algo sin guardar: el formulario lo usa para avisar al salir.
    public bool HayCambios => insertar.Count + actualizar.Count + eliminar.Count > 0;

    // Un producto entra a la plantilla.
    public void Agregar(clsDetallePlantillaDeProductos detalle)
    {
        var eliminado = Buscar(eliminar, detalle);
        if (eliminado != null)
        {
            // Estaba guardado y se quitó: se conserva su fila y solo se actualiza.
            eliminar.Remove(eliminado);
            detalle.Codigo = eliminado.Codigo;
            actualizar.Add(detalle);
            return;
        }
        if (Buscar(insertar, detalle) != null)
        {
            return;
        }
        insertar.Add(detalle);
    }

    // Un producto sale de la plantilla.
    public void Quitar(clsDetallePlantillaDeProductos detalle)
    {
        var nuevo = Buscar(insertar, detalle);
        if (nuevo != null)
        {
            // Nunca se guardó: no hay nada que borrar en la base.
            insertar.Remove(nuevo);
            return;
        }
        Quitar(actualizar, detalle);
        eliminar.Add(detalle);
    }

    // Cambió un dato de una fila (cantidad, unidad, stock mínimo o máximo).
    public void Modificar(clsDetallePlantillaDeProductos detalle)
    {
        if (Reemplazar(insertar, detalle, conservarCodigo: false))
        {
            return;
        }
        if (Reemplazar(actualizar, detalle, conservarCodigo: true))
        {
            return;
        }
        actualizar.Add(detalle);
    }

    // Se guardó con éxito: no queda nada pendiente.
    public void Limpiar()
    {
        insertar.Clear();
        actualizar.Clear();
        eliminar.Clear();
    }

    private static clsDetallePlantillaDeProductos Buscar(List<clsDetallePlantillaDeProductos> lista, clsDetallePlantillaDeProductos detalle)
    {
        return lista.FirstOrDefault(x => x.Codigo_Producto == detalle.Codigo_Producto);
    }

    private static void Quitar(List<clsDetallePlantillaDeProductos> lista, clsDetallePlantillaDeProductos detalle)
    {
        var existente = Buscar(lista, detalle);
        if (existente != null)
        {
            lista.Remove(existente);
        }
    }

    private static bool Reemplazar(List<clsDetallePlantillaDeProductos> lista, clsDetallePlantillaDeProductos detalle, bool conservarCodigo)
    {
        var existente = Buscar(lista, detalle);
        if (existente == null)
        {
            return false;
        }
        if (conservarCodigo)
        {
            detalle.Codigo = existente.Codigo;
        }
        lista[lista.IndexOf(existente)] = detalle;
        return true;
    }
}
