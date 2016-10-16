using System.Data.Entity;
using SimpleInjector;

namespace BringoTest.Data.Repositories.SqLite
{
	public static class Configuration
	{
		public static void RegisterDependencies(this Container container)
		{
			var _dependency = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
			_dependency = typeof(System.Data.SQLite.EF6.SQLiteProviderFactory);

			container.Register<DbContext, SqLiteContext>(Lifestyle.Scoped);
			container.Register(typeof (IRepository<>), typeof (SqLiteRepository<>), Lifestyle.Scoped);
		}
	}
}