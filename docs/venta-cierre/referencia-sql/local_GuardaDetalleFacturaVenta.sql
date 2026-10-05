*************************** 1. row ***************************
GuardaDetalleFacturaVenta
STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION
CREATE DEFINER=`root`@`%` PROCEDURE `GuardaDetalleFacturaVenta`(codpro int(11), codventa int(11), codalma int(11),unidad int(11), serielote varchar(20), cantidad decimal(10,4), cantidadp decimal(10,4), moneda int(11), precio DECIMAL(10,4), subtotal DECIMAL(10,4), dscto1 DECIMAL(10,4), dscto2 DECIMAL(10,4), dscto3 DECIMAL(10,4),montodscto DECIMAL(10,4),igv DECIMAL(10,4),importe DECIMAL(10,4),precioreal DECIMAL(10,4),valoreal DECIMAL(10,4),codDetaCoti int(11),codusu int(11),  codDetaPed int(11),codDetaSep int(11), serie_ varchar(255),nrochasis_ varchar(255),modelo_ varchar(255),marca_ varchar(255),color_ varchar(255), _icbper decimal(10,2),_icbper_band int(1),codlinea int (11),codfamilia int(11) ,OUT  newid  int(11))
BEGIN

DECLARE sal DECIMAL(10,4);
DECLARE varComision DECIMAL (12,2);
DECLARE vp DECIMAL(10,4);
DECLARE cant DECIMAL(10,4);


DECLARE dato INT(11);

DECLARE trans INT(11);

DECLARE tip INT(11);

declare _preciocompra decimal(10,4);

DECLARE stockact DECIMAL(10,2);
DECLARE stockdisp DECIMAL(10,2);
DECLARE _tieneReq INT(5);

DECLARE valProm DECIMAL (10,4);

DECLARE valPromS DECIMAL (10,4);

DECLARE codTran INT(11);



DECLARE unim int(11);

DECLARE fac DECIMAL(10,4);

DECLARE codnota INT(11);

declare _esservicio int(11);



set varComision=(SELECT comision from producto WHERE codProducto=codpro );

SET varComision = (varComision * (importe-igv)) /  100;

SET unim = (SELECT p.codUnidadMedida FROM producto p WHERE p.codProducto = codpro);

 SET fac =  (select ifnull((SELECT ue.factor FROM unidadequivalente ue where ue.codProducto = codpro and ue.compra_venta = 2 and ue.codUnidadMedida = unidad and ue.codUndEqui = unim),1));

SELECT stockactual/fac,stockdisponible/fac INTO stockact,stockdisp FROM productoalmacen WHERE codProducto=codpro AND codAlmacen = codalma;

select EXISTS(select * from req_almacen ra where ra.codPedidoVenta = (select dp.codPedido from detallepedido dp where dp.codDetallePedido = codDetaPed limit 1) AND ra.estado != 12) INTO _tieneReq;

SELECT valorpromedio INTO valProm FROM productoalmacen WHERE codProducto=codpro AND codAlmacen=codalma;

SELECT valorpromediosoles INTO valPromS FROM productoalmacen WHERE codProducto=codpro AND codAlmacen=codalma;

SELECT codTransaccion INTO codTran FROM factura_venta WHERE codFacturaV = codventa AND codAlmacen=codalma AND estado = 1;

select codTipoArticulo into _esservicio from producto where codProducto=codpro;



IF valProm IS NULL THEN

	SET valProm = 0;

END IF;



IF valPromS IS NULL THEN

	SET valPromS = 0;

END IF;



SET vp = (SELECT ifnull(valorpromediosoles,0) FROM productoalmacen WHERE codProducto=codpro AND codAlmacen = codalma limit 1);



SET _preciocompra=ifnull((select (ifnull(ueq.precio,0.00)* ueq1.factor)

from unidadequivalente ueq, unidadequivalente ueq1

where ueq.compra_venta=0

and ueq.codUnidadMedida = (select codUnidadMedida from producto where codproducto =codpro)

and ueq.codproducto =codpro

and ueq1.codunidadmedida=unidad

and ueq1.compra_venta=2 and ueq1.codproducto=codpro),0.00);





