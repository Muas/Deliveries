using SimpleInjector;

namespace BringoTest.Data.Repositories.FileSystem
{
	public static class Configuration
	{
		public static void RegisterDependencies(this Container container)
		{
			container.Register(typeof (IRepository<>), typeof (FileSystemRepository<>), Lifestyle.Scoped);
		}
	}
}