using System;
using System.ComponentModel;
using System.Configuration;

namespace BringoTest.Shared
{
	public class Registry
	{
		public static class Configuration
		{
			private static readonly Lazy<DataSource> _dataSource = new Lazy<DataSource>(() => GetConfigurationValue<DataSource>("DataSource"));
			private static readonly Lazy<int> _minTaskCount = new Lazy<int>(() => GetConfigurationValue<int>("MinTaskCount"));
			private static readonly Lazy<int> _maxTaskCount = new Lazy<int>(() => GetConfigurationValue<int>("MaxTaskCount"));
			private static readonly Lazy<int> _minInterval = new Lazy<int>(() => GetConfigurationValue<int>("MinInterval"));
			private static readonly Lazy<int> _maxInterval = new Lazy<int>(() => GetConfigurationValue<int>("MaxInterval"));
			private static readonly Lazy<int> _expirationOffset = new Lazy<int>(() => GetConfigurationValue<int>("ExpirationOffset"));

			public static DataSource DataSource => _dataSource.Value;
			public static int MinTaskCount => _minTaskCount.Value;
			public static int MaxTaskCount => _maxTaskCount.Value;
			public static int MinInterval => _minInterval.Value;
			public static int MaxInterval => _maxInterval.Value;
			public static int ExpirationOffset => _expirationOffset.Value;

			private static T GetConfigurationValue<T>(string key)
			{
				var stringValue = ConfigurationManager.AppSettings[key];
				if (string.IsNullOrWhiteSpace(stringValue))
				{
					throw new ConfigurationErrorsException($"{key} is missing");
				}
				var typeConverter = TypeDescriptor.GetConverter(typeof(T));
				if (!typeConverter.IsValid(stringValue))
				{
					throw new ConfigurationErrorsException($"{key} should be of type {typeof (T).FullName}");
				}

				return (T)typeConverter.ConvertFromString(stringValue);
			}
		}
	}
}