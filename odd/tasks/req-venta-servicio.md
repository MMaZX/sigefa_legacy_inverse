# req-venta-servicio

Estado: T1 verificada en la VM (2026-10-06); HintPath corregido pendiente de reverificar. T2a y T2b escritas sin verificar en compilador (2026-10-06, commits cca2e76/da1cf8b y 6f7fa04/3f2f6cc); T2c en adelante pendientes.
Espejo Engram: tópico `odd/req-venta-servicio/tasks` (proyecto `sigefa_legacy_inverse`).
Rama: `feat/req-venta-servicio` (desde `main`).

## Objetivo

Reemplazar la lógica dispersa de anulación de requerimientos de almacén de venta (`FrmTPenPedido.btnEliminar_Click` y sus 6 copias) por un servicio único, plano (guard clauses), atómico (una conexión y un `MySqlTransaction`) y verificable, detrás de un flag. La primera entrega es el helper de consultas, estilo Query Builder, que el servicio usará.

## Problema (evidencia, BD dev + código)

- `AnularRequerimientoAlmacen` es incondicional (`estado=12`, sin validar estado previo).
- `btnEliminar_Click` (`SIGEFA.Formularios/FrmTPenPedido.cs:228-343`) anula **antes** del extorno, sin transacción común; el mensaje de éxito depende solo de `anular`.
- 7 puntos de entrada anulan requerimientos con reglas de estado distintas (FrmTPenPedido, frmPedidosPendientes, frmVenta2019:3096, frmVentas:631, frmNotadeCredito:1334 y :1523, frmReqAlmacen:1072).
- Datos dev (`estado=12`, `tipo_req=2`): 2 aprobadas sin extorno (req 6318, 10462; S/ 7,80), 7 con extorno incompleto, 4 pendientes sin extorno, 18 extornos pendientes huérfanos sin nota de salida ni de ingreso.
- Cada capa abre y cierra su propia conexión; no hay `MySqlTransaction` en ningún método del flujo.

## Restricciones (obligatorias)

1. Ruta legacy intacta; todo nuevo comportamiento detrás del flag `VentaCierreRuta=nueva` y verificado con `git diff -w`.
2. Sin ifs anidados de tercer nivel: guard clauses, `switch` por estado, métodos con nombre.
3. SQL siempre parametrizado; sin concatenar valores.
4. Sin `MessageBox` en la lógica nueva; errores como resultado y registrados (`VentaCierreRegistroErrores`).
5. `net461` + `LangVersion latest`: **no usar `ValueTuple`** (parámetros con objeto anónimo).
6. `SIGEFA.csproj` es SDK-style con globbing: el proyecto de pruebas debe quedar fuera del glob (`Compile Remove`) o en carpeta hermana excluida.
7. Conventional Commits, sin `Co-Authored-By` ni atribución de IA. Push, PR y merge los decide el usuario.
8. El host Linux no tiene `dotnet`: compilar y probar solo en la VM Windows, con autorización explícita del usuario para destino, operación y credencial.

## Decisiones

- Opción A (decidida por el usuario, 2026-10-06): servicio con una conexión y un `MySqlTransaction`; SQL inline parametrizado.
- Helper estilo Query Builder (decidido): `Db.Consultar(sql, new { ... }).Get()` / `.First()`, `Db.Ejecutar(...)` -> `ResultadoEjecucion { Id, FilasAfectadas, Ok }`, `Db.Transaccion(tx => ...)`.
- Vacíos (decidido): `Get()` devuelve lista vacía, nunca `null`; `First()` devuelve `null`; NULL de BD -> `null` en el diccionario; clave inexistente lanza excepción clara; claves `OrdinalIgnoreCase`; método de extensión `Valor<T>(clave, defecto)`.
- Confirmado (2026-10-06): estados que permiten anular = 7 y 13, solo `tipo_req=2`.
- Confirmado (2026-10-06): si un extorno falla, la anulación NO se termina de hacer: rollback total, el requerimiento queda como estaba y el usuario ve el error.
- Confirmado (2026-10-06): entrega en PR encadenados con estrategia **stacked-to-main** (cada PR entra a `main` en orden; no se mezcla otra estrategia). Cortes en la sección "Evidencia y entrega".

