namespace SIGEFA.Entidades;

internal class clsDetalleAlmacenPropuestaDePedido
{
	private int _codigoDetallePropuesta;

	private int _codigoAlmacenPDRA;

	private int _codigoProducto;

	public int CodigoDetallePropuesta
	{
		get
		{
			return _codigoDetallePropuesta;
		}
		set
		{
			_codigoDetallePropuesta = value;
		}
	}

	public int CodigoAlmacenPDRA
	{
		get
		{
			return _codigoAlmacenPDRA;
		}
		set
		{
			_codigoAlmacenPDRA = value;
		}
	}

	public int CodigoProducto
	{
		get
		{
			return _codigoProducto;
		}
		set
		{
			_codigoProducto = value;
		}
	}
}
