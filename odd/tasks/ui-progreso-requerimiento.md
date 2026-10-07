# ui-progreso-requerimiento

Estado: T1 escrita sin verificar en compilador (2026-10-07, opencode; test RED `408bf10`, feat `17471c2`). T3 (agy) en curso en paralelo. Pendiente de aprobación del usuario y de las decisiones abiertas.
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
- **T3:** modal, sin cancelar, no se cierra con X, Alt+F4 ni Escape mientras corre; ejecuta el trabajo con `Task.Run` y recibe el progreso con `Progress<T>`; expone `fueExitoso` y el mensaje de error; al fallar muestra el detalle y permite copiarlo; con un solo paso usa barra indeterminada. Sin lógica de negocio.
  - Escrito, no verificado en compilador (2026-10-07, agy). Commit base `4427e54`, corrección `2c8f71f` (`frmProgresoOperacion.cs` y `frmProgresoOperacion.Designer.cs`). Implementa contrato exacto: firma con `IList<PasoOperacion>` y `Func<IProgress<PasoOperacion>, ResultadoOperacion>`, propiedad `Resultado` más conveniencias `fueExitoso` y `mensajeError`. Sin asignaciones a `DialogResult` (el diálogo modal se cierra exclusivamente con `Close()` y el resultado se lee vía `Resultado`). Con pasos y éxito: la lista queda visible y se muestra `btnCerrar` para que el usuario lea los pasos; en modo sin pasos (guardar) se cierra solo al terminar bien. En error sin pasos cambia texto a 'No se pudo completar la operación.'; estado de cada paso rastreado en `ListViewItem.Tag` para marcar en Error el que estaba en curso. `FormClosing` bloqueado hasta terminar; colores por estado (Pendiente gris, EnCurso azul, Listo verde, Error rojo). `SIGEFA.csproj` intacto por globbing de SDK. Quedó sin verificar en compilador ni ejecución gráfica (sin dotnet ni entorno WinForms en host).
- **T4:** el handler `anularRequerimientoRutaNueva` conserva la confirmación existente y reemplaza la llamada directa por el diálogo; recarga la lista al terminar; `git diff -w` sobre el legacy sin borrados.
- **T5a:** informe con cada `MessageBox`/`MessageBoxEx` de la cadena (archivo y línea), cada lectura de control de la interfaz que ocurra dentro de la lógica, y el número real de procedimientos. Verificar si `apruebaTransferencia` de `frmReqAlmacen` también registra la nota de ingreso (el texto de la confirmación depende de eso).
- **T5b:** mensaje de confirmación que explique qué ocurre al aprobar; datos leídos de la interfaz en el hilo de la interfaz y el resto en segundo plano; ningún `MessageBox` desde el hilo de fondo; pasos en español claro; éxito y error igual de visibles que hoy. No se reescribe la lógica (D1 fase 1).
- **T6:** confirmación "¿está seguro de guardar?" conservando la pregunta previa de "sin comentario" sin duplicar diálogos; el cuadro "Guardando el requerimiento…"; el `insert` corre en segundo plano; mismo comportamiento posterior (recarga, `DialogResult.Yes`, cierre). Atender el `else` faltante cuando `insert` devuelve false: mostrar el error, no seguir como si hubiera guardado.

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

Plan aprobado y D1 resuelta (2026-10-07). Primera ola en paralelo: T1 (opencode) y T3 (agy), ambos contra el contrato de arriba. Después T2 (opencode); T4 y T5a (agy); T6 y T5b al final, en ese orden.
