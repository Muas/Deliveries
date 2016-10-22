using System.Diagnostics;
using System.Web.Http;
using BringoTest.Api.ExceptionFilters;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace BringoTest.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			if (!Debugger.IsAttached)
			{
				config.Filters.Add(new ExceptionFilter());
			}

			var container = DependenciesConfig.Register(config);

			var mapperConfig = AutomapperConfig.Register();
			container.Register(() => mapperConfig.CreateMapper(), Lifestyle.Singleton);
			
			container.Verify();

			config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

			Tasks.Configuration.StartSchedules(container);
		}
	}
}
