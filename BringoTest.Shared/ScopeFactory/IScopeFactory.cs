using System;

namespace BringoTest.Shared.ScopeFactory
{
	public interface IScopeFactory
	{
		IDisposable GetScope();
	}
}