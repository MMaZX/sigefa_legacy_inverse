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

#### Resultado de RV-B y revisión final (claude + codex, 2026-10-05)
- **RV-B (codex, T4+T5+T6):** T4 OK; hallazgos T6 corregidos en `7aa4f81` (modo captura solo para efectivo, `CodNota` original en legacy, borradores sin duplicar, impresión bloqueada en captura, aviso al no pagar el restante). **Hallazgo propio:** depósito/cheque/tarjeta/transferencia (6–9) crean además `GuardaPagoPendiente` en la ruta vieja y la nueva no lo replica → esos métodos usan el flujo viejo.
- **Revisión final (codex, `main...HEAD`):** **la ruta vieja no cambia** con el flag ausente o en `legacy`. Hallazgos: (alta) el mensaje "Pago Realizado Correctamente" se mostraba al capturar en memoria → cambiado a "Pago capturado…" (`000800e`); (baja) borradores residuales → `borradoresPago.Clear()` al iniciar captura (`000800e`); (media) **el cierre no es atómico entre almacenes** → límite aceptado y documentado: cada almacén es una transacción y la compensación entre almacenes es la del flujo original.
- **Build final (T7, VM, `Debug|x86`, directorio aparte `sigefa_build`) sobre `000800e`:** `exit=0`, **0 errores**. Nada se subió a GitHub.
- **Límites conocidos de la ruta nueva:** contado nuevo solo en efectivo; pago parcial ("No" a pagar el restante) cancela sin guardar; sin impresión del comprobante de pago desde el formulario en captura; la anulación de compensación puede mostrar `MessageBox`.
- **No verificado:** ejecución real (UI, base de datos, ventas con más de 11 ítems). Humo manual pendiente (lo hace el usuario con el flag `nueva` en la VM, nunca contra producción).

### T7 — Compilación y revisión final (claude, ~6 min)
- [ ] En la VM Windows (`C:\Users\qemu\Documents\sigefa_legacy`): el árbol tiene 4 archivos modificados (`frmLogin.cs`, `MysqlEmpresa.cs`, `MysqlSucursal.cs`, `MysqlUsuario.cs`) y 2 sin seguimiento. Antes de traer la rama: inspeccionar esas diferencias y preservarlas (stash o confirmar con el usuario); no sobrescribir.
- [ ] Llevar la rama con el mecanismo de B1 (`git bundle` + `scp`, sin subir a GitHub), compilar con MSBuild 18.6 (`Debug|x86`) en el directorio aparte. Errores de compilación: corregir en una ronda acotada; declarar cualquier error restante.
- [ ] Revisión final con codex sobre el diff completo; una ronda de corrección máxima.
- [ ] Informe: qué se verificó, qué no (humo manual en UI queda para el usuario), riesgos abiertos.
- Los datos de acceso a la VM viven en `try-print-go/usqay-print-client/windows-test.yaml` (ignorado por git); nunca se imprimen ni se copian.

