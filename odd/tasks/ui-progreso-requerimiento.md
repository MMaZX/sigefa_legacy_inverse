# ui-progreso-requerimiento

Estado (2026-10-07): T1, T2, T3, T4 y T6 entregadas, auditadas por claude y **verificadas en la VM** (`d17bed9`). T1: `408bf10`/`17471c2`; T2: `9e1d3e9`/`a6d3574`; T3: `4427e54`/`2c8f71f`; T4: `5e0e805`; T6: `84b4984`/`715712b` y corrección `ead69ed`/`72cfc63`. Pendientes: prueba manual visual del usuario, T5a/T5b (Aprobar, fase 1) y T1b (revisión de textos con la skill `humanizer`).

### Verificación y auditoría (claude, 2026-10-07)

- **Auditoría:** T1 aprobada. T3 tuvo una ronda de corrección (asignaba `DialogResult`, que cierra un diálogo modal al instante; se cerraba a los 0,5 s en éxito con pasos). T2 aprobada. T4 aprobada. **T6 rechazada en la primera entrega:** `ReqVentaGuardado` llamaba a `GuardaDetalleRequerimientoAlmacen` con los argumentos en otro orden que su firma real (el legacy pasa por nombre y no le afecta); demostrado en la BD dev con transacción y rollback: el detalle se guardaba con `id_req_almacen` NULL y los campos corridos, y el servicio habría dicho Ok. Las 10 pruebas de esa entrega usaban simuladores y no podían verlo. Regla nueva: toda CALL posicional nueva se verifica contra `mysql.proc` y lleva una prueba de integración que lea lo guardado.
- **VM (`sigefa_build`, autorización explícita del usuario para esta ronda, `SIGEFA.exe` cerrado):** build `Debug|x86` de `SIGEFA.csproj` `exit=0` (compila los formularios nuevos y modificados); con `SIGEFA_TEST_CONN` sin `AllowUserVariables` y la BD dev del host: **171 de 171 pruebas en 2 corridas**. BD dev intacta: 5273 en 7, 8416 en 13, stock 1015/1015, sin extornos, sin requerimientos `ZZT%`, sin detalles huérfanos, sin tablas `zz_test_*`.
- **No cubierto:** nadie ha visto los diálogos en pantalla; el aspecto, el texto y el comportamiento al fallar solo se confirman con la prueba manual. En un fallo de anulación `MarcarAnulado` también se marca en rojo y los pasos posteriores quedan en "Pendiente" (decisión de diseño de T2 a confirmar con la prueba manual). El guardado nuevo ya no muestra "Requerimiento de Almacen Guardado Con Exito": el formulario se cierra y la lista se actualiza.
Rama de partida: `feat/req-venta-servicio` (depende de `ReqVentaFlujoService`). Mirror Engram: `odd/ui-progreso-requerimiento/tasks`.

## Objetivo

Que las acciones largas de requerimientos de venta no congelen la ventana: cada una muestra un cuadro de diálogo modal, sin cancelar, con texto en español claro. El usuario sabe qué se está haciendo y, si falla, qué pasó.

## Pedido del usuario (2026-10-07)

1. **Anular** (botón de `FrmTPenPedido`): ventana de progreso como la del cierre de venta, con los pasos reales del extorno, para ver si se hace bien.
2. **Guardar requerimiento**: preguntar "¿está seguro de guardar?" y mostrar un cuadro "Guardando el requerimiento…" (una sola acción, estado pendiente).
3. **Aprobar requerimiento**: preguntar si está seguro y explicar que aprueba el requerimiento; luego un cuadro con barra de progreso. Si ejecuta varios procedimientos, mostrar los pasos en lenguaje humano.
4. Las acciones **no se pueden cancelar**: los cuadros existen para no romper el funcionamiento del programa.
5. Plan repartido entre agy y opencode, con auditoría de claude.

## Evidencia (mapa de solo lectura, 2026-10-07)

- `FrmTPenPedido.btnnuevo_Click` (`FrmTPenPedido.cs:127-168`) abre `frmReqAlmacen` con `TipoReq=2` y `ShowDialog()`.
- `frmReqAlmacen.btnGuarda_Click` (`:830-927`): valida, pregunta solo "¿continuar sin comentario?", llama `admreqalm.insert` (`clsAdmRequerimientoAlmacen.cs:16-58`, `TransactionScope`, 1+N llamadas), muestra éxito, recarga la lista y cierra con `DialogResult.Yes`. Si `insert` devuelve false el flujo sigue sin else (posible defecto previo).
- `frmReqAlmacen.btnAprobar_Click` (`:1557-1672`): sin confirmación previa. Rama `TipoReq==2` (`:1591-1639`): ~10 llamadas fijas + ~3N (`separarStock`, `insertdetalle`, notas) en el hilo de la interfaz. **No hay transacción global**: `aprobar`, `asignarAutorizador` y `separarStock` no tienen scope; solo `update`, `insert` y `apruebaTransferencia` tienen el suyo. Un fallo a mitad deja el estado parcial (defecto previo, no lo introduce este plan).
- Los handlers sirven también a `TipoReq==1`, a la edición (`Proceso==1`) y se usan desde ~14 formularios. `admreqalm.insert` también lo llama `frmPropuestaDeReqAlmacen.cs:1046`.
- `insert`, `update` y `apruebaTransferencia` muestran `MessageBoxEx` dentro de la capa de administración: no es seguro mostrarlos desde un hilo de fondo con un diálogo modal abierto.
- `frmVentaCierreProgreso` está acoplado a `VentaCierreService` y a pasos de venta; es reutilizable el patrón (`Progress<T>` + `Task.Run`, bloqueo de cierre con bandera `terminado`, `ListView` con colores, copiar detalle), no la clase.
- No hay librería Humanizer en el proyecto (`rg` en `*.cs` y `*.csproj`). La skill `humanizer` 3.1.0 está instalada en `~/.claude/skills/humanizer` y sirve para revisar prosa; **no** es una librería de C#.

## Decisiones tomadas

- Alcance: solo requerimientos de venta (`TipoReq == 2`), detrás de `VentaCierreRuta=nueva`. La ruta legacy queda intacta y se verifica con `git diff -w` (consistente con el alcance de `req-venta-servicio`).
- Un único diálogo reutilizable nuevo (`frmProgresoOperacion`), no una copia de `frmVentaCierreProgreso`: ventana modal, `ControlBox=false`, `FixedDialog`, `FormClosing` bloqueado mientras no termine, sin botón Cancelar. Con pasos (anular, aprobar) o sin pasos (guardar: barra indeterminada y una línea).
- La lógica de pasos y los textos viven fuera del formulario, en `SIGEFA.Administradores/ReqVenta/` (esa carpeta ya está enlazada al proyecto de pruebas), para probarlos sin interfaz.
- Textos en español claro, sin nombres de procedimientos ni de tablas. La skill `humanizer` se puede usar para revisar la redacción final.

