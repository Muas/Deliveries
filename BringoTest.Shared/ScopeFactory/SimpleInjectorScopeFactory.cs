using System;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace BringoTest.Shared.ScopeFactory
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