### T8 — Contador de ítems y scroll en `frmVenta2019` (agregada 2026-10-05, a pedido del usuario durante la prueba en VM)
- **Alcance:** solo `SIGEFA.Formularios/frmVenta2019.cs`. Sin tocar servicio, repositorio ni `frmCancelarPago`.
- **Contador:** no se toca ninguno de los ~12 puntos que mutan `dgvdetalle` (líneas 1396, 2099, 3256, 4896–4908, 5492, 5830, 5957…). Se suscribe `RowsAdded`/`RowsRemoved` (cubre agregar, actualizar por recarga, quitar y `Rows.Clear()`) a un único método `ActualizarContadorItems()` que escribe "Ítems: N" y, como dato adicional, la suma de cantidades. Ubicación propuesta: dentro del groupBox "DETALLE ORDEN" (`groupBox3`, 878×153, `Anchor` Top|Left|Right) en el borde inferior o en el título, sin mover controles existentes.
- **Scroll:** `dgvdetalle` ya tiene scroll propio; el panel que queda corto es otro. Pendiente confirmar cuál (candidato: `groupBox10` "DETALLE PRODUCTO", 475×114, con `dgvStockAlmacenes`). Solución prevista: `AutoScroll = true` en el contenedor, sin cambiar tamaños ni `Dock` de sus hijos. Aplica solo con la confirmación del usuario.
- **Riesgo:** el form es de layout absoluto y decompilado (`.Designer` embebido en el `.cs`); un control nuevo mal anclado puede solaparse a otra resolución. Mitigación: `Anchor` Bottom|Right, valor inicial "Ítems: 0", y prueba visual en la VM.
- **Comportamiento con flag:** independiente de `VentaCierreRuta`; es solo presentación.
- [ ] Implementar (pendiente de confirmar el panel del scroll). Verificación: compilar en VM (T7) y prueba visual con 1, 12 y 30 ítems.
- **Pospuesta por el usuario (2026-10-05):** es de diseño, no bloquea. No tomar hasta nueva orden.

## Ampliación de métodos de pago en captura (T13d [ex T9], T10a, T10b, T11, T12, agregada 2026-10-05, a pedido del usuario)

**Decisión del usuario:** probar en la ruta nueva efectivo, depósito, transferencia, nota de crédito y pendiente. **Depósito por cheque (7) se queda en el flujo viejo** (sin lógica nueva). Tarjeta (8) entra porque comparte mecánica con 6 y 9.

**Evidencia (mapeo de solo lectura, 2026-10-05, BD local 3307; igualdad con producción NO verificada):**
- `GuardaPagoPendiente` solo descuenta de un pago tipo 12 ya existente para esa factura: en venta nueva es no-op. La restricción de T6 a solo efectivo era sobre-conservadora para 6, 8 y 9. `GuardaPago` ya inserta `ctactemovimientos` para 6–9.
- **Nota de crédito (10):** todos los efectos reales ocurren en el trigger `ActualizaNotaInsertPago` (AFTER INSERT en `pago`): descuenta `notacredito.pendiente/abonado`, marca `cancelado`, setea `factura_venta.codNotaCredito`, inserta `cajamovimiento` tipo 10. Corre en la transacción del servicio; **no hay nada post-commit**. `ActualizaPendienteCredito` y `ActualizaNCreditoVentaSinAplicar` de la ruta vieja son código muerto (`notaI` nunca se llena, afectan 0 filas): no se replican. `BorradorPago` y `guardarPago` ya llevan `notacre` y `codnotac`.
- **Riesgos NC:** (1) `Pag` es campo único y nunca se resetea: tras una NC, un efectivo capturado después saldría con `notacre=1` y la misma `codnotac` (consumiría la NC dos veces). (2) No hay reserva: se puede elegir la misma NC dos veces en la misma captura, o dos cajeros a la vez; el trigger recorta a 0 en silencio (sobre-consumo sin error). Falta `SELECT pendiente FROM notacredito WHERE codNotaI=? FOR UPDATE` dentro de la transacción y validar monto <= pendiente.
- **Pendiente (12):** no requiere cambios de cabecera (`guardarFacturaVenta` no envía `cancelado`; `pendiente = total`, igual que la ruta vieja). El trigger no toca `abonado/pendiente/cancelado` para tipo 12; crea `pago` 12 + `cajamovimiento` 12. Solo falta habilitarlo. Borde: un 12 seguido de otro método obligaría a replicar `GuardaPagoPendiente` en la transacción.
- Saldo de cuenta del método 9: no aplica en contado (`tipo==3`), tampoco en la ruta vieja.

**Supuestos adoptados (el usuario puede revertirlos):**
1. **12 solo como último borrador**: se exige `txtMontoPago == txtMontoPendiente` al elegir 12, así nunca hay un 12 previo a otro método y no hace falta replicar `GuardaPagoPendiente`.
2. La NC se bloquea con `FOR UPDATE` dentro de la transacción (cambia una carrera que ya existe, pero ahora con ventana más larga).
3. Revalidar la caja al commitear queda **fuera de alcance** (hoy se lee al capturar).
4. Depósito por cheque (7) conserva el aviso y el flujo viejo.

