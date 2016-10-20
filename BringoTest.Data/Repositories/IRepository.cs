using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BringoTest.Data.Models;

namespace BringoTest.Data.Repositories
{
	public interface IRepository<T> where T: class, IEntity
	{
		T Get(int id);
		IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
		void Update(T entity);
		void Update(IEnumerable<T> entities);

		T Create(T entity);
	}
}