# CRUD de Productos — Catálogo (Legacy SIGEFA → Réplica Laravel 8.x, SIN Eloquent)

> Fuente verificada: `SIGEFA.Formularios/frmCatalogo.cs`, `SIGEFA.Formularios/frmRegistroProducto.cs`,
> `SIGEFA.Administradores/clsAdmProducto.cs`, `SIGEFA.InterMySql/MysqlProducto.cs`,
> y procedures reales en `manager_mysql:3307 / database_multi_final`.
> Regla del proyecto: **nada de Eloquent**. Laravel 8.x debe llamar a los **stored procedures
> existentes** con `DB::select / DB::statement` dentro de `DB::transaction`, igual que hace C# con
> `MySqlCommand(CommandType.StoredProcedure)`.

---

## 1. Cómo debe trabajar Laravel (sin Eloquent)

```php
// Patrón obligatorio para todo el CRUD de productos:
use Illuminate\Support\Facades\DB;

DB::transaction(function () {
    DB::select('CALL GuardaProducto(?, ?, ?, ..., @newid)', [$p1, $p2, /* ... */]);
    $newId = DB::select('SELECT @newid AS id')[0]->id;
});
```

* No crear migraciones que cambien columnas. Los nombres `codProducto`, `codGrupo`, etc. los usan
  reportes Crystal, SUNAT y el propio `CatalogoProductosII`. Respetarlos tal cual.
* Los `BIT(1)` llegan a PHP como `"\0" / "\1"` o `0/1` según driver. Castear a `(bool)`.
* Los procedures con parámetro `OUT newid` se invocan con `@newid` + `SELECT @newid`.
* C# bindea parámetros **por nombre** (`AddWithValue`), pero `CALL ... ?, ?` de Laravel es
  **posicional**: respetar el orden de la definición del SP (ver §3 y §4, hay un caso con orden distinto).

Flujo legacy a replicar:

```
frmCatalogo (Nuevo/Modificar/Eliminar)
  → frmRegistroProducto (Proceso=1 crea, 2 edita, 3 solo-lectura)
    → clsAdmProducto → MysqlProducto → CALL <procedure> → MySQL
```

---

## 2. Crear producto: secuencia completa (3 procedures encadenados)

Alta normal = `frmCatalogo.buttonItem16_Click` → `frmRegistroProducto{Proceso=1}` →
botón Guardar `btnAceptar_Click:679`. Si todo va bien ejecuta, **en este orden**:

| # | CALL (nombre exacto) | Cuántas veces | Qué crea |
|---|---|---|---|
| 1 | `GuardaProducto(..., OUT newid)` | 1 | 1 fila en `producto` (o `producto_cotizacion` si `_cotizacion=1`). Devuelve `newid`. Después hace `UPDATE producto SET referencia=LPAD(newid,9,'0')` |
| 2 | `GuardaUnidadEquivalente(..., OUT newid)` (o `GuardaUnidadEquivalente_cotizacion` si cotizador) | 3 | 3 filas en `unidadequivalente`: compra (`compra_venta=0`), venta (`=1`), equivalencia base (`=2`) |
| 3 | `creaciondeproductoalmacenmasivo(_codProd, _codUser)` | 1 | N filas en `productoalmacen`: una por cada fila de `almacen`, stock en 0 |

Todo dentro de **una sola `DB::transaction`**. El legacy no transacciona entre pasos (si falla el
paso 3 el producto queda huérfano) — en Laravel sí debes transaccionar los 3.

