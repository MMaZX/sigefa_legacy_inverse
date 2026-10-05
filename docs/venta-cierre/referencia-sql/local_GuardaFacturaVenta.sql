*************************** 1. row ***************************
GuardaFacturaVenta
STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION
CREATE DEFINER=`root`@`%` PROCEDURE `GuardaFacturaVenta`(codSu int(11), codalma int(11), codtran int(11), codtipo int(11), codlista int(11), codser int(11),serie varchar(4), numdoc varchar(10),tipocliente int(11), codcli int(11),moneda int(1),tipocambio float,fechasalida datetime,comentario varchar(500),bruto DECIMAL(10,4),montodscto DECIMAL(10,4),igv DECIMAL(10,4),total DECIMAL(10,4),pendiente DECIMAL(10,4),estado bit,formapago int(11),fechapago datetime,codven int(11),codCoti int(11), codusu int(11), docreferencia varchar(20),  motiv varchar(500), detcoment varchar(500), consultorext bit, codsalidaconsulext int(11), codped int(11), codsep int(11), tipoventa_ex int(11), gravadas_ex DECIMAL(10,4), exoneradas_ex DECIMAL(10,4), inafectas_ex DECIMAL(10,4), gratuitas_ex DECIMAL(10,4), codEmpresa_ex int(11), Boletafactura_ex int(11), codigobarras_ex varchar(100), codigoBarrasCifrado_ex varchar(100), NombreCliente_ex varchar(300), TipoDocumentoAnticipo_ex varchar(15), DocumentoReferenciaAnticipo_ex varchar(50), MontoAnticipo_ex decimal(10,4), numeroDocumentoIdentidad_ex varchar(15), codigoDocumentoIdentidad_ex int(11), ventasinstock_ex int(11),_valorRetencion int(11),_idTecnico int(11),_idZona int(11),_codCanalVenta char(6), OUT newid  int(11),_icbper decimal(10,2),OUT numeraDoc varchar(30))
BEGIN


declare correlativo int(11);



set correlativo =(select LPAD(numeracion,8,'0') from serie where codSerie=codser);





INSERT INTO factura_venta(codSucursal, codAlmacen, codTransaccion, codTipoDocumento, codListaPrecio, codSerie, serie, numDocumento,

													tipocliente, codCliente, moneda, tipocambio, fechasalida, comentario, bruto, montodscto, valorventa, igv, 

													total, pendiente, estado, formapago, fechapago, codVendedor, codCotizacion, codUsuario, fecharegistro, 

													documentoreferencia, motivo, detallecomentario, codPedido, codSeparacion,tipoventa, gravadas, exoneradas, 

													inafectas, gratuitas, codEmpresa, Boletafactura, codigobarras, codigoBarrasCifrado, NombreCliente, 

													TipoDocumentoAnticipo, DocumentoReferenciaAnticipo, MontoAnticipo, numeroDocumentoIdentidad, 

													codigoDocumentoIdentidad, ventasinstock,icbper,valorRetencion,idTecnico,idZona,codCanalVenta)



VALUES(codSu, codalma, codtran, codtipo, codlista,codser,serie, correlativo,tipocliente, codcli, moneda, tipocambio, fechasalida, 

			 comentario, bruto, montodscto, (total-igv), igv, total, pendiente, estado ,formapago, fechapago,codven, codCoti, codusu, 

			 NOW(), docreferencia, motiv, detcoment, codped, codsep,tipoventa_ex, gravadas_ex, exoneradas_ex, inafectas_ex, gratuitas_ex, 

			 codEmpresa_ex, Boletafactura_ex, codigobarras_ex, codigoBarrasCifrado_ex, NombreCliente_ex, TipoDocumentoAnticipo_ex, 

			 DocumentoReferenciaAnticipo_ex, MontoAnticipo_ex, numeroDocumentoIdentidad_ex, codigoDocumentoIdentidad_ex, ventasinstock_ex,

				_icbper,_valorRetencion,_idTecnico,_idZona,_codCanalVenta);





SET newid = LAST_INSERT_ID();

SET numeraDoc=(SELECT numDocumento from factura_venta WHERE codSucursal=codSu AND codAlmacen=codalma AND codSerie=codser AND codFacturaV=newid);



INSERT INTO notasalida(codSucursal, codAlmacen, codTransaccion, codTipoDocumento, codListaPrecio, codSerie, serie, numdocumento, 

											 tipocliente, codCliente, moneda, tipocambio, fechasalida, comentario, bruto, montodscto, valorventa, igv, 

											 total, pendiente, estado, formapago, fechapago, codVendedor, codUsuario, fecharegistro, documentoreferencia, 

											 consultorext,codsalidaconsulext, codPedido, codSeparacion, ventasinstock)



VALUES(codSu, codalma, codtran, codtipo, codlista, codser, (SELECT f.serie FROM factura_venta f WHERE f.codFacturaV=newid), 

			correlativo, tipocliente, codcli, moneda, tipocambio,fechasalida, comentario, bruto, montodscto, (total-igv), igv, total, pendiente, estado, 				formapago, fechapago, codven,codusu, NOW(), newid, consultorext,codsalidaconsulext,codped, codsep, ventasinstock_ex);







END
utf8mb4
utf8mb4_general_ci
utf8mb4_unicode_ci