## Decisiones abiertas

- ~~D1~~ **Cerrada (usuario, 2026-10-07): fase 1 ahora, fase 2 aparte.** Aprobar solo se envuelve con progreso: la lógica legacy corre en segundo plano tal cual. Hacerla atómica con un servicio nuevo en una sola transacción (separar stock, transferencia, notas) es un task aparte, del tamaño de `req-venta-servicio` y con riesgo sobre stock. Mientras tanto Aprobar sigue sin transacción global (R3).
- **D1 revisada (usuario, 2026-10-10): opción 1.** T5a demostró que la lógica legacy de Aprobar no puede correr tal cual en segundo plano (escribe una celda de la grilla Telerik, lee controles y usa campos de instancia compartidos del formulario, y muestra `MessageBox`). Se extrae a un servicio sin controles (patrón `ReqVentaGuardado`: DTOs puros + `IConsultor`), con los mismos procedimientos y el mismo orden que el legacy, detrás de `VentaCierreRuta=nueva` y solo `TipoReq==2`. Sigue **sin** transacción global (R3): atomicidad de Aprobar sigue siendo un task aparte. Los dos `TransactionScope` existentes (nota de salida, nota de ingreso) y el de `update` se conservan donde estaban.
- ~~D2~~ **Cerrada (usuario, 2026-10-07):** "humanizer" es la **skill** (`~/.claude/skills/humanizer`, 3.1.0) para pulir las palabras de los textos del contexto; no la librería de .NET. No se agrega ninguna dependencia al proyecto. La skill está orientada a inglés: en español se aplica como guía de revisión (relleno, frases infladas, tono de folleto, cierres vacíos), sin reescribir el sentido.
- D3: ¿Los 4 colores de estado de paso (pendiente, en curso, listo, error) son suficientes o se quiere "advertencia" como en el cierre? Se asume los 4.

## Contrato entre piezas (fijo; T1 lo implementa tal cual y T3/T2/T4 lo usan sin cambiarlo)

Namespace `SIGEFA.Administradores.ReqVenta`, carpeta `SIGEFA.Administradores/ReqVenta/`:

```csharp
public enum EstadoPaso { Pendiente, EnCurso, Listo, Error }

public sealed class PasoOperacion            // inmutable
{
    public PasoOperacion(string clave, string texto, EstadoPaso estado, string detalle = null);
    public string Clave { get; }             // identifica el paso; las actualizaciones repiten la clave
    public string Texto { get; }             // español claro, sin jerga
    public EstadoPaso Estado { get; }
    public string Detalle { get; }           // nunca null (cadena vacía)
}

public sealed class ResultadoOperacion       // resultado genérico del diálogo
{
    public ResultadoOperacion(bool ok, string mensaje);   // mensaje nunca null
    public bool Ok { get; }
    public string Mensaje { get; }
    public static ResultadoOperacion De(ResultadoAnulacion resultado);   // null -> fallo
}

public static class ReqVentaTextos           // catálogo; cada método devuelve la lista ordenada, todos en Pendiente
{
    public static IReadOnlyList<PasoOperacion> PasosAnulacionPendiente();
    public static IReadOnlyList<PasoOperacion> PasosAnulacionConExtorno();
    // más adelante: pasos de aprobar (T5b) y texto de guardar (T6)
}
```

Diálogo (T3), carpeta `SIGEFA.Formularios/`:

```csharp
public partial class frmProgresoOperacion : Form
{
    // pasosIniciales null o vacío = modo sin pasos (barra indeterminada y una línea de texto)
    public frmProgresoOperacion(string titulo, string textoSinPasos,
        IList<PasoOperacion> pasosIniciales,
        Func<IProgress<PasoOperacion>, ResultadoOperacion> trabajo);
    public ResultadoOperacion Resultado { get; }   // disponible al cerrar
}
```
El diálogo ejecuta `trabajo` con `Task.Run` desde el evento `Shown`, recibe el progreso con `Progress<PasoOperacion>` y busca el paso por `Clave`. Si `trabajo` lanza, `Resultado` es fallo con el mensaje de la excepción y su cadena de causas.

Servicio (T2): `ReqVentaFlujoService.Anular(int codReq, int codUser, IProgress<PasoOperacion> progreso)` (el progreso puede ser null); la sobrecarga de dos argumentos sigue existiendo y equivale a `progreso == null`.

## Tareas

Cada tarea cierra con un commit de unidad de trabajo (Conventional Commits en español, sin atribución de IA). Un solo escritor por archivo; cada agente hace `git add` solo de sus archivos. Push, PR y merge los decide el usuario.

| ID | Tarea | Agente | Archivos permitidos | Depende de |
|---|---|---|---|---|
| T0 | Aprobar el plan y resolver D1 | usuario | este documento | hecho (2026-10-07) |
| T1 | Modelo de pasos y catálogo de textos (sin interfaz) | opencode | `SIGEFA.Administradores/ReqVenta/PasoOperacion.cs`, `EstadoPaso.cs`, `ReqVentaTextos.cs`; pruebas en `SIGEFA.Tests/ReqVenta/` | T0 |
| T2 | Servicio de anulación emite progreso (`IProgress<PasoOperacion>` opcional) | opencode | `ReqVentaFlujoService.cs`, `ReqVentaAnulacionPendiente.cs`, `ReqVentaAnulacionConExtorno.cs`; sus pruebas | T1 |
| T3 | Diálogo `frmProgresoOperacion` (con y sin pasos) | agy | `SIGEFA.Formularios/frmProgresoOperacion.cs` y `.Designer.cs` (nuevos) | T1 |
| T4 | Botón Anular de `FrmTPenPedido` usa el diálogo | agy | `SIGEFA.Formularios/FrmTPenPedido.cs` | T2, T3 |
| T5a | Inventario de solo lectura de Aprobar: MessageBox, lecturas de controles y llamadas en la cadena | agy | ninguno (informe en este documento) | T0 |
| T5b | Aprobar: confirmación explicativa + diálogo con pasos humanos | agy | `SIGEFA.Formularios/frmReqAlmacen.cs` (solo `TipoReq==2` y flag) | T3, T5a, D1 |
| T6 | Guardar: confirmación + diálogo "Guardando el requerimiento…" | opencode | `SIGEFA.Formularios/frmReqAlmacen.cs` (solo `TipoReq==2` y flag) | T3 |
| T1b | Revisión de las palabras de todos los textos (pasos, confirmaciones, avisos de error) con la skill `humanizer` | claude | solo `ReqVentaTextos.cs` y los textos de T4, T5b y T6, tras entregarse | T1, T4, T5b, T6 |
| T7 | Auditoría, build en la VM y guion de prueba manual | claude | ninguno | cada tarea |

