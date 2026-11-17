using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
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
        MessageBox.Show(owner, VersionTexto.Detalle(ArmarDatos()), "Versión del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static VersionDatos ArmarDatos()
    {
        var datos = new VersionDatos
        {
            Version = VersionEnsamblado,
            Commit = VersionCompilacion.Commit,
            Asunto = VersionCompilacion.Asunto,
            ConCambiosLocales = VersionCompilacion.ConCambiosLocales,
            FechaCompilacion = VersionCompilacion.Fecha,
            Equipo = Environment.MachineName,
            RutaEjecutable = Application.ExecutablePath
        };
        // Solo servidor, puerto y base: nunca usuario ni contrasena.
        try
        {
            var conexion = new MySqlConnectionStringBuilder(clsConexionMysql.sConex);
            datos.BaseDatos = conexion.Database;
            datos.Servidor = conexion.Server + ":" + conexion.Port;
        }
        catch (Exception)
        {
            datos.BaseDatos = null;
            datos.Servidor = null;
        }
        return datos;
    }
}
