using System;
using BringoTest.Shared;
using BringoTest.Shared.Extensions;
using BringoTest.Shared.ScopeFactory;
using BringoTest.Tasks.Tasks;
using Timer = System.Timers.Timer;

namespace BringoTest.Tasks.TaskSchedulers
{
	internal sealed class EqualIntervalsTaskScheduler : ITaskScheduler
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IScopeFactory _scopeFactory;

		public EqualIntervalsTaskScheduler(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_scopeFactory = _serviceProvider.GetService<IScopeFactory>();
		}

		public ITaskScheduler Start<TContext>(Func<TContext> taskContextFactory)
		{
			if (taskContextFactory == null) throw new ArgumentNullException(nameof(taskContextFactory));

			var timer = new Timer
			{
				Interval = TimeSpan.FromSeconds(10).TotalMilliseconds,
				AutoReset = true
			};
			timer.Elapsed += (sender, args) => ExecuteTask(taskContextFactory);
			timer.Start();

			return this;
		}

		private void ExecuteTask<TContext>(Func<TContext> taskContextFactory)
		{
			using (_scopeFactory.GetScope())
			{
				var task = _serviceProvider.GetService<ITask<TContext>>();
				task?.Execute(taskContextFactory());
			}
		}
	}
}