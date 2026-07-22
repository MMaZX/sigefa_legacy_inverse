# Flujo de Pagos Pendientes y Movimientos de Caja: Fases de Ejecución del Código

Este documento detalla el funcionamiento lógico y las fases de ejecución del código de base de datos del sistema **SIGEFA** para el control de **Pagos Pendientes** (método de pago `12: PENDIENTE`) y su interacción con los movimientos de caja, obtenido directamente de los procedimientos almacenados en el servidor MySQL.

---

## 1. Código Fuente de los Procedimientos Almacenados (MySQL)

A continuación se presenta el código exacto recuperado directamente desde el servidor de base de datos para los procesos de anulación e inserción/amortización.

### A. Procedimiento `AnularPagoPendiente`
```sql
CREATE PROCEDURE `AnularPagoPendiente`(codpag INT(11))
BEGIN
	DECLARE codigoNota INT(11);
	DECLARE codPagoPendiente INT(11);
	DECLARE montoAnulado  DECIMAL (10,2);
	DECLARE codigoCaja INT(11);

	UPDATE pago SET estado = 0 where codPago = codpag;
	 
	SET montoAnulado =(select p1.montocobrado from pago p1 where p1.codPago = codpag);
 
	SET codigoNota =(SELECT p.codNota FROM pago p WHERE p.codPago = codpag );
	
	SET codPagoPendiente= (SELECT IFNULL(p.codPago,0) as codPago FROM pago p WHERE p.codNota = codigoNota and p.codTipoPago = 12 limit 1 );

	IF (codPagoPendiente !=0 ) THEN
		UPDATE pago pag SET pag.estado =1, pag.montocobrado = (pag.montocobrado + montoAnulado ), pag.montopagado =(pag.montopagado + montoAnulado)
		WHERE pag.codPago = codPagoPendiente;

		UPDATE cajamovimiento cm SET cm.estado =1,cm.monto =(cm.monto + montoAnulado) WHERE cm.codPago = codPagoPendiente and cm.codTipoPagoCaja = 12;
			
		SET codigoCaja =(SELECT cm1.codcaja FROM cajamovimiento cm1 WHERE cm1.codPago = codPagoPendiente and cm1.codTipoPagoCaja = 12);
			
		UPDATE caja ca
		SET ca.totalpendiente= (ca.totalpendiente + montoAnulado)
		WHERE ca.codcaja = codigoCaja;
	ELSE
		UPDATE pago pag SET pag.estado =1 , pag.codTipoPago =12
		WHERE pag.codPago = codpag;

		UPDATE cajamovimiento cm SET cm.estado =1 , cm.codTipoPagoCaja = 12 WHERE cm.codPago = codpag ;
			
		SET codigoCaja =(SELECT cm1.codcaja FROM cajamovimiento cm1 WHERE cm1.codPago = codpag );
			
		UPDATE caja ca
		SET ca.totalpendiente= (ca.totalpendiente + montoAnulado)
		WHERE ca.codcaja = codigoCaja;
	END IF;
END
```

### B. Procedimiento `GuardaPagoPendiente`
```sql
CREATE PROCEDURE `GuardaPagoPendiente`(codnot int(11),codtipopago int(11), montopa decimal(10,4), montoco decimal(10,4))
BEGIN
	DECLARE codigo INT(11);
	DECLARE montoPago decimal(10,2);

	SET codigo=(SELECT IFNULL(p.codPago,0) as codPago FROM pago p WHERE p.codNota = codnot and p.codTipoPago = 12 and p.estado = 1 limit 1 );

	IF (codigo != 0 and codtipopago != 12) THEN
		UPDATE pago pa SET pa.montocobrado = (pa.montocobrado - montoco), pa.montopagado = (pa.montopagado - montopa)
		WHERE pa.codNota = codnot and pa.codTipoPago = 12;
	END IF;

	SET montoPago = (select IFNULL(p1.montocobrado,0) as montocobrado FROM pago p1 WHERE p1.codNota = codnot and p1.codTipoPago = 12 limit 1 );

	IF (montoPago = 0 or  montoPago < 1) THEN
		UPDATE pago pag SET pag.estado =0 WHERE pag.codNota = codnot and pag.codTipoPago = 12;

		UPDATE cajamovimiento cm SET cm.estado =0 WHERE cm.codPago = codigo and cm.codTipoPagoCaja = 12;
	END IF;
END
```

