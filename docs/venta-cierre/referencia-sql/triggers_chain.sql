##### detallefactura_venta AFTER INSERT tr_insert_ventas_neta
BEGIN
                CALL sp_venta_neta_dia(NEW.codProducto, NEW.codAlmacen, DATE(NEW.fecharegistro));
            END

##### detallenotasalida AFTER INSERT ActualizaEstadoPedido
BEGIN



   DECLARE codigopedido int(11);

   DECLARE forpago int(11);



   SELECT codPedido INTO codigopedido FROM notasalida WHERE codNotaSalida=NEW.codNotaSalida; 

   SELECT formapago INTO forpago FROM notasalida WHERE codNotaSalida=NEW.codNotaSalida; 



   IF codigopedido <> 0 THEN



                 UPDATE pedidosventa

                 SET pendiente = 0, 

                 formapago = forpago

                 WHERE codPedido = codigopedido;



  END IF;



END

##### detallenotasalida AFTER INSERT ActualizaStockInsertS
BEGIN



DECLARE cod INTEGER DEFAULT 0;

DECLARE coment VARCHAR(120);

DECLARE stockpentr DECIMAL(10,4);

DECLARE ext   BIT DEFAULT 0;
DECLARE fac decimal(11,4);
DECLARE unim int(11);
DECLARE codmoneda int(11);
DECLARE tc DECIMAL (10,3);
DECLARE VtaSinStock int(11);


declare _codTipoDoc int(11);
declare _codSerie int(11);
declare _serie varchar(20);
declare _numeracion varchar(30);
declare _fechaRegistroDoc datetime;
declare _stockFinal DECIMAL(10,4);

SELECT ns.codTipoDocumento,ns.codSerie,ns.serie,ns.numdocumento,ns.fecharegistro INTO _codTipoDoc,_codSerie,_serie,_numeracion,_fechaRegistroDoc FROM  notasalida ns WHERE ns.codNotaSalida=NEW.codNotaSalida;

SELECT ventasinstock INTO VtaSinStock FROM notasalida WHERE  codNotaSalida = NEW.codNotaSalida;

SELECT consultorext INTO ext FROM notasalida WHERE codNotaSalida=NEW.codNotaSalida;

SELECT codTransaccion INTO cod  FROM notasalida WHERE codNotaSalida=NEW.codNotaSalida;

SELECT motivo INTO coment FROM notasalida WHERE codNotaSalida=NEW.codNotaSalida;

SELECT stockporentregar INTO stockpentr FROM productoalmacen WHERE codProducto=NEW.codProducto AND codAlmacen=NEW.codAlmacen;



SELECT Unidad INTO unim FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = NEW.codAlmacen;

SELECT  IFNULL(factor,0) INTO fac FROM  unidadequivalente eq WHERE  eq.codProducto =  NEW.codProducto AND eq.codUnidadMedida = NEW.unidadingresada AND eq.codUndEqui = unim and eq.compra_venta = 2;



SELECT moneda INTO codmoneda FROM notasalida WHERE codNotaSalida=NEW.codNotaSalida;

SET tc = (SELECT venta FROM tipocambio WHERE date(fecha) = date(NOW()) limit 1);



IF ext=1 AND VtaSinStock = 0    THEN



       IF EXISTS(SELECT * FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = NEW.codAlmacen) THEN

              UPDATE productoalmacen

              SET stockactual= stockactual  - (NEW.cantidad*fac),

                     soles = soles - valorpromediosoles * (NEW.cantidad*fac),

                     totalsalidas = totalsalidas + (NEW.cantidad*fac),

                     totalsolessalidas = totalsolessalidas + valorpromediosoles * (NEW.cantidad*fac)

             WHERE codProducto = NEW.codProducto AND codAlmacen = NEW.codAlmacen;

       END IF;

END IF;



