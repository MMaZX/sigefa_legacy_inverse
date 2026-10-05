# Contrato de los procedimientos del cierre de venta

Referencia para quien implemente `VentaCierreRepositorio`. Fuente: lectura de código C# y `SHOW CREATE PROCEDURE` en la base **local** MySQL 5.7.44 (los cuerpos están en `referencia-sql/`). Producción es MySQL 5.7.11 y el usuario de solo lectura no puede ver procedures ni triggers, por lo que la igualdad de cuerpos con producción **no está verificada**.

Reglas de lectura:
- Todos los parámetros de un SP aceptan NULL en MySQL. "Nulo" indica cuándo el código viejo envía `null`.
- El código viejo envía además `entregado_ex` a los dos SP de venta; ningún SP lo declara y hoy se ignora. La ruta nueva **no** debe enviarlo.
- Nombres de parámetros SQL: se respetan tal cual (son el contrato). Las variables C# de la ruta nueva usan camelCase de dominio.

## 1. `GuardaFacturaVenta` (55 parámetros, en este orden)

Origen: `clsFacturaVenta` (`MysqlFacturaVenta.insertComprobante`, `SIGEFA.InterMySql/MysqlFacturaVenta.cs:141`).

| # | Parámetro (tipo) | Propiedad origen | Nulo |
|---|---|---|---|
| 1 | `codSu int` | `CodSucursal` | |
| 2 | `codalma int` | `CodAlmacen` | |
| 3 | `codtran int` | `CodTipoTransaccion` | |
| 4 | `codtipo int` | `CodTipoDocumento` | |
| 5 | `codlista int` | `CodListaPrecio` | |
| 6 | `codser int` | `CodSerie` | |
| 7 | `serie varchar(4)` | `Serie` | |
| 8 | `numdoc varchar(10)` | `NumDoc` (el SP lo **ignora**, usa `serie.numeracion`) | |
| 9 | `tipocliente int` | `TipoCliente` | |
| 10 | `codcli int` | `CodCliente`, o 1 si vale 0 | |
| 11 | `moneda int(1)` | `Moneda` | |
| 12 | `tipocambio float` | `TipoCambio` | |
| 13 | `fechasalida datetime` | `FechaSalida` | |
| 14 | `comentario varchar(500)` | `Comentario` | |
| 15 | `bruto decimal(10,4)` | `MontoBruto` | |
| 16 | `montodscto decimal(10,4)` | `MontoDscto` | |
| 17 | `igv decimal(10,4)` | `Igv` | |
| 18 | `total decimal(10,4)` | `Total` | |
| 19 | `pendiente decimal(10,4)` | **`Total`** (no `Pendiente`) | |
| 20 | `estado bit` | `Estado` | |
| 21 | `formapago int` | `FormaPago` | sí, si vale 0 |
| 22 | `fechapago datetime` | `FechaPago` | |
| 23 | `codven int` | `CodVendedor` | |
| 24 | `codCoti int` | `CodCotizacion` | |
| 25 | `codusu int` | `CodUser` | |
| 26 | `docreferencia varchar(20)` | `DocumentoReferencia` | sí, si es null |
| 27 | `motiv varchar(500)` | `Motivo` | sí, si es `""` |
| 28 | `detcoment varchar(500)` | `Detallecomentario` | sí, si es `""` |
| 29 | `consultorext bit` | `Consultorext` | |
| 30 | `codsalidaconsulext int` | `Codsalidaconsulext` | |
| 31 | `codped int` | `CodPedido` | |
| 32 | `codsep int` | `CodSeparacion` | |
| 33 | `tipoventa_ex int` | `Tipoventa` | |
| 34 | `gravadas_ex decimal(10,4)` | `Gravadas` | |
| 35 | `exoneradas_ex decimal(10,4)` | `Exoneradas` | |
| 36 | `inafectas_ex decimal(10,4)` | `Inafectas` | |
| 37 | `gratuitas_ex decimal(10,4)` | `Gratuitas` | |
| 38 | `codEmpresa_ex int` | `CodEmpresa` | |
| 39 | `Boletafactura_ex int` | `Boletafactura` | |
| 40 | `codigobarras_ex varchar(100)` | `CodigoBarras` | |
| 41 | `codigoBarrasCifrado_ex varchar(100)` | `CodigoBarrasCifrado` | |
| 42 | `NombreCliente_ex varchar(300)` | `Nombre` | |
| 43 | `TipoDocumentoAnticipo_ex varchar(15)` | — | siempre null |
| 44 | `DocumentoReferenciaAnticipo_ex varchar(50)` | — | siempre null |
| 45 | `MontoAnticipo_ex decimal(10,4)` | — | siempre null |
| 46 | `numeroDocumentoIdentidad_ex varchar(15)` | `NumeroDocumentoCliente` | |
| 47 | `codigoDocumentoIdentidad_ex int` | `DocumentoIdentidad.CodDocumentoIdentidad` (null-ref si `DocumentoIdentidad` es null) | |
| 48 | `ventasinstock_ex int` | `ventasinstock` | |
| 49 | `_valorRetencion int` | `valorRetencion` | |
| 50 | `_idTecnico int` | `idTecnico` | sí, si ≤ 0 |
| 51 | `_idZona int` | `idZona` | sí, si ≤ 0 |
| 52 | `_codCanalVenta char(6)` | `CodCanalVenta` (null-ref si es null) | sí, si `Length > 6` |
| 53 | **OUT** `newid int` | se asigna a `CodFacturaVenta` | |
| 54 | `_icbper decimal(10,2)` | `icbper` | |
| 55 | **OUT** `numeraDoc varchar(30)` | se asigna a `NumDoc` | |

