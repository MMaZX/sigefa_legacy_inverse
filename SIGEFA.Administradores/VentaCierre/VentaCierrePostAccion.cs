using System;
using System.Threading.Tasks;

// Representa una acción a ejecutar tras confirmar la venta en base de datos.
// Compuesta por el paso, un nombre descriptivo y un delegado asincrónico.
namespace SIGEFA.Administradores.VentaCierre;

public class VentaCierrePostAccion
{
    // Paso posterior asociado.
    public VentaCierrePostPaso paso { get; set; }

    // Nombre legible de la acción para mostrar en la interfaz.
    public string nombre { get; set; }

    // Delegado asincrónico que recibe el contexto de ejecución del paso.
    public Func<VentaCierrePostEjecucion, Task> ejecutar { get; set; }

    public VentaCierrePostAccion()
    {
    }

    public VentaCierrePostAccion(VentaCierrePostPaso paso, Func<VentaCierrePostEjecucion, Task> ejecutar)
    {
        this.paso = paso;
        this.nombre = VentaCierrePostPasoTexto.obtenerNombre(paso);
        this.ejecutar = ejecutar;
    }

    public VentaCierrePostAccion(VentaCierrePostPaso paso, string nombre, Func<VentaCierrePostEjecucion, Task> ejecutar)
    {
        this.paso = paso;
        this.nombre = !string.IsNullOrEmpty(nombre) ? nombre : VentaCierrePostPasoTexto.obtenerNombre(paso);
        this.ejecutar = ejecutar;
    }

    public VentaCierrePostAccion(VentaCierrePostPaso paso, string nombre, Func<Task> ejecutarSimple)
    {
        this.paso = paso;
        this.nombre = !string.IsNullOrEmpty(nombre) ? nombre : VentaCierrePostPasoTexto.obtenerNombre(paso);
        if (ejecutarSimple != null)
        {
            this.ejecutar = async ctx => await ejecutarSimple();
        }
    }

    public VentaCierrePostAccion(string nombre, Func<VentaCierrePostEjecucion, Task> ejecutar)
    {
        this.nombre = nombre;
        this.ejecutar = ejecutar;
    }
}
