# Formatos de Impresion - SIGEFA

## Arquitectura General

- **Motor:** Crystal Reports (archivos `.rpt` en formato binario OLE2)
- **Ubicacion:** Raiz del proyecto como `SIGEFA.Reportes.CR*.rpt`
- **Clases wrapper:** `SIGEFA.Reportes/CR*.cs` (auto-generadas, heredan `ReportClass`)
- **Datasets:** `SIGEFA.Reportes.clsReportes/cls*.cs` - proveen datos via stored procedures MySQL
- **Viewer:** `frmRptFactura` con `CrystalReportViewer`
- **Impresora:** Configurada por serie (`ser.NombreImpresora`, `ser.PaperSize`)
- **Firma digital:** Imagen QR cargada desde `C:\DOCUMENTOS-{RUC}\CERTIFIK\QR\{nombre}.jpeg`

## Stored Procedures (Datasource)

| Stored Procedure | DataSet/Table | Uso |
|---|---|---|
| `ReporteFactura` | default | Factura/Boleta formato antiguo |
| `ReporteFactura2` | `dtFactura` | Factura/Boleta/NC formato principal |
| `ReporteBoleta` | default | Boleta (formato dedicado) |
| `ReporteGuia` | default | Guia de remision |
| `ReporteSalidaDespacho` | `dtsalidadespacho` | Salida de despacho |
| `AlertaFacturasLetrasVencidas` | `RPTFacturasLetraXvencer` | Reporte de vencimiento |

Los datasets se exportan como XML schema a `C:\XML\{nombre}RPT.xml` al ejecutarse.

---

## 1. Boleta de Venta (CodTipoDocumento = 1)

| Archivo .rpt | Tipo | Forma Pago | Invocado desde |
|---|---|---|---|
| `CRReporteBoleta.rpt` | A4 PDF | Cualquiera | Definido en `SIGEFA.Reportes/`, cargado dinamicamente |
| `CRReporteBoletaFormatoAntiguo.rpt` | A4 PDF antiguo | Cualquiera | Definido en `SIGEFA.Reportes/` |
| `CRFacturaFomatoContinuo.rpt` | Ticket/Continuo | Normal | `frmVenta.cs:3170` (`printaBoleta`) |
| `CRFacturaFomatoContinuocont.rpt` | Ticket credito | 5 o 6 | `frmVenta.cs:2954` (`imprime`) |
| `CRFacturaFomatoContinuoAnulado.rpt` | Ticket anulado | Cualquiera | `frmVenta.cs:2873` (`imprimeAnulado`) |
| `CRReporteFactura.rpt` | A4 PDF estandar | Normal | `frmVenta.cs:3958` (`btnImprimirPdf_Click`) |
| `CRReporteFacturacont.rpt` | A4 PDF credito | 5 o 6 | `frmVenta.cs:3929` (`btnImprimirPdf_Click`) |
| `CRReporteFacturaAnulado.rpt` | A4 PDF anulado | Cualquiera | `frmVenta.cs:2874` (`imprimeAnulado`) |