IF ext=0  AND VtaSinStock = 0   THEN

     IF cod=19 THEN



              IF (coment='Anulacion') OR (coment ='Devolucion Mercaderia') THEN 

                       IF EXISTS(SELECT * FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = NEW.codAlmacen) THEN
															select pa1.stockdisponible into _stockFinal from productoalmacen pa1 where pa1.codProducto = NEW.codProducto  and pa1.codAlmacen=NEW.codAlmacen limit 1;
                              UPDATE productoalmacen

                              SET  stockactual= stockactual  - (NEW.cantidad*fac),

                                      stockdisponible = stockdisponible  - (NEW.cantidad*fac),

                                      soles = soles - valorpromediosoles *  (NEW.cantidad*fac),

                                      totalsalidas = totalsalidas + (NEW.cantidad*fac),

                                      totalsolessalidas = totalsolessalidas + valorpromediosoles * (NEW.cantidad*fac)

                              WHERE codProducto = NEW.codProducto AND codAlmacen = NEW.codAlmacen;
															
															INSERT INTO `movimientostock` (`fechaRegistro`, `codAlmacen`, `codTipoDocumento`, `codDetalleNotaSalida`, `codNotaSalida`, `codDetalleNotaIngreso`, `codNotaIngreso`, `codSerie`, `serie`, `numeracion`, `fechaRegistroDoc`, `codProducto`, `codUnidad`, `tipoStock`, `entrada`, `salida`, `stockFinal`) VALUES 
(NOW(), NEW.codAlmacen, _codTipoDoc, NEW.codDetalleSalida, NEW.codNotaSalida, NULL, NULL, _codSerie, _serie, _numeracion, _fechaRegistroDoc, NEW.codProducto, unim, 1,NULL, (NEW.cantidad*fac), (IFNULL(_stockFinal,0) - (NEW.cantidad*fac)));
															
                       END IF;

              END IF;       

         

     ELSEIF cod<>15 THEN

              

                     IF NEW.codDetallePedido = 0 AND NEW.codDetalleSeparacion = 0 THEN                              

                                   IF EXISTS(SELECT * FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = NEW.codAlmacen) THEN


select pa1.stockdisponible into _stockFinal from productoalmacen pa1 where pa1.codProducto = NEW.codProducto  and pa1.codAlmacen=NEW.codAlmacen limit 1;
	    

                                                 UPDATE productoalmacen

                                                 SET stockactual= stockactual  - (NEW.cantidad*fac),

                                                 stockdisponible = stockdisponible  - (NEW.cantidad*fac),

                                                 soles = soles - (valorpromediosoles * (NEW.cantidad*fac)),

                                                 totalsalidas = totalsalidas + (NEW.cantidad*fac),

                                                 totalsolessalidas = totalsolessalidas + (valorpromediosoles * (NEW.cantidad*fac))

                                                 WHERE codProducto = NEW.codProducto AND codAlmacen = NEW.codAlmacen;


INSERT INTO `movimientostock` (`fechaRegistro`, `codAlmacen`, `codTipoDocumento`, `codDetalleNotaSalida`, `codNotaSalida`, `codDetalleNotaIngreso`, `codNotaIngreso`, `codSerie`, `serie`, `numeracion`, `fechaRegistroDoc`, `codProducto`, `codUnidad`, `tipoStock`, `entrada`, `salida`, `stockFinal`) VALUES 
(NOW(), NEW.codAlmacen, _codTipoDoc, NEW.codDetalleSalida, NEW.codNotaSalida, NULL, NULL, _codSerie, _serie, _numeracion, _fechaRegistroDoc, NEW.codProducto, unim, 1,NULL, (NEW.cantidad*fac), (IFNULL(_stockFinal,0) - (NEW.cantidad*fac)));
	           



                                   END IF; 

                    ELSEIF NEW.codDetallePedido <> 0   THEN       

                                   IF EXISTS(SELECT * FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = NEW.codAlmacen) THEN                 