### C. Procedimiento `GuardaPago` (Inserción del Cobro Realizado)
```sql
CREATE PROCEDURE `GuardaPago`(codnot int(11),codlet int(11), codcuopreban INT(11), codtipopago int(11),codmon int(11),codtar int(11),tipo bit,ingegre bit,tipocambio decimal(10,3), montopa decimal(10,4), montoco decimal(10,4), vuelto decimal(10,4), mora Decimal(10,4), codalma int(11),codcta int(11), numcta varchar(20),noperacion varchar(30),ncheque varchar(20),fecha datetime,observa varchar(250),codusu INT(11),codban int(11),codserie int(11),serie varchar(4),numdoc varchar(30), aprob int(1), ref varchar(30), coddoc int(11), provi bit, codsucur int(11), codCaja_ex int(11), notacre INT(11), codnotac int(11),ctdadDetRet decimal(10,4),tipoDetRet varchar(4),montoEnCuenta decimal(10,4),opcionSuma int(11),tipo_descripcion int(11), OUT newid int(11))
BEGIN
	DECLARE sal DECIMAL(15,3);
	DECLARE tc_venta DECIMAL(10,3);
	DECLARE tc_compra, varPendientePres DECIMAL(10,3);
	DECLARE varPreBan INT; 
	DECLARE codCajaNueva int(11); 
	DECLARE codPagoPendiente INT(11);

	SELECT IFNULL(SUM(ccm.ingreso),0) - IFNULL(SUM(ccm.egreso),0) INTO sal FROM ctactemovimientos ccm WHERE ccm.codCuentaCorriente = codcta and ccm.estado = 1 LIMIT 1;
	SELECT tc.compra INTO tc_compra FROM tipocambio tc WHERE tc.fecha=date(fecha);
	SELECT tc.venta INTO tc_venta FROM tipocambio tc WHERE tc.fecha=date(fecha);
   
	SET codPagoPendiente= (SELECT IFNULL(p.codPago,0) as codPago FROM pago p WHERE p.codNota = codnot and p.codTipoPago = 12 limit 1 );

	IF (codPagoPendiente != 0) THEN
		SET codCajaNueva = (SELECT p.codCaja FROM pago p WHERE p.codPago = codPagoPendiente );
	ELSE
		SET codCajaNueva = (codCaja_ex);
	END IF;

	INSERT INTO pago (codNota,codLetra,codCuotaPreBan,codTipoPago,codMoneda,codTarjetasPago,tipo,ingresoegreso,tipocambio,
	montopagado,montocobrado,vuelto,mora,codAlmacen,codctacte,numctacte,noperacion,ncheque,fechapago,
	observacion,codUser,codBanco,fecharegistro, codSerie, serie, numdocumento, Aprobado, referencia, codTipoDocumento,provision,codCaja, notacredito, codNotaCredito,ctdadDetRet,tipoDetRet,montoEnCuenta,opcionSuma, tipo_descripcion_ingreso)
	VALUES (codnot,codlet,codcuopreban, codtipopago,codmon,codtar,tipo,ingegre,tipocambio,montopa,montoco,
	vuelto,mora,codalma,codcta,numcta,noperacion,ncheque,fecha,observa,codusu,codban,NOW(),codserie,serie,numdoc, aprob, ref, coddoc,provi, codCajaNueva, notacre, codnotac,ctdadDetRet,tipoDetRet,montoEnCuenta,opcionSuma, tipo_descripcion);
	SET newid = LAST_INSERT_ID();

	IF(codtipopago=6 or codtipopago=7 or codtipopago=8 or codtipopago=9) THEN
		IF (ingegre = 0) THEN
			INSERT INTO ctactemovimientos (codCuentaCorriente, codDocumento, codAlmacen, documento, NumTransaccion, descripcion, moneda, tcventa,tccompra,egreso, saldo,codUser,fechaMovimiento,fechaRegistro, codPago, codBanco, codSucursal, tipo, estado_conciliacion)
			VALUES(codcta, (SELECT f.codTipoDocumento FROM facturacion f WHERE f.codFactura = codnot LIMIT 1 ), codalma, (SELECT DocumentoFactura FROM facturacion f WHERE f.codFactura = codnot), noperacion, (SELECT mp.descripcion FROM metodopago mp WHERE mp.codMetodoPago=codtipopago), codmon, tc_venta, tc_compra, montopa, (sal-montopa), codusu, fecha, NOW(), newid, codban, codsucur, 2, 1);
		ELSEIF (ingegre = 1 AND aprob=4) THEN 
			INSERT INTO ctactemovimientos (codCuentaCorriente, codDocumento, codAlmacen, documento, NumTransaccion, descripcion, moneda, tcventa,tccompra,ingreso, saldo,codUser,fechaMovimiento,fechaRegistro, codPago, codBanco, codSucursal, tipo, estado_conciliacion)
			VALUES(codcta,(SELECT f.codTipoDocumento FROM factura_venta f WHERE f.codFacturaV = codnot LIMIT 1), codalma, (SELECT CONCAT(f.serie,'-',f.numDocumento) FROM factura_venta f WHERE f.codFacturaV = codnot ), noperacion, (SELECT mp.descripcion FROM metodopago mp WHERE mp.codMetodoPago=codtipopago), codmon, tc_venta, tc_compra, montoco, (sal+montoco), codusu, fecha, NOW(), newid, codban, codsucur, 2, 1);
		END IF;
	END IF;

	IF (codcuopreban>0) THEN
		SELECT codPrestamoBancario INTO varPreBan FROM cuotaprestamo WHERE codCuotaPrestamo=codcuopreban;
		UPDATE prestamobancario SET pendiente=pendiente-montoco, montomora=montomora+mora WHERE codPrestamoBancario=varPreBan;
		SELECT pendiente INTO varPendientePres FROM prestamobancario WHERE codPrestamoBancario=varPreBan LIMIT 1;
		IF(varPendientePres=0) THEN
			UPDATE prestamobancario SET cancelado=1, fechacancelado=DATE(NOW()) WHERE codPrestamoBancario=varPreBan;
		END IF;
	END IF;
END
```

