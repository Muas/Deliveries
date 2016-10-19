using System.Net;

namespace BringoTest.Api.ExceptionFilters
{
	public class ErrorMessage
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public string Message { get; set; }
	}
}