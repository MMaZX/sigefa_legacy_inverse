# venta-cierre-bitacora

Estado de reanudación: T1–T4 escritas; T3 incluye la corrección `5383dcc` (reintento ante 1062 y 1213). Suite aislada ejecutada en VM: 248 correctas, 36 integración omitidas, 0 fallidas (284 total). Build de la aplicación aún pendiente; host sin compilador. Migración aplicada en dev según confirmación del usuario; no aplicar ni consultar producción. Rama `feat/venta-cierre-bitacora`, sincronizada con origin al retomar. Mirror Engram: `odd/venta-cierre-bitacora/tasks`.

## Objetivo

Guardar en tablas de la BD el paso a paso de cada intento de cerrar una venta (ruta nueva), por pedido, con intento incremental, horas con milisegundos y el detalle del fallo, para que el jefe y el usuario lo revisen con SQL desde el servidor.

## Pedido del usuario (2026-10-10)

1. El jefe pidió un log por orden de pedido `numero_orden_pedido.N.log` (N = intento). El usuario prefiere **una tabla** para consultar desde el servidor; el archivo queda solo como respaldo si la BD falla.
2. Optimizado: sin cientos de archivos ni escrituras por evento; horas y fechas correctas por pedido.
3. Implementar rápido. La migración la crea la sesión `gr-backend` (repo `~/www/gruporicardoapi`); aquí se arma primero la implementación.

## Decisiones tomadas

- Dos tablas: `venta_cierre_log` (un intento) y `venta_cierre_log_evento` (pasos). DDL en el mensaje enviado a `gr-backend`; no hay FK de `cod_pedido` a propósito.
- **Conexión aparte en autocommit**, nunca la transacción del cierre: un rollback borraría el log del error.
- Cabecera al inicio (`EN_CURSO`), eventos en memoria y un solo INSERT múltiple al final junto con el UPDATE de cabecera (`OK`/`ERROR`). Una app caída deja `EN_CURSO`.
- Intento = cada clic en "Cerrar venta" del pedido (supuesto del orquestador, el usuario no lo objetó); `UNIQUE (cod_pedido, intento)` y reintento si hay choque.
- Solo ruta nueva (`VentaCierreRuta=nueva`); `VentaCierreService` y `frmVentaCierreProgreso` no cambian: un `IProgress<VentaCierreProgreso>` decorador registra.
- Credenciales enmascaradas con `VentaCierreRegistroErrores.enmascararCredenciales`.
- Si la BD falla: respaldo en `documentos/SIGEFA_LOGS/<pedido>.<intento>.log` o en el log local de errores (a confirmar en T2); la bitácora nunca interrumpe ni cambia el resultado del cierre.
- Nunca se aplica DDL en producción desde aquí.
- **Número de orden de pedido = `pedidosventa.codPedido`** (usuario, 2026-10-10): el que se teclea en la pantalla de venta y se arma directamente en la tabla.
- **Una fila de log por pedido (usuario, 2026-10-10, opción 1):** un cierre con varios pedidos crea un intento por cada `codPedido`. T2 no cambia (un `BitacoraCierre` es de un pedido); T4 crea una instancia por pedido y reparte: los eventos globales (`abrirTransaccion`, `bloquearSerie`, `bloquearStock`, `confirmar`) van a todas, y los de bloque solo a la del pedido de ese bloque; el resultado final y el fallo se aplican a todas (la transacción es una sola, un fallo afecta a todos los pedidos del cierre).
- **Versión de la app = fecha de escritura del ejecutable** (usuario, 2026-10-10, opción 1): `yyyy-MM-dd HH:mm` de `File.GetLastWriteTime` del `.exe`; T4 la calcula y la pasa en `IntentoBitacora.VersionApp`. `AssemblyVersion` sigue fija en 1.0.0.0 y no se toca. Equipo = `Environment.MachineName`; usuario = `frmLogin.iCodUser` / `sUsuario`.
- **Seam A aprobado (usuario, 2026-10-10):** `ejecutarOrdenAtomica` pasa a `virtual` y una subclase `VentaCierreServiceConBitacora` registra el paso a paso; `frmVenta2019:3748` cambia su `new`. Es la única edición permitida en código existente (T4).

## Decisiones abiertas

- D1: ¿el archivo `documentos/SIGEFA_LOGS` solo como respaldo, o siempre (lo pidió el jefe)? Se asume solo respaldo.
- D2: de dónde salen usuario, equipo y versión en el punto de conexión (T1 lo mapea).

## Tareas