Orden: T0, luego T1; después T2 y T3 en paralelo (archivos distintos); T4; T5a puede ir en paralelo con T1-T3. T5b y T6 tocan el mismo archivo: **no en paralelo**; primero T6 (más simple), luego T5b.

### Criterios de aceptación por tarea

- **T1:** `PasoOperacion` (texto, estado, detalle opcional); catálogo con los textos de anular pendiente (rechazar transferencias, devolver reservas, marcar anulado) y anular con extorno (comprobar requerimiento, revisar stock, crear la transferencia que revierte, registrar salida, registrar ingreso, aprobar el extorno, marcar anulado). Pruebas: ningún texto vacío ni con nombres de procedimientos; orden estable.
  - Escrito, no verificado en compilador (2026-10-07, opencode). Test RED `408bf10` (solo `SIGEFA.Tests/ReqVenta/ReqVentaTextosTests.cs`, 14 pruebas), feat `17471c2` (`EstadoPaso.cs`, `PasoOperacion.cs`, `ResultadoOperacion.cs` con `De(ResultadoAnulacion)`, `ReqVentaTextos.cs`; claves públicas compartidas donde corresponde; textos con "revertir/reversión" y "envíos/movimiento" para evitar jerga). `SIGEFA.Tests.csproj` sin cambios (glob ya enlaza la carpeta). `Formularios` intacto (agy en paralelo).
- **T2:** `Anular(codReq, codUser, IProgress<PasoOperacion> progreso)` con sobrecarga de dos argumentos que sigue funcionando; `progreso` nulo no rompe nada; los pasos se emiten en orden y el fallido se marca con error; el rollback y la bitácora no cambian. Pruebas unitarias con un `IProgress` falso y la integración existente sigue en verde.
  - Escrito, no verificado en compilador (2026-10-07, opencode). Test RED `9e1d3e9` (solo `SIGEFA.Tests/ReqVenta/ReqVentaProgresoTests.cs`, 8 pruebas con `ProgresoFalso` síncrono), feat `a6d3574` (sobrecarga con progreso + parámetro opcional en `AnularPendiente`/`AnularConExtorno`, helper `ReqVentaTextos.Paso(clave, estado, detalle)`; `MarcarAnulado` en `Listo` lo emite el servicio al volver de la transacción con Ok mediante `SeguimientoProgreso`, en `Error` si el commit falla o el resultado es fallo). Rollback, bitácora y mensajes idénticos; `Formularios` intacto.
- **T3:** modal, sin cancelar, no se cierra con X, Alt+F4 ni Escape mientras corre; ejecuta el trabajo con `Task.Run` y recibe el progreso con `Progress<T>`; expone `fueExitoso` y el mensaje de error; al fallar muestra el detalle y permite copiarlo; con un solo paso usa barra indeterminada. Sin lógica de negocio.
  - Escrito, no verificado en compilador (2026-10-07, agy). Commit base `4427e54`, corrección `2c8f71f` (`frmProgresoOperacion.cs` y `frmProgresoOperacion.Designer.cs`). Implementa contrato exacto: firma con `IList<PasoOperacion>` y `Func<IProgress<PasoOperacion>, ResultadoOperacion>`, propiedad `Resultado` más conveniencias `fueExitoso` y `mensajeError`. Sin asignaciones a `DialogResult` (el diálogo modal se cierra exclusivamente con `Close()` y el resultado se lee vía `Resultado`). Con pasos y éxito: la lista queda visible y se muestra `btnCerrar` para que el usuario lea los pasos; en modo sin pasos (guardar) se cierra solo al terminar bien. En error sin pasos cambia texto a 'No se pudo completar la operación.'; estado de cada paso rastreado en `ListViewItem.Tag` para marcar en Error el que estaba en curso. `FormClosing` bloqueado hasta terminar; colores por estado (Pendiente gris, EnCurso azul, Listo verde, Error rojo). `SIGEFA.csproj` intacto por globbing de SDK. Quedó sin verificar en compilador ni ejecución gráfica (sin dotnet ni entorno WinForms en host).
- **T4:** el handler `anularRequerimientoRutaNueva` conserva la confirmación existente y reemplaza la llamada directa por el diálogo; recarga la lista al terminar; `git diff -w` sobre el legacy sin borrados.
  - Escrito, no verificado en compilador (2026-10-07, agy). Commit `5e0e805` (`SIGEFA.Formularios/FrmTPenPedido.cs`). Guarda y confirmación intactas; lista inicial según `colCodEstado` (7 -> `PasosAnulacionPendiente()`, 13 -> `PasosAnulacionConExtorno()`, otro -> sin pasos); `frmLogin.iCodUser` leído antes de lanzar; diálogo modal `frmProgresoOperacion` con título "Anulando el requerimiento" y texto "Anulando el requerimiento..."; al volver siempre `cargarlista()` y `btnnuevo.Visible = true`; quitados los `MessageBox` de éxito/error de la ruta nueva ya que los gestiona el diálogo; línea en blanco del usuario preservada en `btnEliminar_Click`. Legacy intacto verificado con `git diff -w`.