**Ruta de ejecución declarada:** T13d (ex T9) → opencode (delegado, un archivo, mecánico); T10a, T10b → antigravity (núcleo transaccional); T11 → opencode; RV-C → codex (solo lectura, vía claude); T12 → claude (build en VM + informe). Disparadores: T10a toca 4 archivos no triviales; T13d/T10b/T11 comparten `frmCancelarPago.cs` (un solo escritor a la vez sobre ese archivo).

**Prioridad (decidida por el usuario, 2026-10-05): T13a/T13b (pasos post-cierre visibles y registro de errores) van ANTES del trabajo de métodos de pago (T10a, T10b, T11). Excepción aprobada por el usuario: T13d (ex T9, un cambio de ~5 líneas en `frmCancelarPago.cs`) se adelanta y corre en paralelo con T13a porque no comparten archivos.** El orden queda así:

```
1. T13a (agy: VentaCierre/* + diálogo) ─> T13b (agy: frmVenta2019 + Facturacion) ─> T13c (agy: cierre multialmacén atómico) ─> B1' (build VM + prueba del usuario)
2. Recién entonces métodos de pago:
   agy:      T10a (VentaCierre/*) ─> T10b (frmCancelarPago)
   opencode: T13d [ex T9] (frmCancelarPago; en paralelo con T13a) ─> T11 (frmCancelarPago)
   B2 (build VM) ─> RV-C (codex) ─> T12 (claude, build final)
```
T13a y T13b se serializan (comparten `VentaCierre/*` y el flujo de `guardaVenta`). T13d corre en paralelo con T13a (archivos disjuntos, `git add <rutas>` explícitas y commits secuenciales). Una vez cerrado T13, T10a (agy) puede correr en paralelo con lo que quede de opencode. T10b y T11 esperan a T13d y a T10a.

### T13a — Pasos "Después de guardar" y registro de errores por paso (antigravity, ~15 min)
**Decisiones del usuario (2026-10-05):** las 4 acciones que hoy corren al pulsar "Cerrar" deben ser pasos visibles; ninguna anula la venta; hay que saber **por qué** falló cada una.

**Orden de los pasos post-cierre** (el despacho primero; la impresión después de la facturación electrónica porque el comprobante impreso lleva el QR/firma de `venta.Qr`, que sale de `facturacion.LogoEmp`):
1. Crear despacho (se muestra "Omitido: sin requerimiento" si el pedido no tiene; hoy crea e imprime el despacho, se conserva).
2. Guardar código de barras del pedido (`GuardaCodigoBarras`: UPDATE de `pedidosventa.codigobarras`; es solo una etiqueta).
3. Generar y firmar comprobante electrónico (XML, firma, PDF, `registrar_repositorio`; **no envía a SUNAT**, eso es `frmEnvioSunat`).
4. Imprimir comprobante (`fnImprimir`; dos copias, se conserva).

