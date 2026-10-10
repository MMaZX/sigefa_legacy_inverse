# venta-cierre-bitacora

Estado (2026-10-10): diseño aprobado por el usuario; migración pedida a la sesión `gr-backend` (sin respuesta aún); implementación C# por empezar. Rama `feat/venta-cierre-bitacora` desde `main`. Mirror Engram: `odd/venta-cierre-bitacora/tasks`.

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

## Verificación

Host sin `dotnet`: lo escrito se reporta como "escrito, no verificado en compilador". Runner de pruebas: `dotnet test` en la VM (`sigefa_build`, con autorización explícita). TDD estricto (fuente: configuración del proyecto). Regla de oro heredada: toda sentencia SQL nueva se verifica contra la BD dev real antes de darla por buena.

## Siguiente paso

T1 (mapa del punto de conexión).
