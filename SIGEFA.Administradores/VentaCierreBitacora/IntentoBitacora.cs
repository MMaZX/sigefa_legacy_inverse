// DTOs puros e inmutables de la bitácora del cierre de venta (ruta nueva).
// Sin lógica de negocio ni acceso a datos: los llena el servicio del cierre y
// los consume el repositorio MySQL o el respaldo en archivo.
using System;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Un intento de cerrar la venta de un pedido (cabecera de la bitácora).
public sealed class IntentoBitacora
{
    public IntentoBitacora(
        int codPedido, int? codUsuario, string usuario, string equipo,
        string versionApp, int totalBloques, DateTime inicio)
    {
        CodPedido = codPedido;
        CodUsuario = codUsuario;
        Usuario = usuario ?? string.Empty;
        Equipo = equipo ?? string.Empty;
        VersionApp = versionApp ?? string.Empty;
        TotalBloques = totalBloques;
        Inicio = inicio;
    }

    public int CodPedido { get; }

    public int? CodUsuario { get; }

    public string Usuario { get; }

    public string Equipo { get; }

    public string VersionApp { get; }

    public int TotalBloques { get; }

    public DateTime Inicio { get; }
}
