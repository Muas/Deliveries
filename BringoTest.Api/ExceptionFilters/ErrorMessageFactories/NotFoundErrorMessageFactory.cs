using System;
using System.Net;
using BringoTest.Shared.Exceptions;

namespace BringoTest.Api.ExceptionFilters.ErrorMessageFactories
{
	internal sealed class NotFoundErrorMessageFactory : IErrorMessageFactory<NotFoundException>
	{
		public ErrorMessage GetMessage(NotFoundException exception) => new ErrorMessage
		{
			HttpStatusCode = HttpStatusCode.NotFound,
			Message = "Not Found"
		};

		public ErrorMessage GetMessage(Exception exception) => GetMessage((NotFoundException) exception);
	}
}