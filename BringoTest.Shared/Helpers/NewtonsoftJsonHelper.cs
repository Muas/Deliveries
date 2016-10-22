using Newtonsoft.Json;

namespace BringoTest.Shared.Helpers
{
	public static class NewtonsoftJsonHelper
	{
		public static bool TryDeserialize<T>(string json, out T result)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				result = default(T);
				return false;
			}

			var isSuccessful = true;
			var settings = new JsonSerializerSettings
			{
				Error = (sender, args) =>
				{
					isSuccessful = false;
					args.ErrorContext.Handled = true;
				}
			};

			result = JsonConvert.DeserializeObject<T>(json, settings);
			return isSuccessful;
		}
	}
}