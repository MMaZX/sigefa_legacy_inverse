using System;
using System.Collections.Generic;
using System.Linq;
using SIGEFA.Administradores.ReqVenta;
using Xunit;

namespace SIGEFA.Tests.ReqVenta;

// Pruebas del modelo de pasos y textos de anulación (T1).
// Solo cubren EstadoPaso, PasoOperacion, ResultadoOperacion y ReqVentaTextos,
// sin base de datos ni interfaz gráfica.
public class ReqVentaTextosTests
{
    // La anulación directa de un pendiente tiene 4 pasos, en este orden.
    [Fact]
    public void PasosAnulacionPendiente_TieneCuatroPasosEnOrdenEsperado()
    {
        IReadOnlyList<PasoOperacion> pasos = ReqVentaTextos.PasosAnulacionPendiente();

        string[] esperadas =
        {
            ReqVentaTextos.ComprobarRequerimiento,
            ReqVentaTextos.RechazarTransferencias,
            ReqVentaTextos.DevolverReservas,
            ReqVentaTextos.MarcarAnulado,
        };

        Assert.Equal(4, pasos.Count);
        Assert.Equal(esperadas, pasos.Select(paso => paso.Clave).ToArray());
    }

    // La anulación con extorno tiene 7 pasos, en este orden.
    [Fact]
    public void PasosAnulacionConExtorno_TieneSietePasosEnOrdenEsperado()
    {
        IReadOnlyList<PasoOperacion> pasos = ReqVentaTextos.PasosAnulacionConExtorno();

        string[] esperadas =
        {
            ReqVentaTextos.ComprobarRequerimiento,
            ReqVentaTextos.RevisarStock,
            ReqVentaTextos.CrearExtorno,
            ReqVentaTextos.RegistrarSalida,
            ReqVentaTextos.RegistrarIngreso,
            ReqVentaTextos.AprobarExtorno,
            ReqVentaTextos.MarcarAnulado,
        };

        Assert.Equal(7, pasos.Count);
        Assert.Equal(esperadas, pasos.Select(paso => paso.Clave).ToArray());
    }

    // El catálogo sale todo en Pendiente: el diálogo marca el avance.
    [Fact]
    public void Listas_TodosLosPasos_SalenEnPendiente()
    {
        List<PasoOperacion> todos = new List<PasoOperacion>(ReqVentaTextos.PasosAnulacionPendiente());
        todos.AddRange(ReqVentaTextos.PasosAnulacionConExtorno());

        foreach (PasoOperacion paso in todos)
        {
            Assert.Equal(EstadoPaso.Pendiente, paso.Estado);
        }
    }

    // Ningún paso sale sin clave ni sin texto, y el detalle nunca es nulo.
    [Fact]
    public void Listas_ClaveTextoYDetalle_SinNulosNiVacios()
    {
        List<PasoOperacion> todos = new List<PasoOperacion>(ReqVentaTextos.PasosAnulacionPendiente());
        todos.AddRange(ReqVentaTextos.PasosAnulacionConExtorno());

        foreach (PasoOperacion paso in todos)
        {
            Assert.False(string.IsNullOrWhiteSpace(paso.Clave));
            Assert.False(string.IsNullOrWhiteSpace(paso.Texto));
            Assert.NotNull(paso.Detalle);
        }
    }

    // Los textos son español claro: sin nombres de procedimientos ni de tablas.
    [Fact]
    public void Listas_Textos_SinJergaTecnica()
    {
        string[] prohibidos =
        {
            "CALL",
            "Guarda",
            "req_almacen",
            "transferencia",
            "productoalmacen",
            "detallenota",
        };

        List<PasoOperacion> todos = new List<PasoOperacion>(ReqVentaTextos.PasosAnulacionPendiente());
        todos.AddRange(ReqVentaTextos.PasosAnulacionConExtorno());

        foreach (PasoOperacion paso in todos)
        {
            foreach (string termino in prohibidos)
            {
                Assert.False(
                    paso.Texto.IndexOf(termino, StringComparison.OrdinalIgnoreCase) >= 0,
                    "El texto '" + paso.Texto + "' contiene jerga técnica: " + termino + ".");
            }
        }
    }

    // Las claves no se repiten dentro de cada lista: el diálogo busca por clave.
    [Fact]
    public void Listas_Claves_SonUnicasDentroDeCadaLista()
    {
        string[] pendiente = ReqVentaTextos.PasosAnulacionPendiente().Select(paso => paso.Clave).ToArray();
        string[] conExtorno = ReqVentaTextos.PasosAnulacionConExtorno().Select(paso => paso.Clave).ToArray();

        Assert.Equal(pendiente.Length, pendiente.Distinct().Count());
        Assert.Equal(conExtorno.Length, conExtorno.Distinct().Count());
    }

