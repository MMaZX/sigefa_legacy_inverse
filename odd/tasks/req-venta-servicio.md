# req-venta-servicio

Estado: T1 en curso (2026-10-06). T2 en adelante pendientes de decisión.
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
- Pendientes del usuario: estados que permiten anular (recomendado 7 y 13, solo `tipo_req=2`); si un extorno fallido revierte toda la anulación (recomendado: sí); estrategia de entrega en cadena cuando el total supere ~400 líneas.

## TDD

- Modo: estricto (configuración de la sesión). Fuente: configuración del orquestador + elección explícita del usuario ("arrancá con el helper y las pruebas").
- Runner: **xUnit sobre net48, ejecutado en la VM Windows** (no existe en el repo ni en el host). El proyecto de pruebas enlaza los fuentes del helper con `<Compile Include>` en vez de referenciar `SIGEFA.exe`, para no arrastrar Crystal ni WinForms.
- RED/GREEN solo observables en la VM; no se declara "pasa" ni "compila" sin esa evidencia.

## Tareas

Cada tarea cierra con un commit de unidad de trabajo y registra su hash y evidencia observada.

### T1 — Helper de consultas con pruebas
Archivos nuevos en `SIGEFA.Conexion/Db/`: `IConsultor.cs`, `Consulta.cs`, `ResultadoEjecucion.cs`, `ConsultorMySql.cs`, `Db.cs`, `FilaExtensiones.cs`. Proyecto `SIGEFA.Tests/` (net48, xUnit) fuera del glob de `SIGEFA.csproj`.
- [ ] T1a — Pruebas sin BD (RED): conversión de objeto anónimo a parámetros, `DBNull` <-> `null`, `Get()` vacío, `First()` nulo, `Valor<T>`, clave inexistente.
- [ ] T1b — Implementación del helper (GREEN) y exclusión del proyecto de pruebas en `SIGEFA.csproj`.
- [ ] T1c — Pruebas de integración contra la BD dev con tabla `TEMPORARY` y transacción con rollback: `Id` en INSERT, `FilasAfectadas` en UPDATE, commit y rollback de `Transaccion`, parámetros contra inyección (`' OR 1=1`), columnas `bit(1)`.
- [ ] T1d — Build y pruebas en la VM Windows (requiere autorización explícita).
- Ruta: delegated direct, un solo escritor (6+ archivos no triviales). Disparador: Writer trigger.

### T2 — Servicio de anulación `ReqVentaFlujoService` (pendiente de decisiones)
- [ ] Reglas puras `ReqVentaReglas` (estados 7 y 13, `tipo_req=2`) con pruebas.
- [ ] `Anular(codReq, codUser)` con guard clauses, `FOR UPDATE`, una transacción, `Resultado { Ok, Mensaje }`.
- [ ] Integración detrás de `VentaCierreRuta=nueva` en `FrmTPenPedido`, sin tocar la ruta legacy.

## Evidencia y entrega

- Pronóstico de líneas: T1 ~450-550 autoreadas (incluye pruebas), T2 ~400. Total sobre el umbral de ~400: aplicar la estrategia de entrega (`ask-on-risk`) antes del siguiente commit tras pactar la cadena con el usuario.
- Diseño de referencia: ver informe de investigación (sesión 2026-10-06; Engram `odd/anular-req-venta/investigacion`).

## Siguiente paso

Delegar T1a-T1c a un escritor; pedir autorización explícita para T1d (VM por SSH).
