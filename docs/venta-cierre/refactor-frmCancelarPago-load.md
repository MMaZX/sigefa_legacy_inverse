# Plan de refactor de `frmCancelarPago_Load` (Etapa A de T14a)

Solo plan: no cambia ningún `.cs`. El código se toca recién en la Etapa B, con la aprobación del usuario.
Referencias: `SIGEFA.Formularios/frmCancelarPago.cs:376-496`, skills `refactor` y `csharp-async` (`.agents/skills/`), sección "Code conventions" de `AGENTS.md`.

## 1. Olores confirmados (con línea)

O1. **Método largo con varias responsabilidades** (`:376-496`, ~120 líneas). Mezcla: banderas y modo captura (`:378-391`), carga de listas (`:392-397`), documento según `tipo` (`:398-457`), métodos de pago y aplicación inicial (`:458-459`), moneda y tipo de cambio (`:460-494`), ajuste de rueda del mouse (`:495`).

O2. **Cadena `if / else if` sobre `tipo` con números mágicos** (`:398`, `:407`, `:420`, `:429`, `:433`, `:442`, `:449`, más `:461` y `:479` en moneda). `tipo` es campo público que fijan otros formularios: no se cambia su tipo ni su significado (solo se leen sus valores en un `switch`).

O3. **Bloque de tipo de cambio duplicado 4 veces**: `:407-419` (rama `tipo == 10`, usa `tc.Venta`), `:461-478` (tipos 1/2/5, usa `tc.Venta`) y `:479-494` (tipos 3/4, usa `tc.Compra` y fija `ReadOnly = true` cuando hay tipo de cambio). Las tres ramas comparten el caso sin tipo de cambio (texto vacío y `ReadOnly = false`).

O4. **Código muerto**: `if (letra == null) { }` vacío en `:445-447`. Fuera del alcance de `Load` pero en el mismo archivo hay otro `if` vacío (`if (tip == 3) { }` en `Pagar()`): se anota y no se toca en esta pasada.

O5. **Decisión de modo captura y aviso de letras mezclados con la carga de la interfaz** (`:378-391` y `:398-406`). El aviso de letras depende del flag `VentaCierreRuta` y se muestra antes de `CargaNotaCredito()`.

O6. **Todas las consultas a la BD corren en el hilo de interfaz** antes de mostrar el formulario: `cargaMoneda()`, `CargarBancos()`, `CargarTarjetas()`, `CargaMetodosPagos()`, `Carga*` según tipo, `CargaMoneda(mon)` y `CargaTipoCambio(...)` (dos veces). Ver decisión en la sección 4.

O7. **Orden con dependencias que no puede cambiar**: `:458 CargaMetodosPagos()` antes de `:459 cmbMetodoPago_SelectionChangeCommitted(...)` (el combo necesita su `DataSource`; tras T14, con el helper ya creado en el constructor); `:460 Mon = AdmMoned.CargaMoneda(mon)` antes del bloque de moneda (`:461-494`). Ningún llamador asigna `mon` (queda en 0), pero el orden se conserva igual.

## 2. Pasos ordenados (un método extraído por paso)

Orden de menor a mayor riesgo. Cada paso es un commit `refactor(venta-cierre): ...` y conserva el orden de inicialización de O7.

### Paso 1 — Quitar el `if` vacío de `letra`

Cuerpo actual (`:445-447`):

```csharp
CargaLetra();
if (letra == null)
{
}
```

Cuerpo propuesto:

```csharp
CargaLetra();
```

Riesgo: nulo (el bloque no hace nada). Skill `refactor` §9 (código muerto): si hace falta, el historial de git lo conserva.
Verificación: `git diff -w` muestra solo líneas eliminadas; humo con `tipo == 4`.

### Paso 2 — Estabilizar los flags en camelCase y extraer la decisión del modo captura

Los tres flags que `Load` inicializa mezclan estilos (`ventana_cobro`, `caja_aperturada` con snake_case; `ventaRecibida` en camelCase) y no dicen con su nombre que son estado booleano. Se renombran con prefijo `es` (los tres son `bool`):

| Antes (público) | Después (público) |
|---|---|
| `ventana_cobro` | `esVentanaCobro` |
| `caja_aperturada` | `esCajaAperturada` |
| `ventaRecibida` | `esVentaRecibida` |

Alcance medido del renombre (únicos lectores externos): `frmVenta2019.cs:3644, 3648, 3652` (ruta contado) y `:3838, 3840, 3844` (ruta crédito). Usos internos en `frmCancelarPago.cs:367, 378-380, 705, 741, 1399, 1494, 2126`. **No tocar** `caja_aperturada` de `frmCancelarPagoMultiple.cs:123` ni de `frmCancelarCobroMultiple.cs:268`: son miembros de otras clases, fuera de alcance.

El renombre va en el mismo commit que la extracción (si quedara a medias, no compila: el compilador hace de red de seguridad) y se verifica con el build en VM.

Cuerpo actual (`:378-391`): las tres banderas, la lectura del flag y el `if/else` que fija `modoCaptura` y limpia `borradoresPago`.