- **T5a:** informe con cada `MessageBox`/`MessageBoxEx` de la cadena (archivo y línea), cada lectura de control de la interfaz que ocurra dentro de la lógica, y el número real de procedimientos. Verificar si `apruebaTransferencia` de `frmReqAlmacen` también registra la nota de ingreso (el texto de la confirmación depende de eso).
  - PASO 0 (informe de solo lectura, 2026-10-07, agy):
    1. **Orden exacto de llamadas y transacciones en `TipoReq == 2`**:
       - `admSerie.BuscaSeriexDocumento(14, req_alm.CodAlmacenDespacho)` (`frmReqAlmacen.cs:1660`) -> DAL MySQL, sin transacción.
       - `obtenerDetalleParaTransferencia()` (`:1699`):
         - Itera `rgvDetalleRequerimiento.Rows` (líneas con `colCtdadRequerimiento > 0`).
         - Por cada línea: `AdmPro.UltimoPrecioCompraProducto(prod, und, 0)` (`:1566`) -> DAL MySQL, sin transacción.
       - Guarda previa: `if (detalle.Count > 0)` (`:1700`).
       - `admreqalm.aprobar(req_alm.Codigo, frmLogin.iCodUser)` (`:1702`) -> ejecuta SP `AprobarRequerimientoAlmacen` (`MysqlRequerimientoAlmacen.cs:316`). **Sin transacción**.
       - `admreqalm.asignarAutorizador(req_alm.Codigo, Convert.ToInt32(cmbusuariodesp.SelectedValue))` (`:1703`) -> SP `SetAutorizadorEnRequerimientoAlmacen` (`MysqlRequerimientoAlmacen.cs:655`). **Sin transacción**.
       - `req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen)` (`:1704`) -> SP `CargaRequerimientoAlmacen` (`MysqlRequerimientoAlmacen.cs:467`). **Sin transacción**.
       - `convertirRGVaListado()` (`:1707`) -> lee `rgvDetalleRequerimiento.Rows`.
       - `admreqalm.update(req_alm, req_alm.ListadoDetalle, detalleOld)` (`:1708`):
         - Envuelto en `using TransactionScope Scope` (`clsAdmRequerimientoAlmacen.cs:90-119`).
         - Llama SP `ActualizaRequerimientoAlmacen` (`MysqlRequerimientoAlmacen.cs:174`).
         - Llama SP `EliminaDetalleRequerimientoAlmacen` (`MysqlRequerimientoAlmacen.cs:123`).
         - Por cada item (N_total): SP `GuardaDetalleRequerimientoAlmacen` (`MysqlRequerimientoAlmacen.cs:881`).
       - Por cada item en `req_alm.ListadoDetalle` (N_total):
         - `admreqalm.separarStock(req_alm.CodAlmacenDespacho, item2.CodProducto, item2.CodUnidad, item2.CantidadConfirmada, item2.Codigo)` (`:1711`) -> SP `SeparandoStockAlAprobarReqAlmacen` (`MysqlRequerimientoAlmacen.cs:397`). **Sin transacción**.
       - `admTransferencia.insert(transfer)` (`:1713`) -> SP `GuardaTransferencia` (`MysqlTransferencia.cs:27`). **Sin transacción**.
       - Si inserta transfer (`if (admTransferencia.insert(transfer))` en `:1713`):
         - `admreqalm.registrarTransferencia(req_alm.Codigo, Convert.ToInt32(transfer.CodTransDir), frmLogin.iCodUser)` (`:1715`) -> SP `RegistrarTransferenciaRequerimientoAlmacen` (`MysqlRequerimientoAlmacen.cs:342`). **Sin transacción**.
         - Por cada fila en `detalle` (N_transfer):
           - `admTransferencia.insertdetalle(det)` (`:1719`) -> SP `GuardaDetalleTransferencia` (`MysqlTransferencia.cs:639`). **Sin transacción**.
         - `apruebaTransferencia(transfer)` (`:1721` -> `:1300`):
           - `AdmTran.MuestraTransaccion(15)` (`:1306`) y `admtd.BuscaTipoDocumento("TD")` (`:1307`). Sin tx.
           - `using (TransactionScope Scope = new TransactionScope())` (`:1333-1364`):
             - `admNS.insert(NS)` (`:1335`) -> SP `GuardaNotaSalida` (`MysqlNotaSalida.cs:29`).
             - `RecorreDetalleNS()` (`:1337` -> `:1480`) y por cada detalleNS (N_transfer): `admNS.insertdetalle(det)` (`:1342`) -> SP `GuardaDetalleSalida` (`MysqlNotaSalida.cs:393`). Si falla alguno: `Transaction.Current.Rollback()`, `bandera = false`, `codproducto_error = det.CodProducto`. Si todos ok: `Scope.Complete()`.
           - `if (bandera)` (`:1365`):
             - `using (TransactionScope Scope2 = new TransactionScope())` (`:1387-1418`):
               - `admNI.insert(NI)` (`:1389`) -> SP `GuardaNotaIngreso` (`MysqlNotaIngreso.cs:33`).
               - `RecorreDetalleNI()` (`:1391` -> `:1440`) y por cada detalleNI (N_transfer): `admNI.insertdetalle(det2)` (`:1396`) -> SP `GuardaDetalleIngreso` (`MysqlNotaIngreso.cs:850`). Si falla alguno: `Transaction.Current.Rollback()`, `bandera = false`, `codproducto_error = det2.CodProducto`. Si todos ok: `Scope2.Complete()`.
           - `if (bandera)` (`:1419`):
             - `admTransferencia.Aprobar(Convert.ToInt32(transfer.CodTransDir))` (`:1421`) -> SP `AprobarTransferencia` (`MysqlTransferencia.cs:1063`). **Sin transacción**.
         - `admreqalm.actualizaCantidadPendienteReqAlmacen(req_alm.Codigo)` (`:1722`) -> SP `ActualizaCantidadPendienteReqAlmacen` (`MysqlRequerimientoAlmacen.cs:330`). **Sin transacción**.
         - `admreqalm.actualizaEstadoReqAlmacen(req_alm.Codigo, 13)` (`:1723`) -> SP `ActualizaEstadoReqAlmacen` (`MysqlRequerimientoAlmacen.cs:370`). **Sin transacción**.
       - **Conclusión de transacciones**: Confirmado R3. NO hay transacción global. Existen 3 scopes aislados (`admreqalm.update`, nota de salida en `apruebaTransferencia`, y nota de ingreso en `apruebaTransferencia`). El resto de pasos (~8 SPs fijos + separación de stock + detalle de transferencias + aprobación de transfer) corren sueltos en autocommit de MySQL.
    2. **Inventario exhaustivo de MessageBox/MessageBoxEx**:
       - En `frmReqAlmacen.cs` (hilo UI / llamadas directas):
         - `:1651`: `MessageBox.Show("Debe definir un usuario autorizador o despachador", "Advertencia", ...)` [Error/validación previa si `cmbusuariodesp.SelectedValue == null`].
         - `:1657`: `MessageBox.Show(rpta, "Aviso", ...)` [Error/validación de `verificarCtdadRequerimiento()`].
         - `:1663`: `MessageBox.Show("No existe serie creada para transferencia en el almacen despachador", "Error", ...)` [Error si serie doc 14 es null].
         - `:1362`: `MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", ...)` [Error en `apruebaTransferencia` si `admNS.insert(NS)` devuelve false].
         - `:1416`: `MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", ...)` [Error en `apruebaTransferencia` si `admNI.insert(NI)` devuelve false].
         - `:1425`: `MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", ...)` [Error en `apruebaTransferencia` si `bandera == false` tras el bloque NI].
         - `:1430`: `MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error, "Transferencia Directa", ...)` [Error en `apruebaTransferencia` si `bandera == false` al salir del bloque NS].
         - `:1727`: `MessageBox.Show("Requerimiento de Almacen Aprobado Con Exito", "Informacion", ...)` [Éxito final legacy].
         - `:1757`: `MessageBox.Show(ex.Message, "", ...)` [Catch general de `btnAprobar_Click`].
       - En `clsAdmRequerimientoAlmacen.cs` (catch bloques de DAL):
         - `:128`/`:132`: `MessageBoxEx.Show(...)` en catch de `update`.
         - `:172`: `MessageBoxEx.Show(...)` en catch de `aprobar`.
         - `:198`: `MessageBoxEx.Show(...)` en catch de `CargaRequerimiento`.
         - `:263`: `MessageBoxEx.Show(...)` en catch de `registrarTransferencia`.
         - `:302`: `MessageBoxEx.Show(...)` en catch de `actualizaCantidadPendienteReqAlmacen`.
         - `:315`: `MessageBoxEx.Show(...)` en catch de `actualizaEstadoReqAlmacen`.
         - `:341`: `MessageBoxEx.Show(...)` en catch de `separarStock`.
         - `:406`: `MessageBoxEx.Show(...)` en catch de `asignarAutorizador`.
         - `:160`: `MessageBoxEx.Show(...)` en catch de `listadoTransferenciasGeneradas`.
         - `:250`: `MessageBoxEx.Show(...)` en catch de `ListaDetalleRequerimiento`.
       - En `clsAdmTransferencia.cs` (catch bloques de DAL):
         - `:23`: `MessageBoxEx.Show(...)` en catch de `insert`.
         - `:244`: `MessageBoxEx.Show(...)` en catch de `insertdetalle`.
         - `:387`: `MessageBoxEx.Show(...)` en catch de `Aprobar`.
       - En `clsAdmNotaSalida.cs`:
         - `:25`/`:29`: `MessageBoxEx.Show(...)` en catch de `insert`.
         - `:43`: `MessageBoxEx.Show(...)` en catch de `insertdetalle`.
       - En `clsAdmNotaIngreso.cs`:
         - `:25`/`:29`: `MessageBoxEx.Show(...)` en catch de `insert`.
         - `:129`: `MessageBoxEx.Show(...)` en catch de `insertdetalle`.
       - En `MysqlNotaIngreso.cs`:
         - `:889`: `MessageBox.Show(ex.Message ?? "", "")` [Catch en DAL de `insertdetalle`].
    3. **Lecturas y escrituras de controles UI en la cadena**:
       - Lecturas previas/en cadena:
         - `TipoReq` (campo int del form).
         - `cmbusuariodesp.SelectedValue` (`:1648`, `:1703`) y `cmbusuariodesp.Text` (si se invocara getDatos).
         - `rgvDetalleRequerimiento.Rows` (`:1874` en `verificarCtdadRequerimiento`, `:1551` en `obtenerDetalleParaTransferencia`, `:1120` en `convertirRGVaListado`): lee `colCtdadRequerimiento`, `colStockAlmacenDespacho`, `colCodDetalle`, `colCodProducto`, `colCodUnidad`, `colCantidad`, `colCtdadPendiente`.
         - `txtComentarioDespacho.Text` (`:1705`).
         - `CodPedido` (string del form, `:1690`).
         - `dtpFecha.Value` (`:1476` en `añadedetalleNI`).
         - `frmLogin.iCodUser`, `frmLogin.Configuracion.IGV`, `frmLogin.iCodAlmacen`.
       - Escrituras a UI durante la ejecución de la lógica:
         - `:1557`: `filaRGV.Cells["colCtdadPendiente"].Value = ...` (modifica celdas de la grilla en el hilo que corre `obtenerDetalleParaTransferencia`).
         - `:1706`: `vieneDeAprobar = true;` (afecta `convertirRGVaListado`).
       - Escrituras posteriores de recarga:
         - `setDatosRequerimientoAlmacen()` (`:1729`): escribe `dtpFecha`, combos, `txtComentario`, `txtComentarioDespacho`, `txtEstado`, `txtNumero`, `txtSerie`, `txtNombreContacto`, `txtTelefonoContacto`, `txtdireccion`, `cmbusuariodesp`, `txtusuariosolic`, `txtusuarioaprob`, `txtFacturaVenta`.
         - `rgvDetalleRequerimiento.DataSource = ...` (`:1730`).
         - `recargaStockRGV()` (`:1731`).
         - Visibilidad de botones: `btnAprobar.Visible = false`, `BtnGenerarTD.Visible = true`, `lblusuarioaprob.Visible = true`, `txtusuarioaprob.Visible = true`, `btnGuarda.Visible = false`, `btnanular.Visible = false`, `btndetalle.Visible = false`.
         - `dgvTransGeneradas.DataSource = ...` (`:1748`).
         - `ventanaListaReqVentas.cargarlista()` (`:1740`).
    4. **Número real de llamadas a BD en función de N**:
       - Sean:
         - $N_{req}$ = total de filas del requerimiento en `rgvDetalleRequerimiento`.
         - $N_{trans}$ = filas con `colCtdadRequerimiento > 0` (las que van a la transferencia directa).
       - Consultas de preparación:
         1. `AdmPro.CargaProductoDetalle` x 2 x $N_{req}$ (en `recargaStockRGV` invocada por `verificarCtdadRequerimiento`) + posibles `CargaUnidadEquivalente`.
         2. `admSerie.BuscaSeriexDocumento` (1 llamada).
         3. `AdmPro.UltimoPrecioCompraProducto` x $N_{trans}$ (en `obtenerDetalleParaTransferencia`).
       - Mutaciones de Aprobación:
         4. `admreqalm.aprobar` (1 llamada: SP `AprobarRequerimientoAlmacen`).
         5. `admreqalm.asignarAutorizador` (1 llamada: SP `SetAutorizadorEnRequerimientoAlmacen`).
         6. `admreqalm.CargaRequerimiento` (1 llamada: SP `CargaRequerimientoAlmacen`).
         7. `admreqalm.update`: 1 SP `ActualizaRequerimientoAlmacen` + 1 SP `EliminaDetalleRequerimientoAlmacen` + $N_{req}$ SP `GuardaDetalleRequerimientoAlmacen`.
         8. `admreqalm.separarStock` x $N_{req}$ (SP `SeparandoStockAlAprobarReqAlmacen`).
         9. `admTransferencia.insert` (1 llamada: SP `GuardaTransferencia`).
         10. `admreqalm.registrarTransferencia` (1 llamada: SP `RegistrarTransferenciaRequerimientoAlmacen`).
         11. `admTransferencia.insertdetalle` x $N_{trans}$ (SP `GuardaDetalleTransferencia`).
         12. `apruebaTransferencia`:
             - 1 SP `MuestraTransaccion` + 1 SP `BuscaTipoDocumento`.
             - 1 SP `GuardaNotaSalida`.
             - $N_{trans}$ SP `GuardaDetalleSalida`.
             - 1 SP `GuardaNotaIngreso`.
             - $N_{trans}$ SP `GuardaDetalleIngreso`.
             - 1 SP `AprobarTransferencia`.
         13. `admreqalm.actualizaCantidadPendienteReqAlmacen` (1 llamada: SP `ActualizaCantidadPendienteReqAlmacen`).
         14. `admreqalm.actualizaEstadoReqAlmacen` (1 llamada: SP `ActualizaEstadoReqAlmacen`).
       - Recarga post-aprobación:
         15. `admreqalm.CargaRequerimiento` (1 llamada).
         16. `AdmAlm.ListaAlmacen2` (1 llamada).
         17. `admreqalm.ListaDetalleRequerimiento` (1 llamada).
         18. `recargaStockRGV`: 2 x $N_{req}$ llamadas.
         19. `admreqalm.listadoTransferenciasGeneradas` (1 llamada).
       - **Fórmula total de llamadas en la mutación**: $12 + 2 N_{req} + 3 N_{trans}$. Para un requerimiento típico de 5 líneas donde se transfieren las 5: $12 + 10 + 15 = 37$ llamadas a base de datos.
    5. **Verificación sobre `apruebaTransferencia`, `bandera` y `codproducto_error`**:
       - **VERIFICADO**: `apruebaTransferencia` registra **AMBAS** notas: primero la Nota de Salida (origen despacho) con su detalle (`Scope`), y luego (si `bandera == true`) la Nota de Ingreso (destino solicitante) con su detalle (`Scope2`). Finalmente, si `bandera` sigue true, ejecuta `admTransferencia.Aprobar(transfer)`. Por tanto, la confirmación al usuario debe explicar con precisión que se aprueba el requerimiento, se genera la transferencia entre almacenes y se registran tanto la salida como el ingreso respectivo.
       - **VERIFICADO**: `bandera` inicializa en `true`. Si cualquier `insertdetalle` de NS o NI falla, se pone en `false`, guarda el código del producto en `codproducto_error = det.CodProducto`, hace `Rollback()` del scope respectivo y sale del bucle con `break`.
       - Si falló en el detalle de NS, no entra al bloque de NI y cae en el `else` (`:1430`): muestra `MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error)`. Si falló en NI o en `admNI.insert`, muestra "Hubo un error al guardar la transferencia ".
    6. **Lectura y mutación de estado compartido peligroso en fondo**:
       - Campos de instancia del formulario:
         - `detalle` (`List<clsDetalleTransferencia>`): limpiada y poblada en `obtenerDetalleParaTransferencia()`, leída en `btnAprobar_Click`, `RecorreDetalleNS()` y `RecorreDetalleNI()`.
         - `transfer` (`clsTransferencia`): instanciada y modificada acumulando totales y `CodTransDir`.
         - `NS` (`clsNotaSalida`) y `NI` (`clsNotaIngreso`): campos del formulario mutados en `apruebaTransferencia`.
         - `detalleNS` y `detalleNI` (`List<...>`): campos públicos del formulario limpiados y llenados en `RecorreDetalleNS` y `RecorreDetalleNI`.
         - `bandera` (bool) y `codproducto_error` (int): campos del formulario modificados como banderas de control de flujo.
         - `req_alm` (`clsRequerimientoAlmacen`): reasignado múltiples veces.
         - `vieneDeAprobar` (bool): modificado a `true` antes de llamar `convertirRGVaListado()`.
       - Mutación de controles de UI dentro del worker:
         - `:1557` en `obtenerDetalleParaTransferencia`: `filaRGV.Cells["colCtdadPendiente"].Value = ...` escribe directamente sobre una celda del control Telerik WinForms `rgvDetalleRequerimiento`! Esto lanzaría `InvalidOperationException` o corrompería la UI si se corre en un hilo de `Task.Run`.
         - `:1476` en `añadedetalleNI`: lee `dtpFecha.Value` (control WinForms `DateTimePicker`).
         - `:1705`: lee `txtComentarioDespacho.Text`.
         - `:1703`: lee `cmbusuariodesp.SelectedValue`.
    7. **Recomendaciones para T5b**:
       - **Fase de UI (hilo principal antes de `Task.Run` / diálogo)**:
         - Validaciones previas (`cmbusuariodesp`, `verificarCtdadRequerimiento`, serie 14).
         - Diálogo explicativo de confirmación: debe informar claramente que aprueba el requerimiento, reserva/separa stock y genera la transferencia directa con sus notas de salida e ingreso correspondientes.
         - Extracción limpia de DTOs en memoria sin tocar controles dentro del hilo de fondo:
           - Leer `cmbusuariodesp.SelectedValue`, `txtComentarioDespacho.Text`, `dtpFecha.Value`.
           - Extraer las filas del detalle necesarias sin mutar `colCtdadPendiente` de `rgvDetalleRequerimiento`.
           - NO usar los campos de instancia compartidos (`detalle`, `detalleNS`, `detalleNI`, `NS`, `NI`, `bandera`, `codproducto_error`, `transfer`); deben ser variables locales al trabajo o un servicio/objeto de contexto autocontenido.
       - **Fase de Fondo (`Task.Run` / progreso)**:
         - Ejecutar toda la secuencia de mutación en un solo bloque continuo (R2).
         - Erradicar `MessageBox` / `MessageBoxEx`: cualquier excepción o `false` debe abortar y reportar el mensaje como fallo en `ResultadoOperacion`, para que `frmProgresoOperacion` pinte el paso en Rojo y permita ver/copiar el detalle sin congelar la app.
       - **Fase posterior (al volver el diálogo en hilo UI)**:
         - Si `Resultado.Ok`: ejecutar el bloque de recarga (`req_alm = CargaRequerimiento`, `setDatosRequerimientoAlmacen`, recargar grillas, ocultar/mostrar botones, `ventanaListaReqVentas.cargarlista()`).
         - Si falló: la UI no se recarga como aprobada y el usuario ve exactamente qué falló en el diálogo.