## TDD

- Modo: estricto (configuración de la sesión). Fuente: configuración del orquestador + elección explícita del usuario ("arrancá con el helper y las pruebas").
- Runner: **xUnit sobre net48, ejecutado en la VM Windows** (no existe en el repo ni en el host). El proyecto de pruebas enlaza los fuentes del helper con `<Compile Include>` en vez de referenciar `SIGEFA.exe`, para no arrastrar Crystal ni WinForms.
- RED/GREEN solo observables en la VM; no se declara "pasa" ni "compila" sin esa evidencia.

## Tareas

Cada tarea cierra con un commit de unidad de trabajo y registra su hash y evidencia observada.

### T1 — Helper de consultas con pruebas
Archivos nuevos en `SIGEFA.Conexion/Db/`: `IConsultor.cs`, `Consulta.cs`, `ResultadoEjecucion.cs`, `ConsultorMySql.cs`, `Db.cs`, `FilaExtensiones.cs`. Proyecto `SIGEFA.Tests/` (net48, xUnit) fuera del glob de `SIGEFA.csproj`.
- [x] T1a — Pruebas sin BD (RED): conversión de objeto anónimo a parámetros, `DBNull` <-> `null`, `Get()` vacío, `First()` nulo, `Valor<T>`, clave inexistente.
  - Escrito (no verificado en compilador). Commit `7d2ad82`.
- [x] T1b — Implementación del helper (GREEN) y exclusión del proyecto de pruebas en `SIGEFA.csproj`.
  - Escrito (no verificado en compilador). Commit `4f1de18`. Namespace de pruebas renombrado a `SIGEFA.Tests.Helper` para no chocar con la clase `Db`.
- [x] T1c — Pruebas de integración contra la BD dev con tabla `TEMPORARY` y transacción con rollback: `Id` en INSERT, `FilasAfectadas` en UPDATE, commit y rollback de `Transaccion`, parámetros contra inyección (`' OR 1=1`), columnas `bit(1)`.
  - Escrito (no verificado en compilador, no ejecutado). Commit `e76bfb2`. Desviacion: usa tabla real `zz_test_db_<guid>` con DROP en Dispose (el DDL fuera de transaccion) en vez de `TEMPORARY`, y la cadena sale solo de `SIGEFA_TEST_CONN`.
- [x] T1d — Build y pruebas en la VM Windows (autorizado por el usuario, 2026-10-06).
  - **Resultado (VM `sigefa_build`, rama `feat/req-venta-servicio`, `3747e93`):**
    - RED sobre `7d2ad82`: `dotnet build SIGEFA.Tests` falla con 4 errores `CS0234` (`SIGEFA.Conexion` no existe en las 4 clases de pruebas): RED válido.
    - GREEN: MSBuild `Debug|x86` de `SIGEFA.csproj`, `exit=0`, 0 errores, 480 advertencias, 33,5 s. Los 7 archivos de `Db/` compilan en el ejecutable; `SIGEFA.Tests\**` queda fuera.
    - Pruebas sin `SIGEFA_TEST_CONN`: 47 totales, 32 correctas, 0 falladas, 15 omitidas (todas de integración).
    - Integración contra la BD dev del host (Docker `manager_mysql`, vía `192.168.122.1:3307`, nunca producción): 47 de 47 correctas, 0 omitidas. Sin tablas `zz_test_db_*` residuales.
    - `BIT(1)` con `MySql.Data 8.0.16.0`: llega como `System.UInt64` (valores 1 y 0); `Valor<bool>` lo admite.
  - **Defecto hallado:** `HintPath` de `MySql.Data.dll` en `SIGEFA.Tests.csproj` apuntaba a `..\Debug_gr`; el proyecto está un nivel más abajo y debe ser `..\..\Debug_gr`. Corregido; **pendiente reverificar el build en la VM** (la verificación se hizo con una copia temporal de la DLL).
  - Compilar y pasar pruebas del helper no prueba el flujo de negocio.
