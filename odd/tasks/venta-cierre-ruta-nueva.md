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
- [ ] Compilar `SIGEFA.csproj` con `/p:Configuration=Debug /p:Platform=x86` (no la `.sln`: da `MSB4126`) con MSBuild 18.6 y registrar **todos** los errores (código, archivo, línea), no solo el primero. Compilar sin `-m` si hace falta para ver el orden de errores.
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
- Los datos de acceso a la VM viven en `.local/windows-test.yaml` (copia de `try-print-go/usqay-print-client/windows-test.yaml`; `.local/` está en `.gitignore`); nunca se imprimen ni se commitean. Conexión: `ssh -o BatchMode=yes <user>@<host>` con la configuración SSH por defecto del host (el yaml no tiene `ssh_key_path`), como documenta `try-print-go/docs/entorno-pruebas-windows.md`. **Autorización durable del usuario (2026-10-05):** conectarse a esa VM sin volver a preguntar, solo para bundle, `scp` a `sigefa_build` y compilación con MSBuild; nada más y sin tocar `sigefa_legacy`.

### T8 — Contador de ítems y scroll en `frmVenta2019` (agregada 2026-10-05, a pedido del usuario durante la prueba en VM)
- **Alcance:** solo `SIGEFA.Formularios/frmVenta2019.cs`. Sin tocar servicio, repositorio ni `frmCancelarPago`.
- **Contador:** no se toca ninguno de los ~12 puntos que mutan `dgvdetalle` (líneas 1396, 2099, 3256, 4896–4908, 5492, 5830, 5957…). Se suscribe `RowsAdded`/`RowsRemoved` (cubre agregar, actualizar por recarga, quitar y `Rows.Clear()`) a un único método `ActualizarContadorItems()` que escribe "Ítems: N" y, como dato adicional, la suma de cantidades. Ubicación propuesta: dentro del groupBox "DETALLE ORDEN" (`groupBox3`, 878×153, `Anchor` Top|Left|Right) en el borde inferior o en el título, sin mover controles existentes.
- **Scroll:** `dgvdetalle` ya tiene scroll propio; el panel que queda corto es otro. Pendiente confirmar cuál (candidato: `groupBox10` "DETALLE PRODUCTO", 475×114, con `dgvStockAlmacenes`). Solución prevista: `AutoScroll = true` en el contenedor, sin cambiar tamaños ni `Dock` de sus hijos. Aplica solo con la confirmación del usuario.
- **Riesgo:** el form es de layout absoluto y decompilado (`.Designer` embebido en el `.cs`); un control nuevo mal anclado puede solaparse a otra resolución. Mitigación: `Anchor` Bottom|Right, valor inicial "Ítems: 0", y prueba visual en la VM.
- **Comportamiento con flag:** independiente de `VentaCierreRuta`; es solo presentación.
- [x] Contador implementado en `126d4cd` (2026-10-06): `RowsAdded`/`RowsRemoved` suscritos en `Load` a `ActualizarContadorItems()`, que escribe en el título de `groupBox3` ("DETALLE ORDEN | Ítems: N | Cant.: X"); se reutilizó el `dgvdetalle_RowsRemoved` vacío existente (estaba sin suscribir). Decisión: título en vez de borde inferior (la grilla ocupa 131 de 153 px, quedan ~4 px abajo; riesgo cero de layout). Sin tocar los puntos que mutan `dgvdetalle`, independiente del flag. Scroll pendiente de confirmar el panel. Verificación: no se compila en Linux; build en VM y prueba visual con 1, 12 y 30 ítems pendientes.
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
   opencode: T13d [ex T9] (hecha) ─> T14 (helper de campos de pago; frmCancelarPago; bug del usuario, va ANTES de T10b y T11) ─> T14a (etapa A: plan de `frmCancelarPago_Load`, solo escribe un documento, puede correr en paralelo con T14; etapa B: refactor tras la aprobación del usuario) ─> T11 (frmCancelarPago)
   B2 (build VM) ─> RV-C (codex) ─> T12 (claude, build final)
