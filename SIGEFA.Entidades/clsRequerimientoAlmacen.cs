using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

internal class clsRequerimientoAlmacen
{
	private int codigo;

	private int codTipoDocumento;

	private string numDocumento;

	private int codSerie;

	private string numSerie;

	private int codAlmacenRegistro;

	private string almacenRegistro;

	private int codUserRegistro;

	private string userRegistro;

	private DateTime fechaRegistro;

	private int codUserModifico;

	private string userModifico;

	private DateTime fechaModifico;

	private int codAlmacenSolicitante;

	private string almacenSolicitante;

	private int codAlmacenDespacho;

	private string almacenDespacho;

	private DateTime fechaRequerimiento;

	private int codUserAnulacion;

	private string userAnulacion;

	private DateTime fechaAnulacion;

	private int iEstado;

	private string sEstado;

	private string comentarioSolicitante;

	private string comentarioDespacho;

	private int tipo;

	private List<clsDetalleRequerimientoAlmacen> listadoDetalle;

	private int codPropuestaDePedido;

	private string codPedidoVenta;

	public int Codigo
	{
		get
		{
			return codigo;
		}
		set
		{
			codigo = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return codTipoDocumento;
		}
		set
		{
			codTipoDocumento = value;
		}
	}

	public string NumDocumento
	{
		get
		{
			return numDocumento;
		}
		set
		{
			numDocumento = value;
		}
	}

	public int CodSerie
	{
		get
		{
			return codSerie;
		}
		set
		{
			codSerie = value;
		}
	}

	public string NumSerie
	{
		get
		{
			return numSerie;
		}
		set
		{
			numSerie = value;
		}
	}

	public int CodAlmacenRegistro
	{
		get
		{
			return codAlmacenRegistro;
		}
		set
		{
			codAlmacenRegistro = value;
		}
	}

	public string AlmacenRegistro
	{
		get
		{
			return almacenRegistro;
		}
		set
		{
			almacenRegistro = value;
		}
	}

	public int CodUserRegistro
	{
		get
		{
			return codUserRegistro;
		}
		set
		{
			codUserRegistro = value;
		}
	}

	public string UserRegistro
	{
		get
		{
			return userRegistro;
		}
		set
		{
			userRegistro = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return fechaRegistro;
		}
		set
		{
			fechaRegistro = value;
		}
	}

	public int CodUserModifico
	{
		get
		{
			return codUserModifico;
		}
		set
		{
			codUserModifico = value;
		}
	}

	public string UserModifico
	{
		get
		{
			return userModifico;
		}
		set
		{
			userModifico = value;
		}
	}

	public DateTime FechaModifico
	{
		get
		{
			return fechaModifico;
		}
		set
		{
			fechaModifico = value;
		}
	}

	public int CodAlmacenSolicitante
	{
		get
		{
			return codAlmacenSolicitante;
		}
		set
		{
			codAlmacenSolicitante = value;
		}
	}

	public string AlmacenSolicitante
	{
		get
		{
			return almacenSolicitante;
		}
		set
		{
			almacenSolicitante = value;
		}
	}

	public int CodAlmacenDespacho
	{
		get
		{
			return codAlmacenDespacho;
		}
		set
		{
			codAlmacenDespacho = value;
		}
	}

	public string AlmacenDespacho
	{
		get
		{
			return almacenDespacho;
		}
		set
		{
			almacenDespacho = value;
		}
	}

	public DateTime FechaRequerimiento
	{
		get
		{
			return fechaRequerimiento;
		}
		set
		{
			fechaRequerimiento = value;
		}
	}

	public int CodUserAnulacion
	{
		get
		{
			return codUserAnulacion;
		}
		set
		{
			codUserAnulacion = value;
		}
	}

	public string UserAnulacion
	{
		get
		{
			return userAnulacion;
		}
		set
		{
			userAnulacion = value;
		}
	}

	public DateTime FechaAnulacion
	{
		get
		{
			return fechaAnulacion;
		}
		set
		{
			fechaAnulacion = value;
		}
	}

	public int IEstado
	{
		get
		{
			return iEstado;
		}
		set
		{
			iEstado = value;
		}
	}

	public string SEstado
	{
		get
		{
			return sEstado;
		}
		set
		{
			sEstado = value;
		}
	}

	public string ComentarioSolicitante
	{
		get
		{
			return comentarioSolicitante;
		}
		set
		{
			comentarioSolicitante = value;
		}
	}

	public string ComentarioDespacho
	{
		get
		{
			return comentarioDespacho;
		}
		set
		{
			comentarioDespacho = value;
		}
	}

	public int Tipo
	{
		get
		{
			return tipo;
		}
		set
		{
			tipo = value;
		}
	}

	public int CodPropuestaDePedido
	{
		get
		{
			return codPropuestaDePedido;
		}
		set
		{
			codPropuestaDePedido = value;
		}
	}

	public string CodPedidoVenta
	{
		get
		{
			return codPedidoVenta;
		}
		set
		{
			codPedidoVenta = value;
		}
	}

	public string NombreContacto { get; internal set; }

	public string TelefonoContacto { get; internal set; }

	public string AutorizadoPor { get; internal set; }

	public int Delivery { get; internal set; }

	public string DireccionDelivery { get; internal set; }

	public int CodFacturaVenta { get; internal set; }

	public string TituloFacturaVenta { get; internal set; }

	public string UserAprobador { get; internal set; }

	public DateTime FechaAprobador { get; internal set; }

	public int CodUserAprobador { get; internal set; }

	internal List<clsDetalleRequerimientoAlmacen> ListadoDetalle
	{
		get
		{
			return listadoDetalle;
		}
		set
		{
			listadoDetalle = value;
		}
	}
}