---

## 2. Fases de Ejecución: Reversión de Cobros ("Pasar a Pendiente" - `AnularPagoPendiente`)

Cuando un usuario autorizado pulsa **"Pasar a Pendiente"** sobre un movimiento de caja, se ejecuta el procedimiento `AnularPagoPendiente(codpag)` en las siguientes fases lógicas exactas:

*   **FASE 1: Inicialización y Declaración de Ámbito**
    Se declaran las variables locales (`codigoNota`, `codPagoPendiente`, `montoAnulado`, `codigoCaja`) necesarias para procesar la transacción y guardar estados temporales.

*   **FASE 2: Desactivación Inmediata del Pago Erróneo**
    Se realiza un `UPDATE` en la tabla `pago` para colocar `estado = 0` para el `codPago` recibido (`codpag`), marcando este registro de cobro específico como anulado.
    ```sql
    UPDATE pago SET estado = 0 where codPago = codpag;
    ```

*   **FASE 3: Recuperación de Datos del Pago Anulado**
    *   Se extrae el monto que fue cobrado en el pago anulado y se almacena en `montoAnulado`.
        ```sql
        SET montoAnulado =(select p1.montocobrado from pago p1 where p1.codPago = codpag);
        ```
    *   Se obtiene el identificador del documento de venta (`codNota`) correspondiente y se guarda en `codigoNota`.
        ```sql
        SET codigoNota =(SELECT p.codNota FROM pago p WHERE p.codPago = codpag );
        ```