| ID | Tarea | Ruta | Depende de |
|---|---|---|---|
| T1 | Mapa: punto de conexión del cierre (quién llama a `VentaCierreService`, dónde está el pedido, usuario, equipo, versión, flag), convenciones de repositorio y de pruebas | delegada (mapper, solo lectura) | — |
| T2 | Servicio `VentaCierreBitacora` (DTOs puros, `IBitacoraRepositorio`, decorador de `IProgress`), pruebas RED/GREEN y respaldo en archivo | delegada (writer) | T1 |
| T3 | Repositorio MySQL (conexión aparte, autocommit, INSERT múltiple, intento con reintento por `UNIQUE`) e integración con rollback/limpieza en dev | delegada (writer) | T1, migración |
| T4 | Conectar en la ruta nueva del cierre (detrás del flag) | delegada | T2, T3 |
| T5 | Auditoría, build y suite en la VM, consultas de revisión | claude | cada tarea |
| M | Migración de las dos tablas | `gr-backend` | pedido enviado 2026-10-10 |

## Migración (respuesta de `gr-backend`, 2026-10-10)

Lista y probada en dev, **sin commit en `gruporicardoapi` y sin aplicar a producción**: `~/www/gruporicardoapi/database/migrations/2026_10_10_130000_create_venta_cierre_log_tables.php` (up y down). DDL final = el borrador, con InnoDB y `utf8mb4_unicode_ci`; `id` y `log_id` son `BIGINT UNSIGNED`; los demás enteros `INT` firmados; `DATETIME(3)` en `inicio`, `fin` y `hora`. Índices: `uq_venta_cierre_log_pedido_intento` (UNIQUE `cod_pedido, intento`), `idx_venta_cierre_log_inicio`, `idx_venta_cierre_log_estado_inicio`, `idx_venta_cierre_log_evento_log_orden`; FK `fk_venta_cierre_log_evento_log` con `ON DELETE CASCADE`, sin FK en `cod_pedido`, sin triggers. Probada con `migrate` y `migrate:rollback` en el contenedor `manager_php_dev`: **dev quedó sin las tablas** (revertida). Para las pruebas de integración de T3 hay que aplicarla en dev: `php artisan migrate --path=<archivo>` y revertirla igual al terminar. Retención: sin particiones (la FK con cascade lo impide); purga `DELETE FROM venta_cierre_log WHERE inicio < NOW() - INTERVAL 90 DAY` (en lotes con `LIMIT` si crece), por comando o evento programado, **no creado**. Aviso: reintentar el mismo `(cod_pedido, intento)` da error 1062; calcular `MAX+1` y reintentar ante 1062.

## T1 hecha: mapa del punto de conexión (claude, solo lectura, 2026-10-10)

- **Único llamador de la ruta nueva:** `frmVenta2019.cs:3748-3750` (`new VentaCierreService()` y `new frmVentaCierreProgreso(...)`, rama `ventaCierreRutaGlobal == "nueva"` en 3577). El formulario ejecuta `servicio.ejecutarOrdenAtomica(bloques, progreso)` en `Task.Run` (`frmVentaCierreProgreso.cs:142`); el `IProgress` lo crea el propio formulario (139). `ejecutarOrdenAtomica` (`VentaCierreService.cs:475`) abre **una sola transacción para todos los bloques** y un solo Commit (710). Por eso **no se puede interceptar el paso a paso sin tocar una línea**: el método no es `virtual`.
- **Eventos (`reportarProgreso`):** `abrirTransaccion` 558, `bloquearSerie` 569, `bloquearStock` 585, `guardarCabecera` 619, `guardarDetalle` 635 (por ítem), `reservarNotaCredito` 662, `guardarPago` 693, `confirmar` 708 (antes del Commit). Se emiten **antes** de cada paso, no hay evento al terminar: la hora de fin se toma al retornar o lanzar. Los pasos post-cierre (despacho, código de barras, comprobante electrónico, impresión) no pasan por `IProgress`: quedan en `erroresPostCierreObtenidos` (datos opcionales).
- **Datos:** pedido `bloque.venta.CodPedido` (int, uno por bloque; un cierre puede traer varios pedidos); usuario `frmLogin.iCodUser` y `frmLogin.sUsuario`; equipo `Environment.MachineName` (hoy no se usa); versión `AssemblyVersion` fija en 1.0.0.0 (no sirve); almacén `bloque.almacenNombre`; `facturaVentaId` y `numeroDocumento` solo en `VentaCierreResultado` al final (correspondencia por índice).
- **Seams posibles:** (A) hacer `ejecutarOrdenAtomica` `virtual` y heredar `VentaCierreServiceConBitacora` (cambia una palabra en el servicio y el `new` en `frmVenta2019:3748`); (B) usar el parámetro `registrarError` del constructor `internal` (solo da el fallo, sin paso a paso ni éxito).
- **Riesgos:** el decorador de `IProgress` debe registrar en su propio `Report` (hilo del servicio) y nunca lanzar; el INSERT final va en `finally` por la conexión aparte; fallos de preparación (serie, fecha, cobro: `frmVenta2019` ~3602-3693) ocurren antes del servicio y no se verían; `SIGEFA.Tests.csproj` solo enlaza `Db` y `ReqVenta`: lo puro va en una carpeta nueva enlazada (p. ej. `SIGEFA.Administradores/VentaCierreBitacora/`).
- **Respaldo en archivo:** `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "documentos", "SIGEFA_LOGS")`, creada bajo demanda.
- **Decisiones que faltan:** seam A o B; `cod_pedido` de un cierre con varios pedidos; qué es el "numero de orden de pedido" del jefe (`codPedido` o `numeracion`); fuente de la versión.

