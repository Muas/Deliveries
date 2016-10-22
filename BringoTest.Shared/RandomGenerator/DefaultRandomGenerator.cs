using System;
using System.Linq;

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

		public string NextString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Range(0, length)
				.Select(_ => _random.Next(chars.Length))
				.Select(i => chars[i])
				.ToArray());
		}
	}
}