select pa1.stockdisponible into _stockFinal from productoalmacen pa1 where pa1.codProducto = NEW.codProducto  and pa1.codAlmacen=NEW.codAlmacen limit 1;

                                                 UPDATE productoalmacen

                                                 SET stockactual= stockactual  - (NEW.cantidad*fac),    

                                                 stockdisponible = stockdisponible  - (NEW.cantidad*fac),                              

                                                 soles = soles - valorpromediosoles * (NEW.cantidad*fac),

                                                 totalsalidas = totalsalidas + (NEW.cantidad*fac),

                                                 totalsolessalidas = totalsolessalidas + valorpromediosoles * (NEW.cantidad*fac)

                                                 WHERE codProducto = NEW.codProducto AND codAlmacen = NEW.codAlmacen;  
																								 
																								 INSERT INTO `movimientostock` (`fechaRegistro`, `codAlmacen`, `codTipoDocumento`, `codDetalleNotaSalida`, `codNotaSalida`, `codDetalleNotaIngreso`, `codNotaIngreso`, `codSerie`, `serie`, `numeracion`, `fechaRegistroDoc`, `codProducto`, `codUnidad`, `tipoStock`, `entrada`, `salida`, `stockFinal`) VALUES 
(NOW(), NEW.codAlmacen, _codTipoDoc, NEW.codDetalleSalida, NEW.codNotaSalida, NULL, NULL, _codSerie, _serie, _numeracion, _fechaRegistroDoc, NEW.codProducto, unim, 1,NULL, (NEW.cantidad*fac), (IFNULL(_stockFinal,0) - (NEW.cantidad*fac)));
	           

                                   END IF;   

                    ELSEIF       NEW.codDetalleSeparacion <> 0  THEN 


                                   IF EXISTS(SELECT * FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = NEW.codAlmacen) THEN

select pa1.stockdisponible into _stockFinal from productoalmacen pa1 where pa1.codProducto = NEW.codProducto  and pa1.codAlmacen=NEW.codAlmacen limit 1;

                                                 UPDATE productoalmacen

                                                 SET stockactual= stockactual  - (NEW.cantidad*fac),

                                                 stockdisponible = stockdisponible  - (NEW.cantidad*fac),

                                                 soles = soles - valorpromediosoles * (NEW.cantidad*fac),

                                                 totalsalidas = totalsalidas + (NEW.cantidad*fac),

                                                 totalsolessalidas = totalsolessalidas + valorpromediosoles * (NEW.cantidad*fac)

                                                 WHERE codProducto = NEW.codProducto AND codAlmacen = NEW.codAlmacen;

INSERT INTO `movimientostock` (`fechaRegistro`, `codAlmacen`, `codTipoDocumento`, `codDetalleNotaSalida`, `codNotaSalida`, `codDetalleNotaIngreso`, `codNotaIngreso`, `codSerie`, `serie`, `numeracion`, `fechaRegistroDoc`, `codProducto`, `codUnidad`, `tipoStock`, `entrada`, `salida`, `stockFinal`) VALUES 
(NOW(), NEW.codAlmacen, _codTipoDoc, NEW.codDetalleSalida, NEW.codNotaSalida, NULL, NULL, _codSerie, _serie, _numeracion, _fechaRegistroDoc, NEW.codProducto, unim, 1,NULL, (NEW.cantidad*fac), (IFNULL(_stockFinal,0) - (NEW.cantidad*fac)));

                                   END IF; 

                     END IF;          



     END IF;

      

     IF cod = 7 THEN 

                  IF stockpentr != 0 THEN

                      UPDATE productoalmacen

                      SET stockporentregar = stockporentregar - NEW.cantidad

                      WHERE codProducto = NEW.codProducto AND codAlmacen = NEW.codAlmacen;                 

                  END IF;

     END IF;

END IF;





END

##### detallenotasalida AFTER INSERT ActualizaStockdisponibleTransferencia
BEGIN

DECLARE cod INTEGER DEFAULT 0;
DECLARE fac decimal(11,4); 
DECLARE unim int(11);
DECLARE u DECIMAL (10,4);


declare _codTipoDoc int(11);
declare _codSerie int(11);
declare _serie varchar(20);
declare _numeracion varchar(30);
declare _fechaRegistroDoc datetime;
declare _stockFinal DECIMAL(10,4);

