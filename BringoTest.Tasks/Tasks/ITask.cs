namespace BringoTest.Tasks.Tasks
{
	public interface ITask<in T>
	{
		void Execute(T context);
	}
}