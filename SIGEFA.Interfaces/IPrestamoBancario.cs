using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IPrestamoBancario
{
	bool Insert(clsPrestamoBancario NuevoPreBan);

	bool Delete(int CodPreBan);

	DataTable ListaPrestamos();

	DataTable MuestraPagosPrestamo(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2);

	clsPrestamoBancario CargaPrestamoBancario(int CodPreBan);
}
