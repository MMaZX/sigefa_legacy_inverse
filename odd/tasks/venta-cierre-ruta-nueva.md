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
| **codex** | RV-A, RV-B | Revisión independiente de solo lectura (no escribe código); la lanza y coordina claude |
| **claude** | B1, T7 (+ orquestación) | Compilar en la VM Windows por SSH (punto de control B1 tras T3 y build final en T7), coordinar a codex, corrección acotada |

Orden (decidido por el usuario: en serie, en una sola rama; el worktree para T4 NO fue aprobado):

```
T1 (opencode) ─> T2→T3 (antigravity) ─> [RV-A (codex, vía claude) + B1 (claude, build en VM)] ─> correcciones (antigravity, 1 ronda)
  ─> T4 (opencode) ─> T5 (opencode) ─> T6 (antigravity) ─> [RV-B (codex, vía claude)] ─> T7 (claude, build final + revisión final)
```

**Punto de control B1 (decidido por el usuario, 2026-10-05):** tras T3 se compila en la VM antes de seguir, para detectar errores de compilación temprano (T1 usa namespace de archivo, C# 10; solo el build lo confirma). RV-A y B1 se ejecutan juntos; los hallazgos y errores vuelven a antigravity en una sola ronda de corrección.

T4 toca solo `frmVentaCierreProgreso*`; T2/T3 tocan solo `VentaCierre/*`. No comparten archivos. Si el usuario no aprueba worktrees, se ejecuta en serie y el total sube a ~70 min (el corte limpio es T1–T5: cubre el caso del error reportado).

## Tareas

Cada tarea cierra con un commit de unidad de trabajo (Conventional Commit) y registra aquí su hash y la evidencia observada. Casillas sin marcar: nada implementado.

### Pre-requisito P0 (quien tome T1)
- [x] Rama `feat/venta-cierre-ruta-nueva` creada desde `main` (2026-10-05). Todo commit va en esta rama; nunca en `main`.
- [x] Confirmado en `SIGEFA.csproj` (SDK-style, 2026-10-05): sin `Compile Include`, solo `Remove`; los `.cs` nuevos se incluyen por globbing, nada que agregar.

### T1 — Contrato (opencode, ~8 min)
Archivos nuevos en `SIGEFA.Administradores/VentaCierre/`:
- `VentaCierrePaso.cs`: enum `abrirTransaccion, bloquearSerie, bloquearStock, guardarCabecera, guardarDetalle, guardarPago, confirmar` y método de apoyo con el nombre legible en español.
- `VentaCierreProgreso.cs`: DTO inmutable (`paso`, `itemActual`, `totalItems`, `bloqueActual`, `totalBloques`, `almacenNombre`, `mensaje`).
- `VentaCierreResultado.cs`: `facturaVentaId`, `numeroDocumento`, `pagoIds`, duración por paso (ms).
- `VentaCierreException.cs`: `paso`, `procedimiento`, `itemIndice`, `productoId`, `mysqlNumero`, `sqlState`, `mysqlMensaje`, `parametros`; conserva la causa como `InnerException`.
- `BorradorPago.cs`: copia en memoria de los datos de un pago (los campos de `clsPago` usados por `GuardaPago`).
- Aceptación: sin lógica de negocio ni acceso a datos; todas las clases con comentarios; nombres camelCase de dominio.
- [x] Implementada en `1fc20a3` (2026-10-05): 5 archivos en `SIGEFA.Administradores/VentaCierre/`; verificación funcional pendiente de compilación en la VM Windows (T7, no se compila en Linux).

### T2 — Repositorio (antigravity, ~10 min)
`SIGEFA.InterMySql/VentaCierre/VentaCierreRepositorio.cs`. Cada método recibe `(MySqlConnection conexion, MySqlTransaction transaccion, ...)` y no abre conexión propia:
- `bloquearSerie(serieId)`: `SELECT numeracion FROM serie WHERE codSerie=@serieId FOR UPDATE`. Debe ser la primera lectura de la transacción.
- `bloquearStock(almacenId, productoIds)`: leer `codProductoAlmacen` y luego `SELECT ... FOR UPDATE` por clave primaria, ordenado por `productoId` ascendente y sin duplicados; validar `stockactual`/`stockdisponible`.
- `guardarFacturaVenta`, `guardarDetalle`, `guardarPago`: parámetros **exactos** de `docs/venta-cierre/contrato-sp.md` (sin `entregado_ex`). Mapear `newid`: cabecera inválida → `CabeceraNoCreada`; detalle `-1` → `StockInsuficiente`; `NULL` → `DetalleSinFactura`; pago `0` filas → `PagoNoCreado`.
- Cada método envuelve su `ExecuteNonQuery` en `try/catch (MySqlException)` y lanza `VentaCierreException` con procedure, ítem, producto, `Number`, `SqlState` y `Message` exactos. Parámetros registrados sin secretos.
- Aceptación: ninguna llamada a `MessageBox`; ningún `throw ex;`; cada SP con su comentario de qué espera y qué devuelve.
- [x] Implementada en `7c0f619` (2026-10-05): `SIGEFA.InterMySql/VentaCierre/VentaCierreRepositorio.cs` con `bloquearSerie`, `bloquearStock` (orden estable ascendente de productoId y bloqueo por PK en productoalmacen), `guardarFacturaVenta` (55 params exactos), `guardarDetalle` (32 params exactos, mapeo newid: -1 StockInsuficiente, NULL DetalleSinFactura) y `guardarPago` (39 params exactos, 0 filas PagoNoCreado). Sin MessageBox, sin throw ex, envuelto en try/catch MySqlException hacia VentaCierreException.

### T3 — Servicio (antigravity, ~10 min)
`SIGEFA.Administradores/VentaCierre/VentaCierreService.cs`:
- `ejecutarBloque(datos, progreso)`: abre `MySqlConnection` propia, `BeginTransaction(IsolationLevel.RepeatableRead)`, ejecuta pasos en este orden: bloquearSerie → bloquearStock → guardarCabecera → guardarDetalle × N (informa `itemActual`) → guardarPago × M (solo si hay `BorradorPago`; en crédito no hay pagos) → `Commit`.
- Ante cualquier excepción: `Rollback()` explícito (en 5.7 el 1205 revierte solo la sentencia); registrar el error en una conexión **aparte** después del rollback; relanzar con `throw;` o envolver en `VentaCierreException` conservando la causa.
- `ejecutarOrden(bloques, progreso)`: recorre un bloque por almacén (una transacción por bloque). Si el bloque k falla, compensa los bloques ya confirmados llamando a la anulación **existente** (`AnulandoVentaEnTryCatchGeneracionVenta` / `ValidaAnulacionVenta`); no se reescribe la anulación.
- SUNAT, impresión y despacho **no** forman parte de la transacción (siguen después del commit, como hoy).
- Aceptación: sin referencias a `System.Windows.Forms`; dependencias (repositorio, anulación) recibidas por constructor simple, sin contenedor.
- [x] Implementada en `48c7b2a` (2026-10-05): `SIGEFA.Administradores/VentaCierre/VentaCierreService.cs` con `ejecutarBloque` (aislamiento RepeatableRead, orden estricto: serie -> stock -> cabecera -> detalle -> pago -> commit; rollback explícito ante cualquier excepción y registro en conexión aparte) y `ejecutarOrden` (recorrido multialmacén con compensación llamando a la anulación existente si falla el bloque k). Sin dependencias de System.Windows.Forms ni contenedor DI.

### RV-A — Revisión de T2+T3 (codex, solo lectura, ~5 min)
Revisar el diff de T2 y T3 contra `docs/venta-cierre/contrato-sp.md`: orden y tipo de parámetros, orden de bloqueo, rollback en todos los caminos, ausencia de `TransactionScope`, `throw;`, nombres. Entregar lista de hallazgos con archivo:línea; **no editar**. Correcciones las hace antigravity (una ronda).

### B1 — Build de control tras T3 (claude, ~8 min)
Mecanismo (decidido por el usuario): **por SSH a la VM, sin subir nada a GitHub**.
- [ ] En el host: `git bundle create` de la rama `feat/venta-cierre-ruta-nueva` (en el scratchpad de la sesión, no en el repo) y `scp` a la VM.
- [ ] En la VM: **no tocar** `C:\Users\qemu\Documents\sigefa_legacy` (tiene 4 archivos modificados). Importar el bundle en un directorio aparte, **hermano de `Debug_gr`** (el proyecto referencia `..\Debug_gr`; confirmar la ubicación antes), por ejemplo `C:\Users\qemu\Documents\sigefa_build`.
- [ ] Compilar `Debug|x86` con MSBuild 18.6 y registrar **todos** los errores (código, archivo, línea), no solo el primero. Compilar sin `-m` si hace falta para ver el orden de errores.
- [ ] Errores de T1–T3 vuelven a antigravity en una sola ronda. Los errores preexistentes del repo base (≈200 warnings, 0 errores según `build_warnings.md`) no son de esta rama.
- Los datos de acceso a la VM viven en `try-print-go/usqay-print-client/windows-test.yaml` (ignorado por git); nunca se imprimen, ni se escriben en commits o informes.

#### Resultado de B1 y RV-A (2026-10-05, sobre `4b00f12`)
- **B1 (build en la VM, 16 s):** 1 error único `CS0051` en `VentaCierreService.cs:63`: el constructor `public` recibe `clsAdmFacturaVenta`, que es `internal` (`clsAdmFacturaVenta.cs:11`). Los errores de accesibilidad se reportan en una fase temprana: **tras corregirlo puede haber más**; hay que recompilar.
- **RV-A (codex, solo lectura):** parámetros 55/32/39 correctos, sin duplicados ni `entregado_ex`; orden serie→stock correcto; sin `TransactionScope`, `throw ex;` ni UI directa. Hallazgos: (1) alta: la compensación de `ejecutarOrden` ignora el resultado/excepción de cada anulación (`VentaCierreService.cs:349-367`); (2) alta: el registro de error por defecto abre una conexión aparte pero no escribe nada (`:373-388`); (3) alta: `bloquearStock` lee la PK sin bloqueo y luego bloquea por PK sin revalidar almacén/producto (`VentaCierreRepositorio.cs:110-160`); (4) media: la anulación vía `clsAdmFacturaVenta` puede mostrar `MessageBox` (`:351-361`) — **aceptado como riesgo conocido**, se revisa en T5; (5) media: ids y número de documento se asignan a las entidades antes del `Commit` (`VentaCierreRepositorio.cs:320-322,452`); (6) baja: `DocumentoReferencia == ""` se envía como NULL (`:244`), el contrato dice solo si es null.
- **Ronda de corrección (antigravity, una sola):** CS0051, hallazgos 1, 2, 3, 5 y 6. Decisión para el hallazgo 2: registrar en un **archivo local** (`%LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log`), sin tocar el esquema de la base. Después, claude recompila (B1b).
  - [x] Corregido en `0fc589c` (2026-10-05): CS0051 resuelto con constructor `internal`; Hallazgo 1 reporta bloques sin compensar en la excepción conservando causa original; Hallazgo 2 escribe en `%LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log` sin abrir conexión extra; Hallazgo 3 revalida almacén/producto con `FOR UPDATE` en la misma lectura bloqueante; Hallazgo 5 asigna ids a entidades solo tras `Commit` y restaura al abortar; Hallazgo 6 preserva cadena vacía en `DocumentoReferencia` (solo `null` envía `DBNull.Value`).
  - [x] Microronda en `VentaCierreService` (`69d8786`, 2026-10-05): validación de colección Detalle (null o elemento null) dentro del flujo protegido reportando `VentaCierreException`; bloque confirmado sin venta o id registrado como no compensado con índice en excepción final; enmascaramiento de credenciales (`Pwd`/`Password`/`Uid`/`User Id`) en log local.

#### Verificación tras la corrección (claude, 2026-10-05)
- **B1b** sobre `9eb0553` y **B1c** sobre `409aafa` (incluye la microronda `69d8786`): MSBuild `Debug|x86` en la VM, `exit=0`, **0 errores**, 0 warnings en `VentaCierre`.
- **RV-A2 (codex, sobre `0fc589c`):** resueltos CS0051, bloqueo de stock, asignación tras `Commit` y `DocumentoReferencia`; parciales compensación y log, más un caso de detalle nulo → corregidos en la microronda `69d8786` y revisados por diff por claude (sin segunda pasada de codex, por tratarse de un solo archivo). Riesgo abierto aceptado: la anulación vía `clsAdmFacturaVenta` puede mostrar `MessageBox` (revisar en T5).
- Compilar no prueba funcionamiento: nada se ejecutó contra la base.

### T4 — Diálogo tasklist (opencode, ~10 min, en serie en esta rama)
`SIGEFA.Formularios/frmVentaCierreProgreso.cs` (+ `.Designer.cs`):
- Modal con `FormBorderStyle=FixedDialog`, `ControlBox=false`, `ShowInTaskbar=false`; sin `CancelButton`; `FormClosing` con `e.Cancel = true` mientras no haya terminado (cubre X, Alt+F4 y `Close()`).
- Lista de pasos con estado (pendiente, en curso, listo, error), encabezado "Bloque k/n (almacén)", y avance por ítem.
- Ejecuta `await Task.Run(() => servicio.ejecutarOrden(...))` con `Progress<VentaCierreProgreso>` creado en el hilo de UI. Al error muestra **tal cual** paso, procedimiento, ítem, número y mensaje de MySQL, con botón "Copiar detalle" y botón "Cerrar" (habilitado solo al terminar).
- Sin rediseño estético del resto del sistema. Depende solo de T1.
- [x] Implementada en `dad00a7` (2026-10-05): `SIGEFA.Formularios/frmVentaCierreProgreso.cs` + `.Designer.cs`; modal FixedDialog sin ControlBox ni CancelButton, cierre bloqueado hasta terminar, lista de 7 pasos con estado, encabezado Bloque k/n, `Task.Run(ejecutarOrden)` con `Progress` en UI, error MySQL tal cual con Copiar detalle; verificación funcional pendiente de compilación en la VM (T7, no se compila en Linux).

### T5 — Integración de crédito (opencode, ~4 min)
`frmVenta2019.cs` en la rama de crédito (`:3590`): `if (VentaCierreRuta == "nueva") { ... frmVentaCierreProgreso ... } else { AdmVenta.insertComprobante(this.venta) ... }`. La rama `else` es el código actual **sin cambios**. Reutilizar `lista_facturas` y el `catch` existente para la compensación.
- Aceptación: con el flag en `legacy` o ausente el comportamiento es idéntico.
- [x] Implementada en `2a79c22` (2026-10-05): rama `nueva` en `guardaVenta` (crédito) con bloque + `ShowDialog(this)`, mismo flujo posterior (CodVenta, lista_facturas, impresión, FE); `else` legacy idéntico salvo indentación (`git diff -w` solo muestra agregados); clave `VentaCierreRuta=legacy` en `app.config`. Nota: el diálogo expone `fueExitoso` pero no el mensaje exacto (T4 sin modificar), así que ante fallo se relanza un error legible y el detalle MySQL queda en el diálogo (ya mostrado con Copiar detalle); el catch existente compensa con `lista_facturas`. Verificación funcional pendiente de compilación en la VM (T7, no se compila en Linux).

### T6 — Modo captura en contado (antigravity, ~12 min)
`frmCancelarPago.cs`, solo con el flag `nueva` y solo para efectivo, tarjeta, banco y cheque:
- `:636`: no llamar `insertComprobante`. `:648`: no recargar la venta desde la base. `:1258`: agregar un `BorradorPago` (copia de `Pag`) a la lista en memoria en vez de `Admpag.insert`. `:1264`: omitir `cargaPago`. `:1066`: `CodNota` se asigna en el servicio tras crear la cabecera. `:2131` y `:2140` (impresión con `Pag.CodPago`): imprimir después del commit.
- Cuando `txtMontoPendiente == 0`, devolver la lista de borradores y ejecutar el servicio (con el diálogo de T4) en **una** transacción.
- Métodos no soportados en modo captura (nota de crédito 10, pendiente 12, letras `tipo == 100`): mostrar aviso y usar el flujo viejo para esa venta; no mezclar borradores con persistencia directa.
- Si el cajero cancela, no hay nada que anular (la venta aún no existe).
- [x] Implementada en `f4afdb3` y ajustada en `7aa4f81` (2026-10-05): modo captura en `SIGEFA.Formularios/frmCancelarPago.cs` acotado exclusivamente a efectivo (método 5; depósito 6, cheque 7, tarjeta 8 y transferencia 9 usan flujo legacy con aviso para conservar GuardaPagoPendiente); lista en memoria de `BorradorPago`, omisión de `insertComprobante`, `CargaFacturaVenta`, `cargaPago` e `insertPagoPendiente`; expresión original de `Pag.CodNota = venta.CodFacturaVenta.ToString()` restaurada en ruta legacy; cálculo y validación antes de agregar borrador con retiro en catch si ocurre error; protección contra impresión en modo captura; aviso al responder "No" a pagar restante; bloqueo de mezcla de borradores con persistencia directa. Integración en contado en `SIGEFA.Formularios/frmVenta2019.cs` conectando los borradores con `VentaCierreService` y `frmVentaCierreProgreso` en transacción única. Con flag legacy o ausente el comportamiento es idéntico. Verificación funcional pendiente de compilación en VM Windows (T7, no se compila en Linux).

### RV-B — Revisión de T4+T5+T6 (codex, solo lectura, ~5 min)
Revisar que la ruta vieja no cambió (diff solo agrega ramas detrás del flag), que el diálogo no se puede cerrar antes de terminar, que los borradores no se persisten por ninguna rama no soportada y que no hay `static` de estado de venta.

### T7 — Compilación y revisión final (claude, ~6 min)
- [ ] En la VM Windows (`C:\Users\qemu\Documents\sigefa_legacy`): el árbol tiene 4 archivos modificados (`frmLogin.cs`, `MysqlEmpresa.cs`, `MysqlSucursal.cs`, `MysqlUsuario.cs`) y 2 sin seguimiento. Antes de traer la rama: inspeccionar esas diferencias y preservarlas (stash o confirmar con el usuario); no sobrescribir.
- [ ] Llevar la rama con el mecanismo de B1 (`git bundle` + `scp`, sin subir a GitHub), compilar con MSBuild 18.6 (`Debug|x86`) en el directorio aparte. Errores de compilación: corregir en una ronda acotada; declarar cualquier error restante.
- [ ] Revisión final con codex sobre el diff completo; una ronda de corrección máxima.
- [ ] Informe: qué se verificó, qué no (humo manual en UI queda para el usuario), riesgos abiertos.
- Los datos de acceso a la VM viven en `try-print-go/usqay-print-client/windows-test.yaml` (ignorado por git); nunca se imprimen ni se copian.

## Protocolo de traspaso entre agentes (obligatorio)

Cada agente, al empezar: `mem_context` → `mem_search "venta-cierre-ruta-nueva"` → leer este archivo y `docs/venta-cierre/contrato-sp.md`. Trabaja **solo** en los archivos de su tarea. Al terminar: commit convencional, marcar la casilla con hash y evidencia, actualizar el espejo Engram (`mem_update` del tópico `odd/venta-cierre-ruta-nueva/tasks`) y **detenerse**. No toma tareas de otro agente. Si la tarea exige tocar un archivo fuera de su alcance, se detiene y lo reporta.

Prompt de arranque (ejemplo para el agente de T2): "Implementa la tarea T2 de `odd/tasks/venta-cierre-ruta-nueva.md` respetando sus restricciones y `docs/venta-cierre/contrato-sp.md`. Solo los archivos de T2. No compiles en Linux ni declares que compila. Al terminar haz el commit, marca la casilla, actualiza Engram y detente."

## Riesgos y decisiones

- **Compilación tardía:** el proyecto no se puede compilar en Linux. Mitigado: punto de control B1 tras T3 y build final en T7, ambos por SSH con `git bundle` (nada sube a GitHub).
- **Modal dentro de modal** (`frmVentaCierreProgreso` sobre `frmCancelarPago`): debería funcionar con `ShowDialog(this)`; solo se confirma al compilar y probar en la VM.
- **Cuerpos de SP/triggers**: leídos de la base local; la igualdad con producción no está verificada.
- **Deadlocks con la ruta vieja:** el orden estable solo protege entre ventas de la ruta nueva. Un 1213 debe mostrarse con el mensaje exacto; el reintento automático es decisión del usuario.
- **Índices** (`notasalida(documentoreferencia, codNotaSalida)`, `movimientostock(codProducto, codAlmacen, tipoStock, fechaRegistro, codMovStock)`): fuera de alcance; cambian el esquema compartido y requieren ventana de mantenimiento. Decisión del usuario.
- **Tamaño y entrega (decidido por el usuario, 2026-10-05):** pronóstico ~1 200 líneas autoradas (> 400). **Una sola rama `feat/venta-cierre-ruta-nueva`; todas las tareas se commitean ahí. Nada pasa a `main` hasta que el compilado se pruebe y funcione.** No hay PR por tarea. Push, PR y merge a `main` siguen siendo decisión del usuario.

## Rutas de ejecución (declaración)

Planificado, sin ejecutar: T1, T4, T5 → agente opencode (delegado, escritor único); T2, T3, T6 → antigravity (delegado); RV-A, RV-B → codex (solo lectura); T7 → claude (compilación y revisión). Disparadores: escritura de ≥2 archivos no triviales por tarea y mapeo previo ya realizado por un subagente de investigación.
