# Anulación de requerimientos de almacén de venta: investigación y decisiones

**Conclusión:** anular un requerimiento de venta hoy no es una operación atómica. Marca el requerimiento como anulado **antes** de devolver la mercadería, usa varias conexiones sin una transacción común, y muestra "Se anuló correctamente" aunque la devolución falle. La solución elegida es un servicio único que lee el estado de la base de datos (nunca de la pantalla) y ejecuta todo en una sola conexión y una sola transacción, con rollback total si algo falla.

Fecha: 2026-10-06. Rama: `feat/req-venta-servicio`. Plan y avance: [`odd/tasks/req-venta-servicio.md`](../odd/tasks/req-venta-servicio.md). Estudio de procedures y triggers: [`anulacion-requerimiento-procedures.md`](anulacion-requerimiento-procedures.md).

## Cómo leer este documento

| Marca | Significa |
|---|---|
| **Hecho** | Verificado con SQL contra la BD dev o con el código |
| **Hipótesis** | Inferencia razonable que no se pudo probar |
| **Pendiente** | Falta verificar o decidir |

Límite general: las consultas se hicieron sobre la BD de desarrollo (MySQL 5.7.44, binlog desactivado). **Producción no se verificó** y puede diferir.

## Explicación simple

Imaginá dos tiendas: **A** (la que vende) y **B** (la bodega).

1. A le manda una nota a B: "mándame un juguete". Eso es el **requerimiento**.
2. Si B todavía no lo mandó, anular es fácil: se tacha la nota y se libera lo que B tenía apartado.
3. Si B ya lo mandó, el juguete está en A. Para anular hay que **devolverlo**: se crea un "envío de vuelta" (el **extorno**) que lo saca de A y lo regresa a B.

El problema: el sistema **tacha la nota primero** y después intenta devolver el juguete. Si la devolución falla, la nota ya está tachada, el juguete sigue en A y el mensaje dice que todo salió bien.

Además, el botón decide qué hacer mirando una **foto vieja** (el estado que mostraba la grilla). Si mientras tanto la bodega lo aprobó, el sistema cree que sigue pendiente y no devuelve nada.

## Dónde está el problema (hechos)

| # | Problema | Evidencia |
|---|---|---|
| 1 | Anula **antes** del extorno, en otra conexión | `FrmTPenPedido.cs:246`, `:274`; `AnularRequerimientoAlmacen` hace commit inmediato |
| 2 | El mensaje de éxito depende solo de `anular`, no del extorno | `FrmTPenPedido.cs:322-324` |
| 3 | El estado se lee de la fila de la grilla, que puede estar desactualizada | `FrmTPenPedido.cs:233` |
| 4 | Sin transacción común: dos `TransactionScope` separados y `Aprobar` fuera de ambos | `FrmTPenPedido.cs:382`, `:436`, `:470` |
| 5 | Estado de formulario que nunca se reinicia (`bandera`, `detalle`) | `FrmTPenPedido.cs:33`, `:53`; tres extornos (13223, 15204, 17014) arrastran líneas de otro requerimiento |
| 6 | Si falla la nota de salida, el flujo continúa y crea la de ingreso | `FrmTPenPedido.cs:407-412` |
| 7 | El procedure de anular es incondicional (`estado=12` sin validar el previo) | `AnularRequerimientoAlmacen` |
| 8 | La rama "aprobada" no devuelve el stock reservado ni rechaza transferencias pendientes | Req. 11702: 56 unidades separadas sin retorno |
| 9 | El extorno recalcula precios con el último precio de compra | 204 de 3.040 líneas de extorno difieren de la original |

## Siete puntos de entrada anulan requerimientos

Cada uno con reglas de estado distintas. Ese es el motivo de centralizar en un servicio.