SELECT codTransaccion INTO cod  FROM  notasalida WHERE codNotaSalida=NEW.codNotaSalida;

SELECT Unidad INTO unim FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = NEW.codAlmacen;

SELECT  IFNULL(factor,0) INTO fac FROM  unidadequivalente eq WHERE  eq.codProducto =  NEW.codProducto AND eq.codUnidadMedida = NEW.unidadingresada AND eq.codUndEqui = unim and eq.compra_venta = 2;



IF cod = 15 

 THEN

      IF EXISTS(SELECT * FROM productoalmacen pp WHERE pp.codProducto = NEW.codProducto and pp.codAlmacen=New.codAlmacen) THEN
				SELECT ns.codTipoDocumento,ns.codSerie,ns.serie,ns.numdocumento,ns.fecharegistro INTO _codTipoDoc,_codSerie,_serie,_numeracion,_fechaRegistroDoc FROM  notasalida ns WHERE ns.codNotaSalida=NEW.codNotaSalida;
						UPDATE productoalmacen SET  
									stockdisponible = stockdisponible - (NEW.cantidad*fac),
									stockactual = stockactual - (NEW.cantidad * fac),
									soles = soles - (NEW.valoreal * (NEW.cantidad*fac)),
									totalsalidas = totalsalidas + (NEW.cantidad*fac),
									totalsolessalidas =totalsolessalidas + (NEW.valoreal * (NEW.cantidad * fac))
								WHERE codProducto = NEW.codProducto  and codAlmacen=NEW.codAlmacen ;
						select pa1.stockdisponible into _stockFinal from productoalmacen pa1 where pa1.codProducto = NEW.codProducto  and pa1.codAlmacen=NEW.codAlmacen limit 1;		
								INSERT INTO `movimientostock` (`fechaRegistro`, `codAlmacen`, `codTipoDocumento`, `codDetalleNotaSalida`, `codNotaSalida`, `codDetalleNotaIngreso`, `codNotaIngreso`, `codSerie`, `serie`, `numeracion`, `fechaRegistroDoc`, `codProducto`, `codUnidad`, `tipoStock`, `entrada`, `salida`, `stockFinal`) VALUES 
(NOW(), NEW.codAlmacen, _codTipoDoc, NEW.codDetalleSalida, NEW.codNotaSalida, NULL, NULL, _codSerie, _serie, _numeracion, _fechaRegistroDoc, NEW.codProducto, unim, 1,NULL, (NEW.cantidad*fac), (IFNULL(_stockFinal,0)));
		END IF;

END IF;



END

##### detallenotasalida AFTER INSERT ModificacionStockDisponibleSegunReqAlmacenParaVenta
BEGIN

DECLARE cod INTEGER DEFAULT 0;
DECLARE codigopedido int(11);
DECLARE codAlmacen INTEGER DEFAULT 0; 
DECLARE fac decimal(11,4);
DECLARE unim int(11);
declare codReqAsoc int; 
declare cantidad DECIMAL(10,4) default 0;
declare codAlmacenNotaSalidaAlmSolic int;


declare _codTipoDoc int(11);
declare _codSerie int(11);
declare _serie varchar(20);
declare _numeracion varchar(30);
declare _fechaRegistroDoc datetime;
declare _stockFinal DECIMAL(10,4);

SELECT codTransaccion INTO cod  FROM  notasalida WHERE codNotaSalida=NEW.codNotaSalida;
IF cod = 7 THEN
	SELECT codPedido INTO codigopedido FROM notasalida WHERE codNotaSalida=NEW.codNotaSalida; 
	IF codigopedido <> 0 THEN
		select ns.codAlmacen into codAlmacenNotaSalidaAlmSolic from notasalida ns WHERE ns.codNotaSalida=NEW.codNotaSalida; 
