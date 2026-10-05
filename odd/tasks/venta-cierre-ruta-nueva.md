# venta-cierre-ruta-nueva

Estado: **PLAN APROBADO, SIN IMPLEMENTAR** (2026-10-05). Ninguna tarea iniciada.
Espejo Engram: tópico `odd/venta-cierre-ruta-nueva/tasks` (proyecto `sigefa_legacy_inverse`).
Contrato de procedures: `docs/venta-cierre/contrato-sp.md`. Cuerpos SQL: `docs/venta-cierre/referencia-sql/`.

## Objetivo

Crear una ruta **nueva y paralela** para cerrar una venta en `frmVenta2019`, sin borrar ni modificar el flujo actual, con:
1. Errores exactos de MySQL (procedure, paso, ítem, `Number`, `SqlState`, mensaje).
2. Una transacción explícita (`MySqlConnection.BeginTransaction`), sin `TransactionScope`.
3. `SELECT ... FOR UPDATE` en `serie` y `productoalmacen`, en orden estable de `productoId`.
4. Un diálogo modal tipo tasklist que ejecuta paso por paso en un hilo de fondo.
5. En contado: capturar los pagos en memoria y persistir venta + pagos en una sola transacción.

## Problema (evidencia)

- `SIGEFA.InterMySql/MysqlFacturaVenta.cs:144` abre `new TransactionScope()` sin opciones; el timeout por defecto es 60 s. Al vencer, Connector/NET lanza `TransactionAbortedException` sin `InnerException`; el `catch` (`:350`) solo atrapa `MySqlException`; `clsAdmFacturaVenta` muestra solo `ex.Message` ("Se anuló la transacción"). Estado: **verificado en código y docs**; el umbral exacto de ~11 ítems **no está medido**.
- Costo medido en producción: ~1,1 s por ítem solo en recorridos completos de tabla (`notasalida.documentoreferencia` sin índice, trigger de `movimientostock`). Esperas de bloqueo de hasta 51 s registradas (`Innodb_row_lock_waits`).
- Flujo real (`frmVenta2019.cs:3492-3703`): **un comprobante por almacén**; crédito llama `insertComprobante` sin cobro; contado abre `frmCancelarPago` por almacén. Si un bloque posterior falla, los anteriores se compensan con `AnulandoVentaEnTryCatchGeneracionVenta`.

## Restricciones (obligatorias para todo agente)

1. **MySQL 5.7**. Prohibido `SKIP LOCKED`, `NOWAIT`, CTE, funciones de ventana.
2. **net461**, `LangVersion latest`. Sin upgrade. Prohibido `record`, `init`, `Span`; tuplas solo si se agrega `System.ValueTuple`. Async/await, `Task.Run` e `IProgress<T>` sí.
3. **No modificar ni borrar la ruta vieja.** Todo cambio en `frmVenta2019.cs` y `frmCancelarPago.cs` queda detrás del flag `VentaCierreRuta` (`appSettings`, valores `nueva` | `legacy`, por defecto `legacy`).
4. **Nombres**: camelCase de dominio (`almacenId`, `productoId`, `facturaVentaId`, `serieId`). Prohibido `codAlmacen`/`codProducto`. Los nombres de parámetros SQL se respetan tal cual.
5. **Comentarios en español neutro** explicando qué hace cada paso y por qué.
6. Sin DI container. Sin `static` para estado de venta (el MDI comparte proceso). Sin `throw ex;` (usar `throw;`).
7. Nunca incluir credenciales en código, documentos ni commits.
8. Commits: Conventional Commits, **sin `Co-Authored-By` ni atribución de IA** (regla global del usuario).
9. No hay runner de pruebas en el proyecto (AGENTS.md: "No tests"): **TDD no disponible**; la verificación funcional es compilar en la VM Windows y humo manual. Quien implemente no puede compilar en Linux; no debe declarar "compila" sin evidencia.
10. Un solo escritor a la vez por archivo. Push, PR y merge son decisión del usuario.

## Reparto por agente

| Agente | Tareas | Rol |
|---|---|---|
| **opencode** | T1, T4, T5 | Código acotado y mecánico: DTOs, diálogo, integración de crédito |
| **antigravity** | T2, T3, T6 | Núcleo transaccional y modo captura (lo más delicado) |
| **codex** | RV-A, RV-B | Revisión independiente de solo lectura (no escribe código) |
| **claude** | T7 (+ orquestación) | Compilar en la VM, revisión final con codex, corrección acotada |