    // Dos llamadas devuelven la misma secuencia de claves, en el mismo orden.
    [Fact]
    public void Listas_Orden_EstableEntreLlamadas()
    {
        string[] primeraPendiente = ReqVentaTextos.PasosAnulacionPendiente().Select(paso => paso.Clave).ToArray();
        string[] segundaPendiente = ReqVentaTextos.PasosAnulacionPendiente().Select(paso => paso.Clave).ToArray();
        string[] primeraConExtorno = ReqVentaTextos.PasosAnulacionConExtorno().Select(paso => paso.Clave).ToArray();
        string[] segundaConExtorno = ReqVentaTextos.PasosAnulacionConExtorno().Select(paso => paso.Clave).ToArray();

        Assert.Equal(primeraPendiente, segundaPendiente);
        Assert.Equal(primeraConExtorno, segundaConExtorno);
    }

    // Cada llamada devuelve una lista nueva: el llamador no altera el catálogo.
    [Fact]
    public void Listas_CadaLlamada_DevuelveUnaListaNueva()
    {
        IReadOnlyList<PasoOperacion> primeraPendiente = ReqVentaTextos.PasosAnulacionPendiente();
        IReadOnlyList<PasoOperacion> segundaPendiente = ReqVentaTextos.PasosAnulacionPendiente();
        IReadOnlyList<PasoOperacion> primeraConExtorno = ReqVentaTextos.PasosAnulacionConExtorno();
        IReadOnlyList<PasoOperacion> segundaConExtorno = ReqVentaTextos.PasosAnulacionConExtorno();

        Assert.NotSame(primeraPendiente, segundaPendiente);
        Assert.NotSame(primeraConExtorno, segundaConExtorno);
    }

    // Los pasos y el resultado son inmutables: sin set público.
    [Fact]
    public void Modelo_Propiedades_SinSetPublico()
    {
        string[] propiedadesPaso = { "Clave", "Texto", "Estado", "Detalle" };
        foreach (string nombre in propiedadesPaso)
        {
            System.Reflection.PropertyInfo propiedad = typeof(PasoOperacion).GetProperty(nombre);
            Assert.NotNull(propiedad);
            Assert.True(propiedad.CanRead);
            Assert.True(propiedad.SetMethod == null || !propiedad.SetMethod.IsPublic);
        }

        string[] propiedadesResultado = { "Ok", "Mensaje" };
        foreach (string nombre in propiedadesResultado)
        {
            System.Reflection.PropertyInfo propiedad = typeof(ResultadoOperacion).GetProperty(nombre);
            Assert.NotNull(propiedad);
            Assert.True(propiedad.CanRead);
            Assert.True(propiedad.SetMethod == null || !propiedad.SetMethod.IsPublic);
        }
    }

    // El detalle por defecto es cadena vacía, nunca nulo.
    [Fact]
    public void PasoOperacion_DetallePorDefecto_EsCadenaVacia()
    {
        PasoOperacion paso = new PasoOperacion("clave", "texto", EstadoPaso.Pendiente);

        Assert.NotNull(paso.Detalle);
        Assert.Equal(string.Empty, paso.Detalle);
    }

    // El detalle nulo explícito también se guarda vacío.
    [Fact]
    public void PasoOperacion_DetalleNulo_SeGuardaVacio()
    {
        PasoOperacion paso = new PasoOperacion("clave", "texto", EstadoPaso.Pendiente, null);

        Assert.Equal(string.Empty, paso.Detalle);
    }

    // Un resultado de anulación nulo se informa como fallo con mensaje.
    [Fact]
    public void De_ResultadoNulo_EsFalloConMensaje()
    {
        ResultadoOperacion resultado = ResultadoOperacion.De(null);

        Assert.False(resultado.Ok);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    // La conversión preserva el estado y el mensaje de la anulación.
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void De_ResultadoAnulacion_PreservaOkYMensaje(bool ok)
    {
        ResultadoAnulacion origen = new ResultadoAnulacion(ok, "motivo de prueba");

        ResultadoOperacion resultado = ResultadoOperacion.De(origen);

        Assert.Equal(ok, resultado.Ok);
        Assert.Equal("motivo de prueba", resultado.Mensaje);
    }

    // El mensaje nulo se guarda vacío para no propagar nulos.
    [Fact]
    public void Constructor_MensajeNulo_SeGuardaVacio()
    {
        ResultadoOperacion resultado = new ResultadoOperacion(false, null);

        Assert.Equal(string.Empty, resultado.Mensaje);
    }
}
