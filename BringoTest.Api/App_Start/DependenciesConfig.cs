using System.Configuration;
using System.Web.Http;
using BringoTest.Api.ExceptionFilters;
using BringoTest.Data.Repositories.FileSystem;
using BringoTest.Data.Repositories.SqLite;
using BringoTest.Shared;
using BringoTest.Shared.Enums;
using BringoTest.Shared.ScopeFactory;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace BringoTest.Api
{
	public static class DependenciesConfig
	{
		public static Container Register(HttpConfiguration config)
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			container.RegisterWebApiControllers(config);

			RegisterDataDependencies(container);
			Shared.Configuration.RegisterDependencies(container);
			Tasks.Configuration.RegisterDependencies(container);

			container.Register(typeof(IErrorMessageFactory<>), new[] { typeof(IErrorMessageFactory<>).Assembly });

			container.Register(() => container, Lifestyle.Singleton);
			container.Register<IScopeFactory, SimpleInjectorScopeFactory>(Lifestyle.Singleton);
			
			return container;
		}

		private static void RegisterDataDependencies(Container container)
		{
			var dataSource = Registry.Configuration.DataSource;
			switch (dataSource)
			{
				case DataSource.SQLite:
					SqLiteConfiguration.RegisterDependencies(container);
					break;
				case DataSource.File:
					FileSystemConfiguration.RegisterDependencies(container);
					break;
				default:
					throw new ConfigurationErrorsException("Invalid data source");
			}
		}
	}
}