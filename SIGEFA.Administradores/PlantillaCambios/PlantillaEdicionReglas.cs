using System.Globalization;
using System.Linq;

namespace SIGEFA.Administradores.PlantillaCambios;

// Reglas de edicion del detalle de una plantilla: que columnas se guardan y como
// se relacionan el stock minimo y el maximo. Sin acceso a datos ni a WinForms.
public static class PlantillaEdicionReglas
{
    // Columnas editables que viajan a ActualizaDetalleProductosAgrupos al guardar.
    // Los checks de recuento se registran aparte, en el clic de la celda.
    private static readonly string[] ColumnasQueSeGuardan =
    {
        "colTxtStockMinimo",
        "colTxtStockMaximo",
        "colUndXPaquete",
        "unidadd",
    };

    // Editar esta columna debe avisar al tracker para que Guardar la persista.
    public static bool RegistraCambio(string columna)
    {
        return ColumnasQueSeGuardan.Contains(columna);
    }

    // Al editar el stock maximo, el minimo solo se completa (maximo / 2) si estaba vacio.
    // Devuelve null cuando el minimo debe quedar como esta.
    public static double? MinimoPorDefecto(string maximo, string minimoActual)
    {
        if (!string.IsNullOrWhiteSpace(minimoActual))
        {
            return null;
        }
        if (!double.TryParse(maximo, NumberStyles.Float, CultureInfo.CurrentCulture, out double valorMaximo))
        {
            return null;
        }
        return valorMaximo / 2.0;
    }
}
