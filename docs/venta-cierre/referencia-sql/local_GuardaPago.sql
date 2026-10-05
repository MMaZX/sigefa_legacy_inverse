*************************** 1. row ***************************
GuardaPago
STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION
CREATE DEFINER=`root`@`%` PROCEDURE `GuardaPago`(codnot int(11),codlet int(11), codcuopreban INT(11), codtipopago int(11),codmon int(11),codtar int(11),tipo bit,ingegre bit,tipocambio decimal(10,3), montopa decimal(10,4), montoco decimal(10,4), vuelto decimal(10,4), mora Decimal(10,4), codalma int(11),codcta int(11), numcta varchar(20),noperacion varchar(30),ncheque varchar(20),fecha datetime,observa varchar(250),codusu INT(11),codban int(11),codserie int(11),serie varchar(4),numdoc varchar(30), aprob int(1), ref varchar(30), coddoc int(11), provi bit, codsucur int(11), codCaja_ex int(11), notacre INT(11), codnotac int(11),ctdadDetRet decimal(10,4),tipoDetRet varchar(4),montoEnCuenta decimal(10,4),opcionSuma int(11),tipo_descripcion int(11), OUT  newid  int(11))
BEGIN



DECLARE sal DECIMAL(15,3);

DECLARE tc_venta DECIMAL(10,3);

DECLARE tc_compra, varPendientePres DECIMAL(10,3);

DECLARE varPreBan INT; 

declare codCajaNueva int(11); 

DECLARE codPagoPendiente INT(11);



SELECT IFNULL(SUM(ccm.ingreso),0) - IFNULL(SUM(ccm.egreso),0) INTO sal FROM ctactemovimientos ccm WHERE ccm.codCuentaCorriente = codcta and ccm.estado = 1 LIMIT 1;

SELECT tc.compra INTO tc_compra FROM tipocambio tc WHERE tc.fecha=date(fecha);

SELECT tc.venta INTO tc_venta FROM tipocambio tc WHERE tc.fecha=date(fecha);

   
    
    SET codPagoPendiente= (SELECT IFNULL(p.codPago,0) as codPago  FROM pago p WHERE p.codNota = codnot and p.codTipoPago = 12  limit 1 );

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

       INSERT INTO ctactemovimientos

         (codCuentaCorriente, codDocumento, codAlmacen, documento, NumTransaccion, descripcion, moneda, tcventa,tccompra,egreso,

          saldo,codUser,fechaMovimiento,fechaRegistro, codPago, codBanco, codSucursal, tipo, estado_conciliacion)

       VALUES(codcta, (SELECT f.codTipoDocumento FROM facturacion f WHERE f.codFactura = codnot LIMIT 1 ), codalma, 

              (SELECT DocumentoFactura FROM facturacion f WHERE f.codFactura = codnot), noperacion, 

               (SELECT mp.descripcion FROM metodopago mp WHERE mp.codMetodoPago=codtipopago), 

              codmon, tc_venta, tc_compra, montopa, (sal-montopa), codusu, fecha, NOW(), newid, codban, codsucur, 2, 1);

   

   ELSEIF (ingegre = 1 AND aprob=4) THEN 

       INSERT INTO ctactemovimientos

         (codCuentaCorriente, codDocumento, codAlmacen, documento, NumTransaccion, descripcion, moneda, tcventa,tccompra,ingreso,

          saldo,codUser,fechaMovimiento,fechaRegistro, codPago, codBanco, codSucursal, tipo, estado_conciliacion)

       VALUES(codcta,(SELECT f.codTipoDocumento FROM factura_venta f WHERE f.codFacturaV = codnot  LIMIT 1), codalma, 

              (SELECT CONCAT(f.serie,'-',f.numDocumento) FROM factura_venta f WHERE f.codFacturaV = codnot ), noperacion, 

              (SELECT mp.descripcion FROM metodopago mp WHERE mp.codMetodoPago=codtipopago), 

              codmon, tc_venta, tc_compra, montoco, (sal+montoco), codusu, fecha, NOW(), newid, codban, codsucur, 2,1);

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
utf8mb4
utf8mb4_general_ci
utf8mb4_unicode_ci
