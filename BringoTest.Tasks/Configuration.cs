using System;
using BringoTest.Shared;
using BringoTest.Tasks.Tasks;
using BringoTest.Tasks.TaskSchedulers;
using SimpleInjector;

namespace BringoTest.Tasks
{
	public static class Configuration
	{
		public static void RegisterDependencies(Container container)
		{
			container.Register(typeof (ITask<>), new[] {typeof (ITask<>).Assembly}, Lifestyle.Scoped);
		}

		public static void StartSchedules(IServiceProvider serviceProvider)
		{
			var expirationOffset = TimeSpan.FromSeconds(Registry.Configuration.ExpirationOffset);
			var minInterval = TimeSpan.FromSeconds(Registry.Configuration.MinInterval);
			var maxInterval = TimeSpan.FromSeconds(Registry.Configuration.MaxInterval);

			new EqualIntervalsTaskScheduler(serviceProvider)
				.Start(() => new ExpireDeliveriesTaskContext());

			new RandomIntervalsTaskScheduler(serviceProvider, minInterval, maxInterval)
				.Start(() => new CreateDeliveriesTaskContext(
					expirationOffset,
					Registry.Configuration.MinTaskCount,
					Registry.Configuration.MaxTaskCount)
				);
		}
	}
}