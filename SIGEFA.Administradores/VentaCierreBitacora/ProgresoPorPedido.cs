using System;
using System.Collections.Generic;
using SIGEFA.Administradores.VentaCierre;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Decorador de IProgress<VentaCierreProgreso> para un cierre con varios pedidos:
// hay una bitácora por pedido. Los eventos globales (almacén "Global": abrir la
// transacción, bloquear series y stock, confirmar) van a todas; los de bloque van
// solo a la del pedido de ese bloque. Después delega al progreso interno (el
// diálogo) y jamás lanza por culpa de la bitácora.
public sealed class ProgresoPorPedido : IProgress<VentaCierreProgreso>
{
    // Nombre de almacén con el que el servicio marca los eventos de toda la orden.
    private const string AlmacenGlobal = "Global";

    private readonly IReadOnlyList<int> _pedidoPorBloque;
    private readonly Dictionary<int, ProgresoConBitacora> _registradorPorPedido;
    private readonly IProgress<VentaCierreProgreso> _interno;

    // pedidoPorBloque: índice 0 = bloque 1. interno puede ser null (solo se registra).
    public ProgresoPorPedido(
        IReadOnlyList<int> pedidoPorBloque,
        IReadOnlyDictionary<int, BitacoraCierre> bitacoraPorPedido,
        IProgress<VentaCierreProgreso> interno)
    {
        _pedidoPorBloque = pedidoPorBloque ?? new int[0];
        _registradorPorPedido = CrearRegistradores(bitacoraPorPedido);
        _interno = interno;
    }

    public void Report(VentaCierreProgreso progreso)
    {
        Registrar(progreso);
        if (_interno != null)
        {
            _interno.Report(progreso);
        }
    }

    private static Dictionary<int, ProgresoConBitacora> CrearRegistradores(
        IReadOnlyDictionary<int, BitacoraCierre> bitacoraPorPedido)
    {
        var registradores = new Dictionary<int, ProgresoConBitacora>();
        if (bitacoraPorPedido == null)
        {
            return registradores;
        }

        foreach (KeyValuePair<int, BitacoraCierre> par in bitacoraPorPedido)
        {
            if (par.Value != null)
            {
                registradores[par.Key] = new ProgresoConBitacora(par.Value, null);
            }
        }

        return registradores;
    }

    private void Registrar(VentaCierreProgreso progreso)
    {
        try
        {
            foreach (ProgresoConBitacora registrador in Destinatarios(progreso))
            {
                registrador.Report(progreso);
            }
        }
        catch (Exception)
        {
        }
    }

    private IEnumerable<ProgresoConBitacora> Destinatarios(VentaCierreProgreso progreso)
    {
        if (progreso.almacenNombre == AlmacenGlobal)
        {
            return _registradorPorPedido.Values;
        }

        int indice = progreso.bloqueActual - 1;
        if (indice < 0 || indice >= _pedidoPorBloque.Count)
        {
            return _registradorPorPedido.Values;
        }

        ProgresoConBitacora propio;
        return _registradorPorPedido.TryGetValue(_pedidoPorBloque[indice], out propio)
            ? new[] { propio }
            : new ProgresoConBitacora[0];
    }
}
