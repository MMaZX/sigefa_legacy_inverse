# Documentación Técnica: Proceso de Anulación de Venta en SIGEFA

Este documento detalla el flujo de ejecución, las comprobaciones previas, la transacción de base de datos, los Stored Procedures invocados, las variables requeridas y las tablas de MySQL afectadas al realizar la **Anulación de una Venta** en el sistema SIGEFA (`frmVentas.cs` -> `btnAnular_Click`).

---

## 1. Flujo General del Proceso

El proceso de anulación de una venta involucra 4 etapas secuenciales:

1. **Pre-validaciones y Autorización:** Se valida que la venta exista, no esté anulada previamente, no posea Nota de Crédito activa, no tenga entregas de despacho pendientes y que el usuario cuente con los permisos requeridos.
2. **Core Transaccional (`TransactionScope`):**
   - Anulación del registro de la venta en base de datos.
   - Anulación de los pagos/cobros asociados a la venta.
   - Registro de un **Documento de Ingreso a Almacén (DIA)** y su detalle para devolver el stock de los productos vendidos al almacén origen.
3. **Anulación de Despachos y Requerimientos:**
   - Anulación del registro de despacho relacionado.
   - Anulación del requerimiento de almacén generado para el pedido de la venta.
4. **Extornación de Transferencias:**
   - Si existía una transferencia entre almacenes aprobada para la venta, se genera y aprueba automáticamente un **Documento de Transferencia de Extornación (DET)** devolviendo los productos al almacén despachador.

---

## 2. Variables y Parámetros Requeridos

Para completar la anulación de forma correcta, se requieren las siguientes variables y parámetros de entrada:

| Variable | Tipo | Origen | Descripción |
| :--- | :--- | :--- | :--- |
| `codFacturaVenta` | `INT` | `dgvVentas1.CurrentRow` / DB | Código identificador primario de la venta (`codFacturaVenta`). |
| `codAlmacen` | `INT` | `cmbAlmacenes.SelectedValue` | Código del almacén desde donde se emitió la venta. |
| `codUsuario` | `INT` | `usuario_click.CodUsuario` | Código del usuario que autoriza y ejecuta la anulación. |
| `frmLogin.iCodSucursal` | `INT` | Sesión global | Código de la sucursal activa del usuario. |
| `frmLogin.iCodAlmacen` | `INT` | Sesión global | Código de almacén asignado al usuario en sesión. |

---

## 3. Stored Procedures Ejecutados por Etapa

### Etapa 1: Consultas y Validaciones Previas
Antes de modificar los datos, el sistema ejecuta los siguientes procedimientos almacenados para verificar el estado de la venta:

1. **`CargaFacturaVenta`**
   - **Parámetros:** `codventa` (`INT`)
   - **Propósito:** Carga la entidad `clsFacturaVenta` para verificar `Anulado == 0`, `TieneNotaCredito == 'N'`, `CodTipoDocumento`, etc.
2. **`VerificaEntregasActivasPorDocumRelacionadoDeDespacho`**
   - **Parámetros:** `_tipoDoc` (`INT` = 1), `_codDoc` (`INT` = `codFacturaVenta`)
   - **Propósito:** Comprueba si la venta tiene entregas de despacho activas en tránsito o pendientes.
3. **`ValidaAnulacionVenta`**
   - **Parámetros:** `codventa` (`INT`)
   - **Propósito:** Valida reglas de negocio adicionales antes de anular (ej. cierres de caja o periodos contables).

---

### Etapa 2: Transacción Core de Anulación (DENTRO DE `TransactionScope`)

Todas las operaciones descritas a continuación se ejecutan dentro de una transacción explícita (`using (TransactionScope Scope = new TransactionScope())`). Si falla cualquiera de ellas, se realiza un `Rollback()`.

#### 2.1 Anulación de la Venta
* **Stored Procedure:** `AnularFacturaVenta`
* **Parámetros:**
  * `codventa` (`INT`) -> Código de la venta a anular.
  * `_codUsuario` (`INT`) -> Código del usuario autorizador.
* **Acción en BD:** Actualiza el campo `anulado = 1` o cambia el estado del registro en la tabla `facturaventa`.

#### 2.2 Anulación de Cobros y Pagos Registrados
* **Stored Procedure de Consulta:** `GetPagosVenta` (o equivalente en `clsAdmPago`)
  * **Parámetros:** `codAlmacen` (`INT`), `codVenta` (`INT`)
  * **Retorna:** `DataTable` con los IDs de pago asociados.
* **Stored Procedure de Anulación:** `AnularPago`
  * **Parámetros:** `codPago` (`INT`)
  * **Acción en BD:** Marca los pagos/cobros de la venta como anulados en la tabla `pago`.

#### 2.3 Devolución de Stock (Registro de Nota de Ingreso / DIA)
Para retornar la mercadería al inventario:
1. **`BuscaNotaSalida`**
   * **Parámetros:** `codFacturaVenta` (`INT`), `codAlmacen` (`INT`)
   * **Propósito:** Obtiene la nota de salida original asociada a la venta.
2. **`MuestraTransaccion`**
   * **Parámetros:** `codTransaccion` (`INT` = 11) -> Tipo de transacción para devolución por anulación.
3. **`BuscaTipoDocumento`**
   * **Parámetros:** `sigla` (`VARCHAR` = "DIA") -> Documento Ingreso Almacén.
4. **`BuscaSeriexDocumento`**
   * **Parámetros:** `codTipoDocumento` (`INT`), `codAlmacen` (`INT`) -> Obtiene la serie correlativa activa.