## T2 entregada y auditada (2026-10-10)

Escrita, **no verificada en compilador**: RED `a2aab24`, feat `6ce5e85` (carpeta `SIGEFA.Administradores/VentaCierreBitacora/`, ~580 líneas; pruebas ~720 líneas; 3 `Link` en `SIGEFA.Tests.csproj`). Incluye `BitacoraCierre` (nunca lanza, modo respaldo), `ProgresoConBitacora` (decorador con `DesdeExito`/`DesdeFallo`/`DesdeExcepcion` para T4), `RespaldoArchivo`. El formato del archivo de respaldo lo definió el escritor (`RespaldoArchivo.ArmarTexto`); ajustar ahí si el jefe quiere otro.
**Auditoría independiente: APROBADO CON CORRECCIONES** (a ojo de compilador; nada ejecutado). Verificado: símbolos de las pruebas, `Link`/`LangVersion`, contrato de no lanzar en todas las rutas, orden/hora coherentes bajo lock, idempotencia, patrón de credenciales idéntico al existente. Correcciones (una sola ronda, delegada): (1) media, carrera `File.Exists`/`WriteAllText` en `RespaldoArchivo` → `FileMode.CreateNew` con reintento; (2) quitar BOM; (3) limpiar/topear `almacen`, `paso`, `Usuario`, `Equipo`, `VersionApp`, `ErrorPaso`, `ErrorProcedimiento`, `ErrorSqlState`; (4) pruebas de enmascarado antes del tope y de constructor con nulos; (5) detalles de prueba. **Requisito para T3/T4:** `Iniciar` se llama antes de lanzar el servicio y en el mismo hilo (mantiene el lock durante `CrearIntento`); el repositorio recorta cada campo al ancho de su columna (con `STRICT_TRANS_TABLES` un texto largo falla el INSERT y el intento caería al respaldo).

## Verificación

Host sin `dotnet`: lo escrito se reporta como "escrito, no verificado en compilador". Runner de pruebas: `dotnet test` en la VM (`sigefa_build`, con autorización explícita). TDD estricto (fuente: configuración del proyecto). Regla de oro heredada: toda sentencia SQL nueva se verifica contra la BD dev real antes de darla por buena.

## T5 en curso: autorización y verificación de reanudación