- **T5b:** mensaje de confirmación que explique qué ocurre al aprobar; datos leídos de la interfaz en el hilo de la interfaz y el resto en segundo plano; ningún `MessageBox` desde el hilo de fondo; pasos en español claro; éxito y error igual de visibles que hoy. No se reescribe la lógica (D1 fase 1).
- **T6:** confirmación "¿está seguro de guardar?" conservando la pregunta previa de "sin comentario" sin duplicar diálogos; el cuadro "Guardando el requerimiento…"; el `insert` corre en segundo plano; mismo comportamiento posterior (recarga, `DialogResult.Yes`, cierre). Atender el `else` faltante cuando `insert` devuelve false: mostrar el error, no seguir como si hubiera guardado.
  - PASO 0 (solo lectura, 2026-10-07, opencode): `clsAdmRequerimientoAlmacen.insert` (`clsAdmRequerimientoAlmacen.cs:16-60`) SÍ muestra `MessageBoxEx` en sus caminos de excepción (`Duplicate entry` → "N°- de Documento Repetido", resto → `ex.Message`) y devuelve `false` sin lanzar; en `false` sin excepción (DAL devolvió `false`) no muestra nada y devuelve `false`. La capa `MysqlRequerimientoAlmacen.insert/insertdetalle` (`:23-89`, `:876-923`) no muestra cuadros, devuelve `bool` y lanza `MySqlException`. `insert` no lee controles (solo DTOs). Quien lee controles es `btnGuarda_Click` (`frmReqAlmacen.cs:830-927`) vía `getDatosRequerimientoAlmacen` (`:980-1028`: `cmbAlmacenesSolicitantes/Despacho`, `dtpFecha`, `txtComentario/NombreContacto/TelefonoContacto/direccion`, `cmbusuariodesp`, `chkDelivery`, `rgvDetalleRequerimiento` por `convertirRGVaListado`, más `ser/doc/frmLogin/pedido`), `verificarCtdadRequerimiento` (`rgv` + `recargaStockRGV`), `validarContacto/Delivery` y `admSerie.CargaSerieEmpresa` con `SelectedValue`. R1 confirmado: no llamar a `admreqalm.insert` desde el fondo. R2: todo el guardado en un solo bloque.
  - Escrito, no verificado en compilador (2026-10-07, opencode). Test RED `84b4984` (solo `SIGEFA.Tests/ReqVenta/ReqVentaGuardadoTests.cs`, 10 pruebas con `ConsultorFalsoGuardado` + `TransaccionFalsa`, sin BD), feat `715712b` (`ReqVentaGuardado.cs` nuevo con `DatosGuardadoRequerimiento/Detalle` puros y `Guardar/GuardarEn` por `IConsultor`+`Db.Transaccion` con los mismos procedimientos y orden que el legacy, `Duplicate entry` con su aviso, `false`/newid 0 como fallo con rollback forzado; `frmReqAlmacen.cs` solo rama `TipoReq==2 && Proceso==0 && VentaCierreRuta==nueva`: mantiene validaciones y "sin comentario", luego confirma "¿Está seguro de guardar el requerimiento? Quedará en estado pendiente.", arma datos en UI, diálogo sin pasos (`null`) "Guardando el requerimiento", éxito sin `MessageBox` duplicado con recarga/`DialogResult.Yes`/cierre, error sin cerrar con el detalle del diálogo). `git diff -w` solo adiciones y la guarda; `FrmTPenPedido.cs` intacto (agy); sin push.
  - Corrección única (2026-10-07, opencode) tras rechazo de auditoría por defecto grave: `InsertarDetalle` pasaba `(@codDet, @codReq, ...)` pero la firma real (`SHOW CREATE PROCEDURE` en dev) es `(_codReqAlmacen, _codProducto, _codUnidad, _cantidad, _cantidadPedida, _cantidadPendiente, _cantidadConfirmada, _cantidadPendienteAprobada, _codDetalleReqAlmacen, OUT newid)`; la cabecera se verificó y coincide (26 args en el mismo orden que `MysqlRequerimientoAlmacen.insert`). Reproducido en dev con transacción+`ROLLBACK`: el orden viejo inserta el detalle con `id_req_almacen` NULL y campos corridos devolviendo `newid>0` (Ok con datos corruptos); el orden de la firma inserta todo bien; conteos intactos tras cada rollback. Test RED `ead69ed` (`ReqVentaGuardadoIntegracionTests` en `Collection("BdReqVentaFilasCompartidas")`, `[HechoConBd]` con rollback siempre copiando valores del req 5273 con `num_documento` único `ZZT`+10, verifica dentro `req_almacen` y cada detalle columna por columna y fuera conteos antes/después; falla con el orden viejo), fix `72cfc63` (orden corregido + indentación legacy de `btnGuarda_Click` restaurada: sin `-w` el bloque legacy muestra solo la guarda agregada). Regla de oro nueva: toda CALL posicional nueva se verifica contra la firma real en la BD dev y tiene prueba de integración con rollback que lea lo guardado. Escrito, no verificado en compilador; sin push. Pendiente: build `Debug|x86` y suite en VM + prueba manual.

  - **T5b-1 hecha (mapa de solo lectura, claude, 2026-10-10, firmas contra dev `127.0.0.1:3307`; producción sin verificar):** 14 SP de la cadena coinciden con el DAL; **discrepancias de orden/cantidad** (el DAL llama por nombre y no le afecta, un CALL posicional sí): `ActualizaRequerimientoAlmacen` (`_fechaAnulacion`/`_codUsuarioAnulacion` invertidos), `GuardaDetalleRequerimientoAlmacen` (`_codDetalleReqAlmacen` va noveno), `GuardaTransferencia` (`codlista` tras `fechapago`), `GuardaDetalleTransferencia` (`unidad` antes de `codalmadest`, `_coddetallereqalm` al final), `GuardaNotaSalida` (`documentorefe` y `_area`/`_responsable`), `GuardaNotaIngreso` (el DAL manda `fechacancelado`, que el SP no tiene; `codref` va tras `numdoc`) y `GuardaDetalleIngreso` (el DAL manda `_estado`, que el SP no tiene). Ningún SP abre transacción; el stock lo mueven triggers de `detallenotasalida`/`detallenotaingreso`. "No hay stock suficiente" = `GuardaDetalleSalida` deja `newid=0`. El servicio solo necesita `codRequerimiento`, `CodUser`, `CodAutorizador`, `ComentarioDespacho`, `CodPedido`, `IGV` y la fecha de ingreso (`dtpFecha`); el resto se recarga. Defectos legacy encontrados (a decidir): (a) si `admNS.insert` devuelve false solo avisa y la nota de ingreso y `AprobarTransferencia` siguen; (b) `bandera` es campo de instancia y nunca se reinicia; (c) `NS.MontoBruto` se asigna dentro del bloque de NI y `NI.MontoBruto` queda por defecto.

  - **Decisión del usuario (2026-10-10) sobre los defectos de T5b-1:** (a) el servicio corta y reporta si falla la nota de salida (no sigue con ingreso ni `AprobarTransferencia`); (b) `bandera` y `codproducto_error` son locales; (c) `NI.MontoBruto` se replica como hoy (queda por defecto) con un comentario, porque corregirlo cambia montos contables y se decide aparte. **Ruta de T5b-2:** delegada (writer, regla de 2+ archivos no triviales), un solo escritor, TDD estricto: runner `dotnet test` en la VM (el host no tiene dotnet; "escrito, no verificado en compilador").

  - **Auditoría independiente de T5b-2 (2026-10-10, solo lectura, firmas ejecutadas en dev con ROLLBACK): APROBADO CON CORRECCIONES.** Verificado: los 22 SP del servicio coinciden con `mysql.proc` y cada valor cae en su columna (7 SP ejecutados con valores distintos por columna); fidelidad a `frmReqAlmacen` en procedimientos, orden y cálculos; sin `MessageBox`, controles ni estáticos de UI; solo 4 archivos tocados. No verificado: compilador y xUnit (sin dotnet), producción. `CantidadesADespachar` no hace falta: `colCtdadRequerimiento` es de solo lectura en `TipoReq==2`. Las validaciones nuevas del paso 1 no rompen casos legítimos (Aprobar solo se ve con estado 7 y `Proceso==1`); rechazar "todas las cantidades en cero" cambia el legacy, que mostraba éxito sin hacer nada.
    - **Correcciones pendientes (una sola ronda):** (1) **grave, prueba:** `ReqVentaAprobacionIntegracionTests.cs:463` afirma `detallenotaingreso.fechaingreso == fechaIngreso`, pero `GuardaDetalleIngreso` guarda `NOW()` y no usa el parámetro; la prueba fallará en la VM. Quitar la aserción o compararla con una ventana de `NOW()`, y corregir el comentario de `FechaIngreso` (`ReqVentaAprobacion.cs:42`: no se persiste, como en el legacy). (2) falta el equivalente de `verificarCtdadRequerimiento`: sin stock suficiente el servicio lo descubre recién en `GuardaDetalleSalida`, con el requerimiento ya aprobado y el stock separado; requisito explícito para T5b-3 (verificar en el hilo de la interfaz antes del diálogo) o paso 1 del servicio. (3) `LeerSerie` (`ReqVentaAprobacion.cs:386-388`) toma la primera fila; el legacy se queda con la última (`MysqlSerie.cs:158-167`); usar la última y probar con dos filas. (4) si falla un paso posterior al 2, el mensaje debe decir hasta qué paso quedó confirmado. (5) si cabe: la integración no falla si faltan datos base (`return` silencioso), no cubre `Aprobar` ni el corte por stock, no lee `cantidadpendiente`, serie `"001"` fija. **Deuda anotada:** aserciones unitarias débiles (`ContieneValor`), RED solo por compilación, carrera entre dos aprobadores, `catch` vacío en `LeerNewId`.
  - **Aviso:** `main` ya contiene T5b-2 con la aserción del hallazgo 1; en la VM esa prueba de integración fallará hasta corregirla. No es un fallo del servicio.