Salida y errores: `newid = LAST_INSERT_ID()`. El código viejo trata como fallo un `newid` vacío, no numérico o igual a `"0"`. El SP no usa `SIGNAL`.

Cuerpo en una línea: lee `serie.numeracion` **sin bloqueo**, inserta `factura_venta`, lee `numeraDoc`, inserta la cabecera espejo en `notasalida` (`documentoreferencia = newid`).

Triggers de `factura_venta AFTER INSERT`, en orden: `ActualizaEstadoCotizacion` → `ActualizaCorrelativoDocFact` (`UPDATE serie`) → `ActualizaLineaDisponibleCliente` (`UPDATE cliente`, solo crédito) → `InsertarVentaCreditoEnCaja`.

## 2. `GuardaDetalleFacturaVenta` (32 parámetros, una llamada por ítem)

Origen: `clsDetalleFacturaVenta` (excepto `codventa`).

| # | Parámetro (tipo) | Propiedad origen |
|---|---|---|
| 1 | `codpro int` | `CodProducto` |
| 2 | `codventa int` | `factura_venta.CodFacturaVenta` (string → int) |
| 3 | `codalma int` | `CodAlmacen` |
| 4 | `unidad int` | `UnidadIngresada` |
| 5 | `serielote varchar(20)` | `SerieLote` |
| 6 | `cantidad decimal(10,4)` | `Cantidad` |
| 7 | `cantidadp decimal(10,4)` | `CantidadPendiente` |
| 8 | `moneda int` | `Moneda` |
| 9 | `precio decimal(10,4)` | `PrecioUnitario` |
| 10 | `subtotal decimal(10,4)` | `Subtotal` |
| 11 | `dscto1 decimal(10,4)` | `Descuento1` |
| 12 | `dscto2 decimal(10,4)` | `Descuento2` |
| 13 | `dscto3 decimal(10,4)` | `Descuento3` |
| 14 | `montodscto decimal(10,4)` | `MontoDescuento` |
| 15 | `igv decimal(10,4)` | `Igv` (el código viejo lanza excepción si vale 0) |
| 16 | `importe decimal(10,4)` | `Importe` |
| 17 | `precioreal decimal(10,4)` | `PrecioReal` |
| 18 | `valoreal decimal(10,4)` | `ValoReal` |
| 19 | `codDetaCoti int` | `CodDetalleCotizacion` |
| 20 | `codusu int` | `CodUser` |
| 21 | `codDetaPed int` | `CodDetallePedido` |
| 22 | `codDetaSep int` | `CodDetalleSeparacion` |
| 23 | `serie_ varchar(255)` | `SerieMotor` |
| 24 | `nrochasis_ varchar(255)` | `NroChasis` |
| 25 | `modelo_ varchar(255)` | `Modelo` |
| 26 | `marca_ varchar(255)` | `Marca` |
| 27 | `color_ varchar(255)` | `Color` |
| 28 | `_icbper decimal(10,2)` | `icbper` |
| 29 | `_icbper_band int(1)` | `icbper_band` |
| 30 | `codlinea int` | `codlinea` |
| 31 | `codfamilia int` | `codfamilia` |
| 32 | **OUT** `newid int` | se asigna a `CodDetalleVenta` |

Salida `newid`:
- `> 0`: id del `detallefactura_venta` insertado.
- `-1`: sin stock (solo `codTran = 7`). Error de negocio: debe convertirse en `StockInsuficiente(productoId)`.
- `NULL`: la factura no cumple `estado = 1` o `codTran` no es 7 ni 21. En el código viejo termina en `InvalidCastException`; la ruta nueva debe tratarlo como `DetalleSinFactura`.

