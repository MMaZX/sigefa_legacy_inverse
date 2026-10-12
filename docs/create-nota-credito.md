# Create Sales Credit Note (Nota de Credito de Venta) — Stored Procedure Chain

> Scope: sales credit notes only (not `NotaCreditoCompra`). UI entry point is
> `SIGEFA.Formularios/frmNotadeCredito.cs:btnGuardar_Click:979`.
> Architecture: `Formularios -> Administradores -> Interfaces -> InterMySql -> Entidades`.
> This document lists the stored procedures in execution order and the purpose of each,
> so any backend can reproduce the creation correctly. No UI click-steps.

## 1. Mandatory reads before writing

These do not create the NC, but creating without them produces an invalid NC.

| Procedure | Code location | Purpose |
|---|---|---|
| `MuestratipoNC` | `SIGEFA.InterMySql/MysqlProducto.cs:3471` | Loads SUNAT motive codes into `cmbMotivo`. Drives branching: `01` = total (annuls everything), `07` = partial with despacho handling, `04/05/09` = direct-only NC without stock re-entry. |
| `MuestraSeriePorDocumentoAsociado`, `MuestraSerie`, `traeNumeracion`, `traeCodSerie` | `SIGEFA.InterMySql/MysqlSerie.cs:471,215,327,352` | Resolves valid serie + next correlativo for the NC (doc type 4 or 8 depending on source invoice). Prevents duplicate document numbers. |
| `MuestraFacturaVenta`, `MuestraDetalleFacturaVenta`, `CargaDetalleVentaCredito` | `SIGEFA.InterMySql/MysqlFacturaVenta.cs:1038,1223,1697` | Loads source invoice header and detail. Provides `CodReferencia = venta.CodFacturaVenta`, amounts, and lines to credit. |
| `MuestraPagoVenta`, `GetPagosVenta` | `SIGEFA.InterMySql/MysqlPago.cs:582,845` | Lists cobranzas applied to the source invoice. Required later to annul them. |
| `VerificaEntregasActivasRespectoADespacho`, `cargaDespachoSegunDocRelacionado`, `ListaDetalleDespacho` | `SIGEFA.InterMySql/MysqlDespacho.cs` via `admdespacho` | For motive `01`, blocks total annulment when active despacho deliveries exist. For `07`, confirms despacho snapshot did not change mid-creation. |
| `VerificarNCVentaAplicada` | `SIGEFA.InterMySql/MysqlNotaIngreso.cs:1561` | Rejects a second NC against the same invoice. Input is the mirror `clsNotaIngreso`; returns applied flag via `nota.Comentario` / `Aplicada`. |
| `ValidaAnulacionVenta` | `SIGEFA.InterMySql/MysqlFacturaVenta.cs:2241` | Checks whether the source invoice is already annulled before attempting annulment. |

Supporting lookups: `MuestraCliente` (customer data for SUNAT), `MuestraTransaccion` + `MuestraConfiguracion` (transaction config), `listaAlmacenxNombre` (warehouse scope).

## 2. Creation writes in order

### Step 1 — `GuardaNotaCredito` (header, required first)
- Code: `SIGEFA.InterMySql/MysqlNotaCredito.cs:28` via `clsAdmNotaCredito.insert` (`SIGEFA.Administradores/clsAdmNotaCredito.cs:16`), called at `frmNotadeCredito.cs:1225,1276`.
- Key inputs (`clsNotaCredito notc`): `DocumentoNotaCredito`, `NumFac` (= `txtNumero`), `CodTipoTransaccion`, `CodTipoDocumento` (4, or 8 if source is 8), `Moneda`, `TipoCambio`, `FechaIngreso`, `Comentario`, `MontoBruto`, `MontoDscto`, `Igv`, `Flete`, `Total` (= `pendiente` initially), `Estado=1`, `CodUser`, `CodSerie`, `Serie`, `CodReferencia` (= source `CodFacturaVenta`), `CodCliente`, `CodAlmacen`, `Motivo`, `MovimientoNC` (0 when motive index 1, else 1), `FormaPago`, `FechaPago`, `Gravadas`, `Exoneradas`, `Inafectas`, `Gratuitas`, `Tipofacturacion`, `icbper`.
- Output: `newid OUTPUT` -> `nota.CodNotaCreditoNueva`. All detail steps depend on this ID.
- Purpose: creates the electronic NC header linked to the source invoice.

