using System.Windows.Forms;
using SIGEFA.Administradores.VersionApp;

namespace SIGEFA.Formularios;

// Punto unico para mostrar la version del build en la interfaz (login y home).
// Lee la version del ensamblado y los datos que MSBuild genera al compilar (VersionCompilacion).
internal static class VersionSistema
{
    private static string VersionEnsamblado => typeof(VersionSistema).Assembly.GetName().Version.ToString();

    public static string TextoCorto()
    {
        return VersionTexto.Formatear(VersionEnsamblado, VersionCompilacion.Commit, VersionCompilacion.Rama, VersionCompilacion.ConCambiosLocales, VersionCompilacion.Fecha);
    }

    public static void MostrarDialogo(IWin32Window owner)
    {
        string detalle = VersionTexto.Detalle(VersionEnsamblado, VersionCompilacion.Commit, VersionCompilacion.Rama, VersionCompilacion.ConCambiosLocales, VersionCompilacion.Fecha);
        MessageBox.Show(owner, detalle, "Versión del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