```php
DB::transaction(function () use ($input, $userId, $almacenSesion) {
    // PASO 1 — cabecera, devuelve $codProducto
    DB::select('CALL GuardaProducto(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,@newid)', [
        $userId, $codGrupo, $codLinea, $codFamilia, $codUnidadMedida, $codTipoArticulo,
        $codMarca, $codControlStock, $referenciaIgnorada, $descripcion, $tipoimpuesto, $codsunat,
        $detraccion, $estado, $comision, $preciocatalogo, $preciocompracatalago, $maxPorcDescto,
        $peso, $porcentajeretencion, $codigoUniversal, $ubicacion, $stockMinimo, $ventaTicket,
        $icbper, $flete, $desestiva, $estiva, $comision1, $comision2, $comision3,
        $gastosAdmin, $gastosAdic, $proveedor1, $proveedor2, $proveedor3,
        $stockMaximo, $descontinuado, 0, // _cotizacion = 0 siempre en alta normal
    ]);
    $codProducto = DB::select('SELECT @newid AS id')[0]->id;

    // PASO 2 — 3 unidades equivalentes (orden POSICIONAL del SP, ver §4)
    foreach ([
        ['precio' => $precioCompra, 'codTipo' => 9,  'cv' => 0],
        ['precio' => $precioVenta,  'codTipo' => 10, 'cv' => 1],
        ['precio' => 0,             'codTipo' => 9,  'cv' => 2],
    ] as $u) {
        DB::select('CALL GuardaUnidadEquivalente(?,?,?,?,?,?,?,?,?,?,@uid)', [
            $codProducto, $codUnidadMedida, 1, $userId, $u['precio'],
            $codUnidadMedida, $u['codTipo'], $almacenSesion, $u['cv'], 1,
        ]);
    }

    // PASO 3 — stock en todos los almacenes
    DB::select('CALL creaciondeproductoalmacenmasivo(?, ?)', [$codProducto, $userId]);
});
```

---

## 3. `GuardaProducto`: qué hace y qué significa cada parámetro

Definición real (resumen): inserta en `producto` con `fecharegistro=NOW(), ultimamodificacion=NOW()`,
luego `SET newid=LAST_INSERT_ID()` y `UPDATE producto SET referencia=LPAD(newid,9,'0')`.
Si `_cotizacion=1` inserta en `producto_cotizacion` en su lugar (solo módulo cotizador; el alta
normal manda `0`).

### 3.1 Identidad y auditoría

| Parámetro SP | Columna | Qué es / de dónde sale | Ejemplo real |
|---|---|---|---|
| `codUsuario_ex INT` | `producto.codUsuario` | Usuario logueado (`frmLogin.iCodUser`). Auditoría, no valida FK | `1` |
| `referencia_ex VARCHAR(13)` | `producto.referencia` | **Se ignora**: el SP la sobrescribe con `LPAD(newid,9,'0')` (ej. `000011473`). Es UNIQUE, formato de 9 dígitos con ceros. El `txtReferencia` ("Código:") del form es decorativo | entrada cualquiera → guardado `000011473` |
| `descripcion_ex VARCHAR(100)` | `producto.descripcion` | Nombre del producto (`txtNombre`, "Nombre *", obligatorio, se guarda en MAYÚSCULAS). Único campo que bloquea el guardado si está vacío | `WINCHA P/MEDIR 2MT 1/2" TRUPER CT-14602` |
| `estado_ex BIT` | `producto.estado` | Checkbox "Activo" (checked por defecto en alta). `1`=visible en catálogo, `0`=dado de baja | `1` |
| `OUT newid INT` | — | `LAST_INSERT_ID()` → `pro.CodProducto` en C#. Lo necesitas para los pasos 2 y 3 | `11473` |

### 3.2 Clasificación (los 6 combos del form, jerarquía `familia → línea → grupo`)

| Parámetro SP | Columna | Qué es / regla |
|---|---|---|
| `codFamilia_ex INT` | `codFamilia` | Combo "Familia *", obligatorio. Tabla `familia` (ej. `5 ABACABADOS Y PEGAMENTOS`) |
| `codLinea_ex INT` | `codLinea` | Combo "Categoria", depende de la familia (`linea.codFamilia`). Si vale `0` C# manda **NULL** |
| `codGrupo_ex INT` | `codGrupo` | Combo "Modelo", depende de la línea (`grupo.codLinea`). Si vale `0` C# manda **NULL** |
| `codTipoArticulo_ex INT` | `codTipoArticulo` | Combo "Tipo Artículo *", obligatorio. `tipoarticulo`: `1 MC MERCADERIA, 2 SV SERVICIO, 3 HM HERRAMIENTAS, 4 AF ACTIVO FIJO, 5 CC CAJA CHICA`. Ojo: `CC` se excluye del catálogo (`CatalogoProductosII`) |
| `codMarca_ex INT` | `codMarca` | Combo "Marca" (ej. `1 3M`). Opcional: si vale `0` C# manda **NULL** |
| `codUnidadMedida_ex INT` | `codUnidadMedida` | Combo "Unidad Base *", obligatorio. **Unidad en la que se lleva el stock** (casi siempre `55 NIU UNIDAD`). No se puede cambiar después si ya hay stock o UE (ver §6) |
| `codControlStock_ex INT` | `codControlStock` | Combo "Control Stock". `controlstock`: `1 LIBRE, 2 POR LOTE, 3 POR SERIE, 4 SERVICIO`. C# manda `SelectedIndex+1` |

