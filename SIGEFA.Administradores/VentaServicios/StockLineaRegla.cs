using System;

namespace SIGEFA.Administradores.VentaServicios;

// Decide si una linea de venta se valida contra stock segun tipoarticulo.is_enabled_stock.
// Los servicios (is_enabled_stock = 0) se venden con cantidad libre; la mercaderia conserva
// su regla. Sin base de datos ni WinForms.
public static class StockLineaRegla
{
    // Texto que muestra la columna de stock para un articulo que no lleva stock.
    public const string TextoSinStock = "—";

    // Sin flag se asume que lleva stock, igual que IFNULL(is_enabled_stock, 1) en los SP.
    public static bool LlevaStock(object isEnabledStock)
    {
        if (isEnabledStock == null || isEnabledStock is DBNull)
        {
            return true;
        }
        return Convert.ToInt32(isEnabledStock) != 0;
    }

    public static bool StockCubreCantidad(bool llevaStock, decimal cantidad, decimal stockMaximo)
    {
        return !llevaStock || cantidad <= stockMaximo;
    }
}
