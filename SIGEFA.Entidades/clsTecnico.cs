using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsTecnico
{
	private int id;

	private string dni;

	private string nombre;

	private string celular;

	private DateTime fechaNacimiento;

	private string direccion;

	private List<clsOficio> oficios;

	public int Id
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	public string Dni
	{
		get
		{
			return dni;
		}
		set
		{
			dni = value;
		}
	}

	public string Nombre
	{
		get
		{
			return nombre;
		}
		set
		{
			nombre = value;
		}
	}

	public string Celular
	{
		get
		{
			return celular;
		}
		set
		{
			celular = value;
		}
	}

	public DateTime FechaNacimiento
	{
		get
		{
			return fechaNacimiento;
		}
		set
		{
			fechaNacimiento = value;
		}
	}

	public string Direccion
	{
		get
		{
			return direccion;
		}
		set
		{
			direccion = value;
		}
	}

	public string Apellidos { get; internal set; }

	public DateTime FechaRegistro { get; internal set; }

	public int CodUserRegistro { get; internal set; }

	internal List<clsOficio> Oficios
	{
		get
		{
			return oficios;
		}
		set
		{
			oficios = value;
		}
	}
}