### 3.3 Tributario / SUNAT (dos campos distintos, no confundir)

| Parámetro SP | Columna | Qué es |
|---|---|---|
| `tipoimpuesto_ex INT` + `codsunat_ex VARCHAR(10)` | `tipoimpuesto`, `codsunat` | Radio buttons del form, derivación **automática** (no pedir `codsunat` al usuario): Gravado `tipoimpuesto=1 → codsunat='10'`, Exonerado `2 → '20'`, Inafecto `3 → '30'`. Es la **afectación al IGV** de la factura electrónica |
| `codigo_universal VARCHAR(500)` | `codUniversal` | Textbox bajo el label confuso "Código Sunat:" (`txtCodigoUniversal`). Es el **código del catálogo SUNAT de productos (UNSPSC)**, texto libre, casi siempre vacío (`''`). No es la afectación al IGV. La tabla `codproductosunat(idclase, descripcionproducto, codsunat)` es el catálogo de referencia, pero el legacy lo deja como texto libre sin validar |
| `detraccion_ex BIT` | `detraccion` | Checkbox "Afecto a Detraccion" (retención SUNAT a transportes/materiales). `0/1` |
| `_porcentajeretencion DECIMAL(10,4)` | `procentajeretencion` | "Porcentaje de Detracción:", default `0.00` |
| `_icbper BIT` | `icbper` | Checkbox "ICBPER" (impuesto a la bolsa plástica). `0/1` |
| `venta_ticket_ex BIT` | `venta_ticket` | Checkbox "VENTA CON TICKET" (permite venderlo por ticket rápido). `0/1` |

### 3.4 Precios y comercial