**Nota:** Boleta y Factura comparten los mismos archivos `.rpt`. La diferencia es el directorio de salida (`BOLETAS\` vs `FACTURAS\`) y el nombre (`B` vs `F` en el correlativo).

## 2. Factura (CodTipoDocumento = 2)

Mismos archivos que Boleta (seccion 1). El sistema determina si es boleta o factura por `CodTipoDocumento`. Los PDFs se guardan en rutas distintas:
- Boleta: `C:\DOCUMENTOS-{RUC}\DOCUMENTOS ENVIAR\BOLETAS\{RUC}-03-B{serie}-{numero}.xml`
- Factura: `C:\DOCUMENTOS-{RUC}\DOCUMENTOS ENVIAR\FACTURAS\{RUC}-01-F{serie}-{numero}.xml`

## 3. Nota de Credito (CodTipoDocumento = 7)

| Archivo .rpt | Tipo | Invocado desde |
|---|---|---|
| `CRNotaCreditoVenta.rpt` | Ticket (impresion directa) | `frmVenta.cs:2814`, `frmGeneraVenta.cs:2049/2078` |
| `CRNotaCreditoVentaTicket.rpt` | Ticket NC | `frmNotadeCredito.cs:2587` |
| `CRNotaCredito.rpt` | NC compra | `frmNotadeCreditoCompra.cs:1620` |
| `CRReporteNotaFormatoAntiguo.rpt` | NC formato antiguo | Definido en `SIGEFA.Reportes/` |
| `CRListaNotas.rpt` | Listado NC | Formularios de listado |

Cuando `CodTipoDocumento == 7` en `frmVenta`, se llama a `fnImprimir()` → `PrintaDocumento()` que usa `CRFacturaInsumos` cargando `CRNotaCreditoVenta.rpt`.

## 4. Nota de Debito

| Archivo .rpt | Tipo | Invocado desde |
|---|---|---|
| `CRNotaDebitoVenta.rpt` | ND venta | `frmNotadeDebito.cs:850` |
| `CRNotaDebito.rpt` | ND compra | `frmNotadeDebitoCompra.cs:1114` |
| `CRListaNotasDebito.rpt` | Listado ND | Formularios de listado |

## 5. Despacho

| Archivo .rpt | Tipo | Invocado desde |
|---|---|---|
| `CRDespacho.rpt` | Estandar A4 | `frmDespacho.cs:847` |
| `CRDespachoFormatoContinuo.rpt` | Ticket/Continuo | `frmDespacho.cs:871`, `frmVenta2019.cs:4010` |
| `CRDespachoDocumento.rpt` | Por documento | `SIGEFA.Reportes/` (con `frmParamDespachoDocumento`) |
| `CRDespachoxArticulo.rpt` | Por articulo | `SIGEFA.Reportes/` |
| `CRSalidaDespacho.rpt` | Salida despacho | SP: `ReporteSalidaDespacho` |
| `CRResumenEntregas_Despacho.rpt` | Resumen | `SIGEFA.Reportes/` |
| `CRMercaderiaEntregar.rpt` | Mercaderia x entregar | `frmMercaderiaEntregar.cs` |

## 6. Entrega

| Archivo .rpt | Tipo | Invocado desde |
|---|---|---|
| `CREntrega.rpt` | Estandar A4 | `frmEntrega.cs:221` |
| `CREntregaFormatoContinuo.rpt` | Ticket/Continuo | `frmEntrega.cs:243` |
| `CRReporteListadoEntrega.rpt` | Listado | `frmListadoReporteEntrega.cs` |

---

## Logica de Seleccion de Formato (frmVenta)

```
btnImprimir_Click (impresion directa a impresora)
│
├── venta.Anulado == 1 → imprimeAnulado()
│   ├── CodTipoDocumento == 7 → fnImprimir() → ticket (CRNotaCreditoVenta.rpt)
│   └── CodTipoDocumento != 7
│       ├── CRFacturaFomatoContinuoAnulado.rpt → PrintToPrinter
│       └── CRReporteFacturaAnulado.rpt → ExportToDisk(PDF) a BOLETAS\ o FACTURAS\
│
└── venta.Anulado != 1 → imprime()
    ├── CodTipoDocumento == 7 → fnImprimir() → ticket (CRNotaCreditoVenta.rpt)
    └── CodTipoDocumento != 7
        ├── FormaPago == 5 o 6 → CRFacturaFomatoContinuocont.rpt → PrintToPrinter
        └── FormaPago != 5/6  → CRFacturaFomatoContinuo.rpt → PrintToPrinter

btnImprimirPdf_Click (exportar PDF)
│
├── codDocumento == "01" (Factura) → Ruta: FACTURAS\
└── codDocumento == "03" (Boleta)  → Ruta: BOLETAS\
    ├── FormaPago == 5 o 6 → CRReporteFacturacont.rpt → ExportToDisk(PDF)
    └── FormaPago != 5/6  → CRReporteFactura.rpt → ExportToDisk(PDF)

printaBoleta() (impresion automatica post-guardado)
│
├── ser.CodDocumento == 26 → CRFacturaFomatoContinuo.rpt → PrintToPrinter
└── ser.CodDocumento == 1  → CRFacturaFomatoContinuo.rpt → PrintToPrinter + ActualizaEstadoImpreso

fnImprimir() → PrintaDocumento()
│
└── CRFacturaInsumos cargando CRNotaCreditoVenta.rpt → PrintToPrinter
    (margen: 0,0,0,5 - papel continuo)
```

### Reglas de seleccion

| Condicion | Formato Ticket (print) | Formato A4 (PDF) |
|---|---|---|
| FormaPago normal (1-4) | `CRFacturaFomatoContinuo` | `CRReporteFactura` |
| FormaPago credito (5-6) | `CRFacturaFomatoContinuocont` | `CRReporteFacturacont` |
| Documento anulado | `CRFacturaFomatoContinuoAnulado` | `CRReporteFacturaAnulado` |
| Nota credito (tipo 7) | `CRNotaCreditoVenta` / `CRFacturaInsumos` | - |

### Configuracion de impresion

```csharp
PrintOptions rptoption = rpt.PrintOptions;
rptoption.PrinterName = ser.NombreImpresora;       // impresora configurada en serie
rptoption.PaperSize = GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
rptoption.ApplyPageMargins(new PageMargins(0, 0, 0, 5)); // margen inferior 5mm
rpt.PrintToPrinter(1, collated: false, 1, 1);       // 1 copia, paginas 1 a 1
```

---

## Sobre los archivos .rpt

### Formato binario (NO legible como texto)

Los `.rpt` son **Composite Document File V2** (formato OLE2 binario de Microsoft), creados con Crystal Reports. No se pueden abrir/editar como texto plano.

### Que se PUEDE leer del codigo

1. **Clases wrapper** (`SIGEFA.Reportes/CR*.cs`): Muestran las secciones del reporte (Section1-5 = ReportHeader, PageHeader, Details, PageFooter, ReportFooter)
2. **Datasets** (`SIGEFA.Reportes.clsReportes/cls*.cs`): Muestran los stored procedures MySQL que alimentan cada reporte y los nombres de las tablas del DataSet
3. **XML schemas** (`C:\XML\*RPT.xml`): Si existen en la maquina de ejecucion, contienen la estructura exacta de campos

### Para editar los .rpt

Se necesita **Crystal Reports** (SAP Crystal Reports, version compatible). Las opciones:
- Abrir con Crystal Reports Designer
- Usar la API de Crystal Reports para modificar programaticamente
- Exportar a XML desde Crystal Reports y re-importar

### Alternativa sin Crystal Reports

Los datos que Crystal muestra vienen de los stored procedures. Se puede:
1. Ejecutar los SPs directamente en MySQL para ver los campos: `CALL ReporteFactura2(123);`
2. Revisar los XML generados en `C:\XML\` si la aplicacion ya se ejecuto
3. Reconstruir el formato visual desde el codigo que pobla los datasets