IF EXISTS(select * from req_almacen ra where ra.codPedidoVenta = codigopedido and ra.estado != 12 and ra.tipo_req = 2 and ra.cod_almacen_solicitante = codAlmacenNotaSalidaAlmSolic) THEN
			
			select ra.id_req_almacen,ra.cod_tipo_documento,ra.cod_serie,ra.num_serie,ra.num_documento,ra.fecha_registro into codReqAsoc,_codTipoDoc,_codSerie,_serie,_numeracion,_fechaRegistroDoc from req_almacen ra where ra.codPedidoVenta = codigopedido and ra.estado != 12 and ra.tipo_req = 2 and ra.cod_almacen_solicitante = codAlmacenNotaSalidaAlmSolic limit 1;
			SELECT ra.cod_almacen_solicitante INTO codAlmacen FROM req_almacen ra WHERE ra.id_req_almacen=codReqAsoc;
			SELECT Unidad INTO unim FROM productoalmacen pal WHERE pal.codProducto = NEW.codProducto AND pal.codAlmacen = codAlmacen;
			SELECT  IFNULL(factor,0) INTO fac FROM  unidadequivalente eq WHERE  eq.codProducto =  NEW.codProducto AND eq.codUnidadMedida = NEW.unidadingresada AND eq.codUndEqui = unim and eq.compra_venta = 2;
			select dra.cantidad_confirmada into cantidad from detalle_req_almacen dra where dra.cod_producto = NEW.codProducto and dra.cod_unidad = NEW.unidadingresada and dra.id_req_almacen = codReqAsoc;
			select pa1.stockdisponible into _stockFinal from productoalmacen pa1 where pa1.codProducto = NEW.codProducto  and pa1.codAlmacen=codAlmacen limit 1;
			UPDATE productoalmacen pa
			SET  pa.stockdisponible = pa.stockdisponible + (cantidad*fac)
			
			WHERE pa.codProducto = NEW.codProducto  and pa.codAlmacen=codAlmacen ;
			
			INSERT INTO `movimientostock` (`fechaRegistro`, `codAlmacen`, `codTipoDocumento`, `codDetalleNotaSalida`, `codNotaSalida`, `codDetalleNotaIngreso`, `codNotaIngreso`, `codSerie`, `serie`, `numeracion`, `fechaRegistroDoc`, `codProducto`, `codUnidad`, `tipoStock`, `entrada`, `salida`, `stockFinal`) VALUES 
(NOW(), codAlmacen, _codTipoDoc, NULL, NULL, NULL, NULL, _codSerie, _serie, _numeracion, _fechaRegistroDoc, NEW.codProducto, unim, 1, (cantidad*fac),NULL, (IFNULL(_stockFinal,0) + (cantidad*fac)));
		END IF;
	END IF;
END IF;
END

##### factura_venta AFTER INSERT ActualizaEstadoCotizacion
BEGIN



IF NEW.codCotizacion <>0 THEN



UPDATE cotizacion SET vigente = 4 WHERE codCotizacion = NEW.codCotizacion;



END IF;



END

##### factura_venta AFTER INSERT ActualizaCorrelativoDocFact
BEGIN



DECLARE manual BIT(1);



IF NEW.numDocumento  <> '' THEN    

      UPDATE serie SET numeracion = numeracion +1 WHERE codserie = NEW.codSerie;

END IF;



END

##### factura_venta AFTER INSERT ActualizaLineaDisponibleCliente
BEGIN

DECLARE tipoforma BIT;

DECLARE tc DECIMAL(10,4);

DECLARE mon INT(11);

DECLARE  lineacred  DECIMAL(10,4);



SELECT lineacreditodisponible INTO lineacred FROM cliente WHERE codCliente=NEW.codCliente;

SELECT moneda INTO mon FROM cliente WHERE codCliente=NEW.codCliente;

SELECT compra INTO tc  FROM tipocambio WHERE date(fecharegistro) = Date(NEW.fechasalida)  ;

SELECT tipo INTO tipoforma FROM formapago WHERE codFormaPago = NEW.formapago;





