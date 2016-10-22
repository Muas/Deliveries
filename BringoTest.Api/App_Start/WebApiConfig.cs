using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Http;
using AutoMapper;
using BringoTest.Api.ExceptionFilters;
using BringoTest.Api.Mappings;
using BringoTest.Data.Repositories.FileSystem;
using BringoTest.Data.Repositories.SqLite;
using BringoTest.Shared;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;

namespace BringoTest.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			//if (!Debugger.IsAttached)
			{
				config.Filters.Add(new ExceptionFilter());
			}

			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			container.RegisterWebApiControllers(config);

			RegisterDataDependencies(container);
			Shared.Configuration.RegisterDependencies(container);
			Tasks.Configuration.RegisterDependencies(container);

			container.Register(typeof (IErrorMessageFactory<>), new[] {typeof (IErrorMessageFactory<>).Assembly});

			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<DeliveryMappingProfile>();
			});
			mapperConfig.AssertConfigurationIsValid();

			container.Register(() => mapperConfig.CreateMapper(), Lifestyle.Singleton);

			container.Register(() => container, Lifestyle.Singleton);
			container.Register<IScopeFactory, SimpleInjectorScopeFactory>(Lifestyle.Singleton);

			container.Verify();

			config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

			Tasks.Configuration.StartSchedules(container);
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
