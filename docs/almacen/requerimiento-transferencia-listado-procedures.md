# SIGEFA — Almacén > Requerimiento de Almacén > Listado Req. y Transf. (Vista combinada)

Documentación de los procedures SQL (MySQL/MariaDB) ejecutados en la vista **Listado Requerimientos Con Transferencias** y sus acciones de doble clic.

Fuente: rastreo estático del código C# (WinForms, .NET Framework 4.8).
Ruta del proyecto: `/home/fulanito/development/sigefa_legacy_inverse`

---

## 1. Formulario de la vista

| Archivo | Clase | Título de ventana |
|---------|-------|-------------------|
| `SIGEFA.Formularios/frmListaRequerimientoCompleta.cs` | `frmListaRequerimientoCompleta` | `Listado Requerimientos Con Transferencias` |

Esta es la vista mostrada en la imagen: un grid maestro de **requerimientos** arriba y un grid detalle de **transferencias** abajo, con relación maestro-detalle.

---

## 2. Arquitectura de llamadas

```
frmListaRequerimientoCompleta.cs (Formulario)
    │
    ├── clsAdmRequerimientoAlmacen (Administrador de negocio)
    │       │
    │       └── SIGEFA.InterMySql/MysqlRequerimientoAlmacen.cs (DAL)
    │               └── Procedures SQL de MySQL
    │
    └── clsAdmAlmacen (Administrador de almacén)
            │
            └── SIGEFA.InterMySql/MysqlAlmacen.cs (DAL)
                    └── Procedures SQL de MySQL
```

---

## 3. Procedures SQL ejecutados

### 3.1 Carga inicial del grid de requerimientos (al cargar el formulario)

**Evento:** `frmListaRequerimientoCompleta_Load`  
**Línea 67:** `rgvrequerimientos.DataSource = admreqalm.ListadoRequerimientos(...)`

| Capa | Método | Archivo | Procedure SQL | Parámetros |
|------|--------|---------|---------------|------------|
| Formulario | `frmListaRequerimientoCompleta_Load` | `frmListaRequerimientoCompleta.cs:67` | — | `iCodAlmacen`, `iCodSucursal`, `tipoFecha=1`, `desde`, `hasta`, `codProducto` |
| Administrador | `clsAdmRequerimientoAlmacen.ListadoRequerimientos` | `clsAdmRequerimientoAlmacen.cs:216` | — | `_codAlmacen`, `_codSucursal`, `_tipoFecha`, `fechaini`, `fechafin`, `codProducto`, `_tipoListado` |
| DAL | `MysqlRequerimientoAlmacen.ListadoRequerimientoAlmacen` | `MysqlRequerimientoAlmacen.cs:1122` | **`ListadoRequerimientoAlmacen`** | `_codAlmacen`, `_codSucursal`, `_tipoFecha`, `fechaini`, `fechafin`, `codProducto`, `_tipoListado` |

### 3.2 Carga de almacenes (combobox)

**Evento:** `cargaAlmacenes()` — llamado desde el Load  
**Línea 74:** `cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen)`

| Capa | Método | Archivo | Procedure SQL | Parámetros |
|------|--------|---------|---------------|------------|
| Formulario | `cargaAlmacenes` | `frmListaRequerimientoCompleta.cs:70` | — | `iCodAlmacen` |
| Administrador | `clsAdmAlmacen.listaAlmacenxNombre` | `clsAdmAlmacen.cs:217` | — | `codalma` |
| DAL | `MysqlAlmacen.almacenxNombre` | `MysqlAlmacen.cs:489` | **`listAlmacenxnombres`** | `_codalma` |

### 3.3 Carga de transferencias al hacer clic en una fila de requerimiento (CellClick)

**Evento:** `rgvrequerimientos_CellClick`  
**Línea 82:** `rgvTransferencias.DataSource = admreqalm.listadoTransferenciasGeneradas(codigoreq, frmLogin.iCodAlmacen)`

| Capa | Método | Archivo | Procedure SQL | Parámetros |
|------|--------|---------|---------------|------------|
| Formulario | `rgvrequerimientos_CellClick` | `frmListaRequerimientoCompleta.cs:77` | — | `codigoreq` (de la fila clickeada), `iCodAlmacen` |
| Administrador | `clsAdmRequerimientoAlmacen.listadoTransferenciasGeneradas` | `clsAdmRequerimientoAlmacen.cs:151` | — | `codigoReq`, `codAlmacen` |
| DAL | `MysqlRequerimientoAlmacen.ListadoTransferenciasGeneradas` | `MysqlRequerimientoAlmacen.cs:232` | **`ListarTransferenciasGeneradasReqAlmacen`** | `codReqAlmacen`, `codalma` |

### 3.4 Doble clic en celda de requerimiento (columna `colDocReqAlmacen`)

**Evento:** `rgvrequerimientos_CellDoubleClick` — líneas 86-125