- Ruta: delegated direct, un solo escritor (6+ archivos no triviales). Disparador: Writer trigger.

### Principio rector del servicio (decidido por el usuario, 2026-10-06)
**La vista nunca aporta el estado.** Hoy `btnEliminar_Click` lee `codEstado` de la fila de la grilla (`FrmTPenPedido.cs:233`), que es una foto vieja: un requerimiento visto como 7 puede ser 13 en la BD. El servicio recibe solo `codReq` y `codUser`, y lee todo lo demás de la BD dentro de la transacción con `SELECT ... FOR UPDATE`.

### T2a — Reglas puras `ReqVentaReglas` (opencode)
Carpeta nueva `SIGEFA.Administradores/ReqVenta/`, namespace `SIGEFA.Administradores.ReqVenta`. Sin BD, sin UI.
- [ ] Constantes de estado (`secciones_de_etiquetas` origen 2): Pendiente 7, Aprobado 8, Cerrado 9, AtendidaParcial 10, AtendidaTotal 11, Anulado 12, AprobadoTransferido 13, Facturado 17; `TipoReqVenta = 2`.
- [ ] `DecisionAnulacion Evaluar(int tipoReq, int estado)`: guard clauses + `switch`; devuelve `Accion` (`Ninguna`, `AnularPendiente`, `AnularConExtorno`), `Permitido` y `Motivo` legible.
- [ ] Estados anulables en UNA constante fácil de cambiar: `{7, 13}` (**confirmado por el usuario, 2026-10-06**; el 8 casi no existe: 0 filas en dev, la aprobación de venta pasa a 13).
- [ ] Reglas: `tipoReq != 2` -> rechazado ("solo requerimientos de venta"); estado 12 -> rechazado ("ya está anulado"); 7 -> `AnularPendiente`; 13 -> `AnularConExtorno`; cualquier otro (8, 9, 10, 11, 17, desconocido) -> rechazado con motivo que nombre el estado.
- [ ] Pruebas unitarias sin BD (primero, RED).
- [x] Escrito, no verificado en compilador (2026-10-06, opencode). Test RED `cca2e76`, feat `da1cf8b`. Archivos: `SIGEFA.Administradores/ReqVenta/ReqVentaReglas.cs`, `AccionAnulacion.cs`, `DecisionAnulacion.cs`; pruebas `SIGEFA.Tests/ReqVenta/ReqVentaReglasTests.cs`; enlace `Compile Include` en `SIGEFA.Tests.csproj`.

### T2b — Lecturas con nombre `ReqVentaConsultas` (opencode)
Misma carpeta. Todas reciben un `IConsultor` (de `SIGEFA.Conexion`, helper `Db`) para poder correr dentro de una transacción. SQL parametrizado; sin `MessageBox`.
- [ ] `Dictionary<string,object> ObtenerRequerimiento(IConsultor c, int codReq, bool bloquear)`: lee `req_almacen` por `id_req_almacen`; `FOR UPDATE` si `bloquear`. Devuelve `null` si no existe. Columnas mínimas: `id_req_almacen`, `estado`, `tipo_req`, `cod_almacen_solicitante`, `cod_almacen_despacho`, `codPedidoVenta`, `codFacturaVenta`.
- [ ] `List<Dictionary<string,object>> ObtenerTransferencias(IConsultor c, int codReq, bool bloquear)`: transferencias ORIGINALES del requerimiento (`id_req_almacen = @id AND codDocExtornacion IS NULL`) con columnas `codTransDir`, `codAlmacenOrigen`, `codAlmacenDestino`, `total`, `estado+0 AS estado`, `pendiente+0 AS pendiente` y `tiene_extorno` calculado con `LEFT JOIN transferencia e ON e.codDocExtornacion = o.codTransDir` (los extornos antiguos NO tienen `id_req_almacen`, por eso se busca por `codDocExtornacion`, no por requerimiento).
- [ ] `List<Dictionary<string,object>> ObtenerDetalle(IConsultor c, int codReq)`: líneas de `detalle_req_almacen` del requerimiento (descubrir columnas con `DESCRIBE`; incluir las de producto, unidad, cantidad pendiente aprobada y el id de detalle que usa `RetornandoStockAlAnularReqAlmacen`).
- [ ] Pruebas de integración de solo lectura contra la BD dev (`SIGEFA_TEST_CONN`; se omiten sin ella): id inexistente -> `null`; casos históricos estables (req 6318: anulado, original aprobada sin extorno; req 11713: dos originales rechazadas; un requerimiento con extorno: `tiene_extorno`). Sin escribir ni borrar nada.
- [x] Escrito, no verificado en compilador (2026-10-06, opencode). Test RED `6f7fa04`, feat `3f2f6cc`. Archivos: `SIGEFA.Administradores/ReqVenta/ReqVentaConsultas.cs`; pruebas `SIGEFA.Tests/ReqVenta/ReqVentaConsultasTests.cs` (solo lectura: req -1 null/vacío, 6318 estado 12 tipo 2 + original 13791 sin extorno, 11713 dos originales, req 1 con extorno). `SIGEFA.Tests.csproj` ya enlazaba la carpeta (sin cambios).