*   **FASE 4: Búsqueda de un Saldo Pendiente Preexistente**
    Se consulta si existe algún registro de cobro de tipo pendiente (`codTipoPago = 12`) asignado a la misma nota/documento.
    ```sql
    SET codPagoPendiente= (SELECT IFNULL(p.codPago,0) as codPago FROM pago p WHERE p.codNota = codigoNota and p.codTipoPago = 12 limit 1 );
    ```

*   **FASE 5: Bifurcación de la Lógica según Existencia de Deuda Pendiente**

    *   **CASO A: Si el documento ya poseía un registro de Pago Pendiente activo o inactivo (`codPagoPendiente != 0`)**
        *   **Fase 5.A.1 (Reactivar y acumular deuda):** Se reactiva el pago pendiente (`estado = 1`) e incrementa sus campos `montocobrado` y `montopagado` sumándoles el `montoAnulado`.
            ```sql
            UPDATE pago pag SET pag.estado = 1, pag.montocobrado = (pag.montocobrado + montoAnulado ), pag.montopagado =(pag.montopagado + montoAnulado) WHERE pag.codPago = codPagoPendiente;
            ```
        *   **Fase 5.A.2 (Reactivar movimiento de caja pendiente):** Se reactiva el movimiento de caja de tipo pendiente (`estado = 1`) y se le adiciona el monto anulado.
            ```sql
            UPDATE cajamovimiento cm SET cm.estado = 1, cm.monto =(cm.monto + montoAnulado) WHERE cm.codPago = codPagoPendiente and cm.codTipoPagoCaja = 12;
            ```
        *   **Fase 5.A.3 (Determinar Caja Relacionada):** Se obtiene el `codcaja` a partir de la tabla `cajamovimiento` del pago pendiente.
            ```sql
            SET codigoCaja =(SELECT cm1.codcaja FROM cajamovimiento cm1 WHERE cm1.codPago = codPagoPendiente and cm1.codTipoPagoCaja = 12);
            ```
        *   **Fase 5.A.4 (Restaurar Saldo de Caja):** Se incrementa el saldo global pendiente de la caja seleccionada.
            ```sql
            UPDATE caja ca SET ca.totalpendiente = (ca.totalpendiente + montoAnulado) WHERE ca.codcaja = codigoCaja;
            ```

    *   **CASO B: Si el documento NO tenía un registro de Pago Pendiente preexistente (`codPagoPendiente = 0`)**
        *   **Fase 5.B.1 (Transformar el pago actual en Pendiente):** Se reutiliza la fila del pago anulado, reactivándola (`estado = 1`) y cambiando su tipo a `12` (`PENDIENTE`).
            ```sql
            UPDATE pago pag SET pag.estado = 1, pag.codTipoPago = 12 WHERE pag.codPago = codpag;
            ```
        *   **Fase 5.B.2 (Transformar movimiento de caja en Pendiente):** Se reactiva su movimiento de caja correspondiente en la tabla `cajamovimiento` (`estado = 1`) y se modifica su tipo a `12`.
            ```sql
            UPDATE cajamovimiento cm SET cm.estado = 1, cm.codTipoPagoCaja = 12 WHERE cm.codPago = codpag;
            ```
        *   **Fase 5.B.3 (Determinar Caja Relacionada):** Se obtiene el identificador de la caja (`codcaja`) a través de la fila de movimiento de caja asociada al cobro anulado.
            ```sql
            SET codigoCaja =(SELECT cm1.codcaja FROM cajamovimiento cm1 WHERE cm1.codPago = codpag );
            ```
        *   **Fase 5.B.4 (Restaurar Saldo de Caja):** Se incrementa el balance pendiente global de la caja física.
            ```sql
            UPDATE caja ca SET ca.totalpendiente = (ca.totalpendiente + montoAnulado) WHERE ca.codcaja = codigoCaja;
            ```

