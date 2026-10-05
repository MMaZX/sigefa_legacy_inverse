using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using SIGEFA.Administradores.VentaCierre;
using SIGEFA.Entidades;

// Repositorio transaccional para la ruta nueva de cierre de venta.
// Ejecuta las operaciones SQL recibiendo la conexión y la transacción activas,
// sin abrir conexiones propias ni gestionar transacciones.
// Mapea errores específicos de MySQL y códigos de salida a VentaCierreException.
namespace SIGEFA.InterMySql.VentaCierre
{
    // Contrato del repositorio para inyección en el servicio de cierre de venta.
    public interface IVentaCierreRepositorio
    {
        // Bloquea la fila de la serie con SELECT ... FOR UPDATE. Debe ser la primera lectura.
        int bloquearSerie(MySqlConnection conexion, MySqlTransaction transaccion, int serieId);

        // Bloquea las filas de productoalmacen en orden ascendente de productoId por clave primaria
        // y revalida codAlmacen y codProducto en la misma lectura que bloquea con FOR UPDATE.
        void bloquearStock(MySqlConnection conexion, MySqlTransaction transaccion, int almacenId, IEnumerable<int> productoIds);

        // Inserta la cabecera mediante GuardaFacturaVenta y devuelve el id generado y el número de documento asignado.
        int guardarFacturaVenta(MySqlConnection conexion, MySqlTransaction transaccion, clsFacturaVenta venta, out string numeroDocumento);

        // Sobrecarga de conveniencia que devuelve el id generado de la cabecera.
        int guardarFacturaVenta(MySqlConnection conexion, MySqlTransaction transaccion, clsFacturaVenta venta);

        // Inserta un ítem mediante GuardaDetalleFacturaVenta y devuelve el id generado.
        int guardarDetalle(MySqlConnection conexion, MySqlTransaction transaccion, clsDetalleFacturaVenta detalle, int facturaVentaId, int itemIndice);

        // Inserta un pago capturado mediante GuardaPago y devuelve el id generado.
        int guardarPago(MySqlConnection conexion, MySqlTransaction transaccion, BorradorPago pago, int facturaVentaId);
    }

    // Implementación MySQL del repositorio transaccional de cierre de venta.
    // Todas las operaciones usan MySQL 5.7 y respetan estrictamente el contrato de SPs.
    public class VentaCierreRepositorio : IVentaCierreRepositorio
    {
        // Bloquea la fila de la serie mediante SELECT numeracion FROM serie WHERE codSerie=@serieId FOR UPDATE.
        // Debe ser la primera lectura de la transacción para asegurar numeración correlativa sin saltos.
        // Espera: serieId existente en la tabla serie.
        // Devuelve: la numeración actual registrada en la serie.
        public int bloquearSerie(MySqlConnection conexion, MySqlTransaction transaccion, int serieId)
        {
            const string sql = "SELECT numeracion FROM serie WHERE codSerie = @serieId FOR UPDATE;";
            string parametrosInfo = "serieId=" + serieId;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conexion, transaccion))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@serieId", serieId);

