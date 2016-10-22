using System;

namespace BringoTest.Tasks.Tasks
{
	public sealed class CreateDeliveriesTaskContext
	{
		public CreateDeliveriesTaskContext(TimeSpan expirationOffset, int minTaskCount, int maxTaskCount)
		{
			if (minTaskCount < 1)
			{
				throw new ArgumentException("Task count should be greated than 0", nameof(minTaskCount));
			}
			if (minTaskCount > maxTaskCount)
			{
				throw new ArgumentException("minTaskCount should be less than maxTaskCount");
			}

			ExpirationOffset = expirationOffset;
			MinTaskCount = minTaskCount;
			MaxTaskCount = maxTaskCount;
		}

		public TimeSpan ExpirationOffset { get; }
		public int MinTaskCount { get; }
		public int MaxTaskCount { get; }
	}
}