### Step 2 — `GuardaDetalleNotaCredito` per line (required)
- Code: `SIGEFA.InterMySql/MysqlNotaCredito.cs:106` via `clsAdmNotaCredito.insertdetalle`, looped over `RecorreDetalle()` at `frmNotadeCredito.cs:1232,1287`.
- Key inputs (`clsDetalleNotaCredito`): `CodProducto`, `CodNotaCredito` (from Step 1), `CodNotaIngreso`, `Moneda`, `UnidadIngresada`, `SerieLote`, `PrecioUnitario`, `Subtotal`, `Descuento1/2/3`, `MontoDescuento`, `Igv`, `Flete`, `Importe`, `PrecioReal`, `ValoReal`, `FechaIngreso`, `CodUser`, `CodAlmacen`, `Cantidad`, `Descripcion`, `TipoImpuesto`, `icbper`, `icbper_band`. When motive is `04`, `CodNotaIngreso` is forced to `"0"`.
- Output: `newid OUTPUT` -> `CodDetalleNotaCredito`.
- Purpose: creates SUNAT-validated line items. One call per grid row.

### Step 3 — Mirror stock re-entry (conditional)
- Branch: if motive is `04`, `05`, or `09`, SKIP this step (direct-only NC).
- Otherwise:
  - `GuardaNotaIngreso` (`SIGEFA.InterMySql/MysqlNotaIngreso.cs:32`) via `clsAdmNotaIngreso.insert` at `frmNotadeCredito.cs:1271`.
  - `GuardaDetalleIngreso` (`SIGEFA.InterMySql/MysqlNotaIngreso.cs:261,411`) via `clsAdmNotaIngreso.insertdetalle` at `frmNotadeCredito.cs:1282`.
- Link: `notc.CodNotaIngreso = nota.CodNotaIngreso` before `AdmFact.insert` in this branch.
- Purpose: creates the Nota de Ingreso mirror that returns goods to the warehouse. Omitting it on a normal motive leaves stock inconsistent.

### Step 4 — Link NC to source invoice (required)
- Procedure: `ActualizarCodNocreditoFV`
- Code: `SIGEFA.InterMySql/MysqlNotaCredito.cs:468` via `actualizarCodNotaCreditoFV` at `frmNotadeCredito.cs:1265,1294`.
- Inputs: `codfac` (source `CodFacturaVenta`), `codnota` (`CodNotaCreditoNueva`), `tipoNC` (`T` = total when `nctotal`, `P` = partial when `ncparcial`, else `N`).
- Purpose: binds invoice to NC for `getTotalNotaCredito`, `ListaNotasCredito`, and balance queries.

### Step 5 — Annul source invoice and its payments (required for `01`, applied per flow otherwise)
- `AnularFacturaVenta` (NC scope): `SIGEFA.InterMySql/MysqlNotaCredito.cs:496` via `anularFactura_venta` at `frmNotadeCredito.cs:1266`.
- `AnularFacturaVenta` (sales scope): `SIGEFA.InterMySql/MysqlFacturaVenta.cs:506,532` via `clsAdmVenta.anular` at `frmNotadeCredito.cs:1251`, guarded by `ValidaAnulacionVenta`.
- `AnulaDetalleVenta`: `SIGEFA.InterMySql/MysqlFacturaVenta.cs:2469` via `AdmVenta.AnulaDetalleVenta` at `frmNotadeCredito.cs:1303` (per grid row: `coddetalle`, `codproducto`).
- `AnularPago`: `SIGEFA.InterMySql/MysqlPago.cs:529` via `admPago.AnularPago` at `frmNotadeCredito.cs:1256`, iterated over `dtPagos` from `GetPagosVenta`.
- Purpose: marks the source invoice and its detail as annulled by the NC and voids its cobranzas. Skipping this leaves the invoice collectible and the customer balance duplicated.

## 3. Close-out (not a stored procedure)

- `DatosNCredito(cli, notc, detalleNotaCredito)` — SUNAT electronic submission, skipped only when `venta.CodTipoDocumento == 8`.
- `printNC()` + `sololectura(true)` — print and lock the form.

## 4. Backend checklist

1. Resolve motive first; it selects the branch in Step 3 and the `T/P/N` flag in Step 4.
2. Resolve serie + correlativo before Step 1; treat duplicate-document errors as retryable with a new correlativo.
3. Execute Step 1, capture `newid`, then execute Step 2 once per line with that ID.
4. Execute Step 3 only for non-`04/05/09` motives; capture the mirror `CodNotaIngreso` and store it on the NC.
5. Execute Step 4 with the correct `T/P/N` flag.
6. Execute Step 5 (invoice + detail + payments) inside the same logical unit as Steps 1–4; on any failure, roll back the NC header to avoid orphan details.
7. Submit to SUNAT after the DB unit commits; a SUNAT failure must not roll back the DB NC but must flag it for retry.

## 5. Related but out of scope for creation

- `ListaNotasCredito`, `ListaNotasCreditosAplicadas`, `ListaNotasCreditoPorProducto`, `MostrarNCsinRepositorio` — listings.
- `MuestraNotaIngreso`, `MuestraNotaIngreso_Regeneracion`, `MuestraDetalleNotaIngreso` — reprint/recovery.
- `AnularNotaCredito` — voids an existing NC (different from annulling the source invoice).
- Despacho/transferencia extorno (`admdespacho.anular`, `admTransferencia.insert`) — only for total NCs with linked logistics documents.
