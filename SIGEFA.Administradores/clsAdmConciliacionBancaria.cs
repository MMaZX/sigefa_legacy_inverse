using System;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores;

internal class clsAdmConciliacionBancaria
{
	private IConciliacionBancaria Macce = new MysqlConciliacionBancaria();

	public bool insert(clsConciliacionBancaria acce)
	{
		try
		{
			return Macce.Insert(acce);
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool insertdetalle(clsDetalleConciliacion dt)
	{
		try
		{
			return Macce.insertdetalle(dt);
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool update(int codalma, int codbanco, int codcuenta, int CodConciliacion)
	{
		try
		{
			return Macce.Update(codalma, codbanco, codcuenta, CodConciliacion);
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool UpdateBandera(int codalma, int codbanco, int codcuenta, int CodConciliacion)
	{
		try
		{
			return Macce.UpdateBandera(codalma, codbanco, codcuenta, CodConciliacion);
		}
		catch (Exception)
		{
			return false;
		}
	}
}
