using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SIGEFA.Formularios;

// Icono unico del sistema: el mismo que el ejecutable (ApplicationIcon en SIGEFA.csproj).
// Los formularios decompilados traen cada uno su propio icono en el .resx; aqui se reemplaza
// en las ventanas principales (login, menu y ventanas hijas del menu) y en las que usan el icono por defecto.
internal static class IconoSistema
{
    private static Icon icono;
    private static bool cargado;

    public static Icon Obtener()
    {
        if (cargado)
        {
            return icono;
        }
        cargado = true;
        try
        {
            icono = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        catch (Exception)
        {
            icono = null;
        }
        return icono;
    }

    public static void Aplicar(Form formulario)
    {
        Icon actual = Obtener();
        if (formulario != null && actual != null)
        {
            formulario.Icon = actual;
        }
    }

    // Formularios sin icono propio usan Form.defaultIcon: se cambia una vez al arrancar.
    public static void UsarComoIconoPorDefecto()
    {
        Icon actual = Obtener();
        FieldInfo campo = typeof(Form).GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static);
        if (actual != null && campo != null)
        {
            campo.SetValue(null, actual);
        }
    }
}
