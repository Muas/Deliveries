using SimpleInjector;

namespace BringoTest.Data.Repositories.FileSystem
{
	public static class FileSystemConfiguration
	{
		public static void RegisterDependencies(Container container)
		{
			container.Register(typeof (IRepository<>), typeof (FileSystemRepository<>), Lifestyle.Scoped);
		}
	}
}