using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace BringoTest.Api.ExceptionFilters
{
	public class ExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
			var errorMessageFactoryType = typeof (IErrorMessageFactory<Exception>)
				.GetGenericTypeDefinition()
				.MakeGenericType(actionExecutedContext.Exception.GetType());
			var errorMessageFactory = dependencyResolver.GetService(errorMessageFactoryType)
							?? dependencyResolver.GetService(typeof (IErrorMessageFactory<Exception>));

			var errorMessage = ((IErrorMessageFactory) errorMessageFactory).GetMessage(actionExecutedContext.Exception);

			actionExecutedContext.Response = new HttpResponseMessage(errorMessage.HttpStatusCode)
			{
				Content = new StringContent(JsonConvert.SerializeObject(new
				{
					error = errorMessage.Message
				}))
			};
		}
	}
}