## Protocolo común para los agentes (pegar al inicio de cada prompt)

1. `mem_search "ui-progreso-requerimiento"` y `mem_search "req-venta-servicio"`; leer este documento, `odd/tasks/req-venta-servicio.md` y `AGENTS.md`.
2. Trabajar SOLO en los archivos de la tarea. TDD estricto cuando haya lógica: prueba en rojo (commit), luego implementación (commit).
3. Reglas: `net461`, sin `ValueTuple`, SQL parametrizado, sin ifs anidados de tercer nivel (guard clauses o `switch`), sin `MessageBox` nuevo para errores de lógica (devolver el error y mostrarlo desde el diálogo), ruta legacy intacta y nuevo comportamiento detrás de `VentaCierreRuta=nueva`.
4. El host no tiene `dotnet`: no afirmar "compila" ni "pasan". Reportar "escrito, no verificado en compilador".
5. Textos de interfaz en español claro; identificadores y comentarios siguiendo el estilo del archivo vecino.
6. Conventional Commits en español, sin `Co-Authored-By` ni atribución de IA; `git add` solo de los archivos propios; sin push.
7. Al terminar: hashes y evidencia en la tarea de este documento, actualizar Engram `odd/ui-progreso-requerimiento/tasks`, listar qué quedó sin verificar.

