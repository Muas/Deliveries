using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using BringoTest.Data.Models;
using Newtonsoft.Json;
using BringoTest.Shared.Exceptions;
using BringoTest.Shared.Extensions;

namespace BringoTest.Data.Repositories.FileSystem
{
	internal class FileSystemRepository<T> : IRepository<T>
	{
		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
		{
			var fileName = Path.ChangeExtension(typeof(T).Name, ".json");
			var appDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
			var fullPath = Path.Combine(appDataDirectory, fileName);

			if (!File.Exists(fullPath))
			{
				throw new NotFoundException();
			}

			var json = File.ReadAllText(fullPath);

			IEnumerable<T> result;
			if (!NewtonsoftJsonHelper.TryDeserialize(json, out result))
			{
				throw new Exception();
			}
			return result;
		}
	}
}