| Archivo:línea | Qué estados considera | Extorno |
|---|---|---|
| `FrmTPenPedido.cs:246/274` | 12 bloquea; 7 pendiente; todo lo demás va a la rama aprobada | Si hay exactamente 1 aprobada |
| `frmPedidosPendientes.cs:213/250` | Igual que el anterior | Sí; es el único que registra `codReqAlm` en el extorno |
| `frmVenta2019.cs:3156/3332` | Solo el 13 es aprobado; el resto aborta | Sí, pero ignora el resultado de `anular` y reutiliza `bandera` de la pantalla |
| `frmVentas.cs:631` | No mira el estado | Sí |
| `frmNotadeCredito.cs:1334` | No mira el estado | Sí |
| `frmNotadeCredito.cs:1523` | Estado del despacho | **No**: solo anula; puede dejar un requerimiento anulado sin extorno |
| `frmReqAlmacen.cs:1104` | Solo `tipo_req=1` (estados 7 y 8) | No aplica a ventas |

## Auditoría de datos (BD dev, cifras corregidas)

Universo: requerimientos de venta anulados (`estado=12`, `tipo_req=2`): **1.200** de 8.652. Una versión anterior de esta auditoría contaba mal por un `JOIN` que multiplicaba filas; estas cifras son las verificadas.

| Caso | Cantidad |
|---|---|
| Transferencias originales (en 1.168 requerimientos) | 1.169 |
| Originales aprobadas | 1.158 |
| ...de ellas, con extorno aprobado | 1.149 |
| ...de ellas, solo con extorno **incompleto** (`pendiente=1`) | 7 (reqs 792, 939, 4862, 5254, 8329, 8649, 9236) |
| ...de ellas, **sin ningún extorno** | **2** (reqs 6318 y 10462; S/ 7,80) |
| Originales aún pendientes, sin extorno ni rechazo | 4 (reqs 6101, 6537, 9150, 10951) |
| Originales rechazadas (`estado=0`) | 7 |
| Requerimientos anulados sin ninguna transferencia | 32 (31 nunca aprobados; el 11702 sí lo estaba) |
| Extornos pendientes huérfanos, sin nota de salida ni de ingreso | **18** |
| Originales con 2 o 3 extornos | 7 (el req. 10785 tiene 3) |
| Requerimientos con `fecha_anulo` y estado distinto de 12 | 6 (958, 2883, 3321, 7709, 10202, 12010) |

**Hecho:** el mecanismo funciona en la gran mayoría de los casos; los fallos son pocos pero reales. Los 4 originales pendientes siguen vivos: si alguien los aprueba, el trigger los pasa a 10 u 11 y mueve stock.

**Hipótesis:** los motivos exactos de varios casos son inferencias, porque no hay registro de qué puesto hizo cada operación.

### Por qué hay extornos huérfanos

Hecho: casi todos se explican por tres causas juntas: el chequeo de stock de `GuardaDetalleSalida` compara sin el factor de unidad y devuelve `newid=0` sin error; `bandera` y `detalle` quedan sin reiniciar en `FrmTPenPedido`; y el listado de transferencias aprobadas no excluye las ya extornadas, lo que permite reintentos. El servicio nuevo elimina las dos últimas por construcción.

## Quién pisa el estado 12

Ningún procedure, trigger ni punto del C# respeta el estado anulado. Hecho:

- No hay eventos del programador de MySQL (`event_scheduler=OFF`).
- Un trigger (`ActualizaDisponibleAprobarTransferencia`) y cuatro procedures escriben `req_almacen.estado` sin la guarda `estado<>12`.
- Los casos reales vienen del C#: `frmDespacho:677/681` (reqs 2883 y 12010) y `frmEntrega:176` (reqs 958, 7709 y 10202, probable carrera con otro puesto).
- Req. 3321 (estado 18, un código de despacho): **hipótesis** de una versión anterior del binario.

El detalle completo está en el [estudio de procedures](anulacion-requerimiento-procedures.md).

## Evidencia web

Todo lo siguiente se verificó contra fuentes primarias, salvo los vacíos del final.