IF(tipoforma <>1) THEN

  IF  lineacred >0 THEN

      IF mon=NEW.moneda THEN

         UPDATE cliente

         SET lineacreditodisponible = lineacreditodisponible - NEW.total

         WHERE codCliente = NEW.codCliente;

      ELSE

         IF mon=2 and NEW.moneda=1 THEN

            UPDATE cliente

            SET lineacreditodisponible = lineacreditodisponible - NEW.total /tc

            WHERE codCliente = NEW.codCliente;

          

         ELSE

            

            UPDATE cliente

            SET lineacreditodisponible = lineacreditodisponible - NEW.total *tc

            WHERE codCliente = NEW.codCliente;

         END IF;

      END IF;

  END IF; 



END IF;

END

##### factura_venta AFTER INSERT InsertarVentaCreditoEnCaja
BEGIN 
		DECLARE codigoCaja INTEGER (11);
	
	if EXISTS(SELECT * FROM formapago WHERE codFormaPago = NEW.formapago and dias > 0 and tipo =0) THEN
	
		
		IF EXISTS(
				SELECT * FROM `caja` 
				WHERE `codsucursal` = NEW.codSucursal AND `codalmacen` = NEW.codAlmacen AND `estado` = '1' 
				AND date(`fechaapertura`) = date(now())
				order by fechaapertura desc
				limit 1
		) THEN
			SELECT codcaja INTO codigoCaja FROM `caja` 
			WHERE `codsucursal` = NEW.codSucursal AND `codalmacen` = NEW.codAlmacen AND `estado` = '1' 
			AND date(`fechaapertura`) = date(now())
			order by fechaapertura desc
			limit 1;
		
			UPDATE caja SET totalVentasCredito = (totalVentasCredito + NEW.total)   
			WHERE codSucursal = NEW.codSucursal and estado =1 AND codcaja = codigoCaja AND codalmacen = NEW.codAlmacen; 

			
		END IF;
						
	end if;

END

##### kardex AFTER INSERT tr_kardex_after_insert_registrar_o_actualizar_stock_diario
BEGIN
                
                IF NEW.fechatransaccion IS NOT NULL AND NEW.codproducto IS NOT NULL THEN
                    INSERT INTO kardex_snapshot_diario 
                        (fecha, producto_id, stock_final, last_kardex_id, movimientos_del_dia)
                    VALUES 
                        (NEW.fechatransaccion, NEW.codproducto, NEW.stock, NEW.item, 1)
                    ON DUPLICATE KEY UPDATE
                        
                        stock_final = IF(NEW.item >= last_kardex_id, NEW.stock, stock_final),
                        last_kardex_id = IF(NEW.item >= last_kardex_id, NEW.item, last_kardex_id),
                        movimientos_del_dia = movimientos_del_dia + 1;
                END IF;
            END

##### movimientostock BEFORE INSERT verificarSiExisteAnteriorMovimiento
BEGIN
DECLARE stockAnterior decimal(10,4);
SET NEW.fechaRegistro = NOW(6);
IF EXISTS(select * from movimientostock ms where ms.codProducto = NEW.codProducto and ms.codAlmacen = NEW.codAlmacen and ms.tipoStock = NEW.tipoStock) THEN
	select ms.stockFinal into stockAnterior from movimientostock ms where ms.codProducto = NEW.codProducto and ms.codAlmacen = NEW.codAlmacen and ms.tipoStock = NEW.tipoStock ORDER BY ms.fechaRegistro DESC,ms.codMovStock desc limit 1;
	SET NEW.stockFinal = (IFNULL(stockAnterior,0) + IFNULL(NEW.entrada,0) - IFNULL(NEW.salida,0));
END IF;

END

##### notasalida BEFORE INSERT ActualizaCorrelativoDoc
BEGIN



DECLARE manual BIT(1);



SELECT preimpreso INTO manual FROM serie WHERE codSerie = NEW.codSerie; 





END

##### notasalida AFTER INSERT ActualizaNotaIngreso
BEGIN

IF NEW.codTransaccion<>7 THEN

          IF (NEW.codTransaccion=19) THEN

                   update facturacion set codReferencia=NEW.codNotaSalida where codFactura=NEW.documentoreferencia;

          END IF;

          IF (NEW.codTransaccion<>19) THEN

          IF NEW.documentoreferencia<>'' THEN

               UPDATE notaingreso SET codReferencia=NEW.codNotaSalida WHERE codNotaIngreso=NEW.documentoreferencia ;

          END IF;

          END IF;

