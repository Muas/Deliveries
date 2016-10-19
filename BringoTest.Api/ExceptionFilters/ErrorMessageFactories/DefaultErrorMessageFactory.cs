using System;
using System.Net;

namespace BringoTest.Api.ExceptionFilters.ErrorMessageFactories
{
	internal sealed class DefaultErrorMessageFactory: IErrorMessageFactory<Exception>
	{
		public ErrorMessage GetMessage(Exception exception) => new ErrorMessage
		{
			HttpStatusCode = HttpStatusCode.InternalServerError,
			Message = "Internal server error"
		};
	}
}