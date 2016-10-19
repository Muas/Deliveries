using System;

namespace BringoTest.Api.ExceptionFilters
{
	public interface IErrorMessageFactory
	{
		ErrorMessage GetMessage(Exception exception);
	}

	public interface IErrorMessageFactory<in T> : IErrorMessageFactory
		where T : Exception
	{
		ErrorMessage GetMessage(T exception);
	}
}