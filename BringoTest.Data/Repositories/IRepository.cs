using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BringoTest.Data.Repositories
{
	public interface IRepository<T>
	{
		IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
	}
}