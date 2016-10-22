using System;

namespace BringoTest.Shared.RandomGenerator
{
	internal sealed class DefaultRandomGenerator : IRandomGenerator
	{
		private readonly Random _random = new Random();

		public int NextInt(int min, int max)
		{
			if (min > max)
			{
				throw new ArgumentException("Min should be less than max");
			}

			return _random.Next(min, max);
		}
	}
}