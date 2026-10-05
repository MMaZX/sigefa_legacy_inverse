using System;
using SIGEFA.Entidades;

// Copia en memoria de los datos de un pago para el modo captura de contado.
// En la ruta nueva el cajero arma los pagos sin persistir nada; el servicio
// los guarda junto con la venta en una sola transacción. Solo lleva los campos
// de clsPago que usa GuardaPago (ver MysqlPago.Insert y el contrato del SP).
// Sin lógica de negocio ni acceso a datos.
namespace SIGEFA.Administradores.VentaCierre;

// Borrador inmutable de un pago. No incluye facturaVentaId (CodNota): el
// servicio lo asigna tras crear la cabecera. Tampoco incluye pagoId (CodPago)
// porque es la salida newid que devuelve GuardaPago. Quedan fuera los campos
// de clsPago que GuardaPago no envía (CodCobrador, Estado, FechaRegistro,
// CodMetPago, SiglaDoc, Pendiente y accion).
public class BorradorPago
{
    // Crea el borrador con los mismos valores que se enviarán a GuardaPago,
    // en el orden de parámetros del contrato. Los textos vacíos se guardan
    // tal cual; el repositorio aplica las reglas de nulos del contrato
    // (por ejemplo serieId 0, serie vacía, referencia vacía y notaCreditoId 0).
    public BorradorPago(
        int letraId,
        int cuotaPreBanId,
        int tipoPagoId,
        int monedaId,
        int tarjetaId,
        bool tipo,
        bool ingresoEgreso,
        decimal tipoCambio,
        decimal montoPagado,
        decimal montoCobrado,
        decimal vuelto,
        decimal mora,
        int almacenId,
        int cuentaCorrienteId,
        string cuentaCorriente,
        string numeroOperacion,
        string numeroCheque,
        DateTime fechaPago,
        string observacion,
        int usuarioId,
        int bancoId,
        bool provision,
        int serieId,
        string serie,
        string numeroDocumento,
        int aprobado,
        string referencia,
        int documentoId,
        int sucursalId,
        int cajaId,
        int notaCredito,
        int notaCreditoId,
        decimal retencionDetraccion,
        string banderaRetencionDetraccion,
        decimal montoEnCuenta,
        int opcionSuma,
        int tipoDescripcion)
    {
        this.letraId = letraId;
        this.cuotaPreBanId = cuotaPreBanId;
        this.tipoPagoId = tipoPagoId;
        this.monedaId = monedaId;
        this.tarjetaId = tarjetaId;
        this.tipo = tipo;
        this.ingresoEgreso = ingresoEgreso;
        this.tipoCambio = tipoCambio;
        this.montoPagado = montoPagado;
        this.montoCobrado = montoCobrado;
        this.vuelto = vuelto;
        this.mora = mora;
        this.almacenId = almacenId;
        this.cuentaCorrienteId = cuentaCorrienteId;
        this.cuentaCorriente = cuentaCorriente ?? string.Empty;
        this.numeroOperacion = numeroOperacion ?? string.Empty;
        this.numeroCheque = numeroCheque ?? string.Empty;
        this.fechaPago = fechaPago;
        this.observacion = observacion ?? string.Empty;
        this.usuarioId = usuarioId;
        this.bancoId = bancoId;
        this.provision = provision;
        this.serieId = serieId;
        this.serie = serie ?? string.Empty;
        this.numeroDocumento = numeroDocumento ?? string.Empty;
        this.aprobado = aprobado;
        this.referencia = referencia ?? string.Empty;
        this.documentoId = documentoId;
        this.sucursalId = sucursalId;
        this.cajaId = cajaId;
        this.notaCredito = notaCredito;
        this.notaCreditoId = notaCreditoId;
        this.retencionDetraccion = retencionDetraccion;
        this.banderaRetencionDetraccion = banderaRetencionDetraccion;
        this.montoEnCuenta = montoEnCuenta;
        this.opcionSuma = opcionSuma;
        this.tipoDescripcion = tipoDescripcion;
    }