**Alcance:** `SIGEFA.Administradores/VentaCierre/*` (archivos nuevos) y `SIGEFA.Formularios/frmVentaCierreProgreso.cs` (+ Designer). Sin tocar `frmVenta2019` ni `Facturacion.cs` (eso es T13b).
- **Modelo (separado del enum transaccional):** `VentaCierrePostPaso` (crearDespacho, guardarCodigoBarras, generarComprobanteElectronico, imprimirComprobante) con estados Pendiente / En curso / Listo / **Advertencia** / Omitido.
- **Errores por paso (uno o más):** clase `ErrorPaso` con paso, hora, tipo de excepción, mensaje, causa interna, `StackTrace` y contexto (factura id, serie-número, almacén, pedido, usuario). Cada paso acumula una lista (`List<ErrorPaso>`); un paso con 1+ errores queda en Advertencia, nunca aborta los siguientes ni anula la venta.
- **Registro:** clase `VentaCierreRegistroErrores` que escribe en el mismo archivo `%LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log`, mismo formato y enmascarado de credenciales que `VentaCierreService.registrarErrorLocal` (extraer la lógica; este es el único escritor del servicio en este momento). Sin abrir conexión a BD ni tabla nueva (tabla = cambio de esquema compartido, fuera de alcance).
- **Diálogo:** segunda sección "Después de guardar" con las 4 filas y el estado Advertencia (color ámbar); la fila muestra el primer error resumido; "Copiar detalle" incluye **todos** los errores de **todos** los pasos. "Cerrar" se habilita solo al terminar todo. El diálogo recibe del formulario la lista de acciones (nombre + delegado asincrónico) y las ejecuta en el hilo UI, sin conocer `frmVenta2019`. `fueExitoso` no cambia por advertencias.
- **Renombrados:** "Guardar cabecera" → "Guardar venta" (`VentaCierrePasoTexto`, `VentaCierrePaso.cs:51`) y el texto de progreso de `VentaCierreService.cs:211` → "Guardando la venta...".
- [x] Implementada en `22bf183` (2026-10-05): `VentaCierrePostPaso` y `VentaCierreEstadoPaso` con estados Pendiente, En curso, Listo, Advertencia (en ámbar), Omitido; `ErrorPaso` con campos de excepción y contexto (factura id, serie-número, almacén, pedido, usuario); `VentaCierreRegistroErrores` para `%LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log` con enmascarado de credenciales, extrayendo la lógica de `VentaCierreService.registrarErrorLocal`; diálogo `frmVentaCierreProgreso` con segunda sección "Después de guardar", ejecución de acciones asincrónicas en hilo UI sin conocer `frmVenta2019`, fila con primer error resumido, "Copiar detalle" con todos los errores de todos los pasos, "Cerrar" habilitado solo al terminar y advertencias sin abortar ni alterar `fueExitoso`; renombrado de "Guardar cabecera" a "Guardar venta" en `VentaCierrePasoTexto` y progreso "Guardando la venta...". Verificación: pendiente de compilación en VM (B2).

