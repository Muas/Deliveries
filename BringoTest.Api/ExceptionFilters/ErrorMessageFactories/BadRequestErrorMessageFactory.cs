using System;
using System.Net;
using BringoTest.Shared.Exceptions;

namespace BringoTest.Api.ExceptionFilters.ErrorMessageFactories
{
	internal sealed class BadRequestErrorMessageFactory : IErrorMessageFactory<BadRequestException>
	{
		public ErrorMessage GetMessage(BadRequestException exception) => new ErrorMessage
		{
			HttpStatusCode = HttpStatusCode.BadRequest,
			Message = "Bad request"
		};

		public ErrorMessage GetMessage(Exception exception) => GetMessage((BadRequestException) exception);
	}
}