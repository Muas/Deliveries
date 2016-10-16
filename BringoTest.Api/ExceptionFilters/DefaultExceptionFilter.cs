using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace BringoTest.Api.ExceptionFilters
{
	public class DefaultExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
			actionExecutedContext.Response.Content = new StringContent(JsonConvert.SerializeObject(new
			{
				error = "Internal server error"
			}));
		}
	}
}