| Parámetro SP | Columna | Qué es |
|---|---|---|
| `preciocatalogo_ex DECIMAL(10,4)` | `preciocatalogo` | "Precio Catálogo :" (`txtPrecioCata`). Precio de lista impreso. Ej. `12.0000`. `0` si vacío |
| `preciocompracatalago_ex DECIMAL(10,4)` | `preciocompracatalago` | Precio de compra de catálogo. El form de alta **no lo pide** (C# manda `pro.PrecioCompra` en `0`), por eso en BD casi siempre es `0` o `NULL`. El precio de compra real del alta va en `unidadequivalente` (paso 2), no aquí |
| `maxPorcDescto_ex DECIMAL(10,2)` | `maxPorcDescto` | "Máximo Porc. dscto:" tope de descuento % que el vendedor puede dar. `0.00` = sin tope configurado |
| `comision_ex DECIMAL(10,4)` | `comision` | "Comisión :" % base al vendedor. `0` si vacío |

### 3.5 Stock y logística

| Parámetro SP | Columna | Qué es |
|---|---|---|
| `ubicacion_almacen VARCHAR(20)` | `ubicacion` | Combo "Rzn. Social" (`cmbUbicacion`), **obligatorio** (tiene validator). No es una ubicación física: es la **empresa dueña**: `F. RICARDO` o `CORP. LM`. En BD se ven esos dos valores |
| `stock_minimo DECIMAL(10,2)` | `stockminimo` | "Stock Mínimo:", alerta de reposición. Default `0.00` |
| `_StockMaximo DECIMAL(10,2)` | `stockmaximo` | "Stock Maximo:". Default `0.00`. C# lo lee de `txtStockMaximo` |
| `propeso DECIMAL(10,2)` | `peso` | "Peso (Kg) :". Default `0.00` |

### 3.6 Costos logísticos y comisiones por tramo (pestaña "Admin. Precios" + "Proveedores")

Se usan para costeo interno. Todos opcionales, `0/NULL` si vacíos:

| Parámetro SP | Columna | Qué es |
|---|---|---|
| `_flete` | `flete` | "Flete Estimado:" costo de transporte imputado al producto |
| `_desestiva` | `desestiva` | "Desestiva:" costo de descarga |
| `_estiva` | `estiva` | "Estiva:" costo de carga/estiba |
| `_comision1/2/3` | `comision1/2/3` | "Comision 1/2/3:" comisiones por tramo o vendedor |
| `_gastosadmin` | `gastos_admin` | "Gastos Admin:" |
| `_gastosadic` | `gastos_adic` | "Gastos Adic:" (adicionales) |
| `_proveedor1/2/3 INT` | `proveedor1/2/3` | Combos "Proveedor 1/2/3" (`proveedor.codproveedor → razonsocial`, ej. `4 SIKA PERU S.A.C`). `-1`/vacío = `0` (ninguno) |
| `_descontinuado BIT` | `descontinuado` | Checkbox "Descontinuado" (en alta va unchecked=`0`; el grid muestra `ACTIVO/DESCONTINUADO`) |
| `_cotizacion BIT` | — (elige tabla) | `0` = inserta en `producto` (alta normal). `1` = inserta en `producto_cotizacion` (solo cotizador). Laravel alta normal: siempre `0` |

---

## 4. `GuardaUnidadEquivalente`: los 3 inserts de precio

Definición real: `GuardaUnidadEquivalente(codpro, coduni, factor, codusu, precio, codUndEqui,
codTipo, codAlmacen, compra_venta, codMoneda_ex, OUT newid)` →
`INSERT INTO unidadequivalente (...)`. Variante `_cotizacion` idéntica pero contra
`unidadequivalente_cotizacion`.

**Atención orden posicional**: el SP define `..., factor, codusu, precio, codUndEqui, ...`
pero el C# añade `..., factor, codUndEqui, codTipo, precio, codAlmacen, codusu, ...` (orden distinto,
funciona porque C# bindea por nombre). En Laravel `CALL ..., ?, ?` es posicional: usar el orden
del SP, como en el ejemplo del §2.

| Parámetro (orden SP) | Qué es / valor en el alta |
|---|---|
| `codpro` | `codProducto` recién creado (paso 1) |
| `coduni` | Unidad base (`producto.codUnidadMedida`, ej. `55`). En el alta las 3 filas usan la base |
| `factor` | Factor de conversión. En el alta siempre `1` (luego se configura en "Configurar Unidades Equivalentes") |
| `codusu` | Usuario logueado |
| `precio` | **El único dato distinto por fila**: compra = `txtPrecioCom` ("Precio Compra con IGV"), venta = `txtPrecioVen` ("Precio Venta con IGV"), equivalencia = `0`. Ej. real prod. 11472: compra `78.00`, venta `99.00`, equivalencia `0` |
| `codUndEqui` | Unidad equivalente. Compra/venta: `0`. Equivalencia base: la propia base (`55`) |
| `codTipo` | FK `tipo_precios.codT`. Compra: `9 COMPRA`, venta: `10 PUBLICO`, equivalencia: `9`. Otros valores (`7 MAYOR, 8 MENOR, 11 PRECIO, 12 CREDITO, 13 DELIVERY`) se crean después, no en el alta |
| `codAlmacen` | Almacén de la sesión (`frmLogin.iCodAlmacen`, ej. `1 ALMACEN D36-FR`) |
| `compra_venta` | Discriminador: `0`=compra, `1`=venta (**la que lista `CatalogoProductosII`**), `2`=equivalencia base |
| `codMoneda_ex` | Siempre `1` en el alta (soles) |
| `OUT newid` | `codUnidadEquivalente` generado (el legacy lo ignora) |

---

## 5. `creaciondeproductoalmacenmasivo`: el stock inicial

`creaciondeproductoalmacenmasivo(_codProd, _codUser)`: con dos cursores (`producto × almacen`)
inserta en `productoalmacen` una fila por cada `almacen` que aún no exista para ese producto, con
`stockactual=0, stockdisponible=0, ..., preciovariable=1, Unidad=(su codUnidadMedida)`, y devuelve
`SELECT contador` (filas creadas). Parámetros:

| Parámetro | Qué es |
|---|---|
| `_codProd` | `codProducto` del paso 1. (Si fuera `0` procesaría TODOS los productos — jamás mandar `0` desde Laravel) |
| `_codUser` | Usuario logueado, queda en `productoalmacen.codUser` |

Qué significa cada columna que deja en 0: `valorpromedio/valorpromediosoles/preciopromedio` (costo
promedio), `pcrecargo` (recargo), `valorventa/precioventa` (precio vigente por almacén),
`pcdescuento/montodescuento/preciooferta/oferta` (ofertas), `stockdisponible` (libre = actual −
reservado), `stockpendiente/stockporentregar` (tránsitos), `totalingresos/totalsalidas/...`
(acumulados kardex). Se calculan solos con las compras/ventas; el alta solo los inicializa.

---

## 6. Campos calculados del catálogo: `CUPC` y `CUPV` (no se guardan, se computan)

Vienen del `SELECT` de `CatalogoProductosII`, no existen como columnas:

```sql
CONCAT(SUBSTRING(p.referencia,4,6), LPAD(U.codUnidadMedida,3,'0'))             AS CUPC,
CONCAT(SUBSTRING(p.referencia,4,6), LPAD(U.codUnidadMedida,3,'0'),
       LPAD(t.codT,2,'0'))                                                     AS CUPV
```

* `SUBSTRING(referencia,4,6)`: la referencia es `LPAD(id,9,'0')` (ej. `000011473`); desde el
  carácter 4 toma los últimos 6 (`011473`). Es el "núcleo" del producto sin los ceros de relleno.
* `LPAD(codUnidadMedida,3,'0')`: unidad de la fila de venta con 3 dígitos (ej. unidad `55` → `055`).
* `LPAD(codT,2,'0')`: tipo de precio con 2 dígitos (ej. `10` → `10`).
* `CUPC` = "Código Único de Producto por Compra/unidad": `011473` + `055` = `011473055`.
  Identifica producto+unidad.
* `CUPV` = CUPC + tipo de precio: `011473055` + `10` = `01147305510`. Identifica
  producto+unidad+lista de precio (se usa como código de barras/venta).

Si replicas el listado sin el SP, computa estas dos columnas en SQL/PHP con la misma fórmula;
si llamas al SP, ya vienen hechas.

---

## 7. Editar y eliminar (mismos procedures, resumen)

* Editar: `CALL ActualizaProducto(codpro, codgru, codlin, codfam, codmar, coduni, codtip,
  control, referencia, descripcion, tipoimpuesto_ex, codsunat_ex, detraccion, estado, comision,
  precioca, maxPorcDesc, propeso, _procentajeretencion, codigo_universal, ubicacion_almacen,
  stock_minimo, _icbper, _flete, _desestiva, _estiva, _comision1/2/3, _gastosadmin, _gastosadic,
  _proveedor1/2/3, _StockMaximo, _descontinuado)` → `UPDATE producto ... WHERE codProducto=codpro`.
  No toca `referencia` (el parámetro se ignora, hace `referencia=referencia`), ni
  `preciocompracatalago`, ni stocks. **Regla**: si cambió `codUnidadMedida` y
  `VerificaProductoAlmacen(codProducto)>0` o ya tiene UE → rechazar (422), porque rompería el stock.
* Edición masiva del grid: `CALL actualizaProductoMasivo(codpro,codgru,codlin,codfam,codmar,...)` —
  solo grupo/línea/familia/marca + `ultimamodificacion=NOW()`.
* Eliminar: **baja lógica**. Pre-check `CALL ValidaStockProducto(codprod, OUT cont)` →
  `cont = COUNT(*) FROM productoalmacen WHERE codProducto AND stockactual>0`. Si `cont>0`,
  mostrar `CALL MuestraStockAlmacenes(codprod)` (`al.nombre, pa.stockactual, um.descripcion` por
  almacén) y no eliminar. Si `cont=0` → `CALL EliminarProducto(codprod)` →
  `UPDATE producto SET estado=0`. Si ya estaba en `0`, avisar "ya esta dado de baja".

---

## 8. Checklist de aceptación (contra `database_multi_final`)

1. Alta con marca/grupo/línea en `0` → columnas en `NULL`, no en `0`.
2. Tras el alta: `referencia = LPAD(codProducto,9,'0')`, `estado=1`, `codsunat` derivado de `tipoimpuesto`.
3. Existen exactamente 3 filas en `unidadequivalente` (`compra_venta` 0/1/2); la de venta trae el precio.
4. Existe 1 fila en `productoalmacen` por cada `almacen`, todo en 0.
5. `CUPC/CUPV` del listado coinciden con la fórmula del §6.
6. Con stock `>0` en cualquier almacén el delete devuelve el detalle por almacén y no da de baja.