    // Copia un pago ya armado (Pag de frmCancelarPago) a un borrador en memoria,
    // sin tocar la base de datos. La bandera nula se conserva nula: el
    // repositorio envía "NAD" en ese caso, como hace hoy MysqlPago.Insert.
    public static BorradorPago desdePago(clsPago pago)
    {
        if (pago == null)
        {
            throw new ArgumentNullException("pago");
        }
        return new BorradorPago(
            pago.CodLetra,
            pago.CodCuotaPreBan,
            pago.CodTipoPago,
            pago.CodMoneda,
            pago.CodTarjeta,
            pago.Tipo,
            pago.IngresoEgreso,
            pago.TipoCambio,
            pago.MontoPagado,
            pago.MontoCobrado,
            pago.Vuelto,
            pago.Mora,
            pago.CodAlmacen,
            pago.codCtaCte,
            pago.CtaCte,
            pago.NOperacion,
            pago.NCheque,
            pago.FechaPago,
            pago.Observacion,
            pago.CodUser,
            pago.CodBanco,
            pago.Provision,
            pago.CodSerie,
            pago.Serie,
            pago.NumDoc,
            pago.Aprobado,
            pago.Referencia,
            pago.CodDoc,
            pago.CodSucursal,
            pago.Codcaja,
            pago.NotaCredito,
            pago.CodNotaCredito,
            pago.RetDet,
            pago.BanderaRetDet,
            pago.MontoEnCuenta,
            pago.OpcionSuma,
            pago.TipoDescripcion);
    }

    // Letra relacionada (codlet); 0 si no aplica.
    public int letraId { get; }

    // Cuota o préstamo bancario relacionado (codcuopreban); 0 si no aplica.
    public int cuotaPreBanId { get; }

    // Tipo de pago: efectivo, tarjeta, banco o cheque (codtipopago).
    public int tipoPagoId { get; }

    // Moneda del pago (codmon).
    public int monedaId { get; }

    // Tarjeta usada (codtar); 0 si no aplica.
    public int tarjetaId { get; }

    // Tipo del comprobante relacionado (tipo).
    public bool tipo { get; }

    // Indica si es ingreso o egreso (ingegre).
    public bool ingresoEgreso { get; }

    // Tipo de cambio aplicado (tipocambio).
    public decimal tipoCambio { get; }

    // Monto pagado por el cliente (montopa).
    public decimal montoPagado { get; }

    // Monto cobrado registrado (montoco).
    public decimal montoCobrado { get; }

    // Vuelto entregado al cliente (vuelto).
    public decimal vuelto { get; }

    // Mora cobrada (mora).
    public decimal mora { get; }

    // Almacén donde se cobra (codalma).
    public int almacenId { get; }

    // Cuenta corriente interna (codcta).
    public int cuentaCorrienteId { get; }

    // Número de cuenta corriente (numcta).
    public string cuentaCorriente { get; }

    // Número de operación bancaria (noperacion).
    public string numeroOperacion { get; }

    // Número de cheque (ncheque).
    public string numeroCheque { get; }

    // Fecha del pago (fecha).
    public DateTime fechaPago { get; }

    // Observación del pago (observa).
    public string observacion { get; }

    // Usuario que registra el pago (codusu).
    public int usuarioId { get; }

    // Banco del pago (codban).
    public int bancoId { get; }

    // Indica si el pago queda en provisión (provi).
    public bool provision { get; }

    // Serie del documento del pago (codserie); 0 se envía como nulo.
    public int serieId { get; }

    // Serie del documento del pago (serie); vacía se envía como nula.
    public string serie { get; }

    // Número de documento del pago (numdoc); vacío se envía como nulo.
    public string numeroDocumento { get; }

    // Estado de aprobación (aprob).
    public int aprobado { get; }

    // Referencia del pago (ref); vacía se envía como nula.
    public string referencia { get; }

    // Documento relacionado (coddoc).
    public int documentoId { get; }

    // Sucursal donde se cobra (codsucur).
    public int sucursalId { get; }

    // Caja donde se cobra (codCaja_ex).
    public int cajaId { get; }

    // Marca si se aplicó nota de crédito (notacre).
    public int notaCredito { get; }

    // Nota de crédito aplicada (codnotac); 0 se envía como nula.
    public int notaCreditoId { get; }

    // Monto de retención o detracción (ctdadDetRet).
    public decimal retencionDetraccion { get; }

    // Bandera de retención o detracción (tipoDetRet); nula se envía como "NAD".
    public string banderaRetencionDetraccion { get; }

    // Monto en cuenta (montoEnCuenta).
    public decimal montoEnCuenta { get; }

    // Opción de suma del pago (opcionSuma).
    public int opcionSuma { get; }

    // Tipo de descripción del pago (tipo_descripcion).
    public int tipoDescripcion { get; }
}