---

## 3. Fases de Ejecución: Registro y Amortización (`GuardaPago` + `GuardaPagoPendiente`)

Cuando se realiza un cobro real (Efectivo, Tarjeta, Depósito) sobre un documento que posee saldo pendiente, el backend ejecuta secuencialmente dos fases principales en la base de datos:

### Parte A: Inserción del Nuevo Cobro Real (`GuardaPago`)
*   **FASE 1: Consulta de Estados y Tipo de Cambio**
    *   Calcula el saldo del cliente en la cuenta corriente (`sal`).
    *   Obtiene los tipos de cambio de compra y venta vigentes a la fecha del cobro.
*   **FASE 2: Heredar Caja del Pago Pendiente**
    Para asegurar que el nuevo cobro se registre en la misma caja física donde se aperturó la deuda, busca la deuda de tipo pendiente (`12`):
    *   Si se encuentra la deuda pendiente, el nuevo pago hereda su `codCaja`.
    *   Si no existía deuda pendiente, utiliza el parámetro `codCaja_ex` provisto por el formulario.
*   **FASE 3: Inserción en Tabla `pago`**
    Inserta la fila del nuevo pago con sus detalles y recupera su ID (`LAST_INSERT_ID()`).
*   **FASE 4: Actualización de Cuenta Corriente y Préstamos**
    *   Si el método de pago es bancario (`6, 7, 8, 9`), se genera el movimiento en `ctactemovimientos`.
    *   Si el pago cancela cuotas de préstamos (`codcuopreban > 0`), se actualiza y reduce la deuda en `prestamobancario`.

---

### Parte B: Amortización de la Deuda Pendiente (`GuardaPagoPendiente`)
Inmediatamente después de insertar el cobro real, se invoca `GuardaPagoPendiente` para recalcular y amortizar el saldo pendiente del documento:

*   **FASE 1: Identificación del Saldo Pendiente Activo**
    Busca si existe un registro de pago pendiente (`codTipoPago = 12`) activo (`estado = 1`) para el documento de venta.
    ```sql
    SET codigo=(SELECT IFNULL(p.codPago,0) as codPago FROM pago p WHERE p.codNota = codnot and p.codTipoPago = 12 and p.estado = 1 limit 1 );
    ```

*   **FASE 2: Deducción / Amortización del Saldo**
    Si se encuentra dicho registro (`codigo != 0`) y el nuevo pago insertado no es de tipo pendiente (`codtipopago != 12`), se resta el monto cobrado y pagado del saldo pendiente restante.
    ```sql
    UPDATE pago pa SET pa.montocobrado = (pa.montocobrado - montoco), pa.montopagado = (pa.montopagado - montopa)
    WHERE pa.codNota = codnot and pa.codTipoPago = 12;
    ```

*   **FASE 3: Lectura del Saldo Actualizado**
    Se consulta el saldo deudor restante una vez efectuada la deducción.
    ```sql
    SET montoPago = (select IFNULL(p1.montocobrado,0) as montocobrado FROM pago p1 WHERE p1.codNota = codnot and p1.codTipoPago = 12 limit 1 );
    ```

*   **FASE 4: Liquidación / Cierre del Pago Pendiente**
    Si el saldo pendiente (`montoPago`) llega a ser menor a `1` (o exactamente `0`), se procede a dar por cancelada la deuda:
    *   Se inactiva la fila de deuda pendiente (`estado = 0`) en la tabla `pago`.
        ```sql
        UPDATE pago pag SET pag.estado = 0 WHERE pag.codNota = codnot and pag.codTipoPago = 12;
        ```
    *   Se inactiva el movimiento de caja de tipo pendiente (`estado = 0`) en la tabla `cajamovimiento`.
        ```sql
        UPDATE cajamovimiento cm SET cm.estado = 0 WHERE cm.codPago = codigo and cm.codTipoPagoCaja = 12;
        ```
