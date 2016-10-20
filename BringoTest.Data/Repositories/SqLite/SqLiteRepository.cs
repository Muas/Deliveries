using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BringoTest.Data.Models;
using BringoTest.Shared.DataTimeProvider;
using BringoTest.Shared.Exceptions;
using BringoTest.Shared.Extensions;
using SQLite;

namespace BringoTest.Data.Repositories.SqLite
{
	internal class SqLiteRepository<T> : IRepository<T> where T : class, IEntity, new()
	{
		protected readonly SQLiteConnection Connection;
		protected readonly IDateTimeProvider DateTimeProvider;

		public SqLiteRepository(SQLiteConnection connection, IDateTimeProvider dateTimeProvider)
		{
			Connection = connection;
			DateTimeProvider = dateTimeProvider;
		}

		public T Get(int id)
		{
			var entity = Connection.Find<T>(x => x.Id == id);
			if (entity == null)
			{
				throw new NotFoundException();
			}
			return entity;
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
		{
			var query = Connection.Table<T>();
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return query;
		}

		public void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			entity.ModificationTime = DateTimeProvider.Now();
			Connection.Update(entity);
		}

		public void Update(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException(nameof(entities));
			}

			var now = DateTimeProvider.Now();
			Connection.UpdateAll(entities.ForEach(x => x.ModificationTime = now));
		}

		public T Create(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			entity.CreationTime = entity.ModificationTime = DateTimeProvider.Now();
			var id = Connection.Insert(entity);
			entity.Id = id;
			return entity;
		}
	}
}