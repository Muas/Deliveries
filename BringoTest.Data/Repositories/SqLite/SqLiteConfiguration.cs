using System;
using System.IO;
using SimpleInjector;
using SQLite;

namespace BringoTest.Data.Repositories.SqLite
{
	public static class SqLiteConfiguration
	{
		public static void RegisterDependencies(Container container)
		{
			var dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DeliveryServiceStorage.db");
			container.Register(() => new SQLiteConnection(dbFilePath) , Lifestyle.Scoped);
			container.Register(typeof (IRepository<>), typeof (SqLiteRepository<>), Lifestyle.Scoped);
		}
	}
}