Orden y paralelismo (planificado, requiere OK del usuario para worktrees):

```
T1 (opencode, 8) ─┬─> T2→T3 (antigravity, 20) ──> RV-A (codex, 5) ─┐
                  └─> T4 (opencode, 10, worktree aparte)           ├─> T5 (opencode, 4) ─> T6 (antigravity, 12) ─> RV-B (codex, 5) ─> T7 (claude, 6)
```

T4 toca solo `frmVentaCierreProgreso*`; T2/T3 tocan solo `VentaCierre/*`. No comparten archivos. Si el usuario no aprueba worktrees, se ejecuta en serie y el total sube a ~70 min (el corte limpio es T1–T5: cubre el caso del error reportado).

## Tareas

Cada tarea cierra con un commit de unidad de trabajo (Conventional Commit) y registra aquí su hash y la evidencia observada. Casillas sin marcar: nada implementado.

### Pre-requisito P0 (quien tome T1)
- [x] Rama `feat/venta-cierre-ruta-nueva` creada desde `main` (2026-10-05). Todo commit va en esta rama; nunca en `main`.
- [ ] Confirmar en `SIGEFA.csproj` (SDK-style) que los `.cs` nuevos se incluyen por globbing; si no, agregarlos.

### T1 — Contrato (opencode, ~8 min)
Archivos nuevos en `SIGEFA.Administradores/VentaCierre/`:
- `VentaCierrePaso.cs`: enum `abrirTransaccion, bloquearSerie, bloquearStock, guardarCabecera, guardarDetalle, guardarPago, confirmar` y método de apoyo con el nombre legible en español.
- `VentaCierreProgreso.cs`: DTO inmutable (`paso`, `itemActual`, `totalItems`, `bloqueActual`, `totalBloques`, `almacenNombre`, `mensaje`).
- `VentaCierreResultado.cs`: `facturaVentaId`, `numeroDocumento`, `pagoIds`, duración por paso (ms).
- `VentaCierreException.cs`: `paso`, `procedimiento`, `itemIndice`, `productoId`, `mysqlNumero`, `sqlState`, `mysqlMensaje`, `parametros`; conserva la causa como `InnerException`.
- `BorradorPago.cs`: copia en memoria de los datos de un pago (los campos de `clsPago` usados por `GuardaPago`).
- Aceptación: sin lógica de negocio ni acceso a datos; todas las clases con comentarios; nombres camelCase de dominio.

### T2 — Repositorio (antigravity, ~10 min)
`SIGEFA.InterMySql/VentaCierre/VentaCierreRepositorio.cs`. Cada método recibe `(MySqlConnection conexion, MySqlTransaction transaccion, ...)` y no abre conexión propia:
- `bloquearSerie(serieId)`: `SELECT numeracion FROM serie WHERE codSerie=@serieId FOR UPDATE`. Debe ser la primera lectura de la transacción.
- `bloquearStock(almacenId, productoIds)`: leer `codProductoAlmacen` y luego `SELECT ... FOR UPDATE` por clave primaria, ordenado por `productoId` ascendente y sin duplicados; validar `stockactual`/`stockdisponible`.
- `guardarFacturaVenta`, `guardarDetalle`, `guardarPago`: parámetros **exactos** de `docs/venta-cierre/contrato-sp.md` (sin `entregado_ex`). Mapear `newid`: cabecera inválida → `CabeceraNoCreada`; detalle `-1` → `StockInsuficiente`; `NULL` → `DetalleSinFactura`; pago `0` filas → `PagoNoCreado`.
- Cada método envuelve su `ExecuteNonQuery` en `try/catch (MySqlException)` y lanza `VentaCierreException` con procedure, ítem, producto, `Number`, `SqlState` y `Message` exactos. Parámetros registrados sin secretos.
- Aceptación: ninguna llamada a `MessageBox`; ningún `throw ex;`; cada SP con su comentario de qué espera y qué devuelve.