## Auditoría de claude (T7), por cada tarea entregada

1. `git show --stat` y lectura completa del diff; comprobar que solo se tocaron los archivos permitidos.
2. `git diff -w` sobre los archivos legacy: solo adiciones y la guarda del flag.
3. Revisión de textos con la skill `humanizer` (T1b): cada mensaje debe decir qué pasa, en pocas palabras, sin relleno, sin jerga técnica y sin tono de anuncio. Los cambios de redacción los hace claude en una sola pasada y quedan en un commit aparte.
4. Revisión contra los criterios de aceptación y los hallazgos típicos: pruebas que pasan por un camino distinto al declarado, pruebas que ocultan el defecto (como el truco de `AllowUserVariables`), mensajes con jerga, `MessageBox` desde el hilo de fondo, deadlocks entre pruebas en paralelo.
5. Build `Debug|x86` y suite completa en la VM, con autorización explícita del usuario; la BD dev debe quedar intacta. No compilar si la app está abierta en la VM.
6. Guion de prueba manual para el usuario (casos que funcionan y que fallan a propósito) y la bitácora real en `%LOCALAPPDATA%\SIGEFA\req_venta_errores.log`.
7. Una sola ronda de corrección por tarea; lo que siga mal vuelve al usuario como decisión, no a un bucle.