#### Revisión de claude sobre T2a/T2b (2026-10-06, sin compilador)
- Defecto 1 (T2b, nacido de la instrucción de claude): `tiene_extorno` con `LEFT JOIN` duplicaba la original con varios extornos (req 10785: 1 original y 3 extornos devolvía 3 filas; verificado en la BD dev). Corregido con `EXISTS` y prueba nueva (RED `bc231b6`, corrección en commit posterior).
- Defecto 2 (T2a): `EstadosAnulables` existía pero `Evaluar` no la usaba (`switch` con 7 y 13 fijos); cambiarla no cambiaba el comportamiento. Ahora es la única fuente de verdad.
- Verificado en la BD dev: columnas de `detalle_req_almacen` existen; el SQL corregido da el resultado esperado en los reqs 10785, 6318, 11713 y 1.
- **Verificado en la VM (2026-10-06, `sigefa_build`, `632336a`, autorización explícita del usuario):**
  - RED sobre `bc231b6`: de 24 pruebas de integración falla 1, `ObtenerTransferencias_10785_UnaOriginalAunqueTengaTresExtornos` (`Assert.Single`: la colección tenía 3 elementos).
  - Build principal `Debug|x86` (MSBuild, `/t:Rebuild`, sin `-m`): `exit=0`, 0 errores, 480 advertencias, 30 s. Los 4 archivos de `ReqVenta\` compilan dentro del ejecutable; `SIGEFA.Tests` queda fuera. No se confirmó el binario `SIGEFA.exe` en disco (ruta de salida no localizada).
  - Sin `SIGEFA_TEST_CONN`: 77 pruebas, 53 correctas, 24 omitidas, 0 falladas.
  - Con la BD dev del host (vía `192.168.122.1:3307`, nunca producción): **77 de 77 correctas**, incluida la del req 10785. Sin tablas `zz_test_db_*` residuales.
  - El `HintPath` corregido en `f18a8a3` funciona sin copiar la DLL.
  - Límite: no se probó el flujo de negocio ni la UI; los datos de las pruebas de integración son los de la BD dev y pueden cambiar.

### T2c — Anular pendiente (estado 7)
- [ ] Rechazar pendientes, devolver stock y marcar anulado en una transacción. Pendiente de T2a/T2b.

### T2d — Anular aprobado con extorno (estado 13)
- [ ] Antes de escribir: leer firmas y salidas de `GuardaTransferencia`, `GuardaDetalleTransferencia`, `GuardaNotaSalida`, `GuardaDetalleSalida`, `GuardaNotaIngreso`, `GuardaDetalleIngreso`, `AprobarTransferencia` y los triggers de stock; llamarlos desde la MISMA conexión. Rollback total ante cualquier fallo (decisión pendiente del usuario; recomendado).

### T2e — `ReqVentaFlujoService.Anular(codReq, codUser)` con guard clauses y `Resultado`.

### T2f — Conectar `FrmTPenPedido.btnEliminar_Click` detrás de `VentaCierreRuta=nueva`; luego migrar uno a uno los otros 6 puntos de entrada.

#### Reparto por agente
| Agente | Tareas | Rol |
|---|---|---|
| **opencode** | T2a, T2b | Escribir código y pruebas (no puede compilar: host sin dotnet) |
| **claude** | verificación en VM, T2c-T2f, orquestación | Build y pruebas en la VM (con autorización explícita del usuario) |

#### Protocolo de traspaso (obligatorio)
1. Antes de empezar: `mem_search "req-venta-servicio"`, leer este documento y `AGENTS.md`.
2. Trabajar SOLO en `SIGEFA.Administradores/ReqVenta/*.cs`, `SIGEFA.Tests/` (pruebas nuevas) y la línea `Compile Include` correspondiente en `SIGEFA.Tests/SIGEFA.Tests.csproj`. No tocar formularios, `SIGEFA.csproj` ni la ruta legacy.
3. No afirmar "compila" ni "las pruebas pasan": no hay `dotnet` en el host. Reportar "escrito, no verificado en compilador".
4. Un commit por unidad de trabajo (Conventional Commits en español, sin atribución de IA): tests RED de T2a, implementación T2a, tests de T2b, implementación T2b. Registrar hash en este documento y actualizar Engram `odd/req-venta-servicio/tasks`.
5. No subir a ningún remoto.

## Evidencia y entrega

### Cortes de entrega (stacked-to-main, decidido 2026-10-06)
Líneas = adiciones sin contar el documento ODD; estimadas por archivo. Cada PR lleva sus pruebas. Los PR 1-6 no cambian el comportamiento de nadie; solo el PR 7 toca el botón, detrás del flag `VentaCierreRuta=nueva` (por defecto, legacy).

| # | PR | Contenido | Líneas |
|---|---|---|---|
| 1 | Núcleo del helper | `IConsultor`, `Consulta`, `ResultadoEjecucion`, `ParametrosSql`, proyecto `SIGEFA.Tests`, exclusión en `SIGEFA.csproj` | ~366 |
| 2 | Lectura tipada | `FilaExtensiones` (`Valor<T>`) + pruebas | ~239 |
| 3 | Acceso a MySQL | `ConsultorMySql`, `Db`, pruebas de integración | ~496 (**recomendar `size:exception`**: separar las pruebas del código que verifican contradice la guía) |
| 4 | Reglas | `ReqVentaReglas`, `DecisionAnulacion`, `AccionAnulacion` + pruebas | ~230 |
| 5 | Lecturas | `ReqVentaConsultas` + pruebas de integración | ~240 |
| 6 | Servicio de anulación | `Anular`, transacción única, extorno (T2c-T2e) | ~400 (estimado) |
| 7 | Conectar el botón | `FrmTPenPedido` detrás del flag | ~100 (estimado) |

- Cada PR: sección Chain Context, diagrama con `📍`, base = `main` tras integrarse el anterior (retarget/rebase para que el diff muestre solo su unidad).
- Las ramas por PR se crean al momento de armar los PR (no antes) reordenando los commits ya agrupados por unidad de trabajo; no se abre ningún PR ni se hace push sin pedido explícito del usuario.
- Plan de verificación por PR: build `Debug|x86` en la VM y pruebas del PR (con autorización explícita del usuario cada vez).

- Pronóstico de líneas: T1 ~450-550 autoreadas (incluye pruebas), T2 ~400. Total sobre el umbral de ~400: aplicar la estrategia de entrega (`ask-on-risk`) antes del siguiente commit tras pactar la cadena con el usuario.
- Diseño de referencia: ver informe de investigación (sesión 2026-10-06; Engram `odd/anular-req-venta/investigacion`).

## Siguiente paso

opencode implementa T2a y T2b; claude las verifica en la VM (con autorización explícita) y reverifica el `HintPath`. Ids anulables 7 y 13 confirmados por el usuario. Estrategia de entrega confirmada: stacked-to-main.