IF codTran = 7 THEN



		if(_esservicio=2)then

			set stockact=1000000;

		end if;



		IF if(_tieneReq = 1,stockact >= cantidad,stockact >= cantidad and stockdisp >= cantidad)  THEN



			INSERT INTO detallefactura_venta

			(codProducto, codFacturaV, codAlmacen, unidadingresada, serielote, cantidad, cantidadpendiente, moneda, preciounitario, subtotal, descuento1, descuento2, descuento3, montodscto, 

			 igv, importe,precioreal,valoreal, codDetalleCotizacion, codUser, fecharegistro,comision, valorpromedio, valorpromediosoles,serie,nrochasis,modelo,marca,color,

			icbper,icbper_band,precioCompra,codlinea,codfamilia)



			VALUES

			(codpro, codventa, codalma, unidad, serielote, cantidad, cantidadp, moneda, precio, subtotal, dscto1, dscto2, dscto3, montodscto, igv, importe,precioreal,valoreal, 

			 codDetaCoti, codusu, NOW(),varComision, valProm, valPromS,serie_,nrochasis_,modelo_,marca_,color_,

			_icbper,_icbper_band,_preciocompra,codlinea,codfamilia);

			SET newid = LAST_INSERT_ID();



			IF newid > 0 THEN

				SET codnota = (SELECT MAX(codNotaSalida) FROM notasalida WHERE documentoreferencia=codventa);

			END IF;

			

			INSERT INTO detallenotasalida

			(codProducto, codNotaSalida, codAlmacen, unidadingresada, serielote, cantidad, preciounitario, subtotal, descuento1, descuento2, descuento3, montodscto, 

			 igv, importe, precioreal, valoreal, codUser, fecharegistro, comision, valorpromedio, valorpromediosoles, codDetallePedido, codDetalleSeparacion)

			

			VALUES

			(codpro, codnota, codalma, unidad, serielote, cantidad, precio, subtotal, dscto1, dscto2, dscto3, montodscto, igv, 

			 importe, precioreal, valoreal, codusu, NOW(), varComision, valProm, valPromS, codDetaPed, codDetaSep);			

		ELSE

			SET newid = -1;

		END IF;

		ELSEIF codTran = 21 THEN

			INSERT INTO detallefactura_venta

			(codProducto, codFacturaV, codAlmacen, unidadingresada, serielote, cantidad, cantidadpendiente, moneda, preciounitario, subtotal, descuento1, descuento2, descuento3, montodscto, 

			 igv, importe,precioreal,valoreal, codDetalleCotizacion, codUser, fecharegistro,comision, valorpromedio, valorpromediosoles,

			icbper,icbper_band,precioCompra,codlinea,codfamilia)



			VALUES

			(codpro, codventa, codalma, unidad, serielote, cantidad, cantidadp, moneda, precio, subtotal, dscto1, dscto2, dscto3, montodscto, igv, importe,precioreal,valoreal, 

			 codDetaCoti, codusu, NOW(),varComision,valProm, valPromS,_icbper,_icbper_band,_preciocompra,codlinea,codfamilia);



			SET newid = LAST_INSERT_ID();

			IF newid > 0 THEN

				SET codnota = (SELECT MAX(codNotaSalida) FROM notasalida WHERE documentoreferencia=codventa);

			END IF;

			

			INSERT INTO detallenotasalida

			(codProducto, codNotaSalida, codAlmacen, unidadingresada, serielote, cantidad, preciounitario, subtotal, descuento1, descuento2, descuento3, montodscto, 

			 igv, importe, precioreal, valoreal, codUser, fecharegistro, comision, valorpromedio, valorpromediosoles)

			

			VALUES

			(codpro, codnota, codalma, unidad, serielote, cantidad, precio, subtotal, dscto1, dscto2, dscto3, montodscto, igv, 

			 importe, precioreal, valoreal, codusu, NOW(), varComision, valProm, valPromS, codDetaPed);	



END IF;



SET dato = (SELECT COUNT(*) FROM productoalmacen WHERE codProducto=codpro AND codAlmacen = codalma);





SET unim = (SELECT codUnidadMedida FROM producto WHERE codProducto = codpro);

SET fac = (SELECT  factor  FROM  unidadequivalente eq WHERE  eq.codProducto = codpro AND eq.codUnidadMedida = unidad  AND eq.codUndEqui = unim and eq.compra_venta = 2);



IF dato = 0 THEN



	SET sal = 0;

	SET cant = 0;

	SET trans = 0;

	SET tip = 0;



ELSE



	SET sal = (SELECT totalsolesingresos FROM productoalmacen WHERE codProducto=codpro AND codAlmacen = codalma) - (SELECT totalsolessalidas FROM productoalmacen WHERE codProducto=codpro AND codAlmacen = codalma);

	SET cant = (SELECT stockactual FROM productoalmacen WHERE codProducto=codpro AND codAlmacen = codalma);

	SET trans = (SELECT codTransaccion FROM notasalida WHERE codNotaSalida = codnota);

	SET tip = (SELECT codTipoDocumento FROM notasalida WHERE codNotaSalida = codnota);



END IF;



if(_esservicio<>2) && (newid > 0) then

INSERT INTO kardex(codproducto,coddetallesalida,cantidad,precio,total,stock,saldo,valorpromedio,fechatransaccion,codTransaccion,codtipodocumento,estado,codNotaSalida,codAlmacen)

VALUES( codpro,@@identity,(cantidad*fac),vp,((cantidad*fac)*vp),cant,(cant*vp),vp,NOW(),trans,tip,0,codnota,codalma);

end if;



END
utf8mb4
utf8mb4_general_ci
utf8mb4_unicode_ci
