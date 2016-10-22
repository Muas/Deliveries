using System;
using BringoTest.Shared;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace BringoTest.Api
{
	public class SimpleInjectorScopeFactory : IScopeFactory
	{
		private readonly Container _container;

		public SimpleInjectorScopeFactory(Container container)
		{
			_container = container;
		}

		public IDisposable GetScope()
		{
			return _container.BeginExecutionContextScope();
		}
	}
}