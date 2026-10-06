# Anulación de requerimientos de almacén de venta: estudio de procedures y triggers

Estudio de solo lectura sobre la BD de desarrollo (`127.0.0.1:3307`, MySQL 5.7.44, 2026-10-06). No se tocó producción ni se escribió en ninguna tabla. Las afirmaciones marcadas **hecho** se verificaron con SQL o con el código; las marcadas **hipótesis** son inferencias. La versión de MySQL y la configuración de producción no se verificaron.

Verificado por segunda vez en la sesión principal: no existen eventos (`event_scheduler=OFF`, 0 eventos); ningún procedure ni trigger del flujo contiene `COMMIT`, `START TRANSACTION`, DDL ni `LOCK TABLES`; el trigger `ActualizaDisponibleAprobarTransferencia` actualiza `req_almacen.estado` sin la guarda `estado<>12`; `GuardaDetalleIngreso` declara `fac INT(11)`; `GuardaDetalleSalida` compara `stockactual >= canti` sin factor de unidad y devuelve `newid = 0` sin error.

## 1. Quién escribe `req_almacen.estado`

| Origen | Estado que fija | Respeta el 12 |
|---|---|---|
| SP `AnularRequerimientoAlmacen` | 12 (y `fecha_anulo`, `cod_user_anulo`) | No (reanular sobrescribe fecha y usuario) |
| SP `AprobarRequerimientoAlmacen` | 8 | No |
| SP `CerrarRequerimientoAlmacen` | 9 | No |
| SP `ActualizaEstadoReqAlmacen(codra, _estado)` | cualquiera | No |
| SP `ActualizaCantidadPendienteAprobadaReqAlmacen` | 11 si la suma pendiente es 0, si no 10 | No |
| SP `ActualizaRequerimientoAlmacen` | lo que traiga la entidad C# | No |
| Trigger `ActualizaDisponibleAprobarTransferencia` (detalletransferencia, AFTER UPDATE de `pendiente` 1 a 0) | 10 u 11 | No |

- No hay eventos del programador de MySQL.
- El único objeto que respeta el estado 12 es el trigger `ModificacionStockDisponibleSegunReqAlmacenParaVenta` (usa `estado != 12`), pero no escribe estado.
- Los extornos no están en `inter_req_almacen_transferencia`, así que el trigger no toca el estado del requerimiento cuando se aprueba un extorno.
- **Latente (hecho):** los reqs 6101, 6537, 9150 y 10951 están anulados y conservan una transferencia sin aprobar ni rechazar (13561, 14039, 16902, 18874). Si alguien la aprueba, el trigger los pasa a 10 u 11 y mueve stock.

### Código C# que cambia el estado

| Archivo:línea | Estado | Guarda previa |
|---|---|---|
| `frmDespacho.cs:504`, `:677` | 11 | No mira si el requerimiento está anulado |
| `frmDespacho.cs:681` | 10 | Ninguna |
| `frmDespacho.cs:735/739` | 11/10 | Sí (`IEstado==12`, `:708`) |
| `frmEntrega.cs:176` | 17 | Ninguna; va después de un `MessageBox` modal (`:161`) |
| `frmEntrega.cs:181` | 10/11 | Ninguna |
| `frmNotadeCredito.cs:1462/1466` | 11/10 | Ninguna |
| `frmVenta2019.cs:4238` | 17 | Al facturar |
| `frmReqAlmacen.cs:1203`, `:1636` | 10, 13 | Al generar o aprobar la transferencia |
| `frmReqAlmacen.cs:1899`, `F2TransferenciaEntreAlmacenes.cs:856` | 10/11 | Ninguna |
| `frmVenta2019.cs:3354` | `IEstado` de la entidad | Edición del pedido |

Llamadas a `anular()`: `FrmTPenPedido:246/274`, `frmPedidosPendientes:213/250`, `frmVenta2019:3156/3332`, `frmVentas:631`, `frmNotadeCredito:1334/1523`, `frmReqAlmacen:1104` (solo tipo 1). No hay SQL inline `UPDATE req_almacen`.