### T13b — Conectar los pasos post-cierre y limpiar la vista al cerrar (opencode por decisión del usuario, 2026-10-05; ~15 min, después de T13a)
**Alcance:** `SIGEFA.Formularios/frmVenta2019.cs` (solo ramas con flag `nueva`, contado y crédito, que comparten el flujo posterior) y `SIGEFA.SunatFacElec/Facturacion.cs`.
- Sacar de `guardaVenta()` (ruta nueva) las 4 acciones y pasarlas al diálogo como delegados en el orden de T13a. La ruta legacy queda **idéntica**.
- **Capturar los errores hoy tragados:** `Facturacion.GeneraDocumento` muestra `MessageBox` y sigue; agregar un colector opcional (`Action<string, Exception>`) que, si está presente (solo ruta nueva), recibe cada error además de/en lugar del `MessageBox`; con `null` el comportamiento es el actual. Lo mismo para `fnImprimir` y `CreacionDespacho` (errores hoy en `MessageBox` o try propio).
- `GuardaCodigoBarras` con try propio **por pedido** y su error como advertencia: en la ruta nueva **no se anula la venta** por este paso (el `catch` general sigue protegiendo fallos del servicio).
- **Al pulsar "Cerrar"** (cuando el diálogo vuelve): llamar `limpiarVentana()` (`:2069`) y reiniciar `venta`, `lista_facturas`, `CodVenta` y `PedidosIngresados`, para que el formulario quede listo para "Iniciar OV". `limpiarVentana()` no los reinicia por sí sola. Consecuencia: el botón Imprimir deja de servir para reimprimir esa venta tras cerrar.
- **Riesgo conocido:** `guardaVenta` es `async void` y se llama sin `await` (`:2989`, `:2999`); no cambiar su firma ni el flujo de quien la llama. Revisar en RV-C.
- **Preparar T13c (obligatorio):** encapsular las 4 acciones post-cierre en un método auxiliar que recibe el bloque (venta, detalle1, cliente, pedidos) y devuelve los delegados, **sin depender del `foreach` de almacenes**. T13c reordenará el recorrido en tres fases y reutilizará ese método por documento; no deben quedar acciones post-cierre pegadas al cuerpo del `foreach`.
- [x] Implementada en `02e42bb` (2026-10-05): helper `construirAccionesPostCierre` (bloque, detalle, cliente, pedidos) con los 4 delegados en orden (despacho con "Omitido: sin requerimiento", código de barras por pedido, FE con `venta.Qr = LogoEmp`, impresión con doble copia y visor de transferencia) pasados al diálogo en crédito y contado-captura; colector opcional `Action<string, Exception>` en `GeneraDocumento` (4 sitios), `CreacionDespacho`/`imprimirDespachos`, `fnImprimir`/`PrintaDocumento`/`PrintaDocumentoTrnas` (con `null` los `MessageBox` quedan idénticos); código de barras como advertencia sin anular; al cerrar: `limpiarVentana()` + reinicio de `venta`, `lista_facturas`, `CodVenta` y `PedidosIngresados`; firma `async void` y llamadores intactos. Verificación: no se compila en Linux; compilar en VM (B2) y humo manual pendientes: (a) venta normal con 4 filas en Listo y formulario limpio; (b) fallas forzadas (FE, impresora, sin requerimiento) con fila en ámbar, error en "Copiar detalle" y en el log.

### T13c — Cierre multialmacén atómico: un bloque por almacén en una sola transacción (antigravity, ~20 min, después de T13b)
**Problema observado por el usuario (2026-10-05, prueba en VM):** con productos de almacenes distintos (p.ej. D36 FR y D36 LM) `guardaVenta()` recorre `foreach (object e in alma)` (`frmVenta2019.cs:3548`) y por cada almacén arma la venta, abre el pago, guarda su transacción y factura **antes** de pasar al siguiente. FR queda guardada y facturada antes de pedir el pago de LM; si el cajero cancela en LM, el `catch` (`:3771`) no hace rollback sino que **anula por compensación** (documento anulado + numeración consumida). Comportamiento deseado: **un bloque por empresa, todos en una sola transacción**; si algo falla o se cancela, no se crea ninguno.

**Alcance:** `SIGEFA.Administradores/VentaCierre/VentaCierreService.cs` (método nuevo), `SIGEFA.Formularios/frmVentaCierreProgreso.cs` (+ Designer, para N bloques) y `SIGEFA.Formularios/frmVenta2019.cs` (solo la rama `nueva`). Ruta legacy intacta (conserva el recorrido y la compensación actuales). No toca el repositorio salvo que sea imprescindible (reusar `bloquearSerie`, `bloquearStock`, `guardarFacturaVenta`, `guardarDetalle`, `guardarPago`).

**Tres fases en `guardaVenta()` (ruta nueva):**
1. **Preparar (sin escribir en la BD):** por cada almacén armar el bloque (`obtenerDatosVenta`, `ArmaCabecera`, serie, `RecorreDetalleVenta`). Verificar que ninguna de esas llamadas escriba en la BD; si alguna lo hace, reportarlo y detenerse.
2. **Cobrar (solo contado):** abrir `frmCancelarPago` (modo captura) **uno tras otro** y acumular los `BorradorPago` de cada bloque en memoria. Cada comprobante conserva su monto y su serie. Si el cajero cancela en cualquiera, se aborta toda la venta con un mensaje claro y **no se guarda nada**. En crédito esta fase no existe.
3. **Guardar:** una sola llamada atómica al servicio con todos los bloques y un único diálogo de progreso; después, por documento, los pasos post-cierre de T13a/T13b.

