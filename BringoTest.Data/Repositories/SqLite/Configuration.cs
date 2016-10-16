using System.Data.Entity;
using SimpleInjector;

namespace BringoTest.Data.Repositories.SqLite
{
	public static class Configuration
	{
		public static void RegisterDependencies(this Container container)
		{
			container.Register<DbContext, SqLiteContext>(Lifestyle.Scoped);
			container.Register(typeof (IRepository<>), typeof (SqLiteRepository<>), Lifestyle.Scoped);
		}
	}
}