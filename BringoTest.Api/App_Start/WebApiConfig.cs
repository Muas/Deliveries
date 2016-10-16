using System;
using System.Diagnostics;
using System.Web.Http;
using AutoMapper;
using BringoTest.Api.ExceptionFilters;
using BringoTest.Api.Mappings;
using BringoTest.Data.Repositories.SqLite;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace BringoTest.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
			config.MapHttpAttributeRoutes();

			if (!Debugger.IsAttached)
			{
				config.Filters.Add(new DefaultExceptionFilter());
			}

			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			container.RegisterWebApiControllers(config);
			container.RegisterDependencies();

			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<DeliveryMappingProfile>();
			});
			mapperConfig.AssertConfigurationIsValid();

			container.Register(() => mapperConfig.CreateMapper(), Lifestyle.Singleton);

			container.Verify();

			config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
		}
	}
}
