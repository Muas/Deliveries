using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using BringoTest.Data.Models;
using BringoTest.Shared.DataTimeProvider;
using BringoTest.Shared.Exceptions;
using BringoTest.Shared.Extensions;
using BringoTest.Shared.Helpers;
using Newtonsoft.Json;

namespace BringoTest.Data.Repositories.FileSystem
{
	internal class FileSystemRepository
	{
		protected static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
	}

	internal class FileSystemRepository<T> : FileSystemRepository, IRepository<T> where T : class, IEntity
	{
		private static readonly string FilePath = Path.Combine(
			Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"),
			Path.ChangeExtension(typeof (T).Name, ".json")
			);

		private readonly IDateTimeProvider _dateTimeProvider;

		public FileSystemRepository(IDateTimeProvider dateTimeProvider)
		{
			_dateTimeProvider = dateTimeProvider;
		}

		// todo: использовать StreamReader/StreamWriter для частичной перезаписи файла 
		public T Get(int id)
		{
			var result = GetAll(x => x.Id == id).FirstOrDefault();
			if (result == null)
			{
				throw new NotFoundException();
			}
			return result;
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
		{
			if (!File.Exists(FilePath))
			{
				throw new NotFoundException();
			}

			string json;
			using (Locker.UseReadLock())
			{
				json = File.ReadAllText(FilePath);
			}

			IEnumerable<T> result;
			if (!NewtonsoftJsonHelper.TryDeserialize(json, out result))
			{
				throw new Exception();
			}
			if (filter != null)
			{
				result = result.Where(filter.Compile());
			}
			return result;
		}

		public void Update(T entity)
		{
			if (!File.Exists(FilePath))
			{
				throw new NotFoundException();
			}

			using (var upgradeableLock = Locker.UseUpgradeableLock())
			{
				var json = File.ReadAllText(FilePath);
				List<T> data;
				if (!NewtonsoftJsonHelper.TryDeserialize(json, out data))
				{
					throw new Exception();
				}
				var toUpdate = data.FirstOrDefault(x => x.Id == entity.Id);
				if (toUpdate == null)
				{
					throw new NotFoundException();
				}

				entity.ModificationTime = _dateTimeProvider.Now();
				data[data.IndexOf(toUpdate)] = entity;

				upgradeableLock.ToWrite(); 
				File.WriteAllText(FilePath, JsonConvert.SerializeObject(data));
			}
		}

		public void Update(IEnumerable<T> entities)
		{
			if (!File.Exists(FilePath))
			{
				throw new NotFoundException();
			}

			using (var upgradeableLock = Locker.UseUpgradeableLock())
			{
				var json = File.ReadAllText(FilePath);
				IEnumerable<T> data;
				if (!NewtonsoftJsonHelper.TryDeserialize(json, out data))
				{
					throw new Exception();
				}

				var dictionary = data.ToDictionary(x => x.Id);

				var now = _dateTimeProvider.Now();
				foreach (var entity in entities.Where(entity => dictionary.ContainsKey(entity.Id)))
				{
					entity.ModificationTime = now;
					dictionary[entity.Id] = entity;
				}

				upgradeableLock.ToWrite();
				File.WriteAllText(FilePath, JsonConvert.SerializeObject(dictionary.Values));
			}
		}

		public T Create(T entity)
		{
			if (!File.Exists(FilePath))
			{
				throw new NotFoundException();
			}

			using (var upgradeableLock = Locker.UseUpgradeableLock())
			{
				var json = File.ReadAllText(FilePath);
				List<T> data;
				if (!NewtonsoftJsonHelper.TryDeserialize(json, out data))
				{
					throw new Exception();
				}

				entity.Id = data.Max(x => x.Id) + 1;
				entity.CreationTime = entity.ModificationTime = _dateTimeProvider.Now();
				data.Add(entity);

				upgradeableLock.ToWrite();
				File.WriteAllText(FilePath, JsonConvert.SerializeObject(data));
			}

			return entity;
		}
	}
}