5. **`insertNotaIngreso` / `InsertNotaIngreso`**
   * **Parámetros:** Datos cabecera de la `clsNotaIngreso` (Almacén, Transacción DIA, Serie, NumDoc, Cliente, etc.).
   * **Acción en BD:** Crea la cabecera de la nota de ingreso por anulación.
6. **`CargaDetalleNotaSalida`**
   * **Parámetros:** `codNotaSalida` (`INT`), `codAlmacen` (`INT`)
   * **Propósito:** Lee los ítems y cantidades despachados originalmente.
7. **`insertDetalleNotaIngreso` / `InsertDetalleNotaIngreso`** (por cada ítem)
   * **Parámetros:** `codNotaIngreso`, `codProducto`, `cantidad`, `precio`, `subtotal`, `igv`, `total`.
   * **Acción en BD:** Registra el detalle del ingreso y **reincrementa el stock físico y disponible** de cada producto en la tabla `productoalmacen` y registra el movimiento en el Kardex.

---

### Etapa 3: Post-Transacción (Despachos y Requerimientos de Almacén)

Una vez confirmada (`Commit`) la transacción principal:

1. **`MuestraDespachoSegunDocRelacionado`**
   * **Parámetros:** `_tipoDoc` (`INT` = 1), `_codDoc` (`INT` = `codFacturaVenta`)
   * **Propósito:** Recupera el objeto `clsDespacho` vinculado a la venta.
2. **`AnulacionDespacho`**
   * **Parámetros:** `codDespacho` (`INT`)
   * **Acción en BD:** Desactiva/anula la orden de despacho en la tabla `despacho`.
3. **`CargaRequerimientosSegun`**
   * **Parámetros:** `codPedido` (`INT`), `codAlmacen` (`INT`), `estado` (`INT` = -1)
   * **Propósito:** Localiza la orden de requerimiento de almacén (`clsRequerimientoAlmacen`) originada por la venta.
4. **`cargaTransferenciasAprobadas`**
   * **Parámetros:** `codReqAlm` (`INT`)
   * **Propósito:** Verifica si existía una transferencia de mercadería previa entre almacenes para atender el pedido.
5. **`AnularRequerimientoAlmacen`**
   * **Parámetros:** `codReqAlm` (`INT`), `codUsuario` (`INT`)
   * **Acción en BD:** Anula la solicitud en la tabla `requerimientoalmacen`.

---

### Etapa 4: Extornación de Transferencias entre Almacenes (Si Aplica)

Si el requerimiento de almacén tuvo una transferencia aprobada:

1. **`CargaTransferencia`**
   * **Parámetros:** `codTransDir` (`INT`)
2. **`insertTransferencia`**
   * **Parámetros:** Datos cabecera del documento de extornación (Tipo `"DET"`, Almacén Origen = Almacén Solicitante, Almacén Destino = Almacén Despachador).
3. **`insertDetalleTransferencia`** (por cada producto)
   * **Parámetros:** `codTransDir`, `codProducto`, `cantidad`, etc.
4. **Aprobación del Extorno (`apruebaTransferencia`):**
   * Ajusta los saldos en los almacenes involucrados devolviendo los productos al almacén despachador original.

---

## 4. Tablas Principales Afectadas en MySQL

Durante el proceso de anulación de una venta, la base de datos MySQL sufre modificaciones en las siguientes tablas:

| Tabla | Campo / Operación Modificada | Descripción |
| :--- | :--- | :--- |
| `facturaventa` | `UPDATE (anulado = 1, fechaanulado, codusuarioanula)` | Marca la factura/boleta de venta como anulada. |
| `pago` / `cobro` | `UPDATE (estado = 0 / anulado = 1)` | Anula los recibos o registros de cobro asociados. |
| `notaingreso` | `INSERT` | Registra el documento de devolución (DIA) de mercadería. |
| `detallenotaingreso` | `INSERT` | Registra cada producto devuelto. |
| `productoalmacen` | `UPDATE (stockactual = stockactual + cantidad)` | Reincrementa el inventario del almacén. |
| `kardex` | `INSERT` | Registra el movimiento de ingreso de inventario por anulación. |
| `despacho` | `UPDATE (estado = 0 / anulado = 1)` | Anula la orden de despacho asociada. |
| `requerimientoalmacen` | `UPDATE (estado = 0 / anulado = 1)` | Anula el requerimiento entre almacenes. |
| `transferencia` | `INSERT` | Registra la transferencia de extorno si la venta movió stock entre almacenes. |

---

## 5. Requisitos para una Eliminación/Anulación Correcta

Para evitar fallos o excepciones (`NullReferenceException`, inconsistencias en Kardex o transacciones abortadas):

1. **Garantizar `codFacturaVenta > 0`:** El código debe obtener correctamente el ID de la venta seleccionada en la grilla y no enviar un valor `0` a `CargaFacturaVenta`.
2. **No poseer Nota de Crédito SUNAT emitida:** Si la venta ya fue enviada a SUNAT o cuenta con Nota de Crédito, la anulación directa en BD es bloqueada y debe canalizarse mediante el módulo de Nota de Crédito (`frmNotadeCredito`).
3. **No tener entregas activas en despacho:** No debe haber entregas parciales pendientes en tránsito.
4. **Ejecución dentro de una Transacción (`TransactionScope`):** Debe mantenerse el bloque de transacción para que, si falla la devolución de stock o anulación de pago, no queden datos huérfanos.
5. **Permisos de Usuario:** El usuario autorizador (`usuario_click`) debe tener asignado el permiso de anulación de ventas (`getPermisoAnularVentas()`).