### Los 6 requerimientos con `fecha_anulo` y estado distinto de 12

| Req | Estado | Qué ocurrió | Responsable |
|---|---|---|---|
| 2883 | 10 | Anulado 11:41:53; entrega creada a las 11:57:06 sobre un despacho ya anulado | **Hecho:** `frmDespacho:681` |
| 12010 | 11 | Anulado 13:52:35 (extorno 20024 huérfano); entrega a las 13:55:40 | `frmDespacho:677` |
| 958, 7709, 10202 | 17 | Entrega anulada, nota de crédito o anulación de factura ~1 min después; despacho con `anulado=1` y codEstado 14 | **Inferencia fuerte:** el código de `frmEntrega` posterior al `MessageBox` (`:166-176`) terminó después de que otro puesto anulara; no hay log para probarlo |
| 3321 | 18 | Misma secuencia; el 18 es un código de despacho (`origen=3`) | **Hipótesis:** una versión anterior del binario copiaba el estado del despacho |

## 2. Cómo se construye el extorno

### Procedures

| SP | Entrada relevante | Devuelve | Notas |
|---|---|---|---|
| `GuardaTransferencia` | 23 IN (moneda, tipocambio, bruto, igv, total, `estado bit`, serie, numerodoc, `_codreqalm`, `_codTransferenciaExtornar`...) | `OUT newid` (`LAST_INSERT_ID`) | `pendiente` queda en 1. Trigger `ActualizaCorrelativoTD` suma 1 a `serie.numeracion`: el extorno consume un correlativo y repite el número de la original |
| `GuardaDetalleTransferencia` | cantidad, precios (**float**), `promedio decimal(10,4)`, `_coddetallereqalm` | `OUT newid` | Pierde precisión por los float |
| `GuardaNotaSalida` | serie `varchar(4)`, comentario `varchar(60)` (modo STRICT), `codTransferecia_ex` | `OUT newid` | Triggers de enlace con nota de ingreso |
| `GuardaDetalleSalida` | `canti decimal(10,4)`... | `OUT newid` | **`newid = 0` si `stockactual < canti`**, sin error; compara sin factor de unidad |
| `GuardaNotaIngreso` | `codref`, `codTransferencia_ex` | `OUT newid` | El C# envía un parámetro que el SP no declara (`fechacancelado`) |
| `GuardaDetalleIngreso` | 24 IN | `OUT newid` | `fac INT`: kardex mal en líneas con factor no entero |
| `AprobarTransferencia(codtrans)` | | | `pendiente=0`, `EstadoTrnas=1`; no valida estado ni pendiente |
| `RechazarTransferencia(codtrans, descripcion)` | | | `estado=0`; no exige `pendiente=1` |
| `RetornandoStockAlAnularReqAlmacen` | `_cantidad_pend_aprob decimal` (= `DECIMAL(10,0)`) | | Redondea cantidades fraccionarias |

### Triggers del flujo (efecto exacto)

- `detallenotasalida` AFTER INSERT, transacción 15: `stockdisponible -= cant*fac` y `stockactual -= cant*fac` en el almacén de la nota; movimientostock; kardex.
- `detallenotaingreso` AFTER INSERT, transacción 15: `stockdisponible += cant*fa` y `stockactual += cant*fa`; `soles += valoreal*cant` (sin factor); `fa DECIMAL(10,2)` sale de `producto.codUnidadMedida` (la salida usa `productoalmacen.Unidad`).
- `detalletransferencia` AFTER UPDATE, `pendiente` de 1 a 0: en transferencia normal repone `stockdisponible` en el despacho y, si `tipo_req=2`, lo descuenta en el solicitante; para un extorno (tipo documento 50), si el pedido no tiene factura, suma `stockdisponible` en el solicitante.
- `movimientostock` BEFORE INSERT: recalcula `stockFinal` con un recorrido sin índice (alarga los bloqueos).

### Secuencia propuesta sobre UNA conexión y UNA transacción

Nivel de aislamiento sugerido: `READ COMMITTED` (los triggers leen stock sin bloqueo; en `REPEATABLE READ` una transacción larga leería una foto vieja).