**Servicio — `ejecutarOrdenAtomica(IList<VentaCierreDatosBloque>, IProgress<...>)`:**
- Una conexión y una transacción (`RepeatableRead`, como hoy) para todos los bloques; un solo `Commit`.
- Orden de bloqueo global y estable para evitar deadlocks entre ventas concurrentes: primero **todas** las series (distintas, ascendentes por `serieId`), luego **todo** el stock (ascendente por `almacenId` y `productoId`), y recién después, bloque por bloque: cabecera, detalle y pagos.
- Si dos bloques usan la misma serie, la numeración debe avanzar correlativa dentro de la transacción: confirmar con `GuardaFacturaVenta` cómo asigna el número y probarlo.
- Cualquier excepción: `Rollback` completo, entidades restauradas (ids asignados solo tras `Commit`, como hoy), error registrado con `VentaCierreRegistroErrores` indicando bloque y paso. Sin compensación en la ruta nueva.
- Reportar el avance con bloque k/n (el DTO ya trae `bloqueActual` y `totalBloques`).

**Diálogo para N bloques:** la lista de pasos transaccionales debe mostrarse sin confundir: series y stock una vez (globales) y luego guardar venta / detalle / pagos por bloque, con el almacén en el encabezado. La sección "Después de guardar" lista las acciones **por documento** (nombre con el almacén). Con un solo bloque se ve igual que hoy. Cuidado: `marcarPasosHasta` compara el valor numérico del enum; hay que adaptarlo a pasos repetidos por bloque.

**Reemplaza** el límite aceptado en la revisión final ("el cierre no es atómico entre almacenes") **solo para la ruta nueva**.
**Riesgos aceptados:** transacción más larga (las filas de serie y stock quedan bloqueadas más tiempo para otras cajas); si falla un bloque no se guarda ninguno y la venta completa se reintenta; los fallos post-cierre de un documento no afectan al otro.
- [ ] Implementar. Verificación: compilar en VM. Humo manual con productos de dos almacenes: (a) caso feliz: dos pagos, un solo diálogo, dos comprobantes; (b) cancelar el pago del segundo almacén: **no queda ninguna fila** en `factura_venta`, sin anulaciones y sin numeración consumida; (c) fallo forzado en el segundo bloque (p.ej. stock insuficiente): el primero tampoco se guarda; (d) venta de un solo almacén: igual que antes.

### T13d (ex T9) — Habilitar efectivo, depósito, tarjeta y transferencia en captura (opencode, ~5 min, en paralelo con T13a)
- **Alcance:** solo `SIGEFA.Formularios/frmCancelarPago.cs`, bloque de `btnAceptar_Click` (~l.642-662).
- `esMetodoSoportado` pasa a `5, 6, 8, 9`. El 7 (depósito por cheque), 10 y 12 siguen con el aviso y el flujo viejo (T10b/T11 habilitan 10 y 12).
- Actualizar el comentario (ya no es cierto que `GuardaPagoPendiente` bloquee: es no-op en venta nueva; ver Evidencia). Mantener el bloqueo de mezcla de borradores con métodos no soportados.
- No tocar servicio, repositorio ni `BorradorPago`. Confirmar que `Pag.codCtaCte`, `CtaCte`, `CodBanco`, `CodTarjeta`, `NOperacion` viajan al borrador (ya lo hace `desdePago`).
- [x] Implementada en `efc68da` (2026-10-05): `esMetodoSoportado` pasa a `5, 6, 8, 9` con comentario actualizado (GuardaPagoPendiente es no-op en venta nueva); 7, 10 y 12 conservan aviso y flujo viejo, y el bloqueo de mezcla con borradores queda intacto. Confirmado por lectura que `desdePago` ya lleva `codCtaCte`, `CtaCte`, `CodBanco`, `CodTarjeta` y `NOperacion` al borrador; sin tocar servicio, repositorio ni `BorradorPago`. Verificación: no se compila en Linux; compilar en VM (B2) y humo manual pendientes: un depósito, una transferencia, una tarjeta y un mixto efectivo + transferencia, revisando `pago` y `ctactemovimientos` en BD local.

