using System;

namespace BringoTest.Shared.DataTimeProvider
{
	internal sealed class UtcDateTimeProvider : IDateTimeProvider
	{
		public DateTime Now()
		{
			return DateTime.UtcNow;
		}
	}
}