**Anular un requerimiento pendiente (estado 7)**
1. `START TRANSACTION`.
2. `SELECT estado, tipo_req FROM req_almacen WHERE id_req_almacen=? FOR UPDATE`; si no es 7, rollback.
3. Rechazar cada transferencia pendiente (`estado=1 AND pendiente=1`, con `FOR UPDATE`) con `RechazarTransferencia`.
4. Devolver el stock reservado solo de los detalles con `cantidad_pendiente_aprobada > 0` (hoy los pendientes tienen 0).
5. `AnularRequerimientoAlmacen` y comprobar 1 fila afectada.
6. Verificar `estado=12` y `COMMIT`.

**Anular un requerimiento aprobado/transferido (estado 13) con extorno**
1. `FOR UPDATE` sobre `req_almacen`; validar que no sea 12 y que `tipo_req=2`.
2. Buscar la transferencia original vigente que **no** tenga extorno (`NOT EXISTS` por `codDocExtornacion`), con `FOR UPDATE`; si no hay exactamente una, rollback.
3. Bloquear `productoalmacen` de los productos y almacenes involucrados, ordenado por (almacén, producto), y validar stock del solicitante **aplicando el factor de unidad**.
4. `GuardaTransferencia` (almacenes invertidos, tipo documento 50, valores de la original) y comprobar `newid > 0`.
5. `GuardaDetalleTransferencia` por línea, copiando los valores de la original.
6. `GuardaNotaSalida` y `GuardaDetalleSalida` por línea; si `newid = 0`, rollback.
7. `GuardaNotaIngreso` y `GuardaDetalleIngreso` por línea.
8. `AprobarTransferencia`.
9. Si quedó `cantidad_pendiente_aprobada > 0`, devolver esa reserva.
10. `AnularRequerimientoAlmacen` al final (no llamar `ActualizaCantidadPendienteAprobadaReqAlmacen`: lo pasaría a 10 u 11).
11. Verificar `estado=12` y `COMMIT`. Ante cualquier fallo, `ROLLBACK`.

Orden de bloqueo: `req_almacen` → transferencia original → `productoalmacen` ordenado → inserciones. Ante el error 1213 (interbloqueo), reintentar todo.

### Aptitud para una sola transacción (hecho)

Cabe: todas las tablas son InnoDB y ningún objeto del flujo hace commit implícito. Lo que complica: `GuardaDetalleSalida` falla en silencio, varios triggers dejan stock en NULL en silencio, y el trigger de `movimientostock` hace un recorrido completo.

### Diferencias con el C# actual (`FrmTPenPedido.cs`)

- **Orden y atomicidad:** `anular()` se confirma primero (`:274`) en su propia conexión; el extorno va después en dos `TransactionScope` separados (`:382`, `:436`) y `Aprobar` queda fuera de ambos.
- **Precios recalculados** con `UltimoPrecioCompraProducto` (`:579`): 204 de 3.040 líneas de extorno difieren en precio y valoreal de la original. Debe copiarse del detalle original: cantidad, unidad, precio, subtotal, descuentos, igv, importe, precioreal, valoreal, valorpromedio, proveedor, `id_det_req_almacen`; y de la cabecera: moneda, tipo de cambio, bruto, igv, total.
- **Estado de formulario que nunca se reinicia (hecho):** `detalle` (`:33`) no se limpia y `bandera` (`:53`) nunca vuelve a `true`. Tres extornos (13223, 15204, 17014) llevan líneas de otro requerimiento. Si `admNS.insert` falla, `bandera` sigue en `true` y se crea la nota de ingreso igual (`:407-412`).
- **El estado se decide con la fila de la grilla**, que puede estar desactualizada.
- **Varios extornos para una misma original (hecho):** 6 originales con 2 extornos y 1 con 3 (req 10785). El listado de transferencias aprobadas no excluye las ya extornadas, y las notas de crédito parciales (`frmNotadeCredito:1481-1516`) crean extornos sin anular el requerimiento.
- Inconsistencia: `frmPedidosPendientes.cs:259` pone `codReqAlm` en el extorno y `FrmTPenPedido` no (266 extornos tienen `id_req_almacen`).

