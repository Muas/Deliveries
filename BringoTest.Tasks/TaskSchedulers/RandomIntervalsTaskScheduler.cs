using System;
using System.Timers;
using BringoTest.Shared;
using BringoTest.Shared.Extensions;
using BringoTest.Shared.RandomGenerator;
using BringoTest.Tasks.Tasks;

namespace BringoTest.Tasks.TaskSchedulers
{
	internal sealed class RandomIntervalsTaskScheduler : ITaskScheduler
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IScopeFactory _scopeFactory;
		private readonly IRandomGenerator _randomGenerator;

		public RandomIntervalsTaskScheduler(IServiceProvider serviceProvider, TimeSpan minInterval, TimeSpan maxInterval)
		{
			_serviceProvider = serviceProvider;
			MinInterval = minInterval;
			MaxInterval = maxInterval;
			_scopeFactory = _serviceProvider.GetService<IScopeFactory>();
			_randomGenerator = _serviceProvider.GetService<IRandomGenerator>();
		}

		private TimeSpan MinInterval { get; }
		private TimeSpan MaxInterval { get; }

		public ITaskScheduler Start<TContext>(Func<TContext> taskContextFactory)
		{
			if (taskContextFactory == null) throw new ArgumentNullException(nameof(taskContextFactory));

			var timer = new Timer
			{
				Interval = TimeSpan.FromSeconds(10).TotalMilliseconds,
				AutoReset = false
			};
			timer.Elapsed += (sender, args) => ExecuteTask((Timer)sender, taskContextFactory);
			timer.Start();

			return this;
		}

		private void ExecuteTask<TContext>(Timer timer, Func<TContext> taskContextFactory)
		{
			using (_scopeFactory.GetScope())
			{
				var task = _serviceProvider.GetService<ITask<TContext>>();
				task?.Execute(taskContextFactory());
			}
			timer.Interval = _randomGenerator.NextInt((int) MinInterval.TotalMilliseconds, (int) MaxInterval.TotalMilliseconds);
			timer.Start();
		}
	}
}