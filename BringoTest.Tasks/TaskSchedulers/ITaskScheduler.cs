using BringoTest.Tasks.Tasks;

namespace BringoTest.Tasks.TaskSchedulers
{
	public interface ITaskScheduler<TTask, TContext> where TTask : ITask<TContext>
	{
		void Start();
	}
}