### T10a — Reserva y validación de nota de crédito en el servicio (antigravity, ~12 min)
- **Alcance:** `SIGEFA.InterMySql/VentaCierre/VentaCierreRepositorio.cs`, su interfaz, `SIGEFA.Administradores/VentaCierre/VentaCierreService.cs` y `VentaCierrePaso.cs`. Sin UI.
- Paso nuevo **antes de `guardarPago`** (orden estricto: serie → stock → cabecera → detalle → **notas de crédito** → pago → commit): por cada `notaCreditoId` distinto en los borradores, `SELECT pendiente FROM notacredito WHERE codNotaI=? FOR UPDATE` y validar que la suma de `montoCobrado` de los borradores que la usan sea `<=` pendiente. Ante falla: `VentaCierreException` con paso nombrado y rollback existente; registrar el nombre del paso en `obtenerNombreProcedimiento`.
- Sin cambios si ningún borrador lleva NC (ruta de efectivo idéntica).
- [ ] Implementar. Verificación: compilar en VM (B2).

### T10b — Habilitar nota de crédito en captura (antigravity, ~8 min, después de T13d)
- **Alcance:** `frmCancelarPago.cs` (y opcionalmente `frmListaNCreditosSinAplicar.cs`).
- Incluir 10 en `esMetodoSoportado`.
- **Resetear `Pag.NotaCredito` y `Pag.CodNotaCredito` a 0** al capturar cualquier método distinto de 10 (hoy `Pag` nunca se limpia).
- Impedir elegir la misma NC dos veces en `borradoresPago` (o restar lo ya capturado del pendiente mostrado en la lista).
- No replicar `ActualizaPendienteCredito`/`ActualizaNCreditoVentaSinAplicar` (código muerto, ver Evidencia).
- [ ] Implementar. Verificación: compilar en VM; humo manual con una NC cuyo pendiente cubra y no cubra la venta; mixto NC + efectivo; comprobar `notacredito.pendiente/abonado/cancelado` y `factura_venta.codNotaCredito` en BD local.

### T11 — Habilitar pendiente (12) en captura (opencode, ~5 min, después de T10b)
- **Alcance:** `frmCancelarPago.cs`.
- Incluir 12 en `esMetodoSoportado`. La confirmación "Esta seguro de cobrar con esta método de pago?" ya existe (l.663): conservarla.
- Al elegir 12 exigir `txtMontoPago == txtMontoPendiente` (12 siempre último borrador, supuesto 1); si no, mensaje claro y no capturar.
- Verificar que `frmVenta2019` no necesita cambios (el mapeo dice que no trata el 12 de forma especial) y que el cobro posterior desde `frmCobros` (`tipo=3`, `vieneDe="frmCobros"`) sigue funcionando con la venta creada por la ruta nueva.
- [ ] Implementar. Verificación: compilar en VM; humo manual: venta a pendiente y luego cobro desde `frmCobros`; comprobar `pago` tipo 12, `cajamovimiento` tipo 12 y `caja.totalpendiente`.

### RV-C — Revisión de T13d y T10a–T11 (codex, solo lectura, ~5 min)
Revisar que la ruta vieja y el efectivo no cambian con flag ausente o `legacy`, que el 7 sigue en flujo viejo, que la NC no se consume dos veces y que el 12 solo puede ser el último borrador.

### T12 — Build final, revisión e informe (claude, ~8 min)
- [ ] Build en la VM (`Debug|x86`, directorio aparte `sigefa_build`) tras T13d (B2) y tras T11; registrar errores completos.
- [ ] Informe: tabla método → ruta (nueva/vieja), qué se verificó, qué queda para humo manual, riesgos abiertos.

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
