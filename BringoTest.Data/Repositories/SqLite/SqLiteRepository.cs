using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BringoTest.Data.Models;

namespace BringoTest.Data.Repositories.SqLite
{
	internal class SqLiteRepository<T> : IRepository<T> where T : class, IEntity
	{
		private readonly DbContext _context;

		public SqLiteRepository(DbContext context)
		{
			_context = context;
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
		{
			_context.Set<T>().Add(new Delivery {Status = 1, CreationTime = DateTime.Now, Title = "Ushch ushch"} as T);
			_context.SaveChanges();
			IQueryable<T> query = _context.Set<T>();
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return query;
		}
	}
}