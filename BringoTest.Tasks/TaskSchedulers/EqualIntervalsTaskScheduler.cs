using System;
using System.Threading;
using System.Timers;
using BringoTest.Tasks.Tasks;
using Timer = System.Timers.Timer;

namespace BringoTest.Tasks.TaskSchedulers
{
	public class EqualIntervalsTaskScheduler<TTask, TContext> : ITaskScheduler<TTask, TContext> where TTask : ITask<TContext>
	{
		public void Start()
		{
			var timer = new Timer();
			timer.Interval = 12;
			timer.Start();
			timer.AutoReset = false;
			timer.Elapsed += (sender, args) =>
			{
				(sender as Timer).Interval = 13;
				(sender as Timer).Start();
			};
		}
	}
}