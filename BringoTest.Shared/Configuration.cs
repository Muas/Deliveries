using BringoTest.Shared.DataTimeProvider;
using BringoTest.Shared.RandomGenerator;
using SimpleInjector;

namespace BringoTest.Shared
{
	public static class Configuration
	{
		public static void RegisterDependencies(Container container)
		{
			container.Register<IDateTimeProvider, UtcDateTimeProvider>(Lifestyle.Singleton);
			container.Register<IRandomGenerator, DefaultRandomGenerator>(Lifestyle.Singleton);
		}
	}
}