### T3 — Servicio (antigravity, ~10 min)
`SIGEFA.Administradores/VentaCierre/VentaCierreService.cs`:
- `ejecutarBloque(datos, progreso)`: abre `MySqlConnection` propia, `BeginTransaction(IsolationLevel.RepeatableRead)`, ejecuta pasos en este orden: bloquearSerie → bloquearStock → guardarCabecera → guardarDetalle × N (informa `itemActual`) → guardarPago × M (solo si hay `BorradorPago`; en crédito no hay pagos) → `Commit`.
- Ante cualquier excepción: `Rollback()` explícito (en 5.7 el 1205 revierte solo la sentencia); registrar el error en una conexión **aparte** después del rollback; relanzar con `throw;` o envolver en `VentaCierreException` conservando la causa.
- `ejecutarOrden(bloques, progreso)`: recorre un bloque por almacén (una transacción por bloque). Si el bloque k falla, compensa los bloques ya confirmados llamando a la anulación **existente** (`AnulandoVentaEnTryCatchGeneracionVenta` / `ValidaAnulacionVenta`); no se reescribe la anulación.
- SUNAT, impresión y despacho **no** forman parte de la transacción (siguen después del commit, como hoy).
- Aceptación: sin referencias a `System.Windows.Forms`; dependencias (repositorio, anulación) recibidas por constructor simple, sin contenedor.

### RV-A — Revisión de T2+T3 (codex, solo lectura, ~5 min)
Revisar el diff de T2 y T3 contra `docs/venta-cierre/contrato-sp.md`: orden y tipo de parámetros, orden de bloqueo, rollback en todos los caminos, ausencia de `TransactionScope`, `throw;`, nombres. Entregar lista de hallazgos con archivo:línea; **no editar**. Correcciones las hace antigravity (una ronda).

### T4 — Diálogo tasklist (opencode, ~10 min, worktree aparte si se aprueba)
`SIGEFA.Formularios/frmVentaCierreProgreso.cs` (+ `.Designer.cs`):
- Modal con `FormBorderStyle=FixedDialog`, `ControlBox=false`, `ShowInTaskbar=false`; sin `CancelButton`; `FormClosing` con `e.Cancel = true` mientras no haya terminado (cubre X, Alt+F4 y `Close()`).
- Lista de pasos con estado (pendiente, en curso, listo, error), encabezado "Bloque k/n (almacén)", y avance por ítem.
- Ejecuta `await Task.Run(() => servicio.ejecutarOrden(...))` con `Progress<VentaCierreProgreso>` creado en el hilo de UI. Al error muestra **tal cual** paso, procedimiento, ítem, número y mensaje de MySQL, con botón "Copiar detalle" y botón "Cerrar" (habilitado solo al terminar).
- Sin rediseño estético del resto del sistema. Depende solo de T1.

### T5 — Integración de crédito (opencode, ~4 min)
`frmVenta2019.cs` en la rama de crédito (`:3590`): `if (VentaCierreRuta == "nueva") { ... frmVentaCierreProgreso ... } else { AdmVenta.insertComprobante(this.venta) ... }`. La rama `else` es el código actual **sin cambios**. Reutilizar `lista_facturas` y el `catch` existente para la compensación.
- Aceptación: con el flag en `legacy` o ausente el comportamiento es idéntico.

### T6 — Modo captura en contado (antigravity, ~12 min)
`frmCancelarPago.cs`, solo con el flag `nueva` y solo para efectivo, tarjeta, banco y cheque:
- `:636`: no llamar `insertComprobante`. `:648`: no recargar la venta desde la base. `:1258`: agregar un `BorradorPago` (copia de `Pag`) a la lista en memoria en vez de `Admpag.insert`. `:1264`: omitir `cargaPago`. `:1066`: `CodNota` se asigna en el servicio tras crear la cabecera. `:2131` y `:2140` (impresión con `Pag.CodPago`): imprimir después del commit.
- Cuando `txtMontoPendiente == 0`, devolver la lista de borradores y ejecutar el servicio (con el diálogo de T4) en **una** transacción.
- Métodos no soportados en modo captura (nota de crédito 10, pendiente 12, letras `tipo == 100`): mostrar aviso y usar el flujo viejo para esa venta; no mezclar borradores con persistencia directa.
- Si el cajero cancela, no hay nada que anular (la venta aún no existe).
- Aceptación: flag `legacy` o ausente → comportamiento idéntico al actual.