Triggers por ítem: `tr_insert_ventas_neta` (→ `sp_venta_neta_dia`); en `detallenotasalida AFTER INSERT`: `ActualizaEstadoPedido` → `ActualizaStockInsertS` → `ActualizaStockdisponibleTransferencia` → `ModificacionStockDisponibleSegunReqAlmacenParaVenta`; en `movimientostock BEFORE INSERT`: `verificarSiExisteAnteriorMovimiento` (recorrido completo de tabla, el más costoso); el SP inserta `kardex` (trigger de snapshot diario).

## 3. `GuardaPago` (39 parámetros)

Origen: `clsPago` (`SIGEFA.InterMySql/MysqlPago.cs`, método `Insert`).

| # | Parámetro (tipo) | Propiedad origen |
|---|---|---|
| 1 | `codnot int` | `CodNota` (= `codFacturaV` de la venta) |
| 2 | `codlet int` | `CodLetra` |
| 3 | `codcuopreban int` | `CodCuotaPreBan` |
| 4 | `codtipopago int` | `CodTipoPago` |
| 5 | `codmon int` | `CodMoneda` |
| 6 | `codtar int` | `CodTarjeta` |
| 7 | `tipo bit` | `Tipo` |
| 8 | `ingegre bit` | `IngresoEgreso` |
| 9 | `tipocambio decimal(10,3)` | `TipoCambio` |
| 10 | `montopa decimal(10,4)` | `MontoPagado` |
| 11 | `montoco decimal(10,4)` | `MontoCobrado` |
| 12 | `vuelto decimal(10,4)` | `Vuelto` |
| 13 | `mora decimal(10,4)` | `Mora` |
| 14 | `codalma int` | `CodAlmacen` |
| 15 | `codcta int` | `codCtaCte` |
| 16 | `numcta varchar(20)` | `CtaCte` |
| 17 | `noperacion varchar(30)` | `NOperacion` |
| 18 | `ncheque varchar(20)` | `NCheque` |
| 19 | `fecha datetime` | `FechaPago` |
| 20 | `observa varchar(250)` | `Observacion` |
| 21 | `codusu int` | `CodUser` |
| 22 | `codban int` | `CodBanco` |
| 23 | `codserie int` | `CodSerie` (null si vale 0) |
| 24 | `serie varchar(4)` | `Serie` (null si es `""`) |
| 25 | `numdoc varchar(30)` | `NumDoc` (null si es `""`) |
| 26 | `aprob int(1)` | `Aprobado` |
| 27 | `ref varchar(30)` | `Referencia` (null si es `""`) |
| 28 | `coddoc int` | `CodDoc` |
| 29 | `provi bit` | `Provision` |
| 30 | `codsucur int` | `CodSucursal` |
| 31 | `codCaja_ex int` | `Codcaja` |
| 32 | `notacre int` | `NotaCredito` |
| 33 | `codnotac int` | `CodNotaCredito` (null si vale 0) |
| 34 | `ctdadDetRet decimal(10,4)` | `RetDet` |
| 35 | `tipoDetRet varchar(4)` | `BanderaRetDet`, o `"NAD"` si es null |
| 36 | `montoEnCuenta decimal(10,4)` | `MontoEnCuenta` |
| 37 | `opcionSuma int` | `OpcionSuma` |
| 38 | `tipo_descripcion int` | `TipoDescripcion` |
| 39 | **OUT** `newid int` | se asigna a `CodPago` |

Salida: el código viejo detecta fallo solo si `ExecuteNonQuery` devuelve 0. Triggers de `pago`: 3 `BEFORE INSERT` (`ActualizaCorrelativoDocPago`, `trg_pago_is_devolucion_bi`, `BeforeInsertPagoLibroBancosGuard`) y 5 `AFTER INSERT` (`ActualizaCuotaPreBanInsertPago`, `ActualizaLetraInsertPago`, `ActualizaPagoCaja`, `trg_insert_update_caja_local`, `ActualizaNotaInsertPago`).

**Verificar al implementar:** que la lectura de `MysqlPago.Insert` coincida con esta tabla (se reconstruyó desde el subagente de investigación; revisar contra el archivo antes de copiar nombres de propiedad).

## 4. Variables MySQL relevantes (producción 5.7.11)

`tx_isolation=REPEATABLE-READ`, `innodb_lock_wait_timeout=50`, `innodb_rollback_on_timeout=OFF` (el error 1205 revierte solo la sentencia: hacer `Rollback()` explícito ante cualquier excepción), `max_allowed_packet=1 GB`. En 5.7 no existen `SKIP LOCKED` ni `NOWAIT` para InnoDB.