| Tema | Conclusión | Fuente |
|---|---|---|
| Una transacción por caso de uso | Transaction Script y Unit of Work: una frontera de transacción por operación | [Fowler: Transaction Script](https://martinfowler.com/eaaCatalog/transactionScript.html), [Unit of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html) |
| Si el segundo paso falla, el primero no debe quedar | Ejemplo de transferencia de dinero | [Microsoft: buenas prácticas con excepciones](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions) |
| Varias conexiones en un `TransactionScope` | Escala a transacción distribuida (MSDTC) | [Microsoft: escalado de transacciones](https://learn.microsoft.com/en-us/dotnet/framework/data/transactions/transaction-management-escalation) |
| MySqlConnector y XA | `UseXaTransactions` es `true` por defecto | [MySqlConnector: opciones](https://mysqlconnector.net/connection-options/) |
| `FOR UPDATE` | Solo bloquea con autocommit apagado | [MySQL: locking reads](https://dev.mysql.com/doc/refman/8.0/en/innodb-locking-reads.html) |
| Guard clauses y `switch` por estado | Aplanan los `if` anidados | [Refactoring.Guru](https://refactoring.guru/replace-nested-conditional-with-guard-clauses), [Microsoft: selection statements](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements) |
| Consultas con parámetros | Nunca concatenar valores | [OWASP](https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html) |
| `DBNull` y parámetros | Pasar `DBNull.Value`; leer con `IsDBNull` | [Microsoft: parámetros ADO.NET](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/configuring-parameters-and-parameter-data-types) |

Vacíos declarados: no existe fuente oficial que defina el "patrón Result" en C# (es una convención de la comunidad); faltan fuentes oficiales de Oracle sobre `MySql.Data` y `TransactionScope`; no se verificó el comportamiento en MariaDB ni en MySQL 5.7 de producción.

## Decisiones

| # | Decisión | Fecha |
|---|---|---|
| 1 | Opción A: servicio con una conexión y un `MySqlTransaction`; SQL inline parametrizado, sin procedures nuevos | 2026-10-06 |
| 2 | **La vista nunca aporta el estado.** El servicio recibe `codReq` y `codUser` y lee todo de la BD con `SELECT ... FOR UPDATE` | 2026-10-06 |
| 3 | Estados que permiten anular: **7** (pendiente) y **13** (aprobado/transferido), solo `tipo_req=2` | 2026-10-06 |
| 4 | Si un extorno falla, la anulación no se termina de hacer: rollback total y el usuario ve el error | 2026-10-06 |
| 5 | Helper de consultas estilo Query Builder: `Db.Consultar(...).Get()` / `.First()`, `Db.Ejecutar(...)`, `Db.Transaccion(...)`; `Get()` nunca devuelve `null`, `First()` devuelve `null` si no hay filas | 2026-10-06 |
| 6 | Entrega en PR encadenados, estrategia apilados a `main` (7 cortes) | 2026-10-06 |
| 7 | La ruta legacy queda intacta; el comportamiento nuevo va detrás del flag `VentaCierreRuta=nueva` | 2026-10-06 |

## Qué se construyó y cómo se verificó

| Pieza | Estado |
|---|---|
| Helper `Db` y sus pruebas (T1) | Verificado en la VM: RED observado, build `Debug|x86` sin errores, 47/47 pruebas contra la BD dev |
| Reglas de anulación y lecturas con nombre (T2a, T2b) | Verificado en la VM sobre `632336a`: 77/77 pruebas. Se corrigieron 2 defectos en revisión (duplicación por `LEFT JOIN` con varios extornos, y la constante de estados que no gobernaba nada) |
| Servicio de anulación y extorno (T2c a T2e) | **Pendiente** |
| Conexión del botón detrás del flag (T2f) | **Pendiente** |

Nada de esto se probó en la interfaz ni sobre el flujo de negocio completo. Nada se subió a un remoto.

## Pendientes

- [ ] **Decisión:** si blindar el estado 12 contra otros flujos (`frmDespacho`, `frmEntrega`, notas de crédito) entra en este alcance o va como tarea aparte. Recomendación: tarea aparte, porque toca procedures de la BD de producción.
- [ ] Escribir T2c a T2e siguiendo la secuencia del [estudio de procedures](anulacion-requerimiento-procedures.md): validar stock con el factor de unidad, tratar `newid=0` o NULL como fallo con rollback, copiar valores de la original al extorno.
- [ ] Verificar la versión y la configuración de MySQL de producción (aislamiento, binlog).
- [ ] Probar el flujo completo con datos reales en la VM antes de activar el flag.

## Siguiente paso

Escribir el servicio de anulación (T2c a T2e). Es el trabajo que toca stock y transacciones, y por eso va primero un diseño de la secuencia con el que revisar cada llamada.
