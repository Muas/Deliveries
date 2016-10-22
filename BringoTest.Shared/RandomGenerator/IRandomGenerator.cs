using System.Data;

namespace BringoTest.Shared.RandomGenerator
{
	public interface IRandomGenerator
	{
		int NextInt(int min, int max);
		string NextString(int length);
	}
}