- El usuario autorizó explícitamente build `Debug|x86` y suite en la VM `sigefa_build`, y confirmó `SIGEFA.exe` cerrado. Comprobar el proceso antes de ejecutar; si está abierto, detenerse sin cerrarlo automáticamente.
- Ruta: verificación delegada a `gentle-ai-verify` por runner externo y auditoría independiente T4 delegada en solo lectura. No ejecutar compilación en el host (no tiene `dotnet`).
- [ ] T5-VM: build `Debug|x86` y suite en curso de preparación. VM con `SIGEFA_PROCESS_COUNT=0`, checkout limpio detached `e310dd74b70b75cb135ceb2d94eb129f98e8318f`, distinto del host `5383dcce8965f873baef7d254c78ff8ea6279a61`. No validar checkout obsoleto. El padre autorizó preparar snapshot exacto del commit en directorio nuevo separado (archive + SHA256), sin cambios ajenos, sin sobrescribir checkout ni usar wrappers históricos de fetch/checkout. Ejecutar MSBuild y dotnet subyacentes con cwd aislado y DLLs existentes; integración omitida mientras no se prueben conexión dev y limpieza. Snapshot materializado en `C:/Users/qemu/Documents/sigefa_verify_5383dcc_20261010T235008Z`: 1795 blobs del commit verificados por SHA256, cero diferencias. Primer MSBuild `/t:Rebuild /p:Configuration=Debug /p:Platform=x86 /v:q /nologo`: exit 1, `NETSDK1004` por `obj/project.assets.json` inexistente en snapshot fresco; sin ejecución de compilador. Padre autorizó restore aislado solo desde cache/feed local existente, sin downloads remotos ni instalaciones, seguido de mismo rebuild y suite sin conexión de integración. Primera suite autorizada `dotnet test SIGEFA.Tests --nologo --logger "console;verbosity=normal"`, `SIGEFA_TEST_CONN` unset: exit 0, 284 total, 248 correctas, 36 integración omitidas, 0 fallidas, 2.0955s. Pruebas de reflexión MySqlException 1062/1213 correctas. Warning `CS0649 ReqVentaAprobacion.cs(125,24)`. Restore implícito tests exitoso 2.17s; log sin downloads, pero no se limitaban feeds en esa ejecución anterior a la instrucción de cache local. Build aplicación y repetición tests `--no-restore` aún pendientes. No extrapolar a integración ni a compilación de la aplicación.
- [x] T5-AUD: auditoría independiente completada, **requiere correcciones**; no es aprobación de T4. Confirmados inicio sincrónico previo al servicio en su mismo hilo, preservación de instancia de resultado/excepción, pedido distinto único y ruta legacy intacta (`git diff -w` contra merge-base `832399d77c2ab418056dbb068d4820105aedb20e`). No ejecutó pruebas/build/BD.
- [ ] T5-FIX: **autorizada por el usuario**, en curso (ruta delegada, cambios en varias fuentes/pruebas). Alcance acotado: discriminación por `paso` y metadata de equipo protegida; sin extraer orquestación ni tocar cleanup del base. TDD: RED de comportamiento en VM y commit, luego GREEN/implementación y commit en español; sin push. Para metadata se permite introducir un helper con proveedor inyectable inicialmente sin protección (seam que preserva comportamiento), usado por el mismo punto de la ruta nueva, para observar RED real antes de añadir el fallback. No presentar ese seam inicial como corrección. Pruebas deben cubrir proveedor que lanza, normal/nulo y cada paso global/de bloque con almacén `Global`. (1) `ProgresoPorPedido.cs:77` usa nombre de almacén `Global`: colisión con datos reales mezcla eventos de bloque entre pedidos. Discriminar por pasos `abrirTransaccion`, `bloquearSerie`, `bloquearStock`, `confirmar`; los demás eventos de bloque se enrutan por índice. (2) `frmVenta2019.cs:3748` obtiene `Environment.MachineName` sin guardia: un error de metadata impide iniciar el cierre. Protegerlo con fallback, sin tocar legacy.
- [ ] T5-MANUAL: usuario cierra una venta y consulta cabecera/eventos por cada pedido.
- Cobertura pendiente: pruebas actuales no enlazan `VentaCierreServiceConBitacora`, precrean/finalizan intentos; no prueban orquestación real. Extraer helper puro para esa cobertura es propuesta de mayor alcance, no autorizada. Riesgos secundarios estructurales: creación de decorador fuera de fallback y conversión con guardia de todo el loop. Excepción de limpieza del base después de Commit es caveat preexistente, no cambiar semántica legacy en esta corrección.
- No tocar ni incluir los cambios ajenos de requerimientos/textos ni `graphify-out`; no modificar `main` ni producción. Commits TDD autorizados solo para corrección T5-FIX en esta feature (Conventional Commits en español, sin atribución de IA); ningún push autorizado en esta reanudación. Build/suite exclusivamente VM y con proceso SIGEFA cerrado.
- Toda sentencia SQL nueva exige contrastar tablas/procedimientos en dev real. No dejar filas de prueba; no ejecutar integración con efectos persistentes sin identificar su aislamiento y limpieza.
- Evidencia de reanudación: `git log` confirma `5383dcc`; el mirror anterior solo contenía el diseño inicial y se reconcilia con este documento y los commits.

## Siguiente paso

**Estado al cierre (2026-10-10):** T1, T2 (+corrección `856ad2a`/`358d093`), T3 (`e5030a1`/`2d4b57b`) y T4 (`31004b4`, `ca25a07`, `5877225`) escritas, **ninguna compilada**; rama `feat/venta-cierre-bitacora` subida a `origin`, `main` intacto. Migración aplicada por el usuario en dev (tablas vacías). Pendiente: (1) leer el informe de la auditoría de T3 (agente `a9b2496a3f702c80b`; si se perdió, repetirla sobre `e5030a1`/`2d4b57b` y la corrección de T2); en particular decidir si se reintenta también ante deadlock 1213; (2) build `Debug|x86` y suite en la VM `sigefa_build` (autorización explícita; `SIGEFA.exe` cerrado) — primer sospechoso si falla: el `Link` de `VentaCierreBitacora\*.cs` en `SIGEFA.Tests.csproj` y el constructor por reflexión de `MySqlException` en las pruebas de T3; (3) prueba manual: cerrar una venta y consultar `SELECT * FROM venta_cierre_log WHERE cod_pedido = <pedido> ORDER BY intento;`; (4) T4 sin auditoría independiente; (5) cambios ajenos sin commitear en el working tree (`ReqVentaTextos.cs`, `FrmTPenPedido.cs`, `frmReqAlmacen.cs`, pruebas de textos) no pertenecen a esta feature.
