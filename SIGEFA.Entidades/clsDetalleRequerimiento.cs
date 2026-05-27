using System;

namespace SIGEFA.Entidades;

public class clsDetalleRequerimiento
{
	private string scomentario;

	private int iEstadoVigente;

	public int CodDetalleRequerimiento { get; set; }

	public int CodProducto { get; set; }

	public int CodRequerimiento { get; set; }

	public int Unidad { get; set; }

	public double Cantidad { get; set; }

	public DateTime FechaRegistro { get; set; }

	public int CodUser { get; set; }

	public int Estado { get; set; }

	public int Anulado { get; set; }

	public string Comentario
	{
		get
		{
			return scomentario;
		}
		set
		{
			scomentario = value;
		}
	}

	public int EstadoVigente
	{
		get
		{
			return iEstadoVigente;
		}
		set
		{
			iEstadoVigente = value;
		}
	}

	public bool CodProEquals(int codigo)
	{
		if (CodProducto == codigo)
		{
			return true;
		}
		return false;
	}
}