## Riesgos conocidos

- R1: la capa de administración muestra `MessageBoxEx` en errores. Hasta que T5a lo mida, no se sabe cuántos hay en la cadena de aprobar.
- R2: `TransactionScope` es sensible al hilo; la lógica legacy debe correr entera dentro de un solo `Task.Run`, sin partirla en varios.
- R3: el defecto de aprobar sin transacción global sigue existiendo en la fase 1: el cuadro de progreso lo hace visible pero no lo corrige.
- R4: `frmReqAlmacen` es compartido con `TipoReq==1`; cualquier cambio fuera de la rama `TipoReq==2` y del flag es un defecto de alcance.

## Evidencia y entrega

Pendiente. Cuando empiece el trabajo: una rama nueva apilada sobre `feat/req-venta-servicio` y PR según `chained-pr`/`work-unit-commits`. Pronóstico de líneas: T1 ~150, T2 ~150, T3 ~300, T4 ~40, T5b ~200, T6 ~120.

## Siguiente paso

**Estado al 2026-10-10 (cierre de sesión):** T5b-1 hecha; T5b-2 entregada (`027065b` RED, `4c18f2c` feat), escrita y **no verificada en compilador**; auditoría independiente lanzada y sin veredicto registrado (si la sesión se cortó, repetirla leyendo esos dos commits). Siguiente: leer el veredicto, una sola ronda de corrección si hace falta, luego T5b-3 (formulario: confirmación explicativa, datos leídos en el hilo de la interfaz incluyendo `CantidadesADespachar` si la grilla se editó, diálogo con `PasosAprobacion()`, recarga al volver), T1b, build y suite en la VM y prueba manual. Decisión a confirmar en la auditoría: las validaciones nuevas del paso 1 (estado distinto de 7, `tipo_req != 2`, cantidades en cero).

(Texto anterior, histórico:)

T1 a T4, T5a y T6 hechas. D1 revisada el 2026-10-10 (opción 1: extraer Aprobar a un servicio sin controles). Siguiente: T5b dividida en T5b-1 (mapa de firmas reales de los SP de la cadena contra `mysql.proc` en dev, solo lectura), T5b-2 (servicio `ReqVentaAprobacion` con pruebas RED/GREEN y una de integración con rollback, regla de oro de CALL posicional), T5b-3 (formulario: confirmación explicativa, extracción de datos en el hilo de la interfaz, diálogo con pasos, recarga al volver). Después T1b, build en la VM y prueba manual.