```
T13a y T13b se serializan (comparten `VentaCierre/*` y el flujo de `guardaVenta`). T13d corre en paralelo con T13a (archivos disjuntos, `git add <rutas>` explícitas y commits secuenciales). Una vez cerrado T13, T10a (agy) puede correr en paralelo con lo que quede de opencode. T10b y T11 esperan a T13d y a T10a.

### Actualización 2026-10-06 — combinar métodos de pago, incluido nota de crédito (10) y pendiente (12)

**Disparador (prueba del usuario en la VM):** en "COBRANZA VENTAS" se capturó un pago en borrador y luego se eligió PENDIENTE (12) para el resto; `btnAceptar_Click` (`frmCancelarPago.cs`, rama `borradoresPago.Count > 0` con método no soportado) mostró "No se pueden combinar pagos en borrador con métodos de persistencia directa...". Es la restricción de diseño de T6/T13d, no un bug: hoy `metodosSoportadosEnCaptura = { 5, 6, 8, 9 }`. **El 10 y el 12 aún NO están aceptados** (el usuario creyó que sí); entran con T10a, T10b y T11, que ya están planificadas arriba.

**Lo que ya está resuelto y no se rehace:** que efectivo (5) y pendiente (12) no habiliten banco, tarjeta, operación ni cheque lo cubre `PagoCamposHelper.aplicarMetodo` (T14, rama `default`: solo el monto queda habilitado). T10b/T11 no tocan esa tabla salvo que la prueba muestre un caso nuevo.

**Ajustes al plan existente (sin tareas nuevas):**
- **T11:** el aviso de bloqueo de mezcla (`borradoresPago.Count > 0`) debe dejar de dispararse para 10 y 12 una vez incluidos en `esMetodoSoportado`; el 7 (cheque) lo conserva. Con el supuesto 1, el 12 se acepta solo si `txtMontoPago == txtMontoPendiente` (siempre último borrador). Mensaje propio y claro cuando no se cumple, en lugar del genérico de persistencia directa.
- **T10b:** al elegir 10 como segundo o tercer borrador, el pendiente mostrado debe ser el restante tras los borradores previos, y la NC ya usada en `borradoresPago` no puede repetirse.
- **Orden de entrega:** T10a → T10b → T11 → B2 (build VM) → RV-C → T12, sin cambios. Cada tarea cierra con su commit de work unit.
- **Humo manual ampliado (usuario, en VM):** (a) efectivo parcial + pendiente; (b) NC parcial + efectivo; (c) NC + transferencia + pendiente; (d) 12 primero con monto menor al pendiente (debe rechazarse con mensaje claro); (e) cheque (7) con borrador previo (debe seguir bloqueado).

**Pregunta abierta de producto:** ¿se acepta el supuesto 1 (12 solo como último borrador) o se necesita 12 en cualquier posición? Cualquier posición obliga a replicar `GuardaPagoPendiente` dentro de la transacción (más riesgo; se evaluaría como tarea aparte).

**Código de nota de crédito (referencia, solo lectura):** captura en `frmCancelarPago.cs` (selección vía `frmListaNCreditosSinAplicar` en ~l.1676-1700; armado de `Pag` con `NotaCredito`/`CodNotaCredito` en ~l.1135-1145 y ~l.1263-1275; `CargaNotaCredito` ~l.537 para devolución tipo 100), dominio en `SIGEFA.Administradores/clsAdmNotaCredito.cs` + `SIGEFA.InterMySql/MysqlNotaCredito.cs`, creación en `frmNotadeCredito.cs`, y los efectos reales sobre la NC en el trigger `ActualizaNotaInsertPago` (BD, tabla `pago`).

**Espejo Engram:** sincronizado el 2026-10-06 (tópico `odd/venta-cierre-ruta-nueva/tasks`, observación 265) como resumen de estado con el locator de este archivo, no como copia íntegra del documento.

**T8 delegada a opencode (decisión del usuario, 2026-10-06).** El contador de ítems puede implementarse ya; el scroll solo tras confirmar el panel con el usuario.

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
- [x] Implementada en `5cd71a2` (2026-10-05): `VentaCierreService.ejecutarOrdenAtomica` con una conexión y transacción `RepeatableRead` para N bloques, orden global estable de bloqueo (series ascendente por serieId, luego stock ascendente por almacenId y productoId), ejecución por bloque (cabecera, detalle, pagos), un solo Commit, Rollback total ante fallos con restauración de entidades sin compensación, registro de errores vía `VentaCierreRegistroErrores`; diálogo `frmVentaCierreProgreso` adaptado para 1 o N bloques (series/stock globales, desglose de venta/detalle/pago por almacén, acciones post-cierre por documento con sufijo de almacén); `frmVenta2019.guardaVenta` reestructurado en tres fases para la ruta nueva (1. Preparar en memoria sin escribir en BD, 2. Cobrar en captura secuencial abortando sin guardar si se cancela algún pago, 3. Guardar atómicamente con diálogo) y ruta legacy preservada idéntica. Verificación: no se compila en Linux; pendiente B1' en VM Windows y pruebas de humo manuales.

### Cierre de la fase 1: B1' + RV-T13 (claude + codex; pedido del usuario, 2026-10-05)
Se ejecuta **cuando T13a, T13b, T13c y T13d estén commiteadas**, antes de empezar T10a/T10b/T11.
- [x] **B1' (claude):** compilar en la VM Windows con el mecanismo de B1/T7 (`git bundle` de la rama + `scp`, directorio aparte `sigefa_build`, `Debug|x86`, MSBuild 18.6; sin tocar `sigefa_legacy` de la VM ni subir nada a GitHub; credenciales solo desde `windows-test.yaml`, nunca impresas). Registrar **todos** los errores (código, archivo, línea). Errores de T13 vuelven al agente dueño de esa tarea en una sola ronda.
  - **Resultado (2026-10-06, sobre `e933d78`, incluye T13a–T13d, T14, T14a, T10a, T10b, T11 y T8 contador):** `exit=0`, **0 errores**, 480 warnings (resumen de MSBuild), 23.75 s, sin `-m`. Commit en la VM verificado igual al HEAD del host. Compilar no prueba funcionamiento: el humo manual sigue pendiente.
  - **Comando de build correcto:** compilar **`SIGEFA.csproj`** (en la raíz del repo, no en `SIGEFA\`) con `/p:Configuration=Debug /p:Platform=x86`. **No** compilar `SIGEFA.sln` con `Debug|x86`: falla con `MSB4126` porque la solución no define esa configuración (no es un error de código).
- [ ] **RV-T13 (codex, solo lectura, lo lanza y coordina claude):** revisar `main...HEAD` limitado a T13a–T13d. Puntos obligatorios: (1) con flag ausente o `legacy` la ruta vieja no cambia (`git diff -w`); (2) ninguna acción post-cierre anula la venta ni cambia `fueExitoso`; (3) `guardaVenta` conserva su firma `async void` y el flujo de sus llamadores; (4) `ejecutarOrdenAtomica`: orden de bloqueo global (series, luego stock), Rollback completo, ids asignados solo tras `Commit`, numeración correlativa si dos bloques comparten serie; (5) la fase "Cobrar" no escribe en la BD y cancelar en cualquier almacén no deja nada guardado; (6) `limpiarVentana()` más el reinicio de `venta`, `lista_facturas`, `CodVenta` y `PedidosIngresados`; (7) el 7, 10 y 12 siguen en flujo viejo. Una ronda de corrección máxima.
- [ ] **Informe:** qué se verificó, qué no (humo manual en UI queda para el usuario: caso feliz con dos almacenes, cancelar el segundo pago, fallo forzado en el segundo bloque, fallas forzadas de facturación electrónica e impresión), riesgos abiertos.
- **Corrección B1' en `9c5e137` (2026-10-05, sobre `be01f5c`):** CS0103 (`postCierreEnDialogo` declarado en alcance de método en `guardaVenta`, se quitó la declaración interna) y CS1061 (`almacen` del paso post-cierre tomado de `bloques[0]`, con nota de contexto fino por documento pendiente). Sin otros usos del mismo error exacto. Alcance: solo esas dos declaraciones; legacy idéntico (`git diff -w`). Evidencia: diff revisado, sin compilar en Linux; recompilar en la VM lo hace claude.
- **Ronda RV-T13 en `271e53b` (H2) + `8a6d614` (H3) (2026-10-05):** H2: con un solo bloque, 7/10/12 siguen por flujo viejo completo sin servicio ni diálogo (y con `!ventaRecibida` se agrega a `lista_facturas` y se lanza para compensar); con varios bloques el formulario bloquea el método con aviso y se queda en captura (`permitirFlujoViejo`, por defecto true). H3: `alMostrar` separa el try post-Commit (`alTerminarConAdvertenciaPosterior`: registra, muestra advertencia, habilita Cerrar/Copiar, nunca toca `fueExitoso`). Alcance: `frmCancelarPago.cs`, `frmVenta2019.cs`, `frmVentaCierreProgreso.cs`; legacy idéntico (`git diff -w`). Evidencia: diff revisado, sin compilar en Linux; build en la VM lo hace claude.

### T13d (ex T9) — Habilitar efectivo, depósito, tarjeta y transferencia en captura (opencode, ~5 min, en paralelo con T13a)
- **Alcance:** solo `SIGEFA.Formularios/frmCancelarPago.cs`, bloque de `btnAceptar_Click` (~l.642-662).
- `esMetodoSoportado` pasa a `5, 6, 8, 9`. El 7 (depósito por cheque), 10 y 12 siguen con el aviso y el flujo viejo (T10b/T11 habilitan 10 y 12).
- Actualizar el comentario (ya no es cierto que `GuardaPagoPendiente` bloquee: es no-op en venta nueva; ver Evidencia). Mantener el bloqueo de mezcla de borradores con métodos no soportados.
- No tocar servicio, repositorio ni `BorradorPago`. Confirmar que `Pag.codCtaCte`, `CtaCte`, `CodBanco`, `CodTarjeta`, `NOperacion` viajan al borrador (ya lo hace `desdePago`).
- [x] Implementada en `efc68da` (2026-10-05) y refactorizada en `641b7da`: `esMetodoSoportado` compara contra la constante `metodosSoportadosEnCaptura` (`5, 6, 8, 9`) con comentario actualizado (GuardaPagoPendiente es no-op en venta nueva); 7, 10 y 12 conservan aviso y flujo viejo, y el bloqueo de mezcla con borradores queda intacto. Confirmado por lectura que `desdePago` ya lleva `codCtaCte`, `CtaCte`, `CodBanco`, `CodTarjeta` y `NOperacion` al borrador; sin tocar servicio, repositorio ni `BorradorPago`. Verificación: no se compila en Linux; compilar en VM (B2) y humo manual pendientes: un depósito, una transferencia, una tarjeta y un mixto efectivo + transferencia, revisando `pago` y `ctactemovimientos` en BD local.

### T14 — Helper de campos de pago y reinicio tras pago parcial (opencode, ~15 min, agregada 2026-10-05 por bug del usuario)
**Bug (prueba del usuario en VM):** en `frmCancelarPago`, al responder "Sí" a "Desea pagar el restante?" en cualquier método de pago, **se habilitan todos los selects** (banco, tarjeta, cuenta corriente) y campos (operación, cheque, serie, número) en vez de quedar bloqueados según el método. Causa verificada: `Pagar()` (~l.1365) llama a `Deshabilita_botones(Estado: true)` (~l.1300), que pone `Enabled = true` a todos sin mirar el método y no reinicia nada. Además el bloque que sí aplica reglas por método está duplicado en un `if/else` largo dentro de `cmbMetodoPago_SelectionChangeCommitted` (~l.1609-1713) con asignaciones repetidas.

**Decisión del usuario:** reiniciar a efectivo tras un pago parcial y centralizar las reglas en un helper reutilizable, con un `switch` por método, en vez de repetir `Enabled`/`SelectedIndex` por todos lados.

**Convención (AGENTS.md, sección "Code conventions"):** sin `if` anidados a más de dos niveles; usar `switch` con `case` agrupados, un método auxiliar (`fijar(...)` con parámetros nombrados) y leer `Convert.ToInt32(cmbMetodoPago.SelectedValue)` una sola vez en una variable local.

**Alcance:** archivo nuevo `SIGEFA.Formularios/PagoCamposHelper.cs` (se incluye solo por globbing; namespace de archivo `SIGEFA.Formularios`, como `frmCancelarPago`) y `SIGEFA.Formularios/frmCancelarPago.cs`. Solo UI: sin tocar servicio, repositorio, `BorradorPago`, ni la lógica de datos de `btnAceptar_Click`/`Pagar()` fuera del punto del bug.

**Helper `PagoCamposHelper` (clase interna, sin lógica de negocio ni acceso a datos):**
- Recibe en el constructor los controles: `cmbMetodoPago`, `cboBanco`, `cboTarjeta`, `cboNumCta`, `txtOperacion`, `txtCheque`, `txtNc`, `txtMontoPago`.
- `aplicarMetodo(int metodoId)`: un `switch` que fija, por método, qué controles quedan habilitados y qué se limpia. Perfiles tomados del código actual:

| Método | Tarjeta | Banco | Operación | Cheque | Cuenta cte. | Monto | Se limpia |
|---|---|---|---|---|---|---|---|
| 5 Efectivo | no | no | no | no | no | sí | banco, tarjeta, cuenta, operación, cheque, NC |
| 6 Depósito / 9 Transferencia | no | sí | sí | no | no (se habilita al elegir banco, ya lo hace `cboBanco_SelectionChangeCommitted`) | sí | tarjeta, cuenta, operación, cheque, NC |
| 7 Dep. por cheque | no | sí | sí | sí | no | sí | todo |
| 8 Tarjeta | sí | sí | sí | no | sí (se conserva el comportamiento actual) | sí | todo |
| 10 Nota de crédito | no | no | no | no | no | **no** | todo (y `txtNc` deshabilitado) |
| 12 Pendiente y cualquier otro | como efectivo | | | | | | |

  Nota: hoy el método 12 (y 11, 13, 14) hace `return` sin tocar nada y arrastra los campos del método anterior; el helper lo trata como efectivo. Es un cambio deliberado para que no viajen banco/operación de otro método. El usuario puede vetarlo.
- `reiniciarAEfectivo()`: fija `cmbMetodoPago.SelectedValue = 5` y llama a `aplicarMetodo(5)`.
- Los efectos que no son de habilitado (`CargarBancos()`, `Focus()`, `buscaAprobacion`, el diálogo de nota de crédito `frmListaNCreditosSinAplicar` y la carga de `notaC`) **se quedan en el formulario**; el helper solo gobierna habilitado y limpieza.

**Cambios en `frmCancelarPago.cs`:**
1. Crear el helper en el constructor/`Load` (después de inicializar los controles) y sustituir el `if/else` de `cmbMetodoPago_SelectionChangeCommitted` por: `buscaAprobacion(...)`, `helper.aplicarMetodo(id)` y, a continuación, solo los efectos propios de cada método (`CargarBancos`, `Focus`, diálogo NC con su `return` actual). El comportamiento visible por método debe ser el mismo que hoy (tabla de arriba).
2. En `Pagar()`, rama "Sí, pagar el restante": **no** llamar a `Deshabilita_botones(true)`. Hacer: `helper.reiniciarAEfectivo()`, rehabilitar solo lo que corresponde a un nuevo ingreso (`txtMontoPago`, `dtpFecha`, `txtObservacion`, `btnAceptar`, y `btnCancelar` según el criterio actual), `txtMontoPago.Text = txtMontoPendiente.Text`, `continua_pago = true`. Limpiar también `txtOperacion`, `txtCheque`, `txtNc`, y poner en 0 `Pag.NotaCredito` y `Pag.CodNotaCredito` (`Pag` nunca se resetea: un efectivo posterior a una nota de crédito saldría marcado como nota de crédito).
3. `Deshabilita_botones(Estado: false)` (las otras llamadas) queda como está: sigue siendo el bloqueo final.
4. No dejes `Deshabilita_botones(true)` sin uso que invite a repetir el error: si ya no lo llama nadie con `true`, conserva el método (lo usan las rutas con `false`) y agrega un comentario de una línea.
5. `Pagar()` y este formulario los usan también otras rutas (`frmCobros`, rutas viejas); la corrección es solo de interfaz y aplica a todas. No toques lógica de datos.

**Verificación:** compilar en VM. Humo manual: (a) pago parcial con efectivo → "Sí" al restante: banco, tarjeta, cuenta, operación y cheque quedan bloqueados y vacíos, método = Efectivo, monto = restante; (b) pago parcial con transferencia → mismo resultado; (c) elegir cada método a mano (5, 6, 7, 8, 9, 10) y comprobar la tabla; (d) nota de crédito seguida de efectivo: el segundo pago no sale marcado como NC; (e) venta con un solo pago total: sin cambios.
- [x] Implementada en `20553c3` (feat) + `4271008` (fix) (2026-10-06): helper interno `PagoCamposHelper` (`switch` por método según la tabla; 12 y otros como efectivo; `reiniciarAEfectivo`); `SelectionChangeCommitted` reducido a `buscaAprobacion` + `aplicarMetodo` + efectos propios (`CargarBancos` tras el helper para no dejar banco residual, `Focus`, diálogo NC intacto); rama "Sí" de `Pagar()` reinicia a efectivo, rehabilita solo lo de un nuevo ingreso, monto = restante, limpia operación/cheque/NC y resetea `Pag.NotaCredito`/`CodNotaCredito`; `Deshabilita_botones(true)` sin llamadas, método conservado con comentario. Sin tocar servicio, repositorio, `BorradorPago` ni datos de `Pagar()`. Verificación: diff revisado, sin compilar en Linux; compilar en VM (B2) y humo manual (a)-(e) pendientes.

### T14a — Plan y refactor de `frmCancelarPago_Load` con las skills `refactor` y `csharp-async` (opencode, agregada 2026-10-05 a pedido del usuario)
**Objetivo:** reducir `frmCancelarPago_Load` (`SIGEFA.Formularios/frmCancelarPago.cs:376-496`, ~120 líneas) sin cambiar su comportamiento, aplicando las skills instaladas en `.agents/skills/refactor/SKILL.md` y `.agents/skills/csharp-async/SKILL.md` (ignoradas por git). **Primero un plan; el código solo después de que el usuario lo apruebe.**

**Olores detectados (lectura de claude, a confirmar/ampliar en el plan):**
1. Método largo con varias responsabilidades: bandera de ruta y modo captura, carga de listas (monedas, bancos, tarjetas, métodos de pago), carga del documento según `tipo` y cálculo de moneda/tipo de cambio.
2. Cadena `if / else if` sobre `tipo` (1, 2, 3, 4, 5) más ramas sueltas para 10 y 100: números mágicos sin nombre. `tipo` es un campo público que fijan otros formularios: no cambiar su tipo ni su significado.
3. Bloque de tipo de cambio duplicado 4 veces (`tc.Venta` o `tc.Compra`, texto vacío y `ReadOnly = false` si no hay tipo de cambio): candidato a un método `cargarTipoCambio(...)`.
4. Código muerto: `if (letra == null) { }` vacío en la rama `tipo == 4` y otros `if` vacíos del archivo.
5. La decisión del modo captura y el aviso de letras en la ruta nueva mezclados con la carga de la interfaz.
6. Todas las consultas a la BD corren en el hilo de interfaz, antes de mostrar el formulario.
7. Orden con dependencias que **no puede cambiar**: `CargaMetodosPagos()` antes de `cmbMetodoPago_SelectionChangeCommitted(cmbMetodoPago, null)` (y, tras T14, con el helper ya creado) y `Mon` antes del bloque de moneda.

**Etapa A — plan (solo lectura del código, escribe únicamente un documento):**
- Lee ambas `SKILL.md` y `AGENTS.md` ("Code conventions").
- Entrega `docs/venta-cierre/refactor-frmCancelarPago-load.md` con: lista ordenada de pasos pequeños (un método extraído por paso), el cuerpo actual y el nuevo de cada uno, el riesgo de cambiar el comportamiento, y cómo se verifica (no hay tests: humo manual por `tipo`).
- Aplicar la regla de oro de `refactor` (no cambiar comportamiento) y su proceso seguro. No proponer cambios de arquitectura ni de firmas públicas.
- **Decisión de `csharp-async` a justificar por escrito:** ¿conviene volver `Load` asincrónico o mover las consultas a segundo plano? Postura de claude: **no** convertir `Load` en `async void` en esta pasada. Los llamadores (`frmVenta2019`, `frmCobros` y otros) abren el formulario con `ShowDialog()` y leen el resultado al volver; con `Load` asincrónico el formulario se mostraría antes de terminar de inicializarse y el usuario podría pulsar Aceptar con listas vacías. Si el plan propone async, que indique el orden exacto de inicialización, qué controles se bloquean mientras carga y cómo se manejan las excepciones.
- Identificar qué otros llamadores abren `frmCancelarPago` (`rg "new frmCancelarPago"`) y con qué `tipo`, para definir el humo.
- No escribir código ni tocar `.cs`.

**Etapa B — implementación (solo tras la aprobación del usuario del documento de la etapa A):**
- Un commit por extracción, `refactor(venta-cierre): ...`; sin cambiar firmas públicas, campos públicos ni el orden de inicialización.
- Humo manual (VM) del plan: `tipo 3` ruta nueva y legacy, cobro desde `frmCobros`, `tipo 1` (cancelar pago), `tipo 5` y la rama `tipo 100`/10 si hay datos; comprobar tipo de cambio, moneda, título del formulario y modo captura iguales a antes.
- Orden: después de T14 (mismo archivo y `Load` llama al método que T14 modifica); antes de T10b y T11.
- [x] Etapa A: plan escrito en `docs/venta-cierre/refactor-frmCancelarPago-load.md`, pendiente aprobación del usuario.
- [x] Etapa B: implementada en `9a02211` (paso 1: quita `if` vacío) + `e6ee211` (paso 2: renombre a `es*` + `determinarModoCaptura`) + `c0cb41f` (paso 3: `cargarListasBase`) + `a0d830e` (paso 4: `cargarDevolucionPorLetras`) + `8bd0e93` (paso 5: `cargarDocumentoPorTipo` con `switch`) + `b845ef0` (paso 6: `cargarTipoCambio`) (2026-10-06). Sin cambios de comportamiento (diff revisado paso a paso), sin async, orden de inicialización intacto. Verificación: compilar en VM (lo hace claude; pasos 2 y 5 son los de mayor riesgo) y humo manual por `tipo` según la sección 5 del documento.

### T10a — Reserva y validación de nota de crédito en el servicio (antigravity, ~12 min)
- **Alcance:** `SIGEFA.InterMySql/VentaCierre/VentaCierreRepositorio.cs`, su interfaz, `SIGEFA.Administradores/VentaCierre/VentaCierreService.cs` y `VentaCierrePaso.cs`. Sin UI.
- Paso nuevo **antes de `guardarPago`** (orden estricto: serie → stock → cabecera → detalle → **notas de crédito** → pago → commit): por cada `notaCreditoId` distinto en los borradores, `SELECT pendiente FROM notacredito WHERE codNotaI=? FOR UPDATE` y validar que la suma de `montoCobrado` de los borradores que la usan sea `<=` pendiente. Ante falla: `VentaCierreException` con paso nombrado y rollback existente; registrar el nombre del paso en `obtenerNombreProcedimiento`.
- Sin cambios si ningún borrador lleva NC (ruta de efectivo idéntica).
- [x] Implementada en `180c427` (2026-10-06): paso nuevo `reservarNotaCredito` en `VentaCierrePaso` y `VentaCierrePasoTexto`; método `bloquearNotaCredito` en `IVentaCierreRepositorio` y `VentaCierreRepositorio` con `SELECT pendiente FROM notacredito WHERE codNotaI = @notaCreditoId FOR UPDATE` manejando `VentaCierreException`; validación en `VentaCierreService` (`ejecutarBloque` y `ejecutarOrdenAtomica`) antes de `guardarPago` bloqueando notas distintas en orden determinístico y validando `sumaCobradoNC <= pendienteActual`; sin cambios cuando ningún borrador lleva NC (ruta de efectivo idéntica); nombre de procedimiento asociado en `obtenerNombreProcedimiento`. Verificación: diff revisado, sin compilar en Linux; compilar en VM (B2) pendiente.

### T10b — Habilitar nota de crédito en captura (antigravity, ~8 min, después de T13d)
- **Alcance:** `frmCancelarPago.cs` (y opcionalmente `frmListaNCreditosSinAplicar.cs`).
- Incluir 10 en `esMetodoSoportado`.
- **Resetear `Pag.NotaCredito` y `Pag.CodNotaCredito` a 0** al capturar cualquier método distinto de 10 (hoy `Pag` nunca se limpia).
- Impedir elegir la misma NC dos veces en `borradoresPago` (o restar lo ya capturado del pendiente mostrado en la lista).
- No replicar `ActualizaPendienteCredito`/`ActualizaNCreditoVentaSinAplicar` (código muerto, ver Evidencia).
- [x] Implementada en `07c0519` (2026-10-06): habilitado método 10 (nota de crédito) en `metodosSoportadosEnCaptura` y en comentarios de `frmCancelarPago.cs`; validación en `cmbMetodoPago_SelectionChangeCommitted` impidiendo re-seleccionar una NC ya existente en `borradoresPago`; reseteo explícito de `Pag.NotaCredito = 0` y `Pag.CodNotaCredito = 0` al ejecutar cualquier método != 10 tanto en tipo 5 como en tipo 3/4; protección de llamadas legacy (`ActualizaPendienteCredito`, `ActualizaNCreditoVentaSinAplicar`, `insertPagoPendiente`) bajo `!modoCaptura` preservando intacto el flujo viejo. Verificación: diff revisado, sin compilar en Linux; compilar en VM (B2) y humo manual pendientes.

### T11 — Habilitar pendiente (12) en captura (opencode, ~5 min, después de T10b)
- **Alcance:** `frmCancelarPago.cs`.
- Incluir 12 en `esMetodoSoportado`. La confirmación "Esta seguro de cobrar con esta método de pago?" ya existe (l.663): conservarla.
- Al elegir 12 exigir `txtMontoPago == txtMontoPendiente` (12 siempre último borrador, supuesto 1); si no, mensaje claro y no capturar.
- Verificar que `frmVenta2019` no necesita cambios (el mapeo dice que no trata el 12 de forma especial) y que el cobro posterior desde `frmCobros` (`tipo=3`, `vieneDe="frmCobros"`) sigue funcionando con la venta creada por la ruta nueva.
- [x] Implementada en `7a59469` (2026-10-06): incluido 12 en `metodosSoportadosEnCaptura` (comentarios actualizados; el 7 queda solo en flujo viejo); validación en captura que exige `txtMontoPago == txtMontoPendiente` con mensaje claro y sin capturar si es parcial (12 siempre último borrador, supuesto 1); confirmación "Esta seguro..." y ruta legacy intactas; perfil de campos del 12 por el `default` del helper (como efectivo, T14); verificado por lectura que `frmVenta2019.cs` no trata el 12 de forma especial (sin cambios ahí). Verificación: no se compila en Linux; compilar en VM (B2) y humo manual pendientes: venta a pendiente y luego cobro desde `frmCobros`; comprobar `pago` tipo 12, `cajamovimiento` tipo 12 y `caja.totalpendiente`.

### T15 — Firma electrónica sin errores engañosos en "Después de guardar" (opencode, ~10 min, agregada 2026-10-06 por prueba del usuario en VM)
**Evidencia (prueba en VM, 4 incidencias tras guardar la venta):** (a) `ArgumentNullException ... parámetro: s` en `Convert.FromBase64String` dentro de `Facturacion.GeneraDocumento` (paso "Generar comprobante electrónico"); (b) `Unknown column 'PV.DocumentoReferenciaAnticipo'` en `ReporteFactura2` (paso "Imprimir comprobante"). (b) **ya lo corrigió el usuario en el procedure**; solo se confirma re-probando. (a) es esta tarea.

**Diagnóstico (por lectura de código, sin confirmar en la VM):** `Firmar()` (`SIGEFA.SunatFacElec/Facturacion.cs` ~l.886) lee `C:\DOCUMENTOS-<RUC>\CERTIFIK\<empresa.Certificado>`; ante cualquier falla (archivo ausente, clave inválida, `!Exito`) se traga el error con `MessageBox` y no avisa a quien la llama. `GeneraDocumento` sigue, y `Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado)` (l.270/271/277/278) recibe null. La causa real nunca llega al colector. `GeneraDocumento` tampoco hace `return` tras `!response.Exito` (l.241-252) y cae en `FromBase64String(response.TramaXmlSinFirma)` (l.254). Riesgo adicional a verificar: `respuestaFirmado` es un campo de instancia (l.41) que solo se reasigna si `Firmar` termina bien; si una misma instancia de `Facturacion` firma varios documentos (multialmacén, `frmVenta2019` ~l.3703) y uno falla, queda la trama firmada del documento anterior y se guardaría bajo el nombre del nuevo.

**Alcance:** solo `SIGEFA.SunatFacElec/Facturacion.cs`. Sin tocar `frmVenta2019`, servicio ni repositorio. Los otros llamadores de `Firmar()` (envío, nota de crédito/débito, l.409/681/823) conservan su comportamiento: el colector es opcional.

**Pasos:**
1. `Firmar(Action<string, Exception> colectorErrores = null)` devuelve `bool` (true solo si hay `TramaXmlFirmado` no vacía). Al inicio reinicia `respuestaFirmado = new FirmadoResponse()` (sin estado viejo). Validaciones previas con mensaje claro: `empresa.Certificado` vacío, certificado inexistente (indicar la ruta, sin la contraseña) y XML sin firma inexistente. Falla de firma o `!Exito` → mensaje con `MensajeError`. Con colector reporta al colector; sin colector conserva el `MessageBox` actual.
2. `GeneraDocumento`: pasa el colector a `Firmar`; si devuelve false, `return` (sin guardar repositorio ni PDF). Tras `!response.Exito` también `return`, y se valida `TramaXmlSinFirma` no vacía antes de `FromBase64String` (l.254).
3. Decisión adoptada (revertible): el `return` aplica también a la ruta legacy; la única diferencia es que desaparece el segundo `MessageBox` redundante que hoy sale del null, porque el código posterior ya no se ejecutaba tras esa excepción.
4. Sin cambios en `GeneraDocumentoEnvio`, `GeneraNotaCredito` ni `GeneraNotaDebito`.
5. **Mensajes controlados (decisión del usuario, 2026-10-06):** ningún error de este flujo puede llegar al usuario como `ArgumentNullException` ni como "El valor no puede ser nulo". Cada causa conocida tiene su texto, con el prefijo "No se pudo generar el comprobante electrónico:". Se muestra la ruta del certificado, nunca la contraseña. Un `catch` final con causa desconocida conserva el mensaje de la excepción, pero `ArgumentNullException` y `NullReferenceException` se reemplazan por "falta un dato obligatorio del comprobante (<paso>)".

| Causa | Mensaje al usuario |
|---|---|
| `empresa.Certificado` vacío | "la empresa no tiene configurado el certificado digital" |
| Archivo de certificado inexistente | "no se encontró el certificado digital en <ruta>" |
| Contraseña de certificado vacía | "la empresa no tiene configurada la contraseña del certificado" |
| XML sin firma inexistente | "no se encontró el XML generado en <ruta>" |
| El servicio de firma responde `!Exito` | "falló la firma digital: <MensajeError>" (o "sin detalle" si viene vacío) |
| Firma devuelve trama vacía | "la firma digital no devolvió el documento firmado" |
| Generación responde `!Exito` | "el servicio no generó el XML: <MensajeError>" (o "sin detalle") |
| Generación devuelve trama vacía | "el servicio no devolvió el XML sin firmar" |
| Faltan datos de la empresa | texto actual de l.218 |

Cada mensaje sale por el colector (con la excepción original cuando exista, para "Copiar detalle" y el log) o, sin colector, por el `MessageBox` actual con el mismo texto.

**TDD:** modo estricto configurado, pero el proyecto no tiene runner de pruebas; no aplica RED/GREEN. Verificación funcional ordinaria: build en la VM y humo manual.
- [x] Implementada en `4bbad19` (2026-10-06): `Firmar` devuelve `bool` con colector opcional, reinicia `respuestaFirmado` y valida con los mensajes de la tabla (certificado, contraseña, XML, `!Exito` con "sin detalle", trama vacía; nunca la contraseña); `GeneraDocumento` corta con `return` si falla la firma o `!response.Exito` y valida tramas antes de `FromBase64String`; `catch` final con `mensajeSeguro` (nulos → "falta un dato obligatorio"); sin cambios en `GeneraDocumentoEnvio`, NC ni ND. Verificación: no se compila en Linux; build en VM y humo (a)-(e) pendientes.
- **Pregunta abierta para el usuario:** ¿la VM debe tener el certificado en `C:\DOCUMENTOS-<RUC>\CERTIFIK\`, o allí la facturación electrónica se espera apagada? Define si en pruebas la incidencia clara es el resultado correcto.
- **Revisión de codex sobre `4bbad19` (2026-10-06, solo lectura, estática; no se compiló):** sin hallazgos P0/P1. Verificado: `Firmar` devuelve true solo con firma no vacía y reinicia el estado; `GeneraDocumento` corta ante `!Exito`, XML sin firma vacío o firma fallida; sin colector el mismo texto sale por `MessageBox`; `mensajeSeguro` reemplaza solo `ArgumentNullException`/`NullReferenceException` en el mensaje mostrado y el colector conserva la excepción original; sin ambigüedad entre la clase `Firmar` y el método; sin código muerto ni `return` faltante. Dos P2, **no corregidos** (seguimiento):
  1. **Contraseña (condicional, no verificado):** `PasswordCertificado` se asigna a la petición (~l.918-953) y `MensajeError` del servicio se concatena sin filtrar; si el servicio de firma devolviera la contraseña en su error, llegaría al mensaje o a "Copiar detalle". Los `EscribirLog` revisados usan textos fijos. Acción posible: enmascarar `empresa.Contrasena` en `MensajeError` antes de mostrarlo.
  2. **Otros llamadores ignoran el `false` de `Firmar`** (`GeneraDocumentoEnvio`, nota de crédito y nota de débito, ~l.402/674/816): tras una firma fallida muestran el mensaje controlado y luego su propio error por trama nula. Nota del plan: antes de T15 el campo ya nacía con trama nula (constructor), así que el segundo error ya existía; lo que cambia es que ya no se reutiliza la trama firmada de un documento anterior. Acción posible: que esos tres métodos corten con `return` si `Firmar` devuelve false (fuera del alcance de T15).

### T15b — Enmascarar la contraseña del certificado en los mensajes de firma (opencode, ~5 min, agregada 2026-10-06; P2 #1 de la revisión de codex sobre T15)
**Problema:** `empresa.Contrasena` viaja en `FirmadoRequest.PasswordCertificado` (`Facturacion.cs` ~l.918). Si el servicio de firma devolviera esa clave dentro de `MensajeError` o de una excepción, llegaría al `MessageBox`, al colector y a "Copiar detalle". Es condicional y no está verificado, pero cerrarlo cuesta casi nada.

**Alcance:** solo `SIGEFA.SunatFacElec/Facturacion.cs`, solo el flujo de T15 (`informarComprobante` y sus llamadores). No tocar `GeneraDocumentoEnvio`, nota de crédito ni nota de débito.

**Diseño (un único punto de salida):**
1. Método privado estático `ocultarContrasena(string texto, string contrasena)`: si `texto` o `contrasena` son null o vacíos devuelve `texto` sin cambios; si no, reemplaza **todas** las apariciones de `contrasena` por `"***"` con comparación ordinal (`StringComparison.Ordinal`), sin expresiones regulares.
2. `informarComprobante` (~l.948): enmascara el `mensaje` final con `empresa.Contrasena` antes de enviarlo al colector o al `MessageBox`. Así quedan cubiertos todos los textos de T15 (incluidos `MensajeConDetalle` y `mensajeSeguro`) sin tocar cada llamador.
3. La excepción `causa`: el colector la usa para "Copiar detalle" y el log. Si `causa` no es null y su `ToString()` contiene la contraseña, enviar en su lugar `new InvalidOperationException(ocultarContrasena(causa.Message, ...))` **sin excepción interna**, con el nombre del tipo original en el texto (`"[" + causa.GetType().Name + "] " + mensajeEnmascarado`). Si no la contiene, pasar `causa` intacta (conserva la traza útil).
4. No loguear, concatenar ni formatear `empresa.Contrasena` en ningún otro lugar. Revisar que ningún `EscribirLog` del flujo la incluya (codex no encontró ninguno).

- [x] Implementada en `46f6e01` (2026-10-06): método `ocultarContrasena` sin expresiones regulares (`StringComparison.Ordinal`); `informarComprobante` enmascara el mensaje final con `empresa.Contrasena`; sanitización de `causa` sustituyéndola por `InvalidOperationException("[" + causa.GetType().Name + "] " + mensajeEnmascarado)` sin excepción interna si su `ToString()` expone la contraseña, o preservándola intacta en caso contrario; sin tocar otros flujos ni P2 #2. Verificación: diff revisado, sin compilar en Linux; build en VM y humo pendientes.
- **Revisión de codex sobre `46f6e01` (2026-10-06, solo lectura, estática, esfuerzo high):** sin hallazgos P0-P3. Verificado (`Facturacion.cs` ~l.947-990): reemplazo ordinal de todas las apariciones; el mensaje se enmascara antes del colector y del `MessageBox`; la excepción se sustituye solo si su `ToString()` contiene la contraseña; sin concatenación ni log de `empresa.Contrasena`; sin contraseña en el texto los mensajes son idénticos a T15; `GeneraDocumentoEnvio`, NC y ND intactos; `git diff -w` solo `Facturacion.cs`. No verificado: compilación y ejecución.

### Seguimiento sin tarea (decisión del usuario, 2026-10-06): P2 #2 de la revisión de T15
Los tres métodos que siguen llamando `await Firmar();` (`GeneraDocumentoEnvio`, nota de crédito, nota de débito) ignoran el `false` y, tras una firma fallida, muestran su propio error por trama nula además del mensaje claro. **No se corrige ahora:** el segundo error ya existía antes de T15, están fuera del flujo de venta nueva y no se pueden probar bien en la VM. Cuando se retome: que cada método corte con `return` si `Firmar` devuelve `false`.

### T16 — Diálogo de progreso alineado con el enum `VentaCierrePaso` renumerado (antigravity, ~10 min, hallazgo P2 de codex, 2026-10-06)
**Evidencia (codex, verificado por lectura; yo revisé las líneas citadas):** T10a insertó `reservarNotaCredito = 5` y movió `guardarPago` a 6 y `confirmar` a 7 (`VentaCierrePaso.cs`). `frmVentaCierreProgreso.cs` no se actualizó: con un solo bloque usa `(int)paso` como índice de fila (`obtenerIndiceFilaTransaccional`, ~l.228-233) sobre 7 filas construidas desde `pasosOrdenados` (~l.78-87), que no incluye `reservarNotaCredito`. Resultado: `guardarPago` (6) marca la fila "Confirmar", `confirmar` (7) queda fuera de rango y `reservarNotaCredito` (5) marca "Guardar pago". Con varios bloques, `reservarNotaCredito` cae en `default: return 0` y marca "Abrir transacción". `obtenerTotalFilasTransaccionales` (~l.216) tiene el 7 fijo y `3 + bloques * 3 + 1`. Es solo visual: no afecta datos ni commit, pero un error de NC se mostraría bajo la etapa equivocada.

**Alcance:** solo `SIGEFA.Formularios/frmVentaCierreProgreso.cs`. No tocar `VentaCierrePaso.cs`, servicio ni repositorio.

**Diseño:**
1. Agregar `VentaCierrePaso.reservarNotaCredito` a `pasosOrdenados`, entre `guardarDetalle` y `guardarPago` (8 filas con un bloque; el texto sale de `VentaCierrePasoTexto`, que ya lo tiene).
2. Un solo bloque: el índice de fila sale de `Array.IndexOf(pasosOrdenados, paso)`, **nunca de `(int)paso`**. Si el resultado es -1, no marcar nada (no caer en la fila 0).
3. Varios bloques: cada bloque pasa de 3 a 4 filas (cabecera, detalle, nota de crédito, pago). Fórmulas: base del bloque = `3 + (bloque - 1) * 4`; cabecera `+0`, detalle `+1`, nota de crédito `+2`, pago `+3`; `confirmar = 3 + bloques.Count * 4`. Total de filas = `3 + bloques.Count * 4 + 1`. Quitar el `default: return 0`: un paso desconocido devuelve -1.
4. `obtenerTotalFilasTransaccionales` debe derivarse de `pasosOrdenados.Length` (un bloque) y de la fórmula anterior (varios); sin el 7 fijo. Revisar y mantener coherentes los usos de ese total en `offsetPost` (~l.261 y ~l.615) y en ~l.433. Crear las filas del modo multibloque (~l.470 en adelante) con la misma estructura de 4 filas por bloque.
5. Todo `marcarPasosHasta` y `marcarPasoTransaccionalConError` deben tolerar índice -1 sin excepción.
6. La fila "Reservar nota de crédito" está siempre presente; si no hay notas de crédito, el servicio no la informa y queda "Listo" cuando se informa un paso posterior (comportamiento actual de `marcarPasosHasta`).

**Convenciones:** las de `AGENTS.md` (guardas, sin `if` anidados de tres niveles, sin repetir cálculos: guardar el índice en una variable local). Sin `MessageBox` nuevo.
**TDD:** no aplica (sin runner de pruebas). Verificación: build en la VM y humo manual.
- [x] Implementada en `55b532a` (2026-10-06, claude): `reservarNotaCredito` agregado a `pasosOrdenados` (8 filas con un bloque); índice con `Array.IndexOf` (sin `(int)paso`); varios bloques con 4 filas por bloque, `confirmar = 3 + bloques * 4`, total `3 + bloques * 4 + 1`, `default` devuelve -1; `marcarPasosHasta` corta si el índice es -1 (`marcarPasoTransaccionalConError` ya lo toleraba); fila "Reservar nota de crédito (almacén)" creada en el modo multibloque. Sin compilar en Linux; build en VM y humo pendientes.
- **Revisión de codex sobre `55b532a` (2026-10-06, solo lectura, estática, esfuerzo high):** sin hallazgos P0-P3. Verificado: 8 filas, sin `(int)paso`, fórmulas multibloque coherentes con `inicializarListaPasos`, índice -1 sin excepción, `offsetPost` coherente (~l.266 y ~l.631), `Designer` sin cambios, `git diff -w` solo el archivo previsto. No verificado: compilación y comportamiento en ejecución.
- **Build en la VM (2026-10-06, `Debug|x86`, `sigefa_build`, MSBuild 18):** sobre `7ac27b8` falló con 2 errores `CS0246` (`StringBuilder`, `Facturacion.cs:958`, de T15b: faltaba `using System.Text`; la revisión estática de codex no lo detectó). Corregido en `4001660`; recompilado: `exit=0`, 0 errores, 21 s. Compilar no prueba funcionamiento: el humo manual sigue pendiente.
- Humo manual pendiente: (a) venta contado de un almacén solo efectivo: 8 filas, avanzan en orden y "Confirmar" queda en Listo al final; (b) con nota de crédito: la fila "Reservar nota de crédito" pasa a En curso y luego Listo, y "Guardar pago" no se adelanta; (c) NC con saldo insuficiente (forzar): el error aparece **bajo "Reservar nota de crédito"**; (d) dos almacenes con NC: 4 filas por bloque y el error de la NC del bloque 2 cae en la fila del bloque 2.

### Hallazgo P1 de codex — pendiente de decisión del usuario (2026-10-06)
Con un solo almacén, el cheque (7) sale al flujo viejo (`frmCancelarPago.cs` ~l.698-704), que persiste comprobante y pago durante "Cobrar" (~l.724-744 y ~l.1403-1414). Si se registra un pago parcial y se cierra la ventana, `FormClosing` marca cancelado (~l.2180-2185), `frmVenta2019` aborta antes de agregar la venta a `lista_facturas` (~l.3669-3683) y el `catch` no la compensa (~l.3928-3933): quedan comprobante y primer pago guardados aunque el cierre se reporte cancelado. Opciones: (1) bloquear el 7 en la ruta nueva con aviso claro; (2) aceptar la excepción y documentarla. **Sin decidir; no hay tarea hasta que el usuario elija.**

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