| Qué hace | Método / Archivo | Procedure SQL |
|----------|------------------|---------------|
| Carga la cabecera del requerimiento | `admreqalm.CargaRequerimiento(codReqAlmacen)` → `clsAdmRequerimientoAlmacen.CargaRequerimiento` (`clsAdmRequerimientoAlmacen.cs:190`) → `MysqlRequerimientoAlmacen.CargaRequerimiento` (`MysqlRequerimientoAlmacen.cs:985`) | **`CargaRequerimientoAlmacen`** (parámetro: `_codReqAlmacen`) |
| Abre el formulario de detalle | Instancia `frmReqAlmacen` con `codRequerimientoAlmacen`, `TipoReq = req.Tipo`, `Proceso = 2` | — |

**Acción al dar doble clic en una celda de requerimiento:**  
Si la columna clickeada es `colDocReqAlmacen` (el número de requerimiento, ej. `RQAL 004-00006453`), el sistema:
1. Obtiene el `codReqAlmacen` de la fila.
2. Ejecuta `CargaRequerimientoAlmacen` para traer los datos del requerimiento.
3. Abre el formulario `frmReqAlmacen` en modo `Proceso = 2` (solo lectura / ver detalle), pasando el código del requerimiento y su tipo.

Si la columna es `colTituloDocVenta`, abre `frmVenta` con `Proceso = 3` usando el código de factura de venta.

### 3.5 Doble clic en celda de transferencia (columna `colDocTrans`)

**Evento:** `rgvTransferencias_CellDoubleClick` — líneas 127-140

| Qué hace | Método / Archivo | Procedure SQL |
|----------|------------------|---------------|
| Abre el formulario de transferencia | Instancia `F2TransferenciaEntreAlmacenes` con `CodTransDirecta = coTrans`, `Proceso = 3`, `caso = caso` | — (no hay procedure SQL directo; es navegación a otro formulario) |

**Acción al dar doble clic en una celda de transferencia:**  
Si la columna clickeada es `colDocTrans` (el número de transferencia, ej. `TD 001-00012715`), el sistema abre el formulario `F2TransferenciaEntreAlmacenes` en modo `Proceso = 3` para ver/gestionar esa transferencia directa entre almacenes.

---

## 4. Resumen de procedures SQL únicos (esta vista)

| # | Procedure SQL | Se usa para | Archivo DAL |
|---|---------------|-------------|-------------|
| 1 | `ListadoRequerimientoAlmacen` | Grid maestro de requerimientos | `MysqlRequerimientoAlmacen.cs:1128` |
| 2 | `listAlmacenxnombres` | Combo de almacenes | `MysqlAlmacen.cs:495` |
| 3 | `ListarTransferenciasGeneradasReqAlmacen` | Grid detalle de transferencias (CellClick en requerimiento) | `MysqlRequerimientoAlmacen.cs:238` |
| 4 | `CargaRequerimientoAlmacen` | Doble clic en requerimiento (carga cabecera) | `MysqlRequerimientoAlmacen.cs:991` |

---

## 5. Archivos de código fuente implicados

| Capa | Archivo |
|------|---------|
| Formulario principal (vista de la imagen) | `SIGEFA.Formularios/frmListaRequerimientoCompleta.cs` |
| Formulario detalle requerimiento (se abre al doble clic) | `SIGEFA.Formularios/frmReqAlmacen.cs` |
| Formulario detalle transferencia (se abre al doble clic en transferencia) | `SIGEFA.Formularios/F2TransferenciaEntreAlmacenes.cs` |
| Administrador requerimiento almacén | `SIGEFA.Administradores/clsAdmRequerimientoAlmacen.cs` |
| Administrador almacén | `SIGEFA.Administradores/clsAdmAlmacen.cs` |
| DAL requerimiento almacén | `SIGEFA.InterMySql/MysqlRequerimientoAlmacen.cs` |
| DAL almacén | `SIGEFA.InterMySql/MysqlAlmacen.cs` |

---

## 6. Notas para el siguiente agente

- Este documento reemplaza al anterior, que documentaba formularios diferentes (`frmlistatranferencias.cs`, `frmControlRequerimientosAlmacen.cs`, `frmLIstadoReqAlmacen.cs`). La vista de la imagen es **exclusivamente** `frmListaRequerimientoCompleta.cs`.
- Los procedures se invocan con `CommandType.StoredProcedure` desde `MySql.Data`.
- Los nombres de procedures conservan mayúsculas/minúsculas exactas del código C# (por ejemplo `listAlmacenxnombres` con minúscula inicial).
- Para verificar la definición real en la base de datos: `SHOW CREATE PROCEDURE <nombre>` o consultar `information_schema.routines`.
- Servidor local: `127.0.0.1:3307`, db: `database_multi_final`.
- Ruta de este documento: `docs/almacen/requerimiento-transferencia-listado-procedures.md`
