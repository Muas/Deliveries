using System;

namespace BringoTest.Shared.Extensions
{
	public static class ServiceProviderExtension
	{
		public static T GetService<T>(this IServiceProvider serviceProvider)
		{
			return (T) serviceProvider.GetService(typeof (T));
		}
	}
}