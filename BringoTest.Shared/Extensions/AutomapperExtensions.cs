using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace BringoTest.Shared.Extensions
{
	public static class AutomapperExtensions
	{
		public static IEnumerable<T> MapAll<T>(this IEnumerable source, IMapper mapper)
		{
			foreach (var item in source)
			{
				yield return mapper.Map<T>(item);
			}
		}

		public static T Map<T>(this object o, IMapper mapper)
		{
			return mapper.Map<T>(o);
		}
	}
}