END IF;

END

##### notasalida AFTER INSERT SalidaXPagoSeparacion
BEGIN 



     IF NEW.codSeparacion > 0 THEN

          INSERT INTO cajaseparacion (codseparacion, codMoneda,codAlmacen, codTipoPago, ingresoegreso, fechapago, fecharegistro,estado, codUser, monto)

                                               VALUES (NEW.codSeparacion, NEW.moneda,NEW.codAlmacen, 5, 0, NOW() , NOW(), 1, NEW.codUsuario, NEW.total);



     END IF;

END

##### notasalida AFTER INSERT NuevoPagoNotaCreditoCompras
BEGIN



DECLARE pendient DECIMAL(10,4);

DECLARE valor DECIMAL(10,4);

DECLARE tip INT(11) DEFAULT 1;

DECLARE codser INT(11) DEFAULT 0;

DECLARE ser VARCHAR(5) DEFAULT '';

DECLARE num INT(11) DEFAULT 0;



SELECT pendiente INTO pendient  FROM facturacion WHERE codFactura = NEW.codAplicada;

SELECT codSerie, LPAD(serie,3,'0'), numeracion INTO codser, ser, num 
FROM serie WHERE codAlmacen = NEW.codAlmacen AND codDocumento = 18 AND preimpreso=0;



IF pendient  <> 0 THEN

         SET valor  = pendient  - NEW.total;

         IF valor  <> 0 THEN

                  SET tip  = 0;

        END IF; 

END IF;





 IF NEW.codTransaccion = 19 AND NEW.aplicada = 1 and false THEN



INSERT INTO pago (codNota,codLetra,codTipoPago,codMoneda,codTarjetasPago,tipo,ingresoegreso,tipocambio,

montopagado,montocobrado,vuelto,codAlmacen,codctacte,numctacte,noperacion,ncheque,fechapago,

observacion,codUser,codBanco,fecharegistro, codSerie, serie, numdocumento, Aprobado, referencia, notacredito, codNotaCredito)



VALUES (NEW.codAplicada,0,10,NEW.moneda,0,tip,0,NEW.tipocambio,NEW.total,NEW.total,

0,NEW.codAlmacen,0,'','','',NOW(),'',NEW.codUsuario,0,NOW(),codser,ser, num, 0, null, 1, NEW.codAplicada);





END IF;



END

##### productoalmacen AFTER INSERT trg_insert_bd_master_logistica_stocks
BEGIN
  
END

##### productoalmacen BEFORE UPDATE ActualizaPrecioPromedio
BEGIN

DECLARE igv float;

DECLARE tc float;

DECLARE fecha DATE;



SET fecha=(SELECT ifnull(fecharegistro, now()) FROM productoalmacen WHERE codProducto=OLD.codProducto AND codAlmacen=OLD.codAlmacen );

SET igv = (SELECT tasaigv FROM configuracion WHERE estado = 1);

SET tc=(SELECT venta FROM tipocambio WHERE date(fecha)=date(fecha) LIMIT 1);



if(tc is null) then SET tc=(SELECT venta FROM tipocambio WHERE date(fecharegistro)=date(now()) LIMIT 1); end if;





                IF OLD.stockactual <>NEW.stockactual THEN

                   

                            IF (NEW.stockactual = 0) THEN                                

                                  SET NEW.valorpromedio = 0;

                                  SET NEW.valorpromediosoles= 0;

                            ELSE

                                  SET NEW.valorpromediosoles =NEW.soles / NEW.stockactual;

                                  SET NEW.valorpromedio =(NEW.valorpromediosoles / tc); 

                            END IF;

                                

                            

                                   SET NEW.preciopromedio =NEW.valorpromedio *(1+igv/100);

                           

                END IF;

END

##### productoalmacen AFTER UPDATE trg_update_bd_master_logistica_stocks
BEGIN
  
END

