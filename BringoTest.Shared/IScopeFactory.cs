using System;

namespace BringoTest.Shared
{
	public interface IScopeFactory
	{
		IDisposable GetScope();
	}
}