                    object valor = cmd.ExecuteScalar();
                    if (valor == null || valor == DBNull.Value)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.bloquearSerie,
                            "bloquearSerie",
                            0,
                            string.Empty,
                            "No se encontró la serie con ID " + serieId,
                            null,
                            null,
                            null,
                            parametrosInfo);
                    }

                    return Convert.ToInt32(valor);
                }
            }
            catch (MySqlException ex)
            {
                throw new VentaCierreException(
                    VentaCierrePaso.bloquearSerie,
                    "bloquearSerie",
                    ex.Number,
                    ex.SqlState,
                    ex.Message,
                    ex,
                    null,
                    null,
                    parametrosInfo);
            }
        }

        // Bloquea las filas de productoalmacen para los productos indicados.
        // Para evitar deadlocks entre transacciones concurrentes, primero elimina duplicados,
        // ordena los productos ascendentemente por productoId, obtiene codProductoAlmacen y
        // finalmente ejecuta SELECT ... FOR UPDATE por clave primaria revalidando codAlmacen y codProducto
        // en la misma lectura bloqueante. Valida existencia y lectura de stock.
        // Espera: almacenId válido y colección de productoIds de la venta.
        public void bloquearStock(MySqlConnection conexion, MySqlTransaction transaccion, int almacenId, IEnumerable<int> productoIds)
        {
            if (productoIds == null)
            {
                return;
            }

            // Orden determinístico por productoId ascendente y sin duplicados para prevenir deadlocks
            List<int> productosOrdenados = productoIds.Distinct().OrderBy(id => id).ToList();

            foreach (int productoId in productosOrdenados)
            {
                string parametrosInfo = "almacenId=" + almacenId + ";productoId=" + productoId;

                try
                {
                    int codProductoAlmacen;

                    // Paso 1: leer la clave primaria codProductoAlmacen
                    const string sqlLeerId = "SELECT codProductoAlmacen FROM productoalmacen WHERE codAlmacen = @almacenId AND codProducto = @productoId LIMIT 1;";
                    using (MySqlCommand cmdLeer = new MySqlCommand(sqlLeerId, conexion, transaccion))
                    {
                        cmdLeer.CommandType = CommandType.Text;
                        cmdLeer.Parameters.AddWithValue("@almacenId", almacenId);
                        cmdLeer.Parameters.AddWithValue("@productoId", productoId);

                        object valorId = cmdLeer.ExecuteScalar();
                        if (valorId == null || valorId == DBNull.Value)
                        {
                            throw new VentaCierreException(
                                VentaCierrePaso.bloquearStock,
                                "bloquearStock",
                                0,
                                string.Empty,
                                "El producto con ID " + productoId + " no tiene registro en el almacén " + almacenId,
                                null,
                                null,
                                productoId,
                                parametrosInfo);
                        }

                        codProductoAlmacen = Convert.ToInt32(valorId);
                    }

                    // Paso 2: bloquear por clave primaria con FOR UPDATE y revalidar codAlmacen y codProducto en la misma lectura
                    const string sqlBloqueo = "SELECT codProductoAlmacen, stockactual, stockdisponible FROM productoalmacen WHERE codProductoAlmacen = @codProductoAlmacen AND codAlmacen = @almacenId AND codProducto = @productoId FOR UPDATE;";
                    using (MySqlCommand cmdBloqueo = new MySqlCommand(sqlBloqueo, conexion, transaccion))
                    {
                        cmdBloqueo.CommandType = CommandType.Text;
                        cmdBloqueo.Parameters.AddWithValue("@codProductoAlmacen", codProductoAlmacen);
                        cmdBloqueo.Parameters.AddWithValue("@almacenId", almacenId);
                        cmdBloqueo.Parameters.AddWithValue("@productoId", productoId);

                        using (MySqlDataReader dr = cmdBloqueo.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                throw new VentaCierreException(
                                    VentaCierrePaso.bloquearStock,
                                    "bloquearStock",
                                    0,
                                    string.Empty,
                                    "No se pudo bloquear o revalidar el registro de stock para el producto " + productoId + " en el almacén " + almacenId + " (codProductoAlmacen " + codProductoAlmacen + ")",
                                    null,
                                    null,
                                    productoId,
                                    parametrosInfo + ";codProductoAlmacen=" + codProductoAlmacen);
                            }

                            // Validar presencia de stockactual y stockdisponible
                            if (dr["stockactual"] == DBNull.Value || dr["stockdisponible"] == DBNull.Value)
                            {
                                throw new VentaCierreException(
                                    VentaCierrePaso.bloquearStock,
                                    "bloquearStock",
                                    0,
                                    string.Empty,
                                    "Valores de stock inválidos (NULL) en productoalmacen para el producto " + productoId,
                                    null,
                                    null,
                                    productoId,
                                    parametrosInfo + ";codProductoAlmacen=" + codProductoAlmacen);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new VentaCierreException(
                        VentaCierrePaso.bloquearStock,
                        "bloquearStock",
                        ex.Number,
                        ex.SqlState,
                        ex.Message,
                        ex,
                        null,
                        productoId,
                        parametrosInfo);
                }
            }
        }

        // Guarda la cabecera de la factura de venta invocando al procedimiento GuardaFacturaVenta.
        // Envía exactamente los 55 parámetros estipulados en el contrato (omitiendo entregado_ex).
        // Mapea newid <= 0 a CabeceraNoCreada y devuelve numeraDoc generado en el parámetro out.
        // NO modifica las propiedades de la entidad hasta que la transacción se confirme en el servicio.
        // Espera: entidad clsFacturaVenta con datos completos de cabecera.
        // Devuelve: facturaVentaId (newid generado por LAST_INSERT_ID()).
        public int guardarFacturaVenta(MySqlConnection conexion, MySqlTransaction transaccion, clsFacturaVenta venta, out string numeroDocumento)
        {
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            string parametrosInfo = "codSu=" + venta.CodSucursal + ";codalma=" + venta.CodAlmacen +
                                    ";codser=" + venta.CodSerie + ";codcli=" + venta.CodCliente +
                                    ";total=" + venta.Total;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("GuardaFacturaVenta", conexion, transaccion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 1..10
                    cmd.Parameters.AddWithValue("codSu", venta.CodSucursal);
                    cmd.Parameters.AddWithValue("codalma", venta.CodAlmacen);
                    cmd.Parameters.AddWithValue("codtran", venta.CodTipoTransaccion);
                    cmd.Parameters.AddWithValue("codtipo", venta.CodTipoDocumento);
                    cmd.Parameters.AddWithValue("codlista", venta.CodListaPrecio);
                    cmd.Parameters.AddWithValue("codser", venta.CodSerie);
                    cmd.Parameters.AddWithValue("serie", venta.Serie ?? string.Empty);
                    cmd.Parameters.AddWithValue("numdoc", venta.NumDoc ?? string.Empty);
                    cmd.Parameters.AddWithValue("tipocliente", venta.TipoCliente);
                    cmd.Parameters.AddWithValue("codcli", venta.CodCliente != 0 ? venta.CodCliente : 1);

                    // 11..20
                    cmd.Parameters.AddWithValue("moneda", venta.Moneda);
                    cmd.Parameters.AddWithValue("tipocambio", venta.TipoCambio);
                    cmd.Parameters.AddWithValue("fechasalida", venta.FechaSalida);
                    cmd.Parameters.AddWithValue("comentario", venta.Comentario ?? string.Empty);
                    cmd.Parameters.AddWithValue("bruto", venta.MontoBruto);
                    cmd.Parameters.AddWithValue("montodscto", venta.MontoDscto);
                    cmd.Parameters.AddWithValue("igv", venta.Igv);
                    cmd.Parameters.AddWithValue("total", venta.Total);
                    cmd.Parameters.AddWithValue("pendiente", venta.Total); // En el flujo original se envía Total
                    cmd.Parameters.AddWithValue("estado", venta.Estado);

                    // 21..30
                    cmd.Parameters.AddWithValue("formapago", venta.FormaPago != 0 ? (object)venta.FormaPago : DBNull.Value);
                    cmd.Parameters.AddWithValue("fechapago", venta.FechaPago);
                    cmd.Parameters.AddWithValue("codven", venta.CodVendedor);
                    cmd.Parameters.AddWithValue("codCoti", venta.CodCotizacion);
                    cmd.Parameters.AddWithValue("codusu", venta.CodUser);
                    // Hallazgo 6: docreferencia solo se envía NULL si es null; la cadena vacía se conserva
                    cmd.Parameters.AddWithValue("docreferencia", (venta.DocumentoReferencia != null) ? (object)venta.DocumentoReferencia : DBNull.Value);
                    cmd.Parameters.AddWithValue("motiv", !string.IsNullOrEmpty(venta.Motivo) ? (object)venta.Motivo : DBNull.Value);
                    cmd.Parameters.AddWithValue("detcoment", !string.IsNullOrEmpty(venta.Detallecomentario) ? (object)venta.Detallecomentario : DBNull.Value);
                    cmd.Parameters.AddWithValue("consultorext", venta.Consultorext);
                    cmd.Parameters.AddWithValue("codsalidaconsulext", venta.Codsalidaconsulext);

                    // 31..40
                    cmd.Parameters.AddWithValue("codped", venta.CodPedido);
                    cmd.Parameters.AddWithValue("codsep", venta.CodSeparacion);
                    cmd.Parameters.AddWithValue("tipoventa_ex", venta.Tipoventa);
                    cmd.Parameters.AddWithValue("gravadas_ex", venta.Gravadas);
                    cmd.Parameters.AddWithValue("exoneradas_ex", venta.Exoneradas);
                    cmd.Parameters.AddWithValue("inafectas_ex", venta.Inafectas);
                    cmd.Parameters.AddWithValue("gratuitas_ex", venta.Gratuitas);
                    cmd.Parameters.AddWithValue("codEmpresa_ex", venta.CodEmpresa);
                    cmd.Parameters.AddWithValue("Boletafactura_ex", venta.Boletafactura);
                    cmd.Parameters.AddWithValue("codigobarras_ex", venta.CodigoBarras ?? string.Empty);

                    // 41..50
                    cmd.Parameters.AddWithValue("codigoBarrasCifrado_ex", venta.CodigoBarrasCifrado ?? string.Empty);
                    cmd.Parameters.AddWithValue("NombreCliente_ex", venta.Nombre ?? string.Empty);
                    cmd.Parameters.AddWithValue("TipoDocumentoAnticipo_ex", DBNull.Value);
                    cmd.Parameters.AddWithValue("DocumentoReferenciaAnticipo_ex", DBNull.Value);
                    cmd.Parameters.AddWithValue("MontoAnticipo_ex", DBNull.Value);
                    cmd.Parameters.AddWithValue("numeroDocumentoIdentidad_ex", venta.NumeroDocumentoCliente ?? string.Empty);
                    cmd.Parameters.AddWithValue("codigoDocumentoIdentidad_ex", (venta.DocumentoIdentidad != null) ? (object)venta.DocumentoIdentidad.CodDocumentoIdentidad : DBNull.Value);
                    cmd.Parameters.AddWithValue("ventasinstock_ex", venta.ventasinstock);
                    cmd.Parameters.AddWithValue("_valorRetencion", venta.valorRetencion);
                    cmd.Parameters.AddWithValue("_idTecnico", venta.idTecnico > 0 ? (object)venta.idTecnico : DBNull.Value);

                    // 51..55
                    cmd.Parameters.AddWithValue("_idZona", venta.idZona > 0 ? (object)venta.idZona : DBNull.Value);
                    cmd.Parameters.AddWithValue("_codCanalVenta", (venta.CodCanalVenta != null && venta.CodCanalVenta.Length <= 6) ? (object)venta.CodCanalVenta : DBNull.Value);

                    MySqlParameter paramNewId = cmd.Parameters.AddWithValue("newid", 0);
                    paramNewId.Direction = ParameterDirection.Output;

                    cmd.Parameters.AddWithValue("_icbper", venta.icbper);

                    MySqlParameter paramNumeraDoc = cmd.Parameters.AddWithValue("numeraDoc", string.Empty);
                    paramNumeraDoc.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    object valorNewId = cmd.Parameters["newid"].Value;
                    if (valorNewId == null || valorNewId == DBNull.Value)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarCabecera,
                            "GuardaFacturaVenta",
                            0,
                            string.Empty,
                            "CabeceraNoCreada: GuardaFacturaVenta devolvió NULL en newid",
                            null,
                            null,
                            null,
                            parametrosInfo);
                    }

                    int facturaVentaId = Convert.ToInt32(valorNewId);
                    if (facturaVentaId <= 0)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarCabecera,
                            "GuardaFacturaVenta",
                            0,
                            string.Empty,
                            "CabeceraNoCreada: GuardaFacturaVenta devolvió newid no válido (" + facturaVentaId + ")",
                            null,
                            null,
                            null,
                            parametrosInfo);
                    }

                    numeroDocumento = Convert.ToString(cmd.Parameters["numeraDoc"].Value);

                    // Hallazgo 5: NO mutar venta.CodFacturaVenta ni venta.NumDoc aquí antes de Commit
                    return facturaVentaId;
                }
            }
            catch (MySqlException ex)
            {
                throw new VentaCierreException(
                    VentaCierrePaso.guardarCabecera,
                    "GuardaFacturaVenta",
                    ex.Number,
                    ex.SqlState,
                    ex.Message,
                    ex,
                    null,
                    null,
                    parametrosInfo);
            }
        }

        // Sobrecarga de conveniencia sin parámetro out para compatibilidad con llamadas que solo necesitan el id.
        public int guardarFacturaVenta(MySqlConnection conexion, MySqlTransaction transaccion, clsFacturaVenta venta)
        {
            string numDocGenerado;
            return guardarFacturaVenta(conexion, transaccion, venta, out numDocGenerado);
        }

        // Guarda un ítem del detalle de venta invocando al procedimiento GuardaDetalleFacturaVenta.
        // Envía exactamente los 32 parámetros del contrato (omitiendo entregado_ex).
        // Mapea newid: >0 éxito, -1 StockInsuficiente, NULL DetalleSinFactura.
        // NO asigna CodDetalleVenta a la entidad hasta que la transacción se confirme en el servicio.
        // Espera: entidad clsDetalleFacturaVenta, facturaVentaId de la cabecera e itemIndice base 1.
        // Devuelve: detalleFacturaVentaId generado.
        public int guardarDetalle(MySqlConnection conexion, MySqlTransaction transaccion, clsDetalleFacturaVenta detalle, int facturaVentaId, int itemIndice)
        {
            if (detalle == null)
            {
                throw new ArgumentNullException(nameof(detalle));
            }

            string parametrosInfo = "codpro=" + detalle.CodProducto + ";codventa=" + facturaVentaId +
                                    ";codalma=" + detalle.CodAlmacen + ";cantidad=" + detalle.Cantidad +
                                    ";precio=" + detalle.PrecioUnitario;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("GuardaDetalleFacturaVenta", conexion, transaccion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 1..10
                    cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
                    cmd.Parameters.AddWithValue("codventa", facturaVentaId);
                    cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
                    cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
                    cmd.Parameters.AddWithValue("serielote", detalle.SerieLote ?? string.Empty);
                    cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("cantidadp", detalle.CantidadPendiente);
                    cmd.Parameters.AddWithValue("moneda", detalle.Moneda);
                    cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
                    cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);

                    // 11..20
                    cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
                    cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
                    cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
                    cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
                    cmd.Parameters.AddWithValue("igv", detalle.Igv);
                    cmd.Parameters.AddWithValue("importe", detalle.Importe);
                    cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
                    cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
                    cmd.Parameters.AddWithValue("codDetaCoti", detalle.CodDetalleCotizacion);
                    cmd.Parameters.AddWithValue("codusu", detalle.CodUser);

                    // 21..30
                    cmd.Parameters.AddWithValue("codDetaPed", detalle.CodDetallePedido);
                    cmd.Parameters.AddWithValue("codDetaSep", detalle.CodDetalleSeparacion);
                    cmd.Parameters.AddWithValue("serie_", detalle.SerieMotor ?? string.Empty);
                    cmd.Parameters.AddWithValue("nrochasis_", detalle.NroChasis ?? string.Empty);
                    cmd.Parameters.AddWithValue("modelo_", detalle.Modelo ?? string.Empty);
                    cmd.Parameters.AddWithValue("marca_", detalle.Marca ?? string.Empty);
                    cmd.Parameters.AddWithValue("color_", detalle.Color ?? string.Empty);
                    cmd.Parameters.AddWithValue("_icbper", detalle.icbper);
                    cmd.Parameters.AddWithValue("_icbper_band", detalle.icbper_band);
                    cmd.Parameters.AddWithValue("codlinea", detalle.codlinea);

                    // 31..32
                    cmd.Parameters.AddWithValue("codfamilia", detalle.codfamilia);

                    MySqlParameter paramNewId = cmd.Parameters.AddWithValue("newid", 0);
                    paramNewId.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    object valorNewId = cmd.Parameters["newid"].Value;
                    if (valorNewId == null || valorNewId == DBNull.Value)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarDetalle,
                            "GuardaDetalleFacturaVenta",
                            0,
                            string.Empty,
                            "DetalleSinFactura: GuardaDetalleFacturaVenta devolvió NULL en newid para la venta " + facturaVentaId,
                            null,
                            itemIndice,
                            detalle.CodProducto,
                            parametrosInfo);
                    }

                    int newId = Convert.ToInt32(valorNewId);
                    if (newId == -1)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarDetalle,
                            "GuardaDetalleFacturaVenta",
                            0,
                            string.Empty,
                            "StockInsuficiente: el producto con ID " + detalle.CodProducto + " no tiene stock suficiente en el almacén " + detalle.CodAlmacen,
                            null,
                            itemIndice,
                            detalle.CodProducto,
                            parametrosInfo);
                    }

                    if (newId <= 0)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarDetalle,
                            "GuardaDetalleFacturaVenta",
                            0,
                            string.Empty,
                            "DetalleNoCreado: GuardaDetalleFacturaVenta devolvió " + newId + " para el producto " + detalle.CodProducto,
                            null,
                            itemIndice,
                            detalle.CodProducto,
                            parametrosInfo);
                    }

                    // Hallazgo 5: NO mutar detalle.CodDetalleVenta aquí antes de Commit
                    return newId;
                }
            }
            catch (MySqlException ex)
            {
                throw new VentaCierreException(
                    VentaCierrePaso.guardarDetalle,
                    "GuardaDetalleFacturaVenta",
                    ex.Number,
                    ex.SqlState,
                    ex.Message,
                    ex,
                    itemIndice,
                    detalle.CodProducto,
                    parametrosInfo);
            }
        }

        // Guarda un pago capturado invocando al procedimiento GuardaPago.
        // Envía exactamente los 39 parámetros estipulados en el contrato.
        // Mapea 0 filas afectadas o newid <= 0 a PagoNoCreado.
        // Espera: BorradorPago inmutable y facturaVentaId de la cabecera.
        // Devuelve: pagoId (CodPago generado).
        public int guardarPago(MySqlConnection conexion, MySqlTransaction transaccion, BorradorPago pago, int facturaVentaId)
        {
            if (pago == null)
            {
                throw new ArgumentNullException(nameof(pago));
            }

            string parametrosInfo = "codnot=" + facturaVentaId + ";codtipopago=" + pago.tipoPagoId +
                                    ";montopa=" + pago.montoPagado + ";montoco=" + pago.montoCobrado +
                                    ";codalma=" + pago.almacenId;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("GuardaPago", conexion, transaccion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 1..10
                    cmd.Parameters.AddWithValue("codnot", facturaVentaId);
                    cmd.Parameters.AddWithValue("codlet", pago.letraId);
                    cmd.Parameters.AddWithValue("codcuopreban", pago.cuotaPreBanId);
                    cmd.Parameters.AddWithValue("codtipopago", pago.tipoPagoId);
                    cmd.Parameters.AddWithValue("codmon", pago.monedaId);
                    cmd.Parameters.AddWithValue("codtar", pago.tarjetaId);
                    cmd.Parameters.AddWithValue("tipo", pago.tipo);
                    cmd.Parameters.AddWithValue("ingegre", pago.ingresoEgreso);
                    cmd.Parameters.AddWithValue("tipocambio", pago.tipoCambio);
                    cmd.Parameters.AddWithValue("montopa", pago.montoPagado);

                    // 11..20
                    cmd.Parameters.AddWithValue("montoco", pago.montoCobrado);
                    cmd.Parameters.AddWithValue("vuelto", pago.vuelto);
                    cmd.Parameters.AddWithValue("mora", pago.mora);
                    cmd.Parameters.AddWithValue("codalma", pago.almacenId);
                    cmd.Parameters.AddWithValue("codcta", pago.cuentaCorrienteId);
                    cmd.Parameters.AddWithValue("numcta", pago.cuentaCorriente ?? string.Empty);
                    cmd.Parameters.AddWithValue("noperacion", pago.numeroOperacion ?? string.Empty);
                    cmd.Parameters.AddWithValue("ncheque", pago.numeroCheque ?? string.Empty);
                    cmd.Parameters.AddWithValue("fecha", pago.fechaPago);
                    cmd.Parameters.AddWithValue("observa", pago.observacion ?? string.Empty);

                    // 21..30
                    cmd.Parameters.AddWithValue("codusu", pago.usuarioId);
                    cmd.Parameters.AddWithValue("codban", pago.bancoId);
                    cmd.Parameters.AddWithValue("codserie", pago.serieId != 0 ? (object)pago.serieId : DBNull.Value);
                    cmd.Parameters.AddWithValue("serie", !string.IsNullOrEmpty(pago.serie) ? (object)pago.serie : DBNull.Value);
                    cmd.Parameters.AddWithValue("numdoc", !string.IsNullOrEmpty(pago.numeroDocumento) ? (object)pago.numeroDocumento : DBNull.Value);
                    cmd.Parameters.AddWithValue("aprob", pago.aprobado);
                    cmd.Parameters.AddWithValue("ref", !string.IsNullOrEmpty(pago.referencia) ? (object)pago.referencia : DBNull.Value);
                    cmd.Parameters.AddWithValue("coddoc", pago.documentoId != 0 ? (object)pago.documentoId : 0);
                    cmd.Parameters.AddWithValue("provi", pago.provision);
                    cmd.Parameters.AddWithValue("codsucur", pago.sucursalId);

                    // 31..39
                    cmd.Parameters.AddWithValue("codCaja_ex", pago.cajaId);
                    cmd.Parameters.AddWithValue("notacre", pago.notaCredito != 0 ? (object)pago.notaCredito : 0);
                    cmd.Parameters.AddWithValue("codnotac", pago.notaCreditoId != 0 ? (object)pago.notaCreditoId : DBNull.Value);
                    cmd.Parameters.AddWithValue("ctdadDetRet", pago.retencionDetraccion);
                    cmd.Parameters.AddWithValue("tipoDetRet", pago.banderaRetencionDetraccion ?? "NAD");
                    cmd.Parameters.AddWithValue("montoEnCuenta", pago.montoEnCuenta);
                    cmd.Parameters.AddWithValue("opcionSuma", pago.opcionSuma);
                    cmd.Parameters.AddWithValue("tipo_descripcion", pago.tipoDescripcion);

                    MySqlParameter paramNewId = cmd.Parameters.AddWithValue("newid", 0);
                    paramNewId.Direction = ParameterDirection.Output;

                    int filas = cmd.ExecuteNonQuery();

                    object valorNewId = cmd.Parameters["newid"].Value;
                    if (filas == 0 || valorNewId == null || valorNewId == DBNull.Value)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarPago,
                            "GuardaPago",
                            0,
                            string.Empty,
                            "PagoNoCreado: GuardaPago no afectó filas o devolvió newid nulo",
                            null,
                            null,
                            null,
                            parametrosInfo);
                    }

                    int pagoId = Convert.ToInt32(valorNewId);
                    if (pagoId <= 0)
                    {
                        throw new VentaCierreException(
                            VentaCierrePaso.guardarPago,
                            "GuardaPago",
                            0,
                            string.Empty,
                            "PagoNoCreado: GuardaPago devolvió newid no válido (" + pagoId + ")",
                            null,
                            null,
                            null,
                            parametrosInfo);
                    }

                    return pagoId;
                }
            }
            catch (MySqlException ex)
            {
                throw new VentaCierreException(
                    VentaCierrePaso.guardarPago,
                    "GuardaPago",
                    ex.Number,
                    ex.SqlState,
                    ex.Message,
                    ex,
                    null,
                    null,
                    parametrosInfo);
            }
        }
    }
}
