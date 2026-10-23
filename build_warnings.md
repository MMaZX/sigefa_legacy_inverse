# Build Warnings — SIGEFA

> **Política:** Comentar código, **nunca eliminar**. Puede ser importante en el futuro.
> Generado del build del 2026-07-30. Total: ~200+ warnings, 0 errores.

---

## Índice por categoría

| Código | Descripción | Cantidad |
|--------|-------------|----------|
| [CS4014](#cs4014--llamada-async-sin-await) | Llamada async sin `await` | 2 |
| [CS1717](#cs1717--auto-asignación) | Auto-asignación a la misma variable | 1 |
| [CS1522](#cs1522--bloque-switch-vacío) | Bloque `switch` vacío | 1 |
| [CS1690](#cs1690--acceso-a-campo-de-clase-marshal-by-reference) | Acceso a campo de clase marshal-by-reference | 3 |
| [CS0252](#cs0252--comparación-de-referencias-no-intencionada) | Comparación de referencias en vez de valores | ~40 |
| [CS0612](#cs0612--api-obsoleta-displaygrouptree) | API obsoleta `DisplayGroupTree` | ~48 |
| [CS0162](#cs0162--código-inalcanzable) | Código inalcanzable (`unreachable code`) | ~13 |
| [CS0219](#cs0219--variable-local-asignada-pero-nunca-usada) | Variable local asignada pero nunca usada | ~70 |
| [CS0168](#cs0168--variable-declarada-pero-nunca-usada) | Variable declarada pero nunca usada | 1 |
| [CS0169](#cs0169--campo-privado-nunca-referenciado) | Campo privado nunca referenciado | ~35 |
| [CS0414](#cs0414--campo-asignado-pero-valor-nunca-leído) | Campo asignado pero valor nunca leído | ~70 |
| [CS0649](#cs0649--campo-nunca-asignado) | Campo nunca asignado (siempre valor por defecto) | 9 |

---

## CS4014 — Llamada async sin `await`

**Qué hacer:** Agregar `await` antes de la llamada. Si es fire-and-forget intencional, agregar comentario `// fire-and-forget intencional` y suprimir con `#pragma warning disable CS4014`.

- [ ] `frmVenta.cs:2107` — Agregar `await` o comentar que es fire-and-forget intencional.
- [ ] `frmNotadeCredito.cs:2875` — Agregar `await` o comentar que es fire-and-forget intencional.

---

## CS1717 — Auto-asignación

**Qué hacer:** Revisar si debía asignarse otra variable distinta. Comentar la línea y anotar la duda con `// CS1717 posible bug: ¿debía asignarse otra variable?`.

- [ ] `frmRTransferencia.cs:850` — `x = x` — Revisar si era otra variable el destino correcto.

---

## CS1522 — Bloque switch vacío

**Qué hacer:** Agregar `// default: break; // pendiente de implementar` dentro del switch vacío para documentar que es intencional.

- [ ] `frmNotadeCreditoCompra.cs:1673` — Switch sin ningún case ni default.

---

## CS1690 — Acceso a campo de clase marshal-by-reference

**Qué hacer:** Copiar el valor del campo a una variable local antes de usarlo. Ej: `var cod = obj.CodProv;` y luego usar `cod`.

- [ ] `frmProveedoresLista.cs:230` — Campo `frmCambioProveedor.CodProv` — copiar a local.
- [ ] `frmProveedoresLista.cs:350` — Campo `frmCambioProveedor.CodProv` — copiar a local.
- [ ] `frmListaPreciosProductos.cs:449` — Campo `frmProductosLista.codigoPro` — copiar a local.
- [ ] `frmListaPreciosProductos.cs:477` — Campo `frmProductosLista.codigoPro` — copiar a local.

---

## CS0252 — Comparación de referencias no intencionada

**Qué hacer:** Cambiar `==` por `.Equals()` o usar `.ToString()` en el lado izquierdo. Ej: `if (obj.ToString() == "valor")` o `if (string.Equals(obj, "valor"))`.

### SIGEFA.Formularios
- [ ] `FormularioParaPruebas.cs:638` — comparación ref vs string
- [ ] `FormularioParaPruebas.cs:766` — comparación ref vs string
- [ ] `FormularioParaPruebas.cs:771` — comparación ref vs string
- [ ] `FormularioParaPruebas.cs:810` — comparación ref vs string
- [ ] `FormularioParaPruebas.cs:872` — comparación ref vs string
- [ ] `frmVentaxEentregar.cs:519` — comparación ref vs string
- [ ] `frmVentaxEentregar.cs:607` — comparación ref vs string
- [ ] `frmVenta.cs:912` — comparación ref vs string
- [ ] `frmVenta.cs:1004` — comparación ref vs string
- [ ] `frmVentaSeparacionAr.cs:369` — comparación ref vs string
- [ ] `frmVentaSeparacionAr.cs:832` — comparación ref vs string
- [ ] `frmReqAlmacen.cs:1036` — comparación ref vs string
- [ ] `frmGuiaRemision.cs:1320` — comparación ref vs string
- [ ] `frmGuiaRemisionCompra.cs:552` — comparación ref vs string
- [ ] `frmGuiaRemisionCompra.cs:1744` — comparación ref vs string
- [ ] `frmGuiaRemisionCompra.cs:1746` — comparación ref vs string
- [ ] `frmGuiaRemisionCompra.cs:2563` — comparación ref vs string
- [ ] `frmDespacho.cs:523` — comparación ref vs string
- [ ] `frmOrdenCompra.cs:1745` — comparación ref vs string
- [ ] `frmListadoProductoFlete.cs:91` — comparación ref vs string
- [ ] `frmGeneraVenta.cs:2787` — comparación ref vs string
- [ ] `frmGeneraVenta.cs:2971` — comparación ref vs string
- [ ] `frmNotaIngreso.cs:2404` — comparación ref vs string
- [ ] `frmNotaIngreso.cs:2409` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:971` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:1177` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:1844` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:1852` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:2132` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:2135` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:2282` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:2287` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:2605` — comparación ref vs string
- [ ] `frmPropuestaDeReqAlmacen.cs:2613` — comparación ref vs string
- [ ] `frmPropuestaDeOrdenCompra.cs:1619` — comparación ref vs string
- [ ] `frmPropuestaDeOrdenCompra.cs:1962` — comparación ref vs string
- [ ] `frmPropuestaDeOrdenCompra.cs:2433` — comparación ref vs string
- [ ] `frmPropuestaDeOrdenCompra.cs:2438` — comparación ref vs string
- [ ] `frmPropuestaDeOrdenCompra.cs:2477` — comparación ref vs string
- [ ] `frmPropuestaDeOrdenCompra.cs:2556` — comparación ref vs string

### SIGEFA.Administradores
- [ ] `clsAdmDespacho.cs:425` — comparación ref vs string

---

## CS0612 — API obsoleta `DisplayGroupTree`

**Qué hacer:** Comentar la línea `viewer.DisplayGroupTree = ...;` y dejar `// CS0612: DisplayGroupTree obsoleto en CrystalReports actual`.

### SIGEFA.Formularios
- [ ] `frmAlmacenesRP.cs:47`
- [ ] `frmAutorizadoRP.cs:47`
- [ ] `frmBancosRP.cs:47`
- [ ] `frmCaracteristicasRP.cs:47`
- [ ] `frmCatalogoRP.cs:47`
- [ ] `frmClientesRP.cs:156`
- [ ] `frmDocumentosRP.cs:47`
- [ ] `frmEmpresasRP.cs:47`
- [ ] `frmFamiliasRP.cs:47`
- [ ] `frmFormaPagoRP.cs:62`
- [ ] `frmMarcasRP.cs:47`
- [ ] `frmPrestamosRP.cs:172`
- [ ] `frmProductosRP.cs:47`
- [ ] `frmProveedoresRP.cs:47`
- [ ] `frmRptPagos.cs:38`
- [ ] `frmrptCotizacion.cs:422`
- [ ] `frmTipoArticulosRP.cs:47`
- [ ] `frmTipoCambioRP.cs:47`
- [ ] `frmTransaccionesRP.cs:47`
- [ ] `frmUnidadesRP.cs:47`
- [ ] `frmUsuariosRP.cs:47`

### SIGEFA.Reportes
- [ ] `frmListaGuias.cs:34`
- [ ] `frmListaNotasCredito.cs:34`
- [ ] `frmListaVentas.cs:43`
- [ ] `frmListaVentasSeparacion.cs:43`
- [ ] `frmNotasIngresoSalida.cs:34`
- [ ] `frmRptArqueo.cs:34`
- [ ] `frmRptArqueoCargado.cs:34`
- [ ] `frmRptCaja.cs:34`
- [ ] `frmRptCobrosGeneral.cs:34`
- [ ] `frmRptCobrosxDia.cs:34`
- [ ] `frmRptComision.cs:34`
- [ ] `frmRptCuotas.cs:34`
- [ ] `frmRptDespachoArticulo.cs:34`
- [ ] `frmRptDespachoDocumento.cs:34`
- [ ] `frmRptEntregaConsultor.cs:34`
- [ ] `frmRptFactura.cs:35`
- [ ] `frmRptGuia.cs:34`
- [ ] `frmRptNotaCredito.cs:34`
- [ ] `frmRptPagosxDia.cs:34`
- [ ] `frmRptRankingArticulo.cs:34`
- [ ] `frmRptRankingCliente.cs:34`
- [ ] `frmRptVentaMesArticulo.cs:35`
- [ ] `frmRptVentCredContDia.cs:35`
- [ ] `FrmRptVentaxArticulo.cs:34`
- [ ] `frmRptVentxArtixVendedor.cs:40`
- [ ] `frmRptVentxCliente.cs:35`
- [ ] `frmRptVentxVendedor.cs:34`

### SIGEFA.Reportes.clsReportes
- [ ] `frmListaNotasDebito.cs:34`

---

## CS0162 — Código inalcanzable

**Qué hacer:** Comentar el bloque inalcanzable con `// CS0162 — código inalcanzable, revisar lógica de control de flujo`.

- [ ] `reporteventasdiarias2.cs:1197` y `:1202`
- [ ] `reporteventasdiarias.cs:978` y `:983`
- [ ] `listaplantillas.cs:574`
- [ ] `frmTecnico.cs:230`
- [ ] `frmStockAlmacenesPendiente.cs:567`
- [ ] `frmPaciente.cs:363`
- [ ] `frmNotaIngreso.cs:1228`
- [ ] `frmNotadeCreditoCompra.cs:2046`
- [ ] `frmListadoProductoFlete.cs:185`
- [ ] `frmGuiaRemision.cs:1037`
- [ ] `frmGuiaRemisionCompra.cs:1864`
- [ ] `frmCatalogo.cs:1336` y `:1541`
- [ ] `ListadoProductos.cs:1846`

---

## CS0219 — Variable local asignada pero nunca usada

**Qué hacer:** Comentar la línea de asignación: `// var x = ...; // CS0219 asignada pero no usada`.

### FinalXML.Librerias
- [ ] `ReniecAPI.cs:77` — `respuesta`

### SIGEFA (mdi_Menu)
- [ ] `mdi_Menu.cs:1764` — `flag`

### SIGEFA.Formularios
- [ ] `reporteventasdiarias2.cs:216-217` — `posSaldoEfectivo`, `posSaldoCajaSistema`
- [ ] `reporteventasdiarias.cs:199-201` — `posNC`, `posSaldoEfectivo`, `posSaldoCajaSistema`
- [ ] `FormularioParaPruebas.cs:435` — `ins`
- [ ] `frmAnalisisDetalladoDeVenta.cs:122` — `sms`
- [ ] `FrmReporteAjustesInventario.cs:182-183` — `fila_a_concatenar`, `fila_first_concat`
- [ ] `frmCajaVentasMovimientos.cs:1128` — `contador`
- [ ] `frmCajaVentasMovimientos.cs:2319` — `contador`
- [ ] `frmCancelarCobroMultiple.cs:543` — `band`
- [ ] `frmCancelarCobroMultiple.cs:847` — `flag`
- [ ] `frmCancelarCobroMultiple.cs:862-864` — `sRutaPrincipal`, `sArchivo`, `sRutaFinal`
- [ ] `frmCancelarPago.cs:1302-1304` — `sRutaPrincipal`, `sArchivo`, `sRutaFinal`
- [ ] `frmCancelarPagoMasivo.cs:749-751` — `sRutaPrincipal`, `sArchivo`, `sRutaFinal`
- [ ] `frmCancelarPagoMultiple.cs:538` — `band`
- [ ] `frmCancelarPagoMultiple.cs:713` — `flag`
- [ ] `frmCancelarPagoMultiple.cs:728-730` — `sRutaPrincipal`, `sArchivo`, `sRutaFinal`
- [ ] `frmCanjearCuota.cs:225-226` — `fechaanterior`, `fechaactual`
- [ ] `frmConsolidadoTransferencia.cs:304` — `cantidad_checks`
- [ ] `frmCorreoElectronico.cs:290` — `sRutaPrincipal`
- [ ] `frmCotizacionesVigentes.cs:565-566` — `fila_a_concatenar`, `fila_first_concat`
- [ ] `frmDetalleSalida.cs:697` — `sumdet`
- [ ] `frmDetalleGuia.cs:396` — `estado`
- [ ] `frmGeneraVenta.cs:1547` — `igv`
- [ ] `frmGeneraVenta.cs:1716-1717` — `Ncantidad`, `Nimporte`
- [ ] `frmGeneraVenta.cs:1894` — `igv`
- [ ] `frmGuiaRemision.cs:1202` — `contadorTotal`
- [ ] `frmGuiaRemisionCompra.cs:816` — `a`
- [ ] `frmGuiaRemisionCompra.cs:829` — `u`
- [ ] `frmGuiaRemisionCompra.cs:986` — `band`
- [ ] `frmGuiaRemisionCompra.cs:995-996` — `ordenes`, `band_m`
- [ ] `frmGuiaRemisionCompra.cs:1030-1031` — `ordenes`, `band_m`
- [ ] `frmGuiaRemisionCompra.cs:1063-1064` — `ordenes`, `band_m`
- [ ] `frmGuiaRemisionCompra.cs:1096-1097` — `ordenes`, `band_m`
- [ ] `frmGuiaRemisionCompra.cs:1129-1130` — `ordenes`, `band_m`
- [ ] `frmGuiaRemisionCompra.cs:1216` — `CodProd`
- [ ] `frmGuiaRemisionCompra.cs:1313` — `band`
- [ ] `frmGuiaRemisionCompra.cs:1366` — `band`
- [ ] `frmGuiaRemisionCompra.cs:1419` — `band`
- [ ] `frmListaGuiasFacturacion.cs:334-335` — `fila_a_concatenar`, `fila_first_concat`
- [ ] `frmListadoDocumentoRelacionadoGuiaRemisionCompra.cs:75` — `flag`
- [ ] `frmListadoReporteEntrega.cs:158-159` — `ruta`, `nombreArchivo`
- [ ] `frmListaProductoDespachar.cs:176-179` — `i`, `fila_a_concatenar`, `fila_first_concat`
- [ ] `frmListaProductoDespachar.cs:368` — `i`
- [ ] `ListadoProductos.cs:1549` — `flag`
- [ ] `ListadoProductos.cs:2079` — `uno`
- [ ] `ListadoProductos.cs:2202` — `uno`
- [ ] `frmNotasCredito.cs:248-249` — `fila_a_concatenar`, `fila_first_concat`
- [ ] `frmNotasCreditoAplicadas.cs:243-244` — `fila_a_concatenar`, `fila_first_concat`
- [ ] `frmNotadeCredito.cs:1952` — `flag`
- [ ] `frmNotadeCredito.cs:1993` — `flag`
- [ ] `frmNotadeCredito.cs:2518` — `total_icbper`
- [ ] `frmNotaIngreso.cs:1879` — `rpta`
- [ ] `frmNotaIngreso.cs:1614` — `descuen`
- [ ] `frmOrdenCompra.cs:1377` — `total_f_s_i`
- [ ] `frmPagos.cs:280` — `valor`
- [ ] `frmPagos.cs:760` — `flag`
- [ ] `frmPedidosPendientes.cs:316` — `flag`
- [ ] `frmProductos.cs:326` — `var`
- [ ] `frmPropuestaDeOrdenCompra.cs:437` — `a`
- [ ] `frmPropuestaDeOrdenCompra.cs:722` — `ins`
- [ ] `frmPropuestaDeOrdenCompra.cs:2502` — `a`
- [ ] `frmPropuestaDeOrdenCompra.cs:2993` — `a`
- [ ] `frmPropuestaDeReqAlmacen.cs:421` — `a`
- [ ] `frmPropuestaDeReqAlmacen.cs:731` — `ins`
- [ ] `frmPropuestaDeReqAlmacen.cs:2331` — `a`
- [ ] `frmPropuestaDeReqAlmacen.cs:2547` — `a`
- [ ] `frmReporteControlCotizaciones.cs:141-142` — `fila_a_concatenar`, `fila_first_concat`
- [ ] `frmRegistroComprasLE.cs:126` — `t`
- [ ] `frmReqAlmacen.cs:592` — `a`
- [ ] `frmReqAlmacen.cs:908` — `a`
- [ ] `frmReqAlmacen.cs:1485` — `flag`
- [ ] `frmReqAlmacen.cs:1527` — `flag`
- [ ] `frmReqAlmacen.cs:1795` — `flag`
- [ ] `frmStockAlmacenesPendiente.cs:432` — `mostrar`
- [ ] `frmTranferenciaDirecta.cs:896` — `valor`
- [ ] `frmUnidadEquivalente.cs:361` — `codigo`
- [ ] `frmVenta.cs:2178` — `igv`
- [ ] `frmVenta.cs:2314-2315` — `Ncantidad`, `Nimporte`
- [ ] `frmVenta.cs:2462` — `igv`
- [ ] `frmVenta2019.cs:1391` — `maxPorcDescto`
- [ ] `frmVenta2019.cs:3260` — `flag`
- [ ] `frmVenta2019.cs:3405` — `igv`
- [ ] `frmVenta2019.cs:5248` — `a`
- [ ] `frmVenta2019.cs:6295` — `flag`
- [ ] `frmVentas.cs:732` — `flag`
- [ ] `frmVentas.cs:1012-1013` — `fila_a_concatenar`, `fila_first_concat`
- [ ] `FrmTPenPedido.cs:172` — `a`
- [ ] `FrmTPenPedido.cs:585` — `flag`

### SIGEFA.InterMySql
- [ ] `MysqlEmpresa.cs:422` — `emp`
- [ ] `MysqlEmpresa.cs:425` — `cod`
- [ ] `MysqlFacturaVenta.cs:2520` — `factura`
- [ ] `MysqlGuiaRemision.cs:575` — `guia`
- [ ] `MysqlGuiaRemision.cs:601` — `guia`
- [ ] `MysqlGuiaRemisionCompra.cs:1293` — `guia`
- [ ] `MysqlGuiaRemisionCompra.cs:1319` — `guia`
- [ ] `MysqlNotaCreditoCompra.cs:293` — `rspta`
- [ ] `MysqlNotaCreditoCompra.cs:434` — `rspta`
- [ ] `MysqlNotaCreditoCompra.cs:463` — `rspta`
- [ ] `MysqlNotaCreditoCompra.cs:492` — `rspta`
- [ ] `MysqlNotaCreditoCompra.cs:554` — `rpta`
- [ ] `MysqlNotaIngreso.cs:31` — `msj`
- [ ] `MysqlNotaIngreso.cs:124` — `msj`
- [ ] `MysqlNotaIngreso.cs:180` — `msj`
- [ ] `MysqlNotaIngreso.cs:330` — `msj`
- [ ] `MysqlPago.cs:376` — `opcion`
- [ ] `MysqlPago.cs:625` — `pag`
- [ ] `MysqlPropuestaDePedido.cs:1092` — `rpta`
- [ ] `MysqlSeparacion.cs:27` — `msj`
- [ ] `MysqlSucursal.cs:453` — `cod`
- [ ] `MysqlTarjetaPago.cs:176` — `total`

---

## CS0168 — Variable declarada pero nunca usada

**Qué hacer:** En bloques `catch (Exception ex)` donde `ex` no se usa, cambiar a `catch (Exception)` (sin nombre de variable). Ej: `catch (Exception) { ... }`.

- [ ] `frmLogin.cs:258` — `catch (Exception ex)` donde `ex` nunca se usa — cambiar a `catch (Exception)`.

---

## CS0169 — Campo privado nunca referenciado

**Qué hacer:** Comentar la declaración: `// private tipo nombre; // CS0169 declarado pero nunca referenciado`.

### SIGEFA (mdi_Menu)
- [ ] `mdi_Menu.cs:48` — `rpta`
- [ ] `mdi_Menu.cs:84` — `eligedoc`
- [ ] `mdi_Menu.cs:164` — `tabItem1`

### SIGEFA.SunatFacElec
- [ ] `Comprobante.cs:8` — `URI_SAT`
- [ ] `Comprobante.cs:10` — `m_xmlDOM`
- [ ] `Comprobante.cs:12` — `Nodo`
- [ ] `Comprobante.cs:14` — `DIR_SAT`
- [ ] `Comprobante.cs:16` — `DIR_PKI`
- [ ] `Comprobante.cs:50` — `vMotivoDescuento`
- [ ] `Comprobante.cs:52` — `vTipoComprobante`
- [ ] `Comprobante.cs:54` — `vTotal`
- [ ] `Comprobante.cs:56` — `vDomicilioReceptor`
- [ ] `Comprobante.cs:58` — `vDomicilioExpedicion`
- [ ] `Comprobante.cs:60` — `vDomicilioFiscalEmisor`
- [ ] `Comprobante.cs:62` — `vIVA`
- [ ] `Comprobante.cs:64` — `vIVARetenido`
- [ ] `Comprobante.cs:66` — `vIEPS`
- [ ] `Comprobante.cs:68` — `vISRRetenido`
- [ ] `Comprobante.cs:48` — `vDescuento`

### SIGEFA.Entidades
- [ ] `clsDetalleSeparacionVenta.cs:47` — `iCodVenta`
- [ ] `clsModeloTransporte.cs:13` — `sReferencia`
- [ ] `clsLocalidad.cs:14` — `dr`

### SIGEFA.Formularios
- [ ] `frmVentadetalle.cs:230` — `codCotizacion`
- [ ] `frmVentadetalle.cs:254` — `tipounidad`
- [ ] `frmVenta2019.cs:247` — `codCotizacion`
- [ ] `frmVentaxEentregar.cs:185` — `codCotizacion`
- [ ] `frmOrdenVenta.cs:66` — `lastChecked`
- [ ] `frmNotaOrdenAlmacen.cs:123` — `moneda`
- [ ] `frmMovimientosControl.cs:18` — `p1`
- [ ] `frmListaDocumentosSinGuia.cs:32` — `codigo`
- [ ] `frmGuiaRemision.cs:179` — `vbcrlf`
- [ ] `frmRequerimiento.cs:87` — `superValidator1`
- [ ] `frmRequerimiento.cs:91` — `highlighter1`
- [ ] `frmRegeneracionArchivos.cs:64` — `NumeroDocumento`
- [ ] `frmResumenDiario.cs:51` — `myBackgroundWorker`
- [ ] `frmParametrosDescuentos.cs:31` — `info`
- [ ] `frmPropuestaDeOrdenCompra.cs:101` — `fondo_celda`
- [ ] `frmPropuestaDeReqAlmacen.cs:91` — `fondo_celda`
- [ ] `frmPropuestaDeReqAlmacen.cs:123` — `dataComboModificaciones`
- [ ] `frmPagos.cs:48` — `contextMenu`
- [ ] `frmListadoReporteEntrega.cs:26` — `objfrm`
- [ ] `frminventario.cs:34` — `excel`
- [ ] `frminventario.cs:36` — `obt`
- [ ] `frminventario.cs:38` — `librotrabajo`

### SIGEFA.InterMySql
- [ ] `MysqlNotaSalida.cs:22` — `tran`
- [ ] `MysqlDosis.cs:16` — `dr`
- [ ] `MysqlComposicionQuimica.cs:16` — `dr`
- [ ] `MysqlPlanContable.cs:15` — `dr`
- [ ] `MysqlParametroDescuento.cs:16` — `dr`
- [ ] `MysqlIngresosEgresos.cs:16` — `dr`

---

## CS0414 — Campo asignado pero valor nunca leído

**Qué hacer:** Comentar solo la(s) línea(s) de asignación inicial (no la declaración): `// campo = valor; // CS0414 valor nunca es leído`.

> Este es el grupo más extenso. Se recomienda abordar de a un archivo por vez.

### SIGEFA.Formularios
- [ ] `frmCajaChica.cs:32-40` — `Ingresos`, `Egresos`, `FilasChequeadas`, `MontoRendido`
- [ ] `frmCajaChicaRendicion.cs:32` — `Tipo`
- [ ] `frmCajaVentasMovimientos.cs:69-91` — `Ingresos`, `Egresos`, `FilasChequeadas`, `MontoRendido`, `info`
- [ ] `frmCaja.cs:39-41` — `Ingresado`, `Soles`
- [ ] `frmCancelarPago.cs:146` — `creoCorrelativo`
- [ ] `frmCancelarPagoMasivo.cs:115` — `creoCorrelativo`
- [ ] `frmCancelarPagoMultiple.cs:107` — `aprobar`
- [ ] `frmCancelarPagoMultiple.cs:115` — `creoCorrelativo`
- [ ] `frmCancelarPagoMultiple.cs:127` — `j`
- [ ] `frmCancelarCobroMultiple.cs:84` — `tipopago`
- [ ] `frmCancelarCobroMultiple.cs:116` — `creoCorrelativo`
- [ ] `frmCatalogo.cs:78` — `ultima_fila_seleccionada`
- [ ] `frmDetalleGuia.cs:44` — `factorconvert`
- [ ] `frmDetalleSalida.cs:74-82` — `factorconvert`, `precioprod`
- [ ] `frmDocumentosPendientes.cs:17` — `estado`
- [ ] `frmDocumentosPendientes.cs:23` — `empresa`
- [ ] `frmFlujoCaja.cs:27` — `proceso`
- [ ] `frmGestionCliente.cs:27` — `Validacion`
- [ ] `frmGestionNombreLE.cs:34` — `BI`
- [ ] `frmGestionNombreLE.cs:36` — `DBI`
- [ ] `frmGuiaRemision.cs:113` — `prosoli`
- [ ] `frmGuiaRemision.cs:175` — `o`
- [ ] `frmGuiaRemisionCompra.cs:71` — `fact_compra`
- [ ] `frmGuiaRemisionCompra.cs:111` — `guardar`
- [ ] `frmListaCaja.cs:57` — `FilasChequeadas`
- [ ] `frmListaCaja.cs:59` — `MontoRendido`
- [ ] `frmListadoConsumoRequerimiento.cs:25` — `usuario_click`
- [ ] `frmListadoGuiaRemisionCompraDeOrdenCompra.cs:14` — `grc`
- [ ] `frmListadoReqPendienteSegunAnalisis.cs:17` — `flgloadcboanalisis`
- [ ] `frmListaPreciosProductos.cs:66` — `mar`
- [ ] `frmListaPreciosProductos.cs:74` — `CodListaOrigen`
- [ ] `ListadoProductos.cs:89` — `band_checkbox_principal_dgv`
- [ ] `frmlistatranferencias.cs:26` — `ventas`
- [ ] `frmNotadeCredito.cs:125` — `Validacion`
- [ ] `frmNotadeCredito.cs:197` — `DespachoTotalVenta`
- [ ] `frmNotadeCreditoCompra.cs:131` — `cantprod`
- [ ] `frmNotadeCreditoCompra.cs:133` — `precprod`
- [ ] `frmNotadeDebitoCompra.cs:99` — `Validacion`
- [ ] `frmNotaIngresoPorOrden.cs:103` — `Validacion`
- [ ] `frmNotaSalida.cs:122` — `QPorDespachar2`
- [ ] `frmNotaSalida.cs:124` — `QDespachada2`
- [ ] `frmOrdenCompra.cs:99` — `Punto`
- [ ] `frmPedidosPendientes.cs:40` — `ser`
- [ ] `frmPropuestaDeOrdenCompra.cs` — múltiples campos, ver warnings CS0414 líneas 437-2993
- [ ] `frmPropuestaDeReqAlmacen.cs` — múltiples campos, ver warnings CS0414 líneas 421-2613
- [ ] `frmPromedioProductosVendidos.cs:41` — `detener_proceso`
- [ ] `frmRecibos.cs:29` — `Saldo`
- [ ] `frmRecibos_CajaChica.cs:50` — `sigl`
- [ ] `frmRequerimientosVigentes.cs:33` — `tip`
- [ ] `frmResumenDiario.cs` — ver campo
- [ ] `frmresumenventas.cs:31` — `ventas`
- [ ] `frmTodosProductosPropuestaOrdenDeCompra.cs:31` — `filaIndex`
- [ ] `frmTodosProductosPropuestaReqAlmacen.cs:31` — `filaIndex`
- [ ] `frmTranferenciaDirecta.cs:75` — `Validacion`
- [ ] `frmUnidadEquivalente.cs:43` — `codEquiVenta`
- [ ] `frmVenta.cs:221` — `CodLista`
- [ ] `frmVenta.cs:235` — `Salida`
- [ ] `frmVenta.cs:277` — `i`
- [ ] `frmVenta2019.cs:43` — `label6`
- [ ] `frmVenta2019.cs:45` — `label7`
- [ ] `frmVenta2019.cs:157` — `keyHold`
- [ ] `frmVenta2019.cs:185` — `tablaFiltro`
- [ ] `frmVentadetalle.cs:214` — `CodLista`
- [ ] `frmVentadetalle.cs:216` — `Validacion`
- [ ] `frmVentadetalle.cs:218` — `TipoCambio`
- [ ] `frmVentadetalle.cs:220` — `ret`
- [ ] `frmVentadetalle.cs:224` — `item`
- [ ] `frmVentadetalle.cs:228` — `Salida`
- [ ] `frmVentaxEentregar.cs:171` — `CodLista`
- [ ] `frmVentaxEentregar.cs:177` — `ret`
- [ ] `frmVentaxEentregar.cs:183` — `Salida`
- [ ] `frmVentaSeparacionAr.cs:76` — `TipoCambio`
- [ ] `frmVentaSeparacionAr.cs:78` — `ret`
- [ ] `frmVentas.cs:149` — `NumeroDocumento`
- [ ] `frmVentasPendientesdeDespacho.cs:47` — `CodTipoDoc`
- [ ] `ventasdiarias.cs:45` — `ventas`
- [ ] `FrmTPenPedido.cs:51` — `ser`

### SIGEFA.SunatFacElec
- [ ] `Facturacion.cs:79` — `lista_repositorio`

### SIGEFA.InterMySql
- [ ] `MysqlAcceso.cs:19` — `adap`
- [ ] `MysqlAcceso.cs:21` — `tabla`
- [ ] `MysqlAccesoSucursales.cs:19` — `adap`
- [ ] `MysqlAccesoSucursales.cs:21` — `tabla`
- [ ] `MysqlArqueo.cs:16` — `dr`
- [ ] `MysqlConciliacionBancaria.cs:16` — `dr`
- [ ] `MysqlConciliacionBancaria.cs:18` — `adap`
- [ ] `MysqlConciliacionBancaria.cs:20` — `tabla`
- [ ] `MysqlDespacho.cs:21` — `tra`
- [ ] `MysqlDocumentoRescom.cs:16` — `dr`
- [ ] `MysqlDocumentoRescom.cs:18` — `adap`
- [ ] `MysqlDocumentoRescom.cs:20` — `tabla`
- [ ] `MysqlFacturaVenta.cs:24` — `tra`
- [ ] `MysqlGuiaFacturacion.cs:20` — `tra`
- [ ] `MysqlNewConfiguracion.cs:15` — `adap`
- [ ] `MysqlNewConfiguracion.cs:17` — `tabla`
- [ ] `MysqlNotaDebito.cs:16` — `dr`
- [ ] `MysqlNotaDebito.cs:18` — `adap`
- [ ] `MysqlNotaDebito.cs:20` — `tabla`
- [ ] `MysqlNotaIngresoIgv.cs:16` — `dr`
- [ ] `MysqlNotaIngresoIgv.cs:18` — `adap`
- [ ] `MysqlNotaIngresoIgv.cs:20` — `tabla`
- [ ] `MysqlOrdenCompra.cs:23` — `uno`
- [ ] `MysqlPlanContable.cs:15` — `dr`
- [ ] `MysqlPuntoLlegada.cs:16` — `dr`
- [ ] `MysqlPuntoLlegada.cs:20` — `tra`
- [ ] `MysqlRepositorio.cs:19` — `adap`
- [ ] `MysqlStatusCajaChica.cs:18` — `adap`
- [ ] `MysqlStatusCajaChica.cs:20` — `tabla`
- [ ] `MysqlUnidadEquivalente.cs:15` — `dr`

### SIGEFA.Entidades
- [ ] `clsLocalidad.cs:14` — `dr`

### SIGEFA.Reportes.clsReportes
- [ ] `clsDocumentosImpresos.cs:14` — `dr`
- [ ] `clsDocumentosImpresos.cs:18` — `tabla`
- [ ] `clsGuia.cs:13` — `dr`
- [ ] `clsNotasCreditoDebitoCompra.cs:13` — `dr`
- [ ] `clsNotasCreditoDebitoVenta.cs:13` — `dr`
- [ ] `clsReportAlmacenVendeMas.cs:14` — `dr`
- [ ] `clsReporteCaja.cs:14` — `dr`
- [ ] `clsReporteArqueo.cs:13` — `dr`
- [ ] `clsReporteArqueoCargado.cs:13` — `dr`
- [ ] `clsReporteCodigoBarras.cs:13` — `dr`
- [ ] `clsReporteCodigoBarras.cs:17` — `tabla`
- [ ] `clsReporteCobrosDia.cs:14` — `dr`
- [ ] `clsReporteCotizacion.cs:13` — `dr`
- [ ] `clsReporteCuotas.cs:13` — `dr`
- [ ] `clsReporteDespacho.cs:14` — `dr`
- [ ] `clsReporteDocumentosEnviados.cs:14` — `dr`
- [ ] `clsReporteFactura.cs:13` — `dr`
- [ ] `clsReporteGananciaxArticulo.cs:14` — `dr`
- [ ] `clsReporteGananciaxCliente.cs:14` — `dr`
- [ ] `clsReporteHistoriaClinica.cs:13` — `dr`
- [ ] `clsReporteInformeSucursal.cs:14` — `dr`
- [ ] `clsReporteKardex.cs:14` — `dr`
- [ ] `clsReporteMercaderiaxEntregar.cs:14` — `dr` (nota: el archivo real es `clsReportMercaderiaxEntregar.cs`)
- [ ] `clsReporteNotaCredito.cs:14` — `dr`
- [ ] `clsReporteOrdenCompra.cs:13` — `dr`
- [ ] `clsReportePagosDia.cs:14` — `dr`
- [ ] `clsReportePagos.cs:14` — `dr`
- [ ] `clsReportePedidos.cs:14` — `dr`
- [ ] `clsReportePropuestaDePedido.cs:14` — `dr`
- [ ] `clsReporteRankingxArticulo.cs:14` — `dr`
- [ ] `clsReporteRankingxCliente.cs:14` — `dr`
- [ ] `clsReporteTransferencia.cs:14` — `dr`
- [ ] `clsReporteVentaMesArticulo.cs:13` — `dr`
- [ ] `clsReporteVentasxVendedor.cs:14` — `dr`
- [ ] `clsReporteVentaxArticulo.cs:14` — `dr`
- [ ] `clsReporteVentCredContDia.cs:14` — `dr`
- [ ] `clsReporteVentas.cs:14` — `dr`
- [ ] `clsReporteVentas.cs:20` — `set`
- [ ] `clsReporteVentSeparacion.cs:14` — `dr`
- [ ] `clsReporteVentxCliente.cs:14` — `dr`
- [ ] `clsRotacionProductos.cs:14` — `dr`
- [ ] `clsReportProductos.cs:14` — `dr`
- [ ] `clsReportProductos.cs:18` — `tabla`

### CDSSoftware
- [ ] `ImprimeTexto.cs:9` — `GENERIC_WRITE`
- [ ] `ImprimeTexto.cs:11` — `OPEN_EXISTING`
- [ ] `ImprimeTexto.cs:13` — `FILE_SHARE_WRITE`

---

## CS0649 — Campo nunca asignado (siempre valor por defecto)

**Qué hacer:** Comentar la declaración o asignar un valor por defecto explícito con comentario `// CS0649: siempre [valor por defecto]`.

- [ ] `CDSSoftware/ImprimeTexto.cs:17` — `hPort` (siempre `0`)
- [ ] `CDSSoftware/ImprimeTexto.cs:23` — `hPortP` (siempre `0`)
- [ ] `SIGEFA.Formularios/frmCancelarCobroMultiple.cs:84` — `tipopago` (siempre `false`)
- [ ] `SIGEFA.Formularios/frmNotaIngreso.cs:129` — `total` (siempre `0`)
- [ ] `SIGEFA.Formularios/frmNotaIngreso.cs:131` — `montoflete` (siempre `0`)
- [ ] `SIGEFA.Formularios/frmNotaIngreso.cs:293` — `flete` (siempre `null`)
- [ ] `SIGEFA.Formularios/frmProductosNoAtendidosGRC.cs:41` — `estadoPropuesta` (siempre `0`)
- [ ] `SIGEFA.Formularios/FormularioParaPruebas.cs:88` — `codPlantilla` (siempre `0`)
- [ ] `SIGEFA.InterMySql/MysqlOrdenCompra.cs:23` — `uno` (siempre `0`)
- [ ] `SIGEFA.Librerias/EsSalud.cs:22` — `state` (siempre valor por defecto del enum)