### RV-B — Revisión de T4+T5+T6 (codex, solo lectura, ~5 min)
Revisar que la ruta vieja no cambió (diff solo agrega ramas detrás del flag), que el diálogo no se puede cerrar antes de terminar, que los borradores no se persisten por ninguna rama no soportada y que no hay `static` de estado de venta.

### T7 — Compilación y revisión final (claude, ~6 min)
- [ ] En la VM Windows (`C:\Users\qemu\Documents\sigefa_legacy`): el árbol tiene 4 archivos modificados (`frmLogin.cs`, `MysqlEmpresa.cs`, `MysqlSucursal.cs`, `MysqlUsuario.cs`) y 2 sin seguimiento. Antes de traer la rama: inspeccionar esas diferencias y preservarlas (stash o confirmar con el usuario); no sobrescribir.
- [ ] Traer la rama (requiere que el usuario la haya subido al remoto) y compilar con MSBuild 18.6 (`Debug|x86`). Errores de compilación: corregir en una ronda acotada; declarar cualquier error restante.
- [ ] Revisión final con codex sobre el diff completo; una ronda de corrección máxima.
- [ ] Informe: qué se verificó, qué no (humo manual en UI queda para el usuario), riesgos abiertos.
- Los datos de acceso a la VM viven en `try-print-go/usqay-print-client/windows-test.yaml` (ignorado por git); nunca se imprimen ni se copian.

## Protocolo de traspaso entre agentes (obligatorio)

Cada agente, al empezar: `mem_context` → `mem_search "venta-cierre-ruta-nueva"` → leer este archivo y `docs/venta-cierre/contrato-sp.md`. Trabaja **solo** en los archivos de su tarea. Al terminar: commit convencional, marcar la casilla con hash y evidencia, actualizar el espejo Engram (`mem_update` del tópico `odd/venta-cierre-ruta-nueva/tasks`) y **detenerse**. No toma tareas de otro agente. Si la tarea exige tocar un archivo fuera de su alcance, se detiene y lo reporta.

Prompt de arranque (ejemplo para el agente de T2): "Implementa la tarea T2 de `odd/tasks/venta-cierre-ruta-nueva.md` respetando sus restricciones y `docs/venta-cierre/contrato-sp.md`. Solo los archivos de T2. No compiles en Linux ni declares que compila. Al terminar haz el commit, marca la casilla, actualiza Engram y detente."

## Riesgos y decisiones

- **Compilación tardía:** nadie compila hasta T7 y el proyecto no se puede compilar en Linux. Los errores se acumulan. Mitigación opcional: un punto de control de compilación en la VM tras T3 (requiere subir la rama).
- **Modal dentro de modal** (`frmVentaCierreProgreso` sobre `frmCancelarPago`): debería funcionar con `ShowDialog(this)`; solo se confirma al compilar y probar en la VM.
- **Cuerpos de SP/triggers**: leídos de la base local; la igualdad con producción no está verificada.
- **Deadlocks con la ruta vieja:** el orden estable solo protege entre ventas de la ruta nueva. Un 1213 debe mostrarse con el mensaje exacto; el reintento automático es decisión del usuario.
- **Índices** (`notasalida(documentoreferencia, codNotaSalida)`, `movimientostock(codProducto, codAlmacen, tipoStock, fechaRegistro, codMovStock)`): fuera de alcance; cambian el esquema compartido y requieren ventana de mantenimiento. Decisión del usuario.
- **Tamaño y entrega (decidido por el usuario, 2026-10-05):** pronóstico ~1 200 líneas autoradas (> 400). **Una sola rama `feat/venta-cierre-ruta-nueva`; todas las tareas se commitean ahí. Nada pasa a `main` hasta que el compilado se pruebe y funcione.** No hay PR por tarea. Push, PR y merge a `main` siguen siendo decisión del usuario.

## Rutas de ejecución (declaración)

Planificado, sin ejecutar: T1, T4, T5 → agente opencode (delegado, escritor único); T2, T3, T6 → antigravity (delegado); RV-A, RV-B → codex (solo lectura); T7 → claude (compilación y revisión). Disparadores: escritura de ≥2 archivos no triviales por tarea y mapeo previo ya realizado por un subagente de investigación.
