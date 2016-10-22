using System;

namespace BringoTest.Tasks.TaskSchedulers
{
	public interface ITaskScheduler
	{
		ITaskScheduler Start<TContext>(Func<TContext> taskContextFactory);
	}
}