Cuerpo propuesto en `Load` (ya con los nombres nuevos):

```csharp
esVentanaCobro = true;
esVentaRecibida = false;
esCajaAperturada = true;
determinarModoCaptura();
```

con el `if/else` movido tal cual a `determinarModoCaptura()` (mismo orden, mismo comentario).

Riesgo: bajísimo (solo flags y una lista en memoria, sin BD).
Verificación: `tipo == 3` con flag `nueva` (captura, borradores vacíos) y con flag `legacy` (sin captura).

### Paso 3 — Extraer la carga de listas base

Cuerpo actual (`:392-397`): `cargaMoneda()`, `CargarBancos()`, `CargarTarjetas()` y los dos `SelectedIndex = -1` más `txtMora.Text = "0.00"`.

Cuerpo propuesto en `Load`:

```csharp
cargarListasBase();
```

con esas seis líneas movidas tal cual a `cargarListasBase()`, en el mismo orden.

Riesgo: bajo (el orden interno no cambia; los `-1` siguen después de cada carga).
Verificación: abrir con cualquier `tipo` y comprobar que moneda, banco y tarjeta traen datos y sin selección.

### Paso 4 — Extraer la rama de devolución por letras (`tipo == 100`)

Cuerpo actual (`:398-406`): el aviso condicionado al flag, `CargaNotaCredito()` y `Text = "DEVOLVER PAGO"`.

Cuerpo propuesto en `Load`:

```csharp
if (tipo == 100)
{
    cargarDevolucionPorLetras(flagRuta);
}
```

con el bloque movido tal cual (el aviso sigue mostrándose antes de `CargaNotaCredito()`).

Riesgo: bajo (el `MessageBox` depende del flag leído al inicio; se pasa como parámetro para no releer configuración).
Verificación: `tipo == 100` con flag `nueva` (aviso + título) y `legacy` (sin aviso). Llamadores: `frmNotasCredito.cs:350`, `frmNotasCreditoAplicadas.cs:345`.

### Paso 5 — Cambiar la cadena por `tipo` a `switch` en `cargarDocumentoPorTipo()`

Cuerpo actual (`:407-457`): ramas sueltas para 10 y 100, más `if/else if` para 1–5 (`CargaFactura`, `CargaLetra`, `CargaNotaSalida` + serie, `CargaCuota`, títulos, `muestra_botones`, `posiciona_textbox`). La rama 100 ya salió en el paso 4; la rama 10 (solo tipo de cambio) sale en el paso 6.

Cuerpo propuesto en `Load`:

```csharp
cargarDocumentoPorTipo();
```

con un `switch (tipo)` con `case 1, 2, 3, 4, 5` y `default` vacío (preserva que los demás valores no hacen nada). Cada `case` lleva su bloque actual verbatim, incluidos `Text`, `muestra_botones` y `posiciona_textbox`.

Riesgo: medio (es el paso con más líneas movidas; el peligro es perder un `Text` o un `muestra_botones`).
Verificación: humo por `tipo`: 1 (desde `frmNotaIngreso.cs:2282`), 2 y 4 (vía `itipo` de grilla en `frmCobros`/`frmPagos`), 3 (contado, título "COBRANZA VENTAS", serie RC visible), 5 (título "CANCELAR PAGO"). Sin este paso no se toca el paso 6.

### Paso 6 — Extraer `cargarTipoCambio(bool usaCompra)`

Cuerpo actual: `:407-419` (rama 10, `tc.Venta`), `:461-478` (tipos 1/2/5, `tc.Venta`, más `txtMoneda`/`cmbMoneda`) y `:479-494` (tipos 3/4, `tc.Compra` + `ReadOnly = true` si hay tipo de cambio).

Cuerpo propuesto: un método que fija `txtTipoCambio` desde `tc.Venta` o `tc.Compra` y, si no hay tipo de cambio, deja texto vacío con `ReadOnly = false`. El `ReadOnly = true` del caso compra encontrada se queda en el llamador (rama 3/4), porque solo existe ahí; la asignación de `txtMoneda`/`cmbMoneda`/`Mon` no se mueve (dependencia O7).

Riesgo: medio (las tres ramas difieren en `Venta` vs `Compra` y en el `ReadOnly`; hay que compararlas lado a lado al extraer).
Verificación por `tipo`: 10 (sin `Mon`, solo TC venta), 1/2/5 (TC venta + moneda), 3/4 (TC compra + `ReadOnly = true` cuando hay TC; editable cuando no hay).
Nota (2026-10-06, a pedido del usuario, posterior a la Etapa B): el despacho de moneda quedó en `switch` con `case` agrupados (1/2/5 y 3/4, `default` vacío, `Mon != null` por rama). Solo estética: la semántica es idéntica al `if/else if` anterior.

## 3. Regla de oro y proceso seguro (skill `refactor`)

Regla de oro: **el refactor no cambia lo que el código hace, solo cómo está escrito**. En esta pasada, además:

- No cambian firmas públicas ni campos públicos (`tipo`, `tip`, `VentComp`, `venta`, `mon`, `Mon`, `vieneDe`, `montoPag`, `borradoresPago`, etc.), **con una sola excepción**: el renombre de los tres flags del paso 2 (`ventana_cobro`, `caja_aperturada`, `ventaRecibida` → prefijo `es`), que actualiza todos sus lectores en el mismo commit y se verifica con el build en VM.
- Por qué esta restricción (no es desconocimiento): los miembros públicos del formulario son su contrato con ~10 formularios llamadores, que los escriben **antes** de `ShowDialog()` (`tipo`, `venta`, `Monto`...) y los leen **después** (`ventaRecibida`, `borradoresPago`...). **Renombrar** es seguro si se miden todos los lectores y viajan en el mismo commit: si falta uno, no compila (el compilador es la red). Lo que **no** se hace es cambiar tipos, significados u orden de inicialización: eso sí cambia el comportamiento en tiempo de ejecución y solo el humo manual lo detectaría, con cobertura fina (tipos 2/4/5/10 sin llamador fijo).
- No cambia el orden de inicialización (O7).
- No hay cambios de arquitectura (sin DI, sin clases nuevas, sin tocar DAL ni servicio).
- Proceso por paso (adaptado de la skill: no hay tests, el "test" es el humo): un paso → `git diff -w` → humo del `tipo` afectado → commit. Un commit por extracción. Si un humo falla, se revierte solo ese paso.

## 4. Decisión de `csharp-async`: NO volver `Load` asincrónico en esta pasada

Postura: se mantiene lo propuesto por claude. Fundamento:

1. Los llamadores abren con `ShowDialog()` y leen el resultado al volver (`frmVenta.cs:2562`, `frmNotaIngresoPorOrden.cs:1150`, `frmNotaIngreso.cs:2285`; mismo patrón en `frmVenta2019`, `frmCobros`, `frmPagos`): campos como `ventaRecibida`, `continua_pago` y `borradoresPago`. Con `Load` en `async void`, el formulario se mostraría antes de terminar de inicializarse y el cajero podría pulsar Aceptar con las listas vacías.
2. Los loaders actuales ya capturan sus errores con `MessageBox` (`CargarBancos`, `CargarTarjetas`). En `async void` las excepciones no observadas terminan el proceso; habría que agregar manejo que hoy no existe → eso sí cambia comportamiento.
3. Las consultas son puntuales y contra BD local/red local; el beneficio no compensa el riesgo en un formulario usado por ~10 llamadores.
4. La skill `csharp-async` desaconseja `async void` salvo manejadores de eventos (y `Load` lo es, pero el punto 1 lo veta igual) y mezclar bloqueo con async.

Si en el futuro se propone async, el plan debe indicar: orden exacto de inicialización (flags y listas base primero, documento después), controles bloqueados mientras carga (`btnAceptar.Enabled = false` hasta terminar, cursor de espera), y manejo de excepciones por carga (aviso + `Close()` si falta algo crítico como métodos de pago, reintento si es transitorio). Nada de eso se implementa ahora.

## 5. Llamadores de `frmCancelarPago` (para definir el humo)

`tipo` fijo:

| Llamador | Líneas | `tipo` | Notas |
|---|---|---|---|
| `frmVenta2019.cs` | 3633, 3830 | 3, `VentComp = 1` | Contado; captura si flag `nueva` |
| `frmGeneraVenta.cs` | 1373, 1448 | 3, `VentComp = 1` | Contado |
| `frmVenta.cs` | 1964, 2084, 2555 | 3, `VentComp = 1` (1964/2084; 2555 usa el valor por defecto 0) | 2555 en `ingresarpago()`, con `ShowDialog()` |
| `frmVentas.cs` | 1175 | 3, `VentComp = 1` | Contado |
| `frmNotaIngreso.cs` | 2282 | 1 | Con `ShowDialog()` en 2285 |
| `frmNotaIngresoPorOrden.cs` | 1146 | 1 | Con `ShowDialog()` en 1150 |
| `frmNotasCredito.cs` | 350 | 100 | Devolución por letras |
| `frmNotasCreditoAplicadas.cs` | 345 | 100 | Devolución por letras |

`tipo` variable según la fila de la grilla (`itipo`):

| Llamador | Líneas | Notas |
|---|---|---|
| `frmCobros.cs` | 1043-1057, 1438-1462, 1499-1542 | `itipo` de la fila; en 1528-1542 además `VentComp = 1`, `vieneDe = "frmCobros"` |
| `frmPagos.cs` | 431-465, 790-825 | `itipo` de la fila, `VentComp = 2` |
| `frmTesoreriaAnuPag.cs` | 303-326, 510-535 | `itipo` de la fila |
| `frmPagosPresBancarios.cs` | 193-212, 295-300 | `itipo` de la fila (con puerta `itipo == 5`) |

Sin llamador fijo hallado: tipos 2, 4, 5 y 10 (llegan vía `itipo` de grilla). El humo de esos tipos requiere una fila de ese tipo en cobros/pagos. Todos los sitios verificados usan `ShowDialog()`.
