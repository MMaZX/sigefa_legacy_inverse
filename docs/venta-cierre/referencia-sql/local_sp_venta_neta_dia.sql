*************************** 1. row ***************************
sp_venta_neta_dia
STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION
CREATE DEFINER=`root`@`%` PROCEDURE `sp_venta_neta_dia`(
    IN p_producto_id  INT,
    IN p_almacen_id   INT,
    IN p_fecha        DATE
)
BEGIN
    DECLARE v_bruto      DECIMAL(18,4) DEFAULT 0;
    DECLARE v_devolucion DECIMAL(18,4) DEFAULT 0;
    DECLARE v_neto       DECIMAL(18,4) DEFAULT 0;

    
    
    
    
    
    
    SELECT IFNULL(SUM(t.cantidad_final), 0) INTO v_bruto
    FROM (
        SELECT DISTINCT
            dfv.codDetalleFacturaV,
            IF(
                dfv.unidadingresada <> p2.codUnidadMedida AND ue.factor IS NOT NULL,
                dfv.cantidad * ue.factor,
                dfv.cantidad
            ) AS cantidad_final
        FROM detallefactura_venta dfv
        JOIN producto p2
            ON p2.codProducto = dfv.codProducto
        LEFT JOIN (
            SELECT codProducto, codUnidadMedida, MAX(factor) AS factor
            FROM unidadequivalente
            WHERE compra_venta = 2
            GROUP BY codProducto, codUnidadMedida
        ) ue
            ON ue.codProducto     = dfv.codProducto
           AND ue.codUnidadMedida = dfv.unidadingresada
        WHERE dfv.codProducto  = p_producto_id
          AND dfv.codAlmacen   = p_almacen_id
          AND dfv.estado       = 1
          AND dfv.anulado      = 0
          AND dfv.fecharegistro >= p_fecha                            
          AND dfv.fecharegistro <  p_fecha + INTERVAL 1 DAY          
    ) t;

    
    
    
    
    
    SELECT IFNULL(SUM(t_dev.cantidad_final), 0) INTO v_devolucion
    FROM (
        SELECT DISTINCT
            dnc.codDetalleNotaCredito,
            IF(
                dnc.unidadingresada <> p3.codUnidadMedida AND ue2.factor IS NOT NULL,
                dnc.cantidad * ue2.factor,
                dnc.cantidad
            ) AS cantidad_final
        FROM factura_venta fv1
        JOIN detallefactura_venta dfv2
            ON dfv2.codFacturaV   = fv1.codFacturaV
           AND dfv2.codProducto   = p_producto_id       
           AND dfv2.codAlmacen    = p_almacen_id
        JOIN notacredito nc
            ON nc.codNotaCredito  = fv1.codNotaCredito
        JOIN detallenotacredito dnc
            ON dnc.codNotaCredito = nc.codNotaCredito
           AND dnc.codProducto    = p_producto_id
        JOIN producto p3
            ON p3.codProducto     = dnc.codProducto
        LEFT JOIN (
            SELECT codProducto, codUnidadMedida, MAX(factor) AS factor
            FROM unidadequivalente
            WHERE compra_venta = 2
            GROUP BY codProducto, codUnidadMedida
        ) ue2
            ON ue2.codProducto     = dnc.codProducto
           AND ue2.codUnidadMedida = dnc.unidadingresada
        WHERE fv1.anulado        = 0
          AND fv1.fecharegistro  >= p_fecha
          AND fv1.fecharegistro  <  p_fecha + INTERVAL 1 DAY
    ) t_dev;

    
    
    
    SET v_neto = v_bruto - v_devolucion;

    INSERT INTO reporte_productos_vendidos_neto_dia (
        fecha_dia, producto_id, almacen_id,
        cantidad_bruta, devoluciones_neta, total_neto, updated_at
    )
    VALUES (
        p_fecha, p_producto_id, p_almacen_id,
        v_bruto, v_devolucion, v_neto, NOW()
    )
    ON DUPLICATE KEY UPDATE
        cantidad_bruta    = VALUES(cantidad_bruta),
        devoluciones_neta = VALUES(devoluciones_neta),
        total_neto        = VALUES(total_neto),
        updated_at        = NOW();

END
utf8mb4
utf8mb4_general_ci
utf8mb4_unicode_ci
