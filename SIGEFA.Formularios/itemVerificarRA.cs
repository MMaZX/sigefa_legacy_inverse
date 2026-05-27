namespace SIGEFA.Formularios;

internal struct itemVerificarRA(int _codProducto, int _codUnidad, int _codAlmacen, double _anteriorCantidad, double _nuevaCantidad, int _proceso = 1)
{
	internal int codProducto = _codProducto;

	internal int codUnidad = _codUnidad;

	internal double anteriorCantidad = _anteriorCantidad;

	internal double nuevaCantidad = _nuevaCantidad;

	internal int codAlmacen = _codAlmacen;

	internal int proceso = _proceso;

	internal int codReqAlm = 0;

	internal int codDetalleReqAlm = 0;

	internal string refProducto = "";

	internal bool aprobada = false;
}