## 3. Defectos en los procedures y triggers

| # | Objeto | Consecuencia | Verificación |
|---|---|---|---|
| 1 | Todos los SP de la sección 1 y el trigger de aprobación, sin `estado<>12` | Pisan requerimientos anulados | 6 casos; 4 latentes |
| 2 | `GuardaDetalleIngreso`: `fac INT` | Kardex con factor redondeado | 1.030 de 1.031 líneas con factor no entero |
| 3 | `RetornandoStock...`: cantidad `decimal` sin escala | Devuelve cantidades redondeadas | Req 6456: 0,5 × factor 100 devolvió 100 en vez de 50; 22 detalles fraccionarios en anulados tipo 2 |
| 4 | `GuardaDetalleSalida`: compara sin factor y devuelve `newid=0` sin error | Deja pasar salidas sin stock o rechaza en silencio | Extorno 15204: 8 unidades × 5,95 = 47,6 contra stock 14,85, y el chequeo pasa |
| 5 | `SELECT ... INTO fac` sin fila deja `fac` en NULL | `stockdisponible` o `stockactual` en NULL | 14 filas de `productoalmacen` con NULL; 93 líneas de transferencia sin fila de factor |
| 6 | Asimetría de factor y precisión entre salida e ingreso | Distorsiona `soles` y `valorpromedio` | 222 de 3.041 líneas de extorno con factor distinto de 1 |
| 7 | `ListarCodTransferenciasAprobadasDeReqAlmacen` no excluye originales ya extornadas | Extornos duplicados o bloqueo "más de una transferencia" | 6 originales ×2, 1 ×3; 266 extornos con `id_req_almacen` |
| 8 | `AnularRequerimientoAlmacen` sin guarda | Reanular sobrescribe fecha y usuario | Req 10785 (hipótesis) |
| 9 | `RechazarTransferencia` y `AprobarTransferencia` sin validar estado | Rechazar una aprobada no revierte stock; aprobar una rechazada mueve stock | 2 transferencias con `estado=0` y `pendiente=0` (19720, 19721) |
| 10 | Triggers con tipo de cambio sin `LIMIT` | Error 1242 aborta la nota de ingreso | 3 fechas duplicadas (2020), latente |
| 11 | Trigger de extorno: `NOT EXISTS factura_venta` ignora `anulado` | Puede no liberar la reserva | No verificado |
| 12 | `RetornandoStock...` con cantidad 0 | Ruido en `movimientostock` | 6.871 entradas en 0 |
| 13 | Sin `HANDLER` ni `ROLLBACK` en los SP del flujo | El servicio no recibe señal de fallo | Código |

### Los 18 extornos huérfanos (sin nota de salida ni de ingreso, `pendiente=1`)

9308, 10550, 12342, 12624, 13223, 13531, 15204, 16007, 16354, 17013, 17014, 18439, 18717, 18795, 19199, 19732, 19734 y 20024.

**Hecho:** casi todos se explican por el defecto 4 (falta de stock mal medida), por `bandera` y `detalle` que no se reinician en el C#, o por intentos repetidos permitidos por el defecto 7.

## 4. Implicaciones para el servicio nuevo

1. El servicio debe leer el estado de la BD dentro de la transacción (`FOR UPDATE`), nunca de la vista.
2. Debe validar stock con el factor de unidad antes de llamar a `GuardaDetalleSalida` y tratar `newid = 0` o NULL como fallo con rollback.
3. El extorno debe copiar valores de la original, no recalcularlos.
4. Debe elegir la original vigente sin extorno con `NOT EXISTS` (ya cubierto por `tiene_extorno` en `ReqVentaConsultas`).
5. Que otros flujos (`frmDespacho`, `frmEntrega`, notas de crédito) puedan pisar el estado 12 **no se resuelve con este servicio**: requiere decidir si se agrega una guarda `estado<>12` en esos procedures o en sus puntos de llamada.
